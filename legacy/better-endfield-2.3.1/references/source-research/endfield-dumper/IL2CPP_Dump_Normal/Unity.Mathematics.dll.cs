// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Mathematics.dll
// Classes:  94
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000005  // size: 0x11
    public sealed struct RotationOrder
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Unity.Mathematics.math.RotationOrder XYZ;  // const
        public static Unity.Mathematics.math.RotationOrder XZY;  // const
        public static Unity.Mathematics.math.RotationOrder YXZ;  // const
        public static Unity.Mathematics.math.RotationOrder YZX;  // const
        public static Unity.Mathematics.math.RotationOrder ZXY;  // const
        public static Unity.Mathematics.math.RotationOrder ZYX;  // const
        public static Unity.Mathematics.math.RotationOrder Default;  // const

    }

    // TypeToken: 0x2000006  // size: 0x11
    public sealed struct ShuffleComponent
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Unity.Mathematics.math.ShuffleComponent LeftX;  // const
        public static Unity.Mathematics.math.ShuffleComponent LeftY;  // const
        public static Unity.Mathematics.math.ShuffleComponent LeftZ;  // const
        public static Unity.Mathematics.math.ShuffleComponent LeftW;  // const
        public static Unity.Mathematics.math.ShuffleComponent RightX;  // const
        public static Unity.Mathematics.math.ShuffleComponent RightY;  // const
        public static Unity.Mathematics.math.ShuffleComponent RightZ;  // const
        public static Unity.Mathematics.math.ShuffleComponent RightW;  // const

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed struct LongDoubleUnion
    {
        // Fields
        public System.Int64 longValue;  // 0x10
        public System.Double doubleValue;  // 0x10

    }

    // TypeToken: 0x2000009  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200000E  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000013  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000018  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200001D  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000022  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000027  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200002C  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000031  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000037  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000039  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200003B  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200003D  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000042  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000047  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000051  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x2000056  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

    // TypeToken: 0x200005B  // size: 0x10
    public sealed class DebuggerProxy
    {
    }

namespace Unity.IL2CPP.CompilerServices
{

    // TypeToken: 0x2000002  // size: 0x10
    public class Il2CppEagerStaticClassConstructionAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace Unity.Mathematics
{

    // TypeToken: 0x2000003  // size: 0x40
    public sealed struct AffineTransform : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float3x3 rs;  // 0x10
        public Unity.Mathematics.float3 t;  // 0x34
        public static readonly Unity.Mathematics.AffineTransform identity;  // static @ 0x0

        // Methods
        // RVA: 0x0B27CF08  token: 0x6000002
        public System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation) { }
        // RVA: 0x0B27CE60  token: 0x6000003
        public System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale) { }
        // RVA: 0x04DB21B0  token: 0x6000004
        public System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.float3x3 rotationScale) { }
        // RVA: 0x0B27CDF8  token: 0x6000005
        public System.Void .ctor(Unity.Mathematics.float3x3 rotationScale) { }
        // RVA: 0x0B27CDA0  token: 0x6000006
        public System.Void .ctor(Unity.Mathematics.RigidTransform rigid) { }
        // RVA: 0x05F0065C  token: 0x6000007
        public System.Void .ctor(Unity.Mathematics.float3x4 m) { }
        // RVA: 0x05F00794  token: 0x6000008
        public System.Void .ctor(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B27C704  token: 0x6000009
        public virtual System.Boolean Equals(Unity.Mathematics.AffineTransform rhs) { }
        // RVA: 0x0B27C688  token: 0x600000A
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27C76C  token: 0x600000B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27CA70  token: 0x600000C
        public virtual System.String ToString() { }
        // RVA: 0x0B27C79C  token: 0x600000D
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04CE39A0  token: 0x600000E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class math
    {
        // Fields
        public static System.Double E_DBL;  // const
        public static System.Double LOG2E_DBL;  // const
        public static System.Double LOG10E_DBL;  // const
        public static System.Double LN2_DBL;  // const
        public static System.Double LN10_DBL;  // const
        public static System.Double PI_DBL;  // const
        public static System.Double PI2_DBL;  // const
        public static System.Double PIHALF_DBL;  // const
        public static System.Double TAU_DBL;  // const
        public static System.Double TODEGREES_DBL;  // const
        public static System.Double TORADIANS_DBL;  // const
        public static System.Double SQRT2_DBL;  // const
        public static System.Double EPSILON_DBL;  // const
        public static System.Double INFINITY_DBL;  // const
        public static System.Double NAN_DBL;  // const
        public static System.Single FLT_MIN_NORMAL;  // const
        public static System.Double DBL_MIN_NORMAL;  // const
        public static System.Single E;  // const
        public static System.Single LOG2E;  // const
        public static System.Single LOG10E;  // const
        public static System.Single LN2;  // const
        public static System.Single LN10;  // const
        public static System.Single PI;  // const
        public static System.Single PI2;  // const
        public static System.Single PIHALF;  // const
        public static System.Single TAU;  // const
        public static System.Single TODEGREES;  // const
        public static System.Single TORADIANS;  // const
        public static System.Single SQRT2;  // const
        public static System.Single EPSILON;  // const
        public static System.Single INFINITY;  // const
        public static System.Single NAN;  // const

        // Methods
        // RVA: 0x0B24A44C  token: 0x600000F
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation) { }
        // RVA: 0x0B24A498  token: 0x6000010
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale) { }
        // RVA: 0x04DACFF0  token: 0x6000011
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.float3x3 rotationScale) { }
        // RVA: 0x0B24A3BC  token: 0x6000012
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3x3 rotationScale) { }
        // RVA: 0x0B24A400  token: 0x6000013
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B24A540  token: 0x6000014
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3x4 m) { }
        // RVA: 0x0B24A4FC  token: 0x6000015
        public static Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.RigidTransform rigid) { }
        // RVA: 0x0B252CF4  token: 0x6000016
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.AffineTransform transform) { }
        // RVA: 0x0B2523A8  token: 0x6000017
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.AffineTransform transform) { }
        // RVA: 0x0B2685A8  token: 0x6000018
        public static Unity.Mathematics.AffineTransform mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.AffineTransform b) { }
        // RVA: 0x0B2672B8  token: 0x6000019
        public static Unity.Mathematics.AffineTransform mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.AffineTransform b) { }
        // RVA: 0x0B26A504  token: 0x600001A
        public static Unity.Mathematics.AffineTransform mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3x3 b) { }
        // RVA: 0x0B26D034  token: 0x600001B
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float4 pos) { }
        // RVA: 0x0B270778  token: 0x600001C
        public static Unity.Mathematics.float3 rotate(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3 dir) { }
        // RVA: 0x0B273EEC  token: 0x600001D
        public static Unity.Mathematics.float3 transform(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3 pos) { }
        // RVA: 0x0B25B258  token: 0x600001E
        public static Unity.Mathematics.AffineTransform inverse(Unity.Mathematics.AffineTransform a) { }
        // RVA: 0x0B24E08C  token: 0x600001F
        public static System.Void decompose(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3& translation, Unity.Mathematics.quaternion& rotation, Unity.Mathematics.float3& scale) { }
        // RVA: 0x0B253A70  token: 0x6000020
        public static System.UInt32 hash(Unity.Mathematics.AffineTransform a) { }
        // RVA: 0x0B2580C4  token: 0x6000021
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.AffineTransform a) { }
        // RVA: 0x04DAD570  token: 0x6000022
        public static Unity.Mathematics.bool2 bool2(System.Boolean x, System.Boolean y) { }
        // RVA: 0x04DAD590  token: 0x6000023
        public static Unity.Mathematics.bool2 bool2(Unity.Mathematics.bool2 xy) { }
        // RVA: 0x04DAD580  token: 0x6000024
        public static Unity.Mathematics.bool2 bool2(System.Boolean v) { }
        // RVA: 0x0B2551F0  token: 0x6000025
        public static System.UInt32 hash(Unity.Mathematics.bool2 v) { }
        // RVA: 0x0B257960  token: 0x6000026
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2 v) { }
        // RVA: 0x05F33718  token: 0x6000027
        public static System.Boolean shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F35030  token: 0x6000028
        public static Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F338F8  token: 0x6000029
        public static Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F33EC4  token: 0x600002A
        public static Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F32CE0  token: 0x600002B
        private static System.Boolean select_shuffle_component(Unity.Mathematics.bool2 a, Unity.Mathematics.bool2 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAD5B0  token: 0x600002C
        public static Unity.Mathematics.bool2x2 bool2x2(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1) { }
        // RVA: 0x05F08B08  token: 0x600002D
        public static Unity.Mathematics.bool2x2 bool2x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11) { }
        // RVA: 0x0B24C8BC  token: 0x600002E
        public static Unity.Mathematics.bool2x2 bool2x2(System.Boolean v) { }
        // RVA: 0x0B274708  token: 0x600002F
        public static Unity.Mathematics.bool2x2 transpose(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B255014  token: 0x6000030
        public static System.UInt32 hash(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B2595B0  token: 0x6000031
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x04DAD5C0  token: 0x6000032
        public static Unity.Mathematics.bool2x3 bool2x3(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2) { }
        // RVA: 0x05F08B38  token: 0x6000033
        public static Unity.Mathematics.bool2x3 bool2x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12) { }
        // RVA: 0x0B24C8E0  token: 0x6000034
        public static Unity.Mathematics.bool2x3 bool2x3(System.Boolean v) { }
        // RVA: 0x0B274414  token: 0x6000035
        public static Unity.Mathematics.bool3x2 transpose(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B255F90  token: 0x6000036
        public static System.UInt32 hash(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B257D68  token: 0x6000037
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x04DAD5E0  token: 0x6000038
        public static Unity.Mathematics.bool2x4 bool2x4(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2, Unity.Mathematics.bool2 c3) { }
        // RVA: 0x0B24C8FC  token: 0x6000039
        public static Unity.Mathematics.bool2x4 bool2x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13) { }
        // RVA: 0x0B24C954  token: 0x600003A
        public static Unity.Mathematics.bool2x4 bool2x4(System.Boolean v) { }
        // RVA: 0x0B274D54  token: 0x600003B
        public static Unity.Mathematics.bool4x2 transpose(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B255460  token: 0x600003C
        public static System.UInt32 hash(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B25A170  token: 0x600003D
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x04DAD610  token: 0x600003E
        public static Unity.Mathematics.bool3 bool3(System.Boolean x, System.Boolean y, System.Boolean z) { }
        // RVA: 0x04DAD650  token: 0x600003F
        public static Unity.Mathematics.bool3 bool3(System.Boolean x, Unity.Mathematics.bool2 yz) { }
        // RVA: 0x04DAD630  token: 0x6000040
        public static Unity.Mathematics.bool3 bool3(Unity.Mathematics.bool2 xy, System.Boolean z) { }
        // RVA: 0x04DAD670  token: 0x6000041
        public static Unity.Mathematics.bool3 bool3(Unity.Mathematics.bool3 xyz) { }
        // RVA: 0x04DAD620  token: 0x6000042
        public static Unity.Mathematics.bool3 bool3(System.Boolean v) { }
        // RVA: 0x05F0F4F0  token: 0x6000043
        public static System.UInt32 hash(Unity.Mathematics.bool3 v) { }
        // RVA: 0x0B258B64  token: 0x6000044
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3 v) { }
        // RVA: 0x05F34180  token: 0x6000045
        public static System.Boolean shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B272344  token: 0x6000046
        public static Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B271EC8  token: 0x6000047
        public static Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B271710  token: 0x6000048
        public static Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F33128  token: 0x6000049
        private static System.Boolean select_shuffle_component(Unity.Mathematics.bool3 a, Unity.Mathematics.bool3 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAD690  token: 0x600004A
        public static Unity.Mathematics.bool3x2 bool3x2(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1) { }
        // RVA: 0x05F08BF4  token: 0x600004B
        public static Unity.Mathematics.bool3x2 bool3x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21) { }
        // RVA: 0x0B24C978  token: 0x600004C
        public static Unity.Mathematics.bool3x2 bool3x2(System.Boolean v) { }
        // RVA: 0x0B274FE4  token: 0x600004D
        public static Unity.Mathematics.bool2x3 transpose(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x05F11430  token: 0x600004E
        public static System.UInt32 hash(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x0B259804  token: 0x600004F
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x04DAD6C0  token: 0x6000050
        public static Unity.Mathematics.bool3x3 bool3x3(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2) { }
        // RVA: 0x05F08C2C  token: 0x6000051
        public static Unity.Mathematics.bool3x3 bool3x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22) { }
        // RVA: 0x0B24C994  token: 0x6000052
        public static Unity.Mathematics.bool3x3 bool3x3(System.Boolean v) { }
        // RVA: 0x0B274CF4  token: 0x6000053
        public static Unity.Mathematics.bool3x3 transpose(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x05F12254  token: 0x6000054
        public static System.UInt32 hash(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x0B25992C  token: 0x6000055
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x04DAD700  token: 0x6000056
        public static Unity.Mathematics.bool3x4 bool3x4(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2, Unity.Mathematics.bool3 c3) { }
        // RVA: 0x05F08C8C  token: 0x6000057
        public static Unity.Mathematics.bool3x4 bool3x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23) { }
        // RVA: 0x0B24C9B0  token: 0x6000058
        public static Unity.Mathematics.bool3x4 bool3x4(System.Boolean v) { }
        // RVA: 0x0B27502C  token: 0x6000059
        public static Unity.Mathematics.bool4x3 transpose(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x05F104F0  token: 0x600005A
        public static System.UInt32 hash(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x0B2584C8  token: 0x600005B
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x04DAD790  token: 0x600005C
        public static Unity.Mathematics.bool4 bool4(System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w) { }
        // RVA: 0x04DAD820  token: 0x600005D
        public static Unity.Mathematics.bool4 bool4(System.Boolean x, System.Boolean y, Unity.Mathematics.bool2 zw) { }
        // RVA: 0x04DAD770  token: 0x600005E
        public static Unity.Mathematics.bool4 bool4(System.Boolean x, Unity.Mathematics.bool2 yz, System.Boolean w) { }
        // RVA: 0x04DAD870  token: 0x600005F
        public static Unity.Mathematics.bool4 bool4(System.Boolean x, Unity.Mathematics.bool3 yzw) { }
        // RVA: 0x04DAD750  token: 0x6000060
        public static Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool2 xy, System.Boolean z, System.Boolean w) { }
        // RVA: 0x04DAD7D0  token: 0x6000061
        public static Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool2 xy, Unity.Mathematics.bool2 zw) { }
        // RVA: 0x04DAD840  token: 0x6000062
        public static Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool3 xyz, System.Boolean w) { }
        // RVA: 0x04DAD7F0  token: 0x6000063
        public static Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool4 xyzw) { }
        // RVA: 0x04DAD7B0  token: 0x6000064
        public static Unity.Mathematics.bool4 bool4(System.Boolean v) { }
        // RVA: 0x05F10CE8  token: 0x6000065
        public static System.UInt32 hash(Unity.Mathematics.bool4 v) { }
        // RVA: 0x0B259D7C  token: 0x6000066
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4 v) { }
        // RVA: 0x05F33710  token: 0x6000067
        public static System.Boolean shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F353D8  token: 0x6000068
        public static Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F34EB0  token: 0x6000069
        public static Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F34570  token: 0x600006A
        public static Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F32BB4  token: 0x600006B
        private static System.Boolean select_shuffle_component(Unity.Mathematics.bool4 a, Unity.Mathematics.bool4 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAD8A0  token: 0x600006C
        public static Unity.Mathematics.bool4x2 bool4x2(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1) { }
        // RVA: 0x0B24C9CC  token: 0x600006D
        public static Unity.Mathematics.bool4x2 bool4x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21, System.Boolean m30, System.Boolean m31) { }
        // RVA: 0x0B24CA24  token: 0x600006E
        public static Unity.Mathematics.bool4x2 bool4x2(System.Boolean v) { }
        // RVA: 0x0B2742A0  token: 0x600006F
        public static Unity.Mathematics.bool2x4 transpose(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x05F0FFD4  token: 0x6000070
        public static System.UInt32 hash(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x05F15008  token: 0x6000071
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x04DAD8B0  token: 0x6000072
        public static Unity.Mathematics.bool4x3 bool4x3(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2) { }
        // RVA: 0x05F08DC4  token: 0x6000073
        public static Unity.Mathematics.bool4x3 bool4x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m30, System.Boolean m31, System.Boolean m32) { }
        // RVA: 0x0B24CA48  token: 0x6000074
        public static Unity.Mathematics.bool4x3 bool4x3(System.Boolean v) { }
        // RVA: 0x0B2750F8  token: 0x6000075
        public static Unity.Mathematics.bool3x4 transpose(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x05F10D90  token: 0x6000076
        public static System.UInt32 hash(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x05F1479C  token: 0x6000077
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x04DAD8C0  token: 0x6000078
        public static Unity.Mathematics.bool4x4 bool4x4(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2, Unity.Mathematics.bool4 c3) { }
        // RVA: 0x05F08E40  token: 0x6000079
        public static Unity.Mathematics.bool4x4 bool4x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23, System.Boolean m30, System.Boolean m31, System.Boolean m32, System.Boolean m33) { }
        // RVA: 0x0B24CA64  token: 0x600007A
        public static Unity.Mathematics.bool4x4 bool4x4(System.Boolean v) { }
        // RVA: 0x0B274210  token: 0x600007B
        public static Unity.Mathematics.bool4x4 transpose(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x05F1201C  token: 0x600007C
        public static System.UInt32 hash(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x05F1628C  token: 0x600007D
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x04DAEB80  token: 0x600007E
        public static Unity.Mathematics.double2 double2(System.Double x, System.Double y) { }
        // RVA: 0x04DAEC90  token: 0x600007F
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.double2 xy) { }
        // RVA: 0x04DAECB0  token: 0x6000080
        public static Unity.Mathematics.double2 double2(System.Double v) { }
        // RVA: 0x0B24E794  token: 0x6000081
        public static Unity.Mathematics.double2 double2(System.Boolean v) { }
        // RVA: 0x0B24E73C  token: 0x6000082
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DAEC50  token: 0x6000083
        public static Unity.Mathematics.double2 double2(System.Int32 v) { }
        // RVA: 0x04DAEBF0  token: 0x6000084
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DAEB90  token: 0x6000085
        public static Unity.Mathematics.double2 double2(System.UInt32 v) { }
        // RVA: 0x04DAEC20  token: 0x6000086
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.uint2 v) { }
        // RVA: 0x0B24E6FC  token: 0x6000087
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.half v) { }
        // RVA: 0x0B24E754  token: 0x6000088
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.half2 v) { }
        // RVA: 0x04DAEC70  token: 0x6000089
        public static Unity.Mathematics.double2 double2(System.Single v) { }
        // RVA: 0x04DAEBC0  token: 0x600008A
        public static Unity.Mathematics.double2 double2(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B254EB0  token: 0x600008B
        public static System.UInt32 hash(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B258E24  token: 0x600008C
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B272110  token: 0x600008D
        public static System.Double shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B271B08  token: 0x600008E
        public static Unity.Mathematics.double2 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B272044  token: 0x600008F
        public static Unity.Mathematics.double3 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B272210  token: 0x6000090
        public static Unity.Mathematics.double4 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F32AC0  token: 0x6000091
        private static System.Double select_shuffle_component(Unity.Mathematics.double2 a, Unity.Mathematics.double2 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAECC0  token: 0x6000092
        public static Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1) { }
        // RVA: 0x05F0AEA4  token: 0x6000093
        public static Unity.Mathematics.double2x2 double2x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11) { }
        // RVA: 0x0B24E7CC  token: 0x6000094
        public static Unity.Mathematics.double2x2 double2x2(System.Double v) { }
        // RVA: 0x0B24E8BC  token: 0x6000095
        public static Unity.Mathematics.double2x2 double2x2(System.Boolean v) { }
        // RVA: 0x0B24E7E8  token: 0x6000096
        public static Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B24E84C  token: 0x6000097
        public static Unity.Mathematics.double2x2 double2x2(System.Int32 v) { }
        // RVA: 0x0B24E868  token: 0x6000098
        public static Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x0B24E808  token: 0x6000099
        public static Unity.Mathematics.double2x2 double2x2(System.UInt32 v) { }
        // RVA: 0x0B24E890  token: 0x600009A
        public static Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x0B24E7B0  token: 0x600009B
        public static Unity.Mathematics.double2x2 double2x2(System.Single v) { }
        // RVA: 0x0B24E824  token: 0x600009C
        public static Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B27472C  token: 0x600009D
        public static Unity.Mathematics.double2x2 transpose(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B25B350  token: 0x600009E
        public static Unity.Mathematics.double2x2 inverse(Unity.Mathematics.double2x2 m) { }
        // RVA: 0x04DAE9C0  token: 0x600009F
        public static System.Double determinant(Unity.Mathematics.double2x2 m) { }
        // RVA: 0x0B254EF8  token: 0x60000A0
        public static System.UInt32 hash(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B259CE4  token: 0x60000A1
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x04DAECF0  token: 0x60000A2
        public static Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2) { }
        // RVA: 0x05F0AECC  token: 0x60000A3
        public static Unity.Mathematics.double2x3 double2x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12) { }
        // RVA: 0x0B24E918  token: 0x60000A4
        public static Unity.Mathematics.double2x3 double2x3(System.Double v) { }
        // RVA: 0x0B24E970  token: 0x60000A5
        public static Unity.Mathematics.double2x3 double2x3(System.Boolean v) { }
        // RVA: 0x0B24E9EC  token: 0x60000A6
        public static Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B24E990  token: 0x60000A7
        public static Unity.Mathematics.double2x3 double2x3(System.Int32 v) { }
        // RVA: 0x0B24EA28  token: 0x60000A8
        public static Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x0B24E8D8  token: 0x60000A9
        public static Unity.Mathematics.double2x3 double2x3(System.UInt32 v) { }
        // RVA: 0x0B24E9B0  token: 0x60000AA
        public static Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x0B24E8F8  token: 0x60000AB
        public static Unity.Mathematics.double2x3 double2x3(System.Single v) { }
        // RVA: 0x0B24E938  token: 0x60000AC
        public static Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B2750A4  token: 0x60000AD
        public static Unity.Mathematics.double3x2 transpose(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B254174  token: 0x60000AE
        public static System.UInt32 hash(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B259AF0  token: 0x60000AF
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x04DAED40  token: 0x60000B0
        public static Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2, Unity.Mathematics.double2 c3) { }
        // RVA: 0x05F0AFB4  token: 0x60000B1
        public static Unity.Mathematics.double2x4 double2x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13) { }
        // RVA: 0x0B24EB24  token: 0x60000B2
        public static Unity.Mathematics.double2x4 double2x4(System.Double v) { }
        // RVA: 0x0B24EA60  token: 0x60000B3
        public static Unity.Mathematics.double2x4 double2x4(System.Boolean v) { }
        // RVA: 0x0B24EAC0  token: 0x60000B4
        public static Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B24EBCC  token: 0x60000B5
        public static Unity.Mathematics.double2x4 double2x4(System.Int32 v) { }
        // RVA: 0x0B24EAE8  token: 0x60000B6
        public static Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x0B24EB6C  token: 0x60000B7
        public static Unity.Mathematics.double2x4 double2x4(System.UInt32 v) { }
        // RVA: 0x0B24EA84  token: 0x60000B8
        public static Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x0B24EB48  token: 0x60000B9
        public static Unity.Mathematics.double2x4 double2x4(System.Single v) { }
        // RVA: 0x0B24EB90  token: 0x60000BA
        public static Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B2744E0  token: 0x60000BB
        public static Unity.Mathematics.double4x2 transpose(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B2547F8  token: 0x60000BC
        public static System.UInt32 hash(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B25A27C  token: 0x60000BD
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x04DAEE50  token: 0x60000BE
        public static Unity.Mathematics.double3 double3(System.Double x, System.Double y, System.Double z) { }
        // RVA: 0x04DAEF60  token: 0x60000BF
        public static Unity.Mathematics.double3 double3(System.Double x, Unity.Mathematics.double2 yz) { }
        // RVA: 0x04DAEE80  token: 0x60000C0
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.double2 xy, System.Double z) { }
        // RVA: 0x04DAEF00  token: 0x60000C1
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.double3 xyz) { }
        // RVA: 0x04DAEE70  token: 0x60000C2
        public static Unity.Mathematics.double3 double3(System.Double v) { }
        // RVA: 0x0B24EC88  token: 0x60000C3
        public static Unity.Mathematics.double3 double3(System.Boolean v) { }
        // RVA: 0x0B24EC54  token: 0x60000C4
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DAEF30  token: 0x60000C5
        public static Unity.Mathematics.double3 double3(System.Int32 v) { }
        // RVA: 0x04DAEDD0  token: 0x60000C6
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DAEE20  token: 0x60000C7
        public static Unity.Mathematics.double3 double3(System.UInt32 v) { }
        // RVA: 0x04DAEEA0  token: 0x60000C8
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.uint3 v) { }
        // RVA: 0x0B24ECA8  token: 0x60000C9
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.half v) { }
        // RVA: 0x0B24EBF0  token: 0x60000CA
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DAEDA0  token: 0x60000CB
        public static Unity.Mathematics.double3 double3(System.Single v) { }
        // RVA: 0x0415F9A0  token: 0x60000CC
        public static Unity.Mathematics.double3 double3(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B255238  token: 0x60000CD
        public static System.UInt32 hash(Unity.Mathematics.double3 v) { }
        // RVA: 0x0B257F04  token: 0x60000CE
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3 v) { }
        // RVA: 0x0B2723BC  token: 0x60000CF
        public static System.Double shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B2717F4  token: 0x60000D0
        public static Unity.Mathematics.double2 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B27245C  token: 0x60000D1
        public static Unity.Mathematics.double3 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B27199C  token: 0x60000D2
        public static Unity.Mathematics.double4 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x0B271410  token: 0x60000D3
        private static System.Double select_shuffle_component(Unity.Mathematics.double3 a, Unity.Mathematics.double3 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAEF80  token: 0x60000D4
        public static Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1) { }
        // RVA: 0x05F0B0C0  token: 0x60000D5
        public static Unity.Mathematics.double3x2 double3x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21) { }
        // RVA: 0x0B24ED58  token: 0x60000D6
        public static Unity.Mathematics.double3x2 double3x2(System.Double v) { }
        // RVA: 0x0B24EDB4  token: 0x60000D7
        public static Unity.Mathematics.double3x2 double3x2(System.Boolean v) { }
        // RVA: 0x0B24EDD8  token: 0x60000D8
        public static Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x0B24EE70  token: 0x60000D9
        public static Unity.Mathematics.double3x2 double3x2(System.Int32 v) { }
        // RVA: 0x0B24EE14  token: 0x60000DA
        public static Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x0B24ECFC  token: 0x60000DB
        public static Unity.Mathematics.double3x2 double3x2(System.UInt32 v) { }
        // RVA: 0x0B24ED78  token: 0x60000DC
        public static Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x0B24EE50  token: 0x60000DD
        public static Unity.Mathematics.double3x2 double3x2(System.Single v) { }
        // RVA: 0x0B24ED1C  token: 0x60000DE
        public static Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B274648  token: 0x60000DF
        public static Unity.Mathematics.double2x3 transpose(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B253690  token: 0x60000E0
        public static System.UInt32 hash(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B256B54  token: 0x60000E1
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x04DAEFB0  token: 0x60000E2
        public static Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2) { }
        // RVA: 0x05F0B1A0  token: 0x60000E3
        public static Unity.Mathematics.double3x3 double3x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22) { }
        // RVA: 0x0B24F030  token: 0x60000E4
        public static Unity.Mathematics.double3x3 double3x3(System.Double v) { }
        // RVA: 0x0B24EFB4  token: 0x60000E5
        public static Unity.Mathematics.double3x3 double3x3(System.Boolean v) { }
        // RVA: 0x05F0B108  token: 0x60000E6
        public static Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x0B24EF88  token: 0x60000E7
        public static Unity.Mathematics.double3x3 double3x3(System.Int32 v) { }
        // RVA: 0x0B24EF38  token: 0x60000E8
        public static Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x0B24EF0C  token: 0x60000E9
        public static Unity.Mathematics.double3x3 double3x3(System.UInt32 v) { }
        // RVA: 0x0B24EFE0  token: 0x60000EA
        public static Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x0B24EE90  token: 0x60000EB
        public static Unity.Mathematics.double3x3 double3x3(System.Single v) { }
        // RVA: 0x0B24EEBC  token: 0x60000EC
        public static Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B2747F4  token: 0x60000ED
        public static Unity.Mathematics.double3x3 transpose(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B25C6E4  token: 0x60000EE
        public static Unity.Mathematics.double3x3 inverse(Unity.Mathematics.double3x3 m) { }
        // RVA: 0x04DAE320  token: 0x60000EF
        public static System.Double determinant(Unity.Mathematics.double3x3 m) { }
        // RVA: 0x0B254B94  token: 0x60000F0
        public static System.UInt32 hash(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B259634  token: 0x60000F1
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x04DAEFF0  token: 0x60000F2
        public static Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2, Unity.Mathematics.double3 c3) { }
        // RVA: 0x05F0B338  token: 0x60000F3
        public static Unity.Mathematics.double3x4 double3x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23) { }
        // RVA: 0x0B24F05C  token: 0x60000F4
        public static Unity.Mathematics.double3x4 double3x4(System.Double v) { }
        // RVA: 0x0B24F1FC  token: 0x60000F5
        public static Unity.Mathematics.double3x4 double3x4(System.Boolean v) { }
        // RVA: 0x05F0B560  token: 0x60000F6
        public static Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x0B24F088  token: 0x60000F7
        public static Unity.Mathematics.double3x4 double3x4(System.Int32 v) { }
        // RVA: 0x0B24F104  token: 0x60000F8
        public static Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x0B24F180  token: 0x60000F9
        public static Unity.Mathematics.double3x4 double3x4(System.UInt32 v) { }
        // RVA: 0x0B24F1AC  token: 0x60000FA
        public static Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x0B24F154  token: 0x60000FB
        public static Unity.Mathematics.double3x4 double3x4(System.Single v) { }
        // RVA: 0x0B24F0B4  token: 0x60000FC
        public static Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B274F08  token: 0x60000FD
        public static Unity.Mathematics.double4x3 transpose(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B2516B0  token: 0x60000FE
        public static Unity.Mathematics.double3x4 fastinverse(Unity.Mathematics.double3x4 m) { }
        // RVA: 0x0B2537BC  token: 0x60000FF
        public static System.UInt32 hash(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B258E7C  token: 0x6000100
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x04DAF120  token: 0x6000101
        public static Unity.Mathematics.double4 double4(System.Double x, System.Double y, System.Double z, System.Double w) { }
        // RVA: 0x04DAF040  token: 0x6000102
        public static Unity.Mathematics.double4 double4(System.Double x, System.Double y, Unity.Mathematics.double2 zw) { }
        // RVA: 0x04DAF060  token: 0x6000103
        public static Unity.Mathematics.double4 double4(System.Double x, Unity.Mathematics.double2 yz, System.Double w) { }
        // RVA: 0x04DAF170  token: 0x6000104
        public static Unity.Mathematics.double4 double4(System.Double x, Unity.Mathematics.double3 yzw) { }
        // RVA: 0x04DAF1D0  token: 0x6000105
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.double2 xy, System.Double z, System.Double w) { }
        // RVA: 0x04DAF140  token: 0x6000106
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.double2 xy, Unity.Mathematics.double2 zw) { }
        // RVA: 0x04DAF0B0  token: 0x6000107
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.double3 xyz, System.Double w) { }
        // RVA: 0x0B24F26C  token: 0x6000108
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.double4 xyzw) { }
        // RVA: 0x04DAF080  token: 0x6000109
        public static Unity.Mathematics.double4 double4(System.Double v) { }
        // RVA: 0x0B24F2E0  token: 0x600010A
        public static Unity.Mathematics.double4 double4(System.Boolean v) { }
        // RVA: 0x0B24F250  token: 0x600010B
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DAF0E0  token: 0x600010C
        public static Unity.Mathematics.double4 double4(System.Int32 v) { }
        // RVA: 0x0B24F2FC  token: 0x600010D
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DAF1A0  token: 0x600010E
        public static Unity.Mathematics.double4 double4(System.UInt32 v) { }
        // RVA: 0x0B24F324  token: 0x600010F
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B24F294  token: 0x6000110
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.half v) { }
        // RVA: 0x0B24F22C  token: 0x6000111
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DAF090  token: 0x6000112
        public static Unity.Mathematics.double4 double4(System.Single v) { }
        // RVA: 0x0B24F2B8  token: 0x6000113
        public static Unity.Mathematics.double4 double4(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B25402C  token: 0x6000114
        public static System.UInt32 hash(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B2575B0  token: 0x6000115
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B272308  token: 0x6000116
        public static System.Double shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B271B74  token: 0x6000117
        public static Unity.Mathematics.double2 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B271C14  token: 0x6000118
        public static Unity.Mathematics.double3 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F33BD8  token: 0x6000119
        public static Unity.Mathematics.double4 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x0B2712DC  token: 0x600011A
        private static System.Double select_shuffle_component(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAF1F0  token: 0x600011B
        public static Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1) { }
        // RVA: 0x05F0B68C  token: 0x600011C
        public static Unity.Mathematics.double4x2 double4x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21, System.Double m30, System.Double m31) { }
        // RVA: 0x0B24F3AC  token: 0x600011D
        public static Unity.Mathematics.double4x2 double4x2(System.Double v) { }
        // RVA: 0x0B24F3F4  token: 0x600011E
        public static Unity.Mathematics.double4x2 double4x2(System.Boolean v) { }
        // RVA: 0x0B24F47C  token: 0x600011F
        public static Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x0B24F3D0  token: 0x6000120
        public static Unity.Mathematics.double4x2 double4x2(System.Int32 v) { }
        // RVA: 0x0B24F370  token: 0x6000121
        public static Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x0B24F418  token: 0x6000122
        public static Unity.Mathematics.double4x2 double4x2(System.UInt32 v) { }
        // RVA: 0x0B24F4A4  token: 0x6000123
        public static Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x0B24F34C  token: 0x6000124
        public static Unity.Mathematics.double4x2 double4x2(System.Single v) { }
        // RVA: 0x0B24F440  token: 0x6000125
        public static Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B27469C  token: 0x6000126
        public static Unity.Mathematics.double2x4 transpose(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B25576C  token: 0x6000127
        public static System.UInt32 hash(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B257FA8  token: 0x6000128
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x04DAF220  token: 0x6000129
        public static Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2) { }
        // RVA: 0x05F0B7D0  token: 0x600012A
        public static Unity.Mathematics.double4x3 double4x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22, System.Double m30, System.Double m31, System.Double m32) { }
        // RVA: 0x0B24F530  token: 0x600012B
        public static Unity.Mathematics.double4x3 double4x3(System.Double v) { }
        // RVA: 0x0B24F608  token: 0x600012C
        public static Unity.Mathematics.double4x3 double4x3(System.Boolean v) { }
        // RVA: 0x05F0B788  token: 0x600012D
        public static Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x0B24F55C  token: 0x600012E
        public static Unity.Mathematics.double4x3 double4x3(System.Int32 v) { }
        // RVA: 0x0B24F5B8  token: 0x600012F
        public static Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x0B24F588  token: 0x6000130
        public static Unity.Mathematics.double4x3 double4x3(System.UInt32 v) { }
        // RVA: 0x0B24F4E0  token: 0x6000131
        public static Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x0B24F634  token: 0x6000132
        public static Unity.Mathematics.double4x3 double4x3(System.Single v) { }
        // RVA: 0x0B24F660  token: 0x6000133
        public static Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B274C64  token: 0x6000134
        public static Unity.Mathematics.double3x4 transpose(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B255578  token: 0x6000135
        public static System.UInt32 hash(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B257BCC  token: 0x6000136
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x04DAF260  token: 0x6000137
        public static Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2, Unity.Mathematics.double4 c3) { }
        // RVA: 0x05F0BAE8  token: 0x6000138
        public static Unity.Mathematics.double4x4 double4x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23, System.Double m30, System.Double m31, System.Double m32, System.Double m33) { }
        // RVA: 0x0B24F6E8  token: 0x6000139
        public static Unity.Mathematics.double4x4 double4x4(System.Double v) { }
        // RVA: 0x0B24F71C  token: 0x600013A
        public static Unity.Mathematics.double4x4 double4x4(System.Boolean v) { }
        // RVA: 0x0B24F898  token: 0x600013B
        public static Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x0B24F8DC  token: 0x600013C
        public static Unity.Mathematics.double4x4 double4x4(System.Int32 v) { }
        // RVA: 0x0B24F750  token: 0x600013D
        public static Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x0B24F6B0  token: 0x600013E
        public static Unity.Mathematics.double4x4 double4x4(System.UInt32 v) { }
        // RVA: 0x0B24F7AC  token: 0x600013F
        public static Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x0B24F864  token: 0x6000140
        public static Unity.Mathematics.double4x4 double4x4(System.Single v) { }
        // RVA: 0x0B24F808  token: 0x6000141
        public static Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B270628  token: 0x6000142
        public static Unity.Mathematics.double3 rotate(Unity.Mathematics.double4x4 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x0B273F90  token: 0x6000143
        public static Unity.Mathematics.double3 transform(Unity.Mathematics.double4x4 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x0B274940  token: 0x6000144
        public static Unity.Mathematics.double4x4 transpose(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B25B7D0  token: 0x6000145
        public static Unity.Mathematics.double4x4 inverse(Unity.Mathematics.double4x4 m) { }
        // RVA: 0x0B251030  token: 0x6000146
        public static Unity.Mathematics.double4x4 fastinverse(Unity.Mathematics.double4x4 m) { }
        // RVA: 0x04DAE0B0  token: 0x6000147
        public static System.Double determinant(Unity.Mathematics.double4x4 m) { }
        // RVA: 0x0B253E20  token: 0x6000148
        public static System.UInt32 hash(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B256C94  token: 0x6000149
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x04DAF4D0  token: 0x600014A
        public static Unity.Mathematics.float2 float2(System.Single x, System.Single y) { }
        // RVA: 0x04DA7CB0  token: 0x600014B
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.float2 xy) { }
        // RVA: 0x04D8B720  token: 0x600014C
        public static Unity.Mathematics.float2 float2(System.Single v) { }
        // RVA: 0x0B2519E8  token: 0x600014D
        public static Unity.Mathematics.float2 float2(System.Boolean v) { }
        // RVA: 0x0B251A08  token: 0x600014E
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DAF4E0  token: 0x600014F
        public static Unity.Mathematics.float2 float2(System.Int32 v) { }
        // RVA: 0x04DAF500  token: 0x6000150
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DAF550  token: 0x6000151
        public static Unity.Mathematics.float2 float2(System.UInt32 v) { }
        // RVA: 0x04DAF520  token: 0x6000152
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.uint2 v) { }
        // RVA: 0x0B251A58  token: 0x6000153
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.half v) { }
        // RVA: 0x0B251A2C  token: 0x6000154
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.half2 v) { }
        // RVA: 0x04DAF580  token: 0x6000155
        public static Unity.Mathematics.float2 float2(System.Double v) { }
        // RVA: 0x04DAF4B0  token: 0x6000156
        public static Unity.Mathematics.float2 float2(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B253658  token: 0x6000157
        public static System.UInt32 hash(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B259CA0  token: 0x6000158
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2 v) { }
        // RVA: 0x05F33FD4  token: 0x6000159
        public static System.Single shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F3477C  token: 0x600015A
        public static Unity.Mathematics.float2 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F34970  token: 0x600015B
        public static Unity.Mathematics.float3 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F341B8  token: 0x600015C
        public static Unity.Mathematics.float4 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F3322C  token: 0x600015D
        private static System.Single select_shuffle_component(Unity.Mathematics.float2 a, Unity.Mathematics.float2 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x02E515B0  token: 0x600015E
        public static Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1) { }
        // RVA: 0x0B251A98  token: 0x600015F
        public static Unity.Mathematics.float2x2 float2x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11) { }
        // RVA: 0x0B251A80  token: 0x6000160
        public static Unity.Mathematics.float2x2 float2x2(System.Single v) { }
        // RVA: 0x0B251B80  token: 0x6000161
        public static Unity.Mathematics.float2x2 float2x2(System.Boolean v) { }
        // RVA: 0x0B251B98  token: 0x6000162
        public static Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B251AE4  token: 0x6000163
        public static Unity.Mathematics.float2x2 float2x2(System.Int32 v) { }
        // RVA: 0x0B251B5C  token: 0x6000164
        public static Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x0B251B2C  token: 0x6000165
        public static Unity.Mathematics.float2x2 float2x2(System.UInt32 v) { }
        // RVA: 0x0B251ABC  token: 0x6000166
        public static Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x0B251B44  token: 0x6000167
        public static Unity.Mathematics.float2x2 float2x2(System.Double v) { }
        // RVA: 0x0B251AFC  token: 0x6000168
        public static Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B2742F4  token: 0x6000169
        public static Unity.Mathematics.float2x2 transpose(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x05F16C64  token: 0x600016A
        public static Unity.Mathematics.float2x2 inverse(Unity.Mathematics.float2x2 m) { }
        // RVA: 0x04DAE9F0  token: 0x600016B
        public static System.Single determinant(Unity.Mathematics.float2x2 m) { }
        // RVA: 0x0B255CA8  token: 0x600016C
        public static System.UInt32 hash(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B25A05C  token: 0x600016D
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x04DAF5A0  token: 0x600016E
        public static Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2) { }
        // RVA: 0x05F0D588  token: 0x600016F
        public static Unity.Mathematics.float2x3 float2x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12) { }
        // RVA: 0x0B251BB4  token: 0x6000170
        public static Unity.Mathematics.float2x3 float2x3(System.Single v) { }
        // RVA: 0x0B251C14  token: 0x6000171
        public static Unity.Mathematics.float2x3 float2x3(System.Boolean v) { }
        // RVA: 0x0B251C70  token: 0x6000172
        public static Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B251CDC  token: 0x6000173
        public static Unity.Mathematics.float2x3 float2x3(System.Int32 v) { }
        // RVA: 0x0B251CFC  token: 0x6000174
        public static Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x0B251BF4  token: 0x6000175
        public static Unity.Mathematics.float2x3 float2x3(System.UInt32 v) { }
        // RVA: 0x0B251CA4  token: 0x6000176
        public static Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x0B251BD4  token: 0x6000177
        public static Unity.Mathematics.float2x3 float2x3(System.Double v) { }
        // RVA: 0x0B251C34  token: 0x6000178
        public static Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B274A98  token: 0x6000179
        public static Unity.Mathematics.float3x2 transpose(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B25440C  token: 0x600017A
        public static System.UInt32 hash(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B257364  token: 0x600017B
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x04DAF5B0  token: 0x600017C
        public static Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2, Unity.Mathematics.float2 c3) { }
        // RVA: 0x05F0D5CC  token: 0x600017D
        public static Unity.Mathematics.float2x4 float2x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13) { }
        // RVA: 0x0B251DEC  token: 0x600017E
        public static Unity.Mathematics.float2x4 float2x4(System.Single v) { }
        // RVA: 0x0B251D34  token: 0x600017F
        public static Unity.Mathematics.float2x4 float2x4(System.Boolean v) { }
        // RVA: 0x0B251D94  token: 0x6000180
        public static Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B251DD0  token: 0x6000181
        public static Unity.Mathematics.float2x4 float2x4(System.Int32 v) { }
        // RVA: 0x0B251E58  token: 0x6000182
        public static Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x0B251DB4  token: 0x6000183
        public static Unity.Mathematics.float2x4 float2x4(System.UInt32 v) { }
        // RVA: 0x0B251E24  token: 0x6000184
        public static Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x0B251E08  token: 0x6000185
        public static Unity.Mathematics.float2x4 float2x4(System.Double v) { }
        // RVA: 0x0B251D50  token: 0x6000186
        public static Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B274868  token: 0x6000187
        public static Unity.Mathematics.float4x2 transpose(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B2550A4  token: 0x6000188
        public static System.UInt32 hash(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B2588B8  token: 0x6000189
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x04DA2820  token: 0x600018A
        public static Unity.Mathematics.float3 float3(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x04DAF730  token: 0x600018B
        public static Unity.Mathematics.float3 float3(System.Single x, Unity.Mathematics.float2 yz) { }
        // RVA: 0x04DAF670  token: 0x600018C
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.float2 xy, System.Single z) { }
        // RVA: 0x04DAF6A0  token: 0x600018D
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x04D8B860  token: 0x600018E
        public static Unity.Mathematics.float3 float3(System.Single v) { }
        // RVA: 0x0B251E8C  token: 0x600018F
        public static Unity.Mathematics.float3 float3(System.Boolean v) { }
        // RVA: 0x0B251F30  token: 0x6000190
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DAF610  token: 0x6000191
        public static Unity.Mathematics.float3 float3(System.Int32 v) { }
        // RVA: 0x04DAF7A0  token: 0x6000192
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DAF760  token: 0x6000193
        public static Unity.Mathematics.float3 float3(System.UInt32 v) { }
        // RVA: 0x04DAF6D0  token: 0x6000194
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.uint3 v) { }
        // RVA: 0x0B251EA8  token: 0x6000195
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.half v) { }
        // RVA: 0x0B251EE4  token: 0x6000196
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DAF640  token: 0x6000197
        public static Unity.Mathematics.float3 float3(System.Double v) { }
        // RVA: 0x04DAF5D0  token: 0x6000198
        public static Unity.Mathematics.float3 float3(Unity.Mathematics.double3 v) { }
        // RVA: 0x05F10F48  token: 0x6000199
        public static System.UInt32 hash(Unity.Mathematics.float3 v) { }
        // RVA: 0x05F13994  token: 0x600019A
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3 v) { }
        // RVA: 0x05F34A30  token: 0x600019B
        public static System.Single shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F3511C  token: 0x600019C
        public static Unity.Mathematics.float2 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F354C8  token: 0x600019D
        public static Unity.Mathematics.float3 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F352B0  token: 0x600019E
        public static Unity.Mathematics.float4 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F329B0  token: 0x600019F
        private static System.Single select_shuffle_component(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAF7F0  token: 0x60001A0
        public static Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1) { }
        // RVA: 0x05F0D638  token: 0x60001A1
        public static Unity.Mathematics.float3x2 float3x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21) { }
        // RVA: 0x0B2520C8  token: 0x60001A2
        public static Unity.Mathematics.float3x2 float3x2(System.Single v) { }
        // RVA: 0x0B251FD8  token: 0x60001A3
        public static Unity.Mathematics.float3x2 float3x2(System.Boolean v) { }
        // RVA: 0x0B252090  token: 0x60001A4
        public static Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x0B251FF8  token: 0x60001A5
        public static Unity.Mathematics.float3x2 float3x2(System.Int32 v) { }
        // RVA: 0x0B252038  token: 0x60001A6
        public static Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x0B252070  token: 0x60001A7
        public static Unity.Mathematics.float3x2 float3x2(System.UInt32 v) { }
        // RVA: 0x0B251FA0  token: 0x60001A8
        public static Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x0B252018  token: 0x60001A9
        public static Unity.Mathematics.float3x2 float3x2(System.Double v) { }
        // RVA: 0x0B251F60  token: 0x60001AA
        public static Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B274B9C  token: 0x60001AB
        public static Unity.Mathematics.float2x3 transpose(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B25649C  token: 0x60001AC
        public static System.UInt32 hash(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B2577B0  token: 0x60001AD
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x034C99D0  token: 0x60001AE
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2) { }
        // RVA: 0x05F0D728  token: 0x60001AF
        public static Unity.Mathematics.float3x3 float3x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22) { }
        // RVA: 0x0B252108  token: 0x60001B0
        public static Unity.Mathematics.float3x3 float3x3(System.Single v) { }
        // RVA: 0x0B2520E8  token: 0x60001B1
        public static Unity.Mathematics.float3x3 float3x3(System.Boolean v) { }
        // RVA: 0x0B2522EC  token: 0x60001B2
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x0B252214  token: 0x60001B3
        public static Unity.Mathematics.float3x3 float3x3(System.Int32 v) { }
        // RVA: 0x0B252180  token: 0x60001B4
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x0B2522AC  token: 0x60001B5
        public static Unity.Mathematics.float3x3 float3x3(System.UInt32 v) { }
        // RVA: 0x0B25226C  token: 0x60001B6
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x0B2522CC  token: 0x60001B7
        public static Unity.Mathematics.float3x3 float3x3(System.Double v) { }
        // RVA: 0x0B252128  token: 0x60001B8
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B274E84  token: 0x60001B9
        public static Unity.Mathematics.float3x3 transpose(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B25B410  token: 0x60001BA
        public static Unity.Mathematics.float3x3 inverse(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x04DAE870  token: 0x60001BB
        public static System.Single determinant(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x0B25619C  token: 0x60001BC
        public static System.UInt32 hash(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B257644  token: 0x60001BD
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x04DAF820  token: 0x60001BE
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3 c3) { }
        // RVA: 0x05F0D8D8  token: 0x60001BF
        public static Unity.Mathematics.float3x4 float3x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23) { }
        // RVA: 0x0B2524A0  token: 0x60001C0
        public static Unity.Mathematics.float3x4 float3x4(System.Single v) { }
        // RVA: 0x0B252388  token: 0x60001C1
        public static Unity.Mathematics.float3x4 float3x4(System.Boolean v) { }
        // RVA: 0x0B25234C  token: 0x60001C2
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x0B25243C  token: 0x60001C3
        public static Unity.Mathematics.float3x4 float3x4(System.Int32 v) { }
        // RVA: 0x0B25245C  token: 0x60001C4
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x0B252564  token: 0x60001C5
        public static Unity.Mathematics.float3x4 float3x4(System.UInt32 v) { }
        // RVA: 0x0B252520  token: 0x60001C6
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x0B25232C  token: 0x60001C7
        public static Unity.Mathematics.float3x4 float3x4(System.Double v) { }
        // RVA: 0x0B2524C0  token: 0x60001C8
        public static Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B2749F4  token: 0x60001C9
        public static Unity.Mathematics.float4x3 transpose(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B2514A4  token: 0x60001CA
        public static Unity.Mathematics.float3x4 fastinverse(Unity.Mathematics.float3x4 m) { }
        // RVA: 0x0B253AC4  token: 0x60001CB
        public static System.UInt32 hash(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B256FE8  token: 0x60001CC
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x04DAF870  token: 0x60001CD
        public static Unity.Mathematics.float4 float4(System.Single x, System.Single y, System.Single z, System.Single w) { }
        // RVA: 0x04DAF8A0  token: 0x60001CE
        public static Unity.Mathematics.float4 float4(System.Single x, System.Single y, Unity.Mathematics.float2 zw) { }
        // RVA: 0x04DAF940  token: 0x60001CF
        public static Unity.Mathematics.float4 float4(System.Single x, Unity.Mathematics.float2 yz, System.Single w) { }
        // RVA: 0x04DAF9C0  token: 0x60001D0
        public static Unity.Mathematics.float4 float4(System.Single x, Unity.Mathematics.float3 yzw) { }
        // RVA: 0x04DAF8D0  token: 0x60001D1
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.float2 xy, System.Single z, System.Single w) { }
        // RVA: 0x04D85B30  token: 0x60001D2
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.float2 xy, Unity.Mathematics.float2 zw) { }
        // RVA: 0x030E7A30  token: 0x60001D3
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.float3 xyz, System.Single w) { }
        // RVA: 0x0B2526F4  token: 0x60001D4
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.float4 xyzw) { }
        // RVA: 0x04DAF890  token: 0x60001D5
        public static Unity.Mathematics.float4 float4(System.Single v) { }
        // RVA: 0x0B2526B8  token: 0x60001D6
        public static Unity.Mathematics.float4 float4(System.Boolean v) { }
        // RVA: 0x0B2525B4  token: 0x60001D7
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DAF920  token: 0x60001D8
        public static Unity.Mathematics.float4 float4(System.Int32 v) { }
        // RVA: 0x0B252674  token: 0x60001D9
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DAF970  token: 0x60001DA
        public static Unity.Mathematics.float4 float4(System.UInt32 v) { }
        // RVA: 0x0B2526D0  token: 0x60001DB
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B252628  token: 0x60001DC
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.half v) { }
        // RVA: 0x0B2525CC  token: 0x60001DD
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DAF900  token: 0x60001DE
        public static Unity.Mathematics.float4 float4(System.Double v) { }
        // RVA: 0x0B252584  token: 0x60001DF
        public static Unity.Mathematics.float4 float4(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B2545F0  token: 0x60001E0
        public static System.UInt32 hash(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B2574F4  token: 0x60001E1
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B271974  token: 0x60001E2
        public static System.Single shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B2723FC  token: 0x60001E3
        public static Unity.Mathematics.float2 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B271F84  token: 0x60001E4
        public static Unity.Mathematics.float3 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B271D10  token: 0x60001E5
        public static Unity.Mathematics.float4 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x0B271530  token: 0x60001E6
        private static System.Single select_shuffle_component(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAFA00  token: 0x60001E7
        public static Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1) { }
        // RVA: 0x05F0DA18  token: 0x60001E8
        public static Unity.Mathematics.float4x2 float4x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21, System.Single m30, System.Single m31) { }
        // RVA: 0x0B252770  token: 0x60001E9
        public static Unity.Mathematics.float4x2 float4x2(System.Single v) { }
        // RVA: 0x0B252840  token: 0x60001EA
        public static Unity.Mathematics.float4x2 float4x2(System.Boolean v) { }
        // RVA: 0x0B25285C  token: 0x60001EB
        public static Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x0B252804  token: 0x60001EC
        public static Unity.Mathematics.float4x2 float4x2(System.Int32 v) { }
        // RVA: 0x0B2527D0  token: 0x60001ED
        public static Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x0B252820  token: 0x60001EE
        public static Unity.Mathematics.float4x2 float4x2(System.UInt32 v) { }
        // RVA: 0x0B25273C  token: 0x60001EF
        public static Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x0B252720  token: 0x60001F0
        public static Unity.Mathematics.float4x2 float4x2(System.Double v) { }
        // RVA: 0x0B25278C  token: 0x60001F1
        public static Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B274BF8  token: 0x60001F2
        public static Unity.Mathematics.float2x4 transpose(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B254AC8  token: 0x60001F3
        public static System.UInt32 hash(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B259BC8  token: 0x60001F4
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x04DAFA20  token: 0x60001F5
        public static Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2) { }
        // RVA: 0x05F0DAE4  token: 0x60001F6
        public static Unity.Mathematics.float4x3 float4x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22, System.Single m30, System.Single m31, System.Single m32) { }
        // RVA: 0x0B2529A4  token: 0x60001F7
        public static Unity.Mathematics.float4x3 float4x3(System.Single v) { }
        // RVA: 0x0B2529C4  token: 0x60001F8
        public static Unity.Mathematics.float4x3 float4x3(System.Boolean v) { }
        // RVA: 0x0B252924  token: 0x60001F9
        public static Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x0B2529E4  token: 0x60001FA
        public static Unity.Mathematics.float4x3 float4x3(System.Int32 v) { }
        // RVA: 0x0B252A04  token: 0x60001FB
        public static Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x0B252900  token: 0x60001FC
        public static Unity.Mathematics.float4x3 float4x3(System.UInt32 v) { }
        // RVA: 0x0B252960  token: 0x60001FD
        public static Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x0B2528E0  token: 0x60001FE
        public static Unity.Mathematics.float4x3 float4x3(System.Double v) { }
        // RVA: 0x0B252880  token: 0x60001FF
        public static Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B2745A4  token: 0x6000200
        public static Unity.Mathematics.float3x4 transpose(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B255D0C  token: 0x6000201
        public static System.UInt32 hash(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B256EAC  token: 0x6000202
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x04DAFA40  token: 0x6000203
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2, Unity.Mathematics.float4 c3) { }
        // RVA: 0x0B252A44  token: 0x6000204
        public static Unity.Mathematics.float4x4 float4x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23, System.Single m30, System.Single m31, System.Single m32, System.Single m33) { }
        // RVA: 0x0308AEC0  token: 0x6000205
        public static Unity.Mathematics.float4x4 float4x4(System.Single v) { }
        // RVA: 0x0B252EC0  token: 0x6000206
        public static Unity.Mathematics.float4x4 float4x4(System.Boolean v) { }
        // RVA: 0x0B252F70  token: 0x6000207
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x0B252EE4  token: 0x6000208
        public static Unity.Mathematics.float4x4 float4x4(System.Int32 v) { }
        // RVA: 0x0B252B94  token: 0x6000209
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x0B252B6C  token: 0x600020A
        public static Unity.Mathematics.float4x4 float4x4(System.UInt32 v) { }
        // RVA: 0x0B252DF0  token: 0x600020B
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x0B252BF0  token: 0x600020C
        public static Unity.Mathematics.float4x4 float4x4(System.Double v) { }
        // RVA: 0x0B252C68  token: 0x600020D
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B270550  token: 0x600020E
        public static Unity.Mathematics.float3 rotate(Unity.Mathematics.float4x4 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x0B274114  token: 0x600020F
        public static Unity.Mathematics.float3 transform(Unity.Mathematics.float4x4 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x0B274DA8  token: 0x6000210
        public static Unity.Mathematics.float4x4 transpose(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x030E7AD0  token: 0x6000211
        public static Unity.Mathematics.float4x4 inverse(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B250D30  token: 0x6000212
        public static Unity.Mathematics.float4x4 fastinverse(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x04DAE3E0  token: 0x6000213
        public static System.Single determinant(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B253C90  token: 0x6000214
        public static System.UInt32 hash(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B25871C  token: 0x6000215
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x04D875C0  token: 0x6000216
        public static Unity.Mathematics.half half(Unity.Mathematics.half x) { }
        // RVA: 0x0B253650  token: 0x6000217
        public static Unity.Mathematics.half half(System.Single v) { }
        // RVA: 0x0B253644  token: 0x6000218
        public static Unity.Mathematics.half half(System.Double v) { }
        // RVA: 0x04DB0230  token: 0x6000219
        public static System.UInt32 hash(Unity.Mathematics.half v) { }
        // RVA: 0x04DAD5B0  token: 0x600021A
        public static Unity.Mathematics.half2 half2(Unity.Mathematics.half x, Unity.Mathematics.half y) { }
        // RVA: 0x04DAB3F0  token: 0x600021B
        public static Unity.Mathematics.half2 half2(Unity.Mathematics.half2 xy) { }
        // RVA: 0x04DB0040  token: 0x600021C
        public static Unity.Mathematics.half2 half2(Unity.Mathematics.half v) { }
        // RVA: 0x0B253388  token: 0x600021D
        public static Unity.Mathematics.half2 half2(System.Single v) { }
        // RVA: 0x0B2533B0  token: 0x600021E
        public static Unity.Mathematics.half2 half2(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B25331C  token: 0x600021F
        public static Unity.Mathematics.half2 half2(System.Double v) { }
        // RVA: 0x0B253350  token: 0x6000220
        public static Unity.Mathematics.half2 half2(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B2562F0  token: 0x6000221
        public static System.UInt32 hash(Unity.Mathematics.half2 v) { }
        // RVA: 0x0B259274  token: 0x6000222
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.half2 v) { }
        // RVA: 0x04DAD5C0  token: 0x6000223
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z) { }
        // RVA: 0x04DB0090  token: 0x6000224
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.half x, Unity.Mathematics.half2 yz) { }
        // RVA: 0x04DB00B0  token: 0x6000225
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.half2 xy, Unity.Mathematics.half z) { }
        // RVA: 0x04DB0050  token: 0x6000226
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.half3 xyz) { }
        // RVA: 0x04DB0080  token: 0x6000227
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.half v) { }
        // RVA: 0x0B2534B8  token: 0x6000228
        public static Unity.Mathematics.half3 half3(System.Single v) { }
        // RVA: 0x0B253474  token: 0x6000229
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B2533E4  token: 0x600022A
        public static Unity.Mathematics.half3 half3(System.Double v) { }
        // RVA: 0x0B253428  token: 0x600022B
        public static Unity.Mathematics.half3 half3(Unity.Mathematics.double3 v) { }
        // RVA: 0x05F0E860  token: 0x600022C
        public static System.UInt32 hash(Unity.Mathematics.half3 v) { }
        // RVA: 0x05F14938  token: 0x600022D
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DAD5E0  token: 0x600022E
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z, Unity.Mathematics.half w) { }
        // RVA: 0x04DB01A0  token: 0x600022F
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half2 zw) { }
        // RVA: 0x04DB0120  token: 0x6000230
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half2 yz, Unity.Mathematics.half w) { }
        // RVA: 0x04DB0200  token: 0x6000231
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half3 yzw) { }
        // RVA: 0x04DB00D0  token: 0x6000232
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half2 xy, Unity.Mathematics.half z, Unity.Mathematics.half w) { }
        // RVA: 0x04DB0150  token: 0x6000233
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half2 xy, Unity.Mathematics.half2 zw) { }
        // RVA: 0x04DB0170  token: 0x6000234
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half3 xyz, Unity.Mathematics.half w) { }
        // RVA: 0x04DB01D0  token: 0x6000235
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half4 xyzw) { }
        // RVA: 0x04DB0100  token: 0x6000236
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.half v) { }
        // RVA: 0x0B253554  token: 0x6000237
        public static Unity.Mathematics.half4 half4(System.Single v) { }
        // RVA: 0x0B2535F0  token: 0x6000238
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B253598  token: 0x6000239
        public static Unity.Mathematics.half4 half4(System.Double v) { }
        // RVA: 0x0B2534F0  token: 0x600023A
        public static Unity.Mathematics.half4 half4(Unity.Mathematics.double4 v) { }
        // RVA: 0x05F0E7C0  token: 0x600023B
        public static System.UInt32 hash(Unity.Mathematics.half4 v) { }
        // RVA: 0x05F160D0  token: 0x600023C
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DAD8A0  token: 0x600023D
        public static Unity.Mathematics.int2 int2(System.Int32 x, System.Int32 y) { }
        // RVA: 0x04DAB5F0  token: 0x600023E
        public static Unity.Mathematics.int2 int2(Unity.Mathematics.int2 xy) { }
        // RVA: 0x04DB02A0  token: 0x600023F
        public static Unity.Mathematics.int2 int2(System.Int32 v) { }
        // RVA: 0x0B25A394  token: 0x6000240
        public static Unity.Mathematics.int2 int2(System.Boolean v) { }
        // RVA: 0x0B25A3AC  token: 0x6000241
        public static Unity.Mathematics.int2 int2(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB02A0  token: 0x6000242
        public static Unity.Mathematics.int2 int2(System.UInt32 v) { }
        // RVA: 0x04DAB5F0  token: 0x6000243
        public static Unity.Mathematics.int2 int2(Unity.Mathematics.uint2 v) { }
        // RVA: 0x04DB0280  token: 0x6000244
        public static Unity.Mathematics.int2 int2(System.Single v) { }
        // RVA: 0x04DB0240  token: 0x6000245
        public static Unity.Mathematics.int2 int2(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DB0260  token: 0x6000246
        public static Unity.Mathematics.int2 int2(System.Double v) { }
        // RVA: 0x04DB02B0  token: 0x6000247
        public static Unity.Mathematics.int2 int2(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B254D4C  token: 0x6000248
        public static System.UInt32 hash(Unity.Mathematics.int2 v) { }
        // RVA: 0x0B259434  token: 0x6000249
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2 v) { }
        // RVA: 0x05F34A6C  token: 0x600024A
        public static System.Int32 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F34E64  token: 0x600024B
        public static Unity.Mathematics.int2 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F34900  token: 0x600024C
        public static Unity.Mathematics.int3 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F33F44  token: 0x600024D
        public static Unity.Mathematics.int4 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F3332C  token: 0x600024E
        private static System.Int32 select_shuffle_component(Unity.Mathematics.int2 a, Unity.Mathematics.int2 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x02E515B0  token: 0x600024F
        public static Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1) { }
        // RVA: 0x0B25A428  token: 0x6000250
        public static Unity.Mathematics.int2x2 int2x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11) { }
        // RVA: 0x0B25A410  token: 0x6000251
        public static Unity.Mathematics.int2x2 int2x2(System.Int32 v) { }
        // RVA: 0x0B25A4CC  token: 0x6000252
        public static Unity.Mathematics.int2x2 int2x2(System.Boolean v) { }
        // RVA: 0x0B25A3CC  token: 0x6000253
        public static Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B25A410  token: 0x6000254
        public static Unity.Mathematics.int2x2 int2x2(System.UInt32 v) { }
        // RVA: 0x0B25A490  token: 0x6000255
        public static Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x0B25A478  token: 0x6000256
        public static Unity.Mathematics.int2x2 int2x2(System.Single v) { }
        // RVA: 0x0B25A3EC  token: 0x6000257
        public static Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B25A4B4  token: 0x6000258
        public static Unity.Mathematics.int2x2 int2x2(System.Double v) { }
        // RVA: 0x0B25A448  token: 0x6000259
        public static Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B274334  token: 0x600025A
        public static Unity.Mathematics.int2x2 transpose(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x04DAE310  token: 0x600025B
        public static System.Int32 determinant(Unity.Mathematics.int2x2 m) { }
        // RVA: 0x0B255708  token: 0x600025C
        public static System.UInt32 hash(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x0B2582B8  token: 0x600025D
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x04DAF5A0  token: 0x600025E
        public static Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2) { }
        // RVA: 0x05F164A8  token: 0x600025F
        public static Unity.Mathematics.int2x3 int2x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12) { }
        // RVA: 0x0B25A53C  token: 0x6000260
        public static Unity.Mathematics.int2x3 int2x3(System.Int32 v) { }
        // RVA: 0x0B25A5F0  token: 0x6000261
        public static Unity.Mathematics.int2x3 int2x3(System.Boolean v) { }
        // RVA: 0x0B25A57C  token: 0x6000262
        public static Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B25A53C  token: 0x6000263
        public static Unity.Mathematics.int2x3 int2x3(System.UInt32 v) { }
        // RVA: 0x0B25A504  token: 0x6000264
        public static Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x0B25A4E4  token: 0x6000265
        public static Unity.Mathematics.int2x3 int2x3(System.Single v) { }
        // RVA: 0x0B25A610  token: 0x6000266
        public static Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B25A55C  token: 0x6000267
        public static Unity.Mathematics.int2x3 int2x3(System.Double v) { }
        // RVA: 0x0B25A5B4  token: 0x6000268
        public static Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B274F98  token: 0x6000269
        public static Unity.Mathematics.int3x2 transpose(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x0B254F84  token: 0x600026A
        public static System.UInt32 hash(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x0B2591D4  token: 0x600026B
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x04DB02D0  token: 0x600026C
        public static Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2, Unity.Mathematics.int2 c3) { }
        // RVA: 0x05F164E0  token: 0x600026D
        public static Unity.Mathematics.int2x4 int2x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13) { }
        // RVA: 0x0B25A66C  token: 0x600026E
        public static Unity.Mathematics.int2x4 int2x4(System.Int32 v) { }
        // RVA: 0x0B25A6BC  token: 0x600026F
        public static Unity.Mathematics.int2x4 int2x4(System.Boolean v) { }
        // RVA: 0x0B25A648  token: 0x6000270
        public static Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B25A66C  token: 0x6000271
        public static Unity.Mathematics.int2x4 int2x4(System.UInt32 v) { }
        // RVA: 0x0B25A688  token: 0x6000272
        public static Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x0B25A728  token: 0x6000273
        public static Unity.Mathematics.int2x4 int2x4(System.Single v) { }
        // RVA: 0x0B25A6F4  token: 0x6000274
        public static Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B25A6D8  token: 0x6000275
        public static Unity.Mathematics.int2x4 int2x4(System.Double v) { }
        // RVA: 0x0B25A744  token: 0x6000276
        public static Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B27454C  token: 0x6000277
        public static Unity.Mathematics.int4x2 transpose(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x0B25473C  token: 0x6000278
        public static System.UInt32 hash(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x0B257B00  token: 0x6000279
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x04DAD8B0  token: 0x600027A
        public static Unity.Mathematics.int3 int3(System.Int32 x, System.Int32 y, System.Int32 z) { }
        // RVA: 0x04DB0330  token: 0x600027B
        public static Unity.Mathematics.int3 int3(System.Int32 x, Unity.Mathematics.int2 yz) { }
        // RVA: 0x04DB0350  token: 0x600027C
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.int2 xy, System.Int32 z) { }
        // RVA: 0x04DB0370  token: 0x600027D
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.int3 xyz) { }
        // RVA: 0x04DB02F0  token: 0x600027E
        public static Unity.Mathematics.int3 int3(System.Int32 v) { }
        // RVA: 0x0B25A788  token: 0x600027F
        public static Unity.Mathematics.int3 int3(System.Boolean v) { }
        // RVA: 0x0B25A7A0  token: 0x6000280
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB02F0  token: 0x6000281
        public static Unity.Mathematics.int3 int3(System.UInt32 v) { }
        // RVA: 0x04DB0370  token: 0x6000282
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.uint3 v) { }
        // RVA: 0x04DB03C0  token: 0x6000283
        public static Unity.Mathematics.int3 int3(System.Single v) { }
        // RVA: 0x04DB0300  token: 0x6000284
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.float3 v) { }
        // RVA: 0x04DB03A0  token: 0x6000285
        public static Unity.Mathematics.int3 int3(System.Double v) { }
        // RVA: 0x04DB03E0  token: 0x6000286
        public static Unity.Mathematics.int3 int3(Unity.Mathematics.double3 v) { }
        // RVA: 0x0B256128  token: 0x6000287
        public static System.UInt32 hash(Unity.Mathematics.int3 v) { }
        // RVA: 0x05F15AF0  token: 0x6000288
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3 v) { }
        // RVA: 0x05F33DE8  token: 0x6000289
        public static System.Int32 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F3429C  token: 0x600028A
        public static Unity.Mathematics.int2 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F34C80  token: 0x600028B
        public static Unity.Mathematics.int3 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F343D8  token: 0x600028C
        public static Unity.Mathematics.int4 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F327BC  token: 0x600028D
        private static System.Int32 select_shuffle_component(Unity.Mathematics.int3 a, Unity.Mathematics.int3 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAF7F0  token: 0x600028E
        public static Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1) { }
        // RVA: 0x05F16534  token: 0x600028F
        public static Unity.Mathematics.int3x2 int3x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21) { }
        // RVA: 0x0B25A884  token: 0x6000290
        public static Unity.Mathematics.int3x2 int3x2(System.Int32 v) { }
        // RVA: 0x0B25A8A4  token: 0x6000291
        public static Unity.Mathematics.int3x2 int3x2(System.Boolean v) { }
        // RVA: 0x0B25A8C4  token: 0x6000292
        public static Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x0B25A884  token: 0x6000293
        public static Unity.Mathematics.int3x2 int3x2(System.UInt32 v) { }
        // RVA: 0x0B25A814  token: 0x6000294
        public static Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x0B25A8FC  token: 0x6000295
        public static Unity.Mathematics.int3x2 int3x2(System.Single v) { }
        // RVA: 0x0B25A84C  token: 0x6000296
        public static Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B25A91C  token: 0x6000297
        public static Unity.Mathematics.int3x2 int3x2(System.Double v) { }
        // RVA: 0x0B25A7D4  token: 0x6000298
        public static Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B274370  token: 0x6000299
        public static Unity.Mathematics.int2x3 transpose(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x0B256334  token: 0x600029A
        public static System.UInt32 hash(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x0B2581C0  token: 0x600029B
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x034C99D0  token: 0x600029C
        public static Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2) { }
        // RVA: 0x05F165C4  token: 0x600029D
        public static Unity.Mathematics.int3x3 int3x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22) { }
        // RVA: 0x0B25A93C  token: 0x600029E
        public static Unity.Mathematics.int3x3 int3x3(System.Int32 v) { }
        // RVA: 0x0B25AAB4  token: 0x600029F
        public static Unity.Mathematics.int3x3 int3x3(System.Boolean v) { }
        // RVA: 0x0B25AA74  token: 0x60002A0
        public static Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x0B25A93C  token: 0x60002A1
        public static Unity.Mathematics.int3x3 int3x3(System.UInt32 v) { }
        // RVA: 0x0B25A97C  token: 0x60002A2
        public static Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x0B25A95C  token: 0x60002A3
        public static Unity.Mathematics.int3x3 int3x3(System.Single v) { }
        // RVA: 0x0B25AA14  token: 0x60002A4
        public static Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B25AA54  token: 0x60002A5
        public static Unity.Mathematics.int3x3 int3x3(System.Double v) { }
        // RVA: 0x0B25A9BC  token: 0x60002A6
        public static Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B2748D4  token: 0x60002A7
        public static Unity.Mathematics.int3x3 transpose(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x04DAE920  token: 0x60002A8
        public static System.Int32 determinant(Unity.Mathematics.int3x3 m) { }
        // RVA: 0x0B25449C  token: 0x60002A9
        public static System.UInt32 hash(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x0B257994  token: 0x60002AA
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x04DAF820  token: 0x60002AB
        public static Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2, Unity.Mathematics.int3 c3) { }
        // RVA: 0x05F16688  token: 0x60002AC
        public static Unity.Mathematics.int3x4 int3x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23) { }
        // RVA: 0x0B25AB90  token: 0x60002AD
        public static Unity.Mathematics.int3x4 int3x4(System.Int32 v) { }
        // RVA: 0x0B25AAD4  token: 0x60002AE
        public static Unity.Mathematics.int3x4 int3x4(System.Boolean v) { }
        // RVA: 0x0B25AB54  token: 0x60002AF
        public static Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x0B25AB90  token: 0x60002B0
        public static Unity.Mathematics.int3x4 int3x4(System.UInt32 v) { }
        // RVA: 0x0B25AC34  token: 0x60002B1
        public static Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x0B25AC14  token: 0x60002B2
        public static Unity.Mathematics.int3x4 int3x4(System.Single v) { }
        // RVA: 0x0B25ABB0  token: 0x60002B3
        public static Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B25ABF4  token: 0x60002B4
        public static Unity.Mathematics.int3x4 int3x4(System.Double v) { }
        // RVA: 0x0B25AAF4  token: 0x60002B5
        public static Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B27445C  token: 0x60002B6
        public static Unity.Mathematics.int4x3 transpose(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x0B254240  token: 0x60002B7
        public static System.UInt32 hash(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x0B258984  token: 0x60002B8
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x04DAD8C0  token: 0x60002B9
        public static Unity.Mathematics.int4 int4(System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 w) { }
        // RVA: 0x04DB04B0  token: 0x60002BA
        public static Unity.Mathematics.int4 int4(System.Int32 x, System.Int32 y, Unity.Mathematics.int2 zw) { }
        // RVA: 0x04DB0530  token: 0x60002BB
        public static Unity.Mathematics.int4 int4(System.Int32 x, Unity.Mathematics.int2 yz, System.Int32 w) { }
        // RVA: 0x04DB0430  token: 0x60002BC
        public static Unity.Mathematics.int4 int4(System.Int32 x, Unity.Mathematics.int3 yzw) { }
        // RVA: 0x04DB04D0  token: 0x60002BD
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.int2 xy, System.Int32 z, System.Int32 w) { }
        // RVA: 0x04DAB610  token: 0x60002BE
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.int2 xy, Unity.Mathematics.int2 zw) { }
        // RVA: 0x04DB04F0  token: 0x60002BF
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.int3 xyz, System.Int32 w) { }
        // RVA: 0x0B25AC78  token: 0x60002C0
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.int4 xyzw) { }
        // RVA: 0x04DB0420  token: 0x60002C1
        public static Unity.Mathematics.int4 int4(System.Int32 v) { }
        // RVA: 0x0B25ACA4  token: 0x60002C2
        public static Unity.Mathematics.int4 int4(System.Boolean v) { }
        // RVA: 0x0B25ACC4  token: 0x60002C3
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB0420  token: 0x60002C4
        public static Unity.Mathematics.int4 int4(System.UInt32 v) { }
        // RVA: 0x0B25AC78  token: 0x60002C5
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.uint4 v) { }
        // RVA: 0x04DB0490  token: 0x60002C6
        public static Unity.Mathematics.int4 int4(System.Single v) { }
        // RVA: 0x0B25AD38  token: 0x60002C7
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.float4 v) { }
        // RVA: 0x04DB0470  token: 0x60002C8
        public static Unity.Mathematics.int4 int4(System.Double v) { }
        // RVA: 0x0B25AD08  token: 0x60002C9
        public static Unity.Mathematics.int4 int4(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B256414  token: 0x60002CA
        public static System.UInt32 hash(Unity.Mathematics.int4 v) { }
        // RVA: 0x0B259FE0  token: 0x60002CB
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4 v) { }
        // RVA: 0x0B271EA0  token: 0x60002CC
        public static System.Int32 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B2716B8  token: 0x60002CD
        public static Unity.Mathematics.int2 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B272564  token: 0x60002CE
        public static Unity.Mathematics.int3 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B27189C  token: 0x60002CF
        public static Unity.Mathematics.int4 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F32DD0  token: 0x60002D0
        private static System.Int32 select_shuffle_component(Unity.Mathematics.int4 a, Unity.Mathematics.int4 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAFA00  token: 0x60002D1
        public static Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1) { }
        // RVA: 0x05F16750  token: 0x60002D2
        public static Unity.Mathematics.int4x2 int4x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21, System.Int32 m30, System.Int32 m31) { }
        // RVA: 0x0B25ADCC  token: 0x60002D3
        public static Unity.Mathematics.int4x2 int4x2(System.Int32 v) { }
        // RVA: 0x0B25AE38  token: 0x60002D4
        public static Unity.Mathematics.int4x2 int4x2(System.Boolean v) { }
        // RVA: 0x0B25AE88  token: 0x60002D5
        public static Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x0B25ADCC  token: 0x60002D6
        public static Unity.Mathematics.int4x2 int4x2(System.UInt32 v) { }
        // RVA: 0x0B25ADE8  token: 0x60002D7
        public static Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x0B25AE1C  token: 0x60002D8
        public static Unity.Mathematics.int4x2 int4x2(System.Single v) { }
        // RVA: 0x0B25AE54  token: 0x60002D9
        public static Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B25AD6C  token: 0x60002DA
        public static Unity.Mathematics.int4x2 int4x2(System.Double v) { }
        // RVA: 0x0B25AD88  token: 0x60002DB
        public static Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B2743BC  token: 0x60002DC
        public static Unity.Mathematics.int2x4 transpose(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x0B25587C  token: 0x60002DD
        public static System.UInt32 hash(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x0B257E2C  token: 0x60002DE
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x04DAFA20  token: 0x60002DF
        public static Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2) { }
        // RVA: 0x05F16804  token: 0x60002E0
        public static Unity.Mathematics.int4x3 int4x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m30, System.Int32 m31, System.Int32 m32) { }
        // RVA: 0x0B25AECC  token: 0x60002E1
        public static Unity.Mathematics.int4x3 int4x3(System.Int32 v) { }
        // RVA: 0x0B25AFEC  token: 0x60002E2
        public static Unity.Mathematics.int4x3 int4x3(System.Boolean v) { }
        // RVA: 0x0B25AF6C  token: 0x60002E3
        public static Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x0B25AECC  token: 0x60002E4
        public static Unity.Mathematics.int4x3 int4x3(System.UInt32 v) { }
        // RVA: 0x0B25AFA8  token: 0x60002E5
        public static Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x0B25AF4C  token: 0x60002E6
        public static Unity.Mathematics.int4x3 int4x3(System.Single v) { }
        // RVA: 0x0B25B00C  token: 0x60002E7
        public static Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B25AEAC  token: 0x60002E8
        public static Unity.Mathematics.int4x3 int4x3(System.Double v) { }
        // RVA: 0x0B25AEEC  token: 0x60002E9
        public static Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B274770  token: 0x60002EA
        public static Unity.Mathematics.int3x4 transpose(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x0B255948  token: 0x60002EB
        public static System.UInt32 hash(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x0B259474  token: 0x60002EC
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x04DAFA40  token: 0x60002ED
        public static Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2, Unity.Mathematics.int4 c3) { }
        // RVA: 0x05F169CC  token: 0x60002EE
        public static Unity.Mathematics.int4x4 int4x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23, System.Int32 m30, System.Int32 m31, System.Int32 m32, System.Int32 m33) { }
        // RVA: 0x0B25B0CC  token: 0x60002EF
        public static Unity.Mathematics.int4x4 int4x4(System.Int32 v) { }
        // RVA: 0x0B25B04C  token: 0x60002F0
        public static Unity.Mathematics.int4x4 int4x4(System.Boolean v) { }
        // RVA: 0x0B25B114  token: 0x60002F1
        public static Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x0B25B0CC  token: 0x60002F2
        public static Unity.Mathematics.int4x4 int4x4(System.UInt32 v) { }
        // RVA: 0x0B25B14C  token: 0x60002F3
        public static Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x0B25B0F0  token: 0x60002F4
        public static Unity.Mathematics.int4x4 int4x4(System.Single v) { }
        // RVA: 0x0B25B070  token: 0x60002F5
        public static Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B25B234  token: 0x60002F6
        public static Unity.Mathematics.int4x4 int4x4(System.Double v) { }
        // RVA: 0x0B25B1A8  token: 0x60002F7
        public static Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B274AF4  token: 0x60002F8
        public static Unity.Mathematics.int4x4 transpose(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x04DAE650  token: 0x60002F9
        public static System.Int32 determinant(Unity.Mathematics.int4x4 m) { }
        // RVA: 0x0B2552C8  token: 0x60002FA
        public static System.UInt32 hash(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x0B25832C  token: 0x60002FB
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x04D88BE0  token: 0x60002FC
        public static System.Int32 asint(System.UInt32 x) { }
        // RVA: 0x0464E630  token: 0x60002FD
        public static Unity.Mathematics.int2 asint(Unity.Mathematics.uint2 x) { }
        // RVA: 0x04D93250  token: 0x60002FE
        public static Unity.Mathematics.int3 asint(Unity.Mathematics.uint3 x) { }
        // RVA: 0x02B77630  token: 0x60002FF
        public static Unity.Mathematics.int4 asint(Unity.Mathematics.uint4 x) { }
        // RVA: 0x04D94B90  token: 0x6000300
        public static System.Int32 asint(System.Single x) { }
        // RVA: 0x04DAD550  token: 0x6000301
        public static Unity.Mathematics.int2 asint(Unity.Mathematics.float2 x) { }
        // RVA: 0x04D93250  token: 0x6000302
        public static Unity.Mathematics.int3 asint(Unity.Mathematics.float3 x) { }
        // RVA: 0x02B77630  token: 0x6000303
        public static Unity.Mathematics.int4 asint(Unity.Mathematics.float4 x) { }
        // RVA: 0x04D88BE0  token: 0x6000304
        public static System.UInt32 asuint(System.Int32 x) { }
        // RVA: 0x0464E630  token: 0x6000305
        public static Unity.Mathematics.uint2 asuint(Unity.Mathematics.int2 x) { }
        // RVA: 0x04D93250  token: 0x6000306
        public static Unity.Mathematics.uint3 asuint(Unity.Mathematics.int3 x) { }
        // RVA: 0x02B77630  token: 0x6000307
        public static Unity.Mathematics.uint4 asuint(Unity.Mathematics.int4 x) { }
        // RVA: 0x04D94B90  token: 0x6000308
        public static System.UInt32 asuint(System.Single x) { }
        // RVA: 0x04DAD550  token: 0x6000309
        public static Unity.Mathematics.uint2 asuint(Unity.Mathematics.float2 x) { }
        // RVA: 0x04D93250  token: 0x600030A
        public static Unity.Mathematics.uint3 asuint(Unity.Mathematics.float3 x) { }
        // RVA: 0x02B77630  token: 0x600030B
        public static Unity.Mathematics.uint4 asuint(Unity.Mathematics.float4 x) { }
        // RVA: 0x0464E630  token: 0x600030C
        public static System.Int64 aslong(System.UInt64 x) { }
        // RVA: 0x04DA48C0  token: 0x600030D
        public static System.Int64 aslong(System.Double x) { }
        // RVA: 0x0464E630  token: 0x600030E
        public static System.UInt64 asulong(System.Int64 x) { }
        // RVA: 0x04DA48C0  token: 0x600030F
        public static System.UInt64 asulong(System.Double x) { }
        // RVA: 0x04D94BC0  token: 0x6000310
        public static System.Single asfloat(System.Int32 x) { }
        // RVA: 0x04DAD350  token: 0x6000311
        public static Unity.Mathematics.float2 asfloat(Unity.Mathematics.int2 x) { }
        // RVA: 0x04D93250  token: 0x6000312
        public static Unity.Mathematics.float3 asfloat(Unity.Mathematics.int3 x) { }
        // RVA: 0x02B77630  token: 0x6000313
        public static Unity.Mathematics.float4 asfloat(Unity.Mathematics.int4 x) { }
        // RVA: 0x04D94BC0  token: 0x6000314
        public static System.Single asfloat(System.UInt32 x) { }
        // RVA: 0x04DAD350  token: 0x6000315
        public static Unity.Mathematics.float2 asfloat(Unity.Mathematics.uint2 x) { }
        // RVA: 0x04D93250  token: 0x6000316
        public static Unity.Mathematics.float3 asfloat(Unity.Mathematics.uint3 x) { }
        // RVA: 0x02B77630  token: 0x6000317
        public static Unity.Mathematics.float4 asfloat(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24C880  token: 0x6000318
        public static System.Int32 bitmask(Unity.Mathematics.bool4 value) { }
        // RVA: 0x04DA48D0  token: 0x6000319
        public static System.Double asdouble(System.Int64 x) { }
        // RVA: 0x04DA48D0  token: 0x600031A
        public static System.Double asdouble(System.UInt64 x) { }
        // RVA: 0x0B25CEC0  token: 0x600031B
        public static System.Boolean isfinite(System.Single x) { }
        // RVA: 0x0B25CE48  token: 0x600031C
        public static Unity.Mathematics.bool2 isfinite(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B25CD08  token: 0x600031D
        public static Unity.Mathematics.bool3 isfinite(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B25CD74  token: 0x600031E
        public static Unity.Mathematics.bool4 isfinite(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25CCE0  token: 0x600031F
        public static System.Boolean isfinite(System.Double x) { }
        // RVA: 0x0B25CE7C  token: 0x6000320
        public static Unity.Mathematics.bool2 isfinite(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25CEDC  token: 0x6000321
        public static Unity.Mathematics.bool3 isfinite(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25CDD8  token: 0x6000322
        public static Unity.Mathematics.bool4 isfinite(Unity.Mathematics.double4 x) { }
        // RVA: 0x05F16FD4  token: 0x6000323
        public static System.Boolean isinf(System.Single x) { }
        // RVA: 0x0B25D030  token: 0x6000324
        public static Unity.Mathematics.bool2 isinf(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B25D0C8  token: 0x6000325
        public static Unity.Mathematics.bool3 isinf(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B25D08C  token: 0x6000326
        public static Unity.Mathematics.bool4 isinf(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25CFB8  token: 0x6000327
        public static System.Boolean isinf(System.Double x) { }
        // RVA: 0x0B25D050  token: 0x6000328
        public static Unity.Mathematics.bool2 isinf(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25CF50  token: 0x6000329
        public static Unity.Mathematics.bool3 isinf(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25CFE4  token: 0x600032A
        public static Unity.Mathematics.bool4 isinf(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B25D1DC  token: 0x600032B
        public static System.Boolean isnan(System.Single x) { }
        // RVA: 0x0B25D190  token: 0x600032C
        public static Unity.Mathematics.bool2 isnan(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B25D338  token: 0x600032D
        public static Unity.Mathematics.bool3 isnan(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B25D2BC  token: 0x600032E
        public static Unity.Mathematics.bool4 isnan(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25D258  token: 0x600032F
        public static System.Boolean isnan(System.Double x) { }
        // RVA: 0x0B25D27C  token: 0x6000330
        public static Unity.Mathematics.bool2 isnan(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25D130  token: 0x6000331
        public static Unity.Mathematics.bool3 isnan(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25D1F0  token: 0x6000332
        public static Unity.Mathematics.bool4 isnan(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B25D404  token: 0x6000333
        public static System.Boolean ispow2(System.Int32 x) { }
        // RVA: 0x0B25D414  token: 0x6000334
        public static Unity.Mathematics.bool2 ispow2(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B25D3B0  token: 0x6000335
        public static Unity.Mathematics.bool3 ispow2(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B25D554  token: 0x6000336
        public static Unity.Mathematics.bool4 ispow2(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B25D4C0  token: 0x6000337
        public static System.Boolean ispow2(System.UInt32 x) { }
        // RVA: 0x0B25D4D0  token: 0x6000338
        public static Unity.Mathematics.bool2 ispow2(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B25D504  token: 0x6000339
        public static Unity.Mathematics.bool3 ispow2(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B25D448  token: 0x600033A
        public static Unity.Mathematics.bool4 ispow2(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B25E778  token: 0x600033B
        public static System.Int32 min(System.Int32 x, System.Int32 y) { }
        // RVA: 0x0B25EA5C  token: 0x600033C
        public static Unity.Mathematics.int2 min(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y) { }
        // RVA: 0x0B25EA9C  token: 0x600033D
        public static Unity.Mathematics.int3 min(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y) { }
        // RVA: 0x0B25E854  token: 0x600033E
        public static Unity.Mathematics.int4 min(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y) { }
        // RVA: 0x0B25EA94  token: 0x600033F
        public static System.UInt32 min(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x0B25E940  token: 0x6000340
        public static Unity.Mathematics.uint2 min(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y) { }
        // RVA: 0x0B25E9D8  token: 0x6000341
        public static Unity.Mathematics.uint3 min(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y) { }
        // RVA: 0x0B25EA0C  token: 0x6000342
        public static Unity.Mathematics.uint4 min(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y) { }
        // RVA: 0x0B25EA50  token: 0x6000343
        public static System.Int64 min(System.Int64 x, System.Int64 y) { }
        // RVA: 0x0B25EA88  token: 0x6000344
        public static System.UInt64 min(System.UInt64 x, System.UInt64 y) { }
        // RVA: 0x0B25E96C  token: 0x6000345
        public static System.Single min(System.Single x, System.Single y) { }
        // RVA: 0x0B25E980  token: 0x6000346
        public static Unity.Mathematics.float2 min(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x0B25E7EC  token: 0x6000347
        public static Unity.Mathematics.float3 min(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x05F1883C  token: 0x6000348
        public static Unity.Mathematics.float4 min(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x0B25E898  token: 0x6000349
        public static System.Double min(System.Double x, System.Double y) { }
        // RVA: 0x05F186FC  token: 0x600034A
        public static Unity.Mathematics.double2 min(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x0B25E780  token: 0x600034B
        public static Unity.Mathematics.double3 min(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B25E8B0  token: 0x600034C
        public static Unity.Mathematics.double4 min(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x03347DE0  token: 0x600034D
        public static System.Int32 max(System.Int32 x, System.Int32 y) { }
        // RVA: 0x0B25E444  token: 0x600034E
        public static Unity.Mathematics.int2 max(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y) { }
        // RVA: 0x0B25E410  token: 0x600034F
        public static Unity.Mathematics.int3 max(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y) { }
        // RVA: 0x0B25E5D0  token: 0x6000350
        public static Unity.Mathematics.int4 max(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y) { }
        // RVA: 0x0B25E6AC  token: 0x6000351
        public static System.UInt32 max(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x0B25E6B4  token: 0x6000352
        public static Unity.Mathematics.uint2 max(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y) { }
        // RVA: 0x0B25E6EC  token: 0x6000353
        public static Unity.Mathematics.uint3 max(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y) { }
        // RVA: 0x0B25E58C  token: 0x6000354
        public static Unity.Mathematics.uint4 max(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y) { }
        // RVA: 0x0B25E6E0  token: 0x6000355
        public static System.Int64 max(System.Int64 x, System.Int64 y) { }
        // RVA: 0x0B25E4F4  token: 0x6000356
        public static System.UInt64 max(System.UInt64 x, System.UInt64 y) { }
        // RVA: 0x0B25E4E0  token: 0x6000357
        public static System.Single max(System.Single x, System.Single y) { }
        // RVA: 0x0B25E720  token: 0x6000358
        public static Unity.Mathematics.float2 max(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x0B25E470  token: 0x6000359
        public static Unity.Mathematics.float3 max(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x05F183B4  token: 0x600035A
        public static Unity.Mathematics.float4 max(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x0B25E500  token: 0x600035B
        public static System.Double max(System.Double x, System.Double y) { }
        // RVA: 0x05F18500  token: 0x600035C
        public static Unity.Mathematics.double2 max(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x0B25E518  token: 0x600035D
        public static Unity.Mathematics.double3 max(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B25E614  token: 0x600035E
        public static Unity.Mathematics.double4 max(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x04D899F0  token: 0x600035F
        public static System.Single lerp(System.Single start, System.Single end, System.Single t) { }
        // RVA: 0x0B25DC44  token: 0x6000360
        public static Unity.Mathematics.float2 lerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, System.Single t) { }
        // RVA: 0x05F176FC  token: 0x6000361
        public static Unity.Mathematics.float3 lerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, System.Single t) { }
        // RVA: 0x0B25DC80  token: 0x6000362
        public static Unity.Mathematics.float4 lerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, System.Single t) { }
        // RVA: 0x0B25D878  token: 0x6000363
        public static Unity.Mathematics.float2 lerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, Unity.Mathematics.float2 t) { }
        // RVA: 0x05F172F4  token: 0x6000364
        public static Unity.Mathematics.float3 lerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, Unity.Mathematics.float3 t) { }
        // RVA: 0x0B25D7F0  token: 0x6000365
        public static Unity.Mathematics.float4 lerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, Unity.Mathematics.float4 t) { }
        // RVA: 0x04DB05D0  token: 0x6000366
        public static System.Double lerp(System.Double start, System.Double end, System.Double t) { }
        // RVA: 0x0B25D774  token: 0x6000367
        public static Unity.Mathematics.double2 lerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, System.Double t) { }
        // RVA: 0x0B25DB84  token: 0x6000368
        public static Unity.Mathematics.double3 lerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, System.Double t) { }
        // RVA: 0x0B25DA48  token: 0x6000369
        public static Unity.Mathematics.double4 lerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, System.Double t) { }
        // RVA: 0x0B25DAFC  token: 0x600036A
        public static Unity.Mathematics.double2 lerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, Unity.Mathematics.double2 t) { }
        // RVA: 0x0B25D978  token: 0x600036B
        public static Unity.Mathematics.double3 lerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, Unity.Mathematics.double3 t) { }
        // RVA: 0x0B25D8B4  token: 0x600036C
        public static Unity.Mathematics.double4 lerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, Unity.Mathematics.double4 t) { }
        // RVA: 0x0B2760B4  token: 0x600036D
        public static System.Single unlerp(System.Single start, System.Single end, System.Single x) { }
        // RVA: 0x0B27617C  token: 0x600036E
        public static Unity.Mathematics.float2 unlerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, Unity.Mathematics.float2 x) { }
        // RVA: 0x05F3A004  token: 0x600036F
        public static Unity.Mathematics.float3 unlerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, Unity.Mathematics.float3 x) { }
        // RVA: 0x0B2760C4  token: 0x6000370
        public static Unity.Mathematics.float4 unlerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, Unity.Mathematics.float4 x) { }
        // RVA: 0x0B2762B4  token: 0x6000371
        public static System.Double unlerp(System.Double start, System.Double end, System.Double x) { }
        // RVA: 0x0B27603C  token: 0x6000372
        public static Unity.Mathematics.double2 unlerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, Unity.Mathematics.double2 x) { }
        // RVA: 0x0B275F4C  token: 0x6000373
        public static Unity.Mathematics.double3 unlerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, Unity.Mathematics.double3 x) { }
        // RVA: 0x0B2761D8  token: 0x6000374
        public static Unity.Mathematics.double4 unlerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26F930  token: 0x6000375
        public static System.Single remap(System.Single srcStart, System.Single srcEnd, System.Single dstStart, System.Single dstEnd, System.Single x) { }
        // RVA: 0x0B26F8AC  token: 0x6000376
        public static Unity.Mathematics.float2 remap(Unity.Mathematics.float2 srcStart, Unity.Mathematics.float2 srcEnd, Unity.Mathematics.float2 dstStart, Unity.Mathematics.float2 dstEnd, Unity.Mathematics.float2 x) { }
        // RVA: 0x05F30DA0  token: 0x6000377
        public static Unity.Mathematics.float3 remap(Unity.Mathematics.float3 srcStart, Unity.Mathematics.float3 srcEnd, Unity.Mathematics.float3 dstStart, Unity.Mathematics.float3 dstEnd, Unity.Mathematics.float3 x) { }
        // RVA: 0x0B26F7F8  token: 0x6000378
        public static Unity.Mathematics.float4 remap(Unity.Mathematics.float4 srcStart, Unity.Mathematics.float4 srcEnd, Unity.Mathematics.float4 dstStart, Unity.Mathematics.float4 dstEnd, Unity.Mathematics.float4 x) { }
        // RVA: 0x0B26F888  token: 0x6000379
        public static System.Double remap(System.Double srcStart, System.Double srcEnd, System.Double dstStart, System.Double dstEnd, System.Double x) { }
        // RVA: 0x0B26FA34  token: 0x600037A
        public static Unity.Mathematics.double2 remap(Unity.Mathematics.double2 srcStart, Unity.Mathematics.double2 srcEnd, Unity.Mathematics.double2 dstStart, Unity.Mathematics.double2 dstEnd, Unity.Mathematics.double2 x) { }
        // RVA: 0x0B26F954  token: 0x600037B
        public static Unity.Mathematics.double3 remap(Unity.Mathematics.double3 srcStart, Unity.Mathematics.double3 srcEnd, Unity.Mathematics.double3 dstStart, Unity.Mathematics.double3 dstEnd, Unity.Mathematics.double3 x) { }
        // RVA: 0x0B26FAC4  token: 0x600037C
        public static Unity.Mathematics.double4 remap(Unity.Mathematics.double4 srcStart, Unity.Mathematics.double4 srcEnd, Unity.Mathematics.double4 dstStart, Unity.Mathematics.double4 dstEnd, Unity.Mathematics.double4 x) { }
        // RVA: 0x04DB09D0  token: 0x600037D
        public static System.Int32 mad(System.Int32 mulA, System.Int32 mulB, System.Int32 addC) { }
        // RVA: 0x0B25E1F0  token: 0x600037E
        public static Unity.Mathematics.int2 mad(Unity.Mathematics.int2 mulA, Unity.Mathematics.int2 mulB, Unity.Mathematics.int2 addC) { }
        // RVA: 0x05F180B4  token: 0x600037F
        public static Unity.Mathematics.int3 mad(Unity.Mathematics.int3 mulA, Unity.Mathematics.int3 mulB, Unity.Mathematics.int3 addC) { }
        // RVA: 0x0B25E18C  token: 0x6000380
        public static Unity.Mathematics.int4 mad(Unity.Mathematics.int4 mulA, Unity.Mathematics.int4 mulB, Unity.Mathematics.int4 addC) { }
        // RVA: 0x04DB09D0  token: 0x6000381
        public static System.UInt32 mad(System.UInt32 mulA, System.UInt32 mulB, System.UInt32 addC) { }
        // RVA: 0x0B25E1F0  token: 0x6000382
        public static Unity.Mathematics.uint2 mad(Unity.Mathematics.uint2 mulA, Unity.Mathematics.uint2 mulB, Unity.Mathematics.uint2 addC) { }
        // RVA: 0x05F180B4  token: 0x6000383
        public static Unity.Mathematics.uint3 mad(Unity.Mathematics.uint3 mulA, Unity.Mathematics.uint3 mulB, Unity.Mathematics.uint3 addC) { }
        // RVA: 0x0B25E18C  token: 0x6000384
        public static Unity.Mathematics.uint4 mad(Unity.Mathematics.uint4 mulA, Unity.Mathematics.uint4 mulB, Unity.Mathematics.uint4 addC) { }
        // RVA: 0x04DB09A0  token: 0x6000385
        public static System.Int64 mad(System.Int64 mulA, System.Int64 mulB, System.Int64 addC) { }
        // RVA: 0x04DB09A0  token: 0x6000386
        public static System.UInt64 mad(System.UInt64 mulA, System.UInt64 mulB, System.UInt64 addC) { }
        // RVA: 0x04DB09B0  token: 0x6000387
        public static System.Single mad(System.Single mulA, System.Single mulB, System.Single addC) { }
        // RVA: 0x0B25E2A0  token: 0x6000388
        public static Unity.Mathematics.float2 mad(Unity.Mathematics.float2 mulA, Unity.Mathematics.float2 mulB, Unity.Mathematics.float2 addC) { }
        // RVA: 0x05F1832C  token: 0x6000389
        public static Unity.Mathematics.float3 mad(Unity.Mathematics.float3 mulA, Unity.Mathematics.float3 mulB, Unity.Mathematics.float3 addC) { }
        // RVA: 0x0B25E31C  token: 0x600038A
        public static Unity.Mathematics.float4 mad(Unity.Mathematics.float4 mulA, Unity.Mathematics.float4 mulB, Unity.Mathematics.float4 addC) { }
        // RVA: 0x04DB09C0  token: 0x600038B
        public static System.Double mad(System.Double mulA, System.Double mulB, System.Double addC) { }
        // RVA: 0x0B25E2B8  token: 0x600038C
        public static Unity.Mathematics.double2 mad(Unity.Mathematics.double2 mulA, Unity.Mathematics.double2 mulB, Unity.Mathematics.double2 addC) { }
        // RVA: 0x0B25E208  token: 0x600038D
        public static Unity.Mathematics.double3 mad(Unity.Mathematics.double3 mulA, Unity.Mathematics.double3 mulB, Unity.Mathematics.double3 addC) { }
        // RVA: 0x0B25E380  token: 0x600038E
        public static Unity.Mathematics.double4 mad(Unity.Mathematics.double4 mulA, Unity.Mathematics.double4 mulB, Unity.Mathematics.double4 addC) { }
        // RVA: 0x0B24D4C8  token: 0x600038F
        public static System.Int32 clamp(System.Int32 valueToClamp, System.Int32 lowerBound, System.Int32 upperBound) { }
        // RVA: 0x0B24D518  token: 0x6000390
        public static Unity.Mathematics.int2 clamp(Unity.Mathematics.int2 valueToClamp, Unity.Mathematics.int2 lowerBound, Unity.Mathematics.int2 upperBound) { }
        // RVA: 0x05F09A58  token: 0x6000391
        public static Unity.Mathematics.int3 clamp(Unity.Mathematics.int3 valueToClamp, Unity.Mathematics.int3 lowerBound, Unity.Mathematics.int3 upperBound) { }
        // RVA: 0x0B24D404  token: 0x6000392
        public static Unity.Mathematics.int4 clamp(Unity.Mathematics.int4 valueToClamp, Unity.Mathematics.int4 lowerBound, Unity.Mathematics.int4 upperBound) { }
        // RVA: 0x0B24D3F0  token: 0x6000393
        public static System.UInt32 clamp(System.UInt32 valueToClamp, System.UInt32 lowerBound, System.UInt32 upperBound) { }
        // RVA: 0x0B24D2CC  token: 0x6000394
        public static Unity.Mathematics.uint2 clamp(Unity.Mathematics.uint2 valueToClamp, Unity.Mathematics.uint2 lowerBound, Unity.Mathematics.uint2 upperBound) { }
        // RVA: 0x05F09C00  token: 0x6000395
        public static Unity.Mathematics.uint3 clamp(Unity.Mathematics.uint3 valueToClamp, Unity.Mathematics.uint3 lowerBound, Unity.Mathematics.uint3 upperBound) { }
        // RVA: 0x0B24D104  token: 0x6000396
        public static Unity.Mathematics.uint4 clamp(Unity.Mathematics.uint4 valueToClamp, Unity.Mathematics.uint4 lowerBound, Unity.Mathematics.uint4 upperBound) { }
        // RVA: 0x0B24D350  token: 0x6000397
        public static System.Int64 clamp(System.Int64 valueToClamp, System.Int64 lowerBound, System.Int64 upperBound) { }
        // RVA: 0x0B24D504  token: 0x6000398
        public static System.UInt64 clamp(System.UInt64 valueToClamp, System.UInt64 lowerBound, System.UInt64 upperBound) { }
        // RVA: 0x0B24D4DC  token: 0x6000399
        public static System.Single clamp(System.Single valueToClamp, System.Single lowerBound, System.Single upperBound) { }
        // RVA: 0x0B24D328  token: 0x600039A
        public static Unity.Mathematics.float2 clamp(Unity.Mathematics.float2 valueToClamp, Unity.Mathematics.float2 lowerBound, Unity.Mathematics.float2 upperBound) { }
        // RVA: 0x0B24D364  token: 0x600039B
        public static Unity.Mathematics.float3 clamp(Unity.Mathematics.float3 valueToClamp, Unity.Mathematics.float3 lowerBound, Unity.Mathematics.float3 upperBound) { }
        // RVA: 0x0B24D1C8  token: 0x600039C
        public static Unity.Mathematics.float4 clamp(Unity.Mathematics.float4 valueToClamp, Unity.Mathematics.float4 lowerBound, Unity.Mathematics.float4 upperBound) { }
        // RVA: 0x05F09928  token: 0x600039D
        public static System.Double clamp(System.Double valueToClamp, System.Double lowerBound, System.Double upperBound) { }
        // RVA: 0x0B24D574  token: 0x600039E
        public static Unity.Mathematics.double2 clamp(Unity.Mathematics.double2 valueToClamp, Unity.Mathematics.double2 lowerBound, Unity.Mathematics.double2 upperBound) { }
        // RVA: 0x0B24D230  token: 0x600039F
        public static Unity.Mathematics.double3 clamp(Unity.Mathematics.double3 valueToClamp, Unity.Mathematics.double3 lowerBound, Unity.Mathematics.double3 upperBound) { }
        // RVA: 0x0B24D5DC  token: 0x60003A0
        public static Unity.Mathematics.double4 clamp(Unity.Mathematics.double4 valueToClamp, Unity.Mathematics.double4 lowerBound, Unity.Mathematics.double4 upperBound) { }
        // RVA: 0x09F3AE20  token: 0x60003A1
        public static System.Single saturate(System.Single x) { }
        // RVA: 0x0B270EE0  token: 0x60003A2
        public static Unity.Mathematics.float2 saturate(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F3215C  token: 0x60003A3
        public static Unity.Mathematics.float3 saturate(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B270E94  token: 0x60003A4
        public static Unity.Mathematics.float4 saturate(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B270E84  token: 0x60003A5
        public static System.Double saturate(System.Double x) { }
        // RVA: 0x0B270FB8  token: 0x60003A6
        public static Unity.Mathematics.double2 saturate(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B270F1C  token: 0x60003A7
        public static Unity.Mathematics.double3 saturate(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B270E18  token: 0x60003A8
        public static Unity.Mathematics.double4 saturate(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24BC98  token: 0x60003A9
        public static System.Int32 abs(System.Int32 x) { }
        // RVA: 0x0B24BB9C  token: 0x60003AA
        public static Unity.Mathematics.int2 abs(Unity.Mathematics.int2 x) { }
        // RVA: 0x05F07C48  token: 0x60003AB
        public static Unity.Mathematics.int3 abs(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24BBE0  token: 0x60003AC
        public static Unity.Mathematics.int4 abs(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B24BB8C  token: 0x60003AD
        public static System.Int64 abs(System.Int64 x) { }
        // RVA: 0x04DAD080  token: 0x60003AE
        public static System.Single abs(System.Single x) { }
        // RVA: 0x04DAD140  token: 0x60003AF
        public static Unity.Mathematics.float2 abs(Unity.Mathematics.float2 x) { }
        // RVA: 0x03B0AD90  token: 0x60003B0
        public static Unity.Mathematics.float3 abs(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B24BB58  token: 0x60003B1
        public static Unity.Mathematics.float4 abs(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DAD120  token: 0x60003B2
        public static System.Double abs(System.Double x) { }
        // RVA: 0x04DAD0F0  token: 0x60003B3
        public static Unity.Mathematics.double2 abs(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAD040  token: 0x60003B4
        public static Unity.Mathematics.double3 abs(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAD090  token: 0x60003B5
        public static Unity.Mathematics.double4 abs(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DAEA70  token: 0x60003B6
        public static System.Int32 dot(System.Int32 x, System.Int32 y) { }
        // RVA: 0x04DAEB00  token: 0x60003B7
        public static System.Int32 dot(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y) { }
        // RVA: 0x04D85D00  token: 0x60003B8
        public static System.Int32 dot(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y) { }
        // RVA: 0x04DAEAC0  token: 0x60003B9
        public static System.Int32 dot(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y) { }
        // RVA: 0x04DAEA70  token: 0x60003BA
        public static System.UInt32 dot(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x04DAEB00  token: 0x60003BB
        public static System.UInt32 dot(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y) { }
        // RVA: 0x04D85D00  token: 0x60003BC
        public static System.UInt32 dot(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y) { }
        // RVA: 0x04DAEAC0  token: 0x60003BD
        public static System.UInt32 dot(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y) { }
        // RVA: 0x04DAEB20  token: 0x60003BE
        public static System.Single dot(System.Single x, System.Single y) { }
        // RVA: 0x04D8B640  token: 0x60003BF
        public static System.Single dot(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x04D8B7C0  token: 0x60003C0
        public static System.Single dot(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x034C9990  token: 0x60003C1
        public static System.Single dot(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x04DAEAF0  token: 0x60003C2
        public static System.Double dot(System.Double x, System.Double y) { }
        // RVA: 0x04DAEB60  token: 0x60003C3
        public static System.Double dot(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x04DAEB30  token: 0x60003C4
        public static System.Double dot(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x04DAEA80  token: 0x60003C5
        public static System.Double dot(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x0B273C40  token: 0x60003C6
        public static System.Single tan(System.Single x) { }
        // RVA: 0x0B273ACC  token: 0x60003C7
        public static Unity.Mathematics.float2 tan(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F36CA4  token: 0x60003C8
        public static Unity.Mathematics.float3 tan(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F36C20  token: 0x60003C9
        public static Unity.Mathematics.float4 tan(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B273BF0  token: 0x60003CA
        public static System.Double tan(System.Double x) { }
        // RVA: 0x05F36A40  token: 0x60003CB
        public static Unity.Mathematics.double2 tan(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B273B8C  token: 0x60003CC
        public static Unity.Mathematics.double3 tan(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B273B08  token: 0x60003CD
        public static Unity.Mathematics.double4 tan(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B273E0C  token: 0x60003CE
        public static System.Single tanh(System.Single x) { }
        // RVA: 0x0B273DD0  token: 0x60003CF
        public static Unity.Mathematics.float2 tanh(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F36F24  token: 0x60003D0
        public static Unity.Mathematics.float3 tanh(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F36E10  token: 0x60003D1
        public static Unity.Mathematics.float4 tanh(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B273D80  token: 0x60003D2
        public static System.Double tanh(System.Double x) { }
        // RVA: 0x05F36FE0  token: 0x60003D3
        public static Unity.Mathematics.double2 tanh(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B273C98  token: 0x60003D4
        public static Unity.Mathematics.double3 tanh(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B273CFC  token: 0x60003D5
        public static Unity.Mathematics.double4 tanh(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24C738  token: 0x60003D6
        public static System.Single atan(System.Single x) { }
        // RVA: 0x0B24C844  token: 0x60003D7
        public static Unity.Mathematics.float2 atan(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F0886C  token: 0x60003D8
        public static Unity.Mathematics.float3 atan(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F087E8  token: 0x60003D9
        public static Unity.Mathematics.float4 atan(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24C7F4  token: 0x60003DA
        public static System.Double atan(System.Double x) { }
        // RVA: 0x05F08A30  token: 0x60003DB
        public static Unity.Mathematics.double2 atan(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24C790  token: 0x60003DC
        public static Unity.Mathematics.double3 atan(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24C6B4  token: 0x60003DD
        public static Unity.Mathematics.double4 atan(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24C5AC  token: 0x60003DE
        public static System.Single atan2(System.Single y, System.Single x) { }
        // RVA: 0x0B24C560  token: 0x60003DF
        public static Unity.Mathematics.float2 atan2(Unity.Mathematics.float2 y, Unity.Mathematics.float2 x) { }
        // RVA: 0x05F08570  token: 0x60003E0
        public static Unity.Mathematics.float3 atan2(Unity.Mathematics.float3 y, Unity.Mathematics.float3 x) { }
        // RVA: 0x05F086A0  token: 0x60003E1
        public static Unity.Mathematics.float4 atan2(Unity.Mathematics.float4 y, Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24C500  token: 0x60003E2
        public static System.Double atan2(System.Double y, System.Double x) { }
        // RVA: 0x05F08640  token: 0x60003E3
        public static Unity.Mathematics.double2 atan2(Unity.Mathematics.double2 y, Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24C480  token: 0x60003E4
        public static Unity.Mathematics.double3 atan2(Unity.Mathematics.double3 y, Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24C610  token: 0x60003E5
        public static Unity.Mathematics.double4 atan2(Unity.Mathematics.double4 y, Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24D9D8  token: 0x60003E6
        public static System.Single cos(System.Single x) { }
        // RVA: 0x04DADB80  token: 0x60003E7
        public static Unity.Mathematics.float2 cos(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DADC10  token: 0x60003E8
        public static Unity.Mathematics.float3 cos(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DADD00  token: 0x60003E9
        public static Unity.Mathematics.float4 cos(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24DA30  token: 0x60003EA
        public static System.Double cos(System.Double x) { }
        // RVA: 0x04DADC60  token: 0x60003EB
        public static Unity.Mathematics.double2 cos(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DADBC0  token: 0x60003EC
        public static Unity.Mathematics.double3 cos(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DADCA0  token: 0x60003ED
        public static Unity.Mathematics.double4 cos(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24DA80  token: 0x60003EE
        public static System.Single cosh(System.Single x) { }
        // RVA: 0x04DADD60  token: 0x60003EF
        public static Unity.Mathematics.float2 cosh(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DADDF0  token: 0x60003F0
        public static Unity.Mathematics.float3 cosh(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DADEA0  token: 0x60003F1
        public static Unity.Mathematics.float4 cosh(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24DAD8  token: 0x60003F2
        public static System.Double cosh(System.Double x) { }
        // RVA: 0x04DADF00  token: 0x60003F3
        public static Unity.Mathematics.double2 cosh(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DADDA0  token: 0x60003F4
        public static Unity.Mathematics.double3 cosh(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DADE40  token: 0x60003F5
        public static Unity.Mathematics.double4 cosh(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24BCF4  token: 0x60003F6
        public static System.Single acos(System.Single x) { }
        // RVA: 0x04DAD170  token: 0x60003F7
        public static Unity.Mathematics.float2 acos(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DAD1B0  token: 0x60003F8
        public static Unity.Mathematics.float3 acos(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DAD260  token: 0x60003F9
        public static Unity.Mathematics.float4 acos(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24BCA4  token: 0x60003FA
        public static System.Double acos(System.Double x) { }
        // RVA: 0x04DAD2C0  token: 0x60003FB
        public static Unity.Mathematics.double2 acos(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAD300  token: 0x60003FC
        public static Unity.Mathematics.double3 acos(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAD200  token: 0x60003FD
        public static Unity.Mathematics.double4 acos(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B272850  token: 0x60003FE
        public static System.Single sin(System.Single x) { }
        // RVA: 0x04DB1B20  token: 0x60003FF
        public static Unity.Mathematics.float2 sin(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DB1A10  token: 0x6000400
        public static Unity.Mathematics.float3 sin(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DB1AC0  token: 0x6000401
        public static Unity.Mathematics.float4 sin(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B2728A8  token: 0x6000402
        public static System.Double sin(System.Double x) { }
        // RVA: 0x04DB1B60  token: 0x6000403
        public static Unity.Mathematics.double2 sin(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DB19C0  token: 0x6000404
        public static Unity.Mathematics.double3 sin(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DB1A60  token: 0x6000405
        public static Unity.Mathematics.double4 sin(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B272BF0  token: 0x6000406
        public static System.Single sinh(System.Single x) { }
        // RVA: 0x04DB1DC0  token: 0x6000407
        public static Unity.Mathematics.float2 sinh(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DB1C60  token: 0x6000408
        public static Unity.Mathematics.float3 sinh(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DB1D60  token: 0x6000409
        public static Unity.Mathematics.float4 sinh(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B272BA0  token: 0x600040A
        public static System.Double sinh(System.Double x) { }
        // RVA: 0x04DB1C20  token: 0x600040B
        public static Unity.Mathematics.double2 sinh(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DB1D10  token: 0x600040C
        public static Unity.Mathematics.double3 sinh(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DB1CB0  token: 0x600040D
        public static Unity.Mathematics.double4 sinh(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24C428  token: 0x600040E
        public static System.Single asin(System.Single x) { }
        // RVA: 0x04DAD370  token: 0x600040F
        public static Unity.Mathematics.float2 asin(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DAD410  token: 0x6000410
        public static Unity.Mathematics.float3 asin(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DAD4F0  token: 0x6000411
        public static Unity.Mathematics.float4 asin(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24C3D8  token: 0x6000412
        public static System.Double asin(System.Double x) { }
        // RVA: 0x04DAD4B0  token: 0x6000413
        public static Unity.Mathematics.double2 asin(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAD460  token: 0x6000414
        public static Unity.Mathematics.double3 asin(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAD3B0  token: 0x6000415
        public static Unity.Mathematics.double4 asin(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B252FA8  token: 0x6000416
        public static System.Single floor(System.Single x) { }
        // RVA: 0x04DAFBC0  token: 0x6000417
        public static Unity.Mathematics.float2 floor(Unity.Mathematics.float2 x) { }
        // RVA: 0x0411D130  token: 0x6000418
        public static Unity.Mathematics.float3 floor(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DAFA70  token: 0x6000419
        public static Unity.Mathematics.float4 floor(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B253000  token: 0x600041A
        public static System.Double floor(System.Double x) { }
        // RVA: 0x04DAFB80  token: 0x600041B
        public static Unity.Mathematics.double2 floor(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAFAD0  token: 0x600041C
        public static Unity.Mathematics.double3 floor(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAFB20  token: 0x600041D
        public static Unity.Mathematics.double4 floor(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24CA7C  token: 0x600041E
        public static System.Single ceil(System.Single x) { }
        // RVA: 0x04DAD930  token: 0x600041F
        public static Unity.Mathematics.float2 ceil(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DADA70  token: 0x6000420
        public static Unity.Mathematics.float3 ceil(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DAD9B0  token: 0x6000421
        public static Unity.Mathematics.float4 ceil(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24CAD4  token: 0x6000422
        public static System.Double ceil(System.Double x) { }
        // RVA: 0x04DAD970  token: 0x6000423
        public static Unity.Mathematics.double2 ceil(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAD8E0  token: 0x6000424
        public static Unity.Mathematics.double3 ceil(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DADA10  token: 0x6000425
        public static Unity.Mathematics.double4 ceil(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B270A54  token: 0x6000426
        public static System.Single round(System.Single x) { }
        // RVA: 0x0B270B94  token: 0x6000427
        public static Unity.Mathematics.float2 round(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F31D10  token: 0x6000428
        public static Unity.Mathematics.float3 round(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F31E7C  token: 0x6000429
        public static Unity.Mathematics.float4 round(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B270A04  token: 0x600042A
        public static System.Double round(System.Double x) { }
        // RVA: 0x05F31CC8  token: 0x600042B
        public static Unity.Mathematics.double2 round(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B270AAC  token: 0x600042C
        public static Unity.Mathematics.double3 round(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B270B10  token: 0x600042D
        public static Unity.Mathematics.double4 round(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B2752E8  token: 0x600042E
        public static System.Single trunc(System.Single x) { }
        // RVA: 0x0B2752AC  token: 0x600042F
        public static Unity.Mathematics.float2 trunc(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F39770  token: 0x6000430
        public static Unity.Mathematics.float3 trunc(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F39618  token: 0x6000431
        public static Unity.Mathematics.float4 trunc(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B275174  token: 0x6000432
        public static System.Double trunc(System.Double x) { }
        // RVA: 0x05F39858  token: 0x6000433
        public static Unity.Mathematics.double2 trunc(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B2751C4  token: 0x6000434
        public static Unity.Mathematics.double3 trunc(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B275228  token: 0x6000435
        public static Unity.Mathematics.double4 trunc(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DA2CA0  token: 0x6000436
        public static System.Single frac(System.Single x) { }
        // RVA: 0x0B253168  token: 0x6000437
        public static Unity.Mathematics.float2 frac(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F0E4F0  token: 0x6000438
        public static Unity.Mathematics.float3 frac(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B253104  token: 0x6000439
        public static Unity.Mathematics.float4 frac(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DB0010  token: 0x600043A
        public static System.Double frac(System.Double x) { }
        // RVA: 0x0B25322C  token: 0x600043B
        public static Unity.Mathematics.double2 frac(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B253198  token: 0x600043C
        public static Unity.Mathematics.double3 frac(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B253290  token: 0x600043D
        public static Unity.Mathematics.double4 frac(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26EE6C  token: 0x600043E
        public static System.Single rcp(System.Single x) { }
        // RVA: 0x0B26ED2C  token: 0x600043F
        public static Unity.Mathematics.float2 rcp(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B26ED98  token: 0x6000440
        public static Unity.Mathematics.float3 rcp(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B26EE00  token: 0x6000441
        public static Unity.Mathematics.float4 rcp(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B26ED1C  token: 0x6000442
        public static System.Double rcp(System.Double x) { }
        // RVA: 0x0B26EDD8  token: 0x6000443
        public static Unity.Mathematics.double2 rcp(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B26ECE0  token: 0x6000444
        public static Unity.Mathematics.double3 rcp(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B26ED54  token: 0x6000445
        public static Unity.Mathematics.double4 rcp(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B27260C  token: 0x6000446
        public static System.Int32 sign(System.Int32 x) { }
        // RVA: 0x0B272754  token: 0x6000447
        public static Unity.Mathematics.int2 sign(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B272808  token: 0x6000448
        public static Unity.Mathematics.int3 sign(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B2726F4  token: 0x6000449
        public static Unity.Mathematics.int4 sign(Unity.Mathematics.int4 x) { }
        // RVA: 0x05F355F4  token: 0x600044A
        public static System.Single sign(System.Single x) { }
        // RVA: 0x0B272654  token: 0x600044B
        public static Unity.Mathematics.float2 sign(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B2727D0  token: 0x600044C
        public static Unity.Mathematics.float3 sign(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B2726AC  token: 0x600044D
        public static Unity.Mathematics.float4 sign(Unity.Mathematics.float4 x) { }
        // RVA: 0x05F355BC  token: 0x600044E
        public static System.Double sign(System.Double x) { }
        // RVA: 0x0B272684  token: 0x600044F
        public static Unity.Mathematics.double2 sign(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B27261C  token: 0x6000450
        public static Unity.Mathematics.double3 sign(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B272788  token: 0x6000451
        public static Unity.Mathematics.double4 sign(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26E22C  token: 0x6000452
        public static System.Single pow(System.Single x, System.Single y) { }
        // RVA: 0x0B26E290  token: 0x6000453
        public static Unity.Mathematics.float2 pow(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x05F2F240  token: 0x6000454
        public static Unity.Mathematics.float3 pow(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x05F2F538  token: 0x6000455
        public static Unity.Mathematics.float4 pow(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x0B26E2DC  token: 0x6000456
        public static System.Double pow(System.Double x, System.Double y) { }
        // RVA: 0x05F2F4D8  token: 0x6000457
        public static Unity.Mathematics.double2 pow(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x0B26E1AC  token: 0x6000458
        public static Unity.Mathematics.double3 pow(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B26E33C  token: 0x6000459
        public static Unity.Mathematics.double4 pow(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x0B24FD18  token: 0x600045A
        public static System.Single exp(System.Single x) { }
        // RVA: 0x04DAF330  token: 0x600045B
        public static Unity.Mathematics.float2 exp(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DAF460  token: 0x600045C
        public static Unity.Mathematics.float3 exp(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DAF2D0  token: 0x600045D
        public static Unity.Mathematics.float4 exp(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24FCC8  token: 0x600045E
        public static System.Double exp(System.Double x) { }
        // RVA: 0x04DAF370  token: 0x600045F
        public static Unity.Mathematics.double2 exp(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAF3B0  token: 0x6000460
        public static Unity.Mathematics.double3 exp(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAF400  token: 0x6000461
        public static Unity.Mathematics.double4 exp(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24FAEC  token: 0x6000462
        public static System.Single exp2(System.Single x) { }
        // RVA: 0x0B24FC8C  token: 0x6000463
        public static Unity.Mathematics.float2 exp2(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F0BF44  token: 0x6000464
        public static Unity.Mathematics.float3 exp2(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F0C1F0  token: 0x6000465
        public static Unity.Mathematics.float4 exp2(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24FB4C  token: 0x6000466
        public static System.Double exp2(System.Double x) { }
        // RVA: 0x05F0C060  token: 0x6000467
        public static Unity.Mathematics.double2 exp2(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24FBA4  token: 0x6000468
        public static Unity.Mathematics.double3 exp2(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24FC08  token: 0x6000469
        public static Unity.Mathematics.double4 exp2(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24FA8C  token: 0x600046A
        public static System.Single exp10(System.Single x) { }
        // RVA: 0x0B24F910  token: 0x600046B
        public static Unity.Mathematics.float2 exp10(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F0BC14  token: 0x600046C
        public static Unity.Mathematics.float3 exp10(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F0BD84  token: 0x600046D
        public static Unity.Mathematics.float4 exp10(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24F9D0  token: 0x600046E
        public static System.Double exp10(System.Double x) { }
        // RVA: 0x05F0BCB8  token: 0x600046F
        public static Unity.Mathematics.double2 exp10(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24FA28  token: 0x6000470
        public static Unity.Mathematics.double3 exp10(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24F94C  token: 0x6000471
        public static Unity.Mathematics.double4 exp10(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B25DFD0  token: 0x6000472
        public static System.Single log(System.Single x) { }
        // RVA: 0x04DB0860  token: 0x6000473
        public static Unity.Mathematics.float2 log(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DB08F0  token: 0x6000474
        public static Unity.Mathematics.float3 log(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DB0800  token: 0x6000475
        public static Unity.Mathematics.float4 log(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25DF80  token: 0x6000476
        public static System.Double log(System.Double x) { }
        // RVA: 0x04DB07C0  token: 0x6000477
        public static Unity.Mathematics.double2 log(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DB08A0  token: 0x6000478
        public static Unity.Mathematics.double3 log(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DB0940  token: 0x6000479
        public static Unity.Mathematics.double4 log(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B25DDA4  token: 0x600047A
        public static System.Single log2(System.Single x) { }
        // RVA: 0x0B25DE00  token: 0x600047B
        public static Unity.Mathematics.float2 log2(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F17BB4  token: 0x600047C
        public static Unity.Mathematics.float3 log2(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F17C18  token: 0x600047D
        public static Unity.Mathematics.float4 log2(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25DE3C  token: 0x600047E
        public static System.Double log2(System.Double x) { }
        // RVA: 0x05F17B10  token: 0x600047F
        public static Unity.Mathematics.double2 log2(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25DE98  token: 0x6000480
        public static Unity.Mathematics.double3 log2(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25DEFC  token: 0x6000481
        public static Unity.Mathematics.double4 log2(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B25DCFC  token: 0x6000482
        public static System.Single log10(System.Single x) { }
        // RVA: 0x04DB0730  token: 0x6000483
        public static Unity.Mathematics.float2 log10(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DB0770  token: 0x6000484
        public static Unity.Mathematics.float3 log10(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DB0680  token: 0x6000485
        public static Unity.Mathematics.float4 log10(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B25DD54  token: 0x6000486
        public static System.Double log10(System.Double x) { }
        // RVA: 0x04DB05E0  token: 0x6000487
        public static Unity.Mathematics.double2 log10(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DB06E0  token: 0x6000488
        public static Unity.Mathematics.double3 log10(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DB0620  token: 0x6000489
        public static Unity.Mathematics.double4 log10(Unity.Mathematics.double4 x) { }
        // RVA: 0x04D88690  token: 0x600048A
        public static System.Single fmod(System.Single x, System.Single y) { }
        // RVA: 0x04DAFCE0  token: 0x600048B
        public static Unity.Mathematics.float2 fmod(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x04DAFC00  token: 0x600048C
        public static Unity.Mathematics.float3 fmod(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x04DAFD30  token: 0x600048D
        public static Unity.Mathematics.float4 fmod(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x04DAFE90  token: 0x600048E
        public static System.Double fmod(System.Double x, System.Double y) { }
        // RVA: 0x04DAFE30  token: 0x600048F
        public static Unity.Mathematics.double2 fmod(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x04DAFC70  token: 0x6000490
        public static Unity.Mathematics.double3 fmod(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x04DAFDB0  token: 0x6000491
        public static Unity.Mathematics.double4 fmod(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x0B25ED5C  token: 0x6000492
        public static System.Single modf(System.Single x, System.Single& i) { }
        // RVA: 0x0B25EC14  token: 0x6000493
        public static Unity.Mathematics.float2 modf(Unity.Mathematics.float2 x, Unity.Mathematics.float2& i) { }
        // RVA: 0x05F189DC  token: 0x6000494
        public static Unity.Mathematics.float3 modf(Unity.Mathematics.float3 x, Unity.Mathematics.float3& i) { }
        // RVA: 0x0B25EB74  token: 0x6000495
        public static Unity.Mathematics.float4 modf(Unity.Mathematics.float4 x, Unity.Mathematics.float4& i) { }
        // RVA: 0x0B25EBE8  token: 0x6000496
        public static System.Double modf(System.Double x, System.Double& i) { }
        // RVA: 0x0B25ECE8  token: 0x6000497
        public static Unity.Mathematics.double2 modf(Unity.Mathematics.double2 x, Unity.Mathematics.double2& i) { }
        // RVA: 0x0B25EAD0  token: 0x6000498
        public static Unity.Mathematics.double3 modf(Unity.Mathematics.double3 x, Unity.Mathematics.double3& i) { }
        // RVA: 0x0B25EC4C  token: 0x6000499
        public static Unity.Mathematics.double4 modf(Unity.Mathematics.double4 x, Unity.Mathematics.double4& i) { }
        // RVA: 0x0B273550  token: 0x600049A
        public static System.Single sqrt(System.Single x) { }
        // RVA: 0x04DB1E00  token: 0x600049B
        public static Unity.Mathematics.float2 sqrt(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DB20F0  token: 0x600049C
        public static Unity.Mathematics.float3 sqrt(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DB1E70  token: 0x600049D
        public static Unity.Mathematics.float4 sqrt(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B273508  token: 0x600049E
        public static System.Double sqrt(System.Double x) { }
        // RVA: 0x04DB1F30  token: 0x600049F
        public static Unity.Mathematics.double2 sqrt(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DB2060  token: 0x60004A0
        public static Unity.Mathematics.double3 sqrt(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DB1FA0  token: 0x60004A1
        public static Unity.Mathematics.double4 sqrt(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B270CB8  token: 0x60004A2
        public static System.Single rsqrt(System.Single x) { }
        // RVA: 0x0B270D6C  token: 0x60004A3
        public static Unity.Mathematics.float2 rsqrt(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F31F40  token: 0x60004A4
        public static Unity.Mathematics.float3 rsqrt(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B270CE8  token: 0x60004A5
        public static Unity.Mathematics.float4 rsqrt(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B270DA0  token: 0x60004A6
        public static System.Double rsqrt(System.Double x) { }
        // RVA: 0x0B270DD0  token: 0x60004A7
        public static Unity.Mathematics.double2 rsqrt(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B270BD0  token: 0x60004A8
        public static Unity.Mathematics.double3 rsqrt(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B270C48  token: 0x60004A9
        public static Unity.Mathematics.double4 rsqrt(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26D990  token: 0x60004AA
        public static Unity.Mathematics.float2 normalize(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B26D9F8  token: 0x60004AB
        public static Unity.Mathematics.float3 normalize(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B26D8B8  token: 0x60004AC
        public static Unity.Mathematics.float4 normalize(Unity.Mathematics.float4 x) { }
        // RVA: 0x05F2ED38  token: 0x60004AD
        public static Unity.Mathematics.double2 normalize(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B26D838  token: 0x60004AE
        public static Unity.Mathematics.double3 normalize(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B26D914  token: 0x60004AF
        public static Unity.Mathematics.double4 normalize(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26DB1C  token: 0x60004B0
        public static Unity.Mathematics.float2 normalizesafe(Unity.Mathematics.float2 x, Unity.Mathematics.float2 defaultvalue) { }
        // RVA: 0x05F2F020  token: 0x60004B1
        public static Unity.Mathematics.float3 normalizesafe(Unity.Mathematics.float3 x, Unity.Mathematics.float3 defaultvalue) { }
        // RVA: 0x0B26DBA4  token: 0x60004B2
        public static Unity.Mathematics.float4 normalizesafe(Unity.Mathematics.float4 x, Unity.Mathematics.float4 defaultvalue) { }
        // RVA: 0x05F2F0C0  token: 0x60004B3
        public static Unity.Mathematics.double2 normalizesafe(Unity.Mathematics.double2 x, Unity.Mathematics.double2 defaultvalue) { }
        // RVA: 0x0B26DCF0  token: 0x60004B4
        public static Unity.Mathematics.double3 normalizesafe(Unity.Mathematics.double3 x, Unity.Mathematics.double3 defaultvalue) { }
        // RVA: 0x0B26DA6C  token: 0x60004B5
        public static Unity.Mathematics.double4 normalizesafe(Unity.Mathematics.double4 x, Unity.Mathematics.double4 defaultvalue) { }
        // RVA: 0x04DAD080  token: 0x60004B6
        public static System.Single length(System.Single x) { }
        // RVA: 0x04D8B6B0  token: 0x60004B7
        public static System.Single length(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B25D5CC  token: 0x60004B8
        public static System.Single length(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B25D670  token: 0x60004B9
        public static System.Single length(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DAD120  token: 0x60004BA
        public static System.Double length(System.Double x) { }
        // RVA: 0x04DB0560  token: 0x60004BB
        public static System.Double length(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25D638  token: 0x60004BC
        public static System.Double length(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25D604  token: 0x60004BD
        public static System.Double length(Unity.Mathematics.double4 x) { }
        // RVA: 0x04D886E0  token: 0x60004BE
        public static System.Single lengthsq(System.Single x) { }
        // RVA: 0x04D8B690  token: 0x60004BF
        public static System.Single lengthsq(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B25D70C  token: 0x60004C0
        public static System.Single lengthsq(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B25D6AC  token: 0x60004C1
        public static System.Single lengthsq(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DB05C0  token: 0x60004C2
        public static System.Double lengthsq(System.Double x) { }
        // RVA: 0x04DB05A0  token: 0x60004C3
        public static System.Double lengthsq(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B25D6D4  token: 0x60004C4
        public static System.Double lengthsq(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B25D740  token: 0x60004C5
        public static System.Double lengthsq(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DAEA10  token: 0x60004C6
        public static System.Single distance(System.Single x, System.Single y) { }
        // RVA: 0x0B24E498  token: 0x60004C7
        public static System.Single distance(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x0B24E324  token: 0x60004C8
        public static System.Single distance(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x0B24E3F8  token: 0x60004C9
        public static System.Single distance(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x04DAEA30  token: 0x60004CA
        public static System.Double distance(System.Double x, System.Double y) { }
        // RVA: 0x0B24E458  token: 0x60004CB
        public static System.Double distance(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x0B24E2B0  token: 0x60004CC
        public static System.Double distance(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B24E38C  token: 0x60004CD
        public static System.Double distance(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x04DAEA50  token: 0x60004CE
        public static System.Single distancesq(System.Single x, System.Single y) { }
        // RVA: 0x0B24E4D4  token: 0x60004CF
        public static System.Single distancesq(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x0B24E630  token: 0x60004D0
        public static System.Single distancesq(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x0B24E50C  token: 0x60004D1
        public static System.Single distancesq(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x04DAEA60  token: 0x60004D2
        public static System.Double distancesq(System.Double x, System.Double y) { }
        // RVA: 0x0B24E694  token: 0x60004D3
        public static System.Double distancesq(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y) { }
        // RVA: 0x0B24E5C0  token: 0x60004D4
        public static System.Double distancesq(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B24E558  token: 0x60004D5
        public static System.Double distancesq(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y) { }
        // RVA: 0x0B24DE8C  token: 0x60004D6
        public static Unity.Mathematics.float3 cross(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x0B24DF74  token: 0x60004D7
        public static Unity.Mathematics.double3 cross(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y) { }
        // RVA: 0x0B273258  token: 0x60004D8
        public static System.Single smoothstep(System.Single xMin, System.Single xMax, System.Single x) { }
        // RVA: 0x0B2732A4  token: 0x60004D9
        public static Unity.Mathematics.float2 smoothstep(Unity.Mathematics.float2 xMin, Unity.Mathematics.float2 xMax, Unity.Mathematics.float2 x) { }
        // RVA: 0x05F35CBC  token: 0x60004DA
        public static Unity.Mathematics.float3 smoothstep(Unity.Mathematics.float3 xMin, Unity.Mathematics.float3 xMax, Unity.Mathematics.float3 x) { }
        // RVA: 0x0B2733C0  token: 0x60004DB
        public static Unity.Mathematics.float4 smoothstep(Unity.Mathematics.float4 xMin, Unity.Mathematics.float4 xMax, Unity.Mathematics.float4 x) { }
        // RVA: 0x0B273374  token: 0x60004DC
        public static System.Double smoothstep(System.Double xMin, System.Double xMax, System.Double x) { }
        // RVA: 0x0B272DE8  token: 0x60004DD
        public static Unity.Mathematics.double2 smoothstep(Unity.Mathematics.double2 xMin, Unity.Mathematics.double2 xMax, Unity.Mathematics.double2 x) { }
        // RVA: 0x0B273098  token: 0x60004DE
        public static Unity.Mathematics.double3 smoothstep(Unity.Mathematics.double3 xMin, Unity.Mathematics.double3 xMax, Unity.Mathematics.double3 x) { }
        // RVA: 0x0B272EF8  token: 0x60004DF
        public static Unity.Mathematics.double4 smoothstep(Unity.Mathematics.double4 xMin, Unity.Mathematics.double4 xMax, Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24C25C  token: 0x60004E0
        public static System.Boolean any(Unity.Mathematics.bool2 x) { }
        // RVA: 0x0B24C318  token: 0x60004E1
        public static System.Boolean any(Unity.Mathematics.bool3 x) { }
        // RVA: 0x05F08324  token: 0x60004E2
        public static System.Boolean any(Unity.Mathematics.bool4 x) { }
        // RVA: 0x0B24C248  token: 0x60004E3
        public static System.Boolean any(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B24C26C  token: 0x60004E4
        public static System.Boolean any(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24C22C  token: 0x60004E5
        public static System.Boolean any(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B24C248  token: 0x60004E6
        public static System.Boolean any(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B24C26C  token: 0x60004E7
        public static System.Boolean any(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24C22C  token: 0x60004E8
        public static System.Boolean any(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24C32C  token: 0x60004E9
        public static System.Boolean any(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B24C2A8  token: 0x60004EA
        public static System.Boolean any(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B24C354  token: 0x60004EB
        public static System.Boolean any(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24C284  token: 0x60004EC
        public static System.Boolean any(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24C38C  token: 0x60004ED
        public static System.Boolean any(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24C2D4  token: 0x60004EE
        public static System.Boolean any(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24C01C  token: 0x60004EF
        public static System.Boolean all(Unity.Mathematics.bool2 x) { }
        // RVA: 0x0B24C14C  token: 0x60004F0
        public static System.Boolean all(Unity.Mathematics.bool3 x) { }
        // RVA: 0x05F08180  token: 0x60004F1
        public static System.Boolean all(Unity.Mathematics.bool4 x) { }
        // RVA: 0x0B24C02C  token: 0x60004F2
        public static System.Boolean all(Unity.Mathematics.int2 x) { }
        // RVA: 0x05F081A0  token: 0x60004F3
        public static System.Boolean all(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24C0D0  token: 0x60004F4
        public static System.Boolean all(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B24C02C  token: 0x60004F5
        public static System.Boolean all(Unity.Mathematics.uint2 x) { }
        // RVA: 0x05F081A0  token: 0x60004F6
        public static System.Boolean all(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24C0D0  token: 0x60004F7
        public static System.Boolean all(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24C0EC  token: 0x60004F8
        public static System.Boolean all(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F081B8  token: 0x60004F9
        public static System.Boolean all(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B24C114  token: 0x60004FA
        public static System.Boolean all(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24C160  token: 0x60004FB
        public static System.Boolean all(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24C084  token: 0x60004FC
        public static System.Boolean all(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24C040  token: 0x60004FD
        public static System.Boolean all(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B27110C  token: 0x60004FE
        public static System.Int32 select(System.Int32 falseValue, System.Int32 trueValue, System.Boolean test) { }
        // RVA: 0x0B271124  token: 0x60004FF
        public static Unity.Mathematics.int2 select(Unity.Mathematics.int2 falseValue, Unity.Mathematics.int2 trueValue, System.Boolean test) { }
        // RVA: 0x0B271148  token: 0x6000500
        public static Unity.Mathematics.int3 select(Unity.Mathematics.int3 falseValue, Unity.Mathematics.int3 trueValue, System.Boolean test) { }
        // RVA: 0x0B271130  token: 0x6000501
        public static Unity.Mathematics.int4 select(Unity.Mathematics.int4 falseValue, Unity.Mathematics.int4 trueValue, System.Boolean test) { }
        // RVA: 0x0B27116C  token: 0x6000502
        public static Unity.Mathematics.int2 select(Unity.Mathematics.int2 falseValue, Unity.Mathematics.int2 trueValue, Unity.Mathematics.bool2 test) { }
        // RVA: 0x05F324D0  token: 0x6000503
        public static Unity.Mathematics.int3 select(Unity.Mathematics.int3 falseValue, Unity.Mathematics.int3 trueValue, Unity.Mathematics.bool3 test) { }
        // RVA: 0x05F32634  token: 0x6000504
        public static Unity.Mathematics.int4 select(Unity.Mathematics.int4 falseValue, Unity.Mathematics.int4 trueValue, Unity.Mathematics.bool4 test) { }
        // RVA: 0x0B27110C  token: 0x6000505
        public static System.UInt32 select(System.UInt32 falseValue, System.UInt32 trueValue, System.Boolean test) { }
        // RVA: 0x0B271124  token: 0x6000506
        public static Unity.Mathematics.uint2 select(Unity.Mathematics.uint2 falseValue, Unity.Mathematics.uint2 trueValue, System.Boolean test) { }
        // RVA: 0x0B271148  token: 0x6000507
        public static Unity.Mathematics.uint3 select(Unity.Mathematics.uint3 falseValue, Unity.Mathematics.uint3 trueValue, System.Boolean test) { }
        // RVA: 0x0B271130  token: 0x6000508
        public static Unity.Mathematics.uint4 select(Unity.Mathematics.uint4 falseValue, Unity.Mathematics.uint4 trueValue, System.Boolean test) { }
        // RVA: 0x0B27116C  token: 0x6000509
        public static Unity.Mathematics.uint2 select(Unity.Mathematics.uint2 falseValue, Unity.Mathematics.uint2 trueValue, Unity.Mathematics.bool2 test) { }
        // RVA: 0x05F324D0  token: 0x600050A
        public static Unity.Mathematics.uint3 select(Unity.Mathematics.uint3 falseValue, Unity.Mathematics.uint3 trueValue, Unity.Mathematics.bool3 test) { }
        // RVA: 0x05F32634  token: 0x600050B
        public static Unity.Mathematics.uint4 select(Unity.Mathematics.uint4 falseValue, Unity.Mathematics.uint4 trueValue, Unity.Mathematics.bool4 test) { }
        // RVA: 0x0B271100  token: 0x600050C
        public static System.Int64 select(System.Int64 falseValue, System.Int64 trueValue, System.Boolean test) { }
        // RVA: 0x0B271100  token: 0x600050D
        public static System.UInt64 select(System.UInt64 falseValue, System.UInt64 trueValue, System.Boolean test) { }
        // RVA: 0x0B271118  token: 0x600050E
        public static System.Single select(System.Single falseValue, System.Single trueValue, System.Boolean test) { }
        // RVA: 0x0B271124  token: 0x600050F
        public static Unity.Mathematics.float2 select(Unity.Mathematics.float2 falseValue, Unity.Mathematics.float2 trueValue, System.Boolean test) { }
        // RVA: 0x0B271148  token: 0x6000510
        public static Unity.Mathematics.float3 select(Unity.Mathematics.float3 falseValue, Unity.Mathematics.float3 trueValue, System.Boolean test) { }
        // RVA: 0x0B271130  token: 0x6000511
        public static Unity.Mathematics.float4 select(Unity.Mathematics.float4 falseValue, Unity.Mathematics.float4 trueValue, System.Boolean test) { }
        // RVA: 0x05F32510  token: 0x6000512
        public static Unity.Mathematics.float2 select(Unity.Mathematics.float2 falseValue, Unity.Mathematics.float2 trueValue, Unity.Mathematics.bool2 test) { }
        // RVA: 0x05F32554  token: 0x6000513
        public static Unity.Mathematics.float3 select(Unity.Mathematics.float3 falseValue, Unity.Mathematics.float3 trueValue, Unity.Mathematics.bool3 test) { }
        // RVA: 0x05F325C8  token: 0x6000514
        public static Unity.Mathematics.float4 select(Unity.Mathematics.float4 falseValue, Unity.Mathematics.float4 trueValue, Unity.Mathematics.bool4 test) { }
        // RVA: 0x0B271118  token: 0x6000515
        public static System.Double select(System.Double falseValue, System.Double trueValue, System.Boolean test) { }
        // RVA: 0x0B271130  token: 0x6000516
        public static Unity.Mathematics.double2 select(Unity.Mathematics.double2 falseValue, Unity.Mathematics.double2 trueValue, System.Boolean test) { }
        // RVA: 0x0B271290  token: 0x6000517
        public static Unity.Mathematics.double3 select(Unity.Mathematics.double3 falseValue, Unity.Mathematics.double3 trueValue, System.Boolean test) { }
        // RVA: 0x0B2712B8  token: 0x6000518
        public static Unity.Mathematics.double4 select(Unity.Mathematics.double4 falseValue, Unity.Mathematics.double4 trueValue, System.Boolean test) { }
        // RVA: 0x05F3249C  token: 0x6000519
        public static Unity.Mathematics.double2 select(Unity.Mathematics.double2 falseValue, Unity.Mathematics.double2 trueValue, Unity.Mathematics.bool2 test) { }
        // RVA: 0x0B271210  token: 0x600051A
        public static Unity.Mathematics.double3 select(Unity.Mathematics.double3 falseValue, Unity.Mathematics.double3 trueValue, Unity.Mathematics.bool3 test) { }
        // RVA: 0x0B27119C  token: 0x600051B
        public static Unity.Mathematics.double4 select(Unity.Mathematics.double4 falseValue, Unity.Mathematics.double4 trueValue, Unity.Mathematics.bool4 test) { }
        // RVA: 0x0B273700  token: 0x600051C
        public static System.Single step(System.Single threshold, System.Single x) { }
        // RVA: 0x0B273A6C  token: 0x600051D
        public static Unity.Mathematics.float2 step(Unity.Mathematics.float2 threshold, Unity.Mathematics.float2 x) { }
        // RVA: 0x0B27391C  token: 0x600051E
        public static Unity.Mathematics.float3 step(Unity.Mathematics.float3 threshold, Unity.Mathematics.float3 x) { }
        // RVA: 0x0B273728  token: 0x600051F
        public static Unity.Mathematics.float4 step(Unity.Mathematics.float4 threshold, Unity.Mathematics.float4 x) { }
        // RVA: 0x0B273714  token: 0x6000520
        public static System.Double step(System.Double threshold, System.Double x) { }
        // RVA: 0x0B2738A8  token: 0x6000521
        public static Unity.Mathematics.double2 step(Unity.Mathematics.double2 threshold, Unity.Mathematics.double2 x) { }
        // RVA: 0x0B2737E4  token: 0x6000522
        public static Unity.Mathematics.double3 step(Unity.Mathematics.double3 threshold, Unity.Mathematics.double3 x) { }
        // RVA: 0x0B2739C8  token: 0x6000523
        public static Unity.Mathematics.double4 step(Unity.Mathematics.double4 threshold, Unity.Mathematics.double4 x) { }
        // RVA: 0x0B26EE7C  token: 0x6000524
        public static Unity.Mathematics.float2 reflect(Unity.Mathematics.float2 i, Unity.Mathematics.float2 n) { }
        // RVA: 0x05F301DC  token: 0x6000525
        public static Unity.Mathematics.float3 reflect(Unity.Mathematics.float3 i, Unity.Mathematics.float3 n) { }
        // RVA: 0x0B26F128  token: 0x6000526
        public static Unity.Mathematics.float4 reflect(Unity.Mathematics.float4 i, Unity.Mathematics.float4 n) { }
        // RVA: 0x0B26F090  token: 0x6000527
        public static Unity.Mathematics.double2 reflect(Unity.Mathematics.double2 i, Unity.Mathematics.double2 n) { }
        // RVA: 0x0B26EFAC  token: 0x6000528
        public static Unity.Mathematics.double3 reflect(Unity.Mathematics.double3 i, Unity.Mathematics.double3 n) { }
        // RVA: 0x0B26EED4  token: 0x6000529
        public static Unity.Mathematics.double4 reflect(Unity.Mathematics.double4 i, Unity.Mathematics.double4 n) { }
        // RVA: 0x0B26F314  token: 0x600052A
        public static Unity.Mathematics.float2 refract(Unity.Mathematics.float2 i, Unity.Mathematics.float2 n, System.Single indexOfRefraction) { }
        // RVA: 0x05F306B0  token: 0x600052B
        public static Unity.Mathematics.float3 refract(Unity.Mathematics.float3 i, Unity.Mathematics.float3 n, System.Single indexOfRefraction) { }
        // RVA: 0x0B26F6DC  token: 0x600052C
        public static Unity.Mathematics.float4 refract(Unity.Mathematics.float4 i, Unity.Mathematics.float4 n, System.Single indexOfRefraction) { }
        // RVA: 0x0B26F418  token: 0x600052D
        public static Unity.Mathematics.double2 refract(Unity.Mathematics.double2 i, Unity.Mathematics.double2 n, System.Double indexOfRefraction) { }
        // RVA: 0x0B26F52C  token: 0x600052E
        public static Unity.Mathematics.double3 refract(Unity.Mathematics.double3 i, Unity.Mathematics.double3 n, System.Double indexOfRefraction) { }
        // RVA: 0x0B26F1BC  token: 0x600052F
        public static Unity.Mathematics.double4 refract(Unity.Mathematics.double4 i, Unity.Mathematics.double4 n, System.Double indexOfRefraction) { }
        // RVA: 0x0B26E3E0  token: 0x6000530
        public static Unity.Mathematics.float2 project(Unity.Mathematics.float2 a, Unity.Mathematics.float2 ontoB) { }
        // RVA: 0x05F2F6F0  token: 0x6000531
        public static Unity.Mathematics.float3 project(Unity.Mathematics.float3 a, Unity.Mathematics.float3 ontoB) { }
        // RVA: 0x0B26E578  token: 0x6000532
        public static Unity.Mathematics.float4 project(Unity.Mathematics.float4 a, Unity.Mathematics.float4 ontoB) { }
        // RVA: 0x0B26E650  token: 0x6000533
        public static Unity.Mathematics.float2 projectsafe(Unity.Mathematics.float2 a, Unity.Mathematics.float2 ontoB, Unity.Mathematics.float2 defaultValue) { }
        // RVA: 0x05F2F9B0  token: 0x6000534
        public static Unity.Mathematics.float3 projectsafe(Unity.Mathematics.float3 a, Unity.Mathematics.float3 ontoB, Unity.Mathematics.float3 defaultValue) { }
        // RVA: 0x0B26E5E8  token: 0x6000535
        public static Unity.Mathematics.float4 projectsafe(Unity.Mathematics.float4 a, Unity.Mathematics.float4 ontoB, Unity.Mathematics.float4 defaultValue) { }
        // RVA: 0x05F2F67C  token: 0x6000536
        public static Unity.Mathematics.double2 project(Unity.Mathematics.double2 a, Unity.Mathematics.double2 ontoB) { }
        // RVA: 0x0B26E4CC  token: 0x6000537
        public static Unity.Mathematics.double3 project(Unity.Mathematics.double3 a, Unity.Mathematics.double3 ontoB) { }
        // RVA: 0x0B26E42C  token: 0x6000538
        public static Unity.Mathematics.double4 project(Unity.Mathematics.double4 a, Unity.Mathematics.double4 ontoB) { }
        // RVA: 0x0B26E78C  token: 0x6000539
        public static Unity.Mathematics.double2 projectsafe(Unity.Mathematics.double2 a, Unity.Mathematics.double2 ontoB, Unity.Mathematics.double2 defaultValue) { }
        // RVA: 0x0B26E7F0  token: 0x600053A
        public static Unity.Mathematics.double3 projectsafe(Unity.Mathematics.double3 a, Unity.Mathematics.double3 ontoB, Unity.Mathematics.double3 defaultValue) { }
        // RVA: 0x0B26E6F0  token: 0x600053B
        public static Unity.Mathematics.double4 projectsafe(Unity.Mathematics.double4 a, Unity.Mathematics.double4 ontoB, Unity.Mathematics.double4 defaultValue) { }
        // RVA: 0x0B250B80  token: 0x600053C
        public static Unity.Mathematics.float2 faceforward(Unity.Mathematics.float2 n, Unity.Mathematics.float2 i, Unity.Mathematics.float2 ng) { }
        // RVA: 0x05F0D468  token: 0x600053D
        public static Unity.Mathematics.float3 faceforward(Unity.Mathematics.float3 n, Unity.Mathematics.float3 i, Unity.Mathematics.float3 ng) { }
        // RVA: 0x0B250AA4  token: 0x600053E
        public static Unity.Mathematics.float4 faceforward(Unity.Mathematics.float4 n, Unity.Mathematics.float4 i, Unity.Mathematics.float4 ng) { }
        // RVA: 0x0B250B0C  token: 0x600053F
        public static Unity.Mathematics.double2 faceforward(Unity.Mathematics.double2 n, Unity.Mathematics.double2 i, Unity.Mathematics.double2 ng) { }
        // RVA: 0x0B250BD8  token: 0x6000540
        public static Unity.Mathematics.double3 faceforward(Unity.Mathematics.double3 n, Unity.Mathematics.double3 i, Unity.Mathematics.double3 ng) { }
        // RVA: 0x0B250C9C  token: 0x6000541
        public static Unity.Mathematics.double4 faceforward(Unity.Mathematics.double4 n, Unity.Mathematics.double4 i, Unity.Mathematics.double4 ng) { }
        // RVA: 0x04DB1BE0  token: 0x6000542
        public static System.Void sincos(System.Single x, System.Single& s, System.Single& c) { }
        // RVA: 0x0B272B5C  token: 0x6000543
        public static System.Void sincos(Unity.Mathematics.float2 x, Unity.Mathematics.float2& s, Unity.Mathematics.float2& c) { }
        // RVA: 0x0B27297C  token: 0x6000544
        public static System.Void sincos(Unity.Mathematics.float3 x, Unity.Mathematics.float3& s, Unity.Mathematics.float3& c) { }
        // RVA: 0x0B272A00  token: 0x6000545
        public static System.Void sincos(Unity.Mathematics.float4 x, Unity.Mathematics.float4& s, Unity.Mathematics.float4& c) { }
        // RVA: 0x04DB1BA0  token: 0x6000546
        public static System.Void sincos(System.Double x, System.Double& s, System.Double& c) { }
        // RVA: 0x0B272A68  token: 0x6000547
        public static System.Void sincos(Unity.Mathematics.double2 x, Unity.Mathematics.double2& s, Unity.Mathematics.double2& c) { }
        // RVA: 0x0B272AD0  token: 0x6000548
        public static System.Void sincos(Unity.Mathematics.double3 x, Unity.Mathematics.double3& s, Unity.Mathematics.double3& c) { }
        // RVA: 0x0B2728F8  token: 0x6000549
        public static System.Void sincos(Unity.Mathematics.double4 x, Unity.Mathematics.double4& s, Unity.Mathematics.double4& c) { }
        // RVA: 0x04DADFA0  token: 0x600054A
        public static System.Int32 countbits(System.Int32 x) { }
        // RVA: 0x0B24DB28  token: 0x600054B
        public static Unity.Mathematics.int2 countbits(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B24DE2C  token: 0x600054C
        public static Unity.Mathematics.int3 countbits(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24DDCC  token: 0x600054D
        public static Unity.Mathematics.int4 countbits(Unity.Mathematics.int4 x) { }
        // RVA: 0x04DADFA0  token: 0x600054E
        public static System.Int32 countbits(System.UInt32 x) { }
        // RVA: 0x0B24DB40  token: 0x600054F
        public static Unity.Mathematics.int2 countbits(Unity.Mathematics.uint2 x) { }
        // RVA: 0x05F0A204  token: 0x6000550
        public static Unity.Mathematics.int3 countbits(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24DC3C  token: 0x6000551
        public static Unity.Mathematics.int4 countbits(Unity.Mathematics.uint4 x) { }
        // RVA: 0x04DADF40  token: 0x6000552
        public static System.Int32 countbits(System.UInt64 x) { }
        // RVA: 0x04DADF40  token: 0x6000553
        public static System.Int32 countbits(System.Int64 x) { }
        // RVA: 0x0B25E12C  token: 0x6000554
        public static System.Int32 lzcnt(System.Int32 x) { }
        // RVA: 0x0B25E0F8  token: 0x6000555
        public static Unity.Mathematics.int2 lzcnt(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B25E028  token: 0x6000556
        public static Unity.Mathematics.int3 lzcnt(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B25E0AC  token: 0x6000557
        public static Unity.Mathematics.int4 lzcnt(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B25E068  token: 0x6000558
        public static System.Int32 lzcnt(System.UInt32 x) { }
        // RVA: 0x0B25E0F8  token: 0x6000559
        public static Unity.Mathematics.int2 lzcnt(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B25E028  token: 0x600055A
        public static Unity.Mathematics.int3 lzcnt(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B25E0AC  token: 0x600055B
        public static Unity.Mathematics.int4 lzcnt(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B25E124  token: 0x600055C
        public static System.Int32 lzcnt(System.Int64 x) { }
        // RVA: 0x0B25E134  token: 0x600055D
        public static System.Int32 lzcnt(System.UInt64 x) { }
        // RVA: 0x0B275450  token: 0x600055E
        public static System.Int32 tzcnt(System.Int32 x) { }
        // RVA: 0x0B2753D0  token: 0x600055F
        public static Unity.Mathematics.int2 tzcnt(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B27538C  token: 0x6000560
        public static Unity.Mathematics.int3 tzcnt(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B2753FC  token: 0x6000561
        public static Unity.Mathematics.int4 tzcnt(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B275344  token: 0x6000562
        public static System.Int32 tzcnt(System.UInt32 x) { }
        // RVA: 0x0B2753D0  token: 0x6000563
        public static Unity.Mathematics.int2 tzcnt(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B27538C  token: 0x6000564
        public static Unity.Mathematics.int3 tzcnt(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B2753FC  token: 0x6000565
        public static Unity.Mathematics.int4 tzcnt(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B275458  token: 0x6000566
        public static System.Int32 tzcnt(System.Int64 x) { }
        // RVA: 0x0B275460  token: 0x6000567
        public static System.Int32 tzcnt(System.UInt64 x) { }
        // RVA: 0x0B270078  token: 0x6000568
        public static System.Int32 reversebits(System.Int32 x) { }
        // RVA: 0x0B270080  token: 0x6000569
        public static Unity.Mathematics.int2 reversebits(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B2700B0  token: 0x600056A
        public static Unity.Mathematics.int3 reversebits(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B26FE50  token: 0x600056B
        public static Unity.Mathematics.int4 reversebits(Unity.Mathematics.int4 x) { }
        // RVA: 0x04DB1910  token: 0x600056C
        public static System.UInt32 reversebits(System.UInt32 x) { }
        // RVA: 0x0B26FEC4  token: 0x600056D
        public static Unity.Mathematics.uint2 reversebits(Unity.Mathematics.uint2 x) { }
        // RVA: 0x05F3111C  token: 0x600056E
        public static Unity.Mathematics.uint3 reversebits(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B26FB98  token: 0x600056F
        public static Unity.Mathematics.uint4 reversebits(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B270070  token: 0x6000570
        public static System.Int64 reversebits(System.Int64 x) { }
        // RVA: 0x04DB1950  token: 0x6000571
        public static System.UInt64 reversebits(System.UInt64 x) { }
        // RVA: 0x04D882E0  token: 0x6000572
        public static System.Int32 rol(System.Int32 x, System.Int32 n) { }
        // RVA: 0x0B2702D0  token: 0x6000573
        public static Unity.Mathematics.int2 rol(Unity.Mathematics.int2 x, System.Int32 n) { }
        // RVA: 0x0B270164  token: 0x6000574
        public static Unity.Mathematics.int3 rol(Unity.Mathematics.int3 x, System.Int32 n) { }
        // RVA: 0x0B2701E4  token: 0x6000575
        public static Unity.Mathematics.int4 rol(Unity.Mathematics.int4 x, System.Int32 n) { }
        // RVA: 0x04D882E0  token: 0x6000576
        public static System.UInt32 rol(System.UInt32 x, System.Int32 n) { }
        // RVA: 0x0B270130  token: 0x6000577
        public static Unity.Mathematics.uint2 rol(Unity.Mathematics.uint2 x, System.Int32 n) { }
        // RVA: 0x05F316C8  token: 0x6000578
        public static Unity.Mathematics.uint3 rol(Unity.Mathematics.uint3 x, System.Int32 n) { }
        // RVA: 0x0B270258  token: 0x6000579
        public static Unity.Mathematics.uint4 rol(Unity.Mathematics.uint4 x, System.Int32 n) { }
        // RVA: 0x04DAB5C0  token: 0x600057A
        public static System.Int64 rol(System.Int64 x, System.Int32 n) { }
        // RVA: 0x04DAB5C0  token: 0x600057B
        public static System.UInt64 rol(System.UInt64 x, System.Int32 n) { }
        // RVA: 0x04DA4210  token: 0x600057C
        public static System.Int32 ror(System.Int32 x, System.Int32 n) { }
        // RVA: 0x0B2703E8  token: 0x600057D
        public static Unity.Mathematics.int2 ror(Unity.Mathematics.int2 x, System.Int32 n) { }
        // RVA: 0x0B270458  token: 0x600057E
        public static Unity.Mathematics.int3 ror(Unity.Mathematics.int3 x, System.Int32 n) { }
        // RVA: 0x0B270374  token: 0x600057F
        public static Unity.Mathematics.int4 ror(Unity.Mathematics.int4 x, System.Int32 n) { }
        // RVA: 0x04DA4210  token: 0x6000580
        public static System.UInt32 ror(System.UInt32 x, System.Int32 n) { }
        // RVA: 0x0B270340  token: 0x6000581
        public static Unity.Mathematics.uint2 ror(Unity.Mathematics.uint2 x, System.Int32 n) { }
        // RVA: 0x05F3175C  token: 0x6000582
        public static Unity.Mathematics.uint3 ror(Unity.Mathematics.uint3 x, System.Int32 n) { }
        // RVA: 0x0B2704D8  token: 0x6000583
        public static Unity.Mathematics.uint4 ror(Unity.Mathematics.uint4 x, System.Int32 n) { }
        // RVA: 0x04DA4260  token: 0x6000584
        public static System.Int64 ror(System.Int64 x, System.Int32 n) { }
        // RVA: 0x04DA4260  token: 0x6000585
        public static System.UInt64 ror(System.UInt64 x, System.Int32 n) { }
        // RVA: 0x037F6550  token: 0x6000586
        public static System.Int32 ceilpow2(System.Int32 x) { }
        // RVA: 0x0B24CBD0  token: 0x6000587
        public static Unity.Mathematics.int2 ceilpow2(Unity.Mathematics.int2 x) { }
        // RVA: 0x05F09228  token: 0x6000588
        public static Unity.Mathematics.int3 ceilpow2(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24CED0  token: 0x6000589
        public static Unity.Mathematics.int4 ceilpow2(Unity.Mathematics.int4 x) { }
        // RVA: 0x04D89420  token: 0x600058A
        public static System.UInt32 ceilpow2(System.UInt32 x) { }
        // RVA: 0x0B24CE18  token: 0x600058B
        public static Unity.Mathematics.uint2 ceilpow2(Unity.Mathematics.uint2 x) { }
        // RVA: 0x05F09014  token: 0x600058C
        public static Unity.Mathematics.uint3 ceilpow2(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24CC88  token: 0x600058D
        public static Unity.Mathematics.uint4 ceilpow2(Unity.Mathematics.uint4 x) { }
        // RVA: 0x04DADB10  token: 0x600058E
        public static System.Int64 ceilpow2(System.Int64 x) { }
        // RVA: 0x04DADAC0  token: 0x600058F
        public static System.UInt64 ceilpow2(System.UInt64 x) { }
        // RVA: 0x05F08FF8  token: 0x6000590
        public static System.Int32 ceillog2(System.Int32 x) { }
        // RVA: 0x0B24CBA4  token: 0x6000591
        public static Unity.Mathematics.int2 ceillog2(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B24CB24  token: 0x6000592
        public static Unity.Mathematics.int3 ceillog2(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24CB5C  token: 0x6000593
        public static Unity.Mathematics.int4 ceillog2(Unity.Mathematics.int4 x) { }
        // RVA: 0x05F08FF8  token: 0x6000594
        public static System.Int32 ceillog2(System.UInt32 x) { }
        // RVA: 0x0B24CBA4  token: 0x6000595
        public static Unity.Mathematics.int2 ceillog2(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B24CB24  token: 0x6000596
        public static Unity.Mathematics.int3 ceillog2(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24CB5C  token: 0x6000597
        public static Unity.Mathematics.int4 ceillog2(Unity.Mathematics.uint4 x) { }
        // RVA: 0x05F0E0A0  token: 0x6000598
        public static System.Int32 floorlog2(System.Int32 x) { }
        // RVA: 0x0B2530D0  token: 0x6000599
        public static Unity.Mathematics.int2 floorlog2(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B253098  token: 0x600059A
        public static Unity.Mathematics.int3 floorlog2(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B253050  token: 0x600059B
        public static Unity.Mathematics.int4 floorlog2(Unity.Mathematics.int4 x) { }
        // RVA: 0x05F0E0A0  token: 0x600059C
        public static System.Int32 floorlog2(System.UInt32 x) { }
        // RVA: 0x0B2530D0  token: 0x600059D
        public static Unity.Mathematics.int2 floorlog2(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B253098  token: 0x600059E
        public static Unity.Mathematics.int3 floorlog2(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B253050  token: 0x600059F
        public static Unity.Mathematics.int4 floorlog2(Unity.Mathematics.uint4 x) { }
        // RVA: 0x04DB18F0  token: 0x60005A0
        public static System.Single radians(System.Single x) { }
        // RVA: 0x0B26EC04  token: 0x60005A1
        public static Unity.Mathematics.float2 radians(Unity.Mathematics.float2 x) { }
        // RVA: 0x05390E8C  token: 0x60005A2
        public static Unity.Mathematics.float3 radians(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B26EBCC  token: 0x60005A3
        public static Unity.Mathematics.float4 radians(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DB1900  token: 0x60005A4
        public static System.Double radians(System.Double x) { }
        // RVA: 0x0B26EC14  token: 0x60005A5
        public static Unity.Mathematics.double2 radians(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B26EC4C  token: 0x60005A6
        public static Unity.Mathematics.double3 radians(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B26EC98  token: 0x60005A7
        public static Unity.Mathematics.double4 radians(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DAE090  token: 0x60005A8
        public static System.Single degrees(System.Single x) { }
        // RVA: 0x0B24E230  token: 0x60005A9
        public static Unity.Mathematics.float2 degrees(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F0A954  token: 0x60005AA
        public static Unity.Mathematics.float3 degrees(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B24E240  token: 0x60005AB
        public static Unity.Mathematics.float4 degrees(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DAE0A0  token: 0x60005AC
        public static System.Double degrees(System.Double x) { }
        // RVA: 0x0B24E278  token: 0x60005AD
        public static Unity.Mathematics.double2 degrees(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24E19C  token: 0x60005AE
        public static Unity.Mathematics.double3 degrees(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24E1E8  token: 0x60005AF
        public static Unity.Mathematics.double4 degrees(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24D91C  token: 0x60005B0
        public static System.Int32 cmin(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B24D7F4  token: 0x60005B1
        public static System.Int32 cmin(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24D874  token: 0x60005B2
        public static System.Int32 cmin(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B24D8B0  token: 0x60005B3
        public static System.UInt32 cmin(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B24D968  token: 0x60005B4
        public static System.UInt32 cmin(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24D808  token: 0x60005B5
        public static System.UInt32 cmin(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24D8F8  token: 0x60005B6
        public static System.Single cmin(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B24D8C4  token: 0x60005B7
        public static System.Single cmin(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F09F28  token: 0x60005B8
        public static System.Single cmin(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24D890  token: 0x60005B9
        public static System.Double cmin(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24D930  token: 0x60005BA
        public static System.Double cmin(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24D824  token: 0x60005BB
        public static System.Double cmin(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B24D774  token: 0x60005BC
        public static System.Int32 cmax(Unity.Mathematics.int2 x) { }
        // RVA: 0x0B24D7A8  token: 0x60005BD
        public static System.Int32 cmax(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B24D6D8  token: 0x60005BE
        public static System.Int32 cmax(Unity.Mathematics.int4 x) { }
        // RVA: 0x0B24D6A0  token: 0x60005BF
        public static System.UInt32 cmax(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B24D760  token: 0x60005C0
        public static System.UInt32 cmax(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B24D6F4  token: 0x60005C1
        public static System.UInt32 cmax(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24D6B4  token: 0x60005C2
        public static System.Single cmax(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B24D66C  token: 0x60005C3
        public static System.Single cmax(Unity.Mathematics.float3 x) { }
        // RVA: 0x05F09DB8  token: 0x60005C4
        public static System.Single cmax(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B24D788  token: 0x60005C5
        public static System.Double cmax(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B24D7BC  token: 0x60005C6
        public static System.Double cmax(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B24D710  token: 0x60005C7
        public static System.Double cmax(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DAE000  token: 0x60005C8
        public static System.Int32 csum(Unity.Mathematics.int2 x) { }
        // RVA: 0x04DAE050  token: 0x60005C9
        public static System.Int32 csum(Unity.Mathematics.int3 x) { }
        // RVA: 0x04D8CAC0  token: 0x60005CA
        public static System.Int32 csum(Unity.Mathematics.int4 x) { }
        // RVA: 0x04DAE000  token: 0x60005CB
        public static System.UInt32 csum(Unity.Mathematics.uint2 x) { }
        // RVA: 0x04DAE050  token: 0x60005CC
        public static System.UInt32 csum(Unity.Mathematics.uint3 x) { }
        // RVA: 0x04D8CAC0  token: 0x60005CD
        public static System.UInt32 csum(Unity.Mathematics.uint4 x) { }
        // RVA: 0x04DAE060  token: 0x60005CE
        public static System.Single csum(Unity.Mathematics.float2 x) { }
        // RVA: 0x04DAE080  token: 0x60005CF
        public static System.Single csum(Unity.Mathematics.float3 x) { }
        // RVA: 0x04DADFE0  token: 0x60005D0
        public static System.Single csum(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DAE020  token: 0x60005D1
        public static System.Double csum(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAE010  token: 0x60005D2
        public static System.Double csum(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAE030  token: 0x60005D3
        public static System.Double csum(Unity.Mathematics.double4 x) { }
        // RVA: 0x04D886E0  token: 0x60005D4
        public static System.Single square(System.Single x) { }
        // RVA: 0x0B2736F8  token: 0x60005D5
        public static Unity.Mathematics.float2 square(Unity.Mathematics.float2 x) { }
        // RVA: 0x05F36644  token: 0x60005D6
        public static Unity.Mathematics.float3 square(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B273628  token: 0x60005D7
        public static Unity.Mathematics.float4 square(Unity.Mathematics.float4 x) { }
        // RVA: 0x04DB05C0  token: 0x60005D8
        public static System.Double square(System.Double x) { }
        // RVA: 0x0B2735F0  token: 0x60005D9
        public static Unity.Mathematics.double2 square(Unity.Mathematics.double2 x) { }
        // RVA: 0x0B273668  token: 0x60005DA
        public static Unity.Mathematics.double3 square(Unity.Mathematics.double3 x) { }
        // RVA: 0x0B2735A0  token: 0x60005DB
        public static Unity.Mathematics.double4 square(Unity.Mathematics.double4 x) { }
        // RVA: 0x04DB2190  token: 0x60005DC
        public static System.Int32 square(System.Int32 x) { }
        // RVA: 0x0B273660  token: 0x60005DD
        public static Unity.Mathematics.int2 square(Unity.Mathematics.int2 x) { }
        // RVA: 0x05F36694  token: 0x60005DE
        public static Unity.Mathematics.int3 square(Unity.Mathematics.int3 x) { }
        // RVA: 0x0B2736C0  token: 0x60005DF
        public static Unity.Mathematics.int4 square(Unity.Mathematics.int4 x) { }
        // RVA: 0x04DB2190  token: 0x60005E0
        public static System.UInt32 square(System.UInt32 x) { }
        // RVA: 0x0B273660  token: 0x60005E1
        public static Unity.Mathematics.uint2 square(Unity.Mathematics.uint2 x) { }
        // RVA: 0x05F36694  token: 0x60005E2
        public static Unity.Mathematics.uint3 square(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B2736C0  token: 0x60005E3
        public static Unity.Mathematics.uint4 square(Unity.Mathematics.uint4 x) { }
        // RVA: 0x0B24A118  token: 0x60005E4
        public static System.Int32 compress(System.Int32* output, System.Int32 index, Unity.Mathematics.int4 val, Unity.Mathematics.bool4 mask) { }
        // RVA: 0x0B24D97C  token: 0x60005E5
        public static System.Int32 compress(System.UInt32* output, System.Int32 index, Unity.Mathematics.uint4 val, Unity.Mathematics.bool4 mask) { }
        // RVA: 0x0B24D97C  token: 0x60005E6
        public static System.Int32 compress(System.Single* output, System.Int32 index, Unity.Mathematics.float4 val, Unity.Mathematics.bool4 mask) { }
        // RVA: 0x05F0C4C4  token: 0x60005E7
        public static System.Single f16tof32(System.UInt32 x) { }
        // RVA: 0x0B250034  token: 0x60005E8
        public static Unity.Mathematics.float2 f16tof32(Unity.Mathematics.uint2 x) { }
        // RVA: 0x0B24FD70  token: 0x60005E9
        public static Unity.Mathematics.float3 f16tof32(Unity.Mathematics.uint3 x) { }
        // RVA: 0x0B250188  token: 0x60005EA
        public static Unity.Mathematics.float4 f16tof32(Unity.Mathematics.uint4 x) { }
        // RVA: 0x05F0CFFC  token: 0x60005EB
        public static System.UInt32 f32tof16(System.Single x) { }
        // RVA: 0x0B2503B4  token: 0x60005EC
        public static Unity.Mathematics.uint2 f32tof16(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B250540  token: 0x60005ED
        public static Unity.Mathematics.uint3 f32tof16(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B250838  token: 0x60005EE
        public static Unity.Mathematics.uint4 f32tof16(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B26DDDC  token: 0x60005EF
        public static System.Void orthonormal_basis(Unity.Mathematics.float3 normal, Unity.Mathematics.float3& basis1, Unity.Mathematics.float3& basis2) { }
        // RVA: 0x0B26DE80  token: 0x60005F0
        public static System.Void orthonormal_basis(Unity.Mathematics.double3 normal, Unity.Mathematics.double3& basis1, Unity.Mathematics.double3& basis2) { }
        // RVA: 0x04DADB60  token: 0x60005F1
        public static System.Single chgsign(System.Single x, System.Single y) { }
        // RVA: 0x0B24D060  token: 0x60005F2
        public static Unity.Mathematics.float2 chgsign(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y) { }
        // RVA: 0x05F0975C  token: 0x60005F3
        public static Unity.Mathematics.float3 chgsign(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y) { }
        // RVA: 0x0B24D0A4  token: 0x60005F4
        public static Unity.Mathematics.float4 chgsign(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y) { }
        // RVA: 0x04DA43B0  token: 0x60005F5
        private static System.UInt32 read32_little_endian(System.Void* pBuffer) { }
        // RVA: 0x0B2565C0  token: 0x60005F6
        private static System.UInt32 hash_with_unaligned_loads(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed) { }
        // RVA: 0x0B2567DC  token: 0x60005F7
        private static System.UInt32 hash_without_unaligned_loads(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed) { }
        // RVA: 0x0B255458  token: 0x60005F8
        public static System.UInt32 hash(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed) { }
        // RVA: 0x04DB21A0  token: 0x60005F9
        public static Unity.Mathematics.float3 up() { }
        // RVA: 0x04DAF2B0  token: 0x60005FA
        public static Unity.Mathematics.float3 down() { }
        // RVA: 0x04DB0000  token: 0x60005FB
        public static Unity.Mathematics.float3 forward() { }
        // RVA: 0x04DAD560  token: 0x60005FC
        public static Unity.Mathematics.float3 back() { }
        // RVA: 0x04DB0550  token: 0x60005FD
        public static Unity.Mathematics.float3 left() { }
        // RVA: 0x04DB19B0  token: 0x60005FE
        public static Unity.Mathematics.float3 right() { }
        // RVA: 0x0B24A584  token: 0x60005FF
        public static Unity.Mathematics.float3 EulerXYZ(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24A908  token: 0x6000600
        public static Unity.Mathematics.float3 EulerXZY(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24AC60  token: 0x6000601
        public static Unity.Mathematics.float3 EulerYXZ(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24AFB4  token: 0x6000602
        public static Unity.Mathematics.float3 EulerYZX(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24B2F0  token: 0x6000603
        public static Unity.Mathematics.float3 EulerZXY(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24B628  token: 0x6000604
        public static Unity.Mathematics.float3 EulerZYX(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24B980  token: 0x6000605
        public static Unity.Mathematics.float3 Euler(Unity.Mathematics.quaternion q, Unity.Mathematics.math.RotationOrder order) { }
        // RVA: 0x0B25EEF8  token: 0x6000606
        public static Unity.Mathematics.float3x3 mulScale(Unity.Mathematics.float3x3 m, Unity.Mathematics.float3 s) { }
        // RVA: 0x0B271008  token: 0x6000607
        public static Unity.Mathematics.float3x3 scaleMul(Unity.Mathematics.float3 s, Unity.Mathematics.float3x3 m) { }
        // RVA: 0x0B276330  token: 0x6000608
        private static Unity.Mathematics.float4 unpacklo(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B27637C  token: 0x6000609
        private static Unity.Mathematics.double4 unpacklo(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B24A2B8  token: 0x600060A
        private static Unity.Mathematics.float4 unpackhi(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B2762C4  token: 0x600060B
        private static Unity.Mathematics.double4 unpackhi(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B25EE40  token: 0x600060C
        private static Unity.Mathematics.float4 movelh(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B25EE8C  token: 0x600060D
        private static Unity.Mathematics.double4 movelh(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B25ED88  token: 0x600060E
        private static Unity.Mathematics.float4 movehl(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B25EDD4  token: 0x600060F
        private static Unity.Mathematics.double4 movehl(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x04DAFFE0  token: 0x6000610
        private static System.UInt32 fold_to_uint(System.Double x) { }
        // RVA: 0x04DAFEA0  token: 0x6000611
        private static Unity.Mathematics.uint2 fold_to_uint(Unity.Mathematics.double2 x) { }
        // RVA: 0x04DAFF70  token: 0x6000612
        private static Unity.Mathematics.uint3 fold_to_uint(Unity.Mathematics.double3 x) { }
        // RVA: 0x04DAFEE0  token: 0x6000613
        private static Unity.Mathematics.uint4 fold_to_uint(Unity.Mathematics.double4 x) { }
        // RVA: 0x0B2521C0  token: 0x6000614
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.float4x4 f4x4) { }
        // RVA: 0x0B252234  token: 0x6000615
        public static Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.quaternion rotation) { }
        // RVA: 0x0B252E4C  token: 0x6000616
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B252F08  token: 0x6000617
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B252C14  token: 0x6000618
        public static Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.RigidTransform transform) { }
        // RVA: 0x0B26DF54  token: 0x6000619
        public static Unity.Mathematics.float3x3 orthonormalize(Unity.Mathematics.float3x3 i) { }
        // RVA: 0x0B26E8D4  token: 0x600061A
        public static Unity.Mathematics.float3x3 pseudoinverse(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x04DAEB20  token: 0x600061B
        public static System.Single mul(System.Single a, System.Single b) { }
        // RVA: 0x04D8B640  token: 0x600061C
        public static System.Single mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2 b) { }
        // RVA: 0x04DB0E40  token: 0x600061D
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x2 b) { }
        // RVA: 0x04DB1540  token: 0x600061E
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x3 b) { }
        // RVA: 0x04DB15E0  token: 0x600061F
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x4 b) { }
        // RVA: 0x04D8B7C0  token: 0x6000620
        public static System.Single mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x04DB0FD0  token: 0x6000621
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x2 b) { }
        // RVA: 0x04DB0F40  token: 0x6000622
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x3 b) { }
        // RVA: 0x04DB1370  token: 0x6000623
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x4 b) { }
        // RVA: 0x034C9990  token: 0x6000624
        public static System.Single mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x04DB14D0  token: 0x6000625
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x2 b) { }
        // RVA: 0x04DB0AA0  token: 0x6000626
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x3 b) { }
        // RVA: 0x04DB16F0  token: 0x6000627
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x4 b) { }
        // RVA: 0x0B2605A4  token: 0x6000628
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2 b) { }
        // RVA: 0x0B269AAC  token: 0x6000629
        public static Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x2 b) { }
        // RVA: 0x0B26A8B8  token: 0x600062A
        public static Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x3 b) { }
        // RVA: 0x0B25EFC0  token: 0x600062B
        public static Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x4 b) { }
        // RVA: 0x0B261160  token: 0x600062C
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x0B2617FC  token: 0x600062D
        public static Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x2 b) { }
        // RVA: 0x0B267D9C  token: 0x600062E
        public static Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x3 b) { }
        // RVA: 0x0B260184  token: 0x600062F
        public static Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x4 b) { }
        // RVA: 0x0B2692E4  token: 0x6000630
        public static Unity.Mathematics.float2 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B25F144  token: 0x6000631
        public static Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x2 b) { }
        // RVA: 0x0B267A68  token: 0x6000632
        public static Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x3 b) { }
        // RVA: 0x0B26829C  token: 0x6000633
        public static Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x4 b) { }
        // RVA: 0x0B262A84  token: 0x6000634
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2 b) { }
        // RVA: 0x0B26970C  token: 0x6000635
        public static Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x2 b) { }
        // RVA: 0x0B26CA28  token: 0x6000636
        public static Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x3 b) { }
        // RVA: 0x0B25F2DC  token: 0x6000637
        public static Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x4 b) { }
        // RVA: 0x0B26353C  token: 0x6000638
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x0B26B3DC  token: 0x6000639
        public static Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x2 b) { }
        // RVA: 0x0B25F918  token: 0x600063A
        public static Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x3 b) { }
        // RVA: 0x0B2662EC  token: 0x600063B
        public static Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x4 b) { }
        // RVA: 0x0B265A9C  token: 0x600063C
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B26C60C  token: 0x600063D
        public static Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x2 b) { }
        // RVA: 0x0B2653E0  token: 0x600063E
        public static Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x3 b) { }
        // RVA: 0x0B260808  token: 0x600063F
        public static Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x4 b) { }
        // RVA: 0x0B2670B0  token: 0x6000640
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2 b) { }
        // RVA: 0x0B267CB8  token: 0x6000641
        public static Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x2 b) { }
        // RVA: 0x0B264868  token: 0x6000642
        public static Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x3 b) { }
        // RVA: 0x0B266814  token: 0x6000643
        public static Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x4 b) { }
        // RVA: 0x0B269F60  token: 0x6000644
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x0B26A0CC  token: 0x6000645
        public static Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x2 b) { }
        // RVA: 0x0B268DCC  token: 0x6000646
        public static Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x3 b) { }
        // RVA: 0x0B26A598  token: 0x6000647
        public static Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x4 b) { }
        // RVA: 0x0B25F54C  token: 0x6000648
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4 b) { }
        // RVA: 0x0B261FF4  token: 0x6000649
        public static Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x2 b) { }
        // RVA: 0x0B268FFC  token: 0x600064A
        public static Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x3 b) { }
        // RVA: 0x0B26CC04  token: 0x600064B
        public static Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x4 b) { }
        // RVA: 0x04DAEAF0  token: 0x600064C
        public static System.Double mul(System.Double a, System.Double b) { }
        // RVA: 0x04DAEB60  token: 0x600064D
        public static System.Double mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2 b) { }
        // RVA: 0x04DB15A0  token: 0x600064E
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x2 b) { }
        // RVA: 0x04DB1030  token: 0x600064F
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x3 b) { }
        // RVA: 0x04DB1100  token: 0x6000650
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x4 b) { }
        // RVA: 0x04DAEB30  token: 0x6000651
        public static System.Double mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x04DB10A0  token: 0x6000652
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x2 b) { }
        // RVA: 0x04DB1180  token: 0x6000653
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x3 b) { }
        // RVA: 0x04DB0E80  token: 0x6000654
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x4 b) { }
        // RVA: 0x04DAEA80  token: 0x6000655
        public static System.Double mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x04DB1210  token: 0x6000656
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x2 b) { }
        // RVA: 0x04DB0C00  token: 0x6000657
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x3 b) { }
        // RVA: 0x04DB1800  token: 0x6000658
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x4 b) { }
        // RVA: 0x0B267240  token: 0x6000659
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2 b) { }
        // RVA: 0x0B265BE4  token: 0x600065A
        public static Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x2 b) { }
        // RVA: 0x0B267F58  token: 0x600065B
        public static Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x3 b) { }
        // RVA: 0x0B267664  token: 0x600065C
        public static Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x4 b) { }
        // RVA: 0x0B25F644  token: 0x600065D
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x0B2666B0  token: 0x600065E
        public static Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x2 b) { }
        // RVA: 0x0B26ADF4  token: 0x600065F
        public static Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x3 b) { }
        // RVA: 0x0B268828  token: 0x6000660
        public static Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x4 b) { }
        // RVA: 0x0B26BD6C  token: 0x6000661
        public static Unity.Mathematics.double2 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B260F80  token: 0x6000662
        public static Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x2 b) { }
        // RVA: 0x0B26B0F4  token: 0x6000663
        public static Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x3 b) { }
        // RVA: 0x0B2613CC  token: 0x6000664
        public static Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x4 b) { }
        // RVA: 0x0B266068  token: 0x6000665
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2 b) { }
        // RVA: 0x0B26C8B0  token: 0x6000666
        public static Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x2 b) { }
        // RVA: 0x0B26268C  token: 0x6000667
        public static Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x3 b) { }
        // RVA: 0x0B26D120  token: 0x6000668
        public static Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x4 b) { }
        // RVA: 0x0B269E4C  token: 0x6000669
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x0B262B20  token: 0x600066A
        public static Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x2 b) { }
        // RVA: 0x0B265CC0  token: 0x600066B
        public static Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x3 b) { }
        // RVA: 0x0B2621D8  token: 0x600066C
        public static Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x4 b) { }
        // RVA: 0x0B26407C  token: 0x600066D
        public static Unity.Mathematics.double3 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B2669FC  token: 0x600066E
        public static Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x2 b) { }
        // RVA: 0x0B26B744  token: 0x600066F
        public static Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x3 b) { }
        // RVA: 0x0B26BFC0  token: 0x6000670
        public static Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x4 b) { }
        // RVA: 0x0B268780  token: 0x6000671
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2 b) { }
        // RVA: 0x0B26B5D8  token: 0x6000672
        public static Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x2 b) { }
        // RVA: 0x0B269858  token: 0x6000673
        public static Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x3 b) { }
        // RVA: 0x0B26D448  token: 0x6000674
        public static Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x4 b) { }
        // RVA: 0x0B2657B8  token: 0x6000675
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3 b) { }
        // RVA: 0x0B26784C  token: 0x6000676
        public static Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x2 b) { }
        // RVA: 0x0B269390  token: 0x6000677
        public static Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x3 b) { }
        // RVA: 0x0B263020  token: 0x6000678
        public static Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x4 b) { }
        // RVA: 0x0B2603CC  token: 0x6000679
        public static Unity.Mathematics.double4 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4 b) { }
        // RVA: 0x0B267394  token: 0x600067A
        public static Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x2 b) { }
        // RVA: 0x0B2641F0  token: 0x600067B
        public static Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x3 b) { }
        // RVA: 0x0B263970  token: 0x600067C
        public static Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x4 b) { }
        // RVA: 0x04DAEA70  token: 0x600067D
        public static System.Int32 mul(System.Int32 a, System.Int32 b) { }
        // RVA: 0x04DAEB00  token: 0x600067E
        public static System.Int32 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2 b) { }
        // RVA: 0x04DB1440  token: 0x600067F
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x2 b) { }
        // RVA: 0x04DB0DE0  token: 0x6000680
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x3 b) { }
        // RVA: 0x04DB1290  token: 0x6000681
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x4 b) { }
        // RVA: 0x04D85D00  token: 0x6000682
        public static System.Int32 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3 b) { }
        // RVA: 0x04DB1480  token: 0x6000683
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x2 b) { }
        // RVA: 0x04DB0CC0  token: 0x6000684
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x3 b) { }
        // RVA: 0x04DB0D40  token: 0x6000685
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x4 b) { }
        // RVA: 0x04DAEAC0  token: 0x6000686
        public static System.Int32 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4 b) { }
        // RVA: 0x04DB1310  token: 0x6000687
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x2 b) { }
        // RVA: 0x04DB0B60  token: 0x6000688
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x3 b) { }
        // RVA: 0x04DB09E0  token: 0x6000689
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x4 b) { }
        // RVA: 0x0B26362C  token: 0x600068A
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2 b) { }
        // RVA: 0x0B26A018  token: 0x600068B
        public static Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x2 b) { }
        // RVA: 0x0B263F74  token: 0x600068C
        public static Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x3 b) { }
        // RVA: 0x0B25FBF4  token: 0x600068D
        public static Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x4 b) { }
        // RVA: 0x0B2658BC  token: 0x600068E
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3 b) { }
        // RVA: 0x0B267130  token: 0x600068F
        public static Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x2 b) { }
        // RVA: 0x0B263674  token: 0x6000690
        public static Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x3 b) { }
        // RVA: 0x0B260D94  token: 0x6000691
        public static Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x4 b) { }
        // RVA: 0x0B262D58  token: 0x6000692
        public static Unity.Mathematics.int2 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4 b) { }
        // RVA: 0x0B26380C  token: 0x6000693
        public static Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x2 b) { }
        // RVA: 0x0B2605F4  token: 0x6000694
        public static Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x3 b) { }
        // RVA: 0x0B268B48  token: 0x6000695
        public static Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x4 b) { }
        // RVA: 0x0B2634A4  token: 0x6000696
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2 b) { }
        // RVA: 0x0B26BC24  token: 0x6000697
        public static Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x2 b) { }
        // RVA: 0x0B264694  token: 0x6000698
        public static Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x3 b) { }
        // RVA: 0x0B26517C  token: 0x6000699
        public static Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x4 b) { }
        // RVA: 0x0B265090  token: 0x600069A
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3 b) { }
        // RVA: 0x0B2611DC  token: 0x600069B
        public static Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x2 b) { }
        // RVA: 0x0B269B80  token: 0x600069C
        public static Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x3 b) { }
        // RVA: 0x0B264CE4  token: 0x600069D
        public static Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x4 b) { }
        // RVA: 0x0B262E00  token: 0x600069E
        public static Unity.Mathematics.int3 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4 b) { }
        // RVA: 0x0B25FD60  token: 0x600069F
        public static Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x2 b) { }
        // RVA: 0x0B266CF0  token: 0x60006A0
        public static Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x3 b) { }
        // RVA: 0x0B261A88  token: 0x60006A1
        public static Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x4 b) { }
        // RVA: 0x0B26052C  token: 0x60006A2
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2 b) { }
        // RVA: 0x0B262F40  token: 0x60006A3
        public static Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x2 b) { }
        // RVA: 0x0B265938  token: 0x60006A4
        public static Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x3 b) { }
        // RVA: 0x0B2680CC  token: 0x60006A5
        public static Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x4 b) { }
        // RVA: 0x0B2600D0  token: 0x60006A6
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3 b) { }
        // RVA: 0x0B261930  token: 0x60006A7
        public static Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x2 b) { }
        // RVA: 0x0B25F6FC  token: 0x60006A8
        public static Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x3 b) { }
        // RVA: 0x0B2649DC  token: 0x60006A9
        public static Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x4 b) { }
        // RVA: 0x0B268690  token: 0x60006AA
        public static Unity.Mathematics.int4 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4 b) { }
        // RVA: 0x0B26611C  token: 0x60006AB
        public static Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x2 b) { }
        // RVA: 0x0B26A234  token: 0x60006AC
        public static Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x3 b) { }
        // RVA: 0x0B26A9E4  token: 0x60006AD
        public static Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x4 b) { }
        // RVA: 0x04DAEA70  token: 0x60006AE
        public static System.UInt32 mul(System.UInt32 a, System.UInt32 b) { }
        // RVA: 0x04DAEB00  token: 0x60006AF
        public static System.UInt32 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2 b) { }
        // RVA: 0x04DB1440  token: 0x60006B0
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x2 b) { }
        // RVA: 0x04DB0DE0  token: 0x60006B1
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x3 b) { }
        // RVA: 0x04DB1670  token: 0x60006B2
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x4 b) { }
        // RVA: 0x04D85D00  token: 0x60006B3
        public static System.UInt32 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3 b) { }
        // RVA: 0x04DB1480  token: 0x60006B4
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x2 b) { }
        // RVA: 0x04DB0CC0  token: 0x60006B5
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x3 b) { }
        // RVA: 0x04DB0D40  token: 0x60006B6
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x4 b) { }
        // RVA: 0x04DAEAC0  token: 0x60006B7
        public static System.UInt32 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4 b) { }
        // RVA: 0x04DB1310  token: 0x60006B8
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x2 b) { }
        // RVA: 0x04DB0B60  token: 0x60006B9
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x3 b) { }
        // RVA: 0x04DB09E0  token: 0x60006BA
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x4 b) { }
        // RVA: 0x0B26362C  token: 0x60006BB
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2 b) { }
        // RVA: 0x0B26A018  token: 0x60006BC
        public static Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x2 b) { }
        // RVA: 0x0B263F74  token: 0x60006BD
        public static Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x3 b) { }
        // RVA: 0x0B25FBF4  token: 0x60006BE
        public static Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x4 b) { }
        // RVA: 0x0B2658BC  token: 0x60006BF
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3 b) { }
        // RVA: 0x0B267130  token: 0x60006C0
        public static Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x2 b) { }
        // RVA: 0x0B263674  token: 0x60006C1
        public static Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x3 b) { }
        // RVA: 0x0B260D94  token: 0x60006C2
        public static Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x4 b) { }
        // RVA: 0x0B262D58  token: 0x60006C3
        public static Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4 b) { }
        // RVA: 0x0B26380C  token: 0x60006C4
        public static Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x2 b) { }
        // RVA: 0x0B2605F4  token: 0x60006C5
        public static Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x3 b) { }
        // RVA: 0x0B268B48  token: 0x60006C6
        public static Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x4 b) { }
        // RVA: 0x0B2634A4  token: 0x60006C7
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2 b) { }
        // RVA: 0x0B26BC24  token: 0x60006C8
        public static Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x2 b) { }
        // RVA: 0x0B264694  token: 0x60006C9
        public static Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x3 b) { }
        // RVA: 0x0B26517C  token: 0x60006CA
        public static Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x4 b) { }
        // RVA: 0x0B265090  token: 0x60006CB
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3 b) { }
        // RVA: 0x0B2611DC  token: 0x60006CC
        public static Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x2 b) { }
        // RVA: 0x0B269B80  token: 0x60006CD
        public static Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x3 b) { }
        // RVA: 0x0B264CE4  token: 0x60006CE
        public static Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x4 b) { }
        // RVA: 0x0B262E00  token: 0x60006CF
        public static Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4 b) { }
        // RVA: 0x0B25FD60  token: 0x60006D0
        public static Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x2 b) { }
        // RVA: 0x0B266CF0  token: 0x60006D1
        public static Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x3 b) { }
        // RVA: 0x0B261A88  token: 0x60006D2
        public static Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x4 b) { }
        // RVA: 0x0B26052C  token: 0x60006D3
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2 b) { }
        // RVA: 0x0B262F40  token: 0x60006D4
        public static Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x2 b) { }
        // RVA: 0x0B265938  token: 0x60006D5
        public static Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x3 b) { }
        // RVA: 0x0B2680CC  token: 0x60006D6
        public static Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x4 b) { }
        // RVA: 0x0B2600D0  token: 0x60006D7
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3 b) { }
        // RVA: 0x0B261930  token: 0x60006D8
        public static Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x2 b) { }
        // RVA: 0x0B25F6FC  token: 0x60006D9
        public static Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x3 b) { }
        // RVA: 0x0B2649DC  token: 0x60006DA
        public static Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x4 b) { }
        // RVA: 0x0B268690  token: 0x60006DB
        public static Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4 b) { }
        // RVA: 0x0B26611C  token: 0x60006DC
        public static Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x2 b) { }
        // RVA: 0x0B26A234  token: 0x60006DD
        public static Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x3 b) { }
        // RVA: 0x0B26A9E4  token: 0x60006DE
        public static Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x4 b) { }
        // RVA: 0x04DAF870  token: 0x60006DF
        public static Unity.Mathematics.quaternion quaternion(System.Single x, System.Single y, System.Single z, System.Single w) { }
        // RVA: 0x02B77630  token: 0x60006E0
        public static Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float4 value) { }
        // RVA: 0x0B26EB8C  token: 0x60006E1
        public static Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x0B26EB44  token: 0x60006E2
        public static Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B24D998  token: 0x60006E3
        public static Unity.Mathematics.quaternion conjugate(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B25CBA4  token: 0x60006E4
        public static Unity.Mathematics.quaternion inverse(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B24E6D4  token: 0x60006E5
        public static System.Single dot(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b) { }
        // RVA: 0x0B25D670  token: 0x60006E6
        public static System.Single length(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B25D6AC  token: 0x60006E7
        public static System.Single lengthsq(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B26D8B8  token: 0x60006E8
        public static Unity.Mathematics.quaternion normalize(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B26DC30  token: 0x60006E9
        public static Unity.Mathematics.quaternion normalizesafe(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B26DBA4  token: 0x60006EA
        public static Unity.Mathematics.quaternion normalizesafe(Unity.Mathematics.quaternion q, Unity.Mathematics.quaternion defaultvalue) { }
        // RVA: 0x05F39B9C  token: 0x60006EB
        public static Unity.Mathematics.quaternion unitexp(Unity.Mathematics.quaternion q) { }
        // RVA: 0x05F0C274  token: 0x60006EC
        public static Unity.Mathematics.quaternion exp(Unity.Mathematics.quaternion q) { }
        // RVA: 0x05F39CC0  token: 0x60006ED
        public static Unity.Mathematics.quaternion unitlog(Unity.Mathematics.quaternion q) { }
        // RVA: 0x05F17EAC  token: 0x60006EE
        public static Unity.Mathematics.quaternion log(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B2628F8  token: 0x60006EF
        public static Unity.Mathematics.quaternion mul(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b) { }
        // RVA: 0x0B26BE64  token: 0x60006F0
        public static Unity.Mathematics.float3 mul(Unity.Mathematics.quaternion q, Unity.Mathematics.float3 v) { }
        // RVA: 0x0B26BE64  token: 0x60006F1
        public static Unity.Mathematics.float3 rotate(Unity.Mathematics.quaternion q, Unity.Mathematics.float3 v) { }
        // RVA: 0x0B26D758  token: 0x60006F2
        public static Unity.Mathematics.quaternion nlerp(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2, System.Single t) { }
        // RVA: 0x0B272C48  token: 0x60006F3
        public static Unity.Mathematics.quaternion slerp(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2, System.Single t) { }
        // RVA: 0x0B24C184  token: 0x60006F4
        public static System.Single angle(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2) { }
        // RVA: 0x0B2707D8  token: 0x60006F5
        public static Unity.Mathematics.quaternion rotation(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x0B24BE4C  token: 0x60006F6
        private static Unity.Mathematics.float3x3 adj(Unity.Mathematics.float3x3 m, System.Single& det) { }
        // RVA: 0x0B24BD4C  token: 0x60006F7
        private static System.Boolean adjInverse(Unity.Mathematics.float3x3 m, Unity.Mathematics.float3x3& i, System.Single epsilon) { }
        // RVA: 0x0B256480  token: 0x60006F8
        public static System.UInt32 hash(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0B2590D8  token: 0x60006F9
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.quaternion q) { }
        // RVA: 0x066B1564  token: 0x60006FA
        public static Unity.Mathematics.float3 forward(Unity.Mathematics.quaternion q) { }
        // RVA: 0x04DAD020  token: 0x60006FB
        public static Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.quaternion rot, Unity.Mathematics.float3 pos) { }
        // RVA: 0x0B24BAAC  token: 0x60006FC
        public static Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B24BB08  token: 0x60006FD
        public static Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.float4x4 transform) { }
        // RVA: 0x0B25CC20  token: 0x60006FE
        public static Unity.Mathematics.RigidTransform inverse(Unity.Mathematics.RigidTransform t) { }
        // RVA: 0x0B26B024  token: 0x60006FF
        public static Unity.Mathematics.RigidTransform mul(Unity.Mathematics.RigidTransform a, Unity.Mathematics.RigidTransform b) { }
        // RVA: 0x0B25FFF4  token: 0x6000700
        public static Unity.Mathematics.float4 mul(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float4 pos) { }
        // RVA: 0x066B2E2C  token: 0x6000701
        public static Unity.Mathematics.float3 rotate(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float3 dir) { }
        // RVA: 0x0B273E64  token: 0x6000702
        public static Unity.Mathematics.float3 transform(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float3 pos) { }
        // RVA: 0x0B25657C  token: 0x6000703
        public static System.UInt32 hash(Unity.Mathematics.RigidTransform t) { }
        // RVA: 0x0B2578A8  token: 0x6000704
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.RigidTransform t) { }
        // RVA: 0x04DAD8A0  token: 0x6000705
        public static Unity.Mathematics.uint2 uint2(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x04DAB5F0  token: 0x6000706
        public static Unity.Mathematics.uint2 uint2(Unity.Mathematics.uint2 xy) { }
        // RVA: 0x04DB02A0  token: 0x6000707
        public static Unity.Mathematics.uint2 uint2(System.UInt32 v) { }
        // RVA: 0x0B25A394  token: 0x6000708
        public static Unity.Mathematics.uint2 uint2(System.Boolean v) { }
        // RVA: 0x0B25A3AC  token: 0x6000709
        public static Unity.Mathematics.uint2 uint2(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB02A0  token: 0x600070A
        public static Unity.Mathematics.uint2 uint2(System.Int32 v) { }
        // RVA: 0x04DAB5F0  token: 0x600070B
        public static Unity.Mathematics.uint2 uint2(Unity.Mathematics.int2 v) { }
        // RVA: 0x0B275514  token: 0x600070C
        public static Unity.Mathematics.uint2 uint2(System.Single v) { }
        // RVA: 0x0B275538  token: 0x600070D
        public static Unity.Mathematics.uint2 uint2(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B2754F4  token: 0x600070E
        public static Unity.Mathematics.uint2 uint2(System.Double v) { }
        // RVA: 0x0B2754C4  token: 0x600070F
        public static Unity.Mathematics.uint2 uint2(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B254904  token: 0x6000710
        public static System.UInt32 hash(Unity.Mathematics.uint2 v) { }
        // RVA: 0x0B257570  token: 0x6000711
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2 v) { }
        // RVA: 0x05F345E8  token: 0x6000712
        public static System.UInt32 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F33804  token: 0x6000713
        public static Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F33D78  token: 0x6000714
        public static Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F34DD4  token: 0x6000715
        public static Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F328C0  token: 0x6000716
        private static System.UInt32 select_shuffle_component(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x02E515B0  token: 0x6000717
        public static Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1) { }
        // RVA: 0x0B25A428  token: 0x6000718
        public static Unity.Mathematics.uint2x2 uint2x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11) { }
        // RVA: 0x0B25A410  token: 0x6000719
        public static Unity.Mathematics.uint2x2 uint2x2(System.UInt32 v) { }
        // RVA: 0x0B25A4CC  token: 0x600071A
        public static Unity.Mathematics.uint2x2 uint2x2(System.Boolean v) { }
        // RVA: 0x0B25A3CC  token: 0x600071B
        public static Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x0B25A410  token: 0x600071C
        public static Unity.Mathematics.uint2x2 uint2x2(System.Int32 v) { }
        // RVA: 0x0B25A490  token: 0x600071D
        public static Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x0B27555C  token: 0x600071E
        public static Unity.Mathematics.uint2x2 uint2x2(System.Single v) { }
        // RVA: 0x0B275618  token: 0x600071F
        public static Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B275578  token: 0x6000720
        public static Unity.Mathematics.uint2x2 uint2x2(System.Double v) { }
        // RVA: 0x0B2755BC  token: 0x6000721
        public static Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B274334  token: 0x6000722
        public static Unity.Mathematics.uint2x2 transpose(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x0B255C44  token: 0x6000723
        public static System.UInt32 hash(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x0B257480  token: 0x6000724
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x04DAF5A0  token: 0x6000725
        public static Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2) { }
        // RVA: 0x05F164A8  token: 0x6000726
        public static Unity.Mathematics.uint2x3 uint2x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12) { }
        // RVA: 0x0B25A53C  token: 0x6000727
        public static Unity.Mathematics.uint2x3 uint2x3(System.UInt32 v) { }
        // RVA: 0x0B25A5F0  token: 0x6000728
        public static Unity.Mathematics.uint2x3 uint2x3(System.Boolean v) { }
        // RVA: 0x0B25A57C  token: 0x6000729
        public static Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x0B25A53C  token: 0x600072A
        public static Unity.Mathematics.uint2x3 uint2x3(System.Int32 v) { }
        // RVA: 0x0B25A504  token: 0x600072B
        public static Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x0B275698  token: 0x600072C
        public static Unity.Mathematics.uint2x3 uint2x3(System.Single v) { }
        // RVA: 0x0B275660  token: 0x600072D
        public static Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B275640  token: 0x600072E
        public static Unity.Mathematics.uint2x3 uint2x3(System.Double v) { }
        // RVA: 0x0B2756B8  token: 0x600072F
        public static Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B274F98  token: 0x6000730
        public static Unity.Mathematics.uint3x2 transpose(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x0B255160  token: 0x6000731
        public static System.UInt32 hash(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x0B25A0D0  token: 0x6000732
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x04DB02D0  token: 0x6000733
        public static Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2, Unity.Mathematics.uint2 c3) { }
        // RVA: 0x05F164E0  token: 0x6000734
        public static Unity.Mathematics.uint2x4 uint2x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13) { }
        // RVA: 0x0B25A66C  token: 0x6000735
        public static Unity.Mathematics.uint2x4 uint2x4(System.UInt32 v) { }
        // RVA: 0x0B25A6BC  token: 0x6000736
        public static Unity.Mathematics.uint2x4 uint2x4(System.Boolean v) { }
        // RVA: 0x0B25A648  token: 0x6000737
        public static Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x0B25A66C  token: 0x6000738
        public static Unity.Mathematics.uint2x4 uint2x4(System.Int32 v) { }
        // RVA: 0x0B25A688  token: 0x6000739
        public static Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x0B275728  token: 0x600073A
        public static Unity.Mathematics.uint2x4 uint2x4(System.Single v) { }
        // RVA: 0x0B2756F4  token: 0x600073B
        public static Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B275748  token: 0x600073C
        public static Unity.Mathematics.uint2x4 uint2x4(System.Double v) { }
        // RVA: 0x0B275764  token: 0x600073D
        public static Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B27454C  token: 0x600073E
        public static Unity.Mathematics.uint4x2 transpose(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x0B2540B8  token: 0x600073F
        public static System.UInt32 hash(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x0B259108  token: 0x6000740
        public static Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x04DAD8B0  token: 0x6000741
        public static Unity.Mathematics.uint3 uint3(System.UInt32 x, System.UInt32 y, System.UInt32 z) { }
        // RVA: 0x04DB0330  token: 0x6000742
        public static Unity.Mathematics.uint3 uint3(System.UInt32 x, Unity.Mathematics.uint2 yz) { }
        // RVA: 0x04DB0350  token: 0x6000743
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.uint2 xy, System.UInt32 z) { }
        // RVA: 0x04DB0370  token: 0x6000744
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.uint3 xyz) { }
        // RVA: 0x04DB02F0  token: 0x6000745
        public static Unity.Mathematics.uint3 uint3(System.UInt32 v) { }
        // RVA: 0x0B25A788  token: 0x6000746
        public static Unity.Mathematics.uint3 uint3(System.Boolean v) { }
        // RVA: 0x0B25A7A0  token: 0x6000747
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB02F0  token: 0x6000748
        public static Unity.Mathematics.uint3 uint3(System.Int32 v) { }
        // RVA: 0x04DB0370  token: 0x6000749
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.int3 v) { }
        // RVA: 0x0B275808  token: 0x600074A
        public static Unity.Mathematics.uint3 uint3(System.Single v) { }
        // RVA: 0x05F3B330  token: 0x600074B
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B2757E0  token: 0x600074C
        public static Unity.Mathematics.uint3 uint3(System.Double v) { }
        // RVA: 0x0B2757A8  token: 0x600074D
        public static Unity.Mathematics.uint3 uint3(Unity.Mathematics.double3 v) { }
        // RVA: 0x05F0F580  token: 0x600074E
        public static System.UInt32 hash(Unity.Mathematics.uint3 v) { }
        // RVA: 0x05F156A4  token: 0x600074F
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3 v) { }
        // RVA: 0x05F34FF4  token: 0x6000750
        public static System.UInt32 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x05F344F0  token: 0x6000751
        public static Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x05F34F18  token: 0x6000752
        public static Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x05F347E8  token: 0x6000753
        public static Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F33024  token: 0x6000754
        private static System.UInt32 select_shuffle_component(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAF7F0  token: 0x6000755
        public static Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1) { }
        // RVA: 0x05F16534  token: 0x6000756
        public static Unity.Mathematics.uint3x2 uint3x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21) { }
        // RVA: 0x0B25A884  token: 0x6000757
        public static Unity.Mathematics.uint3x2 uint3x2(System.UInt32 v) { }
        // RVA: 0x0B25A8A4  token: 0x6000758
        public static Unity.Mathematics.uint3x2 uint3x2(System.Boolean v) { }
        // RVA: 0x0B25A8C4  token: 0x6000759
        public static Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x0B25A884  token: 0x600075A
        public static Unity.Mathematics.uint3x2 uint3x2(System.Int32 v) { }
        // RVA: 0x0B25A814  token: 0x600075B
        public static Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x0B2758F8  token: 0x600075C
        public static Unity.Mathematics.uint3x2 uint3x2(System.Single v) { }
        // RVA: 0x0B2758C0  token: 0x600075D
        public static Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B275824  token: 0x600075E
        public static Unity.Mathematics.uint3x2 uint3x2(System.Double v) { }
        // RVA: 0x0B275884  token: 0x600075F
        public static Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B274370  token: 0x6000760
        public static Unity.Mathematics.uint2x3 transpose(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x0B25465C  token: 0x6000761
        public static System.UInt32 hash(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x0B258BF0  token: 0x6000762
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x034C99D0  token: 0x6000763
        public static Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2) { }
        // RVA: 0x05F165C4  token: 0x6000764
        public static Unity.Mathematics.uint3x3 uint3x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22) { }
        // RVA: 0x0B25A93C  token: 0x6000765
        public static Unity.Mathematics.uint3x3 uint3x3(System.UInt32 v) { }
        // RVA: 0x0B25AAB4  token: 0x6000766
        public static Unity.Mathematics.uint3x3 uint3x3(System.Boolean v) { }
        // RVA: 0x0B25AA74  token: 0x6000767
        public static Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x0B25A93C  token: 0x6000768
        public static Unity.Mathematics.uint3x3 uint3x3(System.Int32 v) { }
        // RVA: 0x0B25A97C  token: 0x6000769
        public static Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x0B275978  token: 0x600076A
        public static Unity.Mathematics.uint3x3 uint3x3(System.Single v) { }
        // RVA: 0x0B275938  token: 0x600076B
        public static Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B275918  token: 0x600076C
        public static Unity.Mathematics.uint3x3 uint3x3(System.Double v) { }
        // RVA: 0x0B27599C  token: 0x600076D
        public static Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B2748D4  token: 0x600076E
        public static Unity.Mathematics.uint3x3 transpose(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x0B255E3C  token: 0x600076F
        public static System.UInt32 hash(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x0B2592C8  token: 0x6000770
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x04DAF820  token: 0x6000771
        public static Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2, Unity.Mathematics.uint3 c3) { }
        // RVA: 0x05F16688  token: 0x6000772
        public static Unity.Mathematics.uint3x4 uint3x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23) { }
        // RVA: 0x0B25AB90  token: 0x6000773
        public static Unity.Mathematics.uint3x4 uint3x4(System.UInt32 v) { }
        // RVA: 0x0B25AAD4  token: 0x6000774
        public static Unity.Mathematics.uint3x4 uint3x4(System.Boolean v) { }
        // RVA: 0x0B25AB54  token: 0x6000775
        public static Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x0B25AB90  token: 0x6000776
        public static Unity.Mathematics.uint3x4 uint3x4(System.Int32 v) { }
        // RVA: 0x0B25AC34  token: 0x6000777
        public static Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x0B275AB0  token: 0x6000778
        public static Unity.Mathematics.uint3x4 uint3x4(System.Single v) { }
        // RVA: 0x0B275A6C  token: 0x6000779
        public static Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B2759F0  token: 0x600077A
        public static Unity.Mathematics.uint3x4 uint3x4(System.Double v) { }
        // RVA: 0x0B275A10  token: 0x600077B
        public static Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B27445C  token: 0x600077C
        public static Unity.Mathematics.uint4x3 transpose(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x0B255A78  token: 0x600077D
        public static System.UInt32 hash(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x0B259E00  token: 0x600077E
        public static Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x04DAD8C0  token: 0x600077F
        public static Unity.Mathematics.uint4 uint4(System.UInt32 x, System.UInt32 y, System.UInt32 z, System.UInt32 w) { }
        // RVA: 0x04DB04B0  token: 0x6000780
        public static Unity.Mathematics.uint4 uint4(System.UInt32 x, System.UInt32 y, Unity.Mathematics.uint2 zw) { }
        // RVA: 0x04DB0530  token: 0x6000781
        public static Unity.Mathematics.uint4 uint4(System.UInt32 x, Unity.Mathematics.uint2 yz, System.UInt32 w) { }
        // RVA: 0x04DB0430  token: 0x6000782
        public static Unity.Mathematics.uint4 uint4(System.UInt32 x, Unity.Mathematics.uint3 yzw) { }
        // RVA: 0x04DB04D0  token: 0x6000783
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint2 xy, System.UInt32 z, System.UInt32 w) { }
        // RVA: 0x04DAB610  token: 0x6000784
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint2 xy, Unity.Mathematics.uint2 zw) { }
        // RVA: 0x04DB04F0  token: 0x6000785
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint3 xyz, System.UInt32 w) { }
        // RVA: 0x0B25AC78  token: 0x6000786
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint4 xyzw) { }
        // RVA: 0x04DB0420  token: 0x6000787
        public static Unity.Mathematics.uint4 uint4(System.UInt32 v) { }
        // RVA: 0x0B25ACA4  token: 0x6000788
        public static Unity.Mathematics.uint4 uint4(System.Boolean v) { }
        // RVA: 0x0B25ACC4  token: 0x6000789
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB0420  token: 0x600078A
        public static Unity.Mathematics.uint4 uint4(System.Int32 v) { }
        // RVA: 0x0B25AC78  token: 0x600078B
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.int4 v) { }
        // RVA: 0x0B275B1C  token: 0x600078C
        public static Unity.Mathematics.uint4 uint4(System.Single v) { }
        // RVA: 0x0B275B64  token: 0x600078D
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B275B34  token: 0x600078E
        public static Unity.Mathematics.uint4 uint4(System.Double v) { }
        // RVA: 0x0B275AD4  token: 0x600078F
        public static Unity.Mathematics.uint4 uint4(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B253A04  token: 0x6000790
        public static System.UInt32 hash(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B257404  token: 0x6000791
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B271AE0  token: 0x6000792
        public static System.UInt32 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x) { }
        // RVA: 0x0B271660  token: 0x6000793
        public static Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y) { }
        // RVA: 0x0B271DF8  token: 0x6000794
        public static Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z) { }
        // RVA: 0x0B272138  token: 0x6000795
        public static Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w) { }
        // RVA: 0x05F33540  token: 0x6000796
        private static System.UInt32 select_shuffle_component(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4 b, Unity.Mathematics.math.ShuffleComponent component) { }
        // RVA: 0x04DAFA00  token: 0x6000797
        public static Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1) { }
        // RVA: 0x05F16750  token: 0x6000798
        public static Unity.Mathematics.uint4x2 uint4x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21, System.UInt32 m30, System.UInt32 m31) { }
        // RVA: 0x0B25ADCC  token: 0x6000799
        public static Unity.Mathematics.uint4x2 uint4x2(System.UInt32 v) { }
        // RVA: 0x0B275BAC  token: 0x600079A
        public static Unity.Mathematics.uint4x2 uint4x2(System.Boolean v) { }
        // RVA: 0x0B275B88  token: 0x600079B
        public static Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x0B25ADCC  token: 0x600079C
        public static Unity.Mathematics.uint4x2 uint4x2(System.Int32 v) { }
        // RVA: 0x0B25ADE8  token: 0x600079D
        public static Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x0B275BC8  token: 0x600079E
        public static Unity.Mathematics.uint4x2 uint4x2(System.Single v) { }
        // RVA: 0x0B275C2C  token: 0x600079F
        public static Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B275C60  token: 0x60007A0
        public static Unity.Mathematics.uint4x2 uint4x2(System.Double v) { }
        // RVA: 0x0B275BE8  token: 0x60007A1
        public static Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B2743BC  token: 0x60007A2
        public static Unity.Mathematics.uint2x4 transpose(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x0B25605C  token: 0x60007A3
        public static System.UInt32 hash(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x0B256A7C  token: 0x60007A4
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x04DAFA20  token: 0x60007A5
        public static Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2) { }
        // RVA: 0x05F16804  token: 0x60007A6
        public static Unity.Mathematics.uint4x3 uint4x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32) { }
        // RVA: 0x0B25AECC  token: 0x60007A7
        public static Unity.Mathematics.uint4x3 uint4x3(System.UInt32 v) { }
        // RVA: 0x0B275D9C  token: 0x60007A8
        public static Unity.Mathematics.uint4x3 uint4x3(System.Boolean v) { }
        // RVA: 0x0B275D60  token: 0x60007A9
        public static Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x0B25AECC  token: 0x60007AA
        public static Unity.Mathematics.uint4x3 uint4x3(System.Int32 v) { }
        // RVA: 0x0B25AFA8  token: 0x60007AB
        public static Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x0B275D1C  token: 0x60007AC
        public static Unity.Mathematics.uint4x3 uint4x3(System.Single v) { }
        // RVA: 0x0B275C7C  token: 0x60007AD
        public static Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B275D40  token: 0x60007AE
        public static Unity.Mathematics.uint4x3 uint4x3(System.Double v) { }
        // RVA: 0x0B275CC0  token: 0x60007AF
        public static Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B274770  token: 0x60007B0
        public static Unity.Mathematics.uint3x4 transpose(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x0B254D80  token: 0x60007B1
        public static System.UInt32 hash(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x0B258CE8  token: 0x60007B2
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x04DAFA40  token: 0x60007B3
        public static Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2, Unity.Mathematics.uint4 c3) { }
        // RVA: 0x05F169CC  token: 0x60007B4
        public static Unity.Mathematics.uint4x4 uint4x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32, System.UInt32 m33) { }
        // RVA: 0x0B25B0CC  token: 0x60007B5
        public static Unity.Mathematics.uint4x4 uint4x4(System.UInt32 v) { }
        // RVA: 0x0B275DBC  token: 0x60007B6
        public static Unity.Mathematics.uint4x4 uint4x4(System.Boolean v) { }
        // RVA: 0x0B275E3C  token: 0x60007B7
        public static Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x0B25B0CC  token: 0x60007B8
        public static Unity.Mathematics.uint4x4 uint4x4(System.Int32 v) { }
        // RVA: 0x0B25B14C  token: 0x60007B9
        public static Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x0B275F00  token: 0x60007BA
        public static Unity.Mathematics.uint4x4 uint4x4(System.Single v) { }
        // RVA: 0x0B275DE0  token: 0x60007BB
        public static Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B275F28  token: 0x60007BC
        public static Unity.Mathematics.uint4x4 uint4x4(System.Double v) { }
        // RVA: 0x0B275E74  token: 0x60007BD
        public static Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B274AF4  token: 0x60007BE
        public static Unity.Mathematics.uint4x4 transpose(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x0B254938  token: 0x60007BF
        public static System.UInt32 hash(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x0B2571C8  token: 0x60007C0
        public static Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x4 v) { }

    }

    // TypeToken: 0x2000008  // size: 0x12
    public sealed struct bool2 : System.IEquatable`1
    {
        // Fields
        public System.Boolean x;  // 0x10
        public System.Boolean y;  // 0x11

        // Methods
        // RVA: 0x02BBF3D0  token: 0x60007C1
        public System.Void .ctor(System.Boolean x, System.Boolean y) { }
        // RVA: 0x04DB21E0  token: 0x60007C2
        public System.Void .ctor(Unity.Mathematics.bool2 xy) { }
        // RVA: 0x04DB21F0  token: 0x60007C3
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x04DAD580  token: 0x60007C4
        public static Unity.Mathematics.bool2 op_Implicit(System.Boolean v) { }
        // RVA: 0x0B27CF60  token: 0x60007C5
        public virtual System.Boolean Equals(Unity.Mathematics.bool2 rhs) { }
        // RVA: 0x0B27CF74  token: 0x60007C6
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27CFE8  token: 0x60007C7
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D030  token: 0x60007C8
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct bool2x2 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool2 c0;  // 0x10
        public Unity.Mathematics.bool2 c1;  // 0x12

        // Methods
        // RVA: 0x04DA4480  token: 0x60007C9
        public System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1) { }
        // RVA: 0x04DB2230  token: 0x60007CA
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11) { }
        // RVA: 0x04DB2200  token: 0x60007CB
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B276A58  token: 0x60007CC
        public virtual System.Boolean Equals(Unity.Mathematics.bool2x2 rhs) { }
        // RVA: 0x0B27D0B8  token: 0x60007CD
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27D118  token: 0x60007CE
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D120  token: 0x60007CF
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000B  // size: 0x16
    public sealed struct bool2x3 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool2 c0;  // 0x10
        public Unity.Mathematics.bool2 c1;  // 0x12
        public Unity.Mathematics.bool2 c2;  // 0x14

        // Methods
        // RVA: 0x04DB22F0  token: 0x60007D0
        public System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2) { }
        // RVA: 0x04DB2260  token: 0x60007D1
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12) { }
        // RVA: 0x04DB22B0  token: 0x60007D2
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B276BE0  token: 0x60007D3
        public virtual System.Boolean Equals(Unity.Mathematics.bool2x3 rhs) { }
        // RVA: 0x0B27D27C  token: 0x60007D4
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27D2EC  token: 0x60007D5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D310  token: 0x60007D6
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public sealed struct bool2x4 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool2 c0;  // 0x10
        public Unity.Mathematics.bool2 c1;  // 0x12
        public Unity.Mathematics.bool2 c2;  // 0x14
        public Unity.Mathematics.bool2 c3;  // 0x16

        // Methods
        // RVA: 0x04DB23B0  token: 0x60007D7
        public System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2, Unity.Mathematics.bool2 c3) { }
        // RVA: 0x04DB2300  token: 0x60007D8
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13) { }
        // RVA: 0x04DB2360  token: 0x60007D9
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B276DE8  token: 0x60007DA
        public virtual System.Boolean Equals(Unity.Mathematics.bool2x4 rhs) { }
        // RVA: 0x0B27D4D8  token: 0x60007DB
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27D538  token: 0x60007DC
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D540  token: 0x60007DD
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000D  // size: 0x13
    public sealed struct bool3 : System.IEquatable`1
    {
        // Fields
        public System.Boolean x;  // 0x10
        public System.Boolean y;  // 0x11
        public System.Boolean z;  // 0x12

        // Properties
        System.Boolean Item { get; /* RVA: 0x03966740 */ set; /* RVA: 0x04D93080 */ }

        // Methods
        // RVA: 0x04DA0780  token: 0x60007DE
        public System.Void .ctor(System.Boolean x, System.Boolean y, System.Boolean z) { }
        // RVA: 0x04DB23E0  token: 0x60007DF
        public System.Void .ctor(System.Boolean x, Unity.Mathematics.bool2 yz) { }
        // RVA: 0x04DB23F0  token: 0x60007E0
        public System.Void .ctor(Unity.Mathematics.bool2 xy, System.Boolean z) { }
        // RVA: 0x04DB2400  token: 0x60007E1
        public System.Void .ctor(Unity.Mathematics.bool3 xyz) { }
        // RVA: 0x04DB23D0  token: 0x60007E2
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x04DAD620  token: 0x60007E3
        public static Unity.Mathematics.bool3 op_Implicit(System.Boolean v) { }
        // RVA: 0x03B885B0  token: 0x60007E4
        public static Unity.Mathematics.bool3 op_BitwiseAnd(Unity.Mathematics.bool3 lhs, Unity.Mathematics.bool3 rhs) { }
        // RVA: 0x0B27D8B8  token: 0x60007E5
        public static Unity.Mathematics.bool3 op_BitwiseOr(Unity.Mathematics.bool3 lhs, Unity.Mathematics.bool3 rhs) { }
        // RVA: 0x0B277070  token: 0x60007E8
        public virtual System.Boolean Equals(Unity.Mathematics.bool3 rhs) { }
        // RVA: 0x0B27D770  token: 0x60007E9
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27D7E0  token: 0x60007EA
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D804  token: 0x60007EB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000F  // size: 0x16
    public sealed struct bool3x2 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool3 c0;  // 0x10
        public Unity.Mathematics.bool3 c1;  // 0x13

        // Methods
        // RVA: 0x04DB2490  token: 0x60007EC
        public System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1) { }
        // RVA: 0x04DB2420  token: 0x60007ED
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21) { }
        // RVA: 0x04DB2460  token: 0x60007EE
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B27D958  token: 0x60007EF
        public virtual System.Boolean Equals(Unity.Mathematics.bool3x2 rhs) { }
        // RVA: 0x0B27D8E8  token: 0x60007F0
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27D9A4  token: 0x60007F1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27D9C8  token: 0x60007F2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000010  // size: 0x19
    public sealed struct bool3x3 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool3 c0;  // 0x10
        public Unity.Mathematics.bool3 c1;  // 0x13
        public Unity.Mathematics.bool3 c2;  // 0x16

        // Methods
        // RVA: 0x04DB2560  token: 0x60007F3
        public System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2) { }
        // RVA: 0x04DB24F0  token: 0x60007F4
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22) { }
        // RVA: 0x04DB24B0  token: 0x60007F5
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B27DC04  token: 0x60007F6
        public virtual System.Boolean Equals(Unity.Mathematics.bool3x3 rhs) { }
        // RVA: 0x0B27DB90  token: 0x60007F7
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27DC78  token: 0x60007F8
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27DCA0  token: 0x60007F9
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000011  // size: 0x1C
    public sealed struct bool3x4 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool3 c0;  // 0x10
        public Unity.Mathematics.bool3 c1;  // 0x13
        public Unity.Mathematics.bool3 c2;  // 0x16
        public Unity.Mathematics.bool3 c3;  // 0x19

        // Methods
        // RVA: 0x04DB2670  token: 0x60007FA
        public System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2, Unity.Mathematics.bool3 c3) { }
        // RVA: 0x04DB25E0  token: 0x60007FB
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23) { }
        // RVA: 0x04DB2590  token: 0x60007FC
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B27DF04  token: 0x60007FD
        public virtual System.Boolean Equals(Unity.Mathematics.bool3x4 rhs) { }
        // RVA: 0x0B27DF98  token: 0x60007FE
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27E00C  token: 0x60007FF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27E030  token: 0x6000800
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000012  // size: 0x14
    public sealed struct bool4 : System.IEquatable`1
    {
        // Fields
        public System.Boolean x;  // 0x10
        public System.Boolean y;  // 0x11
        public System.Boolean z;  // 0x12
        public System.Boolean w;  // 0x13

        // Properties
        System.Int32 Item { set; /* RVA: 0x04D93080 */ }

        // Methods
        // RVA: 0x04DAB240  token: 0x6000801
        public System.Void .ctor(System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w) { }
        // RVA: 0x04DB2790  token: 0x6000802
        public System.Void .ctor(System.Boolean x, System.Boolean y, Unity.Mathematics.bool2 zw) { }
        // RVA: 0x04DB26F0  token: 0x6000803
        public System.Void .ctor(System.Boolean x, Unity.Mathematics.bool2 yz, System.Boolean w) { }
        // RVA: 0x04DB2710  token: 0x6000804
        public System.Void .ctor(System.Boolean x, Unity.Mathematics.bool3 yzw) { }
        // RVA: 0x04DB2730  token: 0x6000805
        public System.Void .ctor(Unity.Mathematics.bool2 xy, System.Boolean z, System.Boolean w) { }
        // RVA: 0x04DB2750  token: 0x6000806
        public System.Void .ctor(Unity.Mathematics.bool2 xy, Unity.Mathematics.bool2 zw) { }
        // RVA: 0x04DB2770  token: 0x6000807
        public System.Void .ctor(Unity.Mathematics.bool3 xyz, System.Boolean w) { }
        // RVA: 0x04DB26C0  token: 0x6000808
        public System.Void .ctor(Unity.Mathematics.bool4 xyzw) { }
        // RVA: 0x04DB26E0  token: 0x6000809
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x04DAD7B0  token: 0x600080A
        public static Unity.Mathematics.bool4 op_Implicit(System.Boolean v) { }
        // RVA: 0x0B2779E4  token: 0x600080C
        public virtual System.Boolean Equals(Unity.Mathematics.bool4 rhs) { }
        // RVA: 0x0B27E330  token: 0x600080D
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27E390  token: 0x600080E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27E398  token: 0x600080F
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public sealed struct bool4x2 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool4 c0;  // 0x10
        public Unity.Mathematics.bool4 c1;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x6000810
        public System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1) { }
        // RVA: 0x04DB27E0  token: 0x6000811
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21, System.Boolean m30, System.Boolean m31) { }
        // RVA: 0x04DB27B0  token: 0x6000812
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B277B6C  token: 0x6000813
        public virtual System.Boolean Equals(Unity.Mathematics.bool4x2 rhs) { }
        // RVA: 0x0B27E4F4  token: 0x6000814
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27E554  token: 0x6000815
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27E55C  token: 0x6000816
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000015  // size: 0x1C
    public sealed struct bool4x3 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool4 c0;  // 0x10
        public Unity.Mathematics.bool4 c1;  // 0x14
        public Unity.Mathematics.bool4 c2;  // 0x18

        // Methods
        // RVA: 0x04D85D60  token: 0x6000817
        public System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2) { }
        // RVA: 0x04DB2830  token: 0x6000818
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m30, System.Boolean m31, System.Boolean m32) { }
        // RVA: 0x04DB28B0  token: 0x6000819
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B27E800  token: 0x600081A
        public virtual System.Boolean Equals(Unity.Mathematics.bool4x3 rhs) { }
        // RVA: 0x0B27E78C  token: 0x600081B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27E840  token: 0x600081C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27E864  token: 0x600081D
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000016  // size: 0x20
    public sealed struct bool4x4 : System.IEquatable`1
    {
        // Fields
        public Unity.Mathematics.bool4 c0;  // 0x10
        public Unity.Mathematics.bool4 c1;  // 0x14
        public Unity.Mathematics.bool4 c2;  // 0x18
        public Unity.Mathematics.bool4 c3;  // 0x1c

        // Methods
        // RVA: 0x04D88C10  token: 0x600081E
        public System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2, Unity.Mathematics.bool4 c3) { }
        // RVA: 0x04DB2900  token: 0x600081F
        public System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23, System.Boolean m30, System.Boolean m31, System.Boolean m32, System.Boolean m33) { }
        // RVA: 0x04DB29B0  token: 0x6000820
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B27EBD0  token: 0x6000821
        public virtual System.Boolean Equals(Unity.Mathematics.bool4x4 rhs) { }
        // RVA: 0x0B27EB64  token: 0x6000822
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27EC20  token: 0x6000823
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27EC3C  token: 0x6000824
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000017  // size: 0x20
    public sealed struct double2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Double x;  // 0x10
        public System.Double y;  // 0x18

        // Methods
        // RVA: 0x04D881B0  token: 0x6000825
        public System.Void .ctor(System.Double x, System.Double y) { }
        // RVA: 0x04D881A0  token: 0x6000826
        public System.Void .ctor(Unity.Mathematics.double2 xy) { }
        // RVA: 0x04DB2A60  token: 0x6000827
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B27F20C  token: 0x6000828
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F00990  token: 0x6000829
        public System.Void .ctor(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB2A90  token: 0x600082A
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB2A40  token: 0x600082B
        public System.Void .ctor(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DB2A70  token: 0x600082C
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB2AB0  token: 0x600082D
        public System.Void .ctor(Unity.Mathematics.uint2 v) { }
        // RVA: 0x0B27F1D0  token: 0x600082E
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B27F190  token: 0x600082F
        public System.Void .ctor(Unity.Mathematics.half2 v) { }
        // RVA: 0x04DB2AD0  token: 0x6000830
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB2A10  token: 0x6000831
        public System.Void .ctor(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DAECB0  token: 0x6000832
        public static Unity.Mathematics.double2 op_Implicit(System.Double v) { }
        // RVA: 0x04DAEC50  token: 0x6000833
        public static Unity.Mathematics.double2 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DAEBF0  token: 0x6000834
        public static Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DAEB90  token: 0x6000835
        public static Unity.Mathematics.double2 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DAEC20  token: 0x6000836
        public static Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.uint2 v) { }
        // RVA: 0x04DAEC70  token: 0x6000837
        public static Unity.Mathematics.double2 op_Implicit(System.Single v) { }
        // RVA: 0x04DAEBC0  token: 0x6000838
        public static Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DB2B10  token: 0x6000839
        public static Unity.Mathematics.double2 op_Multiply(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x04DB2B60  token: 0x600083A
        public static Unity.Mathematics.double2 op_Multiply(Unity.Mathematics.double2 lhs, System.Double rhs) { }
        // RVA: 0x04DB2B40  token: 0x600083B
        public static Unity.Mathematics.double2 op_Multiply(System.Double lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x04DB2AE0  token: 0x600083C
        public static Unity.Mathematics.double2 op_Addition(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x04DB2BA0  token: 0x600083D
        public static Unity.Mathematics.double2 op_Subtraction(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x04DB2B80  token: 0x600083E
        public static Unity.Mathematics.double2 op_Subtraction(System.Double lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x0B27F23C  token: 0x600083F
        public static Unity.Mathematics.double2 op_Division(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x0B27F220  token: 0x6000840
        public static Unity.Mathematics.double2 op_Division(System.Double lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x0B27F2B8  token: 0x6000841
        public static Unity.Mathematics.bool2 op_LessThan(Unity.Mathematics.double2 lhs, System.Double rhs) { }
        // RVA: 0x0B27F294  token: 0x6000842
        public static Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs) { }
        // RVA: 0x04DB2BD0  token: 0x6000843
        public static Unity.Mathematics.double2 op_UnaryNegation(Unity.Mathematics.double2 val) { }
        // RVA: 0x0B27F260  token: 0x6000844
        public static Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.double2 lhs, System.Double rhs) { }
        // RVA: 0x0B278530  token: 0x6000845
        public virtual System.Boolean Equals(Unity.Mathematics.double2 rhs) { }
        // RVA: 0x0B27F00C  token: 0x6000846
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B254EB0  token: 0x6000847
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27F100  token: 0x6000848
        public virtual System.String ToString() { }
        // RVA: 0x0B27F078  token: 0x6000849
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000019  // size: 0x30
    public sealed struct double2x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double2 c0;  // 0x10
        public Unity.Mathematics.double2 c1;  // 0x20
        public static readonly Unity.Mathematics.double2x2 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D8C810  token: 0x600084A
        public System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1) { }
        // RVA: 0x04DB2C40  token: 0x600084B
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11) { }
        // RVA: 0x04DB2C20  token: 0x600084C
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F00A78  token: 0x600084D
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F009F8  token: 0x600084E
        public System.Void .ctor(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x04DB2BF0  token: 0x600084F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB2D60  token: 0x6000850
        public System.Void .ctor(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x04DB2C60  token: 0x6000851
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB2CE0  token: 0x6000852
        public System.Void .ctor(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x04DB2D30  token: 0x6000853
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB2CA0  token: 0x6000854
        public System.Void .ctor(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B27F674  token: 0x6000855
        public static Unity.Mathematics.double2x2 op_Multiply(Unity.Mathematics.double2x2 lhs, System.Double rhs) { }
        // RVA: 0x0B27F2D4  token: 0x6000856
        public virtual System.Boolean Equals(Unity.Mathematics.double2x2 rhs) { }
        // RVA: 0x0B27F31C  token: 0x6000857
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27F390  token: 0x6000858
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27F3B4  token: 0x6000859
        public virtual System.String ToString() { }
        // RVA: 0x0B27F520  token: 0x600085A
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04D19290  token: 0x600085B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x40
    public sealed struct double2x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double2 c0;  // 0x10
        public Unity.Mathematics.double2 c1;  // 0x20
        public Unity.Mathematics.double2 c2;  // 0x30

        // Methods
        // RVA: 0x04DA0760  token: 0x600085C
        public System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2) { }
        // RVA: 0x04DB2F10  token: 0x600085D
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12) { }
        // RVA: 0x04DB2EA0  token: 0x600085E
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F00B04  token: 0x600085F
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F00B38  token: 0x6000860
        public System.Void .ctor(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x04DB3000  token: 0x6000861
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB2DB0  token: 0x6000862
        public System.Void .ctor(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x04DB2FA0  token: 0x6000863
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB2E20  token: 0x6000864
        public System.Void .ctor(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x04DB2EC0  token: 0x6000865
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB2F40  token: 0x6000866
        public System.Void .ctor(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B27F6D0  token: 0x6000867
        public virtual System.Boolean Equals(Unity.Mathematics.double2x3 rhs) { }
        // RVA: 0x0B27F730  token: 0x6000868
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27F7AC  token: 0x6000869
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27F990  token: 0x600086A
        public virtual System.String ToString() { }
        // RVA: 0x0B27F7DC  token: 0x600086B
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200001B  // size: 0x50
    public sealed struct double2x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double2 c0;  // 0x10
        public Unity.Mathematics.double2 c1;  // 0x20
        public Unity.Mathematics.double2 c2;  // 0x30
        public Unity.Mathematics.double2 c3;  // 0x40

        // Methods
        // RVA: 0x04DB3220  token: 0x600086C
        public System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2, Unity.Mathematics.double2 c3) { }
        // RVA: 0x04DB3250  token: 0x600086D
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13) { }
        // RVA: 0x04DB32A0  token: 0x600086E
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F00CC8  token: 0x600086F
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F00BE8  token: 0x6000870
        public System.Void .ctor(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x04DB3150  token: 0x6000871
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB3340  token: 0x6000872
        public System.Void .ctor(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x04DB32C0  token: 0x6000873
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB30B0  token: 0x6000874
        public System.Void .ctor(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x04DB3050  token: 0x6000875
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB31B0  token: 0x6000876
        public System.Void .ctor(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B27FB70  token: 0x6000877
        public virtual System.Boolean Equals(Unity.Mathematics.double2x4 rhs) { }
        // RVA: 0x0B27FBEC  token: 0x6000878
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B27FC70  token: 0x6000879
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B27FEBC  token: 0x600087A
        public virtual System.String ToString() { }
        // RVA: 0x0B27FCA8  token: 0x600087B
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200001C  // size: 0x28
    public sealed struct double3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Double x;  // 0x10
        public System.Double y;  // 0x18
        public System.Double z;  // 0x20
        public static readonly Unity.Mathematics.double3 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.double3 yzx { get; /* RVA: 0x04DB3540 */ }
        Unity.Mathematics.double3 zxy { get; /* RVA: 0x04DB3560 */ }
        System.Double Item { get; /* RVA: 0x04DB3530 */ }

        // Methods
        // RVA: 0x04DB3520  token: 0x600087C
        public System.Void .ctor(System.Double x, System.Double y, System.Double z) { }
        // RVA: 0x04DB3450  token: 0x600087D
        public System.Void .ctor(System.Double x, Unity.Mathematics.double2 yz) { }
        // RVA: 0x04DB3410  token: 0x600087E
        public System.Void .ctor(Unity.Mathematics.double2 xy, System.Double z) { }
        // RVA: 0x04DB3490  token: 0x600087F
        public System.Void .ctor(Unity.Mathematics.double3 xyz) { }
        // RVA: 0x04DB34B0  token: 0x6000880
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F00D48  token: 0x6000881
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F00D0C  token: 0x6000882
        public System.Void .ctor(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB3470  token: 0x6000883
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB3430  token: 0x6000884
        public System.Void .ctor(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DB34C0  token: 0x6000885
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB34F0  token: 0x6000886
        public System.Void .ctor(Unity.Mathematics.uint3 v) { }
        // RVA: 0x0B280318  token: 0x6000887
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B280368  token: 0x6000888
        public System.Void .ctor(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DB33F0  token: 0x6000889
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB33D0  token: 0x600088A
        public System.Void .ctor(Unity.Mathematics.float3 v) { }
        // RVA: 0x04DAEE70  token: 0x600088B
        public static Unity.Mathematics.double3 op_Implicit(System.Double v) { }
        // RVA: 0x04DAEF30  token: 0x600088C
        public static Unity.Mathematics.double3 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DAEDD0  token: 0x600088D
        public static Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DAEE20  token: 0x600088E
        public static Unity.Mathematics.double3 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DAEEA0  token: 0x600088F
        public static Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.uint3 v) { }
        // RVA: 0x04DAEDA0  token: 0x6000890
        public static Unity.Mathematics.double3 op_Implicit(System.Single v) { }
        // RVA: 0x0415F9A0  token: 0x6000891
        public static Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.float3 v) { }
        // RVA: 0x04DB3620  token: 0x6000892
        public static Unity.Mathematics.double3 op_Multiply(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x04DB3600  token: 0x6000893
        public static Unity.Mathematics.double3 op_Multiply(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x04DB35E0  token: 0x6000894
        public static Unity.Mathematics.double3 op_Multiply(System.Double lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x04DB35A0  token: 0x6000895
        public static Unity.Mathematics.double3 op_Addition(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x04DB3580  token: 0x6000896
        public static Unity.Mathematics.double3 op_Addition(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x04DB3680  token: 0x6000897
        public static Unity.Mathematics.double3 op_Subtraction(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x04DB3660  token: 0x6000898
        public static Unity.Mathematics.double3 op_Subtraction(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x04DB36C0  token: 0x6000899
        public static Unity.Mathematics.double3 op_Subtraction(System.Double lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x0B2803BC  token: 0x600089A
        public static Unity.Mathematics.double3 op_Division(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x0B2803F0  token: 0x600089B
        public static Unity.Mathematics.double3 op_Division(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x0B280410  token: 0x600089C
        public static Unity.Mathematics.double3 op_Division(System.Double lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x0B2804F8  token: 0x600089D
        public static Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x0B2804BC  token: 0x600089E
        public static Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x0B280484  token: 0x600089F
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs) { }
        // RVA: 0x04DB36F0  token: 0x60008A0
        public static Unity.Mathematics.double3 op_UnaryNegation(Unity.Mathematics.double3 val) { }
        // RVA: 0x0B28043C  token: 0x60008A1
        public static Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.double3 lhs, System.Double rhs) { }
        // RVA: 0x066A8DA0  token: 0x60008A5
        public virtual System.Boolean Equals(Unity.Mathematics.double3 rhs) { }
        // RVA: 0x0B28010C  token: 0x60008A6
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B280180  token: 0x60008A7
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B280258  token: 0x60008A8
        public virtual System.String ToString() { }
        // RVA: 0x0B2801A8  token: 0x60008A9
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200001E  // size: 0x40
    public sealed struct double3x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double3 c0;  // 0x10
        public Unity.Mathematics.double3 c1;  // 0x28

        // Methods
        // RVA: 0x04DA06E0  token: 0x60008AA
        public System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1) { }
        // RVA: 0x04DB3760  token: 0x60008AB
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21) { }
        // RVA: 0x04DB3800  token: 0x60008AC
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F011E4  token: 0x60008AD
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B28095C  token: 0x60008AE
        public System.Void .ctor(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x04DB3830  token: 0x60008AF
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F012B8  token: 0x60008B0
        public System.Void .ctor(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x04DB37A0  token: 0x60008B1
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F00FEC  token: 0x60008B2
        public System.Void .ctor(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x04DB3720  token: 0x60008B3
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F01064  token: 0x60008B4
        public System.Void .ctor(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B24EE70  token: 0x60008B5
        public static Unity.Mathematics.double3x2 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B24ED1C  token: 0x60008B6
        public static Unity.Mathematics.double3x2 op_Implicit(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B280BB8  token: 0x60008B7
        public static Unity.Mathematics.double3x2 op_Multiply(Unity.Mathematics.double3x2 lhs, System.Double rhs) { }
        // RVA: 0x0B280A60  token: 0x60008B8
        public static Unity.Mathematics.double3x2 op_Addition(Unity.Mathematics.double3x2 lhs, Unity.Mathematics.double3x2 rhs) { }
        // RVA: 0x0B280C3C  token: 0x60008B9
        public static Unity.Mathematics.double3x2 op_Subtraction(Unity.Mathematics.double3x2 lhs, Unity.Mathematics.double3x2 rhs) { }
        // RVA: 0x0B280B24  token: 0x60008BA
        public static Unity.Mathematics.double3x2 op_Division(Unity.Mathematics.double3x2 lhs, System.Double rhs) { }
        // RVA: 0x066A921C  token: 0x60008BB
        public virtual System.Boolean Equals(Unity.Mathematics.double3x2 rhs) { }
        // RVA: 0x0B28051C  token: 0x60008BC
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B280598  token: 0x60008BD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28077C  token: 0x60008BE
        public virtual System.String ToString() { }
        // RVA: 0x0B2805C8  token: 0x60008BF
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200001F  // size: 0x58
    public sealed struct double3x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double3 c0;  // 0x10
        public Unity.Mathematics.double3 c1;  // 0x28
        public Unity.Mathematics.double3 c2;  // 0x40
        public static readonly Unity.Mathematics.double3x3 identity;  // static @ 0x0

        // Properties
        Unity.Mathematics.double3& Item { get; /* RVA: 0x04DB3AC0 */ }

        // Methods
        // RVA: 0x04DB38E0  token: 0x60008C0
        public System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2) { }
        // RVA: 0x04DB3A60  token: 0x60008C1
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22) { }
        // RVA: 0x04DB3920  token: 0x60008C2
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F01698  token: 0x60008C3
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B281338  token: 0x60008C4
        public System.Void .ctor(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x04DB39F0  token: 0x60008C5
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F015F0  token: 0x60008C6
        public System.Void .ctor(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x04DB3960  token: 0x60008C7
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F01548  token: 0x60008C8
        public System.Void .ctor(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x04DB3880  token: 0x60008C9
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F01330  token: 0x60008CA
        public System.Void .ctor(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x0B2814A4  token: 0x60008CB
        public static Unity.Mathematics.double3x3 op_Multiply(Unity.Mathematics.double3x3 lhs, System.Double rhs) { }
        // RVA: 0x0B280D94  token: 0x60008CD
        public virtual System.Boolean Equals(Unity.Mathematics.double3x3 rhs) { }
        // RVA: 0x0B280D00  token: 0x60008CE
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B280E28  token: 0x60008CF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B280E6C  token: 0x60008D0
        public virtual System.String ToString() { }
        // RVA: 0x0B2810F4  token: 0x60008D1
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04B6B1D0  token: 0x60008D2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x70
    public sealed struct double3x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double3 c0;  // 0x10
        public Unity.Mathematics.double3 c1;  // 0x28
        public Unity.Mathematics.double3 c2;  // 0x40
        public Unity.Mathematics.double3 c3;  // 0x58

        // Properties
        Unity.Mathematics.double3& Item { get; /* RVA: 0x04DB3AC0 */ }

        // Methods
        // RVA: 0x04DB3AD0  token: 0x60008D3
        public System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2, Unity.Mathematics.double3 c3) { }
        // RVA: 0x04DB3B20  token: 0x60008D4
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23) { }
        // RVA: 0x04DB3CE0  token: 0x60008D5
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F01B54  token: 0x60008D6
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B281D40  token: 0x60008D7
        public System.Void .ctor(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x04DB3BA0  token: 0x60008D8
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F01A78  token: 0x60008D9
        public System.Void .ctor(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x04DB3C20  token: 0x60008DA
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F0199C  token: 0x60008DB
        public System.Void .ctor(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x04DB3D30  token: 0x60008DC
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F01CD0  token: 0x60008DD
        public System.Void .ctor(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x0B24F088  token: 0x60008DE
        public static Unity.Mathematics.double3x4 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B281568  token: 0x60008E0
        public virtual System.Boolean Equals(Unity.Mathematics.double3x4 rhs) { }
        // RVA: 0x0B281650  token: 0x60008E1
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2816F0  token: 0x60008E2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B281A10  token: 0x60008E3
        public virtual System.String ToString() { }
        // RVA: 0x0B28173C  token: 0x60008E4
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000021  // size: 0x30
    public sealed struct double4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Double x;  // 0x10
        public System.Double y;  // 0x18
        public System.Double z;  // 0x20
        public System.Double w;  // 0x28
        public static readonly Unity.Mathematics.double4 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.double3 xyz { get; /* RVA: 0x04DB3FB0 */ set; /* RVA: 0x04DB3490 */ }
        System.Double Item { get; /* RVA: 0x04DB3530 */ set; /* RVA: 0x04DB4160 */ }

        // Methods
        // RVA: 0x04DB3E70  token: 0x60008E5
        public System.Void .ctor(System.Double x, System.Double y, System.Double z, System.Double w) { }
        // RVA: 0x04DB3DF0  token: 0x60008E6
        public System.Void .ctor(System.Double x, System.Double y, Unity.Mathematics.double2 zw) { }
        // RVA: 0x04DB3E10  token: 0x60008E7
        public System.Void .ctor(System.Double x, Unity.Mathematics.double2 yz, System.Double w) { }
        // RVA: 0x04DB3E50  token: 0x60008E8
        public System.Void .ctor(System.Double x, Unity.Mathematics.double3 yzw) { }
        // RVA: 0x04DB3F50  token: 0x60008E9
        public System.Void .ctor(Unity.Mathematics.double2 xy, System.Double z, System.Double w) { }
        // RVA: 0x04DB3E30  token: 0x60008EA
        public System.Void .ctor(Unity.Mathematics.double2 xy, Unity.Mathematics.double2 zw) { }
        // RVA: 0x04DB3F30  token: 0x60008EB
        public System.Void .ctor(Unity.Mathematics.double3 xyz, System.Double w) { }
        // RVA: 0x04DB3DD0  token: 0x60008EC
        public System.Void .ctor(Unity.Mathematics.double4 xyzw) { }
        // RVA: 0x04DB3F70  token: 0x60008ED
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F01E20  token: 0x60008EE
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F01EC8  token: 0x60008EF
        public System.Void .ctor(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB3E90  token: 0x60008F0
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB3F10  token: 0x60008F1
        public System.Void .ctor(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DB3F80  token: 0x60008F2
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB3EC0  token: 0x60008F3
        public System.Void .ctor(Unity.Mathematics.uint4 v) { }
        // RVA: 0x05F01E64  token: 0x60008F4
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x05F01DAC  token: 0x60008F5
        public System.Void .ctor(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DB3DB0  token: 0x60008F6
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB3F00  token: 0x60008F7
        public System.Void .ctor(Unity.Mathematics.float4 v) { }
        // RVA: 0x04DAF080  token: 0x60008F8
        public static Unity.Mathematics.double4 op_Implicit(System.Double v) { }
        // RVA: 0x04DAF0E0  token: 0x60008F9
        public static Unity.Mathematics.double4 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B24F2FC  token: 0x60008FA
        public static Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DAF1A0  token: 0x60008FB
        public static Unity.Mathematics.double4 op_Implicit(System.UInt32 v) { }
        // RVA: 0x0B24F324  token: 0x60008FC
        public static Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.uint4 v) { }
        // RVA: 0x04DAF090  token: 0x60008FD
        public static Unity.Mathematics.double4 op_Implicit(System.Single v) { }
        // RVA: 0x0B24F2B8  token: 0x60008FE
        public static Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.float4 v) { }
        // RVA: 0x04DB4040  token: 0x60008FF
        public static Unity.Mathematics.double4 op_Multiply(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x04DB4020  token: 0x6000900
        public static Unity.Mathematics.double4 op_Multiply(Unity.Mathematics.double4 lhs, System.Double rhs) { }
        // RVA: 0x04DB4090  token: 0x6000901
        public static Unity.Mathematics.double4 op_Multiply(System.Double lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x04DB3FD0  token: 0x6000902
        public static Unity.Mathematics.double4 op_Addition(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x04DB40F0  token: 0x6000903
        public static Unity.Mathematics.double4 op_Subtraction(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x04DB40B0  token: 0x6000904
        public static Unity.Mathematics.double4 op_Subtraction(System.Double lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x0B2822B8  token: 0x6000905
        public static Unity.Mathematics.double4 op_Division(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x0B2822FC  token: 0x6000906
        public static Unity.Mathematics.double4 op_Division(System.Double lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x0B2823E0  token: 0x6000907
        public static Unity.Mathematics.bool4 op_LessThan(Unity.Mathematics.double4 lhs, System.Double rhs) { }
        // RVA: 0x0B282398  token: 0x6000908
        public static Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs) { }
        // RVA: 0x04DB4140  token: 0x6000909
        public static Unity.Mathematics.double4 op_UnaryNegation(Unity.Mathematics.double4 val) { }
        // RVA: 0x0B282334  token: 0x600090A
        public static Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.double4 lhs, System.Double rhs) { }
        // RVA: 0x0B281F8C  token: 0x600090F
        public virtual System.Boolean Equals(Unity.Mathematics.double4 rhs) { }
        // RVA: 0x0B281F18  token: 0x6000910
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B281FD4  token: 0x6000911
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B281FF8  token: 0x6000912
        public virtual System.String ToString() { }
        // RVA: 0x0B282164  token: 0x6000913
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000023  // size: 0x50
    public sealed struct double4x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double4 c0;  // 0x10
        public Unity.Mathematics.double4 c1;  // 0x30

        // Methods
        // RVA: 0x04DB4230  token: 0x6000914
        public System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1) { }
        // RVA: 0x04DB4170  token: 0x6000915
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21, System.Double m30, System.Double m31) { }
        // RVA: 0x04DB41C0  token: 0x6000916
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F023C4  token: 0x6000917
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F02320  token: 0x6000918
        public System.Void .ctor(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x04DB4250  token: 0x6000919
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B282990  token: 0x600091A
        public System.Void .ctor(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x05F02278  token: 0x600091B
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B2829F4  token: 0x600091C
        public System.Void .ctor(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x04DB41F0  token: 0x600091D
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B282A58  token: 0x600091E
        public System.Void .ctor(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x0B282490  token: 0x600091F
        public virtual System.Boolean Equals(Unity.Mathematics.double4x2 rhs) { }
        // RVA: 0x0B28240C  token: 0x6000920
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2824F4  token: 0x6000921
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B282740  token: 0x6000922
        public virtual System.String ToString() { }
        // RVA: 0x0B28252C  token: 0x6000923
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000024  // size: 0x70
    public sealed struct double4x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double4 c0;  // 0x10
        public Unity.Mathematics.double4 c1;  // 0x30
        public Unity.Mathematics.double4 c2;  // 0x50

        // Methods
        // RVA: 0x04DB4450  token: 0x6000924
        public System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2) { }
        // RVA: 0x04DB4350  token: 0x6000925
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22, System.Double m30, System.Double m31, System.Double m32) { }
        // RVA: 0x04DB42A0  token: 0x6000926
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F027A8  token: 0x6000927
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F026D0  token: 0x6000928
        public System.Void .ctor(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x04DB43E0  token: 0x6000929
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B2832BC  token: 0x600092A
        public System.Void .ctor(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x05F02674  token: 0x600092B
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B28334C  token: 0x600092C
        public System.Void .ctor(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x04DB42F0  token: 0x600092D
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B28322C  token: 0x600092E
        public System.Void .ctor(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B24F55C  token: 0x600092F
        public static Unity.Mathematics.double4x3 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B24F660  token: 0x6000930
        public static Unity.Mathematics.double4x3 op_Implicit(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x0B2833DC  token: 0x6000931
        public static Unity.Mathematics.double4x3 op_Multiply(Unity.Mathematics.double4x3 lhs, Unity.Mathematics.double4x3 rhs) { }
        // RVA: 0x0B282ABC  token: 0x6000932
        public virtual System.Boolean Equals(Unity.Mathematics.double4x3 rhs) { }
        // RVA: 0x0B282B3C  token: 0x6000933
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B282BDC  token: 0x6000934
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B282EFC  token: 0x6000935
        public virtual System.String ToString() { }
        // RVA: 0x0B282C28  token: 0x6000936
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000025  // size: 0x90
    public sealed struct double4x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.double4 c0;  // 0x10
        public Unity.Mathematics.double4 c1;  // 0x30
        public Unity.Mathematics.double4 c2;  // 0x50
        public Unity.Mathematics.double4 c3;  // 0x70
        public static readonly Unity.Mathematics.double4x4 identity;  // static @ 0x0
        public static readonly Unity.Mathematics.double4x4 zero;  // static @ 0x80

        // Methods
        // RVA: 0x04DB4560  token: 0x6000937
        public System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2, Unity.Mathematics.double4 c3) { }
        // RVA: 0x04A30010  token: 0x6000938
        public System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23, System.Double m30, System.Double m31, System.Double m32, System.Double m33) { }
        // RVA: 0x04DB4500  token: 0x6000939
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x05F02960  token: 0x600093A
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F02A40  token: 0x600093B
        public System.Void .ctor(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x04DB45B0  token: 0x600093C
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B283FA8  token: 0x600093D
        public System.Void .ctor(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x05F029C8  token: 0x600093E
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B283EF4  token: 0x600093F
        public System.Void .ctor(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x04DB4490  token: 0x6000940
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B283E40  token: 0x6000941
        public System.Void .ctor(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B24F808  token: 0x6000942
        public static Unity.Mathematics.double4x4 op_Implicit(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x0B2834E4  token: 0x6000943
        public virtual System.Boolean Equals(Unity.Mathematics.double4x4 rhs) { }
        // RVA: 0x0B283588  token: 0x6000944
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B283640  token: 0x6000945
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B283A30  token: 0x6000946
        public virtual System.String ToString() { }
        // RVA: 0x0B28369C  token: 0x6000947
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04A2FEE0  token: 0x6000948
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public sealed struct float2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public static readonly Unity.Mathematics.float2 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.float4 xyxy { get; /* RVA: 0x04DB4750 */ }
        Unity.Mathematics.float2 xy { get; /* RVA: 0x04D889F0 */ }
        Unity.Mathematics.float2 yx { get; /* RVA: 0x04DB4770 */ }

        // Methods
        // RVA: 0x04D87A10  token: 0x6000949
        public System.Void .ctor(System.Single x, System.Single y) { }
        // RVA: 0x04DB4680  token: 0x600094A
        public System.Void .ctor(Unity.Mathematics.float2 xy) { }
        // RVA: 0x04DB4640  token: 0x600094B
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B284218  token: 0x600094C
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F02C08  token: 0x600094D
        public System.Void .ctor(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB46F0  token: 0x600094E
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB46D0  token: 0x600094F
        public System.Void .ctor(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DB46A0  token: 0x6000950
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB4650  token: 0x6000951
        public System.Void .ctor(Unity.Mathematics.uint2 v) { }
        // RVA: 0x0B284234  token: 0x6000952
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B284260  token: 0x6000953
        public System.Void .ctor(Unity.Mathematics.half2 v) { }
        // RVA: 0x04DB4710  token: 0x6000954
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB4730  token: 0x6000955
        public System.Void .ctor(Unity.Mathematics.double2 v) { }
        // RVA: 0x04D8B720  token: 0x6000956
        public static Unity.Mathematics.float2 op_Implicit(System.Single v) { }
        // RVA: 0x04DAF4E0  token: 0x6000957
        public static Unity.Mathematics.float2 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DAF500  token: 0x6000958
        public static Unity.Mathematics.float2 op_Implicit(Unity.Mathematics.int2 v) { }
        // RVA: 0x04DAF550  token: 0x6000959
        public static Unity.Mathematics.float2 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DAF520  token: 0x600095A
        public static Unity.Mathematics.float2 op_Implicit(Unity.Mathematics.uint2 v) { }
        // RVA: 0x04DAF580  token: 0x600095B
        public static Unity.Mathematics.float2 op_Explicit(System.Double v) { }
        // RVA: 0x04DAF4B0  token: 0x600095C
        public static Unity.Mathematics.float2 op_Explicit(Unity.Mathematics.double2 v) { }
        // RVA: 0x04DA8070  token: 0x600095D
        public static Unity.Mathematics.float2 op_Multiply(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DA8040  token: 0x600095E
        public static Unity.Mathematics.float2 op_Multiply(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x04DB47E0  token: 0x600095F
        public static Unity.Mathematics.float2 op_Multiply(System.Single lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DA7FD0  token: 0x6000960
        public static Unity.Mathematics.float2 op_Addition(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DA8010  token: 0x6000961
        public static Unity.Mathematics.float2 op_Addition(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x04DA80E0  token: 0x6000962
        public static Unity.Mathematics.float2 op_Subtraction(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DA80B0  token: 0x6000963
        public static Unity.Mathematics.float2 op_Subtraction(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x04DB4810  token: 0x6000964
        public static Unity.Mathematics.float2 op_Subtraction(System.Single lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x0AAEC45C  token: 0x6000965
        public static Unity.Mathematics.float2 op_Division(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x0AAEC948  token: 0x6000966
        public static Unity.Mathematics.float2 op_Division(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x0B284290  token: 0x6000967
        public static Unity.Mathematics.float2 op_Division(System.Single lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DB4790  token: 0x6000968
        public static Unity.Mathematics.float2 op_Modulus(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x0B284358  token: 0x6000969
        public static Unity.Mathematics.bool2 op_LessThan(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x0B284320  token: 0x600096A
        public static Unity.Mathematics.bool2 op_LessThanOrEqual(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x0B2842E8  token: 0x600096B
        public static Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs) { }
        // RVA: 0x04DB4830  token: 0x600096C
        public static Unity.Mathematics.float2 op_UnaryNegation(Unity.Mathematics.float2 val) { }
        // RVA: 0x0B2842B0  token: 0x600096D
        public static Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.float2 lhs, System.Single rhs) { }
        // RVA: 0x0AAEC490  token: 0x6000971
        public virtual System.Boolean Equals(Unity.Mathematics.float2 rhs) { }
        // RVA: 0x0B28405C  token: 0x6000972
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2840CC  token: 0x6000973
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B284188  token: 0x6000974
        public virtual System.String ToString() { }
        // RVA: 0x0B284100  token: 0x6000975
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04DA7CB0  token: 0x6000976
        public static UnityEngine.Vector2 op_Implicit(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DA7CB0  token: 0x6000977
        public static Unity.Mathematics.float2 op_Implicit(UnityEngine.Vector2 v) { }

    }

    // TypeToken: 0x2000028  // size: 0x20
    public sealed struct float2x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float2 c0;  // 0x10
        public Unity.Mathematics.float2 c1;  // 0x18
        public static readonly Unity.Mathematics.float2x2 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D861B0  token: 0x6000978
        public System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1) { }
        // RVA: 0x04DB4940  token: 0x6000979
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11) { }
        // RVA: 0x04DB48F0  token: 0x600097A
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F02CC0  token: 0x600097B
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B284700  token: 0x600097C
        public System.Void .ctor(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x04DB4910  token: 0x600097D
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB49A0  token: 0x600097E
        public System.Void .ctor(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x04DB49E0  token: 0x600097F
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB4860  token: 0x6000980
        public System.Void .ctor(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x04DB48C0  token: 0x6000981
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB4960  token: 0x6000982
        public System.Void .ctor(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B28477C  token: 0x6000983
        public static Unity.Mathematics.float2x2 op_Multiply(Unity.Mathematics.float2x2 lhs, System.Single rhs) { }
        // RVA: 0x0B28437C  token: 0x6000984
        public virtual System.Boolean Equals(Unity.Mathematics.float2x2 rhs) { }
        // RVA: 0x0B2843B8  token: 0x6000985
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B284424  token: 0x6000986
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B284594  token: 0x6000987
        public virtual System.String ToString() { }
        // RVA: 0x0B284440  token: 0x6000988
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04D27400  token: 0x6000989
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x28
    public sealed struct float2x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float2 c0;  // 0x10
        public Unity.Mathematics.float2 c1;  // 0x18
        public Unity.Mathematics.float2 c2;  // 0x20

        // Methods
        // RVA: 0x04D8C0D0  token: 0x600098A
        public System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2) { }
        // RVA: 0x04DB4B80  token: 0x600098B
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12) { }
        // RVA: 0x04DB4C00  token: 0x600098C
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F02E7C  token: 0x600098D
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B284C94  token: 0x600098E
        public System.Void .ctor(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x04DB4C90  token: 0x600098F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB4B20  token: 0x6000990
        public System.Void .ctor(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x04DB4C20  token: 0x6000991
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB4A90  token: 0x6000992
        public System.Void .ctor(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x04DB4BB0  token: 0x6000993
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB4A30  token: 0x6000994
        public System.Void .ctor(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B28485C  token: 0x6000995
        public virtual System.Boolean Equals(Unity.Mathematics.float2x3 rhs) { }
        // RVA: 0x0B2847E8  token: 0x6000996
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2848D8  token: 0x6000997
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B284900  token: 0x6000998
        public virtual System.String ToString() { }
        // RVA: 0x0B284AE0  token: 0x6000999
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200002A  // size: 0x30
    public sealed struct float2x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float2 c0;  // 0x10
        public Unity.Mathematics.float2 c1;  // 0x18
        public Unity.Mathematics.float2 c2;  // 0x20
        public Unity.Mathematics.float2 c3;  // 0x28

        // Methods
        // RVA: 0x04D8BCB0  token: 0x600099A
        public System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2, Unity.Mathematics.float2 c3) { }
        // RVA: 0x04DB4CE0  token: 0x600099B
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13) { }
        // RVA: 0x04DB4DA0  token: 0x600099C
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F02FFC  token: 0x600099D
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2852D0  token: 0x600099E
        public System.Void .ctor(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x04DB5010  token: 0x600099F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB4DC0  token: 0x60009A0
        public System.Void .ctor(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x04DB4F70  token: 0x60009A1
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB4E40  token: 0x60009A2
        public System.Void .ctor(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x04DB4F00  token: 0x60009A3
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB4D30  token: 0x60009A4
        public System.Void .ctor(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B284D4C  token: 0x60009A5
        public virtual System.Boolean Equals(Unity.Mathematics.float2x4 rhs) { }
        // RVA: 0x0B284DD4  token: 0x60009A6
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B284E48  token: 0x60009A7
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B285080  token: 0x60009A8
        public virtual System.String ToString() { }
        // RVA: 0x0B284E6C  token: 0x60009A9
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200002B  // size: 0x1C
    public sealed struct float3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single z;  // 0x18
        public static readonly Unity.Mathematics.float3 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.float4 xxxx { get; /* RVA: 0x04DB5230 */ }
        Unity.Mathematics.float4 xxxy { get; /* RVA: 0x04DB5250 */ }
        Unity.Mathematics.float4 xxxz { get; /* RVA: 0x04DB5270 */ }
        Unity.Mathematics.float4 xxyx { get; /* RVA: 0x04DB52B0 */ }
        Unity.Mathematics.float4 xxyy { get; /* RVA: 0x04DB52D0 */ }
        Unity.Mathematics.float4 xxyz { get; /* RVA: 0x04DB52F0 */ }
        Unity.Mathematics.float4 xxzx { get; /* RVA: 0x04DB5330 */ }
        Unity.Mathematics.float4 xxzy { get; /* RVA: 0x04DB5350 */ }
        Unity.Mathematics.float4 xxzz { get; /* RVA: 0x04DB5370 */ }
        Unity.Mathematics.float4 xyxx { get; /* RVA: 0x04DB53B0 */ }
        Unity.Mathematics.float4 xyxy { get; /* RVA: 0x04DB4750 */ }
        Unity.Mathematics.float4 xyxz { get; /* RVA: 0x04DB53D0 */ }
        Unity.Mathematics.float4 xyyx { get; /* RVA: 0x04DB5410 */ }
        Unity.Mathematics.float4 xyyy { get; /* RVA: 0x04DB5430 */ }
        Unity.Mathematics.float4 xyyz { get; /* RVA: 0x04DB5450 */ }
        Unity.Mathematics.float4 xyzx { get; /* RVA: 0x04064E30 */ }
        Unity.Mathematics.float4 xyzy { get; /* RVA: 0x04DB5470 */ }
        Unity.Mathematics.float4 xyzz { get; /* RVA: 0x04DB5490 */ }
        Unity.Mathematics.float4 xzxx { get; /* RVA: 0x04DB54D0 */ }
        Unity.Mathematics.float4 xzxy { get; /* RVA: 0x04DB54F0 */ }
        Unity.Mathematics.float4 xzxz { get; /* RVA: 0x04DB5510 */ }
        Unity.Mathematics.float4 xzyx { get; /* RVA: 0x04DB5550 */ }
        Unity.Mathematics.float4 xzyy { get; /* RVA: 0x04DB5570 */ }
        Unity.Mathematics.float4 xzyz { get; /* RVA: 0x04DB5590 */ }
        Unity.Mathematics.float4 xzzx { get; /* RVA: 0x04DB55D0 */ }
        Unity.Mathematics.float4 xzzy { get; /* RVA: 0x04DB55F0 */ }
        Unity.Mathematics.float4 xzzz { get; /* RVA: 0x04DB5610 */ }
        Unity.Mathematics.float4 yxxx { get; /* RVA: 0x04DB5650 */ }
        Unity.Mathematics.float4 yxxy { get; /* RVA: 0x04DB5670 */ }
        Unity.Mathematics.float4 yxxz { get; /* RVA: 0x04DB5690 */ }
        Unity.Mathematics.float4 yxyx { get; /* RVA: 0x04DB56D0 */ }
        Unity.Mathematics.float4 yxyy { get; /* RVA: 0x04DB56F0 */ }
        Unity.Mathematics.float4 yxyz { get; /* RVA: 0x04DB5710 */ }
        Unity.Mathematics.float4 yxzx { get; /* RVA: 0x04DB5750 */ }
        Unity.Mathematics.float4 yxzy { get; /* RVA: 0x04DB5770 */ }
        Unity.Mathematics.float4 yxzz { get; /* RVA: 0x04DB5790 */ }
        Unity.Mathematics.float4 yyxx { get; /* RVA: 0x04DB57F0 */ }
        Unity.Mathematics.float4 yyxy { get; /* RVA: 0x04DB5810 */ }
        Unity.Mathematics.float4 yyxz { get; /* RVA: 0x04DB5830 */ }
        Unity.Mathematics.float4 yyyx { get; /* RVA: 0x04DB5860 */ }
        Unity.Mathematics.float4 yyyy { get; /* RVA: 0x04DB5880 */ }
        Unity.Mathematics.float4 yyyz { get; /* RVA: 0x04DB58A0 */ }
        Unity.Mathematics.float4 yyzx { get; /* RVA: 0x04DB58E0 */ }
        Unity.Mathematics.float4 yyzy { get; /* RVA: 0x04DB5900 */ }
        Unity.Mathematics.float4 yyzz { get; /* RVA: 0x04DB5920 */ }
        Unity.Mathematics.float4 yzxx { get; /* RVA: 0x04DB5960 */ }
        Unity.Mathematics.float4 yzxy { get; /* RVA: 0x04064E10 */ }
        Unity.Mathematics.float4 yzxz { get; /* RVA: 0x04064DF0 */ }
        Unity.Mathematics.float4 yzyx { get; /* RVA: 0x04DB59A0 */ }
        Unity.Mathematics.float4 yzyy { get; /* RVA: 0x04DB59C0 */ }
        Unity.Mathematics.float4 yzyz { get; /* RVA: 0x04DB59E0 */ }
        Unity.Mathematics.float4 yzzx { get; /* RVA: 0x04DB5A20 */ }
        Unity.Mathematics.float4 yzzy { get; /* RVA: 0x04DB5A40 */ }
        Unity.Mathematics.float4 yzzz { get; /* RVA: 0x04DB5A60 */ }
        Unity.Mathematics.float4 zxxx { get; /* RVA: 0x04DB5AC0 */ }
        Unity.Mathematics.float4 zxxy { get; /* RVA: 0x04DB5AE0 */ }
        Unity.Mathematics.float4 zxxz { get; /* RVA: 0x04DB5B00 */ }
        Unity.Mathematics.float4 zxyx { get; /* RVA: 0x04DB5B40 */ }
        Unity.Mathematics.float4 zxyy { get; /* RVA: 0x040B34F0 */ }
        Unity.Mathematics.float4 zxyz { get; /* RVA: 0x04064DD0 */ }
        Unity.Mathematics.float4 zxzx { get; /* RVA: 0x04DB5B80 */ }
        Unity.Mathematics.float4 zxzy { get; /* RVA: 0x04DB5BA0 */ }
        Unity.Mathematics.float4 zxzz { get; /* RVA: 0x04DB5BC0 */ }
        Unity.Mathematics.float4 zyxx { get; /* RVA: 0x04DB5C20 */ }
        Unity.Mathematics.float4 zyxy { get; /* RVA: 0x04DB5C40 */ }
        Unity.Mathematics.float4 zyxz { get; /* RVA: 0x04DB5C60 */ }
        Unity.Mathematics.float4 zyyx { get; /* RVA: 0x04DB5CA0 */ }
        Unity.Mathematics.float4 zyyy { get; /* RVA: 0x04DB5CC0 */ }
        Unity.Mathematics.float4 zyyz { get; /* RVA: 0x04DB5CE0 */ }
        Unity.Mathematics.float4 zyzx { get; /* RVA: 0x04DB5D20 */ }
        Unity.Mathematics.float4 zyzy { get; /* RVA: 0x04DB5D40 */ }
        Unity.Mathematics.float4 zyzz { get; /* RVA: 0x04DB5D60 */ }
        Unity.Mathematics.float4 zzxx { get; /* RVA: 0x04DB5DC0 */ }
        Unity.Mathematics.float4 zzxy { get; /* RVA: 0x04DB5DE0 */ }
        Unity.Mathematics.float4 zzxz { get; /* RVA: 0x04DB5E00 */ }
        Unity.Mathematics.float4 zzyx { get; /* RVA: 0x04DB5E40 */ }
        Unity.Mathematics.float4 zzyy { get; /* RVA: 0x04DB5E60 */ }
        Unity.Mathematics.float4 zzyz { get; /* RVA: 0x04DB5E80 */ }
        Unity.Mathematics.float4 zzzx { get; /* RVA: 0x04DB5EB0 */ }
        Unity.Mathematics.float4 zzzy { get; /* RVA: 0x04DB5ED0 */ }
        Unity.Mathematics.float4 zzzz { get; /* RVA: 0x04DB5EF0 */ }
        Unity.Mathematics.float3 xxx { get; /* RVA: 0x04DB5220 */ }
        Unity.Mathematics.float3 xxy { get; /* RVA: 0x04DB5290 */ }
        Unity.Mathematics.float3 xxz { get; /* RVA: 0x04DB5310 */ }
        Unity.Mathematics.float3 xyx { get; /* RVA: 0x04DB5390 */ }
        Unity.Mathematics.float3 xyy { get; /* RVA: 0x04DB53F0 */ }
        Unity.Mathematics.float3 xyz { get; /* RVA: 0x04D88970 */ set; /* RVA: 0x04D88F40 */ }
        Unity.Mathematics.float3 xzx { get; /* RVA: 0x04DB54B0 */ }
        Unity.Mathematics.float3 xzy { get; /* RVA: 0x04DB5530 */ set; /* RVA: 0x04DB60E0 */ }
        Unity.Mathematics.float3 xzz { get; /* RVA: 0x04DB55B0 */ }
        Unity.Mathematics.float3 yxx { get; /* RVA: 0x04DB5630 */ }
        Unity.Mathematics.float3 yxy { get; /* RVA: 0x04DB56B0 */ }
        Unity.Mathematics.float3 yxz { get; /* RVA: 0x04DB5730 */ set; /* RVA: 0x04DB6120 */ }
        Unity.Mathematics.float3 yyx { get; /* RVA: 0x04DB57D0 */ }
        Unity.Mathematics.float3 yyy { get; /* RVA: 0x04DB5850 */ }
        Unity.Mathematics.float3 yyz { get; /* RVA: 0x04DB58C0 */ }
        Unity.Mathematics.float3 yzx { get; /* RVA: 0x04DB5940 */ set; /* RVA: 0x04DB6160 */ }
        Unity.Mathematics.float3 yzy { get; /* RVA: 0x04DB5980 */ }
        Unity.Mathematics.float3 yzz { get; /* RVA: 0x04DB5A00 */ }
        Unity.Mathematics.float3 zxx { get; /* RVA: 0x04DB5AA0 */ }
        Unity.Mathematics.float3 zxy { get; /* RVA: 0x04DB5B20 */ set; /* RVA: 0x04DB61A0 */ }
        Unity.Mathematics.float3 zxz { get; /* RVA: 0x04DB5B60 */ }
        Unity.Mathematics.float3 zyx { get; /* RVA: 0x04DB5C00 */ set; /* RVA: 0x04DB61E0 */ }
        Unity.Mathematics.float3 zyy { get; /* RVA: 0x04DB5C80 */ }
        Unity.Mathematics.float3 zyz { get; /* RVA: 0x04DB5D00 */ }
        Unity.Mathematics.float3 zzx { get; /* RVA: 0x04DB5DA0 */ }
        Unity.Mathematics.float3 zzy { get; /* RVA: 0x04DB5E20 */ }
        Unity.Mathematics.float3 zzz { get; /* RVA: 0x04DB5EA0 */ }
        Unity.Mathematics.float2 xx { get; /* RVA: 0x04DB5210 */ }
        Unity.Mathematics.float2 xy { get; /* RVA: 0x04D889F0 */ set; /* RVA: 0x04DB4680 */ }
        Unity.Mathematics.float2 xz { get; /* RVA: 0x04D88A40 */ set; /* RVA: 0x04DB60C0 */ }
        Unity.Mathematics.float2 yx { get; /* RVA: 0x04DB4770 */ set; /* RVA: 0x04DB6100 */ }
        Unity.Mathematics.float2 yy { get; /* RVA: 0x04DB57B0 */ }
        Unity.Mathematics.float2 yz { get; /* RVA: 0x04D8B880 */ set; /* RVA: 0x04DB6140 */ }
        Unity.Mathematics.float2 zx { get; /* RVA: 0x04DB5A80 */ set; /* RVA: 0x04DB6180 */ }
        Unity.Mathematics.float2 zy { get; /* RVA: 0x04DB5BE0 */ set; /* RVA: 0x04DB61C0 */ }
        Unity.Mathematics.float2 zz { get; /* RVA: 0x04DB5D80 */ }
        System.Single Item { get; /* RVA: 0x04DB5200 */ set; /* RVA: 0x04DB60B0 */ }

        // Methods
        // RVA: 0x04D88F30  token: 0x60009AA
        public System.Void .ctor(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x04DB51B0  token: 0x60009AB
        public System.Void .ctor(System.Single x, Unity.Mathematics.float2 yz) { }
        // RVA: 0x04DB50E0  token: 0x60009AC
        public System.Void .ctor(Unity.Mathematics.float2 xy, System.Single z) { }
        // RVA: 0x04D88F40  token: 0x60009AD
        public System.Void .ctor(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x04DB5170  token: 0x60009AE
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F0316C  token: 0x60009AF
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F031A0  token: 0x60009B0
        public System.Void .ctor(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB5070  token: 0x60009B1
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB5180  token: 0x60009B2
        public System.Void .ctor(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DB5100  token: 0x60009B3
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB50A0  token: 0x60009B4
        public System.Void .ctor(Unity.Mathematics.uint3 v) { }
        // RVA: 0x0B285610  token: 0x60009B5
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B2855D4  token: 0x60009B6
        public System.Void .ctor(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DB5140  token: 0x60009B7
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB51D0  token: 0x60009B8
        public System.Void .ctor(Unity.Mathematics.double3 v) { }
        // RVA: 0x04D8B860  token: 0x60009B9
        public static Unity.Mathematics.float3 op_Implicit(System.Single v) { }
        // RVA: 0x0B251E8C  token: 0x60009BA
        public static Unity.Mathematics.float3 op_Explicit(System.Boolean v) { }
        // RVA: 0x0B251F30  token: 0x60009BB
        public static Unity.Mathematics.float3 op_Explicit(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DAF610  token: 0x60009BC
        public static Unity.Mathematics.float3 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DAF7A0  token: 0x60009BD
        public static Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.int3 v) { }
        // RVA: 0x04DAF760  token: 0x60009BE
        public static Unity.Mathematics.float3 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DAF6D0  token: 0x60009BF
        public static Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.uint3 v) { }
        // RVA: 0x0B251EA8  token: 0x60009C0
        public static Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.half v) { }
        // RVA: 0x0B251EE4  token: 0x60009C1
        public static Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.half3 v) { }
        // RVA: 0x04DAF640  token: 0x60009C2
        public static Unity.Mathematics.float3 op_Explicit(System.Double v) { }
        // RVA: 0x04DAF5D0  token: 0x60009C3
        public static Unity.Mathematics.float3 op_Explicit(Unity.Mathematics.double3 v) { }
        // RVA: 0x04D886A0  token: 0x60009C4
        public static Unity.Mathematics.float3 op_Multiply(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x030E7A60  token: 0x60009C5
        public static Unity.Mathematics.float3 op_Multiply(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x030E9530  token: 0x60009C6
        public static Unity.Mathematics.float3 op_Multiply(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x030E9EA0  token: 0x60009C7
        public static Unity.Mathematics.float3 op_Addition(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x04118DE0  token: 0x60009C8
        public static Unity.Mathematics.float3 op_Addition(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x04DB5F10  token: 0x60009C9
        public static Unity.Mathematics.float3 op_Addition(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x030E9560  token: 0x60009CA
        public static Unity.Mathematics.float3 op_Subtraction(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x04118E10  token: 0x60009CB
        public static Unity.Mathematics.float3 op_Subtraction(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x04DB6080  token: 0x60009CC
        public static Unity.Mathematics.float3 op_Subtraction(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0AAED088  token: 0x60009CD
        public static Unity.Mathematics.float3 op_Division(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0AADD2FC  token: 0x60009CE
        public static Unity.Mathematics.float3 op_Division(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B28564C  token: 0x60009CF
        public static Unity.Mathematics.float3 op_Division(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x04DAFC00  token: 0x60009D0
        public static Unity.Mathematics.float3 op_Modulus(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x04DB6020  token: 0x60009D1
        public static Unity.Mathematics.float3 op_Modulus(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x04DB5FC0  token: 0x60009D2
        public static Unity.Mathematics.float3 op_Modulus(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x04DB5F80  token: 0x60009D3
        public static Unity.Mathematics.float3 op_Increment(Unity.Mathematics.float3 val) { }
        // RVA: 0x04DB5F40  token: 0x60009D4
        public static Unity.Mathematics.float3 op_Decrement(Unity.Mathematics.float3 val) { }
        // RVA: 0x0B28598C  token: 0x60009D5
        public static Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B2859C0  token: 0x60009D6
        public static Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B28595C  token: 0x60009D7
        public static Unity.Mathematics.bool3 op_LessThan(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B2858C8  token: 0x60009D8
        public static Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B285934  token: 0x60009D9
        public static Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B285900  token: 0x60009DA
        public static Unity.Mathematics.bool3 op_LessThanOrEqual(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B285788  token: 0x60009DB
        public static Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B2857E0  token: 0x60009DC
        public static Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B2857BC  token: 0x60009DD
        public static Unity.Mathematics.bool3 op_GreaterThan(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B2856FC  token: 0x60009DE
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B285730  token: 0x60009DF
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B285760  token: 0x60009E0
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x03F48480  token: 0x60009E1
        public static Unity.Mathematics.float3 op_UnaryNegation(Unity.Mathematics.float3 val) { }
        // RVA: 0x04D88970  token: 0x60009E2
        public static Unity.Mathematics.float3 op_UnaryPlus(Unity.Mathematics.float3 val) { }
        // RVA: 0x0B285678  token: 0x60009E3
        public static Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x05F031DC  token: 0x60009E4
        public static Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B2856C0  token: 0x60009E5
        public static Unity.Mathematics.bool3 op_Equality(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B285844  token: 0x60009E6
        public static Unity.Mathematics.bool3 op_Inequality(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B285888  token: 0x60009E7
        public static Unity.Mathematics.bool3 op_Inequality(Unity.Mathematics.float3 lhs, System.Single rhs) { }
        // RVA: 0x0B28580C  token: 0x60009E8
        public static Unity.Mathematics.bool3 op_Inequality(System.Single lhs, Unity.Mathematics.float3 rhs) { }
        // RVA: 0x030F9620  token: 0x6000A6C
        public virtual System.Boolean Equals(Unity.Mathematics.float3 rhs) { }
        // RVA: 0x0B2853CC  token: 0x6000A6D
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B285440  token: 0x6000A6E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B285464  token: 0x6000A6F
        public virtual System.String ToString() { }
        // RVA: 0x0B285524  token: 0x6000A70
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04D88970  token: 0x6000A71
        public static UnityEngine.Vector3 op_Implicit(Unity.Mathematics.float3 v) { }
        // RVA: 0x04D88970  token: 0x6000A72
        public static Unity.Mathematics.float3 op_Implicit(UnityEngine.Vector3 v) { }

    }

    // TypeToken: 0x200002D  // size: 0x28
    public sealed struct float3x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float3 c0;  // 0x10
        public Unity.Mathematics.float3 c1;  // 0x1c

        // Methods
        // RVA: 0x04D871E0  token: 0x6000A73
        public System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1) { }
        // RVA: 0x04DB6270  token: 0x6000A74
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21) { }
        // RVA: 0x04DB6250  token: 0x6000A75
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F032D0  token: 0x6000A76
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B28B60C  token: 0x6000A77
        public System.Void .ctor(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x04DB6200  token: 0x6000A78
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F03470  token: 0x6000A79
        public System.Void .ctor(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x04DB62A0  token: 0x6000A7A
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F033FC  token: 0x6000A7B
        public System.Void .ctor(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x04DB6310  token: 0x6000A7C
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28B594  token: 0x6000A7D
        public System.Void .ctor(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x0B28B6DC  token: 0x6000A7E
        public static Unity.Mathematics.float3x2 op_Multiply(Unity.Mathematics.float3x2 lhs, System.Single rhs) { }
        // RVA: 0x0B28B174  token: 0x6000A7F
        public virtual System.Boolean Equals(Unity.Mathematics.float3x2 rhs) { }
        // RVA: 0x0B28B100  token: 0x6000A80
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28B1D8  token: 0x6000A81
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28B3B4  token: 0x6000A82
        public virtual System.String ToString() { }
        // RVA: 0x0B28B200  token: 0x6000A83
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200002E  // size: 0x34
    public sealed struct float3x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float3 c0;  // 0x10
        public Unity.Mathematics.float3 c1;  // 0x1c
        public Unity.Mathematics.float3 c2;  // 0x28
        public static readonly Unity.Mathematics.float3x3 identity;  // static @ 0x0
        public static readonly Unity.Mathematics.float3x3 zero;  // static @ 0x24

        // Properties
        Unity.Mathematics.float3& Item { get; /* RVA: 0x04DB6580 */ }

        // Methods
        // RVA: 0x04D8C690  token: 0x6000A84
        public System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2) { }
        // RVA: 0x04DB63D0  token: 0x6000A85
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22) { }
        // RVA: 0x04DB64E0  token: 0x6000A86
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F03630  token: 0x6000A87
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B28BE08  token: 0x6000A88
        public System.Void .ctor(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x04DB6360  token: 0x6000A89
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F034E4  token: 0x6000A8A
        public System.Void .ctor(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x04DB6430  token: 0x6000A8B
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F036AC  token: 0x6000A8C
        public System.Void .ctor(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x04DB6510  token: 0x6000A8D
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28BD60  token: 0x6000A8E
        public System.Void .ctor(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B252214  token: 0x6000A8F
        public static Unity.Mathematics.float3x3 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B28C02C  token: 0x6000A90
        public static Unity.Mathematics.float3x3 op_Multiply(Unity.Mathematics.float3x3 lhs, System.Single rhs) { }
        // RVA: 0x0B28BF2C  token: 0x6000A91
        public static Unity.Mathematics.float3x3 op_Addition(Unity.Mathematics.float3x3 lhs, Unity.Mathematics.float3x3 rhs) { }
        // RVA: 0x0B28B7DC  token: 0x6000A93
        public virtual System.Boolean Equals(Unity.Mathematics.float3x3 rhs) { }
        // RVA: 0x0B28B760  token: 0x6000A94
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28B868  token: 0x6000A95
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28B894  token: 0x6000A96
        public virtual System.String ToString() { }
        // RVA: 0x0B28BB1C  token: 0x6000A97
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x034C93C0  token: 0x6000A98
        public System.Void .ctor(Unity.Mathematics.float4x4 f4x4) { }
        // RVA: 0x030E9010  token: 0x6000A99
        public System.Void .ctor(Unity.Mathematics.quaternion q) { }
        // RVA: 0x04CA6B90  token: 0x6000A9A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x40
    public sealed struct float3x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float3 c0;  // 0x10
        public Unity.Mathematics.float3 c1;  // 0x1c
        public Unity.Mathematics.float3 c2;  // 0x28
        public Unity.Mathematics.float3 c3;  // 0x34
        public static readonly Unity.Mathematics.float3x4 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.float3& Item { get; /* RVA: 0x04DB6580 */ }

        // Methods
        // RVA: 0x04DB65D0  token: 0x6000A9B
        public System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3 c3) { }
        // RVA: 0x04DB6820  token: 0x6000A9C
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23) { }
        // RVA: 0x04DB6590  token: 0x6000A9D
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F0394C  token: 0x6000A9E
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B28C934  token: 0x6000A9F
        public System.Void .ctor(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x04DB6790  token: 0x6000AA0
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F03878  token: 0x6000AA1
        public System.Void .ctor(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x04DB66B0  token: 0x6000AA2
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F039EC  token: 0x6000AA3
        public System.Void .ctor(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x04DB6620  token: 0x6000AA4
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28C858  token: 0x6000AA5
        public System.Void .ctor(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B25243C  token: 0x6000AA6
        public static Unity.Mathematics.float3x4 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B28C160  token: 0x6000AA8
        public virtual System.Boolean Equals(Unity.Mathematics.float3x4 rhs) { }
        // RVA: 0x0B28C0E4  token: 0x6000AA9
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28C224  token: 0x6000AAA
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28C528  token: 0x6000AAB
        public virtual System.String ToString() { }
        // RVA: 0x0B28C254  token: 0x6000AAC
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000030  // size: 0x20
    public sealed struct float4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single z;  // 0x18
        public System.Single w;  // 0x1c
        public static readonly Unity.Mathematics.float4 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.float4 xxzz { get; /* RVA: 0x04DB5370 */ }
        Unity.Mathematics.float4 xyzx { get; /* RVA: 0x04064E30 */ }
        Unity.Mathematics.float4 xzxz { get; /* RVA: 0x04DB5510 */ }
        Unity.Mathematics.float4 yyww { get; /* RVA: 0x04DB6BD0 */ }
        Unity.Mathematics.float4 yzxy { get; /* RVA: 0x04064E10 */ }
        Unity.Mathematics.float4 yzxz { get; /* RVA: 0x04064DF0 */ }
        Unity.Mathematics.float4 yzxw { get; /* RVA: 0x04DB6BF0 */ }
        Unity.Mathematics.float4 zxyy { get; /* RVA: 0x040B34F0 */ }
        Unity.Mathematics.float4 zxyz { get; /* RVA: 0x04064DD0 */ }
        Unity.Mathematics.float4 zwxy { get; /* RVA: 0x034C98E0 */ }
        Unity.Mathematics.float4 wzyx { get; /* RVA: 0x034C98C0 */ }
        Unity.Mathematics.float4 wwwx { get; /* RVA: 0x04DB6B10 */ }
        Unity.Mathematics.float4 wwww { get; /* RVA: 0x04DB6AF0 */ }
        Unity.Mathematics.float3 xxx { get; /* RVA: 0x04DB5220 */ }
        Unity.Mathematics.float3 xyz { get; /* RVA: 0x04D88970 */ set; /* RVA: 0x04D88F40 */ }
        Unity.Mathematics.float3 xyw { get; /* RVA: 0x04DB6B70 */ }
        Unity.Mathematics.float3 yxw { get; /* RVA: 0x04DB6BB0 */ }
        Unity.Mathematics.float3 yzx { get; /* RVA: 0x04DB5940 */ }
        Unity.Mathematics.float3 zwx { get; /* RVA: 0x04DB6C10 */ }
        Unity.Mathematics.float3 wzy { get; /* RVA: 0x04DB6B50 */ }
        Unity.Mathematics.float3 www { get; /* RVA: 0x04DB6AE0 */ }
        Unity.Mathematics.float2 xx { get; /* RVA: 0x04DB5210 */ }
        Unity.Mathematics.float2 xy { get; /* RVA: 0x04D889F0 */ set; /* RVA: 0x04DB4680 */ }
        Unity.Mathematics.float2 xz { get; /* RVA: 0x04D88A40 */ }
        Unity.Mathematics.float2 yy { get; /* RVA: 0x04DB57B0 */ }
        Unity.Mathematics.float2 yw { get; /* RVA: 0x04DB6B90 */ }
        Unity.Mathematics.float2 zw { get; /* RVA: 0x04D9C5E0 */ }
        Unity.Mathematics.float2 wz { get; /* RVA: 0x04DB6B30 */ }
        System.Single Item { get; /* RVA: 0x04DB5200 */ set; /* RVA: 0x04DB60B0 */ }

        // Methods
        // RVA: 0x04D88AD0  token: 0x6000AAD
        public System.Void .ctor(System.Single x, System.Single y, System.Single z, System.Single w) { }
        // RVA: 0x04DB69F0  token: 0x6000AAE
        public System.Void .ctor(System.Single x, System.Single y, Unity.Mathematics.float2 zw) { }
        // RVA: 0x04DB6A90  token: 0x6000AAF
        public System.Void .ctor(System.Single x, Unity.Mathematics.float2 yz, System.Single w) { }
        // RVA: 0x04DB6AC0  token: 0x6000AB0
        public System.Void .ctor(System.Single x, Unity.Mathematics.float3 yzw) { }
        // RVA: 0x04DB69A0  token: 0x6000AB1
        public System.Void .ctor(Unity.Mathematics.float2 xy, System.Single z, System.Single w) { }
        // RVA: 0x04DB68A0  token: 0x6000AB2
        public System.Void .ctor(Unity.Mathematics.float2 xy, Unity.Mathematics.float2 zw) { }
        // RVA: 0x04DB6980  token: 0x6000AB3
        public System.Void .ctor(Unity.Mathematics.float3 xyz, System.Single w) { }
        // RVA: 0x04D92B90  token: 0x6000AB4
        public System.Void .ctor(Unity.Mathematics.float4 xyzw) { }
        // RVA: 0x04DB6930  token: 0x6000AB5
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F03D18  token: 0x6000AB6
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F03D5C  token: 0x6000AB7
        public System.Void .ctor(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB6A20  token: 0x6000AB8
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB6A80  token: 0x6000AB9
        public System.Void .ctor(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DB68E0  token: 0x6000ABA
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB6A30  token: 0x6000ABB
        public System.Void .ctor(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B28CE30  token: 0x6000ABC
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B28CDD8  token: 0x6000ABD
        public System.Void .ctor(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DB69D0  token: 0x6000ABE
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB6940  token: 0x6000ABF
        public System.Void .ctor(Unity.Mathematics.double4 v) { }
        // RVA: 0x04DAF890  token: 0x6000AC0
        public static Unity.Mathematics.float4 op_Implicit(System.Single v) { }
        // RVA: 0x04DAF920  token: 0x6000AC1
        public static Unity.Mathematics.float4 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B252674  token: 0x6000AC2
        public static Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.int4 v) { }
        // RVA: 0x04DAF970  token: 0x6000AC3
        public static Unity.Mathematics.float4 op_Implicit(System.UInt32 v) { }
        // RVA: 0x0B2526D0  token: 0x6000AC4
        public static Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.uint4 v) { }
        // RVA: 0x0B2525CC  token: 0x6000AC5
        public static Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.half4 v) { }
        // RVA: 0x04DAF900  token: 0x6000AC6
        public static Unity.Mathematics.float4 op_Explicit(System.Double v) { }
        // RVA: 0x0B252584  token: 0x6000AC7
        public static Unity.Mathematics.float4 op_Explicit(Unity.Mathematics.double4 v) { }
        // RVA: 0x030E8750  token: 0x6000AC8
        public static Unity.Mathematics.float4 op_Multiply(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0308B480  token: 0x6000AC9
        public static Unity.Mathematics.float4 op_Multiply(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x034C9970  token: 0x6000ACA
        public static Unity.Mathematics.float4 op_Multiply(System.Single lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x030E87A0  token: 0x6000ACB
        public static Unity.Mathematics.float4 op_Addition(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x04DA8140  token: 0x6000ACC
        public static Unity.Mathematics.float4 op_Addition(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x03F5AF20  token: 0x6000ACD
        public static Unity.Mathematics.float4 op_Subtraction(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x04DA8160  token: 0x6000ACE
        public static Unity.Mathematics.float4 op_Subtraction(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x04DB6C30  token: 0x6000ACF
        public static Unity.Mathematics.float4 op_Subtraction(System.Single lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0AAED7A8  token: 0x6000AD0
        public static Unity.Mathematics.float4 op_Division(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0AAED7EC  token: 0x6000AD1
        public static Unity.Mathematics.float4 op_Division(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x0B28CE78  token: 0x6000AD2
        public static Unity.Mathematics.float4 op_Division(System.Single lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0B28CF94  token: 0x6000AD3
        public static Unity.Mathematics.bool4 op_LessThan(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x0B28CF58  token: 0x6000AD4
        public static Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0B28CF14  token: 0x6000AD5
        public static Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x04DB6C70  token: 0x6000AD6
        public static Unity.Mathematics.float4 op_UnaryNegation(Unity.Mathematics.float4 val) { }
        // RVA: 0x0B28CEB0  token: 0x6000AD7
        public static Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs) { }
        // RVA: 0x05F03DB0  token: 0x6000AD8
        public static Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.float4 lhs, System.Single rhs) { }
        // RVA: 0x0415FC30  token: 0x6000AF9
        public virtual System.Boolean Equals(Unity.Mathematics.float4 rhs) { }
        // RVA: 0x0B28CAAC  token: 0x6000AFA
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B256480  token: 0x6000AFB
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28CB18  token: 0x6000AFC
        public virtual System.String ToString() { }
        // RVA: 0x0B28CC84  token: 0x6000AFD
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x0328FB00  token: 0x6000AFE
        public static Unity.Mathematics.float4 op_Implicit(UnityEngine.Vector4 v) { }
        // RVA: 0x0328FB00  token: 0x6000AFF
        public static UnityEngine.Vector4 op_Implicit(Unity.Mathematics.float4 v) { }

    }

    // TypeToken: 0x2000032  // size: 0x30
    public sealed struct float4x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float4 c0;  // 0x10
        public Unity.Mathematics.float4 c1;  // 0x20

        // Methods
        // RVA: 0x04D8C810  token: 0x6000B00
        public System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1) { }
        // RVA: 0x04DB6D40  token: 0x6000B01
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21, System.Single m30, System.Single m31) { }
        // RVA: 0x04DB6D20  token: 0x6000B02
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F04084  token: 0x6000B03
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F03EBC  token: 0x6000B04
        public System.Void .ctor(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x03BACC20  token: 0x6000B05
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B28D554  token: 0x6000B06
        public System.Void .ctor(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x05F04054  token: 0x6000B07
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B28D500  token: 0x6000B08
        public System.Void .ctor(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x04DB6C90  token: 0x6000B09
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28D5F8  token: 0x6000B0A
        public System.Void .ctor(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B252804  token: 0x6000B0B
        public static Unity.Mathematics.float4x2 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B28CFBC  token: 0x6000B0C
        public virtual System.Boolean Equals(Unity.Mathematics.float4x2 rhs) { }
        // RVA: 0x0B28D004  token: 0x6000B0D
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28D078  token: 0x6000B0E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28D2B0  token: 0x6000B0F
        public virtual System.String ToString() { }
        // RVA: 0x0B28D09C  token: 0x6000B10
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000033  // size: 0x40
    public sealed struct float4x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float4 c0;  // 0x10
        public Unity.Mathematics.float4 c1;  // 0x20
        public Unity.Mathematics.float4 c2;  // 0x30

        // Methods
        // RVA: 0x04DA0760  token: 0x6000B11
        public System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2) { }
        // RVA: 0x04DB6ED0  token: 0x6000B12
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22, System.Single m30, System.Single m31, System.Single m32) { }
        // RVA: 0x04DB6E70  token: 0x6000B13
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F04158  token: 0x6000B14
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F040A8  token: 0x6000B15
        public System.Void .ctor(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x04DB6EA0  token: 0x6000B16
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B28DD6C  token: 0x6000B17
        public System.Void .ctor(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x05F04394  token: 0x6000B18
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B28DE5C  token: 0x6000B19
        public System.Void .ctor(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x04DB6DB0  token: 0x6000B1A
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28DED4  token: 0x6000B1B
        public System.Void .ctor(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B2529E4  token: 0x6000B1C
        public static Unity.Mathematics.float4x3 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B28E01C  token: 0x6000B1D
        public static Unity.Mathematics.float4x3 op_Multiply(Unity.Mathematics.float4x3 lhs, Unity.Mathematics.float4x3 rhs) { }
        // RVA: 0x0B28E0D8  token: 0x6000B1E
        public static Unity.Mathematics.float4x3 op_Multiply(Unity.Mathematics.float4x3 lhs, System.Single rhs) { }
        // RVA: 0x0B28DF60  token: 0x6000B1F
        public static Unity.Mathematics.float4x3 op_Addition(Unity.Mathematics.float4x3 lhs, Unity.Mathematics.float4x3 rhs) { }
        // RVA: 0x0B28D6D8  token: 0x6000B20
        public virtual System.Boolean Equals(Unity.Mathematics.float4x3 rhs) { }
        // RVA: 0x0B28D65C  token: 0x6000B21
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28D738  token: 0x6000B22
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28DA3C  token: 0x6000B23
        public virtual System.String ToString() { }
        // RVA: 0x0B28D768  token: 0x6000B24
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000034  // size: 0x50
    public sealed struct float4x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float4 c0;  // 0x10
        public Unity.Mathematics.float4 c1;  // 0x20
        public Unity.Mathematics.float4 c2;  // 0x30
        public Unity.Mathematics.float4 c3;  // 0x40
        public static readonly Unity.Mathematics.float4x4 identity;  // static @ 0x0
        public static readonly Unity.Mathematics.float4x4 zero;  // static @ 0x40

        // Properties
        Unity.Mathematics.float4& Item { get; /* RVA: 0x04DB7090 */ }

        // Methods
        // RVA: 0x04DB3220  token: 0x6000B25
        public System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2, Unity.Mathematics.float4 c3) { }
        // RVA: 0x033A3ED0  token: 0x6000B26
        public System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23, System.Single m30, System.Single m31, System.Single m32, System.Single m33) { }
        // RVA: 0x0308AEF0  token: 0x6000B27
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F044B8  token: 0x6000B28
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F043D8  token: 0x6000B29
        public System.Void .ctor(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x0308BCF0  token: 0x6000B2A
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B28F11C  token: 0x6000B2B
        public System.Void .ctor(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x05F045B4  token: 0x6000B2C
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B28F084  token: 0x6000B2D
        public System.Void .ctor(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x04DB6F90  token: 0x6000B2E
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28EFCC  token: 0x6000B2F
        public System.Void .ctor(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0308AEC0  token: 0x6000B30
        public static Unity.Mathematics.float4x4 op_Implicit(System.Single v) { }
        // RVA: 0x0B252EE4  token: 0x6000B31
        public static Unity.Mathematics.float4x4 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B28F440  token: 0x6000B32
        public static Unity.Mathematics.float4x4 op_Multiply(Unity.Mathematics.float4x4 lhs, System.Single rhs) { }
        // RVA: 0x0B28F254  token: 0x6000B33
        public static Unity.Mathematics.bool4x4 op_Equality(Unity.Mathematics.float4x4 lhs, Unity.Mathematics.float4x4 rhs) { }
        // RVA: 0x0B28E164  token: 0x6000B35
        public virtual System.Boolean Equals(Unity.Mathematics.float4x4 rhs) { }
        // RVA: 0x0B28E1E0  token: 0x6000B36
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28E264  token: 0x6000B37
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28E540  token: 0x6000B38
        public virtual System.String ToString() { }
        // RVA: 0x0B28E950  token: 0x6000B39
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x0B28F328  token: 0x6000B3A
        public static Unity.Mathematics.float4x4 op_Implicit(UnityEngine.Matrix4x4 m) { }
        // RVA: 0x0334D940  token: 0x6000B3B
        public static UnityEngine.Matrix4x4 op_Implicit(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x0B28ECE4  token: 0x6000B3C
        public System.Void .ctor(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B28EDA4  token: 0x6000B3D
        public System.Void .ctor(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B28EEA8  token: 0x6000B3E
        public System.Void .ctor(Unity.Mathematics.RigidTransform transform) { }
        // RVA: 0x0B28E29C  token: 0x6000B3F
        public static Unity.Mathematics.float4x4 Ortho(System.Single width, System.Single height, System.Single near, System.Single far) { }
        // RVA: 0x0B28E388  token: 0x6000B40
        public static Unity.Mathematics.float4x4 TRS(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale) { }
        // RVA: 0x04CC6270  token: 0x6000B41
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000035  // size: 0x12
    public sealed struct half : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.UInt16 value;  // 0x10
        public static readonly Unity.Mathematics.half zero;  // static @ 0x0

        // Methods
        // RVA: 0x04D87160  token: 0x6000B42
        public System.Void .ctor(Unity.Mathematics.half x) { }
        // RVA: 0x0B28FFF0  token: 0x6000B43
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B28FFD4  token: 0x6000B44
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B253650  token: 0x6000B45
        public static Unity.Mathematics.half op_Explicit(System.Single v) { }
        // RVA: 0x0B253644  token: 0x6000B46
        public static Unity.Mathematics.half op_Explicit(System.Double v) { }
        // RVA: 0x0B290028  token: 0x6000B47
        public static System.Single op_Implicit(Unity.Mathematics.half d) { }
        // RVA: 0x0B290010  token: 0x6000B48
        public static System.Double op_Implicit(Unity.Mathematics.half d) { }
        // RVA: 0x0B290008  token: 0x6000B49
        public static System.Boolean op_Equality(Unity.Mathematics.half lhs, Unity.Mathematics.half rhs) { }
        // RVA: 0x03F4C8B0  token: 0x6000B4A
        public virtual System.Boolean Equals(Unity.Mathematics.half rhs) { }
        // RVA: 0x0B28FF1C  token: 0x6000B4B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x04D87110  token: 0x6000B4C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28FF7C  token: 0x6000B4D
        public virtual System.String ToString() { }
        // RVA: 0x0B28FFA0  token: 0x6000B4E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct half2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.half x;  // 0x10
        public Unity.Mathematics.half y;  // 0x12

        // Methods
        // RVA: 0x04DA4480  token: 0x6000B4F
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y) { }
        // RVA: 0x04DB70B0  token: 0x6000B50
        public System.Void .ctor(Unity.Mathematics.half2 xy) { }
        // RVA: 0x04DB70A0  token: 0x6000B51
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B28F708  token: 0x6000B52
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B28F730  token: 0x6000B53
        public System.Void .ctor(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B28F6D8  token: 0x6000B54
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28F760  token: 0x6000B55
        public System.Void .ctor(Unity.Mathematics.double2 v) { }
        // RVA: 0x0B2533B0  token: 0x6000B56
        public static Unity.Mathematics.half2 op_Explicit(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B28F56C  token: 0x6000B57
        public virtual System.Boolean Equals(Unity.Mathematics.half2 rhs) { }
        // RVA: 0x0B28F4F8  token: 0x6000B58
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28F580  token: 0x6000B59
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28F64C  token: 0x6000B5A
        public virtual System.String ToString() { }
        // RVA: 0x0B28F5C8  token: 0x6000B5B
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000038  // size: 0x16
    public sealed struct half3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.half x;  // 0x10
        public Unity.Mathematics.half y;  // 0x12
        public Unity.Mathematics.half z;  // 0x14

        // Methods
        // RVA: 0x04DB22F0  token: 0x6000B5C
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z) { }
        // RVA: 0x04DB70C0  token: 0x6000B5D
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half2 yz) { }
        // RVA: 0x04DB70E0  token: 0x6000B5E
        public System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half z) { }
        // RVA: 0x04DB70F0  token: 0x6000B5F
        public System.Void .ctor(Unity.Mathematics.half3 xyz) { }
        // RVA: 0x04DB7110  token: 0x6000B60
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B28FA54  token: 0x6000B61
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B28FA8C  token: 0x6000B62
        public System.Void .ctor(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B28F9C8  token: 0x6000B63
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28FA0C  token: 0x6000B64
        public System.Void .ctor(Unity.Mathematics.double3 v) { }
        // RVA: 0x0B253474  token: 0x6000B65
        public static Unity.Mathematics.half3 op_Explicit(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B253428  token: 0x6000B66
        public static Unity.Mathematics.half3 op_Explicit(Unity.Mathematics.double3 v) { }
        // RVA: 0x0B28F794  token: 0x6000B67
        public virtual System.Boolean Equals(Unity.Mathematics.half3 rhs) { }
        // RVA: 0x0B28F7B8  token: 0x6000B68
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28F844  token: 0x6000B69
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28F910  token: 0x6000B6A
        public virtual System.String ToString() { }
        // RVA: 0x0B28F868  token: 0x6000B6B
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200003A  // size: 0x18
    public sealed struct half4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.half x;  // 0x10
        public Unity.Mathematics.half y;  // 0x12
        public Unity.Mathematics.half z;  // 0x14
        public Unity.Mathematics.half w;  // 0x16
        public static readonly Unity.Mathematics.half4 zero;  // static @ 0x0

        // Methods
        // RVA: 0x04DB23B0  token: 0x6000B6C
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z, Unity.Mathematics.half w) { }
        // RVA: 0x04DB7190  token: 0x6000B6D
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half2 zw) { }
        // RVA: 0x04DB71B0  token: 0x6000B6E
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half2 yz, Unity.Mathematics.half w) { }
        // RVA: 0x04DB7200  token: 0x6000B6F
        public System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half3 yzw) { }
        // RVA: 0x04DB7120  token: 0x6000B70
        public System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half z, Unity.Mathematics.half w) { }
        // RVA: 0x04DB7140  token: 0x6000B71
        public System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half2 zw) { }
        // RVA: 0x04DB71E0  token: 0x6000B72
        public System.Void .ctor(Unity.Mathematics.half3 xyz, Unity.Mathematics.half w) { }
        // RVA: 0x04DB7160  token: 0x6000B73
        public System.Void .ctor(Unity.Mathematics.half4 xyzw) { }
        // RVA: 0x04DB71D0  token: 0x6000B74
        public System.Void .ctor(Unity.Mathematics.half v) { }
        // RVA: 0x0B28FED8  token: 0x6000B75
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B28FDDC  token: 0x6000B76
        public System.Void .ctor(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B28FE84  token: 0x6000B77
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B28FE28  token: 0x6000B78
        public System.Void .ctor(Unity.Mathematics.double4 v) { }
        // RVA: 0x0B2535F0  token: 0x6000B79
        public static Unity.Mathematics.half4 op_Explicit(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B287BE0  token: 0x6000B7A
        public virtual System.Boolean Equals(Unity.Mathematics.half4 rhs) { }
        // RVA: 0x0B28FAC8  token: 0x6000B7B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B28FB28  token: 0x6000B7C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B28FB30  token: 0x6000B7D
        public virtual System.String ToString() { }
        // RVA: 0x0B28FC94  token: 0x6000B7E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200003C  // size: 0x18
    public sealed struct int2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Int32 x;  // 0x10
        public System.Int32 y;  // 0x14
        public static readonly Unity.Mathematics.int2 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.int2 xy { get; /* RVA: 0x04DB7290 */ }
        System.Int32 Item { set; /* RVA: 0x04DB73A0 */ }

        // Methods
        // RVA: 0x04D85C40  token: 0x6000B7F
        public System.Void .ctor(System.Int32 x, System.Int32 y) { }
        // RVA: 0x04DB7280  token: 0x6000B80
        public System.Void .ctor(Unity.Mathematics.int2 xy) { }
        // RVA: 0x04DB7220  token: 0x6000B81
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B2901E8  token: 0x6000B82
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2901F8  token: 0x6000B83
        public System.Void .ctor(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB7220  token: 0x6000B84
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB7280  token: 0x6000B85
        public System.Void .ctor(Unity.Mathematics.uint2 v) { }
        // RVA: 0x04DB7270  token: 0x6000B86
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB7240  token: 0x6000B87
        public System.Void .ctor(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DB7260  token: 0x6000B88
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB7230  token: 0x6000B89
        public System.Void .ctor(Unity.Mathematics.double2 v) { }
        // RVA: 0x04DB02A0  token: 0x6000B8A
        public static Unity.Mathematics.int2 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DB02A0  token: 0x6000B8B
        public static Unity.Mathematics.int2 op_Explicit(System.UInt32 v) { }
        // RVA: 0x04DAB5F0  token: 0x6000B8C
        public static Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.uint2 v) { }
        // RVA: 0x04DB0280  token: 0x6000B8D
        public static Unity.Mathematics.int2 op_Explicit(System.Single v) { }
        // RVA: 0x04DB0240  token: 0x6000B8E
        public static Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.float2 v) { }
        // RVA: 0x04DB0260  token: 0x6000B8F
        public static Unity.Mathematics.int2 op_Explicit(System.Double v) { }
        // RVA: 0x04DB02B0  token: 0x6000B90
        public static Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.double2 v) { }
        // RVA: 0x04DB7310  token: 0x6000B91
        public static Unity.Mathematics.int2 op_Multiply(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs) { }
        // RVA: 0x04DB72F0  token: 0x6000B92
        public static Unity.Mathematics.int2 op_Multiply(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x04D85C70  token: 0x6000B93
        public static Unity.Mathematics.int2 op_Addition(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs) { }
        // RVA: 0x04DB72B0  token: 0x6000B94
        public static Unity.Mathematics.int2 op_Addition(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB7360  token: 0x6000B95
        public static Unity.Mathematics.int2 op_Subtraction(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x0B290214  token: 0x6000B96
        public static Unity.Mathematics.int2 op_Division(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x0B290258  token: 0x6000B97
        public static Unity.Mathematics.bool2 op_GreaterThan(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x0B290238  token: 0x6000B98
        public static Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.int2 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB7380  token: 0x6000B99
        public static Unity.Mathematics.int2 op_UnaryNegation(Unity.Mathematics.int2 val) { }
        // RVA: 0x04DB7330  token: 0x6000B9A
        public static Unity.Mathematics.int2 op_RightShift(Unity.Mathematics.int2 x, System.Int32 n) { }
        // RVA: 0x04DB72D0  token: 0x6000B9B
        public static Unity.Mathematics.int2 op_BitwiseOr(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs) { }
        // RVA: 0x063516B4  token: 0x6000B9E
        public virtual System.Boolean Equals(Unity.Mathematics.int2 rhs) { }
        // RVA: 0x0B290030  token: 0x6000B9F
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2900A4  token: 0x6000BA0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B290160  token: 0x6000BA1
        public virtual System.String ToString() { }
        // RVA: 0x0B2900D8  token: 0x6000BA2
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200003E  // size: 0x20
    public sealed struct int2x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int2 c0;  // 0x10
        public Unity.Mathematics.int2 c1;  // 0x18
        public static readonly Unity.Mathematics.int2x2 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D861B0  token: 0x6000BA3
        public System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1) { }
        // RVA: 0x04DB74F0  token: 0x6000BA4
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11) { }
        // RVA: 0x04DB7430  token: 0x6000BA5
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B290618  token: 0x6000BA6
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2905A8  token: 0x6000BA7
        public System.Void .ctor(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x04DB7430  token: 0x6000BA8
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB7520  token: 0x6000BA9
        public System.Void .ctor(Unity.Mathematics.uint2x2 v) { }
        // RVA: 0x04DB73B0  token: 0x6000BAA
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB74A0  token: 0x6000BAB
        public System.Void .ctor(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x04DB7460  token: 0x6000BAC
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB73F0  token: 0x6000BAD
        public System.Void .ctor(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B287F80  token: 0x6000BAE
        public virtual System.Boolean Equals(Unity.Mathematics.int2x2 rhs) { }
        // RVA: 0x0B290270  token: 0x6000BAF
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2902DC  token: 0x6000BB0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2902F8  token: 0x6000BB1
        public virtual System.String ToString() { }
        // RVA: 0x0B290454  token: 0x6000BB2
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04CEAE50  token: 0x6000BB3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x28
    public sealed struct int2x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int2 c0;  // 0x10
        public Unity.Mathematics.int2 c1;  // 0x18
        public Unity.Mathematics.int2 c2;  // 0x20

        // Properties
        Unity.Mathematics.int2& Item { get; /* RVA: 0x04DB77A0 */ }

        // Methods
        // RVA: 0x04D8C0D0  token: 0x6000BB4
        public System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2) { }
        // RVA: 0x04DB7750  token: 0x6000BB5
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12) { }
        // RVA: 0x04DB76C0  token: 0x6000BB6
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B290B74  token: 0x6000BB7
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B290ADC  token: 0x6000BB8
        public System.Void .ctor(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x04DB76C0  token: 0x6000BB9
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB7700  token: 0x6000BBA
        public System.Void .ctor(Unity.Mathematics.uint2x3 v) { }
        // RVA: 0x04DB7610  token: 0x6000BBB
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB7660  token: 0x6000BBC
        public System.Void .ctor(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x04DB7560  token: 0x6000BBD
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB75B0  token: 0x6000BBE
        public System.Void .ctor(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B290660  token: 0x6000BC0
        public virtual System.Boolean Equals(Unity.Mathematics.int2x3 rhs) { }
        // RVA: 0x0B2906C4  token: 0x6000BC1
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B290738  token: 0x6000BC2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B290760  token: 0x6000BC3
        public virtual System.String ToString() { }
        // RVA: 0x0B290928  token: 0x6000BC4
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000040  // size: 0x30
    public sealed struct int2x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int2 c0;  // 0x10
        public Unity.Mathematics.int2 c1;  // 0x18
        public Unity.Mathematics.int2 c2;  // 0x20
        public Unity.Mathematics.int2 c3;  // 0x28

        // Methods
        // RVA: 0x04D861C0  token: 0x6000BC5
        public System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2, Unity.Mathematics.int2 c3) { }
        // RVA: 0x04DB78A0  token: 0x6000BC6
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13) { }
        // RVA: 0x04DB7980  token: 0x6000BC7
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B2911F4  token: 0x6000BC8
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291128  token: 0x6000BC9
        public System.Void .ctor(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x04DB7980  token: 0x6000BCA
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04DB7830  token: 0x6000BCB
        public System.Void .ctor(Unity.Mathematics.uint2x4 v) { }
        // RVA: 0x04DB79D0  token: 0x6000BCC
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB77B0  token: 0x6000BCD
        public System.Void .ctor(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x04DB7A40  token: 0x6000BCE
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB7900  token: 0x6000BCF
        public System.Void .ctor(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B290C4C  token: 0x6000BD0
        public virtual System.Boolean Equals(Unity.Mathematics.int2x4 rhs) { }
        // RVA: 0x0B290BD8  token: 0x6000BD1
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B290CC0  token: 0x6000BD2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B290EF8  token: 0x6000BD3
        public virtual System.String ToString() { }
        // RVA: 0x0B290CE4  token: 0x6000BD4
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000041  // size: 0x1C
    public sealed struct int3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Int32 x;  // 0x10
        public System.Int32 y;  // 0x14
        public System.Int32 z;  // 0x18
        public static readonly Unity.Mathematics.int3 zero;  // static @ 0x0

        // Properties
        Unity.Mathematics.int3 xyz { set; /* RVA: 0x04D88F40 */ }
        Unity.Mathematics.int3 xzy { get; /* RVA: 0x04DB5530 */ }
        Unity.Mathematics.int2 xy { get; /* RVA: 0x04DB7290 */ set; /* RVA: 0x04DB7280 */ }
        Unity.Mathematics.int2 yz { get; /* RVA: 0x04DB7B70 */ }
        Unity.Mathematics.int2 zx { get; /* RVA: 0x04DB7B90 */ }
        System.Int32 Item { get; /* RVA: 0x04DB7B60 */ set; /* RVA: 0x04DB73A0 */ }

        // Methods
        // RVA: 0x04D85D60  token: 0x6000BD5
        public System.Void .ctor(System.Int32 x, System.Int32 y, System.Int32 z) { }
        // RVA: 0x04DB7AB0  token: 0x6000BD6
        public System.Void .ctor(System.Int32 x, Unity.Mathematics.int2 yz) { }
        // RVA: 0x04DB7B50  token: 0x6000BD7
        public System.Void .ctor(Unity.Mathematics.int2 xy, System.Int32 z) { }
        // RVA: 0x04D88F40  token: 0x6000BD8
        public System.Void .ctor(Unity.Mathematics.int3 xyz) { }
        // RVA: 0x04DB7B20  token: 0x6000BD9
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B29149C  token: 0x6000BDA
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291470  token: 0x6000BDB
        public System.Void .ctor(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB7B20  token: 0x6000BDC
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04D88F40  token: 0x6000BDD
        public System.Void .ctor(Unity.Mathematics.uint3 v) { }
        // RVA: 0x04DB7AE0  token: 0x6000BDE
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB7AC0  token: 0x6000BDF
        public System.Void .ctor(Unity.Mathematics.float3 v) { }
        // RVA: 0x04DB7B00  token: 0x6000BE0
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB7B30  token: 0x6000BE1
        public System.Void .ctor(Unity.Mathematics.double3 v) { }
        // RVA: 0x04DB02F0  token: 0x6000BE2
        public static Unity.Mathematics.int3 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DB02F0  token: 0x6000BE3
        public static Unity.Mathematics.int3 op_Explicit(System.UInt32 v) { }
        // RVA: 0x04DB0370  token: 0x6000BE4
        public static Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.uint3 v) { }
        // RVA: 0x04DB03C0  token: 0x6000BE5
        public static Unity.Mathematics.int3 op_Explicit(System.Single v) { }
        // RVA: 0x04DB0300  token: 0x6000BE6
        public static Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.float3 v) { }
        // RVA: 0x04DB03A0  token: 0x6000BE7
        public static Unity.Mathematics.int3 op_Explicit(System.Double v) { }
        // RVA: 0x04DB03E0  token: 0x6000BE8
        public static Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.double3 v) { }
        // RVA: 0x03C87E40  token: 0x6000BE9
        public static Unity.Mathematics.int3 op_Multiply(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x04DB7C00  token: 0x6000BEA
        public static Unity.Mathematics.int3 op_Multiply(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x0338E380  token: 0x6000BEB
        public static Unity.Mathematics.int3 op_Addition(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x04DB7BB0  token: 0x6000BEC
        public static Unity.Mathematics.int3 op_Addition(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB7C70  token: 0x6000BED
        public static Unity.Mathematics.int3 op_Subtraction(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x04DB7C50  token: 0x6000BEE
        public static Unity.Mathematics.int3 op_Subtraction(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x0B2914B0  token: 0x6000BEF
        public static Unity.Mathematics.int3 op_Division(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x0B291544  token: 0x6000BF0
        public static Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x0B291524  token: 0x6000BF1
        public static Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x0B2914D8  token: 0x6000BF2
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x0B291504  token: 0x6000BF3
        public static Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.int3 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB7C90  token: 0x6000BF4
        public static Unity.Mathematics.int3 op_UnaryNegation(Unity.Mathematics.int3 val) { }
        // RVA: 0x04DB7C20  token: 0x6000BF5
        public static Unity.Mathematics.int3 op_RightShift(Unity.Mathematics.int3 x, System.Int32 n) { }
        // RVA: 0x04DB7BE0  token: 0x6000BF6
        public static Unity.Mathematics.int3 op_BitwiseOr(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs) { }
        // RVA: 0x06351A88  token: 0x6000BFF
        public virtual System.Boolean Equals(Unity.Mathematics.int3 rhs) { }
        // RVA: 0x0B291278  token: 0x6000C00
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x07042C24  token: 0x6000C01
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2913BC  token: 0x6000C02
        public virtual System.String ToString() { }
        // RVA: 0x0B29130C  token: 0x6000C03
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000043  // size: 0x28
    public sealed struct int3x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int3 c0;  // 0x10
        public Unity.Mathematics.int3 c1;  // 0x1c

        // Methods
        // RVA: 0x04D871E0  token: 0x6000C04
        public System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1) { }
        // RVA: 0x04DB7CB0  token: 0x6000C05
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21) { }
        // RVA: 0x04DB7D40  token: 0x6000C06
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F051A0  token: 0x6000C07
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291A00  token: 0x6000C08
        public System.Void .ctor(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x04DB7D40  token: 0x6000C09
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F05370  token: 0x6000C0A
        public System.Void .ctor(Unity.Mathematics.uint3x2 v) { }
        // RVA: 0x04DB7CF0  token: 0x6000C0B
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F053E4  token: 0x6000C0C
        public System.Void .ctor(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x04DB7D70  token: 0x6000C0D
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B291988  token: 0x6000C0E
        public System.Void .ctor(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x066ACFB0  token: 0x6000C0F
        public virtual System.Boolean Equals(Unity.Mathematics.int3x2 rhs) { }
        // RVA: 0x0B291570  token: 0x6000C10
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B2915E4  token: 0x6000C11
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2917C0  token: 0x6000C12
        public virtual System.String ToString() { }
        // RVA: 0x0B29160C  token: 0x6000C13
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000044  // size: 0x34
    public sealed struct int3x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int3 c0;  // 0x10
        public Unity.Mathematics.int3 c1;  // 0x1c
        public Unity.Mathematics.int3 c2;  // 0x28
        public static readonly Unity.Mathematics.int3x3 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D8C690  token: 0x6000C14
        public System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2) { }
        // RVA: 0x04B832D0  token: 0x6000C15
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22) { }
        // RVA: 0x04DB7DC0  token: 0x6000C16
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F05648  token: 0x6000C17
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2920DC  token: 0x6000C18
        public System.Void .ctor(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x04DB7DC0  token: 0x6000C19
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F056D8  token: 0x6000C1A
        public System.Void .ctor(Unity.Mathematics.uint3x3 v) { }
        // RVA: 0x04DB7E80  token: 0x6000C1B
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F055A4  token: 0x6000C1C
        public System.Void .ctor(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x04DB7E10  token: 0x6000C1D
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B292224  token: 0x6000C1E
        public System.Void .ctor(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B291B68  token: 0x6000C1F
        public virtual System.Boolean Equals(Unity.Mathematics.int3x3 rhs) { }
        // RVA: 0x0B291AEC  token: 0x6000C20
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B291C08  token: 0x6000C21
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B291E78  token: 0x6000C22
        public virtual System.String ToString() { }
        // RVA: 0x0B291C34  token: 0x6000C23
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04B83330  token: 0x6000C24
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x40
    public sealed struct int3x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int3 c0;  // 0x10
        public Unity.Mathematics.int3 c1;  // 0x1c
        public Unity.Mathematics.int3 c2;  // 0x28
        public Unity.Mathematics.int3 c3;  // 0x34

        // Methods
        // RVA: 0x04DB65D0  token: 0x6000C25
        public System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2, Unity.Mathematics.int3 c3) { }
        // RVA: 0x04DB8070  token: 0x6000C26
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23) { }
        // RVA: 0x04DB7F80  token: 0x6000C27
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F059CC  token: 0x6000C28
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B292A08  token: 0x6000C29
        public System.Void .ctor(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x04DB7F80  token: 0x6000C2A
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F05824  token: 0x6000C2B
        public System.Void .ctor(Unity.Mathematics.uint3x4 v) { }
        // RVA: 0x04DB7EF0  token: 0x6000C2C
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F058F8  token: 0x6000C2D
        public System.Void .ctor(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x04DB7FE0  token: 0x6000C2E
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B292B9C  token: 0x6000C2F
        public System.Void .ctor(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B292348  token: 0x6000C30
        public virtual System.Boolean Equals(Unity.Mathematics.int3x4 rhs) { }
        // RVA: 0x0B2922CC  token: 0x6000C31
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B292404  token: 0x6000C32
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B292434  token: 0x6000C33
        public virtual System.String ToString() { }
        // RVA: 0x0B292734  token: 0x6000C34
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000046  // size: 0x20
    public sealed struct int4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Int32 x;  // 0x10
        public System.Int32 y;  // 0x14
        public System.Int32 z;  // 0x18
        public System.Int32 w;  // 0x1c
        public static readonly Unity.Mathematics.int4 zero;  // static @ 0x0

        // Properties
        System.Int32 Item { get; /* RVA: 0x04DB7B60 */ set; /* RVA: 0x04DB73A0 */ }

        // Methods
        // RVA: 0x04D88C10  token: 0x6000C35
        public System.Void .ctor(System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 w) { }
        // RVA: 0x04DB8100  token: 0x6000C36
        public System.Void .ctor(System.Int32 x, System.Int32 y, Unity.Mathematics.int2 zw) { }
        // RVA: 0x04DB8200  token: 0x6000C37
        public System.Void .ctor(System.Int32 x, Unity.Mathematics.int2 yz, System.Int32 w) { }
        // RVA: 0x04DB81D0  token: 0x6000C38
        public System.Void .ctor(System.Int32 x, Unity.Mathematics.int3 yzw) { }
        // RVA: 0x04DB8220  token: 0x6000C39
        public System.Void .ctor(Unity.Mathematics.int2 xy, System.Int32 z, System.Int32 w) { }
        // RVA: 0x04DB8120  token: 0x6000C3A
        public System.Void .ctor(Unity.Mathematics.int2 xy, Unity.Mathematics.int2 zw) { }
        // RVA: 0x04DB81B0  token: 0x6000C3B
        public System.Void .ctor(Unity.Mathematics.int3 xyz, System.Int32 w) { }
        // RVA: 0x04D92B90  token: 0x6000C3C
        public System.Void .ctor(Unity.Mathematics.int4 xyzw) { }
        // RVA: 0x04DB8160  token: 0x6000C3D
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B292FB0  token: 0x6000C3E
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B292FCC  token: 0x6000C3F
        public System.Void .ctor(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB8160  token: 0x6000C40
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x04D92B90  token: 0x6000C41
        public System.Void .ctor(Unity.Mathematics.uint4 v) { }
        // RVA: 0x04DB8140  token: 0x6000C42
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x04DB81F0  token: 0x6000C43
        public System.Void .ctor(Unity.Mathematics.float4 v) { }
        // RVA: 0x04DB8170  token: 0x6000C44
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x04DB8190  token: 0x6000C45
        public System.Void .ctor(Unity.Mathematics.double4 v) { }
        // RVA: 0x04DB0420  token: 0x6000C46
        public static Unity.Mathematics.int4 op_Implicit(System.Int32 v) { }
        // RVA: 0x04DB0420  token: 0x6000C47
        public static Unity.Mathematics.int4 op_Explicit(System.UInt32 v) { }
        // RVA: 0x0B25AC78  token: 0x6000C48
        public static Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.uint4 v) { }
        // RVA: 0x04DB0490  token: 0x6000C49
        public static Unity.Mathematics.int4 op_Explicit(System.Single v) { }
        // RVA: 0x0B25AD38  token: 0x6000C4A
        public static Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.float4 v) { }
        // RVA: 0x04DB0470  token: 0x6000C4B
        public static Unity.Mathematics.int4 op_Explicit(System.Double v) { }
        // RVA: 0x0B25AD08  token: 0x6000C4C
        public static Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.double4 v) { }
        // RVA: 0x04DB82A0  token: 0x6000C4D
        public static Unity.Mathematics.int4 op_Multiply(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs) { }
        // RVA: 0x04DB82D0  token: 0x6000C4E
        public static Unity.Mathematics.int4 op_Multiply(Unity.Mathematics.int4 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB8270  token: 0x6000C4F
        public static Unity.Mathematics.int4 op_Addition(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs) { }
        // RVA: 0x04DB8240  token: 0x6000C50
        public static Unity.Mathematics.int4 op_Addition(Unity.Mathematics.int4 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB8330  token: 0x6000C51
        public static Unity.Mathematics.int4 op_Subtraction(Unity.Mathematics.int4 lhs, System.Int32 rhs) { }
        // RVA: 0x0B293034  token: 0x6000C52
        public static Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.int4 lhs, System.Int32 rhs) { }
        // RVA: 0x0B293010  token: 0x6000C53
        public static Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.int4 lhs, System.Int32 rhs) { }
        // RVA: 0x04DB8360  token: 0x6000C54
        public static Unity.Mathematics.int4 op_UnaryNegation(Unity.Mathematics.int4 val) { }
        // RVA: 0x04DB8300  token: 0x6000C55
        public static Unity.Mathematics.int4 op_RightShift(Unity.Mathematics.int4 x, System.Int32 n) { }
        // RVA: 0x034C9890  token: 0x6000C56
        public static Unity.Mathematics.int4 op_BitwiseOr(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs) { }
        // RVA: 0x068AFB90  token: 0x6000C59
        public virtual System.Boolean Equals(Unity.Mathematics.int4 rhs) { }
        // RVA: 0x0B292C78  token: 0x6000C5A
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B292CE4  token: 0x6000C5B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B292D00  token: 0x6000C5C
        public virtual System.String ToString() { }
        // RVA: 0x0B292E5C  token: 0x6000C5D
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000048  // size: 0x30
    public sealed struct int4x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int4 c0;  // 0x10
        public Unity.Mathematics.int4 c1;  // 0x20

        // Methods
        // RVA: 0x04D8C810  token: 0x6000C5E
        public System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1) { }
        // RVA: 0x04DB83B0  token: 0x6000C5F
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21, System.Int32 m30, System.Int32 m31) { }
        // RVA: 0x03BACBE0  token: 0x6000C60
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F05DC8  token: 0x6000C61
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F05D4C  token: 0x6000C62
        public System.Void .ctor(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x03BACBE0  token: 0x6000C63
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B2935DC  token: 0x6000C64
        public System.Void .ctor(Unity.Mathematics.uint4x2 v) { }
        // RVA: 0x04DB8390  token: 0x6000C65
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F05EB8  token: 0x6000C66
        public System.Void .ctor(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x04DB8400  token: 0x6000C67
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B293578  token: 0x6000C68
        public System.Void .ctor(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B25ADCC  token: 0x6000C69
        public static Unity.Mathematics.int4x2 op_Implicit(System.Int32 v) { }
        // RVA: 0x0B293058  token: 0x6000C6A
        public virtual System.Boolean Equals(Unity.Mathematics.int4x2 rhs) { }
        // RVA: 0x0B29309C  token: 0x6000C6B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B293110  token: 0x6000C6C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B293134  token: 0x6000C6D
        public virtual System.String ToString() { }
        // RVA: 0x0B293364  token: 0x6000C6E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000049  // size: 0x40
    public sealed struct int4x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int4 c0;  // 0x10
        public Unity.Mathematics.int4 c1;  // 0x20
        public Unity.Mathematics.int4 c2;  // 0x30

        // Methods
        // RVA: 0x04DA0760  token: 0x6000C6F
        public System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2) { }
        // RVA: 0x04DB84E0  token: 0x6000C70
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m30, System.Int32 m31, System.Int32 m32) { }
        // RVA: 0x04DB8560  token: 0x6000C71
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F05FF8  token: 0x6000C72
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F05F40  token: 0x6000C73
        public System.Void .ctor(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x04DB8560  token: 0x6000C74
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B293D24  token: 0x6000C75
        public System.Void .ctor(Unity.Mathematics.uint4x3 v) { }
        // RVA: 0x04DB85B0  token: 0x6000C76
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F060B4  token: 0x6000C77
        public System.Void .ctor(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x04DB8460  token: 0x6000C78
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B293DAC  token: 0x6000C79
        public System.Void .ctor(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B2936C0  token: 0x6000C7A
        public virtual System.Boolean Equals(Unity.Mathematics.int4x3 rhs) { }
        // RVA: 0x0B293644  token: 0x6000C7B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B293720  token: 0x6000C7C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B293A24  token: 0x6000C7D
        public virtual System.String ToString() { }
        // RVA: 0x0B293750  token: 0x6000C7E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200004A  // size: 0x50
    public sealed struct int4x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.int4 c0;  // 0x10
        public Unity.Mathematics.int4 c1;  // 0x20
        public Unity.Mathematics.int4 c2;  // 0x30
        public Unity.Mathematics.int4 c3;  // 0x40
        public static readonly Unity.Mathematics.int4x4 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04DB3220  token: 0x6000C7F
        public System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2, Unity.Mathematics.int4 c3) { }
        // RVA: 0x04A3ADB0  token: 0x6000C80
        public System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23, System.Int32 m30, System.Int32 m31, System.Int32 m32, System.Int32 m33) { }
        // RVA: 0x04DB8690  token: 0x6000C81
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F06204  token: 0x6000C82
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F062F4  token: 0x6000C83
        public System.Void .ctor(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x04DB8690  token: 0x6000C84
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B2946D4  token: 0x6000C85
        public System.Void .ctor(Unity.Mathematics.uint4x4 v) { }
        // RVA: 0x04DB86F0  token: 0x6000C86
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F06490  token: 0x6000C87
        public System.Void .ctor(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x04DB85E0  token: 0x6000C88
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B294780  token: 0x6000C89
        public System.Void .ctor(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B293EBC  token: 0x6000C8A
        public virtual System.Boolean Equals(Unity.Mathematics.int4x4 rhs) { }
        // RVA: 0x0B293E38  token: 0x6000C8B
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B293F38  token: 0x6000C8C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B294304  token: 0x6000C8D
        public virtual System.String ToString() { }
        // RVA: 0x0B293F70  token: 0x6000C8E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04A3AE60  token: 0x6000C8F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x20
    public sealed struct quaternion : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.float4 value;  // 0x10
        public static readonly Unity.Mathematics.quaternion identity;  // static @ 0x0

        // Methods
        // RVA: 0x0328FB00  token: 0x6000C90
        public static UnityEngine.Quaternion op_Implicit(Unity.Mathematics.quaternion q) { }
        // RVA: 0x0328FB00  token: 0x6000C91
        public static Unity.Mathematics.quaternion op_Implicit(UnityEngine.Quaternion q) { }
        // RVA: 0x04D88AD0  token: 0x6000C92
        public System.Void .ctor(System.Single x, System.Single y, System.Single z, System.Single w) { }
        // RVA: 0x04D8D270  token: 0x6000C93
        public System.Void .ctor(Unity.Mathematics.float4 value) { }
        // RVA: 0x02B77630  token: 0x6000C94
        public static Unity.Mathematics.quaternion op_Implicit(Unity.Mathematics.float4 v) { }
        // RVA: 0x034C9420  token: 0x6000C95
        public System.Void .ctor(Unity.Mathematics.float3x3 m) { }
        // RVA: 0x0B299FC0  token: 0x6000C96
        public System.Void .ctor(Unity.Mathematics.float4x4 m) { }
        // RVA: 0x062E2884  token: 0x6000C97
        public static Unity.Mathematics.quaternion AxisAngle(Unity.Mathematics.float3 axis, System.Single angle) { }
        // RVA: 0x062E2990  token: 0x6000C98
        public static Unity.Mathematics.quaternion EulerXYZ(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x062E2B70  token: 0x6000C99
        public static Unity.Mathematics.quaternion EulerXZY(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x062E2D50  token: 0x6000C9A
        public static Unity.Mathematics.quaternion EulerYXZ(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x062E2F30  token: 0x6000C9B
        public static Unity.Mathematics.quaternion EulerYZX(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x0B299560  token: 0x6000C9C
        public static Unity.Mathematics.quaternion EulerZXY(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x062E3148  token: 0x6000C9D
        public static Unity.Mathematics.quaternion EulerZYX(Unity.Mathematics.float3 xyz) { }
        // RVA: 0x0B299480  token: 0x6000C9E
        public static Unity.Mathematics.quaternion EulerXYZ(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B2994B8  token: 0x6000C9F
        public static Unity.Mathematics.quaternion EulerXZY(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B2994F0  token: 0x6000CA0
        public static Unity.Mathematics.quaternion EulerYXZ(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B299528  token: 0x6000CA1
        public static Unity.Mathematics.quaternion EulerYZX(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x062E3110  token: 0x6000CA2
        public static Unity.Mathematics.quaternion EulerZXY(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B299740  token: 0x6000CA3
        public static Unity.Mathematics.quaternion EulerZYX(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B299778  token: 0x6000CA4
        public static Unity.Mathematics.quaternion Euler(Unity.Mathematics.float3 xyz, Unity.Mathematics.math.RotationOrder order) { }
        // RVA: 0x062E3328  token: 0x6000CA5
        public static Unity.Mathematics.quaternion Euler(System.Single x, System.Single y, System.Single z, Unity.Mathematics.math.RotationOrder order) { }
        // RVA: 0x062E3364  token: 0x6000CA6
        public static Unity.Mathematics.quaternion RotateX(System.Single angle) { }
        // RVA: 0x062E33C8  token: 0x6000CA7
        public static Unity.Mathematics.quaternion RotateY(System.Single angle) { }
        // RVA: 0x062E342C  token: 0x6000CA8
        public static Unity.Mathematics.quaternion RotateZ(System.Single angle) { }
        // RVA: 0x0B299BE4  token: 0x6000CA9
        public static Unity.Mathematics.quaternion LookRotation(Unity.Mathematics.float3 forward, Unity.Mathematics.float3 up) { }
        // RVA: 0x0B2998D4  token: 0x6000CAA
        public static Unity.Mathematics.quaternion LookRotationSafe(Unity.Mathematics.float3 forward, Unity.Mathematics.float3 up) { }
        // RVA: 0x0415FC30  token: 0x6000CAB
        public virtual System.Boolean Equals(Unity.Mathematics.quaternion x) { }
        // RVA: 0x0B299414  token: 0x6000CAC
        public virtual System.Boolean Equals(System.Object x) { }
        // RVA: 0x0B256480  token: 0x6000CAD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B299D00  token: 0x6000CAE
        public virtual System.String ToString() { }
        // RVA: 0x0B299E6C  token: 0x6000CAF
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04D6E6C0  token: 0x6000CB0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x10
    public static class noise
    {
        // Methods
        // RVA: 0x0B297E28  token: 0x6000CB1
        public static System.Single cnoise(Unity.Mathematics.float2 P) { }
        // RVA: 0x0B298718  token: 0x6000CB2
        public static System.Single pnoise(Unity.Mathematics.float2 P, Unity.Mathematics.float2 rep) { }
        // RVA: 0x0B2984C8  token: 0x6000CB3
        private static Unity.Mathematics.float2 mod289(Unity.Mathematics.float2 x) { }
        // RVA: 0x0B2983FC  token: 0x6000CB4
        private static Unity.Mathematics.float3 mod289(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B298518  token: 0x6000CB5
        private static Unity.Mathematics.float4 mod289(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B298654  token: 0x6000CB6
        private static Unity.Mathematics.float3 permute(Unity.Mathematics.float3 x) { }
        // RVA: 0x0B2985BC  token: 0x6000CB7
        private static Unity.Mathematics.float4 permute(Unity.Mathematics.float4 x) { }
        // RVA: 0x0B2993BC  token: 0x6000CB8
        private static Unity.Mathematics.float4 taylorInvSqrt(Unity.Mathematics.float4 r) { }
        // RVA: 0x0B298380  token: 0x6000CB9
        private static Unity.Mathematics.float2 fade(Unity.Mathematics.float2 t) { }
        // RVA: 0x0B298CAC  token: 0x6000CBA
        public static System.Single snoise(Unity.Mathematics.float2 v) { }

    }

    // TypeToken: 0x200004D  // size: 0x14
    public sealed struct Random
    {
        // Fields
        public System.UInt32 state;  // 0x10

        // Methods
        // RVA: 0x04DB8760  token: 0x6000CBB
        public System.Void .ctor(System.UInt32 seed) { }
        // RVA: 0x0B2979F0  token: 0x6000CBC
        public static Unity.Mathematics.Random CreateFromIndex(System.UInt32 index) { }
        // RVA: 0x04CAFC90  token: 0x6000CBD
        private static System.UInt32 WangHash(System.UInt32 n) { }
        // RVA: 0x04558F50  token: 0x6000CBE
        public System.Single NextFloat() { }
        // RVA: 0x06AF4ED4  token: 0x6000CBF
        public Unity.Mathematics.float3 NextFloat3() { }
        // RVA: 0x04DB8730  token: 0x6000CC0
        private System.UInt32 NextState() { }

    }

    // TypeToken: 0x200004E  // size: 0x2C
    public sealed struct RigidTransform
    {
        // Fields
        public Unity.Mathematics.quaternion rot;  // 0x10
        public Unity.Mathematics.float3 pos;  // 0x20
        public static readonly Unity.Mathematics.RigidTransform identity;  // static @ 0x0

        // Methods
        // RVA: 0x04DB8780  token: 0x6000CC1
        public System.Void .ctor(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B297D48  token: 0x6000CC2
        public System.Void .ctor(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation) { }
        // RVA: 0x0B297DAC  token: 0x6000CC3
        public System.Void .ctor(Unity.Mathematics.float4x4 transform) { }
        // RVA: 0x0B297A94  token: 0x6000CC4
        public System.Boolean Equals(Unity.Mathematics.RigidTransform x) { }
        // RVA: 0x0B297A18  token: 0x6000CC5
        public virtual System.Boolean Equals(System.Object x) { }
        // RVA: 0x0B297AE4  token: 0x6000CC6
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B297B30  token: 0x6000CC7
        public virtual System.String ToString() { }
        // RVA: 0x04D538A0  token: 0x6000CC8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public static class svd
    {
        // Methods
        // RVA: 0x05F3A2D0  token: 0x6000CC9
        private static System.Void condSwap(System.Boolean c, System.Single& x, System.Single& y) { }
        // RVA: 0x05F3A25C  token: 0x6000CCA
        private static System.Void condNegSwap(System.Boolean c, Unity.Mathematics.float3& x, Unity.Mathematics.float3& y) { }
        // RVA: 0x0B29A384  token: 0x6000CCB
        private static Unity.Mathematics.quaternion condNegSwapQuat(System.Boolean c, Unity.Mathematics.quaternion q, Unity.Mathematics.float4 mask) { }
        // RVA: 0x0B29AF50  token: 0x6000CCC
        private static System.Void sortSingularValues(Unity.Mathematics.float3x3& b, Unity.Mathematics.quaternion& v) { }
        // RVA: 0x0B29A2E0  token: 0x6000CCD
        private static Unity.Mathematics.quaternion approxGivensQuat(Unity.Mathematics.float3 pq, Unity.Mathematics.float4 mask) { }
        // RVA: 0x0B29AB58  token: 0x6000CCE
        private static Unity.Mathematics.quaternion qrGivensQuat(Unity.Mathematics.float2 pq, Unity.Mathematics.float4 mask) { }
        // RVA: 0x0B29A440  token: 0x6000CCF
        private static Unity.Mathematics.quaternion givensQRFactorization(Unity.Mathematics.float3x3 b, Unity.Mathematics.float3x3& r) { }
        // RVA: 0x0B29A6E8  token: 0x6000CD0
        private static Unity.Mathematics.quaternion jacobiIteration(Unity.Mathematics.float3x3& s, System.Int32 iterations) { }
        // RVA: 0x0B29AD64  token: 0x6000CD1
        private static Unity.Mathematics.float3 singularValuesDecomposition(Unity.Mathematics.float3x3 a, Unity.Mathematics.quaternion& u, Unity.Mathematics.quaternion& v) { }
        // RVA: 0x0B29AC54  token: 0x6000CD2
        private static Unity.Mathematics.float3 rcpsafe(Unity.Mathematics.float3 x, System.Single epsilon) { }
        // RVA: 0x0B29B0F8  token: 0x6000CD3
        public static Unity.Mathematics.float3x3 svdInverse(Unity.Mathematics.float3x3 a) { }
        // RVA: 0x0B29B2C0  token: 0x6000CD4
        public static Unity.Mathematics.quaternion svdRotation(Unity.Mathematics.float3x3 a) { }

    }

    // TypeToken: 0x2000050  // size: 0x18
    public sealed struct uint2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.UInt32 x;  // 0x10
        public System.UInt32 y;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x6000CD5
        public System.Void .ctor(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x04DB7280  token: 0x6000CD6
        public System.Void .ctor(Unity.Mathematics.uint2 xy) { }
        // RVA: 0x04DB7220  token: 0x6000CD7
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B2901E8  token: 0x6000CD8
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2901F8  token: 0x6000CD9
        public System.Void .ctor(Unity.Mathematics.bool2 v) { }
        // RVA: 0x04DB7220  token: 0x6000CDA
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB7280  token: 0x6000CDB
        public System.Void .ctor(Unity.Mathematics.int2 v) { }
        // RVA: 0x05F3ADA8  token: 0x6000CDC
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B29B534  token: 0x6000CDD
        public System.Void .ctor(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B29B518  token: 0x6000CDE
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29B570  token: 0x6000CDF
        public System.Void .ctor(Unity.Mathematics.double2 v) { }
        // RVA: 0x04DB02A0  token: 0x6000CE0
        public static Unity.Mathematics.uint2 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DB02A0  token: 0x6000CE1
        public static Unity.Mathematics.uint2 op_Explicit(System.Int32 v) { }
        // RVA: 0x04DAB5F0  token: 0x6000CE2
        public static Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.int2 v) { }
        // RVA: 0x0B275514  token: 0x6000CE3
        public static Unity.Mathematics.uint2 op_Explicit(System.Single v) { }
        // RVA: 0x0B275538  token: 0x6000CE4
        public static Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.float2 v) { }
        // RVA: 0x0B2754F4  token: 0x6000CE5
        public static Unity.Mathematics.uint2 op_Explicit(System.Double v) { }
        // RVA: 0x0B2754C4  token: 0x6000CE6
        public static Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.double2 v) { }
        // RVA: 0x04DB7310  token: 0x6000CE7
        public static Unity.Mathematics.uint2 op_Multiply(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x04DB72F0  token: 0x6000CE8
        public static Unity.Mathematics.uint2 op_Multiply(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x04D85C70  token: 0x6000CE9
        public static Unity.Mathematics.uint2 op_Addition(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x04DB72B0  token: 0x6000CEA
        public static Unity.Mathematics.uint2 op_Addition(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x04D85C90  token: 0x6000CEB
        public static Unity.Mathematics.uint2 op_Subtraction(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x04DB7360  token: 0x6000CEC
        public static Unity.Mathematics.uint2 op_Subtraction(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x0B29B5AC  token: 0x6000CED
        public static Unity.Mathematics.bool2 op_GreaterThan(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB87E0  token: 0x6000CEE
        public static Unity.Mathematics.uint2 op_LeftShift(Unity.Mathematics.uint2 x, System.Int32 n) { }
        // RVA: 0x04DB8810  token: 0x6000CEF
        public static Unity.Mathematics.uint2 op_RightShift(Unity.Mathematics.uint2 x, System.Int32 n) { }
        // RVA: 0x0B29B594  token: 0x6000CF0
        public static Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB87A0  token: 0x6000CF1
        public static Unity.Mathematics.uint2 op_BitwiseAnd(Unity.Mathematics.uint2 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB72D0  token: 0x6000CF2
        public static Unity.Mathematics.uint2 op_BitwiseOr(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x04DB87C0  token: 0x6000CF3
        public static Unity.Mathematics.uint2 op_ExclusiveOr(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x063516B4  token: 0x6000CF4
        public virtual System.Boolean Equals(Unity.Mathematics.uint2 rhs) { }
        // RVA: 0x0B29B360  token: 0x6000CF5
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29B3D4  token: 0x6000CF6
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29B490  token: 0x6000CF7
        public virtual System.String ToString() { }
        // RVA: 0x0B29B408  token: 0x6000CF8
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000052  // size: 0x20
    public sealed struct uint2x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint2 c0;  // 0x10
        public Unity.Mathematics.uint2 c1;  // 0x18
        public static readonly Unity.Mathematics.uint2x2 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D861B0  token: 0x6000CF9
        public System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1) { }
        // RVA: 0x04DB74F0  token: 0x6000CFA
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11) { }
        // RVA: 0x04DB7430  token: 0x6000CFB
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B290618  token: 0x6000CFC
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2905A8  token: 0x6000CFD
        public System.Void .ctor(Unity.Mathematics.bool2x2 v) { }
        // RVA: 0x04DB7430  token: 0x6000CFE
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB7520  token: 0x6000CFF
        public System.Void .ctor(Unity.Mathematics.int2x2 v) { }
        // RVA: 0x05F3AF40  token: 0x6000D00
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3AF08  token: 0x6000D01
        public System.Void .ctor(Unity.Mathematics.float2x2 v) { }
        // RVA: 0x0B29B954  token: 0x6000D02
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29B8FC  token: 0x6000D03
        public System.Void .ctor(Unity.Mathematics.double2x2 v) { }
        // RVA: 0x0B287F80  token: 0x6000D04
        public virtual System.Boolean Equals(Unity.Mathematics.uint2x2 rhs) { }
        // RVA: 0x0B29B5C4  token: 0x6000D05
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29B630  token: 0x6000D06
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29B7A0  token: 0x6000D07
        public virtual System.String ToString() { }
        // RVA: 0x0B29B64C  token: 0x6000D08
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04CEADF0  token: 0x6000D09
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x28
    public sealed struct uint2x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint2 c0;  // 0x10
        public Unity.Mathematics.uint2 c1;  // 0x18
        public Unity.Mathematics.uint2 c2;  // 0x20

        // Methods
        // RVA: 0x04D8C0D0  token: 0x6000D0A
        public System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2) { }
        // RVA: 0x04DB7750  token: 0x6000D0B
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12) { }
        // RVA: 0x04DB76C0  token: 0x6000D0C
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B290B74  token: 0x6000D0D
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B290ADC  token: 0x6000D0E
        public System.Void .ctor(Unity.Mathematics.bool2x3 v) { }
        // RVA: 0x04DB76C0  token: 0x6000D0F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB7700  token: 0x6000D10
        public System.Void .ctor(Unity.Mathematics.int2x3 v) { }
        // RVA: 0x05F3B020  token: 0x6000D11
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3AF68  token: 0x6000D12
        public System.Void .ctor(Unity.Mathematics.float2x3 v) { }
        // RVA: 0x0B29BDB0  token: 0x6000D13
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29BE0C  token: 0x6000D14
        public System.Void .ctor(Unity.Mathematics.double2x3 v) { }
        // RVA: 0x0B290660  token: 0x6000D15
        public virtual System.Boolean Equals(Unity.Mathematics.uint2x3 rhs) { }
        // RVA: 0x0B29B998  token: 0x6000D16
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29BA0C  token: 0x6000D17
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29BA34  token: 0x6000D18
        public virtual System.String ToString() { }
        // RVA: 0x0B29BBFC  token: 0x6000D19
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000054  // size: 0x30
    public sealed struct uint2x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint2 c0;  // 0x10
        public Unity.Mathematics.uint2 c1;  // 0x18
        public Unity.Mathematics.uint2 c2;  // 0x20
        public Unity.Mathematics.uint2 c3;  // 0x28

        // Methods
        // RVA: 0x04D861C0  token: 0x6000D1A
        public System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2, Unity.Mathematics.uint2 c3) { }
        // RVA: 0x04DB78A0  token: 0x6000D1B
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13) { }
        // RVA: 0x04DB7980  token: 0x6000D1C
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B2911F4  token: 0x6000D1D
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291128  token: 0x6000D1E
        public System.Void .ctor(Unity.Mathematics.bool2x4 v) { }
        // RVA: 0x04DB7980  token: 0x6000D1F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04DB7830  token: 0x6000D20
        public System.Void .ctor(Unity.Mathematics.int2x4 v) { }
        // RVA: 0x05F3B1F8  token: 0x6000D21
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3B238  token: 0x6000D22
        public System.Void .ctor(Unity.Mathematics.float2x4 v) { }
        // RVA: 0x0B29C404  token: 0x6000D23
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29C364  token: 0x6000D24
        public System.Void .ctor(Unity.Mathematics.double2x4 v) { }
        // RVA: 0x0B290C4C  token: 0x6000D25
        public virtual System.Boolean Equals(Unity.Mathematics.uint2x4 rhs) { }
        // RVA: 0x0B29BE88  token: 0x6000D26
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29BEFC  token: 0x6000D27
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29BF20  token: 0x6000D28
        public virtual System.String ToString() { }
        // RVA: 0x0B29C150  token: 0x6000D29
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000055  // size: 0x1C
    public sealed struct uint3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.UInt32 x;  // 0x10
        public System.UInt32 y;  // 0x14
        public System.UInt32 z;  // 0x18

        // Properties
        Unity.Mathematics.uint4 xyzz { get; /* RVA: 0x04DB5490 */ }
        System.UInt32 Item { get; /* RVA: 0x04DB7B60 */ set; /* RVA: 0x04DB73A0 */ }

        // Methods
        // RVA: 0x04D85D60  token: 0x6000D2A
        public System.Void .ctor(System.UInt32 x, System.UInt32 y, System.UInt32 z) { }
        // RVA: 0x04DB7AB0  token: 0x6000D2B
        public System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint2 yz) { }
        // RVA: 0x04DB7B50  token: 0x6000D2C
        public System.Void .ctor(Unity.Mathematics.uint2 xy, System.UInt32 z) { }
        // RVA: 0x04D88F40  token: 0x6000D2D
        public System.Void .ctor(Unity.Mathematics.uint3 xyz) { }
        // RVA: 0x04DB7B20  token: 0x6000D2E
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B29149C  token: 0x6000D2F
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291470  token: 0x6000D30
        public System.Void .ctor(Unity.Mathematics.bool3 v) { }
        // RVA: 0x04DB7B20  token: 0x6000D31
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04D88F40  token: 0x6000D32
        public System.Void .ctor(Unity.Mathematics.int3 v) { }
        // RVA: 0x05F3B2F8  token: 0x6000D33
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3B2AC  token: 0x6000D34
        public System.Void .ctor(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B29C6C8  token: 0x6000D35
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29C698  token: 0x6000D36
        public System.Void .ctor(Unity.Mathematics.double3 v) { }
        // RVA: 0x04DB02F0  token: 0x6000D37
        public static Unity.Mathematics.uint3 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DB02F0  token: 0x6000D38
        public static Unity.Mathematics.uint3 op_Explicit(System.Int32 v) { }
        // RVA: 0x04DB0370  token: 0x6000D39
        public static Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.int3 v) { }
        // RVA: 0x0B275808  token: 0x6000D3A
        public static Unity.Mathematics.uint3 op_Explicit(System.Single v) { }
        // RVA: 0x05F3B330  token: 0x6000D3B
        public static Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.float3 v) { }
        // RVA: 0x0B2757E0  token: 0x6000D3C
        public static Unity.Mathematics.uint3 op_Explicit(System.Double v) { }
        // RVA: 0x0B2757A8  token: 0x6000D3D
        public static Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.double3 v) { }
        // RVA: 0x03C87E40  token: 0x6000D3E
        public static Unity.Mathematics.uint3 op_Multiply(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x04DB7C00  token: 0x6000D3F
        public static Unity.Mathematics.uint3 op_Multiply(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x0338E380  token: 0x6000D40
        public static Unity.Mathematics.uint3 op_Addition(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x04DB7BB0  token: 0x6000D41
        public static Unity.Mathematics.uint3 op_Addition(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB7C70  token: 0x6000D42
        public static Unity.Mathematics.uint3 op_Subtraction(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x04DB7C50  token: 0x6000D43
        public static Unity.Mathematics.uint3 op_Subtraction(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x0B29C70C  token: 0x6000D44
        public static Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB8880  token: 0x6000D45
        public static Unity.Mathematics.uint3 op_LeftShift(Unity.Mathematics.uint3 x, System.Int32 n) { }
        // RVA: 0x04DB88B0  token: 0x6000D46
        public static Unity.Mathematics.uint3 op_RightShift(Unity.Mathematics.uint3 x, System.Int32 n) { }
        // RVA: 0x0B29C6EC  token: 0x6000D47
        public static Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB8840  token: 0x6000D48
        public static Unity.Mathematics.uint3 op_BitwiseAnd(Unity.Mathematics.uint3 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB7BE0  token: 0x6000D49
        public static Unity.Mathematics.uint3 op_BitwiseOr(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x04DB8860  token: 0x6000D4A
        public static Unity.Mathematics.uint3 op_BitwiseOr(System.UInt32 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x030E9510  token: 0x6000D4B
        public static Unity.Mathematics.uint3 op_ExclusiveOr(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x06351A88  token: 0x6000D4F
        public virtual System.Boolean Equals(Unity.Mathematics.uint3 rhs) { }
        // RVA: 0x0B29C47C  token: 0x6000D50
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29C510  token: 0x6000D51
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29C5E4  token: 0x6000D52
        public virtual System.String ToString() { }
        // RVA: 0x0B29C534  token: 0x6000D53
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000057  // size: 0x28
    public sealed struct uint3x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint3 c0;  // 0x10
        public Unity.Mathematics.uint3 c1;  // 0x1c

        // Methods
        // RVA: 0x04D871E0  token: 0x6000D54
        public System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1) { }
        // RVA: 0x04DB7CB0  token: 0x6000D55
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21) { }
        // RVA: 0x04DB7D40  token: 0x6000D56
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F051A0  token: 0x6000D57
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B291A00  token: 0x6000D58
        public System.Void .ctor(Unity.Mathematics.bool3x2 v) { }
        // RVA: 0x04DB7D40  token: 0x6000D59
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F05370  token: 0x6000D5A
        public System.Void .ctor(Unity.Mathematics.int3x2 v) { }
        // RVA: 0x05F3B37C  token: 0x6000D5B
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3B3D8  token: 0x6000D5C
        public System.Void .ctor(Unity.Mathematics.float3x2 v) { }
        // RVA: 0x0B29CB44  token: 0x6000D5D
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29CBA0  token: 0x6000D5E
        public System.Void .ctor(Unity.Mathematics.double3x2 v) { }
        // RVA: 0x066ACFB0  token: 0x6000D5F
        public virtual System.Boolean Equals(Unity.Mathematics.uint3x2 rhs) { }
        // RVA: 0x0B29C72C  token: 0x6000D60
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29C7A0  token: 0x6000D61
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29C97C  token: 0x6000D62
        public virtual System.String ToString() { }
        // RVA: 0x0B29C7C8  token: 0x6000D63
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x2000058  // size: 0x34
    public sealed struct uint3x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint3 c0;  // 0x10
        public Unity.Mathematics.uint3 c1;  // 0x1c
        public Unity.Mathematics.uint3 c2;  // 0x28
        public static readonly Unity.Mathematics.uint3x3 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04D8C690  token: 0x6000D64
        public System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2) { }
        // RVA: 0x04B832D0  token: 0x6000D65
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22) { }
        // RVA: 0x04DB7DC0  token: 0x6000D66
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F05648  token: 0x6000D67
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B2920DC  token: 0x6000D68
        public System.Void .ctor(Unity.Mathematics.bool3x3 v) { }
        // RVA: 0x04DB7DC0  token: 0x6000D69
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F056D8  token: 0x6000D6A
        public System.Void .ctor(Unity.Mathematics.int3x3 v) { }
        // RVA: 0x05F3B560  token: 0x6000D6B
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3B6A4  token: 0x6000D6C
        public System.Void .ctor(Unity.Mathematics.float3x3 v) { }
        // RVA: 0x05F3B4DC  token: 0x6000D6D
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29D180  token: 0x6000D6E
        public System.Void .ctor(Unity.Mathematics.double3x3 v) { }
        // RVA: 0x0B291B68  token: 0x6000D6F
        public virtual System.Boolean Equals(Unity.Mathematics.uint3x3 rhs) { }
        // RVA: 0x0B29CC30  token: 0x6000D70
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29CCAC  token: 0x6000D71
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29CCD8  token: 0x6000D72
        public virtual System.String ToString() { }
        // RVA: 0x0B29CF3C  token: 0x6000D73
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04B83230  token: 0x6000D74
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x40
    public sealed struct uint3x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint3 c0;  // 0x10
        public Unity.Mathematics.uint3 c1;  // 0x1c
        public Unity.Mathematics.uint3 c2;  // 0x28
        public Unity.Mathematics.uint3 c3;  // 0x34

        // Methods
        // RVA: 0x04DB65D0  token: 0x6000D75
        public System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2, Unity.Mathematics.uint3 c3) { }
        // RVA: 0x04DB8070  token: 0x6000D76
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23) { }
        // RVA: 0x04DB7F80  token: 0x6000D77
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F059CC  token: 0x6000D78
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B292A08  token: 0x6000D79
        public System.Void .ctor(Unity.Mathematics.bool3x4 v) { }
        // RVA: 0x04DB7F80  token: 0x6000D7A
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x05F05824  token: 0x6000D7B
        public System.Void .ctor(Unity.Mathematics.int3x4 v) { }
        // RVA: 0x05F3B854  token: 0x6000D7C
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3B998  token: 0x6000D7D
        public System.Void .ctor(Unity.Mathematics.float3x4 v) { }
        // RVA: 0x05F3B8F0  token: 0x6000D7E
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29D8CC  token: 0x6000D7F
        public System.Void .ctor(Unity.Mathematics.double3x4 v) { }
        // RVA: 0x0B292348  token: 0x6000D80
        public virtual System.Boolean Equals(Unity.Mathematics.uint3x4 rhs) { }
        // RVA: 0x0B29D24C  token: 0x6000D81
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29D2C8  token: 0x6000D82
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29D5CC  token: 0x6000D83
        public virtual System.String ToString() { }
        // RVA: 0x0B29D2F8  token: 0x6000D84
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200005A  // size: 0x20
    public sealed struct uint4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.UInt32 x;  // 0x10
        public System.UInt32 y;  // 0x14
        public System.UInt32 z;  // 0x18
        public System.UInt32 w;  // 0x1c

        // Properties
        System.UInt32 Item { get; /* RVA: 0x04DB7B60 */ set; /* RVA: 0x04DB73A0 */ }

        // Methods
        // RVA: 0x04D88C10  token: 0x6000D85
        public System.Void .ctor(System.UInt32 x, System.UInt32 y, System.UInt32 z, System.UInt32 w) { }
        // RVA: 0x04DB8100  token: 0x6000D86
        public System.Void .ctor(System.UInt32 x, System.UInt32 y, Unity.Mathematics.uint2 zw) { }
        // RVA: 0x04DB8200  token: 0x6000D87
        public System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint2 yz, System.UInt32 w) { }
        // RVA: 0x04DB81D0  token: 0x6000D88
        public System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint3 yzw) { }
        // RVA: 0x04DB8220  token: 0x6000D89
        public System.Void .ctor(Unity.Mathematics.uint2 xy, System.UInt32 z, System.UInt32 w) { }
        // RVA: 0x04DB8120  token: 0x6000D8A
        public System.Void .ctor(Unity.Mathematics.uint2 xy, Unity.Mathematics.uint2 zw) { }
        // RVA: 0x04DB81B0  token: 0x6000D8B
        public System.Void .ctor(Unity.Mathematics.uint3 xyz, System.UInt32 w) { }
        // RVA: 0x04D92B90  token: 0x6000D8C
        public System.Void .ctor(Unity.Mathematics.uint4 xyzw) { }
        // RVA: 0x04DB8160  token: 0x6000D8D
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x0B292FB0  token: 0x6000D8E
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x0B292FCC  token: 0x6000D8F
        public System.Void .ctor(Unity.Mathematics.bool4 v) { }
        // RVA: 0x04DB8160  token: 0x6000D90
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04D92B90  token: 0x6000D91
        public System.Void .ctor(Unity.Mathematics.int4 v) { }
        // RVA: 0x05F3BAD0  token: 0x6000D92
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x05F3BA6C  token: 0x6000D93
        public System.Void .ctor(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B29DD10  token: 0x6000D94
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29DD3C  token: 0x6000D95
        public System.Void .ctor(Unity.Mathematics.double4 v) { }
        // RVA: 0x04DB0420  token: 0x6000D96
        public static Unity.Mathematics.uint4 op_Implicit(System.UInt32 v) { }
        // RVA: 0x04DB0420  token: 0x6000D97
        public static Unity.Mathematics.uint4 op_Explicit(System.Int32 v) { }
        // RVA: 0x0B25AC78  token: 0x6000D98
        public static Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.int4 v) { }
        // RVA: 0x0B275B1C  token: 0x6000D99
        public static Unity.Mathematics.uint4 op_Explicit(System.Single v) { }
        // RVA: 0x0B275B64  token: 0x6000D9A
        public static Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.float4 v) { }
        // RVA: 0x0B275B34  token: 0x6000D9B
        public static Unity.Mathematics.uint4 op_Explicit(System.Double v) { }
        // RVA: 0x0B275AD4  token: 0x6000D9C
        public static Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.double4 v) { }
        // RVA: 0x04DB82A0  token: 0x6000D9D
        public static Unity.Mathematics.uint4 op_Multiply(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x04DB82D0  token: 0x6000D9E
        public static Unity.Mathematics.uint4 op_Multiply(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB8950  token: 0x6000D9F
        public static Unity.Mathematics.uint4 op_Multiply(System.UInt32 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x04DB8270  token: 0x6000DA0
        public static Unity.Mathematics.uint4 op_Addition(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x04DB8240  token: 0x6000DA1
        public static Unity.Mathematics.uint4 op_Addition(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB89D0  token: 0x6000DA2
        public static Unity.Mathematics.uint4 op_Subtraction(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x04DB8330  token: 0x6000DA3
        public static Unity.Mathematics.uint4 op_Subtraction(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x0B29DD9C  token: 0x6000DA4
        public static Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x04DB8920  token: 0x6000DA5
        public static Unity.Mathematics.uint4 op_LeftShift(Unity.Mathematics.uint4 x, System.Int32 n) { }
        // RVA: 0x04DB8980  token: 0x6000DA6
        public static Unity.Mathematics.uint4 op_RightShift(Unity.Mathematics.uint4 x, System.Int32 n) { }
        // RVA: 0x0B29DD78  token: 0x6000DA7
        public static Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x034C9860  token: 0x6000DA8
        public static Unity.Mathematics.uint4 op_OnesComplement(Unity.Mathematics.uint4 val) { }
        // RVA: 0x034C9800  token: 0x6000DA9
        public static Unity.Mathematics.uint4 op_BitwiseAnd(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x04DB88F0  token: 0x6000DAA
        public static Unity.Mathematics.uint4 op_BitwiseAnd(Unity.Mathematics.uint4 lhs, System.UInt32 rhs) { }
        // RVA: 0x034C9890  token: 0x6000DAB
        public static Unity.Mathematics.uint4 op_BitwiseOr(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x034C9830  token: 0x6000DAC
        public static Unity.Mathematics.uint4 op_ExclusiveOr(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x068AFB90  token: 0x6000DAF
        public virtual System.Boolean Equals(Unity.Mathematics.uint4 rhs) { }
        // RVA: 0x0B29D9D8  token: 0x6000DB0
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29DA44  token: 0x6000DB1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29DBB4  token: 0x6000DB2
        public virtual System.String ToString() { }
        // RVA: 0x0B29DA60  token: 0x6000DB3
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200005C  // size: 0x30
    public sealed struct uint4x2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint4 c0;  // 0x10
        public Unity.Mathematics.uint4 c1;  // 0x20

        // Methods
        // RVA: 0x04D8C810  token: 0x6000DB4
        public System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1) { }
        // RVA: 0x04DB83B0  token: 0x6000DB5
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21, System.UInt32 m30, System.UInt32 m31) { }
        // RVA: 0x03BACBE0  token: 0x6000DB6
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F3BC08  token: 0x6000DB7
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F3BD80  token: 0x6000DB8
        public System.Void .ctor(Unity.Mathematics.bool4x2 v) { }
        // RVA: 0x03BACBE0  token: 0x6000DB9
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B2935DC  token: 0x6000DBA
        public System.Void .ctor(Unity.Mathematics.int4x2 v) { }
        // RVA: 0x05F3BCE8  token: 0x6000DBB
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B29E338  token: 0x6000DBC
        public System.Void .ctor(Unity.Mathematics.float4x2 v) { }
        // RVA: 0x05F3BC7C  token: 0x6000DBD
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29E29C  token: 0x6000DBE
        public System.Void .ctor(Unity.Mathematics.double4x2 v) { }
        // RVA: 0x0B293058  token: 0x6000DBF
        public virtual System.Boolean Equals(Unity.Mathematics.uint4x2 rhs) { }
        // RVA: 0x0B29DDC0  token: 0x6000DC0
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29DE34  token: 0x6000DC1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29DE58  token: 0x6000DC2
        public virtual System.String ToString() { }
        // RVA: 0x0B29E088  token: 0x6000DC3
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200005D  // size: 0x40
    public sealed struct uint4x3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint4 c0;  // 0x10
        public Unity.Mathematics.uint4 c1;  // 0x20
        public Unity.Mathematics.uint4 c2;  // 0x30

        // Methods
        // RVA: 0x04DA0760  token: 0x6000DC4
        public System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2) { }
        // RVA: 0x04DB84E0  token: 0x6000DC5
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32) { }
        // RVA: 0x04DB8560  token: 0x6000DC6
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F3C1BC  token: 0x6000DC7
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F3BF60  token: 0x6000DC8
        public System.Void .ctor(Unity.Mathematics.bool4x3 v) { }
        // RVA: 0x04DB8560  token: 0x6000DC9
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B293D24  token: 0x6000DCA
        public System.Void .ctor(Unity.Mathematics.int4x3 v) { }
        // RVA: 0x05F3C06C  token: 0x6000DCB
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B29EA0C  token: 0x6000DCC
        public System.Void .ctor(Unity.Mathematics.float4x3 v) { }
        // RVA: 0x05F3BEC0  token: 0x6000DCD
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29EA84  token: 0x6000DCE
        public System.Void .ctor(Unity.Mathematics.double4x3 v) { }
        // RVA: 0x0B2936C0  token: 0x6000DCF
        public virtual System.Boolean Equals(Unity.Mathematics.uint4x3 rhs) { }
        // RVA: 0x0B29E38C  token: 0x6000DD0
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29E408  token: 0x6000DD1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29E438  token: 0x6000DD2
        public virtual System.String ToString() { }
        // RVA: 0x0B29E738  token: 0x6000DD3
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }

    }

    // TypeToken: 0x200005E  // size: 0x50
    public sealed struct uint4x4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public Unity.Mathematics.uint4 c0;  // 0x10
        public Unity.Mathematics.uint4 c1;  // 0x20
        public Unity.Mathematics.uint4 c2;  // 0x30
        public Unity.Mathematics.uint4 c3;  // 0x40
        public static readonly Unity.Mathematics.uint4x4 identity;  // static @ 0x0

        // Methods
        // RVA: 0x04DB3220  token: 0x6000DD4
        public System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2, Unity.Mathematics.uint4 c3) { }
        // RVA: 0x04A3ADB0  token: 0x6000DD5
        public System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32, System.UInt32 m33) { }
        // RVA: 0x04DB8690  token: 0x6000DD6
        public System.Void .ctor(System.UInt32 v) { }
        // RVA: 0x05F3C260  token: 0x6000DD7
        public System.Void .ctor(System.Boolean v) { }
        // RVA: 0x05F3C5B4  token: 0x6000DD8
        public System.Void .ctor(Unity.Mathematics.bool4x4 v) { }
        // RVA: 0x04DB8690  token: 0x6000DD9
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x0B2946D4  token: 0x6000DDA
        public System.Void .ctor(Unity.Mathematics.int4x4 v) { }
        // RVA: 0x05F3C404  token: 0x6000DDB
        public System.Void .ctor(System.Single v) { }
        // RVA: 0x0B29F4BC  token: 0x6000DDC
        public System.Void .ctor(Unity.Mathematics.float4x4 v) { }
        // RVA: 0x05F3C334  token: 0x6000DDD
        public System.Void .ctor(System.Double v) { }
        // RVA: 0x0B29F38C  token: 0x6000DDE
        public System.Void .ctor(Unity.Mathematics.double4x4 v) { }
        // RVA: 0x0B293EBC  token: 0x6000DDF
        public virtual System.Boolean Equals(Unity.Mathematics.uint4x4 rhs) { }
        // RVA: 0x0B29EB6C  token: 0x6000DE0
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B29EBF0  token: 0x6000DE1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B29EC28  token: 0x6000DE2
        public virtual System.String ToString() { }
        // RVA: 0x0B29EFF8  token: 0x6000DE3
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x04A3ACC0  token: 0x6000DE4
        private static System.Void .cctor() { }

    }

}

