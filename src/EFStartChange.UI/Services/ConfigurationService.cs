using System.Globalization;
using System.Text;
using System.Text.Json;
using EFStartChange.UI.Models;

namespace EFStartChange.UI.Services;

internal static class ConfigurationService
{
    public const string NativeConfigurationFileName = "EFStartChange.ini";
    public const string LogFileName = "IL2CPPDump_Log.txt";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static string SettingsDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "EFStartChange");

    public static string SettingsPath { get; } =
        Path.Combine(SettingsDirectory, "ui-settings.json");

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

    public static async Task SaveModConfigurationAsync(
        string mapperPath,
        ModConfiguration configuration)
    {
        string path = GetNativeConfigurationPath(mapperPath);
        string? directory = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new InvalidOperationException("注入器路径没有有效的父目录。");
        }

        Directory.CreateDirectory(directory);
        await File.WriteAllTextAsync(
            path,
            configuration.ToIni(),
            new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
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

        string[] lines = await File.ReadAllLinesAsync(path);
        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        bool inSection = false;
        foreach (string sourceLine in lines)
        {
            string line = sourceLine.Trim();
            if (line.Length == 0 || line.StartsWith(';') || line.StartsWith('#'))
            {
                continue;
            }

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                inSection = line[1..^1].Equals(
                    "EFStartChange",
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
                values[line[..separator].Trim()] = line[(separator + 1)..].Trim();
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
        configuration.VoiceRouterEnabled = Boolean(
            values,
            "voice_router_enabled",
            configuration.VoiceRouterEnabled);
        configuration.ReplaceNarrativeVoice = Boolean(
            values,
            "replace_narrative_voice",
            configuration.ReplaceNarrativeVoice);
        configuration.VoiceLanguageRules = VoiceRulesForEditor(
            Text(values, "voice_language_rules", configuration.VoiceLanguageRules));
        return configuration;
    }

    public static string GetNativeConfigurationPath(string mapperPath)
    {
        string? directory = Path.GetDirectoryName(mapperPath.Trim());
        return string.IsNullOrWhiteSpace(directory)
            ? NativeConfigurationFileName
            : Path.Combine(directory, NativeConfigurationFileName);
    }

    public static string GetLogPath(string mapperPath)
    {
        string? directory = Path.GetDirectoryName(mapperPath.Trim());
        return string.IsNullOrWhiteSpace(directory)
            ? LogFileName
            : Path.Combine(directory, LogFileName);
    }

    public static string DiscoverMapperPath()
    {
        string colocatedMapper = Path.Combine(
            AppContext.BaseDirectory,
            "Il2cppDumper.exe");
        if (File.Exists(colocatedMapper))
        {
            return colocatedMapper;
        }

        string current = AppContext.BaseDirectory;
        for (int level = 0; level < 8; level++)
        {
            string candidate = Path.Combine(
                current,
                "tools",
                "IL2CPP-Dumper-src",
                "x64",
                "Release",
                "Il2cppDumper.exe");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            DirectoryInfo? parent = Directory.GetParent(current);
            if (parent is null)
            {
                break;
            }

            current = parent.FullName;
        }

        return string.Empty;
    }

    public static string DiscoverGamePath()
    {
        var candidates = new List<string>();
        string programFiles = Environment.GetFolderPath(
            Environment.SpecialFolder.ProgramFiles);
        if (!string.IsNullOrWhiteSpace(programFiles))
        {
            candidates.Add(Path.Combine(programFiles, "Endfield Game", "Endfield.exe"));
        }

        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.DriveType == DriveType.Fixed && drive.IsReady)
            {
                candidates.Add(Path.Combine(
                    drive.RootDirectory.FullName,
                    "Endfield Game",
                    "Endfield.exe"));
            }
        }
        return candidates.FirstOrDefault(File.Exists) ?? string.Empty;
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
