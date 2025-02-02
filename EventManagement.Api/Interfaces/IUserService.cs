namespace EventManagement.Api.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<bool> IsUserInRoleAsync(string username, string role);
    }
}