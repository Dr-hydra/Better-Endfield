// ========================================================
// Dumped by @desirepro
// Assembly: System.Configuration.dll
// Classes:  10
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

CLASS: System.Configuration.ConfigurationElement
TYPE:  abstract class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
PROPERTIES:
  Properties  get=0x0ACAF308
METHODS:
  RVA=0x0ACAF278  token=0x6000002  System.Boolean IsModified()
  RVA=0x0ACAF2D8  token=0x6000003  System.Void Reset(System.Configuration.ConfigurationElement parentElement)
  RVA=0x0ACAF2A8  token=0x6000004  System.Void ResetModified()
END_CLASS

CLASS: System.Configuration.ConfigurationSection
TYPE:  abstract class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Configuration.ConfigurationElement
FIELDS:
METHODS:
  RVA=0x0ACAF338  token=0x6000005  System.Void DeserializeSection(System.Xml.XmlReader reader)
  RVA=0x0ACAF368  token=0x6000006  System.Boolean IsModified()
  RVA=0x0ACAF398  token=0x6000007  System.Void ResetModified()
  RVA=0x0ACAF3C8  token=0x6000008  System.String SerializeSection(System.Configuration.ConfigurationElement parentElement, System.String name, System.Configuration.ConfigurationSaveMode saveMode)
END_CLASS

CLASS: System.Configuration.ConfigurationSaveMode
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Configuration.ConfigurationSaveModeFull  // const
  public    static  System.Configuration.ConfigurationSaveModeMinimal  // const
  public    static  System.Configuration.ConfigurationSaveModeModified  // const
METHODS:
END_CLASS

CLASS: System.Configuration.ConfigurationPropertyCollection
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: System.Configuration.ConfigurationElementCollection
TYPE:  abstract class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Configuration.ConfigurationElement
FIELDS:
METHODS:
END_CLASS

CLASS: System.Configuration.ConfigurationCollectionAttribute
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000009  System.Void .ctor(System.Type itemType)
END_CLASS

CLASS: System.Configuration.ConfigurationSectionGroup
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: System.Configuration.IgnoreSection
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x10
EXTENDS: System.Configuration.ConfigurationSection
FIELDS:
PROPERTIES:
  Properties  get=0x0ACAF518
METHODS:
  RVA=0x0ACAF4E8  token=0x600000A  System.Void .ctor()
  RVA=0x0ACAF3F8  token=0x600000C  System.Void DeserializeSection(System.Xml.XmlReader xmlReader)
  RVA=0x0ACAF428  token=0x600000D  System.Boolean IsModified()
  RVA=0x0ACAF488  token=0x600000E  System.Void Reset(System.Configuration.ConfigurationElement parentSection)
  RVA=0x0ACAF458  token=0x600000F  System.Void ResetModified()
  RVA=0x0ACAF4B8  token=0x6000010  System.String SerializeSection(System.Configuration.ConfigurationElement parentSection, System.String name, System.Configuration.ConfigurationSaveMode saveMode)
END_CLASS

CLASS: Unity.ThrowStub
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x98
EXTENDS: System.ObjectDisposedException
FIELDS:
METHODS:
  RVA=0x0ACAF548  token=0x6000011  System.Void ThrowNotSupportedException()
END_CLASS

