using System.Diagnostics;
using System.Text;
using BetterEndfieldNext.UI.Services;

internal static class SteamReplacementChecks
{
    public static async Task<int> RunAsync(string workspace)
    {
        int checks = 0;
        void Check(bool condition, string message) { if (!condition) throw new Exception(message); checks++; }
        void Reject(Action action, string code)
        {
            try { action(); throw new Exception("Expected replacement rejection: " + code); }
            catch (SteamIntegrationException exception) { Check(exception.Code == code, "Unexpected replacement rejection: " + exception.Code); }
        }
        async Task RejectAsync(Func<Task> action, string code)
        {
            try { await action(); throw new Exception("Expected replacement rejection: " + code); }
            catch (SteamIntegrationException exception) { Check(exception.Code == code, "Unexpected replacement rejection: " + exception.Code); }
        }
        using var http = new HttpClient();
        Fixture Make(string name) => new(Path.Combine(workspace, name), http);

        var empty = Make("managed empty placeholder");
        await empty.Service.ApplyAsync(empty.Library, empty.Game, empty.Metadata, [empty.Library]);
        await empty.Service.ApplyAsync(empty.Library, empty.Game, empty.Metadata, [empty.Library]);
        Check(new FileInfo(empty.Paths.Placeholder).Length == 0 && empty.Service.ReadReceipt() is { Pending: false },
            "Reapplying a managed empty placeholder failed.");

        // Match the observed conflict: Steam adds its app ID beside BE's empty EXE.
        string appId = Path.Combine(empty.Paths.GameDirectory, "steam_appid.txt");
        byte[] appIdBytes = Encoding.ASCII.GetBytes("4732690\n\0");
        File.WriteAllBytes(appId, appIdBytes);
        await empty.Service.ApplyAsync(empty.Library, empty.Game, empty.Metadata, [empty.Library]);
        Check(File.ReadAllBytes(appId).SequenceEqual(appIdBytes), "Ordinary reapply changed Steam's valid app-ID sidecar.");
        File.WriteAllText(Path.Combine(empty.Paths.GameDirectory, "unknown.log"), "Unknown file requires explicit confirmation.");
        var appIdPlan = empty.Prepare();
        Check(File.ReadAllBytes(appId).SequenceEqual(appIdBytes) && new FileInfo(empty.Paths.Placeholder).Length == 0,
            "Preparing the Steam app-ID conflict modified the installation.");
        await empty.Service.ReplaceAsync(appIdPlan, CancellationToken.None);
        Check(!File.Exists(appId) && !File.Exists(Path.Combine(empty.Paths.GameDirectory, "unknown.log")) &&
            new FileInfo(empty.Paths.Placeholder).Length == 0 && File.Exists(empty.Paths.Manifest),
            "Confirmed replacement did not remove all reviewed contents beside the empty EXE.");

        foreach (bool nested in new[] { false, true })
        {
            var sidecarRestore = Make(nested ? "nested sidecar restore" : "sidecar restore");
            if (nested) sidecarRestore.Metadata.Executable = "Global/Launcher.exe";
            await sidecarRestore.Service.ApplyAsync(sidecarRestore.Library, sidecarRestore.Game, sidecarRestore.Metadata, [sidecarRestore.Library]);
            string restoreAppId = Path.Combine(Path.GetDirectoryName(sidecarRestore.Paths.Placeholder)!, "steam_appid.txt");
            File.WriteAllBytes(restoreAppId, appIdBytes);
            await sidecarRestore.Service.ApplyAsync(sidecarRestore.Library, sidecarRestore.Game, sidecarRestore.Metadata, [sidecarRestore.Library]);
            Check(File.ReadAllBytes(restoreAppId).SequenceEqual(appIdBytes), "Reapply changed a valid executable-adjacent Steam sidecar.");
            await sidecarRestore.Service.RestoreAsync();
            Check(File.ReadAllBytes(restoreAppId).SequenceEqual(appIdBytes) && Directory.Exists(Path.GetDirectoryName(restoreAppId)) &&
                !File.Exists(sidecarRestore.Paths.Placeholder) && !File.Exists(sidecarRestore.Paths.Manifest),
                "Ordinary restore removed Steam's sidecar or failed to remove BE's placeholder/manifest.");
        }

        foreach (bool wrongId in new[] { true, false })
        {
            var invalidSidecar = Make(wrongId ? "wrong app ID" : "unrelated subtree app ID");
            await invalidSidecar.Service.ApplyAsync(invalidSidecar.Library, invalidSidecar.Game, invalidSidecar.Metadata, [invalidSidecar.Library]);
            string invalidSidecarDirectory = wrongId ? invalidSidecar.Paths.GameDirectory : Path.Combine(invalidSidecar.Paths.GameDirectory, "Other");
            Directory.CreateDirectory(invalidSidecarDirectory);
            string invalidAppId = Path.Combine(invalidSidecarDirectory, "steam_appid.txt");
            byte[] invalidBytes = wrongId ? Encoding.ASCII.GetBytes("1111111\n\0") : appIdBytes;
            File.WriteAllBytes(invalidAppId, invalidBytes);
            await RejectAsync(() => invalidSidecar.Service.ApplyAsync(invalidSidecar.Library, invalidSidecar.Game, invalidSidecar.Metadata, [invalidSidecar.Library]), "install_changed");
            await RejectAsync(() => invalidSidecar.Service.RestoreAsync(), "install_changed");
            Check(File.ReadAllBytes(invalidAppId).SequenceEqual(invalidBytes), "Rejected sidecar was modified.");
        }

        var foreign = Make("foreign executable and logs");
        foreign.AddForeignContent();
        string originalManifest = File.ReadAllText(foreign.Paths.Manifest);
        byte[] originalExe = File.ReadAllBytes(foreign.Paths.Placeholder);
        string log = Path.Combine(foreign.Paths.GameDirectory, "logs", "launcher.log");
        string unrelated = Path.Combine(foreign.Library, "steamapps", "common", "Other Game", "keep.bin");
        Directory.CreateDirectory(Path.GetDirectoryName(unrelated)!);
        File.WriteAllText(unrelated, "unrelated game must remain");
        var plan = foreign.Prepare();
        Check(SteamIntegrationService.SamePath(plan.GameDirectory, foreign.Paths.GameDirectory) &&
            SteamIntegrationService.SamePath(plan.Manifest, foreign.Paths.Manifest), "Replacement preview showed the wrong paths.");
        Check(plan.FileCount >= 2 && plan.TotalBytes >= originalExe.Length + new FileInfo(log).Length,
            "Replacement preview undercounted the files being deleted.");
        Check(File.ReadAllText(foreign.Paths.Manifest) == originalManifest && File.ReadAllBytes(foreign.Paths.Placeholder).SequenceEqual(originalExe) &&
            File.Exists(log) && foreign.Service.ReadReceipt() is null, "Preparing replacement deleted content or claimed ownership.");
        await foreign.Service.ReplaceAsync(plan, CancellationToken.None);
        Check(new FileInfo(foreign.Paths.Placeholder).Length == 0 && !File.Exists(log), "Confirmed replacement retained the foreign EXE or logs.");
        Check(SteamKeyValues.Parse(File.ReadAllText(foreign.Paths.Manifest)).Object("AppState")!.String("appid") == SteamMetadata.EndfieldAppId &&
            foreign.Service.ReadReceipt() is { Pending: false }, "Confirmed replacement did not install the ACF and completed receipt.");
        Check(File.ReadAllText(foreign.Game) == Fixture.GameContent && File.ReadAllText(unrelated) == "unrelated game must remain",
            "Replacement touched CN files or another Steam game.");

        foreach (bool gameInsideReplacement in new[] { true, false })
        {
            var overlap = Make(gameInsideReplacement ? "CN inside replacement" : "replacement inside CN");
            overlap.AddForeignContent();
            overlap.Game = gameInsideReplacement
                ? Path.Combine(overlap.Paths.GameDirectory, "CN", "Endfield.exe")
                : Path.Combine(overlap.Library, "steamapps", "common", "Endfield.exe");
            Directory.CreateDirectory(Path.GetDirectoryName(overlap.Game)!);
            File.WriteAllText(overlap.Game, Fixture.GameContent);
            Reject(() => overlap.Prepare(), "game_directory_overlap");
            Check(File.ReadAllText(overlap.Game) == Fixture.GameContent && File.ReadAllText(overlap.Paths.Placeholder) == Fixture.ForeignContent,
                "Rejected CN overlap modified either installation.");
        }

        var otherLibrary = Make("other Steam library");
        otherLibrary.AddForeignContent();
        string other = Path.Combine(otherLibrary.Root, "Other library");
        Directory.CreateDirectory(Path.Combine(other, "steamapps"));
        string otherManifest = Path.Combine(other, "steamapps", "appmanifest_4732690.acf");
        File.WriteAllText(otherManifest, "other library manifest");
        Reject(() => otherLibrary.Service.PrepareReplacement(otherLibrary.Library, otherLibrary.Game, otherLibrary.Metadata,
            [otherLibrary.Library, other]), "other_library_install");
        Reject(() => otherLibrary.Service.PrepareReplacement(otherLibrary.Library, otherLibrary.Game, otherLibrary.Metadata, []), "unregistered_library");
        Check(File.ReadAllText(otherManifest) == "other library manifest" && File.ReadAllText(otherLibrary.Paths.Placeholder) == Fixture.ForeignContent,
            "Library guards modified an installation.");

        foreach (string protection in new[] { "BE child", "BE parent", "state child", "state parent" })
        {
            var protectedFixture = Make("protected " + protection);
            protectedFixture.AddForeignContent();
            bool child = protection.EndsWith("child", StringComparison.Ordinal);
            string protectedPath = child ? Path.Combine(protectedFixture.Paths.GameDirectory, "protected") : Path.GetDirectoryName(protectedFixture.Paths.GameDirectory)!;
            string state = protection.StartsWith("state", StringComparison.Ordinal) ? protectedPath : protectedFixture.State;
            string install = protection.StartsWith("BE", StringComparison.Ordinal) ? protectedPath : protectedFixture.Install;
            var guarded = new SteamIntegrationService(state, http, () => false, gameIsRunning: () => false, protectedInstallDirectory: install);
            Reject(() => guarded.PrepareReplacement(protectedFixture.Library, protectedFixture.Game, protectedFixture.Metadata,
                [protectedFixture.Library]), "protected_directory");
            Check(File.ReadAllText(protectedFixture.Paths.Placeholder) == Fixture.ForeignContent, "Protected directory guard deleted content.");
        }

        foreach (string change in new[] { "new file", "changed file", "new directory", "changed manifest" })
        {
            var changed = Make("after confirmation " + change);
            changed.AddForeignContent();
            var changedPlan = changed.Prepare();
            switch (change)
            {
                case "new file": File.WriteAllText(Path.Combine(changed.Paths.GameDirectory, "new.asset"), "new data after preview"); break;
                case "changed file": File.AppendAllText(changed.Paths.Placeholder, " changed after preview"); break;
                case "new directory": Directory.CreateDirectory(Path.Combine(changed.Paths.GameDirectory, "new empty folder")); break;
                default: File.AppendAllText(changed.Paths.Manifest, "\r\n// changed after preview\r\n"); break;
            }
            byte[] changedExe = File.ReadAllBytes(changed.Paths.Placeholder);
            string changedManifest = File.ReadAllText(changed.Paths.Manifest);
            await RejectAsync(() => changed.Service.ReplaceAsync(changedPlan, CancellationToken.None), "replacement_changed");
            Check(File.ReadAllBytes(changed.Paths.Placeholder).SequenceEqual(changedExe) && File.ReadAllText(changed.Paths.Manifest) == changedManifest,
                "Stale confirmation removed files before checking for changes.");
        }

        foreach (string block in new[] { "cancelled", "steam running", "game running" })
        {
            var blocked = Make(block);
            blocked.AddForeignContent();
            var blockedPlan = blocked.Prepare();
            string blockedManifest = File.ReadAllText(blocked.Paths.Manifest);
            if (block == "cancelled")
            {
                using var cancellation = new CancellationTokenSource();
                cancellation.Cancel();
                try { await blocked.Service.ReplaceAsync(blockedPlan, cancellation.Token); throw new Exception("Cancelled replacement completed."); }
                catch (OperationCanceledException) { checks++; }
            }
            else
            {
                blocked.SteamRunning = block == "steam running";
                blocked.GameRunning = block == "game running";
                string code = blocked.SteamRunning ? "steam_running" : "game_running";
                Reject(() => blocked.Prepare(), code);
                await RejectAsync(() => blocked.Service.ReplaceAsync(blockedPlan, CancellationToken.None), code);
            }
            Check(File.ReadAllText(blocked.Paths.Placeholder) == Fixture.ForeignContent && File.ReadAllText(blocked.Paths.Manifest) == blockedManifest,
                "Cancellation or process guards modified installation files.");
        }

        var reparse = Make("junction guard");
        reparse.AddForeignContent();
        string target = Path.Combine(reparse.Root, "outside replacement target");
        Directory.CreateDirectory(target);
        string sentinel = Path.Combine(target, "keep.txt");
        File.WriteAllText(sentinel, "outside target must remain");
        string junction = Path.Combine(reparse.Paths.GameDirectory, "linked folder");
        CreateJunction(junction, target);
        try
        {
            Check((File.GetAttributes(junction) & FileAttributes.ReparsePoint) != 0, "Junction fixture is not a reparse point.");
            Reject(() => reparse.Prepare(), "reparse_point");
            Check(File.ReadAllText(sentinel) == "outside target must remain", "Reparse guard followed and deleted an external target.");
        }
        finally
        {
            // Nonrecursive deletion removes only the fixture junction itself.
            if ((File.GetAttributes(junction) & FileAttributes.ReparsePoint) != 0) Directory.Delete(junction);
        }

        var locked = Make("locked file deletion");
        locked.AddForeignContent();
        var lockedPlan = locked.Prepare();
        string lockedManifest = File.ReadAllText(locked.Paths.Manifest);
        using (var held = new FileStream(locked.Paths.Placeholder, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            bool failed = false;
            try { await locked.Service.ReplaceAsync(lockedPlan, CancellationToken.None); }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or SteamIntegrationException) { failed = true; }
            Check(failed, "Replacement reported success while its EXE could not be deleted.");
            Check(File.ReadAllText(locked.Paths.Placeholder) == Fixture.ForeignContent && File.ReadAllText(locked.Paths.Manifest) == lockedManifest,
                "Failed deletion overwrote the locked EXE or published a new manifest.");
            Check(locked.Service.ReadReceipt() is { Pending: true } && File.Exists(Path.Combine(locked.State, "replacement.json")),
                "Interrupted replacement did not record pending ownership.");
            await RejectAsync(() => locked.Service.ApplyAsync(locked.Library, locked.Game, locked.Metadata, [locked.Library]), "replacement_pending");
            await RejectAsync(() => locked.Service.RestoreAsync(), "replacement_pending");
        }
        Check(File.ReadAllText(locked.Game) == Fixture.GameContent, "Deletion failure affected the CN game.");
        await locked.Service.ReplaceAsync(locked.Prepare(), CancellationToken.None);
        Check(locked.Service.ReadReceipt() is { Pending: false } && !File.Exists(Path.Combine(locked.State, "replacement.json")) &&
            new FileInfo(locked.Paths.Placeholder).Length == 0 && File.Exists(locked.Paths.Manifest),
            "Reconfirming after the file lock was released did not complete replacement and clear pending state.");

        foreach (bool startSteam in new[] { false, true })
        {
            var midDelete = Make(startSteam ? "Steam starts during deletion" : "new file during deletion");
            midDelete.AddForeignContent();
            bool armed = false, fired = false, simulatedRunning = false;
            int processChecks = 0;
            string newFile = Path.Combine(midDelete.Paths.GameDirectory, "new-unconfirmed.bin");
            var raceService = new SteamIntegrationService(midDelete.State, http, () =>
            {
                if (armed && ++processChecks == 3)
                {
                    fired = true;
                    if (startSteam) simulatedRunning = true;
                    else File.WriteAllText(newFile, "not included in the confirmed snapshot");
                }
                return simulatedRunning;
            }, gameIsRunning: () => false, protectedInstallDirectory: midDelete.Install);
            var racePlan = raceService.PrepareReplacement(midDelete.Library, midDelete.Game, midDelete.Metadata, [midDelete.Library]);
            // Trigger after both the initial guard and snapshot revalidation,
            // immediately before the first individually confirmed deletion.
            processChecks = 0;
            armed = true;
            await RejectAsync(() => raceService.ReplaceAsync(racePlan, CancellationToken.None), startSteam ? "steam_running" : "replacement_changed");
            Check(fired, "The deletion race fixture did not reach its intended mutation point.");
            if (startSteam)
                Check(File.ReadAllText(midDelete.Paths.Placeholder) == Fixture.ForeignContent,
                    "Replacement deleted the EXE after Steam started during deletion.");
            else
                Check(File.ReadAllText(newFile) == "not included in the confirmed snapshot",
                    "Nonrecursive confirmed deletion removed a newly appeared, unconfirmed file.");
            Check(raceService.ReadReceipt() is { Pending: true } && File.ReadAllText(midDelete.Game) == Fixture.GameContent,
                "Interrupted deletion lost pending state or affected CN files.");
            armed = false;
            simulatedRunning = false;
            var recovery = raceService.PrepareReplacement(midDelete.Library, midDelete.Game, midDelete.Metadata, [midDelete.Library]);
            await raceService.ReplaceAsync(recovery, CancellationToken.None);
            Check(raceService.ReadReceipt() is { Pending: false } && !File.Exists(Path.Combine(midDelete.State, "replacement.json")) &&
                new FileInfo(midDelete.Paths.Placeholder).Length == 0 && !File.Exists(newFile),
                "Reconfirming the remaining files did not recover an interrupted deletion.");
        }
        return checks;
    }

