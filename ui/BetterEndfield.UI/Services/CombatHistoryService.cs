using System.Reflection;
using System.Text.Json;
using BetterEndfield.UI.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace BetterEndfield.UI.Services;

internal static class CombatHistoryService
{
    private const double RankingBarWidth = 360.0;
    private static readonly Dictionary<string, ImageSource?> AvatarCache =
        new(StringComparer.OrdinalIgnoreCase);
    private static readonly SolidColorBrush[] CategoryBrushes = CombatSkillCategories.Colors
        .Select(color => new SolidColorBrush(ParseColor(color)))
        .ToArray();
    private static readonly SolidColorBrush[] CharacterBrushes =
    [
        new(ParseColor("#43C9FF")), new(ParseColor("#FFCE52")),
        new(ParseColor("#FF7A67")), new(ParseColor("#AA82FF")),
        new(ParseColor("#57D99B")), new(ParseColor("#FF8BD4")),
        new(ParseColor("#78A7FF")), new(ParseColor("#D7DFEB"))
    ];

    public static IReadOnlyList<CombatSessionRecord> Load(string directory)
    {
        if (!Directory.Exists(directory)) return [];
        var records = new List<CombatSessionRecord>();
        foreach (string path in Directory.EnumerateFiles(directory, "combat-*.json"))
        {
            try
            {
                records.Add(Parse(path));
            }
            catch (IOException)
            {
            }
            catch (JsonException)
            {
            }
            catch (InvalidOperationException)
            {
            }
        }
        return records.OrderByDescending(record => record.StartedAt).ToArray();
    }

    public static Brush CategoryBrush(int category) =>
        CategoryBrushes[Math.Clamp(category, 0, CategoryBrushes.Length - 1)];

    public static Brush CharacterBrush(int index) =>
        CharacterBrushes[Math.Abs(index) % CharacterBrushes.Length];

