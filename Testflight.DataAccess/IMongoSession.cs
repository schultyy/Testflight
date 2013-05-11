using System.Linq;
using MongoDB.Bson;

namespace Testflight.DataAccess
{
    public interface IMongoSession
    {
        void Insert<T>(T item)
            where T : class;

        IQueryable<T> GetAll<T>();
        T GetById<T>(ObjectId id);
    }
}