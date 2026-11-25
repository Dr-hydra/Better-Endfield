using System.IO.Compression;
using System.Text.Json;
using BetterEndfieldNext.UI.Services;

string temp=Path.Combine(Path.GetTempPath(),"be-distribution-tests-"+Guid.NewGuid());Directory.CreateDirectory(temp);
string root=GameResourceStore.Root;
string state=Path.Combine(root,"state.json");byte[]? previous=File.Exists(state)?File.ReadAllBytes(state):null;
int checks=0;
void Check(bool ok,string message){if(!ok)throw new Exception(message);Console.WriteLine("PASS "+message);checks++;}
string Archive(string version="91.2.3.1",string platform="windows",string contents="{}",bool traversal=false,bool wrongLength=false)
{
    string path=Path.Combine(temp,Guid.NewGuid()+".zip");
    using(var zip=ZipFile.Open(path,ZipArchiveMode.Create))
    {
        var files=new Dictionary<string,long>();
        foreach(string name in new[]{"model/character-presets.json","model/character-names.json","voice/voice-catalog-index.json"})
        {using var writer=new StreamWriter(zip.CreateEntry(name).Open());writer.Write(contents);files[name]=System.Text.Encoding.UTF8.GetByteCount(contents)+(wrongLength?1:0);}
        using(var writer=new StreamWriter(zip.CreateEntry("manifest.json").Open()))writer.Write(JsonSerializer.Serialize(new{schemaVersion=1,kind="betterendfield-resource-pack",packageType="core",platform,version,gameVersion="91.2.3",files}));
        if(traversal){using var writer=new StreamWriter(zip.CreateEntry("../escape.json").Open());writer.Write("{}");}
    }
    return path;
}
async Task Reject(string path,string message){try{await DistributionService.ImportAsync(path,CancellationToken.None);throw new Exception("Unexpected acceptance: "+message);}catch(InvalidDataException){Check(true,message);}}
try
{
    using(var doc=JsonDocument.Parse(JsonSerializer.Serialize(new{resources=new object[]{
        new{packageType="core",platform="windows",version="1.6.3.3",gameVersion="1.6.3",resourceApi=1},
        new{packageType="core",platform="windows",version="1.7.2.2",gameVersion="1.7.2",resourceApi=1},
        new{packageType="preview",platform="android",version="1.6.4.3",gameVersion="1.6.4",resourceApi=1},
        new{packageType="preview",platform="windows",version="1.6.3.3",gameVersion="1.6.3",resourceApi=1}}})))
    {
        Check(DistributionService.SelectLatestCandidate(doc.RootElement,"core")?.GetProperty("version").GetString()=="1.7.2.2","select latest core regardless of baseline");
        Check(DistributionService.SelectLatestCandidate(doc.RootElement,"preview")?.GetProperty("version").GetString()=="1.6.3.3","select latest preview across game versions");
        var snap=new GameResourceStore.Snapshot(temp,"1.6.3.3",doc.RootElement);
        Check(DistributionService.IsUpToDate(snap,"1.6.3.3"),"cross-baseline installed preview does not re-download");
        Check(!DistributionService.IsUpToDate(snap,"1.7.2.1"),"newer preview version triggers upgrade");
    }
    await Reject(Archive(traversal:true),"reject archive traversal");
    await Reject(Archive(platform:"android"),"reject wrong platform");
    await Reject(Archive(wrongLength:true),"reject truncated inventory");
    await DistributionService.ImportAsync(Archive(contents:"{\"x\":1}"),CancellationToken.None);
    Check(GameResourceStore.Resolve("core")?.Version=="91.2.3.1","install and activate validated pack");
    await Reject(Archive(contents:"{\"x\":2}"),"reject same-length version mutation");
    await DistributionService.ImportAsync(Archive("91.2.3.0"),CancellationToken.None);
    Check(GameResourceStore.Resolve("core")?.Version=="91.2.3.0","manual rollback activation");
    using(var json=JsonDocument.Parse(File.ReadAllText(state)))Check(!json.RootElement.GetProperty("auto.core.windows").GetBoolean(),"rollback pins automatic updates");
    using(var token=new CancellationTokenSource()){token.Cancel();try{await DistributionService.ImportAsync(Archive(),token.Token);throw new Exception("Cancellation ignored");}catch(OperationCanceledException){Check(GameResourceStore.Resolve("core")?.Version=="91.2.3.0","cancellation preserves active version");}}
    var staging=Path.Combine(root,"staging");Check(!Directory.Exists(staging)||Directory.GetDirectories(staging).Length==0,"failed imports clean staging");
    Console.WriteLine($"{checks} checks passed");
}
finally
{
    foreach(string version in new[]{"91.2.3.0","91.2.3.1"}){string folder=Path.Combine(root,"core/windows",version);if(Directory.Exists(folder))Directory.Delete(folder,true);}
    if(previous is null){if(File.Exists(state))File.Delete(state);}else File.WriteAllBytes(state,previous);
    Directory.Delete(temp,true);
}
