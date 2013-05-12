using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using TestFlight.Model;

namespace Testflight.Web.Models
{
    public class ConfigurationDetailViewModel
    {
        public BuildReport[] BuildReports { get; set; }

        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}