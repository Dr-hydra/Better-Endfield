using Microsoft.UI.Xaml.Media;

namespace BetterEndfield.UI.Models;

internal static class CombatSkillCategories
{
    public const int Count = 6;

    public static readonly string[] Names =
    [
        "普攻", "战技", "终结技", "连携技", "被动", "其他"
    ];

    public static readonly string[] Colors =
    [
        "#D3D8E1", "#43C9FF", "#FF6741",
        "#AA82FF", "#5BD68B", "#8F98AA"
    ];
}

internal static class CombatRdpsCategories
{
    public const int Count = 9;

    public static readonly string[] Names =
    [
        "直伤", "攻击力", "增伤", "增幅", "脆弱",
        "承伤易伤", "减防/减抗", "法术强度", "其他"
    ];

    public static readonly string[] Colors =
    [
        "#D3D8E1", "#FFCE52", "#43C9FF", "#FF9148", "#FF7A67",
        "#FF4F82", "#57D99B", "#54B3FF", "#8F98AA"
    ];

    public static int LegacyIndex(int index) => index switch
    {
        0 => 0, // direct
        1 => 1, // attack boost
        2 => 2, // damage boost
        3 => 5, // vulnerability
        4 or 5 => 6, // defense/resistance reduction
        _ => 8
    };
}

internal static class CombatNumberFormatter
{
    private static readonly string[] DecimalUnits =
    [
        "万", "×10万", "×100万", "×1000万",
        "亿", "×10亿", "×100亿", "×1000亿",
        "万亿", "×10万亿", "×100万亿", "×1000万亿"
    ];

    public static string Format(double value)
    {
        double absolute = Math.Abs(value);
        if (absolute < 10_000)
        {
            return value.ToString("N0");
        }

        int exponent = (int)Math.Floor(Math.Log10(absolute));
        if (exponent >= 4 && exponent <= 15)
        {
            return $"{value / Math.Pow(10, exponent):0.##}{DecimalUnits[exponent - 4]}";
        }

        return $"{value / Math.Pow(10, exponent):0.##}×10^{exponent}";
    }
}

internal sealed class CombatDamageSegment
{
    public required Brush Brush { get; init; }

    public double Width { get; init; }

    public required string ToolTip { get; init; }
}

internal sealed class CombatCharacterDamage
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }

    public ImageSource? AvatarSource { get; init; }

    public double Damage { get; init; }

    public ulong Hits { get; init; }

    public ulong CriticalHits { get; init; }

    public double[] DamageByCategory { get; init; } = new double[CombatSkillCategories.Count];

    public string DamageText => CombatNumberFormatter.Format(Damage);

    public string HitSummary => $"{Hits} 命中 · {CriticalHits} 暴击";

    public IReadOnlyList<CombatDamageSegment> Segments { get; set; } = [];
}

internal sealed class CombatTimelinePoint
{
    public double Time { get; init; }

    public double[] DamageByCategory { get; init; } = new double[CombatSkillCategories.Count];

    public IReadOnlyDictionary<string, double> DamageByCharacter { get; init; } =
        new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
}

internal sealed class CombatBuffInterval
{
    public double StartTime { get; init; }

    public double EndTime { get; init; }

    public required string BuffId { get; init; }

    public required string SourceId { get; init; }

    public required string TargetId { get; init; }

    public string TargetEntityId { get; init; } = string.Empty;

    public required string OriginSkillId { get; init; }

    public string SourceEntityId { get; init; } = string.Empty;

    public string SourceKind { get; init; } = "unknown";

    public string SourceTemplateId { get; init; } = string.Empty;

    public string SourceAttribution { get; init; } = "unknown";

    public string AttributionReason { get; init; } = string.Empty;

    public int ContributionType { get; init; }

    public int EffectKind { get; init; }

    public double EffectMin { get; init; }

    public double EffectMax { get; init; }

    public string SourceName => PresetOptions.CharacterNames.TryGetValue(
        SourceId, out string? name) ? name : SourceId;

    public string ContributionName => CombatRdpsCategories.Names[
        Math.Clamp(ContributionType, 0, CombatRdpsCategories.Count - 1)];

    public string EffectText
    {
        get
        {
            if (EffectKind is not (1 or 2)) return string.Empty;
            string label = EffectKind == 1 ? "实际增幅" : "等效贡献";
            string minimum = EffectMin.ToString("+0.##%;-0.##%;0%");
            string maximum = EffectMax.ToString("+0.##%;-0.##%;0%");
            return Math.Abs(EffectMax - EffectMin) <= 0.00005
                ? $"{label}：{maximum}"
                : $"{label}：{minimum} – {maximum}";
        }
    }

    public string ToolTip => string.Join("\n",
        new[]
        {
            $"{SourceName} · {ContributionName}",
            EffectText,
            $"持续时间：{StartTime:0.00}s – {EndTime:0.00}s"
        }.Where(line => !string.IsNullOrWhiteSpace(line)));
}

internal sealed class CombatLegendItem
{
    public required string Label { get; init; }

    public required Brush Brush { get; init; }
}

internal sealed class CombatSemanticAuditItem
{
    public required string BuffId { get; init; }

    public required string Status { get; init; }

    public required string ObservedZone { get; init; }

    public long Count { get; init; }

    public bool ElementMismatch { get; init; }
}

internal sealed class CombatSquadMember
{
    public required string CharId { get; init; }

    public required string CharInstId { get; init; }

    public uint Potential { get; init; }

    public int Level { get; init; }

    public int BreakStage { get; init; }

