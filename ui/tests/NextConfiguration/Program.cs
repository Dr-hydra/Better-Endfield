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
        Console.WriteLine("Next camera configuration: 5 standalone-freeze/MMD/retired-setting checks passed.");
    }
}
