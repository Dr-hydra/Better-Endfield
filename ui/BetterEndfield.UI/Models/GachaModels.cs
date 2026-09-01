using System.Text.Json.Serialization;

namespace BetterEndfield.UI.Models;

public sealed record GachaSession(
    [property: JsonPropertyName("ready")] bool Ready,
    [property: JsonPropertyName("token")] string? Token,
    [property: JsonPropertyName("uid")] string? Uid,
    [property: JsonPropertyName("channel")] string? Channel,
    [property: JsonPropertyName("region")] string? Region);

public sealed class GachaRecord
{
    [JsonPropertyName("seqId")] public string SeqId { get; set; } = "";
    [JsonPropertyName("poolId")] public string? PoolId { get; set; }
    [JsonPropertyName("poolType")] public string? PoolType { get; set; }
    [JsonPropertyName("poolCategory")] public string? PoolCategory { get; set; }
    [JsonPropertyName("charId")] public string? CharId { get; set; }
    [JsonPropertyName("weaponId")] public string? WeaponId { get; set; }
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("charName")] public string? CharName { get; set; }
    [JsonPropertyName("weaponName")] public string? WeaponName { get; set; }
    [JsonPropertyName("rarity")] public int Rarity { get; set; }
    [JsonPropertyName("gachaTs")] public string? GachaTs { get; set; }
    [JsonPropertyName("poolName")] public string? PoolName { get; set; }
    [JsonPropertyName("isFree")] public bool IsFree { get; set; }
    [JsonPropertyName("isNew")] public bool IsNew { get; set; }
    public string Name => CharName ?? WeaponName ?? "未知物品";
}

public sealed record GachaPageResponse(
    [property: JsonPropertyName("code")] int Code,
    [property: JsonPropertyName("msg")] string? Message,
    [property: JsonPropertyName("data")] GachaPageData? Data);

public sealed record GachaPageData(
    [property: JsonPropertyName("list")] List<GachaRecord>? List,
    [property: JsonPropertyName("hasMore")] bool HasMore);

public sealed record WeaponPool(
    [property: JsonPropertyName("poolId")] string PoolId,
    [property: JsonPropertyName("poolName")] string PoolName);

public sealed record WeaponPoolResponse(
    [property: JsonPropertyName("code")] int Code,
    [property: JsonPropertyName("msg")] string? Message,
    [property: JsonPropertyName("data")] List<WeaponPool>? Data);

public sealed class GachaPoolInfo
{
    public string PoolId { get; init; } = "";
    public string PoolType { get; init; } = "";
    public string PoolName { get; init; } = "";
    public string Category { get; init; } = "其他";
    // Stable machine key; Category remains the localized display label.
    public string? CategoryId { get; init; }
    public List<string> UpIds { get; init; } = [];
    // Optional explicit key for future rerun pools (for example, the same weapon).
    // Different weapon pools remain isolated until this metadata is supplied.
    public string? PityCarryGroup { get; init; }
}
