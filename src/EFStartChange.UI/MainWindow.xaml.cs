using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using EFStartChange.UI.Models;
using EFStartChange.UI.Services;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Graphics;
using Windows.Storage.Pickers;

namespace EFStartChange.UI;

public sealed partial class MainWindow : Window
{
    private static readonly Dictionary<string, string> VoiceLanguageNames = new(
        StringComparer.OrdinalIgnoreCase)
    {
        ["Chinese"] = "Chinese",
        ["CN"] = "Chinese",
        ["ZH"] = "Chinese",
        ["English"] = "English",
        ["EN"] = "English",
        ["Japanese"] = "Japanese",
        ["JP"] = "Japanese",
        ["JA"] = "Japanese",
        ["Korean"] = "Korean",
        ["KR"] = "Korean",
        ["KO"] = "Korean",
        ["FollowGlobal"] = "FollowGlobal",
        ["Global"] = "FollowGlobal",
        ["Default"] = "FollowGlobal"
    };

    private static readonly Regex RuntimeClipLengthPattern = new(
        @"\[sequence\].*?phase=(?<label>\S+).*?length=(?<length>[0-9]+(?:\.[0-9]+)?)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly DispatcherTimer _statusTimer = new()
    {
        Interval = TimeSpan.FromSeconds(1)
    };

    private bool _initializing = true;
    private string _durationLogPath = string.Empty;
    private DateTime _durationLogWriteUtc;

    public MainWindow()
    {
        InitializeComponent();
        Title = "EF Start Change";
        TryResizeWindow();

        CharacterComboBox.ItemsSource = PresetOptions.Characters;
        VoiceLanguageComboBox.ItemsSource = new[]
        {
            "Chinese", "English", "Japanese", "Korean", "FollowGlobal"
        };
        VoiceLanguageComboBox.SelectedIndex = 2;
        _statusTimer.Tick += StatusTimer_Tick;
        Closed += MainWindow_Closed;
    }

    private async void MainRoot_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            AppSettings settings = await ConfigurationService.LoadAppSettingsAsync();
            GamePathBox.Text = !string.IsNullOrWhiteSpace(settings.GameExecutablePath)
                ? settings.GameExecutablePath
                : ConfigurationService.DiscoverGamePath();
            MapperPathBox.Text = !string.IsNullOrWhiteSpace(settings.MapperPath)
                ? settings.MapperPath
                : ConfigurationService.DiscoverMapperPath();
            RefreshRuntimeAnimationDurations();

            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(MapperPathBox.Text);
            ApplyConfiguration(configuration);
            _initializing = false;
            UpdateCrossfadePanel();
            RefreshRuntimeStatus();
            _statusTimer.Start();
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            _initializing = false;
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private async void BrowseGameButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 Endfield.exe");
        if (selectedPath is not null)
        {
            GamePathBox.Text = selectedPath;
        }
    }

    private async void BrowseMapperButton_Click(object sender, RoutedEventArgs e)
    {
        string? selectedPath = await PickExecutableAsync("选择 Il2cppDumper.exe");
        if (selectedPath is null)
        {
            return;
        }

        MapperPathBox.Text = selectedPath;
        try
        {
            ModConfiguration configuration =
                await ConfigurationService.LoadModConfigurationAsync(selectedPath);
            ApplyConfiguration(configuration);
        }
        catch (IOException exception)
        {
            ShowStatus("读取配置失败", exception.Message, InfoBarSeverity.Error);
        }
    }

