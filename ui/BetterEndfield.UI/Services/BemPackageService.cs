using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BetterEndfield.UI.Services;

internal sealed class BemAppearance
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public override string ToString() => Name;
}

internal sealed class BemOptionChoice
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public override string ToString() => Name;
}

internal sealed class BemOptionGroup
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string Default { get; init; } = "";
    public List<BemOptionChoice> Choices { get; init; } = [];
    public JsonElement? AvailableWhen { get; init; }
}

internal sealed class BemPackage
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string Author { get; init; } = "";
    public string Version { get; init; } = "";
    public string Character { get; init; } = "";
    public string File { get; init; } = "";
    public long Size { get; init; }
    public string DefaultAppearance { get; init; } = "";
    public List<BemAppearance> Appearances { get; init; } = [];
    public bool Enabled { get; set; }
    public string SelectedAppearance { get; set; } = "";
    public bool IsComposable { get; init; }
    public List<BemOptionGroup> OptionGroups { get; init; } = [];
    public List<JsonElement> SelectionConstraints { get; init; } = [];
    public Dictionary<string, string> SelectedOptions { get; } = new(StringComparer.Ordinal);

    public Dictionary<string, string> EffectiveOptions()
    {
        var active = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var group in OptionGroups)
            if (group.AvailableWhen is null || Evaluate(group.AvailableWhen.Value, active))
                active[group.Id] = SelectedOptions[group.Id];
        return active;
    }

    public bool OptionsValid() => SelectionConstraints.All(rule => Evaluate(rule, EffectiveOptions()));

    private static bool Evaluate(JsonElement condition, IReadOnlyDictionary<string, string> active)
    {
        if (condition.ValueKind is JsonValueKind.True or JsonValueKind.False) return condition.GetBoolean();
        if (condition.TryGetProperty("eq", out var eq))
            return active.TryGetValue(eq[0].GetString() ?? "", out string? value) && value == eq[1].GetString();
        if (condition.TryGetProperty("all", out var all)) return all.EnumerateArray().All(child => Evaluate(child, active));
        if (condition.TryGetProperty("any", out var any)) return any.EnumerateArray().Any(child => Evaluate(child, active));
        if (condition.TryGetProperty("not", out var negated)) return !Evaluate(negated, active);
        throw new InvalidDataException("BEM 选项条件不合法。");
    }

    public string EncodedOptions() => string.Join("&", OptionGroups.Select(g => g.Id + ":" + SelectedOptions[g.Id]));

    public void RestoreOptions(string saved)
    {
        var parsed = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (string pair in saved.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = pair.Split(':');
            if (parts.Length != 2 || !parsed.TryAdd(parts[0], parts[1])) throw new InvalidDataException("BEM 选项设置不合法。");
        }
        if (parsed.Keys.Any(id => OptionGroups.All(group => group.Id != id))) throw new InvalidDataException("BEM 选项组已移除。");
        foreach (var group in OptionGroups)
        {
            string choice = parsed.GetValueOrDefault(group.Id, group.Default);
            if (group.Choices.All(item => item.Id != choice)) throw new InvalidDataException("BEM 选项值已移除。");
            SelectedOptions[group.Id] = choice;
        }
        if (!OptionsValid()) throw new InvalidDataException("BEM 选项组合不可达。");
    }
}

internal sealed class BemPackageService
{
    public string Root { get; } = Path.Combine(ConfigurationService.SettingsDirectory, "catalog", "custom-model");
    public List<BemPackage> Packages { get; } = [];
    public List<string> Notices { get; } = [];
    public bool StandaloneLod { get; set; }
    public bool EffectiveLod => StandaloneLod || Packages.Any(p => p.Enabled);
    private readonly SemaphoreSlim _gate = new(1, 1);

    private static string StableId(JsonElement e, string key)
    {
        string value = e.GetProperty(key).GetString() ?? "";
        if (!Regex.IsMatch(value, @"\A[A-Za-z0-9][A-Za-z0-9_.-]{0,95}\z"))
            throw new InvalidDataException($"BEM: 无效的 {key}");
        return value;
    }

