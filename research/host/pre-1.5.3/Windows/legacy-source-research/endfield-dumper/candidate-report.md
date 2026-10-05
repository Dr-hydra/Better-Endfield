# Opening silhouette candidate report

- Run: `E:\Dr.Hydra\EF-Start-Change\tools\EndfieldDumper`
- Generated UTC: 08/02/2026 21:11:55 +00:00
- Static assemblies: 170 normal / 170 AI

## Candidate chain

``RenderSilhouetteRT`` owns ``cameraRT``, ``mainCamera`` and ``materialSilhouette`` plus static silhouette render textures. The most likely rendering chain is character/avatar renderer -> cameraRT -> RenderTexture -> silhouette material.

``FMVController`` owns a ``PlayableDirector`` and ``PreloadFMVConfig``/``Play``/``Stop``. It remains a competing explanation if the visible opening is a movie-backed timeline rather than live geometry.

``CinematicTimelineManagerBase`` exposes timeline-root instantiation, character spawning, animation-track binding, animator lookup and FMV preprocessing. This is the primary live-model driver to correlate with the silhouette renderer.

``SquadManager.OnEndminGenderSwitch`` and ``NarrativeGenderEnum.Default/Male/Female`` provide the account-state branch. This run only observes the current account state; other branches are static inference.

## Static references

### RenderSilhouetteRT
Role: silhouette render target. Confidence: high-static.
Declaration: `Entry.Beyond.dll.cs:18` public class RenderSilhouetteRT : UnityEngine.MonoBehaviour
Fields: public UnityEngine.Camera cameraRT;  // 0x18; public UnityEngine.Camera mainCamera;  // 0x20; public UnityEngine.Material materialSilhouette;  // 0x28
Methods: private System.Void Update() { }; private System.Void OnDestroy() { }; public System.Void .ctor() { }
- `BeyondDynamicBone.dll.cs:3619` public System.Boolean hasMainCamera;  // 0x80
- `CW.Common.dll.cs:56` public static CW.Common.CwFollow.FollowType MainCamera;  // const
- `Entry.Beyond.dll.cs:18` public class RenderSilhouetteRT : UnityEngine.MonoBehaviour
- `Entry.Beyond.dll.cs:21` public UnityEngine.Camera cameraRT;  // 0x18
- `Entry.Beyond.dll.cs:22` public UnityEngine.Camera mainCamera;  // 0x20
- `Entry.Beyond.dll.cs:23` public UnityEngine.Material materialSilhouette;  // 0x28
- `Entry.Beyond.dll.cs:24` private static UnityEngine.Texture s_mSilhouetteRT;  // static @ 0x0
- `Entry.Beyond.dll.cs:25` private static UnityEngine.RenderTexture s_mSilhouetteRth;  // static @ 0x8
- `FactoryUnsafe.Gameplay.Beyond.dll.cs:12268` public Unity.Mathematics.float3 mainCameraPos;  // 0x20
- `Gameplay.Beyond.dll.cs:7722` public static Beyond.Gameplay.CharInteractPerformEnums.FocusTargetType MainCamera;  // const
- `Gameplay.Beyond.dll.cs:31203` public static Beyond.Gameplay.AI.CharacterFocusBehavior.FocusTarget MainCamera;  // const
- `Gameplay.Beyond.dll.cs:53564` public UnityEngine.Camera mainCamera;  // 0x50

