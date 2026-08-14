using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using EFStartChange.UI.Models;
using EFStartChange.UI.Services;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using Windows.Storage.Pickers;

namespace EFStartChange.UI;

internal sealed class VoiceCharacterChoice
{
    public required string Speaker { get; init; }

    public required string DisplayName { get; init; }

    public string? CharacterId { get; init; }
}

internal sealed class VoiceRuleEntry
{
    public required string Speaker { get; init; }

    public required string DisplayName { get; init; }

    public required string Language { get; init; }

    public required string LanguageDisplayName { get; init; }
}

public sealed partial class MainWindow : Window
{
    private const string DisclaimerVersion = "1";
    private const string BilibiliProfileUrl = "https://space.bilibili.com/441133155";
    private const string XiaoheiheProfileUrl =
        "https://www.xiaoheihe.cn/app/user/profile/38080236";
    private const string QqGroupNumber = "851586605";

    private static readonly Dictionary<string, string> VoiceLanguageNames = new(
        StringComparer.OrdinalIgnoreCase)
    {
        ["Chinese"] = "Chinese",
        ["CN"] = "Chinese",
        ["ZH"] = "Chinese",
        ["English"] = "English",
        ["EN"] = "English",
        ["Japanese"] = "Japanese",
        ["JP"] = "Japanese",
        ["JA"] = "Japanese",
        ["Korean"] = "Korean",
        ["KR"] = "Korean",
        ["KO"] = "Korean",
        ["FollowGlobal"] = "FollowGlobal",
        ["Global"] = "FollowGlobal",
        ["Default"] = "FollowGlobal"
    };

