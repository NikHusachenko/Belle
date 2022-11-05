using Microsoft.AspNetCore.Mvc;

namespace Belle.Web.Views.Shared.Components.HeaderComponent
{
    public class HeaderComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
