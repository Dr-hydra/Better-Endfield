// ========================================================
// Dumped by @desirepro
// Assembly: USD.NET.Unity.dll
// Classes:  52
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

CLASS: CurveType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.BasisCurvesSample.CurveTypeLinear  // const
  public    static  USD.NET.Unity.BasisCurvesSample.CurveTypeCubic  // const
METHODS:
END_CLASS

CLASS: Basis
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.BasisCurvesSample.BasisBezier  // const
  public    static  USD.NET.Unity.BasisCurvesSample.BasisBspline  // const
  public    static  USD.NET.Unity.BasisCurvesSample.BasisCatmullRom  // const
  public    static  USD.NET.Unity.BasisCurvesSample.BasisHermite  // const
  public    static  USD.NET.Unity.BasisCurvesSample.BasisPower  // const
METHODS:
END_CLASS

CLASS: WrapMode
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.BasisCurvesSample.WrapModeNonperiodic  // const
  public    static  USD.NET.Unity.BasisCurvesSample.WrapModePeriodic  // const
METHODS:
END_CLASS

CLASS: ProjectionType
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.CameraSample.ProjectionTypePerspective  // const
  public    static  USD.NET.Unity.CameraSample.ProjectionTypeOrthographic  // const
METHODS:
END_CLASS

CLASS: StereoRole
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.CameraSample.StereoRoleMono  // const
  public    static  USD.NET.Unity.CameraSample.StereoRoleLeft  // const
  public    static  USD.NET.Unity.CameraSample.StereoRoleRight  // const
METHODS:
END_CLASS

CLASS: Shutter
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x20
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.Double                   open  // 0x10
  public            System.Double                   close  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600000C  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x20
FIELDS:
  public            System.Type                     inputParamType  // 0x10
  public            System.Func<System.Reflection.FieldInfo,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000042  System.Void .ctor()
  RVA=0x0B5DB15C  token=0x6000043  System.Boolean <GetInputParameters>b__0(System.Reflection.FieldInfo info)
END_CLASS

CLASS: <GetInputParameters>d__3
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           USD.NET.Unity.ParameterInfo     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x40
  public            USD.NET.Unity.ShaderSample      <>4__this  // 0x48
  private           USD.NET.Unity.ShaderSample.<>c__DisplayClass3_0<>8__1  // 0x50
  private           System.Collections.Generic.IEnumerator<System.Reflection.FieldInfo><>7__wrap1  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<USD.NET.Unity.ParameterInfo>.Current  get=0x04D921A0
  System.Collections.IEnumerator.Current  get=0x0B5D9B44
METHODS:
  RVA=0x0B5D9C60  token=0x6000044  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B5D9BA0  token=0x6000045  System.Void System.IDisposable.Dispose()
  RVA=0x0B5D92E8  token=0x6000046  System.Boolean MoveNext()
  RVA=0x0B5D9C18  token=0x6000047  System.Void <>m__Finally1()
  RVA=0x0B5D9AF8  token=0x6000049  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B5D9A64  token=0x600004B  System.Collections.Generic.IEnumerator<USD.NET.Unity.ParameterInfo> System.Collections.Generic.IEnumerable<USD.NET.Unity.ParameterInfo>.GetEnumerator()
  RVA=0x0B5D9AF0  token=0x600004C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x20
FIELDS:
  public            System.Type                     inputParamType  // 0x10
  public            System.Func<System.Reflection.FieldInfo,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600004D  System.Void .ctor()
  RVA=0x0B5DB15C  token=0x600004E  System.Boolean <GetInputTextures>b__0(System.Reflection.FieldInfo info)
END_CLASS

CLASS: <GetInputTextures>d__4
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x68
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           USD.NET.Unity.ParameterInfo     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x40
  public            USD.NET.Unity.ShaderSample      <>4__this  // 0x48
  private           USD.NET.Unity.ShaderSample.<>c__DisplayClass4_0<>8__1  // 0x50
  private           System.Type                     <requireKeywordType>5__2  // 0x58
  private           System.Collections.Generic.IEnumerator<System.Reflection.FieldInfo><>7__wrap2  // 0x60
PROPERTIES:
  System.Collections.Generic.IEnumerator<USD.NET.Unity.ParameterInfo>.Current  get=0x04D921A0
  System.Collections.IEnumerator.Current  get=0x0B5DA67C
METHODS:
  RVA=0x0B5D9C60  token=0x600004F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B5DA6D8  token=0x6000050  System.Void System.IDisposable.Dispose()
  RVA=0x0B5D9C7C  token=0x6000051  System.Boolean MoveNext()
  RVA=0x0B5DA750  token=0x6000052  System.Void <>m__Finally1()
  RVA=0x0B5DA630  token=0x6000054  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B5DA59C  token=0x6000056  System.Collections.Generic.IEnumerator<USD.NET.Unity.ParameterInfo> System.Collections.Generic.IEnumerable<USD.NET.Unity.ParameterInfo>.GetEnumerator()
  RVA=0x0B5DA628  token=0x6000057  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Outputs
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x20
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            pxr.TfToken                     displacement  // 0x10
  public            pxr.TfToken                     surface  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600005B  System.Void .ctor()
END_CLASS

CLASS: Outputs
TYPE:  class
TOKEN: 0x2000029
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.Nullable<T>              result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600005D  System.Void .ctor()
END_CLASS

