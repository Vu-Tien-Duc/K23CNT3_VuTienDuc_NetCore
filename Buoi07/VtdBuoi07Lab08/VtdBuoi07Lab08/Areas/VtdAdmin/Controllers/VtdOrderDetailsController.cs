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
    public class VtdOrderDetailsController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdOrderDetailsController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdOrderDetails
        // GET: VtdAdmin/VtdOrderDetails
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số bản ghi trên 1 trang

            // Lấy dữ liệu kèm theo khóa ngoại Order và Product
            var query = _context.VtdOrderDetails
                .Include(v => v.VtdOrder)
                .Include(v => v.VtdProduct)
                .AsQueryable();

            // Tìm kiếm theo tên sản phẩm nằm trong chi tiết đơn hàng
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdProduct.VtdName.Contains(name));
            }

            // Sắp xếp và phân trang
            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);

            // Lưu lại từ khóa tìm kiếm
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderDetail = await _context.VtdOrderDetails
                .Include(v => v.VtdOrder)
                .Include(v => v.VtdProduct)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdOrderDetail == null)
            {
                return NotFound();
            }

            return View(vtdOrderDetail);
        }

        // GET: VtdAdmin/VtdOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrders, "VtdId", "VtdId");
            ViewData["VtdProductId"] = new SelectList(_context.VtdProducts, "VtdId", "VtdName");
            return View();
        }

        // POST: VtdAdmin/VtdOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdOrderId,VtdProductId,VtdQuantity,VtdPrice")] VtdOrderDetail vtdOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrders, "VtdId", "VtdId", vtdOrderDetail.VtdOrderId);
            ViewData["VtdProductId"] = new SelectList(_context.VtdProducts, "VtdId", "VtdName", vtdOrderDetail.VtdProductId);
            return View(vtdOrderDetail);
        }

        // GET: VtdAdmin/VtdOrderDetails/Edit/5
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
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrders, "VtdId", "VtdId", vtdOrderDetail.VtdOrderId);
            ViewData["VtdProductId"] = new SelectList(_context.VtdProducts, "VtdId", "VtdName", vtdOrderDetail.VtdProductId);
            return View(vtdOrderDetail);
        }

        // POST: VtdAdmin/VtdOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdOrderId,VtdProductId,VtdQuantity,VtdPrice")] VtdOrderDetail vtdOrderDetail)
        {
            if (id != vtdOrderDetail.VtdId)
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
                    if (!VtdOrderDetailExists(vtdOrderDetail.VtdId))
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
            ViewData["VtdOrderId"] = new SelectList(_context.VtdOrders, "VtdId", "VtdId", vtdOrderDetail.VtdOrderId);
            ViewData["VtdProductId"] = new SelectList(_context.VtdProducts, "VtdId", "VtdName", vtdOrderDetail.VtdProductId);
            return View(vtdOrderDetail);
        }

        // GET: VtdAdmin/VtdOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrderDetail = await _context.VtdOrderDetails
                .Include(v => v.VtdOrder)
                .Include(v => v.VtdProduct)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdOrderDetail == null)
            {
                return NotFound();
            }

            return View(vtdOrderDetail);
        }

        // POST: VtdAdmin/VtdOrderDetails/Delete/5
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
            return _context.VtdOrderDetails.Any(e => e.VtdId == id);
        }
    }
}
