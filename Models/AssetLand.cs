using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("AssetLand", Schema = "Asset")]
public partial class AssetLand
{
    [Key]
    public int AssetId { get; set; }

    public int SurfaceArea { get; set; }

    public byte ZRef_RightsTypeId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? RightsNumber { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? TaxObjectNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal? Value_NJOP { get; set; }

    [Column(TypeName = "money")]
    public decimal? Value_SAP { get; set; }

    [StringLength(10)]
    public string? Value_AppraisalKJPP { get; set; }

    [ForeignKey("AssetId")]
    [InverseProperty("AssetLand")]
    public virtual Asset Asset { get; set; } = null!;

    [InverseProperty(nameof(AssetLandMap.AssetLand))]
    public virtual AssetLandMap? AssetLandMap { get; set; }

}
