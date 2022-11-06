using Belle.Services.ProductServices.Models;
using Belle.Services.Response;

namespace Belle.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse> Create(CreateProductViewModel vm);
    }
}