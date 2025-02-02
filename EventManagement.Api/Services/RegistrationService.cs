using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;

namespace EventManagement.Api.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IEventRepository eventRepository;

        public RegistrationService(IRegistrationRepository registrationRepository, IEventRepository eventRepository)
        {
            this.registrationRepository = registrationRepository;
            this.eventRepository = eventRepository;
        }
    
        
        public async Task RegisterEventAsync(Registration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            var eventEntity = await eventRepository.GetEventByIdAsync(registration.EventId);
            if (eventEntity == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            if (eventEntity.AvailableTickets <= 0)
            {
                throw new InvalidOperationException("No available tickets for the event.");
            }

            try
            {
                eventEntity.AvailableTickets -= 1; // Decrement available tickets
                await eventRepository.UpdateEventAsync(eventEntity); // Save changes to the event

                await registrationRepository.RegisterForEventAsync(registration); // Save the registration
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                throw new InvalidOperationException("An error occurred while registering for the event.", ex);
            }
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsForEventAsync(Guid eventId)
        {
            if (eventId == Guid.Empty)
            {
                throw new ArgumentException("Invalid event ID.", nameof(eventId));
            }

            return await registrationRepository.GetRegistrationsByEventIdAsync(eventId);
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await registrationRepository.GetRegistrationsByEmailIdAsync(email);
        }
    }
}