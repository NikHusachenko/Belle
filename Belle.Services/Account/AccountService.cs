using Belle.Database.Entities;
using Belle.Services.CurrentUser;
using Belle.Services.ProductServices;
using Belle.Services.Response;
using Microsoft.Extensions.Logging;

namespace Belle.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IProductService _productService;
        private readonly ILogger<AccountService> _logger;
        private readonly ICurrentUserContext _currentUserContext;

        public AccountService(IProductService productService, 
            ILogger<AccountService> logger,
            ICurrentUserContext currentUserContext)
        {
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