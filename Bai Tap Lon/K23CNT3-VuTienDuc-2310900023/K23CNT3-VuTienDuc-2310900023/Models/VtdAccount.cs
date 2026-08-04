using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdAccount")]
    public class VtdAccount
    {
        [Key]
        public int VtdId { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ không được để trống")]
        [MinLength(6, ErrorMessage = "Họ tên ít nhất là 6 ký tự")]
        [MaxLength(20, ErrorMessage = "Họ tên tối đa 20 ký tự")]
        public string VtdName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
  
        public string VtdEmail { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string VtdAvatar { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string VtdPassword { get; set; }
    }
}