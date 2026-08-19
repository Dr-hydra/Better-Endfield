using System.Globalization;
using System.Text;

namespace BetterEndfield.UI.Models;

internal sealed class ModConfiguration
{
    public string Character { get; set; } = "chr_0013_aglina";

    public string FinalAction { get; set; } =
        "a_actor_aglina_dialog_state_shy2_walk_loop";

    public string ModelPath { get; set; } = string.Empty;

    public string ModelPathHash { get; set; } = string.Empty;

    public string ModelBundleHash { get; set; } = string.Empty;

    public string SitLoopPath { get; set; } = string.Empty;

    public string SitLoopPathHash { get; set; } = string.Empty;

    public string SitLoopLabel { get; set; } = string.Empty;

    public string SitSpecialPath { get; set; } = string.Empty;

    public string SitSpecialPathHash { get; set; } = string.Empty;

    public string SitSpecialLabel { get; set; } = string.Empty;

    public string SitToWalkPath { get; set; } = string.Empty;

    public string SitToWalkPathHash { get; set; } = string.Empty;

    public string SitToWalkLabel { get; set; } = string.Empty;

    public string FinalPath { get; set; } = string.Empty;

    public string FinalPathHash { get; set; } = string.Empty;

    public string FinalLabel { get; set; } = string.Empty;

    public bool FinalNativeLoop { get; set; }

    public double StartYaw { get; set; } = -120.0;

    public double TurnDuration { get; set; } = 3.0333335;

    public double Scale { get; set; } = 1.0;

    public double ForwardLeanSample { get; set; } = 1.0;

    public double SitLoopSpeed { get; set; } = 1.0;

    public double SitSpecialSpeed { get; set; } = 1.0;

    public double SitToWalkSpeed { get; set; } = 1.0;

    public double FinalSpeed { get; set; } = 1.0;

    public bool FinalLoop { get; set; } = true;

    public bool ForceLoop { get; set; }

    public bool UseCrossfade { get; set; }

    public double LoopStart { get; set; } = 0.968;

    public double LoopEnd { get; set; } = 2.3760002;

    public double CrossfadeDuration { get; set; } = 0.20;

    public bool ModelReplacementEnabled { get; set; } = false;

    public bool LogoThemeEnabled { get; set; } = false;

    public string LogoThemeColor { get; set; } = "#FFC928";

    public bool VoiceRouterEnabled { get; set; } = false;

    public bool ReplaceNarrativeVoice { get; set; } = true;

    public bool VoiceDiagnostics { get; set; } = false;

    public string VoiceLanguageRules { get; set; } = string.Empty;

    public bool MusicReplacementEnabled { get; set; } = false;

    public string OmniMixBackendExe { get; set; } = string.Empty;

    public string OmniMixClientId { get; set; } = string.Empty;

    public bool ReplaceLoginMusic { get; set; } = true;

    public bool ReplaceMetaMusic { get; set; } = true;

    public bool ReplaceGameplayMusic { get; set; } = true;

    public double MusicTargetLatency { get; set; } = 0.4;

    public double MusicPrebufferMilliseconds { get; set; } = 150.0;

    public bool FallbackToNativeMusic { get; set; } = true;

    public bool MusicDiagnostics { get; set; } = false;

    public bool CombatStatsEnabled { get; set; } = false;

    public bool HideDamageNumbers { get; set; } = false;

    public string CombatStartHotkey { get; set; } = "F7";

    public string CombatStopHotkey { get; set; } = "F8";

    public bool RecordAllDamage { get; set; } = true;

    public bool IncludeOverkillDamage { get; set; } = false;

    public double MinimumDamage { get; set; } = 0.0;

    public bool GroupDamageByCharacter { get; set; } = true;

    public bool GroupDamageBySkill { get; set; } = true;

    public bool GroupDamageByType { get; set; } = true;

    public bool SaveRawCombatEvents { get; set; } = false;

    public static ModConfiguration CreateDefaults() => new();

