using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace EventManagement.Api.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/v1/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {
            try
            {
                var createdEvent = await eventService.CreateEventAsync(newEvent);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event updatedEvent)
        {
            try
            {
                var existingEvent = await eventService.GetEventByIdAsync(id);
                if (existingEvent == null)
                {
                    return NotFound();
                }

                updatedEvent.Id = id;
                await eventService.UpdateEventAsync(updatedEvent);
                return Ok(updatedEvent);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var events = await eventService.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            try
            {
                var eventItem = await eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    return NotFound();
                }
                return Ok(eventItem);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("created/{email}")]
        public async Task<IActionResult> GetEventsByCreator(string email)
        {
            try
            {
                var events = await eventService.GetEventsByCreator(email);
                if (events == null || !events.Any())
                {
                    return NotFound();
                }
                return Ok(events);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
