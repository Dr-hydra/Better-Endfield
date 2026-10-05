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

// Wire values use fixed ticks so both platforms serialize exactly the same weight.
internal sealed class BemParameterGroup
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public uint Min { get; init; }
    public uint Max { get; init; }
    public uint Default { get; init; }
    public uint Neutral { get; init; }
    public uint Step { get; init; }
    public JsonElement? AvailableWhen { get; init; }
    public bool Accepts(uint value) => value >= Min && value <= Max && (value - Min) % Step == 0;
    public uint Snap(double value) => (uint)Math.Clamp(Min + Math.Round((value - Min) / Step) * Step, Min, Max);
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
    public List<BemParameterGroup> Parameters { get; init; } = [];
    public Dictionary<string, uint> SelectedParameters { get; } = new(StringComparer.Ordinal);
    // Kept separately from runtime selection, so downgrading then upgrading a package
    // does not lose sliders which are temporarily absent from its metadata.
    private readonly Dictionary<string, uint> _rememberedParameters = new(StringComparer.Ordinal);

    public bool ParameterAvailable(BemParameterGroup parameter) => parameter.AvailableWhen is null || Evaluate(parameter.AvailableWhen.Value, EffectiveOptions());
    public string EncodedParameters() => string.Join("&", Parameters.Select(p => p.Id + ":" + SelectedParameters[p.Id]));
    public string RememberedParameters()
    {
        foreach (var item in SelectedParameters) _rememberedParameters[item.Key] = item.Value;
        return string.Join("&", _rememberedParameters.Select(p => p.Key + ":" + p.Value));
    }
    public bool RestoreParameters(string saved)
    {
        _rememberedParameters.Clear(); SelectedParameters.Clear();
        foreach (string pair in saved.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = pair.Split(':');
            if (parts.Length != 2 || !Regex.IsMatch(parts[0], @"\A[A-Za-z0-9][A-Za-z0-9_.-]{0,95}\z") ||
                !Regex.IsMatch(parts[1], @"\A[0-9]+\z") || !uint.TryParse(parts[1], out uint tick) || tick > 1000 ||
                !_rememberedParameters.TryAdd(parts[0], tick)) throw new InvalidDataException(BemText.Get("BEM 滑条设置不合法。"));
        }
        bool repaired = false;
        foreach (var parameter in Parameters)
        {
            uint value = _rememberedParameters.GetValueOrDefault(parameter.Id, parameter.Default);
            if (!parameter.Accepts(value)) { value = parameter.Default; repaired = true; }
            SelectedParameters[parameter.Id] = value;
        }
        return repaired;
    }

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
        throw new InvalidDataException(BemText.Get("BEM 选项条件不合法。"));
    }

    public string EncodedOptions() => string.Join("&", OptionGroups.Select(g => g.Id + ":" + SelectedOptions[g.Id]));

    public void RestoreOptions(string saved)
    {
        var parsed = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (string pair in saved.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = pair.Split(':');
            if (parts.Length != 2 || !parsed.TryAdd(parts[0], parts[1])) throw new InvalidDataException(BemText.Get("BEM 选项设置不合法。"));
        }
        if (parsed.Keys.Any(id => OptionGroups.All(group => group.Id != id))) throw new InvalidDataException(BemText.Get("BEM 选项组已移除。"));
        foreach (var group in OptionGroups)
        {
            string choice = parsed.GetValueOrDefault(group.Id, group.Default);
            if (group.Choices.All(item => item.Id != choice)) throw new InvalidDataException(BemText.Get("BEM 选项值已移除。"));
            SelectedOptions[group.Id] = choice;
        }
        if (!OptionsValid()) throw new InvalidDataException(BemText.Get("BEM 选项组合不可达。"));
    }
}

