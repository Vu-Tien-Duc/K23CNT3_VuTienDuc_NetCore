using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdPublisher
{
    public int VtdPublisherId { get; set; }

    public string VtdPublisherName { get; set; } = null!;

    public string? VtdPhone { get; set; }

    public string? VtdAddress { get; set; }

    public virtual ICollection<VtdBook> VtdBooks { get; set; } = new List<VtdBook>();
}
