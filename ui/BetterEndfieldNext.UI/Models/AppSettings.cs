using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetterEndfieldNext.UI.Models;

internal sealed class AppSettings
{
    public string GameExecutablePath { get; set; } = string.Empty;

    public string OfficialLauncherPath { get; set; } = string.Empty;

    public List<string> OfficialLauncherHistory { get; set; } = new();

    public bool BypassOfficialLauncher { get; set; } = false;

    public string LoaderMode { get; set; } = "none";

    public string GameLaunchArguments { get; set; } = string.Empty;

    public string Theme { get; set; } = "Default";

    public string Language { get; set; } = "System";

    public bool GachaEnabled { get; set; } = false;

    public string DisclaimerAcceptedVersion { get; set; } = string.Empty;

    public SteamIntegrationSettings SteamIntegration { get; set; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalSettings { get; set; }
}
