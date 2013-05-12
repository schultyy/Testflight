using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testflight.Shared;
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

            foreach (var targetResult in results.TargetResults)
            {
                switch (targetResult.Result)
                {
                    case ResultCode.Failure:
                        Logger.Error(targetResult.Component, targetResult.Exception);
                        break;
                    case ResultCode.Success:
                    case ResultCode.Skipped:
                        Logger.Info(targetResult.Component, targetResult.Result.ToString());
                        break;
                }
            }

            return results.ExitCode == ResultCode.Success;
        }
    }
}
