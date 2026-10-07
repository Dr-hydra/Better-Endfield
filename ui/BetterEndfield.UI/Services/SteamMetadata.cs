using System.Text.Json;
using System.Text.RegularExpressions;

namespace BetterEndfield.UI.Services;

internal sealed record SteamDepotMetadata(string Id, string Manifest);

internal sealed class SteamMetadata
{
    public const string EndfieldAppId = "4732690";
    public string AppId { get; set; } = EndfieldAppId;
    public string Name { get; set; } = "Arknights: Endfield";
    public string InstallDirectory { get; set; } = "";
    public string Executable { get; set; } = "";
    public string LaunchArguments { get; set; } = "";
    public string BuildId { get; set; } = "";
    public List<SteamDepotMetadata> Depots { get; set; } = [];

    public IReadOnlyList<string> MissingFields => new[]
    {
        string.IsNullOrEmpty(InstallDirectory) ? "install_directory" : "",
        string.IsNullOrEmpty(Executable) ? "executable" : "",
        string.IsNullOrEmpty(BuildId) ? "build_id" : "",
        Depots is null || Depots.Count == 0 ? "depots" : ""
    }.Where(field => field.Length > 0).ToArray();

    public void Validate()
    {
        if (Name is null || InstallDirectory is null || Executable is null || LaunchArguments is null || BuildId is null || Depots is null || Depots.Any(d => d is null))
            throw new SteamIntegrationException("invalid_metadata");
        if (AppId != EndfieldAppId) throw new SteamIntegrationException("wrong_app");
        if (MissingFields.Count > 0) throw new SteamIntegrationException("metadata_pending", string.Join(", ", MissingFields));
        ValidateRelativePath(InstallDirectory, singleDirectory: true);
        ValidateRelativePath(Executable);
        if (!Executable.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) throw new SteamIntegrationException("invalid_executable");
        if (!PositiveId(BuildId) || Depots.Count > 256 || Depots.Any(d => !PositiveId(d.Id) || !PositiveId(d.Manifest)) || Depots.Select(d => d.Id).Distinct().Count() != Depots.Count)
            throw new SteamIntegrationException("invalid_metadata_ids");
        if (Name.Length is 0 or > 256 || Name.Any(char.IsControl) || LaunchArguments.Any(c => c is '\0' or '\r' or '\n'))
            throw new SteamIntegrationException("invalid_metadata");
    }

    private static bool PositiveId(string value) => value is not null && Regex.IsMatch(value, "^[1-9][0-9]{0,19}$", RegexOptions.CultureInvariant) && ulong.TryParse(value, out _);

    public static string ValidateRelativePath(string path, bool singleDirectory = false)
    {
        string[] parts = path.Replace('\\', '/').Split('/');
        if (path.Length is 0 or > 240 || (singleDirectory && parts.Length != 1) ||
            parts.Any(part => part.Length == 0 || part is "." or ".." || part.EndsWith(' ') || part.EndsWith('.') ||
                part.IndexOfAny([':', '*', '?', '"', '<', '>', '|']) >= 0 || part.Any(char.IsControl) ||
                Regex.IsMatch(part, "^(CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(?:\\.|$)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)))
            throw new SteamIntegrationException("invalid_relative_path", path);
        return Path.Combine(parts);
    }

    public string Identity() => string.Join('\n', new[] { AppId, Name, InstallDirectory, Executable, BuildId, LaunchArguments }
        .Concat(Depots.OrderBy(d => d.Id, StringComparer.Ordinal).Select(d => d.Id + ":" + d.Manifest)));

    public static SteamMetadata Parse(string json)
    {
        if (json.Length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_metadata");
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        if (root.ValueKind != JsonValueKind.Object) throw new SteamIntegrationException("invalid_metadata");
        if (root.TryGetProperty("data", out var data))
        {
            if (!data.TryGetProperty(EndfieldAppId, out root)) throw new SteamIntegrationException("wrong_app");
        }
        if (!root.TryGetProperty("common", out var common))
        {
            var normalized = JsonSerializer.Deserialize<SteamMetadata>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new SteamIntegrationException("invalid_metadata");
            normalized.Validate();
            return normalized;
        }
        static JsonElement Child(JsonElement node, string key) => node.ValueKind == JsonValueKind.Object && node.TryGetProperty(key, out var child) ? child : default;
        static string Text(JsonElement node) => node.ValueKind switch { JsonValueKind.String => node.GetString() ?? "", JsonValueKind.Number => node.GetRawText(), _ => "" };
        static bool Windows(JsonElement node)
        {
            string platforms = Text(Child(Child(node, "config"), "oslist"));
            return platforms.Length == 0 || platforms.Split(',').Any(p => p.Trim().Equals("windows", StringComparison.OrdinalIgnoreCase));
        }
        var config = Child(root, "config");
        var result = new SteamMetadata { Name = Text(Child(common, "name")), InstallDirectory = Text(Child(config, "installdir")) };
        var launches = Child(config, "launch");
        if (launches.ValueKind == JsonValueKind.Object)
        {
            var candidates = launches.EnumerateObject().Select(p => p.Value).Where(Windows)
                .OrderBy(node => Text(Child(node, "type")) is "default" or "" ? 0 : 1);
            var launch = candidates.FirstOrDefault(node => Text(Child(node, "executable")).Length > 0);
            result.Executable = Text(Child(launch, "executable"));
            result.LaunchArguments = Text(Child(launch, "arguments"));
        }
        var depots = Child(root, "depots");
        result.BuildId = Text(Child(Child(Child(depots, "branches"), "public"), "buildid"));
        if (depots.ValueKind == JsonValueKind.Object)
        {
            foreach (var depot in depots.EnumerateObject().Where(p => PositiveId(p.Name) && Windows(p.Value)))
            {
                var manifest = Child(Child(depot.Value, "manifests"), "public");
                string id = Text(manifest.ValueKind == JsonValueKind.Object ? Child(manifest, "gid") : manifest);
                if (id.Length > 0) result.Depots.Add(new SteamDepotMetadata(depot.Name, id));
            }
        }
        return result;
    }
}

internal sealed class SteamIntegrationException(string code, string detail = "") : Exception(code + (detail.Length > 0 ? ": " + detail : ""))
{
    public string Code { get; } = code;
    public string Detail { get; } = detail;
}
