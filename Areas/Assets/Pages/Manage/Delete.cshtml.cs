using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Areas.Assets.Pages.Manage;

public class DeleteModel(AppDbContext db) : PageModel
{
    private readonly AppDbContext _db = db;
    [BindProperty] public Asset? Asset { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Asset = await _db.Assets
            .Include(a => a.ZrefAssetType)
            .FirstOrDefaultAsync(a => a.Id == id);
        return Asset is null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var asset = await _db.Assets.FindAsync(id);
        if (asset is null) return NotFound();

        _db.Assets.Remove(asset);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
