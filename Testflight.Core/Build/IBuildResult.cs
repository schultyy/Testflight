namespace Testflight.Core
{
    public interface IBuildResult
    {
        string StdOut { get; }
        string StdError { get; }
        int ExitCode { get; set; }
    }
}