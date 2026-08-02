using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace VtdBuoi07Lab08.Models
{
    [Table("VtdCategory")]
    public class VtdCategory
    {
        [Key]
        public int VtdId { get; set; }

        [Required]
        [StringLength(100)]
        public string VtdName { get; set; }

        public byte VtdStatus { get; set; }
        public DateTime VtdCreatedDate { get; set; }
        public string VtdImage { get; set; }
        public string VtdDescription { get; set; }

        // danh sách sản phẩm theo danh mục
        public ICollection<VtdProduct> VtdProducts { get; set; }
    }
}