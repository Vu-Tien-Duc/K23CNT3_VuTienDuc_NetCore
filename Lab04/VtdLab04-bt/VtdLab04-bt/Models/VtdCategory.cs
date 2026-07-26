using System.ComponentModel.DataAnnotations;

namespace VtdLab04_bt.Models
{
    public class VtdCategory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}