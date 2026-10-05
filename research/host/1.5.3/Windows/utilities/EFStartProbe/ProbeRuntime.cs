using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EFStartProbe;

internal sealed class ProbeRuntime : IDisposable
{
    private readonly ManualLogSource logger;
    private readonly ConcurrentQueue<ProbeCommand> commands = new();
    private readonly ConcurrentQueue<HookObservation> hookObservations = new();
    private readonly List<PendingAssetRequest> pendingAssets = new();
    private readonly OriginTracker origins = new();
    private readonly string sessionId;
    private readonly EventSink sink;
    private readonly ProbePipeServer pipeServer;
    private readonly ObservationHooks hooks;
    private readonly Snapshotter snapshotter;
    private readonly int mainThreadId;
    private long sequence;
    private float armedUntil;
    private float nextSample;
    private int frameCount;
    private float realtime;
    private bool disposed;

    public ProbeRuntime(ManualLogSource logger)
    {
        this.logger = logger;
        mainThreadId = Thread.CurrentThread.ManagedThreadId;
        sessionId = Environment.GetEnvironmentVariable("EFSTARTPROBE_SESSION")
                    ?? $"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Environment.ProcessId}";
        var outputDirectory = Environment.GetEnvironmentVariable("EFSTARTPROBE_OUTPUT")
                              ?? System.IO.Path.Combine(System.IO.Path.GetTempPath(), "EFStartProbe", sessionId);
        sink = new EventSink(outputDirectory, logger);
        snapshotter = new Snapshotter(this, origins);
        hooks = new ObservationHooks(this, logger);
        pipeServer = new ProbePipeServer(this, logger, Environment.ProcessId);
    }

    public bool IsArmed => !disposed && Volatile.Read(ref realtime) <= Volatile.Read(ref armedUntil);

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        hooks.Install();
        pipeServer.Start();

        var autoArm = ParseAutoArmSeconds();
        if (autoArm > 0)
        {
            Arm(autoArm);
        }

