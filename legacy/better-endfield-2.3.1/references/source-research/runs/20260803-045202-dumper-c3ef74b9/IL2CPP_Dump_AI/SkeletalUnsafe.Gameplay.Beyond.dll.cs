// ========================================================
// Dumped by @desirepro
// Assembly: SkeletalUnsafe.Gameplay.Beyond.dll
// Classes:  24
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

# AI-FRIENDLY STRUCTURED DUMP
# Optimized for LLM parsing / code generation

CLASS: <Module>
TYPE:  class
TOKEN: 0x2000001
SIZE:  0x1
FIELDS:
METHODS:
END_CLASS

CLASS: SimpleWriteLock
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC4DE38  token=0x6000004  System.Boolean EnterLock(Unity.Collections.NativeArray<System.Int32> lockFlag, System.Int32 maxSpinCount)
  RVA=0x035B4EC0  token=0x6000005  System.Void MainThreadEnterLock(Unity.Collections.NativeArray<System.Int32> lockFlag, System.Int32 millisecondsTimeout, System.Int32 spinBeforeTiming)
  RVA=0x035B5020  token=0x6000006  System.Void ExitLock(Unity.Collections.NativeArray<System.Int32> lockFlag)
END_CLASS

CLASS: TrackerLock
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x28
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.NativeArray<System.Int32>flag  // 0x10
  public            System.Boolean                  isEnter  // 0x20
METHODS:
  RVA=0x0AC4E0B4  token=0x6000007  System.Void Execute()
END_CLASS

CLASS: ApplyBoneToAnimatorTransJob
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0xA8
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            System.Boolean                  runtime_writeNoDirty  // 0x10
  public            System.Single                   runtime_applyWeight  // 0x14
  public            Unity.Collections.NativeArray<Beyond.Gameplay.Core.FBoneRuntimeData>runtime_allBoneBasePose  // 0x18
  public            Unity.Collections.NativeArray<Beyond.Gameplay.Core.FBoneRuntimeData>runtime_allBonePose  // 0x28
  public            UnityEngine.AnimationTransformRWBufferHandleruntime_allBonesTransformRWBufferHandle  // 0x38
METHODS:
  RVA=0x0AC4A844  token=0x6000008  System.Void Execute()
  RVA=0x045A6050  token=0x6000009  System.Void CleanAnimatorTransJob(UnityEngine.AnimationTransformRWBufferHandle rwHandler)
  RVA=0x035B5050  token=0x600000A  System.Void SetAnimatorTransPhysicsRatioPtr(UnityEngine.AnimationTransformRWBufferHandle rwHandler, System.Single value)
END_CLASS

CLASS: __JobReflectionRegistrationOutput__285717778
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02E86550  token=0x6000023  System.Void CreateJobReflectionData()
  RVA=0x02E86D60  token=0x6000024  System.Void EarlyInit()
END_CLASS

CLASS: Beyond.Gameplay.Core.NativeArrayExtensions
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000003  System.Void FastCopy(Unity.Collections.NativeArray<T> src, Unity.Collections.NativeArray<T> dst)
END_CLASS

CLASS: Beyond.Gameplay.Core.SkeletalMorphUnsafeJobDefines
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.FSkeletalMorphShaderPropMappingData
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x20
FIELDS:
  public            System.Int32                    vectorIndex  // 0x10
  public            UnityEngine.Rendering.ShaderPropertyTypepropertyType  // 0x14
  public            System.Int32                    paramNameID  // 0x18
  public            System.Int32                    paramRuntimeIdx  // 0x1c
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.FBoneRuntimeData
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             pos  // 0x10
  public            UnityEngine.Vector3             rot  // 0x1c
  public            UnityEngine.Vector3             sca  // 0x28
  public            System.Boolean                  bIsDirty  // 0x34
METHODS:
  RVA=0x0AC4AAC4  token=0x600000B  System.String ToString()
  RVA=0x0AC4ABD8  token=0x600000C  System.String <>iFixBaseProxy_ToString()
END_CLASS