    private static readonly Regex RuntimeClipLengthPattern = new(
        @"\[sequence\].*?phase=(?<label>\S+).*?length=(?<length>[0-9]+(?:\.[0-9]+)?)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly IReadOnlyDictionary<string, string> VoiceLanguageDisplayNames =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Chinese"] = "中文",
            ["English"] = "English",
            ["Japanese"] = "日本語",
            ["Korean"] = "한국어",
            ["FollowGlobal"] = "跟随游戏"
        };

    private readonly DispatcherTimer _statusTimer = new()
    {
        Interval = TimeSpan.FromSeconds(1)
    };

    private bool _initializing = true;
    private string _durationLogPath = string.Empty;
    private DateTime _durationLogWriteUtc;
    private readonly ObservableCollection<VoiceRuleEntry> _voiceRules = [];
    private readonly IReadOnlyList<VoiceCharacterChoice> _voiceCharacters;
    private AppSettings _appSettings = new();
    private string _latestReleaseUrl = UpdateService.ReleasesUrl;

    public MainWindow()
    {
        InitializeComponent();
        Title = "Better Endfield";
        SystemBackdrop = new MicaBackdrop();
        TryResizeWindow();
        CurrentVersionTextBlock.Text = $"版本 {UpdateService.CurrentVersion}";
        if (FeatureNavigation.SettingsItem is NavigationViewItem settingsItem)
        {
            settingsItem.Content = "设置";
        }

        CharacterComboBox.ItemsSource = PresetOptions.Characters;
        _voiceCharacters = PresetOptions.CharacterNames
            .OrderBy(entry => entry.Key, StringComparer.OrdinalIgnoreCase)
            .Select(entry => new VoiceCharacterChoice
            {
                CharacterId = entry.Key,
                Speaker = GetCharacterSpeakerAlias(entry.Key),
                DisplayName = $"{entry.Value}  ·  {entry.Key}"
            })
            .GroupBy(choice => choice.Speaker, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .Append(new VoiceCharacterChoice
            {
                Speaker = "*",
                DisplayName = "其他角色（默认规则）"
            })
            .ToArray();
        VoiceCharacterComboBox.ItemsSource = _voiceCharacters;
        VoiceCharacterComboBox.SelectedIndex = 0;
        VoiceRulesListView.ItemsSource = _voiceRules;
        SelectVoiceLanguage("Japanese");
        UpdateVoiceRulesEmptyState();
        _statusTimer.Tick += StatusTimer_Tick;
        Closed += MainWindow_Closed;
    }

    private async void MainRoot_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _appSettings = await ConfigurationService.LoadAppSettingsAsync();
            SelectTheme(_appSettings.Theme);
            ApplyTheme(_appSettings.Theme);
            if (!string.Equals(
                _appSettings.DisclaimerAcceptedVersion,
                DisclaimerVersion,
                StringComparison.Ordinal))
            {
                bool accepted = await ShowDisclaimerAsync(requireAcceptance: true);
                if (!accepted)
                {
                    Close();
                    return;
                }

                _appSettings.DisclaimerAcceptedVersion = DisclaimerVersion;
                await ConfigurationService.SaveAppSettingsAsync(_appSettings);
            }

            GamePathBox.Text = !string.IsNullOrWhiteSpace(_appSettings.GameExecutablePath)
                ? _appSettings.GameExecutablePath
                : ConfigurationService.DiscoverGamePath();
            MapperPathBox.Text = !string.IsNullOrWhiteSpace(_appSettings.MapperPath)
                ? _appSettings.MapperPath
                : ConfigurationService.DiscoverMapperPath();
            ExternalLoaderToggle.IsOn = _appSettings.ExternalLoaderEnabled;
            ExternalLoaderPathBox.Text = _appSettings.ExternalLoaderPath;
            ExternalLoaderArgumentsBox.Text = _appSettings.ExternalLoaderArguments;
            ExternalLoaderDelayNumberBox.Value = Math.Clamp(
                _appSettings.ExternalLoaderDelaySeconds,
                ExternalLoaderDelayNumberBox.Minimum,
                ExternalLoaderDelayNumberBox.Maximum);
            RefreshRuntimeAnimationDurations();

            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(MapperPathBox.Text);
            ApplyConfiguration(configuration);
            _initializing = false;
            UpdateCrossfadePanel();
            RefreshRuntimeStatus();
            _statusTimer.Start();
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            _initializing = false;
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void BrowseGameButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 Endfield.exe");
        if (selectedPath is not null)
        {
            GamePathBox.Text = selectedPath;
        }
    }

    private async void BrowseMapperButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 Il2cppDumper.exe");
        if (selectedPath is null)
        {
            return;
        }

        MapperPathBox.Text = selectedPath;
        try
        {
            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(selectedPath);
            ApplyConfiguration(configuration);
        }
        catch (IOException exception)
        {
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void BrowseExternalLoaderButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择外部加载器");
        if (selectedPath is not null)
        {
            ExternalLoaderPathBox.Text = selectedPath;
        }
    }

    private void MapperPathBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ConfigurationPathTextBlock.Text =
            ConfigurationService.GetNativeConfigurationPath(MapperPathBox.Text);
        RefreshRuntimeAnimationDurations();
        RefreshRuntimeStatus();
    }

    private void CharacterComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        string? preferredAction = _initializing
            ? null
            : (CharacterComboBox.SelectedItem as CharacterOption)?.DefaultActionId;
        RefreshActionOptions(preferredAction);
    }

    private void FinalActionComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (FinalActionComboBox.SelectedItem is not ActionOption action)
        {
            ActionDurationTextBlock.Text = string.Empty;
            ActionDescriptionTextBlock.Text = string.Empty;
            return;
        }

        UpdateActionMetadata(action);
        ActionDescriptionTextBlock.Text =
            $"资源哈希：{action.PathHash} · 原生 LoopTime：{(action.NativeLoop ? "是" : "否")}";
        if (!_initializing)
        {
            FinalLoopToggle.IsOn = action.NativeLoop;
            ForceLoopToggle.IsOn = false;
            CrossfadeToggle.IsOn = false;
        }
    }

    private void CrossfadeToggle_Toggled(object sender, RoutedEventArgs e) =>
        UpdateCrossfadePanel();

    private void FeatureNavigation_SelectionChanged(
        NavigationView sender,
        NavigationViewSelectionChangedEventArgs args)
    {
        string page = args.IsSettingsSelected
            ? "settings"
            : args.SelectedItemContainer?.Tag as string ?? "model";
        ModelPageScrollViewer.Visibility = page == "model"
            ? Visibility.Visible
            : Visibility.Collapsed;
        VoicePageScrollViewer.Visibility = page == "voice"
            ? Visibility.Visible
            : Visibility.Collapsed;
        SettingsPageScrollViewer.Visibility = page == "settings"
            ? Visibility.Visible
            : Visibility.Collapsed;
        AboutPageScrollViewer.Visibility = page == "about"
            ? Visibility.Visible
            : Visibility.Collapsed;
        ActionBar.Visibility = page == "about"
            ? Visibility.Collapsed
            : Visibility.Visible;
        PageSelectionHintTextBlock.Text = page switch
        {
            "settings" => "路径与外观会随保存一起写入本机设置。",
            "voice" => "角色配音规则保存后在下一次注入时生效。",
            _ => "角色与动画参数保存后在下一次注入时生效。"
        };
    }

    private void VoiceCharacterComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (VoiceCharacterComboBox.SelectedItem is not VoiceCharacterChoice choice)
        {
            return;
        }

        VoiceRuleEntry? existing = _voiceRules.FirstOrDefault(rule =>
            rule.Speaker.Equals(choice.Speaker, StringComparison.OrdinalIgnoreCase));
        SelectVoiceLanguage(existing?.Language ?? "FollowGlobal");
    }

    private void AddVoiceRuleButton_Click(object sender, RoutedEventArgs e)
    {
        if (VoiceCharacterComboBox.SelectedItem is not VoiceCharacterChoice choice ||
            GetSelectedVoiceLanguage() is not string language)
        {
            return;
        }

        UpsertVoiceRule(choice.Speaker, language, choice.DisplayName);
        ShowStatus(
            "配音规则已更新",
            $"{choice.DisplayName} 将使用 {GetVoiceLanguageDisplayName(language)}。",
            InfoBarSeverity.Success);
    }

    private void RemoveVoiceRuleButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string speaker })
        {
            return;
        }

        VoiceRuleEntry? entry = _voiceRules.FirstOrDefault(rule =>
            rule.Speaker.Equals(speaker, StringComparison.OrdinalIgnoreCase));
        if (entry is not null)
        {
            _voiceRules.Remove(entry);
            UpdateVoiceRulesEmptyState();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        await SaveAsync(showSuccess: true);
    }

    private async void LaunchButton_Click(object sender, RoutedEventArgs e)
    {
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus(
                "游戏已经在运行",
                "为避免重复注入，请完整退出游戏后再使用“保存并启动”。",
                InfoBarSeverity.Warning);
            return;
        }

        if (!File.Exists(GamePathBox.Text.Trim()))
        {
            ShowStatus("游戏路径无效", "请选择有效的 Endfield.exe。", InfoBarSeverity.Error);
            return;
        }

        LaunchProgressRing.Visibility = Visibility.Visible;
        LaunchProgressRing.IsActive = true;
        try
        {
            if (!await SaveAsync(showSuccess: false))
            {
                return;
            }

            if (!await StartExternalLoaderAsync())
            {
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = MapperPathBox.Text.Trim(),
                WorkingDirectory = Path.GetDirectoryName(MapperPathBox.Text.Trim()) ?? string.Empty,
                UseShellExecute = true
            };
            startInfo.ArgumentList.Add(GamePathBox.Text.Trim());
            Process.Start(startInfo);
            ShowStatus(
                "注入器已启动",
                ExternalLoaderToggle.IsOn
                    ? "外部加载器已先行启动；如果出现用户账户控制提示，请允许注入器权限。"
                    : "如果出现用户账户控制提示，请允许管理员权限。游戏启动后状态会自动更新。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or IOException or
                UnauthorizedAccessException or Win32Exception)
        {
            ShowStatus("启动失败", exception.Message, InfoBarSeverity.Error);
        }
        finally
        {
            LaunchProgressRing.IsActive = false;
            LaunchProgressRing.Visibility = Visibility.Collapsed;
        }
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyConfiguration(ModConfiguration.CreateDefaults());
        ShowStatus(
            "已恢复界面默认值",
            "点击“保存参数”后才会覆盖配置文件。",
            InfoBarSeverity.Informational);
    }

    private void OpenConfigurationFolderButton_Click(object sender, RoutedEventArgs e)
    {
        string configPath =
            ConfigurationService.GetNativeConfigurationPath(MapperPathBox.Text);
        string? directory = Path.GetDirectoryName(configPath);
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
        {
            ShowStatus("目录不存在", "请先选择有效的注入器。", InfoBarSeverity.Warning);
            return;
        }

        OpenWithShell(directory);
    }

    private void OpenLogButton_Click(object sender, RoutedEventArgs e)
    {
        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (!File.Exists(logPath))
        {
            ShowStatus(
                "尚未生成日志",
                "至少完成一次注入后才会出现 IL2CPPDump_Log.txt。",
                InfoBarSeverity.Informational);
            return;
        }

        OpenWithShell(logPath);
    }

    private void ThemeComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        string theme = GetSelectedTheme();
        ApplyTheme(theme);
        _appSettings.Theme = theme;
    }

    private void CreateAppShortcutButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string shortcutPath = ShortcutService.CreateApplicationShortcut();
            ShowStatus(
                "快捷方式已创建",
                shortcutPath,
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or COMException)
        {
            ShowStatus("创建快捷方式失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private void CreateGameShortcutButton_Click(object sender, RoutedEventArgs e)
    {
        string mapperPath = MapperPathBox.Text.Trim();
        string gamePath = GamePathBox.Text.Trim();
        if (!File.Exists(mapperPath))
        {
            ShowStatus("注入器路径无效", "请选择有效的 Il2cppDumper.exe。", InfoBarSeverity.Error);
            return;
        }
        if (!File.Exists(gamePath))
        {
            ShowStatus("游戏路径无效", "请选择有效的 Endfield.exe。", InfoBarSeverity.Error);
            return;
        }

        try
        {
            string shortcutPath = ShortcutService.CreateGameShortcut(mapperPath, gamePath);
            ShowStatus(
                "一键启动快捷方式已创建",
                $"已创建：{shortcutPath}",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or COMException)
        {
            ShowStatus("创建快捷方式失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
    {
        CheckUpdatesButton.IsEnabled = false;
        UpdateProgressRing.Visibility = Visibility.Visible;
        UpdateProgressRing.IsActive = true;
        UpdateButtonIcon.Visibility = Visibility.Collapsed;
        UpdateInfoBar.IsOpen = false;
        OpenReleaseButton.Visibility = Visibility.Collapsed;
        try
        {
            UpdateCheckResult result = await UpdateService.CheckAsync(CancellationToken.None);
            _latestReleaseUrl = result.ReleasesUrl;
            if (!result.HasRelease)
            {
                UpdateInfoBar.Title = "暂无可用发布版本";
                UpdateInfoBar.Message = "GitHub Releases 中还没有正式发布记录。";
                UpdateInfoBar.Severity = InfoBarSeverity.Informational;
                OpenReleaseButton.Visibility = Visibility.Visible;
            }
            else if (result.IsUpdateAvailable)
            {
                UpdateInfoBar.Title = "发现新版本";
                UpdateInfoBar.Message =
                    $"当前 {result.CurrentVersion}，最新 {result.LatestVersion}。";
                UpdateInfoBar.Severity = InfoBarSeverity.Success;
                OpenReleaseButton.Visibility = Visibility.Visible;
            }
            else
            {
                UpdateInfoBar.Title = "已经是最新版本";
                UpdateInfoBar.Message =
                    $"当前版本 {result.CurrentVersion}，远程版本 {result.LatestVersion}。";
                UpdateInfoBar.Severity = InfoBarSeverity.Success;
            }
            UpdateInfoBar.IsOpen = true;
        }
        catch (Exception exception) when (
            exception is HttpRequestException or TaskCanceledException or JsonException)
        {
            UpdateInfoBar.Title = "检查更新失败";
            UpdateInfoBar.Message = exception is TaskCanceledException
                ? "连接 GitHub 超时，请稍后重试。"
                : exception.Message;
            UpdateInfoBar.Severity = InfoBarSeverity.Error;
            UpdateInfoBar.IsOpen = true;
            OpenReleaseButton.Visibility = Visibility.Visible;
        }
        finally
        {
            UpdateProgressRing.IsActive = false;
            UpdateProgressRing.Visibility = Visibility.Collapsed;
            UpdateButtonIcon.Visibility = Visibility.Visible;
            CheckUpdatesButton.IsEnabled = true;
        }
    }

    private void OpenReleaseButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(_latestReleaseUrl);

    private void OpenRepositoryButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(UpdateService.RepositoryUrl);

    private void OpenAllReleasesButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(UpdateService.ReleasesUrl);

    private void OpenLicenseButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell($"{UpdateService.RepositoryUrl}/blob/main/LICENSE");

    private void OpenBilibiliButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(BilibiliProfileUrl);

    private void OpenXiaoheiheButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(XiaoheiheProfileUrl);

    private void CopyQqGroupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var package = new DataPackage();
            package.SetText(QqGroupNumber);
            Clipboard.SetContent(package);
            Clipboard.Flush();
            ShowStatus("QQ群号已复制", QqGroupNumber, InfoBarSeverity.Success);
        }
        catch (COMException exception)
        {
            ShowStatus("复制QQ群号失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void ViewDisclaimerButton_Click(object sender, RoutedEventArgs e) =>
        await ShowDisclaimerAsync(requireAcceptance: false);

    private async Task<bool> SaveAsync(bool showSuccess)
    {
        string mapperPath = MapperPathBox.Text.Trim();
        if (!File.Exists(mapperPath))
        {
            ShowStatus("注入器路径无效", "请选择有效的 Il2cppDumper.exe。", InfoBarSeverity.Error);
            return false;
        }

        if (!TryReadConfiguration(out ModConfiguration configuration, out string? error))
        {
            ShowStatus("参数无效", error ?? "请检查输入值。", InfoBarSeverity.Error);
            return false;
        }

        if (!TryValidateExternalLoader(out error))
        {
            ShowStatus("外部加载器设置无效", error ?? "请检查加载器设置。", InfoBarSeverity.Error);
            return false;
        }

        try
        {
            await ConfigurationService.SaveModConfigurationAsync(mapperPath, configuration);
            _appSettings.GameExecutablePath = GamePathBox.Text.Trim();
            _appSettings.MapperPath = mapperPath;
            _appSettings.ExternalLoaderEnabled = ExternalLoaderToggle.IsOn;
            _appSettings.ExternalLoaderPath = ExternalLoaderPathBox.Text.Trim();
            _appSettings.ExternalLoaderArguments = ExternalLoaderArgumentsBox.Text.Trim();
            _appSettings.ExternalLoaderDelaySeconds = ExternalLoaderDelayNumberBox.Value;
            _appSettings.Theme = GetSelectedTheme();
            await ConfigurationService.SaveAppSettingsAsync(_appSettings);

            ConfigurationPathTextBlock.Text =
                ConfigurationService.GetNativeConfigurationPath(mapperPath);
            if (showSuccess)
            {
                ShowStatus(
                    "参数已保存",
                    "语言配置会在约 2 秒内热更新；模型与动画配置将在下次启动并注入时读取。",
                    InfoBarSeverity.Success);
            }

            return true;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("保存失败", exception.Message, InfoBarSeverity.Error);
            return false;
        }
    }

    private bool TryValidateExternalLoader(out string? error)
    {
        error = null;
        double delay = ExternalLoaderDelayNumberBox.Value;
        if (!double.IsFinite(delay) || delay < 0 || delay > 30)
        {
            error = "加载器等待时间必须在 0 到 30 秒之间。";
            return false;
        }

        if (!ExternalLoaderToggle.IsOn)
        {
            return true;
        }

        string loaderPath = ExternalLoaderPathBox.Text.Trim();
        if (!File.Exists(loaderPath))
        {
            error = "请选择有效的外部加载器程序。";
            return false;
        }

        if (PathsEqual(loaderPath, MapperPathBox.Text) ||
            PathsEqual(loaderPath, GamePathBox.Text))
        {
            error = "外部加载器不能与注入器或游戏程序使用同一路径。";
            return false;
        }

        return true;
    }

    private static bool PathsEqual(string first, string second)
    {
        if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(second))
        {
            return false;
        }

        try
        {
            return Path.GetFullPath(first.Trim()).Equals(
                Path.GetFullPath(second.Trim()),
                StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException or
                IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private async Task<bool> StartExternalLoaderAsync()
    {
        if (!ExternalLoaderToggle.IsOn)
        {
            return true;
        }

        string loaderPath = ExternalLoaderPathBox.Text.Trim();
        var startInfo = new ProcessStartInfo
        {
            FileName = loaderPath,
            WorkingDirectory = Path.GetDirectoryName(loaderPath) ?? string.Empty,
            Arguments = ExternalLoaderArgumentsBox.Text.Trim(),
            UseShellExecute = true
        };
        Process? loaderProcess = Process.Start(startInfo);
        if (loaderProcess is null)
        {
            throw new InvalidOperationException("外部加载器没有返回有效进程。");
        }

        loaderProcess.Dispose();
        double delay = ExternalLoaderDelayNumberBox.Value;
        if (delay > 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }

        if (Process.GetProcessesByName("Endfield").Length == 0)
        {
            return true;
        }

        ShowStatus(
            "外部加载器已经启动游戏",
            "EF 无法附加到现有进程。请完整退出游戏，并关闭外部加载器的自动启动游戏功能。",
            InfoBarSeverity.Warning);
        return false;
    }

    private bool TryReadConfiguration(
        out ModConfiguration configuration,
        out string? error)
    {
        configuration = ModConfiguration.CreateDefaults();
        error = null;
        if (CharacterComboBox.SelectedItem is not CharacterOption character ||
            FinalActionComboBox.SelectedItem is not ActionOption action)
        {
            error = "请选择角色和最后一段动作。";
            return false;
        }

        NumberBox[] numberBoxes =
        [
            StartYawNumberBox,
            TurnDurationNumberBox,
            ScaleNumberBox,
            LeanSampleNumberBox,
            SitLoopSpeedNumberBox,
            SitSpecialSpeedNumberBox,
            SitToWalkSpeedNumberBox,
            FinalSpeedNumberBox,
            LoopStartNumberBox,
            LoopEndNumberBox,
            CrossfadeDurationNumberBox
        ];
        if (numberBoxes.Any(box => !double.IsFinite(box.Value)))
        {
            error = "所有数值输入框都必须填写有效数字。";
            return false;
        }

        if (CrossfadeToggle.IsOn && LoopEndNumberBox.Value <= LoopStartNumberBox.Value + 0.05)
        {
            error = "人工循环终点必须至少比起点晚 0.05 秒。";
            return false;
        }

        double loopDuration = LoopEndNumberBox.Value - LoopStartNumberBox.Value;
        if (CrossfadeToggle.IsOn && CrossfadeDurationNumberBox.Value > loopDuration * 0.5)
        {
            error = "交叉混合时长不能超过循环区间的一半。";
            return false;
        }

        if (!TryNormalizeVoiceLanguageRules(
            SerializeVoiceRules(),
            out string voiceLanguageRules,
            out error))
        {
            return false;
        }

        configuration = new ModConfiguration
        {
            Character = character.Id,
            FinalAction = action.Id,
            ModelPath = character.Model.Path,
            ModelPathHash = character.Model.PathHash,
            ModelBundleHash = character.Model.BundleHash,
            SitLoopPath = character.SitLoop.Path,
            SitLoopPathHash = character.SitLoop.PathHash,
            SitLoopLabel = character.SitLoop.DisplayName,
            SitSpecialPath = character.SitSpecial.Path,
            SitSpecialPathHash = character.SitSpecial.PathHash,
            SitSpecialLabel = character.SitSpecial.DisplayName,
            SitToWalkPath = character.SitToWalk.Path,
            SitToWalkPathHash = character.SitToWalk.PathHash,
            SitToWalkLabel = character.SitToWalk.DisplayName,
            FinalPath = action.Path,
            FinalPathHash = action.PathHash,
            FinalLabel = action.Id,
            FinalNativeLoop = action.NativeLoop,
            StartYaw = StartYawNumberBox.Value,
            TurnDuration = TurnDurationNumberBox.Value,
            Scale = ScaleNumberBox.Value,
            ForwardLeanSample = LeanSampleNumberBox.Value,
            SitLoopSpeed = SitLoopSpeedNumberBox.Value,
            SitSpecialSpeed = SitSpecialSpeedNumberBox.Value,
            SitToWalkSpeed = SitToWalkSpeedNumberBox.Value,
            FinalSpeed = FinalSpeedNumberBox.Value,
            FinalLoop = FinalLoopToggle.IsOn,
            ForceLoop = ForceLoopToggle.IsOn,
            UseCrossfade = CrossfadeToggle.IsOn,
            LoopStart = LoopStartNumberBox.Value,
            LoopEnd = LoopEndNumberBox.Value,
            CrossfadeDuration = CrossfadeDurationNumberBox.Value,
            ModelReplacementEnabled = ModelReplacementToggle.IsOn,
            VoiceRouterEnabled = VoiceRouterToggle.IsOn,
            ReplaceNarrativeVoice = NarrativeVoiceToggle.IsOn,
            VoiceLanguageRules = voiceLanguageRules
        };
        return true;
    }

    private void ApplyConfiguration(ModConfiguration configuration)
    {
        bool wasInitializing = _initializing;
        _initializing = true;

        string characterId = PresetOptions.NormalizeCharacterId(configuration.Character);
        string actionId = PresetOptions.NormalizeActionId(configuration.FinalAction);
        CharacterComboBox.SelectedItem = PresetOptions.Characters.FirstOrDefault(
            character => character.Id.Equals(
                characterId,
                StringComparison.OrdinalIgnoreCase)) ??
            PresetOptions.Characters.First(character =>
                character.Id == "chr_0013_aglina");
        RefreshActionOptions(actionId);

        StartYawNumberBox.Value = configuration.StartYaw;
        TurnDurationNumberBox.Value = configuration.TurnDuration;
        ScaleNumberBox.Value = configuration.Scale;
        LeanSampleNumberBox.Value = configuration.ForwardLeanSample;
        SitLoopSpeedNumberBox.Value = configuration.SitLoopSpeed;
        SitSpecialSpeedNumberBox.Value = configuration.SitSpecialSpeed;
        SitToWalkSpeedNumberBox.Value = configuration.SitToWalkSpeed;
        FinalSpeedNumberBox.Value = configuration.FinalSpeed;
        FinalLoopToggle.IsOn = configuration.FinalLoop;
        ForceLoopToggle.IsOn = configuration.ForceLoop;
        CrossfadeToggle.IsOn = configuration.UseCrossfade;
        LoopStartNumberBox.Value = configuration.LoopStart;
        LoopEndNumberBox.Value = configuration.LoopEnd;
        CrossfadeDurationNumberBox.Value = configuration.CrossfadeDuration;
        ModelReplacementToggle.IsOn = configuration.ModelReplacementEnabled;
        VoiceRouterToggle.IsOn = configuration.VoiceRouterEnabled;
        NarrativeVoiceToggle.IsOn = configuration.ReplaceNarrativeVoice;
        LoadVoiceRules(configuration.VoiceLanguageRules);

        _initializing = wasInitializing;
        UpdateCrossfadePanel();
    }

    private void RefreshActionOptions(string? preferredAction)
    {
        if (CharacterComboBox.SelectedItem is not CharacterOption character)
        {
            FinalActionComboBox.ItemsSource = null;
            PresetDetailsTextBlock.Text = string.Empty;
            return;
        }

        IReadOnlyList<ActionOption> actions = character.Actions;
        FinalActionComboBox.ItemsSource = actions;
        FinalActionComboBox.SelectedItem = actions.FirstOrDefault(action =>
            action.Id.Equals(preferredAction, StringComparison.OrdinalIgnoreCase)) ??
            actions.FirstOrDefault(action =>
                action.Id.Equals(
                    character.DefaultActionId,
                    StringComparison.OrdinalIgnoreCase)) ??
            actions.FirstOrDefault();
        PresetDetailsTextBlock.Text =
            $"Sit loop: {character.SitLoop.DisplayName}\n" +
            $"Sit special: {character.SitSpecial.DisplayName}\n" +
            $"Sit to walk: {character.SitToWalk.DisplayName}";
    }

    private void UpdateCrossfadePanel()
    {
        bool enabled = CrossfadeToggle.IsOn;
        CrossfadeSettingsPanel.Opacity = enabled ? 1.0 : 0.55;
        LoopStartNumberBox.IsEnabled = enabled;
        LoopEndNumberBox.IsEnabled = enabled;
        CrossfadeDurationNumberBox.IsEnabled = enabled;
    }

    private static bool TryNormalizeVoiceLanguageRules(
        string source,
        out string normalized,
        out string? error)
    {
        normalized = string.Empty;
        error = null;
        string[] entries = source.Split(
            ['\r', '\n', ',', ';'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var rules = new List<string>(entries.Length);
        foreach (string entry in entries)
        {
            int equals = entry.IndexOf('=');
            int colon = entry.IndexOf(':');
            int separator = equals >= 0 && colon >= 0
                ? Math.Min(equals, colon)
                : Math.Max(equals, colon);
            if (separator <= 0 || separator >= entry.Length - 1)
            {
                error = $"配音规则“{entry}”格式错误，应为 speakerChannel=Japanese。";
                return false;
            }

            string speaker = entry[..separator].Trim();
            string language = entry[(separator + 1)..].Trim();
            if (speaker != "*" && speaker.Any(character =>
                !char.IsAsciiLetterOrDigit(character) &&
                character is not '_' and not '-' and not '.'))
            {
                error = $"speakerChannel“{speaker}”只能包含英文字母、数字、_、- 或 .。";
                return false;
            }
            if (!VoiceLanguageNames.TryGetValue(language, out string? canonicalLanguage))
            {
                error = $"不支持配音语言“{language}”。";
                return false;
            }

            rules.Add($"{speaker}={canonicalLanguage}");
        }

        normalized = string.Join(Environment.NewLine, rules);
        return true;
    }

    private static string GetCharacterSpeakerAlias(string characterId)
    {
        if (characterId.StartsWith("chr_", StringComparison.OrdinalIgnoreCase))
        {
            int separator = characterId.IndexOf('_', 4);
            if (separator >= 0 && separator + 1 < characterId.Length)
            {
                return characterId[(separator + 1)..];
            }
        }
        return characterId;
    }

    private void SelectVoiceLanguage(string language)
    {
        string canonical = VoiceLanguageNames.TryGetValue(language, out string? value)
            ? value
            : "FollowGlobal";
        foreach (RadioButton button in VoiceLanguageRadioButtons.Items.OfType<RadioButton>())
        {
            button.IsChecked = string.Equals(
                button.Tag as string,
                canonical,
                StringComparison.OrdinalIgnoreCase);
        }
    }

    private string? GetSelectedVoiceLanguage()
    {
        RadioButton? selected = VoiceLanguageRadioButtons.Items
            .OfType<RadioButton>()
            .FirstOrDefault(button => button.IsChecked == true);
        return selected?.Tag as string;
    }

    private void UpsertVoiceRule(string speaker, string language, string displayName)
    {
        string canonical = VoiceLanguageNames.TryGetValue(language, out string? value)
            ? value
            : "FollowGlobal";
        var replacement = new VoiceRuleEntry
        {
            Speaker = speaker,
            DisplayName = displayName,
            Language = canonical,
            LanguageDisplayName = GetVoiceLanguageDisplayName(canonical)
        };
        int existingIndex = -1;
        for (int index = 0; index < _voiceRules.Count; index++)
        {
            if (_voiceRules[index].Speaker.Equals(
                speaker,
                StringComparison.OrdinalIgnoreCase))
            {
                existingIndex = index;
                break;
            }
        }
        if (existingIndex >= 0)
        {
            _voiceRules[existingIndex] = replacement;
        }
        else
        {
            _voiceRules.Add(replacement);
        }
        UpdateVoiceRulesEmptyState();
    }

    private string GetVoiceLanguageDisplayName(string language) =>
        VoiceLanguageDisplayNames.TryGetValue(language, out string? displayName)
            ? displayName
            : language;

    private void UpdateVoiceRulesEmptyState()
    {
        VoiceRulesEmptyTextBlock.Visibility = _voiceRules.Count == 0
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void LoadVoiceRules(string rules)
    {
        _voiceRules.Clear();
        if (!TryNormalizeVoiceLanguageRules(rules, out string normalized, out _))
        {
            UpdateVoiceRulesEmptyState();
            return;
        }

        foreach (string entry in normalized.Split(
            ['\r', '\n'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            int separator = entry.IndexOf('=');
            if (separator <= 0 || separator >= entry.Length - 1)
            {
                continue;
            }

            string speaker = entry[..separator].Trim();
            string language = entry[(separator + 1)..].Trim();
            string displayName = _voiceCharacters.FirstOrDefault(choice =>
                choice.Speaker.Equals(speaker, StringComparison.OrdinalIgnoreCase))?.DisplayName
                ?? (speaker == "*" ? "其他角色（默认规则）" : speaker);
            UpsertVoiceRule(speaker, language, displayName);
        }
        UpdateVoiceRulesEmptyState();
    }

    private string SerializeVoiceRules() => string.Join(
        Environment.NewLine,
        _voiceRules.Select(rule => $"{rule.Speaker}={rule.Language}"));

    private void SelectTheme(string theme)
    {
        string normalized = theme is "Light" or "Dark" ? theme : "Default";
        ThemeComboBox.SelectedItem = ThemeComboBox.Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(item => string.Equals(
                item.Tag as string,
                normalized,
                StringComparison.OrdinalIgnoreCase)) ??
            ThemeComboBox.Items.OfType<ComboBoxItem>().First();
    }

    private string GetSelectedTheme() =>
        (ThemeComboBox.SelectedItem as ComboBoxItem)?.Tag as string ?? "Default";

    private void ApplyTheme(string theme)
    {
        MainRoot.RequestedTheme = theme switch
        {
            "Light" => ElementTheme.Light,
            "Dark" => ElementTheme.Dark,
            _ => ElementTheme.Default
        };
    }

    private async Task<bool> ShowDisclaimerAsync(bool requireAcceptance)
    {
        var content = new StackPanel
        {
            Spacing = 12,
            MaxWidth = 560
        };
        content.Children.Add(new TextBlock
        {
            Text = "本软件会将本机代码注入游戏进程，并在运行时修改模型、动画和语音资源选择。",
            TextWrapping = TextWrapping.Wrap,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
        });
        content.Children.Add(new TextBlock
        {
            Text = "可能的风险包括游戏崩溃、存档或配置异常、更新后失效，以及被游戏安全或反作弊系统识别。使用在线账号可能产生账号限制风险。",
            TextWrapping = TextWrapping.Wrap
        });
        content.Children.Add(new TextBlock
        {
            Text = "本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关，也不提供任何形式的担保。请自行备份重要数据，遵守游戏服务条款，并自行承担使用后果。",
            TextWrapping = TextWrapping.Wrap
        });
        content.Children.Add(new TextBlock
        {
            Text = "本软件不负责停用或绕过反作弊组件；游戏更新后如签名不匹配，相关 Hook 应停止使用，等待适配。",
            TextWrapping = TextWrapping.Wrap,
            Foreground = (Brush)Application.Current.Resources[
                "TextFillColorSecondaryBrush"]
        });

        var dialog = new ContentDialog
        {
            XamlRoot = MainRoot.XamlRoot,
            Title = requireAcceptance ? "使用前请阅读" : "风险与免责声明",
            Content = content,
            DefaultButton = requireAcceptance
                ? ContentDialogButton.Primary
                : ContentDialogButton.Close,
            PrimaryButtonText = requireAcceptance ? "我已了解并继续" : string.Empty,
            CloseButtonText = requireAcceptance ? "退出" : "关闭"
        };
        ContentDialogResult result = await dialog.ShowAsync();
        return !requireAcceptance || result == ContentDialogResult.Primary;
    }

    private async Task<string?> PickExecutableAsync(string commitButtonText)
    {
        var picker = new FileOpenPicker
        {
            CommitButtonText = commitButtonText,
            SuggestedStartLocation = PickerLocationId.ComputerFolder
        };
        picker.FileTypeFilter.Add(".exe");
        WinRT.Interop.InitializeWithWindow.Initialize(
            picker,
            WinRT.Interop.WindowNative.GetWindowHandle(this));
        Windows.Storage.StorageFile? file = await picker.PickSingleFileAsync();
        return file?.Path;
    }

    private void StatusTimer_Tick(object? sender, object e) => RefreshRuntimeStatus();

    private void RefreshRuntimeStatus()
    {
        RefreshRuntimeAnimationDurations();
        bool gameRunning = Process.GetProcessesByName("Endfield").Length > 0;
        RuntimeStatusTextBlock.Text = gameRunning ? "游戏正在运行" : "游戏未运行";
        RuntimeStatusIndicator.Fill = gameRunning
            ? new SolidColorBrush(Microsoft.UI.Colors.LimeGreen)
            : (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];

        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (File.Exists(logPath))
        {
            DateTime updated = File.GetLastWriteTime(logPath);
            LogStatusTextBlock.Text = $"日志更新：{updated:MM-dd HH:mm:ss}";
        }
        else
        {
            LogStatusTextBlock.Text = File.Exists(MapperPathBox.Text.Trim())
                ? "注入器已就绪 · 尚无日志"
                : "未找到注入器";
        }
    }

    private void RefreshRuntimeAnimationDurations()
    {
        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (!logPath.Equals(_durationLogPath, StringComparison.OrdinalIgnoreCase))
        {
            _durationLogPath = logPath;
            _durationLogWriteUtc = DateTime.MinValue;
        }
        if (!File.Exists(logPath))
        {
            return;
        }

        DateTime writeUtc = File.GetLastWriteTimeUtc(logPath);
        if (writeUtc <= _durationLogWriteUtc)
        {
            return;
        }

        try
        {
            var actionsById = PresetOptions.Characters
                .SelectMany(character => character.Actions)
                .GroupBy(action => action.Id, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToArray(),
                    StringComparer.OrdinalIgnoreCase);
            foreach (string line in File.ReadLines(logPath))
            {
                Match match = RuntimeClipLengthPattern.Match(line);
                if (!match.Success ||
                    !double.TryParse(
                        match.Groups["length"].Value,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double length) ||
                    length <= 0.0 ||
                    !actionsById.TryGetValue(
                        match.Groups["label"].Value,
                        out ActionOption[]? actions))
                {
                    continue;
                }

                foreach (ActionOption action in actions)
                {
                    action.Duration = length;
                }
            }

            _durationLogWriteUtc = writeUtc;
            if (FinalActionComboBox.SelectedItem is ActionOption selectedAction)
            {
                UpdateActionMetadata(selectedAction);
            }
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
        }
    }

    private void UpdateActionMetadata(ActionOption action)
    {
        ActionDurationTextBlock.Text = action.Duration is double seconds
            ? $"动画原始时长：{seconds.ToString("0.###", CultureInfo.InvariantCulture)} 秒"
            : "动画原始时长：未知（首次播放后自动读取）";
    }

    private void ShowStatus(string title, string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = title;
        StatusInfoBar.Message = message;
        StatusInfoBar.Severity = severity;
        StatusInfoBar.IsOpen = true;
    }

    private static void OpenWithShell(string path)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true
        });
    }

    private void TryResizeWindow()
    {
        try
        {
            nint windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId windowId =
                Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow.GetFromWindowId(windowId).Resize(new SizeInt32(1180, 860));
        }
        catch (InvalidOperationException)
        {
        }
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _statusTimer.Stop();
    }
}