### FMVController
Role: opening FMV / PlayableDirector controller. Confidence: medium-static.
Declaration: `Gameplay.Beyond.dll.cs:81055` public class FMVController : Beyond.TickableUIMono
Fields: private UnityEngine.Playables.PlayableDirector _fmvDirector;  // 0x68; private Beyond.UI.UISubtitle _subtitle;  // 0x70; private Beyond.UI.UIBigLogo _bigLogo;  // 0x78; private System.String m_fmvName;  // 0x80; private System.Collections.Generic.Dictionary<System.String,Beyond.Resource.FAssetProxyHandle> m_loadedBigLogoHandles;  // 0x88; private Beyond.Resource.SimpleAssetLoader m_fmvLoader;  // 0x90; private Beyond.Gameplay.Core.GlobalMultiTagHandle m_globalTagHandle;  // 0x98; private CriWare.CriManaMovieMaterialBase m_boundMovieController;  // 0xa8; private System.Boolean m_shouldSyncMovieUserTime;  // 0xb0; private System.Boolean m_needReleaseFmvResources;  // 0xb1; private UnityEngine.Coroutine m_preloadCoroutine;  // 0xb8; private UnityEngine.Timeline.TimelineAsset m_durationOverrideTimeline;  // 0xc0
Methods: public System.Void BindMovieController(CriWare.CriManaMovieMaterialBase movieController) { }; public System.Void UnbindMovieController() { }; public System.Void Pause(System.Boolean pause) { }; public System.Void Play() { }; public System.Void Stop() { }; public System.Void ReleaseAll() { }; protected virtual System.Void OnRelease() { }; public virtual System.Void LateTick(System.Single deltaTime) { }; private System.Void _SyncMovieUserTime() { }; private System.Boolean _TryGetMovieDuration(CriWare.CriMana.MovieInfo movieInfo, System.Double& duration) { }; private System.Void _ApplyTimelineDurationOverride(UnityEngine.Timeline.TimelineAsset timelineAsset, System.Double videoDuration) { }; private System.Void _RestoreTimelineDurationOverride() { }; private System.Boolean _HasAnyPlayableClip(UnityEngine.Playables.PlayableAsset playableAsset) { }; public System.Void PreloadFMVConfig(System.String fmvName, System.Action callback) { }; private System.Collections.IEnumerator _PreloadFmvConfigInGameplay(System.String fmvName, System.Action callback) { }; private System.Void _PreprocessBigLogo() { }
- `Gameplay.Beyond.dll.cs:5037` public sealed class <_PreloadFmvConfigInGameplay>d__39 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
- `Gameplay.Beyond.dll.cs:5042` public Beyond.Gameplay.FMVController <>4__this;  // 0x20
- `Gameplay.Beyond.dll.cs:81055` public class FMVController : Beyond.TickableUIMono
- `Gameplay.Beyond.dll.cs:81058` private UnityEngine.Playables.PlayableDirector _fmvDirector;  // 0x68
- `Gameplay.Beyond.dll.cs:81079` UnityEngine.Playables.PlayableDirector fmvDirector { get; /* RVA: 0x06C01B08 */ }
- `Gameplay.Beyond.dll.cs:81084` public System.Void BindMovieController(CriWare.CriManaMovieMaterialBase movieController) { }
- `Gameplay.Beyond.dll.cs:81086` public System.Void UnbindMovieController() { }
- `Gameplay.Beyond.dll.cs:81110` public System.Void PreloadFMVConfig(System.String fmvName, System.Action callback) { }
- `Gameplay.Beyond.dll.cs:81112` private System.Collections.IEnumerator _PreloadFmvConfigInGameplay(System.String fmvName, System.Action callback) { }
- `Gameplay.Beyond.dll.cs:359811` public static IFix.IDMAP0 Beyond-Gameplay-FMVController-get_tickOption0;  // const
- `Gameplay.Beyond.dll.cs:359812` public static IFix.IDMAP0 Beyond-Gameplay-FMVController-get_fmvDirector0;  // const
- `Gameplay.Beyond.dll.cs:359813` public static IFix.IDMAP0 Beyond-Gameplay-FMVController-_HasAnyPlayableClip0;  // const

