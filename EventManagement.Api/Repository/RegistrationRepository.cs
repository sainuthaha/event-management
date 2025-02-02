using EventManagement.Api.Data;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Api.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public RegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsByEmailIdAsync(string emailId)
        {
            return await _context.Registrations
                .Where(r => r.EmailAddress == emailId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(Guid eventId)
        {
            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Registration?> RegisterForEventAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            return registration;
        }
    }
}