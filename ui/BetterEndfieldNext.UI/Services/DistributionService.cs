using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BetterEndfieldNext.UI.Services;

internal static class DistributionService
{
    internal const string IndexUrl = "https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/distribution-index.json";
    private const string DownloadRoot = "https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/";
    private static readonly HttpClient Client = new() { Timeout = TimeSpan.FromMinutes(30) };
    private static readonly SemaphoreSlim Gate = new(1, 1);
    private static JsonElement? _cached;
    private static DateTime _cachedAt;

    public static async Task<JsonElement?> IndexAsync(CancellationToken token)
    {
        if (_cached is { } cached && DateTime.UtcNow - _cachedAt < TimeSpan.FromMinutes(2)) return cached;
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
        timeout.CancelAfter(TimeSpan.FromSeconds(15));
        using var request = new HttpRequestMessage(HttpMethod.Get, IndexUrl);
        request.Headers.UserAgent.ParseAdd("BetterEndfieldNext/" + UpdateService.CurrentVersion);
        using var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, timeout.Token);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        await using var stream = await response.Content.ReadAsStreamAsync(timeout.Token);
        using var bytes = new MemoryStream();
        byte[] buffer = new byte[65536]; int count;
        while ((count = await stream.ReadAsync(buffer, timeout.Token)) > 0)
        {
            if (bytes.Length + count > 8 * 1024 * 1024) throw new InvalidDataException("Distribution index is too large.");
            bytes.Write(buffer, 0, count);
        }
        using var document = JsonDocument.Parse(bytes.ToArray());
        var index = document.RootElement;
        try
        {
            if (index.GetProperty("schemaVersion").GetInt32() != 1 || index.GetProperty("kind").GetString() != "betterendfield-distribution-index" ||
                index.GetProperty("resources").ValueKind != JsonValueKind.Array)
                throw new JsonException("Unsupported distribution index.");
            var software = index.GetProperty("software");
            if (!System.Text.RegularExpressions.Regex.IsMatch(software.GetProperty("version").GetString() ?? "", "^[0-9]{1,9}([.][0-9]{1,9}){2}$") ||
                !(software.GetProperty("releaseUrl").GetString() ?? "").StartsWith(UpdateService.ReleasesUrl + "/", StringComparison.Ordinal))
                throw new JsonException("Invalid software release metadata.");
        }
        catch (Exception error) when (error is KeyNotFoundException or InvalidOperationException or FormatException)
        { throw new JsonException("Invalid distribution metadata.", error); }
        _cached = index.Clone(); _cachedAt = DateTime.UtcNow;
        return _cached;
    }

    private static JsonObject State() => File.Exists(Path.Combine(GameResourceStore.Root, "state.json"))
        ? JsonNode.Parse(File.ReadAllText(Path.Combine(GameResourceStore.Root, "state.json")))!.AsObject() : new();

    private static void Activate(string type, string version, bool automatic)
    {
        var state = State(); state[type + ".windows"] = version; state["auto." + type + ".windows"] = automatic;
        string temp = Path.Combine(GameResourceStore.Root, Guid.NewGuid() + ".state.tmp");
        try { File.WriteAllText(temp, state.ToJsonString()); File.Move(temp, Path.Combine(GameResourceStore.Root, "state.json"), true); }
        finally { if (File.Exists(temp)) File.Delete(temp); }
    }

    private static string SafePath(string root, string name)
    {
        if (string.IsNullOrEmpty(name) || name.Contains('\\') || name.Contains(':') || name.Split('/').Any(x => x is "" or "." or ".."))
            throw new InvalidDataException("Invalid resource path.");
        string path = Path.GetFullPath(Path.Combine(root, name));
        if (!path.StartsWith(Path.GetFullPath(root) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Resource path escapes staging.");
        return path;
    }

    // Caller holds Gate; installed directories are immutable, and state changes last.
    private static async Task<string> InstallCoreAsync(string archive, bool automatic, JsonElement? expected, CancellationToken token)
    {
        string root = GameResourceStore.Root;
        string staging = Path.Combine(root, "staging", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(staging);
        try
        {
            using var zip = ZipFile.OpenRead(archive);
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in zip.Entries)
            {
                token.ThrowIfCancellationRequested();
                if (entry.FullName.EndsWith('/')) continue;
                if (!names.Add(entry.FullName) || names.Count > 200000 || ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000)
                    throw new InvalidDataException("Duplicate or unsupported resource entry.");
                string destination = SafePath(staging, entry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                await using var input = entry.Open(); await using var output = File.Create(destination);
                await input.CopyToAsync(output, token);
                if (output.Length != entry.Length) throw new InvalidDataException("Truncated resource entry.");
            }
            using var document = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Combine(staging, "manifest.json"), token));
            var manifest = document.RootElement;
            string type = manifest.GetProperty("packageType").GetString()!;
            string version = manifest.GetProperty("version").GetString()!;
            if (manifest.GetProperty("schemaVersion").GetInt32() != 1 || manifest.GetProperty("kind").GetString() != "betterendfield-resource-pack" ||
                manifest.GetProperty("platform").GetString() != "windows" || type is not ("core" or "preview") ||
                version.Split('.').Length != 4 || !Version.TryParse(version, out var number) || number.Revision < 0 ||
                manifest.GetProperty("gameVersion").GetString() != string.Join('.', version.Split('.').Take(3)))
                throw new InvalidDataException("Wrong platform or unsupported resource manifest.");
            if (expected is { } target && new[] { "packageType", "platform", "version", "gameVersion" }.Any(key => target.GetProperty(key).GetString() != manifest.GetProperty(key).GetString()))
                throw new InvalidDataException("Downloaded resource identity does not match the index.");
            var files = manifest.GetProperty("files");
            var snapshot = new GameResourceStore.Snapshot(staging, version, manifest);
            foreach (var file in files.EnumerateObject()) { token.ThrowIfCancellationRequested(); snapshot.File(file.Name); }
            if (files.EnumerateObject().Count() + 1 != names.Count) throw new InvalidDataException("Resource inventory mismatch.");
            if (type == "preview") snapshot.File("characters.json");
            else { snapshot.File("model/character-presets.json"); snapshot.File("model/character-names.json"); snapshot.File("voice/voice-catalog-index.json"); }
            string installed = Path.Combine(root, type, "windows", version);
            if (Directory.Exists(installed))
            {
                if (Directory.GetFiles(installed, "*", SearchOption.AllDirectories).Length != names.Count) throw new InvalidDataException("Existing resource version differs.");
                foreach (string name in names)
                {
                    token.ThrowIfCancellationRequested();
                    string path = SafePath(installed, name);
                    // Reparse points are rejected even when importing an already installed version.
                    for (string? p = path; p is not null && p.Length >= installed.Length; p = Path.GetDirectoryName(p))
                        if ((File.GetAttributes(p) & FileAttributes.ReparsePoint) != 0) throw new InvalidDataException("Resource links are unsupported.");
                    await using var left = File.OpenRead(SafePath(staging, name)); await using var right = File.OpenRead(path);
                    if (left.Length != right.Length) throw new InvalidDataException("Existing version differs; increment its revision.");
                    byte[] a = new byte[65536], b = new byte[65536]; int n;
                    while ((n = await left.ReadAsync(a, token)) != 0) { await right.ReadExactlyAsync(b.AsMemory(0,n), token); if (!a.AsSpan(0,n).SequenceEqual(b.AsSpan(0,n))) throw new InvalidDataException("Existing version differs; increment its revision."); }
                }
            }
            else { Directory.CreateDirectory(Path.GetDirectoryName(installed)!); Directory.Move(staging, installed); }
            Activate(type, version, automatic);
            return type + " " + version;
        }
        finally { if (Directory.Exists(staging)) Directory.Delete(staging, true); }
    }

    public static async Task<string> ImportAsync(string archive, CancellationToken token)
    {
        await Gate.WaitAsync(token);
        try { return await InstallCoreAsync(archive, false, null, token); }
        finally { Gate.Release(); }
    }

    public static async Task<string> UpdateResourcesAsync(string? gameVersion, bool includePreview, bool resumeAutomatic, CancellationToken token)
    {
        await Gate.WaitAsync(token);
        try
        {
            var index = await IndexAsync(token) ?? throw new IOException("Resource release is not available yet.");
            var messages = new List<string>();
            foreach (string type in new[] { "core", "preview" })
            {
                var current = GameResourceStore.Resolve(type);
                if (type == "preview" && current is null && !includePreview) continue;
                var state = State(); string key = type + ".windows";
                if (state[key] is not null && state["auto." + key]?.GetValue<bool>() != true && !resumeAutomatic)
                { messages.Add(type + " (pinned)"); continue; }
                string? baseline = gameVersion;
                if (baseline is null && current is not null && current.Manifest.TryGetProperty("gameVersion", out var gv)) baseline = gv.GetString();
                if (string.IsNullOrWhiteSpace(baseline)) { messages.Add(type + " (game version unknown)"); continue; }
                var candidates = index.GetProperty("resources").EnumerateArray().Where(e =>
                    e.GetProperty("packageType").GetString() == type && e.GetProperty("platform").GetString() == "windows" &&
                    e.GetProperty("gameVersion").GetString() == baseline && e.GetProperty("resourceApi").GetInt32() == 1 &&
                    Version.TryParse(e.GetProperty("version").GetString(), out _))
                    .OrderByDescending(e => Version.Parse(e.GetProperty("version").GetString()!)).ToArray();
                if (candidates.Length == 0) { messages.Add(type + " (no compatible release)"); continue; }
                var candidate = candidates[0]; string version = candidate.GetProperty("version").GetString()!;
                if (current is not null && current.Manifest.GetProperty("gameVersion").GetString() == baseline && Version.Parse(current.Version) >= Version.Parse(version))
                { if (resumeAutomatic) Activate(type, current.Version, true); messages.Add(type + " " + current.Version); continue; }
                string asset = candidate.GetProperty("asset").GetString()!, url = candidate.GetProperty("url").GetString()!;
                if (Path.GetFileName(asset) != asset || url != DownloadRoot + asset) throw new InvalidDataException("Untrusted resource URL.");
                long expectedBytes = candidate.GetProperty("bytes").GetInt64();
                if (expectedBytes <= 0) throw new InvalidDataException("Invalid resource archive size.");
                string downloads = Path.Combine(GameResourceStore.Root, "downloads"); Directory.CreateDirectory(downloads);
                string temporary = Path.Combine(downloads, Guid.NewGuid() + ".part");
                try
                {
                    using var response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token); response.EnsureSuccessStatusCode();
                    await using (var input = await response.Content.ReadAsStreamAsync(token))
                    await using (var output = File.Create(temporary))
                    {
                        byte[] buffer = new byte[65536]; long total = 0; int n;
                        while ((n = await input.ReadAsync(buffer, token)) != 0) { total += n; if (total > expectedBytes) throw new InvalidDataException("Resource size exceeds index."); await output.WriteAsync(buffer.AsMemory(0,n), token); }
                        if (total != expectedBytes) throw new InvalidDataException("Incomplete resource download.");
                    }
                    messages.Add(await InstallCoreAsync(temporary, true, candidate, token));
                }
                finally { if (File.Exists(temporary)) File.Delete(temporary); }
            }
            return string.Join("; ", messages);
        }
        finally { Gate.Release(); }
    }
}
