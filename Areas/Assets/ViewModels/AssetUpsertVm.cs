namespace AssetManagement.Areas.Assets.ViewModels;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AssetUpsertVm : IValidatableObject
{
    // ==== Master (Asset.Asset) ====
    [Required, Display(Name="Jenis Aset")]
    public byte ZRef_AssetTypeId { get; set; }

    [Required, Display(Name = "Cluster")]
    public byte ZRef_AssetClusterId { get; set; }

    [Required, Display(Name = "Kategori")]
    public byte ZRef_AssetCategoryId { get; set; }

    [Required, Display(Name = "Kode Desa")]
    public string? Reg_VillageCode { get; set; }

    [Required, StringLength(50), Display(Name = "Cost Center")]
    public string? CostCenter { get; set; }

    [StringLength(500), Display(Name = "Deskripsi")]
    public string? Description { get; set; }

    [Display(Name = "Jenis Kepemilikan")]
    public byte? ZRef_OwnershipTypeId { get; set; }

    [Display(Name = "Ketersediaan Aset")]
    public byte? ZRef_AssetAvailabilityId { get; set; }

    [Display(Name = "No SAP")]
    public string? SAP_Number { get; set; }

    // ==== Child sub-VM ====
    public AssetLandFieldsVm Land { get; set; } = new();
    public AssetBuildingFieldsVm Building { get; set; } = new();

    // ==== Dropdowns ====
    public IEnumerable<SelectListItem> Types { get; set; } = [];
    public IEnumerable<SelectListItem> Clusters { get; set; } = [];
    public IEnumerable<SelectListItem> Categories { get; set; } = [];
    public IEnumerable<SelectListItem> OwnershipTypes { get; set; } = [];
    public IEnumerable<SelectListItem> Availabilities { get; set; } = [];
    public IEnumerable<SelectListItem> RightsTypes { get; set; } = [];
    public IEnumerable<SelectListItem> Villages { get; set; } = [];

    // Validasi kondisional
    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (ZRef_AssetTypeId == 1 && Land.SurfaceArea == null)
            yield return new ValidationResult("Luas tanah wajib diisi", new[] { "Land.SurfaceArea" });
    }
}

public class AssetLandFieldsVm
{
    [Display(Name = "Luas Tanah (m²)"), Required]
    [Range(0, 2147483647, ErrorMessage = "Luas Tanah maksimal 2.147.483.647 m²")]
    public int? SurfaceArea { get; set; }

    [Display(Name = "Jenis Alas Hak")]
    public byte? ZRef_RightsTypeId { get; set; }

    [Display(Name = "No Hak")]
    public string? RightsNumber { get; set; }

    [Display(Name = "NOP")]
    public string? TaxObjectNumber { get; set; }

    [Display(Name = "NJOP")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false, NullDisplayText = "-")]
    public decimal? Value_NJOP { get; set; }

    [Display(Name = "Nilai SAP")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false, NullDisplayText = "-")]
    public decimal? Value_SAP { get; set; }

    [Display(Name = "Nilai Appraisal KJPP")]
    public string? Value_AppraisalKJPP { get; set; }
}

public class AssetBuildingFieldsVm
{
    // belum dipakai (tabel belum ada)
    [Display(Name="Luas Bangunan (m²)")] public decimal? BuildingAreaM2 { get; set; }
    [Display(Name="Jumlah Lantai")] public int? Floors { get; set; }
    [Display(Name="Tahun Bangun")] public int? YearBuilt { get; set; }
    [Display(Name="No IMB/PBG")] public string? PermitNo { get; set; }
    [Display(Name="No SLF")] public string? SLFNo { get; set; }
    [Display(Name="Nilai SAP")] public decimal? Value_SAP { get; set; }
}
