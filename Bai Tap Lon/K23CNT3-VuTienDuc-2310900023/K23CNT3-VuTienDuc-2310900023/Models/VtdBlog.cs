using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdBlog")]
    public class VtdBlog
    {
        [Key]
        [Display(Name = "Mã Bài Viết")]
        public int VtdId { get; set; }

        [Display(Name = "Tiêu đề bài viết")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự")]
        public string VtdName { get; set; }

        [Display(Name = "Trạng thái")]
        public byte VtdStatus { get; set; }

        [Display(Name = "Lượt xem")]
        public int VtdViewCount { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime VtdCreatedDate { get; set; }

        [Display(Name = "Hình ảnh")]
        public string VtdImage { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        [StringLength(1500, ErrorMessage = "Nội dung không được vượt quá 1500 ký tự")]
        [DataType(DataType.Text)]
        public string VtdDescription { get; set; }
    }
}