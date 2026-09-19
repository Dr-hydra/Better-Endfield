using System.Diagnostics;
using System.Text;
using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

internal sealed class BemConverterWindow : Window
{
    private readonly string _installRoot;
    private readonly ComboBox _task = new() { Header = "我要做什么", HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly StackPanel _steps = new() { Spacing = 18 };
    private readonly TextBlock _intro = Text("");
    private readonly InfoBar _status = new() { IsClosable = false, IsOpen = false };
    private readonly TextBox _details = new() { IsReadOnly = true, AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, MaxHeight = 260 };
    private readonly Expander _detailPanel = new() { Header = "技术详情（可交给作者或维护者）", Visibility = Visibility.Collapsed, HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly Button _saveReport = new() { Content = "保存检查报告…", IsEnabled = false };
    private readonly Button _cancel = new() { Content = "取消处理", Visibility = Visibility.Collapsed };
    private readonly ProgressRing _progress = new() { Width = 22, Height = 22, IsActive = false, Visibility = Visibility.Collapsed };
    private readonly StackPanel _result = new() { Spacing = 8, Visibility = Visibility.Collapsed };
    private CancellationTokenSource? _operation;
    private string _source = "", _report = "", _scratch = "", _prepared = "", _exported = "";
    private string Mode => (_task.SelectedItem as ComboBoxItem)?.Tag as string ?? "convert";
    private bool _closed;

    public BemConverterWindow(string installRoot)
    {
        _installRoot = installRoot; Title = "BEM 创作者工具";
        AppWindow.Resize(new Windows.Graphics.SizeInt32(900, 820));
        var body = new StackPanel { Spacing = 18, Padding = new Thickness(28), MaxWidth = 840, HorizontalAlignment = HorizontalAlignment.Stretch };
        body.Children.Add(Text("BEM 创作者工具", 28));
        body.Children.Add(Text("先选任务。只想使用下载的 BEM / ZIP？回到“角色外观”直接导入即可。"));
        foreach (var item in new[] { ("转换其他来源的 Mod", "convert"), ("解包 BEM / ZIP", "unpack"), ("将项目打包为 BEM", "pack"), ("制作多 Mod ZIP 合集", "bundle") })
            _task.Items.Add(new ComboBoxItem { Content = item.Item1, Tag = item.Item2 });
        body.Children.Add(_task); body.Children.Add(_intro); body.Children.Add(_steps);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        actions.Children.Add(_progress); actions.Children.Add(_cancel); actions.Children.Add(_saveReport);
        var docs = Button("帮助与支持范围", ShowGuide); actions.Children.Add(docs);
        body.Children.Add(actions); body.Children.Add(_status); body.Children.Add(_result);
        _detailPanel.Content = _details; body.Children.Add(_detailPanel);
        Content = new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        _task.SelectionChanged += (_, _) => ResetTask(); _task.SelectedIndex = 0;
        _cancel.Click += (_, _) => _operation?.Cancel();
        _saveReport.Click += async (_, _) =>
        {
            try { string? file = await SavePath(".json", "检查报告", "bem-report"); if (file != null) await File.WriteAllTextAsync(file, _report, new UTF8Encoding(false)); }
            catch (Exception ex) { Status("无法保存报告", ex.Message, InfoBarSeverity.Error); }
        };
        Closed += (_, _) => { _closed = true; if (_operation != null) _operation.Cancel(); else ClearPrepared(); };
    }

    private static TextBlock Text(string value, double size = 14) => new() { Text = value, FontSize = size, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true };
    private Button Button(string label, Func<Task> action, bool primary = false)
    {
        var b = new Button { Content = label, HorizontalAlignment = HorizontalAlignment.Left };
        if (primary) b.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
        b.Click += async (_, _) => { try { await action(); } catch (Exception ex) { Status("操作未完成", BemReportPresentation.Failure(ex.Message), InfoBarSeverity.Error); } };
        return b;
    }
    private static Border Card(string title, params UIElement[] elements)
    {
        var content = new StackPanel { Spacing = 12 }; content.Children.Add(Text(title, 19));
        foreach (var e in elements) content.Children.Add(e);
        return new Border { Child = content, Padding = new Thickness(18), CornerRadius = new CornerRadius(8), BorderThickness = new Thickness(1),
            BorderBrush = (Brush)Application.Current.Resources["CardStrokeColorDefaultBrush"], Background = (Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] };
    }
    private void Status(string title, string message, InfoBarSeverity severity = InfoBarSeverity.Informational)
    { _status.Title = title; _status.Message = message; _status.Severity = severity; _status.IsOpen = true; }
    private void Report(string raw)
    { _report = raw; _details.Text = raw; _saveReport.IsEnabled = raw.Length > 0; _detailPanel.Visibility = Visibility.Visible; }
    private void ClearPrepared()
    {
        _prepared = "";
        if (_scratch.Length > 0 && Directory.Exists(_scratch)) Directory.Delete(_scratch, true);
        _scratch = "";
    }
    private void ResetTask()
    {
        if (_operation != null) return;
        ClearPrepared(); _source = _exported = _report = ""; _steps.Children.Clear(); _result.Children.Clear(); _result.Visibility = Visibility.Collapsed;
        _status.IsOpen = false; _saveReport.IsEnabled = false; _detailPanel.Visibility = Visibility.Collapsed; _detailPanel.IsExpanded = false;
        if (Mode == "convert")
        {
            _intro.Text = "选源 Mod → 自动检查 → 转换并导出 BEM。角色资料由工具管理；无法转换时会列出具体缺项。";
            var choices = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
            choices.Children.Add(Button("选择 Mod 压缩包…", () => SelectSource(false), true));
            choices.Children.Add(Button("选择已解压目录…", () => SelectSource(true)));
            _steps.Children.Add(Card("1  选择源 Mod", Text("支持 ZIP、RAR、7z 或已解压目录；选好后自动读取并检查。"), choices));
        }
        else
        {
            _intro.Text = Mode switch
            {
                "unpack" => "BEM 解包为可编辑项目；ZIP 合集解出独立 BEM。原文件不会改变。",
                "pack" => "选择解包得到的 project.json，校验资源后输出 BEM。",
                _ => "选择一个或多个 BEM，校验后封装为网站可上传的标准 ZIP。"
            };
            _steps.Children.Add(Card("1  选择输入", Text(Mode == "pack" ? "需要 project.json 及其引用的 payloads 文件。不是转换配方 JSON。" : Mode == "unpack" ? "解包不会还原 Blender 工程或源 Mod 脚本。" : "可包含不同角色；每个 BEM 内的多外观会完整保留。"),
                Button(Mode == "pack" ? "选择 project.json…" : Mode == "unpack" ? "选择 BEM / ZIP…" : "选择 BEM（可多选）…", SelectProjectInput, true)));
        }
    }
    private void SetStepButtons(bool enabled)
    {
        void Walk(DependencyObject node)
        {
            if (node is Control control) control.IsEnabled = enabled;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++) Walk(VisualTreeHelper.GetChild(node, i));
        }
        Walk(_steps);
    }
    private async Task Run(string label, Func<CancellationToken, Task> action)
    {
        if (_operation != null) return;
        _operation = new(); _task.IsEnabled = false; SetStepButtons(false); _saveReport.IsEnabled = false;
        _progress.IsActive = true; _progress.Visibility = _cancel.Visibility = Visibility.Visible;
        Status(label, "正在处理，可以取消。");
        try { await action(_operation.Token); }
        catch (OperationCanceledException) { Status("已取消", "未完成输出。可以重新选择输入或重试。"); }
        catch (Exception ex) { Report(ex.Message); Status("未完成", BemReportPresentation.Failure(ex.Message), InfoBarSeverity.Error); }
        finally
        {
            _operation.Dispose(); _operation = null; _task.IsEnabled = true; SetStepButtons(true);
            _saveReport.IsEnabled = _report.Length > 0; _progress.IsActive = false; _progress.Visibility = _cancel.Visibility = Visibility.Collapsed;
            if (_closed) ClearPrepared();
        }
    }
    private async Task SelectSource(bool folder)
    {
        if (_operation != null) return;
        string? path = folder ? await FolderPath() : await OpenPath(".zip", ".rar", ".7z", ".bem");
        if (path == null) return;
        ResetTask(); _source = path;
        while (_steps.Children.Count > 1) _steps.Children.RemoveAt(1);
        _steps.Children.Add(Card("已选择", Text(path), Button("重新检查", InspectSource)));
        await InspectSource();
    }
    private async Task InspectSource()
    {
        ClearPrepared(); while (_steps.Children.Count > 2) _steps.Children.RemoveAt(2);
        _result.Children.Clear(); _result.Visibility = Visibility.Collapsed;
        await Run("正在检查源 Mod", async token =>
        {
            string raw = await BemToolService.RunAsync(_installRoot, ["inspect", _source], token); Report(raw);
            var summary = BemInspectionSummary.Read(raw);
            var elements = new List<UIElement> { Text(summary.Detail), Text(summary.NextStep) };
            if (summary.CanAutoConvert)
                elements.Add(Button("转换为 BEM", () => PrepareConversion(null), true));
            if (summary.CanProvideRecipe)
            {
                var advancedBody = new StackPanel { Spacing = 12 };
                advancedBody.Children.Add(Text("仅供维护转换器的开发者使用。源 Mod 转换规则描述入口、静态外观与材质/骨骼映射。当前离线规则需包含已核实的依赖，工具会先实际转换并校验。这不是普通用户选择角色资料的步骤。"));
                advancedBody.Children.Add(Button("加载源 Mod 转换规则并验证…", PrepareRecipe));
                elements.Add(new Expander { Header = "开发者：验证源 Mod 转换规则（可跳过）", Content = advancedBody, HorizontalAlignment = HorizontalAlignment.Stretch });
            }
            _steps.Children.Add(Card("2  " + summary.Title, elements.ToArray()));
            Status(summary.AlreadyPackaged ? "无需转换" : "检查完成", summary.NextStep,
                summary.CanAutoConvert ? InfoBarSeverity.Success : summary.AlreadyPackaged ? InfoBarSeverity.Informational : InfoBarSeverity.Warning);
        });
    }
    private async Task PrepareRecipe()
    {
        string? recipe = await OpenPath(".json"); if (recipe == null) return;
        await PrepareConversion(recipe);
    }
    private async Task PrepareConversion(string? recipe)
    {
        ClearPrepared(); while (_steps.Children.Count > 3) _steps.Children.RemoveAt(3);
        _scratch = Path.Combine(Path.GetTempPath(), "BemPrepare-" + Guid.NewGuid()); Directory.CreateDirectory(_scratch);
        string temp = Path.Combine(_scratch, "prepared.bem");
        await Run("正在适配并校验模型", async token =>
        {
            List<string> args = ["convert", _source, "-o", temp];
            if (recipe != null) args.AddRange(["--recipe", recipe]);
            string raw = await BemToolService.RunAsync(_installRoot, args, token);
            token.ThrowIfCancellationRequested(); Report(raw); _prepared = temp;
            _steps.Children.Add(Card("3  已准备好，可以导出", Text(BemReportPresentation.Package(raw)), Text("适配与结构校验通过；游戏中的显示效果仍需实机确认。"),
                Button("导出 BEM…", ExportPrepared, true)));
            Status("适配成功", "点击“导出 BEM”选择保存位置，然后在角色外观管理中导入。", InfoBarSeverity.Success);
        });
    }
    private async Task ExportPrepared()
    {
        if (_prepared.Length == 0 || !File.Exists(_prepared)) return;
        string? output = await SavePath(".bem", "BEM 模型包", Path.GetFileNameWithoutExtension(_source)); if (output == null) return;
        await Run("正在保存 BEM", async token =>
        {
            string temp = Path.Combine(Path.GetDirectoryName(output)!, ".bem-export-" + Guid.NewGuid());
            try
            {
                await using (var input = File.OpenRead(_prepared))
                await using (var target = File.Create(temp)) await input.CopyToAsync(target, token);
                token.ThrowIfCancellationRequested(); File.Move(temp, output, true);
            }
            finally { if (File.Exists(temp)) File.Delete(temp); }
            Completed(output, "回到“角色外观”导入该 BEM，选择外观并启用。下次启动游戏生效。");
        });
    }
    private async Task SelectProjectInput()
    {
        if (_operation != null) return;
        List<string> files;
        if (Mode == "bundle")
        {
            var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".bem"); Init(picker);
            files = (await picker.PickMultipleFilesAsync()).Select(f => f.Path).ToList();
        }
        else
        {
            string? file = await OpenPath(Mode == "pack" ? [".json"] : [".bem", ".zip"]); if (file == null) return;
            files = [file];
        }
        if (files.Count == 0) return;
        ResetTask(); _source = files[0];
        _steps.Children.Add(Card($"2  已选择 {files.Count} 个文件", Text(string.Join("\n", files)),
            Button(Mode == "unpack" ? "选择解包位置并开始…" : Mode == "pack" ? "校验并保存 BEM…" : "校验并保存 ZIP…", () => ProcessProject(files), true)));
    }
    private async Task ProcessProject(List<string> files)
    {
        string mode = Mode; string? output;
        if (mode == "unpack")
        {
            string? folder = await FolderPath(); if (folder == null) return;
            output = Path.Combine(folder, Path.GetFileNameWithoutExtension(files[0]) + "-project");
            if (Directory.Exists(output)) { Status("目标目录已存在", "请选择其他位置，避免混入旧项目：\n" + output, InfoBarSeverity.Warning); return; }
        }
        else output = await SavePath(mode == "bundle" ? ".zip" : ".bem", mode == "bundle" ? "BEM 合集" : "BEM 模型包", mode == "bundle" ? "mod-collection" : "appearance");
        if (output == null) return;
        await Run("正在校验并处理", async token =>
        {
            List<string> args = [mode, .. files, "-o", output];
            string raw = await BemToolService.RunAsync(_installRoot, args, token); Report(raw);
            Completed(output, BemReportPresentation.Package(raw) + "\n" + (mode == "unpack" ? "BEM 项目可编辑 project.json 后重新打包；ZIP 解出的 BEM 可在管理页导入。" : mode == "bundle" ? "这个 ZIP 可以上传分发，也可以在角色外观管理中直接导入。" : "BEM 已生成，可返回角色外观管理导入。"));
        });
    }
    private void Completed(string path, string next)
    {
        _exported = path; _result.Children.Clear(); _result.Visibility = Visibility.Visible;
        _result.Children.Add(Card("已完成", Text(path), Text(next), Button("打开输出位置", () =>
        {
            string folder = Directory.Exists(_exported) ? _exported : Path.GetDirectoryName(_exported)!;
            Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true }); return Task.CompletedTask;
        })));
        Status("处理完成", "输出位置和下一步见下方。", InfoBarSeverity.Success);
    }
    private void Init(object picker) => InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(this));
    private async Task<string?> OpenPath(params string[] types)
    {
        var picker = new FileOpenPicker(); foreach (var type in types) picker.FileTypeFilter.Add(type); Init(picker);
        return (await picker.PickSingleFileAsync())?.Path;
    }
    private async Task<string?> FolderPath()
    {
        var picker = new FolderPicker(); picker.FileTypeFilter.Add("*"); Init(picker); return (await picker.PickSingleFolderAsync())?.Path;
    }
    private async Task<string?> SavePath(string extension, string title, string suggested)
    {
        var picker = new FileSavePicker { SuggestedFileName = suggested }; picker.FileTypeChoices.Add(title, new List<string> { extension }); Init(picker);
        return (await picker.PickSaveFileAsync())?.Path;
    }
    private async Task ShowGuide()
    {
        string path = Path.Combine(_installRoot, "docs", "BEM_CREATOR_GUIDE.md");
        if (!File.Exists(path)) path = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory, "docs", "BEM_CREATOR_GUIDE.md");
        string guide = await File.ReadAllTextAsync(path, Encoding.UTF8);
        await new ContentDialog
        {
            XamlRoot = ((FrameworkElement)Content).XamlRoot, Title = "创作者帮助", CloseButtonText = "返回任务",
            Content = new ScrollViewer { MaxHeight = 520, Content = Text(guide), VerticalScrollBarVisibility = ScrollBarVisibility.Auto }
        }.ShowAsync();
    }
}
