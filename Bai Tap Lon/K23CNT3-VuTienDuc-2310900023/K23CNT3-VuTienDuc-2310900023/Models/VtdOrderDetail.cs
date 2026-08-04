using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using K23CNT3_VuTienDuc_2310900023.Models;

namespace K23CNT3_VuTienDuc_2310900023.Models
{
    [Table("VtdOrderDetail")]
    public class VtdOrderDetail
    {
        [Key]
        [Display(Name = "Mã Chi Tiết")]
        public int VtdId { get; set; }

        [Display(Name = "Mã Đơn Hàng")]
        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        public int VtdOrderId { get; set; }

        [Display(Name = "Mã Sản Phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public int VtdProductId { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int VtdQuantity { get; set; }

        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Đơn giá không được để trống")]
        public float VtdPrice { get; set; }

        // Khóa ngoại tới VtdOrders
        public VtdOrders VtdOrder { get; set; }

        // Khóa ngoại tới VtdProduct
        public VtdProduct VtdProduct { get; set; }
    }
}