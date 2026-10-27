using System.Diagnostics;
using System.Text.Json.Nodes;
using BetterEndfieldNext.UI.Controls;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfieldNext.UI.Views;

internal sealed class BemComposeWindow : Window
{
    private readonly BemPackageService _packages;
    private readonly BemComposeService _composer;
    private readonly string _installRoot;
    private readonly Action _refresh;
    private readonly StackPanel _settings = new() { Spacing = 14 };
    private readonly StackPanel _sources = new() { Spacing = 8 };
    private readonly StackPanel _slots = new() { Spacing = 10 };
    private readonly ContentControl _settingsHost = new(), _slotsHost = new();
    private readonly ComboBox _target = new() { HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly TextBox _name = new(), _author = new(), _output = new();
    private readonly TextBlock _version = new() { TextWrapping = TextWrapping.Wrap };
    private readonly ComboBox _action = new() { HorizontalAlignment = HorizontalAlignment.Stretch };
    private readonly Button _planButton = new(), _buildButton = new() { IsEnabled = false }, _cancelButton = new() { IsEnabled = false };
    private readonly Button _previewButton = new() { IsEnabled = false };
    private readonly JsonObject _previewDefaults = new();
    private int _previewSession;
    private bool _applyingPreview;
    private readonly ContentControl _previewHost = new() { HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
    private readonly ComboBox _previewOutput = new() { HorizontalAlignment = HorizontalAlignment.Stretch, Visibility = Visibility.Collapsed };
    private readonly List<(string File, string Prefix)> _previewFiles = [];
    private readonly Microsoft.UI.Dispatching.DispatcherQueueTimer _previewTimer;
    private BemPreviewView? _compositionPreview;
    private JsonObject? _previewReport;
    private string? _previewAtomicSignature;
    private bool _previewDirty, _settingOutputs;
    private readonly ProgressRing _progress = new() { Width = 22, Height = 22, IsActive = false };
    private readonly InfoBar _status = new() { IsClosable = false, IsOpen = false };
    private readonly TextBlock _budget = new() { TextWrapping = TextWrapping.Wrap };
    private readonly StackPanel _results = new() { Spacing = 8 };
    private readonly List<CheckBox> _sourceChecks = [];
    private readonly Dictionary<string, ComboBox> _choices = new(StringComparer.Ordinal);
    private readonly List<Action> _segmentSummaries = [];
    private readonly HashSet<string> _highlightSegments = new(StringComparer.Ordinal);
    private string? _highlightParent;
    private readonly Dictionary<string, Expander> _segmentExpanders = new(StringComparer.Ordinal);
    private JsonObject? _plan;
    private string? _bodyEditParent;
    private string _bodyEditRegion = "torso";
    private readonly Dictionary<string, Dictionary<int, string>> _bodyTriangleRegions = new(StringComparer.Ordinal);
    private readonly Stack<JsonObject> _bodyUndo = new();
    private static readonly string[] BodyRegions = ["neck", "head", "torso", "pelvis", "left_arm", "right_arm", "left_leg", "right_leg", "unclassified"];
    private static string BodyRegionName(string region) => BemText.Get(region switch
    {
        "neck" => "颈部", "head" => "头部", "torso" => "躯干", "pelvis" => "骨盆",
        "left_arm" => "左臂", "right_arm" => "右臂", "left_leg" => "左腿", "right_leg" => "右腿", _ => "未分类区域"
    });
    private CancellationTokenSource? _operation;
    private bool _preparingPreview;
    private TaskCompletionSource? _operationFinished;
    private bool _closed;

    public BemComposeWindow(BemPackageService packages, string installRoot, Action refresh, Window? owner)
    {
        _packages = packages; _installRoot = installRoot; _refresh = refresh;
        _composer = new BemComposeService(installRoot);
        Title = BemText.Get("组合已安装包");
        WindowPlacementService.SetInitialSize(this, 1540, 900, owner);
        _previewTimer = DispatcherQueue.CreateTimer(); _previewTimer.Interval = TimeSpan.FromMilliseconds(400);
        _previewTimer.IsRepeating = false;
        _previewTimer.Tick += async (_, _) => { if (_operation is null && _plan is not null && _previewDirty) await PreviewAsync(); };
        var page = new PageContentPanel { Padding = new Thickness(28), MaxContentWidth = 840 };
        var body = new StackPanel { Spacing = 16 }; page.Children.Add(body);
        body.Children.Add(new TextBlock { Text = Title, FontSize = 28 });
        body.Children.Add(new TextBlock { Text = BemText.Get("普通模型按部件组合，world/UI 保持共用；大招和武器整体可选，默认不加入。"), TextWrapping = TextWrapping.Wrap });
        _target.Header = BemText.Get("角色或武器");
        foreach (string key in packages.Packages.Where(Supported).Select(p => p.TargetKey).Distinct().OrderBy(x => x)) _target.Items.Add(key);
        _target.SelectionChanged += (_, _) => RenderSources();
        _settings.Children.Add(_target); _settings.Children.Add(_sources);
        _name.Header = BemText.Get("组合包名称"); _name.Text = BemText.Get("组合外观");
        _author.Header = BemText.Get("作者"); _author.Text = "BEM Composer";
        _version.Text = BemText.Get("输出版本由来源和所需功能自动确定。"); _output.Header = BemText.Get("输出目录");
        _output.Text = Path.Combine(packages.PackageDirectory, "Compositions");
        foreach (var box in new[] { _name, _author, _output }) { box.TextChanged += (_, _) => Invalidate(); _settings.Children.Add(box); }
        _settings.Children.Add(_version);
        _action.Header = BemText.Get("生成后操作");
        foreach (string label in new[] { "仅生成", "生成并导入", "生成并启用" }) _action.Items.Add(BemText.Get(label));
        _action.SelectedIndex = 0; _settings.Children.Add(_action);
        _settingsHost.Content = _settings; _slotsHost.Content = _slots;
        _settingsHost.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        _slotsHost.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        body.Children.Add(_settingsHost); body.Children.Add(_budget); body.Children.Add(_slotsHost);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        _planButton.Content = BemText.Get("分析来源"); _buildButton.Content = BemText.Get("生成组合包"); _cancelButton.Content = BemText.Get("取消处理");
        _planButton.Click += async (_, _) => await PlanAsync();
        _buildButton.Click += async (_, _) => await BuildAsync();
        _previewButton.Content = BemText.Get("刷新预览");
        _previewButton.Click += async (_, _) => await PreviewAsync();
        _cancelButton.Click += (_, _) => _operation?.Cancel();
        foreach (var element in new UIElement[] { _planButton, _previewButton, _buildButton, _cancelButton, _progress }) actions.Children.Add(element);
        body.Children.Add(actions); body.Children.Add(_status); body.Children.Add(_results);
        var layout = new Grid();
        layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(520), MinWidth = 380 });
        layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 500 });
        var editor = new ScrollViewer { Content = page, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        layout.Children.Add(editor);
        var preview = new Grid { Margin = new Thickness(0, 20, 20, 20), RowSpacing = 10 };
        preview.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        preview.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        _previewOutput.Header = BemText.Get("预览输出");
        _previewOutput.SelectionChanged += async (_, _) =>
        {
            if (!_settingOutputs && _previewOutput.SelectedIndex >= 0 && !_closed)
            {
                _previewOutput.IsEnabled = false; _previewHost.IsEnabled = false;
                try { await ShowPreviewOutputAsync(_previewSession, _plan!); }
                catch (Exception ex) { Status(ex.Message, InfoBarSeverity.Error); }
                finally { if (!_closed) { _previewOutput.IsEnabled = true; _previewHost.IsEnabled = true; } }
            }
        };
        preview.Children.Add(_previewOutput); preview.Children.Add(_previewHost); Grid.SetRow(_previewHost, 1);
        Grid.SetColumn(preview, 1); layout.Children.Add(preview); Content = layout;
        ShowPreviewPlaceholder();
        Closed += (_, _) => { _closed = true; _previewTimer.Stop(); _compositionPreview?.Dispose(); if (_operation != null) _operation.Cancel(); else _composer.Dispose(); };
        if (_target.Items.Count > 0) _target.SelectedIndex = 0;
    }

    private static bool Supported(BemPackage package) => package.SupportsCurrentPlatform && package.FormatMinor <= 5 && package.Parameters.Count == 0 && package.Drivers.Count == 0;

    private void Invalidate()
    {
        _previewSession++;
        if (_preparingPreview) _operation?.Cancel();
        _previewDefaults.Clear(); _previewButton.IsEnabled = false;
        _previewTimer.Stop(); _previewDirty = false; _previewFiles.Clear();
        _previewReport = null; _previewAtomicSignature = null;
        _compositionPreview?.Dispose(); _compositionPreview = null; _previewOutput.Visibility = Visibility.Collapsed;
        ShowPreviewPlaceholder();
        _bodyEditParent = null; _bodyUndo.Clear(); _bodyTriangleRegions.Clear(); _plan = null; _buildButton.IsEnabled = false; _slots.Children.Clear(); _choices.Clear(); _segmentSummaries.Clear(); _highlightSegments.Clear(); _highlightParent = null; _segmentExpanders.Clear(); _budget.Text = "";
        _version.Text = BemText.Get("输出版本由来源和所需功能自动确定。");
    }

    private void RenderSources()
    {
        Invalidate(); _sources.Children.Clear(); _sourceChecks.Clear();
        string target = _target.SelectedItem as string ?? "";
        foreach (var package in _packages.Packages.Where(p => Supported(p) &&
            (p.TargetKey == target || target.StartsWith("character:", StringComparison.Ordinal) && p.TargetKind == "weapon")))
        {
            string label = package.Name + " · " + package.Version + " · BEM 1." + package.FormatMinor;
            if (package.TargetKind == "weapon") label += " · " + BemText.Get("武器（整体，可选）");
            var check = new CheckBox { Content = label, Tag = package };
            check.Checked += (_, _) => Invalidate(); check.Unchecked += (_, _) => Invalidate();
            _sourceChecks.Add(check); _sources.Children.Add(check);
        }
    }

    private void ShowPreviewPlaceholder() => _previewHost.Content = new TextBlock
    {
        Text = BemText.Get("分析来源后，组合效果将在此处自动预览。"),
        HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
        TextWrapping = TextWrapping.Wrap, Margin = new Thickness(20)
    };

    private void QueuePreview()
    {
        ++_previewSession;
        _compositionPreview?.CancelDraftLoad();
        if (_preparingPreview) _operation?.Cancel();
        _previewDirty = true;
        _previewHost.Opacity = 0.55;
        if (_plan is null || _closed || _operation is not null) return;
        _previewTimer.Stop(); _previewTimer.Start();
    }

    private void Status(string text, InfoBarSeverity severity = InfoBarSeverity.Informational)
    {
        if (_closed) return;
        _status.Message = text; _status.Severity = severity; _status.IsOpen = true;
    }

    private void UpdateOutputVersion()
    {
        if (_plan == null) return;
        var formats = new List<int>();
        void AddFormat(JsonNode child, string prefix)
        {
            bool included = child["slots"]!.AsArray().Count > 0;
            foreach (var atom in child["atomic_items"]!.AsArray())
            {
                string id = prefix + atom!["id"]!.GetValue<string>();
                string choice = _choices.TryGetValue(id, out var box) && box.SelectedItem is ComboBoxItem item
                    ? (string)item.Tag : atom["default"]!.GetValue<string>();
                included |= choice != "original";
            }
            if (included) formats.Add(child["bem_minor"]!.GetValue<int>());
        }
        if (_plan["projects"] is JsonArray projects)
            foreach (var part in projects) AddFormat(part!["plan"]!, part["id"]!.GetValue<string>() + ".");
        else AddFormat(_plan, "");
        _version.Text = formats.Count == 0 ? BemText.Get("请选择至少一个大招或武器整体项。")
            : BemText.Format("包版本：{0} · 输出格式：{1}（自动）", _plan["package_version"]!.GetValue<string>(),
                string.Join(" / ", formats.Distinct().Select(x => "BEM 1." + x)));
        _buildButton.IsEnabled = (_operation == null || _preparingPreview) && formats.Count > 0;
        _previewButton.IsEnabled = _buildButton.IsEnabled;
    }

    private async Task ExecuteAsync(Func<CancellationToken, Task> work, bool preview = false)
    {
        if (_operation is not null)
        {
            if (preview || !_preparingPreview) return;
            // A real operation takes precedence over speculative preview work.
            _operation.Cancel();
            if (_operationFinished is not null) await _operationFinished.Task;
            if (_closed || _operation is not null) return;
        }
        var finished = new TaskCompletionSource();
        _operationFinished = finished; _preparingPreview = preview;
        _operation = new CancellationTokenSource();
        _settings.IsHitTestVisible = preview; _slots.IsHitTestVisible = preview;
        _settingsHost.IsEnabled = preview; _slotsHost.IsEnabled = preview;
        _planButton.IsEnabled = preview; _previewButton.IsEnabled = false; _buildButton.IsEnabled = preview && _plan is not null; _cancelButton.IsEnabled = true; _progress.IsActive = true;
        _previewHost.IsEnabled = preview; _previewOutput.IsEnabled = false;
        try { await work(_operation.Token); }
        catch (OperationCanceledException) { if (!preview) Status(BemText.Get("组合已取消。")); }
        catch (Exception ex)
        {
            string message = BemComposeService.ExplainFailure(ex.Message);
            if (preview)
            {
                _previewHost.Opacity = 1;
                if (_compositionPreview is not null) await _compositionPreview.ShowDraftErrorAsync(message);
                message = BemText.Get("预览更新失败，当前保留上次成功的画面。") + "\n" + message;
            }
            Status(message, InfoBarSeverity.Error);
        }
        finally
        {
            _operation.Dispose(); _operation = null; _preparingPreview = false;
            if (_closed) _composer.Dispose();
            else
            {
                _settings.IsHitTestVisible = true; _slots.IsHitTestVisible = true;
                _settingsHost.IsEnabled = true; _slotsHost.IsEnabled = true;
                _planButton.IsEnabled = true; _buildButton.IsEnabled = _plan != null;
                _cancelButton.IsEnabled = false; _progress.IsActive = false;
                UpdateOutputVersion();
                _previewHost.IsEnabled = true; _previewOutput.IsEnabled = true;
                if (_previewDirty && _plan is not null) QueuePreview();
            }
            finished.TrySetResult();
        }
    }

    private Task PlanAsync() => ExecuteAsync(async token =>
    {
        Invalidate(); _results.Children.Clear();
        var selected = _sourceChecks.Where(x => x.IsChecked == true).Select(x => (BemPackage)x.Tag).ToList();
        if (selected.Count < 2) throw new InvalidOperationException(BemText.Get("请选择至少两个来源包。"));
        var sources = new JsonArray();
        foreach (var package in selected) sources.Add(new JsonObject
        {
            ["path"] = package.File, ["selection"] = package.IsComposable ? package.EncodedOptions() : package.SelectedAppearance,
            ["parameters"] = package.EncodedParameters()
        });
        var request = new JsonObject
        {
            ["schema"] = 1, ["platform"] = "windows-x64", ["project_id"] = "compose." + Guid.NewGuid().ToString("N"),
            ["name"] = _name.Text.Trim(), ["author"] = _author.Text.Trim(), ["sources"] = sources
        };
        Status(BemText.Get("正在分析来源…"));
        var plan = await _composer.PlanAsync(request, token);
        if (_closed) return;
        _plan = plan;
        foreach (var group in plan["slots"]!.AsArray().Concat(plan["atomic_items"]!.AsArray()))
        {
            string id = group!["id"]!.GetValue<string>();
            string label = group["display_name"]!.GetValue<string>();
            string resource = group["resource_name"]?.GetValue<string>() ?? "";
            if (resource.Length > 0) label = resource + " / " + label;
            if (label == "Ability / other resources") label = BemText.Get("大招与其他资源（整体）");
            if (label == "Weapon") label = BemText.Get("武器");
            if (group["resource_names"] is JsonArray roots)
                label += " · " + string.Join(" / ", roots.Select(x => x!.GetValue<string>()));
            var combo = new ComboBox { Header = label, HorizontalAlignment = HorizontalAlignment.Stretch };
            foreach (var candidate in group["candidates"]!.AsArray())
            {
                string choiceId = candidate!["id"]!.GetValue<string>();
                string operation = candidate["operation"]!.GetValue<string>();
                string state = BemText.Get(operation switch { "replace" => "替换", "hide" => "隐藏", "keep" => "保留", "source" => "保留源包选项", _ => "整体" });
                string originalLabel = group["optional"]?.GetValue<bool>() == true ? BemText.Get("不加入组合") : BemText.Get("保留原版");
                var item = new ComboBoxItem { Content = choiceId == "split" ? BemText.Get("按源包选项组合") : choiceId == "original" ? originalLabel : candidate["name"]!.GetValue<string>() + " · " + state, Tag = choiceId };
                combo.Items.Add(item);
                if (choiceId == group["default"]!.GetValue<string>()) combo.SelectedItem = item;
            }
            _choices.Add(id, combo); _slots.Children.Add(combo);
            var segments = group["segments"] as JsonArray;
            if (segments is { Count: > 0 })
            {
                if (segments.Any(segment => segment!["body_region"] is not null)) AddBodyRegionControls(id, segments, combo);
                AddSegmentControls(id, segments, combo);
            }
            string previousChoice = (string)((ComboBoxItem)combo.SelectedItem).Tag;
            combo.SelectionChanged += (_, _) =>
            {
                string choice = (string)((ComboBoxItem)combo.SelectedItem).Tag;
                if (!_applyingPreview && choice == "split" && segments is not null)
                {
                    _applyingPreview = true;
                    try
                    {
                        foreach (var segment in segments)
                        {
                            string segmentId = segment!["id"]!.GetValue<string>();
                            if (_previewDefaults[segmentId] is null)
                                _previewDefaults[segmentId] = segment["source_id"]!.GetValue<string>() == previousChoice ? "on" : "off";
                        }
                    }
                    finally { _applyingPreview = false; }
                }
                previousChoice = choice;
                foreach (var update in _segmentSummaries) update();
                if (choice == "split" && _segmentExpanders.TryGetValue(id, out var expander)) expander.IsExpanded = true;
                if (!_applyingPreview) { _previewSession++; QueuePreview(); }
                UpdateOutputVersion();
            };
        }
        ShowSourceOptions(plan, selected);
        QueuePreview();
        UpdateOutputVersion();
        _budget.Text = plan["projects"] is JsonArray parts
            ? BemText.Format("最多 {0} 个输出包 · 每包组件上限 64 · 未选整体项不输出", parts.Count)
            : BemText.Format("组件 {0}/64 · 选项组 {1} · BEM 1.{2}", plan["estimated_components"]!.GetValue<int>(), plan["estimated_groups"]!.GetValue<int>(), plan["bem_minor"]!.GetValue<int>());
        Status(BemText.Get("分析完成。请选择默认部件来源；源包选项及其联动将在生成后保留。"));
    });

    private bool SegmentEnabled(string parent, JsonNode segment)
    {
        string choice = (string)((ComboBoxItem)_choices[parent].SelectedItem).Tag;
        return choice == "split"
            ? (_previewDefaults[segment["id"]!.GetValue<string>()]?.GetValue<string>() ?? segment["default"]!.GetValue<string>()) == "on"
            : choice == segment["source_id"]!.GetValue<string>();
    }

    private int BodyTriangleCount(JsonNode segment)
    {
        if (segment["body_region"] is null) return segment["count"]!.GetValue<int>() / 3;
        string draw = segment["body_draw"]!.GetValue<string>(), region = segment["body_region"]!.GetValue<string>();
        int count = segment["triangles"]!.AsArray().Count;
        if (_previewDefaults["body_edits"]?[draw] is JsonObject edits && _bodyTriangleRegions.TryGetValue(draw, out var original))
            foreach (var edit in edits)
                if (int.TryParse(edit.Key, out int triangle) && original.TryGetValue(triangle, out string? before))
                { if (before == region) count--; if (edit.Value!.GetValue<string>() == region) count++; }
        return count;
    }

    private void AddBodyRegionControls(string parent, JsonArray segments, ComboBox source)
    {
        foreach (var segment in segments.Where(segment => segment!["body_region"] is not null))
        {
            string draw = segment!["body_draw"]!.GetValue<string>(), region = segment["body_region"]!.GetValue<string>();
            if (!_bodyTriangleRegions.TryGetValue(draw, out var map)) _bodyTriangleRegions[draw] = map = new();
            foreach (var triangle in segment["triangles"]!.AsArray()) map[triangle!.GetValue<int>()] = region;
        }
        var panel = new StackPanel { Spacing = 8 };
        panel.Children.Add(new TextBlock { Text = BemText.Get("身体区域为骨骼权重粗分区。各区域可独立选择来源；缺少区域的包标为未提供，接缝需要预览检查。"), TextWrapping = TextWrapping.Wrap });
        bool updating = false;
        var selectors = new List<(string Region, ComboBox Box)>();
        foreach (string region in BodyRegions)
        {
            var box = new ComboBox { Header = BodyRegionName(region), HorizontalAlignment = HorizontalAlignment.Stretch };
            box.Items.Add(new ComboBoxItem { Content = BemText.Get("不加入此区域"), Tag = "off" });
            box.Items.Add(new ComboBoxItem { Content = BemText.Get("未提供"), Tag = "missing", IsEnabled = false });
            box.Items.Add(new ComboBoxItem { Content = BemText.Get("自定义叠加或部分启用"), Tag = "mixed", IsEnabled = false });
            foreach (var group in segments.GroupBy(segment => segment!["source_id"]!.GetValue<string>()))
                box.Items.Add(new ComboBoxItem { Content = group.First()!["source_name"]!.GetValue<string>(), Tag = group.Key });
            box.SelectionChanged += (_, _) =>
            {
                if (updating || box.SelectedItem is not ComboBoxItem selected || ((string)selected.Tag == "mixed" || (string)selected.Tag == "missing")) return;
                string chosen = (string)selected.Tag;
                var actual = segments.ToDictionary(segment => segment!["id"]!.GetValue<string>(), segment => SegmentEnabled(parent, segment!));
                foreach (var segment in segments.Where(segment => segment!["body_region"]?.GetValue<string>() == region))
                    actual[segment!["id"]!.GetValue<string>()] = segment["source_id"]!.GetValue<string>() == chosen;
                var layouts = segments.Where(segment => actual[segment!["id"]!.GetValue<string>()] && BodyTriangleCount(segment) > 0)
                    .Select(segment => segment!["layout"]?.ToJsonString()).Distinct().ToList();
                if (layouts.Count > 1) { Status(BemText.Get("这些绘制段的顶点布局不兼容，不能合并。请先停用冲突来源，再启用新来源。"), InfoBarSeverity.Warning); Update(); return; }
                _applyingPreview = true;
                try { foreach (var pair in actual) _previewDefaults[pair.Key] = pair.Value ? "on" : "off"; source.SelectedItem = source.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == "split"); }
                finally { _applyingPreview = false; }
                _highlightSegments.Clear(); _highlightParent = null;
                foreach (var refresh in _segmentSummaries) refresh(); UpdateOutputVersion(); QueuePreview();
            };
            selectors.Add((region, box));
            var row = new Grid { ColumnSpacing = 8 }; row.ColumnDefinitions.Add(new ColumnDefinition()); row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            row.Children.Add(box);
            var highlight = new Button { Content = BemText.Get("高亮区域"), VerticalAlignment = VerticalAlignment.Bottom };
            highlight.Click += (_, _) =>
            {
                _bodyEditParent = null; _highlightParent = parent; _highlightSegments.Clear();
                foreach (var segment in segments.Where(segment => segment!["body_region"]?.GetValue<string>() == region && BodyTriangleCount(segment) > 0 && SegmentEnabled(parent, segment)))
                    _highlightSegments.Add(segment!["id"]!.GetValue<string>());
                if (_highlightSegments.Count == 0) { _highlightParent = null; Status(BemText.Get("此区域尚未加入组合。")); return; }
                QueuePreview();
            };
            row.Children.Add(highlight); Grid.SetColumn(highlight, 1); panel.Children.Add(row);
        }
        void Update()
        {
            updating = true;
            try
            {
                foreach (var selector in selectors)
                {
                    var present = segments.Where(segment => segment!["body_region"]?.GetValue<string>() == selector.Region && BodyTriangleCount(segment) > 0).ToList();
                    var active = present.Where(segment => SegmentEnabled(parent, segment!)).ToList();
                    selector.Box.IsEnabled = present.Count > 0;
                    string value = present.Count == 0 ? "missing" : active.Count == 0 ? "off" : active.Select(segment => segment!["source_id"]!.GetValue<string>()).Distinct().Count() == 1
                        && present.Where(segment => segment!["source_id"]!.GetValue<string>() == active[0]!["source_id"]!.GetValue<string>()).All(segment => SegmentEnabled(parent, segment!))
                        ? active[0]!["source_id"]!.GetValue<string>() : "mixed";
                    foreach (ComboBoxItem item in selector.Box.Items)
                    {
                        string id = (string)item.Tag;
                        if (id != "off" && id != "mixed" && id != "missing")
                        {
                            var sourceSegments = segments.Where(segment => segment!["source_id"]!.GetValue<string>() == id).ToList();
                            item.IsEnabled = present.Any(segment => segment!["source_id"]!.GetValue<string>() == id);
                            int count = present.Where(segment => segment!["source_id"]!.GetValue<string>() == id).Sum(segment => BodyTriangleCount(segment!));
                            string name = sourceSegments[0]!["source_name"]!.GetValue<string>();
                            item.Content = name + (item.IsEnabled ? " · " + BemText.Format("{0} 个三角形", count) : " · " + BemText.Get("未提供"));
                            ToolTipService.SetToolTip(item, name + "\n" + BodyRegionName(selector.Region) + " · " + BemText.Format("{0} 个三角形", count));
                        }
                    }
                    selector.Box.SelectedItem = selector.Box.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == value);
                }
            }
            finally { updating = false; }
        }
        var target = new ComboBox { Header = BemText.Get("点选面的目标区域"), HorizontalAlignment = HorizontalAlignment.Stretch };
        foreach (string region in BodyRegions) target.Items.Add(new ComboBoxItem { Content = BodyRegionName(region), Tag = region });
        target.SelectedIndex = 2; target.SelectionChanged += (_, _) => _bodyEditRegion = (string)((ComboBoxItem)target.SelectedItem).Tag;
        panel.Children.Add(target);
        var edit = new Button { Content = BemText.Get("开始点面修正") };
        var undo = new Button { Content = BemText.Get("撤销上次修正") };
        var reset = new Button { Content = BemText.Get("恢复自动分区") };
        edit.Click += async (_, _) =>
        {
            _bodyEditParent = _bodyEditParent == parent ? null : parent; _bodyEditRegion = (string)((ComboBoxItem)target.SelectedItem).Tag;
            _highlightSegments.Clear(); _highlightParent = null;
            if (_bodyEditParent == parent)
            {
                foreach (var segment in segments) _previewDefaults[segment!["id"]!.GetValue<string>()] = SegmentEnabled(parent, segment) ? "on" : "off";
                _applyingPreview = true; try { source.SelectedItem = source.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == "split"); } finally { _applyingPreview = false; }
                foreach (var refresh in _segmentSummaries) refresh(); QueuePreview();
            }
            edit.Content = BemText.Get(_bodyEditParent == parent ? "结束点面修正" : "开始点面修正");
            if (_compositionPreview is not null) { await _compositionPreview.HighlightSegmentsAsync([], false); await SyncBodyEditAsync(); }
        };
        undo.Click += (_, _) =>
        {
            if (!_bodyUndo.TryPop(out var previous)) return;
            if (previous["edits"] is null) _previewDefaults.Remove("body_edits"); else _previewDefaults["body_edits"] = previous["edits"]!.DeepClone();
            foreach (var pair in previous["segments"]!.AsObject()) _previewDefaults[pair.Key] = pair.Value!.DeepClone();
            _applyingPreview = true;
            try { var box = _choices[previous["parent"]!.GetValue<string>()]; box.SelectedItem = box.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == previous["choice"]!.GetValue<string>()); }
            finally { _applyingPreview = false; }
            foreach (var refresh in _segmentSummaries) refresh(); QueuePreview();
        };
        reset.Click += (_, _) => { _previewDefaults.Remove("body_edits"); _bodyUndo.Clear(); foreach (var refresh in _segmentSummaries) refresh(); QueuePreview(); };
        var buttons = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 }; buttons.Children.Add(edit); buttons.Children.Add(undo); buttons.Children.Add(reset);
        panel.Children.Add(buttons);
        panel.Children.Add(new TextBlock { Text = BemText.Get("修正时只显示当前身体。按住 Ctrl 点击可见三角形，将它移到目标区域；普通拖动仍可旋转。修正会随组合保存。"), TextWrapping = TextWrapping.Wrap });
        _segmentSummaries.Add(() => edit.Content = BemText.Get(_bodyEditParent == parent ? "结束点面修正" : "开始点面修正"));
        _segmentSummaries.Add(Update); Update();
        _slots.Children.Add(new Expander { Header = BemText.Get("身体区域来源与边界修正"), Content = panel, IsExpanded = true, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch });
    }

    private Task SyncBodyEditAsync()
    {
        if (_compositionPreview is null) return Task.CompletedTask;
        var group = _plan?["slots"]?.AsArray().FirstOrDefault(group => group!["id"]!.GetValue<string>() == _bodyEditParent);
        string[] keys = group?["segments"]?.AsArray().Where(segment => segment!["body_draw"] is not null)
            .Select(segment => segment!["body_draw"]!.GetValue<string>()).Distinct().ToArray() ?? [];
        return _compositionPreview.SetBodyEditAsync(keys);
    }

    private void BodyTrianglePicked(string draw, int triangle)
    {
        if (_bodyEditParent is null || _plan is null || !_bodyTriangleRegions.TryGetValue(draw, out var original) || !original.ContainsKey(triangle)) return;
        var group = _plan["slots"]!.AsArray().Single(group => group!["id"]!.GetValue<string>() == _bodyEditParent)!;
        var destination = group["segments"]!.AsArray().FirstOrDefault(segment => segment!["body_draw"]?.GetValue<string>() == draw && segment["body_region"]?.GetValue<string>() == _bodyEditRegion);
        if (destination is null) return;
        var beforeState = new JsonObject { ["parent"] = _bodyEditParent, ["choice"] = (string)((ComboBoxItem)_choices[_bodyEditParent].SelectedItem).Tag,
            ["edits"] = _previewDefaults["body_edits"]?.DeepClone(), ["segments"] = new JsonObject() };
        foreach (var segment in group["segments"]!.AsArray()) beforeState["segments"]![segment!["id"]!.GetValue<string>()] = SegmentEnabled(_bodyEditParent, segment) ? "on" : "off";
        var edits = _previewDefaults["body_edits"] as JsonObject;
        if (edits is null) _previewDefaults["body_edits"] = edits = new JsonObject();
        var map = edits[draw] as JsonObject; if (map is null) edits[draw] = map = new JsonObject();
        string key = triangle.ToString(); string? before = map[key]?.GetValue<string>();
        if ((before ?? original[triangle]) == _bodyEditRegion) return;
        _bodyUndo.Push(beforeState); map[key] = _bodyEditRegion;
        var source = _choices[_bodyEditParent];
        foreach (var segment in group["segments"]!.AsArray()) _previewDefaults[segment!["id"]!.GetValue<string>()] = SegmentEnabled(_bodyEditParent, segment) ? "on" : "off";
        _previewDefaults[destination["id"]!.GetValue<string>()] = "on";
        _applyingPreview = true; try { source.SelectedItem = source.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == "split"); } finally { _applyingPreview = false; }
        foreach (var refresh in _segmentSummaries) refresh(); UpdateOutputVersion(); QueuePreview();
        Status(BemText.Format("已将三角形 {0} 归入{1}。", triangle + 1, BodyRegionName(_bodyEditRegion)), InfoBarSeverity.Success);
    }

    private void AddSegmentControls(string parent, JsonArray segments, ComboBox source)
    {
        var body = new StackPanel { Spacing = 10 };
        body.Children.Add(new TextBlock { Text = BemText.Get("默认按源包选项分组。勾选整项会操作全部细分；展开后可只勾选其中几段。原包选项仍决定当前显示的版本。"), TextWrapping = TextWrapping.Wrap });
        var summary = new TextBlock { TextWrapping = TextWrapping.Wrap };
        var warning = new TextBlock { TextWrapping = TextWrapping.Wrap };
        var lists = new List<ListView>();
        var bundleStates = new List<(List<JsonNode> Segments, TextBlock State)>();
        var recordStates = new List<(JsonNode Segment, TextBlock State, TextBlock Label)>();
        var directButtons = new List<(List<JsonNode> Segments, Button Button, string Label)>();
        var actions = new Grid { ColumnSpacing = 6, RowSpacing = 6 };
        actions.ColumnDefinitions.Add(new ColumnDefinition()); actions.ColumnDefinitions.Add(new ColumnDefinition());
        actions.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); actions.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Button AddAction(string label, Action action)
        {
            var button = new Button { Content = BemText.Get(label), Padding = new Thickness(8, 5, 8, 5) };
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.Click += (_, _) => action(); Grid.SetColumn(button, actions.Children.Count % 2); Grid.SetRow(button, actions.Children.Count / 2);
            actions.Children.Add(button); return button;
        }
        List<JsonNode> Selected() => lists.SelectMany(list => list.SelectedItems.Cast<ListViewItem>())
            .SelectMany(item => (List<JsonNode>)item.Tag).DistinctBy(segment => segment["id"]!.GetValue<string>()).ToList();
        void Update()
        {
            var enabled = segments.Where(segment => SegmentEnabled(parent, segment!) && BodyTriangleCount(segment!) > 0).ToList();
            foreach (var bundle in bundleStates)
            {
                var present = bundle.Segments.Where(segment => BodyTriangleCount(segment) > 0).ToList();
                int count = present.Count(segment => SegmentEnabled(parent, segment));
                bundle.State.Text = BemText.Get(present.Count == 0 ? "未提供" : count == 0 ? "未启用" : count == present.Count ? "已启用" : "部分启用");
            }
            foreach (var record in recordStates)
            {
                int count = BodyTriangleCount(record.Segment);
                record.State.Text = BemText.Get(count == 0 ? "未提供" : SegmentEnabled(parent, record.Segment) ? "已启用" : "未启用");
                record.Label.Text = BemText.Format("绘制段 {0} · {1} · {2} 个三角形", record.Segment["draw"]!.GetValue<int>() + 1, record.Segment["material_name"]!.GetValue<string>(), count);
            }
            foreach (var direct in directButtons)
            {
                string action = BemText.Get(direct.Segments.Where(segment => BodyTriangleCount(segment) > 0).All(segment => SegmentEnabled(parent, segment)) ? "停用" : "启用");
                direct.Button.IsEnabled = direct.Segments.Any(segment => BodyTriangleCount(segment) > 0);
                direct.Button.Content = action;
                Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(direct.Button, direct.Label + "：" + action);
            }
            summary.Text = BemText.Format("选中 {0} 条绘制记录 · 已完整启用 {1}/{2} 项", Selected().Count, bundleStates.Count(bundle => bundle.Segments.Any(segment => BodyTriangleCount(segment) > 0) && bundle.Segments.Where(segment => BodyTriangleCount(segment) > 0).All(segment => SegmentEnabled(parent, segment))), bundleStates.Count(bundle => bundle.Segments.Any(segment => BodyTriangleCount(segment) > 0)));
            int sources = enabled.Select(segment => segment!["source_id"]!.GetValue<string>()).Distinct().Count();
            warning.Text = sources > 1
                ? BemText.Format("当前叠加 {0} 个来源：不自动去重、裁剪或按先后覆盖，重叠处可能穿插或闪烁。", sources)
                : BemText.Get("细分会替换整个部件；未启用的段不保留原版。源包选项仍决定各段是否显示。");
        }
        void SetEnabled(bool enabled, List<JsonNode>? targets = null)
        {
            var selected = targets ?? Selected();
            if (selected.Count == 0) { Status(BemText.Get("请先勾选要操作的选项项。")); return; }
            // Switching from a whole source starts from what is currently visible,
            // rather than resurrecting stale refinement choices from an earlier edit.
            var actual = segments.ToDictionary(segment => segment!["id"]!.GetValue<string>(), segment => SegmentEnabled(parent, segment!));
            foreach (var segment in selected) actual[segment["id"]!.GetValue<string>()] = enabled;
            var layouts = segments.Where(segment => actual[segment!["id"]!.GetValue<string>()] && BodyTriangleCount(segment) > 0)
                .Select(segment => segment!["layout"]?.ToJsonString()).Where(layout => layout is not null).Distinct().ToList();
            if (layouts.Count > 1) { Status(BemText.Get("这些绘制段的顶点布局不兼容，不能合并。请先停用冲突来源，再启用新来源。"), InfoBarSeverity.Warning); return; }
            _applyingPreview = true;
            try
            {
                foreach (var pair in actual) _previewDefaults[pair.Key] = pair.Value ? "on" : "off";
                source.SelectedItem = source.Items.Cast<ComboBoxItem>().Single(item => (string)item.Tag == "split");
                _highlightSegments.Clear(); _highlightParent = null;
            }
            finally { _applyingPreview = false; }
            foreach (var update in _segmentSummaries) update();
            UpdateOutputVersion(); QueuePreview();
        }
        Button DirectToggle(List<JsonNode> targets, string label)
        {
            var button = new Button { MinWidth = 54, Padding = new Thickness(8, 4, 8, 4), FontSize = 12, VerticalAlignment = VerticalAlignment.Center };
            button.Click += (_, _) => SetEnabled(!targets.Where(segment => BodyTriangleCount(segment) > 0).All(segment => SegmentEnabled(parent, segment)), targets);
            ToolTipService.SetToolTip(button, BemText.Get("直接启用或停用此项，无需勾选；部分启用时点击会启用全部。"));
            directButtons.Add((targets, button, label)); return button;
        }
        AddAction("启用所选", () => SetEnabled(true));
        AddAction("停用所选", () => SetEnabled(false));
        AddAction("高亮所选", () =>
        {
            var selected = Selected();
            if (selected.Count == 0) { Status(BemText.Get("请先勾选要操作的选项项。")); return; }
            // Inspection uses a temporary snapshot and can never change saved defaults.
            var layouts = selected.Select(segment => segment["layout"]?.ToJsonString()).Where(layout => layout is not null).Distinct().ToList();
            if (layouts.Count > 1) { Status(BemText.Get("所选段包含不兼容的顶点布局，请按来源分别高亮查看。"), InfoBarSeverity.Warning); return; }
            _bodyEditParent = null; _highlightParent = parent; _highlightSegments.Clear();
            foreach (var refresh in _segmentSummaries) refresh();
            foreach (var segment in selected) _highlightSegments.Add(segment["id"]!.GetValue<string>());
            QueuePreview();
        });
        AddAction("退出高亮", () => { _highlightSegments.Clear(); _highlightParent = null; QueuePreview(); });
        body.Children.Add(actions); body.Children.Add(summary); body.Children.Add(warning);
        foreach (var sourceSegments in segments.GroupBy(segment => segment!["source_id"]!.GetValue<string>()))
        {
            string name = sourceSegments.First()!["source_name"]!.GetValue<string>();
            var title = new TextBlock { Text = name, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, TextTrimming = TextTrimming.CharacterEllipsis };
            ToolTipService.SetToolTip(title, name); body.Children.Add(title);
            if (sourceSegments.First()!["shared_option_bindings"] is JsonArray shared)
                body.Children.Add(new TextBlock { Text = BemText.Format("共用选项：{0}", string.Join(" / ", shared.Select(binding => binding!["name"]!.GetValue<string>()))), FontSize = 12, TextWrapping = TextWrapping.Wrap });
            var list = new ListView { SelectionMode = ListViewSelectionMode.Multiple, IsMultiSelectCheckBoxEnabled = true,
                MaxHeight = 260, HorizontalAlignment = HorizontalAlignment.Stretch };
            var sourceLists = new List<ListView> { list };
            foreach (var bundle in sourceSegments.GroupBy(segment => segment!["body_region"]?.GetValue<string>() ?? segment["option_bindings"]?.ToJsonString() ?? "[]"))
            {
                var members = bundle.Select(segment => segment!).ToList();
                var bindings = members[0]["option_bindings"] as JsonArray;
                string label = members[0]["body_region"] is JsonNode regionNode ? BodyRegionName(regionNode.GetValue<string>()) : bindings is { Count: > 0 }
                    ? string.Join(" / ", bindings.Select(binding => binding!["name"]!.GetValue<string>()))
                    : BemText.Get("固定部分");
                var row = new Grid { ColumnSpacing = 8 };
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                var text = new TextBlock { Text = label, TextTrimming = TextTrimming.CharacterEllipsis, VerticalAlignment = VerticalAlignment.Center };
                var state = new TextBlock { FontSize = 12, VerticalAlignment = VerticalAlignment.Center };
                bundleStates.Add((members, state)); row.Children.Add(text); row.Children.Add(state); Grid.SetColumn(state, 1);
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                var direct = DirectToggle(members, label); row.Children.Add(direct); Grid.SetColumn(direct, 2);
                var item = new ListViewItem { Content = row, Tag = members, HorizontalContentAlignment = HorizontalAlignment.Stretch };
                var details = BemText.Format("包含 {0} 条绘制记录；选项变体按原包条件切换。", members.Count);
                if (bindings is { Count: > 1 }) details += "\n" + BemText.Get("由多个选项共同控制，一并操作以保留联动。");
                ToolTipService.SetToolTip(row, name + "\n" + label + "\n" + details);
                if (members.Count > 1)
                {
                    var fine = new ListView { SelectionMode = ListViewSelectionMode.Multiple, IsMultiSelectCheckBoxEnabled = true,
                        MaxHeight = 220, HorizontalAlignment = HorizontalAlignment.Stretch };
                    foreach (var segment in members)
                    {
                        string detail = BemText.Format("绘制段 {0} · {1} · {2} 个三角形", segment["draw"]!.GetValue<int>() + 1,
                            segment["material_name"]!.GetValue<string>(), segment["count"]!.GetValue<int>() / 3);
                        var fineRow = new Grid { ColumnSpacing = 8 };
                        fineRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        fineRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        var fineText = new TextBlock { Text = detail, TextTrimming = TextTrimming.CharacterEllipsis, VerticalAlignment = VerticalAlignment.Center };
                        var fineState = new TextBlock { FontSize = 12, VerticalAlignment = VerticalAlignment.Center };
                        recordStates.Add((segment, fineState, fineText)); fineRow.Children.Add(fineText); fineRow.Children.Add(fineState); Grid.SetColumn(fineState, 1);
                        var fineMembers = new List<JsonNode> { segment };
                        fineRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        var fineDirect = DirectToggle(fineMembers, label + " / " + detail); fineRow.Children.Add(fineDirect); Grid.SetColumn(fineDirect, 2);
                        var fineItem = new ListViewItem { Content = fineRow, Tag = fineMembers, HorizontalContentAlignment = HorizontalAlignment.Stretch };
                        ToolTipService.SetToolTip(fineItem, name + "\n" + label + "\n" + detail); fine.Items.Add(fineItem);
                    }
                    fine.SelectionChanged += (_, _) => Update(); lists.Add(fine); sourceLists.Add(fine);
                    var content = new StackPanel { Spacing = 6 }; content.Children.Add(row);
                    content.Children.Add(new Expander { Header = BemText.Format("展开细分（{0}）", members.Count), Content = fine,
                        HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch });
                    item.Content = content;
                }
                list.Items.Add(item);
            }
            list.SelectionChanged += (_, _) => Update(); lists.Add(list);
            var selectionActions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            var all = new Button { Content = BemText.Get("选择此来源全部项") };
            var clear = new Button { Content = BemText.Get("清除选择") };
            all.Click += (_, _) => list.SelectAll(); clear.Click += (_, _) => { foreach (var sourceList in sourceLists) sourceList.SelectedItems.Clear(); };
            selectionActions.Children.Add(all); selectionActions.Children.Add(clear); body.Children.Add(selectionActions); body.Children.Add(list);
        }
        _segmentSummaries.Add(Update); Update();
        var expander = new Expander { Header = BemText.Get("按源包选项组合"), Content = body,
            HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch };
        _segmentExpanders.Add(parent, expander); _slots.Children.Add(expander);
    }

    private void ShowSourceOptions(JsonObject plan, IReadOnlyList<BemPackage> selected)
    {
        _slots.Children.Add(new TextBlock
        {
            Text = BemText.Get("源包选项沿用当前设置作为默认值，生成后仍可切换。点击关联数量查看部件清单。"),
            TextWrapping = TextWrapping.Wrap
        });
        var childPlans = plan["projects"] is JsonArray projects
            ? projects.Select(p => p!["plan"]!.AsObject()).ToList() : [plan];
        foreach (var child in childPlans)
        {
            if (child["source_options"] is not JsonArray sources) continue;
            foreach (var source in sources)
            {
                string id = source!["package_id"]!.GetValue<string>();
                var package = selected.FirstOrDefault(p => p.Id == id);
                var groups = source["groups"]!.AsArray();
                if (groups.Count == 0) continue;
                var content = new StackPanel { Spacing = 12 };
                foreach (var node in groups)
                {
                    string groupId = node!["id"]!.GetValue<string>();
                    string name = node["name"]!.GetValue<string>();
                    string defaultId = node["default"]!.GetValue<string>();
                    var choices = node["choices"]!.AsArray();
                    string choiceName = choices.First(c => c!["id"]!.GetValue<string>() == defaultId)!["name"]!.GetValue<string>();
                    var bindings = package?.OptionGroups.FirstOrDefault(g => g.Id == groupId)?.RelatedComponents
                        ?? package?.Components ?? [];
                    content.Children.Add(BemOptionHeader.Create(name, bindings));
                    content.Children.Add(new TextBlock { Text = BemText.Format("默认：{0} · {1} 个选项", choiceName, choices.Count), TextTrimming = TextTrimming.CharacterEllipsis });
                }
                string header = source["name"]!.GetValue<string>();
                if (childPlans.Count > 1) header += " / " + child["request"]!["name"]!.GetValue<string>();
                var title = new TextBlock { Text = BemText.Format("{0} · 保留 {1} 组选项", header, groups.Count), TextTrimming = TextTrimming.CharacterEllipsis };
                ToolTipService.SetToolTip(title, header);
                _slots.Children.Add(new Expander { Header = title, Content = content, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch });
            }
        }
    }

    private JsonObject CurrentDefaults()
    {
        var defaults = (JsonObject)_previewDefaults.DeepClone();
        foreach (var pair in _choices) defaults[pair.Key] = (string)((ComboBoxItem)pair.Value.SelectedItem).Tag;
        return defaults;
    }

    private JsonObject PreviewDefaults()
    {
        var defaults = CurrentDefaults();
        if (_highlightParent is null || _highlightSegments.Count == 0 || _plan is null) return defaults;
        var group = _plan["slots"]!.AsArray().Single(item => item!["id"]!.GetValue<string>() == _highlightParent)!;
        var segments = group["segments"]!.AsArray();
        // A temporary isolated selection also lets disabled segments be inspected,
        // without accidentally adding incompatible enabled meshes to this snapshot.
        defaults[_highlightParent] = "split";
        foreach (var segment in segments) defaults[segment!["id"]!.GetValue<string>()] = _highlightSegments.Contains(segment["id"]!.GetValue<string>()) ? "on" : "off";
        return defaults;
    }

    private Task PreviewAsync() => ExecuteAsync(async token =>
    {
        var plan = _plan ?? throw new InvalidOperationException(BemText.Get("请先分析来源。"));
        int session = ++_previewSession;
        _previewDirty = false;
        Status(BemText.Get("正在准备组合预览…"));
        var defaults = PreviewDefaults();
        string atomicSignature = string.Join("&", plan["atomic_items"]!.AsArray().Select(g =>
        {
            string id = g!["id"]!.GetValue<string>();
            string choice = defaults[id]?.GetValue<string>() ?? g["default"]!.GetValue<string>();
            return id + ":" + (choice == "original" ? "0" : "1");
        }));
        foreach (var group in plan["slots"]!.AsArray())
        {
            string id = group!["id"]!.GetValue<string>();
            if (defaults[id]?.GetValue<string>() != "split") continue;
            atomicSignature += "|" + id;
            foreach (var segment in group["segments"]!.AsArray())
            {
                string key = segment!["id"]!.GetValue<string>();
                atomicSignature += "|" + key + ":" + (defaults[key]?.GetValue<string>() ?? segment["default"]!.GetValue<string>());
            }
        }
        atomicSignature += "|body:" + defaults["body_edits"]?.ToJsonString();
        var report = _previewReport is not null && _previewAtomicSignature == atomicSignature
            ? _previewReport : await _composer.PreviewAsync(plan, defaults, token);
        token.ThrowIfCancellationRequested();
        if (_closed || session != _previewSession || !ReferenceEquals(plan, _plan)) return;
        _previewReport = report; _previewAtomicSignature = atomicSignature;
        var files = report["outputs"]!.AsArray();
        string? selectedPrefix = _previewOutput.SelectedIndex >= 0 && _previewOutput.SelectedIndex < _previewFiles.Count
            ? _previewFiles[_previewOutput.SelectedIndex].Prefix : null;
        _settingOutputs = true; _previewOutput.Items.Clear(); _previewFiles.Clear();
        for (int i = 0; i < files.Count; ++i)
        {
            string id = report["output_ids"]![i]!.GetValue<string>();
            string prefix = plan["projects"] is JsonArray projects
                ? projects.Single(p => p!["plan"]!["request"]!["project_id"]!.GetValue<string>() == id)!["id"]!.GetValue<string>() + "." : "";
            string file = files[i]!.GetValue<string>();
            _previewFiles.Add((file, prefix));
            _previewOutput.Items.Add(BemPackageService.ReadMetadata(file).Name);
        }
        _previewOutput.Visibility = files.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
        if (_highlightParent is not null) selectedPrefix = _highlightParent.Contains('.') ? _highlightParent[..(_highlightParent.IndexOf('.') + 1)] : "";
        _previewOutput.SelectedIndex = Math.Max(0, _previewFiles.FindIndex(x => x.Prefix == selectedPrefix));
        _settingOutputs = false;
        await ShowPreviewOutputAsync(session, plan);
        token.ThrowIfCancellationRequested();
        if (_closed || session != _previewSession) return;
        _previewHost.Opacity = 1;
        Status(BemText.Get("组合预览已更新。"), InfoBarSeverity.Success);
    }, preview: true);

    private async Task ShowPreviewOutputAsync(int session, JsonObject plan)
    {
        var (file, prefix) = _previewFiles[_previewOutput.SelectedIndex];
        var package = BemPackageService.ReadMetadata(file);
        var groupIds = package.OptionGroups.Select(g => g.Id).ToHashSet(StringComparer.Ordinal);
        var defaults = PreviewDefaults();
        string selection = string.Join("&", defaults.Where(x => x.Key.StartsWith(prefix, StringComparison.Ordinal) && groupIds.Contains(x.Key[prefix.Length..]))
            .Select(x => x.Key[prefix.Length..] + ":" + x.Value!.GetValue<string>()));
        if (_compositionPreview is null)
        {
            _compositionPreview = new BemPreviewView(package, _installRoot, null, () => { });
            _compositionPreview.BodyTrianglePicked += BodyTrianglePicked;
            await _compositionPreview.SetDraftAsync(file, SaveDraftAsync, selection);
            _previewHost.Content = _compositionPreview;
        }
        else await _compositionPreview.SetDraftAsync(file, SaveDraftAsync, selection);

        var keys = _highlightParent is null ? Array.Empty<string>() : plan["slots"]!.AsArray()
            .Single(group => group!["id"]!.GetValue<string>() == _highlightParent)!["segments"]!.AsArray()
            .Where(segment => _highlightSegments.Contains(segment!["id"]!.GetValue<string>()))
            .Select(segment => segment!["preview_key"]!.GetValue<string>()).ToArray();
        await _compositionPreview.HighlightSegmentsAsync(keys, keys.Length > 0);
        await SyncBodyEditAsync();

        Task SaveDraftAsync(string selection, string parameters)
            {
                if (_highlightSegments.Count > 0) throw new InvalidOperationException(BemText.Get("高亮为临时查看，不会保存；请退出高亮后再应用选项。"));
                if (_closed || _operation is not null || !ReferenceEquals(_plan, plan) || session != _previewSession)
                    throw new InvalidOperationException(BemText.Get("组合已变更，请从组合器重新打开预览。"));
                var values = new Dictionary<string, string>(StringComparer.Ordinal);
                foreach (string pair in selection.Split('&', StringSplitOptions.RemoveEmptyEntries))
                {
                    int split = pair.IndexOf(':');
                    if (split <= 0) throw new InvalidDataException("Invalid composition selection");
                    values.Add(prefix + pair[..split], pair[(split + 1)..]);
                }
                _applyingPreview = true;
                try
                {
                    foreach (var pair in values)
                    {
                        _previewDefaults[pair.Key] = pair.Value;
                        if (_choices.TryGetValue(pair.Key, out var box))
                            box.SelectedItem = box.Items.Cast<ComboBoxItem>().Single(x => (string)x.Tag == pair.Value);
                    }
                }
                finally { _applyingPreview = false; }
                UpdateOutputVersion();
                Status(BemText.Get("预览选择已应用到组合默认值。"), InfoBarSeverity.Success);
                return Task.CompletedTask;
            }
    }

    private Task BuildAsync() => ExecuteAsync(async token =>
    {
        var plan = _plan ?? throw new InvalidOperationException(BemText.Get("请先分析来源。"));
        var defaults = CurrentDefaults();
        string project = plan["request"]!["project_id"]!.GetValue<string>();
        string root = Path.GetFullPath(_output.Text.Trim()); Directory.CreateDirectory(root);
        string output = Path.Combine(root, project); int action = _action.SelectedIndex;
        Status(BemText.Get("正在生成并校验组合包…")); _results.Children.Clear();
        var report = await _composer.BuildAsync(plan, defaults, output, token);
        _plan = null;
        if (_closed) return;
        var files = report["outputs"]!.AsArray().Select(x => x!.GetValue<string>()).ToList();
        var open = new Button { Content = BemText.Get("打开输出目录") };
        open.Click += (_, _) => { try { Process.Start(new ProcessStartInfo(output) { UseShellExecute = true }); } catch (Exception ex) { Status(ex.Message, InfoBarSeverity.Error); } };
        foreach (string file in files) _results.Children.Add(new TextBlock { Text = file, TextWrapping = TextWrapping.Wrap });
        _results.Children.Add(open);
        Status(BemText.Get("组合包已生成并通过校验。"), InfoBarSeverity.Success);
        // Publication is complete. A later import failure leaves the validated
        // output and its folder button available for a manual retry.
        if (action > 0)
        {
            foreach (string file in files) await _packages.ImportAsync(file, _installRoot);
            if (action == 2)
            {
                foreach (string file in files)
                {
                    // Import copies the file; identify each generated package by its recorded ID.
                    string id = report["output_ids"]?[files.IndexOf(file)]?.GetValue<string>() ?? project;
                    var package = _packages.Packages.Single(p => p.Id == id);
                    await _packages.SetEnabledAsync(package, true);
                }
            }
            _refresh();
            Status(BemText.Get(action == 2 ? "组合包已导入并启用。" : "组合包已导入。"), InfoBarSeverity.Success);
        }
    });
}
