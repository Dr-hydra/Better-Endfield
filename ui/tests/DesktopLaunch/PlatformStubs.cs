namespace BetterEndfieldNext.UI.Services;

internal static class ConfigurationService
{
    public static string SettingsDirectory { get; set; } = "";
    public static Task<Models.AppSettings> LoadAppSettingsAsync() => Task.FromResult(new Models.AppSettings());
    public static string ResolveInstallRoot(string injectorPath, string mode) =>
        Path.GetDirectoryName(Path.GetDirectoryName(injectorPath))!;
}

internal static class RuntimePathDiscoveryService
{
    public static string BundledInjectorPath { get; set; } = "";
    public static bool IsGameExecutable(string path) => File.Exists(path) &&
        Path.GetFileName(path).Equals("Endfield.exe", StringComparison.OrdinalIgnoreCase);
}

internal sealed class LocalizationService
{
    public static LocalizationService Instance { get; } = new();
    public bool IsChinese => true;
}
