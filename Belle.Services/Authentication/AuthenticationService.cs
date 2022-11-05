using Belle.Common;
using Belle.Database.Entities;
using Belle.Services.GenericRepository;
using Belle.Services.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Belle.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IRepository<UserEntity> _repository;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor,
            ILogger<AuthenticationService> logger,
            IRepository<UserEntity> repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _repository = repository;
        }

        public List<Claim> CreateClaim(UserEntity userEntity)
        {
            return new List<Claim>()
            {
                new Claim(AuthenticationClaimType.ID, userEntity.Id.ToString()),
                new Claim(AuthenticationClaimType.EMAIL, userEntity.Email),
                new Claim(AuthenticationClaimType.LOGIN, userEntity.Login),
                new Claim(AuthenticationClaimType.USER_TYPE, userEntity.Type.ToString()),
            };
        }

        public async Task<ServiceResponse> Login(string login, string password)
        {
            UserEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(user => !user.DeletedOn.HasValue &&
                    user.Login == login &&
                    user.Password == password);

            if (dbRecord == null)
            {
                return ServiceResponse.Error("User not found");
            }

            ClaimsIdentity identity = new ClaimsIdentity(CreateClaim(dbRecord));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            try
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());
                return ServiceResponse.Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"AuthenticationService -> Login exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return;
        }
    }
}