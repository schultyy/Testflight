using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Testflight.Shared;

namespace Testflight.Model
{
    public class Configuration
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Base directory is required")]
        public string BaseDirectory { get; set; }

        [Required(ErrorMessage = "Solution filename is required")]
        public string SolutionFile { get; set; }

        public BuildConfiguration BuildConfiguration { get; set; }

        public string Target { get; set; }
    }
}
