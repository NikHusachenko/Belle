using Belle.Database.Entities;
using Belle.Services.Response;

namespace Belle.Services.Account
{
    public interface IAccountService
    {
        Task<List<ProductEntity>> GetUserCart();
        Task<ServiceResponse> CancelOrdering(long id);
    }
}