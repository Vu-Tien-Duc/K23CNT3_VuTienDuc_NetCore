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
    public class VtdCustomersController : Controller
    {
        private readonly VtdAppDbContext _context;

        public VtdCustomersController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdCustomers
        // GET: VtdAdmin/VtdCustomers
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số bản ghi trên 1 trang
            var query = _context.VtdCustomers.AsQueryable();

            // Tìm kiếm theo tên khách hàng (VtdFullName)
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdFullName.Contains(name));
            }

            // Sắp xếp theo ID và phân trang
            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);

            // Lưu lại từ khóa tìm kiếm để hiển thị trên ô input
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCustomer = await _context.VtdCustomers
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdCustomer == null)
            {
                return NotFound();
            }

            return View(vtdCustomer);
        }

        // GET: VtdAdmin/VtdCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAdmin/VtdCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VtdId,VtdFullName,VtdEmail,VtdPhone,VtdAddress,VtdAvatar,VtdBirthday,VtdGender,VtdPassword,VtdFacebook")] VtdCustomer vtdCustomer)
        {
            ModelState.Remove("VtdAvatar");
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\customers");

                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdCustomer.VtdAvatar = "/images/customers/" + fileName;
                    }
                }
                _context.Add(vtdCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdCustomer);
        }

        // GET: VtdAdmin/VtdCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCustomer = await _context.VtdCustomers.FindAsync(id);
            if (vtdCustomer == null)
            {
                return NotFound();
            }
            return View(vtdCustomer);
        }

        // POST: VtdAdmin/VtdCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VtdId,VtdFullName,VtdEmail,VtdPhone,VtdAddress,VtdAvatar,VtdBirthday,VtdGender,VtdPassword,VtdFacebook")] VtdCustomer vtdCustomer)
        {
            if (id != vtdCustomer.VtdId) return NotFound();

            ModelState.Remove("VtdAvatar");
            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = file.FileName;
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\customers");

                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdCustomer.VtdAvatar = "/images/customers/" + fileName;
                        }
                    }
                    _context.Update(vtdCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdCustomerExists(vtdCustomer.VtdId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vtdCustomer);
        }

        // GET: VtdAdmin/VtdCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdCustomer = await _context.VtdCustomers
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdCustomer == null)
            {
                return NotFound();
            }

            return View(vtdCustomer);
        }

        // POST: VtdAdmin/VtdCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdCustomer = await _context.VtdCustomers.FindAsync(id);
            if (vtdCustomer != null)
            {
                _context.VtdCustomers.Remove(vtdCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdCustomerExists(int id)
        {
            return _context.VtdCustomers.Any(e => e.VtdId == id);
        }
    }
}
