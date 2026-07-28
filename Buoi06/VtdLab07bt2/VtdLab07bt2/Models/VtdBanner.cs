using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07bt2.Models;

[Table("VtdBanner")]
public partial class VtdBanner
{
    [Key]
    public int VtdId { get; set; }

    [StringLength(100)]
    public string VtdName { get; set; } = null!;

    public byte VtdStatus { get; set; }

    public int VtdPriority { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? VtdImage { get; set; }

    [StringLength(350)]
    public string? VtdDescription { get; set; }
}
