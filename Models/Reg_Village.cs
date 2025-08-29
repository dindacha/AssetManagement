using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("Reg_Village", Schema = "Region")]
public partial class Reg_Village
{
    [Key]
    [StringLength(13)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string Reg_DistrictCode { get; set; } = null!;

    [InverseProperty("Reg_VillageCodeNavigation")]
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
