using AssetManagement.Areas.Assets.ViewModels;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;

        [BindProperty] public AssetUpsertVm Vm { get; set; } = new();
        public int AssetId { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AssetId = id;

            var a = await _db.Assets
                .AsNoTracking()
                .Include(x => x.AssetLand)            // muat detail tanah
                .FirstOrDefaultAsync(x => x.Id == id);

            if (a == null) return NotFound();

            // Inisialisasi VM (Land tidak boleh null)
            Vm = new AssetUpsertVm
            {
                ZRef_AssetTypeId         = a.ZRef_AssetTypeId,
                ZRef_AssetClusterId      = a.ZRef_AssetClusterId,
                ZRef_AssetCategoryId     = a.ZRef_AssetCategoryId,
                Reg_VillageCode          = a.Reg_VillageCode,
                CostCenter               = a.CostCenter,
                Description              = a.Description,
                ZRef_OwnershipTypeId     = a.ZRef_OwnershipTypeId,
                ZRef_AssetAvailabilityId = a.ZRef_AssetAvailabilityId,
                SAP_Number               = a.SAP_Number,
                Land                     = new AssetLandFieldsVm()
            };

            if (a.ZRef_AssetTypeId == 1 && a.AssetLand != null)
            {
                Vm.Land.SurfaceArea          = a.AssetLand.SurfaceArea;
                Vm.Land.ZRef_RightsTypeId    = a.AssetLand.ZRef_RightsTypeId;
                Vm.Land.RightsNumber         = a.AssetLand.RightsNumber;
                Vm.Land.TaxObjectNumber      = a.AssetLand.TaxObjectNumber;
                Vm.Land.Value_NJOP           = a.AssetLand.Value_NJOP;
                Vm.Land.Value_SAP            = a.AssetLand.Value_SAP;
                Vm.Land.Value_AppraisalKJPP  = a.AssetLand.Value_AppraisalKJPP;
            }

            await LoadLookups(); // isi semua dropdown
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            AssetId = id;

            if (!ModelState.IsValid)
            {
                await LoadLookups();
                return Page();
            }

            var asset = await _db.Assets
                .Include(x => x.AssetLand)            // track + muat child
                .FirstOrDefaultAsync(x => x.Id == id);

            if (asset == null) return NotFound();

            // Update kolom-kolom Asset
            asset.ZRef_AssetClusterId      = Vm.ZRef_AssetClusterId;
            asset.ZRef_AssetCategoryId     = Vm.ZRef_AssetCategoryId;
            asset.Reg_VillageCode          = Vm.Reg_VillageCode!;
            asset.CostCenter               = Vm.CostCenter!;
            asset.Description              = Vm.Description ?? string.Empty;
            asset.ZRef_OwnershipTypeId     = Vm.ZRef_OwnershipTypeId ?? asset.ZRef_OwnershipTypeId;
            asset.ZRef_AssetAvailabilityId = Vm.ZRef_AssetAvailabilityId ?? asset.ZRef_AssetAvailabilityId;
            asset.SAP_Number               = Vm.SAP_Number;

            // Update detail tanah
            if (asset.ZRef_AssetTypeId == 1)
            {
                if (asset.AssetLand == null)
                {
                    asset.AssetLand = new AssetLand { AssetId = id };
                    _db.AssetLands.Add(asset.AssetLand);
                }

                asset.AssetLand.SurfaceArea         = Vm.Land.SurfaceArea ?? asset.AssetLand.SurfaceArea;
                asset.AssetLand.ZRef_RightsTypeId   = Vm.Land.ZRef_RightsTypeId ?? asset.AssetLand.ZRef_RightsTypeId;
                asset.AssetLand.RightsNumber        = Vm.Land.RightsNumber;
                asset.AssetLand.TaxObjectNumber     = Vm.Land.TaxObjectNumber;
                asset.AssetLand.Value_NJOP          = Vm.Land.Value_NJOP;
                asset.AssetLand.Value_SAP           = Vm.Land.Value_SAP;
                asset.AssetLand.Value_AppraisalKJPP = Vm.Land.Value_AppraisalKJPP;
            }
            else
            {
                if (asset.AssetLand != null)
                    _db.AssetLands.Remove(asset.AssetLand);
            }

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
}
