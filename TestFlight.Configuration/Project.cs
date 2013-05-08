using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestFlight.Configuration
{
    public class Project
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Project name is required")]
        public string Name { get; set; }

        public UserConfiguration[] Configurations { get; set; }
    }
}
