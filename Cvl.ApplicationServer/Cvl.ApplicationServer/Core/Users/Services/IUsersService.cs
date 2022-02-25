namespace Cvl.ApplicationServer.Core.Users.Services
{
    public interface IUsersService
    {
        Task RegisterNewUserAsync(string email, string password);
        Task AddRootUserAsync();
    }
}
