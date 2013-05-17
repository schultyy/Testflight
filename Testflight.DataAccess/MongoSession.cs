using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Testflight.Model;

namespace Testflight.DataAccess
{
    public class MongoSession : IMongoSession
    {
        private readonly MongoServer server;

        private readonly MongoDatabase database;

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

            var collection = database.GetCollection<T>(typeof(T).Name);
            collection.Insert(item);
        }

        public void Update<T>(T item) where T : class
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var collection = database.GetCollection<T>(typeof(T).Name);
            collection.Save(item);
        }

        public IQueryable<T> GetAll<T>()
            where T : class
        {
            var result = database.GetCollection<T>(typeof(T).Name).FindAll();
            return result.AsQueryable();
        }

        public T GetById<T>(ObjectId id)
            where T : class
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return database.GetCollection<T>(typeof(T).Name).FindOne(Query.EQ("_id", id));
        }

        public void Delete<T>(ObjectId id)
                where T : class
        {
            database.GetCollection<T>(typeof(T).Name).Remove(Query.EQ("_id", id));
        }
    }
}