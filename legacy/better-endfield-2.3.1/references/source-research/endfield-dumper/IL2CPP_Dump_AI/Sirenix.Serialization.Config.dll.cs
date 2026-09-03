// ========================================================
// Dumped by @desirepro
// Assembly: Sirenix.Serialization.Config.dll
// Classes:  8
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

CLASS: Sirenix.Serialization.CustomLogger
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x28
IMPLEMENTS: Sirenix.Serialization.ILogger
FIELDS:
  private           System.Action<System.String>    logWarningDelegate  // 0x10
  private           System.Action<System.String>    logErrorDelegate  // 0x18
  private           System.Action<System.Exception> logExceptionDelegate  // 0x20
METHODS:
  RVA=0x0AC46D10  token=0x6000001  System.Void .ctor(System.Action<System.String> logWarningDelegate, System.Action<System.String> logErrorDelegate, System.Action<System.Exception> logExceptionDelegate)
  RVA=0x0810F944  token=0x6000002  System.Void LogWarning(System.String warning)
  RVA=0x09B21654  token=0x6000003  System.Void LogError(System.String error)
  RVA=0x0AC46CF0  token=0x6000004  System.Void LogException(System.Exception exception)
END_CLASS

CLASS: Sirenix.Serialization.DataFormat
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.Serialization.DataFormatBinary  // const
  public    static  Sirenix.Serialization.DataFormatJSON  // const
  public    static  Sirenix.Serialization.DataFormatNodes  // const
METHODS:
END_CLASS

CLASS: Sirenix.Serialization.DefaultLoggers
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static readonly System.Object                   LOCK  // static @ 0x0
  private   static  Sirenix.Serialization.ILogger   unityLogger  // static @ 0x8
PROPERTIES:
  DefaultLogger  get=0x0AC46EC4
  UnityLogger  get=0x0AC46F04
METHODS:
  RVA=0x0AC46E54  token=0x6000007  System.Void .cctor()
END_CLASS

CLASS: Sirenix.Serialization.ErrorHandlingPolicy
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.Serialization.ErrorHandlingPolicyResilient  // const
  public    static  Sirenix.Serialization.ErrorHandlingPolicyThrowOnErrors  // const
  public    static  Sirenix.Serialization.ErrorHandlingPolicyThrowOnWarningsAndErrors  // const
METHODS:
END_CLASS

CLASS: Sirenix.Serialization.GlobalSerializationConfig
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x30
EXTENDS: Sirenix.Utilities.GlobalConfig`1
FIELDS:
  public    static  System.String                   ODIN_SERIALIZATION_CAUTIONARY_WARNING_TEXT  // const
  public    static  System.String                   ODIN_PREFAB_CAUTIONARY_WARNING_TEXT  // const
  public    static  System.String                   ODIN_SERIALIZATION_CAUTIONARY_WARNING_BUTTON_TEXT  // const
  public    static  System.String                   ODIN_PREFAB_CAUTIONARY_WARNING_BUTTON_TEXT  // const
  private   static readonly Sirenix.Serialization.DataFormat[]BuildFormats  // static @ 0x0
  public            System.Boolean                  HideSerializationCautionaryMessage  // 0x18
  public            System.Boolean                  HidePrefabCautionaryMessage  // 0x19
  public            System.Boolean                  HideOdinSerializeAttributeWarningMessages  // 0x1a
  public            System.Boolean                  HideNonSerializedShowInInspectorWarningMessages  // 0x1b
  private           Sirenix.Serialization.DataFormatbuildSerializationFormat  // 0x1c
  private           Sirenix.Serialization.DataFormateditorSerializationFormat  // 0x20
  private           Sirenix.Serialization.LoggingPolicyloggingPolicy  // 0x24
  private           Sirenix.Serialization.ErrorHandlingPolicyerrorHandlingPolicy  // 0x28
PROPERTIES:
  Logger  get=0x0AC47598
  EditorSerializationFormat  get=0x02B70F90  set=0x04D86330
  BuildSerializationFormat  get=0x04D86310  set=0x04D86320
  LoggingPolicy  get=0x011F0020  set=0x011F0030
  ErrorHandlingPolicy  get=0x011F2A90  set=0x011F2AA0
METHODS:
  RVA=0x0AC47204  token=0x6000011  System.Void OnInspectorGUI()
  RVA=0x0AC4755C  token=0x6000012  System.Void .ctor()
  RVA=0x0AC474D4  token=0x6000013  System.Void .cctor()
END_CLASS

CLASS: Sirenix.Serialization.ILogger
TYPE:  interface
TOKEN: 0x2000007
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000014  System.Void LogWarning(System.String warning)
  RVA=-1  // abstract  token=0x6000015  System.Void LogError(System.String error)
  RVA=-1  // abstract  token=0x6000016  System.Void LogException(System.Exception exception)
END_CLASS

CLASS: Sirenix.Serialization.LoggingPolicy
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.Serialization.LoggingPolicyLogErrors  // const
  public    static  Sirenix.Serialization.LoggingPolicyLogWarningsAndErrors  // const
  public    static  Sirenix.Serialization.LoggingPolicySilent  // const
METHODS:
END_CLASS

