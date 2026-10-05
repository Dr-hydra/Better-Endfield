using System.Text;

namespace BetterEndfield.UI.Services;

// Shared with the native model overlay. Lock only the small settings transaction.
internal static class BemRuntimeSettings
{
    internal const string MutexName = @"Local\BetterEndfield.CustomModel.Settings";
    private const int MaximumBytes = 1024 * 1024;

    internal static Dictionary<string, Dictionary<string, string>> Read(string path)
    {
        var result = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);
        if (!File.Exists(path)) return result;
        if (new FileInfo(path).Length > MaximumBytes) throw new IOException("Model settings exceed 1 MiB.");
        string section = "";
        foreach (string source in File.ReadLines(path, new UTF8Encoding(false, true)))
        {
            string line = source.Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                section = line[1..^1];
                result.TryAdd(section, new(StringComparer.Ordinal));
            }
            else if (line.IndexOf('=') is int split && split >= 0 && result.TryGetValue(section, out var values))
                values[line[..split].Trim()] = line[(split + 1)..].Trim();
        }
        return result;
    }

    internal static Dictionary<string, Dictionary<string, string>> Clone(Dictionary<string, Dictionary<string, string>> source) =>
        source.ToDictionary(pair => pair.Key, pair => new Dictionary<string, string>(pair.Value, StringComparer.Ordinal), StringComparer.Ordinal);

    internal static Dictionary<string, Dictionary<string, string>> Merge(
        Dictionary<string, Dictionary<string, string>> latest,
        Dictionary<string, Dictionary<string, string>> baseline,
        Dictionary<string, Dictionary<string, string>> desired,
        IReadOnlySet<string>? forcedEnabledIds,
        Dictionary<string, Dictionary<string, string>>? loadedDisk = null, bool disableAll = false)
    {
        var result = Clone(latest);
        foreach (var (section, values) in desired)
        {
            baseline.TryGetValue(section, out var before);
            Dictionary<string, string>? originallyRead = null;
            loadedDisk?.TryGetValue(section, out originallyRead);
            if (!result.TryGetValue(section, out var current)) result[section] = current = new(StringComparer.Ordinal);
            foreach (var (key, value) in values)
            {
                bool forced = key == "enabled" && section.StartsWith("Mod.", StringComparison.Ordinal)
                    && forcedEnabledIds?.Contains(section[4..]) == true;
                bool normalizeUnchangedDisk = originallyRead != null && originallyRead.TryGetValue(key, out string? diskValue)
                    && current.GetValueOrDefault(key) == diskValue;
                if (forced || before == null || !before.TryGetValue(key, out string? old) || old != value || !current.ContainsKey(key) || normalizeUnchangedDisk)
                    current[key] = value;
            }
            if (before != null)
                foreach (string removed in before.Keys.Except(values.Keys)) current.Remove(removed);
            if (originallyRead != null)
                foreach (string removed in originallyRead.Keys.Except(values.Keys))
                    if (current.GetValueOrDefault(removed) == originallyRead[removed]) current.Remove(removed);
        }
        foreach (string removed in baseline.Keys.Except(desired.Keys)) result.Remove(removed);
        if (disableAll)
            foreach (var (section, values) in result)
                if (section.StartsWith("Mod.", StringComparison.Ordinal)) values["enabled"] = "false";
        return result;
    }

    internal static (Dictionary<string, Dictionary<string, string>> Settings, (long Ticks, long Size) Stamp) Commit(string path,
        Dictionary<string, Dictionary<string, string>> baseline,
        Dictionary<string, Dictionary<string, string>> desired,
        IReadOnlySet<string>? forcedEnabledIds,
        Dictionary<string, Dictionary<string, string>>? loadedDisk = null, bool disableAll = false)
    {
        using var mutex = new Mutex(false, MutexName);
        bool acquired = false;
        try
        {
            try { acquired = mutex.WaitOne(TimeSpan.FromSeconds(3)); }
            catch (AbandonedMutexException) { acquired = true; }
            if (!acquired) throw new IOException("Model settings are busy.");
            var merged = Merge(Read(path), baseline, desired, forcedEnabledIds, loadedDisk, disableAll);
            var text = new StringBuilder();
            foreach (var (section, values) in merged)
            {
                text.Append('[').Append(section).Append("]\n");
                foreach (var (key, value) in values) text.Append(key).Append('=').Append(value).Append('\n');
                text.Append('\n');
            }
            byte[] bytes = new UTF8Encoding(false).GetBytes(text.ToString());
            if (bytes.Length > MaximumBytes) throw new IOException("Model settings exceed 1 MiB.");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            string temporary = Path.Combine(Path.GetDirectoryName(path)!, Guid.NewGuid() + ".tmp");
            try { File.WriteAllBytes(temporary, bytes); File.Move(temporary, path, true); }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
            var file = new FileInfo(path);
            return (merged, (file.LastWriteTimeUtc.Ticks, file.Length));
        }
        finally { if (acquired) mutex.ReleaseMutex(); }
    }
}
