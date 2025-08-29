using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("Reg_Province", Schema = "Region")]
public partial class Reg_Province
{
    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public byte Reg_RegionId { get; set; }

    [InverseProperty("Reg_ProvinceCodeNavigation")]
    public virtual ICollection<Reg_Regency> Reg_Regencies { get; set; } = new List<Reg_Regency>();
}
