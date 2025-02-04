using Microsoft.AspNetCore.Mvc;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

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
            try
            {
                await registrationService.RegisterEventAsync(reg);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("events/{eventId}/registrations")]
        public async Task<IActionResult> GetRegistrationsForEvent(Guid eventId)
        {
            try
            {
                var registrations = await registrationService.GetRegistrationsForEventAsync(eventId);
                return Ok(registrations);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("registrations/{email}")]
        public async Task<IActionResult> GetRegistrationsByEmail(string email)
        {
            try
            {
                var registrations = await registrationService.GetRegistrationsByEmailAsync(email);
                return Ok(registrations);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}