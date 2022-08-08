using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Libmongocrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("GoogleID")]
        public string GoogleID { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        //[BsonElement("HomeLocation")]
        //public HomeLocation HomeLocation { get; set; }

        //[BsonElement("CurrentLocation")]
        //public CurrentLocation CurrentLocation { get; set; }

        //[BsonElement("Avatar")]
        //public byte[] Avatar { get; set; }

        public User() 
        {
            //HomeLocation = new HomeLocation();
            //CurrentLocation = new CurrentLocation();
        }
    }

    //public class HomeLocation
    //{
    //    [BsonElement("Longitude")]
    //    public double Longitude { get; set; }

    //    [BsonElement("Latitude")]
    //    public double Latitude { get; set; }
    //}

    //public class CurrentLocation
    //{
    //    [BsonElement("Longitude")]
    //    public double Longitude { get; set; }

    //    [BsonElement("Latitude")]
    //    public double Latitude { get; set; }
    //}

    //public class UserDB : User
    //{
    //    [BsonElement("UserEvents")]
    //    public List<string> UserEvents { get; set; }

    //    [BsonElement("JoinedEvents")]
    //    public List<string> JoinedEvents { get; set; }
    //}

    //public class UserFull : User
    //{
    //    [BsonElement("UserEvents")]
    //    public List<Event> UserEvents { get; set; }

    //    [BsonElement("JoinedEvents")]
    //    public List<Event> JoinedEvents { get; set; }
    //}
}
