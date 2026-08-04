using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;
using X.PagedList;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{

    public class VtdOrdersController : VtdBaseController
    {
        private readonly VtdAppDbContext _context;

        public VtdOrdersController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdOrders
        // GET: VtdAdmin/VtdOrders
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số bản ghi trên 1 trang

            // Lấy dữ liệu kèm theo khóa ngoại Customer
            var query = _context.VtdOrders
                .Include(v => v.VtdCustomer)
                .AsQueryable();

            // Tìm kiếm theo tên người nhận đơn hàng (VtdName)
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdName.Contains(name));
            }

            // Sắp xếp theo ID và phân trang
            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);

            // Lưu lại từ khóa tìm kiếm để hiển thị trên ô input
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrders = await _context.VtdOrders
                .Include(v => v.VtdCustomer)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdOrders == null)
            {
                return NotFound();
            }

            return View(vtdOrders);
        }

        // GET: VtdAdmin/VtdOrders/Create
        public IActionResult Create()
        {
            ViewData["VtdCustomerId"] = new SelectList(_context.VtdCustomers, "VtdId", "VtdAddress");
            return View();
        }

        // POST: VtdAdmin/VtdOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdCustomerId,VtdName,VtdEmail,VtdAddress,VtdCreatedDate,VtdStatus")] VtdOrders vtdOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vtdOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VtdCustomerId"] = new SelectList(_context.VtdCustomers, "VtdId", "VtdAddress", vtdOrders.VtdCustomerId);
            return View(vtdOrders);
        }

        // GET: VtdAdmin/VtdOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrders = await _context.VtdOrders.FindAsync(id);
            if (vtdOrders == null)
            {
                return NotFound();
            }
            ViewData["VtdCustomerId"] = new SelectList(_context.VtdCustomers, "VtdId", "VtdAddress", vtdOrders.VtdCustomerId);
            return View(vtdOrders);
        }

        // POST: VtdAdmin/VtdOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdCustomerId,VtdName,VtdEmail,VtdAddress,VtdCreatedDate,VtdStatus")] VtdOrders vtdOrders)
        {
            if (id != vtdOrders.VtdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vtdOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdOrdersExists(vtdOrders.VtdId))
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
            ViewData["VtdCustomerId"] = new SelectList(_context.VtdCustomers, "VtdId", "VtdAddress", vtdOrders.VtdCustomerId);
            return View(vtdOrders);
        }

        // GET: VtdAdmin/VtdOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdOrders = await _context.VtdOrders
                .Include(v => v.VtdCustomer)
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdOrders == null)
            {
                return NotFound();
            }

            return View(vtdOrders);
        }

        // POST: VtdAdmin/VtdOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdOrders = await _context.VtdOrders.FindAsync(id);
            if (vtdOrders != null)
            {
                _context.VtdOrders.Remove(vtdOrders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdOrdersExists(int id)
        {
            return _context.VtdOrders.Any(e => e.VtdId == id);
        }
    }
}