internal sealed class BemPackageService
{
    public string Root { get; } = Path.Combine(ConfigurationService.SettingsDirectory, "catalog", "custom-model");
    private string LegacyPackageDirectory => Path.Combine(Root, "packages");
    // Packages live beside the program when its directory is writable; settings stay in the profile.
    public string PackageDirectory { get; private set; } = Path.Combine(ConfigurationService.SettingsDirectory, "catalog", "custom-model", "packages");
    public List<BemPackage> Packages { get; } = [];
    private readonly List<Func<string>> _notices = [];
    public IReadOnlyList<string> Notices => _notices.Select(notice => notice()).ToArray();
    public bool StandaloneLod { get; set; }
    public bool SkipValidation { get; set; }
    public bool HotSwitch { get; set; }
    public bool FastLoading { get; set; }
    public bool ModelOverlayEnabled { get; set; } = true;
    public bool ModelOverlayVisible { get; set; }
    public string ModelOverlayHotkey { get; set; } = "PLUS";
    public bool EffectiveLod => StandaloneLod || Packages.Any(p => p.Enabled);
    public bool IsSaving => _gate.CurrentCount == 0;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private Dictionary<string, Dictionary<string, string>> _settings = new(StringComparer.Ordinal);
    private Dictionary<string, Dictionary<string, string>> _baseline = new(StringComparer.Ordinal);
    private (long Ticks, long Size) _settingsStamp;

    private (long Ticks, long Size) SettingsStamp()
    {
        var file = new FileInfo(Path.Combine(Root, "runtime.ini"));
        return file.Exists ? (file.LastWriteTimeUtc.Ticks, file.Length) : (0, 0);
    }
    public bool HasExternalChanges() => SettingsStamp() != _settingsStamp;

    private void ApplyCommonSettings(Dictionary<string, Dictionary<string, string>> settings)
    {
        settings.TryGetValue("CustomModel", out var common);
        StandaloneLod = common?.GetValueOrDefault("standalone_lod") is "true" or "1";
        SkipValidation = common?.GetValueOrDefault("skip_validation") is "true" or "1";
        HotSwitch = common?.GetValueOrDefault("hot_switch") is "true" or "1";
        FastLoading = common?.GetValueOrDefault("fast_loading") is "true" or "1";
        ModelOverlayEnabled = common?.GetValueOrDefault("overlay_enabled", "true") is "true" or "1" || common == null;
        ModelOverlayVisible = common?.GetValueOrDefault("overlay_visible") is "true" or "1";
        ModelOverlayHotkey = HotkeyService.TryNormalize(common?.GetValueOrDefault("overlay_hotkey", "PLUS"), out string hotkey)
            ? hotkey : "PLUS";
    }

    private static string StableId(JsonElement e, string key)
    {
        string value = e.GetProperty(key).GetString() ?? "";
        if (!Regex.IsMatch(value, @"\A[A-Za-z0-9][A-Za-z0-9_.-]{0,95}\z"))
            throw new InvalidDataException(BemText.Format("BEM: 无效的 {0}", key));
        return value;
    }

