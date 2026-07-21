using Lab02.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab02.Controllers
{
    public class VtdHomeController : Controller
    {
        private readonly ILogger<VtdHomeController> _logger;

        public VtdHomeController(ILogger<VtdHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult VtdIndex()
        {
            return View();
        }

        public IActionResult VtdPrivacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
