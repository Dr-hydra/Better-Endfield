using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

public sealed class GachaApiClient
{
    private static readonly string[] CharacterPools =
    [
        "E_CharacterGachaPoolType_Special", "E_CharacterGachaPoolType_Joint",
        "E_CharacterGachaPoolType_Standard", "E_CharacterGachaPoolType_Beginner"
    ];
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(15) };

    public async Task<Dictionary<string, GachaPoolInfo>> FetchPoolInfosAsync(
        GachaSession session, IEnumerable<GachaRecord> records,
        IReadOnlyDictionary<string, GachaPoolInfo>? existing = null,
        CancellationToken cancellationToken = default)
    {
        var result = new Dictionary<string, GachaPoolInfo>(StringComparer.Ordinal);
        List<GachaRecord> recordList = records.ToList();
        foreach (string poolId in recordList.Select(x => x.PoolId).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x!).Distinct(StringComparer.Ordinal))
        {
            if (existing is not null && existing.TryGetValue(poolId, out GachaPoolInfo? cached) && cached.Category != "其他")
            {
                result[poolId] = cached;
                continue;
            }
            string? fallbackType = recordList.FirstOrDefault(x => x.PoolId == poolId)?.PoolType;
            GachaPoolInfo? info = await FetchPoolInfoAsync(session, poolId!, fallbackType, cancellationToken);
            if (info is not null) result[poolId!] = info;
            await Task.Delay(100, cancellationToken);
        }
        return result;
    }

    private async Task<GachaPoolInfo?> FetchPoolInfoAsync(GachaSession session, string poolId, string? fallbackType, CancellationToken cancellationToken)
    {
        string host = GetHost(session);
        string server = GetServerId(session);
        string url = $"https://{host}/api/content?lang=zh-cn&token={Uri.EscapeDataString(session.Token!)}&server_id={Uri.EscapeDataString(server)}&pool_id={Uri.EscapeDataString(poolId)}";
        using HttpResponseMessage response = await _http.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode) return null;
        using JsonDocument document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
        JsonElement root = document.RootElement;
        if (!root.TryGetProperty("code", out JsonElement code) || code.GetInt32() != 0 ||
            !root.TryGetProperty("data", out JsonElement data) || !data.TryGetProperty("pool", out JsonElement pool)) return null;
        string name = ReadString(pool, "pool_name");
        string type = ReadString(pool, "pool_type");
        var upIds = new List<string>();
        string upName = ReadString(pool, "up6_name");
        if (pool.TryGetProperty("all", out JsonElement all) && all.ValueKind == JsonValueKind.Array)
            foreach (JsonElement item in all.EnumerateArray())
                if (ReadString(item, "name") == upName && !string.IsNullOrWhiteSpace(ReadString(item, "id"))) upIds.Add(ReadString(item, "id"));
        string? pityCarryGroup = ReadOptionalString(pool, "pity_carry_group", "pity_group", "inherit_pity_group", "rerun_group");
        string category = ClassifyCategory(type, ReadString(pool, "pool_gacha_type"), poolId, fallbackType);
        return new GachaPoolInfo { PoolId = poolId, PoolType = type, PoolName = name, Category = category,
            CategoryId = GachaCategoryKeys.FromLabel(category), UpIds = upIds.Distinct(StringComparer.Ordinal).ToList(), PityCarryGroup = pityCarryGroup };
    }

    private static string ClassifyCategory(string poolType, string gachaType, string poolId, string? fallbackType)
    {
        if (fallbackType is "E_CharacterGachaPoolType_Special") return "限定";
        if (fallbackType is "E_CharacterGachaPoolType_Joint") return "庆典";
        if (fallbackType is "E_CharacterGachaPoolType_Standard") return "常驻";
        if (fallbackType is "E_CharacterGachaPoolType_Beginner") return "新手";
        if (fallbackType is "weapon") return "武器";
        string value = $"{poolType} {gachaType} {poolId}".ToLowerInvariant();
        if (value.Contains("weapon") || value.Contains("arms")) return "武器";
        if (value.Contains("beginner") || value.Contains("newbie")) return "新手";
        if (value.Contains("joint") || value.Contains("extra") || value.Contains("festival")) return "庆典";
        if (value.Contains("constant") || value.Contains("standard")) return "常驻";
        if (value.Contains("special") || value.Contains("charter")) return "限定";
        return "其他";
    }

    private static string ReadString(JsonElement element, string name) =>
        element.TryGetProperty(name, out JsonElement value) && value.ValueKind == JsonValueKind.String ? value.GetString() ?? "" : "";

    private static string? ReadOptionalString(JsonElement element, params string[] names)
    {
        foreach (string name in names)
        {
            string value = ReadString(element, name);
            if (!string.IsNullOrWhiteSpace(value)) return value;
        }
        return null;
    }

    public async Task<List<GachaRecord>> FetchCharactersAsync(GachaSession session,
        IEnumerable<GachaRecord>? existing = null,
        CancellationToken cancellationToken = default)
    {
        var records = new Dictionary<string, GachaRecord>(StringComparer.Ordinal);
        var known = (existing ?? []).Where(x => x.CharName is not null)
            .ToDictionary(x => x.SeqId, StringComparer.Ordinal);
        foreach (string pool in CharacterPools)
            foreach (GachaRecord item in await FetchPagesAsync(session, "record/char", "pool_type", pool, pool, known.Keys, cancellationToken))
                records[item.SeqId] = item;
        foreach (GachaRecord item in known.Values) records.TryAdd(item.SeqId, item);
        return records.Values.OrderByDescending(x => x.GachaTs).ToList();
    }

    public async Task<List<GachaRecord>> FetchWeaponsAsync(GachaSession session,
        IEnumerable<GachaRecord>? existing = null,
        CancellationToken cancellationToken = default)
    {
        string host = GetHost(session);
        string server = GetServerId(session);
        string url = $"https://{host}/api/record/weapon/pool?lang=zh-cn&token={Uri.EscapeDataString(session.Token!)}&server_id={Uri.EscapeDataString(server)}";
        WeaponPoolResponse? pools = await _http.GetFromJsonAsync<WeaponPoolResponse>(url, cancellationToken);
        if (pools?.Code != 0 || pools.Data is null) throw new InvalidOperationException("无法取得武器卡池列表");
        var records = new Dictionary<string, GachaRecord>(StringComparer.Ordinal);
        var known = (existing ?? []).Where(x => x.WeaponName is not null)
            .ToDictionary(x => x.SeqId, StringComparer.Ordinal);
        foreach (WeaponPool pool in pools.Data)
            foreach (GachaRecord item in await FetchPagesAsync(session, "record/weapon", "pool_id", pool.PoolId, "weapon", known.Keys, cancellationToken))
                records[item.SeqId] = item;
        foreach (GachaRecord item in known.Values) records.TryAdd(item.SeqId, item);
        return records.Values.OrderByDescending(x => x.GachaTs).ToList();
    }

    private async Task<List<GachaRecord>> FetchPagesAsync(GachaSession session, string endpoint,
        string poolKey, string poolValue, string poolType, CancellationToken cancellationToken)
        => await FetchPagesAsync(session, endpoint, poolKey, poolValue, poolType, null, cancellationToken);

    private async Task<List<GachaRecord>> FetchPagesAsync(GachaSession session, string endpoint,
        string poolKey, string poolValue, string poolType, IEnumerable<string>? knownSeqIds,
        CancellationToken cancellationToken)
    {
        string host = GetHost(session);
        string? cursor = null;
        var records = new List<GachaRecord>();
        for (int page = 0; page < 100; page++)
        {
            var query = new List<string>
            {
                $"lang=zh-cn", $"token={Uri.EscapeDataString(session.Token!)}",
                $"server_id={Uri.EscapeDataString(GetServerId(session))}",
                $"{poolKey}={Uri.EscapeDataString(poolValue)}"
            };
            if (!string.IsNullOrEmpty(cursor)) query.Add($"seq_id={Uri.EscapeDataString(cursor)}");
            using HttpResponseMessage response = await _http.GetAsync(
                $"https://{host}/api/{endpoint}?{string.Join('&', query)}", cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new InvalidOperationException("会话已过期");
            response.EnsureSuccessStatusCode();
            GachaPageResponse? result = await response.Content.ReadFromJsonAsync<GachaPageResponse>(cancellationToken);
            if (result?.Code != 0 || result.Data?.List is null) throw new InvalidOperationException("寻访接口返回无效数据");
            foreach (GachaRecord item in result.Data.List)
            {
                if (string.IsNullOrWhiteSpace(item.SeqId)) continue;
                item.PoolType ??= poolType;
                item.PoolId ??= poolValue;
                records.Add(item);
            }
            if (knownSeqIds is not null && result.Data.List.Count > 0 &&
                knownSeqIds.Contains(result.Data.List[^1].SeqId, StringComparer.Ordinal)) break;
            if (!result.Data.HasMore || result.Data.List.Count == 0) break;
            cursor = result.Data.List[^1].SeqId;
            await Task.Delay(350, cancellationToken);
        }
        return records;
    }

    private static string GetHost(GachaSession session) =>
        ContainsGlobalMarker(session.Channel) || ContainsGlobalMarker(session.Region)
            ? "ef-webview.gryphline.com" : "ef-webview.hypergryph.com";

    private static bool ContainsGlobalMarker(string? value) => value is not null &&
        (value.Contains("gryphline", StringComparison.OrdinalIgnoreCase) ||
         value.Contains("global", StringComparison.OrdinalIgnoreCase) ||
         value.Contains("oversea", StringComparison.OrdinalIgnoreCase));

    private static string GetServerId(GachaSession session) =>
        GetHost(session).Contains("hypergryph", StringComparison.Ordinal) ? "1" : session.Region ?? "";
}
