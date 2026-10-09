using System.Text.Json;

namespace BetterEndfieldNext.UI.Services;

internal interface ISteamCompatibilityStore
{
    string? Read(string executable);
    void Write(string executable, string? flags);
}

internal sealed record SteamCompatibilityReceipt(string Executable, bool OriginalAdministrator, bool WrittenAdministrator);

internal sealed class SteamCompatibilityService(string receiptPath, ISteamCompatibilityStore store, Func<bool> steamIsRunning)
{
    internal static bool HasAdministrator(string? flags) => Tokens(flags).Contains("RUNASADMIN", StringComparer.OrdinalIgnoreCase);
    private static string[] Tokens(string? flags) => (flags ?? "").Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);

    internal static string? SetAdministrator(string? flags, bool enabled)
    {
        var tokens = Tokens(flags).Where(t => !t.Equals("RUNASADMIN", StringComparison.OrdinalIgnoreCase)).ToList();
        if (enabled)
        {
            if (tokens.Any(t => t.Equals("RUNASINVOKER", StringComparison.OrdinalIgnoreCase) || t.Equals("RUNASNORMAL", StringComparison.OrdinalIgnoreCase)))
                throw new SteamIntegrationException("compatibility_conflict");
            if (tokens.Count == 0) tokens.Add("~");
            tokens.Add("RUNASADMIN");
        }
        if (tokens.All(t => t is "~" or "#")) return null;
        return string.Join(' ', tokens);
    }

    public void Apply(string executable, bool enabled)
    {
        RequireClosed();
        executable = Path.GetFullPath(executable);
        if (!File.Exists(executable) || !Path.GetFileName(executable).Equals("steam.exe", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_steam_path");
        string? previous = store.Read(executable);
        var receipt = ReadReceipt();
        if (receipt is not null && !SteamIntegrationService.SamePath(receipt.Executable, executable)) throw new SteamIntegrationException("steam_path_changed");
        string? desired = SetAdministrator(previous, enabled);
        RequireClosed();
        store.Write(executable, desired);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(receiptPath))!);
            string temporary = receiptPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                File.WriteAllText(temporary, JsonSerializer.Serialize(new SteamCompatibilityReceipt(executable, receipt?.OriginalAdministrator ?? HasAdministrator(previous), enabled)));
                File.Move(temporary, receiptPath, true);
            }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }
        catch { store.Write(executable, previous); throw; }
    }

    public void Restore()
    {
        RequireClosed();
        var receipt = ReadReceipt();
        if (receipt is null) return;
        string? current = store.Read(receipt.Executable);
        if (HasAdministrator(current) != receipt.WrittenAdministrator) throw new SteamIntegrationException("compatibility_changed");
        store.Write(receipt.Executable, SetAdministrator(current, receipt.OriginalAdministrator));
        File.Delete(receiptPath);
    }

    private SteamCompatibilityReceipt? ReadReceipt()
    {
        if (!File.Exists(receiptPath)) return null;
        var receipt = JsonSerializer.Deserialize<SteamCompatibilityReceipt>(File.ReadAllText(receiptPath)) ?? throw new SteamIntegrationException("invalid_receipt");
        if (string.IsNullOrWhiteSpace(receipt.Executable) || !Path.IsPathFullyQualified(receipt.Executable) || !Path.GetFileName(receipt.Executable).Equals("steam.exe", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_receipt");
        return receipt;
    }
    private void RequireClosed() { if (steamIsRunning()) throw new SteamIntegrationException("steam_running"); }
}
