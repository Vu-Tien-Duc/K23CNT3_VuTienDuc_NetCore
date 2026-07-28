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
    public class VtdPublishersController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdPublishersController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdPublishers
        public async Task<IActionResult> Index()
        {
            return View(await _context.VtdPublishers.ToListAsync());
        }

        // GET: VtdPublishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdPublisher = await _context.VtdPublishers
                .FirstOrDefaultAsync(m => m.VtdPublisherId == id);
            if (vtdPublisher == null)
            {
                return NotFound();
            }

            return View(vtdPublisher);
        }

        // GET: VtdPublishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdPublishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdPublisherId,VtdPublisherName,VtdPhone,VtdAddress")] VtdPublisher vtdPublisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdPublisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdPublisher);
        }

        // GET: VtdPublishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdPublisher = await _context.VtdPublishers.FindAsync(id);
            if (vtdPublisher == null)
            {
                return NotFound();
            }
            return View(vtdPublisher);
        }

        // POST: VtdPublishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdPublisherId,VtdPublisherName,VtdPhone,VtdAddress")] VtdPublisher vtdPublisher)
        {
            if (id != vtdPublisher.VtdPublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdPublisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdPublisherExists(vtdPublisher.VtdPublisherId))
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
            return View(vtdPublisher);
        }

        // GET: VtdPublishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdPublisher = await _context.VtdPublishers
                .FirstOrDefaultAsync(m => m.VtdPublisherId == id);
            if (vtdPublisher == null)
            {
                return NotFound();
            }

            return View(vtdPublisher);
        }

        // POST: VtdPublishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdPublisher = await _context.VtdPublishers.FindAsync(id);
            if (vtdPublisher != null)
            {
                _context.VtdPublishers.Remove(vtdPublisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdPublisherExists(int id)
        {
            return _context.VtdPublishers.Any(e => e.VtdPublisherId == id);
        }
    }
}
