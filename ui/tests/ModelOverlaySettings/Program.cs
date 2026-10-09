using BetterEndfieldNext.UI.Services;

static class Program
{
    private static int checks;
    private static void Expect(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
        checks++;
    }
    private static Dictionary<string, Dictionary<string, string>> Settings(string enabled = "false", string option = "outfit:a") => new(StringComparer.Ordinal)
    {
        ["CustomModel"] = new(StringComparer.Ordinal) { ["hot_switch"] = "true", ["overlay_hotkey"] = "PLUS", ["fast_loading"] = "false" },
        ["Mod.a"] = new(StringComparer.Ordinal) { ["enabled"] = enabled, ["options"] = option, ["parameters_saved"] = "height:700&old_slider:300" },
        ["Mod.b"] = new(StringComparer.Ordinal) { ["enabled"] = "false" },
        ["Unknown"] = new(StringComparer.Ordinal) { ["author"] = "中文作者" }
    };

    private static void MetadataFixture(string path, string id, string character)
    {
        byte[] metadata = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(new
        {
            schema = 1, package_id = id, name = "Test " + id, author = "Synthetic", version = "1.0",
            target = new
            {
                platform = "windows-x64", character_id = character,
                world_resource = character + ".world", ui_resource = character + ".ui"
            },
            default_appearance_id = "a", appearances = new[] { new { id = "a", name = "A" }, new { id = "b", name = "B" } }
        });
        using var writer = new BinaryWriter(File.Create(path));
        writer.Write(new byte[] { 66, 69, 77, 0, 80, 75, 71, 0 }); writer.Write((ushort)1); writer.Write((ushort)0);
        writer.Write(40u); writer.Write((ulong)(40 + metadata.Length)); writer.Write((ulong)metadata.Length);
        writer.Write(0u); writer.Write(0u); writer.Write(metadata);
    }

