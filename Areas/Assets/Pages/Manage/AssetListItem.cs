namespace AssetManagement.Areas.Assets.Pages.Manage;

public class AssetListItem
{
    public int Id { get; set; }
    public string? AssetType { get; set; }
    public string? Category { get; set; }
    public string? Cluster { get; set; }
    public string? OwnershipType { get; set; }
    public string? Province { get; set; }
    public string? Regency { get; set; }
    public string? District { get; set; }
    public string? OwnerName { get; set; }
    public decimal? ValueBook { get; set; }
}
