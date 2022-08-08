using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Models
{

    public class UserEvent
    {
        public string UserId { get; set; }
        public string EventId { get; set; }
    }
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Host")]
        public string Host { get; set; }

        [BsonElement("EventType")]
        public string EventType { get; set; }

        [BsonElement("Guests")]
        public List<String> Guests { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        //[BsonElement("Public")]
        //public bool Public { get; set; }

        //[BsonElement("MaxPublic")]
        //public int MaxPublic { get; set; }

        //[BsonElement("UnlimitedPublic")]
        //public bool UnlimitedPublic { get; set; }

        [BsonElement("Location")]
        public Location Location { get; set; }

        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("PlannedDate")]
        public DateTime PlannedDate { get; set; }


        [BsonElement("Address")]
        public String Address { get; set; }



        //[BsonElement("Occured")]
        //public bool Occured { get; set; }

        public Event()
        {
            Location = new Location();            
        }
    }

    public class Location
    {
        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }
    }

//    public class EventDB : Event
//    {
//        [BsonElement("EventTypeId")]
//        public string EventTypeId { get; set; }
//    }

//    public class EventFull : Event
//    {
//        [BsonElement("EventType")]
//        public EventType EventType { get; set; }
//    }
}
