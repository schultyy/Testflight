using System;

namespace Testflight.Logging
{
    public interface ILogger
    {
        void Error(string component, string errorMessage);
        void Info(string component, string infoMessage);
        void Error(string component, AggregateException exceptions);
        void Finished();
        void FinishedWithErrors();
    }
}