CLASS: WrapMode
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.TextureReaderSample.WrapModeBlack  // const
  public    static  USD.NET.Unity.TextureReaderSample.WrapModeClamp  // const
  public    static  USD.NET.Unity.TextureReaderSample.WrapModeRepeat  // const
  public    static  USD.NET.Unity.TextureReaderSample.WrapModeMirror  // const
METHODS:
END_CLASS

CLASS: SRGBMode
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  USD.NET.Unity.TextureReaderSample.SRGBModeYes  // const
  public    static  USD.NET.Unity.TextureReaderSample.SRGBModeNo  // const
  public    static  USD.NET.Unity.TextureReaderSample.SRGBModeAuto  // const
METHODS:
END_CLASS

CLASS: Outputs
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x58
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            System.Nullable<System.Single>  r  // 0x10
  public            System.Nullable<System.Single>  g  // 0x18
  public            System.Nullable<System.Single>  b  // 0x20
  public            System.Nullable<System.Single>  a  // 0x28
  public            System.Nullable<UnityEngine.Vector3>rgb  // 0x30
  public            System.Nullable<UnityEngine.Vector4>rgba  // 0x40
METHODS:
  RVA=0x041E1670  token=0x6000064  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x10
FIELDS:
  public    static readonly USD.NET.Unity.UnityTypeBindings.<>c<>9  // static @ 0x0
  public    static  USD.NET.ToVtConverter           <>9__2_0  // static @ 0x8
  public    static  USD.NET.ToCsConverter           <>9__2_1  // static @ 0x10
  public    static  USD.NET.ToVtConverter           <>9__2_2  // static @ 0x18
  public    static  USD.NET.ToCsConverter           <>9__2_3  // static @ 0x20
  public    static  USD.NET.ToVtConverter           <>9__2_4  // static @ 0x28
  public    static  USD.NET.ToCsConverter           <>9__2_5  // static @ 0x30
  public    static  USD.NET.ToVtConverter           <>9__2_6  // static @ 0x38
  public    static  USD.NET.ToCsConverter           <>9__2_7  // static @ 0x40
  public    static  USD.NET.ToVtConverter           <>9__2_8  // static @ 0x48
  public    static  USD.NET.ToCsConverter           <>9__2_9  // static @ 0x50
  public    static  USD.NET.ToVtConverter           <>9__2_10  // static @ 0x58
  public    static  USD.NET.ToCsConverter           <>9__2_11  // static @ 0x60
  public    static  USD.NET.ToVtConverter           <>9__2_12  // static @ 0x68
  public    static  USD.NET.ToCsConverter           <>9__2_13  // static @ 0x70
  public    static  USD.NET.ToVtConverter           <>9__2_14  // static @ 0x78
  public    static  USD.NET.ToCsConverter           <>9__2_15  // static @ 0x80
  public    static  USD.NET.ToVtConverter           <>9__2_16  // static @ 0x88
  public    static  USD.NET.ToCsConverter           <>9__2_17  // static @ 0x90
  public    static  USD.NET.ToVtConverter           <>9__2_18  // static @ 0x98
  public    static  USD.NET.ToCsConverter           <>9__2_19  // static @ 0xa0
METHODS:
  RVA=0x0B5DB174  token=0x6000067  System.Void .cctor()
  RVA=0x041E1670  token=0x6000068  System.Void .ctor()
  RVA=0x0B5DA798  token=0x6000069  pxr.VtValue <RegisterTypes>b__2_0(System.Object obj)
  RVA=0x0B5DAC9C  token=0x600006A  System.Object <RegisterTypes>b__2_1(pxr.VtValue value)
  RVA=0x0B5DAD18  token=0x600006B  pxr.VtValue <RegisterTypes>b__2_2(System.Object obj)
  RVA=0x0B5DAD80  token=0x600006C  System.Object <RegisterTypes>b__2_3(pxr.VtValue value)
  RVA=0x0B5DADF0  token=0x600006D  pxr.VtValue <RegisterTypes>b__2_4(System.Object obj)
  RVA=0x0B5DAE68  token=0x600006E  System.Object <RegisterTypes>b__2_5(pxr.VtValue value)
  RVA=0x0B5DAEEC  token=0x600006F  pxr.VtValue <RegisterTypes>b__2_6(System.Object obj)
  RVA=0x0B5DAF5C  token=0x6000070  System.Object <RegisterTypes>b__2_7(pxr.VtValue value)
  RVA=0x0B5DAFD8  token=0x6000071  pxr.VtValue <RegisterTypes>b__2_8(System.Object obj)
  RVA=0x0B5DB048  token=0x6000072  System.Object <RegisterTypes>b__2_9(pxr.VtValue value)
  RVA=0x0B5DA808  token=0x6000073  pxr.VtValue <RegisterTypes>b__2_10(System.Object obj)
  RVA=0x0B5DA870  token=0x6000074  System.Object <RegisterTypes>b__2_11(pxr.VtValue vtVal)
  RVA=0x0B5DA8C0  token=0x6000075  pxr.VtValue <RegisterTypes>b__2_12(System.Object obj)
  RVA=0x0B5DA930  token=0x6000076  System.Object <RegisterTypes>b__2_13(pxr.VtValue vtVal)
  RVA=0x0B5DA9AC  token=0x6000077  pxr.VtValue <RegisterTypes>b__2_14(System.Object obj)
  RVA=0x0B5DAA10  token=0x6000078  System.Object <RegisterTypes>b__2_15(pxr.VtValue vtVal)
  RVA=0x0B5DAA80  token=0x6000079  pxr.VtValue <RegisterTypes>b__2_16(System.Object obj)
  RVA=0x0B5DAAFC  token=0x600007A  System.Object <RegisterTypes>b__2_17(pxr.VtValue vtVal)
  RVA=0x0B5DAB74  token=0x600007B  pxr.VtValue <RegisterTypes>b__2_18(System.Object obj)
  RVA=0x0B5DAC00  token=0x600007C  System.Object <RegisterTypes>b__2_19(pxr.VtValue vtVal)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.String                   name  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000C8  System.Void .ctor()
  RVA=0x0B5DB0C4  token=0x60000C9  System.Boolean <HasAnySiblingsWithName>b__0(UnityEngine.GameObject sibling)
