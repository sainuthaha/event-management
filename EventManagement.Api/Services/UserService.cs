using EventManagement.Api.Interfaces;
using EventManagement.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Api.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "password", Roles = new List<string> { "Admin" } }
            // Add more users as needed
        };

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return await Task.FromResult(user != null);
        }

        public async Task<bool> IsUserInRoleAsync(string username, string role)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return await Task.FromResult(user != null && user.Roles.Contains(role));
        }

   
    }
}