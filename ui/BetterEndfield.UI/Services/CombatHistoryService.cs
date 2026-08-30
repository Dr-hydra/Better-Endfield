using System.Reflection;
using System.Text.Json;
using BetterEndfield.UI.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace BetterEndfield.UI.Services;

internal static class CombatHistoryService
{
    private sealed class MutableCharacter
    {
        public double Damage;
        public ulong Hits;
        public ulong CriticalHits;
        public double[] Categories = new double[CombatRdpsCategories.Count];
    }

    private sealed class MutableTimeline
    {
        public double[] Categories = [];
        public Dictionary<string, double> Characters = new(StringComparer.OrdinalIgnoreCase);
    }

    private sealed class DerivedRecord
    {
        public List<CombatCharacterDamage> Characters { get; } = [];
        public List<CombatTimelinePoint> Timeline { get; } = [];
        public List<CombatBuffInterval> Buffs { get; } = [];
        public List<CombatStaggerInterval> Staggers { get; } = [];
        public List<CombatActionRecord> Actions { get; } = [];
        public double TotalDamage { get; set; }
        public long Hits { get; set; }
        public long CriticalHits { get; set; }
    }

    private const double RankingBarWidth = 360.0;
    private static readonly Dictionary<string, ImageSource?> AvatarCache =
        new(StringComparer.OrdinalIgnoreCase);
    private static readonly SolidColorBrush[] CategoryBrushes = CombatSkillCategories.Colors
        .Select(color => new SolidColorBrush(ParseColor(color)))
        .ToArray();
    private static readonly SolidColorBrush[] RdpsBrushes = CombatRdpsCategories.Colors
        .Select(color => new SolidColorBrush(ParseColor(color)))
        .ToArray();
    private static readonly SolidColorBrush[] CharacterBrushes =
    [
        new(ParseColor("#43C9FF")), new(ParseColor("#FFCE52")),
        new(ParseColor("#FF7A67")), new(ParseColor("#AA82FF")),
        new(ParseColor("#57D99B")), new(ParseColor("#FF8BD4")),
        new(ParseColor("#78A7FF")), new(ParseColor("#D7DFEB"))
    ];