        Emit("probe", "probe.started", null, new Dictionary<string, object?>
        {
            ["processId"] = Environment.ProcessId,
            ["pipe"] = pipeServer.PipeName,
            ["mainThreadId"] = mainThreadId,
            ["autoArmSeconds"] = autoArm,
            ["readOnly"] = true
        });
    }

    public void Update()
    {
        if (disposed)
        {
            return;
        }

        frameCount = Time.frameCount;
        realtime = Time.realtimeSinceStartup;
        ProcessCommands();
        ProcessHookObservations();
        ProcessPendingAssets();

        if (!IsArmed || realtime < nextSample)
        {
            return;
        }

        nextSample = realtime + 0.1f;
        var start = Stopwatch.GetTimestamp();
        snapshotter.Capture(full: false, start, TimeSpan.FromMilliseconds(4));
        var elapsedMs = (Stopwatch.GetTimestamp() - start) * 1000.0 / Stopwatch.Frequency;
        if (elapsedMs > 4)
        {
            Emit("sampler", "sample.over_budget", null, new Dictionary<string, object?>
            {
                ["elapsedMs"] = Math.Round(elapsedMs, 3),
                ["budgetMs"] = 4
            });
        }
    }

    public void EnqueueCommand(ProbeCommand command) => commands.Enqueue(command);

    public void EnqueueHook(HookObservation observation) => hookObservations.Enqueue(observation);

    public void Emit(
        string source,
        string eventType,
        ProbeObjectRef? objectRef,
        IReadOnlyDictionary<string, object?>? data,
        int? threadId = null)
    {
        sink.TryWrite(new ProbeEvent
        {
            SessionId = sessionId,
            Sequence = Interlocked.Increment(ref sequence),
            Utc = DateTimeOffset.UtcNow,
            RealtimeSinceStartup = realtime,
            FrameCount = frameCount,
            ThreadId = threadId ?? Thread.CurrentThread.ManagedThreadId,
            Source = source,
            EventType = eventType,
            Object = objectRef,
            Data = data
        });
    }

    public ProbeObjectRef? Describe(UnityEngine.Object? value)
    {
        if (value == null)
        {
            return null;
        }

        try
        {
            var gameObject = value switch
            {
                GameObject item => item,
                Component component => component.gameObject,
                _ => null
            };
            return new ProbeObjectRef
            {
                InstanceId = value.GetInstanceID(),
                Type = value.GetType().FullName ?? value.GetType().Name,
                Name = value.name ?? string.Empty,
                HierarchyPath = gameObject == null ? string.Empty : Snapshotter.GetHierarchyPath(gameObject.transform),
                Scene = gameObject == null ? string.Empty : gameObject.scene.name
            };
        }
        catch
        {
            return null;
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        hooks.Dispose();
        pipeServer.Dispose();
        sink.Dispose();
    }

    private void ProcessCommands()
    {
        while (commands.TryDequeue(out var command))
        {
            try
            {
                command.Completion.TrySetResult(ExecuteCommand(command));
            }
            catch (Exception exception)
            {
                command.Completion.TrySetResult(new ProbeCommandResult(false, Error: exception.Message));
            }
        }
    }

    private ProbeCommandResult ExecuteCommand(ProbeCommand command)
    {
        switch (command.Name.ToLowerInvariant())
        {
            case "ping":
                return new ProbeCommandResult(true, new { sessionId, processId = Environment.ProcessId });
            case "status":
                return new ProbeCommandResult(true, new
                {
                    sessionId,
                    processId = Environment.ProcessId,
                    armed = IsArmed,
                    frameCount,
                    realtimeSinceStartup = realtime,
                    scene = SceneManager.GetActiveScene().name,
                    droppedEvents = sink.Dropped,
                    pendingHooks = hookObservations.Count,
                    pendingAssets = pendingAssets.Count
                });
            case "arm":
                var seconds = ReadInt(command.Args, "durationSeconds", 90);
                if (seconds is < 1 or > 3600)
                {
                    return new ProbeCommandResult(false, Error: "durationSeconds must be between 1 and 3600.");
                }

                Arm(seconds);
                return new ProbeCommandResult(true, new { armedUntil });
            case "snapshot":
                var start = Stopwatch.GetTimestamp();
                snapshotter.Capture(full: true, start, TimeSpan.FromMilliseconds(250));
                return new ProbeCommandResult(true, new { frameCount });
            case "mark":
                var label = ReadString(command.Args, "label") ?? string.Empty;
                Emit("control", "capture.mark", null, new Dictionary<string, object?> { ["label"] = label });
                return new ProbeCommandResult(true, new { label });
            case "stop":
                armedUntil = -1;
                Emit("control", "capture.stopped", null, null);
                return new ProbeCommandResult(true, new { stopped = true });
            default:
                return new ProbeCommandResult(false, Error: $"Unknown read-only command: {command.Name}");
        }
    }

    private void Arm(int seconds)
    {
        armedUntil = realtime + seconds;
        nextSample = realtime;
        Emit("control", "capture.armed", null, new Dictionary<string, object?>
        {
            ["durationSeconds"] = seconds,
            ["armedUntil"] = armedUntil
        });
    }

    private void ProcessHookObservations()
    {
        var processed = 0;
        while (processed < 2_000 && hookObservations.TryDequeue(out var observation))
        {
            processed++;
            try
            {
                ProcessHookObservation(observation);
            }
            catch (Exception exception)
            {
                logger.LogDebug($"Could not flatten hook observation: {exception.Message}");
            }
        }
    }

    private void ProcessHookObservation(HookObservation observation)
    {
        var methodName = $"{observation.Method.DeclaringType?.FullName}.{observation.Method.Name}";
        var argumentText = observation.Args.Select(FlattenArgument).ToArray();
        var origin = BuildOrigin(methodName, argumentText);

        switch (observation.Kind)
        {
            case HookObservationKind.Resource:
                ProcessResourceResult(observation.Result, origin, observation.ThreadId);
                break;
            case HookObservationKind.Instantiate:
                var original = observation.Args.FirstOrDefault() as UnityEngine.Object;
                var clone = observation.Result as UnityEngine.Object;
                var cloneOrigin = origins.Get(original) ?? origin;
                origins.Register(clone, cloneOrigin);
                var root = clone is Component component
                    ? Snapshotter.FindCandidateRoot(component.transform)
                    : clone is GameObject gameObject
                        ? Snapshotter.FindCandidateRoot(gameObject.transform)
                        : null;
                Emit("hook", "object.instantiate", Describe(clone), new Dictionary<string, object?>
                {
                    ["candidateRootInstanceId"] = root == null ? 0 : root.gameObject.GetInstanceID(),
                    ["method"] = methodName,
                    ["origin"] = cloneOrigin,
                    ["original"] = original == null ? null : original.name,
                    ["arguments"] = argumentText
                }, observation.ThreadId);
                break;
            case HookObservationKind.Animation:
                var target = observation.Instance as UnityEngine.Object;
                var targetTransform = target switch
                {
                    Component targetComponent => targetComponent.transform,
                    GameObject targetGameObject => targetGameObject.transform,
                    _ => null
                };
                var candidateRoot = targetTransform == null ? null : Snapshotter.FindCandidateRoot(targetTransform);
                Emit("hook", "animation.call", Describe(target), new Dictionary<string, object?>
                {
                    ["candidateRootInstanceId"] = candidateRoot == null ? 0 : candidateRoot.gameObject.GetInstanceID(),
                    ["method"] = methodName,
                    ["arguments"] = argumentText
                }, observation.ThreadId);
                break;
        }
    }

    private void ProcessResourceResult(object? result, string origin, int sourceThreadId)
    {
        if (result is AssetBundleRequest request)
        {
            pendingAssets.Add(new PendingAssetRequest { Origin = origin, Request = request });
            Emit("hook", "resource.requested", null, new Dictionary<string, object?>
            {
                ["candidateRootInstanceId"] = 0,
                ["origin"] = origin,
                ["async"] = true
            }, sourceThreadId);
            return;
        }

        if (result is UnityEngine.Object value)
        {
            origins.Register(value, origin);
            Emit("hook", "resource.loaded", Describe(value), new Dictionary<string, object?>
            {
                ["candidateRootInstanceId"] = 0,
                ["origin"] = origin,
                ["async"] = false
            }, sourceThreadId);
        }
    }

    private void ProcessPendingAssets()
    {
        for (var index = pendingAssets.Count - 1; index >= 0; index--)
        {
            var pending = pendingAssets[index];
            if (pending.Request == null || !pending.Request.isDone)
            {
                continue;
            }

            pendingAssets.RemoveAt(index);
            try
            {
                var values = pending.Request.allAssets;
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        origins.Register(value, pending.Origin);
                        Emit("hook", "resource.loaded", Describe(value), new Dictionary<string, object?>
                        {
                            ["candidateRootInstanceId"] = 0,
                            ["origin"] = pending.Origin,
                            ["async"] = true
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogDebug($"Could not read completed AssetBundleRequest: {exception.Message}");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Emit("scene", "scene.loaded", null, new Dictionary<string, object?>
        {
            ["scene"] = scene.name,
            ["buildIndex"] = scene.buildIndex,
            ["mode"] = mode.ToString()
        });
    }

    private void OnSceneUnloaded(Scene scene)
    {
        Emit("scene", "scene.unloaded", null, new Dictionary<string, object?>
        {
            ["scene"] = scene.name,
            ["buildIndex"] = scene.buildIndex
        });
    }

    private void OnActiveSceneChanged(Scene previous, Scene next)
    {
        Emit("scene", "scene.active_changed", null, new Dictionary<string, object?>
        {
            ["previous"] = previous.name,
            ["next"] = next.name
        });
    }

    private static string BuildOrigin(string methodName, IReadOnlyList<string?> arguments)
    {
        var meaningful = arguments.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        return string.IsNullOrWhiteSpace(meaningful) ? methodName : $"{methodName}:{meaningful}";
    }

    private static string? FlattenArgument(object? value)
    {
        return value switch
        {
            null => null,
            string text => text,
            bool boolean => boolean.ToString(CultureInfo.InvariantCulture),
            byte or sbyte or short or ushort or int or uint or long or ulong or float or double or decimal =>
                Convert.ToString(value, CultureInfo.InvariantCulture),
            Enum item => item.ToString(),
            UnityEngine.Object unityObject => $"{unityObject.GetType().Name}#{unityObject.GetInstanceID()}:{unityObject.name}",
            Type type => type.FullName,
            _ => value.GetType().FullName
        };
    }

    private static int ParseAutoArmSeconds()
    {
        var raw = Environment.GetEnvironmentVariable("EFSTARTPROBE_AUTO_ARM_SECONDS");
        return int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)
            ? Math.Clamp(value, 0, 3600)
            : 90;
    }

    private static int ReadInt(JsonElement args, string name, int fallback)
    {
        return args.ValueKind == JsonValueKind.Object &&
               args.TryGetProperty(name, out var node) &&
               node.TryGetInt32(out var value)
            ? value
            : fallback;
    }

    private static string? ReadString(JsonElement args, string name)
    {
        return args.ValueKind == JsonValueKind.Object && args.TryGetProperty(name, out var node)
            ? node.GetString()
            : null;
    }
}
