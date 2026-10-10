using System.Diagnostics;
using System.ComponentModel;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfieldNext.UI.Views;

public sealed partial class CustomModelPage : UserControl
{
    private readonly BemPackageService _service = new();
    private bool _rendering = true;
    private bool _importing;
    private bool _disablingAll;
    private bool _updatingCharacterFilter;
    private string _selectedCharacter = "";
    private bool _pendingParameters;
    private readonly DispatcherTimer _externalSettingsTimer = new() { Interval = TimeSpan.FromSeconds(1) };
    private BemConverterWindow? _converter;
    // Packages whose component options are expanded; kept across Render().
    private readonly HashSet<string> _expanded = [];
    private readonly List<Action> _localizeCards = [];
    public Func<string>? InstallRootProvider { get; set; }
    private string InstallRoot => InstallRootProvider?.Invoke() ?? (Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory);
    private string SelectionAppliedHint => _service.HotSwitch
        ? BemText.Get("已启用实验热切换的游戏将在下次切换配队或重新打开详情时更新；首次开启需重启游戏。")
        : BemText.Get("下次启动游戏生效。");

    public CustomModelPage()
    {
        InitializeComponent();
        _rendering = false;
        _externalSettingsTimer.Tick += (_, _) =>
        {
            if (Visibility != Visibility.Visible || XamlRoot?.IsHostVisible != true) return;
            if (_importing || _migrating || _disablingAll || _rendering || _pendingParameters || _service.IsSaving) return;
            try { if (_service.HasExternalChanges()) Reload(); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        };
        UpdatePageLanguage();
        Loaded += (_, _) =>
        {
            LocalizationService.Instance.PropertyChanged += PageLanguageChanged;
            UpdatePageLanguage();
            Reload();
            _externalSettingsTimer.Start();
        };
        Unloaded += (_, _) => { _externalSettingsTimer.Stop(); LocalizationService.Instance.PropertyChanged -= PageLanguageChanged; };
    }

    private void PageLanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(LocalizationService.IsChinese)) return;
        UpdatePageLanguage();
        BemLocalizedUI.Refresh(this);
        foreach (var update in _localizeCards) update();
    }

    private void UpdatePageLanguage()
    {
        PageTitle.Text = BemText.Get("角色与武器 · BEM");
        PageIntro.Text = BemText.Get("支持 BEM 1.0–1.4 模型包，管理角色、武器和技能形态。资源目标不重叠的模型包可以同时启用。");
        ImportButton.Content = BemText.Get("导入 BEM / ZIP");
        ConvertButton.Content = BemText.Get("BEM 创作者工具…");
        RefreshButton.Content = BemText.Get("刷新");
        PackageFolderButton.Content = BemText.Get("打开包目录");
        CharacterFilter.Header = BemText.Get("按角色或武器筛选");
        DisableAllButton.Content = BemText.Get("关闭全部模型");
        bool chinese = LocalizationService.Instance.IsChinese;
        ModelOverlayToggle.Header = chinese ? "模型悬浮窗" : "Model overlay";
        ModelOverlayVisibleToggle.Header = chinese ? "进入游戏时显示" : "Show at game start";
        ModelOverlayHotkeyBox.Header = chinese ? "显示／隐藏快捷键" : "Show/hide hotkey";
        UpdateCharacterFilter();
        LodToggle.Header = BemText.Get("锁定高精度 LOD");
        HotSwitchToggle.Header = BemText.Get("实验：模型热切换");
        CloneSupportToggle.Header = BemText.Get("实验：克隆模型支持");
        HotSwitchHint.Text = BemText.Get("开启后需重启游戏。之后切换包、外观、组件或应用滑条，在切换配队或重新打开详情时更新。会增加内存占用。");
        FastLoadingToggle.Header = BemText.Get("加载速度优先");
        SkipValidationToggle.Header = BemText.Get("实验：关闭模型校验");
        SkipValidationHint.Text = BemText.Get("开启后会跳过兼容性和容量校验，可能导致游戏崩溃或模型错乱，风险自行承担。重启游戏后生效。");
        EmptyHint.Text = BemText.Get("尚未导入模型包。已有其他格式？打开转换窗口查看支持范围与缺少的资料。");
        foreach (var toggle in new[] { LodToggle, HotSwitchToggle, CloneSupportToggle, FastLoadingToggle, SkipValidationToggle })
        {
            toggle.OnContent = BemText.Get("开启");
            toggle.OffContent = BemText.Get("关闭");
        }
        UpdateLodHint();
        bool isZh = LocalizationService.Instance.IsChinese;
        GetModelsTitle.Text = isZh ? BemText.Get("获取模型") : "Get models";
        QuarkModelsButton.Content = isZh ? BemText.Get("夸克网盘") : "Quark Drive";
        BaiduModelsButton.Content = isZh ? BemText.Get("百度网盘") : "Baidu Netdisk";
        KatfileModelsButton.Content = isZh ? BemText.Get("Katfile 网盘") : "Katfile";
        ModelDropTitle.Text = isZh ? BemText.Get("将 BEM 或 ZIP 模型包拖到这里") : "Drop BEM or ZIP model packages here";
        ModelDropHint.Text = isZh ? BemText.Get("支持同时拖入多个文件，按顺序导入；ZIP 内可选择要导入的包。")
            : "Drop multiple files to import them in order. Choose which packages to import from each ZIP.";
    }

    private void UpdateLodHint()
    {
        LodHint.Text = _service.Packages.Any(p => p.Enabled)
            ? BemText.Format("已启用 Mod，强制锁定 LOD。全部停用后恢复独立开关：{0}。",
                _service.StandaloneLod ? BemText.Get("开启") : BemText.Get("关闭"))
            : BemText.Get("没有 Mod 启用时可独立锁定高精度模型；AI 角色也会保持高精度 LOD。");
    }

    private void ModelSource_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string url }) return;
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch (Exception ex)
        {
            Message(() => LocalizationService.Instance.IsChinese ? BemText.Get("无法打开模型链接") : "Could not open model link",
                () => ex.Message, InfoBarSeverity.Error);
        }
    }

    private void Message(Func<string> title, Func<string> detail, InfoBarSeverity severity = InfoBarSeverity.Informational)
    {
        BemLocalizedUI.Set(Status, InfoBar.TitleProperty, title);
        BemLocalizedUI.Set(Status, InfoBar.MessageProperty, detail);
        Status.Severity = severity; Status.IsOpen = true;
    }

    private void Reload()
    {
        try
        {
            string? root = null;
            try { root = InstallRoot; } catch (Exception ex) when (ex is IOException or InvalidOperationException or UnauthorizedAccessException) { }
            _service.UseInstallRoot(root);
            _service.Load(); Render();
            if (_service.Notices.Count != 0) Message(() => BemText.Get("包管理提示"), () => string.Join("\n", _service.Notices), InfoBarSeverity.Warning);
            if (_service.HasLegacyPackages && !_migrating) _ = MigrateLegacyPackagesAsync();
        }
        catch (Exception ex) { Message(() => BemText.Get("读取失败"), () => ex.Message, InfoBarSeverity.Error); }
    }

    private bool _migrating;
    private async Task MigrateLegacyPackagesAsync()
    {
        _migrating = true;
        try
        {
            int moved = await _service.MigrateLegacyPackagesAsync();
            Render();
            if (moved > 0) Message(() => BemText.Get("模型包已移到程序目录"), () => _service.PackageDirectory);
        }
        catch (InvalidOperationException) { /* The game is running; the next refresh retries. */ }
        catch (Exception ex) { Message(() => BemText.Get("模型包迁移未完成"), () => ex.Message, InfoBarSeverity.Warning); }
        finally { _migrating = false; }
    }

    private static string TargetLabel(BemPackage package) => package.TargetKind == "weapon"
        ? BemText.Get("武器") + " · " + package.TargetId : PresetOptions.GetCharacterName(package.TargetId);

    private void UpdateCharacterFilter()
    {
        var targets = _service.Packages.DistinctBy(p => p.TargetKey).OrderBy(p => p.TargetKey).ToArray();
        if (!targets.Any(p => p.TargetKey == _selectedCharacter)) _selectedCharacter = "";
        _updatingCharacterFilter = true;
        try
        {
            CharacterFilter.Items.Clear();
            CharacterFilter.Items.Add(new ComboBoxItem { Content = BemText.Get("全部模型"), Tag = "" });
            foreach (var target in targets)
                CharacterFilter.Items.Add(new ComboBoxItem { Content = TargetLabel(target), Tag = target.TargetKey });
            CharacterFilter.SelectedItem = CharacterFilter.Items.Cast<ComboBoxItem>().First(item => (string)item.Tag == _selectedCharacter);
            CharacterFilter.IsEnabled = targets.Length > 0;
        }
        finally { _updatingCharacterFilter = false; }
        ApplyCharacterFilter();
    }

    private void CharacterFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_updatingCharacterFilter || CharacterFilter.SelectedItem is not ComboBoxItem { Tag: string id }) return;
        _selectedCharacter = id;
        ApplyCharacterFilter();
    }

    private void ApplyCharacterFilter()
    {
        foreach (var card in PackageCards.Children.Cast<FrameworkElement>())
            card.Visibility = _selectedCharacter.Length == 0 || Equals(card.Tag, _selectedCharacter) ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateDisableAllButton() => DisableAllButton.IsEnabled = !_importing && !_disablingAll && _service.Packages.Any(p => p.Enabled);

    private async void DisableAll_Click(object sender, RoutedEventArgs e)
    {
        if (_importing || _disablingAll) return;
        _disablingAll = true;
        UpdateDisableAllButton();
        try
        {
            await _service.DisableAllAsync();
            Render();
            Message(() => BemText.Get("已关闭全部模型"), () => "", InfoBarSeverity.Success);
        }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
        finally { _disablingAll = false; UpdateDisableAllButton(); }
    }

    private void Render()
    {
        _rendering = true;
        _pendingParameters = false;
        try
        {
            SkipValidationToggle.IsOn = _service.SkipValidation;
            HotSwitchToggle.IsOn = _service.HotSwitch;
            CloneSupportToggle.IsOn = _service.CloneSupport;
            FastLoadingToggle.IsOn = _service.FastLoading;
            ModelOverlayToggle.IsOn = _service.ModelOverlayEnabled;
            ModelOverlayVisibleToggle.IsOn = _service.ModelOverlayVisible;
            ModelOverlayHotkeyBox.Text = _service.ModelOverlayHotkey.Replace("PLUS", "=", StringComparison.Ordinal);
            bool forced = _service.Packages.Any(p => p.Enabled);
            LodToggle.IsOn = _service.EffectiveLod; LodToggle.IsEnabled = !forced;
            UpdateLodHint();
            UpdateCharacterFilter();
            UpdateDisableAllButton();
            EmptyHint.Visibility = _service.Packages.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            PackageCards.Children.Clear();
            _localizeCards.Clear();
            foreach (var p in _service.Packages.OrderBy(p => p.TargetKey).ThenBy(p => p.Name))
            {
                var stack = new StackPanel { Spacing = 10 };
                var header = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
                if (p.TargetKind == "character") header.Children.Add(new Image { Source = GachaIconService.Load(p.TargetId), Width = 56, Height = 56 });
                var labels = new StackPanel { Spacing = 4 };
                var packageTitle = new TextBlock { FontSize = 20, TextWrapping = TextWrapping.Wrap };
                BemLocalizedUI.Set(packageTitle, TextBlock.TextProperty,
                    () => TargetLabel(p) + " · " + p.Name);
                labels.Children.Add(packageTitle);
                labels.Children.Add(new TextBlock { Text = $"{p.Author}  /  {p.Version}  /  {p.Size / 1_000_000.0:F1} MB", Opacity = 0.7 });
                header.Children.Add(labels); stack.Children.Add(header);
                var controls = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 16 };
                var enabled = new ToggleSwitch { Header = BemText.Get("启用此包"), IsOn = p.Enabled };
                BemLocalizedUI.Set(enabled, ToggleSwitch.HeaderProperty, () => BemText.Get("启用此包"));
                BemLocalizedUI.Set(enabled, ToggleSwitch.OnContentProperty, () => BemText.Get("开启"));
                BemLocalizedUI.Set(enabled, ToggleSwitch.OffContentProperty, () => BemText.Get("关闭"));
                enabled.Toggled += async (_, _) =>
                {
                    if (_rendering) return;
                    try
                    {
                        await _service.SetEnabledAsync(p, enabled.IsOn); Render();
                        Message(() => BemText.Get("已保存"), () => BemText.Get("资源目标重叠的其他包会自动停用。") + SelectionAppliedHint);
                    }
                    catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(enabled);
                if (!p.IsComposable)
                {
                    var appearance = new ComboBox { Header = BemText.Get("外观"), ItemsSource = p.Appearances, SelectedItem = p.Appearances.First(a => a.Id == p.SelectedAppearance), MinWidth = 220 };
                    BemLocalizedUI.Set(appearance, ComboBox.HeaderProperty, () => BemText.Get("外观"));
                    var description = new TextBlock { Text = p.Appearances.First(a => a.Id == p.SelectedAppearance).Description, TextWrapping = TextWrapping.Wrap };
                    appearance.SelectionChanged += async (_, _) =>
                    {
                        if (_rendering || appearance.SelectedItem is not BemAppearance selected) return;
                        try { p.SelectedAppearance = selected.Id; await _service.SaveAsync(); description.Text = selected.Description; Message(() => BemText.Get("外观已保存"), () => SelectionAppliedHint); }
                        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
                    };
                    controls.Children.Add(appearance);
                    stack.Children.Add(description);
                }
                var remove = new Button { Content = BemText.Get("移除"), VerticalAlignment = VerticalAlignment.Bottom };
                BemLocalizedUI.Set(remove, Button.ContentProperty, () => BemText.Get("移除"));
                remove.Click += async (_, _) =>
                {
                    try { await _service.RemoveAsync(p); Render(); Message(() => BemText.Get("已移除"), () => p.Name); }
                    catch (Exception ex) { Message(() => BemText.Get("无法移除"), () => ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(remove); stack.Children.Add(controls);
                if (p.IsComposable)
                {
                    var optionPanel = new StackPanel { Spacing = 8 };
                    var selectors = new List<(BemOptionGroup Group, ComboBox Box)>();
                    foreach (var group in p.OptionGroups)
                    {
                        var box = new ComboBox
                        {
                            Header = group.Name, ItemsSource = group.Choices,
                            SelectedItem = group.Choices.First(choice => choice.Id == p.SelectedOptions[group.Id]),
                            MinWidth = 220
                        };
                        selectors.Add((group, box)); optionPanel.Children.Add(box);
                    }
                    var parameterRows = new List<(BemParameterGroup Parameter, StackPanel Row)>();
                    var pendingParameters = new Dictionary<string, uint>(p.SelectedParameters, StringComparer.Ordinal);
                    var parameterSliders = new List<(BemParameterGroup Parameter, Slider Slider)>();
                    foreach (var parameter in p.Parameters)
                    {
                        var row = new StackPanel { Spacing = 4 };
                        var label = new TextBlock { TextWrapping = TextWrapping.Wrap };
                        var slider = new Slider
                        {
                            Minimum = parameter.Min, Maximum = parameter.Max,
                            Value = pendingParameters[parameter.Id], StepFrequency = parameter.Step,
                            TickFrequency = parameter.Step, SnapsTo = Microsoft.UI.Xaml.Controls.Primitives.SliderSnapsTo.StepValues,
                            IsThumbToolTipEnabled = false, MinWidth = 220
                        };
                        void RefreshLabel() => BemLocalizedUI.Set(label, TextBlock.TextProperty,
                            () => parameter.Name + BemText.Colon + $"{pendingParameters[parameter.Id] / 1000.0:0.###} " +
                            BemText.Format("（{0:0.###}–{1:0.###}，步长 {2:0.###}，默认 {3:0.###}，原形 {4:0.###}）", parameter.Min / 1000.0, parameter.Max / 1000.0, parameter.Step / 1000.0, parameter.Default / 1000.0, parameter.Neutral / 1000.0));
                        slider.ValueChanged += (_, args) =>
                        {
                            if (!_rendering) _pendingParameters = true;
                            pendingParameters[parameter.Id] = parameter.Snap(args.NewValue);
                            RefreshLabel();
                        };
                        RefreshLabel(); row.Children.Add(label); row.Children.Add(slider);
                        parameterRows.Add((parameter, row)); parameterSliders.Add((parameter, slider)); optionPanel.Children.Add(row);
                    }
                    if (p.Parameters.Count > 0)
                    {
                        var parameterHint = new TextBlock { TextWrapping = TextWrapping.Wrap };
                        BemLocalizedUI.Set(parameterHint, TextBlock.TextProperty, () => BemText.Get("滑条调整后点击应用。隐藏的滑条会按原形生效，并保留你保存的数值。") + SelectionAppliedHint);
                        optionPanel.Children.Add(parameterHint);
                        var parameterActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
                        var apply = new Button { Content = BemText.Get("应用滑条") };
                        BemLocalizedUI.Set(apply, Button.ContentProperty, () => BemText.Get("应用滑条"));
                        apply.Click += async (_, _) =>
                        {
                            apply.IsEnabled = false;
                            try
                            {
                                foreach (var parameter in p.Parameters) p.SelectedParameters[parameter.Id] = pendingParameters[parameter.Id];
                                await _service.SaveAsync(); _pendingParameters = false; Message(() => BemText.Get("滑条已保存"), () => SelectionAppliedHint);
                            }
                            catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
                            finally { apply.IsEnabled = true; }
                        };
                        var defaults = new Button { Content = BemText.Get("恢复作者默认值") };
                        BemLocalizedUI.Set(defaults, Button.ContentProperty, () => BemText.Get("恢复作者默认值"));
                        defaults.Click += (_, _) => { foreach (var (parameter, slider) in parameterSliders) slider.Value = parameter.Default; };
                        parameterActions.Children.Add(apply); parameterActions.Children.Add(defaults); optionPanel.Children.Add(parameterActions);
                    }
                    // Collapsed by default so the package list stays short; the
                    // header summarises the current selection.
                    var expander = new Expander
                    {
                        Content = optionPanel, IsExpanded = _expanded.Contains(p.Id),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch
                    };
                    expander.Expanding += (_, _) => _expanded.Add(p.Id);
                    expander.Collapsed += (_, _) => _expanded.Remove(p.Id);
                    void RefreshAvailability()
                    {
                        var active = p.EffectiveOptions();
                        foreach (var (group, box) in selectors)
                            box.Visibility = active.ContainsKey(group.Id) ? Visibility.Visible : Visibility.Collapsed;
                        foreach (var (parameter, row) in parameterRows)
                            row.Visibility = p.ParameterAvailable(parameter) ? Visibility.Visible : Visibility.Collapsed;
                        var chosen = selectors.Where(s => active.ContainsKey(s.Group.Id))
                            .Select(s => s.Group.Choices.FirstOrDefault(c => c.Id == p.SelectedOptions[s.Group.Id])?.Name)
                            .Where(name => !string.IsNullOrEmpty(name)).ToList();
                        string summary = string.Join(BemText.ListSeparator, chosen.Take(4)) + (chosen.Count > 4 ? BemText.Format(" 等 {0} 项", chosen.Count) : "");
                        expander.Header = new TextBlock
                        {
                            Text = BemText.Format("组件选项（{0} 组）", active.Count) + (p.Parameters.Count > 0 ? BemText.Format(" · {0} 个滑条", p.Parameters.Count) : "") + (summary.Length > 0 ? BemText.Colon + summary : ""),
                            TextTrimming = TextTrimming.CharacterEllipsis
                        };
                    }
                    foreach (var (group, box) in selectors)
                    {
                        box.SelectionChanged += async (_, _) =>
                        {
                            if (_rendering || box.SelectedItem is not BemOptionChoice selected) return;
                            string previous = p.SelectedOptions[group.Id];
                            if (previous == selected.Id) return;
                            p.SelectedOptions[group.Id] = selected.Id;
                            if (!p.OptionsValid())
                            {
                                p.SelectedOptions[group.Id] = previous;
                                box.SelectedItem = group.Choices.First(choice => choice.Id == previous);
                                Message(() => BemText.Get("组合不可达"), () => BemText.Get("这个选项组合不符合包内约束。"), InfoBarSeverity.Warning);
                                return;
                            }
                            try { RefreshAvailability(); await _service.SaveAsync(); Message(() => BemText.Get("选项已保存"), () => SelectionAppliedHint); }
                            catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
                        };
                    }
                    RefreshAvailability(); _localizeCards.Add(RefreshAvailability); stack.Children.Add(expander);
                }
                PackageCards.Children.Add(new Border { Tag = p.TargetKey, Child = stack, Padding = new Thickness(16), CornerRadius = new CornerRadius(8), Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] });
            }
            ApplyCharacterFilter();
        }
        finally { _rendering = false; }
    }

    private async void Import_Click(object sender, RoutedEventArgs e)
    {
        await ImportFilesAsync(async () =>
        {
            var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".bem"); picker.FileTypeFilter.Add(".zip");
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
            var file = await picker.PickSingleFileAsync();
            return file == null ? [] : [file.Path];
        });
    }

    private void ModelDropArea_DragOver(object sender, DragEventArgs e)
    {
        bool accept = !_importing && e.DataView.Contains(StandardDataFormats.StorageItems);
        e.AcceptedOperation = accept ? DataPackageOperation.Copy : DataPackageOperation.None;
        ModelDropHighlight.Visibility = accept ? Visibility.Visible : Visibility.Collapsed;
        if (accept)
        {
            e.DragUIOverride.Caption = LocalizationService.Instance.IsChinese ? BemText.Get("导入 BEM / ZIP 模型包") : "Import BEM / ZIP model packages";
            e.DragUIOverride.IsCaptionVisible = true;
        }
        e.Handled = true;
    }

    private void ModelDropArea_DragLeave(object sender, DragEventArgs e)
    {
        ModelDropHighlight.Visibility = Visibility.Collapsed;
        e.Handled = true;
    }

    private async void ModelDropArea_Drop(object sender, DragEventArgs e)
    {
        ModelDropHighlight.Visibility = Visibility.Collapsed;
        e.Handled = true;
        if (_importing || !e.DataView.Contains(StandardDataFormats.StorageItems)) return;
        var deferral = e.GetDeferral();
        await ImportFilesAsync(async () =>
        {
            try
            {
                var items = await e.DataView.GetStorageItemsAsync();
                // File paths use the same importer as the picker; folders are
                // passed through for an explicit rejection instead of recursion.
                return items.Select(item => item.Path).ToArray();
            }
            finally { deferral.Complete(); }
        });
    }

    private async Task ImportFilesAsync(Func<Task<IReadOnlyList<string>>> selectFiles)
    {
        if (_importing) return;
        _importing = true;
        UpdateDisableAllButton();
        ImportButton.IsEnabled = false; ModelDropArea.AllowDrop = false;
        ModelDropHighlight.Visibility = Visibility.Collapsed;
        Busy.IsActive = true; Busy.Visibility = Visibility.Visible;
        try
        {
            var paths = await selectFiles();
            if (paths.Count == 0) return;
            int count = 0;
            bool processed = false;
            var issues = new List<string>();
            foreach (string path in paths.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                string extension = Path.GetExtension(path);
                if (Directory.Exists(path) || !(extension.Equals(".bem", StringComparison.OrdinalIgnoreCase)
                    || extension.Equals(".zip", StringComparison.OrdinalIgnoreCase)))
                {
                    issues.Add(Path.GetFileName(path) + BemText.Get("：仅支持 BEM 或 ZIP 文件，不支持文件夹。"));
                    continue;
                }
                try
                {
                    if (extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        var result = await ImportBundleAsync(path);
                        if (result.Cancelled) continue;
                        count += result.Count; issues.AddRange(result.Issues);
                    }
                    else
                    {
                        await _service.ImportAsync(path, InstallRoot);
                        count++; issues.AddRange(_service.Notices);
                    }
                    processed = true;
                }
                catch (Exception ex) { issues.Add(Path.GetFileName(path) + "：" + ex.Message); }
            }
            if (!processed && issues.Count == 0) return;
            Render();
            Message(() => count > 0 || processed ? BemText.Format("已导入 {0} 个包", count) : BemText.Get("导入失败"),
                () => issues.Count > 0 ? string.Join("\n", issues)
                    : (_service.SkipValidation ? BemText.Get("已跳过模型校验（实验）。") : BemText.Get("已校验模型包。")) + BemText.Get("新包默认停用；请选择外观或选项组并启用。同 ID 更新保留仍有效的选择。"),
                issues.Count > 0 ? (count > 0 ? InfoBarSeverity.Warning : InfoBarSeverity.Error) : InfoBarSeverity.Success);
        }
        catch (Exception ex) { Message(() => BemText.Get("导入失败"), () => ex.Message, InfoBarSeverity.Error); }
        finally
        {
            _importing = false;
            UpdateDisableAllButton();
            ImportButton.IsEnabled = true; ModelDropArea.AllowDrop = true;
            Busy.IsActive = false; Busy.Visibility = Visibility.Collapsed;
        }
    }
    private async Task<(int Count, IReadOnlyList<string> Issues, bool Cancelled)> ImportBundleAsync(string source)
    {
        using var bundle = await _service.PrepareBundleAsync(source, InstallRoot);
        var body = new StackPanel { Spacing = 12, MaxWidth = 600 };
        body.Children.Add(new TextBlock { Text = BemText.Get("勾选要导入的包。新包默认停用；同 ID 更新保留现有选择。"), TextWrapping = TextWrapping.Wrap });
        var choices = new List<(CheckBox Check, BemPackage Package)>();
        foreach (var package in bundle.Packages)
        {
            bool update = _service.Packages.Any(p => p.Id == package.Id);
            var check = new CheckBox
            {
                IsChecked = true,
                Content = new TextBlock
                {
                    Text = $"{TargetLabel(package)} · {package.Name}\n{package.Version} · {package.Size / 1_000_000.0:F1} MB · {(update ? BemText.Get("更新已有包") : BemText.Get("新包"))}\n"+
                        (package.IsComposable ? BemText.Get("选项组：") + string.Join(BemText.ListSeparator, package.OptionGroups.Select(g => g.Name)) :
                            BemText.Get("外观：") + string.Join(BemText.ListSeparator, package.Appearances.Select(a => a.Name))),
                    TextWrapping = TextWrapping.Wrap
                }
            };
            choices.Add((check, package)); body.Children.Add(check);
        }
        if (bundle.Issues.Count > 0)
            body.Children.Add(new TextBlock { Text = BemText.Get("以下项未通过校验：\n") + string.Join("\n", bundle.Issues), TextWrapping = TextWrapping.Wrap });
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot, Title = BemText.Get("导入 ZIP 中的模型包"), PrimaryButtonText = BemText.Get("导入所选"), CloseButtonText = BemText.Get("取消"),
            Content = new ScrollViewer { Content = body, MaxHeight = 500, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }
        };
        if (await dialog.ShowAsync() != ContentDialogResult.Primary) return (0, [], true);
        int count = 0;
        var issues = new List<string>(bundle.Issues);
        foreach (var choice in choices.Where(c => c.Check.IsChecked == true))
        {
            try
            {
                await _service.ImportAsync(choice.Package.File, InstallRoot); count++;
                issues.AddRange(_service.Notices);
            }
            catch (Exception ex) { issues.Add(choice.Package.Name + "：" + ex.Message); }
        }
        return (count, issues, false);
    }
    private async void Experiments_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try
        {
            _service.HotSwitch = HotSwitchToggle.IsOn;
            _service.CloneSupport = CloneSupportToggle.IsOn;
            _service.FastLoading = FastLoadingToggle.IsOn;
            await _service.SaveAsync();
            Message(() => BemText.Get("实验设置已保存"), () => BemText.Get("各开关相互独立，默认关闭；重启游戏后生效。"));
        }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
    }

    private async void OverlaySettings_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try
        {
            _service.ModelOverlayEnabled = ModelOverlayToggle.IsOn;
            _service.ModelOverlayVisible = ModelOverlayVisibleToggle.IsOn;
            await _service.SaveAsync();
        }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
    }

    private void ModelOverlayHotkey_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        e.Handled = true;
        if (HotkeyService.IsModifier(e.Key)) return;
        if (e.Key is Windows.System.VirtualKey.Back or Windows.System.VirtualKey.Delete)
        { ModelOverlayHotkeyBox.Text = "NONE"; return; }
        string captured = HotkeyService.Capture(e.Key, e.KeyStatus.IsExtendedKey);
        if (captured.Length != 0) ModelOverlayHotkeyBox.Text = captured;
    }

    private async void ModelOverlayHotkey_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_rendering || !HotkeyService.TryNormalize(ModelOverlayHotkeyBox.Text, out string hotkey)) return;
        try { _service.ModelOverlayHotkey = hotkey; await _service.SaveAsync(); }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
    }
    private async void SkipValidation_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try
        {
            _service.SkipValidation = SkipValidationToggle.IsOn;
            await _service.SaveAsync(); Reload();
            Message(() => _service.SkipValidation ? BemText.Get("实验：模型校验已关闭") : BemText.Get("模型校验已恢复"),
                () => _service.SkipValidation ? BemText.Get("兼容性和容量校验已关闭，可能导致游戏崩溃或模型错乱。下次启动游戏生效。") : BemText.Get("下次启动游戏使用正常校验。"),
                _service.SkipValidation ? InfoBarSeverity.Warning : InfoBarSeverity.Informational);
        }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
    }
    private async void Lod_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try { _service.StandaloneLod = LodToggle.IsOn; await _service.SaveAsync(); Render(); Message(() => BemText.Get("LOD 偏好已保存"), () => BemText.Get("下次启动游戏生效。")); }
        catch (Exception ex) { Reload(); Message(() => BemText.Get("保存失败"), () => ex.Message, InfoBarSeverity.Error); }
    }
    private void Convert_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_converter == null) { _converter = new BemConverterWindow(InstallRoot, App.MainWindowInstance); _converter.Closed += (_, _) => _converter = null; }
            _converter.Activate();
        }
        catch (Exception ex) { Message(() => BemText.Get("无法打开转换器"), () => ex.Message, InfoBarSeverity.Error); }
    }
    private void Refresh_Click(object sender, RoutedEventArgs e) => Reload();
    private void OpenFolder_Click(object sender, RoutedEventArgs e)
    {
        try { Directory.CreateDirectory(_service.Root); Process.Start(new ProcessStartInfo(_service.Root) { UseShellExecute = true }); }
        catch (Exception ex) { Message(() => BemText.Get("无法打开目录"), () => ex.Message, InfoBarSeverity.Error); }
    }
}
