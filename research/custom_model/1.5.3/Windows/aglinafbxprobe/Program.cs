using AnimeStudio;
using System.Text.Json;
var output=Path.GetFullPath(args[1]); Directory.CreateDirectory(output);
var json=new JsonSerializerOptions { IncludeFields=true, WriteIndented=true, MaxDepth=256, NumberHandling=System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals };
foreach(var type in new[]{ClassIDType.GameObject,ClassIDType.Transform,ClassIDType.Animator,ClassIDType.Avatar,ClassIDType.Mesh,ClassIDType.SkinnedMeshRenderer,ClassIDType.MeshFilter,ClassIDType.MeshRenderer,ClassIDType.AnimationClip,ClassIDType.AssetBundle}) TypeFlags.SetType(type,true,false);
Logger.Default=new ConsoleLogger();
var manager=new AssetsManager { Game=GameManager.GetGame(GameType.ArknightsEndfield) };
manager.LoadFiles(Directory.GetFiles(Path.GetFullPath(args[0]),"*.ab"));
var all=manager.assetsFileList.SelectMany(f=>f.Objects).ToArray();
Console.WriteLine("COUNTS "+string.Join(", ",all.GroupBy(o=>o.GetType().Name).Select(g=>$"{g.Key}={g.Count()}")));
foreach(var av in all.OfType<Avatar>()) {
 Console.WriteLine($"AVATAR {av.m_Name} paths={av.m_TOS.Count}");
 File.WriteAllText(Path.Combine(output,"avatar.json"),JsonSerializer.Serialize(new{av.m_Name,av.m_TOS,av.m_Avatar,av.m_HumanDescription},json));
}
var roots=all.OfType<GameObject>().Where(g=>g.m_Transform!=null&&!g.m_Transform.m_Father.TryGet(out var p)).ToArray();
foreach(var go in roots) Console.WriteLine($"ROOT {go.m_Name} id={go.m_PathID} animator={go.m_Animator!=null}");
var clips=all.OfType<AnimationClip>().Where(c=>c.m_Name.StartsWith("A_actor_aglina_sprint_dash_sp_")).ToArray();
foreach(var clip in clips) Console.WriteLine($"CLIP {clip.m_Name} fps={clip.m_SampleRate}");
foreach(var go in roots.Where(g=>g.m_Name=="chr_0013_aglina_postmodel")) {
 var opt=new ModelConverter.Options{game=manager.Game, collectAnimations=false, exportMaterials=false, materials=new(), uvs=Enumerable.Range(0,8).ToDictionary(i=>$"UV{i}",i=>(true,0)), texs=new()};
 var model=new ModelConverter(go,opt,clips);
 Console.WriteLine($"CONVERT {go.m_Name} meshes={model.MeshList.Count} animations={model.AnimationList.Count} tracks={string.Join(',',model.AnimationList.Select(a=>a.TrackList.Count))}");
 object Frame(ImportedFrame f)=>new{f.Name,f.Path,f.LocalPosition,f.LocalRotation,f.LocalScale,children=Enumerable.Range(0,f.Count).Select(i=>Frame(f[i])).ToArray()};
 File.WriteAllText(Path.Combine(output,"hierarchy.json"),JsonSerializer.Serialize(Frame(model.RootFrame),json));
 model.MeshList.RemoveAll(m=>!m.Path.Contains("/lod0/"));
 var fbxOptions=new Fbx.ExportOptions{exportAllNodes=true,exportSkins=true,exportAnimations=true,exportBlendShape=false,castToBone=true,boneSize=1,scaleFactor=100,fbxVersion=3,fbxFormat=0};
 if(args.Contains("--baked")) {
  using var report=JsonDocument.Parse(File.ReadAllText(Path.Combine(output,"unity-bake-report.json")));
  var paths=report.RootElement.GetProperty("paths").EnumerateArray().Select(p=>p.GetString()).ToArray();
  var frames=paths.Select(p=>string.IsNullOrEmpty(p)?model.RootFrame:model.RootFrame.FindFrameByPath(p)).ToArray();
  if(frames.Any(f=>f==null)) throw new Exception("Unmapped baked bone: "+string.Join(',',paths.Where((p,i)=>frames[i]==null)));
  foreach(var side in new[]{"left","right"}) {
   var isLoop=args.Contains("--loop");var uncorrected=args.Contains("--uncorrected");
   var bytes=File.ReadAllBytes(Path.Combine(output,side+(uncorrected?"-uncorrected.f32":"-baked.f32")));var data=new float[bytes.Length/4];Buffer.BlockCopy(bytes,0,data,0,bytes.Length);
   int sampleCount=data.Length/(paths.Length*10);
   if(data.Length!=sampleCount*paths.Length*10||sampleCount<2||(!isLoop&&sampleCount!=209)) throw new Exception("Unexpected baked frame count");
   var suffix=isLoop?(uncorrected?"_uncorrected":"_loop_test"):"_full";
   var animation=new ImportedKeyframedAnimation{Name="A_actor_aglina_sprint_dash_sp_"+(side=="left"?"l":"r")+(isLoop?suffix:""),SampleRate=60,TrackList=new()};
   for(int i=0;i<frames.Length;i++) {
    var track=animation.FindTrack(frames[i].Path);
    for(int j=0;j<sampleCount;j++) {
     int k=(j*frames.Length+i)*10;float time=j/60f;
     track.Rotations.Add(new(time,new Quaternion(data[k],-data[k+1],-data[k+2],data[k+3])));
     track.Translations.Add(new(time,new Vector3(-data[k+4],data[k+5],data[k+6])));
     track.Scalings.Add(new(time,new Vector3(data[k+7],data[k+8],data[k+9])));
    }
   }
   model.AnimationList.Clear();model.AnimationList.Add(animation);
   Fbx.Exporter.Export(Path.Combine(output,"aglina_spdash_"+side+suffix+".fbx"),model,fbxOptions);
   Console.WriteLine($"FBX_BAKED {side} tracks={animation.TrackList.Count} samples={sampleCount} meshes={model.MeshList.Count}");
  }
 } else Fbx.Exporter.Export(Path.Combine(output,go.m_Name+".fbx"),model,fbxOptions);
}