    public string ToIni()
    {
        static string Number(double value) =>
            value.ToString("0.########", CultureInfo.InvariantCulture);
        static string Boolean(bool value) => value ? "true" : "false";
        static string VoiceRules(string value) => string.Join(
            ",",
            value.Split(
                ['\r', '\n', ',', ';'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(rule =>
                {
                    int equals = rule.IndexOf('=');
                    int colon = rule.IndexOf(':');
                    int separator = equals >= 0 && colon >= 0
                        ? Math.Min(equals, colon)
                        : Math.Max(equals, colon);
                    return separator > 0
                        ? rule[..separator].Trim() + ":" + rule[(separator + 1)..].Trim()
                        : rule;
                }));

        var text = new StringBuilder();
        text.AppendLine("; Better Endfield runtime configuration");
        text.AppendLine("; Visual and language changes are hot-reloaded; model changes apply on the next injection.");
        text.AppendLine();
        text.AppendLine("[betterendfield.model]");
        text.AppendLine("schema_version=5");
        text.AppendLine($"enabled={Boolean(ModelReplacementEnabled || LogoThemeEnabled)}");
        text.AppendLine($"model_replacement_enabled={Boolean(ModelReplacementEnabled)}");
        text.AppendLine($"logo_theme_enabled={Boolean(LogoThemeEnabled)}");
        text.AppendLine($"logo_theme_color={LogoThemeColor}");
        text.AppendLine("diagnostics=true");
        text.AppendLine($"character={Character}");
        text.AppendLine($"final_action={FinalAction}");
        text.AppendLine($"model_path={ModelPath}");
        text.AppendLine($"model_path_hash={ModelPathHash}");
        text.AppendLine($"model_bundle_hash={ModelBundleHash}");
        text.AppendLine($"sit_loop_path={SitLoopPath}");
        text.AppendLine($"sit_loop_path_hash={SitLoopPathHash}");
        text.AppendLine($"sit_loop_label={SitLoopLabel}");
        text.AppendLine($"sit_special_path={SitSpecialPath}");
        text.AppendLine($"sit_special_path_hash={SitSpecialPathHash}");
        text.AppendLine($"sit_special_label={SitSpecialLabel}");
        text.AppendLine($"sit_to_walk_path={SitToWalkPath}");
        text.AppendLine($"sit_to_walk_path_hash={SitToWalkPathHash}");
        text.AppendLine($"sit_to_walk_label={SitToWalkLabel}");
        text.AppendLine($"final_path={FinalPath}");
        text.AppendLine($"final_path_hash={FinalPathHash}");
        text.AppendLine($"final_label={FinalLabel}");
        text.AppendLine($"final_native_loop={Boolean(FinalNativeLoop)}");
        text.AppendLine($"start_yaw={Number(StartYaw)}");
        text.AppendLine($"turn_duration={Number(TurnDuration)}");
        text.AppendLine($"scale={Number(Scale)}");
        text.AppendLine($"forward_lean_sample={Number(ForwardLeanSample)}");
        text.AppendLine($"sit_loop_speed={Number(SitLoopSpeed)}");
        text.AppendLine($"sit_special_speed={Number(SitSpecialSpeed)}");
        text.AppendLine($"sit_to_walk_speed={Number(SitToWalkSpeed)}");
        text.AppendLine($"final_speed={Number(FinalSpeed)}");
        text.AppendLine($"final_loop={Boolean(FinalLoop)}");
        text.AppendLine($"force_loop={Boolean(ForceLoop)}");
        text.AppendLine($"use_crossfade={Boolean(UseCrossfade)}");
        text.AppendLine($"loop_start={Number(LoopStart)}");
        text.AppendLine($"loop_end={Number(LoopEnd)}");
        text.AppendLine($"crossfade_duration={Number(CrossfadeDuration)}");
        text.AppendLine();
        text.AppendLine("[betterendfield.voice]");
        text.AppendLine("; speakerChannel:Chinese|English|Japanese|Korean|FollowGlobal");
        text.AppendLine($"enabled={Boolean(VoiceRouterEnabled)}");
        text.AppendLine($"voice_router_enabled={Boolean(VoiceRouterEnabled)}");
        text.AppendLine($"replace_narrative_voice={Boolean(ReplaceNarrativeVoice)}");
        text.AppendLine($"voice_diagnostics={Boolean(VoiceDiagnostics)}");
        text.AppendLine($"voice_language_rules={VoiceRules(VoiceLanguageRules)}");
        text.AppendLine();
        text.AppendLine("[betterendfield.music]");
        text.AppendLine("schema_version=1");
        text.AppendLine($"enabled={Boolean(MusicReplacementEnabled)}");
        text.AppendLine($"music_replacement_enabled={Boolean(MusicReplacementEnabled)}");
        text.AppendLine($"backend_exe={OmniMixBackendExe}");
        text.AppendLine($"client_id={OmniMixClientId}");
        text.AppendLine($"replace_login={Boolean(ReplaceLoginMusic)}");
        text.AppendLine($"replace_meta={Boolean(ReplaceMetaMusic)}");
        text.AppendLine($"replace_gameplay={Boolean(ReplaceGameplayMusic)}");
        text.AppendLine($"target_latency={Number(MusicTargetLatency)}");
        text.AppendLine($"prebuffer_ms={Number(MusicPrebufferMilliseconds)}");
        text.AppendLine($"fallback_to_native={Boolean(FallbackToNativeMusic)}");
        text.AppendLine($"diagnostics={Boolean(MusicDiagnostics)}");
        text.AppendLine();
        text.AppendLine("[betterendfield.combat_stats]");
        text.AppendLine("schema_version=1");
        text.AppendLine($"enabled={Boolean(CombatStatsEnabled || HideDamageNumbers)}");
        text.AppendLine($"combat_stats_enabled={Boolean(CombatStatsEnabled)}");
        text.AppendLine($"hide_damage_numbers={Boolean(HideDamageNumbers)}");
        text.AppendLine($"hotkey_start={CombatStartHotkey}");
        text.AppendLine($"hotkey_stop={CombatStopHotkey}");
        text.AppendLine($"record_all_damage={Boolean(RecordAllDamage)}");
        text.AppendLine($"include_overkill={Boolean(IncludeOverkillDamage)}");
        text.AppendLine($"minimum_damage={Number(MinimumDamage)}");
        text.AppendLine($"group_by_character={Boolean(GroupDamageByCharacter)}");
        text.AppendLine($"group_by_skill={Boolean(GroupDamageBySkill)}");
        text.AppendLine($"group_by_damage_type={Boolean(GroupDamageByType)}");
        text.AppendLine($"save_raw_events={Boolean(SaveRawCombatEvents)}");
        return text.ToString();
    }
}
