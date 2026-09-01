namespace BetterEndfield.UI.Models;

public enum GachaPoolKind
{
    CharacterSpecial,
    CharacterJoint,
    CharacterStandard,
    CharacterBeginner,
    Weapon,
    Other
}

public enum GachaGuaranteeKind
{
    None,
    SixStar,
    FeaturedSixStar
}

/// <summary>规则数据只描述卡池能力，具体统计由 GachaStatisticsCalculator 处理。</summary>
public sealed record GachaPoolRule(
    string PoolType,
    GachaPoolKind Kind,
    int? SixStarHardPity,
    int? FeaturedHardPity,
    GachaGuaranteeKind SixStarGuarantee,
    bool HasFreePulls,
    bool FreePullsAffectPity,
    bool PityCarriesAcrossPools);

public static class GachaPoolRules
{
    public const string Special = "E_CharacterGachaPoolType_Special";
    public const string Joint = "E_CharacterGachaPoolType_Joint";
    public const string Standard = "E_CharacterGachaPoolType_Standard";
    public const string Beginner = "E_CharacterGachaPoolType_Beginner";

    private static readonly Dictionary<string, GachaPoolRule> Rules =
        new(StringComparer.Ordinal)
        {
            [Special] = new(Special, GachaPoolKind.CharacterSpecial, 80, 120,
                GachaGuaranteeKind.FeaturedSixStar, true, false, true),
            [Joint] = new(Joint, GachaPoolKind.CharacterJoint, 80, null,
                GachaGuaranteeKind.SixStar, true, false, false),
            [Standard] = new(Standard, GachaPoolKind.CharacterStandard, 80, null,
                GachaGuaranteeKind.SixStar, false, false, true),
            [Beginner] = new(Beginner, GachaPoolKind.CharacterBeginner, 50, null,
                GachaGuaranteeKind.SixStar, false, false, false),
            ["weapon"] = new("weapon", GachaPoolKind.Weapon, 40, 80,
                GachaGuaranteeKind.FeaturedSixStar, false, false, false)
        };

    public static IReadOnlyDictionary<string, GachaPoolRule> Catalog => Rules;

    public static void Register(GachaPoolRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);
        if (string.IsNullOrWhiteSpace(rule.PoolType))
            throw new ArgumentException("PoolType cannot be empty.", nameof(rule));
        Rules[rule.PoolType] = rule;
    }

    public static GachaPoolRule For(string? poolType) =>
        poolType is not null && Catalog.TryGetValue(poolType, out GachaPoolRule? rule)
            ? rule
        : new(poolType ?? "unknown", GachaPoolKind.Other, null, null,
                GachaGuaranteeKind.None, false, false, false);
}

public static class GachaCategoryKeys
{
    public const string Limited = "limited";
    public const string Weapon = "weapon";
    public const string Festival = "festival";
    public const string Standard = "standard";
    public const string Beginner = "beginner";
    public const string Other = "other";

    public static string FromLabel(string? label) => label switch
    {
        "限定" => Limited,
        "武器" => Weapon,
        "庆典" => Festival,
        "常驻" => Standard,
        "新手" => Beginner,
        _ => Other
    };

    public static string Resolve(GachaPoolInfo? info, string? fallbackLabel)
    {
        if (!string.IsNullOrWhiteSpace(info?.CategoryId)) return info.CategoryId!;
        return FromLabel(info?.Category ?? fallbackLabel);
    }
}
