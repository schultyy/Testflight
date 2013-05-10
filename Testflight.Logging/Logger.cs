using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TestFlight.Model;

namespace Testflight.Logging
{
    public abstract class Logger : ILogger
    {
        protected readonly List<LogEntry> logEntries;

        protected Logger()
        {
            logEntries = new List<LogEntry>();
        }

        public void Error(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
                throw new ArgumentNullException("errorMessage");
            logEntries.Add(new LogEntry
                               {
                                   Category = Categories.Error,
                                   Message = errorMessage
                               });
        }

        public void Info(string infoMessage)
        {
            if (string.IsNullOrEmpty(infoMessage))
                throw new ArgumentNullException("infoMessage");
            logEntries.Add(new LogEntry
                               {
                                   Category = Categories.Info,
                                   Message = infoMessage
                               });
        }
    }
}
