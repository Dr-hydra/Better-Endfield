using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Playables;

namespace EFStartProbe;

internal sealed class ObservationHooks : IDisposable
{
    private static ProbeRuntime? runtime;

    private readonly Harmony harmony = new("dev.drhydra.efstartprobe.observation");
    private readonly ManualLogSource logger;
    private readonly HashSet<MethodBase> patched = new();
    private bool disposed;

    public ObservationHooks(ProbeRuntime value, ManualLogSource logger)
    {
        runtime = value;
        this.logger = logger;
    }

    public int Install()
    {
        PatchPostfix(
            typeof(Resources),
            method => method.IsStatic && method.Name.StartsWith("Load", StringComparison.Ordinal),
            nameof(ResourcePostfix));
        PatchPostfix(
            typeof(AssetBundle),
            method => method.Name.StartsWith("Load", StringComparison.Ordinal),
            nameof(ResourcePostfix));
        PatchPostfix(
            typeof(UnityEngine.Object),
            method => method.IsStatic && method.Name == "Instantiate",
            nameof(InstantiatePostfix));

        PatchPrefix(
            typeof(Animator),
            method => method.Name is "Play" or "CrossFade" or "CrossFadeInFixedTime" or "Rebind",
            nameof(AnimationPrefix));
        PatchPrefix(
            typeof(PlayableDirector),
            method => method.Name is "Play" or "Stop" or "Evaluate",
            nameof(AnimationPrefix));

        logger.LogInfo($"Installed {patched.Count} read-only observation hook(s).");
        return patched.Count;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        try
        {
            harmony.UnpatchSelf();
        }
        catch (Exception exception)
        {
            logger.LogWarning($"Could not remove all observation hooks: {exception.Message}");
        }

        runtime = null;
    }

    private void PatchPostfix(Type type, Func<MethodInfo, bool> predicate, string patchName)
    {
        var patch = new HarmonyMethod(typeof(ObservationHooks), patchName);
        foreach (var method in GetPatchableMethods(type, predicate))
        {
            TryPatch(method, null, patch);
        }
    }

    private void PatchPrefix(Type type, Func<MethodInfo, bool> predicate, string patchName)
    {
        var patch = new HarmonyMethod(typeof(ObservationHooks), patchName);
        foreach (var method in GetPatchableMethods(type, predicate))
        {
            TryPatch(method, patch, null);
        }
    }

    private static IEnumerable<MethodInfo> GetPatchableMethods(Type type, Func<MethodInfo, bool> predicate)
    {
        return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(method => !method.IsAbstract && !method.ContainsGenericParameters && predicate(method))
            .GroupBy(method => $"{method.Module.ModuleVersionId}:{method.MetadataToken}", StringComparer.Ordinal)
            .Select(group => group.First());
    }

    private void TryPatch(MethodInfo method, HarmonyMethod? prefix, HarmonyMethod? postfix)
    {
        if (!patched.Add(method))
        {
            return;
        }

        try
        {
            harmony.Patch(method, prefix, postfix);
        }
        catch (Exception exception)
        {
            patched.Remove(method);
            logger.LogDebug($"Skipped hook {method.DeclaringType?.FullName}.{method.Name}: {exception.Message}");
        }
    }

    private static void ResourcePostfix(MethodBase __originalMethod, object? __instance, object?[] __args, object? __result)
    {
        Enqueue(HookObservationKind.Resource, __originalMethod, __instance, __args, __result);
    }

    private static void InstantiatePostfix(MethodBase __originalMethod, object?[] __args, object? __result)
    {
        Enqueue(HookObservationKind.Instantiate, __originalMethod, null, __args, __result);
    }

    private static void AnimationPrefix(MethodBase __originalMethod, object? __instance, object?[] __args)
    {
        Enqueue(HookObservationKind.Animation, __originalMethod, __instance, __args, null);
    }

    private static void Enqueue(
        HookObservationKind kind,
        MethodBase method,
        object? instance,
        object?[] args,
        object? result)
    {
        var current = runtime;
        if (current is null || !current.IsArmed)
        {
            return;
        }

        current.EnqueueHook(new HookObservation
        {
            Kind = kind,
            Method = method,
            Instance = instance,
            Args = args,
            Result = result,
            ThreadId = Thread.CurrentThread.ManagedThreadId
        });
    }
}

