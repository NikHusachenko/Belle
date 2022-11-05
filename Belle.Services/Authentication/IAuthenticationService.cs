using Belle.Database.Entities;
using Belle.Services.Response;
using System.Security.Claims;

namespace Belle.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> Login(string login, string password);
        Task Logout();
        List<Claim> CreateClaim(UserEntity userEntity);
    }
}