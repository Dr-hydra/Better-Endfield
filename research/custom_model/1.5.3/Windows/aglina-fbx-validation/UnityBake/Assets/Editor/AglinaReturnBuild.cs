using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using UnityEngine.Animations;

public static class AglinaReturnBuild {
 [Serializable] public class EventData { public float time,floatParameter; public string functionName,stringParameter;public int intParameter,messageOptions; }
 [Serializable] public class SideEvents {public string side;public EventData[] events;}
 [Serializable] public class Events {public SideEvents[] sides;}
 [Serializable] public class ClipReport { public string side,name;public bool humanMotion,legacy;public float length;public Vector3 averageSpeed,loopRootDisplacement;public int curves,events;public float avatarScale,originalRigLoopGap; }
 [Serializable] public class Report {public string editor;public ClipReport[] clips;public string bundle;}
 public static void Run(){try{Build();EditorApplication.Exit(0);}catch(Exception e){Debug.LogException(e);EditorApplication.Exit(1);}}
 static void Build(){
  var args=Environment.GetCommandLineArgs();var dir=Path.GetFullPath(args[Array.IndexOf(args,"-aglinaOutput")+1]);
  var input=JsonUtility.FromJson<AglinaBake.Input>(File.ReadAllText(Path.Combine(dir,"bake-input.json")));
  var events=JsonUtility.FromJson<Events>(File.ReadAllText(Path.Combine(dir,"events.json")));
  Directory.CreateDirectory("Assets/AglinaReturn");
  AssetDatabase.StartAssetEditing();
  try {foreach(var side in new[]{"left","right"})File.Copy(Path.Combine(dir,"aglina_spdash_"+side+"_full.fbx"),"Assets/AglinaReturn/"+side+".fbx",true);}
  finally{AssetDatabase.StopAssetEditing();}
  AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
  var report=new Report{editor=Application.unityVersion,clips=new ClipReport[2],bundle="aglina_return_v1.bundle"};
  var assets=new string[2];
  for(int side=0;side<2;side++){
   var name=side==0?"left":"right";var path="Assets/AglinaReturn/"+name+".fbx";
   var importer=(ModelImporter)AssetImporter.GetAtPath(path);
   importer.animationType=ModelImporterAnimationType.Human;importer.avatarSetup=ModelImporterAvatarSetup.CreateFromThisModel;
   importer.importAnimation=true;importer.animationCompression=ModelImporterAnimationCompression.Off;importer.optimizeGameObjects=false;
   var desc=input.description.Make();
   var model=AssetDatabase.LoadAssetAtPath<GameObject>(path);
   var sourceSkeleton=desc.skeleton.ToDictionary(s=>s.name,s=>s);
   desc.skeleton=model.GetComponentsInChildren<Transform>(true).Select(t=>sourceSkeleton.TryGetValue(t.name,out var s)?s:new SkeletonBone{name=t.name,position=t.localPosition,rotation=t.localRotation,scale=t.localScale}).ToArray();
   importer.humanDescription=desc;
   var take=importer.defaultClipAnimations.First();
   take.name="BE_Aglina_Return_"+(side==0?"L":"R");take.firstFrame=0;take.lastFrame=208;take.loopTime=false;take.loopPose=false;
   take.lockRootRotation=true;take.keepOriginalOrientation=true;take.lockRootHeightY=true;take.keepOriginalPositionY=true;take.heightFromFeet=false;
   take.lockRootPositionXZ=false;take.keepOriginalPositionXZ=true;
   var mask=new AvatarMask();mask.AddTransformPath(model.transform,true);for(int i=0;i<(int)AvatarMaskBodyPart.LastBodyPart;i++)mask.SetHumanoidBodyPartActive((AvatarMaskBodyPart)i,true);
   var maskPath="Assets/AglinaReturn/"+name+"-mask.mask";AssetDatabase.DeleteAsset(maskPath);AssetDatabase.CreateAsset(mask,maskPath);take.maskType=ClipAnimationMaskType.CopyFromOther;take.maskSource=mask;
   take.events=new AnimationEvent[0];
   importer.clipAnimations=new[]{take};importer.SaveAndReimport();
   var avatar=AssetDatabase.LoadAllAssetsAtPath(path).OfType<Avatar>().FirstOrDefault();
   var clip=AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>().First(c=>!c.name.StartsWith("__preview__"));
   Debug.Log($"RETURN_IMPORT {name}: avatar={avatar!=null} valid={avatar?.isValid} human={avatar?.isHuman} clipHuman={clip.humanMotion} legacy={clip.legacy} length={clip.length} frames={clip.frameRate}");
   if(avatar==null||!avatar.isValid||!avatar.isHuman||!clip.humanMotion||clip.legacy||Math.Abs(clip.length-208f/60)>.002f)throw new Exception("Invalid return clip/avatar "+name);
   var instance=UnityEngine.Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(path));var anim=instance.GetComponent<Animator>();
   if(Math.Abs(anim.humanScale-input.sourceHumanScale)>.001f)throw new Exception("Avatar scale mismatch "+anim.humanScale);
   report.clips[side]=new ClipReport{side=name,name=clip.name,humanMotion=clip.humanMotion,legacy=clip.legacy,length=clip.length,averageSpeed=clip.averageSpeed,curves=AnimationUtility.GetCurveBindings(clip).Length,events=clip.events.Length,avatarScale=anim.humanScale};
   var asset="Assets/AglinaReturn/"+name+".anim";AssetDatabase.DeleteAsset(asset);var standalone=UnityEngine.Object.Instantiate(clip);standalone.name=take.name;
   AnimationUtility.SetAnimationEvents(standalone,events.sides.First(s=>s.side==name).events.Select(e=>new AnimationEvent{time=e.time,functionName=e.functionName,stringParameter=e.stringParameter,floatParameter=e.floatParameter,intParameter=e.intParameter,messageOptions=(SendMessageOptions)e.messageOptions}).ToArray());
   var sourcePaths=new System.Collections.Generic.HashSet<string>(input.sides.First(s=>s.name==name).paths);
   var oldBindings=AnimationUtility.GetCurveBindings(standalone);var revised=new System.Collections.Generic.List<EditorCurveBinding>();var revisedCurves=new System.Collections.Generic.List<AnimationCurve>();
   foreach(var binding in oldBindings){
    if(binding.type==typeof(Animator))continue;
    var next=binding;const string prefix="chr_0013_aglina_postmodel/";
    if(next.path.StartsWith(prefix))next.path=next.path.Substring(prefix.Length);
    revised.Add(binding);revisedCurves.Add(null);
    if(sourcePaths.Contains(next.path)){revised.Add(next);revisedCurves.Add(AnimationUtility.GetEditorCurve(standalone,binding));}
   }
   AnimationUtility.SetEditorCurves(standalone,revised.ToArray(),revisedCurves.ToArray());
   AssetDatabase.CreateAsset(standalone,asset);assets[side]=asset;report.clips[side].events=standalone.events.Length;
   report.clips[side].curves=AnimationUtility.GetCurveBindings(standalone).Length;
   report.clips[side].originalRigLoopGap=ValidateOriginalRig(input,standalone,out report.clips[side].loopRootDisplacement);
   UnityEngine.Object.DestroyImmediate(instance);
  }
  AssetDatabase.SaveAssets();Directory.CreateDirectory(Path.Combine(dir,"bundle"));
  var build=new AssetBundleBuild{assetBundleName=report.bundle,assetNames=assets,addressableNames=new[]{"aglina_left","aglina_right"}};
  var manifest=BuildPipeline.BuildAssetBundles(Path.Combine(dir,"bundle"),new[]{build},BuildAssetBundleOptions.ChunkBasedCompression|BuildAssetBundleOptions.StrictMode,BuildTarget.StandaloneWindows64);
  if(manifest==null)throw new Exception("Bundle build failed");
  var loaded=AssetBundle.LoadFromFile(Path.Combine(dir,"bundle",report.bundle));
  if(loaded==null)throw new Exception("Bundle reload failed");
  foreach(var key in new[]{"aglina_left","aglina_right"}){var clip=loaded.LoadAsset<AnimationClip>(key);if(clip==null||!clip.humanMotion||Math.Abs(clip.length-208f/60)>.002f)throw new Exception("Bundle clip check failed");}
  loaded.Unload(true);File.WriteAllText(Path.Combine(dir,"unity-return-report.json"),JsonUtility.ToJson(report,true));
  Debug.Log("AGLINA_RETURN_BUILD_OK "+JsonUtility.ToJson(report));
 }
 static float ValidateOriginalRig(AglinaBake.Input input,AnimationClip clip,out Vector3 displacement){
  var transforms=new Transform[input.bones.Length];
  for(int i=0;i<transforms.Length;i++){var b=input.bones[i];var t=new GameObject(b.name).transform;transforms[i]=t;if(b.parent>=0)t.SetParent(transforms[b.parent],false);t.localPosition=b.position;t.localRotation=b.rotation;t.localScale=b.scale;}
  var root=transforms[0];var avatar=AvatarBuilder.BuildHumanAvatar(root.gameObject,input.description.Make());var animator=root.gameObject.AddComponent<Animator>();animator.avatar=avatar;animator.applyRootMotion=false;animator.cullingMode=AnimatorCullingMode.AlwaysAnimate;
  var copy=UnityEngine.Object.Instantiate(clip);copy.events=new AnimationEvent[0];
  var graph=PlayableGraph.Create("Return clip validation");var playable=AnimationClipPlayable.Create(graph,copy);playable.SetApplyFootIK(false);playable.SetApplyPlayableIK(false);var output=AnimationPlayableOutput.Create(graph,"Animation",animator);output.SetSourcePlayable(playable);graph.Play();
  playable.SetTime(40.0/60);graph.Evaluate(0);var first=transforms.Select(t=>t.position).ToArray();
  playable.SetTime(143.0/60);graph.Evaluate(0);float gap=0;string worst="";
  for(int i=0;i<transforms.Length;i++){var d=Vector3.Distance(first[i],transforms[i].position);if(d>gap){gap=d;worst=transforms[i].name;}}
  Debug.Log($"ORIGINAL_RIG_LOOP_GAP {clip.name}: {gap} m, {worst}");
  animator.applyRootMotion=true;playable.SetTime(40.0/60);graph.Evaluate(0);displacement=Vector3.zero;
  for(int i=0;i<103;i++){graph.Evaluate(1f/60);displacement+=animator.deltaPosition;}
  Debug.Log($"RETURN_LOOP_ROOT_DISPLACEMENT {clip.name}: {displacement}");
  graph.Destroy();UnityEngine.Object.DestroyImmediate(copy);UnityEngine.Object.DestroyImmediate(root.gameObject);UnityEngine.Object.DestroyImmediate(avatar);
  if(gap>.02f)throw new Exception("Original avatar loop gap exceeds 2 cm: "+gap);
  return gap;
 }
}
