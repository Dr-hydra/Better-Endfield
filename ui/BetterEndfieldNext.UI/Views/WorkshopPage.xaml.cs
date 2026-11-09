using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace BetterEndfieldNext.UI.Views;

public sealed partial class WorkshopPage : UserControl
{
    internal const string DirectUrl = "https://146.235.16.65:8443/endfield/";
    internal const string DomainUrl = "https://endfield.03250799.xyz/endfield/";

    public WorkshopPage()
    {
        InitializeComponent();
        LocalizationService.Instance.PropertyChanged += LocalizationChanged;
        UpdateLocalizedUi();
    }

    private void LocalizationChanged(object? sender, PropertyChangedEventArgs e) =>
        DispatcherQueue.TryEnqueue(UpdateLocalizedUi);

    private void UpdateLocalizedUi()
    {
        bool isZh = LocalizationService.Instance.IsChinese;
        string open = isZh ? "打开" : "Open";
        string copy = isZh ? "复制" : "Copy";

        PageTitleText.Text = isZh ? "创意工坊" : "Workshop";
        DirectTitleText.Text = isZh ? "直连" : "Direct";
        DomainTitleText.Text = isZh ? "域名" : "Domain";
        DirectUrlBox.Text = DirectUrl;
        DomainUrlBox.Text = DomainUrl;
        DirectOpenButton.Content = open;
        DomainOpenButton.Content = open;
        DirectCopyButton.Content = copy;
        DomainCopyButton.Content = copy;
    }

    private void DirectOpenButton_Click(object sender, RoutedEventArgs e) => Open(DirectUrl);

    private void DomainOpenButton_Click(object sender, RoutedEventArgs e) => Open(DomainUrl);

    private async void DirectCopyButton_Click(object sender, RoutedEventArgs e) =>
        await CopyAsync(DirectUrl, DirectCopyButton);

    private async void DomainCopyButton_Click(object sender, RoutedEventArgs e) =>
        await CopyAsync(DomainUrl, DomainCopyButton);

    private async Task CopyAsync(string url, Button button)
    {
        try
        {
            var package = new DataPackage();
            package.SetText(url);
            Clipboard.SetContent(package);
            button.Content = LocalizationService.Instance.IsChinese ? "已复制" : "Copied";
            await Task.Delay(1500);
            UpdateLocalizedUi();
        }
        catch (Exception)
        {
            // 剪贴板被其它进程占用时忽略；地址本身仍可手动选中复制。
        }
    }

    private static void Open(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
        catch (Exception)
        {
            // 系统没有可用的默认浏览器时不做额外提示。
        }
    }
}
