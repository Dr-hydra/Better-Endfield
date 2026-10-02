using System.Diagnostics;
using System.ComponentModel;
using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

public sealed partial class CustomModelPage : UserControl
{
    private readonly BemPackageService _service = new();
    private bool _rendering;
    private bool _importing;
    private BemConverterWindow? _converter;
    // Packages whose component options are expanded; kept across Render().
    private readonly HashSet<string> _expanded = [];
    public Func<string>? InstallRootProvider { get; set; }
    private string InstallRoot => InstallRootProvider?.Invoke() ?? (Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory);
    private string SelectionAppliedHint => _service.HotSwitch
        ? "已启用实验热切换的游戏将在下次切换配队或重新打开详情时更新；首次开启需重启游戏。"
        : "下次启动游戏生效。";

    public CustomModelPage()
    {
        InitializeComponent();
        UpdateModelSourceLanguage();
        Loaded += (_, _) =>
        {
            LocalizationService.Instance.PropertyChanged += ModelSourceLanguageChanged;
            UpdateModelSourceLanguage();
            Reload();
        };
        Unloaded += (_, _) => LocalizationService.Instance.PropertyChanged -= ModelSourceLanguageChanged;
    }

    private void ModelSourceLanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LocalizationService.IsChinese)) UpdateModelSourceLanguage();
    }

    private void UpdateModelSourceLanguage()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        GetModelsTitle.Text = isZh ? "获取模型" : "Get models";
        QuarkModelsButton.Content = isZh ? "夸克网盘" : "Quark Drive";
        BaiduModelsButton.Content = isZh ? "百度网盘" : "Baidu Netdisk";
        KatfileModelsButton.Content = isZh ? "Katfile 网盘" : "Katfile";
        ModelDropTitle.Text = isZh ? "将 BEM 或 ZIP 模型包拖到这里" : "Drop BEM or ZIP model packages here";
        ModelDropHint.Text = isZh ? "支持同时拖入多个文件，按顺序导入；ZIP 内可选择要导入的包。"
            : "Drop multiple files to import them in order. Choose which packages to import from each ZIP.";
    }

    private void ModelSource_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string url }) return;
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch (Exception ex)
        {
            Message(LocalizationService.Instance.IsChinese ? "无法打开模型链接" : "Could not open model link",
                ex.Message, InfoBarSeverity.Error);
        }
    }

    private void Message(string title, string detail, InfoBarSeverity severity = InfoBarSeverity.Informational)
    { Status.Title = title; Status.Message = detail; Status.Severity = severity; Status.IsOpen = true; }

    private void Reload()
    {
        try
        {
            _service.Load(); Render();
            if (_service.Notices.Count != 0) Message("包管理提示", string.Join("\n", _service.Notices), InfoBarSeverity.Warning);
        }
        catch (Exception ex) { Message("读取失败", ex.Message, InfoBarSeverity.Error); }
    }

    private void Render()
    {
        _rendering = true;
        try
        {
            SkipValidationToggle.IsOn = _service.SkipValidation;
            HotSwitchToggle.IsOn = _service.HotSwitch;
            LoadingOptimizationToggle.IsOn = _service.LoadingOptimization;
            bool forced = _service.Packages.Any(p => p.Enabled);
            LodToggle.IsOn = _service.EffectiveLod; LodToggle.IsEnabled = !forced;
            LodHint.Text = forced
                ? $"已启用 Mod，强制锁定 LOD。全部停用后恢复独立开关：{(_service.StandaloneLod ? "开启" : "关闭")}。"
                : "没有 Mod 启用时可独立锁定高精度模型；AI 角色也会保持高精度 LOD。";
            EmptyHint.Visibility = _service.Packages.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            PackageCards.Children.Clear();
            foreach (var p in _service.Packages.OrderBy(p => p.Character).ThenBy(p => p.Name))
            {
                var stack = new StackPanel { Spacing = 10 };
                var header = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
                header.Children.Add(new Image { Source = GachaIconService.Load(p.Character), Width = 56, Height = 56 });
                var labels = new StackPanel { Spacing = 4 };
                labels.Children.Add(new TextBlock { Text = PresetOptions.GetCharacterName(p.Character) + " · " + p.Name, FontSize = 20, TextWrapping = TextWrapping.Wrap });
                labels.Children.Add(new TextBlock { Text = $"{p.Author}  /  {p.Version}  /  {p.Size / 1_000_000.0:F1} MB", Opacity = 0.7 });
                header.Children.Add(labels); stack.Children.Add(header);
                var controls = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 16 };
                var enabled = new ToggleSwitch { Header = "启用此包", IsOn = p.Enabled };
                enabled.Toggled += async (_, _) =>
                {
                    if (_rendering) return;
                    try
                    {
                        await _service.SetEnabledAsync(p, enabled.IsOn); Render();
                        Message("已保存", "同角色只启用一个包。" + SelectionAppliedHint);
                    }
                    catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(enabled);
                if (!p.IsComposable)
                {
                    var appearance = new ComboBox { Header = "外观", ItemsSource = p.Appearances, SelectedItem = p.Appearances.First(a => a.Id == p.SelectedAppearance), MinWidth = 220 };
                    var description = new TextBlock { Text = p.Appearances.First(a => a.Id == p.SelectedAppearance).Description, TextWrapping = TextWrapping.Wrap };
                    appearance.SelectionChanged += async (_, _) =>
                    {
                        if (_rendering || appearance.SelectedItem is not BemAppearance selected) return;
                        try { p.SelectedAppearance = selected.Id; await _service.SaveAsync(); description.Text = selected.Description; Message("外观已保存", SelectionAppliedHint); }
                        catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                    };
                    controls.Children.Add(appearance);
                    stack.Children.Add(description);
                }
                var remove = new Button { Content = "移除", VerticalAlignment = VerticalAlignment.Bottom };
                remove.Click += async (_, _) =>
                {
                    try { await _service.RemoveAsync(p); Render(); Message("已移除", p.Name); }
                    catch (Exception ex) { Message("无法移除", ex.Message, InfoBarSeverity.Error); }
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
                        void RefreshLabel() => label.Text = $"{parameter.Name}：{pendingParameters[parameter.Id] / 1000.0:0.###} " +
                            $"（{parameter.Min / 1000.0:0.###}–{parameter.Max / 1000.0:0.###}，步长 {parameter.Step / 1000.0:0.###}，默认 {parameter.Default / 1000.0:0.###}，原形 {parameter.Neutral / 1000.0:0.###}）";
                        slider.ValueChanged += (_, args) =>
                        {
                            pendingParameters[parameter.Id] = parameter.Snap(args.NewValue);
                            RefreshLabel();
                        };
                        RefreshLabel(); row.Children.Add(label); row.Children.Add(slider);
                        parameterRows.Add((parameter, row)); parameterSliders.Add((parameter, slider)); optionPanel.Children.Add(row);
                    }
                    if (p.Parameters.Count > 0)
                    {
                        optionPanel.Children.Add(new TextBlock { Text = "滑条调整后点击应用。隐藏的滑条会按原形生效，并保留你保存的数值。" + SelectionAppliedHint, TextWrapping = TextWrapping.Wrap });
                        var parameterActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
                        var apply = new Button { Content = "应用滑条" };
                        apply.Click += async (_, _) =>
                        {
                            apply.IsEnabled = false;
                            try
                            {
                                foreach (var parameter in p.Parameters) p.SelectedParameters[parameter.Id] = pendingParameters[parameter.Id];
                                await _service.SaveAsync(); Message("滑条已保存", SelectionAppliedHint);
                            }
                            catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                            finally { apply.IsEnabled = true; }
                        };
                        var defaults = new Button { Content = "恢复作者默认值" };
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
                        string summary = string.Join("、", chosen.Take(4)) + (chosen.Count > 4 ? $" 等 {chosen.Count} 项" : "");
                        expander.Header = new TextBlock
                        {
                            Text = $"组件选项（{active.Count} 组）" + (p.Parameters.Count > 0 ? $" · {p.Parameters.Count} 个滑条" : "") + (summary.Length > 0 ? "：" + summary : ""),
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
                                Message("组合不可达", "这个选项组合不符合包内约束。", InfoBarSeverity.Warning);
                                return;
                            }
                            try { RefreshAvailability(); await _service.SaveAsync(); Message("选项已保存", SelectionAppliedHint); }
                            catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                        };
                    }
                    RefreshAvailability(); stack.Children.Add(expander);
                }
                PackageCards.Children.Add(new Border { Child = stack, Padding = new Thickness(16), CornerRadius = new CornerRadius(8), Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] });
            }
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
            e.DragUIOverride.Caption = LocalizationService.Instance.IsChinese ? "导入 BEM / ZIP 模型包" : "Import BEM / ZIP model packages";
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
                    issues.Add(Path.GetFileName(path) + "：仅支持 BEM 或 ZIP 文件，不支持文件夹。");
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
            Message(count > 0 || processed ? $"已导入 {count} 个包" : "导入失败",
                issues.Count > 0 ? string.Join("\n", issues)
                    : (_service.SkipValidation ? "开发者模式：已跳过模型校验。" : "已校验模型包。") + "新包默认停用；请选择外观或选项组并启用。同 ID 更新保留仍有效的选择。",
                issues.Count > 0 ? (count > 0 ? InfoBarSeverity.Warning : InfoBarSeverity.Error) : InfoBarSeverity.Success);
        }
        catch (Exception ex) { Message("导入失败", ex.Message, InfoBarSeverity.Error); }
        finally
        {
            _importing = false;
            ImportButton.IsEnabled = true; ModelDropArea.AllowDrop = true;
            Busy.IsActive = false; Busy.Visibility = Visibility.Collapsed;
        }
    }
    private async Task<(int Count, IReadOnlyList<string> Issues, bool Cancelled)> ImportBundleAsync(string source)
    {
        using var bundle = await _service.PrepareBundleAsync(source, InstallRoot);
        var body = new StackPanel { Spacing = 12, MaxWidth = 600 };
        body.Children.Add(new TextBlock { Text = "勾选要导入的包。新包默认停用；同 ID 更新保留现有选择。", TextWrapping = TextWrapping.Wrap });
        var choices = new List<(CheckBox Check, BemPackage Package)>();
        foreach (var package in bundle.Packages)
        {
            bool update = _service.Packages.Any(p => p.Id == package.Id);
            var check = new CheckBox
            {
                IsChecked = true,
                Content = new TextBlock
                {
                    Text = $"{PresetOptions.GetCharacterName(package.Character)} · {package.Name}\n{package.Version} · {package.Size / 1_000_000.0:F1} MB · {(update ? "更新已有包" : "新包")}\n"+
                        (package.IsComposable ? "选项组：" + string.Join("、", package.OptionGroups.Select(g => g.Name)) :
                            "外观：" + string.Join("、", package.Appearances.Select(a => a.Name))),
                    TextWrapping = TextWrapping.Wrap
                }
            };
            choices.Add((check, package)); body.Children.Add(check);
        }
        if (bundle.Issues.Count > 0)
            body.Children.Add(new TextBlock { Text = "以下项未通过校验：\n" + string.Join("\n", bundle.Issues), TextWrapping = TextWrapping.Wrap });
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot, Title = "导入 ZIP 中的模型包", PrimaryButtonText = "导入所选", CloseButtonText = "取消",
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
            _service.LoadingOptimization = LoadingOptimizationToggle.IsOn;
            await _service.SaveAsync();
            Message("实验设置已保存", "两个开关相互独立，默认关闭；重启游戏后生效。");
        }
        catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
    }
    private async void SkipValidation_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try
        {
            _service.SkipValidation = SkipValidationToggle.IsOn;
            await _service.SaveAsync(); Reload();
            Message(_service.SkipValidation ? "开发者模式已开启" : "模型校验已恢复",
                _service.SkipValidation ? "兼容性和容量校验已关闭，可能导致游戏崩溃或模型错乱。下次启动游戏生效。" : "下次启动游戏使用正常校验。",
                _service.SkipValidation ? InfoBarSeverity.Warning : InfoBarSeverity.Informational);
        }
        catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
    }
    private async void Lod_Toggled(object sender, RoutedEventArgs e)
    {
        if (_rendering) return;
        try { _service.StandaloneLod = LodToggle.IsOn; await _service.SaveAsync(); Render(); Message("LOD 偏好已保存", "下次启动游戏生效。"); }
        catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
    }
    private void Convert_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_converter == null) { _converter = new BemConverterWindow(InstallRoot); _converter.Closed += (_, _) => _converter = null; }
            _converter.Activate();
        }
        catch (Exception ex) { Message("无法打开转换器", ex.Message, InfoBarSeverity.Error); }
    }
    private void Refresh_Click(object sender, RoutedEventArgs e) => Reload();
    private void OpenFolder_Click(object sender, RoutedEventArgs e)
    {
        try { Directory.CreateDirectory(_service.Root); Process.Start(new ProcessStartInfo(_service.Root) { UseShellExecute = true }); }
        catch (Exception ex) { Message("无法打开目录", ex.Message, InfoBarSeverity.Error); }
    }
}
