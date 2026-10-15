using System.ComponentModel;
using System.Net.Http;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Pickers;

namespace BetterEndfieldNext.UI.Views;

public sealed partial class SteamIntegrationPage : UserControl
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(20) };
    private readonly SteamIntegrationService _service;
    private readonly SteamCompatibilityService _compatibility;
    private readonly SteamLaunchOptionsService _launchOptions;
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(2) };
    private readonly CancellationTokenSource _cancellation = new();
    private SteamIntegrationSettings _settings = new();
    private Func<Task> _saveSettings = () => Task.CompletedTask;
    private Func<string> _gamePath = () => "";
    private Func<string> _gameArguments = () => "";
    private Func<string> _loaderMode = () => "injector";
    private Func<Task<bool>> _prepareLaunch = () => Task.FromResult(false);
    private SteamMetadata? _metadata;
    private bool _initializing = true, _initialized, _busy, _pending, _managed, _preview, _setupPending;
    private DateTimeOffset _nextQuery, _nextApply;

    public SteamIntegrationPage() : this(Path.Combine(ConfigurationService.SettingsDirectory, "steam")) { }
    internal SteamIntegrationPage(string stateDirectory)
    {
        InitializeComponent();
        _service = new SteamIntegrationService(stateDirectory, _http, SteamEnvironment.IsRunning);
        _compatibility = new SteamCompatibilityService(Path.Combine(stateDirectory, "administrator.json"), new SteamRegistryCompatibilityStore(), SteamEnvironment.IsRunning);
        _launchOptions = new SteamLaunchOptionsService(stateDirectory, SteamEnvironment.IsRunning, SteamEnvironment.ActiveAccountId);
        _timer.Tick += async (_, _) => await AutomaticUpdateAsync();
        LocalizationService.Instance.PropertyChanged += LanguageChanged;
        Unloaded += (_, _) =>
        {
            _timer.Stop(); _cancellation.Cancel();
            LocalizationService.Instance.PropertyChanged -= LanguageChanged;
        };
        Localize();
    }

    internal void Initialize(SteamIntegrationSettings settings, Func<Task> saveSettings, Func<string> gamePath, Func<string> arguments, Func<string> loaderMode, Func<Task<bool>> prepareLaunch, bool preview = false)
    {
        _preview = preview;
        ReplacementPreviewButton.Visibility = preview ? Visibility.Visible : Visibility.Collapsed;
        _settings = settings; _saveSettings = saveSettings; _gamePath = gamePath; _gameArguments = arguments;
        _loaderMode = loaderMode; _prepareLaunch = prepareLaunch;
        _initializing = true;
        SteamPathBox.Text = settings.SteamExecutablePath.Length > 0 ? settings.SteamExecutablePath : SteamEnvironment.DiscoverExecutable();
        AutoUpdateBox.IsChecked = settings.AutoUpdateMetadata;
        AdministratorBox.IsChecked = settings.LaunchAsAdministrator;
        try
        {
            var receipt = _service.ReadReceipt();
            RefreshLibraries(settings.LibraryPath.Length > 0 ? settings.LibraryPath : receipt?.LibraryPath ?? "");
            _metadata = _service.ReadCache()?.Metadata ?? receipt?.Metadata;
            _metadata?.Validate();
            _managed = receipt is not null;
            _setupPending = receipt?.Pending == true;
            _pending = receipt is not null && (_setupPending || _metadata is not null && receipt.Metadata.Identity() != _metadata.Identity());
        }
        catch (Exception exception) { Failure(exception); }
        _initializing = false; _initialized = true;
        CapturePreferences();
        RefreshState();
        _timer.Start();
        if (_managed && _settings.AutoUpdateMetadata) _ = AutomaticUpdateAsync();
    }

    internal async Task ImportPreviewMetadataAsync(string path)
    {
        var metadata = await ReadMetadataFileAsync(path, _cancellation.Token);
        metadata.Validate(); await _service.CacheAsync(metadata); _metadata = metadata; RefreshState();
    }

    private static async Task<SteamMetadata> ReadMetadataFileAsync(string path, CancellationToken token)
    {
        if (new FileInfo(path).Length > 4 * 1024 * 1024) throw new SteamIntegrationException("invalid_metadata");
        return SteamMetadata.Parse(await File.ReadAllTextAsync(path, token));
    }

    private static string T(string chinese, string english) => LocalizationService.Instance.IsChinese ? chinese : english;
    private void LanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LocalizationService.IsChinese)) { Localize(); RefreshState(); }
    }
    private void Localize()
    {
        TitleText.Text = T("Steam 国服启动（预览）", "Steam CN launch (Preview)");
        IntroText.Text = T("在 Steam 库中启动国服，沿用主界面选择的加载方式。配置 ACF 不会安装 XInput。", "Launch the CN client from Steam using the loading mode selected in the main window. ACF setup does not install XInput.");
        SteamPathBox.Header = T("Steam 程序", "Steam executable"); LibraryBox.Header = T("Steam 游戏库", "Steam library");
        AutoUpdateBox.Content = new TextBlock { Text = T("BE 自动查询最新元数据，并在 Steam 退出后更新 ACF", "Check metadata automatically; update the ACF after Steam exits"), TextWrapping = TextWrapping.Wrap };
        AdministratorBox.Content = new TextBlock { Text = T("以管理员身份启动 Steam（覆盖层兼容，可选）", "Run Steam as administrator (optional overlay compatibility)"), TextWrapping = TextWrapping.Wrap };
        AdministratorHint.Text = T("默认关闭。当前实测普通权限仍可记录时长和展示状态；游戏提权时，管理员启动有助于覆盖层和截图。", "Off by default. Normal privileges still track playtime and status in current testing. Elevation can help the overlay and screenshots when the game is elevated.");
        ExitHint.Text = T("应用或移除配置前，请从 Steam 菜单选择“退出”。BE 会自动填写当前账号的启动选项，并保留原值供移除时恢复。", "Choose Exit in Steam before applying or removing the setup. BE fills in the current account's launch options automatically and saves the original value for restoration.");
        DetectButton.Content = T("自动检测", "Detect"); FetchButton.Content = T("获取元数据", "Fetch metadata"); ImportButton.Content = T("导入元数据…", "Import metadata…");
        PreviewButton.Content = T("预览 ACF", "Preview ACF"); ApplyButton.Content = T("应用 / 更新配置", "Apply / update setup"); RestoreButton.Content = T("移除 BE 配置", "Remove BE setup");
        ReplacementPreviewButton.Content = T("预览清理确认", "Preview cleanup confirmation");
        CopyButton.Content = T("复制启动命令（备用）", "Copy launch command (fallback)"); StartButton.Content = T("从 Steam 启动国服", "Launch CN from Steam");
        SyncAdminButton.Content = T("同步 Windows 管理员设置", "Sync Windows elevation setting"); RestoreAdminButton.Content = T("恢复管理员设置", "Restore elevation setting");
        CopyHint.Text = T("启动前会检查启动选项。切换账号、加载方式或更新 BE 后，如需更新命令，请先退出 Steam，再点击启动，BE 会自动填写。国服仍由官方启动器更新。", "Launch options are checked before starting. After changing accounts, loading modes or updating BE, exit Steam and launch again if the command needs updating; BE fills it in automatically. The official CN launcher still updates the game.");
    }

    private void CapturePreferences()
    {
        _settings.SteamExecutablePath = SteamPathBox.Text.Trim();
        _settings.LibraryPath = LibraryBox.SelectedItem as string ?? "";
        _settings.AutoUpdateMetadata = AutoUpdateBox.IsChecked == true;
        _settings.LaunchAsAdministrator = AdministratorBox.IsChecked == true;
    }
    private async void PreferencesChanged(object sender, RoutedEventArgs e)
    {
        if (_initializing || !_initialized) return;
        try
        {
            if (ReferenceEquals(sender, SteamPathBox) && File.Exists(SteamPathBox.Text.Trim()))
            {
                _initializing = true;
                try { RefreshLibraries(_settings.LibraryPath); }
                finally { _initializing = false; }
            }
            CapturePreferences(); await _saveSettings(); RefreshState();
        }
        catch (Exception exception) { Failure(exception); }
    }
    private void RefreshLibraries(string preferred)
    {
        var paths = SteamEnvironment.DiscoverLibraries(SteamPathBox.Text.Trim());
        LibraryBox.ItemsSource = paths;
        LibraryBox.SelectedItem = paths.FirstOrDefault(path => preferred.Length > 0 && SteamIntegrationService.SamePath(path, preferred)) ?? paths.FirstOrDefault();
    }
    private void RefreshState()
    {
        bool running = SteamEnvironment.IsRunning();
        MetadataText.Text = _metadata is null ? T("等待获取完整元数据。当前可导入元数据预览；完整数据公布后可自动查询。", "Waiting for complete metadata. Import metadata to preview the setup, or fetch it once published.")
            : T("已缓存版本：", "Cached build: ") + _metadata.BuildId + T("；资源清单：", "; depots: ") + _metadata.Depots.Count;
        ClientText.Text = (running ? T("Steam 正在运行；写入和恢复操作已禁用。", "Steam is running; write and restore operations are disabled.") : T("Steam 已退出，可以应用配置。", "Steam is closed; configuration can be applied."))
            + (_pending ? T(" 最新 ACF 待更新。", " ACF update pending.") : "");
        ApplyButton.IsEnabled = !_preview && !running && _metadata is not null;
        RestoreButton.IsEnabled = !_preview && !running && _managed;
        PreviewButton.IsEnabled = _metadata is not null;
        SyncAdminButton.IsEnabled = RestoreAdminButton.IsEnabled = !_preview && !running;
        StartButton.IsEnabled = !_preview && _managed && !_setupPending;
    }
    private void Notice(string message, InfoBarSeverity severity = InfoBarSeverity.Success)
    {
        StatusBar.Title = message; StatusBar.Message = ""; StatusBar.Severity = severity; StatusBar.IsOpen = true;
    }
    private void Failure(Exception exception)
    {
        string message = exception is SteamIntegrationException error ? error.Code switch
        {
            "steam_running" => T("请先完整退出 Steam（包括托盘后台）。", "Exit Steam completely, including the tray client, first."),
            "launch_options_pending" => T("启动选项需要自动填写。请从 Steam 菜单选择“退出”，再点击“从 Steam 启动国服”，BE 会自动填写并启动。", "Launch options need updating. Choose Exit in Steam, then click Launch CN from Steam; BE will fill them in and launch automatically."),
            "launch_options_changed" => T("Steam 启动选项已被另外修改。请点击“复制启动命令（备用）”，在 Steam 游戏属性中替换启动选项后重试。", "Steam launch options have been changed separately. Use Copy launch command (fallback), replace the game's launch options in Steam, then try again."),
            "steam_account_required" => T("无法确定 Steam 账号。请先打开 Steam 登录所需账号，完整退出 Steam 后重试。", "The Steam account could not be determined. Open Steam, sign in to the intended account, then exit Steam completely and try again."),
            "invalid_steam_config" => T("Steam 用户配置无法安全读取。请打开 Steam 并正常退出后重试。", "The Steam user configuration could not be read safely. Open Steam, exit it normally, then try again."),
            "steam_config_changed" => T("Steam 配置在操作期间发生变化，请确认 Steam 已完整退出后重试。", "Steam configuration changed during the operation. Ensure Steam is completely closed, then try again."),
            "metadata_pending" => T("完整元数据尚未就绪，请稍后刷新或导入有效元数据。", "Complete metadata is not available yet. Refresh later or import valid metadata."),
            "restart_elevated" => T("当前 Steam 未确认以管理员权限运行，请退出后再启动。", "The running Steam client is not confirmed elevated. Exit it before relaunching."),
            "existing_install" or "foreign_manifest" or "install_changed" => T("Steam 目录中有已有文件或清单。请点击“应用 / 更新配置”，查看并确认清理范围后重新配置。", "The Steam directory contains existing files or a manifest. Click Apply / update setup to review and confirm cleanup before setting it up again."),
            "other_library_install" => T("其他 Steam 游戏库中已有终末地安装，请先处理该安装。", "Endfield is already installed in another Steam library. Resolve that installation first."),
            "replacement_changed" => T("目录内容在确认期间发生变化，已停止清理。请再次点击“应用 / 更新配置”查看最新范围。", "Directory contents changed during confirmation; cleanup stopped. Click Apply / update setup again to review the current contents."),
            "replacement_pending" => T("上次清理尚未完成。请退出 Steam，点击“应用 / 更新配置”重新确认剩余文件。", "The previous cleanup is incomplete. Exit Steam and click Apply / update setup to confirm the remaining files."),
            "protected_directory" or "game_directory_overlap" => T("待清理目录与国服游戏、BE 程序或配置目录重叠，无法清理。请另选独立的 Steam 安装位置。", "The cleanup directory overlaps the CN game, BE installation or configuration directory. Choose a separate Steam installation location."),
            "reparse_point" => T("目标路径中有目录链接或重解析点，无法安全清理。请使用实际的 Steam 游戏库目录。", "The target path contains a link or reparse point and cannot be cleaned safely. Use a physical Steam library directory."),
            "layout_changed" or "library_changed" => T("安装位置或启动布局已变化，请先移除 BE 的旧 ACF，再重新配置。", "The installation or launch layout changed. Remove BE's old ACF before setting it up again."),
            "invalid_game_path" => T("请在主程序中选择有效的 Endfield.exe。", "Select a valid Endfield.exe in the main application."),
            "invalid_launcher_path" => T("启动组件缺失，请重新解压完整的 Better Endfield Next。", "A launch component is missing. Extract the complete Better Endfield Next package again."),
            "invalid_launch_arguments" => T("请检查启动参数的引号；不能包含换行、%command% 或 --steam-command。", "Check argument quoting; line breaks, %command% and --steam-command are not allowed."),
            "game_running" => T("游戏已在运行，请先退出。", "The game is already running. Exit it first."),
            "operation_busy" => T("另一 BE 实例正在操作 Steam 配置，请稍后再试。", "Another BE instance is modifying the Steam configuration. Try again shortly."),
            "setup_pending" => T("Steam 配置尚未提交完成，请先退出 Steam 并点击“应用 / 更新配置”。", "Steam setup is not fully committed. Exit Steam and click Apply / update setup first."),
            "invalid_steam_path" => T("请选择有效的 steam.exe。", "Select a valid steam.exe."),
            "invalid_library" or "unregistered_library" => T("请选择 Steam 已登记的游戏库。", "Choose a registered Steam library."),
            "compatibility_conflict" or "compatibility_changed" or "steam_path_changed" => T("Steam 的兼容性设置与当前操作冲突，请先检查 Windows 设置。", "Steam's compatibility settings conflict with this operation. Check the Windows settings first."),
            _ => T("Steam 配置未通过检查：", "Steam configuration did not pass validation: ") + error.Code
        } : exception.Message;
        Notice(message, InfoBarSeverity.Warning);
    }
    private async Task RunAsync(Func<Task> action)
    {
        if (_busy || !_initialized) return;
        _busy = true; IsEnabled = false; BusyRing.IsActive = true; BusyRing.Visibility = Visibility.Visible;
        try { await action(); }
        catch (OperationCanceledException) when (_cancellation.IsCancellationRequested) { }
        catch (Exception exception) { Failure(exception); }
        finally
        {
            // A client-start race can leave a recoverable pending receipt even
            // when the first apply failed. Keep this instance able to resume.
            try
            {
                var receipt = _service.ReadReceipt();
                _managed = receipt is not null;
                _setupPending = receipt?.Pending == true;
                _pending = receipt is not null && (_setupPending || _metadata is not null && receipt.Metadata.Identity() != _metadata.Identity());
            }
            catch (Exception exception) { Failure(exception); }
            _busy = false; IsEnabled = true; BusyRing.IsActive = false; BusyRing.Visibility = Visibility.Collapsed; RefreshState();
        }
    }
    private async Task FetchAsync()
    {
        var latest = await _service.FetchLatestAsync(_cancellation.Token);
        latest.Validate();
        _metadata = latest;
        var receipt = _service.ReadReceipt();
        _setupPending = receipt?.Pending == true;
        _pending = receipt is not null && (_setupPending || receipt.Metadata.Identity() != latest.Identity());
        Notice(_pending ? T("最新元数据已缓存，等待 Steam 退出后更新。", "Latest metadata cached. The ACF will update after Steam exits.") : T("元数据已缓存。", "Metadata cached."));
    }
    private async Task AutomaticUpdateAsync()
    {
        if (!_initialized || _busy || !_managed || !_settings.AutoUpdateMetadata) { if (_initialized) RefreshState(); return; }
        if (DateTimeOffset.UtcNow < _nextQuery && !(_pending && DateTimeOffset.UtcNow >= _nextApply && !SteamEnvironment.IsRunning())) { RefreshState(); return; }
        await RunAsync(async () =>
        {
            if (DateTimeOffset.UtcNow >= _nextQuery)
            {
                _nextQuery = DateTimeOffset.UtcNow.AddHours(1);
                await FetchAsync();
            }
            if (_pending && DateTimeOffset.UtcNow >= _nextApply && !SteamEnvironment.IsRunning() && _metadata is not null)
            {
                _nextApply = DateTimeOffset.UtcNow.AddMinutes(5);
                await _service.ApplyAsync(_settings.LibraryPath, _gamePath(), _metadata, SteamEnvironment.DiscoverLibraries(_settings.SteamExecutablePath), _cancellation.Token);
                _pending = false; _setupPending = false;
                Notice(T("BE 已更新 Steam ACF。", "BE updated the Steam ACF."));
            }
        });
    }
    private async void Detect_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        _initializing = true;
        try { SteamPathBox.Text = SteamEnvironment.DiscoverExecutable(); RefreshLibraries(_settings.LibraryPath); }
        finally { _initializing = false; }
        CapturePreferences(); await _saveSettings();
    });
    private async void Fetch_Click(object sender, RoutedEventArgs e) => await RunAsync(FetchAsync);
    private async void Import_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".json");
        WinRT.Interop.InitializeWithWindow.Initialize(picker, WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindowInstance!));
        var file = await picker.PickSingleFileAsync(); if (file is null) return;
        var metadata = await ReadMetadataFileAsync(file.Path, _cancellation.Token);
        metadata.Validate(); await _service.CacheAsync(metadata, _cancellation.Token); _metadata = metadata;
        var receipt = _service.ReadReceipt();
        _setupPending = receipt?.Pending == true;
        _pending = receipt is not null && (_setupPending || receipt.Metadata.Identity() != metadata.Identity());
        Notice(T("元数据已导入，可以预览 ACF。", "Metadata imported. You can preview the ACF."));
    });
    private async void Preview_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        if (_metadata is null) throw new SteamIntegrationException("metadata_pending");
        await new ContentDialog
        {
            XamlRoot = XamlRoot, Title = T("ACF 预览（尚未写入）", "ACF preview (not written)"), CloseButtonText = T("关闭", "Close"),
            Content = new TextBox { AcceptsReturn = true, IsReadOnly = true, MaxHeight = 440, MinWidth = 380, TextWrapping = TextWrapping.Wrap, Text = SteamIntegrationService.GenerateAcf(_metadata) }
        }.ShowAsync();
    });
    private async void Apply_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        if (SteamEnvironment.IsRunning()) throw new SteamIntegrationException("steam_running");
        if (_metadata is null) throw new SteamIntegrationException("metadata_pending");
        _metadata.Validate(); CapturePreferences();
        string command = CurrentLaunchCommand();
        _launchOptions.ValidateCanConfigure(_settings.SteamExecutablePath, command);
        var libraries = SteamEnvironment.DiscoverLibraries(_settings.SteamExecutablePath);
        try
        {
            await _service.ApplyAsync(_settings.LibraryPath, _gamePath(), _metadata, libraries, _cancellation.Token);
        }
        catch (SteamIntegrationException exception) when (exception.Code is "existing_install" or "install_changed" or "foreign_manifest" or "replacement_pending")
        {
            var plan = _service.PrepareReplacement(_settings.LibraryPath, _gamePath(), _metadata, libraries);
            if (!await ConfirmReplacementAsync(plan.GameDirectory, plan.Manifest, plan.FileCount, plan.TotalBytes))
            {
                Notice(T("已取消清理，未删除文件。", "Cleanup canceled; no files were deleted."), InfoBarSeverity.Informational);
                return;
            }
            // The service rechecks the confirmed paths and contents before deleting.
            await _service.ReplaceAsync(plan, _cancellation.Token);
        }
        _managed = true; _pending = false; _setupPending = false;
        await _saveSettings();
        _launchOptions.EnsureConfigured(_settings.SteamExecutablePath, command);
        Notice(T("配置已应用，Steam 启动选项已自动填写。可以从 Steam 启动国服。", "Setup applied and Steam launch options filled in automatically. You can now launch the CN game from Steam."));
    });
    private async Task<bool> ConfirmReplacementAsync(string directory, string manifest, long fileCount, long totalBytes)
    {
        string content = T(
            "将永久删除以下 Steam 目录中的全部内容及安装清单，然后重新配置国服启动。若目录中有已下载的 Steam 版游戏，这些文件也会删除。\n\n",
            "All contents of the following Steam directory and its install manifest will be permanently deleted, then CN launch will be configured again. Any downloaded Steam game files in this directory will also be deleted.\n\n")
            + T("游戏目录：\n", "Game directory:\n") + directory + "\n\n"
            + T("安装清单：\n", "Install manifest:\n") + manifest + "\n\n"
            + T($"共 {fileCount:N0} 个文件，{totalBytes:N0} 字节。\n此操作不可撤销。", $"{fileCount:N0} files, {totalBytes:N0} bytes in total.\nThis action cannot be undone.");
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = T("清理 Steam 目录并重新配置？", "Clean up Steam directory and configure again?"),
            Content = new ScrollViewer
            {
                MaxHeight = 420,
                Content = new TextBlock { Text = content, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true }
            },
            PrimaryButtonText = T("删除并重新配置", "Delete and configure again"),
            CloseButtonText = T("取消", "Cancel"),
            DefaultButton = ContentDialogButton.Close
        }.ShowAsync();
        return result == ContentDialogResult.Primary;
    }
    private async void ReplacementPreview_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        if (!_preview) return;
        // Exercise the real dialog in the isolated preview without invoking cleanup.
        string steamapps = Path.Combine(Path.GetTempPath(), "BE-SteamPreview", "steamapps");
        bool confirmed = await ConfirmReplacementAsync(Path.Combine(steamapps, "common", "Arknights Endfield"),
            Path.Combine(steamapps, "appmanifest_" + SteamMetadata.EndfieldAppId + ".acf"), 3, 592);
        Notice(confirmed ? T("仅预览：未删除或修改任何文件。", "Preview only: no files were deleted or changed.")
            : T("已取消清理，未删除文件。", "Cleanup canceled; no files were deleted."), InfoBarSeverity.Informational);
    });
    private async void Restore_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        if (SteamEnvironment.IsRunning()) throw new SteamIntegrationException("steam_running");
        _launchOptions.Restore();
        await _service.RestoreAsync(_cancellation.Token); _managed = false; _pending = false; _setupPending = false;
        Notice(T("BE 配置已移除，原 Steam 启动选项已恢复。", "BE setup removed and the original Steam launch options restored."));
    });
    private string CurrentLaunchCommand()
    {
        string helper = GameLaunchService.SelectHelperPath(_loaderMode(), RuntimePathDiscoveryService.BundledInjectorPath,
            RuntimePathDiscoveryService.BundledLauncherPath);
        return SteamIntegrationService.GenerateLaunchCommand(helper, _gamePath(), _gameArguments());
    }
    private async void Copy_Click(object sender, RoutedEventArgs e) => await RunAsync(() =>
    {
        var data = new DataPackage(); data.SetText(CurrentLaunchCommand()); Clipboard.SetContent(data);
        Notice(T("启动命令已复制。", "Launch command copied.")); return Task.CompletedTask;
    });
    private async void Start_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        var processes = System.Diagnostics.Process.GetProcessesByName("Endfield");
        bool running = processes.Length > 0; foreach (var process in processes) process.Dispose();
        if (running) { Notice(T("游戏已在运行，请先退出。", "The game is already running. Exit it first."), InfoBarSeverity.Warning); return; }
        var receipt = _service.ReadReceipt();
        if (receipt is null) throw new SteamIntegrationException("metadata_pending");
        if (receipt.Pending) throw new SteamIntegrationException("setup_pending");
        if (!await _prepareLaunch()) { Notice(T("请先完成主界面的游戏路径和当前加载方式配置。", "Complete the game path and selected loading mode in the main window first."), InfoBarSeverity.Warning); return; }
        CapturePreferences();
        _launchOptions.EnsureConfigured(_settings.SteamExecutablePath, CurrentLaunchCommand());
        SteamEnvironment.Launch(_settings.SteamExecutablePath, _settings.LaunchAsAdministrator, game: true);
        Notice(T("Steam 启动请求已发送。", "Steam launch requested."));
    });
    private async void SyncAdmin_Click(object sender, RoutedEventArgs e) => await RunAsync(() =>
    {
        _compatibility.Apply(_settings.SteamExecutablePath, _settings.LaunchAsAdministrator);
        Notice(SteamRegistryCompatibilityStore.MachineAdministrator(_settings.SteamExecutablePath) && !_settings.LaunchAsAdministrator
            ? T("当前用户设置已同步，但 Windows 的全用户设置仍要求管理员启动。", "Current-user setting synced; the all-users Windows setting still requires elevation.")
            : T("当前用户的 Windows 管理员启动设置已同步；其他兼容选项保留。", "Current-user Windows elevation setting synced; other compatibility flags preserved."));
        return Task.CompletedTask;
    });
    private async void RestoreAdmin_Click(object sender, RoutedEventArgs e) => await RunAsync(() =>
    {
        _compatibility.Restore(); Notice(T("管理员启动设置已恢复，其他兼容选项保留。", "Elevation setting restored; other compatibility flags preserved.")); return Task.CompletedTask;
    });
}
