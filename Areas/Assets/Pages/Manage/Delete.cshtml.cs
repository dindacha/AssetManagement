using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    public AssetSummaryVm Item { get; private set; } = new();

    public class AssetSummaryVm
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Cluster { get; set; } = "";
        public string Category { get; set; } = "";
        public string? Village { get; set; }
        public string CostCenter { get; set; } = "";
        public string? SapNumber { get; set; }
        public int? AreaM2 { get; set; }    // hanya utk Tanah
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var q =
            from a in _db.Assets.AsNoTracking().Where(x => x.Id == id)
            join t  in _db.ZRef_AssetTypes.AsNoTracking()    on a.ZRef_AssetTypeId   equals t.Id
            join cl in _db.ZRef_AssetClusters.AsNoTracking() on a.ZRef_AssetClusterId equals cl.Id
            join ca in _db.ZRef_AssetCategories.AsNoTracking() on a.ZRef_AssetCategoryId equals ca.Id
            join vj in _db.Reg_Villages.AsNoTracking() on a.Reg_VillageCode equals vj.Code into vleft
            from v in vleft.DefaultIfEmpty()
            join lj in _db.AssetLands.AsNoTracking() on a.Id equals lj.AssetId into lleft
            from l in lleft.DefaultIfEmpty()
            select new AssetSummaryVm
            {
                Id        = a.Id,
                Type      = t.Name,
                Cluster   = cl.Name,
                Category  = ca.Name,
                Village   = v != null ? v.Name : null,
                CostCenter= a.CostCenter,
                SapNumber = a.SAP_Number,
                AreaM2    = a.ZRef_AssetTypeId == 1 ? l.SurfaceArea : null
            };

        var item = await q.FirstOrDefaultAsync();
        if (item == null) return NotFound();
        Item = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var asset = await _db.Assets.FirstOrDefaultAsync(a => a.Id == id);
        if (asset == null) return NotFound();

        // Hapus child dulu (belum cascade)
        var land = await _db.AssetLands.FirstOrDefaultAsync(l => l.AssetId == id);
        if (land != null) _db.AssetLands.Remove(land);

        _db.Assets.Remove(asset);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
