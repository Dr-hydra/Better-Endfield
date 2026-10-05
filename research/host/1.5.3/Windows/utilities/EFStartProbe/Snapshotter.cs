using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

namespace EFStartProbe;

internal sealed class Snapshotter
{
    private static readonly string[] CommonColorProperties = { "_Color", "_BaseColor", "_TintColor" };
    private static readonly string[] SilhouetteTerms = { "silhouette", "outline", "shadow", "black", "mask" };

    private readonly ProbeRuntime runtime;
    private readonly OriginTracker origins;
    private readonly Dictionary<int, int> animatorStateHashes = new();

    public Snapshotter(ProbeRuntime runtime, OriginTracker origins)
    {
        this.runtime = runtime;
        this.origins = origins;
    }

    public void Capture(bool full, long startTimestamp, TimeSpan budget)
    {
        var truncated = false;

        try
        {
            foreach (var director in Resources.FindObjectsOfTypeAll<PlayableDirector>())
            {
                if (Expired(startTimestamp, budget))
                {
                    truncated = true;
                    break;
                }

                CaptureDirector(director);
            }

            if (!truncated)
            {
                foreach (var animator in Resources.FindObjectsOfTypeAll<Animator>())
                {
                    if (Expired(startTimestamp, budget))
                    {
                        truncated = true;
                        break;
                    }

                    CaptureAnimator(animator);
                }
            }

            if (!truncated)
            {
                foreach (var renderer in Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>())
                {
                    if (Expired(startTimestamp, budget))
                    {
                        truncated = true;
                        break;
                    }

                    CaptureSkinnedRenderer(renderer);
                }
            }

            if (!truncated)
            {
                foreach (var renderer in Resources.FindObjectsOfTypeAll<MeshRenderer>())
                {
                    if (Expired(startTimestamp, budget))
                    {
                        truncated = true;
                        break;
                    }

                    CaptureMeshRenderer(renderer);
                }
            }

            if (full && !truncated)
            {
                CaptureLegacyAnimations(startTimestamp, budget, ref truncated);
                CaptureSpecialComponents(startTimestamp, budget, ref truncated);
            }
        }
        catch (Exception exception)
        {
            runtime.Emit("sampler", "sample.failed", null, new Dictionary<string, object?>
            {
                ["error"] = exception.Message,
                ["full"] = full
            });
        }

        if (truncated)
        {
            runtime.Emit("sampler", "sample.truncated", null, new Dictionary<string, object?>
            {
                ["budgetMs"] = budget.TotalMilliseconds,
                ["full"] = full
            });
        }
    }

    public static string GetHierarchyPath(Transform? transform)
    {
        if (transform == null)
        {
            return string.Empty;
        }

        var names = new List<string>(16);
        var current = transform;
        while (current != null && names.Count < 64)
        {
            names.Add(current.name ?? string.Empty);
            current = current.parent;
        }

        names.Reverse();
        return string.Join("/", names);
    }

    public static Transform? FindCandidateRoot(Transform? transform)
    {
        if (transform == null)
        {
            return null;
        }

        var current = transform;
        while (current != null)
        {
            if (current.GetComponent<PlayableDirector>() != null || current.GetComponent<Animator>() != null)
            {
                return current;
            }

            current = current.parent;
        }

        return transform.root ?? transform;
    }

    private void CaptureDirector(PlayableDirector? director)
    {
        if (director == null || !director.gameObject.activeInHierarchy)
        {
            return;
        }

        var root = FindCandidateRoot(director.transform) ?? director.transform;
        var data = new Dictionary<string, object?>
        {
            ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
            ["state"] = director.state.ToString(),
            ["time"] = Math.Round(director.time, 4),
            ["duration"] = Math.Round(director.duration, 4),
            ["playableAsset"] = director.playableAsset == null ? null : director.playableAsset.name,
            ["playableAssetType"] = director.playableAsset == null ? null : director.playableAsset.GetType().FullName,
            ["bindings"] = DescribeBindings(director)
        };
        runtime.Emit("sampler", "director.snapshot", runtime.Describe(director), data);
    }

