namespace BetterEndfield.UI.Models;

internal sealed class AppSettings
{
    public string GameExecutablePath { get; set; } = string.Empty;

    public string InjectorPath { get; set; } = string.Empty;

    public string LoaderMode { get; set; } = "injector";

    public string GameLaunchArguments { get; set; } = string.Empty;

    public string Theme { get; set; } = "Default";

    public string Language { get; set; } = "System";

    public string DisclaimerAcceptedVersion { get; set; } = string.Empty;
}
