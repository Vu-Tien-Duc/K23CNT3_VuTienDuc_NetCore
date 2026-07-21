using Microsoft.AspNetCore.Mvc;
using VtdLab02_bt.Models;

namespace VtdLab02_bt.Controllers
{
	public class VtdProductController : Controller
	{
		public IActionResult VtdIndex()
		{
			List<VtdProduct> products = new List<VtdProduct>()
			{
				new VtdProduct()
				{
					Id=1,
					ProductName="iPhone 15 Pro Max",
					Image=Url.Content("~/images/iphone15.jpg"),
					Price=28990000,
					OldPrice=31990000,
					Brand="Apple",
					Description="Chip A17 Pro, 256GB"
				},

				new VtdProduct()
				{
					Id=2,
					ProductName="Samsung Galaxy S24 Ultra",
					Image=Url.Content("~/images/s24ultra.jpg"),
					Price=26990000,
					OldPrice=29990000,
					Brand="Samsung",
					Description="Snapdragon 8 Gen 3"
				},

				new VtdProduct()
				{
					Id=3,
					ProductName="Xiaomi 14 Ultra",
					Image=Url.Content("~/images/xiaomi14.jpg"),
					Price=22990000,
					OldPrice=24990000,
					Brand="Xiaomi",
					Description="Camera Leica"
				},

				new VtdProduct()
				{
					Id=4,
					ProductName="OPPO Find X7",
					Image=Url.Content("~/images/oppofindx7.jpg"),
					Price=18990000,
					OldPrice=20990000,
					Brand="OPPO",
					Description="Camera Hasselblad"
				},

				new VtdProduct()
				{
					Id=5,
					ProductName="Vivo X100 Pro",
					Image=Url.Content("~/images/vivox100.jpg"),
					Price=19990000,
					OldPrice=21990000,
					Brand="Vivo",
					Description="Camera ZEISS"
				},

				new VtdProduct()
				{
					Id=6,
					ProductName="Google Pixel 8 Pro",
					Image=Url.Content("~/images/pixel8pro.jpg"),
					Price=21990000,
					OldPrice=23990000,
					Brand="Google",
					Description="Google AI"
				}
			};

			ViewBag.Products = products;

			return View();
		}

		public IActionResult VtdDetail(int id)
		{
			var products = new List<VtdProduct>()
			{
				new VtdProduct(){Id=1,ProductName="iPhone 15 Pro Max",Image=Url.Content("~/images/iphone15.jpg"),Price=28990000,OldPrice=31990000,Brand="Apple",Description="Chip A17 Pro,256GB"},
				new VtdProduct(){Id=2,ProductName="Samsung Galaxy S24 Ultra",Image=Url.Content("~/images/s24ultra.jpg"),Price=26990000,OldPrice=29990000,Brand="Samsung",Description="Snapdragon 8 Gen 3"},
				new VtdProduct(){Id=3,ProductName="Xiaomi 14 Ultra",Image=Url.Content("~/images/xiaomi14.jpg"),Price=22990000,OldPrice=24990000,Brand="Xiaomi",Description="Camera Leica"},
				new VtdProduct(){Id=4,ProductName="OPPO Find X7",Image=Url.Content("~/images/oppofindx7.jpg"),Price=18990000,OldPrice=20990000,Brand="OPPO",Description="Camera Hasselblad"},
				new VtdProduct(){Id=5,ProductName="Vivo X100 Pro",Image=Url.Content("~/images/vivox100.jpg"),Price=19990000,OldPrice=21990000,Brand="Vivo",Description="Camera ZEISS"},
				new VtdProduct(){Id=6,ProductName="Google Pixel 8 Pro",Image=Url.Content("~/images/pixel8pro.jpg"),Price=21990000,OldPrice=23990000,Brand="Google",Description="Google AI"}
			};

			var product = products.FirstOrDefault(x => x.Id == id);

			ViewBag.Product = product;

			return View();
		}
	}
}
