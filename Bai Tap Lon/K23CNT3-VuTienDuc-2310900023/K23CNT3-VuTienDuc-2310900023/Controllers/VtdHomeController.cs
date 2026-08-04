using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;
using System.Diagnostics;

namespace K23CNT3_VuTienDuc_2310900023.Controllers
{
    public class VtdHomeController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdHomeController(VtdAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Lấy danh sách Banner đang hoạt động (VtdStatus = 1)
            // Sắp xếp theo ID giảm dần để lấy banner mới nhất
            var banners = await _context.VtdBanners
                                        .Where(b => b.VtdStatus == 1)
                                        .OrderByDescending(b => b.VtdId)
                                        .ToListAsync();
            ViewBag.Banners = banners;

            // 2. Lấy danh sách Danh mục sản phẩm (VtdStatus = 1)
            var categories = await _context.VtdCategories
                                           .Where(c => c.VtdStatus == 1)
                                           .OrderBy(c => c.VtdId)
                                           .ToListAsync();
            ViewBag.Categories = categories;

            // 3. Lấy 8 Sản phẩm mới nhất (VtdStatus = 1)
            var products = await _context.VtdProducts
                                         .Where(p => p.VtdStatus == 1)
                                         .OrderByDescending(p => p.VtdId) // Hoặc VtdCreatedDate nếu bạn có trường này
                                         .Take(12) // Lấy hẳn 12 sản phẩm cho đẹp (3 hàng x 4 cột)
                                         .ToListAsync();

            // Truyền Products qua Model chính
            return View(products);
        }

        // GET: VtdHome/ProductDetail/5
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không truyền ID
            }

            // Tìm sản phẩm theo ID
            var product = await _context.VtdProducts
                .FirstOrDefaultAsync(m => m.VtdId == id);

            if (product == null)
            {
                return NotFound(); // Nếu không tìm thấy sản phẩm trong Database
            }

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // GET: VtdHome/Products
        public async Task<IActionResult> Products(int? categoryId, string searchString)
        {
            // Lấy danh sách sản phẩm đang hoạt động
            var products = _context.VtdProducts.Where(p => p.VtdStatus == 1).AsQueryable();

            // Lọc theo danh mục nếu có click vào danh mục
            if (categoryId != null)
            {
                products = products.Where(p => p.VtdCategoryId == categoryId);
                ViewBag.CurrentCategoryId = categoryId;
            }

            // Lọc theo từ khóa tìm kiếm nếu có
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.VtdName.Contains(searchString));
                ViewBag.SearchString = searchString;
            }

            // Truyền danh sách Category ra View để làm Sidebar bộ lọc
            ViewBag.Categories = await _context.VtdCategories.Where(c => c.VtdStatus == 1).ToListAsync();

            return View(await products.ToListAsync());
        }
    }
}