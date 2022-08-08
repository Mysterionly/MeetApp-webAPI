using BachAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Services.Interfaces
{
    public interface IEventTypeService
    {
        List<EventType> Gets();
        EventType Get(string Id);
        EventType Create(EventType eventType);
        void Update(string id, EventType eventType);
        void Remove(EventType eventType);
        void Remove(string id);
    }
}
