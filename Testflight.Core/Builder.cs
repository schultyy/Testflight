using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Run(string solutionFile, BuildConfiguration configuration = BuildConfiguration.Release, string target = "Build")
        {
            if (string.IsNullOrEmpty(solutionFile))
                throw new ArgumentNullException("solutionFile");

            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");

            BuilderCapability.Call(solutionFile, configuration);
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
