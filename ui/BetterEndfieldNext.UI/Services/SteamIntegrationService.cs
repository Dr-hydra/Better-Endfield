using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetterEndfieldNext.UI.Services;

internal sealed class SteamInstallReceipt
{
    [JsonPropertyName("Product")]
    public string Product { get; set; } = "BetterEndfieldNext.SteamCN";
    [JsonPropertyName("LibraryPath")]
    public string LibraryPath { get; set; } = "";
    [JsonPropertyName("Metadata")]
    public SteamMetadata Metadata { get; set; } = new();
    [JsonPropertyName("CreatedDirectories")]
    public List<string> CreatedDirectories { get; set; } = [];
    [JsonPropertyName("UpdatedUtc")]
    public DateTimeOffset UpdatedUtc { get; set; }
    [JsonPropertyName("Pending")]
    public bool Pending { get; set; }
}

internal sealed class SteamMetadataCache
{
    public SteamMetadataCache() { }
    public SteamMetadataCache(DateTimeOffset fetchedUtc, SteamMetadata metadata)
    {
        FetchedUtc = fetchedUtc;
        Metadata = metadata;
    }
    [JsonPropertyName("FetchedUtc")]
    public DateTimeOffset FetchedUtc { get; set; }
    [JsonPropertyName("Metadata")]
    public SteamMetadata Metadata { get; set; } = new();
}
internal sealed record SteamInstallPaths(string Manifest, string GameDirectory, string Placeholder);

internal sealed record SteamReplacementEntry(string Path, bool Directory, long Length, long CreatedUtc, long ModifiedUtc, FileAttributes Attributes);

internal sealed class SteamReplacementPlan
{
    public string GameDirectory { get; }
    public string Manifest { get; }
    public long FileCount { get; }
    public long TotalBytes { get; }
    internal string StateDirectory { get; }
    internal string Library { get; }
    internal string GamePath { get; }
    internal SteamMetadata Metadata { get; }
    internal string MetadataIdentity { get; }
    internal string[] Libraries { get; }
    internal SteamReplacementEntry[] Entries { get; }
    internal SteamReplacementEntry? ManifestEntry { get; }
    internal byte[]? ManifestBytes { get; }
    internal byte[]? ReceiptBytes { get; }
    internal byte[]? PendingBytes { get; }

    internal SteamReplacementPlan(string state, string library, string gamePath, SteamMetadata metadata, string[] libraries,
        SteamInstallPaths paths, SteamReplacementEntry[] entries, SteamReplacementEntry? manifest, byte[]? manifestBytes, byte[]? receiptBytes, byte[]? pendingBytes)
    {
        StateDirectory = state; Library = library; GamePath = gamePath; Metadata = metadata; MetadataIdentity = metadata.Identity(); Libraries = libraries;
        GameDirectory = paths.GameDirectory; Manifest = paths.Manifest; Entries = entries; ManifestEntry = manifest;
        ManifestBytes = manifestBytes; ReceiptBytes = receiptBytes; PendingBytes = pendingBytes;
        FileCount = entries.LongCount(e => !e.Directory) + (manifest is null ? 0 : 1);
        TotalBytes = checked(entries.Where(e => !e.Directory).Sum(e => e.Length) + (manifest?.Length ?? 0));
    }
}

internal sealed class SteamReplacementReceipt
{
    [JsonConstructor]
    public SteamReplacementReceipt() { }
    [JsonPropertyName("Product")]
    public string Product { get; set; } = "BetterEndfieldNext.SteamReplacement";
    [JsonPropertyName("LibraryPath")]
    public string LibraryPath { get; set; } = "";
    [JsonPropertyName("Metadata")]
    public SteamMetadata Metadata { get; set; } = new();
    [JsonPropertyName("Pending")]
    public bool Pending { get; set; } = true;
    [JsonPropertyName("UpdatedUtc")]
    public DateTimeOffset UpdatedUtc { get; set; }
}

