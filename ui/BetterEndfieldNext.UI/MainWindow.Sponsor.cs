using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;

namespace BetterEndfieldNext.UI;

public sealed partial class MainWindow
{
    private const string SponsorImageResourceName =
        "BetterEndfieldNext.UI.Assets.sponsor.wechat-appreciation.png";
    private bool _sponsorDialogOpen;

    private static Stream OpenSponsorImage() => typeof(MainWindow).Assembly
        .GetManifestResourceStream(SponsorImageResourceName)
        ?? throw new IOException(LocalizationService.Instance.IsChinese
            ? "找不到微信赞赏图片。" : "The WeChat appreciation image is unavailable.");

    private async void FeatureNavigation_ItemInvoked(
        NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer?.Tag as string == "workshop")
        {
            OpenWorkshop();
            return;
        }
        if (args.InvokedItemContainer?.Tag as string != "sponsor" || _sponsorDialogOpen) return;
        _sponsorDialogOpen = true;
        bool isZh = LocalizationService.Instance.IsChinese;
        var feedback = new InfoBar { IsOpen = false, IsClosable = true };
        void Report(string title, string message, InfoBarSeverity severity)
        {
            feedback.Title = title;
            feedback.Message = message;
            feedback.Severity = severity;
            feedback.IsOpen = true;
        }

        try
        {
            var image = new Image
            {
                MaxWidth = 300,
                MaxHeight = 360,
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(
                image, isZh ? "微信赞赏码" : "WeChat appreciation code");
            try
            {
                using Stream source = OpenSponsorImage();
                using var memory = new MemoryStream();
                source.CopyTo(memory);
                memory.Position = 0;
                using var randomAccess = memory.AsRandomAccessStream();
                var bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(randomAccess);
                image.Source = bitmap;
            }
            catch (Exception exception)
            {
                Report(isZh ? "图片加载失败" : "Could not load image",
                    exception.Message, InfoBarSeverity.Error);
            }

            var save = new Button
            {
                Content = isZh ? "保存赞赏码" : "Save appreciation code",
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            save.Click += async (_, _) =>
            {
                save.IsEnabled = false;
                try
                {
                    var picker = new FileSavePicker { SuggestedFileName = "wechat-appreciation" };
                    picker.FileTypeChoices.Add(isZh ? "PNG 图片" : "PNG image", new List<string> { ".png" });
                    WinRT.Interop.InitializeWithWindow.Initialize(
                        picker, WinRT.Interop.WindowNative.GetWindowHandle(this));
                    var file = await picker.PickSaveFileAsync();
                    if (file is null) return;
                    using Stream source = OpenSponsorImage();
                    using FileStream output = File.Create(file.Path);
                    await source.CopyToAsync(output);
                    await output.FlushAsync();
                    Report(isZh ? "已保存赞赏码" : "Appreciation code saved",
                        file.Path, InfoBarSeverity.Success);
                }
                catch (Exception exception)
                {
                    Report(isZh ? "保存图片失败" : "Could not save image",
                        exception.Message, InfoBarSeverity.Error);
                }
                finally { save.IsEnabled = true; }
            };

            Button Link(string label, string url)
            {
                var button = new Button { Content = label, HorizontalAlignment = HorizontalAlignment.Stretch };
                button.Click += (_, _) =>
                {
                    try { OpenWithShell(url); }
                    catch (Exception exception)
                    {
                        Report(isZh ? "打开链接失败" : "Could not open link",
                            exception.Message, InfoBarSeverity.Error);
                    }
                };
                return button;
            }

            var content = new StackPanel { Spacing = 12 };
            content.Children.Add(new TextBlock
            {
                Text = isZh ? "微信赞赏" : "WeChat appreciation",
                FontSize = 18,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            content.Children.Add(image);
            content.Children.Add(save);
            content.Children.Add(Link(isZh ? "爱发电" : "Afdian",
                "https://afdian.com/u/e9a7e6ac6fa411ed980752540025c377"));
            content.Children.Add(Link("PayPal", "https://paypal.me/hydra405"));
            content.Children.Add(feedback);

            var dialog = new ContentDialog
            {
                XamlRoot = MainRoot.XamlRoot,
                RequestedTheme = MainRoot.ActualTheme,
                Language = LocalizationService.Instance.EffectiveLanguage,
                Title = isZh ? "给作者充一点token" : "Give the author some tokens",
                CloseButtonText = isZh ? "关闭" : "Close",
                DefaultButton = ContentDialogButton.Close,
                Content = new ScrollViewer
                {
                    MaxHeight = Math.Clamp(MainRoot.ActualHeight - 200, 120, 600),
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    Content = content
                }
            };
            await dialog.ShowAsync();
        }
        catch (Exception exception)
        {
            ShowStatus(isZh ? "无法打开赞助面板" : "Could not open support panel",
                exception.Message, InfoBarSeverity.Error);
        }
        finally { _sponsorDialogOpen = false; }
    }
}
