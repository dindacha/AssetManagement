using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class ZrefRightsType
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
