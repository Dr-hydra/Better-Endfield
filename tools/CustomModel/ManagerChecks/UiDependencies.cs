namespace BetterEndfieldNext.UI.Services;

// The manager regressions exercise production metadata/settings services on a
// plain .NET host; visual localization and keyboard capture are outside scope.
internal sealed class LocalizationService
{
    public static LocalizationService Instance { get; } = new();
    public bool IsChinese => true;
}

internal static class HotkeyService
{
    public static bool TryNormalize(string? value, out string normalized)
    {
        normalized = value ?? "PLUS";
        return true;
    }
}
