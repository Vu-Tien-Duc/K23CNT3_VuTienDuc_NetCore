using Microsoft.AspNetCore.Mvc;

namespace VtdLab07.Areas.VtdAdmins.Controllers
{
	[Area("VtdAdmins")]
	public class VtdDashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
