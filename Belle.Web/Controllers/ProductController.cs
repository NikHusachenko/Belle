using Belle.Services.ProductServices;
using Belle.Services.ProductServices.Models;
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

            // TODO
            // Change to Url.Action("Details", "Product", new { @id = response.Value.Id });
            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }
    }
}