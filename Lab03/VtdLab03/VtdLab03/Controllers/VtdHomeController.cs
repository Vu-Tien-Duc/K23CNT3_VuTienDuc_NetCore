using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VtdLab03.Models;


namespace VtdLab03.Controllers
{
    public class VtdHomeController : Controller
    {
        private readonly ILogger<VtdHomeController> _logger;


        protected VtdProduct product = new VtdProduct();

        public VtdHomeController(ILogger<VtdHomeController> logger)
        {
            _logger = logger;
        }

   
        public IActionResult VtdIndex()
        {
   
            var products = product.GetProductList();
            return View(products);
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