END_CLASS

CLASS: USD.NET.Unity.DiagnosticHandler
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: pxr.DiagnosticHandler
FIELDS:
METHODS:
  RVA=0x0B5D6F40  token=0x6000001  System.Void .ctor()
  RVA=0x0B5D6DD8  token=0x6000002  System.Void OnFatalError(System.String msg)
  RVA=0x0B5D6D14  token=0x6000003  System.Void OnError(System.String msg)
  RVA=0x0B5D6EDC  token=0x6000004  System.Void OnWarning(System.String msg)
  RVA=0x0B5D6E78  token=0x6000005  System.Void OnInfo(System.String msg)
END_CLASS

CLASS: USD.NET.Unity.BasisCurvesSample
TYPE:  class
TOKEN: 0x2000003
SIZE:  0xC0
EXTENDS: USD.NET.Unity.CurvesSample
FIELDS:
  public            USD.NET.Unity.BasisCurvesSample.CurveTypetype  // 0xb0
  public            USD.NET.Unity.BasisCurvesSample.Basisbasis  // 0xb4
  public            USD.NET.Unity.BasisCurvesSample.WrapModewrap  // 0xb8
METHODS:
  RVA=0x0B5D6778  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.BoundableSample
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x78
EXTENDS: USD.NET.Unity.XformableSample
FIELDS:
  public            UnityEngine.Bounds              extent  // 0x60
METHODS:
  RVA=0x0B18072C  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.CameraSample
TYPE:  class
TOKEN: 0x2000008
SIZE:  0xA0
EXTENDS: USD.NET.Unity.XformSample
FIELDS:
  public            USD.NET.Unity.CameraSample.ProjectionTypeprojection  // 0x60
  public            System.Single                   horizontalAperture  // 0x64
  public            System.Single                   horizontalApertureOffset  // 0x68
  public            System.Single                   verticalAperture  // 0x6c
  public            System.Single                   verticalApertureOffset  // 0x70
  public            System.Single                   focalLength  // 0x74
  public            UnityEngine.Vector2             clippingRange  // 0x78
  public            UnityEngine.Vector4[]           clippingPlanes  // 0x80
  public            System.Single                   fStop  // 0x88
  public            System.Single                   focusDistance  // 0x8c
  public            USD.NET.Unity.CameraSample.StereoRolestereoRole  // 0x90
  public            USD.NET.Unity.CameraSample.Shuttershutter  // 0x98
METHODS:
  RVA=0x0B18072C  token=0x6000008  System.Void .ctor()
  RVA=0x0B5D6C08  token=0x6000009  System.Void .ctor(UnityEngine.Camera fromCamera)
  RVA=0x0B5D6780  token=0x600000A  System.Void CopyFromCamera(UnityEngine.Camera camera, System.Boolean convertTransformToUsd)
  RVA=0x0B5D69B0  token=0x600000B  System.Void CopyToCamera(UnityEngine.Camera camera, System.Boolean setTransform)
END_CLASS

CLASS: USD.NET.Unity.CubeSample
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x88
EXTENDS: USD.NET.Unity.GprimSample
FIELDS:
  private           System.Double                   m_size  // 0x80
PROPERTIES:
  size  get=0x04D88140  set=0x0B5D6C68
METHODS:
  RVA=0x0B5D6778  token=0x600000D  System.Void .ctor()
  RVA=0x0B5D6C3C  token=0x600000E  System.Void .ctor(System.Double size)
END_CLASS

CLASS: USD.NET.Unity.CurvesSample
TYPE:  class
TOKEN: 0x200000D
SIZE:  0xB0
EXTENDS: USD.NET.Unity.PointBasedSample
FIELDS:
  public            System.Boolean                  doubleSided  // 0x80
  public            USD.NET.Orientation             orientation  // 0x84
  public            UnityEngine.Vector3[]           points  // 0x88
  public            UnityEngine.Vector3[]           normals  // 0x90
  public            UnityEngine.Vector3[]           velocities  // 0x98
  public            System.Int32[]                  curveVertexCounts  // 0xa0
  public            System.Single[]                 widths  // 0xa8
METHODS:
  RVA=0x0B5D6778  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.GprimSample
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x80
EXTENDS: USD.NET.Unity.BoundableSample
FIELDS:
  public            USD.NET.Primvar<UnityEngine.Color[]>colors  // 0x78
METHODS:
  RVA=0x0B5D6F48  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.ImageableSample
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x10
EXTENDS: USD.NET.SampleBase
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.MeshSample
TYPE:  class
TOKEN: 0x2000010
SIZE:  0xC0
EXTENDS: USD.NET.Unity.MeshSampleBase
FIELDS:
  public            USD.NET.Visibility              visibility  // 0xa8
  public            USD.NET.Purpose                 purpose  // 0xac
  public            System.Boolean                  doubleSided  // 0xb0
  public            USD.NET.Orientation             orientation  // 0xb4
  public            System.Int32[]                  faceVertexCounts  // 0xb8
