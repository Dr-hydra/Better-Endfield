using BetterEndfieldNext.UI.Controls;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfieldNext.UI.Views;

// An isolated view for reviewing layout and ACF previews. It cannot install
// proxies, change Steam files, launch games or modify compatibility flags.
internal sealed class SteamIntegrationPreviewWindow : Window
{
    public SteamIntegrationPreviewWindow(string? metadataPath = null)
    {
        Title = "Steam setup preview";
        LocalizationService.Instance.ApplyLanguage("System");
        var page = new SteamIntegrationPage(Path.Combine(Path.GetTempPath(), "BE-SteamPreview-" + Guid.NewGuid().ToString("N")));
        var column = new PageContentPanel { Padding = new Thickness(20), MaxContentWidth = 900 };
        column.Children.Add(page);
        Content = new ScrollViewer { Content = column, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        WindowPlacementService.SetInitialSize(this, 980, 920);
        page.Initialize(new SteamIntegrationSettings { AutoUpdateMetadata = false }, () => Task.CompletedTask, () => "", () => "", () => Task.FromResult(false), preview: true);
        if (metadataPath is not null)
            page.Loaded += async (_, _) =>
            {
                try { await page.ImportPreviewMetadataAsync(metadataPath); }
                catch (Exception exception)
                {
                    await new ContentDialog { XamlRoot = page.XamlRoot, Title = "Preview metadata error", Content = exception.Message, CloseButtonText = "Close" }.ShowAsync();
                }
            };
    }
}
