using System.ComponentModel.DataAnnotations;

namespace K23CNT3_VuTienDuc_2310900023.Areas.VtdAdmin.ViewModels
{
    public class VtdLogin
    {
        [Required(ErrorMessage = "Địa chỉ email không để trống")]
        public string VtdEmail { get; set; }
        [Required(ErrorMessage = "Mậ khẩu không để trống")]
        public string VtdPassword { get; set; }
        public bool Remember { get; set; }
    }
}
