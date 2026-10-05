using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using System.Text;
static void Check(bool ok, string message) { if (!ok) throw new Exception(message); }
string path = ConfigurationService.GetNativeConfigurationPath("");
await File.WriteAllTextAsync(path, "[Host]\nmodules_root=keep\n[betterendfield.actions]\nschema_version=3\nenabled=true\ncharacters=liino\nexternal_loop=true\n", Encoding.Unicode);
var old = await ConfigurationService.LoadModConfigurationAsync("");
Check(old.LiinoCleanDashEnabled && old.ContinuousSpecialDashLiinoEnabled, "legacy default");
foreach (bool dash in new[]{true,false}) foreach (bool clean in new[]{true,false}) {
 var config = new ModConfiguration { ContinuousSpecialDashAglinaEnabled=true, ContinuousSpecialDashLiinoEnabled=dash, LiinoCleanDashEnabled=clean };
 await ConfigurationService.SaveActionConfigurationAsync(config);
 var loaded = await ConfigurationService.LoadModConfigurationAsync("");
 Check(loaded.LiinoCleanDashEnabled==clean && loaded.ContinuousSpecialDashLiinoEnabled==dash && loaded.ContinuousSpecialDashAglinaEnabled, "save/reload independent preference");
 var ini=await File.ReadAllTextAsync(path);
 Check(ini.Contains("external_loop=true") && ini.Contains("modules_root=keep"), "preservation");
 var full = config.ToIni();
 Check(full.Contains($"liino_clean={clean.ToString().ToLowerInvariant()}"), "full save");
}
Console.WriteLine("UI config smoke passed: legacy default, on/off round-trip, disabled preference, full save, external_loop and other sections.");