### CinematicTimelineManagerBase
Role: timeline root, actor spawn and FMV preprocessing. Confidence: high-static.
Declaration: `Gameplay.Beyond.dll.cs:215560` public abstract class CinematicTimelineManagerBase : Beyond.Gameplay.BaseWorldManager
Fields: protected Beyond.Resource.CachedPathAssetLoader m_assetLoader;  // 0x80; protected Beyond.Resource.SimpleAssetLoader m_configLoader;  // 0x88; protected readonly System.Collections.Generic.Dictionary<System.String,Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle> m_preloadingTimelineHandles;  // 0x90; protected Beyond.Gameplay.ForbidHandle m_forbidSpIdleHandle;  // 0x98; protected UnityEngine.Animator m_animator;  // 0xd0; protected System.UInt32 m_leaderMoveTagHandle;  // 0xd8; protected System.Boolean m_lowFPSHasSet;  // 0xdc; protected System.Boolean m_pauseFrameGen;  // 0xdd; private UnityEngine.Transform <cutsceneRoot>k__BackingField;  // 0xe0; private readonly System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle,Beyond.Gameplay.View.CameraMono> m_ditherFacBuildingCameraMonos;  // 0xe8; private Beyond.Gameplay.Core.GlobalMultiTagHandle m_globalTagHandle;  // 0xf0; private System.Collections.Generic.Dictionary<System.String,Beyond.Gameplay.I18NSingleCutsceneSubtitleConfig> m_i18NSingleCutsceneSubtitleConfigs;  // 0x100
Methods: protected System.Void .ctor() { }; private static Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelinePreloadProfilerScope _CreateTimelinePreloadProfilerScope(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle handle) { }; private static Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelinePreloadStepProfilerScope _CreateTimelinePreloadStepProfilerScope(Unity.Profiling.ProfilerMarker stepMarker) { }; private static Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelinePreloadProfilerStepScope _CreateTimelinePreloadProfilerScope(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle handle, Unity.Profiling.ProfilerMarker stepMarker) { }; protected static System.IDisposable CreateTimelinePreloadProfilerScope(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle handle, Unity.Profiling.ProfilerMarker stepMarker) { }; public System.Void OnApplicationPause(System.Boolean pauseStatus) { }; public virtual System.Void OnMapUnloaded(System.String mapIdStr) { }; protected virtual System.Void OnRelease() { }; private System.Void _CleanUpTimelineManager() { }; private System.Void _CleanUpCacheFile() { }; private System.Void _ClearPreloadTimelineHandles() { }; protected System.Void StopTimelineHandlePreloadProcess(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle timelineHandle) { }; private System.Void _ForceCleanUpGO() { }; protected virtual System.Void ForceStopAllTimeline() { }; protected virtual System.Void OnTick(System.Single deltaTime) { }; protected System.Void TriggerHoldingOptimize(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle handle) { }
- `Assembly-CSharp.dll.cs:3967` public UnityEngine.Playables.PlayableDirector p0;  // 0x18
- `Assembly-CSharp.dll.cs:4765` private System.Void <GetDelegateByType>b__2(UnityEngine.Playables.PlayableDirector p0) { }
- `Assembly-CSharp.dll.cs:120766` public static System.Void __Gen_Delegate_Imp2(XLua.DelegateBridge bridge, UnityEngine.Playables.PlayableDirector p0) { }
- `Assembly-CSharp.dll.cs:120768` private static System.Void __Gen_Delegate_Imp2_AfterGC(XLua.DelegateBridge bridge, UnityEngine.Playables.PlayableDirector p0) { }
- `Assembly-CSharp.dll.cs:156533` public class UnityEnginePlayablesPlayableDirectorWrap
- `Cinemachine.dll.cs:128` public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
- `CriMw.CriWare.Runtime.dll.cs:8190` private UnityEngine.Playables.PlayableDirector m_Director;  // 0x10
- `CriMw.CriWare.Runtime.dll.cs:8367` private UnityEngine.Playables.PlayableDirector m_PlayableDirector;  // 0x10
- `CriMw.CriWare.Runtime.dll.cs:8457` public System.Void BindBehaviourMovieMaterial(UnityEngine.Playables.PlayableDirector director, CriWare.CriManaMovieMaterialBase boundMovieMaterial) { }
- `Gameplay.Beyond.dll.cs:59052` public UnityEngine.Playables.PlayableDirector director;  // 0x10
- `Gameplay.Beyond.dll.cs:59180` UnityEngine.Playables.PlayableDirector director { get; /* RVA: -1  // abstract */ }
- `Gameplay.Beyond.dll.cs:59301` public readonly System.Collections.Generic.HashSet<UnityEngine.Playables.PlayableDirector> asyncCompileDiscoveredDirectors;  // 0x278

