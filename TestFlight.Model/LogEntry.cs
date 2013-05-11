using System;

namespace TestFlight.Model
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

        public string Component { get; set; }
    }
}