    public static BemPackage ReadMetadata(string path, bool skipValidation = false)
    {
        using var stream = System.IO.File.OpenRead(path);
        using var r = new BinaryReader(stream, Encoding.UTF8);
        if (!r.ReadBytes(8).SequenceEqual(new byte[] { 66, 69, 77, 0, 80, 75, 71, 0 }))
            throw new InvalidDataException(BemText.Get("BEM 包头不合法。"));
        ushort major = r.ReadUInt16(), minor = r.ReadUInt16();
        if (major != 1 || minor > 3 || r.ReadUInt32() != 40)
            throw new InvalidDataException(BemText.Get("仅支持 BEM 1.0/1.1/1.2/1.3 包。"));
        ulong fileSize = r.ReadUInt64(), manifestSize = r.ReadUInt64();
        uint count = r.ReadUInt32(), flags = r.ReadUInt32();
        if (fileSize != (ulong)stream.Length || manifestSize == 0 || manifestSize > int.MaxValue || flags != 0 ||
            manifestSize > fileSize - 40 || count > (fileSize - 40 - manifestSize) / 32 ||
            (!skipValidation && (fileSize > 2UL * 1024 * 1024 * 1024 || manifestSize > 4194304 || count > (minor >= 2 ? 16384u : 4096u))))
            throw new InvalidDataException(BemText.Get("BEM 文件长度或目录不合法。"));
        using var document = JsonDocument.Parse(r.ReadBytes((int)manifestSize));
        var m = document.RootElement;
        if (m.GetProperty("schema").GetInt32() != 1 || (!skipValidation && m.GetProperty("target").GetProperty("platform").GetString() != "windows-x64"))
            throw new InvalidDataException(BemText.Get("不支持的 BEM schema 或目标平台。"));
        var appearances = minor == 0 ? m.GetProperty("appearances").EnumerateArray().Select(a => new BemAppearance
        {
            Id = StableId(a, "id"), Name = a.GetProperty("name").GetString() ?? "",
            Description = a.TryGetProperty("description", out var d) ? d.GetString() ?? "" : ""
        }).ToList() : [];
        string def = minor == 0 ? StableId(m, "default_appearance_id") : "";
        if (minor == 0 && (appearances.Count < 1 || (!skipValidation && appearances.Count > 64) || appearances.Select(a => a.Id).Distinct().Count() != appearances.Count || !appearances.Any(a => a.Id == def)))
            throw new InvalidDataException(BemText.Get("BEM 外观目录不合法。"));
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
        if (minor >= 1 && ((minor < 3 && groups.Count < 1) || (!skipValidation && groups.Count > 64) || groups.Select(g => g.Id).Distinct().Count() != groups.Count ||
            groups.Any(g => g.Choices.Count < 1 || (!skipValidation && g.Choices.Count > maxChoices) || g.Choices.Select(c => c.Id).Distinct().Count() != g.Choices.Count ||
                            g.Choices.All(c => c.Id != g.Default))))
            throw new InvalidDataException(BemText.Get("BEM 选项组目录不合法。"));
        var parameters = minor >= 3 ? m.GetProperty("parameters").EnumerateArray().Select(p => new BemParameterGroup
        {
            Id = StableId(p, "id"), Name = p.GetProperty("name").GetString() ?? "",
            Min = p.GetProperty("min").GetUInt32(), Max = p.GetProperty("max").GetUInt32(),
            Default = p.GetProperty("default").GetUInt32(), Neutral = p.GetProperty("neutral").GetUInt32(),
            Step = p.GetProperty("step").GetUInt32(),
            AvailableWhen = p.TryGetProperty("available_when", out var condition) ? condition.Clone() : null
        }).ToList() : [];
        if ((!skipValidation && parameters.Count > 64) || parameters.Select(p => p.Id).Distinct().Count() != parameters.Count ||
            parameters.Any(p => p.Min > p.Max || p.Max > 1000 || p.Step == 0 || p.Step > 1000 ||
                (p.Max - p.Min) % p.Step != 0 || !p.Accepts(p.Default) || !p.Accepts(p.Neutral)))
            throw new InvalidDataException(BemText.Get("BEM 滑条目录不合法。"));
        var package = new BemPackage
        {
            Id = StableId(m, "package_id"), Name = m.GetProperty("name").GetString() ?? "",
            Author = m.GetProperty("author").GetString() ?? "", Version = m.GetProperty("version").GetString() ?? "",
            Character = StableId(m.GetProperty("target"), "character_id"), File = path, Size = stream.Length,
            Appearances = appearances, DefaultAppearance = def, SelectedAppearance = def,
            IsComposable = minor >= 1, OptionGroups = groups, Parameters = parameters,
            SelectionConstraints = minor >= 1 && m.TryGetProperty("selection_constraints", out var constraints)
                ? constraints.EnumerateArray().Select(c => c.Clone()).ToList() : []
        };
        if (minor >= 1) package.RestoreOptions("");
        package.RestoreParameters("");
        return package;
    }

    public void UseInstallRoot(string? installRoot)
    {
        string? candidate = string.IsNullOrEmpty(installRoot) ? null : Path.Combine(installRoot, "models");
        PackageDirectory = candidate != null && CanWrite(candidate) ? Path.GetFullPath(candidate) : LegacyPackageDirectory;
    }