CLASS: Beyond.Gameplay.Core.FSkeletalMorphBoneData
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x3C
FIELDS:
  public            System.Int32                    boneNameHash  // 0x10
  public            System.Int32                    boneID  // 0x14
  public            UnityEngine.Vector3             position  // 0x18
  public            UnityEngine.Vector3             rotation  // 0x24
  public            UnityEngine.Vector3             scale  // 0x30
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.EMorphPartType
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Gameplay.Core.EMorphPartTypeNone  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeREyeMorphs  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeLEyeMorphs  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeRBrowMorphs  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeLBrowMorphs  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeMouthMorphs  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeOther  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeLEar  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeREar  // const
  public    static  Beyond.Gameplay.Core.EMorphPartTypeAll  // const
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.FSkeletalMorphMappingData
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x38
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Int32                    nameHash  // 0x14
  public            System.Int32                    tagHash  // 0x18
  public            Beyond.Gameplay.Core.EMorphPartTypepartType  // 0x1c
  public            System.Int32                    boneOffset  // 0x20
  public            System.Int32                    boneLen  // 0x24
  public            Beyond.Gameplay.Core.FSkeletalMorphBoneData*bones  // 0x28
  public            System.Boolean                  bIsShaderProp  // 0x30
  public    static  Beyond.Gameplay.Core.FSkeletalMorphMappingDatas_defaultMappingData  // static @ 0x0
