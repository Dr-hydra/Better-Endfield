using System.Security.Cryptography;
using System.Text.Json;

namespace BetterEndfield.UI.Services;

internal enum XInputDeploymentState
{
    Unavailable,
    NotInstalled,
    Installed,
    UpdateAvailable,
    Conflict
}

internal sealed record XInputDeploymentStatus(
    XInputDeploymentState State,
    string Message,
    bool CanInstall,
    bool CanUninstall);

internal static class XInputDeploymentService
{
    public const string ProxyFileName = "xinput1_4.dll";
    private const string ManifestFileName = "BetterEndfield.xinput.install.json";
    private const string ProductId = "BetterEndfield.XInputProxy";
    private static readonly string[] RuntimeFiles =
    [
        "BetterEndfield-xinput1_4-proxy.loaded",
        "BetterEndfield-xinput1_4-host.status"
    ];

    private sealed record DeploymentManifest(
        string Product,
        string ProxyFile,
        string Sha256,
        string InstallRoot,
        DateTimeOffset InstalledUtc);

    private sealed record DeploymentPaths(
        string Source,
        string Target,
        string Manifest,
        string GameDirectory,
        string InstallRoot);

    public static async Task<XInputDeploymentStatus> InspectAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths;
        try
        {
            paths = ResolvePaths(gameExecutablePath, injectorPath);
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or FileNotFoundException)
        {
            return new XInputDeploymentStatus(
                XInputDeploymentState.Unavailable,
                exception.Message,
                false,
                false);
        }

