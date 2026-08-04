using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;

namespace K23CNT3_VuTienDuc_2310900023.Controllers
{
    public class VtdBlogController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdBlogController(VtdAppDbContext context)
        {
            _context = context;
        }

        // ================= TRANG DANH SÁCH BÀI VIẾT =================
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách blog, mới nhất lên đầu
            var blogs = await _context.VtdBlogs
                                      .Where(b => b.VtdStatus == 1)
                                      .OrderByDescending(b => b.VtdId)
                                      .ToListAsync();
            return View(blogs);
        }

        // ================= TRANG CHI TIẾT BÀI VIẾT =================
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var blog = await _context.VtdBlogs.FirstOrDefaultAsync(b => b.VtdId == id);

            if (blog == null) return NotFound();

            return View(blog);
        }
    }
}