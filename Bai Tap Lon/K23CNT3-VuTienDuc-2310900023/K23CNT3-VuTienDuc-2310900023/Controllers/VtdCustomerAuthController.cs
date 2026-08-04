using Microsoft.AspNetCore.Mvc;
using K23CNT3_VuTienDuc_2310900023.Models;
using K23CNT3_VuTienDuc_2310900023.ViewModels;
using K23CNT3_VuTienDuc_2310900023.Utilities;

namespace K23CNT3_VuTienDuc_2310900023.Controllers
{
    public class VtdCustomerAuthController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdCustomerAuthController(VtdAppDbContext context)
        {
            _context = context;
        }

        // ================= ĐĂNG KÝ =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(VtdCustomerRegister model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại chưa
                if (_context.VtdCustomers.Any(c => c.VtdEmail == model.VtdEmail))
                {
                    ModelState.AddModelError("VtdEmail", "Email này đã được sử dụng!");
                    return View(model);
                }

                // Tạo khách hàng mới và mã hóa mật khẩu
                var customer = new VtdCustomer
                {
                    VtdFullName = model.VtdFullName,
                    VtdEmail = model.VtdEmail,
                    VtdPhone = model.VtdPhone,
                    VtdPassword = VtdSecurity.ComputeMD5Hash(model.VtdPassword),

                    // Các trường bắt buộc khác gán giá trị mặc định
                    VtdAddress = "",
                    VtdAvatar = "/images/customers/default.jpg",
                    VtdGender = "Khác",
                    VtdFacebook = "",
                    VtdBirthday = DateTime.Now
                };

                _context.VtdCustomers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        // ================= ĐĂNG NHẬP =================
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu có session rồi thì về trang chủ
            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                return RedirectToAction("Index", "VtdHome");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(VtdCustomerLogin model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = VtdSecurity.ComputeMD5Hash(model.VtdPassword);

                var customer = _context.VtdCustomers
                    .SingleOrDefault(c => c.VtdEmail == model.VtdEmail && c.VtdPassword == hashedPassword);

                if (customer != null)
                {
                    // Lưu thông tin vào Session
                    HttpContext.Session.SetString("CustomerId", customer.VtdId.ToString());
                    HttpContext.Session.SetString("CustomerName", customer.VtdFullName);
                    HttpContext.Session.SetString("CustomerAvatar", customer.VtdAvatar ?? "");

                    return RedirectToAction("Index", "VtdHome");
                }

                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác.");
            }
            return View(model);
        }

        // ================= ĐĂNG XUẤT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Remove("CustomerName");
            HttpContext.Session.Remove("CustomerAvatar");

            return RedirectToAction("Index", "VtdHome");
        }
    }
}