        try
        {
            bool isZh = LocalizationService.Instance.IsChinese;
            if (!File.Exists(paths.Target))
            {
                return new XInputDeploymentStatus(
                    XInputDeploymentState.NotInstalled,
                    isZh ? "尚未向游戏目录安装 XInput 自启动代理。" : "XInput auto-load proxy is not installed in game directory.",
                    true,
                    File.Exists(paths.Manifest));
            }

            string sourceHash = await ComputeSha256Async(paths.Source);
            string targetHash = await ComputeSha256Async(paths.Target);
            if (sourceHash.Equals(targetHash, StringComparison.OrdinalIgnoreCase))
            {
                return new XInputDeploymentStatus(
                    XInputDeploymentState.Installed,
                    isZh ? "XInput 自启动代理已安装，版本与当前软件一致。" : "XInput proxy installed; version matches current launcher.",
                    false,
                    true);
            }

            DeploymentManifest? manifest = await ReadManifestAsync(paths.Manifest);
            if (IsOwnedManifest(manifest, targetHash))
            {
                return new XInputDeploymentStatus(
                    XInputDeploymentState.UpdateAvailable,
                    isZh ? "游戏目录中是 Better Endfield 安装的旧版 XInput 代理，可更新或卸载。"
                         : "Outdated XInput proxy installed by Better Endfield found; can be updated or uninstalled.",
                    true,
                    true);
            }

            return new XInputDeploymentStatus(
                XInputDeploymentState.Conflict,
                isZh ? "游戏目录已有未知 xinput1_4.dll。为避免破坏其他加载器，该文件不会被覆盖或删除。"
                     : "Unknown xinput1_4.dll found in game directory. Will not overwrite or remove it.",
                false,
                false);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            bool isZh = LocalizationService.Instance.IsChinese;
            return new XInputDeploymentStatus(
                XInputDeploymentState.Unavailable,
                (isZh ? "无法检查 XInput 状态：" : "Failed to inspect XInput status: ") + exception.Message,
                false,
                false);
        }
    }

    public static async Task<XInputDeploymentStatus> InstallAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths = ResolvePaths(gameExecutablePath, injectorPath);
        XInputDeploymentStatus status = await InspectAsync(gameExecutablePath, injectorPath);
        if (status.State == XInputDeploymentState.Conflict)
        {
            throw new IOException(status.Message);
        }
        if (status.State == XInputDeploymentState.Unavailable)
        {
            throw new IOException(status.Message);
        }
        if (status.State == XInputDeploymentState.Installed)
        {
            await WriteManifestAsync(paths, await ComputeSha256Async(paths.Source));
            return status;
        }

        string temporary = paths.Target + ".betterendfield.tmp";
        try
        {
            File.Copy(paths.Source, temporary, overwrite: true);
            string sourceHash = await ComputeSha256Async(paths.Source);
            string temporaryHash = await ComputeSha256Async(temporary);
            if (!sourceHash.Equals(temporaryHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException("XInput 代理复制后的校验值不一致。");
            }
            File.Move(temporary, paths.Target, overwrite: true);
            await WriteManifestAsync(paths, sourceHash);
        }
        finally
        {
            TryDelete(temporary);
        }

        return new XInputDeploymentStatus(
            XInputDeploymentState.Installed,
            "XInput 自启动代理已安装到游戏目录。",
            false,
            true);
    }

    public static async Task UninstallAsync(
        string gameExecutablePath,
        string injectorPath)
    {
        DeploymentPaths paths = ResolvePaths(gameExecutablePath, injectorPath);
        if (File.Exists(paths.Target))
        {
            string targetHash = await ComputeSha256Async(paths.Target);
            string sourceHash = await ComputeSha256Async(paths.Source);
            DeploymentManifest? manifest = await ReadManifestAsync(paths.Manifest);
            bool owned = sourceHash.Equals(targetHash, StringComparison.OrdinalIgnoreCase) ||
                IsOwnedManifest(manifest, targetHash);
            if (!owned)
            {
                throw new IOException(
                    "xinput1_4.dll 不是由当前 Better Endfield 安装，已拒绝删除。");
            }
            File.Delete(paths.Target);
        }

        TryDelete(paths.Manifest);
        TryDelete(paths.Target + ".betterendfield.tmp");
        foreach (string file in RuntimeFiles)
        {
            TryDelete(Path.Combine(paths.GameDirectory, file));
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
                InvalidOperationException or JsonException)
        {
        }
    }

    private static DeploymentPaths ResolvePaths(
        string gameExecutablePath,
        string injectorPath)
    {
        if (!RuntimePathDiscoveryService.IsGameExecutable(gameExecutablePath))
        {
            throw new FileNotFoundException("未找到有效的 Endfield.exe。", gameExecutablePath);
        }
        string installRoot = ConfigurationService.ResolveInstallRoot(
            injectorPath,
            "xinput");
        string source = Path.Combine(installRoot, "payloads", ProxyFileName);
        if (!File.Exists(source))
        {
            throw new FileNotFoundException("XInput 代理未随当前软件包提供。", source);
        }
        string gameDirectory = Path.GetDirectoryName(Path.GetFullPath(gameExecutablePath)) ??
            throw new InvalidOperationException("游戏目录无效。");
        return new DeploymentPaths(
            source,
            Path.Combine(gameDirectory, ProxyFileName),
            Path.Combine(gameDirectory, ManifestFileName),
            gameDirectory,
            installRoot);
    }

    private static async Task WriteManifestAsync(DeploymentPaths paths, string sha256)
    {
        var manifest = new DeploymentManifest(
            ProductId,
            ProxyFileName,
            sha256,
            paths.InstallRoot,
            DateTimeOffset.UtcNow);
        string temporary = paths.Manifest + ".tmp";
        try
        {
            await File.WriteAllTextAsync(
                temporary,
                JsonSerializer.Serialize(manifest, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            File.Move(temporary, paths.Manifest, overwrite: true);
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
            return await JsonSerializer.DeserializeAsync<DeploymentManifest>(stream);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static bool IsOwnedManifest(DeploymentManifest? manifest, string targetHash) =>
        manifest is not null &&
        manifest.Product.Equals(ProductId, StringComparison.Ordinal) &&
        manifest.ProxyFile.Equals(ProxyFileName, StringComparison.OrdinalIgnoreCase) &&
        manifest.Sha256.Equals(targetHash, StringComparison.OrdinalIgnoreCase);

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
