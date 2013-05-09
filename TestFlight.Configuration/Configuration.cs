using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TestFlight.Shared;

namespace TestFlight.Configuration
{
    public class Configuration
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public string BaseDirectory { get; set; }

        public string SolutionFile { get; set; }

        public BuildConfiguration BuildConfiguration { get; set; }

        public string Target { get; set; }
    }
}
