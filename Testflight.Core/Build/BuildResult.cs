namespace Testflight.Core.Build
{
    public class BuildResult : IBuildResult
    {
        public ITargetResult[] TargetResults { get; set; }
        public ResultCode ExitCode { get; set; }
    }
}