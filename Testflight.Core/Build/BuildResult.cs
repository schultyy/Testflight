namespace Testflight.Core
{
    public class BuildResult : IBuildResult
    {
        public string StdOut { get; set; }
        public string StdError { get; set; }

        public int ExitCode { get; set; }
    }
}