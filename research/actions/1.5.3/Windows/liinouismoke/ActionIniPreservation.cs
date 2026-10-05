namespace BetterEndfield.UI.Services;

internal static class ActionIniPreservation
{
    // No UI control owns this experiment. Preserve explicit native opt-in/opt-out.
    internal static string PreserveExternalLoop(string existing, string replacement)
    {
        string? value = ReadExternalLoop(existing);
        if (value is null || ReadExternalLoop(replacement) is not null) return replacement;
        int offset = 0;
        foreach (string line in replacement.Split('\n'))
        {
            if (line.Trim().Equals("[betterendfield.actions]", StringComparison.OrdinalIgnoreCase))
            {
                string newline = replacement.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : "\n";
                int insert = offset + line.Length;
                if (insert < replacement.Length)
                    return replacement.Insert(insert + 1, "external_loop=" + value + newline);
                return replacement + newline + "external_loop=" + value + newline;
            }
            offset += line.Length + 1;
        }
        return replacement;
    }

    private static string? ReadExternalLoop(string text)
    {
        bool actions = false;
        string? value = null;
        using var reader = new StringReader(text);
        while (reader.ReadLine() is { } raw)
        {
            string line = raw.Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
                actions = line.Equals("[betterendfield.actions]", StringComparison.OrdinalIgnoreCase);
            else if (actions)
            {
                int separator = line.IndexOf('=');
                if (separator > 0 && line[..separator].Trim().Equals("external_loop", StringComparison.OrdinalIgnoreCase))
                    value = line[(separator + 1)..].Trim();
            }
        }
        return value;
    }
}
