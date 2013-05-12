using Testflight.Shared;

namespace Testflight.Core
{
    public interface IBuilderCapability
    {
        IBuildResult Call(string solutionFile, BuildConfiguration buildConfiguration);
    }
}
