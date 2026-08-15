using Microsoft.UI.Xaml;

namespace BetterEndfield.UI;

public partial class App : Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        if (Environment.GetCommandLineArgs().Any(argument => argument.Equals(
                "--uninstall-xinput-silent",
                StringComparison.OrdinalIgnoreCase)))
        {
            Task.Run(Services.XInputDeploymentService.TryUninstallSavedAsync)
                .GetAwaiter()
                .GetResult();
            Exit();
            return;
        }
        _window = new MainWindow();
        _window.Activate();
    }
}
