using System.Reflection;
using System.Text.Json;

namespace BetterEndfield.UI.Models;

internal class AssetOption
{
    public string DisplayName { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public string PathHash { get; set; } = string.Empty;
}

internal sealed class ModelOption
{
    public string Path { get; set; } = string.Empty;

    public string PathHash { get; set; } = string.Empty;

    public string BundleHash { get; set; } = string.Empty;
}

internal sealed class ActionOption : AssetOption
{
    public string Id { get; set; } = string.Empty;

    public bool NativeLoop { get; set; }

    public double? Duration { get; set; }
}

internal sealed class CharacterOption
{
    public string Id { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public ModelOption Model { get; set; } = new();

    public AssetOption SitLoop { get; set; } = new();

    public AssetOption SitSpecial { get; set; } = new();

    public AssetOption SitToWalk { get; set; } = new();

    public string DefaultActionId { get; set; } = string.Empty;

    public List<ActionOption> Actions { get; set; } = [];
}

internal sealed class CharacterPresetCatalog
{
    public int SchemaVersion { get; set; }

    public List<CharacterOption> Characters { get; set; } = [];
}

internal static class PresetOptions
{
    private const string CatalogResourceName =
        "BetterEndfield.UI.Assets.model.character-presets.json";

    private const string ChineseNamesResourceName =
        "BetterEndfield.UI.Assets.model.character-names.json";

    public static IReadOnlyDictionary<string, string> CharacterNames { get; } =
        LoadChineseNames();

    public static IReadOnlyList<CharacterOption> Characters { get; } = LoadCharacters();

    public static string NormalizeCharacterId(string value) =>
        value.ToLowerInvariant() switch
        {
            "aglina" => "chr_0013_aglina",
            "lizhiyan" => "chr_0032_lizhiyan",
            _ => value
        };

    public static string NormalizeActionId(string value) =>
        value.ToLowerInvariant() switch
        {
            "aglina_shy2_loop" => "a_actor_aglina_dialog_state_shy2_walk_loop",
            "aglina_runbye" => "a_actor_aglina_dialog_single_runbye",
            "aglina_dash_attack" => "a_actor_aglina_battle_dash_attack",
            "lizhiyan_walk_l" => "a_actor_lizhiyan_dialog_virtual_single_walk_l",
            _ => value
        };

    private static IReadOnlyList<CharacterOption> LoadCharacters()
    {
        using Stream stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(CatalogResourceName) ??
            throw new InvalidOperationException(
                $"Embedded preset catalog was not found: {CatalogResourceName}");
        CharacterPresetCatalog catalog =
            JsonSerializer.Deserialize<CharacterPresetCatalog>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Preset catalog is invalid.");
        if (catalog.SchemaVersion != 1 || catalog.Characters.Count == 0)
        {
            throw new InvalidOperationException(
                $"Unsupported preset catalog schema: {catalog.SchemaVersion}");
        }

        foreach (CharacterOption character in catalog.Characters)
        {
            if (CharacterNames.TryGetValue(character.Id, out string? chineseName))
            {
                character.DisplayName = $"{chineseName}  ·  {character.Id}";
            }
        }

        return catalog.Characters;
    }

    private static IReadOnlyDictionary<string, string> LoadChineseNames()
    {
        using Stream stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(ChineseNamesResourceName) ??
            throw new InvalidOperationException(
                $"Embedded character-name map was not found: {ChineseNamesResourceName}");
        return JsonSerializer.Deserialize<Dictionary<string, string>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ??
            throw new InvalidOperationException("Character-name map is invalid.");
    }
}
