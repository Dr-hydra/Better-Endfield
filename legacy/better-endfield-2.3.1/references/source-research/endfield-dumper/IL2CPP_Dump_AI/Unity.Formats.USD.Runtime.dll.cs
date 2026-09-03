// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Formats.USD.Runtime.dll
// Classes:  112
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

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x18
FIELDS:
  public            pxr.UsdVariantSets              variantSets  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000032  System.Void .ctor()
  RVA=0x0B1755C8  token=0x6000033  System.String <LoadFromUsd>b__0(System.String setName)
  RVA=0x0B1755E8  token=0x6000034  System.Collections.Generic.IEnumerable<System.String> <LoadFromUsd>b__1(System.String setName)
  RVA=0x0B175618  token=0x6000035  System.Int32 <LoadFromUsd>b__2(System.String setName)
END_CLASS

CLASS: <Import>d__5
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x90
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            USD.NET.Scene                   scene  // 0x20
  public            Unity.Formats.USD.MeshImportStrategy<>4__this  // 0x28
  public            Unity.Formats.USD.PrimMap       primMap  // 0x30
  public            Unity.Formats.USD.SceneImportOptionsimportOptions  // 0x38
  private           System.Reflection.MemberInfo    <faceVertexCounts>5__2  // 0x40
  private           System.Reflection.MemberInfo    <faceVertexIndices>5__3  // 0x48
  private           System.Reflection.MemberInfo    <orientation>5__4  // 0x50
  private           System.Reflection.MemberInfo    <points>5__5  // 0x58
  private           System.Reflection.MemberInfo    <normals>5__6  // 0x60
  private           System.Reflection.MemberInfo    <colors>5__7  // 0x68
  private           System.Reflection.MemberInfo    <purpose>5__8  // 0x70
  private           System.Reflection.MemberInfo    <visibility>5__9  // 0x78
  private           System.Boolean                  <isDynamic>5__10  // 0x80
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<Unity.Formats.USD.SanitizedMeshSample>><>7__wrap10  // 0x88
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000057  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B1754CC  token=0x6000058  System.Void System.IDisposable.Dispose()
  RVA=0x0B173ED4  token=0x6000059  System.Boolean MoveNext()
  RVA=0x0B175544  token=0x600005A  System.Void <>m__Finally1()
  RVA=0x0B175480  token=0x600005C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: GeometrySubsets
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,System.Int32[]><Subsets>k__BackingField  // 0x10
PROPERTIES:
  Subsets  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x0B1679E8  token=0x600006D  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x30
FIELDS:
  public            UnityEngine.Mesh                unityMesh  // 0x10
  public            UnityEngine.Renderer            renderer  // 0x18
  public            System.String                   path  // 0x20
  public            Unity.Formats.USD.SanitizedMeshSampleusdMesh  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600006E  System.Void .ctor()
  RVA=0x0B175594  token=0x600006F  System.Void <BuildMesh_>b__0(USD.NET.Scene scene, UnityEngine.Material boundMat, System.Collections.Generic.List<System.String> primvars)
END_CLASS

CLASS: <>c__DisplayClass5_1
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x20
FIELDS:
  public            System.Int32                    idx  // 0x10
  public            Unity.Formats.USD.MeshImporter.<>c__DisplayClass5_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000070  System.Void .ctor()
  RVA=0x0B175658  token=0x6000071  System.Void <BuildMesh_>b__1(USD.NET.Scene scene, UnityEngine.Material boundMat, System.Collections.Generic.List<System.String> primvars)
END_CLASS

