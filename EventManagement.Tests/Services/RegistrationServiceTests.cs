using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using EventManagement.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Tests.Services
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private Mock<IRegistrationRepository> mockRegistrationRepository;
        private Mock<IEventRepository> mockEventRepository;
        private RegistrationService registrationService;

        [TestInitialize]
        public void Setup()
        {
            mockRegistrationRepository = new Mock<IRegistrationRepository>();
            mockEventRepository = new Mock<IEventRepository>();
            registrationService = new RegistrationService(mockRegistrationRepository.Object, mockEventRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task RegisterEventAsync_ShouldThrowArgumentNullException_WhenRegistrationIsNull()
        {
            // Act
            await registrationService.RegisterEventAsync(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task RegisterEventAsync_ShouldThrowInvalidOperationException_WhenEventNotFound()
        {
            // Arrange
            var registration = new Registration
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                EmailAddress = "john.doe@example.com",
                EventId = Guid.NewGuid()
            };
            mockEventRepository.Setup(repo => repo.GetEventByIdAsync(registration.EventId)).ReturnsAsync((Event)null);

            // Act
            await registrationService.RegisterEventAsync(registration);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task RegisterEventAsync_ShouldThrowInvalidOperationException_WhenNoAvailableTickets()
        {
            // Arrange
            var registration = new Registration
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                EmailAddress = "john.doe@example.com",
                EventId = Guid.NewGuid()
            };
            var eventEntity = new Event
            {
                Id = registration.EventId,
                Name = "Test Event",
                Description = "Test Description",
                Location = "Test Location",
                AvailableTickets = 0,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            mockEventRepository.Setup(repo => repo.GetEventByIdAsync(registration.EventId)).ReturnsAsync(eventEntity);

            // Act
            await registrationService.RegisterEventAsync(registration);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task RegisterEventAsync_ShouldDecrementAvailableTickets_AndSaveRegistration()
        {
            // Arrange
            var registration = new Registration
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                EmailAddress = "john.doe@example.com",
                EventId = Guid.NewGuid()
            };
            var eventEntity = new Event
            {
                Id = registration.EventId,
                Name = "Test Event",
                Description = "Test Description",
                Location = "Test Location",
                AvailableTickets = 10,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            mockEventRepository.Setup(repo => repo.GetEventByIdAsync(registration.EventId)).ReturnsAsync(eventEntity);
            mockEventRepository.Setup(repo => repo.UpdateEventAsync(eventEntity)).ReturnsAsync(eventEntity);
            mockRegistrationRepository.Setup(repo => repo.RegisterForEventAsync(registration)).ReturnsAsync(registration);

            // Act
            await registrationService.RegisterEventAsync(registration);

            // Assert
            mockEventRepository.Verify(repo => repo.UpdateEventAsync(It.Is<Event>(e => e.AvailableTickets == 9)), Times.Once);
            mockRegistrationRepository.Verify(repo => repo.RegisterForEventAsync(registration), Times.Once);
        }

        [TestMethod]
        public async Task GetRegistrationsForEventAsync_ShouldReturnListOfRegistrations()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var registrations = new List<Registration>
            {
                new Registration
                {
                    Name = "John Doe",
                    PhoneNumber = "1234567890",
                    EmailAddress = "john.doe@example.com",
                    EventId = eventId
                }
            };
            mockRegistrationRepository.Setup(repo => repo.GetRegistrationsByEventIdAsync(eventId)).ReturnsAsync(registrations);

            // Act
            var result = await registrationService.GetRegistrationsForEventAsync(eventId);

            // Assert
            Assert.AreEqual(registrations, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetRegistrationsForEventAsync_ShouldThrowArgumentException_WhenEventIdIsEmpty()
        {
            // Act
            await registrationService.GetRegistrationsForEventAsync(Guid.Empty);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task GetRegistrationsByEmailAsync_ShouldReturnListOfRegistrations()
        {
            // Arrange
            var email = "test@example.com";
            var registrations = new List<Registration>
            {
                new Registration
                {
                    Name = "John Doe",
                    PhoneNumber = "1234567890",
                    EmailAddress = email,
                    EventId = Guid.NewGuid()
                }
            };
            mockRegistrationRepository.Setup(repo => repo.GetRegistrationsByEmailIdAsync(email)).ReturnsAsync(registrations);

            // Act
            var result = await registrationService.GetRegistrationsByEmailAsync(email);

            // Assert
            Assert.AreEqual(registrations, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetRegistrationsByEmailAsync_ShouldThrowArgumentException_WhenEmailIsNullOrEmpty()
        {
            // Act
            await registrationService.GetRegistrationsByEmailAsync(null);

            // Assert is handled by ExpectedException
        }
    }
}