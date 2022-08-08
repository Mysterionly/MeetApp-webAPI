using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Models
{
    public class EventType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("DefaultName")]
        public string DefaultName { get; set; }

        //[BsonElement("Icon")]
        //public byte[] Icon { get; set; }

        //[BsonElement("Color")]
        //public string Color { get; set; }
    }
}
