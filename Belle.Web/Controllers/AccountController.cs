using Belle.Services.Account;
using Belle.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Belle.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            CartHttpGetVm vm = new CartHttpGetVm()
            {
                Products = await _accountService.GetUserCart(),
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrdering(long id)
        {
            var response = await _accountService.CancelOrdering(id);
            if (response.IsError)
            {
                return Redirect(Url.Action("UserFriendlyError", "Home", new { errorMessage = response.ErrorMessage }));
            }

            return RedirectToAction("Card", "Account");
        }
    }
}
