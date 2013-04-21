using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            if (string.IsNullOrEmpty(solutionFile))
                throw new ArgumentNullException("solutionFile");

            var results = new BuildResult();

            var arguments = String.Join(" ", solutionFile, string.Format("/p:Configuration={0}", buildConfiguration));

            var process = new Process()
                              {
                                  StartInfo = new ProcessStartInfo
                                                  {
                                                      FileName = Path,
                                                      Arguments = arguments,
                                                      UseShellExecute = false,
                                                      CreateNoWindow = true,
                                                      RedirectStandardError = true,
                                                      RedirectStandardOutput = true
                                                  }
                              };

            process.Start();

            results.StdError = process.StandardError.ReadToEnd();
            results.StdOut = process.StandardOutput.ReadToEnd();

            return results;
        }
    }

    public class BuildResult : IBuildResult
    {
        public BuildResult()
        {

        }

        public string StdOut { get; set; }
        public string StdError { get; set; }
    }
}