    public static IReadOnlyList<CombatSessionRecord> Load(
        string directory, bool useRdps = false)
    {
        if (!Directory.Exists(directory)) return [];
        var records = new List<CombatSessionRecord>();
        foreach (string path in Directory.EnumerateFiles(directory, "combat-*.json"))
        {
            try
            {
                records.Add(Parse(path, useRdps));
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

    public static Brush CategoryBrush(int category, bool rdps = false)
    {
        SolidColorBrush[] brushes = rdps ? RdpsBrushes : CategoryBrushes;
        return brushes[Math.Clamp(category, 0, brushes.Length - 1)];
    }

    public static string CategoryName(int category, bool rdps = false)
    {
        string[] names = rdps ? CombatRdpsCategories.Names : CombatSkillCategories.Names;
        return names[Math.Clamp(category, 0, names.Length - 1)];
    }

    public static Brush CharacterBrush(int index) =>
        CharacterBrushes[Math.Abs(index) % CharacterBrushes.Length];

    private static CombatSessionRecord Parse(string path, bool useRdps)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(path));
        JsonElement root = document.RootElement;
        int schemaVersion = (int)Integer(root, "schemaVersion");
        if (schemaVersion != 11) throw new InvalidOperationException("仅支持 schema 11");
        if (!root.TryGetProperty("battle", out JsonElement battle) ||
            battle.ValueKind != JsonValueKind.Object ||
            !root.TryGetProperty("summary", out JsonElement summary) ||
            summary.ValueKind != JsonValueKind.Object)
        {
            throw new InvalidOperationException("schema 11 结构不完整");
        }
        double duration = Number(battle, "durationSeconds");
        if (duration <= 0) throw new InvalidOperationException("战斗时长无效");
        DerivedRecord derived = DeriveRecord(root, useRdps, duration);
        bool usesRdps = useRdps;
        BuildSegments(derived.Characters, usesRdps);
        JsonElement dictionary = root.TryGetProperty(
            "dictionary", out JsonElement dictionaryValue) ? dictionaryValue : default;
        string dungeonId = Text(battle, "dungeonId");
        string dungeonName = Text(battle, "dungeonName");
        string modeId = Text(battle, "modeId");
        return new CombatSessionRecord
        {
            Path = path,
            FileName = Path.GetFileName(path),
            StartedAt = ParseStartedAt(root, path),
            DurationSeconds = duration,
            TotalDamage = derived.TotalDamage,
            Dps = useRdps ? Number(summary, "rdps") :
                derived.TotalDamage / duration,
            UsesRdps = usesRdps,
            HasRdpsData = true,
            HitCount = derived.Hits,
            CriticalCount = derived.CriticalHits,
            Characters = derived.Characters,
            Timeline = derived.Timeline,
            BuffIntervals = derived.Buffs,
            Squad = ParseSquad(root),
            StaggerIntervals = derived.Staggers,
            Actions = derived.Actions,
            DungeonId = string.IsNullOrWhiteSpace(dungeonId) ? null : dungeonId,
            DungeonName = string.IsNullOrWhiteSpace(dungeonName) ? null : dungeonName,
            ModeId = string.IsNullOrWhiteSpace(modeId) ? null : modeId,
            SemanticCatalogSchema = (int)Integer(dictionary, "schemaVersion"),
            SemanticGameVersion = Text(dictionary, "gameVersion"),
            SemanticHotfixVersion = Text(dictionary, "hotfixVersion")
        };
    }

    private static DerivedRecord DeriveRecord(
        JsonElement root, bool useRdps, double duration)
    {
        if (!root.TryGetProperty("actions", out JsonElement actions) ||
            actions.ValueKind != JsonValueKind.Array ||
            !root.TryGetProperty("effects", out JsonElement effects) ||
            effects.ValueKind != JsonValueKind.Array)
        {
            throw new InvalidOperationException("schema 11 缺少操作或结果事件");
        }
        var result = new DerivedRecord();
        foreach (JsonElement action in actions.EnumerateArray())
        {
            string actorId = NormalizeCharacterId(Text(action, "actorId"));
            if (string.IsNullOrWhiteSpace(actorId)) continue;
            result.Actions.Add(new CombatActionRecord
            {
                Id = (ulong)Math.Max(0, Integer(action, "id")),
                StartTime = Number(action, "start"),
                EndTime = NullableNumber(action, "end"),
                ObservedUntil = Number(action, "observedUntil",
                    Number(action, "end", Number(action, "start"))),
                ActorId = actorId,
                ActionType = Text(action, "type"),
                SkillId = Text(action, "skillId"),
                ParentActionId = (ulong)Math.Max(0, Integer(action, "parentActionId")),
                Result = Text(action, "result")
            });
        }

        var direct = new Dictionary<string, MutableCharacter>(StringComparer.OrdinalIgnoreCase);
        var rdps = new Dictionary<string, MutableCharacter>(StringComparer.OrdinalIgnoreCase);
        var timeline = new SortedDictionary<int, MutableTimeline>();
        var openStatuses = new Dictionary<string, JsonElement>(StringComparer.Ordinal);
        foreach (JsonElement effect in effects.EnumerateArray())
        {
            string type = Text(effect, "type");
            if (type == "damage")
            {
                string actor = NormalizeCharacterId(Text(effect, "sourceId"));
                if (string.IsNullOrWhiteSpace(actor)) actor = "<unknown>";
                string skill = Text(effect, "skillId");
                double amount = Math.Abs(Number(effect, "value"));
                bool critical = Boolean(effect, "critical");
                int category = SkillCategory(skill);
                result.TotalDamage += amount;
                result.Hits++;
                if (critical) result.CriticalHits++;
                MutableCharacter directCharacter = GetCharacter(direct, actor);
                AddHit(directCharacter, amount, critical);
                directCharacter.Categories[category] += amount;

                var shares = new Dictionary<(string Source, int Kind), double>();
                if (effect.TryGetProperty("rdpsShares", out JsonElement shareValues) &&
                    shareValues.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement share in shareValues.EnumerateArray())
                    {
                        string source = NormalizeCharacterId(Text(share, "sourceId"));
                        double fraction = Number(share, "fraction");
                        int kind = (int)Math.Clamp(Integer(share, "contributionType"),
                            0, CombatRdpsCategories.Count - 1);
                        if (string.IsNullOrWhiteSpace(source) || source == actor ||
                            fraction <= 0 || !double.IsFinite(fraction)) continue;
                        shares[(source, kind)] = shares.GetValueOrDefault((source, kind)) + fraction;
                    }
                }
                double rawTransferred = shares.Values.Sum(value => Math.Max(0, value));
                double scale = rawTransferred > 0.999999 ? 0.999999 / rawTransferred : 1;
                double transferred = Math.Min(0.999999, rawTransferred);
                double retained = amount * (1 - transferred);
                MutableCharacter rdpsAttacker = GetCharacter(rdps, actor);
                AddHit(rdpsAttacker, retained, critical);
                rdpsAttacker.Categories[0] += retained;
                foreach (var share in shares)
                {
                    double credited = amount * Math.Max(0, share.Value) * scale;
                    MutableCharacter support = GetCharacter(rdps, share.Key.Source);
                    support.Damage += credited;
                    support.Categories[share.Key.Kind] += credited;
                }

                int bucketIndex = (int)Math.Max(0, Math.Floor(Number(effect, "time") / 0.25));
                if (!timeline.TryGetValue(bucketIndex, out MutableTimeline? bucket))
                {
                    bucket = new MutableTimeline
                    {
                        Categories = new double[useRdps
                            ? CombatRdpsCategories.Count : CombatSkillCategories.Count]
                    };
                    timeline[bucketIndex] = bucket;
                }
                if (useRdps)
                {
                    bucket.Categories[0] += retained;
                    bucket.Characters[actor] = bucket.Characters.GetValueOrDefault(actor) + retained;
                    foreach (var share in shares)
                    {
                        double credited = amount * Math.Max(0, share.Value) * scale;
                        bucket.Categories[share.Key.Kind] += credited;
                        bucket.Characters[share.Key.Source] =
                            bucket.Characters.GetValueOrDefault(share.Key.Source) + credited;
                    }
                }
                else
                {
                    bucket.Categories[category] += amount;
                    bucket.Characters[actor] = bucket.Characters.GetValueOrDefault(actor) + amount;
                }
                continue;
            }

            string instanceId = Text(effect, "statusInstanceId");
            if (string.IsNullOrWhiteSpace(instanceId)) continue;
            if (type is "statusApply" or "statusRefresh")
            {
                openStatuses[instanceId] = effect;
            }
            else if (type == "statusRemove" && openStatuses.Remove(instanceId, out JsonElement start))
            {
                AppendStatusInterval(result, start, Number(effect, "time"));
            }
        }
        foreach (JsonElement start in openStatuses.Values)
            AppendStatusInterval(result, start, duration);

        Dictionary<string, MutableCharacter> selected = useRdps ? rdps : direct;
        foreach ((string id, MutableCharacter value) in selected
            .OrderByDescending(pair => pair.Value.Damage))
        {
            result.Characters.Add(new CombatCharacterDamage
            {
                Id = id,
                DisplayName = id == "<unknown>"
                    ? (LocalizationService.Instance.IsChinese ? "未知来源" : "Unknown Source")
                    : PresetOptions.GetCharacterName(id),
                AvatarSource = LoadAvatar(id),
                Damage = value.Damage,
                Hits = value.Hits,
                CriticalHits = value.CriticalHits,
                DamageByCategory = value.Categories.Take(useRdps
                    ? CombatRdpsCategories.Count : CombatSkillCategories.Count).ToArray()
            });
        }
        result.Timeline.AddRange(timeline.Select(pair => new CombatTimelinePoint
        {
            Time = pair.Key * 0.25,
            DamageByCategory = pair.Value.Categories,
            DamageByCharacter = pair.Value.Characters
        }));
        result.Actions.Sort((left, right) => left.StartTime.CompareTo(right.StartTime));
        result.Buffs.Sort((left, right) => left.StartTime.CompareTo(right.StartTime));
        result.Staggers.Sort((left, right) => left.StartTime.CompareTo(right.StartTime));
        return result;
    }

