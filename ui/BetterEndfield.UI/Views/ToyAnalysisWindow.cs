using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System.Text;
using System.Text.Json;

namespace BetterEndfield.UI.Views;

public sealed class ToyAnalysisWindow : Window
{
    private const string Protocol = "better-endfield-desktop";
    private static readonly HashSet<ToyAnalysisWindow> Windows = [];
    private readonly WebView2 _view = new();
    private readonly TextBlock _status = new() { Margin = new Thickness(12), TextWrapping = TextWrapping.Wrap };
    private readonly CancellationTokenSource _lifetime = new();
    private readonly string _kind;
    private readonly string _json;
    private readonly Uri _url;
    private readonly bool _english = !Services.LocalizationService.Instance.IsChinese;
    private TaskCompletionSource? _ack;
    private string? _transfer;
    private string? _step;
    private int _index;
    private CoreWebView2Frame? _sendingFrame;
    private bool _closed;
    private bool _received;

    private ToyAnalysisWindow(string baseUrl, string kind, string json)
    {
        if (Encoding.UTF8.GetByteCount(json) > 64 * 1024 * 1024) throw new System.IO.InvalidDataException("记录超过 64 MiB");
        _kind = kind; _json = json;
        _url = new Uri(baseUrl + (baseUrl.Contains('?') ? "&" : "?") + "mode=" + kind + "&desktop=1");
        Title = "Better Endfield · " + (kind == "combat" ? Text("战斗数据解析", "Combat analysis") : Text("寻访统计", "Headhunting statistics"));
        _status.Text = Text("正在加载解析页面…", "Loading analysis page…");
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.Children.Add(_status);
        Grid.SetRow(_view, 1); grid.Children.Add(_view); Content = grid;
        AppWindow.Resize(new Windows.Graphics.SizeInt32(1280, 900));
        Closed += (_, _) => { _closed = true; _lifetime.Cancel(); _ack?.TrySetCanceled(); _view.Close(); Windows.Remove(this); };
        _view.Loaded += Initialize;
    }

    private string Text(string zh, string en) => _english ? en : zh;
    public static void Open(string baseUrl, string kind, string json)
    {
        var window = new ToyAnalysisWindow(baseUrl, kind, json);
        Windows.Add(window); window.Activate();
    }
    public static void CloseAll() { foreach (var window in Windows.ToArray()) window.Close(); }

    private async void Initialize(object sender, RoutedEventArgs args)
    {
        _view.Loaded -= Initialize;
        try
        {
            var profile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetterEndfield", "WebViewProbe");
            var environment = await CoreWebView2Environment.CreateWithOptionsAsync(null, profile, null);
            if (_closed) return;
            await _view.EnsureCoreWebView2Async(environment);
            if (_closed) return;
            _view.CoreWebView2.FrameCreated += (_, e) => Attach(e.Frame);
            _view.CoreWebView2.NavigationCompleted += (_, e) =>
            {
                if (!e.IsSuccess && !_closed) _status.Text = Text("页面加载失败，请检查网络后重新打开。", "Page failed to load. Check your connection and reopen.");
            };
            _view.Source = _url;
            await Task.Delay(TimeSpan.FromSeconds(60), _lifetime.Token);
            if (!_closed && !_received && _sendingFrame is null)
                _status.Text = Text("页面尚未连接。请完成登录；若页面已打开，请确认 Toy 已更新至支持桌面直连的版本。", "Waiting for the page. Complete sign-in and ensure Toy supports desktop imports.");
        }
        catch (OperationCanceledException) { }
        catch (Exception) { if (!_closed) _status.Text = Text("无法打开分析窗口，请确认已安装 Microsoft Edge WebView2 Runtime。", "Unable to open analysis. Ensure Microsoft Edge WebView2 Runtime is installed."); }
    }

    private void Attach(CoreWebView2Frame frame)
    {
        frame.NavigationStarting += (_, _) => { if (_sendingFrame == frame) _ack?.TrySetException(new InvalidOperationException("navigation")); };
        frame.WebMessageReceived += async (_, e) =>
        {
            if (_closed || !Uri.TryCreate(e.Source, UriKind.Absolute, out var source) || source.Scheme != "https" || source.Host != "www.bilibilitoy.com" || !source.AbsolutePath.StartsWith("/toy/endfield/", StringComparison.Ordinal)) return;
            try
            {
                using var doc = JsonDocument.Parse(e.WebMessageAsJson);
                var message = doc.RootElement;
                if (!message.TryGetProperty("protocol", out var protocol) || protocol.GetString() != Protocol || message.GetProperty("version").GetInt32() != 1) return;
                var type = message.GetProperty("type").GetString();
                if (type == "ready" && _sendingFrame is null)
                {
                    _sendingFrame = frame;
                    try { await Send(frame); }
                    catch (Exception) { if (!_closed) { _status.Visibility = Visibility.Visible; _status.Text = Text("数据传入失败，请关闭此窗口后重试。", "Data transfer failed. Close this window and try again."); } }
                    finally { _sendingFrame = null; _ack = null; }
                }
                else if (_sendingFrame == frame && type == "error") _ack?.TrySetException(new InvalidOperationException("receiver_error"));
                else if (_sendingFrame == frame && type == "ack" && message.GetProperty("id").GetString() == _transfer && message.GetProperty("step").GetString() == _step && (_step != "chunk" || message.GetProperty("index").GetInt32() == _index)) _ack?.TrySetResult();
            }
            catch (JsonException) { }
            catch (InvalidOperationException) { }
            catch (KeyNotFoundException) { }
        };
    }

    private async Task Post(CoreWebView2Frame frame, string step, object message, int index = 0)
    {
        _step = step; _index = index;
        _ack = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        frame.PostWebMessageAsJson(JsonSerializer.Serialize(message));
        await _ack.Task.WaitAsync(TimeSpan.FromSeconds(20), _lifetime.Token);
    }

    private async Task Send(CoreWebView2Frame frame)
    {
        _transfer = Guid.NewGuid().ToString("N");
        _status.Visibility = Visibility.Visible;
        _status.Text = Text("正在传入记录…", "Transferring record…");
        await Post(frame, "begin", new { protocol = Protocol, version = 1, id = _transfer, type = "begin", kind = _kind, size = Encoding.UTF8.GetByteCount(_json) });
        int index = 0;
        for (int offset = 0; offset < _json.Length; index++)
        {
            int count = Math.Min(65536, _json.Length - offset);
            if (offset + count < _json.Length && char.IsHighSurrogate(_json[offset + count - 1])) count--;
            await Post(frame, "chunk", new { protocol = Protocol, version = 1, id = _transfer, type = "chunk", index, text = _json.Substring(offset, count) }, index);
            offset += count;
        }
        await Post(frame, "end", new { protocol = Protocol, version = 1, id = _transfer, type = "end" });
        _received = true;
        _status.Visibility = Visibility.Collapsed;
    }
}
