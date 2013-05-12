﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Testflight.Shared;

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
            var projectCollection = new ProjectCollection();
            var properties = new Dictionary<string, string> { { "Configuration", buildConfiguration.ToString() } };

            var buildRequest = new BuildRequestData(solutionFile, properties, null, new[] { "Build" }, null);

            var result = BuildManager.DefaultBuildManager.Build(new BuildParameters(projectCollection), buildRequest);

            return new BuildResult
                       {
                           ExitCode = Convert(result.OverallResult),
                           TargetResults = result.ResultsByTarget.Select(c => new Build.TargetResult
                                                                                  {
                                                                                      Component = c.Key,
                                                                                      Exception = c.Value.Exception,
                                                                                      Result = Convert(c.Value.ResultCode)
                                                                                  })
                                                                                  .ToArray()
                       };
        }

        private ResultCode Convert(TargetResultCode buildResultCode)
        {
            switch (buildResultCode)
            {
                case TargetResultCode.Skipped:
                    return ResultCode.Skipped;
                case TargetResultCode.Success:
                    return ResultCode.Success;
                case TargetResultCode.Failure:
                    return ResultCode.Failure;
                default:
                    throw new ArgumentOutOfRangeException("buildResultCode");
            }
        }

        private ResultCode Convert(BuildResultCode buildResultCode)
        {
            switch (buildResultCode)
            {
                case BuildResultCode.Success:
                    return ResultCode.Success;
                case BuildResultCode.Failure:
                    return ResultCode.Failure;
                default:
                    throw new ArgumentOutOfRangeException("buildResultCode");
            }
        }
    }
}
