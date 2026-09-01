namespace BetterEndfield.UI.Models;

public sealed record GachaSixStarResult(
    string Name,
    string? ItemId,
    int Pity,
    int CarryPity,
    bool IsUp,
    bool IsFree,
    string? PoolId,
    string? GachaTs,
    string SeqId);

public sealed record GachaPoolStatistics(
    string PoolType,
    string? PoolId,
    string PoolName,
    int TotalPulls,
    int PaidPulls,
    int FreePulls,
    int SixStarCount,
    int UpCount,
    int OffRateCount,
    int StartingPity,
    int CurrentPity,
    IReadOnlyList<GachaSixStarResult> SixStars,
    IReadOnlyList<GachaSixStarResult> FreeSixStars)
{
    public double? UpAveragePaidPulls => UpCount == 0 ? null : (double)PaidPulls / UpCount;
}

public static class GachaStatisticsCalculator
{
    public static GachaPoolStatistics Calculate(
        IEnumerable<GachaRecord> source,
        ISet<string>? upIds = null,
        GachaPoolRule? rule = null,
        int startingPity = 0)
    {
        List<GachaRecord> records = source
            .Where(x => !string.IsNullOrWhiteSpace(x.SeqId))
            .OrderBy(x => SortNumber(x.GachaTs))
            .ThenBy(x => SortNumber(x.SeqId))
            .ToList();
        rule ??= GachaPoolRules.For(records.FirstOrDefault()?.PoolType);

        int pity = Math.Max(0, startingPity), freePity = 0, paid = 0, free = 0, six = 0, up = 0;
        int carryPity = Math.Max(0, startingPity);
        var sixStars = new List<GachaSixStarResult>();
        var freeSixStars = new List<GachaSixStarResult>();
        foreach (GachaRecord item in records)
        {
            bool countsForPity = !item.IsFree || rule.FreePullsAffectPity;
            if (item.IsFree)
            {
                free++;
                freePity++;
                if (freePity > 10) freePity = 1;
            }
            else
            {
                paid++;
            }
            if (countsForPity) pity++;
            if (item.Rarity != 6) continue;

            six++;
            bool isUp = upIds is not null &&
                ((item.CharId is not null && upIds.Contains(item.CharId)) ||
                 (item.WeaponId is not null && upIds.Contains(item.WeaponId)));
            if (isUp) up++;
            var result = new GachaSixStarResult(item.Name, item.CharId ?? item.WeaponId, item.IsFree ? freePity : pity,
                item.IsFree ? 0 : carryPity, isUp, item.IsFree,
                item.PoolId, item.GachaTs, item.SeqId);
            (item.IsFree ? freeSixStars : sixStars).Add(result);
            if (countsForPity)
            {
                pity = 0;
                carryPity = 0;
            }
        }

        return new(
            records.FirstOrDefault()?.PoolType ?? rule.PoolType,
            records.FirstOrDefault()?.PoolId,
            records.FirstOrDefault()?.PoolName ?? "未知卡池",
            records.Count, paid, free, six, up, six - up, startingPity, pity,
            sixStars.AsEnumerable().Reverse().ToList(), freeSixStars.AsEnumerable().Reverse().ToList());
    }

    public static IReadOnlyList<GachaPoolStatistics> GroupByPool(
        IEnumerable<GachaRecord> source,
        IReadOnlyDictionary<string, GachaPoolInfo>? poolInfos = null)
    {
        var groups = source.GroupBy(x => new { x.PoolType, x.PoolId })
            .OrderBy(group => PoolOrder(group.Key.PoolId))
            .ThenBy(group => group.Max(x => SortNumber(x.GachaTs)))
            .ToList();
        var result = new List<GachaPoolStatistics>(groups.Count);
        int carry = 0;
        string? previousCarryGroup = null;
        bool previousCanCarry = false;
        foreach (var group in groups)
        {
            GachaPoolRule rule = GachaPoolRules.For(group.Key.PoolType);
            GachaPoolInfo? info = poolInfos is not null && group.Key.PoolId is not null && poolInfos.TryGetValue(group.Key.PoolId, out GachaPoolInfo? poolInfo)
                ? poolInfo : null;
            string? carryGroup = GetCarryGroup(rule, info, group.Key.PoolId);
            bool canCarry = carryGroup is not null;
            int startingPity = canCarry && previousCanCarry && string.Equals(previousCarryGroup, carryGroup, StringComparison.Ordinal) ? carry : 0;
            ISet<string>? upIds = info
                is not null
                ? info.UpIds.ToHashSet(StringComparer.Ordinal) : null;
            GachaPoolStatistics stats = Calculate(group, upIds, rule, startingPity);
            result.Add(stats);
            carry = canCarry ? stats.CurrentPity : 0;
            previousCarryGroup = carryGroup;
            previousCanCarry = canCarry;
        }
        return result.AsEnumerable().Reverse().ToList();
    }

    private static long SortNumber(string? value)
    {
        if (long.TryParse(value, out long number)) return number;
        if (DateTimeOffset.TryParse(value, out DateTimeOffset date)) return date.UtcTicks;
        return long.MinValue;
    }

    private static long PoolOrder(string? poolId)
    {
        if (string.IsNullOrWhiteSpace(poolId)) return long.MaxValue;
        string[] parts = poolId.Split('_');
        if (parts.Length >= 4 && long.TryParse(parts[^3], out long major) &&
            long.TryParse(parts[^2], out long minor) && long.TryParse(parts[^1], out long patch))
            return major * 1_000_000 + minor * 1_000 + patch;
        return long.MaxValue;
    }

    private static string? GetCarryGroup(GachaPoolRule rule, GachaPoolInfo? info, string? poolId)
    {
        if (!string.IsNullOrWhiteSpace(info?.PityCarryGroup)) return info.PityCarryGroup;
        if (rule.PityCarriesAcrossPools) return rule.PoolType;
        return null;
    }
}
