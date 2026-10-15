using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetterEndfieldNext.UI.Services;

internal sealed class SteamLaunchOptionsReceipt
{
    [JsonConstructor]
    public SteamLaunchOptionsReceipt() { }
    [JsonPropertyName("Product")]
    public string Product { get; set; } = "BetterEndfieldNext.SteamLaunchOptions";
    [JsonPropertyName("Entries")]
    public List<SteamLaunchOptionsEntry> Entries { get; set; } = [];
}

internal sealed class SteamLaunchOptionsEntry
{
    [JsonConstructor]
    public SteamLaunchOptionsEntry() { }
    [JsonPropertyName("SteamRoot")]
    public string SteamRoot { get; set; } = "";
    [JsonPropertyName("AccountId")]
    public uint AccountId { get; set; }
    [JsonPropertyName("Original")]
    public string? Original { get; set; }
    [JsonPropertyName("Written")]
    public string Written { get; set; } = "";
    [JsonPropertyName("PreviousWritten")]
    public string? PreviousWritten { get; set; }
    [JsonPropertyName("Pending")]
    public bool Pending { get; set; }
}

// Steam owns localconfig while running. Only the selected user's LaunchOptions
// token is changed, after journaling its original value and checking for races.
internal sealed class SteamLaunchOptionsService(string stateDirectory, Func<bool> steamIsRunning, Func<uint?> activeAccountId)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private static readonly string[] OptionPath = ["UserLocalConfigStore", "Software", "Valve", "Steam", "apps", SteamMetadata.EndfieldAppId, "LaunchOptions"];
    private readonly string _state = Path.GetFullPath(stateDirectory);
    private string ReceiptPath => Path.Combine(_state, "launch-options.json");
    private sealed record Target(string SteamRoot, uint AccountId, string Path);

    public bool IsConfigured(string steamExecutable, string command)
    {
        ValidateCommand(command);
        var target = SelectTarget(steamExecutable);
        return Snapshot.Read(target.Path).Document.Read(OptionPath) == command;
    }

    public void ValidateCanConfigure(string steamExecutable, string command)
    {
        ValidateCommand(command);
        var target = SelectTarget(steamExecutable);
        string? current = Snapshot.Read(target.Path).Document.Read(OptionPath);
        ValidateChange(ReadReceipt(), target, current, command);
        if (current != command) RequireClosed();
    }

    public void EnsureConfigured(string steamExecutable, string command)
    {
        ValidateCommand(command);
        using var operation = AcquireLock();
        var target = SelectTarget(steamExecutable);
        var snapshot = Snapshot.Read(target.Path);
        string? current = snapshot.Document.Read(OptionPath);
        var receipt = ReadReceipt();
        var entry = ValidateChange(receipt, target, current, command);
        if (current == command)
        {
            // A user can also finish a pending setup in Steam themselves.
            // Adopt that exact value only when this account is already managed.
            if (entry is not null && (entry.Pending || entry.Written != command))
            {
                entry.Written = command; entry.PreviousWritten = null; entry.Pending = false;
                SaveReceipt(receipt);
            }
            return;
        }
        RequireClosed();
        if (entry is null)
        {
            entry = new SteamLaunchOptionsEntry { SteamRoot = target.SteamRoot, AccountId = target.AccountId, Original = current };
            receipt.Entries.Add(entry);
        }
        entry.PreviousWritten = current;
        entry.Written = command;
        entry.Pending = true;
        SaveReceipt(receipt);
        WriteConfig(target.Path, snapshot, snapshot.Document.Set(OptionPath, command));
        entry.Pending = false; entry.PreviousWritten = null;
        SaveReceipt(receipt);
    }

    public void Restore()
    {
        using var operation = AcquireLock();
        RequireClosed();
        var receipt = ReadReceipt();
        // Check every managed account before restoring any account.
        var targets = receipt.Entries.Select(entry =>
        {
            string path = LocalConfig(entry.SteamRoot, entry.AccountId);
            var snapshot = Snapshot.Read(path);
            string? current = snapshot.Document.Read(OptionPath);
            if (current != entry.Original && !OwnedValue(entry, current))
                throw new SteamIntegrationException("launch_options_changed");
            return (Entry: entry, Path: path, Snapshot: snapshot, Current: current);
        }).ToArray();
        foreach (var target in targets)
        {
            if (target.Current != target.Entry.Original)
                WriteConfig(target.Path, target.Snapshot, target.Snapshot.Document.Set(OptionPath, target.Entry.Original));
            receipt.Entries.Remove(target.Entry);
            SaveReceipt(receipt);
        }
    }

    private static bool OwnedValue(SteamLaunchOptionsEntry entry, string? current) =>
        current == entry.Written || entry.Pending && current == entry.PreviousWritten;

    private static SteamLaunchOptionsEntry? ValidateChange(SteamLaunchOptionsReceipt receipt, Target target, string? current, string command)
    {
        var entry = receipt.Entries.SingleOrDefault(e => e.AccountId == target.AccountId && SteamIntegrationService.SamePath(e.SteamRoot, target.SteamRoot));
        if (entry is not null && current != command && !OwnedValue(entry, current))
            throw new SteamIntegrationException("launch_options_changed");
        return entry;
    }

    private Target SelectTarget(string executable)
    {
        string root = ValidateSteamRoot(executable);
        if (steamIsRunning())
        {
            uint? active = activeAccountId();
            if (active is null or 0) throw new SteamIntegrationException("steam_account_required");
            string activePath = LocalConfig(root, active.Value);
            if (!File.Exists(activePath)) throw new SteamIntegrationException("steam_account_required");
            return new Target(root, active.Value, activePath);
        }
        string loginPath = Path.Combine(root, "config", "loginusers.vdf");
        RequireNoReparsePoint(loginPath);
        if (File.Exists(loginPath))
        {
            var users = Snapshot.Read(loginPath).Document.Root.Unique("users")?.Object;
            if (users is null) throw new SteamIntegrationException("invalid_steam_config");
            var recent = new List<uint>();
            var seen = new HashSet<uint>();
            var timestamps = new List<(uint Account, ulong Timestamp)>();
            foreach (var user in users.Entries)
            {
                if (!ulong.TryParse(user.Key.Value, NumberStyles.None, CultureInfo.InvariantCulture, out ulong steamId) ||
                    steamId <= 76561197960265728UL || steamId - 76561197960265728UL > uint.MaxValue || user.Object is null)
                    throw new SteamIntegrationException("invalid_steam_config");
                uint account = (uint)(steamId - 76561197960265728UL);
                if (!seen.Add(account)) throw new SteamIntegrationException("invalid_steam_config");
                var mostRecent = user.Object.Unique("MostRecent");
                if (mostRecent?.Object is not null) throw new SteamIntegrationException("invalid_steam_config");
                if (mostRecent?.Value?.Value == "1") recent.Add(account);
                var timestamp = user.Object.Unique("Timestamp");
                if (timestamp?.Object is not null) throw new SteamIntegrationException("invalid_steam_config");
                if (ulong.TryParse(timestamp?.Value?.Value, NumberStyles.None, CultureInfo.InvariantCulture, out ulong seconds) && seconds is > 0 and <= 253402300799UL)
                    timestamps.Add((account, seconds));
            }
            if (recent.Count > 1) throw new SteamIntegrationException("steam_account_required");
            if (recent.Count == 1)
            {
                string path = LocalConfig(root, recent[0]);
                if (!File.Exists(path)) throw new SteamIntegrationException("steam_account_required");
                return new Target(root, recent[0], path);
            }
            // Some Steam clients omit MostRecent and only retain login times.
            // Require a valid timestamp for every login record and a unique
            // newest record; orphan userdata directories do not select a user.
            if (seen.Count > 0 && timestamps.Count == seen.Count)
            {
                ulong newest = timestamps.Max(t => t.Timestamp);
                var candidates = timestamps.Where(t => t.Timestamp == newest).ToArray();
                if (candidates.Length != 1) throw new SteamIntegrationException("steam_account_required");
                string path = LocalConfig(root, candidates[0].Account);
                if (!File.Exists(path)) throw new SteamIntegrationException("steam_account_required");
                return new Target(root, candidates[0].Account, path);
            }
            if (seen.Count > 1) throw new SteamIntegrationException("steam_account_required");
        }
        string userdata = Path.Combine(root, "userdata");
        RequireNoReparsePoint(userdata);
        var localAccounts = Directory.Exists(userdata) ? Directory.EnumerateDirectories(userdata)
            .Select(path => uint.TryParse(Path.GetFileName(path), NumberStyles.None, CultureInfo.InvariantCulture, out uint id) ? id : 0)
            .Where(id => id != 0 && File.Exists(LocalConfig(root, id))).ToArray() : [];
        if (localAccounts.Length != 1) throw new SteamIntegrationException("steam_account_required");
        return new Target(root, localAccounts[0], LocalConfig(root, localAccounts[0]));
    }

    private static string ValidateSteamRoot(string executable)
    {
        executable = Path.GetFullPath(executable);
        RequireNoReparsePoint(executable);
        if (!Path.GetFileName(executable).Equals("steam.exe", StringComparison.OrdinalIgnoreCase) || !File.Exists(executable))
            throw new SteamIntegrationException("invalid_steam_path");
        return Path.GetDirectoryName(executable)!;
    }

    private static string LocalConfig(string root, uint accountId)
    {
        string path = Path.Combine(root, "userdata", accountId.ToString(CultureInfo.InvariantCulture), "config", "localconfig.vdf");
        RequireNoReparsePoint(path);
        return path;
    }

    private static void ValidateCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command) || command.Length > 32768 || command.Any(c => c is '\0' or '\r' or '\n'))
            throw new SteamIntegrationException("invalid_launch_arguments");
    }

    private SteamLaunchOptionsReceipt ReadReceipt()
    {
        RequireNoReparsePoint(ReceiptPath);
        if (!File.Exists(ReceiptPath)) return new();
        try
        {
            if (new FileInfo(ReceiptPath).Length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_receipt");
            var receipt = JsonSerializer.Deserialize<SteamLaunchOptionsReceipt>(File.ReadAllText(ReceiptPath), JsonOptions);
            if (receipt?.Product != "BetterEndfieldNext.SteamLaunchOptions" || receipt.Entries is null || receipt.Entries.Count > 256)
                throw new SteamIntegrationException("invalid_receipt");
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in receipt.Entries)
            {
                if (entry is null || entry.AccountId == 0 || string.IsNullOrWhiteSpace(entry.SteamRoot) || !Path.IsPathFullyQualified(entry.SteamRoot) ||
                    string.IsNullOrEmpty(entry.Written) || entry.Written.Length > 32768 || entry.Original?.Length > 32768 || entry.PreviousWritten?.Length > 32768)
                    throw new SteamIntegrationException("invalid_receipt");
                entry.SteamRoot = ValidateSteamRoot(Path.Combine(entry.SteamRoot, "steam.exe"));
                LocalConfig(entry.SteamRoot, entry.AccountId);
                if (!seen.Add(entry.SteamRoot.TrimEnd('\\', '/') + "|" + entry.AccountId.ToString(CultureInfo.InvariantCulture)))
                    throw new SteamIntegrationException("invalid_receipt");
            }
            return receipt;
        }
        catch (JsonException) { throw new SteamIntegrationException("invalid_receipt"); }
    }

    private void SaveReceipt(SteamLaunchOptionsReceipt receipt)
    {
        RequireNoReparsePoint(ReceiptPath);
        AtomicWrite(ReceiptPath, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(receipt, JsonOptions)));
    }

    private FileStream AcquireLock()
    {
        RequireNoReparsePoint(_state);
        Directory.CreateDirectory(_state);
        string path = Path.Combine(_state, "launch-options.lock");
        RequireNoReparsePoint(path);
        try { return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); }
        catch (IOException exception) when ((exception.HResult & 0xffff) is 32 or 33)
        { throw new SteamIntegrationException("operation_busy"); }
    }

    private void RequireClosed()
    {
        if (steamIsRunning()) throw new SteamIntegrationException("launch_options_pending");
    }

    private void WriteConfig(string path, Snapshot original, string updated)
    {
        RequireClosed();
        RequireNoReparsePoint(path);
        // Check under a read handle, then release it: Windows cannot replace a
        // file while this write-denying handle remains open. Recheck immediately
        // before the atomic rename as well, with Steam still required closed.
        using (var held = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            if (held.Length != original.Bytes.Length) throw new SteamIntegrationException("steam_config_changed");
            var latest = new byte[original.Bytes.Length];
            held.ReadExactly(latest);
            if (!latest.AsSpan().SequenceEqual(original.Bytes)) throw new SteamIntegrationException("steam_config_changed");
        }
        byte[] output = original.Encode(updated);
        string temporary = path + ".be-" + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllBytes(temporary, output);
            RequireClosed();
            RequireNoReparsePoint(path);
            if (!File.ReadAllBytes(path).AsSpan().SequenceEqual(original.Bytes)) throw new SteamIntegrationException("steam_config_changed");
            File.Move(temporary, path, true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    private static void AtomicWrite(string path, byte[] bytes)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string temporary = path + ".be-" + Guid.NewGuid().ToString("N") + ".tmp";
        try { File.WriteAllBytes(temporary, bytes); File.Move(temporary, path, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    private static void RequireNoReparsePoint(string path)
    {
        for (string? current = Path.GetFullPath(path); current is not null; current = Path.GetDirectoryName(current))
            if ((File.Exists(current) || Directory.Exists(current)) && File.GetAttributes(current).HasFlag(FileAttributes.ReparsePoint))
                throw new SteamIntegrationException("reparse_point");
    }

    private sealed record Snapshot(byte[] Bytes, Encoding Encoding, int PreambleLength, VdfDocument Document)
    {
        public static Snapshot Read(string path)
        {
            RequireNoReparsePoint(path);
            if (!File.Exists(path) || new FileInfo(path).Length > 16 * 1024 * 1024) throw new SteamIntegrationException("invalid_steam_config");
            byte[] bytes = File.ReadAllBytes(path);
            Encoding encoding = new UTF8Encoding(false, true);
            int preamble = 0;
            if (bytes.AsSpan().StartsWith(new byte[] { 0xef, 0xbb, 0xbf })) preamble = 3;
            else if (bytes.AsSpan().StartsWith(new byte[] { 0xff, 0xfe })) { encoding = new UnicodeEncoding(false, false, true); preamble = 2; }
            else if (bytes.AsSpan().StartsWith(new byte[] { 0xfe, 0xff })) { encoding = new UnicodeEncoding(true, false, true); preamble = 2; }
            try { return new Snapshot(bytes, encoding, preamble, new VdfDocument(encoding.GetString(bytes, preamble, bytes.Length - preamble))); }
            catch (DecoderFallbackException) { throw new SteamIntegrationException("invalid_steam_config"); }
        }
        public byte[] Encode(string text) => Bytes.AsSpan(0, PreambleLength).ToArray().Concat(Encoding.GetBytes(text)).ToArray();
    }

    // A small span parser keeps comments, whitespace, key casing, ordering,
    // and all bytes outside the edited LaunchOptions entry intact.
    private sealed record Token(string Value, int Start, int End, bool Brace = false);
    private sealed record VdfEntry(Token Key, Token? Value, VdfNode? Object);
    private sealed class VdfNode(int close)
    {
        public int Close { get; set; } = close;
        public List<VdfEntry> Entries { get; } = [];
        public VdfEntry? Unique(string key)
        {
            var matches = Entries.Where(e => e.Key.Value.Equals(key, StringComparison.OrdinalIgnoreCase)).Take(2).ToArray();
            if (matches.Length > 1) throw new SteamIntegrationException("invalid_steam_config");
            return matches.FirstOrDefault();
        }
    }

    private sealed class VdfDocument
    {
        private readonly string _text;
        private int _position;
        public VdfNode Root { get; }
        public VdfDocument(string text) { _text = text; Root = Parse(false, 0); }
        private static SteamIntegrationException Invalid() => new("invalid_steam_config");
        private Token? Next()
        {
            while (_position < _text.Length)
            {
                if (char.IsWhiteSpace(_text[_position])) { _position++; continue; }
                if (_position + 1 < _text.Length && _text[_position] == '/')
                {
                    if (_text[_position + 1] == '/') { while (_position < _text.Length && _text[_position] != '\n') _position++; continue; }
                    if (_text[_position + 1] == '*')
                    {
                        int end = _text.IndexOf("*/", _position + 2, StringComparison.Ordinal);
                        if (end < 0) throw Invalid();
                        _position = end + 2; continue;
                    }
                }
                break;
            }
            if (_position == _text.Length) return null;
            int start = _position;
            char first = _text[_position++];
            if (first is '{' or '}') return new Token(first.ToString(), start, _position, true);
            var value = new StringBuilder();
            if (first != '"')
            {
                value.Append(first);
                while (_position < _text.Length && !char.IsWhiteSpace(_text[_position]) && _text[_position] is not ('{' or '}')) value.Append(_text[_position++]);
                return new Token(value.ToString(), start, _position);
            }
            while (_position < _text.Length)
            {
                char next = _text[_position++];
                if (next == '"') return new Token(value.ToString(), start, _position);
                if (next == '\\')
                {
                    if (_position == _text.Length) throw Invalid();
                    char escaped = _text[_position++];
                    if (escaped is '\\' or '"') value.Append(escaped);
                    else if (escaped == 'n') value.Append('\n');
                    else if (escaped == 'r') value.Append('\r');
                    else if (escaped == 't') value.Append('\t');
                    else { value.Append('\\'); value.Append(escaped); }
                }
                else value.Append(next);
            }
            throw Invalid();
        }
        private VdfNode Parse(bool nested, int depth)
        {
            if (depth > 64) throw Invalid();
            var node = new VdfNode(_text.Length);
            while (Next() is Token key)
            {
                if (key.Brace)
                {
                    if (!nested || key.Value != "}") throw Invalid();
                    node.Close = key.Start; return node;
                }
                var value = Next() ?? throw Invalid();
                if (value.Brace)
                {
                    if (value.Value != "{") throw Invalid();
                    node.Entries.Add(new VdfEntry(key, null, Parse(true, depth + 1)));
                }
                else node.Entries.Add(new VdfEntry(key, value, null));
            }
            if (nested) throw Invalid();
            return node;
        }
        public string? Read(string[] path)
        {
            VdfNode node = Root;
            for (int i = 0; i < path.Length; i++)
            {
                var entry = node.Unique(path[i]);
                if (entry is null) return null;
                if (i == path.Length - 1) return entry.Value?.Value ?? throw Invalid();
                node = entry.Object ?? throw Invalid();
            }
            throw Invalid();
        }
        private static string Quote(string text) => "\"" + text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "\"";
        public string Set(string[] path, string? value)
        {
            VdfNode node = Root;
            string newline = _text.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : "\n";
            for (int i = 0; i < path.Length; i++)
            {
                var entry = node.Unique(path[i]);
                if (entry is null)
                {
                    if (value is null) return _text;
                    var addition = new StringBuilder(newline);
                    for (int k = i; k < path.Length - 1; k++)
                        addition.Append(new string('\t', k)).Append(Quote(path[k])).Append(newline).Append(new string('\t', k)).Append('{').Append(newline);
                    addition.Append(new string('\t', path.Length - 1)).Append(Quote(path[^1])).Append('\t').Append(Quote(value)).Append(newline);
                    for (int k = path.Length - 2; k >= i; k--) addition.Append(new string('\t', k)).Append('}').Append(newline);
                    addition.Append(new string('\t', Math.Max(0, i - 1)));
                    return _text.Insert(node.Close, addition.ToString());
                }
                if (i == path.Length - 1)
                {
                    var old = entry.Value ?? throw Invalid();
                    int start = value is null ? entry.Key.Start : old.Start;
                    return _text[..start] + (value is null ? "" : Quote(value)) + _text[old.End..];
                }
                node = entry.Object ?? throw Invalid();
            }
            throw Invalid();
        }
    }
}
