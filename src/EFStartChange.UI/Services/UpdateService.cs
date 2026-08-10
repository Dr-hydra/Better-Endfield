using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EFStartChange.UI.Services;

internal sealed record UpdateCheckResult(
    string CurrentVersion,
    string? LatestVersion,
    string ReleasesUrl,
    bool HasRelease,
    bool IsUpdateAvailable);

internal static partial class UpdateService
{
    public const string RepositoryUrl = "https://github.com/Dr-hydra/EF-Start-Change";
    public const string ReleasesUrl = RepositoryUrl + "/releases";

    private static readonly HttpClient Client = CreateClient();

    public static string CurrentVersion =>
        (Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "1.0.0")
        .Split('+')[0];

    public static async Task<UpdateCheckResult> CheckAsync(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await Client.GetAsync(
            "https://api.github.com/repos/Dr-hydra/EF-Start-Change/releases/latest",
            cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new UpdateCheckResult(
                CurrentVersion,
                null,
                ReleasesUrl,
                HasRelease: false,
                IsUpdateAvailable: false);
        }

        response.EnsureSuccessStatusCode();
        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using JsonDocument document = await JsonDocument.ParseAsync(
            stream,
            cancellationToken: cancellationToken);
        JsonElement root = document.RootElement;
        string latestVersion = root.GetProperty("tag_name").GetString() ?? string.Empty;
        string releasesUrl = root.TryGetProperty("html_url", out JsonElement htmlUrl)
            ? htmlUrl.GetString() ?? ReleasesUrl
            : ReleasesUrl;
        return new UpdateCheckResult(
            CurrentVersion,
            latestVersion,
            releasesUrl,
            HasRelease: !string.IsNullOrWhiteSpace(latestVersion),
            IsUpdateAvailable: CompareVersions(latestVersion, CurrentVersion) > 0);
    }

    private static HttpClient CreateClient()
    {
        var client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(12)
        };
        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            $"EFStartChange/{CurrentVersion}");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        return client;
    }

    private static int CompareVersions(string left, string right)
    {
        SemanticVersion leftVersion = SemanticVersion.Parse(left);
        SemanticVersion rightVersion = SemanticVersion.Parse(right);
        int comparison = leftVersion.Numeric.CompareTo(rightVersion.Numeric);
        if (comparison != 0)
        {
            return comparison;
        }
        if (leftVersion.IsPrerelease != rightVersion.IsPrerelease)
        {
            return leftVersion.IsPrerelease ? -1 : 1;
        }
        return leftVersion.PrereleaseNumber.CompareTo(rightVersion.PrereleaseNumber);
    }

    private readonly record struct SemanticVersion(
        Version Numeric,
        bool IsPrerelease,
        int PrereleaseNumber)
    {
        public static SemanticVersion Parse(string value)
        {
            Match match = VersionPattern().Match(value);
            if (!match.Success)
            {
                return new SemanticVersion(new Version(), true, 0);
            }

            int major = int.Parse(match.Groups["major"].Value);
            int minor = int.Parse(match.Groups["minor"].Value);
            int patch = match.Groups["patch"].Success
                ? int.Parse(match.Groups["patch"].Value)
                : 0;
            string suffix = match.Groups["suffix"].Value;
            Match suffixNumber = TrailingNumberPattern().Match(suffix);
            return new SemanticVersion(
                new Version(major, minor, patch),
                !string.IsNullOrWhiteSpace(suffix),
                suffixNumber.Success ? int.Parse(suffixNumber.Value) : 0);
        }
    }

    [GeneratedRegex(
        @"^[vV]?(?<major>\d+)\.(?<minor>\d+)(?:\.(?<patch>\d+))?(?<suffix>[-+].*)?$",
        RegexOptions.CultureInvariant)]
    private static partial Regex VersionPattern();

    [GeneratedRegex(@"\d+$", RegexOptions.CultureInvariant)]
    private static partial Regex TrailingNumberPattern();
}
