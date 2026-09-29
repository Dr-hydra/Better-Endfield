using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BetterEndfield.UI.Models;

// Free camera look, motion presets, keyframes, VMD camera and MMD playback
// ([betterendfield.camera] keys read by native/modules/camera).
public sealed class FreeCameraExtras
{
    public static readonly string[] MotionPresets = ["orbit", "dolly_zoom", "crane", "truck"];
    // vmd_cloth_mode values, in the order of the MMD cloth combo box.
    public static readonly string[] ClothModes = ["stable", "game", "freeze"];

    // Hotkey layout 2 puts every MMD action on the numpad. Hotkeys saved by
    // layout 1 are replaced by these defaults when read.
    public const int CurrentHotkeyLayout = 2;

    public bool MouseLook { get; set; } = true;
    public bool MouseInvertY { get; set; } = false;
    public double MouseSensitivity { get; set; } = 0.1;
    public double Smoothing { get; set; } = 0.3;
    public string MotionPreset { get; set; } = "orbit";
    public double MotionSpeed { get; set; } = 1.0;
    public double OrbitSpeed { get; set; } = 20.0;
    public double MotionDuration { get; set; } = 0.0;
    public double MotionTargetHeight { get; set; } = 1.2;
    public double KeyframeSegmentSeconds { get; set; } = 3.0;
    public bool KeyframeLoop { get; set; } = false;
    public string KeyframeFile { get; set; } = string.Empty;
    public string KeyframeSaveHotkey { get; set; } = "NONE";
    public string KeyframeLoadHotkey { get; set; } = "NONE";
    // Character motion (EIEM DirectVmd body) used by MMD playback.
    public string VmdMotionFile { get; set; } = string.Empty;
    public bool VmdBodyEnabled { get; set; }
    public bool VmdEyesEnabled { get; set; }
    public bool VmdFaceEnabled { get; set; }
    public bool VmdMotionLoop { get; set; }
    public double VmdMotionWeight { get; set; } = 1.0;
    public bool VmdTerrainEnabled { get; set; }
    public string VmdClothMode { get; set; } = "stable";
    public double VmdMotionScale { get; set; } = 1.0;
    public string VmdMotionHotkey { get; set; } = "NONE";
    public string VmdMotionPauseHotkey { get; set; } = "NONE";
    public string VmdMotionStopHotkey { get; set; } = "NONE";
    public string VmdCameraFile { get; set; } = string.Empty;
    public double VmdCameraScale { get; set; } = 0.07;
    public double VmdCameraFovBias { get; set; } = 5.0;
    public bool VmdCameraLoop { get; set; } = false;
    public string RollLeftHotkey { get; set; } = "NUMPAD7";
    public string RollRightHotkey { get; set; } = "NUMPAD9";
    public string FovWideHotkey { get; set; } = "NUMPAD1";
    public string FovNarrowHotkey { get; set; } = "NUMPAD3";
    public string ViewResetHotkey { get; set; } = "NUMPAD8";
    public string MotionHotkey { get; set; } = "NUMPAD2";
    public string KeyframeAddHotkey { get; set; } = "NUMPAD0";
    public string KeyframePlayHotkey { get; set; } = "DECIMAL";
    public string KeyframeClearHotkey { get; set; } = "NONE";
    public string VmdPlayHotkey { get; set; } = "NONE";
    // MMD playback: one clock for motion, VMD camera and music.
    public bool MmdEnabled { get; set; } = false;
    public bool MmdMusicEnabled { get; set; } = true;
    public bool MmdOverlayEnabled { get; set; } = true;
    public bool MmdOverlayVisible { get; set; } = false;
    public bool MmdLoop { get; set; } = false;
    public double MmdSeekSeconds { get; set; } = 5.0;
    public double MmdMusicGain { get; set; } = 1.0;
    public string MmdWork { get; set; } = string.Empty;
    public string MmdPlayHotkey { get; set; } = "NUMPAD_ENTER";
    public string MmdStopHotkey { get; set; } = "ADD";
    public string MmdSeekBackHotkey { get; set; } = "NUMPAD4";
    public string MmdSeekForwardHotkey { get; set; } = "NUMPAD6";
    public string MmdCameraModeHotkey { get; set; } = "NUMPAD5";
    public string MmdOverlayHotkey { get; set; } = "SUBTRACT";

