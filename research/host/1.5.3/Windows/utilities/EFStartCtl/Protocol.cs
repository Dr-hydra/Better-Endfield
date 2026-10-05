using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFStartCtl;

public static class ProbeProtocol
{
    public const int SchemaVersion = 1;

    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };
}

public sealed record ProbeRequest(
    string Id,
    string Command,
    IReadOnlyDictionary<string, object?>? Args = null);

public sealed record ProbeResponse(
    string Id,
    bool Ok,
    object? Result = null,
    string? Error = null);

public sealed class CandidateEvidence
{
    public int RootInstanceId { get; init; }
    public string Name { get; set; } = string.Empty;
    public string HierarchyPath { get; set; } = string.Empty;
    public bool PlayingDirector { get; set; }
    public bool AnimatorChanged { get; set; }
    public bool VisibleRenderer { get; set; }
    public bool CreatedInWindow { get; set; }
    public bool HasResourceOrigin { get; set; }
    public bool SilhouetteMaterial { get; set; }
    public HashSet<string> Meshes { get; } = new(StringComparer.Ordinal);
    public HashSet<string> Animations { get; } = new(StringComparer.Ordinal);
    public HashSet<string> Materials { get; } = new(StringComparer.Ordinal);
    public HashSet<string> Origins { get; } = new(StringComparer.Ordinal);

    public int Score =>
        (PlayingDirector ? 50 : 0) +
        (AnimatorChanged ? 30 : 0) +
        (VisibleRenderer ? 20 : 0) +
        (CreatedInWindow ? 15 : 0) +
        (HasResourceOrigin ? 15 : 0) +
        (SilhouetteMaterial ? 10 : 0);
}

