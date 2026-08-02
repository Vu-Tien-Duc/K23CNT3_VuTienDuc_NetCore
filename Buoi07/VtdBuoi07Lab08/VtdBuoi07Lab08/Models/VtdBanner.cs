using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace VtdBuoi07Lab08.Models
{
    [Table("VtdBanner")]
    public class VtdBanner
    {
        [Key]
        public int VtdId { get; set; }

        [Required]
        [StringLength(100)]
        public string VtdName { get; set; }

        public byte VtdStatus { get; set; }
        public int VtdPrioty { get; set; }
        public DateTime VtdCreatedDate { get; set; }
        public string VtdImage { get; set; }
        public string VtdDescription { get; set; }
    }
}