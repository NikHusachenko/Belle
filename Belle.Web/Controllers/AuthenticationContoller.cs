using Belle.Services.Authentication;
using Belle.Services.Authentication.Models;
using Belle.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Belle.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LoginAjax([FromBody]LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.Login(vm.Login, vm.Password);
            if(response.IsError)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage });
            }

            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RegistrationAjax([FromBody]RegistrationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addResult = await _userService.Create(vm);
            if(addResult.IsError)
            {
                return BadRequest(new { responseMessage = addResult.ErrorMessage });
            }

            var loginResult = await _authenticationService.Login(vm.Login, vm.Password);
            if(loginResult.IsError)
            {
                return BadRequest(new { responseMessage = loginResult.ErrorMessage });
            }

            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }
    }
}