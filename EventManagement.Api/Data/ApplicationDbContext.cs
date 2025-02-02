using Microsoft.EntityFrameworkCore;
using EventManagement.Api.Models;

namespace EventManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Ensure the database is created
            Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Auto-generate Id value
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => new { r.EmailAddress, r.EventId }); // Composite key
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(r => r.EmailAddress).IsRequired().HasMaxLength(100);
                entity.HasOne(r => r.Event)
                      .WithMany(e => e.Registrations)
                      .HasForeignKey(r => r.EventId);
            });
        }
    }
}