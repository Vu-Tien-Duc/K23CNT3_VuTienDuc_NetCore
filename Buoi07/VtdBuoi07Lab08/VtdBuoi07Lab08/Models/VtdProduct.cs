using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace VtdBuoi07Lab08.Models
{
    [Table("VtdProduct")]
    public class VtdProduct
    {
        [Key]
        public int VtdId { get; set; }

        [Required]
        [StringLength(150)]
        public string VtdName { get; set; }

        [DataType(DataType.Upload)]
        public string VtdImage { get; set; }

        [Required]
        public float VtdPrice { get; set; }

        public float VtdSalePrice { get; set; }
        public byte VtdStatus { get; set; }

        [DataType(DataType.Text)]
        [StringLength(1000)]
        public string VtdDescription { get; set; }

        [Required]
        public int VtdCategoryId { get; set; }

        public DateTime VtdCreatedDate { get; set; }

        // khóa ngoại tới bảng VtdCategory
        public VtdCategory VtdCategory { get; set; }
    }
}