    // The camera module must load for MMD playback even without the free camera.
    public bool RequiresCameraModule => MmdEnabled;

    // MMD music plays through the Music module's local channel.
    public bool RequiresMusicModule => MmdEnabled && MmdMusicEnabled;

    public string ToIniLines()
    {
        static string Boolean(bool value) => value ? "true" : "false";
        static string Number(double value) =>
            value.ToString("0.########", CultureInfo.InvariantCulture);
        static string Clean(string value) =>
            value.Trim().Replace("\r", string.Empty).Replace("\n", string.Empty);

        var text = new StringBuilder();
        void Line(string key, string value) => text.Append(key).Append('=').Append(value).Append("\r\n");
        Line("hotkey_layout", CurrentHotkeyLayout.ToString(CultureInfo.InvariantCulture));
        Line("free_camera_mouse_look", Boolean(MouseLook));
        Line("mouse_invert_y", Boolean(MouseInvertY));
        Line("mouse_sensitivity", Number(MouseSensitivity));
        Line("free_camera_smoothing", Number(Smoothing));
        Line("motion_preset", MotionPreset);
        Line("motion_speed", Number(MotionSpeed));
        Line("orbit_speed", Number(OrbitSpeed));
        Line("motion_duration", Number(MotionDuration));
        Line("motion_target_height", Number(MotionTargetHeight));
        Line("keyframe_segment_seconds", Number(KeyframeSegmentSeconds));
        Line("keyframe_loop", Boolean(KeyframeLoop));
        Line("keyframe_file", Clean(KeyframeFile));
        Line("keyframe_save_hotkey", KeyframeSaveHotkey);
        Line("keyframe_load_hotkey", KeyframeLoadHotkey);
        Line("vmd_motion_file", Clean(VmdMotionFile));
        Line("vmd_body_enabled", Boolean(VmdBodyEnabled));
        Line("vmd_eyes_enabled", Boolean(VmdEyesEnabled));
        Line("vmd_face_enabled", Boolean(VmdFaceEnabled));
        Line("vmd_motion_loop", Boolean(VmdMotionLoop));
        Line("vmd_motion_weight", Number(VmdMotionWeight));
        Line("vmd_terrain_enabled", Boolean(VmdTerrainEnabled));
        Line("vmd_cloth_mode", VmdClothMode);
        Line("vmd_motion_scale", Number(VmdMotionScale));
        Line("vmd_motion_hotkey", VmdMotionHotkey);
        Line("vmd_motion_pause_hotkey", VmdMotionPauseHotkey);
        Line("vmd_motion_stop_hotkey", VmdMotionStopHotkey);
        Line("vmd_camera_file", Clean(VmdCameraFile));
        Line("vmd_camera_scale", Number(VmdCameraScale));
        Line("vmd_camera_fov_bias", Number(VmdCameraFovBias));
        Line("vmd_camera_loop", Boolean(VmdCameraLoop));
        Line("roll_left_hotkey", RollLeftHotkey);
        Line("roll_right_hotkey", RollRightHotkey);
        Line("fov_wide_hotkey", FovWideHotkey);
        Line("fov_narrow_hotkey", FovNarrowHotkey);
        Line("view_reset_hotkey", ViewResetHotkey);
        Line("motion_hotkey", MotionHotkey);
        Line("keyframe_add_hotkey", KeyframeAddHotkey);
        Line("keyframe_play_hotkey", KeyframePlayHotkey);
        Line("keyframe_clear_hotkey", KeyframeClearHotkey);
        Line("vmd_play_hotkey", VmdPlayHotkey);
        Line("mmd_enabled", Boolean(MmdEnabled));
        Line("mmd_overlay_enabled", Boolean(MmdOverlayEnabled));
        Line("mmd_overlay_visible", Boolean(MmdOverlayVisible));
        Line("mmd_loop", Boolean(MmdLoop));
        Line("mmd_seek_seconds", Number(MmdSeekSeconds));
        Line("mmd_music_gain", Number(MmdMusicGain));
        Line("mmd_work", Clean(MmdWork));
        Line("mmd_play_hotkey", MmdPlayHotkey);
        Line("mmd_stop_hotkey", MmdStopHotkey);
        Line("mmd_seek_back_hotkey", MmdSeekBackHotkey);
        Line("mmd_seek_forward_hotkey", MmdSeekForwardHotkey);
        Line("mmd_camera_mode_hotkey", MmdCameraModeHotkey);
        Line("mmd_overlay_hotkey", MmdOverlayHotkey);
        Line("mmd_music_enabled", Boolean(MmdMusicEnabled));
        return text.ToString();
    }