    public static BemPackage ReadMetadata(string path)
    {
        using var stream = System.IO.File.OpenRead(path);
        using var r = new BinaryReader(stream, Encoding.UTF8);
        if (!r.ReadBytes(8).SequenceEqual(new byte[] { 66, 69, 77, 0, 80, 75, 71, 0 }))
            throw new InvalidDataException("BEM 包头不合法。");
        ushort major = r.ReadUInt16(), minor = r.ReadUInt16();
        if (major != 1 || minor > 2 || r.ReadUInt32() != 40)
            throw new InvalidDataException("仅支持 BEM 1.0/1.1/1.2 包。");
        ulong fileSize = r.ReadUInt64(), manifestSize = r.ReadUInt64();
        uint count = r.ReadUInt32(), flags = r.ReadUInt32();
        if (fileSize != (ulong)stream.Length || fileSize > 2UL * 1024 * 1024 * 1024 || manifestSize is 0 or > 4194304 || count > (minor >= 2 ? 16384u : 4096u) || flags != 0 || 40 + manifestSize + count * 32UL > fileSize)
            throw new InvalidDataException("BEM 文件长度或目录不合法。");
        using var document = JsonDocument.Parse(r.ReadBytes((int)manifestSize));
        var m = document.RootElement;
        if (m.GetProperty("schema").GetInt32() != 1 || m.GetProperty("target").GetProperty("platform").GetString() != "windows-x64")
            throw new InvalidDataException("不支持的 BEM schema 或目标平台。");
        var appearances = minor == 0 ? m.GetProperty("appearances").EnumerateArray().Select(a => new BemAppearance
        {
            Id = StableId(a, "id"), Name = a.GetProperty("name").GetString() ?? "",
            Description = a.TryGetProperty("description", out var d) ? d.GetString() ?? "" : ""
        }).ToList() : [];
        string def = minor == 0 ? StableId(m, "default_appearance_id") : "";
        if (minor == 0 && (appearances.Count is < 1 or > 64 || appearances.Select(a => a.Id).Distinct().Count() != appearances.Count || !appearances.Any(a => a.Id == def)))
            throw new InvalidDataException("BEM 外观目录不合法。");
        var groups = minor >= 1 ? m.GetProperty("option_groups").EnumerateArray().Select(g => new BemOptionGroup
        {
            Id = StableId(g, "id"), Name = g.GetProperty("name").GetString() ?? "",
            Default = StableId(g, "default"),
            Choices = g.GetProperty("choices").EnumerateArray().Select(c => new BemOptionChoice
            {
                Id = StableId(c, "id"), Name = c.GetProperty("name").GetString() ?? ""
            }).ToList(),
            AvailableWhen = g.TryGetProperty("available_when", out var condition) ? condition.Clone() : null
        }).ToList() : [];
        int maxChoices = minor >= 2 ? 64 : 16;
        if (minor >= 1 && (groups.Count is < 1 or > 64 || groups.Select(g => g.Id).Distinct().Count() != groups.Count ||
            groups.Any(g => g.Choices.Count < 1 || g.Choices.Count > maxChoices || g.Choices.Select(c => c.Id).Distinct().Count() != g.Choices.Count ||
                            g.Choices.All(c => c.Id != g.Default))))
            throw new InvalidDataException("BEM 选项组目录不合法。");
        var package = new BemPackage
        {
            Id = StableId(m, "package_id"), Name = m.GetProperty("name").GetString() ?? "",
            Author = m.GetProperty("author").GetString() ?? "", Version = m.GetProperty("version").GetString() ?? "",
            Character = StableId(m.GetProperty("target"), "character_id"), File = path, Size = stream.Length,
            Appearances = appearances, DefaultAppearance = def, SelectedAppearance = def,
            IsComposable = minor >= 1, OptionGroups = groups,
            SelectionConstraints = minor >= 1 && m.TryGetProperty("selection_constraints", out var constraints)
                ? constraints.EnumerateArray().Select(c => c.Clone()).ToList() : []
        };
        if (minor >= 1) package.RestoreOptions("");
        return package;
    }

