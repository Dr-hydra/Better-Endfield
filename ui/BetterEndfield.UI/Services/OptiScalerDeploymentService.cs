using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;

namespace BetterEndfield.UI.Services;

internal enum OptiScalerDeploymentState
{
    /// <summary>缺少客户端路径、组件包或锁定清单，无法判断状态。</summary>
    Unavailable,
    NotInstalled,
    Installed,
    UpdateAvailable,
    /// <summary>客户端目录已有同名文件且不属于本软件，拒绝接管。</summary>
    Conflict
}

internal sealed record OptiScalerDeploymentStatus(
    OptiScalerDeploymentState State,
    string Message,
    bool CanInstall,
    bool CanUninstall,
    string? Version = null);

/// <summary>
/// 把 OptiScaler 部署到客户端目录。所有权模型与 <see cref="XInputDeploymentService"/>
/// 一致：SHA256 校验、JSON 清单、拒绝覆盖或删除非本软件安装的文件；区别是本功能
/// 为多文件部署，任一目标冲突即整体拒绝，不做部分部署。
///
/// 组件不随发布包分发。文件清单与哈希由 manifests/shared/optiscaler.lock.json 锁定，
/// 该文件在构建时内嵌进程序集，实际组件从 payloads/optiscaler/ 读取。锁定版本以外
/// 的文件一律不部署。
/// 背景与实测依据见 docs/DISPLAY_PIPELINE.md。
/// </summary>
internal static class OptiScalerDeploymentService
{
    private const string ProductId = "BetterEndfield.DisplayPipeline";
    private const string ManifestFileName = "BetterEndfield.display.install.json";
    private const string PayloadDirectoryName = "optiscaler";
    private const string LockResourceName =
        "BetterEndfield.UI.Assets.shared.optiscaler.lock.json";

    /// <summary>
    /// OptiScaler 主体的目标名。UnityPlayer.dll 静态导入 dxgi.dll，客户端目录下的
    /// 同名文件会在 UnityPlayer 加载时随即进入进程，早于图形设备创建。
    /// </summary>
    public const string PrimaryProxyName = "dxgi.dll";

    /// <summary>由生成器写出、不参与锁定哈希校验的受管配置文件。</summary>
    public const string ConfigFileName = "OptiScaler.ini";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    private sealed record LockedFile(string Source, string Target, string Sha256);

    private sealed record LockFile(string Version, LockedFile[] Files, string? Notes);

    private sealed record DeployedFile(string Name, string Sha256);

    private sealed record DeploymentManifest(
        string Product,
        string OptiScalerVersion,
        DeployedFile[] Files,
        string[] PreexistingFiles,
        string InstallRoot,
        DateTimeOffset InstalledUtc);

    private sealed record DeploymentPaths(
        string PayloadDirectory,
        string GameDirectory,
        string ManifestPath,
        string InstallRoot,
        LockFile Lock);

    public static async Task<OptiScalerDeploymentStatus> InspectAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths;
        try
        {
            paths = await ResolvePathsAsync(gameExecutablePath, injectorPath);
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or FileNotFoundException or
                InvalidDataException or JsonException)
        {
            return new OptiScalerDeploymentStatus(
                OptiScalerDeploymentState.Unavailable,
                exception.Message,
                false,
                false);
        }

