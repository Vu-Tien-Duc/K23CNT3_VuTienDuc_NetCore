using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdBook")]
public partial class VtdBook
{
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string VtdBookId { get; set; } = null!;

    [StringLength(200)]
    public string VtdTitle { get; set; } = null!;

    [StringLength(100)]
    public string? VtdAuthor { get; set; }

    public int? VtdRelease { get; set; }

    public double? VtdPrice { get; set; }

    [Column(TypeName = "ntext")]
    public string? VtdDescription { get; set; }

    [StringLength(100)]
    public string? VtdPicture { get; set; }

    public int? VtdPublisherId { get; set; }

    public int? VtdCategoryId { get; set; }

    [ForeignKey("VtdCategoryId")]
    [InverseProperty("VtdBooks")]
    public virtual VtdCategory? VtdCategory { get; set; }

    [InverseProperty("VtdBook")]
    public virtual ICollection<VtdOrderDetail> VtdOrderDetails { get; set; } = new List<VtdOrderDetail>();

    [ForeignKey("VtdPublisherId")]
    [InverseProperty("VtdBooks")]
    public virtual VtdPublisher? VtdPublisher { get; set; }
}
