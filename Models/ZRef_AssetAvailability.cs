using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("ZRef_AssetAvailability", Schema = "ZRef")]
public partial class ZRef_AssetAvailability
{
    [Key]
    public byte Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("ZRef_AssetAvailability")]
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
