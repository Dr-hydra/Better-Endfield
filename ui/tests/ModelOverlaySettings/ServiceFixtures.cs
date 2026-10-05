namespace BetterEndfield.UI.Services;

// UI/profile dependencies are replaced; all model persistence runs in the real service.
internal static class ConfigurationService
{
    internal static string SettingsDirectory { get; set; } = "";
}
internal static class BemText
{
    internal static string Get(string value) => value;
    internal static string Format(string value, params object[] args) => string.Format(System.Globalization.CultureInfo.InvariantCulture, value, args);
    internal static string Colon => ": ";
}
