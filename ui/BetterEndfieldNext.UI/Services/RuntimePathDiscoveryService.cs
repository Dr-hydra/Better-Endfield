using Microsoft.Win32;

namespace BetterEndfieldNext.UI.Services;

internal sealed record RuntimePathDiscoveryResult(
    string GameExecutablePath,
    string InjectorPath,
    string OfficialLauncherPath = "",
    List<string>? DiscoveredOfficialLaunchers = null);

internal static class RuntimePathDiscoveryService
{
    private const string GameExecutableName = "Endfield.exe";
    private const string InjectorExecutableName = "BetterEndfieldNext.Injector.exe";
    private const string LauncherExecutableName = "BetterEndfieldNext.Launcher.exe";

    // IncludeAllContentForSelfExtract makes BaseDirectory point at the temporary
    // bundle extraction directory. Native payloads remain beside the launcher EXE.
    public static string BundledInjectorPath { get; } = Path.GetFullPath(Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory,
        "loaders",
        InjectorExecutableName));

    public static string BundledLauncherPath { get; } = Path.GetFullPath(Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory,
        "loaders",
        LauncherExecutableName));

    public static Task<RuntimePathDiscoveryResult> DiscoverAsync(
        string preferredGamePath,
        string preferredOfficialLauncherPath = "") => Task.Run(() =>
    {
        var discoveredLaunchers = DiscoverAllOfficialLauncherPaths();
        string bestLauncher = "";
        if (!string.IsNullOrWhiteSpace(preferredOfficialLauncherPath))
        {
            string resolved = ResolveOfficialLauncherPath(preferredOfficialLauncherPath);
            if (File.Exists(resolved))
            {
                bestLauncher = resolved;
            }
        }
        if (string.IsNullOrWhiteSpace(bestLauncher) && discoveredLaunchers.Count > 0)
        {
            bestLauncher = discoveredLaunchers[0];
        }

        return new RuntimePathDiscoveryResult(
            DiscoverGamePath(preferredGamePath),
            BundledInjectorPath,
            bestLauncher,
            discoveredLaunchers);
    });

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

    public static string ResolveOfficialLauncherPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        string trimmed = path.Trim().Trim('"');
        if (trimmed.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase) && File.Exists(trimmed))
        {
            string? target = ShortcutService.ResolveShortcutTarget(trimmed);
            if (!string.IsNullOrWhiteSpace(target))
            {
                trimmed = target;
            }
        }

        if (File.Exists(trimmed) && Path.GetFileName(trimmed).Equals("Games.exe", StringComparison.OrdinalIgnoreCase))
        {
            return Path.GetFullPath(trimmed);
        }

        string searchDir = Directory.Exists(trimmed) ? trimmed : (Path.GetDirectoryName(trimmed) ?? string.Empty);
        if (!string.IsNullOrWhiteSpace(searchDir) && Directory.Exists(searchDir))
        {
            string? best = FindBestGamesExecutable(searchDir);
            if (!string.IsNullOrWhiteSpace(best))
            {
                return best;
            }
        }

        if (File.Exists(trimmed))
        {
            return Path.GetFullPath(trimmed);
        }

        return trimmed;
    }

    public static string FormatLauncherDisplayName(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return string.Empty;
        string normalized = Path.GetFullPath(path.Trim());
        string serverTag = "[自定义]";
        if (normalized.Contains("Hypergryph", StringComparison.OrdinalIgnoreCase) ||
            normalized.Contains("鹰角", StringComparison.OrdinalIgnoreCase))
        {
            serverTag = "[国服]";
        }
        else if (normalized.Contains("GRYPHLINK", StringComparison.OrdinalIgnoreCase) ||
                 normalized.Contains("Gryphline", StringComparison.OrdinalIgnoreCase))
        {
            serverTag = "[国际服]";
        }
        return $"{serverTag} {normalized}";
    }

    public static string DiscoverOfficialLauncherPath(string preferredPath = "")
    {
        if (!string.IsNullOrWhiteSpace(preferredPath))
        {
            string resolved = ResolveOfficialLauncherPath(preferredPath);
            if (File.Exists(resolved))
            {
                return resolved;
            }
        }

        var all = DiscoverAllOfficialLauncherPaths();
        return all.Count > 0 ? all[0] : string.Empty;
    }

    public static List<string> DiscoverAllOfficialLauncherPaths()
    {
        var roots = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // 1. HKCR protocols
        foreach (string protocol in new[] { "hypergryphlauncher", "gryphlinklauncher" })
        {
            try
            {
                string? command = Registry.GetValue($@"HKEY_CLASSES_ROOT\{protocol}\shell\open\command", null, null) as string;
                if (!string.IsNullOrWhiteSpace(command))
                {
                    string exe = ExtractExecutablePath(command);
                    if (!string.IsNullOrWhiteSpace(exe))
                    {
                        string? dir = Path.GetDirectoryName(exe);
                        if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
                        {
                            roots.Add(dir);
                        }
                    }
                }
            }
            catch { }
        }

        // 2. Registry Uninstall keys
        string[] searchHives = [
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
        ];
        string[] targetNames = ["鹰角启动器", "Hypergryph Launcher", "GRYPHLINK", "Gryphlink", "Gryphline Launcher"];

        foreach (RegistryKey baseKey in new[] { Registry.CurrentUser, Registry.LocalMachine })
        {
            foreach (string uninstallPath in searchHives)
            {
                RegistryKey? key = null;
                try
                {
                    key = baseKey.OpenSubKey(uninstallPath);
                    if (key == null) continue;
                    foreach (string subkeyName in key.GetSubKeyNames())
                    {
                        using RegistryKey? subkey = key.OpenSubKey(subkeyName);
                        if (subkey == null) continue;
                        string? displayName = subkey.GetValue("DisplayName") as string;
                        if (string.IsNullOrWhiteSpace(displayName)) continue;
                        if (targetNames.Any(name => displayName.Contains(name, StringComparison.OrdinalIgnoreCase)))
                        {
                            string? installLocation = subkey.GetValue("InstallLocation") as string;
                            if (!string.IsNullOrWhiteSpace(installLocation) && Directory.Exists(installLocation))
                            {
                                roots.Add(installLocation);
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    key?.Dispose();
                }
            }
        }

        // 3. Fixed common environment paths
        string[] commonFolders = ["Hypergryph Launcher", "GRYPHLINK"];
        string[] envRoots = [
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        ];
        foreach (string envRoot in envRoots)
        {
            if (string.IsNullOrWhiteSpace(envRoot)) continue;
            foreach (string folder in commonFolders)
            {
                string combined = Path.Combine(envRoot, folder);
                if (Directory.Exists(combined))
                {
                    roots.Add(combined);
                }
            }
        }

        // 4. From collected roots, find best Games.exe
        var candidates = new List<(string Path, Version Version)>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string root in roots)
        {
            string? found = FindBestGamesExecutable(root);
            if (!string.IsNullOrWhiteSpace(found) && seen.Add(found))
            {
                string dirName = Path.GetFileName(Path.GetDirectoryName(found) ?? string.Empty);
                Version.TryParse(dirName, out Version? ver);
                candidates.Add((found, ver ?? new Version(0, 0, 0)));
            }
        }

        return candidates.OrderByDescending(c => c.Version).Select(c => c.Path).ToList();
    }

    private static string? FindBestGamesExecutable(string rootDir)
    {
        if (!Directory.Exists(rootDir)) return null;

        var candidates = new List<(string Path, Version Version)>();

        string rootGames = Path.Combine(rootDir, "Games.exe");
        if (File.Exists(rootGames))
        {
            candidates.Add((Path.GetFullPath(rootGames), new Version(0, 0, 0)));
        }

        try
        {
            foreach (string subDir in Directory.EnumerateDirectories(rootDir))
            {
                string dirName = Path.GetFileName(subDir);
                if (Version.TryParse(dirName, out Version? ver))
                {
                    string candidate = Path.Combine(subDir, "Games.exe");
                    if (File.Exists(candidate))
                    {
                        candidates.Add((Path.GetFullPath(candidate), ver));
                    }
                }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
        }

        if (candidates.Count == 0) return null;
        return candidates.OrderByDescending(c => c.Version).First().Path;
    }

    private static string ExtractExecutablePath(string command)
    {
        if (string.IsNullOrWhiteSpace(command)) return string.Empty;
        string trimmed = command.Trim();
        if (trimmed.StartsWith("\""))
        {
            int nextQuote = trimmed.IndexOf('"', 1);
            if (nextQuote > 1) return trimmed[1..nextQuote];
        }
        int space = trimmed.IndexOf(' ');
        return space > 0 ? trimmed[..space] : trimmed;
    }
}
