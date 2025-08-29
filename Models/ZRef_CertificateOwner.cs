using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("ZRef_CertificateOwner", Schema = "ZRef")]
public partial class ZRef_CertificateOwner
{
    [Key]
    public byte Id { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("ZRef_CertificateStatus")]
    public virtual ICollection<AssetLandDoc> AssetLandDocs { get; set; } = new List<AssetLandDoc>();
}
