using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07bt2.Models;

[Table("VtdBlog")]
public partial class VtdBlog
{
    [Key]
    public int VtdId { get; set; }

    [StringLength(100)]
    public string VtdName { get; set; } = null!;

    public byte VtdStatus { get; set; }

    public DateOnly VtdCreatedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? VtdImage { get; set; }

    [StringLength(350)]
    public string? VtdDescription { get; set; }
}
