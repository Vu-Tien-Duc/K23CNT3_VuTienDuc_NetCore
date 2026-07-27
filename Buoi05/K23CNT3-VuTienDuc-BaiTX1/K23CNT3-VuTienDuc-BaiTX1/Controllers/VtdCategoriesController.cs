using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_BaiTX1.Models;

namespace K23CNT3_VuTienDuc_BaiTX1.Controllers
{
    public class VtdCategoriesController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdCategoriesController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.VtdCategories.ToListAsync());
        }

        // GET: VtdCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCategory = await _context.VtdCategories
                .FirstOrDefaultAsync(m => m.VtdCategoryId == id);
            if (vtdCategory == null)
            {
                return NotFound();
            }

            return View(vtdCategory);
        }

        // GET: VtdCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdCategoryId,VtdCategoryName")] VtdCategory vtdCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdCategory);
        }

        // GET: VtdCategories/Edit/5
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

        // POST: VtdCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdCategoryId,VtdCategoryName")] VtdCategory vtdCategory)
        {
            if (id != vtdCategory.VtdCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdCategoryExists(vtdCategory.VtdCategoryId))
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
            return View(vtdCategory);
        }

        // GET: VtdCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCategory = await _context.VtdCategories
                .FirstOrDefaultAsync(m => m.VtdCategoryId == id);
            if (vtdCategory == null)
            {
                return NotFound();
            }

            return View(vtdCategory);
        }

        // POST: VtdCategories/Delete/5
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
            return _context.VtdCategories.Any(e => e.VtdCategoryId == id);
        }
    }
}
