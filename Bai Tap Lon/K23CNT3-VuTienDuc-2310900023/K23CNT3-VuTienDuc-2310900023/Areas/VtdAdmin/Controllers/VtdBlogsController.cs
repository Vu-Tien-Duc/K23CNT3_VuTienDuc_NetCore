using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;
using X.PagedList;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{

    public class VtdBlogsController : VtdBaseController
    {
        private readonly VtdAppDbContext _context;

        public VtdBlogsController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdBlogs
        // GET: VtdAdmin/VtdBlogs
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số bản ghi trên 1 trang
            var query = _context.VtdBlogs.AsQueryable();

            // Lọc theo tên nếu có từ khóa tìm kiếm
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdName.Contains(name));
            }

            // Thực hiện sắp xếp và phân trang
            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);

            // Lưu lại từ khóa tìm kiếm để hiển thị trên View
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBlog = await _context.VtdBlogs
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdBlog == null)
            {
                return NotFound();
            }

            return View(vtdBlog);
        }

        // GET: VtdAdmin/VtdBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAdmin/VtdBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdName,VtdStatus,VtdViewCount,VtdCreatedDate,VtdImage,VtdDescription")] VtdBlog vtdBlog)
        {
            ModelState.Remove("VtdImage");
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\blogs");

                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdBlog.VtdImage = "/images/blogs/" + fileName;
                    }
                }
                vtdBlog.VtdCreatedDate = DateTime.Now;
                _context.Add(vtdBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdBlog);
        }

        // GET: VtdAdmin/VtdBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBlog = await _context.VtdBlogs.FindAsync(id);
            if (vtdBlog == null)
            {
                return NotFound();
            }
            return View(vtdBlog);
        }

        // POST: VtdAdmin/VtdBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdName,VtdStatus,VtdViewCount,VtdCreatedDate,VtdImage,VtdDescription")] VtdBlog vtdBlog)
        {
            if (id != vtdBlog.VtdId) return NotFound();

            ModelState.Remove("VtdImage");
            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = file.FileName;
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\blogs");

                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdBlog.VtdImage = "/images/blogs/" + fileName;
                        }
                    }
                    _context.Update(vtdBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdBlogExists(vtdBlog.VtdId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vtdBlog);
        }

        // GET: VtdAdmin/VtdBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBlog = await _context.VtdBlogs
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdBlog == null)
            {
                return NotFound();
            }

            return View(vtdBlog);
        }

        // POST: VtdAdmin/VtdBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdBlog = await _context.VtdBlogs.FindAsync(id);
            if (vtdBlog != null)
            {
                _context.VtdBlogs.Remove(vtdBlog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdBlogExists(int id)
        {
            return _context.VtdBlogs.Any(e => e.VtdId == id);
        }
    }
}
