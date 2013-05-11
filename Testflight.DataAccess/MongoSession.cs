using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TestFlight.Model;

namespace Testflight.DataAccess
{
    public class MongoSession : IMongoSession
    {
        private MongoServer server;

        private MongoDatabase database;

        public MongoSession()
        {
            //set this connection as you need. This is left here as an example, but you could, if you wanted,
            //_connectionString = "mongodb://127.0.0.1/MyDatabase?strict=false";
            var client = new MongoClient("mongodb://localhost:27017");

            server = client.GetServer();

            database = server.GetDatabase("Testflight");
        }

        public void Insert<T>(T item)
            where T : class
        {
            if (item == null)
                throw new ArgumentNullException("item");

            server.Connect();

            var projectCollection = database.GetCollection<Project>(typeof(T).Name);
            projectCollection.Insert(item);
        }

        public IQueryable<T> GetAll<T>()
        {
            var result = database.GetCollection<T>(typeof(T).Name).FindAll();
            return Queryable.AsQueryable<T>(result);
        }

        public T GetById<T>(ObjectId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return database.GetCollection<T>(typeof(T).Name).FindOne(Query.EQ("_id", id));
        }
    }
}