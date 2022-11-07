using Belle.Services.CurrentUser;
using Belle.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Belle.Web.Views.Shared.Components.HeaderComponent
{
    public class HeaderComponent : ViewComponent
    {
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IUserService _userService;

        public HeaderComponent(ICurrentUserContext currentUserContext,
            IUserService userService)
        {
            _currentUserContext = currentUserContext;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel vm = new HeaderViewModel();

            var result = await _userService.GetById(_currentUserContext.Id);

            if(result.IsError)
            {
                vm.IsAuthenticated = false;
                return View(vm);
            }

            vm.IsAuthenticated = true;
            vm.WalletBalance = result.Value.WalletBalance;
            vm.Login = result.Value.Login;
            vm.Type = result.Value.Type;
            vm.CartCount = result.Value.Products.Count;

            return View(vm);
        }
    }
}
