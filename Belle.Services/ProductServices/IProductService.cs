using Belle.Database.Entities;
using Belle.Database.Enums;
using Belle.Services.ProductServices.Models;
using Belle.Services.Response;

namespace Belle.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductEntity>> Create(CreateProductViewModel vm);
        Task<ServiceResponse> Order(long id);
        Task<ServiceResponse> Update(ProductEntity productEntity);
        Task<ServiceResponse> Delete(long id);
        Task<ServiceResponse> RemoveFromCard(long id);

        Task<ServiceResponse<ProductEntity>> GetById(long id);
        Task<List<ProductEntity>> GetByCategory(ProductCategory? category);
        Task<List<ProductEntity>> GetByUserId(long id);
        Task<List<ProductEntity>> GetBuyed();
    }
}