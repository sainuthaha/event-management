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
    public class EventServiceTests
    {
        private Mock<IEventRepository> mockEventRepository;
        private EventService eventService;

        [TestInitialize]
        public void Setup()
        {
            mockEventRepository = new Mock<IEventRepository>();
            eventService = new EventService(mockEventRepository.Object);
        }

        [TestMethod]
        public async Task GetAllEventsAsync_ShouldReturnListOfEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Event",
                    Description = "Test Description",
                    Location = "Test Location",
                    AvailableTickets = 100,
                    StartTime = DateTime.Now.AddDays(1),
                    CreatedBy = "creator@example.com"
                }
            };
            mockEventRepository.Setup(repo => repo.GetAllEventsAsync()).ReturnsAsync(events);

            // Act
            var result = await eventService.GetAllEventsAsync();

            // Assert
            Assert.AreEqual(events, result);
        }

        [TestMethod]
        public async Task GetEventsByCreator_ShouldReturnListOfEvents()
        {
            // Arrange
            var email = "creator@example.com";
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Event",
                    Description = "Test Description",
                    Location = "Test Location",
                    AvailableTickets = 100,
                    StartTime = DateTime.Now.AddDays(1),
                    CreatedBy = email
                }
            };
            mockEventRepository.Setup(repo => repo.GetEventsByCreator(email)).ReturnsAsync(events);

            // Act
            var result = await eventService.GetEventsByCreator(email);

            // Assert
            Assert.AreEqual(events, result);
        }

        [TestMethod]
        public async Task GetEventByIdAsync_ShouldReturnEvent_WhenEventExists()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventItem = new Event
            {
                Id = eventId,
                Name = "Test Event",
                Description = "Test Description",
                Location = "Test Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            mockEventRepository.Setup(repo => repo.GetEventByIdAsync(eventId)).ReturnsAsync(eventItem);

            // Act
            var result = await eventService.GetEventByIdAsync(eventId);

            // Assert
            Assert.AreEqual(eventItem, result);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetEventByIdAsync_ShouldThrowKeyNotFoundException_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            mockEventRepository.Setup(repo => repo.GetEventByIdAsync(eventId)).ReturnsAsync((Event)null);

            // Act
            await eventService.GetEventByIdAsync(eventId);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task CreateEventAsync_ShouldReturnCreatedEvent()
        {
            // Arrange
            var newEvent = new Event
            {
                Name = "New Event",
                Description = "New Description",
                Location = "New Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            var createdEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "New Event",
                Description = "New Description",
                Location = "New Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            mockEventRepository.Setup(repo => repo.CreateEventAsync(newEvent)).ReturnsAsync(createdEvent);

            // Act
            var result = await eventService.CreateEventAsync(newEvent);

            // Assert
            Assert.AreEqual(createdEvent, result);
        }

        [TestMethod]
        public async Task UpdateEventAsync_ShouldReturnUpdatedEvent()
        {
            // Arrange
            var updatedEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(2),
                CreatedBy = "updated@example.com"
            };
            mockEventRepository.Setup(repo => repo.UpdateEventAsync(updatedEvent)).ReturnsAsync(updatedEvent);

            // Act
            var result = await eventService.UpdateEventAsync(updatedEvent);

            // Assert
            Assert.AreEqual(updatedEvent, result);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateEventAsync_ShouldThrowKeyNotFoundException_WhenEventDoesNotExist()
        {
            // Arrange
            var updatedEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(2),
                CreatedBy = "updated@example.com"
            };
            mockEventRepository.Setup(repo => repo.UpdateEventAsync(updatedEvent)).ReturnsAsync((Event)null);

            // Act
            await eventService.UpdateEventAsync(updatedEvent);
        }
    }
}