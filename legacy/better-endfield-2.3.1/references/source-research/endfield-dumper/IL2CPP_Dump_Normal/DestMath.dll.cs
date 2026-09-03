// ========================================================
// Dumped by @desirepro
// Assembly: DestMath.dll
// Classes:  152
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000005  // size: 0x18
    public sealed struct Edge
    {
        // Fields
        public System.Int32 V0;  // 0x10
        public System.Int32 V1;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x6000017
        public System.Void .ctor(System.Int32 v0, System.Int32 v1) { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public sealed struct InnerPoint
    {
        // Fields
        public System.Single AverageDistance;  // 0x10
        public System.Single Distance0;  // 0x14
        public System.Single Distance1;  // 0x18
        public System.Int32 Index;  // 0x1c

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class SortedVertex
    {
        // Fields
        public System.Single Value;  // 0x10
        public System.Int32 Index;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600001C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Dest.Math.ConvexHull1.<>c <>9;  // static @ 0x0
        public static System.Comparison<Dest.Math.ConvexHull1.SortedVertex> <>9__1_0;  // static @ 0x8

        // Methods
        // RVA: 0x06920418  token: 0x600001D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600001E
        public System.Void .ctor() { }
        // RVA: 0x069203AC  token: 0x600001F
        private System.Int32 <Create>b__1_0(Dest.Math.ConvexHull1.SortedVertex e1, Dest.Math.ConvexHull1.SortedVertex e2) { }

    }

    // TypeToken: 0x200000C  // size: 0x30
    public class Edge
    {
        // Fields
        public System.Int32 V0;  // 0x10
        public System.Int32 V1;  // 0x14
        public Dest.Math.ConvexHull2.Edge E0;  // 0x18
        public Dest.Math.ConvexHull2.Edge E1;  // 0x20
        public System.Int32 Sign;  // 0x28
        public System.Int32 Time;  // 0x2c

        // Methods
        // RVA: 0x04D898B0  token: 0x6000023
        public System.Void .ctor(System.Int32 v0, System.Int32 v1) { }
        // RVA: 0x0691FD1C  token: 0x6000024
        public System.Int32 GetSign(System.Int32 i, Dest.Math.Query2 query) { }
        // RVA: 0x0691FD60  token: 0x6000025
        public System.Void Insert(Dest.Math.ConvexHull2.Edge adj0, Dest.Math.ConvexHull2.Edge adj1) { }
        // RVA: 0x0691FC38  token: 0x6000026
        public System.Void DeleteSelf() { }
        // RVA: 0x0691FC78  token: 0x6000027
        public System.Void GetIndices(System.Int32[]& indices) { }

    }

    // TypeToken: 0x200000E  // size: 0x48
    public class Triangle
    {
        // Fields
        public System.Int32 V0;  // 0x10
        public System.Int32 V1;  // 0x14
        public System.Int32 V2;  // 0x18
        public Dest.Math.ConvexHull3.Triangle Adj0;  // 0x20
        public Dest.Math.ConvexHull3.Triangle Adj1;  // 0x28
        public Dest.Math.ConvexHull3.Triangle Adj2;  // 0x30
        public System.Int32 Sign;  // 0x38
        public System.Int32 Time;  // 0x3c
        public System.Boolean OnStack;  // 0x40

        // Methods
        // RVA: 0x04D898C0  token: 0x600002C
        public System.Void .ctor(System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x06920304  token: 0x600002D
        public Dest.Math.ConvexHull3.Triangle GetAdj(System.Int32 index) { }
        // RVA: 0x0692037C  token: 0x600002E
        public System.Void SetAdj(System.Int32 index, Dest.Math.ConvexHull3.Triangle value) { }
        // RVA: 0x06920364  token: 0x600002F
        public System.Int32 GetV(System.Int32 index) { }
        // RVA: 0x0692031C  token: 0x6000030
        public System.Int32 GetSign(System.Int32 i, Dest.Math.Query3 query) { }
        // RVA: 0x06920230  token: 0x6000031
        public System.Void AttachTo(Dest.Math.ConvexHull3.Triangle adj0, Dest.Math.ConvexHull3.Triangle adj1, Dest.Math.ConvexHull3.Triangle adj2) { }
        // RVA: 0x06920268  token: 0x6000032
        public System.Int32 DetachFrom(System.Int32 adjIndex, Dest.Math.ConvexHull3.Triangle adj) { }

    }

    // TypeToken: 0x200000F  // size: 0x28
    public class TerminatorData
    {
        // Fields
        public System.Int32 V0;  // 0x10
        public System.Int32 V1;  // 0x14
        public System.Int32 NullIndex;  // 0x18
        public Dest.Math.ConvexHull3.Triangle T;  // 0x20

        // Methods
        // RVA: 0x069201FC  token: 0x6000033
        public System.Void .ctor(System.Int32 v0, System.Int32 v1, System.Int32 nullIndex, Dest.Math.ConvexHull3.Triangle tri) { }

    }

    // TypeToken: 0x2000055  // size: 0x48
    public class Information
    {
        // Fields
        public System.Int32 Dimension;  // 0x10
        public UnityEngine.Vector2 Min;  // 0x14
        public UnityEngine.Vector2 Max;  // 0x1c
        public System.Single MaxRange;  // 0x24
        public UnityEngine.Vector2 Origin;  // 0x28
        public UnityEngine.Vector2[] Direction;  // 0x30
        public System.Int32[] Extreme;  // 0x38
        public System.Boolean ExtremeCCW;  // 0x40

        // Methods
        // RVA: 0x06934844  token: 0x6000219
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000058  // size: 0x58
    public class Information
    {
        // Fields
        public System.Int32 Dimension;  // 0x10
        public UnityEngine.Vector3 Min;  // 0x14
        public UnityEngine.Vector3 Max;  // 0x20
        public System.Single MaxRange;  // 0x2c
        public UnityEngine.Vector3 Origin;  // 0x30
        public UnityEngine.Vector3[] Direction;  // 0x40
        public System.Int32[] Extreme;  // 0x48
        public System.Boolean ExtremeCCW;  // 0x50

        // Methods
        // RVA: 0x069348B8  token: 0x6000237
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x28
    public class PolyRootFinder
    {
        // Fields
        private System.Int32 _count;  // 0x10
        private System.Int32 _maxRoot;  // 0x14
        private System.Single[] _roots;  // 0x18
        private System.Single _epsilon;  // 0x20

        // Properties
        System.Single[] Roots { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x069469A0  token: 0x6000242
        public System.Void .ctor(System.Single epsilon) { }
        // RVA: 0x069463D0  token: 0x6000243
        public System.Boolean Bisection(Dest.Math.Polynomial poly, System.Single xMin, System.Single xMax, System.Int32 digits, System.Single& root) { }
        // RVA: 0x06946570  token: 0x6000244
        public System.Boolean Find(Dest.Math.Polynomial poly, System.Single xMin, System.Single xMax, System.Int32 digits) { }

    }

    // TypeToken: 0x200008A  // size: 0x60
    public class ItemData
    {
        // Fields
        public UnityEngine.Vector3 Position;  // 0x10
        public UnityEngine.Vector3 A;  // 0x1c
        public UnityEngine.Vector3 B;  // 0x28
        public UnityEngine.Vector3 C;  // 0x34
        public UnityEngine.Vector3 D;  // 0x40
        public System.Single Length;  // 0x4c
        public System.Single AccumulatedLength;  // 0x50
        private static System.Int32 SegmentCount;  // const
        private static System.Int32 SegmentCountPlus1;  // const
        private static System.Single DeltaTime;  // const
        public UnityEngine.Vector3[] RenderPoints;  // 0x58

        // Methods
        // RVA: 0x0694ED80  token: 0x600043F
        public System.Boolean EnsureRenderPointsValidity() { }
        // RVA: 0x0694F47C  token: 0x6000440
        public System.Void UpdateRenderPoints() { }
        // RVA: 0x0694F028  token: 0x6000441
        public UnityEngine.Vector3 EvalPosition(System.Single t) { }
        // RVA: 0x0694EDE0  token: 0x6000442
        public UnityEngine.Vector3 EvalFirstDerivative(System.Single t) { }
        // RVA: 0x0694F148  token: 0x6000443
        public UnityEngine.Vector3 EvalSecondDerivative(System.Single t) { }
        // RVA: 0x0694F400  token: 0x6000444
        public UnityEngine.Vector3 EvalThirdDerivative(System.Single t) { }
        // RVA: 0x0694F1EC  token: 0x6000445
        public System.Single EvalSpeed(System.Single t) { }
        // RVA: 0x0694F2E8  token: 0x6000446
        public UnityEngine.Vector3 EvalTangent(System.Single t) { }
        // RVA: 0x0694EED8  token: 0x6000447
        public System.Single EvalLength() { }
        // RVA: 0x0694EF74  token: 0x6000448
        public System.Single EvalLength(System.Single t0, System.Single t1) { }
        // RVA: 0x0694F448  token: 0x6000449
        public System.Single ProcessLength(System.Single currentLength) { }
        // RVA: 0x041E1670  token: 0x600044A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008B  // size: 0x30
    public class ArcLengthParametrization
    {
        // Fields
        public System.Single[] sSample;  // 0x10
        public System.Single[] tSample;  // 0x18
        public System.Single[] tsSlope;  // 0x20
        public System.Single L;  // 0x28

        // Methods
        // RVA: 0x06949E48  token: 0x600044B
        public System.Single GetApproximateTimeParameter(System.Single s) { }
        // RVA: 0x041E1670  token: 0x600044C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008D  // size: 0x58
    public class Data
    {
        // Fields
        private UnityEngine.Vector3[] _points;  // 0x10
        private System.Collections.Generic.List<System.Int32>[,,] _grid;  // 0x18
        private UnityEngine.Vector3 _min;  // 0x20
        private UnityEngine.Vector3 _max;  // 0x2c
        private System.Int32 _cellsX;  // 0x38
        private System.Int32 _cellsY;  // 0x3c
        private System.Int32 _cellsZ;  // 0x40
        private System.Single _cellSize;  // 0x44
        private System.Single _radius;  // 0x48
        private Dest.Math.Rand _rand;  // 0x50

        // Methods
        // RVA: 0x0694E0A8  token: 0x600044E
        public System.Void .ctor(UnityEngine.Vector3[] points, System.Single radius, Dest.Math.Rand rand, Dest.Math.AAB3 aab) { }
        // RVA: 0x0694D8D0  token: 0x600044F
        private static System.Int32 BinarySearch(System.Collections.Generic.List<System.Int32> array, System.Int32 value) { }
        // RVA: 0x0694D858  token: 0x6000450
        private static System.Int32 BinarySearch(System.Int32[] array, System.Int32 length, System.Int32 value) { }
        // RVA: 0x0694D990  token: 0x6000451
        private System.Void CalcGridIndices(UnityEngine.Vector3& point, System.Int32& i, System.Int32& j, System.Int32& k) { }
        // RVA: 0x0694D9D8  token: 0x6000452
        public System.Collections.Generic.List<System.Int32> Filter() { }

    }

    // TypeToken: 0x200008F  // size: 0x80
    public sealed class PointDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x069518E8  token: 0x600045B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x600045C
        public virtual System.Single Invoke(UnityEngine.Vector2& point) { }
        // RVA: 0x06951868  token: 0x600045D
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Vector2& point, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD4B98  token: 0x600045E
        public virtual System.Single EndInvoke(UnityEngine.Vector2& point, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000095
    public sealed class <GetEnumerator>d__13 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        public Dest.Math.ShuffleBag<T> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0
        private System.Int32 <len>5__3;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000481
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000482
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000483
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000485
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000098  // size: 0x24
    public sealed struct __StaticArrayInitTypeSize=20
    {
    }

    // TypeToken: 0x2000097  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 170A8789272DFBCBA0E6B7D2530CAA16D9199834934F442421DAB41C5EEA8C29;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 92052E6D9D3C30FD20B710DFB226BEEA15F69C2188B5AA373563D6966493C59C;  // static @ 0x14

    }

namespace Dest.Math
{

    // TypeToken: 0x2000002  // size: 0x20
    public class Polynomial
    {
        // Fields
        private System.Int32 _degree;  // 0x10
        private System.Single[] _coeffs;  // 0x18

        // Properties
        System.Int32 Degree { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x06920178 */ }
        System.Single Item { get; /* RVA: 0x06920148 */ set; /* RVA: 0x069201CC */ }

        // Methods
        // RVA: 0x06920140  token: 0x6000005
        public System.Void .ctor(System.Int32 degree) { }
        // RVA: 0x0692003C  token: 0x6000006
        public Dest.Math.Polynomial DeepCopy() { }
        // RVA: 0x0691FDB8  token: 0x6000007
        public Dest.Math.Polynomial CalcDerivative() { }
        // RVA: 0x0691FEBC  token: 0x6000008
        public Dest.Math.Polynomial CalcInversion() { }
        // RVA: 0x0691FF70  token: 0x6000009
        public System.Void Compress(System.Single epsilon) { }
        // RVA: 0x069200EC  token: 0x600000A
        public System.Single Eval(System.Single t) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public static class Approximation
    {
        // Methods
        // RVA: 0x069126D0  token: 0x600000B
        public static Dest.Math.Box2 GaussPointsFit2(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x06912F60  token: 0x600000C
        private static System.Boolean HeightLineFit2(System.Collections.Generic.IList<UnityEngine.Vector2> points, System.Single& a, System.Single& b) { }
        // RVA: 0x069136C8  token: 0x600000D
        public static Dest.Math.Line2 LeastSquaresLineFit2(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x06912A7C  token: 0x600000E
        public static Dest.Math.Box3 GaussPointsFit3(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x06913D70  token: 0x600000F
        public static Dest.Math.Line3 LeastsSquaresLineFit3(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x06913230  token: 0x6000010
        private static System.Boolean HeightPlaneFit3(System.Collections.Generic.IList<UnityEngine.Vector3> points, System.Single& a, System.Single& b, System.Single& c) { }
        // RVA: 0x069139B4  token: 0x6000011
        public static Dest.Math.Plane3 LeastSquaresPlaneFit3(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public class ConcaveHull2
    {
        // Methods
        // RVA: 0x06914F30  token: 0x6000012
        private static System.Boolean _TryFastSort(Dest.Math.ConcaveHull2.InnerPoint[] x, System.Int32 first, System.Int32 last) { }
        // RVA: 0x06914D8C  token: 0x6000013
        private static System.Void Quicksort(Dest.Math.ConcaveHull2.InnerPoint[] x, System.Int32 first, System.Int32 last) { }
        // RVA: 0x069141A4  token: 0x6000014
        private static System.Single CalcDistanceFromPointToEdge(UnityEngine.Vector2& pointA, UnityEngine.Vector2& v0, UnityEngine.Vector2& v1) { }
        // RVA: 0x069142B4  token: 0x6000015
        public static System.Boolean Create(UnityEngine.Vector2[] points, System.Int32[]& concaveHull, System.Int32[] convexHull, System.Single N, System.Single epsilon) { }
        // RVA: 0x041E1670  token: 0x6000016
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public static class ConcaveHull
    {
        // Methods
        // RVA: 0x0691502C  token: 0x6000018
        public static System.Boolean Create2D(UnityEngine.Vector2[] points, System.Int32[]& concaveHull, System.Int32[]& convexHull, System.Single algorithmThreshold, System.Single epsilon) { }
        // RVA: 0x06915004  token: 0x6000019
        public static System.Boolean Create2D(UnityEngine.Vector2[] points, System.Int32[]& concaveHull, System.Single algorithmThreshold, System.Single epsilon) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class ConvexHull1
    {
        // Methods
        // RVA: 0x06915178  token: 0x600001A
        public static System.Void Create(System.Single[] vertices, System.Single epsilon, System.Int32& dimension, System.Int32[]& indices) { }
        // RVA: 0x041E1670  token: 0x600001B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public class ConvexHull2
    {
        // Methods
        // RVA: 0x06915430  token: 0x6000020
        public static System.Boolean Create(System.Collections.Generic.IList<UnityEngine.Vector2> vertices, System.Single epsilon, System.Int32& dimension, System.Int32[]& indices) { }
        // RVA: 0x069158E4  token: 0x6000021
        private static System.Boolean Update(Dest.Math.ConvexHull2.Edge& hull, System.Int32 i, Dest.Math.Query2 query) { }
        // RVA: 0x041E1670  token: 0x6000022
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public class ConvexHull3
    {
        // Methods
        // RVA: 0x06915ABC  token: 0x6000028
        public static System.Boolean Create(System.Collections.Generic.IList<UnityEngine.Vector3> vertices, System.Single epsilon, System.Int32& dimension, System.Int32[]& indices) { }
        // RVA: 0x069164C8  token: 0x6000029
        private static System.Boolean Update(System.Collections.Generic.HashSet<Dest.Math.ConvexHull3.Triangle> hull, System.Int32 i, Dest.Math.Query3 query) { }
        // RVA: 0x069162B0  token: 0x600002A
        private static System.Void ExtractIndices(System.Collections.Generic.HashSet<Dest.Math.ConvexHull3.Triangle> hull, System.Int32[]& indices) { }
        // RVA: 0x041E1670  token: 0x600002B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class ConvexHull
    {
        // Methods
        // RVA: 0x06916D60  token: 0x6000034
        public static System.Boolean Create2D(System.Collections.Generic.IList<UnityEngine.Vector2> points, System.Int32[]& indices, System.Int32& dimension, System.Single epsilon) { }
        // RVA: 0x06916E00  token: 0x6000035
        public static System.Boolean Create3D(System.Collections.Generic.IList<UnityEngine.Vector3> points, System.Int32[]& indices, System.Int32& dimension, System.Single epsilon) { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public static class Distance
    {
        // Methods
        // RVA: 0x0691853C  token: 0x6000036
        public static System.Single Line2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1) { }
        // RVA: 0x06918564  token: 0x6000037
        public static System.Single Line2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x0691982C  token: 0x6000038
        public static System.Single SqrLine2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1) { }
        // RVA: 0x06919944  token: 0x6000039
        public static System.Single SqrLine2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06918594  token: 0x600003A
        public static System.Single Line2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray) { }
        // RVA: 0x069185BC  token: 0x600003B
        public static System.Single Line2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06919B70  token: 0x600003C
        public static System.Single SqrLine2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray) { }
        // RVA: 0x06919CFC  token: 0x600003D
        public static System.Single SqrLine2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x0691861C  token: 0x600003E
        public static System.Single Line2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment) { }
        // RVA: 0x069185EC  token: 0x600003F
        public static System.Single Line2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06919F58  token: 0x6000040
        public static System.Single SqrLine2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment) { }
        // RVA: 0x0691A188  token: 0x6000041
        public static System.Single SqrLine2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06918894  token: 0x6000042
        public static System.Single Point2AAB2(UnityEngine.Vector2& point, Dest.Math.AAB2& box) { }
        // RVA: 0x069187D8  token: 0x6000043
        public static System.Single Point2AAB2(UnityEngine.Vector2& point, Dest.Math.AAB2& box, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691B530  token: 0x6000044
        public static System.Single SqrPoint2AAB2(UnityEngine.Vector2& point, Dest.Math.AAB2& box) { }
        // RVA: 0x0691B5A0  token: 0x6000045
        public static System.Single SqrPoint2AAB2(UnityEngine.Vector2& point, Dest.Math.AAB2& box, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918930  token: 0x6000046
        public static System.Single Point2Box2(UnityEngine.Vector2& point, Dest.Math.Box2& box) { }
        // RVA: 0x06918908  token: 0x6000047
        public static System.Single Point2Box2(UnityEngine.Vector2& point, Dest.Math.Box2& box, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691B658  token: 0x6000048
        public static System.Single SqrPoint2Box2(UnityEngine.Vector2& point, Dest.Math.Box2& box) { }
        // RVA: 0x0691B770  token: 0x6000049
        public static System.Single SqrPoint2Box2(UnityEngine.Vector2& point, Dest.Math.Box2& box, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918A08  token: 0x600004A
        public static System.Single Point2Circle2(UnityEngine.Vector2& point, Dest.Math.Circle2& circle) { }
        // RVA: 0x06918958  token: 0x600004B
        public static System.Single Point2Circle2(UnityEngine.Vector2& point, Dest.Math.Circle2& circle, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691B9E4  token: 0x600004C
        public static System.Single SqrPoint2Circle2(UnityEngine.Vector2& point, Dest.Math.Circle2& circle) { }
        // RVA: 0x0691B930  token: 0x600004D
        public static System.Single SqrPoint2Circle2(UnityEngine.Vector2& point, Dest.Math.Circle2& circle, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918A6C  token: 0x600004E
        public static System.Single Point2Line2(UnityEngine.Vector2& point, Dest.Math.Line2& line) { }
        // RVA: 0x06918A44  token: 0x600004F
        public static System.Single Point2Line2(UnityEngine.Vector2& point, Dest.Math.Line2& line, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691BB08  token: 0x6000050
        public static System.Single SqrPoint2Line2(UnityEngine.Vector2& point, Dest.Math.Line2& line) { }
        // RVA: 0x0691BA24  token: 0x6000051
        public static System.Single SqrPoint2Line2(UnityEngine.Vector2& point, Dest.Math.Line2& line, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918A94  token: 0x6000052
        public static System.Single Point2Ray2(UnityEngine.Vector2& point, Dest.Math.Ray2& ray) { }
        // RVA: 0x06918ABC  token: 0x6000053
        public static System.Single Point2Ray2(UnityEngine.Vector2& point, Dest.Math.Ray2& ray, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691BCE8  token: 0x6000054
        public static System.Single SqrPoint2Ray2(UnityEngine.Vector2& point, Dest.Math.Ray2& ray) { }
        // RVA: 0x0691BBDC  token: 0x6000055
        public static System.Single SqrPoint2Ray2(UnityEngine.Vector2& point, Dest.Math.Ray2& ray, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918AE4  token: 0x6000056
        public static System.Single Point2Segment2(UnityEngine.Vector2& point, Dest.Math.Segment2& segment) { }
        // RVA: 0x06918B0C  token: 0x6000057
        public static System.Single Point2Segment2(UnityEngine.Vector2& point, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691BF08  token: 0x6000058
        public static System.Single SqrPoint2Segment2(UnityEngine.Vector2& point, Dest.Math.Segment2& segment) { }
        // RVA: 0x0691BDD4  token: 0x6000059
        public static System.Single SqrPoint2Segment2(UnityEngine.Vector2& point, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x06918B34  token: 0x600005A
        public static System.Single Point2Triangle2(UnityEngine.Vector2& point, Dest.Math.Triangle2& triangle) { }
        // RVA: 0x06918C9C  token: 0x600005B
        public static System.Single Point2Triangle2(UnityEngine.Vector2& point, Dest.Math.Triangle2& triangle, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691C030  token: 0x600005C
        public static System.Single SqrPoint2Triangle2(UnityEngine.Vector2& point, Dest.Math.Triangle2& triangle) { }
        // RVA: 0x0691C198  token: 0x600005D
        public static System.Single SqrPoint2Triangle2(UnityEngine.Vector2& point, Dest.Math.Triangle2& triangle, UnityEngine.Vector2& closestPoint) { }
        // RVA: 0x0691957C  token: 0x600005E
        public static System.Single Ray2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1) { }
        // RVA: 0x0691954C  token: 0x600005F
        public static System.Single Ray2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x0691E1F8  token: 0x6000060
        public static System.Single SqrRay2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1) { }
        // RVA: 0x0691DF08  token: 0x6000061
        public static System.Single SqrRay2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x069195BC  token: 0x6000062
        public static System.Single Ray2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment) { }
        // RVA: 0x069195FC  token: 0x6000063
        public static System.Single Ray2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x0691E588  token: 0x6000064
        public static System.Single SqrRay2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment) { }
        // RVA: 0x0691E224  token: 0x6000065
        public static System.Single SqrRay2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06919724  token: 0x6000066
        public static System.Single Segment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1) { }
        // RVA: 0x069196F4  token: 0x6000067
        public static System.Single Segment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x0691EE94  token: 0x6000068
        public static System.Single SqrSegment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1) { }
        // RVA: 0x0691EEC0  token: 0x6000069
        public static System.Single SqrSegment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1, UnityEngine.Vector2& closestPoint0, UnityEngine.Vector2& closestPoint1) { }
        // RVA: 0x06917710  token: 0x600006A
        private static System.Void Face(Dest.Math.Box3& mBox, System.Single& mLineParameter, System.Int32 i0, System.Int32 i1, System.Int32 i2, UnityEngine.Vector3& pnt, UnityEngine.Vector3& dir, UnityEngine.Vector3& PmE, System.Single& sqrDistance) { }
        // RVA: 0x06917630  token: 0x600006B
        private static System.Void CaseNoZeros(Dest.Math.Box3& mBox, System.Single& mLineParameter, UnityEngine.Vector3& pnt, UnityEngine.Vector3& dir, System.Single& sqrDistance) { }
        // RVA: 0x069171CC  token: 0x600006C
        private static System.Void Case0(Dest.Math.Box3& mBox, System.Single& mLineParameter, System.Int32 i0, System.Int32 i1, System.Int32 i2, UnityEngine.Vector3& pnt, UnityEngine.Vector3& dir, System.Single& sqrDistance) { }
        // RVA: 0x06916FBC  token: 0x600006D
        private static System.Void Case00(Dest.Math.Box3& mBox, System.Single& mLineParameter, System.Int32 i0, System.Int32 i1, System.Int32 i2, UnityEngine.Vector3& pnt, UnityEngine.Vector3& dir, System.Single& sqrDistance) { }
        // RVA: 0x06916EA0  token: 0x600006E
        private static System.Void Case000(Dest.Math.Box3& mBox, System.Single& mLineParameter, UnityEngine.Vector3& pnt, System.Single& sqrDistance) { }
        // RVA: 0x06918644  token: 0x600006F
        public static System.Single Line3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box, Dest.Math.Line3Box3Dist& info) { }
        // RVA: 0x0691866C  token: 0x6000070
        public static System.Single Line3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box) { }
        // RVA: 0x0691A458  token: 0x6000071
        public static System.Single SqrLine3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box, Dest.Math.Line3Box3Dist& info) { }
        // RVA: 0x0691AA10  token: 0x6000072
        public static System.Single SqrLine3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box) { }
        // RVA: 0x069186AC  token: 0x6000073
        public static System.Single Line3Line3(Dest.Math.Line3& line0, Dest.Math.Line3& line1) { }
        // RVA: 0x069186E0  token: 0x6000074
        public static System.Single Line3Line3(Dest.Math.Line3& line0, Dest.Math.Line3& line1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691AA3C  token: 0x6000075
        public static System.Single SqrLine3Line3(Dest.Math.Line3& line0, Dest.Math.Line3& line1) { }
        // RVA: 0x0691AA70  token: 0x6000076
        public static System.Single SqrLine3Line3(Dest.Math.Line3& line0, Dest.Math.Line3& line1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x06918710  token: 0x6000077
        public static System.Single Line3Ray3(Dest.Math.Line3& line, Dest.Math.Ray3& ray) { }
        // RVA: 0x06918744  token: 0x6000078
        public static System.Single Line3Ray3(Dest.Math.Line3& line, Dest.Math.Ray3& ray, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691B100  token: 0x6000079
        public static System.Single SqrLine3Ray3(Dest.Math.Line3& line, Dest.Math.Ray3& ray) { }
        // RVA: 0x0691ADC0  token: 0x600007A
        public static System.Single SqrLine3Ray3(Dest.Math.Line3& line, Dest.Math.Ray3& ray, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x06918774  token: 0x600007B
        public static System.Single Line3Segment3(Dest.Math.Line3& line, Dest.Math.Segment3& segment) { }
        // RVA: 0x069187A8  token: 0x600007C
        public static System.Single Line3Segment3(Dest.Math.Line3& line, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691B134  token: 0x600007D
        public static System.Single SqrLine3Segment3(Dest.Math.Line3& line, Dest.Math.Segment3& segment) { }
        // RVA: 0x0691B168  token: 0x600007E
        public static System.Single SqrLine3Segment3(Dest.Math.Line3& line, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x06918E54  token: 0x600007F
        public static System.Single Point3AAB3(UnityEngine.Vector3& point, Dest.Math.AAB3& box) { }
        // RVA: 0x06918EF8  token: 0x6000080
        public static System.Single Point3AAB3(UnityEngine.Vector3& point, Dest.Math.AAB3& box, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691C45C  token: 0x6000081
        public static System.Single SqrPoint3AAB3(UnityEngine.Vector3& point, Dest.Math.AAB3& box) { }
        // RVA: 0x0691C350  token: 0x6000082
        public static System.Single SqrPoint3AAB3(UnityEngine.Vector3& point, Dest.Math.AAB3& box, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x06919030  token: 0x6000083
        public static System.Single Point3Box3(UnityEngine.Vector3& point, Dest.Math.Box3& box) { }
        // RVA: 0x06919008  token: 0x6000084
        public static System.Single Point3Box3(UnityEngine.Vector3& point, Dest.Math.Box3& box, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691C4FC  token: 0x6000085
        public static System.Single SqrPoint3Box3(UnityEngine.Vector3& point, Dest.Math.Box3& box) { }
        // RVA: 0x0691C6D8  token: 0x6000086
        public static System.Single SqrPoint3Box3(UnityEngine.Vector3& point, Dest.Math.Box3& box, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x06919088  token: 0x6000087
        public static System.Single Point3Circle3(UnityEngine.Vector3& point, Dest.Math.Circle3& circle, System.Boolean solid) { }
        // RVA: 0x06919058  token: 0x6000088
        public static System.Single Point3Circle3(UnityEngine.Vector3& point, Dest.Math.Circle3& circle, UnityEngine.Vector3& closestPoint, System.Boolean solid) { }
        // RVA: 0x0691CE20  token: 0x6000089
        public static System.Single SqrPoint3Circle3(UnityEngine.Vector3& point, Dest.Math.Circle3& circle, System.Boolean solid) { }
        // RVA: 0x0691CA28  token: 0x600008A
        public static System.Single SqrPoint3Circle3(UnityEngine.Vector3& point, Dest.Math.Circle3& circle, UnityEngine.Vector3& closestPoint, System.Boolean solid) { }
        // RVA: 0x069190C8  token: 0x600008B
        public static System.Single Point3Line3(UnityEngine.Vector3& point, Dest.Math.Line3& line) { }
        // RVA: 0x069190F0  token: 0x600008C
        public static System.Single Point3Line3(UnityEngine.Vector3& point, Dest.Math.Line3& line, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691CFC4  token: 0x600008D
        public static System.Single SqrPoint3Line3(UnityEngine.Vector3& point, Dest.Math.Line3& line) { }
        // RVA: 0x0691CE48  token: 0x600008E
        public static System.Single SqrPoint3Line3(UnityEngine.Vector3& point, Dest.Math.Line3& line, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x06919234  token: 0x600008F
        public static System.Single Point3Plane3(UnityEngine.Vector3& point, Dest.Math.Plane3& plane) { }
        // RVA: 0x06919118  token: 0x6000090
        public static System.Single Point3Plane3(UnityEngine.Vector3& point, Dest.Math.Plane3& plane, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691D250  token: 0x6000091
        public static System.Single SqrPoint3Plane3(UnityEngine.Vector3& point, Dest.Math.Plane3& plane) { }
        // RVA: 0x0691D138  token: 0x6000092
        public static System.Single SqrPoint3Plane3(UnityEngine.Vector3& point, Dest.Math.Plane3& plane, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x069192FC  token: 0x6000093
        public static System.Single Point3Ray3(UnityEngine.Vector3& point, Dest.Math.Ray3& ray) { }
        // RVA: 0x069192D4  token: 0x6000094
        public static System.Single Point3Ray3(UnityEngine.Vector3& point, Dest.Math.Ray3& ray, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691D494  token: 0x6000095
        public static System.Single SqrPoint3Ray3(UnityEngine.Vector3& point, Dest.Math.Ray3& ray) { }
        // RVA: 0x0691D2F0  token: 0x6000096
        public static System.Single SqrPoint3Ray3(UnityEngine.Vector3& point, Dest.Math.Ray3& ray, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691934C  token: 0x6000097
        public static System.Single Point3Rectangle3(UnityEngine.Vector3& point, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x06919324  token: 0x6000098
        public static System.Single Point3Rectangle3(UnityEngine.Vector3& point, Dest.Math.Rectangle3& rectangle, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691D878  token: 0x6000099
        public static System.Single SqrPoint3Rectangle3(UnityEngine.Vector3& point, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x0691D614  token: 0x600009A
        public static System.Single SqrPoint3Rectangle3(UnityEngine.Vector3& point, Dest.Math.Rectangle3& rectangle, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691939C  token: 0x600009B
        public static System.Single Point3Segment3(UnityEngine.Vector3& point, Dest.Math.Segment3& segment) { }
        // RVA: 0x06919374  token: 0x600009C
        public static System.Single Point3Segment3(UnityEngine.Vector3& point, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691DBD8  token: 0x600009D
        public static System.Single SqrPoint3Segment3(UnityEngine.Vector3& point, Dest.Math.Segment3& segment) { }
        // RVA: 0x0691DA18  token: 0x600009E
        public static System.Single SqrPoint3Segment3(UnityEngine.Vector3& point, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x069194D8  token: 0x600009F
        public static System.Single Point3Sphere3(UnityEngine.Vector3& point, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x069193C4  token: 0x60000A0
        public static System.Single Point3Sphere3(UnityEngine.Vector3& point, Dest.Math.Sphere3& sphere, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691DD78  token: 0x60000A1
        public static System.Single SqrPoint3Sphere3(UnityEngine.Vector3& point, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x0691DDF0  token: 0x60000A2
        public static System.Single SqrPoint3Sphere3(UnityEngine.Vector3& point, Dest.Math.Sphere3& sphere, UnityEngine.Vector3& closestPoint) { }
        // RVA: 0x0691965C  token: 0x60000A3
        public static System.Single Ray3Ray3(Dest.Math.Ray3& ray0, Dest.Math.Ray3& ray1) { }
        // RVA: 0x0691962C  token: 0x60000A4
        public static System.Single Ray3Ray3(Dest.Math.Ray3& ray0, Dest.Math.Ray3& ray1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691E9E4  token: 0x60000A5
        public static System.Single SqrRay3Ray3(Dest.Math.Ray3& ray0, Dest.Math.Ray3& ray1) { }
        // RVA: 0x0691E5B4  token: 0x60000A6
        public static System.Single SqrRay3Ray3(Dest.Math.Ray3& ray0, Dest.Math.Ray3& ray1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x069196C0  token: 0x60000A7
        public static System.Single Ray3Segment3(Dest.Math.Ray3& ray, Dest.Math.Segment3& segment) { }
        // RVA: 0x06919690  token: 0x60000A8
        public static System.Single Ray3Segment3(Dest.Math.Ray3& ray, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691EE60  token: 0x60000A9
        public static System.Single SqrRay3Segment3(Dest.Math.Ray3& ray, Dest.Math.Segment3& segment) { }
        // RVA: 0x0691EA18  token: 0x60000AA
        public static System.Single SqrRay3Segment3(Dest.Math.Ray3& ray, Dest.Math.Segment3& segment, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x06919764  token: 0x60000AB
        public static System.Single Segment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x06919794  token: 0x60000AC
        public static System.Single Segment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box) { }
        // RVA: 0x0691F478  token: 0x60000AD
        public static System.Single SqrSegment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691F444  token: 0x60000AE
        public static System.Single SqrSegment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box) { }
        // RVA: 0x069197F8  token: 0x60000AF
        public static System.Single Segment3Segment3(Dest.Math.Segment3& segment0, Dest.Math.Segment3& segment1) { }
        // RVA: 0x069197C8  token: 0x60000B0
        public static System.Single Segment3Segment3(Dest.Math.Segment3& segment0, Dest.Math.Segment3& segment1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }
        // RVA: 0x0691F57C  token: 0x60000B1
        public static System.Single SqrSegment3Segment3(Dest.Math.Segment3& segment0, Dest.Math.Segment3& segment1) { }
        // RVA: 0x0691F5B0  token: 0x60000B2
        public static System.Single SqrSegment3Segment3(Dest.Math.Segment3& segment0, Dest.Math.Segment3& segment1, UnityEngine.Vector3& closestPoint0, UnityEngine.Vector3& closestPoint1) { }

    }

    // TypeToken: 0x2000012  // size: 0x2C
    public sealed struct Line3Box3Dist
    {
        // Fields
        public UnityEngine.Vector3 ClosestPoint0;  // 0x10
        public UnityEngine.Vector3 ClosestPoint1;  // 0x1c
        public System.Single LineParameter;  // 0x28

    }

    // TypeToken: 0x2000013  // size: 0x10
    public static class Intersection
    {
        // Fields
        private static System.Single _intervalThreshold;  // static @ 0x0
        private static System.Single _dotThreshold;  // static @ 0x4
        private static System.Single _distanceThreshold;  // static @ 0x8

        // Properties
        System.Single IntervalThreshold { get; /* RVA: 0x069344B0 */ set; /* RVA: 0x06934630 */ }
        System.Single DotThreshold { get; /* RVA: 0x06934460 */ set; /* RVA: 0x06934598 */ }
        System.Single DistanceThreshold { get; /* RVA: 0x06934410 */ set; /* RVA: 0x06934500 */ }

        // Methods
        // RVA: 0x0692C508  token: 0x60000B3
        public static System.Boolean TestAAB2AAB2(Dest.Math.AAB2& box0, Dest.Math.AAB2& box1) { }
        // RVA: 0x06922EDC  token: 0x60000B4
        public static System.Boolean FindAAB2AAB2(Dest.Math.AAB2& box0, Dest.Math.AAB2& box1, Dest.Math.AAB2& intersection) { }
        // RVA: 0x0692C4D0  token: 0x60000B5
        public static System.Boolean TestAAB2AAB2OverlapX(Dest.Math.AAB2& box0, Dest.Math.AAB2& box1) { }
        // RVA: 0x0692C4EC  token: 0x60000B6
        public static System.Boolean TestAAB2AAB2OverlapY(Dest.Math.AAB2& box0, Dest.Math.AAB2& box1) { }
        // RVA: 0x0355A4A0  token: 0x60000B7
        public static System.Boolean TestAAB2Circle2(Dest.Math.AAB2& box, Dest.Math.Circle2& circle) { }
        // RVA: 0x0692C5D4  token: 0x60000B8
        public static System.Boolean TestBox2Box2(Dest.Math.Box2& box0, Dest.Math.Box2& box1) { }
        // RVA: 0x0692C8A4  token: 0x60000B9
        public static System.Boolean TestBox2Circle2(Dest.Math.Box2& box, Dest.Math.Circle2& circle) { }
        // RVA: 0x0692D6C0  token: 0x60000BA
        public static System.Boolean TestCircle2Circle2(Dest.Math.Circle2& circle0, Dest.Math.Circle2& circle1) { }
        // RVA: 0x06923054  token: 0x60000BB
        public static System.Boolean FindCircle2Circle2(Dest.Math.Circle2& circle0, Dest.Math.Circle2& circle1, Dest.Math.Circle2Circle2Intr& info) { }
        // RVA: 0x0693410C  token: 0x60000BC
        private static System.Int32 WhichSide(Dest.Math.Polygon2 V, UnityEngine.Vector2 P, UnityEngine.Vector2& D) { }
        // RVA: 0x0692D710  token: 0x60000BD
        public static System.Boolean TestConvexPolygon2ConvexPolygon2(Dest.Math.Polygon2 convexPolygon0, Dest.Math.Polygon2 convexPolygon1) { }
        // RVA: 0x06922BFC  token: 0x60000BE
        private static System.Boolean DoClipping(System.Single t0, System.Single t1, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, Dest.Math.AAB2& box, System.Boolean solid, System.Int32& quantity, UnityEngine.Vector2& point0, UnityEngine.Vector2& point1, Dest.Math.IntersectionTypes& intrType) { }
        // RVA: 0x0692D908  token: 0x60000BF
        public static System.Boolean TestLine2AAB2(Dest.Math.Line2& line, Dest.Math.AAB2& box) { }
        // RVA: 0x069232AC  token: 0x60000C0
        public static System.Boolean FindLine2AAB2(Dest.Math.Line2& line, Dest.Math.AAB2& box, Dest.Math.Line2AAB2Intr& info) { }
        // RVA: 0x06921AB0  token: 0x60000C1
        private static System.Boolean Clip(System.Single denom, System.Single numer, System.Single& t0, System.Single& t1) { }
        // RVA: 0x069222A0  token: 0x60000C2
        private static System.Boolean DoClipping(System.Single t0, System.Single t1, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, Dest.Math.Box2& box, System.Boolean solid, System.Int32& quantity, UnityEngine.Vector2& point0, UnityEngine.Vector2& point1, Dest.Math.IntersectionTypes& intrType) { }
        // RVA: 0x0692DA60  token: 0x60000C3
        public static System.Boolean TestLine2Box2(Dest.Math.Line2& line, Dest.Math.Box2& box) { }
        // RVA: 0x06923348  token: 0x60000C4
        public static System.Boolean FindLine2Box2(Dest.Math.Line2& line, Dest.Math.Box2& box, Dest.Math.Line2Box2Intr& info) { }
        // RVA: 0x0692B284  token: 0x60000C5
        private static System.Boolean Find(UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, UnityEngine.Vector2& center, System.Single radius, System.Int32& rootCount, System.Single& t0, System.Single& t1) { }
        // RVA: 0x0692DB50  token: 0x60000C6
        public static System.Boolean TestLine2Circle2(Dest.Math.Line2& line, Dest.Math.Circle2& circle) { }
        // RVA: 0x069233E4  token: 0x60000C7
        public static System.Boolean FindLine2Circle2(Dest.Math.Line2& line, Dest.Math.Circle2& circle, Dest.Math.Line2Circle2Intr& info) { }
        // RVA: 0x0692DBD8  token: 0x60000C8
        public static System.Boolean TestLine2ConvexPolygon2(Dest.Math.Line2& line, Dest.Math.Polygon2 convexPolygon) { }
        // RVA: 0x06923590  token: 0x60000C9
        public static System.Boolean FindLine2ConvexPolygon2(Dest.Math.Line2& line, Dest.Math.Polygon2 convexPolygon, Dest.Math.Line2ConvexPolygon2Intr& info) { }
        // RVA: 0x069206C0  token: 0x60000CA
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Line2& line0, Dest.Math.Line2& line1, System.Single& s0) { }
        // RVA: 0x0692DD40  token: 0x60000CB
        public static System.Boolean TestLine2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692DCE4  token: 0x60000CC
        public static System.Boolean TestLine2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1) { }
        // RVA: 0x069238DC  token: 0x60000CD
        public static System.Boolean FindLine2Line2(Dest.Math.Line2& line0, Dest.Math.Line2& line1, Dest.Math.Line2Line2Intr& info) { }
        // RVA: 0x06920E0C  token: 0x60000CE
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Line2& line, Dest.Math.Ray2& ray, System.Single& s0, System.Single& s1) { }
        // RVA: 0x0692DE0C  token: 0x60000CF
        public static System.Boolean TestLine2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692DDB0  token: 0x60000D0
        public static System.Boolean TestLine2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray) { }
        // RVA: 0x069239B0  token: 0x60000D1
        public static System.Boolean FindLine2Ray2(Dest.Math.Line2& line, Dest.Math.Ray2& ray, Dest.Math.Line2Ray2Intr& info) { }
        // RVA: 0x06921014  token: 0x60000D2
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Segment2& segment, Dest.Math.Line2& line, System.Single& s0, System.Single& s1) { }
        // RVA: 0x0692DF20  token: 0x60000D3
        public static System.Boolean TestLine2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692DEC4  token: 0x60000D4
        public static System.Boolean TestLine2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment) { }
        // RVA: 0x06923AF0  token: 0x60000D5
        public static System.Boolean FindLine2Segment2(Dest.Math.Line2& line, Dest.Math.Segment2& segment, Dest.Math.Line2Segment2Intr& info) { }
        // RVA: 0x06933C74  token: 0x60000D6
        private static System.Void TriangleLineRelations(UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, Dest.Math.Triangle2& triangle, System.Single& dist0, System.Single& dist1, System.Single& dist2, System.Int32& sign0, System.Int32& sign1, System.Int32& sign2, System.Int32& positive, System.Int32& negative, System.Int32& zero) { }
        // RVA: 0x0692B9F4  token: 0x60000D7
        private static System.Boolean GetInterval(UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, Dest.Math.Triangle2& triangle, System.Single dist0, System.Single dist1, System.Single dist2, System.Int32 sign0, System.Int32 sign1, System.Int32 sign2, System.Single& param0, System.Single& param1) { }
        // RVA: 0x0692DFBC  token: 0x60000D8
        public static System.Boolean TestLine2Triangle2(Dest.Math.Line2& line, Dest.Math.Triangle2& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692E184  token: 0x60000D9
        public static System.Boolean TestLine2Triangle2(Dest.Math.Line2& line, Dest.Math.Triangle2& triangle) { }
        // RVA: 0x06923C14  token: 0x60000DA
        public static System.Boolean FindLine2Triangle2(Dest.Math.Line2& line, Dest.Math.Triangle2& triangle, Dest.Math.Line2Triangle2Intr& info) { }
        // RVA: 0x0692F508  token: 0x60000DB
        public static System.Boolean TestRay2AAB2(Dest.Math.Ray2& ray, Dest.Math.AAB2& box) { }
        // RVA: 0x06925B48  token: 0x60000DC
        public static System.Boolean FindRay2AAB2(Dest.Math.Ray2& ray, Dest.Math.AAB2& box, Dest.Math.Ray2AAB2Intr& info) { }
        // RVA: 0x0692F630  token: 0x60000DD
        public static System.Boolean TestRay2Box2(Dest.Math.Ray2& ray, Dest.Math.Box2& box) { }
        // RVA: 0x06925BE0  token: 0x60000DE
        public static System.Boolean FindRay2Box2(Dest.Math.Ray2& ray, Dest.Math.Box2& box, Dest.Math.Ray2Box2Intr& info) { }
        // RVA: 0x0692F81C  token: 0x60000DF
        public static System.Boolean TestRay2Circle2(Dest.Math.Ray2& ray, Dest.Math.Circle2& circle) { }
        // RVA: 0x06925C78  token: 0x60000E0
        public static System.Boolean FindRay2Circle2(Dest.Math.Ray2& ray, Dest.Math.Circle2& circle, Dest.Math.Ray2Circle2Intr& info) { }
        // RVA: 0x0692F8B0  token: 0x60000E1
        public static System.Boolean TestRay2ConvexPolygon2(Dest.Math.Ray2& ray, Dest.Math.Polygon2 convexPolygon) { }
        // RVA: 0x06925E54  token: 0x60000E2
        public static System.Boolean FindRay2ConvexPolygon2(Dest.Math.Ray2& ray, Dest.Math.Polygon2 convexPolygon, Dest.Math.Ray2ConvexPolygon2Intr& info) { }
        // RVA: 0x0692F9BC  token: 0x60000E3
        public static System.Boolean TestRay2Polygon2(Dest.Math.Ray2& ray, Dest.Math.Polygon2 polygon) { }
        // RVA: 0x0692FAC8  token: 0x60000E4
        public static System.Boolean TestRay2Polygon2(Dest.Math.Ray2& ray, Dest.Math.Segment2[] segments) { }
        // RVA: 0x06926318  token: 0x60000E5
        public static System.Boolean FindRay2Polygon2(Dest.Math.Ray2& ray, Dest.Math.Polygon2 polygon, Dest.Math.Ray2Polygon2Intr& info) { }
        // RVA: 0x0692619C  token: 0x60000E6
        public static System.Boolean FindRay2Polygon2(Dest.Math.Ray2& ray, Dest.Math.Segment2[] segments, Dest.Math.Ray2Polygon2Intr& info) { }
        // RVA: 0x0692047C  token: 0x60000E7
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1, System.Single& s0, System.Single& s1) { }
        // RVA: 0x0692FB68  token: 0x60000E8
        public static System.Boolean TestRay2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692FCC4  token: 0x60000E9
        public static System.Boolean TestRay2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1) { }
        // RVA: 0x06926510  token: 0x60000EA
        public static System.Boolean FindRay2Ray2(Dest.Math.Ray2& ray0, Dest.Math.Ray2& ray1, Dest.Math.Ray2Ray2Intr& info) { }
        // RVA: 0x06920B60  token: 0x60000EB
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment, System.Single& s0, System.Single& s1) { }
        // RVA: 0x0692FD7C  token: 0x60000EC
        public static System.Boolean TestRay2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692FD20  token: 0x60000ED
        public static System.Boolean TestRay2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment) { }
        // RVA: 0x06926714  token: 0x60000EE
        public static System.Boolean FindRay2Segment2(Dest.Math.Ray2& ray, Dest.Math.Segment2& segment, Dest.Math.Ray2Segment2Intr& info) { }
        // RVA: 0x0692FEC8  token: 0x60000EF
        public static System.Boolean TestRay2Triangle2(Dest.Math.Ray2& ray, Dest.Math.Triangle2& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0693008C  token: 0x60000F0
        public static System.Boolean TestRay2Triangle2(Dest.Math.Ray2& ray, Dest.Math.Triangle2& triangle) { }
        // RVA: 0x06926924  token: 0x60000F1
        public static System.Boolean FindRay2Triangle2(Dest.Math.Ray2& ray, Dest.Math.Triangle2& triangle, Dest.Math.Ray2Triangle2Intr& info) { }
        // RVA: 0x06931154  token: 0x60000F2
        public static System.Boolean TestSegment2AAB2(Dest.Math.Segment2& segment, Dest.Math.AAB2& box) { }
        // RVA: 0x069280C8  token: 0x60000F3
        public static System.Boolean FindSegment2AAB2(Dest.Math.Segment2& segment, Dest.Math.AAB2& box, Dest.Math.Segment2AAB2Intr& info) { }
        // RVA: 0x06931294  token: 0x60000F4
        public static System.Boolean TestSegment2Box2(Dest.Math.Segment2& segment, Dest.Math.Box2& box) { }
        // RVA: 0x06928188  token: 0x60000F5
        public static System.Boolean FindSegment2Box2(Dest.Math.Segment2& segment, Dest.Math.Box2& box, Dest.Math.Segment2Box2Intr& info) { }
        // RVA: 0x06931494  token: 0x60000F6
        public static System.Boolean TestSegment2Circle2(Dest.Math.Segment2& segment, Dest.Math.Circle2& circle) { }
        // RVA: 0x06928248  token: 0x60000F7
        public static System.Boolean FindSegment2Circle2(Dest.Math.Segment2& segment, Dest.Math.Circle2& circle, Dest.Math.Segment2Circle2Intr& info) { }
        // RVA: 0x06931568  token: 0x60000F8
        public static System.Boolean TestSegment2ConvexPolygon2(Dest.Math.Segment2& segment, Dest.Math.Polygon2 convexPolygon) { }
        // RVA: 0x069284B0  token: 0x60000F9
        public static System.Boolean FindSegment2ConvexPolygon2(Dest.Math.Segment2& segment, Dest.Math.Polygon2 convexPolygon, Dest.Math.Segment2ConvexPolygon2Intr& info) { }
        // RVA: 0x06920888  token: 0x60000FA
        private static Dest.Math.IntersectionTypes Classify(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1, System.Single& s0, System.Single& s1) { }
        // RVA: 0x06931628  token: 0x60000FB
        public static System.Boolean TestSegment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x069315CC  token: 0x60000FC
        public static System.Boolean TestSegment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1) { }
        // RVA: 0x069287F4  token: 0x60000FD
        public static System.Boolean FindSegment2Segment2(Dest.Math.Segment2& segment0, Dest.Math.Segment2& segment1, Dest.Math.Segment2Segment2Intr& info) { }
        // RVA: 0x069317E8  token: 0x60000FE
        public static System.Boolean TestSegment2Triangle2(Dest.Math.Segment2& segment, Dest.Math.Triangle2& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0693178C  token: 0x60000FF
        public static System.Boolean TestSegment2Triangle2(Dest.Math.Segment2& segment, Dest.Math.Triangle2& triangle) { }
        // RVA: 0x06928B10  token: 0x6000100
        public static System.Boolean FindSegment2Triangle2(Dest.Math.Segment2& segment, Dest.Math.Triangle2& triangle, Dest.Math.Segment2Triangle2Intr& info) { }
        // RVA: 0x0693423C  token: 0x6000101
        private static System.Int32 WhichSide(Dest.Math.Triangle2& triangle, UnityEngine.Vector2& P, UnityEngine.Vector2& D) { }
        // RVA: 0x0692122C  token: 0x6000102
        private static System.Void ClipConvexPolygonAgainstLine(UnityEngine.Vector2& edgeStart, UnityEngine.Vector2& edgeEnd, System.Int32& quantity, Dest.Math.Triangle2Triangle2Intr& info) { }
        // RVA: 0x06932B18  token: 0x6000103
        public static System.Boolean TestTriangle2Triangle2(Dest.Math.Triangle2& triangle0, Dest.Math.Triangle2& triangle1) { }
        // RVA: 0x0692A844  token: 0x6000104
        public static System.Boolean FindTriangle2Triangle2(Dest.Math.Triangle2& triangle0, Dest.Math.Triangle2& triangle1, Dest.Math.Triangle2Triangle2Intr& info) { }
        // RVA: 0x0692C58C  token: 0x6000105
        public static System.Boolean TestAAB3AAB3(Dest.Math.AAB3& box0, Dest.Math.AAB3& box1) { }
        // RVA: 0x06922F68  token: 0x6000106
        public static System.Boolean FindAAB3AAB3(Dest.Math.AAB3& box0, Dest.Math.AAB3& box1, Dest.Math.AAB3& intersection) { }
        // RVA: 0x0692C538  token: 0x6000107
        public static System.Boolean TestAAB3AAB3OverlapX(Dest.Math.AAB3& box0, Dest.Math.AAB3& box1) { }
        // RVA: 0x0692C554  token: 0x6000108
        public static System.Boolean TestAAB3AAB3OverlapY(Dest.Math.AAB3& box0, Dest.Math.AAB3& box1) { }
        // RVA: 0x0692C570  token: 0x6000109
        public static System.Boolean TestAAB3AAB3OverlapZ(Dest.Math.AAB3& box0, Dest.Math.AAB3& box1) { }
        // RVA: 0x033A4D40  token: 0x600010A
        public static System.Boolean TestAAB3Sphere3(Dest.Math.AAB3& box, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x0692C9DC  token: 0x600010B
        public static System.Boolean TestBox3Box3(Dest.Math.Box3& box0, Dest.Math.Box3& box1) { }
        // RVA: 0x0692D494  token: 0x600010C
        public static System.Boolean TestBox3Capsule3(Dest.Math.Box3& box, Dest.Math.Capsule3& capsule) { }
        // RVA: 0x0692D4BC  token: 0x600010D
        public static System.Boolean TestBox3Sphere3(Dest.Math.Box3& box, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x06921E50  token: 0x600010E
        private static System.Boolean DoClipping(System.Single t0, System.Single t1, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, Dest.Math.AAB3& box, System.Boolean solid, System.Int32& quantity, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, Dest.Math.IntersectionTypes& intrType) { }
        // RVA: 0x0692E1E0  token: 0x600010F
        public static System.Boolean TestLine3AAB3(Dest.Math.Line3& line, Dest.Math.AAB3& box) { }
        // RVA: 0x06923EB4  token: 0x6000110
        public static System.Boolean FindLine3AAB3(Dest.Math.Line3& line, Dest.Math.AAB3& box, Dest.Math.Line3AAB3Intr& info) { }
        // RVA: 0x06922604  token: 0x6000111
        private static System.Boolean DoClipping(System.Single t0, System.Single t1, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, Dest.Math.Box3& box, System.Boolean solid, System.Int32& quantity, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, Dest.Math.IntersectionTypes& intrType) { }
        // RVA: 0x0692E37C  token: 0x6000112
        public static System.Boolean TestLine3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box) { }
        // RVA: 0x06923F50  token: 0x6000113
        public static System.Boolean FindLine3Box3(Dest.Math.Line3& line, Dest.Math.Box3& box, Dest.Math.Line3Box3Intr& info) { }
        // RVA: 0x0692E63C  token: 0x6000114
        public static System.Boolean TestLine3Circle3(Dest.Math.Line3& line, Dest.Math.Circle3& circle) { }
        // RVA: 0x06923FEC  token: 0x6000115
        public static System.Boolean FindLine3Circle3(Dest.Math.Line3& line, Dest.Math.Circle3& circle, Dest.Math.Line3Circle3Intr& info) { }
        // RVA: 0x0692E69C  token: 0x6000116
        public static System.Boolean TestLine3Plane3(Dest.Math.Line3& line, Dest.Math.Plane3& plane, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692E7D4  token: 0x6000117
        public static System.Boolean TestLine3Plane3(Dest.Math.Line3& line, Dest.Math.Plane3& plane) { }
        // RVA: 0x06924200  token: 0x6000118
        public static System.Boolean FindLine3Plane3(Dest.Math.Line3& line, Dest.Math.Plane3& plane, Dest.Math.Line3Plane3Intr& info) { }
        // RVA: 0x0692E830  token: 0x6000119
        public static System.Boolean TestLine3Polygon3(Dest.Math.Line3& line, Dest.Math.Polygon3 polygon) { }
        // RVA: 0x06924380  token: 0x600011A
        public static System.Boolean FindLine3Polygon3(Dest.Math.Line3& line, Dest.Math.Polygon3 polygon, Dest.Math.Line3Polygon3Intr& info) { }
        // RVA: 0x0692C110  token: 0x600011B
        private static System.Boolean Point3InsideRectangle3(UnityEngine.Vector3& point, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x0692E890  token: 0x600011C
        public static System.Boolean TestLine3Rectangle3(Dest.Math.Line3& line, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x06924560  token: 0x600011D
        public static System.Boolean FindLine3Rectangle3(Dest.Math.Line3& line, Dest.Math.Rectangle3& rectangle, Dest.Math.Line3Rectangle3Intr& info) { }
        // RVA: 0x0692E8F0  token: 0x600011E
        public static System.Boolean TestLine3Sphere3(Dest.Math.Line3& line, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x06924748  token: 0x600011F
        public static System.Boolean FindLine3Sphere3(Dest.Math.Line3& line, Dest.Math.Sphere3& sphere, Dest.Math.Line3Sphere3Intr& info) { }
        // RVA: 0x0692ED68  token: 0x6000120
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, Dest.Math.Triangle3& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692ECA4  token: 0x6000121
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692EB20  token: 0x6000122
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0692EA04  token: 0x6000123
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, Dest.Math.Triangle3& triangle) { }
        // RVA: 0x0692EBE4  token: 0x6000124
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x0692EA60  token: 0x6000125
        public static System.Boolean TestLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06924BC8  token: 0x6000126
        public static System.Boolean FindLine3Triangle3(Dest.Math.Line3& line, Dest.Math.Triangle3& triangle, Dest.Math.Line3Triangle3Intr& info) { }
        // RVA: 0x06924A40  token: 0x6000127
        public static System.Boolean FindLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.Line3Triangle3Intr& info) { }
        // RVA: 0x06924B04  token: 0x6000128
        public static System.Boolean FindLine3Triangle3(Dest.Math.Line3& line, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.Line3Triangle3Intr& info) { }
        // RVA: 0x0692F098  token: 0x6000129
        public static System.Boolean TestPlane3AAB3(Dest.Math.Plane3& plane, Dest.Math.AAB3& box) { }
        // RVA: 0x0692F168  token: 0x600012A
        public static System.Boolean TestPlane3Box3(Dest.Math.Plane3& plane, Dest.Math.Box3& box) { }
        // RVA: 0x0692F2D4  token: 0x600012B
        public static System.Boolean TestPlane3Plane3(Dest.Math.Plane3& plane0, Dest.Math.Plane3& plane1) { }
        // RVA: 0x06924FD0  token: 0x600012C
        public static System.Boolean FindPlane3Plane3(Dest.Math.Plane3& plane0, Dest.Math.Plane3& plane1, Dest.Math.Plane3Plane3Intr& info) { }
        // RVA: 0x0692F374  token: 0x600012D
        public static System.Boolean TestPlane3Sphere3(Dest.Math.Plane3& plane, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x0692520C  token: 0x600012E
        public static System.Boolean FindPlane3Sphere3(Dest.Math.Plane3& plane, Dest.Math.Sphere3& sphere, Dest.Math.Plane3Sphere3Intr& info) { }
        // RVA: 0x0692F3A8  token: 0x600012F
        public static System.Boolean TestPlane3Triangle3(Dest.Math.Plane3& plane, Dest.Math.Triangle3& triangle) { }
        // RVA: 0x0692538C  token: 0x6000130
        public static System.Boolean FindPlane3Triangle3(Dest.Math.Plane3& plane, Dest.Math.Triangle3& triangle, Dest.Math.Plane3Triangle3Intr& info) { }
        // RVA: 0x069300E8  token: 0x6000131
        public static System.Boolean TestRay3AAB3(Dest.Math.Ray3& ray, Dest.Math.AAB3& box) { }
        // RVA: 0x06926BC0  token: 0x6000132
        public static System.Boolean FindRay3AAB3(Dest.Math.Ray3& ray, Dest.Math.AAB3& box, Dest.Math.Ray3AAB3Intr& info) { }
        // RVA: 0x06930334  token: 0x6000133
        public static System.Boolean TestRay3Box3(Dest.Math.Ray3& ray, Dest.Math.Box3& box) { }
        // RVA: 0x06926C58  token: 0x6000134
        public static System.Boolean FindRay3Box3(Dest.Math.Ray3& ray, Dest.Math.Box3& box, Dest.Math.Ray3Box3Intr& info) { }
        // RVA: 0x069306FC  token: 0x6000135
        public static System.Boolean TestRay3Circle3(Dest.Math.Ray3& ray, Dest.Math.Circle3& circle) { }
        // RVA: 0x06926CF0  token: 0x6000136
        public static System.Boolean FindRay3Circle3(Dest.Math.Ray3& ray, Dest.Math.Circle3& circle, Dest.Math.Ray3Circle3Intr& info) { }
        // RVA: 0x069307C0  token: 0x6000137
        public static System.Boolean TestRay3Plane3(Dest.Math.Ray3& ray, Dest.Math.Plane3& plane, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x0693075C  token: 0x6000138
        public static System.Boolean TestRay3Plane3(Dest.Math.Ray3& ray, Dest.Math.Plane3& plane) { }
        // RVA: 0x06926F78  token: 0x6000139
        public static System.Boolean FindRay3Plane3(Dest.Math.Ray3& ray, Dest.Math.Plane3& plane, Dest.Math.Ray3Plane3Intr& info) { }
        // RVA: 0x06930838  token: 0x600013A
        public static System.Boolean TestRay3Polygon3(Dest.Math.Ray3& ray, Dest.Math.Polygon3 polygon) { }
        // RVA: 0x0692717C  token: 0x600013B
        public static System.Boolean FindRay3Polygon3(Dest.Math.Ray3& ray, Dest.Math.Polygon3 polygon, Dest.Math.Ray3Polygon3Intr& info) { }
        // RVA: 0x06930898  token: 0x600013C
        public static System.Boolean TestRay3Rectangle3(Dest.Math.Ray3& ray, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x069273D4  token: 0x600013D
        public static System.Boolean FindRay3Rectangle3(Dest.Math.Ray3& ray, Dest.Math.Rectangle3& rectangle, Dest.Math.Ray3Rectangle3Intr& info) { }
        // RVA: 0x069308F8  token: 0x600013E
        public static System.Boolean TestRay3Sphere3(Dest.Math.Ray3& ray, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x06927628  token: 0x600013F
        public static System.Boolean FindRay3Sphere3(Dest.Math.Ray3& ray, Dest.Math.Sphere3& sphere, Dest.Math.Ray3Sphere3Intr& info) { }
        // RVA: 0x06930DA4  token: 0x6000140
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, Dest.Math.Triangle3& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06930A40  token: 0x6000141
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06930B04  token: 0x6000142
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06930C88  token: 0x6000143
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, Dest.Math.Triangle3& triangle) { }
        // RVA: 0x06930CE4  token: 0x6000144
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x06930BC8  token: 0x6000145
        public static System.Boolean TestRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06927AB8  token: 0x6000146
        public static System.Boolean FindRay3Triangle3(Dest.Math.Ray3& ray, Dest.Math.Triangle3& triangle, Dest.Math.Ray3Triangle3Intr& info) { }
        // RVA: 0x069279F4  token: 0x6000147
        public static System.Boolean FindRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.Ray3Triangle3Intr& info) { }
        // RVA: 0x06927EF0  token: 0x6000148
        public static System.Boolean FindRay3Triangle3(Dest.Math.Ray3& ray, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.Ray3Triangle3Intr& info) { }
        // RVA: 0x069319D0  token: 0x6000149
        public static System.Boolean TestSegment3AAB3(Dest.Math.Segment3& segment, Dest.Math.AAB3& box) { }
        // RVA: 0x06928E20  token: 0x600014A
        public static System.Boolean FindSegment3AAB3(Dest.Math.Segment3& segment, Dest.Math.AAB3& box, Dest.Math.Segment3AAB3Intr& info) { }
        // RVA: 0x06931C1C  token: 0x600014B
        public static System.Boolean TestSegment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box) { }
        // RVA: 0x06928EE0  token: 0x600014C
        public static System.Boolean FindSegment3Box3(Dest.Math.Segment3& segment, Dest.Math.Box3& box, Dest.Math.Segment3Box3Intr& info) { }
        // RVA: 0x06931FD0  token: 0x600014D
        public static System.Boolean TestSegment3Circle3(Dest.Math.Segment3& segment, Dest.Math.Circle3& circle) { }
        // RVA: 0x06928FA0  token: 0x600014E
        public static System.Boolean FindSegment3Circle3(Dest.Math.Segment3& segment, Dest.Math.Circle3& circle, Dest.Math.Segment3Circle3Intr& info) { }
        // RVA: 0x0693208C  token: 0x600014F
        public static System.Boolean TestSegment3Plane3(Dest.Math.Segment3& segment, Dest.Math.Plane3& plane, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06932030  token: 0x6000150
        public static System.Boolean TestSegment3Plane3(Dest.Math.Segment3& segment, Dest.Math.Plane3& plane) { }
        // RVA: 0x06929230  token: 0x6000151
        public static System.Boolean FindSegment3Plane3(Dest.Math.Segment3& segment, Dest.Math.Plane3& plane, Dest.Math.Segment3Plane3Intr& info) { }
        // RVA: 0x069321D8  token: 0x6000152
        public static System.Boolean TestSegment3Polygon3(Dest.Math.Segment3& segment, Dest.Math.Polygon3 polygon) { }
        // RVA: 0x06929468  token: 0x6000153
        public static System.Boolean FindSegment3Polygon3(Dest.Math.Segment3& segment, Dest.Math.Polygon3 polygon, Dest.Math.Segment3Polygon3Intr& info) { }
        // RVA: 0x06932238  token: 0x6000154
        public static System.Boolean TestSegment3Rectangle3(Dest.Math.Segment3& segment, Dest.Math.Rectangle3& rectangle) { }
        // RVA: 0x069296BC  token: 0x6000155
        public static System.Boolean FindSegment3Rectangle3(Dest.Math.Segment3& segment, Dest.Math.Rectangle3& rectangle, Dest.Math.Segment3Rectangle3Intr& info) { }
        // RVA: 0x06932298  token: 0x6000156
        public static System.Boolean TestSegment3Sphere3(Dest.Math.Segment3& segment, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x0692991C  token: 0x6000157
        public static System.Boolean FindSegment3Sphere3(Dest.Math.Segment3& segment, Dest.Math.Sphere3& sphere, Dest.Math.Segment3Sphere3Intr& info) { }
        // RVA: 0x06932714  token: 0x6000158
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, Dest.Math.Triangle3& triangle, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06932650  token: 0x6000159
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x069324CC  token: 0x600015A
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06932ABC  token: 0x600015B
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, Dest.Math.Triangle3& triangle) { }
        // RVA: 0x06932590  token: 0x600015C
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x0693240C  token: 0x600015D
        public static System.Boolean TestSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06929E4C  token: 0x600015E
        public static System.Boolean FindSegment3Triangle3(Dest.Math.Segment3& segment, Dest.Math.Triangle3& triangle, Dest.Math.Segment3Triangle3Intr& info) { }
        // RVA: 0x0692A2D4  token: 0x600015F
        public static System.Boolean FindSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, Dest.Math.Segment3Triangle3Intr& info) { }
        // RVA: 0x06929D88  token: 0x6000160
        public static System.Boolean FindSegment3Triangle3(Dest.Math.Segment3& segment, UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.Segment3Triangle3Intr& info) { }
        // RVA: 0x049E3D50  token: 0x6000161
        public static System.Boolean TestSphere3Sphere3(Dest.Math.Sphere3& sphere0, Dest.Math.Sphere3& sphere1) { }
        // RVA: 0x0692A398  token: 0x6000162
        public static System.Boolean FindSphere3Sphere3(Dest.Math.Sphere3& sphere0, Dest.Math.Sphere3& sphere1, Dest.Math.Sphere3Sphere3Intr& info) { }
        // RVA: 0x0692C23C  token: 0x6000163
        private static System.Void ProjectOntoAxis(Dest.Math.Triangle3& triangle, UnityEngine.Vector3& axis, System.Single& fmin, System.Single& fmax) { }
        // RVA: 0x06933FB0  token: 0x6000164
        private static System.Void TrianglePlaneRelations(Dest.Math.Triangle3& triangle, Dest.Math.Plane3& plane, System.Single& dist0, System.Single& dist1, System.Single& dist2, System.Int32& sign0, System.Int32& sign1, System.Int32& sign2, System.Int32& positive, System.Int32& negative, System.Int32& zero) { }
        // RVA: 0x06933EFC  token: 0x6000165
        private static System.Boolean TrianglePlaneRelationsQuick(Dest.Math.Triangle3& triangle, Dest.Math.Plane3& plane) { }
        // RVA: 0x0692BC60  token: 0x6000166
        private static System.Boolean IntersectsSegment(Dest.Math.Plane3& plane, Dest.Math.Triangle3& triangle, UnityEngine.Vector3& end0, UnityEngine.Vector3& end1, System.Boolean grazing, Dest.Math.Triangle3Triangle3Intr& info) { }
        // RVA: 0x0692C3A0  token: 0x6000167
        private static System.Int32 QueryToLine(UnityEngine.Vector2& test, UnityEngine.Vector2& vec0, UnityEngine.Vector2& vec1) { }
        // RVA: 0x0692C3F4  token: 0x6000168
        private static System.Int32 QueryToTriangle(UnityEngine.Vector2& test, UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2) { }
        // RVA: 0x06921B10  token: 0x6000169
        private static System.Boolean ContainsPoint(Dest.Math.Triangle3& triangle, Dest.Math.Plane3& plane, UnityEngine.Vector3& point, Dest.Math.Triangle3Triangle3Intr& info) { }
        // RVA: 0x0692B3DC  token: 0x600016A
        private static System.Boolean GetCoplanarIntersection(Dest.Math.Plane3& plane, Dest.Math.Triangle3& tri0, Dest.Math.Triangle3& tri1, Dest.Math.Triangle3Triangle3Intr& info) { }
        // RVA: 0x06932D64  token: 0x600016B
        public static System.Boolean TestTriangle3Triangle3(Dest.Math.Triangle3& triangle0, Dest.Math.Triangle3& triangle1, Dest.Math.IntersectionTypes& intersectionType) { }
        // RVA: 0x06933C18  token: 0x600016C
        public static System.Boolean TestTriangle3Triangle3(Dest.Math.Triangle3& triangle0, Dest.Math.Triangle3& triangle1) { }
        // RVA: 0x0692A98C  token: 0x600016D
        public static System.Boolean FindTriangle3Triangle3(Dest.Math.Triangle3& triangle0, Dest.Math.Triangle3& triangle1, Dest.Math.Triangle3Triangle3Intr& info, System.Boolean reportCoplanarIntersections) { }
        // RVA: 0x04D532A0  token: 0x6000174
        private static System.Void .cctor() { }
        // RVA: 0x06927FB4  token: 0x6000175
        public static System.Int32 FindSegment1Segment1(System.Single seg0Start, System.Single seg0End, System.Single seg1Start, System.Single seg1End, System.Single& w0, System.Single& w1) { }

    }

    // TypeToken: 0x2000014  // size: 0x28
    public sealed struct Circle2Circle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000015  // size: 0x28
    public sealed struct Line2AAB2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000016  // size: 0x28
    public sealed struct Line2Box2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000017  // size: 0x24
    public sealed struct Line2Circle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c

    }

    // TypeToken: 0x2000018  // size: 0x30
    public sealed struct Line2ConvexPolygon2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20
        public System.Single Parameter0;  // 0x28
        public System.Single Parameter1;  // 0x2c

    }

    // TypeToken: 0x2000019  // size: 0x20
    public sealed struct Line2Line2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point;  // 0x14
        public System.Single Parameter;  // 0x1c

    }

    // TypeToken: 0x200001A  // size: 0x20
    public sealed struct Line2Ray2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point;  // 0x14
        public System.Single Parameter;  // 0x1c

    }

    // TypeToken: 0x200001B  // size: 0x20
    public sealed struct Line2Segment2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point;  // 0x14
        public System.Single Parameter;  // 0x1c

    }

    // TypeToken: 0x200001C  // size: 0x28
    public sealed struct Line2Triangle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x200001D  // size: 0x28
    public sealed struct Ray2AAB2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x200001E  // size: 0x28
    public sealed struct Ray2Box2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x200001F  // size: 0x24
    public sealed struct Ray2Circle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c

    }

    // TypeToken: 0x2000020  // size: 0x30
    public sealed struct Ray2ConvexPolygon2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20
        public System.Single Parameter0;  // 0x28
        public System.Single Parameter1;  // 0x2c

    }

    // TypeToken: 0x2000021  // size: 0x2C
    public sealed struct Ray2Polygon2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c
        public System.Single Parameter0;  // 0x24
        public System.Single Parameter1;  // 0x28

    }

    // TypeToken: 0x2000022  // size: 0x20
    public sealed struct Ray2Ray2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point;  // 0x14
        public System.Single Parameter;  // 0x1c

    }

    // TypeToken: 0x2000023  // size: 0x2C
    public sealed struct Ray2Segment2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c
        public System.Single Parameter0;  // 0x24
        public System.Single Parameter1;  // 0x28

    }

    // TypeToken: 0x2000024  // size: 0x28
    public sealed struct Ray2Triangle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000025  // size: 0x28
    public sealed struct Segment2AAB2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000026  // size: 0x28
    public sealed struct Segment2Box2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x2000027  // size: 0x24
    public sealed struct Segment2Circle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c

    }

    // TypeToken: 0x2000028  // size: 0x30
    public sealed struct Segment2ConvexPolygon2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20
        public System.Single Parameter0;  // 0x28
        public System.Single Parameter1;  // 0x2c

    }

    // TypeToken: 0x2000029  // size: 0x2C
    public sealed struct Segment2Segment2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector2 Point0;  // 0x14
        public UnityEngine.Vector2 Point1;  // 0x1c
        public System.Single Parameter0;  // 0x24
        public System.Single Parameter1;  // 0x28

    }

    // TypeToken: 0x200002A  // size: 0x28
    public sealed struct Segment2Triangle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20

    }

    // TypeToken: 0x200002B  // size: 0x48
    public sealed struct Triangle2Triangle2Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector2 Point0;  // 0x18
        public UnityEngine.Vector2 Point1;  // 0x20
        public UnityEngine.Vector2 Point2;  // 0x28
        public UnityEngine.Vector2 Point3;  // 0x30
        public UnityEngine.Vector2 Point4;  // 0x38
        public UnityEngine.Vector2 Point5;  // 0x40

        // Properties
        UnityEngine.Vector2 Item { get; /* RVA: 0x0693A094 */ set; /* RVA: 0x0693A110 */ }

    }

    // TypeToken: 0x200002C  // size: 0x28
    public sealed struct Float6
    {
        // Fields
        private System.Single _0;  // 0x10
        private System.Single _1;  // 0x14
        private System.Single _2;  // 0x18
        private System.Single _3;  // 0x1c
        private System.Single _4;  // 0x20
        private System.Single _5;  // 0x24

        // Properties
        System.Single Item { get; /* RVA: 0x069347A8 */ set; /* RVA: 0x069347EC */ }

    }

    // TypeToken: 0x200002D  // size: 0x30
    public sealed struct Line3AAB3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x200002E  // size: 0x30
    public sealed struct Line3Box3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x200002F  // size: 0x20
    public sealed struct Line3Circle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000030  // size: 0x24
    public sealed struct Line3Plane3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single LineParameter;  // 0x20

    }

    // TypeToken: 0x2000031  // size: 0x20
    public sealed struct Line3Polygon3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000032  // size: 0x20
    public sealed struct Line3Rectangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000033  // size: 0x38
    public sealed struct Line3Sphere3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24
        public System.Single LineParameter0;  // 0x30
        public System.Single LineParameter1;  // 0x34

    }

    // TypeToken: 0x2000034  // size: 0x30
    public sealed struct Line3Triangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single LineParameter;  // 0x20
        public System.Single TriBary0;  // 0x24
        public System.Single TriBary1;  // 0x28
        public System.Single TriBary2;  // 0x2c

    }

    // TypeToken: 0x2000035  // size: 0x2C
    public sealed struct Plane3Plane3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public Dest.Math.Line3 Line;  // 0x14

    }

    // TypeToken: 0x2000036  // size: 0x48
    public sealed struct Plane3Sphere3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public Dest.Math.Circle3 Circle;  // 0x14

    }

    // TypeToken: 0x2000037  // size: 0x3C
    public sealed struct Plane3Triangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24
        public UnityEngine.Vector3 Point2;  // 0x30

    }

    // TypeToken: 0x2000038  // size: 0x30
    public sealed struct Ray3AAB3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x2000039  // size: 0x30
    public sealed struct Ray3Box3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x200003A  // size: 0x20
    public sealed struct Ray3Circle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x200003B  // size: 0x24
    public sealed struct Ray3Plane3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single RayParameter;  // 0x20

    }

    // TypeToken: 0x200003C  // size: 0x20
    public sealed struct Ray3Polygon3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x200003D  // size: 0x20
    public sealed struct Ray3Rectangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x200003E  // size: 0x38
    public sealed struct Ray3Sphere3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24
        public System.Single RayParameter0;  // 0x30
        public System.Single RayParameter1;  // 0x34

    }

    // TypeToken: 0x200003F  // size: 0x30
    public sealed struct Ray3Triangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single RayParameter;  // 0x20
        public System.Single TriBary0;  // 0x24
        public System.Single TriBary1;  // 0x28
        public System.Single TriBary2;  // 0x2c

    }

    // TypeToken: 0x2000040  // size: 0x30
    public sealed struct Segment3AAB3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x2000041  // size: 0x30
    public sealed struct Segment3Box3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24

    }

    // TypeToken: 0x2000042  // size: 0x20
    public sealed struct Segment3Circle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000043  // size: 0x24
    public sealed struct Segment3Plane3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single SegmentParameter;  // 0x20

    }

    // TypeToken: 0x2000044  // size: 0x20
    public sealed struct Segment3Polygon3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000045  // size: 0x20
    public sealed struct Segment3Rectangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14

    }

    // TypeToken: 0x2000046  // size: 0x38
    public sealed struct Segment3Sphere3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public System.Int32 Quantity;  // 0x14
        public UnityEngine.Vector3 Point0;  // 0x18
        public UnityEngine.Vector3 Point1;  // 0x24
        public System.Single SegmentParameter0;  // 0x30
        public System.Single SegmentParameter1;  // 0x34

    }

    // TypeToken: 0x2000047  // size: 0x30
    public sealed struct Segment3Triangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public UnityEngine.Vector3 Point;  // 0x14
        public System.Single SegmentParameter;  // 0x20
        public System.Single TriBary0;  // 0x24
        public System.Single TriBary1;  // 0x28
        public System.Single TriBary2;  // 0x2c

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct Sphere3Sphere3IntrTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.Sphere3Sphere3IntrTypes Empty;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Point;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Circle;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Sphere0;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Sphere0Point;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Sphere1;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Sphere1Point;  // const
        public static Dest.Math.Sphere3Sphere3IntrTypes Same;  // const

    }

    // TypeToken: 0x2000049  // size: 0x54
    public sealed struct Sphere3Sphere3Intr
    {
        // Fields
        public Dest.Math.Sphere3Sphere3IntrTypes IntersectionType;  // 0x10
        public Dest.Math.Circle3 Circle;  // 0x14
        public UnityEngine.Vector3 ContactPoint;  // 0x48

    }

    // TypeToken: 0x200004A  // size: 0x68
    public sealed struct Triangle3Triangle3Intr
    {
        // Fields
        public Dest.Math.IntersectionTypes IntersectionType;  // 0x10
        public Dest.Math.IntersectionTypes CoplanarIntersectionType;  // 0x14
        public System.Boolean Touching;  // 0x18
        public System.Int32 Quantity;  // 0x1c
        public UnityEngine.Vector3 Point0;  // 0x20
        public UnityEngine.Vector3 Point1;  // 0x2c
        public UnityEngine.Vector3 Point2;  // 0x38
        public UnityEngine.Vector3 Point3;  // 0x44
        public UnityEngine.Vector3 Point4;  // 0x50
        public UnityEngine.Vector3 Point5;  // 0x5c

        // Properties
        UnityEngine.Vector3 Item { get; /* RVA: 0x0693A14C */ set; /* RVA: 0x0693A1DC */ }

    }

    // TypeToken: 0x200004B  // size: 0x14
    public sealed struct IntersectionTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.IntersectionTypes Empty;  // const
        public static Dest.Math.IntersectionTypes Point;  // const
        public static Dest.Math.IntersectionTypes Segment;  // const
        public static Dest.Math.IntersectionTypes Ray;  // const
        public static Dest.Math.IntersectionTypes Line;  // const
        public static Dest.Math.IntersectionTypes Polygon;  // const
        public static Dest.Math.IntersectionTypes Plane;  // const
        public static Dest.Math.IntersectionTypes Polyhedron;  // const
        public static Dest.Math.IntersectionTypes Other;  // const

    }

    // TypeToken: 0x200004C
    public interface ILogger
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600017C
        public virtual System.Void LogInfo(System.Object value) { }
        // RVA: -1  // abstract  token: 0x600017D
        public virtual System.Void LogWarning(System.Object value) { }
        // RVA: -1  // abstract  token: 0x600017E
        public virtual System.Void LogError(System.Object value) { }

    }

    // TypeToken: 0x200004D  // size: 0x10
    public class Logger
    {
        // Fields
        private static Dest.Math.ILogger _instance;  // static @ 0x0

        // Methods
        // RVA: 0x06934B70  token: 0x600017F
        private static System.Void .cctor() { }
        // RVA: 0x069349C0  token: 0x6000180
        public static System.Void LogInfo(System.Object value) { }
        // RVA: 0x06934A50  token: 0x6000181
        public static System.Void LogWarning(System.Object value) { }
        // RVA: 0x0693492C  token: 0x6000182
        public static System.Void LogError(System.Object value) { }
        // RVA: 0x06934AE8  token: 0x6000183
        public static System.Void SetLogger(Dest.Math.ILogger logger) { }
        // RVA: 0x041E1670  token: 0x6000184
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x10
    public class DefaultLogger : Dest.Math.ILogger
    {
        // Methods
        // RVA: 0x06934710  token: 0x6000185
        public virtual System.Void LogInfo(System.Object value) { }
        // RVA: 0x0693475C  token: 0x6000186
        public virtual System.Void LogWarning(System.Object value) { }
        // RVA: 0x069346C4  token: 0x6000187
        public virtual System.Void LogError(System.Object value) { }
        // RVA: 0x041E1670  token: 0x6000188
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public class EmptyLogger : Dest.Math.ILogger
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000189
        public virtual System.Void LogInfo(System.Object value) { }
        // RVA: 0x041E1670  token: 0x600018A
        public virtual System.Void LogWarning(System.Object value) { }
        // RVA: 0x041E1670  token: 0x600018B
        public virtual System.Void LogError(System.Object value) { }
        // RVA: 0x041E1670  token: 0x600018C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000050  // size: 0x10
    public static class Mathfex
    {
        // Fields
        public static System.Single ZeroTolerance;  // const
        public static System.Single NegativeZeroTolerance;  // const
        public static System.Single ZeroToleranceSqr;  // const
        public static System.Single Pi;  // const
        public static System.Single HalfPi;  // const
        public static System.Single TwoPi;  // const

        // Methods
        // RVA: 0x04D886E0  token: 0x600018D
        public static System.Single EvalSquared(System.Single x) { }
        // RVA: 0x04D88670  token: 0x600018E
        public static System.Single EvalInvSquared(System.Single x) { }
        // RVA: 0x04D89930  token: 0x600018F
        public static System.Single EvalCubic(System.Single x) { }
        // RVA: 0x04D899A0  token: 0x6000190
        public static System.Single EvalInvCubic(System.Single x) { }
        // RVA: 0x04D899B0  token: 0x6000191
        public static System.Single EvalQuadratic(System.Single x, System.Single a, System.Single b, System.Single c) { }
        // RVA: 0x04D899D0  token: 0x6000192
        public static System.Single EvalSigmoid(System.Single x) { }
        // RVA: 0x06934FE0  token: 0x6000193
        public static System.Single EvalOverlappedStep(System.Single x, System.Single overlap, System.Int32 objectIndex, System.Int32 objectCount) { }
        // RVA: 0x06935030  token: 0x6000194
        public static System.Single EvalSmoothOverlappedStep(System.Single x, System.Single overlap, System.Int32 objectIndex, System.Int32 objectCount) { }
        // RVA: 0x06934FA4  token: 0x6000195
        public static System.Single EvalGaussian(System.Single x, System.Single a, System.Single b, System.Single c) { }
        // RVA: 0x04D89940  token: 0x6000196
        public static System.Single EvalGaussian2D(System.Single x, System.Single y, System.Single x0, System.Single y0, System.Single A, System.Single a, System.Single b, System.Single c) { }
        // RVA: 0x06935148  token: 0x6000197
        public static System.Single Lerp(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x04D899F0  token: 0x6000198
        public static System.Single LerpUnclamped(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x0693519C  token: 0x6000199
        public static System.Single SigmoidInterp(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x069351E8  token: 0x600019A
        public static System.Single SinInterp(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x06934EC8  token: 0x600019B
        public static System.Single CosInterp(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x06935244  token: 0x600019C
        public static System.Single WobbleInterp(System.Single value0, System.Single value1, System.Single factor) { }
        // RVA: 0x06934F3C  token: 0x600019D
        public static System.Single CurveInterp(System.Single value0, System.Single value1, System.Single factor, UnityEngine.AnimationCurve curve) { }
        // RVA: 0x06935098  token: 0x600019E
        public static System.Single FuncInterp(System.Single value0, System.Single value1, System.Single factor, System.Func<System.Single,System.Single> func) { }
        // RVA: 0x06935118  token: 0x600019F
        public static System.Single InvSqrt(System.Single value) { }
        // RVA: 0x06935188  token: 0x60001A0
        public static System.Boolean Near(System.Single value0, System.Single value1, System.Single epsilon) { }
        // RVA: 0x06935178  token: 0x60001A1
        public static System.Boolean NearZero(System.Single value, System.Single epsilon) { }
        // RVA: 0x06934CC8  token: 0x60001A2
        public static UnityEngine.Vector2 CartesianToPolar(UnityEngine.Vector2 cartesianCoordinates) { }
        // RVA: 0x04D89A00  token: 0x60001A3
        public static UnityEngine.Vector2 PolarToCartesian(UnityEngine.Vector2 polarCoordinates) { }
        // RVA: 0x06934DAC  token: 0x60001A4
        public static UnityEngine.Vector3 CartesianToSpherical(UnityEngine.Vector3 cartesianCoordinates) { }
        // RVA: 0x04D89A50  token: 0x60001A5
        public static UnityEngine.Vector3 SphericalToCartesian(UnityEngine.Vector3 sphericalCoordinates) { }
        // RVA: 0x06934BE0  token: 0x60001A6
        public static UnityEngine.Vector3 CartesianToCylindrical(UnityEngine.Vector3 cartesianCoordinates) { }
        // RVA: 0x04D898E0  token: 0x60001A7
        public static UnityEngine.Vector3 CylindricalToCartesian(UnityEngine.Vector3 cylindricalCoordinates) { }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public static class Matrix4x4ex
    {
        // Fields
        public static readonly UnityEngine.Matrix4x4 Identity;  // static @ 0x0

        // Methods
        // RVA: 0x06937B64  token: 0x60001A8
        public static System.Void RotationMatrixToQuaternion(UnityEngine.Matrix4x4& matrix, UnityEngine.Quaternion& quaternion) { }
        // RVA: 0x04D8B050  token: 0x60001A9
        public static System.Void QuaternionToRotationMatrix(UnityEngine.Quaternion quaternion, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8B1D0  token: 0x60001AA
        public static System.Void QuaternionToRotationMatrix(UnityEngine.Quaternion& quaternion, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A9C0  token: 0x60001AB
        public static System.Void CreateTranslation(UnityEngine.Vector3 position, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A980  token: 0x60001AC
        public static System.Void CreateTranslation(UnityEngine.Vector3& position, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A000  token: 0x60001AD
        public static System.Void CreateScale(UnityEngine.Vector3 scale, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A040  token: 0x60001AE
        public static System.Void CreateScale(UnityEngine.Vector3& scale, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A080  token: 0x60001AF
        public static System.Void CreateScale(System.Single scale, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x069357BC  token: 0x60001B0
        public static System.Void CreateRotationEuler(System.Single eulerX, System.Single eulerY, System.Single eulerZ, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x069358C0  token: 0x60001B1
        public static System.Void CreateRotationEuler(UnityEngine.Vector3 eulerAngles, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x0693584C  token: 0x60001B2
        public static System.Void CreateRotationEuler(UnityEngine.Vector3& eulerAngles, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D89E00  token: 0x60001B3
        public static System.Void CreateRotationX(System.Single angleInDegrees, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D89E90  token: 0x60001B4
        public static System.Void CreateRotationY(System.Single angleInDegrees, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D89F20  token: 0x60001B5
        public static System.Void CreateRotationZ(System.Single angleInDegrees, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x06935638  token: 0x60001B6
        public static System.Void CreateRotationAngleAxis(System.Single angleInDegrees, UnityEngine.Vector3 rotationAxis, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D89CA0  token: 0x60001B7
        public static System.Void CreateRotationAngleUnitAxis(System.Single angleInDegrees, UnityEngine.Vector3 normalizedAxis, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x06935C50  token: 0x60001B8
        public static System.Void CreateRotation(UnityEngine.Vector3 rotationOrigin, UnityEngine.Quaternion rotation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935B54  token: 0x60001B9
        public static System.Void CreateRotation(UnityEngine.Vector3& rotationOrigin, UnityEngine.Quaternion& rotation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8B350  token: 0x60001BA
        public static System.Void Transpose(UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8B3C0  token: 0x60001BB
        public static System.Void Transpose(UnityEngine.Matrix4x4& matrix, UnityEngine.Matrix4x4& transpose) { }
        // RVA: 0x04D89AF0  token: 0x60001BC
        public static System.Single CalcDeterminant(UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x06936D68  token: 0x60001BD
        public static System.Void Inverse(UnityEngine.Matrix4x4& matrix, System.Single epsilon) { }
        // RVA: 0x0693679C  token: 0x60001BE
        public static System.Void Inverse(UnityEngine.Matrix4x4& matrix, UnityEngine.Matrix4x4& inverse, System.Single epsilon) { }
        // RVA: 0x04D89C40  token: 0x60001BF
        public static System.Void CopyMatrix(UnityEngine.Matrix4x4& source, UnityEngine.Matrix4x4& destination) { }
        // RVA: 0x04D8ABD0  token: 0x60001C0
        public static System.Void Multiply(UnityEngine.Matrix4x4& matrix0, UnityEngine.Matrix4x4& matrix1, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x0693777C  token: 0x60001C1
        public static System.Void MultiplyRight(UnityEngine.Matrix4x4& matrix0, UnityEngine.Matrix4x4& matrix1) { }
        // RVA: 0x06937394  token: 0x60001C2
        public static System.Void MultiplyLeft(UnityEngine.Matrix4x4& matrix1, UnityEngine.Matrix4x4& matrix0) { }
        // RVA: 0x04D8AA00  token: 0x60001C3
        public static System.Void Multiply(UnityEngine.Matrix4x4& matrix, System.Single scalar) { }
        // RVA: 0x04D8AF70  token: 0x60001C4
        public static System.Void Multiply(UnityEngine.Matrix4x4& matrix, System.Single scalar, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8AAD0  token: 0x60001C5
        public static UnityEngine.Vector4 Multiply(UnityEngine.Matrix4x4& matrix, UnityEngine.Vector4 vector) { }
        // RVA: 0x04D8AAD0  token: 0x60001C6
        public static UnityEngine.Vector4 Multiply(UnityEngine.Matrix4x4& matrix, UnityEngine.Vector4& vector) { }
        // RVA: 0x06936188  token: 0x60001C7
        public static System.Void CreateSRT(UnityEngine.Vector3 scaling, UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06936088  token: 0x60001C8
        public static System.Void CreateSRT(UnityEngine.Vector3& scaling, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x069365C4  token: 0x60001C9
        public static System.Void CreateSRT(System.Single scaling, UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x069366B0  token: 0x60001CA
        public static System.Void CreateSRT(System.Single scaling, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x0693641C  token: 0x60001CB
        public static System.Void CreateSRT(UnityEngine.Vector3 scaling, UnityEngine.Vector3 rotationOrigin, UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935EE0  token: 0x60001CC
        public static System.Void CreateSRT(UnityEngine.Vector3& scaling, UnityEngine.Vector3& rotationOrigin, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935D4C  token: 0x60001CD
        public static System.Void CreateSRT(System.Single scaling, UnityEngine.Vector3 rotationOrigin, UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06936288  token: 0x60001CE
        public static System.Void CreateSRT(System.Single scaling, UnityEngine.Vector3& rotationOrigin, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935440  token: 0x60001CF
        public static System.Void CreateRT(UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x069354B0  token: 0x60001D0
        public static System.Void CreateRT(UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935328  token: 0x60001D1
        public static System.Void CreateRT(UnityEngine.Vector3 rotationOrigin, UnityEngine.Quaternion rotation, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935520  token: 0x60001D2
        public static System.Void CreateRT(UnityEngine.Vector3& rotationOrigin, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D89FB0  token: 0x60001D3
        public static System.Void CreateST(UnityEngine.Vector3 scaling, UnityEngine.Vector3 translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D89FB0  token: 0x60001D4
        public static System.Void CreateST(UnityEngine.Vector3& scaling, UnityEngine.Vector3& translation, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06935A44  token: 0x60001D5
        public static System.Void CreateRotationFromColumns(UnityEngine.Vector3 column0, UnityEngine.Vector3 column1, UnityEngine.Vector3 column2, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x06935934  token: 0x60001D6
        public static System.Void CreateRotationFromColumns(UnityEngine.Vector3& column0, UnityEngine.Vector3& column1, UnityEngine.Vector3& column2, UnityEngine.Matrix4x4& matrix) { }
        // RVA: 0x04D8A0C0  token: 0x60001D7
        public static System.Void CreateShadowDirectional(Dest.Math.Plane3 shadowPlane, UnityEngine.Vector3 dirLightOppositeDirection, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8A220  token: 0x60001D8
        public static System.Void CreateShadowDirectional(Dest.Math.Plane3& shadowPlane, UnityEngine.Vector3& dirLightOppositeDirection, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8A380  token: 0x60001D9
        public static System.Void CreateShadowPoint(Dest.Math.Plane3 shadowPlane, UnityEngine.Vector3 pointLightPosition, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8A4E0  token: 0x60001DA
        public static System.Void CreateShadowPoint(Dest.Math.Plane3& shadowPlane, UnityEngine.Vector3& pointLightPosition, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8A7E0  token: 0x60001DB
        public static System.Void CreateShadow(Dest.Math.Plane3 shadowPlane, UnityEngine.Vector4 lightData, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x04D8A640  token: 0x60001DC
        public static System.Void CreateShadow(Dest.Math.Plane3& shadowPlane, UnityEngine.Vector4& lightData, UnityEngine.Matrix4x4& result) { }
        // RVA: 0x06937C2C  token: 0x60001DD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000052  // size: 0x10
    public static class Quaternionex
    {
        // Methods
        // RVA: 0x06937CB4  token: 0x60001DE
        public static UnityEngine.Quaternion DeltaTo(UnityEngine.Quaternion quat, UnityEngine.Quaternion target) { }
        // RVA: 0x06937D1C  token: 0x60001DF
        public static System.String ToStringEx(UnityEngine.Quaternion quat) { }

    }

    // TypeToken: 0x2000053  // size: 0x20
    public class Rand
    {
        // Fields
        private static System.Int32 a;  // const
        private static System.Int32 b;  // const
        private static System.Int32 c;  // const
        private static System.UInt32 DefaultY;  // const
        private static System.UInt32 DefaultZ;  // const
        private static System.UInt32 DefaultW;  // const
        private static System.UInt32 PositiveMask;  // const
        private static System.UInt32 BoolModuloMask;  // const
        private static System.UInt32 ByteModuloMask;  // const
        private static System.Double One_div_uintMaxValuePlusOne;  // const
        private static System.Double TwoPi;  // const
        private static Dest.Math.Rand _seedGenerator;  // static @ 0x0
        private System.UInt32 _x;  // 0x10
        private System.UInt32 _y;  // 0x14
        private System.UInt32 _z;  // 0x18
        private System.UInt32 _w;  // 0x1c
        public static Dest.Math.Rand Instance;  // static @ 0x8

        // Methods
        // RVA: 0x06938E74  token: 0x60001E0
        private static System.Void .cctor() { }
        // RVA: 0x06938F44  token: 0x60001E1
        public System.Void .ctor() { }
        // RVA: 0x04D8B5A0  token: 0x60001E2
        public System.Void .ctor(System.Int32 seed) { }
        // RVA: 0x04D8B5A0  token: 0x60001E3
        public System.Void ResetSeed(System.Int32 seed) { }
        // RVA: 0x04D8B420  token: 0x60001E4
        public System.Void GetState(System.UInt32& x, System.UInt32& y, System.UInt32& z, System.UInt32& w) { }
        // RVA: 0x04D8B5C0  token: 0x60001E5
        public System.Void SetState(System.UInt32 x, System.UInt32 y, System.UInt32 z, System.UInt32 w) { }
        // RVA: 0x04D8B440  token: 0x60001E6
        public System.Int32 NextInt() { }
        // RVA: 0x04D8B500  token: 0x60001E7
        public System.Int32 NextInt(System.Int32 max) { }
        // RVA: 0x06938610  token: 0x60001E8
        public System.Int32 NextInt(System.Int32 min, System.Int32 max) { }
        // RVA: 0x06938604  token: 0x60001E9
        public System.Int32 NextIntInclusive(System.Int32 min, System.Int32 max) { }
        // RVA: 0x04D8B560  token: 0x60001EA
        public System.Int32 NextPositiveInt() { }
        // RVA: 0x04D8B440  token: 0x60001EB
        public System.UInt32 NextUInt() { }
        // RVA: 0x04D8B470  token: 0x60001EC
        public System.Double NextDouble() { }
        // RVA: 0x06938480  token: 0x60001ED
        public System.Double NextDouble(System.Double min, System.Double max) { }
        // RVA: 0x04D8B4B0  token: 0x60001EE
        public System.Single NextFloat() { }
        // RVA: 0x06938540  token: 0x60001EF
        public System.Single NextFloat(System.Single min, System.Single max) { }
        // RVA: 0x0693844C  token: 0x60001F0
        public System.Boolean NextBool() { }
        // RVA: 0x04D8B440  token: 0x60001F1
        public System.Byte NextByte() { }
        // RVA: 0x06938C58  token: 0x60001F2
        public UnityEngine.Color RandomColorOpaque() { }
        // RVA: 0x06938CC4  token: 0x60001F3
        public UnityEngine.Color RandomColorTransparent() { }
        // RVA: 0x06938BC4  token: 0x60001F4
        public UnityEngine.Color32 RandomColor32Opaque() { }
        // RVA: 0x06938C04  token: 0x60001F5
        public UnityEngine.Color32 RandomColor32Transparent() { }
        // RVA: 0x06938BAC  token: 0x60001F6
        public System.Single RandomAngleRadians() { }
        // RVA: 0x06938B94  token: 0x60001F7
        public System.Single RandomAngleDegrees() { }
        // RVA: 0x069380E0  token: 0x60001F8
        public UnityEngine.Vector2 InSquare(System.Single side) { }
        // RVA: 0x06938A5C  token: 0x60001F9
        public UnityEngine.Vector2 OnSquare(System.Single side) { }
        // RVA: 0x06937F50  token: 0x60001FA
        public UnityEngine.Vector3 InCube(System.Single side) { }
        // RVA: 0x0693874C  token: 0x60001FB
        public UnityEngine.Vector3 OnCube(System.Single side) { }
        // RVA: 0x06937ED8  token: 0x60001FC
        public UnityEngine.Vector2 InCircle(System.Single radius) { }
        // RVA: 0x06937E38  token: 0x60001FD
        public UnityEngine.Vector2 InCircle(System.Single radiusMin, System.Single radiusMax) { }
        // RVA: 0x069386F0  token: 0x60001FE
        public UnityEngine.Vector2 OnCircle(System.Single radius) { }
        // RVA: 0x06938000  token: 0x60001FF
        public UnityEngine.Vector3 InSphere(System.Single radius) { }
        // RVA: 0x06938990  token: 0x6000200
        public UnityEngine.Vector3 OnSphere(System.Single radius) { }
        // RVA: 0x069382D0  token: 0x6000201
        public UnityEngine.Vector3 InTriangle(UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x06938154  token: 0x6000202
        public UnityEngine.Vector3 InTriangle(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06938D48  token: 0x6000203
        public UnityEngine.Quaternion RandomRotation() { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public static class Vector2ex
    {
        // Fields
        public static readonly UnityEngine.Vector2 Zero;  // static @ 0x0
        public static readonly UnityEngine.Vector2 One;  // static @ 0x8
        public static readonly UnityEngine.Vector2 UnitX;  // static @ 0x10
        public static readonly UnityEngine.Vector2 UnitY;  // static @ 0x18
        public static readonly UnityEngine.Vector2 PositiveInfinity;  // static @ 0x20
        public static readonly UnityEngine.Vector2 NegativeInfinity;  // static @ 0x28

        // Methods
        // RVA: 0x0693A3C8  token: 0x6000204
        private static Dest.Math.Vector2ex.Information GetInformation(System.Collections.Generic.IList<UnityEngine.Vector2> points, System.Single epsilon) { }
        // RVA: 0x04D8B6B0  token: 0x6000205
        public static System.Single Length(UnityEngine.Vector2 vector) { }
        // RVA: 0x04D8B690  token: 0x6000206
        public static System.Single LengthSqr(UnityEngine.Vector2 vector) { }
        // RVA: 0x04D85F20  token: 0x6000207
        public static System.Single DotPerp(UnityEngine.Vector2 vector, UnityEngine.Vector2 value) { }
        // RVA: 0x04D8B5E0  token: 0x6000208
        public static System.Single DotPerp(UnityEngine.Vector2 vector, UnityEngine.Vector2& value) { }
        // RVA: 0x04D8B600  token: 0x6000209
        public static System.Single DotPerp(UnityEngine.Vector2& vector, UnityEngine.Vector2& value) { }
        // RVA: 0x04D8B640  token: 0x600020A
        public static System.Single Dot(UnityEngine.Vector2 vector, UnityEngine.Vector2 value) { }
        // RVA: 0x04D8B620  token: 0x600020B
        public static System.Single Dot(UnityEngine.Vector2 vector, UnityEngine.Vector2& value) { }
        // RVA: 0x04D8B670  token: 0x600020C
        public static System.Single Dot(UnityEngine.Vector2& vector, UnityEngine.Vector2& value) { }
        // RVA: 0x04D8B6F0  token: 0x600020D
        public static UnityEngine.Vector2 Perp(UnityEngine.Vector2 vector) { }
        // RVA: 0x0693A268  token: 0x600020E
        public static System.Single AngleDeg(UnityEngine.Vector2 vector, UnityEngine.Vector2 target) { }
        // RVA: 0x0693A31C  token: 0x600020F
        public static System.Single AngleRad(UnityEngine.Vector2 vector, UnityEngine.Vector2 target) { }
        // RVA: 0x0693AA7C  token: 0x6000210
        public static System.Single Normalize(UnityEngine.Vector2& vector, System.Single epsilon) { }
        // RVA: 0x0693AAEC  token: 0x6000211
        public static System.Single SetLength(UnityEngine.Vector2& vector, System.Single lengthValue, System.Single epsilon) { }
        // RVA: 0x0693AA04  token: 0x6000212
        public static System.Single GrowLength(UnityEngine.Vector2& vector, System.Single lengthDelta, System.Single epsilon) { }
        // RVA: 0x04D8B720  token: 0x6000213
        public static UnityEngine.Vector2 Replicate(System.Single value) { }
        // RVA: 0x04D88A10  token: 0x6000214
        public static UnityEngine.Vector3 ToVector3XY(UnityEngine.Vector2 vector) { }
        // RVA: 0x04D88A60  token: 0x6000215
        public static UnityEngine.Vector3 ToVector3XZ(UnityEngine.Vector2 vector) { }
        // RVA: 0x04D8B730  token: 0x6000216
        public static UnityEngine.Vector3 ToVector3YZ(UnityEngine.Vector2 vector) { }
        // RVA: 0x0693AB60  token: 0x6000217
        public static System.String ToStringEx(UnityEngine.Vector2 vector) { }
        // RVA: 0x0693ABBC  token: 0x6000218
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct ProjectionPlanes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.ProjectionPlanes XY;  // const
        public static Dest.Math.ProjectionPlanes XZ;  // const
        public static Dest.Math.ProjectionPlanes YZ;  // const

    }

    // TypeToken: 0x2000057  // size: 0x10
    public static class Vector3ex
    {
        // Fields
        public static readonly UnityEngine.Vector3 Zero;  // static @ 0x0
        public static readonly UnityEngine.Vector3 One;  // static @ 0xc
        public static readonly UnityEngine.Vector3 UnitX;  // static @ 0x18
        public static readonly UnityEngine.Vector3 UnitY;  // static @ 0x24
        public static readonly UnityEngine.Vector3 UnitZ;  // static @ 0x30
        public static readonly UnityEngine.Vector3 PositiveInfinity;  // static @ 0x3c
        public static readonly UnityEngine.Vector3 NegativeInfinity;  // static @ 0x48

        // Methods
        // RVA: 0x0693AF04  token: 0x600021A
        private static Dest.Math.Vector3ex.Information GetInformation(System.Collections.Generic.IList<UnityEngine.Vector3> points, System.Single epsilon) { }
        // RVA: 0x04D8B820  token: 0x600021B
        public static System.Single Length(UnityEngine.Vector3 vector) { }
        // RVA: 0x04D8B7F0  token: 0x600021C
        public static System.Single LengthSqr(UnityEngine.Vector3 vector) { }
        // RVA: 0x04D8B7C0  token: 0x600021D
        public static System.Single Dot(UnityEngine.Vector3 vector, UnityEngine.Vector3 value) { }
        // RVA: 0x04D8B7C0  token: 0x600021E
        public static System.Single Dot(UnityEngine.Vector3 vector, UnityEngine.Vector3& value) { }
        // RVA: 0x04D8B7C0  token: 0x600021F
        public static System.Single Dot(UnityEngine.Vector3& vector, UnityEngine.Vector3& value) { }
        // RVA: 0x0693AC6C  token: 0x6000220
        public static System.Single AngleDeg(UnityEngine.Vector3 vector, UnityEngine.Vector3 target) { }
        // RVA: 0x0693AD28  token: 0x6000221
        public static System.Single AngleRad(UnityEngine.Vector3 vector, UnityEngine.Vector3 target) { }
        // RVA: 0x0693BD34  token: 0x6000222
        public static System.Single SignedAngleDeg(UnityEngine.Vector3 vector, UnityEngine.Vector3 target, UnityEngine.Vector3 normal) { }
        // RVA: 0x0693BE94  token: 0x6000223
        public static System.Single SignedAngleRad(UnityEngine.Vector3 vector, UnityEngine.Vector3 target, UnityEngine.Vector3 normal) { }
        // RVA: 0x04D8B760  token: 0x6000224
        public static UnityEngine.Vector3 Cross(UnityEngine.Vector3 vector, UnityEngine.Vector3 value) { }
        // RVA: 0x04D8B760  token: 0x6000225
        public static UnityEngine.Vector3 Cross(UnityEngine.Vector3 vector, UnityEngine.Vector3& value) { }
        // RVA: 0x04D8B760  token: 0x6000226
        public static UnityEngine.Vector3 Cross(UnityEngine.Vector3& vector, UnityEngine.Vector3& value) { }
        // RVA: 0x0693C154  token: 0x6000227
        public static UnityEngine.Vector3 UnitCross(UnityEngine.Vector3 vector, UnityEngine.Vector3 value) { }
        // RVA: 0x0693C094  token: 0x6000228
        public static UnityEngine.Vector3 UnitCross(UnityEngine.Vector3 vector, UnityEngine.Vector3& value) { }
        // RVA: 0x0693C20C  token: 0x6000229
        public static UnityEngine.Vector3 UnitCross(UnityEngine.Vector3& vector, UnityEngine.Vector3& value) { }
        // RVA: 0x0693BB94  token: 0x600022A
        public static System.Single Normalize(UnityEngine.Vector3& vector, System.Single epsilon) { }
        // RVA: 0x0693BCA4  token: 0x600022B
        public static System.Single SetLength(UnityEngine.Vector3& vector, System.Single lengthValue, System.Single epsilon) { }
        // RVA: 0x0693BB00  token: 0x600022C
        public static System.Single GrowLength(UnityEngine.Vector3& vector, System.Single lengthDelta, System.Single epsilon) { }
        // RVA: 0x04D8B860  token: 0x600022D
        public static UnityEngine.Vector3 Replicate(System.Single value) { }
        // RVA: 0x0693ADDC  token: 0x600022E
        public static System.Void CreateOrthonormalBasis(UnityEngine.Vector3& u, UnityEngine.Vector3& v, UnityEngine.Vector3& w) { }
        // RVA: 0x0693BC20  token: 0x600022F
        public static System.Boolean SameDirection(UnityEngine.Vector3 value0, UnityEngine.Vector3 value1) { }
        // RVA: 0x04D889F0  token: 0x6000230
        public static UnityEngine.Vector2 ToVector2XY(UnityEngine.Vector3 vector) { }
        // RVA: 0x04D88A40  token: 0x6000231
        public static UnityEngine.Vector2 ToVector2XZ(UnityEngine.Vector3 vector) { }
        // RVA: 0x04D8B880  token: 0x6000232
        public static UnityEngine.Vector2 ToVector2YZ(UnityEngine.Vector3 vector) { }
        // RVA: 0x0693C06C  token: 0x6000233
        public static UnityEngine.Vector2 ToVector2(UnityEngine.Vector3 vector, Dest.Math.ProjectionPlanes projectionPlane) { }
        // RVA: 0x0693BAC0  token: 0x6000234
        public static Dest.Math.ProjectionPlanes GetProjectionPlane(UnityEngine.Vector3 vector) { }
        // RVA: 0x0693BFEC  token: 0x6000235
        public static System.String ToStringEx(UnityEngine.Vector3 vector) { }
        // RVA: 0x0693C2CC  token: 0x6000236
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x1C
    public sealed struct BrentsRoot
    {
        // Fields
        public System.Single X;  // 0x10
        public System.Int32 Iterations;  // 0x14
        public System.Boolean ExceededMaxIterations;  // 0x18

    }

    // TypeToken: 0x200005A  // size: 0x10
    public static class RootFinder
    {
        // Fields
        private static System.Single third;  // const
        private static System.Single twentySeventh;  // const
        private static System.Single sqrt3;  // static @ 0x0

        // Methods
        // RVA: 0x06938FBC  token: 0x6000238
        public static System.Boolean BrentsMethod(System.Func<System.Single,System.Single> function, System.Single x0, System.Single x1, Dest.Math.BrentsRoot& root, System.Int32 maxIterations, System.Single negativeTolerance, System.Single positiveTolerance, System.Single stepTolerance, System.Single segmentTolerance) { }
        // RVA: 0x06939750  token: 0x6000239
        public static System.Boolean Linear(System.Single c0, System.Single c1, System.Single& root, System.Single epsilon) { }
        // RVA: 0x06939A14  token: 0x600023A
        public static System.Boolean Quadratic(System.Single c0, System.Single c1, System.Single c2, Dest.Math.QuadraticRoots& roots, System.Single epsilon) { }
        // RVA: 0x06939394  token: 0x600023B
        public static System.Boolean Cubic(System.Single c0, System.Single c1, System.Single c2, System.Single c3, Dest.Math.CubicRoots& roots, System.Single epsilon) { }
        // RVA: 0x06939B9C  token: 0x600023C
        public static System.Boolean Quartic(System.Single c0, System.Single c1, System.Single c2, System.Single c3, System.Single c4, Dest.Math.QuarticRoots& roots, System.Single epsilon) { }
        // RVA: 0x0693977C  token: 0x600023D
        public static System.Single PolynomialBound(Dest.Math.Polynomial poly, System.Single epsilon) { }
        // RVA: 0x0693992C  token: 0x600023E
        public static System.Boolean Polynomial(Dest.Math.Polynomial poly, System.Single xMin, System.Single xMax, System.Single[]& roots, System.Int32 digits, System.Single epsilon) { }
        // RVA: 0x06939848  token: 0x600023F
        public static System.Boolean Polynomial(Dest.Math.Polynomial poly, System.Single[]& roots, System.Int32 digits, System.Single epsilon) { }
        // RVA: 0x0693A050  token: 0x6000240
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x28
    public class EigenData
    {
        // Fields
        private System.Int32 _size;  // 0x10
        private System.Single[] _diagonal;  // 0x18
        private System.Single[,] _matrix;  // 0x20

        // Properties
        System.Int32 Size { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x06941A3C  token: 0x6000246
        private System.Void .ctor(System.Single[] diagonal, System.Single[,] matrix) { }
        // RVA: 0x06920148  token: 0x6000247
        public System.Single GetEigenvalue(System.Int32 index) { }
        // RVA: 0x0694179C  token: 0x6000248
        public UnityEngine.Vector2 GetEigenvector2(System.Int32 index) { }
        // RVA: 0x06941854  token: 0x6000249
        public UnityEngine.Vector3 GetEigenvector3(System.Int32 index) { }
        // RVA: 0x06941924  token: 0x600024A
        public System.Single[] GetEigenvector(System.Int32 index) { }
        // RVA: 0x069419C8  token: 0x600024B
        public System.Void GetEigenvector(System.Int32 index, System.Single[] out_eigenvector) { }

    }

    // TypeToken: 0x200005D  // size: 0x10
    public static class EigenDecomposition
    {
        // Methods
        // RVA: 0x069423D4  token: 0x600024C
        private static System.Void Tridiagonal2(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix, System.Boolean& isRotation) { }
        // RVA: 0x069424E8  token: 0x600024D
        private static System.Void Tridiagonal3(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix, System.Boolean& isRotation) { }
        // RVA: 0x0694285C  token: 0x600024E
        private static System.Void TridiagonalN(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix, System.Boolean& isRotation) { }
        // RVA: 0x06941DB4  token: 0x600024F
        private static System.Boolean QLAlgorithm(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix) { }
        // RVA: 0x06941C50  token: 0x6000250
        private static System.Void IncreasingSort(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix, System.Boolean& isRotation) { }
        // RVA: 0x06941A78  token: 0x6000251
        private static System.Void DecreasingSort(System.Single[] diagonal, System.Single[] subdiagonal, System.Single[,] matrix, System.Boolean& isRotation) { }
        // RVA: 0x06941BE0  token: 0x6000252
        private static System.Void GuaranteeRotation(System.Single[,] matrix, System.Boolean isRotation) { }
        // RVA: 0x06942224  token: 0x6000253
        public static Dest.Math.EigenData Solve(System.Single[,] symmetricSquareMatrix, System.Boolean increasingSort) { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public static class Integrator
    {
        // Fields
        private static System.Int32 _degree;  // const
        private static System.Single[] root;  // static @ 0x0
        private static System.Single[] coeff;  // static @ 0x8

        // Methods
        // RVA: 0x06943270  token: 0x6000254
        public static System.Single TrapezoidRule(System.Func<System.Single,System.Single> function, System.Single a, System.Single b, System.Int32 sampleCount) { }
        // RVA: 0x06943004  token: 0x6000255
        public static System.Single RombergIntegral(System.Func<System.Single,System.Single> function, System.Single a, System.Single b, System.Int32 order) { }
        // RVA: 0x06942EE8  token: 0x6000256
        public static System.Single GaussianQuadrature(System.Func<System.Single,System.Single> function, System.Single a, System.Single b) { }
        // RVA: 0x06943374  token: 0x6000257
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005F  // size: 0x10
    public static class LinearSystem
    {
        // Methods
        // RVA: 0x069447D0  token: 0x6000258
        public static System.Boolean Solve2(System.Single[,] A, System.Single[] B, System.Single[]& X, System.Single zeroTolerance) { }
        // RVA: 0x069449D8  token: 0x6000259
        public static System.Boolean Solve2(System.Single[,] A, System.Single[] B, UnityEngine.Vector2& X, System.Single zeroTolerance) { }
        // RVA: 0x06944B8C  token: 0x600025A
        public static System.Boolean Solve3(System.Single[,] A, System.Single[] B, System.Single[]& X, System.Single zeroTolerance) { }
        // RVA: 0x06944AAC  token: 0x600025B
        public static System.Boolean Solve3(System.Single[,] A, System.Single[] B, UnityEngine.Vector3& X, System.Single zeroTolerance) { }
        // RVA: 0x06945A34  token: 0x600025C
        private static System.Void SwapRows(System.Single[,] matrix, System.Int32 row0, System.Int32 row1, System.Int32 columnCount) { }
        // RVA: 0x06945548  token: 0x600025D
        public static System.Boolean Solve(System.Single[,] A, System.Single[] B, System.Single[]& X) { }
        // RVA: 0x069452A0  token: 0x600025E
        public static System.Boolean SolveTridiagonal(System.Single[] A, System.Single[] B, System.Single[] C, System.Single[] R, System.Single[]& U) { }
        // RVA: 0x06944308  token: 0x600025F
        public static System.Boolean Inverse(System.Single[,] A, System.Single[,]& invA) { }

    }

    // TypeToken: 0x2000060  // size: 0x80
    public sealed class OdeFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x06945C4C  token: 0x6000260
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x06945C38  token: 0x6000261
        public virtual System.Void Invoke(System.Single t, System.Single[] y, System.Single[] F) { }
        // RVA: 0x06945BB0  token: 0x6000262
        public virtual System.IAsyncResult BeginInvoke(System.Single t, System.Single[] y, System.Single[] F, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000263
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000061  // size: 0x28
    public abstract class OdeSolver
    {
        // Fields
        protected System.Int32 _dim;  // 0x10
        protected System.Single _step;  // 0x14
        protected Dest.Math.OdeFunction _function;  // 0x18
        protected System.Single[] _FValue;  // 0x20

        // Properties
        System.Single Step { get; /* RVA: 0x04D88D40 */ set; /* RVA: 0x04D88D50 */ }

        // Methods
        // RVA: 0x06946350  token: 0x6000266
        public System.Void .ctor(System.Int32 dim, System.Single step, Dest.Math.OdeFunction function) { }
        // RVA: -1  // abstract  token: 0x6000267
        public virtual System.Void Update(System.Single tIn, System.Single[] yIn, System.Single& tOut, System.Single[] yOut) { }

    }

    // TypeToken: 0x2000062  // size: 0x28
    public class OdeEuler : Dest.Math.OdeSolver
    {
        // Methods
        // RVA: 0x06945BA0  token: 0x6000268
        public System.Void .ctor(System.Int32 dim, System.Single step, Dest.Math.OdeFunction function) { }
        // RVA: 0x06945ADC  token: 0x6000269
        public virtual System.Void Update(System.Single tIn, System.Single[] yIn, System.Single& tOut, System.Single[] yOut) { }

    }

    // TypeToken: 0x2000063  // size: 0x38
    public class OdeMidpoint : Dest.Math.OdeSolver
    {
        // Fields
        private System.Single _halfStep;  // 0x28
        private System.Single[] _yTemp;  // 0x30

        // Properties
        System.Single Step { get; /* RVA: 0x04D88D40 */ set; /* RVA: 0x04D8BF40 */ }

        // Methods
        // RVA: 0x06945E74  token: 0x600026C
        public System.Void .ctor(System.Int32 dim, System.Single step, Dest.Math.OdeFunction function) { }
        // RVA: 0x06945D14  token: 0x600026D
        public virtual System.Void Update(System.Single tIn, System.Single[] yIn, System.Single& tOut, System.Single[] yOut) { }

    }

    // TypeToken: 0x2000064  // size: 0x58
    public class OdeRungeKutta4 : Dest.Math.OdeSolver
    {
        // Fields
        private System.Single _halfStep;  // 0x28
        private System.Single _sixthStep;  // 0x2c
        private System.Single[] _temp1;  // 0x30
        private System.Single[] _temp2;  // 0x38
        private System.Single[] _temp3;  // 0x40
        private System.Single[] _temp4;  // 0x48
        private System.Single[] _yTemp;  // 0x50

        // Properties
        System.Single Step { get; /* RVA: 0x04D88D40 */ set; /* RVA: 0x0694632C */ }

        // Methods
        // RVA: 0x0694621C  token: 0x6000270
        public System.Void .ctor(System.Int32 dim, System.Single step, Dest.Math.OdeFunction function) { }
        // RVA: 0x06945F08  token: 0x6000271
        public virtual System.Void Update(System.Single tIn, System.Single[] yIn, System.Single& tOut, System.Single[] yOut) { }

    }

    // TypeToken: 0x2000065  // size: 0x1C
    public sealed struct QuadraticRoots
    {
        // Fields
        public System.Single X0;  // 0x10
        public System.Single X1;  // 0x14
        public System.Int32 RootCount;  // 0x18

        // Properties
        System.Single Item { get; /* RVA: 0x069488E0 */ }

    }

    // TypeToken: 0x2000066  // size: 0x20
    public sealed struct CubicRoots
    {
        // Fields
        public System.Single X0;  // 0x10
        public System.Single X1;  // 0x14
        public System.Single X2;  // 0x18
        public System.Int32 RootCount;  // 0x1c

        // Properties
        System.Single Item { get; /* RVA: 0x06941774 */ }

    }

    // TypeToken: 0x2000067  // size: 0x24
    public sealed struct QuarticRoots
    {
        // Fields
        public System.Single X0;  // 0x10
        public System.Single X1;  // 0x14
        public System.Single X2;  // 0x18
        public System.Single X3;  // 0x1c
        public System.Int32 RootCount;  // 0x20

        // Properties
        System.Single Item { get; /* RVA: 0x06948900 */ }

    }

    // TypeToken: 0x2000068  // size: 0x20
    public sealed struct AAB2
    {
        // Fields
        public UnityEngine.Vector2 Min;  // 0x10
        public UnityEngine.Vector2 Max;  // 0x18

        // Methods
        // RVA: 0x04D8B9B0  token: 0x6000275
        public System.Void .ctor(UnityEngine.Vector2& min, UnityEngine.Vector2& max) { }
        // RVA: 0x04D861B0  token: 0x6000276
        public System.Void .ctor(UnityEngine.Vector2 min, UnityEngine.Vector2 max) { }
        // RVA: 0x04D8B9D0  token: 0x6000277
        public System.Void .ctor(System.Single xMin, System.Single xMax, System.Single yMin, System.Single yMax) { }
        // RVA: 0x0693CAC8  token: 0x6000278
        public static UnityEngine.Rect op_Implicit(Dest.Math.AAB2 value) { }
        // RVA: 0x04D8B9F0  token: 0x6000279
        public static Dest.Math.AAB2 op_Implicit(UnityEngine.Rect value) { }
        // RVA: 0x04D8B980  token: 0x600027A
        public static Dest.Math.AAB2 CreateFromPoint(UnityEngine.Vector2& point) { }
        // RVA: 0x04D8B970  token: 0x600027B
        public static Dest.Math.AAB2 CreateFromPoint(UnityEngine.Vector2 point) { }
        // RVA: 0x0693C8A4  token: 0x600027C
        public static Dest.Math.AAB2 CreateFromTwoPoints(UnityEngine.Vector2& point0, UnityEngine.Vector2& point1) { }
        // RVA: 0x0693C868  token: 0x600027D
        public static Dest.Math.AAB2 CreateFromTwoPoints(UnityEngine.Vector2 point0, UnityEngine.Vector2 point1) { }
        // RVA: 0x0693C624  token: 0x600027E
        public static Dest.Math.AAB2 CreateFromPoints(System.Collections.Generic.IEnumerable<UnityEngine.Vector2> points) { }
        // RVA: 0x0693C75C  token: 0x600027F
        public static Dest.Math.AAB2 CreateFromPoints(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x0693C568  token: 0x6000280
        public static Dest.Math.AAB2 CreateFromPoints(UnityEngine.Vector2[] points) { }
        // RVA: 0x04D8B8C0  token: 0x6000281
        public System.Void CalcCenterExtents(UnityEngine.Vector2& center, UnityEngine.Vector2& extents) { }
        // RVA: 0x04D8B920  token: 0x6000282
        public System.Void CalcVertices(UnityEngine.Vector2& vertex0, UnityEngine.Vector2& vertex1, UnityEngine.Vector2& vertex2, UnityEngine.Vector2& vertex3) { }
        // RVA: 0x0693C3E0  token: 0x6000283
        public UnityEngine.Vector2[] CalcVertices() { }
        // RVA: 0x0693C4A0  token: 0x6000284
        public System.Void CalcVertices(UnityEngine.Vector2[] array) { }
        // RVA: 0x04D8B8A0  token: 0x6000285
        public System.Single CalcArea() { }
        // RVA: 0x0693C8F0  token: 0x6000286
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x0693CA00  token: 0x6000287
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x0355A9C0  token: 0x6000288
        public System.Boolean Contains(UnityEngine.Vector2& point) { }
        // RVA: 0x0693C530  token: 0x6000289
        public System.Boolean Contains(UnityEngine.Vector2 point) { }
        // RVA: 0x0693C9C0  token: 0x600028A
        public System.Void Include(UnityEngine.Vector2& point) { }
        // RVA: 0x0693C910  token: 0x600028B
        public System.Void Include(UnityEngine.Vector2 point) { }
        // RVA: 0x0693C98C  token: 0x600028C
        public System.Void Include(Dest.Math.AAB2& box) { }
        // RVA: 0x0693C958  token: 0x600028D
        public System.Void Include(Dest.Math.AAB2 box) { }
        // RVA: 0x0693CA30  token: 0x600028E
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000069  // size: 0x30
    public sealed struct Box2
    {
        // Fields
        public UnityEngine.Vector2 Center;  // 0x10
        public UnityEngine.Vector2 Axis0;  // 0x18
        public UnityEngine.Vector2 Axis1;  // 0x20
        public UnityEngine.Vector2 Extents;  // 0x28

        // Methods
        // RVA: 0x04D8BC60  token: 0x600028F
        public System.Void .ctor(UnityEngine.Vector2& center, UnityEngine.Vector2& axis0, UnityEngine.Vector2& axis1, UnityEngine.Vector2& extents) { }
        // RVA: 0x04D8BCB0  token: 0x6000290
        public System.Void .ctor(UnityEngine.Vector2 center, UnityEngine.Vector2 axis0, UnityEngine.Vector2 axis1, UnityEngine.Vector2 extents) { }
        // RVA: 0x0693E578  token: 0x6000291
        public System.Void .ctor(Dest.Math.AAB2& box) { }
        // RVA: 0x0693E614  token: 0x6000292
        public System.Void .ctor(Dest.Math.AAB2 box) { }
        // RVA: 0x0693DB34  token: 0x6000293
        public static Dest.Math.Box2 CreateFromPoints(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x0693DED0  token: 0x6000294
        public UnityEngine.Vector2 GetAxis(System.Int32 index) { }
        // RVA: 0x0693D568  token: 0x6000295
        public System.Void CalcVertices(UnityEngine.Vector2& vertex0, UnityEngine.Vector2& vertex1, UnityEngine.Vector2& vertex2, UnityEngine.Vector2& vertex3) { }
        // RVA: 0x0693D63C  token: 0x6000296
        public UnityEngine.Vector2[] CalcVertices() { }
        // RVA: 0x0693D7E8  token: 0x6000297
        public System.Void CalcVertices(UnityEngine.Vector2[] array) { }
        // RVA: 0x04D8BC40  token: 0x6000298
        public System.Single CalcArea() { }
        // RVA: 0x0693DEB0  token: 0x6000299
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x0693E3DC  token: 0x600029A
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x0693D950  token: 0x600029B
        public System.Boolean Contains(UnityEngine.Vector2& point) { }
        // RVA: 0x0693DA44  token: 0x600029C
        public System.Boolean Contains(UnityEngine.Vector2 point) { }
        // RVA: 0x0693DF54  token: 0x600029D
        public System.Void Include(Dest.Math.Box2& box) { }
        // RVA: 0x0693E3C8  token: 0x600029E
        public System.Void Include(Dest.Math.Box2 box) { }
        // RVA: 0x0693E40C  token: 0x600029F
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006A  // size: 0x1C
    public sealed struct Circle2
    {
        // Fields
        public UnityEngine.Vector2 Center;  // 0x10
        public System.Single Radius;  // 0x18

        // Methods
        // RVA: 0x04D8BEB0  token: 0x60002A0
        public System.Void .ctor(UnityEngine.Vector2& center, System.Single radius) { }
        // RVA: 0x04D8BEC0  token: 0x60002A1
        public System.Void .ctor(UnityEngine.Vector2 center, System.Single radius) { }
        // RVA: 0x06940D78  token: 0x60002A2
        public static Dest.Math.Circle2 CreateFromPointsAAB(System.Collections.Generic.IEnumerable<UnityEngine.Vector2> points) { }
        // RVA: 0x06940CB0  token: 0x60002A3
        public static Dest.Math.Circle2 CreateFromPointsAAB(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x0694105C  token: 0x60002A4
        public static Dest.Math.Circle2 CreateFromPointsAverage(System.Collections.Generic.IEnumerable<UnityEngine.Vector2> points) { }
        // RVA: 0x06940E88  token: 0x60002A5
        public static Dest.Math.Circle2 CreateFromPointsAverage(System.Collections.Generic.IList<UnityEngine.Vector2> points) { }
        // RVA: 0x06940A94  token: 0x60002A6
        public static System.Boolean CreateCircumscribed(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2, Dest.Math.Circle2& circle) { }
        // RVA: 0x06941390  token: 0x60002A7
        public static System.Boolean CreateInscribed(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2, Dest.Math.Circle2& circle) { }
        // RVA: 0x04D8BDD0  token: 0x60002A8
        public System.Single CalcPerimeter() { }
        // RVA: 0x04D8BDB0  token: 0x60002A9
        public System.Single CalcArea() { }
        // RVA: 0x04D8BDE0  token: 0x60002AA
        public UnityEngine.Vector2 Eval(System.Single t) { }
        // RVA: 0x04D8BE40  token: 0x60002AB
        public UnityEngine.Vector2 Eval(System.Single t, System.Single radius) { }
        // RVA: 0x069415A0  token: 0x60002AC
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x069416BC  token: 0x60002AD
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x06940A4C  token: 0x60002AE
        public System.Boolean Contains(UnityEngine.Vector2& point) { }
        // RVA: 0x06940A00  token: 0x60002AF
        public System.Boolean Contains(UnityEngine.Vector2 point) { }
        // RVA: 0x069415D4  token: 0x60002B0
        public System.Void Include(Dest.Math.Circle2& circle) { }
        // RVA: 0x069415C0  token: 0x60002B1
        public System.Void Include(Dest.Math.Circle2 circle) { }
        // RVA: 0x069416EC  token: 0x60002B2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006B  // size: 0x20
    public sealed struct Line2
    {
        // Fields
        public UnityEngine.Vector2 Center;  // 0x10
        public UnityEngine.Vector2 Direction;  // 0x18

        // Methods
        // RVA: 0x04D8B9B0  token: 0x60002B3
        public System.Void .ctor(UnityEngine.Vector2& center, UnityEngine.Vector2& direction) { }
        // RVA: 0x04D861B0  token: 0x60002B4
        public System.Void .ctor(UnityEngine.Vector2 center, UnityEngine.Vector2 direction) { }
        // RVA: 0x06943568  token: 0x60002B5
        public static Dest.Math.Line2 CreateFromTwoPoints(UnityEngine.Vector2& p0, UnityEngine.Vector2& p1) { }
        // RVA: 0x0694351C  token: 0x60002B6
        public static Dest.Math.Line2 CreateFromTwoPoints(UnityEngine.Vector2 p0, UnityEngine.Vector2 p1) { }
        // RVA: 0x06943638  token: 0x60002B7
        public static Dest.Math.Line2 CreatePerpToLineTrhoughPoint(Dest.Math.Line2 line, UnityEngine.Vector2 point) { }
        // RVA: 0x04D8BED0  token: 0x60002B8
        public static Dest.Math.Line2 CreateBetweenAndEquidistantToPoints(UnityEngine.Vector2 point0, UnityEngine.Vector2 point1) { }
        // RVA: 0x069435D4  token: 0x60002B9
        public static Dest.Math.Line2 CreateParallelToGivenLineAtGivenDistance(Dest.Math.Line2 line, System.Single distance) { }
        // RVA: 0x069436DC  token: 0x60002BA
        public UnityEngine.Vector2 Eval(System.Single t) { }
        // RVA: 0x069441EC  token: 0x60002BB
        public System.Single SignedDistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x069436BC  token: 0x60002BC
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x06943F0C  token: 0x60002BD
        public System.Int32 QuerySide(UnityEngine.Vector2 point, System.Single epsilon) { }
        // RVA: 0x0694373C  token: 0x60002BE
        public System.Boolean QuerySideNegative(UnityEngine.Vector2 point, System.Single epsilon) { }
        // RVA: 0x06943B10  token: 0x60002BF
        public System.Boolean QuerySidePositive(UnityEngine.Vector2 point, System.Single epsilon) { }
        // RVA: 0x0694409C  token: 0x60002C0
        public System.Int32 QuerySide(Dest.Math.Box2& box, System.Single epsilon) { }
        // RVA: 0x06943898  token: 0x60002C1
        public System.Boolean QuerySideNegative(Dest.Math.Box2& box, System.Single epsilon) { }
        // RVA: 0x06943BAC  token: 0x60002C2
        public System.Boolean QuerySidePositive(Dest.Math.Box2& box, System.Single epsilon) { }
        // RVA: 0x06943DA4  token: 0x60002C3
        public System.Int32 QuerySide(Dest.Math.AAB2& box, System.Single epsilon) { }
        // RVA: 0x069437D0  token: 0x60002C4
        public System.Boolean QuerySideNegative(Dest.Math.AAB2& box, System.Single epsilon) { }
        // RVA: 0x06943CD4  token: 0x60002C5
        public System.Boolean QuerySidePositive(Dest.Math.AAB2& box, System.Single epsilon) { }
        // RVA: 0x06943FC8  token: 0x60002C6
        public System.Int32 QuerySide(Dest.Math.Circle2& circle, System.Single epsilon) { }
        // RVA: 0x069439C8  token: 0x60002C7
        public System.Boolean QuerySideNegative(Dest.Math.Circle2& circle, System.Single epsilon) { }
        // RVA: 0x06943A70  token: 0x60002C8
        public System.Boolean QuerySidePositive(Dest.Math.Circle2& circle, System.Single epsilon) { }
        // RVA: 0x0694370C  token: 0x60002C9
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x06943458  token: 0x60002CA
        public System.Single AngleBetweenTwoLines(Dest.Math.Line2 anotherLine, System.Boolean acuteAngleDesired) { }
        // RVA: 0x06944270  token: 0x60002CB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006C  // size: 0x34
    public sealed struct Edge2
    {
        // Fields
        public UnityEngine.Vector2 Point0;  // 0x10
        public UnityEngine.Vector2 Point1;  // 0x18
        public UnityEngine.Vector2 Direction;  // 0x20
        public UnityEngine.Vector2 Normal;  // 0x28
        public System.Single Length;  // 0x30

    }

    // TypeToken: 0x200006D  // size: 0x20
    public class Polygon2
    {
        // Fields
        private UnityEngine.Vector2[] _vertices;  // 0x10
        private Dest.Math.Edge2[] _edges;  // 0x18

        // Properties
        UnityEngine.Vector2[] Vertices { get; /* RVA: 0x02B2ECC0 */ }
        Dest.Math.Edge2[] Edges { get; /* RVA: 0x0385B100 */ }
        System.Int32 VertexCount { get; /* RVA: 0x067B6CAC */ }
        UnityEngine.Vector2 Item { get; /* RVA: 0x0694886C */ set; /* RVA: 0x06948898 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60002D1
        private System.Void .ctor() { }
        // RVA: 0x0694874C  token: 0x60002D2
        public System.Void .ctor(UnityEngine.Vector2[] vertices) { }
        // RVA: 0x069487F0  token: 0x60002D3
        public System.Void .ctor(System.Int32 vertexCount) { }
        // RVA: 0x06947664  token: 0x60002D4
        public static Dest.Math.Polygon2 CreateProjected(Dest.Math.Polygon3 polygon, Dest.Math.ProjectionPlanes projectionPlane) { }
        // RVA: 0x06947878  token: 0x60002D5
        public Dest.Math.Edge2 GetEdge(System.Int32 edgeIndex) { }
        // RVA: 0x0694858C  token: 0x60002D6
        public System.Void UpdateEdges() { }
        // RVA: 0x069483EC  token: 0x60002D7
        public System.Void UpdateEdge(System.Int32 edgeIndex) { }
        // RVA: 0x06946B94  token: 0x60002D8
        public UnityEngine.Vector2 CalcCenter() { }
        // RVA: 0x06946C5C  token: 0x60002D9
        public System.Single CalcPerimeter() { }
        // RVA: 0x06946A04  token: 0x60002DA
        public System.Single CalcArea() { }
        // RVA: 0x069479B4  token: 0x60002DB
        public System.Boolean IsConvex(Dest.Math.Orientations& orientation, System.Single threshold) { }
        // RVA: 0x0694798C  token: 0x60002DC
        public System.Boolean IsConvex(System.Single threshold) { }
        // RVA: 0x069478C8  token: 0x60002DD
        public System.Boolean HasZeroCorners(System.Single threshold) { }
        // RVA: 0x06947B30  token: 0x60002DE
        public System.Void ReverseVertices() { }
        // RVA: 0x06946D58  token: 0x60002DF
        public System.Boolean ContainsConvexQuadCCW(UnityEngine.Vector2& point) { }
        // RVA: 0x06946D3C  token: 0x60002E0
        public System.Boolean ContainsConvexQuadCCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06947138  token: 0x60002E1
        public System.Boolean ContainsConvexQuadCW(UnityEngine.Vector2& point) { }
        // RVA: 0x06947518  token: 0x60002E2
        public System.Boolean ContainsConvexQuadCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06946CC4  token: 0x60002E3
        public System.Boolean ContainsConvexCCW(UnityEngine.Vector2& point) { }
        // RVA: 0x06946CE4  token: 0x60002E4
        public System.Boolean ContainsConvexCCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06947C24  token: 0x60002E5
        private System.Boolean SubContainsPointCCW(UnityEngine.Vector2& p, System.Int32 i0, System.Int32 i1) { }
        // RVA: 0x06946D00  token: 0x60002E6
        public System.Boolean ContainsConvexCW(UnityEngine.Vector2& point) { }
        // RVA: 0x06946D20  token: 0x60002E7
        public System.Boolean ContainsConvexCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06947EA4  token: 0x60002E8
        private System.Boolean SubContainsPointCW(UnityEngine.Vector2& p, System.Int32 i0, System.Int32 i1) { }
        // RVA: 0x06947550  token: 0x60002E9
        public System.Boolean ContainsSimple(UnityEngine.Vector2& point) { }
        // RVA: 0x06947534  token: 0x60002EA
        public System.Boolean ContainsSimple(UnityEngine.Vector2 point) { }
        // RVA: 0x06948124  token: 0x60002EB
        public Dest.Math.Segment2[] ToSegmentArray() { }
        // RVA: 0x06948268  token: 0x60002EC
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006E  // size: 0x20
    public sealed struct Ray2
    {
        // Fields
        public UnityEngine.Vector2 Center;  // 0x10
        public UnityEngine.Vector2 Direction;  // 0x18

        // Methods
        // RVA: 0x04D8B9B0  token: 0x60002ED
        public System.Void .ctor(UnityEngine.Vector2& center, UnityEngine.Vector2& direction) { }
        // RVA: 0x04D861B0  token: 0x60002EE
        public System.Void .ctor(UnityEngine.Vector2 center, UnityEngine.Vector2 direction) { }
        // RVA: 0x069436DC  token: 0x60002EF
        public UnityEngine.Vector2 Eval(System.Single t) { }
        // RVA: 0x06948934  token: 0x60002F0
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x06948954  token: 0x60002F1
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x06948984  token: 0x60002F2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006F  // size: 0x34
    public sealed struct Segment2
    {
        // Fields
        public UnityEngine.Vector2 P0;  // 0x10
        public UnityEngine.Vector2 P1;  // 0x18
        public UnityEngine.Vector2 Center;  // 0x20
        public UnityEngine.Vector2 Direction;  // 0x28
        public System.Single Extent;  // 0x30

        // Methods
        // RVA: 0x06948E30  token: 0x60002F3
        public System.Void .ctor(UnityEngine.Vector2& p0, UnityEngine.Vector2& p1) { }
        // RVA: 0x06948DEC  token: 0x60002F4
        public System.Void .ctor(UnityEngine.Vector2 p0, UnityEngine.Vector2 p1) { }
        // RVA: 0x06948E70  token: 0x60002F5
        public System.Void .ctor(UnityEngine.Vector2& center, UnityEngine.Vector2& direction, System.Single extent) { }
        // RVA: 0x06948E08  token: 0x60002F6
        public System.Void .ctor(UnityEngine.Vector2 center, UnityEngine.Vector2 direction, System.Single extent) { }
        // RVA: 0x06948C44  token: 0x60002F7
        public System.Void SetEndpoints(UnityEngine.Vector2 p0, UnityEngine.Vector2 p1) { }
        // RVA: 0x06948C30  token: 0x60002F8
        public System.Void SetCenterDirectionExtent(UnityEngine.Vector2 center, UnityEngine.Vector2 direction, System.Single extent) { }
        // RVA: 0x06948A1C  token: 0x60002F9
        public System.Void CalcCenterDirectionExtent() { }
        // RVA: 0x06948AFC  token: 0x60002FA
        public System.Void CalcEndPoints() { }
        // RVA: 0x06948BB4  token: 0x60002FB
        public UnityEngine.Vector2 Eval(System.Single s) { }
        // RVA: 0x06948B94  token: 0x60002FC
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x06948C00  token: 0x60002FD
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x06948C54  token: 0x60002FE
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000070  // size: 0x28
    public sealed struct Triangle2
    {
        // Fields
        public UnityEngine.Vector2 V0;  // 0x10
        public UnityEngine.Vector2 V1;  // 0x18
        public UnityEngine.Vector2 V2;  // 0x20

        // Properties
        UnityEngine.Vector2 Item { get; /* RVA: 0x06949DEC */ set; /* RVA: 0x06949E2C */ }

        // Methods
        // RVA: 0x04D8C0E0  token: 0x6000301
        public System.Void .ctor(UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2) { }
        // RVA: 0x04D8C0D0  token: 0x6000302
        public System.Void .ctor(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2) { }
        // RVA: 0x069496A8  token: 0x6000303
        public UnityEngine.Vector2 CalcEdge(System.Int32 edgeIndex) { }
        // RVA: 0x04D8C070  token: 0x6000304
        public System.Single CalcDeterminant() { }
        // RVA: 0x0694971C  token: 0x6000305
        public Dest.Math.Orientations CalcOrientation(System.Single threshold) { }
        // RVA: 0x0694946C  token: 0x6000306
        public System.Single CalcArea() { }
        // RVA: 0x04D8BF60  token: 0x6000307
        public static System.Single CalcArea(UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2) { }
        // RVA: 0x04D8BFD0  token: 0x6000308
        public static System.Single CalcArea(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2) { }
        // RVA: 0x06949028  token: 0x6000309
        public UnityEngine.Vector3 CalcAnglesDeg() { }
        // RVA: 0x06948ED4  token: 0x600030A
        public static UnityEngine.Vector3 CalcAnglesDeg(UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2) { }
        // RVA: 0x0694917C  token: 0x600030B
        public static UnityEngine.Vector3 CalcAnglesDeg(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2) { }
        // RVA: 0x0694933C  token: 0x600030C
        public UnityEngine.Vector3 CalcAnglesRad() { }
        // RVA: 0x0694920C  token: 0x600030D
        public static UnityEngine.Vector3 CalcAnglesRad(UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2) { }
        // RVA: 0x069491C4  token: 0x600030E
        public static UnityEngine.Vector3 CalcAnglesRad(UnityEngine.Vector2 v0, UnityEngine.Vector2 v1, UnityEngine.Vector2 v2) { }
        // RVA: 0x06949A10  token: 0x600030F
        public UnityEngine.Vector2 EvalBarycentric(System.Single c0, System.Single c1) { }
        // RVA: 0x06949AA8  token: 0x6000310
        public UnityEngine.Vector2 EvalBarycentric(UnityEngine.Vector3& baryCoords) { }
        // RVA: 0x06949AA8  token: 0x6000311
        public UnityEngine.Vector2 EvalBarycentric(UnityEngine.Vector3 baryCoords) { }
        // RVA: 0x069494C4  token: 0x6000312
        public static System.Void CalcBarycentricCoords(UnityEngine.Vector2& point, UnityEngine.Vector2& v0, UnityEngine.Vector2& v1, UnityEngine.Vector2& v2, UnityEngine.Vector3& baryCoords) { }
        // RVA: 0x0694948C  token: 0x6000313
        public UnityEngine.Vector3 CalcBarycentricCoords(UnityEngine.Vector2& point) { }
        // RVA: 0x0694966C  token: 0x6000314
        public UnityEngine.Vector3 CalcBarycentricCoords(UnityEngine.Vector2 point) { }
        // RVA: 0x069499F0  token: 0x6000315
        public System.Single DistanceTo(UnityEngine.Vector2 point) { }
        // RVA: 0x06949B54  token: 0x6000316
        public System.Int32 QuerySideCCW(UnityEngine.Vector2 point, System.Single epsilon) { }
        // RVA: 0x06949C38  token: 0x6000317
        public System.Int32 QuerySideCW(UnityEngine.Vector2 point, System.Single epsilon) { }
        // RVA: 0x06949B24  token: 0x6000318
        public UnityEngine.Vector2 Project(UnityEngine.Vector2 point) { }
        // RVA: 0x069498F4  token: 0x6000319
        public System.Boolean Contains(UnityEngine.Vector2& point) { }
        // RVA: 0x069499D4  token: 0x600031A
        public System.Boolean Contains(UnityEngine.Vector2 point) { }
        // RVA: 0x06949778  token: 0x600031B
        public System.Boolean ContainsCCW(UnityEngine.Vector2& point) { }
        // RVA: 0x0694975C  token: 0x600031C
        public System.Boolean ContainsCCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06949844  token: 0x600031D
        public System.Boolean ContainsCW(UnityEngine.Vector2& point) { }
        // RVA: 0x06949828  token: 0x600031E
        public System.Boolean ContainsCW(UnityEngine.Vector2 point) { }
        // RVA: 0x06949D28  token: 0x600031F
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000071  // size: 0x28
    public sealed struct AAB3
    {
        // Fields
        public UnityEngine.Vector3 Min;  // 0x10
        public UnityEngine.Vector3 Max;  // 0x1c

        // Methods
        // RVA: 0x04D8BBF0  token: 0x6000320
        public System.Void .ctor(UnityEngine.Vector3& min, UnityEngine.Vector3& max) { }
        // RVA: 0x04D871E0  token: 0x6000321
        public System.Void .ctor(UnityEngine.Vector3 min, UnityEngine.Vector3 max) { }
        // RVA: 0x04D8BC10  token: 0x6000322
        public System.Void .ctor(System.Single xMin, System.Single xMax, System.Single yMin, System.Single yMax, System.Single zMin, System.Single zMax) { }
        // RVA: 0x0693D4FC  token: 0x6000323
        public static UnityEngine.Bounds op_Implicit(Dest.Math.AAB3 value) { }
        // RVA: 0x0693D498  token: 0x6000324
        public static Dest.Math.AAB3 op_Implicit(UnityEngine.Bounds value) { }
        // RVA: 0x04D8BBD0  token: 0x6000325
        public static Dest.Math.AAB3 CreateFromPoint(UnityEngine.Vector3& point) { }
        // RVA: 0x04D8BBD0  token: 0x6000326
        public static Dest.Math.AAB3 CreateFromPoint(UnityEngine.Vector3 point) { }
        // RVA: 0x0693D260  token: 0x6000327
        public static Dest.Math.AAB3 CreateFromTwoPoints(UnityEngine.Vector3& point0, UnityEngine.Vector3& point1) { }
        // RVA: 0x0693D230  token: 0x6000328
        public static Dest.Math.AAB3 CreateFromTwoPoints(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1) { }
        // RVA: 0x0693CE7C  token: 0x6000329
        public static Dest.Math.AAB3 CreateFromPoints(System.Collections.Generic.IEnumerable<UnityEngine.Vector3> points) { }
        // RVA: 0x0693CFF4  token: 0x600032A
        public static Dest.Math.AAB3 CreateFromPoints(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x0693D144  token: 0x600032B
        public static Dest.Math.AAB3 CreateFromPoints(UnityEngine.Vector3[] points) { }
        // RVA: 0x04D8BA30  token: 0x600032C
        public System.Void CalcCenterExtents(UnityEngine.Vector3& center, UnityEngine.Vector3& extents) { }
        // RVA: 0x04D8BAB0  token: 0x600032D
        public System.Void CalcVertices(UnityEngine.Vector3& vertex0, UnityEngine.Vector3& vertex1, UnityEngine.Vector3& vertex2, UnityEngine.Vector3& vertex3, UnityEngine.Vector3& vertex4, UnityEngine.Vector3& vertex5, UnityEngine.Vector3& vertex6, UnityEngine.Vector3& vertex7) { }
        // RVA: 0x0693CC88  token: 0x600032E
        public UnityEngine.Vector3[] CalcVertices() { }
        // RVA: 0x0693CB0C  token: 0x600032F
        public System.Void CalcVertices(UnityEngine.Vector3[] array) { }
        // RVA: 0x04D8BBA0  token: 0x6000330
        public System.Single CalcVolume() { }
        // RVA: 0x0693D2D8  token: 0x6000331
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x0693D3C0  token: 0x6000332
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x033A4CF0  token: 0x6000333
        public System.Boolean Contains(UnityEngine.Vector3& point) { }
        // RVA: 0x0693CE34  token: 0x6000334
        public System.Boolean Contains(UnityEngine.Vector3 point) { }
        // RVA: 0x0693D2F4  token: 0x6000335
        public System.Void Include(UnityEngine.Vector3& point) { }
        // RVA: 0x0693D2F4  token: 0x6000336
        public System.Void Include(UnityEngine.Vector3 point) { }
        // RVA: 0x0693D358  token: 0x6000337
        public System.Void Include(Dest.Math.AAB3& box) { }
        // RVA: 0x0693D38C  token: 0x6000338
        public System.Void Include(Dest.Math.AAB3 box) { }
        // RVA: 0x0693D3EC  token: 0x6000339
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000072  // size: 0x4C
    public sealed struct Box3
    {
        // Fields
        public UnityEngine.Vector3 Center;  // 0x10
        public UnityEngine.Vector3 Axis0;  // 0x1c
        public UnityEngine.Vector3 Axis1;  // 0x28
        public UnityEngine.Vector3 Axis2;  // 0x34
        public UnityEngine.Vector3 Extents;  // 0x40

        // Methods
        // RVA: 0x04D8BCF0  token: 0x600033A
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& axis0, UnityEngine.Vector3& axis1, UnityEngine.Vector3& axis2, UnityEngine.Vector3& extents) { }
        // RVA: 0x04D8BD50  token: 0x600033B
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 axis0, UnityEngine.Vector3 axis1, UnityEngine.Vector3 axis2, UnityEngine.Vector3 extents) { }
        // RVA: 0x069408C4  token: 0x600033C
        public System.Void .ctor(Dest.Math.AAB3& box) { }
        // RVA: 0x06940974  token: 0x600033D
        public System.Void .ctor(Dest.Math.AAB3 box) { }
        // RVA: 0x0693F94C  token: 0x600033E
        public static Dest.Math.Box3 CreateFromPoints(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x0693FEC8  token: 0x600033F
        public UnityEngine.Vector3 GetAxis(System.Int32 index) { }
        // RVA: 0x0693F258  token: 0x6000340
        public System.Void CalcVertices(UnityEngine.Vector3& vertex0, UnityEngine.Vector3& vertex1, UnityEngine.Vector3& vertex2, UnityEngine.Vector3& vertex3, UnityEngine.Vector3& vertex4, UnityEngine.Vector3& vertex5, UnityEngine.Vector3& vertex6, UnityEngine.Vector3& vertex7) { }
        // RVA: 0x0693E6B0  token: 0x6000341
        public UnityEngine.Vector3[] CalcVertices() { }
        // RVA: 0x0693EC9C  token: 0x6000342
        public System.Void CalcVertices(UnityEngine.Vector3[] array) { }
        // RVA: 0x04D8BCD0  token: 0x6000343
        public System.Single CalcVolume() { }
        // RVA: 0x0693FEAC  token: 0x6000344
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x069406D4  token: 0x6000345
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x0693F7A0  token: 0x6000346
        public System.Boolean Contains(UnityEngine.Vector3& point) { }
        // RVA: 0x0693F938  token: 0x6000347
        public System.Boolean Contains(UnityEngine.Vector3 point) { }
        // RVA: 0x0693FF64  token: 0x6000348
        public System.Void Include(Dest.Math.Box3& box) { }
        // RVA: 0x069406C0  token: 0x6000349
        public System.Void Include(Dest.Math.Box3 box) { }
        // RVA: 0x06940700  token: 0x600034A
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000073  // size: 0x48
    public sealed struct Capsule3
    {
        // Fields
        public Dest.Math.Segment3 Segment;  // 0x10
        public System.Single Radius;  // 0x44

        // Methods
        // RVA: 0x04D8C120  token: 0x600034B
        public System.Void .ctor(Dest.Math.Segment3& segment, System.Single radius) { }
        // RVA: 0x04D8C150  token: 0x600034C
        public System.Void .ctor(Dest.Math.Segment3 segment, System.Single radius) { }

    }

    // TypeToken: 0x2000074  // size: 0x44
    public sealed struct Circle3
    {
        // Fields
        public UnityEngine.Vector3 Center;  // 0x10
        public UnityEngine.Vector3 Axis0;  // 0x1c
        public UnityEngine.Vector3 Axis1;  // 0x28
        public UnityEngine.Vector3 Normal;  // 0x34
        public System.Single Radius;  // 0x40

        // Methods
        // RVA: 0x0694B958  token: 0x600034D
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& axis0, UnityEngine.Vector3& axis1, System.Single radius) { }
        // RVA: 0x0694BA4C  token: 0x600034E
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 axis0, UnityEngine.Vector3 axis1, System.Single radius) { }
        // RVA: 0x0694B8C4  token: 0x600034F
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& normal, System.Single radius) { }
        // RVA: 0x0694BB2C  token: 0x6000350
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 normal, System.Single radius) { }
        // RVA: 0x0694AC08  token: 0x6000351
        public static System.Boolean CreateCircumscribed(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.Circle3& circle) { }
        // RVA: 0x0694AFA8  token: 0x6000352
        public static System.Boolean CreateInscribed(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, Dest.Math.Circle3& circle) { }
        // RVA: 0x04D8C1A0  token: 0x6000353
        public System.Single CalcPerimeter() { }
        // RVA: 0x04D8C180  token: 0x6000354
        public System.Single CalcArea() { }
        // RVA: 0x0694B4A4  token: 0x6000355
        public UnityEngine.Vector3 Eval(System.Single t) { }
        // RVA: 0x0694B5BC  token: 0x6000356
        public UnityEngine.Vector3 Eval(System.Single t, System.Single radius) { }
        // RVA: 0x0694B488  token: 0x6000357
        public System.Single DistanceTo(UnityEngine.Vector3 point, System.Boolean solid) { }
        // RVA: 0x0694B6E0  token: 0x6000358
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point, System.Boolean solid) { }
        // RVA: 0x0694B710  token: 0x6000359
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000075  // size: 0x28
    public sealed struct Line3
    {
        // Fields
        public UnityEngine.Vector3 Center;  // 0x10
        public UnityEngine.Vector3 Direction;  // 0x1c

        // Methods
        // RVA: 0x04D8BBF0  token: 0x600035A
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& direction) { }
        // RVA: 0x04D871E0  token: 0x600035B
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 direction) { }
        // RVA: 0x0694F5BC  token: 0x600035C
        public static Dest.Math.Line3 CreateFromTwoPoints(UnityEngine.Vector3& p0, UnityEngine.Vector3& p1) { }
        // RVA: 0x0694F5BC  token: 0x600035D
        public static Dest.Math.Line3 CreateFromTwoPoints(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1) { }
        // RVA: 0x0694F664  token: 0x600035E
        public UnityEngine.Vector3 Eval(System.Single t) { }
        // RVA: 0x0694F648  token: 0x600035F
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x0694F6E0  token: 0x6000360
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x0694F4F4  token: 0x6000361
        public System.Single AngleBetweenTwoLines(Dest.Math.Line3 anotherLine, System.Boolean acuteAngleDesired) { }
        // RVA: 0x0694F70C  token: 0x6000362
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000076  // size: 0x20
    public sealed struct Plane3
    {
        // Fields
        public UnityEngine.Vector3 Normal;  // 0x10
        public System.Single Constant;  // 0x1c

        // Methods
        // RVA: 0x04D8C1B0  token: 0x6000363
        public System.Void .ctor(UnityEngine.Vector3& normal, System.Single constant) { }
        // RVA: 0x04D8C1B0  token: 0x6000364
        public System.Void .ctor(UnityEngine.Vector3 normal, System.Single constant) { }
        // RVA: 0x069514E8  token: 0x6000365
        public System.Void .ctor(UnityEngine.Vector3& normal, UnityEngine.Vector3& point) { }
        // RVA: 0x06951450  token: 0x6000366
        public System.Void .ctor(UnityEngine.Vector3 normal, UnityEngine.Vector3 point) { }
        // RVA: 0x069516DC  token: 0x6000367
        public System.Void .ctor(UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, UnityEngine.Vector3& p2) { }
        // RVA: 0x06951594  token: 0x6000368
        public System.Void .ctor(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2) { }
        // RVA: 0x06951824  token: 0x6000369
        public static UnityEngine.Plane op_Implicit(Dest.Math.Plane3 value) { }
        // RVA: 0x04D8C1D0  token: 0x600036A
        public static Dest.Math.Plane3 op_Implicit(UnityEngine.Plane value) { }
        // RVA: 0x069501F4  token: 0x600036B
        public UnityEngine.Vector3 CalcOrigin() { }
        // RVA: 0x06950238  token: 0x600036C
        public System.Void CreateOrthonormalBasis(UnityEngine.Vector3& u, UnityEngine.Vector3& v, UnityEngine.Vector3& n) { }
        // RVA: 0x0695131C  token: 0x600036D
        private System.Single SignedDistanceTo(UnityEngine.Vector3& point) { }
        // RVA: 0x0695128C  token: 0x600036E
        public System.Single SignedDistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x06950340  token: 0x600036F
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x069511C4  token: 0x6000370
        public System.Int32 QuerySide(UnityEngine.Vector3 point, System.Single epsilon) { }
        // RVA: 0x069507B8  token: 0x6000371
        public System.Boolean QuerySideNegative(UnityEngine.Vector3 point, System.Single epsilon) { }
        // RVA: 0x06950B00  token: 0x6000372
        public System.Boolean QuerySidePositive(UnityEngine.Vector3 point, System.Single epsilon) { }
        // RVA: 0x06950FFC  token: 0x6000373
        public System.Int32 QuerySide(Dest.Math.Box3& box, System.Single epsilon) { }
        // RVA: 0x06950504  token: 0x6000374
        public System.Boolean QuerySideNegative(Dest.Math.Box3& box, System.Single epsilon) { }
        // RVA: 0x06950BAC  token: 0x6000375
        public System.Boolean QuerySidePositive(Dest.Math.Box3& box, System.Single epsilon) { }
        // RVA: 0x06950E44  token: 0x6000376
        public System.Int32 QuerySide(Dest.Math.AAB3& box, System.Single epsilon) { }
        // RVA: 0x069506AC  token: 0x6000377
        public System.Boolean QuerySideNegative(Dest.Math.AAB3& box, System.Single epsilon) { }
        // RVA: 0x069509EC  token: 0x6000378
        public System.Boolean QuerySidePositive(Dest.Math.AAB3& box, System.Single epsilon) { }
        // RVA: 0x06950D4C  token: 0x6000379
        public System.Int32 QuerySide(Dest.Math.Sphere3& sphere, System.Single epsilon) { }
        // RVA: 0x0695085C  token: 0x600037A
        public System.Boolean QuerySideNegative(Dest.Math.Sphere3& sphere, System.Single epsilon) { }
        // RVA: 0x06950928  token: 0x600037B
        public System.Boolean QuerySidePositive(Dest.Math.Sphere3& sphere, System.Single epsilon) { }
        // RVA: 0x069504D8  token: 0x600037C
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x069503D8  token: 0x600037D
        public UnityEngine.Vector3 ProjectVector(UnityEngine.Vector3 vector) { }
        // RVA: 0x06950098  token: 0x600037E
        public System.Single AngleBetweenPlaneNormalAndLine(Dest.Math.Line3 line) { }
        // RVA: 0x0694FFEC  token: 0x600037F
        public System.Single AngleBetweenPlaneNormalAndLine(UnityEngine.Vector3 direction) { }
        // RVA: 0x0694FF24  token: 0x6000380
        public System.Single AngleBetweenPlaneAndLine(Dest.Math.Line3 line) { }
        // RVA: 0x0694FE68  token: 0x6000381
        public System.Single AngleBetweenPlaneAndLine(UnityEngine.Vector3 direction) { }
        // RVA: 0x06950150  token: 0x6000382
        public System.Single AngleBetweenTwoPlanes(Dest.Math.Plane3 anotherPlane) { }
        // RVA: 0x069513B8  token: 0x6000383
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000077  // size: 0x44
    public sealed struct Edge3
    {
        // Fields
        public UnityEngine.Vector3 Point0;  // 0x10
        public UnityEngine.Vector3 Point1;  // 0x1c
        public UnityEngine.Vector3 Direction;  // 0x28
        public UnityEngine.Vector3 Normal;  // 0x34
        public System.Single Length;  // 0x40

    }

    // TypeToken: 0x2000078  // size: 0x30
    public class Polygon3
    {
        // Fields
        private UnityEngine.Vector3[] _vertices;  // 0x10
        private Dest.Math.Edge3[] _edges;  // 0x18
        private Dest.Math.Plane3 _plane;  // 0x20

        // Properties
        UnityEngine.Vector3[] Vertices { get; /* RVA: 0x02B2ECC0 */ }
        Dest.Math.Edge3[] Edges { get; /* RVA: 0x0385B100 */ }
        System.Int32 VertexCount { get; /* RVA: 0x067B6CAC */ }
        UnityEngine.Vector3 Item { get; /* RVA: 0x06953194 */ set; /* RVA: 0x069531C0 */ }
        Dest.Math.Plane3 Plane { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x04D8C210 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600038B
        private System.Void .ctor() { }
        // RVA: 0x069530DC  token: 0x600038C
        public System.Void .ctor(UnityEngine.Vector3[] vertices, Dest.Math.Plane3 plane) { }
        // RVA: 0x0695304C  token: 0x600038D
        public System.Void .ctor(System.Int32 vertexCount, Dest.Math.Plane3 plane) { }
        // RVA: 0x06952850  token: 0x600038E
        public System.Void SetVertexProjected(System.Int32 vertexIndex, UnityEngine.Vector3 vertex) { }
        // RVA: 0x069525F0  token: 0x600038F
        public System.Void ProjectVertices() { }
        // RVA: 0x06952490  token: 0x6000390
        public Dest.Math.Edge3 GetEdge(System.Int32 edgeIndex) { }
        // RVA: 0x06952E44  token: 0x6000391
        public System.Void UpdateEdges() { }
        // RVA: 0x06952C6C  token: 0x6000392
        public System.Void UpdateEdge(System.Int32 edgeIndex) { }
        // RVA: 0x069522F8  token: 0x6000393
        public UnityEngine.Vector3 CalcCenter() { }
        // RVA: 0x06952428  token: 0x6000394
        public System.Single CalcPerimeter() { }
        // RVA: 0x069524EC  token: 0x6000395
        public System.Boolean HasZeroCorners(System.Single threshold) { }
        // RVA: 0x06952764  token: 0x6000396
        public System.Void ReverseVertices() { }
        // RVA: 0x06952974  token: 0x6000397
        public Dest.Math.Segment3[] ToSegmentArray() { }
        // RVA: 0x06952ACC  token: 0x6000398
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000079  // size: 0x28
    public sealed struct Ray3
    {
        // Fields
        public UnityEngine.Vector3 Center;  // 0x10
        public UnityEngine.Vector3 Direction;  // 0x1c

        // Methods
        // RVA: 0x04D8BBF0  token: 0x6000399
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& direction) { }
        // RVA: 0x04D871E0  token: 0x600039A
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 direction) { }
        // RVA: 0x06954118  token: 0x600039B
        public static UnityEngine.Ray op_Implicit(Dest.Math.Ray3 value) { }
        // RVA: 0x04D8C560  token: 0x600039C
        public static Dest.Math.Ray3 op_Implicit(UnityEngine.Ray value) { }
        // RVA: 0x0694F664  token: 0x600039D
        public UnityEngine.Vector3 Eval(System.Single t) { }
        // RVA: 0x06954024  token: 0x600039E
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x06954040  token: 0x600039F
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x0695406C  token: 0x60003A0
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200007A  // size: 0x48
    public sealed struct Rectangle3
    {
        // Fields
        public UnityEngine.Vector3 Center;  // 0x10
        public UnityEngine.Vector3 Axis0;  // 0x1c
        public UnityEngine.Vector3 Axis1;  // 0x28
        public UnityEngine.Vector3 Normal;  // 0x34
        public UnityEngine.Vector2 Extents;  // 0x40

        // Methods
        // RVA: 0x06954F9C  token: 0x60003A1
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& axis0, UnityEngine.Vector3& axis1, UnityEngine.Vector2& extents) { }
        // RVA: 0x06954EBC  token: 0x60003A2
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 axis0, UnityEngine.Vector3 axis1, UnityEngine.Vector2 extents) { }
        // RVA: 0x069548A4  token: 0x60003A3
        public static Dest.Math.Rectangle3 CreateFromCCWPoints(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3) { }
        // RVA: 0x06954A9C  token: 0x60003A4
        public static Dest.Math.Rectangle3 CreateFromCWPoints(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3) { }
        // RVA: 0x069543E0  token: 0x60003A5
        public System.Void CalcVertices(UnityEngine.Vector3& vertex0, UnityEngine.Vector3& vertex1, UnityEngine.Vector3& vertex2, UnityEngine.Vector3& vertex3) { }
        // RVA: 0x06954600  token: 0x60003A6
        public UnityEngine.Vector3[] CalcVertices() { }
        // RVA: 0x0695416C  token: 0x60003A7
        public System.Void CalcVertices(UnityEngine.Vector3[] array) { }
        // RVA: 0x04D8C590  token: 0x60003A8
        public System.Single CalcArea() { }
        // RVA: 0x06954C94  token: 0x60003A9
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x06954CB0  token: 0x60003AA
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x06954CDC  token: 0x60003AB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200007B  // size: 0x44
    public sealed struct Segment3
    {
        // Fields
        public UnityEngine.Vector3 P0;  // 0x10
        public UnityEngine.Vector3 P1;  // 0x1c
        public UnityEngine.Vector3 Center;  // 0x28
        public UnityEngine.Vector3 Direction;  // 0x34
        public System.Single Extent;  // 0x40

        // Methods
        // RVA: 0x069555F4  token: 0x60003AC
        public System.Void .ctor(UnityEngine.Vector3& p0, UnityEngine.Vector3& p1) { }
        // RVA: 0x06955594  token: 0x60003AD
        public System.Void .ctor(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1) { }
        // RVA: 0x069556B4  token: 0x60003AE
        public System.Void .ctor(UnityEngine.Vector3& center, UnityEngine.Vector3& direction, System.Single extent) { }
        // RVA: 0x06955654  token: 0x60003AF
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 direction, System.Single extent) { }
        // RVA: 0x069553B8  token: 0x60003B0
        public System.Void SetEndpoints(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1) { }
        // RVA: 0x0695538C  token: 0x60003B1
        public System.Void SetCenterDirectionExtent(UnityEngine.Vector3 center, UnityEngine.Vector3 direction, System.Single extent) { }
        // RVA: 0x069550B4  token: 0x60003B2
        public System.Void CalcCenterDirectionExtent() { }
        // RVA: 0x069551C4  token: 0x60003B3
        public System.Void CalcEndPoints() { }
        // RVA: 0x069552C0  token: 0x60003B4
        public UnityEngine.Vector3 Eval(System.Single s) { }
        // RVA: 0x069552A4  token: 0x60003B5
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x06955360  token: 0x60003B6
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x069553E0  token: 0x60003B7
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200007C  // size: 0x20
    public sealed struct Sphere3
    {
        // Fields
        private static System.Single _4div3mulPi;  // const
        public UnityEngine.Vector3 Center;  // 0x10
        public System.Single Radius;  // 0x1c

        // Methods
        // RVA: 0x04D8C1B0  token: 0x60003B8
        public System.Void .ctor(UnityEngine.Vector3& center, System.Single radius) { }
        // RVA: 0x04D8C1B0  token: 0x60003B9
        public System.Void .ctor(UnityEngine.Vector3 center, System.Single radius) { }
        // RVA: 0x06955B80  token: 0x60003BA
        public static Dest.Math.Sphere3 CreateFromPointsAAB(System.Collections.Generic.IEnumerable<UnityEngine.Vector3> points) { }
        // RVA: 0x06955AA8  token: 0x60003BB
        public static Dest.Math.Sphere3 CreateFromPointsAAB(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x06955CA4  token: 0x60003BC
        public static Dest.Math.Sphere3 CreateFromPointsAverage(System.Collections.Generic.IEnumerable<UnityEngine.Vector3> points) { }
        // RVA: 0x06956050  token: 0x60003BD
        public static Dest.Math.Sphere3 CreateFromPointsAverage(System.Collections.Generic.IList<UnityEngine.Vector3> points) { }
        // RVA: 0x06955784  token: 0x60003BE
        public static System.Boolean CreateCircumscribed(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 v3, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x06956244  token: 0x60003BF
        public static System.Boolean CreateInscribed(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 v3, Dest.Math.Sphere3& sphere) { }
        // RVA: 0x04D8C5B0  token: 0x60003C0
        public System.Single CalcArea() { }
        // RVA: 0x04D8C5D0  token: 0x60003C1
        public System.Single CalcVolume() { }
        // RVA: 0x04D8C5F0  token: 0x60003C2
        public UnityEngine.Vector3 Eval(System.Single theta, System.Single phi) { }
        // RVA: 0x0695686C  token: 0x60003C3
        public System.Single DistanceTo(UnityEngine.Vector3 point) { }
        // RVA: 0x069569E4  token: 0x60003C4
        public UnityEngine.Vector3 Project(UnityEngine.Vector3 point) { }
        // RVA: 0x06955714  token: 0x60003C5
        public System.Boolean Contains(UnityEngine.Vector3& point) { }
        // RVA: 0x06955714  token: 0x60003C6
        public System.Boolean Contains(UnityEngine.Vector3 point) { }
        // RVA: 0x06956888  token: 0x60003C7
        public System.Void Include(Dest.Math.Sphere3& sphere) { }
        // RVA: 0x069569D0  token: 0x60003C8
        public System.Void Include(Dest.Math.Sphere3 sphere) { }
        // RVA: 0x06956A10  token: 0x60003C9
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200007D  // size: 0x34
    public sealed struct Triangle3
    {
        // Fields
        public UnityEngine.Vector3 V0;  // 0x10
        public UnityEngine.Vector3 V1;  // 0x1c
        public UnityEngine.Vector3 V2;  // 0x28

        // Properties
        UnityEngine.Vector3 Item { get; /* RVA: 0x06959734 */ set; /* RVA: 0x06959794 */ }

        // Methods
        // RVA: 0x04D8C6D0  token: 0x60003CC
        public System.Void .ctor(UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x04D8C690  token: 0x60003CD
        public System.Void .ctor(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x069592F8  token: 0x60003CE
        public UnityEngine.Vector3 CalcEdge(System.Int32 edgeIndex) { }
        // RVA: 0x069593BC  token: 0x60003CF
        public UnityEngine.Vector3 CalcNormal() { }
        // RVA: 0x06958FE8  token: 0x60003D0
        public System.Single CalcArea() { }
        // RVA: 0x06958F28  token: 0x60003D1
        public static System.Single CalcArea(UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x06958F28  token: 0x60003D2
        public static System.Single CalcArea(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06958A94  token: 0x60003D3
        public UnityEngine.Vector3 CalcAnglesDeg() { }
        // RVA: 0x069588D0  token: 0x60003D4
        public static UnityEngine.Vector3 CalcAnglesDeg(UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x06958A60  token: 0x60003D5
        public static UnityEngine.Vector3 CalcAnglesDeg(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06958DC0  token: 0x60003D6
        public UnityEngine.Vector3 CalcAnglesRad() { }
        // RVA: 0x06958C20  token: 0x60003D7
        public static UnityEngine.Vector3 CalcAnglesRad(UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2) { }
        // RVA: 0x06958D8C  token: 0x60003D8
        public static UnityEngine.Vector3 CalcAnglesRad(UnityEngine.Vector3 v0, UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x06959560  token: 0x60003D9
        public UnityEngine.Vector3 EvalBarycentric(System.Single c0, System.Single c1) { }
        // RVA: 0x06959470  token: 0x60003DA
        public UnityEngine.Vector3 EvalBarycentric(UnityEngine.Vector3& baryCoords) { }
        // RVA: 0x06959470  token: 0x60003DB
        public UnityEngine.Vector3 EvalBarycentric(UnityEngine.Vector3 baryCoords) { }
        // RVA: 0x069590E0  token: 0x60003DC
        public static System.Void CalcBarycentricCoords(UnityEngine.Vector3& point, UnityEngine.Vector3& v0, UnityEngine.Vector3& v1, UnityEngine.Vector3& v2, UnityEngine.Vector3& baryCoords) { }
        // RVA: 0x069590A8  token: 0x60003DD
        public UnityEngine.Vector3 CalcBarycentricCoords(UnityEngine.Vector3& point) { }
        // RVA: 0x069590A8  token: 0x60003DE
        public UnityEngine.Vector3 CalcBarycentricCoords(UnityEngine.Vector3 point) { }
        // RVA: 0x06959658  token: 0x60003DF
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200007E  // size: 0x14
    public sealed struct Orientations
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.Orientations CW;  // const
        public static Dest.Math.Orientations CCW;  // const
        public static Dest.Math.Orientations None;  // const

    }

    // TypeToken: 0x200007F  // size: 0x18
    public class Query2 : Dest.Math.Query
    {
        // Fields
        private static System.Single Zero;  // static @ 0x0
        private UnityEngine.Vector2[] _vertices;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60003E0
        public System.Void .ctor(UnityEngine.Vector2[] vertices) { }
        // RVA: 0x06953544  token: 0x60003E1
        public System.Int32 ToLine(System.Int32 i, System.Int32 v0, System.Int32 v1) { }
        // RVA: 0x06953450  token: 0x60003E2
        public System.Int32 ToLine(UnityEngine.Vector2& test, System.Int32 v0, System.Int32 v1) { }
        // RVA: 0x06953644  token: 0x60003E3
        public System.Int32 ToTriangle(System.Int32 i, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x0695359C  token: 0x60003E4
        public System.Int32 ToTriangle(UnityEngine.Vector2& test, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x069531F8  token: 0x60003E5
        public System.Int32 ToCircumcircle(System.Int32 i, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x06953258  token: 0x60003E6
        public System.Int32 ToCircumcircle(UnityEngine.Vector2& test, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x04D8C300  token: 0x60003E7
        public System.Single Dot(System.Single x0, System.Single y0, System.Single x1, System.Single y1) { }
        // RVA: 0x04D8C220  token: 0x60003E8
        public System.Single Det2(System.Single x0, System.Single y0, System.Single x1, System.Single y1) { }
        // RVA: 0x04D8C240  token: 0x60003E9
        public System.Single Det3(System.Single x0, System.Single y0, System.Single z0, System.Single x1, System.Single y1, System.Single z1, System.Single x2, System.Single y2, System.Single z2) { }

    }

    // TypeToken: 0x2000080  // size: 0x18
    public class Query3 : Dest.Math.Query
    {
        // Fields
        private static System.Single Zero;  // static @ 0x0
        private UnityEngine.Vector3[] _vertices;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60003EA
        public System.Void .ctor(UnityEngine.Vector3[] vertices) { }
        // RVA: 0x06953AA0  token: 0x60003EB
        public System.Int32 ToPlane(System.Int32 i, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x06953B00  token: 0x60003EC
        public System.Int32 ToPlane(UnityEngine.Vector3& test, System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x06953CBC  token: 0x60003ED
        public System.Int32 ToTetrahedron(System.Int32 i, System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x06953D24  token: 0x60003EE
        public System.Int32 ToTetrahedron(UnityEngine.Vector3& test, System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x06953A38  token: 0x60003EF
        public System.Int32 ToCircumsphere(System.Int32 i, System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x069536A4  token: 0x60003F0
        public System.Int32 ToCircumsphere(UnityEngine.Vector3& test, System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x04D8C540  token: 0x60003F1
        public System.Single Dot(System.Single x0, System.Single y0, System.Single z0, System.Single x1, System.Single y1, System.Single z1) { }
        // RVA: 0x04D8C240  token: 0x60003F2
        public System.Single Det3(System.Single x0, System.Single y0, System.Single z0, System.Single x1, System.Single y1, System.Single z1, System.Single x2, System.Single y2, System.Single z2) { }
        // RVA: 0x04D8C320  token: 0x60003F3
        public System.Single Det4(System.Single x0, System.Single y0, System.Single z0, System.Single w0, System.Single x1, System.Single y1, System.Single z1, System.Single w1, System.Single x2, System.Single y2, System.Single z2, System.Single w2, System.Single x3, System.Single y3, System.Single z3, System.Single w3) { }

    }

    // TypeToken: 0x2000081  // size: 0x14
    public sealed struct QueryTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.QueryTypes Int64;  // const
        public static Dest.Math.QueryTypes Integer;  // const
        public static Dest.Math.QueryTypes Rational;  // const
        public static Dest.Math.QueryTypes Real;  // const
        public static Dest.Math.QueryTypes Filtered;  // const

    }

    // TypeToken: 0x2000082  // size: 0x10
    public class Query
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003F4
        protected System.Void .ctor() { }
        // RVA: 0x06954010  token: 0x60003F5
        public static System.Boolean Sort(System.Int32& v0, System.Int32& v1) { }
        // RVA: 0x06953E10  token: 0x60003F6
        public static System.Boolean Sort(System.Int32& v0, System.Int32& v1, System.Int32& v2) { }
        // RVA: 0x06953E80  token: 0x60003F7
        public static System.Boolean Sort(System.Int32& v0, System.Int32& v1, System.Int32& v2, System.Int32& v3) { }

    }

    // TypeToken: 0x2000083  // size: 0x48
    public class CatmullRomSpline3 : Dest.Math.SplineBase
    {
        // Properties
        Dest.Math.SplineTypes SplineType { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x0694AB98 */ }

        // Methods
        // RVA: 0x0694A044  token: 0x60003FA
        public static Dest.Math.CatmullRomSpline3 Create() { }
        // RVA: 0x0694A0C0  token: 0x60003FB
        public static Dest.Math.CatmullRomSpline3 Create(System.Collections.Generic.IList<UnityEngine.Vector3> points, Dest.Math.SplineTypes type) { }
        // RVA: 0x0694A6AC  token: 0x60003FC
        private System.Void UpdateSegment(System.Int32 index) { }
        // RVA: 0x0694A57C  token: 0x60003FD
        private System.Void UpdateAdjacentSegments(System.Int32 vertexIndex) { }
        // RVA: 0x06949F18  token: 0x60003FE
        public virtual System.Void AddVertexFirst(UnityEngine.Vector3 position) { }
        // RVA: 0x06949F4C  token: 0x60003FF
        public virtual System.Void AddVertexLast(UnityEngine.Vector3 position) { }
        // RVA: 0x0694A48C  token: 0x6000400
        public virtual System.Void RemoveVertex(System.Int32 index) { }
        // RVA: 0x06949FF8  token: 0x6000401
        public virtual System.Void Clear() { }
        // RVA: 0x0694A3EC  token: 0x6000402
        public virtual System.Void InsertBefore(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694A348  token: 0x6000403
        public virtual System.Void InsertAfter(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694A2D0  token: 0x6000404
        public virtual UnityEngine.Vector3 GetVertex(System.Int32 vertexIndex) { }
        // RVA: 0x0694A4F4  token: 0x6000405
        public virtual System.Void SetVertex(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694AB90  token: 0x6000406
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000084  // size: 0x48
    public class CubicSpline3 : Dest.Math.SplineBase
    {
        // Properties
        Dest.Math.SplineTypes SplineType { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x0694D844 */ }

        // Methods
        // RVA: 0x0694D340  token: 0x6000409
        public static Dest.Math.CubicSpline3 Create() { }
        // RVA: 0x0694D3BC  token: 0x600040A
        public static Dest.Math.CubicSpline3 Create(System.Collections.Generic.IList<UnityEngine.Vector3> points, Dest.Math.SplineTypes type) { }
        // RVA: 0x0694BCE4  token: 0x600040B
        private System.Void BuildSpline() { }
        // RVA: 0x0694CB5C  token: 0x600040C
        private System.Void CreateOpenedSpline() { }
        // RVA: 0x0694BD98  token: 0x600040D
        private System.Void CreateClosedSpline() { }
        // RVA: 0x0694BBC0  token: 0x600040E
        public virtual System.Void AddVertexFirst(UnityEngine.Vector3 position) { }
        // RVA: 0x0694BC54  token: 0x600040F
        public virtual System.Void AddVertexLast(UnityEngine.Vector3 position) { }
        // RVA: 0x0694D75C  token: 0x6000410
        public virtual System.Void RemoveVertex(System.Int32 index) { }
        // RVA: 0x0694BD4C  token: 0x6000411
        public virtual System.Void Clear() { }
        // RVA: 0x0694D6BC  token: 0x6000412
        public virtual System.Void InsertBefore(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694D61C  token: 0x6000413
        public virtual System.Void InsertAfter(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694D5A4  token: 0x6000414
        public virtual UnityEngine.Vector3 GetVertex(System.Int32 vertexIndex) { }
        // RVA: 0x0694D7C0  token: 0x6000415
        public virtual System.Void SetVertex(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x0694AB90  token: 0x6000416
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct SplineTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.SplineTypes Open;  // const
        public static Dest.Math.SplineTypes Closed;  // const

    }

    // TypeToken: 0x2000086  // size: 0x14
    public sealed struct SplinePlaneTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Dest.Math.SplinePlaneTypes XZ;  // const
        public static Dest.Math.SplinePlaneTypes XY;  // const
        public static Dest.Math.SplinePlaneTypes YZ;  // const

    }

    // TypeToken: 0x2000087  // size: 0x28
    public sealed struct PositionTangent
    {
        // Fields
        public UnityEngine.Vector3 Position;  // 0x10
        public UnityEngine.Vector3 Tangent;  // 0x1c

    }

    // TypeToken: 0x2000088  // size: 0x40
    public sealed struct CurveFrame
    {
        // Fields
        public UnityEngine.Vector3 Position;  // 0x10
        public UnityEngine.Vector3 Tangent;  // 0x1c
        public UnityEngine.Vector3 Normal;  // 0x28
        public UnityEngine.Vector3 Binormal;  // 0x34

    }

    // TypeToken: 0x2000089  // size: 0x48
    public abstract class SplineBase : UnityEngine.MonoBehaviour
    {
        // Fields
        protected Dest.Math.SplineBase.ArcLengthParametrization _parametrization;  // 0x18
        protected System.Collections.Generic.List<Dest.Math.SplineBase.ItemData> _data;  // 0x20
        protected Dest.Math.SplineTypes _type;  // 0x28
        protected System.Boolean _recalcSegmentsLength;  // 0x2c
        protected UnityEngine.Color _renderColor;  // 0x30
        protected Dest.Math.SplinePlaneTypes _creationPlane;  // 0x40

        // Properties
        System.Int32 SegmentCount { get; /* RVA: 0x069587FC */ }
        System.Int32 VertexCount { get; /* RVA: 0x06958890 */ }
        System.Boolean Valid { get; /* RVA: 0x0695884C */ }
        Dest.Math.SplineTypes SplineType { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x06956AA8  token: 0x600041C
        private System.Void Awake() { }
        // RVA: 0x06958764  token: 0x600041D
        protected System.Void .ctor() { }
        // RVA: 0x06957FCC  token: 0x600041E
        protected System.Void GetSegmentIndexAndTime(System.Single time, System.Int32& segmentIndex, System.Single& segmentTime) { }
        // RVA: 0x069585FC  token: 0x600041F
        protected System.Void PrepareForRuntime() { }
        // RVA: 0x06958690  token: 0x6000420
        protected System.Void RecalcSegmentsLength() { }
        // RVA: -1  // abstract  token: 0x6000421
        public virtual System.Void AddVertexFirst(UnityEngine.Vector3 position) { }
        // RVA: -1  // abstract  token: 0x6000422
        public virtual System.Void AddVertexLast(UnityEngine.Vector3 position) { }
        // RVA: -1  // abstract  token: 0x6000423
        public virtual System.Void RemoveVertex(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x6000424
        public virtual System.Void Clear() { }
        // RVA: -1  // abstract  token: 0x6000425
        public virtual System.Void InsertBefore(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: -1  // abstract  token: 0x6000426
        public virtual System.Void InsertAfter(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: -1  // abstract  token: 0x6000427
        public virtual UnityEngine.Vector3 GetVertex(System.Int32 vertexIndex) { }
        // RVA: -1  // abstract  token: 0x6000428
        public virtual System.Void SetVertex(System.Int32 vertexIndex, UnityEngine.Vector3 position) { }
        // RVA: 0x069578C8  token: 0x6000429
        public UnityEngine.Vector3 EvalPosition(System.Single time) { }
        // RVA: 0x06957B0C  token: 0x600042A
        public UnityEngine.Vector3 EvalTangent(System.Single time) { }
        // RVA: 0x0695772C  token: 0x600042B
        public Dest.Math.PositionTangent EvalPositionTangent(System.Single time) { }
        // RVA: 0x06957814  token: 0x600042C
        public System.Void EvalPosition(System.Single time, UnityEngine.Vector3& position) { }
        // RVA: 0x06957BC0  token: 0x600042D
        public System.Void EvalTangent(System.Single time, UnityEngine.Vector3& tangent) { }
        // RVA: 0x06957650  token: 0x600042E
        public System.Void EvalPositionTangent(System.Single time, Dest.Math.PositionTangent& positionTangent) { }
        // RVA: 0x069570A4  token: 0x600042F
        public System.Void EvalFrame(System.Single time, Dest.Math.CurveFrame& frame) { }
        // RVA: 0x06956CE0  token: 0x6000430
        public System.Single EvalCurvature(System.Single time) { }
        // RVA: 0x06957E2C  token: 0x6000431
        public System.Single EvalTorsion(System.Single time) { }
        // RVA: 0x069572CC  token: 0x6000432
        public UnityEngine.Vector3 EvalPositionParametrized(System.Single length) { }
        // RVA: 0x06957A44  token: 0x6000433
        public UnityEngine.Vector3 EvalTangentParametrized(System.Single length) { }
        // RVA: 0x0695745C  token: 0x6000434
        public Dest.Math.PositionTangent EvalPositionTangentParametrized(System.Single length) { }
        // RVA: 0x06957394  token: 0x6000435
        public System.Void EvalPositionParametrized(System.Single length, UnityEngine.Vector3& position) { }
        // RVA: 0x0695797C  token: 0x6000436
        public System.Void EvalTangentParametrized(System.Single length, UnityEngine.Vector3& tangent) { }
        // RVA: 0x0695755C  token: 0x6000437
        public System.Void EvalPositionTangentParametrized(System.Single length, Dest.Math.PositionTangent& positionTangent) { }
        // RVA: 0x06956E64  token: 0x6000438
        public System.Void EvalFrameParametrized(System.Single length, Dest.Math.CurveFrame& frame) { }
        // RVA: 0x06956B44  token: 0x6000439
        public System.Single EvalCurvatureParametrized(System.Single length) { }
        // RVA: 0x06957C74  token: 0x600043A
        public System.Single EvalTorsionParametrized(System.Single length) { }
        // RVA: 0x06956AB0  token: 0x600043B
        public System.Single CalcTotalLength() { }
        // RVA: 0x06958084  token: 0x600043C
        public System.Single LengthToTime(System.Single length, System.Int32 iterations, System.Single tolerance) { }
        // RVA: 0x0695805C  token: 0x600043D
        public System.Single LengthToTime(System.Single length) { }
        // RVA: 0x0695834C  token: 0x600043E
        public System.Single ParametrizeByArcLength(System.Int32 pointCount) { }

    }

    // TypeToken: 0x200008C  // size: 0x10
    public static class PointsFilter
    {
        // Methods
        // RVA: 0x069519BC  token: 0x600044D
        public static System.Collections.Generic.List<System.Int32> DistanceFilter(UnityEngine.Vector3[] points, Dest.Math.AAB3 pointsAAB, System.Single radius, Dest.Math.Rand rand) { }

    }

    // TypeToken: 0x200008E  // size: 0x78
    public class PoissonDiskSampler
    {
        // Fields
        private static readonly System.Single one_div_sqrtTwo;  // static @ 0x0
        private Dest.Math.Rand _rand;  // 0x10
        private System.Collections.Generic.List<System.Int32> _activeList;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Vector2> _points;  // 0x20
        private System.Nullable<System.Int32>[,] _grid;  // 0x28
        private System.Single _r;  // 0x30
        private System.Single _rSquared;  // 0x34
        private System.Single _rTwo;  // 0x38
        private System.Single _rMin;  // 0x3c
        private UnityEngine.Vector2 _min;  // 0x40
        private UnityEngine.Vector2 _max;  // 0x48
        private UnityEngine.Vector2 _size;  // 0x50
        private System.Single _cellSize;  // 0x58
        private System.Int32 _cellsX;  // 0x5c
        private System.Int32 _cellsY;  // 0x60
        public Dest.Math.PoissonDiskSampler.PointDelegate DistanceFilter;  // 0x68
        public System.Int32 PointsPerStep;  // 0x70
        private System.Int32 <MaxPoints>k__BackingField;  // 0x74

        // Properties
        System.Int32 MaxPoints { get; /* RVA: 0x04D868B0 */ set; /* RVA: 0x04D868E0 */ }

        // Methods
        // RVA: 0x06952060  token: 0x6000455
        private static System.Void .cctor() { }
        // RVA: 0x069520B0  token: 0x6000456
        public System.Void .ctor(Dest.Math.Rand rand, UnityEngine.Vector2 minCorner, UnityEngine.Vector2 maxCorner, System.Single minDistanceOuter, System.Single minDistanceInner) { }
        // RVA: 0x06951D98  token: 0x6000457
        private System.Void CalcGridIndices(UnityEngine.Vector2& point, System.Int32& i, System.Int32& j) { }
        // RVA: 0x06951DC4  token: 0x6000458
        private System.Void InsertIntoGrid(UnityEngine.Vector2& point, System.Int32 index) { }
        // RVA: 0x06951A70  token: 0x6000459
        private System.Boolean AddPoint(UnityEngine.Vector2& center) { }
        // RVA: 0x06951E5C  token: 0x600045A
        public System.Collections.Generic.List<UnityEngine.Vector2> Sample() { }

    }

    // TypeToken: 0x2000090  // size: 0x28
    public class WeightedSampler
    {
        // Fields
        protected System.Single[] _accum;  // 0x10
        protected System.Single _total;  // 0x18
        protected Dest.Math.Rand _rand;  // 0x20

        // Methods
        // RVA: 0x05390F40  token: 0x600045F
        protected System.Void .ctor(Dest.Math.Rand rand) { }
        // RVA: 0x069597D8  token: 0x6000460
        protected static System.Int32 BinarySearch(System.Single[] array, System.Single value) { }
        // RVA: 0x069599AC  token: 0x6000461
        public System.Void .ctor(System.Single[] weights) { }
        // RVA: 0x06959980  token: 0x6000462
        public System.Void .ctor(System.Single[] weights, Dest.Math.Rand rand) { }
        // RVA: 0x06959844  token: 0x6000463
        protected System.Void Init(System.Single[] weights) { }
        // RVA: 0x06959940  token: 0x6000464
        public System.Int32 SampleIndex() { }

    }

    // TypeToken: 0x2000091  // size: 0x30
    public class TrianglesSamplerBase : Dest.Math.WeightedSampler
    {
        // Fields
        protected UnityEngine.Vector3[] _vertices;  // 0x28

        // Methods
        // RVA: 0x05390F40  token: 0x6000465
        protected System.Void .ctor(Dest.Math.Rand rand) { }
        // RVA: 0x04D8C710  token: 0x6000466
        protected System.Single CalclTriangleArea(UnityEngine.Vector3& e0, UnityEngine.Vector3& e1) { }

    }

    // TypeToken: 0x2000092  // size: 0x38
    public class IndexedTrianglesSampler : Dest.Math.TrianglesSamplerBase
    {
        // Fields
        protected System.Int32[] _indices;  // 0x30

        // Methods
        // RVA: 0x0694EB80  token: 0x6000467
        public System.Void .ctor(UnityEngine.Vector3[] vertices, System.Int32[] indices) { }
        // RVA: 0x0694EC10  token: 0x6000468
        public System.Void .ctor(UnityEngine.Vector3[] vertices, System.Int32[] indices, Dest.Math.Rand rand) { }
        // RVA: 0x0694EC68  token: 0x6000469
        public System.Void .ctor(UnityEngine.Mesh mesh) { }
        // RVA: 0x0694ED10  token: 0x600046A
        public System.Void .ctor(UnityEngine.Mesh mesh, Dest.Math.Rand rand) { }
        // RVA: 0x0694E214  token: 0x600046B
        protected System.Void Init() { }
        // RVA: 0x0694EA74  token: 0x600046C
        public UnityEngine.Vector3 Sample() { }
        // RVA: 0x0694E90C  token: 0x600046D
        public UnityEngine.Vector3[] SampleArray(System.Int32 count) { }
        // RVA: 0x0694E5D4  token: 0x600046E
        public UnityEngine.Vector3[] SampleArray(System.Int32 count, UnityEngine.Vector2[] uvs, UnityEngine.Texture2D sampleMap, System.Single min, System.Single max) { }

    }

    // TypeToken: 0x2000093  // size: 0x30
    public class NonIndexedTrianglesSampler : Dest.Math.TrianglesSamplerBase
    {
        // Methods
        // RVA: 0x0694FDF0  token: 0x600046F
        public System.Void .ctor(UnityEngine.Vector3[] vertices) { }
        // RVA: 0x0694FCDC  token: 0x6000470
        public System.Void .ctor(UnityEngine.Vector3[] vertices, Dest.Math.Rand rand) { }
        // RVA: 0x0694FD10  token: 0x6000471
        public System.Void .ctor(UnityEngine.Mesh mesh) { }
        // RVA: 0x0694FD9C  token: 0x6000472
        public System.Void .ctor(UnityEngine.Mesh mesh, Dest.Math.Rand rand) { }
        // RVA: 0x0694F7B8  token: 0x6000473
        protected System.Void Init() { }
        // RVA: 0x0694FC10  token: 0x6000474
        public UnityEngine.Vector3 Sample() { }
        // RVA: 0x0694FAEC  token: 0x6000475
        public UnityEngine.Vector3[] SampleArray(System.Int32 count) { }

    }

    // TypeToken: 0x2000094
    public class ShuffleBag`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Dest.Math.Rand _rand;  // 0x0
        private System.Collections.Generic.List<T> _items;  // 0x0
        private System.Int32 _index;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000477
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000478
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000479
        public System.Void .ctor(Dest.Math.Rand rand) { }
        // RVA: -1  // not resolved  token: 0x600047A
        public System.Void .ctor(Dest.Math.Rand rand, System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x600047B
        public System.Void Add(T item, System.UInt32 count) { }
        // RVA: -1  // not resolved  token: 0x600047C
        public T NextItem() { }
        // RVA: -1  // not resolved  token: 0x600047D
        public System.Void Reset() { }
        // RVA: -1  // not resolved  token: 0x600047E
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600047F
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000480
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000096  // size: 0x10
    public static class Util
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000487
        public static System.Void Shuffle(System.Collections.Generic.IList<T> collection) { }
        // RVA: -1  // generic def  token: 0x6000488
        public static System.Void Shuffle(System.Collections.Generic.IList<T> collection, Dest.Math.Rand rand) { }
        // RVA: -1  // generic def  token: 0x6000489
        public static System.Void Shuffle(T[] collection) { }
        // RVA: -1  // generic def  token: 0x600048A
        public static System.Void Shuffle(T[] collection, Dest.Math.Rand rand) { }

    }

}

