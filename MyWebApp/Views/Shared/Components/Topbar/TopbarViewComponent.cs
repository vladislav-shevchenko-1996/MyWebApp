using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyWebApp.Views.Shared.Components.Topbar
{
    public class TopbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TopbarViewModel vm = new TopbarViewModel()
            {
                IsAdminUser = false
            };
            return View(vm);
        }
    }
}
