using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace BetterEndfield.UI.Services;

public sealed class LocalizationService : INotifyPropertyChanged
{
    private const string ResourcePrefix = "BetterEndfield.UI.Assets.locales.";
    private static readonly Dictionary<string, Dictionary<string, string>> Locales = new(
        StringComparer.OrdinalIgnoreCase);

    public static LocalizationService Instance { get; } = new();

    private string _configuredLanguage = "System";
    private string _effectiveLanguage = "zh-CN";
    private Dictionary<string, string> _currentStrings = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ConfiguredLanguage => _configuredLanguage;

    public string EffectiveLanguage => _effectiveLanguage;

    public bool IsChinese => _effectiveLanguage.StartsWith("zh", StringComparison.OrdinalIgnoreCase);

    public string this[string key] => GetString(key);

    public LocalizationService()
    {
        LoadAllLocales();
        ApplyLanguage("System");
    }

    private static void LoadAllLocales()
    {
        Assembly assembly = typeof(LocalizationService).Assembly;
        string[] resourceNames = assembly.GetManifestResourceNames();

        foreach (string resourceName in resourceNames)
        {
            if (!resourceName.StartsWith(ResourcePrefix, StringComparison.OrdinalIgnoreCase) ||
                !resourceName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string langCode = resourceName
                .Substring(ResourcePrefix.Length, resourceName.Length - ResourcePrefix.Length - 5);

            try
            {
                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null) continue;

                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
                if (dict != null)
                {
                    Locales[langCode] = new Dictionary<string, string>(dict, StringComparer.OrdinalIgnoreCase);
                }
            }
            catch
            {
                // Fallback gracefully on parsing errors
            }
        }
    }

    public void ApplyLanguage(string languagePreference)
    {
        _configuredLanguage = string.IsNullOrWhiteSpace(languagePreference) ? "System" : languagePreference;

        string effective;
        if (_configuredLanguage.Equals("System", StringComparison.OrdinalIgnoreCase))
        {
            CultureInfo uiCulture = CultureInfo.CurrentUICulture;
            effective = uiCulture.Name.StartsWith("zh", StringComparison.OrdinalIgnoreCase) ? "zh-CN" : "en-US";
        }
        else if (_configuredLanguage.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
        {
            effective = "zh-CN";
        }
        else
        {
            effective = "en-US";
        }

        _effectiveLanguage = effective;

        try
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = effective;
            CultureInfo.CurrentUICulture = new CultureInfo(effective);
            CultureInfo.CurrentCulture = new CultureInfo(effective);
        }
        catch
        {
        }

        if (Locales.TryGetValue(effective, out Dictionary<string, string>? dict))
        {
            _currentStrings = dict;
        }
        else if (Locales.TryGetValue("zh-CN", out Dictionary<string, string>? fallbackDict))
        {
            _currentStrings = fallbackDict;
        }
        else
        {
            _currentStrings = [];
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EffectiveLanguage)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChinese)));
    }

    public string GetString(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) return string.Empty;

        if (_currentStrings.TryGetValue(key, out string? value))
        {
            return value;
        }

        // Try fallback to zh-CN if current is en-US or vice-versa
        if (Locales.TryGetValue("zh-CN", out var zhDict) && zhDict.TryGetValue(key, out string? zhValue))
        {
            return zhValue;
        }

        return key;
    }

    public string GetString(string key, params object[] args)
    {
        string template = GetString(key);
        try
        {
            return string.Format(CultureInfo.CurrentCulture, template, args);
        }
        catch
        {
            return template;
        }
    }
}
