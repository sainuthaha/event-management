using EventManagement.Api.Data;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Api.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(Guid eventId)
        {
            return await _context.Events.FindAsync(eventId);
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event?> UpdateEventAsync(Event updatedEvent)
        {
            var eventItem = await _context.Events.FindAsync(updatedEvent.Id);
            if (eventItem != null)
            {
                eventItem.Name = updatedEvent.Name;
                eventItem.Description = updatedEvent.Description;
                eventItem.Location = updatedEvent.Location;
                eventItem.StartTime = updatedEvent.StartTime;
                // Update other properties as needed

                await _context.SaveChangesAsync();
                return eventItem;
            }
            return null;
        }
    }
}