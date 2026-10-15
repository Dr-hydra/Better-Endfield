using System.Text;
using System.Text.Json;
using BetterEndfieldNext.UI.Services;

internal static class SteamLaunchOptionsChecks
{
    public static int Run(string workspace)
    {
        int checks = 0;
        void Check(bool condition, string message) { if (!condition) throw new Exception(message); checks++; }
        void Reject(Action action, string code)
        {
            try { action(); throw new Exception("Expected launch-options rejection: " + code); }
            catch (SteamIntegrationException exception) { Check(exception.Code == code, "Unexpected launch-options rejection: " + exception.Code); }
        }
        const string first = "\"G:\\BE 中文\\BetterEndfieldNext.Injector.exe\" --game \"G:\\国服\\Endfield.exe\" -- -logFile \"G:\\日志\\a.txt\" --steam-command %command%";
        string second = first.Replace("Injector", "Launcher", StringComparison.Ordinal);
        string MakeRoot(string name)
        {
            string root = Path.Combine(workspace, name);
            Directory.CreateDirectory(root);
            File.WriteAllText(Path.Combine(root, "steam.exe"), "Synthetic, never executed.");
            return root;
        }
        static string Quote(string value) => "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
        static string Config(string? option) => "// retained prefix\r\n\"UserLocalConfigStore\"\r\n{\r\n\t\"Software\" { \"Valve\" { \"Steam\" { \"apps\" {\r\n" +
            "\t\t\"888\" { \"LaunchOptions\" \"do not change\" }\r\n" +
            "\t\t\"4732690\" { /* preserve eula */ \"eula\" \"accepted\" " + (option is null ? "" : "\"LaunchOptions\" " + Quote(option)) + " }\r\n" +
            "\t} } } }\r\n\t\"Unrelated\" \"保持原字节\"\r\n}\r\n// retained suffix\r\n";
        string AddAccount(string root, uint id, string? option = null, Encoding? encoding = null)
        {
            string path = Path.Combine(root, "userdata", id.ToString(), "config", "localconfig.vdf");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, Config(option), encoding ?? new UTF8Encoding(false));
            return path;
        }
        void Login(string root, params (uint Account, bool Recent)[] accounts)
        {
            Directory.CreateDirectory(Path.Combine(root, "config"));
            File.WriteAllText(Path.Combine(root, "config", "loginusers.vdf"), "\"users\" { " + string.Join(" ", accounts.Select(a =>
                Quote((76561197960265728UL + a.Account).ToString()) + " { \"MostRecent\" \"" + (a.Recent ? "1" : "0") + "\" }")) + " }");
        }
        string Exe(string root) => Path.Combine(root, "steam.exe");

        string missingRoot = MakeRoot("launch missing options");
        string missingFile = AddAccount(missingRoot, 101);
        string missingBefore = File.ReadAllText(missingFile);
        bool running = false;
        uint? active = 101;
        var missing = new SteamLaunchOptionsService(Path.Combine(missingRoot, "state"), () => running, () => active);
        Check(!missing.IsConfigured(Exe(missingRoot), first), "Missing launch options reported configured.");
        missing.ValidateCanConfigure(Exe(missingRoot), first);
        Check(File.ReadAllText(missingFile) == missingBefore && !File.Exists(Path.Combine(missingRoot, "state", "launch-options.json")), "Preflight mutated Steam or the receipt.");
        missing.EnsureConfigured(Exe(missingRoot), first);
        Check(missing.IsConfigured(Exe(missingRoot), first), "Unicode command did not round trip.");
        string inserted = File.ReadAllText(missingFile);
        Check(inserted.Contains("\"888\" { \"LaunchOptions\" \"do not change\" }", StringComparison.Ordinal) && inserted.StartsWith("// retained prefix\r\n", StringComparison.Ordinal) && inserted.EndsWith("// retained suffix\r\n", StringComparison.Ordinal), "Unrelated entries/comments changed.");
        running = true;
        missing.EnsureConfigured(Exe(missingRoot), first);
        Check(File.ReadAllText(missingFile) == inserted, "Exact running-client match rewrote localconfig.");
        Reject(() => missing.EnsureConfigured(Exe(missingRoot), second), "launch_options_pending");
        Reject(missing.Restore, "launch_options_pending");
        Check(File.ReadAllText(missingFile) == inserted, "Running Steam configuration was modified.");
        running = false;
        missing.EnsureConfigured(Exe(missingRoot), second);
        missing.Restore();
        Check(!missing.IsConfigured(Exe(missingRoot), second) && !File.ReadAllText(missingFile).Contains(Quote(second), StringComparison.Ordinal), "Absent original option was not restored.");

