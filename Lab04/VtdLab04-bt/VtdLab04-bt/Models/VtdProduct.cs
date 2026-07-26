using System.ComponentModel.DataAnnotations;

namespace VtdLab04_bt.Models
{
    public class VtdProduct
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Giá")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal SalePrice { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;
    }
}
