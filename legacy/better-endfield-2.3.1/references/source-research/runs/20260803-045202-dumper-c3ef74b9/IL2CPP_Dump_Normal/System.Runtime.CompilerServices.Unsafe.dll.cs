// ========================================================
// Dumped by @desirepro
// Assembly: System.Runtime.CompilerServices.Unsafe.dll
// Classes:  4
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class Unsafe
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000001
        public static T ReadUnaligned(System.Byte& source) { }
        // RVA: -1  // generic def  token: 0x6000002
        public static System.Void WriteUnaligned(System.Byte& destination, T value) { }
        // RVA: -1  // generic def  token: 0x6000003
        public static System.Int32 SizeOf() { }
        // RVA: 0x04DA42B0  token: 0x6000004
        public static System.Void CopyBlockUnaligned(System.Byte& destination, System.Byte& source, System.UInt32 byteCount) { }
        // RVA: 0x04DA43F0  token: 0x6000005
        public static System.Void InitBlock(System.Void* startAddress, System.Byte value, System.UInt32 byteCount) { }
        // RVA: -1  // generic def  token: 0x6000006
        public static T As(System.Object o) { }
        // RVA: -1  // generic def  token: 0x6000007
        public static T& AsRef(System.Void* source) { }
        // RVA: -1  // generic def  token: 0x6000008
        public static T& AsRef(T& source) { }
        // RVA: -1  // generic def  token: 0x6000009
        public static TTo& As(TFrom& source) { }
        // RVA: -1  // generic def  token: 0x600000A
        public static T& Add(T& source, System.Int32 elementOffset) { }
        // RVA: -1  // generic def  token: 0x600000B
        public static T& AddByteOffset(T& source, System.IntPtr byteOffset) { }
        // RVA: -1  // generic def  token: 0x600000C
        public static T& NullRef() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public sealed class IsReadOnlyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000E
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.Versioning
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class NonVersionableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000D
        public System.Void .ctor() { }

    }

}

