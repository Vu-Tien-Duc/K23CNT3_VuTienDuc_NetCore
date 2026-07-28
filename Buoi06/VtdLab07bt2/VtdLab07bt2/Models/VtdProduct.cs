using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace VtdLab07bt2.Models;

[Table("VtdProduct")]
public partial class VtdProduct
{
    [Key]
    public int VtdId { get; set; }

    [StringLength(100)]
    public string VtdName { get; set; } = null!;

    public double VtdPrice { get; set; }

    public double? VtdSalePrice { get; set; }

    public byte VtdStatus { get; set; }

    public int VtdCategoryId { get; set; }

    public DateOnly VtdCreatedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? VtdImage { get; set; }

    [StringLength(350)]
    public string? VtdDescription { get; set; }

    [ForeignKey("VtdCategoryId")]
    [InverseProperty("VtdProducts")]
    [ValidateNever]
    public virtual VtdCategory VtdCategory { get; set; } = null!;
}
