using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetterEndfieldNext.UI.Models;

internal sealed class AppSettings
{
    public string GameExecutablePath { get; set; } = string.Empty;

    public string LoaderMode { get; set; } = "injector";

    public string GameLaunchArguments { get; set; } = string.Empty;

    public string Theme { get; set; } = "Default";

    public string Language { get; set; } = "System";

    public bool GachaEnabled { get; set; } = false;

    public string DisclaimerAcceptedVersion { get; set; } = string.Empty;

    public SteamIntegrationSettings SteamIntegration { get; set; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalSettings { get; set; }
}
