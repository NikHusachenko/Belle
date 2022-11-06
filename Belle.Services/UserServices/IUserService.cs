using Belle.Database.Entities;
using Belle.Services.Authentication.Models;
using Belle.Services.Response;

namespace Belle.Services.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse> Create(RegistrationViewModel vm);
        Task<ServiceResponse<UserEntity>> GetById(long? id);
    }
}