        string existingRoot = MakeRoot("launch original value");
        string original = "\"C:\\Some launcher.exe\" --custom \"quoted value\"";
        string existingFile = AddAccount(existingRoot, 202, original, new UTF8Encoding(true));
        byte[] originalBytes = File.ReadAllBytes(existingFile);
        var existing = new SteamLaunchOptionsService(Path.Combine(existingRoot, "state"), () => false, () => null);
        existing.EnsureConfigured(Exe(existingRoot), first);
        Check(File.ReadAllBytes(existingFile).AsSpan().StartsWith(new byte[] { 0xef, 0xbb, 0xbf }), "UTF-8 BOM was lost.");
        Check(File.ReadAllText(existingFile) == Config(original).Replace(Quote(original), Quote(first), StringComparison.Ordinal), "Existing-option edit changed unrelated text.");
        existing.EnsureConfigured(Exe(existingRoot), second);
        File.WriteAllText(existingFile, File.ReadAllText(existingFile).Replace("\"accepted\"", "\"later Steam edit\"", StringComparison.Ordinal), new UTF8Encoding(true));
        existing.Restore();
        Check(File.ReadAllText(existingFile) == Config(original).Replace("\"accepted\"", "\"later Steam edit\"", StringComparison.Ordinal), "Restore lost original options or a later unrelated Steam edit.");
        Check(File.ReadAllBytes(existingFile).AsSpan().StartsWith(originalBytes.AsSpan(0, 3)), "Restore changed encoding.");

        string conflictRoot = MakeRoot("launch user edits");
        string conflictFile = AddAccount(conflictRoot, 303, "old");
        var conflict = new SteamLaunchOptionsService(Path.Combine(conflictRoot, "state"), () => false, () => null);
        conflict.EnsureConfigured(Exe(conflictRoot), first);
        File.WriteAllText(conflictFile, Config("user manually changed this"));
        Reject(() => conflict.ValidateCanConfigure(Exe(conflictRoot), second), "launch_options_changed");
        Reject(() => conflict.EnsureConfigured(Exe(conflictRoot), second), "launch_options_changed");
        Reject(conflict.Restore, "launch_options_changed");
        Check(File.ReadAllText(conflictFile) == Config("user manually changed this"), "User's later option was overwritten.");
        File.WriteAllText(conflictFile, Config(second));
        conflict.EnsureConfigured(Exe(conflictRoot), second);
        conflict.Restore();
        Check(File.ReadAllText(conflictFile) == Config("old"), "Manually completed exact command was not adopted/restored.");

