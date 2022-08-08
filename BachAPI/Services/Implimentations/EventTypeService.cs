using BachAPI.Models;
using BachAPI.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BachAPI.Services.Implimentations
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IMongoCollection<EventType> _eventTypes;
        
        public EventTypeService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _eventTypes = database.GetCollection<EventType>("eventTypes");
        }

        public EventType Get(string id)
        {
            return _eventTypes.Find<EventType>(eventType => eventType.Id == id).FirstOrDefault();
        }

        public List<EventType> Gets()
        {
            return _eventTypes.Find(eventType => true).ToList();
        }

        public EventType Create(EventType eventType)
        {
            _eventTypes.InsertOne(eventType);
            return eventType;
        }

        public void Update(string id, EventType eventIn)
        {            
            _eventTypes.ReplaceOne(eventType => eventType.Id == id, eventIn);
        }

        public void Remove(EventType eventIn)
        {
            _eventTypes.DeleteOne(eventType => eventType.Id == eventIn.Id);
        }        

        public void Remove(string id)
        {
            _eventTypes.DeleteOne(eventType => eventType.Id == id);
        }
    }
}
