using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using K23CNT3_VuTienDuc_2310900023.Models;
using K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using K23CNT3_VuTienDuc_2310900023.Utilities;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{
    [Area("VtdAdmin")]
    public class VtdAuthController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdAuthController(VtdAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "VtdDashboard", new { area = "VtdAdmin" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VtdLogin model)
        {
            if (ModelState.IsValid)
            {
                // 1. Mã hóa mật khẩu người dùng nhập vào thành MD5
                string hashedPassword = VtdSecurity.ComputeMD5Hash(model.VtdPassword);

                // 2. So sánh với Database bằng mật khẩu ĐÃ MÃ HÓA (hashedPassword)
                var admin = _context.VtdAccounts
                    .SingleOrDefault(a => a.VtdEmail == model.VtdEmail && a.VtdPassword == hashedPassword);

                if (admin != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.VtdId.ToString()),
                    new Claim(ClaimTypes.Name, admin.VtdName),
                    new Claim(ClaimTypes.Email, admin.VtdEmail),
                    new Claim("Avatar", admin.VtdAvatar ?? "/images/accounts/admin-avatar.jpg"),
                    new Claim(ClaimTypes.Role, "Administrator")
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.Remember
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    return RedirectToAction("Index", "VtdDashboard", new { area = "VtdAdmin" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Xóa Cookie đăng nhập
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Thoát khỏi khu vực Admin và chuyển hướng về Trang chủ người dùng (VtdHome/Index)
            return RedirectToAction("Index", "VtdHome", new { area = "" });
        }
    }
}