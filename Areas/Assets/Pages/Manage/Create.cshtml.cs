using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class CreateModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;

    [BindProperty] public Asset Asset { get; set; } = new();

    private void LoadLookups()
    {
        ViewData["ZrefAssetTypeId"]         = new SelectList(_db.ZrefAssetTypes.AsNoTracking(), "Id", "Name");
        ViewData["ZrefAssetClusterId"]      = new SelectList(_db.ZrefAssetClusters.AsNoTracking(), "Id", "Name");
        ViewData["ZrefAssetCategoryId"]     = new SelectList(_db.ZrefAssetCategories.AsNoTracking(), "Id", "Name");
        ViewData["ZrefOwnershipTypeId"]     = new SelectList(_db.ZrefOwnershipTypes.AsNoTracking(), "Id", "Name");
        ViewData["ZrefCertificateStatusId"] = new SelectList(_db.ZrefCertificateStatuses.AsNoTracking(), "Id", "Name");
        ViewData["ZrefRightsTypeId"]        = new SelectList(_db.ZrefRightsTypes.AsNoTracking(), "Id", "Name");

        ViewData["RegProvinceId"] = new SelectList(_db.RegProvinces.AsNoTracking(), "Id", "Name");
        ViewData["RegRegencyId"]  = new SelectList(_db.RegRegencies.AsNoTracking(), "Id", "Name");
        ViewData["RegDistrictId"] = new SelectList(_db.RegDistricts.AsNoTracking(), "Id", "Name");
    }

    public IActionResult OnGet()
    {
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

        try
        {
            _db.Assets.Add(Asset);
            await _db.SaveChangesAsync();
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
