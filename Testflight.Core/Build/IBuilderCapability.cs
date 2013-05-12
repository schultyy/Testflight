using Testflight.Shared;

namespace Testflight.Core.Build
{
    public interface IBuilderCapability
    {
        IBuildResult Call(string solutionFile, BuildConfiguration buildConfiguration);
    }
}
