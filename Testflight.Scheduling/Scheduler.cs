using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using Testflight.Core.Build;
using Testflight.Core.Publish;
using Testflight.Model;
using Testflight.Core;
using Testflight.DataAccess;
using Testflight.ErrorHandling;
using Testflight.Logging;

namespace Testflight.Scheduling
{
    public class Scheduler : IScheduler
    {
        public IMongoSession Session { get; set; }

        public IBuilderCapability BuilderCapability { get; set; }

        public IFilesystemProvider FilesystemProvider { get; set; }

        public BuildPublisher Publisher { get; set; }

        public Builder Builder { get; set; }

        public ILogger Logger { get; set; }

        private readonly Dictionary<ObjectId, Task> taskHandles;

        public Scheduler(IMongoSession session,
                        IBuilderCapability builderCapability,
                        IFilesystemProvider filesystemProvider,
                        BuildPublisher publisher,
                        Builder builder,
                        ILogger logger)
        {
            Session = session;
            BuilderCapability = builderCapability;
            FilesystemProvider = filesystemProvider;
            Publisher = publisher;
            Builder = builder;
            Logger = logger;
            taskHandles = new Dictionary<ObjectId, Task>();
        }

        public void QueueNew(ObjectId configurationId)
        {
            if (configurationId == ObjectId.Empty)
                throw new ArgumentException("Configuration Id can not be empty");

            if (taskHandles.ContainsKey(configurationId))
                throw new ArgumentException(string.Format("There is already a job running for configuration {0}", configurationId));

            var taskHandle = Task.Factory.StartNew(() =>
                                                       {
                                                           var configuration = Session.GetById<Configuration>(configurationId);

                                                           if (configuration == null)
                                                               throw new ArgumentException(string.Format("No configuration with Id = {0} found", configurationId));

                                                           var validationResults = new List<ValidationResult>();

                                                           if (Validator.TryValidateObject(configuration, new ValidationContext(configuration, null, null), validationResults))
                                                           {
                                                               var baseDirectory = Path.GetFullPath(configuration.BaseDirectory);

                                                               var solutionFile = Path.Combine(baseDirectory, configuration.SolutionFile);

                                                               var buildWasSuccessfull = Builder.Run(solutionFile, configuration.BuildConfiguration, configuration.Target);

                                                               if (buildWasSuccessfull)
                                                                   Publisher.PublishPackages(Path.Combine(baseDirectory, "bin", configuration.BuildConfiguration.ToString()),
                                                                                             Path.Combine(baseDirectory, "bin", configuration.BuildConfiguration.ToString(), "package"));
                                                               return;
                                                           }

                                                           var message = string.Format("Configuration is not valid:\n{0}",
                                                               string.Join("\n", validationResults.Select(c => c.ErrorMessage).ToArray()));

                                                           throw new ConfigurationValidationException(message);
                                                       }).ContinueWith(t =>
                                                        {
                                                            taskHandles.Remove(configurationId);

                                                            if (t.Exception == null)
                                                            {
                                                                Logger.Finished(configurationId);
                                                                return;
                                                            }
                                                            Logger.Error("General", t.Exception);
                                                            Logger.FinishedWithErrors(configurationId);
                                                        });
            taskHandles.Add(configurationId, taskHandle);
        }

        public TaskInfo[] GetTasks()
        {
            return taskHandles.Select(c => new TaskInfo
                                               {
                                                   IsCompleted = c.Value.IsCompleted,
                                                   ConfigurationId = c.Key
                                               })
                                .ToArray();
        }
    }
}
