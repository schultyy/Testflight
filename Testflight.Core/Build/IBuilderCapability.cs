using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFlight.Shared;

namespace Testflight.Core
{
    public interface IBuilderCapability
    {
        IBuildResult Call(string solutionFile, BuildConfiguration buildConfiguration);
    }
}
