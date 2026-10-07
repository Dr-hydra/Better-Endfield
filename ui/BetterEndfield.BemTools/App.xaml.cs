using BetterEndfield.UI.Services;
using BetterEndfield.UI.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfield.BemTools;

public partial class App : Application
{
    private BemConverterWindow? _window;

    public App()
    {
        UnhandledException += (_, args) => WriteCrashLog(args.Exception);
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        LocalizationService.Instance.ApplyLanguage("System");
        _window = new BemConverterWindow(AppContext.BaseDirectory, standalone: true);
        _window.Closed += (_, _) => Exit();
        _window.Activate();

        string[] arguments = Environment.GetCommandLineArgs().Skip(1).ToArray();
        if (arguments.Length == 0) return;
        _window.DispatcherQueue.TryEnqueue(async () =>
        {
            try
            {
                if (arguments.Length != 1)
                    throw new ArgumentException(LocalizationService.Instance.IsChinese
                        ? "请提供一个 .bemproj.json 导出工程路径。"
                        : "Provide one .bemproj.json export project path.");
                _window.OpenProject(arguments[0]);
            }
            catch (Exception exception)
            {
                await new ContentDialog
                {
                    XamlRoot = ((FrameworkElement)_window.Content).XamlRoot,
                    Title = BemText.Get("操作未完成"),
                    Content = exception.Message,
                    CloseButtonText = BemText.Get("返回任务")
                }.ShowAsync();
            }
        });
    }

    private static void WriteCrashLog(Exception exception)
    {
        try
        {
            string directory = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "BEMTools");
            Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, "crash.log"), exception.ToString());
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}
