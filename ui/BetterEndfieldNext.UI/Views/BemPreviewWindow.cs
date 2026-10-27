using System.IO.Compression;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
namespace BetterEndfieldNext.UI.Views;

internal sealed class BemPreviewView : Grid, IDisposable
{
    private const string Host = "bem-preview.local";
    private BemPackage _package;
    private readonly BemPackageService? _service;
    private Func<string, string, Task>? _saveDraft;
    private readonly Action _saved;
    private string _selection = "", _parameters = "";
    private (long Size, long Ticks) _sourceStamp;
    private readonly string _installRoot;
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "BetterEndfieldNext", "preview-" + Guid.NewGuid().ToString("N"));
    private readonly CancellationTokenSource _lifetime = new();
    private readonly TextBlock _status = new() { Margin = new Thickness(16), TextWrapping = TextWrapping.Wrap };
    private WebView2? _view;
    private Task _load = Task.CompletedTask;
    private CancellationTokenSource? _loadCancellation;
    private bool _draftLoadSuperseded;
    public void CancelDraftLoad() { if (_saveDraft is not null && _loadCancellation is not null) { _draftLoadSuperseded = true; _loadCancellation.Cancel(); } }
    private bool _initialized, _closed, _ready;
    private string? _scene;
    private string? _draftSource;
    public async Task HighlightSegmentAsync(string id)
    {
        if (!_closed && _ready && _view is not null)
            await _view.ExecuteScriptAsync("window.bemPreviewHighlightSegment(" + JsonSerializer.Serialize(id) + ")");
    }
    private string[] _highlightKeys = [];
    private bool _inspection;
    public event Action<string, int>? BodyTrianglePicked;
    private string[] _bodyEditDraws = [];
    public async Task SetBodyEditAsync(string[] draws)
    {
        _bodyEditDraws = draws;
        if (!_closed && _ready && _view is not null) await _view.ExecuteScriptAsync(BodyEditScript());
    }
    private string BodyEditScript() => "window.bemPreviewBodyEdit(" + JsonSerializer.Serialize(_bodyEditDraws) + ");";
    public async Task HighlightSegmentsAsync(string[] ids, bool inspection)
    {
        _highlightKeys = ids; _inspection = inspection;
        if (!_closed && _ready && _view is not null) await _view.ExecuteScriptAsync(HighlightScript());
    }
    public async Task ShowDraftErrorAsync(string message)
    {
        if (!_closed && _ready && _view is not null)
            await _view.ExecuteScriptAsync("window.bemPreviewError(" + JsonSerializer.Serialize(message) + ")");
    }
    private string HighlightScript() => "window.bemPreviewHighlightSegments(" + JsonSerializer.Serialize(_highlightKeys) + "," + (_inspection ? "true" : "false") + ");";
    private bool English => !LocalizationService.Instance.IsChinese;
    private string Text(string zh, string en) => English ? en : zh;

    public BemPreviewView(BemPackage package, string installRoot, BemPackageService? service, Action saved)
    {
        _package = package; _installRoot = installRoot; _service = service; _saved = saved;
        _status.Text = Text("正在初始化预览…", "Initializing preview…");
        Children.Add(_status);
        Loaded += Initialize;
    }
    public void Dispose()
    {
        if (_closed) return;
        _closed = true; _lifetime.Cancel(); _view?.Close();
        _ = CleanupAsync();
    }
    public async Task SetDraftAsync(string file, Func<string, string, Task> save, string selection = "")
    {
        CancelDraftLoad();
        await _load;
        if (_closed) return;
        Directory.CreateDirectory(_directory);
        string copy = _draftSource == file ? _package.File : Path.Combine(_directory, "composition-" + Guid.NewGuid().ToString("N") + ".bem");
        if (_draftSource != file)
        {
            await using (var input = File.OpenRead(file))
            await using (var output = File.Create(copy)) await input.CopyToAsync(output);
        }
        var package = BemPackageService.ReadMetadata(copy);
        package.RestoreOptions(selection);
        bool samePackage = _package.Id == package.Id;
        string previous = _package.File; bool ownedPrevious = _saveDraft is not null;
        _package = package; _saveDraft = save; _scene = null; _draftSource = file;
        if (_ready && !_closed && _view is not null)
            await _view.ExecuteScriptAsync("window.bemPreviewCancelDraftLoad();window.bemPreviewRefreshDraft(" + (samePackage ? "true" : "false") + ")");
        if (ownedPrevious && previous != copy)
            try { File.Delete(previous); } catch (IOException) { } catch (UnauthorizedAccessException) { }
    }
    private async Task CleanupAsync()
    {
        try { await _load; } catch (Exception) { }
        try { if (Directory.Exists(_directory)) Directory.Delete(_directory, true); }
        catch (IOException) { } catch (UnauthorizedAccessException) { }
        _lifetime.Dispose();
    }
    private async void Initialize(object sender, RoutedEventArgs args)
    {
        if (_initialized || _closed) return; _initialized = true;
        try
        {
            // No WebView construction or runtime access on the manager startup path.
            _ = CoreWebView2Environment.GetAvailableBrowserVersionString();
            Directory.CreateDirectory(_directory);
            var assembly = Assembly.GetExecutingAssembly();
            foreach (string name in new[] { "index.html", "renderer.js", "preview.js", "preview.css", "bcn.js", "decode-worker.js", "THIRD_PARTY.md" })
            {
                using var input = assembly.GetManifestResourceStream("BetterEndfieldNext.BemPreview." + name) ?? throw new IOException("Missing preview asset: " + name);
                using var output = File.Create(Path.Combine(_directory, name)); input.CopyTo(output);
            }
            using (var archive = assembly.GetManifestResourceStream("BetterEndfieldNext.BemPreview.native-head-pc.zip"))
                if (archive is not null)
                {
                    using var zip = new ZipArchive(archive);
                    zip.ExtractToDirectory(Path.Combine(_directory, "native"));
                }
            _view = new WebView2(); Children.Insert(0, _view);
            var profile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetterEndfieldNext", "BemPreviewWebView");
            var environment = await CoreWebView2Environment.CreateWithOptionsAsync(null, profile, null);
            if (_closed) return;
            await _view.EnsureCoreWebView2Async(environment);
            if (_closed) return;
            var core = _view.CoreWebView2;
            core.SetVirtualHostNameToFolderMapping(Host, _directory, CoreWebView2HostResourceAccessKind.DenyCors);
            core.Settings.AreDevToolsEnabled = false; core.Settings.AreDefaultContextMenusEnabled = false;
            core.Settings.IsStatusBarEnabled = false; core.Settings.AreHostObjectsAllowed = false;
            core.PermissionRequested += (_, e) => e.State = CoreWebView2PermissionState.Deny;
            core.NewWindowRequested += (_, e) => e.Handled = true;
            core.DownloadStarting += (_, e) => e.Cancel = true;
            core.NavigationStarting += (_, e) => e.Cancel = !Uri.TryCreate(e.Uri, UriKind.Absolute, out var uri) || uri.Scheme != "https" || uri.Host != Host || uri.AbsolutePath != "/index.html";
            core.ProcessFailed += (_, _) => { if (!_closed) { _status.Visibility = Visibility.Visible; _status.Text = Text("预览进程已退出，请关闭后重新打开。", "Preview process stopped. Close and reopen this window."); } };
            core.WebMessageReceived += MessageReceived;
            core.NavigationCompleted += (_, e) =>
            {
                if (_closed) return;
                _ready = e.IsSuccess;
                _status.Visibility = e.IsSuccess ? Visibility.Collapsed : Visibility.Visible;
                if (!e.IsSuccess) _status.Text = Text("预览页面加载失败：", "Preview page failed: ") + e.WebErrorStatus;
            };
            _view.Source = new Uri("https://" + Host + "/index.html?lang=" + (English ? "en" : "zh") + (_saveDraft is null ? "" : "&mode=composition"));
        }
        catch (Exception ex)
        {
            if (_closed) return;
            _view?.Close(); if (_view is not null) Children.Remove(_view); _view = null;
            _status.Visibility = Visibility.Visible;
            var panel = new StackPanel { Margin = new Thickness(16), Spacing = 12 };
            Children.Remove(_status); _status.Margin = new Thickness(0);
            _status.Text = Text("无法打开预览。若缺少 Microsoft Edge WebView2 Runtime，请安装后重试。其他管理功能不受影响。", "Preview unavailable. If Microsoft Edge WebView2 Runtime is missing, install it and try again. Other manager features remain available.") + "\n\n" + ex.Message;
            panel.Children.Add(_status);
            var install = new Button { Content = Text("获取 WebView2 Runtime", "Get WebView2 Runtime") };
            install.Click += async (_, _) => await global::Windows.System.Launcher.LaunchUriAsync(new Uri("https://developer.microsoft.com/microsoft-edge/webview2/"));
            panel.Children.Add(install); Children.Add(panel);
        }
    }
    private void MessageReceived(CoreWebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        if (_closed || !Uri.TryCreate(args.Source, UriKind.Absolute, out var uri) || uri.Scheme != "https" || uri.Host != Host || uri.AbsolutePath != "/index.html") return;
        try
        {
            using var json = JsonDocument.Parse(args.WebMessageAsJson); var root = json.RootElement;
            if (!_load.IsCompleted || args.WebMessageAsJson.Length > 131072) return;
            string? type = root.GetProperty("type").GetString();
            if (type == "bodyPick")
            {
                if (_saveDraft is not null && !_inspection && _scene is not null && root.GetProperty("id").GetString() == _scene &&
                    root.TryGetProperty("bodyDraw", out var body) && body.GetString() is string bodyDraw && _bodyEditDraws.Contains(bodyDraw) &&
                    root.TryGetProperty("triangle", out var face) && face.TryGetInt32(out int triangle) && triangle >= 0)
                    BodyTrianglePicked?.Invoke(bodyDraw, triangle);
                return;
            }
            if (type == "save")
            {
                if (_scene is not null && root.GetProperty("id").GetString() == _scene) _load = SaveAsync();
                return;
            }
            if (type != "load") return;
            string resource = root.GetProperty("resource").GetString() ?? "";
            if (resource.Length > 256) return;
            string? selection = root.TryGetProperty("selection", out var selected) ? selected.GetString() : null;
            string? parameters = root.TryGetProperty("parameters", out var values) ? values.GetString() : null;
            _load = LoadAsync(resource, selection, parameters, root.TryGetProperty("gpu", out var gpu) ? gpu.GetRawText() : "{}");
        }
        catch (JsonException) { } catch (InvalidOperationException) { } catch (KeyNotFoundException) { }
    }
    private string Executable()
    {
        foreach (string root in new[] { _installRoot, AppContext.BaseDirectory, Path.GetDirectoryName(Environment.ProcessPath) ?? "" }.Distinct())
        {
            string file = Path.Combine(root, "tools", "BemCompose", "BetterEndfieldNext.BemCompose.exe");
            if (File.Exists(file)) return file;
        }
        throw new FileNotFoundException(Text("缺少模型预览工具，请使用完整 PC 构建。", "Preview helper missing. Use the complete PC build."));
    }
    private async Task SaveAsync()
    {
        try
        {
            var file = new FileInfo(_package.File);
            if (!file.Exists || (file.Length, file.LastWriteTimeUtc.Ticks) != _sourceStamp)
                throw new IOException(Text("源包已更新，请重新打开预览。", "Package changed. Reopen preview."));
            if (_saveDraft is not null)
            {
                await _saveDraft(_selection, _parameters);
                _package.RestoreOptions(_selection); _package.RestoreParameters(_parameters);
            }
            else await _service!.SavePreviewAsync(_package, _selection, _parameters);
            _saved();
            if (!_closed && _view is not null)
                await _view.ExecuteScriptAsync("window.bemPreviewSaved(" + JsonSerializer.Serialize(_saveDraft is not null ? Text("已应用到组合默认值；生成组合包后生效。", "Applied to composition defaults. Generate the package to use them.") : Text("设置已保存；游戏中的生效时机遵循当前热切换设置。", "Settings saved. Game application follows your hot-switch settings.")) + ")");
        }
        catch (Exception ex)
        {
            if (!_closed && _view is not null) await _view.ExecuteScriptAsync("window.bemPreviewSaveError(" + JsonSerializer.Serialize(ex.Message) + ")");
        }
    }
    private async Task LoadAsync(string resource, string? selection, string? parameters, string gpu)
    {
        string id = Guid.NewGuid().ToString("N"), destination = Path.Combine(_directory, "data", id);
        using var loadCancellation = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        _draftLoadSuperseded = false;
        _loadCancellation = loadCancellation;
        loadCancellation.CancelAfter(TimeSpan.FromSeconds(25));
        var token = loadCancellation.Token;
        try
        {
            var current = _service is null ? _package : _service.Packages.SingleOrDefault(p => p.Id == _package.Id && p.File == _package.File)
                ?? throw new IOException(Text("模型包已移除，请关闭预览。", "Package removed. Close preview."));
            selection ??= current.IsComposable ? current.EncodedOptions() : current.SelectedAppearance;
            parameters ??= current.RememberedParameters();
            var source = new FileInfo(current.File);
            var stamp = (source.Length, source.LastWriteTimeUtc.Ticks);
            var capabilities = System.Text.Json.Nodes.JsonNode.Parse(gpu)!.AsObject();
            capabilities["nativeAssets"] = Path.Combine(_directory, "native");
            capabilities["textureCache"] = Path.Combine(_directory, "texture-cache");
            capabilities["compositionDraft"] = _saveDraft is not null;
            capabilities["desktop"] = true;
            gpu = capabilities.ToJsonString();
            var start = new ProcessStartInfo(Executable()) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true, StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8 };
            foreach (string value in new[] { "preview", "--package", _package.File, "--output-dir", destination, "--options", selection, "--parameters", parameters, "--resource", resource, "--gpu", gpu }) start.ArgumentList.Add(value);
            using var process = Process.Start(start) ?? throw new IOException("Cannot start preview helper");
            using var cancel = token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
            var stdout = process.StandardOutput.ReadToEndAsync(); var stderr = process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync(); string output = await stdout, errors = await stderr;
            token.ThrowIfCancellationRequested();
            using var report = JsonDocument.Parse(output);
            if (process.ExitCode != 0 || !report.RootElement.GetProperty("success").GetBoolean()) throw new IOException(report.RootElement.TryGetProperty("error", out var error) ? error.GetString() : errors);
            if (_closed || _view is null) return;
            using var scene = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Combine(destination, "scene.json"), token));
            _selection = scene.RootElement.GetProperty("selection").GetString() ?? "";
            _parameters = scene.RootElement.GetProperty("parameters").GetString() ?? "";
            source.Refresh();
            if (!source.Exists || (source.Length, source.LastWriteTimeUtc.Ticks) != stamp)
                throw new IOException(Text("源包已更新，请重新打开预览。", "Package changed. Reopen preview."));
            _sourceStamp = stamp;
            string? previous = _scene; _scene = id;
            await _view.ExecuteScriptAsync(BodyEditScript() + HighlightScript() + "window.bemPreviewLoad(" + JsonSerializer.Serialize(id) + ")");
            if (previous is not null)
            {
                var folders = Directory.GetDirectories(Path.Combine(_directory, "data"));
                foreach (string folder in folders) if (Path.GetFileName(folder) != id && Path.GetFileName(folder) != previous) Directory.Delete(folder, true);
            }
        }
        catch (OperationCanceledException)
        {
            if (!_closed && !_lifetime.IsCancellationRequested && loadCancellation.IsCancellationRequested && !_draftLoadSuperseded)
                await ShowDraftErrorAsync(Text("预览读取超时，请减少绘制段或更换来源后重试。", "Preview timed out. Reduce segments or change sources and retry."));
        }
        catch (Exception ex)
        {
            try { if (Directory.Exists(destination)) Directory.Delete(destination, true); } catch (IOException) { }
            if (!_closed && _view is not null) await _view.ExecuteScriptAsync("window.bemPreviewError(" + JsonSerializer.Serialize(ex.Message) + ")");
        }
        finally { _loadCancellation = null; }
    }
}

internal sealed class BemPreviewWindow : Window
{
    private static readonly HashSet<BemPreviewWindow> Windows = [];
    private readonly BemPreviewView _preview;
    private BemPreviewWindow(BemPackage package, string installRoot, BemPackageService service, Action saved)
    {
        Title = package.Name + " · " + (LocalizationService.Instance.IsChinese ? "模型预览" : "Model preview");
        _preview = new BemPreviewView(package, installRoot, service, saved);
        Content = _preview;
        WindowPlacementService.SetInitialSize(this, 1100, 800, App.MainWindowInstance);
        Closed += (_, _) => { _preview.Dispose(); Windows.Remove(this); };
    }
    public static void Open(BemPackage package, string installRoot, BemPackageService service, Action saved)
    {
        var window = new BemPreviewWindow(package, installRoot, service, saved);
        Windows.Add(window); window.Activate();
    }
    public static void CloseAll() { foreach (var window in Windows.ToArray()) window.Close(); }
}
