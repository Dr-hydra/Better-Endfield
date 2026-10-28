using System.Text.Json;
using BetterEndfieldNext.UI.Services;

if (args.Length == 1)
{
    string actualRoot = Path.GetFullPath(args[0]);
    var actual = GameResourceStore.Resolve("core", actualRoot)
        ?? throw new Exception("Installed core resource pack did not resolve: " + actualRoot);
    int checkedFiles = 0;
    foreach (var file in actual.Manifest.GetProperty("files").EnumerateObject())
    {
        actual.File(file.Name);
        checkedFiles++;
    }
    using Stream core = File.OpenRead(actual.File("model/character-presets.json"));
    Console.WriteLine($"PASS installed core {actual.Version}: {checkedFiles} files, character presets {core.Length} bytes");
    return;
}

string root = Path.Combine(Path.GetTempPath(), "be-resource-test-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
void Check(bool value, string name) { if (!value) throw new Exception(name); Console.WriteLine("PASS " + name); }
void Pack(string version, string platform = "windows")
{
    string folder = Path.Combine(root, "core/windows", version); Directory.CreateDirectory(folder);
    File.WriteAllText(Path.Combine(folder, "data.json"), "{}");
    File.WriteAllText(Path.Combine(folder, "manifest.json"), JsonSerializer.Serialize(new
    { schemaVersion = 1, kind = "betterendfield-resource-pack", packageType = "core", platform, version, files = new Dictionary<string,long> { ["data.json"] = 2 } }));
}
try
{
    string installed = Path.Combine(root, "installed");
    string extracted = Path.Combine(root, "bundle-extraction");
    Check(GameResourceStore.ResolveRoot(Path.Combine(installed, "BetterEndfieldNext.exe"), extracted) ==
        Path.Combine(installed, "resources"), "single-file resources resolve beside executable");
    Check(GameResourceStore.ResolveRoot(null, extracted) == Path.Combine(extracted, "resources"),
        "missing process path uses assembly directory");
    Check(GameResourceStore.Resolve("core", root) is null, "missing pack");
    Pack("1.6.3.2"); Pack("1.6.3.10"); Pack("9.9.9.1", "android");
    var snapshot = GameResourceStore.Resolve("core", root)!;
    Check(snapshot.Version == "1.6.3.10", "numeric version and platform filtering");
    Check(File.ReadAllText(snapshot.File("data.json")) == "{}", "manifest-listed payload");
    foreach (string path in new[] { "../data.json", "/data.json", "C:/data.json", "a\\b", "a//b" })
    {
        bool rejected = false; try { snapshot.File(path); } catch (Exception ex) when (ex is IOException or InvalidDataException) { rejected = true; }
        Check(rejected, "reject path " + path);
    }
    File.WriteAllText(Path.Combine(snapshot.Root, "data.json"), "broken");
    bool truncated = false; try { snapshot.File("data.json"); } catch (InvalidDataException) { truncated = true; }
    Check(truncated, "wrong payload length");
    File.WriteAllText(Path.Combine(root, "state.json"), "{\"core.windows\":\"1.6.3.2\"}");
    Check(GameResourceStore.Resolve("core", root)!.Version == "1.6.3.2", "pinned rollback");
    File.WriteAllText(Path.Combine(root, "state.json"), "{\"core.windows\":\"0.0.0.1\"}");
    Check(GameResourceStore.Resolve("core", root) is null, "missing pinned version does not silently upgrade");
}
finally { Directory.Delete(root, true); }
