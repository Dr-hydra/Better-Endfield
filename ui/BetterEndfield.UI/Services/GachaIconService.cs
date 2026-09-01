using System.Collections.Concurrent;
using System.Reflection;
using BetterEndfield.UI.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace BetterEndfield.UI.Services;

internal static class GachaIconService
{
    private static readonly ConcurrentDictionary<string, ImageSource?> Cache = new(StringComparer.OrdinalIgnoreCase);

    public static ImageSource? Load(string? itemId)
    {
        if (string.IsNullOrWhiteSpace(itemId)) return null;
        return Cache.GetOrAdd(itemId, LoadCore);
    }

    private static ImageSource? LoadCore(string itemId)
    {
        if (itemId.StartsWith("chr_", StringComparison.OrdinalIgnoreCase))
            return CombatHistoryService.LoadAvatar(itemId);

        if (!itemId.StartsWith("wpn_", StringComparison.OrdinalIgnoreCase)) return null;
        string resourceName = $"BetterEndfield.UI.Assets.gacha.weapon.{itemId}.png";
        using Stream? source = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (source is null) return null;
        using var memory = new MemoryStream();
        source.CopyTo(memory);
        memory.Position = 0;
        using IRandomAccessStream randomAccess = memory.AsRandomAccessStream();
        var bitmap = new BitmapImage();
        bitmap.SetSource(randomAccess);
        return bitmap;
    }
}
