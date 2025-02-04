using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventManagement.Api.Data;
using EventManagement.Api.Models;
using EventManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Tests.Repository
{
    [TestClass]
    public class EventRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> dbContextOptions;
        private ApplicationDbContext dbContext;
        private EventRepository eventRepository;

        [TestInitialize]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EventManagementTestDb")
                .Options;
            dbContext = new ApplicationDbContext(dbContextOptions);
            eventRepository = new EventRepository(dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task GetAllEventsAsync_ShouldReturnAllEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 1",
                    Description = "Description 1",
                    Location = "Location 1",
                    AvailableTickets = 100,
                    StartTime = DateTime.Now.AddDays(1),
                    CreatedBy = "creator1@example.com"
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 2",
                    Description = "Description 2",
                    Location = "Location 2",
                    AvailableTickets = 200,
                    StartTime = DateTime.Now.AddDays(2),
                    CreatedBy = "creator2@example.com"
                }
            };
            dbContext.Events.AddRange(events);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await eventRepository.GetAllEventsAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
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
            dbContext.Events.Add(eventItem);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await eventRepository.GetEventByIdAsync(eventId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(eventId, result.Id);
        }

        [TestMethod]
        public async Task GetEventByIdAsync_ShouldReturnNull_WhenEventDoesNotExist()
        {
            // Act
            var result = await eventRepository.GetEventByIdAsync(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CreateEventAsync_ShouldAddEvent()
        {
            // Arrange
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "New Event",
                Description = "New Description",
                Location = "New Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };

            // Act
            var result = await eventRepository.CreateEventAsync(newEvent);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newEvent.Id, result.Id);
            Assert.AreEqual(1, dbContext.Events.Count());
        }

        [TestMethod]
        public async Task UpdateEventAsync_ShouldUpdateEvent_WhenEventExists()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var existingEvent = new Event
            {
                Id = eventId,
                Name = "Existing Event",
                Description = "Existing Description",
                Location = "Existing Location",
                AvailableTickets = 100,
                StartTime = DateTime.Now.AddDays(1),
                CreatedBy = "creator@example.com"
            };
            dbContext.Events.Add(existingEvent);
            await dbContext.SaveChangesAsync();

            var updatedEvent = new Event
            {
                Id = eventId,
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                AvailableTickets = 200,
                StartTime = DateTime.Now.AddDays(2),
                CreatedBy = "updated@example.com"
            };

            // Act
            var result = await eventRepository.UpdateEventAsync(updatedEvent);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedEvent.Name, result.Name);
            Assert.AreEqual(updatedEvent.Description, result.Description);
            Assert.AreEqual(updatedEvent.Location, result.Location);
            Assert.AreEqual(updatedEvent.AvailableTickets, result.AvailableTickets);
            Assert.AreEqual(updatedEvent.StartTime, result.StartTime);
        }

        [TestMethod]
        public async Task UpdateEventAsync_ShouldReturnNull_WhenEventDoesNotExist()
        {
            // Arrange
            var updatedEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                AvailableTickets = 200,
                StartTime = DateTime.Now.AddDays(2),
                CreatedBy = "updated@example.com"
            };

            // Act
            var result = await eventRepository.UpdateEventAsync(updatedEvent);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetEventsByCreator_ShouldReturnEvents_WhenEventsExist()
        {
            // Arrange
            var email = "creator@example.com";
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 1",
                    Description = "Description 1",
                    Location = "Location 1",
                    AvailableTickets = 100,
                    StartTime = DateTime.Now.AddDays(1),
                    CreatedBy = email
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 2",
                    Description = "Description 2",
                    Location = "Location 2",
                    AvailableTickets = 200,
                    StartTime = DateTime.Now.AddDays(2),
                    CreatedBy = email
                }
            };
            dbContext.Events.AddRange(events);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await eventRepository.GetEventsByCreator(email);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetEventsByCreator_ShouldReturnEmptyList_WhenNoEventsExist()
        {
            // Act
            var result = await eventRepository.GetEventsByCreator("nonexistent@example.com");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}