using System;

namespace Testflight.Core
{
    [Serializable]
    public class LogEntry
    {
        public LogEntry()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }

        public Categories Category { get; set; }
    }
}