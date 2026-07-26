using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VtdLab04_bt.Models;

namespace VtdLab04_bt.Controllers
{
    public class VtdProductController : Controller
    {
   
        public IActionResult VtdIndex()
        {
            ViewBag.Categories = VtdDataLocal.Categories;
            return View(VtdDataLocal.Products);
        }

   
        public IActionResult VtdDetails(int id)
        {
            var product = VtdDataLocal.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Category = VtdDataLocal.Categories
                .FirstOrDefault(x => x.Id == product.CategoryId);

            return View(product);
        }

        //====================================
        // THÊM MỚI - GET
        //====================================
        public IActionResult VtdCreate()
        {
            ViewBag.CategoryId = new SelectList(
                VtdDataLocal.Categories,
                "Id",
                "Name"
            );

            return View();
        }

        //====================================
        // THÊM MỚI - POST
        //====================================


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VtdCreate(VtdProduct product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Tạo Id
                product.Id = VtdDataLocal.Products.Any()
                    ? VtdDataLocal.Products.Max(x => x.Id) + 1
                    : 1;

                // Ngày tạo
                product.CreatedDate = DateTime.Now;

                // Upload ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    string folder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "product");

                    // Nếu chưa có thư mục thì tạo
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Path.GetFileName(imageFile.FileName);

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    product.Image = "/images/product/" + fileName;
                }
                else
                {
                    product.Image = "";
                }

                // Thêm vào danh sách
                VtdDataLocal.Products.Add(product);

                return RedirectToAction(nameof(VtdIndex));
            }

            ViewBag.CategoryId = new SelectList(
                VtdDataLocal.Categories,
                "Id",
                "Name",
                product.CategoryId);

            return View(product);
        }

        //====================================
        // SỬA - GET
        //====================================
        public IActionResult VtdEdit(int id)
        {
            var product = VtdDataLocal.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(
                VtdDataLocal.Categories,
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }

        //====================================
        // SỬA - POST
        //====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VtdEdit(int id, VtdProduct model, IFormFile imageFile)
        {
            var product = VtdDataLocal.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.SalePrice = model.SalePrice;
                product.Status = model.Status;
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;

                // Upload ảnh mới
                if (imageFile != null && imageFile.Length > 0)
                {
                    string folder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "images",
                        "product");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Path.GetFileName(imageFile.FileName);

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    product.Image = "/images/product/" + fileName;
                }

                return RedirectToAction(nameof(VtdIndex));
            }

            ViewBag.CategoryId = new SelectList(
                VtdDataLocal.Categories,
                "Id",
                "Name",
                model.CategoryId);

            return View(model);
        }

        //====================================
        // XÓA - GET
        //====================================
        public IActionResult VtdDelete(int id)
        {
            var product = VtdDataLocal.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Category = VtdDataLocal.Categories
                .FirstOrDefault(x => x.Id == product.CategoryId);

            return View(product);
        }

        //====================================
        // XÓA - POST
        //====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VtdDelete(int id, IFormCollection collection)
        {
            var product = VtdDataLocal.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                VtdDataLocal.Products.Remove(product);
            }

            return RedirectToAction(nameof(VtdIndex));
        }
    }
}