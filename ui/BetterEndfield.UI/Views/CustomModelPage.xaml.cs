using System.Diagnostics;
using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

public sealed partial class CustomModelPage : UserControl
{
    private readonly BemPackageService _service = new();
    private bool _rendering;
    private BemConverterWindow? _converter;
    public Func<string>? InstallRootProvider { get; set; }
    private string InstallRoot => InstallRootProvider?.Invoke() ?? (Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory);

    public CustomModelPage()
    {
        InitializeComponent();
        Loaded += (_, _) => Reload();
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
                        Message("已保存", "同角色只启用一个包，下次启动游戏生效。");
                    }
                    catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(enabled);
                var appearance = new ComboBox { Header = "外观", ItemsSource = p.Appearances, SelectedItem = p.Appearances.First(a => a.Id == p.SelectedAppearance), MinWidth = 220 };
                var description = new TextBlock { Text = p.Appearances.First(a => a.Id == p.SelectedAppearance).Description, TextWrapping = TextWrapping.Wrap };
                appearance.SelectionChanged += async (_, _) =>
                {
                    if (_rendering || appearance.SelectedItem is not BemAppearance selected) return;
                    try { p.SelectedAppearance = selected.Id; await _service.SaveAsync(); description.Text = selected.Description; Message("外观已保存", "下次启动游戏生效。"); }
                    catch (Exception ex) { Reload(); Message("保存失败", ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(appearance);
                var remove = new Button { Content = "移除", VerticalAlignment = VerticalAlignment.Bottom };
                remove.Click += async (_, _) =>
                {
                    try { await _service.RemoveAsync(p); Render(); Message("已移除", p.Name); }
                    catch (Exception ex) { Message("无法移除", ex.Message, InfoBarSeverity.Error); }
                };
                controls.Children.Add(remove); stack.Children.Add(controls); stack.Children.Add(description);
                PackageCards.Children.Add(new Border { Child = stack, Padding = new Thickness(16), CornerRadius = new CornerRadius(8), Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] });
            }
        }
        finally { _rendering = false; }
    }

    private async void Import_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".bem"); picker.FileTypeFilter.Add(".zip");
        InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
        try
        {
            var file = await picker.PickSingleFileAsync(); if (file == null) return;
            ImportButton.IsEnabled = false; Busy.IsActive = true; Busy.Visibility = Visibility.Visible;
            if (Path.GetExtension(file.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                await ImportBundleAsync(file.Path); return;
            }
            await _service.ImportAsync(file.Path, InstallRoot); Render();
            Message("导入完成", "已校验全部外观。新包默认停用；请选择外观并启用。同 ID 更新保留启用状态与仍存在的外观选择。", InfoBarSeverity.Success);
            if (_service.Notices.Count > 0) Message("导入完成，需注意", string.Join("\n", _service.Notices), InfoBarSeverity.Warning);
        }
        catch (Exception ex) { Message("导入失败", ex.Message, InfoBarSeverity.Error); }
        finally { ImportButton.IsEnabled = true; Busy.IsActive = false; Busy.Visibility = Visibility.Collapsed; }
    }
    private async Task ImportBundleAsync(string source)
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
                    Text = $"{PresetOptions.GetCharacterName(package.Character)} · {package.Name}\n{package.Version} · {package.Size / 1_000_000.0:F1} MB · {(update ? "更新已有包" : "新包")}\n外观：{string.Join("、", package.Appearances.Select(a => a.Name))}",
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
        if (await dialog.ShowAsync() != ContentDialogResult.Primary) return;
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
        Render();
        Message($"已导入 {count} 个包", issues.Count > 0 ? string.Join("\n", issues) : "可分别选择外观并启用；同角色同时启用一个包。",
            issues.Count > 0 ? InfoBarSeverity.Warning : InfoBarSeverity.Success);
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
