using AspNetIdentityOnly.Models;

namespace AspNetIdentityOnly.Services
{

    public interface IUserService
    {
        Task<IdentityResultDTO> RegisterAsync(string username, string password);
        Task<IdentityResultDTO> LoginAsync(string username, string password);
    }
}
