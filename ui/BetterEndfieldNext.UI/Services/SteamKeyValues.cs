using System.Text;

namespace BetterEndfieldNext.UI.Services;

// Steam's text KeyValues format. Keep unknown fields and insertion order when
// updating an ACF; never edit another application's configuration with regexes.
internal sealed class SteamKeyValues
{
    private sealed record Entry(string Key, string? Value, SteamKeyValues? Object);
    private readonly List<Entry> _entries = [];

    public string? String(string key) => _entries.FirstOrDefault(e => e.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Value;
    public SteamKeyValues? Object(string key) => _entries.FirstOrDefault(e => e.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Object;
    public IEnumerable<(string Key, SteamKeyValues Value)> Objects => _entries.Where(e => e.Object is not null).Select(e => (e.Key, e.Object!));
    public void Set(string key, string value) => Put(new Entry(key, value, null));
    public void Set(string key, SteamKeyValues value) => Put(new Entry(key, null, value));

    private void Put(Entry entry)
    {
        int index = _entries.FindIndex(e => e.Key.Equals(entry.Key, StringComparison.OrdinalIgnoreCase));
        _entries.RemoveAll(e => e.Key.Equals(entry.Key, StringComparison.OrdinalIgnoreCase));
        _entries.Insert(index < 0 ? _entries.Count : index, entry);
    }

    public static SteamKeyValues Parse(string text)
    {
        if (text.Length > 4 * 1024 * 1024) throw new InvalidDataException("Steam KeyValues file is too large.");
        int position = 0;
        string? Token()
        {
            while (position < text.Length)
            {
                if (char.IsWhiteSpace(text[position]) || text[position] == '\uFEFF') { position++; continue; }
                if (text[position] == '/' && position + 1 < text.Length && text[position + 1] == '/')
                { while (position < text.Length && text[position] != '\n') position++; continue; }
                break;
            }
            if (position == text.Length) return null;
            char first = text[position++];
            if (first is '{' or '}') return first.ToString();
            var token = new StringBuilder();
            if (first != '"')
            {
                token.Append(first);
                while (position < text.Length && !char.IsWhiteSpace(text[position]) && text[position] is not ('{' or '}')) token.Append(text[position++]);
                return token.ToString();
            }
            while (position < text.Length)
            {
                char current = text[position++];
                if (current == '"') return token.ToString();
                if (current == '\\' && position < text.Length)
                {
                    char escaped = text[position++];
                    if (escaped is '"' or '\\') token.Append(escaped);
                    else { token.Append('\\'); token.Append(escaped); }
                }
                else token.Append(current);
            }
            throw new InvalidDataException("Unterminated Steam KeyValues string.");
        }
        SteamKeyValues Read(bool nested, int depth)
        {
            if (depth > 32) throw new InvalidDataException("Steam KeyValues nesting is too deep.");
            var node = new SteamKeyValues();
            while (Token() is string key)
            {
                if (key == "}")
                {
                    if (!nested) throw new InvalidDataException("Unexpected Steam KeyValues closing brace.");
                    return node;
                }
                if (key == "{") throw new InvalidDataException("Missing Steam KeyValues key.");
                string value = Token() ?? throw new InvalidDataException("Missing Steam KeyValues value.");
                if (value == "}") throw new InvalidDataException("Missing Steam KeyValues value.");
                if (value == "{") node._entries.Add(new Entry(key, null, Read(true, depth + 1)));
                else node._entries.Add(new Entry(key, value, null));
            }
            if (nested) throw new InvalidDataException("Unterminated Steam KeyValues object.");
            return node;
        }
        return Read(false, 0);
    }

    public override string ToString()
    {
        var output = new StringBuilder();
        static string Quote(string text) => "\"" + text.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
        void Write(SteamKeyValues node, int depth)
        {
            string indentation = new('\t', depth);
            foreach (var entry in node._entries)
            {
                output.Append(indentation).Append(Quote(entry.Key));
                if (entry.Object is null) output.Append('\t').AppendLine(Quote(entry.Value!));
                else
                {
                    output.AppendLine().Append(indentation).AppendLine("{");
                    Write(entry.Object, depth + 1);
                    output.Append(indentation).AppendLine("}");
                }
            }
        }
        Write(this, 0);
        return output.ToString();
    }
}
