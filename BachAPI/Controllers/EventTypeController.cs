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
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        [HttpGet("GetEventTypes", Name = "GetEventTypes")]
        public List<EventType> Get()
        {
            return _eventTypeService.Gets();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[HttpGet("GetEventType", Name = "GetEventType")]
        public ActionResult<EventType> Get(string id)
        {            
            var eventType = _eventTypeService.Get(id);

            if (eventType == null)
            {
                return NotFound();
            }

            return eventType;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[HttpPost]
        public ActionResult<EventType> Create(EventType eventType)
        {
            _eventTypeService.Create(eventType);

            return CreatedAtRoute("GetEventType", new { id = eventType.Id.ToString() }, eventType);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[HttpPut("UpdateEventType", Name = "UpdateEventType")]
        public IActionResult Update(string id, EventType eventTypeIn)
        {
            var eventType = _eventTypeService.Get(id);

            if (eventType == null)
            {
                return NotFound();
            }

            _eventTypeService.Update(id, eventTypeIn);

            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[HttpDelete("DeleteEventType", Name = "DeleteEventType")]
        public IActionResult Delete(string id)
        {
            var eventType = _eventTypeService.Get(id);

            if (eventType == null)
            {
                return NotFound();
            }

            _eventTypeService.Remove(eventType.Id);

            return NoContent();
        }
    }
}
