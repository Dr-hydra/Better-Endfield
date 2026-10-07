using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;

namespace BetterEndfield.UI;

public sealed partial class MainWindow
{
    private void InitializeSteamIntegration()
    {
        _appSettings.SteamIntegration ??= new SteamIntegrationSettings();
        SteamIntegrationPage.Initialize(_appSettings.SteamIntegration,
            () => ConfigurationService.SaveAppSettingsAsync(_appSettings),
            () => GamePathBox.Text.Trim(), () => GameLaunchArgumentsBox.Text.Trim(), PrepareSteamXInputAsync);
    }

    private async Task<bool> PrepareSteamXInputAsync()
    {
        var processes = System.Diagnostics.Process.GetProcessesByName("Endfield");
        bool running = processes.Length > 0;
        foreach (var process in processes) process.Dispose();
        if (running) throw new SteamIntegrationException("game_running");
        SelectLoaderMode("xinput");
        if (!await SaveAsync(showSuccess: false)) return false;
        await XInputDeploymentService.InstallAsync(GamePathBox.Text.Trim(), RuntimePathDiscoveryService.BundledInjectorPath);
        await RefreshXInputStatusAsync();
        return true;
    }
}
