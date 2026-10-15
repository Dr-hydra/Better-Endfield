using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;

namespace BetterEndfieldNext.UI;

public sealed partial class MainWindow
{
    private void InitializeSteamIntegration()
    {
        _appSettings.SteamIntegration ??= new SteamIntegrationSettings();
        SteamIntegrationPage.Initialize(_appSettings.SteamIntegration,
            () => ConfigurationService.SaveAppSettingsAsync(_appSettings),
            () => GamePathBox.Text.Trim(), () => GameLaunchArgumentsBox.Text.Trim(),
            GetSelectedLoaderMode, PrepareSteamLaunchAsync);
    }

    private Task EnsureSelectedLoaderReadyAsync() => GameLaunchService.EnsureLoaderReadyAsync(
        GetSelectedLoaderMode(), GamePathBox.Text.Trim(), async () =>
        {
            var status = await XInputDeploymentService.InspectAsync(
                GamePathBox.Text.Trim(), RuntimePathDiscoveryService.BundledInjectorPath);
            if (status.State == XInputDeploymentState.Conflict)
                throw new InvalidOperationException(status.Message);
            return status.State == XInputDeploymentState.Installed;
        });

    private async Task<bool> PrepareSteamLaunchAsync()
    {
        var processes = System.Diagnostics.Process.GetProcessesByName("Endfield");
        bool running = processes.Length > 0;
        foreach (var process in processes) process.Dispose();
        if (running) throw new SteamIntegrationException("game_running");
        SteamIntegrationService.GenerateLaunchCommand(
            GameLaunchService.SelectHelperPath(GetSelectedLoaderMode(), RuntimePathDiscoveryService.BundledInjectorPath,
                RuntimePathDiscoveryService.BundledLauncherPath), GamePathBox.Text.Trim(), GameLaunchArgumentsBox.Text.Trim());
        await EnsureSelectedLoaderReadyAsync();
        return await SaveAsync(showSuccess: false);
    }
}