METHODS:
  RVA=0x0B5D7E9C  token=0x6000014  System.Void SetTriangles(System.Int32[] triangleIndices)
  RVA=0x0B182660  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.MeshSampleBase
TYPE:  class
TOKEN: 0x2000011
SIZE:  0xA8
EXTENDS: USD.NET.Unity.PointBasedSample
IMPLEMENTS: USD.NET.IArbitraryPrimvars
FIELDS:
  public            System.Int32[]                  faceVertexIndices  // 0x80
  public            UnityEngine.Vector3[]           points  // 0x88
  public            UnityEngine.Vector3[]           normals  // 0x90
  public            USD.NET.Primvar<UnityEngine.Vector4[]>tangents  // 0x98
  public            System.Collections.Generic.Dictionary<System.String,USD.NET.Primvar<System.Object>>ArbitraryPrimvars  // 0xa0
METHODS:
  RVA=0x04D86210  token=0x6000016  System.Collections.Generic.Dictionary<System.String,USD.NET.Primvar<System.Object>> GetArbitraryPrimvars()
  RVA=0x0B5D7DCC  token=0x6000017  System.Void .ctor()
  RVA=0x0B5D7BEC  token=0x6000018  System.Void AddPrimvars(System.Collections.Generic.List<System.String> primvars)
END_CLASS

CLASS: USD.NET.Unity.PointBasedSample
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x80
EXTENDS: USD.NET.Unity.GprimSample
FIELDS:
METHODS:
  RVA=0x0B5D6778  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PointInstancerPrototypesSample
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x80
EXTENDS: USD.NET.Unity.BoundableSample
FIELDS:
  public            USD.NET.Relationship            prototypes  // 0x78
METHODS:
  RVA=0x0B5D8090  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PointInstancerSample
TYPE:  class
TOKEN: 0x2000014
SIZE:  0xC0
EXTENDS: USD.NET.Unity.PointInstancerPrototypesSample
FIELDS:
  public            System.Int32[]                  protoIndices  // 0x80
  public            System.Int64[]                  ids  // 0x88
  public            System.Int64[]                  invisibleIds  // 0x90
  public            UnityEngine.Vector3[]           positions  // 0x98
  public            UnityEngine.Quaternion[]        rotations  // 0xa0
  public            UnityEngine.Vector3[]           scales  // 0xa8
  public            UnityEngine.Vector3[]           velocities  // 0xb0
  public            UnityEngine.Vector3[]           angularVelocities  // 0xb8
METHODS:
  RVA=0x0B5D80E8  token=0x600001B  UnityEngine.Matrix4x4[] ComputeInstanceMatrices(USD.NET.Scene scene, System.String primPath)
  RVA=0x0B5D833C  token=0x600001C  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.ScopeSample
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x10
EXTENDS: USD.NET.Unity.ImageableSample
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001D  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.SphereSample
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x88
EXTENDS: USD.NET.Unity.GprimSample
FIELDS:
  private           System.Double                   m_radius  // 0x80
PROPERTIES:
  radius  get=0x04D88140  set=0x0B5D8840
METHODS:
  RVA=0x0B5D6778  token=0x600001E  System.Void .ctor()
  RVA=0x0B5D6C3C  token=0x600001F  System.Void .ctor(System.Double radius)
END_CLASS

CLASS: USD.NET.Unity.XformableSample
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x60
EXTENDS: USD.NET.Unity.ImageableSample
FIELDS:
  private   readonly System.String[]                 kXformOpTransform  // 0x10
  private           UnityEngine.Matrix4x4           m_xf  // 0x18
  public            System.String[]                 xformOpOrder  // 0x58
PROPERTIES:
  transform  get=0x04D978B0  set=0x0B5E1780
METHODS:
  RVA=0x0B5E1588  token=0x6000022  USD.NET.Unity.XformableSample FromTransform(UnityEngine.Transform transform)
  RVA=0x0B5E16C0  token=0x6000023  System.Void .ctor()
  RVA=0x0B5E14C4  token=0x6000026  System.Void ConvertTransform()
END_CLASS

CLASS: USD.NET.Unity.XformSample
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x60
EXTENDS: USD.NET.Unity.XformableSample
FIELDS:
METHODS:
  RVA=0x0B18072C  token=0x6000027  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.MaterialBindingSample
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            USD.NET.Relationship            binding  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000028  System.Void .ctor()
  RVA=0x0B5D711C  token=0x6000029  System.Void .ctor(System.String materialPath)
END_CLASS

CLASS: USD.NET.Unity.MaterialSample
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x28
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            USD.NET.Connectable<pxr.TfToken>surface  // 0x10
  public            USD.NET.Connectable<pxr.TfToken>displacement  // 0x18
  public            System.String[]                 requiredKeywords  // 0x20
METHODS:
  RVA=0x0B5D7AA4  token=0x600002A  System.Void .ctor()
  RVA=0x0B5D79FC  token=0x600002B  System.Void .ctor(System.String surfacePath)
  RVA=0x0B5D7B1C  token=0x600002C  System.Void .ctor(System.String surfacePath, System.String displacementPath)
  RVA=0x0B5D741C  token=0x600002D  System.Boolean ReadMaterial(USD.NET.Scene scene, System.String geometryPath, USD.NET.Unity.MaterialSample materialSample, System.String& shaderId)
  RVA=0x0B5D7194  token=0x600002E  System.Void Bind(USD.NET.Scene scene, System.String primPath, System.String materialPath)
  RVA=0x0B5D78A4  token=0x600002F  System.Void Unbind(USD.NET.Scene scene, System.String primPath)
