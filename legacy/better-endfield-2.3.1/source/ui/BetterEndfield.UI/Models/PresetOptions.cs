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

    private const string CombatDictionaryResourceName =
        "BetterEndfield.UI.Assets.combat.combat-dictionary.json";

    private const string ChineseNamesResourceName =
        "BetterEndfield.UI.Assets.model.character-names.json";

    private const string EnglishNamesResourceName =
        "BetterEndfield.UI.Assets.model.character-names-en.json";

    public static IReadOnlyDictionary<string, string> CharacterNamesZh { get; } =
        LoadChineseCharacterNames();

    public static IReadOnlyDictionary<string, string> CharacterNamesEn { get; } =
        LoadEnglishCharacterNames();

    public static IReadOnlyDictionary<string, string> CharacterNames =>
        Services.LocalizationService.Instance.IsChinese ? CharacterNamesZh : CharacterNamesEn;

    public static IReadOnlyDictionary<string, string> DungeonNames { get; } =
        LoadDungeonNames();

    public static IReadOnlyDictionary<string, string> WeaponNames { get; } =
        LoadWeaponNames();

    public static IReadOnlyDictionary<string, string> SuitNames { get; } =
        LoadSuitNames();

    public static IReadOnlyDictionary<string, string> SkillNames { get; } =
        LoadSkillNames();

    public static IReadOnlyDictionary<string, int> SkillCategories { get; } =
        LoadSkillCategories();

    public static IReadOnlyList<CharacterOption> Characters { get; } = LoadCharacters();

    public static string GetCharacterName(string? characterId)
    {
        if (string.IsNullOrWhiteSpace(characterId)) return string.Empty;
        bool isZh = Services.LocalizationService.Instance.IsChinese;
        if (isZh)
        {
            if (CharacterNamesZh.TryGetValue(characterId, out string? zhName) && !string.IsNullOrWhiteSpace(zhName))
            {
                return zhName;
            }
        }
        else
        {
            if (CharacterNamesEn.TryGetValue(characterId, out string? enName) && !string.IsNullOrWhiteSpace(enName))
            {
                return enName;
            }
        }

        if (CharacterNamesZh.TryGetValue(characterId, out string? fallbackZh) && !string.IsNullOrWhiteSpace(fallbackZh))
        {
            return fallbackZh;
        }

        return characterId;
    }

    public static void RefreshCharacterDisplayNames()
    {
        foreach (CharacterOption character in Characters)
        {
            character.DisplayName = GetCharacterName(character.Id);
        }
    }

    public static string? FormatDungeonName(string? id, string? name)
    {
        if (!string.IsNullOrWhiteSpace(name)) return name;
        if (string.IsNullOrWhiteSpace(id)) return null;
        if (DungeonNames.TryGetValue(id, out string? mapped) && !string.IsNullOrWhiteSpace(mapped))
        {
            return mapped;
        }
        return id;
    }

    public static string TranslateSemanticStatus(string status) => status.ToLowerInvariant() switch
    {
        "verified" => "已验证",
        "candidate" => "候选",
        "excluded" => "已排除",
        "unknown" => "未知",
        "dropped" => "丢弃",
        _ => status
    };

    public static string TranslateSemanticZone(string zone) => zone.ToLowerInvariant() switch
    {
        "direct" or "direct_damage" => "直伤",
        "attack" => "攻击力",
        "damage" or "damage_boost" => "增伤",
        "amplification" => "增幅",
        "vulnerability" => "脆弱",
        "vulnerability_taken" or "vuln_taken" => "承伤易伤",
        "defense" or "resistance" or "def_res" => "减防/减抗",
        "spell_intensity" or "intensity" => "法术强度",
        "other" => "其他",
        _ => zone
    };

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
            character.DisplayName = GetCharacterName(character.Id);
        }

        return catalog.Characters;
    }

    private static JsonDocument? LoadCombatDictionaryDocument()
    {
        Stream? stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(CombatDictionaryResourceName);
        if (stream == null) return null;
        using (stream)
        {
            return JsonDocument.Parse(stream);
        }
    }

    private static IReadOnlyDictionary<string, string> LoadCategoryNames(string categoryKey)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using JsonDocument? doc = LoadCombatDictionaryDocument();
        if (doc != null && doc.RootElement.TryGetProperty(categoryKey, out JsonElement catEl) &&
            catEl.ValueKind == JsonValueKind.Object)
        {
            foreach (JsonProperty prop in catEl.EnumerateObject())
            {
                string key = prop.Name;
                string name = string.Empty;
                if (prop.Value.ValueKind == JsonValueKind.Object &&
                    prop.Value.TryGetProperty("name", out JsonElement nameEl))
                {
                    name = nameEl.GetString() ?? string.Empty;
                }
                else if (prop.Value.ValueKind == JsonValueKind.String)
                {
                    name = prop.Value.GetString() ?? string.Empty;
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    result[key] = name;
                }
            }
        }
        return result;
    }

    private static IReadOnlyDictionary<string, string> LoadChineseCharacterNames()
    {
        var names = new Dictionary<string, string>(LoadCategoryNames("characters"), StringComparer.OrdinalIgnoreCase);
        Stream? stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(ChineseNamesResourceName);
        if (stream != null)
        {
            using (stream)
            {
                var fallback = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    stream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (fallback != null)
                {
                    foreach (var (key, value) in fallback)
                    {
                        names.TryAdd(key, value);
                    }
                }
            }
        }
        return names;
    }

    private static IReadOnlyDictionary<string, string> LoadEnglishCharacterNames()
    {
        var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        Stream? stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(EnglishNamesResourceName);
        if (stream != null)
        {
            using (stream)
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    stream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (dict != null)
                {
                    foreach (var (key, value) in dict)
                    {
                        names[key] = value;
                    }
                }
            }
        }
        return names;
    }

    private static IReadOnlyDictionary<string, int> LoadSkillCategories()
    {
        var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        using JsonDocument? doc = LoadCombatDictionaryDocument();
        if (doc == null ||
            !doc.RootElement.TryGetProperty("skills", out JsonElement skills) ||
            skills.ValueKind != JsonValueKind.Object)
        {
            return result;
        }
        var categories = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["attack"] = 0,
            ["skill"] = 1,
            ["ultimate"] = 2,
            ["combo"] = 3,
            ["passive"] = 4
        };
        foreach (JsonProperty group in skills.EnumerateObject())
        {
            if (group.Value.ValueKind != JsonValueKind.Object ||
                !group.Value.TryGetProperty("category", out JsonElement categoryValue) ||
                categoryValue.ValueKind != JsonValueKind.String ||
                !categories.TryGetValue(categoryValue.GetString() ?? string.Empty,
                    out int category))
            {
                continue;
            }
            result[group.Name] = category;
            if (!group.Value.TryGetProperty("skillIds", out JsonElement skillIds) ||
                skillIds.ValueKind != JsonValueKind.Array) continue;
            foreach (JsonElement skillId in skillIds.EnumerateArray())
            {
                if (skillId.ValueKind == JsonValueKind.String &&
                    !string.IsNullOrWhiteSpace(skillId.GetString()))
                {
                    result[skillId.GetString()!] = category;
                }
            }
        }
        return result;
    }

    private static IReadOnlyDictionary<string, string> LoadDungeonNames()
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using JsonDocument? doc = LoadCombatDictionaryDocument();
        if (doc != null && doc.RootElement.TryGetProperty("dungeons", out JsonElement dungsEl) &&
            dungsEl.ValueKind == JsonValueKind.Object)
        {
            foreach (JsonProperty prop in dungsEl.EnumerateObject())
            {
                string id = prop.Name;
                if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    string name = prop.Value.TryGetProperty("name", out JsonElement nameEl)
                        ? (nameEl.GetString() ?? string.Empty)
                        : string.Empty;
                    string seriesName = prop.Value.TryGetProperty("seriesName", out JsonElement sEl)
                        ? (sEl.GetString() ?? string.Empty)
                        : string.Empty;

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        result[id] = !string.IsNullOrWhiteSpace(seriesName) && !name.Contains(seriesName)
                            ? $"{seriesName} · {name}"
                            : name;
                    }
                }
            }
        }
        return result;
    }

    private static IReadOnlyDictionary<string, string> LoadWeaponNames() =>
        LoadCategoryNames("weapons");

    private static IReadOnlyDictionary<string, string> LoadSuitNames() =>
        LoadCategoryNames("suits");

    private static IReadOnlyDictionary<string, string> LoadSkillNames() =>
        LoadCategoryNames("skills");
}
