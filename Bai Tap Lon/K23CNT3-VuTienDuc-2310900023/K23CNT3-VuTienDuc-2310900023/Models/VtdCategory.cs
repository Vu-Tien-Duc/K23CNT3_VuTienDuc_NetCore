using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using K23CNT3_VuTienDuc_2310900023.Models;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdCategory")]
    public class VtdCategory
    {
        [Key]
        [Display(Name = "Mã Danh Mục")]
        public int VtdId { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string VtdName { get; set; }

        [Display(Name = "Trạng thái")]
        public byte VtdStatus { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime VtdCreatedDate { get; set; }

        [Display(Name = "Hình ảnh")]
        public string VtdImage { get; set; }

        [Display(Name = "Mô tả")]
        public string VtdDescription { get; set; }

        // Danh sách sản phẩm theo danh mục
        public ICollection<VtdProduct> VtdProducts { get; set; }
    }
}