using System;
using MongoDB.Bson;

namespace Testflight.Logging
{
    public interface ILogger
    {
        void Error(string component, string errorMessage);
        void Info(string component, string infoMessage);
        void Error(string component, AggregateException exceptions);
        void Finished(ObjectId configurationId);
        void FinishedWithErrors(ObjectId configurationId);
        void Error(string component, Exception exception);
    }
}