using Microsoft.AspNetCore.Mvc;

namespace VtdSession01.Controllers
{
    public class VtdDemoController : Controller
    {
        public IActionResult VtdIndex()
        {
            return View();
        }
    }
}
