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

internal sealed class CombatLegendItem
{
    public required string Label { get; init; }

    public required Brush Brush { get; init; }
}

internal sealed class CombatSessionRecord
{
    public required string Path { get; init; }

    public required string FileName { get; init; }

    public DateTimeOffset StartedAt { get; init; }

    public double DurationSeconds { get; init; }

    public double TotalDamage { get; init; }

    public double Dps { get; init; }

    public long HitCount { get; init; }

    public long CriticalCount { get; init; }

    public IReadOnlyList<CombatCharacterDamage> Characters { get; init; } = [];

    public IReadOnlyList<CombatCharacterDamage> PreviewCharacters => Characters.Take(4).ToArray();

    public IReadOnlyList<CombatTimelinePoint> Timeline { get; init; } = [];

    public string DateText => StartedAt.LocalDateTime.ToString("yyyy年M月d日  HH:mm");

    public string TotalDamageText => CombatNumberFormatter.Format(TotalDamage);

    public string Summary => $"DPS {CombatNumberFormatter.Format(Dps)} · {HitCount} 次命中";

    public string CharacterSummary => Characters.Count == 0
        ? "未识别角色"
        : string.Join("、", Characters.Take(3).Select(character => character.DisplayName));
}

internal sealed class CombatCharacterFilterChoice
{
    public string? Id { get; init; }

    public required string DisplayName { get; init; }
}
