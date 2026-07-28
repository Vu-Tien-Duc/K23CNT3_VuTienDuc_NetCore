using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdOrderDetail")]
public partial class VtdOrderDetail
{
    [Key]
    public int VtdOrderDetailId { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? VtdOrderId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? VtdBookId { get; set; }

    public int? VtdQuantity { get; set; }

    public int? VtdPrice { get; set; }

    public int? VtdTotalMoney { get; set; }

    [ForeignKey("VtdBookId")]
    [InverseProperty("VtdOrderDetails")]
    public virtual VtdBook? VtdBook { get; set; }

    [ForeignKey("VtdOrderId")]
    [InverseProperty("VtdOrderDetails")]
    public virtual VtdOrderBook? VtdOrder { get; set; }
}