    private static bool CanWrite(string directory)
    {
        try
        {
            Directory.CreateDirectory(directory);
            string probe = Path.Combine(directory, Guid.NewGuid() + ".tmp");
            System.IO.File.WriteAllBytes(probe, []); System.IO.File.Delete(probe);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { return false; }
    }

    private bool UsesLegacyDirectory => SameDirectory(PackageDirectory, LegacyPackageDirectory);
    private static bool SameDirectory(string a, string b) =>
        string.Equals(Path.GetFullPath(a).TrimEnd('\\'), Path.GetFullPath(b).TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);

    public bool HasLegacyPackages => !UsesLegacyDirectory && Directory.Exists(LegacyPackageDirectory)
        && Directory.EnumerateFiles(LegacyPackageDirectory, "*.bem").Any();

    /// <summary>Moves profile-stored packages beside the program. Skipped while the game may read them.</summary>
    public async Task<int> MigrateLegacyPackagesAsync(CancellationToken token = default)
    {
        if (!HasLegacyPackages) return 0;
        RequireGameClosed();
        int moved = 0;
        await Task.Run(() =>
        {
            Directory.CreateDirectory(PackageDirectory);
            foreach (string source in Directory.EnumerateFiles(LegacyPackageDirectory, "*.bem").ToArray())
            {
                token.ThrowIfCancellationRequested();
                string target = Path.Combine(PackageDirectory, Path.GetFileName(source));
                if (System.IO.File.Exists(target)) continue;
                string temp = Path.Combine(PackageDirectory, Guid.NewGuid() + ".tmp");
                try
                {
                    System.IO.File.Copy(source, temp);
                    if (new FileInfo(temp).Length != new FileInfo(source).Length) throw new IOException(BemText.Get("模型包迁移校验失败。"));
                    System.IO.File.Move(temp, target);
                    System.IO.File.Delete(source); moved++;
                }
                finally { if (System.IO.File.Exists(temp)) System.IO.File.Delete(temp); }
            }
        }, token);
        Load(); await SaveAsync();
        return moved;
    }

    public void Load()
    {
        Packages.Clear(); _notices.Clear(); StandaloneLod = false; SkipValidation = false;
        HotSwitch = false; FastLoading = false;
        var stamp = SettingsStamp();
        var settings = _settings = BemRuntimeSettings.Read(Path.Combine(Root, "runtime.ini"));
        ApplyCommonSettings(settings);
        var directories = new List<string> { PackageDirectory };
        if (!UsesLegacyDirectory) directories.Add(LegacyPackageDirectory);
        var files = directories.Where(Directory.Exists).SelectMany(dir => Directory.EnumerateFiles(dir, "*.bem").Order()).ToArray();
        foreach (string path in files)
        {
            try
            {
                var p = ReadMetadata(path, SkipValidation);
                // A copy not yet migrated from the profile yields to the one beside the program.
                bool legacy = !UsesLegacyDirectory && SameDirectory(Path.GetDirectoryName(path)!, LegacyPackageDirectory);
                if (legacy && Packages.Any(x => x.Id == p.Id)) continue;
                if (Packages.Any(x => x.Id == p.Id)) throw new InvalidDataException(BemText.Get("重复包 ID"));
                if (settings.TryGetValue("Mod." + p.Id, out var state))
                {
                    p.Enabled = state.GetValueOrDefault("enabled") is "true" or "1";
                    try
                    {
                        if (p.RestoreParameters(state.GetValueOrDefault("parameters_saved", state.GetValueOrDefault("parameters", ""))))
                            _notices.Add(() => BemText.Format("{0}：部分滑条范围已变化，回退作者默认值。", p.Name));
                    }
                    catch (InvalidDataException) { p.RestoreParameters(""); _notices.Add(() => BemText.Format("{0}：滑条设置损坏，回退作者默认值。", p.Name)); }
                    if (p.IsComposable)
                    {
                        try { p.RestoreOptions(state.GetValueOrDefault("options", "")); }
                        catch (InvalidDataException) { p.RestoreOptions(""); _notices.Add(() => BemText.Format("{0}：原选项已移除或不可达，回退默认组合。", p.Name)); }
                    }
                    else
                    {
                        string selected = state.GetValueOrDefault("appearance", p.DefaultAppearance);
                        if (p.Appearances.Any(a => a.Id == selected)) p.SelectedAppearance = selected;
                        else _notices.Add(() => BemText.Format("{0}：原外观已移除，回退到默认外观。", p.Name));
                    }
                }
                Packages.Add(p);
            }
            catch (Exception ex) when (ex is IOException or InvalidDataException or JsonException or InvalidOperationException or KeyNotFoundException or OverflowException or FormatException)
            { _notices.Add(() => Path.GetFileName(path) + BemText.Colon + ex.Message); }
        }
        foreach (var group in Packages.Where(p => p.Enabled).GroupBy(p => p.Character).Where(g => g.Count() > 1))
        {
            foreach (var p in group) p.Enabled = false;
            _notices.Add(() => BemText.Format("{0}：存在多个启用包，已在界面停用，请重新选择。", group.Key));
        }
        _baseline = BuildDesiredSettings();
        _settingsStamp = stamp;
    }

    private Dictionary<string, Dictionary<string, string>> BuildDesiredSettings()
    {
        var settings = BemRuntimeSettings.Clone(_settings);
        if (!settings.TryGetValue("CustomModel", out var common)) settings["CustomModel"] = common = new(StringComparer.Ordinal);
        common["standalone_lod"] = StandaloneLod ? "true" : "false";
        common["skip_validation"] = SkipValidation ? "true" : "false";
        common["hot_switch"] = HotSwitch ? "true" : "false";
        common["fast_loading"] = FastLoading ? "true" : "false";
        common["overlay_enabled"] = ModelOverlayEnabled ? "true" : "false";
        common["overlay_visible"] = ModelOverlayVisible ? "true" : "false";
        common["overlay_hotkey"] = HotkeyService.TryNormalize(ModelOverlayHotkey, out string hotkey) ? hotkey : "PLUS";
        common.Remove("loading_optimization");
        foreach (var p in Packages)
        {
            if (!settings.TryGetValue("Mod." + p.Id, out var values)) settings["Mod." + p.Id] = values = new(StringComparer.Ordinal);
            values["enabled"] = p.Enabled ? "true" : "false";
            values["package"] = SameDirectory(Path.GetDirectoryName(p.File)!, LegacyPackageDirectory)
                ? "packages/" + Path.GetFileName(p.File) : Path.GetFullPath(p.File);
            values[p.IsComposable ? "options" : "appearance"] = p.IsComposable ? p.EncodedOptions() : p.SelectedAppearance;
            values["parameters"] = p.EncodedParameters();
            values["parameters_saved"] = p.RememberedParameters();
        }
        return settings;
    }

    public Task SaveAsync() => SaveCoreAsync(null);

    private async Task SaveCoreAsync(IReadOnlySet<string>? forcedEnabledIds, bool disableAll = false)
    {
        await _gate.WaitAsync();
        try
        {
            var desired = BuildDesiredSettings();
            var baseline = BemRuntimeSettings.Clone(_baseline);
            var disk = BemRuntimeSettings.Clone(_settings);
            var committed = await Task.Run(() => BemRuntimeSettings.Commit(Path.Combine(Root, "runtime.ini"), baseline, desired, forcedEnabledIds, disk, disableAll));
            var advanced = BuildDesiredSettings();
            _settings = committed.Settings;
            ApplyCommonSettings(_settings);
            foreach (var p in Packages)
            {
                if (!_settings.TryGetValue("Mod." + p.Id, out var state)) continue;
                p.Enabled = state.GetValueOrDefault("enabled") is "true" or "1";
                if (p.IsComposable) p.RestoreOptions(state.GetValueOrDefault("options", ""));
                else p.SelectedAppearance = state.GetValueOrDefault("appearance", p.DefaultAppearance);
                p.RestoreParameters(state.GetValueOrDefault("parameters_saved", state.GetValueOrDefault("parameters", "")));
            }
            _baseline = BuildDesiredSettings();
            RestoreNewerEdits(advanced, desired);
            _settingsStamp = committed.Stamp;
        }
        finally { _gate.Release(); }
    }

    // UI callbacks may queue another edit while the file transaction is running.
    // Restore that edit after recording the acknowledged disk snapshot.
    private void RestoreNewerEdits(Dictionary<string, Dictionary<string, string>> advanced,
        Dictionary<string, Dictionary<string, string>> submitted)
    {
        bool Changed(string section, string key) => advanced.TryGetValue(section, out var values)
            && submitted.TryGetValue(section, out var old) && values.GetValueOrDefault(key) != old.GetValueOrDefault(key);
        var common = advanced["CustomModel"];
        if (Changed("CustomModel", "standalone_lod")) StandaloneLod = common["standalone_lod"] == "true";
        if (Changed("CustomModel", "skip_validation")) SkipValidation = common["skip_validation"] == "true";
        if (Changed("CustomModel", "hot_switch")) HotSwitch = common["hot_switch"] == "true";
        if (Changed("CustomModel", "fast_loading")) FastLoading = common["fast_loading"] == "true";
        if (Changed("CustomModel", "overlay_enabled")) ModelOverlayEnabled = common["overlay_enabled"] == "true";
        if (Changed("CustomModel", "overlay_visible")) ModelOverlayVisible = common["overlay_visible"] == "true";
        if (Changed("CustomModel", "overlay_hotkey")) ModelOverlayHotkey = common["overlay_hotkey"];
        foreach (var p in Packages)
        {
            string section = "Mod." + p.Id;
            if (!advanced.TryGetValue(section, out var state)) continue;
            if (Changed(section, "enabled")) p.Enabled = state["enabled"] == "true";
            if (p.IsComposable && Changed(section, "options")) p.RestoreOptions(state["options"]);
            if (!p.IsComposable && Changed(section, "appearance")) p.SelectedAppearance = state["appearance"];
            if (Changed(section, "parameters_saved")) p.RestoreParameters(state["parameters_saved"]);
        }
    }

    private static void RequireGameClosed()
    {
        var processes = Process.GetProcessesByName("Endfield");
        try { if (processes.Length > 0) throw new InvalidOperationException(BemText.Get("请关闭游戏后导入、更新或删除包，避免资源延迟加载时读到变更文件。启停与外观选择可先保存，下次启动生效。")); }
        finally { foreach (var p in processes) p.Dispose(); }
    }

    public async Task<BemBundleImport> PrepareBundleAsync(string source, string installRoot, CancellationToken token = default)
    {
        RequireGameClosed();
        string staging = Path.Combine(Path.GetTempPath(), "BemBundle-" + Guid.NewGuid());
        var result = new BemBundleImport(staging);
        try
        {
            if (SkipValidation)
            {
                Directory.CreateDirectory(staging);
                using var archive = System.IO.Compression.ZipFile.OpenRead(source);
                foreach (var entry in archive.Entries.Where(e => e.FullName.EndsWith(".bem", StringComparison.OrdinalIgnoreCase)))
                {
                    token.ThrowIfCancellationRequested();
                    string path = Path.Combine(staging, Guid.NewGuid() + ".bem");
                    try
                    {
                        if (entry.Length > 2L * 1024 * 1024 * 1024) throw new InvalidDataException(BemText.Get("ZIP 内模型包超过导入文件上限。"));
                        await using (var input = entry.Open())
                        await using (var output = System.IO.File.Create(path)) await input.CopyToAsync(output, token);
                        result.Packages.Add(ReadMetadata(path, true));
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException) { result.Issues.Add($"{entry.FullName}：{ex.Message}"); }
                }
                return result;
            }
            string raw = await BemToolService.RunAsync(installRoot, ["unpack", source, "-o", staging], token);
            using var json = JsonDocument.Parse(raw);
            if (json.RootElement.GetProperty("format").GetString() != "BEM-ZIP") throw new InvalidDataException(BemText.Get("请选择包含 BEM 模型包的 ZIP。"));
            foreach (var item in json.RootElement.GetProperty("packages").EnumerateArray())
            {
                string name = item.GetProperty("file").GetString() ?? "";
                if (Path.GetFileName(name) != name || !name.EndsWith(".bem", StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException(BemText.Get("转换器返回无效文件名。"));
                result.Packages.Add(ReadMetadata(Path.Combine(staging, name), SkipValidation));
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
        if (new FileInfo(source).Length > 2L * 1024 * 1024 * 1024) throw new InvalidDataException(BemText.Get("BEM 包超过 2 GiB 上限。"));
        string dir = PackageDirectory; Directory.CreateDirectory(dir);
        string temp = Path.Combine(dir, Guid.NewGuid() + ".tmp");
        try
        {
            await using (var input = System.IO.File.OpenRead(source))
            await using (var output = System.IO.File.Create(temp)) await input.CopyToAsync(output, token);
            // Validate the exact staged bytes before exposing them to the library/runtime.
            if (!SkipValidation) await BemToolService.RunAsync(installRoot, ["validate", temp], token);
            var p = ReadMetadata(temp, SkipValidation);
            var old = Packages.FirstOrDefault(x => x.Id == p.Id);
            var caseCollision = Packages.FirstOrDefault(x => x.Id.Equals(p.Id, StringComparison.OrdinalIgnoreCase) && x.Id != p.Id);
            if (caseCollision != null) throw new InvalidDataException(BemText.Get("包 ID 与现有包仅大小写不同，不能安全存储。"));
            if (old != null && old.Character != p.Character) throw new InvalidDataException(BemText.Get("同一个包 ID 不能更新为另一角色。"));
            RequireGameClosed();
            System.IO.File.Move(temp, Path.Combine(dir, p.Id + ".bem"), true);
            if (old != null && !SameDirectory(Path.GetDirectoryName(old.File)!, dir)) System.IO.File.Delete(old.File);
            Load(); await SaveAsync();
        }
        finally { if (System.IO.File.Exists(temp)) System.IO.File.Delete(temp); }
    }

    public async Task RemoveAsync(BemPackage package)
    {
        RequireGameClosed();
        System.IO.File.Delete(package.File); Packages.Remove(package); _settings.Remove("Mod." + package.Id); await SaveAsync();
    }

    public async Task SetEnabledAsync(BemPackage package, bool enabled)
    {
        if (enabled)
            foreach (var other in Packages.Where(p => p.Character == package.Character)) other.Enabled = false;
        package.Enabled = enabled;
        await SaveCoreAsync((enabled ? Packages.Where(p => p.Character == package.Character) : new[] { package }).Select(p => p.Id).ToHashSet(StringComparer.Ordinal));
    }

    public async Task DisableAllAsync()
    {
        foreach (var package in Packages) package.Enabled = false;
        await SaveCoreAsync(Packages.Select(p => p.Id).ToHashSet(StringComparer.Ordinal), disableAll: true);
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
        if (!System.IO.File.Exists(tool)) throw new FileNotFoundException(BemText.Get("缺少随软件提供的 BEM 转换工具。请使用包含 tools/BemConverter 的完整构建；开发环境运行 BuildBemTools.ps1。"));
        var info = new ProcessStartInfo(tool) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true, StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8 };
        foreach (string arg in args) info.ArgumentList.Add(arg);
        using var process = Process.Start(info) ?? throw new InvalidOperationException(BemText.Get("无法启动转换工具。"));
        using var cancel = token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(token), stderr = process.StandardError.ReadToEndAsync(token);
        await process.WaitForExitAsync(token);
        string report = await stdout, errors = await stderr;
        if (process.ExitCode != 0) throw new InvalidDataException(string.IsNullOrWhiteSpace(report) ? errors : report);
        return report;
    }
}
