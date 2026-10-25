using System.Collections.Generic;
using System.Text;

namespace BetterEndfieldNext.UI.Models;

internal sealed class WeaponAppearanceConfiguration
{
    public bool Enabled { get; set; } = true;
    public bool ReplaceWeapon { get; set; } = true;
    public string WeaponMode { get; set; } = "ink";
    public bool ReplaceCharacter { get; set; } = true;
    public string CharacterMode { get; set; } = "ink";
    public bool ReplaceEffects { get; set; } = true;
    public string EffectsMode { get; set; } = "ink";

    public string ToIni()
    {
        var sb = new StringBuilder();
        sb.AppendLine("[betterendfieldnext.weapon_appearance]");
        sb.AppendLine($"enabled={(Enabled ? "true" : "false")}");
        sb.AppendLine($"replace_weapon={(ReplaceWeapon ? "true" : "false")}");
        sb.AppendLine($"weapon_mode={WeaponMode}");
        sb.AppendLine($"replace_character={(ReplaceCharacter ? "true" : "false")}");
        sb.AppendLine($"character_mode={CharacterMode}");
        sb.AppendLine($"replace_effects={(ReplaceEffects ? "true" : "false")}");
        sb.AppendLine($"effects_mode={EffectsMode}");
        sb.AppendLine();
        return sb.ToString();
    }

    public static WeaponAppearanceConfiguration FromValues(IReadOnlyDictionary<string, string> values)
    {
        static bool Flag(IReadOnlyDictionary<string, string> dict, string key, bool fallback) =>
            dict.TryGetValue(key, out var v) ? v is "true" or "1" or "yes" or "on" : fallback;
        static string Text(IReadOnlyDictionary<string, string> dict, string key, string fallback) =>
            dict.TryGetValue(key, out var v) && !string.IsNullOrWhiteSpace(v) ? v.Trim().ToLowerInvariant() : fallback;

        return new WeaponAppearanceConfiguration
        {
            Enabled = Flag(values, "enabled", true),
            ReplaceWeapon = Flag(values, "replace_weapon", true),
            WeaponMode = Text(values, "weapon_mode", "ink"),
            ReplaceCharacter = Flag(values, "replace_character", true),
            CharacterMode = Text(values, "character_mode", "ink"),
            ReplaceEffects = Flag(values, "replace_effects", true),
            EffectsMode = Text(values, "effects_mode", "ink"),
        };
    }
}
