using System;
using Testflight.DataAccess;

namespace Testflight.Logging
{
    public class DatabaseLogger : Logger, IDatabaseLogger
    {
        protected IMongoSession session;

        public DatabaseLogger(IMongoSession session)
        {
            this.session = session;
        }

        public void Save()
        {
        }
    }
}
