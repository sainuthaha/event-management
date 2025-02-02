using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Api.Models;

namespace EventManagement.Api.Interfaces
{
    public interface IRegistrationService
    {
        Task RegisterEventAsync(Registration registration);
        Task<IEnumerable<Registration>> GetRegistrationsForEventAsync(Guid  eventId);
        Task<IEnumerable<Registration>> GetRegistrationsByEmailAsync(string email);
    }
}