        try
        {
            DeploymentManifest? manifest = await ReadManifestAsync(paths.ManifestPath);
            var missing = new List<string>();
            var foreign = new List<string>();
            var stale = new List<string>();

            foreach (LockedFile file in paths.Lock.Files)
            {
                string target = Path.Combine(paths.GameDirectory, file.Target);
                if (!File.Exists(target))
                {
                    missing.Add(file.Target);
                    continue;
                }
                if (file.Target.Equals(ConfigFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // 配置由生成器改写，内容必然偏离锁定哈希；存在即视为就绪。
                    // 所有权仍由清单记录，卸载时据此判断是否保留用户的手工改动。
                    continue;
                }
                string targetHash = await ComputeSha256Async(target);
                if (targetHash.Equals(file.Sha256, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (IsOwnedFile(manifest, file.Target, targetHash))
                {
                    stale.Add(file.Target);
                }
                else
                {
                    foreign.Add(file.Target);
                }
            }

            bool isZh = LocalizationService.Instance.IsChinese;
            if (foreign.Count > 0)
            {
                return new OptiScalerDeploymentStatus(
                    OptiScalerDeploymentState.Conflict,
                    isZh
                        ? "客户端目录已有非本软件安装的同名文件（" + string.Join("、", foreign) + "）。为避免破坏其他加载器或客户端自带文件，不会覆盖或删除它们。"
                        : "Conflicting foreign files detected in game directory (" + string.Join(", ", foreign) + "). Will not overwrite or remove them.",
                    false,
                    false,
                    manifest?.OptiScalerVersion);
            }

            bool anyOwned = manifest is not null;
            if (missing.Count == paths.Lock.Files.Length)
            {
                return new OptiScalerDeploymentStatus(
                    OptiScalerDeploymentState.NotInstalled,
                    isZh ? "尚未向客户端目录部署显示增强组件。" : "Display enhancement components are not yet deployed to game directory.",
                    true,
                    anyOwned);
            }
            if (missing.Count > 0 || stale.Count > 0)
            {
                return new OptiScalerDeploymentStatus(
                    OptiScalerDeploymentState.UpdateAvailable,
                    isZh
                        ? "客户端目录中的显示增强组件不完整或版本较旧，可更新或卸载。"
                        : "Display enhancement components in game directory are incomplete or outdated; can be updated or uninstalled.",
                    true,
                    true,
                    manifest?.OptiScalerVersion);
            }

            return new OptiScalerDeploymentStatus(
                OptiScalerDeploymentState.Installed,
                isZh ? $"显示增强组件已部署，版本 {paths.Lock.Version}。" : $"Display enhancement components deployed, version {paths.Lock.Version}.",
                false,
                true,
                paths.Lock.Version);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            bool isZh = LocalizationService.Instance.IsChinese;
            return new OptiScalerDeploymentStatus(
                OptiScalerDeploymentState.Unavailable,
                (isZh ? "无法检查显示增强状态：" : "Failed to inspect display enhancement: ") + exception.Message,
                false,
                false);
        }
    }

    public static async Task<OptiScalerDeploymentStatus> InstallAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths = await ResolvePathsAsync(gameExecutablePath, injectorPath);
        OptiScalerDeploymentStatus status =
            await InspectAsync(gameExecutablePath, injectorPath);
        if (status.State is OptiScalerDeploymentState.Conflict
            or OptiScalerDeploymentState.Unavailable)
        {
            throw new IOException(status.Message);
        }

        // 部署前采集快照：记录目标名中已经存在的文件，卸载时永不删除这些条目。
        string[] preexisting = paths.Lock.Files
            .Select(file => file.Target)
            .Where(target => File.Exists(Path.Combine(paths.GameDirectory, target)))
            .ToArray();

        var staged = new List<(string Temporary, string Target, string Name, string Sha256)>();
        var committed = new List<(string Target, string Backup)>();
        try
        {
            foreach (LockedFile file in paths.Lock.Files)
            {
                string source = Path.Combine(paths.PayloadDirectory, file.Source);
                if (!File.Exists(source))
                {
                    throw new FileNotFoundException(
                        $"组件包缺少 {file.Source}。", source);
                }
                string sourceHash = await ComputeSha256Async(source);
                if (!sourceHash.Equals(file.Sha256, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException(
                        $"{file.Source} 的校验值与锁定清单不符，已中止部署。");
                }

                string target = Path.Combine(paths.GameDirectory, file.Target);
                Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                string temporary = target + ".betterendfield.tmp";
                File.Copy(source, temporary, overwrite: true);
                string temporaryHash = await ComputeSha256Async(temporary);
                if (!temporaryHash.Equals(sourceHash, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException(
                        $"{file.Target} 复制后的校验值不一致。");
                }
                staged.Add((temporary, target, file.Target, sourceHash));
            }

            foreach ((string temporary, string target, _, _) in staged)
            {
                string? backup = null;
                if (File.Exists(target))
                {
                    backup = target + ".betterendfield.bak";
                    File.Move(target, backup, overwrite: true);
                }
                File.Move(temporary, target, overwrite: true);
                committed.Add((target, backup ?? string.Empty));
            }
        }
        catch
        {
            // 任一步失败都回滚到部署前状态，不留下半套组件。
            foreach ((string target, string backup) in Enumerable.Reverse(committed))
            {
                TryDelete(target);
                if (!string.IsNullOrEmpty(backup) && File.Exists(backup))
                {
                    File.Move(backup, target, overwrite: true);
                }
            }
            foreach ((string temporary, _, _, _) in staged)
            {
                TryDelete(temporary);
            }
            throw;
        }

        foreach ((_, string backup) in committed)
        {
            if (!string.IsNullOrEmpty(backup))
            {
                TryDelete(backup);
            }
        }

        await WriteManifestAsync(
            paths,
            staged.Select(entry => new DeployedFile(entry.Name, entry.Sha256)).ToArray(),
            preexisting);

        return new OptiScalerDeploymentStatus(
            OptiScalerDeploymentState.Installed,
            $"显示增强组件已部署到客户端目录，版本 {paths.Lock.Version}。",
            false,
            true,
            paths.Lock.Version);
    }

    public static async Task UninstallAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths = await ResolvePathsAsync(gameExecutablePath, injectorPath);
        DeploymentManifest? manifest = await ReadManifestAsync(paths.ManifestPath);
        var retained = new List<string>();

        IEnumerable<string> candidates = (manifest?.Files.Select(file => file.Name) ??
            paths.Lock.Files.Select(file => file.Target))
            .Concat([ConfigFileName])
            .Distinct(StringComparer.OrdinalIgnoreCase);

        foreach (string name in candidates)
        {
            if (manifest is not null &&
                manifest.PreexistingFiles.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                retained.Add(name);
                continue;
            }
            string target = Path.Combine(paths.GameDirectory, name);
            if (!File.Exists(target))
            {
                continue;
            }
            string targetHash = await ComputeSha256Async(target);
            if (!IsOwnedFile(manifest, name, targetHash))
            {
                // 客户端更新替换过同名文件，或用户手工改过配置：留在原地。
                retained.Add(name);
                continue;
            }
            File.Delete(target);
            TryRemoveEmptyDirectory(paths.GameDirectory, name);
        }

        TryDelete(paths.ManifestPath);
        foreach (LockedFile file in paths.Lock.Files)
        {
            TryDelete(Path.Combine(paths.GameDirectory, file.Target + ".betterendfield.tmp"));
            TryDelete(Path.Combine(paths.GameDirectory, file.Target + ".betterendfield.bak"));
        }

        if (retained.Count > 0)
        {
            throw new IOException(
                "以下文件不是由当前 Better Endfield 安装或已被改动，已保留未删除：" +
                string.Join("、", retained));
        }
    }

    public static async Task TryUninstallSavedAsync()
    {
        try
        {
            Models.AppSettings settings = await ConfigurationService.LoadAppSettingsAsync();
            if (RuntimePathDiscoveryService.IsGameExecutable(settings.GameExecutablePath))
            {
                await UninstallAsync(settings.GameExecutablePath, settings.InjectorPath);
            }
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidOperationException or InvalidDataException or
                FileNotFoundException or JsonException)
        {
        }
    }

    /// <summary>登记生成的 OptiScaler.ini，使其纳入所有权校验与卸载范围。</summary>
    public static async Task RegisterGeneratedConfigurationAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths = await ResolvePathsAsync(gameExecutablePath, injectorPath);
        DeploymentManifest? manifest = await ReadManifestAsync(paths.ManifestPath);
        if (manifest is null)
        {
            return;
        }
        string config = Path.Combine(paths.GameDirectory, ConfigFileName);
        if (!File.Exists(config))
        {
            return;
        }
        DeployedFile entry = new(ConfigFileName, await ComputeSha256Async(config));
        DeployedFile[] files = manifest.Files
            .Where(file => !file.Name.Equals(ConfigFileName, StringComparison.OrdinalIgnoreCase))
            .Append(entry)
            .ToArray();
        await WriteManifestRawAsync(paths.ManifestPath, manifest with { Files = files });
    }

    private static async Task<DeploymentPaths> ResolvePathsAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        if (!RuntimePathDiscoveryService.IsGameExecutable(gameExecutablePath))
        {
            throw new FileNotFoundException("未找到有效的 Endfield.exe。", gameExecutablePath);
        }
        string installRoot = ConfigurationService.ResolveInstallRoot(injectorPath);

        // 锁定清单是完整性来源，随程序集内嵌而非以散文件随安装目录分发，
        // 避免被替换后绕过哈希校验。
        LockFile? lockFile;
        await using (Stream? stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(LockResourceName))
        {
            if (stream is null)
            {
                throw new FileNotFoundException(
                    "缺少内嵌的 OptiScaler 锁定清单，无法确定要部署的文件与版本。",
                    LockResourceName);
            }
            lockFile = await JsonSerializer.DeserializeAsync<LockFile>(stream, JsonOptions);
        }
        if (lockFile is null || string.IsNullOrWhiteSpace(lockFile.Version))
        {
            throw new InvalidDataException("OptiScaler 锁定清单格式无效。");
        }
        if (lockFile.Files.Length == 0)
        {
            throw new InvalidDataException(
                "OptiScaler 锁定清单尚未固定版本与文件哈希，显示增强不可用。");
        }
        foreach (LockedFile file in lockFile.Files)
        {
            // 允许单层子目录（发布包的 D3D12_Optiscaler/ 与 Licenses/ 需要），
            // 但拒绝绝对路径与任何形式的向上穿越。
            if (!IsSafeRelativePath(file.Source) || !IsSafeRelativePath(file.Target))
            {
                throw new InvalidDataException(
                    $"锁定清单包含非法路径：{file.Source} -> {file.Target}。");
            }
        }

        string payloadDirectory = Path.Combine(installRoot, "payloads", PayloadDirectoryName);
        if (!Directory.Exists(payloadDirectory))
        {
            bool isZh = LocalizationService.Instance.IsChinese;
            throw new FileNotFoundException(
                isZh ? "组件包未随当前软件提供，请先获取 OptiScaler 组件。"
                     : "Payload package is not bundled with the current release. Please obtain OptiScaler payloads first.",
                payloadDirectory);
        }
        string gameDirectory = Path.GetDirectoryName(Path.GetFullPath(gameExecutablePath)) ??
            throw new InvalidOperationException(LocalizationService.Instance.IsChinese ? "客户端目录无效。" : "Invalid game directory.");

        return new DeploymentPaths(
            payloadDirectory,
            gameDirectory,
            Path.Combine(gameDirectory, ManifestFileName),
            installRoot,
            lockFile);
    }

