using AnimeStudio;
using Newtonsoft.Json;
// Generic read-only TypeTree dumper for Endfield bundles.
//   dump <bundle.ab|dir> <outdir> [cabFilter] [classIdFilter]
// Writes <CAB>--<pathid>.<classID>.tree.json per object plus <CAB>.objects.json.
var game=GameManager.GetGame(GameType.ArknightsEndfield);
var manager=new AssetsManager { Game=game };
if(args.Length<3||args[0]!="dump") throw new ArgumentException("dump <bundle|dir> <outdir> [cabFilter] [classId]");
string? cabFilter=args.Length>3?args[3]:null; int? classFilter=args.Length>4?int.Parse(args[4]):null;
Directory.CreateDirectory(args[2]);
var paths=Directory.Exists(args[1])?Directory.GetFiles(args[1],"*.ab"):new[]{args[1]};
int dumped=0;
foreach(var path in paths) {
    using var reader=new FileReader(path);
    var bundle=new VFSFile(reader,path,game.Type);
    foreach(var file in bundle.fileList) {
        if(cabFilter!=null&&!file.fileName.Contains(cabFilter,StringComparison.OrdinalIgnoreCase)) { file.stream.Dispose(); continue; }
        file.stream.Position=0;
        using var r=new FileReader(file.fileName,file.stream,true);
        var sf=new SerializedFile(r,manager);
        var objects=new List<object>();
        foreach(var obj in sf.m_Objects) {
            objects.Add(new { obj.m_PathID,obj.classID,obj.byteStart,obj.byteSize,obj.typeID });
            if(classFilter!=null&&obj.classID!=classFilter) continue;
            var or=new ObjectReader(r,sf,obj,game);or.Reset();
            var tree=TypeTreeHelper.ReadType(obj.serializedType.m_Type,or);
            if(or.Position!=obj.byteStart+obj.byteSize) { Console.Error.WriteLine($"Skipping non-exact object {obj.m_PathID} class={obj.classID}"); continue; }
            File.WriteAllText(Path.Combine(args[2],$"{file.fileName}--{obj.m_PathID}.{obj.classID}.tree.json"),JsonConvert.SerializeObject(tree));
            File.WriteAllText(Path.Combine(args[2],$"{file.fileName}--{obj.m_PathID}.{obj.classID}.nodes.json"),JsonConvert.SerializeObject(obj.serializedType.m_Type.m_Nodes));
            dumped++;
        }
        File.WriteAllText(Path.Combine(args[2],file.fileName+".objects.json"),JsonConvert.SerializeObject(new {archive=Path.GetFullPath(path),file.fileName,sf.unityVersion,externals=sf.m_Externals.Select(e=>e.fileName),objects},Formatting.Indented));
        Console.WriteLine($"{Path.GetFileName(path)} {file.fileName}: {objects.Count} objects, externals={sf.m_Externals.Count}");
    }
}
Console.WriteLine($"DUMPED {dumped}");
