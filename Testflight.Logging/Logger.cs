using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using MongoDB.Bson;
using Testflight.Model;

namespace Testflight.Logging
{
    public abstract class Logger : ILogger
    {
        protected readonly List<LogEntry> logEntries;

        protected Logger()
        {
            logEntries = new List<LogEntry>();
        }

        public void Error(string component, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
                throw new ArgumentNullException("errorMessage");
            if (string.IsNullOrEmpty(component))
                throw new ArgumentNullException("component");
            logEntries.Add(new LogEntry
                               {
                                   Category = Categories.Error,
                                   Message = errorMessage,
                                   Component = component,
                                   TimeStamp = DateTime.Now
                               });
        }

        public void Info(string component, string infoMessage)
        {
            if (string.IsNullOrEmpty(infoMessage))
                throw new ArgumentNullException("infoMessage");
            if (string.IsNullOrEmpty(component))
                throw new ArgumentNullException("component");
            logEntries.Add(new LogEntry
                               {
                                   Category = Categories.Info,
                                   Message = infoMessage,
                                   Component = component,
                                   TimeStamp = DateTime.Now
                               });
        }

        public void Error(string component, AggregateException exceptions)
        {
            foreach (var innerException in exceptions.InnerExceptions)
                Error(component, innerException);
        }

        public void Error(string component, Exception exception)
        {
            if (string.IsNullOrEmpty(component))
                throw new ArgumentNullException("component");

            logEntries.Add(new LogEntry
                               {
                                   Category = Categories.Error,
                                   Component = component,
                                   Message = exception == null ? string.Empty : exception.Message,
                                   TimeStamp = DateTime.Now
                               });
        }

        public abstract void Finished(ObjectId configurationId);

        public abstract void FinishedWithErrors(ObjectId configurationId);
    }
}
