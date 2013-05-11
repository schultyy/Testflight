using Testflight.DataAccess;

namespace Testflight.Scheduling
{
    public interface IScheduler
    {
        IMongoSession Session { get; set; }
    }
}