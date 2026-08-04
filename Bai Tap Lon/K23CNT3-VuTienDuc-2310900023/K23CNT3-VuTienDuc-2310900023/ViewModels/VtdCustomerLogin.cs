using System.ComponentModel.DataAnnotations;

namespace K23CNT3_VuTienDuc_2310900023.ViewModels
{
    // ViewModel cho form Đăng nhập Khách hàng
    public class VtdCustomerLogin
    {
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string VtdEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string VtdPassword { get; set; }
    }

    // ViewModel cho form Đăng ký Khách hàng
    public class VtdCustomerRegister
    {
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string VtdFullName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string VtdPhone { get; set; }

        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string VtdEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string VtdPassword { get; set; }
    }
}