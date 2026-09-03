// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Splines.dll
// Classes:  37
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

CLASS: FrenetFrame
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x40
FIELDS:
  public            Unity.Mathematics.float3        origin  // 0x10
  public            Unity.Mathematics.float3        tangent  // 0x1c
  public            Unity.Mathematics.float3        normal  // 0x28
  public            Unity.Mathematics.float3        binormal  // 0x34
METHODS:
END_CLASS

CLASS: KnotLink
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x18
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  public            UnityEngine.Splines.SplineKnotIndex[]Knots  // 0x10
PROPERTIES:
  Count  get=0x067B6CAC
  Item  get=0x0B2F072C
METHODS:
  RVA=0x0B2F06E4  token=0x6000036  System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineKnotIndex> GetEnumerator()
  RVA=0x0AFBA390  token=0x6000037  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x041E1670  token=0x600003A  System.Void .ctor()
END_CLASS

CLASS: Slice`1
TYPE:  sealed struct
TOKEN: 0x2000013
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private           Unity.Collections.NativeSlice<T>m_Slice  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000047  System.Void .ctor(Unity.Collections.NativeArray<T> array, System.Int32 start, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000048  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000049  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: MetaData
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x20
FIELDS:
  public            UnityEngine.Splines.TangentMode Mode  // 0x10
  public            System.Single                   Tension  // 0x14
  private           UnityEngine.Splines.DistanceToInterpolation[]m_DistanceToInterpolation  // 0x18
PROPERTIES:
  DistanceToInterpolation  get=0x03155AD0
METHODS:
  RVA=0x031575E0  token=0x6000074  System.Void .ctor()
  RVA=0x03157550  token=0x6000075  System.Void InvalidateCache()
END_CLASS

CLASS: <get_embeddedSplineData>d__15
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Splines.ISplineModificationHandler<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            UnityEngine.Splines.Spline      <>4__this  // 0x28
  private           System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineDataKeyValuePair<System.Int32>><>7__wrap1  // 0x30
  private           System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineDataKeyValuePair<System.Single>><>7__wrap2  // 0x38
  private           System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineDataKeyValuePair<Unity.Mathematics.float4>><>7__wrap3  // 0x40
  private           System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineDataKeyValuePair<UnityEngine.Object>><>7__wrap4  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Splines.ISplineModificationHandler>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000076  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B2F4C7C  token=0x6000077  System.Void System.IDisposable.Dispose()
  RVA=0x0B2F4350  token=0x6000078  System.Boolean MoveNext()
  RVA=0x0B2F4E4C  token=0x6000079  System.Void <>m__Finally1()
  RVA=0x0B2F4E94  token=0x600007A  System.Void <>m__Finally2()
  RVA=0x0B2F4EDC  token=0x600007B  System.Void <>m__Finally3()
  RVA=0x0B2F4F24  token=0x600007C  System.Void <>m__Finally4()
  RVA=0x0B2F4C30  token=0x600007E  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B2F4B9C  token=0x6000080  System.Collections.Generic.IEnumerator<UnityEngine.Splines.ISplineModificationHandler> System.Collections.Generic.IEnumerable<UnityEngine.Splines.ISplineModificationHandler>.GetEnumerator()
  RVA=0x0B2F4C28  token=0x6000081  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass18_0
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.IReadOnlyList<UnityEngine.Splines.Spline>value  // 0x10
  public            System.Int32                    i  // 0x18
  public            System.Predicate<UnityEngine.Splines.Spline><>9__0  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000A9  System.Void .ctor()
  RVA=0x0B2F42F8  token=0x60000AA  System.Boolean <set_Splines>b__0(UnityEngine.Splines.Spline spline)
END_CLASS

CLASS: <GetEnumerator>d__46
TYPE:  sealed class
TOKEN: 0x200001C
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           UnityEngine.Splines.DataPoint<T><>2__current  // 0x0
  public            UnityEngine.Splines.SplineData<T><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
  private           System.Int32                    <c>5__3  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Splines.DataPoint<T>>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000B2  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000B3  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000B4  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000B6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Segment
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x18
FIELDS:
  public            System.Single                   start  // 0x10
  public            System.Single                   length  // 0x14
METHODS:
  RVA=0x04D87A10  token=0x60000DD  System.Void .ctor(System.Single start, System.Single length)
END_CLASS

CLASS: UnityEngine.Splines.ArrayUtility
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000001  System.Void RemoveAt(T[]& array, System.Int32 index)
  RVA=-1  // generic def  token=0x6000002  System.Void Remove(T[]& array, T element)
END_CLASS

CLASS: UnityEngine.Splines.BezierCurve
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.float3        P0  // 0x10
  public            Unity.Mathematics.float3        P1  // 0x1c
  public            Unity.Mathematics.float3        P2  // 0x28
  public            Unity.Mathematics.float3        P3  // 0x34
METHODS:
  RVA=0x04DB65D0  token=0x6000003  System.Void .ctor(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, Unity.Mathematics.float3 p3)
  RVA=0x03C29DD0  token=0x6000004  System.Void .ctor(UnityEngine.Splines.BezierKnot a, UnityEngine.Splines.BezierKnot b)
  RVA=0x0B28C160  token=0x6000005  System.Boolean Equals(UnityEngine.Splines.BezierCurve other)
  RVA=0x0B2EE70C  token=0x6000006  System.Boolean Equals(System.Object obj)
  RVA=0x0B2EE78C  token=0x6000007  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Splines.BezierKnot
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x48
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.float3        Position  // 0x10
  public            Unity.Mathematics.float3        TangentIn  // 0x1c
  public            Unity.Mathematics.float3        TangentOut  // 0x28
  public            Unity.Mathematics.quaternion    Rotation  // 0x34
  public            System.Single                   Width  // 0x44
METHODS:
  RVA=0x0B2EF15C  token=0x6000008  System.Void .ctor(Unity.Mathematics.float3 position)
  RVA=0x04DB9900  token=0x6000009  System.Void .ctor(Unity.Mathematics.float3 position, Unity.Mathematics.float3 tangentIn, Unity.Mathematics.float3 tangentOut, Unity.Mathematics.quaternion rotation, System.Single width)
  RVA=0x0B2EEED0  token=0x600000A  UnityEngine.Splines.BezierKnot Transform(Unity.Mathematics.float4x4 matrix)
  RVA=0x0B2EE81C  token=0x600000B  UnityEngine.Splines.BezierKnot BakeTangentDirectionToRotation(System.Boolean mirrored, UnityEngine.Splines.BezierTangent main)
  RVA=0x041E1670  token=0x600000C  System.Void OnBeforeSerialize()
  RVA=0x045E25C0  token=0x600000D  System.Void OnAfterDeserialize()
  RVA=0x0B2EED44  token=0x600000E  System.String ToString()
  RVA=0x0B2EEB20  token=0x600000F  System.Boolean Equals(UnityEngine.Splines.BezierKnot other)
  RVA=0x0B2EEBF4  token=0x6000010  System.Boolean Equals(System.Object obj)
  RVA=0x0B2EEC80  token=0x6000011  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Splines.TangentMode
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Splines.TangentMode AutoSmooth  // const
  public    static  UnityEngine.Splines.TangentMode Linear  // const
  public    static  UnityEngine.Splines.TangentMode Mirrored  // const
  public    static  UnityEngine.Splines.TangentMode Continuous  // const
  public    static  UnityEngine.Splines.TangentMode Broken  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.BezierTangent
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Splines.BezierTangentIn  // const
  public    static  UnityEngine.Splines.BezierTangentOut  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.CurveUtility
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  private   static readonly UnityEngine.Splines.DistanceToInterpolation[]k_DistanceLUT  // static @ 0x0
METHODS:
  RVA=0x030E7670  token=0x6000012  Unity.Mathematics.float3 EvaluatePosition(UnityEngine.Splines.BezierCurve curve, System.Single t)
  RVA=0x03280B50  token=0x6000013  Unity.Mathematics.float3 EvaluateTangent(UnityEngine.Splines.BezierCurve curve, System.Single t)
  RVA=0x0B2EF244  token=0x6000014  Unity.Mathematics.float3 EvaluateAcceleration(UnityEngine.Splines.BezierCurve curve, System.Single t)
  RVA=0x034C8110  token=0x6000015  System.Void CalculateCurveLengths(UnityEngine.Splines.BezierCurve curve, UnityEngine.Splines.DistanceToInterpolation[] lookupTable)
  RVA=0x0B2EF1E4  token=0x6000016  System.Boolean Approximately(System.Single a, System.Single b)
  RVA=0x0B2EF3C8  token=0x6000017  Unity.Mathematics.float3 EvaluateUpVector(UnityEngine.Splines.BezierCurve curve, System.Single t, Unity.Mathematics.float3 startUp, Unity.Mathematics.float3 endUp)
  RVA=0x0B2EFEB8  token=0x6000018  UnityEngine.Splines.CurveUtility.FrenetFrame GetNextRotationMinimizingFrame(UnityEngine.Splines.BezierCurve curve, UnityEngine.Splines.CurveUtility.FrenetFrame previousRMFrame, System.Single nextRMFrameT)
  RVA=-1  // generic def  token=0x6000019  System.Single GetDistanceToInterpolation(T lut, System.Single distance)
  RVA=0x04D388A0  token=0x600001A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Splines.IDataPoint
TYPE:  interface
TOKEN: 0x2000009
FIELDS:
PROPERTIES:
  Index  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.DataPoint`1