    public int SkillGroupCount { get; init; }

    public int EquipSuitCount { get; init; }

    public string DisplayName => PresetOptions.CharacterNames.TryGetValue(
        CharId, out string? name) ? name : CharId;
}

internal sealed class CombatActionRecord
{
    public ulong Id { get; init; }

    public double StartTime { get; init; }

    public double? EndTime { get; init; }

    public double ObservedUntil { get; init; }

    public required string ActorId { get; init; }

    public required string ActionType { get; init; }

    public required string SkillId { get; init; }

    public ulong ParentActionId { get; init; }

    public required string Result { get; init; }

}

internal sealed class CombatStaggerInterval
{
    public required string TargetId { get; init; }

    public double StartTime { get; init; }

    public double EndTime { get; init; }

    public required string BuffId { get; init; }

    public double Duration => Math.Max(0, EndTime - StartTime);
}

internal sealed class CombatSessionRecord
{
    public required string Path { get; init; }

    public required string FileName { get; init; }

    public DateTimeOffset StartedAt { get; init; }

    public double DurationSeconds { get; init; }

    public double TotalDamage { get; init; }

    public double Dps { get; init; }

    public bool UsesRdps { get; init; }

    public bool HasRdpsData { get; init; }

    public long HitCount { get; init; }

    public long CriticalCount { get; init; }

    public IReadOnlyList<CombatCharacterDamage> Characters { get; init; } = [];

    public IReadOnlyList<CombatCharacterDamage> PreviewCharacters => Characters.Take(4).ToArray();

    public IReadOnlyList<CombatTimelinePoint> Timeline { get; init; } = [];

    public IReadOnlyList<CombatBuffInterval> BuffIntervals { get; init; } = [];

    public IReadOnlyList<CombatSquadMember> Squad { get; init; } = [];

    public IReadOnlyList<CombatStaggerInterval> StaggerIntervals { get; init; } = [];

    public IReadOnlyList<CombatActionRecord> Actions { get; init; } = [];

    public int SemanticCatalogSchema { get; init; }

    public string SemanticGameVersion { get; init; } = string.Empty;

    public string SemanticHotfixVersion { get; init; } = string.Empty;

    public long SemanticObserved { get; init; }

    public long SemanticVerified { get; init; }

    public long SemanticCandidate { get; init; }

    public long SemanticExcluded { get; init; }

    public long SemanticUnknown { get; init; }

    public long SemanticUnresolvedDropped { get; init; }

    public IReadOnlyList<CombatSemanticAuditItem> UnresolvedSemantics { get; init; } = [];

    public string? DungeonId { get; init; }

    public string? DungeonName { get; init; }

    public string? ModeId { get; init; }

    public string DateText => StartedAt.LocalDateTime.ToString("yyyy年M月d日  HH:mm");

    public string DurationText => TimeSpan.FromSeconds(Math.Max(0, DurationSeconds)).ToString(@"mm\:ss");

    public string TotalDamageText => CombatNumberFormatter.Format(TotalDamage);

    public string MetricName => UsesRdps ? "rDPS" : "DPS";

    public string Summary => $"{MetricName} {CombatNumberFormatter.Format(Dps)} · 耗时 {DurationText} · {HitCount} 次命中";

    public string? DungeonDisplayName => PresetOptions.FormatDungeonName(DungeonId, DungeonName);

    public string SubtitleText => DungeonDisplayName != null
        ? $"{CharacterSummary} · 关卡：{DungeonDisplayName}"
        : CharacterSummary;

    public string CharacterSummary => Characters.Count == 0
        ? "未识别角色"
        : string.Join("、", Characters.Take(3).Select(character => character.DisplayName));

    public string SemanticSummary
    {
        get
        {
            if (SemanticCatalogSchema <= 0) return "战斗字典：记录中未包含版本信息";
            if (SemanticObserved <= 0)
                return $"战斗字典 {SemanticGameVersion} / {SemanticHotfixVersion}";
            double ratio = SemanticObserved > 0
                ? (double)SemanticVerified / SemanticObserved : 0;
            return $"语义目录 {SemanticGameVersion} / {SemanticHotfixVersion} · " +
                $"验证覆盖 {ratio:P1}（{SemanticVerified}/{SemanticObserved}）· " +
                $"候选 {SemanticCandidate} · 排除 {SemanticExcluded} · " +
                $"未解析 {SemanticUnknown}";
        }
    }

    public string SemanticAuditToolTip
    {
        get
        {
            if (UnresolvedSemantics.Count == 0)
                return SemanticUnresolvedDropped > 0
                    ? $"另有 {SemanticUnresolvedDropped} 项未写入明细。"
                    : "本次记录没有未解析语义项。";
            IEnumerable<string> lines = UnresolvedSemantics.Take(12).Select(item =>
                $"{item.BuffId} · {PresetOptions.TranslateSemanticStatus(item.Status)}/{PresetOptions.TranslateSemanticZone(item.ObservedZone)} ×{item.Count}" +
                (item.ElementMismatch ? "（元素不匹配）" : string.Empty));
            string suffix = UnresolvedSemantics.Count > 12 || SemanticUnresolvedDropped > 0
                ? $"\n其余/丢弃：{Math.Max(0, UnresolvedSemantics.Count - 12) + SemanticUnresolvedDropped}"
                : string.Empty;
            return string.Join("\n", lines) + suffix;
        }
    }
}

internal sealed class CombatCharacterFilterChoice
{
    public string? Id { get; init; }

    public required string DisplayName { get; init; }
}
