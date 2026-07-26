using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VtdLab04.Models;

namespace VtdLab04.Controllers
{
    public class VtdPeopleController : Controller
    {
        // GET: VtdPeopleController
        public ActionResult VtdIndex()
        {
            var _people = VtdDataLocal.GetVtdPeople();
            return View(_people);
        }

        // GET: VtdPeopleController/Details/5
        public ActionResult VtdDetails(int id)
        {
            var peoples = VtdDataLocal.GetPeopleById(id);
            return View(peoples);
        
        }

        // GET: VtdPeopleController/Create
        public ActionResult VtdCreate()
        {
            VtdPeople people = new VtdPeople();
            return View(people);
        }

        // POST: VtdPeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VtdCreate(VtdPeople model)
        {
            try
            {
                // upload file vào thư mục wwwroot/images/avatar
                var files = HttpContext.Request.Form.Files;
                //using System.Linq;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];

                    var FileName = file.FileName;
                    // Nhớ tạo thư mục avatar trong thư mục wwwroot/images

                    //using System.IO;
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\avatar", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        model.Avatar = "/images/avatar/" + FileName; // gán tên ảnh cho thuộc tính Avatar
                    }
                }
                // thêm peoples vào danh sách DataLocal
                VtdDataLocal._peoples.Add(model);
                return RedirectToAction(nameof(VtdIndex));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(model);
            }
        }

        // GET: VtdPeopleController/Edit/5
        public ActionResult VtdEdit(int id)
        {
            return View();
        }

        // POST: VtdPeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VtdEdit(int id, VtdPeople model)
        {
            try
            {
                // upload file vào thư mục wwwroot/images/avatar
                var files = HttpContext.Request.Form.Files;
                //using System.Linq;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];

                    var FileName = file.FileName;
                    // Nhớ tạo thư mục avatar trong thư mục wwwroot/images

                    //using System.IO;

                    var path = Path.Combine(Directory.GetCurrentDirectory(),

                    "wwwroot\\images\\avatar", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);

                        model.Avatar = "/images/avatar/" + FileName; // gán tên ảnh cho thuộc tính Avatar
}
                }
                // cập nhật model vào danh sách DataLocal
                for (int i = 0; i < VtdDataLocal._peoples.Count; i++)
                {
                    if (VtdDataLocal._peoples[i].Id == id)

                    {

                        VtdDataLocal._peoples[i] = model;

                        break;
                    }
                }
                return RedirectToAction(nameof(VtdIndex));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: VtdPeopleController/Delete/5
        public ActionResult VtdDelete(int id)
        {
            var peoples = VtdDataLocal.GetPeopleById(id);
            return View(peoples);
        }

        // POST: VtdPeopleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VtdDelete(int id, VtdPeople model)
        {
            try
            {
                for (int i = 0; i < VtdDataLocal._peoples.Count; i++)
                {
                    if (VtdDataLocal._peoples[i].Id == id)

                    {

                        VtdDataLocal._peoples.RemoveAt(i);

                        break;
                    }
                }
                return RedirectToAction(nameof(VtdIndex));
            }
            catch
            {
                return View();
            }
        }
    }
}
