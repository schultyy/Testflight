using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Testflight.Core
{
    public class Logger : ILogger
    {
        private readonly List<LogEntry> logEntries;

        public Logger()
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

        public void WriteToFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");
            using (var streamWriter = new StreamWriter(filename))
            {
                var serializer = new XmlSerializer(logEntries.GetType());
                serializer.Serialize(streamWriter, logEntries.OrderBy(c => c.TimeStamp).ToList());
            }
        }
    }
}
