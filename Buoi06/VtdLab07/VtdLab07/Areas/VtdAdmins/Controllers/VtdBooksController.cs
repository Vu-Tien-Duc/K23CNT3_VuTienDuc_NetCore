using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtdLab07.Models;

namespace VtdLab07.Controllers
{
    [Area("VtdAdmins")]
    public class VtdBooksController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdBooksController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdBooks
        public async Task<IActionResult> Index()
        {
            var vtdBookStoreDbContext = _context.VtdBooks.Include(v => v.VtdCategory).Include(v => v.VtdPublisher);
            return View(await vtdBookStoreDbContext.ToListAsync());
        }

        // GET: VtdBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBook = await _context.VtdBooks
                .Include(v => v.VtdCategory)
                .Include(v => v.VtdPublisher)
                .FirstOrDefaultAsync(m => m.VtdBookId == id);
            if (vtdBook == null)
            {
                return NotFound();
            }

            return View(vtdBook);
        }

        // GET: VtdBooks/Create
        public IActionResult Create()
        {
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdCategoryId", "VtdCategoryId");
            ViewData["VtdPublisherId"] = new SelectList(_context.VtdPublishers, "VtdPublisherId", "VtdPublisherId");
            return View();
        }

        // POST: VtdBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
     [Bind("VtdBookId,VtdTitle,VtdAuthor,VtdRelease,VtdPrice,VtdDescription,VtdPublisherId,VtdCategoryId")] VtdBook vtdBook,
     IFormFile? anhBia)
        {
            if (ModelState.IsValid)
            {
                if (anhBia != null && anhBia.Length > 0)
                {
                    string fileName = Path.GetFileName(anhBia.FileName);

                    string folder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "book");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await anhBia.CopyToAsync(stream);
                    }

                    vtdBook.VtdPicture = "/images/book/" + fileName;
                }

                _context.Add(vtdBook);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["VtdCategoryId"] = new SelectList(
                _context.VtdCategories,
                "VtdCategoryId",
                "VtdCategoryName",
                vtdBook.VtdCategoryId);

            ViewData["VtdPublisherId"] = new SelectList(
                _context.VtdPublishers,
                "VtdPublisherId",
                "VtdPublisherName",
                vtdBook.VtdPublisherId);

            return View(vtdBook);
        }

        // GET: VtdBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBook = await _context.VtdBooks.FindAsync(id);
            if (vtdBook == null)
            {
                return NotFound();
            }
            ViewData["VtdCategoryId"] = new SelectList(_context.VtdCategories, "VtdCategoryId", "VtdCategoryId", vtdBook.VtdCategoryId);
            ViewData["VtdPublisherId"] = new SelectList(_context.VtdPublishers, "VtdPublisherId", "VtdPublisherId", vtdBook.VtdPublisherId);
            return View(vtdBook);
        }

        // POST: VtdBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    string id,
    [Bind("VtdBookId,VtdTitle,VtdAuthor,VtdRelease,VtdPrice,VtdDescription,VtdPicture,VtdPublisherId,VtdCategoryId")] VtdBook vtdBook,
    IFormFile? anhBia)
        {
            if (id != vtdBook.VtdBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (anhBia != null && anhBia.Length > 0)
                    {
                        string fileName = Path.GetFileName(anhBia.FileName);

                        string folder = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "images",
                            "book");

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string filePath = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await anhBia.CopyToAsync(stream);
                        }

                        vtdBook.VtdPicture = "/images/book/" + fileName;
                    }
                    else
                    {
                        vtdBook.VtdPicture = _context.VtdBooks
                            .AsNoTracking()
                            .First(x => x.VtdBookId == id)
                            .VtdPicture;
                    }

                    _context.Update(vtdBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdBookExists(vtdBook.VtdBookId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["VtdCategoryId"] = new SelectList(
                _context.VtdCategories,
                "VtdCategoryId",
                "VtdCategoryName",
                vtdBook.VtdCategoryId);

            ViewData["VtdPublisherId"] = new SelectList(
                _context.VtdPublishers,
                "VtdPublisherId",
                "VtdPublisherName",
                vtdBook.VtdPublisherId);

            return View(vtdBook);
        }

        // GET: VtdBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdBook = await _context.VtdBooks
                .Include(v => v.VtdCategory)
                .Include(v => v.VtdPublisher)
                .FirstOrDefaultAsync(m => m.VtdBookId == id);
            if (vtdBook == null)
            {
                return NotFound();
            }

            return View(vtdBook);
        }

        // POST: VtdBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vtdBook = await _context.VtdBooks.FindAsync(id);
            if (vtdBook != null)
            {
                _context.VtdBooks.Remove(vtdBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdBookExists(string id)
        {
            return _context.VtdBooks.Any(e => e.VtdBookId == id);
        }
    }
}
