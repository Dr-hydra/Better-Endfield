using System.Text;
using System.Text.Json;

namespace BetterEndfieldNext.UI.Services;

internal sealed class SteamInstallReceipt
{
    public string Product { get; set; } = "BetterEndfieldNext.SteamCN";
    public string LibraryPath { get; set; } = "";
    public SteamMetadata Metadata { get; set; } = new();
    public List<string> CreatedDirectories { get; set; } = [];
    public DateTimeOffset UpdatedUtc { get; set; }
    public bool Pending { get; set; }
}

internal sealed record SteamMetadataCache(DateTimeOffset FetchedUtc, SteamMetadata Metadata);
internal sealed record SteamInstallPaths(string Manifest, string GameDirectory, string Placeholder);

internal sealed class SteamIntegrationService(string stateDirectory, HttpClient http, Func<bool> steamIsRunning)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    private readonly string _state = Path.GetFullPath(stateDirectory);
    private readonly SemaphoreSlim _writeLock = new(1, 1);
    private string ReceiptPath => Path.Combine(_state, "installed.json");
    private string CachePath => Path.Combine(_state, "metadata.json");

    public SteamInstallReceipt? ReadReceipt()
    {
        if (!File.Exists(ReceiptPath)) return null;
        var receipt = JsonSerializer.Deserialize<SteamInstallReceipt>(File.ReadAllText(ReceiptPath), JsonOptions)
            ?? throw new SteamIntegrationException("invalid_receipt");
        if (receipt.Product != "BetterEndfieldNext.SteamCN" || receipt.Metadata is null || receipt.CreatedDirectories is null || string.IsNullOrWhiteSpace(receipt.LibraryPath))
            throw new SteamIntegrationException("invalid_receipt");
        receipt.Metadata.Validate();
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

    public static string GenerateLaunchCommand(string gamePath, string arguments)
    {
        string game = Path.GetFullPath(gamePath);
        if (!File.Exists(game) || !Path.GetFileName(game).Equals("Endfield.exe", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_game_path");
        if (game.Contains('"') || arguments.Any(c => c is '\0' or '\r' or '\n') || arguments.Contains("%command%", StringComparison.OrdinalIgnoreCase))
            throw new SteamIntegrationException("invalid_launch_arguments");
        return "\"" + game + "\"" + (string.IsNullOrWhiteSpace(arguments) ? "" : " " + arguments.Trim()) + " %command%";
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
        GenerateLaunchCommand(gamePath, "");
        await _writeLock.WaitAsync(token);
        try
        {
            using var operation = AcquireOperationLock();
            RequireSteamClosed();
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

    public async Task RestoreAsync(CancellationToken token = default)
    {
        await _writeLock.WaitAsync(token);
        try
        {
            using var operation = AcquireOperationLock();
            RequireSteamClosed();
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
            var state = SteamKeyValues.Parse(File.ReadAllText(paths.Manifest)).Object("AppState");
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
                else if (!SamePath(entry, paths.Placeholder) || new FileInfo(entry).Length != 0) throw new SteamIntegrationException("install_changed");
            }
        }
    }

    private void Backup(string acf) => AtomicWrite(Path.Combine(_state, "backups", DateTimeOffset.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + "-" + Guid.NewGuid().ToString("N") + ".acf"), acf);

    private static void AtomicWrite(string path, string text)
    {
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
