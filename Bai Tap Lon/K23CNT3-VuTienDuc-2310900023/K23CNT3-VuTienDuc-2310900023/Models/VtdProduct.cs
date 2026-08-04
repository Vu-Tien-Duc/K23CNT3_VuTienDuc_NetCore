using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using K23CNT3_VuTienDuc_2310900023.Models;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdProduct")]
    public class VtdProduct
    {
        [Key]
        [Display(Name = "Mã Sản Phẩm")]
        public int VtdId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm không được vượt quá 150 ký tự")]
        public string VtdName { get; set; }

        [Display(Name = "Hình ảnh")]
        [DataType(DataType.Upload)]
        public string VtdImage { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public float VtdPrice { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public float VtdSalePrice { get; set; }

        [Display(Name = "Trạng thái")]
        public byte VtdStatus { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự")]
        public string VtdDescription { get; set; }

        [Display(Name = "Mã danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int VtdCategoryId { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime VtdCreatedDate { get; set; }

        // Khóa ngoại tới bảng VtdCategory
        public VtdCategory VtdCategory { get; set; }
    }
}