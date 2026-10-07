using System.Diagnostics;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using BetterEndfield.UI.Controls;
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
    private readonly ComboBox _task = new() { Header = BemText.Get("我要做什么"), HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly StackPanel _steps = new() { Spacing = 18 };
    private readonly TextBlock _intro = Text("");
    private readonly InfoBar _status = new() { IsClosable = false, IsOpen = false };
    private readonly TextBox _details = new() { IsReadOnly = true, AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, MaxHeight = 260 };
    private readonly Expander _detailPanel = new() { Header = BemText.Get("技术详情（可交给作者或维护者）"), Visibility = Visibility.Collapsed, HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly Button _saveReport = new() { Content = BemText.Get("保存检查报告…"), IsEnabled = false };
    private readonly Button _cancel = new() { Content = BemText.Get("取消处理"), Visibility = Visibility.Collapsed };
    private readonly ProgressRing _progress = new() { Width = 22, Height = 22, IsActive = false, Visibility = Visibility.Collapsed };
    private readonly StackPanel _result = new() { Spacing = 8, Visibility = Visibility.Collapsed };
    private CancellationTokenSource? _operation;
    private string _source = "", _report = "", _scratch = "", _prepared = "", _exported = "";
    private string _workspaceSource = "", _workspaceDirectory = "", _workspaceRecipe = "", _workspaceMode = "convert";
    private string Mode => (_task.SelectedItem as ComboBoxItem)?.Tag as string ?? "convert";
    private bool _closed;
    private BemExportProject _project = new();
    private string? _projectFile;

    public BemConverterWindow(string installRoot, Window? owner = null, bool standalone = false)
    {
        _installRoot = installRoot; Title = BemText.Get("BEM 创作者工具");
        WindowPlacementService.SetInitialSize(this, 900, 820, owner);
        var page = new PageContentPanel { Padding = new Thickness(28), MaxContentWidth = 840 };
        var body = new StackPanel { Spacing = 18 };
        page.Children.Add(body);
        body.Children.Add(Text(() => BemText.Get("BEM 创作者工具"), 28));
        body.Children.Add(Text(() => BemText.Get(standalone
            ? "先选任务。导出的 BEM / ZIP 可在 Better Endfield 的“角色外观”页面导入使用。"
            : "先选任务。只想使用下载的 BEM / ZIP？回到“角色外观”直接导入即可。")));
        foreach (var item in new (Func<string> Label, string Mode)[] {
            (() => BemText.Get("创建 / 打开导出工程"), "build"),
            (() => BemText.Get("创建标准工作区"), "workspace"),
            (() => BemText.Get("转换其他来源的 Mod"), "convert"),
            (() => BemText.Get("解包 BEM / ZIP"), "unpack"),
            (() => BemText.Get("将项目打包为 BEM"), "pack"),
            (() => BemText.Get("制作多 Mod ZIP 合集"), "bundle") })
            _task.Items.Add(Choice(item.Label, item.Mode));
        BemLocalizedUI.Set(_task, ComboBox.HeaderProperty, () => BemText.Get("我要做什么"));
        BemLocalizedUI.Set(_detailPanel, Expander.HeaderProperty, () => BemText.Get("技术详情（可交给作者或维护者）"));
        BemLocalizedUI.Set(_saveReport, Microsoft.UI.Xaml.Controls.Button.ContentProperty, () => BemText.Get("保存检查报告…"));
        BemLocalizedUI.Set(_cancel, Microsoft.UI.Xaml.Controls.Button.ContentProperty, () => BemText.Get("取消处理"));
        body.Children.Add(_task); body.Children.Add(_intro); body.Children.Add(_steps);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        actions.Children.Add(_progress); actions.Children.Add(_cancel); actions.Children.Add(_saveReport);
        var docs = Button(() => BemText.Get("帮助与支持范围"), ShowGuide); actions.Children.Add(docs);
        body.Children.Add(actions); body.Children.Add(_status); body.Children.Add(_result);
        _detailPanel.Content = _details; body.Children.Add(_detailPanel);
        Content = new ScrollViewer { Content = page, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        _task.SelectionChanged += (_, _) => ResetTask(); _task.SelectedIndex = 0;
        _cancel.Click += (_, _) => _operation?.Cancel();
        _saveReport.Click += async (_, _) =>
        {
            try { string? file = await SavePath(".json", BemText.Get("检查报告"), "bem-report"); if (file != null) await File.WriteAllTextAsync(file, _report, new UTF8Encoding(false)); }
            catch (Exception ex) { Status(() => BemText.Get("无法保存报告"), () => ex.Message, InfoBarSeverity.Error); }
        };
        LocalizationService.Instance.PropertyChanged += LanguageChanged;
        Closed += (_, _) =>
        {
            _closed = true;
            LocalizationService.Instance.PropertyChanged -= LanguageChanged;
            if (_operation != null) _operation.Cancel(); else ClearPrepared();
        };
    }

    private void LanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(LocalizationService.IsChinese)) return;
        DispatcherQueue.TryEnqueue(() =>
        {
            if (_closed) return;
            Title = BemText.Get("BEM 创作者工具");
            if (Content is DependencyObject root) BemLocalizedUI.Refresh(root);
        });
    }

    private static ComboBoxItem Choice(Func<string> label, string tag)
    {
        var item = new ComboBoxItem { Tag = tag };
        BemLocalizedUI.Set(item, ComboBoxItem.ContentProperty, label);
        return item;
    }

    private static TextBlock Text(string value, double size = 14) => new() { Text = value, FontSize = size, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true };
    private static TextBlock Text(Func<string> value, double size = 14)
    {
        var text = Text("", size);
        BemLocalizedUI.Set(text, TextBlock.TextProperty, value);
        return text;
    }
    private Button Button(string label, Func<Task> action, bool primary = false)
    {
        var b = new Button { Content = label, HorizontalAlignment = HorizontalAlignment.Left };
        if (primary) b.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
        b.Click += async (_, _) => { try { await action(); } catch (Exception ex) { Status(() => BemText.Get("操作未完成"), () => BemReportPresentation.Failure(ex.Message), InfoBarSeverity.Error); } };
        return b;
    }
    private Button Button(Func<string> label, Func<Task> action, bool primary = false)
    {
        var button = Button("", action, primary);
        BemLocalizedUI.Set(button, Microsoft.UI.Xaml.Controls.Button.ContentProperty, label);
        return button;
    }
    private static Border Card(string title, params UIElement[] elements)
    {
        var content = new StackPanel { Spacing = 12 }; content.Children.Add(Text(title, 19));
        foreach (var e in elements) content.Children.Add(e);
        return new Border { Child = content, Padding = new Thickness(18), CornerRadius = new CornerRadius(8), BorderThickness = new Thickness(1),
            BorderBrush = (Brush)Application.Current.Resources["CardStrokeColorDefaultBrush"], Background = (Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] };
    }
    private static Border Card(Func<string> title, params UIElement[] elements)
    {
        var card = Card("", elements);
        var heading = (TextBlock)((StackPanel)card.Child).Children[0];
        BemLocalizedUI.Set(heading, TextBlock.TextProperty, title);
        return card;
    }
    private void Status(Func<string> title, Func<string> message, InfoBarSeverity severity = InfoBarSeverity.Informational)
    {
        BemLocalizedUI.Set(_status, InfoBar.TitleProperty, title);
        BemLocalizedUI.Set(_status, InfoBar.MessageProperty, message);
        _status.Severity = severity; _status.IsOpen = true;
    }
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
        if (Mode == "workspace")
        {
            BemLocalizedUI.Set(_intro, TextBlock.TextProperty, () => BemText.Get("复制输入并创建可移动的 BEM 创作者工作区，继续使用 export.bemproj.json。"));
            RenderWorkspace();
        }
        else if (Mode == "build")
        {
            BemLocalizedUI.Set(_intro, TextBlock.TextProperty, () => BemText.Get("保存输入、转换配方、体型滑条与输出参数；打开工程后可修改并重复导出。BEM 1.3 支持作者提供的位置形态，路径相对工程目录。"));
            RenderExportProject();
        }
        else if (Mode == "convert")
        {
            BemLocalizedUI.Set(_intro, TextBlock.TextProperty, () => BemText.Get("选源 Mod → 自动检查 → 转换并导出 BEM。角色资料由工具管理；无法转换时会列出具体缺项。"));
            var choices = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
            choices.Children.Add(Button(() => BemText.Get("选择 Mod 压缩包…"), () => SelectSource(false), true));
            choices.Children.Add(Button(() => BemText.Get("选择已解压目录…"), () => SelectSource(true)));
            _steps.Children.Add(Card(() => BemText.Get("1  选择源 Mod"), Text(() => BemText.Get("支持 ZIP、RAR、7z 或已解压目录；选好后自动读取并检查。")), choices));
        }
        else
        {
            BemLocalizedUI.Set(_intro, TextBlock.TextProperty, () => Mode switch
            {
                "unpack" => BemText.Get("BEM 解包为可编辑项目；ZIP 合集解出独立 BEM。原文件不会改变。"),
                "pack" => BemText.Get("选择解包得到的 project.json，校验资源后输出 BEM。"),
                _ => BemText.Get("选择一个或多个 BEM，校验后封装为网站可上传的标准 ZIP。")
            });
            _steps.Children.Add(Card(() => BemText.Get("1  选择输入"), Text(() => Mode == "pack" ? BemText.Get("需要 project.json 及其引用的 payloads 文件。不是转换配方 JSON。") : Mode == "unpack" ? BemText.Get("解包不会还原 Blender 工程或源 Mod 脚本。") : BemText.Get("可包含不同角色；每个 BEM 内的多外观会完整保留。")),
                Button(() => Mode == "pack" ? BemText.Get("选择 project.json…") : Mode == "unpack" ? BemText.Get("选择 BEM / ZIP…") : BemText.Get("选择 BEM（可多选）…"), SelectProjectInput, true)));
        }
    }
    private TextBox ProjectField(string label, string initial, Action<string> change)
    {
        var field = new TextBox { Header = label, Text = initial, HorizontalAlignment = HorizontalAlignment.Stretch };
        field.TextChanged += (_, _) => change(field.Text);
        return field;
    }
    private TextBox ProjectField(Func<string> label, string initial, Action<string> change)
    {
        var field = ProjectField("", initial, change);
        BemLocalizedUI.Set(field, TextBox.HeaderProperty, label);
        return field;
    }
    private void RenderExportProject()
    {
        _steps.Children.Clear();
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        actions.Children.Add(Button(() => BemText.Get("新建工程"), () => { _project = new(); _projectFile = null; RenderExportProject(); return Task.CompletedTask; }));
        actions.Children.Add(Button(() => BemText.Get("打开工程…"), OpenExportProject));
        actions.Children.Add(Button(() => BemText.Get("保存工程…"), () => SaveExportProject(true)));
        _steps.Children.Add(Card(() => _projectFile ?? BemText.Get("尚未保存的导出工程"), actions));
        var mode = new ComboBox { HorizontalAlignment = HorizontalAlignment.Stretch };
        BemLocalizedUI.Set(mode, ComboBox.HeaderProperty, () => BemText.Get("输入类型"));
        mode.Items.Add(Choice(() => BemText.Get("源 Mod（自动匹配或使用转换配方）"), "convert"));
        mode.Items.Add(Choice(() => BemText.Get("BEM 可编辑项目（project.json + payloads）"), "pack"));
        mode.SelectedIndex = _project.Mode == "pack" ? 1 : 0;
        mode.SelectionChanged += (_, _) => { _project.Mode = (mode.SelectedItem as ComboBoxItem)?.Tag as string ?? "convert"; };
        var inputActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        inputActions.Children.Add(Button(() => BemText.Get("选择输入文件…"), () => SelectExportInput(false)));
        inputActions.Children.Add(Button(() => BemText.Get("选择源目录…"), () => SelectExportInput(true)));
        var recipeActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        recipeActions.Children.Add(Button(() => BemText.Get("选择转换配方…"), async () =>
        {
            string? file = await OpenPath(".json"); if (file == null) return;
            _project.Recipe = _projectFile == null ? file : BemExportProject.PortablePath(file, _projectFile);
            RenderExportProject();
        }));
        var deformationActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        deformationActions.Children.Add(Button(() => BemText.Get("选择形态配置…"), async () =>
        {
            string? file = await OpenPath(".json"); if (file == null) return;
            _project.Deformations = _projectFile == null ? file : BemExportProject.PortablePath(file, _projectFile);
            RenderExportProject();
        }));
        deformationActions.Children.Add(Button(() => BemText.Get("编辑 / 新建形态配置…"), EditDeformations));
        _steps.Children.Add(Card(() => BemText.Get("输入和导出参数"), mode,
            ProjectField(() => BemText.Get("源文件或目录"), _project.Source, value => _project.Source = value), inputActions,
            ProjectField(() => BemText.Get("转换配方（自动转换可留空；打包模式留空）"), _project.Recipe, value => _project.Recipe = value), recipeActions,
            ProjectField(() => BemText.Get("BEM 1.3 形态配置（可留空）"), _project.Deformations, value => _project.Deformations = value), deformationActions,
            Text(() => BemText.Get("配置滑条名称、范围、默认值与网格形态。支持同顶点顺序目标、稀疏位置增量和官方 EFMI ShapeKey buffers 的显式绑定；原骨骼和材质继续使用。")),
            ProjectField(() => BemText.Get("BEM 输出路径"), _project.Output, value => _project.Output = value),
            ProjectField(() => BemText.Get("报告路径（可留空）"), _project.Report, value => _project.Report = value)));
        _steps.Children.Add(Card(() => BemText.Get("包信息"), Text(() => BemText.Get("包 ID 在工程中保持不变；再次导出同 ID 可更新已发布包。别名、骨架、纹理与外观规则保存在配方/输入项目中。")),
            ProjectField(() => BemText.Get("包 ID"), _project.Package["id"], value => _project.Package["id"] = value),
            ProjectField(() => BemText.Get("名称"), _project.Package["name"], value => _project.Package["name"] = value),
            ProjectField(() => BemText.Get("作者"), _project.Package["author"], value => _project.Package["author"] = value),
            ProjectField(() => BemText.Get("包版本"), _project.Package["version"], value => _project.Package["version"] = value),
            Button(() => BemText.Get("保存参数并导出 BEM"), BuildExportProject, true)));
    }

    private void RenderWorkspace()
    {
        _steps.Children.Clear();
        var mode = new ComboBox { HorizontalAlignment = HorizontalAlignment.Stretch };
        BemLocalizedUI.Set(mode, ComboBox.HeaderProperty, () => BemText.Get("输入类型"));
        mode.Items.Add(Choice(() => BemText.Get("源 Mod"), "convert"));
        mode.Items.Add(Choice(() => BemText.Get("BEM 可编辑项目"), "pack"));
        mode.SelectedIndex = _workspaceMode == "pack" ? 1 : 0;
        mode.SelectionChanged += (_, _) => { _workspaceMode = (mode.SelectedItem as ComboBoxItem)?.Tag as string ?? "convert"; };

        var sourceButtons = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        sourceButtons.Children.Add(Button(() => BemText.Get("选择源文件…"), () => SelectWorkspaceSource(false)));
        sourceButtons.Children.Add(Button(() => BemText.Get("选择源目录…"), () => SelectWorkspaceSource(true)));
        var workspaceButtons = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        workspaceButtons.Children.Add(Button(() => BemText.Get("选择工作区目录…"), SelectWorkspaceDirectory));
        var recipeButtons = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        recipeButtons.Children.Add(Button(() => BemText.Get("选择转换配方…"), SelectWorkspaceRecipe));

        _steps.Children.Add(Card(() => BemText.Get("输入与工作区"), mode,
            ProjectField(() => BemText.Get("源文件或目录"), _workspaceSource, value => _workspaceSource = value), sourceButtons,
            ProjectField(() => BemText.Get("工作区目录"), _workspaceDirectory, value => _workspaceDirectory = value), workspaceButtons,
            ProjectField(() => BemText.Get("转换配方（可留空）"), _workspaceRecipe, value => _workspaceRecipe = value), recipeButtons,
            Button(() => BemText.Get("创建工作区"), CreateWorkspace, true)));
    }

    private async Task SelectWorkspaceSource(bool folder)
    {
        string? file = folder ? await FolderPath() : await OpenPath(".zip", ".rar", ".7z", ".json", ".bem");
        if (file == null) return;
        _workspaceSource = file;
        if (_workspaceMode == "pack") _workspaceRecipe = "";
        RenderWorkspace();
    }

    private async Task SelectWorkspaceDirectory()
    {
        string? folder = await FolderPath();
        if (folder == null) return;
        _workspaceDirectory = folder;
        RenderWorkspace();
    }

    private async Task SelectWorkspaceRecipe()
    {
        string? file = await OpenPath(".json");
        if (file == null) return;
        _workspaceRecipe = file;
        RenderWorkspace();
    }

    private async Task CreateWorkspace()
    {
        if (string.IsNullOrWhiteSpace(_workspaceSource) || string.IsNullOrWhiteSpace(_workspaceDirectory))
        {
            Status(() => BemText.Get("缺少工作区参数"), () => BemText.Get("请选择源文件和工作区目录。"), InfoBarSeverity.Warning);
            return;
        }
        await Run(() => BemText.Get("正在创建工作区"), async token =>
        {
            List<string> args = ["workspace", "init", _workspaceDirectory, "--source", _workspaceSource, "--mode", _workspaceMode];
            if (!string.IsNullOrWhiteSpace(_workspaceRecipe)) args.AddRange(["--recipe", _workspaceRecipe]);
            string raw = await BemToolService.RunAsync(_installRoot, args, token);
            Report(raw);
            string output = Path.Combine(_workspaceDirectory, "export.bemproj.json");
            Completed(output, () => BemText.Get("工作区已创建，可以修改工程文件后执行导出。"));
        });
    }
    private async Task EditDeformations()
    {
        string? existing = string.IsNullOrWhiteSpace(_project.Deformations) ? null :
            _projectFile == null ? Path.GetFullPath(_project.Deformations) : BemExportProject.Resolve(_project.Deformations, _projectFile);
        var editor = new TextBox { AcceptsReturn = true, TextWrapping = TextWrapping.NoWrap,
            MinWidth = 600, MinHeight = 350, MaxHeight = 500, IsSpellCheckEnabled = false,
            Text = existing != null && File.Exists(existing) ? await File.ReadAllTextAsync(existing) :
                """
                {
                  "schema": 1,
                  "kind": "bem-position-morphs",
                  "parameters": [{"id":"body","name":"体型","min":0,"max":1000,"neutral":0,"default":0,"step":1}],
                  "mesh_deformations": [{"mesh":0,"parameter":"body","frames":[{"value":0,"neutral":true},{"value":1000,"target_positions":"target-positions.json"}]}]
                }
                """ };
        var content = new StackPanel { Spacing = 12 };
        content.Children.Add(Text(() => BemText.Get("滑条使用 0–1000 整数刻度。每个网格提供覆盖范围端点和 neutral 的帧；目标位置必须沿用导出顶点顺序。详细格式和可运行示例见 BEM 1.3 创作者文档。")));
        content.Children.Add(editor);
        var dialog = new ContentDialog { Title = BemText.Get("BEM 1.3 形态配置"), Content = content,
            PrimaryButtonText = BemText.Get("保存配置…"), CloseButtonText = BemText.Get("取消"), DefaultButton = ContentDialogButton.Primary,
            XamlRoot = ((FrameworkElement)Content).XamlRoot };
        if (await dialog.ShowAsync() != ContentDialogResult.Primary) return;
        using var parsed = JsonDocument.Parse(editor.Text);
        if (parsed.RootElement.GetProperty("kind").GetString() != "bem-position-morphs" ||
            parsed.RootElement.GetProperty("schema").GetInt32() != 1)
            throw new InvalidDataException(BemText.Get("请选择 BEM 位置形态配置。完整结构会在导出时校验。"));
        string? file = existing ?? await SavePath(".json", BemText.Get("BEM 形态配置"), "body-morphs");
        if (file == null) return;
        var comparer = StringComparer.OrdinalIgnoreCase;
        var pathBase = _projectFile ?? file;
        if (new[] { _project.Source, _project.Recipe, _project.Output, _project.Report }
                .Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => BemExportProject.Resolve(p, pathBase))
                .Any(p => comparer.Equals(p, Path.GetFullPath(file))) || comparer.Equals(_projectFile, Path.GetFullPath(file)))
            throw new InvalidDataException(BemText.Get("形态配置不能覆盖工程、源文件、配方、输出或报告。"));
        await File.WriteAllTextAsync(file, editor.Text, new UTF8Encoding(false));
        _project.Deformations = _projectFile == null ? file : BemExportProject.PortablePath(file, _projectFile);
        RenderExportProject(); Status(() => BemText.Get("形态配置已保存"), () => BemText.Get("参数与形态绑定将在导出时校验。目标和增量路径相对此配置文件。"));
    }
    private async Task OpenExportProject()
    {
        string? file = await OpenPath(".json"); if (file == null) return;
        OpenProject(file);
    }
    public void OpenProject(string file)
    {
        file = Path.GetFullPath(file);
        var project = BemExportProject.Load(file);
        _task.SelectedIndex = 0;
        _project = project; _projectFile = file; RenderExportProject();
        Status(() => BemText.Get("工程已打开"), () => BemText.Get("修改参数后点击“保存参数并导出 BEM”。"));
    }
    private async Task SelectExportInput(bool folder)
    {
        string? file = folder ? await FolderPath() : await OpenPath(_project.Mode == "pack" ? [".json"] : [".zip", ".rar", ".7z"]);
        if (file == null) return;
        if (_project.Mode == "pack") _project.ReadPackMetadata(file);
        _project.Source = _projectFile == null ? file : BemExportProject.PortablePath(file, _projectFile);
        RenderExportProject();
    }
    private async Task SaveExportProject(bool chooseLocation)
    {
        string? file = _projectFile;
        if (file == null || chooseLocation) file = await SavePath(".json", BemText.Get("BEM 导出工程"), file == null ? "character.bemproj" : Path.GetFileNameWithoutExtension(file));
        if (file == null) return;
        _project.Save(file, _projectFile); _projectFile = file; RenderExportProject();
        Status(() => BemText.Get("工程已保存"), () => file);
    }
    private async Task BuildExportProject()
    {
        await SaveExportProject(false);
        if (_projectFile == null) return;
        string projectFile = _projectFile;
        await Run(() => BemText.Get("正在按工程参数导出"), async token =>
        {
            string raw = await BemToolService.RunAsync(_installRoot, ["build", projectFile], token); Report(raw);
            string output = BemExportProject.Resolve(_project.Output, projectFile);
            Completed(output, () => BemReportPresentation.Package(raw) + BemText.Get("\n工程已保留，可修改参数后再次导出。导出校验通过，游戏显示效果仍需实机确认。"));
        });
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
    private async Task Run(Func<string> label, Func<CancellationToken, Task> action)
    {
        if (_operation != null) return;
        _operation = new(); _task.IsEnabled = false; SetStepButtons(false); _saveReport.IsEnabled = false;
        _progress.IsActive = true; _progress.Visibility = _cancel.Visibility = Visibility.Visible;
        Status(label, () => BemText.Get("正在处理，可以取消。"));
        try { await action(_operation.Token); }
        catch (OperationCanceledException) { Status(() => BemText.Get("已取消"), () => BemText.Get("未完成输出。可以重新选择输入或重试。")); }
        catch (Exception ex) { Report(ex.Message); Status(() => BemText.Get("未完成"), () => BemReportPresentation.Failure(ex.Message), InfoBarSeverity.Error); }
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
        _steps.Children.Add(Card(() => BemText.Get("已选择"), Text(path), Button(() => BemText.Get("重新检查"), InspectSource)));
        await InspectSource();
    }
    private async Task InspectSource()
    {
        ClearPrepared(); while (_steps.Children.Count > 2) _steps.Children.RemoveAt(2);
        _result.Children.Clear(); _result.Visibility = Visibility.Collapsed;
        await Run(() => BemText.Get("正在检查源 Mod"), async token =>
        {
            string raw = await BemToolService.RunAsync(_installRoot, ["inspect", _source], token); Report(raw);
            var summary = BemInspectionSummary.Read(raw);
            var elements = new List<UIElement> { Text(() => BemInspectionSummary.Read(raw).Detail), Text(() => BemInspectionSummary.Read(raw).NextStep) };
            if (summary.CanAutoConvert)
                elements.Add(Button(() => BemText.Get("转换为 BEM"), () => PrepareConversion(null), true));
            if (summary.CanProvideRecipe)
            {
                var advancedBody = new StackPanel { Spacing = 12 };
                advancedBody.Children.Add(Text(() => BemText.Get("仅供维护转换器的开发者使用。源 Mod 转换规则描述入口、静态外观与材质/骨骼映射。当前离线规则需包含已核实的依赖，工具会先实际转换并校验。这不是普通用户选择角色资料的步骤。")));
                advancedBody.Children.Add(Button(() => BemText.Get("加载源 Mod 转换规则并验证…"), PrepareRecipe));
                var advanced = new Expander { Content = advancedBody, HorizontalAlignment = HorizontalAlignment.Stretch };
                BemLocalizedUI.Set(advanced, Expander.HeaderProperty, () => BemText.Get("开发者：验证源 Mod 转换规则（可跳过）"));
                elements.Add(advanced);
            }
            _steps.Children.Add(Card(() => "2  " + BemInspectionSummary.Read(raw).Title, elements.ToArray()));
            Status(() => summary.AlreadyPackaged ? BemText.Get("无需转换") : BemText.Get("检查完成"), () => BemInspectionSummary.Read(raw).NextStep,
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
        await Run(() => BemText.Get("正在适配并校验模型"), async token =>
        {
            List<string> args = ["convert", _source, "-o", temp];
            if (recipe != null) args.AddRange(["--recipe", recipe]);
            string raw = await BemToolService.RunAsync(_installRoot, args, token);
            token.ThrowIfCancellationRequested(); Report(raw); _prepared = temp;
            _steps.Children.Add(Card(() => BemText.Get("3  已准备好，可以导出"), Text(BemReportPresentation.Package(raw)), Text(() => BemText.Get("适配与结构校验通过；游戏中的显示效果仍需实机确认。")),
                Button(() => BemText.Get("导出 BEM…"), ExportPrepared, true)));
            Status(() => BemText.Get("适配成功"), () => BemText.Get("点击“导出 BEM”选择保存位置，然后在角色外观管理中导入。"), InfoBarSeverity.Success);
        });
    }
    private async Task ExportPrepared()
    {
        if (_prepared.Length == 0 || !File.Exists(_prepared)) return;
        string? output = await SavePath(".bem", BemText.Get("BEM 模型包"), Path.GetFileNameWithoutExtension(_source)); if (output == null) return;
        await Run(() => BemText.Get("正在保存 BEM"), async token =>
        {
            string temp = Path.Combine(Path.GetDirectoryName(output)!, ".bem-export-" + Guid.NewGuid());
            try
            {
                await using (var input = File.OpenRead(_prepared))
                await using (var target = File.Create(temp)) await input.CopyToAsync(target, token);
                token.ThrowIfCancellationRequested(); File.Move(temp, output, true);
            }
            finally { if (File.Exists(temp)) File.Delete(temp); }
            Completed(output, () => BemText.Get("回到“角色外观”导入该 BEM，选择外观并启用。下次启动游戏生效。"));
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
        _steps.Children.Add(Card(() => BemText.Format("2  已选择 {0} 个文件", files.Count), Text(string.Join("\n", files)),
            Button(() => Mode == "unpack" ? BemText.Get("选择解包位置并开始…") : Mode == "pack" ? BemText.Get("校验并保存 BEM…") : BemText.Get("校验并保存 ZIP…"), () => ProcessProject(files), true)));
    }
    private async Task ProcessProject(List<string> files)
    {
        string mode = Mode; string? output;
        if (mode == "unpack")
        {
            string? folder = await FolderPath(); if (folder == null) return;
            output = Path.Combine(folder, Path.GetFileNameWithoutExtension(files[0]) + "-project");
            if (Directory.Exists(output)) { Status(() => BemText.Get("目标目录已存在"), () => BemText.Get("请选择其他位置，避免混入旧项目：\n") + output, InfoBarSeverity.Warning); return; }
        }
        else output = await SavePath(mode == "bundle" ? ".zip" : ".bem", mode == "bundle" ? BemText.Get("BEM 合集") : BemText.Get("BEM 模型包"), mode == "bundle" ? "mod-collection" : "appearance");
        if (output == null) return;
        await Run(() => BemText.Get("正在校验并处理"), async token =>
        {
            List<string> args = [mode, .. files, "-o", output];
            string raw = await BemToolService.RunAsync(_installRoot, args, token); Report(raw);
            Completed(output, () => BemReportPresentation.Package(raw) + "\n" + (mode == "unpack" ? BemText.Get("BEM 项目可编辑 project.json 后重新打包；ZIP 解出的 BEM 可在管理页导入。") : mode == "bundle" ? BemText.Get("这个 ZIP 可以上传分发，也可以在角色外观管理中直接导入。") : BemText.Get("BEM 已生成，可返回角色外观管理导入。")));
        });
    }
    private void Completed(string path, Func<string> next)
    {
        _exported = path; _result.Children.Clear(); _result.Visibility = Visibility.Visible;
        _result.Children.Add(Card(() => BemText.Get("已完成"), Text(path), Text(next), Button(() => BemText.Get("打开输出位置"), () =>
        {
            string folder = Directory.Exists(_exported) ? _exported : Path.GetDirectoryName(_exported)!;
            Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true }); return Task.CompletedTask;
        })));
        Status(() => BemText.Get("处理完成"), () => BemText.Get("输出位置和下一步见下方。"), InfoBarSeverity.Success);
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
            XamlRoot = ((FrameworkElement)Content).XamlRoot, Title = BemText.Get("创作者帮助"), CloseButtonText = BemText.Get("返回任务"),
            Content = new ScrollViewer { MaxHeight = 520, Content = Text(guide), VerticalScrollBarVisibility = ScrollBarVisibility.Auto }
        }.ShowAsync();
    }
}