    private void MapperPathBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ConfigurationPathTextBlock.Text =
            ConfigurationService.GetNativeConfigurationPath(MapperPathBox.Text);
        RefreshRuntimeAnimationDurations();
        RefreshRuntimeStatus();
    }

    private void CharacterComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        string? preferredAction = _initializing
            ? null
            : (CharacterComboBox.SelectedItem as CharacterOption)?.DefaultActionId;
        RefreshActionOptions(preferredAction);
    }

    private void FinalActionComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (FinalActionComboBox.SelectedItem is not ActionOption action)
        {
            ActionDurationTextBlock.Text = string.Empty;
            ActionDescriptionTextBlock.Text = string.Empty;
            return;
        }

        UpdateActionMetadata(action);
        ActionDescriptionTextBlock.Text =
            $"资源哈希：{action.PathHash} · 原生 LoopTime：{(action.NativeLoop ? "是" : "否")}";
        if (!_initializing)
        {
            FinalLoopToggle.IsOn = action.NativeLoop;
            ForceLoopToggle.IsOn = false;
            CrossfadeToggle.IsOn = false;
        }
    }

    private void CrossfadeToggle_Toggled(object sender, RoutedEventArgs e) =>
        UpdateCrossfadePanel();

    private void AddVoiceRuleButton_Click(object sender, RoutedEventArgs e)
    {
        if (CharacterComboBox.SelectedItem is not CharacterOption character ||
            VoiceLanguageComboBox.SelectedItem is not string language)
        {
            return;
        }

        string speaker = GetCharacterSpeakerAlias(character.Id);
        if (!TryNormalizeVoiceLanguageRules(
            VoiceLanguageRulesBox.Text,
            out string normalizedRules,
            out string? ruleError))
        {
            ShowStatus(
                "配音规则无效",
                ruleError ?? "请先修正现有规则。",
                InfoBarSeverity.Error);
            return;
        }
        var lines = normalizedRules.Split(
            ['\r', '\n'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
        int existingIndex = lines.FindIndex(line =>
        {
            int separator = line.IndexOfAny(['=', ':']);
            return separator > 0 && line[..separator].Trim().Equals(
                speaker,
                StringComparison.OrdinalIgnoreCase);
        });
        string rule = $"{speaker}={language}";
        if (existingIndex >= 0)
        {
            lines[existingIndex] = rule;
        }
        else
        {
            lines.Add(rule);
        }

        VoiceLanguageRulesBox.Text = string.Join(Environment.NewLine, lines);
        VoiceRouterToggle.IsOn = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        await SaveAsync(showSuccess: true);
    }

    private async void LaunchButton_Click(object sender, RoutedEventArgs e)
    {
        if (Process.GetProcessesByName("Endfield").Length > 0)
        {
            ShowStatus(
                "游戏已经在运行",
                "为避免重复注入，请完整退出游戏后再使用“保存并启动”。",
                InfoBarSeverity.Warning);
            return;
        }

        if (!File.Exists(GamePathBox.Text.Trim()))
        {
            ShowStatus("游戏路径无效", "请选择有效的 Endfield.exe。", InfoBarSeverity.Error);
            return;
        }

        LaunchProgressRing.Visibility = Visibility.Visible;
        LaunchProgressRing.IsActive = true;
        try
        {
            if (!await SaveAsync(showSuccess: false))
            {
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = MapperPathBox.Text.Trim(),
                WorkingDirectory = Path.GetDirectoryName(MapperPathBox.Text.Trim()) ?? string.Empty,
                UseShellExecute = true
            };
            startInfo.ArgumentList.Add(GamePathBox.Text.Trim());
            Process.Start(startInfo);
            ShowStatus(
                "注入器已启动",
                "如果出现用户账户控制提示，请允许管理员权限。游戏启动后状态会自动更新。",
                InfoBarSeverity.Success);
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or IOException or UnauthorizedAccessException)
        {
            ShowStatus("启动失败", exception.Message, InfoBarSeverity.Error);
        }
        finally
        {
            LaunchProgressRing.IsActive = false;
            LaunchProgressRing.Visibility = Visibility.Collapsed;
        }
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyConfiguration(ModConfiguration.CreateDefaults());
        ShowStatus(
            "已恢复界面默认值",
            "点击“保存参数”后才会覆盖配置文件。",
            InfoBarSeverity.Informational);
    }

    private void OpenConfigurationFolderButton_Click(object sender, RoutedEventArgs e)
    {
        string configPath =
            ConfigurationService.GetNativeConfigurationPath(MapperPathBox.Text);
        string? directory = Path.GetDirectoryName(configPath);
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
        {
            ShowStatus("目录不存在", "请先选择有效的注入器。", InfoBarSeverity.Warning);
            return;
        }

        OpenWithShell(directory);
    }

    private void OpenLogButton_Click(object sender, RoutedEventArgs e)
    {
        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (!File.Exists(logPath))
        {
            ShowStatus(
                "尚未生成日志",
                "至少完成一次注入后才会出现 IL2CPPDump_Log.txt。",
                InfoBarSeverity.Informational);
            return;
        }

        OpenWithShell(logPath);
    }

    private async Task<bool> SaveAsync(bool showSuccess)
    {
        string mapperPath = MapperPathBox.Text.Trim();
        if (!File.Exists(mapperPath))
        {
            ShowStatus("注入器路径无效", "请选择有效的 Il2cppDumper.exe。", InfoBarSeverity.Error);
            return false;
        }

        if (!TryReadConfiguration(out ModConfiguration configuration, out string? error))
        {
            ShowStatus("参数无效", error ?? "请检查输入值。", InfoBarSeverity.Error);
            return false;
        }

        try
        {
            await ConfigurationService.SaveModConfigurationAsync(mapperPath, configuration);
            await ConfigurationService.SaveAppSettingsAsync(new AppSettings
            {
                GameExecutablePath = GamePathBox.Text.Trim(),
                MapperPath = mapperPath
            });

            ConfigurationPathTextBlock.Text =
                ConfigurationService.GetNativeConfigurationPath(mapperPath);
            if (showSuccess)
            {
                ShowStatus(
                    "参数已保存",
                    "配置会在下一次启动并注入时读取。",
                    InfoBarSeverity.Success);
            }

            return true;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
            ShowStatus("保存失败", exception.Message, InfoBarSeverity.Error);
            return false;
        }
    }

    private bool TryReadConfiguration(
        out ModConfiguration configuration,
        out string? error)
    {
        configuration = ModConfiguration.CreateDefaults();
        error = null;
        if (CharacterComboBox.SelectedItem is not CharacterOption character ||
            FinalActionComboBox.SelectedItem is not ActionOption action)
        {
            error = "请选择角色和最后一段动作。";
            return false;
        }

        NumberBox[] numberBoxes =
        [
            StartYawNumberBox,
            TurnDurationNumberBox,
            ScaleNumberBox,
            LeanSampleNumberBox,
            SitLoopSpeedNumberBox,
            SitSpecialSpeedNumberBox,
            SitToWalkSpeedNumberBox,
            FinalSpeedNumberBox,
            LoopStartNumberBox,
            LoopEndNumberBox,
            CrossfadeDurationNumberBox
        ];
        if (numberBoxes.Any(box => !double.IsFinite(box.Value)))
        {
            error = "所有数值输入框都必须填写有效数字。";
            return false;
        }

        if (CrossfadeToggle.IsOn && LoopEndNumberBox.Value <= LoopStartNumberBox.Value + 0.05)
        {
            error = "人工循环终点必须至少比起点晚 0.05 秒。";
            return false;
        }

        double loopDuration = LoopEndNumberBox.Value - LoopStartNumberBox.Value;
        if (CrossfadeToggle.IsOn && CrossfadeDurationNumberBox.Value > loopDuration * 0.5)
        {
            error = "交叉混合时长不能超过循环区间的一半。";
            return false;
        }

        if (!TryNormalizeVoiceLanguageRules(
            VoiceLanguageRulesBox.Text,
            out string voiceLanguageRules,
            out error))
        {
            return false;
        }

        configuration = new ModConfiguration
        {
            Character = character.Id,
            FinalAction = action.Id,
            ModelPath = character.Model.Path,
            ModelPathHash = character.Model.PathHash,
            ModelBundleHash = character.Model.BundleHash,
            SitLoopPath = character.SitLoop.Path,
            SitLoopPathHash = character.SitLoop.PathHash,
            SitLoopLabel = character.SitLoop.DisplayName,
            SitSpecialPath = character.SitSpecial.Path,
            SitSpecialPathHash = character.SitSpecial.PathHash,
            SitSpecialLabel = character.SitSpecial.DisplayName,
            SitToWalkPath = character.SitToWalk.Path,
            SitToWalkPathHash = character.SitToWalk.PathHash,
            SitToWalkLabel = character.SitToWalk.DisplayName,
            FinalPath = action.Path,
            FinalPathHash = action.PathHash,
            FinalLabel = action.Id,
            FinalNativeLoop = action.NativeLoop,
            StartYaw = StartYawNumberBox.Value,
            TurnDuration = TurnDurationNumberBox.Value,
            Scale = ScaleNumberBox.Value,
            ForwardLeanSample = LeanSampleNumberBox.Value,
            SitLoopSpeed = SitLoopSpeedNumberBox.Value,
            SitSpecialSpeed = SitSpecialSpeedNumberBox.Value,
            SitToWalkSpeed = SitToWalkSpeedNumberBox.Value,
            FinalSpeed = FinalSpeedNumberBox.Value,
            FinalLoop = FinalLoopToggle.IsOn,
            ForceLoop = ForceLoopToggle.IsOn,
            UseCrossfade = CrossfadeToggle.IsOn,
            LoopStart = LoopStartNumberBox.Value,
            LoopEnd = LoopEndNumberBox.Value,
            CrossfadeDuration = CrossfadeDurationNumberBox.Value,
            VoiceRouterEnabled = VoiceRouterToggle.IsOn,
            VoiceLanguageRules = voiceLanguageRules
        };
        return true;
    }

    private void ApplyConfiguration(ModConfiguration configuration)
    {
        bool wasInitializing = _initializing;
        _initializing = true;

        string characterId = PresetOptions.NormalizeCharacterId(configuration.Character);
        string actionId = PresetOptions.NormalizeActionId(configuration.FinalAction);
        CharacterComboBox.SelectedItem = PresetOptions.Characters.FirstOrDefault(
            character => character.Id.Equals(
                characterId,
                StringComparison.OrdinalIgnoreCase)) ??
            PresetOptions.Characters.First(character =>
                character.Id == "chr_0013_aglina");
        RefreshActionOptions(actionId);

        StartYawNumberBox.Value = configuration.StartYaw;
        TurnDurationNumberBox.Value = configuration.TurnDuration;
        ScaleNumberBox.Value = configuration.Scale;
        LeanSampleNumberBox.Value = configuration.ForwardLeanSample;
        SitLoopSpeedNumberBox.Value = configuration.SitLoopSpeed;
        SitSpecialSpeedNumberBox.Value = configuration.SitSpecialSpeed;
        SitToWalkSpeedNumberBox.Value = configuration.SitToWalkSpeed;
        FinalSpeedNumberBox.Value = configuration.FinalSpeed;
        FinalLoopToggle.IsOn = configuration.FinalLoop;
        ForceLoopToggle.IsOn = configuration.ForceLoop;
        CrossfadeToggle.IsOn = configuration.UseCrossfade;
        LoopStartNumberBox.Value = configuration.LoopStart;
        LoopEndNumberBox.Value = configuration.LoopEnd;
        CrossfadeDurationNumberBox.Value = configuration.CrossfadeDuration;
        VoiceRouterToggle.IsOn = configuration.VoiceRouterEnabled;
        VoiceLanguageRulesBox.Text = configuration.VoiceLanguageRules;

        _initializing = wasInitializing;
        UpdateCrossfadePanel();
    }

    private void RefreshActionOptions(string? preferredAction)
    {
        if (CharacterComboBox.SelectedItem is not CharacterOption character)
        {
            FinalActionComboBox.ItemsSource = null;
            PresetDetailsTextBlock.Text = string.Empty;
            return;
        }

        IReadOnlyList<ActionOption> actions = character.Actions;
        FinalActionComboBox.ItemsSource = actions;
        FinalActionComboBox.SelectedItem = actions.FirstOrDefault(action =>
            action.Id.Equals(preferredAction, StringComparison.OrdinalIgnoreCase)) ??
            actions.FirstOrDefault(action =>
                action.Id.Equals(
                    character.DefaultActionId,
                    StringComparison.OrdinalIgnoreCase)) ??
            actions.FirstOrDefault();
        PresetDetailsTextBlock.Text =
            $"Sit loop: {character.SitLoop.DisplayName}\n" +
            $"Sit special: {character.SitSpecial.DisplayName}\n" +
            $"Sit to walk: {character.SitToWalk.DisplayName}";
    }

    private void UpdateCrossfadePanel()
    {
        bool enabled = CrossfadeToggle.IsOn;
        CrossfadeSettingsPanel.Opacity = enabled ? 1.0 : 0.55;
        LoopStartNumberBox.IsEnabled = enabled;
        LoopEndNumberBox.IsEnabled = enabled;
        CrossfadeDurationNumberBox.IsEnabled = enabled;
    }

    private static bool TryNormalizeVoiceLanguageRules(
        string source,
        out string normalized,
        out string? error)
    {
        normalized = string.Empty;
        error = null;
        string[] entries = source.Split(
            ['\r', '\n', ',', ';'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var rules = new List<string>(entries.Length);
        foreach (string entry in entries)
        {
            int equals = entry.IndexOf('=');
            int colon = entry.IndexOf(':');
            int separator = equals >= 0 && colon >= 0
                ? Math.Min(equals, colon)
                : Math.Max(equals, colon);
            if (separator <= 0 || separator >= entry.Length - 1)
            {
                error = $"配音规则“{entry}”格式错误，应为 speakerChannel=Japanese。";
                return false;
            }

            string speaker = entry[..separator].Trim();
            string language = entry[(separator + 1)..].Trim();
            if (speaker != "*" && speaker.Any(character =>
                !char.IsAsciiLetterOrDigit(character) &&
                character is not '_' and not '-' and not '.'))
            {
                error = $"speakerChannel“{speaker}”只能包含英文字母、数字、_、- 或 .。";
                return false;
            }
            if (!VoiceLanguageNames.TryGetValue(language, out string? canonicalLanguage))
            {
                error = $"不支持配音语言“{language}”。";
                return false;
            }

            rules.Add($"{speaker}={canonicalLanguage}");
        }

        normalized = string.Join(Environment.NewLine, rules);
        return true;
    }

    private static string GetCharacterSpeakerAlias(string characterId)
    {
        if (characterId.StartsWith("chr_", StringComparison.OrdinalIgnoreCase))
        {
            int separator = characterId.IndexOf('_', 4);
            if (separator >= 0 && separator + 1 < characterId.Length)
            {
                return characterId[(separator + 1)..];
            }
        }
        return characterId;
    }

    private async Task<string?> PickExecutableAsync(string commitButtonText)
    {
        var picker = new FileOpenPicker
        {
            CommitButtonText = commitButtonText,
            SuggestedStartLocation = PickerLocationId.ComputerFolder
        };
        picker.FileTypeFilter.Add(".exe");
        WinRT.Interop.InitializeWithWindow.Initialize(
            picker,
            WinRT.Interop.WindowNative.GetWindowHandle(this));
        Windows.Storage.StorageFile? file = await picker.PickSingleFileAsync();
        return file?.Path;
    }

    private void StatusTimer_Tick(object? sender, object e) => RefreshRuntimeStatus();

    private void RefreshRuntimeStatus()
    {
        RefreshRuntimeAnimationDurations();
        bool gameRunning = Process.GetProcessesByName("Endfield").Length > 0;
        RuntimeStatusTextBlock.Text = gameRunning ? "● 游戏正在运行" : "○ 游戏未运行";
        RuntimeStatusTextBlock.Foreground = gameRunning
            ? new SolidColorBrush(Microsoft.UI.Colors.LimeGreen)
            : (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];

        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (File.Exists(logPath))
        {
            DateTime updated = File.GetLastWriteTime(logPath);
            LogStatusTextBlock.Text = $"日志更新：{updated:MM-dd HH:mm:ss}";
        }
        else
        {
            LogStatusTextBlock.Text = File.Exists(MapperPathBox.Text.Trim())
                ? "注入器已就绪 · 尚无日志"
                : "未找到注入器";
        }
    }

    private void RefreshRuntimeAnimationDurations()
    {
        string logPath = ConfigurationService.GetLogPath(MapperPathBox.Text);
        if (!logPath.Equals(_durationLogPath, StringComparison.OrdinalIgnoreCase))
        {
            _durationLogPath = logPath;
            _durationLogWriteUtc = DateTime.MinValue;
        }
        if (!File.Exists(logPath))
        {
            return;
        }

        DateTime writeUtc = File.GetLastWriteTimeUtc(logPath);
        if (writeUtc <= _durationLogWriteUtc)
        {
            return;
        }

        try
        {
            var actionsById = PresetOptions.Characters
                .SelectMany(character => character.Actions)
                .GroupBy(action => action.Id, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToArray(),
                    StringComparer.OrdinalIgnoreCase);
            foreach (string line in File.ReadLines(logPath))
            {
                Match match = RuntimeClipLengthPattern.Match(line);
                if (!match.Success ||
                    !double.TryParse(
                        match.Groups["length"].Value,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double length) ||
                    length <= 0.0 ||
                    !actionsById.TryGetValue(
                        match.Groups["label"].Value,
                        out ActionOption[]? actions))
                {
                    continue;
                }

                foreach (ActionOption action in actions)
                {
                    action.Duration = length;
                }
            }

            _durationLogWriteUtc = writeUtc;
            if (FinalActionComboBox.SelectedItem is ActionOption selectedAction)
            {
                UpdateActionMetadata(selectedAction);
            }
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException)
        {
        }
    }

    private void UpdateActionMetadata(ActionOption action)
    {
        ActionDurationTextBlock.Text = action.Duration is double seconds
            ? $"动画原始时长：{seconds.ToString("0.###", CultureInfo.InvariantCulture)} 秒"
            : "动画原始时长：未知（首次播放后自动读取）";
    }

    private void ShowStatus(string title, string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = title;
        StatusInfoBar.Message = message;
        StatusInfoBar.Severity = severity;
        StatusInfoBar.IsOpen = true;
    }

    private static void OpenWithShell(string path)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true
        });
    }

    private void TryResizeWindow()
    {
        try
        {
            nint windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId windowId =
                Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow.GetFromWindowId(windowId).Resize(new SizeInt32(1180, 860));
        }
        catch (InvalidOperationException)
        {
        }
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _statusTimer.Stop();
    }
}
