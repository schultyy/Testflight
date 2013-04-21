using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core
{
    public interface IBuilderCapability
    {
        IBuildResult Call(string solutionFile, BuildConfiguration buildConfiguration);
    }

    public interface IBuildResult
    {
        string StdOut { get; }
        string StdError { get; }
        int ExitCode { get; set; }
    }
}
