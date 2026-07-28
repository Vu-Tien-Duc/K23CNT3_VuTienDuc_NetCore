using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdCategory")]
public partial class VtdCategory
{
    [Key]
    public int VtdCategoryId { get; set; }

    [StringLength(100)]
    public string VtdCategoryName { get; set; } = null!;

    [InverseProperty("VtdCategory")]
    public virtual ICollection<VtdBook> VtdBooks { get; set; } = new List<VtdBook>();
}
