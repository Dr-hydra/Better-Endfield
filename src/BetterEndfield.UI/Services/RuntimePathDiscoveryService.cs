using Microsoft.Win32;

namespace BetterEndfield.UI.Services;

internal sealed record RuntimePathDiscoveryResult(
    string GameExecutablePath,
    string InjectorPath);

internal static class RuntimePathDiscoveryService
{
    private const string GameExecutableName = "Endfield.exe";
    private const string InjectorExecutableName = "BetterEndfield.Injector.exe";

    public static Task<RuntimePathDiscoveryResult> DiscoverAsync(
        string preferredGamePath,
        string preferredInjectorPath) => Task.Run(() => new RuntimePathDiscoveryResult(
            DiscoverGamePath(preferredGamePath),
            DiscoverInjectorPath(preferredInjectorPath)));

    public static string DiscoverGamePath(string preferredPath)
    {
        foreach (string candidate in EnumerateGameCandidates(preferredPath))
        {
            if (IsGameExecutable(candidate))
            {
                return Path.GetFullPath(candidate);
            }
        }
        return string.Empty;
    }

    public static string DiscoverInjectorPath(string preferredPath)
    {
        foreach (string candidate in EnumerateInjectorCandidates(preferredPath))
        {
            if (!File.Exists(candidate))
            {
                continue;
            }
            string? loaderDirectory = Path.GetDirectoryName(candidate);
            string? installRoot = loaderDirectory is null
                ? null
                : Directory.GetParent(loaderDirectory)?.FullName;
            if (!string.IsNullOrWhiteSpace(installRoot) &&
                ConfigurationService.IsCompleteInstallRoot(installRoot))
            {
                return Path.GetFullPath(candidate);
            }
        }
        return string.Empty;
    }

    public static bool IsGameExecutable(string path) =>
        !string.IsNullOrWhiteSpace(path) &&
        File.Exists(path) &&
        Path.GetFileName(path).Equals(
            GameExecutableName,
            StringComparison.OrdinalIgnoreCase);

    private static IEnumerable<string> EnumerateGameCandidates(string preferredPath)
    {
        var emitted = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string candidate in AddCandidate(preferredPath, emitted))
        {
            yield return candidate;
        }
        foreach (string candidate in ReadUninstallRegistryCandidates())
        {
            foreach (string path in AddCandidate(candidate, emitted))
            {
                yield return path;
            }
        }

        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            bool usable;
            try
            {
                usable = drive.IsReady && drive.DriveType == DriveType.Fixed;
            }
            catch (IOException)
            {
                usable = false;
            }
            if (!usable)
            {
                continue;
            }

            string root = drive.RootDirectory.FullName;
            string[] directCandidates =
            [
                Path.Combine(root, "Endfield Game", GameExecutableName),
                Path.Combine(root, "Games", "Endfield Game", GameExecutableName),
                Path.Combine(root, "Game", "Endfield Game", GameExecutableName),
                Path.Combine(root, "Program Files", "Endfield Game", GameExecutableName),
                Path.Combine(root, "Program Files (x86)", "Endfield Game", GameExecutableName)
            ];
            foreach (string candidate in directCandidates)
            {
                foreach (string path in AddCandidate(candidate, emitted))
                {
                    yield return path;
                }
            }

            string[] boundedRoots =
            [
                root,
                Path.Combine(root, "Games"),
                Path.Combine(root, "Game"),
                Path.Combine(root, "Program Files"),
                Path.Combine(root, "Program Files (x86)")
            ];
            foreach (string boundedRoot in boundedRoots)
            {
                foreach (string candidate in FindNamedGameDirectories(boundedRoot))
                {
                    foreach (string path in AddCandidate(candidate, emitted))
                    {
                        yield return path;
                    }
                }
            }
        }
    }

    private static IEnumerable<string> EnumerateInjectorCandidates(string preferredPath)
    {
        var emitted = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string candidate in AddCandidate(preferredPath, emitted))
        {
            yield return candidate;
        }

        var roots = new List<string>
        {
            AppContext.BaseDirectory,
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Programs",
                "Better Endfield"),
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "Better Endfield")
        };
        string current = AppContext.BaseDirectory;
        for (int level = 0; level < 8; level++)
        {
            roots.Add(current);
            DirectoryInfo? parent = Directory.GetParent(current);
            if (parent is null)
            {
                break;
            }
            current = parent.FullName;
        }

        foreach (string root in roots.Where(root => !string.IsNullOrWhiteSpace(root)))
        {
            foreach (string candidate in AddCandidate(
                Path.Combine(root, "loaders", InjectorExecutableName), emitted))
            {
                yield return candidate;
            }
        }
    }

    private static IEnumerable<string> AddCandidate(
        string candidate,
        ISet<string> emitted)
    {
        if (string.IsNullOrWhiteSpace(candidate))
        {
            yield break;
        }
        string fullPath;
        try
        {
            fullPath = Path.GetFullPath(candidate.Trim().Trim('"'));
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException)
        {
            yield break;
        }
        if (emitted.Add(fullPath))
        {
            yield return fullPath;
        }
    }

    private static IEnumerable<string> FindNamedGameDirectories(string root)
    {
        if (!Directory.Exists(root))
        {
            yield break;
        }
        IEnumerable<string> directories;
        try
        {
            directories = Directory.EnumerateDirectories(root).Take(512).ToArray();
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            yield break;
        }
        foreach (string directory in directories)
        {
            string name = Path.GetFileName(directory);
            if (!name.Contains("endfield", StringComparison.OrdinalIgnoreCase) &&
                !name.Contains("终末地", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            yield return Path.Combine(directory, GameExecutableName);
            yield return Path.Combine(directory, "Endfield Game", GameExecutableName);
        }
    }

    private static IReadOnlyList<string> ReadUninstallRegistryCandidates()
    {
        const string uninstallKey =
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        var candidates = new List<string>();
        foreach (RegistryHive hive in new[] { RegistryHive.CurrentUser, RegistryHive.LocalMachine })
        {
            foreach (RegistryView view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
            {
                RegistryKey? baseKey = null;
                RegistryKey? uninstall = null;
                try
                {
                    baseKey = RegistryKey.OpenBaseKey(hive, view);
                    uninstall = baseKey.OpenSubKey(uninstallKey);
                    if (uninstall is null)
                    {
                        continue;
                    }
                    foreach (string name in uninstall.GetSubKeyNames())
                    {
                        using RegistryKey? product = uninstall.OpenSubKey(name);
                        string displayName = product?.GetValue("DisplayName") as string ?? string.Empty;
                        if (!displayName.Contains("Endfield", StringComparison.OrdinalIgnoreCase) &&
                            !displayName.Contains("终末地", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        string installLocation =
                            product?.GetValue("InstallLocation") as string ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(installLocation))
                        {
                            candidates.Add(Path.Combine(installLocation, GameExecutableName));
                        }
                        string displayIcon = product?.GetValue("DisplayIcon") as string ?? string.Empty;
                        string iconPath = displayIcon.Split(',', 2)[0].Trim().Trim('"');
                        if (!string.IsNullOrWhiteSpace(iconPath))
                        {
                            candidates.Add(iconPath);
                        }
                    }
                }
                catch (Exception exception) when (
                    exception is IOException or UnauthorizedAccessException or
                        System.Security.SecurityException)
                {
                }
                finally
                {
                    uninstall?.Dispose();
                    baseKey?.Dispose();
                }
            }
        }
        return candidates;
    }
}
