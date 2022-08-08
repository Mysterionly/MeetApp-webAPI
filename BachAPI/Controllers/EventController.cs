using BachAPI.Models;
using BachAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;

        public EventController(IEventService eventService, IEventTypeService eventTypeService)
        {
            _eventService = eventService;
            _eventTypeService = eventTypeService;
        }

        // GET: api/<MainController>
        [HttpGet("GetEvents", Name = "GetEvents")]
        public List<Event> Get()
        {
            //берем все события из бд
            List<Event> eventsDB = _eventService.Gets();

            List<Event> eventsFull = new List<Event>();

            foreach (Event item in eventsDB)
            {
                eventsFull.Add(ConvertEvent(item));
            }

            return eventsFull;
        }


        [HttpPost("GetEventGuests", Name = "GetEventGuests")]
        public List<String> GetEventGuests([FromBody] string eventId)
        {
            return _eventService.GetUsers(eventId);
        }


        [HttpPost("GuestList", Name = "GuestList")]
        public List<string> AddOrRemoveMeFromGuests(UserEvent ue)
        {
            return _eventService.AddOrRemoveMeToGuestList(ue);
        }

        [HttpPost("GetEventsImHost", Name = "GetEventsImHost")]
        public List<Event> GetEventsImHost(string uid)
        {
            return _eventService.GetEventsImHost(uid);
        }

        [HttpPost("GetEventsImGuest", Name = "GetEventsImGuest")]
        public List<Event> GetEventsImGuest(string uid)
        {
            return _eventService.GetEventsImGuest(uid);
        }

        //// GET: api/<MainController>
        //[HttpPost("GetEventGuests", Name = "GetEventGuests")]
        //public List<Event> Get(string eventId)
        //{
        //    //берем все события из бд
        //    List<Event> eventsDB = _eventService.Gets();

        //    List<Event> eventsFull = new List<Event>();

        //    foreach (Event item in eventsDB)
        //    {
        //        eventsFull.Add(ConvertEvent(item));
        //    }

        //    return eventsFull;
        //}

        //// GET api/<MainController>/5
        //[HttpGet("GetEvent", Name = "GetEvent")]
        //public ActionResult<Event> Get(string id)
        //{
        //    //берем событие из бд
        //    Event eventDB = _eventService.Get(id);

        //    if (eventDB == null)
        //    {
        //        return NotFound();
        //    }

        //    return ConvertEvent(eventDB);
        //}

        // POST api/<MainController>
        [HttpPost]
        public ActionResult<Event> Create(Event myevent)
        {
            

            //Переходим в Get с именем GetEvent
            return _eventService.Create(myevent); //CreatedAtRoute("GetEvent", new { id = myevent.Id.ToString() }, myevent);
        }

        //[HttpPut("UpdateEvent", Name = "UpdateEvent")]
        //public IActionResult Update(string id, Event myeventIn)
        //{
        //    var myevent = _eventService.Get(id);

        //    if (myevent == null)
        //    {
        //        return NotFound();
        //    }

        //    _eventService.Update(id, myeventIn);

        //    return NoContent();
        //}

        [HttpDelete("DeleteEvent", Name = "DeleteEvent")]
        public IActionResult Delete(string id)
        {
            var myevent = _eventService.Get(id);

            if (myevent == null)
            {
                return NotFound();
            }

            _eventService.Remove(myevent.Id);

            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Event ConvertEvent(Event eventDB)
        {
            //берем тип события по EventTypeId
            EventType eventType = _eventTypeService.Get(eventDB.EventType);

            //заполняем Событие + объект Тип события
            Event eventFull = new Event()
            {
                Id = eventDB.Id,
                Description = eventDB.Description,
                PlannedDate= eventDB.PlannedDate.ToUniversalTime(),
                Location = eventDB.Location,
                //MaxPublic = eventDB.MaxPublic,
                //Occured = eventDB.Occured,
                //Public = eventDB.Public,
                Title = eventDB.Title,
                //UnlimitedPublic = eventDB.UnlimitedPublic,
                EventType = eventType.Id,
                Host = eventDB.Host,
                Address = eventDB.Address,
                Guests = eventDB.Guests
            };

            return eventFull;
        }
    }
}