    public void Load()
    {
        Packages.Clear(); Notices.Clear(); StandaloneLod = false;
        var settings = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);
        string ini = Path.Combine(Root, "runtime.ini");
        if (System.IO.File.Exists(ini))
        {
            string section = "";
            foreach (string source in System.IO.File.ReadLines(ini))
            {
                string line = source.Trim();
                if (line.StartsWith('[') && line.EndsWith(']')) { section = line[1..^1]; settings.TryAdd(section, []); }
                else if (line.Contains('=') && settings.TryGetValue(section, out var values))
                { int e = line.IndexOf('='); values[line[..e].Trim()] = line[(e + 1)..].Trim(); }
            }
        }
        if (settings.TryGetValue("CustomModel", out var common)) StandaloneLod = common.GetValueOrDefault("standalone_lod") is "true" or "1";
        string dir = Path.Combine(Root, "packages");
        if (!Directory.Exists(dir)) return;
        foreach (string path in Directory.EnumerateFiles(dir, "*.bem").Order())
        {
            try
            {
                var p = ReadMetadata(path);
                if (Packages.Any(x => x.Id == p.Id)) throw new InvalidDataException("重复包 ID");
                if (settings.TryGetValue("Mod." + p.Id, out var state))
                {
                    p.Enabled = state.GetValueOrDefault("enabled") is "true" or "1";
                    if (p.IsComposable)
                    {
                        try { p.RestoreOptions(state.GetValueOrDefault("options", "")); }
                        catch (InvalidDataException) { p.RestoreOptions(""); Notices.Add($"{p.Name}：原选项已移除或不可达，回退默认组合。"); }
                    }
                    else
                    {
                        string selected = state.GetValueOrDefault("appearance", p.DefaultAppearance);
                        if (p.Appearances.Any(a => a.Id == selected)) p.SelectedAppearance = selected;
                        else Notices.Add($"{p.Name}：原外观已移除，回退到默认外观。");
                    }
                }
                Packages.Add(p);
            }
            catch (Exception ex) when (ex is IOException or JsonException or InvalidOperationException or KeyNotFoundException)
            { Notices.Add($"{Path.GetFileName(path)}：{ex.Message}"); }
        }
        foreach (var group in Packages.Where(p => p.Enabled).GroupBy(p => p.Character).Where(g => g.Count() > 1))
        {
            foreach (var p in group) p.Enabled = false;
            Notices.Add($"{group.Key}：存在多个启用包，已在界面停用，请重新选择。");
        }
    }

    public async Task SaveAsync()
    {
        await _gate.WaitAsync();
        try
        {
            Directory.CreateDirectory(Root);
            var text = new StringBuilder("[CustomModel]\nstandalone_lod=").Append(StandaloneLod ? "true" : "false").Append('\n');
            foreach (var p in Packages)
                text.Append("\n[Mod.").Append(p.Id).Append("]\nenabled=").Append(p.Enabled ? "true" : "false")
                    .Append("\npackage=packages/").Append(Path.GetFileName(p.File))
                    .Append(p.IsComposable ? "\noptions=" + p.EncodedOptions() : "\nappearance=" + p.SelectedAppearance).Append('\n');
            if (Encoding.UTF8.GetByteCount(text.ToString()) > 1024 * 1024) throw new InvalidOperationException("包管理配置超过运行时 1 MiB 上限。");
            string temp = Path.Combine(Root, Guid.NewGuid() + ".tmp");
            try { await System.IO.File.WriteAllTextAsync(temp, text.ToString(), new UTF8Encoding(false)); System.IO.File.Move(temp, Path.Combine(Root, "runtime.ini"), true); }
            finally { if (System.IO.File.Exists(temp)) System.IO.File.Delete(temp); }
        }
        finally { _gate.Release(); }
    }

    private static void RequireGameClosed()
    {
        var processes = Process.GetProcessesByName("Endfield");
        try { if (processes.Length > 0) throw new InvalidOperationException("请关闭游戏后导入、更新或删除包，避免资源延迟加载时读到变更文件。启停与外观选择可先保存，下次启动生效。"); }
        finally { foreach (var p in processes) p.Dispose(); }
    }

    public async Task<BemBundleImport> PrepareBundleAsync(string source, string installRoot, CancellationToken token = default)
    {
        RequireGameClosed();
        string staging = Path.Combine(Path.GetTempPath(), "BemBundle-" + Guid.NewGuid());
        var result = new BemBundleImport(staging);
        try
        {
            string raw = await BemToolService.RunAsync(installRoot, ["unpack", source, "-o", staging], token);
            using var json = JsonDocument.Parse(raw);
            if (json.RootElement.GetProperty("format").GetString() != "BEM-ZIP") throw new InvalidDataException("请选择包含 BEM 模型包的 ZIP。");
            foreach (var item in json.RootElement.GetProperty("packages").EnumerateArray())
            {
                string name = item.GetProperty("file").GetString() ?? "";
                if (Path.GetFileName(name) != name || !name.EndsWith(".bem", StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("转换器返回无效文件名。");
                result.Packages.Add(ReadMetadata(Path.Combine(staging, name)));
            }
            foreach (var issue in json.RootElement.GetProperty("issues").EnumerateArray())
                result.Issues.Add($"{issue.GetProperty("entry").GetString()}：{issue.GetProperty("message").GetString()}");
            return result;
        }
        catch { result.Dispose(); throw; }
    }

    public async Task ImportAsync(string source, string installRoot, CancellationToken token = default)
    {
        RequireGameClosed();
        if (new FileInfo(source).Length > 2L * 1024 * 1024 * 1024) throw new InvalidDataException("BEM 包超过 2 GiB 上限。");
        string dir = Path.Combine(Root, "packages"); Directory.CreateDirectory(dir);
        string temp = Path.Combine(dir, Guid.NewGuid() + ".tmp");
        try
        {
            await using (var input = System.IO.File.OpenRead(source))
            await using (var output = System.IO.File.Create(temp)) await input.CopyToAsync(output, token);
            // Validate the exact staged bytes before exposing them to the library/runtime.
            await BemToolService.RunAsync(installRoot, ["validate", temp], token);
            var p = ReadMetadata(temp);
            var old = Packages.FirstOrDefault(x => x.Id == p.Id);
            var caseCollision = Packages.FirstOrDefault(x => x.Id.Equals(p.Id, StringComparison.OrdinalIgnoreCase) && x.Id != p.Id);
            if (caseCollision != null) throw new InvalidDataException("包 ID 与现有包仅大小写不同，不能安全存储。");
            if (old != null && old.Character != p.Character) throw new InvalidDataException("同一个包 ID 不能更新为另一角色。");
            RequireGameClosed();
            System.IO.File.Move(temp, Path.Combine(dir, p.Id + ".bem"), true);
            Load(); await SaveAsync();
        }
        finally { if (System.IO.File.Exists(temp)) System.IO.File.Delete(temp); }
    }

    public async Task RemoveAsync(BemPackage package)
    {
        RequireGameClosed();
        System.IO.File.Delete(package.File); Packages.Remove(package); await SaveAsync();
    }

    public async Task SetEnabledAsync(BemPackage package, bool enabled)
    {
        if (enabled)
            foreach (var other in Packages.Where(p => p.Character == package.Character)) other.Enabled = false;
        package.Enabled = enabled;
        await SaveAsync();
    }
}

internal sealed class BemBundleImport(string directory) : IDisposable
{
    public List<BemPackage> Packages { get; } = [];
    public List<string> Issues { get; } = [];
    public void Dispose()
    {
        // This directory is allocated internally with a fresh GUID, never from ZIP paths.
        if (Directory.Exists(directory)) Directory.Delete(directory, true);
    }
}

internal static class BemToolService
{
    public static async Task<string> RunAsync(string installRoot, IEnumerable<string> args, CancellationToken token = default)
    {
        string tool = Path.Combine(installRoot, "tools", "BemConverter", "BetterEndfield.BemConverter.exe");
        if (!System.IO.File.Exists(tool)) tool = Path.Combine((Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory), "tools", "BemConverter", "BetterEndfield.BemConverter.exe");
        if (!System.IO.File.Exists(tool)) throw new FileNotFoundException("缺少随软件提供的 BEM 转换工具。请使用包含 tools/BemConverter 的完整构建；开发环境运行 BuildBemTools.ps1。");
        var info = new ProcessStartInfo(tool) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true, StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8 };
        foreach (string arg in args) info.ArgumentList.Add(arg);
        using var process = Process.Start(info) ?? throw new InvalidOperationException("无法启动转换工具。");
        using var cancel = token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(token), stderr = process.StandardError.ReadToEndAsync(token);
        await process.WaitForExitAsync(token);
        string report = await stdout, errors = await stderr;
        if (process.ExitCode != 0) throw new InvalidDataException(string.IsNullOrWhiteSpace(report) ? errors : report);
        return report;
    }
}
