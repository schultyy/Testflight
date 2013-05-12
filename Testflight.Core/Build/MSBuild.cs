using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using TestFlight.Shared;

namespace Testflight.Core
{
    public class MSBuild : IBuilderCapability
    {
        public string Path { get; set; }

        public MSBuild()
        {
            Path = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\MSBuild.exe";
        }

        public IBuildResult Call(string solutionFile, BuildConfiguration buildConfiguration)
        {
            //string projectFileName = @"...\ConsoleApplication3\ConsoleApplication3.sln";
            //ProjectCollection pc = new ProjectCollection();
            //Dictionary<string, string> GlobalProperty = new Dictionary<string, string>();
            //GlobalProperty.Add("Configuration", "Debug");
            //GlobalProperty.Add("Platform", "x86");

            //BuildRequestData BuidlRequest = new BuildRequestData(projectFileName, GlobalProperty, null, new string[] { "Build" }, null);

            //BuildResult buildResult = BuildManager.DefaultBuildManager.Build(new BuildParameters(pc), BuidlRequest);

            var projectCollection = new ProjectCollection();
            var properties = new Dictionary<string, string> { { "Configuration", buildConfiguration.ToString() } };

            var buildRequest = new BuildRequestData(solutionFile, properties, null, new[] { "Build" }, null);

            var result = BuildManager.DefaultBuildManager.Build(new BuildParameters(projectCollection), buildRequest);

            return null;

            //if (string.IsNullOrEmpty(solutionFile))
            //    throw new ArgumentNullException("solutionFile");

            //var results = new BuildResult();

            //var arguments = String.Join(" ", solutionFile, string.Format("/p:Configuration={0}", buildConfiguration));

            //var process = new Process()
            //                  {
            //                      StartInfo = new ProcessStartInfo
            //                                      {
            //                                          //WindowStyle = ProcessWindowStyle.Hidden,
            //                                          //CreateNoWindow = true,
            //                                          FileName = Path,
            //                                          Arguments = arguments,
            //                                          UseShellExecute = false,
            //                                          RedirectStandardError = true,
            //                                          RedirectStandardOutput = true
            //                                      }
            //                  };

            //process.Start();
            //results.StdError = process.StandardError.ReadToEnd();
            //results.StdOut = process.StandardOutput.ReadToEnd();
            //results.ExitCode = process.ExitCode;
            //return results;
        }
    }
}
