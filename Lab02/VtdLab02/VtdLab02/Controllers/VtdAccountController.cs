using Microsoft.AspNetCore.Mvc;
using Lab02.Models;
using System.Security.Principal;
namespace Lab02.Controllers
{
    public class VtdAccountController : Controller
    {
        public IActionResult VtdIndex()
		{
			List<VtdAccount> accounts = new List<VtdAccount>
			{
				new VtdAccount()
				{
					Id = 1,Name="Hoàng Anh",
					Email="anh@gmail.com",
					Phone="0986456789",
					Address="Hà Nội",
					Avatar= Url.Content("~/images/anh2.jpg"),
					Gender=1, Bio="My name is small",
					Birthday= new DateTime(1998,7,15)
				},
				new VtdAccount()
				{
					Id = 2,Name="Trường Giang",
					Email="giang@gmail.com",
					Phone="0986456789",
					Address="Hà Nội",
					Avatar= Url.Content("~/images/anh3.jpg"),
					Gender=1, Bio="My name is small",
					Birthday= new DateTime(1998,7,15)
					},
				new VtdAccount()
				{
					Id = 3,Name="Hoàng Thúy",
					Email="thuy@gmail.com",
					Phone="0986456789",
					Address="Hà Nội",
					Avatar= Url.Content("~/images/anh4.jpg"),
					Gender=1, Bio="My name is small",
					Birthday= new DateTime(1998,7,15)
					},
			};
			ViewBag.Accounts = accounts;
			return View();
		}
	
		public IActionResult VtdProfile(int id)
		{
			List<VtdAccount> accounts = new List<VtdAccount>
	{
		new VtdAccount()
		{
			Id = 1,
			Name = "Hoàng Anh",
			Email = "anh@gmail.com",
			Phone = "0986456789",
			Address = "Hà Nội",
			Avatar = Url.Content("~/images/anh2.jpg"),
			Gender = 1,
			Bio = "My name is small",
			Birthday = new DateTime(1998,7,15)
		},
		new VtdAccount()
		{
			Id = 2,
			Name = "Trường Giang",
			Email = "giang@gmail.com",
			Phone = "0986456789",
			Address = "Hà Nội",
			Avatar = Url.Content("~/images/anh3.jpg"),
			Gender = 1,
			Bio = "My name is small",
			Birthday = new DateTime(1998,7,15)
		},
		new VtdAccount()
		{
			Id = 3,
			Name = "Hoàng Thúy",
			Email = "thuy@gmail.com",
			Phone = "0986456789",
			Address = "Hà Nội",
			Avatar = Url.Content("~/images/anh4.jpg"),
			Gender = 1,
			Bio = "My name is small",
			Birthday = new DateTime(1998,7,15)
		}
	};

			var account = accounts.FirstOrDefault(x => x.Id == id);

			if (account == null)
			{
				return NotFound();
			}

			ViewBag.account = account;

			return View();
		}
	}
}