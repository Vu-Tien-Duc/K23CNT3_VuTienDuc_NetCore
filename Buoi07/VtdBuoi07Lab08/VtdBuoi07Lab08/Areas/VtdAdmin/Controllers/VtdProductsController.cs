using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtdBuoi07Lab08.Models;
using X.PagedList;

namespace VtdBuoi07Lab08.Areas.VtdAdmin.Controllers
{
    [Area("VtdAdmin")]
    public class VtdProductsController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdProductsController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdProducts
        // GET: VtdAdmin/VtdProducts
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số bản ghi trên 1 trang

            // Lấy dữ liệu sản phẩm, kết hợp lấy thông tin Category qua khóa ngoại
            var query = _context.VtdProducts.Include(v => v.VtdCategory).AsQueryable();

            // Tìm kiếm theo tên sản phẩm
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdName.Contains(name));
            }

            // Sắp xếp theo ID và phân trang
            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);

            // Lưu lại từ khóa tìm kiếm để hiển thị lại trên ô input
            ViewBag.keyword = name;

            return View(pagedList);
        }
        // GET: VtdAdmin/VtdProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdProduct = await _context.VtdProducts
                .Include(v => v.VtdCategory)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdProduct == null)
            {
                return NotFound();
            }

            return View(vtdProduct);
        }

        // GET: VtdAdmin/VtdProducts/Create
        public IActionResult Create()
        {
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdName");
            return View();
        }

        // POST: VtdAdmin/VtdProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdName,VtdImage,VtdPrice,VtdSalePrice,VtdStatus,VtdDescription,VtdCategoryId,VtdCreatedDate")] VtdProduct vtdProduct)
        {
            ModelState.Remove("VtdImage");
            ModelState.Remove("VtdCategory"); // Bỏ qua kiểm tra khóa ngoại để tránh báo lỗi require

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products");

                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdProduct.VtdImage = "/images/products/" + fileName;
                    }
                }
                vtdProduct.VtdCreatedDate = DateTime.Now;
                _context.Add(vtdProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdName", vtdProduct.VtdCategoryId);
            return View(vtdProduct);
        }

        // GET: VtdAdmin/VtdProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdProduct = await _context.VtdProducts.FindAsync(id);
            if (vtdProduct == null)
            {
                return NotFound();
            }
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdName", vtdProduct.VtdCategoryId);
            return View(vtdProduct);
        }

        // POST: VtdAdmin/VtdProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdName,VtdImage,VtdPrice,VtdSalePrice,VtdStatus,VtdDescription,VtdCategoryId,VtdCreatedDate")] VtdProduct vtdProduct)
        {
            if (id != vtdProduct.VtdId) return NotFound();

            ModelState.Remove("VtdImage");
            ModelState.Remove("VtdCategory");

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = file.FileName;
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products");

                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdProduct.VtdImage = "/images/products/" + fileName;
                        }
                    }
                    _context.Update(vtdProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdProductExists(vtdProduct.VtdId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdName", vtdProduct.VtdCategoryId);
            return View(vtdProduct);
        }

        // GET: VtdAdmin/VtdProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdProduct = await _context.VtdProducts
                .Include(v => v.VtdCategory)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdProduct == null)
            {
                return NotFound();
            }

            return View(vtdProduct);
        }

        // POST: VtdAdmin/VtdProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdProduct = await _context.VtdProducts.FindAsync(id);
            if (vtdProduct != null)
            {
                _context.VtdProducts.Remove(vtdProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdProductExists(int id)
        {
            return _context.VtdProducts.Any(e => e.VtdId == id);
        }
    }
}
