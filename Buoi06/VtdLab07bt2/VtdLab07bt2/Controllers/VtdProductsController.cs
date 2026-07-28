using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtdLab07bt2.Models;

using Microsoft.AspNetCore.Hosting;
namespace VtdLab07bt2.Controllers
{
    public class VtdProductsController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        private readonly IWebHostEnvironment _environment;

        public VtdProductsController(
            VtdBookStoreDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: VtdProducts
        public async Task<IActionResult> Index()
        {
            var vtdBookStoreDbContext = _context.VtdProducts.Include(v => v.VtdCategory);
            return View(await vtdBookStoreDbContext.ToListAsync());
        }

        // GET: VtdProducts/Details/5
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

        // GET: VtdProducts/Create
        public IActionResult Create()
        {
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdId");
            return View();
        }

        // POST: VtdProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
      [Bind("VtdId,VtdName,VtdPrice,VtdSalePrice,VtdStatus,VtdCategoryId,VtdDescription")] VtdProduct vtdProduct,
      IFormFile? ImageFile) // Đổi tên tham số thành ImageFile
        {
            if (ModelState.IsValid)
            {
                // Gán ngày tạo tự động nếu form không gửi lên
                vtdProduct.VtdCreatedDate = DateOnly.FromDateTime(DateTime.Now);

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string folder = Path.Combine(_environment.WebRootPath, "images", "product");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    vtdProduct.VtdImage = fileName;
                }

                _context.Add(vtdProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdId", "VtdName", vtdProduct.VtdCategoryId);
            return View(vtdProduct);
        }

 
        // GET: VtdProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vtdProduct = await _context.VtdProducts.FindAsync(id);

            if (vtdProduct == null)
                return NotFound();

            ViewData["VtdCategoryId"] = new SelectList(
                _context.VtdCategories,
                "VtdId",
                "VtdName",
                vtdProduct.VtdCategoryId);

            return View(vtdProduct);
        }

        // POST: VtdProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: VtdProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
       int id,
       [Bind("VtdId,VtdName,VtdPrice,VtdSalePrice,VtdStatus,VtdCategoryId,VtdCreatedDate,VtdDescription,VtdImage")] VtdProduct vtdProduct,
       IFormFile? ImageFile)
        {
            if (id != vtdProduct.VtdId)
                return NotFound();

            // ===== THÊM ĐOẠN NÀY =====
            if (!ModelState.IsValid)
            {
                string error = "";

                foreach (var item in ModelState)
                {
                    foreach (var e in item.Value.Errors)
                    {
                        error += $"{item.Key} : {e.ErrorMessage}<br/>";
                    }
                }

                return Content(error, "text/html");
            }
            // ==========================

            var old = await _context.VtdProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.VtdId == id);

            if (old == null)
                return NotFound();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string folder = Path.Combine(_environment.WebRootPath, "images", "product");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);

                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                vtdProduct.VtdImage = fileName;
            }
            else
            {
                vtdProduct.VtdImage = old.VtdImage;
            }

            _context.Update(vtdProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: VtdProducts/Delete/5
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

        // POST: VtdProducts/Delete/5
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
