using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

[Table("ZRef_RightsType", Schema = "ZRef")]
public partial class ZRef_RightsType
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
}