        string accountsRoot = MakeRoot("launch multiple accounts");
        string accountA = AddAccount(accountsRoot, 401, "A-original");
        string accountB = AddAccount(accountsRoot, 402, "B-original");
        Login(accountsRoot, (401, true), (402, false));
        bool accountRunning = false;
        uint? selectedAccount = 402;
        var accounts = new SteamLaunchOptionsService(Path.Combine(accountsRoot, "state"), () => accountRunning, () => selectedAccount);
        accounts.EnsureConfigured(Exe(accountsRoot), first);
        Check(File.ReadAllText(accountA) == Config(first) && File.ReadAllText(accountB) == Config("B-original"), "Closed client selected a stale registry account.");
        accountRunning = true;
        Check(!accounts.IsConfigured(Exe(accountsRoot), first), "Running client ignored its active registry account.");
        selectedAccount = null;
        Reject(() => accounts.IsConfigured(Exe(accountsRoot), first), "steam_account_required");
        accountRunning = false;
        Login(accountsRoot, (401, false), (402, true));
        accounts.EnsureConfigured(Exe(accountsRoot), second);
        accounts.Restore();
        Check(File.ReadAllText(accountA) == Config("A-original") && File.ReadAllText(accountB) == Config("B-original"), "Multi-account restore missed a managed account.");
        Login(accountsRoot, (401, true), (402, true));
        Reject(() => accounts.EnsureConfigured(Exe(accountsRoot), first), "steam_account_required");
        Login(accountsRoot, (401, false), (402, false));
        Reject(() => accounts.EnsureConfigured(Exe(accountsRoot), first), "steam_account_required");
        string loginPath = Path.Combine(accountsRoot, "config", "loginusers.vdf");
        string TimestampUsers(string firstTimestamp, string secondTimestamp) => "\"users\" { " +
            Quote((76561197960265728UL + 401).ToString()) + " { \"Timestamp\" " + Quote(firstTimestamp) + " } " +
            Quote((76561197960265728UL + 402).ToString()) + " { \"timestamp\" " + Quote(secondTimestamp) + " } }";
        AddAccount(accountsRoot, 499, "orphan remains unchanged");
        File.WriteAllText(loginPath, TimestampUsers("1792036194", "1781071224"));
        accounts.EnsureConfigured(Exe(accountsRoot), first);
        Check(File.ReadAllText(accountA) == Config(first) && File.ReadAllText(accountB) == Config("B-original"), "Unique latest login timestamp did not select the correct account.");
        accounts.Restore();
        File.WriteAllText(loginPath, TimestampUsers("1792036194", "1792036194"));
        Reject(() => accounts.EnsureConfigured(Exe(accountsRoot), first), "steam_account_required");
        File.WriteAllText(loginPath, TimestampUsers("1792036194", "invalid"));
        Reject(() => accounts.EnsureConfigured(Exe(accountsRoot), first), "steam_account_required");

        string parsingRoot = MakeRoot("launch parsing");
        string parsingFile = AddAccount(parsingRoot, 505, "old", new UnicodeEncoding(false, true));
        string lower = Config("old").Replace("UserLocalConfigStore", "userlocalconfigstore", StringComparison.Ordinal).Replace("LaunchOptions", "launchoptions", StringComparison.Ordinal);
        File.WriteAllText(parsingFile, lower, new UnicodeEncoding(false, true));
        var parsing = new SteamLaunchOptionsService(Path.Combine(parsingRoot, "state"), () => false, () => null);
        parsing.EnsureConfigured(Exe(parsingRoot), first);
        Check(parsing.IsConfigured(Exe(parsingRoot), first) && File.ReadAllBytes(parsingFile).AsSpan().StartsWith(new byte[] { 0xff, 0xfe }), "Case-insensitive or UTF-16 config failed.");
        parsing.Restore();
        Check(File.ReadAllText(parsingFile) == lower, "Case-insensitive edit was not surgical.");
        File.WriteAllText(parsingFile, Config("old").Replace("\"eula\" \"accepted\"", "\"LaunchOptions\" \"duplicate\"", StringComparison.Ordinal));
        Reject(() => parsing.EnsureConfigured(Exe(parsingRoot), first), "invalid_steam_config");
        File.WriteAllText(parsingFile, "\"UserLocalConfigStore\" { \"Software\" \"scalar\" }");
        Reject(() => parsing.EnsureConfigured(Exe(parsingRoot), first), "invalid_steam_config");
        File.WriteAllText(parsingFile, "/* unclosed comment");
        Reject(() => parsing.EnsureConfigured(Exe(parsingRoot), first), "invalid_steam_config");
        File.WriteAllText(parsingFile, "\"UserLocalConfigStore\" { \"keep\" \"unrelated\" }");
        parsing.EnsureConfigured(Exe(parsingRoot), first);
        Check(parsing.IsConfigured(Exe(parsingRoot), first) && File.ReadAllText(parsingFile).Contains("\"keep\" \"unrelated\"", StringComparison.Ordinal), "Missing intermediate objects were not created safely.");

