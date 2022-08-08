using BachAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Services.Interfaces
{
    public interface IEventService
    {
        List<Event> Gets();
        Event Get(string Id);
        Event Create(Event myevent);
        void Update(string id, Event myevent);
        List<String> GetUsers(string eventId);
        void Remove(Event myevent);
        void Remove(string id);

        List<Event> GetEventsImHost(string uid);
        List<Event> GetEventsImGuest(string uid);
        List<string> AddOrRemoveMeToGuestList(UserEvent ue);
    }
}
