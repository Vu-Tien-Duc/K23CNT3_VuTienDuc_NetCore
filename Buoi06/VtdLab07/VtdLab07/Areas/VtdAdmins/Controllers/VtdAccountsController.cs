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
    public class VtdAccountsController : Controller
    {
        private readonly VtdBookStoreDbContext _context;

        public VtdAccountsController(VtdBookStoreDbContext context)
        {
            _context = context;
        }

        // GET: VtdAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.VtdAccounts.ToListAsync());
        }

        // GET: VtdAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdAccount = await _context.VtdAccounts
                .FirstOrDefaultAsync(m => m.VtdAccountId == id);
            if (vtdAccount == null)
            {
                return NotFound();
            }

            return View(vtdAccount);
        }

        // GET: VtdAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VtdAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
      [Bind("VtdAccountId,VtdUsername,VtdPassword,VtdFullName,VtdEmail,VtdAddress,VtdPhone,VtdIsAdmin,VtdActive")] VtdAccount vtdAccount,
      IFormFile? anhDaiDien)
        {
            if (ModelState.IsValid)
            {
                if (anhDaiDien != null && anhDaiDien.Length > 0)
                {
                    string fileName = Path.GetFileName(anhDaiDien.FileName);

                    string folder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "account");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await anhDaiDien.CopyToAsync(stream);
                    }

                    vtdAccount.VtdPicture = "/images/account/" + fileName;
                }

                _context.Add(vtdAccount);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vtdAccount);
        }


        // GET: VtdAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
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

        // POST: VtdAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    string id,
    [Bind("VtdAccountId,VtdUsername,VtdPassword,VtdFullName,VtdPicture,VtdEmail,VtdAddress,VtdPhone,VtdIsAdmin,VtdActive")] VtdAccount vtdAccount,
    IFormFile? anhDaiDien)
        {
            if (id != vtdAccount.VtdAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (anhDaiDien != null && anhDaiDien.Length > 0)
                    {
                        string fileName = Path.GetFileName(anhDaiDien.FileName);

                        string folder = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "images",
                            "account");

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string filePath = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await anhDaiDien.CopyToAsync(stream);
                        }

                        vtdAccount.VtdPicture = "/images/account/" + fileName;
                    }
                    else
                    {
                        vtdAccount.VtdPicture = _context.VtdAccounts
                            .AsNoTracking()
                            .First(x => x.VtdAccountId == id)
                            .VtdPicture;
                    }

                    _context.Update(vtdAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VtdAccountExists(vtdAccount.VtdAccountId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vtdAccount);
        }

        // GET: VtdAccounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtdAccount = await _context.VtdAccounts
                .FirstOrDefaultAsync(m => m.VtdAccountId == id);
            if (vtdAccount == null)
            {
                return NotFound();
            }

            return View(vtdAccount);
        }

        // POST: VtdAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vtdAccount = await _context.VtdAccounts.FindAsync(id);
            if (vtdAccount != null)
            {
                _context.VtdAccounts.Remove(vtdAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VtdAccountExists(string id)
        {
            return _context.VtdAccounts.Any(e => e.VtdAccountId == id);
        }
    }
}
