using BepInEx.Configuration;
using Landoria.Shared;

namespace Landoria.FirstPerson
{
    // Reads and saves the player's first-person settings.
    internal static class Preference
    {
        internal const float DefaultFieldOfView = 65f; // Degrees.
        internal const float MinimumFieldOfView = 65f; // Degrees.
        internal const float MaximumFieldOfView = 120f; // Degrees.
        internal const float DefaultFirstPersonFieldOfViewBonus = 15f; // Degrees.
        internal const float MaximumFirstPersonFieldOfViewBonus = 50f; // Degrees.
        internal const int DefaultThirdPersonAutoDistance = 3; // Meters.
        internal const int DefaultAutomaticReturnDelay = 3; // Seconds.
        internal const int DefaultHeadBobStrength = 2;

        private static ConfigEntry<bool> enabled;
        private static ConfigEntry<float> fieldOfView;
        private static ConfigEntry<float> firstPersonFieldOfViewBonus;
        private static ConfigEntry<KeyboardShortcut> toggleShortcut;
        private static ConfigEntry<KeyboardShortcut> toggleThirdPersonAutoShortcut;
        private static ConfigEntry<bool> thirdPersonAuto;
        private static ConfigEntry<int> thirdPersonAutoDistance;
        private static ConfigEntry<int> automaticReturnDelay;
        private static ConfigEntry<int> headBobStrength;

        internal static bool Enabled => enabled.Value;
        internal static float FieldOfView => fieldOfView.Value;
        internal static float FirstPersonFieldOfViewBonus =>
            firstPersonFieldOfViewBonus.Value;
        internal static KeyboardShortcut ToggleShortcut => toggleShortcut.Value;
        internal static KeyboardShortcut ToggleThirdPersonAutoShortcut =>
            toggleThirdPersonAutoShortcut.Value;
        internal static bool ThirdPersonAuto => thirdPersonAuto.Value;
        internal static int ThirdPersonAutoDistance =>
            thirdPersonAutoDistance.Value;
        internal static int AutomaticReturnDelay => automaticReturnDelay.Value;
        internal static int HeadBobStrength => headBobStrength.Value;

        // Creates the saved configuration entries used by the mod.
        internal static void Initialize(ConfigFile config)
        {
            enabled = config.Bind(
                "Camera", "FirstPersonEnabled", false,
                "Whether first-person view is enabled at minimum camera zoom.");
            fieldOfView = config.Bind(
                "Camera", "FieldOfView", DefaultFieldOfView,
                new ConfigDescription(
                    "Field of view shared by first-person, third-person, and free-fly cameras.",
                    new AcceptableValueRange<float>(
                        MinimumFieldOfView, MaximumFieldOfView)));
            firstPersonFieldOfViewBonus = config.Bind(
                "First Person", "FirstPersonFieldOfViewBonus",
                DefaultFirstPersonFieldOfViewBonus,
                new ConfigDescription(
                    "Additional field of view applied only in first person.",
                    new AcceptableValueRange<float>(
                        0f, MaximumFirstPersonFieldOfViewBonus)));
            toggleShortcut = config.Bind(
                "First Person", "ToggleShortcut",
                new KeyboardShortcut(UnityEngine.KeyCode.F6),
                "Shortcut used to enable or disable automatic first-person view.\n" +
                "\nExamples: Mouse2 for the middle mouse button.\n" +
                "\nMouse3/Mouse4 for the Forward/Back side button.\n" +
                "\nSpace + LeftControl for Left Ctrl + Space.\n" +
                "\nhttps://docs.unity3d.com/ScriptReference/KeyCode.html");
            toggleThirdPersonAutoShortcut = config.Bind(
                "Third Person - Auto", "ToggleThirdPersonAutoShortcut",
                new KeyboardShortcut(
                    UnityEngine.KeyCode.F6, UnityEngine.KeyCode.LeftAlt),
                "Shortcut used to enable or disable automatic third person during actions.");
            thirdPersonAuto = config.Bind(
                "Third Person - Auto", "ThirdPersonAuto", false,
                "Whether actions such as attacking, blocking, chopping, mining, or building temporarily move the camera to third person.");
            thirdPersonAutoDistance = config.Bind(
                "Third Person - Auto", "ThirdPersonAutoDistance",
                DefaultThirdPersonAutoDistance,
                new ConfigDescription(
                    "Camera distance in meters while attacking or blocking.",
                    new AcceptableValueRange<int>(1, 10)));
            automaticReturnDelay = config.Bind(
                "Third Person - Auto", "AutomaticReturnDelay",
                DefaultAutomaticReturnDelay,
                new ConfigDescription(
                    "ThirdPersonAuto delay before returning to first person after an action.",
                    new AcceptableValueRange<int>(1, 10)));
            headBobStrength = config.Bind(
                "First Person", "HeadBobStrength", DefaultHeadBobStrength,
                new ConfigDescription(
                    "First-person head bob strength. Set to 0 to disable head bob.",
                    new AcceptableValueRange<int>(0, 3)));
            SetFieldOfView(fieldOfView.Value);
        }

        // Saves whether first person is enabled.
        internal static void SetEnabled(bool value)
        {
            enabled.Value = value;
            ConfigWatcher.IgnoreCurrentFileVersion();
        }

        // Saves whether actions temporarily use third person.
        internal static void SetThirdPersonAuto(bool value)
        {
            thirdPersonAuto.Value = value;
            ConfigWatcher.IgnoreCurrentFileVersion();
        }

        // Saves a field of view after applying its supported limit.
        internal static void SetFieldOfView(float value)
        {
            fieldOfView.Value = System.Math.Max(
                MinimumFieldOfView,
                System.Math.Min(value, MaximumFieldOfView));
            ConfigWatcher.IgnoreCurrentFileVersion();
        }

        // Restores every setting and recreates the configuration file.
        internal static void RestoreDefaults(ConfigFile config)
        {
            bool saveOnConfigSet = config.SaveOnConfigSet;
            config.SaveOnConfigSet = false;
            try
            {
                enabled.Value = false;
                fieldOfView.Value = DefaultFieldOfView;
                firstPersonFieldOfViewBonus.Value =
                    DefaultFirstPersonFieldOfViewBonus;
                toggleShortcut.Value = new KeyboardShortcut(UnityEngine.KeyCode.F6);
                toggleThirdPersonAutoShortcut.Value = new KeyboardShortcut(
                    UnityEngine.KeyCode.F6, UnityEngine.KeyCode.LeftAlt);
                thirdPersonAuto.Value = false;
                thirdPersonAutoDistance.Value =
                    DefaultThirdPersonAutoDistance;
                automaticReturnDelay.Value = DefaultAutomaticReturnDelay;
                headBobStrength.Value = DefaultHeadBobStrength;
            }
            finally
            {
                config.SaveOnConfigSet = saveOnConfigSet;
            }

            config.Save();
            ConfigWatcher.IgnoreCurrentFileVersion();
        }
    }
}
