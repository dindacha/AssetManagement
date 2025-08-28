using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class DetailsModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;
    public Asset? Asset { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Asset = await _db.Assets
            .Include(a => a.ZrefAssetType)
            .Include(a => a.ZrefAssetCategory)
            .Include(a => a.ZrefAssetCluster)
            .Include(a => a.ZrefOwnershipType)
            .Include(a => a.RegProvince)
            .Include(a => a.RegRegency)
            .Include(a => a.RegDistrict)
            .FirstOrDefaultAsync(a => a.Id == id);

        return Asset is null ? NotFound() : Page();
    }
}
