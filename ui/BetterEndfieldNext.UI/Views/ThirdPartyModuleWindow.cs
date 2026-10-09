using BetterEndfieldNext.UI.Services;
using System.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System.Text.Json.Nodes;

namespace BetterEndfieldNext.UI.Views;

internal sealed class ThirdPartyModuleWindow : Window
{
    private static readonly Dictionary<string, ThirdPartyModuleWindow> Windows = new(StringComparer.Ordinal);
    private readonly ThirdPartyModuleService _service = new();
    private readonly ThirdPartyModuleRecord _record;
    private readonly WebView2 _view = new();
    private readonly TextBlock _status = new() { Margin = new Thickness(12), TextWrapping = TextWrapping.Wrap };
    private readonly DispatcherTimer _poll = new() { Interval = TimeSpan.FromMilliseconds(500) };
    private readonly string _host;
    private bool _closed, _polling;
    private static string L(string key) => LocalizationService.Instance["Modules_" + key];
    private void Status(Func<string> text) => BemLocalizedUI.Set(_status, TextBlock.TextProperty, text);
    private void LanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LocalizationService.IsChinese))
            DispatcherQueue.TryEnqueue(() => { if (!_closed) BemLocalizedUI.Refresh(_status); });
    }

    private ThirdPartyModuleWindow(ThirdPartyModuleRecord record)
    {
        _record = record; _host = "m-" + record.Generation.Replace("-", "") + ".bemod.local";
        Title = "Better Endfield Next · " + record.Name;
        Status(() => L("Opening"));
        LocalizationService.Instance.PropertyChanged += LanguageChanged;
        var grid = new Grid(); grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.Children.Add(_status); Grid.SetRow(_view, 1); grid.Children.Add(_view); Content = grid;
        WindowPlacementService.SetInitialSize(this, 1120, 820, App.MainWindowInstance);
        _view.Loaded += Initialize;
        Closed += (_, _) => { _closed = true; LocalizationService.Instance.PropertyChanged -= LanguageChanged; _poll.Stop(); _view.Close(); if (Windows.GetValueOrDefault(record.Id) == this) Windows.Remove(record.Id); };
        _poll.Tick += Poll;
    }
    public static void Open(ThirdPartyModuleRecord record)
    {
        if (record.Ui.Length == 0) throw new InvalidOperationException(L("NoWeb"));
        if (Windows.TryGetValue(record.Id, out var existing))
        {
            if (existing._record.Generation == record.Generation) { existing.Activate(); return; }
            existing.Close();
        }
        var window = new ThirdPartyModuleWindow(record); Windows[record.Id] = window; window.Activate();
    }
    public static void CloseAll() { foreach (var window in Windows.Values.ToArray()) window.Close(); }
    private bool Owned(string uri) => Uri.TryCreate(uri, UriKind.Absolute, out var value) && value.Scheme == "https" && value.Host == _host;
    private async void Initialize(object sender, RoutedEventArgs args)
    {
        _view.Loaded -= Initialize;
        try
        {
            var environment = await CoreWebView2Environment.CreateWithOptionsAsync(null, Path.Combine(_service.Root, "webview-profile"), null);
            if (_closed) return; await _view.EnsureCoreWebView2Async(environment); if (_closed) return;
            var core = _view.CoreWebView2; core.Settings.AreHostObjectsAllowed = false;
            core.SetVirtualHostNameToFolderMapping(_host, _record.Directory, CoreWebView2HostResourceAccessKind.DenyCors);
            core.NavigationStarting += (_, e) => { if (!Owned(e.Uri)) e.Cancel = true; };
            core.NewWindowRequested += (_, e) => e.Handled = true;
            core.WebMessageReceived += Message;
            await core.AddScriptToExecuteOnDocumentCreatedAsync(ThirdPartyWebBridge.Script);
            core.NavigationCompleted += (_, e) =>
            {
                bool succeeded = e.IsSuccess;
                if (!_closed) Status(() => L(succeeded ? "WebReady" : "WebFailed"));
            };
            _view.Source = new Uri("https://" + _host + "/" + _record.Ui); _poll.Start();
        }
        catch (Exception e) { if (!_closed) Status(() => L("WebOpenFailed") + e.Message); }
    }
    private void Reply(string requestId, JsonNode? value, string? error = null)
    {
        if (!_closed) _view.CoreWebView2.PostWebMessageAsJson(new JsonObject { ["kind"] = "bridge_reply", ["request_id"] = requestId,
            ["value"] = value?.DeepClone(), ["error"] = error }.ToJsonString());
    }
    private async void Message(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        if (_closed || !Owned(e.Source) || e.WebMessageAsJson.Length > 256 * 1024) return;
        string requestId = "";
        try
        {
            var message = JsonNode.Parse(e.WebMessageAsJson)!.AsObject();
            if (message["protocol"]?.GetValue<string>() != "better-endfield-next.module-ui.v1") return;
            requestId = message["request_id"]!.GetValue<string>(); if (requestId.Length > 128) return;
            string operation = message["operation"]!.GetValue<string>();
            switch (operation)
            {
                case "readConfig": Reply(requestId, _service.Record(_record.Id).State["configuration"]); break;
                case "saveConfig":
                    var configuration = message["payload"]?.AsObject() ?? throw new InvalidDataException(L("ConfigObject"));
                    await _service.SaveConfigurationAsync(_record.Id, configuration);
                    try { await _service.RuntimeAsync("configure", _record.Id, configuration); } catch (HttpRequestException) { } catch (TaskCanceledException) { }
                    Reply(requestId, new JsonObject { ["saved"] = true }); break;
                case "send": Reply(requestId, await _service.RuntimeAsync("send", _record.Id, message["payload"], requestId)); break;
                case "status":
                    try { Reply(requestId, await _service.RuntimeAsync("status", _record.Id)); }
                    catch (HttpRequestException) { Reply(requestId, new JsonObject { ["connected"] = false }); }
                    catch (TaskCanceledException) { Reply(requestId, new JsonObject { ["connected"] = false }); } break;
                default: throw new InvalidOperationException(L("UnsupportedWebOperation"));
            }
        }
        catch (Exception error) { if (requestId.Length > 0) Reply(requestId, null, error.Message); }
    }
    private async void Poll(object? sender, object e)
    {
        if (_closed || _polling || _view.CoreWebView2 is null) return; _polling = true;
        try
        {
            var result = await _service.RuntimeAsync("poll", _record.Id);
            if (!_closed && result["messages"] is JsonArray messages) foreach (var message in messages)
                _view.CoreWebView2.PostWebMessageAsJson(new JsonObject { ["kind"] = "runtime_message", ["message"] = message?.DeepClone() }.ToJsonString());
        }
        catch (Exception) { /* Game can be stopped while this local page remains open. */ }
        finally { _polling = false; }
    }
}