internal sealed class SteamIntegrationService(string stateDirectory, HttpClient http, Func<bool> steamIsRunning,
    Func<bool>? gameIsRunning = null, string? protectedInstallDirectory = null)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    private readonly string _state = Path.GetFullPath(stateDirectory);
    private readonly string _protectedInstall = Path.GetFullPath(protectedInstallDirectory ??
        (Environment.ProcessPath is string executable ? Path.GetDirectoryName(executable) : null) ?? AppContext.BaseDirectory);
    private readonly SemaphoreSlim _writeLock = new(1, 1);
    private string ReceiptPath => Path.Combine(_state, "installed.json");
    private string CachePath => Path.Combine(_state, "metadata.json");
    private string ReplacementPath => Path.Combine(_state, "replacement.json");

    public SteamInstallReceipt? ReadReceipt()
    {
        RequireNoReparsePoint(ReceiptPath);
        if (!File.Exists(ReceiptPath)) return null;
        var receipt = JsonSerializer.Deserialize<SteamInstallReceipt>(File.ReadAllText(ReceiptPath), JsonOptions)
            ?? throw new SteamIntegrationException("invalid_receipt");
        if (receipt.Product != "BetterEndfieldNext.SteamCN" || receipt.Metadata is null || receipt.CreatedDirectories is null || string.IsNullOrWhiteSpace(receipt.LibraryPath))
            throw new SteamIntegrationException("invalid_receipt");
        receipt.Metadata.Validate();
        string gameRoot = GetPaths(receipt.LibraryPath, receipt.Metadata).GameDirectory;
        foreach (string directory in receipt.CreatedDirectories)
        {
            if (string.IsNullOrWhiteSpace(directory) || !Path.IsPathFullyQualified(directory) || !Within(gameRoot, directory))
                throw new SteamIntegrationException("invalid_receipt");
            RequireNoReparsePoint(directory);
        }
        return receipt;
    }

    public SteamMetadataCache? ReadCache() => File.Exists(CachePath)
        ? JsonSerializer.Deserialize<SteamMetadataCache>(File.ReadAllText(CachePath), JsonOptions) : null;

    public async Task<SteamMetadata> FetchLatestAsync(CancellationToken token = default)
    {
        using var response = await http.GetAsync("https://api.steamcmd.net/v1/info/" + SteamMetadata.EndfieldAppId, HttpCompletionOption.ResponseHeadersRead, token);
        response.EnsureSuccessStatusCode();
        if (response.Content.Headers.ContentLength > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_metadata");
        await using var input = await response.Content.ReadAsStreamAsync(token);
        using var bytes = new MemoryStream();
        var buffer = new byte[16384];
        int length;
        while ((length = await input.ReadAsync(buffer, token)) > 0)
        {
            if (bytes.Length + length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_metadata");
            bytes.Write(buffer, 0, length);
        }
        var metadata = SteamMetadata.Parse(Encoding.UTF8.GetString(bytes.ToArray()));
        if (metadata.MissingFields.Count == 0) await CacheAsync(metadata, token);
        return metadata;
    }

    public async Task CacheAsync(SteamMetadata metadata, CancellationToken token = default)
    {
        metadata.Validate();
        await _writeLock.WaitAsync(token);
        try { AtomicWrite(CachePath, JsonSerializer.Serialize(new SteamMetadataCache(DateTimeOffset.UtcNow, metadata), JsonOptions)); }
        finally { _writeLock.Release(); }
    }

    public static SteamInstallPaths GetPaths(string libraryPath, SteamMetadata metadata)
    {
        metadata.Validate();
        string library = Path.GetFullPath(libraryPath);
        string game = Path.Combine(library, "steamapps", "common", metadata.InstallDirectory);
        return new SteamInstallPaths(Path.Combine(library, "steamapps", "appmanifest_" + metadata.AppId + ".acf"), game,
            Path.Combine(game, SteamMetadata.ValidateRelativePath(metadata.Executable)));
    }

    private static void ValidateGamePath(string gamePath)
    {
        if (!File.Exists(gamePath) || !Path.GetFileName(gamePath).Equals("Endfield.exe", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_game_path");
    }

    public static string GenerateLaunchCommand(string helperPath, string gamePath, string arguments)
    {
        ValidateGamePath(gamePath);
        if (!GameLaunchService.AreArgumentsValid(arguments) || arguments.Contains("%command%", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_launch_arguments");
        string helper = Path.GetFullPath(helperPath);
        string name = Path.GetFileName(helper);
        if (!File.Exists(helper) || !(name.Equals("BetterEndfieldNext.Launcher.exe", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("BetterEndfieldNext.Injector.exe", StringComparison.OrdinalIgnoreCase)))
            throw new SteamIntegrationException("invalid_launcher_path");
        return "\"" + helper + "\" " + GameLaunchService.BuildArguments(gamePath, arguments) + " --steam-command %command%";
    }

    public static string GenerateAcf(SteamMetadata metadata, string? previous = null)
    {
        metadata.Validate();
        var document = previous is null ? new SteamKeyValues() : SteamKeyValues.Parse(previous);
        var state = document.Object("AppState") ?? new SteamKeyValues();
        if (previous is not null && (state.String("appid") != metadata.AppId || state.String("installdir") != metadata.InstallDirectory))
            throw new SteamIntegrationException("foreign_manifest");
        state.Set("appid", metadata.AppId);
        state.Set("name", metadata.Name);
        state.Set("universe", "1");
        state.Set("StateFlags", "4");
        state.Set("installdir", metadata.InstallDirectory);
        state.Set("buildid", metadata.BuildId);
        state.Set("SizeOnDisk", "0");
        state.Set("BytesToDownload", "0");
        state.Set("BytesDownloaded", "0");
        state.Set("BytesToStage", "0");
        state.Set("BytesStaged", "0");
        state.Set("TargetBuildID", "0");
        state.Set("UpdateResult", "0");
        state.Set("AutoUpdateBehavior", "1");
        if (state.String("LastUpdated") is null) state.Set("LastUpdated", "0");
        if (state.String("LastPlayed") is null) state.Set("LastPlayed", "0");
        var depots = new SteamKeyValues();
        foreach (var depot in metadata.Depots)
        {
            var entry = new SteamKeyValues();
            entry.Set("manifest", depot.Manifest); entry.Set("size", "0");
            depots.Set(depot.Id, entry);
        }
        state.Set("InstalledDepots", depots);
        foreach (string key in new[] { "UserConfig", "MountedConfig" })
        {
            var config = state.Object(key) ?? new SteamKeyValues();
            if (config.String("language") is null) config.Set("language", "schinese");
            state.Set(key, config);
        }
        document.Set("AppState", state);
        return document.ToString();
    }

    public async Task ApplyAsync(string libraryPath, string gamePath, SteamMetadata metadata, IEnumerable<string> knownLibraries, CancellationToken token = default)
    {
        metadata.Validate();
        ValidateGamePath(gamePath);
        await _writeLock.WaitAsync(token);
        try
        {
            using var operation = AcquireOperationLock();
            RequireSteamClosed();
            RequireNoPendingReplacement();
            string library = Path.GetFullPath(libraryPath);
            if (!Directory.Exists(Path.Combine(library, "steamapps"))) throw new SteamIntegrationException("invalid_library");
            if (!knownLibraries.Any(path => SamePath(path, library))) throw new SteamIntegrationException("unregistered_library");
            var paths = GetPaths(library, metadata);
            RequireNoReparsePoint(paths.Manifest);
            RequireNoReparsePoint(paths.Placeholder);
            if (Within(paths.GameDirectory, gamePath)) throw new SteamIntegrationException("game_directory_overlap");
            foreach (string other in knownLibraries.Where(path => !SamePath(path, library)))
                if (File.Exists(Path.Combine(other, "steamapps", "appmanifest_" + metadata.AppId + ".acf"))) throw new SteamIntegrationException("other_library_install");
            var receipt = ReadReceipt();
            string? previousReceipt = File.Exists(ReceiptPath) ? File.ReadAllText(ReceiptPath) : null;
            if (receipt is not null)
            {
                if (!SamePath(receipt.LibraryPath, library)) throw new SteamIntegrationException("library_changed");
                if (receipt.Metadata.InstallDirectory != metadata.InstallDirectory || receipt.Metadata.Executable != metadata.Executable)
                    throw new SteamIntegrationException("layout_changed");
                RequireOwned(receipt);
            }
            else if (File.Exists(paths.Manifest) || Directory.Exists(paths.GameDirectory) && Directory.EnumerateFileSystemEntries(paths.GameDirectory).Any())
                throw new SteamIntegrationException("existing_install");

            string? previous = File.Exists(paths.Manifest) ? File.ReadAllText(paths.Manifest) : null;
            string acf = GenerateAcf(metadata, previous);
            if (previous is not null) Backup(previous);
            var created = receipt?.CreatedDirectories.ToList() ?? [];
            var missing = new Stack<string>();
            for (string? directory = Path.GetDirectoryName(paths.Placeholder); directory is not null && !Directory.Exists(directory); directory = Path.GetDirectoryName(directory))
                missing.Push(directory);
            bool createdPlaceholder = !File.Exists(paths.Placeholder);
            RequireSteamClosed();
            created.AddRange(missing.Where(directory => Within(paths.GameDirectory, directory)));
            var nextReceipt = new SteamInstallReceipt
            {
                LibraryPath = library, Metadata = metadata, CreatedDirectories = created.Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
                UpdatedUtc = DateTimeOffset.UtcNow, Pending = true
            };
            // Journal ownership before touching Steam. A client-start race or
            // process interruption remains recoverable on the next closed run.
            AtomicWrite(ReceiptPath, JsonSerializer.Serialize(nextReceipt, JsonOptions));
            try
            {
                foreach (string directory in missing) Directory.CreateDirectory(directory);
                if (createdPlaceholder) using (File.Create(paths.Placeholder)) { }
                RequireSteamClosed();
                AtomicWrite(paths.Manifest, acf);
                nextReceipt.Pending = false;
                AtomicWrite(ReceiptPath, JsonSerializer.Serialize(nextReceipt, JsonOptions));
            }
            catch
            {
                // Do not race a client that started during the operation.
                if (!steamIsRunning())
                {
                    if (previous is not null) AtomicWrite(paths.Manifest, previous);
                    else if (File.Exists(paths.Manifest) && File.ReadAllText(paths.Manifest) == acf) File.Delete(paths.Manifest);
                    if (createdPlaceholder && File.Exists(paths.Placeholder) && new FileInfo(paths.Placeholder).Length == 0) File.Delete(paths.Placeholder);
                    RemoveEmptyDirectories(paths.GameDirectory, created.Except(receipt?.CreatedDirectories ?? []));
                    if (previousReceipt is not null) AtomicWrite(ReceiptPath, previousReceipt);
                    else File.Delete(ReceiptPath);
                }
                throw;
            }
        }
        finally { _writeLock.Release(); }
    }

    // Preparing a replacement is read-only. The returned plan is the exact
    // directory/manifest snapshot the UI must show before asking for consent.
    public SteamReplacementPlan PrepareReplacement(string libraryPath, string gamePath, SteamMetadata metadata, IEnumerable<string> knownLibraries)
    {
        RequireReplacementSafe(CancellationToken.None);
        metadata.Validate();
        var frozen = new SteamMetadata
        {
            AppId = metadata.AppId, Name = metadata.Name, InstallDirectory = metadata.InstallDirectory,
            Executable = metadata.Executable, LaunchArguments = metadata.LaunchArguments, BuildId = metadata.BuildId,
            Depots = metadata.Depots.Select(d => new SteamDepotMetadata(d.Id, d.Manifest)).ToList()
        };
        string library = Path.GetFullPath(libraryPath);
        string game = Path.GetFullPath(gamePath);
        string[] libraries = knownLibraries.Select(Path.GetFullPath).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        var paths = ValidateReplacementTargets(library, game, frozen, libraries);
        ValidateReplacementReceipt(library, frozen);
        var entries = CaptureTree(paths.GameDirectory);
        var manifest = CaptureEntry(paths.Manifest);
        if (manifest?.Directory == true) throw new SteamIntegrationException("foreign_manifest");
        byte[]? manifestBytes = ReadSmallFile(paths.Manifest);
        var plan = new SteamReplacementPlan(_state, library, game, frozen, libraries, paths, entries, manifest,
            manifestBytes, ReadSmallFile(ReceiptPath), ReadSmallFile(ReplacementPath));
        RequireReplacementSafe(CancellationToken.None);
        return plan;
    }

    public async Task ReplaceAsync(SteamReplacementPlan plan, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        await _writeLock.WaitAsync(token);
        try
        {
            using var operation = AcquireOperationLock();
            RequireReplacementSafe(token);
            if (!SamePath(plan.StateDirectory, _state) || plan.Metadata.Identity() != plan.MetadataIdentity)
                throw new SteamIntegrationException("replacement_changed");
            var paths = ValidateReplacementTargets(plan.Library, plan.GamePath, plan.Metadata, plan.Libraries);
            if (!SamePath(paths.GameDirectory, plan.GameDirectory) || !SamePath(paths.Manifest, plan.Manifest))
                throw new SteamIntegrationException("replacement_changed");
            ValidateReplacementReceipt(plan.Library, plan.Metadata);
            if (!CaptureTree(paths.GameDirectory).SequenceEqual(plan.Entries) || CaptureEntry(paths.Manifest) != plan.ManifestEntry ||
                !SameBytes(ReadSmallFile(paths.Manifest), plan.ManifestBytes) || !SameBytes(ReadSmallFile(ReceiptPath), plan.ReceiptBytes) ||
                !SameBytes(ReadSmallFile(ReplacementPath), plan.PendingBytes))
                throw new SteamIntegrationException("replacement_changed");
            RequireReplacementSafe(token);
            // Keep the small original metadata files; game data is explicitly
            // discarded by this confirmed operation and is never backed up.
            string backupId = DateTimeOffset.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + "-" + Guid.NewGuid().ToString("N");
            if (plan.ManifestBytes is not null) BackupReplacementBytes(backupId + "-manifest.acf", plan.ManifestBytes);
            if (plan.ReceiptBytes is not null) BackupReplacementBytes(backupId + "-installed.json", plan.ReceiptBytes);
            AtomicWrite(ReplacementPath, JsonSerializer.Serialize(new SteamReplacementReceipt
            {
                LibraryPath = plan.Library, Metadata = plan.Metadata, UpdatedUtc = DateTimeOffset.UtcNow
            }, JsonOptions));
            var pending = new SteamInstallReceipt { LibraryPath = plan.Library, Metadata = plan.Metadata, Pending = true, UpdatedUtc = DateTimeOffset.UtcNow };
            AtomicWrite(ReceiptPath, JsonSerializer.Serialize(pending, JsonOptions));
            // Delete only individually confirmed paths. An unconfirmed new
            // child makes non-recursive directory removal fail and survives.
            foreach (var entry in plan.Entries.Where(e => !e.Directory)) DeleteConfirmedEntry(plan, entry, token);
            foreach (var entry in plan.Entries.Where(e => e.Directory).OrderByDescending(e => e.Path.Length)) DeleteConfirmedEntry(plan, entry, token);
            if (plan.ManifestEntry is not null) DeleteConfirmedEntry(plan, plan.ManifestEntry, token);
            RequireReplacementSafe(token);
            ValidateReplacementTargets(plan.Library, plan.GamePath, plan.Metadata, plan.Libraries);
            if (File.Exists(paths.Manifest) || Directory.Exists(paths.Manifest) || File.Exists(paths.GameDirectory) || Directory.Exists(paths.GameDirectory))
                throw new SteamIntegrationException("replacement_changed");
            var missing = new Stack<string>();
            for (string? directory = Path.GetDirectoryName(paths.Placeholder); directory is not null && !Directory.Exists(directory); directory = Path.GetDirectoryName(directory))
                missing.Push(directory);
            pending.CreatedDirectories = missing.Where(directory => Within(paths.GameDirectory, directory)).ToList();
            AtomicWrite(ReceiptPath, JsonSerializer.Serialize(pending, JsonOptions));
            foreach (string directory in missing)
            {
                RequireReplacementSafe(token);
                RequireNoReparsePoint(directory);
                if (Directory.Exists(directory) || File.Exists(directory)) throw new SteamIntegrationException("replacement_changed");
                Directory.CreateDirectory(directory);
            }
            RequireReplacementSafe(token);
            RequireNoReparsePoint(paths.Placeholder);
            using (new FileStream(paths.Placeholder, FileMode.CreateNew, FileAccess.Write, FileShare.None)) { }
            RequireReplacementSafe(token);
            RequireOwned(pending);
            WriteNewManifest(paths.Manifest, GenerateAcf(plan.Metadata), token);
            pending.Pending = false;
            AtomicWrite(ReceiptPath, JsonSerializer.Serialize(pending, JsonOptions));
            RequireNoReparsePoint(ReplacementPath);
            File.Delete(ReplacementPath);
        }
        finally { _writeLock.Release(); }
    }

    private SteamInstallPaths ValidateReplacementTargets(string library, string gamePath, SteamMetadata metadata, IReadOnlyList<string> libraries)
    {
        metadata.Validate();
        ValidateGamePath(gamePath);
        RequireNoReparsePoint(gamePath);
        RequireNoReparsePoint(_state);
        RequireNoReparsePoint(_protectedInstall);
        if (!libraries.Any(path => SamePath(path, library))) throw new SteamIntegrationException("unregistered_library");
        if (!Directory.Exists(Path.Combine(library, "steamapps"))) throw new SteamIntegrationException("invalid_library");
        var paths = GetPaths(library, metadata);
        string common = Path.GetFullPath(Path.Combine(library, "steamapps", "common"));
        // A metadata-derived single game directory, never a library, common,
        // drive root, CN game directory, state directory, or BE installation.
        if (!SamePath(Path.GetDirectoryName(paths.GameDirectory)!, common) || SamePath(paths.GameDirectory, common) ||
            SamePath(paths.GameDirectory, library) || SamePath(paths.GameDirectory, Path.GetPathRoot(paths.GameDirectory)!))
            throw new SteamIntegrationException("invalid_library");
        string cnDirectory = Path.GetDirectoryName(Path.GetFullPath(gamePath))!;
        if (Overlaps(paths.GameDirectory, cnDirectory)) throw new SteamIntegrationException("game_directory_overlap");
        if (Overlaps(paths.GameDirectory, _state) || Overlaps(paths.GameDirectory, _protectedInstall))
            throw new SteamIntegrationException("protected_directory");
        RequireNoReparsePoint(paths.GameDirectory);
        RequireNoReparsePoint(paths.Manifest);
        RequireNoReparsePoint(paths.Placeholder);
        foreach (string other in libraries.Where(path => !SamePath(path, library)))
            if (File.Exists(Path.Combine(other, "steamapps", "appmanifest_" + metadata.AppId + ".acf")))
                throw new SteamIntegrationException("other_library_install");
        return paths;
    }

    private void ValidateReplacementReceipt(string library, SteamMetadata metadata)
    {
        var receipt = ReadReceipt();
        if (receipt is not null)
        {
            if (!SamePath(receipt.LibraryPath, library)) throw new SteamIntegrationException("library_changed");
            if (receipt.Metadata.InstallDirectory != metadata.InstallDirectory || receipt.Metadata.Executable != metadata.Executable)
                throw new SteamIntegrationException("layout_changed");
        }
        var replacement = ReadReplacementReceipt();
        if (replacement is not null && (!SamePath(replacement.LibraryPath, library) || replacement.Metadata.InstallDirectory != metadata.InstallDirectory ||
            replacement.Metadata.Executable != metadata.Executable)) throw new SteamIntegrationException("replacement_pending");
    }

    private SteamReplacementReceipt? ReadReplacementReceipt()
    {
        byte[]? bytes = ReadSmallFile(ReplacementPath);
        if (bytes is null) return null;
        try
        {
            var receipt = JsonSerializer.Deserialize<SteamReplacementReceipt>(bytes, JsonOptions);
            if (receipt?.Product != "BetterEndfieldNext.SteamReplacement" || !receipt.Pending || receipt.Metadata is null ||
                string.IsNullOrWhiteSpace(receipt.LibraryPath) || !Path.IsPathFullyQualified(receipt.LibraryPath))
                throw new SteamIntegrationException("invalid_receipt");
            receipt.Metadata.Validate();
            return receipt;
        }
        catch (JsonException) { throw new SteamIntegrationException("invalid_receipt"); }
    }

    private void RequireNoPendingReplacement()
    {
        if (ReadReplacementReceipt() is not null) throw new SteamIntegrationException("replacement_pending");
    }

    private static bool Overlaps(string left, string right) => Within(left, right) || Within(right, left);
    private static bool SameBytes(byte[]? left, byte[]? right) => left is null ? right is null : right is not null && left.AsSpan().SequenceEqual(right);
    private static byte[]? ReadSmallFile(string path)
    {
        RequireNoReparsePoint(path);
        if (Directory.Exists(path)) throw new SteamIntegrationException("invalid_receipt");
        if (!File.Exists(path)) return null;
        if (new FileInfo(path).Length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_receipt");
        byte[] bytes = File.ReadAllBytes(path);
        if (bytes.Length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_receipt");
        return bytes;
    }

    private static SteamReplacementEntry? CaptureEntry(string path)
    {
        RequireNoReparsePoint(path);
        if (!File.Exists(path) && !Directory.Exists(path)) return null;
        var info = new FileInfo(path);
        bool directory = info.Attributes.HasFlag(FileAttributes.Directory);
        return new SteamReplacementEntry(Path.GetFullPath(path), directory, directory ? 0 : info.Length,
            info.CreationTimeUtc.Ticks, info.LastWriteTimeUtc.Ticks, info.Attributes);
    }

    private static SteamReplacementEntry[] CaptureTree(string root)
    {
        var result = new List<SteamReplacementEntry>();
        var queue = new Queue<string>();
        queue.Enqueue(root);
        while (queue.TryDequeue(out string? path))
        {
            var entry = CaptureEntry(path);
            if (entry is null) { if (!SamePath(path, root)) throw new SteamIntegrationException("replacement_changed"); continue; }
            result.Add(entry);
            if (entry.Directory) foreach (string child in Directory.EnumerateFileSystemEntries(path)) queue.Enqueue(child);
        }
        return result.OrderBy(e => e.Path, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    private void DeleteConfirmedEntry(SteamReplacementPlan plan, SteamReplacementEntry entry, CancellationToken token)
    {
        RequireReplacementSafe(token);
        ValidateReplacementTargets(plan.Library, plan.GamePath, plan.Metadata, plan.Libraries);
        if (!Within(plan.GameDirectory, entry.Path) && !SamePath(plan.Manifest, entry.Path)) throw new SteamIntegrationException("replacement_changed");
        var current = CaptureEntry(entry.Path);
        if (current is null || current.Directory != entry.Directory || current.CreatedUtc != entry.CreatedUtc || current.Attributes != entry.Attributes ||
            !entry.Directory && (current.Length != entry.Length || current.ModifiedUtc != entry.ModifiedUtc))
            throw new SteamIntegrationException("replacement_changed");
        if (!entry.Directory) File.Delete(entry.Path);
        else
        {
            if (Directory.EnumerateFileSystemEntries(entry.Path).Any()) throw new SteamIntegrationException("replacement_changed");
            Directory.Delete(entry.Path, recursive: false);
        }
    }

    private void RequireReplacementSafe(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        RequireSteamClosed();
        if (gameIsRunning?.Invoke() ?? IsGameRunning()) throw new SteamIntegrationException("game_running");
    }

    private static bool IsGameRunning()
    {
        var processes = System.Diagnostics.Process.GetProcessesByName("Endfield");
        try { return processes.Length != 0; }
        finally { foreach (var process in processes) process.Dispose(); }
    }

    private void BackupReplacementBytes(string name, byte[] bytes)
    {
        string path = Path.Combine(_state, "backups", name);
        RequireNoReparsePoint(path);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using var file = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        file.Write(bytes);
    }

    private void WriteNewManifest(string path, string acf, CancellationToken token)
    {
        RequireNoReparsePoint(path);
        string temporary = path + ".be-" + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporary, acf, new UTF8Encoding(false));
            RequireReplacementSafe(token);
            RequireNoReparsePoint(path);
            File.Move(temporary, path, overwrite: false);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    public async Task RestoreAsync(CancellationToken token = default)
    {
        await _writeLock.WaitAsync(token);
        try
        {
            using var operation = AcquireOperationLock();
            RequireSteamClosed();
            RequireNoPendingReplacement();
            var receipt = ReadReceipt();
            if (receipt is null) return;
            RequireOwned(receipt);
            var paths = GetPaths(receipt.LibraryPath, receipt.Metadata);
            if (File.Exists(paths.Manifest)) Backup(File.ReadAllText(paths.Manifest));
            RequireSteamClosed();
            if (File.Exists(paths.Manifest)) File.Delete(paths.Manifest);
            if (File.Exists(paths.Placeholder)) File.Delete(paths.Placeholder);
            RemoveEmptyDirectories(paths.GameDirectory, receipt.CreatedDirectories);
            File.Delete(ReceiptPath);
        }
        finally { _writeLock.Release(); }
    }

    private void RequireSteamClosed()
    {
        if (steamIsRunning()) throw new SteamIntegrationException("steam_running");
    }

    private FileStream AcquireOperationLock()
    {
        RequireNoReparsePoint(_state);
        RequireNoReparsePoint(Path.Combine(_state, "operation.lock"));
        Directory.CreateDirectory(_state);
        try { return new FileStream(Path.Combine(_state, "operation.lock"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); }
        catch (IOException exception) when ((exception.HResult & 0xffff) is 32 or 33)
        { throw new SteamIntegrationException("operation_busy"); }
    }

    private static void RequireOwned(SteamInstallReceipt receipt)
    {
        var paths = GetPaths(receipt.LibraryPath, receipt.Metadata);
        RequireNoReparsePoint(paths.Manifest);
        RequireNoReparsePoint(paths.Placeholder);
        if (File.Exists(paths.Manifest))
        {
            SteamKeyValues? state;
            try { state = SteamKeyValues.Parse(File.ReadAllText(paths.Manifest)).Object("AppState"); }
            catch (InvalidDataException) { throw new SteamIntegrationException("foreign_manifest"); }
            if (state?.String("appid") != receipt.Metadata.AppId || state.String("installdir") != receipt.Metadata.InstallDirectory)
                throw new SteamIntegrationException("foreign_manifest");
        }
        var queue = new Queue<string>();
        if (Directory.Exists(paths.GameDirectory)) queue.Enqueue(paths.GameDirectory);
        while (queue.TryDequeue(out var directory))
        {
            foreach (string entry in Directory.EnumerateFileSystemEntries(directory))
            {
                var attributes = File.GetAttributes(entry);
                if (attributes.HasFlag(FileAttributes.ReparsePoint)) throw new SteamIntegrationException("reparse_point");
                if (attributes.HasFlag(FileAttributes.Directory)) queue.Enqueue(entry);
                else if (!(SamePath(entry, paths.Placeholder) && new FileInfo(entry).Length == 0) && !IsAppIdHint(entry, paths, receipt.Metadata.AppId))
                    throw new SteamIntegrationException("install_changed");
            }
        }
    }

    private static bool IsAppIdHint(string path, SteamInstallPaths paths, string appId)
    {
        // Steamworks app-ID hints are not BE-owned. Recognize only the expected
        // ID in the game's root or beside its executable, and never delete it
        // during ordinary restoration of BE's ACF and placeholder.
        if (!SamePath(path, Path.Combine(paths.GameDirectory, "steam_appid.txt")) &&
            !SamePath(path, Path.Combine(Path.GetDirectoryName(paths.Placeholder)!, "steam_appid.txt"))) return false;
        RequireNoReparsePoint(path);
        if (new FileInfo(path).Length > 128) return false;
        byte[] bytes = File.ReadAllBytes(path);
        if (bytes.Length > 128) return false;
        string text;
        try { text = new UTF8Encoding(false, true).GetString(bytes); }
        catch (DecoderFallbackException) { return false; }
        int start = text.Length > 0 && text[0] == '\uFEFF' ? 1 : 0;
        int end = text.Length;
        while (start < end && (char.IsWhiteSpace(text[start]) || text[start] == '\0')) start++;
        while (end > start && (char.IsWhiteSpace(text[end - 1]) || text[end - 1] == '\0')) end--;
        return text[start..end] == appId;
    }

    private void Backup(string acf) => AtomicWrite(Path.Combine(_state, "backups", DateTimeOffset.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + "-" + Guid.NewGuid().ToString("N") + ".acf"), acf);

    private static void AtomicWrite(string path, string text)
    {
        RequireNoReparsePoint(path);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string temporary = path + ".be-" + Guid.NewGuid().ToString("N") + ".tmp";
        try { File.WriteAllText(temporary, text, new UTF8Encoding(false)); File.Move(temporary, path, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    internal static bool SamePath(string left, string right) => Path.GetFullPath(left).TrimEnd('\\', '/').Equals(Path.GetFullPath(right).TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase);
    internal static bool Within(string root, string path) => SamePath(root, path) || Path.GetFullPath(path).StartsWith(Path.GetFullPath(root).TrimEnd('\\', '/') + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);

    private static void RequireNoReparsePoint(string path)
    {
        for (string? current = Path.GetFullPath(path); current is not null; current = Path.GetDirectoryName(current))
            if ((File.Exists(current) || Directory.Exists(current)) && File.GetAttributes(current).HasFlag(FileAttributes.ReparsePoint))
                throw new SteamIntegrationException("reparse_point");
    }

    private static void RemoveEmptyDirectories(string gameRoot, IEnumerable<string> directories)
    {
        foreach (string directory in directories.Distinct(StringComparer.OrdinalIgnoreCase).OrderByDescending(path => path.Length))
        {
            if (!Within(gameRoot, directory)) throw new SteamIntegrationException("invalid_receipt");
            RequireNoReparsePoint(directory);
            if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any()) Directory.Delete(directory, false);
        }
    }
}
