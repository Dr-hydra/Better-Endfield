using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace EFStartProbe;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public sealed class ProbePlugin : BasePlugin
{
    public const string PluginGuid = "dev.drhydra.efstartprobe";
    public const string PluginName = "EF Start Probe";
    public const string PluginVersion = "0.1.0";

    private ProbeRuntime? runtime;

    public override void Load()
    {
        runtime = new ProbeRuntime(Log);
#if EFSTART_STUB
        runtime.Start();
#else
        var behaviour = AddComponent<ProbeBehaviour>();
        behaviour.Initialize(runtime);
        runtime.Start();
#endif
        Log.LogInfo("EF Start Probe loaded in read-only mode.");
    }

    public override bool Unload()
    {
        runtime?.Dispose();
        runtime = null;
        return true;
    }
}
