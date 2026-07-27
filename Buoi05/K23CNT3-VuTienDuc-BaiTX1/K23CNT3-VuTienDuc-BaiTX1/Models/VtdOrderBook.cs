using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdOrderBook
{
    public string VtdOrderId { get; set; } = null!;

    public DateTime? VtdOrderDate { get; set; }

    public string? VtdAccountId { get; set; }

    public string? VtdReceiveAddress { get; set; }

    public string? VtdReceivePhone { get; set; }

    public DateTime? VtdOrderReceive { get; set; }

    public string? VtdNote { get; set; }

    public string? VtdStatus { get; set; }

    public virtual VtdAccount? VtdAccount { get; set; }

    public virtual ICollection<VtdOrderDetail> VtdOrderDetails { get; set; } = new List<VtdOrderDetail>();
}
