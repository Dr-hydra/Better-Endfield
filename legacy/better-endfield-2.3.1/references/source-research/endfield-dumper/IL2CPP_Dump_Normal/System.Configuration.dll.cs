// ========================================================
// Dumped by @desirepro
// Assembly: System.Configuration.dll
// Classes:  10
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace System.Configuration
{

    // TypeToken: 0x2000002  // size: 0x10
    public abstract class ConfigurationElement
    {
        // Properties
        System.Configuration.ConfigurationPropertyCollection Properties { get; /* RVA: 0x0ACAF308 */ }

        // Methods
        // RVA: 0x0ACAF278  token: 0x6000002
        protected internal virtual System.Boolean IsModified() { }
        // RVA: 0x0ACAF2D8  token: 0x6000003
        protected internal virtual System.Void Reset(System.Configuration.ConfigurationElement parentElement) { }
        // RVA: 0x0ACAF2A8  token: 0x6000004
        protected internal virtual System.Void ResetModified() { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public abstract class ConfigurationSection : System.Configuration.ConfigurationElement
    {
        // Methods
        // RVA: 0x0ACAF338  token: 0x6000005
        protected internal virtual System.Void DeserializeSection(System.Xml.XmlReader reader) { }
        // RVA: 0x0ACAF368  token: 0x6000006
        protected internal virtual System.Boolean IsModified() { }
        // RVA: 0x0ACAF398  token: 0x6000007
        protected internal virtual System.Void ResetModified() { }
        // RVA: 0x0ACAF3C8  token: 0x6000008
        protected internal virtual System.String SerializeSection(System.Configuration.ConfigurationElement parentElement, System.String name, System.Configuration.ConfigurationSaveMode saveMode) { }

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct ConfigurationSaveMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Configuration.ConfigurationSaveMode Full;  // const
        public static System.Configuration.ConfigurationSaveMode Minimal;  // const
        public static System.Configuration.ConfigurationSaveMode Modified;  // const

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class ConfigurationPropertyCollection
    {
    }

    // TypeToken: 0x2000006  // size: 0x10
    public abstract class ConfigurationElementCollection : System.Configuration.ConfigurationElement
    {
    }

    // TypeToken: 0x2000007  // size: 0x10
    public sealed class ConfigurationCollectionAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000009
        public System.Void .ctor(System.Type itemType) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class ConfigurationSectionGroup
    {
    }

    // TypeToken: 0x2000009  // size: 0x10
    public sealed class IgnoreSection : System.Configuration.ConfigurationSection
    {
        // Properties
        System.Configuration.ConfigurationPropertyCollection Properties { get; /* RVA: 0x0ACAF518 */ }

        // Methods
        // RVA: 0x0ACAF4E8  token: 0x600000A
        public System.Void .ctor() { }
        // RVA: 0x0ACAF3F8  token: 0x600000C
        protected internal virtual System.Void DeserializeSection(System.Xml.XmlReader xmlReader) { }
        // RVA: 0x0ACAF428  token: 0x600000D
        protected internal virtual System.Boolean IsModified() { }
        // RVA: 0x0ACAF488  token: 0x600000E
        protected internal virtual System.Void Reset(System.Configuration.ConfigurationElement parentSection) { }
        // RVA: 0x0ACAF458  token: 0x600000F
        protected internal virtual System.Void ResetModified() { }
        // RVA: 0x0ACAF4B8  token: 0x6000010
        protected internal virtual System.String SerializeSection(System.Configuration.ConfigurationElement parentSection, System.String name, System.Configuration.ConfigurationSaveMode saveMode) { }

    }

}

namespace Unity
{

    // TypeToken: 0x200000A  // size: 0x98
    public sealed class ThrowStub : System.ObjectDisposedException
    {
        // Methods
        // RVA: 0x0ACAF548  token: 0x6000011
        public static System.Void ThrowNotSupportedException() { }

    }

}

