using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace BetterEndfield.UI.Services;

internal sealed record ThirdPartyModuleRecord(JsonObject State, JsonObject Manifest, string Error = "")
{
    public string Id => State["id"]!.GetValue<string>();
    public string Generation => State["generation"]!.GetValue<string>();
    public string Directory => State["directory"]!.GetValue<string>();
    public string Name => Manifest["name"]!.GetValue<string>();
    public bool Enabled => State["enabled"]!.GetValue<bool>();
    public string Ui => Manifest["ui"]?.GetValue<string>() ?? "";
    public bool Supported => Error.Length == 0 && (Ui.Length > 0 || Manifest["libraries"]?["windows-x64"] is not null);
}

internal sealed class ThirdPartyModuleService
{
    internal const long ArchiveLimit = 256L * 1024 * 1024, EntryLimit = 128L * 1024 * 1024;
    private static readonly SemaphoreSlim Gate = new(1, 1);
    public string Root { get; }
    public string IndexPath => Path.Combine(Root, "index.json");
    public ThirdPartyModuleService(string? root = null) => Root = root ?? Path.Combine(ConfigurationService.SettingsDirectory, "third-party");

    internal static void RequireId(string id, bool package = true)
    {
        if (!Regex.IsMatch(id, @"\A[A-Za-z0-9][A-Za-z0-9_.-]{0,95}\z") ||
            (package && (id.StartsWith("betterendfield.", StringComparison.OrdinalIgnoreCase) || id.Equals("voice.character", StringComparison.OrdinalIgnoreCase))))
            throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidId"]);
    }
    internal static string Relative(string value)
    {
        if (value.Length == 0 || value.Length > 240 || value.Contains('\\') || value.Contains(':') || value.StartsWith('/'))
            throw new InvalidDataException(LocalizationService.Instance["Modules_UnsafePath"]);
        foreach (string part in value.Split('/'))
            if (part.Length == 0 || part is "." or ".." || part.EndsWith('.') || part.EndsWith(' ') || part.Any(char.IsControl) ||
                Regex.IsMatch(part.Split('.')[0], @"\A(CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])\z", RegexOptions.IgnoreCase))
                throw new InvalidDataException(LocalizationService.Instance["Modules_UnsafePath"]);
        return value;
    }
    internal static JsonObject ReadManifest(string directory)
    {
        string path = Path.Combine(directory, "module.json");
        if (new FileInfo(path).Length > 256 * 1024) throw new InvalidDataException(LocalizationService.Instance["Modules_ManifestSize"]);
        var manifest = JsonNode.Parse(File.ReadAllText(path, Encoding.UTF8))?.AsObject() ?? throw new InvalidDataException(LocalizationService.Instance["Modules_NoManifest"]);
        if (manifest["format"]?.GetValue<int>() != 1 || manifest["abi"]?.GetValue<int>() != 1) throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidFormat"]);
        RequireId(manifest["id"]!.GetValue<string>());
        foreach (string key in new[] { "name", "author", "version" })
            if (manifest[key]?.GetValue<string>() is not string value || value.Length > 200 || (key == "name" && value.Length == 0))
                throw new InvalidDataException(LocalizationService.Instance.GetString("Modules_InvalidField", key));
        var libraries = manifest["libraries"]?.AsObject() ?? throw new InvalidDataException(LocalizationService.Instance["Modules_NoLibraries"]);
        foreach (var library in libraries)
        {
            string relative = Relative(library.Value!.GetValue<string>());
            if (!File.Exists(Path.Combine(directory, relative)) ||
                (library.Key == "windows-x64" && !relative.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)) ||
                (library.Key == "android-arm64" && !relative.EndsWith(".so", StringComparison.Ordinal)))
                throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidLibrary"]);
        }
        string ui = manifest["ui"]?.GetValue<string>() ?? "";
        if (ui.Length > 0 && (!Relative(ui).EndsWith(".html", StringComparison.OrdinalIgnoreCase) || !File.Exists(Path.Combine(directory, ui))))
            throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidWebEntry"]);
        if (ui.Length == 0 && libraries.Count == 0) throw new InvalidDataException(LocalizationService.Instance["Modules_NoEntry"]);
        if (manifest["default_configuration"] is not null && manifest["default_configuration"] is not JsonObject)
            throw new InvalidDataException(LocalizationService.Instance["Modules_DefaultConfig"]);
        if (manifest["dependencies"] is JsonArray dependencies)
        {
            if (dependencies.Count > 128) throw new InvalidDataException(LocalizationService.Instance["Modules_DependencyLimit"]);
            foreach (var dependency in dependencies)
            {
                string dependencyId = dependency!.GetValue<string>(); RequireId(dependencyId);
                if (dependencyId == manifest["id"]!.GetValue<string>()) throw new InvalidDataException(LocalizationService.Instance["Modules_SelfDependency"]);
            }
        }
        else if (manifest["dependencies"] is not null) throw new InvalidDataException(LocalizationService.Instance["Modules_DependencyList"]);
        return manifest;
    }
    internal static JsonObject NewIndex()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0); listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port; listener.Stop();
        return new JsonObject { ["schema"] = 1, ["port"] = port,
            ["token"] = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLowerInvariant(), ["modules"] = new JsonArray() };
    }
    public JsonObject LoadIndex()
    {
        if (!File.Exists(IndexPath)) return NewIndex();
        if (new FileInfo(IndexPath).Length > 1024 * 1024) throw new InvalidDataException(LocalizationService.Instance["Modules_IndexSize"]);
        var index = JsonNode.Parse(File.ReadAllText(IndexPath, Encoding.UTF8))!.AsObject();
        if (index["schema"]?.GetValue<int>() != 1 || index["port"]?.GetValue<int>() is not int port || port < 1024 || port > 65535 ||
            index["token"]?.GetValue<string>() is not string token || !Regex.IsMatch(token, @"\A[a-f0-9]{64}\z") || index["modules"] is not JsonArray modules)
            throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidIndex"]);
        var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in modules)
        {
            var state = node!.AsObject(); string id = state["id"]!.GetValue<string>(); RequireId(id);
            if (!ids.Add(id) || !Guid.TryParseExact(state["generation"]!.GetValue<string>(), "D", out _) || state["configuration"] is not JsonObject)
                throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidRecord"]);
            _ = state["enabled"]!.GetValue<bool>();
            string expected = Path.GetFullPath(Path.Combine(Root, "packages", state["generation"]!.GetValue<string>()));
            if (!Path.GetFullPath(state["directory"]!.GetValue<string>()).Equals(expected, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidDirectory"]);
        }
        return index;
    }
    private async Task WriteAsync(JsonObject index)
    {
        Directory.CreateDirectory(Root); string value = index.ToJsonString();
        if (Encoding.UTF8.GetByteCount(value) > 1024 * 1024) throw new InvalidDataException(LocalizationService.Instance["Modules_ConfigLimit"]);
        string temp = Path.Combine(Root, Guid.NewGuid() + ".tmp");
        try { await File.WriteAllTextAsync(temp, value, new UTF8Encoding(false)); File.Move(temp, IndexPath, true); }
        finally { if (File.Exists(temp)) File.Delete(temp); }
    }
    public IReadOnlyList<ThirdPartyModuleRecord> Records()
    {
        var records = new List<ThirdPartyModuleRecord>();
        foreach (var node in LoadIndex()["modules"]!.AsArray())
        {
            var state = node!.AsObject();
            try { records.Add(new ThirdPartyModuleRecord(state, ReadManifest(state["directory"]!.GetValue<string>()))); }
            catch (Exception error) when (error is IOException or InvalidDataException or InvalidOperationException or ArgumentException or NullReferenceException or System.Text.Json.JsonException)
            {
                records.Add(new ThirdPartyModuleRecord(state, new JsonObject { ["name"] = state["id"]!.GetValue<string>(),
                    ["author"] = "", ["version"] = "", ["libraries"] = new JsonObject() }, error.Message));
            }
        }
        return records;
    }
    public ThirdPartyModuleRecord Record(string id) => Records().FirstOrDefault(record => record.Id == id) ?? throw new InvalidOperationException(LocalizationService.Instance["Modules_RecordRemoved"]);
    private async Task ChangeAsync(Action<JsonObject, JsonArray> change)
    {
        await Gate.WaitAsync();
        try { var index = LoadIndex(); change(index, index["modules"]!.AsArray()); await WriteAsync(index); }
        finally { Gate.Release(); }
    }
    public Task SetEnabledAsync(string id, bool enabled) => ChangeAsync((_, modules) =>
    {
        var state = modules.First(m => m!["id"]!.GetValue<string>() == id)!.AsObject();
        if (enabled && !new ThirdPartyModuleRecord(state, ReadManifest(state["directory"]!.GetValue<string>())).Supported)
            throw new InvalidOperationException(LocalizationService.Instance["Modules_NoWindowsEntry"]);
        state["enabled"] = enabled;
    });
    public Task SaveConfigurationAsync(string id, JsonObject configuration) => ChangeAsync((_, modules) =>
        modules.First(m => m!["id"]!.GetValue<string>() == id)!["configuration"] = configuration.DeepClone());
    public Task MoveAsync(string id, int direction) => ChangeAsync((_, modules) =>
    {
        int from = modules.Select(m => m!["id"]!.GetValue<string>()).ToList().IndexOf(id), to = from + direction;
        if (from < 0 || to < 0 || to >= modules.Count) return;
        var record = modules[from]; modules.RemoveAt(from); modules.Insert(to, record);
    });
    public Task RemoveAsync(string id) => ChangeAsync((index, modules) =>
    {
        var record = modules.First(m => m!["id"]!.GetValue<string>() == id)!;
        (index["retired_directories"] ??= new JsonArray()).AsArray().Add(record["directory"]!.GetValue<string>());
        modules.Remove(record); // Generations remain immutable while the game can still own their DLLs.
    });
    public async Task<ThirdPartyModuleRecord> ImportAsync(string source)
    {
        if (new FileInfo(source).Length > ArchiveLimit) throw new InvalidDataException(LocalizationService.Instance["Modules_ArchiveLimit"]);
        string generation = Guid.NewGuid().ToString(), directory = Path.Combine(Root, "packages", generation);
        Directory.CreateDirectory(directory);
        bool published = false;
        try
        {
            using var zip = ZipFile.OpenRead(source);
            if (zip.Entries.Count > 4096) throw new InvalidDataException(LocalizationService.Instance["Modules_FileCount"]);
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase); long declared = 0, actual = 0;
            foreach (var entry in zip.Entries)
            {
                bool folder = entry.FullName.EndsWith('/'); string relative = Relative(folder ? entry.FullName[..^1] : entry.FullName);
                if (!names.Add(relative) || ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000 || entry.Length > EntryLimit ||
                    (declared += entry.Length) > ArchiveLimit) throw new InvalidDataException(LocalizationService.Instance["Modules_InvalidArchive"]);
                string destination = Path.Combine(directory, relative);
                if (folder) { Directory.CreateDirectory(destination); continue; }
                Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                await using var input = entry.Open(); await using var output = File.Create(destination);
                byte[] buffer = new byte[65536]; long copied = 0; int count;
                while ((count = await input.ReadAsync(buffer)) > 0)
                {
                    copied += count; actual += count;
                    if (copied > EntryLimit || actual > ArchiveLimit) throw new InvalidDataException(LocalizationService.Instance["Modules_ExtractionLimit"]);
                    await output.WriteAsync(buffer.AsMemory(0, count));
                }
                if (copied != entry.Length) throw new InvalidDataException(LocalizationService.Instance["Modules_LengthMismatch"]);
            }
            if (!zip.Entries.Any(e => e.FullName == "module.json")) throw new InvalidDataException(LocalizationService.Instance["Modules_ManifestRoot"]);
            var manifest = ReadManifest(directory); string id = manifest["id"]!.GetValue<string>();
            JsonObject? installed = null;
            await ChangeAsync((index, modules) =>
            {
                var previous = modules.FirstOrDefault(m => m!["id"]!.GetValue<string>().Equals(id, StringComparison.OrdinalIgnoreCase));
                if (previous is not null && previous["id"]!.GetValue<string>() != id) throw new InvalidDataException(LocalizationService.Instance["Modules_IdCase"]);
                installed = new JsonObject { ["id"] = id, ["enabled"] = previous?["enabled"]?.GetValue<bool>() ?? false,
                    ["directory"] = directory, ["generation"] = generation,
                    ["configuration"] = previous?["configuration"]?.DeepClone() ?? manifest["default_configuration"]?.DeepClone() ?? new JsonObject() };
                if (!new ThirdPartyModuleRecord(installed, manifest).Supported) installed["enabled"] = false;
                if (previous is not null)
                {
                    int position = modules.IndexOf(previous); (index["retired_directories"] ??= new JsonArray()).AsArray().Add(previous["directory"]!.GetValue<string>());
                    modules.RemoveAt(position); modules.Insert(position, installed);
                }
                else modules.Add(installed);
            });
            published = true; return new ThirdPartyModuleRecord(installed!, manifest);
        }
        finally { if (!published && Directory.Exists(directory)) Directory.Delete(directory, true); }
    }
    public async Task<JsonNode> RuntimeAsync(string operation, string id, JsonNode? payload = null, string? requestId = null)
    {
        var index = LoadIndex(); _ = Record(id);
        using var client = new HttpClient(new HttpClientHandler { UseProxy = false }) { Timeout = TimeSpan.FromSeconds(2) };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", index["token"]!.GetValue<string>());
        var url = new Uri($"http://127.0.0.1:{index["port"]!.GetValue<int>()}/{operation}");
        using var request = new HttpRequestMessage(operation == "status" ? HttpMethod.Get : HttpMethod.Post, url);
        if (operation != "status")
        {
            var body = new JsonObject { ["module_id"] = id };
            if (operation == "send") { body["request_id"] = requestId ?? Guid.NewGuid().ToString(); body["body"] = payload?.DeepClone(); }
            if (operation == "configure") body["configuration"] = payload?.DeepClone();
            request.Content = new StringContent(body.ToJsonString(), Encoding.UTF8, "application/json");
        }
        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead); response.EnsureSuccessStatusCode();
        await using var responseStream = await response.Content.ReadAsStreamAsync(); using var bytes = new MemoryStream();
        byte[] responseBuffer = new byte[8192]; int read;
        while ((read = await responseStream.ReadAsync(responseBuffer)) > 0)
        {
            if (bytes.Length + read > 1024 * 1024) throw new InvalidDataException(LocalizationService.Instance["Modules_ResponseLimit"]);
            await bytes.WriteAsync(responseBuffer.AsMemory(0, read));
        }
        string text = Encoding.UTF8.GetString(bytes.ToArray());
        var result = JsonNode.Parse(text)!;
        if (operation != "status") return result;
        var module = result["modules"]?.AsArray().FirstOrDefault(m => m?["id"]?.GetValue<string>() == id)?.DeepClone();
        return new JsonObject { ["connected"] = result["connected"]?.DeepClone() ?? true, ["module"] = module };
    }
}
