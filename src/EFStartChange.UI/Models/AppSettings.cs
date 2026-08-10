namespace EFStartChange.UI.Models;

internal sealed class AppSettings
{
    public string GameExecutablePath { get; set; } = string.Empty;

    public string MapperPath { get; set; } = string.Empty;

    public string Theme { get; set; } = "Default";

    public string DisclaimerAcceptedVersion { get; set; } = string.Empty;
}
