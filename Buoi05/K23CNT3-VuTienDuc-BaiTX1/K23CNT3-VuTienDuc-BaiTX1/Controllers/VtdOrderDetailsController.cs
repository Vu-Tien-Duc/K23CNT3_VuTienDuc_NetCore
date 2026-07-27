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
    public class VtdOrderDetailsController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdOrderDetailsController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdOrderDetails
        public async Task<IActionResult> Index()
        {
            var vtdBookStoreDbContext = _context.VtdOrderDetails.Include(v => v.VtdBook).Include(v => v.VtdOrder);
            return View(await vtdBookStoreDbContext.ToListAsync());
        }

        // GET: VtdOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderDetail = await _context.VtdOrderDetails
                .Include(v => v.VtdBook)
                .Include(v => v.VtdOrder)
                .FirstOrDefaultAsync(m => m.VtdOrderDetailId == id);
            if (vtdOrderDetail == null)
            {
                return NotFound();
            }

            return View(vtdOrderDetail);
        }

        // GET: VtdOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["VtdBookId"] = new SelectList(_context.VtdBooks, "VtdBookId", "VtdBookId");
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrderBooks, "VtdOrderId", "VtdOrderId");
            return View();
        }

        // POST: VtdOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdOrderDetailId,VtdOrderId,VtdBookId,VtdQuantity,VtdPrice,VtdTotalMoney")] VtdOrderDetail vtdOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdBookId"] = new SelectList(_context.VtdBooks, "VtdBookId", "VtdBookId", vtdOrderDetail.VtdBookId);
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrderBooks, "VtdOrderId", "VtdOrderId", vtdOrderDetail.VtdOrderId);
            return View(vtdOrderDetail);
        }

        // GET: VtdOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderDetail = await _context.VtdOrderDetails.FindAsync(id);
            if (vtdOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["VtdBookId"] = new SelectList(_context.VtdBooks, "VtdBookId", "VtdBookId", vtdOrderDetail.VtdBookId);
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrderBooks, "VtdOrderId", "VtdOrderId", vtdOrderDetail.VtdOrderId);
            return View(vtdOrderDetail);
        }

        // POST: VtdOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdOrderDetailId,VtdOrderId,VtdBookId,VtdQuantity,VtdPrice,VtdTotalMoney")] VtdOrderDetail vtdOrderDetail)
        {
            if (id != vtdOrderDetail.VtdOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdOrderDetailExists(vtdOrderDetail.VtdOrderDetailId))
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
            ViewData["VtdBookId"] = new SelectList(_context.VtdBooks, "VtdBookId", "VtdBookId", vtdOrderDetail.VtdBookId);
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrderBooks, "VtdOrderId", "VtdOrderId", vtdOrderDetail.VtdOrderId);
            return View(vtdOrderDetail);
        }

        // GET: VtdOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderDetail = await _context.VtdOrderDetails
                .Include(v => v.VtdBook)
                .Include(v => v.VtdOrder)
                .FirstOrDefaultAsync(m => m.VtdOrderDetailId == id);
            if (vtdOrderDetail == null)
            {
                return NotFound();
            }

            return View(vtdOrderDetail);
        }

        // POST: VtdOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdOrderDetail = await _context.VtdOrderDetails.FindAsync(id);
            if (vtdOrderDetail != null)
            {
                _context.VtdOrderDetails.Remove(vtdOrderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdOrderDetailExists(int id)
        {
            return _context.VtdOrderDetails.Any(e => e.VtdOrderDetailId == id);
        }
    }
}
