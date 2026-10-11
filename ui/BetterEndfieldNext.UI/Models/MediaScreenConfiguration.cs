using System.Globalization;
using System.Text;
namespace BetterEndfieldNext.UI.Models;
internal sealed class MediaScreenConfiguration
{
    public const int MaxDimension = 3840, MaxPixels = 3840 * 2160, MaxFps = 120;
    public bool Enabled { get; set; }
    public bool WallScreenEnabled { get; set; } = true;
    public bool DeskScreenEnabled { get; set; } = true;
    public string DeskFitMode { get; set; } = "fit";
    public string Source { get; set; } = "image";
    public string SourcePath { get; set; } = "";
    public string ImagePaths { get; set; } = "";
    public string TargetObject { get; set; } = "ReceptionRoomImagePoster";
    public bool AudioEnabled { get; set; } = true;
    public bool PauseWindowBgm { get; set; } = true;
    public bool Loop { get; set; } = true;
    public double Gain { get; set; } = 1;
    public double SlideInterval { get; set; } = 10;
    public int Width { get; set; } = 1920;
    public int Height { get; set; } = 1080;
    public int Fps { get; set; } = 60;
    public string Hotkey { get; set; } = "-";
    public string PauseHotkey { get; set; } = "CTRL+-";
    public string ControlHotkey { get; set; } = "ALT+-";
    public ulong WindowHandle { get; set; }
    public uint WindowPid { get; set; }
    public bool RequiresAudio => Enabled && (WallScreenEnabled || DeskScreenEnabled) && AudioEnabled && Source == "video";
    public bool RequiresMusicModule => RequiresAudio || Enabled && (WallScreenEnabled || DeskScreenEnabled) && Source == "window" && PauseWindowBgm;
    public string ToIni()
    {
        var text = new StringBuilder("[betterendfieldnext.media_screen]\n");
        void Add(string key, object value) {
            var rendered = value is bool flag ? flag ? "true" : "false" : Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
            if (rendered.IndexOfAny(['\r', '\n', '\0']) >= 0) throw new InvalidDataException("Media setting contains a line break.");
            text.Append(key).Append('=').AppendLine(rendered);
        }
        Add("enabled", Enabled); Add("source", Source); Add("source_path", SourcePath); Add("image_paths", ImagePaths);
        Add("wall_screen_enabled", WallScreenEnabled); Add("desk_screen_enabled", DeskScreenEnabled); Add("desk_fit_mode", DeskFitMode);
        Add("target_object", TargetObject); Add("audio_enabled", AudioEnabled); Add("loop", Loop);
        Add("pause_window_bgm", PauseWindowBgm);
        Add("gain", Gain); Add("slide_interval", SlideInterval); Add("width", Width); Add("height", Height);
        Add("fps", Fps); Add("toggle_hotkey", Hotkey); Add("pause_hotkey", string.IsNullOrWhiteSpace(PauseHotkey) ? "NONE" : PauseHotkey);
        Add("control_hotkey", string.IsNullOrWhiteSpace(ControlHotkey) ? "NONE" : ControlHotkey);
        Add("window_handle", WindowHandle); Add("window_pid", WindowPid);
        return text.ToString();
    }
    public static MediaScreenConfiguration FromValues(IReadOnlyDictionary<string, string> values)
    {
        string Text(string key, string fallback) => values.TryGetValue(key, out var v) ? v : fallback;
        bool Flag(string key, bool fallback) => values.TryGetValue(key, out var v) ? v is "true" or "1" : fallback;
        double Number(string key, double fallback) => double.TryParse(Text(key, ""), NumberStyles.Float,
            CultureInfo.InvariantCulture, out var n) && double.IsFinite(n) ? n : fallback;
        ulong.TryParse(Text("window_handle", ""), out var handle); uint.TryParse(Text("window_pid", ""), out var pid);
        string LegacyHotkey() {
            if (!int.TryParse(Text("hotkey_vk", ""), out int key) || key == 0x77) return "-";
            if (key is >= 0x70 and <= 0x87) return "F" + (key - 0x70 + 1);
            return key == 0 ? "NONE" : "-";
        }
        return new() {
            Enabled = Flag("enabled", false), Source = Text("source", "image"), SourcePath = Text("source_path", ""),
            WallScreenEnabled = Flag("wall_screen_enabled", true), DeskScreenEnabled = Flag("desk_screen_enabled", true),
            DeskFitMode = Text("desk_fit_mode", "fit") is "crop" ? "crop" : Text("desk_fit_mode", "fit") is "stretch" ? "stretch" : "fit",
            ImagePaths = Text("image_paths", ""), TargetObject = Text("target_object", "ReceptionRoomImagePoster"),
            AudioEnabled = Flag("audio_enabled", true), PauseWindowBgm = Flag("pause_window_bgm", true), Loop = Flag("loop", true), Gain = Math.Clamp(Number("gain", 1), 0, 2),
            SlideInterval = Math.Clamp(Number("slide_interval", 10), 1, 3600), Width = (int)Math.Clamp(Number("width", 1920), 64, MaxDimension),
            Height = (int)Math.Clamp(Number("height", 1080), 64, MaxDimension), Fps = (int)Math.Clamp(Number("fps", 60), 1, MaxFps),
            Hotkey = Text("toggle_hotkey", LegacyHotkey()), PauseHotkey = Text("pause_hotkey", "CTRL+-"), ControlHotkey = Text("control_hotkey", "ALT+-"),
            WindowHandle = handle, WindowPid = pid
        };
    }
}
