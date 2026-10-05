using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

// Character-generic bake: same reconstruction as AglinaBakeV2, stride derived from the input, but the native Endfield humanoid stream carries
// 101 muscle channels (61 body + 2x20 hand). Standard Unity indices map as 0-28 -> same,
// 29-36 -> +3, 37-94 -> +6 (six extra native channels at native 29-31 and 40-42 are ignored).
public static class CharBakeV1 {
 [Serializable] public class ExtraChannel { public int nativeIndex; public float min,max,meanAbs; }
 [Serializable] public class Report : AglinaBake.Report { public string muscleMap; public int[] stdToNative; public ExtraChannel[] ignoredNativeChannels; public float maxMuscleValueAbs; }
 public static void Run() {
  try { Bake(); EditorApplication.Exit(0); } catch(Exception e) { Debug.LogException(e); EditorApplication.Exit(1); }
 }
 static int Map(int i)=>i<=28?i:(i<=36?i+3:i+6);
 static void Bake() {
  var dir=Path.GetFullPath(Path.Combine(Application.dataPath,"../.."));
  var args=Environment.GetCommandLineArgs();int outputArg=Array.IndexOf(args,"-bakeOutput");if(outputArg>=0)dir=Path.GetFullPath(args[outputArg+1]);
  var input=JsonUtility.FromJson<AglinaBake.Input>(File.ReadAllText(Path.Combine(dir,"bake-input.json")));
  var ts=new Transform[input.bones.Length];
  for(int i=0;i<ts.Length;i++) {
   var b=input.bones[i]; ts[i]=new GameObject(b.name).transform;
   if(b.parent>=0) ts[i].SetParent(ts[b.parent],false);
   ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;
  }
  var avatar=AvatarBuilder.BuildHumanAvatar(ts[0].gameObject,input.description.Make());
  if(!avatar.isValid||!avatar.isHuman) throw new Exception("Original HumanDescription produced invalid Avatar");
  var anim=ts[0].gameObject.AddComponent<Animator>();anim.avatar=avatar;
  int muscleCount=HumanTrait.MuscleCount; if(muscleCount!=95) throw new Exception("Unexpected standard muscle count "+muscleCount);
  // The native humanoid stream is 3+4 motion, 3+4 root, 28 IK goals, 101 muscles, then
  // per-character IK weight channels (8 for Aglina, 9 for Liino). Derive the stride.
  const int nativeMuscles=101, muscleOffset=42;
  int stride=input.sides[0].humanoid.Length/input.frames;
  if(stride<muscleOffset+nativeMuscles||stride>muscleOffset+nativeMuscles+16) throw new Exception("Unexpected humanoid stride "+stride);
  var report=new Report{valid=avatar.isValid,human=avatar.isHuman,sourceScale=input.sourceHumanScale,builtScale=anim.humanScale,
   paths=input.bones.Select(b=>b.path).ToArray(),muscleNames=HumanTrait.MuscleName,muscleMap="EIEM 95->101: i<=28 same, 29-36 +3, 37-94 +6",
   stdToNative=Enumerable.Range(0,muscleCount).Select(Map).ToArray(),
   note="Offline reconstruction using stock Unity 2022.3.62f3 HumanPoseHandler with corrected native 101-channel muscle indexing. Game-specific IK/cloth/effects not simulated. XZ root travel removed for inspection; source ACL files retained."};
  var mapped=new System.Collections.Generic.HashSet<int>(report.stdToNative);
  var extras=Enumerable.Range(0,nativeMuscles).Where(i=>!mapped.Contains(i)).ToArray();
  if(extras.Length!=6) throw new Exception("Expected six unmapped native channels, got "+extras.Length);
  var extraStats=extras.Select(i=>new ExtraChannel{nativeIndex=i,min=float.MaxValue,max=float.MinValue}).ToArray();
  var handler=new HumanPoseHandler(avatar,ts[0]);
  report.channelErrors=Enumerable.Range(0,muscleCount).Select(i=>new AglinaBake.MuscleError{index=i,name=HumanTrait.MuscleName[i]}).ToArray();
  var pose=new HumanPose{muscles=new float[muscleCount]};
  var check=new HumanPose{muscles=new float[muscleCount]};
  var dict=input.bones.Select((b,i)=>new{b.path,i}).ToDictionary(x=>x.path,x=>x.i);
  foreach(var side in input.sides) {
   if(side.humanoid.Length!=input.frames*stride) throw new Exception("Unexpected humanoid stride for "+side.name);
   byte[] bytes=File.ReadAllBytes(side.transformsFile); var extra=new float[bytes.Length/4];Buffer.BlockCopy(bytes,0,extra,0,bytes.Length);
   var indices=side.paths.Select(p=>dict[p]).ToArray();
   using(var writer=new BinaryWriter(File.Create(Path.Combine(dir,side.name+"-baked.f32")))) {
    for(int frame=0;frame<input.frames;frame++) {
     for(int i=0;i<ts.Length;i++){var b=input.bones[i];ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;}
     int o=frame*stride; var f=side.humanoid;
     var motionQ=new Quaternion(f[o+3],f[o+4],f[o+5],f[o+6]);
     pose.bodyPosition=Quaternion.Inverse(motionQ)*new Vector3(f[o+7]-f[o],f[o+8]-f[o+1],f[o+9]-f[o+2]);
     pose.bodyRotation=Quaternion.Inverse(motionQ)*new Quaternion(f[o+10],f[o+11],f[o+12],f[o+13]);
     for(int m=0;m<muscleCount;m++){pose.muscles[m]=f[o+muscleOffset+Map(m)];report.maxMuscleValueAbs=Math.Max(report.maxMuscleValueAbs,Math.Abs(pose.muscles[m]));}
     for(int e=0;e<extras.Length;e++){float v=f[o+muscleOffset+extras[e]];var s=extraStats[e];s.min=Math.Min(s.min,v);s.max=Math.Max(s.max,v);s.meanAbs+=Math.Abs(v)/(input.frames*input.sides.Length);}
     handler.SetHumanPose(ref pose);
     var sourceQ=ts.Select(t=>t.localRotation).ToArray();var sourceP=ts.Select(t=>t.position).ToArray();
     handler.GetHumanPose(ref check);
     report.maxBodyPositionError=Math.Max(report.maxBodyPositionError,Vector3.Distance(check.bodyPosition,pose.bodyPosition));
     report.maxBodyAngleError=Math.Max(report.maxBodyAngleError,Quaternion.Angle(check.bodyRotation,pose.bodyRotation));
     for(int k=0;k<muscleCount;k++) if(HumanTrait.BoneFromMuscle(k)>=0 && anim.GetBoneTransform((HumanBodyBones)HumanTrait.BoneFromMuscle(k))!=null)
     {
      float error=Math.Abs(check.muscles[k]-pose.muscles[k]);report.maxMappedMuscleError=Math.Max(report.maxMappedMuscleError,error);
      if(error>report.channelErrors[k].error){var e=report.channelErrors[k];e.error=error;e.frame=frame;e.side=side.name;e.source=pose.muscles[k];e.roundtrip=check.muscles[k];}
     }
     handler.SetHumanPose(ref check);
     for(int i=0;i<ts.Length;i++) {
      float angle=Quaternion.Angle(sourceQ[i],ts[i].localRotation),position=Vector3.Distance(sourceP[i],ts[i].position);
      if(angle>report.maxRoundtripLocalAngle){report.maxRoundtripLocalAngle=angle;report.worstAngleBone=ts[i].name;}
      if(position>report.maxRoundtripWorldPosition){report.maxRoundtripWorldPosition=position;report.worstPositionBone=ts[i].name;}
     }
     handler.SetHumanPose(ref pose);
     for(int j=0;j<indices.Length;j++) {
      int k=(frame*indices.Length+j)*10;var t=ts[indices[j]];
      t.localRotation=new Quaternion(extra[k],extra[k+1],extra[k+2],extra[k+3]);
      t.localPosition=new Vector3(extra[k+4],extra[k+5],extra[k+6]);
      t.localScale=new Vector3(extra[k+7],extra[k+8],extra[k+9]);
     }
     foreach(var t in ts){var q=t.localRotation;var p=t.localPosition;var s=t.localScale;
      foreach(float v in new[]{q.x,q.y,q.z,q.w,p.x,p.y,p.z,s.x,s.y,s.z}) {if(float.IsNaN(v)||float.IsInfinity(v))throw new Exception("Non-finite pose");writer.Write(v);}}
    }
   }
   Debug.Log("Baked "+side.name+": "+input.frames+" frames, "+ts.Length+" transforms");
  }
  report.ignoredNativeChannels=extraStats;
  handler.Dispose();
  File.WriteAllText(Path.Combine(dir,"unity-bake-report.json"),JsonUtility.ToJson(report,true));
  Debug.Log("CHAR_BAKE_V1_OK stride="+stride+" "+JsonUtility.ToJson(report));
 }
}
