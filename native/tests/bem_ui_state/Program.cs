using System.Text;
using System.Text.Json.Nodes;
using BetterEndfield.UI.Services;

// The host test exercises locale selection without the WinRT app-language API.
namespace Windows.Globalization
{
    internal static class ApplicationLanguages
    {
        public static string PrimaryLanguageOverride { get; set; } = "";
    }
}

namespace BetterEndfield.UI.Services
{
    internal static class ConfigurationService
    {
        internal static string SettingsDirectory { get; set; } = "";
    }
}

internal static class Program
{
    private static int _checks;
    private static void Check(bool value, string message) { _checks++; if (!value) throw new Exception(message); }
    private static void Reject(Action action, string message)
    {
        try { action(); } catch (Exception e) when (e is IOException or InvalidDataException or InvalidOperationException or OverflowException) { _checks++; return; }
        throw new Exception(message);
    }

    private static JsonObject Manifest(int minor = 3)
    {
        var manifest = JsonNode.Parse("""
        {"schema":1,"package_id":"shape.fixture","name":"Shape fixture","author":"Fixture","version":"1",
         "target":{"platform":"windows-x64","character_id":"chr_test"},
         "option_groups":[{"id":"body","name":"Body","default":"on","choices":[{"id":"on","name":"On"},{"id":"off","name":"Off"}]}],
         "parameters":[{"id":"size","name":"Size","min":0,"max":1000,"default":500,"neutral":0,"step":10},
                       {"id":"trim","name":"Trim","min":100,"max":900,"default":500,"neutral":100,"step":20,"available_when":{"eq":["body","on"]}}],
         "appearances":[{"id":"base","name":"Base"}],"default_appearance_id":"base"}
        """)!.AsObject();
        if (minor < 3) manifest.Remove("parameters");
        return manifest;
    }

