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
    public class VtdBannersController : Controller
    {
        private readonly VtdBookStoreDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public VtdBannersController(VtdBookStoreDbContext context,
                                    IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
     

        // GET: VtdBanners
        public async Task<IActionResult> Index()
        {
            return View(await _context.VtdBanners.ToListAsync());
        }

        // GET: VtdBanners/Details/5
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

        // GET: VtdBanners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdBanners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("VtdId,VtdName,VtdStatus,VtdPriority,VtdDescription")] VtdBanner vtdBanner,
    IFormFile VtdImage)
        {
            if (ModelState.IsValid)
            {
                if (VtdImage != null && VtdImage.Length > 0)
                {
                    string folder = Path.Combine(_environment.WebRootPath, "images", "banner");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid() + Path.GetExtension(VtdImage.FileName);

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await VtdImage.CopyToAsync(stream);
                    }

                    vtdBanner.VtdImage = fileName;
                }

                _context.Add(vtdBanner);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vtdBanner);
        }

        // GET: VtdBanners/Edit/5
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

        // POST: VtdBanners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
     int id,
     [Bind("VtdId,VtdName,VtdStatus,VtdPriority,VtdDescription")] VtdBanner vtdBanner,
     IFormFile VtdImage)
        {
            if (id != vtdBanner.VtdId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var oldBanner = await _context.VtdBanners.AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.VtdId == id);

                    if (VtdImage != null && VtdImage.Length > 0)
                    {
                        string folder = Path.Combine(_environment.WebRootPath, "images", "banner");

                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        string fileName = Guid.NewGuid() + Path.GetExtension(VtdImage.FileName);

                        string filePath = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await VtdImage.CopyToAsync(stream);
                        }

                        vtdBanner.VtdImage = fileName;
                    }
                    else
                    {
                        vtdBanner.VtdImage = oldBanner?.VtdImage;
                    }

                    _context.Update(vtdBanner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdBannerExists(vtdBanner.VtdId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vtdBanner);
        }

        // GET: VtdBanners/Delete/5
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

        // POST: VtdBanners/Delete/5
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
