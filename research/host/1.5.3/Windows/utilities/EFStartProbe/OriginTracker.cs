using System;
using System.Collections.Generic;
using UnityEngine;

namespace EFStartProbe;

internal sealed class OriginTracker
{
    private readonly Dictionary<int, string> origins = new();

    public void Register(UnityEngine.Object? value, string origin)
    {
        if (value == null || string.IsNullOrWhiteSpace(origin))
        {
            return;
        }

        try
        {
            origins[value.GetInstanceID()] = origin;
        }
        catch
        {
            // Unity object may have been destroyed before the hook was drained.
        }
    }

    public string? Get(UnityEngine.Object? value)
    {
        if (value == null)
        {
            return null;
        }

        try
        {
            return origins.TryGetValue(value.GetInstanceID(), out var origin) ? origin : null;
        }
        catch
        {
            return null;
        }
    }

    public string? FindForRenderer(Renderer renderer, UnityEngine.Object? asset)
    {
        var direct = Get(asset) ?? Get(renderer) ?? Get(renderer.gameObject);
        if (!string.IsNullOrWhiteSpace(direct))
        {
            return direct;
        }

        var current = renderer.transform;
        while (current != null)
        {
            var origin = Get(current.gameObject) ?? Get(current);
            if (!string.IsNullOrWhiteSpace(origin))
            {
                return origin;
            }

            current = current.parent;
        }

        return null;
    }
}