        string raceRoot = MakeRoot("launch client-start race");
        string raceFile = AddAccount(raceRoot, 606, "original");
        int processChecks = 0;
        bool startDuringWrite = true;
        var race = new SteamLaunchOptionsService(Path.Combine(raceRoot, "state"), () => startDuringWrite && ++processChecks >= 3, () => 606);
        Reject(() => race.EnsureConfigured(Exe(raceRoot), first), "launch_options_pending");
        string raceReceipt = Path.Combine(raceRoot, "state", "launch-options.json");
        Check(File.ReadAllText(raceFile) == Config("original") && JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(File.ReadAllText(raceReceipt))!.Entries.Single().Pending, "Interrupted write lacked a recoverable journal.");
        startDuringWrite = false;
        race.EnsureConfigured(Exe(raceRoot), first);
        Check(race.IsConfigured(Exe(raceRoot), first), "Pending write could not resume.");
        // Simulate termination after localconfig replacement but before clearing Pending.
        var interrupted = JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(File.ReadAllText(raceReceipt))!;
        interrupted.Entries.Single().Pending = true;
        interrupted.Entries.Single().PreviousWritten = "original";
        File.WriteAllText(raceReceipt, JsonSerializer.Serialize(interrupted));
        race.Restore();
        Check(File.ReadAllText(raceFile) == Config("original"), "Post-write interruption could not restore.");
        processChecks = 0; startDuringWrite = true;
        Reject(() => race.EnsureConfigured(Exe(raceRoot), first), "launch_options_pending");
        startDuringWrite = false;
        race.Restore();
        Check(File.ReadAllText(raceFile) == Config("original") && JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(File.ReadAllText(raceReceipt))!.Entries.Count == 0,
            "A write interrupted before touching Steam could not be restored directly.");

        string lateRaceRoot = MakeRoot("launch client-start before rename");
        string lateRaceFile = AddAccount(lateRaceRoot, 609, "original");
        int lateChecks = 0;
        var lateRace = new SteamLaunchOptionsService(Path.Combine(lateRaceRoot, "state"), () => ++lateChecks >= 4, () => 609);
        Reject(() => lateRace.EnsureConfigured(Exe(lateRaceRoot), first), "launch_options_pending");
        Check(File.ReadAllText(lateRaceFile) == Config("original"), "A client start immediately before rename did not stop the write.");

        string changedRoot = MakeRoot("launch concurrent edit");
        string changedFile = AddAccount(changedRoot, 707, "original");
        int changedChecks = 0;
        var changed = new SteamLaunchOptionsService(Path.Combine(changedRoot, "state"), () =>
        {
            if (++changedChecks == 3) File.WriteAllText(changedFile, Config("outside edit"));
            return false;
        }, () => null);
        Reject(() => changed.EnsureConfigured(Exe(changedRoot), first), "steam_config_changed");
        Check(File.ReadAllText(changedFile) == Config("outside edit"), "Concurrent Steam configuration edit was overwritten.");

        string restoredRoot = MakeRoot("launch interrupted restore");
        string restoredFile = AddAccount(restoredRoot, 808, "original");
        var restored = new SteamLaunchOptionsService(Path.Combine(restoredRoot, "state"), () => false, () => null);
        restored.EnsureConfigured(Exe(restoredRoot), first);
        File.WriteAllText(restoredFile, Config("original"));
        restored.Restore();
        Check(JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(File.ReadAllText(Path.Combine(restoredRoot, "state", "launch-options.json")))!.Entries.Count == 0, "Already restored original value was not accepted.");
        restored.EnsureConfigured(Exe(restoredRoot), first);
        string restoredReceiptPath = Path.Combine(restoredRoot, "state", "launch-options.json");
        string validReceipt = File.ReadAllText(restoredReceiptPath);
        var invalidReceipt = JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(validReceipt)!;
        invalidReceipt.Entries.Single().SteamRoot = "relative/steam";
        File.WriteAllText(restoredReceiptPath, JsonSerializer.Serialize(invalidReceipt));
        Reject(restored.Restore, "invalid_receipt");
        Check(File.ReadAllText(restoredFile) == Config(first), "Malformed receipt changed Steam configuration.");
        invalidReceipt = JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(validReceipt)!;
        invalidReceipt.Entries.Add(invalidReceipt.Entries.Single());
        File.WriteAllText(restoredReceiptPath, JsonSerializer.Serialize(invalidReceipt));
        Reject(restored.Restore, "invalid_receipt");
        File.WriteAllText(restoredReceiptPath, validReceipt);
        restored.Restore();
        restored.Restore();
        Check(File.ReadAllText(restoredFile) == Config("original"), "Repeated restore changed the restored configuration.");
        Check(!Directory.EnumerateFiles(workspace, "*.be-*.tmp", SearchOption.AllDirectories).Any(), "Launch-options transaction left temporary files.");
        return checks;
    }
}
