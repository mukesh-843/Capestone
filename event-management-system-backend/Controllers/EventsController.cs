using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using event_management_system_backend.DTOs;
using event_management_system_backend.Models;
using event_management_system_backend.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace event_management_system_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            //return Ok(await _eventService.GetEventsAsync());
            try
            {
                var events = await _eventService.GetEventsAsync();
                if (events == null || !events.Any())
                {
                    return NotFound("No events found.");
                }
                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _eventService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return Ok(@event);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDTO eventDTO)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return BadRequest("Invalid user ID");
            }

            var newEvent = await _eventService.CreateEventAsync(eventDTO, userId);
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
        }

        [HttpPost("{id}/rsvp")]
        [Authorize]
        public async Task<IActionResult> RSVP(Guid id)
        {
            if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                await _eventService.RSVPToEventAsync(id, userId);
                return Ok();
            }
            return BadRequest("Invalid user ID");
        }

        [HttpPost("{id}/remind")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendReminder(Guid id)
        {
            await _eventService.SendReminderAsync(id); // Removed assignment to fix error
            return Ok("Reminders sent");
        }

        [HttpGet("{id}/attendees")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAttendees(Guid id)
        {
            var attendees = await _eventService.GetAttendeesAsync(id);
            if (attendees == null) return NotFound();
            return Ok(attendees);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}


