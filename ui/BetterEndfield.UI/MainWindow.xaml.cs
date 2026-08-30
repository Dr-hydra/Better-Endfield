using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using Windows.Storage.Pickers;
using Windows.UI;

namespace BetterEndfield.UI;

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
    private const string WindowIconResourceName =
        "BetterEndfield.UI.Assets.shared.gilberta.ico";
    private const string BilibiliProfileUrl = "https://space.bilibili.com/441133155";
    private const string CombatAnalysisUrl =
        "https://www.bilibili.com/toy/endfield";
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
    private readonly ObservableCollection<CombatSessionRecord> _combatSessions = [];
    private readonly ObservableCollection<CombatCharacterFilterChoice> _combatCharacterFilters = [];
    private readonly ObservableCollection<CombatLegendItem> _combatCategoryLegend = [];
    private IReadOnlyList<CombatSessionRecord> _allCombatSessions = [];
    private CombatSessionRecord? _selectedCombatSession;
    private bool _combatHistoryExpanded;
    private bool _updatingCombatHistory;
    private IReadOnlyList<VoiceCharacterChoice> _voiceCharacters = [];
    private AppSettings _appSettings = new();
    private string _latestReleaseUrl = UpdateService.ReleasesUrl;
    private bool _pathScanRunning;
    private int _xInputStatusRevision;

    public MainWindow()
    {
        InitializeComponent();
        Title = "Better Endfield";
        SystemBackdrop = new MicaBackdrop();
        TrySetWindowIcon();
        TryResizeWindow();
        CurrentVersionTextBlock.Text = $"版本 {UpdateService.CurrentVersion}";
        if (FeatureNavigation.SettingsItem is NavigationViewItem settingsItem)
        {
            settingsItem.Content = "设置";
        }

        CharacterComboBox.ItemsSource = PresetOptions.Characters;
        _voiceCharacters = BuildVoiceCharacterChoices();
        VoiceCharacterComboBox.ItemsSource = _voiceCharacters;
        VoiceCharacterComboBox.SelectedIndex = 0;
        VoiceRulesListView.ItemsSource = _voiceRules;
        CombatSessionsListView.ItemsSource = _combatSessions;
        foreach (ComboBox comboBox in CombatCharacterFilterBoxes())
            comboBox.ItemsSource = _combatCharacterFilters;
        CombatCategoryLegendItemsControl.ItemsSource = _combatCategoryLegend;
        UpdateCombatCategoryLegend();
        SelectVoiceLanguage("Japanese");
        UpdateVoiceRulesEmptyState();
        UpdateLocalizedUI();
        _statusTimer.Tick += StatusTimer_Tick;
        Closed += MainWindow_Closed;
    }

    private async void MainRoot_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _appSettings = await ConfigurationService.LoadAppSettingsAsync();
            SelectLanguage(_appSettings.Language);
            LocalizationService.Instance.ApplyLanguage(_appSettings.Language);
            UpdateLocalizedUI();
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

            GamePathBox.Text = _appSettings.GameExecutablePath;
            InjectorPathBox.Text = _appSettings.InjectorPath;
            GameLaunchArgumentsBox.Text = _appSettings.GameLaunchArguments;
            SelectLoaderMode(_appSettings.LoaderMode);
            await ScanRuntimePathsAsync(showResult: false);
            RefreshRuntimeAnimationDurations();

            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(InjectorPathBox.Text);
            ApplyConfiguration(configuration);
            RefreshCombatSessions();
            _initializing = false;
            UpdateCrossfadePanel();
            UpdateLoaderModePanel();
            UpdatePathStatusText();
            await RefreshXInputStatusAsync();
            await InitializeDisplayPageAsync();
            RefreshRuntimeStatus();
            _statusTimer.Start();
        }
        catch (Exception exception)
        {
            _initializing = false;
            try
            {
                string logPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "BetterEndfield", "crash.log");
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)!);
                System.IO.File.WriteAllText(logPath, $"MainRoot_Loaded Exception: {exception.Message}\r\n{exception}\r\n{exception.StackTrace}");
            }
            catch { }
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void BrowseGameButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 Endfield.exe");
        if (selectedPath is not null)
        {
            GamePathBox.Text = selectedPath;
            await RefreshXInputStatusAsync();
        }
    }

    private async void BrowseInjectorButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 BetterEndfield.Injector.exe");
        if (selectedPath is null)
        {
            return;
        }

        InjectorPathBox.Text = selectedPath;
        try
        {
            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(selectedPath);
            ApplyConfiguration(configuration);
            await RefreshXInputStatusAsync();
        }
        catch (IOException exception)
        {
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private void InjectorPathBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ConfigurationPathTextBlock.Text =
            ConfigurationService.GetNativeConfigurationPath(InjectorPathBox.Text);
        RefreshRuntimeAnimationDurations();
        RefreshRuntimeStatus();
        UpdatePathStatusText();
        _ = RefreshXInputStatusAsync();
    }

    private void GamePathBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdatePathStatusText();
        _ = RefreshXInputStatusAsync();
    }

    private void SelectLoaderMode(string mode)
    {
        bool useXInput = mode.Equals("xinput", StringComparison.OrdinalIgnoreCase) ||
            mode.Equals("version", StringComparison.OrdinalIgnoreCase);
        XInputModeRadioButton.IsChecked = useXInput;
        InjectorModeRadioButton.IsChecked = !useXInput;
    }

    private string GetSelectedLoaderMode() =>
        XInputModeRadioButton.IsChecked == true ? "xinput" : "injector";

    private void LoaderModeRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (!_initializing)
        {
            UpdateLoaderModePanel();
            _ = RefreshXInputStatusAsync();
        }
    }

    private async void ScanPathsButton_Click(object sender, RoutedEventArgs e) =>
        await ScanRuntimePathsAsync(showResult: true);

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

        bool isZh = LocalizationService.Instance.IsChinese;
        UpdateActionMetadata(action);
        ActionDescriptionTextBlock.Text = isZh
            ? $"资源哈希：{action.PathHash} · 原生 LoopTime：{(action.NativeLoop ? "是" : "否")}"
            : $"Asset Hash: {action.PathHash} · Native LoopTime: {(action.NativeLoop ? "Yes" : "No")}";
        if (!_initializing)
        {
            FinalLoopToggle.IsOn = action.NativeLoop;
            ForceLoopToggle.IsOn = false;
            CrossfadeToggle.IsOn = false;
        }
    }

    private void CrossfadeToggle_Toggled(object sender, RoutedEventArgs e) =>
        UpdateCrossfadePanel();

    private async void UiEnhancementToggle_Toggled(object sender, RoutedEventArgs e)
    {
        await SaveUiEnhancementAsync();
    }

    private async void HideHudHotkeyBox_LostFocus(object sender, RoutedEventArgs e)
    {
        await SaveUiEnhancementAsync();
    }

    private async Task SaveUiEnhancementAsync()
    {
        if (_initializing)
        {
            return;
        }

        if (!TryNormalizeCameraHotkey(
                HideHudHotkeyBox.Text, out string hideHudHotkey))
        {
            ShowStatus(
                "HUD 热键无效",
                "请输入单个字母、数字、F1-F24，或 NUMPAD0-NUMPAD9。",
                InfoBarSeverity.Error);
            return;
        }
        HideHudHotkeyBox.Text = hideHudHotkey;

        try
        {
            await ConfigurationService.SaveUiEnhancementConfigurationAsync(
                MobileUiToggle.IsOn,
                HideUidToggle.IsOn,
                HideHudToggle.IsOn,
                hideHudHotkey);
            ShowStatus(
                "界面增强设置已更新",
                "配置已保存；游戏已注入时会在约 0.5 秒内刷新，否则于下次注入时应用。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("界面模式保存失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void CameraEnhancementToggle_Toggled(object sender, RoutedEventArgs e)
    {
        await SaveCameraEnhancementAsync();
    }

    private async void CameraEnhancementNumberBox_ValueChanged(
        NumberBox sender,
        NumberBoxValueChangedEventArgs args)
    {
        await SaveCameraEnhancementAsync();
    }

    private async void FreeCameraHotkeyBox_LostFocus(
        object sender,
        RoutedEventArgs e)
    {
        await SaveCameraEnhancementAsync();
    }

    private async Task SaveCameraEnhancementAsync()
    {
        if (_initializing)
        {
            return;
        }

        static double Value(NumberBox numberBox, double fallback) =>
            double.IsFinite(numberBox.Value) ? numberBox.Value : fallback;

        if (!TryNormalizeCameraHotkey(
                FreeCameraHotkeyBox.Text, out string toggleHotkey))
        {
            ShowStatus(
                "相机热键无效",
                "请输入单个字母、数字、F1-F24，或 NUMPAD0-NUMPAD9。",
                InfoBarSeverity.Error);
            return;
        }
        FreeCameraHotkeyBox.Text = toggleHotkey;

        try
        {
            await ConfigurationService.SaveCameraEnhancementConfigurationAsync(
                FreeCameraToggle.IsOn,
                DisableDitherToggle.IsOn,
                PauseGameInFreeCameraToggle.IsOn,
                toggleHotkey,
                Value(FreeCameraMovementSpeedNumberBox, 5.0),
                Value(FreeCameraFieldOfViewNumberBox, 60.0));
            ShowStatus(
                "相机增强设置已更新",
                $"配置已保存；模块加载后可在游戏内按 {toggleHotkey} 切换自由视角。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("相机增强设置保存失败", exception.Message, InfoBarSeverity.Error);
        }
    }

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
        MusicPageScrollViewer.Visibility = page == "music"
            ? Visibility.Visible
            : Visibility.Collapsed;
        CombatPageScrollViewer.Visibility = page == "combat"
            ? Visibility.Visible
            : Visibility.Collapsed;
        UiPageScrollViewer.Visibility = page == "ui"
            ? Visibility.Visible
            : Visibility.Collapsed;
        CameraPageScrollViewer.Visibility = page == "camera"
            ? Visibility.Visible
            : Visibility.Collapsed;
        DisplayPageScrollViewer.Visibility = page == "display"
            ? Visibility.Visible
            : Visibility.Collapsed;
        if (page == "display")
        {
            _ = RefreshDisplayStatusAsync();
        }
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
            "music" => "首次启用在下一次注入时加载；已加载模块的设置会热更新。",
            "combat" => "F11 切换记录，F12 切换悬浮窗；结果会保存到本机目录。",
            "ui" => "界面模式设置将在保存后热更新或于下次注入时应用。",
            "camera" => "相机设置会立即保存；游戏内按配置的热键进入或退出自由视角。",
            "display" => "显示增强直接写入游戏目录，改动在下一次启动客户端时生效。",
            _ => "角色与动画参数保存后在下一次注入时生效。"
        };
    }

    private void RefreshCombatSessionsButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshCombatSessions();
    }

    private void CombatRdpsDisplayToggle_Toggled(object sender, RoutedEventArgs e)
    {
        if (_initializing) return;
        RefreshCombatSessions();
    }

    private void OpenCombatAnalysisWebButton_Click(object sender, RoutedEventArgs e) =>
        OpenWithShell(CombatAnalysisUrl);

    private void OpenCombatRecordsFolderButton_Click(object sender, RoutedEventArgs e)
    {
        string directory = Path.Combine(
            ConfigurationService.SettingsDirectory, "combat-sessions");
        try
        {
            Directory.CreateDirectory(directory);
            OpenWithShell(directory);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or Win32Exception)
        {
            ShowStatus("无法打开记录文件夹", exception.Message, InfoBarSeverity.Error);
        }
    }

    private void RefreshCombatSessions()
    {
        string directory = Path.Combine(
            ConfigurationService.SettingsDirectory, "combat-sessions");
        string? selectedPath = _selectedCombatSession?.Path;
        bool useRdps = CombatRdpsDisplayToggle.IsOn;
        _allCombatSessions = CombatHistoryService.Load(directory, useRdps);
        UpdateCombatCategoryLegend();
        _combatHistoryExpanded = false;
        RebuildCombatCharacterFilters();
        ApplyCombatFilters(selectedPath);
    }

    private void UpdateCombatCategoryLegend()
    {
        bool useRdps = CombatRdpsDisplayToggle.IsOn;
        int count = useRdps ? CombatRdpsCategories.Count : CombatSkillCategories.Count;
        _combatCategoryLegend.Clear();
        for (int category = 0; category < count; ++category)
        {
            _combatCategoryLegend.Add(new CombatLegendItem
            {
                Label = CombatHistoryService.CategoryName(category, useRdps),
                Brush = CombatHistoryService.CategoryBrush(category, useRdps)
            });
        }
    }

    private void RebuildCombatCharacterFilters()
    {
        _updatingCombatHistory = true;
        ComboBox[] filterBoxes = CombatCharacterFilterBoxes();
        string?[] selectedIds = filterBoxes.Select(comboBox =>
            (comboBox.SelectedItem as CombatCharacterFilterChoice)?.Id).ToArray();
        try
        {
            _combatCharacterFilters.Clear();
            _combatCharacterFilters.Add(new CombatCharacterFilterChoice
            {
                Id = null,
                DisplayName = LocalizationService.Instance.IsChinese ? "不限" : "Any"
            });
            foreach (CombatCharacterDamage character in _allCombatSessions
                .SelectMany(record => record.Characters)
                .Where(character => character.Id.StartsWith(
                    "chr_", StringComparison.OrdinalIgnoreCase))
                .GroupBy(character => character.Id, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.First())
                .OrderBy(character => character.DisplayName, StringComparer.CurrentCulture))
            {
                _combatCharacterFilters.Add(new CombatCharacterFilterChoice
                {
                    Id = character.Id,
                    DisplayName = character.DisplayName
                });
            }
            for (int index = 0; index < filterBoxes.Length; ++index)
            {
                string? selectedId = selectedIds[index];
                filterBoxes[index].SelectedItem = _combatCharacterFilters.FirstOrDefault(
                    choice => string.Equals(
                        choice.Id, selectedId, StringComparison.OrdinalIgnoreCase))
                    ?? _combatCharacterFilters[0];
            }
        }
        finally
        {
            _updatingCombatHistory = false;
        }
    }

    private ComboBox[] CombatCharacterFilterBoxes() =>
    [
        CombatCharacterFilter1ComboBox,
        CombatCharacterFilter2ComboBox,
        CombatCharacterFilter3ComboBox,
        CombatCharacterFilter4ComboBox
    ];

    private void ApplyCombatFilters(string? preferredPath = null)
    {
        DateTime? from = CombatFromDatePicker.Date?.LocalDateTime.Date;
        DateTime? to = CombatToDatePicker.Date?.LocalDateTime.Date;
        string[] characterIds = CombatCharacterFilterBoxes()
            .Select(comboBox =>
                (comboBox.SelectedItem as CombatCharacterFilterChoice)?.Id)
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Cast<string>()
            .ToArray();
        IEnumerable<CombatSessionRecord> filtered = _allCombatSessions;
        if (from.HasValue)
        {
            filtered = filtered.Where(record =>
                record.StartedAt.LocalDateTime.Date >= from.Value);
        }
        if (to.HasValue)
        {
            filtered = filtered.Where(record =>
                record.StartedAt.LocalDateTime.Date <= to.Value);
        }
        if (characterIds.Length > 0)
        {
            filtered = filtered.Where(record => characterIds.All(characterId =>
                record.Characters.Any(character => character.Id.Equals(
                    characterId, StringComparison.OrdinalIgnoreCase))));
        }

        CombatSessionRecord[] matches = filtered.ToArray();
        CombatSessionRecord[] visible = (_combatHistoryExpanded ? matches : matches.Take(3))
            .ToArray();
        _combatSessions.Clear();
        foreach (CombatSessionRecord record in visible) _combatSessions.Add(record);

        int hidden = Math.Max(0, matches.Length - 3);
        ExpandCombatSessionsButton.Visibility = hidden > 0
            ? Visibility.Visible
            : Visibility.Collapsed;
        bool isZh = LocalizationService.Instance.IsChinese;
        ExpandCombatSessionsButton.Content = _combatHistoryExpanded
            ? (isZh ? "收起，仅显示最近三条" : "Collapse to recent 3")
            : (isZh ? $"展开其余 {hidden} 条" : $"Expand remaining {hidden}");
        CombatSessionsEmptyTextBlock.Visibility = matches.Length == 0
            ? Visibility.Visible
            : Visibility.Collapsed;

        CombatSessionRecord? selected = visible.FirstOrDefault(record =>
            string.Equals(record.Path, preferredPath, StringComparison.OrdinalIgnoreCase))
            ?? visible.FirstOrDefault();
        CombatSessionsListView.SelectedItem = selected;
        UpdateCombatSessionDetail(selected);
    }

    private void CombatDateFilter_DateChanged(
        CalendarDatePicker sender,
        CalendarDatePickerDateChangedEventArgs args)
    {
        if (_initializing) return;
        _combatHistoryExpanded = false;
        ApplyCombatFilters();
    }

    private void CombatCharacterFilterComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (_initializing || _updatingCombatHistory) return;
        _combatHistoryExpanded = false;
        ApplyCombatFilters();
    }

    private void ExpandCombatSessionsButton_Click(object sender, RoutedEventArgs e)
    {
        _combatHistoryExpanded = !_combatHistoryExpanded;
        ApplyCombatFilters(_selectedCombatSession?.Path);
    }

    private void CombatSessionsListView_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        UpdateCombatSessionDetail(CombatSessionsListView.SelectedItem as CombatSessionRecord);
    }

    private async void DeleteCombatSessionButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: CombatSessionRecord record }) return;
        var dialog = new ContentDialog
        {
            XamlRoot = MainRoot.XamlRoot,
            Title = "删除战斗记录？",
            Content = $"{record.DateText}\n{record.CharacterSummary}\n此操作会删除对应的本地 JSON 文件。",
            PrimaryButtonText = "删除",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Close
        };
        if (await dialog.ShowAsync() != ContentDialogResult.Primary) return;
        try
        {
            File.Delete(record.Path);
            RefreshCombatSessions();
            ShowStatus("记录已删除", record.FileName, InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("删除失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private void UpdateCombatSessionDetail(CombatSessionRecord? record)
    {
        _selectedCombatSession = record;
        CombatCharacterBreakdownListView.ItemsSource = record?.Characters;
        CombatBreakdownEmptyTextBlock.Visibility =
            record is null || record.Characters.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        CombatSelectedSessionTextBlock.Text = record is null
            ? "选择一条记录查看。"
            : $"{record.DateText} · 总伤害 {record.TotalDamageText} · {record.Summary}";
    }

#if false // 内置时间轴已弃用；详细分轨解析统一由网页提供。
    private void CombatTimelineCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        RenderCombatTimeline();
    }

    private void CombatDetailViewToggle_Click(object sender, RoutedEventArgs e)
    {
        bool showTimeline = ReferenceEquals(sender, CombatTimelineViewToggle);
        CombatRankingViewToggle.IsChecked = !showTimeline;
        CombatTimelineViewToggle.IsChecked = showTimeline;
        CombatRankingPanel.Visibility = showTimeline
            ? Visibility.Collapsed
            : Visibility.Visible;
        CombatTimelinePanel.Visibility = showTimeline
            ? Visibility.Visible
            : Visibility.Collapsed;
        if (showTimeline) DispatcherQueue.TryEnqueue(RenderCombatTimeline);
    }

    private void CombatTimelineGroupComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (CombatTimelineChartCanvas is null) return;
        RenderCombatTimeline();
    }

    private void CombatTimelineGroupComboBox_DropDownOpened(object sender, object e)
    {
        _combatTimelineComboScrollOffset = CombatPageScrollViewer.VerticalOffset;
    }

    private void CombatTimelineGroupComboBox_DropDownClosed(object sender, object e)
    {
        if (!_combatTimelineComboScrollOffset.HasValue) return;
        double offset = _combatTimelineComboScrollOffset.Value;
        _combatTimelineComboScrollOffset = null;
        DispatcherQueue.TryEnqueue(() =>
        {
            DispatcherQueue.TryEnqueue(() =>
                CombatPageScrollViewer.ChangeView(null, offset, null, true));
        });
    }

    private void CombatTimelinePanel_BringIntoViewRequested(
        UIElement sender,
        BringIntoViewRequestedEventArgs e)
    {
        e.Handled = true;
    }

    private void RenderCombatTimeline()
    {
        RenderCombatTimelineChart();
        RenderCombatTimelineRange();
    }

    private void RenderCombatTimelineChart()
    {
        Canvas canvas = CombatTimelineChartCanvas;
        canvas.Children.Clear();
        CombatSessionRecord? record = _selectedCombatSession;
        IReadOnlyList<CombatTimelineSeries> series = record is null
            ? []
            : BuildCombatTimelineSeries(record);
        UpdateCombatTimelineLegend(series);
        if (record is null || record.Timeline.Count == 0 || series.Count == 0 ||
            canvas.ActualWidth < 120 || canvas.ActualHeight < 100)
        {
            CombatTimelineEmptyTextBlock.Text =
                CombatTimelineGroupComboBox?.SelectedIndex == 1
                    ? "该记录没有按角色保存的时间轴数据。"
                    : "该记录没有技能分类时间轴数据。";
            CombatTimelineEmptyTextBlock.Visibility = Visibility.Visible;
            return;
        }
        CombatTimelineEmptyTextBlock.Visibility = Visibility.Collapsed;

        double duration = Math.Max(
            record.DurationSeconds,
            record.Timeline.Count == 0 ? 0 : record.Timeline.Max(point => point.Time) + 0.25);
        double rangeStart = duration * _timelineRangeStart;
        double rangeEnd = duration * _timelineRangeEnd;
        double rangeDuration = Math.Max(0.25, rangeEnd - rangeStart);
        const double left = 62;
        const double top = 16;
        const double right = 16;
        const double bottom = 34;
        double plotWidth = Math.Max(1, canvas.ActualWidth - left - right);
        double plotHeight = Math.Max(1, canvas.ActualHeight - top - bottom);
        int columns = Math.Clamp((int)(plotWidth / 8), 12, 120);
        var buckets = new double[columns, series.Count];
        foreach (CombatTimelinePoint point in record.Timeline)
        {
            if (point.Time < rangeStart || point.Time > rangeEnd) continue;
            int column = Math.Clamp(
                (int)((point.Time - rangeStart) / rangeDuration * columns),
                0,
                columns - 1);
            for (int seriesIndex = 0; seriesIndex < series.Count; ++seriesIndex)
            {
                buckets[column, seriesIndex] += Math.Max(
                    0, series[seriesIndex].Value(point));
            }
        }
        double maximum = 0;
        for (int column = 0; column < columns; ++column)
        {
            double total = 0;
            for (int seriesIndex = 0; seriesIndex < series.Count; ++seriesIndex)
                total += buckets[column, seriesIndex];
            maximum = Math.Max(maximum, total);
        }
        maximum = Math.Max(1, maximum);

        Brush gridBrush = new SolidColorBrush(ColorHelper.FromArgb(45, 150, 160, 178));
        Brush textBrush = new SolidColorBrush(ColorHelper.FromArgb(190, 174, 183, 199));
        for (int lineIndex = 0; lineIndex <= 4; ++lineIndex)
        {
            double y = top + plotHeight * lineIndex / 4;
            canvas.Children.Add(new Line
            {
                X1 = left,
                X2 = left + plotWidth,
                Y1 = y,
                Y2 = y,
                Stroke = gridBrush,
                StrokeThickness = 1
            });
            double value = maximum * (1 - lineIndex / 4.0);
            AddTimelineText(
                canvas,
                CombatNumberFormatter.Format(value),
                2,
                y - 9,
                54,
                textBrush,
                TextAlignment.Right);
        }

        double columnWidth = plotWidth / columns;
        for (int column = 0; column < columns; ++column)
        {
            double y = top + plotHeight;
            for (int seriesIndex = 0; seriesIndex < series.Count; ++seriesIndex)
            {
                double amount = buckets[column, seriesIndex];
                if (amount <= 0) continue;
                double height = plotHeight * amount / maximum;
                y -= height;
                var bar = new Rectangle
                {
                    Width = Math.Max(1, columnWidth - 1),
                    Height = Math.Max(0.5, height),
                    Fill = series[seriesIndex].Brush,
                    RadiusX = 1,
                    RadiusY = 1
                };
                ToolTipService.SetToolTip(
                    bar,
                    $"{series[seriesIndex].Label}：{CombatNumberFormatter.Format(amount)}");
                Canvas.SetLeft(bar, left + column * columnWidth);
                Canvas.SetTop(bar, y);
                canvas.Children.Add(bar);
            }
        }

        AddTimelineText(canvas, FormatTimelineTime(rangeStart), left, top + plotHeight + 8,
            80, textBrush, TextAlignment.Left);
        AddTimelineText(canvas, FormatTimelineTime((rangeStart + rangeEnd) * 0.5),
            left + plotWidth * 0.5 - 40, top + plotHeight + 8, 80, textBrush,
            TextAlignment.Center);
        AddTimelineText(canvas, FormatTimelineTime(rangeEnd), left + plotWidth - 80,
            top + plotHeight + 8, 80, textBrush, TextAlignment.Right);
    }

    private IReadOnlyList<CombatTimelineSeries> BuildCombatTimelineSeries(
        CombatSessionRecord record)
    {
        if (CombatTimelineGroupComboBox?.SelectedIndex != 1)
        {
            return Enumerable.Range(0, CombatSkillCategories.Count)
                .Select(category =>
                {
                    int captured = category;
                    return new CombatTimelineSeries(
                        CombatSkillCategories.Names[category],
                        CombatHistoryService.CategoryBrush(category),
                        point => point.DamageByCategory[captured]);
                })
                .ToArray();
        }

        Dictionary<string, double> totals = new(StringComparer.OrdinalIgnoreCase);
        foreach (CombatTimelinePoint point in record.Timeline)
        {
            foreach ((string id, double damage) in point.DamageByCharacter)
            {
                if (!id.StartsWith("chr_", StringComparison.OrdinalIgnoreCase)) continue;
                totals[id] = totals.GetValueOrDefault(id) + damage;
            }
        }
        return totals
            .OrderByDescending(entry => entry.Value)
            .Take(8)
            .Select((entry, index) =>
            {
                string id = entry.Key;
                string label = record.Characters.FirstOrDefault(character =>
                    character.Id.Equals(id, StringComparison.OrdinalIgnoreCase))?.DisplayName ?? id;
                return new CombatTimelineSeries(
                    label,
                    CombatHistoryService.CharacterBrush(index),
                    point => point.DamageByCharacter.TryGetValue(id, out double damage)
                        ? damage
                        : 0);
            })
            .ToArray();
    }

    private void UpdateCombatTimelineLegend(IReadOnlyList<CombatTimelineSeries> series)
    {
        if (_combatTimelineLegend.Count == series.Count &&
            _combatTimelineLegend.Select(item => item.Label)
                .SequenceEqual(series.Select(item => item.Label), StringComparer.Ordinal))
        {
            return;
        }
        _combatTimelineLegend.Clear();
        foreach (CombatTimelineSeries item in series)
        {
            _combatTimelineLegend.Add(new CombatLegendItem
            {
                Label = item.Label,
                Brush = item.Brush
            });
        }
    }

    private static void AddTimelineText(
        Canvas canvas,
        string text,
        double left,
        double top,
        double width,
        Brush brush,
        TextAlignment alignment)
    {
        var label = new TextBlock
        {
            Text = text,
            Width = width,
            FontSize = 11,
            Foreground = brush,
            TextAlignment = alignment
        };
        Canvas.SetLeft(label, left);
        Canvas.SetTop(label, top);
        canvas.Children.Add(label);
    }

    private void RenderCombatTimelineRange()
    {
        Canvas canvas = CombatTimelineRangeCanvas;
        canvas.Children.Clear();
        double width = canvas.ActualWidth;
        if (width < 40) return;
        const double margin = 12;
        const double center = 22;
        double trackWidth = width - margin * 2;
        double startX = margin + trackWidth * _timelineRangeStart;
        double endX = margin + trackWidth * _timelineRangeEnd;
        var track = new Rectangle
        {
            Width = trackWidth,
            Height = 4,
            Fill = new SolidColorBrush(ColorHelper.FromArgb(80, 150, 160, 178)),
            RadiusX = 2,
            RadiusY = 2
        };
        Canvas.SetLeft(track, margin);
        Canvas.SetTop(track, center - 2);
        canvas.Children.Add(track);
        var selection = new Rectangle
        {
            Width = Math.Max(0, endX - startX),
            Height = 6,
            Fill = new SolidColorBrush(ColorHelper.FromArgb(255, 67, 201, 255)),
            RadiusX = 3,
            RadiusY = 3
        };
        Canvas.SetLeft(selection, startX);
        Canvas.SetTop(selection, center - 3);
        canvas.Children.Add(selection);
        AddTimelineRangeHandle(canvas, startX, center);
        AddTimelineRangeHandle(canvas, endX, center);

        double duration = _selectedCombatSession?.DurationSeconds ?? 0;
        CombatTimelineRangeTextBlock.Text = duration <= 0
            ? "完整时间范围"
            : $"{FormatTimelineTime(duration * _timelineRangeStart)} – " +
              $"{FormatTimelineTime(duration * _timelineRangeEnd)}";
    }

    private static void AddTimelineRangeHandle(Canvas canvas, double x, double y)
    {
        var handle = new Ellipse
        {
            Width = 18,
            Height = 18,
            Fill = new SolidColorBrush(ColorHelper.FromArgb(255, 245, 248, 252)),
            Stroke = new SolidColorBrush(ColorHelper.FromArgb(255, 67, 201, 255)),
            StrokeThickness = 3
        };
        Canvas.SetLeft(handle, x - 9);
        Canvas.SetTop(handle, y - 9);
        canvas.Children.Add(handle);
    }

    private void CombatTimelineRangeCanvas_PointerPressed(
        object sender,
        PointerRoutedEventArgs e)
    {
        if (_selectedCombatSession?.Timeline.Count is null or 0) return;
        double normalized = TimelinePointerPosition(e);
        _timelineDraggingHandle =
            Math.Abs(normalized - _timelineRangeStart) <= Math.Abs(normalized - _timelineRangeEnd)
                ? 1
                : 2;
        CombatTimelineRangeCanvas.CapturePointer(e.Pointer);
        UpdateTimelineRange(normalized);
        e.Handled = true;
    }

    private void CombatTimelineRangeCanvas_PointerMoved(
        object sender,
        PointerRoutedEventArgs e)
    {
        if (_timelineDraggingHandle == 0) return;
        if (!e.GetCurrentPoint(CombatTimelineRangeCanvas).Properties.IsLeftButtonPressed)
        {
            ReleaseTimelinePointer(e);
            return;
        }
        UpdateTimelineRange(TimelinePointerPosition(e));
        e.Handled = true;
    }

    private void CombatTimelineRangeCanvas_PointerReleased(
        object sender,
        PointerRoutedEventArgs e)
    {
        ReleaseTimelinePointer(e);
        e.Handled = true;
    }

    private void ReleaseTimelinePointer(PointerRoutedEventArgs e)
    {
        if (_timelineDraggingHandle == 0) return;
        _timelineDraggingHandle = 0;
        CombatTimelineRangeCanvas.ReleasePointerCapture(e.Pointer);
    }

    private double TimelinePointerPosition(PointerRoutedEventArgs e)
    {
        const double margin = 12;
        double width = Math.Max(1, CombatTimelineRangeCanvas.ActualWidth - margin * 2);
        return Math.Clamp(
            (e.GetCurrentPoint(CombatTimelineRangeCanvas).Position.X - margin) / width,
            0,
            1);
    }

    private void UpdateTimelineRange(double value)
    {
        const double minimumSpan = 0.02;
        if (_timelineDraggingHandle == 1)
            _timelineRangeStart = Math.Clamp(value, 0, _timelineRangeEnd - minimumSpan);
        else if (_timelineDraggingHandle == 2)
            _timelineRangeEnd = Math.Clamp(value, _timelineRangeStart + minimumSpan, 1);
        RenderCombatTimeline();
    }

    private static string FormatTimelineTime(double seconds)
    {
        seconds = Math.Max(0, seconds);
        int minutes = (int)(seconds / 60);
        return minutes > 0
            ? $"{minutes}:{seconds % 60:00.0}"
            : $"{seconds:0.0}s";
    }
#endif

    private async void BrowseOmniMixBackendButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync(
            "选择 OmniMixPlayer.Backend.exe");
        if (selectedPath is null)
        {
            return;
        }

        try
        {
            OmniMixRegistrationStatus status =
                await OmniMixRegistrationService.RegisterAsync(selectedPath);
            ApplyOmniMixRegistration(status);
            ShowStatus(
                "OmniMix 后端已注册",
                "注册只保存路径；请保存配置后再启动游戏。",
                InfoBarSeverity.Success);
        }
        catch (OmniMixRegistrationException exception)
        {
            ShowStatus("OmniMix 后端无效", exception.Message, InfoBarSeverity.Error);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("OmniMix 注册失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void UnregisterOmniMixButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            OmniMixRegistrationStatus status =
                await OmniMixRegistrationService.UnregisterAsync();
            ApplyOmniMixRegistration(status);
            MusicReplacementToggle.IsOn = false;
            ShowStatus(
                "OmniMix 注册已清除",
                "未删除 OmniMix 或 Better Endfield 的任何程序文件。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("解除注册失败", exception.Message, InfoBarSeverity.Error);
        }
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
        bool isZh = LocalizationService.Instance.IsChinese;
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus(
                isZh ? "游戏已经在运行" : "Game Already Running",
                isZh ? "为避免重复注入，请完整退出游戏后再使用“保存并启动”。"
                     : "To avoid duplicate injection, please exit the game before using Save & Launch.",
                InfoBarSeverity.Warning);
            return;
        }

        if (!File.Exists(GamePathBox.Text.Trim()))
        {
            ShowStatus(
                isZh ? "游戏路径无效" : "Invalid Game Path",
                isZh ? "请选择有效的 Endfield.exe。" : "Please select a valid Endfield.exe.",
                InfoBarSeverity.Error);
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

            string loaderMode = GetSelectedLoaderMode();
            ProcessStartInfo startInfo;
            string launchArguments = GameLaunchArgumentsBox.Text.Trim();
            if (loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase))
            {
                await XInputDeploymentService.InstallAsync(
                    GamePathBox.Text.Trim(),
                    InjectorPathBox.Text.Trim());
                startInfo = new ProcessStartInfo
                {
                    FileName = GamePathBox.Text.Trim(),
                    WorkingDirectory = Path.GetDirectoryName(GamePathBox.Text.Trim()) ?? string.Empty,
                    UseShellExecute = true,
                    Arguments = launchArguments
                };
            }
            else
            {
                startInfo = new ProcessStartInfo
                {
                    FileName = InjectorPathBox.Text.Trim(),
                    WorkingDirectory = Path.GetDirectoryName(InjectorPathBox.Text.Trim()) ?? string.Empty,
                    UseShellExecute = true,
                    Verb = "runas",
                    Arguments = BuildInjectorArguments(
                        GamePathBox.Text.Trim(),
                        launchArguments)
                };
            }
            Process.Start(startInfo);
            ShowStatus(
                loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase)
                    ? (isZh ? "XInput 自启动已就绪" : "XInput Auto-load Ready")
                    : (isZh ? "注入器已启动" : "Injector Launched"),
                loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase)
                    ? (isZh ? "游戏将通过 xinput1_4.dll 加载 Better Endfield Host。"
                            : "The game will load Better Endfield Host via xinput1_4.dll.")
                    : (isZh ? "如果出现用户账户控制提示，请允许管理员权限。游戏启动后状态会自动更新。"
                            : "Please grant administrator permissions if prompted by UAC. Status will update once game starts."),
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or IOException or
                UnauthorizedAccessException or Win32Exception)
        {
            ShowStatus(isZh ? "启动失败" : "Launch Failed", exception.Message, InfoBarSeverity.Error);
        }
        finally
        {
            LaunchProgressRing.IsActive = false;
            LaunchProgressRing.Visibility = Visibility.Collapsed;
        }
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        ApplyConfiguration(ModConfiguration.CreateDefaults());
        ShowStatus(
            isZh ? "已恢复界面默认值" : "Restored Defaults",
            isZh ? "点击“保存”后才会覆盖配置文件。" : "Configuration file will be updated after clicking Save.",
            InfoBarSeverity.Informational);
    }

    private void OpenConfigurationFolderButton_Click(object sender, RoutedEventArgs e)
    {
        string configPath =
            ConfigurationService.GetNativeConfigurationPath(InjectorPathBox.Text);
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
        string logPath = ConfigurationService.GetLogPath(InjectorPathBox.Text);
        if (!File.Exists(logPath))
        {
            ShowStatus(
                "尚未生成日志",
                "至少完成一次启动后才会出现 BetterEndfield.log。",
                InfoBarSeverity.Informational);
            return;
        }

        OpenWithShell(logPath);
    }

    private async void LanguageComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (_initializing) return;
        string language = GetSelectedLanguage();
        _appSettings.Language = language;
        LocalizationService.Instance.ApplyLanguage(language);
        UpdateLocalizedUI();
        await ConfigurationService.SaveAppSettingsAsync(_appSettings);
        RefreshRuntimeStatus();
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
        bool isZh = LocalizationService.Instance.IsChinese;
        try
        {
            string shortcutPath = ShortcutService.CreateApplicationShortcut();
            ShowStatus(
                isZh ? "快捷方式已创建" : "Shortcut Created",
                shortcutPath,
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or COMException)
        {
            ShowStatus(isZh ? "创建快捷方式失败" : "Failed to Create Shortcut", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void CreateGameShortcutButton_Click(object sender, RoutedEventArgs e)
    {
        if (!await SaveAsync(showSuccess: false))
        {
            return;
        }

        bool isZh = LocalizationService.Instance.IsChinese;
        try
        {
            string loaderMode = GetSelectedLoaderMode();
            if (loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase))
            {
                await XInputDeploymentService.InstallAsync(
                    GamePathBox.Text.Trim(),
                    InjectorPathBox.Text.Trim());
            }
            string shortcutPath = ShortcutService.CreateGameShortcut(
                loaderMode,
                InjectorPathBox.Text,
                GamePathBox.Text,
                GameLaunchArgumentsBox.Text);
            ShowStatus(
                isZh ? "一键启动快捷方式已创建" : "Quick Launch Shortcut Created",
                isZh ? $"已保存当前配置并创建：{shortcutPath}" : $"Saved configuration and created: {shortcutPath}",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or COMException)
        {
            ShowStatus(isZh ? "创建快捷方式失败" : "Failed to Create Shortcut", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
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
                UpdateInfoBar.Title = isZh ? "暂无可用发布版本" : "No Releases Available";
                UpdateInfoBar.Message = isZh ? "GitHub Releases 中还没有正式发布记录。" : "No releases found in GitHub repository.";
                UpdateInfoBar.Severity = InfoBarSeverity.Informational;
                OpenReleaseButton.Visibility = Visibility.Visible;
            }
            else if (result.IsUpdateAvailable)
            {
                UpdateInfoBar.Title = isZh ? "发现新版本" : "New Version Available";
                UpdateInfoBar.Message = isZh
                    ? $"当前 {result.CurrentVersion}，最新 {result.LatestVersion}。"
                    : $"Current {result.CurrentVersion}, Latest {result.LatestVersion}.";
                UpdateInfoBar.Severity = InfoBarSeverity.Success;
                OpenReleaseButton.Visibility = Visibility.Visible;
            }
            else
            {
                UpdateInfoBar.Title = isZh ? "已经是最新版本" : "Up to Date";
                UpdateInfoBar.Message = isZh
                    ? $"当前版本 {result.CurrentVersion}，远程版本 {result.LatestVersion}。"
                    : $"Current version {result.CurrentVersion}, Remote {result.LatestVersion}.";
                UpdateInfoBar.Severity = InfoBarSeverity.Success;
            }
            UpdateInfoBar.IsOpen = true;
        }
        catch (Exception exception) when (
            exception is HttpRequestException or TaskCanceledException or JsonException)
        {
            UpdateInfoBar.Title = isZh ? "检查更新失败" : "Update Check Failed";
            UpdateInfoBar.Message = exception is TaskCanceledException
                ? (isZh ? "连接 GitHub 超时，请稍后重试。" : "Connection timed out. Please retry later.")
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
        bool isZh = LocalizationService.Instance.IsChinese;
        try
        {
            var package = new DataPackage();
            package.SetText(QqGroupNumber);
            Clipboard.SetContent(package);
            ShowStatus(isZh ? "QQ群号已复制" : "QQ Group Number Copied", QqGroupNumber, InfoBarSeverity.Success);
        }
        catch (COMException exception)
        {
            ShowStatus(isZh ? "复制QQ群号失败" : "Failed to Copy QQ Group Number", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void ViewDisclaimerButton_Click(object sender, RoutedEventArgs e) =>
        await ShowDisclaimerAsync(requireAcceptance: false);

    private async Task<bool> SaveAsync(bool showSuccess)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        string gamePath = GamePathBox.Text.Trim();
        if (!RuntimePathDiscoveryService.IsGameExecutable(gamePath))
        {
            ShowStatus(
                isZh ? "游戏路径无效" : "Invalid Game Path",
                isZh ? "请选择有效的 Endfield.exe。" : "Please select a valid Endfield.exe.",
                InfoBarSeverity.Error);
            return false;
        }

        string injectorPath = InjectorPathBox.Text.Trim();
        if (!File.Exists(injectorPath))
        {
            ShowStatus(
                isZh ? "注入器路径无效" : "Invalid Injector Path",
                isZh ? "请选择有效的 BetterEndfield.Injector.exe。" : "Please select a valid BetterEndfield.Injector.exe.",
                InfoBarSeverity.Error);
            return false;
        }
        try
        {
            ConfigurationService.ResolveInstallRoot(
                injectorPath,
                GetSelectedLoaderMode());
        }
        catch (InvalidOperationException exception)
        {
            ShowStatus(
                isZh ? "软件目录不完整" : "Incomplete Directory",
                exception.Message,
                InfoBarSeverity.Error);
            return false;
        }

        if (!TryReadConfiguration(out ModConfiguration configuration, out string? error))
        {
            ShowStatus(
                isZh ? "参数无效" : "Invalid Parameters",
                error ?? (isZh ? "请检查输入值。" : "Please check input values."),
                InfoBarSeverity.Error);
            return false;
        }

        if (!TryValidateLaunchArguments(out error))
        {
            ShowStatus(
                isZh ? "启动参数无效" : "Invalid Launch Arguments",
                error ?? (isZh ? "请检查游戏启动参数。" : "Please check game launch arguments."),
                InfoBarSeverity.Error);
            return false;
        }

        try
        {
            if (MusicReplacementToggle.IsOn)
            {
                OmniMixRegistrationStatus registration =
                    await OmniMixRegistrationService.RegisterAsync(
                        OmniMixBackendPathBox.Text);
                ApplyOmniMixRegistration(registration);
            }
            IReadOnlyList<VoiceCatalogRequest> catalogRequests =
                BuildVoiceCatalogRequests(configuration.VoiceRouterEnabled);
            if (catalogRequests.Count > 0)
            {
                ShowStatus(
                    isZh ? "正在准备配音资源" : "Preparing Voice Assets",
                    isZh ? "正在从本地游戏语言包生成所选角色的 catalog。"
                         : "Generating character voice catalog from local game language packs.",
                    InfoBarSeverity.Informational);
            }
            VoiceCatalogPreparation catalogPreparation =
                await VoiceCatalogService.PrepareAsync(
                    gamePath,
                    catalogRequests);
            await ConfigurationService.SaveModConfigurationAsync(
                injectorPath,
                GetSelectedLoaderMode(),
                configuration);
            await VoiceCatalogService.CommitAsync(catalogPreparation);
            _appSettings.GameExecutablePath = gamePath;
            _appSettings.InjectorPath = injectorPath;
            _appSettings.LoaderMode = GetSelectedLoaderMode();
            _appSettings.GameLaunchArguments = GameLaunchArgumentsBox.Text.Trim();
            _appSettings.Theme = GetSelectedTheme();
            await ConfigurationService.SaveAppSettingsAsync(_appSettings);

            ConfigurationPathTextBlock.Text =
                ConfigurationService.GetNativeConfigurationPath(injectorPath);
            if (showSuccess)
            {
                ShowStatus(
                    isZh ? "参数已保存" : "Configuration Saved",
                    isZh ? "视觉、语言与已加载的音乐模块会在约 2 秒内热更新；模型、动画及首次启用的模块在下次注入时读取。"
                         : "Visuals, language, and loaded music reload within 2s; models and animations load on next launch.",
                    InfoBarSeverity.Success);
            }

            return true;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
            InvalidDataException or InvalidOperationException or
            OmniMixRegistrationException)
        {
            ShowStatus(isZh ? "保存失败" : "Save Failed", exception.Message, InfoBarSeverity.Error);
            return false;
        }
    }

    private bool TryValidateLaunchArguments(out string? error)
    {
        error = null;
        string arguments = GameLaunchArgumentsBox.Text;
        if (arguments.Contains('\0') || arguments.Contains('\r') || arguments.Contains('\n'))
        {
            error = "启动参数不能包含换行或空字符。";
            return false;
        }
        if (arguments.Length > 2048)
        {
            error = "启动参数不能超过 2048 个字符。";
            return false;
        }
        return true;
    }

    private static string BuildInjectorArguments(
        string gameExecutablePath,
        string gameArguments)
    {
        string arguments = "--game " + QuoteProcessArgument(gameExecutablePath);
        return string.IsNullOrWhiteSpace(gameArguments)
            ? arguments
            : arguments + " -- " + gameArguments.Trim();
    }

    private async Task ScanRuntimePathsAsync(bool showResult)
    {
        if (_pathScanRunning)
        {
            return;
        }
        _pathScanRunning = true;
        ScanPathsButton.IsEnabled = false;
        PathScanProgressRing.Visibility = Visibility.Visible;
        PathScanProgressRing.IsActive = true;
        try
        {
            RuntimePathDiscoveryResult result =
                await RuntimePathDiscoveryService.DiscoverAsync(
                    GamePathBox.Text,
                    InjectorPathBox.Text);
            if (!string.IsNullOrWhiteSpace(result.GameExecutablePath))
            {
                GamePathBox.Text = result.GameExecutablePath;
            }
            if (!string.IsNullOrWhiteSpace(result.InjectorPath))
            {
                InjectorPathBox.Text = result.InjectorPath;
            }
            UpdatePathStatusText();
            await RefreshXInputStatusAsync();
            if (showResult)
            {
                bool gameFound = RuntimePathDiscoveryService.IsGameExecutable(
                    GamePathBox.Text);
                bool injectorFound = File.Exists(InjectorPathBox.Text.Trim());
                ShowStatus(
                    gameFound && injectorFound ? "路径扫描完成" : "路径扫描未完成",
                    gameFound && injectorFound
                        ? "已定位 Endfield.exe 和 Better Endfield 注入器。"
                        : "未找到的路径需要手动选择。",
                    gameFound && injectorFound
                        ? InfoBarSeverity.Success
                        : InfoBarSeverity.Warning);
            }
        }
        finally
        {
            _pathScanRunning = false;
            ScanPathsButton.IsEnabled = true;
            PathScanProgressRing.IsActive = false;
            PathScanProgressRing.Visibility = Visibility.Collapsed;
        }
    }

    private void UpdatePathStatusText()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        bool gameValid = RuntimePathDiscoveryService.IsGameExecutable(
            GamePathBox.Text.Trim());
        GamePathStatusTextBlock.Text = gameValid
            ? (isZh ? "已找到游戏程序。" : "Valid game executable found.")
            : (isZh ? "未找到有效的 Endfield.exe。" : "No valid Endfield.exe found.");

        bool injectorValid = false;
        try
        {
            ConfigurationService.ResolveInstallRoot(
                InjectorPathBox.Text.Trim(),
                GetSelectedLoaderMode());
            injectorValid = true;
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or ArgumentException)
        {
        }
        InjectorPathStatusTextBlock.Text = injectorValid
            ? (isZh ? "已找到完整的 runtime、modules 和注入器目录。" : "Valid runtime, modules, and injector directory found.")
            : (isZh ? "注入器或相邻 runtime/modules 目录不完整。" : "Injector or adjacent runtime/modules directory is incomplete.");
    }

    private void UpdateLoaderModePanel()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        bool xinput = GetSelectedLoaderMode().Equals(
            "xinput",
            StringComparison.OrdinalIgnoreCase);
        XInputManagementPanel.Visibility = xinput
            ? Visibility.Visible
            : Visibility.Collapsed;
        LoaderModeDescriptionTextBlock.Text = xinput
            ? (isZh ? "游戏每次启动都会自动加载 Better Endfield。适合与其他加载器共存，或通过官方启动器直接启动。"
                    : "Better Endfield loads automatically whenever game starts. Recommended when coexisting with other mods or launching via official launcher.")
            : (isZh ? "由 Better Endfield 启动游戏并在启动阶段加载 Host，游戏目录保持不变。"
                    : "Launched and injected by Better Endfield at startup; game directory remains unmodified.");
        PageSelectionHintTextBlock.Text = xinput
            ? (isZh ? "启动时会确保 XInput 代理已安装到游戏目录。" : "Launcher will ensure XInput proxy is installed in game directory.")
            : (isZh ? "保存后在下一次注入时生效。" : "Changes will take effect on next game launch/injection.");
        UpdatePathStatusText();
    }

    private sealed record DisplayBackendOption(UpscalerBackend Backend, string Label);

    private GpuInfo _displayGpu = GpuInfo.Unknown;
    private DisplayConfiguration _displayConfiguration = new();
    private bool _displayInitializing;
    private int _displayStatusRevision;

    private async Task InitializeDisplayPageAsync()
    {
        _displayInitializing = true;
        try
        {
            _displayGpu = GpuDetectionService.Detect();
            _displayConfiguration = await ConfigurationService.LoadDisplayConfigurationAsync();

            DisplayGpuTextBlock.Text = _displayGpu.Architecture == GpuArchitecture.Unknown
                ? $"{_displayGpu.Description}。未能判定架构代次，请自行选择后端。"
                : $"{_displayGpu.Description}（{DescribeArchitecture(_displayGpu.Architecture)}）";

            DisplayBackendOption[] options = BuildBackendOptions(_displayGpu);
            DisplayBackendComboBox.ItemsSource = options;
            DisplayBackendComboBox.DisplayMemberPath = nameof(DisplayBackendOption.Label);

            // 未配置过时落到按硬件给出的建议值；配置过但当前硬件不支持该后端时
            // （换卡）同样回退，避免把无效组合写进 ini。
            UpscalerBackend desired = _displayConfiguration.Enabled
                ? _displayConfiguration.Backend
                : DisplayConfiguration.SuggestBackend(_displayGpu);
            DisplayBackendOption selected =
                options.FirstOrDefault(option => option.Backend == desired) ?? options[0];
            DisplayBackendComboBox.SelectedItem = selected;
            _displayConfiguration.Backend = selected.Backend;

            DisplaySpoofingToggle.IsOn = _displayConfiguration.GpuSpoofing;
            DisplayDiagnosticsToggle.IsOn = _displayConfiguration.Diagnostics;
            UpdateDisplayTradeoffText(selected.Backend);
        }
        finally
        {
            _displayInitializing = false;
        }
        await RefreshDisplayStatusAsync();
    }

    private static DisplayBackendOption[] BuildBackendOptions(GpuInfo gpu)
    {
        var options = new List<DisplayBackendOption>
        {
            new(UpscalerBackend.Disabled, DisplayConfiguration.DescribeBackend(UpscalerBackend.Disabled))
        };
        void Add(UpscalerBackend backend) =>
            options.Add(new DisplayBackendOption(
                backend, DisplayConfiguration.DescribeBackend(backend)));

        switch (gpu.Architecture)
        {
            case GpuArchitecture.AmdRdna4:
                Add(UpscalerBackend.Fsr31);
                Add(UpscalerBackend.Fsr4Fp8);
                break;
            case GpuArchitecture.AmdRdna3:
            case GpuArchitecture.AmdRdna2:
                Add(UpscalerBackend.Fsr31);
                Add(UpscalerBackend.Fsr4Int8);
                break;
            case GpuArchitecture.AmdRdna1:
            case GpuArchitecture.AmdPreRdna:
                Add(UpscalerBackend.Fsr31);
                break;
            case GpuArchitecture.IntelArc:
                Add(UpscalerBackend.XeSS);
                break;
            case GpuArchitecture.Nvidia:
                // 客户端原生 DLSS 已是 N 卡上的最佳路径，不提供替代后端。
                break;
            default:
                Add(UpscalerBackend.Fsr31);
                Add(UpscalerBackend.Fsr4Int8);
                Add(UpscalerBackend.Fsr4Fp8);
                Add(UpscalerBackend.XeSS);
                break;
        }
        return [.. options];
    }

    private static string DescribeArchitecture(GpuArchitecture architecture) => architecture switch
    {
        GpuArchitecture.AmdRdna4 => "RDNA4",
        GpuArchitecture.AmdRdna3 => "RDNA3",
        GpuArchitecture.AmdRdna2 => "RDNA2",
        GpuArchitecture.AmdRdna1 => "RDNA1",
        GpuArchitecture.AmdPreRdna => "RDNA 之前",
        GpuArchitecture.IntelArc => "Intel Arc",
        GpuArchitecture.Nvidia => "NVIDIA",
        _ => "未知"
    };

    private void UpdateDisplayTradeoffText(UpscalerBackend backend)
    {
        string? tradeoff = DisplayConfiguration.DescribeTradeoff(backend, _displayGpu);
        DisplayTradeoffTextBlock.Text = tradeoff ?? string.Empty;
        DisplayTradeoffTextBlock.Visibility = tradeoff is null
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    private void DisplayBackendComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_displayInitializing ||
            DisplayBackendComboBox.SelectedItem is not DisplayBackendOption option)
        {
            return;
        }
        _displayConfiguration.Backend = option.Backend;
        UpdateDisplayTradeoffText(option.Backend);
    }

    private async Task RefreshDisplayStatusAsync()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        int revision = Interlocked.Increment(ref _displayStatusRevision);
        OptiScalerDeploymentStatus status = await OptiScalerDeploymentService.InspectAsync(
            GamePathBox.Text.Trim(),
            InjectorPathBox.Text.Trim());
        if (revision != _displayStatusRevision)
        {
            return;
        }
        DisplayStatusInfoBar.Title = status.State switch
        {
            OptiScalerDeploymentState.Installed => isZh ? "已部署" : "Deployed",
            OptiScalerDeploymentState.UpdateAvailable => isZh ? "可更新" : "Update Available",
            OptiScalerDeploymentState.Conflict => isZh ? "检测到冲突" : "Conflict Detected",
            OptiScalerDeploymentState.NotInstalled => isZh ? "未部署" : "Not Deployed",
            _ => isZh ? "暂不可用" : "Unavailable"
        };
        DisplayStatusInfoBar.Message = status.Message;
        DisplayStatusInfoBar.Severity = status.State switch
        {
            OptiScalerDeploymentState.Installed => InfoBarSeverity.Success,
            OptiScalerDeploymentState.Conflict => InfoBarSeverity.Error,
            OptiScalerDeploymentState.UpdateAvailable => InfoBarSeverity.Warning,
            OptiScalerDeploymentState.NotInstalled => InfoBarSeverity.Informational,
            _ => InfoBarSeverity.Warning
        };
        InstallDisplayButton.IsEnabled = status.CanInstall;
        UninstallDisplayButton.IsEnabled = status.CanUninstall;
        ApplyDisplayButton.IsEnabled = status.State == OptiScalerDeploymentState.Installed;
    }

    private bool IsGameRunningForDisplay(string action)
    {
        if (Process.GetProcessesByName("Endfield").Length == 0)
        {
            return false;
        }
        bool isZh = LocalizationService.Instance.IsChinese;
        ShowStatus(
            isZh ? "游戏正在运行" : "Game Already Running",
            isZh ? $"请退出游戏后再{action}显示增强组件。"
                 : $"Please exit the game before attempting to {action} display enhancement components.",
            InfoBarSeverity.Warning);
        return true;
    }

    private async void InstallDisplayButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        if (IsGameRunningForDisplay(isZh ? "部署" : "deploy"))
        {
            return;
        }
        try
        {
            OptiScalerDeploymentStatus status = await OptiScalerDeploymentService.InstallAsync(
                GamePathBox.Text.Trim(),
                InjectorPathBox.Text.Trim());
            await ApplyDisplayConfigurationAsync(silent: true);
            await RefreshDisplayStatusAsync();
            ShowStatus(isZh ? "显示增强已部署" : "Display Enhancement Deployed", status.Message, InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidDataException or InvalidOperationException or FileNotFoundException)
        {
            ShowStatus(isZh ? "显示增强部署失败" : "Deployment Failed", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void ApplyDisplayButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        if (IsGameRunningForDisplay(isZh ? "重新配置" : "reconfigure"))
        {
            return;
        }
        await ApplyDisplayConfigurationAsync(silent: false);
    }

    private async Task ApplyDisplayConfigurationAsync(bool silent)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        _displayConfiguration.GpuSpoofing = DisplaySpoofingToggle.IsOn;
        _displayConfiguration.Diagnostics = DisplayDiagnosticsToggle.IsOn;
        _displayConfiguration.Enabled =
            _displayConfiguration.Backend != UpscalerBackend.Disabled;
        try
        {
            await ConfigurationService.SaveDisplayConfigurationAsync(_displayConfiguration);
            IReadOnlyList<string> notes = await OptiScalerConfigurationService.ApplyAsync(
                GamePathBox.Text.Trim(),
                InjectorPathBox.Text.Trim(),
                _displayConfiguration,
                _displayGpu);
            DisplayNotesTextBlock.Text = string.Join(
                Environment.NewLine, notes.Select(note => "· " + note));
            DisplayNotesTextBlock.Visibility = notes.Count > 0
                ? Visibility.Visible
                : Visibility.Collapsed;
            if (!silent)
            {
                ShowStatus(
                    isZh ? "显示增强配置已应用" : "Display Configuration Applied",
                    isZh ? "已写入 OptiScaler.ini，改动在下一次启动客户端时生效。"
                         : "Written to OptiScaler.ini; changes will take effect on next game launch.",
                    InfoBarSeverity.Success);
            }
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidDataException or InvalidOperationException or FileNotFoundException)
        {
            ShowStatus(isZh ? "显示增强配置失败" : "Configuration Failed", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void UninstallDisplayButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        if (IsGameRunningForDisplay(isZh ? "卸载" : "uninstall"))
        {
            return;
        }
        try
        {
            await OptiScalerDeploymentService.UninstallAsync(
                GamePathBox.Text.Trim(),
                InjectorPathBox.Text.Trim());
            DisplayNotesTextBlock.Visibility = Visibility.Collapsed;
            await RefreshDisplayStatusAsync();
            ShowStatus(
                isZh ? "显示增强已卸载" : "Display Enhancement Uninstalled",
                isZh ? "已移除 Better Endfield 写入游戏目录的 OptiScaler 组件与归属记录。"
                     : "Removed OptiScaler components and attribution records written by Better Endfield.",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidOperationException or InvalidDataException or FileNotFoundException)
        {
            // 卸载会在保留了非本软件文件时抛出，属于预期结果而非失败，用警告呈现。
            await RefreshDisplayStatusAsync();
            ShowStatus(isZh ? "显示增强部分保留" : "Partial Components Preserved", exception.Message, InfoBarSeverity.Warning);
        }
    }

    private async Task RefreshXInputStatusAsync()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        int revision = Interlocked.Increment(ref _xInputStatusRevision);
        XInputDeploymentStatus status = await XInputDeploymentService.InspectAsync(
            GamePathBox.Text.Trim(),
            InjectorPathBox.Text.Trim());
        if (revision != _xInputStatusRevision)
        {
            return;
        }
        XInputStatusInfoBar.Title = status.State switch
        {
            XInputDeploymentState.Installed => isZh ? "已安装" : "Installed",
            XInputDeploymentState.UpdateAvailable => isZh ? "可更新" : "Update Available",
            XInputDeploymentState.Conflict => isZh ? "检测到冲突" : "Conflict Detected",
            XInputDeploymentState.NotInstalled => isZh ? "未安装" : "Not Installed",
            _ => isZh ? "暂不可用" : "Unavailable"
        };
        XInputStatusInfoBar.Message = status.Message;
        XInputStatusInfoBar.Severity = status.State switch
        {
            XInputDeploymentState.Installed => InfoBarSeverity.Success,
            XInputDeploymentState.Conflict => InfoBarSeverity.Error,
            XInputDeploymentState.UpdateAvailable => InfoBarSeverity.Warning,
            XInputDeploymentState.NotInstalled => InfoBarSeverity.Informational,
            _ => InfoBarSeverity.Warning
        };
        InstallXInputButton.IsEnabled = status.CanInstall;
        UninstallXInputButton.IsEnabled = status.CanUninstall;
    }

    private async void InstallXInputButton_Click(object sender, RoutedEventArgs e)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus(
                isZh ? "游戏正在运行" : "Game Already Running",
                isZh ? "请退出游戏后再安装或更新 XInput 代理。" : "Please exit the game before installing or updating XInput proxy.",
                InfoBarSeverity.Warning);
            return;
        }
        if (!await SaveAsync(showSuccess: false))
        {
            return;
        }
        try
        {
            XInputDeploymentStatus status = await XInputDeploymentService.InstallAsync(
                GamePathBox.Text.Trim(),
                InjectorPathBox.Text.Trim());
            await RefreshXInputStatusAsync();
            ShowStatus(isZh ? "XInput 已安装" : "XInput Installed", status.Message, InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidDataException or InvalidOperationException)
        {
            ShowStatus(isZh ? "XInput 安装失败" : "XInput Installation Failed", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void UninstallXInputButton_Click(object sender, RoutedEventArgs e)
    {
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus("游戏正在运行", "请退出游戏后再卸载 XInput 代理。", InfoBarSeverity.Warning);
            return;
        }
        try
        {
            await XInputDeploymentService.UninstallAsync(
                GamePathBox.Text.Trim(),
                InjectorPathBox.Text.Trim());
            await RefreshXInputStatusAsync();
            ShowStatus(
                "XInput 已卸载",
                "已移除 Better Endfield 写入游戏目录的代理、归属记录和状态文件。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidOperationException or FileNotFoundException)
        {
            ShowStatus("XInput 卸载失败", exception.Message, InfoBarSeverity.Error);
        }
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
            CrossfadeDurationNumberBox,
            MusicTargetLatencyNumberBox,
            MusicPrebufferNumberBox,
            FreeCameraMovementSpeedNumberBox,
            FreeCameraFieldOfViewNumberBox
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

        if (!TryNormalizeCombatHotkey(
                CombatToggleHotkeyBox.Text, out string toggleHotkey) ||
            !TryNormalizeCombatHotkey(
                CombatOverlayHotkeyBox.Text, out string overlayHotkey))
        {
            error = "战斗统计快捷键必须为 F1-F24、Ctrl+F1-F24 或单个字母/数字。";
            return false;
        }
        if (toggleHotkey.Equals(overlayHotkey, StringComparison.OrdinalIgnoreCase))
        {
            error = "记录开关和悬浮窗开关不能使用同一个快捷键。";
            return false;
        }
        if (!TryNormalizeCameraHotkey(
                FreeCameraHotkeyBox.Text, out string cameraToggleHotkey))
        {
            error = "相机热键必须为单个字母/数字、F1-F24 或 NUMPAD0-NUMPAD9。";
            return false;
        }
        if (!TryNormalizeCameraHotkey(
                HideHudHotkeyBox.Text, out string hideHudToggleHotkey))
        {
            error = "HUD 热键必须为单个字母/数字、F1-F24 或 NUMPAD0-NUMPAD9。";
            return false;
        }

        if (MusicReplacementToggle.IsOn &&
            !OmniMixRegistrationService.IsValidBackendPath(
                OmniMixBackendPathBox.Text.Trim()))
        {
            error = "启用音乐替换前，请选择有效的 x64 OmniMixPlayer.Backend.exe。";
            return false;
        }

        if (MusicReplacementToggle.IsOn &&
            string.IsNullOrWhiteSpace(OmniMixClientIdTextBlock.Tag as string))
        {
            error = "OmniMix 客户端标识缺失，请重新注册后端。";
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
            LogoThemeEnabled = LogoThemeToggle.IsOn,
            LogoThemeColor = $"#{LogoThemeColorPicker.Color.R:X2}{LogoThemeColorPicker.Color.G:X2}{LogoThemeColorPicker.Color.B:X2}",
            VoiceRouterEnabled = VoiceRouterToggle.IsOn,
            ReplaceNarrativeVoice = NarrativeVoiceToggle.IsOn,
            VoiceLanguageRules = voiceLanguageRules,
            MusicReplacementEnabled = MusicReplacementToggle.IsOn,
            OmniMixBackendExe = OmniMixBackendPathBox.Text.Trim(),
            OmniMixClientId = OmniMixClientIdTextBlock.Tag as string ?? string.Empty,
            ReplaceLoginMusic = ReplaceLoginMusicToggle.IsOn,
            ReplaceMetaMusic = ReplaceMetaMusicToggle.IsOn,
            ReplaceGameplayMusic = ReplaceGameplayMusicToggle.IsOn,
            MusicTargetLatency = MusicTargetLatencyNumberBox.Value,
            MusicPrebufferMilliseconds = MusicPrebufferNumberBox.Value,
            FallbackToNativeMusic = FallbackToNativeMusicToggle.IsOn,
            MusicDiagnostics = MusicDiagnosticsToggle.IsOn,
            CombatStatsEnabled = CombatStatsToggle.IsOn,
            HideDamageNumbers = HideDamageNumbersToggle.IsOn,
            CombatOverlayEnabled = CombatOverlayToggle.IsOn,
            CombatRdpsDisplay = CombatRdpsDisplayToggle.IsOn,
            CombatToggleHotkey = toggleHotkey,
            CombatOverlayHotkey = overlayHotkey,
            AutoDungeonSession = AutoDungeonSessionToggle.IsOn,
            UiEnhancementEnabled = MobileUiToggle.IsOn || HideUidToggle.IsOn ||
                HideHudToggle.IsOn,
            MobileUiEnabled = MobileUiToggle.IsOn,
            HideUidEnabled = HideUidToggle.IsOn,
            HideHudEnabled = HideHudToggle.IsOn,
            HideHudToggleHotkey = hideHudToggleHotkey,
            FreeCameraEnabled = FreeCameraToggle.IsOn,
            DisableDitherEnabled = DisableDitherToggle.IsOn,
            PauseGameInFreeCamera = PauseGameInFreeCameraToggle.IsOn,
            FreeCameraToggleHotkey = cameraToggleHotkey,
            FreeCameraMovementSpeed = FreeCameraMovementSpeedNumberBox.Value,
            FreeCameraFieldOfView = FreeCameraFieldOfViewNumberBox.Value
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
        LogoThemeToggle.IsOn = configuration.LogoThemeEnabled;
        LogoThemeColorPicker.Color = ParseLogoThemeColor(configuration.LogoThemeColor);
        VoiceRouterToggle.IsOn = configuration.VoiceRouterEnabled;
        NarrativeVoiceToggle.IsOn = configuration.ReplaceNarrativeVoice;
        LoadVoiceRules(configuration.VoiceLanguageRules);
        MusicReplacementToggle.IsOn = configuration.MusicReplacementEnabled;
        OmniMixBackendPathBox.Text = configuration.OmniMixBackendExe;
        OmniMixClientIdTextBlock.Tag = configuration.OmniMixClientId;
        UpdateOmniMixStatusTexts();
        ReplaceLoginMusicToggle.IsOn = configuration.ReplaceLoginMusic;
        ReplaceMetaMusicToggle.IsOn = configuration.ReplaceMetaMusic;
        ReplaceGameplayMusicToggle.IsOn = configuration.ReplaceGameplayMusic;
        MusicTargetLatencyNumberBox.Value = configuration.MusicTargetLatency;
        MusicPrebufferNumberBox.Value = configuration.MusicPrebufferMilliseconds;
        FallbackToNativeMusicToggle.IsOn = configuration.FallbackToNativeMusic;
        MusicDiagnosticsToggle.IsOn = configuration.MusicDiagnostics;
        CombatStatsToggle.IsOn = configuration.CombatStatsEnabled;
        HideDamageNumbersToggle.IsOn = configuration.HideDamageNumbers;
        CombatOverlayToggle.IsOn = configuration.CombatOverlayEnabled;
        CombatRdpsDisplayToggle.IsOn = configuration.CombatRdpsDisplay;
        CombatToggleHotkeyBox.Text = configuration.CombatToggleHotkey;
        CombatOverlayHotkeyBox.Text = configuration.CombatOverlayHotkey;
        AutoDungeonSessionToggle.IsOn = configuration.AutoDungeonSession;
        MobileUiToggle.IsOn = configuration.MobileUiEnabled;
        HideUidToggle.IsOn = configuration.HideUidEnabled;
        HideHudToggle.IsOn = configuration.HideHudEnabled;
        HideHudHotkeyBox.Text = configuration.HideHudToggleHotkey;
        FreeCameraToggle.IsOn = configuration.FreeCameraEnabled;
        DisableDitherToggle.IsOn = configuration.DisableDitherEnabled;
        PauseGameInFreeCameraToggle.IsOn = configuration.PauseGameInFreeCamera;
        FreeCameraHotkeyBox.Text = configuration.FreeCameraToggleHotkey;
        FreeCameraMovementSpeedNumberBox.Value = configuration.FreeCameraMovementSpeed;
        FreeCameraFieldOfViewNumberBox.Value = configuration.FreeCameraFieldOfView;

        _initializing = wasInitializing;
        UpdateCrossfadePanel();
    }

    private void UpdateOmniMixStatusTexts()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        string backend = OmniMixBackendPathBox.Text?.Trim() ?? string.Empty;
        string? clientId = OmniMixClientIdTextBlock.Tag as string;

        if (string.IsNullOrWhiteSpace(backend))
        {
            OmniMixRegistrationStatusTextBlock.Text = isZh ? "尚未注册 OmniMix 后端" : "OmniMix backend not registered";
        }
        else
        {
            bool valid = OmniMixRegistrationService.IsValidBackendPath(backend);
            OmniMixRegistrationStatusTextBlock.Text = valid
                ? (isZh ? "OmniMix 后端路径有效" : "OmniMix backend path valid")
                : (isZh ? "OmniMix 后端路径已失效" : "OmniMix backend path is invalid");
        }

        OmniMixClientIdTextBlock.Text = string.IsNullOrWhiteSpace(clientId)
            ? (isZh ? "客户端标识将在注册时生成。" : "Client ID will be generated upon registration.")
            : (isZh ? $"客户端标识：{clientId}" : $"Client ID: {clientId}");
    }

    private void LogoThemeColorPicker_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateColorPickerLabels();
    }

    private void UpdateColorPickerLabels()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        UpdateColorPickerVisualTree(LogoThemeColorPicker, isZh);
    }

    private static void UpdateColorPickerVisualTree(DependencyObject parent, bool isChinese)
    {
        int count = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if (child is TextBlock tb)
            {
                string text = tb.Text?.Trim() ?? string.Empty;
                string? target = null;
                if (string.Equals(text, "红色", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(text, "Red", StringComparison.OrdinalIgnoreCase))
                {
                    target = isChinese ? "红色" : "Red";
                }
                else if (string.Equals(text, "绿色", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(text, "Green", StringComparison.OrdinalIgnoreCase))
                {
                    target = isChinese ? "绿色" : "Green";
                }
                else if (string.Equals(text, "蓝色", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(text, "Blue", StringComparison.OrdinalIgnoreCase))
                {
                    target = isChinese ? "蓝色" : "Blue";
                }
                else if (string.Equals(text, "十六进制", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(text, "Hex", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(text, "Hexadecimal", StringComparison.OrdinalIgnoreCase))
                {
                    target = isChinese ? "十六进制" : "Hex";
                }

                if (target != null && tb.Text != target)
                {
                    tb.Text = target;
                }
            }
            else if (child is TextBox textBox)
            {
                if (textBox.Header is string headerStr)
                {
                    string header = headerStr.Trim();
                    string? target = null;
                    if (string.Equals(header, "红色", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(header, "Red", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "红色" : "Red";
                    }
                    else if (string.Equals(header, "绿色", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(header, "Green", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "绿色" : "Green";
                    }
                    else if (string.Equals(header, "蓝色", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(header, "Blue", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "蓝色" : "Blue";
                    }
                    else if (string.Equals(header, "十六进制", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(header, "Hex", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "十六进制" : "Hex";
                    }

                    if (target != null && !Equals(textBox.Header, target))
                    {
                        textBox.Header = target;
                    }
                }
            }
            else if (child is NumberBox numberBox)
            {
                if (numberBox.Header is string headerStr)
                {
                    string header = headerStr.Trim();
                    string? target = null;
                    if (string.Equals(header, "红色", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(header, "Red", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "红色" : "Red";
                    }
                    else if (string.Equals(header, "绿色", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(header, "Green", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "绿色" : "Green";
                    }
                    else if (string.Equals(header, "蓝色", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(header, "Blue", StringComparison.OrdinalIgnoreCase))
                    {
                        target = isChinese ? "蓝色" : "Blue";
                    }

                    if (target != null && !Equals(numberBox.Header, target))
                    {
                        numberBox.Header = target;
                    }
                }
            }

            UpdateColorPickerVisualTree(child, isChinese);
        }
    }

    private static Color ParseLogoThemeColor(string value)
    {
        string normalized = value.Trim().TrimStart('#');
        return normalized.Length == 6 &&
            byte.TryParse(normalized[..2], NumberStyles.HexNumber,
                CultureInfo.InvariantCulture, out byte red) &&
            byte.TryParse(normalized.Substring(2, 2), NumberStyles.HexNumber,
                CultureInfo.InvariantCulture, out byte green) &&
            byte.TryParse(normalized.Substring(4, 2), NumberStyles.HexNumber,
                CultureInfo.InvariantCulture, out byte blue)
            ? Color.FromArgb(255, red, green, blue)
            : Color.FromArgb(255, 255, 201, 40);
    }

    private void ApplyOmniMixRegistration(OmniMixRegistrationStatus status)
    {
        OmniMixBackendPathBox.Text = status.BackendExe;
        OmniMixClientIdTextBlock.Tag = status.ClientId;
        bool isZh = LocalizationService.Instance.IsChinese;
        OmniMixRegistrationStatusTextBlock.Text = status.Registered
            ? status.Valid
                ? string.IsNullOrWhiteSpace(status.BackendVersion)
                    ? (isZh ? "OmniMix 后端路径有效" : "OmniMix backend path valid")
                    : (isZh ? $"OmniMix 后端 {status.BackendVersion}" : $"OmniMix backend {status.BackendVersion}")
                : (isZh ? $"OmniMix 后端不可用：{status.Reason}" : $"OmniMix backend unavailable: {status.Reason}")
            : (isZh ? "尚未注册 OmniMix 后端" : "OmniMix backend not registered");
        OmniMixClientIdTextBlock.Text = string.IsNullOrWhiteSpace(status.ClientId)
            ? (isZh ? "客户端标识将在注册时生成。" : "Client ID will be generated upon registration.")
            : (isZh ? $"客户端标识：{status.ClientId}" : $"Client ID: {status.ClientId}");
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

    private static bool TryNormalizeCombatHotkey(string value, out string normalized)
    {
        normalized = value.Trim().Replace(" ", string.Empty).ToUpperInvariant();
        string key = normalized.StartsWith("CTRL+", StringComparison.Ordinal)
            ? normalized[5..]
            : normalized;
        if (key.Length == 1 && char.IsAsciiLetterOrDigit(key[0]))
        {
            return true;
        }
        return key.Length >= 2 && key[0] == 'F' &&
            int.TryParse(key[1..], NumberStyles.None, CultureInfo.InvariantCulture,
                out int functionKey) &&
            functionKey is >= 1 and <= 24;
    }

    private static bool TryNormalizeCameraHotkey(string value, out string normalized)
    {
        normalized = value.Trim().Replace(" ", string.Empty).ToUpperInvariant();
        if (normalized.Length == 1 && char.IsAsciiLetterOrDigit(normalized[0]))
        {
            return true;
        }
        if (normalized.StartsWith("NUMPAD", StringComparison.Ordinal) &&
            normalized.Length == 7 && char.IsAsciiDigit(normalized[^1]))
        {
            return true;
        }
        return normalized.Length >= 2 && normalized[0] == 'F' &&
            int.TryParse(normalized[1..], NumberStyles.None,
                CultureInfo.InvariantCulture, out int functionKey) &&
            functionKey is >= 1 and <= 24;
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

    private static IReadOnlyList<VoiceCharacterChoice> BuildVoiceCharacterChoices()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        return PresetOptions.CharacterNamesZh
            .OrderBy(entry => entry.Key, StringComparer.OrdinalIgnoreCase)
            .Select(entry => new VoiceCharacterChoice
            {
                CharacterId = entry.Key,
                Speaker = GetCharacterSpeakerAlias(entry.Key),
                DisplayName = PresetOptions.GetCharacterName(entry.Key)
            })
            .GroupBy(choice => choice.Speaker, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .OrderBy(choice => choice.DisplayName, StringComparer.CurrentCulture)
            .Append(new VoiceCharacterChoice
            {
                Speaker = "*",
                DisplayName = isZh ? "其他角色（默认规则）" : "All Other Characters (Default Rule)"
            })
            .ToArray();
    }

    private void RefreshVoiceCharactersList()
    {
        string? selectedSpeaker = (VoiceCharacterComboBox.SelectedItem as VoiceCharacterChoice)?.Speaker;
        _voiceCharacters = BuildVoiceCharacterChoices();
        VoiceCharacterComboBox.ItemsSource = _voiceCharacters;
        VoiceCharacterComboBox.SelectedItem = _voiceCharacters.FirstOrDefault(
            choice => string.Equals(choice.Speaker, selectedSpeaker, StringComparison.OrdinalIgnoreCase))
            ?? _voiceCharacters.FirstOrDefault();
    }

    private void RefreshVoiceRulesDisplay()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        for (int index = 0; index < _voiceRules.Count; index++)
        {
            VoiceRuleEntry old = _voiceRules[index];
            string characterId = _voiceCharacters.FirstOrDefault(
                c => c.Speaker.Equals(old.Speaker, StringComparison.OrdinalIgnoreCase))?.CharacterId ?? old.Speaker;
            string displayName = old.Speaker == "*"
                ? (isZh ? "其他角色（默认规则）" : "All Other Characters (Default Rule)")
                : PresetOptions.GetCharacterName(characterId);
            _voiceRules[index] = new VoiceRuleEntry
            {
                Speaker = old.Speaker,
                DisplayName = displayName,
                Language = old.Language,
                LanguageDisplayName = GetVoiceLanguageDisplayName(old.Language)
            };
        }
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

    private string GetVoiceLanguageDisplayName(string language)
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        return language switch
        {
            "Chinese" or "CN" or "ZH" => "中文",
            "English" or "EN" => "English",
            "Japanese" or "JP" or "JA" => "日本語",
            "Korean" or "KR" or "KO" => "한국어",
            "FollowGlobal" or "Global" or "Default" => isZh ? "跟随游戏" : "Follow Game",
            _ => language
        };
    }

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

        bool isZh = LocalizationService.Instance.IsChinese;
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
            string characterId = _voiceCharacters.FirstOrDefault(choice =>
                choice.Speaker.Equals(speaker, StringComparison.OrdinalIgnoreCase))?.CharacterId ?? speaker;
            string displayName = speaker == "*"
                ? (isZh ? "其他角色（默认规则）" : "All Other Characters (Default Rule)")
                : PresetOptions.GetCharacterName(characterId);
            UpsertVoiceRule(speaker, language, displayName);
        }
        UpdateVoiceRulesEmptyState();
    }

    private string SerializeVoiceRules() => string.Join(
        Environment.NewLine,
        _voiceRules.Select(rule => $"{rule.Speaker}={rule.Language}"));

    private IReadOnlyList<VoiceCatalogRequest> BuildVoiceCatalogRequests(bool enabled)
    {
        if (!enabled)
        {
            return [];
        }

        var result = new List<VoiceCatalogRequest>();
        foreach (VoiceRuleEntry rule in _voiceRules)
        {
            if (rule.Language.Equals("FollowGlobal", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            VoiceCharacterChoice? choice = _voiceCharacters.FirstOrDefault(item =>
                item.Speaker.Equals(rule.Speaker, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(
                    item.CharacterId,
                    rule.Speaker,
                    StringComparison.OrdinalIgnoreCase));
            if (choice is null)
            {
                throw new InvalidOperationException(
                    $"角色“{rule.Speaker}”不在当前语音映射清单中。");
            }
            result.Add(new VoiceCatalogRequest(
                rule.Speaker,
                choice.CharacterId,
                rule.Language));
        }
        return result;
    }

    private void SelectLanguage(string language)
    {
        if (LanguageComboBox == null || LanguageComboBox.Items.Count == 0) return;
        string normalized = language is "zh-CN" or "en-US" ? language : "System";
        LanguageComboBox.SelectedItem = LanguageComboBox.Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(item => string.Equals(
                item.Tag as string,
                normalized,
                StringComparison.OrdinalIgnoreCase)) ??
            LanguageComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault();
    }

    private string GetSelectedLanguage() =>
        (LanguageComboBox?.SelectedItem as ComboBoxItem)?.Tag as string ?? "System";

    private void SelectTheme(string theme)
    {
        if (ThemeComboBox == null || ThemeComboBox.Items.Count == 0) return;
        string normalized = theme is "Light" or "Dark" ? theme : "Default";
        ThemeComboBox.SelectedItem = ThemeComboBox.Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(item => string.Equals(
                item.Tag as string,
                normalized,
                StringComparison.OrdinalIgnoreCase)) ??
            ThemeComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault();
    }

    private string GetSelectedTheme() =>
        (ThemeComboBox?.SelectedItem as ComboBoxItem)?.Tag as string ?? "Default";

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
        bool isZh = LocalizationService.Instance.IsChinese;
        var content = new StackPanel
        {
            Spacing = 12,
            MaxWidth = 560
        };
        content.Children.Add(new TextBlock
        {
            Text = isZh
                ? "本软件会将本机代码注入游戏进程，并在运行时修改模型、动画和语音资源选择。"
                : "This software injects native code into the game process to modify models, animations, and voice audio routing at runtime.",
            TextWrapping = TextWrapping.Wrap,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
        });
        content.Children.Add(new TextBlock
        {
            Text = isZh
                ? "可能的风险包括游戏崩溃、存档或配置异常、更新后失效，以及被游戏安全或反作弊系统识别。使用在线账号可能产生账号限制风险。"
                : "Potential risks include game crashes, configuration anomalies, invalidation after updates, and detection by game security or anti-cheat systems. Online accounts may be subject to restrictions.",
            TextWrapping = TextWrapping.Wrap
        });
        content.Children.Add(new TextBlock
        {
            Text = isZh
                ? "本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关，也不提供任何形式的担保。请自行备份重要数据，遵守游戏服务条款，并自行承担使用后果。"
                : "This project is an unofficial experimental tool, not affiliated with Hypergryph, Mountain Contour, or GRYPHLINE, and provides no warranties. Please backup important data, comply with terms of service, and use at your own risk.",
            TextWrapping = TextWrapping.Wrap
        });
        content.Children.Add(new TextBlock
        {
            Text = isZh
                ? "本软件不负责停用或绕过反作弊组件；游戏更新后如签名不匹配，相关 Hook 应停止使用，等待适配。"
                : "This software does not disable or bypass anti-cheat components. If signatures mismatch after a game update, related hooks should be suspended pending compatibility fixes.",
            TextWrapping = TextWrapping.Wrap,
            Foreground = (Brush)Application.Current.Resources[
                "TextFillColorSecondaryBrush"]
        });

        var dialog = new ContentDialog
        {
            XamlRoot = MainRoot.XamlRoot,
            Title = requireAcceptance
                ? (isZh ? "使用前请阅读" : "Please Read Before Use")
                : (isZh ? "风险与免责声明" : "Risk & Disclaimer"),
            Content = content,
            DefaultButton = requireAcceptance
                ? ContentDialogButton.Primary
                : ContentDialogButton.Close,
            PrimaryButtonText = requireAcceptance
                ? (isZh ? "我已了解并继续" : "I Understand & Continue")
                : string.Empty,
            CloseButtonText = requireAcceptance
                ? (isZh ? "退出" : "Exit")
                : (isZh ? "关闭" : "Close")
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
        bool isZh = LocalizationService.Instance.IsChinese;
        bool gameRunning = Process.GetProcessesByName("Endfield").Length > 0;
        RuntimeStatusTextBlock.Text = gameRunning
            ? (isZh ? "游戏正在运行" : "Game is running")
            : (isZh ? "游戏未运行" : "Game is not running");
        RuntimeStatusIndicator.Fill = gameRunning
            ? new SolidColorBrush(Microsoft.UI.Colors.LimeGreen)
            : (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];

        string logPath = ConfigurationService.GetLogPath(InjectorPathBox.Text);
        if (File.Exists(logPath))
        {
            DateTime updated = File.GetLastWriteTime(logPath);
            LogStatusTextBlock.Text = isZh
                ? $"日志更新：{updated:MM-dd HH:mm:ss}"
                : $"Log updated: {updated:MM-dd HH:mm:ss}";
        }
        else
        {
            LogStatusTextBlock.Text = File.Exists(InjectorPathBox.Text.Trim())
                ? (isZh ? "注入器已就绪 · 尚无日志" : "Injector ready · No logs yet")
                : (isZh ? "未找到注入器" : "Injector not found");
        }
    }

    private void RefreshRuntimeAnimationDurations()
    {
        string logPath = ConfigurationService.GetLogPath(InjectorPathBox.Text);
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
        bool isZh = LocalizationService.Instance.IsChinese;
        ActionDurationTextBlock.Text = action.Duration is double seconds
            ? (isZh
                ? $"动画原始时长：{seconds.ToString("0.###", CultureInfo.InvariantCulture)} 秒"
                : $"Original Duration: {seconds.ToString("0.###", CultureInfo.InvariantCulture)} s")
            : (isZh ? "动画原始时长：未知（首次播放后自动读取）" : "Original Duration: Unknown (read on first play)");
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

    private static string QuoteProcessArgument(string value)
    {
        string escaped = value.Replace("\"", "\\\"");
        return $"\"{escaped}\"";
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

    private void TrySetWindowIcon()
    {
        try
        {
            Version? version = typeof(MainWindow).Assembly.GetName().Version;
            string versionLabel = version is null
                ? "current"
                : $"{version.Major}.{version.Minor}.{version.Build}";
            string iconPath = Path.Combine(
                ConfigurationService.SettingsDirectory,
                $"window-icon-{versionLabel}.ico");
            if (!File.Exists(iconPath))
            {
                using Stream? resource = typeof(MainWindow).Assembly
                    .GetManifestResourceStream(WindowIconResourceName);
                if (resource is null)
                {
                    return;
                }
                Directory.CreateDirectory(ConfigurationService.SettingsDirectory);
                using FileStream output = File.Create(iconPath);
                resource.CopyTo(output);
            }

            nint windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId windowId =
                Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow.GetFromWindowId(windowId).SetIcon(iconPath);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
            InvalidOperationException or COMException)
        {
        }
    }

    private void UpdateLocalizedUI()
    {
        string lang = LocalizationService.Instance.EffectiveLanguage;
        bool isZh = LocalizationService.Instance.IsChinese;

        MainRoot.Language = lang;
        LogoThemeColorPicker.Language = lang;

        // Window & Title
        Title = "Better Endfield";
        PaneSubtitleTextBlock.Text = isZh ? "终末地 Mod 控制器" : "Endfield Mod Controller";

        // NavigationView Menu Items
        ModelNavigationItem.Content = isZh ? "开屏" : "Title Screen";
        VoiceNavigationItem.Content = isZh ? "配音语言" : "Voice Language";
        MusicNavigationItem.Content = isZh ? "音乐集成" : "Music Integration";
        CombatNavigationItem.Content = isZh ? "战斗数据" : "Combat Stats";
        UiNavigationItem.Content = isZh ? "界面增强" : "Touch & UI";
        CameraNavigationItem.Content = isZh ? "相机增强" : "Camera";
        DisplayNavigationItem.Content = isZh ? "显示增强" : "Display & Pipeline";
        if (FeatureNavigation.SettingsItem is NavigationViewItem settingsItem)
        {
            settingsItem.Content = isZh ? "设置" : "Settings";
        }
        AboutNavigationItem.Content = isZh ? "关于" : "About";

        // Model Page
        ModelPageTitleTextBlock.Text = isZh ? "开屏" : "Title Screen";
        ModelPageDescriptionTextBlock.Text = isZh
            ? "调整登录场景的视觉效果、角色模型与四阶段动画。"
            : "Customize title screen visuals, character models, and 4-phase animations.";
        ModelVisualSectionTitle.Text = isZh ? "界面视觉" : "Interface Visuals";
        ModelVisualSectionHint.Text = isZh
            ? "主题色可与原登录演员或角色替换同时使用。"
            : "Theme color works with both original actors and custom replacements.";
        LogoThemeToggle.Header = isZh ? "自定义 Logo 与色带主题色" : "Custom Logo & Ribbon Accent Theme";
        LogoThemeToggle.OffContent = isZh ? "使用游戏原色" : "Use game default colors";
        LogoThemeToggle.OnContent = isZh ? "使用所选颜色" : "Use selected accent color";
        ModelReplacementToggle.Header = isZh ? "启用角色替换" : "Enable Character Replacement";
        ModelReplacementToggle.OffContent = isZh ? "保持原登录演员" : "Keep original login actor";
        ModelReplacementToggle.OnContent = isZh ? "使用下方角色与动画" : "Use custom character & animations";
        ModelCharacterSectionTitle.Text = isZh ? "角色与动作" : "Character & Action Sequence";
        ModelCharacterSectionHint.Text = isZh
            ? "角色使用自己的坐姿动画链，最后动作从该角色的已索引动画中选择。"
            : "Characters use their own sit animation chains; final action is selected from indexed animations.";
        CharacterComboBox.Header = isZh ? "角色模型" : "Character Model";
        FinalActionComboBox.Header = isZh ? "最后动作" : "Final Action";
        StartYawNumberBox.Header = isZh ? "起始角度 (°)" : "Initial Yaw (°)";
        TurnDurationNumberBox.Header = isZh ? "转正时长 (秒)" : "Turn Duration (s)";
        ScaleNumberBox.Header = isZh ? "模型缩放" : "Model Scale";
        LeanSampleNumberBox.Header = isZh ? "前倾采样 (秒)" : "Forward Lean Sample (s)";
        ModelSpeedSectionTitle.Text = isZh ? "播放速度" : "Playback Speed";
        ModelSpeedSectionHint.Text = isZh
            ? "1.0 为动画原速。各阶段独立设置，不改变镜头时间轴。"
            : "1.0 is default speed. Each phase is configured independently without altering camera timeline.";
        SitLoopSpeedNumberBox.Header = isZh ? "坐姿循环" : "Sit Loop";
        SitSpecialSpeedNumberBox.Header = isZh ? "坐姿小动作" : "Sit Special";
        SitToWalkSpeedNumberBox.Header = isZh ? "起身过渡" : "Sit to Walk";
        FinalSpeedNumberBox.Header = isZh ? "最后动作" : "Final Action";
        ModelLoopSectionTitle.Text = isZh ? "连续循环" : "Continuous Looping";
        ModelLoopSectionHint.Text = isZh
            ? "可遵循资源 LoopTime、强制覆盖非循环资源，或按自定义区间交叉混合。"
            : "Follow asset LoopTime, force loop non-looping clips, or blend custom crossfade intervals.";
        FinalLoopToggle.Header = isZh ? "原生循环" : "Native Loop";
        FinalLoopToggle.OffContent = isZh ? "播放一次并停在末帧" : "Play once & hold last frame";
        FinalLoopToggle.OnContent = isZh ? "使用动画 LoopTime" : "Use clip LoopTime";
        ForceLoopToggle.Header = isZh ? "强制循环" : "Force Loop";
        ForceLoopToggle.OffContent = isZh ? "遵循资源原生标志" : "Follow native clip flag";
        ForceLoopToggle.OnContent = isZh ? "强制忽略末帧停留" : "Force continuous looping";
        CrossfadeToggle.Header = isZh ? "首尾交叉过渡" : "Crossfade Loop Transition";
        CrossfadeToggle.OffContent = isZh ? "不混合" : "No blending";
        CrossfadeToggle.OnContent = isZh ? "首尾平滑混合" : "Smooth head-to-tail crossfade";
        LoopStartNumberBox.Header = isZh ? "循环起点 (秒)" : "Loop Start (s)";
        LoopEndNumberBox.Header = isZh ? "循环终点 (秒)" : "Loop End (s)";
        CrossfadeDurationNumberBox.Header = isZh ? "混合时长 (秒)" : "Crossfade Duration (s)";

        // Voice Page
        VoicePageTitleTextBlock.Text = isZh ? "配音语言" : "Voice Language";
        VoicePageDescriptionTextBlock.Text = isZh
            ? "为单个角色指定语音包，同时保持游戏全局语言不变。"
            : "Assign per-character audio language packs while keeping global game language intact.";
        VoicePageDownloadHintTextBlock.Text = isZh
            ? "请先下载对应语言包。"
            : "Please ensure corresponding language packs are downloaded in game first.";
        VoiceRouterToggle.Header = isZh ? "启用按角色配音" : "Enable Per-Character Voice";
        VoiceRouterToggle.OffContent = isZh ? "全部跟随游戏" : "Follow game global language";
        VoiceRouterToggle.OnContent = isZh ? "应用下方角色规则" : "Apply custom rules below";
        NarrativeVoiceToggle.Header = isZh ? "替换剧情语音与口型" : "Replace Story Dialogue & Lip-sync";
        NarrativeVoiceToggle.OffContent = isZh ? "剧情对话跟随游戏语言" : "Story dialogues follow game global";
        NarrativeVoiceToggle.OnContent = isZh ? "剧情对话应用角色规则" : "Story dialogues apply character rules";
        VoiceRuleSectionTitle.Text = isZh ? "设置角色语言" : "Configure Character Rule";
        VoiceRuleSectionHint.Text = isZh
            ? "同一角色再次添加时会更新现有规则。"
            : "Adding the same character again will update the existing rule.";
        VoiceCharacterComboBox.Header = isZh ? "角色" : "Character";
        VoiceLanguageRadioButtons.Header = isZh ? "配音语言" : "Voice Language";
        VoiceAddRuleButtonTextBlock.Text = isZh ? "添加或更新规则" : "Add or Update Rule";
        VoiceConfiguredSectionTitle.Text = isZh ? "已配置角色" : "Configured Rules";
        VoiceRulesEmptyTextBlock.Text = isZh
            ? "尚未配置角色，将全部跟随游戏全局语言。"
            : "No custom character rules configured; all characters follow global language.";

        var radioButtons = VoiceLanguageRadioButtons.Items.OfType<RadioButton>().ToList();
        if (radioButtons.Count >= 5)
        {
            radioButtons[0].Content = isZh ? "中文" : "Chinese";
            radioButtons[1].Content = "English";
            radioButtons[2].Content = "日本語";
            radioButtons[3].Content = "한국어";
            radioButtons[4].Content = isZh ? "跟随游戏" : "Follow Global";
        }

        // Music Page
        MusicPageTitleTextBlock.Text = isZh ? "OmniMix 音乐集成" : "OmniMix Music Integration";
        MusicPageDescriptionTextBlock.Text = isZh
            ? "将 OmniMix 输出的音乐送入游戏原生 Wwise 音乐总线。曲库、播放列表与后端仍由 OmniMix 管理。"
            : "Streams OmniMix audio into native Wwise music bus. Tracks, playlists, and playback remain managed by OmniMix.";
        MusicReplacementToggle.Header = isZh ? "启用音乐替换" : "Enable Music Replacement";
        MusicReplacementToggle.OffContent = isZh ? "保持游戏原生音乐" : "Keep game native music";
        MusicReplacementToggle.OnContent = isZh ? "使用 OmniMix 音乐流" : "Stream OmniMix audio";
        MusicBackendSectionTitle.Text = isZh ? "OmniMix 后端" : "OmniMix Backend";
        MusicBackendSectionHint.Text = isZh
            ? "Better Endfield 只保存后端绝对路径，不复制或修改 OmniMix 文件。"
            : "Better Endfield only saves the backend executable path without modifying OmniMix files.";
        OmniMixBackendPathBox.PlaceholderText = isZh ? "选择 OmniMix 后端程序" : "Select OmniMix backend executable";
        UnregisterOmniMixButton.Content = isZh ? "解除注册" : "Unregister";
        MusicScopeSectionTitle.Text = isZh ? "替换范围" : "Replacement Channels";
        MusicScopeSectionHint.Text = isZh
            ? "关闭的场景继续使用游戏原生音乐。"
            : "Disabled channels will continue using native game background music.";
        ReplaceLoginMusicToggle.Header = isZh ? "登录场景" : "Title / Login Screen";
        ReplaceLoginMusicToggle.OffContent = isZh ? "原生音乐" : "Native Music";
        ReplaceLoginMusicToggle.OnContent = "OmniMix";
        ReplaceMetaMusicToggle.Header = isZh ? "主界面与基地" : "Main Menu & Base";
        ReplaceMetaMusicToggle.OffContent = isZh ? "原生音乐" : "Native Music";
        ReplaceMetaMusicToggle.OnContent = "OmniMix";
        ReplaceGameplayMusicToggle.Header = isZh ? "游戏内场景" : "In-Game & Combat";
        ReplaceGameplayMusicToggle.OffContent = isZh ? "原生音乐" : "Native Music";
        ReplaceGameplayMusicToggle.OnContent = "OmniMix";
        MusicBufferSectionTitle.Text = isZh ? "缓冲与回退" : "Buffering & Fallback";
        MusicBufferSectionHint.Text = isZh
            ? "只有 PCM 达到预缓冲且 Wwise 回调正常后，原游戏音乐才会暂停。"
            : "Original game audio is paused only when PCM buffer threshold and Wwise callbacks are healthy.";
        MusicTargetLatencyNumberBox.Header = isZh ? "OmniMix 目标延迟 (秒)" : "Target Latency (s)";
        MusicPrebufferNumberBox.Header = isZh ? "本地预缓冲 (毫秒)" : "Local Prebuffer (ms)";
        FallbackToNativeMusicToggle.Header = isZh ? "故障时恢复原游戏音乐" : "Fallback to Native on Failure";
        FallbackToNativeMusicToggle.OffContent = isZh ? "保持静音" : "Stay silent";
        FallbackToNativeMusicToggle.OnContent = isZh ? "自动恢复" : "Auto fallback";
        MusicDiagnosticsToggle.Header = isZh ? "详细诊断日志" : "Detailed Diagnostics Log";
        MusicDiagnosticsToggle.OffContent = isZh ? "常规日志" : "Normal logging";
        MusicDiagnosticsToggle.OnContent = isZh ? "记录流状态与缓冲统计" : "Log stream & buffer telemetry";

        // Combat Page
        CombatPageTitleTextBlock.Text = isZh ? "战斗数据" : "Combat Statistics";
        CombatPageDescriptionTextBlock.Text = isZh
            ? "记录战斗伤害并按角色、技能和伤害类型汇总。统计默认关闭，不会修改伤害计算。"
            : "Track combat damage grouped by character, skill, and damage category. Does not alter damage calculations.";
        CombatStatsToggle.Header = isZh ? "启用战斗数据统计" : "Enable Combat Statistics";
        CombatStatsToggle.OffContent = isZh ? "关闭" : "Disabled";
        CombatStatsToggle.OnContent = isZh ? "按快捷键记录" : "Record via hotkey";
        CombatDisplaySectionTitle.Text = isZh ? "显示与快捷键" : "Display & Hotkeys";
        CombatDisplaySectionHint.Text = isZh
            ? "F11 切换开始/停止记录，F12 切换悬浮窗；均可在下方修改。按住 Ctrl 并用鼠标左键拖动悬浮窗可调整位置。"
            : "F11 toggles recording, F12 toggles overlay. Hold Ctrl and drag with left mouse button to reposition overlay.";
        HideDamageNumbersToggle.Header = isZh ? "隐藏伤害数字" : "Hide Damage Numbers";
        HideDamageNumbersToggle.OffContent = isZh ? "显示游戏数字" : "Show damage numbers";
        HideDamageNumbersToggle.OnContent = isZh ? "隐藏数字" : "Hide damage numbers";
        CombatOverlayToggle.Header = isZh ? "显示战斗悬浮窗" : "Show In-Game HUD Overlay";
        CombatOverlayToggle.OffContent = isZh ? "不启动悬浮窗" : "Do not launch overlay";
        CombatOverlayToggle.OnContent = isZh ? "随模块自动启动" : "Launch automatically";
        CombatRdpsDisplayToggle.Header = isZh ? "悬浮窗与本地排行口径" : "Ranking & Overlay Metric";
        CombatToggleHotkeyBox.Header = isZh ? "开始/停止记录快捷键" : "Start/Stop Recording Hotkey";
        CombatOverlayHotkeyBox.Header = isZh ? "显示/隐藏悬浮窗快捷键" : "Show/Hide Overlay Hotkey";
        CombatRecordSectionTitle.Text = isZh ? "记录方式" : "Recording Protocol";
        CombatRecordSectionHint.Text = isZh
            ? "schema 11 仅保存可验证的操作与原子结果；64 位实例 ID 使用字符串保存，无法确定的归属明确标为未知。"
            : "Schema 11 persists verifiable atomic events; 64-bit instance IDs are stored as strings with strict attribution.";
        AutoDungeonSessionToggle.Header = isZh ? "自动记录" : "Auto-Dungeon Recording";
        AutoDungeonSessionToggle.OffContent = isZh ? "手动快捷键" : "Manual hotkey only";
        AutoDungeonSessionToggle.OnContent = isZh ? "进关卡自动开启 / 结算自动保存" : "Auto-start on dungeon enter / auto-save on finish";
        CombatHistorySectionTitle.Text = isZh ? "历史统计" : "Combat History";
        CombatHistorySectionHint.Text = isZh
            ? "默认显示最近三条；可按日期和参战角色筛选。"
            : "Showing recent 3 sessions by default; filter by date range and team characters.";
        CombatWebButtonTextBlock.Text = isZh ? "打开解析网页" : "Open Web Visualizer";
        CombatRecordsFolderButtonTextBlock.Text = isZh ? "记录文件夹" : "Open Log Folder";
        CombatRefreshButtonTextBlock.Text = isZh ? "刷新" : "Refresh";
        CombatFromDatePicker.Header = isZh ? "开始日期" : "Start Date";
        CombatFromDatePicker.PlaceholderText = isZh ? "不限" : "Any";
        CombatToDatePicker.Header = isZh ? "结束日期" : "End Date";
        CombatToDatePicker.PlaceholderText = isZh ? "不限" : "Any";
        CombatDateFilterHintTextBlock.Text = isZh ? "日期范围两端均可留空。" : "Date range boundaries can be left empty.";
        CombatTeamFilterHintTextBlock.Text = isZh
            ? "队伍角色（最多四个；记录需包含全部已选角色）"
            : "Team Characters (up to 4; sessions must contain all selected characters)";
        CombatCharacterFilter1ComboBox.PlaceholderText = isZh ? "角色 1" : "Operator 1";
        CombatCharacterFilter2ComboBox.PlaceholderText = isZh ? "角色 2" : "Operator 2";
        CombatCharacterFilter3ComboBox.PlaceholderText = isZh ? "角色 3" : "Operator 3";
        CombatCharacterFilter4ComboBox.PlaceholderText = isZh ? "角色 4" : "Operator 4";
        CombatSelectedSessionSectionTitle.Text = isZh ? "记录明细" : "Session Breakdown";
        CombatSelectedSessionTextBlock.Text = isZh ? "选择一条记录查看。" : "Select a session record to view breakdown.";
        CombatSessionsEmptyTextBlock.Text = isZh ? "没有符合筛选条件的战斗统计。" : "No combat sessions match the filter criteria.";
        CombatBreakdownEmptyTextBlock.Text = isZh ? "当前记录没有可显示的角色明细。" : "No character breakdown available for this record.";

        // UI Enhancement Page
        UiPageTitleTextBlock.Text = isZh ? "界面增强" : "Touch & UI Enhancements";
        UiPageDescriptionTextBlock.Text = isZh
            ? "自定义游戏界面布局、画面遮挡与交互模式。各项默认关闭，切换后会立即保存。"
            : "Customize HUD layout, overlays, and touch simulation. Disabled by default; changes apply immediately.";
        MobileUiSectionTitle.Text = isZh ? "移动端 UI 模式" : "Mobile Touch UI Layout";
        MobileUiSectionHint.Text = isZh
            ? "在电脑端开启手机版 UI（移动端虚拟摇杆、技能轮盘与触控布局）。保留 PC 端常规登录；切换后会自动保存，已启动的游戏会刷新界面。"
            : "Enable mobile touch layout (virtual joystick, action wheels, touch UI) on PC. Retains standard PC login flow.";
        MobileUiToggle.Header = isZh ? "使用手机版 UI" : "Use Mobile Touch Layout";
        MobileUiToggle.OffContent = isZh ? "原生 PC 界面" : "Native PC UI";
        MobileUiToggle.OnContent = isZh ? "手机版触控界面" : "Mobile Touch UI";
        HideUidSectionTitle.Text = isZh ? "UID 水印" : "Account UID Watermark";
        HideUidSectionHint.Text = isZh ? "隐藏游戏画面中的账号 UID 水印。" : "Hide the account UID watermark from game rendering.";
        HideUidToggle.Header = isZh ? "隐藏 UID 水印" : "Hide UID Watermark";
        HideUidToggle.OffContent = isZh ? "显示 UID" : "Show UID";
        HideUidToggle.OnContent = isZh ? "隐藏 UID" : "Hide UID";
        HideHudSectionTitle.Text = isZh ? "HUD 显示" : "In-Game HUD Visibility";
        HideHudSectionHint.Text = isZh
            ? "通过热键隐藏或恢复游戏内 MainHudRoot 下的全部 HUD 画布，不影响菜单和设置界面。"
            : "Toggle in-game HUD canvases under MainHudRoot via hotkey without affecting menus or pause screens.";
        HideHudToggle.Header = isZh ? "启用隐藏全部 HUD 热键" : "Enable Toggle HUD Hotkey";
        HideHudToggle.OffContent = isZh ? "关闭" : "Disabled";
        HideHudToggle.OnContent = isZh ? "允许热键切换" : "Enabled";
        HideHudHotkeyBox.Header = isZh ? "HUD 显示切换热键" : "HUD Toggle Hotkey";
        HideHudHotkeyBox.PlaceholderText = isZh ? "例如 0、F10、NUMPAD0" : "e.g. 0, F10, NUMPAD0";

        // Camera Page
        CameraPageTitleTextBlock.Text = isZh ? "相机增强" : "Camera Enhancements";
        CameraPageDescriptionTextBlock.Text = isZh
            ? "自由控制游戏相机，并调整镜头相关的画面效果。各项默认关闭，修改后立即保存。"
            : "Free camera control and visual tuning. Disabled by default; changes apply immediately.";
        CameraFreeInfoBar.Title = isZh ? "自由视角操作" : "Free Camera Controls";
        CameraFreeInfoBar.Message = isZh
            ? "启用后按设置的热键进入或退出。方向键前后左右移动，PageUp/PageDown 升降；视角旋转继续使用游戏原生鼠标控制。切换场景或主相机时会自动退出。"
            : "Press configured hotkey to enter/exit. Arrow keys move camera, PageUp/PageDown elevates; mouse rotates native view. Exits automatically on scene transitions.";
        CameraFreeSectionTitle.Text = isZh ? "自由视角" : "Free Camera";
        CameraFreeSectionHint.Text = isZh
            ? "进入时捕获主相机的位置和视野，退出后恢复原值。模块只接管相机位置，鼠标旋转仍由游戏原生相机逻辑处理。"
            : "Captures main camera transform on entry and restores on exit. Mod handles translation while mouse rotates.";
        FreeCameraToggle.Header = isZh ? "启用自由视角功能" : "Enable Free Camera";
        FreeCameraToggle.OffContent = isZh ? "关闭" : "Disabled";
        FreeCameraToggle.OnContent = isZh ? "允许热键切换" : "Enabled";
        PauseGameInFreeCameraToggle.Header = isZh ? "进入自由视角时暂停游戏" : "Pause Game in Free Camera";
        PauseGameInFreeCameraToggle.OffContent = isZh ? "游戏继续运行" : "Game keeps running";
        PauseGameInFreeCameraToggle.OnContent = isZh ? "暂停角色与世界" : "Freeze world & entities";
        FreeCameraMovementSpeedNumberBox.Header = isZh ? "移动速度" : "Movement Speed";
        FreeCameraHotkeyBox.Header = isZh ? "切换热键" : "Toggle Hotkey";
        FreeCameraHotkeyBox.PlaceholderText = isZh ? "例如 9、F9、NUMPAD9" : "e.g. 9, F9, NUMPAD9";
        FreeCameraFieldOfViewNumberBox.Header = isZh ? "视野（FOV）" : "Field of View (FOV)";
        CameraVisualSectionTitle.Text = isZh ? "镜头画面" : "Visual & Occlusion";
        CameraVisualSectionHint.Text = isZh
            ? "调用游戏自身的清理逻辑，移除镜头贴近角色时出现的半透明虚化。"
            : "Invokes native engine cleanup to eliminate character mesh dithering/transparency on close camera.";
        DisableDitherToggle.Header = isZh ? "移除角色近距离虚化" : "Disable Character Mesh Dither";
        DisableDitherToggle.OffContent = isZh ? "保留原效果" : "Keep default dithering";
        DisableDitherToggle.OnContent = isZh ? "关闭虚化" : "Remove dithering";

        // Display Page
        DisplayPageTitleTextBlock.Text = isZh ? "显示增强" : "Display & Upscaling Pipeline";
        DisplayPageDescriptionTextBlock.Text = isZh
            ? "通过 OptiScaler 接管客户端的画质提升选项，为 AMD 与 Intel 显卡提供客户端本身不支持的超分方案。"
            : "Leverages OptiScaler to intercept upscaling, offering DLSS/FSR/XeSS upscaling on all GPUs.";
        DisplayWarningInfoBar.Title = isZh ? "会写入游戏目录" : "Writes to Game Directory";
        DisplayWarningInfoBar.Message = isZh
            ? "启用会向 Endfield.exe 所在目录写入 OptiScaler 组件（含 dxgi.dll）与归属记录，不会覆盖未知同名文件。客户端带有内核级反作弊，向游戏目录写入文件比运行时注入更容易被静态扫描发现，是否使用请自行判断。"
            : "Installs OptiScaler binaries (dxgi.dll) into the game folder. Game features kernel anti-cheat; writing files to game directory has different risk profiles than memory injection.";
        DisplayAdapterSectionTitle.Text = isZh ? "显示适配器" : "Graphics Hardware";
        DisplayBackendComboBox.Header = isZh ? "超分后端" : "Upscaler Backend";
        DisplaySpoofingToggle.Header = isZh ? "GPU 欺骗" : "GPU Architecture Spoofing";
        DisplaySpoofingToggle.OffContent = isZh ? "关闭" : "Disabled";
        DisplaySpoofingToggle.OnContent = isZh ? "开启" : "Enabled";
        DisplaySpoofingHintTextBlock.Text = isZh
            ? "客户端的 FSR3 为引擎内置、没有可拦截的 FidelityFX 接口，因此非 NVIDIA 显卡必须开启欺骗让客户端暴露 DLSS 选项，OptiScaler 才能接管。开启后请在客户端【设置-性能与画面-画质提升】中选择 NVIDIA DLSS。"
            : "Non-NVIDIA GPUs require spoofing so game unlocks DLSS settings menu for OptiScaler interception. Choose NVIDIA DLSS in game Graphic settings.";
        DisplayDiagnosticsToggle.Header = isZh ? "诊断日志" : "Diagnostic Logging";
        DisplayDiagnosticsToggle.OffContent = isZh ? "关闭" : "Disabled";
        DisplayDiagnosticsToggle.OnContent = isZh ? "写入 OptiScaler 日志到游戏目录" : "Write OptiScaler log to game folder";
        DisplayDeploySectionTitle.Text = isZh ? "组件部署" : "Deployment";
        DisplayStatusInfoBar.Title = isZh ? "部署状态" : "Deployment Status";
        InstallDisplayButtonTextBlock.Text = isZh ? "部署或更新" : "Deploy / Update";
        ApplyDisplayButtonTextBlock.Text = isZh ? "应用配置" : "Apply Config";
        UninstallDisplayButtonTextBlock.Text = isZh ? "从游戏目录卸载" : "Uninstall from Game";
        DisplayUsageSectionTitle.Text = isZh ? "使用须知" : "Notes & Compatibility";
        DisplayUsageNotice1TextBlock.Text = isZh
            ? "OptiScaler 覆盖层由 Insert 键呼出，与战斗数据模块的 F11/F12 不冲突。帧生成不会启用：客户端已有原生 DLSS 帧生成。FSR4 在客户端的 DX11 与 Vulkan 后端下都经 DX12 interop 运行，会额外承担一层同步开销。"
            : "Press Insert in-game to toggle OptiScaler overlay menu. Frame generation is not replaced since game provides native DLSS frame gen.";
        DisplayUsageNotice2TextBlock.Text = isZh
            ? "OptiScaler 为 GPL-3.0 授权的独立开源项目，不随本软件分发，需自行放入软件目录下的 payloads/optiscaler。"
            : "OptiScaler is licensed under GPL-3.0 and not bundled directly; place payloads into payloads/optiscaler folder.";

        // Settings Page
        SettingsPageTitleTextBlock.Text = isZh ? "设置" : "Global Settings";
        SettingsPageDescriptionTextBlock.Text = isZh
            ? "管理运行路径、加载方式、启动参数、外观和桌面入口。"
            : "Configure application paths, loader mode, launch arguments, theme, and shortcuts.";
        SettingsPathsSectionTitle.Text = isZh ? "运行路径" : "Application & Game Paths";
        SettingsPathsSectionHint.Text = isZh
            ? "首次启动会自动扫描；移动游戏或软件后可重新扫描。"
            : "Scanned automatically on launch; re-scan if game or launcher location changes.";
        ScanPathsButtonTextBlock.Text = isZh ? "重新扫描" : "Rescan Paths";
        GamePathBox.Header = isZh ? "游戏程序" : "Game Executable";
        GameLaunchArgumentsBox.Header = isZh ? "游戏启动参数（可选）" : "Game Launch Arguments (Optional)";
        GameLaunchArgumentsBox.PlaceholderText = isZh ? "例如：-force-d3d11" : "e.g. -force-d3d11";
        GameLaunchArgumentsHintTextBlock.Text = isZh
            ? "参数将原样传给 Endfield.exe；-force-d3d11 会使用 Direct3D 11 启动。"
            : "Arguments are passed directly to Endfield.exe. E.g. -force-d3d11 forces Direct3D 11 backend.";
        InjectorPathBox.Header = isZh ? "注入器" : "Mod Injector Path";
        CurrentConfigLabelTextBlock.Text = isZh ? "当前配置文件" : "Active Configuration File";
        OpenConfigFolderButtonTextBlock.Text = isZh ? "配置目录" : "Config Directory";
        OpenLogButtonTextBlock.Text = isZh ? "运行日志" : "Runtime Logs";
        LoaderModeSectionTitle.Text = isZh ? "加载方式" : "Loader & Injection Mode";
        InjectorModeTitleTextBlock.Text = isZh ? "内置注入器" : "Built-in Injector";
        InjectorModeHintTextBlock.Text = isZh ? "不写入游戏目录" : "Leaves game folder intact";
        XInputModeTitleTextBlock.Text = isZh ? "XInput 自启动" : "XInput Auto-load";
        XInputModeHintTextBlock.Text = isZh ? "随游戏自动加载" : "Loads automatically with game";
        XInputWarningInfoBar.Title = isZh ? "会写入游戏目录" : "Writes to Game Directory";
        XInputWarningInfoBar.Message = isZh
            ? "需要兼容其他加载器，或希望通过官方启动器、桌面快捷方式自启动时使用。安装会向 Endfield.exe 所在目录写入 xinput1_4.dll 和归属记录；不会覆盖未知同名文件。"
            : "Used for launching directly via official client or shortcuts. Deploys xinput1_4.dll into game folder.";
        XInputStatusInfoBar.Title = isZh ? "XInput 状态" : "XInput Status";
        InstallXInputButtonTextBlock.Text = isZh ? "安装或更新" : "Install / Update";
        UninstallXInputButtonTextBlock.Text = isZh ? "从游戏目录卸载" : "Uninstall from Game";
        AppearanceSectionTitle.Text = isZh ? "外观与语言" : "Appearance & Language";
        LanguageComboBox.Header = isZh ? "界面语言 / Language" : "Language / 界面语言";
        ThemeComboBox.Header = isZh ? "应用主题" : "Theme";
        ThemeItemDefault.Content = isZh ? "跟随系统" : "System Default";
        ThemeItemLight.Content = isZh ? "浅色" : "Light";
        ThemeItemDark.Content = isZh ? "深色" : "Dark";
        ShortcutsSectionTitle.Text = isZh ? "桌面快捷方式" : "Desktop Shortcuts";
        AppShortcutTitleTextBlock.Text = "Better Endfield";
        AppShortcutHintTextBlock.Text = isZh
            ? "在桌面创建本控制器的启动入口。"
            : "Create a desktop shortcut for Better Endfield launcher.";
        CreateAppShortcutButtonTextBlock.Text = isZh ? "创建" : "Create";
        GameShortcutTitleTextBlock.Text = isZh ? "一键启动终末地" : "Quick Launch Endfield";
        GameShortcutHintTextBlock.Text = isZh
            ? "使用当前加载方式、游戏路径和启动参数创建桌面入口。"
            : "Create a one-click desktop shortcut with current launcher mode and parameters.";
        CreateGameShortcutButtonTextBlock.Text = isZh ? "创建" : "Create";

        // About Page
        AboutAppTitleTextBlock.Text = "Better Endfield";
        AboutAppSubtitleTextBlock.Text = isZh
            ? "终末地登录场景模型与角色配音控制器"
            : "Endfield Title Scene Models & Voice Router Controller";
        CurrentVersionTextBlock.Text = $"{(isZh ? "版本" : "Version")} {UpdateService.CurrentVersion}";
        AboutUpdateSectionTitle.Text = isZh ? "软件更新" : "Software Updates";
        AboutUpdateSectionHint.Text = isZh
            ? "仅在点击检查时访问 GitHub Releases，不会后台自动联网。"
            : "Connects to GitHub Releases only when manually checked; no background network tracking.";
        CheckUpdatesButtonTextBlock.Text = isZh ? "检查更新" : "Check for Updates";
        OpenReleaseButtonTextBlock.Text = isZh ? "打开下载页" : "Open Release Page";
        AboutProjectSectionTitle.Text = isZh ? "项目" : "Project";
        OpenRepoButtonTextBlock.Text = isZh ? "GitHub 仓库" : "GitHub Repository";
        OpenReleasesButtonTextBlock.Text = isZh ? "全部版本" : "All Releases";
        OpenLicenseButtonTextBlock.Text = isZh ? "GNU AGPL v3.0 开源许可" : "GNU AGPL v3.0 License";
        AboutAuthorSectionTitle.Text = isZh ? "作者与交流" : "Author & Community";
        BilibiliLabelTextBlock.Text = "Bilibili";
        OpenBilibiliButtonTextBlock.Text = isZh ? "打开 B站主页" : "Bilibili Space";
        XiaoheiheLabelTextBlock.Text = isZh ? "小黑盒" : "Heybox";
        OpenXiaoheiheButtonTextBlock.Text = isZh ? "打开小黑盒主页" : "Heybox Profile";
        QqGroupLabelTextBlock.Text = isZh ? "QQ群" : "QQ Group";
        ToolTipService.SetToolTip(CopyQqGroupButton, isZh ? "复制QQ群号" : "Copy QQ Group Number");
        AboutDisclaimerSectionTitle.Text = isZh ? "风险与免责声明" : "Disclaimer & Terms";
        AboutDisclaimerHintTextBlock.Text = isZh
            ? "本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关。注入和运行时修改可能造成游戏崩溃、版本不兼容或账号风险。"
            : "This is an unofficial experimental project not affiliated with Hypergryph or GRYPHLINE. Use at your own risk.";
        ViewDisclaimerButton.Content = isZh ? "查看完整说明" : "View Full Disclaimer";

        // Bottom Action Bar
        PageSelectionHintTextBlock.Text = isZh
            ? "保存后在下一次注入时生效。"
            : "Changes will take effect on next game launch/injection.";
        ResetButtonTextBlock.Text = isZh ? "恢复默认" : "Reset to Defaults";
        SaveButtonTextBlock.Text = isZh ? "保存" : "Save";
        LaunchButtonTextBlock.Text = isZh ? "保存并启动" : "Save & Launch";

        if (FinalActionComboBox.SelectedItem is ActionOption selectedAction)
        {
            UpdateActionMetadata(selectedAction);
            ActionDescriptionTextBlock.Text = isZh
                ? $"资源哈希：{selectedAction.PathHash} · 原生 LoopTime：{(selectedAction.NativeLoop ? "是" : "否")}"
                : $"Asset Hash: {selectedAction.PathHash} · Native LoopTime: {(selectedAction.NativeLoop ? "Yes" : "No")}";
        }

        PresetOptions.RefreshCharacterDisplayNames();
        string? selectedModelCharacterId = (CharacterComboBox.SelectedItem as CharacterOption)?.Id;
        CharacterComboBox.ItemsSource = null;
        CharacterComboBox.ItemsSource = PresetOptions.Characters;
        CharacterComboBox.SelectedItem = PresetOptions.Characters.FirstOrDefault(
            c => string.Equals(c.Id, selectedModelCharacterId, StringComparison.OrdinalIgnoreCase))
            ?? PresetOptions.Characters.FirstOrDefault();

        RefreshVoiceCharactersList();
        RefreshVoiceRulesDisplay();

        UpdateOmniMixStatusTexts();
        UpdateColorPickerLabels();
        UpdateCombatCategoryLegend();
        RebuildCombatCharacterFilters();
        ApplyCombatFilters(_selectedCombatSession?.Path);
        UpdatePathStatusText();
        _ = RefreshDisplayStatusAsync();
        _ = RefreshXInputStatusAsync();
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _statusTimer.Stop();
    }
}
