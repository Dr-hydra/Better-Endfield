using System.ComponentModel;
using System.Net.Http;
using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Pickers;

namespace BetterEndfield.UI.Views;

public sealed partial class SteamIntegrationPage : UserControl
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(20) };
    private readonly SteamIntegrationService _service;
    private readonly SteamCompatibilityService _compatibility;
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(2) };
    private readonly CancellationTokenSource _cancellation = new();
    private SteamIntegrationSettings _settings = new();
    private Func<Task> _saveSettings = () => Task.CompletedTask;
    private Func<string> _gamePath = () => "";
    private Func<string> _gameArguments = () => "";
    private Func<Task<bool>> _prepareXInput = () => Task.FromResult(false);
    private SteamMetadata? _metadata;
    private bool _initializing = true, _initialized, _busy, _pending, _managed, _preview, _setupPending;
    private DateTimeOffset _nextQuery, _nextApply;

    public SteamIntegrationPage() : this(Path.Combine(ConfigurationService.SettingsDirectory, "steam")) { }
    internal SteamIntegrationPage(string stateDirectory)
    {
        InitializeComponent();
        _service = new SteamIntegrationService(stateDirectory, _http, SteamEnvironment.IsRunning);
        _compatibility = new SteamCompatibilityService(Path.Combine(stateDirectory, "administrator.json"), new SteamRegistryCompatibilityStore(), SteamEnvironment.IsRunning);
        _timer.Tick += async (_, _) => await AutomaticUpdateAsync();
        LocalizationService.Instance.PropertyChanged += LanguageChanged;
        Unloaded += (_, _) =>
        {
            _timer.Stop(); _cancellation.Cancel();
            LocalizationService.Instance.PropertyChanged -= LanguageChanged;
        };
        Localize();
    }

    internal void Initialize(SteamIntegrationSettings settings, Func<Task> saveSettings, Func<string> gamePath, Func<string> arguments, Func<Task<bool>> prepareXInput, bool preview = false)
    {
        _preview = preview;
        _settings = settings; _saveSettings = saveSettings; _gamePath = gamePath; _gameArguments = arguments; _prepareXInput = prepareXInput;
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
        IntroText.Text = T("在 Steam 库中直接启动国服，使用 BE 的 XInput 自启动加载模块。", "Launch the CN client from your Steam library with BE's XInput auto-loader.");
        SteamPathBox.Header = T("Steam 程序", "Steam executable"); LibraryBox.Header = T("Steam 游戏库", "Steam library");
        AutoUpdateBox.Content = new TextBlock { Text = T("BE 自动查询最新元数据，并在 Steam 退出后更新 ACF", "Check metadata automatically; update the ACF after Steam exits"), TextWrapping = TextWrapping.Wrap };
        AdministratorBox.Content = new TextBlock { Text = T("以管理员身份启动 Steam（覆盖层兼容，可选）", "Run Steam as administrator (optional overlay compatibility)"), TextWrapping = TextWrapping.Wrap };
        AdministratorHint.Text = T("默认关闭。当前实测普通权限仍可记录时长和展示状态；游戏提权时，管理员启动有助于覆盖层和截图。", "Off by default. Normal privileges still track playtime and status in current testing. Elevation can help the overlay and screenshots when the game is elevated.");
        ExitHint.Text = T("应用、更新或移除 ACF 前，请在 Steam 菜单选择“退出”，并确认托盘后台也已结束。", "Before applying, updating or removing an ACF, choose Exit in Steam and ensure the tray client has closed.");
        DetectButton.Content = T("自动检测", "Detect"); FetchButton.Content = T("获取元数据", "Fetch metadata"); ImportButton.Content = T("导入元数据…", "Import metadata…");
        PreviewButton.Content = T("预览 ACF", "Preview ACF"); ApplyButton.Content = T("应用 / 更新 ACF", "Apply / update ACF"); RestoreButton.Content = T("移除 BE 的 ACF", "Remove BE's ACF");
        CopyButton.Content = T("复制启动命令", "Copy launch command"); StartButton.Content = T("从 Steam 启动国服", "Launch CN from Steam");
        SyncAdminButton.Content = T("同步 Windows 管理员设置", "Sync Windows elevation setting"); RestoreAdminButton.Content = T("恢复管理员设置", "Restore elevation setting");
        CopyHint.Text = T("首次配置后，将复制的命令粘贴到 Steam 游戏属性的“启动选项”。国服仍由官方启动器更新。", "After initial setup, paste the copied command into the game's Steam Launch Options. The official CN launcher still updates the CN game.");
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
            "metadata_pending" => T("完整元数据尚未就绪，请稍后刷新或导入有效元数据。", "Complete metadata is not available yet. Refresh later or import valid metadata."),
            "restart_elevated" => T("当前 Steam 未确认以管理员权限运行，请退出后再启动。", "The running Steam client is not confirmed elevated. Exit it before relaunching."),
            "existing_install" or "other_library_install" or "foreign_manifest" => T("检测到已有 Steam 安装或其他来源的 ACF，BE 不会接管，请检查游戏库。", "An existing Steam installation or another tool's ACF was found. Check the selected library."),
            "install_changed" => T("占位目录已包含其他文件，请先检查 Steam 下载或安装状态。", "The placeholder directory now contains other files. Check Steam's download or installation state."),
            "layout_changed" or "library_changed" => T("安装位置或启动布局已变化，请先移除 BE 的旧 ACF，再重新配置。", "The installation or launch layout changed. Remove BE's old ACF before setting it up again."),
            "invalid_game_path" => T("请在主程序中选择有效的 Endfield.exe。", "Select a valid Endfield.exe in the main application."),
            "game_running" => T("请先退出游戏，再准备 XInput 启动。", "Exit the game before preparing XInput launch."),
            "operation_busy" => T("另一 BE 实例正在操作 Steam 配置，请稍后再试。", "Another BE instance is modifying the Steam configuration. Try again shortly."),
            "setup_pending" => T("Steam 配置尚未提交完成，请先退出 Steam 并应用 ACF。", "Steam setup is not fully committed. Exit Steam and apply the ACF first."),
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
        if (!await _prepareXInput()) { Notice(T("请先完成主程序游戏路径和 XInput 配置。", "Complete the game path and XInput setup in the main application first."), InfoBarSeverity.Warning); return; }
        await _service.ApplyAsync(_settings.LibraryPath, _gamePath(), _metadata, SteamEnvironment.DiscoverLibraries(_settings.SteamExecutablePath), _cancellation.Token);
        _managed = true; _pending = false; _setupPending = false;
        await _saveSettings();
        Notice(T("ACF 已应用。请复制启动命令，粘贴到 Steam 游戏的启动选项。", "ACF applied. Copy the launch command into the game's Steam Launch Options."));
    });
    private async void Restore_Click(object sender, RoutedEventArgs e) => await RunAsync(async () =>
    {
        await _service.RestoreAsync(_cancellation.Token); _managed = false; _pending = false; _setupPending = false;
        Notice(T("BE 的 ACF 和占位文件已移除，备份保留在 BE 配置目录。", "BE's ACF and placeholder were removed. Backups are retained in BE's settings directory."));
    });
    private async void Copy_Click(object sender, RoutedEventArgs e) => await RunAsync(() =>
    {
        var data = new DataPackage(); data.SetText(SteamIntegrationService.GenerateLaunchCommand(_gamePath(), _gameArguments())); Clipboard.SetContent(data);
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
        if (!await _prepareXInput()) { Notice(T("请先完成游戏路径和 XInput 配置。", "Complete the game path and XInput setup first."), InfoBarSeverity.Warning); return; }
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
