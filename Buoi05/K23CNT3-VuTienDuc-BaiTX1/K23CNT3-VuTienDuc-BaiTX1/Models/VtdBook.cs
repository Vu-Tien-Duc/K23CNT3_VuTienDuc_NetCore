using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdBook
{
    public string VtdBookId { get; set; } = null!;

    public string VtdTitle { get; set; } = null!;

    public string? VtdAuthor { get; set; }

    public int? VtdRelease { get; set; }

    public double? VtdPrice { get; set; }

    public string? VtdDescription { get; set; }

    public string? VtdPicture { get; set; }

    public int? VtdPublisherId { get; set; }

    public int? VtdCategoryId { get; set; }

    public virtual VtdCategory? VtdCategory { get; set; }

    public virtual ICollection<VtdOrderDetail> VtdOrderDetails { get; set; } = new List<VtdOrderDetail>();

    public virtual VtdPublisher? VtdPublisher { get; set; }
}
