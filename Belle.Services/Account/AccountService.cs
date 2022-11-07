using Belle.Database.Entities;
using Belle.Services.CurrentUser;
using Belle.Services.GenericRepository;
using Belle.Services.ProductServices;
using Belle.Services.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Belle.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IProductService _productService;
        private readonly ILogger<AccountService> _logger;
        private readonly ICurrentUserContext _currentUserContext;

        public AccountService(IRepository<UserEntity> repository,
            IProductService productService, 
            ILogger<AccountService> logger,
            ICurrentUserContext currentUserContext)
        {
            _repository = repository;
            _productService = productService;
            _logger = logger;
            _currentUserContext = currentUserContext;
        }

        public async Task<ServiceResponse> CancelOrdering(long id)
        {
            var response = await _productService.GetById(id);
            if (response.IsError)
            {
                return ServiceResponse.Error(response.ErrorMessage);
            }

            ProductEntity dbRecord = response.Value;

            UserEntity userRecord = await _repository.Entities
                .FirstOrDefaultAsync(user => user.Id == dbRecord.UserFK);

            if (userRecord == null)
            {
                return ServiceResponse.Error("User with such id not found");
            }

            userRecord.WalletBalance += dbRecord.Price;
            try
            {
                _repository.Update(userRecord);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountService -> CancelOrdering exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }

            dbRecord.UserFK = null;
            var updateResult = await _productService.Update(dbRecord);
            return updateResult;
        }

        public async Task<List<ProductEntity>> GetUserCart()
        {
            List<ProductEntity> products = await _productService.GetByUserId(_currentUserContext.Id.Value);
            return products;
        }
    }
}