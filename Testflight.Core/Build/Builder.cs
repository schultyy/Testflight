using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFlight.Shared;
using Testflight.Logging;

namespace Testflight.Core
{
    public class Builder
    {
        public ILogger Logger { get; set; }
        public IBuilderCapability BuilderCapability { get; set; }

        public Builder(ILogger logger, IBuilderCapability builderCapability)
        {
            this.Logger = logger;
            this.BuilderCapability = builderCapability;
        }

        public bool Run(string solutionFile, BuildConfiguration configuration = BuildConfiguration.Release, string target = "Build")
        {
            if (string.IsNullOrEmpty(solutionFile))
                throw new ArgumentNullException("solutionFile");

            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");

            var results = BuilderCapability.Call(solutionFile, configuration);

            if (!string.IsNullOrEmpty(results.StdOut))
                Logger.Info("Build", results.StdOut);

            if (!string.IsNullOrEmpty(results.StdError))
                Logger.Error("Build", results.StdError);

            return results.ExitCode == 0;
        }
    }
}
