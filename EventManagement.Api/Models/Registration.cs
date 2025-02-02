using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagement.Api.Models
{
    public class Registration
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Key, Column(Order = 0)]
        [Required]
        public required string EmailAddress { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public Guid EventId { get; set; }

        // Navigation property
        public Event? Event { get; set; }
    }
}