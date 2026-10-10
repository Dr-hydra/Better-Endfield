using System.Buffers.Binary;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using BetterEndfieldNext.UI.Services;

internal static class Program
{
    private static int checks;
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
        checks++;
    }

    private static byte[] Package()
    {
        // One synthetic Wwise media entry. No game payload or executable.
        var bytes = new byte[72];"AKPK"u8.CopyTo(bytes);
        void U(int offset, uint value) => BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(offset), value);
        U(4,48);U(8,1);U(20,28);U(28,1);U(32,100);U(36,1);
        BinaryPrimitives.WriteUInt64LittleEndian(bytes.AsSpan(40),16);U(48,56);U(52,2);
        return bytes;
    }

    public static async Task Main(string[] args)
    {
        if (args.Length==4 && args[0]=="--published-manifest")
        {
            string dependencies=Path.GetFullPath(args[3]);
            System.Runtime.Loader.AssemblyLoadContext.Default.Resolving+=(_,name)=>
            {
                string path=Path.Combine(dependencies,name.Name+".dll");
                return File.Exists(path)?System.Reflection.Assembly.LoadFrom(path):null;
            };
            var assembly=System.Reflection.Assembly.LoadFrom(Path.GetFullPath(args[1]));
            System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver(assembly,(name,_,_)=>
            {
                string path=Path.Combine(dependencies,name.EndsWith(".dll",StringComparison.OrdinalIgnoreCase)?name:name+".dll");
                return System.Runtime.InteropServices.NativeLibrary.TryLoad(path,out nint handle)?handle:0;
            });
            Type type=assembly.GetType(args[2],throwOnError:true)!;
            string json="{\"Product\":\"BetterEndfieldNext.XInputProxy\",\"ProxyFile\":\"xinput1_4.dll\",\"Sha256\":\"ABC\",\"InstallRoot\":\"fixture\",\"InstalledUtc\":\"2026-10-10T00:00:00+00:00\"}";
            object value=JsonSerializer.Deserialize(json,type)!;
            using var roundtrip=JsonDocument.Parse(JsonSerializer.Serialize(value,type));
            Check(roundtrip.RootElement.GetProperty("Product").GetString()=="BetterEndfieldNext.XInputProxy","published manifest product changed");
            Check(roundtrip.RootElement.GetProperty("Sha256").GetString()=="ABC","published manifest digest changed");
            Check(roundtrip.RootElement.GetProperty("InstallRoot").GetString()=="fixture","published manifest root changed");
            Console.WriteLine("PASS actual published obfuscated manifest JSON roundtrip.");
            return;
        }
        string root=Path.Combine(Path.GetFullPath(args[0]),"desktop-launch-"+Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        ConfigurationService.SettingsDirectory=Path.Combine(root,"settings");
        string game=Path.Combine(root,"game","Endfield.exe");Directory.CreateDirectory(Path.GetDirectoryName(game)!);
        await File.WriteAllTextAsync(game,"Synthetic, never executed.");
        string source=Path.Combine(root,"install","payloads","xinput1_4.dll");Directory.CreateDirectory(Path.GetDirectoryName(source)!);
        await File.WriteAllBytesAsync(source,[1,2,3,4]);
        string injector=Path.Combine(root,"install","loaders","BetterEndfieldNext.Injector.exe");
        Directory.CreateDirectory(Path.GetDirectoryName(injector)!);await File.WriteAllTextAsync(injector,"Synthetic, never executed.");
        RuntimePathDiscoveryService.BundledInjectorPath=injector;
        string target=Path.Combine(Path.GetDirectoryName(game)!,"xinput1_4.dll");
        string manifest=Path.Combine(Path.GetDirectoryName(game)!,"BetterEndfieldNext.xinput.install.json");
        string pck=Path.Combine(Path.GetDirectoryName(game)!,"Endfield_Data","Persistent","VFS","fixture.chk");
        Directory.CreateDirectory(Path.GetDirectoryName(pck)!);await File.WriteAllBytesAsync(pck,Package());
        var request=new[]{new VoiceCatalogRequest("aglina","chr_0013_aglina","Japanese")};
        var stopwatch=Stopwatch.StartNew();
        VoiceCatalogPreparation voice=await VoiceCatalogService.PrepareAsync(game,request);
        await VoiceCatalogService.CommitAsync(voice);
        Check(voice.FileNames.Count==1,"voice preparation did not complete before loader installation");
        string catalog=Path.Combine(ConfigurationService.SettingsDirectory,"catalog",voice.FileNames[0]);
        byte[] original=await File.ReadAllBytesAsync(catalog);DateTime stamp=File.GetLastWriteTimeUtc(catalog);
        Check(original.AsSpan(0,8).SequenceEqual("BEVCAT01"u8),"invalid prepared voice catalog");
        await VoiceCatalogService.PrepareAsync(game,request);
        Check(File.GetLastWriteTimeUtc(catalog)==stamp,"current voice catalog was regenerated");
        Check((await File.ReadAllBytesAsync(catalog)).SequenceEqual(original),"current voice catalog changed");
        Console.WriteLine($"Voice prepare+commit completed in {stopwatch.ElapsedMilliseconds} ms; next stage: XInput.");

        var status=await XInputDeploymentService.InspectAsync(game,injector);
        Check(status.State==XInputDeploymentState.NotInstalled,"initial proxy status incorrect");
        status=await XInputDeploymentService.InstallAsync(game,injector);
        Check(status.State==XInputDeploymentState.Installed && File.Exists(manifest),"installation did not persist ownership");
        using (var json=JsonDocument.Parse(await File.ReadAllTextAsync(manifest)))
        {
            Check(json.RootElement.GetProperty("Product").GetString()=="BetterEndfieldNext.XInputProxy","manifest product contract changed");
            Check(json.RootElement.GetProperty("ProxyFile").GetString()=="xinput1_4.dll","manifest filename contract changed");
        }
        status=await XInputDeploymentService.InspectAsync(game,injector);
        Check(status.State==XInputDeploymentState.Installed && status.CanUninstall && !status.CanInstall,"post-install status did not refresh");
        await XInputDeploymentService.InstallAsync(game,injector);
        Check(File.Exists(manifest),"idempotent launcher installation lost the manifest");
        File.Delete(manifest);
        await XInputDeploymentService.InstallAsync(game,injector);
        Check(File.Exists(manifest),"same-hash proxy left by interrupted installation was not repaired");
        await File.WriteAllBytesAsync(source,[5,6,7,8]);
        status=await XInputDeploymentService.InspectAsync(game,injector);
        Check(status.State==XInputDeploymentState.UpdateAvailable && status.CanInstall && status.CanUninstall,"owned old proxy manifest could not be deserialized");
        await XInputDeploymentService.InstallAsync(game,injector);
        Check((await File.ReadAllBytesAsync(source)).SequenceEqual(await File.ReadAllBytesAsync(target)),"proxy update failed");
        await XInputDeploymentService.UninstallAsync(game,injector);
        status=await XInputDeploymentService.InspectAsync(game,injector);
        Check(status.State==XInputDeploymentState.NotInstalled && !File.Exists(manifest),"post-uninstall state incorrect");
        await File.WriteAllBytesAsync(target,[9,9,9]);
        status=await XInputDeploymentService.InspectAsync(game,injector);
        Check(status.State==XInputDeploymentState.Conflict && !status.CanInstall && !status.CanUninstall,"unknown proxy was not protected");
        try {await XInputDeploymentService.InstallAsync(game,injector);throw new Exception("unknown proxy was overwritten");}
        catch(IOException) {Check((await File.ReadAllBytesAsync(target)).SequenceEqual(new byte[]{9,9,9}),"unknown proxy changed");}
        await VoiceCatalogService.CommitAsync(new VoiceCatalogPreparation([]));
        Check(!File.Exists(catalog),"disabled voice catalog was not retired");
        Console.WriteLine($"PASS {checks} desktop voice preparation and XInput installation/status/ownership checks.");
    }
}
