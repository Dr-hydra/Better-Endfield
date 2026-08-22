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
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Ellipse = Microsoft.UI.Xaml.Shapes.Ellipse;
using Line = Microsoft.UI.Xaml.Shapes.Line;
using Rectangle = Microsoft.UI.Xaml.Shapes.Rectangle;
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
    private sealed record CombatTimelineSeries(
        string Label,
        Brush Brush,
        Func<CombatTimelinePoint, double> Value);

    private const string DisclaimerVersion = "1";
    private const string WindowIconResourceName =
        "BetterEndfield.UI.Assets.shared.gilberta.ico";
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
    private readonly ObservableCollection<CombatSessionRecord> _combatSessions = [];
    private readonly ObservableCollection<CombatCharacterFilterChoice> _combatCharacterFilters = [];
    private readonly ObservableCollection<CombatLegendItem> _combatCategoryLegend = [];
    private readonly ObservableCollection<CombatLegendItem> _combatTimelineLegend = [];
    private IReadOnlyList<CombatSessionRecord> _allCombatSessions = [];
    private CombatSessionRecord? _selectedCombatSession;
    private bool _combatHistoryExpanded;
    private bool _updatingCombatHistory;
    private double _timelineRangeStart;
    private double _timelineRangeEnd = 1.0;
    private int _timelineDraggingHandle;
    private double? _combatTimelineComboScrollOffset;
    private readonly IReadOnlyList<VoiceCharacterChoice> _voiceCharacters;
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
        CombatSessionsListView.ItemsSource = _combatSessions;
        foreach (ComboBox comboBox in CombatCharacterFilterBoxes())
            comboBox.ItemsSource = _combatCharacterFilters;
        for (int category = 0; category < CombatSkillCategories.Count; ++category)
        {
            _combatCategoryLegend.Add(new CombatLegendItem
            {
                Label = CombatSkillCategories.Names[category],
                Brush = CombatHistoryService.CategoryBrush(category)
            });
        }
        CombatCategoryLegendItemsControl.ItemsSource = _combatCategoryLegend;
        CombatTimelineLegendItemsControl.ItemsSource = _combatTimelineLegend;
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
        MusicPageScrollViewer.Visibility = page == "music"
            ? Visibility.Visible
            : Visibility.Collapsed;
        CombatPageScrollViewer.Visibility = page == "combat"
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
            "music" => "首次启用在下一次注入时加载；已加载模块的设置会热更新。",
            "combat" => "F11 切换记录，F12 切换悬浮窗；结果会保存到本机目录。",
            _ => "角色与动画参数保存后在下一次注入时生效。"
        };
    }

    private void RefreshCombatSessionsButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshCombatSessions();
    }

    private void RefreshCombatSessions()
    {
        string directory = Path.Combine(
            ConfigurationService.SettingsDirectory, "combat-sessions");
        string? selectedPath = _selectedCombatSession?.Path;
        _allCombatSessions = CombatHistoryService.Load(directory);
        _combatHistoryExpanded = false;
        RebuildCombatCharacterFilters();
        ApplyCombatFilters(selectedPath);
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
                DisplayName = "不限"
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
        ExpandCombatSessionsButton.Content = _combatHistoryExpanded
            ? "收起，仅显示最近三条"
            : $"展开其余 {hidden} 条";
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
        _timelineRangeStart = 0;
        _timelineRangeEnd = 1;
        RenderCombatTimeline();
    }

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
                    ? "XInput 自启动已就绪"
                    : "注入器已启动",
                loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase)
                    ? "游戏将通过 xinput1_4.dll 加载 Better Endfield Host。"
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

    private async void CreateGameShortcutButton_Click(object sender, RoutedEventArgs e)
    {
        if (!await SaveAsync(showSuccess: false))
        {
            return;
        }

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
                "一键启动快捷方式已创建",
                $"已保存当前配置并创建：{shortcutPath}",
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
        string gamePath = GamePathBox.Text.Trim();
        if (!RuntimePathDiscoveryService.IsGameExecutable(gamePath))
        {
            ShowStatus("游戏路径无效", "请选择有效的 Endfield.exe。", InfoBarSeverity.Error);
            return false;
        }

        string injectorPath = InjectorPathBox.Text.Trim();
        if (!File.Exists(injectorPath))
        {
            ShowStatus("注入器路径无效", "请选择有效的 BetterEndfield.Injector.exe。", InfoBarSeverity.Error);
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
            ShowStatus("软件目录不完整", exception.Message, InfoBarSeverity.Error);
            return false;
        }

        if (!TryReadConfiguration(out ModConfiguration configuration, out string? error))
        {
            ShowStatus("参数无效", error ?? "请检查输入值。", InfoBarSeverity.Error);
            return false;
        }

        if (!TryValidateLaunchArguments(out error))
        {
            ShowStatus("启动参数无效", error ?? "请检查游戏启动参数。", InfoBarSeverity.Error);
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
                    "正在准备配音资源",
                    "正在从本地游戏语言包生成所选角色的 catalog。",
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
                    "参数已保存",
                    "视觉、语言与已加载的音乐模块会在约 2 秒内热更新；模型、动画及首次启用的模块在下次注入时读取。",
                    InfoBarSeverity.Success);
            }

            return true;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
            InvalidDataException or InvalidOperationException or
            OmniMixRegistrationException)
        {
            ShowStatus("保存失败", exception.Message, InfoBarSeverity.Error);
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
        bool gameValid = RuntimePathDiscoveryService.IsGameExecutable(
            GamePathBox.Text.Trim());
        GamePathStatusTextBlock.Text = gameValid
            ? "已找到游戏程序。"
            : "未找到有效的 Endfield.exe。";

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
            ? "已找到完整的 runtime、modules 和注入器目录。"
            : "注入器或相邻 runtime/modules 目录不完整。";
    }

    private void UpdateLoaderModePanel()
    {
        bool xinput = GetSelectedLoaderMode().Equals(
            "xinput",
            StringComparison.OrdinalIgnoreCase);
        XInputManagementPanel.Visibility = xinput
            ? Visibility.Visible
            : Visibility.Collapsed;
        LoaderModeDescriptionTextBlock.Text = xinput
            ? "游戏每次启动都会自动加载 Better Endfield。适合与其他加载器共存，或通过官方启动器直接启动。"
            : "由 Better Endfield 启动游戏并在启动阶段加载 Host，游戏目录保持不变。";
        PageSelectionHintTextBlock.Text = xinput
            ? "启动时会确保 XInput 代理已安装到游戏目录。"
            : "保存后在下一次注入时生效。";
        UpdatePathStatusText();
    }

    private async Task RefreshXInputStatusAsync()
    {
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
            XInputDeploymentState.Installed => "已安装",
            XInputDeploymentState.UpdateAvailable => "可更新",
            XInputDeploymentState.Conflict => "检测到冲突",
            XInputDeploymentState.NotInstalled => "未安装",
            _ => "暂不可用"
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
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus("游戏正在运行", "请退出游戏后再安装或更新 XInput 代理。", InfoBarSeverity.Warning);
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
            ShowStatus("XInput 已安装", status.Message, InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or
                InvalidDataException or InvalidOperationException)
        {
            ShowStatus("XInput 安装失败", exception.Message, InfoBarSeverity.Error);
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
            MinimumDamageNumberBox
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
            CombatToggleHotkey = toggleHotkey,
            CombatOverlayHotkey = overlayHotkey,
            RecordAllDamage = RecordAllDamageToggle.IsOn,
            IncludeOverkillDamage = IncludeOverkillToggle.IsOn,
            MinimumDamage = MinimumDamageNumberBox.Value,
            GroupDamageByCharacter = GroupDamageByCharacterToggle.IsOn,
            GroupDamageBySkill = GroupDamageBySkillToggle.IsOn,
            GroupDamageByCategory = GroupDamageByCategoryToggle.IsOn,
            SaveRawCombatEvents = SaveRawCombatEventsToggle.IsOn
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
        OmniMixRegistrationStatusTextBlock.Text =
            string.IsNullOrWhiteSpace(configuration.OmniMixBackendExe)
                ? "尚未注册 OmniMix 后端"
                : OmniMixRegistrationService.IsValidBackendPath(
                    configuration.OmniMixBackendExe)
                    ? "OmniMix 后端路径有效"
                    : "OmniMix 后端路径已失效";
        OmniMixClientIdTextBlock.Text = string.IsNullOrWhiteSpace(
            configuration.OmniMixClientId)
                ? "客户端标识将在注册时生成。"
                : $"客户端标识：{configuration.OmniMixClientId}";
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
        CombatToggleHotkeyBox.Text = configuration.CombatToggleHotkey;
        CombatOverlayHotkeyBox.Text = configuration.CombatOverlayHotkey;
        RecordAllDamageToggle.IsOn = configuration.RecordAllDamage;
        IncludeOverkillToggle.IsOn = configuration.IncludeOverkillDamage;
        MinimumDamageNumberBox.Value = configuration.MinimumDamage;
        GroupDamageByCharacterToggle.IsOn = configuration.GroupDamageByCharacter;
        GroupDamageBySkillToggle.IsOn = configuration.GroupDamageBySkill;
        GroupDamageByCategoryToggle.IsOn = configuration.GroupDamageByCategory;
        SaveRawCombatEventsToggle.IsOn = configuration.SaveRawCombatEvents;

        _initializing = wasInitializing;
        UpdateCrossfadePanel();
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
        OmniMixRegistrationStatusTextBlock.Text = status.Registered
            ? status.Valid
                ? string.IsNullOrWhiteSpace(status.BackendVersion)
                    ? "OmniMix 后端路径有效"
                    : $"OmniMix 后端 {status.BackendVersion}"
                : $"OmniMix 后端不可用：{status.Reason}"
            : "尚未注册 OmniMix 后端";
        OmniMixClientIdTextBlock.Text = string.IsNullOrWhiteSpace(status.ClientId)
            ? "客户端标识将在注册时生成。"
            : $"客户端标识：{status.ClientId}";
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

        string logPath = ConfigurationService.GetLogPath(InjectorPathBox.Text);
        if (File.Exists(logPath))
        {
            DateTime updated = File.GetLastWriteTime(logPath);
            LogStatusTextBlock.Text = $"日志更新：{updated:MM-dd HH:mm:ss}";
        }
        else
        {
            LogStatusTextBlock.Text = File.Exists(InjectorPathBox.Text.Trim())
                ? "注入器已就绪 · 尚无日志"
                : "未找到注入器";
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

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _statusTimer.Stop();
    }
}