### SquadManager
Role: account character / endmin gender switch. Confidence: medium-static.
Declaration: `Gameplay.Beyond.dll.cs:215022` public class SquadManager : Beyond.Gameplay.BasePlayerManager
Fields: private readonly System.Collections.Generic.Dictionary<System.UInt64,Beyond.Gameplay.Core.Entity> m_characters;  // 0x70; private readonly System.Collections.Generic.List<Beyond.Gameplay.Core.Entity> m_serverChars;  // 0x78; private readonly System.Collections.Generic.List<Beyond.Gameplay.Core.Entity> m_clientChars;  // 0x80; private Beyond.Gameplay.View.ModelVisibleTypeMask m_squadHideTypeMask;  // 0x88; private Beyond.Resource.Runtime.PreloadGroupHandle m_endminCharPreloadHandle;  // 0x90; private System.String m_lastEndminTid;  // 0x98; private Beyond.Gameplay.Core.SquadManager.Squad <curSquad>k__BackingField;  // 0xa0; private Beyond.Gameplay.Core.SquadManager.ServerSquad <curRealSquad>k__BackingField;  // 0xa8; private System.Boolean m_serverTeamChanged;  // 0xb0; private readonly System.Collections.Generic.List<Beyond.Gameplay.Core.SquadManager.ClientSquad.Request> m_clientTempSquadRequests;  // 0xb8; private readonly System.Collections.Generic.Dictionary<System.UInt64,Beyond.Gameplay.Core.MultiTagHandle> m_realSquadInvincibleTags;  // 0xc0; private System.Boolean <isWaitingNewSelfInfo>k__BackingField;  // 0xc8
Methods: public System.Void .ctor() { }; public Beyond.Gameplay.Core.SquadManager.Squad.Slot GetSlotInCurSquad(System.UInt64 charInstId) { }; public Beyond.Gameplay.Core.SquadManager.Squad.Slot GetSlotInCurSquad(System.Int32 slotIndex) { }; public Beyond.Gameplay.Core.SquadManager.Squad.Slot GetSlotInCurSquad(Beyond.Gameplay.Core.AbilitySystem abilityCom) { }; public System.Int32 GetMemberIndex(Beyond.Gameplay.Core.Entity character) { }; public Beyond.Gameplay.Core.Entity GetMemberBySlot(System.Int32 slotIndex) { }; public Beyond.Gameplay.Core.Entity GetMemberByTemplateId(System.String charTemplateId) { }; private Beyond.Gameplay.Core.Entity _GetCharacter(System.UInt64 charInstId) { }; public System.Boolean TryGetCharacter(System.UInt64 charInstId, Beyond.Gameplay.Core.Entity& character) { }; public System.Boolean SetLeaderByClient(Beyond.Gameplay.Core.Entity newLeader, Beyond.Gameplay.SetLeaderReason reason) { }; public System.Void OnEndminGenderSwitch() { }; private System.Void _RecreateSlotCharacter(Beyond.Gameplay.Core.SquadManager.Squad squad, Beyond.Gameplay.Core.SquadManager.Squad.Slot slot) { }; public System.Void ServerTeleportSquad(System.Int32 levelIdNum, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, Beyond.Gameplay.TeleportParam& teleportParam, System.Action teleportCb) { }; public System.Void TeleportSquad(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, Beyond.Gameplay.TeleportSource source, Beyond.Gameplay.TeleportUIType tpUIType, Beyond.Gameplay.TeleportOptions options, System.Action teleportCb) { }; private System.Void _DoTeleportImmediately(UnityEngine.Vector3 pos, UnityEngine.Quaternion centerCharRot) { }; private System.Void _OnSquadTeleport(UnityEngine.Vector3 mainCharNewPos) { }
- `Gameplay.Beyond.dll.cs:215030` private System.String m_lastEndminTid;  // 0x98
- `Gameplay.Beyond.dll.cs:215074` public System.Void OnEndminGenderSwitch() { }
- `Gameplay.Beyond.dll.cs:215076` private System.Void _RecreateSlotCharacter(Beyond.Gameplay.Core.SquadManager.Squad squad, Beyond.Gameplay.Core.SquadManager.Squad.Slot slot) { }
- `Gameplay.Beyond.dll.cs:375959` public static IFix.IDMAP0 Beyond-Gameplay-Core-SquadManager-_RecreateSlotCharacter0;  // const
- `Gameplay.Beyond.dll.cs:375960` public static IFix.IDMAP0 Beyond-Gameplay-Core-SquadManager-OnEndminGenderSwitch0;  // const

