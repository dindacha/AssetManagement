using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("ZRef_OwnershipType", Schema = "ZRef")]
public partial class ZRef_OwnershipType
{
    [Key]
    public byte Id { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string? Abbreviation { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("ZRef_OwnershipType")]
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
