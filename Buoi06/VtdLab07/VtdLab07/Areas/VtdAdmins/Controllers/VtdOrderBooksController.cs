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
    public class VtdOrderBooksController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdOrderBooksController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdOrderBooks
        public async Task<IActionResult> Index()
        {
            var vtdBookStoreDbContext = _context.VtdOrderBooks.Include(v => v.VtdAccount);
            return View(await vtdBookStoreDbContext.ToListAsync());
        }

        // GET: VtdOrderBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderBook = await _context.VtdOrderBooks
                .Include(v => v.VtdAccount)
                .FirstOrDefaultAsync(m => m.VtdOrderId == id);
            if (vtdOrderBook == null)
            {
                return NotFound();
            }

            return View(vtdOrderBook);
        }

        // GET: VtdOrderBooks/Create
        public IActionResult Create()
        {
            ViewData["VtdAccountId"] = new SelectList(_context.VtdAccounts, "VtdAccountId", "VtdAccountId");
            return View();
        }

        // POST: VtdOrderBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdOrderId,VtdOrderDate,VtdAccountId,VtdReceiveAddress,VtdReceivePhone,VtdOrderReceive,VtdNote,VtdStatus")] VtdOrderBook vtdOrderBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdOrderBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdAccountId"] = new SelectList(_context.VtdAccounts, "VtdAccountId", "VtdAccountId", vtdOrderBook.VtdAccountId);
            return View(vtdOrderBook);
        }

        // GET: VtdOrderBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderBook = await _context.VtdOrderBooks.FindAsync(id);
            if (vtdOrderBook == null)
            {
                return NotFound();
            }
            ViewData["VtdAccountId"] = new SelectList(_context.VtdAccounts, "VtdAccountId", "VtdAccountId", vtdOrderBook.VtdAccountId);
            return View(vtdOrderBook);
        }

        // POST: VtdOrderBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VtdOrderId,VtdOrderDate,VtdAccountId,VtdReceiveAddress,VtdReceivePhone,VtdOrderReceive,VtdNote,VtdStatus")] VtdOrderBook vtdOrderBook)
        {
            if (id != vtdOrderBook.VtdOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdOrderBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdOrderBookExists(vtdOrderBook.VtdOrderId))
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
            ViewData["VtdAccountId"] = new SelectList(_context.VtdAccounts, "VtdAccountId", "VtdAccountId", vtdOrderBook.VtdAccountId);
            return View(vtdOrderBook);
        }

        // GET: VtdOrderBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderBook = await _context.VtdOrderBooks
                .Include(v => v.VtdAccount)
                .FirstOrDefaultAsync(m => m.VtdOrderId == id);
            if (vtdOrderBook == null)
            {
                return NotFound();
            }

            return View(vtdOrderBook);
        }

        // POST: VtdOrderBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vtdOrderBook = await _context.VtdOrderBooks.FindAsync(id);
            if (vtdOrderBook != null)
            {
                _context.VtdOrderBooks.Remove(vtdOrderBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdOrderBookExists(string id)
        {
            return _context.VtdOrderBooks.Any(e => e.VtdOrderId == id);
        }
    }
}