### NarrativeGenderEnum
Role: Default/Male/Female branch. Confidence: high-static.
Declaration: `Gameplay.Beyond.dll.cs:91448` public sealed struct NarrativeGenderEnum
Fields: public System.Int32 value__;  // 0x10
- `AK.Wwise.Unity.API.dll.cs:275` public static AkAudioObjectDestination eDefault;  // const
- `AK.Wwise.Unity.API.dll.cs:733` System.Boolean isDefaultDevice { get; /* RVA: 0x04009060 */ set; /* RVA: 0x0539FFA0 */ }
- `AK.Wwise.Unity.API.dll.cs:1013` public static AkFloorPlane AkFloorPlane_Default;  // const
- `AK.Wwise.Unity.API.dll.cs:2051` public static AkMonitorErrorCode ErrorCode_RevertingToDefaultAudioDevice;  // const
- `AK.Wwise.Unity.API.dll.cs:2089` public static AkMonitorErrorCode ErrorCode_NoDefaultSwitch;  // const
- `AK.Wwise.Unity.API.dll.cs:2637` public static AkQueryRTPCValue RTPCValue_Default;  // const
- `AK.Wwise.Unity.API.dll.cs:2947` public static AkSetPositionFlags AkSetPositionFlags_Default;  // const
- `AK.Wwise.Unity.API.dll.cs:3449` public static System.Void CSharp_AkDeviceDescription_isDefaultDevice_set(System.IntPtr jarg1, System.Boolean jarg2) { }
- `AK.Wwise.Unity.API.dll.cs:3451` public static System.Boolean CSharp_AkDeviceDescription_isDefaultDevice_get(System.IntPtr jarg1) { }
- `AK.Wwise.Unity.API.dll.cs:4317` public static System.Int32 CSharp_AddDefaultListener(System.UInt64 jarg1) { }
- `AK.Wwise.Unity.API.dll.cs:4319` public static System.Int32 CSharp_RemoveDefaultListener(System.UInt64 jarg1) { }
- `AK.Wwise.Unity.API.dll.cs:4321` public static System.Int32 CSharp_ResetListenersToDefault(System.UInt64 jarg1) { }