END_CLASS

CLASS: USD.NET.Unity.ParameterInfo
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x38
FIELDS:
  public            System.Object                   value  // 0x10
  public            System.String                   connectedPath  // 0x18
  public            System.String                   usdName  // 0x20
  public            System.String                   unityName  // 0x28
  public            System.String[]                 requiredShaderKeywords  // 0x30
METHODS:
  RVA=0x0B5D7F58  token=0x6000030  System.String ToString()
END_CLASS

CLASS: USD.NET.Unity.InputParameterAttribute
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
EXTENDS: USD.NET.UsdNamespaceAttribute
FIELDS:
  private           System.String                   <UnityName>k__BackingField  // 0x18
PROPERTIES:
  UnityName  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0B5D700C  token=0x6000033  System.Void .ctor()
  RVA=0x0B5D6FB4  token=0x6000034  System.Void .ctor(System.String unityName)
END_CLASS

CLASS: USD.NET.Unity.InputTextureAttribute
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x20
EXTENDS: USD.NET.UsdNamespaceAttribute
FIELDS:
  private           System.String                   <UnityName>k__BackingField  // 0x18
PROPERTIES:
  UnityName  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0B5D7068  token=0x6000037  System.Void .ctor()
  RVA=0x0B5D70C4  token=0x6000038  System.Void .ctor(System.String unityName)
END_CLASS

CLASS: USD.NET.Unity.RequireShaderKeywordsAttribute
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.String[]                 <Keywords>k__BackingField  // 0x10
PROPERTIES:
  Keywords  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x0B5D86C4  token=0x600003B  System.Void .ctor(System.String keyword)
  RVA=0x053908C0  token=0x600003C  System.Void .ctor(System.String[] keywords)
END_CLASS

CLASS: USD.NET.Unity.ShaderSample
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x18
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            pxr.TfToken                     id  // 0x10
METHODS:
  RVA=0x05390BB0  token=0x600003D  System.Type GetClassType()
  RVA=0x0B5D8820  token=0x600003E  System.Object GetValue(System.Reflection.FieldInfo info)
  RVA=0x0B5D8740  token=0x600003F  System.Collections.Generic.IEnumerable<USD.NET.Unity.ParameterInfo> GetInputParameters()
  RVA=0x0B5D87B0  token=0x6000040  System.Collections.Generic.IEnumerable<USD.NET.Unity.ParameterInfo> GetInputTextures()
  RVA=0x041E1670  token=0x6000041  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.StandardShaderSample
TYPE:  class
TOKEN: 0x2000024
SIZE:  0xF8
EXTENDS: USD.NET.Unity.ShaderSample
FIELDS:
  public            System.Boolean                  enableGpuInstancing  // 0x18
  public            USD.NET.Connectable<UnityEngine.Color>albedo  // 0x20
  public            USD.NET.Connectable<UnityEngine.Color>albedoMap  // 0x28
  public            USD.NET.Connectable<System.Single>cutoff  // 0x30
  public            USD.NET.Connectable<System.Single>smoothness  // 0x38
  public            USD.NET.Connectable<System.Single>smoothnessScale  // 0x40
  public            USD.NET.Connectable<System.Single>smoothnessTextureChannel  // 0x48
  public            USD.NET.Connectable<System.Single>metallicMap  // 0x50
  public            USD.NET.Connectable<System.Single>metallicScale  // 0x58
  public            USD.NET.Connectable<System.Boolean>enableSpecularHighlights  // 0x60
  public            USD.NET.Connectable<System.Boolean>enableGlossyReflections  // 0x68
  public            USD.NET.Connectable<UnityEngine.Color>normalMap  // 0x70
  public            USD.NET.Connectable<System.Single>normalMapScale  // 0x78
  public            USD.NET.Connectable<UnityEngine.Color>parallaxMap  // 0x80
  public            USD.NET.Connectable<System.Single>parallaxMapScale  // 0x88
  public            USD.NET.Connectable<System.Single>occlusionMap  // 0x90
  public            USD.NET.Connectable<System.Single>occlusionMapScale  // 0x98
  public            USD.NET.Connectable<UnityEngine.Color>emission  // 0xa0
  public            USD.NET.Connectable<UnityEngine.Color>emissionMap  // 0xa8
  public            USD.NET.Connectable<UnityEngine.Color>detailMask  // 0xb0
  public            USD.NET.Connectable<UnityEngine.Color>detailAlbedoMap  // 0xb8
  public            USD.NET.Connectable<UnityEngine.Color>detailNormalMap  // 0xc0
  public            USD.NET.Connectable<System.Single>detailNormalMapScale  // 0xc8
  public            USD.NET.Connectable<System.Single>uvSetForSecondaryTextures  // 0xd0
  public            USD.NET.Connectable<System.Single>renderingMode  // 0xd8
  public            USD.NET.Connectable<System.Single>srcBlend  // 0xe0
  public            USD.NET.Connectable<System.Single>dstBlend  // 0xe8
  public            USD.NET.Connectable<System.Single>zwrite  // 0xf0
METHODS:
  RVA=0x0B5D8910  token=0x6000058  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.Texture2DSample
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x20
EXTENDS: USD.NET.SampleBase
FIELDS:
  public            USD.NET.Connectable<System.String>sourceFile  // 0x10
  public            System.Boolean                  sRgb  // 0x18
