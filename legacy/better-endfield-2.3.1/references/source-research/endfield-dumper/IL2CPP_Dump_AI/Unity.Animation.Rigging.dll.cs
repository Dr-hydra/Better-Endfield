// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Animation.Rigging.dll
// Classes:  136
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

CLASS: WorldUpType
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeNone  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeSceneUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeObjectUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeObjectRotationUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeVector  // const
METHODS:
END_CLASS

CLASS: Space
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.OverrideTransformJob.SpaceWorld  // const
  public    static  UnityEngine.Animations.Rigging.OverrideTransformJob.SpaceLocal  // const
  public    static  UnityEngine.Animations.Rigging.OverrideTransformJob.SpacePivot  // const
METHODS:
END_CLASS

CLASS: TransformSyncer
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.Collections.NativeArray<UnityEngine.Animations.TransformSceneHandle>sceneHandles  // 0x10
  public            Unity.Collections.NativeArray<UnityEngine.Animations.TransformStreamHandle>streamHandles  // 0x20
METHODS:
  RVA=0x0B157674  token=0x60000D7  UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.TransformSyncer Create(System.Int32 size)
  RVA=0x0B157724  token=0x60000D8  System.Void Dispose()
  RVA=0x0B1575F8  token=0x60000D9  System.Void BindAt(System.Int32 index, UnityEngine.Animator animator, UnityEngine.Transform transform)
  RVA=0x0B1577A0  token=0x60000DA  System.Void Sync(UnityEngine.Animations.AnimationStream& stream)
END_CLASS

CLASS: PropertySyncer
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertySceneHandle>sceneHandles  // 0x10
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>streamHandles  // 0x20
  public            Unity.Collections.NativeArray<System.Single>buffer  // 0x30
