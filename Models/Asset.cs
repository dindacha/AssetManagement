using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.Models;

[Table("Asset", Schema = "Asset")]
public partial class Asset
{
    [Key]
    public int Id { get; set; }

    public byte ZRef_AssetTypeId { get; set; }
    public byte ZRef_AssetClusterId { get; set; }
    public byte ZRef_AssetCategoryId { get; set; }

    // master lokasi yg baru: pakai kode desa
    public string Reg_VillageCode { get; set; } = null!;

    public string CostCenter { get; set; } = null!;
    public string? Description { get; set; }

    public byte ZRef_OwnershipTypeId { get; set; }
    public byte ZRef_AssetAvailabilityId { get; set; }
    public string? SAP_Number { get; set; }

    // ===== Navigations =====
    public virtual ZRef_AssetAvailability ZRef_AssetAvailability { get; set; } = null!;
    public virtual ZRef_AssetCategory     ZRef_AssetCategory     { get; set; } = null!;
    public virtual ZRef_AssetCluster      ZRef_AssetCluster      { get; set; } = null!;
    public virtual ZRef_AssetType         ZRef_AssetType         { get; set; } = null!;
    public virtual ZRef_OwnershipType     ZRef_OwnershipType     { get; set; } = null!;
    public virtual Reg_Village            Reg_VillageCodeNavigation { get; set; } = null!;
    public virtual AssetLand?             AssetLand { get; set; }
    public virtual ICollection<AssetLandDoc> AssetLandDocs { get; set; } = new List<AssetLandDoc>();
}