    private static CombatSessionRecord Parse(string path)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(path));
        JsonElement root = document.RootElement;
        double total = Number(root, "totalDamage");
        double duration = Number(root, "durationSeconds");
        var characters = ParseCharacters(root);
        BuildSegments(characters);
        return new CombatSessionRecord
        {
            Path = path,
            FileName = Path.GetFileName(path),
            StartedAt = ParseStartedAt(root, path),
            DurationSeconds = duration,
            TotalDamage = total,
            Dps = Number(root, "dps", duration > 0 ? total / duration : 0),
            HitCount = Integer(root, "hitCount"),
            CriticalCount = Integer(root, "criticalCount"),
            Characters = characters,
            Timeline = ParseTimeline(root)
        };
    }

    private static List<CombatCharacterDamage> ParseCharacters(JsonElement root)
    {
        var result = new List<CombatCharacterDamage>();
        Dictionary<string, double[]> legacyCategories = ParseLegacySkillCategories(root);
        if (!root.TryGetProperty("characters", out JsonElement map) ||
            map.ValueKind != JsonValueKind.Object)
        {
            return result;
        }
        foreach (JsonProperty property in map.EnumerateObject())
        {
            JsonElement value = property.Value;
            string id = NormalizeCharacterId(property.Name);
            double[] damageByCategory = new double[CombatSkillCategories.Count];
            if (value.TryGetProperty("damageByCategory", out JsonElement categories) &&
                categories.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement amount in categories.EnumerateArray())
                {
                    if (index >= damageByCategory.Length) break;
                    if (amount.TryGetDouble(out double parsed)) damageByCategory[index] = parsed;
                    ++index;
                }
            }
            else if (legacyCategories.TryGetValue(id, out double[]? legacy))
            {
                Array.Copy(legacy, damageByCategory, damageByCategory.Length);
            }
            double characterDamage = Number(value, "damage");
            if (damageByCategory.Sum() <= 0 && characterDamage > 0)
                damageByCategory[^1] = characterDamage;
            result.Add(new CombatCharacterDamage
            {
                Id = id,
                DisplayName = PresetOptions.CharacterNames.TryGetValue(id, out string? name)
                    ? name
                    : id == "<unknown>" ? "未知来源" : id,
                AvatarSource = LoadAvatar(id),
                Damage = characterDamage,
                Hits = (ulong)Math.Max(0, Integer(value, "hits")),
                CriticalHits = (ulong)Math.Max(0, Integer(value, "criticalHits")),
                DamageByCategory = damageByCategory
            });
        }
        return result.OrderByDescending(character => character.Damage).ToList();
    }

    private static Dictionary<string, double[]> ParseLegacySkillCategories(JsonElement root)
    {
        var result = new Dictionary<string, double[]>(StringComparer.OrdinalIgnoreCase);
        if (!root.TryGetProperty("skills", out JsonElement skills) ||
            skills.ValueKind != JsonValueKind.Object)
        {
            return result;
        }
        foreach (JsonProperty property in skills.EnumerateObject())
        {
            string? characterId = CharacterIdFromSkill(property.Name);
            if (characterId is null) continue;
            if (!result.TryGetValue(characterId, out double[]? categories))
            {
                categories = new double[CombatSkillCategories.Count];
                result[characterId] = categories;
            }
            categories[SkillCategory(property.Name)] += Number(property.Value, "damage");
        }
        return result;
    }

    private static IReadOnlyList<CombatTimelinePoint> ParseTimeline(JsonElement root)
    {
        if (!root.TryGetProperty("timeline", out JsonElement timeline) ||
            timeline.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatTimelinePoint>();
        foreach (JsonElement item in timeline.EnumerateArray())
        {
            double[] values = new double[CombatSkillCategories.Count];
            if (item.TryGetProperty("damageByCategory", out JsonElement categories) &&
                categories.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement amount in categories.EnumerateArray())
                {
                    if (index >= values.Length) break;
                    if (amount.TryGetDouble(out double parsed)) values[index] = parsed;
                    ++index;
                }
            }
            else if (item.TryGetProperty("damageByType", out JsonElement legacyTypes) &&
                legacyTypes.ValueKind == JsonValueKind.Array)
            {
                values[^1] = legacyTypes.EnumerateArray()
                    .Where(amount => amount.ValueKind == JsonValueKind.Number)
                    .Sum(amount => amount.GetDouble());
            }
            var byCharacter = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            if (item.TryGetProperty("damageByCharacter", out JsonElement characters) &&
                characters.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty character in characters.EnumerateObject())
                    byCharacter[NormalizeCharacterId(character.Name)] = character.Value.GetDouble();
            }
            result.Add(new CombatTimelinePoint
            {
                Time = Number(item, "time"),
                DamageByCategory = values,
                DamageByCharacter = byCharacter
            });
        }
        return result.OrderBy(point => point.Time).ToArray();
    }

    private static void BuildSegments(IReadOnlyList<CombatCharacterDamage> characters)
    {
        double maximum = Math.Max(1.0, characters.Count == 0
            ? 1.0
            : characters.Max(character => character.Damage));
        foreach (CombatCharacterDamage character in characters)
        {
            var segments = new List<CombatDamageSegment>();
            double typedTotal = character.DamageByCategory.Sum(value => Math.Max(0, value));
            if (typedTotal <= 0)
            {
                segments.Add(new CombatDamageSegment
                {
                    Brush = new SolidColorBrush(ColorHelper.FromArgb(255, 148, 158, 177)),
                    Width = RankingBarWidth * character.Damage / maximum,
                    ToolTip = $"未分类：{CombatNumberFormatter.Format(character.Damage)}"
                });
            }
            else
            {
                foreach ((double amount, int category) in character.DamageByCategory
                    .Select((amount, category) => (amount, category)))
                {
                    if (amount <= 0) continue;
                    segments.Add(new CombatDamageSegment
                    {
                        Brush = CategoryBrushes[category],
                        Width = RankingBarWidth * character.Damage / maximum * amount / typedTotal,
                        ToolTip = $"{CombatSkillCategories.Names[category]}：{CombatNumberFormatter.Format(amount)}"
                    });
                }
            }
            character.Segments = segments;
        }
    }

    private static ImageSource? LoadAvatar(string id)
    {
        if (AvatarCache.TryGetValue(id, out ImageSource? cached)) return cached;
        string assetId = id is "chr_0002_endminm" or "chr_0003_endminf"
            ? "chr_9000_endmin"
            : id;
        string resourceName = $"BetterEndfield.UI.Assets.combat.{assetId}.png";
        using Stream? source = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (source is null)
        {
            AvatarCache[id] = null;
            return null;
        }
        using var memory = new MemoryStream();
        source.CopyTo(memory);
        memory.Position = 0;
        using var randomAccess = memory.AsRandomAccessStream();
        var bitmap = new BitmapImage();
        bitmap.SetSource(randomAccess);
        AvatarCache[id] = bitmap;
        return bitmap;
    }

    private static string NormalizeCharacterId(string value)
    {
        int separator = value.LastIndexOf('_');
        return separator > 0 && value[(separator + 1)..].All(char.IsDigit)
            ? value[..separator]
            : value;
    }

    private static string? CharacterIdFromSkill(string skill)
    {
        System.Text.RegularExpressions.Match match =
            System.Text.RegularExpressions.Regex.Match(
                skill,
                @"^((?:chr|eny)_\d{4}_[^_]+)_",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                System.Text.RegularExpressions.RegexOptions.CultureInvariant);
        return match.Success ? NormalizeCharacterId(match.Groups[1].Value) : null;
    }

    private static int SkillCategory(string skill)
    {
        if (skill.Contains("_combo", StringComparison.OrdinalIgnoreCase)) return 3;
        if (skill.Contains("_ultimate_skill", StringComparison.OrdinalIgnoreCase)) return 2;
        if (skill.Contains("_normal_skill", StringComparison.OrdinalIgnoreCase)) return 1;
        if (skill.Contains("_attack", StringComparison.OrdinalIgnoreCase)) return 0;
        if (skill.Contains("_passive_skill", StringComparison.OrdinalIgnoreCase)) return 4;
        return 5;
    }

    private static DateTimeOffset ParseStartedAt(JsonElement root, string path)
    {
        long seconds = Integer(root, "startedUnixSeconds");
        if (seconds > 0)
        {
            try { return DateTimeOffset.FromUnixTimeSeconds(seconds); }
            catch (ArgumentOutOfRangeException) { }
        }
        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(
            Path.GetFileName(path), @"^combat-(\d+)-");
        if (match.Success && long.TryParse(match.Groups[1].Value, out seconds))
        {
            try { return DateTimeOffset.FromUnixTimeSeconds(seconds); }
            catch (ArgumentOutOfRangeException) { }
        }
        return File.GetLastWriteTime(path);
    }

    private static double Number(JsonElement element, string property, double fallback = 0) =>
        element.TryGetProperty(property, out JsonElement value) && value.TryGetDouble(out double result)
            ? result
            : fallback;

    private static long Integer(JsonElement element, string property) =>
        element.TryGetProperty(property, out JsonElement value) && value.TryGetInt64(out long result)
            ? result
            : 0;

    private static Windows.UI.Color ParseColor(string value) => ColorHelper.FromArgb(
        255,
        Convert.ToByte(value.Substring(1, 2), 16),
        Convert.ToByte(value.Substring(3, 2), 16),
        Convert.ToByte(value.Substring(5, 2), 16));
}
