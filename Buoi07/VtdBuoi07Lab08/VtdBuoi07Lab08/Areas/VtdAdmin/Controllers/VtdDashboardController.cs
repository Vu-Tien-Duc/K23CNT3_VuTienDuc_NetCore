using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VtdBuoi07Lab08.Models;

namespace VtdBuoi07Lab08.Areas.VtdAdmin.Controllers
{
    [Area("VtdAdmin")]
    public class VtdDashboardController : Controller
    {
        private readonly VtdAppDbContext _context;

        // Tiêm DbContext vào Controller
        public VtdDashboardController(VtdAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1. Tổng số đơn hàng
            ViewBag.TotalOrders = _context.VtdOrders.Count();

            // 2. Tổng doanh thu (Tổng Giá * Số lượng từ chi tiết đơn hàng)
            // Nếu chưa có đơn hàng nào, hàm Sum sẽ trả về 0
            ViewBag.TotalRevenue = _context.VtdOrderDetails.Sum(od => od.VtdPrice * od.VtdQuantity);

            // 3. Tổng số khách hàng
            ViewBag.TotalCustomers = _context.VtdCustomers.Count();

            // 4. Tổng số sản phẩm
            ViewBag.TotalProducts = _context.VtdProducts.Count();

            return View();
        }
    }
}