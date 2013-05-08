using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Testflight.Web
{
    public class MongoSession<T> : IDisposable
    {
        private string _connectionString;

        private MongoServer server;

        public MongoSession()
        {
            //set this connection as you need. This is left here as an example, but you could, if you wanted,
            _connectionString = "mongodb://127.0.0.1/MyDatabase?strict=false";
        }

        public void Dispose()
        {
            server.Disconnect();
        }
    }
}