    private static async Task WriteManifestAsync(
        DeploymentPaths paths,
        DeployedFile[] files,
        string[] preexisting)
    {
        var manifest = new DeploymentManifest(
            ProductId,
            paths.Lock.Version,
            files,
            preexisting,
            paths.InstallRoot,
            DateTimeOffset.UtcNow);
        await WriteManifestRawAsync(paths.ManifestPath, manifest);
    }

    private static async Task WriteManifestRawAsync(
        string manifestPath,
        DeploymentManifest manifest)
    {
        string temporary = manifestPath + ".tmp";
        try
        {
            await File.WriteAllTextAsync(
                temporary,
                JsonSerializer.Serialize(manifest, JsonOptions));
            File.Move(temporary, manifestPath, overwrite: true);
        }
        finally
        {
            TryDelete(temporary);
        }
    }

    private static async Task<DeploymentManifest?> ReadManifestAsync(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        try
        {
            await using FileStream stream = File.OpenRead(path);
            DeploymentManifest? manifest =
                await JsonSerializer.DeserializeAsync<DeploymentManifest>(stream, JsonOptions);
            return manifest?.Product.Equals(ProductId, StringComparison.Ordinal) == true
                ? manifest
                : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>
    /// 相对路径合法性：非空、非绝对、不含盘符或根、且规范化后仍位于起点之下。
    /// 直接比较规范化结果而不是过滤 ".." 字符串，避免被编码或分隔符变体绕过。
    /// </summary>
    private static bool IsSafeRelativePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path) ||
            Path.IsPathRooted(path) ||
            path.Contains(':', StringComparison.Ordinal))
        {
            return false;
        }
        string root = Path.GetFullPath("X:\\betterendfield-path-probe");
        string combined = Path.GetFullPath(Path.Combine(root, path));
        return combined.StartsWith(
            root + Path.DirectorySeparatorChar,
            StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>删除部署时创建、现已为空的子目录，不触碰客户端自有目录。</summary>
    private static void TryRemoveEmptyDirectory(string gameDirectory, string relativePath)
    {
        string? relativeDirectory = Path.GetDirectoryName(relativePath);
        if (string.IsNullOrEmpty(relativeDirectory))
        {
            return;
        }
        string directory = Path.Combine(gameDirectory, relativeDirectory);
        try
        {
            if (Directory.Exists(directory) &&
                !Directory.EnumerateFileSystemEntries(directory).Any())
            {
                Directory.Delete(directory);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    private static bool IsOwnedFile(
        DeploymentManifest? manifest,
        string name,
        string targetHash) =>
        manifest is not null &&
        manifest.Files.Any(file =>
            file.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            file.Sha256.Equals(targetHash, StringComparison.OrdinalIgnoreCase));

    private static async Task<string> ComputeSha256Async(string path)
    {
        await using FileStream stream = File.OpenRead(path);
        byte[] digest = await SHA256.HashDataAsync(stream);
        return Convert.ToHexString(digest);
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
