﻿using Belle.Database.Entities;
using Belle.Database.Enums;
using Belle.Services.ProductServices.Models;
using Belle.Services.Response;

namespace Belle.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductEntity>> Create(CreateProductViewModel vm);
        Task<ServiceResponse<ProductEntity>> GetById(long id);
        Task<List<ProductEntity>> GetByCategory(ProductCategory? category);
    }
}