    private void CaptureAnimator(Animator? animator)
    {
        if (animator == null || !animator.enabled || !animator.gameObject.activeInHierarchy)
        {
            return;
        }

        var root = FindCandidateRoot(animator.transform) ?? animator.transform;
        var stateHash = 0;
        var normalizedTime = 0f;
        var clips = new List<string>();

        try
        {
            if (animator.layerCount > 0)
            {
                var state = animator.GetCurrentAnimatorStateInfo(0);
                stateHash = state.fullPathHash;
                normalizedTime = state.normalizedTime;
                foreach (var clip in animator.GetCurrentAnimatorClipInfo(0))
                {
                    if (clip.clip != null)
                    {
                        clips.Add($"{clip.clip.name}@{clip.weight:0.###}");
                    }
                }
            }
        }
        catch
        {
            // Some custom Animator implementations reject state access during initialization.
        }

        var instanceId = animator.GetInstanceID();
        var changed = animatorStateHashes.TryGetValue(instanceId, out var previous) && previous != stateHash;
        animatorStateHashes[instanceId] = stateHash;

        var controllerClips = new List<string>();
        var controller = animator.runtimeAnimatorController;
        if (controller != null)
        {
            try
            {
                foreach (var clip in controller.animationClips)
                {
                    if (clip != null)
                    {
                        controllerClips.Add(clip.name);
                    }
                }
            }
            catch
            {
                // A stripped controller may not expose its complete clip array.
            }
        }

        runtime.Emit("sampler", "animator.snapshot", runtime.Describe(animator), new Dictionary<string, object?>
        {
            ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
            ["controller"] = controller == null ? null : controller.name,
            ["avatar"] = animator.avatar == null ? null : animator.avatar.name,
            ["layerCount"] = animator.layerCount,
            ["applyRootMotion"] = animator.applyRootMotion,
            ["speed"] = animator.speed,
            ["stateHash"] = stateHash,
            ["normalizedTime"] = Math.Round(normalizedTime, 4),
            ["changed"] = changed,
            ["clips"] = clips.Count == 0 ? controllerClips.Distinct(StringComparer.Ordinal).ToArray() : clips.ToArray()
        });
    }

    private void CaptureSkinnedRenderer(SkinnedMeshRenderer? renderer)
    {
        if (!ShouldCapture(renderer))
        {
            return;
        }

        var mesh = renderer!.sharedMesh;
        var root = FindCandidateRoot(renderer.transform) ?? renderer.transform;
        var materialData = DescribeMaterials(renderer.sharedMaterials);
        var bones = renderer.bones;
        var bonePaths = new List<string>();
        if (bones != null)
        {
            foreach (var bone in bones)
            {
                if (bone != null)
                {
                    bonePaths.Add(GetHierarchyPath(bone));
                }
            }
        }

        runtime.Emit("sampler", "renderer.snapshot", runtime.Describe(renderer), new Dictionary<string, object?>
        {
            ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
            ["rendererType"] = nameof(SkinnedMeshRenderer),
            ["enabled"] = renderer.enabled,
            ["visible"] = renderer.isVisible,
            ["mesh"] = mesh == null ? null : mesh.name,
            ["vertexCount"] = mesh == null ? 0 : mesh.vertexCount,
            ["subMeshCount"] = mesh == null ? 0 : mesh.subMeshCount,
            ["blendShapeCount"] = mesh == null ? 0 : mesh.blendShapeCount,
            ["bindPoseCount"] = mesh == null ? 0 : mesh.bindposes.Length,
            ["rootBone"] = renderer.rootBone == null ? null : GetHierarchyPath(renderer.rootBone),
            ["boneCount"] = bonePaths.Count,
            ["boneHash"] = HashStrings(bonePaths),
            ["materials"] = materialData.Names,
            ["shaders"] = materialData.Shaders,
            ["silhouetteLike"] = materialData.SilhouetteLike,
            ["origin"] = origins.FindForRenderer(renderer, mesh),
            ["bounds"] = DescribeBounds(renderer.bounds)
        });
    }

    private void CaptureMeshRenderer(MeshRenderer? renderer)
    {
        if (!ShouldCapture(renderer))
        {
            return;
        }

        var filter = renderer!.GetComponent<MeshFilter>();
        var mesh = filter == null ? null : filter.sharedMesh;
        var root = FindCandidateRoot(renderer.transform) ?? renderer.transform;
        var materialData = DescribeMaterials(renderer.sharedMaterials);
        runtime.Emit("sampler", "renderer.snapshot", runtime.Describe(renderer), new Dictionary<string, object?>
        {
            ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
            ["rendererType"] = nameof(MeshRenderer),
            ["enabled"] = renderer.enabled,
            ["visible"] = renderer.isVisible,
            ["mesh"] = mesh == null ? null : mesh.name,
            ["vertexCount"] = mesh == null ? 0 : mesh.vertexCount,
            ["subMeshCount"] = mesh == null ? 0 : mesh.subMeshCount,
            ["blendShapeCount"] = mesh == null ? 0 : mesh.blendShapeCount,
            ["materials"] = materialData.Names,
            ["shaders"] = materialData.Shaders,
            ["silhouetteLike"] = materialData.SilhouetteLike,
            ["origin"] = origins.FindForRenderer(renderer, mesh),
            ["bounds"] = DescribeBounds(renderer.bounds)
        });
    }

