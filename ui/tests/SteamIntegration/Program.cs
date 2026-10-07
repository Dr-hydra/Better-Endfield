using System.Net;
using System.Text;
using System.Text.Json;
using BetterEndfield.UI.Services;

internal static class Program
{
    private static int _checks;
    private static void Check(bool condition, string message) { if (!condition) throw new Exception(message); _checks++; }
    private static void Reject(Action action, string code)
    {
        try { action(); throw new Exception("Expected rejection: " + code); }
        catch (SteamIntegrationException exception) { Check(exception.Code == code, "Unexpected rejection: " + exception.Code); }
    }
    private static async Task RejectAsync(Func<Task> action, string code)
    {
        try { await action(); throw new Exception("Expected rejection: " + code); }
        catch (SteamIntegrationException exception) { Check(exception.Code == code, "Unexpected rejection: " + exception.Code); }
    }
    private static SteamMetadata Metadata(string build = "100") => new()
    {
        Name = "Synthetic Steam test", InstallDirectory = "Steam 占位测试", Executable = "Global/Launcher.exe", BuildId = build,
        Depots = [new("60001", "18446744073709551615"), new("60002", "1234567890123456789")]
    };

    private static string Api(SteamMetadata metadata) => JsonSerializer.Serialize(new
    {
        data = new Dictionary<string, object>
        {
            [SteamMetadata.EndfieldAppId] = new
            {
                common = new { name = metadata.Name },
                config = new
                {
                    installdir = metadata.InstallDirectory,
                    launch = new Dictionary<string, object>
                    {
                        ["0"] = new { executable = "linux", config = new { oslist = "linux" } },
                        ["1"] = new { executable = metadata.Executable, arguments = "-channel steam", type = "default", config = new { oslist = "windows" } }
                    }
                },
                depots = new Dictionary<string, object>
                {
                    ["branches"] = new { @public = new { buildid = metadata.BuildId } },
                    ["60001"] = new { config = new { oslist = "windows" }, manifests = new { @public = new { gid = metadata.Depots[0].Manifest } } },
                    ["60002"] = new { manifests = new { @public = metadata.Depots[1].Manifest } },
                    ["60003"] = new { config = new { oslist = "linux" }, manifests = new { @public = "321" } }
                }
            }
        }
    });

