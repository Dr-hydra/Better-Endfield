using AnimeStudio;
using Newtonsoft.Json;
var game=GameManager.GetGame(GameType.ArknightsEndfield);
var manager=new AssetsManager { Game=game };
if(args[0]=="verifybundle") {
    using var r=new FileReader(args[1]);var bundle=new VFSFile(r,args[1],game.Type);
    if(bundle.fileList.Count!=1)throw new Exception("Expected one native serialized file");
    var file=bundle.fileList[0];file.stream.Position=0;
    string extracted=args[1]+".assets";
    using(var output=File.Create(extracted))file.stream.CopyTo(output);
    Console.WriteLine("Endfield archive decoded: "+file.fileName);
    args[0]="verify";args[1]=extracted;
}
if(args[0]=="extract") {
    Directory.CreateDirectory(args[2]); int found=0;
    foreach(var path in Directory.GetFiles(args[1],"*.ab")) {
        using var reader=new FileReader(path);
        var bundle=new VFSFile(reader,path,game.Type);
        foreach(var file in bundle.fileList) {
            string? side=file.fileName=="CAB-7772877a4d28dd736840e8579433319c"?"left":file.fileName=="CAB-7e5ac3233687f1f1cd9130a07eb92de8"?"right":null;
            if(side==null) { file.stream.Dispose(); continue; }
            file.stream.Position=0;
            using var r=new FileReader(file.fileName,file.stream,true);
            var sf=new SerializedFile(r,manager);
            file.stream.Position=0; using(var copy=File.Create(Path.Combine(args[2],side+"-original.assets"))) file.stream.CopyTo(copy);
            var objects=new List<object>();
            foreach(var obj in sf.m_Objects) {
                var or=new ObjectReader(r,sf,obj,game);or.Reset();
                var tree=TypeTreeHelper.ReadType(obj.serializedType.m_Type,or);
                if(or.Position!=obj.byteStart+obj.byteSize) throw new Exception("TypeTree did not consume entire object");
                File.WriteAllText(Path.Combine(args[2],$"{side}-{obj.classID}-nodes.json"),JsonConvert.SerializeObject(obj.serializedType.m_Type.m_Nodes));
                File.WriteAllText(Path.Combine(args[2],$"{side}-{obj.classID}-tree.json"),JsonConvert.SerializeObject(tree));
                objects.Add(new { obj.m_PathID,obj.classID,obj.byteStart,obj.byteSize,obj.typeID });
            }
            File.WriteAllText(Path.Combine(args[2],side+"-metadata.json"),JsonConvert.SerializeObject(new {archive=Path.GetFullPath(path),file.fileName,sf.unityVersion,version=(int)sf.header.m_Version,dataOffset=sf.header.m_DataOffset,objects},Formatting.Indented));
            Console.WriteLine($"{side}: {Path.GetFileName(path)}, {objects.Count} objects, {sf.unityVersion}");found++;
        }
        if(found==2)break;
    }
    if(found!=2)throw new Exception("Missing native clips");
} else if(args[0]=="verify") {
    using var r=new FileReader(args[1]);var sf=new SerializedFile(r,manager);
    foreach(var obj in sf.m_Objects) {
        var or=new ObjectReader(r,sf,obj,game);or.Reset();
        var tree=TypeTreeHelper.ReadType(obj.serializedType.m_Type,or);
        if(or.Position!=obj.byteStart+obj.byteSize)throw new Exception("TypeTree trailing/missing data");
        if(obj.classID==74) {
            or.Reset();var clip=new AnimationClip(or);
            if(or.Position!=obj.byteStart+obj.byteSize)throw new Exception("Endfield AnimationClip reader trailing/missing data");
            var a=clip.m_AclCompressedBuffer;
            Console.WriteLine($"PASS {clip.m_Name}: native + TypeTree readers consumed {obj.byteSize} bytes; ACL version={a.Version}, transforms={a.OutputTrackCount}, scalars={a.FloatCurveCount}, events={clip.m_Events.Count}");
        } else if(obj.classID==142) {
            or.Reset();var bundle=new AssetBundle(or);
            Console.WriteLine($"PASS native AssetBundle prefix: preload={bundle.m_PreloadTable.Count}, container={bundle.m_Container.Count}; full TypeTree consumed {obj.byteSize} bytes");
        } else Console.WriteLine($"PASS class {obj.classID}: TypeTree consumed {obj.byteSize} bytes");
        File.WriteAllText(args[1]+$".{obj.classID}.{obj.m_PathID}.readback.json",JsonConvert.SerializeObject(tree));
    }
} else throw new ArgumentException("extract/verify");
