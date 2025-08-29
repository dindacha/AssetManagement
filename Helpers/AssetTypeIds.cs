using AssetManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Helpers;

public static class AssetTypeIds
{
    public static byte Land { get; private set; }
    public static byte Building { get; private set; }
    private static bool _isInit;

    public static async Task InitAsync(AppDbContext db, CancellationToken ct = default)
    {
        if (_isInit) return;

        var map = await db.ZRef_AssetTypes
            .AsNoTracking()
            .ToDictionaryAsync(t => t.Name, t => t.Id, ct);

        Land     = map["Tanah"];     
        Building = map["Bangunan"];  
        _isInit  = true;
    }
}
