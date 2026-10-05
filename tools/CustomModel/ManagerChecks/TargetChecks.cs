using System.Text;
using System.Text.Json.Nodes;
using BetterEndfield.UI.Services;

internal static class TargetChecks
{
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    private static JsonObject Manifest(string package, string kind, string owner, string resource, string platform = "windows-x64") => new()
    {
        ["schema"] = 1, ["package_id"] = package, ["name"] = package, ["author"] = "test", ["version"] = "1.0",
        ["target"] = new JsonObject
        {
            ["kind"] = kind, ["id"] = owner, ["platform"] = "windows-x64", ["profile_id"] = "fixture", ["revision"] = "r1",
            ["resources"] = new JsonArray(new JsonObject
            {
                ["id"] = "main", ["name"] = resource, ["asset_path"] = "assets/test/" + resource + ".prefab",
                ["platforms"] = new JsonArray(platform), ["lod"] = 0
            })
        },
        ["option_groups"] = new JsonArray(), ["parameters"] = new JsonArray()
    };

    private static void Write(string file, JsonObject manifest, ushort minor = 4)
    {
        // This fixture exercises metadata only; native payload checks have separate fixtures.
        byte[] json = Encoding.UTF8.GetBytes(manifest.ToJsonString());
        using var stream = File.Create(file);
        using var writer = new BinaryWriter(stream);
        writer.Write(new byte[] { 66, 69, 77, 0, 80, 75, 71, 0 });
        writer.Write((ushort)1); writer.Write(minor); writer.Write(40u);
        writer.Write((ulong)(40 + json.Length)); writer.Write((ulong)json.Length);
        writer.Write(0u); writer.Write(0u); writer.Write(json);
    }

