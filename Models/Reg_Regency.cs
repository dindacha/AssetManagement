using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("Reg_Regency", Schema = "Region")]
public partial class Reg_Regency
{
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string Reg_ProvinceCode { get; set; } = null!;

    [InverseProperty("Reg_RegencyCodeNavigation")]
    public virtual ICollection<Reg_District> Reg_Districts { get; set; } = new List<Reg_District>();

    [ForeignKey("Reg_ProvinceCode")]
    [InverseProperty("Reg_Regencies")]
    public virtual Reg_Province Reg_ProvinceCodeNavigation { get; set; } = null!;
}
