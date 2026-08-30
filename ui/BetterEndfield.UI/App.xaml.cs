using Microsoft.UI.Xaml;
using System.Text.Json;

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
        string[] arguments = Environment.GetCommandLineArgs().Skip(1).ToArray();
        if (TryRunOmniMixCommand(arguments))
        {
            return;
        }
        // --uninstall-xinput-silent 是旧名，已安装版本的卸载日志里记的是它，必须继续接受。
        if (arguments.Any(argument =>
                argument.Equals("--uninstall-game-files-silent", StringComparison.OrdinalIgnoreCase) ||
                argument.Equals("--uninstall-xinput-silent", StringComparison.OrdinalIgnoreCase)))
        {
            Task.Run(async () =>
            {
                await Services.XInputDeploymentService.TryUninstallSavedAsync();
                await Services.OptiScalerDeploymentService.TryUninstallSavedAsync();
            }).GetAwaiter().GetResult();
            Exit();
            return;
        }
        _window = new MainWindow();
        _window.Activate();
    }

    private static bool TryRunOmniMixCommand(string[] arguments)
    {
        int registerIndex = Array.FindIndex(arguments, argument => argument.Equals(
            "--register-omnimix-backend", StringComparison.OrdinalIgnoreCase));
        int registerCount = arguments.Count(argument => argument.Equals(
            "--register-omnimix-backend", StringComparison.OrdinalIgnoreCase));
        int queryCount = arguments.Count(argument => argument.Equals(
            "--query-omnimix-registration", StringComparison.OrdinalIgnoreCase));
        int unregisterCount = arguments.Count(argument => argument.Equals(
            "--unregister-omnimix", StringComparison.OrdinalIgnoreCase));
        bool query = queryCount != 0;
        bool unregister = unregisterCount != 0;
        if (registerIndex < 0 && !query && !unregister)
        {
            return false;
        }

        int commandCount = registerCount + queryCount + unregisterCount;
        if (commandCount != 1)
        {
            ExitCommand(2, new { schemaVersion = 1, error = "conflicting_commands" });
            return true;
        }

        try
        {
            Services.OmniMixRegistrationStatus status;
            if (registerIndex >= 0)
            {
                if (registerIndex + 1 >= arguments.Length ||
                    arguments[registerIndex + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    ExitCommand(2, new { schemaVersion = 1, error = "path_missing" });
                    return true;
                }
                bool validShape = registerIndex == 0 &&
                    (arguments.Length == 2 ||
                        (arguments.Length == 3 && arguments[2].Equals(
                            "--silent", StringComparison.OrdinalIgnoreCase)));
                if (!validShape)
                {
                    ExitCommand(2, new { schemaVersion = 1, error = "unknown_argument" });
                    return true;
                }
                status = Task.Run(() =>
                    Services.OmniMixRegistrationService.RegisterAsync(
                        arguments[registerIndex + 1])).GetAwaiter().GetResult();
            }
            else if (query)
            {
                bool validShape = arguments.Length == 1 ||
                    (arguments.Length == 2 && arguments[1].Equals(
                        "--silent", StringComparison.OrdinalIgnoreCase));
                if (!validShape)
                {
                    ExitCommand(2, new { schemaVersion = 1, error = "unknown_argument" });
                    return true;
                }
                status = Task.Run(
                    Services.OmniMixRegistrationService.QueryAsync)
                    .GetAwaiter().GetResult();
            }
            else
            {
                bool validShape = arguments.Length == 1 ||
                    (arguments.Length == 2 && arguments[1].Equals(
                        "--silent", StringComparison.OrdinalIgnoreCase));
                if (!validShape)
                {
                    ExitCommand(2, new { schemaVersion = 1, error = "unknown_argument" });
                    return true;
                }
                status = Task.Run(
                    Services.OmniMixRegistrationService.UnregisterAsync)
                    .GetAwaiter().GetResult();
            }
            ExitCommand(0, status);
        }
        catch (Services.OmniMixRegistrationException exception)
        {
            ExitCommand(exception.ExitCode, new
            {
                schemaVersion = 1,
                error = exception.Reason,
                message = exception.Message
            });
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or InvalidDataException)
        {
            ExitCommand(4, new
            {
                schemaVersion = 1,
                error = "configuration_io_failed",
                message = exception.Message
            });
        }
        catch (Exception exception)
        {
            ExitCommand(10, new
            {
                schemaVersion = 1,
                error = "internal_error",
                message = exception.Message
            });
        }
        return true;
    }

    private static void ExitCommand(int exitCode, object payload)
    {
        try
        {
            using Stream standardOutput = Console.OpenStandardOutput();
            using var writer = new StreamWriter(standardOutput) { AutoFlush = true };
            writer.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
        catch (IOException)
        {
        }
        Environment.Exit(exitCode);
    }
}
