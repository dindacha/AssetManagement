using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class RegProvince
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<RegRegency> RegRegencies { get; set; } = new List<RegRegency>();
}
