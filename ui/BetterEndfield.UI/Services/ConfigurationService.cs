using System.Globalization;
using System.Text;
using System.Text.Json;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

internal static class ConfigurationService
{
    public const string NativeConfigurationFileName = "BetterEndfield.ini";
    public const string LogFileName = "BetterEndfield.log";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static readonly SemaphoreSlim NativeConfigurationWriteLock = new(1, 1);

    public static string SettingsDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "BetterEndfield");

    public static string SettingsPath { get; } =
        Path.Combine(SettingsDirectory, "ui-settings.json");

    public static AppSettings LoadAppSettings()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(
                    json,
                    JsonOptions);
                if (settings is not null)
                {
                    return settings;
                }
            }
        }
        catch (IOException)
        {
        }
        catch (JsonException)
        {
        }

        return new AppSettings();
    }

    public static async Task<AppSettings> LoadAppSettingsAsync()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                await using FileStream stream = File.OpenRead(SettingsPath);
                AppSettings? settings = await JsonSerializer.DeserializeAsync<AppSettings>(
                    stream,
                    JsonOptions);
                if (settings is not null)
                {
                    return settings;
                }
            }
        }
        catch (IOException)
        {
        }
        catch (JsonException)
        {
        }

        return new AppSettings();
    }

    public static async Task SaveAppSettingsAsync(AppSettings settings)
    {
        Directory.CreateDirectory(SettingsDirectory);
        await using FileStream stream = File.Create(SettingsPath);
        await JsonSerializer.SerializeAsync(stream, settings, JsonOptions);
    }

    /// <summary>
    /// 显示增强的设置单独存放：它是部署期选项，由 UI 投影到客户端目录的
    /// OptiScaler.ini，不经 BetterEndfield.ini，也不会被 Host 或任何模块读取。
    /// </summary>
    public static string DisplaySettingsPath { get; } =
        Path.Combine(SettingsDirectory, "display-settings.json");

    public static async Task<DisplayConfiguration> LoadDisplayConfigurationAsync()
    {
        try
        {
            if (File.Exists(DisplaySettingsPath))
            {
                await using FileStream stream = File.OpenRead(DisplaySettingsPath);
                DisplayConfiguration? configuration =
                    await JsonSerializer.DeserializeAsync<DisplayConfiguration>(
                        stream,
                        JsonOptions);
                if (configuration is not null)
                {
                    return configuration;
                }
            }
        }
        catch (IOException)
        {
        }
        catch (JsonException)
        {
        }

        return new DisplayConfiguration();
    }

    public static async Task SaveDisplayConfigurationAsync(DisplayConfiguration configuration)
    {
        Directory.CreateDirectory(SettingsDirectory);
        await using FileStream stream = File.Create(DisplaySettingsPath);
        await JsonSerializer.SerializeAsync(stream, configuration, JsonOptions);
    }

    public static async Task SaveModConfigurationAsync(
        string injectorPath,
        string loaderMode,
        ModConfiguration configuration)
    {
        if (!loaderMode.Equals("injector", StringComparison.OrdinalIgnoreCase) &&
            !loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("不支持的加载方式。");
        }
        string path = GetNativeConfigurationPath(injectorPath);
        string? directory = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new InvalidOperationException("注入器路径没有有效的父目录。");
        }

        Directory.CreateDirectory(directory);
        string installRoot = ResolveInstallRoot(injectorPath, loaderMode);
        string hostConfiguration = configuration.ToIni() +
            Environment.NewLine +
            "[Host]" + Environment.NewLine +
            "modules_root=" + Path.Combine(installRoot, "modules") +
            Environment.NewLine +
            "[Loader]" + Environment.NewLine +
            "install_root=" + installRoot + Environment.NewLine +
            "load_host=true" + Environment.NewLine;
        await NativeConfigurationWriteLock.WaitAsync();
        try
        {
            await File.WriteAllTextAsync(
                path,
                hostConfiguration,
                Encoding.Unicode);
        }
        finally
        {
            NativeConfigurationWriteLock.Release();
        }
    }

    public static async Task SaveUiEnhancementConfigurationAsync(
        bool mobileUiEnabled,
        bool hideUidEnabled,
        bool hideHudEnabled,
        string hideHudHotkey)
    {
        string path = GetNativeConfigurationPath(string.Empty);
        Directory.CreateDirectory(SettingsDirectory);

        await NativeConfigurationWriteLock.WaitAsync();
        try
        {
            string existing = File.Exists(path)
                ? await File.ReadAllTextAsync(path)
                : string.Empty;
            static string Boolean(bool value) => value ? "true" : "false";
            bool anyEnabled = mobileUiEnabled || hideUidEnabled || hideHudEnabled;
            string section =
                "[betterendfield.ui]" + Environment.NewLine +
                "schema_version=3" + Environment.NewLine +
                "enabled=" + Boolean(anyEnabled) + Environment.NewLine +
                "mobile_ui_enabled=" + Boolean(mobileUiEnabled) + Environment.NewLine +
                "hide_uid_enabled=" + Boolean(hideUidEnabled) + Environment.NewLine +
                "hide_hud_enabled=" + Boolean(hideHudEnabled) + Environment.NewLine +
                "hide_hud_hotkey=" + hideHudHotkey + Environment.NewLine +
                "diagnostics=true" + Environment.NewLine;
            string updated = UpsertIniSection(existing, "betterendfield.ui", section);
            string temporary = path + ".ui.tmp";
            await File.WriteAllTextAsync(temporary, updated, Encoding.Unicode);
            File.Move(temporary, path, overwrite: true);
        }
        finally
        {
            NativeConfigurationWriteLock.Release();
        }
    }

    public static async Task SaveCameraEnhancementConfigurationAsync(
        bool freeCameraEnabled,
        bool disableDitherEnabled,
        bool pauseGameEnabled,
        string toggleHotkey,
        double movementSpeed,
        double fieldOfView)
    {
        static string Boolean(bool value) => value ? "true" : "false";
        static string Number(double value) =>
            value.ToString("0.########", CultureInfo.InvariantCulture);

        string path = GetNativeConfigurationPath(string.Empty);
        Directory.CreateDirectory(SettingsDirectory);
        await NativeConfigurationWriteLock.WaitAsync();
        try
        {
            string existing = File.Exists(path)
                ? await File.ReadAllTextAsync(path)
                : string.Empty;
            string section =
                "[betterendfield.camera]" + Environment.NewLine +
                "schema_version=3" + Environment.NewLine +
                "enabled=" + Boolean(freeCameraEnabled || disableDitherEnabled) + Environment.NewLine +
                "free_camera_enabled=" + Boolean(freeCameraEnabled) + Environment.NewLine +
                "disable_dither_enabled=" + Boolean(disableDitherEnabled) + Environment.NewLine +
                "pause_game_enabled=" + Boolean(pauseGameEnabled) + Environment.NewLine +
                "toggle_hotkey=" + toggleHotkey + Environment.NewLine +
                "movement_speed=" + Number(movementSpeed) + Environment.NewLine +
                "field_of_view=" + Number(fieldOfView) + Environment.NewLine +
                "diagnostics=true" + Environment.NewLine;
            string updated = UpsertIniSection(existing, "betterendfield.camera", section);
            string temporary = path + ".camera.tmp";
            await File.WriteAllTextAsync(temporary, updated, Encoding.Unicode);
            File.Move(temporary, path, overwrite: true);
        }
        finally
        {
            NativeConfigurationWriteLock.Release();
        }
    }

    internal static string ResolveInstallRoot(string injectorPath, string loaderMode)
    {
        if (!loaderMode.Equals("injector", StringComparison.OrdinalIgnoreCase) &&
            !loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("不支持的加载方式。");
        }
        return ResolveInstallRoot(injectorPath);
    }

    /// <summary>
    /// 定位安装根目录，不校验加载方式。供不属于任何加载方式的功能使用，例如显示
    /// 增强的组件部署——它只需要读取安装目录下的负载，与 Host 的加载路径无关。
    /// </summary>
    internal static string ResolveInstallRoot(string injectorPath)
    {
        string? installRoot = TryResolveInstallRootFromInjector(injectorPath);
        if (string.IsNullOrWhiteSpace(installRoot))
        {
            installRoot = TryResolveInstallRoot(AppContext.BaseDirectory);
        }
        if (string.IsNullOrWhiteSpace(installRoot) ||
            !File.Exists(Path.Combine(installRoot, "runtime", "BetterEndfield.Host.dll")) ||
            !Directory.Exists(Path.Combine(installRoot, "modules")))
        {
            throw new InvalidOperationException(
                "注入器旁未找到 Better Endfield runtime 和 modules 目录。");
        }
        return installRoot;
    }

    private static string? TryResolveInstallRootFromInjector(string injectorPath)
    {
        if (string.IsNullOrWhiteSpace(injectorPath))
        {
            return null;
        }

        string fullInjectorPath;
        try
        {
            fullInjectorPath = Path.GetFullPath(injectorPath.Trim());
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException or IOException)
        {
            return null;
        }

        string? loaderDirectory = Path.GetDirectoryName(fullInjectorPath);
        string? installRoot = loaderDirectory is null ? null :
            TryResolveInstallRoot(loaderDirectory);
        return installRoot;
    }

    private static string? TryResolveInstallRoot(string startDirectory)
    {
        string current;
        try
        {
            current = Path.GetFullPath(startDirectory);
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException)
        {
            return null;
        }

        for (int level = 0; level < 3; level++)
        {
            if (File.Exists(Path.Combine(current, "runtime", "BetterEndfield.Host.dll")) &&
                Directory.Exists(Path.Combine(current, "modules")) &&
                File.Exists(Path.Combine(
                    current,
                    "loaders",
                    "BetterEndfield.Injector.exe")))
            {
                return current;
            }

            DirectoryInfo? parent = Directory.GetParent(current);
            if (parent is null)
            {
                break;
            }
            current = parent.FullName;
        }

        return null;
    }

    internal static bool IsCompleteInstallRoot(string installRoot)
    {
        if (string.IsNullOrWhiteSpace(installRoot) ||
            !File.Exists(Path.Combine(installRoot, "runtime", "BetterEndfield.Host.dll")) ||
            !Directory.Exists(Path.Combine(installRoot, "modules")) ||
            !File.Exists(Path.Combine(
                installRoot,
                "loaders",
                "BetterEndfield.Injector.exe")))
        {
            return false;
        }
        return true;
    }

    public static async Task<ModConfiguration> LoadModConfigurationAsync(
        string mapperPath)
    {
        var configuration = ModConfiguration.CreateDefaults();
        string path = GetNativeConfigurationPath(mapperPath);
        if (!File.Exists(path))
        {
            return configuration;
        }

        await EnsureUnicodeProfileEncodingAsync(path);
        string[] lines = await File.ReadAllLinesAsync(path);
        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        bool inSection = false;
        bool cameraSectionPresent = false;
        bool inCameraSection = false;
        int cameraSchemaVersion = 0;
        foreach (string sourceLine in lines)
        {
            string line = sourceLine.Trim();
            if (line.Length == 0 || line.StartsWith(';') || line.StartsWith('#'))
            {
                continue;
            }

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                string section = line[1..^1];
                inCameraSection = section.Equals(
                    "betterendfield.camera", StringComparison.OrdinalIgnoreCase);
                cameraSectionPresent |= inCameraSection;
                inSection = section.Equals("betterendfield.model",
                    StringComparison.OrdinalIgnoreCase) ||
                    section.Equals("betterendfield.voice",
                        StringComparison.OrdinalIgnoreCase) ||
                    section.Equals("betterendfield.music",
                        StringComparison.OrdinalIgnoreCase) ||
                    section.Equals("betterendfield.combat_stats",
                        StringComparison.OrdinalIgnoreCase) ||
                    section.Equals("betterendfield.ui",
                        StringComparison.OrdinalIgnoreCase) ||
                    section.Equals("betterendfield.camera",
                        StringComparison.OrdinalIgnoreCase);
                continue;
            }

            if (!inSection)
            {
                continue;
            }

            int separator = line.IndexOf('=');
            if (separator > 0)
            {
                string key = line[..separator].Trim();
                string value = line[(separator + 1)..].Trim();
                if (inCameraSection &&
                    key.Equals("schema_version", StringComparison.OrdinalIgnoreCase))
                {
                    int.TryParse(value, NumberStyles.Integer,
                        CultureInfo.InvariantCulture, out cameraSchemaVersion);
                }
                values[key] = value;
            }
        }

        configuration.Character = Text(values, "character", configuration.Character);
        configuration.FinalAction = Text(values, "final_action", configuration.FinalAction);
        configuration.StartYaw = Number(values, "start_yaw", configuration.StartYaw);
        configuration.TurnDuration = Number(values, "turn_duration", configuration.TurnDuration);
        configuration.Scale = Number(values, "scale", configuration.Scale);
        configuration.ForwardLeanSample = Number(
            values,
            "forward_lean_sample",
            configuration.ForwardLeanSample);
        configuration.SitLoopSpeed = Number(
            values,
            "sit_loop_speed",
            configuration.SitLoopSpeed);
        configuration.SitSpecialSpeed = Number(
            values,
            "sit_special_speed",
            configuration.SitSpecialSpeed);
        configuration.SitToWalkSpeed = Number(
            values,
            "sit_to_walk_speed",
            configuration.SitToWalkSpeed);
        configuration.FinalSpeed = Number(values, "final_speed", configuration.FinalSpeed);
        configuration.FinalLoop = Boolean(values, "final_loop", configuration.FinalLoop);
        configuration.ForceLoop = Boolean(values, "force_loop", configuration.ForceLoop);
        configuration.UseCrossfade = Boolean(
            values,
            "use_crossfade",
            configuration.UseCrossfade);
        configuration.LoopStart = Number(values, "loop_start", configuration.LoopStart);
        configuration.LoopEnd = Number(values, "loop_end", configuration.LoopEnd);
        configuration.CrossfadeDuration = Number(
            values,
            "crossfade_duration",
            configuration.CrossfadeDuration);
        configuration.ModelReplacementEnabled = Boolean(
            values,
            "model_replacement_enabled",
            configuration.ModelReplacementEnabled);
        configuration.LogoThemeEnabled = Boolean(
            values,
            "logo_theme_enabled",
            configuration.LogoThemeEnabled);
        configuration.LogoThemeColor = Text(
            values,
            "logo_theme_color",
            configuration.LogoThemeColor);
        configuration.VoiceRouterEnabled = Boolean(
            values,
            "voice_router_enabled",
            configuration.VoiceRouterEnabled);
        configuration.ReplaceNarrativeVoice = Boolean(
            values,
            "replace_narrative_voice",
            configuration.ReplaceNarrativeVoice);
        configuration.VoiceDiagnostics = Boolean(
            values,
            "voice_diagnostics",
            configuration.VoiceDiagnostics);
        configuration.VoiceLanguageRules = VoiceRulesForEditor(
            Text(values, "voice_language_rules", configuration.VoiceLanguageRules));
        configuration.MusicReplacementEnabled = Boolean(
            values,
            "music_replacement_enabled",
            configuration.MusicReplacementEnabled);
        configuration.OmniMixBackendExe = Text(
            values, "backend_exe", configuration.OmniMixBackendExe);
        configuration.OmniMixClientId = Text(
            values, "client_id", configuration.OmniMixClientId);
        configuration.ReplaceLoginMusic = Boolean(
            values, "replace_login", configuration.ReplaceLoginMusic);
        configuration.ReplaceMetaMusic = Boolean(
            values, "replace_meta", configuration.ReplaceMetaMusic);
        configuration.ReplaceGameplayMusic = Boolean(
            values, "replace_gameplay", configuration.ReplaceGameplayMusic);
        configuration.MusicTargetLatency = Number(
            values, "target_latency", configuration.MusicTargetLatency);
        configuration.MusicPrebufferMilliseconds = Number(
            values, "prebuffer_ms", configuration.MusicPrebufferMilliseconds);
        configuration.FallbackToNativeMusic = Boolean(
            values, "fallback_to_native", configuration.FallbackToNativeMusic);
        configuration.MusicDiagnostics = Boolean(
            values, "diagnostics", configuration.MusicDiagnostics);
        configuration.CombatStatsEnabled = Boolean(
            values, "combat_stats_enabled", configuration.CombatStatsEnabled);
        configuration.HideDamageNumbers = Boolean(
            values, "hide_damage_numbers", configuration.HideDamageNumbers);
        configuration.CombatOverlayEnabled = Boolean(
            values, "overlay_enabled", configuration.CombatOverlayEnabled);
        configuration.CombatRdpsDisplay = Boolean(
            values, "rdps_display", configuration.CombatRdpsDisplay);
        string legacyCombatHotkey = Text(
            values, "hotkey_start", configuration.CombatToggleHotkey);
        configuration.CombatToggleHotkey = Text(
            values, "hotkey_toggle", legacyCombatHotkey);
        configuration.CombatOverlayHotkey = Text(
            values, "overlay_hotkey", configuration.CombatOverlayHotkey);
        configuration.AutoDungeonSession = Boolean(
            values, "auto_dungeon_session", configuration.AutoDungeonSession);
        configuration.UiEnhancementEnabled = Boolean(
            values, "ui_enhancement_enabled",
            Boolean(values, "enabled", configuration.UiEnhancementEnabled));
        configuration.MobileUiEnabled = Boolean(
            values, "mobile_ui_enabled", configuration.MobileUiEnabled);
        configuration.HideUidEnabled = Boolean(
            values, "hide_uid_enabled", configuration.HideUidEnabled);
        configuration.HideHudEnabled = Boolean(
            values, "hide_hud_enabled", configuration.HideHudEnabled);
        configuration.HideHudToggleHotkey = Text(
            values, "hide_hud_hotkey", configuration.HideHudToggleHotkey);
        configuration.FreeCameraEnabled = Boolean(
            values, "free_camera_enabled", configuration.FreeCameraEnabled);
        configuration.DisableDitherEnabled = Boolean(
            values, "disable_dither_enabled", configuration.DisableDitherEnabled);
        configuration.PauseGameInFreeCamera = Boolean(
            values, "pause_game_enabled", configuration.PauseGameInFreeCamera);
        configuration.FreeCameraToggleHotkey = Text(
            values, "toggle_hotkey", configuration.FreeCameraToggleHotkey);
        configuration.FreeCameraMovementSpeed = Number(
            values, "movement_speed", configuration.FreeCameraMovementSpeed);
        configuration.FreeCameraFieldOfView = Number(
            values, "field_of_view", configuration.FreeCameraFieldOfView);
        if (cameraSectionPresent && cameraSchemaVersion < 3)
        {
            // Earlier test schemas used F8, then 8. Adopt the current default
            // for existing test configs while keeping pause disabled.
            configuration.PauseGameInFreeCamera = false;
            configuration.FreeCameraToggleHotkey = "9";
        }
        if ((!cameraSectionPresent && values.ContainsKey("disable_dither_enabled")) ||
            (cameraSectionPresent && cameraSchemaVersion < 3))
        {
            // v2.4 stored anti-dither under betterendfield.ui. Preserve that
            // choice when the feature moves to the independent camera module.
            await SaveCameraEnhancementConfigurationAsync(
                configuration.FreeCameraEnabled,
                configuration.DisableDitherEnabled,
                configuration.PauseGameInFreeCamera,
                configuration.FreeCameraToggleHotkey,
                configuration.FreeCameraMovementSpeed,
                configuration.FreeCameraFieldOfView);
        }
        return configuration;
    }

    private static string UpsertIniSection(
        string contents,
        string sectionName,
        string replacement)
    {
        string[] lines = contents.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
        int start = Array.FindIndex(lines, line =>
            line.Trim().Equals($"[{sectionName}]", StringComparison.OrdinalIgnoreCase));
        if (start < 0)
        {
            string separator = contents.Length == 0 || contents.EndsWith('\n')
                ? string.Empty
                : Environment.NewLine;
            return contents + separator +
                (contents.Length == 0 ? string.Empty : Environment.NewLine) + replacement;
        }

        int end = start + 1;
        while (end < lines.Length)
        {
            string line = lines[end].Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                break;
            }
            end++;
        }

        string prefix = string.Join(Environment.NewLine, lines[..start]);
        string suffix = string.Join(Environment.NewLine, lines[end..]);
        return string.IsNullOrEmpty(prefix)
            ? replacement + suffix
            : prefix + Environment.NewLine + replacement + suffix;
    }

    private static async Task EnsureUnicodeProfileEncodingAsync(string path)
    {
        byte[] prefix = new byte[2];
        await using (FileStream stream = new(
            path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
        {
            if (await stream.ReadAsync(prefix) == prefix.Length &&
                prefix[0] == 0xFF && prefix[1] == 0xFE)
            {
                return;
            }
        }

        string contents = await File.ReadAllTextAsync(path);
        string temporary = path + ".encoding.tmp";
        await File.WriteAllTextAsync(temporary, contents, Encoding.Unicode);
        File.Move(temporary, path, overwrite: true);
    }

    public static string GetNativeConfigurationPath(string _)
    {
        Directory.CreateDirectory(SettingsDirectory);
        return Path.Combine(SettingsDirectory, NativeConfigurationFileName);
    }

    public static string GetLogPath(string _)
    {
        string logDirectory = Path.Combine(SettingsDirectory, "logs");
        Directory.CreateDirectory(logDirectory);
        return Path.Combine(logDirectory, LogFileName);
    }

    private static string Text(
        IReadOnlyDictionary<string, string> values,
        string key,
        string fallback) =>
        values.TryGetValue(key, out string? value) && !string.IsNullOrWhiteSpace(value)
            ? value
            : fallback;

    private static double Number(
        IReadOnlyDictionary<string, string> values,
        string key,
        double fallback) =>
        values.TryGetValue(key, out string? value) &&
        double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double number)
            ? number
            : fallback;

    private static bool Boolean(
        IReadOnlyDictionary<string, string> values,
        string key,
        bool fallback) =>
        values.TryGetValue(key, out string? value)
            ? value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("on", StringComparison.OrdinalIgnoreCase)
             : fallback;

    private static string VoiceRulesForEditor(string value) => string.Join(
        Environment.NewLine,
        value.Split(
            [',', ';'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
}
