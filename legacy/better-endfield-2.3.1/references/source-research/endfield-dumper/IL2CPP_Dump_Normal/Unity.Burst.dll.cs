// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Burst.dll
// Classes:  52
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000009  // size: 0x18
    public class StaticTypeReinitAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type reinitType;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000013
        public System.Void .ctor(System.Type toReinit) { }

    }

    // TypeToken: 0x200000B  // size: 0x80
    public sealed class IsBurstEnabledDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03FAEAA0  token: 0x6000018
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x07378308  token: 0x6000019
        public virtual System.Boolean Invoke() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public static class BurstCompilerHelper
    {
        // Fields
        private static readonly Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate IsBurstEnabledImpl;  // static @ 0x0
        public static readonly System.Boolean IsBurstGenerated;  // static @ 0x8

        // Methods
        // RVA: 0x0B15C238  token: 0x6000014
        private static System.Boolean IsBurstEnabled() { }
        // RVA: 0x04D87170  token: 0x6000015
        private static System.Void DiscardedMethod(System.Boolean& value) { }
        // RVA: 0x03FB0A20  token: 0x6000016
        private static System.Boolean IsCompiledByBurst(System.Delegate del) { }
        // RVA: 0x03FAEB30  token: 0x6000017
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public class FakeDelegate
    {
        // Fields
        private readonly System.Reflection.MethodInfo <Method>k__BackingField;  // 0x10

        // Properties
        System.Reflection.MethodInfo Method { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600001A
        public System.Void .ctor(System.Reflection.MethodInfo method) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.Burst.BurstCompiler.<>c <>9;  // static @ 0x0
        public static System.Func<System.Attribute,System.Boolean> <>9__22_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D45010  token: 0x600001C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600001D
        public System.Void .ctor() { }
        // RVA: 0x04843A60  token: 0x600001E
        private System.Boolean <Compile>b__22_0(System.Attribute s) { }

    }

    // TypeToken: 0x2000010  // size: 0x11
    public sealed struct HashCode64`1
    {
        // Fields
        public static readonly System.Int64 Value;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000030
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public class PreserveAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000031
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class PreserveAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000061
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct NumberBufferKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Burst.BurstString.NumberBufferKind Integer;  // const
        public static Unity.Burst.BurstString.NumberBufferKind Float;  // const

    }

    // TypeToken: 0x2000015  // size: 0x28
    public sealed struct NumberBuffer
    {
        // Fields
        private readonly System.Byte* _buffer;  // 0x10
        public Unity.Burst.BurstString.NumberBufferKind Kind;  // 0x18
        public System.Int32 DigitsCount;  // 0x1c
        public System.Int32 Scale;  // 0x20
        public readonly System.Boolean IsNegative;  // 0x24

        // Methods
        // RVA: 0x04DAB260  token: 0x6000062
        public System.Void .ctor(Unity.Burst.BurstString.NumberBufferKind kind, System.Byte* buffer, System.Int32 digitsCount, System.Int32 scale, System.Boolean isNegative) { }
        // RVA: 0x02B76770  token: 0x6000063
        public System.Byte* GetDigitsPointer() { }

    }

    // TypeToken: 0x2000016  // size: 0x11
    public sealed struct NumberFormatKind
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Unity.Burst.BurstString.NumberFormatKind General;  // const
        public static Unity.Burst.BurstString.NumberFormatKind Decimal;  // const
        public static Unity.Burst.BurstString.NumberFormatKind DecimalForceSigned;  // const
        public static Unity.Burst.BurstString.NumberFormatKind Hexadecimal;  // const

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct FormatOptions
    {
        // Fields
        public Unity.Burst.BurstString.NumberFormatKind Kind;  // 0x10
        public System.SByte AlignAndSize;  // 0x11
        public System.Byte Specifier;  // 0x12
        public System.Boolean Lowercase;  // 0x13

        // Properties
        System.Boolean Uppercase { get; /* RVA: 0x0AE38EF0 */ }

        // Methods
        // RVA: 0x04DAB240  token: 0x6000064
        public System.Void .ctor(Unity.Burst.BurstString.NumberFormatKind kind, System.SByte alignAndSize, System.Byte specifier, System.Boolean lowercase) { }
        // RVA: 0x0B15F580  token: 0x6000066
        public System.Int32 GetBase() { }
        // RVA: 0x0B15F594  token: 0x6000067
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000019  // size: 0x9C
    public sealed struct <m_blocks>e__FixedBuffer
    {
        // Fields
        public System.UInt32 FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000018  // size: 0xA0
    public sealed struct tBigInt
    {
        // Fields
        public System.Int32 m_length;  // 0x10
        public Unity.Burst.BurstString.tBigInt.<m_blocks>e__FixedBuffer m_blocks;  // 0x14

        // Methods
        // RVA: 0x02B77620  token: 0x6000068
        public System.Int32 GetLength() { }
        // RVA: 0x04DAB360  token: 0x6000069
        public System.UInt32 GetBlock(System.Int32 idx) { }
        // RVA: 0x0426F6A0  token: 0x600006A
        public System.Boolean IsZero() { }
        // RVA: 0x0B15F91C  token: 0x600006B
        public System.Void SetU64(System.UInt64 val) { }
        // RVA: 0x0B15F908  token: 0x600006C
        public System.Void SetU32(System.UInt32 val) { }

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct CutoffMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Burst.BurstString.CutoffMode Unique;  // const
        public static Unity.Burst.BurstString.CutoffMode TotalLength;  // const
        public static Unity.Burst.BurstString.CutoffMode FractionLength;  // const

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct tFloatUnion32
    {
        // Fields
        public System.Single m_floatingPoint;  // 0x10
        public System.UInt32 m_integer;  // 0x10

        // Methods
        // RVA: 0x0A879674  token: 0x600006D
        public System.Boolean IsNegative() { }
        // RVA: 0x04DAB370  token: 0x600006E
        public System.UInt32 GetExponent() { }
        // RVA: 0x04DAB380  token: 0x600006F
        public System.UInt32 GetMantissa() { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed struct tFloatUnion64
    {
        // Fields
        public System.Double m_floatingPoint;  // 0x10
        public System.UInt64 m_integer;  // 0x10

        // Methods
        // RVA: 0x0B15F94C  token: 0x6000070
        public System.Boolean IsNegative() { }
        // RVA: 0x04DAB390  token: 0x6000071
        public System.UInt32 GetExponent() { }
        // RVA: 0x04DAB3A0  token: 0x6000072
        public System.UInt64 GetMantissa() { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public class PreserveAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600007D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x10
    public static class Avx
    {
        // Methods
        // RVA: 0x04D91D40  token: 0x6000081
        public static Unity.Burst.Intrinsics.v256 mm256_load_ps(System.Void* ptr) { }
        // RVA: 0x04DAB1C0  token: 0x6000082
        public static System.Void mm256_store_ps(System.Void* ptr, Unity.Burst.Intrinsics.v256 val) { }
        // RVA: 0x04D91D40  token: 0x6000083
        public static Unity.Burst.Intrinsics.v256 mm256_loadu_si256(System.Void* ptr) { }
        // RVA: 0x04DAB1C0  token: 0x6000084
        public static System.Void mm256_storeu_si256(System.Void* ptr, Unity.Burst.Intrinsics.v256 v) { }
        // RVA: 0x0B15C1C8  token: 0x6000085
        public static Unity.Burst.Intrinsics.v256 mm256_set1_epi32(System.Int32 a) { }

    }

    // TypeToken: 0x200002C  // size: 0x10
    public static class Avx2
    {
        // Properties
        System.Boolean IsAvx2Supported { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0B15C12C  token: 0x6000087
        public static Unity.Burst.Intrinsics.v256 mm256_xor_si256(Unity.Burst.Intrinsics.v256 a, Unity.Burst.Intrinsics.v256 b) { }
        // RVA: 0x0B15BE80  token: 0x6000088
        public static Unity.Burst.Intrinsics.v256 mm256_add_epi64(Unity.Burst.Intrinsics.v256 a, Unity.Burst.Intrinsics.v256 b) { }
        // RVA: 0x0B15BF1C  token: 0x6000089
        public static Unity.Burst.Intrinsics.v256 mm256_mul_epu32(Unity.Burst.Intrinsics.v256 a, Unity.Burst.Intrinsics.v256 b) { }
        // RVA: 0x0B15C034  token: 0x600008A
        public static Unity.Burst.Intrinsics.v256 mm256_slli_epi64(Unity.Burst.Intrinsics.v256 a, System.Int32 imm8) { }
        // RVA: 0x0B15C0B0  token: 0x600008B
        public static Unity.Burst.Intrinsics.v256 mm256_srli_epi64(Unity.Burst.Intrinsics.v256 a, System.Int32 imm8) { }
        // RVA: 0x0B15BFB8  token: 0x600008C
        public static Unity.Burst.Intrinsics.v256 mm256_shuffle_epi32(Unity.Burst.Intrinsics.v256 a, System.Int32 imm8) { }

    }

    // TypeToken: 0x200002D  // size: 0x10
    public static class Sse
    {
        // Methods
        // RVA: 0x04DAB330  token: 0x600008D
        public static System.Int32 SHUFFLE(System.Int32 d, System.Int32 c, System.Int32 b, System.Int32 a) { }

    }

    // TypeToken: 0x200002E  // size: 0x10
    public static class Sse2
    {
        // Methods
        // RVA: 0x04DAB280  token: 0x600008E
        public static Unity.Burst.Intrinsics.v128 add_epi64(Unity.Burst.Intrinsics.v128 a, Unity.Burst.Intrinsics.v128 b) { }
        // RVA: 0x04DAB2A0  token: 0x600008F
        public static Unity.Burst.Intrinsics.v128 mul_epu32(Unity.Burst.Intrinsics.v128 a, Unity.Burst.Intrinsics.v128 b) { }
        // RVA: 0x0B15F7D0  token: 0x6000090
        public static Unity.Burst.Intrinsics.v128 slli_epi64(Unity.Burst.Intrinsics.v128 a, System.Int32 imm8) { }
        // RVA: 0x0B15F86C  token: 0x6000091
        public static Unity.Burst.Intrinsics.v128 srli_epi64(Unity.Burst.Intrinsics.v128 a, System.Int32 imm8) { }
        // RVA: 0x04DAB310  token: 0x6000092
        public static Unity.Burst.Intrinsics.v128 xor_si128(Unity.Burst.Intrinsics.v128 a, Unity.Burst.Intrinsics.v128 b) { }
        // RVA: 0x04DAB2C0  token: 0x6000093
        public static Unity.Burst.Intrinsics.v128 shuffle_epi32(Unity.Burst.Intrinsics.v128 a, System.Int32 imm8) { }

    }

    // TypeToken: 0x2000032  // size: 0x13
    public sealed struct __StaticArrayInitTypeSize=3
    {
    }

    // TypeToken: 0x2000033  // size: 0x30
    public sealed struct __StaticArrayInitTypeSize=32
    {
    }

    // TypeToken: 0x2000034  // size: 0x110
    public sealed struct __StaticArrayInitTypeSize=256
    {
    }

    // TypeToken: 0x2000031  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 07DB995E8ED2CFB0AB71EBA69F3A3EC07D5C6AC10C0C64F33E94ED2949B348AA;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 C69994AC61B52FBCEA582D6CCCD595C12E00BDB18F0C6F593FB6B393CAEDB08C;  // static @ 0x100
        private static readonly System.Int64 D0067CAD9A63E0813759A2BB841051CA73570C0DA2E08E840A8EB45DB6A7A010;  // static @ 0x120
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 D5B592C05DC25B5032553F1B27F4139BE95E881F73DB33B02B05AB20C3F9981E;  // static @ 0x128

    }

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsReadOnlyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace Unity.Burst
{

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct OptimizeFor
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Burst.OptimizeFor Default;  // const
        public static Unity.Burst.OptimizeFor Performance;  // const
        public static Unity.Burst.OptimizeFor Size;  // const
        public static Unity.Burst.OptimizeFor FastCompilation;  // const
        public static Unity.Burst.OptimizeFor Balanced;  // const

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct FloatMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Burst.FloatMode Default;  // const
        public static Unity.Burst.FloatMode Strict;  // const
        public static Unity.Burst.FloatMode Deterministic;  // const
        public static Unity.Burst.FloatMode Fast;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct FloatPrecision
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Burst.FloatPrecision Standard;  // const
        public static Unity.Burst.FloatPrecision High;  // const
        public static Unity.Burst.FloatPrecision Medium;  // const
        public static Unity.Burst.FloatPrecision Low;  // const

    }

    // TypeToken: 0x2000007  // size: 0x28
    public class BurstCompileAttribute : System.Attribute
    {
        // Fields
        private Unity.Burst.FloatMode <FloatMode>k__BackingField;  // 0x10
        private Unity.Burst.FloatPrecision <FloatPrecision>k__BackingField;  // 0x14
        private System.Nullable<System.Boolean> _compileSynchronously;  // 0x18
        private System.Nullable<System.Boolean> _disableDirectCall;  // 0x1a
        private Unity.Burst.OptimizeFor <OptimizeFor>k__BackingField;  // 0x1c
        private System.String[] <Options>k__BackingField;  // 0x20

        // Properties
        Unity.Burst.FloatMode FloatMode { set; /* RVA: 0x014F51F0 */ }
        Unity.Burst.FloatPrecision FloatPrecision { set; /* RVA: 0x04D8F540 */ }
        System.Boolean CompileSynchronously { set; /* RVA: 0x03F26250 */ }
        System.Boolean DisableDirectCall { set; /* RVA: 0x0B15C1EC */ }
        Unity.Burst.OptimizeFor OptimizeFor { set; /* RVA: 0x04D86320 */ }
        System.String[] Options { set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000009
        public System.Void .ctor() { }
        // RVA: 0x04D85930  token: 0x600000A
        public System.Void .ctor(Unity.Burst.FloatPrecision floatPrecision, Unity.Burst.FloatMode floatMode) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public static class BurstCompiler
    {
        // Fields
        private static System.Boolean _IsEnabled;  // static @ 0x0
        public static readonly Unity.Burst.BurstCompilerOptions Options;  // static @ 0x8
        private static System.Action OnCompileILPPMethod2;  // static @ 0x10
        private static readonly System.Reflection.MethodInfo DummyMethodInfo;  // static @ 0x18

        // Properties
        System.Boolean IsEnabled { get; /* RVA: 0x0307B8D0 */ }

        // Methods
        // RVA: 0x03FB0BC0  token: 0x600000C
        public static System.IntPtr CompileILPPMethod2(System.RuntimeMethodHandle burstMethodHandle) { }
        // RVA: 0x0474F6F0  token: 0x600000D
        public static System.Void* GetILPPMethodFunctionPointer2(System.IntPtr ilppMethod, System.RuntimeMethodHandle managedMethodHandle, System.RuntimeTypeHandle delegateTypeHandle) { }
        // RVA: -1  // generic def  token: 0x600000E
        public static Unity.Burst.FunctionPointer<T> CompileFunctionPointer(T delegateMethod) { }
        // RVA: 0x03FB0AF0  token: 0x600000F
        private static System.Void* Compile(System.Object delegateObj, System.Boolean isFunctionPointer) { }
        // RVA: 0x03FB0D30  token: 0x6000010
        private static System.Void* Compile(System.Object delegateObj, System.Reflection.MethodInfo methodInfo, System.Boolean isFunctionPointer, System.Boolean isILPostProcessing) { }
        // RVA: 0x041E1670  token: 0x6000011
        private static System.Void DummyMethod() { }
        // RVA: 0x0495EE80  token: 0x6000012
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed class BurstCompilerOptions
    {
        // Fields
        private static readonly System.Boolean ForceDisableBurstCompilation;  // static @ 0x0
        private static readonly System.Boolean ForceBurstCompilationSynchronously;  // static @ 0x1
        private static readonly System.Boolean IsSecondaryUnityProcess;  // static @ 0x2
        private System.Boolean _enableBurstCompilation;  // 0x10
        private System.Boolean _enableBurstSafetyChecks;  // 0x11
        private readonly System.Boolean <IsGlobal>k__BackingField;  // 0x12
        private System.Action <OptionsChanged>k__BackingField;  // 0x18

        // Properties
        System.Boolean IsGlobal { get; /* RVA: 0x04D88390 */ }
        System.Boolean EnableBurstCompilation { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x0495EFE0 */ }
        System.Boolean EnableBurstSafetyChecks { set; /* RVA: 0x0495F0D0 */ }
        System.Action OptionsChanged { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0495EFA0  token: 0x600001F
        private System.Void .ctor(System.Boolean isGlobal) { }
        // RVA: 0x03FB10C0  token: 0x6000025
        private static System.Boolean TryGetAttribute(System.Reflection.MemberInfo member, Unity.Burst.BurstCompileAttribute& attribute) { }
        // RVA: 0x03FB1140  token: 0x6000026
        private static Unity.Burst.BurstCompileAttribute GetBurstCompileAttribute(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x03FB1060  token: 0x6000027
        private static System.Boolean HasBurstCompileAttribute(System.Reflection.MemberInfo member) { }
        // RVA: 0x0495F0F0  token: 0x6000028
        private System.Void OnOptionsChanged() { }
        // RVA: 0x041E1670  token: 0x6000029
        private System.Void MaybeTriggerRecompilation() { }
        // RVA: 0x041A6310  token: 0x600002A
        private static System.Void .cctor() { }
        // RVA: 0x012081B0  token: 0x600002B
        private static System.Boolean CheckIsSecondaryUnityProcess() { }

    }

    // TypeToken: 0x200000F  // size: 0x10
    public static class BurstRuntime
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600002C
        public static System.Int64 GetHashCode64() { }
        // RVA: 0x03B99BA0  token: 0x600002D
        private static System.Int64 HashStringWithFNV1A64(System.String text) { }
        // RVA: 0x0B15C298  token: 0x600002E
        private static System.Void PreventRequiredAttributeStrip() { }
        // RVA: 0x0B15C274  token: 0x600002F
        private static System.Void Log(System.Byte* message, System.Int32 logType, System.Byte* fileName, System.Int32 lineNumber) { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public static class BurstString
    {
        // Fields
        private static readonly System.Char[] SplitByColon;  // static @ 0x0
        private static readonly System.Byte[] logTable;  // static @ 0x8
        private static readonly System.UInt32[] g_PowerOf10_U32;  // static @ 0x10
        private static readonly System.Byte[] InfinityString;  // static @ 0x18
        private static readonly System.Byte[] NanString;  // static @ 0x20

        // Methods
        // RVA: 0x0B15D844  token: 0x6000032
        public static System.Void CopyFixedString(System.Byte* dest, System.Int32 destLength, System.Byte* src, System.Int32 srcLength) { }
        // RVA: 0x0B15E9CC  token: 0x6000033
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Byte* src, System.Int32 srcLength, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15ED80  token: 0x6000034
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Single value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15EB24  token: 0x6000035
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Double value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15EBA0  token: 0x6000036
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Boolean value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E54C  token: 0x6000037
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Char value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15EDFC  token: 0x6000038
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Byte value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E784  token: 0x6000039
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.UInt16 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15EAA8  token: 0x600003A
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.UInt32 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E708  token: 0x600003B
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.UInt64 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E804  token: 0x600003C
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.SByte value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E934  token: 0x600003D
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int16 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15E89C  token: 0x600003E
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int32 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15ECEC  token: 0x600003F
        public static System.Void Format(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int64 value, System.Int32 formatOptionsRaw) { }
        // RVA: 0x0B15D6D4  token: 0x6000040
        private static System.Void ConvertUnsignedIntegerToString(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.UInt64 value, Unity.Burst.BurstString.FormatOptions options) { }
        // RVA: 0x0B15EF50  token: 0x6000041
        private static System.Int32 GetLengthIntegerToString(System.Int64 value, System.Int32 basis, System.Int32 zeroPadding) { }
        // RVA: 0x0B15D53C  token: 0x6000042
        private static System.Void ConvertIntegerToString(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int64 value, Unity.Burst.BurstString.FormatOptions options) { }
        // RVA: 0x0B15E34C  token: 0x6000043
        private static System.Void FormatNumber(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, Unity.Burst.BurstString.NumberBuffer& number, System.Int32 nMaxDigits, Unity.Burst.BurstString.FormatOptions options) { }
        // RVA: 0x0B15DF98  token: 0x6000044
        private static System.Void FormatDecimalOrHexadecimal(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, Unity.Burst.BurstString.NumberBuffer& number, System.Int32 zeroPadding, System.Boolean outputPositiveSign) { }
        // RVA: 0x0B15F188  token: 0x6000045
        private static System.Byte ValueToIntegerChar(System.Int32 value, System.Boolean uppercase) { }
        // RVA: 0x0B15C3A4  token: 0x6000046
        private static System.Boolean AlignRight(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int32 align, System.Int32 length) { }
        // RVA: 0x0B15C374  token: 0x6000047
        private static System.Boolean AlignLeft(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Int32 align, System.Int32 length) { }
        // RVA: 0x0B15EE7C  token: 0x6000048
        private static System.Int32 GetLengthForFormatGeneral(Unity.Burst.BurstString.NumberBuffer& number, System.Int32 nMaxDigits) { }
        // RVA: 0x0B15E038  token: 0x6000049
        private static System.Void FormatGeneral(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, Unity.Burst.BurstString.NumberBuffer& number, System.Int32 nMaxDigits, System.Byte expChar) { }
        // RVA: 0x0B15F070  token: 0x600004A
        private static System.Void RoundNumber(Unity.Burst.BurstString.NumberBuffer& number, System.Int32 pos, System.Boolean isCorrectlyRounded) { }
        // RVA: 0x0B15F164  token: 0x600004B
        private static System.Boolean ShouldRoundUp(System.Byte* dig, System.Int32 i, System.Boolean isCorrectlyRounded) { }
        // RVA: 0x0B15EF7C  token: 0x600004C
        private static System.UInt32 LogBase2(System.UInt32 val) { }
        // RVA: 0x0B15C54C  token: 0x600004D
        private static System.Int32 BigInt_Compare(Unity.Burst.BurstString.tBigInt& lhs, Unity.Burst.BurstString.tBigInt& rhs) { }
        // RVA: 0x0B15C4D8  token: 0x600004E
        private static System.Void BigInt_Add(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& lhs, Unity.Burst.BurstString.tBigInt& rhs) { }
        // RVA: 0x0B15C42C  token: 0x600004F
        private static System.Void BigInt_Add_internal(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& pLarge, Unity.Burst.BurstString.tBigInt& pSmall) { }
        // RVA: 0x0B15CBEC  token: 0x6000050
        private static System.Void BigInt_Multiply(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& lhs, Unity.Burst.BurstString.tBigInt& rhs) { }
        // RVA: 0x0B15CAAC  token: 0x6000051
        private static System.Void BigInt_Multiply_internal(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& pLarge, Unity.Burst.BurstString.tBigInt& pSmall) { }
        // RVA: 0x0B15CB84  token: 0x6000052
        private static System.Void BigInt_Multiply(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& lhs, System.UInt32 rhs) { }
        // RVA: 0x0B15C744  token: 0x6000053
        private static System.Void BigInt_Multiply2(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& input) { }
        // RVA: 0x0B15C79C  token: 0x6000054
        private static System.Void BigInt_Multiply2(Unity.Burst.BurstString.tBigInt& pResult) { }
        // RVA: 0x0B15C708  token: 0x6000055
        private static System.Void BigInt_Multiply10(Unity.Burst.BurstString.tBigInt& pResult) { }
        // RVA: 0x0B15F3BC  token: 0x6000056
        private static Unity.Burst.BurstString.tBigInt g_PowerOf10_Big(System.Int32 i) { }
        // RVA: 0x0B15CC60  token: 0x6000057
        private static System.Void BigInt_Pow10(Unity.Burst.BurstString.tBigInt& pResult, System.UInt32 exponent) { }
        // RVA: 0x0B15C7D8  token: 0x6000058
        private static System.Void BigInt_MultiplyPow10(Unity.Burst.BurstString.tBigInt& pResult, Unity.Burst.BurstString.tBigInt& input, System.UInt32 exponent) { }
        // RVA: 0x0B15CECC  token: 0x6000059
        private static System.Void BigInt_Pow2(Unity.Burst.BurstString.tBigInt& pResult, System.UInt32 exponent) { }
        // RVA: 0x0B15C5A0  token: 0x600005A
        private static System.UInt32 BigInt_DivideWithRemainder_MaxQuotient9(Unity.Burst.BurstString.tBigInt& pDividend, Unity.Burst.BurstString.tBigInt& divisor) { }
        // RVA: 0x0B15CF14  token: 0x600005B
        private static System.Void BigInt_ShiftLeft(Unity.Burst.BurstString.tBigInt& pResult, System.UInt32 shift) { }
        // RVA: 0x0B15D868  token: 0x600005C
        private static System.UInt32 Dragon4(System.UInt64 mantissa, System.Int32 exponent, System.UInt32 mantissaHighBitIdx, System.Boolean hasUnequalMargins, Unity.Burst.BurstString.CutoffMode cutoffMode, System.UInt32 cutoffNumber, System.Byte* pOutBuffer, System.UInt32 bufferSize, System.Int32& pOutExponent) { }
        // RVA: 0x0B15E1B0  token: 0x600005D
        private static System.Void FormatInfinityNaN(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.UInt64 mantissa, System.Boolean isNegative, Unity.Burst.BurstString.FormatOptions formatOptions) { }
        // RVA: 0x0B15D2E0  token: 0x600005E
        private static System.Void ConvertFloatToString(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Single value, Unity.Burst.BurstString.FormatOptions formatOptions) { }
        // RVA: 0x0B15D060  token: 0x600005F
        private static System.Void ConvertDoubleToString(System.Byte* dest, System.Int32& destIndex, System.Int32 destLength, System.Double value, Unity.Burst.BurstString.FormatOptions formatOptions) { }
        // RVA: 0x0B15F1B0  token: 0x6000060
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001D
    public sealed struct FunctionPointer`1
    {
        // Fields
        private readonly System.IntPtr _ptr;  // 0x0

        // Properties
        T Invoke { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000073
        public System.Void .ctor(System.IntPtr ptr) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public class NoAliasAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000075
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F
    public sealed struct SharedStatic`1
    {
        // Fields
        private readonly System.Void* _buffer;  // 0x0

        // Properties
        T& Data { get; /* RVA: -1  // not resolved */ }
        System.Void* UnsafeDataPointer { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000076
        private System.Void .ctor(System.Void* buffer) { }
        // RVA: -1  // generic def  token: 0x6000079
        public static Unity.Burst.SharedStatic<T> GetOrCreate(System.UInt32 alignment) { }
        // RVA: -1  // generic def  token: 0x600007A
        public static Unity.Burst.SharedStatic<T> GetOrCreate(System.UInt32 alignment) { }
        // RVA: -1  // not resolved  token: 0x600007B
        public static Unity.Burst.SharedStatic<T> GetOrCreateUnsafe(System.UInt32 alignment, System.Int64 hashCode, System.Int64 subHashCode) { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public static class SharedStatic
    {
        // Methods
        // RVA: 0x042094B0  token: 0x600007C
        public static System.Void* GetOrCreateSharedStaticInternal(System.Int64 getHashCode64, System.Int64 getSubHashCode64, System.UInt32 sizeOf, System.UInt32 alignment) { }

    }

}

namespace Unity.Burst.CompilerServices
{

    // TypeToken: 0x200002F  // size: 0x10
    public class AssumeRangeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000094
        public System.Void .ctor(System.Int64 min, System.Int64 max) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public static class Hint
    {
        // Methods
        // RVA: 0x04D94BA0  token: 0x6000095
        public static System.Boolean Likely(System.Boolean condition) { }

    }

}

namespace Unity.Burst.Intrinsics
{

    // TypeToken: 0x2000022  // size: 0x10
    public static class Common
    {
        // Methods
        // RVA: 0x04DAB1D0  token: 0x600007E
        public static System.UInt64 umul128(System.UInt64 x, System.UInt64 y, System.UInt64& high) { }

    }

    // TypeToken: 0x2000023  // size: 0x10
    public sealed class BurstTargetCpuAttribute : System.Attribute
    {
    }

    // TypeToken: 0x2000024  // size: 0x10
    public class V64DebugView
    {
    }

    // TypeToken: 0x2000025  // size: 0x10
    public class V128DebugView
    {
    }

    // TypeToken: 0x2000026  // size: 0x10
    public class V256DebugView
    {
    }

    // TypeToken: 0x2000027  // size: 0x20
    public sealed struct v128
    {
        // Fields
        public System.Byte Byte0;  // 0x10
        public System.Byte Byte1;  // 0x11
        public System.Byte Byte2;  // 0x12
        public System.Byte Byte3;  // 0x13
        public System.Byte Byte4;  // 0x14
        public System.Byte Byte5;  // 0x15
        public System.Byte Byte6;  // 0x16
        public System.Byte Byte7;  // 0x17
        public System.Byte Byte8;  // 0x18
        public System.Byte Byte9;  // 0x19
        public System.Byte Byte10;  // 0x1a
        public System.Byte Byte11;  // 0x1b
        public System.Byte Byte12;  // 0x1c
        public System.Byte Byte13;  // 0x1d
        public System.Byte Byte14;  // 0x1e
        public System.Byte Byte15;  // 0x1f
        public System.SByte SByte0;  // 0x10
        public System.SByte SByte1;  // 0x11
        public System.SByte SByte2;  // 0x12
        public System.SByte SByte3;  // 0x13
        public System.SByte SByte4;  // 0x14
        public System.SByte SByte5;  // 0x15
        public System.SByte SByte6;  // 0x16
        public System.SByte SByte7;  // 0x17
        public System.SByte SByte8;  // 0x18
        public System.SByte SByte9;  // 0x19
        public System.SByte SByte10;  // 0x1a
        public System.SByte SByte11;  // 0x1b
        public System.SByte SByte12;  // 0x1c
        public System.SByte SByte13;  // 0x1d
        public System.SByte SByte14;  // 0x1e
        public System.SByte SByte15;  // 0x1f
        public System.UInt16 UShort0;  // 0x10
        public System.UInt16 UShort1;  // 0x12
        public System.UInt16 UShort2;  // 0x14
        public System.UInt16 UShort3;  // 0x16
        public System.UInt16 UShort4;  // 0x18
        public System.UInt16 UShort5;  // 0x1a
        public System.UInt16 UShort6;  // 0x1c
        public System.UInt16 UShort7;  // 0x1e
        public System.Int16 SShort0;  // 0x10
        public System.Int16 SShort1;  // 0x12
        public System.Int16 SShort2;  // 0x14
        public System.Int16 SShort3;  // 0x16
        public System.Int16 SShort4;  // 0x18
        public System.Int16 SShort5;  // 0x1a
        public System.Int16 SShort6;  // 0x1c
        public System.Int16 SShort7;  // 0x1e
        public System.UInt32 UInt0;  // 0x10
        public System.UInt32 UInt1;  // 0x14
        public System.UInt32 UInt2;  // 0x18
        public System.UInt32 UInt3;  // 0x1c
        public System.Int32 SInt0;  // 0x10
        public System.Int32 SInt1;  // 0x14
        public System.Int32 SInt2;  // 0x18
        public System.Int32 SInt3;  // 0x1c
        public System.UInt64 ULong0;  // 0x10
        public System.UInt64 ULong1;  // 0x18
        public System.Int64 SLong0;  // 0x10
        public System.Int64 SLong1;  // 0x18
        public System.Single Float0;  // 0x10
        public System.Single Float1;  // 0x14
        public System.Single Float2;  // 0x18
        public System.Single Float3;  // 0x1c
        public System.Double Double0;  // 0x10
        public System.Double Double1;  // 0x18
        public Unity.Burst.Intrinsics.v64 Lo64;  // 0x10
        public Unity.Burst.Intrinsics.v64 Hi64;  // 0x18

    }

    // TypeToken: 0x2000028  // size: 0x30
    public sealed struct v256
    {
        // Fields
        public System.Byte Byte0;  // 0x10
        public System.Byte Byte1;  // 0x11
        public System.Byte Byte2;  // 0x12
        public System.Byte Byte3;  // 0x13
        public System.Byte Byte4;  // 0x14
        public System.Byte Byte5;  // 0x15
        public System.Byte Byte6;  // 0x16
        public System.Byte Byte7;  // 0x17
        public System.Byte Byte8;  // 0x18
        public System.Byte Byte9;  // 0x19
        public System.Byte Byte10;  // 0x1a
        public System.Byte Byte11;  // 0x1b
        public System.Byte Byte12;  // 0x1c
        public System.Byte Byte13;  // 0x1d
        public System.Byte Byte14;  // 0x1e
        public System.Byte Byte15;  // 0x1f
        public System.Byte Byte16;  // 0x20
        public System.Byte Byte17;  // 0x21
        public System.Byte Byte18;  // 0x22
        public System.Byte Byte19;  // 0x23
        public System.Byte Byte20;  // 0x24
        public System.Byte Byte21;  // 0x25
        public System.Byte Byte22;  // 0x26
        public System.Byte Byte23;  // 0x27
        public System.Byte Byte24;  // 0x28
        public System.Byte Byte25;  // 0x29
        public System.Byte Byte26;  // 0x2a
        public System.Byte Byte27;  // 0x2b
        public System.Byte Byte28;  // 0x2c
        public System.Byte Byte29;  // 0x2d
        public System.Byte Byte30;  // 0x2e
        public System.Byte Byte31;  // 0x2f
        public System.SByte SByte0;  // 0x10
        public System.SByte SByte1;  // 0x11
        public System.SByte SByte2;  // 0x12
        public System.SByte SByte3;  // 0x13
        public System.SByte SByte4;  // 0x14
        public System.SByte SByte5;  // 0x15
        public System.SByte SByte6;  // 0x16
        public System.SByte SByte7;  // 0x17
        public System.SByte SByte8;  // 0x18
        public System.SByte SByte9;  // 0x19
        public System.SByte SByte10;  // 0x1a
        public System.SByte SByte11;  // 0x1b
        public System.SByte SByte12;  // 0x1c
        public System.SByte SByte13;  // 0x1d
        public System.SByte SByte14;  // 0x1e
        public System.SByte SByte15;  // 0x1f
        public System.SByte SByte16;  // 0x20
        public System.SByte SByte17;  // 0x21
        public System.SByte SByte18;  // 0x22
        public System.SByte SByte19;  // 0x23
        public System.SByte SByte20;  // 0x24
        public System.SByte SByte21;  // 0x25
        public System.SByte SByte22;  // 0x26
        public System.SByte SByte23;  // 0x27
        public System.SByte SByte24;  // 0x28
        public System.SByte SByte25;  // 0x29
        public System.SByte SByte26;  // 0x2a
        public System.SByte SByte27;  // 0x2b
        public System.SByte SByte28;  // 0x2c
        public System.SByte SByte29;  // 0x2d
        public System.SByte SByte30;  // 0x2e
        public System.SByte SByte31;  // 0x2f
        public System.UInt16 UShort0;  // 0x10
        public System.UInt16 UShort1;  // 0x12
        public System.UInt16 UShort2;  // 0x14
        public System.UInt16 UShort3;  // 0x16
        public System.UInt16 UShort4;  // 0x18
        public System.UInt16 UShort5;  // 0x1a
        public System.UInt16 UShort6;  // 0x1c
        public System.UInt16 UShort7;  // 0x1e
        public System.UInt16 UShort8;  // 0x20
        public System.UInt16 UShort9;  // 0x22
        public System.UInt16 UShort10;  // 0x24
        public System.UInt16 UShort11;  // 0x26
        public System.UInt16 UShort12;  // 0x28
        public System.UInt16 UShort13;  // 0x2a
        public System.UInt16 UShort14;  // 0x2c
        public System.UInt16 UShort15;  // 0x2e
        public System.Int16 SShort0;  // 0x10
        public System.Int16 SShort1;  // 0x12
        public System.Int16 SShort2;  // 0x14
        public System.Int16 SShort3;  // 0x16
        public System.Int16 SShort4;  // 0x18
        public System.Int16 SShort5;  // 0x1a
        public System.Int16 SShort6;  // 0x1c
        public System.Int16 SShort7;  // 0x1e
        public System.Int16 SShort8;  // 0x20
        public System.Int16 SShort9;  // 0x22
        public System.Int16 SShort10;  // 0x24
        public System.Int16 SShort11;  // 0x26
        public System.Int16 SShort12;  // 0x28
        public System.Int16 SShort13;  // 0x2a
        public System.Int16 SShort14;  // 0x2c
        public System.Int16 SShort15;  // 0x2e
        public System.UInt32 UInt0;  // 0x10
        public System.UInt32 UInt1;  // 0x14
        public System.UInt32 UInt2;  // 0x18
        public System.UInt32 UInt3;  // 0x1c
        public System.UInt32 UInt4;  // 0x20
        public System.UInt32 UInt5;  // 0x24
        public System.UInt32 UInt6;  // 0x28
        public System.UInt32 UInt7;  // 0x2c
        public System.Int32 SInt0;  // 0x10
        public System.Int32 SInt1;  // 0x14
        public System.Int32 SInt2;  // 0x18
        public System.Int32 SInt3;  // 0x1c
        public System.Int32 SInt4;  // 0x20
        public System.Int32 SInt5;  // 0x24
        public System.Int32 SInt6;  // 0x28
        public System.Int32 SInt7;  // 0x2c
        public System.UInt64 ULong0;  // 0x10
        public System.UInt64 ULong1;  // 0x18
        public System.UInt64 ULong2;  // 0x20
        public System.UInt64 ULong3;  // 0x28
        public System.Int64 SLong0;  // 0x10
        public System.Int64 SLong1;  // 0x18
        public System.Int64 SLong2;  // 0x20
        public System.Int64 SLong3;  // 0x28
        public System.Single Float0;  // 0x10
        public System.Single Float1;  // 0x14
        public System.Single Float2;  // 0x18
        public System.Single Float3;  // 0x1c
        public System.Single Float4;  // 0x20
        public System.Single Float5;  // 0x24
        public System.Single Float6;  // 0x28
        public System.Single Float7;  // 0x2c
        public System.Double Double0;  // 0x10
        public System.Double Double1;  // 0x18
        public System.Double Double2;  // 0x20
        public System.Double Double3;  // 0x28
        public Unity.Burst.Intrinsics.v128 Lo128;  // 0x10
        public Unity.Burst.Intrinsics.v128 Hi128;  // 0x20

        // Methods
        // RVA: 0x04DAB3C0  token: 0x600007F
        public System.Void .ctor(System.Int32 v) { }
        // RVA: 0x04D8C810  token: 0x6000080
        public System.Void .ctor(Unity.Burst.Intrinsics.v128 lo, Unity.Burst.Intrinsics.v128 hi) { }

    }

    // TypeToken: 0x2000029  // size: 0x18
    public sealed struct v64
    {
        // Fields
        public System.Byte Byte0;  // 0x10
        public System.Byte Byte1;  // 0x11
        public System.Byte Byte2;  // 0x12
        public System.Byte Byte3;  // 0x13
        public System.Byte Byte4;  // 0x14
        public System.Byte Byte5;  // 0x15
        public System.Byte Byte6;  // 0x16
        public System.Byte Byte7;  // 0x17
        public System.SByte SByte0;  // 0x10
        public System.SByte SByte1;  // 0x11
        public System.SByte SByte2;  // 0x12
        public System.SByte SByte3;  // 0x13
        public System.SByte SByte4;  // 0x14
        public System.SByte SByte5;  // 0x15
        public System.SByte SByte6;  // 0x16
        public System.SByte SByte7;  // 0x17
        public System.UInt16 UShort0;  // 0x10
        public System.UInt16 UShort1;  // 0x12
        public System.UInt16 UShort2;  // 0x14
        public System.UInt16 UShort3;  // 0x16
        public System.Int16 SShort0;  // 0x10
        public System.Int16 SShort1;  // 0x12
        public System.Int16 SShort2;  // 0x14
        public System.Int16 SShort3;  // 0x16
        public System.UInt32 UInt0;  // 0x10
        public System.UInt32 UInt1;  // 0x14
        public System.Int32 SInt0;  // 0x10
        public System.Int32 SInt1;  // 0x14
        public System.UInt64 ULong0;  // 0x10
        public System.Int64 SLong0;  // 0x10
        public System.Single Float0;  // 0x10
        public System.Single Float1;  // 0x14
        public System.Double Double0;  // 0x10

    }

    // TypeToken: 0x200002A  // size: 0x10
    public static class X86
    {
    }

}

