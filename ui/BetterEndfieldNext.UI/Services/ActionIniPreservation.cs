namespace BetterEndfieldNext.UI.Services;

internal static class ActionIniPreservation
{
    // No UI control owns these native settings. Preserve explicit opt-in/opt-out.
    // verbose is the hand-added second log level; a launcher save must not drop it.
    private static readonly string[] NativeKeys = ["external_loop", "jsspsi_quality", "verbose"];

    internal static string PreserveNativeSettings(string existing, string replacement)
    {
        foreach (string key in NativeKeys)
            replacement = PreserveKey(existing, replacement, key);
        return replacement;
    }

    private static string PreserveKey(string existing, string replacement, string key)
    {
        string? value = ReadKey(existing, key);
        if (value is null || ReadKey(replacement, key) is not null) return replacement;
        int offset = 0;
        foreach (string line in replacement.Split('\n'))
        {
            if (line.Trim().Equals("[betterendfieldnext.actions]", StringComparison.OrdinalIgnoreCase))
            {
                string newline = replacement.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : "\n";
                int insert = offset + line.Length;
                if (insert < replacement.Length)
                    return replacement.Insert(insert + 1, key + "=" + value + newline);
                return replacement + newline + key + "=" + value + newline;
            }
            offset += line.Length + 1;
        }
        return replacement;
    }

    private static string? ReadKey(string text, string key)
    {
        bool actions = false;
        string? value = null;
        using var reader = new StringReader(text);
        while (reader.ReadLine() is { } raw)
        {
            string line = raw.Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
                actions = line.Equals("[betterendfieldnext.actions]", StringComparison.OrdinalIgnoreCase);
            else if (actions)
            {
                int separator = line.IndexOf('=');
                if (separator > 0 && line[..separator].Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    value = line[(separator + 1)..].Trim();
            }
        }
        return value;
    }
}
