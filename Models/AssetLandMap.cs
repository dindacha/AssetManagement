using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.Models;

[Table("AssetLandMap", Schema = "Asset")]
public partial class AssetLandMap
{
    [Key]
    public int AssetId { get; set; }                  

    [Column(TypeName = "decimal(8, 6)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? Longitude { get; set; }

    [ForeignKey(nameof(AssetId))]
    [InverseProperty(nameof(AssetManagement.Models.AssetLand.AssetLandMap))]
    public virtual AssetLand AssetLand { get; set; } = null!;
}
