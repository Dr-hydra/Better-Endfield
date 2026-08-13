namespace EFStartChange.UI.Models;

internal sealed class AppSettings
{
    public string GameExecutablePath { get; set; } = string.Empty;

    public string MapperPath { get; set; } = string.Empty;

    public bool ExternalLoaderEnabled { get; set; }

    public string ExternalLoaderPath { get; set; } = string.Empty;

    public string ExternalLoaderArguments { get; set; } = string.Empty;

    public double ExternalLoaderDelaySeconds { get; set; } = 2.0;

    public string Theme { get; set; } = "Default";

    public string DisclaimerAcceptedVersion { get; set; } = string.Empty;
}