CLASS: ExportHandler
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09715DA4  token=0x600008B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014DD080  token=0x600008C  System.Void Invoke(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0677C924  token=0x600008D  System.IAsyncResult BeginInvoke(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600008E  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: TextureResolver
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B18ABE4  token=0x6000096  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x6000097  UnityEngine.Texture2D Invoke(pxr.SdfAssetPath textureAssetPath, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B18AB5C  token=0x6000098  System.IAsyncResult BeginInvoke(pxr.SdfAssetPath textureAssetPath, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions importOptions, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6000099  UnityEngine.Texture2D EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: MaterialBinder
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09717B88  token=0x60000AC  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x01546280  token=0x60000AD  System.Void Invoke(USD.NET.Scene scene, UnityEngine.Material mat, System.Collections.Generic.List<System.String> uvPrimvars)
  RVA=0x05FD2238  token=0x60000AE  System.IAsyncResult BeginInvoke(USD.NET.Scene scene, UnityEngine.Material mat, System.Collections.Generic.List<System.String> uvPrimvars, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60000AF  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ConversionType
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeNone  // const
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeSwapRASmoothnessToBGRoughness  // const
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeInvertAlpha  // const
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeUnpackNormal  // const
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeMaskMapToORM  // const
  public    static  Unity.Formats.USD.ShaderExporterBase.ConversionTypeRemoveAlpha  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass34_0
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  public            System.String                   localName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000BC  System.Void .ctor()
  RVA=0x0B191A88  token=0x60000BD  System.Boolean <MergePrimvars>b__0(System.String str)
END_CLASS

CLASS: StandardShaderBlendMode
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.StandardShaderExporter.StandardShaderBlendModeOpaque  // const
  public    static  Unity.Formats.USD.StandardShaderExporter.StandardShaderBlendModeCutout  // const
  public    static  Unity.Formats.USD.StandardShaderExporter.StandardShaderBlendModeFade  // const
  public    static  Unity.Formats.USD.StandardShaderExporter.StandardShaderBlendModeTransparent  // const
METHODS:
END_CLASS

CLASS: HierInfo
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x48
FIELDS:
  public            System.Boolean                  isVisible  // 0x10
  public            System.Boolean                  isInstance  // 0x11
  public            System.Boolean                  isAssembly  // 0x12
  public            System.Boolean                  isModel  // 0x13
  public            System.Boolean                  hasPayload  // 0x14
  public            pxr.UsdPrim                     prim  // 0x18
  public            pxr.UsdSkelBindingVector        skelBindings  // 0x20
  public            pxr.SdfPath[]                   skelJoints  // 0x28
  public            System.String                   modelAssetPath  // 0x30
  public            System.String                   modelName  // 0x38
  public            System.String                   modelVersion  // 0x40
METHODS:
END_CLASS

CLASS: ReadHierJob
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x11
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public    static  Unity.Formats.USD.HierarchyBuilder.HierInfo[]result  // static @ 0x0
  public    static  USD.NET.Scene                   scene  // static @ 0x8
  public    static  pxr.UsdSkelCache                skelCache  // static @ 0x10
  public    static  pxr.SdfPath[]                   paths  // static @ 0x18
METHODS:
  RVA=0x0B180698  token=0x6000106  System.Void Run()
  RVA=0x0B1804AC  token=0x6000107  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: IQuery
TYPE:  interface
TOKEN: 0x2000042
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600010A  pxr.SdfPath[] Find(USD.NET.Scene scene, pxr.SdfPath usdRoot)
END_CLASS

CLASS: Query`1
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x11
IMPLEMENTS: IQuery
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600010B  pxr.SdfPath[] Find(USD.NET.Scene scene, pxr.SdfPath usdRoot)
END_CLASS

CLASS: FindPathsJob
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x11
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public    static  pxr.SdfPath                     usdRoot  // static @ 0x0
  public    static  USD.NET.Scene                   scene  // static @ 0x8
  public    static  pxr.SdfPath[][]                 results  // static @ 0x10
  public    static  Unity.Formats.USD.HierarchyBuilder.FindPathsJob.IQuery[]queries  // static @ 0x18
METHODS:
  RVA=0x0B17AE58  token=0x6000108  System.Void Run()
  RVA=0x0B17AD50  token=0x6000109  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000044
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Formats.USD.HierarchyBuilder.<>c<>9  // static @ 0x0
  public    static  System.Func<pxr.SdfPath[],System.Boolean><>9__4_0  // static @ 0x8
  public    static  System.Func<pxr.SdfPath[],System.Collections.Generic.IEnumerable<pxr.SdfPath>><>9__4_1  // static @ 0x10
METHODS:
  RVA=0x0B191A9C  token=0x600010C  System.Void .cctor()
  RVA=0x041E1670  token=0x600010D  System.Void .ctor()
  RVA=0x0721682C  token=0x600010E  System.Boolean <BeginReading>b__4_0(pxr.SdfPath[] i)
  RVA=0x04D88C30  token=0x600010F  System.Collections.Generic.IEnumerable<pxr.SdfPath> <BeginReading>b__4_1(pxr.SdfPath[] i)
END_CLASS

CLASS: InstanceRoot
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x20
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            pxr.SdfPath                     masterPath  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Formats.USD.SceneExporter.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Transform,System.String><>9__8_0  // static @ 0x8
  public    static  System.Func<System.String,System.String><>9__8_1  // static @ 0x10
METHODS:
  RVA=0x0B191B00  token=0x6000152  System.Void .cctor()
  RVA=0x041E1670  token=0x6000153  System.Void .ctor()
  RVA=0x0B191A3C  token=0x6000154  System.String <MergeBonesBelowAnimator>b__8_0(UnityEngine.Transform boneXf)
  RVA=0x04D88C30  token=0x6000155  System.String <MergeBonesBelowAnimator>b__8_1(System.String str)
END_CLASS

CLASS: ImportException
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B17E7A8  token=0x6000162  System.Void .ctor()
  RVA=0x0B17E854  token=0x6000163  System.Void .ctor(System.String message)
  RVA=0x0B17E7F4  token=0x6000164  System.Void .ctor(System.String message, System.Exception innerException)
END_CLASS

CLASS: <BuildScene>d__8
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x110
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Unity.Formats.USD.SceneImportOptionsimportOptions  // 0x20
  public            System.Single                   targetFrameMilliseconds  // 0x28
  public            UnityEngine.GameObject          root  // 0x30
  public            Unity.Formats.USD.PrimMap       primMap  // 0x38
  public            USD.NET.Scene                   scene  // 0x40
  public            System.Boolean                  composingSubtree  // 0x48
  private           System.Diagnostics.Stopwatch    <timer>5__2  // 0x50
  private           pxr.SdfPath                     <usdPrimRoot>5__3  // 0x58
  private           System.Single                   <targetTime>5__4  // 0x60
  private           System.Collections.Generic.List<pxr.UsdSkelRoot><skelRoots>5__5  // 0x68
  private           Unity.Formats.USD.ReadAllJob<Unity.Formats.USD.SanitizedXformSample><readXforms>5__6  // 0x70
  private           pxr.SdfPath[]                   <>7__wrap6  // 0x78
  private           System.Int32                    <>7__wrap7  // 0x80
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<USD.NET.Unity.MaterialSample>><>7__wrap8  // 0x88
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<Unity.Formats.USD.SanitizedXformSample>><>7__wrap9  // 0x90
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<USD.NET.Unity.XformSample>><>7__wrap10  // 0x98
  private           System.Collections.IEnumerator  <it>5__12  // 0xa0
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<USD.NET.Unity.CubeSample>><>7__wrap12  // 0xa8
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<USD.NET.Unity.SphereSample>><>7__wrap13  // 0xb0
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<Unity.Formats.USD.SanitizedCameraSample>><>7__wrap14  // 0xb8
  private           System.Collections.Generic.Dictionary.KeyCollection.Enumerator<pxr.SdfPath,UnityEngine.GameObject><>7__wrap15  // 0xc0
  private           System.Collections.Generic.Dictionary<pxr.SdfPath,Unity.Formats.USD.SkeletonSample><skeletonSamples>5__17  // 0xd8
  private           System.Collections.Generic.Dictionary.Enumerator<pxr.SdfPath,Unity.Formats.USD.SkeletonSample><>7__wrap17  // 0xe0
  private           System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<USD.NET.Unity.PointInstancerSample>><>7__wrap18  // 0x108
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000165  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B191278  token=0x6000166  System.Void System.IDisposable.Dispose()
  RVA=0x0B18AD0C  token=0x6000167  System.Boolean MoveNext()
  RVA=0x0B191794  token=0x6000168  System.Void <>m__Finally1()
  RVA=0x0B1917E4  token=0x6000169  System.Void <>m__Finally2()
  RVA=0x0B191834  token=0x600016A  System.Void <>m__Finally3()
  RVA=0x0B191884  token=0x600016B  System.Void <>m__Finally4()
  RVA=0x0B1918D4  token=0x600016C  System.Void <>m__Finally5()
  RVA=0x0B191924  token=0x600016D  System.Void <>m__Finally6()
  RVA=0x0B191974  token=0x600016E  System.Void <>m__Finally7()
  RVA=0x0B1919C4  token=0x600016F  System.Void <>m__Finally8()
  RVA=0x0B191A00  token=0x6000170  System.Void <>m__Finally9()
  RVA=0x0B191744  token=0x6000171  System.Void <>m__Finally10()
  RVA=0x0B19122C  token=0x6000173  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass17_0
TYPE:  sealed class
TOKEN: 0x200006C
SIZE:  0x70
FIELDS:
  public            Unity.Formats.USD.UsdRecorderBehaviour<>4__this  // 0x10
  public            UnityEngine.Playables.Playable  playable  // 0x18
  public            UnityEngine.Playables.FrameData info  // 0x28
  public            System.Object                   playerData  // 0x68
METHODS:
  RVA=0x041E1670  token=0x60001AC  System.Void .ctor()
  RVA=0x0B195350  token=0x60001AD  System.Void <ProcessFrame>b__0()
END_CLASS

CLASS: <WaitForEndOfFrame>d__4
TYPE:  sealed class
TOKEN: 0x2000070
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Unity.Formats.USD.UsdWaitForEndOfFrame<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001BE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001BF  System.Void System.IDisposable.Dispose()
  RVA=0x0B1953B8  token=0x60001C0  System.Boolean MoveNext()
  RVA=0x0B19563C  token=0x60001C2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000002  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdAssemblyRoot
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdAsset
TYPE:  class
TOKEN: 0x2000009
SIZE:  0xD0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.String                   m_usdFile  // 0x18
  public            System.String                   m_projectAssetPath  // 0x20
  public            System.String                   m_usdRootPath  // 0x28
  public            System.Single                   m_usdTimeOffset  // 0x30
  public            Unity.Formats.USD.PayloadPolicy m_payloadPolicy  // 0x34
  public            System.Boolean                  m_importHierarchy  // 0x38
  public            System.Single                   m_scale  // 0x3c
  public            Unity.Formats.USD.BasisTransformationm_changeHandedness  // 0x40
  public            USD.NET.Scene.InterpolationMode m_interpolation  // 0x44
  public            System.Boolean                  m_useOriginalShaderIfAvailable  // 0x48
  public            UnityEngine.Material            m_displayColorMaterial  // 0x50
  public            UnityEngine.Material            m_specularWorkflowMaterial  // 0x58
  public            UnityEngine.Material            m_metallicWorkflowMaterial  // 0x60
  public            System.Boolean                  m_enableGpuInstancing  // 0x68
  public            Unity.Formats.USD.ImportMode    m_points  // 0x6c
  public            Unity.Formats.USD.ImportMode    m_topology  // 0x70
  public            Unity.Formats.USD.ImportMode    m_boundingBox  // 0x74
  public            Unity.Formats.USD.ImportMode    m_color  // 0x78
  public            Unity.Formats.USD.ImportMode    m_normals  // 0x7c
  public            Unity.Formats.USD.ImportMode    m_tangents  // 0x80
  public            System.Boolean                  m_generateLightmapUVs  // 0x84
  public            System.Single                   m_unwrapAngleError  // 0x88
  public            System.Single                   m_unwrapAreaError  // 0x8c
  public            System.Single                   m_unwrapHardAngle  // 0x90
  public            System.Int32                    m_unwrapPackMargin  // 0x94
  public            Unity.Formats.USD.MaterialImportModem_materialImportMode  // 0x98
  public            System.Boolean                  m_importCameras  // 0x9c
  public            System.Boolean                  m_importMeshes  // 0x9d
  public            System.Boolean                  m_importSkinning  // 0x9e
  public            System.Boolean                  m_importTransforms  // 0x9f
  public            System.Boolean                  m_importSceneInstances  // 0xa0
  public            System.Boolean                  m_importPointInstances  // 0xa1
  public            System.Boolean                  m_importMonoBehaviors  // 0xa2
  public            System.Boolean                  m_debugShowSkeletonBindPose  // 0xa3
  public            System.Boolean                  m_debugShowSkeletonRestPose  // 0xa4
  public            System.Boolean                  m_debugPrintVariabilityCache  // 0xa5
  public            System.Boolean                  m_usdVariabilityCache  // 0xa6
  public            Unity.Formats.USD.BasisTransformationLastHandedness  // 0xa8
  public            System.Single                   LastScale  // 0xac
  private           System.Single                   m_lastTime  // 0xb0
  private           USD.NET.Scene                   m_lastScene  // 0xb8
  private           Unity.Formats.USD.PrimMap       m_lastPrimMap  // 0xc0
  private           USD.NET.AccessMask              m_lastAccessMask  // 0xc8
PROPERTIES:
  Length  get=0x0B17882C
  usdFullPath  get=0x0B178834  set=0x05392C40
METHODS:
  RVA=0x0B1766F0  token=0x600000B  System.Void OnDestroy()
  RVA=0x011EC580  token=0x600000C  System.String GetPrefabAssetPath(UnityEngine.GameObject root)
  RVA=0x0B176798  token=0x600000D  System.Void OptionsToState(Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B178338  token=0x600000E  System.Void StateToOptions(Unity.Formats.USD.SceneImportOptions& options)
  RVA=0x0B176DDC  token=0x600000F  System.Boolean SceneFileChanged()
  RVA=0x0B17620C  token=0x6000010  USD.NET.Scene GetScene()
  RVA=0x0B176734  token=0x6000011  System.Void OnReload()
  RVA=0x0B175BBC  token=0x6000012  System.Void DestroyComponent(UnityEngine.Component comp)
  RVA=0x0B1759EC  token=0x6000013  System.Void ClearLastData()
  RVA=0x0B176BE0  token=0x6000014  System.Void RemoveAllUsdComponents()
  RVA=0x0B175A98  token=0x6000015  System.Void DestroyAllImportedObjects()
  RVA=0x0B176A58  token=0x6000016  System.Void Reload(System.Boolean forceRebuild)
  RVA=0x0B176170  token=0x6000017  pxr.UsdPrim GetFirstPrim(USD.NET.Scene scene)
  RVA=0x0B175C1C  token=0x6000018  System.Void ExportOverrides(USD.NET.Scene sceneInWhichToStoreTransforms)
  RVA=0x0B175A34  token=0x6000019  System.Double ComputeLength()
  RVA=0x0B1774C0  token=0x600001A  System.Void SetTime(System.Double time, Unity.Formats.USD.UsdAsset foreignRoot, System.Boolean saveMeshUpdates)
  RVA=0x0B178730  token=0x600001B  System.Void Update()
  RVA=0x0B1769D4  token=0x600001C  System.Void PrepOptionsForTimeChange(Unity.Formats.USD.SceneImportOptions& options)
  RVA=0x0B176520  token=0x600001D  System.Void ImportUsdAsCoroutine(UnityEngine.GameObject goRoot, System.String usdFilePath, System.Double time, Unity.Formats.USD.SceneImportOptions importOptions, System.Single targetFrameMilliseconds)
  RVA=0x0B176EDC  token=0x600001E  System.Void SetPayloadState(UnityEngine.GameObject go, System.Boolean isLoaded)
  RVA=0x0B1756A8  token=0x600001F  System.Void ApplyVariantSelectionState(USD.NET.Scene scene, Unity.Formats.USD.UsdVariantSet variants)
  RVA=0x0B177C80  token=0x6000020  System.Void SetVariantSelection(UnityEngine.GameObject go, System.String usdPrimPath, System.Collections.Generic.Dictionary<System.String,System.String> selections)
  RVA=0x0B178770  token=0x6000021  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdLayerStack
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   m_targetLayer  // 0x18
  public            System.String[]                 m_layerStack  // 0x20
  public            System.String[]                 m_mutedLayers  // 0x28
METHODS:
  RVA=0x0B178D90  token=0x6000022  System.Void SetupNewSubLayer(USD.NET.Scene parentScene, USD.NET.Scene subLayerScene)
  RVA=0x0B1788AC  token=0x6000023  System.Void SaveLayerStack(USD.NET.Scene scene, System.String[] layerStack)
  RVA=0x0B178A58  token=0x6000024  System.Void SaveToLayer()
  RVA=0x05393520  token=0x6000025  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdModelRoot
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   m_modelName  // 0x18
  public            System.String                   m_modelVersion  // 0x20
  public            System.String                   m_modelAssetPath  // 0x28
METHODS:
  RVA=0x05393520  token=0x6000026  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdPayload
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  m_isLoaded  // 0x18
  private           System.Boolean                  m_wasLoaded  // 0x19
PROPERTIES:
  IsLoaded  get=0x04D86340
METHODS:
  RVA=0x04D8EB60  token=0x6000028  System.Void Load()
  RVA=0x04D91AA0  token=0x6000029  System.Void Unload()
  RVA=0x04DAB6F0  token=0x600002A  System.Void SetInitialState(System.Boolean loaded)
  RVA=0x0B178E20  token=0x600002B  System.Void Update()
  RVA=0x0B178EAC  token=0x600002C  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdPrimSource
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   m_usdPrimPath  // 0x18
METHODS:
  RVA=0x05393520  token=0x600002D  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdVariantSet
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String[]                 m_variantSetNames  // 0x18
  public            System.String[]                 m_selected  // 0x20
  public            System.String[]                 m_variants  // 0x28
  public            System.Int32[]                  m_variantCounts  // 0x30
  public            System.String                   m_primPath  // 0x38
METHODS:
  RVA=0x0B179030  token=0x600002E  System.Void LoadFromUsd(pxr.UsdPrim prim)
  RVA=0x0B178EC8  token=0x600002F  System.Void ApplyVariantSelections()
  RVA=0x0B178F60  token=0x6000030  System.Collections.Generic.Dictionary<System.String,System.String> GetVariantSelections()
  RVA=0x05393520  token=0x6000031  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkinnedMeshUnityDiff
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.SkinnedMeshRenderer m_usdMesh  // 0x18
  public            UnityEngine.SkinnedMeshRenderer m_unityMesh  // 0x20
METHODS:
  RVA=0x0B17124C  token=0x6000036  System.Void OnEnable()
  RVA=0x0B171170  token=0x6000037  System.Boolean Approximately(UnityEngine.Matrix4x4 rhs, UnityEngine.Matrix4x4 lhs)
  RVA=0x0B172220  token=0x6000038  System.Boolean WeightsMatch(UnityEngine.BoneWeight w1, UnityEngine.BoneWeight w2)
  RVA=0x0B171220  token=0x6000039  System.Boolean IndicesMatch(UnityEngine.BoneWeight w1, UnityEngine.BoneWeight w2)
  RVA=0x041E1670  token=0x600003A  System.Void Update()
  RVA=0x05393520  token=0x600003B  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkinnedMeshUsdDiff
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   m_usdFile  // 0x18
  public            System.String                   m_usdMeshPath  // 0x20
  public            Unity.Formats.USD.BasisTransformationm_basisTransform  // 0x28
METHODS:
  RVA=0x0B1724C4  token=0x600003C  System.Void OnEnable()
  RVA=-1  // generic def  token=0x600003D  System.String GetUsdBoneData(System.Int32 bone, System.Int32 weightIndex, USD.NET.Primvar<T[]> primvar)
  RVA=0x0B172320  token=0x600003E  USD.NET.Scene GetScene()
  RVA=0x0B173790  token=0x600003F  Unity.Formats.USD.SkelBindingSample ReadUsdWeights(USD.NET.Scene scene)
  RVA=0x0B173374  token=0x6000040  Unity.Formats.USD.SkeletonSample ReadUsdSkeleton(USD.NET.Scene scene, System.String& skelRootPath)
  RVA=0x041E1670  token=0x6000041  System.Void Update()
  RVA=0x05393520  token=0x6000042  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.CameraExporter
TYPE:  static class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B165B8C  token=0x6000043  System.Void ExportCamera(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
END_CLASS

CLASS: Unity.Formats.USD.CameraImporter
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B165E58  token=0x6000044  System.Void BuildCamera(USD.NET.Unity.CameraSample usdCamera, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options)
END_CLASS

CLASS: Unity.Formats.USD.CubeImporter
TYPE:  static class
TOKEN: 0x2000014
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B165F14  token=0x6000045  System.Void BuildCube(USD.NET.Unity.CubeSample usdCube, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options, System.Boolean skinnedMesh)
  RVA=0x0AAA50A8  token=0x6000046  System.Boolean ShouldImport(Unity.Formats.USD.ImportMode mode)
  RVA=0x06D41C40  token=0x6000047  System.Boolean ShouldCompute(Unity.Formats.USD.ImportMode mode)
END_CLASS

CLASS: Unity.Formats.USD.InstanceImporter
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B16A330  token=0x6000048  System.Void BuildSceneInstances(Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B169C9C  token=0x6000049  System.Void BuildPointInstances(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, System.String pointInstancerPath, USD.NET.Unity.PointInstancerSample sample, UnityEngine.GameObject root, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B16A928  token=0x600004A  System.Void EnableGpuInstancing(UnityEngine.GameObject go)
END_CLASS

CLASS: Unity.Formats.USD.MeshExporter
TYPE:  static class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
  private   static  pxr.TfToken                     m_materialBindToken  // static @ 0x0
  private   static  pxr.TfToken                     m_subMeshesToken  // static @ 0x8
METHODS:
  RVA=0x0B16E8D0  token=0x600004B  System.Void ExportSkinnedMesh(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B16E378  token=0x600004C  System.Void ExportSkelWeights(USD.NET.Scene scene, System.String path, UnityEngine.Mesh unityMesh, UnityEngine.Transform rootBone, UnityEngine.Transform[] bones, System.String skeletonPath)
  RVA=0x0B16E248  token=0x600004D  System.Void ExportMesh(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B16CDCC  token=0x600004E  System.Void ExportMesh(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext, UnityEngine.Mesh mesh, UnityEngine.Material sharedMaterial, UnityEngine.Material[] sharedMaterials, System.Boolean exportMeshPose)
  RVA=0x0B16EBD0  token=0x600004F  System.Void .cctor()
END_CLASS

CLASS: Unity.Formats.USD.MeshImportFunction`1
TYPE:  sealed class
TOKEN: 0x2000017
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000050  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000051  System.Void Invoke(System.String path, T sample, Unity.Formats.USD.MeshImporter.GeometrySubsets subsets, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions option, System.Boolean isDynamic, pxr.UsdSkelSkinningQuery query)
  RVA=-1  // runtime  token=0x6000052  System.IAsyncResult BeginInvoke(System.String path, T sample, Unity.Formats.USD.MeshImporter.GeometrySubsets subsets, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions option, System.Boolean isDynamic, pxr.UsdSkelSkinningQuery query, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000053  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Unity.Formats.USD.MeshImportStrategy
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x28
IMPLEMENTS: Unity.Formats.USD.IImporter
FIELDS:
  private           Unity.Formats.USD.MeshImportFunction<Unity.Formats.USD.SanitizedMeshSample>m_meshImporter  // 0x10
  private           Unity.Formats.USD.MeshImportFunction<Unity.Formats.USD.SanitizedMeshSample>m_skinnedMeshImporter  // 0x18
  private           Unity.Formats.USD.ReadAllJob<Unity.Formats.USD.SanitizedMeshSample>m_readMeshesJob  // 0x20
METHODS:
  RVA=0x032AF420  token=0x6000054  System.Void .ctor(Unity.Formats.USD.MeshImportFunction<Unity.Formats.USD.SanitizedMeshSample> meshImporter, Unity.Formats.USD.MeshImportFunction<Unity.Formats.USD.SanitizedMeshSample> skinnedMeshImporter)
  RVA=0x0B16ECC0  token=0x6000055  System.Void BeginReading(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B16ED8C  token=0x6000056  System.Collections.IEnumerator Import(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
END_CLASS

CLASS: Unity.Formats.USD.MeshImporter
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B170D18  token=0x600005E  Unity.Formats.USD.MeshImporter.GeometrySubsets ReadGeomSubsets(USD.NET.Scene scene, System.String path)
  RVA=0x0B1700F0  token=0x600005F  System.Void BuildSkinnedMesh(System.String path, Unity.Formats.USD.SanitizedMeshSample usdMesh, Unity.Formats.USD.MeshImporter.GeometrySubsets geomSubsets, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options, System.Boolean isDynamic, pxr.UsdSkelSkinningQuery skinningQuery)
  RVA=0x0B1702B0  token=0x6000060  System.Void ImportSkinning(System.String path, Unity.Formats.USD.SanitizedMeshSample usdMesh, UnityEngine.Mesh unityMesh, pxr.UsdSkelSkinningQuery skinningQuery)
  RVA=0x0B16FF38  token=0x6000061  System.Void BuildMesh(System.String path, Unity.Formats.USD.SanitizedMeshSample usdMesh, Unity.Formats.USD.MeshImporter.GeometrySubsets geomSubsets, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options, System.Boolean isDynamic, pxr.UsdSkelSkinningQuery skinQuery)
  RVA=0x0B16EF28  token=0x6000062  System.Void BuildMesh_(System.String path, Unity.Formats.USD.SanitizedMeshSample usdMesh, UnityEngine.Mesh unityMesh, Unity.Formats.USD.MeshImporter.GeometrySubsets geomSubsets, UnityEngine.GameObject go, UnityEngine.Renderer renderer, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B170A80  token=0x6000063  System.Void LoadPrimvars(UnityEngine.Mesh unityMesh, System.String usdMeshPath, System.Collections.Generic.List<System.String> primvars, USD.NET.Unity.MeshSample sample)
  RVA=0x0B16EED8  token=0x6000064  System.Void BindMat(UnityEngine.Mesh unityMesh, UnityEngine.Material mat, UnityEngine.Renderer renderer, System.String usdMeshPath, System.Collections.Generic.List<System.String> primvars, USD.NET.Unity.MeshSample sample)
  RVA=0x0B16EE34  token=0x6000065  System.Void BindMat(UnityEngine.Mesh unityMesh, UnityEngine.Material mat, UnityEngine.Renderer renderer, System.Int32 index, System.String usdMeshPath, System.Collections.Generic.List<System.String> primvars, USD.NET.Unity.MeshSample sample)
  RVA=-1  // generic def  token=0x6000066  T[] TryGetPrimvarValue(USD.NET.Primvar<System.Object> primvar)
  RVA=0x0B1708C8  token=0x6000067  System.Void ImportUv(UnityEngine.Mesh unityMesh, System.Int32 uvSetIndex, USD.NET.Primvar<System.Object> uv)
  RVA=0x0AAA50A8  token=0x6000068  System.Boolean ShouldImport(Unity.Formats.USD.ImportMode mode)
  RVA=0x06D41C40  token=0x6000069  System.Boolean ShouldCompute(Unity.Formats.USD.ImportMode mode)
  RVA=0x0B1710C0  token=0x600006A  System.String UniqueMeshName(System.String meshName)
END_CLASS

CLASS: Unity.Formats.USD.ImportMode
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.ImportMode    Import  // const
  public    static  Unity.Formats.USD.ImportMode    ImportOrCompute  // const
  public    static  Unity.Formats.USD.ImportMode    Compute  // const
  public    static  Unity.Formats.USD.ImportMode    Ignore  // const
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.MeshImportOptions
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x40
FIELDS:
  public            System.Boolean                  debugShowSkeletonRestPose  // 0x10
  public            System.Boolean                  debugShowSkeletonBindPose  // 0x11
  public            Unity.Formats.USD.ImportMode    points  // 0x14
  public            Unity.Formats.USD.ImportMode    topology  // 0x18
  public            System.Boolean                  triangulateMesh  // 0x1c
  public            System.Boolean                  generateLightmapUVs  // 0x1d
  public            System.Single                   unwrapAngleError  // 0x20
  public            System.Single                   unwrapAreaError  // 0x24
  public            System.Single                   unwrapHardAngle  // 0x28
  public            System.Int32                    unwrapPackMargin  // 0x2c
  public            Unity.Formats.USD.ImportMode    color  // 0x30
  public            Unity.Formats.USD.ImportMode    normals  // 0x34
  public            Unity.Formats.USD.ImportMode    tangents  // 0x38
  public            Unity.Formats.USD.ImportMode    boundingBox  // 0x3c
METHODS:
  RVA=0x04DAB6B0  token=0x6000072  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SphereImporter
TYPE:  static class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1738AC  token=0x6000073  System.Void BuildSphere(USD.NET.Unity.SphereSample usdSphere, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options, System.Boolean skinnedMesh)
  RVA=0x0AAA50A8  token=0x6000074  System.Boolean ShouldImport(Unity.Formats.USD.ImportMode mode)
  RVA=0x06D41C40  token=0x6000075  System.Boolean ShouldCompute(Unity.Formats.USD.ImportMode mode)
END_CLASS

CLASS: Unity.Formats.USD.XformExporter
TYPE:  static class
TOKEN: 0x2000021
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1792B8  token=0x6000076  System.Void ExportXform(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B17981C  token=0x6000077  System.Void WriteSparseOverrides(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.BasisTransformation changeHandedness, System.Single tolerance)
  RVA=0x0B179544  token=0x6000078  UnityEngine.Matrix4x4 GetLocalTransformMatrix(UnityEngine.Transform tr, System.Boolean correctZUp, System.Boolean isRootPrim, Unity.Formats.USD.BasisTransformation conversionType)
END_CLASS

CLASS: Unity.Formats.USD.XformImporter
TYPE:  static class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B17A084  token=0x6000079  System.Void BuildXform(pxr.SdfPath path, USD.NET.Unity.XformableSample usdXf, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options, USD.NET.Scene scene)
  RVA=0x0B179EE4  token=0x600007A  System.Void BuildXform(UnityEngine.Matrix4x4 xf, UnityEngine.GameObject go, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17A204  token=0x600007B  System.Void ImportXform(UnityEngine.Matrix4x4& mat, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B179B44  token=0x600007C  System.Void BuildSceneRoot(USD.NET.Scene scene, UnityEngine.Transform root, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17A2EC  token=0x600007D  System.Void UndoRootTransform(USD.NET.Scene scene, Unity.Formats.USD.UsdAsset stageRoot, UnityEngine.Vector3& localScale, UnityEngine.Quaternion& localRotation)
END_CLASS

CLASS: Unity.Formats.USD.ImporterBase
TYPE:  static class
TOKEN: 0x2000023
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B169800  token=0x600007E  System.String MakeRelativePath(System.String anchorPath, System.String pathToMakeRelative)
  RVA=0x0B16974C  token=0x600007F  System.Boolean ApproximatelyEqual(UnityEngine.Matrix4x4 lhs, UnityEngine.Matrix4x4 rhs)
  RVA=0x0B169A84  token=0x6000080  System.Void MoveComponentFirst(UnityEngine.Component comp)
  RVA=0x0B169AD8  token=0x6000081  System.Void MoveComponentLast(UnityEngine.Component comp)
  RVA=-1  // generic def  token=0x6000082  T GetOrAddComponent(UnityEngine.GameObject go, System.Boolean insertFirst)
  RVA=0x0B169B2C  token=0x6000083  System.String SafeGetDirectoryName(System.String fullPath)
END_CLASS

CLASS: Unity.Formats.USD.HdrpShaderExporter
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x10
EXTENDS: Unity.Formats.USD.ShaderExporterBase
FIELDS:
METHODS:
  RVA=0x0B167A60  token=0x6000084  System.Void ExportLit(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, USD.NET.Unity.PreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.HdrpShaderImporter
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x100
EXTENDS: Unity.Formats.USD.ShaderImporterBase
FIELDS:
  private   static  UnityEngine.Material            ChannelCombinerMat  // static @ 0x0
METHODS:
  RVA=0x053908C0  token=0x6000086  System.Void .ctor(UnityEngine.Material material)
  RVA=0x0B169124  token=0x6000087  System.Void ImportFromUsd()
  RVA=0x0B168A18  token=0x6000088  UnityEngine.Texture2D BuildMaskMap(UnityEngine.Texture2D red, UnityEngine.Texture2D green, UnityEngine.Texture2D blue, UnityEngine.Texture2D alpha)
END_CLASS

CLASS: Unity.Formats.USD.MaterialExporter
TYPE:  static class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   kStandardFallbackHandler  // static @ 0x0
  public    static readonly System.String                   kHdrpFallbackHandler  // static @ 0x8
  public    static readonly System.String                   kLwrpFallbackHandler  // static @ 0x10
  public    static  System.Collections.Generic.Dictionary<System.String,Unity.Formats.USD.MaterialExporter.ExportHandler>ExportRegistry  // static @ 0x18
METHODS:
  RVA=0x0B16B0A0  token=0x6000089  System.Void .cctor()
  RVA=0x0B16AB80  token=0x600008A  System.Void ExportMaterial(USD.NET.Scene scene, UnityEngine.Material mat, System.String usdMaterialPath)
END_CLASS

CLASS: Unity.Formats.USD.MaterialImporter
TYPE:  static class
TOKEN: 0x2000028
SIZE:  0x10
FIELDS:
  private   static readonly pxr.TfToken                     materialBindToken  // static @ 0x0
  private   static  UnityEngine.Material            AlbedoGlossCombiner  // static @ 0x8
  public    static  Unity.Formats.USD.MaterialImporter.TextureResolverOnResolveTexture  // static @ 0x10
METHODS:
  RVA=0x0B16C828  token=0x600008F  System.Void ProcessMaterialBindings(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B16B748  token=0x6000090  UnityEngine.Material BuildMaterial(USD.NET.Scene scene, System.String materialPath, USD.NET.Unity.MaterialSample sample, Unity.Formats.USD.SceneImportOptions options)
  RVA=-1  // generic def  token=0x6000091  UnityEngine.Texture2D ImportConnectedTexture(USD.NET.Scene scene, USD.NET.Connectable<T> connection, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions options, System.String& uvPrimvar)
  RVA=0x011EC580  token=0x6000092  UnityEngine.Texture2D DefaultTextureResolver(pxr.SdfAssetPath textureAssetPath, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B16C30C  token=0x6000093  UnityEngine.Texture2D CombineRoughness(UnityEngine.Texture2D rgbTex, UnityEngine.Texture2D roughnessTex, System.String fileNameSuffix)
  RVA=0x0B16C60C  token=0x6000094  Unity.Formats.USD.UnityPreviewSurfaceSample GetSurfaceShaderPrim(USD.NET.Scene scene, System.String primPath)
  RVA=0x0B16CD3C  token=0x6000095  System.Void .cctor()
END_CLASS

CLASS: Unity.Formats.USD.MaterialMap
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Material>m_map  // 0x10
  private           System.Collections.Generic.Dictionary<UnityEngine.Color,UnityEngine.Material>m_colorMap  // 0x18
  private           System.Collections.Generic.Dictionary<System.String,Unity.Formats.USD.MaterialMap.MaterialBinder>m_requestedBindings  // 0x20
  private           System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.String>>m_primvars  // 0x28
  public            System.Boolean                  useOriginalShaderIfAvailable  // 0x30
  private           UnityEngine.Material            m_displayColorMaterial  // 0x38
  private           UnityEngine.Material            m_specularWorkflowMaterial  // 0x40
  private           UnityEngine.Material            m_metallicWorkflowMaterial  // 0x48
PROPERTIES:
  DisplayColorMaterial  get=0x0B17F5F0  set=0x046A91B0
  SpecularWorkflowMaterial  get=0x0B17F724  set=0x042B4AE0
  MetallicWorkflowMaterial  get=0x0B17F6C0  set=0x035A41A0
  Item  get=0x0B17F654  set=0x0B17F788
METHODS:
  RVA=0x0B17F060  token=0x60000A0  System.Void InstantiateMaterials()
  RVA=0x0B17EF28  token=0x60000A1  System.Collections.Generic.Dictionary<System.String,Unity.Formats.USD.MaterialMap.MaterialBinder> ClearRequestedBindings()
  RVA=0x0B17F348  token=0x60000A2  System.Void RequestBinding(System.String usdPath, Unity.Formats.USD.MaterialMap.MaterialBinder binder)
  RVA=0x0B17EFF8  token=0x60000A5  System.Collections.Generic.List<System.String> GetPrimvars(System.String materialPath)
  RVA=0x0B17F3AC  token=0x60000A6  System.Void SetPrimvars(System.String materialPath, System.Collections.Generic.List<System.String> primvars)
  RVA=0x0B17F230  token=0x60000A7  UnityEngine.Material InstantiateSolidColor(UnityEngine.Color color)
  RVA=0x0B17EE74  token=0x60000A8  System.Void AssignColor(UnityEngine.Material material, UnityEngine.Color color)
  RVA=0x0B17EFB0  token=0x60000A9  System.Collections.IEnumerator GetEnumerator()
  RVA=0x0B17F410  token=0x60000AA  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,UnityEngine.Material>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,UnityEngine.Material>>.GetEnumerator()
  RVA=0x0B17F498  token=0x60000AB  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.ShaderExporterBase
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Material            _metalGlossChannelSwapMaterial  // static @ 0x0
  private   static  UnityEngine.Material            _normalChannelMaterial  // static @ 0x8
METHODS:
  RVA=0x0B186C68  token=0x60000B0  System.String SetupTexture(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, USD.NET.Unity.PreviewSurfaceSample surface, UnityEngine.Vector4 scale, System.String destTexturePath, System.String textureName, System.String textureOutput, Unity.Formats.USD.ShaderExporterBase.ConversionType conversionType)
  RVA=0x041E1670  token=0x60000B1  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.ShaderImporterBase
TYPE:  abstract class
TOKEN: 0x200002E
SIZE:  0x100
FIELDS:
  private           UnityEngine.Material            <Material>k__BackingField  // 0x10
  private           System.Boolean                  <IsSpecularWorkflow>k__BackingField  // 0x18
  public            System.Nullable<UnityEngine.Color>Diffuse  // 0x1c
  public            UnityEngine.Texture2D           DiffuseMap  // 0x30
  public            System.Nullable<UnityEngine.Color>Specular  // 0x38
  public            UnityEngine.Texture2D           SpecularMap  // 0x50
  public            System.Nullable<UnityEngine.Vector3>Normal  // 0x58
  public            UnityEngine.Texture2D           NormalMap  // 0x68
  public            System.Nullable<System.Single>  Displacement  // 0x70
  public            UnityEngine.Texture2D           DisplacementMap  // 0x78
  public            System.Nullable<System.Single>  Occlusion  // 0x80
  public            UnityEngine.Texture2D           OcclusionMap  // 0x88
  public            System.Nullable<UnityEngine.Color>Emission  // 0x90
  public            UnityEngine.Texture2D           EmissionMap  // 0xa8
  public            System.Nullable<System.Single>  Roughness  // 0xb0
  public            UnityEngine.Texture2D           RoughnessMap  // 0xb8
  public            System.Nullable<System.Single>  Metallic  // 0xc0
  public            UnityEngine.Texture2D           MetallicMap  // 0xc8
  public            System.Nullable<System.Single>  Clearcoat  // 0xd0
  public            UnityEngine.Texture2D           ClearcoatMap  // 0xd8
  public            System.Nullable<System.Single>  ClearcoatRoughness  // 0xe0
  public            System.Nullable<System.Single>  OpacityThreshold  // 0xe8
  public            System.Nullable<System.Single>  Opacity  // 0xf0
  public            UnityEngine.Texture2D           OpacityMap  // 0xf8
PROPERTIES:
  Material  get=0x02B2ECC0  set=0x053908C0
  IsSpecularWorkflow  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x053908C0  token=0x60000B6  System.Void .ctor(UnityEngine.Material material)
  RVA=-1  // abstract  token=0x60000B7  System.Void ImportFromUsd()
  RVA=0x0B1880CC  token=0x60000B8  System.Void ImportColorOrMap(USD.NET.Scene scene, USD.NET.Connectable<UnityEngine.Vector3> usdParam, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions options, UnityEngine.Texture2D& map, System.Nullable<UnityEngine.Color>& value, System.String& uvPrimvar)
  RVA=-1  // generic def  token=0x60000B9  System.Void ImportValueOrMap(USD.NET.Scene scene, USD.NET.Connectable<T> usdParam, System.Boolean isNormalMap, Unity.Formats.USD.SceneImportOptions options, UnityEngine.Texture2D& map, System.Nullable<T>& value, System.String& uvPrimvar)
  RVA=0x0B1886AC  token=0x60000BA  System.Void MergePrimvars(System.String newPrimvar, System.Collections.Generic.List<System.String> primvars)
  RVA=0x0B188224  token=0x60000BB  System.Void ImportParametersFromUsd(USD.NET.Scene scene, System.String materialPath, USD.NET.Unity.MaterialSample materialSample, USD.NET.Unity.PreviewSurfaceSample previewSurf, Unity.Formats.USD.SceneImportOptions options)
END_CLASS

CLASS: Unity.Formats.USD.StandardShaderExporter
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x10
EXTENDS: Unity.Formats.USD.ShaderExporterBase
FIELDS:
METHODS:
  RVA=0x0B189C84  token=0x60000BE  System.Void ExportStandardSpecular(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0B189848  token=0x60000BF  System.Void ExportStandardRoughness(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0B189F80  token=0x60000C0  System.Void ExportStandard(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0B1887B4  token=0x60000C1  System.Void ExportGeneric(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material material, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0B188E34  token=0x60000C2  System.Void ExportStandardCommon(USD.NET.Scene scene, System.String usdShaderPath, UnityEngine.Material mat, Unity.Formats.USD.UnityPreviewSurfaceSample surface, System.String destTexturePath)
  RVA=0x0B18A2B4  token=0x60000C3  System.Boolean ShouldUseSpecularWorkflow(UnityEngine.Material material)
  RVA=0x041E1670  token=0x60000C4  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.StandardShaderImporter
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x100
EXTENDS: Unity.Formats.USD.ShaderImporterBase
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x60000C5  System.Void .ctor(UnityEngine.Material material)
  RVA=0x0B18A3E8  token=0x60000C6  System.Void ImportFromUsd()
END_CLASS

CLASS: Unity.Formats.USD.UnityMaterialData
TYPE:  class
TOKEN: 0x2000033
SIZE:  0x38
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.String                   shaderName  // 0x10
  public            System.String[]                 shaderKeywords  // 0x18
  public            System.Collections.Generic.Dictionary<System.String,System.Single>floatArgs  // 0x20
  public            System.Collections.Generic.Dictionary<System.String,UnityEngine.Color>colorArgs  // 0x28
  public            System.Collections.Generic.Dictionary<System.String,UnityEngine.Vector4>vectorArgs  // 0x30
METHODS:
  RVA=0x0B191B64  token=0x60000C7  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UnityPreviewSurfaceSample
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x98
EXTENDS: USD.NET.Unity.PreviewSurfaceSample
FIELDS:
  public            Unity.Formats.USD.UnityMaterialDataunity  // 0x90
METHODS:
  RVA=0x0B191C70  token=0x60000C8  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UrpShaderImporter
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x100
EXTENDS: Unity.Formats.USD.ShaderImporterBase
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x60000C9  System.Void .ctor(UnityEngine.Material material)
  RVA=0x0B191CE4  token=0x60000CA  System.Void ImportFromUsd()
END_CLASS

CLASS: Unity.Formats.USD.ReadAllJob`1
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Unity.Jobs.IJobParallelFor
FIELDS:
  private   static  USD.NET.Scene                   m_scene  // static @ 0x0
  private   static  pxr.SdfPath[]                   m_paths  // static @ 0x0
  private   static  T[]                             m_results  // static @ 0x0
  private   static  System.Object[]                 m_done  // static @ 0x0
  private   static  USD.NET.SampleEnumerator.SampleHolder<T>m_current  // static @ 0x0
  private   static  System.Threading.AutoResetEvent m_ready  // static @ 0x0
  private   static  Unity.Formats.USD.SceneImportOptionsm_importOptions  // static @ 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000CD  System.Void .ctor(USD.NET.Scene scene, pxr.SdfPath[] paths, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=-1  // not resolved  token=0x60000CE  System.Boolean ShouldReadPath(USD.NET.Scene scene, pxr.SdfPath path)
  RVA=-1  // not resolved  token=0x60000CF  System.Void Run()
  RVA=-1  // not resolved  token=0x60000D0  System.Void Execute(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000D1  System.Void AddPrimvarsFromMaterial(System.Int32 index, USD.NET.IArbitraryPrimvars& sample)
  RVA=-1  // not resolved  token=0x60000D2  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000D3  System.Void Reset()
  RVA=-1  // not resolved  token=0x60000D4  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000D5  System.Collections.Generic.IEnumerator<USD.NET.SampleEnumerator.SampleHolder<T>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60000D6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Formats.USD.IRestorable
TYPE:  interface
TOKEN: 0x2000037
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000D7  System.Boolean IsRestoredFromCachedData()
  RVA=-1  // abstract  token=0x60000D8  System.Void FromCachedData(USD.NET.IRestorableData data)
  RVA=-1  // abstract  token=0x60000D9  USD.NET.IRestorableData ToCachedData()
END_CLASS

CLASS: Unity.Formats.USD.ISanitizable
TYPE:  interface
TOKEN: 0x2000038
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000DA  System.Void Sanitize(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
END_CLASS

CLASS: Unity.Formats.USD.MeshStaticPropertiesData
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x28
IMPLEMENTS: USD.NET.IRestorableData
FIELDS:
  private           System.Int32[]                  originalFaceVertexCounts  // 0x10
  private           System.Int32[]                  originalFaceVertexIndices  // 0x18
  private           System.Boolean                  arePrimvarsFaceVarying  // 0x20
  private           USD.NET.Orientation             orientation  // 0x24
METHODS:
  RVA=0x041E1670  token=0x60000DB  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SanitizedXformSample
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x60
EXTENDS: USD.NET.Unity.XformSample
IMPLEMENTS: Unity.Formats.USD.ISanitizable
FIELDS:
METHODS:
  RVA=0x0B180704  token=0x60000DC  System.Void Sanitize(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B18072C  token=0x60000DD  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SanitizedCameraSample
TYPE:  class
TOKEN: 0x200003B
SIZE:  0xA0
EXTENDS: USD.NET.Unity.CameraSample
IMPLEMENTS: Unity.Formats.USD.ISanitizable
FIELDS:
METHODS:
  RVA=0x0B180704  token=0x60000DE  System.Void Sanitize(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B18072C  token=0x60000DF  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SanitizedMeshSample
TYPE:  class
TOKEN: 0x200003C
SIZE:  0xF0
EXTENDS: USD.NET.Unity.MeshSample
IMPLEMENTS: Unity.Formats.USD.ISanitizable Unity.Formats.USD.IRestorable
FIELDS:
  private           System.Int32[]                  originalFaceVertexCounts  // 0xc0
  private           System.Int32[]                  originalFaceVertexIndices  // 0xc8
  private           System.Boolean                  arePrimvarsFaceVarying  // 0xd0
  private           System.Int32[]                  faceMapping  // 0xd8
  private           System.Int32[]                  triangulatedFaceVertexIndices  // 0xe0
  private           System.Boolean                  isRestoredFromCachedData  // 0xe8
METHODS:
  RVA=0x04D8C8C0  token=0x60000E0  System.Boolean IsRestoredFromCachedData()
  RVA=0x0B180EB4  token=0x60000E1  System.Void FromCachedData(USD.NET.IRestorableData restorableData)
  RVA=0x0B181DBC  token=0x60000E2  USD.NET.IRestorableData ToCachedData()
  RVA=0x0B1808A8  token=0x60000E3  System.Void BackupTopology()
  RVA=0x0B181258  token=0x60000E4  System.Boolean IsTopologyBackedUp()
  RVA=0x0B181274  token=0x60000E5  System.Void Sanitize(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B181E44  token=0x60000E6  System.Void Triangulate(System.Boolean changeHandedness)
  RVA=0x0B181BE0  token=0x60000E7  System.Boolean ShouldUnweldVertices(System.Boolean bindMaterials)
  RVA=0x0B180734  token=0x60000E8  System.Boolean AreAnyArbitraryPrimvarsFaceVarying()
  RVA=-1  // generic def  token=0x60000E9  System.Void Flatten(T[]& values, System.Int32[] indices)
  RVA=0x0B1822A8  token=0x60000EA  System.Void UnweldPrimvar(USD.NET.Primvar<System.Object> primvar, System.Boolean changeHandedness)
  RVA=0x0B180CB0  token=0x60000EB  System.Void FlattenPrimvar(USD.NET.Primvar<System.Object> primvar)
  RVA=-1  // generic def  token=0x60000EC  System.Void TriangulateAttributes(T[]& values, System.Int32[] faceVertexCounts, System.Boolean changeHandedness)
  RVA=-1  // generic def  token=0x60000ED  System.Void ConvertInterpolationToFaceVarying(T[]& values, System.Int32[] vertexIndices, System.Boolean changeHandedness, pxr.TfToken interpolation)
  RVA=0x0B180F60  token=0x60000EE  pxr.TfToken GuessInterpolation(System.Int32 count)
  RVA=0x0B1810B8  token=0x60000EF  System.Boolean IsInterpolationValid(pxr.TfToken interpolation, System.Int32 count)
  RVA=-1  // generic def  token=0x60000F0  System.Void UniformToFaceVarying(T[]& values, System.Int32 vertexCount)
  RVA=0x0B1809B0  token=0x60000F1  System.Void ComputeNormals(UnityEngine.Vector3[] points, System.Int32[] faceVertexIndices, UnityEngine.Vector3[]& normals)
  RVA=0x0B181DA4  token=0x60000F2  System.Boolean ShouldUnwindVertices(System.Boolean changeHandedness)
  RVA=0x0B182660  token=0x60000F3  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.ExportHelpers
TYPE:  static class
TOKEN: 0x200003D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B17AB80  token=0x60000F4  USD.NET.Scene InitForSave(System.String filePath)
  RVA=0x0B17A910  token=0x60000F5  System.Void ExportGameObjects(UnityEngine.GameObject[] objects, USD.NET.Scene scene, Unity.Formats.USD.BasisTransformation basisTransform, System.Boolean exportMonoBehaviours)
END_CLASS

CLASS: Unity.Formats.USD.HierarchyBuilder
TYPE:  static class
TOKEN: 0x200003E
SIZE:  0x10
FIELDS:
  private   static readonly pxr.SdfPath                     kAbsoluteRootPath  // static @ 0x0
METHODS:
  RVA=0x0B17B38C  token=0x60000F6  Unity.Jobs.JobHandle BeginReading(USD.NET.Scene scene, pxr.SdfPath usdRoot, Unity.Formats.USD.PrimMap map, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17CAAC  token=0x60000F7  Unity.Formats.USD.HierarchyBuilder.HierInfo[] BuildObjectLists(USD.NET.Scene scene, UnityEngine.GameObject unityRoot, pxr.SdfPath usdRoot, Unity.Formats.USD.PrimMap map, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17BD64  token=0x60000F8  Unity.Formats.USD.PrimMap BuildGameObjects(USD.NET.Scene scene, UnityEngine.GameObject unityRoot, pxr.SdfPath usdRoot, System.Collections.Generic.IEnumerable<pxr.SdfPath> paths, Unity.Formats.USD.PrimMap map, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17D578  token=0x60000F9  System.Boolean IsVisible(pxr.UsdPrim usdPrim)
  RVA=0x0B17B2FC  token=0x60000FA  System.Void ApplySelfVisibility(UnityEngine.GameObject go, pxr.UsdPrim usdPrim)
  RVA=0x0B17CC40  token=0x60000FB  System.Void CreateAncestors(pxr.SdfPath path, Unity.Formats.USD.PrimMap map, UnityEngine.GameObject unityRoot, pxr.SdfPath usdRoot, Unity.Formats.USD.SceneImportOptions options, UnityEngine.GameObject& parentGo)
  RVA=0x0B17D944  token=0x60000FC  System.Void ProcessPaths(Unity.Formats.USD.HierarchyBuilder.HierInfo[] infos, USD.NET.Scene scene, UnityEngine.GameObject unityRoot, pxr.SdfPath usdRoot, Unity.Formats.USD.PrimMap map, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17D6C0  token=0x60000FD  System.Void PopulateSkelCache(Unity.Formats.USD.HierarchyBuilder.HierInfo& skelRootInfo, pxr.UsdSkelCache skelCache)
  RVA=0x0B17E1D8  token=0x60000FE  System.Void ReadSkeletonJoints(Unity.Formats.USD.HierarchyBuilder.HierInfo& skelRootInfo)
  RVA=0x0B17CF8C  token=0x60000FF  System.Void ExpandSkeleton(Unity.Formats.USD.HierarchyBuilder.HierInfo info, UnityEngine.GameObject unityRoot, pxr.SdfPath usdRoot, pxr.UsdPrim prim, Unity.Formats.USD.PrimMap map, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17DEE0  token=0x6000100  System.Void ReadModelInfo(Unity.Formats.USD.HierarchyBuilder.HierInfo& info)
  RVA=0x0B17AEC4  token=0x6000101  System.Void AddModelRoot(UnityEngine.GameObject go, Unity.Formats.USD.HierarchyBuilder.HierInfo info)
  RVA=0x0B17B0D4  token=0x6000102  System.Void AddPayload(UnityEngine.GameObject go, Unity.Formats.USD.HierarchyBuilder.HierInfo info, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17B1CC  token=0x6000103  System.Void AddVariantSet(UnityEngine.GameObject go, pxr.UsdPrim prim)
  RVA=0x0B17D2D8  token=0x6000104  UnityEngine.GameObject FindOrCreateGameObject(UnityEngine.Transform parent, pxr.SdfPath path, UnityEngine.Transform unityRoot, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B17E758  token=0x6000105  System.Void .cctor()
END_CLASS

CLASS: Unity.Formats.USD.ImportHelpers
TYPE:  static class
TOKEN: 0x2000045
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B17EA80  token=0x6000110  UnityEngine.GameObject ImportSceneAsGameObject(USD.NET.Scene scene, UnityEngine.GameObject parent, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B17ECE8  token=0x6000111  USD.NET.Scene InitForOpen(System.String path, pxr.UsdStage.InitialLoadSet loadSet)
  RVA=0x0B17E8B0  token=0x6000112  pxr.SdfPath GetDefaultRoot(USD.NET.Scene scene)
  RVA=0x0B17ED5C  token=0x6000113  UnityEngine.GameObject UsdToGameObject(UnityEngine.GameObject parent, USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B17E964  token=0x6000114  System.String GetObjectName(pxr.SdfPath rootPrimName, System.String path)
  RVA=0x0B17E9EC  token=0x6000115  System.String GetObjectName(System.String path)
  RVA=0x0B17EA38  token=0x6000116  System.String GetPrefabName(System.String path)
END_CLASS

CLASS: Unity.Formats.USD.IImportProcessUsd
TYPE:  interface
TOKEN: 0x2000046
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000117  System.Void ProcessUsd(USD.NET.Scene scene, Unity.Formats.USD.SceneImportOptions sceneImportOptions)
END_CLASS

CLASS: Unity.Formats.USD.IImportPostProcessHierarchy
TYPE:  interface
TOKEN: 0x2000047
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000118  System.Void PostProcessHierarchy(Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions sceneImportOptions)
END_CLASS

CLASS: Unity.Formats.USD.IImportPostProcessComponents
TYPE:  interface
TOKEN: 0x2000048
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000119  System.Void PostProcessComponents(Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions sceneImportOptions)
END_CLASS

CLASS: Unity.Formats.USD.PrimMap
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x98
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  public            pxr.UsdSkelCache                SkelCache  // 0x10
  public            System.Collections.Generic.Dictionary<pxr.SdfPath,pxr.UsdSkelBindingVector>SkelBindings  // 0x18
  public            System.Collections.Generic.Dictionary<pxr.SdfPath,pxr.UsdSkelSkinningQuery>SkinningQueries  // 0x20
  public            System.Collections.Generic.Dictionary<pxr.SdfPath,Unity.Formats.USD.MeshImporter.GeometrySubsets>MeshSubsets  // 0x28
  private           pxr.SdfPath[]                   <Cameras>k__BackingField  // 0x30
  private           pxr.SdfPath[]                   <Meshes>k__BackingField  // 0x38
  private           pxr.SdfPath[]                   <Cubes>k__BackingField  // 0x40
  private           pxr.SdfPath[]                   <Spheres>k__BackingField  // 0x48
  private           pxr.SdfPath[]                   <Xforms>k__BackingField  // 0x50
  private           pxr.SdfPath[]                   <SkelRoots>k__BackingField  // 0x58
  private           pxr.SdfPath[]                   <Skeletons>k__BackingField  // 0x60
  private           pxr.SdfPath[]                   <Materials>k__BackingField  // 0x68
  private           System.Boolean                  <ContainsPointInstances>k__BackingField  // 0x70
  private           System.Boolean                  <HasErrors>k__BackingField  // 0x71
  private           System.Collections.Generic.Dictionary<pxr.SdfPath,UnityEngine.GameObject>m_prims  // 0x78
  private           System.Collections.Generic.Dictionary<pxr.SdfPath,Unity.Formats.USD.PrimMap.InstanceRoot>m_instanceRoots  // 0x80
  private           System.Collections.Generic.HashSet<UnityEngine.GameObject>m_instances  // 0x88
  private           System.Collections.Generic.Dictionary<pxr.SdfPath,UnityEngine.GameObject>m_masterRoots  // 0x90
PROPERTIES:
  Cameras  get=0x011F36E0  set=0x05396200
  Meshes  get=0x04D85A50  set=0x046A91B0
  Cubes  get=0x04D85A60  set=0x042B4AE0
  Spheres  get=0x04D86200  set=0x035A41A0
  Xforms  get=0x04D86270  set=0x02FDAEA0
  SkelRoots  get=0x02E56440  set=0x036E5590
  Skeletons  get=0x04D86280  set=0x063CC164
  Materials  get=0x04D86230  set=0x0485F170
  ContainsPointInstances  get=0x04D86A50  set=0x04D86A80
  HasErrors  get=0x04D89040  set=0x04D89050
  Item  get=0x0B18035C  set=0x0B180448
METHODS:
  RVA=0x0B180170  token=0x600012E  System.Void .ctor()
  RVA=0x0B18010C  token=0x6000131  System.Boolean TryGetValue(pxr.SdfPath key, UnityEngine.GameObject& obj)
  RVA=0x0B17FF6C  token=0x6000132  System.Collections.IEnumerator GetEnumerator()
  RVA=0x0B180084  token=0x6000133  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<pxr.SdfPath,UnityEngine.GameObject>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<pxr.SdfPath,UnityEngine.GameObject>>.GetEnumerator()
  RVA=0x0B17F898  token=0x6000134  System.Void AddInstance(UnityEngine.GameObject goInst)
  RVA=0x0B17F8F0  token=0x6000135  System.Void AddMasterRoot(pxr.SdfPath path, UnityEngine.GameObject go)
  RVA=0x0B17F7EC  token=0x6000136  System.Void AddInstanceRoot(pxr.SdfPath instancePath, UnityEngine.GameObject go, pxr.SdfPath masterPath)
  RVA=0x0B18003C  token=0x6000137  System.Collections.Generic.Dictionary.KeyCollection<pxr.SdfPath,UnityEngine.GameObject> GetMasterRootPaths()
  RVA=0x0B17FFF4  token=0x6000138  System.Collections.Generic.Dictionary.ValueCollection<pxr.SdfPath,Unity.Formats.USD.PrimMap.InstanceRoot> GetInstanceRoots()
  RVA=0x0B17FA94  token=0x6000139  System.Void DestroyAll()
  RVA=0x0B17F968  token=0x600013A  System.Void Clear()
END_CLASS

CLASS: Unity.Formats.USD.ExportFunction
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B17A848  token=0x600013B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x07D85524  token=0x600013C  System.Void Invoke(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B17A7C0  token=0x600013D  System.IAsyncResult BeginInvoke(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600013E  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Unity.Formats.USD.ObjectProcessor
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AAFCFE0  token=0x600013F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000140  System.Void Invoke(UnityEngine.GameObject go, Unity.Formats.USD.ExportContext context)
  RVA=0x05E43E3C  token=0x6000141  System.IAsyncResult BeginInvoke(UnityEngine.GameObject go, Unity.Formats.USD.ExportContext context, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000142  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Unity.Formats.USD.ObjectContext
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x30
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            System.String                   path  // 0x18
  public            USD.NET.SampleBase              sample  // 0x20
  public            System.Object                   additionalData  // 0x28
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.ActiveExportPolicy
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.ActiveExportPolicyExportAsVisibility  // const
  public    static  Unity.Formats.USD.ActiveExportPolicyExportAsActive  // const
  public    static  Unity.Formats.USD.ActiveExportPolicyDoNotExport  // const
  public    static  Unity.Formats.USD.ActiveExportPolicyIgnore  // const
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.ExportContext
TYPE:  class
TOKEN: 0x200004F
SIZE:  0x88
FIELDS:
  public            USD.NET.Scene                   scene  // 0x10
  public            UnityEngine.Transform           exportRoot  // 0x18
  public            System.Boolean                  exportMaterials  // 0x20
  public            System.Boolean                  exportNative  // 0x21
  public            System.Single                   scale  // 0x24
  public            System.Boolean                  exportTransformOverrides  // 0x28
  public            Unity.Formats.USD.BasisTransformationbasisTransform  // 0x2c
  public            Unity.Formats.USD.ActiveExportPolicyactivePolicy  // 0x30
  public            System.Collections.Generic.Dictionary<UnityEngine.GameObject,Unity.Formats.USD.ExportPlan>plans  // 0x38
  public            System.Collections.Generic.Dictionary<UnityEngine.Material,System.String>matMap  // 0x40
  public            System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform>meshToSkelRoot  // 0x48
  public            System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform[]>meshToBones  // 0x50
  public            System.Collections.Generic.Dictionary<UnityEngine.Transform,System.Collections.Generic.List<System.String>>skelSortedMap  // 0x58
  public            System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform>pathToBone  // 0x60
  public            System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform>boneToRoot  // 0x68
  public            System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Matrix4x4>bindPoses  // 0x70
  public            System.Collections.Generic.Dictionary<System.Type,USD.NET.SampleBase>samples  // 0x78
  public            System.Diagnostics.Stopwatch    analyticsTotalTimeStopwatch  // 0x80
METHODS:
  RVA=0x0B17A4C8  token=0x6000143  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.Exporter
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x30
FIELDS:
  public            System.String                   path  // 0x10
  public            USD.NET.SampleBase              sample  // 0x18
  public            Unity.Formats.USD.ExportFunctionexportFunc  // 0x20
  public            System.Object                   data  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000144  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.ExportPlan
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<Unity.Formats.USD.Exporter>exporters  // 0x10
METHODS:
  RVA=0x0B17ACD8  token=0x6000145  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SceneExporter
TYPE:  static class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B183ED4  token=0x6000146  System.Void Export(UnityEngine.GameObject root, USD.NET.Scene scene, Unity.Formats.USD.BasisTransformation basisTransform, System.Boolean exportUnvarying, System.Boolean zeroRootTransform, System.Boolean exportMaterials, System.Boolean exportMonoBehaviours, System.Boolean exportOverrides)
  RVA=0x0B183940  token=0x6000147  System.Void Export(UnityEngine.GameObject root, Unity.Formats.USD.ExportContext context, System.Boolean zeroRootTransform)
  RVA=0x0B182DA8  token=0x6000148  System.Void ExportImpl(UnityEngine.GameObject root, Unity.Formats.USD.ExportContext context)
  RVA=0x0B186354  token=0x6000149  System.Void Traverse(UnityEngine.GameObject obj, Unity.Formats.USD.ObjectProcessor processor, Unity.Formats.USD.ExportContext context)
  RVA=0x0B182668  token=0x600014A  System.Void AccumNestedBones(UnityEngine.Transform curXf, System.Collections.Generic.List<UnityEngine.Transform> children, Unity.Formats.USD.ExportContext ctx)
  RVA=-1  // generic def  token=0x600014B  T CreateSample(Unity.Formats.USD.ExportContext context)
  RVA=0x0B185658  token=0x600014C  System.Void SyncExportContext(UnityEngine.GameObject exportRoot, Unity.Formats.USD.ExportContext context)
  RVA=0x0B184044  token=0x600014D  System.Void InitExportableObjects(UnityEngine.GameObject go, Unity.Formats.USD.ExportContext context)
  RVA=0x0B184A6C  token=0x600014E  UnityEngine.Transform MergeBonesBelowAnimator(UnityEngine.Transform animator, Unity.Formats.USD.ExportContext context)
  RVA=0x0B185208  token=0x600014F  System.Void MergeBonesSimple(UnityEngine.Transform source, UnityEngine.Transform rootBone, UnityEngine.Transform[] bones, UnityEngine.Matrix4x4[] bindPoses, Unity.Formats.USD.ExportContext context)
  RVA=0x0B1829D8  token=0x6000150  System.Void CreateExportPlan(UnityEngine.GameObject go, USD.NET.SampleBase sample, Unity.Formats.USD.ExportFunction exportFunc, Unity.Formats.USD.ExportContext context, System.String pathSuffix, System.Boolean insertFirst)
  RVA=0x0B182860  token=0x6000151  UnityEngine.Matrix4x4 ComputeWorldXf(UnityEngine.Transform curBone, Unity.Formats.USD.ExportContext context)
END_CLASS

CLASS: Unity.Formats.USD.IImporter
TYPE:  interface
TOKEN: 0x2000054
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000156  System.Void BeginReading(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=-1  // abstract  token=0x6000157  System.Collections.IEnumerator Import(USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
END_CLASS

CLASS: Unity.Formats.USD.SceneImporter
TYPE:  static class
TOKEN: 0x2000055
SIZE:  0x10
FIELDS:
  public    static  Unity.Formats.USD.IImporter     ActiveMeshImporter  // static @ 0x0
METHODS:
  RVA=0x0B186B58  token=0x6000158  System.Void .cctor()
  RVA=0x0B186778  token=0x6000159  System.Void ImportUsd(UnityEngine.GameObject goRoot, USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B1867F4  token=0x600015A  System.Void ImportUsd(UnityEngine.GameObject goRoot, USD.NET.Scene scene, Unity.Formats.USD.PrimMap primMap, System.Boolean composingSubtree, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B186700  token=0x600015B  Unity.Formats.USD.UsdEditorAnalytics.ImportResult CreateImportResult(System.Boolean success, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.ImportType importType)
  RVA=0x0B186528  token=0x600015C  Unity.Formats.USD.PrimMap BuildScene(USD.NET.Scene scene, UnityEngine.GameObject root, Unity.Formats.USD.SceneImportOptions importOptions, Unity.Formats.USD.PrimMap primMap, System.Boolean composingSubtree)
  RVA=-1  // generic def  token=0x600015D  System.Void RemoveComponent(UnityEngine.GameObject go)
  RVA=0x0B186644  token=0x600015E  System.Collections.IEnumerator BuildScene(USD.NET.Scene scene, UnityEngine.GameObject root, Unity.Formats.USD.SceneImportOptions importOptions, Unity.Formats.USD.PrimMap primMap, System.Single targetFrameMilliseconds, System.Boolean composingSubtree)
  RVA=0x0B186B1C  token=0x600015F  System.Boolean ShouldYield(System.Single targetTime, System.Diagnostics.Stopwatch timer)
  RVA=0x0B186AE4  token=0x6000160  System.Void ResetTimer(System.Diagnostics.Stopwatch timer)
  RVA=0x0B186A28  token=0x6000161  System.Boolean IsSkinnedMesh(pxr.UsdPrim prim, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions importOptions)
END_CLASS

CLASS: Unity.Formats.USD.PayloadPolicy
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.PayloadPolicy DontLoadPayloads  // const
  public    static  Unity.Formats.USD.PayloadPolicy LoadAll  // const
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.BasisTransformation
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.BasisTransformationFastWithNegativeScale  // const
  public    static  Unity.Formats.USD.BasisTransformationSlowAndSafe  // const
  public    static  Unity.Formats.USD.BasisTransformationSlowAndSafeAsFBX  // const
  public    static  Unity.Formats.USD.BasisTransformationNone  // const
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.MaterialImportMode
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Formats.USD.MaterialImportModeImportPreviewSurface  // const
  public    static  Unity.Formats.USD.MaterialImportModeImportDisplayColor  // const
  public    static  Unity.Formats.USD.MaterialImportModeNone  // const
METHODS:
END_CLASS

CLASS: Unity.Formats.USD.SceneImportOptions
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x60
FIELDS:
  public            System.String                   projectAssetPath  // 0x10
  public            pxr.SdfPath                     usdRootPath  // 0x18
  public            System.Boolean                  forceRebuild  // 0x20
  public            Unity.Formats.USD.PayloadPolicy payloadPolicy  // 0x24
  public            System.Boolean                  importHierarchy  // 0x28
  public            System.Boolean                  importCameras  // 0x29
  public            System.Boolean                  importMeshes  // 0x2a
  public            System.Boolean                  importSkinning  // 0x2b
  public            System.Boolean                  importSkinWeights  // 0x2c
  public            System.Boolean                  importTransforms  // 0x2d
  public            System.Boolean                  importSceneInstances  // 0x2e
  public            System.Boolean                  importPointInstances  // 0x2f
  public            System.Boolean                  importMonoBehaviours  // 0x30
  public            Unity.Formats.USD.BasisTransformationchangeHandedness  // 0x34
  public            System.Single                   scale  // 0x38
  public            System.Boolean                  interpolate  // 0x3c
  public            System.Boolean                  enableGpuInstancing  // 0x3d
  public            System.Boolean                  useDisplayColorAsFallbackMaterial  // 0x3e
  public            Unity.Formats.USD.MaterialImportModematerialImportMode  // 0x40
  public            Unity.Formats.USD.MaterialMap   materialMap  // 0x48
  public            Unity.Formats.USD.MeshImportOptionsmeshOptions  // 0x50
  private           Unity.Formats.USD.ImportType    <ImportType>k__BackingField  // 0x58
PROPERTIES:
  ShouldBindMaterials  get=0x0B192578
  ImportType  get=0x04D86570  set=0x04D865D0
METHODS:
  RVA=0x0B192458  token=0x6000178  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdzExporter
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1975F0  token=0x6000179  System.Void ExportUsdz(System.String usdzFilePath, UnityEngine.GameObject root)
  RVA=0x041E1670  token=0x600017A  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkeletonExporter
TYPE:  static class
TOKEN: 0x200005D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1932E0  token=0x600017B  System.Void ExportSkeleton(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B192E40  token=0x600017C  System.Void ExportSkelRoot(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x0B19274C  token=0x600017D  System.Void ExportSkelAnimation(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
END_CLASS

CLASS: Unity.Formats.USD.SkeletonImporter
TYPE:  static class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1944C8  token=0x600017E  System.Void BuildSkeletonBone(System.String skelPath, UnityEngine.GameObject go, UnityEngine.Matrix4x4 restXform, pxr.VtTokenArray joints, Unity.Formats.USD.SceneImportOptions importOptions)
  RVA=0x0B193C28  token=0x600017F  System.Void BuildDebugBindTransforms(Unity.Formats.USD.SkeletonSample skelSample, UnityEngine.GameObject goSkeleton, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B193B1C  token=0x6000180  System.Void BuildBindTransforms(System.String path, Unity.Formats.USD.SkeletonSample skelSample, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x0B1951A4  token=0x6000181  System.Boolean JointsMatch(System.String[] lhs, System.String[] rhs)
  RVA=0x0B194938  token=0x6000182  System.Void BuildSkinnedMesh(System.String meshPath, System.String skelPath, Unity.Formats.USD.SkeletonSample skeleton, pxr.UsdSkelSkinningQuery skinningQuery, UnityEngine.GameObject go, Unity.Formats.USD.PrimMap primMap, Unity.Formats.USD.SceneImportOptions options)
END_CLASS

CLASS: Unity.Formats.USD.UnitySkeleton
TYPE:  class
TOKEN: 0x200005F
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           rootBone  // 0x10
  public            UnityEngine.Transform[]         bones  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000183  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkelBindingSample
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x50
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.String[]                 blendShapes  // 0x10
  public            USD.NET.Relationship            blendShapeTargets  // 0x18
  public            USD.NET.Relationship            animationSource  // 0x20
  public            USD.NET.Relationship            skeleton  // 0x28
  public            System.String[]                 joints  // 0x30
  public            USD.NET.Primvar<System.Int32[]> jointIndices  // 0x38
  public            USD.NET.Primvar<UnityEngine.Matrix4x4>geomBindTransform  // 0x40
  public            USD.NET.Primvar<System.Single[]>jointWeights  // 0x48
METHODS:
  RVA=0x0B192580  token=0x6000184  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkelRootSample
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x88
EXTENDS: USD.NET.Unity.BoundableSample
FIELDS:
  public            USD.NET.Relationship            skeleton  // 0x78
  public            USD.NET.Relationship            animationSource  // 0x80
METHODS:
  RVA=0x0B1926D0  token=0x6000185  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkeletonSample
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x78
EXTENDS: USD.NET.Unity.XformableSample
FIELDS:
  public            System.String[]                 joints  // 0x60
  public            UnityEngine.Matrix4x4[]         bindTransforms  // 0x68
  public            UnityEngine.Matrix4x4[]         restTransforms  // 0x70
METHODS:
  RVA=0x0B18072C  token=0x6000186  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkelAnimationSample
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x40
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.String[]                 joints  // 0x10
  public            UnityEngine.Vector3[]           translations  // 0x18
  public            UnityEngine.Quaternion[]        rotations  // 0x20
  public            pxr.VtVec3hArray                scales  // 0x28
  public            System.String[]                 blendShapes  // 0x30
  public            System.Single[]                 blendShapeWeights  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000187  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.BlendShapeSample
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x20
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            UnityEngine.Vector3[]           offsets  // 0x10
  public            System.UInt32[]                 pointIndices  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000188  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.SkeletonIo
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform[]>m_bindings  // 0x10
METHODS:
  RVA=0x0B195220  token=0x6000189  UnityEngine.Transform[] GetBones(UnityEngine.Transform rootBone)
  RVA=0x0B195274  token=0x600018A  System.Void RegisterSkeleton(UnityEngine.Transform rootBone, UnityEngine.Transform[] bones)
  RVA=0x0B1952D8  token=0x600018B  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.NativeExporter
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600018C  System.Void ExportObject(Unity.Formats.USD.ObjectContext objContext, Unity.Formats.USD.ExportContext exportContext)
  RVA=0x041E1670  token=0x600018D  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.NativeImporter
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600018E  System.Void ImportObject(USD.NET.Scene scene, UnityEngine.GameObject go, pxr.UsdPrim usdPrim, Unity.Formats.USD.SceneImportOptions options)
  RVA=0x041E1670  token=0x600018F  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdPlayableAsset
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x30
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  private           Unity.Formats.USD.UsdAsset      m_sourceUsdAsset  // 0x18
  public            UnityEngine.ExposedReference<Unity.Formats.USD.UsdAsset>SourceUsdAsset  // 0x20
PROPERTIES:
  clipCaps  get=0x04D8D050
  duration  get=0x0B195880
METHODS:
  RVA=0x0B195854  token=0x6000191  Unity.Formats.USD.UsdAsset GetUsdAsset(System.String usdRootPath)
  RVA=0x0B195688  token=0x6000192  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x6000194  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdPlayableBehaviour
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            Unity.Formats.USD.UsdPlayableAssetplayableAsset  // 0x10
  private           System.Boolean                  m_errorOnce  // 0x18
METHODS:
  RVA=0x0B1959D8  token=0x6000195  System.Void OnGraphStart(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x6000196  System.Void OnGraphStop(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x6000197  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x6000198  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x6000199  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B1959E0  token=0x600019A  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x041E1670  token=0x600019B  System.Void PrepareData(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x04D8EB60  token=0x600019C  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdPlayableTrack
TYPE:  class
TOKEN: 0x200006A
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B195B60  token=0x600019D  System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x0B195B9C  token=0x600019E  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdRecorderBehaviour
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x50
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private   static  System.Int32                    kExportFrameRate  // const
  private           System.Boolean                  m_isPaused  // 0x10
  public            Unity.Formats.USD.UsdRecorderClipClip  // 0x18
  private           System.String                   usdcFileName  // 0x20
  private           System.String                   usdzFileName  // 0x28
  private           System.String                   usdzFilePath  // 0x30
  private           System.String                   currentDir  // 0x38
  private           System.IO.DirectoryInfo         usdzTemporaryDir  // 0x40
  private           UnityEngine.GameObject          _root  // 0x48
METHODS:
  RVA=0x0B195BE8  token=0x600019F  System.Void BeginRecording(System.Double currentTime, UnityEngine.GameObject root)
  RVA=0x0B196C9C  token=0x60001A0  System.Void StopRecording(System.Double currentTime)
  RVA=0x0B196B18  token=0x60001A1  System.Void ProcessRecording(System.Double currentTime, UnityEngine.GameObject root)
  RVA=0x02FFF600  token=0x60001A2  System.Boolean IsPlaying()
  RVA=0x041E1670  token=0x60001A3  System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable)
  RVA=0x0B1969A4  token=0x60001A4  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x0B196884  token=0x60001A5  System.Void OnGraphStart(UnityEngine.Playables.Playable playable)
  RVA=0x0B19694C  token=0x60001A6  System.Void OnGraphStop(UnityEngine.Playables.Playable playable)
  RVA=0x0B1969FC  token=0x60001A7  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x04D8CCB0  token=0x60001A8  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x015EFCD0  token=0x60001A9  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B19679C  token=0x60001AA  System.Void OnFrameEnd(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x041E1670  token=0x60001AB  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdRecorderClip
TYPE:  class
TOKEN: 0x200006D
SIZE:  0x58
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            UnityEngine.ExposedReference<UnityEngine.GameObject>m_exportRoot  // 0x18
  private           UnityEngine.GameObject[]        m_trackedRoots  // 0x28
  public            System.Boolean                  m_exportMaterials  // 0x30
  public            Unity.Formats.USD.BasisTransformationm_convertHandedness  // 0x34
  public            Unity.Formats.USD.ActiveExportPolicym_activePolicy  // 0x38
  public            System.String                   m_usdFile  // 0x40
  private           USD.NET.Scene                   <UsdScene>k__BackingField  // 0x48
  private           Unity.Formats.USD.ExportContext m_context  // 0x50
PROPERTIES:
  UsdScene  get=0x04D86200  set=0x035A41A0
  Context  get=0x04D86270  set=0x02FDAEA0
  clipCaps  get=0x011EC580
  IsUSDZ  get=0x0B197268
METHODS:
  RVA=0x0B197178  token=0x60001B2  UnityEngine.GameObject GetExportRoot(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x0B197080  token=0x60001B5  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x041E1670  token=0x60001B6  System.Void OnDestroy()
  RVA=0x0B1971CC  token=0x60001B7  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdRecorderTrack
TYPE:  class
TOKEN: 0x200006E
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B1972D4  token=0x60001B8  System.Void .ctor()
END_CLASS

CLASS: Unity.Formats.USD.UsdWaitForEndOfFrame
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<System.Action>m_pending  // 0x18
  private   static  Unity.Formats.USD.UsdWaitForEndOfFrames_instance  // static @ 0x0
METHODS:
  RVA=0x0B197374  token=0x60001B9  Unity.Formats.USD.UsdWaitForEndOfFrame GetInstance()
  RVA=0x0B197320  token=0x60001BA  System.Void Add(System.Action callback)
  RVA=0x0B197514  token=0x60001BB  System.Collections.IEnumerator WaitForEndOfFrame()
  RVA=0x0B1974F0  token=0x60001BC  System.Void LateUpdate()
  RVA=0x0B197570  token=0x60001BD  System.Void .ctor()
END_CLASS

