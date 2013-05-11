using System;
using System.Collections.Generic;
using System.Linq;
using TestFlight.Model;
using Testflight.DataAccess;

namespace Testflight.Logging
{
    public class DatabaseLogger : Logger
    {
        protected IMongoSession session;

        public DatabaseLogger(IMongoSession session)
        {
            this.session = session;
        }

        public override void Finished()
        {
            CreateBuildResult(true);
        }

        public override void FinishedWithErrors()
        {
            CreateBuildResult(false);
        }

        private void CreateBuildResult(bool wasSuccessfull)
        {

            var report = new BuildReport
            {
                LogEntries =
                    logEntries.GroupBy(c => c.Component).ToDictionary(c => c.Key, c => c.ToArray()),
                WasSuccessfull = wasSuccessfull,
                Timestamp = DateTime.Now
            };
            session.Insert(report);
        }
    }
}
