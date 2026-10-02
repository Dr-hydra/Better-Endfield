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
            throw new InvalidDataException("无效或与内置模块冲突的模块 ID。");
    }
    internal static string Relative(string value)
    {
        if (value.Length == 0 || value.Length > 240 || value.Contains('\\') || value.Contains(':') || value.StartsWith('/'))
            throw new InvalidDataException("模块包含不安全的资源路径。");
        foreach (string part in value.Split('/'))
            if (part.Length == 0 || part is "." or ".." || part.EndsWith('.') || part.EndsWith(' ') || part.Any(char.IsControl) ||
                Regex.IsMatch(part.Split('.')[0], @"\A(CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])\z", RegexOptions.IgnoreCase))
                throw new InvalidDataException("模块包含不安全的资源路径。");
        return value;
    }
    internal static JsonObject ReadManifest(string directory)
    {
        string path = Path.Combine(directory, "module.json");
        if (new FileInfo(path).Length > 256 * 1024) throw new InvalidDataException("模块声明过大。");
        var manifest = JsonNode.Parse(File.ReadAllText(path, Encoding.UTF8))?.AsObject() ?? throw new InvalidDataException("缺少模块声明。");
        if (manifest["format"]?.GetValue<int>() != 1 || manifest["abi"]?.GetValue<int>() != 1) throw new InvalidDataException("不支持的第三方模块格式或 ABI。");
        RequireId(manifest["id"]!.GetValue<string>());
        foreach (string key in new[] { "name", "author", "version" })
            if (manifest[key]?.GetValue<string>() is not string value || value.Length > 200 || (key == "name" && value.Length == 0))
                throw new InvalidDataException("模块声明缺少有效的 " + key + "。");
        var libraries = manifest["libraries"]?.AsObject() ?? throw new InvalidDataException("缺少模块平台目录。");
        foreach (var library in libraries)
        {
            string relative = Relative(library.Value!.GetValue<string>());
            if (!File.Exists(Path.Combine(directory, relative)) ||
                (library.Key == "windows-x64" && !relative.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)) ||
                (library.Key == "android-arm64" && !relative.EndsWith(".so", StringComparison.Ordinal)))
                throw new InvalidDataException("模块原生文件不存在或平台类型不正确。");
        }
        string ui = manifest["ui"]?.GetValue<string>() ?? "";
        if (ui.Length > 0 && (!Relative(ui).EndsWith(".html", StringComparison.OrdinalIgnoreCase) || !File.Exists(Path.Combine(directory, ui))))
            throw new InvalidDataException("模块网页入口不存在或格式不正确。");
        if (ui.Length == 0 && libraries.Count == 0) throw new InvalidDataException("模块必须提供原生库或网页入口。");
        if (manifest["default_configuration"] is not null && manifest["default_configuration"] is not JsonObject)
            throw new InvalidDataException("模块默认配置必须是 JSON 对象。");
        if (manifest["dependencies"] is JsonArray dependencies)
        {
            if (dependencies.Count > 128) throw new InvalidDataException("模块依赖不能超过 128 项。");
            foreach (var dependency in dependencies)
            {
                string dependencyId = dependency!.GetValue<string>(); RequireId(dependencyId);
                if (dependencyId == manifest["id"]!.GetValue<string>()) throw new InvalidDataException("模块不能依赖自身。");
            }
        }
        else if (manifest["dependencies"] is not null) throw new InvalidDataException("模块依赖必须是 ID 列表。");
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
        if (new FileInfo(IndexPath).Length > 1024 * 1024) throw new InvalidDataException("第三方模块索引过大。");
        var index = JsonNode.Parse(File.ReadAllText(IndexPath, Encoding.UTF8))!.AsObject();
        if (index["schema"]?.GetValue<int>() != 1 || index["port"]?.GetValue<int>() is not int port || port < 1024 || port > 65535 ||
            index["token"]?.GetValue<string>() is not string token || !Regex.IsMatch(token, @"\A[a-f0-9]{64}\z") || index["modules"] is not JsonArray modules)
            throw new InvalidDataException("第三方模块索引不合法。");
        var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in modules)
        {
            var state = node!.AsObject(); string id = state["id"]!.GetValue<string>(); RequireId(id);
            if (!ids.Add(id) || !Guid.TryParseExact(state["generation"]!.GetValue<string>(), "D", out _) || state["configuration"] is not JsonObject)
                throw new InvalidDataException("第三方模块记录损坏。");
            _ = state["enabled"]!.GetValue<bool>();
            string expected = Path.GetFullPath(Path.Combine(Root, "packages", state["generation"]!.GetValue<string>()));
            if (!Path.GetFullPath(state["directory"]!.GetValue<string>()).Equals(expected, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("模块目录不属于当前安装索引。");
        }
        return index;
    }
    private async Task WriteAsync(JsonObject index)
    {
        Directory.CreateDirectory(Root); string value = index.ToJsonString();
        if (Encoding.UTF8.GetByteCount(value) > 1024 * 1024) throw new InvalidDataException("第三方模块配置超过 1 MiB。");
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
    public ThirdPartyModuleRecord Record(string id) => Records().FirstOrDefault(record => record.Id == id) ?? throw new InvalidOperationException("模块已移除，请重新打开。");
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
            throw new InvalidOperationException("此包没有 Windows 原生模块或网页入口。");
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
        if (new FileInfo(source).Length > ArchiveLimit) throw new InvalidDataException("模块 ZIP 超过 256 MiB。");
        string generation = Guid.NewGuid().ToString(), directory = Path.Combine(Root, "packages", generation);
        Directory.CreateDirectory(directory);
        bool published = false;
        try
        {
            using var zip = ZipFile.OpenRead(source);
            if (zip.Entries.Count > 4096) throw new InvalidDataException("模块 ZIP 文件数量超过 4096。");
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase); long declared = 0, actual = 0;
            foreach (var entry in zip.Entries)
            {
                bool folder = entry.FullName.EndsWith('/'); string relative = Relative(folder ? entry.FullName[..^1] : entry.FullName);
                if (!names.Add(relative) || ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000 || entry.Length > EntryLimit ||
                    (declared += entry.Length) > ArchiveLimit) throw new InvalidDataException("模块 ZIP 存在重复路径、链接或过大资源。");
                string destination = Path.Combine(directory, relative);
                if (folder) { Directory.CreateDirectory(destination); continue; }
                Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                await using var input = entry.Open(); await using var output = File.Create(destination);
                byte[] buffer = new byte[65536]; long copied = 0; int count;
                while ((count = await input.ReadAsync(buffer)) > 0)
                {
                    copied += count; actual += count;
                    if (copied > EntryLimit || actual > ArchiveLimit) throw new InvalidDataException("模块 ZIP 解压超过限制。");
                    await output.WriteAsync(buffer.AsMemory(0, count));
                }
                if (copied != entry.Length) throw new InvalidDataException("模块 ZIP 长度不一致。");
            }
            if (!zip.Entries.Any(e => e.FullName == "module.json")) throw new InvalidDataException("ZIP 根目录必须包含 module.json。");
            var manifest = ReadManifest(directory); string id = manifest["id"]!.GetValue<string>();
            JsonObject? installed = null;
            await ChangeAsync((index, modules) =>
            {
                var previous = modules.FirstOrDefault(m => m!["id"]!.GetValue<string>().Equals(id, StringComparison.OrdinalIgnoreCase));
                if (previous is not null && previous["id"]!.GetValue<string>() != id) throw new InvalidDataException("模块 ID 仅大小写不同，不能安全更新。");
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
            if (bytes.Length + read > 1024 * 1024) throw new InvalidDataException("模块桥响应超过限制。");
            await bytes.WriteAsync(responseBuffer.AsMemory(0, read));
        }
        string text = Encoding.UTF8.GetString(bytes.ToArray());
        var result = JsonNode.Parse(text)!;
        if (operation != "status") return result;
        var module = result["modules"]?.AsArray().FirstOrDefault(m => m?["id"]?.GetValue<string>() == id)?.DeepClone();
        return new JsonObject { ["connected"] = result["connected"]?.DeepClone() ?? true, ["module"] = module };
    }
}
