using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdAccount
{
    public string VtdAccountId { get; set; } = null!;

    public string VtdUsername { get; set; } = null!;

    public string VtdPassword { get; set; } = null!;

    public string? VtdFullName { get; set; }

    public string? VtdPicture { get; set; }

    public string? VtdEmail { get; set; }

    public string? VtdAddress { get; set; }

    public string? VtdPhone { get; set; }

    public bool? VtdIsAdmin { get; set; }

    public bool? VtdActive { get; set; }

    public virtual ICollection<VtdOrderBook> VtdOrderBooks { get; set; } = new List<VtdOrderBook>();
}
