// ========================================================
// Dumped by @desirepro
// Assembly: System.Numerics.dll
// Classes:  23
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x10
    public static class SR
    {
        // Methods
        // RVA: 0x0AE33D18  token: 0x6000001
        private static System.String Format(System.String resourceFormat, System.Object p1) { }

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct GetBytesMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Numerics.BigInteger.GetBytesMode AllocateArray;  // const
        public static System.Numerics.BigInteger.GetBytesMode Count;  // const
        public static System.Numerics.BigInteger.GetBytesMode Span;  // const

    }

    // TypeToken: 0x200000E  // size: 0x28
    public sealed struct BigNumberBuffer
    {
        // Fields
        public System.Text.StringBuilder digits;  // 0x10
        public System.Int32 precision;  // 0x18
        public System.Int32 scale;  // 0x1c
        public System.Boolean sign;  // 0x20

        // Methods
        // RVA: 0x0AE2DD2C  token: 0x600008A
        public static System.Numerics.BigNumber.BigNumberBuffer Create() { }

    }

    // TypeToken: 0x2000015  // size: 0x28
    public sealed struct NumberBuffer
    {
        // Fields
        public System.Int32 precision;  // 0x10
        public System.Int32 scale;  // 0x14
        public System.Boolean sign;  // 0x18
        public System.Char* overrideDigits;  // 0x20

        // Properties
        System.Char* digits { get; /* RVA: 0x02B2ECC0 */ }

    }

    // TypeToken: 0x2000014  // size: 0x10
    public class Number
    {
        // Fields
        private static System.String[] s_posCurrencyFormats;  // static @ 0x0
        private static System.String[] s_negCurrencyFormats;  // static @ 0x8
        private static System.String[] s_posPercentFormats;  // static @ 0x10
        private static System.String[] s_negPercentFormats;  // static @ 0x18
        private static System.String[] s_negNumberFormats;  // static @ 0x20
        private static System.String s_posNumberFormat;  // static @ 0x28

        // Methods
        // RVA: 0x0AE31558  token: 0x600009F
        private static System.Boolean IsWhite(System.Char ch) { }
        // RVA: 0x0AE315C0  token: 0x60000A0
        private static System.Char* MatchChars(System.Char* p, System.Char* pEnd, System.String str) { }
        // RVA: 0x0AE31574  token: 0x60000A1
        private static System.Char* MatchChars(System.Char* p, System.Char* pEnd, System.Char* str) { }
        // RVA: 0x0AE32730  token: 0x60000A2
        private static System.Boolean ParseNumber(System.Char*& str, System.Char* strEnd, System.Globalization.NumberStyles options, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Text.StringBuilder sb, System.Globalization.NumberFormatInfo numfmt, System.Boolean parseDecimal) { }
        // RVA: 0x0AE32F40  token: 0x60000A3
        private static System.Boolean TrailingZeros(System.ReadOnlySpan<System.Char> s, System.Int32 index) { }
        // RVA: 0x0AE32FA4  token: 0x60000A4
        private static System.Boolean TryStringToNumber(System.ReadOnlySpan<System.Char> str, System.Globalization.NumberStyles options, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Text.StringBuilder sb, System.Globalization.NumberFormatInfo numfmt, System.Boolean parseDecimal) { }
        // RVA: 0x0AE314F0  token: 0x60000A5
        private static System.Void Int32ToDecChars(System.Char* buffer, System.Int32& index, System.UInt32 value, System.Int32 digits) { }
        // RVA: 0x0AE325CC  token: 0x60000A6
        private static System.Char ParseFormatSpecifier(System.ReadOnlySpan<System.Char> format, System.Int32& digits) { }
        // RVA: 0x0AE321E8  token: 0x60000A7
        private static System.Void NumberToString(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Char format, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info, System.Boolean isDecimal) { }
        // RVA: 0x0AE308D0  token: 0x60000A8
        private static System.Void FormatCurrency(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE3379C  token: 0x60000A9
        private static System.Int32 wcslen(System.Char* s) { }
        // RVA: 0x0AE30B78  token: 0x60000AA
        private static System.Void FormatFixed(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info, System.Int32[] groupDigits, System.String sDecimal, System.String sGroup) { }
        // RVA: 0x0AE310AC  token: 0x60000AB
        private static System.Void FormatNumber(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE313DC  token: 0x60000AC
        private static System.Void FormatScientific(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info, System.Char expChar) { }
        // RVA: 0x0AE30A74  token: 0x60000AD
        private static System.Void FormatExponent(System.Text.ValueStringBuilder& sb, System.Globalization.NumberFormatInfo info, System.Int32 value, System.Char expChar, System.Int32 minDigits, System.Boolean positiveSign) { }
        // RVA: 0x0AE30F50  token: 0x60000AE
        private static System.Void FormatGeneral(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info, System.Char expChar, System.Boolean bSuppressScientific) { }
        // RVA: 0x0AE31230  token: 0x60000AF
        private static System.Void FormatPercent(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 nMinDigits, System.Int32 nMaxDigits, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE32E7C  token: 0x60000B0
        private static System.Void RoundNumber(System.Globalization.FormatProvider.Number.NumberBuffer& number, System.Int32 pos) { }
        // RVA: 0x0AE307C4  token: 0x60000B1
        private static System.Int32 FindSection(System.ReadOnlySpan<System.Char> format, System.Int32 section) { }
        // RVA: 0x0AE31628  token: 0x60000B2
        private static System.Void NumberToStringFormat(System.Text.ValueStringBuilder& sb, System.Globalization.FormatProvider.Number.NumberBuffer& number, System.ReadOnlySpan<System.Char> format, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE330D8  token: 0x60000B3
        private static System.Void .cctor() { }

    }

namespace System.Globalization
{

    // TypeToken: 0x2000013  // size: 0x10
    public class FormatProvider
    {
        // Methods
        // RVA: 0x0AE2FA34  token: 0x600009D
        private static System.Void FormatBigInteger(System.Text.ValueStringBuilder& sb, System.Int32 precision, System.Int32 scale, System.Boolean sign, System.ReadOnlySpan<System.Char> format, System.Globalization.NumberFormatInfo numberFormatInfo, System.Char[] digits, System.Int32 startIndex) { }
        // RVA: 0x0AE2FB68  token: 0x600009E
        private static System.Boolean TryStringToBigInteger(System.ReadOnlySpan<System.Char> s, System.Globalization.NumberStyles styles, System.Globalization.NumberFormatInfo numberFormatInfo, System.Text.StringBuilder receiver, System.Int32& precision, System.Int32& scale, System.Boolean& sign) { }

    }

}

namespace System.Numerics
{

    // TypeToken: 0x2000003  // size: 0x28
    public sealed struct Matrix3x2 : System.IEquatable`1
    {
        // Fields
        public System.Single M11;  // 0x10
        public System.Single M12;  // 0x14
        public System.Single M21;  // 0x18
        public System.Single M22;  // 0x1c
        public System.Single M31;  // 0x20
        public System.Single M32;  // 0x24
        private static readonly System.Numerics.Matrix3x2 _identity;  // static @ 0x0

        // Methods
        // RVA: 0x04DAA360  token: 0x6000002
        public System.Void .ctor(System.Single m11, System.Single m12, System.Single m21, System.Single m22, System.Single m31, System.Single m32) { }
        // RVA: 0x0AE2FD44  token: 0x6000003
        public virtual System.Boolean Equals(System.Numerics.Matrix3x2 other) { }
        // RVA: 0x0AE2FCC0  token: 0x6000004
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE2FE50  token: 0x6000005
        public virtual System.String ToString() { }
        // RVA: 0x0AE2FDC4  token: 0x6000006
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE30004  token: 0x6000007
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x50
    public sealed struct Matrix4x4 : System.IEquatable`1
    {
        // Fields
        public System.Single M11;  // 0x10
        public System.Single M12;  // 0x14
        public System.Single M13;  // 0x18
        public System.Single M14;  // 0x1c
        public System.Single M21;  // 0x20
        public System.Single M22;  // 0x24
        public System.Single M23;  // 0x28
        public System.Single M24;  // 0x2c
        public System.Single M31;  // 0x30
        public System.Single M32;  // 0x34
        public System.Single M33;  // 0x38
        public System.Single M34;  // 0x3c
        public System.Single M41;  // 0x40
        public System.Single M42;  // 0x44
        public System.Single M43;  // 0x48
        public System.Single M44;  // 0x4c
        private static readonly System.Numerics.Matrix4x4 _identity;  // static @ 0x0

        // Methods
        // RVA: 0x04DAA390  token: 0x6000008
        public System.Void .ctor(System.Single m11, System.Single m12, System.Single m13, System.Single m14, System.Single m21, System.Single m22, System.Single m23, System.Single m24, System.Single m31, System.Single m32, System.Single m33, System.Single m34, System.Single m41, System.Single m42, System.Single m43, System.Single m44) { }
        // RVA: 0x0AE30050  token: 0x6000009
        public virtual System.Boolean Equals(System.Numerics.Matrix4x4 other) { }
        // RVA: 0x0AE30174  token: 0x600000A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE3035C  token: 0x600000B
        public virtual System.String ToString() { }
        // RVA: 0x0AE30208  token: 0x600000C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE306D4  token: 0x600000D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x20
    public sealed struct Plane : System.IEquatable`1
    {
        // Fields
        public System.Numerics.Vector3 Normal;  // 0x10
        public System.Single D;  // 0x1c

        // Methods
        // RVA: 0x031C3A00  token: 0x600000E
        public virtual System.Boolean Equals(System.Numerics.Plane other) { }
        // RVA: 0x0AE339B8  token: 0x600000F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE33A54  token: 0x6000010
        public virtual System.String ToString() { }
        // RVA: 0x0AE33A24  token: 0x6000011
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public sealed struct Quaternion : System.IEquatable`1
    {
        // Fields
        public System.Single X;  // 0x10
        public System.Single Y;  // 0x14
        public System.Single Z;  // 0x18
        public System.Single W;  // 0x1c

        // Methods
        // RVA: 0x0415FC30  token: 0x6000012
        public virtual System.Boolean Equals(System.Numerics.Quaternion other) { }
        // RVA: 0x0AE33AEC  token: 0x6000013
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE33BBC  token: 0x6000014
        public virtual System.String ToString() { }
        // RVA: 0x0AE33B5C  token: 0x6000015
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed struct Vector2 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single X;  // 0x10
        public System.Single Y;  // 0x14

        // Methods
        // RVA: 0x0AE34684  token: 0x6000016
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE34620  token: 0x6000017
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE346F0  token: 0x6000018
        public virtual System.String ToString() { }
        // RVA: 0x0AE34750  token: 0x6000019
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x0AAEC490  token: 0x600001A
        public virtual System.Boolean Equals(System.Numerics.Vector2 other) { }

    }

    // TypeToken: 0x2000008  // size: 0x1C
    public sealed struct Vector3 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single X;  // 0x10
        public System.Single Y;  // 0x14
        public System.Single Z;  // 0x18

        // Methods
        // RVA: 0x0AE348EC  token: 0x600001B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE34874  token: 0x600001C
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE34B70  token: 0x600001D
        public virtual System.String ToString() { }
        // RVA: 0x0AE34984  token: 0x600001E
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x030F9620  token: 0x600001F
        public virtual System.Boolean Equals(System.Numerics.Vector3 other) { }

    }

    // TypeToken: 0x2000009  // size: 0x20
    public sealed struct Vector4 : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public System.Single X;  // 0x10
        public System.Single Y;  // 0x14
        public System.Single Z;  // 0x18
        public System.Single W;  // 0x1c

        // Methods
        // RVA: 0x0AE34C40  token: 0x6000020
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE34BD0  token: 0x6000021
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE34E98  token: 0x6000022
        public virtual System.String ToString() { }
        // RVA: 0x0AE34CF8  token: 0x6000023
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x0415FC30  token: 0x6000024
        public virtual System.Boolean Equals(System.Numerics.Vector4 other) { }

    }

    // TypeToken: 0x200000A  // size: 0x20
    public sealed struct BigInteger : System.IFormattable, System.IComparable, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private readonly System.Int32 _sign;  // 0x10
        private readonly System.UInt32[] _bits;  // 0x18
        private static readonly System.Numerics.BigInteger s_bnMinInt;  // static @ 0x0
        private static readonly System.Numerics.BigInteger s_bnOneInt;  // static @ 0x10
        private static readonly System.Numerics.BigInteger s_bnZeroInt;  // static @ 0x20
        private static readonly System.Numerics.BigInteger s_bnMinusOneInt;  // static @ 0x30
        private static readonly System.Byte[] s_success;  // static @ 0x40

        // Properties
        System.Numerics.BigInteger Zero { get; /* RVA: 0x0AE2C744 */ }
        System.Numerics.BigInteger MinusOne { get; /* RVA: 0x0AE2C6E8 */ }
        System.Boolean IsZero { get; /* RVA: 0x0426F6A0 */ }

        // Methods
        // RVA: 0x0AE2C694  token: 0x6000025
        public System.Void .ctor(System.Int32 value) { }
        // RVA: 0x0AE2C070  token: 0x6000026
        public System.Void .ctor(System.UInt32 value) { }
        // RVA: 0x0AE2BF40  token: 0x6000027
        public System.Void .ctor(System.Int64 value) { }
        // RVA: 0x0AE2B93C  token: 0x6000028
        public System.Void .ctor(System.UInt64 value) { }
        // RVA: 0x0AE2BEE8  token: 0x6000029
        public System.Void .ctor(System.Single value) { }
        // RVA: 0x0AE2BA30  token: 0x600002A
        public System.Void .ctor(System.Double value) { }
        // RVA: 0x0AE2BD0C  token: 0x600002B
        public System.Void .ctor(System.Decimal value) { }
        // RVA: 0x0AE2B858  token: 0x600002C
        public System.Void .ctor(System.Byte[] value) { }
        // RVA: 0x0AE2C110  token: 0x600002D
        public System.Void .ctor(System.ReadOnlySpan<System.Byte> value, System.Boolean isUnsigned, System.Boolean isBigEndian) { }
        // RVA: 0x06B17430  token: 0x600002E
        private System.Void .ctor(System.Int32 n, System.UInt32[] rgu) { }
        // RVA: 0x0AE2B6E0  token: 0x600002F
        private System.Void .ctor(System.UInt32[] value, System.Boolean negative) { }
        // RVA: 0x0AE2AC28  token: 0x6000033
        public static System.Numerics.BigInteger Parse(System.String value, System.IFormatProvider provider) { }
        // RVA: 0x0AE2ACAC  token: 0x6000034
        public static System.Numerics.BigInteger Parse(System.String value, System.Globalization.NumberStyles style, System.IFormatProvider provider) { }
        // RVA: 0x0AE2AB28  token: 0x6000035
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE2AA58  token: 0x6000036
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE2A9F0  token: 0x6000037
        public System.Boolean Equals(System.Int64 other) { }
        // RVA: 0x0AE2A950  token: 0x6000038
        public virtual System.Boolean Equals(System.Numerics.BigInteger other) { }
        // RVA: 0x0AE2A7D8  token: 0x6000039
        public System.Int32 CompareTo(System.Int64 other) { }
        // RVA: 0x0AE2A6A4  token: 0x600003A
        public virtual System.Int32 CompareTo(System.Numerics.BigInteger other) { }
        // RVA: 0x0AE2A868  token: 0x600003B
        public virtual System.Int32 CompareTo(System.Object obj) { }
        // RVA: 0x0AE2B3E8  token: 0x600003C
        public System.Boolean TryWriteBytes(System.Span<System.Byte> destination, System.Int32& bytesWritten, System.Boolean isUnsigned, System.Boolean isBigEndian) { }
        // RVA: 0x0AE2B48C  token: 0x600003D
        private System.Boolean TryWriteOrCountBytes(System.Span<System.Byte> destination, System.Int32& bytesWritten, System.Boolean isUnsigned, System.Boolean isBigEndian) { }
        // RVA: 0x0AE2AF24  token: 0x600003E
        private System.Byte[] TryGetBytes(System.Numerics.BigInteger.GetBytesMode mode, System.Span<System.Byte> destination, System.Boolean isUnsigned, System.Boolean isBigEndian, System.Int32& bytesWritten) { }
        // RVA: 0x0AE2AE70  token: 0x600003F
        public virtual System.String ToString() { }
        // RVA: 0x0AE2AEA8  token: 0x6000040
        public System.String ToString(System.IFormatProvider provider) { }
        // RVA: 0x0AE2AEE4  token: 0x6000041
        public virtual System.String ToString(System.String format, System.IFormatProvider provider) { }
        // RVA: 0x0AE2A594  token: 0x6000042
        private static System.Numerics.BigInteger Add(System.UInt32[] leftBits, System.Int32 leftSign, System.UInt32[] rightBits, System.Int32 rightSign) { }
        // RVA: 0x0AE2DC48  token: 0x6000043
        public static System.Numerics.BigInteger op_Subtraction(System.Numerics.BigInteger left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2AD04  token: 0x6000044
        private static System.Numerics.BigInteger Subtract(System.UInt32[] leftBits, System.Int32 leftSign, System.UInt32[] rightBits, System.Int32 rightSign) { }
        // RVA: 0x0AE2D2D8  token: 0x6000045
        public static System.Numerics.BigInteger op_Implicit(System.Byte value) { }
        // RVA: 0x0AE2D250  token: 0x6000046
        public static System.Numerics.BigInteger op_Implicit(System.SByte value) { }
        // RVA: 0x0AE2D2B4  token: 0x6000047
        public static System.Numerics.BigInteger op_Implicit(System.Int16 value) { }
        // RVA: 0x0AE2D31C  token: 0x6000048
        public static System.Numerics.BigInteger op_Implicit(System.UInt16 value) { }
        // RVA: 0x0AE2D230  token: 0x6000049
        public static System.Numerics.BigInteger op_Implicit(System.Int32 value) { }
        // RVA: 0x0AE2D294  token: 0x600004A
        public static System.Numerics.BigInteger op_Implicit(System.UInt32 value) { }
        // RVA: 0x0AE2D2FC  token: 0x600004B
        public static System.Numerics.BigInteger op_Implicit(System.Int64 value) { }
        // RVA: 0x0AE2D274  token: 0x600004C
        public static System.Numerics.BigInteger op_Implicit(System.UInt64 value) { }
        // RVA: 0x0AE2D090  token: 0x600004D
        public static System.Byte op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CCEC  token: 0x600004E
        public static System.SByte op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2C9EC  token: 0x600004F
        public static System.Int16 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2D018  token: 0x6000050
        public static System.UInt16 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CBB8  token: 0x6000051
        public static System.Int32 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CDB4  token: 0x6000052
        public static System.UInt32 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2D108  token: 0x6000053
        public static System.Int64 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CF54  token: 0x6000054
        public static System.UInt64 op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CD64  token: 0x6000055
        public static System.Single op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CE5C  token: 0x6000056
        public static System.Double op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2CA64  token: 0x6000057
        public static System.Decimal op_Explicit(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2D3F4  token: 0x6000058
        public static System.Numerics.BigInteger op_LeftShift(System.Numerics.BigInteger value, System.Int32 shift) { }
        // RVA: 0x0AE2D940  token: 0x6000059
        public static System.Numerics.BigInteger op_RightShift(System.Numerics.BigInteger value, System.Int32 shift) { }
        // RVA: 0x0AE2DCFC  token: 0x600005A
        public static System.Numerics.BigInteger op_UnaryNegation(System.Numerics.BigInteger value) { }
        // RVA: 0x0AE2C7A0  token: 0x600005B
        public static System.Numerics.BigInteger op_Addition(System.Numerics.BigInteger left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2D7C4  token: 0x600005C
        public static System.Numerics.BigInteger op_Multiply(System.Numerics.BigInteger left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2C844  token: 0x600005D
        public static System.Numerics.BigInteger op_Division(System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor) { }
        // RVA: 0x0AE2D604  token: 0x600005E
        public static System.Boolean op_LessThanOrEqual(System.Numerics.BigInteger left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2D394  token: 0x600005F
        public static System.Boolean op_Inequality(System.Numerics.BigInteger left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2D770  token: 0x6000060
        public static System.Boolean op_LessThan(System.Numerics.BigInteger left, System.Int64 right) { }
        // RVA: 0x0AE2D668  token: 0x6000061
        public static System.Boolean op_LessThanOrEqual(System.Numerics.BigInteger left, System.Int64 right) { }
        // RVA: 0x0AE2C990  token: 0x6000062
        public static System.Boolean op_Equality(System.Numerics.BigInteger left, System.Int64 right) { }
        // RVA: 0x0AE2D340  token: 0x6000063
        public static System.Boolean op_Inequality(System.Numerics.BigInteger left, System.Int64 right) { }
        // RVA: 0x0AE2D718  token: 0x6000064
        public static System.Boolean op_LessThan(System.Int64 left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2D6C0  token: 0x6000065
        public static System.Boolean op_LessThanOrEqual(System.Int64 left, System.Numerics.BigInteger right) { }
        // RVA: 0x0AE2AB78  token: 0x6000066
        private static System.Boolean GetPartsForBitManipulation(System.Numerics.BigInteger& x, System.UInt32[]& xd, System.Int32& xl) { }
        // RVA: 0x0AE2AAD4  token: 0x6000067
        private static System.Int32 GetDiffLength(System.UInt32[] rgu1, System.UInt32[] rgu2, System.Int32 cu) { }
        // RVA: 0x0AE2B528  token: 0x6000068
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class BigIntegerCalculator
    {
        // Fields
        private static System.Int32 ReducerThreshold;  // static @ 0x0
        private static System.Int32 SquareThreshold;  // static @ 0x4
        private static System.Int32 AllocationThreshold;  // static @ 0x8
        private static System.Int32 MultiplyThreshold;  // static @ 0xc

        // Methods
        // RVA: 0x0AE28F6C  token: 0x6000069
        public static System.UInt32[] Add(System.UInt32[] left, System.UInt32 right) { }
        // RVA: 0x0AE29028  token: 0x600006A
        public static System.UInt32[] Add(System.UInt32[] left, System.UInt32[] right) { }
        // RVA: 0x0AE28EC0  token: 0x600006B
        private static System.Void Add(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt32* bits, System.Int32 bitsLength) { }
        // RVA: 0x0AE28E50  token: 0x600006C
        private static System.Void AddSelf(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength) { }
        // RVA: 0x0AE2A478  token: 0x600006D
        public static System.UInt32[] Subtract(System.UInt32[] left, System.UInt32 right) { }
        // RVA: 0x0AE2A2C4  token: 0x600006E
        public static System.UInt32[] Subtract(System.UInt32[] left, System.UInt32[] right) { }
        // RVA: 0x0AE2A3D4  token: 0x600006F
        private static System.Void Subtract(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt32* bits, System.Int32 bitsLength) { }
        // RVA: 0x0AE29128  token: 0x6000070
        public static System.Int32 Compare(System.UInt32[] left, System.UInt32[] right) { }
        // RVA: 0x0AE29598  token: 0x6000071
        public static System.UInt32[] Divide(System.UInt32[] left, System.UInt32 right) { }
        // RVA: 0x0AE2963C  token: 0x6000072
        public static System.UInt32[] Divide(System.UInt32[] left, System.UInt32[] right) { }
        // RVA: 0x0AE29258  token: 0x6000073
        private static System.Void Divide(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt32* bits, System.Int32 bitsLength) { }
        // RVA: 0x0AE28E1C  token: 0x6000074
        private static System.UInt32 AddDivisor(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength) { }
        // RVA: 0x0AE2A284  token: 0x6000075
        private static System.UInt32 SubtractDivisor(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt64 q) { }
        // RVA: 0x0AE2921C  token: 0x6000076
        private static System.Boolean DivideGuessTooBig(System.UInt64 q, System.UInt64 valHi, System.UInt32 valLo, System.UInt32 divHi, System.UInt32 divLo) { }
        // RVA: 0x0AE291A4  token: 0x6000077
        private static System.UInt32[] CreateCopy(System.UInt32[] value) { }
        // RVA: 0x0AE2973C  token: 0x6000078
        private static System.Int32 LeadingZeros(System.UInt32 value) { }
        // RVA: 0x0AE2A0C8  token: 0x6000079
        public static System.UInt32[] Square(System.UInt32[] value) { }
        // RVA: 0x0AE29DDC  token: 0x600007A
        private static System.Void Square(System.UInt32* value, System.Int32 valueLength, System.UInt32* bits, System.Int32 bitsLength) { }
        // RVA: 0x0AE29D28  token: 0x600007B
        public static System.UInt32[] Multiply(System.UInt32[] left, System.UInt32 right) { }
        // RVA: 0x0AE297A4  token: 0x600007C
        public static System.UInt32[] Multiply(System.UInt32[] left, System.UInt32[] right) { }
        // RVA: 0x0AE298B4  token: 0x600007D
        private static System.Void Multiply(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt32* bits, System.Int32 bitsLength) { }
        // RVA: 0x0AE2A1A0  token: 0x600007E
        private static System.Void SubtractCore(System.UInt32* left, System.Int32 leftLength, System.UInt32* right, System.Int32 rightLength, System.UInt32* core, System.Int32 coreLength) { }
        // RVA: 0x0AE2A520  token: 0x600007F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class BigNumber
    {
        // Methods
        // RVA: 0x0AE2F5D4  token: 0x6000080
        private static System.Boolean TryValidateParseStyleInteger(System.Globalization.NumberStyles style, System.ArgumentException& e) { }
        // RVA: 0x0AE2F498  token: 0x6000081
        private static System.Boolean TryParseBigInteger(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.Globalization.NumberFormatInfo info, System.Numerics.BigInteger& result) { }
        // RVA: 0x0AE2F1A4  token: 0x6000082
        private static System.Numerics.BigInteger ParseBigInteger(System.String value, System.Globalization.NumberStyles style, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE2F260  token: 0x6000083
        private static System.Numerics.BigInteger ParseBigInteger(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE2EE08  token: 0x6000084
        private static System.Boolean HexNumberToBigInteger(System.Numerics.BigNumber.BigNumberBuffer& number, System.Numerics.BigInteger& value) { }
        // RVA: 0x0AE2EF8C  token: 0x6000085
        private static System.Boolean NumberToBigInteger(System.Numerics.BigNumber.BigNumberBuffer& number, System.Numerics.BigInteger& value) { }
        // RVA: 0x0AE2F38C  token: 0x6000086
        private static System.Char ParseFormatSpecifier(System.ReadOnlySpan<System.Char> format, System.Int32& digits) { }
        // RVA: 0x0AE2DDA0  token: 0x6000087
        private static System.String FormatBigIntegerToHex(System.Boolean targetSpan, System.Numerics.BigInteger value, System.Char format, System.Int32 digits, System.Globalization.NumberFormatInfo info, System.Span<System.Char> destination, System.Int32& charsWritten, System.Boolean& spanSuccess) { }
        // RVA: 0x0AE2E200  token: 0x6000088
        private static System.String FormatBigInteger(System.Numerics.BigInteger value, System.String format, System.Globalization.NumberFormatInfo info) { }
        // RVA: 0x0AE2E2A0  token: 0x6000089
        private static System.String FormatBigInteger(System.Boolean targetSpan, System.Numerics.BigInteger value, System.String formatString, System.ReadOnlySpan<System.Char> formatSpan, System.Globalization.NumberFormatInfo info, System.Span<System.Char> destination, System.Int32& charsWritten, System.Boolean& spanSuccess) { }

    }

    // TypeToken: 0x200000F  // size: 0x20
    public sealed struct Complex : System.IEquatable`1, System.IFormattable
    {
        // Fields
        public static readonly System.Numerics.Complex Zero;  // static @ 0x0
        public static readonly System.Numerics.Complex One;  // static @ 0x10
        public static readonly System.Numerics.Complex ImaginaryOne;  // static @ 0x20
        private static readonly System.Double s_sqrtRescaleThreshold;  // static @ 0x30
        private static readonly System.Double s_asinOverflowThreshold;  // static @ 0x38
        private static readonly System.Double s_log2;  // static @ 0x40
        private System.Double m_real;  // 0x10
        private System.Double m_imaginary;  // 0x18

        // Methods
        // RVA: 0x04D881B0  token: 0x600008B
        public System.Void .ctor(System.Double real, System.Double imaginary) { }
        // RVA: 0x0AE2F710  token: 0x600008C
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE2F6D0  token: 0x600008D
        public virtual System.Boolean Equals(System.Numerics.Complex value) { }
        // RVA: 0x0AE2F78C  token: 0x600008E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE2F868  token: 0x600008F
        public virtual System.String ToString() { }
        // RVA: 0x0AE2F7D8  token: 0x6000090
        public virtual System.String ToString(System.String format, System.IFormatProvider provider) { }
        // RVA: 0x0AE2F92C  token: 0x6000091
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed struct DoubleUlong
    {
        // Fields
        public System.Double dbl;  // 0x10
        public System.UInt64 uu;  // 0x10

    }

    // TypeToken: 0x2000011  // size: 0x10
    public static class NumericsHelpers
    {
        // Methods
        // RVA: 0x0AE33930  token: 0x6000092
        public static System.Void GetDoubleParts(System.Double dbl, System.Int32& sign, System.Int32& exp, System.UInt64& man, System.Boolean& fFinite) { }
        // RVA: 0x0AE3386C  token: 0x6000093
        public static System.Double GetDoubleFromParts(System.Int32 sign, System.Int32 exp, System.UInt64 man) { }
        // RVA: 0x0AE337EC  token: 0x6000094
        public static System.Void DangerousMakeTwosComplement(System.UInt32[] d) { }
        // RVA: 0x04D91D20  token: 0x6000095
        public static System.UInt64 MakeUlong(System.UInt32 uHi, System.UInt32 uLo) { }
        // RVA: 0x04DAA440  token: 0x6000096
        public static System.UInt32 Abs(System.Int32 a) { }
        // RVA: 0x04DAA450  token: 0x6000097
        public static System.UInt32 CombineHash(System.UInt32 u1, System.UInt32 u2) { }
        // RVA: 0x04DAA450  token: 0x6000098
        public static System.Int32 CombineHash(System.Int32 n1, System.Int32 n2) { }
        // RVA: 0x0AE2973C  token: 0x6000099
        public static System.Int32 CbitHighZero(System.UInt32 u) { }
        // RVA: 0x0AE337BC  token: 0x600009A
        public static System.Int32 CbitHighZero(System.UInt64 uu) { }

    }

}

namespace System.Numerics.Hashing
{

    // TypeToken: 0x2000012  // size: 0x10
    public static class HashHelpers
    {
        // Fields
        public static readonly System.Int32 RandomSeed;  // static @ 0x0

        // Methods
        // RVA: 0x04DA4450  token: 0x600009B
        public static System.Int32 Combine(System.Int32 h1, System.Int32 h2) { }
        // RVA: 0x0AE2FC60  token: 0x600009C
        private static System.Void .cctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000017  // size: 0x10
    public sealed class IntrinsicAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000C3
        public System.Void .ctor() { }

    }

}

namespace System.Text
{

    // TypeToken: 0x2000016  // size: 0x30
    public sealed struct ValueStringBuilder
    {
        // Fields
        private System.Char[] _arrayToReturnToPool;  // 0x10
        private System.Span<System.Char> _chars;  // 0x18
        private System.Int32 _pos;  // 0x28

        // Properties
        System.Int32 Length { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x04DA4110  token: 0x60000B5
        public System.Void .ctor(System.Span<System.Char> initialBuffer) { }
        // RVA: 0x0AE344C0  token: 0x60000B7
        public virtual System.String ToString() { }
        // RVA: 0x0AE34568  token: 0x60000B8
        public System.Boolean TryCopyTo(System.Span<System.Char> destination, System.Int32& charsWritten) { }
        // RVA: 0x0AE34380  token: 0x60000B9
        public System.Void Insert(System.Int32 index, System.Char value, System.Int32 count) { }
        // RVA: 0x0AE3410C  token: 0x60000BA
        public System.Void Append(System.Char c) { }
        // RVA: 0x0AE33FAC  token: 0x60000BB
        public System.Void Append(System.String s) { }
        // RVA: 0x0AE33D74  token: 0x60000BC
        private System.Void AppendSlow(System.String s) { }
        // RVA: 0x0AE33EF0  token: 0x60000BD
        public System.Void Append(System.Char c, System.Int32 count) { }
        // RVA: 0x0AE34048  token: 0x60000BE
        public System.Void Append(System.Char* value, System.Int32 length) { }
        // RVA: 0x0AE33E48  token: 0x60000BF
        public System.Span<System.Char> AppendSpan(System.Int32 length) { }
        // RVA: 0x0AE341F4  token: 0x60000C0
        private System.Void GrowAndAppend(System.Char c) { }
        // RVA: 0x0AE3422C  token: 0x60000C1
        private System.Void Grow(System.Int32 requiredAdditionalCapacity) { }
        // RVA: 0x0AE34174  token: 0x60000C2
        public System.Void Dispose() { }

    }

}

