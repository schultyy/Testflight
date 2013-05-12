using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using Testflight.Model;

namespace Testflight.Web.Models
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public ConfigurationViewModel[] Configurations { get; set; }
    }

    public class ConfigurationViewModel
    {
        public ObjectId Id { get; set; }

        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public bool WasLastBuildSuccessful { get; set; }
    }
}