using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("AssetLandDoc", Schema = "Asset")]
public partial class AssetLandDoc
{
    [Key]
    public int Id { get; set; }

    public int AssetId { get; set; }

    public byte ZRef_CertificateStatusId { get; set; }

    public byte ZRef_CertificateOwnerId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string DocumentNumber { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    [ForeignKey("AssetId")]
    [InverseProperty("AssetLandDocs")]
    public virtual Asset Asset { get; set; } = null!;

    [ForeignKey("ZRef_CertificateStatusId")]
    [InverseProperty("AssetLandDocs")]
    public virtual ZRef_CertificateOwner ZRef_CertificateStatus { get; set; } = null!;

    [ForeignKey("ZRef_CertificateStatusId")]
    [InverseProperty("AssetLandDocs")]
    public virtual ZRef_CertificateStatus ZRef_CertificateStatusNavigation { get; set; } = null!;
}
