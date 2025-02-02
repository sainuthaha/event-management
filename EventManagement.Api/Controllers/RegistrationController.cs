using Microsoft.AspNetCore.Mvc;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpPost]
        [Route("registrations")]
        public async Task<IActionResult> RegisterForEvent([FromBody] Registration reg)
        {
            await registrationService.RegisterEventAsync(reg);
            return Ok();
        }


        [HttpGet("events/{eventId}/registrations")]
        public async Task<IActionResult> GetRegistrationsForEvent(Guid eventId)
        {
            var registrations = await registrationService.GetRegistrationsForEventAsync(eventId);
            return Ok(registrations);
        }

        [HttpGet("registrations/{email}")]
        public async Task<IActionResult> GetRegistrationsByEmail(string email)
        {
            var registrations = await registrationService.GetRegistrationsByEmailAsync(email);
            return Ok(registrations);
        }
    }
}