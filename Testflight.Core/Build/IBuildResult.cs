using System;

namespace Testflight.Core.Build
{
    public interface IBuildResult
    {
        ITargetResult[] TargetResults { get; set; }
        ResultCode ExitCode { get; set; }
    }

    public interface ITargetResult
    {
        Exception Exception { get; set; }
        ResultCode Result { get; set; }
        string Component { get; set; }
    }

    public enum ResultCode
    {
        Success,
        Failure,
        Skipped
    }
}