    private static MutableCharacter GetCharacter(
        Dictionary<string, MutableCharacter> values, string id)
    {
        if (!values.TryGetValue(id, out MutableCharacter? value))
        {
            value = new MutableCharacter();
            values[id] = value;
        }
        return value;
    }

    private static void AddHit(MutableCharacter value, double damage, bool critical)
    {
        value.Damage += damage;
        value.Hits++;
        if (critical) value.CriticalHits++;
    }

    private static void AppendStatusInterval(
        DerivedRecord result, JsonElement start, double end)
    {
        double startTime = Number(start, "time");
        if (end <= startTime) return;
        string statusId = Text(start, "statusId");
        string targetId = NormalizeCharacterId(Text(start, "targetId"));
        if (Text(start, "statusKind") == "stagger")
        {
            result.Staggers.Add(new CombatStaggerInterval
            {
                TargetId = targetId,
                StartTime = startTime,
                EndTime = end,
                BuffId = statusId
            });
            return;
        }
        result.Buffs.Add(new CombatBuffInterval
        {
            StartTime = startTime,
            EndTime = end,
            BuffId = statusId,
            SourceId = NormalizeCharacterId(Text(start, "creditedActorId",
                Text(start, "sourceId"))),
            TargetId = targetId,
            TargetEntityId = Text(start, "targetEntityId"),
            OriginSkillId = Text(start, "skillId"),
            SourceEntityId = Text(start, "sourceEntityId"),
            SourceKind = Text(start, "sourceKind", "unknown"),
            SourceTemplateId = Text(start, "sourceTemplateId"),
            SourceAttribution = Text(start, "sourceAttribution", "unknown"),
            AttributionReason = Text(start, "attributionReason"),
            ContributionType = (int)Math.Clamp(Integer(start, "contributionType"),
                0, CombatRdpsCategories.Count - 1),
            EffectKind = (int)Math.Clamp(Integer(start, "effectKind"), 0, 2),
            EffectMin = Number(start, "effectMin"),
            EffectMax = Number(start, "effectMax")
        });
    }

