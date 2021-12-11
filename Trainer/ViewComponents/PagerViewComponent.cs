using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trainer.Data;

namespace Trainer.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase result, string viewName)
        {
            result.LinkTemplate = Url.Action(RouteData.Values["action"].ToString(), new { page = "{0}" });

            return await Task.FromResult(View(viewName, result));
        }
    }
}
