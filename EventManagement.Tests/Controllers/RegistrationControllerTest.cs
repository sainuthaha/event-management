using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EventManagement.Api.Controllers;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTests
    {
        private Mock<IRegistrationService> mockRegistrationService;
        private RegistrationController registrationController;

        [TestInitialize]
        public void Setup()
        {
            mockRegistrationService = new Mock<IRegistrationService>();
            registrationController = new RegistrationController(mockRegistrationService.Object);
        }

        [TestMethod]
        public async Task RegisterForEvent_ShouldReturnOkResult()
        {
            // Arrange
            var registration = new Registration
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                EmailAddress = "john.doe@example.com",
                EventId = Guid.NewGuid()
            };

            mockRegistrationService.Setup(service => service.RegisterEventAsync(registration)).Returns(Task.CompletedTask);

            // Act
            var result = await registrationController.RegisterForEvent(registration);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetRegistrationsForEvent_ShouldReturnOkResult_WithListOfRegistrations()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var registrations = new List<Registration>
            {
                new Registration { Name = "John Doe", PhoneNumber = "1234567890", EmailAddress = "john.doe@example.com", EventId = eventId }
            };

            mockRegistrationService.Setup(service => service.GetRegistrationsForEventAsync(eventId)).ReturnsAsync(registrations);

            // Act
            var result = await registrationController.GetRegistrationsForEvent(eventId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(registrations, okResult.Value);
        }

        [TestMethod]
        public async Task GetRegistrationsByEmail_ShouldReturnOkResult_WithListOfRegistrations()
        {
            // Arrange
            var email = "john.doe@example.com";
            var registrations = new List<Registration>
            {
                new Registration { Name = "John Doe", PhoneNumber = "1234567890", EmailAddress = email, EventId = Guid.NewGuid() }
            };

            mockRegistrationService.Setup(service => service.GetRegistrationsByEmailAsync(email)).ReturnsAsync(registrations);

            // Act
            var result = await registrationController.GetRegistrationsByEmail(email);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(registrations, okResult.Value);
        }
    }
}