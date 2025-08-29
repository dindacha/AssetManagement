using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("Reg_District", Schema = "Region")]
public partial class Reg_District
{
    [Key]
    [StringLength(8)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string Reg_RegencyCode { get; set; } = null!;

    [ForeignKey("Reg_RegencyCode")]
    [InverseProperty("Reg_Districts")]
    public virtual Reg_Regency Reg_RegencyCodeNavigation { get; set; } = null!;
}
