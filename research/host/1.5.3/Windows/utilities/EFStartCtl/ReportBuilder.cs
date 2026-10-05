using System.Text;
using System.Text.Json;

namespace EFStartCtl;

public static class ReportBuilder
{
    public static IReadOnlyList<CandidateEvidence> Build(string runDirectory)
    {
        var eventsPath = Path.Combine(runDirectory, "events.jsonl");
        if (!File.Exists(eventsPath))
        {
            throw new FileNotFoundException("events.jsonl was not found.", eventsPath);
        }

        var candidates = new Dictionary<int, CandidateEvidence>();

        foreach (var line in File.ReadLines(eventsPath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            using var document = JsonDocument.Parse(line);
            Consume(document.RootElement, candidates);
        }

        var ranked = candidates.Values
            .OrderByDescending(candidate => candidate.Score)
            .ThenBy(candidate => candidate.HierarchyPath, StringComparer.Ordinal)
            .Take(3)
            .ToArray();

        WriteSummary(runDirectory, ranked);
        WriteMarkdown(runDirectory, ranked);
        return ranked;
    }

    private static void Consume(
        JsonElement root,
        IDictionary<int, CandidateEvidence> candidates)
    {
        var eventType = GetString(root, "eventType");
        if (!root.TryGetProperty("data", out var data) || data.ValueKind != JsonValueKind.Object)
        {
            return;
        }

        var rootId = GetInt32(data, "candidateRootInstanceId");
        if (rootId == 0)
        {
            return;
        }

        if (!candidates.TryGetValue(rootId, out var candidate))
        {
            candidate = new CandidateEvidence { RootInstanceId = rootId };
            candidates.Add(rootId, candidate);
        }

        if (root.TryGetProperty("object", out var objectNode))
        {
            candidate.Name = GetString(objectNode, "name") ?? candidate.Name;
            candidate.HierarchyPath = GetString(objectNode, "hierarchyPath") ?? candidate.HierarchyPath;
        }

        switch (eventType)
        {
            case "director.snapshot":
                candidate.PlayingDirector |= string.Equals(
                    GetString(data, "state"), "Playing", StringComparison.OrdinalIgnoreCase);
                Add(candidate.Animations, GetString(data, "playableAsset"));
                break;
            case "animator.snapshot":
                candidate.AnimatorChanged |= GetBoolean(data, "changed");
                Add(candidate.Animations, GetString(data, "controller"));
                AddArray(candidate.Animations, data, "clips");
                break;
            case "renderer.snapshot":
                candidate.VisibleRenderer |= GetBoolean(data, "visible");
                candidate.SilhouetteMaterial |= GetBoolean(data, "silhouetteLike");
                candidate.HasResourceOrigin |= !string.IsNullOrWhiteSpace(GetString(data, "origin"));
                Add(candidate.Meshes, GetString(data, "mesh"));
                AddArray(candidate.Materials, data, "materials");
                Add(candidate.Origins, GetString(data, "origin"));
                break;
            case "object.instantiate":
                candidate.CreatedInWindow = true;
                Add(candidate.Origins, GetString(data, "origin"));
                break;
            case "resource.loaded":
                candidate.HasResourceOrigin = true;
                Add(candidate.Origins, GetString(data, "origin"));
                break;
        }
    }

    private static void WriteSummary(string runDirectory, IReadOnlyList<CandidateEvidence> candidates)
    {
        var summary = new
        {
            schemaVersion = ProbeProtocol.SchemaVersion,
            generatedUtc = DateTimeOffset.UtcNow,
            candidates = candidates.Select(candidate => new
            {
                candidate.RootInstanceId,
                candidate.Name,
                candidate.HierarchyPath,
                candidate.Score,
                candidate.PlayingDirector,
                candidate.AnimatorChanged,
                candidate.VisibleRenderer,
                candidate.CreatedInWindow,
                candidate.HasResourceOrigin,
                candidate.SilhouetteMaterial,
                meshes = candidate.Meshes.Order(StringComparer.Ordinal),
                animations = candidate.Animations.Order(StringComparer.Ordinal),
                materials = candidate.Materials.Order(StringComparer.Ordinal),
                origins = candidate.Origins.Order(StringComparer.Ordinal)
            })
        };

        var path = Path.Combine(runDirectory, "summary.json");
        File.WriteAllText(
            path,
            JsonSerializer.Serialize(summary, new JsonSerializerOptions(ProbeProtocol.JsonOptions)
            {
                WriteIndented = true
            }));
    }

    private static void WriteMarkdown(string runDirectory, IReadOnlyList<CandidateEvidence> candidates)
    {
        var output = new StringBuilder();
        output.AppendLine("# Endfield opening silhouette candidates");
        output.AppendLine();
        output.AppendLine("This report verifies the current account state only. Other account states remain inferred and untested.");
        output.AppendLine();

        if (candidates.Count == 0)
        {
            output.AppendLine("No candidate accumulated enough correlated runtime evidence.");
        }

        foreach (var candidate in candidates)
        {
            output.AppendLine($"## Score {candidate.Score}: {Escape(candidate.HierarchyPath)}");
            output.AppendLine();
            output.AppendLine($"- Instance ID: `{candidate.RootInstanceId}`");
            output.AppendLine($"- Playing Timeline: `{candidate.PlayingDirector}`");
            output.AppendLine($"- Animator changed: `{candidate.AnimatorChanged}`");
            output.AppendLine($"- Visible renderer: `{candidate.VisibleRenderer}`");
            output.AppendLine($"- Created in window: `{candidate.CreatedInWindow}`");
            output.AppendLine($"- Resource origin: `{candidate.HasResourceOrigin}`");
            output.AppendLine($"- Silhouette-like material: `{candidate.SilhouetteMaterial}`");
            AppendValues(output, "Meshes", candidate.Meshes);
            AppendValues(output, "Animations", candidate.Animations);
            AppendValues(output, "Materials", candidate.Materials);
            AppendValues(output, "Origins", candidate.Origins);
            output.AppendLine();
        }

        File.WriteAllText(Path.Combine(runDirectory, "candidate-report.md"), output.ToString());
    }

    private static void AppendValues(StringBuilder output, string label, IEnumerable<string> values)
    {
        var joined = string.Join(", ", values.Order(StringComparer.Ordinal).Select(value => $"`{Escape(value)}`"));
        output.AppendLine($"- {label}: {(joined.Length == 0 ? "none" : joined)}");
    }

    private static string Escape(string value) => value.Replace("`", "'", StringComparison.Ordinal);

    private static string? GetString(JsonElement element, string name)
    {
        return element.TryGetProperty(name, out var node) && node.ValueKind == JsonValueKind.String
            ? node.GetString()
            : null;
    }

    private static int GetInt32(JsonElement element, string name)
    {
        return element.TryGetProperty(name, out var node) && node.TryGetInt32(out var value) ? value : 0;
    }

    private static bool GetBoolean(JsonElement element, string name)
    {
        return element.TryGetProperty(name, out var node) &&
               (node.ValueKind == JsonValueKind.True ||
                (node.ValueKind == JsonValueKind.String && bool.TryParse(node.GetString(), out var value) && value));
    }

    private static void Add(ISet<string> values, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            values.Add(value);
        }
    }

    private static void AddArray(ISet<string> values, JsonElement data, string name)
    {
        if (!data.TryGetProperty(name, out var node) || node.ValueKind != JsonValueKind.Array)
        {
            return;
        }

        foreach (var item in node.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.String)
            {
                Add(values, item.GetString());
            }
        }
    }
}

