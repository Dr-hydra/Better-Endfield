using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EFStartProbe;

internal static class ProbeJson
{
    public const int SchemaVersion = 1;

    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };
}

internal sealed class ProbeEvent
{
    public int SchemaVersion { get; init; } = ProbeJson.SchemaVersion;
    public string SessionId { get; init; } = string.Empty;
    public long Sequence { get; init; }
    public DateTimeOffset Utc { get; init; }
    public float RealtimeSinceStartup { get; init; }
    public int FrameCount { get; init; }
    public int ThreadId { get; init; }
    public string Source { get; init; } = string.Empty;
    public string EventType { get; init; } = string.Empty;

    [JsonPropertyName("object")]
    public ProbeObjectRef? Object { get; init; }

    public IReadOnlyDictionary<string, object?>? Data { get; init; }
}

internal sealed class ProbeObjectRef
{
    public int InstanceId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string HierarchyPath { get; init; } = string.Empty;
    public string Scene { get; init; } = string.Empty;
}

internal sealed class ProbeCommand
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public JsonElement Args { get; init; }
    public TaskCompletionSource<ProbeCommandResult> Completion { get; } =
        new(TaskCreationOptions.RunContinuationsAsynchronously);
}

internal sealed record ProbeCommandResult(bool Ok, object? Result = null, string? Error = null);

internal enum HookObservationKind
{
    Resource,
    Instantiate,
    Animation
}

internal sealed class HookObservation
{
    public HookObservationKind Kind { get; init; }
    public MethodBase Method { get; init; } = null!;
    public object? Instance { get; init; }
    public object?[] Args { get; init; } = Array.Empty<object?>();
    public object? Result { get; init; }
    public int ThreadId { get; init; }
}

internal sealed class PendingAssetRequest
{
    public string Origin { get; init; } = string.Empty;
    public UnityEngine.AssetBundleRequest Request { get; init; } = null!;
}

