using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K23CNT3_VuTienDuc_2310900023.Models;
using X.PagedList;
using K23CNT3_VuTienDuc_2310900023.Utilities;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{

    public class VtdAccountsController : VtdBaseController
    {
        private readonly VtdAppDbContext _context;

        public VtdAccountsController(VtdAppDbContext context)
        {
            _context = context;
        }

        // GET: VtdAdmin/VtdAccounts
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5; // Số lượng bản ghi trên mỗi trang
            var query = _context.VtdAccounts.AsQueryable();

            // Nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.VtdName.Contains(name));
            }

            var pagedList = await query.OrderBy(c => c.VtdId).ToPagedListAsync(page, limit);
            ViewBag.keyword = name;

            return View(pagedList);
        }

        // GET: VtdAdmin/VtdAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdAccount = await _context.VtdAccounts
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdAccount == null)
            {
                return NotFound();
            }

            return View(vtdAccount);
        }

        // GET: VtdAdmin/VtdAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAdmin/VtdAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create([Bind("VtdId,VtdName,VtdEmail,VtdAvatar,VtdPassword")] VtdAccount vtdAccount)
        {
            ModelState.Remove("VtdAvatar");
            if (ModelState.IsValid)
            {
                // 1. MÃ HÓA MẬT KHẨU TRƯỚC KHI THÊM MỚI
                vtdAccount.VtdPassword = VtdSecurity.ComputeMD5Hash(vtdAccount.VtdPassword);

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\accounts");

                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        vtdAccount.VtdAvatar = "/images/accounts/" + fileName;
                    }
                }

                _context.Add(vtdAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vtdAccount);
        }

        // GET: VtdAdmin/VtdAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdAccount = await _context.VtdAccounts.FindAsync(id);
            if (vtdAccount == null)
            {
                return NotFound();
            }
            return View(vtdAccount);
        }

        // POST: VtdAdmin/VtdAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: VtdAdmin/VtdAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>  Edit(int id, [Bind("VtdId,VtdName,VtdEmail,VtdAvatar,VtdPassword")] VtdAccount vtdAccount)
        {
            if (id != vtdAccount.VtdId) return NotFound();

            ModelState.Remove("VtdAvatar");
            if (ModelState.IsValid)
            {
                try
                {
                    // 2. MÃ HÓA MẬT KHẨU KHI CẬP NHẬT
                    // Lưu ý: Đảm bảo form Edit truyền lên mật khẩu (chưa mã hóa) mới
                    vtdAccount.VtdPassword = VtdSecurity.ComputeMD5Hash(vtdAccount.VtdPassword);

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = file.FileName;
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\accounts");

                        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            vtdAccount.VtdAvatar = "/images/accounts/" + fileName;
                        }
                    }
                    _context.Update(vtdAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdAccountExists(vtdAccount.VtdId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vtdAccount);
        }

        // GET: VtdAdmin/VtdAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdAccount = await _context.VtdAccounts
                .FirstOrDefaultAsync(m => m.VtdId == id);
            if (vtdAccount == null)
            {
                return NotFound();
            }

            return View(vtdAccount);
        }

        // POST: VtdAdmin/VtdAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vtdAccount = await _context.VtdAccounts.FindAsync(id);
            if (vtdAccount != null)
            {
                _context.VtdAccounts.Remove(vtdAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdAccountExists(int id)
        {
            return _context.VtdAccounts.Any(e => e.VtdId == id);
        }
    }
}
