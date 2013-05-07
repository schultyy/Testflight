using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Testflight.Core;
using Testflight.Core.Build;
using Testflight.Core.Configuration;
using Testflight.Core.Publish;

namespace Testflight.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided.");
                Console.WriteLine("Nothing to do - Exit");
#if DEBUG
                Console.ReadLine();
#endif
                return;
            }

            try
            {
                UserConfiguration configFile = null;

                Console.WriteLine(string.Format("Reading configuration {0}", args.First()));

                using (var reader = new StreamReader(args.First()))
                {
                    var deserializer = new XmlSerializer(typeof(UserConfiguration));
                    configFile = deserializer.Deserialize(reader) as UserConfiguration;
                }

                if (configFile == null)
                    throw new ApplicationException("Failed to deserialize user configuration - Aborting");

                Console.WriteLine("Validating configuration");

                if (string.IsNullOrEmpty(configFile.SolutionFile))
                    throw new ArgumentNullException("Solution file can not be null or empty");

                if (string.IsNullOrEmpty(configFile.Target))
                    throw new ArgumentNullException("Target can not be null or empty");

                if (string.IsNullOrEmpty(configFile.BaseDirectory))
                    throw new ArgumentNullException("Base directory can not be null or empty");

                Console.WriteLine(string.Format("Solution file = {0}", configFile.SolutionFile));
                Console.WriteLine(string.Format("Build configuration = {0}", configFile.BuildConfiguration));
                Console.WriteLine(string.Format("Base directory = {0}", configFile.BaseDirectory));
                Console.WriteLine(string.Format("Target = {0}", configFile.Target));

                var logger = new Logger();

                var msBuild = new MSBuild();

                var filesystemProvider = new FilesystemProvider();

                var publisher = new BuildPublisher(filesystemProvider);

                var builder = new Builder(logger, msBuild);

                var baseDirectory = Path.GetFullPath(configFile.BaseDirectory);

                var solutionFile = Path.Combine(baseDirectory, configFile.SolutionFile);

                var buildWasSuccessfull = builder.Run(solutionFile, configFile.BuildConfiguration, configFile.Target);

                if (buildWasSuccessfull)
                    publisher.PublishPackages(Path.Combine(baseDirectory, "bin", "Debug"),
                                              Path.Combine(baseDirectory, "bin", "Debug", "package"));

                var logFilename = Path.Combine("Log",
                                               string.Format("{0}_{1}.xml", configFile.Name,
                                                             DateTime.Now.ToString().Replace(":", "_")));

                Console.WriteLine("Writing log to {0}", logFilename);

                if (!Directory.Exists("Log"))
                    Directory.CreateDirectory("Log");
                logger.WriteToFile(logFilename);
#if DEBUG
                Console.ReadLine();
#endif
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exc.Message);

#if DEBUG
                Console.ReadLine();
#endif
            }
        }
    }
}