METHODS:
  RVA=0x0B5D8E38  token=0x6000059  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PreviewSurfaceSample
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x90
EXTENDS: USD.NET.Unity.ShaderSample
FIELDS:
  public            USD.NET.Connectable<UnityEngine.Vector3>diffuseColor  // 0x18
  public            USD.NET.Connectable<UnityEngine.Vector3>emissiveColor  // 0x20
  public            USD.NET.Connectable<System.Int32>useSpecularWorkflow  // 0x28
  public            USD.NET.Connectable<UnityEngine.Vector3>specularColor  // 0x30
  public            USD.NET.Connectable<System.Single>metallic  // 0x38
  public            USD.NET.Connectable<System.Single>roughness  // 0x40
  public            USD.NET.Connectable<System.Single>clearcoat  // 0x48
  public            USD.NET.Connectable<System.Single>clearcoatRoughness  // 0x50
  public            USD.NET.Connectable<System.Single>opacity  // 0x58
  public            USD.NET.Connectable<System.Single>opacityThreshold  // 0x60
  public            USD.NET.Connectable<System.Single>ior  // 0x68
  public            USD.NET.Connectable<UnityEngine.Vector3>normal  // 0x70
  public            USD.NET.Connectable<System.Single>displacement  // 0x78
  public            USD.NET.Connectable<System.Single>occlusion  // 0x80
  public            USD.NET.Unity.PreviewSurfaceSample.Outputsoutputs  // 0x88
METHODS:
  RVA=0x0B5D8344  token=0x600005A  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PrimvarReaderSample`1
TYPE:  class
TOKEN: 0x2000028
EXTENDS: USD.NET.Unity.ShaderSample
FIELDS:
  public            USD.NET.Connectable<T>          fallback  // 0x0
  public            USD.NET.Unity.PrimvarReaderSample.Outputs<T>outputs  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600005C  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PrimvarReaderImportSample`1
TYPE:  class
TOKEN: 0x200002A
EXTENDS: USD.NET.Unity.PrimvarReaderSample`1
FIELDS:
  public            USD.NET.Connectable<System.String>varname  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600005E  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.PrimvarReaderExportSample`1
