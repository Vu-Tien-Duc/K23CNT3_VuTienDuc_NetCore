using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07bt2.Models;

[Table("VtdCategory")]
public partial class VtdCategory
{
    [Key]
    public int VtdId { get; set; }

    [StringLength(100)]
    public string VtdName { get; set; } = null!;

    public byte VtdStatus { get; set; }

    public DateOnly VtdCreatedDate { get; set; }

    [InverseProperty("VtdCategory")]
    public virtual ICollection<VtdProduct> VtdProducts { get; set; } = new List<VtdProduct>();
}
