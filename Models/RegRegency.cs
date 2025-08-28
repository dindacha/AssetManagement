using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class RegRegency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegProvinceId { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<RegDistrict> RegDistricts { get; set; } = new List<RegDistrict>();

    public virtual RegProvince RegProvince { get; set; } = null!;
}
