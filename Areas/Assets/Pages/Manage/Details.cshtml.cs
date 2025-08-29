using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;



public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;

    public Asset? AssetItem { get; private set; }
    public AssetLand? Land { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        AssetItem = await _db.Assets.AsNoTracking()
            .Include(a => a.ZRef_AssetType)
            .Include(a => a.ZRef_AssetCluster)
            .Include(a => a.ZRef_AssetCategory)
            .Include(a => a.ZRef_OwnershipType)
            .Include(a => a.ZRef_AssetAvailability)
            .Include(a => a.Reg_VillageCodeNavigation)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (AssetItem == null) return NotFound();

        if (AssetItem.ZRef_AssetTypeId == 1)
        {
            Land = await _db.AssetLands.AsNoTracking()
                .FirstOrDefaultAsync(l => l.AssetId == id);
        }

        return Page();
    }
}
