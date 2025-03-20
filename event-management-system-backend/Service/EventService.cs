using event_management_system_backend.Data;
using event_management_system_backend.DTOs;
using event_management_system_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace event_management_system_backend.Service
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(Guid id);
        Task<Event> CreateEventAsync(EventDTO eventDTO, Guid userId);
        Task RSVPToEventAsync(Guid eventId, Guid userId);
        Task SendReminderAsync(Guid eventId);
        Task<List<User>> GetAttendeesAsync(Guid eventId);
        Task DeleteEventAsync(Guid eventId);
    }

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Attendees)
                .Include(e => e.CreatedBy)
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            return await _context.Events
                .Include(e => e.Attendees)
                .Include(e => e.CreatedBy)
                .FirstOrDefaultAsync(e => e.Id == id) ?? new Event();
        }

        public async Task<Event> CreateEventAsync(EventDTO eventDTO, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new KeyNotFoundException("User not found");

            var newEvent = new Event
            {
                Title = eventDTO.Title,
                Description = eventDTO.Description,
                Date = eventDTO.Date,
                Time = eventDTO.Time,
                Location = eventDTO.Location,
                CreatedBy = user
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task RSVPToEventAsync(Guid eventId, Guid userId)
        {
            var eventToAttend = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            var user = await _context.Users.FindAsync(userId);

            if (eventToAttend == null || user == null)
                throw new KeyNotFoundException("Event or User not found");

            if (eventToAttend.Attendees.Any(a => a.Id == userId))
                throw new InvalidOperationException("User already RSVP'd");

            eventToAttend.Attendees.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task SendReminderAsync(Guid eventId)
        {
            var eventToRemind = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventToRemind == null)
                throw new KeyNotFoundException("Event not found");

            foreach (var attendee in eventToRemind.Attendees)
            {
                attendee.Reminder = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAttendeesAsync(Guid eventId)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            return eventEntity?.Attendees ?? new List<User>();
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await _context.Events.FindAsync(eventId);
            if (eventToDelete == null)
                throw new KeyNotFoundException("Event not found");

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
    }
}