    public static async Task<int> Main(string[] arguments)
    {
        string workspace = Path.GetFullPath(arguments.Length == 1 ? arguments[0] : Path.GetTempPath());
        string root = Path.Combine(workspace, "be-steam-test-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        string library = Path.Combine(root, "Steam Library 中文");
        Directory.CreateDirectory(Path.Combine(library, "steamapps"));
        string game = Path.Combine(root, "国服目录", "Endfield.exe");
        Directory.CreateDirectory(Path.GetDirectoryName(game)!); File.WriteAllText(game, "Synthetic, never executable.");
        string steam = Path.Combine(root, "steam.exe"); File.WriteAllText(steam, "Synthetic, never executable.");
        bool running = false;
        var handler = new Handler();
        using var http = new HttpClient(handler);
        var service = new SteamIntegrationService(Path.Combine(root, "BE settings"), http, () => running);
        var metadata = Metadata(); metadata.Validate();
        var settings = JsonSerializer.Deserialize<BetterEndfield.UI.Models.AppSettings>("{\"LoaderMode\":\"injector\",\"futureSetting\":\"keep\"}")!;
        settings.SteamIntegration.LaunchAsAdministrator = true;
        using (var settingsJson = JsonDocument.Parse(JsonSerializer.Serialize(settings)))
        {
            Check(settingsJson.RootElement.GetProperty("futureSetting").GetString() == "keep", "Unknown application settings were lost.");
            Check(settings.LoaderMode == "injector" && settingsJson.RootElement.GetProperty("SteamIntegration").GetProperty("LaunchAsAdministrator").GetBoolean(), "Steam settings changed the existing loader preference.");
        }
        string previewMetadata = Path.Combine(root, "preview-metadata.json");
        File.WriteAllText(previewMetadata, JsonSerializer.Serialize(metadata));
        var parsed = SteamMetadata.Parse(Api(metadata)); parsed.Validate();
        Check(parsed.Depots.Count == 2 && parsed.Executable == metadata.Executable, "Windows depot/launch selection failed.");
        Check(parsed.Depots[0].Manifest == metadata.Depots[0].Manifest, "64-bit manifest ID lost precision.");
        Check(parsed.LaunchArguments == "-channel steam", "Upstream launch arguments were lost.");
        Check(SteamMetadata.Parse("{\"data\":{\"4732690\":{\"common\":{\"name\":\"Endfield\"}}}}").MissingFields.Count == 4, "Unpublished metadata was accepted.");
        Reject(() => SteamMetadata.Parse("{\"data\":{\"1\":{}}}"), "wrong_app");
        Reject(() => SteamMetadata.Parse("{\"Name\":null}"), "invalid_metadata");
        Reject(() => SteamMetadata.Parse("[]"), "invalid_metadata");
        foreach (string path in new[] { "../evil.exe", "C:\\evil.exe", "/evil.exe", "a//b.exe", "a/CON.exe", "a./b.exe", "a /b.exe", "a:b.exe" })
            Reject(() => SteamMetadata.ValidateRelativePath(path), "invalid_relative_path");
        Reject(() => SteamMetadata.ValidateRelativePath("a/b", true), "invalid_relative_path");
        var overflow = Metadata(); overflow.Depots = [new("60001", "99999999999999999999")];
        Reject(overflow.Validate, "invalid_metadata_ids");
        var duplicate = Metadata(); duplicate.Depots.Add(duplicate.Depots[0]); Reject(duplicate.Validate, "invalid_metadata_ids");

        string acf = SteamIntegrationService.GenerateAcf(metadata);
        var tree = SteamKeyValues.Parse(acf);
        tree.Object("AppState")!.Set("LastPlayed", "42"); tree.Object("AppState")!.Set("LastOwner", "76561198000000000");
        tree.Object("AppState")!.Set("custom", "quotes \" and backslash \\ 中文");
        string updated = SteamIntegrationService.GenerateAcf(Metadata("101"), tree.ToString());
        var updatedState = SteamKeyValues.Parse(updated).Object("AppState")!;
        Check(updatedState.String("LastPlayed") == "42" && updatedState.String("LastOwner") == "76561198000000000", "Steam runtime fields were overwritten.");
        Check(updatedState.String("custom") == "quotes \" and backslash \\ 中文", "Unknown ACF field was lost.");
        Check(updatedState.Object("InstalledDepots")!.Objects.Count() == 2, "Multi-depot ACF generation failed.");
        Reject(() => SteamIntegrationService.GenerateAcf(metadata, "\"AppState\" { \"appid\" \"1\" }"), "foreign_manifest");
        try { SteamKeyValues.Parse("\"x\" { \"y\" \"z\""); throw new Exception("Malformed VDF accepted."); }
        catch (InvalidDataException) { _checks++; }

        handler.Body = Api(metadata);
        running = true;
        var fetched = await service.FetchLatestAsync();
        Check(handler.Requests == 1 && service.ReadCache()!.Metadata.BuildId == "100", "Metadata query while Steam runs failed.");
        await RejectAsync(() => service.ApplyAsync(library, game, fetched, [library]), "steam_running");
        Check(!File.Exists(SteamIntegrationService.GetPaths(library, metadata).Manifest), "A blocked apply changed Steam files.");
        handler.Body = "{\"data\":{\"4732690\":{\"common\":{\"name\":\"Pending\"}}}}";
        Check((await service.FetchLatestAsync()).MissingFields.Count == 4 && service.ReadCache()!.Metadata.BuildId == "100", "Incomplete response clobbered the valid cache.");
        handler.Status = HttpStatusCode.BadGateway;
        try { await service.FetchLatestAsync(); throw new Exception("HTTP failure accepted."); }
        catch (HttpRequestException) { Check(service.ReadCache()!.Metadata.BuildId == "100", "HTTP failure clobbered the cache."); }
        handler.Status = HttpStatusCode.OK;

        running = false;
        await RejectAsync(() => service.ApplyAsync(library, game, metadata, []), "unregistered_library");
        await service.ApplyAsync(library, game, metadata, [library]);
        var paths = SteamIntegrationService.GetPaths(library, metadata);
        Check(File.Exists(paths.Manifest) && File.Exists(paths.Placeholder) && new FileInfo(paths.Placeholder).Length == 0, "Initial install did not create a valid placeholder.");
        Check(service.ReadReceipt()!.Metadata.BuildId == "100", "Install receipt missing.");
        File.WriteAllText(paths.Manifest, tree.ToString());
        await service.ApplyAsync(library, game, Metadata("101"), [library]);
        Check(SteamKeyValues.Parse(File.ReadAllText(paths.Manifest)).Object("AppState")!.String("LastPlayed") == "42", "Version update lost playtime fields.");
        Check(service.ReadReceipt()!.Metadata.BuildId == "101", "Version update receipt was not committed.");
        Check(Directory.GetFiles(Path.Combine(root, "BE settings", "backups")).Length > 0, "ACF update lacked a backup.");
        var layout = Metadata("102"); layout.Executable = "Other.exe";
        await RejectAsync(() => service.ApplyAsync(library, game, layout, [library]), "layout_changed");
        running = true;
        string original = File.ReadAllText(paths.Manifest);
        await RejectAsync(() => service.ApplyAsync(library, game, Metadata("102"), [library]), "steam_running");
        await RejectAsync(() => service.RestoreAsync(), "steam_running");
        Check(File.ReadAllText(paths.Manifest) == original, "Running-client guard was not read-only.");
        running = false;
        string downloaded = Path.Combine(paths.GameDirectory, "downloaded.asset"); File.WriteAllText(downloaded, "keep");
        await RejectAsync(() => service.ApplyAsync(library, game, Metadata("102"), [library]), "install_changed");
        await RejectAsync(() => service.RestoreAsync(), "install_changed");
        Check(File.ReadAllText(downloaded) == "keep" && File.Exists(paths.Manifest), "Real installation content was changed.");
        File.Delete(downloaded);
        await service.RestoreAsync();
        Check(!File.Exists(paths.Manifest) && !File.Exists(paths.Placeholder) && service.ReadReceipt() is null, "Managed ACF restoration failed.");
        File.WriteAllText(paths.Manifest, "Foreign configuration");
        await RejectAsync(() => service.ApplyAsync(library, game, metadata, [library]), "existing_install");
        Check(File.ReadAllText(paths.Manifest) == "Foreign configuration", "Foreign ACF was overwritten.");
        File.Delete(paths.Manifest);
        string other = Path.Combine(root, "Other library"); Directory.CreateDirectory(Path.Combine(other, "steamapps"));
        File.WriteAllText(Path.Combine(other, "steamapps", "appmanifest_4732690.acf"), "Foreign install");
        await RejectAsync(() => service.ApplyAsync(library, game, metadata, [library, other]), "other_library_install");
        Check(SteamIntegrationService.GenerateLaunchCommand(game, "-force-d3d11").EndsWith(" -force-d3d11 %command%"), "Saved CN launch arguments were lost.");
        Reject(() => SteamIntegrationService.GenerateLaunchCommand(game, "%command%"), "invalid_launch_arguments");

        var store = new CompatibilityStore { Value = "~ HIGHDPIAWARE" };
        var compatibility = new SteamCompatibilityService(Path.Combine(root, "compatibility.json"), store, () => running);
        compatibility.Apply(steam, true);
        Check(store.Value == "~ HIGHDPIAWARE RUNASADMIN", "Administrator setting lost DPI flags.");
        store.Value += " GDIDPISCALING";
        compatibility.Restore();
        Check(store.Value == "~ HIGHDPIAWARE GDIDPISCALING", "Restore lost later compatibility changes.");
        store.Value = "~ RUNASADMIN HIGHDPIAWARE";
        compatibility.Apply(steam, false); compatibility.Restore();
        Check(SteamCompatibilityService.HasAdministrator(store.Value) && store.Value.Contains("HIGHDPIAWARE"), "Existing admin setting was not restored.");
        running = true; int writes = store.Writes;
        Reject(() => compatibility.Apply(steam, false), "steam_running");
        Check(store.Writes == writes, "Compatibility setting changed while Steam ran.");
        running = false;
        Reject(() => SteamCompatibilityService.SetAdministrator("~ RUNASINVOKER", true), "compatibility_conflict");
        Check(SteamCompatibilityService.SetAdministrator("~ RUNASADMIN", false) is null, "Empty compatibility flags were retained.");

        using (var held = new FileStream(Path.Combine(root, "BE settings", "operation.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            await RejectAsync(() => service.ApplyAsync(library, game, metadata, [library]), "operation_busy");
        string raceLibrary = Path.Combine(root, "Client start race"); Directory.CreateDirectory(Path.Combine(raceLibrary, "steamapps"));
        int processChecks = 0; bool startClientDuringWrite = true;
        var race = new SteamIntegrationService(Path.Combine(root, "Race settings"), http, () => startClientDuringWrite && ++processChecks >= 3);
        await RejectAsync(() => race.ApplyAsync(raceLibrary, game, metadata, [raceLibrary]), "steam_running");
        var racePaths = SteamIntegrationService.GetPaths(raceLibrary, metadata);
        Check(race.ReadReceipt()!.Pending && File.Exists(racePaths.Placeholder) && !File.Exists(racePaths.Manifest), "Interrupted ownership was not journaled.");
        startClientDuringWrite = false;
        await race.ApplyAsync(raceLibrary, game, metadata, [raceLibrary]);
        Check(!race.ReadReceipt()!.Pending && File.Exists(racePaths.Manifest), "Interrupted ACF setup could not resume.");
        await race.RestoreAsync();
        Check(race.ReadReceipt() is null && !File.Exists(racePaths.Placeholder), "Recovered ACF setup could not be removed.");
        InjectorLaunchGuard.EnsureAllowed(game);
        File.WriteAllText(Path.Combine(Path.GetDirectoryName(game)!, "xinput1_4.dll"), "Synthetic proxy");
        try { InjectorLaunchGuard.EnsureAllowed(game); throw new Exception("Duplicate loader was accepted."); }
        catch (InvalidOperationException) { _checks++; }
        File.Delete(Path.Combine(Path.GetDirectoryName(game)!, "xinput1_4.dll"));
        Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(game)!, "xinput1_4.dll"));
        try { InjectorLaunchGuard.EnsureAllowed(game); throw new Exception("Conflicting proxy directory was accepted."); }
        catch (InvalidOperationException) { _checks++; }

        await Task.WhenAll(Enumerable.Range(0, 8).Select(i => service.CacheAsync(Metadata((200 + i).ToString()))));
        service.ReadCache()!.Metadata.Validate();
        Check(!Directory.EnumerateFiles(Path.Combine(root, "BE settings"), "*.tmp").Any(), "Atomic cache write left temporary files.");
        Directory.CreateDirectory(workspace);
        File.WriteAllText(Path.Combine(workspace, "latest.json"), JsonSerializer.Serialize(new { checks = _checks, fixtureDirectory = root, previewMetadata }));
        Console.WriteLine($"Steam integration: {_checks} checks passed. Synthetic workspace: {root}");
        return 0;
    }

    private sealed class Handler : HttpMessageHandler
    {
        public string Body { get; set; } = "";
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public int Requests { get; private set; }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests++;
            return Task.FromResult(new HttpResponseMessage(Status) { Content = new StringContent(Body, Encoding.UTF8, "application/json") });
        }
    }
    private sealed class CompatibilityStore : ISteamCompatibilityStore
    {
        public string? Value { get; set; }
        public int Writes { get; private set; }
        public string? Read(string executable) => Value;
        public void Write(string executable, string? flags) { Value = flags; Writes++; }
    }
}
