using EventManagement.Api.Models;

namespace EventManagement.Api.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<Registration>> GetRegistrationsByEmailIdAsync(string emailId);
        Task<Registration?> RegisterForEventAsync(Registration registration);
        Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(Guid eventId);
    }
}