METHODS:
  RVA=0x0B151B3C  token=0x60000DB  UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.PropertySyncer Create(System.Int32 size)
  RVA=0x0B151C2C  token=0x60000DC  System.Void Dispose()
  RVA=0x0B151A70  token=0x60000DD  System.Void BindAt(System.Int32 index, UnityEngine.Animator animator, UnityEngine.Component component, System.String property)
  RVA=0x0B151D54  token=0x60000DE  System.Void Sync(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B151CD8  token=0x60000DF  Unity.Collections.NativeArray<System.Single> StreamValues(UnityEngine.Animations.AnimationStream& stream)
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisX  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisX_NEG  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisY  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisY_NEG  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisZ  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.AxisZ_NEG  // const
METHODS:
END_CLASS

CLASS: WorldUpType
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypeNone  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypeSceneUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypeObjectUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypeObjectRotationUp  // const
  public    static  UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypeVector  // const
METHODS:
END_CLASS

CLASS: Space
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.OverrideTransformData.SpaceWorld  // const
  public    static  UnityEngine.Animations.Rigging.OverrideTransformData.SpaceLocal  // const
  public    static  UnityEngine.Animations.Rigging.OverrideTransformData.SpacePivot  // const
METHODS:
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.TwistCorrectionData.AxisX  // const
  public    static  UnityEngine.Animations.Rigging.TwistCorrectionData.AxisY  // const
  public    static  UnityEngine.Animations.Rigging.TwistCorrectionData.AxisZ  // const
METHODS:
END_CLASS

CLASS: OnAddRigBuilderCallback
TYPE:  sealed class
TOKEN: 0x2000065
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000274  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000275  System.Void Invoke(UnityEngine.Animations.Rigging.RigBuilder rigBuilder)
  RVA=0x047FDD70  token=0x6000276  System.IAsyncResult BeginInvoke(UnityEngine.Animations.Rigging.RigBuilder rigBuilder, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000277  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: OnRemoveRigBuilderCallback
TYPE:  sealed class
TOKEN: 0x2000066
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000278  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000279  System.Void Invoke(UnityEngine.Animations.Rigging.RigBuilder rigBuilder)
  RVA=0x047FDD70  token=0x600027A  System.IAsyncResult BeginInvoke(UnityEngine.Animations.Rigging.RigBuilder rigBuilder, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600027B  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: PlayableChain
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.Playables.Playable[]playables  // 0x18
METHODS:
  RVA=0x0B151A58  token=0x6000280  System.Boolean IsValid()
END_CLASS

CLASS: RigSyncSceneToStreamData
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x30
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IRigSyncSceneToStreamData
FIELDS:
  private           UnityEngine.Transform[]         <syncableTransforms>k__BackingField  // 0x10
  private           UnityEngine.Animations.Rigging.SyncableProperties[]<syncableProperties>k__BackingField  // 0x18
  private           System.Boolean[]                <rigStates>k__BackingField  // 0x20
  private   readonly System.Boolean                  m_IsValid  // 0x28
PROPERTIES:
  syncableTransforms  get=0x02B76770  set=0x05392320
  syncableProperties  get=0x04D88320  set=0x067D5E9C
  rigStates  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x0B1548C8  token=0x60002AA  System.Void .ctor(UnityEngine.Transform[] transforms, UnityEngine.Animations.Rigging.SyncableProperties[] properties, System.Int32 rigCount)
  RVA=0x0B154740  token=0x60002AB  System.Int32[] UniqueTransformIndices(UnityEngine.Transform[] transforms)
  RVA=0x04D86340  token=0x60002B2  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B154898  token=0x60002B3  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006E
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Animations.Rigging.RigUtils.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.FieldInfo,System.Boolean><>9__6_0  // static @ 0x8
METHODS:
  RVA=0x0B158E18  token=0x60002B4  System.Void .cctor()
  RVA=0x041E1670  token=0x60002B5  System.Void .ctor()
  RVA=0x0B158DD8  token=0x60002B6  System.Boolean <ExtractNestedPropertyType>b__6_0(System.Reflection.FieldInfo info)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0xA0
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_Array  // 0x10
  private           System.Int32                    m_Index  // 0x98
PROPERTIES:
  Current  get=0x0B14BF40
  System.Collections.IEnumerator.Current  get=0x0B14BE1C
METHODS:
  RVA=0x0B14BE74  token=0x60002ED  System.Void .ctor(UnityEngine.Animations.Rigging.WeightedTransformArray& array)
  RVA=0x0B14BDC8  token=0x60002EE  System.Boolean MoveNext()
  RVA=0x04DAB0B0  token=0x60002EF  System.Void Reset()
  RVA=0x041E1670  token=0x60002F0  System.Void System.IDisposable.Dispose()
END_CLASS

CLASS: Style
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x48
FIELDS:
  public            UnityEngine.Mesh                shape  // 0x10
  public            UnityEngine.Color               color  // 0x18
  public            System.Single                   size  // 0x28
  public            UnityEngine.Vector3             position  // 0x2c
  public            UnityEngine.Vector3             rotation  // 0x38
METHODS:
END_CLASS

CLASS: BoneShape
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.BoneRenderer.BoneShapeLine  // const
  public    static  UnityEngine.Animations.Rigging.BoneRenderer.BoneShapePyramid  // const
  public    static  UnityEngine.Animations.Rigging.BoneRenderer.BoneShapeBox  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000086
SIZE:  0x18
FIELDS:
  public            System.Single                   totalLength  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600031F  System.Void .ctor()
  RVA=0x04DAB1A0  token=0x6000320  System.Void <ExtractSteps>b__0(System.Single length)
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

CLASS: UnityEngine.Animations.Rigging.CacheIndex
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  private           System.Int32                    idx  // 0x10
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.AnimationJobCache
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<System.Single>m_Data  // 0x10
METHODS:
  RVA=0x0B14850C  token=0x6000003  System.Void .ctor(System.Single[] data)
  RVA=0x0B1484D0  token=0x6000004  System.Void Dispose()
  RVA=0x04DAB030  token=0x6000005  System.Single GetRaw(UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset)
  RVA=0x04DAB040  token=0x6000006  System.Void SetRaw(System.Single val, UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset)
  RVA=-1  // generic def  token=0x6000007  T Get(UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset)
  RVA=-1  // generic def  token=0x6000008  System.Void Set(T val, UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset)
  RVA=-1  // generic def  token=0x6000009  System.Void SetArray(T[] v, UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.AnimationJobCacheBuilder
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<System.Single>m_Data  // 0x10
METHODS:
  RVA=0x0B148458  token=0x600000A  System.Void .ctor()
  RVA=0x0B1481D0  token=0x600000B  UnityEngine.Animations.Rigging.CacheIndex Add(System.Single v)
  RVA=0x0B148240  token=0x600000C  UnityEngine.Animations.Rigging.CacheIndex Add(UnityEngine.Vector2 v)
  RVA=0x0B148050  token=0x600000D  UnityEngine.Animations.Rigging.CacheIndex Add(UnityEngine.Vector3 v)
  RVA=0x0B147F90  token=0x600000E  UnityEngine.Animations.Rigging.CacheIndex Add(UnityEngine.Vector4 v)
  RVA=0x0B1481B0  token=0x600000F  UnityEngine.Animations.Rigging.CacheIndex Add(UnityEngine.Quaternion v)
  RVA=0x0B1480F8  token=0x6000010  UnityEngine.Animations.Rigging.CacheIndex Add(UnityEngine.Animations.Rigging.AffineTransform tx)
  RVA=0x0B1482C8  token=0x6000011  UnityEngine.Animations.Rigging.CacheIndex AllocateChunk(System.Int32 size)
  RVA=0x0B1483CC  token=0x6000012  System.Void SetValue(UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 offset, System.Single value)
  RVA=0x0B148360  token=0x6000013  UnityEngine.Animations.Rigging.AnimationJobCache Build()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BlendConstraintJob
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0xD4
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Int32                    k_BlendTranslationMask  // const
  private   static  System.Int32                    k_BlendRotationMask  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlesourceA  // 0x1c
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlesourceB  // 0x34
  public            UnityEngine.Animations.Rigging.AffineTransformsourceAOffset  // 0x4c
  public            UnityEngine.Animations.Rigging.AffineTransformsourceBOffset  // 0x68
  public            UnityEngine.Animations.Rigging.BoolPropertyblendPosition  // 0x84
  public            UnityEngine.Animations.Rigging.BoolPropertyblendRotation  // 0x94
  public            UnityEngine.Animations.Rigging.FloatPropertypositionWeight  // 0xa4
  public            UnityEngine.Animations.Rigging.FloatPropertyrotationWeight  // 0xb4
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0xc4
PROPERTIES:
  jobWeight  get=0x04DAB050  set=0x04DAB060
METHODS:
  RVA=0x041E1670  token=0x6000016  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B149CF0  token=0x6000017  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IBlendConstraintData
TYPE:  interface
TOKEN: 0x2000008
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObjectA  get=-1  // abstract
  sourceObjectB  get=-1  // abstract
  maintainPositionOffsets  get=-1  // abstract
  maintainRotationOffsets  get=-1  // abstract
  blendPositionBoolProperty  get=-1  // abstract
  blendRotationBoolProperty  get=-1  // abstract
  positionWeightFloatProperty  get=-1  // abstract
  rotationWeightFloatProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BlendConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000009
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000021  UnityEngine.Animations.Rigging.BlendConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x6000022  System.Void Destroy(UnityEngine.Animations.Rigging.BlendConstraintJob job)
  RVA=-1  // not resolved  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ChainIKConstraintJob
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0xC8
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadWriteTransformHandle>chain  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandletarget  // 0x20
  public            UnityEngine.Animations.Rigging.AffineTransformtargetOffset  // 0x38
  public            Unity.Collections.NativeArray<System.Single>linkLengths  // 0x58
  public            Unity.Collections.NativeArray<UnityEngine.Vector3>linkPositions  // 0x68
  public            UnityEngine.Animations.Rigging.FloatPropertychainRotationWeight  // 0x78
  public            UnityEngine.Animations.Rigging.FloatPropertytipRotationWeight  // 0x88
  public            UnityEngine.Animations.Rigging.CacheIndextoleranceIdx  // 0x98
  public            UnityEngine.Animations.Rigging.CacheIndexmaxIterationsIdx  // 0x9c
  public            UnityEngine.Animations.Rigging.AnimationJobCachecache  // 0xa0
  public            System.Single                   maxReach  // 0xb0
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0xb4
PROPERTIES:
  jobWeight  get=0x04DAB070  set=0x04DAB080
METHODS:
  RVA=0x041E1670  token=0x6000026  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14A6EC  token=0x6000027  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IChainIKConstraintData
TYPE:  interface
TOKEN: 0x200000B
FIELDS:
PROPERTIES:
  root  get=-1  // abstract
  tip  get=-1  // abstract
  target  get=-1  // abstract
  maxIterations  get=-1  // abstract
  tolerance  get=-1  // abstract
  maintainTargetPositionOffset  get=-1  // abstract
  maintainTargetRotationOffset  get=-1  // abstract
  chainRotationWeightFloatProperty  get=-1  // abstract
  tipRotationWeightFloatProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ChainIKConstraintJobBinder`1
TYPE:  class
TOKEN: 0x200000C
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000031  UnityEngine.Animations.Rigging.ChainIKConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x6000032  System.Void Destroy(UnityEngine.Animations.Rigging.ChainIKConstraintJob job)
  RVA=-1  // not resolved  token=0x6000033  System.Void Update(UnityEngine.Animations.Rigging.ChainIKConstraintJob job, T& data)
  RVA=-1  // not resolved  token=0x6000034  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.DampedTransformJob
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0xA8
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Single                   k_FixedDt  // const
  private   static  System.Single                   k_DampFactor  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlesource  // 0x1c
  public            UnityEngine.Animations.Rigging.AffineTransformlocalBindTx  // 0x34
  public            UnityEngine.Vector3             aimBindAxis  // 0x50
  public            UnityEngine.Animations.Rigging.AffineTransformprevDrivenTx  // 0x5c
  public            UnityEngine.Animations.Rigging.FloatPropertydampPosition  // 0x78
  public            UnityEngine.Animations.Rigging.FloatPropertydampRotation  // 0x88
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x98
PROPERTIES:
  jobWeight  get=0x04DAB090  set=0x04DAB0A0
METHODS:
  RVA=0x041E1670  token=0x6000037  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14B568  token=0x6000038  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IDampedTransformData
TYPE:  interface
TOKEN: 0x200000E
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObject  get=-1  // abstract
  maintainAim  get=-1  // abstract
  dampPositionFloatProperty  get=-1  // abstract
  dampRotationFloatProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.DampedTransformJobBinder`1
TYPE:  class
TOKEN: 0x200000F
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600003E  UnityEngine.Animations.Rigging.DampedTransformJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600003F  System.Void Destroy(UnityEngine.Animations.Rigging.DampedTransformJob job)
  RVA=-1  // not resolved  token=0x6000040  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
TYPE:  interface
TOKEN: 0x2000010
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000041  T Get(UnityEngine.Animations.AnimationStream stream)
  RVA=-1  // abstract  token=0x6000042  System.Void Set(UnityEngine.Animations.AnimationStream stream, T value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BoolProperty
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlevalue  // 0x10
METHODS:
  RVA=0x0B14A3A0  token=0x6000043  UnityEngine.Animations.Rigging.BoolProperty Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B14A368  token=0x6000044  UnityEngine.Animations.Rigging.BoolProperty BindCustom(UnityEngine.Animator animator, System.String property)
  RVA=0x0B14A430  token=0x6000045  System.Boolean Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14A46C  token=0x6000046  System.Void Set(UnityEngine.Animations.AnimationStream stream, System.Boolean v)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IntProperty
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlevalue  // 0x10
METHODS:
  RVA=0x0B14A3A0  token=0x6000047  UnityEngine.Animations.Rigging.IntProperty Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B14C060  token=0x6000048  UnityEngine.Animations.Rigging.IntProperty BindCustom(UnityEngine.Animator animator, System.String property)
  RVA=0x0B14C098  token=0x6000049  System.Int32 Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14C0D4  token=0x600004A  System.Void Set(UnityEngine.Animations.AnimationStream stream, System.Int32 v)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.FloatProperty
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlevalue  // 0x10
METHODS:
  RVA=0x0B14A3A0  token=0x600004B  UnityEngine.Animations.Rigging.FloatProperty Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B14BFB0  token=0x600004C  UnityEngine.Animations.Rigging.FloatProperty BindCustom(UnityEngine.Animator animator, System.String property)
  RVA=0x0B14BFE8  token=0x600004D  System.Single Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14C024  token=0x600004E  System.Void Set(UnityEngine.Animations.AnimationStream stream, System.Single v)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector2Property
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x30
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlex  // 0x10
  public            UnityEngine.Animations.PropertyStreamHandley  // 0x20
METHODS:
  RVA=0x0B158F44  token=0x600004F  UnityEngine.Animations.Rigging.Vector2Property Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B158E7C  token=0x6000050  UnityEngine.Animations.Rigging.Vector2Property BindCustom(UnityEngine.Animator animator, System.String name)
  RVA=0x0B159064  token=0x6000051  UnityEngine.Vector2 Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15910C  token=0x6000052  System.Void Set(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector2 value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector3Property
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x40
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlex  // 0x10
  public            UnityEngine.Animations.PropertyStreamHandley  // 0x20
  public            UnityEngine.Animations.PropertyStreamHandlez  // 0x30
METHODS:
  RVA=0x0B159B68  token=0x6000053  UnityEngine.Animations.Rigging.Vector3Property Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B159A58  token=0x6000054  UnityEngine.Animations.Rigging.Vector3Property BindCustom(UnityEngine.Animator animator, System.String name)
  RVA=0x0B159CE0  token=0x6000055  UnityEngine.Vector3 Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B159DE0  token=0x6000056  System.Void Set(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector3IntProperty
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x40
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlex  // 0x10
  public            UnityEngine.Animations.PropertyStreamHandley  // 0x20
  public            UnityEngine.Animations.PropertyStreamHandlez  // 0x30
METHODS:
  RVA=0x0B159718  token=0x6000057  UnityEngine.Animations.Rigging.Vector3IntProperty Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B159608  token=0x6000058  UnityEngine.Animations.Rigging.Vector3IntProperty BindCustom(UnityEngine.Animator animator, System.String name)
  RVA=0x0B159890  token=0x6000059  UnityEngine.Vector3Int Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B159980  token=0x600005A  System.Void Set(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3Int value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector3BoolProperty
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x40
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlex  // 0x10
  public            UnityEngine.Animations.PropertyStreamHandley  // 0x20
  public            UnityEngine.Animations.PropertyStreamHandlez  // 0x30
METHODS:
  RVA=0x0B1592C8  token=0x600005B  UnityEngine.Animations.Rigging.Vector3BoolProperty Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B1591B8  token=0x600005C  UnityEngine.Animations.Rigging.Vector3BoolProperty BindCustom(UnityEngine.Animator animator, System.String name)
  RVA=0x0B159440  token=0x600005D  UnityEngine.Animations.Rigging.Vector3Bool Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B159530  token=0x600005E  System.Void Set(UnityEngine.Animations.AnimationStream stream, UnityEngine.Animations.Rigging.Vector3Bool value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector4Property
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x50
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimatableProperty`1
FIELDS:
  public            UnityEngine.Animations.PropertyStreamHandlex  // 0x10
  public            UnityEngine.Animations.PropertyStreamHandley  // 0x20
  public            UnityEngine.Animations.PropertyStreamHandlez  // 0x30
  public            UnityEngine.Animations.PropertyStreamHandlew  // 0x40
METHODS:
  RVA=0x0B15A00C  token=0x600005F  UnityEngine.Animations.Rigging.Vector4Property Bind(UnityEngine.Animator animator, UnityEngine.Component component, System.String name)
  RVA=0x0B159EBC  token=0x6000060  UnityEngine.Animations.Rigging.Vector4Property BindCustom(UnityEngine.Animator animator, System.String name)
  RVA=0x0B15A1D8  token=0x6000061  UnityEngine.Vector4 Get(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15A324  token=0x6000062  System.Void Set(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector4 value)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IAnimationJobBinder
TYPE:  interface
TOKEN: 0x2000019
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000063  UnityEngine.Animations.IAnimationJob Create(UnityEngine.Animator animator, UnityEngine.Animations.Rigging.IAnimationJobData data, UnityEngine.Component component)
  RVA=-1  // abstract  token=0x6000064  System.Void Destroy(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // abstract  token=0x6000065  System.Void Update(UnityEngine.Animations.IAnimationJob job, UnityEngine.Animations.Rigging.IAnimationJobData data)
  RVA=-1  // abstract  token=0x6000066  UnityEngine.Animations.AnimationScriptPlayable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.IAnimationJob job)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
TYPE:  abstract class
TOKEN: 0x200001A
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobBinder
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000067  TJob Create(UnityEngine.Animator animator, TData& data, UnityEngine.Component component)
  RVA=-1  // abstract  token=0x6000068  System.Void Destroy(TJob job)
  RVA=-1  // not resolved  token=0x6000069  System.Void Update(TJob job, TData& data)
  RVA=-1  // not resolved  token=0x600006A  UnityEngine.Animations.IAnimationJob UnityEngine.Animations.Rigging.IAnimationJobBinder.Create(UnityEngine.Animator animator, UnityEngine.Animations.Rigging.IAnimationJobData data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600006B  System.Void UnityEngine.Animations.Rigging.IAnimationJobBinder.Destroy(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x600006C  System.Void UnityEngine.Animations.Rigging.IAnimationJobBinder.Update(UnityEngine.Animations.IAnimationJob job, UnityEngine.Animations.Rigging.IAnimationJobData data)
  RVA=-1  // not resolved  token=0x600006D  UnityEngine.Animations.AnimationScriptPlayable UnityEngine.Animations.Rigging.IAnimationJobBinder.CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x600006E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IAnimationJobData
TYPE:  interface
TOKEN: 0x200001B
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600006F  System.Boolean IsValid()
  RVA=-1  // abstract  token=0x6000070  System.Void SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IWeightedAnimationJob
TYPE:  interface
TOKEN: 0x200001C
IMPLEMENTS: UnityEngine.Animations.IAnimationJob
FIELDS:
PROPERTIES:
  jobWeight  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiAimConstraintJob
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x128
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Single                   k_Epsilon  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandledrivenParent  // 0x1c
  public            UnityEngine.Animations.Rigging.Vector3PropertydrivenOffset  // 0x34
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadOnlyTransformHandle>sourceTransforms  // 0x68
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>sourceWeights  // 0x78
  public            Unity.Collections.NativeArray<UnityEngine.Quaternion>sourceOffsets  // 0x88
  public            Unity.Collections.NativeArray<System.Single>weightBuffer  // 0x98
  public            UnityEngine.Vector3             aimAxis  // 0xa8
  public            UnityEngine.Vector3             upAxis  // 0xb4
  public            UnityEngine.Animations.Rigging.MultiAimConstraintJob.WorldUpTypeworldUpType  // 0xc0
  public            UnityEngine.Vector3             worldUpAxis  // 0xc4
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandleworldUpObject  // 0xd0
  public            UnityEngine.Vector3             axesMask  // 0xe8
  public            UnityEngine.Animations.Rigging.FloatPropertyminLimit  // 0xf4
  public            UnityEngine.Animations.Rigging.FloatPropertymaxLimit  // 0x104
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x114
PROPERTIES:
  jobWeight  get=0x04DAB120  set=0x04DAB130
METHODS:
  RVA=0x041E1670  token=0x6000075  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14C910  token=0x6000076  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14C690  token=0x6000077  UnityEngine.Vector3 ComputeWorldUpVector(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IMultiAimConstraintData
TYPE:  interface
TOKEN: 0x200001F
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObjects  get=-1  // abstract
  maintainOffset  get=-1  // abstract
  aimAxis  get=-1  // abstract
  upAxis  get=-1  // abstract
  worldUpType  get=-1  // abstract
  worldUpAxis  get=-1  // abstract
  worldUpObject  get=-1  // abstract
  constrainedXAxis  get=-1  // abstract
  constrainedYAxis  get=-1  // abstract
  constrainedZAxis  get=-1  // abstract
  offsetVector3Property  get=-1  // abstract
  minLimitFloatProperty  get=-1  // abstract
  maxLimitFloatProperty  get=-1  // abstract
  sourceObjectsProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiAimConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000020
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000087  UnityEngine.Animations.Rigging.MultiAimConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x6000088  System.Void Destroy(UnityEngine.Animations.Rigging.MultiAimConstraintJob job)
  RVA=-1  // not resolved  token=0x6000089  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiParentConstraintJob
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0xA0
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Single                   k_Epsilon  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandledrivenParent  // 0x1c
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadOnlyTransformHandle>sourceTransforms  // 0x38
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>sourceWeights  // 0x48
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.AffineTransform>sourceOffsets  // 0x58
  public            Unity.Collections.NativeArray<System.Single>weightBuffer  // 0x68
  public            UnityEngine.Vector3             positionAxesMask  // 0x78
  public            UnityEngine.Vector3             rotationAxesMask  // 0x84
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x90
PROPERTIES:
  jobWeight  get=0x04D90620  set=0x04D9DEE0
METHODS:
  RVA=0x041E1670  token=0x600008C  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14DD20  token=0x600008D  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IMultiParentConstraintData
TYPE:  interface
TOKEN: 0x2000022
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObjects  get=-1  // abstract
  maintainPositionOffset  get=-1  // abstract
  maintainRotationOffset  get=-1  // abstract
  constrainedPositionXAxis  get=-1  // abstract
  constrainedPositionYAxis  get=-1  // abstract
  constrainedPositionZAxis  get=-1  // abstract
  constrainedRotationXAxis  get=-1  // abstract
  constrainedRotationYAxis  get=-1  // abstract
  constrainedRotationZAxis  get=-1  // abstract
  sourceObjectsProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiParentConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000023
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000099  UnityEngine.Animations.Rigging.MultiParentConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600009A  System.Void Destroy(UnityEngine.Animations.Rigging.MultiParentConstraintJob job)
  RVA=-1  // not resolved  token=0x600009B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiPositionConstraintJob
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0xC8
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Single                   k_Epsilon  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandledrivenParent  // 0x1c
  public            UnityEngine.Animations.Rigging.Vector3PropertydrivenOffset  // 0x34
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadOnlyTransformHandle>sourceTransforms  // 0x68
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>sourceWeights  // 0x78
  public            Unity.Collections.NativeArray<UnityEngine.Vector3>sourceOffsets  // 0x88
  public            Unity.Collections.NativeArray<System.Single>weightBuffer  // 0x98
  public            UnityEngine.Vector3             axesMask  // 0xa8
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0xb4
PROPERTIES:
  jobWeight  get=0x04DAB070  set=0x04DAB080
METHODS:
  RVA=0x041E1670  token=0x600009E  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14EC7C  token=0x600009F  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IMultiPositionConstraintData
TYPE:  interface
TOKEN: 0x2000025
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObjects  get=-1  // abstract
  maintainOffset  get=-1  // abstract
  offsetVector3Property  get=-1  // abstract
  sourceObjectsProperty  get=-1  // abstract
  constrainedXAxis  get=-1  // abstract
  constrainedYAxis  get=-1  // abstract
  constrainedZAxis  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiPositionConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000026
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60000A8  UnityEngine.Animations.Rigging.MultiPositionConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x60000A9  System.Void Destroy(UnityEngine.Animations.Rigging.MultiPositionConstraintJob job)
  RVA=-1  // not resolved  token=0x60000AA  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiReferentialConstraintJob
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x68
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.IntPropertydriver  // 0x10
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadWriteTransformHandle>sources  // 0x20
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.AffineTransform>sourceBindTx  // 0x30
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.AffineTransform>offsetTx  // 0x40
  private           System.Int32                    m_PrevDriverIdx  // 0x50
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x54
PROPERTIES:
  jobWeight  get=0x04DAAF10  set=0x04DAAF20
METHODS:
  RVA=0x041E1670  token=0x60000AD  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14F900  token=0x60000AE  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B14FDB8  token=0x60000AF  System.Void UpdateOffsets(System.Int32 driver)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IMultiReferentialConstraintData
TYPE:  interface
TOKEN: 0x2000028
FIELDS:
PROPERTIES:
  driverValue  get=-1  // abstract
  driverIntProperty  get=-1  // abstract
  sourceObjects  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiReferentialConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000029
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60000B3  UnityEngine.Animations.Rigging.MultiReferentialConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x60000B4  System.Void Destroy(UnityEngine.Animations.Rigging.MultiReferentialConstraintJob job)
  RVA=-1  // not resolved  token=0x60000B5  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiRotationConstraintJob
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0xC8
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static  System.Single                   k_Epsilon  // const
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandledrivenParent  // 0x1c
  public            UnityEngine.Animations.Rigging.Vector3PropertydrivenOffset  // 0x34
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadOnlyTransformHandle>sourceTransforms  // 0x68
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>sourceWeights  // 0x78
  public            Unity.Collections.NativeArray<UnityEngine.Quaternion>sourceOffsets  // 0x88
  public            Unity.Collections.NativeArray<System.Single>weightBuffer  // 0x98
  public            UnityEngine.Vector3             axesMask  // 0xa8
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0xb4
PROPERTIES:
  jobWeight  get=0x04DAB070  set=0x04DAB080
METHODS:
  RVA=0x041E1670  token=0x60000B8  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1502E8  token=0x60000B9  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IMultiRotationConstraintData
TYPE:  interface
TOKEN: 0x200002B
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObjects  get=-1  // abstract
  maintainOffset  get=-1  // abstract
  offsetVector3Property  get=-1  // abstract
  sourceObjectsProperty  get=-1  // abstract
  constrainedXAxis  get=-1  // abstract
  constrainedYAxis  get=-1  // abstract
  constrainedZAxis  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiRotationConstraintJobBinder`1
TYPE:  class
TOKEN: 0x200002C
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60000C2  UnityEngine.Animations.Rigging.MultiRotationConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x60000C3  System.Void Destroy(UnityEngine.Animations.Rigging.MultiRotationConstraintJob job)
  RVA=-1  // not resolved  token=0x60000C4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideTransformJob
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x128
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandledriven  // 0x10
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlesource  // 0x1c
  public            UnityEngine.Animations.Rigging.AffineTransformsourceInvLocalBindTx  // 0x34
  public            UnityEngine.Quaternion          sourceToWorldRot  // 0x50
  public            UnityEngine.Quaternion          sourceToLocalRot  // 0x60
  public            UnityEngine.Quaternion          sourceToPivotRot  // 0x70
  public            UnityEngine.Animations.Rigging.CacheIndexspaceIdx  // 0x80
  public            UnityEngine.Animations.Rigging.CacheIndexsourceToCurrSpaceRotIdx  // 0x84
  public            UnityEngine.Animations.Rigging.Vector3Propertyposition  // 0x88
  public            UnityEngine.Animations.Rigging.Vector3Propertyrotation  // 0xb8
  public            UnityEngine.Animations.Rigging.FloatPropertypositionWeight  // 0xe8
  public            UnityEngine.Animations.Rigging.FloatPropertyrotationWeight  // 0xf8
  public            UnityEngine.Animations.Rigging.AnimationJobCachecache  // 0x108
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x118
PROPERTIES:
  jobWeight  get=0x04DAA110  set=0x04DAA170
METHODS:
  RVA=0x041E1670  token=0x60000C7  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1510A4  token=0x60000C8  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B151900  token=0x60000C9  System.Void UpdateSpace(System.Int32 space)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IOverrideTransformData
TYPE:  interface
TOKEN: 0x200002F
FIELDS:
PROPERTIES:
  constrainedObject  get=-1  // abstract
  sourceObject  get=-1  // abstract
  space  get=-1  // abstract
  positionWeightFloatProperty  get=-1  // abstract
  rotationWeightFloatProperty  get=-1  // abstract
  positionVector3Property  get=-1  // abstract
  rotationVector3Property  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideTransformJobBinder`1
TYPE:  class
TOKEN: 0x2000030
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60000D1  UnityEngine.Animations.Rigging.OverrideTransformJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x60000D2  System.Void Destroy(UnityEngine.Animations.Rigging.OverrideTransformJob job)
  RVA=-1  // not resolved  token=0x60000D3  System.Void Update(UnityEngine.Animations.Rigging.OverrideTransformJob job, T& data)
  RVA=-1  // not resolved  token=0x60000D4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0xF0
IMPLEMENTS: UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.TransformSyncertransformSyncer  // 0x10
  public            UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.PropertySyncerpropertySyncer  // 0x30
  public            UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.PropertySyncerrigWeightSyncer  // 0x60
  public            UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob.PropertySyncerconstraintWeightSyncer  // 0x90
  public            Unity.Collections.NativeArray<System.Single>rigStates  // 0xc0
  public            Unity.Collections.NativeArray<System.Int32>rigConstraintEndIdx  // 0xd0
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>modulatedConstraintWeights  // 0xe0
METHODS:
  RVA=0x041E1670  token=0x60000D5  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B154A50  token=0x60000D6  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.SyncableProperties
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x20
FIELDS:
  public            UnityEngine.Animations.Rigging.RigPropertiesrig  // 0x10
  public            UnityEngine.Animations.Rigging.ConstraintProperties[]constraints  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IRigSyncSceneToStreamData
TYPE:  interface
TOKEN: 0x2000035
FIELDS:
PROPERTIES:
  syncableTransforms  get=-1  // abstract
  syncableProperties  get=-1  // abstract
  rigStates  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigSyncSceneToStreamJobBinder`1
TYPE:  class
TOKEN: 0x2000036
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
  private   static  System.String[]                 s_PropertyElementNames  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000E3  UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x60000E4  System.Void Destroy(UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob job)
  RVA=-1  // not resolved  token=0x60000E5  System.Void Update(UnityEngine.Animations.Rigging.RigSyncSceneToStreamJob job, T& data)
  RVA=-1  // not resolved  token=0x60000E6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000E7  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ReadWriteTransformHandle
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x1C
FIELDS:
  private           UnityEngine.Animations.TransformStreamHandlem_Handle  // 0x10
METHODS:
  RVA=0x0B15283C  token=0x60000E8  UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152894  token=0x60000E9  UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1528E4  token=0x60000EA  UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15293C  token=0x60000EB  System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B152B60  token=0x60000EC  System.Void SetLocalPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position)
  RVA=0x0B152BB4  token=0x60000ED  System.Void SetLocalRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation)
  RVA=0x0B152BFC  token=0x60000EE  System.Void SetLocalScale(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 scale)
  RVA=0x0B152C50  token=0x60000EF  System.Void SetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, System.Boolean useMask)
  RVA=0x0B15298C  token=0x60000F0  UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1529E4  token=0x60000F1  UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1527F8  token=0x60000F2  System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B152CDC  token=0x60000F3  System.Void SetPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position)
  RVA=0x0B152D30  token=0x60000F4  System.Void SetRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation)
  RVA=0x0B152AE8  token=0x60000F5  System.Void SetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useMask)
  RVA=0x0B152A34  token=0x60000F6  System.Boolean IsResolved(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152A70  token=0x60000F7  System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152AAC  token=0x60000F8  System.Void Resolve(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152664  token=0x60000F9  UnityEngine.Animations.Rigging.ReadWriteTransformHandle Bind(UnityEngine.Animator animator, UnityEngine.Transform transform)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ReadOnlyTransformHandle
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x28
FIELDS:
  private           UnityEngine.Animations.TransformStreamHandlem_StreamHandle  // 0x10
  private           UnityEngine.Animations.TransformSceneHandlem_SceneHandle  // 0x1c
  private           System.Byte                     m_InStream  // 0x24
METHODS:
  RVA=0x0B1522EC  token=0x60000FA  UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152364  token=0x60000FB  UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1523D4  token=0x60000FC  UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15244C  token=0x60000FD  System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B1524A8  token=0x60000FE  UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152520  token=0x60000FF  UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1522A0  token=0x6000100  System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B152590  token=0x6000101  System.Boolean IsResolved(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B1525D4  token=0x6000102  System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152620  token=0x6000103  System.Void Resolve(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B152154  token=0x6000104  UnityEngine.Animations.Rigging.ReadOnlyTransformHandle Bind(UnityEngine.Animator animator, UnityEngine.Transform transform)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistChainConstraintJob
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x78
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandlerootTarget  // 0x10
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandletipTarget  // 0x1c
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadWriteTransformHandle>chain  // 0x28
  public            Unity.Collections.NativeArray<System.Single>steps  // 0x38
  public            Unity.Collections.NativeArray<System.Single>weights  // 0x48
  public            Unity.Collections.NativeArray<UnityEngine.Quaternion>rotations  // 0x58
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x68
PROPERTIES:
  jobWeight  get=0x04D87E00  set=0x04DA61E0
METHODS:
  RVA=0x041E1670  token=0x6000107  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B157AC0  token=0x6000108  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ITwistChainConstraintData
TYPE:  interface
TOKEN: 0x200003A
FIELDS:
PROPERTIES:
  root  get=-1  // abstract
  tip  get=-1  // abstract
  rootTarget  get=-1  // abstract
  tipTarget  get=-1  // abstract
  curve  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistChainConstraintJobBinder`1
TYPE:  class
TOKEN: 0x200003B
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600010E  UnityEngine.Animations.Rigging.TwistChainConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600010F  System.Void Destroy(UnityEngine.Animations.Rigging.TwistChainConstraintJob job)
  RVA=-1  // not resolved  token=0x6000110  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistCorrectionJob
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x98
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlesource  // 0x10
  public            UnityEngine.Quaternion          sourceInverseBindRotation  // 0x28
  public            UnityEngine.Vector3             axisMask  // 0x38
  public            Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadWriteTransformHandle>twistTransforms  // 0x48
  public            Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>twistWeights  // 0x58
  public            Unity.Collections.NativeArray<UnityEngine.Quaternion>twistBindRotations  // 0x68
  public            Unity.Collections.NativeArray<System.Single>weightBuffer  // 0x78
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0x88
PROPERTIES:
  jobWeight  get=0x04D8E8F0  set=0x04DA96F0
METHODS:
  RVA=0x041E1670  token=0x6000113  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15820C  token=0x6000114  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x04DAB160  token=0x6000115  UnityEngine.Quaternion TwistRotation(UnityEngine.Vector3 axis, UnityEngine.Quaternion rot)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ITwistCorrectionData
TYPE:  interface
TOKEN: 0x200003D
FIELDS:
PROPERTIES:
  source  get=-1  // abstract
  twistNodes  get=-1  // abstract
  twistAxis  get=-1  // abstract
  twistNodesProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistCorrectionJobBinder`1
TYPE:  class
TOKEN: 0x200003E
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600011A  UnityEngine.Animations.Rigging.TwistCorrectionJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600011B  System.Void Destroy(UnityEngine.Animations.Rigging.TwistCorrectionJob job)
  RVA=-1  // not resolved  token=0x600011C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwoBoneIKConstraintJob
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0xC0
IMPLEMENTS: UnityEngine.Animations.Rigging.IWeightedAnimationJob UnityEngine.Animations.IAnimationJob
FIELDS:
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandleroot  // 0x10
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandlemid  // 0x1c
  public            UnityEngine.Animations.Rigging.ReadWriteTransformHandletip  // 0x28
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandlehint  // 0x34
  public            UnityEngine.Animations.Rigging.ReadOnlyTransformHandletarget  // 0x4c
  public            UnityEngine.Animations.Rigging.AffineTransformtargetOffset  // 0x64
  public            UnityEngine.Animations.Rigging.FloatPropertytargetPositionWeight  // 0x80
  public            UnityEngine.Animations.Rigging.FloatPropertytargetRotationWeight  // 0x90
  public            UnityEngine.Animations.Rigging.FloatPropertyhintWeight  // 0xa0
  private           UnityEngine.Animations.Rigging.FloatProperty<jobWeight>k__BackingField  // 0xb0
PROPERTIES:
  jobWeight  get=0x04DA1260  set=0x04DA1300
METHODS:
  RVA=0x041E1670  token=0x600011F  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B15896C  token=0x6000120  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ITwoBoneIKConstraintData
TYPE:  interface
TOKEN: 0x2000040
FIELDS:
PROPERTIES:
  root  get=-1  // abstract
  mid  get=-1  // abstract
  tip  get=-1  // abstract
  target  get=-1  // abstract
  hint  get=-1  // abstract
  maintainTargetPositionOffset  get=-1  // abstract
  maintainTargetRotationOffset  get=-1  // abstract
  targetPositionWeightFloatProperty  get=-1  // abstract
  targetRotationWeightFloatProperty  get=-1  // abstract
  hintWeightFloatProperty  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwoBoneIKConstraintJobBinder`1
TYPE:  class
TOKEN: 0x2000041
EXTENDS: UnityEngine.Animations.Rigging.AnimationJobBinder`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600012B  UnityEngine.Animations.Rigging.TwoBoneIKConstraintJob Create(UnityEngine.Animator animator, T& data, UnityEngine.Component component)
  RVA=-1  // not resolved  token=0x600012C  System.Void Destroy(UnityEngine.Animations.Rigging.TwoBoneIKConstraintJob job)
  RVA=-1  // not resolved  token=0x600012D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.WeightedTransformArrayBinder
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B15A440  token=0x600012E  System.Void BindReadOnlyTransforms(UnityEngine.Animator animator, UnityEngine.Component component, UnityEngine.Animations.Rigging.WeightedTransformArray weightedTransformArray, Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadOnlyTransformHandle>& transforms)
  RVA=0x0B15A554  token=0x600012F  System.Void BindReadWriteTransforms(UnityEngine.Animator animator, UnityEngine.Component component, UnityEngine.Animations.Rigging.WeightedTransformArray weightedTransformArray, Unity.Collections.NativeArray<UnityEngine.Animations.Rigging.ReadWriteTransformHandle>& transforms)
  RVA=0x0B15A664  token=0x6000130  System.Void BindWeights(UnityEngine.Animator animator, UnityEngine.Component component, UnityEngine.Animations.Rigging.WeightedTransformArray weightedTransformArray, System.String name, Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle>& weights)
  RVA=0x041E1670  token=0x6000131  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BlendConstraintData
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x38
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IBlendConstraintData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Transform           m_SourceA  // 0x18
  private           UnityEngine.Transform           m_SourceB  // 0x20
  private           System.Boolean                  m_BlendPosition  // 0x28
  private           System.Boolean                  m_BlendRotation  // 0x29
  private           System.Single                   m_PositionWeight  // 0x2c
  private           System.Single                   m_RotationWeight  // 0x30
  private           System.Boolean                  m_MaintainPositionOffsets  // 0x34
  private           System.Boolean                  m_MaintainRotationOffsets  // 0x35
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObjectA  get=0x04D88320  set=0x067D5E9C
  sourceObjectB  get=0x02B2ECC0  set=0x053908C0
  blendPosition  get=0x04D86340  set=0x04D86350
  blendRotation  get=0x04D866A0  set=0x04D866C0
  positionWeight  get=0x04D868F0  set=0x09F4C9E8
  rotationWeight  get=0x011EC8A0  set=0x09F4C9D0
  maintainPositionOffsets  get=0x04D86870  set=0x04D86880
  maintainRotationOffsets  get=0x04D86930  set=0x04D86950
  UnityEngine.Animations.Rigging.IBlendConstraintData.blendPositionBoolProperty  get=0x0B149C20
  UnityEngine.Animations.Rigging.IBlendConstraintData.blendRotationBoolProperty  get=0x0B149C54
  UnityEngine.Animations.Rigging.IBlendConstraintData.positionWeightFloatProperty  get=0x0B149C88
  UnityEngine.Animations.Rigging.IBlendConstraintData.rotationWeightFloatProperty  get=0x0B149CBC
METHODS:
  RVA=0x0B149B3C  token=0x6000148  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B149BD4  token=0x6000149  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BlendConstraint
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x48
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14A274  token=0x600014A  System.Void OnValidate()
  RVA=0x0B14A2D8  token=0x600014B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ChainIKConstraintData
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x40
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IChainIKConstraintData
FIELDS:
  private   static  System.Int32                    k_MinIterations  // const
  private   static  System.Int32                    k_MaxIterations  // const
  private   static  System.Single                   k_MinTolerance  // const
  private   static  System.Single                   k_MaxTolerance  // const
  private           UnityEngine.Transform           m_Root  // 0x10
  private           UnityEngine.Transform           m_Tip  // 0x18
  private           UnityEngine.Transform           m_Target  // 0x20
  private           System.Single                   m_ChainRotationWeight  // 0x28
  private           System.Single                   m_TipRotationWeight  // 0x2c
  private           System.Int32                    m_MaxIterations  // 0x30
  private           System.Single                   m_Tolerance  // 0x34
  private           System.Boolean                  m_MaintainTargetPositionOffset  // 0x38
  private           System.Boolean                  m_MaintainTargetRotationOffset  // 0x39
PROPERTIES:
  root  get=0x02B76770  set=0x05392320
  tip  get=0x04D88320  set=0x067D5E9C
  target  get=0x02B2ECC0  set=0x053908C0
  chainRotationWeight  get=0x04D86380  set=0x090D4BA8
  tipRotationWeight  get=0x04D868F0  set=0x09F4C9E8
  maxIterations  get=0x02B70F90  set=0x0B14A6A8
  tolerance  get=0x04D89850  set=0x0B14A6C8
  maintainTargetPositionOffset  get=0x011F33C0  set=0x011F33D0
  maintainTargetRotationOffset  get=0x04D867B0  set=0x04D867C0
  UnityEngine.Animations.Rigging.IChainIKConstraintData.chainRotationWeightFloatProperty  get=0x0B14A640
  UnityEngine.Animations.Rigging.IChainIKConstraintData.tipRotationWeightFloatProperty  get=0x0B14A674
METHODS:
  RVA=0x0B14A4A8  token=0x6000160  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14A5EC  token=0x6000161  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ChainIKConstraint
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x50
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14AE00  token=0x6000162  System.Void OnValidate()
  RVA=0x0B14AEB4  token=0x6000163  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.DampedTransformData
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x30
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IDampedTransformData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Transform           m_Source  // 0x18
  private           System.Single                   m_DampPosition  // 0x20
  private           System.Single                   m_DampRotation  // 0x24
  private           System.Boolean                  m_MaintainAim  // 0x28
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObject  get=0x04D88320  set=0x067D5E9C
  dampPosition  get=0x04D85F70  set=0x0B14B538
  dampRotation  get=0x04D88D40  set=0x0B14B550
  maintainAim  get=0x04D86340  set=0x04D86350
  UnityEngine.Animations.Rigging.IDampedTransformData.dampPositionFloatProperty  get=0x0B14B4D0
  UnityEngine.Animations.Rigging.IDampedTransformData.dampRotationFloatProperty  get=0x0B14B504
METHODS:
  RVA=0x0B14B41C  token=0x6000170  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14B494  token=0x6000171  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.DampedTransform
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x40
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14BD10  token=0x6000172  System.Void OnValidate()
  RVA=0x0B14BD74  token=0x6000173  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiAimConstraintData
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0xD0
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IMultiAimConstraintData
FIELDS:
  private   static  System.Single                   k_MinAngularLimit  // const
  private   static  System.Single                   k_MaxAngularLimit  // const
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_SourceObjects  // 0x18
  private           UnityEngine.Vector3             m_Offset  // 0xa0
  private           System.Single                   m_MinLimit  // 0xac
  private           System.Single                   m_MaxLimit  // 0xb0
  private           UnityEngine.Animations.Rigging.MultiAimConstraintData.Axism_AimAxis  // 0xb4
  private           UnityEngine.Animations.Rigging.MultiAimConstraintData.Axism_UpAxis  // 0xb8
  private           UnityEngine.Animations.Rigging.MultiAimConstraintData.WorldUpTypem_WorldUpType  // 0xbc
  private           UnityEngine.Transform           m_WorldUpObject  // 0xc0
  private           UnityEngine.Animations.Rigging.MultiAimConstraintData.Axism_WorldUpAxis  // 0xc8
  private           System.Boolean                  m_MaintainOffset  // 0xcc
  private           UnityEngine.Animations.Rigging.Vector3Boolm_ConstrainedAxes  // 0xcd
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObjects  get=0x04D9C2C0  set=0x0B14C62C
  maintainOffset  get=0x04D8E640  set=0x04D8E670
  offset  get=0x04D90DD0  set=0x04D90E30
  limits  get=0x04DA84B0  set=0x0B14C5CC
  aimAxis  get=0x04D88C90  set=0x04D88CF0
  upAxis  get=0x04D88C80  set=0x04D88CE0
  worldUpType  get=0x04D88CA0  set=0x04D88D00
  worldUpAxis  get=0x04D8E580  set=0x04D8E5A0
  worldUpObject  get=0x04D86250  set=0x04D84CE0
  constrainedXAxis  get=0x04DAB0C0  set=0x04DAB0F0
  constrainedYAxis  get=0x04DAB0D0  set=0x04DAB100
  constrainedZAxis  get=0x04DAB0E0  set=0x04DAB110
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.aimAxis  get=0x0B14C460
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.upAxis  get=0x0B14C564
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.worldUpType  get=0x04D88CA0
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.worldUpAxis  get=0x0B14C598
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.offsetVector3Property  get=0x0B14C4FC
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.minLimitFloatProperty  get=0x0B14C4C8
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.maxLimitFloatProperty  get=0x0B14C494
  UnityEngine.Animations.Rigging.IMultiAimConstraintData.sourceObjectsProperty  get=0x0B14C530
METHODS:
  RVA=0x0B14C17C  token=0x6000196  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14C38C  token=0x6000197  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
  RVA=0x0B14C110  token=0x6000198  UnityEngine.Vector3 Convert(UnityEngine.Animations.Rigging.MultiAimConstraintData.Axis axis)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiAimConstraint
TYPE:  class
TOKEN: 0x200004C
SIZE:  0xE0
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14D860  token=0x6000199  System.Void OnValidate()
  RVA=0x0B14DA04  token=0x600019A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiParentConstraintData
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0xA8
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IMultiParentConstraintData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_SourceObjects  // 0x18
  private           UnityEngine.Animations.Rigging.Vector3Boolm_ConstrainedPositionAxes  // 0xa0
  private           UnityEngine.Animations.Rigging.Vector3Boolm_ConstrainedRotationAxes  // 0xa3
  private           System.Boolean                  m_MaintainPositionOffset  // 0xa6
  private           System.Boolean                  m_MaintainRotationOffset  // 0xa7
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObjects  get=0x04D9C2C0  set=0x0B14C62C
  maintainPositionOffset  get=0x04D893A0  set=0x04D91BE0
  maintainRotationOffset  get=0x04DAB140  set=0x04DAB150
  constrainedPositionXAxis  get=0x02EC6590  set=0x02EC65A0
  constrainedPositionYAxis  get=0x04D88160  set=0x04D88180
  constrainedPositionZAxis  get=0x04D88170  set=0x04D88190
  constrainedRotationXAxis  get=0x04DAA1C0  set=0x04DAA980
  constrainedRotationYAxis  get=0x04D91BC0  set=0x04D91BD0
  constrainedRotationZAxis  get=0x04D893B0  set=0x04D91BF0
  UnityEngine.Animations.Rigging.IMultiParentConstraintData.sourceObjectsProperty  get=0x0B14DCEC
METHODS:
  RVA=0x0B14DA58  token=0x60001B0  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14DC68  token=0x60001B1  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiParentConstraint
TYPE:  class
TOKEN: 0x200004E
SIZE:  0xB8
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14E7EC  token=0x60001B2  System.Void OnValidate()
  RVA=0x0B14E928  token=0x60001B3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiPositionConstraintData
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0xB0
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IMultiPositionConstraintData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_SourceObjects  // 0x18
  private           UnityEngine.Vector3             m_Offset  // 0xa0
  private           UnityEngine.Animations.Rigging.Vector3Boolm_ConstrainedAxes  // 0xac
  private           System.Boolean                  m_MaintainOffset  // 0xaf
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObjects  get=0x04D9C2C0  set=0x0B14C62C
  maintainOffset  get=0x04DA34E0  set=0x04DA3530
  offset  get=0x04D90DD0  set=0x04D90E30
  constrainedXAxis  get=0x04D92ED0  set=0x04D92F60
  constrainedYAxis  get=0x04DA34D0  set=0x04DA3520
  constrainedZAxis  get=0x04DA34F0  set=0x04DA3540
  UnityEngine.Animations.Rigging.IMultiPositionConstraintData.offsetVector3Property  get=0x0B14EC14
  UnityEngine.Animations.Rigging.IMultiPositionConstraintData.sourceObjectsProperty  get=0x0B14EC48
METHODS:
  RVA=0x0B14E97C  token=0x60001C4  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14EB8C  token=0x60001C5  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiPositionConstraint
TYPE:  class
TOKEN: 0x2000050
SIZE:  0xC0
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14F378  token=0x60001C6  System.Void OnValidate()
  RVA=0x0B14F4B4  token=0x60001C7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiReferentialConstraintData
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IMultiReferentialConstraintData
FIELDS:
  private           System.Int32                    m_Driver  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Transform>m_SourceObjects  // 0x18
PROPERTIES:
  driver  get=0x02B77620  set=0x0B14F834
  sourceObjects  get=0x0B14F7B0  set=0x0B14F890
  UnityEngine.Animations.Rigging.IMultiReferentialConstraintData.sourceObjects  get=0x0B14F718
  UnityEngine.Animations.Rigging.IMultiReferentialConstraintData.driverValue  get=0x02B77620
  UnityEngine.Animations.Rigging.IMultiReferentialConstraintData.driverIntProperty  get=0x0B14F6E4
METHODS:
  RVA=0x0B14F508  token=0x60001CF  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B14F66C  token=0x60001D0  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
  RVA=0x0B14F760  token=0x60001D1  System.Void UpdateDriver()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiReferentialConstraint
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x30
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B14FF50  token=0x60001D2  System.Void OnValidate()
  RVA=0x0B14FF94  token=0x60001D3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiRotationConstraintData
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0xB0
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IMultiRotationConstraintData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_SourceObjects  // 0x18
  private           UnityEngine.Vector3             m_Offset  // 0xa0
  private           UnityEngine.Animations.Rigging.Vector3Boolm_ConstrainedAxes  // 0xac
  private           System.Boolean                  m_MaintainOffset  // 0xaf
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObjects  get=0x04D9C2C0  set=0x0B14C62C
  maintainOffset  get=0x04DA34E0  set=0x04DA3530
  offset  get=0x04D90DD0  set=0x04D90E30
  constrainedXAxis  get=0x04D92ED0  set=0x04D92F60
  constrainedYAxis  get=0x04DA34D0  set=0x04DA3520
  constrainedZAxis  get=0x04DA34F0  set=0x04DA3540
  UnityEngine.Animations.Rigging.IMultiRotationConstraintData.offsetVector3Property  get=0x0B150280
  UnityEngine.Animations.Rigging.IMultiRotationConstraintData.sourceObjectsProperty  get=0x0B1502B4
METHODS:
  RVA=0x0B14FFE8  token=0x60001E4  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B1501F8  token=0x60001E5  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.MultiRotationConstraint
TYPE:  class
TOKEN: 0x2000054
SIZE:  0xC0
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B1509D8  token=0x60001E6  System.Void OnValidate()
  RVA=0x0B150B14  token=0x60001E7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideTransformData
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x48
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.IOverrideTransformData
FIELDS:
  private           UnityEngine.Transform           m_ConstrainedObject  // 0x10
  private           UnityEngine.Transform           m_OverrideSource  // 0x18
  private           UnityEngine.Vector3             m_OverridePosition  // 0x20
  private           UnityEngine.Vector3             m_OverrideRotation  // 0x2c
  private           System.Single                   m_PositionWeight  // 0x38
  private           System.Single                   m_RotationWeight  // 0x3c
  private           UnityEngine.Animations.Rigging.OverrideTransformData.Spacem_Space  // 0x40
PROPERTIES:
  constrainedObject  get=0x02B76770  set=0x05392320
  sourceObject  get=0x04D88320  set=0x067D5E9C
  space  get=0x04D864F0  set=0x04D86740
  position  get=0x04D8C950  set=0x04D8C970
  rotation  get=0x04D90520  set=0x04D90500
  positionWeight  get=0x04D86BF0  set=0x0B151074
  rotationWeight  get=0x04D86AF0  set=0x0B15108C
  UnityEngine.Animations.Rigging.IOverrideTransformData.space  get=0x04D864F0
  UnityEngine.Animations.Rigging.IOverrideTransformData.positionWeightFloatProperty  get=0x0B150FD8
  UnityEngine.Animations.Rigging.IOverrideTransformData.rotationWeightFloatProperty  get=0x0B151040
  UnityEngine.Animations.Rigging.IOverrideTransformData.positionVector3Property  get=0x0B150FA4
  UnityEngine.Animations.Rigging.IOverrideTransformData.rotationVector3Property  get=0x0B15100C
METHODS:
  RVA=0x0B150EE4  token=0x60001FB  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B150F34  token=0x60001FC  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideTransform
TYPE:  class
TOKEN: 0x2000057
SIZE:  0x58
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B1519A0  token=0x60001FD  System.Void OnValidate()
  RVA=0x0B151A04  token=0x60001FE  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistChainConstraintData
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x38
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.ITwistChainConstraintData
FIELDS:
  private           UnityEngine.Transform           m_Root  // 0x10
  private           UnityEngine.Transform           m_Tip  // 0x18
  private           UnityEngine.Transform           m_RootTarget  // 0x20
  private           UnityEngine.Transform           m_TipTarget  // 0x28
  private           UnityEngine.AnimationCurve      m_Curve  // 0x30
PROPERTIES:
  root  get=0x02B76770  set=0x05392320
  tip  get=0x04D88320  set=0x067D5E9C
  rootTarget  get=0x02B2ECC0  set=0x053908C0
  tipTarget  get=0x0385B100  set=0x05392C40
  curve  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x0B15796C  token=0x6000209  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B157A54  token=0x600020A  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistChainConstraint
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x48
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B157FB8  token=0x600020B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistCorrectionData
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0xA8
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.ITwistCorrectionData
FIELDS:
  private           UnityEngine.Transform           m_Source  // 0x10
  private           UnityEngine.Animations.Rigging.TwistCorrectionData.Axism_TwistAxis  // 0x18
  private           UnityEngine.Animations.Rigging.WeightedTransformArraym_TwistNodes  // 0x20
PROPERTIES:
  sourceObject  get=0x02B76770  set=0x05392320
  twistNodes  get=0x04D97660  set=0x0B1581A8
  twistAxis  get=0x04D88B10  set=0x04D88B40
  UnityEngine.Animations.Rigging.ITwistCorrectionData.source  get=0x02B76770
  UnityEngine.Animations.Rigging.ITwistCorrectionData.twistAxis  get=0x0B158144
  UnityEngine.Animations.Rigging.ITwistCorrectionData.twistNodesProperty  get=0x0B158174
METHODS:
  RVA=0x0ABD4DF4  token=0x6000215  UnityEngine.Vector3 Convert(UnityEngine.Animations.Rigging.TwistCorrectionData.Axis axis)
  RVA=0x0B15800C  token=0x6000216  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B1580E4  token=0x6000217  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwistCorrection
TYPE:  class
TOKEN: 0x200005C
SIZE:  0xB8
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B1585C8  token=0x6000218  System.Void OnValidate()
  RVA=0x0B15870C  token=0x6000219  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwoBoneIKConstraintData
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x48
IMPLEMENTS: UnityEngine.Animations.Rigging.IAnimationJobData UnityEngine.Animations.Rigging.ITwoBoneIKConstraintData
FIELDS:
  private           UnityEngine.Transform           m_Root  // 0x10
  private           UnityEngine.Transform           m_Mid  // 0x18
  private           UnityEngine.Transform           m_Tip  // 0x20
  private           UnityEngine.Transform           m_Target  // 0x28
  private           UnityEngine.Transform           m_Hint  // 0x30
  private           System.Single                   m_TargetPositionWeight  // 0x38
  private           System.Single                   m_TargetRotationWeight  // 0x3c
  private           System.Single                   m_HintWeight  // 0x40
  private           System.Boolean                  m_MaintainTargetPositionOffset  // 0x44
  private           System.Boolean                  m_MaintainTargetRotationOffset  // 0x45
PROPERTIES:
  root  get=0x02B76770  set=0x05392320
  mid  get=0x04D88320  set=0x067D5E9C
  tip  get=0x02B2ECC0  set=0x053908C0
  target  get=0x0385B100  set=0x05392C40
  hint  get=0x04D862C0  set=0x05390F40
  targetPositionWeight  get=0x04D86BF0  set=0x0B151074
  targetRotationWeight  get=0x04D86AF0  set=0x0B15108C
  hintWeight  get=0x04D86E20  set=0x0B158954
  maintainTargetPositionOffset  get=0x04D86A90  set=0x04D86AA0
  maintainTargetRotationOffset  get=0x04D86D30  set=0x04D86D40
  UnityEngine.Animations.Rigging.ITwoBoneIKConstraintData.targetPositionWeightFloatProperty  get=0x0B1588EC
  UnityEngine.Animations.Rigging.ITwoBoneIKConstraintData.targetRotationWeightFloatProperty  get=0x0B158920
  UnityEngine.Animations.Rigging.ITwoBoneIKConstraintData.hintWeightFloatProperty  get=0x0B1588B8
METHODS:
  RVA=0x0B158760  token=0x6000231  System.Boolean UnityEngine.Animations.Rigging.IAnimationJobData.IsValid()
  RVA=0x0B158854  token=0x6000232  System.Void UnityEngine.Animations.Rigging.IAnimationJobData.SetDefaultValues()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.TwoBoneIKConstraint
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x58
EXTENDS: UnityEngine.Animations.Rigging.RigConstraint`3
FIELDS:
METHODS:
  RVA=0x0B158D0C  token=0x6000233  System.Void OnValidate()
  RVA=0x0B158D84  token=0x6000234  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IRigConstraint
TYPE:  interface
TOKEN: 0x200005F
FIELDS:
PROPERTIES:
  data  get=-1  // abstract
  binder  get=-1  // abstract
  component  get=-1  // abstract
  weight  get=-1  // abstract  set=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000235  System.Boolean IsValid()
  RVA=-1  // abstract  token=0x6000236  UnityEngine.Animations.IAnimationJob CreateJob(UnityEngine.Animator animator)
  RVA=-1  // abstract  token=0x6000237  System.Void UpdateJob(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // abstract  token=0x6000238  System.Void DestroyJob(UnityEngine.Animations.IAnimationJob job)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IRigLayer
TYPE:  interface
TOKEN: 0x2000060
FIELDS:
PROPERTIES:
  rig  get=-1  // abstract
  constraints  get=-1  // abstract
  jobs  get=-1  // abstract
  active  get=-1  // abstract
  name  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000243  System.Boolean Initialize(UnityEngine.Animator animator)
  RVA=-1  // abstract  token=0x6000244  System.Void Update()
  RVA=-1  // abstract  token=0x6000245  System.Void Reset()
  RVA=-1  // abstract  token=0x6000246  System.Boolean IsValid()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideRigConstraint`4
TYPE:  class
TOKEN: 0x2000061
IMPLEMENTS: UnityEngine.Animations.Rigging.IRigConstraint
FIELDS:
  protected         TConstraint                     m_Constraint  // 0x0
  private   static readonly TBinder                         s_Binder  // static @ 0x0
PROPERTIES:
  UnityEngine.Animations.Rigging.IRigConstraint.binder  get=-1  // not resolved
  UnityEngine.Animations.Rigging.IRigConstraint.data  get=-1  // not resolved
  UnityEngine.Animations.Rigging.IRigConstraint.component  get=-1  // not resolved
  weight  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000247  System.Void .ctor(TConstraint baseConstraint)
  RVA=-1  // not resolved  token=0x6000248  UnityEngine.Animations.IAnimationJob CreateJob(UnityEngine.Animator animator)
  RVA=-1  // not resolved  token=0x6000249  System.Void DestroyJob(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x600024A  System.Void UpdateJob(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x600024B  System.Boolean IsValid()
  RVA=-1  // not resolved  token=0x6000251  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.OverrideRigLayer
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x38
IMPLEMENTS: UnityEngine.Animations.Rigging.IRigLayer
FIELDS:
  private           UnityEngine.Animations.Rigging.Rigm_Rig  // 0x10
  private           System.Boolean                  m_Active  // 0x18
  private           UnityEngine.Animations.Rigging.IRigConstraint[]m_Constraints  // 0x20
  private           UnityEngine.Animations.IAnimationJob[]m_Jobs  // 0x28
  private           System.Boolean                  <isInitialized>k__BackingField  // 0x30
PROPERTIES:
  rig  get=0x02B2ECC0  set=0x053908C0
  active  get=0x04D86340  set=0x04D86350
  name  get=0x0B150E54
  constraints  get=0x0B150E34
  jobs  get=0x0B150E44
  isInitialized  get=0x04D865F0  set=0x04D86620
METHODS:
  RVA=0x0B150E04  token=0x600025B  System.Void .ctor(UnityEngine.Animations.Rigging.Rig rig, UnityEngine.Animations.Rigging.IRigConstraint[] constraints, System.Boolean active)
  RVA=0x0B150B68  token=0x600025C  System.Boolean Initialize(UnityEngine.Animator animator)
  RVA=0x0B150D98  token=0x600025D  System.Void Update()
  RVA=0x0B150CF4  token=0x600025E  System.Void Reset()
  RVA=0x0B150C98  token=0x600025F  System.Boolean IsValid()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Rig
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Animations.Rigging.IRigEffectorHolder
FIELDS:
  private           System.Single                   m_Weight  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Animations.Rigging.RigEffectorData>m_Effectors  // 0x20
PROPERTIES:
  weight  get=0x04D86380  set=0x090D4BA8
METHODS:
  RVA=0x0B1571A0  token=0x6000262  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigBuilder
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Animations.IAnimationWindowPreview UnityEngine.Animations.Rigging.IRigEffectorHolder
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.Animations.Rigging.RigLayer>m_RigLayers  // 0x18
  private           UnityEngine.Animations.Rigging.IRigLayer[]m_RuntimeRigLayers  // 0x20
  private           UnityEngine.Animations.Rigging.SyncSceneToStreamLayerm_SyncSceneToStreamLayer  // 0x28
  private           System.Collections.Generic.List<UnityEngine.Animations.Rigging.RigEffectorData>m_Effectors  // 0x30
  public    static  UnityEngine.Animations.Rigging.RigBuilder.OnAddRigBuilderCallbackonAddRigBuilder  // static @ 0x0
  public    static  UnityEngine.Animations.Rigging.RigBuilder.OnRemoveRigBuilderCallbackonRemoveRigBuilder  // static @ 0x8
  private           UnityEngine.Playables.PlayableGraph<graph>k__BackingField  // 0x38
PROPERTIES:
  layers  get=0x0B154304  set=0x05392C40
  syncSceneToStreamLayer  get=0x0B154388  set=0x02FDB880
  graph  get=0x04D8FD20  set=0x04D91AB0
METHODS:
  RVA=0x0B153F4C  token=0x6000263  System.Void OnEnable()
  RVA=0x0B153EE4  token=0x6000264  System.Void OnDisable()
  RVA=0x0B153EDC  token=0x6000265  System.Void OnDestroy()
  RVA=0x0B154190  token=0x6000266  System.Void Update()
  RVA=0x0B153C7C  token=0x6000267  System.Boolean Build()
  RVA=0x0B153DF4  token=0x6000268  System.Void Clear()
  RVA=0x0B153FB0  token=0x6000269  System.Void StartPreview()
  RVA=0x0B1540B0  token=0x600026A  System.Void StopPreview()
  RVA=0x0B1540E4  token=0x600026B  System.Void UpdatePreviewGraph(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x0B1539C0  token=0x600026C  UnityEngine.Playables.Playable BuildPreviewGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable inputPlayable)
  RVA=0x0B154284  token=0x6000273  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigBuilderUtils
TYPE:  static class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
  private   static readonly System.UInt16                   k_AnimationOutputPriority  // static @ 0x0
METHODS:
  RVA=0x0B153760  token=0x600027C  UnityEngine.Playables.Playable[] BuildRigPlayables(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.Rigging.IRigLayer layer)
  RVA=0x0B153148  token=0x600027D  System.Collections.Generic.IEnumerable<UnityEngine.Animations.Rigging.RigBuilderUtils.PlayableChain> BuildPlayables(UnityEngine.Animator animator, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers, UnityEngine.Animations.Rigging.SyncSceneToStreamLayer syncSceneToStreamLayer)
  RVA=0x0B152D78  token=0x600027E  UnityEngine.Playables.PlayableGraph BuildPlayableGraph(UnityEngine.Animator animator, System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers, UnityEngine.Animations.Rigging.SyncSceneToStreamLayer syncSceneToStreamLayer)
  RVA=0x0B153988  token=0x600027F  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigConstraint`3
TYPE:  class
TOKEN: 0x2000069
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Animations.Rigging.IRigConstraint
FIELDS:
  protected         System.Single                   m_Weight  // 0x0
  protected         TData                           m_Data  // 0x0
  private   static readonly TBinder                         s_Binder  // static @ 0x0
PROPERTIES:
  data  get=-1  // not resolved
  weight  get=-1  // not resolved  set=-1  // not resolved
  UnityEngine.Animations.Rigging.IRigConstraint.binder  get=-1  // not resolved
  UnityEngine.Animations.Rigging.IRigConstraint.data  get=-1  // not resolved
  UnityEngine.Animations.Rigging.IRigConstraint.component  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000281  System.Void Reset()
  RVA=-1  // not resolved  token=0x6000282  System.Boolean IsValid()
  RVA=-1  // not resolved  token=0x6000283  System.Void OnValidate()
  RVA=-1  // not resolved  token=0x6000287  UnityEngine.Animations.IAnimationJob CreateJob(UnityEngine.Animator animator)
  RVA=-1  // not resolved  token=0x6000288  System.Void DestroyJob(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x6000289  System.Void UpdateJob(UnityEngine.Animations.IAnimationJob job)
  RVA=-1  // not resolved  token=0x600028D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600028E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigLayer
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x38
IMPLEMENTS: UnityEngine.Animations.Rigging.IRigLayer
FIELDS:
  private           UnityEngine.Animations.Rigging.Rigm_Rig  // 0x10
  private           System.Boolean                  m_Active  // 0x18
  private           UnityEngine.Animations.Rigging.IRigConstraint[]m_Constraints  // 0x20
  private           UnityEngine.Animations.IAnimationJob[]m_Jobs  // 0x28
  private           System.Boolean                  <isInitialized>k__BackingField  // 0x30
PROPERTIES:
  rig  get=0x02B2ECC0  set=0x053908C0
  active  get=0x04D86340  set=0x04D86350
  name  get=0x0B154654
  constraints  get=0x0B150E34
  jobs  get=0x0B150E44
  isInitialized  get=0x04D865F0  set=0x04D86620
METHODS:
  RVA=0x0A8D8D40  token=0x6000298  System.Void .ctor(UnityEngine.Animations.Rigging.Rig rig, System.Boolean active)
  RVA=0x0B154418  token=0x6000299  System.Boolean Initialize(UnityEngine.Animator animator)
  RVA=0x0B1545D0  token=0x600029A  System.Void Update()
  RVA=0x0B154548  token=0x600029B  System.Void Reset()
  RVA=0x0B154500  token=0x600029C  System.Boolean IsValid()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigTransform
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x600029D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigUtils
TYPE:  static class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,UnityEngine.Animations.Rigging.PropertyDescriptor>s_SupportedPropertyTypeToDescriptor  // static @ 0x0
  private   static readonly UnityEngine.Animations.Rigging.IAnimationJobBinder<syncSceneToStreamBinder>k__BackingField  // static @ 0x8
PROPERTIES:
  syncSceneToStreamBinder  get=0x0B157150
METHODS:
  RVA=0x0B156BA4  token=0x600029E  UnityEngine.Animations.Rigging.IRigConstraint[] GetConstraints(UnityEngine.Animations.Rigging.Rig rig)
  RVA=0x0B156CD8  token=0x600029F  UnityEngine.Transform[] GetSyncableRigTransforms(UnityEngine.Animator animator)
  RVA=0x0B1560D0  token=0x60002A0  System.Boolean ExtractTransformType(UnityEngine.Animator animator, System.Reflection.FieldInfo field, System.Object data, System.Collections.Generic.List<UnityEngine.Transform> syncableTransforms)
  RVA=0x0B155F90  token=0x60002A1  System.Boolean ExtractPropertyType(System.Reflection.FieldInfo field, System.Object data, System.Collections.Generic.List<UnityEngine.Animations.Rigging.Property> syncableProperties, System.String namePrefix)
  RVA=0x0B156508  token=0x60002A2  System.Boolean ExtractWeightedTransforms(UnityEngine.Animator animator, System.Reflection.FieldInfo field, System.Object data, System.Collections.Generic.List<UnityEngine.Transform> syncableTransforms, System.Collections.Generic.List<UnityEngine.Animations.Rigging.Property> syncableProperties)
  RVA=0x0B155980  token=0x60002A3  System.Boolean ExtractNestedPropertyType(UnityEngine.Animator animator, System.Reflection.FieldInfo field, System.Object data, System.Collections.Generic.List<UnityEngine.Transform> syncableTransforms, System.Collections.Generic.List<UnityEngine.Animations.Rigging.Property> syncableProperties, System.String namePrefix)
  RVA=0x0B154E0C  token=0x60002A4  System.Void ExtractAllSyncableData(UnityEngine.Animator animator, System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers, System.Collections.Generic.List<UnityEngine.Transform>& syncableTransforms, System.Collections.Generic.List<UnityEngine.Animations.Rigging.SyncableProperties>& syncableProperties)
  RVA=0x0B154B9C  token=0x60002A5  UnityEngine.Animations.IAnimationJob[] CreateAnimationJobs(UnityEngine.Animator animator, UnityEngine.Animations.Rigging.IRigConstraint[] constraints)
  RVA=0x0B154D9C  token=0x60002A6  System.Void DestroyAnimationJobs(UnityEngine.Animations.Rigging.IRigConstraint[] constraints, UnityEngine.Animations.IAnimationJob[] jobs)
  RVA=0x0B154C68  token=0x60002A7  UnityEngine.Animations.Rigging.IAnimationJobData CreateSyncSceneToStreamData(UnityEngine.Animator animator, System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers)
  RVA=0x0B156DC4  token=0x60002A9  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.SyncSceneToStreamLayer
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x30
FIELDS:
  private           System.Boolean                  <isInitialized>k__BackingField  // 0x10
  public            UnityEngine.Animations.IAnimationJobjob  // 0x18
  private           UnityEngine.Animations.Rigging.IAnimationJobDatam_Data  // 0x20
  private           System.Collections.Generic.List<System.Int32>m_RigIndices  // 0x28
PROPERTIES:
  isInitialized  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x0B157228  token=0x60002B7  System.Boolean Initialize(UnityEngine.Animator animator, System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers)
  RVA=0x0B157498  token=0x60002B8  System.Void Update(System.Collections.Generic.IList<UnityEngine.Animations.Rigging.IRigLayer> layers)
  RVA=0x0B15740C  token=0x60002B9  System.Void Reset()
  RVA=0x0B1573F8  token=0x60002BA  System.Boolean IsValid()
  RVA=0x041E1670  token=0x60002BD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ITransformProvider
TYPE:  interface
TOKEN: 0x2000070
FIELDS:
PROPERTIES:
  transform  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IWeightProvider
TYPE:  interface
TOKEN: 0x2000071
FIELDS:
PROPERTIES:
  weight  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.WeightedTransform
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.Rigging.ITransformProvider UnityEngine.Animations.Rigging.IWeightProvider System.IEquatable`1
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.Single                   weight  // 0x18
PROPERTIES:
  UnityEngine.Animations.Rigging.ITransformProvider.transform  get=0x02B76770  set=0x05392320
  UnityEngine.Animations.Rigging.IWeightProvider.weight  get=0x04D9C280  set=0x0B15BE44
METHODS:
  RVA=0x0B15BE5C  token=0x60002C2  System.Void .ctor(UnityEngine.Transform transform, System.Single weight)
  RVA=0x0B15BDA4  token=0x60002C3  UnityEngine.Animations.Rigging.WeightedTransform Default(System.Single weight)
  RVA=0x0B15BDCC  token=0x60002C4  System.Boolean Equals(UnityEngine.Animations.Rigging.WeightedTransform other)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.WeightedTransformArray
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x98
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.IList System.Collections.ICollection
FIELDS:
  public    static readonly System.Int32                    k_MaxLength  // static @ 0x0
  private           System.Int32                    m_Length  // 0x10
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item0  // 0x18
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item1  // 0x28
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item2  // 0x38
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item3  // 0x48
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item4  // 0x58
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item5  // 0x68
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item6  // 0x78
  private           UnityEngine.Animations.Rigging.WeightedTransformm_Item7  // 0x88
PROPERTIES:
  System.Collections.IList.Item  get=0x0B15BB14  set=0x0B15BB90
  Item  get=0x0B15BCC8  set=0x0B15BD38
  Count  get=0x02B77620
  IsReadOnly  get=0x012081B0
  IsFixedSize  get=0x012081B0
  System.Collections.ICollection.IsSynchronized  get=0x02FFF600
  System.Collections.ICollection.SyncRoot  get=0x011EC580
METHODS:
  RVA=0x0B15BC54  token=0x60002C9  System.Void .ctor(System.Int32 size)
  RVA=0x0B15ACF0  token=0x60002CA  System.Collections.Generic.IEnumerator<UnityEngine.Animations.Rigging.WeightedTransform> GetEnumerator()
  RVA=0x0B15B7E8  token=0x60002CB  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B15B8A8  token=0x60002CC  System.Int32 System.Collections.IList.Add(System.Object value)
  RVA=0x0B15A7F4  token=0x60002CD  System.Void Add(UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x04D910C0  token=0x60002CE  System.Void Clear()
  RVA=0x0B15B99C  token=0x60002CF  System.Int32 System.Collections.IList.IndexOf(System.Object value)
  RVA=0x0B15AF20  token=0x60002D0  System.Int32 IndexOf(UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x0B15B924  token=0x60002D1  System.Boolean System.Collections.IList.Contains(System.Object value)
  RVA=0x0B15AA8C  token=0x60002D2  System.Boolean Contains(UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x0B15B600  token=0x60002D3  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 arrayIndex)
  RVA=0x0B15AB28  token=0x60002D4  System.Void CopyTo(UnityEngine.Animations.Rigging.WeightedTransform[] array, System.Int32 arrayIndex)
  RVA=0x0B15BA9C  token=0x60002D5  System.Void System.Collections.IList.Remove(System.Object value)
  RVA=0x0B15B2E8  token=0x60002D6  System.Boolean Remove(UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x0B15B250  token=0x60002D7  System.Void RemoveAt(System.Int32 index)
  RVA=0x0B15BA14  token=0x60002D8  System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value)
  RVA=0x0B15AFBC  token=0x60002D9  System.Void Insert(System.Int32 index, UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x0B15AA38  token=0x60002DA  System.Int32 ClampSize(System.Int32 size)
  RVA=0x0B15A928  token=0x60002DB  System.Void CheckOutOfRangeIndex(System.Int32 index)
  RVA=0x0B15AE64  token=0x60002DC  UnityEngine.Animations.Rigging.WeightedTransform Get(System.Int32 index)
  RVA=0x0B15B504  token=0x60002DD  System.Void Set(System.Int32 index, UnityEngine.Animations.Rigging.WeightedTransform value)
  RVA=0x0B15B474  token=0x60002DE  System.Void SetWeight(System.Int32 index, System.Single weight)
  RVA=0x0B15AE08  token=0x60002DF  System.Single GetWeight(System.Int32 index)
  RVA=0x0B15B3D4  token=0x60002E0  System.Void SetTransform(System.Int32 index, UnityEngine.Transform transform)
  RVA=0x0B15ADB0  token=0x60002E1  UnityEngine.Transform GetTransform(System.Int32 index)
  RVA=0x0B15B18C  token=0x60002E2  System.Void OnValidate(UnityEngine.Animations.Rigging.WeightedTransformArray& array, System.Single min, System.Single max)
  RVA=0x0B15BC18  token=0x60002EC  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ExpandChildrenAttribute
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002F3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.SyncSceneToStreamAttribute
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002F4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.PropertyType
TYPE:  sealed struct
TOKEN: 0x2000077
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.Animations.Rigging.PropertyTypeBool  // const
  public    static  UnityEngine.Animations.Rigging.PropertyTypeInt  // const
  public    static  UnityEngine.Animations.Rigging.PropertyTypeFloat  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.PropertyDescriptor
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x18
FIELDS:
  public            System.Int32                    size  // 0x10
  public            UnityEngine.Animations.Rigging.PropertyTypetype  // 0x14
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Property
TYPE:  sealed struct
TOKEN: 0x2000079
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.Animations.Rigging.PropertyDescriptordescriptor  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigProperties
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x18
FIELDS:
  public    static  System.String                   s_Weight  // static @ 0x0
  public            UnityEngine.Component           component  // 0x10
METHODS:
  RVA=0x0B1546E4  token=0x60002F5  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ConstraintProperties
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x20
FIELDS:
  public    static  System.String                   s_Weight  // static @ 0x0
  public            UnityEngine.Component           component  // 0x10
  public            UnityEngine.Animations.Rigging.Property[]properties  // 0x18
METHODS:
  RVA=0x0B14AF08  token=0x60002F6  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.PropertyUtils
TYPE:  static class
TOKEN: 0x200007C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B151E1C  token=0x60002F7  System.String ConstructConstraintDataPropertyName(System.String property)
  RVA=0x0B151E24  token=0x60002F8  System.String ConstructCustomPropertyName(UnityEngine.Component component, System.String property)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.WeightRangeAttribute
TYPE:  sealed class
TOKEN: 0x200007D
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.Single                   min  // 0x10
  public    readonly System.Single                   max  // 0x14
METHODS:
  RVA=0x04D8D120  token=0x60002F9  System.Void .ctor(System.Single min, System.Single max)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.IRigEffectorHolder
TYPE:  interface
TOKEN: 0x200007E
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.Rigging.RigEffectorData
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x58
FIELDS:
  private           UnityEngine.Transform           m_Transform  // 0x10
  private           UnityEngine.Animations.Rigging.RigEffectorData.Stylem_Style  // 0x18
  private           System.Boolean                  m_Visible  // 0x50
PROPERTIES:
  transform  get=0x02B2ECC0
  style  get=0x04D98300
  visible  get=0x04D86800  set=0x04D86840
METHODS:
  RVA=0x0B1543C8  token=0x60002FE  System.Void Initialize(UnityEngine.Transform transform, UnityEngine.Animations.Rigging.RigEffectorData.Style style)
  RVA=0x04DA9DE0  token=0x60002FF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.AffineTransform
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x2C
FIELDS:
  public            UnityEngine.Vector3             translation  // 0x10
  public            UnityEngine.Quaternion          rotation  // 0x1c
  private   static readonly UnityEngine.Animations.Rigging.AffineTransform<identity>k__BackingField  // static @ 0x0
PROPERTIES:
  identity  get=0x0B147E1C
METHODS:
  RVA=0x04D91FA0  token=0x6000300  System.Void .ctor(UnityEngine.Vector3 t, UnityEngine.Quaternion r)
  RVA=0x04D91FA0  token=0x6000301  System.Void Set(UnityEngine.Vector3 t, UnityEngine.Quaternion r)
  RVA=0x0722ACFC  token=0x6000302  UnityEngine.Vector3 Transform(UnityEngine.Vector3 p)
  RVA=0x0722AB8C  token=0x6000303  UnityEngine.Vector3 InverseTransform(UnityEngine.Vector3 p)
  RVA=0x0722AC4C  token=0x6000304  UnityEngine.Animations.Rigging.AffineTransform Inverse()
  RVA=0x0722AA64  token=0x6000305  UnityEngine.Animations.Rigging.AffineTransform InverseMul(UnityEngine.Animations.Rigging.AffineTransform transform)
  RVA=0x0722ACFC  token=0x6000306  UnityEngine.Vector3 op_Multiply(UnityEngine.Animations.Rigging.AffineTransform lhs, UnityEngine.Vector3 rhs)
  RVA=0x0B147E7C  token=0x6000307  UnityEngine.Animations.Rigging.AffineTransform op_Multiply(UnityEngine.Animations.Rigging.AffineTransform lhs, UnityEngine.Animations.Rigging.AffineTransform rhs)
  RVA=0x0722AF88  token=0x6000308  UnityEngine.Animations.Rigging.AffineTransform op_Multiply(UnityEngine.Quaternion lhs, UnityEngine.Animations.Rigging.AffineTransform rhs)
  RVA=0x0722B028  token=0x6000309  UnityEngine.Animations.Rigging.AffineTransform op_Multiply(UnityEngine.Animations.Rigging.AffineTransform lhs, UnityEngine.Quaternion rhs)
  RVA=0x0B147D90  token=0x600030B  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.AnimationRuntimeUtils
TYPE:  static class
TOKEN: 0x2000082
SIZE:  0x10
FIELDS:
  private   static  System.Single                   k_SqrEpsilon  // const
METHODS:
  RVA=0x0B148D48  token=0x600030C  System.Void SolveTwoBoneIK(UnityEngine.Animations.AnimationStream stream, UnityEngine.Animations.Rigging.ReadWriteTransformHandle root, UnityEngine.Animations.Rigging.ReadWriteTransformHandle mid, UnityEngine.Animations.Rigging.ReadWriteTransformHandle tip, UnityEngine.Animations.Rigging.ReadOnlyTransformHandle target, UnityEngine.Animations.Rigging.ReadOnlyTransformHandle hint, System.Single posWeight, System.Single rotWeight, System.Single hintWeight, UnityEngine.Animations.Rigging.AffineTransform targetOffset)
  RVA=0x0B149AEC  token=0x600030D  System.Single TriangleAngle(System.Single aLen, System.Single aLen1, System.Single aLen2)
  RVA=0x0B14889C  token=0x600030E  System.Boolean SolveFABRIK(Unity.Collections.NativeArray<UnityEngine.Vector3>& linkPositions, Unity.Collections.NativeArray<System.Single>& linkLengths, UnityEngine.Vector3 target, System.Single tolerance, System.Single maxReach, System.Int32 maxIterations)
  RVA=0x06888FD0  token=0x600030F  System.Single SqrDistance(UnityEngine.Vector3 lhs, UnityEngine.Vector3 rhs)
  RVA=0x04D886E0  token=0x6000310  System.Single Square(System.Single value)
  RVA=0x0B148570  token=0x6000311  UnityEngine.Vector3 Lerp(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 t)
  RVA=0x0B148890  token=0x6000312  System.Single Select(System.Single a, System.Single b, System.Single c)
  RVA=0x0B148834  token=0x6000313  UnityEngine.Vector3 Select(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x0B148774  token=0x6000314  UnityEngine.Vector3 ProjectOnPlane(UnityEngine.Vector3 vector, UnityEngine.Vector3 planeNormal)
  RVA=0x0B149AC4  token=0x6000315  System.Single Sum(UnityEngine.Animations.Rigging.AnimationJobCache cache, UnityEngine.Animations.Rigging.CacheIndex index, System.Int32 count)
  RVA=0x0B149A9C  token=0x6000316  System.Single Sum(Unity.Collections.NativeArray<System.Single> floatBuffer)
  RVA=0x0B148674  token=0x6000317  System.Void PassThrough(UnityEngine.Animations.AnimationStream stream, UnityEngine.Animations.Rigging.ReadWriteTransformHandle handle)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.BoneRenderer
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Animations.Rigging.BoneRenderer.BoneShapeboneShape  // 0x18
  public            System.Boolean                  drawBones  // 0x1c
  public            System.Boolean                  drawTripods  // 0x1d
  public            System.Single                   boneSize  // 0x20
  public            System.Single                   tripodSize  // 0x24
  public            UnityEngine.Color               boneColor  // 0x28
  private           UnityEngine.Transform[]         m_Transforms  // 0x38
PROPERTIES:
  transforms  get=0x04D85A50
METHODS:
  RVA=0x0B14A32C  token=0x6000319  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.ConstraintsUtils
TYPE:  static class
TOKEN: 0x2000085
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B14B0D0  token=0x600031A  UnityEngine.Transform[] ExtractChain(UnityEngine.Transform root, UnityEngine.Transform tip)
  RVA=0x0B14B220  token=0x600031B  System.Single[] ExtractLengths(UnityEngine.Transform[] chain)
  RVA=0x0B14B2EC  token=0x600031C  System.Single[] ExtractSteps(UnityEngine.Transform[] chain)
  RVA=0x0B14AF64  token=0x600031D  System.String ConstructConstraintDataPropertyName(System.String property)
  RVA=0x0B14AFA0  token=0x600031E  System.String ConstructCustomPropertyName(UnityEngine.Component component, System.String property)
END_CLASS

CLASS: UnityEngine.Animations.Rigging.QuaternionExt
TYPE:  static class
TOKEN: 0x2000087
SIZE:  0x10
FIELDS:
  private   static  System.Single                   k_FloatMin  // const
  public    static readonly UnityEngine.Quaternion          zero  // static @ 0x0
METHODS:
  RVA=0x0B151EB0  token=0x6000321  UnityEngine.Quaternion FromToRotation(UnityEngine.Vector3 from, UnityEngine.Vector3 to)
  RVA=0x0B151E2C  token=0x6000322  UnityEngine.Quaternion Add(UnityEngine.Quaternion rhs, UnityEngine.Quaternion lhs)
  RVA=0x0308B480  token=0x6000323  UnityEngine.Quaternion Scale(UnityEngine.Quaternion q, System.Single scale)
  RVA=0x0B1520A4  token=0x6000324  UnityEngine.Quaternion NormalizeSafe(UnityEngine.Quaternion q)
  RVA=0x0B152118  token=0x6000325  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.Rigging.Vector3Bool
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x13
FIELDS:
  public            System.Boolean                  x  // 0x10
  public            System.Boolean                  y  // 0x11
  public            System.Boolean                  z  // 0x12
METHODS:
  RVA=0x04DAB1B0  token=0x6000326  System.Void .ctor(System.Boolean val)
  RVA=0x04DA0780  token=0x6000327  System.Void .ctor(System.Boolean x, System.Boolean y, System.Boolean z)
END_CLASS

