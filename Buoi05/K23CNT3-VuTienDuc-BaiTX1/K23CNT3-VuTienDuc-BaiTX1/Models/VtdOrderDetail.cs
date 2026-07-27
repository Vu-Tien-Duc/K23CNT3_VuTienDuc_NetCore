using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdOrderDetail
{
    public int VtdOrderDetailId { get; set; }

    public string? VtdOrderId { get; set; }

    public string? VtdBookId { get; set; }

    public int? VtdQuantity { get; set; }

    public int? VtdPrice { get; set; }

    public int? VtdTotalMoney { get; set; }

    public virtual VtdBook? VtdBook { get; set; }

    public virtual VtdOrderBook? VtdOrder { get; set; }
}
