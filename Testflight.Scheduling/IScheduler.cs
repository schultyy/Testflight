using MongoDB.Bson;
using Testflight.DataAccess;

namespace Testflight.Scheduling
{
    public interface IScheduler
    {
        IMongoSession Session { get; set; }
        void QueueNew(ObjectId configurationId);
        TaskInfo[] GetTasks();
    }
}