using System.Runtime.InteropServices;
using Windows.System;

namespace BetterEndfield.UI.Services;

internal static class HotkeyService
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int key);

    internal static bool IsModifier(VirtualKey key) => key is VirtualKey.Control or
        VirtualKey.Menu or VirtualKey.Shift or VirtualKey.LeftWindows or VirtualKey.RightWindows;

    internal static string Capture(VirtualKey key, bool extended)
    {
        var parts = new List<string>();
        if ((GetAsyncKeyState(0x11) & 0x8000) != 0) parts.Add("CTRL");
        if ((GetAsyncKeyState(0x12) & 0x8000) != 0) parts.Add("ALT");
        if ((GetAsyncKeyState(0x10) & 0x8000) != 0) parts.Add("SHIFT");
        if ((GetAsyncKeyState(0x5B) & 0x8000) != 0 || (GetAsyncKeyState(0x5C) & 0x8000) != 0) parts.Add("WIN");
        int code = (int)key;
        string name = code switch
        {
            >= 0x30 and <= 0x39 => ((char)code).ToString(),
            >= 0x41 and <= 0x5A => ((char)code).ToString(),
            >= 0x70 and <= 0x87 => "F" + (code - 0x70 + 1),
            >= 0x60 and <= 0x69 => "NUMPAD" + (code - 0x60),
            0x0D => extended ? "NUMPAD_ENTER" : "ENTER",
            0x20 => "SPACE", 0x09 => "TAB", 0x1B => "ESC",
            0x6B => "ADD", 0x6D => "SUBTRACT", 0x6A => "MULTIPLY", 0x6F => "DIVIDE", 0x6E => "DECIMAL",
            0xBD => "-", 0x26 => "UP", 0x28 => "DOWN", 0x25 => "LEFT", 0x27 => "RIGHT",
            _ => string.Empty
        };
        if (name.Length == 0) return string.Empty;
        parts.Add(name);
        return string.Join('+', parts);
    }

    internal static bool TryNormalize(string? value, out string normalized)
    {
        normalized = (value ?? string.Empty).Trim().Replace(" ", string.Empty).ToUpperInvariant();
        if (normalized is "NONE" or "OFF" or "DISABLED") { normalized = "NONE"; return true; }
        string[] parts = normalized.Split('+', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return false;
        foreach (string modifier in parts[..^1])
            if (modifier is not ("CTRL" or "ALT" or "SHIFT" or "WIN")) return false;
        string key = parts[^1];
        if (key is "MINUS" or "OEM_MINUS") { parts[^1] = "-"; key = "-"; }
        else if (key is "NUMPAD_MINUS" or "NUMPADSUBTRACT") { parts[^1] = "SUBTRACT"; key = "SUBTRACT"; }
        else if (key is "NUMPAD_PLUS" or "NUMPADADD") { parts[^1] = "ADD"; key = "ADD"; }
        normalized = string.Join('+', parts);
        if (key.Length == 1 && (char.IsAsciiLetterOrDigit(key[0]) || key == "-")) return true;
        if (key is "ENTER" or "NUMPAD_ENTER" or "SPACE" or "TAB" or "ESC" or
            "ADD" or "SUBTRACT" or "MULTIPLY" or "DIVIDE" or "DECIMAL" or "UP" or "DOWN" or "LEFT" or "RIGHT") return true;
        if (key.StartsWith("NUMPAD", StringComparison.Ordinal) && key.Length == 7 && key[^1] is >= '0' and <= '9') return true;
        return key.StartsWith('F') && int.TryParse(key[1..], out int number) && number is >= 1 and <= 24;
    }
}
