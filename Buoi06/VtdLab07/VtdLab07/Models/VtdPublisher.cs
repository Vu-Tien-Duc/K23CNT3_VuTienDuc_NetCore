using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdPublisher")]
public partial class VtdPublisher
{
    [Key]
    public int VtdPublisherId { get; set; }

    [StringLength(200)]
    public string VtdPublisherName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? VtdPhone { get; set; }

    [StringLength(200)]
    public string? VtdAddress { get; set; }

    [InverseProperty("VtdPublisher")]
    public virtual ICollection<VtdBook> VtdBooks { get; set; } = new List<VtdBook>();
}
