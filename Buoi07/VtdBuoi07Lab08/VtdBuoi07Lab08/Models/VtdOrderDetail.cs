using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VtdBuoi07Lab08.Models
{
    [Table("VtdOrderDetail")]
    public class VtdOrderDetail
    {
        [Key]
        public int VtdId { get; set; }

        [Required]
        public int VtdOrderId { get; set; }

        [Required]
        public int VtdProductId { get; set; }

        [Required]
        public int VtdQuantity { get; set; }

        [Required]
        public float VtdPrice { get; set; }

        // Khóa ngoại tới VtdOrders
        public VtdOrders VtdOrder { get; set; }

        // Khóa ngoại tới VtdProduct
        public VtdProduct VtdProduct { get; set; }
    }
}