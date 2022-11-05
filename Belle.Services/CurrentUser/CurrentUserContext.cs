using Belle.Common;
using Belle.Database.Enums;
using Belle.Services.UserService.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Belle.Services.CurrentUser
{
    public class CurrentUserContext : ICurrentUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _userPrincipal;

        public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public string? Login
        {
            get
            {
                string value = _userPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimType.LOGIN)?.Value;
                if(string.IsNullOrEmpty(value))
                {
                    return null;
                }
                return value;
            }
        }
        public UserType? Type 
        {
            get
            {
                string value = _userPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimType.USER_TYPE)?.Value;

                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }

                UserType? type = UserTypeHelper.GetTypeAsEnum(value);

                if (type == null)
                {
                    throw new Exception("Can't to get User Type claim");
                }

                return type.Value;
            }
        }
        public long? Id
        {
            get
            {
                string value = _userPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimType.ID)?.Value;
                if(!long.TryParse(value, out long id))
                {
                    return null;
                }
                return id;
            }
        }

        public string? Email
        {
            get
            {
                string value = _userPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimType.EMAIL)?.Value;
                if(!string.IsNullOrEmpty(value))
                {
                    return null;
                }
                return value;
            }
        }
    }
}