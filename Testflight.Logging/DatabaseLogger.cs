using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Testflight.Model;
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

        public override void Finished(ObjectId configurationId)
        {
            CreateBuildResult(true, configurationId);
        }

        public override void FinishedWithErrors(ObjectId configurationId)
        {
            CreateBuildResult(false, configurationId);
        }

        private void CreateBuildResult(bool wasSuccessfull, ObjectId configurationId)
        {

            var report = new BuildReport
            {
                ConfigurationId = configurationId,
                LogEntries =
                    logEntries.GroupBy(c => c.Component).ToDictionary(c => c.Key, c => c.ToArray()),
                WasSuccessfull = wasSuccessfull,
                Timestamp = DateTime.Now
            };
            session.Insert(report);
        }
    }
}
