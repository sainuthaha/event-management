namespace EventManagement.Api.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user with the specified username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A token or identifier for the logged-in session.</returns>
        Task<string> Login(string username, string password);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <summary>
        /// Logs out the current user.
        /// </summary>
        void Logout();
    }
}   