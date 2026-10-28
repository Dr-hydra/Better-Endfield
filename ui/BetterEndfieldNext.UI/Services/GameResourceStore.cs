using System.Text.Json;

namespace BetterEndfieldNext.UI.Services;

// Game data is deliberately outside the executable. A snapshot is immutable for
// the duration of a consumer; imported versions never overwrite an active tree.
internal static class GameResourceStore
{
    internal sealed record Snapshot(string Root, string Version, JsonElement Manifest)
    {
        public string File(string relative)
        {
            if (relative.Contains('\\') || relative.Contains(':') || relative.Split('/').Any(x => x is "" or "." or ".."))
                throw new InvalidDataException("Invalid resource path.");
            string path = Path.GetFullPath(Path.Combine(Root, relative));
            if (!path.StartsWith(Path.GetFullPath(Root) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Resource path escapes its root.");
            // Reject reparse points, including parents: a manifest cannot bless a link.
            for (string? p = path; p is not null && p.Length >= Root.Length; p = Path.GetDirectoryName(p))
                if ((System.IO.File.GetAttributes(p) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidDataException("Resource links are not supported.");
            if (!Manifest.GetProperty("files").TryGetProperty(relative, out JsonElement size) ||
                new FileInfo(path).Length != size.GetInt64())
                throw new InvalidDataException("Resource file is missing or incomplete: " + relative);
            return path;
        }
    }

    public static string Root => Path.Combine(AppContext.BaseDirectory, "resources");
    public static Snapshot? Resolve(string type, string? resourceRoot = null)
    {
        string baseRoot = resourceRoot ?? Root;
        string directory = Path.Combine(baseRoot, type, "windows");
        if (!Directory.Exists(directory)) return null;
        string? selected = null;
        string state = Path.Combine(baseRoot, "state.json");
        if (System.IO.File.Exists(state))
        {
            try
            {
                using var doc = JsonDocument.Parse(System.IO.File.ReadAllText(state));
                if (doc.RootElement.TryGetProperty(type + ".windows", out var value)) selected = value.GetString();
            }
            catch (Exception ex) when (ex is IOException or JsonException or InvalidOperationException) { return null; }
        }
        var candidates = Directory.GetDirectories(directory)
            .Select(p => (Path: p, Version: ParseVersion(Path.GetFileName(p))))
            .Where(x => x.Version is not null && (selected is null || Path.GetFileName(x.Path) == selected))
            .OrderByDescending(x => x.Version);
        foreach (var candidate in candidates)
        {
            try
            {
                using var doc = JsonDocument.Parse(System.IO.File.ReadAllText(Path.Combine(candidate.Path, "manifest.json")));
                var m = doc.RootElement;
                if (m.GetProperty("schemaVersion").GetInt32() != 1 || m.GetProperty("kind").GetString() != "betterendfield-resource-pack" ||
                    m.GetProperty("packageType").GetString() != type || m.GetProperty("platform").GetString() != "windows" ||
                    m.GetProperty("version").GetString() != Path.GetFileName(candidate.Path) || m.GetProperty("files").ValueKind != JsonValueKind.Object)
                    continue;
                return new Snapshot(candidate.Path, Path.GetFileName(candidate.Path), m.Clone());
            }
            catch (Exception ex) when (ex is IOException or JsonException or InvalidOperationException or KeyNotFoundException) { }
        }
        return null;
    }
    private static Version? ParseVersion(string value) => value.Split('.').Length == 4 &&
        Version.TryParse(value, out var version) && version.Revision >= 0 ? version : null;

    private static readonly Lazy<Snapshot?> Core = new(() => Resolve("core"));
    public static Stream? OpenCore(string path)
    {
        try { return Core.Value is { } core ? System.IO.File.OpenRead(core.File(path)) : null; }
        catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException) { return null; }
    }
}
