using System;
using System.Collections.Generic;

namespace K23CNT3_VuTienDuc_BaiTX1.Models;

public partial class VtdCategory
{
    public int VtdCategoryId { get; set; }

    public string VtdCategoryName { get; set; } = null!;

    public virtual ICollection<VtdBook> VtdBooks { get; set; } = new List<VtdBook>();
}
