using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Text.Json.Nodes;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

public sealed class ThirdPartyModulesPage : UserControl
{
    private readonly ThirdPartyModuleService _service = new();
    private readonly StackPanel _cards = new() { Spacing = 12 };
    private readonly TextBlock _status = new() { TextWrapping = TextWrapping.Wrap };
    private bool _busy;
    public ThirdPartyModulesPage()
    {
        var page = new StackPanel { Spacing = 16, Padding = new Thickness(24) };
        page.Children.Add(new TextBlock { Text = "第三方模块", FontSize = 28, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        page.Children.Add(new TextBlock { Text = "导入作者提供的模块 ZIP。原生模块会运行作者代码，请选择信任的来源。配置和已加载模块的启停可在运行中更新；二进制更新、移除和排序需重启游戏完成。旧版本文件会保留，避免打断运行中的模块。", TextWrapping = TextWrapping.Wrap });
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        var import = new Button { Content = "导入模块 ZIP" }; import.Click += Import;
        var refresh = new Button { Content = "刷新" }; refresh.Click += (_, _) => Render();
        actions.Children.Add(import); actions.Children.Add(refresh); page.Children.Add(actions); page.Children.Add(_status); page.Children.Add(_cards);
        Content = new ScrollViewer { Content = page, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }; Loaded += (_, _) => Render();
    }
    private void Render()
    {
        _cards.Children.Clear();
        try
        {
            var records = _service.Records();
            if (records.Count == 0) _cards.Children.Add(new TextBlock { Text = "尚未导入第三方模块。" });
            foreach (var record in records)
            {
                var card = new StackPanel { Spacing = 10 };
                card.Children.Add(new TextBlock { Text = record.Name, FontSize = 20 });
                card.Children.Add(new TextBlock { Text = $"{record.Manifest["author"]} · {record.Manifest["version"]} · {record.Id}", TextWrapping = TextWrapping.Wrap });
                card.Children.Add(new TextBlock { Text = record.Error.Length > 0 ? "模块文件读取失败：" + record.Error : record.Supported ? "此包支持 Windows 原生模块或网页。" : "此包仅支持其他平台，无法在 Windows 启用。", TextWrapping = TextWrapping.Wrap });
                var enabled = new ToggleSwitch { Header = "启用模块", IsOn = record.Enabled, IsEnabled = record.Supported && !_busy };
                enabled.Toggled += async (_, _) => { if (_busy) return; await Act(() => _service.SetEnabledAsync(record.Id, enabled.IsOn), "启用状态已保存；已连接的游戏会更新状态，原生库仍保留到游戏退出。"); };
                card.Children.Add(enabled);
                var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
                void Button(string text, Action click, bool available = true) { var button = new Microsoft.UI.Xaml.Controls.Button { Content = text, IsEnabled = available && !_busy }; button.Click += (_, _) => click(); actions.Children.Add(button); }
                Button("打开网页", () => ThirdPartyModuleWindow.Open(record), record.Ui.Length > 0);
                Button("状态 / 日志", async () =>
                {
                    try { var status = await _service.RuntimeAsync("status", record.Id); _status.Text = status.ToJsonString(new System.Text.Json.JsonSerializerOptions { WriteIndented = true }); }
                    catch (Exception) { _status.Text = "游戏模块未连接。已保存的安装和配置仍然有效；启用模块后重启游戏。"; }
                });
                Button("上移", async () => await Act(() => _service.MoveAsync(record.Id, -1), "模块顺序已保存，重启游戏生效。"));
                Button("下移", async () => await Act(() => _service.MoveAsync(record.Id, 1), "模块顺序已保存，重启游戏生效。"));
                Button("移除", async () => await Act(() => _service.RemoveAsync(record.Id), "模块已从加载列表移除，重启游戏生效。旧文件保留到运行中的模块释放。"));
                card.Children.Add(actions); _cards.Children.Add(new Border { Child = card, Padding = new Thickness(16), CornerRadius = new CornerRadius(8), Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] });
            }
        }
        catch (Exception e) { _status.Text = "模块列表读取失败：" + e.Message; }
    }
    private async Task Act(Func<Task> action, string success)
    {
        _busy = true;
        try { await action(); _status.Text = success; } catch (Exception e) { _status.Text = "操作未完成：" + e.Message; }
        finally { _busy = false; Render(); }
    }
    private async void Import(object sender, RoutedEventArgs e)
    {
        if (_busy) return;
        var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".zip"); InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
        try { var file = await picker.PickSingleFileAsync(); if (file is not null) await Act(async () => { _ = await _service.ImportAsync(file.Path); }, "模块导入完成。新包默认停用；更新保留配置，重启游戏生效。"); }
        catch (Exception error) { _status.Text = "导入未完成：" + error.Message; }
    }
}
