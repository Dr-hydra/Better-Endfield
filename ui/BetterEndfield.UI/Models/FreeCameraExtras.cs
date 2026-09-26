using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BetterEndfield.UI.Models;

// Free camera look, motion presets, keyframes and VMD camera playback
// ([betterendfield.camera] keys read by native/modules/camera).
public sealed class FreeCameraExtras
{
    public static readonly string[] MotionPresets = ["orbit", "dolly_zoom", "crane", "truck"];

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
    public string VmdCameraFile { get; set; } = string.Empty;
    public double VmdCameraScale { get; set; } = 0.07;
    public double VmdCameraFovBias { get; set; } = 5.0;
    public bool VmdCameraLoop { get; set; } = false;
    public string RollLeftHotkey { get; set; } = "NUMPAD7";
    public string RollRightHotkey { get; set; } = "NUMPAD9";
    public string FovWideHotkey { get; set; } = "NUMPAD1";
    public string FovNarrowHotkey { get; set; } = "NUMPAD3";
    public string ViewResetHotkey { get; set; } = "NUMPAD5";
    public string MotionHotkey { get; set; } = "NUMPAD8";
    public string KeyframeAddHotkey { get; set; } = "NUMPAD0";
    public string KeyframePlayHotkey { get; set; } = "NUMPAD2";
    public string KeyframeClearHotkey { get; set; } = "NUMPAD4";
    public string VmdPlayHotkey { get; set; } = "NUMPAD6";

    public string ToIniLines()
    {
        static string Boolean(bool value) => value ? "true" : "false";
        static string Number(double value) =>
            value.ToString("0.########", CultureInfo.InvariantCulture);

        var text = new StringBuilder();
        void Line(string key, string value) => text.Append(key).Append('=').Append(value).Append("\r\n");
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
        Line("vmd_camera_file", VmdCameraFile.Trim());
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
        extras.VmdCameraFile = values.TryGetValue("vmd_camera_file", out string? file) ? file.Trim() : string.Empty;
        extras.VmdCameraScale = Number("vmd_camera_scale", extras.VmdCameraScale);
        extras.VmdCameraFovBias = Number("vmd_camera_fov_bias", extras.VmdCameraFovBias);
        extras.VmdCameraLoop = Boolean("vmd_camera_loop", extras.VmdCameraLoop);
        extras.RollLeftHotkey = Text("roll_left_hotkey", extras.RollLeftHotkey);
        extras.RollRightHotkey = Text("roll_right_hotkey", extras.RollRightHotkey);
        extras.FovWideHotkey = Text("fov_wide_hotkey", extras.FovWideHotkey);
        extras.FovNarrowHotkey = Text("fov_narrow_hotkey", extras.FovNarrowHotkey);
        extras.ViewResetHotkey = Text("view_reset_hotkey", extras.ViewResetHotkey);
        extras.MotionHotkey = Text("motion_hotkey", extras.MotionHotkey);
        extras.KeyframeAddHotkey = Text("keyframe_add_hotkey", extras.KeyframeAddHotkey);
        extras.KeyframePlayHotkey = Text("keyframe_play_hotkey", extras.KeyframePlayHotkey);
        extras.KeyframeClearHotkey = Text("keyframe_clear_hotkey", extras.KeyframeClearHotkey);
        extras.VmdPlayHotkey = Text("vmd_play_hotkey", extras.VmdPlayHotkey);
        return extras;
    }
}