    public static async Task Run()
    {
        var service = new BemPackageService();
        Directory.CreateDirectory(service.PackageDirectory);
        string Folder(string name) => Path.Combine(service.PackageDirectory, name + ".bem");
        var normal = Manifest("normal", "character", "zhuangfy", "normal_world");
        normal["target"]!.AsObject().Remove("resources");
        normal["target"]!.AsObject().Remove("kind");
        normal["target"]!.AsObject().Remove("id");
        normal["target"]!["character_id"] = "zhuangfy";
        normal["target"]!["world_resource"] = "normal_world";
        normal["target"]!["ui_resource"] = "normal_ui";
        Write(Folder("normal"), normal, 3);
        Write(Folder("ultimate"), Manifest("ultimate", "character", "zhuangfy", "ultimate_world"));
        Write(Folder("weapon"), Manifest("weapon", "weapon", "zhuangfy", "sword_world"));
        Write(Folder("alternate"), Manifest("alternate", "character", "different_owner", "ultimate_world"));
        Write(Folder("android"), Manifest("android", "character", "zhuangfy", "ultimate_world", "android-arm64"));
        service.Load();
        Check(service.Packages.Count == 5, string.Join(";", service.Notices));
        BemPackage Get(string id) => service.Packages.Single(p => p.Id == id);
        Check(Get("normal").ResourceKeys.Count == 4 && Get("normal").TargetKind == "character", "legacy normalization");
        Check(Get("weapon").TargetKind == "weapon" && Get("weapon").TargetId == "zhuangfy", "weapon without character_id");
        Check(Get("ultimate").ConflictsWith(Get("alternate")), "same resource with different owners must conflict");
        Check(!Get("ultimate").ConflictsWith(Get("android")), "platform-specific resource keys");
        foreach (string id in new[] { "normal", "ultimate", "weapon" }) await service.SetEnabledAsync(Get(id), true);
        Check(!Get("android").SupportsCurrentPlatform, "Android-only metadata incorrectly supports Windows");
        bool unavailableRejected = false;
        try { await service.SetEnabledAsync(Get("android"), true); }
        catch (InvalidOperationException ex) { unavailableRejected = ex.Message.Contains("Windows"); }
        Check(unavailableRejected && !Get("android").Enabled, "Android-only package silently enabled on Windows");
        service.SkipValidation = true;
        bool importRejected = false;
        try { await service.ImportAsync(Folder("android"), ""); }
        catch (InvalidDataException ex) { importRejected = ex.Message.Contains("Windows"); }
        Check(importRejected, "Android-only package imported on Windows with validation disabled");
        service.SkipValidation = false;
        service.Load();
        Check(service.Packages.Count(p => p.Enabled) == 3, "same owner distinct resources did not persist");
        await service.SetEnabledAsync(Get("alternate"), true);
        Check(!Get("ultimate").Enabled && Get("normal").Enabled && Get("weapon").Enabled && !Get("android").Enabled,
            "enable must disable only overlapping resources");
        Get("ultimate").Enabled = true;
        await service.SaveAsync(); service.Load();
        Check(!Get("ultimate").Enabled && !Get("alternate").Enabled && Get("normal").Enabled,
            "corrupt snapshot conflict normalization must preserve unrelated packages");
        var multi = Manifest("multi", "character", "zhuangfy", "ultimate_world");
        multi["target"]!["resources"]!.AsArray().Add(new JsonObject
        {
            ["id"] = "mirror", ["name"] = "mirror_world", ["asset_path"] = "assets/test/mirror_world.prefab",
            ["platforms"] = new JsonArray("windows-x64", "android-arm64"), ["lod"] = 1
        });
        string probe = Path.Combine(ConfigurationService.SettingsDirectory, "probe.bem");
        var sharedAndroid = Manifest("shared.android", "character", "zhuangfy", "unique_windows");
        sharedAndroid["target"]!["resources"]!.AsArray().Add(new JsonObject
        {
            ["id"] = "android", ["name"] = "ultimate_world", ["asset_path"] = "assets/test/ultimate_world.prefab",
            ["platforms"] = new JsonArray("android-arm64"), ["lod"] = 0
        });
        Write(probe, sharedAndroid);
        Check(!BemPackageService.ReadMetadata(probe).ConflictsWith(Get("android")), "Windows conflict included Android-only keys");
        var legacyOther = (JsonObject)normal.DeepClone(); legacyOther["package_id"] = "other.legacy";
        legacyOther["target"]!["world_resource"] = "another_world"; legacyOther["target"]!["ui_resource"] = "another_ui";
        Write(probe, legacyOther, 3);
        Check(Get("normal").ConflictsWith(BemPackageService.ReadMetadata(probe)), "two legacy packages must retain same-owner exclusivity");
        Write(probe, multi);
        Check(BemPackageService.ReadMetadata(probe).ResourceKeys.Count == 3, "multiple resource metadata");
        multi.Remove("parameters"); Write(probe, multi);
        Check(BemPackageService.ReadMetadata(probe).Parameters.Count == 0, "optional v1.4 parameters");
        foreach (Action<JsonObject> mutate in new Action<JsonObject>[]
        {
            m => m["target"]!.AsObject().Remove("resources"),
            m => m["target"]!["resources"] = new JsonArray(),
            m => m["target"]!["kind"] = "unknown",
            m => m["target"]!["resources"]![0]!["name"] = "bad/root",
            m => m["target"]!["resources"]![0]!["name"] = "Uppercase",
            m => m["target"]!["resources"]![0]!["asset_path"] = "assets/test/wrong_stem.prefab",
            m => m["target"]!["resources"]![0]!["platforms"] = new JsonArray("windows-x64", "windows-x64"),
            m => m["target"]!["resources"]![0]!["asset_path"] = "assets/../test.prefab",
            m => m["target"]!["resources"]![0]!["lod"] = 4
        })
        {
            var invalid = (JsonObject)multi.DeepClone(); mutate(invalid); Write(probe, invalid);
            foreach (bool skip in new[] { false, true })
            {
                bool rejected = false;
                try { BemPackageService.ReadMetadata(probe, skip); }
                catch (Exception ex) when (ex is InvalidDataException or KeyNotFoundException) { rejected = true; }
                Check(rejected, "malformed target accepted with skipValidation=" + skip);
            }
        }
        Write(probe, multi, 5);
        try { BemPackageService.ReadMetadata(probe, true); throw new Exception("future minor accepted"); }
        catch (InvalidDataException) { }
        Check(BemInspectionSummary.Read("""{"format":"BEMv1.4"}""").AlreadyPackaged, "v1.4 direct-import presentation");
        Console.WriteLine("PASS BEM 1.4 metadata, legacy normalization, resource/platform conflicts, persistence and malformed targets");
    }
}