    public static FreeCameraExtras FromValues(IReadOnlyDictionary<string, string> values)
    {
        var extras = new FreeCameraExtras();
        string Text(string key, string fallback) =>
            values.TryGetValue(key, out string? value) && !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : fallback;
        double Number(string key, double fallback) =>
            values.TryGetValue(key, out string? value) &&
            double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double number) &&
            double.IsFinite(number)
                ? number
                : fallback;
        bool Boolean(string key, bool fallback) =>
            values.TryGetValue(key, out string? value)
                ? value.Trim().ToLowerInvariant() is "true" or "1" or "yes" or "on"
                : fallback;
        // Layout 1 numpad bindings collide with the MMD layout; keep the new defaults.
        bool currentLayout = Number("hotkey_layout", 1) >= CurrentHotkeyLayout;
        string Hotkey(string key, string fallback) => currentLayout ? Text(key, fallback) : fallback;

        extras.MouseLook = Boolean("free_camera_mouse_look", extras.MouseLook);
        extras.MouseInvertY = Boolean("mouse_invert_y", extras.MouseInvertY);
        extras.MouseSensitivity = Number("mouse_sensitivity", extras.MouseSensitivity);
        extras.Smoothing = Number("free_camera_smoothing", extras.Smoothing);
        string preset = Text("motion_preset", extras.MotionPreset).ToLowerInvariant();
        extras.MotionPreset = System.Array.IndexOf(MotionPresets, preset) >= 0 ? preset : "orbit";
        extras.MotionSpeed = Number("motion_speed", extras.MotionSpeed);
        extras.OrbitSpeed = Number("orbit_speed", extras.OrbitSpeed);
        extras.MotionDuration = Number("motion_duration", extras.MotionDuration);
        extras.MotionTargetHeight = Number("motion_target_height", extras.MotionTargetHeight);
        extras.KeyframeSegmentSeconds = Number("keyframe_segment_seconds", extras.KeyframeSegmentSeconds);
        extras.KeyframeLoop = Boolean("keyframe_loop", extras.KeyframeLoop);
        extras.KeyframeFile = Text("keyframe_file", string.Empty);
        extras.KeyframeSaveHotkey = Hotkey("keyframe_save_hotkey", extras.KeyframeSaveHotkey);
        extras.KeyframeLoadHotkey = Hotkey("keyframe_load_hotkey", extras.KeyframeLoadHotkey);
        extras.VmdMotionFile = values.TryGetValue("vmd_motion_file", out string? motionFile) ? motionFile.Trim() : string.Empty;
        extras.VmdBodyEnabled = Boolean("vmd_body_enabled", false);
        extras.VmdEyesEnabled = Boolean("vmd_eyes_enabled", false);
        extras.VmdFaceEnabled = Boolean("vmd_face_enabled", false);
        extras.VmdMotionLoop = Boolean("vmd_motion_loop", false);
        extras.VmdMotionWeight = System.Math.Clamp(Number("vmd_motion_weight", 1), 0, 1);
        extras.VmdTerrainEnabled = Boolean("vmd_terrain_enabled", false);
        string cloth = Text("vmd_cloth_mode", "stable").ToLowerInvariant();
        extras.VmdClothMode = System.Array.IndexOf(ClothModes, cloth) >= 0 ? cloth : "stable";
        extras.VmdMotionScale = System.Math.Clamp(Number("vmd_motion_scale", 1), 0.05, 5);
        extras.VmdMotionHotkey = Hotkey("vmd_motion_hotkey", extras.VmdMotionHotkey);
        extras.VmdMotionPauseHotkey = Hotkey("vmd_motion_pause_hotkey", extras.VmdMotionPauseHotkey);
        extras.VmdMotionStopHotkey = Hotkey("vmd_motion_stop_hotkey", extras.VmdMotionStopHotkey);
        extras.VmdCameraFile = values.TryGetValue("vmd_camera_file", out string? file) ? file.Trim() : string.Empty;
        extras.VmdCameraScale = Number("vmd_camera_scale", extras.VmdCameraScale);
        extras.VmdCameraFovBias = Number("vmd_camera_fov_bias", extras.VmdCameraFovBias);
        extras.VmdCameraLoop = Boolean("vmd_camera_loop", extras.VmdCameraLoop);
        extras.RollLeftHotkey = Hotkey("roll_left_hotkey", extras.RollLeftHotkey);
        extras.RollRightHotkey = Hotkey("roll_right_hotkey", extras.RollRightHotkey);
        extras.FovWideHotkey = Hotkey("fov_wide_hotkey", extras.FovWideHotkey);
        extras.FovNarrowHotkey = Hotkey("fov_narrow_hotkey", extras.FovNarrowHotkey);
        extras.ViewResetHotkey = Hotkey("view_reset_hotkey", extras.ViewResetHotkey);
        extras.MotionHotkey = Hotkey("motion_hotkey", extras.MotionHotkey);
        extras.KeyframeAddHotkey = Hotkey("keyframe_add_hotkey", extras.KeyframeAddHotkey);
        extras.KeyframePlayHotkey = Hotkey("keyframe_play_hotkey", extras.KeyframePlayHotkey);
        extras.KeyframeClearHotkey = Hotkey("keyframe_clear_hotkey", extras.KeyframeClearHotkey);
        extras.VmdPlayHotkey = Hotkey("vmd_play_hotkey", extras.VmdPlayHotkey);
        extras.MmdEnabled = Boolean("mmd_enabled", extras.MmdEnabled);
        extras.MmdMusicEnabled = Boolean("mmd_music_enabled", extras.MmdMusicEnabled);
        extras.MmdOverlayEnabled = Boolean("mmd_overlay_enabled", extras.MmdOverlayEnabled);
        extras.MmdOverlayVisible = Boolean("mmd_overlay_visible", extras.MmdOverlayVisible);
        extras.MmdLoop = Boolean("mmd_loop", extras.MmdLoop);
        extras.MmdSeekSeconds = System.Math.Clamp(Number("mmd_seek_seconds", extras.MmdSeekSeconds), 0.5, 60);
        extras.MmdMusicGain = System.Math.Clamp(Number("mmd_music_gain", extras.MmdMusicGain), 0, 2);
        extras.MmdWork = Text("mmd_work", string.Empty);
        extras.MmdPlayHotkey = Hotkey("mmd_play_hotkey", extras.MmdPlayHotkey);
        extras.MmdStopHotkey = Hotkey("mmd_stop_hotkey", extras.MmdStopHotkey);
        extras.MmdSeekBackHotkey = Hotkey("mmd_seek_back_hotkey", extras.MmdSeekBackHotkey);
        extras.MmdSeekForwardHotkey = Hotkey("mmd_seek_forward_hotkey", extras.MmdSeekForwardHotkey);
        extras.MmdCameraModeHotkey = Hotkey("mmd_camera_mode_hotkey", extras.MmdCameraModeHotkey);
        extras.MmdOverlayHotkey = Hotkey("mmd_overlay_hotkey", extras.MmdOverlayHotkey);
        return extras;
    }
}
