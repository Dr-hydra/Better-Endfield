using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace BetterEndfield.UI.Views;

// Explicit opt-in technical probe. No real records or cloud writes.
public sealed class WebViewProbeWindow : Window
{
    private readonly WebView2 _view = new();
    private readonly TextBlock _status = new() { Text = "正在初始化 WebView2…", Margin = new Thickness(12), TextWrapping = TextWrapping.Wrap };
    private readonly string _log = System.IO.Path.Combine(AppContext.BaseDirectory, "webview-probe.log");
    private bool _closed;

    public WebViewProbeWindow()
    {
        Title = "Better Endfield · Toy WebView2 技术验证";
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.Children.Add(_status);
        Grid.SetRow(_view, 1);
        grid.Children.Add(_view);
        Content = grid;
        AppWindow.Resize(new Windows.Graphics.SizeInt32(1280, 900));
        Closed += (_, _) => { _closed = true; _view.Close(); };
        _view.Loaded += Initialize;
    }

    private void Report(string text)
    {
        if (_closed) return;
        _status.Text = text;
        System.IO.File.AppendAllText(_log, $"{DateTimeOffset.Now:O} {text}{Environment.NewLine}");
    }

    private async void Initialize(object sender, RoutedEventArgs args)
    {
        _view.Loaded -= Initialize;
        try
        {
            var profile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetterEndfield", "WebViewProbe");
            var environment = await CoreWebView2Environment.CreateWithOptionsAsync(null, profile, null);
            await _view.EnsureCoreWebView2Async(environment);
            Report("Runtime " + environment.BrowserVersionString);
            _view.CoreWebView2.NavigationCompleted += (_, e) => Report($"顶层加载：{e.IsSuccess} / {e.WebErrorStatus}");
            _view.CoreWebView2.FrameCreated += (_, e) => Attach(e.Frame);
            var arguments = Environment.GetCommandLineArgs();
            var index = Array.IndexOf(arguments, "--webview-probe");
            _view.Source = new Uri(index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : "https://www.bilibili.com/toy/endfield/index.html");
        }
        catch (Exception e) { Report("初始化失败：" + e.GetType().Name + " " + e.Message); }
    }

    private void Attach(CoreWebView2Frame frame)
    {
        frame.WebMessageReceived += async (_, e) =>
        {
            // Only the probe listener produces this fixed, credential-free summary.
            try
            {
                using var json = JsonDocument.Parse(e.WebMessageAsJson);
                var source = new Uri(e.Source);
                if (source.Scheme != "https" || source.Host != "www.bilibilitoy.com" || !(source.AbsolutePath.StartsWith("/toy/endfield/", StringComparison.Ordinal) || source.AbsolutePath.StartsWith("/toy/preview/", StringComparison.Ordinal))) return;
                if (json.RootElement.GetProperty("protocol").GetString() != "better-endfield-desktop") return;
                var type = json.RootElement.GetProperty("type").GetString();
                if (type == "ready")
                {
                    Report("Toy 前端 ready；开始发送 8 MiB 原始测试数据");
                    frame.PostWebMessageAsJson(JsonSerializer.Serialize(new { protocol = "better-endfield-desktop", version = 1, id = "probe", type = "begin", kind = "probe", size = 8 * 1024 * 1024 }));
                    for (int i = 0; i < 32 && !_closed; i++)
                    {
                        frame.PostWebMessageAsJson(JsonSerializer.Serialize(new { protocol = "better-endfield-desktop", version = 1, id = "probe", type = "chunk", index = i, text = new string('x', 262144) }));
                        await Task.Delay(30);
                    }
                    if (!_closed) frame.PostWebMessageAsJson(JsonSerializer.Serialize(new { protocol = "better-endfield-desktop", version = 1, id = "probe", type = "end" }));
                }
                else Report("Toy " + e.WebMessageAsJson);
            }
            catch (Exception) { /* Ignore unrelated Toy SDK messages. */ }
        };
        frame.NavigationCompleted += async (_, e) =>
        {
            if (!e.IsSuccess || _closed) return;
            try
            {
                var location = await frame.ExecuteScriptAsync("JSON.stringify({origin:location.origin,path:location.pathname})");
                var locationJson = JsonSerializer.Deserialize<string>(location);
                if (locationJson is null) return;
                using var info = JsonDocument.Parse(locationJson);
                if (info.RootElement.GetProperty("origin").GetString() != "https://www.bilibilitoy.com") return;
                var sdk = await frame.ExecuteScriptAsync("JSON.stringify({sdk:!!window.toy,cloud:!!window.toy?.setCloudStorage})");
                Report("Toy 内页加载成功；SDK 检测：" + sdk);
            }
            catch (Exception ex) { Report("Frame 验证失败：" + ex.GetType().Name + " " + ex.Message); }
        };
    }
}
