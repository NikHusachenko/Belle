using Belle.Database.Entities;
using Belle.Database.Enums;
using Belle.Services.CurrentUser;
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
        private readonly IRepository<UserEntity> _userRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserContext _currentUserContext;

        public ProductService(IRepository<ProductEntity> repository, 
            IRepository<UserEntity> userRepository,
            ILogger<ProductService> logger,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserContext currentUserContext)
        {
            _repository = repository;
            _userRepository = userRepository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _currentUserContext = currentUserContext;
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

        public async Task<ServiceResponse> Delete(long id)
        {
            ProductEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(prod => prod.Id == id);

            if (dbRecord == null)
            {
                return ServiceResponse.Error("Product with such id not found");
            }

            try
            {
                _repository.Remove(dbRecord);
                await _repository.SaveChanges();
                return ServiceResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Delete exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }
        }

        public async Task<List<ProductEntity>> GetBuyed()
        {
            List<ProductEntity> products = await _repository.Entities
                .Include(prod => prod.UserEntity)
                .Where(prod => !prod.DeletedOn.HasValue &&
                    prod.UserFK != null)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductEntity>> GetByCategory(ProductCategory? category)
        {
            IQueryable<ProductEntity> query = _repository.Entities
                .Where(prod => !prod.DeletedOn.HasValue &&
                    prod.UserEntity == null);

            if (!category.HasValue)
            {
                return await query.ToListAsync();
            }

            return await query.Where(prod => prod.Category == category).ToListAsync();
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

        public async Task<List<ProductEntity>> GetByUserId(long id)
        {
            List<ProductEntity> products = await _repository.Entities
                .Where(prod => !prod.DeletedOn.HasValue &&
                    prod.UserFK == id)
                .ToListAsync();

            return products;
        }

        public async Task<ServiceResponse> Order(long id)
        {
            ProductEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(prod => prod.Id == id);

            if (dbRecord == null)
            {
                return ServiceResponse.Error("Product with such id not found");
            }

            UserEntity userRecord = await _userRepository.Entities
                .FirstOrDefaultAsync(user => user.Id == _currentUserContext.Id);
            if (userRecord == null)
            {
                return ServiceResponse.Error("User with such id not found");
            }

            if (dbRecord.Price > userRecord.WalletBalance)
            {
                return ServiceResponse.Error("Insufficient funds");
            }

            userRecord.WalletBalance -= dbRecord.Price;
            dbRecord.UserFK = _currentUserContext.Id;

            try
            {
                _repository.Update(dbRecord);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> public Order exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }

            try
            {
                _userRepository.Update(userRecord);
                await _userRepository.SaveChanges();
                return ServiceResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Order exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse> RemoveFromCard(long id)
        {
            ProductEntity dbRecord = await _repository.Entities
                .FirstOrDefaultAsync(prod => prod.Id == id);

            if (dbRecord == null)
            {
                return ServiceResponse.Error("Product with such id not found");
            }

            dbRecord.UserFK = null;
            return await Update(dbRecord);
        }

        public async Task<ServiceResponse> Update(ProductEntity productEntity)
        {
            try
            {
                _repository.Update(productEntity);
                await _repository.SaveChanges();
                return ServiceResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductService -> Update exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }
        }
    }
}