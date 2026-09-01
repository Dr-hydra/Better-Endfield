using System.Text.Json.Serialization;

namespace BetterEndfield.UI.Models;

/// <summary>
/// Stable, credential-free payload shared with the web page. It intentionally
/// contains only data needed to render and merge gacha statistics.
/// </summary>
public sealed record GachaWebSnapshot(
    [property: JsonPropertyName("schemaVersion")] int SchemaVersion,
    [property: JsonPropertyName("kind")] string Kind,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("categories")] IReadOnlyList<GachaSnapshotCategory> Categories,
    [property: JsonPropertyName("pools")] IReadOnlyList<GachaSnapshotPool> Pools);

public sealed record GachaSnapshotCategory(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("totalPulls")] int TotalPulls,
    [property: JsonPropertyName("sixStarCount")] int SixStarCount,
    [property: JsonPropertyName("upCount")] int UpCount,
    [property: JsonPropertyName("offRateCount")] int OffRateCount,
    [property: JsonPropertyName("upAveragePaidPulls")] double? UpAveragePaidPulls);

public sealed record GachaSnapshotPool(
    [property: JsonPropertyName("poolId")] string? PoolId,
    [property: JsonPropertyName("poolType")] string PoolType,
    [property: JsonPropertyName("categoryId")] string CategoryId,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("poolName")] string PoolName,
    [property: JsonPropertyName("totalPulls")] int TotalPulls,
    [property: JsonPropertyName("freePulls")] int FreePulls,
    [property: JsonPropertyName("sixStarCount")] int SixStarCount,
    [property: JsonPropertyName("upCount")] int UpCount,
    [property: JsonPropertyName("offRateCount")] int OffRateCount,
    [property: JsonPropertyName("startingPity")] int StartingPity,
    [property: JsonPropertyName("currentPity")] int CurrentPity,
    [property: JsonPropertyName("lastSeqId")] string? LastSeqId,
    [property: JsonPropertyName("sixStars")] IReadOnlyList<GachaSnapshotStar> SixStars,
    [property: JsonPropertyName("freeSixStars")] IReadOnlyList<GachaSnapshotStar> FreeSixStars);

public sealed record GachaSnapshotStar(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("itemId")] string? ItemId,
    [property: JsonPropertyName("pity")] int Pity,
    [property: JsonPropertyName("carryPity")] int CarryPity,
    [property: JsonPropertyName("isUp")] bool IsUp,
    [property: JsonPropertyName("isFree")] bool IsFree,
    [property: JsonPropertyName("seqId")] string SeqId,
    [property: JsonPropertyName("gachaTs")] string? GachaTs);

public static class GachaWebSnapshotBuilder
{
    public const int CurrentSchemaVersion = 1;
    public const string SnapshotKind = "betterendfield.gacha";

    public static GachaWebSnapshot Build(
        IEnumerable<GachaRecord> records,
        IReadOnlyDictionary<string, GachaPoolInfo>? poolInfos = null,
        DateTimeOffset? createdAt = null)
    {
        ArgumentNullException.ThrowIfNull(records);
        List<GachaRecord> source = records
            .Where(x => !string.IsNullOrWhiteSpace(x.SeqId))
            .ToList();
        IReadOnlyDictionary<string, GachaPoolInfo> infos = poolInfos ??
            new Dictionary<string, GachaPoolInfo>(StringComparer.Ordinal);

        IReadOnlyList<GachaPoolStatistics> poolStats =
            GachaStatisticsCalculator.GroupByPool(source, infos);
        var pools = new List<GachaSnapshotPool>(poolStats.Count);
        foreach (GachaPoolStatistics stats in poolStats)
        {
            GachaPoolInfo? info = stats.PoolId is not null && infos.TryGetValue(stats.PoolId, out GachaPoolInfo? value)
                ? value : null;
            List<GachaRecord> poolRecords = source.Where(x =>
                string.Equals(x.PoolId, stats.PoolId, StringComparison.Ordinal) &&
                string.Equals(x.PoolType, stats.PoolType, StringComparison.Ordinal)).ToList();
            string? lastSeqId = poolRecords
                .OrderByDescending(x => SortNumber(x.GachaTs))
                .ThenByDescending(x => SortNumber(x.SeqId))
                .Select(x => x.SeqId)
                .FirstOrDefault();
            string category = info?.Category ?? poolRecords.FirstOrDefault()?.PoolCategory ?? "其他";
            pools.Add(new(
                stats.PoolId,
                stats.PoolType,
                GachaCategoryKeys.Resolve(info, category),
                category,
                stats.PoolName,
                stats.PaidPulls,
                stats.FreePulls,
                stats.SixStarCount,
                stats.UpCount,
                stats.OffRateCount,
                stats.StartingPity,
                stats.CurrentPity,
                lastSeqId,
                stats.SixStars.Select(ToSnapshotStar).ToList(),
                stats.FreeSixStars.Select(ToSnapshotStar).ToList()));
        }

        var categories = new List<GachaSnapshotCategory>();
        foreach (IGrouping<(string Id, string Name), GachaRecord> group in source
                     .GroupBy(x => GetCategory(x, infos))
                     .OrderBy(x => CategoryOrder(x.Key.Name)).ThenBy(x => x.Key.Id, StringComparer.Ordinal))
        {
            ISet<string> upIds = group.SelectMany(x => x.PoolId is not null && infos.TryGetValue(x.PoolId, out GachaPoolInfo? info)
                ? info.UpIds : []).ToHashSet(StringComparer.Ordinal);
            GachaPoolStatistics stats = GachaStatisticsCalculator.Calculate(group, upIds);
            categories.Add(new(group.Key.Id, group.Key.Name, stats.PaidPulls, stats.SixStarCount,
                stats.UpCount, stats.OffRateCount, stats.UpAveragePaidPulls));
        }

        return new(CurrentSchemaVersion, SnapshotKind, createdAt ?? DateTimeOffset.UtcNow, categories, pools);
    }

    private static GachaSnapshotStar ToSnapshotStar(GachaSixStarResult value) => new(
        StableId(value), value.Name, value.ItemId, value.Pity, value.CarryPity,
        value.IsUp, value.IsFree, value.SeqId, value.GachaTs);

    public static string StableId(GachaSixStarResult value) => string.Join('|',
        value.PoolId ?? "", value.SeqId, value.ItemId ?? "", value.GachaTs ?? "");

    private static int CategoryOrder(string value) => value switch
    {
        "限定" => 0,
        "武器" => 1,
        "庆典" => 2,
        "常驻" => 3,
        "新手" => 4,
        _ => 5
    };

    private static (string Id, string Name) GetCategory(
        GachaRecord record,
        IReadOnlyDictionary<string, GachaPoolInfo> infos)
    {
        GachaPoolInfo? info = record.PoolId is not null && infos.TryGetValue(record.PoolId, out GachaPoolInfo? value)
            ? value : null;
        string name = info?.Category ?? (string.IsNullOrWhiteSpace(record.PoolCategory) ? "其他" : record.PoolCategory!);
        return (GachaCategoryKeys.Resolve(info, name), name);
    }

    private static long SortNumber(string? value)
    {
        if (long.TryParse(value, out long number)) return number;
        if (DateTimeOffset.TryParse(value, out DateTimeOffset date)) return date.UtcTicks;
        return long.MinValue;
    }
}
