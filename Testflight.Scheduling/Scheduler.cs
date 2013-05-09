using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Testflight.DataAccess;

namespace Testflight.Scheduling
{
    public interface IScheduler
    {
        IMongoSession Session { get; set; }
    }

    public class Scheduler : IScheduler
    {
        [Dependency]
        public IMongoSession Session { get; set; }

        public Scheduler()
        {

        }
    }
}
