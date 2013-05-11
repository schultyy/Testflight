using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Testflight.Logging
{
    public class FileLogger : Logger
    {
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

        public override void Finished()
        {
            var logFilename = Path.Combine("Log",
                                               string.Format("{0}.xml", DateTime.Now.ToString().Replace(":", "_")));
            WriteToFile(logFilename);
        }

        public override void FinishedWithErrors()
        {
            Finished();
        }
    }
}
