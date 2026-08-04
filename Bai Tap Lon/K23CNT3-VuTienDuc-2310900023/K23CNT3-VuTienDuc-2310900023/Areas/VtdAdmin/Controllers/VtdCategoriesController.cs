using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{

    public class VtdCategoriesController : VtdBaseController
    {
        private readonly VtdAppDbContext _context;

        public VtdCategoriesController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdCategories
      
        public async Task<IActionResult> Index(string name, int page=1)
        {
            int limit = 5;
            var category = await _context.VtdCategories.ToPagedListAsync(page, limit);
            // nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                category = await _context.VtdCategories.Where(c =>
                c.VtdName.Contains(name)).OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(category);
        }

        // GET: VtdAdmin/VtdCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCategory = await _context.VtdCategories
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdCategory == null)
            {
                return NotFound();
            }

            return View(vtdCategory);
        }

        // GET: VtdAdmin/VtdCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAdmin/VtdCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // SỬA LỖI 1: Cập nhật lại đúng tên các thuộc tính Vtd... trong [Bind]
        public async Task<IActionResult> Create([Bind("VtdId,VtdName,VtdStatus,VtdCreatedDate,VtdImage,VtdDescription")] VtdCategory vtdcategory)
        {
            // SỬA LỖI 2: Xóa Validate mặc định của VtdImage và VtdProducts (nếu có)
            ModelState.Remove("VtdImage");
            ModelState.Remove("VtdProducts");

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    // upload ảnh vào thư mục wwwroot\images\categories
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdcategory.VtdImage = "/images/categories/" + FileName;
                    }
                }
                vtdcategory.VtdCreatedDate = DateTime.Now;
                _context.Add(vtdcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdcategory);
        }

        // GET: VtdAdmin/VtdCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCategory = await _context.VtdCategories.FindAsync(id);
            if (vtdCategory == null)
            {
                return NotFound();
            }
            return View(vtdCategory);
        }

        // POST: VtdAdmin/VtdCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Cập nhật lại đúng tên thuộc tính trong [Bind]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdName,VtdStatus,VtdCreatedDate,VtdImage,VtdDescription")] VtdCategory vtdcategory)
        {
            if (id != vtdcategory.VtdId)
            {
                return NotFound();
            }

            // Loại bỏ Validate cho VtdImage
            ModelState.Remove("VtdImage");
            ModelState.Remove("VtdProducts");

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count() > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var FileName = file.FileName;

                        // Đã sửa lại đường dẫn cho đồng bộ với Create (Category -> categories)
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdcategory.VtdImage = "/images/categories/" + FileName;
                        }
                    }

                    _context.Update(vtdcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdCategoryExists(vtdcategory.VtdId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vtdcategory);
        }

        // GET: VtdAdmin/VtdCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCategory = await _context.VtdCategories
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdCategory == null)
            {
                return NotFound();
            }

            return View(vtdCategory);
        }

        // POST: VtdAdmin/VtdCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdCategory = await _context.VtdCategories.FindAsync(id);
            if (vtdCategory != null)
            {
                _context.VtdCategories.Remove(vtdCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdCategoryExists(int id)
        {
            return _context.VtdCategories.Any(e => e.VtdId == id);
        }
    }
}