    private static IReadOnlyList<CombatStaggerInterval> ParseStaggerIntervals(JsonElement root)
    {
        if (!root.TryGetProperty("staggerIntervals", out JsonElement staggers) ||
            staggers.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatStaggerInterval>();
        foreach (JsonElement item in staggers.EnumerateArray())
        {
            string targetId = Text(item, "targetId");
            double start = Number(item, "start");
            double end = Number(item, "end");
            string buffId = Text(item, "buffId");
            if (end > start)
            {
                result.Add(new CombatStaggerInterval
                {
                    TargetId = targetId,
                    StartTime = start,
                    EndTime = end,
                    BuffId = buffId
                });
            }
        }
        return result;
    }

    private static IReadOnlyList<CombatSquadMember> ParseSquad(JsonElement root)
    {
        if (!root.TryGetProperty("squad", out JsonElement squad) ||
            squad.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatSquadMember>();
        foreach (JsonElement item in squad.EnumerateArray())
        {
            string charId = Text(item, "charId");
            if (string.IsNullOrWhiteSpace(charId)) continue;
            result.Add(new CombatSquadMember
            {
                CharId = NormalizeCharacterId(charId),
                CharInstId = Text(item, "charInstId"),
                Potential = (uint)Integer(item, "potential"),
                Level = (int)Integer(item, "level"),
                BreakStage = (int)Integer(item, "breakStage"),
                SkillGroupCount = ArrayLength(item, "skillGroups"),
                EquipSuitCount = ArrayLength(item, "equipSuits")
            });
        }
        return result;
    }

    private static IReadOnlyList<CombatSemanticAuditItem> ParseUnresolvedSemantics(
        JsonElement root)
    {
        if (!root.TryGetProperty("unresolvedSemantics", out JsonElement values) ||
            values.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatSemanticAuditItem>();
        foreach (JsonElement value in values.EnumerateArray())
        {
            string buffId = Text(value, "buffId");
            if (string.IsNullOrWhiteSpace(buffId)) continue;
            result.Add(new CombatSemanticAuditItem
            {
                BuffId = buffId,
                Status = Text(value, "status"),
                ObservedZone = Text(value, "observedZone"),
                Count = Integer(value, "count"),
                ElementMismatch = Boolean(value, "elementMismatch")
            });
        }
        return result;
    }

    private static List<CombatCharacterDamage> ParseCharacters(
        JsonElement root, string propertyName, bool useRdps, int schemaVersion)
    {
        var result = new List<CombatCharacterDamage>();
        Dictionary<string, double[]> legacyCategories = ParseLegacySkillCategories(root);
        if (!root.TryGetProperty(propertyName, out JsonElement map) ||
            map.ValueKind != JsonValueKind.Object)
        {
            return result;
        }
        foreach (JsonProperty property in map.EnumerateObject())
        {
            JsonElement value = property.Value;
            string id = NormalizeCharacterId(property.Name);
            int categoryCount = useRdps
                ? CombatRdpsCategories.Count : CombatSkillCategories.Count;
            double[] damageByCategory = new double[categoryCount];
            string categoryProperty = useRdps
                ? "rdpsByContribution" : "damageByCategory";
            if (value.TryGetProperty(categoryProperty, out JsonElement categories) &&
                categories.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement amount in categories.EnumerateArray())
                {
                    int destination = useRdps && schemaVersion < 7
                        ? CombatRdpsCategories.LegacyIndex(index) : index;
                    if (destination >= damageByCategory.Length) break;
                    if (amount.TryGetDouble(out double parsed))
                        damageByCategory[destination] += parsed;
                    ++index;
                }
            }
            else if (!useRdps && legacyCategories.TryGetValue(id, out double[]? legacy))
            {
                Array.Copy(legacy, damageByCategory, damageByCategory.Length);
            }
            double characterDamage = Number(value, "damage");
            if (damageByCategory.Sum() <= 0 && characterDamage > 0)
                damageByCategory[useRdps ? 0 : damageByCategory.Length - 1] = characterDamage;
            result.Add(new CombatCharacterDamage
            {
                Id = id,
                DisplayName = id == "<unknown>"
                    ? (LocalizationService.Instance.IsChinese ? "未知来源" : "Unknown Source")
                    : PresetOptions.GetCharacterName(id),
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

    private static IReadOnlyList<CombatTimelinePoint> ParseTimeline(
        JsonElement root, bool useRdps, int schemaVersion)
    {
        if (!root.TryGetProperty("timeline", out JsonElement timeline) ||
            timeline.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatTimelinePoint>();
        foreach (JsonElement item in timeline.EnumerateArray())
        {
            int categoryCount = useRdps
                ? CombatRdpsCategories.Count : CombatSkillCategories.Count;
            double[] values = new double[categoryCount];
            string categoryProperty = useRdps
                ? "rdpsByContribution" : "damageByCategory";
            if (item.TryGetProperty(categoryProperty, out JsonElement categories) &&
                categories.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement amount in categories.EnumerateArray())
                {
                    int destination = useRdps && schemaVersion < 7
                        ? CombatRdpsCategories.LegacyIndex(index) : index;
                    if (destination >= values.Length) break;
                    if (amount.TryGetDouble(out double parsed)) values[destination] += parsed;
                    ++index;
                }
            }
            else if (!useRdps && item.TryGetProperty("damageByType", out JsonElement legacyTypes) &&
                legacyTypes.ValueKind == JsonValueKind.Array)
            {
                values[^1] = legacyTypes.EnumerateArray()
                    .Where(amount => amount.ValueKind == JsonValueKind.Number)
                    .Sum(amount => amount.GetDouble());
            }
            var byCharacter = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            string characterProperty = useRdps ? "rdpsByCharacter" : "damageByCharacter";
            if (item.TryGetProperty(characterProperty, out JsonElement characters) &&
                characters.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty character in characters.EnumerateObject())
                    byCharacter[NormalizeCharacterId(character.Name)] = character.Value.GetDouble();
            }
            if (useRdps && values.Sum() <= 0 && byCharacter.Count > 0)
                values[0] = byCharacter.Values.Sum();
            result.Add(new CombatTimelinePoint
            {
                Time = Number(item, "time"),
                DamageByCategory = values,
                DamageByCharacter = byCharacter
            });
        }
        return result.OrderBy(point => point.Time).ToArray();
    }

    private static IReadOnlyList<CombatBuffInterval> ParseBuffIntervals(
        JsonElement root, int schemaVersion)
    {
        if (!root.TryGetProperty("buffIntervals", out JsonElement intervals) ||
            intervals.ValueKind != JsonValueKind.Array)
        {
            return [];
        }
        var result = new List<CombatBuffInterval>();
        foreach (JsonElement item in intervals.EnumerateArray())
        {
            double start = Number(item, "start");
            double end = Number(item, "end");
            string source = Text(item, "sourceId");
            if (end <= start || string.IsNullOrWhiteSpace(source)) continue;
            result.Add(new CombatBuffInterval
            {
                StartTime = start,
                EndTime = end,
                BuffId = Text(item, "buffId"),
                SourceId = NormalizeCharacterId(source),
                TargetId = NormalizeCharacterId(Text(item, "targetId")),
                OriginSkillId = Text(item, "originSkillId"),
                ContributionType = schemaVersion < 7
                    ? CombatRdpsCategories.LegacyIndex((int)Integer(item, "contributionType"))
                    : (int)Math.Clamp(Integer(item, "contributionType"), 0,
                        CombatRdpsCategories.Count - 1),
                EffectKind = (int)Math.Clamp(Integer(item, "effectKind"), 0, 2),
                EffectMin = Number(item, "effectMin"),
                EffectMax = Number(item, "effectMax")
            });
        }
        return result.OrderBy(interval => interval.StartTime).ToArray();
    }

    private static void BuildSegments(
        IReadOnlyList<CombatCharacterDamage> characters, bool useRdps)
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
                        Brush = CategoryBrush(category, useRdps),
                        Width = RankingBarWidth * character.Damage / maximum * amount / typedTotal,
                        ToolTip = $"{CategoryName(category, useRdps)}：{CombatNumberFormatter.Format(amount)}"
                    });
                }
            }
            character.Segments = segments;
        }
    }

    internal static ImageSource? LoadAvatar(string id)
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
        return PresetOptions.SkillCategories.TryGetValue(skill, out int category)
            ? category
            : 5;
    }

    private static DateTimeOffset ParseStartedAt(JsonElement root, string path)
    {
        JsonElement source = root.TryGetProperty("battle", out JsonElement battle) &&
            battle.ValueKind == JsonValueKind.Object ? battle : root;
        long seconds = Integer(source, "startedUnixSeconds");
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
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) && value.TryGetDouble(out double result)
            ? result
            : fallback;

    private static double? NullableNumber(JsonElement element, string property) =>
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) &&
        value.ValueKind == JsonValueKind.Number && value.TryGetDouble(out double result)
            ? result
            : null;

    private static long Integer(JsonElement element, string property) =>
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) && value.TryGetInt64(out long result)
            ? result
            : 0;

    private static string Text(
        JsonElement element, string property, string fallback = "") =>
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) &&
        value.ValueKind == JsonValueKind.String
            ? value.GetString() ?? string.Empty
            : fallback;

    private static bool Boolean(JsonElement element, string property) =>
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) &&
        value.ValueKind is JsonValueKind.True or JsonValueKind.False &&
        value.GetBoolean();

    private static int ArrayLength(JsonElement element, string property) =>
        element.ValueKind == JsonValueKind.Object &&
        element.TryGetProperty(property, out JsonElement value) &&
        value.ValueKind == JsonValueKind.Array
            ? value.GetArrayLength()
            : 0;

    private static Windows.UI.Color ParseColor(string value) => ColorHelper.FromArgb(
        255,
        Convert.ToByte(value.Substring(1, 2), 16),
        Convert.ToByte(value.Substring(3, 2), 16),
        Convert.ToByte(value.Substring(5, 2), 16));
}
