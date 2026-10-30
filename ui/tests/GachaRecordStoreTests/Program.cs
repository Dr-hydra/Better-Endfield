using System.Text.Json;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;

string root = Path.Combine(Path.GetTempPath(), "be-gacha-store-tests-" + Guid.NewGuid());
Directory.CreateDirectory(root);
int checks = 0;
void Check(bool condition, string message)
{
    if (!condition) throw new Exception(message);
    Console.WriteLine("PASS " + message);
    checks++;
}

try
{
    // The original anonymous export must reproduce #36 after production obfuscation.
    if (args.Contains("--expect-anonymous-failure"))
    {
        bool reproduced = false;
        try
        {
            JsonSerializer.Serialize(new
            {
                exportedAt = DateTimeOffset.Now,
                records = new List<GachaRecord>(),
                pools = new List<GachaPoolInfo>()
            });
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("null names", StringComparison.Ordinal))
        {
            reproduced = true;
        }
        Check(reproduced, "original anonymous export reproduces missing constructor metadata after obfuscation");
    }

    var store = new GachaRecordStore(Path.Combine(root, "cache"), Path.Combine(root, "exports"));
    var records = new List<GachaRecord>
    {
        new() { SeqId = "100000000000000001", PoolId = "char-pool", PoolType = "limited",
            PoolCategory = "限定", CharId = "char-test", CharName = "测试角色", Kind = "char",
            Rarity = 6, GachaTs = "1793318400", PoolName = "测试卡池", IsFree = true, IsNew = true },
        new() { SeqId = "100000000000000002", PoolId = "weapon-pool", WeaponId = "weapon-test",
            WeaponName = "测试武器", Kind = "weapon", Rarity = 5 }
    };
    var pools = new List<GachaPoolInfo>
    {
        new() { PoolId = "char-pool", PoolType = "limited", PoolName = "测试卡池", Category = "限定",
            CategoryId = "limited", UpIds = ["char-test"], PityCarryGroup = "char-limited" },
        new() { PoolId = "weapon-pool", PoolName = "测试武器池", Category = "武器", UpIds = ["weapon-test"] }
    };

    string path = await store.ExportAsync(records.Where(_ => true), pools.Where(_ => true));
    Check(File.Exists(path) && Path.GetDirectoryName(path) == Path.Combine(root, "exports"),
        "export creates JSON in the configured directory");
    string exported = await File.ReadAllTextAsync(path);
    using (JsonDocument document = JsonDocument.Parse(exported))
    {
        JsonElement data = document.RootElement;
        Check(data.EnumerateObject().Select(p => p.Name).SequenceEqual(new[] { "exportedAt", "records", "pools" })
            && (DateTimeOffset.Now - data.GetProperty("exportedAt").GetDateTimeOffset()).Duration() < TimeSpan.FromMinutes(1),
            "export preserves existing wire keys and timestamp");
        Check(data.GetProperty("records").GetArrayLength() == 2 && data.GetProperty("pools").GetArrayLength() == 2,
            "export includes characters, weapons and pool metadata");
    }
    GachaRecordStore.ImportData imported = await store.ImportAsync(path);
    Check(JsonSerializer.Serialize(imported.Records) == JsonSerializer.Serialize(records)
        && JsonSerializer.Serialize(imported.Pools) == JsonSerializer.Serialize(pools),
        "export/import round trip preserves all record and pool fields");

    var emptyStore = new GachaRecordStore(Path.Combine(root, "cache"), Path.Combine(root, "empty-exports"));
    string emptyPath = await emptyStore.ExportAsync([], []);
    GachaRecordStore.ImportData empty = await emptyStore.ImportAsync(emptyPath);
    Check(empty.Records.Count == 0 && empty.Pools.Count == 0, "empty exports round trip");

    string legacyPath = Path.Combine(root, "legacy.json");
    await File.WriteAllTextAsync(legacyPath, "{\"exportedAt\":\"2026-01-01T00:00:00Z\",\"records\":[{\"seqId\":\"old-1\",\"charName\":\"Old character\",\"rarity\":6}],\"pools\":[{\"PoolId\":\"old-pool\",\"PoolName\":\"Old pool\"}]}");
    GachaRecordStore.ImportData legacy = await store.ImportAsync(legacyPath);
    Check(legacy.Records.Single().SeqId == "old-1" && legacy.Pools.Single().PoolId == "old-pool",
        "imports the existing export format");
    await File.WriteAllTextAsync(legacyPath, "{\"records\":[]}");
    Check((await store.ImportAsync(legacyPath)).Pools.Count == 0, "imports files without optional pools");

    await store.SaveAsync(records);
    await store.SavePoolInfosAsync(pools);
    Check(JsonSerializer.Serialize(await store.LoadAsync()) == JsonSerializer.Serialize(records)
        && (await store.LoadPoolInfosAsync())["char-pool"].UpIds.Single() == "char-test",
        "automatic record and pool caches remain readable");

    string blocked = Path.Combine(root, "blocked");
    await File.WriteAllTextAsync(blocked, "not a directory");
    bool failed = false;
    try { await new GachaRecordStore(Path.Combine(root, "cache"), blocked).ExportAsync(records, pools); }
    catch (IOException) { failed = true; }
    Check(failed && await File.ReadAllTextAsync(path) == exported, "failed export preserves the previous export");
    Console.WriteLine($"{checks} checks passed");
}
finally
{
    Directory.Delete(root, true);
}
