using System.ComponentModel.DataAnnotations;

namespace EventManagement.Api.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Location { get; set; }
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

        [Required]
        public required int AvailableTickets { get; set; }
        public required DateTime StartTime { get; set; }
    }
}