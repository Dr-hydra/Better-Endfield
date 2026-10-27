using System.Text.Json;

namespace BetterEndfieldNext.UI.Services;

internal sealed record BemComponentBinding(int Id, string Name, string Resource, string RendererPath)
{
    public string FullName => string.Join("\n", new[] { Name, Resource, RendererPath }.Where(s => s.Length > 0));
}

// Read author rules, not component-name heuristics. A shared option has one
// binding list; availability dependencies, draw predicates and texture slots
// all contribute. These are possible associations, not a current-state claim.
internal static class BemOptionBindings
{
    private static HashSet<string> References(JsonElement value)
    {
        var ids = new HashSet<string>(StringComparer.Ordinal);
        void Visit(JsonElement node)
        {
            if (node.ValueKind == JsonValueKind.Object)
            {
                if (node.TryGetProperty("eq", out var eq) && eq.ValueKind == JsonValueKind.Array && eq.GetArrayLength() == 2)
                    ids.Add(eq[0].GetString() ?? "");
                else foreach (var property in node.EnumerateObject()) Visit(property.Value);
            }
            else if (node.ValueKind == JsonValueKind.Array) foreach (var child in node.EnumerateArray()) Visit(child);
        }
        Visit(value); return ids;
    }

    public static List<BemComponentBinding> Components(JsonElement manifest)
    {
        var target = manifest.GetProperty("target");
        var resources = new Dictionary<string, string>(StringComparer.Ordinal);
        if (target.TryGetProperty("resources", out var roots))
            foreach (var root in roots.EnumerateArray())
                resources[root.GetProperty("id").GetString() ?? ""] = root.GetProperty("name").GetString() ?? "";
        if (!target.TryGetProperty("components", out var componentList)) return [];
        return componentList.EnumerateArray().Select(c => new BemComponentBinding(
            c.GetProperty("id").GetInt32(), c.GetProperty("mesh_name").GetString() ?? "",
            c.TryGetProperty("resource", out var resource) ? resources.GetValueOrDefault(resource.GetString() ?? "", "") : "world / UI",
            c.TryGetProperty("renderer_path", out var path) ? path.GetString() ?? "" : "")).ToList();
    }

    public static Dictionary<string, IReadOnlyList<BemComponentBinding>> Read(JsonElement manifest)
    {
        var result = new Dictionary<string, IReadOnlyList<BemComponentBinding>>(StringComparer.Ordinal);
        if (!manifest.TryGetProperty("option_groups", out var groups) || groups.GetArrayLength() == 0) return result;
        var dependencies = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
        var bound = new Dictionary<string, HashSet<int>>(StringComparer.Ordinal);
        foreach (var group in groups.EnumerateArray())
        {
            string id = group.GetProperty("id").GetString() ?? "";
            dependencies[id] = group.TryGetProperty("available_when", out var condition) ? References(condition) : [];
            bound[id] = [];
        }
        var slots = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
        if (manifest.TryGetProperty("texture_slots", out var textureSlots))
            foreach (var slot in textureSlots.EnumerateArray())
                slots[slot.GetProperty("id").GetString() ?? ""] = References(slot.GetProperty("candidates"));
        var components = Components(manifest);
        var meshes = manifest.GetProperty("meshes");
        foreach (var rule in manifest.GetProperty("component_rules").EnumerateArray())
        {
            int target = rule.GetProperty("target").GetInt32();
            var refs = References(rule.GetProperty("candidates"));
            void Textures(JsonElement node)
            {
                if (node.ValueKind == JsonValueKind.Object)
                {
                    if (node.TryGetProperty("slot", out var slot) && slots.TryGetValue(slot.GetString() ?? "", out var ids)) refs.UnionWith(ids);
                    foreach (var property in node.EnumerateObject()) Textures(property.Value);
                }
                else if (node.ValueKind == JsonValueKind.Array) foreach (var child in node.EnumerateArray()) Textures(child);
            }
            foreach (var candidate in rule.GetProperty("candidates").EnumerateArray())
            {
                Textures(candidate);
                if (candidate.TryGetProperty("mesh", out var mesh))
                {
                    var draws = meshes[mesh.GetInt32()].GetProperty("draws");
                    refs.UnionWith(References(draws)); Textures(draws);
                }
            }
            var pending = new Stack<string>(refs);
            var visited = new HashSet<string>(StringComparer.Ordinal);
            while (pending.TryPop(out var id))
                if (visited.Add(id) && bound.TryGetValue(id, out var ids))
                {
                    ids.Add(target);
                    foreach (string parent in dependencies[id]) pending.Push(parent);
                }
        }
        foreach (var (id, ids) in bound) result[id] = components.Where(c => ids.Contains(c.Id)).ToList();
        return result;
    }
}
