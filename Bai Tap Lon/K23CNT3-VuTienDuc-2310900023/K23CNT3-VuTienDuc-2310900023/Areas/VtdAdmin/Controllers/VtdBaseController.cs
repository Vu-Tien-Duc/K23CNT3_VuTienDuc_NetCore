using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.Controllers
{
    [Area("VtdAdmin")]
    [Authorize] // 👈 Chỉ cần 1 từ khóa này, hệ thống sẽ tự động kiểm tra Cookie đăng nhập
    public class VtdBaseController : Controller
    {
      
    }
}