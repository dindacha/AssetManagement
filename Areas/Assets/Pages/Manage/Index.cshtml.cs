using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class IndexModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;

    // filter via querystring ?FilterTypeId=1
    [BindProperty(SupportsGet = true)]
    public byte? FilterTypeId { get; set; }

    public SelectList AssetTypes { get; private set; } = default!;
    public List<AssetListItem> Items { get; private set; } = new();

    public class AssetListItem
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Cluster { get; set; } = "";
        public string Category { get; set; } = "";
        public string? Village { get; set; }
        public string CostCenter { get; set; } = "";
        public string? SapNumber { get; set; }
        public decimal? AreaM2 { get; set; }   // hanya untuk Tanah
    }

    public async Task OnGetAsync()
    {
        // dropdown jenis aset
        AssetTypes = new SelectList(
            await _db.ZRef_AssetTypes.AsNoTracking().ToListAsync(), "Id", "Name", FilterTypeId);

        // base query master
        var baseQ = _db.Assets.AsNoTracking().AsQueryable();
        if (FilterTypeId.HasValue)
            baseQ = baseQ.Where(a => a.ZRef_AssetTypeId == FilterTypeId.Value);

        // join ke lookup & child tanah
        Items = await
        (
            from a in baseQ
            join t  in _db.ZRef_AssetTypes.AsNoTracking()    on a.ZRef_AssetTypeId  equals t.Id
            join cl in _db.ZRef_AssetClusters.AsNoTracking() on a.ZRef_AssetClusterId equals cl.Id
            join ca in _db.ZRef_AssetCategories.AsNoTracking() on a.ZRef_AssetCategoryId equals ca.Id
            join vj in _db.Reg_Villages.AsNoTracking() on a.Reg_VillageCode equals vj.Code into vleft
            from v in vleft.DefaultIfEmpty()
            join lj in _db.AssetLands.AsNoTracking() on a.Id equals lj.AssetId into lleft
            from l in lleft.DefaultIfEmpty()
            orderby a.Id descending
            select new AssetListItem
            {
                Id        = a.Id,
                Type      = t.Name,
                Cluster   = cl.Name,
                Category  = ca.Name,
                Village   = v != null ? v.Name : null,
                CostCenter= a.CostCenter,
                SapNumber = a.SAP_Number,
                AreaM2    = a.ZRef_AssetTypeId == (byte)1 ? l.SurfaceArea : null
            }
        ).ToListAsync();
    }
}
