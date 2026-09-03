// ========================================================
// Dumped by @desirepro
// Assembly: System.Runtime.Serialization.dll
// Classes:  5
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace System.Runtime.Serialization
{

    // TypeToken: 0x2000002  // size: 0x18
    public sealed class DataContractAttribute : System.Attribute
    {
        // Fields
        private System.Boolean isReference;  // 0x10

        // Properties
        System.Boolean IsReference { get; /* RVA: 0x015EFCE0 */ }

    }

    // TypeToken: 0x2000003  // size: 0x20
    public sealed class DataMemberAttribute : System.Attribute
    {
        // Fields
        private System.String name;  // 0x10
        private System.Int32 order;  // 0x18
        private System.Boolean isRequired;  // 0x1c
        private System.Boolean emitDefaultValue;  // 0x1d

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ }
        System.Int32 Order { get; /* RVA: 0x011EF5B0 */ }
        System.Boolean IsRequired { get; /* RVA: 0x04D86150 */ }
        System.Boolean EmitDefaultValue { get; /* RVA: 0x04D88310 */ }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class EnumMemberAttribute : System.Attribute
    {
        // Fields
        private System.String value;  // 0x10

        // Properties
        System.String Value { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class IgnoreDataMemberAttribute : System.Attribute
    {
    }

}

