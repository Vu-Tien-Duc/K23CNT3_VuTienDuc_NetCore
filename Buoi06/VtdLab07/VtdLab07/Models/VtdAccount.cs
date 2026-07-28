using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdAccount")]
public partial class VtdAccount
{
    [Key]
    [StringLength(36)]
    [Unicode(false)]
    public string VtdAccountId { get; set; } = null!;

    [StringLength(64)]
    [Unicode(false)]
    public string VtdUsername { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string VtdPassword { get; set; } = null!;

    [StringLength(100)]
    public string? VtdFullName { get; set; }

    [StringLength(512)]
    public string? VtdPicture { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? VtdEmail { get; set; }

    [StringLength(512)]
    public string? VtdAddress { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? VtdPhone { get; set; }

    public bool? VtdIsAdmin { get; set; }

    public bool? VtdActive { get; set; }

    [InverseProperty("VtdAccount")]
    public virtual ICollection<VtdOrderBook> VtdOrderBooks { get; set; } = new List<VtdOrderBook>();
}
