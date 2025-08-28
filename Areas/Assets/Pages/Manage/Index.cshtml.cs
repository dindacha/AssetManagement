using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class IndexModel(AppDbContext context) : PageModel
{
    private readonly AppDbContext _db = context;

    public IList<AssetListItem> Items { get; private set; } = new List<AssetListItem>();

    public async Task OnGetAsync()
    {
        Items = await _db.Assets
            .Include(a => a.ZrefAssetType)
            .Include(a => a.ZrefAssetCategory)
            .Include(a => a.ZrefAssetCluster)
            .Include(a => a.ZrefOwnershipType)
            .Include(a => a.RegProvince)
            .Include(a => a.RegRegency)
            .Include(a => a.RegDistrict)
            .Select(a => new AssetListItem
            {
                Id            = a.Id,
                AssetType     = a.ZrefAssetType!.Name,     
                Category      = a.ZrefAssetCategory!.Name,
                Cluster       = a.ZrefAssetCluster!.Name,
                OwnershipType = a.ZrefOwnershipType!.Name,
                Province      = a.RegProvince!.Name,
                Regency       = a.RegRegency!.Name,
                District      = a.RegDistrict!.Name,
                OwnerName     = a.OwnerName,
                ValueBook     = a.ValueBook
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
