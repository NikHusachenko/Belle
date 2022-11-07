using Belle.Database.Entities;
using Belle.Database.Enums;
using Belle.Services.ProductServices;
using Belle.Services.ProductServices.Models;
using Belle.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Belle.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateAjax(CreateProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.Create(vm);
            if (response.IsError)
            {
                return BadRequest(new { responseMessage = response.ErrorMessage });
            }

            return Ok(new { redirectUrl = Url.Action("Details", "Product", new { @id = response.Value.Id }) });
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _productService.GetById(id);
            if (response.IsError)
            {
                return Redirect(Url.Action("UserFriendlyError", "Home", new { errorMessage = response.ErrorMessage }));
            }

            ProductDetailsHttpGetVm vm = new ProductDetailsHttpGetVm()
            {
                Category = response.Value.Category,
                Count = response.Value.Count,
                Description = response.Value.Description,
                Details = response.Value.Details,
                PathToPhoto = response.Value.PathToImage,
                Name = response.Value.Name,
                Price = response.Value.Price,
                Size = response.Value.Size,
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Listview(ProductCategory? category = null)
        {
            ListviewHttpGetVm vm = new ListviewHttpGetVm()
            {
                Products = await _productService.GetByCategory(category),
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Order(long id)
        {
            var response = await _productService.Order(id);
            if(response.IsError)
            {
                return Redirect(Url.Action("UserFriendlyError", "Home", new { errorMessage = response.ErrorMessage }));
            }

            return RedirectToAction("Cart", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Buyed()
        {
            BuyedHttpGetVm vm = new BuyedHttpGetVm()
            {
                Products = await _productService.GetBuyed(),
            };
            return View(vm);
        }
    }
}