using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AssetManagement.Models;

public partial class Asset
{
    public int Id { get; set; }

    [Display(Name = "Jenis Aset")]
    public byte ZrefAssetTypeId { get; set; }

    [Display(Name = "Cluster")]
    public byte ZrefAssetClusterId { get; set; }

    [Display(Name = "Kategori")]

    public byte ZrefAssetCategoryId { get; set; }

    [Display(Name = "Jenis Kepemilikan")]

    public byte ZrefOwnershipTypeId { get; set; }

    [Display(Name = "Status Sertifikat")]

    public byte ZrefCertificateStatusId { get; set; }

    [Display(Name = "Jenis Alas Hak")]

    public byte ZrefRightsTypeId { get; set; }

    [Display(Name = "Provinsi")]

    public int RegProvinceId { get; set; }
    
    [Display(Name = "Kab/Kota")]

    public int RegRegencyId { get; set; }

    [Display(Name = "Kecamatan")]

    public int RegDistrictId { get; set; }

    // Wajib teks/angka/tanggal (NOT NULL)

    [Display(Name = "Cost Center")]

    public string CostCenter { get; set; } = string.Empty;

    [Display(Name = "Desa")]

    public string Village { get; set; } = string.Empty;

    [Display(Name = "Deskripsi")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Nama Pemilik Sertifikat")]
    public string OwnerName { get; set; } = string.Empty;

    [Display(Name = "No Dokumen")]
    public string DocumentNumber { get; set; } = string.Empty;

    [Display(Name = "No Sertifikat")]

    public string CertificateNumber { get; set; } = string.Empty;
    
    [Display(Name = "Luas Tanah")]

    public int SurfaceArea { get; set; }

    [Display(Name = "Tanggal Terbit Sertifikat")]
    public DateOnly CertificateIssueDate { get; set; }

    [Display(Name = "Tanggal Expired Sertifikat")]
    public DateOnly CertificateExpiryDate { get; set; }

    [Display(Name = "No Hak Alas")]

    public string RightsNumber { get; set; } = string.Empty;

    [Display(Name = "NOP")]

    public string TaxObjectNumber { get; set; } = string.Empty;

    [Display(Name = "NJOP")]

    public decimal ValueNjop { get; set; }      

    [Display(Name = "Nilai Akuisisi")]

    public decimal ValueAcquisition { get; set; }  

    [Display(Name = "Nilai Buku")]

    public decimal ValueBook { get; set; }

    // Nullable only for these 3 (sesuai DB)
    [NotMapped]
    public decimal? MapLatitude { get; set; }
    [NotMapped]
    public decimal? MapLongitude { get; set; }
    
    [NotMapped]
    public object? MapPolygon { get; set; } 

    public virtual RegDistrict?           RegDistrict { get; set; }
    public virtual RegProvince?           RegProvince { get; set; }
    public virtual RegRegency?            RegRegency { get; set; }
    public virtual ZrefAssetCategory?     ZrefAssetCategory { get; set; }
    public virtual ZrefAssetCluster?      ZrefAssetCluster { get; set; }
    public virtual ZrefAssetType?         ZrefAssetType { get; set; }
    public virtual ZrefCertificateStatus? ZrefCertificateStatus { get; set; }
    public virtual ZrefOwnershipType?     ZrefOwnershipType { get; set; }
    public virtual ZrefRightsType?        ZrefRightsType { get; set; }
}
