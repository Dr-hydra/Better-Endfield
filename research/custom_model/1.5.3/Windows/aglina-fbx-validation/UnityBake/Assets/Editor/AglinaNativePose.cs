using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class AglinaNativePose {
 [Serializable] class SideReport {public string side;public float maxWorldError,maxBodyBoneError,maxLocalAngle;public string worstBone;public int worstFrame;public float endpointWorldGap;}
 [Serializable] class Report {public SideReport[] sides;public string note="HumanPose projection on original Avatar; generic tracks restored after humanoid evaluation. Game IK not simulated.";}
 static float[] Read(string p){var bytes=File.ReadAllBytes(p);var data=new float[bytes.Length/4];Buffer.BlockCopy(bytes,0,data,0,bytes.Length);return data;}
 static void Write(string p,float[] data){var bytes=new byte[data.Length*4];Buffer.BlockCopy(data,0,bytes,0,bytes.Length);File.WriteAllBytes(p,bytes);}
 static Quaternion Q(float[] f,int o)=>new Quaternion(f[o],f[o+1],f[o+2],f[o+3]);
 static Vector3 V(float[] f,int o)=>new Vector3(f[o],f[o+1],f[o+2]);
 static void Store(float[] f,int o,Vector3 v){f[o]=v.x;f[o+1]=v.y;f[o+2]=v.z;}
 static void Store(float[] f,int o,Quaternion q){f[o]=q.x;f[o+1]=q.y;f[o+2]=q.z;f[o+3]=q.w;}
 public static void Run(){try{Bake();EditorApplication.Exit(0);}catch(Exception e){Debug.LogException(e);EditorApplication.Exit(1);}}
 static void Bake(){
  var args=Environment.GetCommandLineArgs();var dir=Path.GetFullPath(args[Array.IndexOf(args,"-aglinaOutput")+1]);
  bool decodedMode=args.Contains("-aglinaDecoded");
  var input=JsonUtility.FromJson<AglinaBake.Input>(File.ReadAllText(Path.Combine(dir,"bake-input.json")));
  var ts=new Transform[input.bones.Length];
  for(int i=0;i<ts.Length;i++){var b=input.bones[i];ts[i]=new GameObject(b.name).transform;if(b.parent>=0)ts[i].SetParent(ts[b.parent],false);ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;}
  var avatar=AvatarBuilder.BuildHumanAvatar(ts[0].gameObject,input.description.Make());
  if(!avatar.isValid||!avatar.isHuman)throw new Exception("Invalid original Avatar");
  var animator=ts[0].gameObject.AddComponent<Animator>();animator.avatar=avatar;
  var handler=new HumanPoseHandler(avatar,ts[0]);var pose=new HumanPose{muscles=new float[HumanTrait.MuscleCount]};
  var paths=input.bones.Select((b,i)=>new{b.path,i}).ToDictionary(x=>x.path,x=>x.i);
  var report=new Report{sides=new SideReport[2]};
  for(int sideIndex=0;sideIndex<2;sideIndex++){
   var side=input.sides[sideIndex];var target=Read(Path.Combine(dir,side.name+"-target-inplace.f32"));var f=Read(Path.Combine(dir,side.name+(decodedMode?"-Float-decoded.f32":"-Float-seed.f32")));
   var generic=decodedMode?Read(Path.Combine(dir,side.name+"-Transform-decoded.f32")):null;
   var extras=side.paths.Select(p=>paths[p]).ToArray();var r=new SideReport{side=side.name};report.sides[sideIndex]=r;
   var decoded=new float[target.Length];Vector3[] begin=null;
   for(int frame=0;frame<209;frame++){
    for(int i=0;i<ts.Length;i++){int at=(frame*ts.Length+i)*10;ts[i].localRotation=Q(target,at);ts[i].localPosition=V(target,at+4);ts[i].localScale=V(target,at+7);}
    var desiredP=ts.Select(t=>t.position).ToArray();var desiredQ=ts.Select(t=>t.localRotation).ToArray();
    int o=frame*151;
    // Preserve source channels exactly outside the edited interval.
    if(!decodedMode&&frame>=40&&frame<=161){
     handler.GetHumanPose(ref pose);var motion=Q(f,o+3);
     Store(f,o+7,V(f,o)+motion*pose.bodyPosition);Store(f,o+10,motion*pose.bodyRotation);
     for(int muscle=0;muscle<95;muscle++){int bone=HumanTrait.BoneFromMuscle(muscle);if(bone>=0&&animator.GetBoneTransform((HumanBodyBones)bone)!=null)f[o+42+muscle]=pose.muscles[muscle];}
    }
    for(int i=0;i<ts.Length;i++){var b=input.bones[i];ts[i].localPosition=b.position;ts[i].localRotation=b.rotation;ts[i].localScale=b.scale;}
    pose.bodyPosition=Quaternion.Inverse(Q(f,o+3))*(V(f,o+7)-V(f,o));pose.bodyRotation=Quaternion.Inverse(Q(f,o+3))*Q(f,o+10);Array.Copy(f,o+42,pose.muscles,0,95);handler.SetHumanPose(ref pose);
    for(int j=0;j<extras.Length;j++){int i=extras[j];int at=decodedMode?(frame*extras.Length+j)*10:(frame*ts.Length+i)*10;var g=decodedMode?generic:target;ts[i].localRotation=Q(g,at);ts[i].localPosition=V(g,at+4);ts[i].localScale=V(g,at+7);}
    for(int i=0;i<ts.Length;i++){
     float e=Vector3.Distance(ts[i].position,desiredP[i]);if(e>r.maxWorldError){r.maxWorldError=e;r.worstBone=ts[i].name;r.worstFrame=frame;}
     r.maxLocalAngle=Math.Max(r.maxLocalAngle,Quaternion.Angle(ts[i].localRotation,desiredQ[i]));
     int at=(frame*ts.Length+i)*10;Store(decoded,at,ts[i].localRotation);Store(decoded,at+4,ts[i].localPosition);Store(decoded,at+7,ts[i].localScale);
    }
    if(frame==40)begin=ts.Select(t=>t.position).ToArray();
    if(frame==143)for(int i=0;i<ts.Length;i++)r.endpointWorldGap=Math.Max(r.endpointWorldGap,Vector3.Distance(begin[i],ts[i].position));
   }
   if(f.Any(v=>float.IsNaN(v)||float.IsInfinity(v)))throw new Exception("Non-finite output");
   Write(Path.Combine(dir,side.name+(decodedMode?"-decoded-projected.f32":"-projected.f32")),decoded);
   if(!decodedMode){Write(Path.Combine(dir,side.name+"-Float.f32"),f);var roots=new float[209*28];for(int frame=0;frame<209;frame++)Array.Copy(f,frame*151,roots,frame*28,28);Write(Path.Combine(dir,side.name+"-RootMotion.f32"),roots);}
   Debug.Log("NATIVE_POSE "+JsonUtility.ToJson(r));
  }
  handler.Dispose();File.WriteAllText(Path.Combine(dir,decodedMode?"native-pose-decoded-report.json":"native-pose-report.json"),JsonUtility.ToJson(report,true));
 }
}
