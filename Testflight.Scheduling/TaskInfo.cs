using MongoDB.Bson;

namespace Testflight.Scheduling
{
    public class TaskInfo
    {
        public bool IsCompleted { get; set; }

        public ObjectId ConfigurationId { get; set; }
    }
}