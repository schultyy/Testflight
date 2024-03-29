﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Testflight.Model
{
    public class BuildReport
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId ConfigurationId { get; set; }

        public Dictionary<string, LogEntry[]> LogEntries { get; set; }

        public bool WasSuccessful { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
