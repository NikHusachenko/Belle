using Belle.Database.Entities;
using Belle.Database.Enums;
using Belle.Services.Authentication.Models;
using Belle.Services.GenericRepository;
using Belle.Services.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Belle.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<UserEntity> repository,
            ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ServiceResponse> Create(RegistrationViewModel vm)
        {
            UserEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(user => !user.DeletedOn.HasValue &&
                    (user.Email == vm.Email ||
                    user.Login == vm.Login));

            if (dbRecord != null)
            {
                return ServiceResponse.Error("User with such email or login exists");
            }

            UserEntity newRecord = new UserEntity()
            {
                CreatedOn = DateTime.Now,
                Email = vm.Email,
                Login = vm.Login,
                Password = vm.Password,
                Type = UserType.Client,
                WalletBalance = 0,
            };

            try
            {
                await _repository.Add(newRecord);
                await _repository.SaveChanges();
                return ServiceResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserService -> public Create exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<UserEntity>> GetById(long? id)
        {
            if (id == null)
            {
                return ServiceResponse<UserEntity>.Error("User with such id not found");
            }

            UserEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(user => !user.DeletedOn.HasValue
                    && user.Id == id);

            if (dbRecord == null)
            {
                return ServiceResponse<UserEntity>.Error("User with such id not found");
            }

            return ServiceResponse<UserEntity>.Ok(dbRecord);
        }
    }
}