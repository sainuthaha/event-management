namespace EventManagement.Api.Models
{
    public class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required List<string> Roles { get; set; }
    }
}