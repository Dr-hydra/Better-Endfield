using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfieldNext.UI;

public sealed partial class MainWindow
{
    private const string WorkshopUrl = "https://146.235.16.65:8443/endfield/";

    private void OpenWorkshop()
    {
        try
        {
            OpenWithShell(WorkshopUrl);
        }
        catch (Exception exception)
        {
            ShowStatus(
                LocalizationService.Instance.IsChinese ? "无法打开创意工坊" : "Could not open Workshop",
                exception.Message,
                InfoBarSeverity.Error);
        }
    }
}