TYPE:  sealed struct
TOKEN: 0x200000A
IMPLEMENTS: System.IComparable`1 System.IComparable`1 UnityEngine.Splines.IDataPoint
FIELDS:
  private           System.Single                   m_Index  // 0x0
  private           TDataType                       m_Value  // 0x0
PROPERTIES:
  Index  get=-1  // not resolved  set=-1  // not resolved
  Value  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600001F  System.Int32 CompareTo(UnityEngine.Splines.DataPoint<TDataType> other)
  RVA=-1  // not resolved  token=0x6000020  System.Int32 CompareTo(System.Single other)
  RVA=-1  // not resolved  token=0x6000021  System.String ToString()
END_CLASS

CLASS: UnityEngine.Splines.DataPointComparer`1
TYPE:  class
TOKEN: 0x200000B
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000022  System.Int32 Compare(T x, T y)
  RVA=-1  // not resolved  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Splines.DistanceToInterpolation
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x18
FIELDS:
  public            System.Single                   Distance  // 0x10
  public            System.Single                   T  // 0x14
  private   static readonly UnityEngine.Splines.DistanceToInterpolationInvalid  // static @ 0x0
METHODS:
  RVA=0x04D758C0  token=0x6000024  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Splines.IHasEmptyCurves
TYPE:  interface
TOKEN: 0x200000D
FIELDS:
PROPERTIES:
  EmptyCurves  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.ISpline
TYPE:  interface
TOKEN: 0x200000E
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
PROPERTIES:
  Closed  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000027  System.Single GetLength()
  RVA=-1  // abstract  token=0x6000028  UnityEngine.Splines.BezierCurve GetCurve(System.Int32 index)
  RVA=-1  // abstract  token=0x6000029  System.Single GetCurveLength(System.Int32 index)
  RVA=-1  // abstract  token=0x600002A  System.Single GetCurveInterpolation(System.Int32 curveIndex, System.Single curveDistance)
END_CLASS

CLASS: UnityEngine.Splines.ISplineContainer
TYPE:  interface
TOKEN: 0x200000F
FIELDS:
PROPERTIES:
  Splines  get=-1  // abstract
  KnotLinkCollection  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.KnotLinkCollection
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
FIELDS:
  private           UnityEngine.Splines.KnotLinkCollection.KnotLink[]m_KnotsLink  // 0x10
METHODS:
  RVA=0x0B2F0410  token=0x600002D  UnityEngine.Splines.KnotLinkCollection.KnotLink GetKnotLinksInternal(UnityEngine.Splines.SplineKnotIndex index)
  RVA=0x0B2F05FC  token=0x600002E  System.Boolean TryGetKnotLinks(UnityEngine.Splines.SplineKnotIndex knotIndex, System.Collections.Generic.IReadOnlyList<UnityEngine.Splines.SplineKnotIndex>& linkedKnots)
  RVA=0x0B2F0628  token=0x600002F  System.Void Unlink(UnityEngine.Splines.SplineKnotIndex knot)
  RVA=0x0B2F04DC  token=0x6000030  System.Void KnotRemoved(System.Int32 splineIndex, System.Int32 knotIndex)
  RVA=0x0B2F04FC  token=0x6000031  System.Void KnotRemoved(UnityEngine.Splines.SplineKnotIndex index)
  RVA=0x0B2F04A8  token=0x6000032  System.Void KnotInserted(System.Int32 splineIndex, System.Int32 knotIndex)
  RVA=0x0B2F04CC  token=0x6000033  System.Void KnotInserted(UnityEngine.Splines.SplineKnotIndex index)
  RVA=0x0B2F0534  token=0x6000034  System.Void ShiftKnotIndices(UnityEngine.Splines.SplineKnotIndex index, System.Int32 offset)
  RVA=0x03C2C120  token=0x6000035  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Splines.NativeSpline
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x48
IMPLEMENTS: UnityEngine.Splines.ISpline System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1 System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<UnityEngine.Splines.BezierKnot>m_Knots  // 0x10
  private           Unity.Collections.NativeArray<UnityEngine.Splines.BezierCurve>m_Curves  // 0x20
  private           Unity.Collections.NativeArray<UnityEngine.Splines.DistanceToInterpolation>m_SegmentLengthsLookupTable  // 0x30
  private           System.Boolean                  m_Closed  // 0x40
  private           System.Single                   m_Length  // 0x44
PROPERTIES:
  Closed  get=0x04D865F0
  Count  get=0x04D88B10
  Item  get=0x04D9F8A0
METHODS:
  RVA=0x04D8E050  token=0x600003D  System.Single GetLength()
  RVA=0x0B2F08C0  token=0x600003F  System.Collections.Generic.IEnumerator<UnityEngine.Splines.BezierKnot> GetEnumerator()
  RVA=0x0B2F095C  token=0x6000040  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B2F0F90  token=0x6000041  System.Void .ctor(UnityEngine.Splines.ISpline spline, Unity.Mathematics.float4x4 transform, Unity.Collections.Allocator allocator)
  RVA=0x0B2F0964  token=0x6000042  System.Void .ctor(System.Collections.Generic.IReadOnlyList<UnityEngine.Splines.BezierKnot> knots, System.Collections.Generic.IReadOnlyList<System.Int32> splits, System.Boolean closed, Unity.Mathematics.float4x4 transform, Unity.Collections.Allocator allocator)
  RVA=0x04DB9970  token=0x6000043  UnityEngine.Splines.BezierCurve GetCurve(System.Int32 index)
  RVA=0x04DB9950  token=0x6000044  System.Single GetCurveLength(System.Int32 curveIndex)
  RVA=0x0B2F0758  token=0x6000045  System.Void Dispose()
  RVA=0x0B2F07CC  token=0x6000046  System.Single GetCurveInterpolation(System.Int32 curveIndex, System.Single curveDistance)
END_CLASS

CLASS: UnityEngine.Splines.Spline
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x68
IMPLEMENTS: UnityEngine.Splines.ISpline System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1 System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1
FIELDS:
  private   static  UnityEngine.Splines.TangentMode k_DefaultTangentMode  // const
  private   static  UnityEngine.Splines.BezierTangentk_DefaultMainTangent  // const
  private   static  System.Int32                    k_BatchModification  // const
  private   static  System.Int32                    k_CurveDistanceLutResolution  // const
  private           UnityEngine.Splines.SplineType  m_EditModeType  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Splines.BezierKnot>m_Knots  // 0x18
  private           System.Single                   m_Length  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Splines.Spline.MetaData>m_MetaData  // 0x28
  private           System.Boolean                  m_Closed  // 0x30
  private           UnityEngine.Splines.SplineDataDictionary<System.Int32>m_IntData  // 0x38
  private           UnityEngine.Splines.SplineDataDictionary<System.Single>m_FloatData  // 0x40
  private           UnityEngine.Splines.SplineDataDictionary<Unity.Mathematics.float4>m_Float4Data  // 0x48
  private           UnityEngine.Splines.SplineDataDictionary<UnityEngine.Object>m_ObjectData  // 0x50
  private           System.Action                   changed  // 0x58
  private   static  System.Action<UnityEngine.Splines.Spline,System.Int32,UnityEngine.Splines.SplineModification>Changed  // static @ 0x0
  private           System.ValueTuple<System.Single,System.Single>m_LastKnotChangeCurveLengths  // 0x60
PROPERTIES:
  embeddedSplineData  get=0x0B2F423C
  Count  get=0x03157740
  IsReadOnly  get=0x012081B0
  Knots  get=0x0385B100
  Closed  get=0x04D865F0  set=0x03159990
  Item  get=0x03EFD750  set=0x0B2F42AC
EVENTS:
  Changed  add=add_Changed  remove=remove_Changed
METHODS:
  RVA=0x0B2F3BF4  token=0x6000051  System.Void SetDirtyNoNotify()
  RVA=0x0B2F3C94  token=0x6000052  System.Void SetDirty(UnityEngine.Splines.SplineModification modificationEvent, System.Int32 knotIndex)
  RVA=0x041E1670  token=0x6000053  System.Void OnSplineChanged()
  RVA=0x03155B20  token=0x6000054  System.Void EnsureMetaDataValid()
  RVA=0x0B2F3710  token=0x6000055  UnityEngine.Splines.TangentMode GetTangentMode(System.Int32 index)
  RVA=0x0B2F3190  token=0x6000056  System.Void ApplyTangentModeNoNotify(System.Int32 index, UnityEngine.Splines.BezierTangent main)
  RVA=0x0B2F3778  token=0x600005A  System.Int32 IndexOf(UnityEngine.Splines.BezierKnot item)
  RVA=0x0B2F39EC  token=0x600005B  System.Void Insert(System.Int32 index, UnityEngine.Splines.BezierKnot knot)
  RVA=0x0B2F399C  token=0x600005C  System.Void Insert(System.Int32 index, UnityEngine.Splines.BezierKnot knot, UnityEngine.Splines.TangentMode mode)
  RVA=0x0B2F37F4  token=0x600005D  System.Void Insert(System.Int32 index, UnityEngine.Splines.BezierKnot knot, UnityEngine.Splines.TangentMode mode, System.Single tension)
  RVA=0x0B2F3A44  token=0x600005E  System.Void RemoveAt(System.Int32 index)
  RVA=0x0B2F4108  token=0x6000061  System.Void SetKnot(System.Int32 index, UnityEngine.Splines.BezierKnot value, UnityEngine.Splines.BezierTangent main)
  RVA=0x0B2F3FAC  token=0x6000062  System.Void SetKnotNoNotify(System.Int32 index, UnityEngine.Splines.BezierKnot value, UnityEngine.Splines.BezierTangent main)
  RVA=0x03C2A7A0  token=0x6000063  System.Void .ctor()
  RVA=0x03C2A4D0  token=0x6000064  System.Void .ctor(System.Collections.Generic.IEnumerable<UnityEngine.Splines.BezierKnot> knots, System.Boolean closed)
  RVA=0x03C29BF0  token=0x6000065  UnityEngine.Splines.BezierCurve GetCurve(System.Int32 index)
  RVA=0x03155900  token=0x6000066  System.Single GetCurveLength(System.Int32 index)
  RVA=0x03155C20  token=0x6000067  System.Single GetLength()
  RVA=0x03155820  token=0x6000068  UnityEngine.Splines.DistanceToInterpolation[] GetCurveDistanceLut(System.Int32 index)
  RVA=0x031555B0  token=0x6000069  System.Single GetCurveInterpolation(System.Int32 curveIndex, System.Single curveDistance)
  RVA=0x0B2F3668  token=0x600006A  System.Collections.Generic.IEnumerator<UnityEngine.Splines.BezierKnot> GetEnumerator()
  RVA=0x0B2F4194  token=0x600006B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B2F30DC  token=0x600006C  System.Void Add(UnityEngine.Splines.BezierKnot item)
  RVA=0x0B2F3120  token=0x600006D  System.Void Add(UnityEngine.Splines.BezierKnot item, UnityEngine.Splines.TangentMode mode)
  RVA=0x0B2F3514  token=0x600006E  System.Void Clear()
  RVA=0x0B2F3588  token=0x600006F  System.Boolean Contains(UnityEngine.Splines.BezierKnot item)
  RVA=0x0B2F3604  token=0x6000070  System.Void CopyTo(UnityEngine.Splines.BezierKnot[] array, System.Int32 arrayIndex)
  RVA=0x0B2F3B60  token=0x6000071  System.Boolean Remove(UnityEngine.Splines.BezierKnot item)
  RVA=0x0B2F348C  token=0x6000072  System.Void CacheKnotOperationCurves(System.Int32 index)
END_CLASS

CLASS: UnityEngine.Splines.SplineContainer
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Splines.ISplineContainer UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private   static  System.String                   k_IconPath  // const
  private           UnityEngine.Splines.Spline      m_Spline  // 0x18
  private           UnityEngine.Splines.Spline[]    m_Splines  // 0x20
  private           UnityEngine.Splines.KnotLinkCollectionm_Knots  // 0x28
  private           System.Collections.Generic.List<System.ValueTuple<System.Int32,System.Int32>>m_ReorderedSplinesIndices  // 0x30
  private           System.Collections.Generic.List<System.Int32>m_RemovedSplinesIndices  // 0x38
  private           System.Collections.Generic.List<System.Int32>m_AddedSplinesIndices  // 0x40
  private   static  System.Action<UnityEngine.Splines.SplineContainer,System.Int32>SplineAdded  // static @ 0x0
  private   static  System.Action<UnityEngine.Splines.SplineContainer,System.Int32>SplineRemoved  // static @ 0x8
  private   static  System.Action<UnityEngine.Splines.SplineContainer,System.Int32,System.Int32>SplineReordered  // static @ 0x10
PROPERTIES:
  Splines  get=0x0B2F1B24  set=0x0B2F1DC4
  KnotLinkCollection  get=0x04D86240
  Item  get=0x0B2F1AC4
  IsScaled  get=0x0B2F1A34
  Spline  get=0x0B2F1AF0  set=0x0B2F1D7C
EVENTS:
  SplineAdded  add=add_SplineAdded  remove=remove_SplineAdded
  SplineRemoved  add=add_SplineRemoved  remove=remove_SplineRemoved
  SplineReordered  add=add_SplineReordered  remove=remove_SplineReordered
METHODS:
  RVA=0x0B2F1664  token=0x600008A  System.Int32 IndexOf(System.Collections.Generic.IReadOnlyList<UnityEngine.Splines.Spline> self, UnityEngine.Splines.Spline elementToFind)
  RVA=0x047C3B10  token=0x600008D  System.Void OnEnable()
  RVA=0x047C3B90  token=0x600008E  System.Void OnDisable()
  RVA=0x0B2F1774  token=0x600008F  System.Void OnSplineChanged(UnityEngine.Splines.Spline spline, System.Int32 index, UnityEngine.Splines.SplineModification modificationType)
  RVA=0x0B2F16F0  token=0x6000090  System.Void OnKnotModified(UnityEngine.Splines.Spline spline, System.Int32 index)
  RVA=0x0B2F1598  token=0x6000094  System.Boolean Evaluate(System.Single t, Unity.Mathematics.float3& position, Unity.Mathematics.float3& tangent, Unity.Mathematics.float3& upVector)
  RVA=0x0B2F15C8  token=0x6000095  System.Boolean Evaluate(System.Int32 splineIndex, System.Single t, Unity.Mathematics.float3& position, Unity.Mathematics.float3& tangent, Unity.Mathematics.float3& upVector)
  RVA=-1  // generic def  token=0x6000096  System.Boolean Evaluate(T spline, System.Single t, Unity.Mathematics.float3& position, Unity.Mathematics.float3& tangent, Unity.Mathematics.float3& upVector)
  RVA=0x0B2F12D4  token=0x6000097  Unity.Mathematics.float3 EvaluatePosition(System.Single t)
  RVA=0x0B2F1278  token=0x6000098  UnityEngine.Vector3 EvaluatePositionV3(System.Single t)
  RVA=0x0B2F1310  token=0x6000099  Unity.Mathematics.float3 EvaluatePosition(System.Int32 splineIndex, System.Single t)
  RVA=-1  // generic def  token=0x600009A  Unity.Mathematics.float3 EvaluatePosition(T spline, System.Single t)
  RVA=0x0B2F1470  token=0x600009B  Unity.Mathematics.float3 EvaluateTangent(System.Single t)
  RVA=0x0B2F13C0  token=0x600009C  Unity.Mathematics.float3 EvaluateTangent(System.Int32 splineIndex, System.Single t)
  RVA=-1  // generic def  token=0x600009D  Unity.Mathematics.float3 EvaluateTangent(T spline, System.Single t)
  RVA=0x0B2F155C  token=0x600009E  Unity.Mathematics.float3 EvaluateUpVector(System.Single t)
  RVA=0x0B2F14AC  token=0x600009F  Unity.Mathematics.float3 EvaluateUpVector(System.Int32 splineIndex, System.Single t)
  RVA=-1  // generic def  token=0x60000A0  Unity.Mathematics.float3 EvaluateUpVector(T spline, System.Single t)
  RVA=0x0B2F123C  token=0x60000A1  Unity.Mathematics.float3 EvaluateAcceleration(System.Single t)
  RVA=0x0B2F118C  token=0x60000A2  Unity.Mathematics.float3 EvaluateAcceleration(System.Int32 splineIndex, System.Single t)
  RVA=-1  // generic def  token=0x60000A3  Unity.Mathematics.float3 EvaluateAcceleration(T spline, System.Single t)
  RVA=0x0B2F1180  token=0x60000A4  System.Single CalculateLength()
  RVA=0x0B2F107C  token=0x60000A5  System.Single CalculateLength(System.Int32 splineIndex)
  RVA=0x041E1670  token=0x60000A6  System.Void OnBeforeSerialize()
  RVA=0x03157F60  token=0x60000A7  System.Void OnAfterDeserialize()
  RVA=0x03C2BF40  token=0x60000A8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Splines.PathIndexUnit
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Splines.PathIndexUnitDistance  // const
  public    static  UnityEngine.Splines.PathIndexUnitNormalized  // const
  public    static  UnityEngine.Splines.PathIndexUnitKnot  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.ISplineModificationHandler
TYPE:  interface
TOKEN: 0x200001A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000AB  System.Void OnSplineModified(UnityEngine.Splines.SplineModificationData info)
END_CLASS

CLASS: UnityEngine.Splines.SplineData`1
TYPE:  class
TOKEN: 0x200001B
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable UnityEngine.Splines.ISplineModificationHandler
FIELDS:
  private   static readonly UnityEngine.Splines.DataPointComparer<UnityEngine.Splines.DataPoint<T>>k_DataPointComparer  // static @ 0x0
  private           UnityEngine.Splines.PathIndexUnitm_IndexUnit  // 0x0
  private           T                               m_DefaultValue  // 0x0
  private           System.Collections.Generic.List<UnityEngine.Splines.DataPoint<T>>m_DataPoints  // 0x0
  private           System.Boolean                  m_NeedsSort  // 0x0
  private           System.Action                   changed  // 0x0
  private           System.Action                   Changed  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000AD  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000AE  System.Collections.Generic.IEnumerator<UnityEngine.Splines.DataPoint<T>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60000AF  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000B0  System.Void UnityEngine.Splines.ISplineModificationHandler.OnSplineModified(UnityEngine.Splines.SplineModificationData data)
  RVA=-1  // not resolved  token=0x60000B1  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Splines.SplineDataKeyValuePair`1
