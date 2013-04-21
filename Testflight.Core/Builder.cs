using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core
{
    public class Builder
    {
        public string Path { get; set; }

        public ILogger Logger { get; set; }

        public Builder(ILogger logger)
        {
            Path = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\MSBuild.exe";
        }

        public void Run(string solutionFile, BuildConfiguration configuration = BuildConfiguration.Release, string target = "Build")
        {

        }
    }

    public interface ILogger
    {
    }


    public enum BuildConfiguration
    {
        Debug,
        Release
    }

}
