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
    public class EventControllerTests
    {
        private Mock<IEventService> mockEventService;
        private EventController eventController;

        [TestInitialize]
        public void Setup()
        {
            mockEventService = new Mock<IEventService>();
            eventController = new EventController(mockEventService.Object);
        }

        [TestMethod]
        public async Task CreateEvent_ShouldReturnCreatedAtActionResult_WithEvent()
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
            mockEventService.Setup(service => service.CreateEventAsync(newEvent)).ReturnsAsync(createdEvent);

            // Act
            var result = await eventController.CreateEvent(newEvent);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(201, createdAtActionResult.StatusCode);
            Assert.AreEqual(createdEvent, createdAtActionResult.Value);
        }

        [TestMethod]
        public async Task UpdateEvent_ShouldReturnOkResult_WithUpdatedEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var existingEvent = new Event
            {
                Id = eventId,
                Name = "Existing Event",
                Description = "Existing Description",
                Location = "Existing Location",
                AvailableTickets = 50,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "existing@example.com"
            };
            var updatedEvent = new Event
            {
                Id = eventId,
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(2),
                CreatedBy = "updated@example.com"
            };

            mockEventService.Setup(service => service.GetEventByIdAsync(eventId)).ReturnsAsync(existingEvent);
            mockEventService.Setup(service => service.UpdateEventAsync(updatedEvent)).ReturnsAsync(updatedEvent);

            // Act
            var result = await eventController.UpdateEvent(eventId, updatedEvent);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(updatedEvent, okResult.Value);
        }

        [TestMethod]
        public async Task GetAllEvents_ShouldReturnOkResult_WithListOfEvents()
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
            mockEventService.Setup(service => service.GetAllEventsAsync()).ReturnsAsync(events);

            // Act
            var result = await eventController.GetAllEvents();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(events, okResult.Value);
        }

        [TestMethod]
        public async Task GetEventById_ShouldReturnOkResult_WithEvent()
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
            mockEventService.Setup(service => service.GetEventByIdAsync(eventId)).ReturnsAsync(eventItem);

            // Act
            var result = await eventController.GetEventById(eventId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(eventItem, okResult.Value);
        }

        [TestMethod]
        public async Task GetEventsByCreator_ShouldReturnOkResult_WithListOfEvents()
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
            mockEventService.Setup(service => service.GetEventsByCreator(email)).ReturnsAsync(events);

            // Act
            var result = await eventController.GetEventsByCreator(email);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(events, okResult.Value);
        }
    }
}