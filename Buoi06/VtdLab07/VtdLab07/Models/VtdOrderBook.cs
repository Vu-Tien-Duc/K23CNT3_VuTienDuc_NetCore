using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VtdLab07.Models;

[Table("VtdOrderBook")]
public partial class VtdOrderBook
{
    [Key]
    [StringLength(16)]
    [Unicode(false)]
    public string VtdOrderId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? VtdOrderDate { get; set; }

    [StringLength(36)]
    [Unicode(false)]
    public string? VtdAccountId { get; set; }

    [StringLength(512)]
    public string? VtdReceiveAddress { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? VtdReceivePhone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? VtdOrderReceive { get; set; }

    [StringLength(512)]
    public string? VtdNote { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? VtdStatus { get; set; }

    [ForeignKey("VtdAccountId")]
    [InverseProperty("VtdOrderBooks")]
    public virtual VtdAccount? VtdAccount { get; set; }

    [InverseProperty("VtdOrder")]
    public virtual ICollection<VtdOrderDetail> VtdOrderDetails { get; set; } = new List<VtdOrderDetail>();
}