### CheckNarrativeGender
Role: runtime narrative gender condition. Confidence: medium-static.
Declaration: `Gameplay.Beyond.dll.cs:126962` public class CheckNarrativeGender : Beyond.Gameplay.GameConditionClient
Fields: public Beyond.Gameplay.NarrativeGenderEnum narrativeGender;  // 0x90
Methods: public virtual System.Void CollectParams(System.Collections.Generic.List<Beyond.Gameplay.Actions.IParamBindable>& paramList) { }; protected virtual System.Void OnActivate() { }; protected virtual System.Void OnDeactivate() { }; protected System.Void GetResult() { }; public System.Void .ctor() { }; public System.Void <>iFixBaseProxy_OnActivate() { }; public System.Void <>iFixBaseProxy_OnDeactivate() { }
- `Assembly-CSharp.dll.cs:1799` private System.Void <Init>b__836(System.IntPtr L, System.Int32 idx, Beyond.Gameplay.NarrativeGenderEnum& val) { }
- `Assembly-CSharp.dll.cs:30157` public sealed class stringboolobjectstringBeyond_Gameplay_NarrativeGenderEnum_uintDelegate`6 : System.MulticastDelegate
- `Assembly-CSharp.dll.cs:30172` public sealed class stringobjectstringBeyond_Gameplay_NarrativeGenderEnum_uintDelegate`5 : System.MulticastDelegate
- `Assembly-CSharp.dll.cs:50377` public sealed class stringBeyond_Gameplay_NarrativeGenderEnum_stringDelegate`3 : System.MulticastDelegate
- `Assembly-CSharp.dll.cs:50392` public sealed class Beyond_Gameplay_NarrativeGenderEnumstring_stringDelegate`3 : System.MulticastDelegate
- `Assembly-CSharp.dll.cs:60397` public sealed class Beyond_Gameplay_NarrativeGenderEnum_voidDelegate`1 : System.MulticastDelegate
- `Assembly-CSharp.dll.cs:76107` private static System.Int32 LuastringboolobjectstringBeyond_Gameplay_NarrativeGenderEnum_uintCall(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator translator, System.Collections.Generic.List<System.Type> castType) { }
- `Assembly-CSharp.dll.cs:76109` private static System.Int32 LuastringobjectstringBeyond_Gameplay_NarrativeGenderEnum_uintCall(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator translator, System.Collections.Generic.List<System.Type> castType) { }
- `Assembly-CSharp.dll.cs:80475` private static System.Int32 LuastringBeyond_Gameplay_NarrativeGenderEnum_stringCall(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator translator, System.Collections.Generic.List<System.Type> castType) { }
- `Assembly-CSharp.dll.cs:80477` private static System.Int32 LuaBeyond_Gameplay_NarrativeGenderEnumstring_stringCall(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator translator, System.Collections.Generic.List<System.Type> castType) { }
- `Assembly-CSharp.dll.cs:82511` private static System.Int32 LuaBeyond_Gameplay_NarrativeGenderEnum_voidCall(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator translator, System.Collections.Generic.List<System.Type> castType) { }
- `Assembly-CSharp.dll.cs:86278` public Beyond.Reflection.GenericDelegateCallerGen.stringboolobjectstringBeyond_Gameplay_NarrativeGenderEnum_uintDelegate<System.String,System.Boolean,System.Object,System.String,Beyond.Gameplay.NarrativeGenderEnum,System.UInt32> _6247534217743843420;  // 0x17b0

### CheckCurCutsceneName
Role: cutscene and gender condition. Confidence: medium-static.
Declaration: `Gameplay.Beyond.dll.cs:126564` public class CheckCurCutsceneName : Beyond.Gameplay.GameConditionClient
Fields: public Beyond.Gameplay.Actions.Param<System.String> _cutsceneName;  // 0x90; public Beyond.Gameplay.Actions.Param<System.Boolean> _acceptEndminGenderMale;  // 0x98; public Beyond.Gameplay.Actions.Param<System.Boolean> _acceptEndminGenderFemale;  // 0xa0; private Beyond.Gameplay.Audio.AudioStateSystem.ActionHandler m_audioStateActionHandler;  // 0xa8
Methods: public virtual System.Void CollectParams(System.Collections.Generic.List<Beyond.Gameplay.Actions.IParamBindable>& paramList) { }; protected virtual System.Void OnActivate() { }; protected virtual System.Void OnDeactivate() { }; private System.Void _OnAudioCutsceneStateChanged(Beyond.Gameplay.Audio.AudioStateSystem.EAudioState fromState, Beyond.Gameplay.Audio.AudioStateSystem.EAudioState toState) { }; private System.Void _UpdateResult() { }; public System.Void .ctor() { }; public System.Void <>iFixBaseProxy_OnActivate() { }; public System.Void <>iFixBaseProxy_OnDeactivate() { }
- `Gameplay.Beyond.dll.cs:126564` public class CheckCurCutsceneName : Beyond.Gameplay.GameConditionClient
- `Gameplay.Beyond.dll.cs:126568` public Beyond.Gameplay.Actions.Param<System.Boolean> _acceptEndminGenderMale;  // 0x98
- `Gameplay.Beyond.dll.cs:126569` public Beyond.Gameplay.Actions.Param<System.Boolean> _acceptEndminGenderFemale;  // 0xa0
- `Gameplay.Beyond.dll.cs:373284` public static IFix.IDMAP0 Beyond-Gameplay-CheckCurCutsceneName-CollectParams0;  // const
- `Gameplay.Beyond.dll.cs:373285` public static IFix.IDMAP0 Beyond-Gameplay-CheckCurCutsceneName-_UpdateResult0;  // const
- `Gameplay.Beyond.dll.cs:373286` public static IFix.IDMAP0 Beyond-Gameplay-CheckCurCutsceneName-_OnAudioCutsceneStateChanged0;  // const
- `Gameplay.Beyond.dll.cs:373288` public static IFix.IDMAP0 Beyond-Gameplay-CheckCurCutsceneName-OnActivate0;  // const
- `Gameplay.Beyond.dll.cs:373294` public static IFix.IDMAP0 Beyond-Gameplay-CheckCurCutsceneName-OnDeactivate0;  // const
- `MemoryPack.Beyond.dll.cs:26647` public class Beyond_Gameplay_CheckCurCutsceneNameFormatter : MemoryPack.MemoryPackFormatter`1
- `MemoryPack.Beyond.dll.cs:26651` public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, Beyond.Gameplay.CheckCurCutsceneName& value) { }
- `MemoryPack.Beyond.dll.cs:26658` public sealed class Beyond_Gameplay_CheckCurCutsceneNameForMemoryPackFormatter : MemoryPack.MemoryPackFormatter`1
- `MemoryPack.Beyond.dll.cs:26662` public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, Beyond_Gameplay_CheckCurCutsceneNameForMemoryPack& value) { }

## Dynamic evidence status

Dynamic snapshot files: IL2CPP_Scan_20260803_050615.txt, IL2CPP_World_Dump_20260803_050558.txt, IL2CPP_World_Dump_20260803_050608.txt
Latest world dump candidate hits: 78
- `IL2CPP_World_Dump_20260803_050608.txt:22582` @go id=-1088 name="S_actor_endminf_iris_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22591` @go id=-1082 name="S_actor_endminf_hair_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22600` @go id=-1076 name="S_actor_endminf_face_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22609` @go id=-1070 name="S_actor_endminf_eyeshadow_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22618` @go id=-1064 name="S_actor_endminf_eyebrow_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22627` @go id=-1058 name="S_actor_endminf_cloth_05_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22636` @go id=-1052 name="S_actor_endminf_cloth_04_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22645` @go id=-1046 name="S_actor_endminf_cloth_03_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22654` @go id=-1040 name="S_actor_endminf_cloth_02_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22663` @go id=-1034 name="S_actor_endminf_cloth_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:22672` @go id=-1028 name="S_actor_endminf_body_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:28746` material[0] name="Intro_Silhouette" main_tex="Background"
- `IL2CPP_World_Dump_20260803_050608.txt:28803` @go id=62338 name="Camera_Silhouette_RT" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:28809` comp "RenderSilhouetteRT"
- `IL2CPP_World_Dump_20260803_050608.txt:29073` @go id=64100 name="S_actor_endminf_body_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:29880` @go id=64562 name="S_actor_endminf_iris_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30148` @go id=64734 name="S_actor_endminf_cloth_05_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30220` @go id=64780 name="S_actor_endminf_cloth_03_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30292` @go id=64826 name="S_actor_endminf_cloth_04_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30322` @go id=64838 name="S_actor_endminf_face_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30359` @go id=64860 name="S_actor_endminf_hair_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30606` @go id=65018 name="S_actor_endminf_eyebrow_01_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30671` @go id=65052 name="S_actor_endminf_cloth_02_lod0" active=true layer=0 tag="Untagged"
- `IL2CPP_World_Dump_20260803_050608.txt:30701` @go id=65062 name="S_actor_endminf_eyeshadow_01_lod0" active=true layer=0 tag="Untagged"

The targeted class scan reported zero instances for RenderSilhouetteRT, but the world dump captured the component on Camera_Silhouette_RT. Treat the world dump as the runtime object evidence and the class-scan zero as a namespace/registration enumeration limitation.

No replacement, state write, resource mutation or anti-cheat bypass is part of this evidence run.