METHODS:
  RVA=0x0AC4AF30  token=0x600000D  System.Boolean IsValid()
  RVA=0x0AC4AE24  token=0x600000E  Beyond.Gameplay.Core.FSkeletalMorphBoneData GetBoneData(System.Int32 idx)
  RVA=0x035B4500  token=0x600000F  System.Void SetMapping(Unity.Collections.NativeArray<Beyond.Gameplay.Core.FSkeletalMorphBoneData> bigList, System.Int32 bonesOffset, System.Int32 bonesLen)
  RVA=0x041E1670  token=0x6000010  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.PtrList`1
TYPE:  sealed struct
TOKEN: 0x200000E
FIELDS:
  public            System.Int32                    len  // 0x0
  public            T*                              data  // 0x0
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.FSkeletalMorphAnim
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0xA0
FIELDS:
  public    static  Beyond.Gameplay.Core.FSkeletalMorphAnimINVALID  // static @ 0x0
  public            System.Int32                    animSoTagID  // 0x10
  public            System.Single                   duration  // 0x14
  public            System.Boolean                  bIsAdditiveClip  // 0x18
  public            System.Boolean                  bIsOverride  // 0x19
  public            System.Boolean                  bIsPauseAutoBlink  // 0x1a
  public            System.Boolean                  bIsPoseAnim  // 0x1b
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>eyeCurveR  // 0x20
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>eyeCurveL  // 0x30
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>browCurveR  // 0x40
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>browCurveL  // 0x50
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>mouthCurve  // 0x60
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>otherCurve  // 0x70
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>lEarCurve  // 0x80
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlCurve>rEarCurve  // 0x90
METHODS:
  RVA=0x035B4E00  token=0x6000011  System.Boolean IsValid()
  RVA=0x042EA200  token=0x6000012  Unity.Collections.NativeArray<Beyond.Gameplay.Core.FMorphCtrlCurve> Create(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 curveLen)
  RVA=0x0AC4AC3C  token=0x6000013  System.Boolean GetCurveData(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 idx, Beyond.Gameplay.Core.FMorphCtrlCurve& curve)
  RVA=0x0AC4AD7C  token=0x6000014  System.Int32 GetCurveLen(Beyond.Gameplay.Core.EMorphPartType partType)
  RVA=0x035B52C0  token=0x6000015  System.Void SetCurveData(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 idx, Beyond.Gameplay.Core.FMorphCtrlCurve& curve)
  RVA=0x041E1670  token=0x6000016  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.FMorphCtrlCurve
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x30
FIELDS:
  public    static  System.Int32                    RESOLUTION  // const
  public            System.Int32                    ctrlNameHash  // 0x10
  public            Beyond.FPtrAnimNativeCurve      curve  // 0x18
METHODS:
  RVA=0x03B04FB0  token=0x6000017  Unity.Collections.NativeArray<UnityEngine.Keyframe> Create(System.String name, UnityEngine.AnimationCurve animCurve, Unity.Collections.Allocator allocator)
  RVA=0x0419EF50  token=0x6000018  Unity.Collections.NativeArray<UnityEngine.Keyframe> Create(System.String name, System.Single poseVal, Unity.Collections.Allocator allocator)
END_CLASS

CLASS: Beyond.Gameplay.Core.FSkeletalMorphPose
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x78
FIELDS:
  public    static  Beyond.Gameplay.Core.FSkeletalMorphPoseINVALID  // static @ 0x0
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>eyeValueR  // 0x10
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>eyeValueL  // 0x20
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>browValueR  // 0x30
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>browValueL  // 0x40
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>mouthValue  // 0x50
  public            Beyond.Gameplay.Core.PtrList<Beyond.Gameplay.Core.FMorphCtrlValue>otherValue  // 0x60
  public            System.Boolean                  bIsAdditivePose  // 0x70
METHODS:
  RVA=0x0AC4B2A4  token=0x6000019  System.Boolean IsValid()
  RVA=0x0AC4AF7C  token=0x600001A  System.Int32 AllCurveNum()
  RVA=0x0AC4AFD4  token=0x600001B  Unity.Collections.NativeArray<Beyond.Gameplay.Core.FMorphCtrlValue> Create(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 curveLen)
  RVA=0x0AC4B128  token=0x600001C  System.Boolean GetValueData(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 idx, Beyond.Gameplay.Core.FMorphCtrlValue& curve)
  RVA=0x0AC4B214  token=0x600001D  System.Int32 GetValueLen(Beyond.Gameplay.Core.EMorphPartType partType)
  RVA=0x0AC4B318  token=0x600001E  System.Void SetValueData(Beyond.Gameplay.Core.EMorphPartType partType, System.Int32 idx, Beyond.Gameplay.Core.FMorphCtrlValue& curve)
  RVA=0x041E1670  token=0x600001F  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.FMorphCtrlValue
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x18
FIELDS:
  public            System.Int32                    ctrlNameHash  // 0x10
  public            System.Single                   value  // 0x14
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.SkeletalMorphUnsafeJobUtils
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000020  T& GetUnsafeElementRef(Unity.Collections.NativeArray<T> array, System.Int32 idx)
  RVA=-1  // generic def  token=0x6000021  System.Void SetUnsafeElementRef(Unity.Collections.NativeArray<T> array, System.Int32 idx, T& val)
  RVA=0x0AC4DEEC  token=0x6000022  UnityEngine.Quaternion FromMaya(UnityEngine.Vector3 eulerAngle)
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000025  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0AC4B3F4  token=0x6000026  System.Boolean __Gen_Wrap_0(Unity.Collections.NativeArray<System.Int32> P0, System.Int32 P1)
  RVA=0x0AC4C890  token=0x6000027  System.Void __Gen_Wrap_1(Unity.Collections.NativeArray<System.Int32> P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0AC4D344  token=0x6000028  System.Void __Gen_Wrap_2(Unity.Collections.NativeArray<System.Int32> P0)
  RVA=0x0AC4D43C  token=0x6000029  System.Void __Gen_Wrap_3(Beyond.Gameplay.Core.SkeletalMorphUnsafeJobDefines.TrackerLock& P0)
  RVA=0x0AC4D570  token=0x600002A  UnityEngine.Quaternion __Gen_Wrap_4(UnityEngine.Vector3 P0)
  RVA=0x0AC4D698  token=0x600002B  System.Void __Gen_Wrap_5(Beyond.Gameplay.Core.SkeletalMorphUnsafeJobDefines.ApplyBoneToAnimatorTransJob& P0)
  RVA=0x0AC4D874  token=0x600002C  System.Void __Gen_Wrap_6(UnityEngine.AnimationTransformRWBufferHandle P0)
  RVA=0x0AC4D99C  token=0x600002D  System.Void __Gen_Wrap_7(UnityEngine.AnimationTransformRWBufferHandle P0, System.Single P1)
  RVA=0x0AC4DAEC  token=0x600002E  System.String __Gen_Wrap_8(Beyond.Gameplay.Core.FBoneRuntimeData& P0)
  RVA=0x0AC4DC50  token=0x600002F  System.Boolean __Gen_Wrap_9(Beyond.Gameplay.Core.FSkeletalMorphMappingData& P0)
  RVA=0x0AC4B500  token=0x6000030  Beyond.Gameplay.Core.FSkeletalMorphBoneData __Gen_Wrap_10(Beyond.Gameplay.Core.FSkeletalMorphMappingData& P0, System.Int32 P1)
  RVA=0x0AC4B6B4  token=0x6000031  System.Void __Gen_Wrap_11(Beyond.Gameplay.Core.FSkeletalMorphMappingData& P0, Unity.Collections.NativeArray<Beyond.Gameplay.Core.FSkeletalMorphBoneData> P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0AC4B874  token=0x6000032  System.Boolean __Gen_Wrap_12(Beyond.Gameplay.Core.FSkeletalMorphAnim& P0)
  RVA=0x0AC4BA64  token=0x6000033  Unity.Collections.NativeArray<Beyond.Gameplay.Core.FMorphCtrlCurve> __Gen_Wrap_13(Beyond.Gameplay.Core.FSkeletalMorphAnim& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2)
  RVA=0x0AC4BCA4  token=0x6000034  System.Boolean __Gen_Wrap_14(Beyond.Gameplay.Core.FSkeletalMorphAnim& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2, Beyond.Gameplay.Core.FMorphCtrlCurve& P3)
  RVA=0x0AC4BF04  token=0x6000035  System.Int32 __Gen_Wrap_15(Beyond.Gameplay.Core.FSkeletalMorphAnim& P0, Beyond.Gameplay.Core.EMorphPartType P1)
  RVA=0x0AC4C0F8  token=0x6000036  System.Void __Gen_Wrap_16(Beyond.Gameplay.Core.FSkeletalMorphAnim& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2, Beyond.Gameplay.Core.FMorphCtrlCurve& P3)
  RVA=0x0AC4C38C  token=0x6000037  Unity.Collections.NativeArray<UnityEngine.Keyframe> __Gen_Wrap_17(Beyond.Gameplay.Core.FMorphCtrlCurve& P0, System.Object P1, System.Object P2, Unity.Collections.Allocator P3)
  RVA=0x0AC4C544  token=0x6000038  Unity.Collections.NativeArray<UnityEngine.Keyframe> __Gen_Wrap_18(Beyond.Gameplay.Core.FMorphCtrlCurve& P0, System.Object P1, System.Single P2, Unity.Collections.Allocator P3)
  RVA=0x0AC4C6FC  token=0x6000039  System.Boolean __Gen_Wrap_19(Beyond.Gameplay.Core.FSkeletalMorphPose& P0)
  RVA=0x0AC4C9BC  token=0x600003A  System.Int32 __Gen_Wrap_20(Beyond.Gameplay.Core.FSkeletalMorphPose& P0)
  RVA=0x0AC4CB50  token=0x600003B  Unity.Collections.NativeArray<Beyond.Gameplay.Core.FMorphCtrlValue> __Gen_Wrap_21(Beyond.Gameplay.Core.FSkeletalMorphPose& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2)
  RVA=0x0AC4CD4C  token=0x600003C  System.Boolean __Gen_Wrap_22(Beyond.Gameplay.Core.FSkeletalMorphPose& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2, Beyond.Gameplay.Core.FMorphCtrlValue& P3)
  RVA=0x0AC4CF64  token=0x600003D  System.Int32 __Gen_Wrap_23(Beyond.Gameplay.Core.FSkeletalMorphPose& P0, Beyond.Gameplay.Core.EMorphPartType P1)
  RVA=0x0AC4D114  token=0x600003E  System.Void __Gen_Wrap_24(Beyond.Gameplay.Core.FSkeletalMorphPose& P0, Beyond.Gameplay.Core.EMorphPartType P1, System.Int32 P2, Beyond.Gameplay.Core.FMorphCtrlValue& P3)
  RVA=0x04D37090  token=0x600003F  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0AC4DDA4  token=0x6000040  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000041  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000042  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0AC4E310  token=0x6000043  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x035B4E70  token=0x6000044  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0AC4E1CC  token=0x6000045  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0AC4E298  token=0x6000046  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0AC4E374  token=0x6000047  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0AC4E128  token=0x6000048  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-SimpleWriteLock-EnterLock0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-SimpleWriteLock-MainThreadEnterLock0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-SimpleWriteLock-ExitLock0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-TrackerLock-Execute0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobUtils-FromMaya0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-ApplyBoneToAnimatorTransJob-Execute0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-ApplyBoneToAnimatorTransJob-CleanAnimatorTransJob0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-SkeletalMorphUnsafeJobDefines-ApplyBoneToAnimatorTransJob-SetAnimatorTransPhysicsRatioPtr0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FBoneRuntimeData-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphMappingData-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphMappingData-GetBoneData0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphMappingData-SetMapping0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphAnim-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphAnim-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphAnim-GetCurveData0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphAnim-GetCurveLen0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphAnim-SetCurveData0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FMorphCtrlCurve-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FMorphCtrlCurve-Create1  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-AllCurveNum0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-GetValueData0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-GetValueLen0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-FSkeletalMorphPose-SetValueData0  // const
METHODS:
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

