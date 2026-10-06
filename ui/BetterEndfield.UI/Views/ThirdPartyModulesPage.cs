using BetterEndfield.UI.Services;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Text.Json.Nodes;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

public sealed class ThirdPartyModulesPage : UserControl
{
    private const string ResourceCenterUrl = "https://146.235.16.65:8443/endfield/";
    private readonly ThirdPartyModuleService _service = new();
    private readonly StackPanel _cards = new() { Spacing = 12 };
    private readonly TextBlock _status = new() { TextWrapping = TextWrapping.Wrap };
    private bool _busy;
    private static string L(string key) => LocalizationService.Instance["Modules_" + key];
    private static TextBlock Text(Func<string> value, double size = 14)
    {
        var text = new TextBlock { FontSize = size, TextWrapping = TextWrapping.Wrap };
        BemLocalizedUI.Set(text, TextBlock.TextProperty, value);
        return text;
    }
    private static Button ActionButton(string key, Action click, bool enabled = true)
    {
        var button = new Button { IsEnabled = enabled };
        BemLocalizedUI.Set(button, Button.ContentProperty, () => L(key));
        button.Click += (_, _) => click();
        return button;
    }
    private void Status(Func<string> text) => BemLocalizedUI.Set(_status, TextBlock.TextProperty, text);
    private void LanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LocalizationService.IsChinese))
            DispatcherQueue.TryEnqueue(() => BemLocalizedUI.Refresh(this));
    }
    public ThirdPartyModulesPage()
    {
        var page = new StackPanel { Spacing = 16, Padding = new Thickness(24) };
        var title = Text(() => L("Title"), 28);
        title.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;
        page.Children.Add(title);
        page.Children.Add(Text(() => L("Description")));
        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };
        var import = new Button();
        BemLocalizedUI.Set(import, Button.ContentProperty, () => L("Import"));
        import.Click += Import;
        var refresh = ActionButton("Refresh", Render);
        actions.Children.Add(ActionButton("ResourceCenter", OpenResourceCenter));
        actions.Children.Add(import); actions.Children.Add(refresh); page.Children.Add(actions); page.Children.Add(_status); page.Children.Add(_cards);
        Content = new ScrollViewer { Content = page, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        Loaded += (_, _) =>
        {
            LocalizationService.Instance.PropertyChanged -= LanguageChanged;
            LocalizationService.Instance.PropertyChanged += LanguageChanged;
            BemLocalizedUI.Refresh(this);
            Render();
        };
        Unloaded += (_, _) => LocalizationService.Instance.PropertyChanged -= LanguageChanged;
    }
    private void OpenResourceCenter()
    {
        try { Process.Start(new ProcessStartInfo(ResourceCenterUrl) { UseShellExecute = true }); }
        catch (Exception error) { Status(() => L("ResourceCenterOpenFailed") + error.Message); }
    }
    private void Render()
    {
        _cards.Children.Clear();
        try
        {
            var records = _service.Records();
            if (records.Count == 0) _cards.Children.Add(Text(() => L("Empty")));
            foreach (var record in records)
            {
                var card = new StackPanel { Spacing = 10 };
                card.Children.Add(new TextBlock { Text = record.Name, FontSize = 20 });
                card.Children.Add(new TextBlock { Text = $"{record.Manifest["author"]} · {record.Manifest["version"]} · {record.Id}", TextWrapping = TextWrapping.Wrap });
                card.Children.Add(Text(() => record.Error.Length > 0 ? L("ReadFailed") + record.Error : L(record.Supported ? "Supported" : "Unsupported")));
                var enabled = new ToggleSwitch { IsOn = record.Enabled, IsEnabled = record.Supported && !_busy };
                BemLocalizedUI.Set(enabled, ToggleSwitch.HeaderProperty, () => L("Enable"));
                BemLocalizedUI.Set(enabled, ToggleSwitch.OnContentProperty, () => L("On"));
                BemLocalizedUI.Set(enabled, ToggleSwitch.OffContentProperty, () => L("Off"));
                enabled.Toggled += async (_, _) => { if (_busy) return; await Act(() => _service.SetEnabledAsync(record.Id, enabled.IsOn), "EnabledSaved"); };
                card.Children.Add(enabled);
                var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
                void AddButton(string key, Action click, bool available = true) => actions.Children.Add(ActionButton(key, click, available && !_busy));
                AddButton("OpenWeb", () => ThirdPartyModuleWindow.Open(record), record.Ui.Length > 0);
                AddButton("StatusLog", async () =>
                {
                    try { var status = await _service.RuntimeAsync("status", record.Id); Status(() => status.ToJsonString(new System.Text.Json.JsonSerializerOptions { WriteIndented = true })); }
                    catch (Exception) { Status(() => L("Disconnected")); }
                });
                AddButton("MoveUp", async () => await Act(() => _service.MoveAsync(record.Id, -1), "OrderSaved"));
                AddButton("MoveDown", async () => await Act(() => _service.MoveAsync(record.Id, 1), "OrderSaved"));
                AddButton("Remove", async () => await Act(() => _service.RemoveAsync(record.Id), "Removed"));
                card.Children.Add(actions); _cards.Children.Add(new Border { Child = card, Padding = new Thickness(16), CornerRadius = new CornerRadius(8), Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] });
            }
        }
        catch (Exception e) { Status(() => L("ListFailed") + e.Message); }
    }
    private async Task Act(Func<Task> action, string successKey)
    {
        _busy = true;
        try { await action(); Status(() => L(successKey)); } catch (Exception e) { Status(() => L("ActionFailed") + e.Message); }
        finally { _busy = false; Render(); }
    }
    private async void Import(object sender, RoutedEventArgs e)
    {
        if (_busy) return;
        var picker = new FileOpenPicker(); picker.FileTypeFilter.Add(".zip"); InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
        try { var file = await picker.PickSingleFileAsync(); if (file is not null) await Act(async () => { _ = await _service.ImportAsync(file.Path); }, "Imported"); }
        catch (Exception error) { Status(() => L("ImportFailed") + error.Message); }
    }
}
