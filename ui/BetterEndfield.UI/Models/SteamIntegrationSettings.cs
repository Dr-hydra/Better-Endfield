namespace BetterEndfield.UI.Models;

internal sealed class SteamIntegrationSettings
{
    public string SteamExecutablePath { get; set; } = "";
    public string LibraryPath { get; set; } = "";
    public bool AutoUpdateMetadata { get; set; } = true;
    public bool LaunchAsAdministrator { get; set; }
}
