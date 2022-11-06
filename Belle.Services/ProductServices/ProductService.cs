using Belle.Database.Entities;
using Belle.Services.GenericRepository;
using Belle.Services.ProductServices.Models;
using Belle.Services.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Belle.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly ILogger<ProductService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IRepository<ProductEntity> repository, 
            ILogger<ProductService> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<ProductEntity>> Create(CreateProductViewModel vm)
        {
            ProductEntity dbRecord = new ProductEntity()
            {
                Category = vm.Category,
                Count = vm.Count,
                CreatedOn = DateTime.Now,
                Description = vm.Description,
                Details = vm.Details,
                Name = vm.Name,
                Price = vm.Price,
                Size = vm.Size,
            };

            try
            {
                await _repository.Add(dbRecord);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Create exception: {ex.Message}");
                return ServiceResponse<ProductEntity>.Error(ex.Message);
            }

            string fullFileName = vm.Photo.FileName;
            string fileExtension = fullFileName.Split('.').Last();
            string newFileName = $"{dbRecord.Id}.{fileExtension}";
            
            if (!Directory.Exists($"{_webHostEnvironment.WebRootPath}\\ProductImages"))
            {
                Directory.CreateDirectory($"{_webHostEnvironment.WebRootPath}\\ProductImages");
            }

            try
            {
                using (FileStream fileStream = new FileStream($"{_webHostEnvironment.WebRootPath}\\ProductImages\\{newFileName}", FileMode.CreateNew))
                {
                    vm.Photo.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Create exception: {ex.Message}");
                return ServiceResponse<ProductEntity>.Error(ex.Message);
            }

            dbRecord.PathToImage = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/ProductImages/{newFileName}";

            try
            {
                _repository.Update(dbRecord);
                await _repository.SaveChanges();
                return ServiceResponse<ProductEntity>.Ok(dbRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Create exception: {ex.Message}");
                return ServiceResponse<ProductEntity>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ProductEntity>> GetById(long id)
        {
            ProductEntity dbRecored = await _repository.Entities
                .FirstOrDefaultAsync(prod => !prod.DeletedOn.HasValue &&
                    prod.Id == id);

            if (dbRecored == null)
            {
                return ServiceResponse<ProductEntity>.Error("Product with such id not found");
            }

            return ServiceResponse<ProductEntity>.Ok(dbRecored);
        }
    }
}