    private static void Write(string path, JsonObject manifest, ushort minor = 3)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(manifest.ToJsonString());
        using var writer = new BinaryWriter(File.Create(path));
        writer.Write(new byte[] {66,69,77,0,80,75,71,0}); writer.Write((ushort)1); writer.Write(minor);
        writer.Write(40u); writer.Write((ulong)(40 + bytes.Length)); writer.Write((ulong)bytes.Length); writer.Write(0u); writer.Write(0u); writer.Write(bytes);
    }

    private static async Task Main(string[] args)
    {
        LocalizationService.Instance.ApplyLanguage("zh-CN");
        string root = Path.Combine(Path.GetTempPath(), "bem-ui-state-" + Guid.NewGuid());
        Directory.CreateDirectory(root); ConfigurationService.SettingsDirectory = root;
        try
        {
            var service = new BemPackageService(); Directory.CreateDirectory(Path.Combine(service.Root, "packages"));
            string file = Path.Combine(service.Root, "packages", "shape.fixture.bem"), ini = Path.Combine(service.Root, "runtime.ini");
            Write(file, Manifest());
            var metadata = BemPackageService.ReadMetadata(file);
            Check(metadata.Parameters.Count == 2 && metadata.EncodedParameters() == "size:500&trim:500", "1.3 metadata/defaults not decoded");
            Check(metadata.Parameters[0].Snap(826) == 830, "Slider step does not snap from minimum");
            metadata.SelectedOptions["body"] = "off";
            Check(!metadata.ParameterAvailable(metadata.Parameters[1]) && metadata.SelectedParameters["trim"] == 500, "Hidden parameter lost saved value");
            Reject(() => metadata.RestoreParameters("size:20&size:30"), "Accepted duplicate saved parameter");
            Reject(() => metadata.RestoreParameters("size:-1"), "Accepted negative parameter");
            metadata.RestoreParameters("trim:740&size:820&retired:620");
            Check(metadata.EncodedParameters() == "size:820&trim:740" && metadata.RememberedParameters().Contains("retired:620"), "Unknown saved weights contaminated runtime selection");
            await File.WriteAllTextAsync(ini, "[CustomModel]\nhot_switch=true\nfuture_option=retained\n[Mod.shape.fixture]\nenabled=true\noptions=body:off\nparameters=size:820&trim:740\nfuture_mod_option=kept\n[OtherExtension]\nvalue=preserved\n");
            service.Load(); await service.SaveAsync();
            string saved = await File.ReadAllTextAsync(ini);
            Check(saved.Contains("future_option=retained") && saved.Contains("future_mod_option=kept") && saved.Contains("[OtherExtension]"), "Saving erased future configuration keys");
            Check(service.Packages[0].EncodedParameters() == "size:820&trim:740" && !service.Packages[0].ParameterAvailable(service.Packages[0].Parameters[1]), "Load did not preserve hidden slider selection");
            Write(file, Manifest(1), 1); service.Load(); await service.SaveAsync();
            Check(service.Packages[0].Parameters.Count == 0 && service.Packages[0].EncodedParameters() == "", "Downgrade exposed unsupported parameters");
            Check((await File.ReadAllTextAsync(ini)).Contains("parameters_saved=size:820&trim:740"), "Downgrade erased parameter memory");
            Write(file, Manifest()); service.Load();
            Check(service.Packages[0].EncodedParameters() == "size:820&trim:740", "Upgrade failed to restore parameters");
            var resized = Manifest(); resized["parameters"]![0]!["max"] = 600; Write(file, resized); service.Load();
            Check(service.Packages[0].EncodedParameters() == "size:500&trim:740" && service.Notices.Count == 1, "Range change did not repair only invalid slider");
            var zeroOptions = Manifest(); zeroOptions["option_groups"] = new JsonArray(); Write(file, zeroOptions);
            Check(BemPackageService.ReadMetadata(file).OptionGroups.Count == 0, "Shape-only 1.3 package rejected");
            Write(file, Manifest(), 4); Reject(() => BemPackageService.ReadMetadata(file, true), "Skip validation accepted unsupported minor");
            var bad = Manifest(); bad["parameters"]![0]!["step"] = 0; Write(file, bad);
            Reject(() => BemPackageService.ReadMetadata(file, true), "Skip validation bypassed zero step structure");
            bad = Manifest(); bad["parameters"]![0]!["default"] = 501; Write(file, bad);
            Reject(() => BemPackageService.ReadMetadata(file, true), "Accepted misaligned default weight");
            bad = Manifest(); bad["parameters"]!.AsArray().Add(bad["parameters"]![0]!.DeepClone()); Write(file, bad);
            Reject(() => BemPackageService.ReadMetadata(file, true), "Accepted duplicate parameter metadata");
            Write(file, Manifest(0), 0);
            Check(!BemPackageService.ReadMetadata(file).IsComposable, "Legacy 1.0 appearance package regressed");
            Check(BemInspectionSummary.Read("{\"format\":\"BEMv1.3\"}").AlreadyPackaged, "Report UI failed to recognize 1.3");
            string report = new JsonObject { ["package"] = Manifest() }.ToJsonString();
            Check(BemReportPresentation.Package(report).Contains("形态滑条：Size、Trim"), "Package report omitted slider metadata");
            if (args.Length > 0)
            {
                byte[] original = await File.ReadAllBytesAsync(args[0]);
                var actual = BemPackageService.ReadMetadata(args[0]);
                Check(actual.Parameters.Count > 0 && actual.EncodedParameters().Length > 0, "Creator 1.3 fixture metadata not decoded by Windows");
                actual.SelectedParameters[actual.Parameters[0].Id] = actual.Parameters[0].Snap(500);
                Check(actual.EncodedParameters().Contains(":500"), "Creator fixture selection not represented in Windows wire format");
                Check(original.SequenceEqual(await File.ReadAllBytesAsync(args[0])), "Windows metadata reader changed package bytes");
            }
            string remembered = service.Packages[0].EncodedParameters();
            LocalizationService.Instance.ApplyLanguage("en-US");
            Check(BemInspectionSummary.Read("{\"format\":\"BEMv1.3\"}").Title == "This BEM can be imported directly", "English inspection summary remained Chinese");
            Check(BemReportPresentation.Package(report).Contains("Shape sliders: Size, Trim"), "English report did not localize labels/separators");
            Check(service.Packages[0].EncodedParameters() == remembered, "Language switch changed saved shape parameters");
            var authored = Manifest(); authored["name"] = "作者作品";
            Check(BemReportPresentation.Package(new JsonObject { ["package"] = authored }.ToJsonString()).Contains("作者作品"), "Locale selection changed author metadata");
            LocalizationService.Instance.ApplyLanguage("zh-CN");
            Check(BemReportPresentation.Package(report).Contains("形态滑条：Size、Trim"), "Chinese report did not restore labels/separators");
            Console.WriteLine($"PASS {_checks} Windows BEM metadata/persistence/upgrade and bilingual report checks");
        }
        finally { Directory.Delete(root, true); }
    }
}
