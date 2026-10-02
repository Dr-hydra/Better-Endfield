using BetterEndfield.UI.Services;
using System.IO.Compression;
using System.Text.Json.Nodes;
using System.Text;
using System.Net;

namespace BetterEndfield.UI.Services { internal static class ConfigurationService { public static string SettingsDirectory { get; set; } = ""; } }
internal static class Program
{
    private static int checks;
    static void Check(bool valid,string message){checks++;if(!valid)throw new Exception(message);}
    static async Task Reject(Func<Task> action,string message){try{await action();}catch(Exception e)when(e is IOException or InvalidDataException or InvalidOperationException or ArgumentException){checks++;return;}throw new Exception(message);}
    static JsonObject Manifest(string id="example.test")=>new(){["format"]=1,["abi"]=1,["id"]=id,["name"]="Example",["author"]="Creator",["version"]="1",["libraries"]=new JsonObject(),["ui"]="ui/index.html",["default_configuration"]=new JsonObject{["unknown"]="retained"}};
    static string Zip(string root,JsonObject manifest, params string[] extras)
    {
        string file=Path.Combine(root,Guid.NewGuid()+".zip");using var zip=ZipFile.Open(file,ZipArchiveMode.Create);
        void Entry(string path,string value){using var writer=new StreamWriter(zip.CreateEntry(path).Open(),new UTF8Encoding(false));writer.Write(value);}
        Entry("module.json",manifest.ToJsonString());Entry("ui/index.html","<!doctype html><html><body>Author UI</body></html>");
        foreach(string extra in extras)Entry(extra,"extra");return file;
    }
    static async Task Main(string[] args)
    {
        string temp=Path.Combine(Path.GetTempPath(),"third-party-ui-"+Guid.NewGuid());Directory.CreateDirectory(temp);
        try
        {
            var service=new ThirdPartyModuleService(Path.Combine(temp,"third-party"));var imported=await service.ImportAsync(Zip(temp,Manifest()));
            Check(imported.Supported&&!imported.Enabled&&imported.Ui=="ui/index.html","UI-only package/default disabled failed");
            await service.SetEnabledAsync(imported.Id,true);var configuration=JsonNode.Parse("{\"label\":\"changed\",\"future\":[1,{\"nested\":true}],\"float\":0.25}")!.AsObject();
            await service.SaveConfigurationAsync(imported.Id,configuration);string old=imported.Directory;
            var update=Manifest();update["version"]="2";update["default_configuration"]=new JsonObject{["label"]="new default"};
            var upgraded=await service.ImportAsync(Zip(temp,update));
            Check(upgraded.Enabled&&JsonNode.DeepEquals(upgraded.State["configuration"],configuration),"Upgrade lost enabled/opaque JSON config");
            Check(upgraded.Directory!=old&&Directory.Exists(old),"Upgrade overwrote a possibly loaded generation");
            Check(service.LoadIndex()["retired_directories"]!.AsArray().Any(n=>n!.GetValue<string>()==old),"Old generation not tracked");
            string before=await File.ReadAllTextAsync(service.IndexPath);
            foreach(string bad in new[]{"../escape","ui/../escape","ui\\escape","C:/escape","/absolute","UI/INDEX.HTML","module.json","native/CON.dll"})
                await Reject(()=>service.ImportAsync(Zip(temp,Manifest(),bad)),"Accepted unsafe/duplicate ZIP entry "+bad);
            Check(before==await File.ReadAllTextAsync(service.IndexPath),"Rejected imports altered installed snapshot");
            await Reject(()=>service.ImportAsync(Zip(temp,Manifest("betterendfield.camera"))),"Accepted internal module overwrite");
            await Reject(()=>service.ImportAsync(Zip(temp,Manifest("voice.character"))),"Accepted Android builtin voice module ID");
            await Reject(()=>service.ImportAsync(Zip(temp,Manifest("VOICE.Character"))),"Accepted builtin voice ID case alias");
            foreach(string dependency in new[]{"voice.character","betterendfield.camera","example.test"})
            {
                var badDependency=Manifest();badDependency["dependencies"]=new JsonArray(dependency);
                await Reject(()=>service.ImportAsync(Zip(temp,badDependency)),"Accepted invalid dependency "+dependency);
            }
            var excessiveDependencies=Manifest();var manyDependencies=new JsonArray();
            for(int i=0;i<129;i++)manyDependencies.Add("example.dependency"+i);
            excessiveDependencies["dependencies"]=manyDependencies;
            await Reject(()=>service.ImportAsync(Zip(temp,excessiveDependencies)),"Accepted >128 dependencies");
            var badAbi=Manifest();badAbi["abi"]=2;await Reject(()=>service.ImportAsync(Zip(temp,badAbi)),"Accepted unsupported ABI");
            var missing=Manifest();missing["ui"]="ui/missing.html";await Reject(()=>service.ImportAsync(Zip(temp,missing)),"Accepted missing web entry");
            var badConfig=Manifest();badConfig["default_configuration"]=new JsonArray();await Reject(()=>service.ImportAsync(Zip(temp,badConfig)),"Accepted non-object config");
            var onlyAndroid=Manifest("example.android");onlyAndroid.Remove("ui");onlyAndroid["libraries"]=new JsonObject{["android-arm64"]="native/libtest.so"};
            var foreign=await service.ImportAsync(Zip(temp,onlyAndroid,"native/libtest.so"));Check(!foreign.Supported&&!foreign.Enabled,"Other-platform native package enabled on Windows");
            await Reject(()=>service.SetEnabledAsync(foreign.Id,true),"Enabled unsupported package");
            await service.MoveAsync(foreign.Id,-1);Check(service.Records()[0].Id==foreign.Id,"Order update failed");
            await service.RemoveAsync(foreign.Id);Check(service.Records().Count==1&&Directory.Exists(foreign.Directory),"Removal deleted loaded generation or retained registry entry");
            var index=service.LoadIndex();Check(index["token"]!.GetValue<string>().Length==64&&index["port"]!.GetValue<int>()>1023,"Invalid private bridge endpoint");
            using var listener=new HttpListener();listener.Prefixes.Add($"http://127.0.0.1:{index["port"]!.GetValue<int>()}/");listener.Start();
            var exchange=Task.Run(async()=>{var context=await listener.GetContextAsync();Check(context.Request.Headers["Authorization"]=="Bearer "+index["token"]!.GetValue<string>(),"Missing native authorization");
                using var reader=new StreamReader(context.Request.InputStream);var request=JsonNode.Parse(await reader.ReadToEndAsync())!;
                Check(request["module_id"]!.GetValue<string>()==imported.Id&&request["request_id"]!.GetValue<string>()=="request.test", "Request identity not bound to installed module");
                Check(JsonNode.DeepEquals(request["body"],new JsonArray(1,"test")),"Opaque array request changed");
                byte[] result=Encoding.UTF8.GetBytes("{\"accepted\":true}");await context.Response.OutputStream.WriteAsync(result);context.Response.Close();});
            Check((await service.RuntimeAsync("send",imported.Id,new JsonArray(1,"test"),"request.test"))["accepted"]!.GetValue<bool>(),"Bridge acceptance response lost");await exchange;
            var statusExchange=Task.Run(async()=>{var context=await listener.GetContextAsync();byte[] result=Encoding.UTF8.GetBytes("{\"connected\":true,\"modules\":[{\"id\":\"example.test\",\"status\":\"ui_only\"},{\"id\":\"another.module\",\"status\":\"active\"}]}");await context.Response.OutputStream.WriteAsync(result);context.Response.Close();});
            var status=await service.RuntimeAsync("status",imported.Id);await statusExchange;
            Check(status["module"]?["status"]?.GetValue<string>()=="ui_only"&&status["modules"] is null,"Status escaped page module scope");
            Check(!ThirdPartyWebBridge.Script.Contains("Bearer")&&!ThirdPartyWebBridge.Script.Contains("module_id"),"Private routing leaked to author JavaScript");
            File.Delete(Path.Combine(upgraded.Directory,"module.json"));
            Check(service.Records().Count==1&&service.Records()[0].Error.Length>0&&!service.Records()[0].Supported,"Broken module lost its removable management card");
            if(args.Length>0)
            {
                var sample=await new ThirdPartyModuleService(Path.Combine(temp,"sdk-import")).ImportAsync(args[0]);
                Check(sample.Id=="example.echo"&&sample.Supported&&!sample.Enabled,"Packaged SDK Echo is not importable/default disabled");
                Check(sample.Manifest["libraries"]?["windows-x64"] is not null&&sample.Manifest["libraries"]?["android-arm64"] is not null,"Packaged SDK Echo is not dual-platform");
                Check(File.Exists(Path.Combine(sample.Directory,sample.Ui)),"Packaged SDK Echo UI missing after import");
            }
            Console.WriteLine($"PASS {checks} Windows third-party ZIP, immutable updates, configuration and HTTP bridge checks");
        }
        finally{Directory.Delete(temp,true);}
    }
}
