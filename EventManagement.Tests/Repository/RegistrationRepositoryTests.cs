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
    public class RegistrationRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> dbContextOptions;
        private ApplicationDbContext dbContext;
        private RegistrationRepository registrationRepository;

        [TestInitialize]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EventManagementTestDb")
                .Options;
            dbContext = new ApplicationDbContext(dbContextOptions);
            registrationRepository = new RegistrationRepository(dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task GetRegistrationsByEmailIdAsync_ShouldReturnRegistrations_WhenRegistrationsExist()
        {
            // Arrange
            var email = "john.doe@example.com";
            var registrations = new List<Registration>
            {
                new Registration
                {
                    Name = "John Doe",
                    PhoneNumber = "1234567890",
                    EmailAddress = email,
                    EventId = Guid.NewGuid()
                },
                new Registration
                {
                    Name = "Jane Doe",
                    PhoneNumber = "0987654321",
                    EmailAddress = email,
                    EventId = Guid.NewGuid()
                }
            };
            dbContext.Registrations.AddRange(registrations);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await registrationRepository.GetRegistrationsByEmailIdAsync(email);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetRegistrationsByEmailIdAsync_ShouldReturnEmptyList_WhenNoRegistrationsExist()
        {
            // Act
            var result = await registrationRepository.GetRegistrationsByEmailIdAsync("nonexistent@example.com");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetRegistrationsByEventIdAsync_ShouldReturnRegistrations_WhenRegistrationsExist()
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
                },
                new Registration
                {
                    Name = "Jane Doe",
                    PhoneNumber = "0987654321",
                    EmailAddress = "jane.doe@example.com",
                    EventId = eventId
                }
            };
            dbContext.Registrations.AddRange(registrations);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await registrationRepository.GetRegistrationsByEventIdAsync(eventId);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetRegistrationsByEventIdAsync_ShouldReturnEmptyList_WhenNoRegistrationsExist()
        {
            // Act
            var result = await registrationRepository.GetRegistrationsByEventIdAsync(Guid.NewGuid());

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task RegisterForEventAsync_ShouldAddRegistration()
        {
            // Arrange
            var registration = new Registration
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                EmailAddress = "john.doe@example.com",
                EventId = Guid.NewGuid()
            };

            // Act
            var result = await registrationRepository.RegisterForEventAsync(registration);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, dbContext.Registrations.Count());
        }
    }
}