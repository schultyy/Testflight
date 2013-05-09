﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using TestFlight.Configuration;
using Testflight.Core;
using Testflight.Core.Build;
using Testflight.Core.Publish;
using Testflight.DataAccess;

namespace Testflight.Scheduling
{
    public interface IScheduler
    {
        IMongoSession Session { get; set; }
    }

    public class Scheduler : IScheduler
    {
        [Dependency]
        public IMongoSession Session { get; set; }

        [Dependency]
        public IBuilderCapability BuilderCapability { get; set; }

        [Dependency]
        public IFilesystemProvider FilesystemProvider { get; set; }

        [Dependency]
        public BuildPublisher Publisher { get; set; }

        [Dependency]
        public Builder Builder { get; set; }

        [Dependency]
        public ILogger Logger { get; set; }

        public Scheduler()
        {

        }

        public void QueueNew(ObjectId configurationId)
        {
            if (configurationId == ObjectId.Empty)
                throw new ArgumentException("Configuration Id can not be empty");

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
                    Publisher.PublishPackages(Path.Combine(baseDirectory, "bin", "Debug"),
                                              Path.Combine(baseDirectory, "bin", "Debug", "package"));
                return;
            }
        }
    }
}
