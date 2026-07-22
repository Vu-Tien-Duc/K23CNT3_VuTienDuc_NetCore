using Microsoft.AspNetCore.Mvc;

namespace VtdLab03.ViewComponents
{
    public class VtdHotProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
