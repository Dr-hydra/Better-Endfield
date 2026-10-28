using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;

namespace BetterEndfieldNext.UI;

public sealed partial class MainWindow
{
    private CancellationTokenSource? _resourceUpdateCancellation;

    private void LocalizeResourceControls(bool zh)
    {
        ResourceUpdateButton.Content = zh ? "更新基础资源" : "Update core resources";
        PreviewResourceButton.Content = zh ? "下载预览资源" : "Download preview resources";
        ImportResourceButton.Content = zh ? "导入资源包 / 回退" : "Import / roll back resources";
        ResumeResourceUpdatesButton.Content = zh ? "恢复资源自动更新" : "Resume resource auto-update";
        CancelResourceUpdateButton.Content = zh ? "取消下载" : "Cancel download";
    }

    private string? ResourceGameVersion()
    {
        // Automatic upgrades never guess across game baselines. A newly installed
        // baseline can be selected through manual import, including rollback.
        var core = GameResourceStore.Resolve("core");
        return core is not null && core.Manifest.TryGetProperty("gameVersion", out var value) ? value.GetString() : null;
    }

    private async Task RunResourceUpdateAsync(bool preview, bool resume)
    {
        if (_resourceUpdateCancellation is not null) return;
        using var cancellation = new CancellationTokenSource(); _resourceUpdateCancellation = cancellation;
        bool zh = LocalizationService.Instance.IsChinese;
        ResourceUpdateButton.IsEnabled = PreviewResourceButton.IsEnabled = ImportResourceButton.IsEnabled = ResumeResourceUpdatesButton.IsEnabled = false;
        CancelResourceUpdateButton.IsEnabled = true;
        ResourceUpdateStatus.Text = zh ? "正在下载并校验资源…" : "Downloading and validating resources…";
        try
        {
            string status = await Task.Run(() => DistributionService.UpdateResourcesAsync(ResourceGameVersion(), preview, resume, cancellation.Token));
            ResourceUpdateStatus.Text = (zh ? "资源检查完成；已更新的基础资源需重启程序及游戏后生效。\n" : "Resource check complete; restart the app and game to load updated core resources.\n") + status;
        }
        catch (OperationCanceledException) { ResourceUpdateStatus.Text = zh ? "已取消，原资源保持可用。" : "Cancelled; existing resources remain available."; }
        catch (Exception error) { ResourceUpdateStatus.Text = (zh ? "资源更新失败：" : "Resource update failed: ") + error.Message; }
        finally
        {
            _resourceUpdateCancellation = null;
            ResourceUpdateButton.IsEnabled = PreviewResourceButton.IsEnabled = ImportResourceButton.IsEnabled = ResumeResourceUpdatesButton.IsEnabled = true;
            CancelResourceUpdateButton.IsEnabled = false;
        }
    }

    private async void ResourceUpdateButton_Click(object sender, RoutedEventArgs e) => await RunResourceUpdateAsync(false, false);
    private async void PreviewResourceButton_Click(object sender, RoutedEventArgs e) => await RunResourceUpdateAsync(true, false);
    private async void ResumeResourceUpdatesButton_Click(object sender, RoutedEventArgs e) => await RunResourceUpdateAsync(false, true);
    private void CancelResourceUpdateButton_Click(object sender, RoutedEventArgs e) => _resourceUpdateCancellation?.Cancel();

    private async void ImportResourceButton_Click(object sender, RoutedEventArgs e)
    {
        if (_resourceUpdateCancellation is not null) return;
        var picker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.Downloads };
        picker.FileTypeFilter.Add(".zip");
        WinRT.Interop.InitializeWithWindow.Initialize(picker, WinRT.Interop.WindowNative.GetWindowHandle(this));
        var file = await picker.PickSingleFileAsync(); if (file is null) return;
        using var cancellation = new CancellationTokenSource(); _resourceUpdateCancellation = cancellation;
        bool zh = LocalizationService.Instance.IsChinese;
        ResourceUpdateButton.IsEnabled = PreviewResourceButton.IsEnabled = ImportResourceButton.IsEnabled = ResumeResourceUpdatesButton.IsEnabled = false;
        CancelResourceUpdateButton.IsEnabled = true;
        ResourceUpdateStatus.Text = zh ? "正在导入并校验资源…" : "Importing and validating resources…";
        try
        {
            string result = await Task.Run(() => DistributionService.ImportAsync(file.Path, cancellation.Token));
            ResourceUpdateStatus.Text = (zh ? "已启用并固定版本；重启后生效：" : "Activated and pinned; restart to apply: ") + result;
        }
        catch (Exception error) { ResourceUpdateStatus.Text = (zh ? "导入失败：" : "Import failed: ") + error.Message; }
        finally
        {
            _resourceUpdateCancellation = null;
            ResourceUpdateButton.IsEnabled = PreviewResourceButton.IsEnabled = ImportResourceButton.IsEnabled = ResumeResourceUpdatesButton.IsEnabled = true;
            CancelResourceUpdateButton.IsEnabled = false;
        }
    }
}
