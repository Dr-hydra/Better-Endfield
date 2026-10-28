using BetterEndfieldNext.UI.Services;
static void Check(bool value) { if (!value) throw new Exception("Action INI preservation regression"); }
string[] keys = ["external_loop", "jsspsi_quality"];
foreach (string newline in new[] { "\n", "\r\n" }) foreach (string key in keys)
{
    string existing = $"[other]{newline}{key}=wrong{newline}[betterendfieldnext.actions]{newline}{key}=saved{newline}";
    string section = $"[betterendfieldnext.actions]{newline}schema_version=3{newline}enabled=false{newline}";
    foreach (string replacement in new[] { section, "[Host]" + newline + "modules_root=keep" + newline + section + "[Loader]" + newline + "load_host=true" + newline })
    {
        string result = ActionIniPreservation.PreserveNativeSettings(existing, replacement);
        Check(result.Contains(key + "=saved" + newline)); Check(!result.Contains("wrong"));
        Check(result.Contains("enabled=false"));
        Check(ActionIniPreservation.PreserveNativeSettings(existing, result) == result);
        Check(ActionIniPreservation.PreserveNativeSettings("[other]\n" + key + "=wrong", replacement) == replacement);
    }
    Check(ActionIniPreservation.PreserveNativeSettings(existing, section + key + "=explicit" + newline).Contains(key + "=explicit"));
}
string all = "[betterendfieldnext.actions]\n" + string.Join('\n', keys.Select((key, i) => key + "=" + i)) + "\n";
string saved = ActionIniPreservation.PreserveNativeSettings(all, "[betterendfieldnext.actions]\nenabled=true\n");
foreach (var pair in keys.Select((key, i) => key + "=" + i)) Check(saved.Contains(pair + "\n"));
// Nothing to preserve: the launcher's own section is written unchanged.
string plain = "[betterendfieldnext.actions]\nenabled=true\n";
Check(ActionIniPreservation.PreserveNativeSettings("[betterendfieldnext.actions]\nenabled=false\n", plain) == plain);
Console.WriteLine("Actions INI: external_loop and jsspsi_quality survive section and full saves, newlines and repeated saves.");