TYPE:  class
TOKEN: 0x200001D
FIELDS:
  public            System.String                   Key  // 0x0
  public            UnityEngine.Splines.SplineData<T>Value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000B8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Splines.SplineDataDictionary`1
TYPE:  class
TOKEN: 0x200001E
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.Splines.SplineDataKeyValuePair<T>>m_Data  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000B9  System.Collections.Generic.IEnumerator<UnityEngine.Splines.SplineDataKeyValuePair<T>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60000BA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000BB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Splines.SplineKnotIndex
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Int32                    Spline  // 0x10
  public            System.Int32                    Knot  // 0x14
METHODS:
  RVA=0x04D85C40  token=0x60000BC  System.Void .ctor(System.Int32 spline, System.Int32 knot)
  RVA=0x063516B4  token=0x60000BD  System.Boolean Equals(UnityEngine.Splines.SplineKnotIndex otherIndex)
  RVA=0x0B2F24AC  token=0x60000BE  System.Boolean Equals(System.Object obj)
  RVA=0x04DB99A0  token=0x60000BF  System.Int32 GetHashCode()
  RVA=0x0B2F2510  token=0x60000C0  System.String ToString()
END_CLASS

CLASS: UnityEngine.Splines.SplineMath
TYPE:  static class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2F2598  token=0x60000C1  Unity.Mathematics.float3 PointLineNearestPoint(Unity.Mathematics.float3 p, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, System.Single& lineParam)
END_CLASS

CLASS: UnityEngine.Splines.SplineModification
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Splines.SplineModificationDefault  // const
  public    static  UnityEngine.Splines.SplineModificationClosedModified  // const
  public    static  UnityEngine.Splines.SplineModificationKnotModified  // const
  public    static  UnityEngine.Splines.SplineModificationKnotInserted  // const
  public    static  UnityEngine.Splines.SplineModificationKnotRemoved  // const
  public    static  UnityEngine.Splines.SplineModificationKnotReordered  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.SplineModificationData
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x28
FIELDS:
  public    readonly UnityEngine.Splines.Spline      Spline  // 0x10
  public    readonly UnityEngine.Splines.SplineModificationModification  // 0x18
  public    readonly System.Int32                    KnotIndex  // 0x1c
  public    readonly System.Single                   PrevCurveLength  // 0x20
  public    readonly System.Single                   NextCurveLength  // 0x24
METHODS:
  RVA=0x0B2F274C  token=0x60000C2  System.Void .ctor(UnityEngine.Splines.Spline spline, UnityEngine.Splines.SplineModification modification, System.Int32 knotIndex, System.Single prevCurveLength, System.Single nextCurveLength)
END_CLASS

CLASS: UnityEngine.Splines.SplineType
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.Splines.SplineType  CatmullRom  // const
  public    static  UnityEngine.Splines.SplineType  Bezier  // const
  public    static  UnityEngine.Splines.SplineType  Linear  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Splines.SplineUtility
TYPE:  static class
TOKEN: 0x2000024
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000C3  System.Boolean Evaluate(T spline, System.Single t, Unity.Mathematics.float3& position, Unity.Mathematics.float3& tangent, Unity.Mathematics.float3& upVector)
  RVA=-1  // generic def  token=0x60000C4  Unity.Mathematics.float3 EvaluatePosition(T spline, System.Single t)
  RVA=-1  // generic def  token=0x60000C5  UnityEngine.Vector3 EvaluatePositionV3(T spline, System.Single t)
  RVA=-1  // generic def  token=0x60000C6  Unity.Mathematics.float3 EvaluateTangent(T spline, System.Single t)
  RVA=-1  // generic def  token=0x60000C7  Unity.Mathematics.float3 EvaluateUpVector(T spline, System.Single t)
  RVA=-1  // generic def  token=0x60000C8  Unity.Mathematics.float3 EvaluateUpVector(T spline, System.Int32 curveIndex, System.Single curveT)
  RVA=-1  // generic def  token=0x60000C9  Unity.Mathematics.float3 EvaluateAcceleration(T spline, System.Single t)
  RVA=-1  // generic def  token=0x60000CA  System.Int32 SplineToCurveT(T spline, System.Single splineT, System.Single& curveT)
  RVA=-1  // generic def  token=0x60000CB  System.Int32 SplineToCurveT(T spline, System.Single splineT, System.Single& curveT, System.Boolean useLUT)
  RVA=-1  // generic def  token=0x60000CC  System.Single CalculateLength(T spline, Unity.Mathematics.float4x4 transform)
  RVA=-1  // generic def  token=0x60000CD  System.Int32 GetCurveCount(T spline)
  RVA=0x0B2F304C  token=0x60000CE  System.Int32 GetSubdivisionCount(System.Single length, System.Int32 resolution)
  RVA=-1  // generic def  token=0x60000CF  UnityEngine.Splines.SplineUtility.Segment GetNearestPoint(T spline, Unity.Mathematics.float3 point, UnityEngine.Splines.SplineUtility.Segment range, System.Single& distance, Unity.Mathematics.float3& nearest, System.Single& time, System.Int32 segments)
  RVA=-1  // generic def  token=0x60000D0  System.Single GetNearestPoint(T spline, Unity.Mathematics.float3 point, Unity.Mathematics.float3& nearest, System.Single& t, System.Int32 resolution, System.Int32 iterations)
  RVA=-1  // generic def  token=0x60000D1  System.Int32 PreviousIndex(T spline, System.Int32 index)
  RVA=-1  // generic def  token=0x60000D2  System.Int32 NextIndex(T spline, System.Int32 index)
  RVA=-1  // generic def  token=0x60000D3  UnityEngine.Splines.BezierKnot Previous(T spline, System.Int32 index)
  RVA=-1  // generic def  token=0x60000D4  UnityEngine.Splines.BezierKnot Next(T spline, System.Int32 index)
  RVA=0x0B2F30C0  token=0x60000D5  System.Int32 PreviousIndex(System.Int32 index, System.Int32 count, System.Boolean wrap)
  RVA=0x0B2F30A4  token=0x60000D6  System.Int32 NextIndex(System.Int32 index, System.Int32 count, System.Boolean wrap)
  RVA=0x0B2F2CE8  token=0x60000D7  Unity.Mathematics.float3 GetExplicitLinearTangent(Unity.Mathematics.float3 point, Unity.Mathematics.float3 to)
  RVA=0x0B2F29D0  token=0x60000D8  Unity.Mathematics.float3 GetAutoSmoothTangent(Unity.Mathematics.float3 previous, Unity.Mathematics.float3 current, Unity.Mathematics.float3 next, System.Single tension)
  RVA=0x0B2F2784  token=0x60000D9  UnityEngine.Splines.BezierKnot GetAutoSmoothKnot(Unity.Mathematics.float3 position, Unity.Mathematics.float3 previous, Unity.Mathematics.float3 next, Unity.Mathematics.float3 normal, System.Single width, System.Single tension)
  RVA=0x0B2F2D6C  token=0x60000DA  Unity.Mathematics.quaternion GetKnotRotation(Unity.Mathematics.float3 tangent, Unity.Mathematics.float3 normal)
  RVA=-1  // generic def  token=0x60000DB  System.Boolean IsIndexValid(T container, UnityEngine.Splines.SplineKnotIndex index)
  RVA=-1  // generic def  token=0x60000DC  System.Void SetLinkedKnotPosition(T container, UnityEngine.Splines.SplineKnotIndex index)
END_CLASS