    public static async Task<int> Main(string[] arguments)
    {
        if (arguments.Length > 1) throw new ArgumentException("Provide at most one temporary workspace test directory.");
        string root = Path.GetFullPath(arguments.Length == 1 ? arguments[0] :
            Path.Combine(Environment.GetEnvironmentVariable("BE_WORKSPACE_TEMP_ROOT") ?? Path.GetTempPath(),
                "model-overlay-settings-" + Guid.NewGuid().ToString("N")));
        Directory.CreateDirectory(root);
        var baseline = Settings();
        var latest = Settings("true", "outfit:b");
        latest["CustomModel"]["opaque_overlay_field"] = "keep";
        var desired = Settings();
        desired["CustomModel"]["fast_loading"] = "true";
        var merged = BemRuntimeSettings.Merge(latest, baseline, desired, null);
        Expect(merged["Mod.a"]["enabled"] == "true", "Untouched overlay enable state was lost.");
        Expect(merged["Mod.a"]["options"] == "outfit:b", "Untouched overlay component selection was lost.");
        Expect(merged["CustomModel"]["fast_loading"] == "true", "The explicit desktop edit was lost.");
        Expect(merged["CustomModel"]["opaque_overlay_field"] == "keep" && merged["Unknown"]["author"] == "中文作者", "Unknown fields changed.");
        Expect(merged["Mod.a"]["parameters_saved"] == "height:700&old_slider:300", "Removed slider memory changed.");

        // Explicitly disabling all must act on the newest state, even with a stale UI.
        merged = BemRuntimeSettings.Merge(latest, baseline, Settings(), new HashSet<string> { "a", "b" });
        Expect(merged["Mod.a"]["enabled"] == "false" && merged["Mod.b"]["enabled"] == "false", "Disable-all did not close the externally enabled package.");
        desired = Settings(); desired["Mod.b"]["enabled"] = "true";
        merged = BemRuntimeSettings.Merge(latest, baseline, desired, new HashSet<string> { "a", "b" });
        Expect(merged["Mod.a"]["enabled"] == "false" && merged["Mod.b"]["enabled"] == "true", "Explicit same-character selection was not exclusive.");

        // Missing keys represented by UI defaults must not overwrite external values.
        var defaults = Settings(); defaults["CustomModel"]["overlay_visible"] = "false";
        latest = Settings(); latest["CustomModel"]["overlay_visible"] = "true";
        merged = BemRuntimeSettings.Merge(latest, defaults, defaults, null, Settings());
        Expect(merged["CustomModel"]["overlay_visible"] == "true", "A default overwrote a newly stored setting.");

        string ini = Path.Combine(root, "runtime.ini");
        var initial = BemRuntimeSettings.Commit(ini, new(StringComparer.Ordinal), Settings(), null);
        var changed = Settings("true", "outfit:b");
        BemRuntimeSettings.Commit(ini, initial.Settings, changed, null);
        var saved = BemRuntimeSettings.Commit(ini, baseline, desired, new HashSet<string> { "a", "b" });
        Expect(saved.Settings["Mod.b"]["enabled"] == "true" && BemRuntimeSettings.Read(ini)["Mod.a"]["options"] == "outfit:b", "Atomic commit lost a concurrent selection.");
        var info = new FileInfo(ini);
        Expect(saved.Stamp == (info.LastWriteTimeUtc.Ticks, info.Length), "Commit stamp does not describe its saved snapshot.");
        byte[] before = File.ReadAllBytes(ini);
        using (var occupied = new FileStream(ini, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            try { BemRuntimeSettings.Commit(ini, baseline, desired, null); throw new InvalidOperationException("Expected a locked-file failure."); }
            catch (IOException) { }
        }
        Expect(File.ReadAllBytes(ini).SequenceEqual(before), "Failed commit damaged the old file.");
        Expect(!Directory.EnumerateFiles(root, "*.tmp").Any(), "Commit leaked a temporary file.");
        foreach (var (input, expected) in new[] { ("=", "PLUS"), ("+", "PLUS"), ("OEM_PLUS", "PLUS"), ("Shift+=", "SHIFT+PLUS"), ("NUMPAD_PLUS", "ADD"), ("Ctrl+NumPad_Plus", "CTRL+ADD"), ("NONE", "NONE") })
            Expect(HotkeyService.TryNormalize(input, out string actual) && actual == expected, $"Hotkey normalization failed: {input}");
        Expect(!HotkeyService.TryNormalize("CTRL", out _), "Modifier-only key was accepted.");

        ConfigurationService.SettingsDirectory = Path.Combine(root, "profile");
        var service = new BemPackageService();
        Directory.CreateDirectory(service.PackageDirectory);
        MetadataFixture(Path.Combine(service.PackageDirectory, "a.bem"), "a", "character.one");
        MetadataFixture(Path.Combine(service.PackageDirectory, "b.bem"), "b", "character.one");
        MetadataFixture(Path.Combine(service.PackageDirectory, "c.bem"), "c", "character.two");
        service.Load();
        Expect(service.Packages.Count == 3, "Synthetic legacy packages failed to load.");
        await service.SaveAsync();
        string runtime = Path.Combine(service.Root, "runtime.ini");
        var loaded = BemRuntimeSettings.Read(runtime);
        var external = BemRuntimeSettings.Clone(loaded);
        external["Mod.a"]["enabled"] = "true"; external["Mod.a"]["appearance"] = "b";
        external["Mod.c"]["enabled"] = "true";
        external["CustomModel"]["overlay_visible"] = "true";
        BemRuntimeSettings.Commit(runtime, loaded, external, null);
        Expect(service.HasExternalChanges(), "External write was not detectable.");
        service.FastLoading = true; await service.SaveAsync();
        var persisted = BemRuntimeSettings.Read(runtime);
        Expect(persisted["Mod.a"]["appearance"] == "b" && persisted["Mod.a"]["enabled"] == "true", "Real service clobbered overlay package selection.");
        Expect(service.ModelOverlayVisible && service.Packages.Single(p => p.Id == "a").SelectedAppearance == "b", "Real service failed to adopt acknowledged external state.");
        Expect(!service.HasExternalChanges(), "Successful service commit has a stale stamp.");
        await service.SetEnabledAsync(service.Packages.Single(p => p.Id == "b"), true);
        persisted = BemRuntimeSettings.Read(runtime);
        Expect(persisted["Mod.a"]["enabled"] == "false" && persisted["Mod.b"]["enabled"] == "true" && persisted["Mod.c"]["enabled"] == "true", "Real service mutual exclusion changed another character or missed its peer.");
        loaded = BemRuntimeSettings.Read(runtime); external = BemRuntimeSettings.Clone(loaded);
        external["Mod.a"]["enabled"] = "true"; external["Mod.b"]["enabled"] = "false";
        external["Mod.missing"] = new(StringComparer.Ordinal) { ["enabled"] = "true", ["opaque"] = "retain" };
        BemRuntimeSettings.Commit(runtime, loaded, external, null);
        await service.DisableAllAsync();
        persisted = BemRuntimeSettings.Read(runtime);
        Expect(persisted.Where(p => p.Key.StartsWith("Mod.")).All(p => p.Value["enabled"] == "false"), "Real service disable-all missed stale external state.");
        Expect(persisted["Mod.missing"]["opaque"] == "retain", "Disabling an unavailable package lost its unknown fields.");
        Console.WriteLine($"Model overlay settings: {checks} checks passed.");
        return 0;
    }
}
