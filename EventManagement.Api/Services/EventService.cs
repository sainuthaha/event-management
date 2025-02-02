using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;

namespace EventManagement.Api.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await eventRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            var eventItem = await eventRepository.GetEventByIdAsync(eventId);
            if (eventItem == null)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} was not found.");
            }
            return eventItem;
        }

     public async Task<Event> CreateEventAsync(Event newEvent)
    {
        if (newEvent.Id == Guid.Empty)
        {
            newEvent.Id = Guid.NewGuid();
        }
        return await eventRepository.CreateEventAsync(newEvent);
    }

        public async Task<Event> UpdateEventAsync(Event updatedEvent)
        {
            var updatedEventItem = await eventRepository.UpdateEventAsync(updatedEvent);
            if (updatedEventItem == null)
            {
                throw new KeyNotFoundException($"Event with ID {updatedEvent.Id} was not found.");
            }
            return updatedEventItem;
        }

    }
}