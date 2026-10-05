using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class AglinaBake {
 [Serializable] public class Bone { public string name,path; public int parent; public Vector3 position,scale; public Quaternion rotation; }
 [Serializable] public class Side { public string name,clipName,transformsFile; public string[] paths; public float[] humanoid; }
 [Serializable] public class Limit { public bool useDefaultValues; public Vector3 min,max,center; public float axisLength; }
 [Serializable] public class Human { public string boneName,humanName; public Limit limit; }
 [Serializable] public class Skeleton { public string name; public Vector3 position,scale; public Quaternion rotation; }
 [Serializable] public class Description { public Human[] human; public Skeleton[] skeleton; public float upperArmTwist,lowerArmTwist,upperLegTwist,lowerLegTwist,armStretch,legStretch,feetSpacing; public bool hasTranslationDoF;
  public HumanDescription Make()=>new HumanDescription{human=human.Select(h=>new HumanBone{boneName=h.boneName,humanName=h.humanName,limit=new HumanLimit{useDefaultValues=h.limit.useDefaultValues,min=h.limit.min,max=h.limit.max,center=h.limit.center,axisLength=h.limit.axisLength}}).ToArray(),
   skeleton=skeleton.Select(s=>new SkeletonBone{name=s.name,position=s.position,rotation=s.rotation,scale=s.scale}).ToArray(),upperArmTwist=upperArmTwist,lowerArmTwist=lowerArmTwist,upperLegTwist=upperLegTwist,lowerLegTwist=lowerLegTwist,armStretch=armStretch,legStretch=legStretch,feetSpacing=feetSpacing,hasTranslationDoF=hasTranslationDoF}; }
 [Serializable] public class Input { public Bone[] bones; public Description description; public Side[] sides; public int frames,fps; public float sourceHumanScale; }
 [Serializable] public class MuscleError { public int index,frame;public string side,name;public float source,roundtrip,error; }
 [Serializable] public class Report { public bool valid,human; public float sourceScale,builtScale,maxBodyPositionError,maxBodyAngleError,maxMappedMuscleError,maxRoundtripLocalAngle,maxRoundtripWorldPosition; public string worstAngleBone,worstPositionBone;public MuscleError[] channelErrors;public string[] paths; public string[] muscleNames; public string note; }
 public static void Run() {
  try { Bake(); EditorApplication.Exit(0); } catch(Exception e) { Debug.LogException(e); EditorApplication.Exit(1); }
 }
 static void Bake() {
  var dir=Path.GetFullPath(Path.Combine(Application.dataPath,"../.."));
  var args=Environment.GetCommandLineArgs();int outputArg=Array.IndexOf(args,"-aglinaOutput");if(outputArg>=0)dir=Path.GetFullPath(args[outputArg+1]);
  var input=JsonUtility.FromJson<Input>(File.ReadAllText(Path.Combine(dir,"bake-input.json")));
  var ts=new Transform[input.bones.Length];
  for(int i=0;i<ts.Length;i++) {
   var b=input.bones[i]; ts[i]=new GameObject(b.name).transform;
   if(b.parent>=0) ts[i].SetParent(ts[b.parent],false);
   ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;
  }
  var avatar=AvatarBuilder.BuildHumanAvatar(ts[0].gameObject,input.description.Make());
  if(!avatar.isValid||!avatar.isHuman) throw new Exception("Original HumanDescription produced invalid Avatar");
  var anim=ts[0].gameObject.AddComponent<Animator>();anim.avatar=avatar;
  var report=new Report{valid=avatar.isValid,human=avatar.isHuman,sourceScale=input.sourceHumanScale,builtScale=anim.humanScale,
   paths=input.bones.Select(b=>b.path).ToArray(),muscleNames=HumanTrait.MuscleName,
   note="Offline reconstruction using stock Unity 2022.3.62f3 HumanPoseHandler. Game-specific IK/cloth/effects not simulated. XZ root travel removed for inspection; source ACL files retained."};
  var handler=new HumanPoseHandler(avatar,ts[0]);
  report.channelErrors=Enumerable.Range(0,55).Select(i=>new MuscleError{index=i,name=HumanTrait.MuscleName[i]}).ToArray();
  var pose=new HumanPose{muscles=new float[HumanTrait.MuscleCount]};
  var check=new HumanPose{muscles=new float[HumanTrait.MuscleCount]};
  var dict=input.bones.Select((b,i)=>new{b.path,i}).ToDictionary(x=>x.path,x=>x.i);
  foreach(var side in input.sides) {
   byte[] bytes=File.ReadAllBytes(side.transformsFile); var extra=new float[bytes.Length/4];Buffer.BlockCopy(bytes,0,extra,0,bytes.Length);
   var indices=side.paths.Select(p=>dict[p]).ToArray();
   using(var writer=new BinaryWriter(File.Create(Path.Combine(dir,side.name+"-baked.f32")))) {
    for(int frame=0;frame<input.frames;frame++) {
     for(int i=0;i<ts.Length;i++){var b=input.bones[i];ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;}
     int o=frame*151; var f=side.humanoid;
     var motionQ=new Quaternion(f[o+3],f[o+4],f[o+5],f[o+6]);
     pose.bodyPosition=Quaternion.Inverse(motionQ)*new Vector3(f[o+7]-f[o],f[o+8]-f[o+1],f[o+9]-f[o+2]);
     pose.bodyRotation=Quaternion.Inverse(motionQ)*new Quaternion(f[o+10],f[o+11],f[o+12],f[o+13]);
     Array.Copy(f,o+42,pose.muscles,0,pose.muscles.Length);
     handler.SetHumanPose(ref pose);
     var sourceQ=ts.Select(t=>t.localRotation).ToArray();var sourceP=ts.Select(t=>t.position).ToArray();
     handler.GetHumanPose(ref check);
     report.maxBodyPositionError=Math.Max(report.maxBodyPositionError,Vector3.Distance(check.bodyPosition,pose.bodyPosition));
     report.maxBodyAngleError=Math.Max(report.maxBodyAngleError,Quaternion.Angle(check.bodyRotation,pose.bodyRotation));
     for(int k=0;k<55;k++) if(HumanTrait.BoneFromMuscle(k)>=0 && anim.GetBoneTransform((HumanBodyBones)HumanTrait.BoneFromMuscle(k))!=null)
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
  handler.Dispose();
  File.WriteAllText(Path.Combine(dir,"unity-bake-report.json"),JsonUtility.ToJson(report,true));
  Debug.Log("AGLINA_BAKE_OK "+JsonUtility.ToJson(report));
 }
}