TYPE:  class
TOKEN: 0x200002B
EXTENDS: USD.NET.Unity.PrimvarReaderSample`1
FIELDS:
  public            USD.NET.Connectable<pxr.TfToken>varname  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600005F  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.TextureReaderSample
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x60
EXTENDS: USD.NET.Unity.ShaderSample
FIELDS:
  public            USD.NET.Connectable<pxr.SdfAssetPath>file  // 0x18
  public            USD.NET.Connectable<UnityEngine.Vector2>st  // 0x20
  public            USD.NET.Connectable<USD.NET.Unity.TextureReaderSample.WrapMode>wrapS  // 0x28
  public            USD.NET.Connectable<USD.NET.Unity.TextureReaderSample.WrapMode>wrapT  // 0x30
  public            USD.NET.Connectable<UnityEngine.Vector4>fallback  // 0x38
  public            USD.NET.Connectable<UnityEngine.Vector4>scale  // 0x40
  public            USD.NET.Connectable<UnityEngine.Vector4>bias  // 0x48
  public            USD.NET.Connectable<USD.NET.Unity.TextureReaderSample.SRGBMode>isSRGB  // 0x50
  public            USD.NET.Unity.TextureReaderSample.Outputsoutputs  // 0x58
METHODS:
  RVA=0x0B5D8EBC  token=0x6000060  System.Void .ctor()
  RVA=0x0B5D9260  token=0x6000061  System.Void .ctor(System.String filePath)
  RVA=0x0B5D919C  token=0x6000062  System.Void .ctor(System.String filePath, System.String stConnectionPath)
  RVA=0x0B5D8E94  token=0x6000063  USD.NET.Unity.TextureReaderSample.WrapMode GetWrapMode(UnityEngine.TextureWrapMode wrap)
END_CLASS

CLASS: USD.NET.Unity.UnityTypeBindings
TYPE:  static class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  isInitialized  // static @ 0x0
METHODS:
  RVA=0x0B5DCAF0  token=0x6000065  System.Void .cctor()
  RVA=0x0B5DB1D8  token=0x6000066  System.Void RegisterTypes()
END_CLASS

CLASS: USD.NET.Unity.PreserveAttribute
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600007D  System.Void .ctor()
END_CLASS

CLASS: USD.NET.Unity.UnityTypeConverter
TYPE:  class
TOKEN: 0x2000033
SIZE:  0x10
EXTENDS: USD.NET.IntrinsicTypeConverter
FIELDS:
  public    static  UnityEngine.Matrix4x4           basisChange  // static @ 0x0
  public    static  UnityEngine.Matrix4x4           inverseBasisChange  // static @ 0x40
METHODS:
  RVA=0x0B5DD0B4  token=0x600007E  UnityEngine.Matrix4x4 ChangeBasis(UnityEngine.Matrix4x4 input)
  RVA=0x0B5DD02C  token=0x600007F  UnityEngine.Vector3 ChangeBasis(UnityEngine.Vector3 point)
  RVA=0x0B5E002C  token=0x6000080  System.Void SetTransform(UnityEngine.Matrix4x4 localXf, UnityEngine.Transform transform)
  RVA=0x0B5DD604  token=0x6000081  System.Boolean Decompose(UnityEngine.Matrix4x4 matrix, UnityEngine.Vector3& translation, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B5E1334  token=0x6000082  UnityEngine.Vector3 WeightedAvg(UnityEngine.Vector3 a, UnityEngine.Vector3 b, System.Single aWeight, System.Single bWeight)
  RVA=0x0B5DE56C  token=0x6000083  System.Void ExtractTrs(UnityEngine.Matrix4x4 transform, UnityEngine.Vector3& localPosition, UnityEngine.Quaternion& localRotation, UnityEngine.Vector3& localScale)
  RVA=0x0B5DE3F0  token=0x6000084  UnityEngine.Quaternion ExtractRotation(UnityEngine.Matrix4x4 mat4)
  RVA=0x04DBF4E0  token=0x6000085  UnityEngine.Vector3 ExtractPosition(UnityEngine.Matrix4x4 mat4)
  RVA=0x0B5DE480  token=0x6000086  UnityEngine.Vector3 ExtractScale(UnityEngine.Matrix4x4 mat4)
  RVA=0x0B5DF430  token=0x6000087  System.Boolean HasAnySiblingsWithName(UnityEngine.Transform transform, System.String name)
  RVA=0x0B5DF374  token=0x6000088  System.String GetUniqueName(UnityEngine.Transform transform)
  RVA=0x0B5DF328  token=0x6000089  System.String GetPath(UnityEngine.Transform unityObj)
  RVA=0x0B5DF160  token=0x600008A  System.String GetPath(UnityEngine.Transform unityObj, UnityEngine.Transform unityObjRoot)
  RVA=0x0B5DF07C  token=0x600008B  UnityEngine.Matrix4x4 GetLocalToParentXf(UnityEngine.Transform unityXf)
  RVA=0x0B5E01D4  token=0x600008C  pxr.GfMatrix4d ToGfMatrix(UnityEngine.Transform unityXf)
  RVA=0x0B5E0250  token=0x600008D  pxr.GfMatrix4d ToGfMatrix(UnityEngine.Matrix4x4 unityMat4)
  RVA=0x0B5DE688  token=0x600008E  UnityEngine.Matrix4x4 FromMatrix(pxr.GfMatrix4d gfMat)
  RVA=0x0B5E08C0  token=0x600008F  pxr.VtMatrix4dArray ToVtArray(UnityEngine.Matrix4x4[] input)
  RVA=0x0B5DECF0  token=0x6000090  UnityEngine.Matrix4x4[] FromVtArray(pxr.VtMatrix4dArray input)
  RVA=0x0B5DFE14  token=0x6000091  pxr.VtMatrix4dArray ListToVtArray(System.Collections.Generic.List<UnityEngine.Matrix4x4> input)
  RVA=0x0B5DF7B8  token=0x6000092  System.Collections.Generic.List<UnityEngine.Matrix4x4> ListFromVtArray(pxr.VtMatrix4dArray input)
  RVA=0x0B5DD384  token=0x6000093  pxr.GfVec4f Color32ToVec4f(UnityEngine.Color32 c)
  RVA=0x0B5E10EC  token=0x6000094  UnityEngine.Color32 Vec4fToColor32(pxr.GfVec4f v)
  RVA=0x0B5E0C9C  token=0x6000095  pxr.VtVec4fArray ToVtArray(System.Collections.Generic.List<UnityEngine.Color32> input)
  RVA=0x0B5E0B80  token=0x6000096  System.Void ToVtArray(System.Collections.Generic.List<UnityEngine.Color32> input, pxr.VtVec3fArray& rgb, pxr.VtFloatArray& alpha)
  RVA=0x0B5E0EF4  token=0x6000097  pxr.VtVec4fArray ToVtArray(UnityEngine.Color32[] input)
  RVA=0x0B5DD200  token=0x6000098  UnityEngine.Color32[] Color32FromVtArray(pxr.VtVec4fArray input)
  RVA=0x0B5DD58C  token=0x6000099  pxr.GfVec4f ColorToVec4f(UnityEngine.Color c)
  RVA=0x0B5E1148  token=0x600009A  UnityEngine.Color Vec4fToColor(pxr.GfVec4f v)
  RVA=0x0B5E0544  token=0x600009B  pxr.VtVec4fArray ToVtArray(System.Collections.Generic.List<UnityEngine.Color> input)
  RVA=0x0B5E0714  token=0x600009C  System.Void ToVtArray(System.Collections.Generic.List<UnityEngine.Color> input, pxr.VtVec3fArray& rgb, pxr.VtFloatArray& alpha)
  RVA=0x0B5E0684  token=0x600009D  pxr.VtVec4fArray ToVtArray(UnityEngine.Color[] input)
  RVA=0x0B5DD3E8  token=0x600009E  UnityEngine.Color[] ColorFromVtArray(pxr.VtVec4fArray input)
  RVA=0x0B5DD498  token=0x600009F  UnityEngine.Color[] ColorFromVtArray(pxr.VtVec4fArray input, UnityEngine.Color[]& output)
  RVA=0x0B5E0D08  token=0x60000A0  System.Void ToVtArray(UnityEngine.Color32[] input, pxr.VtVec3fArray& rgb, pxr.VtFloatArray& alpha)
  RVA=0x0B5DE1A8  token=0x60000A1  UnityEngine.Vector3[] ExtractRgb(UnityEngine.Color32[] colors)
  RVA=0x0B5DE020  token=0x60000A2  System.Single[] ExtractAlpha(UnityEngine.Color32[] colors)
  RVA=0x0B5E09BC  token=0x60000A3  System.Void ToVtArray(UnityEngine.Color[] input, pxr.VtVec3fArray& rgb, pxr.VtFloatArray& alpha)
  RVA=0x0B5DEE1C  token=0x60000A4  UnityEngine.Color[] FromVtArray(pxr.VtVec3fArray rgbIn, pxr.VtFloatArray alphaIn)
  RVA=0x0B5DE2EC  token=0x60000A5  UnityEngine.Vector3[] ExtractRgb(UnityEngine.Color[] colors)
  RVA=0x0B5DE0EC  token=0x60000A6  System.Single[] ExtractAlpha(UnityEngine.Color[] colors)
  RVA=0x0B5E0138  token=0x60000A7  System.Void SwapQuaternionReal(UnityEngine.Quaternion[]& input)
  RVA=0x0B5DFE80  token=0x60000A8  pxr.GfQuatf QuaternionToQuatf(UnityEngine.Quaternion quaternion)
  RVA=0x0B5DFEF8  token=0x60000A9  UnityEngine.Quaternion QuatfToQuaternion(pxr.GfQuatf quat)
  RVA=0x0B5E05B0  token=0x60000AA  pxr.VtQuatfArray ToVtArray(UnityEngine.Quaternion[] input)
  RVA=0x0B5DE9A4  token=0x60000AB  UnityEngine.Quaternion[] FromVtArray(pxr.VtQuatfArray input)
  RVA=0x0B5DFDA8  token=0x60000AC  pxr.VtQuatfArray ListToVtArray(System.Collections.Generic.List<UnityEngine.Quaternion> input)
  RVA=0x0B5DFB6C  token=0x60000AD  System.Collections.Generic.List<UnityEngine.Quaternion> ListFromVtArray(pxr.VtQuatfArray input)
  RVA=0x0B5E07A0  token=0x60000AE  pxr.VtVec4fArray ToVtArray(UnityEngine.Vector4[] input)
  RVA=0x0B5DEB78  token=0x60000AF  UnityEngine.Vector4[] FromVtArray(pxr.VtVec4fArray input)
  RVA=0x0B5DFCD0  token=0x60000B0  pxr.VtVec4fArray ListToVtArray(System.Collections.Generic.List<UnityEngine.Vector4> input)
  RVA=0x0B5DF8FC  token=0x60000B1  System.Collections.Generic.List<UnityEngine.Vector4> ListFromVtArray(pxr.VtVec4fArray input)
  RVA=0x0B5DCE6C  token=0x60000B2  pxr.VtVec3fArray BoundsToVtArray(UnityEngine.Bounds input)
  RVA=0x0B5DCBF8  token=0x60000B3  UnityEngine.Bounds BoundsFromVtArray(pxr.VtValue vtBounds)
  RVA=0x0B5DCAF8  token=0x60000B4  UnityEngine.Bounds BoundsFromVtArray(pxr.VtValue vtBounds, UnityEngine.Vector3[] bbox)
  RVA=0x0B5E0830  token=0x60000B5  pxr.VtVec3fArray ToVtArray(UnityEngine.Vector3[] input)
  RVA=0x0B5DEFC4  token=0x60000B6  UnityEngine.Vector3[] FromVtArray(pxr.VtVec3fArray input)
  RVA=0x0B5DEA88  token=0x60000B7  System.Void FromVtArray(pxr.VtVec3fArray input, UnityEngine.Vector3[]& output)
  RVA=0x0B5DFC64  token=0x60000B8  pxr.VtVec3fArray ListToVtArray(System.Collections.Generic.List<UnityEngine.Vector3> input)
  RVA=0x0B5DF9CC  token=0x60000B9  System.Collections.Generic.List<UnityEngine.Vector3> ListFromVtArray(pxr.VtVec3fArray input)
  RVA=0x0B5E0C0C  token=0x60000BA  pxr.VtVec2fArray ToVtArray(UnityEngine.Vector2[] input)
  RVA=0x0B5DEC34  token=0x60000BB  UnityEngine.Vector2[] FromVtArray(pxr.VtVec2fArray input)
  RVA=0x0B5DFD3C  token=0x60000BC  pxr.VtVec2fArray ListToVtArray(System.Collections.Generic.List<UnityEngine.Vector2> input)
  RVA=0x0B5DFA9C  token=0x60000BD  System.Collections.Generic.List<UnityEngine.Vector2> ListFromVtArray(pxr.VtVec2fArray input)
  RVA=0x0B5E11E8  token=0x60000BE  pxr.GfVec2f Vector2ToVec2f(UnityEngine.Vector2 vec2)
  RVA=0x0B5E0FE0  token=0x60000BF  UnityEngine.Vector2 Vec2fToVector2(pxr.GfVec2f value)
  RVA=0x0B5E124C  token=0x60000C0  pxr.GfVec3f Vector3ToVec3f(UnityEngine.Vector3 vec3)
  RVA=0x0B5E1070  token=0x60000C1  UnityEngine.Vector3 Vec3fToVector3(pxr.GfVec3f v3)
  RVA=0x0B5E12BC  token=0x60000C2  pxr.GfVec4f Vector4ToVec4f(UnityEngine.Vector4 vector4)
  RVA=0x0B5E1148  token=0x60000C3  UnityEngine.Vector4 Vec4fToVector4(pxr.GfVec4f v4)
  RVA=0x0B5DFFB4  token=0x60000C4  pxr.GfVec4f RectToVtVec4(UnityEngine.Rect rect)
  RVA=0x0B5E1148  token=0x60000C5  UnityEngine.Rect Vec4fToRect(pxr.GfVec4f v4)
  RVA=0x041E1670  token=0x60000C6  System.Void .ctor()
  RVA=0x0B5E13D0  token=0x60000C7  System.Void .cctor()
END_CLASS

