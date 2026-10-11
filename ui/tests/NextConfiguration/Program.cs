using BetterEndfieldNext.UI.Models;

static class Program
{
    static void Require(bool value, string message)
    {
        if (!value) throw new InvalidOperationException(message);
    }
    static string Camera(ModConfiguration configuration)
    {
        string ini = configuration.ToIni();
        int start = ini.IndexOf("[betterendfieldnext.camera]", StringComparison.Ordinal);
        int end = ini.IndexOf("[betterendfieldnext.actions]", start, StringComparison.Ordinal);
        return ini[start..end].Replace("\r\n", "\n", StringComparison.Ordinal);
    }
    static void Main()
    {
        var configuration = new ModConfiguration();
        Require(Camera(configuration).Contains("\nenabled=false\n", StringComparison.Ordinal), "All disabled camera controls must leave the module disabled.");
        configuration.PauseGameInFreeCamera = true;
        string pause = Camera(configuration);
        Require(pause.Contains("\nenabled=true\n", StringComparison.Ordinal) && pause.Contains("\npause_enabled=true\n", StringComparison.Ordinal), "Freeze alone must load and enable the camera module.");
        Require(pause.Contains("\nfree_camera_enabled=false\n", StringComparison.Ordinal), "Standalone freeze must not enable free camera.");
        Require(!pause.Contains("first_person", StringComparison.Ordinal), "Next must not publish retired settings.");
        configuration.PauseGameInFreeCamera = false;
        configuration.FreeCameraExtras.MmdEnabled = true;
        Require(Camera(configuration).Contains("\nenabled=true\n", StringComparison.Ordinal), "MMD alone must still enable its camera runtime.");
        configuration.FreeCameraExtras.MmdEnabled = false;
        configuration.MusicReplacementEnabled = false;
        configuration.MediaScreen.Enabled = true;
        var screenDefaults = MediaScreenConfiguration.FromValues(new Dictionary<string, string>());
        Require(screenDefaults.WallScreenEnabled && screenDefaults.DeskScreenEnabled && screenDefaults.DeskFitMode == "fit",
            "Existing media configurations must retain both screen targets and default to aspect-preserving fit.");
        var screenTargets = MediaScreenConfiguration.FromValues(new Dictionary<string, string> {
            ["enabled"] = "true", ["source"] = "video", ["wall_screen_enabled"] = "false", ["desk_screen_enabled"] = "true", ["desk_fit_mode"] = "crop"
        });
        Require(!screenTargets.WallScreenEnabled && screenTargets.DeskScreenEnabled && screenTargets.DeskFitMode == "crop" &&
            screenTargets.ToIni().Contains("wall_screen_enabled=false") && screenTargets.ToIni().Contains("desk_fit_mode=crop"),
            "Independent screen targets and fit mode must round trip.");
        screenTargets.DeskScreenEnabled = false;
        Require(!screenTargets.RequiresAudio && !screenTargets.RequiresMusicModule,"Disabled display targets must not request media audio or BGM control.");
        Require(configuration.MediaScreen.Hotkey == "-", "Media screen default must use the main-keyboard minus key.");
        Require(configuration.MediaScreen.PauseHotkey == "CTRL+-" && configuration.MediaScreen.ControlHotkey == "ALT+-" &&
            configuration.MediaScreen.ToIni().Contains("pause_hotkey=CTRL+-") && configuration.MediaScreen.ToIni().Contains("control_hotkey=ALT+-"),
            "Video pause and window control must default to Ctrl+- and Alt+-.");
        configuration.MediaScreen.Source = "window";
        Require(configuration.MediaScreen.RequiresMusicModule && !configuration.MediaScreen.RequiresAudio &&
            configuration.ToIni().Contains("pause_window_bgm=true"), "Window BGM pause must load Music independently of local file playback.");
        configuration.MediaScreen.PauseWindowBgm = false;
        Require(!configuration.MediaScreen.RequiresMusicModule, "Window source with BGM pause off must not require Music.");
        configuration.MediaScreen.PauseWindowBgm = true;
        var customOutput = MediaScreenConfiguration.FromValues(new Dictionary<string, string> { ["width"] = "3840", ["height"] = "2160", ["fps"] = "120" });
        Require(customOutput.Width == 3840 && customOutput.Height == 2160 && customOutput.Fps == 120, "4K/120 configuration was clamped to old limits.");
        var migrated = MediaScreenConfiguration.FromValues(new Dictionary<string, string> { ["hotkey_vk"] = "119" });
        Require(migrated.Hotkey == "-", "Old prototype F8 default must migrate to minus.");
        var keys = MediaScreenConfiguration.FromValues(new Dictionary<string, string> { ["toggle_hotkey"] = "CTRL+-", ["pause_hotkey"] = "ALT+P" });
        Require(keys.Hotkey == "CTRL+-" && keys.PauseHotkey == "ALT+P" && keys.ToIni().Contains("toggle_hotkey=CTRL+-") &&
            keys.ToIni().Contains("pause_hotkey=ALT+P") && !keys.ToIni().Contains("hotkey_vk="), "Custom chords must round trip as text bindings.");
        configuration.MediaScreen.Source = "video";
        configuration.MediaScreen.SourcePath = "G:/素材/片段=a.mp4";
        configuration.MediaScreen.Gain = 1.25;
        string media = configuration.ToIni();
        string music = media[media.IndexOf("[betterendfieldnext.music]", StringComparison.Ordinal)..media.IndexOf("[betterendfieldnext.combat_stats]", StringComparison.Ordinal)];
        Require(music.Contains("enabled=true") && music.Contains("local_playback_enabled=true") && music.Contains("music_replacement_enabled=false"), "Video audio must load Music without enabling OmniMix replacement.");
        Require(media.Contains("source_path=G:/素材/片段=a.mp4") && media.Contains("gain=1.25"), "Media paths and decimal volume must round trip unchanged.");
        configuration.MediaScreen.AudioEnabled = false;
        Require(!configuration.MediaScreen.RequiresAudio, "Muted video must not require the Music module.");
        configuration.MediaScreen.AudioEnabled = true;
        configuration.MediaScreen.Source = "image";
        Require(!configuration.MediaScreen.RequiresAudio, "Images must not require the Music module.");
        configuration.MediaScreen.Source = "video";
        configuration.MediaScreen.Enabled = false;
        Require(!configuration.MediaScreen.RequiresAudio, "Disabled media must not require the Music module.");
        configuration.MediaScreen.SourcePath = "a.png\nenabled=true";
        bool rejected = false;
        try { configuration.MediaScreen.ToIni(); } catch (InvalidDataException) { rejected = true; }
        Require(rejected, "Media paths must not inject configuration lines.");
        Console.WriteLine("Next camera configuration: 5 standalone-freeze/MMD/retired-setting checks passed.");
        Console.WriteLine("Next media configuration: 6 audio-gating/path/decimal/injection checks passed.");
        Console.WriteLine("Next media hotkeys: default minus, legacy migration and custom chord serialization passed.");
    }
}
