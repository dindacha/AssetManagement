namespace AssetManagement.Areas.Assets.Pages.Manage;
using AssetManagement.Models;
using AssetManagement.Areas.Assets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class CreateModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;

    [BindProperty] public AssetUpsertVm Vm { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadLookups();
        Vm.ZRef_AssetTypeId = 1;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadLookups();
            return Page();
        }

        // 1) Map ke master
        var asset = new Asset
        {
            ZRef_AssetTypeId         = Vm.ZRef_AssetTypeId,
            ZRef_AssetClusterId      = Vm.ZRef_AssetClusterId,
            ZRef_AssetCategoryId     = Vm.ZRef_AssetCategoryId,
            Reg_VillageCode          = Vm.Reg_VillageCode!,
            CostCenter               = Vm.CostCenter!,
            Description              = Vm.Description ?? "",
            ZRef_OwnershipTypeId     = Vm.ZRef_OwnershipTypeId ?? default,
            ZRef_AssetAvailabilityId = Vm.ZRef_AssetAvailabilityId ?? default,
            SAP_Number               = Vm.SAP_Number
        };

        _db.Assets.Add(asset);

        // 2) Kalau tipe Tanah → insert child AssetLand
        if (Vm.ZRef_AssetTypeId == 1)
        {
            _db.AssetLands.Add(new AssetLand
            {
                Asset               = asset,
                SurfaceArea         = Vm.Land.SurfaceArea ?? 0,
                ZRef_RightsTypeId   = Vm.Land.ZRef_RightsTypeId ?? default,
                RightsNumber        = Vm.Land.RightsNumber,
                TaxObjectNumber     = Vm.Land.TaxObjectNumber,
                Value_NJOP          = Vm.Land.Value_NJOP,
                Value_SAP           = Vm.Land.Value_SAP,
                Value_AppraisalKJPP = Vm.Land.Value_AppraisalKJPP
            });
        }
        // else if (Vm.ZRef_AssetTypeId == 2) { // nanti untuk AssetBuilding }

        try
        {
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError(string.Empty, $"DB error: {ex.InnerException?.Message ?? ex.Message}");
            await LoadLookups();
            return Page();
        }
    }

    private async Task LoadLookups()
    {
        Vm.Types = await _db.ZRef_AssetTypes.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.Clusters = await _db.ZRef_AssetClusters.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.Categories = await _db.ZRef_AssetCategories.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.OwnershipTypes = await _db.ZRef_OwnershipTypes.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.Availabilities = await _db.ZRef_AssetAvailabilities.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.RightsTypes = await _db.ZRef_RightsTypes.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

        Vm.Villages = await _db.Reg_Villages.AsNoTracking()
            .Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToListAsync();
    }
}
