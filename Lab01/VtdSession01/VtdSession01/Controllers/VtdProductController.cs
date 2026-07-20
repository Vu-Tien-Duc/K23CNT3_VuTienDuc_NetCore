using Microsoft.AspNetCore.Mvc;

namespace VtdSession01.Controllers
{
	public class VtdProductController : Controller
	{
		public IActionResult VtdIndex()
		{
			return View();
		}
		public IActionResult VtdDetails()
		{
			return View();
		}
	}
}
