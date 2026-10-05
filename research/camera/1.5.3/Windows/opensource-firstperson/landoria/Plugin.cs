using BepInEx;
using HarmonyLib;
using Landoria.Shared;

namespace Landoria.FirstPerson
{
    // Starts and stops the First Person mod.
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "Landoria.FirstPerson";
        private const string PluginName = "Landoria.FirstPerson";
        private const string PluginVersion = "1.0.14";

        private Harmony _harmony;

        private void Awake()
        {
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            _harmony.PatchAll();
            Preference.Initialize(Config);
            ConfigWatcher.Initialize(
                Config,
                Logger,
                "First Person",
                () => Preference.RestoreDefaults(Config),
                () =>
                {
                    Mode.SetEnabled(Preference.Enabled);
                    Mode.ApplyConfiguredFieldOfView(GameCamera.instance);
                });
            Logger.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        // Applies configuration file changes from Unity's main thread.
        private void Update()
        {
            ConfigWatcher.Update();
        }

        private void OnDestroy()
        {
            ConfigWatcher.Dispose();
            Mode.Reset();
            _harmony?.UnpatchSelf();
            _harmony = null;
            Logger.LogInfo($"{PluginName} {PluginVersion} is unloaded.");
        }
    }
}
