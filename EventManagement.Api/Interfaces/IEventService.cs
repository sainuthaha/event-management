using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Api.Models;

namespace EventManagement.Api.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event> UpdateEventAsync(Event updatedEvent);
        Task<IEnumerable<Event>> GetEventsByCreator(string email);
    }
}