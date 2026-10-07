using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace BetterEndfield.UI.Services;

internal sealed class SteamRegistryCompatibilityStore : ISteamCompatibilityStore
{
    private const string Layers = @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
    public string? Read(string executable)
    {
        using var key = Registry.CurrentUser.OpenSubKey(Layers);
        return key?.GetValue(executable) as string;
    }
    public void Write(string executable, string? flags)
    {
        using var key = Registry.CurrentUser.CreateSubKey(Layers, writable: true);
        if (flags is null) key.DeleteValue(executable, throwOnMissingValue: false);
        else key.SetValue(executable, flags, RegistryValueKind.String);
    }
    public static bool MachineAdministrator(string executable)
    {
        using var key = Registry.LocalMachine.OpenSubKey(Layers);
        return SteamCompatibilityService.HasAdministrator(key?.GetValue(executable) as string);
    }
}

internal static class SteamEnvironment
{
    public static string DiscoverExecutable()
    {
        using var user = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
        using var machine = Registry.LocalMachine.OpenSubKey(@"Software\WOW6432Node\Valve\Steam");
        foreach (string? root in new[] { user?.GetValue("SteamPath") as string, machine?.GetValue("InstallPath") as string,
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam") })
        {
            if (root is not null && File.Exists(Path.Combine(root, "steam.exe"))) return Path.GetFullPath(Path.Combine(root, "steam.exe"));
        }
        return "";
    }

    public static IReadOnlyList<string> DiscoverLibraries(string executable)
    {
        var libraries = new List<string>();
        if (!File.Exists(executable)) return libraries;
        string root = Path.GetDirectoryName(Path.GetFullPath(executable))!;
        if (Directory.Exists(Path.Combine(root, "steamapps"))) libraries.Add(root);
        string file = Path.Combine(root, "steamapps", "libraryfolders.vdf");
        if (File.Exists(file))
        {
            var folders = SteamKeyValues.Parse(File.ReadAllText(file)).Object("libraryfolders");
            if (folders is not null)
                foreach (var (_, folder) in folders.Objects)
                    if (folder.String("path") is string path && Directory.Exists(Path.Combine(path, "steamapps"))) libraries.Add(Path.GetFullPath(path));
        }
        return libraries.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
    }

    public static bool IsRunning()
    {
        var processes = Process.GetProcessesByName("steam");
        try { return processes.Length > 0; }
        finally { foreach (var process in processes) process.Dispose(); }
    }

    public static bool? IsRunningElevated()
    {
        var processes = Process.GetProcessesByName("steam");
        try
        {
            if (processes.Length == 0) return null;
            foreach (var process in processes)
            {
                using var handle = OpenProcess(0x1000, false, process.Id);
                if (handle.IsInvalid || !OpenProcessToken(handle, 8, out var token)) return null;
                using (token)
                {
                    if (!GetTokenInformation(token, 20, out int elevated, sizeof(int), out _)) return null;
                    if (elevated != 0) return true;
                }
            }
            return false;
        }
        finally { foreach (var process in processes) process.Dispose(); }
    }

    public static void Launch(string executable, bool administrator, bool game)
    {
        executable = Path.GetFullPath(executable);
        if (!File.Exists(executable) || !Path.GetFileName(executable).Equals("steam.exe", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_steam_path");
        if (administrator && IsRunning() && IsRunningElevated() != true) throw new SteamIntegrationException("restart_elevated");
        Process.Start(new ProcessStartInfo(executable)
        {
            UseShellExecute = true,
            Verb = administrator ? "runas" : "open",
            Arguments = game ? "-applaunch " + SteamMetadata.EndfieldAppId : "",
            WorkingDirectory = Path.GetDirectoryName(executable)!
        });
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern SafeProcessHandle OpenProcess(uint access, bool inherit, int processId);
    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(SafeProcessHandle process, uint access, out SafeAccessTokenHandle token);
    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetTokenInformation(SafeAccessTokenHandle token, int informationClass, out int information, int size, out int returned);
}