    private static void CreateJunction(string link, string target)
    {
        static string Quote(string value) => "'" + value.Replace("'", "''", StringComparison.Ordinal) + "'";
        var start = new ProcessStartInfo("powershell.exe") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true };
        start.ArgumentList.Add("-NoProfile");
        start.ArgumentList.Add("-NonInteractive");
        start.ArgumentList.Add("-Command");
        start.ArgumentList.Add("New-Item -ItemType Junction -Path " + Quote(link) + " -Target " + Quote(target) + " -ErrorAction Stop | Out-Null");
        using var process = Process.Start(start) ?? throw new Exception("Unable to start junction fixture helper.");
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0) throw new Exception("Junction fixture creation failed: " + error);
    }

    private sealed class Fixture
    {
        public const string GameContent = "Synthetic CN executable, never executed.";
        public const string ForeignContent = "Synthetic foreign Steam executable, never executed.";
        public string Root { get; }
        public string Library { get; }
        public string Game { get; set; }
        public string State { get; }
        public string Install { get; }
        public bool SteamRunning { get; set; }
        public bool GameRunning { get; set; }
        public SteamMetadata Metadata { get; } = new()
        {
            Name = "Synthetic replacement test", InstallDirectory = "Steam international placeholder", Executable = "Endfield.exe",
            BuildId = "100", Depots = [new("60001", "1234567890123456789")]
        };
        public SteamInstallPaths Paths => SteamIntegrationService.GetPaths(Library, Metadata);
        public SteamIntegrationService Service { get; }
        public Fixture(string root, HttpClient http)
        {
            Root = Path.GetFullPath(root);
            Library = Path.Combine(Root, "Steam Library 中文");
            Directory.CreateDirectory(Path.Combine(Library, "steamapps"));
            Game = Path.Combine(Root, "国服目录", "Endfield.exe");
            Directory.CreateDirectory(Path.GetDirectoryName(Game)!);
            File.WriteAllText(Game, GameContent);
            State = Path.Combine(Root, "BE state");
            Install = Path.Combine(Root, "BE install");
            Directory.CreateDirectory(Install);
            Service = new SteamIntegrationService(State, http, () => SteamRunning, gameIsRunning: () => GameRunning, protectedInstallDirectory: Install);
        }
        public SteamReplacementPlan Prepare() => Service.PrepareReplacement(Library, Game, Metadata, [Library]);
        public void AddForeignContent()
        {
            Directory.CreateDirectory(Path.Combine(Paths.GameDirectory, "logs"));
            File.WriteAllText(Paths.Placeholder, ForeignContent);
            File.WriteAllText(Path.Combine(Paths.GameDirectory, "logs", "launcher.log"), "Synthetic launcher log.");
            File.WriteAllText(Paths.Manifest, "// existing Steam manifest\r\n" + SteamIntegrationService.GenerateAcf(Metadata));
        }
    }
}