    private void CaptureLegacyAnimations(long startTimestamp, TimeSpan budget, ref bool truncated)
    {
        foreach (var animation in Resources.FindObjectsOfTypeAll<Animation>())
        {
            if (Expired(startTimestamp, budget))
            {
                truncated = true;
                return;
            }

            if (animation == null || !animation.gameObject.activeInHierarchy)
            {
                continue;
            }

            var root = FindCandidateRoot(animation.transform) ?? animation.transform;
            runtime.Emit("sampler", "animation.snapshot", runtime.Describe(animation), new Dictionary<string, object?>
            {
                ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
                ["isPlaying"] = animation.isPlaying,
                ["clip"] = animation.clip == null ? null : animation.clip.name
            });
        }
    }

    private void CaptureSpecialComponents(long startTimestamp, TimeSpan budget, ref bool truncated)
    {
        foreach (var component in Resources.FindObjectsOfTypeAll<Component>())
        {
            if (Expired(startTimestamp, budget))
            {
                truncated = true;
                return;
            }

            if (component == null || !component.gameObject.activeInHierarchy)
            {
                continue;
            }

            var typeName = component.GetType().FullName ?? component.GetType().Name;
            if (!ContainsAny(typeName, "Alembic", "Slate", "Cutscene", "Timeline", "USD"))
            {
                continue;
            }

            var root = FindCandidateRoot(component.transform) ?? component.transform;
            runtime.Emit("sampler", "special_component.snapshot", runtime.Describe(component), new Dictionary<string, object?>
            {
                ["candidateRootInstanceId"] = root.gameObject.GetInstanceID(),
                ["componentType"] = typeName
            });
        }
    }

    private static bool ShouldCapture(Renderer? renderer)
    {
        return renderer != null && renderer.enabled && renderer.gameObject.activeInHierarchy;
    }

    private static IReadOnlyList<object> DescribeBindings(PlayableDirector director)
    {
        var results = new List<object>();
        var asset = director.playableAsset;
        if (asset == null)
        {
            return results;
        }

        try
        {
            foreach (var output in asset.outputs)
            {
                var bound = director.GetGenericBinding(output.sourceObject);
                results.Add(new
                {
                    stream = output.streamName,
                    targetType = output.outputTargetType?.FullName,
                    source = output.sourceObject == null ? null : output.sourceObject.name,
                    binding = bound == null ? null : bound.name
                });
            }
        }
        catch
        {
            // Some third-party PlayableAssets expose malformed output enumerators.
        }

        return results;
    }

    private static (string[] Names, string[] Shaders, bool SilhouetteLike) DescribeMaterials(Material[]? materials)
    {
        if (materials == null || materials.Length == 0)
        {
            return (Array.Empty<string>(), Array.Empty<string>(), false);
        }

        var names = new List<string>(materials.Length);
        var shaders = new List<string>(materials.Length);
        var silhouetteLike = false;

        foreach (var material in materials)
        {
            if (material == null)
            {
                continue;
            }

            var name = material.name ?? string.Empty;
            var shaderName = material.shader == null ? string.Empty : material.shader.name ?? string.Empty;
            names.Add(name);
            shaders.Add(shaderName);
            silhouetteLike |= ContainsAny(name, SilhouetteTerms) || ContainsAny(shaderName, SilhouetteTerms);

            foreach (var property in CommonColorProperties)
            {
                if (!material.HasProperty(property))
                {
                    continue;
                }

                var color = material.GetColor(property);
                var luminance = 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
                silhouetteLike |= color.a >= 0.5f && luminance <= 0.15f;
            }
        }

        return (
            names.Distinct(StringComparer.Ordinal).ToArray(),
            shaders.Distinct(StringComparer.Ordinal).ToArray(),
            silhouetteLike);
    }

    private static object DescribeBounds(Bounds bounds)
    {
        return new
        {
            center = new[] { bounds.center.x, bounds.center.y, bounds.center.z },
            size = new[] { bounds.size.x, bounds.size.y, bounds.size.z }
        };
    }

    private static string HashStrings(IReadOnlyList<string> values)
    {
        if (values.Count == 0)
        {
            return string.Empty;
        }

        using var hash = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(string.Join("\n", values));
        return Convert.ToHexString(hash.ComputeHash(bytes));
    }

    private static bool ContainsAny(string value, params string[] terms)
    {
        return terms.Any(term => value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    private static bool Expired(long startTimestamp, TimeSpan budget)
    {
        var elapsedSeconds = (Stopwatch.GetTimestamp() - startTimestamp) / (double)Stopwatch.Frequency;
        return elapsedSeconds >= budget.TotalSeconds;
    }
}

