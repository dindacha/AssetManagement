using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class RegDistrict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegRegencyId { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual RegRegency RegRegency { get; set; } = null!;
}
