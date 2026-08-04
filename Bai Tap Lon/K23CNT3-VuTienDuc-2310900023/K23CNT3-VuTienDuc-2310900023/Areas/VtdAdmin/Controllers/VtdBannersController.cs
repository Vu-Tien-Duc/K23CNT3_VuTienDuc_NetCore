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

    public class VtdBannersController : VtdBaseController
    {
        private readonly VtdAppDbContext _context;

        public VtdBannersController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdBanners
        // GET: VtdAdmin/VtdBanners (Đã cập nhật Phân trang & Tìm kiếm)
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Hiển thị 5 bản ghi 1 trang
            var query = _context.VtdBanners.AsQueryable();

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdName.Contains(name));
            }

            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdBanners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBanner = await _context.VtdBanners
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdBanner == null)
            {
                return NotFound();
            }

            return View(vtdBanner);
        }

        // GET: VtdAdmin/VtdBanners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAdmin/VtdBanners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdName,VtdStatus,VtdPrioty,VtdCreatedDate,VtdImage,VtdDescription")] VtdBanner vtdBanner)
        {
            ModelState.Remove("VtdImage");
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\banners");

                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdBanner.VtdImage = "/images/banners/" + fileName;
                    }
                }
                vtdBanner.VtdCreatedDate = DateTime.Now; // Tự động gán ngày tạo
                _context.Add(vtdBanner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdBanner);
        }
        // GET: VtdAdmin/VtdBanners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBanner = await _context.VtdBanners.FindAsync(id);
            if (vtdBanner == null)
            {
                return NotFound();
            }
            return View(vtdBanner);
        }

        // POST: VtdAdmin/VtdBanners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdName,VtdStatus,VtdPrioty,VtdCreatedDate,VtdImage,VtdDescription")] VtdBanner vtdBanner)
        {
            if (id != vtdBanner.VtdId) return NotFound();

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
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\banners");

                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdBanner.VtdImage = "/images/banners/" + fileName;
                        }
                    }
                    _context.Update(vtdBanner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdBannerExists(vtdBanner.VtdId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vtdBanner);
        }

        // GET: VtdAdmin/VtdBanners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBanner = await _context.VtdBanners
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdBanner == null)
            {
                return NotFound();
            }

            return View(vtdBanner);
        }

        // POST: VtdAdmin/VtdBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdBanner = await _context.VtdBanners.FindAsync(id);
            if (vtdBanner != null)
            {
                _context.VtdBanners.Remove(vtdBanner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdBannerExists(int id)
        {
            return _context.VtdBanners.Any(e => e.VtdId == id);
        }
    }
}
