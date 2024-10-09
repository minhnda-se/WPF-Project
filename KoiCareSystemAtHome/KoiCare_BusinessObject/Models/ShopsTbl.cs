using System;
using System.Collections.Generic;

namespace KoiCare_BusinessObject.Models;

public partial class ShopsTbl
{
    public int ShopId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<ProductsTbl> ProductsTbls { get; set; } = new List<ProductsTbl>();
}
