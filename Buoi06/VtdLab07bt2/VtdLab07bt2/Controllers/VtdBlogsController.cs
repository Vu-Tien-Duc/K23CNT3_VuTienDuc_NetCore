using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtdLab07bt2.Models;

namespace VtdLab07bt2.Controllers
{
    public class VtdBlogsController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdBlogsController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.VtdBlogs.ToListAsync());
        }

        // GET: VtdBlogs/Details/5
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

        // GET: VtdBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdName,VtdStatus,VtdCreatedDate,VtdImage,VtdDescription")] VtdBlog vtdBlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdBlog);
        }

        // GET: VtdBlogs/Edit/5
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

        // POST: VtdBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdName,VtdStatus,VtdCreatedDate,VtdImage,VtdDescription")] VtdBlog vtdBlog)
        {
            if (id != vtdBlog.VtdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdBlogExists(vtdBlog.VtdId))
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
            return View(vtdBlog);
        }

        // GET: VtdBlogs/Delete/5
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

        // POST: VtdBlogs/Delete/5
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
