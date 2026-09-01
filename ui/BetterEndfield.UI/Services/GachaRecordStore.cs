using System.Text.Json;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

public sealed class GachaRecordStore
{
    public sealed record ImportData(List<GachaRecord> Records, List<GachaPoolInfo> Pools);
    private readonly string _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetterEndfield");
    private string RecordsPath => Path.Combine(_directory, "gacha-records.json");
    private string PoolInfoPath => Path.Combine(_directory, "gacha-pools.json");
    public async Task SaveAsync(IEnumerable<GachaRecord> records)
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(RecordsPath, JsonSerializer.Serialize(records));
    }
    public async Task<List<GachaRecord>> LoadAsync()
    {
        if (!File.Exists(RecordsPath)) return [];
        try { return JsonSerializer.Deserialize<List<GachaRecord>>(await File.ReadAllTextAsync(RecordsPath)) ?? []; }
        catch (JsonException) { return []; }
    }

    public async Task SavePoolInfosAsync(IEnumerable<GachaPoolInfo> infos)
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(PoolInfoPath, JsonSerializer.Serialize(infos));
    }

    public async Task<Dictionary<string, GachaPoolInfo>> LoadPoolInfosAsync()
    {
        if (!File.Exists(PoolInfoPath)) return new(StringComparer.Ordinal);
        try
        {
            List<GachaPoolInfo> values = JsonSerializer.Deserialize<List<GachaPoolInfo>>(await File.ReadAllTextAsync(PoolInfoPath)) ?? [];
            return values.Where(x => !string.IsNullOrWhiteSpace(x.PoolId)).ToDictionary(x => x.PoolId, StringComparer.Ordinal);
        }
        catch (JsonException) { return new(StringComparer.Ordinal); }
    }

    public async Task<string> ExportAsync(IEnumerable<GachaRecord> records, IEnumerable<GachaPoolInfo> infos)
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BetterEndfield");
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, $"gacha-export-{DateTime.Now:yyyyMMdd-HHmmss}.json");
        var payload = new { exportedAt = DateTimeOffset.Now, records = records.ToList(), pools = infos.ToList() };
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }));
        return path;
    }

    public async Task<ImportData> ImportAsync(string path)
    {
        using JsonDocument document = JsonDocument.Parse(await File.ReadAllTextAsync(path));
        JsonElement root = document.RootElement;
        List<GachaRecord> records = root.TryGetProperty("records", out JsonElement recordValue)
            ? JsonSerializer.Deserialize<List<GachaRecord>>(recordValue.GetRawText()) ?? [] : [];
        List<GachaPoolInfo> pools = root.TryGetProperty("pools", out JsonElement poolValue)
            ? JsonSerializer.Deserialize<List<GachaPoolInfo>>(poolValue.GetRawText()) ?? [] : [];
        return new ImportData(records, pools);
    }
}
