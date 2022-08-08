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
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _events;
        private readonly IMongoCollection<User> _users;

        public EventService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<Event>("events");
            _users = database.GetCollection<User>("users");
        }

        public Event Get(string id)
        {
            return _events.Find<Event>(myevent => myevent.Id == id).FirstOrDefault();
        }

        public List<Event> Gets()
        {
            List<Event> evnts = _events.Find(m => m.PlannedDate > DateTime.Now.AddDays(-1)).ToList();

            foreach (Event e in evnts)
            {
                List<string> sss = new List<string>();
                if (e.Guests != null)
                {
                    foreach (string s in e.Guests)
                    {
                        sss.Add(_users.Find(u => u.Id == s).First().Username);
                    }
                }
                e.Guests = sss;
                e.PlannedDate = e.PlannedDate.AddHours(3);
            }
            return evnts;
        }

        public List<Event> GetEventsImHost(string uid)
        {
            List<Event> evnts = _events.Find(e => e.Host == uid).ToList();
            foreach (Event e in evnts)
            {
                List<string> sss = new List<string>();
                if (e.Guests != null)
                {
                    foreach (string s in e.Guests)
                    {
                        sss.Add(_users.Find(u => u.Id == s).First().Username);
                    }
                }
                e.Guests = sss;
                e.PlannedDate = e.PlannedDate.AddHours(3);
            }
            return evnts;

        }
        public List<Event> GetEventsImGuest(string uid)
        {
            List<Event> evnts = _events.Find(e => e.Guests.Contains(uid)).ToList();
            foreach (Event e in evnts)
            {
                List<string> sss = new List<string>();
                if (e.Guests != null)
                {
                    foreach (string s in e.Guests)
                    {
                        sss.Add(_users.Find(u => u.Id == s).First().Username);
                    }
                }
                e.Guests = sss;
                e.PlannedDate = e.PlannedDate.AddHours(3);
            }
            return evnts;
        }

        public List<string> AddOrRemoveMeToGuestList(UserEvent ue)
        {
            Event e = _events.Find(e => e.Id == ue.EventId).First();
            if (e.Guests == null)
            {
                e.Guests = new List<string>();
                e.Guests.Add(ue.UserId);
            }
            else if (e.Guests.Contains(ue.UserId))
            {
                e.Guests.Remove(ue.UserId);
            }
            else
            {
                e.Guests.Add(ue.UserId);
            }

            _events.ReplaceOne(myevent => myevent.Id == ue.EventId, e);
            return GetUsers(ue.EventId);
        }

        public Event Create(Event myevent)
        {
            _events.InsertOne(myevent);
            return myevent;
        }

        public List<String> GetUsers(string eventId)
        {
            List<String> guestList = new List<String>();
            Event e = _events.Find(t => t.Id == eventId).First();
            foreach(string u in e.Guests)
            {
                guestList.Add(_users.Find(q => q.Id == u).First().Username);
            }
            return guestList;
        }

        public void Update(string id, Event eventIn)
        {            
            _events.ReplaceOne(myevent => myevent.Id == id, eventIn);
        }

        public void Remove(Event eventIn)
        {
            _events.DeleteOne(myevent => myevent.Id == eventIn.Id);
        }        

        public void Remove(string id)
        {
            _events.DeleteOne(myevent => myevent.Id == id);
        }
    }
}
