using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdBanner")]
    public class VtdBanner
    {
        [Key]
        [Display(Name = "Mã Banner")]
        public int VtdId { get; set; }

        [Display(Name = "Tên Banner")]
        [Required(ErrorMessage = "Tên banner không được để trống")]
        [StringLength(100, ErrorMessage = "Tên banner không được vượt quá 100 ký tự")]
        public string VtdName { get; set; }

        [Display(Name = "Trạng thái")]
        public byte VtdStatus { get; set; }

        [Display(Name = "Độ ưu tiên")]
        public int VtdPrioty { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime VtdCreatedDate { get; set; }

        [Display(Name = "Hình ảnh")]
        public string VtdImage { get; set; }

        [Display(Name = "Mô tả")]
        public string VtdDescription { get; set; }
    }
}