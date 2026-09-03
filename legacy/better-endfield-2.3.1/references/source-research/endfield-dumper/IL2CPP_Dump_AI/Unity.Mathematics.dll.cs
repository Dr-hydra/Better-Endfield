// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Mathematics.dll
// Classes:  94
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

CLASS: RotationOrder
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Unity.Mathematics.math.RotationOrderXYZ  // const
  public    static  Unity.Mathematics.math.RotationOrderXZY  // const
  public    static  Unity.Mathematics.math.RotationOrderYXZ  // const
  public    static  Unity.Mathematics.math.RotationOrderYZX  // const
  public    static  Unity.Mathematics.math.RotationOrderZXY  // const
  public    static  Unity.Mathematics.math.RotationOrderZYX  // const
  public    static  Unity.Mathematics.math.RotationOrderDefault  // const
METHODS:
END_CLASS

CLASS: ShuffleComponent
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Unity.Mathematics.math.ShuffleComponentLeftX  // const
  public    static  Unity.Mathematics.math.ShuffleComponentLeftY  // const
  public    static  Unity.Mathematics.math.ShuffleComponentLeftZ  // const
  public    static  Unity.Mathematics.math.ShuffleComponentLeftW  // const
  public    static  Unity.Mathematics.math.ShuffleComponentRightX  // const
  public    static  Unity.Mathematics.math.ShuffleComponentRightY  // const
  public    static  Unity.Mathematics.math.ShuffleComponentRightZ  // const
  public    static  Unity.Mathematics.math.ShuffleComponentRightW  // const
METHODS:
END_CLASS

CLASS: LongDoubleUnion
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x18
FIELDS:
  public            System.Int64                    longValue  // 0x10
  public            System.Double                   doubleValue  // 0x10
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200003B
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000042
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000051
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x2000056
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebuggerProxy
TYPE:  sealed class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstructionAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: Unity.Mathematics.AffineTransform
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float3x3      rs  // 0x10
  public            Unity.Mathematics.float3        t  // 0x34
  public    static readonly Unity.Mathematics.AffineTransformidentity  // static @ 0x0
METHODS:
  RVA=0x0B27CF08  token=0x6000002  System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation)
  RVA=0x0B27CE60  token=0x6000003  System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale)
  RVA=0x04DB21B0  token=0x6000004  System.Void .ctor(Unity.Mathematics.float3 translation, Unity.Mathematics.float3x3 rotationScale)
  RVA=0x0B27CDF8  token=0x6000005  System.Void .ctor(Unity.Mathematics.float3x3 rotationScale)
  RVA=0x0B27CDA0  token=0x6000006  System.Void .ctor(Unity.Mathematics.RigidTransform rigid)
  RVA=0x05F0065C  token=0x6000007  System.Void .ctor(Unity.Mathematics.float3x4 m)
  RVA=0x05F00794  token=0x6000008  System.Void .ctor(Unity.Mathematics.float4x4 m)
  RVA=0x0B27C704  token=0x6000009  System.Boolean Equals(Unity.Mathematics.AffineTransform rhs)
  RVA=0x0B27C688  token=0x600000A  System.Boolean Equals(System.Object o)
  RVA=0x0B27C76C  token=0x600000B  System.Int32 GetHashCode()
  RVA=0x0B27CA70  token=0x600000C  System.String ToString()
  RVA=0x0B27C79C  token=0x600000D  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04CE39A0  token=0x600000E  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.math
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  public    static  System.Double                   E_DBL  // const
  public    static  System.Double                   LOG2E_DBL  // const
  public    static  System.Double                   LOG10E_DBL  // const
  public    static  System.Double                   LN2_DBL  // const
  public    static  System.Double                   LN10_DBL  // const
  public    static  System.Double                   PI_DBL  // const
  public    static  System.Double                   PI2_DBL  // const
  public    static  System.Double                   PIHALF_DBL  // const
  public    static  System.Double                   TAU_DBL  // const
  public    static  System.Double                   TODEGREES_DBL  // const
  public    static  System.Double                   TORADIANS_DBL  // const
  public    static  System.Double                   SQRT2_DBL  // const
  public    static  System.Double                   EPSILON_DBL  // const
  public    static  System.Double                   INFINITY_DBL  // const
  public    static  System.Double                   NAN_DBL  // const
  public    static  System.Single                   FLT_MIN_NORMAL  // const
  public    static  System.Double                   DBL_MIN_NORMAL  // const
  public    static  System.Single                   E  // const
  public    static  System.Single                   LOG2E  // const
  public    static  System.Single                   LOG10E  // const
  public    static  System.Single                   LN2  // const
  public    static  System.Single                   LN10  // const
  public    static  System.Single                   PI  // const
  public    static  System.Single                   PI2  // const
  public    static  System.Single                   PIHALF  // const
  public    static  System.Single                   TAU  // const
  public    static  System.Single                   TODEGREES  // const
  public    static  System.Single                   TORADIANS  // const
  public    static  System.Single                   SQRT2  // const
  public    static  System.Single                   EPSILON  // const
  public    static  System.Single                   INFINITY  // const
  public    static  System.Single                   NAN  // const
METHODS:
  RVA=0x0B24A44C  token=0x600000F  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation)
  RVA=0x0B24A498  token=0x6000010  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale)
  RVA=0x04DACFF0  token=0x6000011  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3 translation, Unity.Mathematics.float3x3 rotationScale)
  RVA=0x0B24A3BC  token=0x6000012  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3x3 rotationScale)
  RVA=0x0B24A400  token=0x6000013  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float4x4 m)
  RVA=0x0B24A540  token=0x6000014  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.float3x4 m)
  RVA=0x0B24A4FC  token=0x6000015  Unity.Mathematics.AffineTransform AffineTransform(Unity.Mathematics.RigidTransform rigid)
  RVA=0x0B252CF4  token=0x6000016  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.AffineTransform transform)
  RVA=0x0B2523A8  token=0x6000017  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.AffineTransform transform)
  RVA=0x0B2685A8  token=0x6000018  Unity.Mathematics.AffineTransform mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.AffineTransform b)
  RVA=0x0B2672B8  token=0x6000019  Unity.Mathematics.AffineTransform mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.AffineTransform b)
  RVA=0x0B26A504  token=0x600001A  Unity.Mathematics.AffineTransform mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3x3 b)
  RVA=0x0B26D034  token=0x600001B  Unity.Mathematics.float4 mul(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float4 pos)
  RVA=0x0B270778  token=0x600001C  Unity.Mathematics.float3 rotate(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3 dir)
  RVA=0x0B273EEC  token=0x600001D  Unity.Mathematics.float3 transform(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3 pos)
  RVA=0x0B25B258  token=0x600001E  Unity.Mathematics.AffineTransform inverse(Unity.Mathematics.AffineTransform a)
  RVA=0x0B24E08C  token=0x600001F  System.Void decompose(Unity.Mathematics.AffineTransform a, Unity.Mathematics.float3& translation, Unity.Mathematics.quaternion& rotation, Unity.Mathematics.float3& scale)
  RVA=0x0B253A70  token=0x6000020  System.UInt32 hash(Unity.Mathematics.AffineTransform a)
  RVA=0x0B2580C4  token=0x6000021  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.AffineTransform a)
  RVA=0x04DAD570  token=0x6000022  Unity.Mathematics.bool2 bool2(System.Boolean x, System.Boolean y)
  RVA=0x04DAD590  token=0x6000023  Unity.Mathematics.bool2 bool2(Unity.Mathematics.bool2 xy)
  RVA=0x04DAD580  token=0x6000024  Unity.Mathematics.bool2 bool2(System.Boolean v)
  RVA=0x0B2551F0  token=0x6000025  System.UInt32 hash(Unity.Mathematics.bool2 v)
  RVA=0x0B257960  token=0x6000026  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2 v)
  RVA=0x05F33718  token=0x6000027  System.Boolean shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F35030  token=0x6000028  Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F338F8  token=0x6000029  Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F33EC4  token=0x600002A  Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool2 left, Unity.Mathematics.bool2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F32CE0  token=0x600002B  System.Boolean select_shuffle_component(Unity.Mathematics.bool2 a, Unity.Mathematics.bool2 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAD5B0  token=0x600002C  Unity.Mathematics.bool2x2 bool2x2(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1)
  RVA=0x05F08B08  token=0x600002D  Unity.Mathematics.bool2x2 bool2x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11)
  RVA=0x0B24C8BC  token=0x600002E  Unity.Mathematics.bool2x2 bool2x2(System.Boolean v)
  RVA=0x0B274708  token=0x600002F  Unity.Mathematics.bool2x2 transpose(Unity.Mathematics.bool2x2 v)
  RVA=0x0B255014  token=0x6000030  System.UInt32 hash(Unity.Mathematics.bool2x2 v)
  RVA=0x0B2595B0  token=0x6000031  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x2 v)
  RVA=0x04DAD5C0  token=0x6000032  Unity.Mathematics.bool2x3 bool2x3(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2)
  RVA=0x05F08B38  token=0x6000033  Unity.Mathematics.bool2x3 bool2x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12)
  RVA=0x0B24C8E0  token=0x6000034  Unity.Mathematics.bool2x3 bool2x3(System.Boolean v)
  RVA=0x0B274414  token=0x6000035  Unity.Mathematics.bool3x2 transpose(Unity.Mathematics.bool2x3 v)
  RVA=0x0B255F90  token=0x6000036  System.UInt32 hash(Unity.Mathematics.bool2x3 v)
  RVA=0x0B257D68  token=0x6000037  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x3 v)
  RVA=0x04DAD5E0  token=0x6000038  Unity.Mathematics.bool2x4 bool2x4(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2, Unity.Mathematics.bool2 c3)
  RVA=0x0B24C8FC  token=0x6000039  Unity.Mathematics.bool2x4 bool2x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13)
  RVA=0x0B24C954  token=0x600003A  Unity.Mathematics.bool2x4 bool2x4(System.Boolean v)
  RVA=0x0B274D54  token=0x600003B  Unity.Mathematics.bool4x2 transpose(Unity.Mathematics.bool2x4 v)
  RVA=0x0B255460  token=0x600003C  System.UInt32 hash(Unity.Mathematics.bool2x4 v)
  RVA=0x0B25A170  token=0x600003D  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.bool2x4 v)
  RVA=0x04DAD610  token=0x600003E  Unity.Mathematics.bool3 bool3(System.Boolean x, System.Boolean y, System.Boolean z)
  RVA=0x04DAD650  token=0x600003F  Unity.Mathematics.bool3 bool3(System.Boolean x, Unity.Mathematics.bool2 yz)
  RVA=0x04DAD630  token=0x6000040  Unity.Mathematics.bool3 bool3(Unity.Mathematics.bool2 xy, System.Boolean z)
  RVA=0x04DAD670  token=0x6000041  Unity.Mathematics.bool3 bool3(Unity.Mathematics.bool3 xyz)
  RVA=0x04DAD620  token=0x6000042  Unity.Mathematics.bool3 bool3(System.Boolean v)
  RVA=0x05F0F4F0  token=0x6000043  System.UInt32 hash(Unity.Mathematics.bool3 v)
  RVA=0x0B258B64  token=0x6000044  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3 v)
  RVA=0x05F34180  token=0x6000045  System.Boolean shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B272344  token=0x6000046  Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B271EC8  token=0x6000047  Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B271710  token=0x6000048  Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool3 left, Unity.Mathematics.bool3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F33128  token=0x6000049  System.Boolean select_shuffle_component(Unity.Mathematics.bool3 a, Unity.Mathematics.bool3 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAD690  token=0x600004A  Unity.Mathematics.bool3x2 bool3x2(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1)
  RVA=0x05F08BF4  token=0x600004B  Unity.Mathematics.bool3x2 bool3x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21)
  RVA=0x0B24C978  token=0x600004C  Unity.Mathematics.bool3x2 bool3x2(System.Boolean v)
  RVA=0x0B274FE4  token=0x600004D  Unity.Mathematics.bool2x3 transpose(Unity.Mathematics.bool3x2 v)
  RVA=0x05F11430  token=0x600004E  System.UInt32 hash(Unity.Mathematics.bool3x2 v)
  RVA=0x0B259804  token=0x600004F  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x2 v)
  RVA=0x04DAD6C0  token=0x6000050  Unity.Mathematics.bool3x3 bool3x3(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2)
  RVA=0x05F08C2C  token=0x6000051  Unity.Mathematics.bool3x3 bool3x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22)
  RVA=0x0B24C994  token=0x6000052  Unity.Mathematics.bool3x3 bool3x3(System.Boolean v)
  RVA=0x0B274CF4  token=0x6000053  Unity.Mathematics.bool3x3 transpose(Unity.Mathematics.bool3x3 v)
  RVA=0x05F12254  token=0x6000054  System.UInt32 hash(Unity.Mathematics.bool3x3 v)
  RVA=0x0B25992C  token=0x6000055  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x3 v)
  RVA=0x04DAD700  token=0x6000056  Unity.Mathematics.bool3x4 bool3x4(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2, Unity.Mathematics.bool3 c3)
  RVA=0x05F08C8C  token=0x6000057  Unity.Mathematics.bool3x4 bool3x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23)
  RVA=0x0B24C9B0  token=0x6000058  Unity.Mathematics.bool3x4 bool3x4(System.Boolean v)
  RVA=0x0B27502C  token=0x6000059  Unity.Mathematics.bool4x3 transpose(Unity.Mathematics.bool3x4 v)
  RVA=0x05F104F0  token=0x600005A  System.UInt32 hash(Unity.Mathematics.bool3x4 v)
  RVA=0x0B2584C8  token=0x600005B  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.bool3x4 v)
  RVA=0x04DAD790  token=0x600005C  Unity.Mathematics.bool4 bool4(System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w)
  RVA=0x04DAD820  token=0x600005D  Unity.Mathematics.bool4 bool4(System.Boolean x, System.Boolean y, Unity.Mathematics.bool2 zw)
  RVA=0x04DAD770  token=0x600005E  Unity.Mathematics.bool4 bool4(System.Boolean x, Unity.Mathematics.bool2 yz, System.Boolean w)
  RVA=0x04DAD870  token=0x600005F  Unity.Mathematics.bool4 bool4(System.Boolean x, Unity.Mathematics.bool3 yzw)
  RVA=0x04DAD750  token=0x6000060  Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool2 xy, System.Boolean z, System.Boolean w)
  RVA=0x04DAD7D0  token=0x6000061  Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool2 xy, Unity.Mathematics.bool2 zw)
  RVA=0x04DAD840  token=0x6000062  Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool3 xyz, System.Boolean w)
  RVA=0x04DAD7F0  token=0x6000063  Unity.Mathematics.bool4 bool4(Unity.Mathematics.bool4 xyzw)
  RVA=0x04DAD7B0  token=0x6000064  Unity.Mathematics.bool4 bool4(System.Boolean v)
  RVA=0x05F10CE8  token=0x6000065  System.UInt32 hash(Unity.Mathematics.bool4 v)
  RVA=0x0B259D7C  token=0x6000066  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4 v)
  RVA=0x05F33710  token=0x6000067  System.Boolean shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F353D8  token=0x6000068  Unity.Mathematics.bool2 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F34EB0  token=0x6000069  Unity.Mathematics.bool3 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F34570  token=0x600006A  Unity.Mathematics.bool4 shuffle(Unity.Mathematics.bool4 left, Unity.Mathematics.bool4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F32BB4  token=0x600006B  System.Boolean select_shuffle_component(Unity.Mathematics.bool4 a, Unity.Mathematics.bool4 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAD8A0  token=0x600006C  Unity.Mathematics.bool4x2 bool4x2(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1)
  RVA=0x0B24C9CC  token=0x600006D  Unity.Mathematics.bool4x2 bool4x2(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21, System.Boolean m30, System.Boolean m31)
  RVA=0x0B24CA24  token=0x600006E  Unity.Mathematics.bool4x2 bool4x2(System.Boolean v)
  RVA=0x0B2742A0  token=0x600006F  Unity.Mathematics.bool2x4 transpose(Unity.Mathematics.bool4x2 v)
  RVA=0x05F0FFD4  token=0x6000070  System.UInt32 hash(Unity.Mathematics.bool4x2 v)
  RVA=0x05F15008  token=0x6000071  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x2 v)
  RVA=0x04DAD8B0  token=0x6000072  Unity.Mathematics.bool4x3 bool4x3(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2)
  RVA=0x05F08DC4  token=0x6000073  Unity.Mathematics.bool4x3 bool4x3(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m30, System.Boolean m31, System.Boolean m32)
  RVA=0x0B24CA48  token=0x6000074  Unity.Mathematics.bool4x3 bool4x3(System.Boolean v)
  RVA=0x0B2750F8  token=0x6000075  Unity.Mathematics.bool3x4 transpose(Unity.Mathematics.bool4x3 v)
  RVA=0x05F10D90  token=0x6000076  System.UInt32 hash(Unity.Mathematics.bool4x3 v)
  RVA=0x05F1479C  token=0x6000077  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x3 v)
  RVA=0x04DAD8C0  token=0x6000078  Unity.Mathematics.bool4x4 bool4x4(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2, Unity.Mathematics.bool4 c3)
  RVA=0x05F08E40  token=0x6000079  Unity.Mathematics.bool4x4 bool4x4(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23, System.Boolean m30, System.Boolean m31, System.Boolean m32, System.Boolean m33)
  RVA=0x0B24CA64  token=0x600007A  Unity.Mathematics.bool4x4 bool4x4(System.Boolean v)
  RVA=0x0B274210  token=0x600007B  Unity.Mathematics.bool4x4 transpose(Unity.Mathematics.bool4x4 v)
  RVA=0x05F1201C  token=0x600007C  System.UInt32 hash(Unity.Mathematics.bool4x4 v)
  RVA=0x05F1628C  token=0x600007D  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.bool4x4 v)
  RVA=0x04DAEB80  token=0x600007E  Unity.Mathematics.double2 double2(System.Double x, System.Double y)
  RVA=0x04DAEC90  token=0x600007F  Unity.Mathematics.double2 double2(Unity.Mathematics.double2 xy)
  RVA=0x04DAECB0  token=0x6000080  Unity.Mathematics.double2 double2(System.Double v)
  RVA=0x0B24E794  token=0x6000081  Unity.Mathematics.double2 double2(System.Boolean v)
  RVA=0x0B24E73C  token=0x6000082  Unity.Mathematics.double2 double2(Unity.Mathematics.bool2 v)
  RVA=0x04DAEC50  token=0x6000083  Unity.Mathematics.double2 double2(System.Int32 v)
  RVA=0x04DAEBF0  token=0x6000084  Unity.Mathematics.double2 double2(Unity.Mathematics.int2 v)
  RVA=0x04DAEB90  token=0x6000085  Unity.Mathematics.double2 double2(System.UInt32 v)
  RVA=0x04DAEC20  token=0x6000086  Unity.Mathematics.double2 double2(Unity.Mathematics.uint2 v)
  RVA=0x0B24E6FC  token=0x6000087  Unity.Mathematics.double2 double2(Unity.Mathematics.half v)
  RVA=0x0B24E754  token=0x6000088  Unity.Mathematics.double2 double2(Unity.Mathematics.half2 v)
  RVA=0x04DAEC70  token=0x6000089  Unity.Mathematics.double2 double2(System.Single v)
  RVA=0x04DAEBC0  token=0x600008A  Unity.Mathematics.double2 double2(Unity.Mathematics.float2 v)
  RVA=0x0B254EB0  token=0x600008B  System.UInt32 hash(Unity.Mathematics.double2 v)
  RVA=0x0B258E24  token=0x600008C  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2 v)
  RVA=0x0B272110  token=0x600008D  System.Double shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B271B08  token=0x600008E  Unity.Mathematics.double2 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B272044  token=0x600008F  Unity.Mathematics.double3 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B272210  token=0x6000090  Unity.Mathematics.double4 shuffle(Unity.Mathematics.double2 left, Unity.Mathematics.double2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F32AC0  token=0x6000091  System.Double select_shuffle_component(Unity.Mathematics.double2 a, Unity.Mathematics.double2 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAECC0  token=0x6000092  Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1)
  RVA=0x05F0AEA4  token=0x6000093  Unity.Mathematics.double2x2 double2x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11)
  RVA=0x0B24E7CC  token=0x6000094  Unity.Mathematics.double2x2 double2x2(System.Double v)
  RVA=0x0B24E8BC  token=0x6000095  Unity.Mathematics.double2x2 double2x2(System.Boolean v)
  RVA=0x0B24E7E8  token=0x6000096  Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.bool2x2 v)
  RVA=0x0B24E84C  token=0x6000097  Unity.Mathematics.double2x2 double2x2(System.Int32 v)
  RVA=0x0B24E868  token=0x6000098  Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.int2x2 v)
  RVA=0x0B24E808  token=0x6000099  Unity.Mathematics.double2x2 double2x2(System.UInt32 v)
  RVA=0x0B24E890  token=0x600009A  Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.uint2x2 v)
  RVA=0x0B24E7B0  token=0x600009B  Unity.Mathematics.double2x2 double2x2(System.Single v)
  RVA=0x0B24E824  token=0x600009C  Unity.Mathematics.double2x2 double2x2(Unity.Mathematics.float2x2 v)
  RVA=0x0B27472C  token=0x600009D  Unity.Mathematics.double2x2 transpose(Unity.Mathematics.double2x2 v)
  RVA=0x0B25B350  token=0x600009E  Unity.Mathematics.double2x2 inverse(Unity.Mathematics.double2x2 m)
  RVA=0x04DAE9C0  token=0x600009F  System.Double determinant(Unity.Mathematics.double2x2 m)
  RVA=0x0B254EF8  token=0x60000A0  System.UInt32 hash(Unity.Mathematics.double2x2 v)
  RVA=0x0B259CE4  token=0x60000A1  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x2 v)
  RVA=0x04DAECF0  token=0x60000A2  Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2)
  RVA=0x05F0AECC  token=0x60000A3  Unity.Mathematics.double2x3 double2x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12)
  RVA=0x0B24E918  token=0x60000A4  Unity.Mathematics.double2x3 double2x3(System.Double v)
  RVA=0x0B24E970  token=0x60000A5  Unity.Mathematics.double2x3 double2x3(System.Boolean v)
  RVA=0x0B24E9EC  token=0x60000A6  Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.bool2x3 v)
  RVA=0x0B24E990  token=0x60000A7  Unity.Mathematics.double2x3 double2x3(System.Int32 v)
  RVA=0x0B24EA28  token=0x60000A8  Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.int2x3 v)
  RVA=0x0B24E8D8  token=0x60000A9  Unity.Mathematics.double2x3 double2x3(System.UInt32 v)
  RVA=0x0B24E9B0  token=0x60000AA  Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.uint2x3 v)
  RVA=0x0B24E8F8  token=0x60000AB  Unity.Mathematics.double2x3 double2x3(System.Single v)
  RVA=0x0B24E938  token=0x60000AC  Unity.Mathematics.double2x3 double2x3(Unity.Mathematics.float2x3 v)
  RVA=0x0B2750A4  token=0x60000AD  Unity.Mathematics.double3x2 transpose(Unity.Mathematics.double2x3 v)
  RVA=0x0B254174  token=0x60000AE  System.UInt32 hash(Unity.Mathematics.double2x3 v)
  RVA=0x0B259AF0  token=0x60000AF  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x3 v)
  RVA=0x04DAED40  token=0x60000B0  Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2, Unity.Mathematics.double2 c3)
  RVA=0x05F0AFB4  token=0x60000B1  Unity.Mathematics.double2x4 double2x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13)
  RVA=0x0B24EB24  token=0x60000B2  Unity.Mathematics.double2x4 double2x4(System.Double v)
  RVA=0x0B24EA60  token=0x60000B3  Unity.Mathematics.double2x4 double2x4(System.Boolean v)
  RVA=0x0B24EAC0  token=0x60000B4  Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.bool2x4 v)
  RVA=0x0B24EBCC  token=0x60000B5  Unity.Mathematics.double2x4 double2x4(System.Int32 v)
  RVA=0x0B24EAE8  token=0x60000B6  Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.int2x4 v)
  RVA=0x0B24EB6C  token=0x60000B7  Unity.Mathematics.double2x4 double2x4(System.UInt32 v)
  RVA=0x0B24EA84  token=0x60000B8  Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.uint2x4 v)
  RVA=0x0B24EB48  token=0x60000B9  Unity.Mathematics.double2x4 double2x4(System.Single v)
  RVA=0x0B24EB90  token=0x60000BA  Unity.Mathematics.double2x4 double2x4(Unity.Mathematics.float2x4 v)
  RVA=0x0B2744E0  token=0x60000BB  Unity.Mathematics.double4x2 transpose(Unity.Mathematics.double2x4 v)
  RVA=0x0B2547F8  token=0x60000BC  System.UInt32 hash(Unity.Mathematics.double2x4 v)
  RVA=0x0B25A27C  token=0x60000BD  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.double2x4 v)
  RVA=0x04DAEE50  token=0x60000BE  Unity.Mathematics.double3 double3(System.Double x, System.Double y, System.Double z)
  RVA=0x04DAEF60  token=0x60000BF  Unity.Mathematics.double3 double3(System.Double x, Unity.Mathematics.double2 yz)
  RVA=0x04DAEE80  token=0x60000C0  Unity.Mathematics.double3 double3(Unity.Mathematics.double2 xy, System.Double z)
  RVA=0x04DAEF00  token=0x60000C1  Unity.Mathematics.double3 double3(Unity.Mathematics.double3 xyz)
  RVA=0x04DAEE70  token=0x60000C2  Unity.Mathematics.double3 double3(System.Double v)
  RVA=0x0B24EC88  token=0x60000C3  Unity.Mathematics.double3 double3(System.Boolean v)
  RVA=0x0B24EC54  token=0x60000C4  Unity.Mathematics.double3 double3(Unity.Mathematics.bool3 v)
  RVA=0x04DAEF30  token=0x60000C5  Unity.Mathematics.double3 double3(System.Int32 v)
  RVA=0x04DAEDD0  token=0x60000C6  Unity.Mathematics.double3 double3(Unity.Mathematics.int3 v)
  RVA=0x04DAEE20  token=0x60000C7  Unity.Mathematics.double3 double3(System.UInt32 v)
  RVA=0x04DAEEA0  token=0x60000C8  Unity.Mathematics.double3 double3(Unity.Mathematics.uint3 v)
  RVA=0x0B24ECA8  token=0x60000C9  Unity.Mathematics.double3 double3(Unity.Mathematics.half v)
  RVA=0x0B24EBF0  token=0x60000CA  Unity.Mathematics.double3 double3(Unity.Mathematics.half3 v)
  RVA=0x04DAEDA0  token=0x60000CB  Unity.Mathematics.double3 double3(System.Single v)
  RVA=0x0415F9A0  token=0x60000CC  Unity.Mathematics.double3 double3(Unity.Mathematics.float3 v)
  RVA=0x0B255238  token=0x60000CD  System.UInt32 hash(Unity.Mathematics.double3 v)
  RVA=0x0B257F04  token=0x60000CE  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3 v)
  RVA=0x0B2723BC  token=0x60000CF  System.Double shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B2717F4  token=0x60000D0  Unity.Mathematics.double2 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B27245C  token=0x60000D1  Unity.Mathematics.double3 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B27199C  token=0x60000D2  Unity.Mathematics.double4 shuffle(Unity.Mathematics.double3 left, Unity.Mathematics.double3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x0B271410  token=0x60000D3  System.Double select_shuffle_component(Unity.Mathematics.double3 a, Unity.Mathematics.double3 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAEF80  token=0x60000D4  Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1)
  RVA=0x05F0B0C0  token=0x60000D5  Unity.Mathematics.double3x2 double3x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21)
  RVA=0x0B24ED58  token=0x60000D6  Unity.Mathematics.double3x2 double3x2(System.Double v)
  RVA=0x0B24EDB4  token=0x60000D7  Unity.Mathematics.double3x2 double3x2(System.Boolean v)
  RVA=0x0B24EDD8  token=0x60000D8  Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.bool3x2 v)
  RVA=0x0B24EE70  token=0x60000D9  Unity.Mathematics.double3x2 double3x2(System.Int32 v)
  RVA=0x0B24EE14  token=0x60000DA  Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.int3x2 v)
  RVA=0x0B24ECFC  token=0x60000DB  Unity.Mathematics.double3x2 double3x2(System.UInt32 v)
  RVA=0x0B24ED78  token=0x60000DC  Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.uint3x2 v)
  RVA=0x0B24EE50  token=0x60000DD  Unity.Mathematics.double3x2 double3x2(System.Single v)
  RVA=0x0B24ED1C  token=0x60000DE  Unity.Mathematics.double3x2 double3x2(Unity.Mathematics.float3x2 v)
  RVA=0x0B274648  token=0x60000DF  Unity.Mathematics.double2x3 transpose(Unity.Mathematics.double3x2 v)
  RVA=0x0B253690  token=0x60000E0  System.UInt32 hash(Unity.Mathematics.double3x2 v)
  RVA=0x0B256B54  token=0x60000E1  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x2 v)
  RVA=0x04DAEFB0  token=0x60000E2  Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2)
  RVA=0x05F0B1A0  token=0x60000E3  Unity.Mathematics.double3x3 double3x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22)
  RVA=0x0B24F030  token=0x60000E4  Unity.Mathematics.double3x3 double3x3(System.Double v)
  RVA=0x0B24EFB4  token=0x60000E5  Unity.Mathematics.double3x3 double3x3(System.Boolean v)
  RVA=0x05F0B108  token=0x60000E6  Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.bool3x3 v)
  RVA=0x0B24EF88  token=0x60000E7  Unity.Mathematics.double3x3 double3x3(System.Int32 v)
  RVA=0x0B24EF38  token=0x60000E8  Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.int3x3 v)
  RVA=0x0B24EF0C  token=0x60000E9  Unity.Mathematics.double3x3 double3x3(System.UInt32 v)
  RVA=0x0B24EFE0  token=0x60000EA  Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.uint3x3 v)
  RVA=0x0B24EE90  token=0x60000EB  Unity.Mathematics.double3x3 double3x3(System.Single v)
  RVA=0x0B24EEBC  token=0x60000EC  Unity.Mathematics.double3x3 double3x3(Unity.Mathematics.float3x3 v)
  RVA=0x0B2747F4  token=0x60000ED  Unity.Mathematics.double3x3 transpose(Unity.Mathematics.double3x3 v)
  RVA=0x0B25C6E4  token=0x60000EE  Unity.Mathematics.double3x3 inverse(Unity.Mathematics.double3x3 m)
  RVA=0x04DAE320  token=0x60000EF  System.Double determinant(Unity.Mathematics.double3x3 m)
  RVA=0x0B254B94  token=0x60000F0  System.UInt32 hash(Unity.Mathematics.double3x3 v)
  RVA=0x0B259634  token=0x60000F1  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x3 v)
  RVA=0x04DAEFF0  token=0x60000F2  Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2, Unity.Mathematics.double3 c3)
  RVA=0x05F0B338  token=0x60000F3  Unity.Mathematics.double3x4 double3x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23)
  RVA=0x0B24F05C  token=0x60000F4  Unity.Mathematics.double3x4 double3x4(System.Double v)
  RVA=0x0B24F1FC  token=0x60000F5  Unity.Mathematics.double3x4 double3x4(System.Boolean v)
  RVA=0x05F0B560  token=0x60000F6  Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.bool3x4 v)
  RVA=0x0B24F088  token=0x60000F7  Unity.Mathematics.double3x4 double3x4(System.Int32 v)
  RVA=0x0B24F104  token=0x60000F8  Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.int3x4 v)
  RVA=0x0B24F180  token=0x60000F9  Unity.Mathematics.double3x4 double3x4(System.UInt32 v)
  RVA=0x0B24F1AC  token=0x60000FA  Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.uint3x4 v)
  RVA=0x0B24F154  token=0x60000FB  Unity.Mathematics.double3x4 double3x4(System.Single v)
  RVA=0x0B24F0B4  token=0x60000FC  Unity.Mathematics.double3x4 double3x4(Unity.Mathematics.float3x4 v)
  RVA=0x0B274F08  token=0x60000FD  Unity.Mathematics.double4x3 transpose(Unity.Mathematics.double3x4 v)
  RVA=0x0B2516B0  token=0x60000FE  Unity.Mathematics.double3x4 fastinverse(Unity.Mathematics.double3x4 m)
  RVA=0x0B2537BC  token=0x60000FF  System.UInt32 hash(Unity.Mathematics.double3x4 v)
  RVA=0x0B258E7C  token=0x6000100  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.double3x4 v)
  RVA=0x04DAF120  token=0x6000101  Unity.Mathematics.double4 double4(System.Double x, System.Double y, System.Double z, System.Double w)
  RVA=0x04DAF040  token=0x6000102  Unity.Mathematics.double4 double4(System.Double x, System.Double y, Unity.Mathematics.double2 zw)
  RVA=0x04DAF060  token=0x6000103  Unity.Mathematics.double4 double4(System.Double x, Unity.Mathematics.double2 yz, System.Double w)
  RVA=0x04DAF170  token=0x6000104  Unity.Mathematics.double4 double4(System.Double x, Unity.Mathematics.double3 yzw)
  RVA=0x04DAF1D0  token=0x6000105  Unity.Mathematics.double4 double4(Unity.Mathematics.double2 xy, System.Double z, System.Double w)
  RVA=0x04DAF140  token=0x6000106  Unity.Mathematics.double4 double4(Unity.Mathematics.double2 xy, Unity.Mathematics.double2 zw)
  RVA=0x04DAF0B0  token=0x6000107  Unity.Mathematics.double4 double4(Unity.Mathematics.double3 xyz, System.Double w)
  RVA=0x0B24F26C  token=0x6000108  Unity.Mathematics.double4 double4(Unity.Mathematics.double4 xyzw)
  RVA=0x04DAF080  token=0x6000109  Unity.Mathematics.double4 double4(System.Double v)
  RVA=0x0B24F2E0  token=0x600010A  Unity.Mathematics.double4 double4(System.Boolean v)
  RVA=0x0B24F250  token=0x600010B  Unity.Mathematics.double4 double4(Unity.Mathematics.bool4 v)
  RVA=0x04DAF0E0  token=0x600010C  Unity.Mathematics.double4 double4(System.Int32 v)
  RVA=0x0B24F2FC  token=0x600010D  Unity.Mathematics.double4 double4(Unity.Mathematics.int4 v)
  RVA=0x04DAF1A0  token=0x600010E  Unity.Mathematics.double4 double4(System.UInt32 v)
  RVA=0x0B24F324  token=0x600010F  Unity.Mathematics.double4 double4(Unity.Mathematics.uint4 v)
  RVA=0x0B24F294  token=0x6000110  Unity.Mathematics.double4 double4(Unity.Mathematics.half v)
  RVA=0x0B24F22C  token=0x6000111  Unity.Mathematics.double4 double4(Unity.Mathematics.half4 v)
  RVA=0x04DAF090  token=0x6000112  Unity.Mathematics.double4 double4(System.Single v)
  RVA=0x0B24F2B8  token=0x6000113  Unity.Mathematics.double4 double4(Unity.Mathematics.float4 v)
  RVA=0x0B25402C  token=0x6000114  System.UInt32 hash(Unity.Mathematics.double4 v)
  RVA=0x0B2575B0  token=0x6000115  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4 v)
  RVA=0x0B272308  token=0x6000116  System.Double shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B271B74  token=0x6000117  Unity.Mathematics.double2 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B271C14  token=0x6000118  Unity.Mathematics.double3 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F33BD8  token=0x6000119  Unity.Mathematics.double4 shuffle(Unity.Mathematics.double4 left, Unity.Mathematics.double4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x0B2712DC  token=0x600011A  System.Double select_shuffle_component(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAF1F0  token=0x600011B  Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1)
  RVA=0x05F0B68C  token=0x600011C  Unity.Mathematics.double4x2 double4x2(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21, System.Double m30, System.Double m31)
  RVA=0x0B24F3AC  token=0x600011D  Unity.Mathematics.double4x2 double4x2(System.Double v)
  RVA=0x0B24F3F4  token=0x600011E  Unity.Mathematics.double4x2 double4x2(System.Boolean v)
  RVA=0x0B24F47C  token=0x600011F  Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.bool4x2 v)
  RVA=0x0B24F3D0  token=0x6000120  Unity.Mathematics.double4x2 double4x2(System.Int32 v)
  RVA=0x0B24F370  token=0x6000121  Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.int4x2 v)
  RVA=0x0B24F418  token=0x6000122  Unity.Mathematics.double4x2 double4x2(System.UInt32 v)
  RVA=0x0B24F4A4  token=0x6000123  Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.uint4x2 v)
  RVA=0x0B24F34C  token=0x6000124  Unity.Mathematics.double4x2 double4x2(System.Single v)
  RVA=0x0B24F440  token=0x6000125  Unity.Mathematics.double4x2 double4x2(Unity.Mathematics.float4x2 v)
  RVA=0x0B27469C  token=0x6000126  Unity.Mathematics.double2x4 transpose(Unity.Mathematics.double4x2 v)
  RVA=0x0B25576C  token=0x6000127  System.UInt32 hash(Unity.Mathematics.double4x2 v)
  RVA=0x0B257FA8  token=0x6000128  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x2 v)
  RVA=0x04DAF220  token=0x6000129  Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2)
  RVA=0x05F0B7D0  token=0x600012A  Unity.Mathematics.double4x3 double4x3(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22, System.Double m30, System.Double m31, System.Double m32)
  RVA=0x0B24F530  token=0x600012B  Unity.Mathematics.double4x3 double4x3(System.Double v)
  RVA=0x0B24F608  token=0x600012C  Unity.Mathematics.double4x3 double4x3(System.Boolean v)
  RVA=0x05F0B788  token=0x600012D  Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.bool4x3 v)
  RVA=0x0B24F55C  token=0x600012E  Unity.Mathematics.double4x3 double4x3(System.Int32 v)
  RVA=0x0B24F5B8  token=0x600012F  Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.int4x3 v)
  RVA=0x0B24F588  token=0x6000130  Unity.Mathematics.double4x3 double4x3(System.UInt32 v)
  RVA=0x0B24F4E0  token=0x6000131  Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.uint4x3 v)
  RVA=0x0B24F634  token=0x6000132  Unity.Mathematics.double4x3 double4x3(System.Single v)
  RVA=0x0B24F660  token=0x6000133  Unity.Mathematics.double4x3 double4x3(Unity.Mathematics.float4x3 v)
  RVA=0x0B274C64  token=0x6000134  Unity.Mathematics.double3x4 transpose(Unity.Mathematics.double4x3 v)
  RVA=0x0B255578  token=0x6000135  System.UInt32 hash(Unity.Mathematics.double4x3 v)
  RVA=0x0B257BCC  token=0x6000136  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x3 v)
  RVA=0x04DAF260  token=0x6000137  Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2, Unity.Mathematics.double4 c3)
  RVA=0x05F0BAE8  token=0x6000138  Unity.Mathematics.double4x4 double4x4(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23, System.Double m30, System.Double m31, System.Double m32, System.Double m33)
  RVA=0x0B24F6E8  token=0x6000139  Unity.Mathematics.double4x4 double4x4(System.Double v)
  RVA=0x0B24F71C  token=0x600013A  Unity.Mathematics.double4x4 double4x4(System.Boolean v)
  RVA=0x0B24F898  token=0x600013B  Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.bool4x4 v)
  RVA=0x0B24F8DC  token=0x600013C  Unity.Mathematics.double4x4 double4x4(System.Int32 v)
  RVA=0x0B24F750  token=0x600013D  Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.int4x4 v)
  RVA=0x0B24F6B0  token=0x600013E  Unity.Mathematics.double4x4 double4x4(System.UInt32 v)
  RVA=0x0B24F7AC  token=0x600013F  Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.uint4x4 v)
  RVA=0x0B24F864  token=0x6000140  Unity.Mathematics.double4x4 double4x4(System.Single v)
  RVA=0x0B24F808  token=0x6000141  Unity.Mathematics.double4x4 double4x4(Unity.Mathematics.float4x4 v)
  RVA=0x0B270628  token=0x6000142  Unity.Mathematics.double3 rotate(Unity.Mathematics.double4x4 a, Unity.Mathematics.double3 b)
  RVA=0x0B273F90  token=0x6000143  Unity.Mathematics.double3 transform(Unity.Mathematics.double4x4 a, Unity.Mathematics.double3 b)
  RVA=0x0B274940  token=0x6000144  Unity.Mathematics.double4x4 transpose(Unity.Mathematics.double4x4 v)
  RVA=0x0B25B7D0  token=0x6000145  Unity.Mathematics.double4x4 inverse(Unity.Mathematics.double4x4 m)
  RVA=0x0B251030  token=0x6000146  Unity.Mathematics.double4x4 fastinverse(Unity.Mathematics.double4x4 m)
  RVA=0x04DAE0B0  token=0x6000147  System.Double determinant(Unity.Mathematics.double4x4 m)
  RVA=0x0B253E20  token=0x6000148  System.UInt32 hash(Unity.Mathematics.double4x4 v)
  RVA=0x0B256C94  token=0x6000149  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.double4x4 v)
  RVA=0x04DAF4D0  token=0x600014A  Unity.Mathematics.float2 float2(System.Single x, System.Single y)
  RVA=0x04DA7CB0  token=0x600014B  Unity.Mathematics.float2 float2(Unity.Mathematics.float2 xy)
  RVA=0x04D8B720  token=0x600014C  Unity.Mathematics.float2 float2(System.Single v)
  RVA=0x0B2519E8  token=0x600014D  Unity.Mathematics.float2 float2(System.Boolean v)
  RVA=0x0B251A08  token=0x600014E  Unity.Mathematics.float2 float2(Unity.Mathematics.bool2 v)
  RVA=0x04DAF4E0  token=0x600014F  Unity.Mathematics.float2 float2(System.Int32 v)
  RVA=0x04DAF500  token=0x6000150  Unity.Mathematics.float2 float2(Unity.Mathematics.int2 v)
  RVA=0x04DAF550  token=0x6000151  Unity.Mathematics.float2 float2(System.UInt32 v)
  RVA=0x04DAF520  token=0x6000152  Unity.Mathematics.float2 float2(Unity.Mathematics.uint2 v)
  RVA=0x0B251A58  token=0x6000153  Unity.Mathematics.float2 float2(Unity.Mathematics.half v)
  RVA=0x0B251A2C  token=0x6000154  Unity.Mathematics.float2 float2(Unity.Mathematics.half2 v)
  RVA=0x04DAF580  token=0x6000155  Unity.Mathematics.float2 float2(System.Double v)
  RVA=0x04DAF4B0  token=0x6000156  Unity.Mathematics.float2 float2(Unity.Mathematics.double2 v)
  RVA=0x0B253658  token=0x6000157  System.UInt32 hash(Unity.Mathematics.float2 v)
  RVA=0x0B259CA0  token=0x6000158  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2 v)
  RVA=0x05F33FD4  token=0x6000159  System.Single shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F3477C  token=0x600015A  Unity.Mathematics.float2 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F34970  token=0x600015B  Unity.Mathematics.float3 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F341B8  token=0x600015C  Unity.Mathematics.float4 shuffle(Unity.Mathematics.float2 left, Unity.Mathematics.float2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F3322C  token=0x600015D  System.Single select_shuffle_component(Unity.Mathematics.float2 a, Unity.Mathematics.float2 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x02E515B0  token=0x600015E  Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1)
  RVA=0x0B251A98  token=0x600015F  Unity.Mathematics.float2x2 float2x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11)
  RVA=0x0B251A80  token=0x6000160  Unity.Mathematics.float2x2 float2x2(System.Single v)
  RVA=0x0B251B80  token=0x6000161  Unity.Mathematics.float2x2 float2x2(System.Boolean v)
  RVA=0x0B251B98  token=0x6000162  Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.bool2x2 v)
  RVA=0x0B251AE4  token=0x6000163  Unity.Mathematics.float2x2 float2x2(System.Int32 v)
  RVA=0x0B251B5C  token=0x6000164  Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.int2x2 v)
  RVA=0x0B251B2C  token=0x6000165  Unity.Mathematics.float2x2 float2x2(System.UInt32 v)
  RVA=0x0B251ABC  token=0x6000166  Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.uint2x2 v)
  RVA=0x0B251B44  token=0x6000167  Unity.Mathematics.float2x2 float2x2(System.Double v)
  RVA=0x0B251AFC  token=0x6000168  Unity.Mathematics.float2x2 float2x2(Unity.Mathematics.double2x2 v)
  RVA=0x0B2742F4  token=0x6000169  Unity.Mathematics.float2x2 transpose(Unity.Mathematics.float2x2 v)
  RVA=0x05F16C64  token=0x600016A  Unity.Mathematics.float2x2 inverse(Unity.Mathematics.float2x2 m)
  RVA=0x04DAE9F0  token=0x600016B  System.Single determinant(Unity.Mathematics.float2x2 m)
  RVA=0x0B255CA8  token=0x600016C  System.UInt32 hash(Unity.Mathematics.float2x2 v)
  RVA=0x0B25A05C  token=0x600016D  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x2 v)
  RVA=0x04DAF5A0  token=0x600016E  Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2)
  RVA=0x05F0D588  token=0x600016F  Unity.Mathematics.float2x3 float2x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12)
  RVA=0x0B251BB4  token=0x6000170  Unity.Mathematics.float2x3 float2x3(System.Single v)
  RVA=0x0B251C14  token=0x6000171  Unity.Mathematics.float2x3 float2x3(System.Boolean v)
  RVA=0x0B251C70  token=0x6000172  Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.bool2x3 v)
  RVA=0x0B251CDC  token=0x6000173  Unity.Mathematics.float2x3 float2x3(System.Int32 v)
  RVA=0x0B251CFC  token=0x6000174  Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.int2x3 v)
  RVA=0x0B251BF4  token=0x6000175  Unity.Mathematics.float2x3 float2x3(System.UInt32 v)
  RVA=0x0B251CA4  token=0x6000176  Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.uint2x3 v)
  RVA=0x0B251BD4  token=0x6000177  Unity.Mathematics.float2x3 float2x3(System.Double v)
  RVA=0x0B251C34  token=0x6000178  Unity.Mathematics.float2x3 float2x3(Unity.Mathematics.double2x3 v)
  RVA=0x0B274A98  token=0x6000179  Unity.Mathematics.float3x2 transpose(Unity.Mathematics.float2x3 v)
  RVA=0x0B25440C  token=0x600017A  System.UInt32 hash(Unity.Mathematics.float2x3 v)
  RVA=0x0B257364  token=0x600017B  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x3 v)
  RVA=0x04DAF5B0  token=0x600017C  Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2, Unity.Mathematics.float2 c3)
  RVA=0x05F0D5CC  token=0x600017D  Unity.Mathematics.float2x4 float2x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13)
  RVA=0x0B251DEC  token=0x600017E  Unity.Mathematics.float2x4 float2x4(System.Single v)
  RVA=0x0B251D34  token=0x600017F  Unity.Mathematics.float2x4 float2x4(System.Boolean v)
  RVA=0x0B251D94  token=0x6000180  Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.bool2x4 v)
  RVA=0x0B251DD0  token=0x6000181  Unity.Mathematics.float2x4 float2x4(System.Int32 v)
  RVA=0x0B251E58  token=0x6000182  Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.int2x4 v)
  RVA=0x0B251DB4  token=0x6000183  Unity.Mathematics.float2x4 float2x4(System.UInt32 v)
  RVA=0x0B251E24  token=0x6000184  Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.uint2x4 v)
  RVA=0x0B251E08  token=0x6000185  Unity.Mathematics.float2x4 float2x4(System.Double v)
  RVA=0x0B251D50  token=0x6000186  Unity.Mathematics.float2x4 float2x4(Unity.Mathematics.double2x4 v)
  RVA=0x0B274868  token=0x6000187  Unity.Mathematics.float4x2 transpose(Unity.Mathematics.float2x4 v)
  RVA=0x0B2550A4  token=0x6000188  System.UInt32 hash(Unity.Mathematics.float2x4 v)
  RVA=0x0B2588B8  token=0x6000189  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.float2x4 v)
  RVA=0x04DA2820  token=0x600018A  Unity.Mathematics.float3 float3(System.Single x, System.Single y, System.Single z)
  RVA=0x04DAF730  token=0x600018B  Unity.Mathematics.float3 float3(System.Single x, Unity.Mathematics.float2 yz)
  RVA=0x04DAF670  token=0x600018C  Unity.Mathematics.float3 float3(Unity.Mathematics.float2 xy, System.Single z)
  RVA=0x04DAF6A0  token=0x600018D  Unity.Mathematics.float3 float3(Unity.Mathematics.float3 xyz)
  RVA=0x04D8B860  token=0x600018E  Unity.Mathematics.float3 float3(System.Single v)
  RVA=0x0B251E8C  token=0x600018F  Unity.Mathematics.float3 float3(System.Boolean v)
  RVA=0x0B251F30  token=0x6000190  Unity.Mathematics.float3 float3(Unity.Mathematics.bool3 v)
  RVA=0x04DAF610  token=0x6000191  Unity.Mathematics.float3 float3(System.Int32 v)
  RVA=0x04DAF7A0  token=0x6000192  Unity.Mathematics.float3 float3(Unity.Mathematics.int3 v)
  RVA=0x04DAF760  token=0x6000193  Unity.Mathematics.float3 float3(System.UInt32 v)
  RVA=0x04DAF6D0  token=0x6000194  Unity.Mathematics.float3 float3(Unity.Mathematics.uint3 v)
  RVA=0x0B251EA8  token=0x6000195  Unity.Mathematics.float3 float3(Unity.Mathematics.half v)
  RVA=0x0B251EE4  token=0x6000196  Unity.Mathematics.float3 float3(Unity.Mathematics.half3 v)
  RVA=0x04DAF640  token=0x6000197  Unity.Mathematics.float3 float3(System.Double v)
  RVA=0x04DAF5D0  token=0x6000198  Unity.Mathematics.float3 float3(Unity.Mathematics.double3 v)
  RVA=0x05F10F48  token=0x6000199  System.UInt32 hash(Unity.Mathematics.float3 v)
  RVA=0x05F13994  token=0x600019A  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3 v)
  RVA=0x05F34A30  token=0x600019B  System.Single shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F3511C  token=0x600019C  Unity.Mathematics.float2 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F354C8  token=0x600019D  Unity.Mathematics.float3 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F352B0  token=0x600019E  Unity.Mathematics.float4 shuffle(Unity.Mathematics.float3 left, Unity.Mathematics.float3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F329B0  token=0x600019F  System.Single select_shuffle_component(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAF7F0  token=0x60001A0  Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1)
  RVA=0x05F0D638  token=0x60001A1  Unity.Mathematics.float3x2 float3x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21)
  RVA=0x0B2520C8  token=0x60001A2  Unity.Mathematics.float3x2 float3x2(System.Single v)
  RVA=0x0B251FD8  token=0x60001A3  Unity.Mathematics.float3x2 float3x2(System.Boolean v)
  RVA=0x0B252090  token=0x60001A4  Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.bool3x2 v)
  RVA=0x0B251FF8  token=0x60001A5  Unity.Mathematics.float3x2 float3x2(System.Int32 v)
  RVA=0x0B252038  token=0x60001A6  Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.int3x2 v)
  RVA=0x0B252070  token=0x60001A7  Unity.Mathematics.float3x2 float3x2(System.UInt32 v)
  RVA=0x0B251FA0  token=0x60001A8  Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.uint3x2 v)
  RVA=0x0B252018  token=0x60001A9  Unity.Mathematics.float3x2 float3x2(System.Double v)
  RVA=0x0B251F60  token=0x60001AA  Unity.Mathematics.float3x2 float3x2(Unity.Mathematics.double3x2 v)
  RVA=0x0B274B9C  token=0x60001AB  Unity.Mathematics.float2x3 transpose(Unity.Mathematics.float3x2 v)
  RVA=0x0B25649C  token=0x60001AC  System.UInt32 hash(Unity.Mathematics.float3x2 v)
  RVA=0x0B2577B0  token=0x60001AD  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x2 v)
  RVA=0x034C99D0  token=0x60001AE  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2)
  RVA=0x05F0D728  token=0x60001AF  Unity.Mathematics.float3x3 float3x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22)
  RVA=0x0B252108  token=0x60001B0  Unity.Mathematics.float3x3 float3x3(System.Single v)
  RVA=0x0B2520E8  token=0x60001B1  Unity.Mathematics.float3x3 float3x3(System.Boolean v)
  RVA=0x0B2522EC  token=0x60001B2  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.bool3x3 v)
  RVA=0x0B252214  token=0x60001B3  Unity.Mathematics.float3x3 float3x3(System.Int32 v)
  RVA=0x0B252180  token=0x60001B4  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.int3x3 v)
  RVA=0x0B2522AC  token=0x60001B5  Unity.Mathematics.float3x3 float3x3(System.UInt32 v)
  RVA=0x0B25226C  token=0x60001B6  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.uint3x3 v)
  RVA=0x0B2522CC  token=0x60001B7  Unity.Mathematics.float3x3 float3x3(System.Double v)
  RVA=0x0B252128  token=0x60001B8  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.double3x3 v)
  RVA=0x0B274E84  token=0x60001B9  Unity.Mathematics.float3x3 transpose(Unity.Mathematics.float3x3 v)
  RVA=0x0B25B410  token=0x60001BA  Unity.Mathematics.float3x3 inverse(Unity.Mathematics.float3x3 m)
  RVA=0x04DAE870  token=0x60001BB  System.Single determinant(Unity.Mathematics.float3x3 m)
  RVA=0x0B25619C  token=0x60001BC  System.UInt32 hash(Unity.Mathematics.float3x3 v)
  RVA=0x0B257644  token=0x60001BD  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x3 v)
  RVA=0x04DAF820  token=0x60001BE  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3 c3)
  RVA=0x05F0D8D8  token=0x60001BF  Unity.Mathematics.float3x4 float3x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23)
  RVA=0x0B2524A0  token=0x60001C0  Unity.Mathematics.float3x4 float3x4(System.Single v)
  RVA=0x0B252388  token=0x60001C1  Unity.Mathematics.float3x4 float3x4(System.Boolean v)
  RVA=0x0B25234C  token=0x60001C2  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.bool3x4 v)
  RVA=0x0B25243C  token=0x60001C3  Unity.Mathematics.float3x4 float3x4(System.Int32 v)
  RVA=0x0B25245C  token=0x60001C4  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.int3x4 v)
  RVA=0x0B252564  token=0x60001C5  Unity.Mathematics.float3x4 float3x4(System.UInt32 v)
  RVA=0x0B252520  token=0x60001C6  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.uint3x4 v)
  RVA=0x0B25232C  token=0x60001C7  Unity.Mathematics.float3x4 float3x4(System.Double v)
  RVA=0x0B2524C0  token=0x60001C8  Unity.Mathematics.float3x4 float3x4(Unity.Mathematics.double3x4 v)
  RVA=0x0B2749F4  token=0x60001C9  Unity.Mathematics.float4x3 transpose(Unity.Mathematics.float3x4 v)
  RVA=0x0B2514A4  token=0x60001CA  Unity.Mathematics.float3x4 fastinverse(Unity.Mathematics.float3x4 m)
  RVA=0x0B253AC4  token=0x60001CB  System.UInt32 hash(Unity.Mathematics.float3x4 v)
  RVA=0x0B256FE8  token=0x60001CC  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.float3x4 v)
  RVA=0x04DAF870  token=0x60001CD  Unity.Mathematics.float4 float4(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x04DAF8A0  token=0x60001CE  Unity.Mathematics.float4 float4(System.Single x, System.Single y, Unity.Mathematics.float2 zw)
  RVA=0x04DAF940  token=0x60001CF  Unity.Mathematics.float4 float4(System.Single x, Unity.Mathematics.float2 yz, System.Single w)
  RVA=0x04DAF9C0  token=0x60001D0  Unity.Mathematics.float4 float4(System.Single x, Unity.Mathematics.float3 yzw)
  RVA=0x04DAF8D0  token=0x60001D1  Unity.Mathematics.float4 float4(Unity.Mathematics.float2 xy, System.Single z, System.Single w)
  RVA=0x04D85B30  token=0x60001D2  Unity.Mathematics.float4 float4(Unity.Mathematics.float2 xy, Unity.Mathematics.float2 zw)
  RVA=0x030E7A30  token=0x60001D3  Unity.Mathematics.float4 float4(Unity.Mathematics.float3 xyz, System.Single w)
  RVA=0x0B2526F4  token=0x60001D4  Unity.Mathematics.float4 float4(Unity.Mathematics.float4 xyzw)
  RVA=0x04DAF890  token=0x60001D5  Unity.Mathematics.float4 float4(System.Single v)
  RVA=0x0B2526B8  token=0x60001D6  Unity.Mathematics.float4 float4(System.Boolean v)
  RVA=0x0B2525B4  token=0x60001D7  Unity.Mathematics.float4 float4(Unity.Mathematics.bool4 v)
  RVA=0x04DAF920  token=0x60001D8  Unity.Mathematics.float4 float4(System.Int32 v)
  RVA=0x0B252674  token=0x60001D9  Unity.Mathematics.float4 float4(Unity.Mathematics.int4 v)
  RVA=0x04DAF970  token=0x60001DA  Unity.Mathematics.float4 float4(System.UInt32 v)
  RVA=0x0B2526D0  token=0x60001DB  Unity.Mathematics.float4 float4(Unity.Mathematics.uint4 v)
  RVA=0x0B252628  token=0x60001DC  Unity.Mathematics.float4 float4(Unity.Mathematics.half v)
  RVA=0x0B2525CC  token=0x60001DD  Unity.Mathematics.float4 float4(Unity.Mathematics.half4 v)
  RVA=0x04DAF900  token=0x60001DE  Unity.Mathematics.float4 float4(System.Double v)
  RVA=0x0B252584  token=0x60001DF  Unity.Mathematics.float4 float4(Unity.Mathematics.double4 v)
  RVA=0x0B2545F0  token=0x60001E0  System.UInt32 hash(Unity.Mathematics.float4 v)
  RVA=0x0B2574F4  token=0x60001E1  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4 v)
  RVA=0x0B271974  token=0x60001E2  System.Single shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B2723FC  token=0x60001E3  Unity.Mathematics.float2 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B271F84  token=0x60001E4  Unity.Mathematics.float3 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B271D10  token=0x60001E5  Unity.Mathematics.float4 shuffle(Unity.Mathematics.float4 left, Unity.Mathematics.float4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x0B271530  token=0x60001E6  System.Single select_shuffle_component(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAFA00  token=0x60001E7  Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1)
  RVA=0x05F0DA18  token=0x60001E8  Unity.Mathematics.float4x2 float4x2(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21, System.Single m30, System.Single m31)
  RVA=0x0B252770  token=0x60001E9  Unity.Mathematics.float4x2 float4x2(System.Single v)
  RVA=0x0B252840  token=0x60001EA  Unity.Mathematics.float4x2 float4x2(System.Boolean v)
  RVA=0x0B25285C  token=0x60001EB  Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.bool4x2 v)
  RVA=0x0B252804  token=0x60001EC  Unity.Mathematics.float4x2 float4x2(System.Int32 v)
  RVA=0x0B2527D0  token=0x60001ED  Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.int4x2 v)
  RVA=0x0B252820  token=0x60001EE  Unity.Mathematics.float4x2 float4x2(System.UInt32 v)
  RVA=0x0B25273C  token=0x60001EF  Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.uint4x2 v)
  RVA=0x0B252720  token=0x60001F0  Unity.Mathematics.float4x2 float4x2(System.Double v)
  RVA=0x0B25278C  token=0x60001F1  Unity.Mathematics.float4x2 float4x2(Unity.Mathematics.double4x2 v)
  RVA=0x0B274BF8  token=0x60001F2  Unity.Mathematics.float2x4 transpose(Unity.Mathematics.float4x2 v)
  RVA=0x0B254AC8  token=0x60001F3  System.UInt32 hash(Unity.Mathematics.float4x2 v)
  RVA=0x0B259BC8  token=0x60001F4  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x2 v)
  RVA=0x04DAFA20  token=0x60001F5  Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2)
  RVA=0x05F0DAE4  token=0x60001F6  Unity.Mathematics.float4x3 float4x3(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22, System.Single m30, System.Single m31, System.Single m32)
  RVA=0x0B2529A4  token=0x60001F7  Unity.Mathematics.float4x3 float4x3(System.Single v)
  RVA=0x0B2529C4  token=0x60001F8  Unity.Mathematics.float4x3 float4x3(System.Boolean v)
  RVA=0x0B252924  token=0x60001F9  Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.bool4x3 v)
  RVA=0x0B2529E4  token=0x60001FA  Unity.Mathematics.float4x3 float4x3(System.Int32 v)
  RVA=0x0B252A04  token=0x60001FB  Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.int4x3 v)
  RVA=0x0B252900  token=0x60001FC  Unity.Mathematics.float4x3 float4x3(System.UInt32 v)
  RVA=0x0B252960  token=0x60001FD  Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.uint4x3 v)
  RVA=0x0B2528E0  token=0x60001FE  Unity.Mathematics.float4x3 float4x3(System.Double v)
  RVA=0x0B252880  token=0x60001FF  Unity.Mathematics.float4x3 float4x3(Unity.Mathematics.double4x3 v)
  RVA=0x0B2745A4  token=0x6000200  Unity.Mathematics.float3x4 transpose(Unity.Mathematics.float4x3 v)
  RVA=0x0B255D0C  token=0x6000201  System.UInt32 hash(Unity.Mathematics.float4x3 v)
  RVA=0x0B256EAC  token=0x6000202  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x3 v)
  RVA=0x04DAFA40  token=0x6000203  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2, Unity.Mathematics.float4 c3)
  RVA=0x0B252A44  token=0x6000204  Unity.Mathematics.float4x4 float4x4(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23, System.Single m30, System.Single m31, System.Single m32, System.Single m33)
  RVA=0x0308AEC0  token=0x6000205  Unity.Mathematics.float4x4 float4x4(System.Single v)
  RVA=0x0B252EC0  token=0x6000206  Unity.Mathematics.float4x4 float4x4(System.Boolean v)
  RVA=0x0B252F70  token=0x6000207  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.bool4x4 v)
  RVA=0x0B252EE4  token=0x6000208  Unity.Mathematics.float4x4 float4x4(System.Int32 v)
  RVA=0x0B252B94  token=0x6000209  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.int4x4 v)
  RVA=0x0B252B6C  token=0x600020A  Unity.Mathematics.float4x4 float4x4(System.UInt32 v)
  RVA=0x0B252DF0  token=0x600020B  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.uint4x4 v)
  RVA=0x0B252BF0  token=0x600020C  Unity.Mathematics.float4x4 float4x4(System.Double v)
  RVA=0x0B252C68  token=0x600020D  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.double4x4 v)
  RVA=0x0B270550  token=0x600020E  Unity.Mathematics.float3 rotate(Unity.Mathematics.float4x4 a, Unity.Mathematics.float3 b)
  RVA=0x0B274114  token=0x600020F  Unity.Mathematics.float3 transform(Unity.Mathematics.float4x4 a, Unity.Mathematics.float3 b)
  RVA=0x0B274DA8  token=0x6000210  Unity.Mathematics.float4x4 transpose(Unity.Mathematics.float4x4 v)
  RVA=0x030E7AD0  token=0x6000211  Unity.Mathematics.float4x4 inverse(Unity.Mathematics.float4x4 m)
  RVA=0x0B250D30  token=0x6000212  Unity.Mathematics.float4x4 fastinverse(Unity.Mathematics.float4x4 m)
  RVA=0x04DAE3E0  token=0x6000213  System.Single determinant(Unity.Mathematics.float4x4 m)
  RVA=0x0B253C90  token=0x6000214  System.UInt32 hash(Unity.Mathematics.float4x4 v)
  RVA=0x0B25871C  token=0x6000215  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.float4x4 v)
  RVA=0x04D875C0  token=0x6000216  Unity.Mathematics.half half(Unity.Mathematics.half x)
  RVA=0x0B253650  token=0x6000217  Unity.Mathematics.half half(System.Single v)
  RVA=0x0B253644  token=0x6000218  Unity.Mathematics.half half(System.Double v)
  RVA=0x04DB0230  token=0x6000219  System.UInt32 hash(Unity.Mathematics.half v)
  RVA=0x04DAD5B0  token=0x600021A  Unity.Mathematics.half2 half2(Unity.Mathematics.half x, Unity.Mathematics.half y)
  RVA=0x04DAB3F0  token=0x600021B  Unity.Mathematics.half2 half2(Unity.Mathematics.half2 xy)
  RVA=0x04DB0040  token=0x600021C  Unity.Mathematics.half2 half2(Unity.Mathematics.half v)
  RVA=0x0B253388  token=0x600021D  Unity.Mathematics.half2 half2(System.Single v)
  RVA=0x0B2533B0  token=0x600021E  Unity.Mathematics.half2 half2(Unity.Mathematics.float2 v)
  RVA=0x0B25331C  token=0x600021F  Unity.Mathematics.half2 half2(System.Double v)
  RVA=0x0B253350  token=0x6000220  Unity.Mathematics.half2 half2(Unity.Mathematics.double2 v)
  RVA=0x0B2562F0  token=0x6000221  System.UInt32 hash(Unity.Mathematics.half2 v)
  RVA=0x0B259274  token=0x6000222  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.half2 v)
  RVA=0x04DAD5C0  token=0x6000223  Unity.Mathematics.half3 half3(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z)
  RVA=0x04DB0090  token=0x6000224  Unity.Mathematics.half3 half3(Unity.Mathematics.half x, Unity.Mathematics.half2 yz)
  RVA=0x04DB00B0  token=0x6000225  Unity.Mathematics.half3 half3(Unity.Mathematics.half2 xy, Unity.Mathematics.half z)
  RVA=0x04DB0050  token=0x6000226  Unity.Mathematics.half3 half3(Unity.Mathematics.half3 xyz)
  RVA=0x04DB0080  token=0x6000227  Unity.Mathematics.half3 half3(Unity.Mathematics.half v)
  RVA=0x0B2534B8  token=0x6000228  Unity.Mathematics.half3 half3(System.Single v)
  RVA=0x0B253474  token=0x6000229  Unity.Mathematics.half3 half3(Unity.Mathematics.float3 v)
  RVA=0x0B2533E4  token=0x600022A  Unity.Mathematics.half3 half3(System.Double v)
  RVA=0x0B253428  token=0x600022B  Unity.Mathematics.half3 half3(Unity.Mathematics.double3 v)
  RVA=0x05F0E860  token=0x600022C  System.UInt32 hash(Unity.Mathematics.half3 v)
  RVA=0x05F14938  token=0x600022D  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.half3 v)
  RVA=0x04DAD5E0  token=0x600022E  Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z, Unity.Mathematics.half w)
  RVA=0x04DB01A0  token=0x600022F  Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half2 zw)
  RVA=0x04DB0120  token=0x6000230  Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half2 yz, Unity.Mathematics.half w)
  RVA=0x04DB0200  token=0x6000231  Unity.Mathematics.half4 half4(Unity.Mathematics.half x, Unity.Mathematics.half3 yzw)
  RVA=0x04DB00D0  token=0x6000232  Unity.Mathematics.half4 half4(Unity.Mathematics.half2 xy, Unity.Mathematics.half z, Unity.Mathematics.half w)
  RVA=0x04DB0150  token=0x6000233  Unity.Mathematics.half4 half4(Unity.Mathematics.half2 xy, Unity.Mathematics.half2 zw)
  RVA=0x04DB0170  token=0x6000234  Unity.Mathematics.half4 half4(Unity.Mathematics.half3 xyz, Unity.Mathematics.half w)
  RVA=0x04DB01D0  token=0x6000235  Unity.Mathematics.half4 half4(Unity.Mathematics.half4 xyzw)
  RVA=0x04DB0100  token=0x6000236  Unity.Mathematics.half4 half4(Unity.Mathematics.half v)
  RVA=0x0B253554  token=0x6000237  Unity.Mathematics.half4 half4(System.Single v)
  RVA=0x0B2535F0  token=0x6000238  Unity.Mathematics.half4 half4(Unity.Mathematics.float4 v)
  RVA=0x0B253598  token=0x6000239  Unity.Mathematics.half4 half4(System.Double v)
  RVA=0x0B2534F0  token=0x600023A  Unity.Mathematics.half4 half4(Unity.Mathematics.double4 v)
  RVA=0x05F0E7C0  token=0x600023B  System.UInt32 hash(Unity.Mathematics.half4 v)
  RVA=0x05F160D0  token=0x600023C  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.half4 v)
  RVA=0x04DAD8A0  token=0x600023D  Unity.Mathematics.int2 int2(System.Int32 x, System.Int32 y)
  RVA=0x04DAB5F0  token=0x600023E  Unity.Mathematics.int2 int2(Unity.Mathematics.int2 xy)
  RVA=0x04DB02A0  token=0x600023F  Unity.Mathematics.int2 int2(System.Int32 v)
  RVA=0x0B25A394  token=0x6000240  Unity.Mathematics.int2 int2(System.Boolean v)
  RVA=0x0B25A3AC  token=0x6000241  Unity.Mathematics.int2 int2(Unity.Mathematics.bool2 v)
  RVA=0x04DB02A0  token=0x6000242  Unity.Mathematics.int2 int2(System.UInt32 v)
  RVA=0x04DAB5F0  token=0x6000243  Unity.Mathematics.int2 int2(Unity.Mathematics.uint2 v)
  RVA=0x04DB0280  token=0x6000244  Unity.Mathematics.int2 int2(System.Single v)
  RVA=0x04DB0240  token=0x6000245  Unity.Mathematics.int2 int2(Unity.Mathematics.float2 v)
  RVA=0x04DB0260  token=0x6000246  Unity.Mathematics.int2 int2(System.Double v)
  RVA=0x04DB02B0  token=0x6000247  Unity.Mathematics.int2 int2(Unity.Mathematics.double2 v)
  RVA=0x0B254D4C  token=0x6000248  System.UInt32 hash(Unity.Mathematics.int2 v)
  RVA=0x0B259434  token=0x6000249  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2 v)
  RVA=0x05F34A6C  token=0x600024A  System.Int32 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F34E64  token=0x600024B  Unity.Mathematics.int2 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F34900  token=0x600024C  Unity.Mathematics.int3 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F33F44  token=0x600024D  Unity.Mathematics.int4 shuffle(Unity.Mathematics.int2 left, Unity.Mathematics.int2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F3332C  token=0x600024E  System.Int32 select_shuffle_component(Unity.Mathematics.int2 a, Unity.Mathematics.int2 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x02E515B0  token=0x600024F  Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1)
  RVA=0x0B25A428  token=0x6000250  Unity.Mathematics.int2x2 int2x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11)
  RVA=0x0B25A410  token=0x6000251  Unity.Mathematics.int2x2 int2x2(System.Int32 v)
  RVA=0x0B25A4CC  token=0x6000252  Unity.Mathematics.int2x2 int2x2(System.Boolean v)
  RVA=0x0B25A3CC  token=0x6000253  Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.bool2x2 v)
  RVA=0x0B25A410  token=0x6000254  Unity.Mathematics.int2x2 int2x2(System.UInt32 v)
  RVA=0x0B25A490  token=0x6000255  Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.uint2x2 v)
  RVA=0x0B25A478  token=0x6000256  Unity.Mathematics.int2x2 int2x2(System.Single v)
  RVA=0x0B25A3EC  token=0x6000257  Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.float2x2 v)
  RVA=0x0B25A4B4  token=0x6000258  Unity.Mathematics.int2x2 int2x2(System.Double v)
  RVA=0x0B25A448  token=0x6000259  Unity.Mathematics.int2x2 int2x2(Unity.Mathematics.double2x2 v)
  RVA=0x0B274334  token=0x600025A  Unity.Mathematics.int2x2 transpose(Unity.Mathematics.int2x2 v)
  RVA=0x04DAE310  token=0x600025B  System.Int32 determinant(Unity.Mathematics.int2x2 m)
  RVA=0x0B255708  token=0x600025C  System.UInt32 hash(Unity.Mathematics.int2x2 v)
  RVA=0x0B2582B8  token=0x600025D  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x2 v)
  RVA=0x04DAF5A0  token=0x600025E  Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2)
  RVA=0x05F164A8  token=0x600025F  Unity.Mathematics.int2x3 int2x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12)
  RVA=0x0B25A53C  token=0x6000260  Unity.Mathematics.int2x3 int2x3(System.Int32 v)
  RVA=0x0B25A5F0  token=0x6000261  Unity.Mathematics.int2x3 int2x3(System.Boolean v)
  RVA=0x0B25A57C  token=0x6000262  Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.bool2x3 v)
  RVA=0x0B25A53C  token=0x6000263  Unity.Mathematics.int2x3 int2x3(System.UInt32 v)
  RVA=0x0B25A504  token=0x6000264  Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.uint2x3 v)
  RVA=0x0B25A4E4  token=0x6000265  Unity.Mathematics.int2x3 int2x3(System.Single v)
  RVA=0x0B25A610  token=0x6000266  Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.float2x3 v)
  RVA=0x0B25A55C  token=0x6000267  Unity.Mathematics.int2x3 int2x3(System.Double v)
  RVA=0x0B25A5B4  token=0x6000268  Unity.Mathematics.int2x3 int2x3(Unity.Mathematics.double2x3 v)
  RVA=0x0B274F98  token=0x6000269  Unity.Mathematics.int3x2 transpose(Unity.Mathematics.int2x3 v)
  RVA=0x0B254F84  token=0x600026A  System.UInt32 hash(Unity.Mathematics.int2x3 v)
  RVA=0x0B2591D4  token=0x600026B  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x3 v)
  RVA=0x04DB02D0  token=0x600026C  Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2, Unity.Mathematics.int2 c3)
  RVA=0x05F164E0  token=0x600026D  Unity.Mathematics.int2x4 int2x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13)
  RVA=0x0B25A66C  token=0x600026E  Unity.Mathematics.int2x4 int2x4(System.Int32 v)
  RVA=0x0B25A6BC  token=0x600026F  Unity.Mathematics.int2x4 int2x4(System.Boolean v)
  RVA=0x0B25A648  token=0x6000270  Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.bool2x4 v)
  RVA=0x0B25A66C  token=0x6000271  Unity.Mathematics.int2x4 int2x4(System.UInt32 v)
  RVA=0x0B25A688  token=0x6000272  Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.uint2x4 v)
  RVA=0x0B25A728  token=0x6000273  Unity.Mathematics.int2x4 int2x4(System.Single v)
  RVA=0x0B25A6F4  token=0x6000274  Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.float2x4 v)
  RVA=0x0B25A6D8  token=0x6000275  Unity.Mathematics.int2x4 int2x4(System.Double v)
  RVA=0x0B25A744  token=0x6000276  Unity.Mathematics.int2x4 int2x4(Unity.Mathematics.double2x4 v)
  RVA=0x0B27454C  token=0x6000277  Unity.Mathematics.int4x2 transpose(Unity.Mathematics.int2x4 v)
  RVA=0x0B25473C  token=0x6000278  System.UInt32 hash(Unity.Mathematics.int2x4 v)
  RVA=0x0B257B00  token=0x6000279  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.int2x4 v)
  RVA=0x04DAD8B0  token=0x600027A  Unity.Mathematics.int3 int3(System.Int32 x, System.Int32 y, System.Int32 z)
  RVA=0x04DB0330  token=0x600027B  Unity.Mathematics.int3 int3(System.Int32 x, Unity.Mathematics.int2 yz)
  RVA=0x04DB0350  token=0x600027C  Unity.Mathematics.int3 int3(Unity.Mathematics.int2 xy, System.Int32 z)
  RVA=0x04DB0370  token=0x600027D  Unity.Mathematics.int3 int3(Unity.Mathematics.int3 xyz)
  RVA=0x04DB02F0  token=0x600027E  Unity.Mathematics.int3 int3(System.Int32 v)
  RVA=0x0B25A788  token=0x600027F  Unity.Mathematics.int3 int3(System.Boolean v)
  RVA=0x0B25A7A0  token=0x6000280  Unity.Mathematics.int3 int3(Unity.Mathematics.bool3 v)
  RVA=0x04DB02F0  token=0x6000281  Unity.Mathematics.int3 int3(System.UInt32 v)
  RVA=0x04DB0370  token=0x6000282  Unity.Mathematics.int3 int3(Unity.Mathematics.uint3 v)
  RVA=0x04DB03C0  token=0x6000283  Unity.Mathematics.int3 int3(System.Single v)
  RVA=0x04DB0300  token=0x6000284  Unity.Mathematics.int3 int3(Unity.Mathematics.float3 v)
  RVA=0x04DB03A0  token=0x6000285  Unity.Mathematics.int3 int3(System.Double v)
  RVA=0x04DB03E0  token=0x6000286  Unity.Mathematics.int3 int3(Unity.Mathematics.double3 v)
  RVA=0x0B256128  token=0x6000287  System.UInt32 hash(Unity.Mathematics.int3 v)
  RVA=0x05F15AF0  token=0x6000288  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3 v)
  RVA=0x05F33DE8  token=0x6000289  System.Int32 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F3429C  token=0x600028A  Unity.Mathematics.int2 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F34C80  token=0x600028B  Unity.Mathematics.int3 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F343D8  token=0x600028C  Unity.Mathematics.int4 shuffle(Unity.Mathematics.int3 left, Unity.Mathematics.int3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F327BC  token=0x600028D  System.Int32 select_shuffle_component(Unity.Mathematics.int3 a, Unity.Mathematics.int3 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAF7F0  token=0x600028E  Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1)
  RVA=0x05F16534  token=0x600028F  Unity.Mathematics.int3x2 int3x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21)
  RVA=0x0B25A884  token=0x6000290  Unity.Mathematics.int3x2 int3x2(System.Int32 v)
  RVA=0x0B25A8A4  token=0x6000291  Unity.Mathematics.int3x2 int3x2(System.Boolean v)
  RVA=0x0B25A8C4  token=0x6000292  Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.bool3x2 v)
  RVA=0x0B25A884  token=0x6000293  Unity.Mathematics.int3x2 int3x2(System.UInt32 v)
  RVA=0x0B25A814  token=0x6000294  Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.uint3x2 v)
  RVA=0x0B25A8FC  token=0x6000295  Unity.Mathematics.int3x2 int3x2(System.Single v)
  RVA=0x0B25A84C  token=0x6000296  Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.float3x2 v)
  RVA=0x0B25A91C  token=0x6000297  Unity.Mathematics.int3x2 int3x2(System.Double v)
  RVA=0x0B25A7D4  token=0x6000298  Unity.Mathematics.int3x2 int3x2(Unity.Mathematics.double3x2 v)
  RVA=0x0B274370  token=0x6000299  Unity.Mathematics.int2x3 transpose(Unity.Mathematics.int3x2 v)
  RVA=0x0B256334  token=0x600029A  System.UInt32 hash(Unity.Mathematics.int3x2 v)
  RVA=0x0B2581C0  token=0x600029B  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x2 v)
  RVA=0x034C99D0  token=0x600029C  Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2)
  RVA=0x05F165C4  token=0x600029D  Unity.Mathematics.int3x3 int3x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22)
  RVA=0x0B25A93C  token=0x600029E  Unity.Mathematics.int3x3 int3x3(System.Int32 v)
  RVA=0x0B25AAB4  token=0x600029F  Unity.Mathematics.int3x3 int3x3(System.Boolean v)
  RVA=0x0B25AA74  token=0x60002A0  Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.bool3x3 v)
  RVA=0x0B25A93C  token=0x60002A1  Unity.Mathematics.int3x3 int3x3(System.UInt32 v)
  RVA=0x0B25A97C  token=0x60002A2  Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.uint3x3 v)
  RVA=0x0B25A95C  token=0x60002A3  Unity.Mathematics.int3x3 int3x3(System.Single v)
  RVA=0x0B25AA14  token=0x60002A4  Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.float3x3 v)
  RVA=0x0B25AA54  token=0x60002A5  Unity.Mathematics.int3x3 int3x3(System.Double v)
  RVA=0x0B25A9BC  token=0x60002A6  Unity.Mathematics.int3x3 int3x3(Unity.Mathematics.double3x3 v)
  RVA=0x0B2748D4  token=0x60002A7  Unity.Mathematics.int3x3 transpose(Unity.Mathematics.int3x3 v)
  RVA=0x04DAE920  token=0x60002A8  System.Int32 determinant(Unity.Mathematics.int3x3 m)
  RVA=0x0B25449C  token=0x60002A9  System.UInt32 hash(Unity.Mathematics.int3x3 v)
  RVA=0x0B257994  token=0x60002AA  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x3 v)
  RVA=0x04DAF820  token=0x60002AB  Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2, Unity.Mathematics.int3 c3)
  RVA=0x05F16688  token=0x60002AC  Unity.Mathematics.int3x4 int3x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23)
  RVA=0x0B25AB90  token=0x60002AD  Unity.Mathematics.int3x4 int3x4(System.Int32 v)
  RVA=0x0B25AAD4  token=0x60002AE  Unity.Mathematics.int3x4 int3x4(System.Boolean v)
  RVA=0x0B25AB54  token=0x60002AF  Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.bool3x4 v)
  RVA=0x0B25AB90  token=0x60002B0  Unity.Mathematics.int3x4 int3x4(System.UInt32 v)
  RVA=0x0B25AC34  token=0x60002B1  Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.uint3x4 v)
  RVA=0x0B25AC14  token=0x60002B2  Unity.Mathematics.int3x4 int3x4(System.Single v)
  RVA=0x0B25ABB0  token=0x60002B3  Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.float3x4 v)
  RVA=0x0B25ABF4  token=0x60002B4  Unity.Mathematics.int3x4 int3x4(System.Double v)
  RVA=0x0B25AAF4  token=0x60002B5  Unity.Mathematics.int3x4 int3x4(Unity.Mathematics.double3x4 v)
  RVA=0x0B27445C  token=0x60002B6  Unity.Mathematics.int4x3 transpose(Unity.Mathematics.int3x4 v)
  RVA=0x0B254240  token=0x60002B7  System.UInt32 hash(Unity.Mathematics.int3x4 v)
  RVA=0x0B258984  token=0x60002B8  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.int3x4 v)
  RVA=0x04DAD8C0  token=0x60002B9  Unity.Mathematics.int4 int4(System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 w)
  RVA=0x04DB04B0  token=0x60002BA  Unity.Mathematics.int4 int4(System.Int32 x, System.Int32 y, Unity.Mathematics.int2 zw)
  RVA=0x04DB0530  token=0x60002BB  Unity.Mathematics.int4 int4(System.Int32 x, Unity.Mathematics.int2 yz, System.Int32 w)
  RVA=0x04DB0430  token=0x60002BC  Unity.Mathematics.int4 int4(System.Int32 x, Unity.Mathematics.int3 yzw)
  RVA=0x04DB04D0  token=0x60002BD  Unity.Mathematics.int4 int4(Unity.Mathematics.int2 xy, System.Int32 z, System.Int32 w)
  RVA=0x04DAB610  token=0x60002BE  Unity.Mathematics.int4 int4(Unity.Mathematics.int2 xy, Unity.Mathematics.int2 zw)
  RVA=0x04DB04F0  token=0x60002BF  Unity.Mathematics.int4 int4(Unity.Mathematics.int3 xyz, System.Int32 w)
  RVA=0x0B25AC78  token=0x60002C0  Unity.Mathematics.int4 int4(Unity.Mathematics.int4 xyzw)
  RVA=0x04DB0420  token=0x60002C1  Unity.Mathematics.int4 int4(System.Int32 v)
  RVA=0x0B25ACA4  token=0x60002C2  Unity.Mathematics.int4 int4(System.Boolean v)
  RVA=0x0B25ACC4  token=0x60002C3  Unity.Mathematics.int4 int4(Unity.Mathematics.bool4 v)
  RVA=0x04DB0420  token=0x60002C4  Unity.Mathematics.int4 int4(System.UInt32 v)
  RVA=0x0B25AC78  token=0x60002C5  Unity.Mathematics.int4 int4(Unity.Mathematics.uint4 v)
  RVA=0x04DB0490  token=0x60002C6  Unity.Mathematics.int4 int4(System.Single v)
  RVA=0x0B25AD38  token=0x60002C7  Unity.Mathematics.int4 int4(Unity.Mathematics.float4 v)
  RVA=0x04DB0470  token=0x60002C8  Unity.Mathematics.int4 int4(System.Double v)
  RVA=0x0B25AD08  token=0x60002C9  Unity.Mathematics.int4 int4(Unity.Mathematics.double4 v)
  RVA=0x0B256414  token=0x60002CA  System.UInt32 hash(Unity.Mathematics.int4 v)
  RVA=0x0B259FE0  token=0x60002CB  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4 v)
  RVA=0x0B271EA0  token=0x60002CC  System.Int32 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B2716B8  token=0x60002CD  Unity.Mathematics.int2 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B272564  token=0x60002CE  Unity.Mathematics.int3 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B27189C  token=0x60002CF  Unity.Mathematics.int4 shuffle(Unity.Mathematics.int4 left, Unity.Mathematics.int4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F32DD0  token=0x60002D0  System.Int32 select_shuffle_component(Unity.Mathematics.int4 a, Unity.Mathematics.int4 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAFA00  token=0x60002D1  Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1)
  RVA=0x05F16750  token=0x60002D2  Unity.Mathematics.int4x2 int4x2(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21, System.Int32 m30, System.Int32 m31)
  RVA=0x0B25ADCC  token=0x60002D3  Unity.Mathematics.int4x2 int4x2(System.Int32 v)
  RVA=0x0B25AE38  token=0x60002D4  Unity.Mathematics.int4x2 int4x2(System.Boolean v)
  RVA=0x0B25AE88  token=0x60002D5  Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.bool4x2 v)
  RVA=0x0B25ADCC  token=0x60002D6  Unity.Mathematics.int4x2 int4x2(System.UInt32 v)
  RVA=0x0B25ADE8  token=0x60002D7  Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.uint4x2 v)
  RVA=0x0B25AE1C  token=0x60002D8  Unity.Mathematics.int4x2 int4x2(System.Single v)
  RVA=0x0B25AE54  token=0x60002D9  Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.float4x2 v)
  RVA=0x0B25AD6C  token=0x60002DA  Unity.Mathematics.int4x2 int4x2(System.Double v)
  RVA=0x0B25AD88  token=0x60002DB  Unity.Mathematics.int4x2 int4x2(Unity.Mathematics.double4x2 v)
  RVA=0x0B2743BC  token=0x60002DC  Unity.Mathematics.int2x4 transpose(Unity.Mathematics.int4x2 v)
  RVA=0x0B25587C  token=0x60002DD  System.UInt32 hash(Unity.Mathematics.int4x2 v)
  RVA=0x0B257E2C  token=0x60002DE  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x2 v)
  RVA=0x04DAFA20  token=0x60002DF  Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2)
  RVA=0x05F16804  token=0x60002E0  Unity.Mathematics.int4x3 int4x3(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m30, System.Int32 m31, System.Int32 m32)
  RVA=0x0B25AECC  token=0x60002E1  Unity.Mathematics.int4x3 int4x3(System.Int32 v)
  RVA=0x0B25AFEC  token=0x60002E2  Unity.Mathematics.int4x3 int4x3(System.Boolean v)
  RVA=0x0B25AF6C  token=0x60002E3  Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.bool4x3 v)
  RVA=0x0B25AECC  token=0x60002E4  Unity.Mathematics.int4x3 int4x3(System.UInt32 v)
  RVA=0x0B25AFA8  token=0x60002E5  Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.uint4x3 v)
  RVA=0x0B25AF4C  token=0x60002E6  Unity.Mathematics.int4x3 int4x3(System.Single v)
  RVA=0x0B25B00C  token=0x60002E7  Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.float4x3 v)
  RVA=0x0B25AEAC  token=0x60002E8  Unity.Mathematics.int4x3 int4x3(System.Double v)
  RVA=0x0B25AEEC  token=0x60002E9  Unity.Mathematics.int4x3 int4x3(Unity.Mathematics.double4x3 v)
  RVA=0x0B274770  token=0x60002EA  Unity.Mathematics.int3x4 transpose(Unity.Mathematics.int4x3 v)
  RVA=0x0B255948  token=0x60002EB  System.UInt32 hash(Unity.Mathematics.int4x3 v)
  RVA=0x0B259474  token=0x60002EC  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x3 v)
  RVA=0x04DAFA40  token=0x60002ED  Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2, Unity.Mathematics.int4 c3)
  RVA=0x05F169CC  token=0x60002EE  Unity.Mathematics.int4x4 int4x4(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23, System.Int32 m30, System.Int32 m31, System.Int32 m32, System.Int32 m33)
  RVA=0x0B25B0CC  token=0x60002EF  Unity.Mathematics.int4x4 int4x4(System.Int32 v)
  RVA=0x0B25B04C  token=0x60002F0  Unity.Mathematics.int4x4 int4x4(System.Boolean v)
  RVA=0x0B25B114  token=0x60002F1  Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.bool4x4 v)
  RVA=0x0B25B0CC  token=0x60002F2  Unity.Mathematics.int4x4 int4x4(System.UInt32 v)
  RVA=0x0B25B14C  token=0x60002F3  Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.uint4x4 v)
  RVA=0x0B25B0F0  token=0x60002F4  Unity.Mathematics.int4x4 int4x4(System.Single v)
  RVA=0x0B25B070  token=0x60002F5  Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.float4x4 v)
  RVA=0x0B25B234  token=0x60002F6  Unity.Mathematics.int4x4 int4x4(System.Double v)
  RVA=0x0B25B1A8  token=0x60002F7  Unity.Mathematics.int4x4 int4x4(Unity.Mathematics.double4x4 v)
  RVA=0x0B274AF4  token=0x60002F8  Unity.Mathematics.int4x4 transpose(Unity.Mathematics.int4x4 v)
  RVA=0x04DAE650  token=0x60002F9  System.Int32 determinant(Unity.Mathematics.int4x4 m)
  RVA=0x0B2552C8  token=0x60002FA  System.UInt32 hash(Unity.Mathematics.int4x4 v)
  RVA=0x0B25832C  token=0x60002FB  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.int4x4 v)
  RVA=0x04D88BE0  token=0x60002FC  System.Int32 asint(System.UInt32 x)
  RVA=0x0464E630  token=0x60002FD  Unity.Mathematics.int2 asint(Unity.Mathematics.uint2 x)
  RVA=0x04D93250  token=0x60002FE  Unity.Mathematics.int3 asint(Unity.Mathematics.uint3 x)
  RVA=0x02B77630  token=0x60002FF  Unity.Mathematics.int4 asint(Unity.Mathematics.uint4 x)
  RVA=0x04D94B90  token=0x6000300  System.Int32 asint(System.Single x)
  RVA=0x04DAD550  token=0x6000301  Unity.Mathematics.int2 asint(Unity.Mathematics.float2 x)
  RVA=0x04D93250  token=0x6000302  Unity.Mathematics.int3 asint(Unity.Mathematics.float3 x)
  RVA=0x02B77630  token=0x6000303  Unity.Mathematics.int4 asint(Unity.Mathematics.float4 x)
  RVA=0x04D88BE0  token=0x6000304  System.UInt32 asuint(System.Int32 x)
  RVA=0x0464E630  token=0x6000305  Unity.Mathematics.uint2 asuint(Unity.Mathematics.int2 x)
  RVA=0x04D93250  token=0x6000306  Unity.Mathematics.uint3 asuint(Unity.Mathematics.int3 x)
  RVA=0x02B77630  token=0x6000307  Unity.Mathematics.uint4 asuint(Unity.Mathematics.int4 x)
  RVA=0x04D94B90  token=0x6000308  System.UInt32 asuint(System.Single x)
  RVA=0x04DAD550  token=0x6000309  Unity.Mathematics.uint2 asuint(Unity.Mathematics.float2 x)
  RVA=0x04D93250  token=0x600030A  Unity.Mathematics.uint3 asuint(Unity.Mathematics.float3 x)
  RVA=0x02B77630  token=0x600030B  Unity.Mathematics.uint4 asuint(Unity.Mathematics.float4 x)
  RVA=0x0464E630  token=0x600030C  System.Int64 aslong(System.UInt64 x)
  RVA=0x04DA48C0  token=0x600030D  System.Int64 aslong(System.Double x)
  RVA=0x0464E630  token=0x600030E  System.UInt64 asulong(System.Int64 x)
  RVA=0x04DA48C0  token=0x600030F  System.UInt64 asulong(System.Double x)
  RVA=0x04D94BC0  token=0x6000310  System.Single asfloat(System.Int32 x)
  RVA=0x04DAD350  token=0x6000311  Unity.Mathematics.float2 asfloat(Unity.Mathematics.int2 x)
  RVA=0x04D93250  token=0x6000312  Unity.Mathematics.float3 asfloat(Unity.Mathematics.int3 x)
  RVA=0x02B77630  token=0x6000313  Unity.Mathematics.float4 asfloat(Unity.Mathematics.int4 x)
  RVA=0x04D94BC0  token=0x6000314  System.Single asfloat(System.UInt32 x)
  RVA=0x04DAD350  token=0x6000315  Unity.Mathematics.float2 asfloat(Unity.Mathematics.uint2 x)
  RVA=0x04D93250  token=0x6000316  Unity.Mathematics.float3 asfloat(Unity.Mathematics.uint3 x)
  RVA=0x02B77630  token=0x6000317  Unity.Mathematics.float4 asfloat(Unity.Mathematics.uint4 x)
  RVA=0x0B24C880  token=0x6000318  System.Int32 bitmask(Unity.Mathematics.bool4 value)
  RVA=0x04DA48D0  token=0x6000319  System.Double asdouble(System.Int64 x)
  RVA=0x04DA48D0  token=0x600031A  System.Double asdouble(System.UInt64 x)
  RVA=0x0B25CEC0  token=0x600031B  System.Boolean isfinite(System.Single x)
  RVA=0x0B25CE48  token=0x600031C  Unity.Mathematics.bool2 isfinite(Unity.Mathematics.float2 x)
  RVA=0x0B25CD08  token=0x600031D  Unity.Mathematics.bool3 isfinite(Unity.Mathematics.float3 x)
  RVA=0x0B25CD74  token=0x600031E  Unity.Mathematics.bool4 isfinite(Unity.Mathematics.float4 x)
  RVA=0x0B25CCE0  token=0x600031F  System.Boolean isfinite(System.Double x)
  RVA=0x0B25CE7C  token=0x6000320  Unity.Mathematics.bool2 isfinite(Unity.Mathematics.double2 x)
  RVA=0x0B25CEDC  token=0x6000321  Unity.Mathematics.bool3 isfinite(Unity.Mathematics.double3 x)
  RVA=0x0B25CDD8  token=0x6000322  Unity.Mathematics.bool4 isfinite(Unity.Mathematics.double4 x)
  RVA=0x05F16FD4  token=0x6000323  System.Boolean isinf(System.Single x)
  RVA=0x0B25D030  token=0x6000324  Unity.Mathematics.bool2 isinf(Unity.Mathematics.float2 x)
  RVA=0x0B25D0C8  token=0x6000325  Unity.Mathematics.bool3 isinf(Unity.Mathematics.float3 x)
  RVA=0x0B25D08C  token=0x6000326  Unity.Mathematics.bool4 isinf(Unity.Mathematics.float4 x)
  RVA=0x0B25CFB8  token=0x6000327  System.Boolean isinf(System.Double x)
  RVA=0x0B25D050  token=0x6000328  Unity.Mathematics.bool2 isinf(Unity.Mathematics.double2 x)
  RVA=0x0B25CF50  token=0x6000329  Unity.Mathematics.bool3 isinf(Unity.Mathematics.double3 x)
  RVA=0x0B25CFE4  token=0x600032A  Unity.Mathematics.bool4 isinf(Unity.Mathematics.double4 x)
  RVA=0x0B25D1DC  token=0x600032B  System.Boolean isnan(System.Single x)
  RVA=0x0B25D190  token=0x600032C  Unity.Mathematics.bool2 isnan(Unity.Mathematics.float2 x)
  RVA=0x0B25D338  token=0x600032D  Unity.Mathematics.bool3 isnan(Unity.Mathematics.float3 x)
  RVA=0x0B25D2BC  token=0x600032E  Unity.Mathematics.bool4 isnan(Unity.Mathematics.float4 x)
  RVA=0x0B25D258  token=0x600032F  System.Boolean isnan(System.Double x)
  RVA=0x0B25D27C  token=0x6000330  Unity.Mathematics.bool2 isnan(Unity.Mathematics.double2 x)
  RVA=0x0B25D130  token=0x6000331  Unity.Mathematics.bool3 isnan(Unity.Mathematics.double3 x)
  RVA=0x0B25D1F0  token=0x6000332  Unity.Mathematics.bool4 isnan(Unity.Mathematics.double4 x)
  RVA=0x0B25D404  token=0x6000333  System.Boolean ispow2(System.Int32 x)
  RVA=0x0B25D414  token=0x6000334  Unity.Mathematics.bool2 ispow2(Unity.Mathematics.int2 x)
  RVA=0x0B25D3B0  token=0x6000335  Unity.Mathematics.bool3 ispow2(Unity.Mathematics.int3 x)
  RVA=0x0B25D554  token=0x6000336  Unity.Mathematics.bool4 ispow2(Unity.Mathematics.int4 x)
  RVA=0x0B25D4C0  token=0x6000337  System.Boolean ispow2(System.UInt32 x)
  RVA=0x0B25D4D0  token=0x6000338  Unity.Mathematics.bool2 ispow2(Unity.Mathematics.uint2 x)
  RVA=0x0B25D504  token=0x6000339  Unity.Mathematics.bool3 ispow2(Unity.Mathematics.uint3 x)
  RVA=0x0B25D448  token=0x600033A  Unity.Mathematics.bool4 ispow2(Unity.Mathematics.uint4 x)
  RVA=0x0B25E778  token=0x600033B  System.Int32 min(System.Int32 x, System.Int32 y)
  RVA=0x0B25EA5C  token=0x600033C  Unity.Mathematics.int2 min(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y)
  RVA=0x0B25EA9C  token=0x600033D  Unity.Mathematics.int3 min(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y)
  RVA=0x0B25E854  token=0x600033E  Unity.Mathematics.int4 min(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y)
  RVA=0x0B25EA94  token=0x600033F  System.UInt32 min(System.UInt32 x, System.UInt32 y)
  RVA=0x0B25E940  token=0x6000340  Unity.Mathematics.uint2 min(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y)
  RVA=0x0B25E9D8  token=0x6000341  Unity.Mathematics.uint3 min(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y)
  RVA=0x0B25EA0C  token=0x6000342  Unity.Mathematics.uint4 min(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y)
  RVA=0x0B25EA50  token=0x6000343  System.Int64 min(System.Int64 x, System.Int64 y)
  RVA=0x0B25EA88  token=0x6000344  System.UInt64 min(System.UInt64 x, System.UInt64 y)
  RVA=0x0B25E96C  token=0x6000345  System.Single min(System.Single x, System.Single y)
  RVA=0x0B25E980  token=0x6000346  Unity.Mathematics.float2 min(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x0B25E7EC  token=0x6000347  Unity.Mathematics.float3 min(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x05F1883C  token=0x6000348  Unity.Mathematics.float4 min(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x0B25E898  token=0x6000349  System.Double min(System.Double x, System.Double y)
  RVA=0x05F186FC  token=0x600034A  Unity.Mathematics.double2 min(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x0B25E780  token=0x600034B  Unity.Mathematics.double3 min(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B25E8B0  token=0x600034C  Unity.Mathematics.double4 min(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x03347DE0  token=0x600034D  System.Int32 max(System.Int32 x, System.Int32 y)
  RVA=0x0B25E444  token=0x600034E  Unity.Mathematics.int2 max(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y)
  RVA=0x0B25E410  token=0x600034F  Unity.Mathematics.int3 max(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y)
  RVA=0x0B25E5D0  token=0x6000350  Unity.Mathematics.int4 max(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y)
  RVA=0x0B25E6AC  token=0x6000351  System.UInt32 max(System.UInt32 x, System.UInt32 y)
  RVA=0x0B25E6B4  token=0x6000352  Unity.Mathematics.uint2 max(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y)
  RVA=0x0B25E6EC  token=0x6000353  Unity.Mathematics.uint3 max(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y)
  RVA=0x0B25E58C  token=0x6000354  Unity.Mathematics.uint4 max(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y)
  RVA=0x0B25E6E0  token=0x6000355  System.Int64 max(System.Int64 x, System.Int64 y)
  RVA=0x0B25E4F4  token=0x6000356  System.UInt64 max(System.UInt64 x, System.UInt64 y)
  RVA=0x0B25E4E0  token=0x6000357  System.Single max(System.Single x, System.Single y)
  RVA=0x0B25E720  token=0x6000358  Unity.Mathematics.float2 max(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x0B25E470  token=0x6000359  Unity.Mathematics.float3 max(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x05F183B4  token=0x600035A  Unity.Mathematics.float4 max(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x0B25E500  token=0x600035B  System.Double max(System.Double x, System.Double y)
  RVA=0x05F18500  token=0x600035C  Unity.Mathematics.double2 max(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x0B25E518  token=0x600035D  Unity.Mathematics.double3 max(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B25E614  token=0x600035E  Unity.Mathematics.double4 max(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x04D899F0  token=0x600035F  System.Single lerp(System.Single start, System.Single end, System.Single t)
  RVA=0x0B25DC44  token=0x6000360  Unity.Mathematics.float2 lerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, System.Single t)
  RVA=0x05F176FC  token=0x6000361  Unity.Mathematics.float3 lerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, System.Single t)
  RVA=0x0B25DC80  token=0x6000362  Unity.Mathematics.float4 lerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, System.Single t)
  RVA=0x0B25D878  token=0x6000363  Unity.Mathematics.float2 lerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, Unity.Mathematics.float2 t)
  RVA=0x05F172F4  token=0x6000364  Unity.Mathematics.float3 lerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, Unity.Mathematics.float3 t)
  RVA=0x0B25D7F0  token=0x6000365  Unity.Mathematics.float4 lerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, Unity.Mathematics.float4 t)
  RVA=0x04DB05D0  token=0x6000366  System.Double lerp(System.Double start, System.Double end, System.Double t)
  RVA=0x0B25D774  token=0x6000367  Unity.Mathematics.double2 lerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, System.Double t)
  RVA=0x0B25DB84  token=0x6000368  Unity.Mathematics.double3 lerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, System.Double t)
  RVA=0x0B25DA48  token=0x6000369  Unity.Mathematics.double4 lerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, System.Double t)
  RVA=0x0B25DAFC  token=0x600036A  Unity.Mathematics.double2 lerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, Unity.Mathematics.double2 t)
  RVA=0x0B25D978  token=0x600036B  Unity.Mathematics.double3 lerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, Unity.Mathematics.double3 t)
  RVA=0x0B25D8B4  token=0x600036C  Unity.Mathematics.double4 lerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, Unity.Mathematics.double4 t)
  RVA=0x0B2760B4  token=0x600036D  System.Single unlerp(System.Single start, System.Single end, System.Single x)
  RVA=0x0B27617C  token=0x600036E  Unity.Mathematics.float2 unlerp(Unity.Mathematics.float2 start, Unity.Mathematics.float2 end, Unity.Mathematics.float2 x)
  RVA=0x05F3A004  token=0x600036F  Unity.Mathematics.float3 unlerp(Unity.Mathematics.float3 start, Unity.Mathematics.float3 end, Unity.Mathematics.float3 x)
  RVA=0x0B2760C4  token=0x6000370  Unity.Mathematics.float4 unlerp(Unity.Mathematics.float4 start, Unity.Mathematics.float4 end, Unity.Mathematics.float4 x)
  RVA=0x0B2762B4  token=0x6000371  System.Double unlerp(System.Double start, System.Double end, System.Double x)
  RVA=0x0B27603C  token=0x6000372  Unity.Mathematics.double2 unlerp(Unity.Mathematics.double2 start, Unity.Mathematics.double2 end, Unity.Mathematics.double2 x)
  RVA=0x0B275F4C  token=0x6000373  Unity.Mathematics.double3 unlerp(Unity.Mathematics.double3 start, Unity.Mathematics.double3 end, Unity.Mathematics.double3 x)
  RVA=0x0B2761D8  token=0x6000374  Unity.Mathematics.double4 unlerp(Unity.Mathematics.double4 start, Unity.Mathematics.double4 end, Unity.Mathematics.double4 x)
  RVA=0x0B26F930  token=0x6000375  System.Single remap(System.Single srcStart, System.Single srcEnd, System.Single dstStart, System.Single dstEnd, System.Single x)
  RVA=0x0B26F8AC  token=0x6000376  Unity.Mathematics.float2 remap(Unity.Mathematics.float2 srcStart, Unity.Mathematics.float2 srcEnd, Unity.Mathematics.float2 dstStart, Unity.Mathematics.float2 dstEnd, Unity.Mathematics.float2 x)
  RVA=0x05F30DA0  token=0x6000377  Unity.Mathematics.float3 remap(Unity.Mathematics.float3 srcStart, Unity.Mathematics.float3 srcEnd, Unity.Mathematics.float3 dstStart, Unity.Mathematics.float3 dstEnd, Unity.Mathematics.float3 x)
  RVA=0x0B26F7F8  token=0x6000378  Unity.Mathematics.float4 remap(Unity.Mathematics.float4 srcStart, Unity.Mathematics.float4 srcEnd, Unity.Mathematics.float4 dstStart, Unity.Mathematics.float4 dstEnd, Unity.Mathematics.float4 x)
  RVA=0x0B26F888  token=0x6000379  System.Double remap(System.Double srcStart, System.Double srcEnd, System.Double dstStart, System.Double dstEnd, System.Double x)
  RVA=0x0B26FA34  token=0x600037A  Unity.Mathematics.double2 remap(Unity.Mathematics.double2 srcStart, Unity.Mathematics.double2 srcEnd, Unity.Mathematics.double2 dstStart, Unity.Mathematics.double2 dstEnd, Unity.Mathematics.double2 x)
  RVA=0x0B26F954  token=0x600037B  Unity.Mathematics.double3 remap(Unity.Mathematics.double3 srcStart, Unity.Mathematics.double3 srcEnd, Unity.Mathematics.double3 dstStart, Unity.Mathematics.double3 dstEnd, Unity.Mathematics.double3 x)
  RVA=0x0B26FAC4  token=0x600037C  Unity.Mathematics.double4 remap(Unity.Mathematics.double4 srcStart, Unity.Mathematics.double4 srcEnd, Unity.Mathematics.double4 dstStart, Unity.Mathematics.double4 dstEnd, Unity.Mathematics.double4 x)
  RVA=0x04DB09D0  token=0x600037D  System.Int32 mad(System.Int32 mulA, System.Int32 mulB, System.Int32 addC)
  RVA=0x0B25E1F0  token=0x600037E  Unity.Mathematics.int2 mad(Unity.Mathematics.int2 mulA, Unity.Mathematics.int2 mulB, Unity.Mathematics.int2 addC)
  RVA=0x05F180B4  token=0x600037F  Unity.Mathematics.int3 mad(Unity.Mathematics.int3 mulA, Unity.Mathematics.int3 mulB, Unity.Mathematics.int3 addC)
  RVA=0x0B25E18C  token=0x6000380  Unity.Mathematics.int4 mad(Unity.Mathematics.int4 mulA, Unity.Mathematics.int4 mulB, Unity.Mathematics.int4 addC)
  RVA=0x04DB09D0  token=0x6000381  System.UInt32 mad(System.UInt32 mulA, System.UInt32 mulB, System.UInt32 addC)
  RVA=0x0B25E1F0  token=0x6000382  Unity.Mathematics.uint2 mad(Unity.Mathematics.uint2 mulA, Unity.Mathematics.uint2 mulB, Unity.Mathematics.uint2 addC)
  RVA=0x05F180B4  token=0x6000383  Unity.Mathematics.uint3 mad(Unity.Mathematics.uint3 mulA, Unity.Mathematics.uint3 mulB, Unity.Mathematics.uint3 addC)
  RVA=0x0B25E18C  token=0x6000384  Unity.Mathematics.uint4 mad(Unity.Mathematics.uint4 mulA, Unity.Mathematics.uint4 mulB, Unity.Mathematics.uint4 addC)
  RVA=0x04DB09A0  token=0x6000385  System.Int64 mad(System.Int64 mulA, System.Int64 mulB, System.Int64 addC)
  RVA=0x04DB09A0  token=0x6000386  System.UInt64 mad(System.UInt64 mulA, System.UInt64 mulB, System.UInt64 addC)
  RVA=0x04DB09B0  token=0x6000387  System.Single mad(System.Single mulA, System.Single mulB, System.Single addC)
  RVA=0x0B25E2A0  token=0x6000388  Unity.Mathematics.float2 mad(Unity.Mathematics.float2 mulA, Unity.Mathematics.float2 mulB, Unity.Mathematics.float2 addC)
  RVA=0x05F1832C  token=0x6000389  Unity.Mathematics.float3 mad(Unity.Mathematics.float3 mulA, Unity.Mathematics.float3 mulB, Unity.Mathematics.float3 addC)
  RVA=0x0B25E31C  token=0x600038A  Unity.Mathematics.float4 mad(Unity.Mathematics.float4 mulA, Unity.Mathematics.float4 mulB, Unity.Mathematics.float4 addC)
  RVA=0x04DB09C0  token=0x600038B  System.Double mad(System.Double mulA, System.Double mulB, System.Double addC)
  RVA=0x0B25E2B8  token=0x600038C  Unity.Mathematics.double2 mad(Unity.Mathematics.double2 mulA, Unity.Mathematics.double2 mulB, Unity.Mathematics.double2 addC)
  RVA=0x0B25E208  token=0x600038D  Unity.Mathematics.double3 mad(Unity.Mathematics.double3 mulA, Unity.Mathematics.double3 mulB, Unity.Mathematics.double3 addC)
  RVA=0x0B25E380  token=0x600038E  Unity.Mathematics.double4 mad(Unity.Mathematics.double4 mulA, Unity.Mathematics.double4 mulB, Unity.Mathematics.double4 addC)
  RVA=0x0B24D4C8  token=0x600038F  System.Int32 clamp(System.Int32 valueToClamp, System.Int32 lowerBound, System.Int32 upperBound)
  RVA=0x0B24D518  token=0x6000390  Unity.Mathematics.int2 clamp(Unity.Mathematics.int2 valueToClamp, Unity.Mathematics.int2 lowerBound, Unity.Mathematics.int2 upperBound)
  RVA=0x05F09A58  token=0x6000391  Unity.Mathematics.int3 clamp(Unity.Mathematics.int3 valueToClamp, Unity.Mathematics.int3 lowerBound, Unity.Mathematics.int3 upperBound)
  RVA=0x0B24D404  token=0x6000392  Unity.Mathematics.int4 clamp(Unity.Mathematics.int4 valueToClamp, Unity.Mathematics.int4 lowerBound, Unity.Mathematics.int4 upperBound)
  RVA=0x0B24D3F0  token=0x6000393  System.UInt32 clamp(System.UInt32 valueToClamp, System.UInt32 lowerBound, System.UInt32 upperBound)
  RVA=0x0B24D2CC  token=0x6000394  Unity.Mathematics.uint2 clamp(Unity.Mathematics.uint2 valueToClamp, Unity.Mathematics.uint2 lowerBound, Unity.Mathematics.uint2 upperBound)
  RVA=0x05F09C00  token=0x6000395  Unity.Mathematics.uint3 clamp(Unity.Mathematics.uint3 valueToClamp, Unity.Mathematics.uint3 lowerBound, Unity.Mathematics.uint3 upperBound)
  RVA=0x0B24D104  token=0x6000396  Unity.Mathematics.uint4 clamp(Unity.Mathematics.uint4 valueToClamp, Unity.Mathematics.uint4 lowerBound, Unity.Mathematics.uint4 upperBound)
  RVA=0x0B24D350  token=0x6000397  System.Int64 clamp(System.Int64 valueToClamp, System.Int64 lowerBound, System.Int64 upperBound)
  RVA=0x0B24D504  token=0x6000398  System.UInt64 clamp(System.UInt64 valueToClamp, System.UInt64 lowerBound, System.UInt64 upperBound)
  RVA=0x0B24D4DC  token=0x6000399  System.Single clamp(System.Single valueToClamp, System.Single lowerBound, System.Single upperBound)
  RVA=0x0B24D328  token=0x600039A  Unity.Mathematics.float2 clamp(Unity.Mathematics.float2 valueToClamp, Unity.Mathematics.float2 lowerBound, Unity.Mathematics.float2 upperBound)
  RVA=0x0B24D364  token=0x600039B  Unity.Mathematics.float3 clamp(Unity.Mathematics.float3 valueToClamp, Unity.Mathematics.float3 lowerBound, Unity.Mathematics.float3 upperBound)
  RVA=0x0B24D1C8  token=0x600039C  Unity.Mathematics.float4 clamp(Unity.Mathematics.float4 valueToClamp, Unity.Mathematics.float4 lowerBound, Unity.Mathematics.float4 upperBound)
  RVA=0x05F09928  token=0x600039D  System.Double clamp(System.Double valueToClamp, System.Double lowerBound, System.Double upperBound)
  RVA=0x0B24D574  token=0x600039E  Unity.Mathematics.double2 clamp(Unity.Mathematics.double2 valueToClamp, Unity.Mathematics.double2 lowerBound, Unity.Mathematics.double2 upperBound)
  RVA=0x0B24D230  token=0x600039F  Unity.Mathematics.double3 clamp(Unity.Mathematics.double3 valueToClamp, Unity.Mathematics.double3 lowerBound, Unity.Mathematics.double3 upperBound)
  RVA=0x0B24D5DC  token=0x60003A0  Unity.Mathematics.double4 clamp(Unity.Mathematics.double4 valueToClamp, Unity.Mathematics.double4 lowerBound, Unity.Mathematics.double4 upperBound)
  RVA=0x09F3AE20  token=0x60003A1  System.Single saturate(System.Single x)
  RVA=0x0B270EE0  token=0x60003A2  Unity.Mathematics.float2 saturate(Unity.Mathematics.float2 x)
  RVA=0x05F3215C  token=0x60003A3  Unity.Mathematics.float3 saturate(Unity.Mathematics.float3 x)
  RVA=0x0B270E94  token=0x60003A4  Unity.Mathematics.float4 saturate(Unity.Mathematics.float4 x)
  RVA=0x0B270E84  token=0x60003A5  System.Double saturate(System.Double x)
  RVA=0x0B270FB8  token=0x60003A6  Unity.Mathematics.double2 saturate(Unity.Mathematics.double2 x)
  RVA=0x0B270F1C  token=0x60003A7  Unity.Mathematics.double3 saturate(Unity.Mathematics.double3 x)
  RVA=0x0B270E18  token=0x60003A8  Unity.Mathematics.double4 saturate(Unity.Mathematics.double4 x)
  RVA=0x0B24BC98  token=0x60003A9  System.Int32 abs(System.Int32 x)
  RVA=0x0B24BB9C  token=0x60003AA  Unity.Mathematics.int2 abs(Unity.Mathematics.int2 x)
  RVA=0x05F07C48  token=0x60003AB  Unity.Mathematics.int3 abs(Unity.Mathematics.int3 x)
  RVA=0x0B24BBE0  token=0x60003AC  Unity.Mathematics.int4 abs(Unity.Mathematics.int4 x)
  RVA=0x0B24BB8C  token=0x60003AD  System.Int64 abs(System.Int64 x)
  RVA=0x04DAD080  token=0x60003AE  System.Single abs(System.Single x)
  RVA=0x04DAD140  token=0x60003AF  Unity.Mathematics.float2 abs(Unity.Mathematics.float2 x)
  RVA=0x03B0AD90  token=0x60003B0  Unity.Mathematics.float3 abs(Unity.Mathematics.float3 x)
  RVA=0x0B24BB58  token=0x60003B1  Unity.Mathematics.float4 abs(Unity.Mathematics.float4 x)
  RVA=0x04DAD120  token=0x60003B2  System.Double abs(System.Double x)
  RVA=0x04DAD0F0  token=0x60003B3  Unity.Mathematics.double2 abs(Unity.Mathematics.double2 x)
  RVA=0x04DAD040  token=0x60003B4  Unity.Mathematics.double3 abs(Unity.Mathematics.double3 x)
  RVA=0x04DAD090  token=0x60003B5  Unity.Mathematics.double4 abs(Unity.Mathematics.double4 x)
  RVA=0x04DAEA70  token=0x60003B6  System.Int32 dot(System.Int32 x, System.Int32 y)
  RVA=0x04DAEB00  token=0x60003B7  System.Int32 dot(Unity.Mathematics.int2 x, Unity.Mathematics.int2 y)
  RVA=0x04D85D00  token=0x60003B8  System.Int32 dot(Unity.Mathematics.int3 x, Unity.Mathematics.int3 y)
  RVA=0x04DAEAC0  token=0x60003B9  System.Int32 dot(Unity.Mathematics.int4 x, Unity.Mathematics.int4 y)
  RVA=0x04DAEA70  token=0x60003BA  System.UInt32 dot(System.UInt32 x, System.UInt32 y)
  RVA=0x04DAEB00  token=0x60003BB  System.UInt32 dot(Unity.Mathematics.uint2 x, Unity.Mathematics.uint2 y)
  RVA=0x04D85D00  token=0x60003BC  System.UInt32 dot(Unity.Mathematics.uint3 x, Unity.Mathematics.uint3 y)
  RVA=0x04DAEAC0  token=0x60003BD  System.UInt32 dot(Unity.Mathematics.uint4 x, Unity.Mathematics.uint4 y)
  RVA=0x04DAEB20  token=0x60003BE  System.Single dot(System.Single x, System.Single y)
  RVA=0x04D8B640  token=0x60003BF  System.Single dot(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x04D8B7C0  token=0x60003C0  System.Single dot(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x034C9990  token=0x60003C1  System.Single dot(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x04DAEAF0  token=0x60003C2  System.Double dot(System.Double x, System.Double y)
  RVA=0x04DAEB60  token=0x60003C3  System.Double dot(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x04DAEB30  token=0x60003C4  System.Double dot(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x04DAEA80  token=0x60003C5  System.Double dot(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x0B273C40  token=0x60003C6  System.Single tan(System.Single x)
  RVA=0x0B273ACC  token=0x60003C7  Unity.Mathematics.float2 tan(Unity.Mathematics.float2 x)
  RVA=0x05F36CA4  token=0x60003C8  Unity.Mathematics.float3 tan(Unity.Mathematics.float3 x)
  RVA=0x05F36C20  token=0x60003C9  Unity.Mathematics.float4 tan(Unity.Mathematics.float4 x)
  RVA=0x0B273BF0  token=0x60003CA  System.Double tan(System.Double x)
  RVA=0x05F36A40  token=0x60003CB  Unity.Mathematics.double2 tan(Unity.Mathematics.double2 x)
  RVA=0x0B273B8C  token=0x60003CC  Unity.Mathematics.double3 tan(Unity.Mathematics.double3 x)
  RVA=0x0B273B08  token=0x60003CD  Unity.Mathematics.double4 tan(Unity.Mathematics.double4 x)
  RVA=0x0B273E0C  token=0x60003CE  System.Single tanh(System.Single x)
  RVA=0x0B273DD0  token=0x60003CF  Unity.Mathematics.float2 tanh(Unity.Mathematics.float2 x)
  RVA=0x05F36F24  token=0x60003D0  Unity.Mathematics.float3 tanh(Unity.Mathematics.float3 x)
  RVA=0x05F36E10  token=0x60003D1  Unity.Mathematics.float4 tanh(Unity.Mathematics.float4 x)
  RVA=0x0B273D80  token=0x60003D2  System.Double tanh(System.Double x)
  RVA=0x05F36FE0  token=0x60003D3  Unity.Mathematics.double2 tanh(Unity.Mathematics.double2 x)
  RVA=0x0B273C98  token=0x60003D4  Unity.Mathematics.double3 tanh(Unity.Mathematics.double3 x)
  RVA=0x0B273CFC  token=0x60003D5  Unity.Mathematics.double4 tanh(Unity.Mathematics.double4 x)
  RVA=0x0B24C738  token=0x60003D6  System.Single atan(System.Single x)
  RVA=0x0B24C844  token=0x60003D7  Unity.Mathematics.float2 atan(Unity.Mathematics.float2 x)
  RVA=0x05F0886C  token=0x60003D8  Unity.Mathematics.float3 atan(Unity.Mathematics.float3 x)
  RVA=0x05F087E8  token=0x60003D9  Unity.Mathematics.float4 atan(Unity.Mathematics.float4 x)
  RVA=0x0B24C7F4  token=0x60003DA  System.Double atan(System.Double x)
  RVA=0x05F08A30  token=0x60003DB  Unity.Mathematics.double2 atan(Unity.Mathematics.double2 x)
  RVA=0x0B24C790  token=0x60003DC  Unity.Mathematics.double3 atan(Unity.Mathematics.double3 x)
  RVA=0x0B24C6B4  token=0x60003DD  Unity.Mathematics.double4 atan(Unity.Mathematics.double4 x)
  RVA=0x0B24C5AC  token=0x60003DE  System.Single atan2(System.Single y, System.Single x)
  RVA=0x0B24C560  token=0x60003DF  Unity.Mathematics.float2 atan2(Unity.Mathematics.float2 y, Unity.Mathematics.float2 x)
  RVA=0x05F08570  token=0x60003E0  Unity.Mathematics.float3 atan2(Unity.Mathematics.float3 y, Unity.Mathematics.float3 x)
  RVA=0x05F086A0  token=0x60003E1  Unity.Mathematics.float4 atan2(Unity.Mathematics.float4 y, Unity.Mathematics.float4 x)
  RVA=0x0B24C500  token=0x60003E2  System.Double atan2(System.Double y, System.Double x)
  RVA=0x05F08640  token=0x60003E3  Unity.Mathematics.double2 atan2(Unity.Mathematics.double2 y, Unity.Mathematics.double2 x)
  RVA=0x0B24C480  token=0x60003E4  Unity.Mathematics.double3 atan2(Unity.Mathematics.double3 y, Unity.Mathematics.double3 x)
  RVA=0x0B24C610  token=0x60003E5  Unity.Mathematics.double4 atan2(Unity.Mathematics.double4 y, Unity.Mathematics.double4 x)
  RVA=0x0B24D9D8  token=0x60003E6  System.Single cos(System.Single x)
  RVA=0x04DADB80  token=0x60003E7  Unity.Mathematics.float2 cos(Unity.Mathematics.float2 x)
  RVA=0x04DADC10  token=0x60003E8  Unity.Mathematics.float3 cos(Unity.Mathematics.float3 x)
  RVA=0x04DADD00  token=0x60003E9  Unity.Mathematics.float4 cos(Unity.Mathematics.float4 x)
  RVA=0x0B24DA30  token=0x60003EA  System.Double cos(System.Double x)
  RVA=0x04DADC60  token=0x60003EB  Unity.Mathematics.double2 cos(Unity.Mathematics.double2 x)
  RVA=0x04DADBC0  token=0x60003EC  Unity.Mathematics.double3 cos(Unity.Mathematics.double3 x)
  RVA=0x04DADCA0  token=0x60003ED  Unity.Mathematics.double4 cos(Unity.Mathematics.double4 x)
  RVA=0x0B24DA80  token=0x60003EE  System.Single cosh(System.Single x)
  RVA=0x04DADD60  token=0x60003EF  Unity.Mathematics.float2 cosh(Unity.Mathematics.float2 x)
  RVA=0x04DADDF0  token=0x60003F0  Unity.Mathematics.float3 cosh(Unity.Mathematics.float3 x)
  RVA=0x04DADEA0  token=0x60003F1  Unity.Mathematics.float4 cosh(Unity.Mathematics.float4 x)
  RVA=0x0B24DAD8  token=0x60003F2  System.Double cosh(System.Double x)
  RVA=0x04DADF00  token=0x60003F3  Unity.Mathematics.double2 cosh(Unity.Mathematics.double2 x)
  RVA=0x04DADDA0  token=0x60003F4  Unity.Mathematics.double3 cosh(Unity.Mathematics.double3 x)
  RVA=0x04DADE40  token=0x60003F5  Unity.Mathematics.double4 cosh(Unity.Mathematics.double4 x)
  RVA=0x0B24BCF4  token=0x60003F6  System.Single acos(System.Single x)
  RVA=0x04DAD170  token=0x60003F7  Unity.Mathematics.float2 acos(Unity.Mathematics.float2 x)
  RVA=0x04DAD1B0  token=0x60003F8  Unity.Mathematics.float3 acos(Unity.Mathematics.float3 x)
  RVA=0x04DAD260  token=0x60003F9  Unity.Mathematics.float4 acos(Unity.Mathematics.float4 x)
  RVA=0x0B24BCA4  token=0x60003FA  System.Double acos(System.Double x)
  RVA=0x04DAD2C0  token=0x60003FB  Unity.Mathematics.double2 acos(Unity.Mathematics.double2 x)
  RVA=0x04DAD300  token=0x60003FC  Unity.Mathematics.double3 acos(Unity.Mathematics.double3 x)
  RVA=0x04DAD200  token=0x60003FD  Unity.Mathematics.double4 acos(Unity.Mathematics.double4 x)
  RVA=0x0B272850  token=0x60003FE  System.Single sin(System.Single x)
  RVA=0x04DB1B20  token=0x60003FF  Unity.Mathematics.float2 sin(Unity.Mathematics.float2 x)
  RVA=0x04DB1A10  token=0x6000400  Unity.Mathematics.float3 sin(Unity.Mathematics.float3 x)
  RVA=0x04DB1AC0  token=0x6000401  Unity.Mathematics.float4 sin(Unity.Mathematics.float4 x)
  RVA=0x0B2728A8  token=0x6000402  System.Double sin(System.Double x)
  RVA=0x04DB1B60  token=0x6000403  Unity.Mathematics.double2 sin(Unity.Mathematics.double2 x)
  RVA=0x04DB19C0  token=0x6000404  Unity.Mathematics.double3 sin(Unity.Mathematics.double3 x)
  RVA=0x04DB1A60  token=0x6000405  Unity.Mathematics.double4 sin(Unity.Mathematics.double4 x)
  RVA=0x0B272BF0  token=0x6000406  System.Single sinh(System.Single x)
  RVA=0x04DB1DC0  token=0x6000407  Unity.Mathematics.float2 sinh(Unity.Mathematics.float2 x)
  RVA=0x04DB1C60  token=0x6000408  Unity.Mathematics.float3 sinh(Unity.Mathematics.float3 x)
  RVA=0x04DB1D60  token=0x6000409  Unity.Mathematics.float4 sinh(Unity.Mathematics.float4 x)
  RVA=0x0B272BA0  token=0x600040A  System.Double sinh(System.Double x)
  RVA=0x04DB1C20  token=0x600040B  Unity.Mathematics.double2 sinh(Unity.Mathematics.double2 x)
  RVA=0x04DB1D10  token=0x600040C  Unity.Mathematics.double3 sinh(Unity.Mathematics.double3 x)
  RVA=0x04DB1CB0  token=0x600040D  Unity.Mathematics.double4 sinh(Unity.Mathematics.double4 x)
  RVA=0x0B24C428  token=0x600040E  System.Single asin(System.Single x)
  RVA=0x04DAD370  token=0x600040F  Unity.Mathematics.float2 asin(Unity.Mathematics.float2 x)
  RVA=0x04DAD410  token=0x6000410  Unity.Mathematics.float3 asin(Unity.Mathematics.float3 x)
  RVA=0x04DAD4F0  token=0x6000411  Unity.Mathematics.float4 asin(Unity.Mathematics.float4 x)
  RVA=0x0B24C3D8  token=0x6000412  System.Double asin(System.Double x)
  RVA=0x04DAD4B0  token=0x6000413  Unity.Mathematics.double2 asin(Unity.Mathematics.double2 x)
  RVA=0x04DAD460  token=0x6000414  Unity.Mathematics.double3 asin(Unity.Mathematics.double3 x)
  RVA=0x04DAD3B0  token=0x6000415  Unity.Mathematics.double4 asin(Unity.Mathematics.double4 x)
  RVA=0x0B252FA8  token=0x6000416  System.Single floor(System.Single x)
  RVA=0x04DAFBC0  token=0x6000417  Unity.Mathematics.float2 floor(Unity.Mathematics.float2 x)
  RVA=0x0411D130  token=0x6000418  Unity.Mathematics.float3 floor(Unity.Mathematics.float3 x)
  RVA=0x04DAFA70  token=0x6000419  Unity.Mathematics.float4 floor(Unity.Mathematics.float4 x)
  RVA=0x0B253000  token=0x600041A  System.Double floor(System.Double x)
  RVA=0x04DAFB80  token=0x600041B  Unity.Mathematics.double2 floor(Unity.Mathematics.double2 x)
  RVA=0x04DAFAD0  token=0x600041C  Unity.Mathematics.double3 floor(Unity.Mathematics.double3 x)
  RVA=0x04DAFB20  token=0x600041D  Unity.Mathematics.double4 floor(Unity.Mathematics.double4 x)
  RVA=0x0B24CA7C  token=0x600041E  System.Single ceil(System.Single x)
  RVA=0x04DAD930  token=0x600041F  Unity.Mathematics.float2 ceil(Unity.Mathematics.float2 x)
  RVA=0x04DADA70  token=0x6000420  Unity.Mathematics.float3 ceil(Unity.Mathematics.float3 x)
  RVA=0x04DAD9B0  token=0x6000421  Unity.Mathematics.float4 ceil(Unity.Mathematics.float4 x)
  RVA=0x0B24CAD4  token=0x6000422  System.Double ceil(System.Double x)
  RVA=0x04DAD970  token=0x6000423  Unity.Mathematics.double2 ceil(Unity.Mathematics.double2 x)
  RVA=0x04DAD8E0  token=0x6000424  Unity.Mathematics.double3 ceil(Unity.Mathematics.double3 x)
  RVA=0x04DADA10  token=0x6000425  Unity.Mathematics.double4 ceil(Unity.Mathematics.double4 x)
  RVA=0x0B270A54  token=0x6000426  System.Single round(System.Single x)
  RVA=0x0B270B94  token=0x6000427  Unity.Mathematics.float2 round(Unity.Mathematics.float2 x)
  RVA=0x05F31D10  token=0x6000428  Unity.Mathematics.float3 round(Unity.Mathematics.float3 x)
  RVA=0x05F31E7C  token=0x6000429  Unity.Mathematics.float4 round(Unity.Mathematics.float4 x)
  RVA=0x0B270A04  token=0x600042A  System.Double round(System.Double x)
  RVA=0x05F31CC8  token=0x600042B  Unity.Mathematics.double2 round(Unity.Mathematics.double2 x)
  RVA=0x0B270AAC  token=0x600042C  Unity.Mathematics.double3 round(Unity.Mathematics.double3 x)
  RVA=0x0B270B10  token=0x600042D  Unity.Mathematics.double4 round(Unity.Mathematics.double4 x)
  RVA=0x0B2752E8  token=0x600042E  System.Single trunc(System.Single x)
  RVA=0x0B2752AC  token=0x600042F  Unity.Mathematics.float2 trunc(Unity.Mathematics.float2 x)
  RVA=0x05F39770  token=0x6000430  Unity.Mathematics.float3 trunc(Unity.Mathematics.float3 x)
  RVA=0x05F39618  token=0x6000431  Unity.Mathematics.float4 trunc(Unity.Mathematics.float4 x)
  RVA=0x0B275174  token=0x6000432  System.Double trunc(System.Double x)
  RVA=0x05F39858  token=0x6000433  Unity.Mathematics.double2 trunc(Unity.Mathematics.double2 x)
  RVA=0x0B2751C4  token=0x6000434  Unity.Mathematics.double3 trunc(Unity.Mathematics.double3 x)
  RVA=0x0B275228  token=0x6000435  Unity.Mathematics.double4 trunc(Unity.Mathematics.double4 x)
  RVA=0x04DA2CA0  token=0x6000436  System.Single frac(System.Single x)
  RVA=0x0B253168  token=0x6000437  Unity.Mathematics.float2 frac(Unity.Mathematics.float2 x)
  RVA=0x05F0E4F0  token=0x6000438  Unity.Mathematics.float3 frac(Unity.Mathematics.float3 x)
  RVA=0x0B253104  token=0x6000439  Unity.Mathematics.float4 frac(Unity.Mathematics.float4 x)
  RVA=0x04DB0010  token=0x600043A  System.Double frac(System.Double x)
  RVA=0x0B25322C  token=0x600043B  Unity.Mathematics.double2 frac(Unity.Mathematics.double2 x)
  RVA=0x0B253198  token=0x600043C  Unity.Mathematics.double3 frac(Unity.Mathematics.double3 x)
  RVA=0x0B253290  token=0x600043D  Unity.Mathematics.double4 frac(Unity.Mathematics.double4 x)
  RVA=0x0B26EE6C  token=0x600043E  System.Single rcp(System.Single x)
  RVA=0x0B26ED2C  token=0x600043F  Unity.Mathematics.float2 rcp(Unity.Mathematics.float2 x)
  RVA=0x0B26ED98  token=0x6000440  Unity.Mathematics.float3 rcp(Unity.Mathematics.float3 x)
  RVA=0x0B26EE00  token=0x6000441  Unity.Mathematics.float4 rcp(Unity.Mathematics.float4 x)
  RVA=0x0B26ED1C  token=0x6000442  System.Double rcp(System.Double x)
  RVA=0x0B26EDD8  token=0x6000443  Unity.Mathematics.double2 rcp(Unity.Mathematics.double2 x)
  RVA=0x0B26ECE0  token=0x6000444  Unity.Mathematics.double3 rcp(Unity.Mathematics.double3 x)
  RVA=0x0B26ED54  token=0x6000445  Unity.Mathematics.double4 rcp(Unity.Mathematics.double4 x)
  RVA=0x0B27260C  token=0x6000446  System.Int32 sign(System.Int32 x)
  RVA=0x0B272754  token=0x6000447  Unity.Mathematics.int2 sign(Unity.Mathematics.int2 x)
  RVA=0x0B272808  token=0x6000448  Unity.Mathematics.int3 sign(Unity.Mathematics.int3 x)
  RVA=0x0B2726F4  token=0x6000449  Unity.Mathematics.int4 sign(Unity.Mathematics.int4 x)
  RVA=0x05F355F4  token=0x600044A  System.Single sign(System.Single x)
  RVA=0x0B272654  token=0x600044B  Unity.Mathematics.float2 sign(Unity.Mathematics.float2 x)
  RVA=0x0B2727D0  token=0x600044C  Unity.Mathematics.float3 sign(Unity.Mathematics.float3 x)
  RVA=0x0B2726AC  token=0x600044D  Unity.Mathematics.float4 sign(Unity.Mathematics.float4 x)
  RVA=0x05F355BC  token=0x600044E  System.Double sign(System.Double x)
  RVA=0x0B272684  token=0x600044F  Unity.Mathematics.double2 sign(Unity.Mathematics.double2 x)
  RVA=0x0B27261C  token=0x6000450  Unity.Mathematics.double3 sign(Unity.Mathematics.double3 x)
  RVA=0x0B272788  token=0x6000451  Unity.Mathematics.double4 sign(Unity.Mathematics.double4 x)
  RVA=0x0B26E22C  token=0x6000452  System.Single pow(System.Single x, System.Single y)
  RVA=0x0B26E290  token=0x6000453  Unity.Mathematics.float2 pow(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x05F2F240  token=0x6000454  Unity.Mathematics.float3 pow(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x05F2F538  token=0x6000455  Unity.Mathematics.float4 pow(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x0B26E2DC  token=0x6000456  System.Double pow(System.Double x, System.Double y)
  RVA=0x05F2F4D8  token=0x6000457  Unity.Mathematics.double2 pow(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x0B26E1AC  token=0x6000458  Unity.Mathematics.double3 pow(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B26E33C  token=0x6000459  Unity.Mathematics.double4 pow(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x0B24FD18  token=0x600045A  System.Single exp(System.Single x)
  RVA=0x04DAF330  token=0x600045B  Unity.Mathematics.float2 exp(Unity.Mathematics.float2 x)
  RVA=0x04DAF460  token=0x600045C  Unity.Mathematics.float3 exp(Unity.Mathematics.float3 x)
  RVA=0x04DAF2D0  token=0x600045D  Unity.Mathematics.float4 exp(Unity.Mathematics.float4 x)
  RVA=0x0B24FCC8  token=0x600045E  System.Double exp(System.Double x)
  RVA=0x04DAF370  token=0x600045F  Unity.Mathematics.double2 exp(Unity.Mathematics.double2 x)
  RVA=0x04DAF3B0  token=0x6000460  Unity.Mathematics.double3 exp(Unity.Mathematics.double3 x)
  RVA=0x04DAF400  token=0x6000461  Unity.Mathematics.double4 exp(Unity.Mathematics.double4 x)
  RVA=0x0B24FAEC  token=0x6000462  System.Single exp2(System.Single x)
  RVA=0x0B24FC8C  token=0x6000463  Unity.Mathematics.float2 exp2(Unity.Mathematics.float2 x)
  RVA=0x05F0BF44  token=0x6000464  Unity.Mathematics.float3 exp2(Unity.Mathematics.float3 x)
  RVA=0x05F0C1F0  token=0x6000465  Unity.Mathematics.float4 exp2(Unity.Mathematics.float4 x)
  RVA=0x0B24FB4C  token=0x6000466  System.Double exp2(System.Double x)
  RVA=0x05F0C060  token=0x6000467  Unity.Mathematics.double2 exp2(Unity.Mathematics.double2 x)
  RVA=0x0B24FBA4  token=0x6000468  Unity.Mathematics.double3 exp2(Unity.Mathematics.double3 x)
  RVA=0x0B24FC08  token=0x6000469  Unity.Mathematics.double4 exp2(Unity.Mathematics.double4 x)
  RVA=0x0B24FA8C  token=0x600046A  System.Single exp10(System.Single x)
  RVA=0x0B24F910  token=0x600046B  Unity.Mathematics.float2 exp10(Unity.Mathematics.float2 x)
  RVA=0x05F0BC14  token=0x600046C  Unity.Mathematics.float3 exp10(Unity.Mathematics.float3 x)
  RVA=0x05F0BD84  token=0x600046D  Unity.Mathematics.float4 exp10(Unity.Mathematics.float4 x)
  RVA=0x0B24F9D0  token=0x600046E  System.Double exp10(System.Double x)
  RVA=0x05F0BCB8  token=0x600046F  Unity.Mathematics.double2 exp10(Unity.Mathematics.double2 x)
  RVA=0x0B24FA28  token=0x6000470  Unity.Mathematics.double3 exp10(Unity.Mathematics.double3 x)
  RVA=0x0B24F94C  token=0x6000471  Unity.Mathematics.double4 exp10(Unity.Mathematics.double4 x)
  RVA=0x0B25DFD0  token=0x6000472  System.Single log(System.Single x)
  RVA=0x04DB0860  token=0x6000473  Unity.Mathematics.float2 log(Unity.Mathematics.float2 x)
  RVA=0x04DB08F0  token=0x6000474  Unity.Mathematics.float3 log(Unity.Mathematics.float3 x)
  RVA=0x04DB0800  token=0x6000475  Unity.Mathematics.float4 log(Unity.Mathematics.float4 x)
  RVA=0x0B25DF80  token=0x6000476  System.Double log(System.Double x)
  RVA=0x04DB07C0  token=0x6000477  Unity.Mathematics.double2 log(Unity.Mathematics.double2 x)
  RVA=0x04DB08A0  token=0x6000478  Unity.Mathematics.double3 log(Unity.Mathematics.double3 x)
  RVA=0x04DB0940  token=0x6000479  Unity.Mathematics.double4 log(Unity.Mathematics.double4 x)
  RVA=0x0B25DDA4  token=0x600047A  System.Single log2(System.Single x)
  RVA=0x0B25DE00  token=0x600047B  Unity.Mathematics.float2 log2(Unity.Mathematics.float2 x)
  RVA=0x05F17BB4  token=0x600047C  Unity.Mathematics.float3 log2(Unity.Mathematics.float3 x)
  RVA=0x05F17C18  token=0x600047D  Unity.Mathematics.float4 log2(Unity.Mathematics.float4 x)
  RVA=0x0B25DE3C  token=0x600047E  System.Double log2(System.Double x)
  RVA=0x05F17B10  token=0x600047F  Unity.Mathematics.double2 log2(Unity.Mathematics.double2 x)
  RVA=0x0B25DE98  token=0x6000480  Unity.Mathematics.double3 log2(Unity.Mathematics.double3 x)
  RVA=0x0B25DEFC  token=0x6000481  Unity.Mathematics.double4 log2(Unity.Mathematics.double4 x)
  RVA=0x0B25DCFC  token=0x6000482  System.Single log10(System.Single x)
  RVA=0x04DB0730  token=0x6000483  Unity.Mathematics.float2 log10(Unity.Mathematics.float2 x)
  RVA=0x04DB0770  token=0x6000484  Unity.Mathematics.float3 log10(Unity.Mathematics.float3 x)
  RVA=0x04DB0680  token=0x6000485  Unity.Mathematics.float4 log10(Unity.Mathematics.float4 x)
  RVA=0x0B25DD54  token=0x6000486  System.Double log10(System.Double x)
  RVA=0x04DB05E0  token=0x6000487  Unity.Mathematics.double2 log10(Unity.Mathematics.double2 x)
  RVA=0x04DB06E0  token=0x6000488  Unity.Mathematics.double3 log10(Unity.Mathematics.double3 x)
  RVA=0x04DB0620  token=0x6000489  Unity.Mathematics.double4 log10(Unity.Mathematics.double4 x)
  RVA=0x04D88690  token=0x600048A  System.Single fmod(System.Single x, System.Single y)
  RVA=0x04DAFCE0  token=0x600048B  Unity.Mathematics.float2 fmod(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x04DAFC00  token=0x600048C  Unity.Mathematics.float3 fmod(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x04DAFD30  token=0x600048D  Unity.Mathematics.float4 fmod(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x04DAFE90  token=0x600048E  System.Double fmod(System.Double x, System.Double y)
  RVA=0x04DAFE30  token=0x600048F  Unity.Mathematics.double2 fmod(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x04DAFC70  token=0x6000490  Unity.Mathematics.double3 fmod(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x04DAFDB0  token=0x6000491  Unity.Mathematics.double4 fmod(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x0B25ED5C  token=0x6000492  System.Single modf(System.Single x, System.Single& i)
  RVA=0x0B25EC14  token=0x6000493  Unity.Mathematics.float2 modf(Unity.Mathematics.float2 x, Unity.Mathematics.float2& i)
  RVA=0x05F189DC  token=0x6000494  Unity.Mathematics.float3 modf(Unity.Mathematics.float3 x, Unity.Mathematics.float3& i)
  RVA=0x0B25EB74  token=0x6000495  Unity.Mathematics.float4 modf(Unity.Mathematics.float4 x, Unity.Mathematics.float4& i)
  RVA=0x0B25EBE8  token=0x6000496  System.Double modf(System.Double x, System.Double& i)
  RVA=0x0B25ECE8  token=0x6000497  Unity.Mathematics.double2 modf(Unity.Mathematics.double2 x, Unity.Mathematics.double2& i)
  RVA=0x0B25EAD0  token=0x6000498  Unity.Mathematics.double3 modf(Unity.Mathematics.double3 x, Unity.Mathematics.double3& i)
  RVA=0x0B25EC4C  token=0x6000499  Unity.Mathematics.double4 modf(Unity.Mathematics.double4 x, Unity.Mathematics.double4& i)
  RVA=0x0B273550  token=0x600049A  System.Single sqrt(System.Single x)
  RVA=0x04DB1E00  token=0x600049B  Unity.Mathematics.float2 sqrt(Unity.Mathematics.float2 x)
  RVA=0x04DB20F0  token=0x600049C  Unity.Mathematics.float3 sqrt(Unity.Mathematics.float3 x)
  RVA=0x04DB1E70  token=0x600049D  Unity.Mathematics.float4 sqrt(Unity.Mathematics.float4 x)
  RVA=0x0B273508  token=0x600049E  System.Double sqrt(System.Double x)
  RVA=0x04DB1F30  token=0x600049F  Unity.Mathematics.double2 sqrt(Unity.Mathematics.double2 x)
  RVA=0x04DB2060  token=0x60004A0  Unity.Mathematics.double3 sqrt(Unity.Mathematics.double3 x)
  RVA=0x04DB1FA0  token=0x60004A1  Unity.Mathematics.double4 sqrt(Unity.Mathematics.double4 x)
  RVA=0x0B270CB8  token=0x60004A2  System.Single rsqrt(System.Single x)
  RVA=0x0B270D6C  token=0x60004A3  Unity.Mathematics.float2 rsqrt(Unity.Mathematics.float2 x)
  RVA=0x05F31F40  token=0x60004A4  Unity.Mathematics.float3 rsqrt(Unity.Mathematics.float3 x)
  RVA=0x0B270CE8  token=0x60004A5  Unity.Mathematics.float4 rsqrt(Unity.Mathematics.float4 x)
  RVA=0x0B270DA0  token=0x60004A6  System.Double rsqrt(System.Double x)
  RVA=0x0B270DD0  token=0x60004A7  Unity.Mathematics.double2 rsqrt(Unity.Mathematics.double2 x)
  RVA=0x0B270BD0  token=0x60004A8  Unity.Mathematics.double3 rsqrt(Unity.Mathematics.double3 x)
  RVA=0x0B270C48  token=0x60004A9  Unity.Mathematics.double4 rsqrt(Unity.Mathematics.double4 x)
  RVA=0x0B26D990  token=0x60004AA  Unity.Mathematics.float2 normalize(Unity.Mathematics.float2 x)
  RVA=0x0B26D9F8  token=0x60004AB  Unity.Mathematics.float3 normalize(Unity.Mathematics.float3 x)
  RVA=0x0B26D8B8  token=0x60004AC  Unity.Mathematics.float4 normalize(Unity.Mathematics.float4 x)
  RVA=0x05F2ED38  token=0x60004AD  Unity.Mathematics.double2 normalize(Unity.Mathematics.double2 x)
  RVA=0x0B26D838  token=0x60004AE  Unity.Mathematics.double3 normalize(Unity.Mathematics.double3 x)
  RVA=0x0B26D914  token=0x60004AF  Unity.Mathematics.double4 normalize(Unity.Mathematics.double4 x)
  RVA=0x0B26DB1C  token=0x60004B0  Unity.Mathematics.float2 normalizesafe(Unity.Mathematics.float2 x, Unity.Mathematics.float2 defaultvalue)
  RVA=0x05F2F020  token=0x60004B1  Unity.Mathematics.float3 normalizesafe(Unity.Mathematics.float3 x, Unity.Mathematics.float3 defaultvalue)
  RVA=0x0B26DBA4  token=0x60004B2  Unity.Mathematics.float4 normalizesafe(Unity.Mathematics.float4 x, Unity.Mathematics.float4 defaultvalue)
  RVA=0x05F2F0C0  token=0x60004B3  Unity.Mathematics.double2 normalizesafe(Unity.Mathematics.double2 x, Unity.Mathematics.double2 defaultvalue)
  RVA=0x0B26DCF0  token=0x60004B4  Unity.Mathematics.double3 normalizesafe(Unity.Mathematics.double3 x, Unity.Mathematics.double3 defaultvalue)
  RVA=0x0B26DA6C  token=0x60004B5  Unity.Mathematics.double4 normalizesafe(Unity.Mathematics.double4 x, Unity.Mathematics.double4 defaultvalue)
  RVA=0x04DAD080  token=0x60004B6  System.Single length(System.Single x)
  RVA=0x04D8B6B0  token=0x60004B7  System.Single length(Unity.Mathematics.float2 x)
  RVA=0x0B25D5CC  token=0x60004B8  System.Single length(Unity.Mathematics.float3 x)
  RVA=0x0B25D670  token=0x60004B9  System.Single length(Unity.Mathematics.float4 x)
  RVA=0x04DAD120  token=0x60004BA  System.Double length(System.Double x)
  RVA=0x04DB0560  token=0x60004BB  System.Double length(Unity.Mathematics.double2 x)
  RVA=0x0B25D638  token=0x60004BC  System.Double length(Unity.Mathematics.double3 x)
  RVA=0x0B25D604  token=0x60004BD  System.Double length(Unity.Mathematics.double4 x)
  RVA=0x04D886E0  token=0x60004BE  System.Single lengthsq(System.Single x)
  RVA=0x04D8B690  token=0x60004BF  System.Single lengthsq(Unity.Mathematics.float2 x)
  RVA=0x0B25D70C  token=0x60004C0  System.Single lengthsq(Unity.Mathematics.float3 x)
  RVA=0x0B25D6AC  token=0x60004C1  System.Single lengthsq(Unity.Mathematics.float4 x)
  RVA=0x04DB05C0  token=0x60004C2  System.Double lengthsq(System.Double x)
  RVA=0x04DB05A0  token=0x60004C3  System.Double lengthsq(Unity.Mathematics.double2 x)
  RVA=0x0B25D6D4  token=0x60004C4  System.Double lengthsq(Unity.Mathematics.double3 x)
  RVA=0x0B25D740  token=0x60004C5  System.Double lengthsq(Unity.Mathematics.double4 x)
  RVA=0x04DAEA10  token=0x60004C6  System.Single distance(System.Single x, System.Single y)
  RVA=0x0B24E498  token=0x60004C7  System.Single distance(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x0B24E324  token=0x60004C8  System.Single distance(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x0B24E3F8  token=0x60004C9  System.Single distance(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x04DAEA30  token=0x60004CA  System.Double distance(System.Double x, System.Double y)
  RVA=0x0B24E458  token=0x60004CB  System.Double distance(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x0B24E2B0  token=0x60004CC  System.Double distance(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B24E38C  token=0x60004CD  System.Double distance(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x04DAEA50  token=0x60004CE  System.Single distancesq(System.Single x, System.Single y)
  RVA=0x0B24E4D4  token=0x60004CF  System.Single distancesq(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x0B24E630  token=0x60004D0  System.Single distancesq(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x0B24E50C  token=0x60004D1  System.Single distancesq(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x04DAEA60  token=0x60004D2  System.Double distancesq(System.Double x, System.Double y)
  RVA=0x0B24E694  token=0x60004D3  System.Double distancesq(Unity.Mathematics.double2 x, Unity.Mathematics.double2 y)
  RVA=0x0B24E5C0  token=0x60004D4  System.Double distancesq(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B24E558  token=0x60004D5  System.Double distancesq(Unity.Mathematics.double4 x, Unity.Mathematics.double4 y)
  RVA=0x0B24DE8C  token=0x60004D6  Unity.Mathematics.float3 cross(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x0B24DF74  token=0x60004D7  Unity.Mathematics.double3 cross(Unity.Mathematics.double3 x, Unity.Mathematics.double3 y)
  RVA=0x0B273258  token=0x60004D8  System.Single smoothstep(System.Single xMin, System.Single xMax, System.Single x)
  RVA=0x0B2732A4  token=0x60004D9  Unity.Mathematics.float2 smoothstep(Unity.Mathematics.float2 xMin, Unity.Mathematics.float2 xMax, Unity.Mathematics.float2 x)
  RVA=0x05F35CBC  token=0x60004DA  Unity.Mathematics.float3 smoothstep(Unity.Mathematics.float3 xMin, Unity.Mathematics.float3 xMax, Unity.Mathematics.float3 x)
  RVA=0x0B2733C0  token=0x60004DB  Unity.Mathematics.float4 smoothstep(Unity.Mathematics.float4 xMin, Unity.Mathematics.float4 xMax, Unity.Mathematics.float4 x)
  RVA=0x0B273374  token=0x60004DC  System.Double smoothstep(System.Double xMin, System.Double xMax, System.Double x)
  RVA=0x0B272DE8  token=0x60004DD  Unity.Mathematics.double2 smoothstep(Unity.Mathematics.double2 xMin, Unity.Mathematics.double2 xMax, Unity.Mathematics.double2 x)
  RVA=0x0B273098  token=0x60004DE  Unity.Mathematics.double3 smoothstep(Unity.Mathematics.double3 xMin, Unity.Mathematics.double3 xMax, Unity.Mathematics.double3 x)
  RVA=0x0B272EF8  token=0x60004DF  Unity.Mathematics.double4 smoothstep(Unity.Mathematics.double4 xMin, Unity.Mathematics.double4 xMax, Unity.Mathematics.double4 x)
  RVA=0x0B24C25C  token=0x60004E0  System.Boolean any(Unity.Mathematics.bool2 x)
  RVA=0x0B24C318  token=0x60004E1  System.Boolean any(Unity.Mathematics.bool3 x)
  RVA=0x05F08324  token=0x60004E2  System.Boolean any(Unity.Mathematics.bool4 x)
  RVA=0x0B24C248  token=0x60004E3  System.Boolean any(Unity.Mathematics.int2 x)
  RVA=0x0B24C26C  token=0x60004E4  System.Boolean any(Unity.Mathematics.int3 x)
  RVA=0x0B24C22C  token=0x60004E5  System.Boolean any(Unity.Mathematics.int4 x)
  RVA=0x0B24C248  token=0x60004E6  System.Boolean any(Unity.Mathematics.uint2 x)
  RVA=0x0B24C26C  token=0x60004E7  System.Boolean any(Unity.Mathematics.uint3 x)
  RVA=0x0B24C22C  token=0x60004E8  System.Boolean any(Unity.Mathematics.uint4 x)
  RVA=0x0B24C32C  token=0x60004E9  System.Boolean any(Unity.Mathematics.float2 x)
  RVA=0x0B24C2A8  token=0x60004EA  System.Boolean any(Unity.Mathematics.float3 x)
  RVA=0x0B24C354  token=0x60004EB  System.Boolean any(Unity.Mathematics.float4 x)
  RVA=0x0B24C284  token=0x60004EC  System.Boolean any(Unity.Mathematics.double2 x)
  RVA=0x0B24C38C  token=0x60004ED  System.Boolean any(Unity.Mathematics.double3 x)
  RVA=0x0B24C2D4  token=0x60004EE  System.Boolean any(Unity.Mathematics.double4 x)
  RVA=0x0B24C01C  token=0x60004EF  System.Boolean all(Unity.Mathematics.bool2 x)
  RVA=0x0B24C14C  token=0x60004F0  System.Boolean all(Unity.Mathematics.bool3 x)
  RVA=0x05F08180  token=0x60004F1  System.Boolean all(Unity.Mathematics.bool4 x)
  RVA=0x0B24C02C  token=0x60004F2  System.Boolean all(Unity.Mathematics.int2 x)
  RVA=0x05F081A0  token=0x60004F3  System.Boolean all(Unity.Mathematics.int3 x)
  RVA=0x0B24C0D0  token=0x60004F4  System.Boolean all(Unity.Mathematics.int4 x)
  RVA=0x0B24C02C  token=0x60004F5  System.Boolean all(Unity.Mathematics.uint2 x)
  RVA=0x05F081A0  token=0x60004F6  System.Boolean all(Unity.Mathematics.uint3 x)
  RVA=0x0B24C0D0  token=0x60004F7  System.Boolean all(Unity.Mathematics.uint4 x)
  RVA=0x0B24C0EC  token=0x60004F8  System.Boolean all(Unity.Mathematics.float2 x)
  RVA=0x05F081B8  token=0x60004F9  System.Boolean all(Unity.Mathematics.float3 x)
  RVA=0x0B24C114  token=0x60004FA  System.Boolean all(Unity.Mathematics.float4 x)
  RVA=0x0B24C160  token=0x60004FB  System.Boolean all(Unity.Mathematics.double2 x)
  RVA=0x0B24C084  token=0x60004FC  System.Boolean all(Unity.Mathematics.double3 x)
  RVA=0x0B24C040  token=0x60004FD  System.Boolean all(Unity.Mathematics.double4 x)
  RVA=0x0B27110C  token=0x60004FE  System.Int32 select(System.Int32 falseValue, System.Int32 trueValue, System.Boolean test)
  RVA=0x0B271124  token=0x60004FF  Unity.Mathematics.int2 select(Unity.Mathematics.int2 falseValue, Unity.Mathematics.int2 trueValue, System.Boolean test)
  RVA=0x0B271148  token=0x6000500  Unity.Mathematics.int3 select(Unity.Mathematics.int3 falseValue, Unity.Mathematics.int3 trueValue, System.Boolean test)
  RVA=0x0B271130  token=0x6000501  Unity.Mathematics.int4 select(Unity.Mathematics.int4 falseValue, Unity.Mathematics.int4 trueValue, System.Boolean test)
  RVA=0x0B27116C  token=0x6000502  Unity.Mathematics.int2 select(Unity.Mathematics.int2 falseValue, Unity.Mathematics.int2 trueValue, Unity.Mathematics.bool2 test)
  RVA=0x05F324D0  token=0x6000503  Unity.Mathematics.int3 select(Unity.Mathematics.int3 falseValue, Unity.Mathematics.int3 trueValue, Unity.Mathematics.bool3 test)
  RVA=0x05F32634  token=0x6000504  Unity.Mathematics.int4 select(Unity.Mathematics.int4 falseValue, Unity.Mathematics.int4 trueValue, Unity.Mathematics.bool4 test)
  RVA=0x0B27110C  token=0x6000505  System.UInt32 select(System.UInt32 falseValue, System.UInt32 trueValue, System.Boolean test)
  RVA=0x0B271124  token=0x6000506  Unity.Mathematics.uint2 select(Unity.Mathematics.uint2 falseValue, Unity.Mathematics.uint2 trueValue, System.Boolean test)
  RVA=0x0B271148  token=0x6000507  Unity.Mathematics.uint3 select(Unity.Mathematics.uint3 falseValue, Unity.Mathematics.uint3 trueValue, System.Boolean test)
  RVA=0x0B271130  token=0x6000508  Unity.Mathematics.uint4 select(Unity.Mathematics.uint4 falseValue, Unity.Mathematics.uint4 trueValue, System.Boolean test)
  RVA=0x0B27116C  token=0x6000509  Unity.Mathematics.uint2 select(Unity.Mathematics.uint2 falseValue, Unity.Mathematics.uint2 trueValue, Unity.Mathematics.bool2 test)
  RVA=0x05F324D0  token=0x600050A  Unity.Mathematics.uint3 select(Unity.Mathematics.uint3 falseValue, Unity.Mathematics.uint3 trueValue, Unity.Mathematics.bool3 test)
  RVA=0x05F32634  token=0x600050B  Unity.Mathematics.uint4 select(Unity.Mathematics.uint4 falseValue, Unity.Mathematics.uint4 trueValue, Unity.Mathematics.bool4 test)
  RVA=0x0B271100  token=0x600050C  System.Int64 select(System.Int64 falseValue, System.Int64 trueValue, System.Boolean test)
  RVA=0x0B271100  token=0x600050D  System.UInt64 select(System.UInt64 falseValue, System.UInt64 trueValue, System.Boolean test)
  RVA=0x0B271118  token=0x600050E  System.Single select(System.Single falseValue, System.Single trueValue, System.Boolean test)
  RVA=0x0B271124  token=0x600050F  Unity.Mathematics.float2 select(Unity.Mathematics.float2 falseValue, Unity.Mathematics.float2 trueValue, System.Boolean test)
  RVA=0x0B271148  token=0x6000510  Unity.Mathematics.float3 select(Unity.Mathematics.float3 falseValue, Unity.Mathematics.float3 trueValue, System.Boolean test)
  RVA=0x0B271130  token=0x6000511  Unity.Mathematics.float4 select(Unity.Mathematics.float4 falseValue, Unity.Mathematics.float4 trueValue, System.Boolean test)
  RVA=0x05F32510  token=0x6000512  Unity.Mathematics.float2 select(Unity.Mathematics.float2 falseValue, Unity.Mathematics.float2 trueValue, Unity.Mathematics.bool2 test)
  RVA=0x05F32554  token=0x6000513  Unity.Mathematics.float3 select(Unity.Mathematics.float3 falseValue, Unity.Mathematics.float3 trueValue, Unity.Mathematics.bool3 test)
  RVA=0x05F325C8  token=0x6000514  Unity.Mathematics.float4 select(Unity.Mathematics.float4 falseValue, Unity.Mathematics.float4 trueValue, Unity.Mathematics.bool4 test)
  RVA=0x0B271118  token=0x6000515  System.Double select(System.Double falseValue, System.Double trueValue, System.Boolean test)
  RVA=0x0B271130  token=0x6000516  Unity.Mathematics.double2 select(Unity.Mathematics.double2 falseValue, Unity.Mathematics.double2 trueValue, System.Boolean test)
  RVA=0x0B271290  token=0x6000517  Unity.Mathematics.double3 select(Unity.Mathematics.double3 falseValue, Unity.Mathematics.double3 trueValue, System.Boolean test)
  RVA=0x0B2712B8  token=0x6000518  Unity.Mathematics.double4 select(Unity.Mathematics.double4 falseValue, Unity.Mathematics.double4 trueValue, System.Boolean test)
  RVA=0x05F3249C  token=0x6000519  Unity.Mathematics.double2 select(Unity.Mathematics.double2 falseValue, Unity.Mathematics.double2 trueValue, Unity.Mathematics.bool2 test)
  RVA=0x0B271210  token=0x600051A  Unity.Mathematics.double3 select(Unity.Mathematics.double3 falseValue, Unity.Mathematics.double3 trueValue, Unity.Mathematics.bool3 test)
  RVA=0x0B27119C  token=0x600051B  Unity.Mathematics.double4 select(Unity.Mathematics.double4 falseValue, Unity.Mathematics.double4 trueValue, Unity.Mathematics.bool4 test)
  RVA=0x0B273700  token=0x600051C  System.Single step(System.Single threshold, System.Single x)
  RVA=0x0B273A6C  token=0x600051D  Unity.Mathematics.float2 step(Unity.Mathematics.float2 threshold, Unity.Mathematics.float2 x)
  RVA=0x0B27391C  token=0x600051E  Unity.Mathematics.float3 step(Unity.Mathematics.float3 threshold, Unity.Mathematics.float3 x)
  RVA=0x0B273728  token=0x600051F  Unity.Mathematics.float4 step(Unity.Mathematics.float4 threshold, Unity.Mathematics.float4 x)
  RVA=0x0B273714  token=0x6000520  System.Double step(System.Double threshold, System.Double x)
  RVA=0x0B2738A8  token=0x6000521  Unity.Mathematics.double2 step(Unity.Mathematics.double2 threshold, Unity.Mathematics.double2 x)
  RVA=0x0B2737E4  token=0x6000522  Unity.Mathematics.double3 step(Unity.Mathematics.double3 threshold, Unity.Mathematics.double3 x)
  RVA=0x0B2739C8  token=0x6000523  Unity.Mathematics.double4 step(Unity.Mathematics.double4 threshold, Unity.Mathematics.double4 x)
  RVA=0x0B26EE7C  token=0x6000524  Unity.Mathematics.float2 reflect(Unity.Mathematics.float2 i, Unity.Mathematics.float2 n)
  RVA=0x05F301DC  token=0x6000525  Unity.Mathematics.float3 reflect(Unity.Mathematics.float3 i, Unity.Mathematics.float3 n)
  RVA=0x0B26F128  token=0x6000526  Unity.Mathematics.float4 reflect(Unity.Mathematics.float4 i, Unity.Mathematics.float4 n)
  RVA=0x0B26F090  token=0x6000527  Unity.Mathematics.double2 reflect(Unity.Mathematics.double2 i, Unity.Mathematics.double2 n)
  RVA=0x0B26EFAC  token=0x6000528  Unity.Mathematics.double3 reflect(Unity.Mathematics.double3 i, Unity.Mathematics.double3 n)
  RVA=0x0B26EED4  token=0x6000529  Unity.Mathematics.double4 reflect(Unity.Mathematics.double4 i, Unity.Mathematics.double4 n)
  RVA=0x0B26F314  token=0x600052A  Unity.Mathematics.float2 refract(Unity.Mathematics.float2 i, Unity.Mathematics.float2 n, System.Single indexOfRefraction)
  RVA=0x05F306B0  token=0x600052B  Unity.Mathematics.float3 refract(Unity.Mathematics.float3 i, Unity.Mathematics.float3 n, System.Single indexOfRefraction)
  RVA=0x0B26F6DC  token=0x600052C  Unity.Mathematics.float4 refract(Unity.Mathematics.float4 i, Unity.Mathematics.float4 n, System.Single indexOfRefraction)
  RVA=0x0B26F418  token=0x600052D  Unity.Mathematics.double2 refract(Unity.Mathematics.double2 i, Unity.Mathematics.double2 n, System.Double indexOfRefraction)
  RVA=0x0B26F52C  token=0x600052E  Unity.Mathematics.double3 refract(Unity.Mathematics.double3 i, Unity.Mathematics.double3 n, System.Double indexOfRefraction)
  RVA=0x0B26F1BC  token=0x600052F  Unity.Mathematics.double4 refract(Unity.Mathematics.double4 i, Unity.Mathematics.double4 n, System.Double indexOfRefraction)
  RVA=0x0B26E3E0  token=0x6000530  Unity.Mathematics.float2 project(Unity.Mathematics.float2 a, Unity.Mathematics.float2 ontoB)
  RVA=0x05F2F6F0  token=0x6000531  Unity.Mathematics.float3 project(Unity.Mathematics.float3 a, Unity.Mathematics.float3 ontoB)
  RVA=0x0B26E578  token=0x6000532  Unity.Mathematics.float4 project(Unity.Mathematics.float4 a, Unity.Mathematics.float4 ontoB)
  RVA=0x0B26E650  token=0x6000533  Unity.Mathematics.float2 projectsafe(Unity.Mathematics.float2 a, Unity.Mathematics.float2 ontoB, Unity.Mathematics.float2 defaultValue)
  RVA=0x05F2F9B0  token=0x6000534  Unity.Mathematics.float3 projectsafe(Unity.Mathematics.float3 a, Unity.Mathematics.float3 ontoB, Unity.Mathematics.float3 defaultValue)
  RVA=0x0B26E5E8  token=0x6000535  Unity.Mathematics.float4 projectsafe(Unity.Mathematics.float4 a, Unity.Mathematics.float4 ontoB, Unity.Mathematics.float4 defaultValue)
  RVA=0x05F2F67C  token=0x6000536  Unity.Mathematics.double2 project(Unity.Mathematics.double2 a, Unity.Mathematics.double2 ontoB)
  RVA=0x0B26E4CC  token=0x6000537  Unity.Mathematics.double3 project(Unity.Mathematics.double3 a, Unity.Mathematics.double3 ontoB)
  RVA=0x0B26E42C  token=0x6000538  Unity.Mathematics.double4 project(Unity.Mathematics.double4 a, Unity.Mathematics.double4 ontoB)
  RVA=0x0B26E78C  token=0x6000539  Unity.Mathematics.double2 projectsafe(Unity.Mathematics.double2 a, Unity.Mathematics.double2 ontoB, Unity.Mathematics.double2 defaultValue)
  RVA=0x0B26E7F0  token=0x600053A  Unity.Mathematics.double3 projectsafe(Unity.Mathematics.double3 a, Unity.Mathematics.double3 ontoB, Unity.Mathematics.double3 defaultValue)
  RVA=0x0B26E6F0  token=0x600053B  Unity.Mathematics.double4 projectsafe(Unity.Mathematics.double4 a, Unity.Mathematics.double4 ontoB, Unity.Mathematics.double4 defaultValue)
  RVA=0x0B250B80  token=0x600053C  Unity.Mathematics.float2 faceforward(Unity.Mathematics.float2 n, Unity.Mathematics.float2 i, Unity.Mathematics.float2 ng)
  RVA=0x05F0D468  token=0x600053D  Unity.Mathematics.float3 faceforward(Unity.Mathematics.float3 n, Unity.Mathematics.float3 i, Unity.Mathematics.float3 ng)
  RVA=0x0B250AA4  token=0x600053E  Unity.Mathematics.float4 faceforward(Unity.Mathematics.float4 n, Unity.Mathematics.float4 i, Unity.Mathematics.float4 ng)
  RVA=0x0B250B0C  token=0x600053F  Unity.Mathematics.double2 faceforward(Unity.Mathematics.double2 n, Unity.Mathematics.double2 i, Unity.Mathematics.double2 ng)
  RVA=0x0B250BD8  token=0x6000540  Unity.Mathematics.double3 faceforward(Unity.Mathematics.double3 n, Unity.Mathematics.double3 i, Unity.Mathematics.double3 ng)
  RVA=0x0B250C9C  token=0x6000541  Unity.Mathematics.double4 faceforward(Unity.Mathematics.double4 n, Unity.Mathematics.double4 i, Unity.Mathematics.double4 ng)
  RVA=0x04DB1BE0  token=0x6000542  System.Void sincos(System.Single x, System.Single& s, System.Single& c)
  RVA=0x0B272B5C  token=0x6000543  System.Void sincos(Unity.Mathematics.float2 x, Unity.Mathematics.float2& s, Unity.Mathematics.float2& c)
  RVA=0x0B27297C  token=0x6000544  System.Void sincos(Unity.Mathematics.float3 x, Unity.Mathematics.float3& s, Unity.Mathematics.float3& c)
  RVA=0x0B272A00  token=0x6000545  System.Void sincos(Unity.Mathematics.float4 x, Unity.Mathematics.float4& s, Unity.Mathematics.float4& c)
  RVA=0x04DB1BA0  token=0x6000546  System.Void sincos(System.Double x, System.Double& s, System.Double& c)
  RVA=0x0B272A68  token=0x6000547  System.Void sincos(Unity.Mathematics.double2 x, Unity.Mathematics.double2& s, Unity.Mathematics.double2& c)
  RVA=0x0B272AD0  token=0x6000548  System.Void sincos(Unity.Mathematics.double3 x, Unity.Mathematics.double3& s, Unity.Mathematics.double3& c)
  RVA=0x0B2728F8  token=0x6000549  System.Void sincos(Unity.Mathematics.double4 x, Unity.Mathematics.double4& s, Unity.Mathematics.double4& c)
  RVA=0x04DADFA0  token=0x600054A  System.Int32 countbits(System.Int32 x)
  RVA=0x0B24DB28  token=0x600054B  Unity.Mathematics.int2 countbits(Unity.Mathematics.int2 x)
  RVA=0x0B24DE2C  token=0x600054C  Unity.Mathematics.int3 countbits(Unity.Mathematics.int3 x)
  RVA=0x0B24DDCC  token=0x600054D  Unity.Mathematics.int4 countbits(Unity.Mathematics.int4 x)
  RVA=0x04DADFA0  token=0x600054E  System.Int32 countbits(System.UInt32 x)
  RVA=0x0B24DB40  token=0x600054F  Unity.Mathematics.int2 countbits(Unity.Mathematics.uint2 x)
  RVA=0x05F0A204  token=0x6000550  Unity.Mathematics.int3 countbits(Unity.Mathematics.uint3 x)
  RVA=0x0B24DC3C  token=0x6000551  Unity.Mathematics.int4 countbits(Unity.Mathematics.uint4 x)
  RVA=0x04DADF40  token=0x6000552  System.Int32 countbits(System.UInt64 x)
  RVA=0x04DADF40  token=0x6000553  System.Int32 countbits(System.Int64 x)
  RVA=0x0B25E12C  token=0x6000554  System.Int32 lzcnt(System.Int32 x)
  RVA=0x0B25E0F8  token=0x6000555  Unity.Mathematics.int2 lzcnt(Unity.Mathematics.int2 x)
  RVA=0x0B25E028  token=0x6000556  Unity.Mathematics.int3 lzcnt(Unity.Mathematics.int3 x)
  RVA=0x0B25E0AC  token=0x6000557  Unity.Mathematics.int4 lzcnt(Unity.Mathematics.int4 x)
  RVA=0x0B25E068  token=0x6000558  System.Int32 lzcnt(System.UInt32 x)
  RVA=0x0B25E0F8  token=0x6000559  Unity.Mathematics.int2 lzcnt(Unity.Mathematics.uint2 x)
  RVA=0x0B25E028  token=0x600055A  Unity.Mathematics.int3 lzcnt(Unity.Mathematics.uint3 x)
  RVA=0x0B25E0AC  token=0x600055B  Unity.Mathematics.int4 lzcnt(Unity.Mathematics.uint4 x)
  RVA=0x0B25E124  token=0x600055C  System.Int32 lzcnt(System.Int64 x)
  RVA=0x0B25E134  token=0x600055D  System.Int32 lzcnt(System.UInt64 x)
  RVA=0x0B275450  token=0x600055E  System.Int32 tzcnt(System.Int32 x)
  RVA=0x0B2753D0  token=0x600055F  Unity.Mathematics.int2 tzcnt(Unity.Mathematics.int2 x)
  RVA=0x0B27538C  token=0x6000560  Unity.Mathematics.int3 tzcnt(Unity.Mathematics.int3 x)
  RVA=0x0B2753FC  token=0x6000561  Unity.Mathematics.int4 tzcnt(Unity.Mathematics.int4 x)
  RVA=0x0B275344  token=0x6000562  System.Int32 tzcnt(System.UInt32 x)
  RVA=0x0B2753D0  token=0x6000563  Unity.Mathematics.int2 tzcnt(Unity.Mathematics.uint2 x)
  RVA=0x0B27538C  token=0x6000564  Unity.Mathematics.int3 tzcnt(Unity.Mathematics.uint3 x)
  RVA=0x0B2753FC  token=0x6000565  Unity.Mathematics.int4 tzcnt(Unity.Mathematics.uint4 x)
  RVA=0x0B275458  token=0x6000566  System.Int32 tzcnt(System.Int64 x)
  RVA=0x0B275460  token=0x6000567  System.Int32 tzcnt(System.UInt64 x)
  RVA=0x0B270078  token=0x6000568  System.Int32 reversebits(System.Int32 x)
  RVA=0x0B270080  token=0x6000569  Unity.Mathematics.int2 reversebits(Unity.Mathematics.int2 x)
  RVA=0x0B2700B0  token=0x600056A  Unity.Mathematics.int3 reversebits(Unity.Mathematics.int3 x)
  RVA=0x0B26FE50  token=0x600056B  Unity.Mathematics.int4 reversebits(Unity.Mathematics.int4 x)
  RVA=0x04DB1910  token=0x600056C  System.UInt32 reversebits(System.UInt32 x)
  RVA=0x0B26FEC4  token=0x600056D  Unity.Mathematics.uint2 reversebits(Unity.Mathematics.uint2 x)
  RVA=0x05F3111C  token=0x600056E  Unity.Mathematics.uint3 reversebits(Unity.Mathematics.uint3 x)
  RVA=0x0B26FB98  token=0x600056F  Unity.Mathematics.uint4 reversebits(Unity.Mathematics.uint4 x)
  RVA=0x0B270070  token=0x6000570  System.Int64 reversebits(System.Int64 x)
  RVA=0x04DB1950  token=0x6000571  System.UInt64 reversebits(System.UInt64 x)
  RVA=0x04D882E0  token=0x6000572  System.Int32 rol(System.Int32 x, System.Int32 n)
  RVA=0x0B2702D0  token=0x6000573  Unity.Mathematics.int2 rol(Unity.Mathematics.int2 x, System.Int32 n)
  RVA=0x0B270164  token=0x6000574  Unity.Mathematics.int3 rol(Unity.Mathematics.int3 x, System.Int32 n)
  RVA=0x0B2701E4  token=0x6000575  Unity.Mathematics.int4 rol(Unity.Mathematics.int4 x, System.Int32 n)
  RVA=0x04D882E0  token=0x6000576  System.UInt32 rol(System.UInt32 x, System.Int32 n)
  RVA=0x0B270130  token=0x6000577  Unity.Mathematics.uint2 rol(Unity.Mathematics.uint2 x, System.Int32 n)
  RVA=0x05F316C8  token=0x6000578  Unity.Mathematics.uint3 rol(Unity.Mathematics.uint3 x, System.Int32 n)
  RVA=0x0B270258  token=0x6000579  Unity.Mathematics.uint4 rol(Unity.Mathematics.uint4 x, System.Int32 n)
  RVA=0x04DAB5C0  token=0x600057A  System.Int64 rol(System.Int64 x, System.Int32 n)
  RVA=0x04DAB5C0  token=0x600057B  System.UInt64 rol(System.UInt64 x, System.Int32 n)
  RVA=0x04DA4210  token=0x600057C  System.Int32 ror(System.Int32 x, System.Int32 n)
  RVA=0x0B2703E8  token=0x600057D  Unity.Mathematics.int2 ror(Unity.Mathematics.int2 x, System.Int32 n)
  RVA=0x0B270458  token=0x600057E  Unity.Mathematics.int3 ror(Unity.Mathematics.int3 x, System.Int32 n)
  RVA=0x0B270374  token=0x600057F  Unity.Mathematics.int4 ror(Unity.Mathematics.int4 x, System.Int32 n)
  RVA=0x04DA4210  token=0x6000580  System.UInt32 ror(System.UInt32 x, System.Int32 n)
  RVA=0x0B270340  token=0x6000581  Unity.Mathematics.uint2 ror(Unity.Mathematics.uint2 x, System.Int32 n)
  RVA=0x05F3175C  token=0x6000582  Unity.Mathematics.uint3 ror(Unity.Mathematics.uint3 x, System.Int32 n)
  RVA=0x0B2704D8  token=0x6000583  Unity.Mathematics.uint4 ror(Unity.Mathematics.uint4 x, System.Int32 n)
  RVA=0x04DA4260  token=0x6000584  System.Int64 ror(System.Int64 x, System.Int32 n)
  RVA=0x04DA4260  token=0x6000585  System.UInt64 ror(System.UInt64 x, System.Int32 n)
  RVA=0x037F6550  token=0x6000586  System.Int32 ceilpow2(System.Int32 x)
  RVA=0x0B24CBD0  token=0x6000587  Unity.Mathematics.int2 ceilpow2(Unity.Mathematics.int2 x)
  RVA=0x05F09228  token=0x6000588  Unity.Mathematics.int3 ceilpow2(Unity.Mathematics.int3 x)
  RVA=0x0B24CED0  token=0x6000589  Unity.Mathematics.int4 ceilpow2(Unity.Mathematics.int4 x)
  RVA=0x04D89420  token=0x600058A  System.UInt32 ceilpow2(System.UInt32 x)
  RVA=0x0B24CE18  token=0x600058B  Unity.Mathematics.uint2 ceilpow2(Unity.Mathematics.uint2 x)
  RVA=0x05F09014  token=0x600058C  Unity.Mathematics.uint3 ceilpow2(Unity.Mathematics.uint3 x)
  RVA=0x0B24CC88  token=0x600058D  Unity.Mathematics.uint4 ceilpow2(Unity.Mathematics.uint4 x)
  RVA=0x04DADB10  token=0x600058E  System.Int64 ceilpow2(System.Int64 x)
  RVA=0x04DADAC0  token=0x600058F  System.UInt64 ceilpow2(System.UInt64 x)
  RVA=0x05F08FF8  token=0x6000590  System.Int32 ceillog2(System.Int32 x)
  RVA=0x0B24CBA4  token=0x6000591  Unity.Mathematics.int2 ceillog2(Unity.Mathematics.int2 x)
  RVA=0x0B24CB24  token=0x6000592  Unity.Mathematics.int3 ceillog2(Unity.Mathematics.int3 x)
  RVA=0x0B24CB5C  token=0x6000593  Unity.Mathematics.int4 ceillog2(Unity.Mathematics.int4 x)
  RVA=0x05F08FF8  token=0x6000594  System.Int32 ceillog2(System.UInt32 x)
  RVA=0x0B24CBA4  token=0x6000595  Unity.Mathematics.int2 ceillog2(Unity.Mathematics.uint2 x)
  RVA=0x0B24CB24  token=0x6000596  Unity.Mathematics.int3 ceillog2(Unity.Mathematics.uint3 x)
  RVA=0x0B24CB5C  token=0x6000597  Unity.Mathematics.int4 ceillog2(Unity.Mathematics.uint4 x)
  RVA=0x05F0E0A0  token=0x6000598  System.Int32 floorlog2(System.Int32 x)
  RVA=0x0B2530D0  token=0x6000599  Unity.Mathematics.int2 floorlog2(Unity.Mathematics.int2 x)
  RVA=0x0B253098  token=0x600059A  Unity.Mathematics.int3 floorlog2(Unity.Mathematics.int3 x)
  RVA=0x0B253050  token=0x600059B  Unity.Mathematics.int4 floorlog2(Unity.Mathematics.int4 x)
  RVA=0x05F0E0A0  token=0x600059C  System.Int32 floorlog2(System.UInt32 x)
  RVA=0x0B2530D0  token=0x600059D  Unity.Mathematics.int2 floorlog2(Unity.Mathematics.uint2 x)
  RVA=0x0B253098  token=0x600059E  Unity.Mathematics.int3 floorlog2(Unity.Mathematics.uint3 x)
  RVA=0x0B253050  token=0x600059F  Unity.Mathematics.int4 floorlog2(Unity.Mathematics.uint4 x)
  RVA=0x04DB18F0  token=0x60005A0  System.Single radians(System.Single x)
  RVA=0x0B26EC04  token=0x60005A1  Unity.Mathematics.float2 radians(Unity.Mathematics.float2 x)
  RVA=0x05390E8C  token=0x60005A2  Unity.Mathematics.float3 radians(Unity.Mathematics.float3 x)
  RVA=0x0B26EBCC  token=0x60005A3  Unity.Mathematics.float4 radians(Unity.Mathematics.float4 x)
  RVA=0x04DB1900  token=0x60005A4  System.Double radians(System.Double x)
  RVA=0x0B26EC14  token=0x60005A5  Unity.Mathematics.double2 radians(Unity.Mathematics.double2 x)
  RVA=0x0B26EC4C  token=0x60005A6  Unity.Mathematics.double3 radians(Unity.Mathematics.double3 x)
  RVA=0x0B26EC98  token=0x60005A7  Unity.Mathematics.double4 radians(Unity.Mathematics.double4 x)
  RVA=0x04DAE090  token=0x60005A8  System.Single degrees(System.Single x)
  RVA=0x0B24E230  token=0x60005A9  Unity.Mathematics.float2 degrees(Unity.Mathematics.float2 x)
  RVA=0x05F0A954  token=0x60005AA  Unity.Mathematics.float3 degrees(Unity.Mathematics.float3 x)
  RVA=0x0B24E240  token=0x60005AB  Unity.Mathematics.float4 degrees(Unity.Mathematics.float4 x)
  RVA=0x04DAE0A0  token=0x60005AC  System.Double degrees(System.Double x)
  RVA=0x0B24E278  token=0x60005AD  Unity.Mathematics.double2 degrees(Unity.Mathematics.double2 x)
  RVA=0x0B24E19C  token=0x60005AE  Unity.Mathematics.double3 degrees(Unity.Mathematics.double3 x)
  RVA=0x0B24E1E8  token=0x60005AF  Unity.Mathematics.double4 degrees(Unity.Mathematics.double4 x)
  RVA=0x0B24D91C  token=0x60005B0  System.Int32 cmin(Unity.Mathematics.int2 x)
  RVA=0x0B24D7F4  token=0x60005B1  System.Int32 cmin(Unity.Mathematics.int3 x)
  RVA=0x0B24D874  token=0x60005B2  System.Int32 cmin(Unity.Mathematics.int4 x)
  RVA=0x0B24D8B0  token=0x60005B3  System.UInt32 cmin(Unity.Mathematics.uint2 x)
  RVA=0x0B24D968  token=0x60005B4  System.UInt32 cmin(Unity.Mathematics.uint3 x)
  RVA=0x0B24D808  token=0x60005B5  System.UInt32 cmin(Unity.Mathematics.uint4 x)
  RVA=0x0B24D8F8  token=0x60005B6  System.Single cmin(Unity.Mathematics.float2 x)
  RVA=0x0B24D8C4  token=0x60005B7  System.Single cmin(Unity.Mathematics.float3 x)
  RVA=0x05F09F28  token=0x60005B8  System.Single cmin(Unity.Mathematics.float4 x)
  RVA=0x0B24D890  token=0x60005B9  System.Double cmin(Unity.Mathematics.double2 x)
  RVA=0x0B24D930  token=0x60005BA  System.Double cmin(Unity.Mathematics.double3 x)
  RVA=0x0B24D824  token=0x60005BB  System.Double cmin(Unity.Mathematics.double4 x)
  RVA=0x0B24D774  token=0x60005BC  System.Int32 cmax(Unity.Mathematics.int2 x)
  RVA=0x0B24D7A8  token=0x60005BD  System.Int32 cmax(Unity.Mathematics.int3 x)
  RVA=0x0B24D6D8  token=0x60005BE  System.Int32 cmax(Unity.Mathematics.int4 x)
  RVA=0x0B24D6A0  token=0x60005BF  System.UInt32 cmax(Unity.Mathematics.uint2 x)
  RVA=0x0B24D760  token=0x60005C0  System.UInt32 cmax(Unity.Mathematics.uint3 x)
  RVA=0x0B24D6F4  token=0x60005C1  System.UInt32 cmax(Unity.Mathematics.uint4 x)
  RVA=0x0B24D6B4  token=0x60005C2  System.Single cmax(Unity.Mathematics.float2 x)
  RVA=0x0B24D66C  token=0x60005C3  System.Single cmax(Unity.Mathematics.float3 x)
  RVA=0x05F09DB8  token=0x60005C4  System.Single cmax(Unity.Mathematics.float4 x)
  RVA=0x0B24D788  token=0x60005C5  System.Double cmax(Unity.Mathematics.double2 x)
  RVA=0x0B24D7BC  token=0x60005C6  System.Double cmax(Unity.Mathematics.double3 x)
  RVA=0x0B24D710  token=0x60005C7  System.Double cmax(Unity.Mathematics.double4 x)
  RVA=0x04DAE000  token=0x60005C8  System.Int32 csum(Unity.Mathematics.int2 x)
  RVA=0x04DAE050  token=0x60005C9  System.Int32 csum(Unity.Mathematics.int3 x)
  RVA=0x04D8CAC0  token=0x60005CA  System.Int32 csum(Unity.Mathematics.int4 x)
  RVA=0x04DAE000  token=0x60005CB  System.UInt32 csum(Unity.Mathematics.uint2 x)
  RVA=0x04DAE050  token=0x60005CC  System.UInt32 csum(Unity.Mathematics.uint3 x)
  RVA=0x04D8CAC0  token=0x60005CD  System.UInt32 csum(Unity.Mathematics.uint4 x)
  RVA=0x04DAE060  token=0x60005CE  System.Single csum(Unity.Mathematics.float2 x)
  RVA=0x04DAE080  token=0x60005CF  System.Single csum(Unity.Mathematics.float3 x)
  RVA=0x04DADFE0  token=0x60005D0  System.Single csum(Unity.Mathematics.float4 x)
  RVA=0x04DAE020  token=0x60005D1  System.Double csum(Unity.Mathematics.double2 x)
  RVA=0x04DAE010  token=0x60005D2  System.Double csum(Unity.Mathematics.double3 x)
  RVA=0x04DAE030  token=0x60005D3  System.Double csum(Unity.Mathematics.double4 x)
  RVA=0x04D886E0  token=0x60005D4  System.Single square(System.Single x)
  RVA=0x0B2736F8  token=0x60005D5  Unity.Mathematics.float2 square(Unity.Mathematics.float2 x)
  RVA=0x05F36644  token=0x60005D6  Unity.Mathematics.float3 square(Unity.Mathematics.float3 x)
  RVA=0x0B273628  token=0x60005D7  Unity.Mathematics.float4 square(Unity.Mathematics.float4 x)
  RVA=0x04DB05C0  token=0x60005D8  System.Double square(System.Double x)
  RVA=0x0B2735F0  token=0x60005D9  Unity.Mathematics.double2 square(Unity.Mathematics.double2 x)
  RVA=0x0B273668  token=0x60005DA  Unity.Mathematics.double3 square(Unity.Mathematics.double3 x)
  RVA=0x0B2735A0  token=0x60005DB  Unity.Mathematics.double4 square(Unity.Mathematics.double4 x)
  RVA=0x04DB2190  token=0x60005DC  System.Int32 square(System.Int32 x)
  RVA=0x0B273660  token=0x60005DD  Unity.Mathematics.int2 square(Unity.Mathematics.int2 x)
  RVA=0x05F36694  token=0x60005DE  Unity.Mathematics.int3 square(Unity.Mathematics.int3 x)
  RVA=0x0B2736C0  token=0x60005DF  Unity.Mathematics.int4 square(Unity.Mathematics.int4 x)
  RVA=0x04DB2190  token=0x60005E0  System.UInt32 square(System.UInt32 x)
  RVA=0x0B273660  token=0x60005E1  Unity.Mathematics.uint2 square(Unity.Mathematics.uint2 x)
  RVA=0x05F36694  token=0x60005E2  Unity.Mathematics.uint3 square(Unity.Mathematics.uint3 x)
  RVA=0x0B2736C0  token=0x60005E3  Unity.Mathematics.uint4 square(Unity.Mathematics.uint4 x)
  RVA=0x0B24A118  token=0x60005E4  System.Int32 compress(System.Int32* output, System.Int32 index, Unity.Mathematics.int4 val, Unity.Mathematics.bool4 mask)
  RVA=0x0B24D97C  token=0x60005E5  System.Int32 compress(System.UInt32* output, System.Int32 index, Unity.Mathematics.uint4 val, Unity.Mathematics.bool4 mask)
  RVA=0x0B24D97C  token=0x60005E6  System.Int32 compress(System.Single* output, System.Int32 index, Unity.Mathematics.float4 val, Unity.Mathematics.bool4 mask)
  RVA=0x05F0C4C4  token=0x60005E7  System.Single f16tof32(System.UInt32 x)
  RVA=0x0B250034  token=0x60005E8  Unity.Mathematics.float2 f16tof32(Unity.Mathematics.uint2 x)
  RVA=0x0B24FD70  token=0x60005E9  Unity.Mathematics.float3 f16tof32(Unity.Mathematics.uint3 x)
  RVA=0x0B250188  token=0x60005EA  Unity.Mathematics.float4 f16tof32(Unity.Mathematics.uint4 x)
  RVA=0x05F0CFFC  token=0x60005EB  System.UInt32 f32tof16(System.Single x)
  RVA=0x0B2503B4  token=0x60005EC  Unity.Mathematics.uint2 f32tof16(Unity.Mathematics.float2 x)
  RVA=0x0B250540  token=0x60005ED  Unity.Mathematics.uint3 f32tof16(Unity.Mathematics.float3 x)
  RVA=0x0B250838  token=0x60005EE  Unity.Mathematics.uint4 f32tof16(Unity.Mathematics.float4 x)
  RVA=0x0B26DDDC  token=0x60005EF  System.Void orthonormal_basis(Unity.Mathematics.float3 normal, Unity.Mathematics.float3& basis1, Unity.Mathematics.float3& basis2)
  RVA=0x0B26DE80  token=0x60005F0  System.Void orthonormal_basis(Unity.Mathematics.double3 normal, Unity.Mathematics.double3& basis1, Unity.Mathematics.double3& basis2)
  RVA=0x04DADB60  token=0x60005F1  System.Single chgsign(System.Single x, System.Single y)
  RVA=0x0B24D060  token=0x60005F2  Unity.Mathematics.float2 chgsign(Unity.Mathematics.float2 x, Unity.Mathematics.float2 y)
  RVA=0x05F0975C  token=0x60005F3  Unity.Mathematics.float3 chgsign(Unity.Mathematics.float3 x, Unity.Mathematics.float3 y)
  RVA=0x0B24D0A4  token=0x60005F4  Unity.Mathematics.float4 chgsign(Unity.Mathematics.float4 x, Unity.Mathematics.float4 y)
  RVA=0x04DA43B0  token=0x60005F5  System.UInt32 read32_little_endian(System.Void* pBuffer)
  RVA=0x0B2565C0  token=0x60005F6  System.UInt32 hash_with_unaligned_loads(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed)
  RVA=0x0B2567DC  token=0x60005F7  System.UInt32 hash_without_unaligned_loads(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed)
  RVA=0x0B255458  token=0x60005F8  System.UInt32 hash(System.Void* pBuffer, System.Int32 numBytes, System.UInt32 seed)
  RVA=0x04DB21A0  token=0x60005F9  Unity.Mathematics.float3 up()
  RVA=0x04DAF2B0  token=0x60005FA  Unity.Mathematics.float3 down()
  RVA=0x04DB0000  token=0x60005FB  Unity.Mathematics.float3 forward()
  RVA=0x04DAD560  token=0x60005FC  Unity.Mathematics.float3 back()
  RVA=0x04DB0550  token=0x60005FD  Unity.Mathematics.float3 left()
  RVA=0x04DB19B0  token=0x60005FE  Unity.Mathematics.float3 right()
  RVA=0x0B24A584  token=0x60005FF  Unity.Mathematics.float3 EulerXYZ(Unity.Mathematics.quaternion q)
  RVA=0x0B24A908  token=0x6000600  Unity.Mathematics.float3 EulerXZY(Unity.Mathematics.quaternion q)
  RVA=0x0B24AC60  token=0x6000601  Unity.Mathematics.float3 EulerYXZ(Unity.Mathematics.quaternion q)
  RVA=0x0B24AFB4  token=0x6000602  Unity.Mathematics.float3 EulerYZX(Unity.Mathematics.quaternion q)
  RVA=0x0B24B2F0  token=0x6000603  Unity.Mathematics.float3 EulerZXY(Unity.Mathematics.quaternion q)
  RVA=0x0B24B628  token=0x6000604  Unity.Mathematics.float3 EulerZYX(Unity.Mathematics.quaternion q)
  RVA=0x0B24B980  token=0x6000605  Unity.Mathematics.float3 Euler(Unity.Mathematics.quaternion q, Unity.Mathematics.math.RotationOrder order)
  RVA=0x0B25EEF8  token=0x6000606  Unity.Mathematics.float3x3 mulScale(Unity.Mathematics.float3x3 m, Unity.Mathematics.float3 s)
  RVA=0x0B271008  token=0x6000607  Unity.Mathematics.float3x3 scaleMul(Unity.Mathematics.float3 s, Unity.Mathematics.float3x3 m)
  RVA=0x0B276330  token=0x6000608  Unity.Mathematics.float4 unpacklo(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b)
  RVA=0x0B27637C  token=0x6000609  Unity.Mathematics.double4 unpacklo(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b)
  RVA=0x0B24A2B8  token=0x600060A  Unity.Mathematics.float4 unpackhi(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b)
  RVA=0x0B2762C4  token=0x600060B  Unity.Mathematics.double4 unpackhi(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b)
  RVA=0x0B25EE40  token=0x600060C  Unity.Mathematics.float4 movelh(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b)
  RVA=0x0B25EE8C  token=0x600060D  Unity.Mathematics.double4 movelh(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b)
  RVA=0x0B25ED88  token=0x600060E  Unity.Mathematics.float4 movehl(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b)
  RVA=0x0B25EDD4  token=0x600060F  Unity.Mathematics.double4 movehl(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b)
  RVA=0x04DAFFE0  token=0x6000610  System.UInt32 fold_to_uint(System.Double x)
  RVA=0x04DAFEA0  token=0x6000611  Unity.Mathematics.uint2 fold_to_uint(Unity.Mathematics.double2 x)
  RVA=0x04DAFF70  token=0x6000612  Unity.Mathematics.uint3 fold_to_uint(Unity.Mathematics.double3 x)
  RVA=0x04DAFEE0  token=0x6000613  Unity.Mathematics.uint4 fold_to_uint(Unity.Mathematics.double4 x)
  RVA=0x0B2521C0  token=0x6000614  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.float4x4 f4x4)
  RVA=0x0B252234  token=0x6000615  Unity.Mathematics.float3x3 float3x3(Unity.Mathematics.quaternion rotation)
  RVA=0x0B252E4C  token=0x6000616  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B252F08  token=0x6000617  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B252C14  token=0x6000618  Unity.Mathematics.float4x4 float4x4(Unity.Mathematics.RigidTransform transform)
  RVA=0x0B26DF54  token=0x6000619  Unity.Mathematics.float3x3 orthonormalize(Unity.Mathematics.float3x3 i)
  RVA=0x0B26E8D4  token=0x600061A  Unity.Mathematics.float3x3 pseudoinverse(Unity.Mathematics.float3x3 m)
  RVA=0x04DAEB20  token=0x600061B  System.Single mul(System.Single a, System.Single b)
  RVA=0x04D8B640  token=0x600061C  System.Single mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2 b)
  RVA=0x04DB0E40  token=0x600061D  Unity.Mathematics.float2 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x2 b)
  RVA=0x04DB1540  token=0x600061E  Unity.Mathematics.float3 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x3 b)
  RVA=0x04DB15E0  token=0x600061F  Unity.Mathematics.float4 mul(Unity.Mathematics.float2 a, Unity.Mathematics.float2x4 b)
  RVA=0x04D8B7C0  token=0x6000620  System.Single mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b)
  RVA=0x04DB0FD0  token=0x6000621  Unity.Mathematics.float2 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x2 b)
  RVA=0x04DB0F40  token=0x6000622  Unity.Mathematics.float3 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x3 b)
  RVA=0x04DB1370  token=0x6000623  Unity.Mathematics.float4 mul(Unity.Mathematics.float3 a, Unity.Mathematics.float3x4 b)
  RVA=0x034C9990  token=0x6000624  System.Single mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4 b)
  RVA=0x04DB14D0  token=0x6000625  Unity.Mathematics.float2 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x2 b)
  RVA=0x04DB0AA0  token=0x6000626  Unity.Mathematics.float3 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x3 b)
  RVA=0x04DB16F0  token=0x6000627  Unity.Mathematics.float4 mul(Unity.Mathematics.float4 a, Unity.Mathematics.float4x4 b)
  RVA=0x0B2605A4  token=0x6000628  Unity.Mathematics.float2 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2 b)
  RVA=0x0B269AAC  token=0x6000629  Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x2 b)
  RVA=0x0B26A8B8  token=0x600062A  Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x3 b)
  RVA=0x0B25EFC0  token=0x600062B  Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x2 a, Unity.Mathematics.float2x4 b)
  RVA=0x0B261160  token=0x600062C  Unity.Mathematics.float2 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3 b)
  RVA=0x0B2617FC  token=0x600062D  Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x2 b)
  RVA=0x0B267D9C  token=0x600062E  Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x3 b)
  RVA=0x0B260184  token=0x600062F  Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x3 a, Unity.Mathematics.float3x4 b)
  RVA=0x0B2692E4  token=0x6000630  Unity.Mathematics.float2 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4 b)
  RVA=0x0B25F144  token=0x6000631  Unity.Mathematics.float2x2 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x2 b)
  RVA=0x0B267A68  token=0x6000632  Unity.Mathematics.float2x3 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x3 b)
  RVA=0x0B26829C  token=0x6000633  Unity.Mathematics.float2x4 mul(Unity.Mathematics.float2x4 a, Unity.Mathematics.float4x4 b)
  RVA=0x0B262A84  token=0x6000634  Unity.Mathematics.float3 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2 b)
  RVA=0x0B26970C  token=0x6000635  Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x2 b)
  RVA=0x0B26CA28  token=0x6000636  Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x3 b)
  RVA=0x0B25F2DC  token=0x6000637  Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x2 a, Unity.Mathematics.float2x4 b)
  RVA=0x0B26353C  token=0x6000638  Unity.Mathematics.float3 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3 b)
  RVA=0x0B26B3DC  token=0x6000639  Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x2 b)
  RVA=0x0B25F918  token=0x600063A  Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x3 b)
  RVA=0x0B2662EC  token=0x600063B  Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x3 a, Unity.Mathematics.float3x4 b)
  RVA=0x0B265A9C  token=0x600063C  Unity.Mathematics.float3 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4 b)
  RVA=0x0B26C60C  token=0x600063D  Unity.Mathematics.float3x2 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x2 b)
  RVA=0x0B2653E0  token=0x600063E  Unity.Mathematics.float3x3 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x3 b)
  RVA=0x0B260808  token=0x600063F  Unity.Mathematics.float3x4 mul(Unity.Mathematics.float3x4 a, Unity.Mathematics.float4x4 b)
  RVA=0x0B2670B0  token=0x6000640  Unity.Mathematics.float4 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2 b)
  RVA=0x0B267CB8  token=0x6000641  Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x2 b)
  RVA=0x0B264868  token=0x6000642  Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x3 b)
  RVA=0x0B266814  token=0x6000643  Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x2 a, Unity.Mathematics.float2x4 b)
  RVA=0x0B269F60  token=0x6000644  Unity.Mathematics.float4 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3 b)
  RVA=0x0B26A0CC  token=0x6000645  Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x2 b)
  RVA=0x0B268DCC  token=0x6000646  Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x3 b)
  RVA=0x0B26A598  token=0x6000647  Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x3 a, Unity.Mathematics.float3x4 b)
  RVA=0x0B25F54C  token=0x6000648  Unity.Mathematics.float4 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4 b)
  RVA=0x0B261FF4  token=0x6000649  Unity.Mathematics.float4x2 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x2 b)
  RVA=0x0B268FFC  token=0x600064A  Unity.Mathematics.float4x3 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x3 b)
  RVA=0x0B26CC04  token=0x600064B  Unity.Mathematics.float4x4 mul(Unity.Mathematics.float4x4 a, Unity.Mathematics.float4x4 b)
  RVA=0x04DAEAF0  token=0x600064C  System.Double mul(System.Double a, System.Double b)
  RVA=0x04DAEB60  token=0x600064D  System.Double mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2 b)
  RVA=0x04DB15A0  token=0x600064E  Unity.Mathematics.double2 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x2 b)
  RVA=0x04DB1030  token=0x600064F  Unity.Mathematics.double3 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x3 b)
  RVA=0x04DB1100  token=0x6000650  Unity.Mathematics.double4 mul(Unity.Mathematics.double2 a, Unity.Mathematics.double2x4 b)
  RVA=0x04DAEB30  token=0x6000651  System.Double mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3 b)
  RVA=0x04DB10A0  token=0x6000652  Unity.Mathematics.double2 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x2 b)
  RVA=0x04DB1180  token=0x6000653  Unity.Mathematics.double3 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x3 b)
  RVA=0x04DB0E80  token=0x6000654  Unity.Mathematics.double4 mul(Unity.Mathematics.double3 a, Unity.Mathematics.double3x4 b)
  RVA=0x04DAEA80  token=0x6000655  System.Double mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4 b)
  RVA=0x04DB1210  token=0x6000656  Unity.Mathematics.double2 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x2 b)
  RVA=0x04DB0C00  token=0x6000657  Unity.Mathematics.double3 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x3 b)
  RVA=0x04DB1800  token=0x6000658  Unity.Mathematics.double4 mul(Unity.Mathematics.double4 a, Unity.Mathematics.double4x4 b)
  RVA=0x0B267240  token=0x6000659  Unity.Mathematics.double2 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2 b)
  RVA=0x0B265BE4  token=0x600065A  Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x2 b)
  RVA=0x0B267F58  token=0x600065B  Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x3 b)
  RVA=0x0B267664  token=0x600065C  Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x2 a, Unity.Mathematics.double2x4 b)
  RVA=0x0B25F644  token=0x600065D  Unity.Mathematics.double2 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3 b)
  RVA=0x0B2666B0  token=0x600065E  Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x2 b)
  RVA=0x0B26ADF4  token=0x600065F  Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x3 b)
  RVA=0x0B268828  token=0x6000660  Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x3 a, Unity.Mathematics.double3x4 b)
  RVA=0x0B26BD6C  token=0x6000661  Unity.Mathematics.double2 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4 b)
  RVA=0x0B260F80  token=0x6000662  Unity.Mathematics.double2x2 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x2 b)
  RVA=0x0B26B0F4  token=0x6000663  Unity.Mathematics.double2x3 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x3 b)
  RVA=0x0B2613CC  token=0x6000664  Unity.Mathematics.double2x4 mul(Unity.Mathematics.double2x4 a, Unity.Mathematics.double4x4 b)
  RVA=0x0B266068  token=0x6000665  Unity.Mathematics.double3 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2 b)
  RVA=0x0B26C8B0  token=0x6000666  Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x2 b)
  RVA=0x0B26268C  token=0x6000667  Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x3 b)
  RVA=0x0B26D120  token=0x6000668  Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x2 a, Unity.Mathematics.double2x4 b)
  RVA=0x0B269E4C  token=0x6000669  Unity.Mathematics.double3 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3 b)
  RVA=0x0B262B20  token=0x600066A  Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x2 b)
  RVA=0x0B265CC0  token=0x600066B  Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x3 b)
  RVA=0x0B2621D8  token=0x600066C  Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x3 a, Unity.Mathematics.double3x4 b)
  RVA=0x0B26407C  token=0x600066D  Unity.Mathematics.double3 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4 b)
  RVA=0x0B2669FC  token=0x600066E  Unity.Mathematics.double3x2 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x2 b)
  RVA=0x0B26B744  token=0x600066F  Unity.Mathematics.double3x3 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x3 b)
  RVA=0x0B26BFC0  token=0x6000670  Unity.Mathematics.double3x4 mul(Unity.Mathematics.double3x4 a, Unity.Mathematics.double4x4 b)
  RVA=0x0B268780  token=0x6000671  Unity.Mathematics.double4 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2 b)
  RVA=0x0B26B5D8  token=0x6000672  Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x2 b)
  RVA=0x0B269858  token=0x6000673  Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x3 b)
  RVA=0x0B26D448  token=0x6000674  Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x2 a, Unity.Mathematics.double2x4 b)
  RVA=0x0B2657B8  token=0x6000675  Unity.Mathematics.double4 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3 b)
  RVA=0x0B26784C  token=0x6000676  Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x2 b)
  RVA=0x0B269390  token=0x6000677  Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x3 b)
  RVA=0x0B263020  token=0x6000678  Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x3 a, Unity.Mathematics.double3x4 b)
  RVA=0x0B2603CC  token=0x6000679  Unity.Mathematics.double4 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4 b)
  RVA=0x0B267394  token=0x600067A  Unity.Mathematics.double4x2 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x2 b)
  RVA=0x0B2641F0  token=0x600067B  Unity.Mathematics.double4x3 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x3 b)
  RVA=0x0B263970  token=0x600067C  Unity.Mathematics.double4x4 mul(Unity.Mathematics.double4x4 a, Unity.Mathematics.double4x4 b)
  RVA=0x04DAEA70  token=0x600067D  System.Int32 mul(System.Int32 a, System.Int32 b)
  RVA=0x04DAEB00  token=0x600067E  System.Int32 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2 b)
  RVA=0x04DB1440  token=0x600067F  Unity.Mathematics.int2 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x2 b)
  RVA=0x04DB0DE0  token=0x6000680  Unity.Mathematics.int3 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x3 b)
  RVA=0x04DB1290  token=0x6000681  Unity.Mathematics.int4 mul(Unity.Mathematics.int2 a, Unity.Mathematics.int2x4 b)
  RVA=0x04D85D00  token=0x6000682  System.Int32 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3 b)
  RVA=0x04DB1480  token=0x6000683  Unity.Mathematics.int2 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x2 b)
  RVA=0x04DB0CC0  token=0x6000684  Unity.Mathematics.int3 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x3 b)
  RVA=0x04DB0D40  token=0x6000685  Unity.Mathematics.int4 mul(Unity.Mathematics.int3 a, Unity.Mathematics.int3x4 b)
  RVA=0x04DAEAC0  token=0x6000686  System.Int32 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4 b)
  RVA=0x04DB1310  token=0x6000687  Unity.Mathematics.int2 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x2 b)
  RVA=0x04DB0B60  token=0x6000688  Unity.Mathematics.int3 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x3 b)
  RVA=0x04DB09E0  token=0x6000689  Unity.Mathematics.int4 mul(Unity.Mathematics.int4 a, Unity.Mathematics.int4x4 b)
  RVA=0x0B26362C  token=0x600068A  Unity.Mathematics.int2 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2 b)
  RVA=0x0B26A018  token=0x600068B  Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x2 b)
  RVA=0x0B263F74  token=0x600068C  Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x3 b)
  RVA=0x0B25FBF4  token=0x600068D  Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x2 a, Unity.Mathematics.int2x4 b)
  RVA=0x0B2658BC  token=0x600068E  Unity.Mathematics.int2 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3 b)
  RVA=0x0B267130  token=0x600068F  Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x2 b)
  RVA=0x0B263674  token=0x6000690  Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x3 b)
  RVA=0x0B260D94  token=0x6000691  Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x3 a, Unity.Mathematics.int3x4 b)
  RVA=0x0B262D58  token=0x6000692  Unity.Mathematics.int2 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4 b)
  RVA=0x0B26380C  token=0x6000693  Unity.Mathematics.int2x2 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x2 b)
  RVA=0x0B2605F4  token=0x6000694  Unity.Mathematics.int2x3 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x3 b)
  RVA=0x0B268B48  token=0x6000695  Unity.Mathematics.int2x4 mul(Unity.Mathematics.int2x4 a, Unity.Mathematics.int4x4 b)
  RVA=0x0B2634A4  token=0x6000696  Unity.Mathematics.int3 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2 b)
  RVA=0x0B26BC24  token=0x6000697  Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x2 b)
  RVA=0x0B264694  token=0x6000698  Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x3 b)
  RVA=0x0B26517C  token=0x6000699  Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x2 a, Unity.Mathematics.int2x4 b)
  RVA=0x0B265090  token=0x600069A  Unity.Mathematics.int3 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3 b)
  RVA=0x0B2611DC  token=0x600069B  Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x2 b)
  RVA=0x0B269B80  token=0x600069C  Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x3 b)
  RVA=0x0B264CE4  token=0x600069D  Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x3 a, Unity.Mathematics.int3x4 b)
  RVA=0x0B262E00  token=0x600069E  Unity.Mathematics.int3 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4 b)
  RVA=0x0B25FD60  token=0x600069F  Unity.Mathematics.int3x2 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x2 b)
  RVA=0x0B266CF0  token=0x60006A0  Unity.Mathematics.int3x3 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x3 b)
  RVA=0x0B261A88  token=0x60006A1  Unity.Mathematics.int3x4 mul(Unity.Mathematics.int3x4 a, Unity.Mathematics.int4x4 b)
  RVA=0x0B26052C  token=0x60006A2  Unity.Mathematics.int4 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2 b)
  RVA=0x0B262F40  token=0x60006A3  Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x2 b)
  RVA=0x0B265938  token=0x60006A4  Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x3 b)
  RVA=0x0B2680CC  token=0x60006A5  Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x2 a, Unity.Mathematics.int2x4 b)
  RVA=0x0B2600D0  token=0x60006A6  Unity.Mathematics.int4 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3 b)
  RVA=0x0B261930  token=0x60006A7  Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x2 b)
  RVA=0x0B25F6FC  token=0x60006A8  Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x3 b)
  RVA=0x0B2649DC  token=0x60006A9  Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x3 a, Unity.Mathematics.int3x4 b)
  RVA=0x0B268690  token=0x60006AA  Unity.Mathematics.int4 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4 b)
  RVA=0x0B26611C  token=0x60006AB  Unity.Mathematics.int4x2 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x2 b)
  RVA=0x0B26A234  token=0x60006AC  Unity.Mathematics.int4x3 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x3 b)
  RVA=0x0B26A9E4  token=0x60006AD  Unity.Mathematics.int4x4 mul(Unity.Mathematics.int4x4 a, Unity.Mathematics.int4x4 b)
  RVA=0x04DAEA70  token=0x60006AE  System.UInt32 mul(System.UInt32 a, System.UInt32 b)
  RVA=0x04DAEB00  token=0x60006AF  System.UInt32 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2 b)
  RVA=0x04DB1440  token=0x60006B0  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x2 b)
  RVA=0x04DB0DE0  token=0x60006B1  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x3 b)
  RVA=0x04DB1670  token=0x60006B2  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2x4 b)
  RVA=0x04D85D00  token=0x60006B3  System.UInt32 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3 b)
  RVA=0x04DB1480  token=0x60006B4  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x2 b)
  RVA=0x04DB0CC0  token=0x60006B5  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x3 b)
  RVA=0x04DB0D40  token=0x60006B6  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3x4 b)
  RVA=0x04DAEAC0  token=0x60006B7  System.UInt32 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4 b)
  RVA=0x04DB1310  token=0x60006B8  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x2 b)
  RVA=0x04DB0B60  token=0x60006B9  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x3 b)
  RVA=0x04DB09E0  token=0x60006BA  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4x4 b)
  RVA=0x0B26362C  token=0x60006BB  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2 b)
  RVA=0x0B26A018  token=0x60006BC  Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x2 b)
  RVA=0x0B263F74  token=0x60006BD  Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x3 b)
  RVA=0x0B25FBF4  token=0x60006BE  Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x2 a, Unity.Mathematics.uint2x4 b)
  RVA=0x0B2658BC  token=0x60006BF  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3 b)
  RVA=0x0B267130  token=0x60006C0  Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x2 b)
  RVA=0x0B263674  token=0x60006C1  Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x3 b)
  RVA=0x0B260D94  token=0x60006C2  Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x3 a, Unity.Mathematics.uint3x4 b)
  RVA=0x0B262D58  token=0x60006C3  Unity.Mathematics.uint2 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4 b)
  RVA=0x0B26380C  token=0x60006C4  Unity.Mathematics.uint2x2 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x2 b)
  RVA=0x0B2605F4  token=0x60006C5  Unity.Mathematics.uint2x3 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x3 b)
  RVA=0x0B268B48  token=0x60006C6  Unity.Mathematics.uint2x4 mul(Unity.Mathematics.uint2x4 a, Unity.Mathematics.uint4x4 b)
  RVA=0x0B2634A4  token=0x60006C7  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2 b)
  RVA=0x0B26BC24  token=0x60006C8  Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x2 b)
  RVA=0x0B264694  token=0x60006C9  Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x3 b)
  RVA=0x0B26517C  token=0x60006CA  Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x2 a, Unity.Mathematics.uint2x4 b)
  RVA=0x0B265090  token=0x60006CB  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3 b)
  RVA=0x0B2611DC  token=0x60006CC  Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x2 b)
  RVA=0x0B269B80  token=0x60006CD  Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x3 b)
  RVA=0x0B264CE4  token=0x60006CE  Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x3 a, Unity.Mathematics.uint3x4 b)
  RVA=0x0B262E00  token=0x60006CF  Unity.Mathematics.uint3 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4 b)
  RVA=0x0B25FD60  token=0x60006D0  Unity.Mathematics.uint3x2 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x2 b)
  RVA=0x0B266CF0  token=0x60006D1  Unity.Mathematics.uint3x3 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x3 b)
  RVA=0x0B261A88  token=0x60006D2  Unity.Mathematics.uint3x4 mul(Unity.Mathematics.uint3x4 a, Unity.Mathematics.uint4x4 b)
  RVA=0x0B26052C  token=0x60006D3  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2 b)
  RVA=0x0B262F40  token=0x60006D4  Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x2 b)
  RVA=0x0B265938  token=0x60006D5  Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x3 b)
  RVA=0x0B2680CC  token=0x60006D6  Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x2 a, Unity.Mathematics.uint2x4 b)
  RVA=0x0B2600D0  token=0x60006D7  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3 b)
  RVA=0x0B261930  token=0x60006D8  Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x2 b)
  RVA=0x0B25F6FC  token=0x60006D9  Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x3 b)
  RVA=0x0B2649DC  token=0x60006DA  Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x3 a, Unity.Mathematics.uint3x4 b)
  RVA=0x0B268690  token=0x60006DB  Unity.Mathematics.uint4 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4 b)
  RVA=0x0B26611C  token=0x60006DC  Unity.Mathematics.uint4x2 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x2 b)
  RVA=0x0B26A234  token=0x60006DD  Unity.Mathematics.uint4x3 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x3 b)
  RVA=0x0B26A9E4  token=0x60006DE  Unity.Mathematics.uint4x4 mul(Unity.Mathematics.uint4x4 a, Unity.Mathematics.uint4x4 b)
  RVA=0x04DAF870  token=0x60006DF  Unity.Mathematics.quaternion quaternion(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x02B77630  token=0x60006E0  Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float4 value)
  RVA=0x0B26EB8C  token=0x60006E1  Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float3x3 m)
  RVA=0x0B26EB44  token=0x60006E2  Unity.Mathematics.quaternion quaternion(Unity.Mathematics.float4x4 m)
  RVA=0x0B24D998  token=0x60006E3  Unity.Mathematics.quaternion conjugate(Unity.Mathematics.quaternion q)
  RVA=0x0B25CBA4  token=0x60006E4  Unity.Mathematics.quaternion inverse(Unity.Mathematics.quaternion q)
  RVA=0x0B24E6D4  token=0x60006E5  System.Single dot(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b)
  RVA=0x0B25D670  token=0x60006E6  System.Single length(Unity.Mathematics.quaternion q)
  RVA=0x0B25D6AC  token=0x60006E7  System.Single lengthsq(Unity.Mathematics.quaternion q)
  RVA=0x0B26D8B8  token=0x60006E8  Unity.Mathematics.quaternion normalize(Unity.Mathematics.quaternion q)
  RVA=0x0B26DC30  token=0x60006E9  Unity.Mathematics.quaternion normalizesafe(Unity.Mathematics.quaternion q)
  RVA=0x0B26DBA4  token=0x60006EA  Unity.Mathematics.quaternion normalizesafe(Unity.Mathematics.quaternion q, Unity.Mathematics.quaternion defaultvalue)
  RVA=0x05F39B9C  token=0x60006EB  Unity.Mathematics.quaternion unitexp(Unity.Mathematics.quaternion q)
  RVA=0x05F0C274  token=0x60006EC  Unity.Mathematics.quaternion exp(Unity.Mathematics.quaternion q)
  RVA=0x05F39CC0  token=0x60006ED  Unity.Mathematics.quaternion unitlog(Unity.Mathematics.quaternion q)
  RVA=0x05F17EAC  token=0x60006EE  Unity.Mathematics.quaternion log(Unity.Mathematics.quaternion q)
  RVA=0x0B2628F8  token=0x60006EF  Unity.Mathematics.quaternion mul(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b)
  RVA=0x0B26BE64  token=0x60006F0  Unity.Mathematics.float3 mul(Unity.Mathematics.quaternion q, Unity.Mathematics.float3 v)
  RVA=0x0B26BE64  token=0x60006F1  Unity.Mathematics.float3 rotate(Unity.Mathematics.quaternion q, Unity.Mathematics.float3 v)
  RVA=0x0B26D758  token=0x60006F2  Unity.Mathematics.quaternion nlerp(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2, System.Single t)
  RVA=0x0B272C48  token=0x60006F3  Unity.Mathematics.quaternion slerp(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2, System.Single t)
  RVA=0x0B24C184  token=0x60006F4  System.Single angle(Unity.Mathematics.quaternion q1, Unity.Mathematics.quaternion q2)
  RVA=0x0B2707D8  token=0x60006F5  Unity.Mathematics.quaternion rotation(Unity.Mathematics.float3x3 m)
  RVA=0x0B24BE4C  token=0x60006F6  Unity.Mathematics.float3x3 adj(Unity.Mathematics.float3x3 m, System.Single& det)
  RVA=0x0B24BD4C  token=0x60006F7  System.Boolean adjInverse(Unity.Mathematics.float3x3 m, Unity.Mathematics.float3x3& i, System.Single epsilon)
  RVA=0x0B256480  token=0x60006F8  System.UInt32 hash(Unity.Mathematics.quaternion q)
  RVA=0x0B2590D8  token=0x60006F9  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.quaternion q)
  RVA=0x066B1564  token=0x60006FA  Unity.Mathematics.float3 forward(Unity.Mathematics.quaternion q)
  RVA=0x04DAD020  token=0x60006FB  Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.quaternion rot, Unity.Mathematics.float3 pos)
  RVA=0x0B24BAAC  token=0x60006FC  Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B24BB08  token=0x60006FD  Unity.Mathematics.RigidTransform RigidTransform(Unity.Mathematics.float4x4 transform)
  RVA=0x0B25CC20  token=0x60006FE  Unity.Mathematics.RigidTransform inverse(Unity.Mathematics.RigidTransform t)
  RVA=0x0B26B024  token=0x60006FF  Unity.Mathematics.RigidTransform mul(Unity.Mathematics.RigidTransform a, Unity.Mathematics.RigidTransform b)
  RVA=0x0B25FFF4  token=0x6000700  Unity.Mathematics.float4 mul(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float4 pos)
  RVA=0x066B2E2C  token=0x6000701  Unity.Mathematics.float3 rotate(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float3 dir)
  RVA=0x0B273E64  token=0x6000702  Unity.Mathematics.float3 transform(Unity.Mathematics.RigidTransform a, Unity.Mathematics.float3 pos)
  RVA=0x0B25657C  token=0x6000703  System.UInt32 hash(Unity.Mathematics.RigidTransform t)
  RVA=0x0B2578A8  token=0x6000704  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.RigidTransform t)
  RVA=0x04DAD8A0  token=0x6000705  Unity.Mathematics.uint2 uint2(System.UInt32 x, System.UInt32 y)
  RVA=0x04DAB5F0  token=0x6000706  Unity.Mathematics.uint2 uint2(Unity.Mathematics.uint2 xy)
  RVA=0x04DB02A0  token=0x6000707  Unity.Mathematics.uint2 uint2(System.UInt32 v)
  RVA=0x0B25A394  token=0x6000708  Unity.Mathematics.uint2 uint2(System.Boolean v)
  RVA=0x0B25A3AC  token=0x6000709  Unity.Mathematics.uint2 uint2(Unity.Mathematics.bool2 v)
  RVA=0x04DB02A0  token=0x600070A  Unity.Mathematics.uint2 uint2(System.Int32 v)
  RVA=0x04DAB5F0  token=0x600070B  Unity.Mathematics.uint2 uint2(Unity.Mathematics.int2 v)
  RVA=0x0B275514  token=0x600070C  Unity.Mathematics.uint2 uint2(System.Single v)
  RVA=0x0B275538  token=0x600070D  Unity.Mathematics.uint2 uint2(Unity.Mathematics.float2 v)
  RVA=0x0B2754F4  token=0x600070E  Unity.Mathematics.uint2 uint2(System.Double v)
  RVA=0x0B2754C4  token=0x600070F  Unity.Mathematics.uint2 uint2(Unity.Mathematics.double2 v)
  RVA=0x0B254904  token=0x6000710  System.UInt32 hash(Unity.Mathematics.uint2 v)
  RVA=0x0B257570  token=0x6000711  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2 v)
  RVA=0x05F345E8  token=0x6000712  System.UInt32 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F33804  token=0x6000713  Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F33D78  token=0x6000714  Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F34DD4  token=0x6000715  Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint2 left, Unity.Mathematics.uint2 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F328C0  token=0x6000716  System.UInt32 select_shuffle_component(Unity.Mathematics.uint2 a, Unity.Mathematics.uint2 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x02E515B0  token=0x6000717  Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1)
  RVA=0x0B25A428  token=0x6000718  Unity.Mathematics.uint2x2 uint2x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11)
  RVA=0x0B25A410  token=0x6000719  Unity.Mathematics.uint2x2 uint2x2(System.UInt32 v)
  RVA=0x0B25A4CC  token=0x600071A  Unity.Mathematics.uint2x2 uint2x2(System.Boolean v)
  RVA=0x0B25A3CC  token=0x600071B  Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.bool2x2 v)
  RVA=0x0B25A410  token=0x600071C  Unity.Mathematics.uint2x2 uint2x2(System.Int32 v)
  RVA=0x0B25A490  token=0x600071D  Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.int2x2 v)
  RVA=0x0B27555C  token=0x600071E  Unity.Mathematics.uint2x2 uint2x2(System.Single v)
  RVA=0x0B275618  token=0x600071F  Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.float2x2 v)
  RVA=0x0B275578  token=0x6000720  Unity.Mathematics.uint2x2 uint2x2(System.Double v)
  RVA=0x0B2755BC  token=0x6000721  Unity.Mathematics.uint2x2 uint2x2(Unity.Mathematics.double2x2 v)
  RVA=0x0B274334  token=0x6000722  Unity.Mathematics.uint2x2 transpose(Unity.Mathematics.uint2x2 v)
  RVA=0x0B255C44  token=0x6000723  System.UInt32 hash(Unity.Mathematics.uint2x2 v)
  RVA=0x0B257480  token=0x6000724  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x2 v)
  RVA=0x04DAF5A0  token=0x6000725  Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2)
  RVA=0x05F164A8  token=0x6000726  Unity.Mathematics.uint2x3 uint2x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12)
  RVA=0x0B25A53C  token=0x6000727  Unity.Mathematics.uint2x3 uint2x3(System.UInt32 v)
  RVA=0x0B25A5F0  token=0x6000728  Unity.Mathematics.uint2x3 uint2x3(System.Boolean v)
  RVA=0x0B25A57C  token=0x6000729  Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.bool2x3 v)
  RVA=0x0B25A53C  token=0x600072A  Unity.Mathematics.uint2x3 uint2x3(System.Int32 v)
  RVA=0x0B25A504  token=0x600072B  Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.int2x3 v)
  RVA=0x0B275698  token=0x600072C  Unity.Mathematics.uint2x3 uint2x3(System.Single v)
  RVA=0x0B275660  token=0x600072D  Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.float2x3 v)
  RVA=0x0B275640  token=0x600072E  Unity.Mathematics.uint2x3 uint2x3(System.Double v)
  RVA=0x0B2756B8  token=0x600072F  Unity.Mathematics.uint2x3 uint2x3(Unity.Mathematics.double2x3 v)
  RVA=0x0B274F98  token=0x6000730  Unity.Mathematics.uint3x2 transpose(Unity.Mathematics.uint2x3 v)
  RVA=0x0B255160  token=0x6000731  System.UInt32 hash(Unity.Mathematics.uint2x3 v)
  RVA=0x0B25A0D0  token=0x6000732  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x3 v)
  RVA=0x04DB02D0  token=0x6000733  Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2, Unity.Mathematics.uint2 c3)
  RVA=0x05F164E0  token=0x6000734  Unity.Mathematics.uint2x4 uint2x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13)
  RVA=0x0B25A66C  token=0x6000735  Unity.Mathematics.uint2x4 uint2x4(System.UInt32 v)
  RVA=0x0B25A6BC  token=0x6000736  Unity.Mathematics.uint2x4 uint2x4(System.Boolean v)
  RVA=0x0B25A648  token=0x6000737  Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.bool2x4 v)
  RVA=0x0B25A66C  token=0x6000738  Unity.Mathematics.uint2x4 uint2x4(System.Int32 v)
  RVA=0x0B25A688  token=0x6000739  Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.int2x4 v)
  RVA=0x0B275728  token=0x600073A  Unity.Mathematics.uint2x4 uint2x4(System.Single v)
  RVA=0x0B2756F4  token=0x600073B  Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.float2x4 v)
  RVA=0x0B275748  token=0x600073C  Unity.Mathematics.uint2x4 uint2x4(System.Double v)
  RVA=0x0B275764  token=0x600073D  Unity.Mathematics.uint2x4 uint2x4(Unity.Mathematics.double2x4 v)
  RVA=0x0B27454C  token=0x600073E  Unity.Mathematics.uint4x2 transpose(Unity.Mathematics.uint2x4 v)
  RVA=0x0B2540B8  token=0x600073F  System.UInt32 hash(Unity.Mathematics.uint2x4 v)
  RVA=0x0B259108  token=0x6000740  Unity.Mathematics.uint2 hashwide(Unity.Mathematics.uint2x4 v)
  RVA=0x04DAD8B0  token=0x6000741  Unity.Mathematics.uint3 uint3(System.UInt32 x, System.UInt32 y, System.UInt32 z)
  RVA=0x04DB0330  token=0x6000742  Unity.Mathematics.uint3 uint3(System.UInt32 x, Unity.Mathematics.uint2 yz)
  RVA=0x04DB0350  token=0x6000743  Unity.Mathematics.uint3 uint3(Unity.Mathematics.uint2 xy, System.UInt32 z)
  RVA=0x04DB0370  token=0x6000744  Unity.Mathematics.uint3 uint3(Unity.Mathematics.uint3 xyz)
  RVA=0x04DB02F0  token=0x6000745  Unity.Mathematics.uint3 uint3(System.UInt32 v)
  RVA=0x0B25A788  token=0x6000746  Unity.Mathematics.uint3 uint3(System.Boolean v)
  RVA=0x0B25A7A0  token=0x6000747  Unity.Mathematics.uint3 uint3(Unity.Mathematics.bool3 v)
  RVA=0x04DB02F0  token=0x6000748  Unity.Mathematics.uint3 uint3(System.Int32 v)
  RVA=0x04DB0370  token=0x6000749  Unity.Mathematics.uint3 uint3(Unity.Mathematics.int3 v)
  RVA=0x0B275808  token=0x600074A  Unity.Mathematics.uint3 uint3(System.Single v)
  RVA=0x05F3B330  token=0x600074B  Unity.Mathematics.uint3 uint3(Unity.Mathematics.float3 v)
  RVA=0x0B2757E0  token=0x600074C  Unity.Mathematics.uint3 uint3(System.Double v)
  RVA=0x0B2757A8  token=0x600074D  Unity.Mathematics.uint3 uint3(Unity.Mathematics.double3 v)
  RVA=0x05F0F580  token=0x600074E  System.UInt32 hash(Unity.Mathematics.uint3 v)
  RVA=0x05F156A4  token=0x600074F  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3 v)
  RVA=0x05F34FF4  token=0x6000750  System.UInt32 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x05F344F0  token=0x6000751  Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x05F34F18  token=0x6000752  Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x05F347E8  token=0x6000753  Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint3 left, Unity.Mathematics.uint3 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F33024  token=0x6000754  System.UInt32 select_shuffle_component(Unity.Mathematics.uint3 a, Unity.Mathematics.uint3 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAF7F0  token=0x6000755  Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1)
  RVA=0x05F16534  token=0x6000756  Unity.Mathematics.uint3x2 uint3x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21)
  RVA=0x0B25A884  token=0x6000757  Unity.Mathematics.uint3x2 uint3x2(System.UInt32 v)
  RVA=0x0B25A8A4  token=0x6000758  Unity.Mathematics.uint3x2 uint3x2(System.Boolean v)
  RVA=0x0B25A8C4  token=0x6000759  Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.bool3x2 v)
  RVA=0x0B25A884  token=0x600075A  Unity.Mathematics.uint3x2 uint3x2(System.Int32 v)
  RVA=0x0B25A814  token=0x600075B  Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.int3x2 v)
  RVA=0x0B2758F8  token=0x600075C  Unity.Mathematics.uint3x2 uint3x2(System.Single v)
  RVA=0x0B2758C0  token=0x600075D  Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.float3x2 v)
  RVA=0x0B275824  token=0x600075E  Unity.Mathematics.uint3x2 uint3x2(System.Double v)
  RVA=0x0B275884  token=0x600075F  Unity.Mathematics.uint3x2 uint3x2(Unity.Mathematics.double3x2 v)
  RVA=0x0B274370  token=0x6000760  Unity.Mathematics.uint2x3 transpose(Unity.Mathematics.uint3x2 v)
  RVA=0x0B25465C  token=0x6000761  System.UInt32 hash(Unity.Mathematics.uint3x2 v)
  RVA=0x0B258BF0  token=0x6000762  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x2 v)
  RVA=0x034C99D0  token=0x6000763  Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2)
  RVA=0x05F165C4  token=0x6000764  Unity.Mathematics.uint3x3 uint3x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22)
  RVA=0x0B25A93C  token=0x6000765  Unity.Mathematics.uint3x3 uint3x3(System.UInt32 v)
  RVA=0x0B25AAB4  token=0x6000766  Unity.Mathematics.uint3x3 uint3x3(System.Boolean v)
  RVA=0x0B25AA74  token=0x6000767  Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.bool3x3 v)
  RVA=0x0B25A93C  token=0x6000768  Unity.Mathematics.uint3x3 uint3x3(System.Int32 v)
  RVA=0x0B25A97C  token=0x6000769  Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.int3x3 v)
  RVA=0x0B275978  token=0x600076A  Unity.Mathematics.uint3x3 uint3x3(System.Single v)
  RVA=0x0B275938  token=0x600076B  Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.float3x3 v)
  RVA=0x0B275918  token=0x600076C  Unity.Mathematics.uint3x3 uint3x3(System.Double v)
  RVA=0x0B27599C  token=0x600076D  Unity.Mathematics.uint3x3 uint3x3(Unity.Mathematics.double3x3 v)
  RVA=0x0B2748D4  token=0x600076E  Unity.Mathematics.uint3x3 transpose(Unity.Mathematics.uint3x3 v)
  RVA=0x0B255E3C  token=0x600076F  System.UInt32 hash(Unity.Mathematics.uint3x3 v)
  RVA=0x0B2592C8  token=0x6000770  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x3 v)
  RVA=0x04DAF820  token=0x6000771  Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2, Unity.Mathematics.uint3 c3)
  RVA=0x05F16688  token=0x6000772  Unity.Mathematics.uint3x4 uint3x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23)
  RVA=0x0B25AB90  token=0x6000773  Unity.Mathematics.uint3x4 uint3x4(System.UInt32 v)
  RVA=0x0B25AAD4  token=0x6000774  Unity.Mathematics.uint3x4 uint3x4(System.Boolean v)
  RVA=0x0B25AB54  token=0x6000775  Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.bool3x4 v)
  RVA=0x0B25AB90  token=0x6000776  Unity.Mathematics.uint3x4 uint3x4(System.Int32 v)
  RVA=0x0B25AC34  token=0x6000777  Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.int3x4 v)
  RVA=0x0B275AB0  token=0x6000778  Unity.Mathematics.uint3x4 uint3x4(System.Single v)
  RVA=0x0B275A6C  token=0x6000779  Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.float3x4 v)
  RVA=0x0B2759F0  token=0x600077A  Unity.Mathematics.uint3x4 uint3x4(System.Double v)
  RVA=0x0B275A10  token=0x600077B  Unity.Mathematics.uint3x4 uint3x4(Unity.Mathematics.double3x4 v)
  RVA=0x0B27445C  token=0x600077C  Unity.Mathematics.uint4x3 transpose(Unity.Mathematics.uint3x4 v)
  RVA=0x0B255A78  token=0x600077D  System.UInt32 hash(Unity.Mathematics.uint3x4 v)
  RVA=0x0B259E00  token=0x600077E  Unity.Mathematics.uint3 hashwide(Unity.Mathematics.uint3x4 v)
  RVA=0x04DAD8C0  token=0x600077F  Unity.Mathematics.uint4 uint4(System.UInt32 x, System.UInt32 y, System.UInt32 z, System.UInt32 w)
  RVA=0x04DB04B0  token=0x6000780  Unity.Mathematics.uint4 uint4(System.UInt32 x, System.UInt32 y, Unity.Mathematics.uint2 zw)
  RVA=0x04DB0530  token=0x6000781  Unity.Mathematics.uint4 uint4(System.UInt32 x, Unity.Mathematics.uint2 yz, System.UInt32 w)
  RVA=0x04DB0430  token=0x6000782  Unity.Mathematics.uint4 uint4(System.UInt32 x, Unity.Mathematics.uint3 yzw)
  RVA=0x04DB04D0  token=0x6000783  Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint2 xy, System.UInt32 z, System.UInt32 w)
  RVA=0x04DAB610  token=0x6000784  Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint2 xy, Unity.Mathematics.uint2 zw)
  RVA=0x04DB04F0  token=0x6000785  Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint3 xyz, System.UInt32 w)
  RVA=0x0B25AC78  token=0x6000786  Unity.Mathematics.uint4 uint4(Unity.Mathematics.uint4 xyzw)
  RVA=0x04DB0420  token=0x6000787  Unity.Mathematics.uint4 uint4(System.UInt32 v)
  RVA=0x0B25ACA4  token=0x6000788  Unity.Mathematics.uint4 uint4(System.Boolean v)
  RVA=0x0B25ACC4  token=0x6000789  Unity.Mathematics.uint4 uint4(Unity.Mathematics.bool4 v)
  RVA=0x04DB0420  token=0x600078A  Unity.Mathematics.uint4 uint4(System.Int32 v)
  RVA=0x0B25AC78  token=0x600078B  Unity.Mathematics.uint4 uint4(Unity.Mathematics.int4 v)
  RVA=0x0B275B1C  token=0x600078C  Unity.Mathematics.uint4 uint4(System.Single v)
  RVA=0x0B275B64  token=0x600078D  Unity.Mathematics.uint4 uint4(Unity.Mathematics.float4 v)
  RVA=0x0B275B34  token=0x600078E  Unity.Mathematics.uint4 uint4(System.Double v)
  RVA=0x0B275AD4  token=0x600078F  Unity.Mathematics.uint4 uint4(Unity.Mathematics.double4 v)
  RVA=0x0B253A04  token=0x6000790  System.UInt32 hash(Unity.Mathematics.uint4 v)
  RVA=0x0B257404  token=0x6000791  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4 v)
  RVA=0x0B271AE0  token=0x6000792  System.UInt32 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x)
  RVA=0x0B271660  token=0x6000793  Unity.Mathematics.uint2 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y)
  RVA=0x0B271DF8  token=0x6000794  Unity.Mathematics.uint3 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z)
  RVA=0x0B272138  token=0x6000795  Unity.Mathematics.uint4 shuffle(Unity.Mathematics.uint4 left, Unity.Mathematics.uint4 right, Unity.Mathematics.math.ShuffleComponent x, Unity.Mathematics.math.ShuffleComponent y, Unity.Mathematics.math.ShuffleComponent z, Unity.Mathematics.math.ShuffleComponent w)
  RVA=0x05F33540  token=0x6000796  System.UInt32 select_shuffle_component(Unity.Mathematics.uint4 a, Unity.Mathematics.uint4 b, Unity.Mathematics.math.ShuffleComponent component)
  RVA=0x04DAFA00  token=0x6000797  Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1)
  RVA=0x05F16750  token=0x6000798  Unity.Mathematics.uint4x2 uint4x2(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21, System.UInt32 m30, System.UInt32 m31)
  RVA=0x0B25ADCC  token=0x6000799  Unity.Mathematics.uint4x2 uint4x2(System.UInt32 v)
  RVA=0x0B275BAC  token=0x600079A  Unity.Mathematics.uint4x2 uint4x2(System.Boolean v)
  RVA=0x0B275B88  token=0x600079B  Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.bool4x2 v)
  RVA=0x0B25ADCC  token=0x600079C  Unity.Mathematics.uint4x2 uint4x2(System.Int32 v)
  RVA=0x0B25ADE8  token=0x600079D  Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.int4x2 v)
  RVA=0x0B275BC8  token=0x600079E  Unity.Mathematics.uint4x2 uint4x2(System.Single v)
  RVA=0x0B275C2C  token=0x600079F  Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.float4x2 v)
  RVA=0x0B275C60  token=0x60007A0  Unity.Mathematics.uint4x2 uint4x2(System.Double v)
  RVA=0x0B275BE8  token=0x60007A1  Unity.Mathematics.uint4x2 uint4x2(Unity.Mathematics.double4x2 v)
  RVA=0x0B2743BC  token=0x60007A2  Unity.Mathematics.uint2x4 transpose(Unity.Mathematics.uint4x2 v)
  RVA=0x0B25605C  token=0x60007A3  System.UInt32 hash(Unity.Mathematics.uint4x2 v)
  RVA=0x0B256A7C  token=0x60007A4  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x2 v)
  RVA=0x04DAFA20  token=0x60007A5  Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2)
  RVA=0x05F16804  token=0x60007A6  Unity.Mathematics.uint4x3 uint4x3(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32)
  RVA=0x0B25AECC  token=0x60007A7  Unity.Mathematics.uint4x3 uint4x3(System.UInt32 v)
  RVA=0x0B275D9C  token=0x60007A8  Unity.Mathematics.uint4x3 uint4x3(System.Boolean v)
  RVA=0x0B275D60  token=0x60007A9  Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.bool4x3 v)
  RVA=0x0B25AECC  token=0x60007AA  Unity.Mathematics.uint4x3 uint4x3(System.Int32 v)
  RVA=0x0B25AFA8  token=0x60007AB  Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.int4x3 v)
  RVA=0x0B275D1C  token=0x60007AC  Unity.Mathematics.uint4x3 uint4x3(System.Single v)
  RVA=0x0B275C7C  token=0x60007AD  Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.float4x3 v)
  RVA=0x0B275D40  token=0x60007AE  Unity.Mathematics.uint4x3 uint4x3(System.Double v)
  RVA=0x0B275CC0  token=0x60007AF  Unity.Mathematics.uint4x3 uint4x3(Unity.Mathematics.double4x3 v)
  RVA=0x0B274770  token=0x60007B0  Unity.Mathematics.uint3x4 transpose(Unity.Mathematics.uint4x3 v)
  RVA=0x0B254D80  token=0x60007B1  System.UInt32 hash(Unity.Mathematics.uint4x3 v)
  RVA=0x0B258CE8  token=0x60007B2  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x3 v)
  RVA=0x04DAFA40  token=0x60007B3  Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2, Unity.Mathematics.uint4 c3)
  RVA=0x05F169CC  token=0x60007B4  Unity.Mathematics.uint4x4 uint4x4(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32, System.UInt32 m33)
  RVA=0x0B25B0CC  token=0x60007B5  Unity.Mathematics.uint4x4 uint4x4(System.UInt32 v)
  RVA=0x0B275DBC  token=0x60007B6  Unity.Mathematics.uint4x4 uint4x4(System.Boolean v)
  RVA=0x0B275E3C  token=0x60007B7  Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.bool4x4 v)
  RVA=0x0B25B0CC  token=0x60007B8  Unity.Mathematics.uint4x4 uint4x4(System.Int32 v)
  RVA=0x0B25B14C  token=0x60007B9  Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.int4x4 v)
  RVA=0x0B275F00  token=0x60007BA  Unity.Mathematics.uint4x4 uint4x4(System.Single v)
  RVA=0x0B275DE0  token=0x60007BB  Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.float4x4 v)
  RVA=0x0B275F28  token=0x60007BC  Unity.Mathematics.uint4x4 uint4x4(System.Double v)
  RVA=0x0B275E74  token=0x60007BD  Unity.Mathematics.uint4x4 uint4x4(Unity.Mathematics.double4x4 v)
  RVA=0x0B274AF4  token=0x60007BE  Unity.Mathematics.uint4x4 transpose(Unity.Mathematics.uint4x4 v)
  RVA=0x0B254938  token=0x60007BF  System.UInt32 hash(Unity.Mathematics.uint4x4 v)
  RVA=0x0B2571C8  token=0x60007C0  Unity.Mathematics.uint4 hashwide(Unity.Mathematics.uint4x4 v)
END_CLASS

CLASS: Unity.Mathematics.bool2
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x12
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Boolean                  x  // 0x10
  public            System.Boolean                  y  // 0x11
METHODS:
  RVA=0x02BBF3D0  token=0x60007C1  System.Void .ctor(System.Boolean x, System.Boolean y)
  RVA=0x04DB21E0  token=0x60007C2  System.Void .ctor(Unity.Mathematics.bool2 xy)
  RVA=0x04DB21F0  token=0x60007C3  System.Void .ctor(System.Boolean v)
  RVA=0x04DAD580  token=0x60007C4  Unity.Mathematics.bool2 op_Implicit(System.Boolean v)
  RVA=0x0B27CF60  token=0x60007C5  System.Boolean Equals(Unity.Mathematics.bool2 rhs)
  RVA=0x0B27CF74  token=0x60007C6  System.Boolean Equals(System.Object o)
  RVA=0x0B27CFE8  token=0x60007C7  System.Int32 GetHashCode()
  RVA=0x0B27D030  token=0x60007C8  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool2x2
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool2         c0  // 0x10
  public            Unity.Mathematics.bool2         c1  // 0x12
METHODS:
  RVA=0x04DA4480  token=0x60007C9  System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1)
  RVA=0x04DB2230  token=0x60007CA  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11)
  RVA=0x04DB2200  token=0x60007CB  System.Void .ctor(System.Boolean v)
  RVA=0x0B276A58  token=0x60007CC  System.Boolean Equals(Unity.Mathematics.bool2x2 rhs)
  RVA=0x0B27D0B8  token=0x60007CD  System.Boolean Equals(System.Object o)
  RVA=0x0B27D118  token=0x60007CE  System.Int32 GetHashCode()
  RVA=0x0B27D120  token=0x60007CF  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool2x3
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x16
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool2         c0  // 0x10
  public            Unity.Mathematics.bool2         c1  // 0x12
  public            Unity.Mathematics.bool2         c2  // 0x14
METHODS:
  RVA=0x04DB22F0  token=0x60007D0  System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2)
  RVA=0x04DB2260  token=0x60007D1  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12)
  RVA=0x04DB22B0  token=0x60007D2  System.Void .ctor(System.Boolean v)
  RVA=0x0B276BE0  token=0x60007D3  System.Boolean Equals(Unity.Mathematics.bool2x3 rhs)
  RVA=0x0B27D27C  token=0x60007D4  System.Boolean Equals(System.Object o)
  RVA=0x0B27D2EC  token=0x60007D5  System.Int32 GetHashCode()
  RVA=0x0B27D310  token=0x60007D6  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool2x4
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool2         c0  // 0x10
  public            Unity.Mathematics.bool2         c1  // 0x12
  public            Unity.Mathematics.bool2         c2  // 0x14
  public            Unity.Mathematics.bool2         c3  // 0x16
METHODS:
  RVA=0x04DB23B0  token=0x60007D7  System.Void .ctor(Unity.Mathematics.bool2 c0, Unity.Mathematics.bool2 c1, Unity.Mathematics.bool2 c2, Unity.Mathematics.bool2 c3)
  RVA=0x04DB2300  token=0x60007D8  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13)
  RVA=0x04DB2360  token=0x60007D9  System.Void .ctor(System.Boolean v)
  RVA=0x0B276DE8  token=0x60007DA  System.Boolean Equals(Unity.Mathematics.bool2x4 rhs)
  RVA=0x0B27D4D8  token=0x60007DB  System.Boolean Equals(System.Object o)
  RVA=0x0B27D538  token=0x60007DC  System.Int32 GetHashCode()
  RVA=0x0B27D540  token=0x60007DD  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool3
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x13
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Boolean                  x  // 0x10
  public            System.Boolean                  y  // 0x11
  public            System.Boolean                  z  // 0x12
PROPERTIES:
  Item  get=0x03966740  set=0x04D93080
METHODS:
  RVA=0x04DA0780  token=0x60007DE  System.Void .ctor(System.Boolean x, System.Boolean y, System.Boolean z)
  RVA=0x04DB23E0  token=0x60007DF  System.Void .ctor(System.Boolean x, Unity.Mathematics.bool2 yz)
  RVA=0x04DB23F0  token=0x60007E0  System.Void .ctor(Unity.Mathematics.bool2 xy, System.Boolean z)
  RVA=0x04DB2400  token=0x60007E1  System.Void .ctor(Unity.Mathematics.bool3 xyz)
  RVA=0x04DB23D0  token=0x60007E2  System.Void .ctor(System.Boolean v)
  RVA=0x04DAD620  token=0x60007E3  Unity.Mathematics.bool3 op_Implicit(System.Boolean v)
  RVA=0x03B885B0  token=0x60007E4  Unity.Mathematics.bool3 op_BitwiseAnd(Unity.Mathematics.bool3 lhs, Unity.Mathematics.bool3 rhs)
  RVA=0x0B27D8B8  token=0x60007E5  Unity.Mathematics.bool3 op_BitwiseOr(Unity.Mathematics.bool3 lhs, Unity.Mathematics.bool3 rhs)
  RVA=0x0B277070  token=0x60007E8  System.Boolean Equals(Unity.Mathematics.bool3 rhs)
  RVA=0x0B27D770  token=0x60007E9  System.Boolean Equals(System.Object o)
  RVA=0x0B27D7E0  token=0x60007EA  System.Int32 GetHashCode()
  RVA=0x0B27D804  token=0x60007EB  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool3x2
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x16
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool3         c0  // 0x10
  public            Unity.Mathematics.bool3         c1  // 0x13
METHODS:
  RVA=0x04DB2490  token=0x60007EC  System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1)
  RVA=0x04DB2420  token=0x60007ED  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21)
  RVA=0x04DB2460  token=0x60007EE  System.Void .ctor(System.Boolean v)
  RVA=0x0B27D958  token=0x60007EF  System.Boolean Equals(Unity.Mathematics.bool3x2 rhs)
  RVA=0x0B27D8E8  token=0x60007F0  System.Boolean Equals(System.Object o)
  RVA=0x0B27D9A4  token=0x60007F1  System.Int32 GetHashCode()
  RVA=0x0B27D9C8  token=0x60007F2  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool3x3
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x19
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool3         c0  // 0x10
  public            Unity.Mathematics.bool3         c1  // 0x13
  public            Unity.Mathematics.bool3         c2  // 0x16
METHODS:
  RVA=0x04DB2560  token=0x60007F3  System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2)
  RVA=0x04DB24F0  token=0x60007F4  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22)
  RVA=0x04DB24B0  token=0x60007F5  System.Void .ctor(System.Boolean v)
  RVA=0x0B27DC04  token=0x60007F6  System.Boolean Equals(Unity.Mathematics.bool3x3 rhs)
  RVA=0x0B27DB90  token=0x60007F7  System.Boolean Equals(System.Object o)
  RVA=0x0B27DC78  token=0x60007F8  System.Int32 GetHashCode()
  RVA=0x0B27DCA0  token=0x60007F9  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool3x4
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool3         c0  // 0x10
  public            Unity.Mathematics.bool3         c1  // 0x13
  public            Unity.Mathematics.bool3         c2  // 0x16
  public            Unity.Mathematics.bool3         c3  // 0x19
METHODS:
  RVA=0x04DB2670  token=0x60007FA  System.Void .ctor(Unity.Mathematics.bool3 c0, Unity.Mathematics.bool3 c1, Unity.Mathematics.bool3 c2, Unity.Mathematics.bool3 c3)
  RVA=0x04DB25E0  token=0x60007FB  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23)
  RVA=0x04DB2590  token=0x60007FC  System.Void .ctor(System.Boolean v)
  RVA=0x0B27DF04  token=0x60007FD  System.Boolean Equals(Unity.Mathematics.bool3x4 rhs)
  RVA=0x0B27DF98  token=0x60007FE  System.Boolean Equals(System.Object o)
  RVA=0x0B27E00C  token=0x60007FF  System.Int32 GetHashCode()
  RVA=0x0B27E030  token=0x6000800  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool4
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Boolean                  x  // 0x10
  public            System.Boolean                  y  // 0x11
  public            System.Boolean                  z  // 0x12
  public            System.Boolean                  w  // 0x13
PROPERTIES:
  Item  set=0x04D93080
METHODS:
  RVA=0x04DAB240  token=0x6000801  System.Void .ctor(System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w)
  RVA=0x04DB2790  token=0x6000802  System.Void .ctor(System.Boolean x, System.Boolean y, Unity.Mathematics.bool2 zw)
  RVA=0x04DB26F0  token=0x6000803  System.Void .ctor(System.Boolean x, Unity.Mathematics.bool2 yz, System.Boolean w)
  RVA=0x04DB2710  token=0x6000804  System.Void .ctor(System.Boolean x, Unity.Mathematics.bool3 yzw)
  RVA=0x04DB2730  token=0x6000805  System.Void .ctor(Unity.Mathematics.bool2 xy, System.Boolean z, System.Boolean w)
  RVA=0x04DB2750  token=0x6000806  System.Void .ctor(Unity.Mathematics.bool2 xy, Unity.Mathematics.bool2 zw)
  RVA=0x04DB2770  token=0x6000807  System.Void .ctor(Unity.Mathematics.bool3 xyz, System.Boolean w)
  RVA=0x04DB26C0  token=0x6000808  System.Void .ctor(Unity.Mathematics.bool4 xyzw)
  RVA=0x04DB26E0  token=0x6000809  System.Void .ctor(System.Boolean v)
  RVA=0x04DAD7B0  token=0x600080A  Unity.Mathematics.bool4 op_Implicit(System.Boolean v)
  RVA=0x0B2779E4  token=0x600080C  System.Boolean Equals(Unity.Mathematics.bool4 rhs)
  RVA=0x0B27E330  token=0x600080D  System.Boolean Equals(System.Object o)
  RVA=0x0B27E390  token=0x600080E  System.Int32 GetHashCode()
  RVA=0x0B27E398  token=0x600080F  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool4x2
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool4         c0  // 0x10
  public            Unity.Mathematics.bool4         c1  // 0x14
METHODS:
  RVA=0x04D85C40  token=0x6000810  System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1)
  RVA=0x04DB27E0  token=0x6000811  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m10, System.Boolean m11, System.Boolean m20, System.Boolean m21, System.Boolean m30, System.Boolean m31)
  RVA=0x04DB27B0  token=0x6000812  System.Void .ctor(System.Boolean v)
  RVA=0x0B277B6C  token=0x6000813  System.Boolean Equals(Unity.Mathematics.bool4x2 rhs)
  RVA=0x0B27E4F4  token=0x6000814  System.Boolean Equals(System.Object o)
  RVA=0x0B27E554  token=0x6000815  System.Int32 GetHashCode()
  RVA=0x0B27E55C  token=0x6000816  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool4x3
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool4         c0  // 0x10
  public            Unity.Mathematics.bool4         c1  // 0x14
  public            Unity.Mathematics.bool4         c2  // 0x18
METHODS:
  RVA=0x04D85D60  token=0x6000817  System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2)
  RVA=0x04DB2830  token=0x6000818  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m30, System.Boolean m31, System.Boolean m32)
  RVA=0x04DB28B0  token=0x6000819  System.Void .ctor(System.Boolean v)
  RVA=0x0B27E800  token=0x600081A  System.Boolean Equals(Unity.Mathematics.bool4x3 rhs)
  RVA=0x0B27E78C  token=0x600081B  System.Boolean Equals(System.Object o)
  RVA=0x0B27E840  token=0x600081C  System.Int32 GetHashCode()
  RVA=0x0B27E864  token=0x600081D  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.bool4x4
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Unity.Mathematics.bool4         c0  // 0x10
  public            Unity.Mathematics.bool4         c1  // 0x14
  public            Unity.Mathematics.bool4         c2  // 0x18
  public            Unity.Mathematics.bool4         c3  // 0x1c
METHODS:
  RVA=0x04D88C10  token=0x600081E  System.Void .ctor(Unity.Mathematics.bool4 c0, Unity.Mathematics.bool4 c1, Unity.Mathematics.bool4 c2, Unity.Mathematics.bool4 c3)
  RVA=0x04DB2900  token=0x600081F  System.Void .ctor(System.Boolean m00, System.Boolean m01, System.Boolean m02, System.Boolean m03, System.Boolean m10, System.Boolean m11, System.Boolean m12, System.Boolean m13, System.Boolean m20, System.Boolean m21, System.Boolean m22, System.Boolean m23, System.Boolean m30, System.Boolean m31, System.Boolean m32, System.Boolean m33)
  RVA=0x04DB29B0  token=0x6000820  System.Void .ctor(System.Boolean v)
  RVA=0x0B27EBD0  token=0x6000821  System.Boolean Equals(Unity.Mathematics.bool4x4 rhs)
  RVA=0x0B27EB64  token=0x6000822  System.Boolean Equals(System.Object o)
  RVA=0x0B27EC20  token=0x6000823  System.Int32 GetHashCode()
  RVA=0x0B27EC3C  token=0x6000824  System.String ToString()
END_CLASS

CLASS: Unity.Mathematics.double2
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Double                   x  // 0x10
  public            System.Double                   y  // 0x18
METHODS:
  RVA=0x04D881B0  token=0x6000825  System.Void .ctor(System.Double x, System.Double y)
  RVA=0x04D881A0  token=0x6000826  System.Void .ctor(Unity.Mathematics.double2 xy)
  RVA=0x04DB2A60  token=0x6000827  System.Void .ctor(System.Double v)
  RVA=0x0B27F20C  token=0x6000828  System.Void .ctor(System.Boolean v)
  RVA=0x05F00990  token=0x6000829  System.Void .ctor(Unity.Mathematics.bool2 v)
  RVA=0x04DB2A90  token=0x600082A  System.Void .ctor(System.Int32 v)
  RVA=0x04DB2A40  token=0x600082B  System.Void .ctor(Unity.Mathematics.int2 v)
  RVA=0x04DB2A70  token=0x600082C  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB2AB0  token=0x600082D  System.Void .ctor(Unity.Mathematics.uint2 v)
  RVA=0x0B27F1D0  token=0x600082E  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B27F190  token=0x600082F  System.Void .ctor(Unity.Mathematics.half2 v)
  RVA=0x04DB2AD0  token=0x6000830  System.Void .ctor(System.Single v)
  RVA=0x04DB2A10  token=0x6000831  System.Void .ctor(Unity.Mathematics.float2 v)
  RVA=0x04DAECB0  token=0x6000832  Unity.Mathematics.double2 op_Implicit(System.Double v)
  RVA=0x04DAEC50  token=0x6000833  Unity.Mathematics.double2 op_Implicit(System.Int32 v)
  RVA=0x04DAEBF0  token=0x6000834  Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.int2 v)
  RVA=0x04DAEB90  token=0x6000835  Unity.Mathematics.double2 op_Implicit(System.UInt32 v)
  RVA=0x04DAEC20  token=0x6000836  Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.uint2 v)
  RVA=0x04DAEC70  token=0x6000837  Unity.Mathematics.double2 op_Implicit(System.Single v)
  RVA=0x04DAEBC0  token=0x6000838  Unity.Mathematics.double2 op_Implicit(Unity.Mathematics.float2 v)
  RVA=0x04DB2B10  token=0x6000839  Unity.Mathematics.double2 op_Multiply(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs)
  RVA=0x04DB2B60  token=0x600083A  Unity.Mathematics.double2 op_Multiply(Unity.Mathematics.double2 lhs, System.Double rhs)
  RVA=0x04DB2B40  token=0x600083B  Unity.Mathematics.double2 op_Multiply(System.Double lhs, Unity.Mathematics.double2 rhs)
  RVA=0x04DB2AE0  token=0x600083C  Unity.Mathematics.double2 op_Addition(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs)
  RVA=0x04DB2BA0  token=0x600083D  Unity.Mathematics.double2 op_Subtraction(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs)
  RVA=0x04DB2B80  token=0x600083E  Unity.Mathematics.double2 op_Subtraction(System.Double lhs, Unity.Mathematics.double2 rhs)
  RVA=0x0B27F23C  token=0x600083F  Unity.Mathematics.double2 op_Division(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs)
  RVA=0x0B27F220  token=0x6000840  Unity.Mathematics.double2 op_Division(System.Double lhs, Unity.Mathematics.double2 rhs)
  RVA=0x0B27F2B8  token=0x6000841  Unity.Mathematics.bool2 op_LessThan(Unity.Mathematics.double2 lhs, System.Double rhs)
  RVA=0x0B27F294  token=0x6000842  Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.double2 lhs, Unity.Mathematics.double2 rhs)
  RVA=0x04DB2BD0  token=0x6000843  Unity.Mathematics.double2 op_UnaryNegation(Unity.Mathematics.double2 val)
  RVA=0x0B27F260  token=0x6000844  Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.double2 lhs, System.Double rhs)
  RVA=0x0B278530  token=0x6000845  System.Boolean Equals(Unity.Mathematics.double2 rhs)
  RVA=0x0B27F00C  token=0x6000846  System.Boolean Equals(System.Object o)
  RVA=0x0B254EB0  token=0x6000847  System.Int32 GetHashCode()
  RVA=0x0B27F100  token=0x6000848  System.String ToString()
  RVA=0x0B27F078  token=0x6000849  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double2x2
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double2       c0  // 0x10
  public            Unity.Mathematics.double2       c1  // 0x20
  public    static readonly Unity.Mathematics.double2x2     identity  // static @ 0x0
METHODS:
  RVA=0x04D8C810  token=0x600084A  System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1)
  RVA=0x04DB2C40  token=0x600084B  System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11)
  RVA=0x04DB2C20  token=0x600084C  System.Void .ctor(System.Double v)
  RVA=0x05F00A78  token=0x600084D  System.Void .ctor(System.Boolean v)
  RVA=0x05F009F8  token=0x600084E  System.Void .ctor(Unity.Mathematics.bool2x2 v)
  RVA=0x04DB2BF0  token=0x600084F  System.Void .ctor(System.Int32 v)
  RVA=0x04DB2D60  token=0x6000850  System.Void .ctor(Unity.Mathematics.int2x2 v)
  RVA=0x04DB2C60  token=0x6000851  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB2CE0  token=0x6000852  System.Void .ctor(Unity.Mathematics.uint2x2 v)
  RVA=0x04DB2D30  token=0x6000853  System.Void .ctor(System.Single v)
  RVA=0x04DB2CA0  token=0x6000854  System.Void .ctor(Unity.Mathematics.float2x2 v)
  RVA=0x0B27F674  token=0x6000855  Unity.Mathematics.double2x2 op_Multiply(Unity.Mathematics.double2x2 lhs, System.Double rhs)
  RVA=0x0B27F2D4  token=0x6000856  System.Boolean Equals(Unity.Mathematics.double2x2 rhs)
  RVA=0x0B27F31C  token=0x6000857  System.Boolean Equals(System.Object o)
  RVA=0x0B27F390  token=0x6000858  System.Int32 GetHashCode()
  RVA=0x0B27F3B4  token=0x6000859  System.String ToString()
  RVA=0x0B27F520  token=0x600085A  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04D19290  token=0x600085B  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.double2x3
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double2       c0  // 0x10
  public            Unity.Mathematics.double2       c1  // 0x20
  public            Unity.Mathematics.double2       c2  // 0x30
METHODS:
  RVA=0x04DA0760  token=0x600085C  System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2)
  RVA=0x04DB2F10  token=0x600085D  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12)
  RVA=0x04DB2EA0  token=0x600085E  System.Void .ctor(System.Double v)
  RVA=0x05F00B04  token=0x600085F  System.Void .ctor(System.Boolean v)
  RVA=0x05F00B38  token=0x6000860  System.Void .ctor(Unity.Mathematics.bool2x3 v)
  RVA=0x04DB3000  token=0x6000861  System.Void .ctor(System.Int32 v)
  RVA=0x04DB2DB0  token=0x6000862  System.Void .ctor(Unity.Mathematics.int2x3 v)
  RVA=0x04DB2FA0  token=0x6000863  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB2E20  token=0x6000864  System.Void .ctor(Unity.Mathematics.uint2x3 v)
  RVA=0x04DB2EC0  token=0x6000865  System.Void .ctor(System.Single v)
  RVA=0x04DB2F40  token=0x6000866  System.Void .ctor(Unity.Mathematics.float2x3 v)
  RVA=0x0B27F6D0  token=0x6000867  System.Boolean Equals(Unity.Mathematics.double2x3 rhs)
  RVA=0x0B27F730  token=0x6000868  System.Boolean Equals(System.Object o)
  RVA=0x0B27F7AC  token=0x6000869  System.Int32 GetHashCode()
  RVA=0x0B27F990  token=0x600086A  System.String ToString()
  RVA=0x0B27F7DC  token=0x600086B  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double2x4
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x50
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double2       c0  // 0x10
  public            Unity.Mathematics.double2       c1  // 0x20
  public            Unity.Mathematics.double2       c2  // 0x30
  public            Unity.Mathematics.double2       c3  // 0x40
METHODS:
  RVA=0x04DB3220  token=0x600086C  System.Void .ctor(Unity.Mathematics.double2 c0, Unity.Mathematics.double2 c1, Unity.Mathematics.double2 c2, Unity.Mathematics.double2 c3)
  RVA=0x04DB3250  token=0x600086D  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13)
  RVA=0x04DB32A0  token=0x600086E  System.Void .ctor(System.Double v)
  RVA=0x05F00CC8  token=0x600086F  System.Void .ctor(System.Boolean v)
  RVA=0x05F00BE8  token=0x6000870  System.Void .ctor(Unity.Mathematics.bool2x4 v)
  RVA=0x04DB3150  token=0x6000871  System.Void .ctor(System.Int32 v)
  RVA=0x04DB3340  token=0x6000872  System.Void .ctor(Unity.Mathematics.int2x4 v)
  RVA=0x04DB32C0  token=0x6000873  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB30B0  token=0x6000874  System.Void .ctor(Unity.Mathematics.uint2x4 v)
  RVA=0x04DB3050  token=0x6000875  System.Void .ctor(System.Single v)
  RVA=0x04DB31B0  token=0x6000876  System.Void .ctor(Unity.Mathematics.float2x4 v)
  RVA=0x0B27FB70  token=0x6000877  System.Boolean Equals(Unity.Mathematics.double2x4 rhs)
  RVA=0x0B27FBEC  token=0x6000878  System.Boolean Equals(System.Object o)
  RVA=0x0B27FC70  token=0x6000879  System.Int32 GetHashCode()
  RVA=0x0B27FEBC  token=0x600087A  System.String ToString()
  RVA=0x0B27FCA8  token=0x600087B  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double3
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Double                   x  // 0x10
  public            System.Double                   y  // 0x18
  public            System.Double                   z  // 0x20
  public    static readonly Unity.Mathematics.double3       zero  // static @ 0x0
PROPERTIES:
  yzx  get=0x04DB3540
  zxy  get=0x04DB3560
  Item  get=0x04DB3530
METHODS:
  RVA=0x04DB3520  token=0x600087C  System.Void .ctor(System.Double x, System.Double y, System.Double z)
  RVA=0x04DB3450  token=0x600087D  System.Void .ctor(System.Double x, Unity.Mathematics.double2 yz)
  RVA=0x04DB3410  token=0x600087E  System.Void .ctor(Unity.Mathematics.double2 xy, System.Double z)
  RVA=0x04DB3490  token=0x600087F  System.Void .ctor(Unity.Mathematics.double3 xyz)
  RVA=0x04DB34B0  token=0x6000880  System.Void .ctor(System.Double v)
  RVA=0x05F00D48  token=0x6000881  System.Void .ctor(System.Boolean v)
  RVA=0x05F00D0C  token=0x6000882  System.Void .ctor(Unity.Mathematics.bool3 v)
  RVA=0x04DB3470  token=0x6000883  System.Void .ctor(System.Int32 v)
  RVA=0x04DB3430  token=0x6000884  System.Void .ctor(Unity.Mathematics.int3 v)
  RVA=0x04DB34C0  token=0x6000885  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB34F0  token=0x6000886  System.Void .ctor(Unity.Mathematics.uint3 v)
  RVA=0x0B280318  token=0x6000887  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B280368  token=0x6000888  System.Void .ctor(Unity.Mathematics.half3 v)
  RVA=0x04DB33F0  token=0x6000889  System.Void .ctor(System.Single v)
  RVA=0x04DB33D0  token=0x600088A  System.Void .ctor(Unity.Mathematics.float3 v)
  RVA=0x04DAEE70  token=0x600088B  Unity.Mathematics.double3 op_Implicit(System.Double v)
  RVA=0x04DAEF30  token=0x600088C  Unity.Mathematics.double3 op_Implicit(System.Int32 v)
  RVA=0x04DAEDD0  token=0x600088D  Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.int3 v)
  RVA=0x04DAEE20  token=0x600088E  Unity.Mathematics.double3 op_Implicit(System.UInt32 v)
  RVA=0x04DAEEA0  token=0x600088F  Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.uint3 v)
  RVA=0x04DAEDA0  token=0x6000890  Unity.Mathematics.double3 op_Implicit(System.Single v)
  RVA=0x0415F9A0  token=0x6000891  Unity.Mathematics.double3 op_Implicit(Unity.Mathematics.float3 v)
  RVA=0x04DB3620  token=0x6000892  Unity.Mathematics.double3 op_Multiply(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x04DB3600  token=0x6000893  Unity.Mathematics.double3 op_Multiply(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x04DB35E0  token=0x6000894  Unity.Mathematics.double3 op_Multiply(System.Double lhs, Unity.Mathematics.double3 rhs)
  RVA=0x04DB35A0  token=0x6000895  Unity.Mathematics.double3 op_Addition(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x04DB3580  token=0x6000896  Unity.Mathematics.double3 op_Addition(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x04DB3680  token=0x6000897  Unity.Mathematics.double3 op_Subtraction(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x04DB3660  token=0x6000898  Unity.Mathematics.double3 op_Subtraction(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x04DB36C0  token=0x6000899  Unity.Mathematics.double3 op_Subtraction(System.Double lhs, Unity.Mathematics.double3 rhs)
  RVA=0x0B2803BC  token=0x600089A  Unity.Mathematics.double3 op_Division(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x0B2803F0  token=0x600089B  Unity.Mathematics.double3 op_Division(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x0B280410  token=0x600089C  Unity.Mathematics.double3 op_Division(System.Double lhs, Unity.Mathematics.double3 rhs)
  RVA=0x0B2804F8  token=0x600089D  Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x0B2804BC  token=0x600089E  Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x0B280484  token=0x600089F  Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.double3 lhs, Unity.Mathematics.double3 rhs)
  RVA=0x04DB36F0  token=0x60008A0  Unity.Mathematics.double3 op_UnaryNegation(Unity.Mathematics.double3 val)
  RVA=0x0B28043C  token=0x60008A1  Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.double3 lhs, System.Double rhs)
  RVA=0x066A8DA0  token=0x60008A5  System.Boolean Equals(Unity.Mathematics.double3 rhs)
  RVA=0x0B28010C  token=0x60008A6  System.Boolean Equals(System.Object o)
  RVA=0x0B280180  token=0x60008A7  System.Int32 GetHashCode()
  RVA=0x0B280258  token=0x60008A8  System.String ToString()
  RVA=0x0B2801A8  token=0x60008A9  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double3x2
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double3       c0  // 0x10
  public            Unity.Mathematics.double3       c1  // 0x28
METHODS:
  RVA=0x04DA06E0  token=0x60008AA  System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1)
  RVA=0x04DB3760  token=0x60008AB  System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21)
  RVA=0x04DB3800  token=0x60008AC  System.Void .ctor(System.Double v)
  RVA=0x05F011E4  token=0x60008AD  System.Void .ctor(System.Boolean v)
  RVA=0x0B28095C  token=0x60008AE  System.Void .ctor(Unity.Mathematics.bool3x2 v)
  RVA=0x04DB3830  token=0x60008AF  System.Void .ctor(System.Int32 v)
  RVA=0x05F012B8  token=0x60008B0  System.Void .ctor(Unity.Mathematics.int3x2 v)
  RVA=0x04DB37A0  token=0x60008B1  System.Void .ctor(System.UInt32 v)
  RVA=0x05F00FEC  token=0x60008B2  System.Void .ctor(Unity.Mathematics.uint3x2 v)
  RVA=0x04DB3720  token=0x60008B3  System.Void .ctor(System.Single v)
  RVA=0x05F01064  token=0x60008B4  System.Void .ctor(Unity.Mathematics.float3x2 v)
  RVA=0x0B24EE70  token=0x60008B5  Unity.Mathematics.double3x2 op_Implicit(System.Int32 v)
  RVA=0x0B24ED1C  token=0x60008B6  Unity.Mathematics.double3x2 op_Implicit(Unity.Mathematics.float3x2 v)
  RVA=0x0B280BB8  token=0x60008B7  Unity.Mathematics.double3x2 op_Multiply(Unity.Mathematics.double3x2 lhs, System.Double rhs)
  RVA=0x0B280A60  token=0x60008B8  Unity.Mathematics.double3x2 op_Addition(Unity.Mathematics.double3x2 lhs, Unity.Mathematics.double3x2 rhs)
  RVA=0x0B280C3C  token=0x60008B9  Unity.Mathematics.double3x2 op_Subtraction(Unity.Mathematics.double3x2 lhs, Unity.Mathematics.double3x2 rhs)
  RVA=0x0B280B24  token=0x60008BA  Unity.Mathematics.double3x2 op_Division(Unity.Mathematics.double3x2 lhs, System.Double rhs)
  RVA=0x066A921C  token=0x60008BB  System.Boolean Equals(Unity.Mathematics.double3x2 rhs)
  RVA=0x0B28051C  token=0x60008BC  System.Boolean Equals(System.Object o)
  RVA=0x0B280598  token=0x60008BD  System.Int32 GetHashCode()
  RVA=0x0B28077C  token=0x60008BE  System.String ToString()
  RVA=0x0B2805C8  token=0x60008BF  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double3x3
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x58
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double3       c0  // 0x10
  public            Unity.Mathematics.double3       c1  // 0x28
  public            Unity.Mathematics.double3       c2  // 0x40
  public    static readonly Unity.Mathematics.double3x3     identity  // static @ 0x0
PROPERTIES:
  Item  get=0x04DB3AC0
METHODS:
  RVA=0x04DB38E0  token=0x60008C0  System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2)
  RVA=0x04DB3A60  token=0x60008C1  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22)
  RVA=0x04DB3920  token=0x60008C2  System.Void .ctor(System.Double v)
  RVA=0x05F01698  token=0x60008C3  System.Void .ctor(System.Boolean v)
  RVA=0x0B281338  token=0x60008C4  System.Void .ctor(Unity.Mathematics.bool3x3 v)
  RVA=0x04DB39F0  token=0x60008C5  System.Void .ctor(System.Int32 v)
  RVA=0x05F015F0  token=0x60008C6  System.Void .ctor(Unity.Mathematics.int3x3 v)
  RVA=0x04DB3960  token=0x60008C7  System.Void .ctor(System.UInt32 v)
  RVA=0x05F01548  token=0x60008C8  System.Void .ctor(Unity.Mathematics.uint3x3 v)
  RVA=0x04DB3880  token=0x60008C9  System.Void .ctor(System.Single v)
  RVA=0x05F01330  token=0x60008CA  System.Void .ctor(Unity.Mathematics.float3x3 v)
  RVA=0x0B2814A4  token=0x60008CB  Unity.Mathematics.double3x3 op_Multiply(Unity.Mathematics.double3x3 lhs, System.Double rhs)
  RVA=0x0B280D94  token=0x60008CD  System.Boolean Equals(Unity.Mathematics.double3x3 rhs)
  RVA=0x0B280D00  token=0x60008CE  System.Boolean Equals(System.Object o)
  RVA=0x0B280E28  token=0x60008CF  System.Int32 GetHashCode()
  RVA=0x0B280E6C  token=0x60008D0  System.String ToString()
  RVA=0x0B2810F4  token=0x60008D1  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04B6B1D0  token=0x60008D2  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.double3x4
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x70
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double3       c0  // 0x10
  public            Unity.Mathematics.double3       c1  // 0x28
  public            Unity.Mathematics.double3       c2  // 0x40
  public            Unity.Mathematics.double3       c3  // 0x58
PROPERTIES:
  Item  get=0x04DB3AC0
METHODS:
  RVA=0x04DB3AD0  token=0x60008D3  System.Void .ctor(Unity.Mathematics.double3 c0, Unity.Mathematics.double3 c1, Unity.Mathematics.double3 c2, Unity.Mathematics.double3 c3)
  RVA=0x04DB3B20  token=0x60008D4  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23)
  RVA=0x04DB3CE0  token=0x60008D5  System.Void .ctor(System.Double v)
  RVA=0x05F01B54  token=0x60008D6  System.Void .ctor(System.Boolean v)
  RVA=0x0B281D40  token=0x60008D7  System.Void .ctor(Unity.Mathematics.bool3x4 v)
  RVA=0x04DB3BA0  token=0x60008D8  System.Void .ctor(System.Int32 v)
  RVA=0x05F01A78  token=0x60008D9  System.Void .ctor(Unity.Mathematics.int3x4 v)
  RVA=0x04DB3C20  token=0x60008DA  System.Void .ctor(System.UInt32 v)
  RVA=0x05F0199C  token=0x60008DB  System.Void .ctor(Unity.Mathematics.uint3x4 v)
  RVA=0x04DB3D30  token=0x60008DC  System.Void .ctor(System.Single v)
  RVA=0x05F01CD0  token=0x60008DD  System.Void .ctor(Unity.Mathematics.float3x4 v)
  RVA=0x0B24F088  token=0x60008DE  Unity.Mathematics.double3x4 op_Implicit(System.Int32 v)
  RVA=0x0B281568  token=0x60008E0  System.Boolean Equals(Unity.Mathematics.double3x4 rhs)
  RVA=0x0B281650  token=0x60008E1  System.Boolean Equals(System.Object o)
  RVA=0x0B2816F0  token=0x60008E2  System.Int32 GetHashCode()
  RVA=0x0B281A10  token=0x60008E3  System.String ToString()
  RVA=0x0B28173C  token=0x60008E4  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double4
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Double                   x  // 0x10
  public            System.Double                   y  // 0x18
  public            System.Double                   z  // 0x20
  public            System.Double                   w  // 0x28
  public    static readonly Unity.Mathematics.double4       zero  // static @ 0x0
PROPERTIES:
  xyz  get=0x04DB3FB0  set=0x04DB3490
  Item  get=0x04DB3530  set=0x04DB4160
METHODS:
  RVA=0x04DB3E70  token=0x60008E5  System.Void .ctor(System.Double x, System.Double y, System.Double z, System.Double w)
  RVA=0x04DB3DF0  token=0x60008E6  System.Void .ctor(System.Double x, System.Double y, Unity.Mathematics.double2 zw)
  RVA=0x04DB3E10  token=0x60008E7  System.Void .ctor(System.Double x, Unity.Mathematics.double2 yz, System.Double w)
  RVA=0x04DB3E50  token=0x60008E8  System.Void .ctor(System.Double x, Unity.Mathematics.double3 yzw)
  RVA=0x04DB3F50  token=0x60008E9  System.Void .ctor(Unity.Mathematics.double2 xy, System.Double z, System.Double w)
  RVA=0x04DB3E30  token=0x60008EA  System.Void .ctor(Unity.Mathematics.double2 xy, Unity.Mathematics.double2 zw)
  RVA=0x04DB3F30  token=0x60008EB  System.Void .ctor(Unity.Mathematics.double3 xyz, System.Double w)
  RVA=0x04DB3DD0  token=0x60008EC  System.Void .ctor(Unity.Mathematics.double4 xyzw)
  RVA=0x04DB3F70  token=0x60008ED  System.Void .ctor(System.Double v)
  RVA=0x05F01E20  token=0x60008EE  System.Void .ctor(System.Boolean v)
  RVA=0x05F01EC8  token=0x60008EF  System.Void .ctor(Unity.Mathematics.bool4 v)
  RVA=0x04DB3E90  token=0x60008F0  System.Void .ctor(System.Int32 v)
  RVA=0x04DB3F10  token=0x60008F1  System.Void .ctor(Unity.Mathematics.int4 v)
  RVA=0x04DB3F80  token=0x60008F2  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB3EC0  token=0x60008F3  System.Void .ctor(Unity.Mathematics.uint4 v)
  RVA=0x05F01E64  token=0x60008F4  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x05F01DAC  token=0x60008F5  System.Void .ctor(Unity.Mathematics.half4 v)
  RVA=0x04DB3DB0  token=0x60008F6  System.Void .ctor(System.Single v)
  RVA=0x04DB3F00  token=0x60008F7  System.Void .ctor(Unity.Mathematics.float4 v)
  RVA=0x04DAF080  token=0x60008F8  Unity.Mathematics.double4 op_Implicit(System.Double v)
  RVA=0x04DAF0E0  token=0x60008F9  Unity.Mathematics.double4 op_Implicit(System.Int32 v)
  RVA=0x0B24F2FC  token=0x60008FA  Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.int4 v)
  RVA=0x04DAF1A0  token=0x60008FB  Unity.Mathematics.double4 op_Implicit(System.UInt32 v)
  RVA=0x0B24F324  token=0x60008FC  Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.uint4 v)
  RVA=0x04DAF090  token=0x60008FD  Unity.Mathematics.double4 op_Implicit(System.Single v)
  RVA=0x0B24F2B8  token=0x60008FE  Unity.Mathematics.double4 op_Implicit(Unity.Mathematics.float4 v)
  RVA=0x04DB4040  token=0x60008FF  Unity.Mathematics.double4 op_Multiply(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs)
  RVA=0x04DB4020  token=0x6000900  Unity.Mathematics.double4 op_Multiply(Unity.Mathematics.double4 lhs, System.Double rhs)
  RVA=0x04DB4090  token=0x6000901  Unity.Mathematics.double4 op_Multiply(System.Double lhs, Unity.Mathematics.double4 rhs)
  RVA=0x04DB3FD0  token=0x6000902  Unity.Mathematics.double4 op_Addition(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs)
  RVA=0x04DB40F0  token=0x6000903  Unity.Mathematics.double4 op_Subtraction(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs)
  RVA=0x04DB40B0  token=0x6000904  Unity.Mathematics.double4 op_Subtraction(System.Double lhs, Unity.Mathematics.double4 rhs)
  RVA=0x0B2822B8  token=0x6000905  Unity.Mathematics.double4 op_Division(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs)
  RVA=0x0B2822FC  token=0x6000906  Unity.Mathematics.double4 op_Division(System.Double lhs, Unity.Mathematics.double4 rhs)
  RVA=0x0B2823E0  token=0x6000907  Unity.Mathematics.bool4 op_LessThan(Unity.Mathematics.double4 lhs, System.Double rhs)
  RVA=0x0B282398  token=0x6000908  Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.double4 lhs, Unity.Mathematics.double4 rhs)
  RVA=0x04DB4140  token=0x6000909  Unity.Mathematics.double4 op_UnaryNegation(Unity.Mathematics.double4 val)
  RVA=0x0B282334  token=0x600090A  Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.double4 lhs, System.Double rhs)
  RVA=0x0B281F8C  token=0x600090F  System.Boolean Equals(Unity.Mathematics.double4 rhs)
  RVA=0x0B281F18  token=0x6000910  System.Boolean Equals(System.Object o)
  RVA=0x0B281FD4  token=0x6000911  System.Int32 GetHashCode()
  RVA=0x0B281FF8  token=0x6000912  System.String ToString()
  RVA=0x0B282164  token=0x6000913  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double4x2
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x50
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double4       c0  // 0x10
  public            Unity.Mathematics.double4       c1  // 0x30
METHODS:
  RVA=0x04DB4230  token=0x6000914  System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1)
  RVA=0x04DB4170  token=0x6000915  System.Void .ctor(System.Double m00, System.Double m01, System.Double m10, System.Double m11, System.Double m20, System.Double m21, System.Double m30, System.Double m31)
  RVA=0x04DB41C0  token=0x6000916  System.Void .ctor(System.Double v)
  RVA=0x05F023C4  token=0x6000917  System.Void .ctor(System.Boolean v)
  RVA=0x05F02320  token=0x6000918  System.Void .ctor(Unity.Mathematics.bool4x2 v)
  RVA=0x04DB4250  token=0x6000919  System.Void .ctor(System.Int32 v)
  RVA=0x0B282990  token=0x600091A  System.Void .ctor(Unity.Mathematics.int4x2 v)
  RVA=0x05F02278  token=0x600091B  System.Void .ctor(System.UInt32 v)
  RVA=0x0B2829F4  token=0x600091C  System.Void .ctor(Unity.Mathematics.uint4x2 v)
  RVA=0x04DB41F0  token=0x600091D  System.Void .ctor(System.Single v)
  RVA=0x0B282A58  token=0x600091E  System.Void .ctor(Unity.Mathematics.float4x2 v)
  RVA=0x0B282490  token=0x600091F  System.Boolean Equals(Unity.Mathematics.double4x2 rhs)
  RVA=0x0B28240C  token=0x6000920  System.Boolean Equals(System.Object o)
  RVA=0x0B2824F4  token=0x6000921  System.Int32 GetHashCode()
  RVA=0x0B282740  token=0x6000922  System.String ToString()
  RVA=0x0B28252C  token=0x6000923  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double4x3
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x70
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double4       c0  // 0x10
  public            Unity.Mathematics.double4       c1  // 0x30
  public            Unity.Mathematics.double4       c2  // 0x50
METHODS:
  RVA=0x04DB4450  token=0x6000924  System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2)
  RVA=0x04DB4350  token=0x6000925  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m10, System.Double m11, System.Double m12, System.Double m20, System.Double m21, System.Double m22, System.Double m30, System.Double m31, System.Double m32)
  RVA=0x04DB42A0  token=0x6000926  System.Void .ctor(System.Double v)
  RVA=0x05F027A8  token=0x6000927  System.Void .ctor(System.Boolean v)
  RVA=0x05F026D0  token=0x6000928  System.Void .ctor(Unity.Mathematics.bool4x3 v)
  RVA=0x04DB43E0  token=0x6000929  System.Void .ctor(System.Int32 v)
  RVA=0x0B2832BC  token=0x600092A  System.Void .ctor(Unity.Mathematics.int4x3 v)
  RVA=0x05F02674  token=0x600092B  System.Void .ctor(System.UInt32 v)
  RVA=0x0B28334C  token=0x600092C  System.Void .ctor(Unity.Mathematics.uint4x3 v)
  RVA=0x04DB42F0  token=0x600092D  System.Void .ctor(System.Single v)
  RVA=0x0B28322C  token=0x600092E  System.Void .ctor(Unity.Mathematics.float4x3 v)
  RVA=0x0B24F55C  token=0x600092F  Unity.Mathematics.double4x3 op_Implicit(System.Int32 v)
  RVA=0x0B24F660  token=0x6000930  Unity.Mathematics.double4x3 op_Implicit(Unity.Mathematics.float4x3 v)
  RVA=0x0B2833DC  token=0x6000931  Unity.Mathematics.double4x3 op_Multiply(Unity.Mathematics.double4x3 lhs, Unity.Mathematics.double4x3 rhs)
  RVA=0x0B282ABC  token=0x6000932  System.Boolean Equals(Unity.Mathematics.double4x3 rhs)
  RVA=0x0B282B3C  token=0x6000933  System.Boolean Equals(System.Object o)
  RVA=0x0B282BDC  token=0x6000934  System.Int32 GetHashCode()
  RVA=0x0B282EFC  token=0x6000935  System.String ToString()
  RVA=0x0B282C28  token=0x6000936  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.double4x4
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x90
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.double4       c0  // 0x10
  public            Unity.Mathematics.double4       c1  // 0x30
  public            Unity.Mathematics.double4       c2  // 0x50
  public            Unity.Mathematics.double4       c3  // 0x70
  public    static readonly Unity.Mathematics.double4x4     identity  // static @ 0x0
  public    static readonly Unity.Mathematics.double4x4     zero  // static @ 0x80
METHODS:
  RVA=0x04DB4560  token=0x6000937  System.Void .ctor(Unity.Mathematics.double4 c0, Unity.Mathematics.double4 c1, Unity.Mathematics.double4 c2, Unity.Mathematics.double4 c3)
  RVA=0x04A30010  token=0x6000938  System.Void .ctor(System.Double m00, System.Double m01, System.Double m02, System.Double m03, System.Double m10, System.Double m11, System.Double m12, System.Double m13, System.Double m20, System.Double m21, System.Double m22, System.Double m23, System.Double m30, System.Double m31, System.Double m32, System.Double m33)
  RVA=0x04DB4500  token=0x6000939  System.Void .ctor(System.Double v)
  RVA=0x05F02960  token=0x600093A  System.Void .ctor(System.Boolean v)
  RVA=0x05F02A40  token=0x600093B  System.Void .ctor(Unity.Mathematics.bool4x4 v)
  RVA=0x04DB45B0  token=0x600093C  System.Void .ctor(System.Int32 v)
  RVA=0x0B283FA8  token=0x600093D  System.Void .ctor(Unity.Mathematics.int4x4 v)
  RVA=0x05F029C8  token=0x600093E  System.Void .ctor(System.UInt32 v)
  RVA=0x0B283EF4  token=0x600093F  System.Void .ctor(Unity.Mathematics.uint4x4 v)
  RVA=0x04DB4490  token=0x6000940  System.Void .ctor(System.Single v)
  RVA=0x0B283E40  token=0x6000941  System.Void .ctor(Unity.Mathematics.float4x4 v)
  RVA=0x0B24F808  token=0x6000942  Unity.Mathematics.double4x4 op_Implicit(Unity.Mathematics.float4x4 v)
  RVA=0x0B2834E4  token=0x6000943  System.Boolean Equals(Unity.Mathematics.double4x4 rhs)
  RVA=0x0B283588  token=0x6000944  System.Boolean Equals(System.Object o)
  RVA=0x0B283640  token=0x6000945  System.Int32 GetHashCode()
  RVA=0x0B283A30  token=0x6000946  System.String ToString()
  RVA=0x0B28369C  token=0x6000947  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04A2FEE0  token=0x6000948  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.float2
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public    static readonly Unity.Mathematics.float2        zero  // static @ 0x0
PROPERTIES:
  xyxy  get=0x04DB4750
  xy  get=0x04D889F0
  yx  get=0x04DB4770
METHODS:
  RVA=0x04D87A10  token=0x6000949  System.Void .ctor(System.Single x, System.Single y)
  RVA=0x04DB4680  token=0x600094A  System.Void .ctor(Unity.Mathematics.float2 xy)
  RVA=0x04DB4640  token=0x600094B  System.Void .ctor(System.Single v)
  RVA=0x0B284218  token=0x600094C  System.Void .ctor(System.Boolean v)
  RVA=0x05F02C08  token=0x600094D  System.Void .ctor(Unity.Mathematics.bool2 v)
  RVA=0x04DB46F0  token=0x600094E  System.Void .ctor(System.Int32 v)
  RVA=0x04DB46D0  token=0x600094F  System.Void .ctor(Unity.Mathematics.int2 v)
  RVA=0x04DB46A0  token=0x6000950  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB4650  token=0x6000951  System.Void .ctor(Unity.Mathematics.uint2 v)
  RVA=0x0B284234  token=0x6000952  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B284260  token=0x6000953  System.Void .ctor(Unity.Mathematics.half2 v)
  RVA=0x04DB4710  token=0x6000954  System.Void .ctor(System.Double v)
  RVA=0x04DB4730  token=0x6000955  System.Void .ctor(Unity.Mathematics.double2 v)
  RVA=0x04D8B720  token=0x6000956  Unity.Mathematics.float2 op_Implicit(System.Single v)
  RVA=0x04DAF4E0  token=0x6000957  Unity.Mathematics.float2 op_Implicit(System.Int32 v)
  RVA=0x04DAF500  token=0x6000958  Unity.Mathematics.float2 op_Implicit(Unity.Mathematics.int2 v)
  RVA=0x04DAF550  token=0x6000959  Unity.Mathematics.float2 op_Implicit(System.UInt32 v)
  RVA=0x04DAF520  token=0x600095A  Unity.Mathematics.float2 op_Implicit(Unity.Mathematics.uint2 v)
  RVA=0x04DAF580  token=0x600095B  Unity.Mathematics.float2 op_Explicit(System.Double v)
  RVA=0x04DAF4B0  token=0x600095C  Unity.Mathematics.float2 op_Explicit(Unity.Mathematics.double2 v)
  RVA=0x04DA8070  token=0x600095D  Unity.Mathematics.float2 op_Multiply(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DA8040  token=0x600095E  Unity.Mathematics.float2 op_Multiply(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x04DB47E0  token=0x600095F  Unity.Mathematics.float2 op_Multiply(System.Single lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DA7FD0  token=0x6000960  Unity.Mathematics.float2 op_Addition(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DA8010  token=0x6000961  Unity.Mathematics.float2 op_Addition(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x04DA80E0  token=0x6000962  Unity.Mathematics.float2 op_Subtraction(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DA80B0  token=0x6000963  Unity.Mathematics.float2 op_Subtraction(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x04DB4810  token=0x6000964  Unity.Mathematics.float2 op_Subtraction(System.Single lhs, Unity.Mathematics.float2 rhs)
  RVA=0x0AAEC45C  token=0x6000965  Unity.Mathematics.float2 op_Division(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x0AAEC948  token=0x6000966  Unity.Mathematics.float2 op_Division(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x0B284290  token=0x6000967  Unity.Mathematics.float2 op_Division(System.Single lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DB4790  token=0x6000968  Unity.Mathematics.float2 op_Modulus(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x0B284358  token=0x6000969  Unity.Mathematics.bool2 op_LessThan(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x0B284320  token=0x600096A  Unity.Mathematics.bool2 op_LessThanOrEqual(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x0B2842E8  token=0x600096B  Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.float2 lhs, Unity.Mathematics.float2 rhs)
  RVA=0x04DB4830  token=0x600096C  Unity.Mathematics.float2 op_UnaryNegation(Unity.Mathematics.float2 val)
  RVA=0x0B2842B0  token=0x600096D  Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.float2 lhs, System.Single rhs)
  RVA=0x0AAEC490  token=0x6000971  System.Boolean Equals(Unity.Mathematics.float2 rhs)
  RVA=0x0B28405C  token=0x6000972  System.Boolean Equals(System.Object o)
  RVA=0x0B2840CC  token=0x6000973  System.Int32 GetHashCode()
  RVA=0x0B284188  token=0x6000974  System.String ToString()
  RVA=0x0B284100  token=0x6000975  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04DA7CB0  token=0x6000976  UnityEngine.Vector2 op_Implicit(Unity.Mathematics.float2 v)
  RVA=0x04DA7CB0  token=0x6000977  Unity.Mathematics.float2 op_Implicit(UnityEngine.Vector2 v)
END_CLASS

CLASS: Unity.Mathematics.float2x2
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float2        c0  // 0x10
  public            Unity.Mathematics.float2        c1  // 0x18
  public    static readonly Unity.Mathematics.float2x2      identity  // static @ 0x0
METHODS:
  RVA=0x04D861B0  token=0x6000978  System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1)
  RVA=0x04DB4940  token=0x6000979  System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11)
  RVA=0x04DB48F0  token=0x600097A  System.Void .ctor(System.Single v)
  RVA=0x05F02CC0  token=0x600097B  System.Void .ctor(System.Boolean v)
  RVA=0x0B284700  token=0x600097C  System.Void .ctor(Unity.Mathematics.bool2x2 v)
  RVA=0x04DB4910  token=0x600097D  System.Void .ctor(System.Int32 v)
  RVA=0x04DB49A0  token=0x600097E  System.Void .ctor(Unity.Mathematics.int2x2 v)
  RVA=0x04DB49E0  token=0x600097F  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB4860  token=0x6000980  System.Void .ctor(Unity.Mathematics.uint2x2 v)
  RVA=0x04DB48C0  token=0x6000981  System.Void .ctor(System.Double v)
  RVA=0x04DB4960  token=0x6000982  System.Void .ctor(Unity.Mathematics.double2x2 v)
  RVA=0x0B28477C  token=0x6000983  Unity.Mathematics.float2x2 op_Multiply(Unity.Mathematics.float2x2 lhs, System.Single rhs)
  RVA=0x0B28437C  token=0x6000984  System.Boolean Equals(Unity.Mathematics.float2x2 rhs)
  RVA=0x0B2843B8  token=0x6000985  System.Boolean Equals(System.Object o)
  RVA=0x0B284424  token=0x6000986  System.Int32 GetHashCode()
  RVA=0x0B284594  token=0x6000987  System.String ToString()
  RVA=0x0B284440  token=0x6000988  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04D27400  token=0x6000989  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.float2x3
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float2        c0  // 0x10
  public            Unity.Mathematics.float2        c1  // 0x18
  public            Unity.Mathematics.float2        c2  // 0x20
METHODS:
  RVA=0x04D8C0D0  token=0x600098A  System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2)
  RVA=0x04DB4B80  token=0x600098B  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12)
  RVA=0x04DB4C00  token=0x600098C  System.Void .ctor(System.Single v)
  RVA=0x05F02E7C  token=0x600098D  System.Void .ctor(System.Boolean v)
  RVA=0x0B284C94  token=0x600098E  System.Void .ctor(Unity.Mathematics.bool2x3 v)
  RVA=0x04DB4C90  token=0x600098F  System.Void .ctor(System.Int32 v)
  RVA=0x04DB4B20  token=0x6000990  System.Void .ctor(Unity.Mathematics.int2x3 v)
  RVA=0x04DB4C20  token=0x6000991  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB4A90  token=0x6000992  System.Void .ctor(Unity.Mathematics.uint2x3 v)
  RVA=0x04DB4BB0  token=0x6000993  System.Void .ctor(System.Double v)
  RVA=0x04DB4A30  token=0x6000994  System.Void .ctor(Unity.Mathematics.double2x3 v)
  RVA=0x0B28485C  token=0x6000995  System.Boolean Equals(Unity.Mathematics.float2x3 rhs)
  RVA=0x0B2847E8  token=0x6000996  System.Boolean Equals(System.Object o)
  RVA=0x0B2848D8  token=0x6000997  System.Int32 GetHashCode()
  RVA=0x0B284900  token=0x6000998  System.String ToString()
  RVA=0x0B284AE0  token=0x6000999  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float2x4
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float2        c0  // 0x10
  public            Unity.Mathematics.float2        c1  // 0x18
  public            Unity.Mathematics.float2        c2  // 0x20
  public            Unity.Mathematics.float2        c3  // 0x28
METHODS:
  RVA=0x04D8BCB0  token=0x600099A  System.Void .ctor(Unity.Mathematics.float2 c0, Unity.Mathematics.float2 c1, Unity.Mathematics.float2 c2, Unity.Mathematics.float2 c3)
  RVA=0x04DB4CE0  token=0x600099B  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13)
  RVA=0x04DB4DA0  token=0x600099C  System.Void .ctor(System.Single v)
  RVA=0x05F02FFC  token=0x600099D  System.Void .ctor(System.Boolean v)
  RVA=0x0B2852D0  token=0x600099E  System.Void .ctor(Unity.Mathematics.bool2x4 v)
  RVA=0x04DB5010  token=0x600099F  System.Void .ctor(System.Int32 v)
  RVA=0x04DB4DC0  token=0x60009A0  System.Void .ctor(Unity.Mathematics.int2x4 v)
  RVA=0x04DB4F70  token=0x60009A1  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB4E40  token=0x60009A2  System.Void .ctor(Unity.Mathematics.uint2x4 v)
  RVA=0x04DB4F00  token=0x60009A3  System.Void .ctor(System.Double v)
  RVA=0x04DB4D30  token=0x60009A4  System.Void .ctor(Unity.Mathematics.double2x4 v)
  RVA=0x0B284D4C  token=0x60009A5  System.Boolean Equals(Unity.Mathematics.float2x4 rhs)
  RVA=0x0B284DD4  token=0x60009A6  System.Boolean Equals(System.Object o)
  RVA=0x0B284E48  token=0x60009A7  System.Int32 GetHashCode()
  RVA=0x0B285080  token=0x60009A8  System.String ToString()
  RVA=0x0B284E6C  token=0x60009A9  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float3
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public            System.Single                   z  // 0x18
  public    static readonly Unity.Mathematics.float3        zero  // static @ 0x0
PROPERTIES:
  xxxx  get=0x04DB5230
  xxxy  get=0x04DB5250
  xxxz  get=0x04DB5270
  xxyx  get=0x04DB52B0
  xxyy  get=0x04DB52D0
  xxyz  get=0x04DB52F0
  xxzx  get=0x04DB5330
  xxzy  get=0x04DB5350
  xxzz  get=0x04DB5370
  xyxx  get=0x04DB53B0
  xyxy  get=0x04DB4750
  xyxz  get=0x04DB53D0
  xyyx  get=0x04DB5410
  xyyy  get=0x04DB5430
  xyyz  get=0x04DB5450
  xyzx  get=0x04064E30
  xyzy  get=0x04DB5470
  xyzz  get=0x04DB5490
  xzxx  get=0x04DB54D0
  xzxy  get=0x04DB54F0
  xzxz  get=0x04DB5510
  xzyx  get=0x04DB5550
  xzyy  get=0x04DB5570
  xzyz  get=0x04DB5590
  xzzx  get=0x04DB55D0
  xzzy  get=0x04DB55F0
  xzzz  get=0x04DB5610
  yxxx  get=0x04DB5650
  yxxy  get=0x04DB5670
  yxxz  get=0x04DB5690
  yxyx  get=0x04DB56D0
  yxyy  get=0x04DB56F0
  yxyz  get=0x04DB5710
  yxzx  get=0x04DB5750
  yxzy  get=0x04DB5770
  yxzz  get=0x04DB5790
  yyxx  get=0x04DB57F0
  yyxy  get=0x04DB5810
  yyxz  get=0x04DB5830
  yyyx  get=0x04DB5860
  yyyy  get=0x04DB5880
  yyyz  get=0x04DB58A0
  yyzx  get=0x04DB58E0
  yyzy  get=0x04DB5900
  yyzz  get=0x04DB5920
  yzxx  get=0x04DB5960
  yzxy  get=0x04064E10
  yzxz  get=0x04064DF0
  yzyx  get=0x04DB59A0
  yzyy  get=0x04DB59C0
  yzyz  get=0x04DB59E0
  yzzx  get=0x04DB5A20
  yzzy  get=0x04DB5A40
  yzzz  get=0x04DB5A60
  zxxx  get=0x04DB5AC0
  zxxy  get=0x04DB5AE0
  zxxz  get=0x04DB5B00
  zxyx  get=0x04DB5B40
  zxyy  get=0x040B34F0
  zxyz  get=0x04064DD0
  zxzx  get=0x04DB5B80
  zxzy  get=0x04DB5BA0
  zxzz  get=0x04DB5BC0
  zyxx  get=0x04DB5C20
  zyxy  get=0x04DB5C40
  zyxz  get=0x04DB5C60
  zyyx  get=0x04DB5CA0
  zyyy  get=0x04DB5CC0
  zyyz  get=0x04DB5CE0
  zyzx  get=0x04DB5D20
  zyzy  get=0x04DB5D40
  zyzz  get=0x04DB5D60
  zzxx  get=0x04DB5DC0
  zzxy  get=0x04DB5DE0
  zzxz  get=0x04DB5E00
  zzyx  get=0x04DB5E40
  zzyy  get=0x04DB5E60
  zzyz  get=0x04DB5E80
  zzzx  get=0x04DB5EB0
  zzzy  get=0x04DB5ED0
  zzzz  get=0x04DB5EF0
  xxx  get=0x04DB5220
  xxy  get=0x04DB5290
  xxz  get=0x04DB5310
  xyx  get=0x04DB5390
  xyy  get=0x04DB53F0
  xyz  get=0x04D88970  set=0x04D88F40
  xzx  get=0x04DB54B0
  xzy  get=0x04DB5530  set=0x04DB60E0
  xzz  get=0x04DB55B0
  yxx  get=0x04DB5630
  yxy  get=0x04DB56B0
  yxz  get=0x04DB5730  set=0x04DB6120
  yyx  get=0x04DB57D0
  yyy  get=0x04DB5850
  yyz  get=0x04DB58C0
  yzx  get=0x04DB5940  set=0x04DB6160
  yzy  get=0x04DB5980
  yzz  get=0x04DB5A00
  zxx  get=0x04DB5AA0
  zxy  get=0x04DB5B20  set=0x04DB61A0
  zxz  get=0x04DB5B60
  zyx  get=0x04DB5C00  set=0x04DB61E0
  zyy  get=0x04DB5C80
  zyz  get=0x04DB5D00
  zzx  get=0x04DB5DA0
  zzy  get=0x04DB5E20
  zzz  get=0x04DB5EA0
  xx  get=0x04DB5210
  xy  get=0x04D889F0  set=0x04DB4680
  xz  get=0x04D88A40  set=0x04DB60C0
  yx  get=0x04DB4770  set=0x04DB6100
  yy  get=0x04DB57B0
  yz  get=0x04D8B880  set=0x04DB6140
  zx  get=0x04DB5A80  set=0x04DB6180
  zy  get=0x04DB5BE0  set=0x04DB61C0
  zz  get=0x04DB5D80
  Item  get=0x04DB5200  set=0x04DB60B0
METHODS:
  RVA=0x04D88F30  token=0x60009AA  System.Void .ctor(System.Single x, System.Single y, System.Single z)
  RVA=0x04DB51B0  token=0x60009AB  System.Void .ctor(System.Single x, Unity.Mathematics.float2 yz)
  RVA=0x04DB50E0  token=0x60009AC  System.Void .ctor(Unity.Mathematics.float2 xy, System.Single z)
  RVA=0x04D88F40  token=0x60009AD  System.Void .ctor(Unity.Mathematics.float3 xyz)
  RVA=0x04DB5170  token=0x60009AE  System.Void .ctor(System.Single v)
  RVA=0x05F0316C  token=0x60009AF  System.Void .ctor(System.Boolean v)
  RVA=0x05F031A0  token=0x60009B0  System.Void .ctor(Unity.Mathematics.bool3 v)
  RVA=0x04DB5070  token=0x60009B1  System.Void .ctor(System.Int32 v)
  RVA=0x04DB5180  token=0x60009B2  System.Void .ctor(Unity.Mathematics.int3 v)
  RVA=0x04DB5100  token=0x60009B3  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB50A0  token=0x60009B4  System.Void .ctor(Unity.Mathematics.uint3 v)
  RVA=0x0B285610  token=0x60009B5  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B2855D4  token=0x60009B6  System.Void .ctor(Unity.Mathematics.half3 v)
  RVA=0x04DB5140  token=0x60009B7  System.Void .ctor(System.Double v)
  RVA=0x04DB51D0  token=0x60009B8  System.Void .ctor(Unity.Mathematics.double3 v)
  RVA=0x04D8B860  token=0x60009B9  Unity.Mathematics.float3 op_Implicit(System.Single v)
  RVA=0x0B251E8C  token=0x60009BA  Unity.Mathematics.float3 op_Explicit(System.Boolean v)
  RVA=0x0B251F30  token=0x60009BB  Unity.Mathematics.float3 op_Explicit(Unity.Mathematics.bool3 v)
  RVA=0x04DAF610  token=0x60009BC  Unity.Mathematics.float3 op_Implicit(System.Int32 v)
  RVA=0x04DAF7A0  token=0x60009BD  Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.int3 v)
  RVA=0x04DAF760  token=0x60009BE  Unity.Mathematics.float3 op_Implicit(System.UInt32 v)
  RVA=0x04DAF6D0  token=0x60009BF  Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.uint3 v)
  RVA=0x0B251EA8  token=0x60009C0  Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.half v)
  RVA=0x0B251EE4  token=0x60009C1  Unity.Mathematics.float3 op_Implicit(Unity.Mathematics.half3 v)
  RVA=0x04DAF640  token=0x60009C2  Unity.Mathematics.float3 op_Explicit(System.Double v)
  RVA=0x04DAF5D0  token=0x60009C3  Unity.Mathematics.float3 op_Explicit(Unity.Mathematics.double3 v)
  RVA=0x04D886A0  token=0x60009C4  Unity.Mathematics.float3 op_Multiply(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x030E7A60  token=0x60009C5  Unity.Mathematics.float3 op_Multiply(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x030E9530  token=0x60009C6  Unity.Mathematics.float3 op_Multiply(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x030E9EA0  token=0x60009C7  Unity.Mathematics.float3 op_Addition(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x04118DE0  token=0x60009C8  Unity.Mathematics.float3 op_Addition(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x04DB5F10  token=0x60009C9  Unity.Mathematics.float3 op_Addition(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x030E9560  token=0x60009CA  Unity.Mathematics.float3 op_Subtraction(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x04118E10  token=0x60009CB  Unity.Mathematics.float3 op_Subtraction(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x04DB6080  token=0x60009CC  Unity.Mathematics.float3 op_Subtraction(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0AAED088  token=0x60009CD  Unity.Mathematics.float3 op_Division(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0AADD2FC  token=0x60009CE  Unity.Mathematics.float3 op_Division(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B28564C  token=0x60009CF  Unity.Mathematics.float3 op_Division(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x04DAFC00  token=0x60009D0  Unity.Mathematics.float3 op_Modulus(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x04DB6020  token=0x60009D1  Unity.Mathematics.float3 op_Modulus(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x04DB5FC0  token=0x60009D2  Unity.Mathematics.float3 op_Modulus(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x04DB5F80  token=0x60009D3  Unity.Mathematics.float3 op_Increment(Unity.Mathematics.float3 val)
  RVA=0x04DB5F40  token=0x60009D4  Unity.Mathematics.float3 op_Decrement(Unity.Mathematics.float3 val)
  RVA=0x0B28598C  token=0x60009D5  Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B2859C0  token=0x60009D6  Unity.Mathematics.bool3 op_LessThan(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B28595C  token=0x60009D7  Unity.Mathematics.bool3 op_LessThan(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B2858C8  token=0x60009D8  Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B285934  token=0x60009D9  Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B285900  token=0x60009DA  Unity.Mathematics.bool3 op_LessThanOrEqual(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B285788  token=0x60009DB  Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B2857E0  token=0x60009DC  Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B2857BC  token=0x60009DD  Unity.Mathematics.bool3 op_GreaterThan(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B2856FC  token=0x60009DE  Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B285730  token=0x60009DF  Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B285760  token=0x60009E0  Unity.Mathematics.bool3 op_GreaterThanOrEqual(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x03F48480  token=0x60009E1  Unity.Mathematics.float3 op_UnaryNegation(Unity.Mathematics.float3 val)
  RVA=0x04D88970  token=0x60009E2  Unity.Mathematics.float3 op_UnaryPlus(Unity.Mathematics.float3 val)
  RVA=0x0B285678  token=0x60009E3  Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x05F031DC  token=0x60009E4  Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B2856C0  token=0x60009E5  Unity.Mathematics.bool3 op_Equality(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B285844  token=0x60009E6  Unity.Mathematics.bool3 op_Inequality(Unity.Mathematics.float3 lhs, Unity.Mathematics.float3 rhs)
  RVA=0x0B285888  token=0x60009E7  Unity.Mathematics.bool3 op_Inequality(Unity.Mathematics.float3 lhs, System.Single rhs)
  RVA=0x0B28580C  token=0x60009E8  Unity.Mathematics.bool3 op_Inequality(System.Single lhs, Unity.Mathematics.float3 rhs)
  RVA=0x030F9620  token=0x6000A6C  System.Boolean Equals(Unity.Mathematics.float3 rhs)
  RVA=0x0B2853CC  token=0x6000A6D  System.Boolean Equals(System.Object o)
  RVA=0x0B285440  token=0x6000A6E  System.Int32 GetHashCode()
  RVA=0x0B285464  token=0x6000A6F  System.String ToString()
  RVA=0x0B285524  token=0x6000A70  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04D88970  token=0x6000A71  UnityEngine.Vector3 op_Implicit(Unity.Mathematics.float3 v)
  RVA=0x04D88970  token=0x6000A72  Unity.Mathematics.float3 op_Implicit(UnityEngine.Vector3 v)
END_CLASS

CLASS: Unity.Mathematics.float3x2
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float3        c0  // 0x10
  public            Unity.Mathematics.float3        c1  // 0x1c
METHODS:
  RVA=0x04D871E0  token=0x6000A73  System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1)
  RVA=0x04DB6270  token=0x6000A74  System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21)
  RVA=0x04DB6250  token=0x6000A75  System.Void .ctor(System.Single v)
  RVA=0x05F032D0  token=0x6000A76  System.Void .ctor(System.Boolean v)
  RVA=0x0B28B60C  token=0x6000A77  System.Void .ctor(Unity.Mathematics.bool3x2 v)
  RVA=0x04DB6200  token=0x6000A78  System.Void .ctor(System.Int32 v)
  RVA=0x05F03470  token=0x6000A79  System.Void .ctor(Unity.Mathematics.int3x2 v)
  RVA=0x04DB62A0  token=0x6000A7A  System.Void .ctor(System.UInt32 v)
  RVA=0x05F033FC  token=0x6000A7B  System.Void .ctor(Unity.Mathematics.uint3x2 v)
  RVA=0x04DB6310  token=0x6000A7C  System.Void .ctor(System.Double v)
  RVA=0x0B28B594  token=0x6000A7D  System.Void .ctor(Unity.Mathematics.double3x2 v)
  RVA=0x0B28B6DC  token=0x6000A7E  Unity.Mathematics.float3x2 op_Multiply(Unity.Mathematics.float3x2 lhs, System.Single rhs)
  RVA=0x0B28B174  token=0x6000A7F  System.Boolean Equals(Unity.Mathematics.float3x2 rhs)
  RVA=0x0B28B100  token=0x6000A80  System.Boolean Equals(System.Object o)
  RVA=0x0B28B1D8  token=0x6000A81  System.Int32 GetHashCode()
  RVA=0x0B28B3B4  token=0x6000A82  System.String ToString()
  RVA=0x0B28B200  token=0x6000A83  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float3x3
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x34
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float3        c0  // 0x10
  public            Unity.Mathematics.float3        c1  // 0x1c
  public            Unity.Mathematics.float3        c2  // 0x28
  public    static readonly Unity.Mathematics.float3x3      identity  // static @ 0x0
  public    static readonly Unity.Mathematics.float3x3      zero  // static @ 0x24
PROPERTIES:
  Item  get=0x04DB6580
METHODS:
  RVA=0x04D8C690  token=0x6000A84  System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2)
  RVA=0x04DB63D0  token=0x6000A85  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22)
  RVA=0x04DB64E0  token=0x6000A86  System.Void .ctor(System.Single v)
  RVA=0x05F03630  token=0x6000A87  System.Void .ctor(System.Boolean v)
  RVA=0x0B28BE08  token=0x6000A88  System.Void .ctor(Unity.Mathematics.bool3x3 v)
  RVA=0x04DB6360  token=0x6000A89  System.Void .ctor(System.Int32 v)
  RVA=0x05F034E4  token=0x6000A8A  System.Void .ctor(Unity.Mathematics.int3x3 v)
  RVA=0x04DB6430  token=0x6000A8B  System.Void .ctor(System.UInt32 v)
  RVA=0x05F036AC  token=0x6000A8C  System.Void .ctor(Unity.Mathematics.uint3x3 v)
  RVA=0x04DB6510  token=0x6000A8D  System.Void .ctor(System.Double v)
  RVA=0x0B28BD60  token=0x6000A8E  System.Void .ctor(Unity.Mathematics.double3x3 v)
  RVA=0x0B252214  token=0x6000A8F  Unity.Mathematics.float3x3 op_Implicit(System.Int32 v)
  RVA=0x0B28C02C  token=0x6000A90  Unity.Mathematics.float3x3 op_Multiply(Unity.Mathematics.float3x3 lhs, System.Single rhs)
  RVA=0x0B28BF2C  token=0x6000A91  Unity.Mathematics.float3x3 op_Addition(Unity.Mathematics.float3x3 lhs, Unity.Mathematics.float3x3 rhs)
  RVA=0x0B28B7DC  token=0x6000A93  System.Boolean Equals(Unity.Mathematics.float3x3 rhs)
  RVA=0x0B28B760  token=0x6000A94  System.Boolean Equals(System.Object o)
  RVA=0x0B28B868  token=0x6000A95  System.Int32 GetHashCode()
  RVA=0x0B28B894  token=0x6000A96  System.String ToString()
  RVA=0x0B28BB1C  token=0x6000A97  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x034C93C0  token=0x6000A98  System.Void .ctor(Unity.Mathematics.float4x4 f4x4)
  RVA=0x030E9010  token=0x6000A99  System.Void .ctor(Unity.Mathematics.quaternion q)
  RVA=0x04CA6B90  token=0x6000A9A  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.float3x4
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float3        c0  // 0x10
  public            Unity.Mathematics.float3        c1  // 0x1c
  public            Unity.Mathematics.float3        c2  // 0x28
  public            Unity.Mathematics.float3        c3  // 0x34
  public    static readonly Unity.Mathematics.float3x4      zero  // static @ 0x0
PROPERTIES:
  Item  get=0x04DB6580
METHODS:
  RVA=0x04DB65D0  token=0x6000A9B  System.Void .ctor(Unity.Mathematics.float3 c0, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3 c3)
  RVA=0x04DB6820  token=0x6000A9C  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23)
  RVA=0x04DB6590  token=0x6000A9D  System.Void .ctor(System.Single v)
  RVA=0x05F0394C  token=0x6000A9E  System.Void .ctor(System.Boolean v)
  RVA=0x0B28C934  token=0x6000A9F  System.Void .ctor(Unity.Mathematics.bool3x4 v)
  RVA=0x04DB6790  token=0x6000AA0  System.Void .ctor(System.Int32 v)
  RVA=0x05F03878  token=0x6000AA1  System.Void .ctor(Unity.Mathematics.int3x4 v)
  RVA=0x04DB66B0  token=0x6000AA2  System.Void .ctor(System.UInt32 v)
  RVA=0x05F039EC  token=0x6000AA3  System.Void .ctor(Unity.Mathematics.uint3x4 v)
  RVA=0x04DB6620  token=0x6000AA4  System.Void .ctor(System.Double v)
  RVA=0x0B28C858  token=0x6000AA5  System.Void .ctor(Unity.Mathematics.double3x4 v)
  RVA=0x0B25243C  token=0x6000AA6  Unity.Mathematics.float3x4 op_Implicit(System.Int32 v)
  RVA=0x0B28C160  token=0x6000AA8  System.Boolean Equals(Unity.Mathematics.float3x4 rhs)
  RVA=0x0B28C0E4  token=0x6000AA9  System.Boolean Equals(System.Object o)
  RVA=0x0B28C224  token=0x6000AAA  System.Int32 GetHashCode()
  RVA=0x0B28C528  token=0x6000AAB  System.String ToString()
  RVA=0x0B28C254  token=0x6000AAC  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float4
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public            System.Single                   z  // 0x18
  public            System.Single                   w  // 0x1c
  public    static readonly Unity.Mathematics.float4        zero  // static @ 0x0
PROPERTIES:
  xxzz  get=0x04DB5370
  xyzx  get=0x04064E30
  xzxz  get=0x04DB5510
  yyww  get=0x04DB6BD0
  yzxy  get=0x04064E10
  yzxz  get=0x04064DF0
  yzxw  get=0x04DB6BF0
  zxyy  get=0x040B34F0
  zxyz  get=0x04064DD0
  zwxy  get=0x034C98E0
  wzyx  get=0x034C98C0
  wwwx  get=0x04DB6B10
  wwww  get=0x04DB6AF0
  xxx  get=0x04DB5220
  xyz  get=0x04D88970  set=0x04D88F40
  xyw  get=0x04DB6B70
  yxw  get=0x04DB6BB0
  yzx  get=0x04DB5940
  zwx  get=0x04DB6C10
  wzy  get=0x04DB6B50
  www  get=0x04DB6AE0
  xx  get=0x04DB5210
  xy  get=0x04D889F0  set=0x04DB4680
  xz  get=0x04D88A40
  yy  get=0x04DB57B0
  yw  get=0x04DB6B90
  zw  get=0x04D9C5E0
  wz  get=0x04DB6B30
  Item  get=0x04DB5200  set=0x04DB60B0
METHODS:
  RVA=0x04D88AD0  token=0x6000AAD  System.Void .ctor(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x04DB69F0  token=0x6000AAE  System.Void .ctor(System.Single x, System.Single y, Unity.Mathematics.float2 zw)
  RVA=0x04DB6A90  token=0x6000AAF  System.Void .ctor(System.Single x, Unity.Mathematics.float2 yz, System.Single w)
  RVA=0x04DB6AC0  token=0x6000AB0  System.Void .ctor(System.Single x, Unity.Mathematics.float3 yzw)
  RVA=0x04DB69A0  token=0x6000AB1  System.Void .ctor(Unity.Mathematics.float2 xy, System.Single z, System.Single w)
  RVA=0x04DB68A0  token=0x6000AB2  System.Void .ctor(Unity.Mathematics.float2 xy, Unity.Mathematics.float2 zw)
  RVA=0x04DB6980  token=0x6000AB3  System.Void .ctor(Unity.Mathematics.float3 xyz, System.Single w)
  RVA=0x04D92B90  token=0x6000AB4  System.Void .ctor(Unity.Mathematics.float4 xyzw)
  RVA=0x04DB6930  token=0x6000AB5  System.Void .ctor(System.Single v)
  RVA=0x05F03D18  token=0x6000AB6  System.Void .ctor(System.Boolean v)
  RVA=0x05F03D5C  token=0x6000AB7  System.Void .ctor(Unity.Mathematics.bool4 v)
  RVA=0x04DB6A20  token=0x6000AB8  System.Void .ctor(System.Int32 v)
  RVA=0x04DB6A80  token=0x6000AB9  System.Void .ctor(Unity.Mathematics.int4 v)
  RVA=0x04DB68E0  token=0x6000ABA  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB6A30  token=0x6000ABB  System.Void .ctor(Unity.Mathematics.uint4 v)
  RVA=0x0B28CE30  token=0x6000ABC  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B28CDD8  token=0x6000ABD  System.Void .ctor(Unity.Mathematics.half4 v)
  RVA=0x04DB69D0  token=0x6000ABE  System.Void .ctor(System.Double v)
  RVA=0x04DB6940  token=0x6000ABF  System.Void .ctor(Unity.Mathematics.double4 v)
  RVA=0x04DAF890  token=0x6000AC0  Unity.Mathematics.float4 op_Implicit(System.Single v)
  RVA=0x04DAF920  token=0x6000AC1  Unity.Mathematics.float4 op_Implicit(System.Int32 v)
  RVA=0x0B252674  token=0x6000AC2  Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.int4 v)
  RVA=0x04DAF970  token=0x6000AC3  Unity.Mathematics.float4 op_Implicit(System.UInt32 v)
  RVA=0x0B2526D0  token=0x6000AC4  Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.uint4 v)
  RVA=0x0B2525CC  token=0x6000AC5  Unity.Mathematics.float4 op_Implicit(Unity.Mathematics.half4 v)
  RVA=0x04DAF900  token=0x6000AC6  Unity.Mathematics.float4 op_Explicit(System.Double v)
  RVA=0x0B252584  token=0x6000AC7  Unity.Mathematics.float4 op_Explicit(Unity.Mathematics.double4 v)
  RVA=0x030E8750  token=0x6000AC8  Unity.Mathematics.float4 op_Multiply(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x0308B480  token=0x6000AC9  Unity.Mathematics.float4 op_Multiply(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x034C9970  token=0x6000ACA  Unity.Mathematics.float4 op_Multiply(System.Single lhs, Unity.Mathematics.float4 rhs)
  RVA=0x030E87A0  token=0x6000ACB  Unity.Mathematics.float4 op_Addition(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x04DA8140  token=0x6000ACC  Unity.Mathematics.float4 op_Addition(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x03F5AF20  token=0x6000ACD  Unity.Mathematics.float4 op_Subtraction(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x04DA8160  token=0x6000ACE  Unity.Mathematics.float4 op_Subtraction(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x04DB6C30  token=0x6000ACF  Unity.Mathematics.float4 op_Subtraction(System.Single lhs, Unity.Mathematics.float4 rhs)
  RVA=0x0AAED7A8  token=0x6000AD0  Unity.Mathematics.float4 op_Division(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x0AAED7EC  token=0x6000AD1  Unity.Mathematics.float4 op_Division(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x0B28CE78  token=0x6000AD2  Unity.Mathematics.float4 op_Division(System.Single lhs, Unity.Mathematics.float4 rhs)
  RVA=0x0B28CF94  token=0x6000AD3  Unity.Mathematics.bool4 op_LessThan(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x0B28CF58  token=0x6000AD4  Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x0B28CF14  token=0x6000AD5  Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x04DB6C70  token=0x6000AD6  Unity.Mathematics.float4 op_UnaryNegation(Unity.Mathematics.float4 val)
  RVA=0x0B28CEB0  token=0x6000AD7  Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.float4 lhs, Unity.Mathematics.float4 rhs)
  RVA=0x05F03DB0  token=0x6000AD8  Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.float4 lhs, System.Single rhs)
  RVA=0x0415FC30  token=0x6000AF9  System.Boolean Equals(Unity.Mathematics.float4 rhs)
  RVA=0x0B28CAAC  token=0x6000AFA  System.Boolean Equals(System.Object o)
  RVA=0x0B256480  token=0x6000AFB  System.Int32 GetHashCode()
  RVA=0x0B28CB18  token=0x6000AFC  System.String ToString()
  RVA=0x0B28CC84  token=0x6000AFD  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x0328FB00  token=0x6000AFE  Unity.Mathematics.float4 op_Implicit(UnityEngine.Vector4 v)
  RVA=0x0328FB00  token=0x6000AFF  UnityEngine.Vector4 op_Implicit(Unity.Mathematics.float4 v)
END_CLASS

CLASS: Unity.Mathematics.float4x2
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float4        c0  // 0x10
  public            Unity.Mathematics.float4        c1  // 0x20
METHODS:
  RVA=0x04D8C810  token=0x6000B00  System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1)
  RVA=0x04DB6D40  token=0x6000B01  System.Void .ctor(System.Single m00, System.Single m01, System.Single m10, System.Single m11, System.Single m20, System.Single m21, System.Single m30, System.Single m31)
  RVA=0x04DB6D20  token=0x6000B02  System.Void .ctor(System.Single v)
  RVA=0x05F04084  token=0x6000B03  System.Void .ctor(System.Boolean v)
  RVA=0x05F03EBC  token=0x6000B04  System.Void .ctor(Unity.Mathematics.bool4x2 v)
  RVA=0x03BACC20  token=0x6000B05  System.Void .ctor(System.Int32 v)
  RVA=0x0B28D554  token=0x6000B06  System.Void .ctor(Unity.Mathematics.int4x2 v)
  RVA=0x05F04054  token=0x6000B07  System.Void .ctor(System.UInt32 v)
  RVA=0x0B28D500  token=0x6000B08  System.Void .ctor(Unity.Mathematics.uint4x2 v)
  RVA=0x04DB6C90  token=0x6000B09  System.Void .ctor(System.Double v)
  RVA=0x0B28D5F8  token=0x6000B0A  System.Void .ctor(Unity.Mathematics.double4x2 v)
  RVA=0x0B252804  token=0x6000B0B  Unity.Mathematics.float4x2 op_Implicit(System.Int32 v)
  RVA=0x0B28CFBC  token=0x6000B0C  System.Boolean Equals(Unity.Mathematics.float4x2 rhs)
  RVA=0x0B28D004  token=0x6000B0D  System.Boolean Equals(System.Object o)
  RVA=0x0B28D078  token=0x6000B0E  System.Int32 GetHashCode()
  RVA=0x0B28D2B0  token=0x6000B0F  System.String ToString()
  RVA=0x0B28D09C  token=0x6000B10  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float4x3
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float4        c0  // 0x10
  public            Unity.Mathematics.float4        c1  // 0x20
  public            Unity.Mathematics.float4        c2  // 0x30
METHODS:
  RVA=0x04DA0760  token=0x6000B11  System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2)
  RVA=0x04DB6ED0  token=0x6000B12  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m10, System.Single m11, System.Single m12, System.Single m20, System.Single m21, System.Single m22, System.Single m30, System.Single m31, System.Single m32)
  RVA=0x04DB6E70  token=0x6000B13  System.Void .ctor(System.Single v)
  RVA=0x05F04158  token=0x6000B14  System.Void .ctor(System.Boolean v)
  RVA=0x05F040A8  token=0x6000B15  System.Void .ctor(Unity.Mathematics.bool4x3 v)
  RVA=0x04DB6EA0  token=0x6000B16  System.Void .ctor(System.Int32 v)
  RVA=0x0B28DD6C  token=0x6000B17  System.Void .ctor(Unity.Mathematics.int4x3 v)
  RVA=0x05F04394  token=0x6000B18  System.Void .ctor(System.UInt32 v)
  RVA=0x0B28DE5C  token=0x6000B19  System.Void .ctor(Unity.Mathematics.uint4x3 v)
  RVA=0x04DB6DB0  token=0x6000B1A  System.Void .ctor(System.Double v)
  RVA=0x0B28DED4  token=0x6000B1B  System.Void .ctor(Unity.Mathematics.double4x3 v)
  RVA=0x0B2529E4  token=0x6000B1C  Unity.Mathematics.float4x3 op_Implicit(System.Int32 v)
  RVA=0x0B28E01C  token=0x6000B1D  Unity.Mathematics.float4x3 op_Multiply(Unity.Mathematics.float4x3 lhs, Unity.Mathematics.float4x3 rhs)
  RVA=0x0B28E0D8  token=0x6000B1E  Unity.Mathematics.float4x3 op_Multiply(Unity.Mathematics.float4x3 lhs, System.Single rhs)
  RVA=0x0B28DF60  token=0x6000B1F  Unity.Mathematics.float4x3 op_Addition(Unity.Mathematics.float4x3 lhs, Unity.Mathematics.float4x3 rhs)
  RVA=0x0B28D6D8  token=0x6000B20  System.Boolean Equals(Unity.Mathematics.float4x3 rhs)
  RVA=0x0B28D65C  token=0x6000B21  System.Boolean Equals(System.Object o)
  RVA=0x0B28D738  token=0x6000B22  System.Int32 GetHashCode()
  RVA=0x0B28DA3C  token=0x6000B23  System.String ToString()
  RVA=0x0B28D768  token=0x6000B24  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.float4x4
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x50
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float4        c0  // 0x10
  public            Unity.Mathematics.float4        c1  // 0x20
  public            Unity.Mathematics.float4        c2  // 0x30
  public            Unity.Mathematics.float4        c3  // 0x40
  public    static readonly Unity.Mathematics.float4x4      identity  // static @ 0x0
  public    static readonly Unity.Mathematics.float4x4      zero  // static @ 0x40
PROPERTIES:
  Item  get=0x04DB7090
METHODS:
  RVA=0x04DB3220  token=0x6000B25  System.Void .ctor(Unity.Mathematics.float4 c0, Unity.Mathematics.float4 c1, Unity.Mathematics.float4 c2, Unity.Mathematics.float4 c3)
  RVA=0x033A3ED0  token=0x6000B26  System.Void .ctor(System.Single m00, System.Single m01, System.Single m02, System.Single m03, System.Single m10, System.Single m11, System.Single m12, System.Single m13, System.Single m20, System.Single m21, System.Single m22, System.Single m23, System.Single m30, System.Single m31, System.Single m32, System.Single m33)
  RVA=0x0308AEF0  token=0x6000B27  System.Void .ctor(System.Single v)
  RVA=0x05F044B8  token=0x6000B28  System.Void .ctor(System.Boolean v)
  RVA=0x05F043D8  token=0x6000B29  System.Void .ctor(Unity.Mathematics.bool4x4 v)
  RVA=0x0308BCF0  token=0x6000B2A  System.Void .ctor(System.Int32 v)
  RVA=0x0B28F11C  token=0x6000B2B  System.Void .ctor(Unity.Mathematics.int4x4 v)
  RVA=0x05F045B4  token=0x6000B2C  System.Void .ctor(System.UInt32 v)
  RVA=0x0B28F084  token=0x6000B2D  System.Void .ctor(Unity.Mathematics.uint4x4 v)
  RVA=0x04DB6F90  token=0x6000B2E  System.Void .ctor(System.Double v)
  RVA=0x0B28EFCC  token=0x6000B2F  System.Void .ctor(Unity.Mathematics.double4x4 v)
  RVA=0x0308AEC0  token=0x6000B30  Unity.Mathematics.float4x4 op_Implicit(System.Single v)
  RVA=0x0B252EE4  token=0x6000B31  Unity.Mathematics.float4x4 op_Implicit(System.Int32 v)
  RVA=0x0B28F440  token=0x6000B32  Unity.Mathematics.float4x4 op_Multiply(Unity.Mathematics.float4x4 lhs, System.Single rhs)
  RVA=0x0B28F254  token=0x6000B33  Unity.Mathematics.bool4x4 op_Equality(Unity.Mathematics.float4x4 lhs, Unity.Mathematics.float4x4 rhs)
  RVA=0x0B28E164  token=0x6000B35  System.Boolean Equals(Unity.Mathematics.float4x4 rhs)
  RVA=0x0B28E1E0  token=0x6000B36  System.Boolean Equals(System.Object o)
  RVA=0x0B28E264  token=0x6000B37  System.Int32 GetHashCode()
  RVA=0x0B28E540  token=0x6000B38  System.String ToString()
  RVA=0x0B28E950  token=0x6000B39  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x0B28F328  token=0x6000B3A  Unity.Mathematics.float4x4 op_Implicit(UnityEngine.Matrix4x4 m)
  RVA=0x0334D940  token=0x6000B3B  UnityEngine.Matrix4x4 op_Implicit(Unity.Mathematics.float4x4 m)
  RVA=0x0B28ECE4  token=0x6000B3C  System.Void .ctor(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B28EDA4  token=0x6000B3D  System.Void .ctor(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B28EEA8  token=0x6000B3E  System.Void .ctor(Unity.Mathematics.RigidTransform transform)
  RVA=0x0B28E29C  token=0x6000B3F  Unity.Mathematics.float4x4 Ortho(System.Single width, System.Single height, System.Single near, System.Single far)
  RVA=0x0B28E388  token=0x6000B40  Unity.Mathematics.float4x4 TRS(Unity.Mathematics.float3 translation, Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 scale)
  RVA=0x04CC6270  token=0x6000B41  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.half
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x12
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.UInt16                   value  // 0x10
  public    static readonly Unity.Mathematics.half          zero  // static @ 0x0
METHODS:
  RVA=0x04D87160  token=0x6000B42  System.Void .ctor(Unity.Mathematics.half x)
  RVA=0x0B28FFF0  token=0x6000B43  System.Void .ctor(System.Single v)
  RVA=0x0B28FFD4  token=0x6000B44  System.Void .ctor(System.Double v)
  RVA=0x0B253650  token=0x6000B45  Unity.Mathematics.half op_Explicit(System.Single v)
  RVA=0x0B253644  token=0x6000B46  Unity.Mathematics.half op_Explicit(System.Double v)
  RVA=0x0B290028  token=0x6000B47  System.Single op_Implicit(Unity.Mathematics.half d)
  RVA=0x0B290010  token=0x6000B48  System.Double op_Implicit(Unity.Mathematics.half d)
  RVA=0x0B290008  token=0x6000B49  System.Boolean op_Equality(Unity.Mathematics.half lhs, Unity.Mathematics.half rhs)
  RVA=0x03F4C8B0  token=0x6000B4A  System.Boolean Equals(Unity.Mathematics.half rhs)
  RVA=0x0B28FF1C  token=0x6000B4B  System.Boolean Equals(System.Object o)
  RVA=0x04D87110  token=0x6000B4C  System.Int32 GetHashCode()
  RVA=0x0B28FF7C  token=0x6000B4D  System.String ToString()
  RVA=0x0B28FFA0  token=0x6000B4E  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.half2
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.half          x  // 0x10
  public            Unity.Mathematics.half          y  // 0x12
METHODS:
  RVA=0x04DA4480  token=0x6000B4F  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y)
  RVA=0x04DB70B0  token=0x6000B50  System.Void .ctor(Unity.Mathematics.half2 xy)
  RVA=0x04DB70A0  token=0x6000B51  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B28F708  token=0x6000B52  System.Void .ctor(System.Single v)
  RVA=0x0B28F730  token=0x6000B53  System.Void .ctor(Unity.Mathematics.float2 v)
  RVA=0x0B28F6D8  token=0x6000B54  System.Void .ctor(System.Double v)
  RVA=0x0B28F760  token=0x6000B55  System.Void .ctor(Unity.Mathematics.double2 v)
  RVA=0x0B2533B0  token=0x6000B56  Unity.Mathematics.half2 op_Explicit(Unity.Mathematics.float2 v)
  RVA=0x0B28F56C  token=0x6000B57  System.Boolean Equals(Unity.Mathematics.half2 rhs)
  RVA=0x0B28F4F8  token=0x6000B58  System.Boolean Equals(System.Object o)
  RVA=0x0B28F580  token=0x6000B59  System.Int32 GetHashCode()
  RVA=0x0B28F64C  token=0x6000B5A  System.String ToString()
  RVA=0x0B28F5C8  token=0x6000B5B  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.half3
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x16
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.half          x  // 0x10
  public            Unity.Mathematics.half          y  // 0x12
  public            Unity.Mathematics.half          z  // 0x14
METHODS:
  RVA=0x04DB22F0  token=0x6000B5C  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z)
  RVA=0x04DB70C0  token=0x6000B5D  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half2 yz)
  RVA=0x04DB70E0  token=0x6000B5E  System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half z)
  RVA=0x04DB70F0  token=0x6000B5F  System.Void .ctor(Unity.Mathematics.half3 xyz)
  RVA=0x04DB7110  token=0x6000B60  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B28FA54  token=0x6000B61  System.Void .ctor(System.Single v)
  RVA=0x0B28FA8C  token=0x6000B62  System.Void .ctor(Unity.Mathematics.float3 v)
  RVA=0x0B28F9C8  token=0x6000B63  System.Void .ctor(System.Double v)
  RVA=0x0B28FA0C  token=0x6000B64  System.Void .ctor(Unity.Mathematics.double3 v)
  RVA=0x0B253474  token=0x6000B65  Unity.Mathematics.half3 op_Explicit(Unity.Mathematics.float3 v)
  RVA=0x0B253428  token=0x6000B66  Unity.Mathematics.half3 op_Explicit(Unity.Mathematics.double3 v)
  RVA=0x0B28F794  token=0x6000B67  System.Boolean Equals(Unity.Mathematics.half3 rhs)
  RVA=0x0B28F7B8  token=0x6000B68  System.Boolean Equals(System.Object o)
  RVA=0x0B28F844  token=0x6000B69  System.Int32 GetHashCode()
  RVA=0x0B28F910  token=0x6000B6A  System.String ToString()
  RVA=0x0B28F868  token=0x6000B6B  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.half4
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.half          x  // 0x10
  public            Unity.Mathematics.half          y  // 0x12
  public            Unity.Mathematics.half          z  // 0x14
  public            Unity.Mathematics.half          w  // 0x16
  public    static readonly Unity.Mathematics.half4         zero  // static @ 0x0
METHODS:
  RVA=0x04DB23B0  token=0x6000B6C  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half z, Unity.Mathematics.half w)
  RVA=0x04DB7190  token=0x6000B6D  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half y, Unity.Mathematics.half2 zw)
  RVA=0x04DB71B0  token=0x6000B6E  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half2 yz, Unity.Mathematics.half w)
  RVA=0x04DB7200  token=0x6000B6F  System.Void .ctor(Unity.Mathematics.half x, Unity.Mathematics.half3 yzw)
  RVA=0x04DB7120  token=0x6000B70  System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half z, Unity.Mathematics.half w)
  RVA=0x04DB7140  token=0x6000B71  System.Void .ctor(Unity.Mathematics.half2 xy, Unity.Mathematics.half2 zw)
  RVA=0x04DB71E0  token=0x6000B72  System.Void .ctor(Unity.Mathematics.half3 xyz, Unity.Mathematics.half w)
  RVA=0x04DB7160  token=0x6000B73  System.Void .ctor(Unity.Mathematics.half4 xyzw)
  RVA=0x04DB71D0  token=0x6000B74  System.Void .ctor(Unity.Mathematics.half v)
  RVA=0x0B28FED8  token=0x6000B75  System.Void .ctor(System.Single v)
  RVA=0x0B28FDDC  token=0x6000B76  System.Void .ctor(Unity.Mathematics.float4 v)
  RVA=0x0B28FE84  token=0x6000B77  System.Void .ctor(System.Double v)
  RVA=0x0B28FE28  token=0x6000B78  System.Void .ctor(Unity.Mathematics.double4 v)
  RVA=0x0B2535F0  token=0x6000B79  Unity.Mathematics.half4 op_Explicit(Unity.Mathematics.float4 v)
  RVA=0x0B287BE0  token=0x6000B7A  System.Boolean Equals(Unity.Mathematics.half4 rhs)
  RVA=0x0B28FAC8  token=0x6000B7B  System.Boolean Equals(System.Object o)
  RVA=0x0B28FB28  token=0x6000B7C  System.Int32 GetHashCode()
  RVA=0x0B28FB30  token=0x6000B7D  System.String ToString()
  RVA=0x0B28FC94  token=0x6000B7E  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int2
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
  public    static readonly Unity.Mathematics.int2          zero  // static @ 0x0
PROPERTIES:
  xy  get=0x04DB7290
  Item  set=0x04DB73A0
METHODS:
  RVA=0x04D85C40  token=0x6000B7F  System.Void .ctor(System.Int32 x, System.Int32 y)
  RVA=0x04DB7280  token=0x6000B80  System.Void .ctor(Unity.Mathematics.int2 xy)
  RVA=0x04DB7220  token=0x6000B81  System.Void .ctor(System.Int32 v)
  RVA=0x0B2901E8  token=0x6000B82  System.Void .ctor(System.Boolean v)
  RVA=0x0B2901F8  token=0x6000B83  System.Void .ctor(Unity.Mathematics.bool2 v)
  RVA=0x04DB7220  token=0x6000B84  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB7280  token=0x6000B85  System.Void .ctor(Unity.Mathematics.uint2 v)
  RVA=0x04DB7270  token=0x6000B86  System.Void .ctor(System.Single v)
  RVA=0x04DB7240  token=0x6000B87  System.Void .ctor(Unity.Mathematics.float2 v)
  RVA=0x04DB7260  token=0x6000B88  System.Void .ctor(System.Double v)
  RVA=0x04DB7230  token=0x6000B89  System.Void .ctor(Unity.Mathematics.double2 v)
  RVA=0x04DB02A0  token=0x6000B8A  Unity.Mathematics.int2 op_Implicit(System.Int32 v)
  RVA=0x04DB02A0  token=0x6000B8B  Unity.Mathematics.int2 op_Explicit(System.UInt32 v)
  RVA=0x04DAB5F0  token=0x6000B8C  Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.uint2 v)
  RVA=0x04DB0280  token=0x6000B8D  Unity.Mathematics.int2 op_Explicit(System.Single v)
  RVA=0x04DB0240  token=0x6000B8E  Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.float2 v)
  RVA=0x04DB0260  token=0x6000B8F  Unity.Mathematics.int2 op_Explicit(System.Double v)
  RVA=0x04DB02B0  token=0x6000B90  Unity.Mathematics.int2 op_Explicit(Unity.Mathematics.double2 v)
  RVA=0x04DB7310  token=0x6000B91  Unity.Mathematics.int2 op_Multiply(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs)
  RVA=0x04DB72F0  token=0x6000B92  Unity.Mathematics.int2 op_Multiply(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x04D85C70  token=0x6000B93  Unity.Mathematics.int2 op_Addition(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs)
  RVA=0x04DB72B0  token=0x6000B94  Unity.Mathematics.int2 op_Addition(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x04DB7360  token=0x6000B95  Unity.Mathematics.int2 op_Subtraction(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x0B290214  token=0x6000B96  Unity.Mathematics.int2 op_Division(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x0B290258  token=0x6000B97  Unity.Mathematics.bool2 op_GreaterThan(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x0B290238  token=0x6000B98  Unity.Mathematics.bool2 op_GreaterThanOrEqual(Unity.Mathematics.int2 lhs, System.Int32 rhs)
  RVA=0x04DB7380  token=0x6000B99  Unity.Mathematics.int2 op_UnaryNegation(Unity.Mathematics.int2 val)
  RVA=0x04DB7330  token=0x6000B9A  Unity.Mathematics.int2 op_RightShift(Unity.Mathematics.int2 x, System.Int32 n)
  RVA=0x04DB72D0  token=0x6000B9B  Unity.Mathematics.int2 op_BitwiseOr(Unity.Mathematics.int2 lhs, Unity.Mathematics.int2 rhs)
  RVA=0x063516B4  token=0x6000B9E  System.Boolean Equals(Unity.Mathematics.int2 rhs)
  RVA=0x0B290030  token=0x6000B9F  System.Boolean Equals(System.Object o)
  RVA=0x0B2900A4  token=0x6000BA0  System.Int32 GetHashCode()
  RVA=0x0B290160  token=0x6000BA1  System.String ToString()
  RVA=0x0B2900D8  token=0x6000BA2  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int2x2
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int2          c0  // 0x10
  public            Unity.Mathematics.int2          c1  // 0x18
  public    static readonly Unity.Mathematics.int2x2        identity  // static @ 0x0
METHODS:
  RVA=0x04D861B0  token=0x6000BA3  System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1)
  RVA=0x04DB74F0  token=0x6000BA4  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11)
  RVA=0x04DB7430  token=0x6000BA5  System.Void .ctor(System.Int32 v)
  RVA=0x0B290618  token=0x6000BA6  System.Void .ctor(System.Boolean v)
  RVA=0x0B2905A8  token=0x6000BA7  System.Void .ctor(Unity.Mathematics.bool2x2 v)
  RVA=0x04DB7430  token=0x6000BA8  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB7520  token=0x6000BA9  System.Void .ctor(Unity.Mathematics.uint2x2 v)
  RVA=0x04DB73B0  token=0x6000BAA  System.Void .ctor(System.Single v)
  RVA=0x04DB74A0  token=0x6000BAB  System.Void .ctor(Unity.Mathematics.float2x2 v)
  RVA=0x04DB7460  token=0x6000BAC  System.Void .ctor(System.Double v)
  RVA=0x04DB73F0  token=0x6000BAD  System.Void .ctor(Unity.Mathematics.double2x2 v)
  RVA=0x0B287F80  token=0x6000BAE  System.Boolean Equals(Unity.Mathematics.int2x2 rhs)
  RVA=0x0B290270  token=0x6000BAF  System.Boolean Equals(System.Object o)
  RVA=0x0B2902DC  token=0x6000BB0  System.Int32 GetHashCode()
  RVA=0x0B2902F8  token=0x6000BB1  System.String ToString()
  RVA=0x0B290454  token=0x6000BB2  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04CEAE50  token=0x6000BB3  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.int2x3
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int2          c0  // 0x10
  public            Unity.Mathematics.int2          c1  // 0x18
  public            Unity.Mathematics.int2          c2  // 0x20
PROPERTIES:
  Item  get=0x04DB77A0
METHODS:
  RVA=0x04D8C0D0  token=0x6000BB4  System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2)
  RVA=0x04DB7750  token=0x6000BB5  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12)
  RVA=0x04DB76C0  token=0x6000BB6  System.Void .ctor(System.Int32 v)
  RVA=0x0B290B74  token=0x6000BB7  System.Void .ctor(System.Boolean v)
  RVA=0x0B290ADC  token=0x6000BB8  System.Void .ctor(Unity.Mathematics.bool2x3 v)
  RVA=0x04DB76C0  token=0x6000BB9  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB7700  token=0x6000BBA  System.Void .ctor(Unity.Mathematics.uint2x3 v)
  RVA=0x04DB7610  token=0x6000BBB  System.Void .ctor(System.Single v)
  RVA=0x04DB7660  token=0x6000BBC  System.Void .ctor(Unity.Mathematics.float2x3 v)
  RVA=0x04DB7560  token=0x6000BBD  System.Void .ctor(System.Double v)
  RVA=0x04DB75B0  token=0x6000BBE  System.Void .ctor(Unity.Mathematics.double2x3 v)
  RVA=0x0B290660  token=0x6000BC0  System.Boolean Equals(Unity.Mathematics.int2x3 rhs)
  RVA=0x0B2906C4  token=0x6000BC1  System.Boolean Equals(System.Object o)
  RVA=0x0B290738  token=0x6000BC2  System.Int32 GetHashCode()
  RVA=0x0B290760  token=0x6000BC3  System.String ToString()
  RVA=0x0B290928  token=0x6000BC4  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int2x4
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int2          c0  // 0x10
  public            Unity.Mathematics.int2          c1  // 0x18
  public            Unity.Mathematics.int2          c2  // 0x20
  public            Unity.Mathematics.int2          c3  // 0x28
METHODS:
  RVA=0x04D861C0  token=0x6000BC5  System.Void .ctor(Unity.Mathematics.int2 c0, Unity.Mathematics.int2 c1, Unity.Mathematics.int2 c2, Unity.Mathematics.int2 c3)
  RVA=0x04DB78A0  token=0x6000BC6  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13)
  RVA=0x04DB7980  token=0x6000BC7  System.Void .ctor(System.Int32 v)
  RVA=0x0B2911F4  token=0x6000BC8  System.Void .ctor(System.Boolean v)
  RVA=0x0B291128  token=0x6000BC9  System.Void .ctor(Unity.Mathematics.bool2x4 v)
  RVA=0x04DB7980  token=0x6000BCA  System.Void .ctor(System.UInt32 v)
  RVA=0x04DB7830  token=0x6000BCB  System.Void .ctor(Unity.Mathematics.uint2x4 v)
  RVA=0x04DB79D0  token=0x6000BCC  System.Void .ctor(System.Single v)
  RVA=0x04DB77B0  token=0x6000BCD  System.Void .ctor(Unity.Mathematics.float2x4 v)
  RVA=0x04DB7A40  token=0x6000BCE  System.Void .ctor(System.Double v)
  RVA=0x04DB7900  token=0x6000BCF  System.Void .ctor(Unity.Mathematics.double2x4 v)
  RVA=0x0B290C4C  token=0x6000BD0  System.Boolean Equals(Unity.Mathematics.int2x4 rhs)
  RVA=0x0B290BD8  token=0x6000BD1  System.Boolean Equals(System.Object o)
  RVA=0x0B290CC0  token=0x6000BD2  System.Int32 GetHashCode()
  RVA=0x0B290EF8  token=0x6000BD3  System.String ToString()
  RVA=0x0B290CE4  token=0x6000BD4  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int3
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
  public            System.Int32                    z  // 0x18
  public    static readonly Unity.Mathematics.int3          zero  // static @ 0x0
PROPERTIES:
  xyz  set=0x04D88F40
  xzy  get=0x04DB5530
  xy  get=0x04DB7290  set=0x04DB7280
  yz  get=0x04DB7B70
  zx  get=0x04DB7B90
  Item  get=0x04DB7B60  set=0x04DB73A0
METHODS:
  RVA=0x04D85D60  token=0x6000BD5  System.Void .ctor(System.Int32 x, System.Int32 y, System.Int32 z)
  RVA=0x04DB7AB0  token=0x6000BD6  System.Void .ctor(System.Int32 x, Unity.Mathematics.int2 yz)
  RVA=0x04DB7B50  token=0x6000BD7  System.Void .ctor(Unity.Mathematics.int2 xy, System.Int32 z)
  RVA=0x04D88F40  token=0x6000BD8  System.Void .ctor(Unity.Mathematics.int3 xyz)
  RVA=0x04DB7B20  token=0x6000BD9  System.Void .ctor(System.Int32 v)
  RVA=0x0B29149C  token=0x6000BDA  System.Void .ctor(System.Boolean v)
  RVA=0x0B291470  token=0x6000BDB  System.Void .ctor(Unity.Mathematics.bool3 v)
  RVA=0x04DB7B20  token=0x6000BDC  System.Void .ctor(System.UInt32 v)
  RVA=0x04D88F40  token=0x6000BDD  System.Void .ctor(Unity.Mathematics.uint3 v)
  RVA=0x04DB7AE0  token=0x6000BDE  System.Void .ctor(System.Single v)
  RVA=0x04DB7AC0  token=0x6000BDF  System.Void .ctor(Unity.Mathematics.float3 v)
  RVA=0x04DB7B00  token=0x6000BE0  System.Void .ctor(System.Double v)
  RVA=0x04DB7B30  token=0x6000BE1  System.Void .ctor(Unity.Mathematics.double3 v)
  RVA=0x04DB02F0  token=0x6000BE2  Unity.Mathematics.int3 op_Implicit(System.Int32 v)
  RVA=0x04DB02F0  token=0x6000BE3  Unity.Mathematics.int3 op_Explicit(System.UInt32 v)
  RVA=0x04DB0370  token=0x6000BE4  Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.uint3 v)
  RVA=0x04DB03C0  token=0x6000BE5  Unity.Mathematics.int3 op_Explicit(System.Single v)
  RVA=0x04DB0300  token=0x6000BE6  Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.float3 v)
  RVA=0x04DB03A0  token=0x6000BE7  Unity.Mathematics.int3 op_Explicit(System.Double v)
  RVA=0x04DB03E0  token=0x6000BE8  Unity.Mathematics.int3 op_Explicit(Unity.Mathematics.double3 v)
  RVA=0x03C87E40  token=0x6000BE9  Unity.Mathematics.int3 op_Multiply(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x04DB7C00  token=0x6000BEA  Unity.Mathematics.int3 op_Multiply(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x0338E380  token=0x6000BEB  Unity.Mathematics.int3 op_Addition(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x04DB7BB0  token=0x6000BEC  Unity.Mathematics.int3 op_Addition(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x04DB7C70  token=0x6000BED  Unity.Mathematics.int3 op_Subtraction(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x04DB7C50  token=0x6000BEE  Unity.Mathematics.int3 op_Subtraction(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x0B2914B0  token=0x6000BEF  Unity.Mathematics.int3 op_Division(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x0B291544  token=0x6000BF0  Unity.Mathematics.bool3 op_LessThanOrEqual(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x0B291524  token=0x6000BF1  Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x0B2914D8  token=0x6000BF2  Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x0B291504  token=0x6000BF3  Unity.Mathematics.bool3 op_GreaterThanOrEqual(Unity.Mathematics.int3 lhs, System.Int32 rhs)
  RVA=0x04DB7C90  token=0x6000BF4  Unity.Mathematics.int3 op_UnaryNegation(Unity.Mathematics.int3 val)
  RVA=0x04DB7C20  token=0x6000BF5  Unity.Mathematics.int3 op_RightShift(Unity.Mathematics.int3 x, System.Int32 n)
  RVA=0x04DB7BE0  token=0x6000BF6  Unity.Mathematics.int3 op_BitwiseOr(Unity.Mathematics.int3 lhs, Unity.Mathematics.int3 rhs)
  RVA=0x06351A88  token=0x6000BFF  System.Boolean Equals(Unity.Mathematics.int3 rhs)
  RVA=0x0B291278  token=0x6000C00  System.Boolean Equals(System.Object o)
  RVA=0x07042C24  token=0x6000C01  System.Int32 GetHashCode()
  RVA=0x0B2913BC  token=0x6000C02  System.String ToString()
  RVA=0x0B29130C  token=0x6000C03  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int3x2
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int3          c0  // 0x10
  public            Unity.Mathematics.int3          c1  // 0x1c
METHODS:
  RVA=0x04D871E0  token=0x6000C04  System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1)
  RVA=0x04DB7CB0  token=0x6000C05  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21)
  RVA=0x04DB7D40  token=0x6000C06  System.Void .ctor(System.Int32 v)
  RVA=0x05F051A0  token=0x6000C07  System.Void .ctor(System.Boolean v)
  RVA=0x0B291A00  token=0x6000C08  System.Void .ctor(Unity.Mathematics.bool3x2 v)
  RVA=0x04DB7D40  token=0x6000C09  System.Void .ctor(System.UInt32 v)
  RVA=0x05F05370  token=0x6000C0A  System.Void .ctor(Unity.Mathematics.uint3x2 v)
  RVA=0x04DB7CF0  token=0x6000C0B  System.Void .ctor(System.Single v)
  RVA=0x05F053E4  token=0x6000C0C  System.Void .ctor(Unity.Mathematics.float3x2 v)
  RVA=0x04DB7D70  token=0x6000C0D  System.Void .ctor(System.Double v)
  RVA=0x0B291988  token=0x6000C0E  System.Void .ctor(Unity.Mathematics.double3x2 v)
  RVA=0x066ACFB0  token=0x6000C0F  System.Boolean Equals(Unity.Mathematics.int3x2 rhs)
  RVA=0x0B291570  token=0x6000C10  System.Boolean Equals(System.Object o)
  RVA=0x0B2915E4  token=0x6000C11  System.Int32 GetHashCode()
  RVA=0x0B2917C0  token=0x6000C12  System.String ToString()
  RVA=0x0B29160C  token=0x6000C13  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int3x3
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x34
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int3          c0  // 0x10
  public            Unity.Mathematics.int3          c1  // 0x1c
  public            Unity.Mathematics.int3          c2  // 0x28
  public    static readonly Unity.Mathematics.int3x3        identity  // static @ 0x0
METHODS:
  RVA=0x04D8C690  token=0x6000C14  System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2)
  RVA=0x04B832D0  token=0x6000C15  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22)
  RVA=0x04DB7DC0  token=0x6000C16  System.Void .ctor(System.Int32 v)
  RVA=0x05F05648  token=0x6000C17  System.Void .ctor(System.Boolean v)
  RVA=0x0B2920DC  token=0x6000C18  System.Void .ctor(Unity.Mathematics.bool3x3 v)
  RVA=0x04DB7DC0  token=0x6000C19  System.Void .ctor(System.UInt32 v)
  RVA=0x05F056D8  token=0x6000C1A  System.Void .ctor(Unity.Mathematics.uint3x3 v)
  RVA=0x04DB7E80  token=0x6000C1B  System.Void .ctor(System.Single v)
  RVA=0x05F055A4  token=0x6000C1C  System.Void .ctor(Unity.Mathematics.float3x3 v)
  RVA=0x04DB7E10  token=0x6000C1D  System.Void .ctor(System.Double v)
  RVA=0x0B292224  token=0x6000C1E  System.Void .ctor(Unity.Mathematics.double3x3 v)
  RVA=0x0B291B68  token=0x6000C1F  System.Boolean Equals(Unity.Mathematics.int3x3 rhs)
  RVA=0x0B291AEC  token=0x6000C20  System.Boolean Equals(System.Object o)
  RVA=0x0B291C08  token=0x6000C21  System.Int32 GetHashCode()
  RVA=0x0B291E78  token=0x6000C22  System.String ToString()
  RVA=0x0B291C34  token=0x6000C23  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04B83330  token=0x6000C24  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.int3x4
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int3          c0  // 0x10
  public            Unity.Mathematics.int3          c1  // 0x1c
  public            Unity.Mathematics.int3          c2  // 0x28
  public            Unity.Mathematics.int3          c3  // 0x34
METHODS:
  RVA=0x04DB65D0  token=0x6000C25  System.Void .ctor(Unity.Mathematics.int3 c0, Unity.Mathematics.int3 c1, Unity.Mathematics.int3 c2, Unity.Mathematics.int3 c3)
  RVA=0x04DB8070  token=0x6000C26  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23)
  RVA=0x04DB7F80  token=0x6000C27  System.Void .ctor(System.Int32 v)
  RVA=0x05F059CC  token=0x6000C28  System.Void .ctor(System.Boolean v)
  RVA=0x0B292A08  token=0x6000C29  System.Void .ctor(Unity.Mathematics.bool3x4 v)
  RVA=0x04DB7F80  token=0x6000C2A  System.Void .ctor(System.UInt32 v)
  RVA=0x05F05824  token=0x6000C2B  System.Void .ctor(Unity.Mathematics.uint3x4 v)
  RVA=0x04DB7EF0  token=0x6000C2C  System.Void .ctor(System.Single v)
  RVA=0x05F058F8  token=0x6000C2D  System.Void .ctor(Unity.Mathematics.float3x4 v)
  RVA=0x04DB7FE0  token=0x6000C2E  System.Void .ctor(System.Double v)
  RVA=0x0B292B9C  token=0x6000C2F  System.Void .ctor(Unity.Mathematics.double3x4 v)
  RVA=0x0B292348  token=0x6000C30  System.Boolean Equals(Unity.Mathematics.int3x4 rhs)
  RVA=0x0B2922CC  token=0x6000C31  System.Boolean Equals(System.Object o)
  RVA=0x0B292404  token=0x6000C32  System.Int32 GetHashCode()
  RVA=0x0B292434  token=0x6000C33  System.String ToString()
  RVA=0x0B292734  token=0x6000C34  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int4
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
  public            System.Int32                    z  // 0x18
  public            System.Int32                    w  // 0x1c
  public    static readonly Unity.Mathematics.int4          zero  // static @ 0x0
PROPERTIES:
  Item  get=0x04DB7B60  set=0x04DB73A0
METHODS:
  RVA=0x04D88C10  token=0x6000C35  System.Void .ctor(System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 w)
  RVA=0x04DB8100  token=0x6000C36  System.Void .ctor(System.Int32 x, System.Int32 y, Unity.Mathematics.int2 zw)
  RVA=0x04DB8200  token=0x6000C37  System.Void .ctor(System.Int32 x, Unity.Mathematics.int2 yz, System.Int32 w)
  RVA=0x04DB81D0  token=0x6000C38  System.Void .ctor(System.Int32 x, Unity.Mathematics.int3 yzw)
  RVA=0x04DB8220  token=0x6000C39  System.Void .ctor(Unity.Mathematics.int2 xy, System.Int32 z, System.Int32 w)
  RVA=0x04DB8120  token=0x6000C3A  System.Void .ctor(Unity.Mathematics.int2 xy, Unity.Mathematics.int2 zw)
  RVA=0x04DB81B0  token=0x6000C3B  System.Void .ctor(Unity.Mathematics.int3 xyz, System.Int32 w)
  RVA=0x04D92B90  token=0x6000C3C  System.Void .ctor(Unity.Mathematics.int4 xyzw)
  RVA=0x04DB8160  token=0x6000C3D  System.Void .ctor(System.Int32 v)
  RVA=0x0B292FB0  token=0x6000C3E  System.Void .ctor(System.Boolean v)
  RVA=0x0B292FCC  token=0x6000C3F  System.Void .ctor(Unity.Mathematics.bool4 v)
  RVA=0x04DB8160  token=0x6000C40  System.Void .ctor(System.UInt32 v)
  RVA=0x04D92B90  token=0x6000C41  System.Void .ctor(Unity.Mathematics.uint4 v)
  RVA=0x04DB8140  token=0x6000C42  System.Void .ctor(System.Single v)
  RVA=0x04DB81F0  token=0x6000C43  System.Void .ctor(Unity.Mathematics.float4 v)
  RVA=0x04DB8170  token=0x6000C44  System.Void .ctor(System.Double v)
  RVA=0x04DB8190  token=0x6000C45  System.Void .ctor(Unity.Mathematics.double4 v)
  RVA=0x04DB0420  token=0x6000C46  Unity.Mathematics.int4 op_Implicit(System.Int32 v)
  RVA=0x04DB0420  token=0x6000C47  Unity.Mathematics.int4 op_Explicit(System.UInt32 v)
  RVA=0x0B25AC78  token=0x6000C48  Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.uint4 v)
  RVA=0x04DB0490  token=0x6000C49  Unity.Mathematics.int4 op_Explicit(System.Single v)
  RVA=0x0B25AD38  token=0x6000C4A  Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.float4 v)
  RVA=0x04DB0470  token=0x6000C4B  Unity.Mathematics.int4 op_Explicit(System.Double v)
  RVA=0x0B25AD08  token=0x6000C4C  Unity.Mathematics.int4 op_Explicit(Unity.Mathematics.double4 v)
  RVA=0x04DB82A0  token=0x6000C4D  Unity.Mathematics.int4 op_Multiply(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs)
  RVA=0x04DB82D0  token=0x6000C4E  Unity.Mathematics.int4 op_Multiply(Unity.Mathematics.int4 lhs, System.Int32 rhs)
  RVA=0x04DB8270  token=0x6000C4F  Unity.Mathematics.int4 op_Addition(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs)
  RVA=0x04DB8240  token=0x6000C50  Unity.Mathematics.int4 op_Addition(Unity.Mathematics.int4 lhs, System.Int32 rhs)
  RVA=0x04DB8330  token=0x6000C51  Unity.Mathematics.int4 op_Subtraction(Unity.Mathematics.int4 lhs, System.Int32 rhs)
  RVA=0x0B293034  token=0x6000C52  Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.int4 lhs, System.Int32 rhs)
  RVA=0x0B293010  token=0x6000C53  Unity.Mathematics.bool4 op_GreaterThanOrEqual(Unity.Mathematics.int4 lhs, System.Int32 rhs)
  RVA=0x04DB8360  token=0x6000C54  Unity.Mathematics.int4 op_UnaryNegation(Unity.Mathematics.int4 val)
  RVA=0x04DB8300  token=0x6000C55  Unity.Mathematics.int4 op_RightShift(Unity.Mathematics.int4 x, System.Int32 n)
  RVA=0x034C9890  token=0x6000C56  Unity.Mathematics.int4 op_BitwiseOr(Unity.Mathematics.int4 lhs, Unity.Mathematics.int4 rhs)
  RVA=0x068AFB90  token=0x6000C59  System.Boolean Equals(Unity.Mathematics.int4 rhs)
  RVA=0x0B292C78  token=0x6000C5A  System.Boolean Equals(System.Object o)
  RVA=0x0B292CE4  token=0x6000C5B  System.Int32 GetHashCode()
  RVA=0x0B292D00  token=0x6000C5C  System.String ToString()
  RVA=0x0B292E5C  token=0x6000C5D  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int4x2
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int4          c0  // 0x10
  public            Unity.Mathematics.int4          c1  // 0x20
METHODS:
  RVA=0x04D8C810  token=0x6000C5E  System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1)
  RVA=0x04DB83B0  token=0x6000C5F  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m10, System.Int32 m11, System.Int32 m20, System.Int32 m21, System.Int32 m30, System.Int32 m31)
  RVA=0x03BACBE0  token=0x6000C60  System.Void .ctor(System.Int32 v)
  RVA=0x05F05DC8  token=0x6000C61  System.Void .ctor(System.Boolean v)
  RVA=0x05F05D4C  token=0x6000C62  System.Void .ctor(Unity.Mathematics.bool4x2 v)
  RVA=0x03BACBE0  token=0x6000C63  System.Void .ctor(System.UInt32 v)
  RVA=0x0B2935DC  token=0x6000C64  System.Void .ctor(Unity.Mathematics.uint4x2 v)
  RVA=0x04DB8390  token=0x6000C65  System.Void .ctor(System.Single v)
  RVA=0x05F05EB8  token=0x6000C66  System.Void .ctor(Unity.Mathematics.float4x2 v)
  RVA=0x04DB8400  token=0x6000C67  System.Void .ctor(System.Double v)
  RVA=0x0B293578  token=0x6000C68  System.Void .ctor(Unity.Mathematics.double4x2 v)
  RVA=0x0B25ADCC  token=0x6000C69  Unity.Mathematics.int4x2 op_Implicit(System.Int32 v)
  RVA=0x0B293058  token=0x6000C6A  System.Boolean Equals(Unity.Mathematics.int4x2 rhs)
  RVA=0x0B29309C  token=0x6000C6B  System.Boolean Equals(System.Object o)
  RVA=0x0B293110  token=0x6000C6C  System.Int32 GetHashCode()
  RVA=0x0B293134  token=0x6000C6D  System.String ToString()
  RVA=0x0B293364  token=0x6000C6E  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int4x3
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int4          c0  // 0x10
  public            Unity.Mathematics.int4          c1  // 0x20
  public            Unity.Mathematics.int4          c2  // 0x30
METHODS:
  RVA=0x04DA0760  token=0x6000C6F  System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2)
  RVA=0x04DB84E0  token=0x6000C70  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m30, System.Int32 m31, System.Int32 m32)
  RVA=0x04DB8560  token=0x6000C71  System.Void .ctor(System.Int32 v)
  RVA=0x05F05FF8  token=0x6000C72  System.Void .ctor(System.Boolean v)
  RVA=0x05F05F40  token=0x6000C73  System.Void .ctor(Unity.Mathematics.bool4x3 v)
  RVA=0x04DB8560  token=0x6000C74  System.Void .ctor(System.UInt32 v)
  RVA=0x0B293D24  token=0x6000C75  System.Void .ctor(Unity.Mathematics.uint4x3 v)
  RVA=0x04DB85B0  token=0x6000C76  System.Void .ctor(System.Single v)
  RVA=0x05F060B4  token=0x6000C77  System.Void .ctor(Unity.Mathematics.float4x3 v)
  RVA=0x04DB8460  token=0x6000C78  System.Void .ctor(System.Double v)
  RVA=0x0B293DAC  token=0x6000C79  System.Void .ctor(Unity.Mathematics.double4x3 v)
  RVA=0x0B2936C0  token=0x6000C7A  System.Boolean Equals(Unity.Mathematics.int4x3 rhs)
  RVA=0x0B293644  token=0x6000C7B  System.Boolean Equals(System.Object o)
  RVA=0x0B293720  token=0x6000C7C  System.Int32 GetHashCode()
  RVA=0x0B293A24  token=0x6000C7D  System.String ToString()
  RVA=0x0B293750  token=0x6000C7E  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.int4x4
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x50
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.int4          c0  // 0x10
  public            Unity.Mathematics.int4          c1  // 0x20
  public            Unity.Mathematics.int4          c2  // 0x30
  public            Unity.Mathematics.int4          c3  // 0x40
  public    static readonly Unity.Mathematics.int4x4        identity  // static @ 0x0
METHODS:
  RVA=0x04DB3220  token=0x6000C7F  System.Void .ctor(Unity.Mathematics.int4 c0, Unity.Mathematics.int4 c1, Unity.Mathematics.int4 c2, Unity.Mathematics.int4 c3)
  RVA=0x04A3ADB0  token=0x6000C80  System.Void .ctor(System.Int32 m00, System.Int32 m01, System.Int32 m02, System.Int32 m03, System.Int32 m10, System.Int32 m11, System.Int32 m12, System.Int32 m13, System.Int32 m20, System.Int32 m21, System.Int32 m22, System.Int32 m23, System.Int32 m30, System.Int32 m31, System.Int32 m32, System.Int32 m33)
  RVA=0x04DB8690  token=0x6000C81  System.Void .ctor(System.Int32 v)
  RVA=0x05F06204  token=0x6000C82  System.Void .ctor(System.Boolean v)
  RVA=0x05F062F4  token=0x6000C83  System.Void .ctor(Unity.Mathematics.bool4x4 v)
  RVA=0x04DB8690  token=0x6000C84  System.Void .ctor(System.UInt32 v)
  RVA=0x0B2946D4  token=0x6000C85  System.Void .ctor(Unity.Mathematics.uint4x4 v)
  RVA=0x04DB86F0  token=0x6000C86  System.Void .ctor(System.Single v)
  RVA=0x05F06490  token=0x6000C87  System.Void .ctor(Unity.Mathematics.float4x4 v)
  RVA=0x04DB85E0  token=0x6000C88  System.Void .ctor(System.Double v)
  RVA=0x0B294780  token=0x6000C89  System.Void .ctor(Unity.Mathematics.double4x4 v)
  RVA=0x0B293EBC  token=0x6000C8A  System.Boolean Equals(Unity.Mathematics.int4x4 rhs)
  RVA=0x0B293E38  token=0x6000C8B  System.Boolean Equals(System.Object o)
  RVA=0x0B293F38  token=0x6000C8C  System.Int32 GetHashCode()
  RVA=0x0B294304  token=0x6000C8D  System.String ToString()
  RVA=0x0B293F70  token=0x6000C8E  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04A3AE60  token=0x6000C8F  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.quaternion
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.float4        value  // 0x10
  public    static readonly Unity.Mathematics.quaternion    identity  // static @ 0x0
METHODS:
  RVA=0x0328FB00  token=0x6000C90  UnityEngine.Quaternion op_Implicit(Unity.Mathematics.quaternion q)
  RVA=0x0328FB00  token=0x6000C91  Unity.Mathematics.quaternion op_Implicit(UnityEngine.Quaternion q)
  RVA=0x04D88AD0  token=0x6000C92  System.Void .ctor(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x04D8D270  token=0x6000C93  System.Void .ctor(Unity.Mathematics.float4 value)
  RVA=0x02B77630  token=0x6000C94  Unity.Mathematics.quaternion op_Implicit(Unity.Mathematics.float4 v)
  RVA=0x034C9420  token=0x6000C95  System.Void .ctor(Unity.Mathematics.float3x3 m)
  RVA=0x0B299FC0  token=0x6000C96  System.Void .ctor(Unity.Mathematics.float4x4 m)
  RVA=0x062E2884  token=0x6000C97  Unity.Mathematics.quaternion AxisAngle(Unity.Mathematics.float3 axis, System.Single angle)
  RVA=0x062E2990  token=0x6000C98  Unity.Mathematics.quaternion EulerXYZ(Unity.Mathematics.float3 xyz)
  RVA=0x062E2B70  token=0x6000C99  Unity.Mathematics.quaternion EulerXZY(Unity.Mathematics.float3 xyz)
  RVA=0x062E2D50  token=0x6000C9A  Unity.Mathematics.quaternion EulerYXZ(Unity.Mathematics.float3 xyz)
  RVA=0x062E2F30  token=0x6000C9B  Unity.Mathematics.quaternion EulerYZX(Unity.Mathematics.float3 xyz)
  RVA=0x0B299560  token=0x6000C9C  Unity.Mathematics.quaternion EulerZXY(Unity.Mathematics.float3 xyz)
  RVA=0x062E3148  token=0x6000C9D  Unity.Mathematics.quaternion EulerZYX(Unity.Mathematics.float3 xyz)
  RVA=0x0B299480  token=0x6000C9E  Unity.Mathematics.quaternion EulerXYZ(System.Single x, System.Single y, System.Single z)
  RVA=0x0B2994B8  token=0x6000C9F  Unity.Mathematics.quaternion EulerXZY(System.Single x, System.Single y, System.Single z)
  RVA=0x0B2994F0  token=0x6000CA0  Unity.Mathematics.quaternion EulerYXZ(System.Single x, System.Single y, System.Single z)
  RVA=0x0B299528  token=0x6000CA1  Unity.Mathematics.quaternion EulerYZX(System.Single x, System.Single y, System.Single z)
  RVA=0x062E3110  token=0x6000CA2  Unity.Mathematics.quaternion EulerZXY(System.Single x, System.Single y, System.Single z)
  RVA=0x0B299740  token=0x6000CA3  Unity.Mathematics.quaternion EulerZYX(System.Single x, System.Single y, System.Single z)
  RVA=0x0B299778  token=0x6000CA4  Unity.Mathematics.quaternion Euler(Unity.Mathematics.float3 xyz, Unity.Mathematics.math.RotationOrder order)
  RVA=0x062E3328  token=0x6000CA5  Unity.Mathematics.quaternion Euler(System.Single x, System.Single y, System.Single z, Unity.Mathematics.math.RotationOrder order)
  RVA=0x062E3364  token=0x6000CA6  Unity.Mathematics.quaternion RotateX(System.Single angle)
  RVA=0x062E33C8  token=0x6000CA7  Unity.Mathematics.quaternion RotateY(System.Single angle)
  RVA=0x062E342C  token=0x6000CA8  Unity.Mathematics.quaternion RotateZ(System.Single angle)
  RVA=0x0B299BE4  token=0x6000CA9  Unity.Mathematics.quaternion LookRotation(Unity.Mathematics.float3 forward, Unity.Mathematics.float3 up)
  RVA=0x0B2998D4  token=0x6000CAA  Unity.Mathematics.quaternion LookRotationSafe(Unity.Mathematics.float3 forward, Unity.Mathematics.float3 up)
  RVA=0x0415FC30  token=0x6000CAB  System.Boolean Equals(Unity.Mathematics.quaternion x)
  RVA=0x0B299414  token=0x6000CAC  System.Boolean Equals(System.Object x)
  RVA=0x0B256480  token=0x6000CAD  System.Int32 GetHashCode()
  RVA=0x0B299D00  token=0x6000CAE  System.String ToString()
  RVA=0x0B299E6C  token=0x6000CAF  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04D6E6C0  token=0x6000CB0  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.noise
TYPE:  static class
TOKEN: 0x200004C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B297E28  token=0x6000CB1  System.Single cnoise(Unity.Mathematics.float2 P)
  RVA=0x0B298718  token=0x6000CB2  System.Single pnoise(Unity.Mathematics.float2 P, Unity.Mathematics.float2 rep)
  RVA=0x0B2984C8  token=0x6000CB3  Unity.Mathematics.float2 mod289(Unity.Mathematics.float2 x)
  RVA=0x0B2983FC  token=0x6000CB4  Unity.Mathematics.float3 mod289(Unity.Mathematics.float3 x)
  RVA=0x0B298518  token=0x6000CB5  Unity.Mathematics.float4 mod289(Unity.Mathematics.float4 x)
  RVA=0x0B298654  token=0x6000CB6  Unity.Mathematics.float3 permute(Unity.Mathematics.float3 x)
  RVA=0x0B2985BC  token=0x6000CB7  Unity.Mathematics.float4 permute(Unity.Mathematics.float4 x)
  RVA=0x0B2993BC  token=0x6000CB8  Unity.Mathematics.float4 taylorInvSqrt(Unity.Mathematics.float4 r)
  RVA=0x0B298380  token=0x6000CB9  Unity.Mathematics.float2 fade(Unity.Mathematics.float2 t)
  RVA=0x0B298CAC  token=0x6000CBA  System.Single snoise(Unity.Mathematics.float2 v)
END_CLASS

CLASS: Unity.Mathematics.Random
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x14
FIELDS:
  public            System.UInt32                   state  // 0x10
METHODS:
  RVA=0x04DB8760  token=0x6000CBB  System.Void .ctor(System.UInt32 seed)
  RVA=0x0B2979F0  token=0x6000CBC  Unity.Mathematics.Random CreateFromIndex(System.UInt32 index)
  RVA=0x04CAFC90  token=0x6000CBD  System.UInt32 WangHash(System.UInt32 n)
  RVA=0x04558F50  token=0x6000CBE  System.Single NextFloat()
  RVA=0x06AF4ED4  token=0x6000CBF  Unity.Mathematics.float3 NextFloat3()
  RVA=0x04DB8730  token=0x6000CC0  System.UInt32 NextState()
END_CLASS

CLASS: Unity.Mathematics.RigidTransform
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x2C
FIELDS:
  public            Unity.Mathematics.quaternion    rot  // 0x10
  public            Unity.Mathematics.float3        pos  // 0x20
  public    static readonly Unity.Mathematics.RigidTransformidentity  // static @ 0x0
METHODS:
  RVA=0x04DB8780  token=0x6000CC1  System.Void .ctor(Unity.Mathematics.quaternion rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B297D48  token=0x6000CC2  System.Void .ctor(Unity.Mathematics.float3x3 rotation, Unity.Mathematics.float3 translation)
  RVA=0x0B297DAC  token=0x6000CC3  System.Void .ctor(Unity.Mathematics.float4x4 transform)
  RVA=0x0B297A94  token=0x6000CC4  System.Boolean Equals(Unity.Mathematics.RigidTransform x)
  RVA=0x0B297A18  token=0x6000CC5  System.Boolean Equals(System.Object x)
  RVA=0x0B297AE4  token=0x6000CC6  System.Int32 GetHashCode()
  RVA=0x0B297B30  token=0x6000CC7  System.String ToString()
  RVA=0x04D538A0  token=0x6000CC8  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.svd
TYPE:  static class
TOKEN: 0x200004F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05F3A2D0  token=0x6000CC9  System.Void condSwap(System.Boolean c, System.Single& x, System.Single& y)
  RVA=0x05F3A25C  token=0x6000CCA  System.Void condNegSwap(System.Boolean c, Unity.Mathematics.float3& x, Unity.Mathematics.float3& y)
  RVA=0x0B29A384  token=0x6000CCB  Unity.Mathematics.quaternion condNegSwapQuat(System.Boolean c, Unity.Mathematics.quaternion q, Unity.Mathematics.float4 mask)
  RVA=0x0B29AF50  token=0x6000CCC  System.Void sortSingularValues(Unity.Mathematics.float3x3& b, Unity.Mathematics.quaternion& v)
  RVA=0x0B29A2E0  token=0x6000CCD  Unity.Mathematics.quaternion approxGivensQuat(Unity.Mathematics.float3 pq, Unity.Mathematics.float4 mask)
  RVA=0x0B29AB58  token=0x6000CCE  Unity.Mathematics.quaternion qrGivensQuat(Unity.Mathematics.float2 pq, Unity.Mathematics.float4 mask)
  RVA=0x0B29A440  token=0x6000CCF  Unity.Mathematics.quaternion givensQRFactorization(Unity.Mathematics.float3x3 b, Unity.Mathematics.float3x3& r)
  RVA=0x0B29A6E8  token=0x6000CD0  Unity.Mathematics.quaternion jacobiIteration(Unity.Mathematics.float3x3& s, System.Int32 iterations)
  RVA=0x0B29AD64  token=0x6000CD1  Unity.Mathematics.float3 singularValuesDecomposition(Unity.Mathematics.float3x3 a, Unity.Mathematics.quaternion& u, Unity.Mathematics.quaternion& v)
  RVA=0x0B29AC54  token=0x6000CD2  Unity.Mathematics.float3 rcpsafe(Unity.Mathematics.float3 x, System.Single epsilon)
  RVA=0x0B29B0F8  token=0x6000CD3  Unity.Mathematics.float3x3 svdInverse(Unity.Mathematics.float3x3 a)
  RVA=0x0B29B2C0  token=0x6000CD4  Unity.Mathematics.quaternion svdRotation(Unity.Mathematics.float3x3 a)
END_CLASS

CLASS: Unity.Mathematics.uint2
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.UInt32                   x  // 0x10
  public            System.UInt32                   y  // 0x14
METHODS:
  RVA=0x04D85C40  token=0x6000CD5  System.Void .ctor(System.UInt32 x, System.UInt32 y)
  RVA=0x04DB7280  token=0x6000CD6  System.Void .ctor(Unity.Mathematics.uint2 xy)
  RVA=0x04DB7220  token=0x6000CD7  System.Void .ctor(System.UInt32 v)
  RVA=0x0B2901E8  token=0x6000CD8  System.Void .ctor(System.Boolean v)
  RVA=0x0B2901F8  token=0x6000CD9  System.Void .ctor(Unity.Mathematics.bool2 v)
  RVA=0x04DB7220  token=0x6000CDA  System.Void .ctor(System.Int32 v)
  RVA=0x04DB7280  token=0x6000CDB  System.Void .ctor(Unity.Mathematics.int2 v)
  RVA=0x05F3ADA8  token=0x6000CDC  System.Void .ctor(System.Single v)
  RVA=0x0B29B534  token=0x6000CDD  System.Void .ctor(Unity.Mathematics.float2 v)
  RVA=0x0B29B518  token=0x6000CDE  System.Void .ctor(System.Double v)
  RVA=0x0B29B570  token=0x6000CDF  System.Void .ctor(Unity.Mathematics.double2 v)
  RVA=0x04DB02A0  token=0x6000CE0  Unity.Mathematics.uint2 op_Implicit(System.UInt32 v)
  RVA=0x04DB02A0  token=0x6000CE1  Unity.Mathematics.uint2 op_Explicit(System.Int32 v)
  RVA=0x04DAB5F0  token=0x6000CE2  Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.int2 v)
  RVA=0x0B275514  token=0x6000CE3  Unity.Mathematics.uint2 op_Explicit(System.Single v)
  RVA=0x0B275538  token=0x6000CE4  Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.float2 v)
  RVA=0x0B2754F4  token=0x6000CE5  Unity.Mathematics.uint2 op_Explicit(System.Double v)
  RVA=0x0B2754C4  token=0x6000CE6  Unity.Mathematics.uint2 op_Explicit(Unity.Mathematics.double2 v)
  RVA=0x04DB7310  token=0x6000CE7  Unity.Mathematics.uint2 op_Multiply(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs)
  RVA=0x04DB72F0  token=0x6000CE8  Unity.Mathematics.uint2 op_Multiply(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x04D85C70  token=0x6000CE9  Unity.Mathematics.uint2 op_Addition(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs)
  RVA=0x04DB72B0  token=0x6000CEA  Unity.Mathematics.uint2 op_Addition(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x04D85C90  token=0x6000CEB  Unity.Mathematics.uint2 op_Subtraction(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs)
  RVA=0x04DB7360  token=0x6000CEC  Unity.Mathematics.uint2 op_Subtraction(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x0B29B5AC  token=0x6000CED  Unity.Mathematics.bool2 op_GreaterThan(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x04DB87E0  token=0x6000CEE  Unity.Mathematics.uint2 op_LeftShift(Unity.Mathematics.uint2 x, System.Int32 n)
  RVA=0x04DB8810  token=0x6000CEF  Unity.Mathematics.uint2 op_RightShift(Unity.Mathematics.uint2 x, System.Int32 n)
  RVA=0x0B29B594  token=0x6000CF0  Unity.Mathematics.bool2 op_Equality(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x04DB87A0  token=0x6000CF1  Unity.Mathematics.uint2 op_BitwiseAnd(Unity.Mathematics.uint2 lhs, System.UInt32 rhs)
  RVA=0x04DB72D0  token=0x6000CF2  Unity.Mathematics.uint2 op_BitwiseOr(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs)
  RVA=0x04DB87C0  token=0x6000CF3  Unity.Mathematics.uint2 op_ExclusiveOr(Unity.Mathematics.uint2 lhs, Unity.Mathematics.uint2 rhs)
  RVA=0x063516B4  token=0x6000CF4  System.Boolean Equals(Unity.Mathematics.uint2 rhs)
  RVA=0x0B29B360  token=0x6000CF5  System.Boolean Equals(System.Object o)
  RVA=0x0B29B3D4  token=0x6000CF6  System.Int32 GetHashCode()
  RVA=0x0B29B490  token=0x6000CF7  System.String ToString()
  RVA=0x0B29B408  token=0x6000CF8  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint2x2
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint2         c0  // 0x10
  public            Unity.Mathematics.uint2         c1  // 0x18
  public    static readonly Unity.Mathematics.uint2x2       identity  // static @ 0x0
METHODS:
  RVA=0x04D861B0  token=0x6000CF9  System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1)
  RVA=0x04DB74F0  token=0x6000CFA  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11)
  RVA=0x04DB7430  token=0x6000CFB  System.Void .ctor(System.UInt32 v)
  RVA=0x0B290618  token=0x6000CFC  System.Void .ctor(System.Boolean v)
  RVA=0x0B2905A8  token=0x6000CFD  System.Void .ctor(Unity.Mathematics.bool2x2 v)
  RVA=0x04DB7430  token=0x6000CFE  System.Void .ctor(System.Int32 v)
  RVA=0x04DB7520  token=0x6000CFF  System.Void .ctor(Unity.Mathematics.int2x2 v)
  RVA=0x05F3AF40  token=0x6000D00  System.Void .ctor(System.Single v)
  RVA=0x05F3AF08  token=0x6000D01  System.Void .ctor(Unity.Mathematics.float2x2 v)
  RVA=0x0B29B954  token=0x6000D02  System.Void .ctor(System.Double v)
  RVA=0x0B29B8FC  token=0x6000D03  System.Void .ctor(Unity.Mathematics.double2x2 v)
  RVA=0x0B287F80  token=0x6000D04  System.Boolean Equals(Unity.Mathematics.uint2x2 rhs)
  RVA=0x0B29B5C4  token=0x6000D05  System.Boolean Equals(System.Object o)
  RVA=0x0B29B630  token=0x6000D06  System.Int32 GetHashCode()
  RVA=0x0B29B7A0  token=0x6000D07  System.String ToString()
  RVA=0x0B29B64C  token=0x6000D08  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04CEADF0  token=0x6000D09  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.uint2x3
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint2         c0  // 0x10
  public            Unity.Mathematics.uint2         c1  // 0x18
  public            Unity.Mathematics.uint2         c2  // 0x20
METHODS:
  RVA=0x04D8C0D0  token=0x6000D0A  System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2)
  RVA=0x04DB7750  token=0x6000D0B  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12)
  RVA=0x04DB76C0  token=0x6000D0C  System.Void .ctor(System.UInt32 v)
  RVA=0x0B290B74  token=0x6000D0D  System.Void .ctor(System.Boolean v)
  RVA=0x0B290ADC  token=0x6000D0E  System.Void .ctor(Unity.Mathematics.bool2x3 v)
  RVA=0x04DB76C0  token=0x6000D0F  System.Void .ctor(System.Int32 v)
  RVA=0x04DB7700  token=0x6000D10  System.Void .ctor(Unity.Mathematics.int2x3 v)
  RVA=0x05F3B020  token=0x6000D11  System.Void .ctor(System.Single v)
  RVA=0x05F3AF68  token=0x6000D12  System.Void .ctor(Unity.Mathematics.float2x3 v)
  RVA=0x0B29BDB0  token=0x6000D13  System.Void .ctor(System.Double v)
  RVA=0x0B29BE0C  token=0x6000D14  System.Void .ctor(Unity.Mathematics.double2x3 v)
  RVA=0x0B290660  token=0x6000D15  System.Boolean Equals(Unity.Mathematics.uint2x3 rhs)
  RVA=0x0B29B998  token=0x6000D16  System.Boolean Equals(System.Object o)
  RVA=0x0B29BA0C  token=0x6000D17  System.Int32 GetHashCode()
  RVA=0x0B29BA34  token=0x6000D18  System.String ToString()
  RVA=0x0B29BBFC  token=0x6000D19  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint2x4
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint2         c0  // 0x10
  public            Unity.Mathematics.uint2         c1  // 0x18
  public            Unity.Mathematics.uint2         c2  // 0x20
  public            Unity.Mathematics.uint2         c3  // 0x28
METHODS:
  RVA=0x04D861C0  token=0x6000D1A  System.Void .ctor(Unity.Mathematics.uint2 c0, Unity.Mathematics.uint2 c1, Unity.Mathematics.uint2 c2, Unity.Mathematics.uint2 c3)
  RVA=0x04DB78A0  token=0x6000D1B  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13)
  RVA=0x04DB7980  token=0x6000D1C  System.Void .ctor(System.UInt32 v)
  RVA=0x0B2911F4  token=0x6000D1D  System.Void .ctor(System.Boolean v)
  RVA=0x0B291128  token=0x6000D1E  System.Void .ctor(Unity.Mathematics.bool2x4 v)
  RVA=0x04DB7980  token=0x6000D1F  System.Void .ctor(System.Int32 v)
  RVA=0x04DB7830  token=0x6000D20  System.Void .ctor(Unity.Mathematics.int2x4 v)
  RVA=0x05F3B1F8  token=0x6000D21  System.Void .ctor(System.Single v)
  RVA=0x05F3B238  token=0x6000D22  System.Void .ctor(Unity.Mathematics.float2x4 v)
  RVA=0x0B29C404  token=0x6000D23  System.Void .ctor(System.Double v)
  RVA=0x0B29C364  token=0x6000D24  System.Void .ctor(Unity.Mathematics.double2x4 v)
  RVA=0x0B290C4C  token=0x6000D25  System.Boolean Equals(Unity.Mathematics.uint2x4 rhs)
  RVA=0x0B29BE88  token=0x6000D26  System.Boolean Equals(System.Object o)
  RVA=0x0B29BEFC  token=0x6000D27  System.Int32 GetHashCode()
  RVA=0x0B29BF20  token=0x6000D28  System.String ToString()
  RVA=0x0B29C150  token=0x6000D29  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint3
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.UInt32                   x  // 0x10
  public            System.UInt32                   y  // 0x14
  public            System.UInt32                   z  // 0x18
PROPERTIES:
  xyzz  get=0x04DB5490
  Item  get=0x04DB7B60  set=0x04DB73A0
METHODS:
  RVA=0x04D85D60  token=0x6000D2A  System.Void .ctor(System.UInt32 x, System.UInt32 y, System.UInt32 z)
  RVA=0x04DB7AB0  token=0x6000D2B  System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint2 yz)
  RVA=0x04DB7B50  token=0x6000D2C  System.Void .ctor(Unity.Mathematics.uint2 xy, System.UInt32 z)
  RVA=0x04D88F40  token=0x6000D2D  System.Void .ctor(Unity.Mathematics.uint3 xyz)
  RVA=0x04DB7B20  token=0x6000D2E  System.Void .ctor(System.UInt32 v)
  RVA=0x0B29149C  token=0x6000D2F  System.Void .ctor(System.Boolean v)
  RVA=0x0B291470  token=0x6000D30  System.Void .ctor(Unity.Mathematics.bool3 v)
  RVA=0x04DB7B20  token=0x6000D31  System.Void .ctor(System.Int32 v)
  RVA=0x04D88F40  token=0x6000D32  System.Void .ctor(Unity.Mathematics.int3 v)
  RVA=0x05F3B2F8  token=0x6000D33  System.Void .ctor(System.Single v)
  RVA=0x05F3B2AC  token=0x6000D34  System.Void .ctor(Unity.Mathematics.float3 v)
  RVA=0x0B29C6C8  token=0x6000D35  System.Void .ctor(System.Double v)
  RVA=0x0B29C698  token=0x6000D36  System.Void .ctor(Unity.Mathematics.double3 v)
  RVA=0x04DB02F0  token=0x6000D37  Unity.Mathematics.uint3 op_Implicit(System.UInt32 v)
  RVA=0x04DB02F0  token=0x6000D38  Unity.Mathematics.uint3 op_Explicit(System.Int32 v)
  RVA=0x04DB0370  token=0x6000D39  Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.int3 v)
  RVA=0x0B275808  token=0x6000D3A  Unity.Mathematics.uint3 op_Explicit(System.Single v)
  RVA=0x05F3B330  token=0x6000D3B  Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.float3 v)
  RVA=0x0B2757E0  token=0x6000D3C  Unity.Mathematics.uint3 op_Explicit(System.Double v)
  RVA=0x0B2757A8  token=0x6000D3D  Unity.Mathematics.uint3 op_Explicit(Unity.Mathematics.double3 v)
  RVA=0x03C87E40  token=0x6000D3E  Unity.Mathematics.uint3 op_Multiply(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x04DB7C00  token=0x6000D3F  Unity.Mathematics.uint3 op_Multiply(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x0338E380  token=0x6000D40  Unity.Mathematics.uint3 op_Addition(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x04DB7BB0  token=0x6000D41  Unity.Mathematics.uint3 op_Addition(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x04DB7C70  token=0x6000D42  Unity.Mathematics.uint3 op_Subtraction(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x04DB7C50  token=0x6000D43  Unity.Mathematics.uint3 op_Subtraction(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x0B29C70C  token=0x6000D44  Unity.Mathematics.bool3 op_GreaterThan(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x04DB8880  token=0x6000D45  Unity.Mathematics.uint3 op_LeftShift(Unity.Mathematics.uint3 x, System.Int32 n)
  RVA=0x04DB88B0  token=0x6000D46  Unity.Mathematics.uint3 op_RightShift(Unity.Mathematics.uint3 x, System.Int32 n)
  RVA=0x0B29C6EC  token=0x6000D47  Unity.Mathematics.bool3 op_Equality(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x04DB8840  token=0x6000D48  Unity.Mathematics.uint3 op_BitwiseAnd(Unity.Mathematics.uint3 lhs, System.UInt32 rhs)
  RVA=0x04DB7BE0  token=0x6000D49  Unity.Mathematics.uint3 op_BitwiseOr(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x04DB8860  token=0x6000D4A  Unity.Mathematics.uint3 op_BitwiseOr(System.UInt32 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x030E9510  token=0x6000D4B  Unity.Mathematics.uint3 op_ExclusiveOr(Unity.Mathematics.uint3 lhs, Unity.Mathematics.uint3 rhs)
  RVA=0x06351A88  token=0x6000D4F  System.Boolean Equals(Unity.Mathematics.uint3 rhs)
  RVA=0x0B29C47C  token=0x6000D50  System.Boolean Equals(System.Object o)
  RVA=0x0B29C510  token=0x6000D51  System.Int32 GetHashCode()
  RVA=0x0B29C5E4  token=0x6000D52  System.String ToString()
  RVA=0x0B29C534  token=0x6000D53  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint3x2
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint3         c0  // 0x10
  public            Unity.Mathematics.uint3         c1  // 0x1c
METHODS:
  RVA=0x04D871E0  token=0x6000D54  System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1)
  RVA=0x04DB7CB0  token=0x6000D55  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21)
  RVA=0x04DB7D40  token=0x6000D56  System.Void .ctor(System.UInt32 v)
  RVA=0x05F051A0  token=0x6000D57  System.Void .ctor(System.Boolean v)
  RVA=0x0B291A00  token=0x6000D58  System.Void .ctor(Unity.Mathematics.bool3x2 v)
  RVA=0x04DB7D40  token=0x6000D59  System.Void .ctor(System.Int32 v)
  RVA=0x05F05370  token=0x6000D5A  System.Void .ctor(Unity.Mathematics.int3x2 v)
  RVA=0x05F3B37C  token=0x6000D5B  System.Void .ctor(System.Single v)
  RVA=0x05F3B3D8  token=0x6000D5C  System.Void .ctor(Unity.Mathematics.float3x2 v)
  RVA=0x0B29CB44  token=0x6000D5D  System.Void .ctor(System.Double v)
  RVA=0x0B29CBA0  token=0x6000D5E  System.Void .ctor(Unity.Mathematics.double3x2 v)
  RVA=0x066ACFB0  token=0x6000D5F  System.Boolean Equals(Unity.Mathematics.uint3x2 rhs)
  RVA=0x0B29C72C  token=0x6000D60  System.Boolean Equals(System.Object o)
  RVA=0x0B29C7A0  token=0x6000D61  System.Int32 GetHashCode()
  RVA=0x0B29C97C  token=0x6000D62  System.String ToString()
  RVA=0x0B29C7C8  token=0x6000D63  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint3x3
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x34
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint3         c0  // 0x10
  public            Unity.Mathematics.uint3         c1  // 0x1c
  public            Unity.Mathematics.uint3         c2  // 0x28
  public    static readonly Unity.Mathematics.uint3x3       identity  // static @ 0x0
METHODS:
  RVA=0x04D8C690  token=0x6000D64  System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2)
  RVA=0x04B832D0  token=0x6000D65  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22)
  RVA=0x04DB7DC0  token=0x6000D66  System.Void .ctor(System.UInt32 v)
  RVA=0x05F05648  token=0x6000D67  System.Void .ctor(System.Boolean v)
  RVA=0x0B2920DC  token=0x6000D68  System.Void .ctor(Unity.Mathematics.bool3x3 v)
  RVA=0x04DB7DC0  token=0x6000D69  System.Void .ctor(System.Int32 v)
  RVA=0x05F056D8  token=0x6000D6A  System.Void .ctor(Unity.Mathematics.int3x3 v)
  RVA=0x05F3B560  token=0x6000D6B  System.Void .ctor(System.Single v)
  RVA=0x05F3B6A4  token=0x6000D6C  System.Void .ctor(Unity.Mathematics.float3x3 v)
  RVA=0x05F3B4DC  token=0x6000D6D  System.Void .ctor(System.Double v)
  RVA=0x0B29D180  token=0x6000D6E  System.Void .ctor(Unity.Mathematics.double3x3 v)
  RVA=0x0B291B68  token=0x6000D6F  System.Boolean Equals(Unity.Mathematics.uint3x3 rhs)
  RVA=0x0B29CC30  token=0x6000D70  System.Boolean Equals(System.Object o)
  RVA=0x0B29CCAC  token=0x6000D71  System.Int32 GetHashCode()
  RVA=0x0B29CCD8  token=0x6000D72  System.String ToString()
  RVA=0x0B29CF3C  token=0x6000D73  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04B83230  token=0x6000D74  System.Void .cctor()
END_CLASS

CLASS: Unity.Mathematics.uint3x4
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint3         c0  // 0x10
  public            Unity.Mathematics.uint3         c1  // 0x1c
  public            Unity.Mathematics.uint3         c2  // 0x28
  public            Unity.Mathematics.uint3         c3  // 0x34
METHODS:
  RVA=0x04DB65D0  token=0x6000D75  System.Void .ctor(Unity.Mathematics.uint3 c0, Unity.Mathematics.uint3 c1, Unity.Mathematics.uint3 c2, Unity.Mathematics.uint3 c3)
  RVA=0x04DB8070  token=0x6000D76  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23)
  RVA=0x04DB7F80  token=0x6000D77  System.Void .ctor(System.UInt32 v)
  RVA=0x05F059CC  token=0x6000D78  System.Void .ctor(System.Boolean v)
  RVA=0x0B292A08  token=0x6000D79  System.Void .ctor(Unity.Mathematics.bool3x4 v)
  RVA=0x04DB7F80  token=0x6000D7A  System.Void .ctor(System.Int32 v)
  RVA=0x05F05824  token=0x6000D7B  System.Void .ctor(Unity.Mathematics.int3x4 v)
  RVA=0x05F3B854  token=0x6000D7C  System.Void .ctor(System.Single v)
  RVA=0x05F3B998  token=0x6000D7D  System.Void .ctor(Unity.Mathematics.float3x4 v)
  RVA=0x05F3B8F0  token=0x6000D7E  System.Void .ctor(System.Double v)
  RVA=0x0B29D8CC  token=0x6000D7F  System.Void .ctor(Unity.Mathematics.double3x4 v)
  RVA=0x0B292348  token=0x6000D80  System.Boolean Equals(Unity.Mathematics.uint3x4 rhs)
  RVA=0x0B29D24C  token=0x6000D81  System.Boolean Equals(System.Object o)
  RVA=0x0B29D2C8  token=0x6000D82  System.Int32 GetHashCode()
  RVA=0x0B29D5CC  token=0x6000D83  System.String ToString()
  RVA=0x0B29D2F8  token=0x6000D84  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint4
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            System.UInt32                   x  // 0x10
  public            System.UInt32                   y  // 0x14
  public            System.UInt32                   z  // 0x18
  public            System.UInt32                   w  // 0x1c
PROPERTIES:
  Item  get=0x04DB7B60  set=0x04DB73A0
METHODS:
  RVA=0x04D88C10  token=0x6000D85  System.Void .ctor(System.UInt32 x, System.UInt32 y, System.UInt32 z, System.UInt32 w)
  RVA=0x04DB8100  token=0x6000D86  System.Void .ctor(System.UInt32 x, System.UInt32 y, Unity.Mathematics.uint2 zw)
  RVA=0x04DB8200  token=0x6000D87  System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint2 yz, System.UInt32 w)
  RVA=0x04DB81D0  token=0x6000D88  System.Void .ctor(System.UInt32 x, Unity.Mathematics.uint3 yzw)
  RVA=0x04DB8220  token=0x6000D89  System.Void .ctor(Unity.Mathematics.uint2 xy, System.UInt32 z, System.UInt32 w)
  RVA=0x04DB8120  token=0x6000D8A  System.Void .ctor(Unity.Mathematics.uint2 xy, Unity.Mathematics.uint2 zw)
  RVA=0x04DB81B0  token=0x6000D8B  System.Void .ctor(Unity.Mathematics.uint3 xyz, System.UInt32 w)
  RVA=0x04D92B90  token=0x6000D8C  System.Void .ctor(Unity.Mathematics.uint4 xyzw)
  RVA=0x04DB8160  token=0x6000D8D  System.Void .ctor(System.UInt32 v)
  RVA=0x0B292FB0  token=0x6000D8E  System.Void .ctor(System.Boolean v)
  RVA=0x0B292FCC  token=0x6000D8F  System.Void .ctor(Unity.Mathematics.bool4 v)
  RVA=0x04DB8160  token=0x6000D90  System.Void .ctor(System.Int32 v)
  RVA=0x04D92B90  token=0x6000D91  System.Void .ctor(Unity.Mathematics.int4 v)
  RVA=0x05F3BAD0  token=0x6000D92  System.Void .ctor(System.Single v)
  RVA=0x05F3BA6C  token=0x6000D93  System.Void .ctor(Unity.Mathematics.float4 v)
  RVA=0x0B29DD10  token=0x6000D94  System.Void .ctor(System.Double v)
  RVA=0x0B29DD3C  token=0x6000D95  System.Void .ctor(Unity.Mathematics.double4 v)
  RVA=0x04DB0420  token=0x6000D96  Unity.Mathematics.uint4 op_Implicit(System.UInt32 v)
  RVA=0x04DB0420  token=0x6000D97  Unity.Mathematics.uint4 op_Explicit(System.Int32 v)
  RVA=0x0B25AC78  token=0x6000D98  Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.int4 v)
  RVA=0x0B275B1C  token=0x6000D99  Unity.Mathematics.uint4 op_Explicit(System.Single v)
  RVA=0x0B275B64  token=0x6000D9A  Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.float4 v)
  RVA=0x0B275B34  token=0x6000D9B  Unity.Mathematics.uint4 op_Explicit(System.Double v)
  RVA=0x0B275AD4  token=0x6000D9C  Unity.Mathematics.uint4 op_Explicit(Unity.Mathematics.double4 v)
  RVA=0x04DB82A0  token=0x6000D9D  Unity.Mathematics.uint4 op_Multiply(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x04DB82D0  token=0x6000D9E  Unity.Mathematics.uint4 op_Multiply(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x04DB8950  token=0x6000D9F  Unity.Mathematics.uint4 op_Multiply(System.UInt32 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x04DB8270  token=0x6000DA0  Unity.Mathematics.uint4 op_Addition(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x04DB8240  token=0x6000DA1  Unity.Mathematics.uint4 op_Addition(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x04DB89D0  token=0x6000DA2  Unity.Mathematics.uint4 op_Subtraction(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x04DB8330  token=0x6000DA3  Unity.Mathematics.uint4 op_Subtraction(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x0B29DD9C  token=0x6000DA4  Unity.Mathematics.bool4 op_GreaterThan(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x04DB8920  token=0x6000DA5  Unity.Mathematics.uint4 op_LeftShift(Unity.Mathematics.uint4 x, System.Int32 n)
  RVA=0x04DB8980  token=0x6000DA6  Unity.Mathematics.uint4 op_RightShift(Unity.Mathematics.uint4 x, System.Int32 n)
  RVA=0x0B29DD78  token=0x6000DA7  Unity.Mathematics.bool4 op_Equality(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x034C9860  token=0x6000DA8  Unity.Mathematics.uint4 op_OnesComplement(Unity.Mathematics.uint4 val)
  RVA=0x034C9800  token=0x6000DA9  Unity.Mathematics.uint4 op_BitwiseAnd(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x04DB88F0  token=0x6000DAA  Unity.Mathematics.uint4 op_BitwiseAnd(Unity.Mathematics.uint4 lhs, System.UInt32 rhs)
  RVA=0x034C9890  token=0x6000DAB  Unity.Mathematics.uint4 op_BitwiseOr(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x034C9830  token=0x6000DAC  Unity.Mathematics.uint4 op_ExclusiveOr(Unity.Mathematics.uint4 lhs, Unity.Mathematics.uint4 rhs)
  RVA=0x068AFB90  token=0x6000DAF  System.Boolean Equals(Unity.Mathematics.uint4 rhs)
  RVA=0x0B29D9D8  token=0x6000DB0  System.Boolean Equals(System.Object o)
  RVA=0x0B29DA44  token=0x6000DB1  System.Int32 GetHashCode()
  RVA=0x0B29DBB4  token=0x6000DB2  System.String ToString()
  RVA=0x0B29DA60  token=0x6000DB3  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint4x2
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint4         c0  // 0x10
  public            Unity.Mathematics.uint4         c1  // 0x20
METHODS:
  RVA=0x04D8C810  token=0x6000DB4  System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1)
  RVA=0x04DB83B0  token=0x6000DB5  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m10, System.UInt32 m11, System.UInt32 m20, System.UInt32 m21, System.UInt32 m30, System.UInt32 m31)
  RVA=0x03BACBE0  token=0x6000DB6  System.Void .ctor(System.UInt32 v)
  RVA=0x05F3BC08  token=0x6000DB7  System.Void .ctor(System.Boolean v)
  RVA=0x05F3BD80  token=0x6000DB8  System.Void .ctor(Unity.Mathematics.bool4x2 v)
  RVA=0x03BACBE0  token=0x6000DB9  System.Void .ctor(System.Int32 v)
  RVA=0x0B2935DC  token=0x6000DBA  System.Void .ctor(Unity.Mathematics.int4x2 v)
  RVA=0x05F3BCE8  token=0x6000DBB  System.Void .ctor(System.Single v)
  RVA=0x0B29E338  token=0x6000DBC  System.Void .ctor(Unity.Mathematics.float4x2 v)
  RVA=0x05F3BC7C  token=0x6000DBD  System.Void .ctor(System.Double v)
  RVA=0x0B29E29C  token=0x6000DBE  System.Void .ctor(Unity.Mathematics.double4x2 v)
  RVA=0x0B293058  token=0x6000DBF  System.Boolean Equals(Unity.Mathematics.uint4x2 rhs)
  RVA=0x0B29DDC0  token=0x6000DC0  System.Boolean Equals(System.Object o)
  RVA=0x0B29DE34  token=0x6000DC1  System.Int32 GetHashCode()
  RVA=0x0B29DE58  token=0x6000DC2  System.String ToString()
  RVA=0x0B29E088  token=0x6000DC3  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint4x3
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x40
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint4         c0  // 0x10
  public            Unity.Mathematics.uint4         c1  // 0x20
  public            Unity.Mathematics.uint4         c2  // 0x30
METHODS:
  RVA=0x04DA0760  token=0x6000DC4  System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2)
  RVA=0x04DB84E0  token=0x6000DC5  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32)
  RVA=0x04DB8560  token=0x6000DC6  System.Void .ctor(System.UInt32 v)
  RVA=0x05F3C1BC  token=0x6000DC7  System.Void .ctor(System.Boolean v)
  RVA=0x05F3BF60  token=0x6000DC8  System.Void .ctor(Unity.Mathematics.bool4x3 v)
  RVA=0x04DB8560  token=0x6000DC9  System.Void .ctor(System.Int32 v)
  RVA=0x0B293D24  token=0x6000DCA  System.Void .ctor(Unity.Mathematics.int4x3 v)
  RVA=0x05F3C06C  token=0x6000DCB  System.Void .ctor(System.Single v)
  RVA=0x0B29EA0C  token=0x6000DCC  System.Void .ctor(Unity.Mathematics.float4x3 v)
  RVA=0x05F3BEC0  token=0x6000DCD  System.Void .ctor(System.Double v)
  RVA=0x0B29EA84  token=0x6000DCE  System.Void .ctor(Unity.Mathematics.double4x3 v)
  RVA=0x0B2936C0  token=0x6000DCF  System.Boolean Equals(Unity.Mathematics.uint4x3 rhs)
  RVA=0x0B29E38C  token=0x6000DD0  System.Boolean Equals(System.Object o)
  RVA=0x0B29E408  token=0x6000DD1  System.Int32 GetHashCode()
  RVA=0x0B29E438  token=0x6000DD2  System.String ToString()
  RVA=0x0B29E738  token=0x6000DD3  System.String ToString(System.String format, System.IFormatProvider formatProvider)
END_CLASS

CLASS: Unity.Mathematics.uint4x4
TYPE:  sealed struct
TOKEN: 0x200005E
SIZE:  0x50
IMPLEMENTS: System.IEquatable`1 System.IFormattable
FIELDS:
  public            Unity.Mathematics.uint4         c0  // 0x10
  public            Unity.Mathematics.uint4         c1  // 0x20
  public            Unity.Mathematics.uint4         c2  // 0x30
  public            Unity.Mathematics.uint4         c3  // 0x40
  public    static readonly Unity.Mathematics.uint4x4       identity  // static @ 0x0
METHODS:
  RVA=0x04DB3220  token=0x6000DD4  System.Void .ctor(Unity.Mathematics.uint4 c0, Unity.Mathematics.uint4 c1, Unity.Mathematics.uint4 c2, Unity.Mathematics.uint4 c3)
  RVA=0x04A3ADB0  token=0x6000DD5  System.Void .ctor(System.UInt32 m00, System.UInt32 m01, System.UInt32 m02, System.UInt32 m03, System.UInt32 m10, System.UInt32 m11, System.UInt32 m12, System.UInt32 m13, System.UInt32 m20, System.UInt32 m21, System.UInt32 m22, System.UInt32 m23, System.UInt32 m30, System.UInt32 m31, System.UInt32 m32, System.UInt32 m33)
  RVA=0x04DB8690  token=0x6000DD6  System.Void .ctor(System.UInt32 v)
  RVA=0x05F3C260  token=0x6000DD7  System.Void .ctor(System.Boolean v)
  RVA=0x05F3C5B4  token=0x6000DD8  System.Void .ctor(Unity.Mathematics.bool4x4 v)
  RVA=0x04DB8690  token=0x6000DD9  System.Void .ctor(System.Int32 v)
  RVA=0x0B2946D4  token=0x6000DDA  System.Void .ctor(Unity.Mathematics.int4x4 v)
  RVA=0x05F3C404  token=0x6000DDB  System.Void .ctor(System.Single v)
  RVA=0x0B29F4BC  token=0x6000DDC  System.Void .ctor(Unity.Mathematics.float4x4 v)
  RVA=0x05F3C334  token=0x6000DDD  System.Void .ctor(System.Double v)
  RVA=0x0B29F38C  token=0x6000DDE  System.Void .ctor(Unity.Mathematics.double4x4 v)
  RVA=0x0B293EBC  token=0x6000DDF  System.Boolean Equals(Unity.Mathematics.uint4x4 rhs)
  RVA=0x0B29EB6C  token=0x6000DE0  System.Boolean Equals(System.Object o)
  RVA=0x0B29EBF0  token=0x6000DE1  System.Int32 GetHashCode()
  RVA=0x0B29EC28  token=0x6000DE2  System.String ToString()
  RVA=0x0B29EFF8  token=0x6000DE3  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x04A3ACC0  token=0x6000DE4  System.Void .cctor()
END_CLASS

