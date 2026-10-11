using System.Runtime.InteropServices;
using System.Text;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.Storage.Pickers;
using WinRT.Interop;
namespace BetterEndfieldNext.UI.Views;
public sealed class MediaScreenPage : UserControl
{
    internal event EventHandler? ConfigurationChanged;
    private readonly ToggleSwitch enabled = new(), audio = new(), loop = new();
    private readonly ToggleSwitch pauseBgm = new() { Name = "MediaScreenPauseBgmToggle", IsOn = true };
    private readonly ToggleSwitch wallScreen = new() { Name = "MediaScreenWallToggle", IsOn = true }, deskScreen = new() { Name = "MediaScreenDeskToggle", IsOn = true };
    private readonly ComboBox deskFit = new() { Name = "MediaScreenDeskFitMode" };
    private readonly ComboBox source = new(), windows = new();
    private readonly TextBox hotkey = new() { Name = "MediaScreenToggleHotkeyBox", Text = "-", IsSpellCheckEnabled = false };
    private readonly TextBox pauseHotkey = new() { Name = "MediaScreenPauseHotkeyBox", Text = "CTRL+-", IsSpellCheckEnabled = false };
    private readonly TextBox controlHotkey = new() { Name = "MediaScreenControlHotkeyBox", Text = "ALT+-", IsSpellCheckEnabled = false };
    private readonly TextBox path = new(), images = new() { AcceptsReturn = true, MinHeight = 100 }, target = new();
    private readonly NumberBox interval = Number(10, 1, 3600), gain = Number(1, 0, 2);
    private readonly NumberBox width = Number(1920, 64, MediaScreenConfiguration.MaxDimension), height = Number(1080, 64, MediaScreenConfiguration.MaxDimension), fps = Number(60, 1, MediaScreenConfiguration.MaxFps);
    private readonly ComboBox resolution = new() { Name = "MediaScreenResolutionPreset" }, frameRate = new() { Name = "MediaScreenFrameRatePreset" };
    private readonly StackPanel customResolution = new() { Spacing = 8 };
    private static readonly (int Width, int Height)[] Resolutions = [(1280, 720), (1920, 1080), (2560, 1440), (3840, 2160)];
    private static readonly int[] FrameRates = [30, 60, 90, 120];
    private readonly TextBlock message = new() { TextWrapping = TextWrapping.Wrap };
    private readonly Button pick = new(), refresh = new();
    private readonly TextBlock title = new() { FontSize = 26 }, introduction = new() { TextWrapping = TextWrapping.Wrap };
    private readonly Expander advanced = new() { HorizontalAlignment = HorizontalAlignment.Stretch };
    private ulong windowHandle; private uint windowPid;
    private static string T(string zh, string en) => LocalizationService.Instance.IsChinese ? zh : en;
    private static NumberBox Number(double value, double min, double max) => new()
    { Value = value, Minimum = min, Maximum = max, SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact };
    private sealed record CaptureWindow(ulong Handle, uint Pid, string Title) { public override string ToString() => Title; }
    private string Source => (source.SelectedItem as ComboBoxItem)?.Tag as string ?? "image";
    public MediaScreenPage()
    {
        var body = new StackPanel { Spacing = 14, Padding = new Thickness(24) };
        body.Children.Add(title); body.Children.Add(introduction);
        enabled.Header = T("启用媒体屏", "Enable media screen"); body.Children.Add(enabled);
        body.Children.Add(wallScreen); body.Children.Add(deskScreen);
        foreach (var mode in new[] { "fit", "crop", "stretch" }) deskFit.Items.Add(new ComboBoxItem { Tag = mode });
        deskFit.SelectedIndex = 0; body.Children.Add(deskFit);
        source.Header = T("来源", "Source");
        foreach (var item in new[] { ("image", T("图片", "Image")), ("slideshow", T("图片轮播", "Slideshow")), ("video", T("视频文件", "Video file")), ("window", T("本地窗口", "Local window")) })
            source.Items.Add(new ComboBoxItem { Tag = item.Item1, Content = item.Item2 });
        source.SelectedIndex = 0; body.Children.Add(source);
        path.Header = T("文件", "File"); body.Children.Add(path);
        images.Header = T("轮播图片（每行一个路径）", "Slideshow images (one path per line)"); body.Children.Add(images);
        pick.Content = T("选择文件", "Choose files"); pick.Click += Pick; body.Children.Add(pick);
        windows.Header = T("窗口", "Window"); body.Children.Add(windows);
        refresh.Content = T("刷新窗口列表", "Refresh windows"); refresh.Click += (_, _) => RefreshWindows(); body.Children.Add(refresh);
        body.Children.Add(pauseBgm);
        interval.Header = T("轮播间隔（秒）", "Slide interval (seconds)"); body.Children.Add(interval);
        audio.Header = T("播放视频声音", "Play video audio"); audio.IsOn = true; body.Children.Add(audio);
        gain.Header = T("音量", "Volume"); body.Children.Add(gain);
        loop.Header = T("循环播放", "Loop"); loop.IsOn = true; body.Children.Add(loop);
        width.Header = T("画布宽度", "Canvas width"); height.Header = T("画布高度", "Canvas height"); fps.Header = T("窗口捕获帧率", "Window capture FPS");
        for (int i = 0; i < Resolutions.Length; i++) {
            var size = Resolutions[i];
            string label = size.Height == 2160 ? "4K" : $"{size.Height}p";
            resolution.Items.Add(new ComboBoxItem { Content = $"{label} · {size.Width} × {size.Height}" });
        }
        resolution.Items.Add(new ComboBoxItem()); resolution.SelectedIndex = 1;
        foreach (int rate in FrameRates) frameRate.Items.Add(new ComboBoxItem { Content = $"{rate} FPS" });
        frameRate.Items.Add(new ComboBoxItem()); frameRate.SelectedIndex = 1;
        customResolution.Children.Add(width); customResolution.Children.Add(height);
        body.Children.Add(resolution); body.Children.Add(customResolution); body.Children.Add(frameRate); body.Children.Add(fps);
        hotkey.Header = T("启停快捷键", "Start/stop key");
        body.Children.Add(hotkey); body.Children.Add(pauseHotkey); body.Children.Add(controlHotkey);
        hotkey.PreviewKeyDown += CaptureHotkey; pauseHotkey.PreviewKeyDown += CaptureHotkey; controlHotkey.PreviewKeyDown += CaptureHotkey;
        target.Header = T("屏幕对象路径", "Screen object path"); target.Text = "ReceptionRoomImagePoster";
        advanced.Content = target; body.Children.Add(advanced);
        body.Children.Add(message); Content = new ScrollViewer { Content = body };
        source.SelectionChanged += (_, _) => Availability();
        resolution.SelectionChanged += (_, _) => {
            if (resolution.SelectedIndex >= 0 && resolution.SelectedIndex < Resolutions.Length) {
                var size = Resolutions[resolution.SelectedIndex]; width.Value = size.Width; height.Value = size.Height;
            }
            Availability(); ConfigurationChanged?.Invoke(this, EventArgs.Empty);
        };
        frameRate.SelectionChanged += (_, _) => {
            if (frameRate.SelectedIndex >= 0 && frameRate.SelectedIndex < FrameRates.Length) fps.Value = FrameRates[frameRate.SelectedIndex];
            Availability(); ConfigurationChanged?.Invoke(this, EventArgs.Empty);
        };
        windows.SelectionChanged += (_, _) => { if (windows.SelectedItem is CaptureWindow w) { windowHandle = w.Handle; windowPid = w.Pid; } };
        Availability(); UpdateLanguage();
        foreach (var item in new[] { enabled, audio, loop, pauseBgm, wallScreen, deskScreen }) item.Toggled += (_, _) => { Availability(); ConfigurationChanged?.Invoke(this, EventArgs.Empty); };
        foreach (var item in new[] { path, images, target, hotkey, pauseHotkey, controlHotkey }) item.TextChanged += (_, _) => ConfigurationChanged?.Invoke(this, EventArgs.Empty);
        foreach (var item in new[] { interval, gain, width, height, fps }) item.ValueChanged += (_, _) => ConfigurationChanged?.Invoke(this, EventArgs.Empty);
        foreach (var item in new[] { source, windows, deskFit }) item.SelectionChanged += (_, _) => ConfigurationChanged?.Invoke(this, EventArgs.Empty);
    }
    private void CaptureHotkey(object sender, KeyRoutedEventArgs args)
    {
        args.Handled = true;
        if (sender is not TextBox box || HotkeyService.IsModifier(args.Key)) return;
        if (args.Key is VirtualKey.Back or VirtualKey.Delete) { box.Text = ""; return; }
        string captured = HotkeyService.Capture(args.Key, args.KeyStatus.IsExtendedKey);
        if (captured.Length > 0) { box.Text = captured; box.SelectAll(); }
    }
    private static string ReadHotkey(TextBox box)
    {
        if (string.IsNullOrWhiteSpace(box.Text)) return "NONE";
        if (!HotkeyService.TryNormalize(box.Text, out string value))
            throw new InvalidDataException(T("快捷键无效", "Invalid hotkey"));
        return value;
    }
    internal void UpdateLanguage()
    {
        title.Text = T("会客室媒体屏", "Guest Room Media Screen");
        introduction.Text = T("进入会客室后按启停快捷键开始或恢复原画。默认启停键为主键盘 -。", "Use the toggle key in the guest room to start or restore. The default toggle is main-keyboard -.");
        enabled.Header = T("启用媒体屏", "Enable media screen"); source.Header = T("来源", "Source");
        wallScreen.Header = T("大屏投屏（墙面轮播屏）", "Project to large wall screens");
        deskScreen.Header = T("小屏投屏（桌面电脑）", "Project to small desk monitors");
        deskFit.Header = T("小屏画面适配", "Desk monitor image fit");
        string[] fitLabels = [T("完整显示（保持比例，留黑边）", "Fit (keep aspect ratio, letterbox)"), T("裁剪填满（保持比例）", "Fill (keep aspect ratio, crop)"), T("拉伸填满", "Stretch")];
        for (int i = 0; i < deskFit.Items.Count; ++i) ((ComboBoxItem)deskFit.Items[i]).Content = fitLabels[i];
        string[] labels = [T("图片", "Image"), T("图片轮播", "Slideshow"), T("视频文件", "Video file"), T("窗口", "Window")];
        for (int i = 0; i < source.Items.Count; ++i) ((ComboBoxItem)source.Items[i]).Content = labels[i];
        path.Header = T("文件路径", "File path"); images.Header = T("轮播图片（每行一个路径）", "Slideshow images (one path per line)");
        pick.Content = T("选择文件", "Choose files"); windows.Header = T("窗口", "Window"); refresh.Content = T("刷新窗口列表", "Refresh windows");
        interval.Header = T("轮播间隔（秒）", "Slide interval (seconds)"); audio.Header = T("播放视频声音", "Play video audio");
        gain.Header = T("音量", "Volume"); loop.Header = T("循环播放", "Loop");
        pauseBgm.Header = T("窗口投屏时暂停游戏 BGM", "Pause game BGM during window projection");
        resolution.Header = T("输出分辨率", "Output resolution"); frameRate.Header = T("窗口捕获帧率", "Window capture FPS");
        ((ComboBoxItem)resolution.Items[Resolutions.Length]).Content = T("自定义", "Custom");
        ((ComboBoxItem)frameRate.Items[FrameRates.Length]).Content = T("自定义", "Custom");
        controlHotkey.PlaceholderText = pauseHotkey.PlaceholderText = hotkey.PlaceholderText = T("未设置", "Not set");
        width.Header = T("画布宽度", "Canvas width"); height.Header = T("画布高度", "Canvas height"); fps.Header = T("窗口捕获帧率", "Window capture FPS");
        hotkey.Header = T("启停快捷键", "Start/stop key"); target.Header = T("屏幕对象路径", "Screen object path"); advanced.Header = T("高级设置", "Advanced");
        pauseHotkey.Header = T("视频暂停／继续快捷键", "Video pause/resume key");
        controlHotkey.Header = T("窗口操控快捷键", "Window control key");
    }
    private void Availability()
    {
        deskFit.IsEnabled = deskScreen.IsOn;
        path.Visibility = Source is "image" or "video" ? Visibility.Visible : Visibility.Collapsed;
        images.Visibility = interval.Visibility = Source == "slideshow" ? Visibility.Visible : Visibility.Collapsed;
        pick.Visibility = Source == "window" ? Visibility.Collapsed : Visibility.Visible;
        windows.Visibility = refresh.Visibility = Source == "window" ? Visibility.Visible : Visibility.Collapsed;
        audio.Visibility = gain.Visibility = Source == "video" ? Visibility.Visible : Visibility.Collapsed;
        pauseHotkey.Visibility = Source == "video" ? Visibility.Visible : Visibility.Collapsed;
        controlHotkey.Visibility = Source == "window" ? Visibility.Visible : Visibility.Collapsed;
        pauseBgm.Visibility = frameRate.Visibility = Source == "window" ? Visibility.Visible : Visibility.Collapsed;
        customResolution.Visibility = resolution.SelectedIndex == Resolutions.Length ? Visibility.Visible : Visibility.Collapsed;
        fps.Visibility = Source == "window" && frameRate.SelectedIndex == FrameRates.Length ? Visibility.Visible : Visibility.Collapsed;
    }
    private static int Integer(NumberBox control, int fallback) => double.IsFinite(control.Value) ? (int)control.Value : fallback;
    internal MediaScreenConfiguration ReadConfiguration()
    {
        var config = new MediaScreenConfiguration {
        Enabled = enabled.IsOn, Source = Source, SourcePath = path.Text.Trim(),
        WallScreenEnabled = wallScreen.IsOn, DeskScreenEnabled = deskScreen.IsOn, DeskFitMode = ((deskFit.SelectedItem as ComboBoxItem)?.Tag as string) ?? "fit",
        ImagePaths = string.Join('|', images.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)),
        TargetObject = target.Text.Trim(), AudioEnabled = audio.IsOn, PauseWindowBgm = pauseBgm.IsOn, Loop = loop.IsOn,
        Gain = double.IsFinite(gain.Value) ? gain.Value : 1, SlideInterval = double.IsFinite(interval.Value) ? interval.Value : 10,
        Width = Integer(width, 1920), Height = Integer(height, 1080), Fps = Integer(fps, 60),
        Hotkey = ReadHotkey(hotkey), PauseHotkey = ReadHotkey(pauseHotkey), ControlHotkey = ReadHotkey(controlHotkey), WindowHandle = windowHandle, WindowPid = windowPid
        };
        if ((long)config.Width * config.Height > MediaScreenConfiguration.MaxPixels)
            throw new InvalidDataException(T("分辨率超过 4K 像素上限", "Resolution exceeds the 4K pixel limit"));
        string[] bindings = [config.Hotkey, config.PauseHotkey, config.ControlHotkey];
        if (bindings.Where(key => key != "NONE").Distinct().Count() != bindings.Count(key => key != "NONE"))
            throw new InvalidDataException(T("媒体屏快捷键不能重复", "Media screen shortcuts must be distinct"));
        return config;
    }
    internal void ApplyConfiguration(MediaScreenConfiguration config)
    {
        enabled.IsOn = config.Enabled; source.SelectedIndex = Array.IndexOf(new[] { "image", "slideshow", "video", "window" }, config.Source);
        path.Text = config.SourcePath; images.Text = config.ImagePaths.Replace("|", Environment.NewLine);
        target.Text = config.TargetObject; audio.IsOn = config.AudioEnabled; pauseBgm.IsOn = config.PauseWindowBgm; loop.IsOn = config.Loop;
        wallScreen.IsOn = config.WallScreenEnabled; deskScreen.IsOn = config.DeskScreenEnabled;
        deskFit.SelectedIndex = config.DeskFitMode switch { "crop" => 1, "stretch" => 2, _ => 0 };
        gain.Value = config.Gain; interval.Value = config.SlideInterval; width.Value = config.Width; height.Value = config.Height; fps.Value = config.Fps;
        int preset = Array.IndexOf(Resolutions, (config.Width, config.Height)); resolution.SelectedIndex = preset < 0 ? Resolutions.Length : preset;
        preset = Array.IndexOf(FrameRates, config.Fps); frameRate.SelectedIndex = preset < 0 ? FrameRates.Length : preset;
        hotkey.Text = config.Hotkey.Replace("PLUS", "=", StringComparison.Ordinal);
        pauseHotkey.Text = config.PauseHotkey is "NONE" or "OFF" or "DISABLED" ? "" : config.PauseHotkey.Replace("PLUS", "=", StringComparison.Ordinal);
        controlHotkey.Text = config.ControlHotkey is "NONE" or "OFF" or "DISABLED" ? "" : config.ControlHotkey.Replace("PLUS", "=", StringComparison.Ordinal);
        windowHandle = config.WindowHandle; windowPid = config.WindowPid; Availability();
    }
    private async void Pick(object sender, RoutedEventArgs args)
    {
        try {
            var picker = new FileOpenPicker();
            foreach (var extension in Source == "video" ? new[] { ".mp4" } : new[] { ".png", ".jpg", ".jpeg" }) picker.FileTypeFilter.Add(extension);
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
            if (Source == "slideshow") {
                var files = await picker.PickMultipleFilesAsync(); if (files.Count > 0) images.Text = string.Join(Environment.NewLine, files.Select(file => file.Path));
            } else { var file = await picker.PickSingleFileAsync(); if (file is not null) path.Text = file.Path; }
            message.Text = "";
        } catch (Exception error) { message.Text = error.Message; }
    }
    private void RefreshWindows()
    {
        var list = new List<CaptureWindow>();
        EnumWindows((handle, _) => {
            if (!IsWindowVisible(handle)) return true;
            var title = new StringBuilder(512); GetWindowText(handle, title, title.Capacity); GetWindowThreadProcessId(handle, out uint pid);
            if (pid != (uint)Environment.ProcessId && title.Length > 0) list.Add(new(unchecked((ulong)handle.ToInt64()), pid, title.ToString()));
            return true;
        }, 0);
        windows.ItemsSource = list.OrderBy(item => item.Title).ToList();
        windows.SelectedItem = list.FirstOrDefault(item => item.Handle == windowHandle && item.Pid == windowPid);
        message.Text = list.Count == 0 ? T("没有可选窗口", "No windows available") : "";
    }
    private delegate bool EnumCallback(nint handle, nint state);
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumCallback callback, nint state);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(nint handle);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetWindowText(nint handle, StringBuilder text, int count);
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(nint handle, out uint process);
}
