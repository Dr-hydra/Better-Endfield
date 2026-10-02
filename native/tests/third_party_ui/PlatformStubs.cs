// These host tests exercise locale selection without the WinRT app-language API.
namespace Windows.Globalization;

internal static class ApplicationLanguages
{
    public static string PrimaryLanguageOverride { get; set; } = "";
}
