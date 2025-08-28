using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class EditModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;

    [BindProperty] public Asset? Asset { get; set; }

    private void LoadLookups()
    {
        ViewData["ZrefAssetTypeId"]         = new SelectList(_db.ZrefAssetTypes.AsNoTracking(), "Id", "Name", Asset?.ZrefAssetTypeId);
        ViewData["ZrefAssetClusterId"]      = new SelectList(_db.ZrefAssetClusters.AsNoTracking(), "Id", "Name", Asset?.ZrefAssetClusterId);
        ViewData["ZrefAssetCategoryId"]     = new SelectList(_db.ZrefAssetCategories.AsNoTracking(), "Id", "Name", Asset?.ZrefAssetCategoryId);
        ViewData["ZrefOwnershipTypeId"]     = new SelectList(_db.ZrefOwnershipTypes.AsNoTracking(), "Id", "Name", Asset?.ZrefOwnershipTypeId);
        ViewData["ZrefCertificateStatusId"] = new SelectList(_db.ZrefCertificateStatuses.AsNoTracking(), "Id", "Name", Asset?.ZrefCertificateStatusId);
        ViewData["ZrefRightsTypeId"]        = new SelectList(_db.ZrefRightsTypes.AsNoTracking(), "Id", "Name", Asset?.ZrefRightsTypeId);

        ViewData["RegProvinceId"] = new SelectList(_db.RegProvinces.AsNoTracking(), "Id", "Name", Asset?.RegProvinceId);
        ViewData["RegRegencyId"]  = new SelectList(_db.RegRegencies.AsNoTracking(), "Id", "Name", Asset?.RegRegencyId);
        ViewData["RegDistrictId"] = new SelectList(_db.RegDistricts.AsNoTracking(), "Id", "Name", Asset?.RegDistrictId);
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var asset = await _db.Assets.FindAsync(id);
        if (asset is null) return NotFound();

        Asset = asset;
        LoadLookups();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadLookups();
            return Page();
        }

        if (Asset is null) return BadRequest();

        try
        {
            _db.Attach(Asset).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await _db.Assets.AnyAsync(a => a.Id == Asset.Id);
            if (!exists) return NotFound();
            throw;
        }
        catch (DbUpdateException ex)
        {
            var msg = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError(string.Empty, $"DB error: {msg}");
            LoadLookups();
            return Page();
        }

        return RedirectToPage("Index", new { area = "Assets" });
    }
}
