// ========================================================
// Dumped by @desirepro
// Assembly: Unity.InputSystem.dll
// Classes:  647
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

CLASS: UISupport
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03DF6FF0  token=0x6000001  System.Void Initialize()
END_CLASS

CLASS: ActionFlags
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputAction.ActionFlagsWantsInitialStateCheck  // const
METHODS:
END_CLASS

CLASS: CallbackContext
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x20
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           System.Int32                    m_ActionIndex  // 0x18
PROPERTIES:
  actionIndex  get=0x04D88B10
  bindingIndex  get=0x03529170
  controlIndex  get=0x03528B80
  interactionIndex  get=0x0B1984A8
  phase  get=0x03529890
  started  get=0x0B198560
  performed  get=0x0B198528
  canceled  get=0x03529860
  action  get=0x03527400
  control  get=0x03527700
  interaction  get=0x0B1984D8
  time  get=0x0B198578
  startTime  get=0x0B198540
  duration  get=0x0B198474
  valueType  get=0x0B1985EC
  valueSizeInBytes  get=0x0B198598
METHODS:
  RVA=0x0B198104  token=0x600017F  System.Void ReadValue(System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // generic def  token=0x6000180  TValue ReadValue()
  RVA=0x035265D0  token=0x6000181  System.Boolean ReadValueAsButton()
  RVA=0x0B198088  token=0x6000182  System.Object ReadValueAsObject()
  RVA=0x0B1982C8  token=0x6000183  System.String ToString()
END_CLASS

CLASS: WriteFileJson
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.InputSystem.InputActionMap.WriteMapJson[]maps  // 0x18
  public            UnityEngine.InputSystem.InputControlScheme.SchemeJson[]controlSchemes  // 0x20
METHODS:
END_CLASS

CLASS: WriteFileJsonNoName
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x20
FIELDS:
  public            UnityEngine.InputSystem.InputActionMap.WriteMapJson[]maps  // 0x10
  public            UnityEngine.InputSystem.InputControlScheme.SchemeJson[]controlSchemes  // 0x18
METHODS:
END_CLASS

CLASS: ReadFileJson
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.InputSystem.InputActionMap.ReadMapJson[]maps  // 0x18
  public            UnityEngine.InputSystem.InputControlScheme.SchemeJson[]controlSchemes  // 0x20
METHODS:
  RVA=0x0B1A52C8  token=0x60001A5  System.Void ToAsset(UnityEngine.InputSystem.InputActionAsset asset)
END_CLASS

CLASS: <get_bindings>d__9
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x98
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.InputBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x70
  public            UnityEngine.InputSystem.InputActionAsset<>4__this  // 0x78
  private           System.Int32                    <numActionMaps>5__2  // 0x80
  private           System.Int32                    <i>5__3  // 0x84
  private           UnityEngine.InputSystem.InputBinding[]<bindings>5__4  // 0x88
  private           System.Int32                    <numBindings>5__5  // 0x90
  private           System.Int32                    <n>5__6  // 0x94
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputBinding>.Current  get=0x04DAB820
  System.Collections.IEnumerator.Current  get=0x0B1A966C
METHODS:
  RVA=0x0B1A96E8  token=0x60001A6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001A7  System.Void System.IDisposable.Dispose()
  RVA=0x0B1A93F0  token=0x60001A8  System.Boolean MoveNext()
  RVA=0x0B1A9620  token=0x60001AA  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1A958C  token=0x60001AC  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputBinding> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputBinding>.GetEnumerator()
  RVA=0x0B1A9618  token=0x60001AD  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <GetEnumerator>d__32
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.InputAction<>2__current  // 0x18
  public            UnityEngine.InputSystem.InputActionAsset<>4__this  // 0x20
  private           System.Int32                    <i>5__2  // 0x28
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputAction><actions>5__3  // 0x30
  private           System.Int32                    <actionCount>5__4  // 0x40
  private           System.Int32                    <n>5__5  // 0x44
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputAction>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001AE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001AF  System.Void System.IDisposable.Dispose()
  RVA=0x0B1A9288  token=0x60001B0  System.Boolean MoveNext()
  RVA=0x0B1A93A4  token=0x60001B2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionMap.FlagsNeedToResolveBindings  // const
  public    static  UnityEngine.InputSystem.InputActionMap.FlagsBindingResolutionNeedsFullReResolve  // const
  public    static  UnityEngine.InputSystem.InputActionMap.FlagsControlsForEachActionInitialized  // const
  public    static  UnityEngine.InputSystem.InputActionMap.FlagsBindingsForEachActionInitialized  // const
METHODS:
END_CLASS

CLASS: DeviceArray
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x20
FIELDS:
  private           System.Boolean                  m_HaveValue  // 0x10
  private           System.Int32                    m_DeviceCount  // 0x14
  private           UnityEngine.InputSystem.InputDevice[]m_DeviceArray  // 0x18
METHODS:
  RVA=0x0B1988D8  token=0x60001F1  System.Int32 IndexOf(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B198928  token=0x60001F2  System.Boolean Remove(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0363A460  token=0x60001F3  System.Nullable<UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputDevice>> Get()
  RVA=0x0B198990  token=0x60001F4  System.Boolean Set(System.Nullable<UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputDevice>> devices)
END_CLASS

CLASS: BindingOverrideListJson
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.InputSystem.InputActionMap.BindingOverrideJson>bindings  // 0x10
METHODS:
END_CLASS

CLASS: BindingOverrideJson
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x38
FIELDS:
  public            System.String                   action  // 0x10
  public            System.String                   id  // 0x18
  public            System.String                   path  // 0x20
  public            System.String                   interactions  // 0x28
  public            System.String                   processors  // 0x30
METHODS:
  RVA=0x0B197DF0  token=0x60001F5  UnityEngine.InputSystem.InputActionMap.BindingOverrideJson FromBinding(UnityEngine.InputSystem.InputBinding binding, System.String actionName)
  RVA=0x0B197EEC  token=0x60001F6  UnityEngine.InputSystem.InputActionMap.BindingOverrideJson FromBinding(UnityEngine.InputSystem.InputBinding binding)
  RVA=0x0B197F68  token=0x60001F7  UnityEngine.InputSystem.InputBinding ToBinding(UnityEngine.InputSystem.InputActionMap.BindingOverrideJson bindingOverride)
END_CLASS

CLASS: BindingJson
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x50
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   id  // 0x18
  public            System.String                   path  // 0x20
  public            System.String                   interactions  // 0x28
  public            System.String                   processors  // 0x30
  public            System.String                   groups  // 0x38
  public            System.String                   action  // 0x40
  public            System.Boolean                  isComposite  // 0x48
  public            System.Boolean                  isPartOfComposite  // 0x49
METHODS:
  RVA=0x0B197CCC  token=0x60001F8  UnityEngine.InputSystem.InputBinding ToBinding()
  RVA=0x0B197C14  token=0x60001F9  UnityEngine.InputSystem.InputActionMap.BindingJson FromBinding(UnityEngine.InputSystem.InputBinding& binding)
END_CLASS

CLASS: ReadActionJson
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x58
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   type  // 0x18
  public            System.String                   id  // 0x20
  public            System.String                   expectedControlType  // 0x28
  public            System.String                   expectedControlLayout  // 0x30
  public            System.String                   processors  // 0x38
  public            System.String                   interactions  // 0x40
  public            System.Boolean                  passThrough  // 0x48
  public            System.Boolean                  initialStateCheck  // 0x49
  public            UnityEngine.InputSystem.InputActionMap.BindingJson[]bindings  // 0x50
METHODS:
  RVA=0x0B1A508C  token=0x60001FA  UnityEngine.InputSystem.InputAction ToAction(System.String actionName)
END_CLASS

CLASS: WriteActionJson
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x48
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   type  // 0x18
  public            System.String                   id  // 0x20
  public            System.String                   expectedControlType  // 0x28
  public            System.String                   processors  // 0x30
  public            System.String                   interactions  // 0x38
  public            System.Boolean                  initialStateCheck  // 0x40
METHODS:
  RVA=0x0B1A9E70  token=0x60001FB  UnityEngine.InputSystem.InputActionMap.WriteActionJson FromAction(UnityEngine.InputSystem.InputAction action)
END_CLASS

CLASS: ReadMapJson
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x30
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   id  // 0x18
  public            UnityEngine.InputSystem.InputActionMap.ReadActionJson[]actions  // 0x20
  public            UnityEngine.InputSystem.InputActionMap.BindingJson[]bindings  // 0x28
METHODS:
END_CLASS

CLASS: WriteMapJson
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x30
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   id  // 0x18
  public            UnityEngine.InputSystem.InputActionMap.WriteActionJson[]actions  // 0x20
  public            UnityEngine.InputSystem.InputActionMap.BindingJson[]bindings  // 0x28
METHODS:
  RVA=0x0B1AA214  token=0x60001FC  UnityEngine.InputSystem.InputActionMap.WriteMapJson FromMap(UnityEngine.InputSystem.InputActionMap map)
END_CLASS

CLASS: WriteFileJson
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.InputActionMap.WriteMapJson[]maps  // 0x10
METHODS:
  RVA=0x0B1A9F5C  token=0x60001FD  UnityEngine.InputSystem.InputActionMap.WriteFileJson FromMap(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1A9FF4  token=0x60001FE  UnityEngine.InputSystem.InputActionMap.WriteFileJson FromMaps(System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputActionMap> maps)
END_CLASS

CLASS: ReadFileJson
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x20
FIELDS:
  public            UnityEngine.InputSystem.InputActionMap.ReadActionJson[]actions  // 0x10
  public            UnityEngine.InputSystem.InputActionMap.ReadMapJson[]maps  // 0x18
METHODS:
  RVA=0x0B1A5388  token=0x60001FF  UnityEngine.InputSystem.InputActionMap[] ToMaps()
END_CLASS

CLASS: Parameter
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x28
FIELDS:
  public            System.Object                   instance  // 0x10
  public            System.Reflection.FieldInfo     field  // 0x18
  public            System.Int32                    bindingIndex  // 0x20
METHODS:
END_CLASS

CLASS: ParameterEnumerable
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x98
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverridem_Parameter  // 0x18
  private           System.Int32                    m_MapIndex  // 0x90
METHODS:
  RVA=0x0B1A3C98  token=0x600022D  System.Void .ctor(UnityEngine.InputSystem.InputActionState state, UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameter, System.Int32 mapIndex)
  RVA=0x0B1A3A00  token=0x600022E  UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerator GetEnumerator()
  RVA=0x0B1A3B20  token=0x600022F  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter>.GetEnumerator()
  RVA=0x0B1A3BDC  token=0x6000230  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: ParameterEnumerator
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0xB8
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           System.Int32                    m_MapIndex  // 0x18
  private           System.Int32                    m_BindingCurrentIndex  // 0x1c
  private           System.Int32                    m_BindingEndIndex  // 0x20
  private           System.Int32                    m_InteractionCurrentIndex  // 0x24
  private           System.Int32                    m_InteractionEndIndex  // 0x28
  private           System.Int32                    m_ProcessorCurrentIndex  // 0x2c
  private           System.Int32                    m_ProcessorEndIndex  // 0x30
  private           UnityEngine.InputSystem.InputBindingm_BindingMask  // 0x38
  private           System.Type                     m_ObjectType  // 0x90
  private           System.String                   m_ParameterName  // 0x98
  private           System.Boolean                  m_MayBeInteraction  // 0xa0
  private           System.Boolean                  m_MayBeProcessor  // 0xa1
  private           System.Boolean                  m_MayBeComposite  // 0xa2
  private           System.Boolean                  m_CurrentBindingIsComposite  // 0xa3
  private           System.Object                   m_CurrentObject  // 0xa8
  private           System.Reflection.FieldInfo     m_CurrentParameter  // 0xb0
PROPERTIES:
  Current  get=0x0B1A4484
  System.Collections.IEnumerator.Current  get=0x0B1A41DC
METHODS:
  RVA=0x0B1A423C  token=0x6000231  System.Void .ctor(UnityEngine.InputSystem.InputActionState state, UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameter, System.Int32 mapIndex)
  RVA=0x0B1A3EB8  token=0x6000232  System.Boolean MoveToNextBinding()
  RVA=0x0B1A4024  token=0x6000233  System.Boolean MoveToNextInteraction()
  RVA=0x0B1A4088  token=0x6000234  System.Boolean MoveToNextProcessor()
  RVA=0x0B1A3D10  token=0x6000235  System.Boolean FindParameter(System.Object instance)
  RVA=0x0B1A3DFC  token=0x6000236  System.Boolean MoveNext()
  RVA=0x0B1A40EC  token=0x6000237  System.Void Reset()
  RVA=0x041E1670  token=0x600023A  System.Void Dispose()
END_CLASS

CLASS: ParameterOverride
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x88
FIELDS:
  public            System.String                   objectRegistrationName  // 0x10
  public            System.String                   parameter  // 0x18
  public            UnityEngine.InputSystem.InputBindingbindingMask  // 0x20
  public            UnityEngine.InputSystem.Utilities.PrimitiveValuevalue  // 0x78
PROPERTIES:
  objectType  get=0x0B1A4FF4
METHODS:
  RVA=0x0B1A4F08  token=0x600023C  System.Void .ctor(System.String parameterName, UnityEngine.InputSystem.InputBinding bindingMask, UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x0B1A4E8C  token=0x600023D  System.Void .ctor(System.String objectRegistrationName, System.String parameterName, UnityEngine.InputSystem.InputBinding bindingMask, UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x0B1A4888  token=0x600023E  System.Nullable<UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride> Find(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding& binding, System.String parameterName, System.String objectRegistrationName)
  RVA=0x0B1A44D0  token=0x600023F  System.Nullable<UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride> Find(UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[] overrides, System.Int32 overrideCount, UnityEngine.InputSystem.InputBinding& binding, System.String parameterName, System.String objectRegistrationName)
  RVA=0x0B1A4AFC  token=0x6000240  System.Nullable<UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride> PickMoreSpecificOne(System.Nullable<UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride> first, System.Nullable<UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride> second)
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsStarted  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsCompleted  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsCanceled  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsOnEventHooked  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsOnAfterUpdateHooked  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsDontIgnoreNoisyControls  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsDontGeneralizePathOfSelectedControl  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsAddNewBinding  // const
  public    static  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.FlagsSuppressMatchingEvents  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x18
FIELDS:
  public            System.String                   group  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600027B  System.Void .ctor()
  RVA=0x0B1B5A10  token=0x600027C  System.Boolean <WithTargetBinding>b__0(UnityEngine.InputSystem.InputControlScheme x)
END_CLASS

CLASS: RebindingOperation
TYPE:  sealed class
TOKEN: 0x2000035
SIZE:  0x178
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static  System.Single                   kDefaultMagnitudeThreshold  // const
  private           UnityEngine.InputSystem.InputActionm_ActionToRebind  // 0x10
  private           System.Nullable<UnityEngine.InputSystem.InputBinding>m_BindingMask  // 0x18
  private           System.Type                     m_ControlType  // 0x78
  private           UnityEngine.InputSystem.Utilities.InternedStringm_ExpectedLayout  // 0x80
  private           System.Int32                    m_IncludePathCount  // 0x90
  private           System.String[]                 m_IncludePaths  // 0x98
  private           System.Int32                    m_ExcludePathCount  // 0xa0
  private           System.String[]                 m_ExcludePaths  // 0xa8
  private           System.Int32                    m_TargetBindingIndex  // 0xb0
  private           System.String                   m_BindingGroupForNewBinding  // 0xb8
  private           System.String                   m_CancelBinding  // 0xc0
  private           System.Single                   m_MagnitudeThreshold  // 0xc8
  private           System.Single[]                 m_Scores  // 0xd0
  private           System.Single[]                 m_Magnitudes  // 0xd8
  private           System.Double                   m_LastMatchTime  // 0xe0
  private           System.Double                   m_StartTime  // 0xe8
  private           System.Single                   m_Timeout  // 0xf0
  private           System.Single                   m_WaitSecondsAfterMatch  // 0xf4
  private           UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>m_Candidates  // 0xf8
  private           System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation>m_OnComplete  // 0x118
  private           System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation>m_OnCancel  // 0x120
  private           System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation>m_OnPotentialMatch  // 0x128
  private           System.Func<UnityEngine.InputSystem.InputControl,System.String>m_OnGeneratePath  // 0x130
  private           System.Func<UnityEngine.InputSystem.InputControl,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Single>m_OnComputeScore  // 0x138
  private           System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation,System.String>m_OnApplyBinding  // 0x140
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice>m_OnEventDelegate  // 0x148
  private           System.Action                   m_OnAfterUpdateDelegate  // 0x150
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.Cachem_LayoutCache  // 0x158
  private           System.Text.StringBuilder       m_PathBuilder  // 0x160
  private           UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flagsm_Flags  // 0x168
  private           System.Collections.Generic.Dictionary<UnityEngine.InputSystem.InputControl,System.Single>m_StartingActuations  // 0x170
PROPERTIES:
  action  get=0x02B2ECC0
  bindingMask  get=0x04D96DC0
  candidates  get=0x04DAB7F0
  scores  get=0x0B1A91AC
  magnitudes  get=0x0B1A9138
  selectedControl  get=0x0B1A9220
  started  get=0x0B1A927C
  completed  get=0x0B1A9108
  canceled  get=0x0B1A90FC
  startTime  get=0x04DA0AD0
  timeout  get=0x04DAB810
  expectedControlType  get=0x0B1A9114
METHODS:
  RVA=0x0B1A81E4  token=0x600024D  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1A8A1C  token=0x600024E  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithMatchingEventsBeingSuppressed(System.Boolean value)
  RVA=0x0B1A8480  token=0x600024F  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(System.String binding)
  RVA=0x0B1A84B8  token=0x6000250  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1A88EC  token=0x6000251  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(System.String layoutName)
  RVA=0x0B1A879C  token=0x6000252  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(System.Type type)
  RVA=-1  // generic def  token=0x6000253  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType()
  RVA=0x0B1A8A90  token=0x6000254  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithTargetBinding(System.Int32 bindingIndex)
  RVA=0x0B1A8438  token=0x6000255  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithBindingMask(System.Nullable<UnityEngine.InputSystem.InputBinding> bindingMask)
  RVA=0x0B1A832C  token=0x6000256  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithBindingGroup(System.String group)
  RVA=0x04DAB7E0  token=0x6000257  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithoutGeneralizingPathOfSelectedControl()
  RVA=0x0B1A8A64  token=0x6000258  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithRebindAddingNewBinding(System.String group)
  RVA=0x0B1A8944  token=0x6000259  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithMagnitudeHavingToBeGreaterThan(System.Single magnitude)
  RVA=0x0B1A9050  token=0x600025A  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithoutIgnoringNoisyControls()
  RVA=0x0B1A8674  token=0x600025B  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithControlsHavingToMatchPath(System.String path)
  RVA=0x0B1A854C  token=0x600025C  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithControlsExcluding(System.String path)
  RVA=0x04DAB7D0  token=0x600025D  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithTimeout(System.Single timeInSeconds)
  RVA=0x0B1A6BE0  token=0x600025E  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnComplete(System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
  RVA=0x0B1A680C  token=0x600025F  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnCancel(System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
  RVA=0x0B1A7B2C  token=0x6000260  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnPotentialMatch(System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
  RVA=0x0B1A7B08  token=0x6000261  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnGeneratePath(System.Func<UnityEngine.InputSystem.InputControl,System.String> callback)
  RVA=0x0B1A6C04  token=0x6000262  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnComputeScore(System.Func<UnityEngine.InputSystem.InputControl,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Single> callback)
  RVA=0x0B1A67B0  token=0x6000263  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnApplyBinding(System.Action<UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation,System.String> callback)
  RVA=0x04DAB7C0  token=0x6000264  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnMatchWaitForAnother(System.Single seconds)
  RVA=0x0B1A7F14  token=0x6000265  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation Start()
  RVA=0x0B1A62D8  token=0x6000266  System.Void Cancel()
  RVA=0x0B1A62EC  token=0x6000267  System.Void Complete()
  RVA=0x0B1A6148  token=0x6000268  System.Void AddCandidate(UnityEngine.InputSystem.InputControl control, System.Single score, System.Single magnitude)
  RVA=0x0B1A7B50  token=0x6000269  System.Void RemoveCandidate(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1A6300  token=0x600026A  System.Void Dispose()
  RVA=0x0B1A6364  token=0x600026B  System.Void Finalize()
  RVA=0x0B1A7D04  token=0x600026C  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation Reset()
  RVA=0x0B1A663C  token=0x600026D  System.Void HookOnEvent()
  RVA=0x0B1A818C  token=0x600026E  System.Void UnhookOnEvent()
  RVA=0x0B1A6C28  token=0x600026F  System.Void OnEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1A7DE8  token=0x6000270  System.Void SortCandidatesByScore()
  RVA=0x0B1A6500  token=0x6000271  System.Boolean HavePathMatch(UnityEngine.InputSystem.InputControl control, System.String[] paths, System.Int32 pathCount)
  RVA=0x0B1A6574  token=0x6000272  System.Void HookOnAfterUpdate()
  RVA=0x0B1A8128  token=0x6000273  System.Void UnhookOnAfterUpdate()
  RVA=0x0B1A6704  token=0x6000274  System.Void OnAfterUpdate()
  RVA=0x0B1A6830  token=0x6000275  System.Void OnComplete()
  RVA=0x0B1A67D4  token=0x6000276  System.Void OnCancel()
  RVA=0x0B1A7C5C  token=0x6000277  System.Void ResetAfterMatchCompleted()
  RVA=0x0B1A80BC  token=0x6000278  System.Void ThrowIfRebindInProgress()
  RVA=0x0B1A63D4  token=0x6000279  System.String GeneratePathForControl(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1A9070  token=0x600027A  System.Void .ctor()
END_CLASS

CLASS: DeferBindingResolutionWrapper
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x10
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x03528E70  token=0x600027D  System.Void Acquire()
  RVA=0x03DFB430  token=0x600027E  System.Void Dispose()
  RVA=0x041E1670  token=0x600027F  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x38
FIELDS:
  public            UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputBinding>bindings  // 0x10
  public            System.Int32                    firstPartIndex  // 0x20
  public            System.String[]                 partStrings  // 0x28
  public            System.Int32                    partCount  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000280  System.Void .ctor()
  RVA=0x0B1B58BC  token=0x6000281  System.String <GetBindingDisplayString>b__0(System.String fragment)
END_CLASS

CLASS: BindingSyntax
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x28
FIELDS:
  private   readonly UnityEngine.InputSystem.InputActionMapm_ActionMap  // 0x10
  private   readonly UnityEngine.InputSystem.InputActionm_Action  // 0x18
  private   readonly System.Int32                    m_BindingIndexInMap  // 0x20
PROPERTIES:
  valid  get=0x0B1ABFA8
  bindingIndex  get=0x0B1ABEC8
  binding  get=0x0B1ABF08
METHODS:
  RVA=0x0B1ABEA0  token=0x60002C2  System.Void .ctor(UnityEngine.InputSystem.InputActionMap map, System.Int32 bindingIndexInMap, UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AB968  token=0x60002C3  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithName(System.String name)
  RVA=0x0B1ABA3C  token=0x60002C4  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithPath(System.String path)
  RVA=0x0B1AB248  token=0x60002C5  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithGroup(System.String group)
  RVA=0x0B1AB424  token=0x60002C6  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithGroups(System.String groups)
  RVA=0x0B1AB5D8  token=0x60002C7  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteraction(System.String interaction)
  RVA=0x0B1AB7B4  token=0x60002C8  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteractions(System.String interactions)
  RVA=-1  // generic def  token=0x60002C9  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteraction()
  RVA=0x0B1ABB10  token=0x60002CA  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessor(System.String processor)
  RVA=0x0B1ABCEC  token=0x60002CB  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessors(System.String processors)
  RVA=-1  // generic def  token=0x60002CC  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessor()
  RVA=0x0B1AB088  token=0x60002CD  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax Triggering(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AAF30  token=0x60002CE  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax To(UnityEngine.InputSystem.InputBinding binding)
  RVA=0x0B1AAD20  token=0x60002CF  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextBinding()
  RVA=0x0B1AAE28  token=0x60002D0  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousBinding()
  RVA=0x0B1AAD90  token=0x60002D1  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextPartBinding(System.String partName)
  RVA=0x0B1AAE98  token=0x60002D2  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousPartBinding(System.String partName)
  RVA=0x0B1AAD54  token=0x60002D3  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextCompositeBinding(System.String compositeName)
  RVA=0x0B1AAE5C  token=0x60002D4  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousCompositeBinding(System.String compositeName)
  RVA=0x0B1AAC38  token=0x60002D5  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax Iterate(System.Boolean next)
  RVA=0x0B1AA95C  token=0x60002D6  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax IterateCompositeBinding(System.Boolean next, System.String compositeName)
  RVA=0x0B1AAABC  token=0x60002D7  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax IteratePartBinding(System.Boolean next, System.String partName)
  RVA=0x0B1AA53C  token=0x60002D8  System.Void Erase()
  RVA=0x0B1AA6E8  token=0x60002D9  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax InsertPartBinding(System.String partName, System.String path)
END_CLASS

CLASS: CompositeSyntax
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x28
FIELDS:
  private   readonly UnityEngine.InputSystem.InputActionm_Action  // 0x10
  private   readonly UnityEngine.InputSystem.InputActionMapm_ActionMap  // 0x18
  private           System.Int32                    m_BindingIndexInMap  // 0x20
PROPERTIES:
  bindingIndex  get=0x0B1AC21C
METHODS:
  RVA=0x0B1AC1F4  token=0x60002DB  System.Void .ctor(UnityEngine.InputSystem.InputActionMap map, UnityEngine.InputSystem.InputAction action, System.Int32 compositeIndex)
  RVA=0x0B1AC000  token=0x60002DC  UnityEngine.InputSystem.InputActionSetupExtensions.CompositeSyntax With(System.String name, System.String binding, System.String groups, System.String processors)
END_CLASS

CLASS: ControlSchemeSyntax
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x38
FIELDS:
  private   readonly UnityEngine.InputSystem.InputActionAssetm_Asset  // 0x10
  private   readonly System.Int32                    m_ControlSchemeIndex  // 0x18
  private           UnityEngine.InputSystem.InputControlSchemem_ControlScheme  // 0x20
METHODS:
  RVA=0x0B1AC748  token=0x60002DD  System.Void .ctor(UnityEngine.InputSystem.InputActionAsset asset, System.Int32 index)
  RVA=0x0B1AC708  token=0x60002DE  System.Void .ctor(UnityEngine.InputSystem.InputControlScheme controlScheme)
  RVA=0x0B1AC54C  token=0x60002DF  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithBindingGroup(System.String bindingGroup)
  RVA=-1  // generic def  token=0x60002E0  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice()
  RVA=-1  // generic def  token=0x60002E1  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice()
  RVA=-1  // generic def  token=0x60002E2  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice()
  RVA=-1  // generic def  token=0x60002E3  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice()
  RVA=0x0B1AC6BC  token=0x60002E4  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice(System.String controlPath)
  RVA=0x0B1AC670  token=0x60002E5  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice(System.String controlPath)
  RVA=0x0B1AC500  token=0x60002E6  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice(System.String controlPath)
  RVA=0x0B1AC4B4  token=0x60002E7  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice(System.String controlPath)
  RVA=-1  // generic def  token=0x60002E8  System.String DeviceTypeToControlPath()
  RVA=0x0B1AC418  token=0x60002E9  UnityEngine.InputSystem.InputControlScheme Done()
  RVA=0x0B1AC240  token=0x60002EA  System.Void AddDeviceEntry(System.String controlPath, UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags flags)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x68
FIELDS:
  public            UnityEngine.InputSystem.InputBindingbinding  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002EB  System.Void .ctor()
  RVA=0x0B1B5A3C  token=0x60002EC  System.Boolean <RemoveAction>b__0(UnityEngine.InputSystem.InputBinding b)
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionState.InteractionState.FlagsTimerRunning  // const
METHODS:
END_CLASS

CLASS: InteractionState
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x40
FIELDS:
  private           System.UInt16                   m_TriggerControlIndex  // 0x10
  private           System.Byte                     m_Phase  // 0x12
  private           System.Byte                     m_Flags  // 0x13
  private           System.Single                   m_TimerDuration  // 0x14
  private           System.Double                   m_StartTime  // 0x18
  private           System.Double                   m_TimerStartTime  // 0x20
  private           System.Double                   m_PerformedTime  // 0x28
  private           System.Single                   m_TotalTimeoutCompletionTimeDone  // 0x30
  private           System.Single                   m_TotalTimeoutCompletionTimeRemaining  // 0x34
  private           System.Int64                    m_TimerMonitorIndex  // 0x38
PROPERTIES:
  triggerControlIndex  get=0x0B1B5644  set=0x0B1B567C
  startTime  get=0x04D9C270  set=0x04D9E9A0
  performedTime  get=0x04D86360  set=0x04D86370
  timerStartTime  get=0x04D96320  set=0x04D85BA0
  timerDuration  get=0x04D9C560  set=0x04DAB920
  totalTimeoutCompletionDone  get=0x011EC8A0  set=0x011EC9E0
  totalTimeoutCompletionTimeRemaining  get=0x04D89850  set=0x04D89860
  timerMonitorIndex  get=0x04D86240  set=0x04D86520
  isTimerRunning  get=0x0B1B563C  set=0x0B1B5658
  phase  get=0x04D91110  set=0x04D91140
METHODS:
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsChainsWithNext  // const
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsEndOfChain  // const
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsComposite  // const
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsPartOfComposite  // const
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsInitialStateCheckPending  // const
  public    static  UnityEngine.InputSystem.InputActionState.BindingState.FlagsWantsInitialStateCheck  // const
METHODS:
END_CLASS

CLASS: BindingState
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x30
FIELDS:
  private           System.Byte                     m_ControlCount  // 0x10
  private           System.Byte                     m_InteractionCount  // 0x11
  private           System.Byte                     m_ProcessorCount  // 0x12
  private           System.Byte                     m_MapIndex  // 0x13
  private           System.Byte                     m_Flags  // 0x14
  private           System.Byte                     m_PartIndex  // 0x15
  private           System.UInt16                   m_ActionIndex  // 0x16
  private           System.UInt16                   m_CompositeOrCompositeBindingIndex  // 0x18
  private           System.UInt16                   m_ProcessorStartIndex  // 0x1a
  private           System.UInt16                   m_InteractionStartIndex  // 0x1c
  private           System.UInt16                   m_ControlStartIndex  // 0x1e
  private           System.Double                   m_PressTime  // 0x20
  private           System.Int32                    m_TriggerEventIdForComposite  // 0x28
  private           System.Int32                    __padding  // 0x2c
PROPERTIES:
  controlStartIndex  get=0x04DA8BD0  set=0x03DFE4B0
  controlCount  get=0x04D883E0  set=0x03DFE5C0
  interactionStartIndex  get=0x03DFE280  set=0x03DFE4D0
  interactionCount  get=0x04D91120  set=0x03DFE4F0
  processorStartIndex  get=0x0B1AA4DC  set=0x03DFE510
  processorCount  get=0x04D91110  set=0x03DFE530
  actionIndex  get=0x03529150  set=0x03DFE3C0
  mapIndex  get=0x04D91130  set=0x03DFE550
  compositeOrCompositeBindingIndex  get=0x03DFDF40  set=0x03DFE3F0
  triggerEventIdForComposite  get=0x011EF5B0  set=0x011EF9B0
  pressTime  get=0x04D96320  set=0x04D85BA0
  flags  get=0x04D90BB0  set=0x04D90BC0
  chainsWithNext  get=0x0B1AA4B4  set=0x0B1AA4F4
  isEndOfChain  get=0x0B1AA4BC  set=0x0B1AA518
  isPartOfChain  get=0x0B1AA4C8
  isComposite  get=0x03DFE3A0  set=0x03DFE420
  isPartOfComposite  get=0x03529320  set=0x03DFE450
  initialStateCheckPending  get=0x03DFD420  set=0x03DFD240
  wantsInitialStateCheck  get=0x03DFCD60  set=0x03DFE480
  partIndex  get=0x04D92DC0  set=0x03DFE590
METHODS:
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsHaveMagnitude  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsPassThrough  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsMayNeedConflictResolution  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsHasMultipleConcurrentActuations  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsInProcessing  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsButton  // const
  public    static  UnityEngine.InputSystem.InputActionState.TriggerState.FlagsPressed  // const
METHODS:
END_CLASS

CLASS: TriggerState
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x58
FIELDS:
  public    static  System.Int32                    kMaxNumMaps  // const
  public    static  System.Int32                    kMaxNumControls  // const
  public    static  System.Int32                    kMaxNumBindings  // const
  private           System.Byte                     m_Phase  // 0x10
  private           System.Byte                     m_Flags  // 0x11
  private           System.Byte                     m_MapIndex  // 0x12
  private           System.UInt16                   m_ControlIndex  // 0x14
  private           System.Double                   m_Time  // 0x18
  private           System.Double                   m_StartTime  // 0x20
  private           System.UInt16                   m_BindingIndex  // 0x28
  private           System.UInt16                   m_InteractionIndex  // 0x2a
  private           System.Single                   m_Magnitude  // 0x2c
  private           System.UInt32                   m_LastPerformedInUpdate  // 0x30
  private           System.UInt32                   m_LastCanceledInUpdate  // 0x34
  private           System.UInt32                   m_PressedInUpdate  // 0x38
  private           System.UInt32                   m_ReleasedInUpdate  // 0x3c
  private           System.UInt32                   m_LastCompletedInUpdate  // 0x40
  private           System.Int32                    framePerformed  // 0x44
  private           System.Int32                    framePressed  // 0x48
  private           System.Int32                    frameReleased  // 0x4c
  private           System.Int32                    frameCompleted  // 0x50
PROPERTIES:
  phase  get=0x04D883E0  set=0x04D87180
  isDisabled  get=0x035291C0
  isWaiting  get=0x0ADC865C
  isStarted  get=0x0ADC8664
  isPerformed  get=0x0B1B570C
  isCanceled  get=0x0B1B5704
  time  get=0x04D9C270  set=0x04D9E9A0
  startTime  get=0x04D96320  set=0x04D85BA0
  magnitude  get=0x04D868F0  set=0x04DAB930
  haveMagnitude  get=0x0B1B56FC
  mapIndex  get=0x04D91110  set=0x03529370
  controlIndex  get=0x03528D80  set=0x03529420
  bindingIndex  get=0x04DA8B70  set=0x03529350
  interactionIndex  get=0x03529130  set=0x03529330
  lastPerformedInUpdate  get=0x02B70F90  set=0x04D86330
  lastCompletedInUpdate  get=0x04D864F0  set=0x04D86740
  lastCanceledInUpdate  get=0x011F0020  set=0x011F0030
  pressedInUpdate  get=0x011F2A90  set=0x011F2AA0
  releasedInUpdate  get=0x04D86710  set=0x04D86720
  isPassThrough  get=0x035291B0  set=0x035293F0
  isButton  get=0x03529310  set=0x035293C0
  isPressed  get=0x03528DA0  set=0x0459ECB0
  mayNeedConflictResolution  get=0x03529720  set=0x03DFE0F0
  hasMultipleConcurrentActuations  get=0x0B1B56F4  set=0x0B1B5714
  inProcessing  get=0x035291A0  set=0x03529390
  flags  get=0x04D91120  set=0x04D91150
METHODS:
END_CLASS

CLASS: ActionMapIndices
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x40
FIELDS:
  public            System.Int32                    actionStartIndex  // 0x10
  public            System.Int32                    actionCount  // 0x14
  public            System.Int32                    controlStartIndex  // 0x18
  public            System.Int32                    controlCount  // 0x1c
  public            System.Int32                    bindingStartIndex  // 0x20
  public            System.Int32                    bindingCount  // 0x24
  public            System.Int32                    interactionStartIndex  // 0x28
  public            System.Int32                    interactionCount  // 0x2c
  public            System.Int32                    processorStartIndex  // 0x30
  public            System.Int32                    processorCount  // 0x34
  public            System.Int32                    compositeStartIndex  // 0x38
  public            System.Int32                    compositeCount  // 0x3c
METHODS:
END_CLASS

CLASS: UnmanagedMemory
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x90
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.Void*                    basePtr  // 0x10
  public            System.Int32                    mapCount  // 0x18
  public            System.Int32                    actionCount  // 0x1c
  public            System.Int32                    interactionCount  // 0x20
  public            System.Int32                    bindingCount  // 0x24
  public            System.Int32                    controlCount  // 0x28
  public            System.Int32                    compositeCount  // 0x2c
  public            UnityEngine.InputSystem.InputActionState.TriggerState*actionStates  // 0x30
  public            UnityEngine.InputSystem.InputActionState.BindingState*bindingStates  // 0x38
  public            UnityEngine.InputSystem.InputActionState.InteractionState*interactionStates  // 0x40
  public            System.Single*                  controlMagnitudes  // 0x48
  public            System.Single*                  compositeMagnitudes  // 0x50
  public            System.Int32*                   enabledControls  // 0x58
  public            System.UInt16*                  actionBindingIndicesAndCounts  // 0x60
  public            System.UInt16*                  actionBindingIndices  // 0x68
  public            System.Int32*                   controlIndexToBindingIndex  // 0x70
  public            System.UInt16*                  controlGroupingAndComplexity  // 0x78
  public            System.Boolean                  controlGroupingInitialized  // 0x80
  public            UnityEngine.InputSystem.InputActionState.ActionMapIndices*mapIndices  // 0x88
PROPERTIES:
  isAllocated  get=0x030CFC40
  sizeInBytes  get=0x03DFDBA0
METHODS:
  RVA=0x03DFDA20  token=0x60003C9  System.Void Allocate(System.Int32 mapCount, System.Int32 actionCount, System.Int32 bindingCount, System.Int32 controlCount, System.Int32 interactionCount, System.Int32 compositeCount)
  RVA=0x03DFDBE0  token=0x60003CA  System.Void Dispose()
  RVA=0x03DFD430  token=0x60003CB  System.Void CopyDataFrom(UnityEngine.InputSystem.InputActionState.UnmanagedMemory memory)
  RVA=0x0B1B5B90  token=0x60003CC  UnityEngine.InputSystem.InputActionState.UnmanagedMemory Clone()
END_CLASS

CLASS: GlobalState
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0xC8
FIELDS:
  private           UnityEngine.InputSystem.Utilities.InlinedArray<System.Runtime.InteropServices.GCHandle>globalList  // 0x10
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<System.Object,UnityEngine.InputSystem.InputActionChange>>onActionChange  // 0x28
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<System.Object>>onActionControlsChanged  // 0x78
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputActionState.<>c<>9  // static @ 0x0
  public    static  UnityEngine.InputSystem.Utilities.SavedStructState.TypedRestore<UnityEngine.InputSystem.InputActionState.GlobalState><>9__140_0  // static @ 0x8
  public    static  System.Action                   <>9__140_1  // static @ 0x10
METHODS:
  RVA=0x0B1B5B2C  token=0x60003CD  System.Void .cctor()
  RVA=0x041E1670  token=0x60003CE  System.Void .ctor()
  RVA=0x0B1B5738  token=0x60003CF  System.Void <SaveAndResetState>b__140_0(UnityEngine.InputSystem.InputActionState.GlobalState& state)
  RVA=0x0B1B587C  token=0x60003D0  System.Void <SaveAndResetState>b__140_1()
END_CLASS

CLASS: DisplayStringOptions
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputBinding.DisplayStringOptionsDontUseShortDisplayNames  // const
  public    static  UnityEngine.InputSystem.InputBinding.DisplayStringOptionsDontOmitDevice  // const
  public    static  UnityEngine.InputSystem.InputBinding.DisplayStringOptionsDontIncludeInteractions  // const
  public    static  UnityEngine.InputSystem.InputBinding.DisplayStringOptionsIgnoreBindingOverrides  // const
METHODS:
END_CLASS

CLASS: MatchOptions
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputBinding.MatchOptionsEmptyGroupMatchesAny  // const
METHODS:
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputBinding.FlagsNone  // const
  public    static  UnityEngine.InputSystem.InputBinding.FlagsComposite  // const
  public    static  UnityEngine.InputSystem.InputBinding.FlagsPartOfComposite  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputBinding.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Boolean><>9__45_0  // static @ 0x8
METHODS:
  RVA=0x0B1B5AC8  token=0x60003FF  System.Void .cctor()
  RVA=0x041E1670  token=0x6000400  System.Void .ctor()
  RVA=0x04B313B0  token=0x6000401  System.Boolean <MaskByGroups>b__45_0(System.String x)
END_CLASS

CLASS: <GetPartNames>d__12
TYPE:  sealed class
TOKEN: 0x2000054
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.String                   <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.String                   composite  // 0x28
  public            System.String                   <>3__composite  // 0x30
  private           System.Reflection.FieldInfo[]   <>7__wrap1  // 0x38
  private           System.Int32                    <>7__wrap2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.String>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x600040E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600040F  System.Void System.IDisposable.Dispose()
  RVA=0x0B1D88C8  token=0x6000410  System.Boolean MoveNext()
  RVA=0x0B1D8AFC  token=0x6000412  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1D8A60  token=0x6000414  System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
  RVA=0x0B1D8AF4  token=0x6000415  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: PartBinding
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x20
FIELDS:
  private           System.Int32                    <part>k__BackingField  // 0x10
  private           UnityEngine.InputSystem.InputControl<control>k__BackingField  // 0x18
PROPERTIES:
  part  get=0x02B77620  set=0x04D86140
  control  get=0x04D88320  set=0x067D5E9C
METHODS:
END_CLASS

CLASS: DefaultComparer`1
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600042A  System.Int32 Compare(TValue x, TValue y)
END_CLASS

CLASS: <get_controls>d__2
TYPE:  sealed class
TOKEN: 0x2000059
SIZE:  0x80
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.InputBindingCompositeContext.PartBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x28
  public            UnityEngine.InputSystem.InputBindingCompositeContext<>4__this  // 0x30
  public            UnityEngine.InputSystem.InputBindingCompositeContext<>3__<>4__this  // 0x40
  private           System.Int32                    <totalBindingCount>5__2  // 0x50
  private           System.Int32                    <bindingIndex>5__3  // 0x54
  private           UnityEngine.InputSystem.InputActionState.BindingState<bindingState>5__4  // 0x58
  private           System.Int32                    <controlStartIndex>5__5  // 0x78
  private           System.Int32                    <i>5__6  // 0x7c
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputBindingCompositeContext.PartBinding>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0B1D91A4
METHODS:
  RVA=0x06C07014  token=0x600042B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600042C  System.Void System.IDisposable.Dispose()
  RVA=0x0B1D8F90  token=0x600042D  System.Boolean MoveNext()
  RVA=0x0B1D9158  token=0x600042F  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1D90BC  token=0x6000431  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputBindingCompositeContext.PartBinding> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputBindingCompositeContext.PartBinding>.GetEnumerator()
  RVA=0x0B1D9150  token=0x6000432  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Result
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlScheme.MatchResult.ResultAllSatisfied  // const
  public    static  UnityEngine.InputSystem.InputControlScheme.MatchResult.ResultMissingRequired  // const
  public    static  UnityEngine.InputSystem.InputControlScheme.MatchResult.ResultMissingOptional  // const
METHODS:
END_CLASS

CLASS: Match
TYPE:  sealed struct
TOKEN: 0x200005E
SIZE:  0x40
FIELDS:
  private           System.Int32                    m_RequirementIndex  // 0x10
  private           UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[]m_Requirements  // 0x18
  private           UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>m_Controls  // 0x20
PROPERTIES:
  control  get=0x0B1D6D30
  device  get=0x0B1D6D6C
  requirementIndex  get=0x02B77620
  requirement  get=0x0B1D6DAC
  isOptional  get=0x0B1D6D88
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    m_Index  // 0x10
  private           UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[]m_Requirements  // 0x18
  private           UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>m_Controls  // 0x20
PROPERTIES:
  Current  get=0x0B1CAFC8
  System.Collections.IEnumerator.Current  get=0x0B1CAF5C
METHODS:
  RVA=0x0B1CAF40  token=0x600045D  System.Boolean MoveNext()
  RVA=0x04DABDE0  token=0x600045E  System.Void Reset()
  RVA=0x041E1670  token=0x6000461  System.Void Dispose()
END_CLASS

CLASS: MatchResult
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.InputControlScheme.MatchResult.Resultm_Result  // 0x10
  private           System.Single                   m_Score  // 0x14
  private           UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputDevice>m_Devices  // 0x18
  private           UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>m_Controls  // 0x38
  private           UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[]m_Requirements  // 0x58
PROPERTIES:
  score  get=0x04D9C560
  isSuccessfulMatch  get=0x09DE6CF4
  hasMissingRequiredDevices  get=0x0B1D6D28
  hasMissingOptionalDevices  get=0x0B1D6D20
  devices  get=0x0B1D6BFC
  Item  get=0x0B1D6B44
METHODS:
  RVA=0x0B1D6AA8  token=0x6000455  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControlScheme.MatchResult.Match> GetEnumerator()
  RVA=0x0B1D6B3C  token=0x6000456  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B1D6A50  token=0x6000457  System.Void Dispose()
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.FlagsNone  // const
  public    static  UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.FlagsOptional  // const
  public    static  UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.FlagsOr  // const
METHODS:
END_CLASS

CLASS: DeviceRequirement
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.String                   m_ControlPath  // 0x10
  private           UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flagsm_Flags  // 0x18
PROPERTIES:
  controlPath  get=0x02B76770  set=0x05392320
  isOptional  get=0x0B1CAEA4  set=0x0B1CAF24
  isAND  get=0x0B1CAE94  set=0x0B1CAEEC
  isOR  get=0x0B1CAE9C  set=0x0B1CAF08
METHODS:
  RVA=0x0B1CADFC  token=0x600046A  System.String ToString()
  RVA=0x0B1CAC8C  token=0x600046B  System.Boolean Equals(UnityEngine.InputSystem.InputControlScheme.DeviceRequirement other)
  RVA=0x0B1CACE8  token=0x600046C  System.Boolean Equals(System.Object obj)
  RVA=0x0B1CAD58  token=0x600046D  System.Int32 GetHashCode()
  RVA=0x0B1CAEAC  token=0x600046E  System.Boolean op_Equality(UnityEngine.InputSystem.InputControlScheme.DeviceRequirement left, UnityEngine.InputSystem.InputControlScheme.DeviceRequirement right)
  RVA=0x0B1CAECC  token=0x600046F  System.Boolean op_Inequality(UnityEngine.InputSystem.InputControlScheme.DeviceRequirement left, UnityEngine.InputSystem.InputControlScheme.DeviceRequirement right)
END_CLASS

CLASS: DeviceJson
TYPE:  sealed struct
TOKEN: 0x2000063
SIZE:  0x20
FIELDS:
  public            System.String                   devicePath  // 0x10
  public            System.Boolean                  isOptional  // 0x18
  public            System.Boolean                  isOR  // 0x19
METHODS:
  RVA=0x0B1CAC40  token=0x6000474  UnityEngine.InputSystem.InputControlScheme.DeviceRequirement ToDeviceEntry()
  RVA=0x0B1CAC04  token=0x6000475  UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson From(UnityEngine.InputSystem.InputControlScheme.DeviceRequirement requirement)
END_CLASS

CLASS: SchemeJson
TYPE:  sealed struct
TOKEN: 0x2000062
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   bindingGroup  // 0x18
  public            UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson[]devices  // 0x20
METHODS:
  RVA=0x0B1D81FC  token=0x6000470  UnityEngine.InputSystem.InputControlScheme ToScheme()
  RVA=0x0B1D7F60  token=0x6000471  UnityEngine.InputSystem.InputControlScheme.SchemeJson ToJson(UnityEngine.InputSystem.InputControlScheme scheme)
  RVA=0x0B1D80FC  token=0x6000472  UnityEngine.InputSystem.InputControlScheme.SchemeJson[] ToJson(UnityEngine.InputSystem.InputControlScheme[] schemes)
  RVA=0x0B1D834C  token=0x6000473  UnityEngine.InputSystem.InputControlScheme[] ToSchemes(UnityEngine.InputSystem.InputControlScheme.SchemeJson[] schemes)
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputInteractionContext.FlagsTimerHasExpired  // const
METHODS:
END_CLASS

CLASS: <data>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0x20F
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: StateEventBuffer
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x228
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.StateEventstateEvent  // 0x10
  public    static  System.Int32                    kMaxSize  // const
  public            UnityEngine.InputSystem.InputSystem.StateEventBuffer.<data>e__FixedBufferdata  // 0x29
METHODS:
END_CLASS

CLASS: <data>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x20F
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: DeltaStateEventBuffer
TYPE:  sealed struct
TOKEN: 0x2000069
SIZE:  0x22C
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.DeltaStateEventstateEvent  // 0x10
  public    static  System.Int32                    kMaxSize  // const
  public            UnityEngine.InputSystem.InputSystem.DeltaStateEventBuffer.<data>e__FixedBufferdata  // 0x2d
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006B
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputSystem.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputControl><>9__80_0  // static @ 0x8
  public    static  System.Func<UnityEngine.InputSystem.InputControl,System.Boolean><>9__80_1  // static @ 0x10
METHODS:
  RVA=0x0B1D8EC8  token=0x60004F9  System.Void .cctor()
  RVA=0x041E1670  token=0x60004FA  System.Void .ctor()
  RVA=0x0B1D8E4C  token=0x60004FB  UnityEngine.InputSystem.InputControl <get_onAnyButtonPress>b__80_0(UnityEngine.InputSystem.LowLevel.InputEventPtr e)
  RVA=0x0721682C  token=0x60004FC  System.Boolean <get_onAnyButtonPress>b__80_1(UnityEngine.InputSystem.InputControl c)
END_CLASS

CLASS: ControlFlags
TYPE:  sealed struct
TOKEN: 0x200006E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsConfigUpToDate  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsIsNoisy  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsIsSynthetic  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsIsButton  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsDontReset  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsSetupFinished  // const
  public    static  UnityEngine.InputSystem.InputControl.ControlFlagsUsesStateFromOtherControl  // const
METHODS:
END_CLASS

CLASS: Enumerate
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlExtensions.EnumerateIgnoreControlsInDefaultState  // const
  public    static  UnityEngine.InputSystem.InputControlExtensions.EnumerateIgnoreControlsInCurrentState  // const
  public    static  UnityEngine.InputSystem.InputControlExtensions.EnumerateIncludeSyntheticControls  // const
  public    static  UnityEngine.InputSystem.InputControlExtensions.EnumerateIncludeNoisyControls  // const
  public    static  UnityEngine.InputSystem.InputControlExtensions.EnumerateIncludeNonLeafControls  // const
METHODS:
END_CLASS

CLASS: InputEventControlCollection
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           UnityEngine.InputSystem.InputDevicem_Device  // 0x10
  private           UnityEngine.InputSystem.LowLevel.InputEventPtrm_EventPtr  // 0x18
  private           UnityEngine.InputSystem.InputControlExtensions.Enumeratem_Flags  // 0x20
  private           System.Single                   m_MagnitudeThreshold  // 0x24
PROPERTIES:
  eventPtr  get=0x04D88320
METHODS:
  RVA=0x0B1D1FA0  token=0x600058D  UnityEngine.InputSystem.InputControlExtensions.InputEventControlEnumerator GetEnumerator()
  RVA=0x0B1D2010  token=0x600058E  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControl> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControl>.GetEnumerator()
  RVA=0x0B1D20A8  token=0x600058F  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: InputEventControlEnumerator
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x80
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.InputControlExtensions.Enumeratem_Flags  // 0x10
  private   readonly UnityEngine.InputSystem.InputDevicem_Device  // 0x18
  private   readonly System.UInt32[]                 m_StateOffsetToControlIndex  // 0x20
  private   readonly System.Int32                    m_StateOffsetToControlIndexLength  // 0x28
  private   readonly UnityEngine.InputSystem.InputControl[]m_AllControls  // 0x30
  private           System.Byte*                    m_DefaultState  // 0x38
  private           System.Byte*                    m_CurrentState  // 0x40
  private           System.Byte*                    m_NoiseMask  // 0x48
  private           UnityEngine.InputSystem.LowLevel.InputEventPtrm_EventPtr  // 0x50
  private           UnityEngine.InputSystem.InputControlm_CurrentControl  // 0x58
  private           System.Int32                    m_CurrentIndexInStateOffsetToControlIndexMap  // 0x60
  private           System.UInt32                   m_CurrentControlStateBitOffset  // 0x64
  private           System.Byte*                    m_EventState  // 0x68
  private           System.UInt32                   m_CurrentBitOffset  // 0x70
  private           System.UInt32                   m_EndBitOffset  // 0x74
  private           System.Single                   m_MagnitudeThreshold  // 0x78
PROPERTIES:
  Current  get=0x04D86200
  System.Collections.IEnumerator.Current  get=0x04D86200
METHODS:
  RVA=0x0B1D28F4  token=0x6000590  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputControlExtensions.Enumerate flags, System.Single magnitudeThreshold)
  RVA=0x0B1D2170  token=0x6000591  System.Boolean CheckDefault(System.UInt32 numBits)
  RVA=0x0B1D2140  token=0x6000592  System.Boolean CheckCurrent(System.UInt32 numBits)
  RVA=0x0B1D21A0  token=0x6000593  System.Boolean MoveNext()
  RVA=0x0B1D256C  token=0x6000594  System.Void Reset()
  RVA=0x04DABE70  token=0x6000595  System.Void Dispose()
END_CLASS

CLASS: ControlBuilder
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x18
FIELDS:
  private           UnityEngine.InputSystem.InputControl<control>k__BackingField  // 0x10
PROPERTIES:
  control  get=0x02B76770  set=0x05392320
METHODS:
  RVA=0x0B1CA3C8  token=0x600059A  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder At(UnityEngine.InputSystem.InputDevice device, System.Int32 index)
  RVA=0x0B1CA650  token=0x600059B  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithParent(UnityEngine.InputSystem.InputControl parent)
  RVA=0x041AC210  token=0x600059C  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithName(System.String name)
  RVA=0x0B1CA584  token=0x600059D  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithDisplayName(System.String displayName)
  RVA=0x0B1CA680  token=0x600059E  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithShortDisplayName(System.String shortDisplayName)
  RVA=0x0B1CA5E8  token=0x600059F  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithLayout(UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x040D5050  token=0x60005A0  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithUsages(System.Int32 startIndex, System.Int32 count)
  RVA=0x040D5020  token=0x60005A1  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithAliases(System.Int32 startIndex, System.Int32 count)
  RVA=0x040D5220  token=0x60005A2  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithChildren(System.Int32 startIndex, System.Int32 count)
  RVA=0x0B1CA6E4  token=0x60005A3  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithStateBlock(UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock)
  RVA=0x0B1CA53C  token=0x60005A4  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithDefaultState(UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x0B1CA618  token=0x60005A5  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithMinAndMax(UnityEngine.InputSystem.Utilities.PrimitiveValue min, UnityEngine.InputSystem.Utilities.PrimitiveValue max)
  RVA=-1  // generic def  token=0x60005A6  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithProcessor(TProcessor processor)
  RVA=0x0B1CA4EC  token=0x60005A7  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsNoisy(System.Boolean value)
  RVA=0x0B1CA514  token=0x60005A8  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsSynthetic(System.Boolean value)
  RVA=0x0B1CA450  token=0x60005A9  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder DontReset(System.Boolean value)
  RVA=0x0B1CA4C4  token=0x60005AA  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsButton(System.Boolean value)
  RVA=0x0B1CA4A4  token=0x60005AB  System.Void Finish()
END_CLASS

CLASS: DeviceBuilder
TYPE:  sealed struct
TOKEN: 0x2000075
SIZE:  0x18
FIELDS:
  private           UnityEngine.InputSystem.InputDevice<device>k__BackingField  // 0x10
PROPERTIES:
  device  get=0x02B76770  set=0x05392320
METHODS:
  RVA=0x041AC210  token=0x60005AE  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithName(System.String name)
  RVA=0x0B1CA584  token=0x60005AF  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithDisplayName(System.String displayName)
  RVA=0x0B1CA680  token=0x60005B0  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithShortDisplayName(System.String shortDisplayName)
  RVA=0x0B1CA5E8  token=0x60005B1  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithLayout(UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x040D5220  token=0x60005B2  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithChildren(System.Int32 startIndex, System.Int32 count)
  RVA=0x0B1CA6E4  token=0x60005B3  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithStateBlock(UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock)
  RVA=0x0B1CA4EC  token=0x60005B4  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder IsNoisy(System.Boolean value)
  RVA=0x0B1CAB70  token=0x60005B5  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlUsage(System.Int32 controlIndex, UnityEngine.InputSystem.Utilities.InternedString usage, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CA9EC  token=0x60005B6  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlAlias(System.Int32 controlIndex, UnityEngine.InputSystem.Utilities.InternedString alias)
  RVA=0x041AC150  token=0x60005B7  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithStateOffsetToControlIndexMap(System.UInt32[] map)
  RVA=0x0B1CAA30  token=0x60005B8  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlTree(System.Byte[] controlTreeNodes, System.UInt16[] controlTreeIndicies)
  RVA=0x0B1CA708  token=0x60005B9  System.Void Finish()
END_CLASS

CLASS: <GetAllButtonPresses>d__43
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0xB8
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.InputControl<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           UnityEngine.InputSystem.LowLevel.InputEventPtreventPtr  // 0x28
  public            UnityEngine.InputSystem.LowLevel.InputEventPtr<>3__eventPtr  // 0x30
  private           System.Single                   magnitude  // 0x38
  public            System.Single                   <>3__magnitude  // 0x3c
  private           System.Boolean                  buttonControlsOnly  // 0x40
  public            System.Boolean                  <>3__buttonControlsOnly  // 0x41
  private           UnityEngine.InputSystem.InputControlExtensions.InputEventControlEnumerator<>7__wrap1  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControl>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60005BA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B1D8850  token=0x60005BB  System.Void System.IDisposable.Dispose()
  RVA=0x0B1D8424  token=0x60005BC  System.Boolean MoveNext()
  RVA=0x04DABE90  token=0x60005BD  System.Void <>m__Finally1()
  RVA=0x0B1D8804  token=0x60005BF  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1D8764  token=0x60005C1  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControl> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControl>.GetEnumerator()
  RVA=0x0B1D87FC  token=0x60005C2  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000079
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly System.UInt64*                  m_Indices  // 0x0
  private   readonly System.Int32                    m_Count  // 0x0
  private           System.Int32                    m_Current  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60005E3  System.Void .ctor(UnityEngine.InputSystem.InputControlList<TControl> list)
  RVA=-1  // not resolved  token=0x60005E4  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60005E5  System.Void Reset()
  RVA=-1  // not resolved  token=0x60005E8  System.Void Dispose()
END_CLASS

CLASS: HumanReadableStringOptions
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptionsNone  // const
  public    static  UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptionsOmitDevice  // const
  public    static  UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptionsUseShortNames  // const
METHODS:
END_CLASS

CLASS: PathComponentType
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlPath.PathComponentTypeName  // const
  public    static  UnityEngine.InputSystem.InputControlPath.PathComponentTypeDisplayName  // const
  public    static  UnityEngine.InputSystem.InputControlPath.PathComponentTypeUsage  // const
  public    static  UnityEngine.InputSystem.InputControlPath.PathComponentTypeLayout  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007E
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputControlPath.ParsedPathComponent.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.Substring,System.String><>9__7_0  // static @ 0x8
METHODS:
  RVA=0x0B1D8E64  token=0x600060F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000610  System.Void .ctor()
  RVA=0x0B1D8E38  token=0x6000611  System.String <get_usages>b__7_0(UnityEngine.InputSystem.Utilities.Substring x)
END_CLASS

CLASS: ParsedPathComponent
TYPE:  sealed struct
TOKEN: 0x200007D
SIZE:  0x60
FIELDS:
  private           UnityEngine.InputSystem.Utilities.Substringm_Layout  // 0x10
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Utilities.Substring>m_Usages  // 0x20
  private           UnityEngine.InputSystem.Utilities.Substringm_Name  // 0x40
  private           UnityEngine.InputSystem.Utilities.Substringm_DisplayName  // 0x50
PROPERTIES:
  layout  get=0x0B1D7A60
  usages  get=0x0B1D7A74
  name  get=0x0B1D7A68
  displayName  get=0x0B1D7974
  isWildcard  get=0x0B1D79F0
  isDoubleWildcard  get=0x0B1D7980
METHODS:
  RVA=0x0B1D71DC  token=0x600060B  System.String ToHumanReadableString(System.String parentLayoutName, System.String parentControlPath, System.String& referencedLayoutName, System.String& controlPath, UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions options)
  RVA=0x0B1D7190  token=0x600060C  System.String ToHumanReadableString(UnityEngine.InputSystem.Utilities.Substring substring)
  RVA=0x0B1D6ECC  token=0x600060D  System.Boolean Matches(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1D6DDC  token=0x600060E  System.Boolean ComparePathElementToString(UnityEngine.InputSystem.Utilities.Substring pathElement, System.String element)
END_CLASS

CLASS: PathParser
TYPE:  sealed struct
TOKEN: 0x200007F
SIZE:  0x78
FIELDS:
  private           System.String                   path  // 0x10
  private           System.Int32                    length  // 0x18
  private           System.Int32                    leftIndexInPath  // 0x1c
  private           System.Int32                    rightIndexInPath  // 0x20
  public            UnityEngine.InputSystem.InputControlPath.ParsedPathComponentcurrent  // 0x28
PROPERTIES:
  isAtEnd  get=0x0B1D7F54
METHODS:
  RVA=0x0B1D7F10  token=0x6000613  System.Void .ctor(System.String path)
  RVA=0x0B1D7BB0  token=0x6000614  System.Boolean MoveToNextComponent()
  RVA=0x0B1D7E40  token=0x6000615  UnityEngine.InputSystem.Utilities.Substring ParseComponentPart(System.Char terminator)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000080
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputControlPath.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.Substring,System.String><>9__9_0  // static @ 0x8
METHODS:
  RVA=0x0B1D8F2C  token=0x6000616  System.Void .cctor()
  RVA=0x041E1670  token=0x6000617  System.Void .ctor()
  RVA=0x0B1D8E38  token=0x6000618  System.String <TryGetDeviceUsages>b__9_0(UnityEngine.InputSystem.Utilities.Substring x)
END_CLASS

CLASS: <Parse>d__34
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0xE8
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.InputControlPath.ParsedPathComponent<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x68
  private           System.String                   path  // 0x70
  public            System.String                   <>3__path  // 0x78
  private           UnityEngine.InputSystem.InputControlPath.PathParser<parser>5__2  // 0x80
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControlPath.ParsedPathComponent>.Current  get=0x04D9C000
  System.Collections.IEnumerator.Current  get=0x0B1D8DB0
METHODS:
  RVA=0x0B1D8E1C  token=0x6000619  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600061A  System.Void System.IDisposable.Dispose()
  RVA=0x0B1D8B48  token=0x600061B  System.Boolean MoveNext()
  RVA=0x0B1D8D64  token=0x600061D  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1D8CC8  token=0x600061F  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputControlPath.ParsedPathComponent> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControlPath.ParsedPathComponent>.GetEnumerator()
  RVA=0x0B1D8D5C  token=0x6000620  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: CachingPolicy
TYPE:  sealed struct
TOKEN: 0x2000083
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputProcessor.CachingPolicyCacheResult  // const
  public    static  UnityEngine.InputSystem.InputProcessor.CachingPolicyEvaluateOnEveryRead  // const
METHODS:
END_CLASS

CLASS: DeviceFlags
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsUpdateBeforeRender  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsHasStateCallbacks  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsHasControlsWithDefaultState  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsHasDontResetControls  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsHasEventMerger  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsHasEventPreProcessor  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsRemote  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsNative  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsDisabledInFrontend  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsDisabledInRuntime  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsDisabledWhileInBackground  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsDisabledStateHasBeenQueriedFromRuntime  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsCanRunInBackground  // const
  public    static  UnityEngine.InputSystem.InputDevice.DeviceFlagsCanRunInBackgroundHasBeenQueried  // const
METHODS:
END_CLASS

CLASS: ControlBitRangeNode
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x17
FIELDS:
  public            System.UInt16                   endBitOffset  // 0x10
  public            System.Int16                    leftChildIndex  // 0x12
  public            System.UInt16                   controlStartIndex  // 0x14
  public            System.Byte                     controlCount  // 0x16
METHODS:
  RVA=0x04DABEB0  token=0x600069C  System.Void .ctor(System.UInt16 endOffset)
END_CLASS

CLASS: MessageType
TYPE:  sealed struct
TOKEN: 0x2000096
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeConnect  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeDisconnect  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeNewLayout  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeNewDevice  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeNewEvents  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeRemoveDevice  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeRemoveLayout  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeChangeUsages  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeStartSending  // const
  public    static  UnityEngine.InputSystem.InputRemoting.MessageTypeStopSending  // const
METHODS:
END_CLASS

CLASS: Message
TYPE:  sealed struct
TOKEN: 0x2000097
SIZE:  0x20
FIELDS:
  public            System.Int32                    participantId  // 0x10
  public            UnityEngine.InputSystem.InputRemoting.MessageTypetype  // 0x14
  public            System.Byte[]                   data  // 0x18
METHODS:
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputRemoting.FlagsSending  // const
  public    static  UnityEngine.InputSystem.InputRemoting.FlagsStartSendingOnConnect  // const
METHODS:
END_CLASS

CLASS: RemoteSender
TYPE:  sealed struct
TOKEN: 0x2000099
SIZE:  0x28
FIELDS:
  public            System.Int32                    senderId  // 0x10
  public            UnityEngine.InputSystem.Utilities.InternedString[]layouts  // 0x18
  public            UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[]devices  // 0x20
METHODS:
END_CLASS

CLASS: RemoteInputDevice
TYPE:  sealed struct
TOKEN: 0x200009A
SIZE:  0x50
FIELDS:
  public            System.Int32                    remoteId  // 0x10
  public            System.Int32                    localId  // 0x14
  public            UnityEngine.InputSystem.Layouts.InputDeviceDescriptiondescription  // 0x18
METHODS:
END_CLASS

CLASS: Subscriber
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  public            UnityEngine.InputSystem.InputRemotingowner  // 0x10
  public            System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>observer  // 0x18
METHODS:
  RVA=0x0B206834  token=0x6000986  System.Void Dispose()
  RVA=0x041E1670  token=0x6000987  System.Void .ctor()
END_CLASS

CLASS: ConnectMsg
TYPE:  static class
TOKEN: 0x200009C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1FFBF4  token=0x6000988  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver)
END_CLASS

CLASS: StartSendingMsg
TYPE:  static class
TOKEN: 0x200009D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B206674  token=0x6000989  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver)
END_CLASS

CLASS: StopSendingMsg
TYPE:  static class
TOKEN: 0x200009E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2067CC  token=0x600098A  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver)
END_CLASS

CLASS: DisconnectMsg
TYPE:  static class
TOKEN: 0x200009F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1FFC28  token=0x600098B  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: Data
TYPE:  sealed struct
TOKEN: 0x20000A1
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   layoutJson  // 0x18
  public            System.Boolean                  isOverride  // 0x20
METHODS:
END_CLASS

CLASS: NewLayoutMsg
TYPE:  static class
TOKEN: 0x20000A0
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B205204  token=0x600098C  System.Nullable<UnityEngine.InputSystem.InputRemoting.Message> Create(UnityEngine.InputSystem.InputRemoting sender, System.String layoutName)
  RVA=0x0B2054DC  token=0x600098D  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: Data
TYPE:  sealed struct
TOKEN: 0x20000A3
SIZE:  0x68
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   layout  // 0x18
  public            System.Int32                    deviceId  // 0x20
  public            System.String[]                 usages  // 0x28
  public            UnityEngine.InputSystem.Layouts.InputDeviceDescriptiondescription  // 0x30
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A4
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x0B20817C  token=0x6000990  System.Void .cctor()
  RVA=0x041E1670  token=0x6000991  System.Void .ctor()
  RVA=0x0B1C93D0  token=0x6000992  System.String <Create>b__1_0(UnityEngine.InputSystem.Utilities.InternedString x)
END_CLASS

CLASS: NewDeviceMsg
TYPE:  static class
TOKEN: 0x20000A2
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B20466C  token=0x600098E  UnityEngine.InputSystem.InputRemoting.Message Create(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B204904  token=0x600098F  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: NewEventsMsg
TYPE:  static class
TOKEN: 0x20000A5
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B204EB0  token=0x6000993  UnityEngine.InputSystem.InputRemoting.Message CreateResetEvent(UnityEngine.InputSystem.InputDevice device, System.Boolean isHardReset)
  RVA=0x0B204F24  token=0x6000994  UnityEngine.InputSystem.InputRemoting.Message CreateStateEvent(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20502C  token=0x6000995  UnityEngine.InputSystem.InputRemoting.Message Create(UnityEngine.InputSystem.LowLevel.InputEvent* events, System.Int32 eventCount)
  RVA=0x0B205114  token=0x6000996  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: Data
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0x20
FIELDS:
  public            System.Int32                    deviceId  // 0x10
  public            System.String[]                 usages  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x0B2081E0  token=0x6000999  System.Void .cctor()
  RVA=0x041E1670  token=0x600099A  System.Void .ctor()
  RVA=0x0B1C93D0  token=0x600099B  System.String <Create>b__1_0(UnityEngine.InputSystem.Utilities.InternedString x)
END_CLASS

CLASS: ChangeUsageMsg
TYPE:  static class
TOKEN: 0x20000A6
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1FF67C  token=0x6000997  UnityEngine.InputSystem.InputRemoting.Message Create(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1FF85C  token=0x6000998  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: RemoveDeviceMsg
TYPE:  static class
TOKEN: 0x20000A9
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2063FC  token=0x600099C  UnityEngine.InputSystem.InputRemoting.Message Create(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B206478  token=0x600099D  System.Void Process(UnityEngine.InputSystem.InputRemoting receiver, UnityEngine.InputSystem.InputRemoting.Message msg)
END_CLASS

CLASS: Subscriber
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  public            UnityEngine.InputSystem.RemoteInputPlayerConnectionowner  // 0x10
  public            System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>observer  // 0x18
METHODS:
  RVA=0x0B2067E8  token=0x60009AF  System.Void Dispose()
  RVA=0x041E1670  token=0x60009B0  System.Void .ctor()
END_CLASS

CLASS: DeviceDisableScope
TYPE:  sealed struct
TOKEN: 0x20000C0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputManager.DeviceDisableScopeEverywhere  // const
  public    static  UnityEngine.InputSystem.InputManager.DeviceDisableScopeInFrontendOnly  // const
  public    static  UnityEngine.InputSystem.InputManager.DeviceDisableScopeTemporaryWhilePlayerIsInBackground  // const
METHODS:
END_CLASS

CLASS: AvailableDevice
TYPE:  sealed struct
TOKEN: 0x20000C1
SIZE:  0x50
FIELDS:
  public            UnityEngine.InputSystem.Layouts.InputDeviceDescriptiondescription  // 0x10
  public            System.Int32                    deviceId  // 0x48
  public            System.Boolean                  isNative  // 0x4c
  public            System.Boolean                  isRemoved  // 0x4d
METHODS:
END_CLASS

CLASS: StateChangeMonitorTimeout
TYPE:  sealed struct
TOKEN: 0x20000C2
SIZE:  0x38
FIELDS:
  public            UnityEngine.InputSystem.InputControlcontrol  // 0x10
  public            System.Double                   time  // 0x18
  public            UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitormonitor  // 0x20
  public            System.Int64                    monitorIndex  // 0x28
  public            System.Int32                    timerIndex  // 0x30
METHODS:
END_CLASS

CLASS: StateChangeMonitorListener
TYPE:  sealed struct
TOKEN: 0x20000C3
SIZE:  0x30
FIELDS:
  public            UnityEngine.InputSystem.InputControlcontrol  // 0x10
  public            UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitormonitor  // 0x18
  public            System.Int64                    monitorIndex  // 0x20
  public            System.UInt32                   groupIndex  // 0x28
METHODS:
END_CLASS

CLASS: StateChangeMonitorsForDevice
TYPE:  sealed struct
TOKEN: 0x20000C4
SIZE:  0x48
FIELDS:
  public            UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion[]memoryRegions  // 0x10
  public            UnityEngine.InputSystem.InputManager.StateChangeMonitorListener[]listeners  // 0x18
  public            UnityEngine.InputSystem.DynamicBitfieldsignalled  // 0x20
  public            System.Boolean                  needToUpdateOrderingOfMonitors  // 0x40
  public            System.Boolean                  needToCompactArrays  // 0x41
PROPERTIES:
  count  get=0x011F2A90
METHODS:
  RVA=0x03DFCF40  token=0x6000ACE  System.Void Add(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.UInt32 groupIndex)
  RVA=0x0B21A06C  token=0x6000ACF  System.Void Remove(UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.Boolean deferRemoval)
  RVA=0x0B219F34  token=0x6000AD0  System.Void Clear()
  RVA=0x0B219F84  token=0x6000AD1  System.Void CompactArrays()
  RVA=0x0B219FE0  token=0x6000AD2  System.Void RemoveAt(System.Int32 i)
  RVA=0x04886DA0  token=0x6000AD3  System.Void SortMonitorsByIndex()
END_CLASS

CLASS: <>c__76`1
TYPE:  sealed class
TOKEN: 0x20000C5
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputManager.<>c__76<TDevice><>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.InputDevice><>9__76_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000AD4  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000AD5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000AD6  UnityEngine.InputSystem.InputDevice <RegisterPrecompiledLayout>b__76_0()
END_CLASS

CLASS: <ListControlLayouts>d__91
TYPE:  sealed class
TOKEN: 0x20000C6
SIZE:  0xE0
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.String                   <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.String                   basedOn  // 0x28
  public            System.String                   <>3__basedOn  // 0x30
  public            UnityEngine.InputSystem.InputManager<>4__this  // 0x38
  private           UnityEngine.InputSystem.Utilities.InternedString<internedBasedOn>5__2  // 0x40
  private           System.Collections.Generic.Dictionary.Enumerator<UnityEngine.InputSystem.Utilities.InternedString,System.Type><>7__wrap2  // 0x50
  private           System.Collections.Generic.Dictionary.Enumerator<UnityEngine.InputSystem.Utilities.InternedString,System.String><>7__wrap3  // 0x80
  private           System.Collections.Generic.Dictionary.Enumerator<UnityEngine.InputSystem.Utilities.InternedString,System.Func<UnityEngine.InputSystem.Layouts.InputControlLayout>><>7__wrap4  // 0xb0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.String>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000AD7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B21C570  token=0x6000AD8  System.Void System.IDisposable.Dispose()
  RVA=0x0B21B71C  token=0x6000AD9  System.Boolean MoveNext()
  RVA=0x0B21C820  token=0x6000ADA  System.Void <>m__Finally1()
  RVA=0x0B21C85C  token=0x6000ADB  System.Void <>m__Finally2()
  RVA=0x0B21C898  token=0x6000ADC  System.Void <>m__Finally3()
  RVA=0x0B21C8D4  token=0x6000ADD  System.Void <>m__Finally4()
  RVA=0x0B21C910  token=0x6000ADE  System.Void <>m__Finally5()
  RVA=0x0B21C94C  token=0x6000ADF  System.Void <>m__Finally6()
  RVA=0x0B21C524  token=0x6000AE1  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B21C478  token=0x6000AE3  System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
  RVA=0x0B21C51C  token=0x6000AE4  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C7
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.InputManager.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.InputDevice,System.String><>9__175_0  // static @ 0x8
METHODS:
  RVA=0x04D45C10  token=0x6000AE5  System.Void .cctor()
  RVA=0x041E1670  token=0x6000AE6  System.Void .ctor()
  RVA=0x04A1E9B0  token=0x6000AE7  System.String <MakeDeviceNameUnique>b__175_0(UnityEngine.InputSystem.InputDevice x)
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x20000C9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputSettings.UpdateModeProcessEventsInDynamicUpdate  // const
  public    static  UnityEngine.InputSystem.InputSettings.UpdateModeProcessEventsInFixedUpdate  // const
  public    static  UnityEngine.InputSystem.InputSettings.UpdateModeProcessEventsManually  // const
METHODS:
END_CLASS

CLASS: ScrollDeltaBehavior
TYPE:  sealed struct
TOKEN: 0x20000CA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputSettings.ScrollDeltaBehaviorUniformAcrossAllPlatforms  // const
  public    static  UnityEngine.InputSystem.InputSettings.ScrollDeltaBehaviorKeepPlatformSpecificInputRange  // const
METHODS:
END_CLASS

CLASS: BackgroundBehavior
TYPE:  sealed struct
TOKEN: 0x20000CB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputSettings.BackgroundBehaviorResetAndDisableNonBackgroundDevices  // const
  public    static  UnityEngine.InputSystem.InputSettings.BackgroundBehaviorResetAndDisableAllDevices  // const
  public    static  UnityEngine.InputSystem.InputSettings.BackgroundBehaviorIgnoreFocus  // const
METHODS:
END_CLASS

CLASS: EditorInputBehaviorInPlayMode
TYPE:  sealed struct
TOKEN: 0x20000CC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputSettings.EditorInputBehaviorInPlayModePointersAndKeyboardsRespectGameViewFocus  // const
  public    static  UnityEngine.InputSystem.InputSettings.EditorInputBehaviorInPlayModeAllDevicesRespectGameViewFocus  // const
  public    static  UnityEngine.InputSystem.InputSettings.EditorInputBehaviorInPlayModeAllDeviceInputAlwaysGoesToGameView  // const
METHODS:
END_CLASS

CLASS: InputActionPropertyDrawerMode
TYPE:  sealed struct
TOKEN: 0x20000CD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputSettings.InputActionPropertyDrawerModeCompact  // const
  public    static  UnityEngine.InputSystem.InputSettings.InputActionPropertyDrawerModeMultilineEffective  // const
  public    static  UnityEngine.InputSystem.InputSettings.InputActionPropertyDrawerModeMultilineBoth  // const
METHODS:
END_CLASS

CLASS: PlayerActions
TYPE:  sealed struct
TOKEN: 0x20000CF
SIZE:  0x18
FIELDS:
  private           UnityEngine.InputSystem.DefaultInputActionsm_Wrapper  // 0x10
PROPERTIES:
  Move  get=0x0AF06FDC
  Look  get=0x0B211C98
  Fire  get=0x0B211C7C
  enabled  get=0x0B211CB4
METHODS:
  RVA=0x05392320  token=0x6000B31  System.Void .ctor(UnityEngine.InputSystem.DefaultInputActions wrapper)
  RVA=0x0AF07200  token=0x6000B35  UnityEngine.InputSystem.InputActionMap Get()
  RVA=0x0B211470  token=0x6000B36  System.Void Enable()
  RVA=0x0B21144C  token=0x6000B37  System.Void Disable()
  RVA=0x0B211CD8  token=0x6000B39  UnityEngine.InputSystem.InputActionMap op_Implicit(UnityEngine.InputSystem.DefaultInputActions.PlayerActions set)
  RVA=0x0B211494  token=0x6000B3A  System.Void SetCallbacks(UnityEngine.InputSystem.DefaultInputActions.IPlayerActions instance)
END_CLASS

CLASS: UIActions
TYPE:  sealed struct
TOKEN: 0x20000D0
SIZE:  0x18
FIELDS:
  private           UnityEngine.InputSystem.DefaultInputActionsm_Wrapper  // 0x10
PROPERTIES:
  Navigate  get=0x0B21E37C
  Submit  get=0x0AF0721C
  Cancel  get=0x0B21E324
  Point  get=0x0B21E398
  Click  get=0x0B21E340
  ScrollWheel  get=0x0B21E3D4
  MiddleClick  get=0x0B21E35C
  RightClick  get=0x0B21E3B4
  TrackedDevicePosition  get=0x0B21E410
  TrackedDeviceOrientation  get=0x0B21E3F0
  enabled  get=0x0B21E430
METHODS:
  RVA=0x05392320  token=0x6000B3B  System.Void .ctor(UnityEngine.InputSystem.DefaultInputActions wrapper)
  RVA=0x0B21C9F0  token=0x6000B46  UnityEngine.InputSystem.InputActionMap Get()
  RVA=0x0B21C9CC  token=0x6000B47  System.Void Enable()
  RVA=0x0B21C9A8  token=0x6000B48  System.Void Disable()
  RVA=0x0B21E454  token=0x6000B4A  UnityEngine.InputSystem.InputActionMap op_Implicit(UnityEngine.InputSystem.DefaultInputActions.UIActions set)
  RVA=0x0B21CA0C  token=0x6000B4B  System.Void SetCallbacks(UnityEngine.InputSystem.DefaultInputActions.IUIActions instance)
END_CLASS

CLASS: IPlayerActions
TYPE:  interface
TOKEN: 0x20000D1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000B4C  System.Void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B4D  System.Void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B4E  System.Void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
END_CLASS

CLASS: IUIActions
TYPE:  interface
TOKEN: 0x20000D2
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000B4F  System.Void OnNavigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B50  System.Void OnSubmit(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B51  System.Void OnCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B52  System.Void OnPoint(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B53  System.Void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B54  System.Void OnScrollWheel(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B55  System.Void OnMiddleClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B56  System.Void OnRightClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B57  System.Void OnTrackedDevicePosition(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=-1  // abstract  token=0x6000B58  System.Void OnTrackedDeviceOrientation(UnityEngine.InputSystem.InputAction.CallbackContext context)
END_CLASS

CLASS: ActionEvent
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x40
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
  private           System.String                   m_ActionId  // 0x30
  private           System.String                   m_ActionName  // 0x38
PROPERTIES:
  actionId  get=0x011F36E0
  actionName  get=0x04D85A50
METHODS:
  RVA=0x0B2083E0  token=0x6000BB2  System.Void .ctor()
  RVA=0x0B20841C  token=0x6000BB3  System.Void .ctor(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B20865C  token=0x6000BB4  System.Void .ctor(System.Guid actionGUID, System.String name)
END_CLASS

CLASS: DeviceLostEvent
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B20960C  token=0x6000BB5  System.Void .ctor()
END_CLASS

CLASS: DeviceRegainedEvent
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B209648  token=0x6000BB6  System.Void .ctor()
END_CLASS

CLASS: ControlsChangedEvent
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B208B5C  token=0x6000BB7  System.Void .ctor()
END_CLASS

CLASS: PlayerJoinedEvent
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B219484  token=0x6000BE2  System.Void .ctor()
END_CLASS

CLASS: PlayerLeftEvent
TYPE:  class
TOKEN: 0x20000DB
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B2194C0  token=0x6000BE3  System.Void .ctor()
END_CLASS

CLASS: TrackingType
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingTypeRotationAndPosition  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingTypeRotationOnly  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingTypePositionOnly  // const
METHODS:
END_CLASS

CLASS: TrackingStates
TYPE:  sealed struct
TOKEN: 0x20000E6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStatesNone  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStatesPosition  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStatesRotation  // const
METHODS:
END_CLASS

CLASS: UpdateType
TYPE:  sealed struct
TOKEN: 0x20000E7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateTypeUpdateAndBeforeRender  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateTypeUpdate  // const
  public    static  UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateTypeBeforeRender  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.XR.XRLayoutBuilderlayout  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000C4B  System.Void .ctor()
  RVA=0x0B21C988  token=0x6000C4C  UnityEngine.InputSystem.Layouts.InputControlLayout <OnFindLayoutForDevice>b__0()
END_CLASS

CLASS: <buffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000FA
SIZE:  0x410
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: DeviceType
TYPE:  sealed struct
TOKEN: 0x20000FE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceTypeGamepad  // const
METHODS:
END_CLASS

CLASS: DeviceSubType
TYPE:  sealed struct
TOKEN: 0x20000FF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeUnknown  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeGamepad  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeWheel  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeArcadeStick  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeFlightStick  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeDancePad  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeGuitar  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeGuitarAlternate  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeDrumKit  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeGuitarBass  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypeArcadePad  // const
METHODS:
END_CLASS

CLASS: DeviceFlags
TYPE:  sealed struct
TOKEN: 0x2000100
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsForceFeedbackSupported  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsWireless  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsVoiceSupported  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsPluginModulesSupported  // const
  public    static  UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsNoNavigation  // const
METHODS:
END_CLASS

CLASS: Capabilities
TYPE:  sealed struct
TOKEN: 0x2000101
SIZE:  0x1C
FIELDS:
  public            UnityEngine.InputSystem.XInput.XInputController.DeviceTypetype  // 0x10
  public            UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypesubType  // 0x14
  public            UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsflags  // 0x18
METHODS:
END_CLASS

CLASS: Button
TYPE:  sealed struct
TOKEN: 0x2000105
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonDPadUp  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonDPadDown  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonDPadLeft  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonDPadRight  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonStart  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonSelect  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonLeftThumbstickPress  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonRightThumbstickPress  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonLeftShoulder  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonRightShoulder  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonA  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonB  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonX  // const
  public    static  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.ButtonY  // const
METHODS:
END_CLASS

CLASS: ControlSchemeChangeSyntax
TYPE:  sealed struct
TOKEN: 0x2000107
SIZE:  0x14
FIELDS:
  private           System.Int32                    m_UserIndex  // 0x10
METHODS:
  RVA=0x0B220704  token=0x6000CF6  UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax AndPairRemainingDevices()
END_CLASS

CLASS: UserFlags
TYPE:  sealed struct
TOKEN: 0x2000108
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Users.InputUser.UserFlagsBindToAllDevices  // const
  public    static  UnityEngine.InputSystem.Users.InputUser.UserFlagsUserAccountSelectionInProgress  // const
METHODS:
END_CLASS

CLASS: UserData
TYPE:  sealed struct
TOKEN: 0x2000109
SIZE:  0xC8
FIELDS:
  public            System.Nullable<UnityEngine.InputSystem.Users.InputUserAccountHandle>platformUserAccountHandle  // 0x10
  public            System.String                   platformUserAccountName  // 0x28
  public            System.String                   platformUserAccountId  // 0x30
  public            System.Int32                    deviceCount  // 0x38
  public            System.Int32                    deviceStartIndex  // 0x3c
  public            UnityEngine.InputSystem.IInputActionCollectionactions  // 0x40
  public            System.Nullable<UnityEngine.InputSystem.InputControlScheme>controlScheme  // 0x48
  public            UnityEngine.InputSystem.InputControlScheme.MatchResultcontrolSchemeMatch  // 0x68
  public            System.Int32                    lostDeviceCount  // 0xb8
  public            System.Int32                    lostDeviceStartIndex  // 0xbc
  public            UnityEngine.InputSystem.Users.InputUser.UserFlagsflags  // 0xc0
METHODS:
END_CLASS

CLASS: CompareDevicesByUserAccount
TYPE:  sealed struct
TOKEN: 0x200010A
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
  public            UnityEngine.InputSystem.Users.InputUserAccountHandleplatformUserAccountHandle  // 0x10
METHODS:
  RVA=0x0B2205BC  token=0x6000CF7  System.Int32 Compare(UnityEngine.InputSystem.InputDevice x, UnityEngine.InputSystem.InputDevice y)
  RVA=0x04D8F590  token=0x6000CF8  System.Nullable<UnityEngine.InputSystem.Users.InputUserAccountHandle> GetUserAccountHandleForDevice(UnityEngine.InputSystem.InputDevice device)
END_CLASS

CLASS: OngoingAccountSelection
TYPE:  sealed struct
TOKEN: 0x200010B
SIZE:  0x20
FIELDS:
  public            UnityEngine.InputSystem.InputDevicedevice  // 0x10
  public            System.UInt32                   userId  // 0x18
METHODS:
END_CLASS

CLASS: GlobalState
TYPE:  sealed struct
TOKEN: 0x200010C
SIZE:  0x178
FIELDS:
  private           System.Int32                    pairingStateVersion  // 0x10
  private           System.UInt32                   lastUserId  // 0x14
  private           System.Int32                    allUserCount  // 0x18
  private           System.Int32                    allPairedDeviceCount  // 0x1c
  private           System.Int32                    allLostDeviceCount  // 0x20
  private           UnityEngine.InputSystem.Users.InputUser[]allUsers  // 0x28
  private           UnityEngine.InputSystem.Users.InputUser.UserData[]allUserData  // 0x30
  private           UnityEngine.InputSystem.InputDevice[]allPairedDevices  // 0x38
  private           UnityEngine.InputSystem.InputDevice[]allLostDevices  // 0x40
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Users.InputUser.OngoingAccountSelection>ongoingAccountSelections  // 0x48
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.Users.InputUser,UnityEngine.InputSystem.Users.InputUserChange,UnityEngine.InputSystem.InputDevice>>onChange  // 0x68
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputControl,UnityEngine.InputSystem.LowLevel.InputEventPtr>>onUnpairedDeviceUsed  // 0xb8
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Func<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Boolean>>onPreFilterUnpairedDeviceUsed  // 0x108
  private           System.Action<System.Object,UnityEngine.InputSystem.InputActionChange>actionChangeDelegate  // 0x158
  private           System.Action<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.InputDeviceChange>onDeviceChangeDelegate  // 0x160
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice>onEventDelegate  // 0x168
  private           System.Boolean                  onActionChangeHooked  // 0x170
  private           System.Boolean                  onDeviceChangeHooked  // 0x171
  private           System.Boolean                  onEventHooked  // 0x172
  private           System.Int32                    listenForUnpairedDeviceActivity  // 0x174
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200010D
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Users.InputUser.<>c<>9  // static @ 0x0
  public    static  UnityEngine.InputSystem.Utilities.SavedStructState.TypedRestore<UnityEngine.InputSystem.Users.InputUser.GlobalState><>9__88_0  // static @ 0x8
  public    static  System.Action                   <>9__88_1  // static @ 0x10
METHODS:
  RVA=0x0B229154  token=0x6000CF9  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CFA  System.Void .ctor()
  RVA=0x0B228ED8  token=0x6000CFB  System.Void <SaveAndResetState>b__88_0(UnityEngine.InputSystem.Users.InputUser.GlobalState& state)
  RVA=0x0B229068  token=0x6000CFC  System.Void <SaveAndResetState>b__88_1()
END_CLASS

CLASS: InputActionReferenceState
TYPE:  sealed struct
TOKEN: 0x200011A
SIZE:  0x18
FIELDS:
  public            System.Int32                    refCount  // 0x10
  public            System.Boolean                  enabledByInputModule  // 0x14
METHODS:
END_CLASS

CLASS: CursorLockBehavior
TYPE:  sealed struct
TOKEN: 0x200011B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehaviorOutsideScreen  // const
  public    static  UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehaviorScreenCenter  // const
METHODS:
END_CLASS

CLASS: ButtonState
TYPE:  sealed struct
TOKEN: 0x2000120
SIZE:  0xA0
FIELDS:
  private           System.Boolean                  m_IsPressed  // 0x10
  private           UnityEngine.EventSystems.PointerEventData.FramePressStatem_FramePressState  // 0x14
  private           System.Single                   m_PressTime  // 0x18
  private           UnityEngine.EventSystems.RaycastResultm_PressRaycast  // 0x20
  private           UnityEngine.GameObject          m_PressObject  // 0x68
  private           UnityEngine.GameObject          m_RawPressObject  // 0x70
  private           UnityEngine.GameObject          m_LastPressObject  // 0x78
  private           UnityEngine.GameObject          m_DragObject  // 0x80
  private           UnityEngine.Vector2             m_PressPosition  // 0x88
  private           System.Single                   m_ClickTime  // 0x90
  private           System.Int32                    m_ClickCount  // 0x94
  private           System.Boolean                  m_Dragging  // 0x98
  private           System.Boolean                  m_ClickedOnSameGameObject  // 0x99
  private           System.Boolean                  m_IgnoreNextClick  // 0x9a
PROPERTIES:
  isPressed  get=0x04D883E0  set=0x03526970
  ignoreNextClick  get=0x04D890B0  set=0x04DA9BE0
  pressTime  get=0x04D9C280  set=0x04DAC1D0
  clickedOnSameGameObject  get=0x04D89060  set=0x04D90C50
  wasPressedThisFrame  get=0x033F1BA0
  wasReleasedThisFrame  get=0x033F1B80
METHODS:
  RVA=0x033F2DE0  token=0x6000DD2  System.Void CopyPressStateTo(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x033F1BC0  token=0x6000DD3  System.Void CopyPressStateFrom(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04DAC1C0  token=0x6000DD4  System.Void OnEndFrame()
END_CLASS

CLASS: RaycastHitData
TYPE:  sealed struct
TOKEN: 0x2000122
SIZE:  0x30
FIELDS:
  private   readonly UnityEngine.UI.Graphic          <graphic>k__BackingField  // 0x10
  private   readonly UnityEngine.Vector3             <worldHitPosition>k__BackingField  // 0x18
  private   readonly UnityEngine.Vector2             <screenPosition>k__BackingField  // 0x24
  private   readonly System.Single                   <distance>k__BackingField  // 0x2c
PROPERTIES:
  graphic  get=0x02B76770
  worldHitPosition  get=0x04D9C4D0
  screenPosition  get=0x04D96410
  distance  get=0x04D868F0
METHODS:
  RVA=0x0B226D28  token=0x6000DE9  System.Void .ctor(UnityEngine.UI.Graphic graphic, UnityEngine.Vector3 worldHitPosition, UnityEngine.Vector2 screenPosition, System.Single distance)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000123
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData><>9__25_0  // static @ 0x8
METHODS:
  RVA=0x0B2290F0  token=0x6000DEE  System.Void .cctor()
  RVA=0x041E1670  token=0x6000DEF  System.Void .ctor()
  RVA=0x0B2290A8  token=0x6000DF0  System.Int32 <SortedRaycastGraphics>b__25_0(UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData g1, UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData g2)
END_CLASS

CLASS: CursorMode
TYPE:  sealed struct
TOKEN: 0x2000125
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.UI.VirtualMouseInput.CursorModeSoftwareCursor  // const
  public    static  UnityEngine.InputSystem.UI.VirtualMouseInput.CursorModeHardwareCursorIfAvailable  // const
METHODS:
END_CLASS

CLASS: SwitchInputOnlyReport
TYPE:  sealed struct
TOKEN: 0x2000127
SIZE:  0x17
FIELDS:
  public    static  System.Int32                    kSize  // const
  public            System.Byte                     buttons0  // 0x10
  public            System.Byte                     buttons1  // 0x11
  public            System.Byte                     hat  // 0x12
  public            System.Byte                     leftX  // 0x13
  public            System.Byte                     leftY  // 0x14
  public            System.Byte                     rightX  // 0x15
  public            System.Byte                     rightY  // 0x16
METHODS:
  RVA=0x0B226FD0  token=0x6000E21  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
END_CLASS

CLASS: SwitchSimpleInputReport
TYPE:  sealed struct
TOKEN: 0x2000128
SIZE:  0x1C
FIELDS:
  public    static  System.Int32                    kSize  // const
  public    static  System.Byte                     ExpectedReportId  // const
  public            System.Byte                     reportId  // 0x10
  public            System.Byte                     buttons0  // 0x11
  public            System.Byte                     buttons1  // 0x12
  public            System.Byte                     hat  // 0x13
  public            System.UInt16                   leftX  // 0x14
  public            System.UInt16                   leftY  // 0x16
  public            System.UInt16                   rightX  // 0x18
  public            System.UInt16                   rightY  // 0x1a
METHODS:
  RVA=0x0B227A98  token=0x6000E22  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
END_CLASS

CLASS: SwitchFullInputReport
TYPE:  sealed struct
TOKEN: 0x2000129
SIZE:  0x29
FIELDS:
  public    static  System.Int32                    kSize  // const
  public    static  System.Byte                     ExpectedReportId  // const
  public            System.Byte                     reportId  // 0x10
  public            System.Byte                     buttons0  // 0x13
  public            System.Byte                     buttons1  // 0x14
  public            System.Byte                     buttons2  // 0x15
  public            System.Byte                     left0  // 0x16
  public            System.Byte                     left1  // 0x17
  public            System.Byte                     left2  // 0x18
  public            System.Byte                     right0  // 0x19
  public            System.Byte                     right1  // 0x1a
  public            System.Byte                     right2  // 0x1b
METHODS:
  RVA=0x0B226D64  token=0x6000E23  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
END_CLASS

CLASS: SwitchHIDGenericInputReport
TYPE:  sealed struct
TOKEN: 0x200012A
SIZE:  0x11
FIELDS:
  public            System.Byte                     reportId  // 0x10
PROPERTIES:
  Format  get=0x04DAC340
METHODS:
END_CLASS

CLASS: ReportType
TYPE:  sealed struct
TOKEN: 0x200012C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.ReportTypeMagic  // const
METHODS:
END_CLASS

CLASS: CommandIdType
TYPE:  sealed struct
TOKEN: 0x200012D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdTypeStatus  // const
  public    static  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdTypeHandshake  // const
  public    static  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdTypeHighspeed  // const
  public    static  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdTypeForceUSB  // const
METHODS:
END_CLASS

CLASS: SwitchMagicOutputReport
TYPE:  sealed struct
TOKEN: 0x200012B
SIZE:  0x41
FIELDS:
  public    static  System.Int32                    kSize  // const
  public    static  System.Byte                     ExpectedReplyInputReportId  // const
  public            System.Byte                     reportType  // 0x10
  public            System.Byte                     commandId  // 0x11
METHODS:
END_CLASS

CLASS: SwitchMagicOutputHIDBluetooth
TYPE:  sealed struct
TOKEN: 0x200012E
SIZE:  0x4C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  public    static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReportreport  // 0x18
PROPERTIES:
  Type  get=0x04DAC3B0
  typeStatic  get=0x04DAC3B0
METHODS:
  RVA=0x04DAC350  token=0x6000E27  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDBluetooth Create(UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
END_CLASS

CLASS: SwitchMagicOutputHIDUSB
TYPE:  sealed struct
TOKEN: 0x200012F
SIZE:  0x58
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  public    static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReportreport  // 0x18
PROPERTIES:
  Type  get=0x04DAC3B0
  typeStatic  get=0x04DAC3B0
METHODS:
  RVA=0x04DAC3C0  token=0x6000E2A  UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDUSB Create(UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
END_CLASS

CLASS: Button
TYPE:  sealed struct
TOKEN: 0x2000132
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonUp  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonRight  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonDown  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonLeft  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonWest  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonNorth  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonSouth  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonEast  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonL  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonR  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonStickL  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonStickR  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonZL  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonZR  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonPlus  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonMinus  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonCapture  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonHome  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonX  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonB  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonY  // const
  public    static  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.ButtonA  // const
METHODS:
END_CLASS

CLASS: OnScreenDeviceInfo
TYPE:  sealed struct
TOKEN: 0x2000135
SIZE:  0x38
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.InputEventPtreventPtr  // 0x10
  public            Unity.Collections.NativeArray<System.Byte>buffer  // 0x18
  public            UnityEngine.InputSystem.InputDevicedevice  // 0x28
  public            UnityEngine.InputSystem.OnScreen.OnScreenControlfirstControl  // 0x30
METHODS:
  RVA=0x0B224EC4  token=0x6000E44  UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo AddControl(UnityEngine.InputSystem.OnScreen.OnScreenControl control)
  RVA=0x0B224FBC  token=0x6000E45  UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo RemoveControl(UnityEngine.InputSystem.OnScreen.OnScreenControl control)
  RVA=0x0B224F20  token=0x6000E46  System.Void Destroy()
END_CLASS

CLASS: Behaviour
TYPE:  sealed struct
TOKEN: 0x2000138
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.OnScreen.OnScreenStick.BehaviourRelativePositionWithStaticOrigin  // const
  public    static  UnityEngine.InputSystem.OnScreen.OnScreenStick.BehaviourExactPositionWithStaticOrigin  // const
  public    static  UnityEngine.InputSystem.OnScreen.OnScreenStick.BehaviourExactPositionWithDynamicOrigin  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200013B
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.HID.HID.HIDLayoutBuilder.<>c<>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.InputSystem.HID.HID.HIDElementDescriptor><>9__4_0  // static @ 0x8
  public    static  System.Predicate<UnityEngine.InputSystem.HID.HID.HIDElementDescriptor><>9__4_1  // static @ 0x10
  public    static  System.Func<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem,System.String><>9__4_2  // static @ 0x18
METHODS:
  RVA=0x0B2395EC  token=0x6000E6F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000E70  System.Void .ctor()
  RVA=0x0B2393A4  token=0x6000E71  System.Boolean <Build>b__4_0(UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element)
  RVA=0x0B2393C0  token=0x6000E72  System.Boolean <Build>b__4_1(UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element)
  RVA=0x0B2393DC  token=0x6000E73  System.String <Build>b__4_2(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem x)
END_CLASS

CLASS: HIDLayoutBuilder
TYPE:  class
TOKEN: 0x200013A
SIZE:  0x58
FIELDS:
  public            System.String                   displayName  // 0x10
  public            UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorhidDescriptor  // 0x18
  public            System.String                   parentLayout  // 0x48
  public            System.Type                     deviceType  // 0x50
METHODS:
  RVA=0x0B220F08  token=0x6000E6D  UnityEngine.InputSystem.Layouts.InputControlLayout Build()
  RVA=0x041E1670  token=0x6000E6E  System.Void .ctor()
END_CLASS

CLASS: HIDReportType
TYPE:  sealed struct
TOKEN: 0x200013C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.HIDReportTypeUnknown  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDReportTypeInput  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDReportTypeOutput  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDReportTypeFeature  // const
METHODS:
END_CLASS

CLASS: HIDCollectionType
TYPE:  sealed struct
TOKEN: 0x200013D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypePhysical  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeApplication  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeLogical  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeReport  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeNamedArray  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeUsageSwitch  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDCollectionTypeUsageModifier  // const
METHODS:
END_CLASS

CLASS: HIDElementFlags
TYPE:  sealed struct
TOKEN: 0x200013E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsConstant  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsVariable  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsRelative  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsWrap  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsNonLinear  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsNoPreferred  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsNullState  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsVolatile  // const
  public    static  UnityEngine.InputSystem.HID.HID.HIDElementFlagsBufferedBytes  // const
METHODS:
END_CLASS

CLASS: HIDElementDescriptor
TYPE:  sealed struct
TOKEN: 0x200013F
SIZE:  0x58
FIELDS:
  public            System.Int32                    usage  // 0x10
  public            UnityEngine.InputSystem.HID.HID.UsagePageusagePage  // 0x14
  public            System.Int32                    unit  // 0x18
  public            System.Int32                    unitExponent  // 0x1c
  public            System.Int32                    logicalMin  // 0x20
  public            System.Int32                    logicalMax  // 0x24
  public            System.Int32                    physicalMin  // 0x28
  public            System.Int32                    physicalMax  // 0x2c
  public            UnityEngine.InputSystem.HID.HID.HIDReportTypereportType  // 0x30
  public            System.Int32                    collectionIndex  // 0x34
  public            System.Int32                    reportId  // 0x38
  public            System.Int32                    reportSizeInBits  // 0x3c
  public            System.Int32                    reportOffsetInBits  // 0x40
  public            UnityEngine.InputSystem.HID.HID.HIDElementFlagsflags  // 0x44
  public            System.Nullable<System.Int32>   usageMin  // 0x48
  public            System.Nullable<System.Int32>   usageMax  // 0x50
PROPERTIES:
  hasNullState  get=0x0B2303E4
  hasPreferredState  get=0x0B2303F0
  isArray  get=0x0B2303FC
  isNonLinear  get=0x0B230410
  isRelative  get=0x0B23041C
  isConstant  get=0x0B230408
  isWrapping  get=0x0B230430
  isSigned  get=0x0B230428
  minFloatValue  get=0x0B2304A4
  maxFloatValue  get=0x0B23043C
METHODS:
  RVA=0x0B2303D4  token=0x6000E7E  System.Boolean Is(UnityEngine.InputSystem.HID.HID.UsagePage usagePage, System.Int32 usage)
  RVA=0x0B22FECC  token=0x6000E7F  System.String DetermineName()
  RVA=0x0B22FC14  token=0x6000E80  System.String DetermineDisplayName()
  RVA=0x0B2303A8  token=0x6000E81  System.Boolean IsUsableElement()
  RVA=0x0B22FDD8  token=0x6000E82  System.String DetermineLayout()
  RVA=0x0B22FCD4  token=0x6000E83  UnityEngine.InputSystem.Utilities.FourCC DetermineFormat()
  RVA=0x0B230214  token=0x6000E84  UnityEngine.InputSystem.Utilities.InternedString[] DetermineUsages()
  RVA=0x0B230088  token=0x6000E85  System.String DetermineParameters()
  RVA=0x0B22F930  token=0x6000E86  System.String DetermineAxisNormalizationParameters()
  RVA=0x0B2301C4  token=0x6000E87  System.String DetermineProcessors()
  RVA=0x0B22FAF4  token=0x6000E88  UnityEngine.InputSystem.Utilities.PrimitiveValue DetermineDefaultState()
  RVA=0x0B22F2AC  token=0x6000E89  System.Void AddChildControls(UnityEngine.InputSystem.HID.HID.HIDElementDescriptor& element, System.String controlName, UnityEngine.InputSystem.Layouts.InputControlLayout.Builder& builder)
END_CLASS

CLASS: HIDCollectionDescriptor
TYPE:  sealed struct
TOKEN: 0x2000140
SIZE:  0x28
FIELDS:
  public            UnityEngine.InputSystem.HID.HID.HIDCollectionTypetype  // 0x10
  public            System.Int32                    usage  // 0x14
  public            UnityEngine.InputSystem.HID.HID.UsagePageusagePage  // 0x18
  public            System.Int32                    parent  // 0x1c
  public            System.Int32                    childCount  // 0x20
  public            System.Int32                    firstChild  // 0x24
METHODS:
END_CLASS

CLASS: HIDDeviceDescriptor
TYPE:  sealed struct
TOKEN: 0x2000141
SIZE:  0x40
FIELDS:
  public            System.Int32                    vendorId  // 0x10
  public            System.Int32                    productId  // 0x14
  public            System.Int32                    usage  // 0x18
  public            UnityEngine.InputSystem.HID.HID.UsagePageusagePage  // 0x1c
  public            System.Int32                    inputReportSize  // 0x20
  public            System.Int32                    outputReportSize  // 0x24
  public            System.Int32                    featureReportSize  // 0x28
  public            UnityEngine.InputSystem.HID.HID.HIDElementDescriptor[]elements  // 0x30
  public            UnityEngine.InputSystem.HID.HID.HIDCollectionDescriptor[]collections  // 0x38
METHODS:
  RVA=0x0B22F248  token=0x6000E8A  System.String ToJson()
  RVA=0x02F13CC0  token=0x6000E8B  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor FromJson(System.String json)
END_CLASS

CLASS: HIDDeviceDescriptorBuilder
TYPE:  sealed struct
TOKEN: 0x2000142
SIZE:  0x48
FIELDS:
  public            UnityEngine.InputSystem.HID.HID.UsagePageusagePage  // 0x10
  public            System.Int32                    usage  // 0x14
  private           System.Int32                    m_CurrentReportId  // 0x18
  private           UnityEngine.InputSystem.HID.HID.HIDReportTypem_CurrentReportType  // 0x1c
  private           System.Int32                    m_CurrentReportOffsetInBits  // 0x20
  private           System.Collections.Generic.List<UnityEngine.InputSystem.HID.HID.HIDElementDescriptor>m_Elements  // 0x28
  private           System.Collections.Generic.List<UnityEngine.InputSystem.HID.HID.HIDCollectionDescriptor>m_Collections  // 0x30
  private           System.Int32                    m_InputReportSize  // 0x38
  private           System.Int32                    m_OutputReportSize  // 0x3c
  private           System.Int32                    m_FeatureReportSize  // 0x40
METHODS:
  RVA=0x04DAC990  token=0x6000E8C  System.Void .ctor(UnityEngine.InputSystem.HID.HID.UsagePage usagePage, System.Int32 usage)
  RVA=0x04DAC960  token=0x6000E8D  System.Void .ctor(UnityEngine.InputSystem.HID.HID.GenericDesktop usage)
  RVA=0x04DAC920  token=0x6000E8E  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder StartReport(UnityEngine.InputSystem.HID.HID.HIDReportType reportType, System.Int32 reportId)
  RVA=0x0B22EA84  token=0x6000E8F  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder AddElement(UnityEngine.InputSystem.HID.HID.UsagePage usagePage, System.Int32 usage, System.Int32 sizeInBits)
  RVA=0x0B22EE48  token=0x6000E90  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder AddElement(UnityEngine.InputSystem.HID.HID.GenericDesktop usage, System.Int32 sizeInBits)
  RVA=0x0B22F0CC  token=0x6000E91  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder WithPhysicalMinMax(System.Int32 min, System.Int32 max)
  RVA=0x0B22EF50  token=0x6000E92  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder WithLogicalMinMax(System.Int32 min, System.Int32 max)
  RVA=0x0B22EE98  token=0x6000E93  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor Finish()
END_CLASS

CLASS: UsagePage
TYPE:  sealed struct
TOKEN: 0x2000143
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageUndefined  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageGenericDesktop  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageSimulation  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageVRControls  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageSportControls  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageGameControls  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageGenericDeviceControls  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageKeyboard  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageLEDs  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageButton  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageOrdinal  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageTelephony  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageConsumer  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageDigitizer  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePagePID  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageUnicode  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageAlphanumericDisplay  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageMedicalInstruments  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageMonitor  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePagePower  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageBarCodeScanner  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageMagneticStripeReader  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageCamera  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageArcade  // const
  public    static  UnityEngine.InputSystem.HID.HID.UsagePageVendorDefined  // const
METHODS:
END_CLASS

CLASS: GenericDesktop
TYPE:  sealed struct
TOKEN: 0x2000144
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopUndefined  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopPointer  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopMouse  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopJoystick  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopGamepad  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopKeyboard  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopKeypad  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopMultiAxisController  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopTabletPCControls  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopAssistiveControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopX  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopY  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopZ  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopRx  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopRy  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopRz  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSlider  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopDial  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopWheel  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopHatSwitch  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopCountedBuffer  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopByteCount  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopMotionWakeup  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopStart  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSelect  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVx  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVy  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVz  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVbrx  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVbry  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVbrz  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopVno  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopFeatureNotification  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopResolutionMultiplier  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemPowerDown  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemSleep  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemWakeUp  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemContextMenu  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMainMenu  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemAppMenu  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuHelp  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuExit  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuSelect  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuRight  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuLeft  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuUp  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemMenuDown  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemColdRestart  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemWarmRestart  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopDpadUp  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopDpadDown  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopDpadRight  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopDpadLeft  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDock  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemUndock  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemSetup  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDebuggerBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopApplicationBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopApplicationDebuggerBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemSpeakerMute  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemHibernate  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayInvert  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayInternal  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayExternal  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayBoth  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayDual  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayToggleIntExt  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplaySwapPrimarySecondary  // const
  public    static  UnityEngine.InputSystem.HID.HID.GenericDesktopSystemDisplayLCDAutoScale  // const
METHODS:
END_CLASS

CLASS: Simulation
TYPE:  sealed struct
TOKEN: 0x2000145
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.SimulationUndefined  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlightSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAutomobileSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationTankSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationSpaceshipSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationSubmarineSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationSailingSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationMotorcycleSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationSportsSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAirplaneSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationHelicopterSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationMagicCarpetSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationBicylcleSimulationDevice  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlightControlStick  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlightStick  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationCyclicControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationCyclicTrim  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlightYoke  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationTrackControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAileron  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAileronTrim  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAntiTorqueControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAutopilotEnable  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationChaffRelease  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationCollectiveControl  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationDiveBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationElectronicCountermeasures  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationElevator  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationElevatorTrim  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationRudder  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationThrottle  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlightCommunications  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFlareRelease  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationLandingGear  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationToeBreak  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationTrigger  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationWeaponsArm  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationWeaponsSelect  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationWingFlaps  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationAccelerator  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationBrake  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationClutch  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationShifter  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationSteering  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationTurretDirection  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationBarrelElevation  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationDivePlane  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationBallast  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationBicycleCrank  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationHandleBars  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationFrontBrake  // const
  public    static  UnityEngine.InputSystem.HID.HID.SimulationRearBrake  // const
METHODS:
END_CLASS

CLASS: Button
TYPE:  sealed struct
TOKEN: 0x2000146
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HID.ButtonUndefined  // const
  public    static  UnityEngine.InputSystem.HID.HID.ButtonPrimary  // const
  public    static  UnityEngine.InputSystem.HID.HID.ButtonSecondary  // const
  public    static  UnityEngine.InputSystem.HID.HID.ButtonTertiary  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000147
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.HID.HID.HIDLayoutBuilderlayout  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000E94  System.Void .ctor()
  RVA=0x0B239568  token=0x6000E95  UnityEngine.InputSystem.Layouts.InputControlLayout <OnFindLayoutForDevice>b__0()
END_CLASS

CLASS: HIDReportData
TYPE:  sealed struct
TOKEN: 0x2000149
SIZE:  0x1C
FIELDS:
  public            System.Int32                    reportId  // 0x10
  public            UnityEngine.InputSystem.HID.HID.HIDReportTypereportType  // 0x14
  public            System.Int32                    currentBitOffset  // 0x18
METHODS:
  RVA=0x0B23170C  token=0x6000E99  System.Int32 FindOrAddReport(System.Nullable<System.Int32> reportId, UnityEngine.InputSystem.HID.HID.HIDReportType reportType, System.Collections.Generic.List<UnityEngine.InputSystem.HID.HIDParser.HIDReportData> reports)
END_CLASS

CLASS: HIDItemTypeAndTag
TYPE:  sealed struct
TOKEN: 0x200014A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagInput  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagOutput  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagFeature  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagCollection  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagEndCollection  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUsagePage  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagLogicalMinimum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagLogicalMaximum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagPhysicalMinimum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagPhysicalMaximum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUnitExponent  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUnit  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagReportSize  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagReportID  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagReportCount  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagPush  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagPop  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUsage  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUsageMinimum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagUsageMaximum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagDesignatorIndex  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagDesignatorMinimum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagDesignatorMaximum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagStringIndex  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagStringMinimum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagStringMaximum  // const
  public    static  UnityEngine.InputSystem.HID.HIDParser.HIDItemTypeAndTagDelimiter  // const
METHODS:
END_CLASS

CLASS: HIDItemStateLocal
TYPE:  sealed struct
TOKEN: 0x200014B
SIZE:  0x60
FIELDS:
  public            System.Nullable<System.Int32>   usage  // 0x10
  public            System.Nullable<System.Int32>   usageMinimum  // 0x18
  public            System.Nullable<System.Int32>   usageMaximum  // 0x20
  public            System.Nullable<System.Int32>   designatorIndex  // 0x28
  public            System.Nullable<System.Int32>   designatorMinimum  // 0x30
  public            System.Nullable<System.Int32>   designatorMaximum  // 0x38
  public            System.Nullable<System.Int32>   stringIndex  // 0x40
  public            System.Nullable<System.Int32>   stringMinimum  // 0x48
  public            System.Nullable<System.Int32>   stringMaximum  // 0x50
  public            System.Collections.Generic.List<System.Int32>usageList  // 0x58
METHODS:
  RVA=0x0B2307B4  token=0x6000E9A  System.Void Reset(UnityEngine.InputSystem.HID.HIDParser.HIDItemStateLocal& state)
  RVA=0x0B230814  token=0x6000E9B  System.Void SetUsage(System.Int32 value)
  RVA=0x0B2306C4  token=0x6000E9C  System.Int32 GetUsage(System.Int32 index)
END_CLASS

CLASS: HIDItemStateGlobal
TYPE:  sealed struct
TOKEN: 0x200014C
SIZE:  0x60
FIELDS:
  public            System.Nullable<System.Int32>   usagePage  // 0x10
  public            System.Nullable<System.Int32>   logicalMinimum  // 0x18
  public            System.Nullable<System.Int32>   logicalMaximum  // 0x20
  public            System.Nullable<System.Int32>   physicalMinimum  // 0x28
  public            System.Nullable<System.Int32>   physicalMaximum  // 0x30
  public            System.Nullable<System.Int32>   unitExponent  // 0x38
  public            System.Nullable<System.Int32>   unit  // 0x40
  public            System.Nullable<System.Int32>   reportSize  // 0x48
  public            System.Nullable<System.Int32>   reportCount  // 0x50
  public            System.Nullable<System.Int32>   reportId  // 0x58
METHODS:
  RVA=0x0B23064C  token=0x6000E9D  UnityEngine.InputSystem.HID.HID.UsagePage GetUsagePage(System.Int32 index, UnityEngine.InputSystem.HID.HIDParser.HIDItemStateLocal& localItemState)
  RVA=0x0B2305AC  token=0x6000E9E  System.Int32 GetPhysicalMin()
  RVA=0x0B23050C  token=0x6000E9F  System.Int32 GetPhysicalMax()
END_CLASS

CLASS: HIDPageUsage
TYPE:  sealed struct
TOKEN: 0x200014E
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.HID.HID.UsagePagepage  // 0x10
  public            System.Int32                    usage  // 0x14
METHODS:
  RVA=0x04D85C40  token=0x6000EA3  System.Void .ctor(UnityEngine.InputSystem.HID.HID.UsagePage page, System.Int32 usage)
  RVA=0x04DAC9C0  token=0x6000EA4  System.Void .ctor(UnityEngine.InputSystem.HID.HID.GenericDesktop usage)
END_CLASS

CLASS: GlobalState
TYPE:  sealed struct
TOKEN: 0x2000152
SIZE:  0x160
FIELDS:
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Touchscreen>touchscreens  // 0x10
  private           System.Int32                    historyLengthPerFinger  // 0x28
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.EnhancedTouch.Finger>>onFingerDown  // 0x30
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.EnhancedTouch.Finger>>onFingerMove  // 0x80
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.EnhancedTouch.Finger>>onFingerUp  // 0xd0
  private           UnityEngine.InputSystem.EnhancedTouch.Touch.FingerAndTouchStateplayerState  // 0x120
METHODS:
END_CLASS

CLASS: FingerAndTouchState
TYPE:  sealed struct
TOKEN: 0x2000153
SIZE:  0x50
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.InputUpdateTypeupdateMask  // 0x10
  public            UnityEngine.InputSystem.EnhancedTouch.Finger[]fingers  // 0x18
  public            UnityEngine.InputSystem.EnhancedTouch.Finger[]activeFingers  // 0x20
  public            UnityEngine.InputSystem.EnhancedTouch.Touch[]activeTouches  // 0x28
  public            System.Int32                    activeFingerCount  // 0x30
  public            System.Int32                    activeTouchCount  // 0x34
  public            System.Int32                    totalFingerCount  // 0x38
  public            System.UInt32                   lastId  // 0x3c
  public            System.Boolean                  haveBuiltActiveTouches  // 0x40
  public            System.Boolean                  haveActiveTouchesNeedingRefreshNextUpdate  // 0x41
  public            UnityEngine.InputSystem.LowLevel.InputStateHistory<UnityEngine.InputSystem.LowLevel.TouchState>activeTouchState  // 0x48
METHODS:
  RVA=0x0B22D870  token=0x6000EE8  System.Void AddFingers(UnityEngine.InputSystem.Touchscreen screen)
  RVA=0x0B22DA08  token=0x6000EE9  System.Void RemoveFingers(UnityEngine.InputSystem.Touchscreen screen)
  RVA=0x0B22D98C  token=0x6000EEA  System.Void Destroy()
  RVA=0x0B22DB24  token=0x6000EEB  System.Void UpdateActiveFingers()
  RVA=0x042EEFE0  token=0x6000EEC  System.Void UpdateActiveTouches()
END_CLASS

CLASS: ExtraDataPerTouchState
TYPE:  sealed struct
TOKEN: 0x2000154
SIZE:  0x1C
FIELDS:
  public            UnityEngine.Vector2             accumulatedDelta  // 0x10
  public            System.UInt32                   uniqueId  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000155
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.EnhancedTouch.Touch.<>c<>9  // static @ 0x0
  public    static  UnityEngine.InputSystem.Utilities.SavedStructState.TypedRestore<UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState><>9__80_0  // static @ 0x8
  public    static  System.Action                   <>9__80_1  // static @ 0x10
METHODS:
  RVA=0x0B239588  token=0x6000EED  System.Void .cctor()
  RVA=0x041E1670  token=0x6000EEE  System.Void .ctor()
  RVA=0x0B2393FC  token=0x6000EEF  System.Void <SaveAndResetState>b__80_0(UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState& state)
  RVA=0x041E1670  token=0x6000EF0  System.Void <SaveAndResetState>b__80_1()
END_CLASS

CLASS: Enumerator
TYPE:  class
TOKEN: 0x2000157
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.EnhancedTouch.TouchHistorym_Owner  // 0x10
  private           System.Int32                    m_Index  // 0x30
PROPERTIES:
  Current  get=0x0B22D828
  System.Collections.IEnumerator.Current  get=0x0B22D71C
METHODS:
  RVA=0x0B22D7C4  token=0x6000EF7  System.Void .ctor(UnityEngine.InputSystem.EnhancedTouch.TouchHistory owner)
  RVA=0x0B22D640  token=0x6000EF8  System.Boolean MoveNext()
  RVA=0x02B2C650  token=0x6000EF9  System.Void Reset()
  RVA=0x041E1670  token=0x6000EFC  System.Void Dispose()
END_CLASS

CLASS: DualSenseHIDGenericInputReport
TYPE:  sealed struct
TOKEN: 0x200015B
SIZE:  0x11
FIELDS:
  public            System.Byte                     reportId  // 0x10
PROPERTIES:
  Format  get=0x04DAC340
METHODS:
END_CLASS

CLASS: DualSenseHIDUSBInputReport
TYPE:  sealed struct
TOKEN: 0x200015C
SIZE:  0x1B
FIELDS:
  public    static  System.Int32                    ExpectedReportId  // const
  public            System.Byte                     reportId  // 0x10
  public            System.Byte                     leftStickX  // 0x11
  public            System.Byte                     leftStickY  // 0x12
  public            System.Byte                     rightStickX  // 0x13
  public            System.Byte                     rightStickY  // 0x14
  public            System.Byte                     leftTrigger  // 0x15
  public            System.Byte                     rightTrigger  // 0x16
  public            System.Byte                     buttons0  // 0x18
  public            System.Byte                     buttons1  // 0x19
  public            System.Byte                     buttons2  // 0x1a
METHODS:
  RVA=0x04DAC700  token=0x6000F42  UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
END_CLASS

CLASS: DualSenseHIDBluetoothInputReport
TYPE:  sealed struct
TOKEN: 0x200015D
SIZE:  0x1C
FIELDS:
  public    static  System.Int32                    ExpectedReportId  // const
  public            System.Byte                     reportId  // 0x10
  public            System.Byte                     leftStickX  // 0x12
  public            System.Byte                     leftStickY  // 0x13
  public            System.Byte                     rightStickX  // 0x14
  public            System.Byte                     rightStickY  // 0x15
  public            System.Byte                     leftTrigger  // 0x16
  public            System.Byte                     rightTrigger  // 0x17
  public            System.Byte                     buttons0  // 0x19
  public            System.Byte                     buttons1  // 0x1a
  public            System.Byte                     buttons2  // 0x1b
METHODS:
  RVA=0x04DAC5F0  token=0x6000F43  UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
END_CLASS

CLASS: DualSenseHIDMinimalInputReport
TYPE:  sealed struct
TOKEN: 0x200015E
SIZE:  0x1A
FIELDS:
  public    static  System.Int32                    ExpectedSize1  // static @ 0x0
  public    static  System.Int32                    ExpectedSize2  // static @ 0x4
  public            System.Byte                     reportId  // 0x10
  public            System.Byte                     leftStickX  // 0x11
  public            System.Byte                     leftStickY  // 0x12
  public            System.Byte                     rightStickX  // 0x13
  public            System.Byte                     rightStickY  // 0x14
  public            System.Byte                     buttons0  // 0x15
  public            System.Byte                     buttons1  // 0x16
  public            System.Byte                     buttons2  // 0x17
  public            System.Byte                     leftTrigger  // 0x18
  public            System.Byte                     rightTrigger  // 0x19
METHODS:
  RVA=0x04DAC6B0  token=0x6000F44  UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
  RVA=0x0B22C128  token=0x6000F45  System.Void .cctor()
END_CLASS

CLASS: DualShock4HIDGenericInputReport
TYPE:  sealed struct
TOKEN: 0x2000160
SIZE:  0x19
FIELDS:
  public            System.Byte                     leftStickX  // 0x10
  public            System.Byte                     leftStickY  // 0x11
  public            System.Byte                     rightStickX  // 0x12
  public            System.Byte                     rightStickY  // 0x13
  public            System.Byte                     buttons0  // 0x14
  public            System.Byte                     buttons1  // 0x15
  public            System.Byte                     buttons2  // 0x16
  public            System.Byte                     leftTrigger  // 0x17
  public            System.Byte                     rightTrigger  // 0x18
PROPERTIES:
  Format  get=0x04DAC340
METHODS:
  RVA=0x04DAC7A0  token=0x6000F59  UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport ToHIDInputReport()
END_CLASS

CLASS: <rawData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000168
SIZE:  0x5A
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <padding3>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200016B
SIZE:  0x18
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x200016D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.FlagsRumble  // const
  public    static  UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.FlagsColor  // const
METHODS:
END_CLASS

CLASS: <unknown1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200016E
SIZE:  0x12
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <unknown2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200016F
SIZE:  0x27
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Result
TYPE:  sealed struct
TOKEN: 0x2000179
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand.ResultSuccessfullyInitiated  // const
  public    static  UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand.ResultErrorNotSupported  // const
  public    static  UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand.ResultErrorAlreadyInProgress  // const
METHODS:
END_CLASS

CLASS: <nameBuffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000181
SIZE:  0x110
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <nameBuffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000183
SIZE:  0x110
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Result
TYPE:  sealed struct
TOKEN: 0x2000185
SIZE:  0x18
FIELDS:
  public            System.Int64                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.ResultDevicePairedToUserAccount  // const
  public    static  UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.ResultUserAccountSelectionInProgress  // const
  public    static  UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.ResultUserAccountSelectionComplete  // const
  public    static  UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.ResultUserAccountSelectionCanceled  // const
METHODS:
END_CLASS

CLASS: <nameBuffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000186
SIZE:  0x210
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <idBuffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000187
SIZE:  0x210
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <idBuffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200018A
SIZE:  0x210
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Button
TYPE:  sealed struct
TOKEN: 0x2000199
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.JoystickState.ButtonHatSwitchUp  // const
  public    static  UnityEngine.InputSystem.LowLevel.JoystickState.ButtonHatSwitchDown  // const
  public    static  UnityEngine.InputSystem.LowLevel.JoystickState.ButtonHatSwitchLeft  // const
  public    static  UnityEngine.InputSystem.LowLevel.JoystickState.ButtonHatSwitchRight  // const
  public    static  UnityEngine.InputSystem.LowLevel.JoystickState.ButtonTrigger  // const
METHODS:
END_CLASS

CLASS: <keys>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200019B
SIZE:  0x20
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <primaryTouchData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001A8
SIZE:  0x48
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <touchData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001A9
SIZE:  0x240
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <m_ValueData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001AB
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <stateData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001AD
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x20001B4
SIZE:  0x9C
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.LowLevel.IMECompositionStringm_CompositionString  // 0x10
  private           System.Char                     m_CurrentCharacter  // 0x94
  private           System.Int32                    m_CurrentIndex  // 0x98
PROPERTIES:
  Current  get=0x04D9CCF0
  System.Collections.IEnumerator.Current  get=0x0B22D77C
METHODS:
  RVA=0x04DAC890  token=0x6001041  System.Void .ctor(UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString)
  RVA=0x0B22D6AC  token=0x6001042  System.Boolean MoveNext()
  RVA=0x04DAB0B0  token=0x6001043  System.Void Reset()
  RVA=0x041E1670  token=0x6001044  System.Void Dispose()
END_CLASS

CLASS: <buffer>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001B5
SIZE:  0x90
FIELDS:
  public            System.Char                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x20001B8
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputEvent*m_Buffer  // 0x10
  private   readonly System.Int32                    m_EventCount  // 0x18
  private           UnityEngine.InputSystem.LowLevel.InputEvent*m_CurrentEvent  // 0x20
  private           System.Int32                    m_CurrentIndex  // 0x28
PROPERTIES:
  Current  get=0x02B2ECC0
  System.Collections.IEnumerator.Current  get=0x0B22D6D4
METHODS:
  RVA=0x0B22D7F0  token=0x600106B  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEventBuffer buffer)
  RVA=0x0B22D65C  token=0x600106C  System.Boolean MoveNext()
  RVA=0x04D8FA80  token=0x600106D  System.Void Reset()
  RVA=0x041E1670  token=0x600106E  System.Void Dispose()
END_CLASS

CLASS: ObserverState
TYPE:  class
TOKEN: 0x20001BA
SIZE:  0x30
FIELDS:
  public            UnityEngine.InputSystem.Utilities.InlinedArray<System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr>>observers  // 0x10
  public            System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice>onEventDelegate  // 0x28
METHODS:
  RVA=0x0B23587C  token=0x6001074  System.Void .ctor()
  RVA=0x0B2357F4  token=0x6001075  System.Void <.ctor>b__2_0(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device)
END_CLASS

CLASS: DisposableObserver
TYPE:  class
TOKEN: 0x20001BB
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr>observer  // 0x10
METHODS:
  RVA=0x0B22B064  token=0x6001076  System.Void Dispose()
  RVA=0x041E1670  token=0x6001077  System.Void .ctor()
END_CLASS

CLASS: Enumerator
TYPE:  class
TOKEN: 0x20001BF
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.LowLevel.InputEventTracem_Trace  // 0x10
  private           System.Int32                    m_ChangeCounter  // 0x18
  private           UnityEngine.InputSystem.LowLevel.InputEventPtrm_Current  // 0x20
PROPERTIES:
  Current  get=0x04D862C0
  System.Collections.IEnumerator.Current  get=0x0B23CDB0
METHODS:
  RVA=0x0B23CE50  token=0x60010CE  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEventTrace trace)
  RVA=0x0B23CC88  token=0x60010CF  System.Void Dispose()
  RVA=0x0B23CCAC  token=0x60010D0  System.Boolean MoveNext()
  RVA=0x0B23CD8C  token=0x60010D1  System.Void Reset()
END_CLASS

CLASS: FileFlags
TYPE:  sealed struct
TOKEN: 0x20001C0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.InputEventTrace.FileFlagsFixedUpdate  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001C2
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.InputSystem.LowLevel.InputEventPtr><>9__38_0  // static @ 0x8
METHODS:
  RVA=0x0B249EF0  token=0x60010ED  System.Void .cctor()
  RVA=0x041E1670  token=0x60010EE  System.Void .ctor()
  RVA=0x0B249D80  token=0x60010EF  System.Int32 <PlayAllEventsAccordingToTimestamps>b__38_0(UnityEngine.InputSystem.LowLevel.InputEventPtr a, UnityEngine.InputSystem.LowLevel.InputEventPtr b)
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x20001C3
SIZE:  0x18
FIELDS:
  public            System.Int32                    originalDeviceId  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60010F0  System.Void .ctor()
  RVA=0x0702DACC  token=0x60010F1  System.Boolean <ApplyDeviceMapping>b__0(UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo x)
END_CLASS

CLASS: ReplayController
TYPE:  class
TOKEN: 0x20001C1
SIZE:  0x90
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  <finished>k__BackingField  // 0x10
  private           System.Boolean                  <paused>k__BackingField  // 0x11
  private           System.Int32                    <position>k__BackingField  // 0x14
  private           UnityEngine.InputSystem.LowLevel.InputEventTracem_EventTrace  // 0x18
  private           UnityEngine.InputSystem.LowLevel.InputEventTrace.Enumeratorm_Enumerator  // 0x20
  private           UnityEngine.InputSystem.Utilities.InlinedArray<System.Collections.Generic.KeyValuePair<System.Int32,System.Int32>>m_DeviceIDMappings  // 0x28
  private           System.Boolean                  m_CreateNewDevices  // 0x40
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputDevice>m_CreatedDevices  // 0x48
  private           System.Action                   m_OnFinished  // 0x60
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr>m_OnEvent  // 0x68
  private           System.Double                   m_StartTimeAsPerFirstEvent  // 0x70
  private           System.Double                   m_StartTimeAsPerRuntime  // 0x78
  private           System.Int32                    m_AllEventsByTimeIndex  // 0x80
  private           System.Collections.Generic.List<UnityEngine.InputSystem.LowLevel.InputEventPtr>m_AllEventsByTime  // 0x88
PROPERTIES:
  trace  get=0x0385B100
  finished  get=0x015EFCE0  set=0x04D86130
  paused  get=0x04D88380  set=0x04D88460
  position  get=0x04D882A0  set=0x04D8F540
  createdDevices  get=0x0B2491BC
METHODS:
  RVA=0x0B249148  token=0x60010DC  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEventTrace trace)
  RVA=0x0B2482E4  token=0x60010DD  System.Void Dispose()
  RVA=0x0B248F6C  token=0x60010DE  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithDeviceMappedFromTo(UnityEngine.InputSystem.InputDevice recordedDevice, UnityEngine.InputSystem.InputDevice playbackDevice)
  RVA=0x0B249040  token=0x60010DF  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithDeviceMappedFromTo(System.Int32 recordedDeviceId, System.Int32 playbackDeviceId)
  RVA=0x04DACF10  token=0x60010E0  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithAllDevicesMappedToNewInstances()
  RVA=0x06969620  token=0x60010E1  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController OnFinished(System.Action action)
  RVA=0x0696963C  token=0x60010E2  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController OnEvent(System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr> action)
  RVA=0x0B248CB4  token=0x60010E3  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayOneEvent()
  RVA=0x0B248F28  token=0x60010E4  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController Rewind()
  RVA=0x0B248C1C  token=0x60010E5  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllFramesOneByOne()
  RVA=0x0B248B78  token=0x60010E6  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllEvents()
  RVA=0x0B248930  token=0x60010E7  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllEventsAccordingToTimestamps()
  RVA=0x0B2487B0  token=0x60010E8  System.Void OnBeginFrame()
  RVA=0x0B248488  token=0x60010E9  System.Void Finished()
  RVA=0x0B248D44  token=0x60010EA  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B24852C  token=0x60010EB  System.Boolean MoveNext(System.Boolean skipFrameEvents, UnityEngine.InputSystem.LowLevel.InputEventPtr& eventPtr)
  RVA=0x0B247F88  token=0x60010EC  System.Int32 ApplyDeviceMapping(System.Int32 originalDeviceId)
END_CLASS

CLASS: DeviceInfo
TYPE:  sealed struct
TOKEN: 0x20001C4
SIZE:  0x30
FIELDS:
  private           System.Int32                    m_DeviceId  // 0x10
  private           System.String                   m_Layout  // 0x18
  private           UnityEngine.InputSystem.Utilities.FourCCm_StateFormat  // 0x20
  private           System.Int32                    m_StateSizeInBytes  // 0x24
  private           System.String                   m_FullLayoutJson  // 0x28
PROPERTIES:
  deviceId  get=0x02B77620  set=0x04D86140
  layout  get=0x04D88320  set=0x067D5E9C
  stateFormat  get=0x02B2E2D0  set=0x014F51F0
  stateSizeInBytes  get=0x04D882A0  set=0x04D8F540
METHODS:
END_CLASS

CLASS: <stateData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001C6
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: UpdateStepCount
TYPE:  sealed struct
TOKEN: 0x20001CF
SIZE:  0x18
FIELDS:
  private           System.Boolean                  m_WasUpdated  // 0x10
  private           System.UInt32                   <value>k__BackingField  // 0x14
PROPERTIES:
  value  get=0x04D88B20  set=0x04D88B50
METHODS:
  RVA=0x04DACFE0  token=0x600114C  System.Void OnBeforeUpdate()
  RVA=0x030CE770  token=0x600114D  System.Void OnUpdate()
END_CLASS

CLASS: SerializedState
TYPE:  sealed struct
TOKEN: 0x20001D0
SIZE:  0x1C
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.InputUpdateTypelastUpdateType  // 0x10
  public            UnityEngine.InputSystem.LowLevel.InputUpdate.UpdateStepCountplayerUpdateStepCount  // 0x14
METHODS:
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x20001D2
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.InputUpdateDelegatevalue  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600116F  System.Void .ctor()
  RVA=0x030CE920  token=0x6001170  System.Void <set_onUpdate>b__0(UnityEngineInternal.Input.NativeInputUpdateType updateType, UnityEngineInternal.Input.NativeInputEventBuffer* eventBufferPtr)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x20001D3
SIZE:  0x18
FIELDS:
  public            System.Action<UnityEngine.InputSystem.LowLevel.InputUpdateType>value  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001171  System.Void .ctor()
  RVA=0x03EF9F40  token=0x6001172  System.Void <set_onBeforeUpdate>b__0(UnityEngineInternal.Input.NativeInputUpdateType updateType)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x20001D4
SIZE:  0x18
FIELDS:
  public            System.Func<UnityEngine.InputSystem.LowLevel.InputUpdateType,System.Boolean>value  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001173  System.Void .ctor()
  RVA=0x03DF4990  token=0x6001174  System.Boolean <set_onShouldRunUpdate>b__0(UnityEngineInternal.Input.NativeInputUpdateType updateType)
END_CLASS

CLASS: StateChangeMonitorDelegate
TYPE:  class
TOKEN: 0x20001D9
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
FIELDS:
  public            System.Action<UnityEngine.InputSystem.InputControl,System.Double,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Int64>valueChangeCallback  // 0x10
  public            System.Action<UnityEngine.InputSystem.InputControl,System.Double,System.Int64,System.Int32>timerExpiredCallback  // 0x18
METHODS:
  RVA=0x0B249488  token=0x6001189  System.Void NotifyControlStateChanged(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 monitorIndex)
  RVA=0x0B2494B0  token=0x600118A  System.Void NotifyTimerExpired(UnityEngine.InputSystem.InputControl control, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
  RVA=0x041E1670  token=0x600118B  System.Void .ctor()
END_CLASS

CLASS: DoubleBuffers
TYPE:  sealed struct
TOKEN: 0x20001DC
SIZE:  0x20
FIELDS:
  public            System.Void**                   deviceToBufferMapping  // 0x10
  public            System.Int32                    deviceCount  // 0x18
PROPERTIES:
  valid  get=0x030CF830
METHODS:
  RVA=0x030CF7F0  token=0x60011B0  System.Void SetFrontBuffer(System.Int32 deviceIndex, System.Void* ptr)
  RVA=0x030CF810  token=0x60011B1  System.Void SetBackBuffer(System.Int32 deviceIndex, System.Void* ptr)
  RVA=0x030D1280  token=0x60011B2  System.Void* GetFrontBuffer(System.Int32 deviceIndex)
  RVA=0x030CF840  token=0x60011B3  System.Void* GetBackBuffer(System.Int32 deviceIndex)
  RVA=0x030CF860  token=0x60011B4  System.Void SwapBuffers(System.Int32 deviceIndex)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x20001DE
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistorym_History  // 0x10
  private           System.Int32                    m_Index  // 0x18
PROPERTIES:
  Current  get=0x0B23CE80
  System.Collections.IEnumerator.Current  get=0x0B23CDF8
METHODS:
  RVA=0x0802AFB8  token=0x60011DE  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputStateHistory history)
  RVA=0x0808E204  token=0x60011DF  System.Boolean MoveNext()
  RVA=0x04D94F70  token=0x60011E0  System.Void Reset()
  RVA=0x041E1670  token=0x60011E3  System.Void Dispose()
END_CLASS

CLASS: <m_StateWithoutControlIndex>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001E0
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <m_StateWithControlIndex>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20001E1
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: RecordHeader
TYPE:  sealed struct
TOKEN: 0x20001DF
SIZE:  0x28
FIELDS:
  public            System.Double                   time  // 0x10
  public            System.UInt32                   version  // 0x18
  public            System.Int32                    controlIndex  // 0x1c
  private           UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader.<m_StateWithoutControlIndex>e__FixedBufferm_StateWithoutControlIndex  // 0x1c
  private           UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader.<m_StateWithControlIndex>e__FixedBufferm_StateWithControlIndex  // 0x20
  public    static  System.Int32                    kSizeWithControlIndex  // const
  public    static  System.Int32                    kSizeWithoutControlIndex  // const
PROPERTIES:
  statePtrWithControlIndex  get=0x04DACF00
  statePtrWithoutControlIndex  get=0x04DA51C0
METHODS:
END_CLASS

CLASS: Record
TYPE:  sealed struct
TOKEN: 0x20001E2
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistorym_Owner  // 0x10
  private   readonly System.Int32                    m_IndexPlusOne  // 0x18
  private           System.UInt32                   m_Version  // 0x1c
PROPERTIES:
  header  get=0x0B247D44
  recordIndex  get=0x04D9E120
  version  get=0x04D8CAD0
  valid  get=0x0B247F48
  owner  get=0x02B76770
  index  get=0x0B247D6C
  time  get=0x0B247F18
  control  get=0x0B247C98
  next  get=0x0B247D9C
  previous  get=0x0B247E5C
METHODS:
  RVA=0x0907C3C0  token=0x60011F0  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputStateHistory owner, System.Int32 index, UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header)
  RVA=-1  // generic def  token=0x60011F1  TValue ReadValue()
  RVA=0x0B247B90  token=0x60011F2  System.Object ReadValueAsObject()
  RVA=0x0B247B70  token=0x60011F3  System.Void* GetUnsafeMemoryPtr()
  RVA=0x0B247AF0  token=0x60011F4  System.Void* GetUnsafeMemoryPtrUnchecked()
  RVA=0x0B247AD0  token=0x60011F5  System.Void* GetUnsafeExtraMemoryPtr()
  RVA=0x0B247A24  token=0x60011F6  System.Void* GetUnsafeExtraMemoryPtrUnchecked()
  RVA=0x0B2475A8  token=0x60011F7  System.Void CopyFrom(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
  RVA=0x0B2474D0  token=0x60011F8  System.Void CheckValid()
  RVA=0x08CC0AF0  token=0x60011F9  System.Boolean Equals(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record other)
  RVA=0x0B2479B4  token=0x60011FA  System.Boolean Equals(System.Object obj)
  RVA=0x0907BF10  token=0x60011FB  System.Int32 GetHashCode()
  RVA=0x0B247BD4  token=0x60011FC  System.String ToString()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x20001E4
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>m_History  // 0x0
  private           System.Int32                    m_Index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001207  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> history)
  RVA=-1  // not resolved  token=0x6001208  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6001209  System.Void Reset()
  RVA=-1  // not resolved  token=0x600120C  System.Void Dispose()
END_CLASS

CLASS: Record
TYPE:  sealed struct
TOKEN: 0x20001E5
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>m_Owner  // 0x0
  private   readonly System.Int32                    m_IndexPlusOne  // 0x0
  private           System.UInt32                   m_Version  // 0x0
PROPERTIES:
  header  get=-1  // not resolved
  recordIndex  get=-1  // not resolved
  valid  get=-1  // not resolved
  owner  get=-1  // not resolved
  index  get=-1  // not resolved
  time  get=-1  // not resolved
  control  get=-1  // not resolved
  next  get=-1  // not resolved
  previous  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001216  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> owner, System.Int32 index, UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header)
  RVA=-1  // not resolved  token=0x6001217  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> owner, System.Int32 index)
  RVA=-1  // not resolved  token=0x6001218  TValue ReadValue()
  RVA=-1  // not resolved  token=0x6001219  System.Void* GetUnsafeMemoryPtr()
  RVA=-1  // not resolved  token=0x600121A  System.Void* GetUnsafeMemoryPtrUnchecked()
  RVA=-1  // not resolved  token=0x600121B  System.Void* GetUnsafeExtraMemoryPtr()
  RVA=-1  // not resolved  token=0x600121C  System.Void* GetUnsafeExtraMemoryPtrUnchecked()
  RVA=-1  // not resolved  token=0x600121D  System.Void CopyFrom(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue> record)
  RVA=-1  // not resolved  token=0x600121E  System.Void CheckValid()
  RVA=-1  // not resolved  token=0x600121F  System.Boolean Equals(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue> other)
  RVA=-1  // not resolved  token=0x6001220  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6001221  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6001222  System.String ToString()
END_CLASS

CLASS: Select
TYPE:  class
TOKEN: 0x20001E7
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           UnityEngine.InputSystem.LowLevel.SelectObservable<TSource,TResult>m_Observable  // 0x0
  private   readonly System.IObserver<TResult>       m_Observer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001225  System.Void .ctor(UnityEngine.InputSystem.LowLevel.SelectObservable<TSource,TResult> observable, System.IObserver<TResult> observer)
  RVA=-1  // not resolved  token=0x6001226  System.Void OnCompleted()
  RVA=-1  // not resolved  token=0x6001227  System.Void OnError(System.Exception error)
  RVA=-1  // not resolved  token=0x6001228  System.Void OnNext(TSource evt)
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x20001FA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.FlagsisModifyingExistingControl  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.FlagsIsNoisy  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.FlagsIsSynthetic  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.FlagsIsFirstDefinedInThisLayout  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.FlagsDontReset  // const
METHODS:
END_CLASS

CLASS: ControlItem
TYPE:  sealed struct
TOKEN: 0x20001F9
SIZE:  0xE0
FIELDS:
  private           UnityEngine.InputSystem.Utilities.InternedString<name>k__BackingField  // 0x10
  private           UnityEngine.InputSystem.Utilities.InternedString<layout>k__BackingField  // 0x20
  private           UnityEngine.InputSystem.Utilities.InternedString<variants>k__BackingField  // 0x30
  private           System.String                   <useStateFrom>k__BackingField  // 0x40
  private           System.String                   <displayName>k__BackingField  // 0x48
  private           System.String                   <shortDisplayName>k__BackingField  // 0x50
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.InternedString><usages>k__BackingField  // 0x58
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.InternedString><aliases>k__BackingField  // 0x68
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.NamedValue><parameters>k__BackingField  // 0x78
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.NameAndParameters><processors>k__BackingField  // 0x88
  private           System.UInt32                   <offset>k__BackingField  // 0x98
  private           System.UInt32                   <bit>k__BackingField  // 0x9c
  private           System.UInt32                   <sizeInBits>k__BackingField  // 0xa0
  private           UnityEngine.InputSystem.Utilities.FourCC<format>k__BackingField  // 0xa4
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags<flags>k__BackingField  // 0xa8
  private           System.Int32                    <arraySize>k__BackingField  // 0xac
  private           UnityEngine.InputSystem.Utilities.PrimitiveValue<defaultState>k__BackingField  // 0xb0
  private           UnityEngine.InputSystem.Utilities.PrimitiveValue<minValue>k__BackingField  // 0xc0
  private           UnityEngine.InputSystem.Utilities.PrimitiveValue<maxValue>k__BackingField  // 0xd0
PROPERTIES:
  name  get=0x02B77630  set=0x06D4B144
  layout  get=0x02B2D510  set=0x032A8A90
  variants  get=0x04D8C200  set=0x06E7AA80
  useStateFrom  get=0x011F36E0  set=0x05396200
  displayName  get=0x04D85A50  set=0x046A91B0
  shortDisplayName  get=0x04D85A60  set=0x042B4AE0
  usages  get=0x04D8FD50  set=0x06D94C5C
  aliases  get=0x04D87E00  set=0x06E77204
  parameters  get=0x04DA63C0  set=0x0B23CC78
  processors  get=0x04D8E8F0  set=0x06C5932C
  offset  get=0x04D86890  set=0x04D868C0
  bit  get=0x04D90C40  set=0x04D90C60
  sizeInBits  get=0x04D86B60  set=0x04D86BC0
  format  get=0x04D86B20  set=0x04D86B80
  flags  get=0x04D862D0  set=0x04D86300
  arraySize  get=0x04D86C60  set=0x04D86C80
  defaultState  get=0x04DA1260  set=0x04DA1300
  minValue  get=0x04DA1230  set=0x04DA12B0
  maxValue  get=0x04DA1220  set=0x04DA12A0
  isModifyingExistingControl  get=0x0B23CBB4  set=0x0B23CC18
  isNoisy  get=0x0B23CBC0  set=0x0B23CC38
  isSynthetic  get=0x0B23CBCC  set=0x0B23CC58
  dontReset  get=0x0B23CB90  set=0x0B23CBD8
  isFirstDefinedInThisLayout  get=0x0B23CBA8  set=0x0B23CBF8
  isArray  get=0x0B23CB9C
METHODS:
  RVA=0x0B23C820  token=0x60012F0  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem Merge(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem other)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001FD
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__14_0  // static @ 0x8
METHODS:
  RVA=0x0B249E8C  token=0x600131D  System.Void .cctor()
  RVA=0x041E1670  token=0x600131E  System.Void .ctor()
  RVA=0x0B1C2520  token=0x600131F  UnityEngine.InputSystem.Utilities.InternedString <WithUsages>b__14_0(System.String x)
END_CLASS

CLASS: ControlBuilder
TYPE:  sealed struct
TOKEN: 0x20001FC
SIZE:  0x20
FIELDS:
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.Builderbuilder  // 0x10
  private           System.Int32                    index  // 0x18
METHODS:
  RVA=0x0B23B144  token=0x600130A  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithDisplayName(System.String displayName)
  RVA=0x0B23B250  token=0x600130B  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithLayout(System.String layout)
  RVA=0x0B23B1F8  token=0x600130C  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithFormat(UnityEngine.InputSystem.Utilities.FourCC format)
  RVA=0x0B23B1A4  token=0x600130D  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithFormat(System.String format)
  RVA=0x0B23B08C  token=0x600130E  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithByteOffset(System.UInt32 offset)
  RVA=0x0B23B034  token=0x600130F  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithBitOffset(System.UInt32 bit)
  RVA=0x0B23AF54  token=0x6001310  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder IsSynthetic(System.Boolean value)
  RVA=0x0B23AEE8  token=0x6001311  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder IsNoisy(System.Boolean value)
  RVA=0x0B23AE7C  token=0x6001312  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder DontReset(System.Boolean value)
  RVA=0x0B23B5CC  token=0x6001313  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithSizeInBits(System.UInt32 sizeInBits)
  RVA=0x0B23B50C  token=0x6001314  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithRange(System.Single minValue, System.Single maxValue)
  RVA=0x0B23B624  token=0x6001315  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(UnityEngine.InputSystem.Utilities.InternedString[] usages)
  RVA=0x0B23B800  token=0x6001316  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(System.Collections.Generic.IEnumerable<System.String> usages)
  RVA=0x0B23B958  token=0x6001317  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(System.String[] usages)
  RVA=0x0B23B358  token=0x6001318  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithParameters(System.String parameters)
  RVA=0x0B23B424  token=0x6001319  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithProcessors(System.String processors)
  RVA=0x0B23B0E4  token=0x600131A  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithDefaultState(UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x0B23AFC0  token=0x600131B  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder UsingStateFrom(System.String path)
  RVA=0x0B23AE24  token=0x600131C  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(System.Int32 arraySize)
END_CLASS

CLASS: Builder
TYPE:  class
TOKEN: 0x20001FB
SIZE:  0x48
FIELDS:
  private           System.String                   <name>k__BackingField  // 0x10
  private           System.String                   <displayName>k__BackingField  // 0x18
  private           System.Type                     <type>k__BackingField  // 0x20
  private           UnityEngine.InputSystem.Utilities.FourCC<stateFormat>k__BackingField  // 0x28
  private           System.Int32                    <stateSizeInBytes>k__BackingField  // 0x2c
  private           System.String                   m_ExtendsLayout  // 0x30
  private           System.Nullable<System.Boolean> <updateBeforeRender>k__BackingField  // 0x38
  private           System.Int32                    m_ControlCount  // 0x3c
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[]m_Controls  // 0x40
PROPERTIES:
  name  get=0x02B2ECC0  set=0x053908C0
  displayName  get=0x0385B100  set=0x05392C40
  type  get=0x04D862C0  set=0x05390F40
  stateFormat  get=0x011F2A90  set=0x011F2AA0
  stateSizeInBytes  get=0x04D86710  set=0x04D86720
  extendsLayout  get=0x011F36E0  set=0x0B239E0C
  updateBeforeRender  get=0x04DACEA0  set=0x04DA8B30
  controls  get=0x0B239DAC
METHODS:
  RVA=0x0B23997C  token=0x6001300  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder AddControl(System.String name)
  RVA=0x069698F4  token=0x6001301  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithName(System.String name)
  RVA=0x06969910  token=0x6001302  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithDisplayName(System.String displayName)
  RVA=-1  // generic def  token=0x6001303  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithType()
  RVA=0x04DACE80  token=0x6001304  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithFormat(UnityEngine.InputSystem.Utilities.FourCC format)
  RVA=0x0B239D7C  token=0x6001305  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithFormat(System.String format)
  RVA=0x04DACE90  token=0x6001306  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithSizeInBytes(System.Int32 sizeInBytes)
  RVA=0x0B239D60  token=0x6001307  UnityEngine.InputSystem.Layouts.InputControlLayout.Builder Extend(System.String baseLayoutName)
  RVA=0x0B239B58  token=0x6001308  UnityEngine.InputSystem.Layouts.InputControlLayout Build()
  RVA=0x041E1670  token=0x6001309  System.Void .ctor()
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x20001FE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsIsGenericTypeOfDevice  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsHideInUI  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsIsOverride  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsCanRunInBackground  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsCanRunInBackgroundIsSet  // const
  public    static  UnityEngine.InputSystem.Layouts.InputControlLayout.FlagsIsNoisy  // const
METHODS:
END_CLASS

CLASS: LayoutJsonNameAndDescriptorOnly
TYPE:  sealed struct
TOKEN: 0x20001FF
SIZE:  0x88
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   extend  // 0x18
  public            System.String[]                 extendMultiple  // 0x20
  public            UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJsondevice  // 0x28
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000201
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__14_0  // static @ 0x8
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__15_0  // static @ 0x10
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__15_1  // static @ 0x18
METHODS:
  RVA=0x0B249E28  token=0x6001322  System.Void .cctor()
  RVA=0x041E1670  token=0x6001323  System.Void .ctor()
  RVA=0x0B1C2520  token=0x6001324  UnityEngine.InputSystem.Utilities.InternedString <ToLayout>b__14_0(System.String x)
  RVA=0x0B1C93D0  token=0x6001325  System.String <FromLayout>b__15_0(UnityEngine.InputSystem.Utilities.InternedString x)
  RVA=0x0B1C93D0  token=0x6001326  System.String <FromLayout>b__15_1(UnityEngine.InputSystem.Utilities.InternedString x)
END_CLASS

CLASS: LayoutJson
TYPE:  sealed struct
TOKEN: 0x2000200
SIZE:  0x78
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   extend  // 0x18
  public            System.String[]                 extendMultiple  // 0x20
  public            System.String                   format  // 0x28
  public            System.String                   beforeRender  // 0x30
  public            System.String                   runInBackground  // 0x38
  public            System.String[]                 commonUsages  // 0x40
  public            System.String                   displayName  // 0x48
  public            System.String                   description  // 0x50
  public            System.String                   type  // 0x58
  public            System.String                   variant  // 0x60
  public            System.Boolean                  isGenericTypeOfDevice  // 0x68
  public            System.Boolean                  hideInUI  // 0x69
  public            UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[]controls  // 0x70
METHODS:
  RVA=0x0B2466BC  token=0x6001320  UnityEngine.InputSystem.Layouts.InputControlLayout ToLayout()
  RVA=0x0B246300  token=0x6001321  UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson FromLayout(UnityEngine.InputSystem.Layouts.InputControlLayout layout)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000203
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__24_0  // static @ 0x8
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__24_1  // static @ 0x10
  public    static  System.Func<UnityEngine.InputSystem.Utilities.NamedValue,System.String><>9__25_0  // static @ 0x18
  public    static  System.Func<UnityEngine.InputSystem.Utilities.NameAndParameters,System.String><>9__25_1  // static @ 0x20
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__25_2  // static @ 0x28
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__25_3  // static @ 0x30
METHODS:
  RVA=0x0B249DC4  token=0x600132A  System.Void .cctor()
  RVA=0x041E1670  token=0x600132B  System.Void .ctor()
  RVA=0x0B1C2520  token=0x600132C  UnityEngine.InputSystem.Utilities.InternedString <ToLayout>b__24_0(System.String x)
  RVA=0x0B1C2520  token=0x600132D  UnityEngine.InputSystem.Utilities.InternedString <ToLayout>b__24_1(System.String x)
  RVA=0x0B1C93BC  token=0x600132E  System.String <FromControlItems>b__25_0(UnityEngine.InputSystem.Utilities.NamedValue x)
  RVA=0x0B249D6C  token=0x600132F  System.String <FromControlItems>b__25_1(UnityEngine.InputSystem.Utilities.NameAndParameters x)
  RVA=0x0B1C93D0  token=0x6001330  System.String <FromControlItems>b__25_2(UnityEngine.InputSystem.Utilities.InternedString x)
  RVA=0x0B1C93D0  token=0x6001331  System.String <FromControlItems>b__25_3(UnityEngine.InputSystem.Utilities.InternedString x)
END_CLASS

CLASS: ControlItemJson
TYPE:  class
TOKEN: 0x2000202
SIZE:  0xB0
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   layout  // 0x18
  public            System.String                   variants  // 0x20
  public            System.String                   usage  // 0x28
  public            System.String                   alias  // 0x30
  public            System.String                   useStateFrom  // 0x38
  public            System.UInt32                   offset  // 0x40
  public            System.UInt32                   bit  // 0x44
  public            System.UInt32                   sizeInBits  // 0x48
  public            System.String                   format  // 0x50
  public            System.Int32                    arraySize  // 0x58
  public            System.String[]                 usages  // 0x60
  public            System.String[]                 aliases  // 0x68
  public            System.String                   parameters  // 0x70
  public            System.String                   processors  // 0x78
  public            System.String                   displayName  // 0x80
  public            System.String                   shortDisplayName  // 0x88
  public            System.Boolean                  noisy  // 0x90
  public            System.Boolean                  dontReset  // 0x91
  public            System.Boolean                  synthetic  // 0x92
  public            System.String                   defaultState  // 0x98
  public            System.String                   minValue  // 0xa0
  public            System.String                   maxValue  // 0xa8
METHODS:
  RVA=0x04DACEB0  token=0x6001327  System.Void .ctor()
  RVA=0x0B23C148  token=0x6001328  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem ToLayout()
  RVA=0x0B23B980  token=0x6001329  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[] FromControlItems(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] items)
END_CLASS

CLASS: LayoutMatcher
TYPE:  sealed struct
TOKEN: 0x2000205
SIZE:  0x28
FIELDS:
  public            UnityEngine.InputSystem.Utilities.InternedStringlayoutName  // 0x10
  public            UnityEngine.InputSystem.Layouts.InputDeviceMatcherdeviceMatcher  // 0x20
METHODS:
END_CLASS

CLASS: PrecompiledLayout
TYPE:  sealed struct
TOKEN: 0x2000206
SIZE:  0x20
FIELDS:
  public            System.Func<UnityEngine.InputSystem.InputDevice>factoryMethod  // 0x10
  public            System.String                   metadata  // 0x18
METHODS:
END_CLASS

CLASS: <GetBaseLayouts>d__24
TYPE:  sealed class
TOKEN: 0x2000207
SIZE:  0xD0
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.Utilities.InternedString<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x28
  private           System.Boolean                  includeSelf  // 0x2c
  public            System.Boolean                  <>3__includeSelf  // 0x2d
  private           UnityEngine.InputSystem.Utilities.InternedStringlayout  // 0x30
  public            UnityEngine.InputSystem.Utilities.InternedString<>3__layout  // 0x40
  public            UnityEngine.InputSystem.Layouts.InputControlLayout.Collection<>4__this  // 0x50
  public            UnityEngine.InputSystem.Layouts.InputControlLayout.Collection<>3__<>4__this  // 0x90
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.Utilities.InternedString>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0B1C24D8
METHODS:
  RVA=0x06C07014  token=0x6001342  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6001343  System.Void System.IDisposable.Dispose()
  RVA=0x0B1C22FC  token=0x6001344  System.Boolean MoveNext()
  RVA=0x0B1C248C  token=0x6001346  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1C23B0  token=0x6001348  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.Utilities.InternedString> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.Utilities.InternedString>.GetEnumerator()
  RVA=0x0B1C2484  token=0x6001349  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Collection
TYPE:  sealed struct
TOKEN: 0x2000204
SIZE:  0x50
FIELDS:
  public    static  System.Single                   kBaseScoreForNonGeneratedLayouts  // const
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,System.Type>layoutTypes  // 0x10
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,System.String>layoutStrings  // 0x18
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,System.Func<UnityEngine.InputSystem.Layouts.InputControlLayout>>layoutBuilders  // 0x20
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,UnityEngine.InputSystem.Utilities.InternedString>baseLayoutTable  // 0x28
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,UnityEngine.InputSystem.Utilities.InternedString[]>layoutOverrides  // 0x30
  public            System.Collections.Generic.HashSet<UnityEngine.InputSystem.Utilities.InternedString>layoutOverrideNames  // 0x38
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.PrecompiledLayout>precompiledLayouts  // 0x40
  public            System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.LayoutMatcher>layoutMatchers  // 0x48
METHODS:
  RVA=0x03DF51F0  token=0x6001332  System.Void Allocate()
  RVA=0x03AEDEB0  token=0x6001333  UnityEngine.InputSystem.Utilities.InternedString TryFindLayoutForType(System.Type layoutType)
  RVA=0x03AEC6C0  token=0x6001334  UnityEngine.InputSystem.Utilities.InternedString TryFindMatchingLayout(UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
  RVA=0x03AEF410  token=0x6001335  System.Boolean HasLayout(UnityEngine.InputSystem.Utilities.InternedString name)
  RVA=0x0B23A474  token=0x6001336  UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadLayoutInternal(UnityEngine.InputSystem.Utilities.InternedString name)
  RVA=0x0B23A660  token=0x6001337  UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadLayout(UnityEngine.InputSystem.Utilities.InternedString name, System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,UnityEngine.InputSystem.Layouts.InputControlLayout> table)
  RVA=0x0B23A1B0  token=0x6001338  UnityEngine.InputSystem.Utilities.InternedString GetBaseLayoutName(UnityEngine.InputSystem.Utilities.InternedString layoutName)
  RVA=0x0B23A300  token=0x6001339  UnityEngine.InputSystem.Utilities.InternedString GetRootLayoutName(UnityEngine.InputSystem.Utilities.InternedString layoutName)
  RVA=0x0B239F00  token=0x600133A  System.Boolean ComputeDistanceInInheritanceHierarchy(UnityEngine.InputSystem.Utilities.InternedString firstLayout, UnityEngine.InputSystem.Utilities.InternedString secondLayout, System.Int32& distance)
  RVA=0x0B239FDC  token=0x600133B  UnityEngine.InputSystem.Utilities.InternedString FindLayoutThatIntroducesControl(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.Layouts.InputControlLayout.Cache cache)
  RVA=0x03AE94D0  token=0x600133C  System.Type GetControlTypeForLayout(UnityEngine.InputSystem.Utilities.InternedString layoutName)
  RVA=0x0B23AA7C  token=0x600133D  System.Boolean ValueTypeIsAssignableFrom(UnityEngine.InputSystem.Utilities.InternedString layoutName, System.Type valueType)
  RVA=0x0B23A418  token=0x600133E  System.Boolean IsGeneratedLayout(UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x0B23A238  token=0x600133F  System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.Utilities.InternedString> GetBaseLayouts(UnityEngine.InputSystem.Utilities.InternedString layout, System.Boolean includeSelf)
  RVA=0x0B23A38C  token=0x6001340  System.Boolean IsBasedOn(UnityEngine.InputSystem.Utilities.InternedString parentLayout, UnityEngine.InputSystem.Utilities.InternedString childLayout)
  RVA=0x03AEC880  token=0x6001341  System.Void AddMatcher(UnityEngine.InputSystem.Utilities.InternedString layout, UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
END_CLASS

CLASS: LayoutNotFoundException
TYPE:  class
TOKEN: 0x2000208
SIZE:  0x98
EXTENDS: System.Exception
FIELDS:
  private   readonly System.String                   <layout>k__BackingField  // 0x90
PROPERTIES:
  layout  get=0x04D86220
METHODS:
  RVA=0x0B1C0550  token=0x600134B  System.Void .ctor()
  RVA=0x0B1C06A0  token=0x600134C  System.Void .ctor(System.String name, System.String message)
  RVA=0x0B1C059C  token=0x600134D  System.Void .ctor(System.String name)
  RVA=0x0B1C0640  token=0x600134E  System.Void .ctor(System.String message, System.Exception innerException)
  RVA=0x0B1C0710  token=0x600134F  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
END_CLASS

CLASS: Cache
TYPE:  sealed struct
TOKEN: 0x2000209
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,UnityEngine.InputSystem.Layouts.InputControlLayout>table  // 0x10
METHODS:
  RVA=0x05395148  token=0x6001350  System.Void Clear()
  RVA=0x0B1B6E58  token=0x6001351  UnityEngine.InputSystem.Layouts.InputControlLayout FindOrLoadLayout(System.String name, System.Boolean throwIfNotFound)
END_CLASS

CLASS: CacheRefInstance
TYPE:  sealed struct
TOKEN: 0x200020A
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.Boolean                  valid  // 0x10
METHODS:
  RVA=0x0B1B6DC4  token=0x6001352  System.Void Dispose()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200020B
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Layouts.InputControlLayout.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__52_0  // static @ 0x8
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__75_0  // static @ 0x10
  public    static  System.Func<System.String,UnityEngine.InputSystem.Utilities.InternedString><>9__75_1  // static @ 0x18
METHODS:
  RVA=0x0B1C2544  token=0x6001353  System.Void .cctor()
  RVA=0x041E1670  token=0x6001354  System.Void .ctor()
  RVA=0x0B1C2520  token=0x6001355  UnityEngine.InputSystem.Utilities.InternedString <FromType>b__52_0(System.String x)
  RVA=0x0B1C2520  token=0x6001356  UnityEngine.InputSystem.Utilities.InternedString <CreateControlItemFromMember>b__75_0(System.String x)
  RVA=0x0B1C2520  token=0x6001357  UnityEngine.InputSystem.Utilities.InternedString <CreateControlItemFromMember>b__75_1(System.String x)
END_CLASS

CLASS: RefInstance
TYPE:  sealed struct
TOKEN: 0x200020E
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x0B1C1640  token=0x600138C  System.Void Dispose()
END_CLASS

CLASS: DeviceDescriptionJson
TYPE:  sealed struct
TOKEN: 0x2000210
SIZE:  0x48
FIELDS:
  public            System.String                   interface  // 0x10
  public            System.String                   type  // 0x18
  public            System.String                   product  // 0x20
  public            System.String                   serial  // 0x28
  public            System.String                   version  // 0x30
  public            System.String                   manufacturer  // 0x38
  public            System.String                   capabilities  // 0x40
METHODS:
END_CLASS

CLASS: Capability
TYPE:  sealed struct
TOKEN: 0x2000213
SIZE:  0x20
FIELDS:
  public            System.String                   path  // 0x10
  public            System.String                   value  // 0x18
METHODS:
END_CLASS

CLASS: MatcherJson
TYPE:  sealed struct
TOKEN: 0x2000212
SIZE:  0x70
FIELDS:
  public            System.String                   interface  // 0x10
  public            System.String[]                 interfaces  // 0x18
  public            System.String                   deviceClass  // 0x20
  public            System.String[]                 deviceClasses  // 0x28
  public            System.String                   manufacturer  // 0x30
  public            System.String                   manufacturerContains  // 0x38
  public            System.String[]                 manufacturers  // 0x40
  public            System.String                   product  // 0x48
  public            System.String[]                 products  // 0x50
  public            System.String                   version  // 0x58
  public            System.String[]                 versions  // 0x60
  public            UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson.Capability[]capabilities  // 0x68
METHODS:
  RVA=0x0B1C077C  token=0x60013BB  UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson FromMatcher(UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
  RVA=0x03DF7830  token=0x60013BC  UnityEngine.InputSystem.Layouts.InputDeviceMatcher ToMatcher()
END_CLASS

CLASS: <get_patterns>d__4
TYPE:  sealed class
TOKEN: 0x2000214
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Collections.Generic.KeyValuePair<System.String,System.Object><>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x28
  public            UnityEngine.InputSystem.Layouts.InputDeviceMatcher<>4__this  // 0x30
  public            UnityEngine.InputSystem.Layouts.InputDeviceMatcher<>3__<>4__this  // 0x38
  private           System.Int32                    <count>5__2  // 0x40
  private           System.Int32                    <i>5__3  // 0x44
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0B1C27A0
METHODS:
  RVA=0x06C07014  token=0x60013BD  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60013BE  System.Void System.IDisposable.Dispose()
  RVA=0x0B1C25A8  token=0x60013BF  System.Boolean MoveNext()
  RVA=0x0B1C2754  token=0x60013C1  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1C26B8  token=0x60013C3  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
  RVA=0x0B1C274C  token=0x60013C4  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000215
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Layouts.InputDeviceMatcher.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Char,System.Boolean><>9__12_0  // static @ 0x8
METHODS:
  RVA=0x04D456D0  token=0x60013C5  System.Void .cctor()
  RVA=0x041E1670  token=0x60013C6  System.Void .ctor()
  RVA=0x044834A0  token=0x60013C7  System.Boolean <With>b__12_0(System.Char ch)
END_CLASS

CLASS: Clamp
TYPE:  sealed struct
TOKEN: 0x2000218
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Controls.AxisControl.ClampNone  // const
  public    static  UnityEngine.InputSystem.Controls.AxisControl.ClampBeforeNormalize  // const
  public    static  UnityEngine.InputSystem.Controls.AxisControl.ClampAfterNormalize  // const
  public    static  UnityEngine.InputSystem.Controls.AxisControl.ClampToConstantBeforeNormalize  // const
METHODS:
END_CLASS

CLASS: WriteMode
TYPE:  sealed struct
TOKEN: 0x200021C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Controls.DiscreteButtonControl.WriteModeWriteDisabled  // const
  public    static  UnityEngine.InputSystem.Controls.DiscreteButtonControl.WriteModeWriteNullAndMaxValue  // const
METHODS:
END_CLASS

CLASS: DpadAxisControl
TYPE:  class
TOKEN: 0x200021F
SIZE:  0x138
EXTENDS: UnityEngine.InputSystem.Controls.AxisControl
FIELDS:
  private           System.Int32                    <component>k__BackingField  // 0x130
PROPERTIES:
  component  get=0x04D8D520  set=0x04D8D530
METHODS:
  RVA=0x0B1B74AC  token=0x60013FF  System.Void FinishSetup()
  RVA=0x0B1B7530  token=0x6001400  System.Single ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1B75C4  token=0x6001401  System.Void .ctor()
END_CLASS

CLASS: ButtonBits
TYPE:  sealed struct
TOKEN: 0x2000220
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Controls.DpadControl.ButtonBitsUp  // const
  public    static  UnityEngine.InputSystem.Controls.DpadControl.ButtonBitsDown  // const
  public    static  UnityEngine.InputSystem.Controls.DpadControl.ButtonBitsLeft  // const
  public    static  UnityEngine.InputSystem.Controls.DpadControl.ButtonBitsRight  // const
METHODS:
END_CLASS

CLASS: TapPhase
TYPE:  sealed struct
TOKEN: 0x200022C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Interactions.MultiTapInteraction.TapPhaseNone  // const
  public    static  UnityEngine.InputSystem.Interactions.MultiTapInteraction.TapPhaseWaitingForNextRelease  // const
  public    static  UnityEngine.InputSystem.Interactions.MultiTapInteraction.TapPhaseWaitingForNextPress  // const
METHODS:
END_CLASS

CLASS: ActionEventPtr
TYPE:  sealed struct
TOKEN: 0x2000232
SIZE:  0x20
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           UnityEngine.InputSystem.LowLevel.ActionEvent*m_Ptr  // 0x18
PROPERTIES:
  action  get=0x0B1B639C
  phase  get=0x04DAB940
  control  get=0x0B1B63C8
  interaction  get=0x0B1B6420
  time  get=0x0B1B646C
  startTime  get=0x04DAB950
  duration  get=0x0B1B6400
  valueSizeInBytes  get=0x04DAB960
METHODS:
  RVA=0x0B1B5DE8  token=0x6001499  System.Object ReadValueAsObject()
  RVA=0x0B1B6004  token=0x600149A  System.Void ReadValue(System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // generic def  token=0x600149B  TValue ReadValue()
  RVA=0x0B1B6100  token=0x600149C  System.String ToString()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000233
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.Utilities.InputActionTracem_Trace  // 0x10
  private   readonly UnityEngine.InputSystem.LowLevel.ActionEvent*m_Buffer  // 0x18
  private   readonly System.Int32                    m_EventCount  // 0x20
  private           UnityEngine.InputSystem.LowLevel.ActionEvent*m_CurrentEvent  // 0x28
  private           System.Int32                    m_CurrentIndex  // 0x30
PROPERTIES:
  Current  get=0x0B1B7B90
  System.Collections.IEnumerator.Current  get=0x0B1B7AEC
METHODS:
  RVA=0x0B1B7B44  token=0x600149D  System.Void .ctor(UnityEngine.InputSystem.Utilities.InputActionTrace trace)
  RVA=0x0B1B7A9C  token=0x600149E  System.Boolean MoveNext()
  RVA=0x04DAB9B0  token=0x600149F  System.Void Reset()
  RVA=0x041E1670  token=0x60014A0  System.Void Dispose()
END_CLASS

CLASS: <>c__DisplayClass33_0`1
TYPE:  sealed class
TOKEN: 0x2000235
FIELDS:
  public            TValue                          secondValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60014D3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60014D4  System.Boolean <Merge>b__0(TValue x)
END_CLASS

CLASS: <>c__DisplayClass34_0`1
TYPE:  sealed class
TOKEN: 0x2000236
FIELDS:
  public            System.Collections.Generic.IEqualityComparer<TValue>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60014D5  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass34_1`1
TYPE:  sealed class
TOKEN: 0x2000237
FIELDS:
  public            TValue                          secondValue  // 0x0
  public            UnityEngine.InputSystem.Utilities.ArrayHelpers.<>c__DisplayClass34_0<TValue>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60014D6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60014D7  System.Boolean <Merge>b__0(TValue x)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000241
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  public            UnityEngine.InputSystem.Utilities.InlinedArray<TValue>array  // 0x0
  public            System.Int32                    index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001517  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6001518  System.Void Reset()
  RVA=-1  // not resolved  token=0x600151B  System.Void Dispose()
END_CLASS

CLASS: JsonValueType
TYPE:  sealed struct
TOKEN: 0x2000245
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeNone  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeBool  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeReal  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeInteger  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeString  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeArray  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeObject  // const
  public    static  UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypeAny  // const
METHODS:
END_CLASS

CLASS: JsonString
TYPE:  sealed struct
TOKEN: 0x2000246
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            UnityEngine.InputSystem.Utilities.Substringtext  // 0x10
  public            System.Boolean                  hasEscapes  // 0x20
METHODS:
  RVA=0x0B1BF8C0  token=0x6001543  System.String ToString()
  RVA=0x0B1BF668  token=0x6001544  System.Boolean Equals(UnityEngine.InputSystem.Utilities.JsonParser.JsonString other)
  RVA=0x0B1BF7E0  token=0x6001545  System.Boolean Equals(System.Object obj)
  RVA=0x0B1BF858  token=0x6001546  System.Int32 GetHashCode()
  RVA=0x0B1BF98C  token=0x6001547  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.JsonParser.JsonString left, UnityEngine.InputSystem.Utilities.JsonParser.JsonString right)
  RVA=0x0B1BF9F4  token=0x6001548  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.JsonParser.JsonString left, UnityEngine.InputSystem.Utilities.JsonParser.JsonString right)
  RVA=0x0B1BF9B8  token=0x6001549  UnityEngine.InputSystem.Utilities.JsonParser.JsonString op_Implicit(System.String str)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000248
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Utilities.JsonParser.JsonValue.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.JsonParser.JsonValue,System.String><>9__11_0  // static @ 0x8
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.String,UnityEngine.InputSystem.Utilities.JsonParser.JsonValue>,System.String><>9__11_1  // static @ 0x10
METHODS:
  RVA=0x0B1C94AC  token=0x600155C  System.Void .cctor()
  RVA=0x041E1670  token=0x600155D  System.Void .ctor()
  RVA=0x0B1C9304  token=0x600155E  System.String <ToString>b__11_0(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue x)
  RVA=0x0B1C9318  token=0x600155F  System.String <ToString>b__11_1(System.Collections.Generic.KeyValuePair<System.String,UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> pair)
END_CLASS

CLASS: JsonValue
TYPE:  sealed struct
TOKEN: 0x2000247
SIZE:  0x58
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            UnityEngine.InputSystem.Utilities.JsonParser.JsonValueTypetype  // 0x10
  public            System.Boolean                  boolValue  // 0x14
  public            System.Double                   realValue  // 0x18
  public            System.Int64                    integerValue  // 0x20
  public            UnityEngine.InputSystem.Utilities.JsonParser.JsonStringstringValue  // 0x28
  public            System.Collections.Generic.List<UnityEngine.InputSystem.Utilities.JsonParser.JsonValue>arrayValue  // 0x40
  public            System.Collections.Generic.Dictionary<System.String,UnityEngine.InputSystem.Utilities.JsonParser.JsonValue>objectValue  // 0x48
  public            System.Object                   anyValue  // 0x50
METHODS:
  RVA=0x0B1BFBD8  token=0x600154A  System.Boolean ToBoolean()
  RVA=0x0B1BFD00  token=0x600154B  System.Int64 ToInteger()
  RVA=0x0B1BFC64  token=0x600154C  System.Double ToDouble()
  RVA=0x0B1BFD8C  token=0x600154D  System.String ToString()
  RVA=0x04DABA30  token=0x600154E  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Boolean val)
  RVA=0x04DABA60  token=0x600154F  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Int64 val)
  RVA=0x04DABA90  token=0x6001550  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Double val)
  RVA=0x0B1C01F0  token=0x6001551  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.String str)
  RVA=0x0B1C02B4  token=0x6001552  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(UnityEngine.InputSystem.Utilities.JsonParser.JsonString str)
  RVA=0x0B1C01AC  token=0x6001553  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Collections.Generic.List<UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> array)
  RVA=0x0B1C0300  token=0x6001554  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Collections.Generic.Dictionary<System.String,UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> obj)
  RVA=0x0B1C0270  token=0x6001555  UnityEngine.InputSystem.Utilities.JsonParser.JsonValue op_Implicit(System.Enum val)
  RVA=0x04160A50  token=0x6001556  System.Boolean Equals(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue other)
  RVA=0x04160B20  token=0x6001557  System.Boolean Equals(System.Object obj, UnityEngine.InputSystem.Utilities.JsonParser.JsonValue value)
  RVA=0x0B1BFA20  token=0x6001558  System.Boolean Equals(System.Object obj)
  RVA=0x0B1BFAB8  token=0x6001559  System.Int32 GetHashCode()
  RVA=0x053950EC  token=0x600155A  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue left, UnityEngine.InputSystem.Utilities.JsonParser.JsonValue right)
  RVA=0x0B1C0344  token=0x600155B  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue left, UnityEngine.InputSystem.Utilities.JsonParser.JsonValue right)
END_CLASS

CLASS: BitRegion
TYPE:  sealed struct
TOKEN: 0x200024A
SIZE:  0x18
FIELDS:
  public            System.UInt32                   bitOffset  // 0x10
  public            System.UInt32                   sizeInBits  // 0x14
PROPERTIES:
  isEmpty  get=0x030D1700
METHODS:
  RVA=0x04D85C40  token=0x6001574  System.Void .ctor(System.UInt32 bitOffset, System.UInt32 sizeInBits)
  RVA=0x04DABAF0  token=0x6001575  System.Void .ctor(System.UInt32 byteOffset, System.UInt32 bitOffset, System.UInt32 sizeInBits)
  RVA=0x030D1680  token=0x6001576  UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion Overlap(UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion other)
END_CLASS

CLASS: <EveryNth>d__1`1
TYPE:  sealed class
TOKEN: 0x200024C
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TValue                          <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TValue>enumerable  // 0x0
  public            System.Collections.Generic.IEnumerable<TValue><>3__enumerable  // 0x0
  private           System.Int32                    start  // 0x0
  public            System.Int32                    <>3__start  // 0x0
  private           System.Int32                    n  // 0x0
  public            System.Int32                    <>3__n  // 0x0
  private           System.Int32                    <index>5__2  // 0x0
  private           System.Collections.Generic.IEnumerator<TValue><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TValue>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600157A  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600157B  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600157C  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600157D  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600157F  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6001581  System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6001582  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200024E
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Utilities.NameAndParameters.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.NamedValue,System.String><>9__8_0  // static @ 0x8
METHODS:
  RVA=0x0B1C93E4  token=0x600158D  System.Void .cctor()
  RVA=0x041E1670  token=0x600158E  System.Void .ctor()
  RVA=0x0B1C93BC  token=0x600158F  System.String <ToString>b__8_0(UnityEngine.InputSystem.Utilities.NamedValue x)
END_CLASS

CLASS: ForDevice
TYPE:  class
TOKEN: 0x2000252
SIZE:  0x28
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr>m_Observer  // 0x10
  private           UnityEngine.InputSystem.InputDevicem_Device  // 0x18
  private           System.Type                     m_DeviceType  // 0x20
METHODS:
  RVA=0x0B1C3790  token=0x60015AD  System.Void .ctor(System.Type deviceType, UnityEngine.InputSystem.InputDevice device, System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
  RVA=0x041E1670  token=0x60015AE  System.Void OnCompleted()
  RVA=0x0B1C3650  token=0x60015AF  System.Void OnError(System.Exception error)
  RVA=0x0B1C369C  token=0x60015B0  System.Void OnNext(UnityEngine.InputSystem.LowLevel.InputEventPtr value)
END_CLASS

CLASS: <>c__DisplayClass6_0`1
TYPE:  sealed class
TOKEN: 0x2000254
FIELDS:
  public            System.IDisposable              subscription  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015B9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60015BA  System.Void <CallOnce>b__0()
END_CLASS

CLASS: Select
TYPE:  class
TOKEN: 0x2000257
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource,TResult>m_Observable  // 0x0
  private   readonly System.IObserver<TResult>       m_Observer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015C1  System.Void .ctor(UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource,TResult> observable, System.IObserver<TResult> observer)
  RVA=-1  // not resolved  token=0x60015C2  System.Void OnCompleted()
  RVA=-1  // not resolved  token=0x60015C3  System.Void OnError(System.Exception error)
  RVA=-1  // not resolved  token=0x60015C4  System.Void OnNext(TSource evt)
END_CLASS

CLASS: Take
TYPE:  class
TOKEN: 0x2000259
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           System.IObserver<TValue>        m_Observer  // 0x0
  private           System.Int32                    m_Remaining  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015C7  System.Void .ctor(UnityEngine.InputSystem.Utilities.TakeNObservable<TValue> observable, System.IObserver<TValue> observer)
  RVA=-1  // not resolved  token=0x60015C8  System.Void OnCompleted()
  RVA=-1  // not resolved  token=0x60015C9  System.Void OnError(System.Exception error)
  RVA=-1  // not resolved  token=0x60015CA  System.Void OnNext(TValue evt)
END_CLASS

CLASS: Where
TYPE:  class
TOKEN: 0x200025B
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           UnityEngine.InputSystem.Utilities.WhereObservable<TValue>m_Observable  // 0x0
  private   readonly System.IObserver<TValue>        m_Observer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015CD  System.Void .ctor(UnityEngine.InputSystem.Utilities.WhereObservable<TValue> observable, System.IObserver<TValue> observer)
  RVA=-1  // not resolved  token=0x60015CE  System.Void OnCompleted()
  RVA=-1  // not resolved  token=0x60015CF  System.Void OnError(System.Exception error)
  RVA=-1  // not resolved  token=0x60015D0  System.Void OnNext(TValue evt)
END_CLASS

CLASS: Enumerator
TYPE:  class
TOKEN: 0x200025D
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    m_Index  // 0x0
  private           UnityEngine.InputSystem.Utilities.OneOrMore<TValue,TList>m_List  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60015D9  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60015DA  System.Void Reset()
  RVA=-1  // not resolved  token=0x60015DD  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60015DE  System.Void .ctor()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000261
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly TValue[]                        m_Array  // 0x0
  private   readonly System.Int32                    m_IndexStart  // 0x0
  private   readonly System.Int32                    m_IndexEnd  // 0x0
  private           System.Int32                    m_Index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001632  System.Void .ctor(TValue[] array, System.Int32 index, System.Int32 length)
  RVA=-1  // not resolved  token=0x6001633  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6001634  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6001635  System.Void Reset()
END_CLASS

CLASS: TypedRestore
TYPE:  sealed class
TOKEN: 0x2000265
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001641  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001642  System.Void Invoke(T& state)
  RVA=-1  // runtime  token=0x6001643  System.IAsyncResult BeginInvoke(T& state, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001644  System.Void EndInvoke(T& state, System.IAsyncResult result)
END_CLASS

CLASS: <Tokenize>d__8
TYPE:  sealed class
TOKEN: 0x2000268
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.InputSystem.Utilities.Substring<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x28
  private           System.String                   str  // 0x30
  public            System.String                   <>3__str  // 0x38
  private           System.Int32                    <length>5__2  // 0x40
  private           System.Int32                    <endPos>5__3  // 0x44
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.Utilities.Substring>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0B1C92BC
METHODS:
  RVA=0x06C07014  token=0x600165D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600165E  System.Void System.IDisposable.Dispose()
  RVA=0x0B1C8FB0  token=0x600165F  System.Boolean MoveNext()
  RVA=0x0B1C9270  token=0x6001661  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1C91D4  token=0x6001663  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.Utilities.Substring> System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.Utilities.Substring>.GetEnumerator()
  RVA=0x0B1C9268  token=0x6001664  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <Split>d__9
TYPE:  sealed class
TOKEN: 0x2000269
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.String                   <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.String                   str  // 0x28
  public            System.String                   <>3__str  // 0x30
  private           System.Func<System.Char,System.Boolean>predicate  // 0x38
  public            System.Func<System.Char,System.Boolean><>3__predicate  // 0x40
  private           System.Int32                    <length>5__2  // 0x48
  private           System.Int32                    <position>5__3  // 0x4c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.String>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6001665  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6001666  System.Void System.IDisposable.Dispose()
  RVA=0x0B1C8DB8  token=0x6001667  System.Boolean MoveNext()
  RVA=0x0B1C8F64  token=0x6001669  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B1C8EB8  token=0x600166B  System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
  RVA=0x0B1C8F5C  token=0x600166C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200026D
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Utilities.TypeTable.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.InputSystem.Utilities.InternedString,System.String><>9__2_0  // static @ 0x8
METHODS:
  RVA=0x0B1C9448  token=0x6001690  System.Void .cctor()
  RVA=0x041E1670  token=0x6001691  System.Void .ctor()
  RVA=0x0B1C93D0  token=0x6001692  System.String <get_names>b__2_0(UnityEngine.InputSystem.Utilities.InternedString x)
END_CLASS

CLASS: WhichSideWins
TYPE:  sealed struct
TOKEN: 0x200026F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWinsNeither  // const
  public    static  UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWinsPositive  // const
  public    static  UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWinsNegative  // const
METHODS:
END_CLASS

CLASS: ModifiersOrder
TYPE:  sealed struct
TOKEN: 0x2000271
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrderDefault  // const
  public    static  UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrderOrdered  // const
  public    static  UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrderUnordered  // const
METHODS:
END_CLASS

CLASS: ModifiersOrder
TYPE:  sealed struct
TOKEN: 0x2000273
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers.ModifiersOrderDefault  // const
  public    static  UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers.ModifiersOrderOrdered  // const
  public    static  UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers.ModifiersOrderUnordered  // const
METHODS:
END_CLASS

CLASS: ModifiersOrder
TYPE:  sealed struct
TOKEN: 0x2000275
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrderDefault  // const
  public    static  UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrderOrdered  // const
  public    static  UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrderUnordered  // const
METHODS:
END_CLASS

CLASS: ModifiersOrder
TYPE:  sealed struct
TOKEN: 0x2000277
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrderDefault  // const
  public    static  UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrderOrdered  // const
  public    static  UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrderUnordered  // const
METHODS:
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x2000279
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.Vector2Composite.ModeAnalog  // const
  public    static  UnityEngine.InputSystem.Composites.Vector2Composite.ModeDigitalNormalized  // const
  public    static  UnityEngine.InputSystem.Composites.Vector2Composite.ModeDigital  // const
METHODS:
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x200027B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Composites.Vector3Composite.ModeAnalog  // const
  public    static  UnityEngine.InputSystem.Composites.Vector3Composite.ModeDigitalNormalized  // const
  public    static  UnityEngine.InputSystem.Composites.Vector3Composite.ModeDigital  // const
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=20
TYPE:  sealed struct
TOKEN: 0x200027D
SIZE:  0x24
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=24
TYPE:  sealed struct
TOKEN: 0x200027E
SIZE:  0x28
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=84
TYPE:  sealed struct
TOKEN: 0x200027F
SIZE:  0x64
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=104
TYPE:  sealed struct
TOKEN: 0x2000280
SIZE:  0x78
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=262
TYPE:  sealed struct
TOKEN: 0x2000281
SIZE:  0x116
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=371
TYPE:  sealed struct
TOKEN: 0x2000282
SIZE:  0x183
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=508
TYPE:  sealed struct
TOKEN: 0x2000283
SIZE:  0x20C
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=976
TYPE:  sealed struct
TOKEN: 0x2000284
SIZE:  0x3E0
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=1036
TYPE:  sealed struct
TOKEN: 0x2000285
SIZE:  0x41C
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=1799
TYPE:  sealed struct
TOKEN: 0x2000286
SIZE:  0x717
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=3983
TYPE:  sealed struct
TOKEN: 0x2000287
SIZE:  0xF9F
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200027C
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=2621D0F855A98F398E261D1E4C657C33BD47F11A47FD9CF90F160D467BC8C7FEBE3  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=5081E9B4B805C18CBD7D7326E392008A12FBD20465C53E9C60AEBEA427165641632  // static @ 0x106
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=976286DD866DF46AD63DA3C6198BE2F9CDB96A5223055FDC3E0287B7CCA9F45AFB4  // static @ 0x302
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=207BD1B2B340030CA32C0F7873A8B4949D42124F6206A455BA2860B27C103E8261  // static @ 0x6d2
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=371BD3331923AE2D87F6296377CB80C86CE12BF445ED38D4485D28FACFEC06BFF5B  // static @ 0x6e6
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3983CB22D97D4BE9F7A4259E5E355F9F66F2C2D11559A9EA0634CFF23EAA93DA62AE  // static @ 0x859
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1036D4ADEDA8D4E42C7133AFF7B673F4893FEA7C6A101764B0179449453A7E7F516F  // static @ 0x17e8
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=84D9D05356900CBD90C107CDBD184BC94526EC3F9228772F900FC7C60E3FE82B5A  // static @ 0x1bf4
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1799E90C194F8229C7E8552701BC1D1F1BD2537F77555B9151B4B09FF9D1BA956FE6  // static @ 0x1c48
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24EEAB0A49E323B940CB01CCD3E80D47F1566617F04466CD5D6190ACDBC495CB2A  // static @ 0x234f
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=104FFCB0A1E90587D61767DBCE765CBEC8743DC4A67B865900B464ED065E4625BD5  // static @ 0x2367
METHODS:
END_CLASS

CLASS: Unity.XR.GoogleVr.DaydreamHMD
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x1E0
EXTENDS: UnityEngine.InputSystem.XR.XRHMD
FIELDS:
METHODS:
  RVA=0x053971D4  token=0x60000D5  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.GoogleVr.DaydreamController
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x208
EXTENDS: UnityEngine.InputSystem.XR.XRController
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<volumeUp>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.ButtonControl<recentered>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<volumeDown>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<recentering>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<app>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<home>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadClicked>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadTouched>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAcceleration>k__BackingField  // 0x200
PROPERTIES:
  touchpad  get=0x04D8D960  set=0x053971DC
  volumeUp  get=0x04D8DA90  set=0x05397240
  recentered  get=0x04D8DA80  set=0x06B8B42C
  volumeDown  get=0x04D8D790  set=0x06B8B0D0
  recentering  get=0x04D8D7B0  set=0x06B8B0F8
  app  get=0x04D87870  set=0x06B8B134
  home  get=0x04D8D930  set=0x06B8B2C4
  touchpadClicked  get=0x04D87930  set=0x06799DA0
  touchpadTouched  get=0x04D87940  set=0x06799DB4
  deviceVelocity  get=0x04D87920  set=0x06799D8C
  deviceAcceleration  get=0x04D8DA40  set=0x06B8B3DC
METHODS:
  RVA=0x0B198640  token=0x60000EC  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000ED  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.OculusHMD
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x248
EXTENDS: UnityEngine.InputSystem.XR.XRHMD
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<userPresence>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAcceleration>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularAcceleration>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyeAngularVelocity>k__BackingField  // 0x200
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyeAcceleration>k__BackingField  // 0x208
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyeAngularAcceleration>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyeAngularVelocity>k__BackingField  // 0x218
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyeAcceleration>k__BackingField  // 0x220
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyeAngularAcceleration>k__BackingField  // 0x228
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyeAngularVelocity>k__BackingField  // 0x230
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyeAcceleration>k__BackingField  // 0x238
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyeAngularAcceleration>k__BackingField  // 0x240
PROPERTIES:
  userPresence  get=0x04D8D930  set=0x06B8B2C4
  deviceAngularVelocity  get=0x04D87930  set=0x06799DA0
  deviceAcceleration  get=0x04D87940  set=0x06799DB4
  deviceAngularAcceleration  get=0x04D87920  set=0x06799D8C
  leftEyeAngularVelocity  get=0x04D8DA40  set=0x06B8B3DC
  leftEyeAcceleration  get=0x04D8D8C0  set=0x06B8B24C
  leftEyeAngularAcceleration  get=0x04D8D8B0  set=0x06B8B238
  rightEyeAngularVelocity  get=0x04D8D3D0  set=0x06B1D640
  rightEyeAcceleration  get=0x04D8D870  set=0x06B8B1FC
  rightEyeAngularAcceleration  get=0x04D8D440  set=0x06B22BCC
  centerEyeAngularVelocity  get=0x04D8DAE0  set=0x06B8B490
  centerEyeAcceleration  get=0x04D8DAD0  set=0x06B8B47C
  centerEyeAngularAcceleration  get=0x04D8DE80  set=0x06B8B8C8
METHODS:
  RVA=0x0B1A2A7C  token=0x6000087  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000088  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.OculusTouchController
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x238
EXTENDS: UnityEngine.InputSystem.XR.XRControllerWithRumble
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<thumbstick>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<primaryButton>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<secondaryButton>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<start>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<thumbstickClicked>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.ButtonControl<primaryTouched>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.ButtonControl<secondaryTouched>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.AxisControl<triggerTouched>k__BackingField  // 0x200
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x208
  private           UnityEngine.InputSystem.Controls.ButtonControl<thumbstickTouched>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x218
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x220
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAcceleration>k__BackingField  // 0x228
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularAcceleration>k__BackingField  // 0x230
PROPERTIES:
  thumbstick  get=0x04D8D960  set=0x053971DC
  trigger  get=0x04D8DA90  set=0x05397240
  grip  get=0x04D8DA80  set=0x06B8B42C
  primaryButton  get=0x04D8D790  set=0x06B8B0D0
  secondaryButton  get=0x04D8D7B0  set=0x06B8B0F8
  gripPressed  get=0x04D87870  set=0x06B8B134
  start  get=0x04D8D930  set=0x06B8B2C4
  thumbstickClicked  get=0x04D87930  set=0x06799DA0
  primaryTouched  get=0x04D87940  set=0x06799DB4
  secondaryTouched  get=0x04D87920  set=0x06799D8C
  triggerTouched  get=0x04D8DA40  set=0x06B8B3DC
  triggerPressed  get=0x04D8D8C0  set=0x06B8B24C
  thumbstickTouched  get=0x04D8D8B0  set=0x06B8B238
  deviceVelocity  get=0x04D8D3D0  set=0x06B1D640
  deviceAngularVelocity  get=0x04D8D870  set=0x06B8B1FC
  deviceAcceleration  get=0x04D8D440  set=0x06B22BCC
  deviceAngularAcceleration  get=0x04D8DAE0  set=0x06B8B490
METHODS:
  RVA=0x0B1A2E4C  token=0x60000AB  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000AC  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.OculusTrackingReference
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x1C0
EXTENDS: UnityEngine.InputSystem.TrackedDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.IntegerControl<trackingState>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<isTracked>k__BackingField  // 0x1b8
PROPERTIES:
  trackingState  get=0x04D8D960  set=0x053971DC
  isTracked  get=0x04D8DA90  set=0x05397240
METHODS:
  RVA=0x0B1A3230  token=0x60000B1  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000B2  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.OculusRemote
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x1A8
EXTENDS: UnityEngine.InputSystem.InputDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<back>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.ButtonControl<start>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x1a0
PROPERTIES:
  back  get=0x04D8DD50  set=0x0539722C
  start  get=0x04D8DED0  set=0x05397218
  touchpad  get=0x04D8DA10  set=0x05397204
METHODS:
  RVA=0x0B1A2D74  token=0x60000B9  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000BA  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.OculusHMDExtended
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x258
EXTENDS: Unity.XR.Oculus.Input.OculusHMD
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<back>k__BackingField  // 0x248
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x250
PROPERTIES:
  back  get=0x04D8D840  set=0x06B8B1C0
  touchpad  get=0x04D8DCE0  set=0x06B8B6E8
METHODS:
  RVA=0x0B1A29D0  token=0x60000BF  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000C0  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.Oculus.Input.GearVRTrackedController
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x1F8
EXTENDS: UnityEngine.InputSystem.XR.XRController
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.ButtonControl<back>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadClicked>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadTouched>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAcceleration>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularAcceleration>k__BackingField  // 0x1f0
PROPERTIES:
  touchpad  get=0x04D8D960  set=0x053971DC
  trigger  get=0x04D8DA90  set=0x05397240
  back  get=0x04D8DA80  set=0x06B8B42C
  triggerPressed  get=0x04D8D790  set=0x06B8B0D0
  touchpadClicked  get=0x04D8D7B0  set=0x06B8B0F8
  touchpadTouched  get=0x04D87870  set=0x06B8B134
  deviceAngularVelocity  get=0x04D8D930  set=0x06B8B2C4
  deviceAcceleration  get=0x04D87930  set=0x06799DA0
  deviceAngularAcceleration  get=0x04D87940  set=0x06799DB4
METHODS:
  RVA=0x0B198ADC  token=0x60000D3  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000D4  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.OpenVRHMD
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x220
EXTENDS: UnityEngine.InputSystem.XR.XRHMD
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyeVelocity>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyeAngularVelocity>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyeVelocity>k__BackingField  // 0x200
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyeAngularVelocity>k__BackingField  // 0x208
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyeVelocity>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyeAngularVelocity>k__BackingField  // 0x218
PROPERTIES:
  deviceVelocity  get=0x04D8D930  set=0x06B8B2C4
  deviceAngularVelocity  get=0x04D87930  set=0x06799DA0
  leftEyeVelocity  get=0x04D87940  set=0x06799DB4
  leftEyeAngularVelocity  get=0x04D87920  set=0x06799D8C
  rightEyeVelocity  get=0x04D8DA40  set=0x06B8B3DC
  rightEyeAngularVelocity  get=0x04D8D8C0  set=0x06B8B24C
  centerEyeVelocity  get=0x04D8D8B0  set=0x06B8B238
  centerEyeAngularVelocity  get=0x04D8D3D0  set=0x06B1D640
METHODS:
  RVA=0x0B1A3580  token=0x6000012  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.OpenVRControllerWMR
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x208
EXTENDS: UnityEngine.InputSystem.XR.XRController
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadClick>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadTouch>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<menu>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector2Control<joystick>k__BackingField  // 0x200
PROPERTIES:
  deviceVelocity  get=0x04D8D960  set=0x053971DC
  deviceAngularVelocity  get=0x04D8DA90  set=0x05397240
  touchpadClick  get=0x04D8DA80  set=0x06B8B42C
  touchpadTouch  get=0x04D8D790  set=0x06B8B0D0
  gripPressed  get=0x04D8D7B0  set=0x06B8B0F8
  triggerPressed  get=0x04D87870  set=0x06B8B134
  menu  get=0x04D8D930  set=0x06B8B2C4
  trigger  get=0x04D87930  set=0x06799DA0
  grip  get=0x04D87940  set=0x06799DB4
  touchpad  get=0x04D87920  set=0x06799D8C
  joystick  get=0x04D8DA40  set=0x06B8B3DC
METHODS:
  RVA=0x0B1A32DC  token=0x600002A  System.Void FinishSetup()
  RVA=0x053971D4  token=0x600002B  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.ViveWand
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x200
EXTENDS: UnityEngine.InputSystem.XR.XRControllerWithRumble
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.ButtonControl<primary>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<trackpadPressed>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<trackpadTouched>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.Vector2Control<trackpad>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1f8
PROPERTIES:
  grip  get=0x04D8D960  set=0x053971DC
  gripPressed  get=0x04D8DA90  set=0x05397240
  primary  get=0x04D8DA80  set=0x06B8B42C
  trackpadPressed  get=0x04D8D790  set=0x06B8B0D0
  trackpadTouched  get=0x04D8D7B0  set=0x06B8B0F8
  trackpad  get=0x04D87870  set=0x06B8B134
  trigger  get=0x04D8D930  set=0x06B8B2C4
  triggerPressed  get=0x04D87930  set=0x06799DA0
  deviceVelocity  get=0x04D87940  set=0x06799DB4
  deviceAngularVelocity  get=0x04D87920  set=0x06799D8C
METHODS:
  RVA=0x0B1A97A4  token=0x6000040  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000041  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.ViveLighthouse
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x1B0
EXTENDS: UnityEngine.InputSystem.TrackedDevice
FIELDS:
METHODS:
  RVA=0x053971D4  token=0x6000042  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.ViveTracker
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x1C0
EXTENDS: UnityEngine.InputSystem.TrackedDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x1b8
PROPERTIES:
  deviceVelocity  get=0x04D8D960  set=0x053971DC
  deviceAngularVelocity  get=0x04D8DA90  set=0x05397240
METHODS:
  RVA=0x0B1A9704  token=0x6000047  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000048  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.HandedViveTracker
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x1E8
EXTENDS: Unity.XR.OpenVR.ViveTracker
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<primary>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<trackpadPressed>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1e0
PROPERTIES:
  grip  get=0x04D8DA80  set=0x06B8B42C
  gripPressed  get=0x04D8D790  set=0x06B8B0D0
  primary  get=0x04D8D7B0  set=0x06B8B0F8
  trackpadPressed  get=0x04D87870  set=0x06B8B134
  triggerPressed  get=0x04D8D930  set=0x06B8B2C4
METHODS:
  RVA=0x0B198D18  token=0x6000053  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000054  System.Void .ctor()
END_CLASS

CLASS: Unity.XR.OpenVR.OpenVROculusTouchController
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x208
EXTENDS: UnityEngine.InputSystem.XR.XRControllerWithRumble
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<thumbstick>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<primaryButton>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<secondaryButton>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<thumbstickClicked>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.ButtonControl<thumbstickTouched>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x200
PROPERTIES:
  thumbstick  get=0x04D8D960  set=0x053971DC
  trigger  get=0x04D8DA90  set=0x05397240
  grip  get=0x04D8DA80  set=0x06B8B42C
  primaryButton  get=0x04D8D790  set=0x06B8B0D0
  secondaryButton  get=0x04D8D7B0  set=0x06B8B0F8
  gripPressed  get=0x04D87870  set=0x06B8B134
  triggerPressed  get=0x04D8D930  set=0x06B8B2C4
  thumbstickClicked  get=0x04D87930  set=0x06799DA0
  thumbstickTouched  get=0x04D87940  set=0x06799DB4
  deviceVelocity  get=0x04D87920  set=0x06799D8C
  deviceAngularVelocity  get=0x04D8DA40  set=0x06B8B3DC
METHODS:
  RVA=0x0B1A375C  token=0x600006B  System.Void FinishSetup()
  RVA=0x053971D4  token=0x600006C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.IInputActionCollection
TYPE:  interface
TOKEN: 0x2000015
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  bindingMask  get=-1  // abstract  set=-1  // abstract
  devices  get=-1  // abstract  set=-1  // abstract
  controlSchemes  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000125  System.Boolean Contains(UnityEngine.InputSystem.InputAction action)
  RVA=-1  // abstract  token=0x6000126  System.Void Enable()
  RVA=-1  // abstract  token=0x6000127  System.Void Disable()
END_CLASS

CLASS: UnityEngine.InputSystem.IInputActionCollection2
TYPE:  interface
TOKEN: 0x2000016
IMPLEMENTS: UnityEngine.InputSystem.IInputActionCollection System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  bindings  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000129  UnityEngine.InputSystem.InputAction FindAction(System.String actionNameOrId, System.Boolean throwIfNotFound)
  RVA=-1  // abstract  token=0x600012A  System.Int32 FindBinding(UnityEngine.InputSystem.InputBinding mask, UnityEngine.InputSystem.InputAction& action)
END_CLASS

CLASS: UnityEngine.InputSystem.IInputInteraction
TYPE:  interface
TOKEN: 0x2000017
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600012B  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=-1  // abstract  token=0x600012C  System.Void Reset()
END_CLASS

CLASS: UnityEngine.InputSystem.IInputInteraction`1
TYPE:  interface
TOKEN: 0x2000018
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputInteraction
TYPE:  static class
TOKEN: 0x2000019
SIZE:  0x10
FIELDS:
  public    static  UnityEngine.InputSystem.Utilities.TypeTables_Interactions  // static @ 0x0
METHODS:
  RVA=0x0B1A28FC  token=0x600012D  System.Type GetValueType(System.Type interactionType)
  RVA=0x0B1A2820  token=0x600012E  System.String GetDisplayName(System.String interaction)
  RVA=0x0B1A26D0  token=0x600012F  System.String GetDisplayName(System.Type interactionType)
END_CLASS

CLASS: UnityEngine.InputSystem.InputAction
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x1C0
IMPLEMENTS: System.ICloneable System.IDisposable
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputActionEnableProfilerMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputActionDisableProfilerMarker  // static @ 0x8
  private           System.String                   m_Name  // 0x10
  private           UnityEngine.InputSystem.InputActionTypem_Type  // 0x18
  private           System.String                   m_ExpectedControlType  // 0x20
  private           System.String                   m_Id  // 0x28
  private           System.String                   m_Processors  // 0x30
  private           System.String                   m_Interactions  // 0x38
  private           UnityEngine.InputSystem.InputBinding[]m_SingletonActionBindings  // 0x40
  private           UnityEngine.InputSystem.InputAction.ActionFlagsm_Flags  // 0x48
  private           System.Nullable<UnityEngine.InputSystem.InputBinding>m_BindingMask  // 0x50
  private           System.Int32                    m_BindingsStartIndex  // 0xb0
  private           System.Int32                    m_BindingsCount  // 0xb4
  private           System.Int32                    m_ControlStartIndex  // 0xb8
  private           System.Int32                    m_ControlCount  // 0xbc
  private           System.Int32                    m_ActionIndexInState  // 0xc0
  private           UnityEngine.InputSystem.InputActionMapm_ActionMap  // 0xc8
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>m_OnStarted  // 0xd0
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>m_OnCanceled  // 0x120
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>m_OnPerformed  // 0x170
PROPERTIES:
  name  get=0x02B2ECC0
  type  get=0x011EF5B0
  id  get=0x0B1A22A0
  idDontGenerate  get=0x03525C40
  expectedControlType  get=0x04D862C0  set=0x05390F40
  processors  get=0x011F36E0
  interactions  get=0x04D85A50
  actionMap  get=0x0B1A2148
  bindingMask  get=0x04DAB780  set=0x0B1A2400
  bindings  get=0x0B1A2250
  controls  get=0x03DFB3C0
  phase  get=0x03DFC600
  inProgress  get=0x0B1A22E0
  enabled  get=0x03DFC5E0
  triggered  get=0x0B1A2314
  activeControl  get=0x0B1A2170
  activeValueType  get=0x0B1A21E4
  wantsInitialStateCheck  get=0x03DFE570  set=0x0B1A26B4
  isSingletonAction  get=0x0B1A22F8
  currentState  get=0x03DFC630
EVENTS:
  started  add=add_started  remove=remove_started
  canceled  add=add_canceled  remove=remove_canceled
  performed  add=add_performed  remove=remove_performed
METHODS:
  RVA=0x03CBE7A0  token=0x600014B  System.Void .ctor()
  RVA=0x0B1A1F0C  token=0x600014C  System.Void .ctor(System.String name, UnityEngine.InputSystem.InputActionType type, System.String binding, System.String interactions, System.String processors, System.String expectedControlType)
  RVA=0x0B1A13BC  token=0x600014D  System.Void Dispose()
  RVA=0x0B1A1908  token=0x600014E  System.String ToString()
  RVA=0x03DFB810  token=0x600014F  System.Void Enable()
  RVA=0x0B1A12BC  token=0x6000150  System.Void Disable()
  RVA=0x0B1A10C4  token=0x6000151  UnityEngine.InputSystem.InputAction Clone()
  RVA=0x0B1A1900  token=0x6000152  System.Object System.ICloneable.Clone()
  RVA=-1  // generic def  token=0x6000153  TValue ReadValue()
  RVA=0x0B1A17E0  token=0x6000154  System.Object ReadValueAsObject()
  RVA=0x0B1A15E0  token=0x6000155  System.Single GetControlMagnitude()
  RVA=0x0B1A189C  token=0x6000156  System.Void Reset()
  RVA=0x0B1A1798  token=0x6000157  System.Boolean IsPressed()
  RVA=0x0B1A174C  token=0x6000158  System.Boolean IsInProgress()
  RVA=0x0B1A13DC  token=0x6000159  System.Int32 ExpectedFrame()
  RVA=0x0B1A1DB8  token=0x600015A  System.Boolean WasPressedThisFrame()
  RVA=0x0B1A1D5C  token=0x600015B  System.Boolean WasPressedThisDynamicUpdate()
  RVA=0x0B1A1E90  token=0x600015C  System.Boolean WasReleasedThisFrame()
  RVA=0x0B1A1E34  token=0x600015D  System.Boolean WasReleasedThisDynamicUpdate()
  RVA=0x0B1A1CE0  token=0x600015E  System.Boolean WasPerformedThisFrame()
  RVA=0x0B1A1C84  token=0x600015F  System.Boolean WasPerformedThisDynamicUpdate()
  RVA=0x0B1A1C08  token=0x6000160  System.Boolean WasCompletedThisFrame()
  RVA=0x0B1A1BAC  token=0x6000161  System.Boolean WasCompletedThisDynamicUpdate()
  RVA=0x0B1A162C  token=0x6000162  System.Single GetTimeoutCompletionPercentage()
  RVA=0x035248B0  token=0x6000165  System.String MakeSureIdIsInPlace()
  RVA=0x0B1A159C  token=0x6000166  System.Void GenerateId()
  RVA=0x03DFC000  token=0x6000167  UnityEngine.InputSystem.InputActionMap GetOrCreateActionMap()
  RVA=0x0B1A11E8  token=0x6000168  System.Void CreateInternalActionMapForSingletonAction()
  RVA=0x0B1A1860  token=0x6000169  System.Void RequestInitialStateCheckOnEnabledAction()
  RVA=0x0B1A0DCC  token=0x600016A  System.Boolean ActiveControlIsValid(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1A1440  token=0x600016B  System.Nullable<UnityEngine.InputSystem.InputBinding> FindEffectiveBindingMask()
  RVA=0x0B1A0EAC  token=0x600016C  System.Int32 BindingIndexOnActionToBindingIndexOnMap(System.Int32 indexOfBindingOnAction)
  RVA=0x0B1A1024  token=0x600016D  System.Int32 BindingIndexOnMapToBindingIndexOnAction(System.Int32 indexOfBindingOnMap)
  RVA=0x04CEA0D0  token=0x600016E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionAsset
TYPE:  class
TOKEN: 0x200001D
SIZE:  0xB0
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: UnityEngine.InputSystem.IInputActionCollection2 UnityEngine.InputSystem.IInputActionCollection System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  public    static  System.String                   Extension  // const
  private   static  System.String                   kDefaultAssetLayoutJson  // const
  private           UnityEngine.InputSystem.InputActionMap[]m_ActionMaps  // 0x18
  private           UnityEngine.InputSystem.InputControlScheme[]m_ControlSchemes  // 0x20
  private           UnityEngine.InputSystem.InputActionStatem_SharedStateForAllMaps  // 0x28
  private           System.Nullable<UnityEngine.InputSystem.InputBinding>m_BindingMask  // 0x30
  private           System.Int32                    m_ParameterOverridesCount  // 0x90
  private           UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[]m_ParameterOverrides  // 0x98
  private           UnityEngine.InputSystem.InputActionMap.DeviceArraym_Devices  // 0xa0
PROPERTIES:
  enabled  get=0x0B19A2E4
  actionMaps  get=0x043E1320
  controlSchemes  get=0x0B19A290
  bindings  get=0x0B19A220
  bindingMask  get=0x04DAB700  set=0x0B19A414
  devices  get=0x05393388  set=0x0B19A5B8
  Item  get=0x0B19A18C
METHODS:
  RVA=0x0B19A0D4  token=0x600018D  System.String ToJson()
  RVA=0x0B199E40  token=0x600018E  System.Void LoadFromJson(System.String json)
  RVA=0x0B199B74  token=0x600018F  UnityEngine.InputSystem.InputActionAsset FromJson(System.String json)
  RVA=0x0B19950C  token=0x6000190  UnityEngine.InputSystem.InputAction FindAction(System.String actionNameOrId, System.Boolean throwIfNotFound)
  RVA=0x0B19986C  token=0x6000191  System.Int32 FindBinding(UnityEngine.InputSystem.InputBinding mask, UnityEngine.InputSystem.InputAction& action)
  RVA=0x0B199254  token=0x6000192  UnityEngine.InputSystem.InputActionMap FindActionMap(System.String nameOrId, System.Boolean throwIfNotFound)
  RVA=0x0B199464  token=0x6000193  UnityEngine.InputSystem.InputActionMap FindActionMap(System.Guid id)
  RVA=0x035247B0  token=0x6000194  UnityEngine.InputSystem.InputAction FindAction(System.Guid guid)
  RVA=0x0B19997C  token=0x6000195  System.Int32 FindControlSchemeIndex(System.String name)
  RVA=0x0B199A44  token=0x6000196  System.Nullable<UnityEngine.InputSystem.InputControlScheme> FindControlScheme(System.String name)
  RVA=0x0B199CFC  token=0x6000197  System.Boolean IsUsableWithDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B199124  token=0x6000198  System.Void Enable()
  RVA=0x0B198FF4  token=0x6000199  System.Void Disable()
  RVA=0x0B198F84  token=0x600019A  System.Boolean Contains(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B199C2C  token=0x600019B  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputAction> GetEnumerator()
  RVA=0x0B19A0CC  token=0x600019C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x041E1670  token=0x600019D  System.Void MarkAsDirty()
  RVA=0x0B199C88  token=0x600019E  System.Boolean IsEmpty()
  RVA=0x0B199FC0  token=0x600019F  System.Void OnWantToChangeSetup()
  RVA=0x0B199F4C  token=0x60001A0  System.Void OnSetupChanged()
  RVA=0x0B19A018  token=0x60001A1  System.Void ReResolveIfNecessary(System.Boolean fullResolve)
  RVA=0x0B19A054  token=0x60001A2  System.Void ResolveBindingsIfNecessary()
  RVA=0x0B199F10  token=0x60001A3  System.Void OnDestroy()
  RVA=0x03573370  token=0x60001A4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionChange
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionChangeActionEnabled  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionDisabled  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionMapEnabled  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionMapDisabled  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionStarted  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionPerformed  // const
  public    static  UnityEngine.InputSystem.InputActionChangeActionCanceled  // const
  public    static  UnityEngine.InputSystem.InputActionChangeBoundControlsAboutToChange  // const
  public    static  UnityEngine.InputSystem.InputActionChangeBoundControlsChanged  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionMap
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x140
IMPLEMENTS: System.ICloneable UnityEngine.ISerializationCallbackReceiver UnityEngine.InputSystem.IInputActionCollection2 UnityEngine.InputSystem.IInputActionCollection System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IDisposable
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  k_ResolveBindingsProfilerMarker  // static @ 0x0
  private           System.String                   m_Name  // 0x10
  private           System.String                   m_Id  // 0x18
  private           UnityEngine.InputSystem.InputActionAssetm_Asset  // 0x20
  private           UnityEngine.InputSystem.InputAction[]m_Actions  // 0x28
  private           UnityEngine.InputSystem.InputBinding[]m_Bindings  // 0x30
  private           UnityEngine.InputSystem.InputBinding[]m_BindingsForEachAction  // 0x38
  private           UnityEngine.InputSystem.InputControl[]m_ControlsForEachAction  // 0x40
  private           System.Int32                    m_EnabledActionsCount  // 0x48
  private           UnityEngine.InputSystem.InputActionm_SingletonAction  // 0x50
  private           System.Int32                    m_MapIndexInState  // 0x58
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x60
  private           System.Nullable<UnityEngine.InputSystem.InputBinding>m_BindingMask  // 0x68
  private           UnityEngine.InputSystem.InputActionMap.Flagsm_Flags  // 0xc8
  private           System.Int32                    m_ParameterOverridesCount  // 0xcc
  private           UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[]m_ParameterOverrides  // 0xd0
  private           UnityEngine.InputSystem.InputActionMap.DeviceArraym_Devices  // 0xd8
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>m_ActionCallbacks  // 0xe8
  private           System.Collections.Generic.Dictionary<System.String,System.Int32>m_ActionIndexByNameOrId  // 0x138
  private   static  System.Int32                    s_DeferBindingResolution  // static @ 0x8
  private   static  System.Boolean                  s_NeedToResolveBindings  // static @ 0xc
PROPERTIES:
  name  get=0x02B2ECC0
  asset  get=0x04D862C0
  id  get=0x0B19B87C
  idDontGenerate  get=0x0B19B830
  enabled  get=0x0AD6F584
  actions  get=0x0B19B6EC
  bindings  get=0x0B19B74C
  UnityEngine.InputSystem.IInputActionCollection2.bindings  get=0x0B19B54C
  controlSchemes  get=0x0B19B7A0
  bindingMask  get=0x04DAB740  set=0x0B19B914
  devices  get=0x0363A3C0  set=0x0B19BACC
  Item  get=0x0B19B5F0
  needToResolveBindings  get=0x03DFC720  set=0x043E1300
  bindingResolutionNeedsFullReResolve  get=0x043E1110  set=0x043E12E0
  controlsForEachActionInitialized  get=0x03DFD1E0  set=0x043E12C0
  bindingsForEachActionInitialized  get=0x0B19B740  set=0x043E12A0
EVENTS:
  actionTriggered  add=add_actionTriggered  remove=remove_actionTriggered
METHODS:
  RVA=0x0484F570  token=0x60001C4  System.Void .ctor()
  RVA=0x0484F540  token=0x60001C5  System.Void .ctor(System.String name)
  RVA=0x0B19A8A8  token=0x60001C6  System.Void Dispose()
  RVA=0x03525D90  token=0x60001C7  System.Int32 FindActionIndex(System.String nameOrId)
  RVA=0x03525E80  token=0x60001C8  System.Void SetUpActionLookupTable()
  RVA=0x043E1210  token=0x60001C9  System.Void ClearActionLookupTable()
  RVA=0x03525C90  token=0x60001CA  System.Int32 FindActionIndex(System.Guid id)
  RVA=0x03526450  token=0x60001CB  UnityEngine.InputSystem.InputAction FindAction(System.String actionNameOrId, System.Boolean throwIfNotFound)
  RVA=0x03524860  token=0x60001CC  UnityEngine.InputSystem.InputAction FindAction(System.Guid id)
  RVA=0x0B19ACC4  token=0x60001CD  System.Boolean IsUsableWithDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B19A8BC  token=0x60001CE  System.Void Enable()
  RVA=0x0B19A878  token=0x60001CF  System.Void Disable()
  RVA=0x0B19A600  token=0x60001D0  UnityEngine.InputSystem.InputActionMap Clone()
  RVA=0x0B19B3A0  token=0x60001D1  System.Object System.ICloneable.Clone()
  RVA=0x0B19A84C  token=0x60001D2  System.Boolean Contains(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B19B4B0  token=0x60001D3  System.String ToString()
  RVA=0x0B19AC34  token=0x60001D4  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputAction> GetEnumerator()
  RVA=0x0B19B398  token=0x60001D5  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B19ABA0  token=0x60001DE  UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputBinding> GetBindingsForSingleAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x03DFD140  token=0x60001DF  UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.InputControl> GetControlsForSingleAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x03525F90  token=0x60001E0  System.Void SetUpPerActionControlAndBindingArrays()
  RVA=0x0B19B130  token=0x60001E1  System.Void OnWantToChangeSetup()
  RVA=0x0B19AED8  token=0x60001E2  System.Void OnSetupChanged()
  RVA=0x0B19AEB0  token=0x60001E3  System.Void OnBindingModified()
  RVA=0x043E1250  token=0x60001E4  System.Void ClearCachedActionData(System.Boolean onlyControls)
  RVA=0x0B19AB5C  token=0x60001E5  System.Void GenerateId()
  RVA=0x0B19ADE8  token=0x60001E6  System.Boolean LazyResolveBindings(System.Boolean fullResolve)
  RVA=0x03DFC6F0  token=0x60001E7  System.Boolean ResolveBindingsIfNecessary()
  RVA=0x043DFC40  token=0x60001E8  System.Void ResolveBindings()
  RVA=0x0B19A98C  token=0x60001E9  System.Int32 FindBinding(UnityEngine.InputSystem.InputBinding mask, UnityEngine.InputSystem.InputAction& action)
  RVA=0x0B19A900  token=0x60001EA  System.Int32 FindBindingRelativeToMap(UnityEngine.InputSystem.InputBinding mask)
  RVA=0x0B19AAAC  token=0x60001EB  UnityEngine.InputSystem.InputActionMap[] FromJson(System.String json)
  RVA=0x0B19B3A8  token=0x60001EC  System.String ToJson(System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputActionMap> maps)
  RVA=0x0B19B458  token=0x60001ED  System.String ToJson()
  RVA=0x041E1670  token=0x60001EE  System.Void OnBeforeSerialize()
  RVA=0x043E1120  token=0x60001EF  System.Void OnAfterDeserialize()
  RVA=0x04D38EA0  token=0x60001F0  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionRebindingExtensions
TYPE:  static class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolutionWrappers_DeferBindingResolutionWrapper  // static @ 0x0
METHODS:
  RVA=0x0B19EB54  token=0x6000200  System.Nullable<UnityEngine.InputSystem.Utilities.PrimitiveValue> GetParameterValue(UnityEngine.InputSystem.InputAction action, System.String name, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19ED3C  token=0x6000201  System.Nullable<UnityEngine.InputSystem.Utilities.PrimitiveValue> GetParameterValue(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride)
  RVA=0x0B19F09C  token=0x6000202  System.Nullable<UnityEngine.InputSystem.Utilities.PrimitiveValue> GetParameterValue(UnityEngine.InputSystem.InputAction action, System.String name, System.Int32 bindingIndex)
  RVA=-1  // generic def  token=0x6000203  System.Nullable<TValue> GetParameterValue(UnityEngine.InputSystem.InputAction action, System.Linq.Expressions.Expression<System.Func<TObject,TValue>> expr, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=-1  // generic def  token=0x6000204  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputAction action, System.Linq.Expressions.Expression<System.Func<TObject,TValue>> expr, TValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=-1  // generic def  token=0x6000205  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputActionMap actionMap, System.Linq.Expressions.Expression<System.Func<TObject,TValue>> expr, TValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=-1  // generic def  token=0x6000206  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputActionAsset asset, System.Linq.Expressions.Expression<System.Func<TObject,TValue>> expr, TValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=-1  // generic def  token=0x6000207  UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride ExtractParameterOverride(System.Linq.Expressions.Expression<System.Func<TObject,TValue>> expr, UnityEngine.InputSystem.InputBinding bindingMask, UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x0B19D38C  token=0x6000208  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputActionMap actionMap, System.String name, UnityEngine.InputSystem.Utilities.PrimitiveValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19D824  token=0x6000209  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputActionAsset asset, System.String name, UnityEngine.InputSystem.Utilities.PrimitiveValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19D170  token=0x600020A  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputAction action, System.String name, UnityEngine.InputSystem.Utilities.PrimitiveValue value, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19D588  token=0x600020B  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputAction action, System.String name, UnityEngine.InputSystem.Utilities.PrimitiveValue value, System.Int32 bindingIndex)
  RVA=0x0B19C988  token=0x600020C  System.Void ApplyParameterOverride(UnityEngine.InputSystem.InputActionState state, System.Int32 mapIndex, UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[]& parameterOverrides, System.Int32& parameterOverridesCount, UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride)
  RVA=0x0B19E7A8  token=0x600020D  System.Int32 GetBindingIndex(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19EA08  token=0x600020E  System.Int32 GetBindingIndex(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19E8F4  token=0x600020F  System.Int32 GetBindingIndex(UnityEngine.InputSystem.InputAction action, System.String group, System.String path)
  RVA=0x0B19E400  token=0x6000210  System.Nullable<UnityEngine.InputSystem.InputBinding> GetBindingForControl(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B19E604  token=0x6000211  System.Int32 GetBindingIndexForControl(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B19E03C  token=0x6000212  System.String GetBindingDisplayString(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding.DisplayStringOptions options, System.String group)
  RVA=0x0B19E1F4  token=0x6000213  System.String GetBindingDisplayString(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding bindingMask, UnityEngine.InputSystem.InputBinding.DisplayStringOptions options)
  RVA=0x0B19DA58  token=0x6000214  System.String GetBindingDisplayString(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex, UnityEngine.InputSystem.InputBinding.DisplayStringOptions options)
  RVA=0x0B19DAE0  token=0x6000215  System.String GetBindingDisplayString(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex, System.String& deviceLayoutName, System.String& controlPath, UnityEngine.InputSystem.InputBinding.DisplayStringOptions options)
  RVA=0x0B19BCD4  token=0x6000216  System.Void ApplyBindingOverride(UnityEngine.InputSystem.InputAction action, System.String newPath, System.String group, System.String path)
  RVA=0x0B19BEF8  token=0x6000217  System.Void ApplyBindingOverride(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding bindingOverride)
  RVA=0x0B19C018  token=0x6000218  System.Void ApplyBindingOverride(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex, UnityEngine.InputSystem.InputBinding bindingOverride)
  RVA=0x0B19BDF0  token=0x6000219  System.Void ApplyBindingOverride(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex, System.String path)
  RVA=0x0B19C110  token=0x600021A  System.Int32 ApplyBindingOverride(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding bindingOverride)
  RVA=0x0B19C264  token=0x600021B  System.Void ApplyBindingOverride(UnityEngine.InputSystem.InputActionMap actionMap, System.Int32 bindingIndex, UnityEngine.InputSystem.InputBinding bindingOverride)
  RVA=0x0B1A0254  token=0x600021C  System.Void RemoveBindingOverride(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex)
  RVA=0x0B1A0168  token=0x600021D  System.Void RemoveBindingOverride(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B1A0330  token=0x600021E  System.Void RemoveBindingOverride(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding bindingMask)
  RVA=0x0B19FD90  token=0x600021F  System.Void RemoveAllBindingOverrides(UnityEngine.InputSystem.IInputActionCollection2 actions)
  RVA=0x0B1A0028  token=0x6000220  System.Void RemoveAllBindingOverrides(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B19C748  token=0x6000221  System.Void ApplyBindingOverrides(UnityEngine.InputSystem.InputActionMap actionMap, System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputBinding> overrides)
  RVA=0x0B1A041C  token=0x6000222  System.Void RemoveBindingOverrides(UnityEngine.InputSystem.InputActionMap actionMap, System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputBinding> overrides)
  RVA=0x0B19C5A4  token=0x6000223  System.Int32 ApplyBindingOverridesOnMatchingControls(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B19C444  token=0x6000224  System.Int32 ApplyBindingOverridesOnMatchingControls(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1A065C  token=0x6000225  System.String SaveBindingOverridesAsJson(UnityEngine.InputSystem.IInputActionCollection2 actions)
  RVA=0x0B1A09D0  token=0x6000226  System.String SaveBindingOverridesAsJson(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B19BB14  token=0x6000227  System.Void AddBindingOverrideJsonTo(UnityEngine.InputSystem.IInputActionCollection2 actions, UnityEngine.InputSystem.InputBinding binding, System.Collections.Generic.List<UnityEngine.InputSystem.InputActionMap.BindingOverrideJson> list, UnityEngine.InputSystem.InputAction action)
  RVA=0x0B19F6F0  token=0x6000228  System.Void LoadBindingOverridesFromJson(UnityEngine.InputSystem.IInputActionCollection2 actions, System.String json, System.Boolean removeExisting)
  RVA=0x0B19F814  token=0x6000229  System.Void LoadBindingOverridesFromJson(UnityEngine.InputSystem.InputAction action, System.String json, System.Boolean removeExisting)
  RVA=0x0B19F340  token=0x600022A  System.Void LoadBindingOverridesFromJsonInternal(UnityEngine.InputSystem.IInputActionCollection2 actions, System.String json)
  RVA=0x0B19F940  token=0x600022B  UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation PerformInteractiveRebinding(UnityEngine.InputSystem.InputAction action, System.Int32 bindingIndex)
  RVA=0x03528DB0  token=0x600022C  UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolutionWrapper DeferBindingResolution()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionPhase
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionPhaseDisabled  // const
  public    static  UnityEngine.InputSystem.InputActionPhaseWaiting  // const
  public    static  UnityEngine.InputSystem.InputActionPhaseStarted  // const
  public    static  UnityEngine.InputSystem.InputActionPhasePerformed  // const
  public    static  UnityEngine.InputSystem.InputActionPhaseCanceled  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionProperty
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.IEquatable`1 System.IEquatable`1
FIELDS:
  private           System.Boolean                  m_UseReference  // 0x10
  private           UnityEngine.InputSystem.InputActionm_Action  // 0x18
  private           UnityEngine.InputSystem.InputActionReferencem_Reference  // 0x20
PROPERTIES:
  action  get=0x0B1AC9E0
  reference  get=0x0B1ACA5C
  serializedAction  get=0x04D88320
  serializedReference  get=0x02B2ECC0
METHODS:
  RVA=0x0B1AC984  token=0x6000286  System.Void .ctor(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AC9B0  token=0x6000287  System.Void .ctor(UnityEngine.InputSystem.InputActionReference reference)
  RVA=0x0B1AC774  token=0x6000288  System.Boolean Equals(UnityEngine.InputSystem.InputActionProperty other)
  RVA=0x0B1AC890  token=0x6000289  System.Boolean Equals(UnityEngine.InputSystem.InputAction other)
  RVA=0x0B1AC8AC  token=0x600028A  System.Boolean Equals(UnityEngine.InputSystem.InputActionReference other)
  RVA=0x0B1AC7FC  token=0x600028B  System.Boolean Equals(System.Object obj)
  RVA=0x0B1AC900  token=0x600028C  System.Int32 GetHashCode()
  RVA=0x0B1ACA6C  token=0x600028D  System.Boolean op_Equality(UnityEngine.InputSystem.InputActionProperty left, UnityEngine.InputSystem.InputActionProperty right)
  RVA=0x0B1ACA98  token=0x600028E  System.Boolean op_Inequality(UnityEngine.InputSystem.InputActionProperty left, UnityEngine.InputSystem.InputActionProperty right)
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionReference
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           UnityEngine.InputSystem.InputActionAssetm_Asset  // 0x18
  private           System.String                   m_ActionId  // 0x20
  private           UnityEngine.InputSystem.InputActionm_Action  // 0x28
PROPERTIES:
  asset  get=0x0385B100
  action  get=0x03DFC4B0
METHODS:
  RVA=0x0B1ACE2C  token=0x6000291  System.Void Set(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1ACF4C  token=0x6000292  System.Void Set(UnityEngine.InputSystem.InputActionAsset asset, System.String mapName, System.String actionName)
  RVA=0x0B1ACC9C  token=0x6000293  System.Void SetInternal(UnityEngine.InputSystem.InputActionAsset asset, UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AD260  token=0x6000294  System.String ToString()
  RVA=0x0B1ACB2C  token=0x6000295  System.String GetDisplayName(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AD214  token=0x6000296  System.String ToDisplayName()
  RVA=0x0B1AD458  token=0x6000297  UnityEngine.InputSystem.InputAction op_Implicit(UnityEngine.InputSystem.InputActionReference reference)
  RVA=0x0B1ACAC4  token=0x6000298  UnityEngine.InputSystem.InputActionReference Create(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1ACBB8  token=0x6000299  System.Void ResetCachedAction()
  RVA=0x0B1AD258  token=0x600029A  UnityEngine.InputSystem.InputAction ToInputAction()
  RVA=0x03573370  token=0x600029B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionSetupExtensions
TYPE:  static class
TOKEN: 0x200003D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1AD764  token=0x600029C  UnityEngine.InputSystem.InputActionMap AddActionMap(UnityEngine.InputSystem.InputActionAsset asset, System.String name)
  RVA=0x0B1AD474  token=0x600029D  System.Void AddActionMap(UnityEngine.InputSystem.InputActionAsset asset, UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1AF998  token=0x600029E  System.Void RemoveActionMap(UnityEngine.InputSystem.InputActionAsset asset, UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1AFB18  token=0x600029F  System.Void RemoveActionMap(UnityEngine.InputSystem.InputActionAsset asset, System.String nameOrId)
  RVA=0x0B1AD95C  token=0x60002A0  UnityEngine.InputSystem.InputAction AddAction(UnityEngine.InputSystem.InputActionMap map, System.String name, UnityEngine.InputSystem.InputActionType type, System.String binding, System.String interactions, System.String processors, System.String groups, System.String expectedControlLayout)
  RVA=0x0B1AFC44  token=0x60002A1  System.Void RemoveAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1AFFC4  token=0x60002A2  System.Void RemoveAction(UnityEngine.InputSystem.InputActionAsset asset, System.String nameOrId)
  RVA=0x0B1ADEA4  token=0x60002A3  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputAction action, System.String path, System.String interactions, System.String processors, System.String groups)
  RVA=0x0B1AE30C  token=0x60002A4  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1AE08C  token=0x60002A5  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding binding)
  RVA=0x0B1AE3C4  token=0x60002A6  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputActionMap actionMap, System.String path, System.String interactions, System.String groups, System.String action, System.String processors)
  RVA=0x0B1AE53C  token=0x60002A7  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputActionMap actionMap, System.String path, UnityEngine.InputSystem.InputAction action, System.String interactions, System.String groups)
  RVA=0x0B1ADF9C  token=0x60002A8  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputActionMap actionMap, System.String path, System.Guid action, System.String interactions, System.String groups)
  RVA=0x0B1AE1AC  token=0x60002A9  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding binding)
  RVA=0x0B1AE698  token=0x60002AA  UnityEngine.InputSystem.InputActionSetupExtensions.CompositeSyntax AddCompositeBinding(UnityEngine.InputSystem.InputAction action, System.String composite, System.String interactions, System.String processors)
  RVA=0x0B1ADD30  token=0x60002AB  System.Int32 AddBindingInternal(UnityEngine.InputSystem.InputActionMap map, UnityEngine.InputSystem.InputBinding binding, System.Int32 bindingIndex)
  RVA=0x0B1AF534  token=0x60002AC  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(UnityEngine.InputSystem.InputAction action, System.Int32 index)
  RVA=0x0B1AF290  token=0x60002AD  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(UnityEngine.InputSystem.InputAction action, System.String name)
  RVA=0x0B1AF168  token=0x60002AE  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(UnityEngine.InputSystem.InputActionMap actionMap, System.Int32 index)
  RVA=0x0B1AEE28  token=0x60002AF  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(UnityEngine.InputSystem.InputAction action, System.String id)
  RVA=0x0B1AEF3C  token=0x60002B0  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(UnityEngine.InputSystem.InputAction action, System.Guid id)
  RVA=0x0B1AED18  token=0x60002B1  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithGroup(UnityEngine.InputSystem.InputAction action, System.String group)
  RVA=0x0B1AF058  token=0x60002B2  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithPath(UnityEngine.InputSystem.InputAction action, System.String path)
  RVA=0x0B1AF348  token=0x60002B3  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(UnityEngine.InputSystem.InputAction action, UnityEngine.InputSystem.InputBinding match)
  RVA=0x0B1AF5F4  token=0x60002B4  UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeCompositeBinding(UnityEngine.InputSystem.InputAction action, System.String compositeName)
  RVA=0x0B1B022C  token=0x60002B5  System.Void Rename(UnityEngine.InputSystem.InputAction action, System.String newName)
  RVA=0x0B1AE894  token=0x60002B6  System.Void AddControlScheme(UnityEngine.InputSystem.InputActionAsset asset, UnityEngine.InputSystem.InputControlScheme controlScheme)
  RVA=0x0B1AEB48  token=0x60002B7  UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax AddControlScheme(UnityEngine.InputSystem.InputActionAsset asset, System.String name)
  RVA=0x0B1B00EC  token=0x60002B8  System.Void RemoveControlScheme(UnityEngine.InputSystem.InputActionAsset asset, System.String name)
  RVA=0x0B1B04B8  token=0x60002B9  UnityEngine.InputSystem.InputControlScheme WithBindingGroup(UnityEngine.InputSystem.InputControlScheme scheme, System.String bindingGroup)
  RVA=0x0B1B0588  token=0x60002BA  UnityEngine.InputSystem.InputControlScheme WithDevice(UnityEngine.InputSystem.InputControlScheme scheme, System.String controlPath, System.Boolean required)
  RVA=0x0B1B0760  token=0x60002BB  UnityEngine.InputSystem.InputControlScheme WithRequiredDevice(UnityEngine.InputSystem.InputControlScheme scheme, System.String controlPath)
  RVA=0x0B1B0690  token=0x60002BC  UnityEngine.InputSystem.InputControlScheme WithOptionalDevice(UnityEngine.InputSystem.InputControlScheme scheme, System.String controlPath)
  RVA=0x0B1AF8C8  token=0x60002BD  UnityEngine.InputSystem.InputControlScheme OrWithRequiredDevice(UnityEngine.InputSystem.InputControlScheme scheme, System.String controlPath)
  RVA=0x0B1AF7F8  token=0x60002BE  UnityEngine.InputSystem.InputControlScheme OrWithOptionalDevice(UnityEngine.InputSystem.InputControlScheme scheme, System.String controlPath)
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionState
TYPE:  class
TOKEN: 0x2000042
SIZE:  0xE0
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor System.ICloneable System.IDisposable
FIELDS:
  public    static  System.Int32                    kInvalidIndex  // const
  public            UnityEngine.InputSystem.InputActionMap[]maps  // 0x10
  public            UnityEngine.InputSystem.InputControl[]controls  // 0x18
  public            UnityEngine.InputSystem.IInputInteraction[]interactions  // 0x20
  public            UnityEngine.InputSystem.InputProcessor[]processors  // 0x28
  public            UnityEngine.InputSystem.InputBindingComposite[]composites  // 0x30
  public            System.Int32                    totalProcessorCount  // 0x38
  public            UnityEngine.InputSystem.InputActionState.UnmanagedMemorymemory  // 0x40
  private           System.Boolean                  m_OnBeforeUpdateHooked  // 0xc0
  private           System.Boolean                  m_OnAfterUpdateHooked  // 0xc1
  private           System.Boolean                  m_InProcessControlStateChange  // 0xc2
  private           UnityEngine.InputSystem.LowLevel.InputEventPtrm_CurrentlyProcessingThisEvent  // 0xc8
  private           System.Action                   m_OnBeforeUpdateDelegate  // 0xd0
  private           System.Action                   m_OnAfterUpdateDelegate  // 0xd8
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputInitialActionStateCheckMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputActionResolveConflictMarker  // static @ 0x8
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputActionCallbackMarker  // static @ 0x10
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnActionChangeMarker  // static @ 0x18
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnDeviceChangeMarker  // static @ 0x20
  private   static  UnityEngine.InputSystem.InputActionState.GlobalStates_GlobalState  // static @ 0x28
PROPERTIES:
  totalCompositeCount  get=0x04D86560
  totalMapCount  get=0x04D863D0
  totalActionCount  get=0x04D86600
  totalBindingCount  get=0x04D86540
  totalInteractionCount  get=0x04D86550
  totalControlCount  get=0x04D86570
  mapIndices  get=0x04D85EF0
  actionStates  get=0x04D86280
  bindingStates  get=0x04D86230
  interactionStates  get=0x04D862B0
  controlIndexToBindingIndex  get=0x04D86210
  controlGroupingAndComplexity  get=0x04D86290
  controlMagnitudes  get=0x04D862A0
  enabledControls  get=0x04D861F0
  isProcessingControlStateChange  get=0x04DA5BF0
METHODS:
  RVA=0x043E13E0  token=0x60002FC  System.Void Initialize(UnityEngine.InputSystem.InputBindingResolver resolver)
  RVA=0x043E17E0  token=0x60002FD  System.Void ComputeControlGroupingIfNecessary()
  RVA=0x043E15D0  token=0x60002FE  System.Void ClaimDataFrom(UnityEngine.InputSystem.InputBindingResolver resolver)
  RVA=0x0B1B1BD8  token=0x60002FF  System.Void Finalize()
  RVA=0x0B1B1994  token=0x6000300  System.Void Dispose()
  RVA=0x0B1B1218  token=0x6000301  System.Void Destroy(System.Boolean isFinalizing)
  RVA=0x0B1B0EFC  token=0x6000302  UnityEngine.InputSystem.InputActionState Clone()
  RVA=0x0B1B44D0  token=0x6000303  System.Object System.ICloneable.Clone()
  RVA=0x0363A280  token=0x6000304  System.Boolean IsUsingDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1B0830  token=0x6000305  System.Boolean CanUseDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1B2364  token=0x6000306  System.Boolean HasEnabledActions()
  RVA=0x03DFE2A0  token=0x6000307  System.Void FinishBindingCompositeSetups()
  RVA=0x0B1B2434  token=0x6000308  System.Void PrepareForBindingReResolution(System.Boolean needFullResolve, UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>& activeControls, System.Boolean& hasEnabledActions)
  RVA=0x043E1380  token=0x6000309  System.Void FinishBindingResolution(System.Boolean hasEnabledActions, UnityEngine.InputSystem.InputActionState.UnmanagedMemory oldMemory, UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl> activeControls, System.Boolean isFullResolve)
  RVA=0x0B1B3750  token=0x600030A  System.Void RestoreActionStatesAfterReResolvingBindings(UnityEngine.InputSystem.InputActionState.UnmanagedMemory oldState, UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl> activeControls, System.Boolean isFullResolve)
  RVA=0x04B87700  token=0x600030B  System.Boolean IsActiveControl(System.Int32 bindingIndex, System.Int32 controlIndex)
  RVA=0x0B1B1EA0  token=0x600030C  System.Int32 FindControlIndexOnBinding(System.Int32 bindingIndex, UnityEngine.InputSystem.InputControl control)
  RVA=0x03639F50  token=0x600030D  System.Void ResetActionStatesDrivenBy(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0363A180  token=0x600030E  System.Boolean IsActionBoundToControlFromDevice(UnityEngine.InputSystem.InputDevice device, System.Int32 actionIndex)
  RVA=0x0B1B32B0  token=0x600030F  System.Void ResetActionState(System.Int32 actionIndex, UnityEngine.InputSystem.InputActionPhase toPhase, System.Boolean hardReset)
  RVA=0x03DFC6C0  token=0x6000310  UnityEngine.InputSystem.InputActionState.TriggerState& FetchActionState(UnityEngine.InputSystem.InputAction action)
  RVA=0x04D54C60  token=0x6000311  UnityEngine.InputSystem.InputActionState.ActionMapIndices FetchMapIndices(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1B19A0  token=0x6000312  System.Void EnableAllActions(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1B1A9C  token=0x6000313  System.Void EnableControls(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x03DFC7D0  token=0x6000314  System.Void EnableSingleAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x03DFC980  token=0x6000315  System.Void EnableControls(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B14CC  token=0x6000316  System.Void DisableAllActions(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1B18A0  token=0x6000317  System.Void DisableControls(UnityEngine.InputSystem.InputActionMap map)
  RVA=0x0B1B1900  token=0x6000318  System.Void DisableSingleAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B1660  token=0x6000319  System.Void DisableControls(UnityEngine.InputSystem.InputAction action)
  RVA=0x03DFCBB0  token=0x600031A  System.Void EnableControls(System.Int32 mapIndex, System.Int32 controlStartIndex, System.Int32 numControls)
  RVA=0x0B1B1740  token=0x600031B  System.Void DisableControls(System.Int32 mapIndex, System.Int32 controlStartIndex, System.Int32 numControls)
  RVA=0x0B1B4184  token=0x600031C  System.Void SetInitialStateCheckPending(System.Int32 actionIndex, System.Boolean value)
  RVA=0x03DFD1F0  token=0x600031D  System.Void SetInitialStateCheckPending(UnityEngine.InputSystem.InputActionState.BindingState* bindingStatePtr, System.Boolean value)
  RVA=0x03DFCDB0  token=0x600031E  System.Boolean IsControlEnabled(System.Int32 controlIndex)
  RVA=0x03DFCD70  token=0x600031F  System.Void SetControlEnabled(System.Int32 controlIndex, System.Boolean state)
  RVA=0x03DFC880  token=0x6000320  System.Void HookOnBeforeUpdate()
  RVA=0x04CDCD80  token=0x6000321  System.Void UnhookOnBeforeUpdate()
  RVA=0x03DFD270  token=0x6000322  System.Void OnBeforeInitialUpdate()
  RVA=0x03527120  token=0x6000323  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 mapControlAndBindingIndex)
  RVA=0x0B1B44D8  token=0x6000324  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(UnityEngine.InputSystem.InputControl control, System.Double time, System.Int64 mapControlAndBindingIndex, System.Int32 interactionIndex)
  RVA=0x04DAB8E0  token=0x6000325  System.Int64 ToCombinedMapAndControlAndBindingIndex(System.Int32 mapIndex, System.Int32 controlIndex, System.Int32 bindingIndex)
  RVA=0x04DAB8B0  token=0x6000326  System.Void SplitUpMapAndControlAndBindingIndex(System.Int64 mapControlAndBindingIndex, System.Int32& mapIndex, System.Int32& controlIndex, System.Int32& bindingIndex)
  RVA=0x04DAB8A0  token=0x6000327  System.Int32 GetComplexityFromMonitorIndex(System.Int64 mapControlAndBindingIndex)
  RVA=0x03527760  token=0x6000328  System.Void ProcessControlStateChange(System.Int32 mapIndex, System.Int32 controlIndex, System.Int32 bindingIndex, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x03528BC0  token=0x6000329  System.Void ProcessButtonState(UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Int32 actionIndex, UnityEngine.InputSystem.InputActionState.BindingState* bindingStatePtr)
  RVA=0x0B1B4288  token=0x600032A  System.Boolean ShouldIgnoreInputOnCompositeBinding(UnityEngine.InputSystem.InputActionState.BindingState* binding, UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x035296B0  token=0x600032B  System.Boolean IsConflictingInput(UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Int32 actionIndex)
  RVA=0x04DAB860  token=0x600032C  System.UInt16 GetActionBindingStartIndexAndCount(System.Int32 actionIndex, System.UInt16& bindingCount)
  RVA=0x035291D0  token=0x600032D  System.Void ProcessDefaultInteraction(UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Int32 actionIndex)
  RVA=0x0B1B28F8  token=0x600032E  System.Void ProcessInteractions(UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Int32 interactionStartIndex, System.Int32 interactionCount)
  RVA=0x0B1B2A34  token=0x600032F  System.Void ProcessTimeout(System.Double time, System.Int32 mapIndex, System.Int32 controlIndex, System.Int32 bindingIndex, System.Int32 interactionIndex)
  RVA=0x0B1B4244  token=0x6000330  System.Void SetTotalTimeoutCompletionTime(System.Single seconds, UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x0B1B42A8  token=0x6000331  System.Void StartTimeout(System.Single seconds, UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x0B1B440C  token=0x6000332  System.Void StopTimeout(System.Int32 interactionIndex)
  RVA=0x0B1B09E0  token=0x6000333  System.Void ChangePhaseOfInteraction(UnityEngine.InputSystem.InputActionPhase newPhase, UnityEngine.InputSystem.InputActionState.TriggerState& trigger, UnityEngine.InputSystem.InputActionPhase phaseAfterPerformed, UnityEngine.InputSystem.InputActionPhase phaseAfterCanceled, System.Boolean processNextInteractionOnCancel)
  RVA=0x03528EB0  token=0x6000334  System.Boolean ChangePhaseOfAction(UnityEngine.InputSystem.InputActionPhase newPhase, UnityEngine.InputSystem.InputActionState.TriggerState& trigger, UnityEngine.InputSystem.InputActionPhase phaseAfterPerformedOrCanceled)
  RVA=0x035274D0  token=0x6000335  System.Void ChangePhaseOfActionInternal(System.Int32 actionIndex, UnityEngine.InputSystem.InputActionState.TriggerState* actionState, UnityEngine.InputSystem.InputActionPhase newPhase, UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Boolean isDisablingAction)
  RVA=0x03527170  token=0x6000336  System.Void CallActionListeners(System.Int32 actionIndex, UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputActionPhase phase, UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>& listeners, System.String callbackName)
  RVA=0x0B1B1F48  token=0x6000337  System.Object GetActionOrNoneString(UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x03527430  token=0x6000338  UnityEngine.InputSystem.InputAction GetActionOrNull(System.Int32 bindingIndex)
  RVA=0x0B1B1FA0  token=0x6000339  UnityEngine.InputSystem.InputAction GetActionOrNull(UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x0B1B21AC  token=0x600033A  UnityEngine.InputSystem.InputControl GetControl(UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x0B1B21E8  token=0x600033B  UnityEngine.InputSystem.IInputInteraction GetInteractionOrNull(UnityEngine.InputSystem.InputActionState.TriggerState& trigger)
  RVA=0x0B1B2040  token=0x600033C  System.Int32 GetBindingIndexInMap(System.Int32 bindingIndex)
  RVA=0x0B1B2084  token=0x600033D  System.Int32 GetBindingIndexInState(System.Int32 mapIndex, System.Int32 bindingIndexInMap)
  RVA=0x04DAB890  token=0x600033E  UnityEngine.InputSystem.InputActionState.BindingState& GetBindingState(System.Int32 bindingIndex)
  RVA=0x0B1B20BC  token=0x600033F  UnityEngine.InputSystem.InputBinding& GetBinding(System.Int32 bindingIndex)
  RVA=0x0B1B1F0C  token=0x6000340  UnityEngine.InputSystem.InputActionMap GetActionMap(System.Int32 bindingIndex)
  RVA=0x0B1B358C  token=0x6000341  System.Void ResetInteractionStateAndCancelIfNecessary(System.Int32 mapIndex, System.Int32 bindingIndex, System.Int32 interactionIndex, UnityEngine.InputSystem.InputActionPhase phaseAfterCanceled)
  RVA=0x0B1B3644  token=0x6000342  System.Void ResetInteractionState(System.Int32 interactionIndex)
  RVA=0x0B1B2244  token=0x6000343  System.Int32 GetValueSizeInBytes(System.Int32 bindingIndex, System.Int32 controlIndex)
  RVA=0x0B1B22D4  token=0x6000344  System.Type GetValueType(System.Int32 bindingIndex, System.Int32 controlIndex)
  RVA=0x0B1B23B4  token=0x6000345  System.Boolean IsActuated(UnityEngine.InputSystem.InputActionState.TriggerState& trigger, System.Single threshold)
  RVA=0x0B1B2FCC  token=0x6000346  System.Void ReadValue(System.Int32 bindingIndex, System.Int32 controlIndex, System.Void* buffer, System.Int32 bufferSize, System.Boolean ignoreComposites)
  RVA=-1  // generic def  token=0x6000347  TValue ReadValue(System.Int32 bindingIndex, System.Int32 controlIndex, System.Boolean ignoreComposites)
  RVA=-1  // generic def  token=0x6000348  TValue ApplyProcessors(System.Int32 bindingIndex, TValue value, UnityEngine.InputSystem.InputControl<TValue> controlOfType)
  RVA=0x0B1B1AFC  token=0x6000349  System.Single EvaluateCompositePartMagnitude(System.Int32 bindingIndex, System.Int32 partNumber)
  RVA=0x0B1B2134  token=0x600034A  System.Double GetCompositePartPressTime(System.Int32 bindingIndex, System.Int32 partNumber)
  RVA=-1  // generic def  token=0x600034B  TValue ReadCompositePartValue(System.Int32 bindingIndex, System.Int32 partNumber, System.Boolean* buttonValuePtr, System.Int32& controlIndex, TComparer comparer)
  RVA=0x0B1B2CD0  token=0x600034C  System.Boolean ReadCompositePartValue(System.Int32 bindingIndex, System.Int32 partNumber, System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1B2BC0  token=0x600034D  System.Object ReadCompositePartValueAsObject(System.Int32 bindingIndex, System.Int32 partNumber)
  RVA=0x0B1B2E04  token=0x600034E  System.Object ReadValueAsObject(System.Int32 bindingIndex, System.Int32 controlIndex, System.Boolean ignoreComposites)
  RVA=0x03526650  token=0x600034F  System.Boolean ReadValueAsButton(System.Int32 bindingIndex, System.Int32 controlIndex)
  RVA=0x0B1B3F4C  token=0x6000350  UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
  RVA=0x043E1480  token=0x6000351  System.Void AddToGlobalList()
  RVA=0x0B1B318C  token=0x6000352  System.Void RemoveMapFromGlobalList()
  RVA=0x043E1510  token=0x6000353  System.Void CompactGlobalList()
  RVA=0x043E16B0  token=0x6000354  System.Void NotifyListenersOfActionChange(UnityEngine.InputSystem.InputActionChange change)
  RVA=0x03DFCA80  token=0x6000355  System.Void NotifyListenersOfActionChange(UnityEngine.InputSystem.InputActionChange change, System.Object actionOrMapOrAsset)
  RVA=0x0B1B3418  token=0x6000356  System.Void ResetGlobals()
  RVA=0x0B1B1C4C  token=0x6000357  System.Int32 FindAllEnabledActions(System.Collections.Generic.List<UnityEngine.InputSystem.InputAction> result)
  RVA=0x0363B6C0  token=0x6000358  System.Void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x03DFB4E0  token=0x6000359  System.Void DeferredResolutionOfBindings()
  RVA=0x0B1B1394  token=0x600035A  System.Void DisableAllActions()
  RVA=0x0B1B109C  token=0x600035B  System.Void DestroyAllActionMapStates()
  RVA=0x041E1670  token=0x600035C  System.Void .ctor()
  RVA=0x04B1D8F0  token=0x600035D  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputActionType
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputActionTypeValue  // const
  public    static  UnityEngine.InputSystem.InputActionTypeButton  // const
  public    static  UnityEngine.InputSystem.InputActionTypePassThrough  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputBinding
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x68
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    static  System.Char                     Separator  // const
  private   static  System.String                   kSeparatorString  // const
  private           System.String                   m_Name  // 0x10
  private           System.String                   m_Id  // 0x18
  private           System.String                   m_Path  // 0x20
  private           System.String                   m_Interactions  // 0x28
  private           System.String                   m_Processors  // 0x30
  private           System.String                   m_Groups  // 0x38
  private           System.String                   m_Action  // 0x40
  private           UnityEngine.InputSystem.InputBinding.Flagsm_Flags  // 0x48
  private           System.String                   m_OverridePath  // 0x50
  private           System.String                   m_OverrideInteractions  // 0x58
  private           System.String                   m_OverrideProcessors  // 0x60
PROPERTIES:
  name  get=0x02B76770  set=0x05392320
  id  get=0x0B1B546C  set=0x0B1B55D8
  path  get=0x02B2ECC0  set=0x053908C0
  overridePath  get=0x04D85A60  set=0x042B4AE0
  interactions  get=0x0385B100  set=0x05392C40
  overrideInteractions  get=0x04D86200  set=0x035A41A0
  processors  get=0x04D862C0  set=0x05390F40
  overrideProcessors  get=0x04D86270  set=0x02FDAEA0
  groups  get=0x04D86240  set=0x02FDB880
  action  get=0x011F36E0  set=0x05396200
  isComposite  get=0x03DFE580  set=0x0B1B5604
  isPartOfComposite  get=0x03DFE5E0  set=0x0B1B5620
  hasOverrides  get=0x0B1B5450
  effectivePath  get=0x03DFE5A0
  effectiveInteractions  get=0x03DFE3B0
  effectiveProcessors  get=0x03DFE5B0
  isEmpty  get=0x0B1B54B8
METHODS:
  RVA=0x0B1B53AC  token=0x60003EA  System.Void .ctor(System.String path, System.String action, System.String groups, System.String processors, System.String interactions, System.String name)
  RVA=0x0B1B4BF8  token=0x60003EB  System.String GetNameOfComposite()
  RVA=0x0B1B4A98  token=0x60003EC  System.Void GenerateId()
  RVA=0x0B1B4F20  token=0x60003ED  System.Void RemoveOverrides()
  RVA=0x0B1B4C28  token=0x60003EE  UnityEngine.InputSystem.InputBinding MaskByGroup(System.String group)
  RVA=0x0B1B4C60  token=0x60003EF  UnityEngine.InputSystem.InputBinding MaskByGroups(System.String[] groups)
  RVA=0x0B1B49B0  token=0x60003F4  System.Boolean Equals(UnityEngine.InputSystem.InputBinding other)
  RVA=0x0B1B490C  token=0x60003F5  System.Boolean Equals(System.Object obj)
  RVA=0x0B1B54FC  token=0x60003F6  System.Boolean op_Equality(UnityEngine.InputSystem.InputBinding left, UnityEngine.InputSystem.InputBinding right)
  RVA=0x0B1B554C  token=0x60003F7  System.Boolean op_Inequality(UnityEngine.InputSystem.InputBinding left, UnityEngine.InputSystem.InputBinding right)
  RVA=0x0B1B4ADC  token=0x60003F8  System.Int32 GetHashCode()
  RVA=0x0B1B5264  token=0x60003F9  System.String ToString()
  RVA=0x0B1B5230  token=0x60003FA  System.String ToDisplayString(UnityEngine.InputSystem.InputBinding.DisplayStringOptions options, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1B4F58  token=0x60003FB  System.String ToDisplayString(System.String& deviceLayoutName, System.String& controlPath, UnityEngine.InputSystem.InputBinding.DisplayStringOptions options, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1B5358  token=0x60003FC  System.Boolean TriggersAction(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B4DBC  token=0x60003FD  System.Boolean Matches(UnityEngine.InputSystem.InputBinding binding)
  RVA=0x0B1B4DD0  token=0x60003FE  System.Boolean Matches(UnityEngine.InputSystem.InputBinding& binding, UnityEngine.InputSystem.InputBinding.MatchOptions options)
END_CLASS

CLASS: UnityEngine.InputSystem.InputBindingComposite
TYPE:  abstract class
TOKEN: 0x2000053
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.InputSystem.Utilities.TypeTables_Composites  // static @ 0x0
PROPERTIES:
  valueType  get=-1  // abstract
  valueSizeInBytes  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000404  System.Void ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context, System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // abstract  token=0x6000405  System.Object ReadValueAsObject(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x04DA9E10  token=0x6000406  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x041E1670  token=0x6000407  System.Void FinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x05395D44  token=0x6000408  System.Void CallFinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1B4800  token=0x6000409  System.Type GetValueType(System.String composite)
  RVA=0x0B1B460C  token=0x600040A  System.String GetExpectedControlLayoutName(System.String composite, System.String part)
  RVA=0x0B1B4790  token=0x600040B  System.Collections.Generic.IEnumerable<System.String> GetPartNames(System.String composite)
  RVA=0x0B1B4520  token=0x600040C  System.String GetDisplayFormatString(System.String composite)
  RVA=0x041E1670  token=0x600040D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputBindingComposite`1
TYPE:  abstract class
TOKEN: 0x2000055
EXTENDS: UnityEngine.InputSystem.InputBindingComposite
FIELDS:
PROPERTIES:
  valueType  get=-1  // not resolved
  valueSizeInBytes  get=-1  // not resolved
METHODS:
  RVA=-1  // abstract  token=0x6000418  TValue ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=-1  // not resolved  token=0x6000419  System.Void ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context, System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // not resolved  token=0x600041A  System.Object ReadValueAsObject(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=-1  // not resolved  token=0x600041B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputBindingCompositeContext
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x20
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           System.Int32                    m_BindingIndex  // 0x18
PROPERTIES:
  controls  get=0x0B1CB9B8
METHODS:
  RVA=0x0B1CB880  token=0x600041D  System.Single EvaluateMagnitude(System.Int32 partNumber)
  RVA=-1  // generic def  token=0x600041E  TValue ReadValue(System.Int32 partNumber)
  RVA=-1  // generic def  token=0x600041F  TValue ReadValue(System.Int32 partNumber, UnityEngine.InputSystem.InputControl& sourceControl)
  RVA=-1  // generic def  token=0x6000420  TValue ReadValue(System.Int32 partNumber, TComparer comparer)
  RVA=-1  // generic def  token=0x6000421  TValue ReadValue(System.Int32 partNumber, UnityEngine.InputSystem.InputControl& sourceControl, TComparer comparer)
  RVA=0x0B1CB8D8  token=0x6000422  System.Boolean ReadValueAsButton(System.Int32 partNumber)
  RVA=0x0B1CB98C  token=0x6000423  System.Void ReadValue(System.Int32 partNumber, System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1CB960  token=0x6000424  System.Object ReadValueAsObject(System.Int32 partNumber)
  RVA=0x0B1CB8AC  token=0x6000425  System.Double GetPressTime(System.Int32 partNumber)
END_CLASS

CLASS: UnityEngine.InputSystem.InputBindingResolver
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x138
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.Int32                    totalProcessorCount  // 0x10
  public            System.Int32                    totalCompositeCount  // 0x14
  public            System.Int32                    totalInteractionCount  // 0x18
  public            UnityEngine.InputSystem.InputActionMap[]maps  // 0x20
  public            UnityEngine.InputSystem.InputControl[]controls  // 0x28
  public            UnityEngine.InputSystem.InputActionState.UnmanagedMemorymemory  // 0x30
  public            UnityEngine.InputSystem.IInputInteraction[]interactions  // 0xb0
  public            UnityEngine.InputSystem.InputProcessor[]processors  // 0xb8
  public            UnityEngine.InputSystem.InputBindingComposite[]composites  // 0xc0
  public            System.Nullable<UnityEngine.InputSystem.InputBinding>bindingMask  // 0xc8
  private           System.Boolean                  m_IsControlOnlyResolve  // 0x128
  private           System.Collections.Generic.List<UnityEngine.InputSystem.Utilities.NameAndParameters>m_Parameters  // 0x130
PROPERTIES:
  totalMapCount  get=0x011F2A90
  totalActionCount  get=0x04D86710
  totalBindingCount  get=0x04D86730
  totalControlCount  get=0x04D863B0
METHODS:
  RVA=0x0B1CBA2C  token=0x6000437  System.Void Dispose()
  RVA=0x0B1CBA38  token=0x6000438  System.Void StartWithPreviousResolve(UnityEngine.InputSystem.InputActionState state, System.Boolean isFullResolve)
  RVA=0x03DFE5F0  token=0x6000439  System.Void AddActionMap(UnityEngine.InputSystem.InputActionMap actionMap)
  RVA=-1  // generic def  token=0x600043A  System.Int32 InstantiateWithParameters(UnityEngine.InputSystem.Utilities.TypeTable registrations, System.String namesAndParameters, TType[]& array, System.Int32& count, UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding& binding)
  RVA=0x03DFDF60  token=0x600043B  UnityEngine.InputSystem.InputBindingComposite InstantiateBindingComposite(UnityEngine.InputSystem.InputBinding& binding, UnityEngine.InputSystem.InputActionMap actionMap)
  RVA=0x03DFD580  token=0x600043C  System.Void ApplyParameters(UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.NamedValue> parameters, System.Object instance, UnityEngine.InputSystem.InputActionMap actionMap, UnityEngine.InputSystem.InputBinding& binding, System.String objectRegistrationName, System.String namesAndParameters)
  RVA=0x03DFE110  token=0x600043D  System.Int32 AssignCompositePartIndex(System.Object composite, System.String name, System.Int32& currentCompositePartCount)
END_CLASS

CLASS: UnityEngine.InputSystem.InputControlScheme
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.String                   m_Name  // 0x10
  private           System.String                   m_BindingGroup  // 0x18
  private           UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[]m_DeviceRequirements  // 0x20
PROPERTIES:
  name  get=0x02B76770
  bindingGroup  get=0x04D88320  set=0x067D5E9C
  deviceRequirements  get=0x0B1CFDC4
METHODS:
  RVA=0x0B1CFCA8  token=0x6000442  System.Void .ctor(System.String name, System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControlScheme.DeviceRequirement> devices, System.String bindingGroup)
  RVA=0x0B1CF9E0  token=0x6000443  System.Void SetNameAndBindingGroup(System.String name, System.String bindingGroup)
  RVA=-1  // generic def  token=0x6000444  System.Nullable<UnityEngine.InputSystem.InputControlScheme> FindControlSchemeForDevices(TDevices devices, TSchemes schemes, UnityEngine.InputSystem.InputDevice mustIncludeDevice, System.Boolean allowUnsuccesfulMatch)
  RVA=-1  // generic def  token=0x6000445  System.Boolean FindControlSchemeForDevices(TDevices devices, TSchemes schemes, UnityEngine.InputSystem.InputControlScheme& controlScheme, UnityEngine.InputSystem.InputControlScheme.MatchResult& matchResult, UnityEngine.InputSystem.InputDevice mustIncludeDevice, System.Boolean allowUnsuccessfulMatch)
  RVA=-1  // generic def  token=0x6000446  System.Nullable<UnityEngine.InputSystem.InputControlScheme> FindControlSchemeForDevice(UnityEngine.InputSystem.InputDevice device, TSchemes schemes)
  RVA=0x0B1CFA94  token=0x6000447  System.Boolean SupportsDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=-1  // generic def  token=0x6000448  UnityEngine.InputSystem.InputControlScheme.MatchResult PickDevicesFrom(TDevices devices, UnityEngine.InputSystem.InputDevice favorDevice)
  RVA=0x0B1CF71C  token=0x6000449  System.Boolean Equals(UnityEngine.InputSystem.InputControlScheme other)
  RVA=0x0B1CF8E4  token=0x600044A  System.Boolean Equals(System.Object obj)
  RVA=0x0B1CF95C  token=0x600044B  System.Int32 GetHashCode()
  RVA=0x0B1CFB50  token=0x600044C  System.String ToString()
  RVA=0x0B1CFE18  token=0x600044D  System.Boolean op_Equality(UnityEngine.InputSystem.InputControlScheme left, UnityEngine.InputSystem.InputControlScheme right)
  RVA=0x0B1CFE44  token=0x600044E  System.Boolean op_Inequality(UnityEngine.InputSystem.InputControlScheme left, UnityEngine.InputSystem.InputControlScheme right)
END_CLASS

CLASS: UnityEngine.InputSystem.InputInteractionContext
TYPE:  sealed struct
TOKEN: 0x2000064
SIZE:  0x68
FIELDS:
  private           UnityEngine.InputSystem.InputActionStatem_State  // 0x10
  private           UnityEngine.InputSystem.InputInteractionContext.Flagsm_Flags  // 0x18
  private           UnityEngine.InputSystem.InputActionState.TriggerStatem_TriggerState  // 0x20
PROPERTIES:
  action  get=0x0B1D2D6C
  control  get=0x0B1D2DA0
  phase  get=0x015EFCE0
  time  get=0x04D86360
  startTime  get=0x04D86970
  timerHasExpired  get=0x0B1CAE9C  set=0x0B1CAF08
  isWaiting  get=0x0B1D2DDC
  isStarted  get=0x0B1D2DD4
  mapIndex  get=0x04D88390
  controlIndex  get=0x0B1D2D94
  bindingIndex  get=0x04DABE80
  interactionIndex  get=0x0B1D2DC8
METHODS:
  RVA=0x04D86AF0  token=0x600047F  System.Single ComputeMagnitude()
  RVA=0x0B1D2AB4  token=0x6000480  System.Boolean ControlIsActuated(System.Single threshold)
  RVA=0x0B1D2CD8  token=0x6000481  System.Void Started()
  RVA=0x0B1D2BA8  token=0x6000482  System.Void Performed()
  RVA=0x0B1D2B5C  token=0x6000483  System.Void PerformedAndStayStarted()
  RVA=0x0B1D2B10  token=0x6000484  System.Void PerformedAndStayPerformed()
  RVA=0x0B1D2A68  token=0x6000485  System.Void Canceled()
  RVA=0x0B1D2D24  token=0x6000486  System.Void Waiting()
  RVA=0x0B1D2BF4  token=0x6000487  System.Void SetTimeout(System.Single seconds)
  RVA=0x0B1D2C1C  token=0x6000488  System.Void SetTotalTimeoutCompletionTime(System.Single seconds)
  RVA=-1  // generic def  token=0x6000489  TValue ReadValue()
END_CLASS

CLASS: UnityEngine.InputSystem.InputSystem
TYPE:  static class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
  private   static  System.String                   kAssemblyVersion  // const
  private   static  System.String                   kDocUrl  // const
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputResetMarker  // static @ 0x0
  private   static  System.Single                   scrollWheelDeltaPerTick  // const
  private   static  UnityEngine.InputSystem.InputManagers_Manager  // static @ 0x8
  private   static  UnityEngine.InputSystem.InputRemotings_Remote  // static @ 0x10
PROPERTIES:
  devices  get=0x03DF7FE0
  disconnectedDevices  get=0x0B1D5B14
  pollingFrequency  get=0x0B1D5E9C  set=0x0B1D682C
  isProcessingEvents  get=0x0B1D5BA8
  onEvent  get=0x012081B0  set=0x041E1670
  onAnyButtonPress  get=0x0B1D5C90
  settings  get=0x033F20D0  set=0x0B1D6924
  remoting  get=0x0B1D5EFC
  version  get=0x0B1D5FAC
  runInBackground  get=0x0B1D5F4C  set=0x0B1D6894
  metrics  get=0x0B1D5C0C
EVENTS:
  onLayoutChange  add=add_onLayoutChange  remove=remove_onLayoutChange
  onDeviceChange  add=add_onDeviceChange  remove=remove_onDeviceChange
  onDeviceCommand  add=add_onDeviceCommand  remove=remove_onDeviceCommand
  onFindLayoutForDevice  add=add_onFindLayoutForDevice  remove=remove_onFindLayoutForDevice
  onBeforeUpdate  add=add_onBeforeUpdate  remove=remove_onBeforeUpdate
  onAfterUpdate  add=add_onAfterUpdate  remove=remove_onAfterUpdate
  onSettingsChange  add=add_onSettingsChange  remove=remove_onSettingsChange
  onActionChange  add=add_onActionChange  remove=remove_onActionChange
METHODS:
  RVA=0x03AEC0D0  token=0x6000490  System.Void RegisterLayout(System.Type type, System.String name, System.Nullable<UnityEngine.InputSystem.Layouts.InputDeviceMatcher> matches)
  RVA=-1  // generic def  token=0x6000491  System.Void RegisterLayout(System.String name, System.Nullable<UnityEngine.InputSystem.Layouts.InputDeviceMatcher> matches)
  RVA=0x03DF7060  token=0x6000492  System.Void RegisterLayout(System.String json, System.String name, System.Nullable<UnityEngine.InputSystem.Layouts.InputDeviceMatcher> matches)
  RVA=0x0B1D492C  token=0x6000493  System.Void RegisterLayoutOverride(System.String json, System.String name)
  RVA=0x0B1D48BC  token=0x6000494  System.Void RegisterLayoutMatcher(System.String layoutName, UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
  RVA=-1  // generic def  token=0x6000495  System.Void RegisterLayoutMatcher(UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
  RVA=0x0B1D470C  token=0x6000496  System.Void RegisterLayoutBuilder(System.Func<UnityEngine.InputSystem.Layouts.InputControlLayout> buildMethod, System.String name, System.String baseLayout, System.Nullable<UnityEngine.InputSystem.Layouts.InputDeviceMatcher> matches)
  RVA=-1  // generic def  token=0x6000497  System.Void RegisterPrecompiledLayout(System.String metadata)
  RVA=0x0B1D4F4C  token=0x6000498  System.Void RemoveLayout(System.String name)
  RVA=0x0B1D5368  token=0x6000499  System.String TryFindMatchingLayout(UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
  RVA=0x0B1D3D6C  token=0x600049A  System.Collections.Generic.IEnumerable<System.String> ListLayouts()
  RVA=0x0B1D3CB4  token=0x600049B  System.Collections.Generic.IEnumerable<System.String> ListLayoutsBasedOn(System.String baseLayout)
  RVA=0x0B1D3E28  token=0x600049C  UnityEngine.InputSystem.Layouts.InputControlLayout LoadLayout(System.String name)
  RVA=-1  // generic def  token=0x600049D  UnityEngine.InputSystem.Layouts.InputControlLayout LoadLayout()
  RVA=0x0B1D37B4  token=0x600049E  System.String GetNameOfBaseLayout(System.String layoutName)
  RVA=0x0B1D39AC  token=0x600049F  System.Boolean IsFirstLayoutBasedOnSecond(System.String firstLayoutName, System.String secondLayoutName)
  RVA=0x0B1D49A0  token=0x60004A0  System.Void RegisterProcessor(System.Type type, System.String name)
  RVA=-1  // generic def  token=0x60004A1  System.Void RegisterProcessor(System.String name)
  RVA=0x0B1D5578  token=0x60004A2  System.Type TryGetProcessor(System.String name)
  RVA=0x0B1D3DC4  token=0x60004A3  System.Collections.Generic.IEnumerable<System.String> ListProcessors()
  RVA=0x0B1D3088  token=0x60004AE  UnityEngine.InputSystem.InputDevice AddDevice(System.String layout, System.String name, System.String variants)
  RVA=-1  // generic def  token=0x60004AF  TDevice AddDevice(System.String name)
  RVA=0x0B1D31A0  token=0x60004B0  UnityEngine.InputSystem.InputDevice AddDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
  RVA=0x0B1D2FD4  token=0x60004B1  System.Void AddDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1D4EE8  token=0x60004B2  System.Void RemoveDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1D350C  token=0x60004B3  System.Void FlushDisconnectedDevices()
  RVA=0x0B1D3754  token=0x60004B4  UnityEngine.InputSystem.InputDevice GetDevice(System.String nameOrLayout)
  RVA=-1  // generic def  token=0x60004B5  TDevice GetDevice()
  RVA=0x0B1D3560  token=0x60004B6  UnityEngine.InputSystem.InputDevice GetDevice(System.Type type)
  RVA=-1  // generic def  token=0x60004B7  TDevice GetDevice(UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=-1  // generic def  token=0x60004B8  TDevice GetDevice(System.String usage)
  RVA=0x033FE6A0  token=0x60004B9  UnityEngine.InputSystem.InputDevice GetDeviceById(System.Int32 deviceId)
  RVA=0x0B1D38C8  token=0x60004BA  System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputDeviceDescription> GetUnsupportedDevices()
  RVA=0x0B1D394C  token=0x60004BB  System.Int32 GetUnsupportedDevices(System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputDeviceDescription> descriptions)
  RVA=0x03DF9D80  token=0x60004BC  System.Void EnableDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1D32E0  token=0x60004BD  System.Void DisableDevice(UnityEngine.InputSystem.InputDevice device, System.Boolean keepSendingEvents)
  RVA=0x0B1D56AC  token=0x60004BE  System.Boolean TrySyncDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1D4FAC  token=0x60004BF  System.Void ResetDevice(UnityEngine.InputSystem.InputDevice device, System.Boolean alsoResetDontResetControls)
  RVA=0x0B1D5640  token=0x60004C0  System.Boolean TryResetDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1D3F18  token=0x60004C1  System.Void PauseHaptics()
  RVA=0x0B1D5150  token=0x60004C2  System.Void ResumeHaptics()
  RVA=0x0B1D502C  token=0x60004C3  System.Void ResetHaptics()
  RVA=0x0B1D5274  token=0x60004C4  System.Void SetDeviceUsage(UnityEngine.InputSystem.InputDevice device, System.String usage)
  RVA=0x0B1D52EC  token=0x60004C5  System.Void SetDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B1D2F34  token=0x60004C6  System.Void AddDeviceUsage(UnityEngine.InputSystem.InputDevice device, System.String usage)
  RVA=0x0B1D2EB8  token=0x60004C7  System.Void AddDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B1D4E48  token=0x60004C8  System.Void RemoveDeviceUsage(UnityEngine.InputSystem.InputDevice device, System.String usage)
  RVA=0x0B1D4DCC  token=0x60004C9  System.Void RemoveDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B1D3354  token=0x60004CA  UnityEngine.InputSystem.InputControl FindControl(System.String path)
  RVA=0x0B1D3494  token=0x60004CB  UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl> FindControls(System.String path)
  RVA=-1  // generic def  token=0x60004CC  UnityEngine.InputSystem.InputControlList<TControl> FindControls(System.String path)
  RVA=-1  // generic def  token=0x60004CD  System.Int32 FindControls(System.String path, UnityEngine.InputSystem.InputControlList<TControl>& controls)
  RVA=0x0B1D41D8  token=0x60004D2  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // generic def  token=0x60004D3  System.Void QueueEvent(TEvent& inputEvent)
  RVA=-1  // generic def  token=0x60004D4  System.Void QueueStateEvent(UnityEngine.InputSystem.InputDevice device, TState state, System.Double time)
  RVA=-1  // generic def  token=0x60004D5  System.Void QueueDeltaStateEvent(UnityEngine.InputSystem.InputControl control, TDelta delta, System.Double time)
  RVA=0x0B1D4038  token=0x60004D6  System.Void QueueConfigChangeEvent(UnityEngine.InputSystem.InputDevice device, System.Double time)
  RVA=0x0B1D42A4  token=0x60004D7  System.Void QueueTextEvent(UnityEngine.InputSystem.InputDevice device, System.Char character, System.Double time)
  RVA=0x0B1D5790  token=0x60004D8  System.Void Update()
  RVA=0x04B1EC20  token=0x60004D9  System.Void Update(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x0B1D45B4  token=0x60004E4  System.Void RegisterInteraction(System.Type type, System.String name)
  RVA=-1  // generic def  token=0x60004E5  System.Void RegisterInteraction(System.String name)
  RVA=0x0B1D54B0  token=0x60004E6  System.Type TryGetInteraction(System.String name)
  RVA=0x0B1D3C50  token=0x60004E7  System.Collections.Generic.IEnumerable<System.String> ListInteractions()
  RVA=0x0B1D445C  token=0x60004E8  System.Void RegisterBindingComposite(System.Type type, System.String name)
  RVA=-1  // generic def  token=0x60004E9  System.Void RegisterBindingComposite(System.String name)
  RVA=0x0B1D53E8  token=0x60004EA  System.Type TryGetBindingComposite(System.String name)
  RVA=0x0B1D32A0  token=0x60004EB  System.Void DisableAllEnabledActions()
  RVA=0x0B1D3BCC  token=0x60004EC  System.Collections.Generic.List<UnityEngine.InputSystem.InputAction> ListEnabledActions()
  RVA=0x0B1D3B30  token=0x60004ED  System.Int32 ListEnabledActions(System.Collections.Generic.List<UnityEngine.InputSystem.InputAction> actions)
  RVA=0x03DF6D10  token=0x60004F3  System.Void .cctor()
  RVA=0x04D5D0C0  token=0x60004F4  System.Void RunInitializeInPlayer()
  RVA=0x041E1670  token=0x60004F5  System.Void EnsureInitialized()
  RVA=0x03DF6D80  token=0x60004F6  System.Void InitializeInPlayer(UnityEngine.InputSystem.LowLevel.IInputRuntime runtime, UnityEngine.InputSystem.InputSettings settings)
  RVA=0x04B1EB80  token=0x60004F7  System.Void RunInitialUpdate()
  RVA=0x03DF6F50  token=0x60004F8  System.Void PerformDefaultPluginInitialization()
END_CLASS

CLASS: UnityEngine.InputSystem.CommonUsages
TYPE:  static class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPrimary2DMotion  // static @ 0x0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringSecondary2DMotion  // static @ 0x10
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPrimaryAction  // static @ 0x20
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringSecondaryAction  // static @ 0x30
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPrimaryTrigger  // static @ 0x40
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringSecondaryTrigger  // static @ 0x50
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringModifier  // static @ 0x60
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPosition  // static @ 0x70
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringOrientation  // static @ 0x80
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringHatswitch  // static @ 0x90
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringBack  // static @ 0xa0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringForward  // static @ 0xb0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringMenu  // static @ 0xc0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringSubmit  // static @ 0xd0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringCancel  // static @ 0xe0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringHorizontal  // static @ 0xf0
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringVertical  // static @ 0x100
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringTwist  // static @ 0x110
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPressure  // static @ 0x120
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringScrollHorizontal  // static @ 0x130
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringScrollVertical  // static @ 0x140
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringPoint  // static @ 0x150
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringLowFreqMotor  // static @ 0x160
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringHighFreqMotor  // static @ 0x170
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringLeftHand  // static @ 0x180
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringRightHand  // static @ 0x190
  public    static readonly UnityEngine.InputSystem.Utilities.InternedStringBatteryStrength  // static @ 0x1a0
METHODS:
  RVA=0x0B1C9A08  token=0x60004FD  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputControl
TYPE:  abstract class
TOKEN: 0x200006D
SIZE:  0xE0
FIELDS:
  protected internal        UnityEngine.InputSystem.LowLevel.InputStateBlockm_StateBlock  // 0x10
  private           UnityEngine.InputSystem.Utilities.InternedStringm_Name  // 0x20
  private           System.String                   m_Path  // 0x30
  private           System.String                   m_DisplayName  // 0x38
  private           System.String                   m_DisplayNameFromLayout  // 0x40
  private           System.String                   m_ShortDisplayName  // 0x48
  private           System.String                   m_ShortDisplayNameFromLayout  // 0x50
  private           UnityEngine.InputSystem.Utilities.InternedStringm_Layout  // 0x58
  private           UnityEngine.InputSystem.Utilities.InternedStringm_Variants  // 0x68
  private           UnityEngine.InputSystem.InputDevicem_Device  // 0x78
  private           UnityEngine.InputSystem.InputControlm_Parent  // 0x80
  private           System.Int32                    m_UsageCount  // 0x88
  private           System.Int32                    m_UsageStartIndex  // 0x8c
  private           System.Int32                    m_AliasCount  // 0x90
  private           System.Int32                    m_AliasStartIndex  // 0x94
  private           System.Int32                    m_ChildCount  // 0x98
  private           System.Int32                    m_ChildStartIndex  // 0x9c
  private           UnityEngine.InputSystem.InputControl.ControlFlagsm_ControlFlags  // 0xa0
  private           System.Boolean                  m_CachedValueIsStale  // 0xa4
  private           System.Boolean                  m_UnprocessedCachedValueIsStale  // 0xa5
  private           UnityEngine.InputSystem.Utilities.PrimitiveValuem_DefaultState  // 0xa8
  private           UnityEngine.InputSystem.Utilities.PrimitiveValuem_MinValue  // 0xb8
  private           UnityEngine.InputSystem.Utilities.PrimitiveValuem_MaxValue  // 0xc8
  private           UnityEngine.InputSystem.Utilities.FourCCm_OptimizedControlDataType  // 0xd8
PROPERTIES:
  name  get=0x05395154
  displayName  get=0x03DF9B90  set=0x046A91B0
  shortDisplayName  get=0x0B1D0C04  set=0x035A41A0
  path  get=0x0B1D0BB4
  layout  get=0x05395174
  variants  get=0x0B1D0D1C
  device  get=0x04D862A0
  parent  get=0x04D86260
  children  get=0x03AF2640
  usages  get=0x0B1D0CA4
  aliases  get=0x03AF04C0
  stateBlock  get=0x02B2D510
  noisy  get=0x030D12E0  set=0x0B1D0D3C
  synthetic  get=0x0B1D0C98  set=0x040D48E0
  Item  get=0x0B1D0AB0
  valueType  get=-1  // abstract
  valueSizeInBytes  get=-1  // abstract
  magnitude  get=0x05392E24
  currentStatePtr  get=0x030D11F0
  previousFrameStatePtr  get=0x0448E040
  defaultStatePtr  get=0x0B1D0B44
  noiseMaskPtr  get=0x0B1D0B7C
  stateOffsetRelativeToDeviceRoot  get=0x0B1D0C38
  optimizedControlDataType  get=0x04D8F100
  isSetupFinished  get=0x040D2980  set=0x040D09D0
  isButton  get=0x03529300  set=0x040D5630
  isConfigUpToDate  get=0x03DF9C40  set=0x03DF9C50
  dontReset  get=0x03AF0520  set=0x04A27DE0
  usesStateFromOtherControl  get=0x03AF0510  set=0x0B1D0E18
  hasDefaultState  get=0x0426F5B0
METHODS:
  RVA=0x0B1D08E4  token=0x6000514  System.String ToString()
  RVA=0x0B1CFF64  token=0x6000515  System.String DebuggerDisplay()
  RVA=0x0412A560  token=0x6000516  System.Single EvaluateMagnitude()
  RVA=0x04DA9E10  token=0x6000517  System.Single EvaluateMagnitude(System.Void* statePtr)
  RVA=-1  // abstract  token=0x6000518  System.Object ReadValueFromBufferAsObject(System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // abstract  token=0x6000519  System.Object ReadValueFromStateAsObject(System.Void* statePtr)
  RVA=-1  // abstract  token=0x600051A  System.Void ReadValueFromStateIntoBuffer(System.Void* statePtr, System.Void* bufferPtr, System.Int32 bufferSize)
  RVA=0x0B1D09C8  token=0x600051B  System.Void WriteValueFromBufferIntoState(System.Void* bufferPtr, System.Int32 bufferSize, System.Void* statePtr)
  RVA=0x0B1D0A3C  token=0x600051C  System.Void WriteValueFromObjectIntoState(System.Object value, System.Void* statePtr)
  RVA=-1  // abstract  token=0x600051D  System.Boolean CompareValue(System.Void* firstStatePtr, System.Void* secondStatePtr)
  RVA=0x0B1D0950  token=0x600051E  UnityEngine.InputSystem.InputControl TryGetChildControl(System.String path)
  RVA=-1  // generic def  token=0x600051F  TControl TryGetChildControl(System.String path)
  RVA=0x0B1D0424  token=0x6000520  UnityEngine.InputSystem.InputControl GetChildControl(System.String path)
  RVA=-1  // generic def  token=0x6000521  TControl GetChildControl(System.String path)
  RVA=0x04550310  token=0x6000522  System.Void .ctor()
  RVA=0x041E1670  token=0x6000523  System.Void FinishSetup()
  RVA=0x03DF9C10  token=0x6000524  System.Void RefreshConfigurationIfNeeded()
  RVA=0x041E1670  token=0x6000525  System.Void RefreshConfiguration()
  RVA=0x011EC580  token=0x600052C  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
  RVA=0x0B1CFE70  token=0x600052D  System.Void ApplyParameterChanges()
  RVA=0x0B1D0870  token=0x600052E  System.Void SetOptimizedControlDataType()
  RVA=0x0B1D0724  token=0x600052F  System.Void SetOptimizedControlDataTypeRecursively()
  RVA=0x0B1D0084  token=0x6000530  System.Void EnsureOptimizationTypeHasNotChanged()
  RVA=0x0B1CFEB4  token=0x600053C  System.Void CallFinishSetupRecursive()
  RVA=0x0B1D0548  token=0x600053D  System.String MakeChildPath(System.String path)
  RVA=0x03AF20F0  token=0x600053E  System.Void BakeOffsetIntoStateBlockRecursive(System.UInt32 offset)
  RVA=0x030D12A0  token=0x600053F  System.Int32 GetDeviceIndex()
  RVA=0x03529280  token=0x6000540  System.Boolean IsValueConsideredPressed(System.Single value)
  RVA=0x041E1670  token=0x6000541  System.Void AddProcessor(System.Object first)
  RVA=0x04DABDF0  token=0x6000542  System.Void MarkAsStale()
  RVA=0x0B1D05C4  token=0x6000543  System.Void MarkAsStaleRecursively()
END_CLASS

CLASS: UnityEngine.InputSystem.InputControl`1
TYPE:  abstract class
TOKEN: 0x200006F
EXTENDS: UnityEngine.InputSystem.InputControl
FIELDS:
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputProcessor<TValue>>m_ProcessorStack  // 0x0
  private           TValue                          m_CachedValue  // 0x0
  private           TValue                          m_UnprocessedCachedValue  // 0x0
  private           System.Boolean                  evaluateProcessorsEveryRead  // 0x0
PROPERTIES:
  valueType  get=-1  // not resolved
  valueSizeInBytes  get=-1  // not resolved
  value  get=-1  // not resolved
  unprocessedValue  get=-1  // not resolved
  processors  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000548  TValue ReadValue()
  RVA=-1  // not resolved  token=0x6000549  TValue ReadValueFromPreviousFrame()
  RVA=-1  // not resolved  token=0x600054A  TValue ReadDefaultValue()
  RVA=-1  // not resolved  token=0x600054B  TValue ReadValueFromState(System.Void* statePtr)
  RVA=-1  // not resolved  token=0x600054C  TValue ReadValueFromStateWithCaching(System.Void* statePtr)
  RVA=-1  // not resolved  token=0x600054D  TValue ReadUnprocessedValueFromStateWithCaching(System.Void* statePtr)
  RVA=-1  // not resolved  token=0x600054E  TValue ReadUnprocessedValue()
  RVA=-1  // abstract  token=0x600054F  TValue ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=-1  // not resolved  token=0x6000550  System.Object ReadValueFromStateAsObject(System.Void* statePtr)
  RVA=-1  // not resolved  token=0x6000551  System.Void ReadValueFromStateIntoBuffer(System.Void* statePtr, System.Void* bufferPtr, System.Int32 bufferSize)
  RVA=-1  // not resolved  token=0x6000552  System.Void WriteValueFromBufferIntoState(System.Void* bufferPtr, System.Int32 bufferSize, System.Void* statePtr)
  RVA=-1  // not resolved  token=0x6000553  System.Void WriteValueFromObjectIntoState(System.Object value, System.Void* statePtr)
  RVA=-1  // not resolved  token=0x6000554  System.Void WriteValueIntoState(TValue value, System.Void* statePtr)
  RVA=-1  // not resolved  token=0x6000555  System.Object ReadValueFromBufferAsObject(System.Void* buffer, System.Int32 bufferSize)
  RVA=-1  // not resolved  token=0x6000556  System.Boolean CompareValue(TValue& firstValue, TValue& secondValue)
  RVA=-1  // not resolved  token=0x6000557  System.Boolean CompareValue(System.Void* firstStatePtr, System.Void* secondStatePtr)
  RVA=-1  // not resolved  token=0x6000558  TValue ProcessValue(TValue value)
  RVA=-1  // not resolved  token=0x6000559  System.Void ProcessValue(TValue& value)
  RVA=-1  // generic def  token=0x600055A  TProcessor TryGetProcessor()
  RVA=-1  // not resolved  token=0x600055B  System.Void AddProcessor(System.Object processor)
  RVA=-1  // not resolved  token=0x600055C  System.Void FinishSetup()
  RVA=-1  // not resolved  token=0x600055E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputControlExtensions
TYPE:  static class
TOKEN: 0x2000070
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600055F  TControl FindInParentChain(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CD0A0  token=0x6000560  System.Boolean IsPressed(UnityEngine.InputSystem.InputControl control, System.Single buttonPressPoint)
  RVA=0x0B1CCFF0  token=0x6000561  System.Boolean IsActuated(UnityEngine.InputSystem.InputControl control, System.Single threshold)
  RVA=0x0B1CD23C  token=0x6000562  System.Object ReadValueAsObject(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CD348  token=0x6000563  System.Void ReadValueIntoBuffer(UnityEngine.InputSystem.InputControl control, System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1CD1C4  token=0x6000564  System.Object ReadDefaultValueAsObject(UnityEngine.InputSystem.InputControl control)
  RVA=-1  // generic def  token=0x6000565  TValue ReadValueFromEvent(UnityEngine.InputSystem.InputControl<TValue> control, UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent)
  RVA=-1  // generic def  token=0x6000566  System.Boolean ReadValueFromEvent(UnityEngine.InputSystem.InputControl<TValue> control, UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, TValue& value)
  RVA=0x0B1CD2B8  token=0x6000567  System.Object ReadValueFromEventAsObject(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent)
  RVA=-1  // generic def  token=0x6000568  TValue ReadUnprocessedValueFromEvent(UnityEngine.InputSystem.InputControl<TValue> control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // generic def  token=0x6000569  System.Boolean ReadUnprocessedValueFromEvent(UnityEngine.InputSystem.InputControl<TValue> control, UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, TValue& value)
  RVA=0x0B1CD600  token=0x600056A  System.Void WriteValueFromObjectIntoEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Object value)
  RVA=0x0B1CD6B0  token=0x600056B  System.Void WriteValueIntoState(UnityEngine.InputSystem.InputControl control, System.Void* statePtr)
  RVA=-1  // generic def  token=0x600056C  System.Void WriteValueIntoState(UnityEngine.InputSystem.InputControl control, TValue value, System.Void* statePtr)
  RVA=-1  // generic def  token=0x600056D  System.Void WriteValueIntoState(UnityEngine.InputSystem.InputControl<TValue> control, TValue value, System.Void* statePtr)
  RVA=-1  // generic def  token=0x600056E  System.Void WriteValueIntoState(UnityEngine.InputSystem.InputControl<TValue> control, System.Void* statePtr)
  RVA=-1  // generic def  token=0x600056F  System.Void WriteValueIntoState(UnityEngine.InputSystem.InputControl<TValue> control, TValue value, TState& state)
  RVA=-1  // generic def  token=0x6000570  System.Void WriteValueIntoEvent(UnityEngine.InputSystem.InputControl control, TValue value, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // generic def  token=0x6000571  System.Void WriteValueIntoEvent(UnityEngine.InputSystem.InputControl<TValue> control, TValue value, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B1CC440  token=0x6000572  System.Void CopyState(UnityEngine.InputSystem.InputDevice device, System.Void* buffer, System.Int32 bufferSizeInBytes)
  RVA=-1  // generic def  token=0x6000573  System.Void CopyState(UnityEngine.InputSystem.InputDevice device, TState& state)
  RVA=0x03529450  token=0x6000574  System.Boolean CheckStateIsAtDefault(UnityEngine.InputSystem.InputControl control)
  RVA=0x035294B0  token=0x6000575  System.Boolean CheckStateIsAtDefault(UnityEngine.InputSystem.InputControl control, System.Void* statePtr, System.Void* maskPtr)
  RVA=0x0B1CC0B0  token=0x6000576  System.Boolean CheckStateIsAtDefaultIgnoringNoise(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CC12C  token=0x6000577  System.Boolean CheckStateIsAtDefaultIgnoringNoise(UnityEngine.InputSystem.InputControl control, System.Void* statePtr)
  RVA=0x0B1CC24C  token=0x6000578  System.Boolean CompareStateIgnoringNoise(UnityEngine.InputSystem.InputControl control, System.Void* statePtr)
  RVA=0x03529550  token=0x6000579  System.Boolean CompareState(UnityEngine.InputSystem.InputControl control, System.Void* firstStatePtr, System.Void* secondStatePtr, System.Void* maskPtr)
  RVA=0x0B1CC348  token=0x600057A  System.Boolean CompareState(UnityEngine.InputSystem.InputControl control, System.Void* statePtr, System.Void* maskPtr)
  RVA=0x0B1CCF14  token=0x600057B  System.Boolean HasValueChangeInState(UnityEngine.InputSystem.InputControl control, System.Void* statePtr)
  RVA=0x0B1CCE20  token=0x600057C  System.Boolean HasValueChangeInEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B1CCD20  token=0x600057D  System.Void* GetStatePtrFromStateEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B1CCB00  token=0x600057E  System.Void* GetStatePtrFromStateEventUnchecked(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.Utilities.FourCC eventType)
  RVA=0x0B1CD438  token=0x600057F  System.Boolean ResetToDefaultStateInEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // generic def  token=0x6000580  System.Void QueueValueChange(UnityEngine.InputSystem.InputControl<TValue> control, TValue value, System.Double time)
  RVA=0x0B1CBBAC  token=0x6000581  System.Void AccumulateValueInEvent(UnityEngine.InputSystem.InputControl<System.Single> control, System.Void* currentStatePtr, UnityEngine.InputSystem.LowLevel.InputEventPtr newState)
  RVA=0x0B1CBCA0  token=0x6000582  System.Void AccumulateValueInEvent(UnityEngine.InputSystem.InputControl<UnityEngine.Vector2> control, System.Void* currentStatePtr, UnityEngine.InputSystem.LowLevel.InputEventPtr newState)
  RVA=-1  // generic def  token=0x6000583  System.Void FindControlsRecursive(UnityEngine.InputSystem.InputControl parent, System.Collections.Generic.IList<TControl> controls, System.Func<TControl,System.Boolean> predicate)
  RVA=0x0B1CBD98  token=0x6000584  System.String BuildPath(UnityEngine.InputSystem.InputControl control, System.String deviceLayout, System.Text.StringBuilder builder)
  RVA=0x0B1CC604  token=0x6000585  UnityEngine.InputSystem.InputControlExtensions.InputEventControlCollection EnumerateControls(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputControlExtensions.Enumerate flags, UnityEngine.InputSystem.InputDevice device, System.Single magnitudeThreshold)
  RVA=0x0B1CC5C0  token=0x6000586  UnityEngine.InputSystem.InputControlExtensions.InputEventControlCollection EnumerateChangedControls(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device, System.Single magnitudeThreshold)
  RVA=0x0B1CCE08  token=0x6000587  System.Boolean HasButtonPress(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Single magnitude, System.Boolean buttonControlsOnly)
  RVA=0x0B1CC904  token=0x6000588  UnityEngine.InputSystem.InputControl GetFirstButtonPressOrNull(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Single magnitude, System.Boolean buttonControlsOnly)
  RVA=0x0B1CC87C  token=0x6000589  System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControl> GetAllButtonPresses(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Single magnitude, System.Boolean buttonControlsOnly)
  RVA=0x040D2930  token=0x600058A  UnityEngine.InputSystem.InputControlExtensions.ControlBuilder Setup(UnityEngine.InputSystem.InputControl control)
  RVA=0x040D27F0  token=0x600058B  UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder Setup(UnityEngine.InputSystem.InputDevice device, System.Int32 controlCount, System.Int32 usageCount, System.Int32 aliasCount)
END_CLASS

CLASS: UnityEngine.InputSystem.InputControlLayoutChange
TYPE:  sealed struct
TOKEN: 0x2000077
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputControlLayoutChangeAdded  // const
  public    static  UnityEngine.InputSystem.InputControlLayoutChangeRemoved  // const
  public    static  UnityEngine.InputSystem.InputControlLayoutChangeReplaced  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputControlList`1
TYPE:  sealed struct
TOKEN: 0x2000078
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IReadOnlyCollection`1 System.IDisposable
FIELDS:
  private           System.Int32                    m_Count  // 0x0
  private           Unity.Collections.NativeArray<System.UInt64>m_Indices  // 0x0
  private   readonly Unity.Collections.Allocator     m_Allocator  // 0x0
  private   static  System.UInt64                   kInvalidIndex  // const
PROPERTIES:
  Count  get=-1  // not resolved
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60005C9  System.Void .ctor(Unity.Collections.Allocator allocator, System.Int32 initialCapacity)
  RVA=-1  // not resolved  token=0x60005CA  System.Void .ctor(System.Collections.Generic.IEnumerable<TControl> values, Unity.Collections.Allocator allocator)
  RVA=-1  // not resolved  token=0x60005CB  System.Void .ctor(TControl[] values)
  RVA=-1  // not resolved  token=0x60005CC  System.Void Resize(System.Int32 size)
  RVA=-1  // not resolved  token=0x60005CD  System.Void Add(TControl item)
  RVA=-1  // generic def  token=0x60005CE  System.Void AddSlice(TList list, System.Int32 count, System.Int32 destinationIndex, System.Int32 sourceIndex)
  RVA=-1  // not resolved  token=0x60005CF  System.Void AddRange(System.Collections.Generic.IEnumerable<TControl> list, System.Int32 count, System.Int32 destinationIndex)
  RVA=-1  // not resolved  token=0x60005D0  System.Boolean Remove(TControl item)
  RVA=-1  // not resolved  token=0x60005D1  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60005D2  System.Void CopyTo(TControl[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x60005D3  System.Int32 IndexOf(TControl item)
  RVA=-1  // not resolved  token=0x60005D4  System.Int32 IndexOf(TControl item, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // not resolved  token=0x60005D5  System.Void Insert(System.Int32 index, TControl item)
  RVA=-1  // not resolved  token=0x60005D6  System.Void Clear()
  RVA=-1  // not resolved  token=0x60005D7  System.Boolean Contains(TControl item)
  RVA=-1  // not resolved  token=0x60005D8  System.Boolean Contains(TControl item, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // not resolved  token=0x60005D9  System.Void SwapElements(System.Int32 index1, System.Int32 index2)
  RVA=-1  // generic def  token=0x60005DA  System.Void Sort(System.Int32 startIndex, System.Int32 count, TCompare comparer)
  RVA=-1  // not resolved  token=0x60005DB  TControl[] ToArray(System.Boolean dispose)
  RVA=-1  // not resolved  token=0x60005DC  System.Void AppendTo(TControl[]& array, System.Int32& count)
  RVA=-1  // not resolved  token=0x60005DD  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60005DE  System.Collections.Generic.IEnumerator<TControl> GetEnumerator()
  RVA=-1  // not resolved  token=0x60005DF  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60005E0  System.String ToString()
  RVA=-1  // not resolved  token=0x60005E1  System.UInt64 ToIndex(TControl control)
  RVA=-1  // not resolved  token=0x60005E2  TControl FromIndex(System.UInt64 index)
END_CLASS

CLASS: UnityEngine.InputSystem.InputControlPath
TYPE:  static class
TOKEN: 0x200007A
SIZE:  0x10
FIELDS:
  public    static  System.String                   Wildcard  // const
  public    static  System.String                   DoubleWildcard  // const
  public    static  System.Char                     Separator  // const
  private   static  System.Char                     SeparatorReplacement  // const
METHODS:
  RVA=0x0B1CD874  token=0x60005E9  System.String CleanSlashes(System.String pathComponent)
  RVA=0x0B1CD89C  token=0x60005EA  System.String Combine(UnityEngine.InputSystem.InputControl parent, System.String path)
  RVA=0x0B1CE5BC  token=0x60005EB  System.String ToHumanReadableString(System.String path, UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions options, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CE5F0  token=0x60005EC  System.String ToHumanReadableString(System.String path, System.String& deviceLayoutName, System.String& controlPath, UnityEngine.InputSystem.InputControlPath.HumanReadableStringOptions options, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CF578  token=0x60005ED  System.String[] TryGetDeviceUsages(System.String path)
  RVA=0x0B1CF464  token=0x60005EE  System.String TryGetDeviceLayout(System.String path)
  RVA=0x0B1CF244  token=0x60005EF  System.String TryGetControlLayout(System.String path)
  RVA=0x0B1CDCD0  token=0x60005F0  System.String FindControlLayoutRecursive(UnityEngine.InputSystem.InputControlPath.PathParser& parser, System.String layoutName)
  RVA=0x0B1CDAF0  token=0x60005F1  System.String FindControlLayoutRecursive(UnityEngine.InputSystem.InputControlPath.PathParser& parser, UnityEngine.InputSystem.Layouts.InputControlLayout layout)
  RVA=0x0B1CD964  token=0x60005F2  System.Boolean ControlLayoutMatchesPathComponent(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem, UnityEngine.InputSystem.InputControlPath.PathParser& parser)
  RVA=0x0B1CE404  token=0x60005F3  System.Boolean StringMatches(UnityEngine.InputSystem.Utilities.Substring str, UnityEngine.InputSystem.Utilities.InternedString matchTo)
  RVA=0x0B1CF040  token=0x60005F4  UnityEngine.InputSystem.InputControl TryFindControl(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath)
  RVA=0x0B1CF108  token=0x60005F5  UnityEngine.InputSystem.InputControl[] TryFindControls(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath)
  RVA=0x0B1CF098  token=0x60005F6  System.Int32 TryFindControls(UnityEngine.InputSystem.InputControl control, System.String path, UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputControl>& matches, System.Int32 indexInPath)
  RVA=-1  // generic def  token=0x60005F7  TControl TryFindControl(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath)
  RVA=-1  // generic def  token=0x60005F8  System.Int32 TryFindControls(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath, UnityEngine.InputSystem.InputControlList<TControl>& matches)
  RVA=0x0B1CEFE8  token=0x60005F9  UnityEngine.InputSystem.InputControl TryFindChild(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath)
  RVA=-1  // generic def  token=0x60005FA  TControl TryFindChild(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath)
  RVA=0x0B1CE280  token=0x60005FB  System.Boolean Matches(System.String expected, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CDE44  token=0x60005FC  System.Boolean MatchControlComponent(UnityEngine.InputSystem.InputControlPath.ParsedPathComponent& expectedControlComponent, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem, System.Boolean matchAlias)
  RVA=0x0B1CE0EC  token=0x60005FD  System.Boolean MatchesPrefix(System.String expected, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1CE210  token=0x60005FE  System.Boolean MatchesRecursive(UnityEngine.InputSystem.InputControlPath.PathParser& parser, UnityEngine.InputSystem.InputControl currentControl, System.Boolean prefixOnly)
  RVA=-1  // generic def  token=0x60005FF  TControl MatchControlsRecursive(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath, UnityEngine.InputSystem.InputControlList<TControl>& matches, System.Boolean matchMultiple)
  RVA=-1  // generic def  token=0x6000600  TControl MatchByUsageAtDeviceRootRecursive(UnityEngine.InputSystem.InputDevice device, System.String path, System.Int32 indexInPath, UnityEngine.InputSystem.InputControlList<TControl>& matches, System.Boolean matchMultiple)
  RVA=-1  // generic def  token=0x6000601  TControl MatchChildrenRecursive(UnityEngine.InputSystem.InputControl control, System.String path, System.Int32 indexInPath, UnityEngine.InputSystem.InputControlList<TControl>& matches, System.Boolean matchMultiple)
  RVA=0x03AF0220  token=0x6000602  System.Boolean MatchPathComponent(System.String component, System.String path, System.Int32& indexInPath, UnityEngine.InputSystem.InputControlPath.PathComponentType componentType, System.Int32 startIndexInComponent)
  RVA=0x0454BF40  token=0x6000603  System.Boolean PathComponentCanYieldMultipleMatches(System.String path, System.Int32 indexInPath)
  RVA=0x0B1CE394  token=0x6000604  System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControlPath.ParsedPathComponent> Parse(System.String path)
END_CLASS

CLASS: UnityEngine.InputSystem.InputProcessor
TYPE:  abstract class
TOKEN: 0x2000082
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.InputSystem.Utilities.TypeTables_Processors  // static @ 0x0
PROPERTIES:
  cachingPolicy  get=0x011EC580
METHODS:
  RVA=-1  // abstract  token=0x6000621  System.Object ProcessAsObject(System.Object value, UnityEngine.InputSystem.InputControl control)
  RVA=-1  // abstract  token=0x6000622  System.Void Process(System.Void* buffer, System.Int32 bufferSize, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1D2DE4  token=0x6000623  System.Type GetValueTypeFromType(System.Type processorType)
  RVA=0x041E1670  token=0x6000625  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputProcessor`1
TYPE:  abstract class
TOKEN: 0x2000084
EXTENDS: UnityEngine.InputSystem.InputProcessor
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000626  TValue Process(TValue value, UnityEngine.InputSystem.InputControl control)
  RVA=-1  // not resolved  token=0x6000627  System.Object ProcessAsObject(System.Object value, UnityEngine.InputSystem.InputControl control)
  RVA=-1  // not resolved  token=0x6000628  System.Void Process(System.Void* buffer, System.Int32 bufferSize, UnityEngine.InputSystem.InputControl control)
  RVA=-1  // not resolved  token=0x6000629  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Gamepad
TYPE:  class
TOKEN: 0x2000085
SIZE:  0x210
EXTENDS: UnityEngine.InputSystem.InputDevice
IMPLEMENTS: UnityEngine.InputSystem.Haptics.IDualMotorRumble UnityEngine.InputSystem.Haptics.IHaptics
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<buttonWest>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.ButtonControl<buttonNorth>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.ButtonControl<buttonSouth>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.ButtonControl<buttonEast>k__BackingField  // 0x1a8
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftStickButton>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightStickButton>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.ButtonControl<startButton>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<selectButton>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.DpadControl<dpad>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftShoulder>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightShoulder>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.StickControl<leftStick>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.StickControl<rightStick>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftTrigger>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightTrigger>k__BackingField  // 0x200
  private   static  UnityEngine.InputSystem.Gamepad <current>k__BackingField  // static @ 0x0
  private           UnityEngine.InputSystem.Haptics.DualMotorRumblem_Rumble  // 0x208
  private   static  System.Int32                    s_GamepadCount  // static @ 0x8
  private   static  UnityEngine.InputSystem.Gamepad[]s_Gamepads  // static @ 0x10
PROPERTIES:
  buttonWest  get=0x04D8DD50  set=0x0539722C
  buttonNorth  get=0x04D8DED0  set=0x05397218
  buttonSouth  get=0x04D8DA10  set=0x05397204
  buttonEast  get=0x04D8D9C0  set=0x053971F0
  leftStickButton  get=0x04D8D960  set=0x053971DC
  rightStickButton  get=0x04D8DA90  set=0x05397240
  startButton  get=0x04D8DA80  set=0x06B8B42C
  selectButton  get=0x04D8D790  set=0x06B8B0D0
  dpad  get=0x04D8D7B0  set=0x06B8B0F8
  leftShoulder  get=0x04D87870  set=0x06B8B134
  rightShoulder  get=0x04D8D930  set=0x06B8B2C4
  leftStick  get=0x04D87930  set=0x06799DA0
  rightStick  get=0x04D87940  set=0x06799DB4
  leftTrigger  get=0x04D87920  set=0x06799D8C
  rightTrigger  get=0x04D8DA40  set=0x06B8B3DC
  aButton  get=0x04D8DA10
  bButton  get=0x04D8D9C0
  xButton  get=0x04D8DD50
  yButton  get=0x04D8DED0
  triangleButton  get=0x04D8DED0
  squareButton  get=0x04D8DD50
  circleButton  get=0x04D8D9C0
  crossButton  get=0x04D8DA10
  Item  get=0x0B1CB56C
  current  get=0x0B1CB7F8  set=0x0B1CB830
  all  get=0x0B1CB788
METHODS:
  RVA=0x0B1CB080  token=0x6000654  System.Void FinishSetup()
  RVA=0x0B1CB3E4  token=0x6000655  System.Void MakeCurrent()
  RVA=0x0B1CB3EC  token=0x6000656  System.Void OnAdded()
  RVA=0x0B1CB45C  token=0x6000657  System.Void OnRemoved()
  RVA=0x0B1CB508  token=0x6000658  System.Void PauseHaptics()
  RVA=0x0B1CB530  token=0x6000659  System.Void ResumeHaptics()
  RVA=0x0B1CB51C  token=0x600065A  System.Void ResetHaptics()
  RVA=0x0B1CB544  token=0x600065B  System.Void SetMotorSpeeds(System.Single lowFrequency, System.Single highFrequency)
  RVA=0x053971D4  token=0x600065C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputDevice
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x190
EXTENDS: UnityEngine.InputSystem.InputControl
FIELDS:
  public    static  System.Int32                    InvalidDeviceId  // const
  private   static  System.Int32                    kLocalParticipantId  // const
  private   static  System.Int32                    kInvalidDeviceIndex  // const
  private           UnityEngine.InputSystem.InputDevice.DeviceFlagsm_DeviceFlags  // 0xe0
  private           System.Int32                    m_DeviceId  // 0xe4
  private           System.Int32                    m_ParticipantId  // 0xe8
  private           System.Int32                    m_DeviceIndex  // 0xec
  private           System.UInt32                   m_CurrentProcessedEventBytesOnUpdate  // 0xf0
  private           UnityEngine.InputSystem.Layouts.InputDeviceDescriptionm_Description  // 0xf8
  private           System.Double                   m_LastUpdateTimeInternal  // 0x130
  private           System.UInt32                   m_CurrentUpdateStepCount  // 0x138
  private           UnityEngine.InputSystem.Utilities.InternedString[]m_AliasesForEachControl  // 0x140
  private           UnityEngine.InputSystem.Utilities.InternedString[]m_UsagesForEachControl  // 0x148
  private           UnityEngine.InputSystem.InputControl[]m_UsageToControl  // 0x150
  private           UnityEngine.InputSystem.InputControl[]m_ChildrenForEachControl  // 0x158
  private           System.Collections.Generic.HashSet<System.Int32>m_UpdatedButtons  // 0x160
  private           System.Collections.Generic.List<UnityEngine.InputSystem.Controls.ButtonControl>m_ButtonControlsCheckingPressState  // 0x168
  private           System.Boolean                  m_UseCachePathForButtonPresses  // 0x170
  private           System.UInt32[]                 m_StateOffsetToControlMap  // 0x178
  private           UnityEngine.InputSystem.InputDevice.ControlBitRangeNode[]m_ControlTreeNodes  // 0x180
  private           System.UInt16[]                 m_ControlTreeIndices  // 0x188
  private   static  System.Int32                    kControlIndexBits  // const
  private   static  System.Int32                    kStateOffsetBits  // const
  private   static  System.Int32                    kStateSizeBits  // const
PROPERTIES:
  description  get=0x04DABE30
  enabled  get=0x030CDAC0
  canRunInBackground  get=0x05393A9C
  canDeviceRunInBackground  get=0x04750930
  added  get=0x0381FC40
  remote  get=0x0B1D1EB8
  native  get=0x04D1F000
  updateBeforeRender  get=0x03AE8890
  deviceId  get=0x04DA3670
  lastUpdateTime  get=0x0B1D1E70
  wasUpdatedThisFrame  get=0x04664C40
  allControls  get=0x04461DF0
  valueType  get=0x0B1D1F10
  valueSizeInBytes  get=0x0B1D1EC4
  all  get=0x0B1D1E20
  disabledInFrontend  get=0x03820730  set=0x0B1D1F60
  disabledInRuntime  get=0x03820720  set=0x038202B0
  disabledWhileInBackground  get=0x038202A0  set=0x0381FF80
  hasControlsWithDefaultState  get=0x04D80970  set=0x04D30E00
  hasDontResetControls  get=0x0381FC70  set=0x04A27E00
  hasStateCallbacks  get=0x030D1A50  set=0x0B1D1F80
  hasEventMerger  get=0x030CDBE0  set=0x03AE82F0
  hasEventPreProcessor  get=0x041D18C0  set=0x03AE8310
METHODS:
  RVA=0x0B1D1DF4  token=0x600066C  System.Void .ctor()
  RVA=0x0B1D1640  token=0x600066D  System.Object ReadValueFromBufferAsObject(System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1D168C  token=0x600066E  System.Object ReadValueFromStateAsObject(System.Void* statePtr)
  RVA=0x0B1D176C  token=0x600066F  System.Void ReadValueFromStateIntoBuffer(System.Void* statePtr, System.Void* bufferPtr, System.Int32 bufferSize)
  RVA=0x0B1D0F38  token=0x6000670  System.Boolean CompareValue(System.Void* firstStatePtr, System.Void* secondStatePtr)
  RVA=0x0B1D15C8  token=0x6000671  System.Void NotifyConfigurationChanged()
  RVA=0x041E1670  token=0x6000672  System.Void MakeCurrent()
  RVA=0x041E1670  token=0x6000673  System.Void OnAdded()
  RVA=0x041E1670  token=0x6000674  System.Void OnRemoved()
  RVA=0x041E1670  token=0x6000675  System.Void OnConfigurationChanged()
  RVA=-1  // generic def  token=0x6000676  System.Int64 ExecuteCommand(TCommand& command)
  RVA=0x04320670  token=0x6000677  System.Int64 ExecuteCommand(UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr)
  RVA=0x030CDB50  token=0x6000678  System.Boolean QueryEnabledStateFromRuntime()
  RVA=0x04DABE20  token=0x600067F  System.UInt32 EncodeStateOffsetToControlMapEntry(System.UInt32 controlIndex, System.UInt32 stateOffsetInBits, System.UInt32 stateSizeInBits)
  RVA=0x04DABE00  token=0x6000680  System.Void DecodeStateOffsetToControlMapEntry(System.UInt32 entry, System.UInt32& controlIndex, System.UInt32& stateOffset, System.UInt32& stateSize)
  RVA=0x0B1D0E38  token=0x600068B  System.Void AddDeviceUsage(UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B1D1964  token=0x600068C  System.Void RemoveDeviceUsage(UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B1D0EE0  token=0x600068D  System.Void ClearDeviceUsages()
  RVA=0x038208A0  token=0x600068E  System.Boolean RequestSync()
  RVA=0x0B1D1A34  token=0x600068F  System.Boolean RequestReset()
  RVA=0x03820820  token=0x6000690  System.Boolean ExecuteEnableCommand()
  RVA=0x038207A0  token=0x6000691  System.Boolean ExecuteDisableCommand()
  RVA=0x053950CC  token=0x6000692  System.Void NotifyAdded()
  RVA=0x0B1D1630  token=0x6000693  System.Void NotifyRemoved()
  RVA=-1  // generic def  token=0x6000694  TDevice Build(System.String layoutName, System.String layoutVariants, UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription, System.Boolean noPrecompiledLayouts)
  RVA=0x041EFF10  token=0x6000695  System.Void WriteChangedControlStates(System.Byte* deviceStateBuffer, System.Void* statePtr, System.UInt32 stateSizeInBytes, System.UInt32 stateOffsetInDevice)
  RVA=0x0B1D1AA8  token=0x6000696  System.Void WritePartialChangedControlStatesInternal(System.UInt32 stateSizeInBits, System.UInt32 stateOffsetInDeviceInBits, UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, System.UInt32 startOffset)
  RVA=0x0B1D1070  token=0x6000697  System.Void DumpControlBitRangeNode(System.Int32 nodeIndex, UnityEngine.InputSystem.InputDevice.ControlBitRangeNode node, System.UInt32 startOffset, System.UInt32 sizeInBits, System.Collections.Generic.List<System.String> output)
  RVA=0x0B1D138C  token=0x6000698  System.Void DumpControlTree(UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, System.UInt32 startOffset, System.Collections.Generic.List<System.String> output)
  RVA=0x0B1D14F4  token=0x6000699  System.String DumpControlTree()
  RVA=0x035298C0  token=0x600069A  System.Void WriteChangedControlStatesInternal(System.Void* statePtr, System.Byte* deviceStatePtr, UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, System.UInt32 startOffset)
  RVA=0x03529D30  token=0x600069B  System.Boolean HasDataChangedInRange(System.Byte* deviceStatePtr, System.Void* statePtr, System.UInt32 startOffset, System.UInt32 sizeInBits)
END_CLASS

CLASS: UnityEngine.InputSystem.InputDeviceChange
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.InputDeviceChangeAdded  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeRemoved  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeDisconnected  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeReconnected  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeEnabled  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeDisabled  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeUsageChanged  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeConfigurationChanged  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeSoftReset  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeHardReset  // const
  public    static  UnityEngine.InputSystem.InputDeviceChangeDestroyed  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Joystick
TYPE:  class
TOKEN: 0x200008A
SIZE:  0x1B0
EXTENDS: UnityEngine.InputSystem.InputDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<trigger>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.StickControl<stick>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.AxisControl<twist>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.Vector2Control<hatswitch>k__BackingField  // 0x1a8
  private   static  UnityEngine.InputSystem.Joystick<current>k__BackingField  // static @ 0x0
  private   static  System.Int32                    s_JoystickCount  // static @ 0x8
  private   static  UnityEngine.InputSystem.Joystick[]s_Joysticks  // static @ 0x10
PROPERTIES:
  trigger  get=0x04D8DD50  set=0x0539722C
  stick  get=0x04D8DED0  set=0x05397218
  twist  get=0x04D8DA10  set=0x05397204
  hatswitch  get=0x04D8D9C0  set=0x053971F0
  current  get=0x0B1D95A0  set=0x0B1D95D8
  all  get=0x0B1D9530
METHODS:
  RVA=0x0B1D92C4  token=0x60006A8  System.Void FinishSetup()
  RVA=0x0B1D93E8  token=0x60006A9  System.Void MakeCurrent()
  RVA=0x0B1D93F0  token=0x60006AA  System.Void OnAdded()
  RVA=0x0B1D9460  token=0x60006AB  System.Void OnRemoved()
  RVA=0x053971D4  token=0x60006AC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Key
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Key     None  // const
  public    static  UnityEngine.InputSystem.Key     Space  // const
  public    static  UnityEngine.InputSystem.Key     Enter  // const
  public    static  UnityEngine.InputSystem.Key     Tab  // const
  public    static  UnityEngine.InputSystem.Key     Backquote  // const
  public    static  UnityEngine.InputSystem.Key     Quote  // const
  public    static  UnityEngine.InputSystem.Key     Semicolon  // const
  public    static  UnityEngine.InputSystem.Key     Comma  // const
  public    static  UnityEngine.InputSystem.Key     Period  // const
  public    static  UnityEngine.InputSystem.Key     Slash  // const
  public    static  UnityEngine.InputSystem.Key     Backslash  // const
  public    static  UnityEngine.InputSystem.Key     LeftBracket  // const
  public    static  UnityEngine.InputSystem.Key     RightBracket  // const
  public    static  UnityEngine.InputSystem.Key     Minus  // const
  public    static  UnityEngine.InputSystem.Key     Equals  // const
  public    static  UnityEngine.InputSystem.Key     A  // const
  public    static  UnityEngine.InputSystem.Key     B  // const
  public    static  UnityEngine.InputSystem.Key     C  // const
  public    static  UnityEngine.InputSystem.Key     D  // const
  public    static  UnityEngine.InputSystem.Key     E  // const
  public    static  UnityEngine.InputSystem.Key     F  // const
  public    static  UnityEngine.InputSystem.Key     G  // const
  public    static  UnityEngine.InputSystem.Key     H  // const
  public    static  UnityEngine.InputSystem.Key     I  // const
  public    static  UnityEngine.InputSystem.Key     J  // const
  public    static  UnityEngine.InputSystem.Key     K  // const
  public    static  UnityEngine.InputSystem.Key     L  // const
  public    static  UnityEngine.InputSystem.Key     M  // const
  public    static  UnityEngine.InputSystem.Key     N  // const
  public    static  UnityEngine.InputSystem.Key     O  // const
  public    static  UnityEngine.InputSystem.Key     P  // const
  public    static  UnityEngine.InputSystem.Key     Q  // const
  public    static  UnityEngine.InputSystem.Key     R  // const
  public    static  UnityEngine.InputSystem.Key     S  // const
  public    static  UnityEngine.InputSystem.Key     T  // const
  public    static  UnityEngine.InputSystem.Key     U  // const
  public    static  UnityEngine.InputSystem.Key     V  // const
  public    static  UnityEngine.InputSystem.Key     W  // const
  public    static  UnityEngine.InputSystem.Key     X  // const
  public    static  UnityEngine.InputSystem.Key     Y  // const
  public    static  UnityEngine.InputSystem.Key     Z  // const
  public    static  UnityEngine.InputSystem.Key     Digit1  // const
  public    static  UnityEngine.InputSystem.Key     Digit2  // const
  public    static  UnityEngine.InputSystem.Key     Digit3  // const
  public    static  UnityEngine.InputSystem.Key     Digit4  // const
  public    static  UnityEngine.InputSystem.Key     Digit5  // const
  public    static  UnityEngine.InputSystem.Key     Digit6  // const
  public    static  UnityEngine.InputSystem.Key     Digit7  // const
  public    static  UnityEngine.InputSystem.Key     Digit8  // const
  public    static  UnityEngine.InputSystem.Key     Digit9  // const
  public    static  UnityEngine.InputSystem.Key     Digit0  // const
  public    static  UnityEngine.InputSystem.Key     LeftShift  // const
  public    static  UnityEngine.InputSystem.Key     RightShift  // const
  public    static  UnityEngine.InputSystem.Key     LeftAlt  // const
  public    static  UnityEngine.InputSystem.Key     RightAlt  // const
  public    static  UnityEngine.InputSystem.Key     AltGr  // const
  public    static  UnityEngine.InputSystem.Key     LeftCtrl  // const
  public    static  UnityEngine.InputSystem.Key     RightCtrl  // const
  public    static  UnityEngine.InputSystem.Key     LeftMeta  // const
  public    static  UnityEngine.InputSystem.Key     RightMeta  // const
  public    static  UnityEngine.InputSystem.Key     LeftWindows  // const
  public    static  UnityEngine.InputSystem.Key     RightWindows  // const
  public    static  UnityEngine.InputSystem.Key     LeftApple  // const
  public    static  UnityEngine.InputSystem.Key     RightApple  // const
  public    static  UnityEngine.InputSystem.Key     LeftCommand  // const
  public    static  UnityEngine.InputSystem.Key     RightCommand  // const
  public    static  UnityEngine.InputSystem.Key     ContextMenu  // const
  public    static  UnityEngine.InputSystem.Key     Escape  // const
  public    static  UnityEngine.InputSystem.Key     LeftArrow  // const
  public    static  UnityEngine.InputSystem.Key     RightArrow  // const
  public    static  UnityEngine.InputSystem.Key     UpArrow  // const
  public    static  UnityEngine.InputSystem.Key     DownArrow  // const
  public    static  UnityEngine.InputSystem.Key     Backspace  // const
  public    static  UnityEngine.InputSystem.Key     PageDown  // const
  public    static  UnityEngine.InputSystem.Key     PageUp  // const
  public    static  UnityEngine.InputSystem.Key     Home  // const
  public    static  UnityEngine.InputSystem.Key     End  // const
  public    static  UnityEngine.InputSystem.Key     Insert  // const
  public    static  UnityEngine.InputSystem.Key     Delete  // const
  public    static  UnityEngine.InputSystem.Key     CapsLock  // const
  public    static  UnityEngine.InputSystem.Key     NumLock  // const
  public    static  UnityEngine.InputSystem.Key     PrintScreen  // const
  public    static  UnityEngine.InputSystem.Key     ScrollLock  // const
  public    static  UnityEngine.InputSystem.Key     Pause  // const
  public    static  UnityEngine.InputSystem.Key     NumpadEnter  // const
  public    static  UnityEngine.InputSystem.Key     NumpadDivide  // const
  public    static  UnityEngine.InputSystem.Key     NumpadMultiply  // const
  public    static  UnityEngine.InputSystem.Key     NumpadPlus  // const
  public    static  UnityEngine.InputSystem.Key     NumpadMinus  // const
  public    static  UnityEngine.InputSystem.Key     NumpadPeriod  // const
  public    static  UnityEngine.InputSystem.Key     NumpadEquals  // const
  public    static  UnityEngine.InputSystem.Key     Numpad0  // const
  public    static  UnityEngine.InputSystem.Key     Numpad1  // const
  public    static  UnityEngine.InputSystem.Key     Numpad2  // const
  public    static  UnityEngine.InputSystem.Key     Numpad3  // const
  public    static  UnityEngine.InputSystem.Key     Numpad4  // const
  public    static  UnityEngine.InputSystem.Key     Numpad5  // const
  public    static  UnityEngine.InputSystem.Key     Numpad6  // const
  public    static  UnityEngine.InputSystem.Key     Numpad7  // const
  public    static  UnityEngine.InputSystem.Key     Numpad8  // const
  public    static  UnityEngine.InputSystem.Key     Numpad9  // const
  public    static  UnityEngine.InputSystem.Key     F1  // const
  public    static  UnityEngine.InputSystem.Key     F2  // const
  public    static  UnityEngine.InputSystem.Key     F3  // const
  public    static  UnityEngine.InputSystem.Key     F4  // const
  public    static  UnityEngine.InputSystem.Key     F5  // const
  public    static  UnityEngine.InputSystem.Key     F6  // const
  public    static  UnityEngine.InputSystem.Key     F7  // const
  public    static  UnityEngine.InputSystem.Key     F8  // const
  public    static  UnityEngine.InputSystem.Key     F9  // const
  public    static  UnityEngine.InputSystem.Key     F10  // const
  public    static  UnityEngine.InputSystem.Key     F11  // const
  public    static  UnityEngine.InputSystem.Key     F12  // const
  public    static  UnityEngine.InputSystem.Key     OEM1  // const
  public    static  UnityEngine.InputSystem.Key     OEM2  // const
  public    static  UnityEngine.InputSystem.Key     OEM3  // const
  public    static  UnityEngine.InputSystem.Key     OEM4  // const
  public    static  UnityEngine.InputSystem.Key     OEM5  // const
  public    static  UnityEngine.InputSystem.Key     IMESelected  // const
  public    static  UnityEngine.InputSystem.Key     F13  // const
  public    static  UnityEngine.InputSystem.Key     F14  // const
  public    static  UnityEngine.InputSystem.Key     F15  // const
  public    static  UnityEngine.InputSystem.Key     F16  // const
  public    static  UnityEngine.InputSystem.Key     F17  // const
  public    static  UnityEngine.InputSystem.Key     F18  // const
  public    static  UnityEngine.InputSystem.Key     F19  // const
  public    static  UnityEngine.InputSystem.Key     F20  // const
  public    static  UnityEngine.InputSystem.Key     F21  // const
  public    static  UnityEngine.InputSystem.Key     F22  // const
  public    static  UnityEngine.InputSystem.Key     F23  // const
  public    static  UnityEngine.InputSystem.Key     F24  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.KeyEx
TYPE:  static class
TOKEN: 0x200008C
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.InputSystem.Key     IMESelected  // const
  private   static  UnityEngine.InputSystem.Key     RemappedIMESelected  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Keyboard
TYPE:  class
TOKEN: 0x200008D
SIZE:  0x1F8
EXTENDS: UnityEngine.InputSystem.InputDevice
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.ITextInputReceiver UnityEngine.InputSystem.LowLevel.IEventPreProcessor
FIELDS:
  public    static  System.Int32                    KeyCount  // const
  private   static  System.Int32                    ExtendedKeyCount  // const
  private           UnityEngine.InputSystem.Controls.AnyKeyControl<anyKey>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.ButtonControl<shiftKey>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.ButtonControl<ctrlKey>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.ButtonControl<altKey>k__BackingField  // 0x1a8
  private           UnityEngine.InputSystem.Controls.ButtonControl<imeSelected>k__BackingField  // 0x1b0
  private   static  UnityEngine.InputSystem.Keyboard<current>k__BackingField  // static @ 0x0
  private           UnityEngine.InputSystem.Utilities.InlinedArray<System.Action<System.Char>>m_TextInputListeners  // 0x1b8
  private           System.String                   m_KeyboardLayoutName  // 0x1d0
  private           UnityEngine.InputSystem.Controls.KeyControl[]m_Keys  // 0x1d8
  private           UnityEngine.InputSystem.Utilities.InlinedArray<System.Action<UnityEngine.InputSystem.LowLevel.IMECompositionString>>m_ImeCompositionListeners  // 0x1e0
PROPERTIES:
  keyboardLayout  get=0x0B1DB0D0  set=0x06B8B0F8
  anyKey  get=0x04D8DD50  set=0x0539722C
  spaceKey  get=0x0B1DB440
  enterKey  get=0x0B1DAEA0
  tabKey  get=0x0B1DB460
  backquoteKey  get=0x0B1DAD08
  quoteKey  get=0x0B1DB380
  semicolonKey  get=0x0B1DB420
  commaKey  get=0x0B1DAD58
  periodKey  get=0x0B1DB350
  slashKey  get=0x0B1DB430
  backslashKey  get=0x0B1DAD18
  leftBracketKey  get=0x0B1DB130
  rightBracketKey  get=0x0B1DB3D0
  minusKey  get=0x0B1DB170
  equalsKey  get=0x0B1DAEB0
  aKey  get=0x0B1DAC90
  bKey  get=0x0B1DACF8
  cKey  get=0x0B1DAD38
  dKey  get=0x0B1DADB0
  eKey  get=0x0B1DAE80
  fKey  get=0x0B1DB050
  gKey  get=0x0B1DB060
  hKey  get=0x0B1DB070
  iKey  get=0x0B1DB090
  jKey  get=0x0B1DB0B0
  kKey  get=0x0B1DB0C0
  lKey  get=0x0B1DB0F0
  mKey  get=0x0B1DB160
  nKey  get=0x0B1DB180
  oKey  get=0x0B1DB2B0
  pKey  get=0x0B1DB310
  qKey  get=0x0B1DB370
  rKey  get=0x0B1DB390
  sKey  get=0x0B1DB400
  tKey  get=0x0B1DB450
  uKey  get=0x0B1DB470
  vKey  get=0x0B1DB490
  wKey  get=0x0B1DB4A0
  xKey  get=0x0B1DB4B0
  yKey  get=0x0B1DB4C0
  zKey  get=0x0B1DB4D0
  digit1Key  get=0x0B1DADE0
  digit2Key  get=0x0B1DADF0
  digit3Key  get=0x0B1DAE00
  digit4Key  get=0x0B1DAE10
  digit5Key  get=0x0B1DAE20
  digit6Key  get=0x0B1DAE30
  digit7Key  get=0x0B1DAE40
  digit8Key  get=0x0B1DAE50
  digit9Key  get=0x0B1DAE60
  digit0Key  get=0x0B1DADD0
  leftShiftKey  get=0x0B1DB150
  rightShiftKey  get=0x0B1DB3F0
  leftAltKey  get=0x0B1DB100
  rightAltKey  get=0x0B1DB3A0
  leftCtrlKey  get=0x0B1DB140
  rightCtrlKey  get=0x0B1DB3E0
  leftMetaKey  get=0x0B1DB110
  rightMetaKey  get=0x0B1DB3B0
  leftWindowsKey  get=0x0B1DB110
  rightWindowsKey  get=0x0B1DB3B0
  leftAppleKey  get=0x0B1DB110
  rightAppleKey  get=0x0B1DB3B0
  leftCommandKey  get=0x0B1DB110
  rightCommandKey  get=0x0B1DB3B0
  contextMenuKey  get=0x0B1DAD68
  escapeKey  get=0x0B1DAEC0
  leftArrowKey  get=0x0B1DB120
  rightArrowKey  get=0x0B1DB3C0
  upArrowKey  get=0x0B1DB480
  downArrowKey  get=0x0B1DAE70
  backspaceKey  get=0x0B1DAD28
  pageDownKey  get=0x0B1DB320
  pageUpKey  get=0x0B1DB330
  homeKey  get=0x0B1DB080
  endKey  get=0x0B1DAE90
  insertKey  get=0x0B1DB0A0
  deleteKey  get=0x0B1DADC0
  capsLockKey  get=0x0B1DAD48
  scrollLockKey  get=0x0B1DB410
  numLockKey  get=0x0B1DB190
  printScreenKey  get=0x0B1DB360
  pauseKey  get=0x0B1DB340
  numpadEnterKey  get=0x0B1DB250
  numpadDivideKey  get=0x0B1DB240
  numpadMultiplyKey  get=0x0B1DB280
  numpadMinusKey  get=0x0B1DB270
  numpadPlusKey  get=0x0B1DB2A0
  numpadPeriodKey  get=0x0B1DB290
  numpadEqualsKey  get=0x0B1DB260
  numpad0Key  get=0x0B1DB1A0
  numpad1Key  get=0x0B1DB1B0
  numpad2Key  get=0x0B1DB1C0
  numpad3Key  get=0x0B1DB1D0
  numpad4Key  get=0x0B1DB1E0
  numpad5Key  get=0x0B1DB1F0
  numpad6Key  get=0x0B1DB200
  numpad7Key  get=0x0B1DB210
  numpad8Key  get=0x0B1DB220
  numpad9Key  get=0x0B1DB230
  f1Key  get=0x0B1DAF70
  f2Key  get=0x0B1DAFD0
  f3Key  get=0x0B1DAFE0
  f4Key  get=0x0B1DAFF0
  f5Key  get=0x0B1DB000
  f6Key  get=0x0B1DB010
  f7Key  get=0x0B1DB020
  f8Key  get=0x0B1DB030
  f9Key  get=0x0B1DB040
  f10Key  get=0x0B1DAED0
  f11Key  get=0x0B1DAEE0
  f12Key  get=0x0B1DAEF0
  oem1Key  get=0x0B1DB2C0
  oem2Key  get=0x0B1DB2D0
  oem3Key  get=0x0B1DB2E0
  oem4Key  get=0x0B1DB2F0
  oem5Key  get=0x0B1DB300
  f13Key  get=0x0B1DAF00
  f14Key  get=0x0B1DAF10
  f15Key  get=0x0B1DAF20
  f16Key  get=0x0B1DAF30
  f17Key  get=0x0B1DAF40
  f18Key  get=0x0B1DAF50
  f19Key  get=0x0B1DAF60
  f20Key  get=0x0B1DAF80
  f21Key  get=0x0B1DAF90
  f22Key  get=0x0B1DAFA0
  f23Key  get=0x0B1DAFB0
  f24Key  get=0x0B1DAFC0
  shiftKey  get=0x04D8DED0  set=0x05397218
  ctrlKey  get=0x04D8DA10  set=0x05397204
  altKey  get=0x04D8D9C0  set=0x053971F0
  imeSelected  get=0x04D8D960  set=0x053971DC
  Item  get=0x033068F0
  allKeys  get=0x0B1DACA0
  current  get=0x0B1DAD78  set=0x0B1DB578
  keys  get=0x04D87870  set=0x06B8B134
EVENTS:
  onTextInput  add=add_onTextInput  remove=remove_onTextInput
  onIMECompositionChange  add=add_onIMECompositionChange  remove=remove_onIMECompositionChange
METHODS:
  RVA=0x0B1DAA40  token=0x60006B1  System.Void SetIMEEnabled(System.Boolean enabled)
  RVA=0x0B1DA9CC  token=0x60006B2  System.Void SetIMECursorPosition(UnityEngine.Vector2 position)
  RVA=0x0444BC80  token=0x6000743  System.Void MakeCurrent()
  RVA=0x0B1DA8A8  token=0x6000744  System.Void OnRemoved()
  RVA=0x0B1D96F4  token=0x6000745  System.Void FinishSetup()
  RVA=0x0B1DA8D0  token=0x6000746  System.Void RefreshConfiguration()
  RVA=0x042514B0  token=0x6000747  System.Void OnTextInput(System.Char character)
  RVA=0x0B1D9628  token=0x6000748  UnityEngine.InputSystem.Controls.KeyControl FindKeyOnCurrentKeyboardLayout(System.String displayName)
  RVA=0x04899200  token=0x6000749  System.Void OnIMECompositionChanged(UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString)
  RVA=0x0400B6A0  token=0x600074A  System.Boolean UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr)
  RVA=0x053971D4  token=0x600074D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Mouse
TYPE:  class
TOKEN: 0x200008E
SIZE:  0x1F8
EXTENDS: UnityEngine.InputSystem.Pointer
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
FIELDS:
  private           UnityEngine.InputSystem.Controls.DeltaControl<scroll>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftButton>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<middleButton>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightButton>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<backButton>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<forwardButton>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.IntegerControl<clickCount>k__BackingField  // 0x1f0
  private   static  UnityEngine.InputSystem.Mouse   <current>k__BackingField  // static @ 0x0
  private   static  UnityEngine.InputSystem.Mouse   s_PlatformMouseDevice  // static @ 0x8
PROPERTIES:
  scroll  get=0x04D8DA80  set=0x06B8B42C
  leftButton  get=0x04D8D790  set=0x06B8B0D0
  middleButton  get=0x04D8D7B0  set=0x06B8B0F8
  rightButton  get=0x04D87870  set=0x06B8B134
  backButton  get=0x04D8D930  set=0x06B8B2C4
  forwardButton  get=0x04D87930  set=0x06799DA0
  clickCount  get=0x04D87940  set=0x06799DB4
  current  get=0x0B1DB928  set=0x0B1DB960
METHODS:
  RVA=0x03F64C80  token=0x600075E  System.Void MakeCurrent()
  RVA=0x04D1EF90  token=0x600075F  System.Void OnAdded()
  RVA=0x0B1DB824  token=0x6000760  System.Void OnRemoved()
  RVA=0x0B1DB8B4  token=0x6000761  System.Void WarpCursorPosition(UnityEngine.Vector2 position)
  RVA=0x0B1DB5C8  token=0x6000762  System.Void FinishSetup()
  RVA=0x0B1DB7C0  token=0x6000763  System.Void OnNextUpdate()
  RVA=0x0B1DB850  token=0x6000764  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B1DB8A4  token=0x6000765  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
  RVA=0x0B1DB8AC  token=0x6000766  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x053971D4  token=0x6000767  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.PenButton
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.PenButtonTip  // const
  public    static  UnityEngine.InputSystem.PenButtonEraser  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrelFirst  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrelSecond  // const
  public    static  UnityEngine.InputSystem.PenButtonInRange  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrelThird  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrelFourth  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrel1  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrel2  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrel3  // const
  public    static  UnityEngine.InputSystem.PenButtonBarrel4  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Pen
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x208
EXTENDS: UnityEngine.InputSystem.Pointer
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<tip>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<eraser>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<firstBarrelButton>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.ButtonControl<secondBarrelButton>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<thirdBarrelButton>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<fourthBarrelButton>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.ButtonControl<inRange>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.Vector2Control<tilt>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.AxisControl<twist>k__BackingField  // 0x200
  private   static  UnityEngine.InputSystem.Pen     <current>k__BackingField  // static @ 0x0
PROPERTIES:
  tip  get=0x04D8DA80  set=0x06B8B42C
  eraser  get=0x04D8D790  set=0x06B8B0D0
  firstBarrelButton  get=0x04D8D7B0  set=0x06B8B0F8
  secondBarrelButton  get=0x04D87870  set=0x06B8B134
  thirdBarrelButton  get=0x04D8D930  set=0x06B8B2C4
  fourthBarrelButton  get=0x04D87930  set=0x06799DA0
  inRange  get=0x04D87940  set=0x06799DB4
  tilt  get=0x04D87920  set=0x06799D8C
  twist  get=0x04D8DA40  set=0x06B8B3DC
  current  get=0x0B1DBDC4  set=0x0B1DBDFC
  Item  get=0x0B1DBC90
METHODS:
  RVA=0x0B1DBC20  token=0x600077D  System.Void MakeCurrent()
  RVA=0x0B1DBC3C  token=0x600077E  System.Void OnRemoved()
  RVA=0x0B1DB9B0  token=0x600077F  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000780  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Pointer
TYPE:  class
TOKEN: 0x2000091
SIZE:  0x1C0
EXTENDS: UnityEngine.InputSystem.InputDevice
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<position>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.DeltaControl<delta>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.Vector2Control<radius>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.AxisControl<pressure>k__BackingField  // 0x1a8
  private           UnityEngine.InputSystem.Controls.ButtonControl<press>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.IntegerControl<displayIndex>k__BackingField  // 0x1b8
  private   static  UnityEngine.InputSystem.Pointer <current>k__BackingField  // static @ 0x0
PROPERTIES:
  position  get=0x04D8DD50  set=0x0539722C
  delta  get=0x04D8DED0  set=0x05397218
  radius  get=0x04D8DA10  set=0x05397204
  pressure  get=0x04D8D9C0  set=0x053971F0
  press  get=0x04D8D960  set=0x053971DC
  displayIndex  get=0x04D8DA90  set=0x05397240
  current  get=0x0B1DC0CC  set=0x0B1DC104
METHODS:
  RVA=0x03F64CA0  token=0x600078F  System.Void MakeCurrent()
  RVA=0x0B1DC040  token=0x6000790  System.Void OnRemoved()
  RVA=0x0B1DBE4C  token=0x6000791  System.Void FinishSetup()
  RVA=0x0B1DBFE8  token=0x6000792  System.Void OnNextUpdate()
  RVA=0x0B1DC068  token=0x6000793  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B1DC0BC  token=0x6000794  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
  RVA=0x0B1DC0C4  token=0x6000795  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x012081B0  token=0x6000796  System.Boolean UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
  RVA=0x053971D4  token=0x6000797  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.FastKeyboard
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x1F8
EXTENDS: UnityEngine.InputSystem.Keyboard
FIELDS:
  public    static  System.String                   metadata  // const
METHODS:
  RVA=0x03AF3C50  token=0x6000798  System.Void .ctor()
  RVA=0x0429A410  token=0x6000799  UnityEngine.InputSystem.Controls.AnyKeyControl Initialize_ctrlKeyboardanyKey(UnityEngine.InputSystem.Utilities.InternedString kAnyKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049E2190  token=0x600079A  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardescape(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FFFF0  token=0x600079B  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardspace(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049E1D90  token=0x600079C  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardenter(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FFC30  token=0x600079D  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardtab(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03A10  token=0x600079E  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardbackquote(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00D10  token=0x600079F  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardquote(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A003B0  token=0x60007A0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardsemicolon(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03470  token=0x60007A1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardcomma(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A010D0  token=0x60007A2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardperiod(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A001D0  token=0x60007A3  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardslash(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03830  token=0x60007A4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardbackslash(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01C10  token=0x60007A5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftBracket(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00770  token=0x60007A6  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightBracket(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01850  token=0x60007A7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardminus(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02CF0  token=0x60007A8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardequals(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FF870  token=0x60007A9  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardupArrow(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A030B0  token=0x60007AA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboarddownArrow(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01DF0  token=0x60007AB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftArrow(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00950  token=0x60007AC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightArrow(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03DD0  token=0x60007AD  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboarda(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03BF0  token=0x60007AE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardb(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03650  token=0x60007AF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardc(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A03290  token=0x60007B0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardd(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02ED0  token=0x60007B1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboarde(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02B10  token=0x60007B2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02930  token=0x60007B3  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardg(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02750  token=0x60007B4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardh(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02570  token=0x60007B5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardi(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A02390  token=0x60007B6  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardj(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A021B0  token=0x60007B7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardk(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01FD0  token=0x60007B8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardl(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01A30  token=0x60007B9  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardm(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01670  token=0x60007BA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardn(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A01490  token=0x60007BB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardo(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A012B0  token=0x60007BC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardp(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00EF0  token=0x60007BD  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardq(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00B30  token=0x60007BE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardr(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A00590  token=0x60007BF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboards(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FFE10  token=0x60007C0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardt(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FFA50  token=0x60007C1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardu(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FF690  token=0x60007C2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardv(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FF4B0  token=0x60007C3  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardw(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FF2D0  token=0x60007C4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardx(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FF0F0  token=0x60007C5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardy(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x049FEF10  token=0x60007C6  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardz(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A1B4C0  token=0x60007C7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard1(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x04A1B2E0  token=0x60007C8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard2(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CEA90  token=0x60007C9  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard3(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CEC70  token=0x60007CA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard4(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CEE50  token=0x60007CB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard5(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF030  token=0x60007CC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard6(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF210  token=0x60007CD  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard7(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF3F0  token=0x60007CE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard8(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF5D0  token=0x60007CF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard9(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF7B0  token=0x60007D0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboard0(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CF990  token=0x60007D1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftShift(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CFB90  token=0x60007D2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightShift(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2990  token=0x60007D3  UnityEngine.InputSystem.Controls.DiscreteButtonControl Initialize_ctrlKeyboardshift(UnityEngine.InputSystem.Utilities.InternedString kDiscreteButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CFD90  token=0x60007D4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftAlt(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040CFF90  token=0x60007D5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightAlt(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2BB0  token=0x60007D6  UnityEngine.InputSystem.Controls.DiscreteButtonControl Initialize_ctrlKeyboardalt(UnityEngine.InputSystem.Utilities.InternedString kDiscreteButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0190  token=0x60007D7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftCtrl(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0390  token=0x60007D8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightCtrl(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2DD0  token=0x60007D9  UnityEngine.InputSystem.Controls.DiscreteButtonControl Initialize_ctrlKeyboardctrl(UnityEngine.InputSystem.Utilities.InternedString kDiscreteButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0590  token=0x60007DA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardleftMeta(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D07A0  token=0x60007DB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardrightMeta(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DB620  token=0x60007DC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardcontextMenu(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DB440  token=0x60007DD  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardbackspace(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DB260  token=0x60007DE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardpageDown(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DB080  token=0x60007DF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardpageUp(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DAEA0  token=0x60007E0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardhome(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DACC0  token=0x60007E1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardend(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DAAE0  token=0x60007E2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardinsert(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DA900  token=0x60007E3  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboarddelete(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DA720  token=0x60007E4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardcapsLock(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DA540  token=0x60007E5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumLock(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DA360  token=0x60007E6  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardprintScreen(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040DA180  token=0x60007E7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardscrollLock(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9FA0  token=0x60007E8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardpause(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9DC0  token=0x60007E9  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadEnter(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9BE0  token=0x60007EA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadDivide(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9A00  token=0x60007EB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadMultiply(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9820  token=0x60007EC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadPlus(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9640  token=0x60007ED  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadMinus(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D09F0  token=0x60007EE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadPeriod(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0BD0  token=0x60007EF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpadEquals(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0DB0  token=0x60007F0  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad1(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D0F90  token=0x60007F1  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad2(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1170  token=0x60007F2  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad3(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1350  token=0x60007F3  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad4(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1530  token=0x60007F4  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad5(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1710  token=0x60007F5  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad6(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D18F0  token=0x60007F6  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad7(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1AD0  token=0x60007F7  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad8(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1CB0  token=0x60007F8  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad9(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D1E90  token=0x60007F9  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardnumpad0(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2070  token=0x60007FA  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf1(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2250  token=0x60007FB  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf2(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2430  token=0x60007FC  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf3(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2610  token=0x60007FD  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf4(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9460  token=0x60007FE  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf5(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D9280  token=0x60007FF  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf6(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D90A0  token=0x6000800  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf7(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8EC0  token=0x6000801  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf8(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8CE0  token=0x6000802  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf9(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8B00  token=0x6000803  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf10(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8920  token=0x6000804  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf11(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8740  token=0x6000805  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf12(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8560  token=0x6000806  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardOEM1(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D8380  token=0x6000807  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardOEM2(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D81A0  token=0x6000808  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardOEM3(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7FC0  token=0x6000809  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardOEM4(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7DE0  token=0x600080A  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardOEM5(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7C00  token=0x600080B  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf13(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7A20  token=0x600080C  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf14(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7840  token=0x600080D  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf15(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7660  token=0x600080E  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf16(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7440  token=0x600080F  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf17(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7260  token=0x6000810  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf18(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D7080  token=0x6000811  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf19(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6EA0  token=0x6000812  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf20(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6CC0  token=0x6000813  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf21(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6AE0  token=0x6000814  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf22(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6900  token=0x6000815  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf23(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6720  token=0x6000816  UnityEngine.InputSystem.Controls.KeyControl Initialize_ctrlKeyboardf24(UnityEngine.InputSystem.Utilities.InternedString kKeyLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6540  token=0x6000817  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlKeyboardIMESelected(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
END_CLASS

CLASS: UnityEngine.InputSystem.FastMouse
TYPE:  class
TOKEN: 0x2000093
SIZE:  0x1F8
EXTENDS: UnityEngine.InputSystem.Mouse
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver UnityEngine.InputSystem.LowLevel.IEventMerger
FIELDS:
  public    static  System.String                   metadata  // const
METHODS:
  RVA=0x041AB300  token=0x6000818  System.Void .ctor()
  RVA=0x040D6380  token=0x6000819  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlMouseposition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D61E0  token=0x600081A  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlMousedelta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D6050  token=0x600081B  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlMousescroll(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5E60  token=0x600081C  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousepress(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5C50  token=0x600081D  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouseleftButton(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5A40  token=0x600081E  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouserightButton(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5840  token=0x600081F  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousemiddleButton(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5650  token=0x6000820  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouseforwardButton(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5410  token=0x6000821  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousebackButton(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5250  token=0x6000822  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D5080  token=0x6000823  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlMouseradius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D2FF0  token=0x6000824  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMousepointerId(UnityEngine.InputSystem.Utilities.InternedString kDigitalLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3160  token=0x6000825  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMousedisplayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D32E0  token=0x6000826  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMouseclickCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3470  token=0x6000827  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3620  token=0x6000828  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D37D0  token=0x6000829  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3980  token=0x600082A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3B40  token=0x600082B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3D00  token=0x600082C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D3EB0  token=0x600082D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4040  token=0x600082E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D41D0  token=0x600082F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4380  token=0x6000830  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrolldown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4540  token=0x6000831  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4700  token=0x6000832  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4900  token=0x6000833  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4AB0  token=0x6000834  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrolly(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4C70  token=0x6000835  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMouseradiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x040D4E00  token=0x6000836  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMouseradiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x03BF2E70  token=0x6000837  System.Void OnNextUpdate()
  RVA=0x03BF2D00  token=0x6000838  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x05391098  token=0x6000839  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
  RVA=0x05396E64  token=0x600083A  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x030CE360  token=0x600083B  System.Boolean MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
  RVA=0x05396978  token=0x600083C  System.Boolean UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.FastTouchscreen
TYPE:  class
TOKEN: 0x2000094
SIZE:  0x1D8
EXTENDS: UnityEngine.InputSystem.Touchscreen
FIELDS:
  public    static  System.String                   metadata  // const
METHODS:
  RVA=0x0B1F99FC  token=0x600083D  System.Void .ctor()
  RVA=0x0B1DD5CC  token=0x600083E  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreenprimaryTouch(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DCDB0  token=0x600083F  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreenposition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC154  token=0x6000840  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreendelta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DD424  token=0x6000841  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenpressure(UnityEngine.InputSystem.Utilities.InternedString kAnalogLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DFE90  token=0x6000842  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreenradius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DD25C  token=0x6000843  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreenpress(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DCC4C  token=0x6000844  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreendisplayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0310  token=0x6000845  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch0(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2BB4  token=0x6000846  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch1(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E5458  token=0x6000847  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch2(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7CFC  token=0x6000848  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch3(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA5B0  token=0x6000849  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch4(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ECE68  token=0x600084A  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch5(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF720  token=0x600084B  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch6(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1FD8  token=0x600084C  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch7(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F488C  token=0x600084D  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch8(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7144  token=0x600084E  UnityEngine.InputSystem.Controls.TouchControl Initialize_ctrlTouchscreentouch9(UnityEngine.InputSystem.Utilities.InternedString kTouchLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DFCFC  token=0x600084F  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreenprimaryTouchtouchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE720  token=0x6000850  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreenprimaryTouchposition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DD750  token=0x6000851  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreenprimaryTouchdelta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DED98  token=0x6000852  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchpressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DEF10  token=0x6000853  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreenprimaryTouchradius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE598  token=0x6000854  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreenprimaryTouchphase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DEBCC  token=0x6000855  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreenprimaryTouchpress(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF9A0  token=0x6000856  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreenprimaryTouchtapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE244  token=0x6000857  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreenprimaryTouchdisplayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE3BC  token=0x6000858  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreenprimaryTouchindirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DFB18  token=0x6000859  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreenprimaryTouchtap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF818  token=0x600085A  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreenprimaryTouchstartTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF38C  token=0x600085B  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreenprimaryTouchstartPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE8BC  token=0x600085C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchpositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DEA44  token=0x600085D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchpositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DDDB8  token=0x600085E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DD8DC  token=0x600085F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DDA7C  token=0x6000860  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DDC1C  token=0x6000861  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DDF54  token=0x6000862  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DE0CC  token=0x6000863  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF09C  token=0x6000864  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchradiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF214  token=0x6000865  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchradiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF528  token=0x6000866  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchstartPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DF6A0  token=0x6000867  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenprimaryTouchstartPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DCF4C  token=0x6000868  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenpositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DD0D4  token=0x6000869  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenpositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC7C0  token=0x600086A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC2E4  token=0x600086B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC484  token=0x600086C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC624  token=0x600086D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DC95C  token=0x600086E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1DCAD4  token=0x600086F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreendeltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0020  token=0x6000870  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenradiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0198  token=0x6000871  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreenradiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2A20  token=0x6000872  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch0touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1458  token=0x6000873  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch0position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0488  token=0x6000874  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch0delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1AD0  token=0x6000875  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1C48  token=0x6000876  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch0radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E12D0  token=0x6000877  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch0phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1904  token=0x6000878  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch0press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E26D8  token=0x6000879  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch0tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0F7C  token=0x600087A  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch0displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E10F4  token=0x600087B  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch0indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2850  token=0x600087C  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch0tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2550  token=0x600087D  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch0startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E20C4  token=0x600087E  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch0startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E15F4  token=0x600087F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E177C  token=0x6000880  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0AF0  token=0x6000881  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0614  token=0x6000882  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E07B4  token=0x6000883  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0954  token=0x6000884  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0C8C  token=0x6000885  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E0E04  token=0x6000886  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1DD4  token=0x6000887  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E1F4C  token=0x6000888  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2260  token=0x6000889  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E23D8  token=0x600088A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch0startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E52C4  token=0x600088B  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch1touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3CFC  token=0x600088C  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch1position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2D2C  token=0x600088D  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch1delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4374  token=0x600088E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E44EC  token=0x600088F  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch1radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3B74  token=0x6000890  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch1phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E41A8  token=0x6000891  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch1press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4F7C  token=0x6000892  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch1tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3820  token=0x6000893  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch1displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3998  token=0x6000894  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch1indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E50F4  token=0x6000895  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch1tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4DF4  token=0x6000896  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch1startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4968  token=0x6000897  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch1startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3E98  token=0x6000898  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4020  token=0x6000899  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3394  token=0x600089A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E2EB8  token=0x600089B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3058  token=0x600089C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E31F8  token=0x600089D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E3530  token=0x600089E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E36A8  token=0x600089F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4678  token=0x60008A0  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E47F0  token=0x60008A1  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4B04  token=0x60008A2  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E4C7C  token=0x60008A3  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch1startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7B68  token=0x60008A4  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch2touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E65A0  token=0x60008A5  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch2position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E55D0  token=0x60008A6  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch2delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E6C18  token=0x60008A7  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E6D90  token=0x60008A8  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch2radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E6418  token=0x60008A9  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch2phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E6A4C  token=0x60008AA  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch2press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7820  token=0x60008AB  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch2tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E60C4  token=0x60008AC  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch2displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E623C  token=0x60008AD  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch2indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7998  token=0x60008AE  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch2tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7698  token=0x60008AF  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch2startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E720C  token=0x60008B0  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch2startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E673C  token=0x60008B1  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E68C4  token=0x60008B2  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E5C38  token=0x60008B3  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E575C  token=0x60008B4  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E58FC  token=0x60008B5  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E5A9C  token=0x60008B6  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E5DD4  token=0x60008B7  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E5F4C  token=0x60008B8  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E6F1C  token=0x60008B9  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7094  token=0x60008BA  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E73A8  token=0x60008BB  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7520  token=0x60008BC  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch2startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA41C  token=0x60008BD  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch3touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8E48  token=0x60008BE  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch3position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E7E74  token=0x60008BF  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch3delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E94C4  token=0x60008C0  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E963C  token=0x60008C1  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch3radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8CC0  token=0x60008C2  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch3phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E92F8  token=0x60008C3  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch3press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA0D4  token=0x60008C4  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch3tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E896C  token=0x60008C5  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch3displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8AE4  token=0x60008C6  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch3indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA24C  token=0x60008C7  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch3tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9F4C  token=0x60008C8  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch3startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9ABC  token=0x60008C9  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch3startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8FE8  token=0x60008CA  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9170  token=0x60008CB  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E84E0  token=0x60008CC  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8004  token=0x60008CD  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E81A4  token=0x60008CE  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E8344  token=0x60008CF  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E867C  token=0x60008D0  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E87F4  token=0x60008D1  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E97CC  token=0x60008D2  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9944  token=0x60008D3  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9C5C  token=0x60008D4  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1E9DD4  token=0x60008D5  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch3startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ECCD4  token=0x60008D6  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch4touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB700  token=0x60008D7  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch4position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA72C  token=0x60008D8  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch4delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EBD7C  token=0x60008D9  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EBEF4  token=0x60008DA  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch4radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB578  token=0x60008DB  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch4phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EBBB0  token=0x60008DC  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch4press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC98C  token=0x60008DD  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch4tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB224  token=0x60008DE  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch4displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB39C  token=0x60008DF  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch4indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ECB04  token=0x60008E0  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch4tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC804  token=0x60008E1  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch4startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC374  token=0x60008E2  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch4startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB8A0  token=0x60008E3  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EBA28  token=0x60008E4  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EAD98  token=0x60008E5  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EA8BC  token=0x60008E6  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EAA5C  token=0x60008E7  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EABFC  token=0x60008E8  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EAF34  token=0x60008E9  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EB0AC  token=0x60008EA  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC084  token=0x60008EB  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC1FC  token=0x60008EC  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC514  token=0x60008ED  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EC68C  token=0x60008EE  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch4startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF58C  token=0x60008EF  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch5touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EDFB8  token=0x60008F0  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch5position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ECFE4  token=0x60008F1  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch5delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE634  token=0x60008F2  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE7AC  token=0x60008F3  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch5radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EDE30  token=0x60008F4  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch5phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE468  token=0x60008F5  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch5press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF244  token=0x60008F6  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch5tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EDADC  token=0x60008F7  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch5displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EDC54  token=0x60008F8  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch5indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF3BC  token=0x60008F9  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch5tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF0BC  token=0x60008FA  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch5startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EEC2C  token=0x60008FB  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch5startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE158  token=0x60008FC  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE2E0  token=0x60008FD  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED650  token=0x60008FE  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED174  token=0x60008FF  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED314  token=0x6000900  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED4B4  token=0x6000901  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED7EC  token=0x6000902  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1ED964  token=0x6000903  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EE93C  token=0x6000904  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EEAB4  token=0x6000905  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EEDCC  token=0x6000906  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EEF44  token=0x6000907  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch5startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1E44  token=0x6000908  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch6touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0870  token=0x6000909  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch6position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EF89C  token=0x600090A  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch6delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0EEC  token=0x600090B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1064  token=0x600090C  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch6radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F06E8  token=0x600090D  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch6phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0D20  token=0x600090E  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch6press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1AFC  token=0x600090F  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch6tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0394  token=0x6000910  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch6displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F050C  token=0x6000911  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch6indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1C74  token=0x6000912  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch6tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1974  token=0x6000913  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch6startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F14E4  token=0x6000914  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch6startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0A10  token=0x6000915  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F0B98  token=0x6000916  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EFF08  token=0x6000917  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EFA2C  token=0x6000918  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EFBCC  token=0x6000919  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1EFD6C  token=0x600091A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F00A4  token=0x600091B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F021C  token=0x600091C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F11F4  token=0x600091D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F136C  token=0x600091E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F1684  token=0x600091F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F17FC  token=0x6000920  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch6startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F46F8  token=0x6000921  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch7touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3124  token=0x6000922  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch7position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2150  token=0x6000923  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch7delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F37A0  token=0x6000924  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3918  token=0x6000925  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch7radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2F9C  token=0x6000926  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch7phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F35D4  token=0x6000927  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch7press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F43B0  token=0x6000928  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch7tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2C48  token=0x6000929  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch7displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2DC0  token=0x600092A  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch7indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4528  token=0x600092B  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch7tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4228  token=0x600092C  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch7startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3D98  token=0x600092D  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch7startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F32C4  token=0x600092E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F344C  token=0x600092F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F27BC  token=0x6000930  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F22E0  token=0x6000931  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2480  token=0x6000932  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2620  token=0x6000933  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2958  token=0x6000934  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F2AD0  token=0x6000935  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3AA8  token=0x6000936  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3C20  token=0x6000937  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F3F38  token=0x6000938  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F40B0  token=0x6000939  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch7startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6FB0  token=0x600093A  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch8touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F59DC  token=0x600093B  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch8position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4A08  token=0x600093C  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch8delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6058  token=0x600093D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F61D0  token=0x600093E  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch8radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5854  token=0x600093F  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch8phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5E8C  token=0x6000940  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch8press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6C68  token=0x6000941  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch8tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5500  token=0x6000942  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch8displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5678  token=0x6000943  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch8indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6DE0  token=0x6000944  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch8tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6AE0  token=0x6000945  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch8startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6650  token=0x6000946  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch8startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5B7C  token=0x6000947  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5D04  token=0x6000948  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5074  token=0x6000949  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4B98  token=0x600094A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4D38  token=0x600094B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F4ED8  token=0x600094C  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5210  token=0x600094D  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F5388  token=0x600094E  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6360  token=0x600094F  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F64D8  token=0x6000950  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F67F0  token=0x6000951  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F6968  token=0x6000952  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch8startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F9868  token=0x6000953  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch9touchId(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8294  token=0x6000954  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch9position(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F72C0  token=0x6000955  UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlTouchscreentouch9delta(UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8910  token=0x6000956  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9pressure(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8A88  token=0x6000957  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch9radius(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F810C  token=0x6000958  UnityEngine.InputSystem.Controls.TouchPhaseControl Initialize_ctrlTouchscreentouch9phase(UnityEngine.InputSystem.Utilities.InternedString kTouchPhaseLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8744  token=0x6000959  UnityEngine.InputSystem.Controls.TouchPressControl Initialize_ctrlTouchscreentouch9press(UnityEngine.InputSystem.Utilities.InternedString kTouchPressLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F9520  token=0x600095A  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch9tapCount(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7DB8  token=0x600095B  UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlTouchscreentouch9displayIndex(UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7F30  token=0x600095C  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch9indirectTouch(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F9698  token=0x600095D  UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlTouchscreentouch9tap(UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F9398  token=0x600095E  UnityEngine.InputSystem.Controls.DoubleControl Initialize_ctrlTouchscreentouch9startTime(UnityEngine.InputSystem.Utilities.InternedString kDoubleLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8F08  token=0x600095F  UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlTouchscreentouch9startPosition(UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8434  token=0x6000960  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9positionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F85BC  token=0x6000961  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9positiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F792C  token=0x6000962  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltaup(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7450  token=0x6000963  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltadown(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F75F0  token=0x6000964  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltaleft(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7790  token=0x6000965  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltaright(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7AC8  token=0x6000966  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltax(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F7C40  token=0x6000967  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9deltay(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8C18  token=0x6000968  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9radiusx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F8D90  token=0x6000969  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9radiusy(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F90A8  token=0x600096A  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9startPositionx(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1F9220  token=0x600096B  UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlTouchscreentouch9startPositiony(UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, UnityEngine.InputSystem.InputControl parent)
END_CLASS

CLASS: UnityEngine.InputSystem.InputRemoting
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x30
IMPLEMENTS: System.IObservable`1 System.IObserver`1
FIELDS:
  private           UnityEngine.InputSystem.InputRemoting.Flagsm_Flags  // 0x10
  private           UnityEngine.InputSystem.InputManagerm_LocalManager  // 0x18
  private           UnityEngine.InputSystem.InputRemoting.Subscriber[]m_Subscribers  // 0x20
  private           UnityEngine.InputSystem.InputRemoting.RemoteSender[]m_Senders  // 0x28
PROPERTIES:
  sending  get=0x02F2E060  set=0x0B20429C
  manager  get=0x0385B100
METHODS:
  RVA=0x0B204218  token=0x600096E  System.Void .ctor(UnityEngine.InputSystem.InputManager manager, System.Boolean startSendingOnConnect)
  RVA=0x0B203D38  token=0x600096F  System.Void StartSending()
  RVA=0x0B203E9C  token=0x6000970  System.Void StopSending()
  RVA=0x0B2040E0  token=0x6000971  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnNext(UnityEngine.InputSystem.InputRemoting.Message msg)
  RVA=0x041E1670  token=0x6000972  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnError(System.Exception error)
  RVA=0x041E1670  token=0x6000973  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnCompleted()
  RVA=0x0B203FF4  token=0x6000974  System.IDisposable Subscribe(System.IObserver<UnityEngine.InputSystem.InputRemoting.Message> observer)
  RVA=0x0B203ACC  token=0x6000975  System.Void SendInitialMessages()
  RVA=0x0B2037DC  token=0x6000976  System.Void SendAllGeneratedLayouts()
  RVA=0x0B203C00  token=0x6000977  System.Void SendLayout(System.String layoutName)
  RVA=0x0B203694  token=0x6000978  System.Void SendAllDevices()
  RVA=0x0B2039F4  token=0x6000979  System.Void SendDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B203A7C  token=0x600097A  System.Void SendEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B203940  token=0x600097B  System.Void SendDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x0B203AEC  token=0x600097C  System.Void SendLayoutChange(System.String layout, UnityEngine.InputSystem.InputControlLayoutChange change)
  RVA=0x0B203CAC  token=0x600097D  System.Void Send(UnityEngine.InputSystem.InputRemoting.Message msg)
  RVA=0x0B2034D8  token=0x600097E  System.Int32 FindOrCreateSenderRecord(System.Int32 senderId)
  RVA=0x0B2033EC  token=0x600097F  UnityEngine.InputSystem.Utilities.InternedString BuildLayoutNamespace(System.Int32 senderId)
  RVA=0x0B20346C  token=0x6000980  System.Int32 FindLocalDeviceId(System.Int32 remoteDeviceId, System.Int32 senderIndex)
  RVA=0x0B2041E8  token=0x6000981  UnityEngine.InputSystem.InputDevice TryGetDeviceByRemoteId(System.Int32 remoteDeviceId, System.Int32 senderIndex)
  RVA=0x0B2035B0  token=0x6000983  System.Void RemoveRemoteDevices(System.Int32 participantId)
  RVA=-1  // generic def  token=0x6000984  System.Byte[] SerializeData(TData data)
  RVA=-1  // generic def  token=0x6000985  TData DeserializeData(System.Byte[] data)
END_CLASS

CLASS: UnityEngine.InputSystem.RemoteInputPlayerConnection
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: System.IObserver`1 System.IObservable`1
FIELDS:
  public    static readonly System.Guid                     kNewDeviceMsg  // static @ 0x0
  public    static readonly System.Guid                     kNewLayoutMsg  // static @ 0x10
  public    static readonly System.Guid                     kNewEventsMsg  // static @ 0x20
  public    static readonly System.Guid                     kRemoveDeviceMsg  // static @ 0x30
  public    static readonly System.Guid                     kChangeUsagesMsg  // static @ 0x40
  public    static readonly System.Guid                     kStartSendingMsg  // static @ 0x50
  public    static readonly System.Guid                     kStopSendingMsg  // static @ 0x60
  private           UnityEngine.Networking.PlayerConnection.IEditorPlayerConnectionm_Connection  // 0x18
  private           UnityEngine.InputSystem.RemoteInputPlayerConnection.Subscriber[]m_Subscribers  // 0x20
  private           System.Int32[]                  m_ConnectedIds  // 0x28
METHODS:
  RVA=0x0B205830  token=0x600099E  System.Void Bind(UnityEngine.Networking.PlayerConnection.IEditorPlayerConnection connection, System.Boolean isConnected)
  RVA=0x0B205F78  token=0x600099F  System.IDisposable Subscribe(System.IObserver<UnityEngine.InputSystem.InputRemoting.Message> observer)
  RVA=0x0B205CC0  token=0x60009A0  System.Void OnConnected(System.Int32 id)
  RVA=0x0B205D68  token=0x60009A1  System.Void OnDisconnected(System.Int32 id)
  RVA=0x0B205E14  token=0x60009A2  System.Void OnNewDevice(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E34  token=0x60009A3  System.Void OnNewLayout(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E24  token=0x60009A4  System.Void OnNewEvents(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E44  token=0x60009A5  System.Void OnRemoveDevice(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205CB0  token=0x60009A6  System.Void OnChangeUsages(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E54  token=0x60009A7  System.Void OnStartSending(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E64  token=0x60009A8  System.Void OnStopSending(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B205E74  token=0x60009A9  System.Void SendToSubscribers(UnityEngine.InputSystem.InputRemoting.MessageType type, UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
  RVA=0x0B2060CC  token=0x60009AA  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnNext(UnityEngine.InputSystem.InputRemoting.Message msg)
  RVA=0x041E1670  token=0x60009AB  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnError(System.Exception error)
  RVA=0x041E1670  token=0x60009AC  System.Void System.IObserver<UnityEngine.InputSystem.InputRemoting.Message>.OnCompleted()
  RVA=0x03573370  token=0x60009AD  System.Void .ctor()
  RVA=0x0B206224  token=0x60009AE  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Sensor
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x190
EXTENDS: UnityEngine.InputSystem.InputDevice
FIELDS:
PROPERTIES:
  samplingFrequency  get=0x0B20651C  set=0x0B2065FC
METHODS:
  RVA=0x053971D4  token=0x60009B3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Accelerometer
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<acceleration>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.Accelerometer<current>k__BackingField  // static @ 0x0
PROPERTIES:
  acceleration  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FF37C  set=0x0B1FF3B4
METHODS:
  RVA=0x0B1FF328  token=0x60009B8  System.Void MakeCurrent()
  RVA=0x0B1FF330  token=0x60009B9  System.Void OnRemoved()
  RVA=0x0B1FF2C8  token=0x60009BA  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009BB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Gyroscope
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<angularVelocity>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.Gyroscope<current>k__BackingField  // static @ 0x0
PROPERTIES:
  angularVelocity  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FFE98  set=0x0B1FFED0
METHODS:
  RVA=0x0B1FFE44  token=0x60009C0  System.Void MakeCurrent()
  RVA=0x0B1FFE4C  token=0x60009C1  System.Void OnRemoved()
  RVA=0x0B1FFDE4  token=0x60009C2  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009C3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.GravitySensor
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<gravity>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.GravitySensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  gravity  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FFD5C  set=0x0B1FFD94
METHODS:
  RVA=0x0B1FFCA8  token=0x60009C8  System.Void FinishSetup()
  RVA=0x0B1FFD08  token=0x60009C9  System.Void MakeCurrent()
  RVA=0x0B1FFD10  token=0x60009CA  System.Void OnRemoved()
  RVA=0x053971D4  token=0x60009CB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.AttitudeSensor
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.QuaternionControl<attitude>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.AttitudeSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  attitude  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FF5F4  set=0x0B1FF62C
METHODS:
  RVA=0x0B1FF5A0  token=0x60009D0  System.Void MakeCurrent()
  RVA=0x0B1FF5A8  token=0x60009D1  System.Void OnRemoved()
  RVA=0x0B1FF540  token=0x60009D2  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009D3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LinearAccelerationSensor
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<acceleration>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.LinearAccelerationSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  acceleration  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B2044A8  set=0x0B2044E0
METHODS:
  RVA=0x0B204454  token=0x60009D8  System.Void MakeCurrent()
  RVA=0x0B20445C  token=0x60009D9  System.Void OnRemoved()
  RVA=0x0B2043F4  token=0x60009DA  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009DB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.MagneticFieldSensor
TYPE:  class
TOKEN: 0x20000B2
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<magneticField>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.MagneticFieldSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  magneticField  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B2045E4  set=0x0B20461C
METHODS:
  RVA=0x0B204590  token=0x60009E0  System.Void MakeCurrent()
  RVA=0x0B204598  token=0x60009E1  System.Void OnRemoved()
  RVA=0x0B204530  token=0x60009E2  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009E3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LightSensor
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<lightLevel>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.LightSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  lightLevel  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B20436C  set=0x0B2043A4
METHODS:
  RVA=0x0B204318  token=0x60009E8  System.Void MakeCurrent()
  RVA=0x0B204320  token=0x60009E9  System.Void OnRemoved()
  RVA=0x0B2042B8  token=0x60009EA  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009EB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.PressureSensor
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<atmosphericPressure>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.PressureSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  atmosphericPressure  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B20566C  set=0x0B2056A4
METHODS:
  RVA=0x0B205618  token=0x60009F0  System.Void MakeCurrent()
  RVA=0x0B205620  token=0x60009F1  System.Void OnRemoved()
  RVA=0x0B2055B8  token=0x60009F2  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009F3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.ProximitySensor
TYPE:  class
TOKEN: 0x20000B5
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<distance>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.ProximitySensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  distance  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B2057A8  set=0x0B2057E0
METHODS:
  RVA=0x0B205754  token=0x60009F8  System.Void MakeCurrent()
  RVA=0x0B20575C  token=0x60009F9  System.Void OnRemoved()
  RVA=0x0B2056F4  token=0x60009FA  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60009FB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.HumiditySensor
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<relativeHumidity>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.HumiditySensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  relativeHumidity  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B200110  set=0x0B200148
METHODS:
  RVA=0x0B2000BC  token=0x6000A00  System.Void MakeCurrent()
  RVA=0x0B2000C4  token=0x6000A01  System.Void OnRemoved()
  RVA=0x0B20005C  token=0x6000A02  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000A03  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.AmbientTemperatureSensor
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<ambientTemperature>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.AmbientTemperatureSensor<current>k__BackingField  // static @ 0x0
PROPERTIES:
  ambientTemperature  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FF4B8  set=0x0B1FF4F0
METHODS:
  RVA=0x0B1FF464  token=0x6000A08  System.Void MakeCurrent()
  RVA=0x0B1FF46C  token=0x6000A09  System.Void OnRemoved()
  RVA=0x0B1FF404  token=0x6000A0A  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000A0B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.StepCounter
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.IntegerControl<stepCounter>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.StepCounter<current>k__BackingField  // static @ 0x0
PROPERTIES:
  stepCounter  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B206744  set=0x0B20677C
METHODS:
  RVA=0x0B2066F0  token=0x6000A10  System.Void MakeCurrent()
  RVA=0x0B2066F8  token=0x6000A11  System.Void OnRemoved()
  RVA=0x0B206690  token=0x6000A12  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000A13  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.HingeAngle
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x198
EXTENDS: UnityEngine.InputSystem.Sensor
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<angle>k__BackingField  // 0x190
  private   static  UnityEngine.InputSystem.HingeAngle<current>k__BackingField  // static @ 0x0
PROPERTIES:
  angle  get=0x04D8DD50  set=0x0539722C
  current  get=0x0B1FFFD4  set=0x0B20000C
METHODS:
  RVA=0x0B1FFF80  token=0x6000A18  System.Void MakeCurrent()
  RVA=0x0B1FFF88  token=0x6000A19  System.Void OnRemoved()
  RVA=0x0B1FFF20  token=0x6000A1A  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000A1B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.TouchPhase
TYPE:  sealed struct
TOKEN: 0x20000BA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.TouchPhaseNone  // const
  public    static  UnityEngine.InputSystem.TouchPhaseBegan  // const
  public    static  UnityEngine.InputSystem.TouchPhaseMoved  // const
  public    static  UnityEngine.InputSystem.TouchPhaseEnded  // const
  public    static  UnityEngine.InputSystem.TouchPhaseCanceled  // const
  public    static  UnityEngine.InputSystem.TouchPhaseStationary  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Touchscreen
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x1D8
EXTENDS: UnityEngine.InputSystem.Pointer
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver UnityEngine.InputSystem.LowLevel.IEventMerger UnityEngine.InputSystem.LowLevel.ICustomDeviceReset
FIELDS:
  private           UnityEngine.InputSystem.Controls.TouchControl<primaryTouch>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Controls.TouchControl><touches>k__BackingField  // 0x1c8
  private   static readonly Unity.Profiling.ProfilerMarker  k_TouchscreenUpdateMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  k_TouchAllocateMarker  // static @ 0x8
  private   static  UnityEngine.InputSystem.Touchscreen<current>k__BackingField  // static @ 0x10
  private   static  System.Single                   s_TapTime  // static @ 0x18
  private   static  System.Single                   s_TapDelayTime  // static @ 0x1c
  private   static  System.Single                   s_TapRadiusSquared  // static @ 0x20
PROPERTIES:
  primaryTouch  get=0x04D8DA80  set=0x06B8B42C
  touches  get=0x04DABF40  set=0x0B208040
  touchControlArray  get=0x04D8D790  set=0x0B207FD4
  current  get=0x0B207F24  set=0x0B207F74
METHODS:
  RVA=0x0B206C18  token=0x6000A24  System.Void MakeCurrent()
  RVA=0x0B2070A8  token=0x6000A25  System.Void OnRemoved()
  RVA=0x0B206880  token=0x6000A26  System.Void FinishSetup()
  RVA=0x0B206D4C  token=0x6000A27  System.Void OnNextUpdate()
  RVA=0x0B207108  token=0x6000A28  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B207F14  token=0x6000A29  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
  RVA=0x0B207F1C  token=0x6000A2A  System.Void UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B207CC8  token=0x6000A2B  System.Boolean UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
  RVA=0x0B207908  token=0x6000A2C  System.Void UnityEngine.InputSystem.LowLevel.ICustomDeviceReset.Reset()
  RVA=0x0B206C68  token=0x6000A2D  System.Boolean MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
  RVA=0x0B207C6C  token=0x6000A2E  System.Boolean UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
  RVA=0x0B207870  token=0x6000A2F  System.Void TriggerTap(UnityEngine.InputSystem.Controls.TouchControl control, UnityEngine.InputSystem.LowLevel.TouchState& state, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x053971D4  token=0x6000A30  System.Void .ctor()
  RVA=0x04CE9B30  token=0x6000A31  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.TrackedDevice
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x1B0
EXTENDS: UnityEngine.InputSystem.InputDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.IntegerControl<trackingState>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.ButtonControl<isTracked>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.Vector3Control<devicePosition>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.QuaternionControl<deviceRotation>k__BackingField  // 0x1a8
PROPERTIES:
  trackingState  get=0x04D8DD50  set=0x0539722C
  isTracked  get=0x04D8DED0  set=0x05397218
  devicePosition  get=0x04D8DA10  set=0x05397204
  deviceRotation  get=0x04D8D9C0  set=0x053971F0
METHODS:
  RVA=0x0B208058  token=0x6000A3A  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000A3B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.InputExtensions
TYPE:  static class
TOKEN: 0x20000BD
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x035298B0  token=0x6000A3C  System.Boolean IsInProgress(UnityEngine.InputSystem.InputActionPhase phase)
  RVA=0x0B2001AC  token=0x6000A3D  System.Boolean IsEndedOrCanceled(UnityEngine.InputSystem.TouchPhase phase)
  RVA=0x0B200198  token=0x6000A3E  System.Boolean IsActive(UnityEngine.InputSystem.TouchPhase phase)
  RVA=0x0B2001BC  token=0x6000A3F  System.Boolean IsModifierKey(UnityEngine.InputSystem.Key key)
  RVA=0x0B2001C8  token=0x6000A40  System.Boolean IsTextInputKey(UnityEngine.InputSystem.Key key)
END_CLASS

CLASS: UnityEngine.InputSystem.InputFeatureNames
TYPE:  static class
TOKEN: 0x20000BE
SIZE:  0x10
FIELDS:
  public    static  System.String                   kRunPlayerUpdatesInEditMode  // const
  public    static  System.String                   kDisableUnityRemoteSupport  // const
  public    static  System.String                   kUseOptimizedControls  // const
  public    static  System.String                   kUseReadValueCaching  // const
  public    static  System.String                   kParanoidReadValueCachingChecks  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.InputManager
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x4E0
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputUpdateProfilerMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputTryFindMatchingControllerMarker  // static @ 0x8
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputAddDeviceMarker  // static @ 0x10
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputRestoreDevicesAfterReloadMarker  // static @ 0x18
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputRegisterCustomTypesMarker  // static @ 0x20
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnBeforeUpdateMarker  // static @ 0x28
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnAfterUpdateMarker  // static @ 0x30
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnSettingsChangeMarker  // static @ 0x38
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnDeviceSettingsChangeMarker  // static @ 0x40
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnEventMarker  // static @ 0x48
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnLayoutChangeMarker  // static @ 0x50
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnDeviceChangeMarker  // static @ 0x58
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputOnActionsChangeMarker  // static @ 0x60
  private           System.Int32                    m_LayoutRegistrationVersion  // 0x10
  private           System.Single                   m_PollingFrequency  // 0x14
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.Collectionm_Layouts  // 0x18
  private           UnityEngine.InputSystem.Utilities.TypeTablem_Processors  // 0x58
  private           UnityEngine.InputSystem.Utilities.TypeTablem_Interactions  // 0x60
  private           UnityEngine.InputSystem.Utilities.TypeTablem_Composites  // 0x68
  private           System.Int32                    m_DevicesCount  // 0x70
  private           UnityEngine.InputSystem.InputDevice[]m_Devices  // 0x78
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.InputSystem.InputDevice>m_DevicesById  // 0x80
  private           System.Int32                    m_AvailableDeviceCount  // 0x88
  private           UnityEngine.InputSystem.InputManager.AvailableDevice[]m_AvailableDevices  // 0x90
  private           System.Int32                    m_DisconnectedDevicesCount  // 0x98
  private           UnityEngine.InputSystem.InputDevice[]m_DisconnectedDevices  // 0xa0
  private           UnityEngine.InputSystem.LowLevel.InputUpdateTypem_UpdateMask  // 0xa8
  private           UnityEngine.InputSystem.LowLevel.InputUpdateTypem_CurrentUpdate  // 0xac
  private           UnityEngine.InputSystem.LowLevel.InputStateBuffersm_StateBuffers  // 0xb0
  private           UnityEngine.InputSystem.InputSettings.ScrollDeltaBehaviorm_ScrollDeltaBehavior  // 0xe8
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.InputDeviceChange>>m_DeviceChangeListeners  // 0xf0
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.LowLevel.InputEventPtr>>m_DeviceStateChangeListeners  // 0x140
  private           UnityEngine.InputSystem.Utilities.CallbackArray<UnityEngine.InputSystem.Layouts.InputDeviceFindControlLayoutDelegate>m_DeviceFindLayoutCallbacks  // 0x190
  private           UnityEngine.InputSystem.Utilities.CallbackArray<UnityEngine.InputSystem.LowLevel.InputDeviceCommandDelegate>m_DeviceCommandCallbacks  // 0x1e0
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<System.String,UnityEngine.InputSystem.InputControlLayoutChange>>m_LayoutChangeListeners  // 0x230
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice>>m_EventListeners  // 0x280
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action>m_BeforeUpdateListeners  // 0x2d0
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action>m_AfterUpdateListeners  // 0x320
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action>m_SettingsChangedListeners  // 0x370
  private           System.Boolean                  m_NativeBeforeUpdateHooked  // 0x3c0
  private           System.Boolean                  m_HaveDevicesWithStateCallbackReceivers  // 0x3c1
  private           System.Boolean                  m_HasFocus  // 0x3c2
  private           UnityEngine.InputSystem.LowLevel.InputEventStreamm_InputEventStream  // 0x3c8
  private           UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegatem_DeviceFindExecuteCommandDelegate  // 0x440
  private           System.Int32                    m_DeviceFindExecuteCommandDeviceId  // 0x448
  private           UnityEngine.InputSystem.LowLevel.IInputRuntimem_Runtime  // 0x450
  private           UnityEngine.InputSystem.LowLevel.InputMetricsm_Metrics  // 0x458
  private           UnityEngine.InputSystem.InputSettingsm_Settings  // 0x490
  private           System.Boolean                  m_OptimizedControlsFeatureEnabled  // 0x498
  private           System.Boolean                  m_ReadValueCachingFeatureEnabled  // 0x499
  private           System.Boolean                  m_ParanoidReadValueCachingChecksEnabled  // 0x49a
  private           System.Boolean                  m_ShouldMakeCurrentlyUpdatingDeviceCurrent  // 0x49b
  private           UnityEngine.InputSystem.InputManager.StateChangeMonitorsForDevice[]m_StateChangeMonitors  // 0x4a0
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout>m_StateChangeMonitorTimeouts  // 0x4a8
PROPERTIES:
  devices  get=0x03DF8D60
  processors  get=0x02E56440
  interactions  get=0x04D86280
  composites  get=0x04D86230
  metrics  get=0x0B202E74
  settings  get=0x04D8DC30  set=0x0B2032F8
  updateMask  get=0x04D88C80  set=0x03DF9BE0
  defaultUpdateType  get=0x0381FC00
  scrollDeltaBehavior  get=0x04D92890  set=0x03DF9BD0
  pollingFrequency  get=0x04D88D40  set=0x0B203258
  isProcessingEvents  get=0x04DABED0
  gameIsPlaying  get=0x02FFF600
  gameHasFocus  get=0x030CE7D0
  gameShouldGetInputRegardlessOfFocus  get=0x030CD8B0
  optimizedControlsFeatureEnabled  get=0x04DABEE0  set=0x04DABF10
  readValueCachingFeatureEnabled  get=0x04DABF00  set=0x04DABF30
  paranoidReadValueCachingChecksEnabled  get=0x04DABEF0  set=0x04DABF20
EVENTS:
  onDeviceChange  add=add_onDeviceChange  remove=remove_onDeviceChange
  onDeviceStateChange  add=add_onDeviceStateChange  remove=remove_onDeviceStateChange
  onDeviceCommand  add=add_onDeviceCommand  remove=remove_onDeviceCommand
  onFindControlLayoutForDevice  add=add_onFindControlLayoutForDevice  remove=remove_onFindControlLayoutForDevice
  onLayoutChange  add=add_onLayoutChange  remove=remove_onLayoutChange
  onEvent  add=add_onEvent  remove=remove_onEvent
  onBeforeUpdate  add=add_onBeforeUpdate  remove=remove_onBeforeUpdate
  onAfterUpdate  add=add_onAfterUpdate  remove=remove_onAfterUpdate
  onSettingsChange  add=add_onSettingsChange  remove=remove_onSettingsChange
METHODS:
  RVA=0x03AEE050  token=0x6000A65  System.Void RegisterControlLayout(System.String name, System.Type type)
  RVA=0x03DF7450  token=0x6000A66  System.Void RegisterControlLayout(System.String json, System.String name, System.Boolean isOverride)
  RVA=0x0B2016DC  token=0x6000A67  System.Void RegisterControlLayoutBuilder(System.Func<UnityEngine.InputSystem.Layouts.InputControlLayout> method, System.String name, System.String baseLayout)
  RVA=0x03AEDAF0  token=0x6000A68  System.Void PerformLayoutPostRegistration(UnityEngine.InputSystem.Utilities.InternedString layoutName, UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Utilities.InternedString> baseLayouts, System.Boolean isReplacement, System.Boolean isKnownToBeDeviceLayout, System.Boolean isOverride)
  RVA=-1  // generic def  token=0x6000A69  System.Void RegisterPrecompiledLayout(System.String metadata)
  RVA=0x03AEF210  token=0x6000A6A  System.Void RecreateDevicesUsingLayout(UnityEngine.InputSystem.Utilities.InternedString layout, System.Boolean isKnownToBeDeviceLayout)
  RVA=0x0B200E5C  token=0x6000A6B  System.Boolean IsControlOrChildUsingLayoutRecursive(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x0B200F40  token=0x6000A6C  System.Boolean IsControlUsingLayout(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x03AEC5C0  token=0x6000A6D  System.Void RegisterControlLayoutMatcher(System.String layoutName, UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
  RVA=0x03AEBCE0  token=0x6000A6E  System.Void RegisterControlLayoutMatcher(System.Type type, UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
  RVA=0x03AEC240  token=0x6000A6F  System.Void RecreateDevicesUsingLayoutWithInferiorMatch(UnityEngine.InputSystem.Layouts.InputDeviceMatcher deviceMatcher)
  RVA=0x0B201550  token=0x6000A70  System.Void RecreateDevice(UnityEngine.InputSystem.InputDevice oldDevice, UnityEngine.InputSystem.Utilities.InternedString newLayout)
  RVA=0x03AEC4D0  token=0x6000A71  System.Void AddAvailableDevicesMatchingDescription(UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher, UnityEngine.InputSystem.Utilities.InternedString layout)
  RVA=0x0B2018EC  token=0x6000A72  System.Void RemoveControlLayout(System.String name)
  RVA=0x0B2028F4  token=0x6000A73  UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadControlLayout(System.Type type)
  RVA=0x0B2028CC  token=0x6000A74  UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadControlLayout(UnityEngine.InputSystem.Utilities.InternedString name)
  RVA=0x03AEB070  token=0x6000A75  UnityEngine.InputSystem.Utilities.InternedString TryFindMatchingControlLayout(UnityEngine.InputSystem.Layouts.InputDeviceDescription& deviceDescription, System.Int32 deviceId)
  RVA=0x046EA290  token=0x6000A76  UnityEngine.InputSystem.Utilities.InternedString FindOrRegisterDeviceLayoutForType(System.Type type)
  RVA=0x0B201014  token=0x6000A77  System.Boolean IsDeviceLayoutMarkedAsSupportedInSettings(UnityEngine.InputSystem.Utilities.InternedString layoutName)
  RVA=0x0B201128  token=0x6000A78  System.Collections.Generic.IEnumerable<System.String> ListControlLayouts(System.String basedOn)
  RVA=-1  // generic def  token=0x6000A79  System.Int32 GetControls(System.String path, UnityEngine.InputSystem.InputControlList<TControl>& controls)
  RVA=0x0B202544  token=0x6000A7A  System.Void SetDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B2001E4  token=0x6000A7B  System.Void AddDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B201B58  token=0x6000A7C  System.Void RemoveDeviceUsage(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Utilities.InternedString usage)
  RVA=0x0B201430  token=0x6000A7D  System.Void NotifyUsageChanged(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B2004B8  token=0x6000A7E  UnityEngine.InputSystem.InputDevice AddDevice(System.Type type, System.String name)
  RVA=0x0B2005AC  token=0x6000A7F  UnityEngine.InputSystem.InputDevice AddDevice(System.String layout, System.String name, UnityEngine.InputSystem.Utilities.InternedString variants)
  RVA=0x03AE9300  token=0x6000A80  UnityEngine.InputSystem.InputDevice AddDevice(UnityEngine.InputSystem.Utilities.InternedString layout, System.Int32 deviceId, System.String deviceName, UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription, UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags, UnityEngine.InputSystem.Utilities.InternedString variants)
  RVA=0x03AE8510  token=0x6000A81  System.Void AddDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20070C  token=0x6000A82  UnityEngine.InputSystem.InputDevice AddDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
  RVA=0x03AEAEC0  token=0x6000A83  UnityEngine.InputSystem.InputDevice AddDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription description, System.Boolean throwIfNoLayoutFound, System.String deviceName, System.Int32 deviceId, UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags)
  RVA=0x0B200348  token=0x6000A84  UnityEngine.InputSystem.InputDevice AddDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription description, UnityEngine.InputSystem.Utilities.InternedString layout, System.String deviceName, System.Int32 deviceId, UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags)
  RVA=0x0B201CBC  token=0x6000A85  System.Void RemoveDevice(UnityEngine.InputSystem.InputDevice device, System.Boolean keepOnListOfAvailableDevices)
  RVA=0x0B200A80  token=0x6000A86  System.Void FlushDisconnectedDevices()
  RVA=0x0381F4F0  token=0x6000A87  System.Void ResetDevice(UnityEngine.InputSystem.InputDevice device, System.Boolean alsoResetDontResetControls, System.Nullable<System.Boolean> issueResetCommand)
  RVA=0x0B202760  token=0x6000A88  UnityEngine.InputSystem.InputDevice TryGetDevice(System.String nameOrLayout)
  RVA=0x0B200AD0  token=0x6000A89  UnityEngine.InputSystem.InputDevice GetDevice(System.String nameOrLayout)
  RVA=0x0B202878  token=0x6000A8A  UnityEngine.InputSystem.InputDevice TryGetDevice(System.Type layoutType)
  RVA=0x033FE7F0  token=0x6000A8B  UnityEngine.InputSystem.InputDevice TryGetDeviceById(System.Int32 id)
  RVA=0x0B200B80  token=0x6000A8C  System.Int32 GetUnsupportedDevices(System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputDeviceDescription> descriptions)
  RVA=0x0381FDC0  token=0x6000A8D  System.Void EnableOrDisableDevice(UnityEngine.InputSystem.InputDevice device, System.Boolean enable, UnityEngine.InputSystem.InputManager.DeviceDisableScope scope)
  RVA=0x0B2014F0  token=0x6000A8E  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x076D2304  token=0x6000A8F  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=-1  // generic def  token=0x6000A90  System.Void QueueEvent(TEvent& inputEvent)
  RVA=0x0B202D20  token=0x6000A91  System.Void Update()
  RVA=0x04B1ECA0  token=0x6000A92  System.Void Update(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x03DF6F90  token=0x6000A93  System.Void Initialize(UnityEngine.InputSystem.LowLevel.IInputRuntime runtime, UnityEngine.InputSystem.InputSettings settings)
  RVA=0x0B20099C  token=0x6000A94  System.Void Destroy()
  RVA=0x03DF5480  token=0x6000A95  System.Void InitializeData()
  RVA=0x02F2AD30  token=0x6000A96  System.Void RegisterCustomTypes(System.Type[] types)
  RVA=0x03B0E4A0  token=0x6000A97  System.Void RegisterCustomTypes()
  RVA=0x03EB34C0  token=0x6000A98  System.Void InstallRuntime(UnityEngine.InputSystem.LowLevel.IInputRuntime runtime)
  RVA=0x03DF7940  token=0x6000A99  System.Void InstallGlobals()
  RVA=0x0B202B44  token=0x6000A9A  System.Void UninstallGlobals()
  RVA=0x03AE8900  token=0x6000AA1  System.Void MakeDeviceNameUnique(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B202478  token=0x6000AA2  System.Void ResetControlPathsRecursive(UnityEngine.InputSystem.InputControl control)
  RVA=0x03AE88A0  token=0x6000AA3  System.Void AssignUniqueDeviceId(UnityEngine.InputSystem.InputDevice device)
  RVA=0x03AE8AB0  token=0x6000AA4  System.Void ReallocateStateBuffers()
  RVA=0x0B200CC8  token=0x6000AA5  System.Void InitializeDefaultState(UnityEngine.InputSystem.InputDevice device)
  RVA=0x03AEF850  token=0x6000AA6  System.Void InitializeDeviceState(UnityEngine.InputSystem.InputDevice device)
  RVA=0x03AEA710  token=0x6000AA7  System.Void OnNativeDeviceDiscovered(System.Int32 deviceId, System.String deviceDescriptor)
  RVA=0x0B2011B0  token=0x6000AA8  UnityEngine.InputSystem.Utilities.JsonParser.JsonString MakeEscapedJsonString(System.String theString)
  RVA=0x03AEACE0  token=0x6000AA9  UnityEngine.InputSystem.InputDevice TryMatchDisconnectedDevice(System.String deviceDescriptor)
  RVA=0x03DFB220  token=0x6000AAA  System.Void InstallBeforeUpdateHookIfNecessary()
  RVA=0x041E1670  token=0x6000AAB  System.Void RestoreDevicesAfterDomainReloadIfNecessary()
  RVA=0x041E1670  token=0x6000AAC  System.Void WarnAboutDevicesFailingToRecreateAfterDomainReload()
  RVA=0x030CFC50  token=0x6000AAD  System.Void OnBeforeUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x03DF8060  token=0x6000AAE  System.Void ApplySettings()
  RVA=-1  // generic def  token=0x6000AAF  System.Int64 ExecuteGlobalCommand(TCommand& command)
  RVA=0x03DF7F40  token=0x6000AB0  System.Void AddAvailableDevicesThatAreNowRecognized()
  RVA=0x038202E0  token=0x6000AB1  System.Boolean ShouldRunDeviceInBackground(UnityEngine.InputSystem.InputDevice device)
  RVA=0x038200C0  token=0x6000AB2  System.Void OnFocusChanged(System.Boolean focus)
  RVA=0x03EE1670  token=0x6000AB3  System.Boolean ShouldRunUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x030CEAB0  token=0x6000AB4  System.Void OnUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer)
  RVA=0x03FA35E0  token=0x6000AB5  System.Boolean AreMaximumEventBytesPerUpdateExceeded(System.UInt32 totalEventBytesProcessed)
  RVA=0x0B201300  token=0x6000AB6  System.String MakeStringWithEventsProcessedByDevice()
  RVA=0x03FCE910  token=0x6000AB7  System.Void ResetCurrentProcessedEventBytesForDevices()
  RVA=0x0B200834  token=0x6000AB8  System.Void CheckAllDevicesOptimizedControlsHaveValidState()
  RVA=0x030CE6A0  token=0x6000AB9  System.Void InvokeAfterUpdateCallback(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x04DABEC0  token=0x6000ABA  System.Void DontMakeCurrentlyUpdatingDeviceCurrent()
  RVA=0x0400B550  token=0x6000ABB  System.Boolean UpdateState(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x030D01F0  token=0x6000ABC  System.Boolean UpdateState(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, System.Void* statePtr, System.UInt32 stateOffsetInDevice, System.UInt32 stateSize, System.Double internalTime, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x030D0CB0  token=0x6000ABD  System.Void WriteStateChange(UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers buffers, System.Int32 deviceIndex, UnityEngine.InputSystem.LowLevel.InputStateBlock& deviceStateBlock, System.UInt32 stateOffsetInDevice, System.Void* statePtr, System.UInt32 stateSizeInBytes, System.Boolean flippedBuffers)
  RVA=0x030D1710  token=0x6000ABE  System.Boolean FlipBuffersForDeviceIfNecessary(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x03DFCDF0  token=0x6000ABF  System.Void AddStateChangeMonitor(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.UInt32 groupIndex)
  RVA=0x0B202358  token=0x6000AC0  System.Void RemoveStateChangeMonitors(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B2021F0  token=0x6000AC1  System.Void RemoveStateChangeMonitor(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex)
  RVA=0x0B200760  token=0x6000AC2  System.Void AddStateChangeMonitorTimeout(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
  RVA=0x0B2020D0  token=0x6000AC3  System.Void RemoveStateChangeMonitorTimeout(UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.Int32 timerIndex)
  RVA=0x030D12F0  token=0x6000AC4  System.Void SortStateChangeMonitorsIfNecessary(System.Int32 deviceIndex)
  RVA=0x0B2026A8  token=0x6000AC5  System.Void SignalStateChangeMonitor(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor)
  RVA=0x04B6B440  token=0x6000AC6  System.Void FireStateChangeNotifications()
  RVA=0x030D1350  token=0x6000AC7  System.Boolean ProcessStateChangeMonitors(System.Int32 deviceIndex, System.Void* newStateFromEvent, System.Void* oldStateOfDevice, System.UInt32 newStateSizeInBytes, System.UInt32 newStateOffsetInBytes)
  RVA=0x030D26E0  token=0x6000AC8  System.Void FireStateChangeNotifications(System.Int32 deviceIndex, System.Double internalTime, UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x030CE880  token=0x6000AC9  System.Void ProcessStateChangeMonitorTimeouts()
  RVA=0x041E1670  token=0x6000ACA  System.Void .ctor()
  RVA=0x0489AC30  token=0x6000ACB  System.Void .cctor()
  RVA=0x048AE410  token=0x6000ACC  System.Int64 <TryFindMatchingControlLayout>b__88_0(UnityEngine.InputSystem.LowLevel.InputDeviceCommand& commandRef)
END_CLASS

CLASS: UnityEngine.InputSystem.InputSettings
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x70
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.String[]                 m_SupportedDevices  // 0x18
  private           UnityEngine.InputSystem.InputSettings.UpdateModem_UpdateMode  // 0x20
  private           UnityEngine.InputSystem.InputSettings.ScrollDeltaBehaviorm_ScrollDeltaBehavior  // 0x24
  private           System.Int32                    m_MaxEventBytesPerUpdate  // 0x28
  private           System.Int32                    m_MaxQueuedEventsPerUpdate  // 0x2c
  private           System.Boolean                  m_CompensateForScreenOrientation  // 0x30
  private           UnityEngine.InputSystem.InputSettings.BackgroundBehaviorm_BackgroundBehavior  // 0x34
  private           UnityEngine.InputSystem.InputSettings.EditorInputBehaviorInPlayModem_EditorInputBehaviorInPlayMode  // 0x38
  private           UnityEngine.InputSystem.InputSettings.InputActionPropertyDrawerModem_InputActionPropertyDrawerMode  // 0x3c
  private           System.Single                   m_DefaultDeadzoneMin  // 0x40
  private           System.Single                   m_DefaultDeadzoneMax  // 0x44
  private           System.Single                   m_DefaultButtonPressPoint  // 0x48
  private           System.Single                   m_ButtonReleaseThreshold  // 0x4c
  private           System.Single                   m_DefaultTapTime  // 0x50
  private           System.Single                   m_DefaultSlowTapTime  // 0x54
  private           System.Single                   m_DefaultHoldTime  // 0x58
  private           System.Single                   m_TapRadius  // 0x5c
  private           System.Single                   m_MultiTapDelayTime  // 0x60
  private           System.Boolean                  m_DisableRedundantEventsMerging  // 0x64
  private           System.Boolean                  m_ShortcutKeysConsumeInputs  // 0x65
  private           System.Collections.Generic.HashSet<System.String>m_FeatureFlags  // 0x68
  private   static  System.Int32                    s_OldUnsupportedFixedAndDynamicUpdateSetting  // const
PROPERTIES:
  updateMode  get=0x02B70F90  set=0x0B20A388
  scrollDeltaBehavior  get=0x011F0020  set=0x0B20A218
  compensateForScreenOrientation  get=0x04D865F0  set=0x0B20A0D8
  filterNoiseOnCurrent  get=0x012081B0  set=0x041E1670
  defaultDeadzoneMin  get=0x04D863F0  set=0x0B20A13C
  defaultDeadzoneMax  get=0x04D86400  set=0x0B20A120
  defaultButtonPressPoint  get=0x04D8D350  set=0x0B20A0E8
  buttonReleaseThreshold  get=0x04D87860  set=0x0B20A0BC
  defaultTapTime  get=0x04D885A0  set=0x0B20A190
  defaultSlowTapTime  get=0x04D86E00  set=0x0B20A174
  defaultHoldTime  get=0x04D88400  set=0x0B20A158
  tapRadius  get=0x04D91500  set=0x0B20A36C
  multiTapDelayTime  get=0x04D88480  set=0x0B20A1FC
  backgroundBehavior  get=0x04D86730  set=0x0B20A0AC
  editorInputBehaviorInPlayMode  get=0x04D863B0  set=0x0B20A1BC
  inputActionPropertyDrawerMode  get=0x04D86640  set=0x0B20A1CC
  maxEventBytesPerUpdate  get=0x011F2A90  set=0x0B20A1DC
  maxQueuedEventsPerUpdate  get=0x04D86710  set=0x0B20A1EC
  supportedDevices  get=0x03AEAC80  set=0x0B20A238
  disableRedundantEventsMerging  get=0x04D8F050  set=0x0B20A1AC
  shortcutKeysConsumeInput  get=0x04DAA1E0  set=0x0B20A228
METHODS:
  RVA=0x0B209F4C  token=0x6000B12  System.Void SetInternalFeatureFlag(System.String featureName, System.Boolean enabled)
  RVA=0x0B209E38  token=0x6000B13  System.Boolean IsFeatureEnabled(System.String featureName)
  RVA=0x0B209EA8  token=0x6000B14  System.Void OnChange()
  RVA=0x0B209E24  token=0x6000B15  System.Boolean CompareFloats(System.Single a, System.Single b)
  RVA=-1  // generic def  token=0x6000B16  System.Boolean CompareSets(UnityEngine.InputSystem.Utilities.ReadOnlyArray<T> a, UnityEngine.InputSystem.Utilities.ReadOnlyArray<T> b)
  RVA=0x0B209DCC  token=0x6000B17  System.Boolean CompareFeatureFlag(UnityEngine.InputSystem.InputSettings a, UnityEngine.InputSystem.InputSettings b, System.String featureName)
  RVA=0x0B209B40  token=0x6000B18  System.Boolean AreEqual(UnityEngine.InputSystem.InputSettings a, UnityEngine.InputSystem.InputSettings b)
  RVA=0x04D54560  token=0x6000B19  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DefaultInputActions
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0xB8
IMPLEMENTS: UnityEngine.InputSystem.IInputActionCollection2 UnityEngine.InputSystem.IInputActionCollection System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IDisposable
FIELDS:
  private   readonly UnityEngine.InputSystem.InputActionAsset<asset>k__BackingField  // 0x10
  private   readonly UnityEngine.InputSystem.InputActionMapm_Player  // 0x18
  private           UnityEngine.InputSystem.DefaultInputActions.IPlayerActionsm_PlayerActionsCallbackInterface  // 0x20
  private   readonly UnityEngine.InputSystem.InputActionm_Player_Move  // 0x28
  private   readonly UnityEngine.InputSystem.InputActionm_Player_Look  // 0x30
  private   readonly UnityEngine.InputSystem.InputActionm_Player_Fire  // 0x38
  private   readonly UnityEngine.InputSystem.InputActionMapm_UI  // 0x40
  private           UnityEngine.InputSystem.DefaultInputActions.IUIActionsm_UIActionsCallbackInterface  // 0x48
  private   readonly UnityEngine.InputSystem.InputActionm_UI_Navigate  // 0x50
  private   readonly UnityEngine.InputSystem.InputActionm_UI_Submit  // 0x58
  private   readonly UnityEngine.InputSystem.InputActionm_UI_Cancel  // 0x60
  private   readonly UnityEngine.InputSystem.InputActionm_UI_Point  // 0x68
  private   readonly UnityEngine.InputSystem.InputActionm_UI_Click  // 0x70
  private   readonly UnityEngine.InputSystem.InputActionm_UI_ScrollWheel  // 0x78
  private   readonly UnityEngine.InputSystem.InputActionm_UI_MiddleClick  // 0x80
  private   readonly UnityEngine.InputSystem.InputActionm_UI_RightClick  // 0x88
  private   readonly UnityEngine.InputSystem.InputActionm_UI_TrackedDevicePosition  // 0x90
  private   readonly UnityEngine.InputSystem.InputActionm_UI_TrackedDeviceOrientation  // 0x98
  private           System.Int32                    m_KeyboardMouseSchemeIndex  // 0xa0
  private           System.Int32                    m_GamepadSchemeIndex  // 0xa4
  private           System.Int32                    m_TouchSchemeIndex  // 0xa8
  private           System.Int32                    m_JoystickSchemeIndex  // 0xac
  private           System.Int32                    m_XRSchemeIndex  // 0xb0
PROPERTIES:
  asset  get=0x02B2ECC0
  bindingMask  get=0x0B209490  set=0x0B209574
  devices  get=0x0B209534  set=0x0B2095D4
  controlSchemes  get=0x0B209500
  bindings  get=0x0B2094E0
  Player  get=0x0339B870
  UI  get=0x0339B870
  KeyboardMouseScheme  get=0x0B209244
  GamepadScheme  get=0x0B2090BC
  TouchScheme  get=0x0B209308
  JoystickScheme  get=0x0B209180
  XRScheme  get=0x0B2093CC
METHODS:
  RVA=0x0B208CF4  token=0x6000B1B  System.Void .ctor()
  RVA=0x0B208BD8  token=0x6000B1C  System.Void Dispose()
  RVA=0x0B208B98  token=0x6000B22  System.Boolean Contains(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B208CCC  token=0x6000B23  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.InputAction> GetEnumerator()
  RVA=0x0B208CEC  token=0x6000B24  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B208C28  token=0x6000B25  System.Void Enable()
  RVA=0x0B208BB8  token=0x6000B26  System.Void Disable()
  RVA=0x0B208C48  token=0x6000B28  UnityEngine.InputSystem.InputAction FindAction(System.String actionNameOrId, System.Boolean throwIfNotFound)
  RVA=0x0B208C68  token=0x6000B29  System.Int32 FindBinding(UnityEngine.InputSystem.InputBinding bindingMask, UnityEngine.InputSystem.InputAction& action)
END_CLASS

CLASS: UnityEngine.InputSystem.InputValue
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x28
FIELDS:
  private           System.Nullable<UnityEngine.InputSystem.InputAction.CallbackContext>m_Context  // 0x10
PROPERTIES:
  isPressed  get=0x0B2113F0
METHODS:
  RVA=0x0B211398  token=0x6000B59  System.Object Get()
  RVA=-1  // generic def  token=0x6000B5A  TValue Get()
  RVA=0x041E1670  token=0x6000B5C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.PlayerInput
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x218
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.String                   DeviceLostMessage  // const
  public    static  System.String                   DeviceRegainedMessage  // const
  public    static  System.String                   ControlsChangedMessage  // const
  private           System.Int32                    m_AllMapsHashCode  // 0x18
  private           UnityEngine.InputSystem.InputActionAssetm_Actions  // 0x20
  private           UnityEngine.InputSystem.PlayerNotificationsm_NotificationBehavior  // 0x28
  private           UnityEngine.InputSystem.UI.InputSystemUIInputModulem_UIInputModule  // 0x30
  private           UnityEngine.InputSystem.PlayerInput.DeviceLostEventm_DeviceLostEvent  // 0x38
  private           UnityEngine.InputSystem.PlayerInput.DeviceRegainedEventm_DeviceRegainedEvent  // 0x40
  private           UnityEngine.InputSystem.PlayerInput.ControlsChangedEventm_ControlsChangedEvent  // 0x48
  private           UnityEngine.InputSystem.PlayerInput.ActionEvent[]m_ActionEvents  // 0x50
  private           System.Boolean                  m_NeverAutoSwitchControlSchemes  // 0x58
  private           System.String                   m_DefaultControlScheme  // 0x60
  private           System.String                   m_DefaultActionMap  // 0x68
  private           System.Int32                    m_SplitScreenIndex  // 0x70
  private           UnityEngine.Camera              m_Camera  // 0x78
  private           UnityEngine.InputSystem.InputValuem_InputValueObject  // 0x80
  private           UnityEngine.InputSystem.InputActionMapm_CurrentActionMap  // 0x88
  private           System.Int32                    m_PlayerIndex  // 0x90
  private           System.Boolean                  m_InputActive  // 0x94
  private           System.Boolean                  m_Enabled  // 0x95
  private           System.Boolean                  m_ActionsInitialized  // 0x96
  private           System.Collections.Generic.Dictionary<System.String,System.String>m_ActionMessageNames  // 0x98
  private           UnityEngine.InputSystem.Users.InputUserm_InputUser  // 0xa0
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_ActionTriggeredDelegate  // 0xa8
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.PlayerInput>>m_DeviceLostCallbacks  // 0xb0
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.PlayerInput>>m_DeviceRegainedCallbacks  // 0x100
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.PlayerInput>>m_ControlsChangedCallbacks  // 0x150
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>m_ActionTriggeredCallbacks  // 0x1a0
  private           System.Action<UnityEngine.InputSystem.InputControl,UnityEngine.InputSystem.LowLevel.InputEventPtr>m_UnpairedDeviceUsedDelegate  // 0x1f0
  private           System.Func<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Boolean>m_PreFilterUnpairedDeviceUsedDelegate  // 0x1f8
  private           System.Boolean                  m_OnUnpairedDeviceUsedHooked  // 0x200
  private           System.Action<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.InputDeviceChange>m_DeviceChangeDelegate  // 0x208
  private           System.Boolean                  m_OnDeviceChangeHooked  // 0x210
  private   static  System.Int32                    s_AllActivePlayersCount  // static @ 0x0
  private   static  UnityEngine.InputSystem.PlayerInput[]s_AllActivePlayers  // static @ 0x8
  private   static  System.Action<UnityEngine.InputSystem.Users.InputUser,UnityEngine.InputSystem.Users.InputUserChange,UnityEngine.InputSystem.InputDevice>s_UserChangeDelegate  // static @ 0x10
  private   static  System.Int32                    s_InitPairWithDevicesCount  // static @ 0x18
  private   static  UnityEngine.InputSystem.InputDevice[]s_InitPairWithDevices  // static @ 0x20
  private   static  System.Int32                    s_InitPlayerIndex  // static @ 0x28
  private   static  System.Int32                    s_InitSplitScreenIndex  // static @ 0x2c
  private   static  System.String                   s_InitControlScheme  // static @ 0x30
  private   static  System.Boolean                  s_DestroyIfDeviceSetupUnsuccessful  // static @ 0x38
PROPERTIES:
  inputIsActive  get=0x04D91BC0
  active  get=0x04D91BC0
  playerIndex  get=0x04D86B60
  splitScreenIndex  get=0x04D868A0
  actions  get=0x0B2189BC  set=0x0B219118
  currentControlScheme  get=0x0B218AEC
  defaultControlScheme  get=0x04D86280  set=0x063CC164
  neverAutoSwitchControlSchemes  get=0x04D86F70  set=0x0B2192B4
  currentActionMap  get=0x04D861F0  set=0x0B219248
  defaultActionMap  get=0x04D86230  set=0x0485F170
  notificationBehavior  get=0x011F2A90  set=0x0B2192F8
  actionEvents  get=0x0B218964  set=0x0B21909C
  deviceLostEvent  get=0x0B218B88
  deviceRegainedEvent  get=0x0B218BF8
  controlsChangedEvent  get=0x0B218A7C
  camera  get=0x04D862A0  set=0x06402320
  uiInputModule  get=0x011F36E0  set=0x0B219340
  user  get=0x04D88CD0
  devices  get=0x0B218C68
  hasMissingRequiredDevices  get=0x0B218CEC
  all  get=0x0B218A04
  isSinglePlayer  get=0x0B218D64
EVENTS:
  onActionTriggered  add=add_onActionTriggered  remove=remove_onActionTriggered
  onDeviceLost  add=add_onDeviceLost  remove=remove_onDeviceLost
  onDeviceRegained  add=add_onDeviceRegained  remove=remove_onDeviceRegained
  onControlsChanged  add=add_onControlsChanged  remove=remove_onControlsChanged
METHODS:
  RVA=-1  // generic def  token=0x6000B84  TDevice GetDevice()
  RVA=0x0B213BA8  token=0x6000B85  System.Void ActivateInput()
  RVA=0x0B218474  token=0x6000B86  System.Void UpdateDelegates()
  RVA=0x0B214E94  token=0x6000B87  System.Void DeactivateInput()
  RVA=0x0B217278  token=0x6000B88  System.Void PassivateInput()
  RVA=0x0B217AA8  token=0x6000B89  System.Boolean SwitchCurrentControlScheme(UnityEngine.InputSystem.InputDevice[] devices)
  RVA=0x0B217CA0  token=0x6000B8A  System.Void SwitchCurrentControlScheme(System.String controlScheme, UnityEngine.InputSystem.InputDevice[] devices)
  RVA=0x0B217978  token=0x6000B8B  System.Void SwitchCurrentActionMap(System.String mapNameOrId)
  RVA=0x0B2152BC  token=0x6000B8C  UnityEngine.InputSystem.PlayerInput GetPlayerByIndex(System.Int32 playerIndex)
  RVA=0x0B215140  token=0x6000B8D  UnityEngine.InputSystem.PlayerInput FindFirstPairedToDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B215E2C  token=0x6000B8E  UnityEngine.InputSystem.PlayerInput Instantiate(UnityEngine.GameObject prefab, System.Int32 playerIndex, System.String controlScheme, System.Int32 splitScreenIndex, UnityEngine.InputSystem.InputDevice pairWithDevice)
  RVA=0x0B215C6C  token=0x6000B8F  UnityEngine.InputSystem.PlayerInput Instantiate(UnityEngine.GameObject prefab, System.Int32 playerIndex, System.String controlScheme, System.Int32 splitScreenIndex, UnityEngine.InputSystem.InputDevice[] pairWithDevices)
  RVA=0x0B214F10  token=0x6000B90  UnityEngine.InputSystem.PlayerInput DoInstantiate(UnityEngine.GameObject prefab)
  RVA=0x0B215748  token=0x6000B91  System.Void InitializeActions()
  RVA=0x0B214C54  token=0x6000B92  System.Void CopyActionAssetAndApplyBindingOverrides()
  RVA=0x0B21811C  token=0x6000B93  System.Void UninitializeActions()
  RVA=0x0B215AB0  token=0x6000B94  System.Void InstallOnActionTriggeredHook()
  RVA=0x0B218310  token=0x6000B95  System.Void UninstallOnActionTriggeredHook()
  RVA=0x0B215FC4  token=0x6000B96  System.Void OnActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B214944  token=0x6000B97  System.Void CacheMessageNames()
  RVA=0x0B214C0C  token=0x6000B98  System.Void ClearCaches()
  RVA=0x0B213E18  token=0x6000B99  System.Void AssignUserAndDevices()
  RVA=0x0B21565C  token=0x6000B9A  System.Boolean HaveBindingForDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B21804C  token=0x6000B9B  System.Void UnassignUserAndDevices()
  RVA=0x0B217DEC  token=0x6000B9C  System.Boolean TryToActivateControlScheme(UnityEngine.InputSystem.InputControlScheme controlScheme)
  RVA=0x0B213C4C  token=0x6000B9D  System.Void AssignPlayerIndex()
  RVA=0x0B2148E4  token=0x6000B9E  System.Void Awake()
  RVA=0x0B2164F0  token=0x6000B9F  System.Void OnEnable()
  RVA=0x0B217348  token=0x6000BA0  System.Void StartListeningForUnpairedDeviceActivity()
  RVA=0x0B2174F4  token=0x6000BA1  System.Void StopListeningForUnpairedDeviceActivity()
  RVA=0x0B217280  token=0x6000BA2  System.Void StartListeningForDeviceChanges()
  RVA=0x0B217490  token=0x6000BA3  System.Void StopListeningForDeviceChanges()
  RVA=0x0B2162F4  token=0x6000BA4  System.Void OnDisable()
  RVA=0x0B214EC0  token=0x6000BA5  System.Void DebugLogAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B215484  token=0x6000BA6  System.Void HandleDeviceLost()
  RVA=0x0B215570  token=0x6000BA7  System.Void HandleDeviceRegained()
  RVA=0x0B215398  token=0x6000BA8  System.Void HandleControlsChanged()
  RVA=0x0B2170AC  token=0x6000BA9  System.Void OnUserChange(UnityEngine.InputSystem.Users.InputUser user, UnityEngine.InputSystem.Users.InputUserChange change, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B216950  token=0x6000BAA  System.Boolean OnPreFilterUnpairedDeviceUsed(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B216A48  token=0x6000BAB  System.Void OnUnpairedDeviceUsed(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B2161CC  token=0x6000BAC  System.Void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x0B217574  token=0x6000BAD  System.Void SwitchControlSchemeInternal(UnityEngine.InputSystem.InputControlScheme& controlScheme, UnityEngine.InputSystem.InputDevice[] devices)
  RVA=0x0B21869C  token=0x6000BAE  System.Void .ctor()
  RVA=0x0B21864C  token=0x6000BAF  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.PlayerInputManager
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x128
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.String                   PlayerJoinedMessage  // const
  public    static  System.String                   PlayerLeftMessage  // const
  private   static  UnityEngine.InputSystem.PlayerInputManager<instance>k__BackingField  // static @ 0x0
  private           UnityEngine.InputSystem.PlayerNotificationsm_NotificationBehavior  // 0x18
  private           System.Int32                    m_MaxPlayerCount  // 0x1c
  private           System.Boolean                  m_AllowJoining  // 0x20
  private           UnityEngine.InputSystem.PlayerJoinBehaviorm_JoinBehavior  // 0x24
  private           UnityEngine.InputSystem.PlayerInputManager.PlayerJoinedEventm_PlayerJoinedEvent  // 0x28
  private           UnityEngine.InputSystem.PlayerInputManager.PlayerLeftEventm_PlayerLeftEvent  // 0x30
  private           UnityEngine.InputSystem.InputActionPropertym_JoinAction  // 0x38
  private           UnityEngine.GameObject          m_PlayerPrefab  // 0x50
  private           System.Boolean                  m_SplitScreen  // 0x58
  private           System.Boolean                  m_MaintainAspectRatioInSplitScreen  // 0x59
  private           System.Int32                    m_FixedNumberOfSplitScreens  // 0x5c
  private           UnityEngine.Rect                m_SplitScreenRect  // 0x60
  private           System.Boolean                  m_JoinActionDelegateHooked  // 0x70
  private           System.Boolean                  m_UnpairedDeviceUsedDelegateHooked  // 0x71
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_JoinActionDelegate  // 0x78
  private           System.Action<UnityEngine.InputSystem.InputControl,UnityEngine.InputSystem.LowLevel.InputEventPtr>m_UnpairedDeviceUsedDelegate  // 0x80
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.PlayerInput>>m_PlayerJoinedCallbacks  // 0x88
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.PlayerInput>>m_PlayerLeftCallbacks  // 0xd8
PROPERTIES:
  splitScreen  get=0x04D86F70  set=0x0B2139CC
  maintainAspectRatioInSplitScreen  get=0x04D8EF10
  fixedNumberOfSplitScreens  get=0x04D86560
  splitScreenArea  get=0x04DA1280
  playerCount  get=0x0B21360C
  maxPlayerCount  get=0x04D86310
  joiningEnabled  get=0x04D866B0
  joinBehavior  get=0x011F0020  set=0x0B213980
  joinAction  get=0x04D8F4A0  set=0x0B2138DC
  notificationBehavior  get=0x011EF5B0  set=0x011EF9B0
  playerJoinedEvent  get=0x0B21365C
  playerLeftEvent  get=0x0B2136CC
  playerPrefab  get=0x04D86270  set=0x02FDAEA0
  instance  get=0x0B21354C  set=0x0B21388C
  messages  get=0x0B213584
EVENTS:
  onPlayerJoined  add=add_onPlayerJoined  remove=remove_onPlayerJoined
  onPlayerLeft  add=add_onPlayerLeft  remove=remove_onPlayerLeft
METHODS:
  RVA=0x0B212058  token=0x6000BD0  System.Void EnableJoining()
  RVA=0x0B211F7C  token=0x6000BD1  System.Void DisableJoining()
  RVA=0x0B212758  token=0x6000BD2  System.Void JoinPlayerFromUI()
  RVA=0x0B2126F8  token=0x6000BD3  System.Void JoinPlayerFromAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21262C  token=0x6000BD4  System.Void JoinPlayerFromActionIfNotAlreadyJoined(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B2127B8  token=0x6000BD5  UnityEngine.InputSystem.PlayerInput JoinPlayer(System.Int32 playerIndex, System.Int32 splitScreenIndex, System.String controlScheme, UnityEngine.InputSystem.InputDevice pairWithDevice)
  RVA=0x0B21285C  token=0x6000BD6  UnityEngine.InputSystem.PlayerInput JoinPlayer(System.Int32 playerIndex, System.Int32 splitScreenIndex, System.String controlScheme, UnityEngine.InputSystem.InputDevice[] pairWithDevices)
  RVA=0x0B211CF4  token=0x6000BD8  System.Boolean CheckIfPlayerCanJoin(System.Int32 playerIndex)
  RVA=0x0B212E40  token=0x6000BD9  System.Void OnUnpairedDeviceUsed(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B212B6C  token=0x6000BDA  System.Void OnEnable()
  RVA=0x0B212AF8  token=0x6000BDB  System.Void OnDisable()
  RVA=0x0B212ED8  token=0x6000BDC  System.Void UpdateSplitScreen()
  RVA=0x0B212230  token=0x6000BDD  System.Boolean IsDeviceUsableWithPlayerActions(UnityEngine.InputSystem.InputDevice device)
  RVA=0x041E1670  token=0x6000BDE  System.Void ValidateInputActionAsset()
  RVA=0x0B212900  token=0x6000BDF  System.Void NotifyPlayerJoined(UnityEngine.InputSystem.PlayerInput player)
  RVA=0x0B2129FC  token=0x6000BE0  System.Void NotifyPlayerLeft(UnityEngine.InputSystem.PlayerInput player)
  RVA=0x0B2133C8  token=0x6000BE1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.PlayerJoinBehavior
TYPE:  sealed struct
TOKEN: 0x20000DC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.PlayerJoinBehaviorJoinPlayersWhenButtonIsPressed  // const
  public    static  UnityEngine.InputSystem.PlayerJoinBehaviorJoinPlayersWhenJoinActionIsTriggered  // const
  public    static  UnityEngine.InputSystem.PlayerJoinBehaviorJoinPlayersManually  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.PlayerNotifications
TYPE:  sealed struct
TOKEN: 0x20000DD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.PlayerNotificationsSendMessages  // const
  public    static  UnityEngine.InputSystem.PlayerNotificationsBroadcastMessages  // const
  public    static  UnityEngine.InputSystem.PlayerNotificationsInvokeUnityEvents  // const
  public    static  UnityEngine.InputSystem.PlayerNotificationsInvokeCSharpEvents  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.DynamicBitfield
TYPE:  sealed struct
TOKEN: 0x20000DE
SIZE:  0x30
FIELDS:
  public            UnityEngine.InputSystem.Utilities.InlinedArray<System.UInt64>array  // 0x10
  public            System.Int32                    length  // 0x28
METHODS:
  RVA=0x03DFD0E0  token=0x6000BE4  System.Void SetLength(System.Int32 newLength)
  RVA=0x03E99830  token=0x6000BE5  System.Void SetBit(System.Int32 bitIndex)
  RVA=0x030D29B0  token=0x6000BE6  System.Boolean TestBit(System.Int32 bitIndex)
  RVA=0x03E997B0  token=0x6000BE7  System.Void ClearBit(System.Int32 bitIndex)
  RVA=0x0404D270  token=0x6000BE8  System.Boolean AnyBitIsSet()
  RVA=0x04DABF60  token=0x6000BE9  System.Int32 BitCountToULongCount(System.Int32 bitCount)
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.AxisComposite
TYPE:  class
TOKEN: 0x200026E
SIZE:  0x28
EXTENDS: UnityEngine.InputSystem.InputBindingComposite`1
FIELDS:
  public            System.Int32                    negative  // 0x10
  public            System.Int32                    positive  // 0x14
  public            System.Single                   minValue  // 0x18
  public            System.Single                   maxValue  // 0x1c
  public            UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWinswhichSideWins  // 0x20
PROPERTIES:
  midPoint  get=0x04DABAD0
METHODS:
  RVA=0x0B1C3110  token=0x6001694  System.Single ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3084  token=0x6001695  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3214  token=0x6001696  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.ButtonWithOneModifier
TYPE:  class
TOKEN: 0x2000270
SIZE:  0x20
EXTENDS: UnityEngine.InputSystem.InputBindingComposite`1
FIELDS:
  public            System.Int32                    modifier  // 0x10
  public            System.Int32                    button  // 0x14
  public            System.Boolean                  overrideModifiersNeedToBePressedFirst  // 0x18
  public            UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrdermodifiersOrder  // 0x1c
METHODS:
  RVA=0x0B1C3344  token=0x6001697  System.Single ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C32E0  token=0x6001698  System.Boolean ModifierIsPressed(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3260  token=0x6001699  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3274  token=0x600169A  System.Void FinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C33A4  token=0x600169B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers
TYPE:  class
TOKEN: 0x2000272
SIZE:  0x28
EXTENDS: UnityEngine.InputSystem.InputBindingComposite`1
FIELDS:
  public            System.Int32                    modifier1  // 0x10
  public            System.Int32                    modifier2  // 0x14
  public            System.Int32                    button  // 0x18
  public            System.Boolean                  overrideModifiersNeedToBePressedFirst  // 0x1c
  public            UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers.ModifiersOrdermodifiersOrder  // 0x20
METHODS:
  RVA=0x0B1C34D8  token=0x600169C  System.Single ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3438  token=0x600169D  System.Boolean ModifiersArePressed(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3260  token=0x600169E  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C33CC  token=0x600169F  System.Void FinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C3538  token=0x60016A0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.OneModifierComposite
TYPE:  class
TOKEN: 0x2000274
SIZE:  0x38
EXTENDS: UnityEngine.InputSystem.InputBindingComposite
FIELDS:
  public            System.Int32                    modifier  // 0x10
  public            System.Int32                    binding  // 0x14
  public            System.Boolean                  overrideModifiersNeedToBePressedFirst  // 0x18
  public            UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrdermodifiersOrder  // 0x1c
  private           System.Int32                    m_ValueSizeInBytes  // 0x20
  private           System.Type                     m_ValueType  // 0x28
  private           System.Boolean                  m_BindingIsButton  // 0x30
PROPERTIES:
  valueType  get=0x04D86240
  valueSizeInBytes  get=0x02B70F90
METHODS:
  RVA=0x0B1C52F4  token=0x60016A3  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C5478  token=0x60016A4  System.Void ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context, System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1C53CC  token=0x60016A5  System.Boolean ModifierIsPressed(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C5330  token=0x60016A6  System.Void FinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C5438  token=0x60016A7  System.Object ReadValueAsObject(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C501C  token=0x60016A8  System.Void DetermineValueTypeAndSize(UnityEngine.InputSystem.InputBindingCompositeContext& context, System.Int32 part, System.Type& valueType, System.Int32& valueSizeInBytes, System.Boolean& isButton)
  RVA=0x041E1670  token=0x60016A9  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.TwoModifiersComposite
TYPE:  class
TOKEN: 0x2000276
SIZE:  0x38
EXTENDS: UnityEngine.InputSystem.InputBindingComposite
FIELDS:
  public            System.Int32                    modifier1  // 0x10
  public            System.Int32                    modifier2  // 0x14
  public            System.Int32                    binding  // 0x18
  public            System.Boolean                  overrideModifiersNeedToBePressedFirst  // 0x1c
  public            UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrdermodifiersOrder  // 0x20
  private           System.Int32                    m_ValueSizeInBytes  // 0x24
  private           System.Type                     m_ValueType  // 0x28
  private           System.Boolean                  m_BindingIsButton  // 0x30
PROPERTIES:
  valueType  get=0x04D86240
  valueSizeInBytes  get=0x011F0020
METHODS:
  RVA=0x0B1C8050  token=0x60016AC  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C8220  token=0x60016AD  System.Void ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context, System.Void* buffer, System.Int32 bufferSize)
  RVA=0x0B1C8128  token=0x60016AE  System.Boolean ModifiersArePressed(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C808C  token=0x60016AF  System.Void FinishSetup(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C81CC  token=0x60016B0  System.Object ReadValueAsObject(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x041E1670  token=0x60016B1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.Vector2Composite
TYPE:  class
TOKEN: 0x2000278
SIZE:  0x28
EXTENDS: UnityEngine.InputSystem.InputBindingComposite`1
FIELDS:
  public            System.Int32                    up  // 0x10
  public            System.Int32                    down  // 0x14
  public            System.Int32                    left  // 0x18
  public            System.Int32                    right  // 0x1c
  public            System.Boolean                  normalize  // 0x20
  public            UnityEngine.InputSystem.Composites.Vector2Composite.Modemode  // 0x24
METHODS:
  RVA=0x0B1C9534  token=0x60016B2  UnityEngine.Vector2 ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C9510  token=0x60016B3  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x04D33260  token=0x60016B4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Composites.Vector3Composite
TYPE:  class
TOKEN: 0x200027A
SIZE:  0x30
EXTENDS: UnityEngine.InputSystem.InputBindingComposite`1
FIELDS:
  public            System.Int32                    up  // 0x10
  public            System.Int32                    down  // 0x14
  public            System.Int32                    left  // 0x18
  public            System.Int32                    right  // 0x1c
  public            System.Int32                    forward  // 0x20
  public            System.Int32                    backward  // 0x24
  public            UnityEngine.InputSystem.Composites.Vector3Composite.Modemode  // 0x28
METHODS:
  RVA=0x0B1C96C4  token=0x60016B5  UnityEngine.Vector3 ReadValue(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C9690  token=0x60016B6  System.Single EvaluateMagnitude(UnityEngine.InputSystem.InputBindingCompositeContext& context)
  RVA=0x0B1C98F0  token=0x60016B7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.AnyKeyControl
TYPE:  class
TOKEN: 0x2000216
SIZE:  0x148
EXTENDS: UnityEngine.InputSystem.Controls.ButtonControl
FIELDS:
METHODS:
  RVA=0x0429A600  token=0x60013C8  System.Void .ctor()
  RVA=0x045659C0  token=0x60013C9  System.Single ReadUnprocessedValueFromState(System.Void* statePtr)
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.AxisControl
TYPE:  class
TOKEN: 0x2000217
SIZE:  0x130
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  public            UnityEngine.InputSystem.Controls.AxisControl.Clampclamp  // 0x108
  public            System.Single                   clampMin  // 0x10c
  public            System.Single                   clampMax  // 0x110
  public            System.Single                   clampConstant  // 0x114
  public            System.Boolean                  invert  // 0x118
  public            System.Boolean                  normalize  // 0x119
  public            System.Single                   normalizeMin  // 0x11c
  public            System.Single                   normalizeMax  // 0x120
  public            System.Single                   normalizeZero  // 0x124
  public            System.Boolean                  scale  // 0x128
  public            System.Single                   scaleFactor  // 0x12c
METHODS:
  RVA=0x0B1B6754  token=0x60013CA  System.Single Preprocess(System.Single value)
  RVA=0x0B1B683C  token=0x60013CB  System.Single Unpreprocess(System.Single value)
  RVA=0x0429A750  token=0x60013CC  System.Void .ctor()
  RVA=0x0B1B6684  token=0x60013CD  System.Void FinishSetup()
  RVA=0x030D1A90  token=0x60013CE  System.Single ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1B689C  token=0x60013CF  System.Void WriteValueIntoState(System.Single value, System.Void* statePtr)
  RVA=0x0B1B65C4  token=0x60013D0  System.Boolean CompareValue(System.Void* firstStatePtr, System.Void* secondStatePtr)
  RVA=0x0426F5C0  token=0x60013D1  System.Single EvaluateMagnitude(System.Void* statePtr)
  RVA=0x0426F5F0  token=0x60013D2  System.Single EvaluateMagnitude(System.Single value)
  RVA=0x0B1B648C  token=0x60013D3  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.ButtonControl
TYPE:  class
TOKEN: 0x2000219
SIZE:  0x148
EXTENDS: UnityEngine.InputSystem.Controls.AxisControl
FIELDS:
  private           System.Boolean                  m_NeedsToCheckFramePress  // 0x130
  private           System.UInt32                   m_UpdateCountLastPressed  // 0x134
  private           System.UInt32                   m_UpdateCountLastReleased  // 0x138
  private           System.Boolean                  m_LastUpdateWasPress  // 0x13c
  private           System.Boolean                  <needsToCheckFramePress>k__BackingField  // 0x13d
  public            System.Single                   pressPoint  // 0x140
  private   static  System.Single                   s_GlobalDefaultButtonPressPoint  // static @ 0x0
  private   static  System.Single                   s_GlobalDefaultButtonReleaseThreshold  // static @ 0x4
  private   static  System.Single                   kMinButtonPressPoint  // const
PROPERTIES:
  needsToCheckFramePress  get=0x04DAB970  set=0x04DAB980
  pressPointOrDefault  get=0x03526940
  isPressed  get=0x03306A00
  wasPressedThisFrame  get=0x03306940
  wasReleasedThisFrame  get=0x03306A60
METHODS:
  RVA=0x0429A670  token=0x60013D7  System.Void .ctor()
  RVA=0x0B1B694C  token=0x60013D8  System.Boolean IsValueConsideredPressed(System.Single value)
  RVA=0x04528810  token=0x60013DA  System.Void BeginTestingForFramePresses(System.Boolean currentlyPressed, System.Boolean pressedLastFrame)
  RVA=0x03526890  token=0x60013DD  System.Void UpdateWasPressed()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.DeltaControl
TYPE:  class
TOKEN: 0x200021A
SIZE:  0x140
EXTENDS: UnityEngine.InputSystem.Controls.Vector2Control
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<up>k__BackingField  // 0x120
  private           UnityEngine.InputSystem.Controls.AxisControl<down>k__BackingField  // 0x128
  private           UnityEngine.InputSystem.Controls.AxisControl<left>k__BackingField  // 0x130
  private           UnityEngine.InputSystem.Controls.AxisControl<right>k__BackingField  // 0x138
PROPERTIES:
  up  get=0x04D8DC70  set=0x053971C0
  down  get=0x04D8D910  set=0x053971AC
  left  get=0x04D8D8A0  set=0x05397198
  right  get=0x04D8DC90  set=0x05397184
METHODS:
  RVA=0x0B1B6F9C  token=0x60013E6  System.Void FinishSetup()
  RVA=0x0B1B70A4  token=0x60013E7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.DiscreteButtonControl
TYPE:  class
TOKEN: 0x200021B
SIZE:  0x160
EXTENDS: UnityEngine.InputSystem.Controls.ButtonControl
FIELDS:
  public            System.Int32                    minValue  // 0x148
  public            System.Int32                    maxValue  // 0x14c
  public            System.Int32                    wrapAtValue  // 0x150
  public            System.Int32                    nullValue  // 0x154
  public            UnityEngine.InputSystem.Controls.DiscreteButtonControl.WriteModewriteMode  // 0x158
METHODS:
  RVA=0x0B1B70AC  token=0x60013E8  System.Void FinishSetup()
  RVA=0x0B1B71C0  token=0x60013E9  System.Single ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1B7280  token=0x60013EA  System.Void WriteValueIntoState(System.Single value, System.Void* statePtr)
  RVA=0x05396EA0  token=0x60013EB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.DoubleControl
TYPE:  class
TOKEN: 0x200021D
SIZE:  0x110
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
METHODS:
  RVA=0x0B1B7448  token=0x60013EC  System.Void .ctor()
  RVA=0x0B1B7388  token=0x60013ED  System.Double ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1B73E4  token=0x60013EE  System.Void WriteValueIntoState(System.Double value, System.Void* statePtr)
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.DpadControl
TYPE:  class
TOKEN: 0x200021E
SIZE:  0x140
EXTENDS: UnityEngine.InputSystem.Controls.Vector2Control
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<up>k__BackingField  // 0x120
  private           UnityEngine.InputSystem.Controls.ButtonControl<down>k__BackingField  // 0x128
  private           UnityEngine.InputSystem.Controls.ButtonControl<left>k__BackingField  // 0x130
  private           UnityEngine.InputSystem.Controls.ButtonControl<right>k__BackingField  // 0x138
PROPERTIES:
  up  get=0x04D8DC70  set=0x053971C0
  down  get=0x04D8D910  set=0x053971AC
  left  get=0x04D8D8A0  set=0x05397198
  right  get=0x04D8DC90  set=0x05397184
METHODS:
  RVA=0x0B1B7A40  token=0x60013F7  System.Void .ctor()
  RVA=0x0B1B75CC  token=0x60013F8  System.Void FinishSetup()
  RVA=0x0B1B7748  token=0x60013F9  UnityEngine.Vector2 ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1B78C8  token=0x60013FA  System.Void WriteValueIntoState(UnityEngine.Vector2 value, System.Void* statePtr)
  RVA=0x0B1B76D4  token=0x60013FB  UnityEngine.Vector2 MakeDpadVector(System.Boolean up, System.Boolean down, System.Boolean left, System.Boolean right, System.Boolean normalize)
  RVA=0x04DAB990  token=0x60013FC  UnityEngine.Vector2 MakeDpadVector(System.Single up, System.Single down, System.Single left, System.Single right)
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.IntegerControl
TYPE:  class
TOKEN: 0x2000221
SIZE:  0x108
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
METHODS:
  RVA=0x04CAFCC0  token=0x6001402  System.Void .ctor()
  RVA=0x03F51020  token=0x6001403  System.Int32 ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1BE9B0  token=0x6001404  System.Void WriteValueIntoState(System.Int32 value, System.Void* statePtr)
  RVA=0x0B1BE908  token=0x6001405  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.KeyControl
TYPE:  class
TOKEN: 0x2000222
SIZE:  0x150
EXTENDS: UnityEngine.InputSystem.Controls.ButtonControl
FIELDS:
  private           UnityEngine.InputSystem.Key     <keyCode>k__BackingField  // 0x148
  private           System.Int32                    m_ScanCode  // 0x14c
PROPERTIES:
  keyCode  get=0x04D8D6F0  set=0x04D8D720
  scanCode  get=0x0B1C0534
METHODS:
  RVA=0x0B1C0388  token=0x6001409  System.Void RefreshConfiguration()
  RVA=0x05396EA0  token=0x600140A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.QuaternionControl
TYPE:  class
TOKEN: 0x2000223
SIZE:  0x140
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<x>k__BackingField  // 0x120
  private           UnityEngine.InputSystem.Controls.AxisControl<y>k__BackingField  // 0x128
  private           UnityEngine.InputSystem.Controls.AxisControl<z>k__BackingField  // 0x130
  private           UnityEngine.InputSystem.Controls.AxisControl<w>k__BackingField  // 0x138
PROPERTIES:
  x  get=0x04D8DC70  set=0x053971C0
  y  get=0x04D8D910  set=0x053971AC
  z  get=0x04D8D8A0  set=0x05397198
  w  get=0x04D8DC90  set=0x05397184
METHODS:
  RVA=0x0B1C15D8  token=0x6001413  System.Void .ctor()
  RVA=0x0B1C12AC  token=0x6001414  System.Void FinishSetup()
  RVA=0x0B1C13C8  token=0x6001415  UnityEngine.Quaternion ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C1504  token=0x6001416  System.Void WriteValueIntoState(UnityEngine.Quaternion value, System.Void* statePtr)
  RVA=0x0B1C1060  token=0x6001417  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.StickControl
TYPE:  class
TOKEN: 0x2000224
SIZE:  0x140
EXTENDS: UnityEngine.InputSystem.Controls.Vector2Control
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<up>k__BackingField  // 0x120
  private           UnityEngine.InputSystem.Controls.ButtonControl<down>k__BackingField  // 0x128
  private           UnityEngine.InputSystem.Controls.ButtonControl<left>k__BackingField  // 0x130
  private           UnityEngine.InputSystem.Controls.ButtonControl<right>k__BackingField  // 0x138
PROPERTIES:
  up  get=0x04D8DC70  set=0x053971C0
  down  get=0x04D8D910  set=0x053971AC
  left  get=0x04D8D8A0  set=0x05397198
  right  get=0x04D8DC90  set=0x05397184
METHODS:
  RVA=0x0B1C1828  token=0x6001420  System.Void FinishSetup()
  RVA=0x0B1B70A4  token=0x6001421  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.TouchControl
TYPE:  class
TOKEN: 0x2000225
SIZE:  0x1D8
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.TouchPressControl<press>k__BackingField  // 0x170
  private           UnityEngine.InputSystem.Controls.IntegerControl<displayIndex>k__BackingField  // 0x178
  private           UnityEngine.InputSystem.Controls.IntegerControl<touchId>k__BackingField  // 0x180
  private           UnityEngine.InputSystem.Controls.Vector2Control<position>k__BackingField  // 0x188
  private           UnityEngine.InputSystem.Controls.DeltaControl<delta>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.AxisControl<pressure>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.Vector2Control<radius>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.TouchPhaseControl<phase>k__BackingField  // 0x1a8
  private           UnityEngine.InputSystem.Controls.ButtonControl<indirectTouch>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<tap>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.IntegerControl<tapCount>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.DoubleControl<startTime>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.Vector2Control<startPosition>k__BackingField  // 0x1d0
PROPERTIES:
  press  get=0x04D8DEF0  set=0x06B8B940
  displayIndex  get=0x04D8DEE0  set=0x06B8B92C
  touchId  get=0x04D8DD60  set=0x06B8B774
  position  get=0x04D8DA20  set=0x06B8B3B4
  delta  get=0x04D8DD50  set=0x0539722C
  pressure  get=0x04D8DED0  set=0x05397218
  radius  get=0x04D8DA10  set=0x05397204
  phase  get=0x04D8D9C0  set=0x053971F0
  indirectTouch  get=0x04D8D960  set=0x053971DC
  tap  get=0x04D8DA90  set=0x05397240
  tapCount  get=0x04D8DA80  set=0x06B8B42C
  startTime  get=0x04D8D790  set=0x06B8B0D0
  startPosition  get=0x04D8D7B0  set=0x06B8B0F8
  isInProgress  get=0x0B1C1F9C
METHODS:
  RVA=0x0B1C1F48  token=0x600143D  System.Void .ctor()
  RVA=0x0B1C1AFC  token=0x600143E  System.Void FinishSetup()
  RVA=0x0B1C1E4C  token=0x600143F  UnityEngine.InputSystem.LowLevel.TouchState ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C1EC8  token=0x6001440  System.Void WriteValueIntoState(UnityEngine.InputSystem.LowLevel.TouchState value, System.Void* statePtr)
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.TouchPhaseControl
TYPE:  class
TOKEN: 0x2000226
SIZE:  0x108
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
METHODS:
  RVA=0x0B1C20BC  token=0x6001441  System.Void .ctor()
  RVA=0x0B1C1FFC  token=0x6001442  UnityEngine.InputSystem.TouchPhase ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C205C  token=0x6001443  System.Void WriteValueIntoState(UnityEngine.InputSystem.TouchPhase value, System.Void* statePtr)
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.TouchPressControl
TYPE:  class
TOKEN: 0x2000227
SIZE:  0x148
EXTENDS: UnityEngine.InputSystem.Controls.ButtonControl
FIELDS:
METHODS:
  RVA=0x0B1C2120  token=0x6001444  System.Void FinishSetup()
  RVA=0x0B1C2234  token=0x6001445  System.Single ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C22B0  token=0x6001446  System.Void WriteValueIntoState(System.Single value, System.Void* statePtr)
  RVA=0x05396EA0  token=0x6001447  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.Vector2Control
TYPE:  class
TOKEN: 0x2000228
SIZE:  0x120
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<x>k__BackingField  // 0x110
  private           UnityEngine.InputSystem.Controls.AxisControl<y>k__BackingField  // 0x118
PROPERTIES:
  x  get=0x04D8D1B0  set=0x05397170
  y  get=0x04D85F80  set=0x0539715C
METHODS:
  RVA=0x04B4E8C0  token=0x600144C  System.Void .ctor()
  RVA=0x0B1C2910  token=0x600144D  System.Void FinishSetup()
  RVA=0x03EE2190  token=0x600144E  UnityEngine.Vector2 ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C29C0  token=0x600144F  System.Void WriteValueIntoState(UnityEngine.Vector2 value, System.Void* statePtr)
  RVA=0x0428E420  token=0x6001450  System.Single EvaluateMagnitude(System.Void* statePtr)
  RVA=0x0B1C27E8  token=0x6001451  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.Controls.Vector3Control
TYPE:  class
TOKEN: 0x2000229
SIZE:  0x130
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.AxisControl<x>k__BackingField  // 0x118
  private           UnityEngine.InputSystem.Controls.AxisControl<y>k__BackingField  // 0x120
  private           UnityEngine.InputSystem.Controls.AxisControl<z>k__BackingField  // 0x128
PROPERTIES:
  x  get=0x04D85F80  set=0x0539715C
  y  get=0x04D8DC70  set=0x053971C0
  z  get=0x04D8D910  set=0x053971AC
METHODS:
  RVA=0x0B1C2F6C  token=0x6001458  System.Void .ctor()
  RVA=0x0B1C2CC4  token=0x6001459  System.Void FinishSetup()
  RVA=0x0B1C2DA8  token=0x600145A  UnityEngine.Vector3 ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B1C2EA8  token=0x600145B  System.Void WriteValueIntoState(UnityEngine.Vector3 value, System.Void* statePtr)
  RVA=0x0B1C2C58  token=0x600145C  System.Single EvaluateMagnitude(System.Void* statePtr)
  RVA=0x0B1C2AC0  token=0x600145D  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.DualShockGamepad
TYPE:  class
TOKEN: 0x2000159
SIZE:  0x288
EXTENDS: UnityEngine.InputSystem.Gamepad
IMPLEMENTS: UnityEngine.InputSystem.DualShock.IDualShockHaptics UnityEngine.InputSystem.Haptics.IDualMotorRumble UnityEngine.InputSystem.Haptics.IHaptics
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadButton>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.ButtonControl<optionsButton>k__BackingField  // 0x218
  private           UnityEngine.InputSystem.Controls.ButtonControl<shareButton>k__BackingField  // 0x220
  private           UnityEngine.InputSystem.Controls.ButtonControl<L1>k__BackingField  // 0x228
  private           UnityEngine.InputSystem.Controls.ButtonControl<R1>k__BackingField  // 0x230
  private           UnityEngine.InputSystem.Controls.ButtonControl<L2>k__BackingField  // 0x238
  private           UnityEngine.InputSystem.Controls.ButtonControl<R2>k__BackingField  // 0x240
  private           UnityEngine.InputSystem.Controls.ButtonControl<L3>k__BackingField  // 0x248
  private           UnityEngine.InputSystem.Controls.ButtonControl<R3>k__BackingField  // 0x250
  private   static  UnityEngine.InputSystem.DualShock.DualShockGamepad<current>k__BackingField  // static @ 0x0
  private           UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor<hidDescriptor>k__BackingField  // 0x258
PROPERTIES:
  touchpadButton  get=0x04D8D8B0  set=0x06B8B238
  optionsButton  get=0x04D8D3D0  set=0x06B1D640
  shareButton  get=0x04D8D870  set=0x06B8B1FC
  L1  get=0x04D8D440  set=0x06B22BCC
  R1  get=0x04D8DAE0  set=0x06B8B490
  L2  get=0x04D8DAD0  set=0x06B8B47C
  R2  get=0x04D8DE80  set=0x06B8B8C8
  L3  get=0x04D8D840  set=0x06B8B1C0
  R3  get=0x04D8DCE0  set=0x06B8B6E8
  current  get=0x0B22D00C  set=0x0B22D044
  hidDescriptor  get=0x04DAC7F0  set=0x0B22D094
METHODS:
  RVA=0x0B22CF9C  token=0x6000F26  System.Void MakeCurrent()
  RVA=0x0B22CFB8  token=0x6000F27  System.Void OnRemoved()
  RVA=0x0B22CDF0  token=0x6000F28  System.Void FinishSetup()
  RVA=0x041E1670  token=0x6000F29  System.Void SetLightBarColor(UnityEngine.Color color)
  RVA=0x053971D4  token=0x6000F2A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.DualSenseGamepadHID
TYPE:  class
TOKEN: 0x200015A
SIZE:  0x2C8
EXTENDS: UnityEngine.InputSystem.DualShock.DualShockGamepad
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IEventMerger UnityEngine.InputSystem.LowLevel.IEventPreProcessor UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftTriggerButton>k__BackingField  // 0x288
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightTriggerButton>k__BackingField  // 0x290
  private           UnityEngine.InputSystem.Controls.ButtonControl<playStationButton>k__BackingField  // 0x298
  private           System.Nullable<System.Single>  m_LowFrequencyMotorSpeed  // 0x2a0
  private           System.Nullable<System.Single>  m_HighFrequenceyMotorSpeed  // 0x2a8
  protected         System.Nullable<UnityEngine.Color>m_LightBarColor  // 0x2b0
  private           System.Byte                     outputSequenceId  // 0x2c4
  private   static  System.Byte                     JitterMaskLow  // const
  private   static  System.Byte                     JitterMaskHigh  // const
PROPERTIES:
  leftTriggerButton  get=0x04D8D820  set=0x06B8B198
  rightTriggerButton  get=0x04D8D800  set=0x06B8B170
  playStationButton  get=0x04D8DA30  set=0x06B8B3C8
METHODS:
  RVA=0x0B22B540  token=0x6000F31  System.Void FinishSetup()
  RVA=0x0B22B830  token=0x6000F32  System.Void PauseHaptics()
  RVA=0x0B22B8CC  token=0x6000F33  System.Void ResetHaptics()
  RVA=0x0B22B950  token=0x6000F34  System.Void ResumeHaptics()
  RVA=0x0B22B9C4  token=0x6000F35  System.Void SetLightBarColor(UnityEngine.Color color)
  RVA=0x0B22BC68  token=0x6000F36  System.Void SetMotorSpeeds(System.Single lowFrequency, System.Single highFrequency)
  RVA=0x0B22BA58  token=0x6000F37  System.Boolean SetMotorSpeedsAndLightBarColor(System.Nullable<System.Single> lowFrequency, System.Nullable<System.Single> highFrequency, System.Nullable<UnityEngine.Color> color)
  RVA=0x0B22B614  token=0x6000F38  System.Boolean MergeForward(UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState, UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState)
  RVA=0x0B22B64C  token=0x6000F39  System.Boolean MergeForward(UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState, UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState)
  RVA=0x0B22B684  token=0x6000F3A  System.Boolean MergeForward(UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState, UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState)
  RVA=0x0B22BD28  token=0x6000F3B  System.Boolean UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
  RVA=0x0B22BED4  token=0x6000F3C  System.Boolean UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x041E1670  token=0x6000F3D  System.Void OnNextUpdate()
  RVA=0x0B22B6BC  token=0x6000F3E  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x012081B0  token=0x6000F3F  System.Boolean GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
  RVA=0x053971D4  token=0x6000F40  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.DualShock4GamepadHID
TYPE:  class
TOKEN: 0x200015F
SIZE:  0x2C8
EXTENDS: UnityEngine.InputSystem.DualShock.DualShockGamepad
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IEventPreProcessor UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftTriggerButton>k__BackingField  // 0x288
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightTriggerButton>k__BackingField  // 0x290
  private           UnityEngine.InputSystem.Controls.ButtonControl<playStationButton>k__BackingField  // 0x298
  private           System.Nullable<System.Single>  m_LowFrequencyMotorSpeed  // 0x2a0
  private           System.Nullable<System.Single>  m_HighFrequenceyMotorSpeed  // 0x2a8
  private           System.Nullable<UnityEngine.Color>m_LightBarColor  // 0x2b0
  private   static  System.Byte                     JitterMaskLow  // const
  private   static  System.Byte                     JitterMaskHigh  // const
PROPERTIES:
  leftTriggerButton  get=0x04D8D820  set=0x06B8B198
  rightTriggerButton  get=0x04D8D800  set=0x06B8B170
  playStationButton  get=0x04D8DA30  set=0x06B8B3C8
METHODS:
  RVA=0x0B22C24C  token=0x6000F4C  System.Void FinishSetup()
  RVA=0x0B22C494  token=0x6000F4D  System.Void PauseHaptics()
  RVA=0x0B22C5B0  token=0x6000F4E  System.Void ResetHaptics()
  RVA=0x0B22C6EC  token=0x6000F4F  System.Void ResumeHaptics()
  RVA=0x0B22C874  token=0x6000F50  System.Void SetLightBarColor(UnityEngine.Color color)
  RVA=0x0B22CAF4  token=0x6000F51  System.Void SetMotorSpeeds(System.Single lowFrequency, System.Single highFrequency)
  RVA=0x0B22C97C  token=0x6000F52  System.Boolean SetMotorSpeedsAndLightBarColor(System.Single lowFrequency, System.Single highFrequency, UnityEngine.Color color)
  RVA=0x0B22CC0C  token=0x6000F53  System.Boolean UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x041E1670  token=0x6000F54  System.Void OnNextUpdate()
  RVA=0x0B22C320  token=0x6000F55  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x012081B0  token=0x6000F56  System.Boolean GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
  RVA=0x053971D4  token=0x6000F57  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.DualShock3GamepadHID
TYPE:  class
TOKEN: 0x2000161
SIZE:  0x2A0
EXTENDS: UnityEngine.InputSystem.DualShock.DualShockGamepad
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<leftTriggerButton>k__BackingField  // 0x288
  private           UnityEngine.InputSystem.Controls.ButtonControl<rightTriggerButton>k__BackingField  // 0x290
  private           UnityEngine.InputSystem.Controls.ButtonControl<playStationButton>k__BackingField  // 0x298
PROPERTIES:
  leftTriggerButton  get=0x04D8D820  set=0x06B8B198
  rightTriggerButton  get=0x04D8D800  set=0x06B8B170
  playStationButton  get=0x04D8DA30  set=0x06B8B3C8
METHODS:
  RVA=0x0B22C178  token=0x6000F60  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000F61  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.DualShockSupport
TYPE:  static class
TOKEN: 0x2000162
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03DF67A0  token=0x6000F62  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.IDualShockHaptics
TYPE:  interface
TOKEN: 0x2000163
IMPLEMENTS: UnityEngine.InputSystem.Haptics.IDualMotorRumble UnityEngine.InputSystem.Haptics.IHaptics
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000F63  System.Void SetLightBarColor(UnityEngine.Color color)
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport
TYPE:  sealed struct
TOKEN: 0x2000164
SIZE:  0x19
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public    static  UnityEngine.InputSystem.Utilities.FourCCFormat  // static @ 0x0
  public            System.Byte                     leftStickX  // 0x10
  public            System.Byte                     leftStickY  // 0x11
  public            System.Byte                     rightStickX  // 0x12
  public            System.Byte                     rightStickY  // 0x13
  public            System.Byte                     leftTrigger  // 0x14
  public            System.Byte                     rightTrigger  // 0x15
  public            System.Byte                     buttons0  // 0x16
  public            System.Byte                     buttons1  // 0x17
  public            System.Byte                     buttons2  // 0x18
PROPERTIES:
  format  get=0x0B22C0D8
METHODS:
  RVA=0x0B22C09C  token=0x6000F65  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload
TYPE:  sealed struct
TOKEN: 0x2000165
SIZE:  0x3F
FIELDS:
  public            System.Byte                     enableFlags1  // 0x10
  public            System.Byte                     enableFlags2  // 0x11
  public            System.Byte                     highFrequencyMotorSpeed  // 0x12
  public            System.Byte                     lowFrequencyMotorSpeed  // 0x13
  public            System.Byte                     redColor  // 0x3c
  public            System.Byte                     greenColor  // 0x3d
  public            System.Byte                     blueColor  // 0x3e
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport
TYPE:  sealed struct
TOKEN: 0x2000166
SIZE:  0x48
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Byte                     reportId  // 0x18
  public            UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayloadpayload  // 0x19
PROPERTIES:
  Type  get=0x04DAC3B0
  typeStatic  get=0x04DAC3B0
METHODS:
  RVA=0x04DAC750  token=0x6000F68  UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport Create(UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload, System.Int32 outputReportSize)
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDBluetoothOutputReport
TYPE:  sealed struct
TOKEN: 0x2000167
SIZE:  0x68
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Byte                     reportId  // 0x18
  public            System.Byte                     tag1  // 0x19
  public            System.Byte                     tag2  // 0x1a
  public            UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayloadpayload  // 0x1b
  public            System.UInt32                   crc32  // 0x62
  public            UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDBluetoothOutputReport.<rawData>e__FixedBufferrawData  // 0x18
PROPERTIES:
  Type  get=0x04DAC3B0
  typeStatic  get=0x04DAC3B0
METHODS:
  RVA=0x04DAC640  token=0x6000F6B  UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDBluetoothOutputReport Create(UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload, System.Byte outputSequenceId, System.Int32 outputReportSize)
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport
TYPE:  sealed struct
TOKEN: 0x2000169
SIZE:  0x19
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public    static  UnityEngine.InputSystem.Utilities.FourCCFormat  // static @ 0x0
  public            System.Byte                     leftStickX  // 0x10
  public            System.Byte                     leftStickY  // 0x11
  public            System.Byte                     rightStickX  // 0x12
  public            System.Byte                     rightStickY  // 0x13
  public            System.Byte                     buttons1  // 0x14
  public            System.Byte                     buttons2  // 0x15
  public            System.Byte                     buttons3  // 0x16
  public            System.Byte                     leftTrigger  // 0x17
  public            System.Byte                     rightTrigger  // 0x18
PROPERTIES:
  format  get=0x0B22CDA0
METHODS:
  RVA=0x0B22CD64  token=0x6000F6D  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualShock3HIDInputReport
TYPE:  sealed struct
TOKEN: 0x200016A
SIZE:  0x30
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private           System.UInt16                   padding1  // 0x10
  public            System.Byte                     buttons1  // 0x12
  public            System.Byte                     buttons2  // 0x13
  public            System.Byte                     buttons3  // 0x14
  private           System.Byte                     padding2  // 0x15
  public            System.Byte                     leftStickX  // 0x16
  public            System.Byte                     leftStickY  // 0x17
  public            System.Byte                     rightStickX  // 0x18
  public            System.Byte                     rightStickY  // 0x19
  private           UnityEngine.InputSystem.DualShock.LowLevel.DualShock3HIDInputReport.<padding3>e__FixedBufferpadding3  // 0x1a
  public            System.Byte                     leftTrigger  // 0x22
  public            System.Byte                     rightTrigger  // 0x23
PROPERTIES:
  format  get=0x04DAC340
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport
TYPE:  sealed struct
TOKEN: 0x200016C
SIZE:  0x38
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  private   static  System.Int32                    kReportId  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Byte                     reportId  // 0x18
  public            System.Byte                     flags  // 0x19
  public            UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.<unknown1>e__FixedBufferunknown1  // 0x1a
  public            System.Byte                     highFrequencyMotorSpeed  // 0x1c
  public            System.Byte                     lowFrequencyMotorSpeed  // 0x1d
  public            System.Byte                     redColor  // 0x1e
  public            System.Byte                     greenColor  // 0x1f
  public            System.Byte                     blueColor  // 0x20
  public            UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.<unknown2>e__FixedBufferunknown2  // 0x21
PROPERTIES:
  Type  get=0x04DAC3B0
  typeStatic  get=0x04DAC3B0
METHODS:
  RVA=0x0B22D138  token=0x6000F71  System.Void SetMotorSpeeds(System.Single lowFreq, System.Single highFreq)
  RVA=0x0B22D0C0  token=0x6000F72  System.Void SetColor(UnityEngine.Color color)
  RVA=0x04DAC820  token=0x6000F73  UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport Create(System.Int32 outputReportSize)
END_CLASS

CLASS: UnityEngine.InputSystem.Editor.SampleFrequencyCalculator
TYPE:  sealed struct
TOKEN: 0x2000170
SIZE:  0x28
FIELDS:
  private           System.Double                   m_LastUpdateTime  // 0x10
  private           System.Int32                    m_SampleCount  // 0x18
  private           System.Single                   <targetFrequency>k__BackingField  // 0x1c
  private           System.Single                   <frequency>k__BackingField  // 0x20
PROPERTIES:
  targetFrequency  get=0x04D9CC80  set=0x04DACD70
  frequency  get=0x04D85F70  set=0x04D88280
METHODS:
  RVA=0x04DACD50  token=0x6000F74  System.Void .ctor(System.Single targetFrequency, System.Double realtimeSinceStartup)
  RVA=0x0B235C00  token=0x6000F79  System.Void ProcessSample(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B235C48  token=0x6000F7A  System.Boolean Update()
  RVA=0x0B235C0C  token=0x6000F7B  System.Boolean Update(System.Double realtimeSinceStartup)
END_CLASS

CLASS: UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport
TYPE:  static class
TOKEN: 0x200014F
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    s_Enabled  // static @ 0x0
  private   static  UnityEngine.InputSystem.InputSettings.UpdateModes_UpdateMode  // static @ 0x4
PROPERTIES:
  enabled  get=0x0B22D628
METHODS:
  RVA=0x03DFAA20  token=0x6000EA6  System.Void Enable()
  RVA=0x0B22D228  token=0x6000EA7  System.Void Disable()
  RVA=0x0B22D3F8  token=0x6000EA8  System.Void Reset()
  RVA=0x03DF96D0  token=0x6000EA9  System.Void SetUpState()
  RVA=0x0B22D49C  token=0x6000EAA  System.Void TearDownState()
  RVA=0x03DF9B20  token=0x6000EAB  System.Void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x0B22D380  token=0x6000EAC  System.Void OnSettingsChange()
  RVA=0x0B22D1B4  token=0x6000EAD  System.Void CheckEnabled()
END_CLASS

CLASS: UnityEngine.InputSystem.EnhancedTouch.Finger
TYPE:  class
TOKEN: 0x2000150
SIZE:  0x28
FIELDS:
  private   readonly UnityEngine.InputSystem.Touchscreen<screen>k__BackingField  // 0x10
  private   readonly System.Int32                    <index>k__BackingField  // 0x18
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistory<UnityEngine.InputSystem.LowLevel.TouchState>m_StateHistory  // 0x20
PROPERTIES:
  screen  get=0x02B2ECC0
  index  get=0x011EF5B0
  isActive  get=0x0B22E798
  screenPosition  get=0x0B22E8D0
  lastTouch  get=0x0B22E800
  currentTouch  get=0x0B22E69C
  touchHistory  get=0x0B22E95C
METHODS:
  RVA=0x0B22E39C  token=0x6000EB5  System.Void .ctor(UnityEngine.InputSystem.Touchscreen screen, System.Int32 index, UnityEngine.InputSystem.LowLevel.InputUpdateType updateMask)
  RVA=0x0B22E2F8  token=0x6000EB6  System.Boolean ShouldRecordTouch(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B22E044  token=0x6000EB7  System.Void OnTouchRecorded(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
  RVA=0x0B22DC28  token=0x6000EB8  UnityEngine.InputSystem.EnhancedTouch.Touch FindTouch(System.UInt32 uniqueId)
  RVA=0x0B22DE2C  token=0x6000EB9  UnityEngine.InputSystem.EnhancedTouch.TouchHistory GetTouchHistory(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
END_CLASS

CLASS: UnityEngine.InputSystem.EnhancedTouch.Touch
TYPE:  sealed struct
TOKEN: 0x2000151
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly UnityEngine.InputSystem.EnhancedTouch.Fingerm_Finger  // 0x10
  private           UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<UnityEngine.InputSystem.LowLevel.TouchState>m_TouchRecord  // 0x18
  private   static  UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalStates_GlobalState  // static @ 0x0
PROPERTIES:
  valid  get=0x0B239130
  finger  get=0x02B76770
  phase  get=0x0B238CCC
  began  get=0x0B23885C
  inProgress  get=0x0B238B5C
  ended  get=0x0B238948
  touchId  get=0x0B239044
  pressure  get=0x0B238D1C
  radius  get=0x0B238D6C
  startTime  get=0x0B238F2C
  time  get=0x0B239008
  screen  get=0x0B238E14
  screenPosition  get=0x0B238DC0
  startScreenPosition  get=0x0B238ED8
  delta  get=0x0B2388A4
  tapCount  get=0x0B238FB8
  isTap  get=0x0B238C34
  displayIndex  get=0x0B2388F8
  isInProgress  get=0x0B238BE0
  updateStepCount  get=0x0B2390E0
  uniqueId  get=0x0B239090
  state  get=0x0B238F7C
  extraData  get=0x0B2389B0
  history  get=0x0B238A6C
  activeTouches  get=0x042EEF60
  fingers  get=0x0B2389EC
  activeFingers  get=0x0B2387C0
  screens  get=0x0B238E68
  maxHistoryLengthPerFinger  get=0x0B238C7C
EVENTS:
  onFingerDown  add=add_onFingerDown  remove=remove_onFingerDown
  onFingerUp  add=add_onFingerUp  remove=remove_onFingerUp
  onFingerMove  add=add_onFingerMove  remove=remove_onFingerMove
METHODS:
  RVA=0x043106D0  token=0x6000EDD  System.Void .ctor(UnityEngine.InputSystem.EnhancedTouch.Finger finger, UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<UnityEngine.InputSystem.LowLevel.TouchState> touchRecord)
  RVA=0x0B238318  token=0x6000EDE  System.String ToString()
  RVA=0x0B237DEC  token=0x6000EDF  System.Boolean Equals(UnityEngine.InputSystem.EnhancedTouch.Touch other)
  RVA=0x0B237E78  token=0x6000EE0  System.Boolean Equals(System.Object obj)
  RVA=0x0B237F14  token=0x6000EE1  System.Int32 GetHashCode()
  RVA=0x0B237D64  token=0x6000EE2  System.Void AddTouchscreen(UnityEngine.InputSystem.Touchscreen screen)
  RVA=0x0B237F78  token=0x6000EE3  System.Void RemoveTouchscreen(UnityEngine.InputSystem.Touchscreen screen)
  RVA=0x03F953B0  token=0x6000EE4  System.Void BeginUpdate()
  RVA=0x04DACE00  token=0x6000EE5  UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState CreateGlobalState()
  RVA=0x0B238038  token=0x6000EE6  UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
  RVA=0x04B88FB0  token=0x6000EE7  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.EnhancedTouch.TouchHistory
TYPE:  sealed struct
TOKEN: 0x2000156
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputStateHistory<UnityEngine.InputSystem.LowLevel.TouchState>m_History  // 0x10
  private   readonly UnityEngine.InputSystem.EnhancedTouch.Fingerm_Finger  // 0x18
  private   readonly System.Int32                    m_Count  // 0x20
  private   readonly System.Int32                    m_StartIndex  // 0x24
  private   readonly System.UInt32                   m_Version  // 0x28
PROPERTIES:
  Count  get=0x02B2E2D0
  Item  get=0x0B235E2C
METHODS:
  RVA=0x0B235DC0  token=0x6000EF1  System.Void .ctor(UnityEngine.InputSystem.EnhancedTouch.Finger finger, UnityEngine.InputSystem.LowLevel.InputStateHistory<UnityEngine.InputSystem.LowLevel.TouchState> history, System.Int32 startIndex, System.Int32 count)
  RVA=0x0B235D38  token=0x6000EF2  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.EnhancedTouch.Touch> GetEnumerator()
  RVA=0x0B235DB8  token=0x6000EF3  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B235C6C  token=0x6000EF6  System.Void CheckValid()
END_CLASS

CLASS: UnityEngine.InputSystem.EnhancedTouch.TouchSimulation
TYPE:  class
TOKEN: 0x2000158
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
FIELDS:
  private           UnityEngine.InputSystem.Touchscreen<simulatedTouchscreen>k__BackingField  // 0x18
  private           System.Int32                    m_NumPointers  // 0x20
  private           UnityEngine.InputSystem.Pointer[]m_Pointers  // 0x28
  private           UnityEngine.Vector2[]           m_CurrentPositions  // 0x30
  private           System.Int32[]                  m_CurrentDisplayIndices  // 0x38
  private           UnityEngine.InputSystem.Controls.ButtonControl[]m_Touches  // 0x40
  private           System.Int32[]                  m_TouchIds  // 0x48
  private           System.Int32                    m_LastTouchId  // 0x50
  private           System.Action<UnityEngine.InputSystem.InputDevice,UnityEngine.InputSystem.InputDeviceChange>m_OnDeviceChange  // 0x58
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice>m_OnEvent  // 0x60
  private   static  UnityEngine.InputSystem.EnhancedTouch.TouchSimulations_Instance  // static @ 0x0
PROPERTIES:
  simulatedTouchscreen  get=0x0385B100  set=0x05392C40
  instance  get=0x0B2379B4
METHODS:
  RVA=0x0B236218  token=0x6000F00  System.Void Enable()
  RVA=0x0B2361AC  token=0x6000F01  System.Void Disable()
  RVA=0x0B2360E0  token=0x6000F02  System.Void Destroy()
  RVA=0x0B235F9C  token=0x6000F03  System.Void AddPointer(UnityEngine.InputSystem.Pointer pointer)
  RVA=0x0B2375FC  token=0x6000F04  System.Void RemovePointer(UnityEngine.InputSystem.Pointer pointer)
  RVA=0x0B236974  token=0x6000F05  System.Void OnEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B236340  token=0x6000F06  System.Void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x0B236534  token=0x6000F07  System.Void OnEnable()
  RVA=0x0B23640C  token=0x6000F08  System.Void OnDisable()
  RVA=0x0B2377AC  token=0x6000F09  System.Void UpdateTouch(System.Int32 touchIndex, System.Int32 pointerIndex, UnityEngine.InputSystem.TouchPhase phase, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x041E1670  token=0x6000F0A  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 monitorIndex)
  RVA=0x041E1670  token=0x6000F0B  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(UnityEngine.InputSystem.InputControl control, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
  RVA=0x041E1670  token=0x6000F0C  System.Void InstallStateChangeMonitors(System.Int32 startIndex)
  RVA=0x041E1670  token=0x6000F0D  System.Void OnSourceControlChangedValue(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 sourceDeviceAndButtonIndex)
  RVA=0x041E1670  token=0x6000F0E  System.Void UninstallStateChangeMonitors(System.Int32 startIndex)
  RVA=0x05393520  token=0x6000F0F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.HID.HID
TYPE:  class
TOKEN: 0x2000139
SIZE:  0x1C8
EXTENDS: UnityEngine.InputSystem.InputDevice
FIELDS:
  private   static  System.String                   kHIDInterface  // const
  private   static  System.String                   kHIDNamespace  // const
  private           System.Boolean                  m_HaveParsedHIDDescriptor  // 0x190
  private           UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorm_HIDDescriptor  // 0x198
  private   static readonly Unity.Profiling.ProfilerMarker  k_HIDParseDescriptorFallback  // static @ 0x0
PROPERTIES:
  QueryHIDReportDescriptorDeviceCommandType  get=0x04DAC280
  QueryHIDReportDescriptorSizeDeviceCommandType  get=0x04DAC290
  QueryHIDParsedReportDescriptorDeviceCommandType  get=0x04DAC270
  hidDescriptor  get=0x0B221DFC
METHODS:
  RVA=0x03AE96F0  token=0x6000E67  System.String OnFindLayoutForDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription& description, System.String matchedLayout, UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeDeviceCommand)
  RVA=0x03AE9AB0  token=0x6000E68  UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor ReadHIDDeviceDescriptor(UnityEngine.InputSystem.Layouts.InputDeviceDescription& deviceDescription, UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeCommandDelegate)
  RVA=0x0B221D14  token=0x6000E69  System.String UsagePageToString(UnityEngine.InputSystem.HID.HID.UsagePage usagePage)
  RVA=0x0B221D80  token=0x6000E6A  System.String UsageToString(UnityEngine.InputSystem.HID.HID.UsagePage usagePage, System.Int32 usage)
  RVA=0x053971D4  token=0x6000E6B  System.Void .ctor()
  RVA=0x04D38F00  token=0x6000E6C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.HID.HIDParser
TYPE:  static class
TOKEN: 0x2000148
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B230914  token=0x6000E96  System.Boolean ParseReportDescriptor(System.Byte[] buffer, UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor& deviceDescriptor)
  RVA=0x0B2309A8  token=0x6000E97  System.Boolean ParseReportDescriptor(System.Byte* bufferPtr, System.Int32 bufferLength, UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor& deviceDescriptor)
  RVA=0x0B2316B8  token=0x6000E98  System.Int32 ReadData(System.Int32 itemSize, System.Byte* currentPtr, System.Byte* endPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.HID.HIDSupport
TYPE:  static class
TOKEN: 0x200014D
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage[]s_SupportedHIDUsages  // static @ 0x0
PROPERTIES:
  supportedHIDUsages  get=0x03AE9A00  set=0x0B231834
METHODS:
  RVA=0x03DF7230  token=0x6000EA2  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.InputSystem.Haptics.DualMotorRumble
TYPE:  sealed struct
TOKEN: 0x2000171
SIZE:  0x18
FIELDS:
  private           System.Single                   <lowFrequencyMotorSpeed>k__BackingField  // 0x10
  private           System.Single                   <highFrequencyMotorSpeed>k__BackingField  // 0x14
PROPERTIES:
  lowFrequencyMotorSpeed  get=0x04D871D0  set=0x04D932C0
  highFrequencyMotorSpeed  get=0x04D9C560  set=0x04DAB920
  isRumbling  get=0x0B22B4B4
METHODS:
  RVA=0x0B22B18C  token=0x6000F81  System.Void PauseHaptics(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B22B2FC  token=0x6000F82  System.Void ResumeHaptics(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B22B264  token=0x6000F83  System.Void ResetHaptics(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B22B398  token=0x6000F84  System.Void SetMotorSpeeds(UnityEngine.InputSystem.InputDevice device, System.Single lowFrequency, System.Single highFrequency)
END_CLASS

CLASS: UnityEngine.InputSystem.Haptics.IDualMotorRumble
TYPE:  interface
TOKEN: 0x2000172
IMPLEMENTS: UnityEngine.InputSystem.Haptics.IHaptics
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000F85  System.Void SetMotorSpeeds(System.Single lowFrequency, System.Single highFrequency)
END_CLASS

CLASS: UnityEngine.InputSystem.Haptics.IHaptics
TYPE:  interface
TOKEN: 0x2000173
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000F86  System.Void PauseHaptics()
  RVA=-1  // abstract  token=0x6000F87  System.Void ResumeHaptics()
  RVA=-1  // abstract  token=0x6000F88  System.Void ResetHaptics()
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.HoldInteraction
TYPE:  class
TOKEN: 0x200022A
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction
FIELDS:
  public            System.Single                   duration  // 0x10
  public            System.Single                   pressPoint  // 0x14
  private           System.Double                   m_TimePressed  // 0x18
PROPERTIES:
  durationOrDefault  get=0x0B1B80F4
  pressPointOrDefault  get=0x0B1B8150
METHODS:
  RVA=0x0B1B8008  token=0x6001460  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=0x04DAB9F0  token=0x6001461  System.Void Reset()
  RVA=0x041E1670  token=0x6001462  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.MultiTapInteraction
TYPE:  class
TOKEN: 0x200022B
SIZE:  0x38
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction`1 UnityEngine.InputSystem.IInputInteraction
FIELDS:
  public            System.Single                   tapTime  // 0x10
  public            System.Single                   tapDelay  // 0x14
  public            System.Int32                    tapCount  // 0x18
  public            System.Single                   pressPoint  // 0x1c
  private           UnityEngine.InputSystem.Interactions.MultiTapInteraction.TapPhasem_CurrentTapPhase  // 0x20
  private           System.Int32                    m_CurrentTapCount  // 0x24
  private           System.Double                   m_CurrentTapStartTime  // 0x28
  private           System.Double                   m_LastTapReleaseTime  // 0x30
PROPERTIES:
  tapTimeOrDefault  get=0x0B1C0CF4
  tapDelayOrDefault  get=0x0B1C0C98
  pressPointOrDefault  get=0x0B1C0C04
  releasePointOrDefault  get=0x0B1C0C50
METHODS:
  RVA=0x0B1C0A30  token=0x6001467  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=0x04DABAC0  token=0x6001468  System.Void Reset()
  RVA=0x04D8D750  token=0x6001469  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.PressInteraction
TYPE:  class
TOKEN: 0x200022D
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction
FIELDS:
  public            System.Single                   pressPoint  // 0x10
  public            UnityEngine.InputSystem.Interactions.PressBehaviorbehavior  // 0x14
  private           System.Boolean                  m_WaitingForRelease  // 0x18
PROPERTIES:
  pressPointOrDefault  get=0x0B1C0FCC
  releasePointOrDefault  get=0x0B1C1018
METHODS:
  RVA=0x0B1C0D50  token=0x600146C  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=0x04D91AA0  token=0x600146D  System.Void Reset()
  RVA=0x041E1670  token=0x600146E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.PressBehavior
TYPE:  sealed struct
TOKEN: 0x200022E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Interactions.PressBehaviorPressOnly  // const
  public    static  UnityEngine.InputSystem.Interactions.PressBehaviorReleaseOnly  // const
  public    static  UnityEngine.InputSystem.Interactions.PressBehaviorPressAndRelease  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.SlowTapInteraction
TYPE:  class
TOKEN: 0x200022F
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction
FIELDS:
  public            System.Single                   duration  // 0x10
  public            System.Single                   pressPoint  // 0x14
  private           System.Double                   m_SlowTapStartTime  // 0x18
PROPERTIES:
  durationOrDefault  get=0x0B1C1780
  pressPointOrDefault  get=0x0B1C17DC
METHODS:
  RVA=0x0B1C16D4  token=0x6001471  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=0x04DAB9F0  token=0x6001472  System.Void Reset()
  RVA=0x041E1670  token=0x6001473  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Interactions.TapInteraction
TYPE:  class
TOKEN: 0x2000230
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.IInputInteraction
FIELDS:
  public            System.Single                   duration  // 0x10
  public            System.Single                   pressPoint  // 0x14
  private           System.Double                   m_TapStartTime  // 0x18
PROPERTIES:
  durationOrDefault  get=0x0B1C1A0C
  pressPointOrDefault  get=0x0B1C1A68
  releasePointOrDefault  get=0x0B1C1AB4
METHODS:
  RVA=0x0B1C1930  token=0x6001477  System.Void Process(UnityEngine.InputSystem.InputInteractionContext& context)
  RVA=0x04DAB9F0  token=0x6001478  System.Void Reset()
  RVA=0x041E1670  token=0x6001479  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputControlAttribute
TYPE:  sealed class
TOKEN: 0x20001F6
SIZE:  0xA8
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  private           System.String                   <layout>k__BackingField  // 0x10
  private           System.String                   <variants>k__BackingField  // 0x18
  private           System.String                   <name>k__BackingField  // 0x20
  private           System.String                   <format>k__BackingField  // 0x28
  private           System.String                   <usage>k__BackingField  // 0x30
  private           System.String[]                 <usages>k__BackingField  // 0x38
  private           System.String                   <parameters>k__BackingField  // 0x40
  private           System.String                   <processors>k__BackingField  // 0x48
  private           System.String                   <alias>k__BackingField  // 0x50
  private           System.String[]                 <aliases>k__BackingField  // 0x58
  private           System.String                   <useStateFrom>k__BackingField  // 0x60
  private           System.UInt32                   <bit>k__BackingField  // 0x68
  private           System.UInt32                   <offset>k__BackingField  // 0x6c
  private           System.UInt32                   <sizeInBits>k__BackingField  // 0x70
  private           System.Int32                    <arraySize>k__BackingField  // 0x74
  private           System.String                   <displayName>k__BackingField  // 0x78
  private           System.String                   <shortDisplayName>k__BackingField  // 0x80
  private           System.Boolean                  <noisy>k__BackingField  // 0x88
  private           System.Boolean                  <synthetic>k__BackingField  // 0x89
  private           System.Boolean                  <dontReset>k__BackingField  // 0x8a
  private           System.Object                   <defaultState>k__BackingField  // 0x90
  private           System.Object                   <minValue>k__BackingField  // 0x98
  private           System.Object                   <maxValue>k__BackingField  // 0xa0
PROPERTIES:
  layout  get=0x02B2ECC0  set=0x053908C0
  variants  get=0x0385B100  set=0x05392C40
  name  get=0x04D862C0  set=0x05390F40
  format  get=0x04D86240  set=0x02FDB880
  usage  get=0x011F36E0  set=0x05396200
  usages  get=0x04D85A50  set=0x046A91B0
  parameters  get=0x04D85A60  set=0x042B4AE0
  processors  get=0x04D86200  set=0x035A41A0
  alias  get=0x04D86270  set=0x02FDAEA0
  aliases  get=0x02E56440  set=0x036E5590
  useStateFrom  get=0x04D86280  set=0x063CC164
  bit  get=0x04D86CF0  set=0x04D86D00
  offset  get=0x04D86B50  set=0x04D86BB0
  sizeInBits  get=0x04D868A0  set=0x04D868D0
  arraySize  get=0x04D868B0  set=0x04D868E0
  displayName  get=0x04D862A0  set=0x06402320
  shortDisplayName  get=0x04D86260  set=0x04CD9970
  noisy  get=0x011F2750  set=0x011F2760
  synthetic  get=0x04D89060  set=0x04D90C50
  dontReset  get=0x04D890B0  set=0x04DA9BE0
  defaultState  get=0x04D86220  set=0x06402330
  minValue  get=0x04D85EE0  set=0x06352D0C
  maxValue  get=0x04D86210  set=0x05391334
METHODS:
  RVA=0x04DACEC0  token=0x600128A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputDeviceFindControlLayoutDelegate
TYPE:  sealed class
TOKEN: 0x20001F7
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03DF73C0  token=0x600128B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x600128C  System.String Invoke(UnityEngine.InputSystem.Layouts.InputDeviceDescription& description, System.String matchedLayout, UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeDeviceCommand)
  RVA=0x0B242808  token=0x600128D  System.IAsyncResult BeginInvoke(UnityEngine.InputSystem.Layouts.InputDeviceDescription& description, System.String matchedLayout, UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeDeviceCommand, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD0344  token=0x600128E  System.String EndInvoke(UnityEngine.InputSystem.Layouts.InputDeviceDescription& description, System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputControlLayout
TYPE:  class
TOKEN: 0x20001F8
SIZE:  0xB0
FIELDS:
  private   static  UnityEngine.InputSystem.Utilities.InternedStrings_DefaultVariant  // static @ 0x0
  public    static  System.String                   VariantSeparator  // const
  private           UnityEngine.InputSystem.Utilities.InternedStringm_Name  // 0x10
  private           System.Type                     m_Type  // 0x20
  private           UnityEngine.InputSystem.Utilities.InternedStringm_Variants  // 0x28
  private           UnityEngine.InputSystem.Utilities.FourCCm_StateFormat  // 0x38
  private           System.Int32                    m_StateSizeInBytes  // 0x3c
  private           System.Nullable<System.Boolean> m_UpdateBeforeRender  // 0x40
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Utilities.InternedString>m_BaseLayouts  // 0x48
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Utilities.InternedString>m_AppliedOverrides  // 0x68
  private           UnityEngine.InputSystem.Utilities.InternedString[]m_CommonUsages  // 0x88
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[]m_Controls  // 0x90
  private           System.String                   m_DisplayName  // 0x98
  private           System.String                   m_Description  // 0xa0
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.Flagsm_Flags  // 0xa8
  private   static  UnityEngine.InputSystem.Layouts.InputControlLayout.Collections_Layouts  // static @ 0x10
  private   static  UnityEngine.InputSystem.Layouts.InputControlLayout.Caches_CacheInstance  // static @ 0x50
  private   static  System.Int32                    s_CacheInstanceRef  // static @ 0x58
PROPERTIES:
  DefaultVariant  get=0x0B2421F0
  name  get=0x02B2D510
  displayName  get=0x0B2425D8
  type  get=0x04D862C0
  variants  get=0x04D8FAD0
  stateFormat  get=0x04D863B0
  stateSizeInBytes  get=0x04D86640
  baseLayouts  get=0x0B24242C
  appliedOverrides  get=0x0B2423DC
  commonUsages  get=0x0B242528
  controls  get=0x0B242580
  updateBeforeRender  get=0x0B2426C0
  isDeviceLayout  get=0x0B242624
  isControlLayout  get=0x0B242610
  isOverride  get=0x0B2426B4  set=0x0B2427E8
  isGenericTypeOfDevice  get=0x0B24269C  set=0x0B2427A8
  hideInUI  get=0x0B242604  set=0x0B242788
  isNoisy  get=0x0B2426A8  set=0x0B2427C8
  canRunInBackground  get=0x0B2424CC  set=0x0B2426F8
  Item  get=0x0B24224C
  cache  get=0x0B24247C
METHODS:
  RVA=0x0B23E7BC  token=0x60012A8  System.Nullable<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> FindControl(UnityEngine.InputSystem.Utilities.InternedString path)
  RVA=0x0B23E3E0  token=0x60012A9  System.Nullable<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> FindControlIncludingArrayElements(System.String path, System.Int32& arrayIndex)
  RVA=0x0B23EF98  token=0x60012AA  System.Type GetValueType()
  RVA=0x0B23EB44  token=0x60012AB  UnityEngine.InputSystem.Layouts.InputControlLayout FromType(System.String name, System.Type type)
  RVA=0x0B241F44  token=0x60012AC  System.String ToJson()
  RVA=0x0B23EAA8  token=0x60012AD  UnityEngine.InputSystem.Layouts.InputControlLayout FromJson(System.String json)
  RVA=0x0B2421A0  token=0x60012AE  System.Void .ctor(System.String name, System.Type type)
  RVA=0x0B23D66C  token=0x60012AF  System.Void AddControlItems(System.Type type, System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, System.String layoutName)
  RVA=0x0B23CEBC  token=0x60012B0  System.Void AddControlItemsFromFields(System.Type type, System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, System.String layoutName)
  RVA=0x0B23D5F0  token=0x60012B1  System.Void AddControlItemsFromProperties(System.Type type, System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, System.String layoutName)
  RVA=0x0B23D14C  token=0x60012B2  System.Void AddControlItemsFromMembers(System.Reflection.MemberInfo[] members, System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlItems, System.String layoutName)
  RVA=0x0B23CF38  token=0x60012B3  System.Void AddControlItemsFromMember(System.Reflection.MemberInfo member, UnityEngine.InputSystem.Layouts.InputControlAttribute[] attributes, System.Collections.Generic.List<UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlItems)
  RVA=0x0B23D738  token=0x60012B4  UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem CreateControlItemFromMember(System.Reflection.MemberInfo member, UnityEngine.InputSystem.Layouts.InputControlAttribute attribute)
  RVA=0x0B23F00C  token=0x60012B5  System.String InferLayoutFromValueType(System.Type type)
  RVA=0x0B23F1EC  token=0x60012B6  System.Void MergeLayout(UnityEngine.InputSystem.Layouts.InputControlLayout other)
  RVA=0x0B23E05C  token=0x60012B7  System.Collections.Generic.Dictionary<System.String,UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> CreateLookupTableForControls(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] controlItems, System.Collections.Generic.List<System.String> variants)
  RVA=0x0B24205C  token=0x60012B8  System.Boolean VariantsMatch(UnityEngine.InputSystem.Utilities.InternedString expected, UnityEngine.InputSystem.Utilities.InternedString actual)
  RVA=0x0B2420B8  token=0x60012B9  System.Boolean VariantsMatch(System.String expected, System.String actual)
  RVA=0x03DF76B0  token=0x60012BA  System.Void ParseHeaderFieldsFromJson(System.String json, UnityEngine.InputSystem.Utilities.InternedString& name, UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.Utilities.InternedString>& baseLayouts, UnityEngine.InputSystem.Layouts.InputDeviceMatcher& deviceMatcher)
  RVA=0x0B23D6E0  token=0x60012BC  UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRefInstance CacheRef()
  RVA=0x04D29060  token=0x60012BD  System.Void .cctor()
  RVA=0x0B241FE8  token=0x60012BE  System.Boolean <MergeLayout>b__77_0(UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem x)
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputControlLayoutAttribute
TYPE:  sealed class
TOKEN: 0x200020C
SIZE:  0x50
EXTENDS: System.Attribute
FIELDS:
  private           System.Type                     <stateType>k__BackingField  // 0x10
  private           System.String                   <stateFormat>k__BackingField  // 0x18
  private           System.String[]                 <commonUsages>k__BackingField  // 0x20
  private           System.String                   <variants>k__BackingField  // 0x28
  private           System.Boolean                  <isNoisy>k__BackingField  // 0x30
  private           System.Nullable<System.Boolean> canRunInBackgroundInternal  // 0x31
  private           System.Nullable<System.Boolean> updateBeforeRenderInternal  // 0x33
  private           System.Boolean                  <isGenericTypeOfDevice>k__BackingField  // 0x35
  private           System.String                   <displayName>k__BackingField  // 0x38
  private           System.String                   <description>k__BackingField  // 0x40
  private           System.Boolean                  <hideInUI>k__BackingField  // 0x48
PROPERTIES:
  stateType  get=0x02B2ECC0  set=0x053908C0
  stateFormat  get=0x0385B100  set=0x05392C40
  commonUsages  get=0x04D862C0  set=0x05390F40
  variants  get=0x04D86240  set=0x02FDB880
  isNoisy  get=0x04D865F0  set=0x04D86620
  canRunInBackground  get=0x0B1B92D0  set=0x0B1B9348
  updateBeforeRender  get=0x0B1B930C  set=0x0B1B9394
  isGenericTypeOfDevice  get=0x04D86D30  set=0x04D86D40
  displayName  get=0x04D85A50  set=0x046A91B0
  description  get=0x04D85A60  set=0x042B4AE0
  hideInUI  get=0x04D867D0  set=0x04D86810
METHODS:
  RVA=0x041E1670  token=0x600136E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputDeviceBuilder
TYPE:  sealed struct
TOKEN: 0x200020D
SIZE:  0x38
IMPLEMENTS: System.IDisposable
FIELDS:
  private           UnityEngine.InputSystem.InputDevicem_Device  // 0x10
  private           UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRefInstancem_LayoutCacheRef  // 0x18
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>m_ChildControlOverrides  // 0x20
  private           System.Collections.Generic.List<System.UInt32>m_StateOffsetToControlMap  // 0x28
  private           System.Text.StringBuilder       m_StringBuilder  // 0x30
  private   static  System.UInt32                   kSizeForControlUsingStateFromOtherControl  // const
  private   static  UnityEngine.InputSystem.Layouts.InputDeviceBuilders_Instance  // static @ 0x0
  private   static  System.Int32                    s_InstanceRef  // static @ 0x28
PROPERTIES:
  instance  get=0x0B1BDC40
METHODS:
  RVA=0x0B1BDA70  token=0x600136F  System.Void Setup(UnityEngine.InputSystem.Utilities.InternedString layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
  RVA=0x0B1BBC6C  token=0x6001370  UnityEngine.InputSystem.InputDevice Finish()
  RVA=0x0B1BB510  token=0x6001371  System.Void Dispose()
  RVA=0x0B1BD704  token=0x6001372  System.Void Reset()
  RVA=0x0B1BD644  token=0x6001373  UnityEngine.InputSystem.InputControl InstantiateLayout(UnityEngine.InputSystem.Utilities.InternedString layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.Utilities.InternedString name, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1BCF9C  token=0x6001374  UnityEngine.InputSystem.InputControl InstantiateLayout(UnityEngine.InputSystem.Layouts.InputControlLayout layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.Utilities.InternedString name, UnityEngine.InputSystem.InputControl parent)
  RVA=0x0B1B9DC0  token=0x6001375  System.Void AddChildControls(UnityEngine.InputSystem.Layouts.InputControlLayout layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.InputControl parent, System.Boolean& haveChildrenUsingStateFromOtherControls)
  RVA=0x0B1B94A8  token=0x6001376  UnityEngine.InputSystem.InputControl AddChildControl(UnityEngine.InputSystem.Layouts.InputControlLayout layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.InputControl parent, System.Boolean& haveChildrenUsingStateFromOtherControls, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem, System.Int32 childIndex, System.String nameOverride)
  RVA=0x0B1BC55C  token=0x6001377  System.Void InsertChildControlOverride(UnityEngine.InputSystem.InputControl parent, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem)
  RVA=0x0B1BABF8  token=0x6001378  System.String ChildControlOverridePath(UnityEngine.InputSystem.InputControl parent, UnityEngine.InputSystem.Utilities.InternedString controlName)
  RVA=0x0B1B93E0  token=0x6001379  System.Void AddChildControlIfMissing(UnityEngine.InputSystem.Layouts.InputControlLayout layout, UnityEngine.InputSystem.Utilities.InternedString variants, UnityEngine.InputSystem.InputControl parent, System.Boolean& haveChildrenUsingStateFromOtherControls, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem)
  RVA=0x0B1BC894  token=0x600137A  UnityEngine.InputSystem.InputControl InsertChildControl(UnityEngine.InputSystem.Layouts.InputControlLayout layout, UnityEngine.InputSystem.Utilities.InternedString variant, UnityEngine.InputSystem.InputControl parent, System.Boolean& haveChildrenUsingStateFromOtherControls, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem)
  RVA=0x0B1BAA24  token=0x600137B  System.Void ApplyUseStateFrom(UnityEngine.InputSystem.InputControl parent, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem, UnityEngine.InputSystem.Layouts.InputControlLayout layout)
  RVA=0x0B1BDBDC  token=0x600137C  System.Void ShiftChildIndicesInHierarchyOneUp(UnityEngine.InputSystem.InputDevice device, System.Int32 startIndex, UnityEngine.InputSystem.InputControl exceptControl)
  RVA=0x0B1BD76C  token=0x600137D  System.Void SetDisplayName(UnityEngine.InputSystem.InputControl control, System.String longDisplayNameFromLayout, System.String shortDisplayNameFromLayout, System.Boolean shortName)
  RVA=0x0B1BA778  token=0x600137E  System.Void AddParentDisplayNameRecursive(UnityEngine.InputSystem.InputControl control, System.Text.StringBuilder stringBuilder, System.Boolean shortName)
  RVA=0x0B1BA814  token=0x600137F  System.Void AddProcessors(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem& controlItem, System.String layoutName)
  RVA=0x0B1BD9E0  token=0x6001380  System.Void SetFormat(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem)
  RVA=0x0B1BBC1C  token=0x6001381  UnityEngine.InputSystem.Layouts.InputControlLayout FindOrLoadLayout(System.String name)
  RVA=0x0B1BAC70  token=0x6001382  System.Void ComputeStateLayout(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1BB988  token=0x6001383  System.Void FinalizeControlHierarchy()
  RVA=0x0B1BB51C  token=0x6001384  System.Void FinalizeControlHierarchyRecursive(UnityEngine.InputSystem.InputControl control, System.Int32 controlIndex, UnityEngine.InputSystem.InputControl[] allControls, System.Boolean noisy, System.Boolean dontReset, System.Int32& controlIndiciesNextFreeIndex)
  RVA=0x0B1BCCA0  token=0x6001385  System.Void InsertControlBitRangeNode(UnityEngine.InputSystem.InputDevice.ControlBitRangeNode& parent, UnityEngine.InputSystem.InputControl control, System.Int32& controlIndiciesNextFreeIndex, System.UInt16 startOffset)
  RVA=0x0B1BC034  token=0x6001386  System.UInt16 GetBestMidPoint(UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parent, System.UInt16 startOffset)
  RVA=0x0B1BA61C  token=0x6001387  System.Void AddControlToNode(UnityEngine.InputSystem.InputControl control, System.Int32& controlIndiciesNextFreeIndex, System.Int32 nodeIndex)
  RVA=0x0B1BA4EC  token=0x6001388  System.Void AddChildren(UnityEngine.InputSystem.InputDevice.ControlBitRangeNode& parent, UnityEngine.InputSystem.InputDevice.ControlBitRangeNode left, UnityEngine.InputSystem.InputDevice.ControlBitRangeNode right)
  RVA=0x0B1BC494  token=0x6001389  System.UInt16 GetControlIndex(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B1BD6C4  token=0x600138B  UnityEngine.InputSystem.Layouts.InputDeviceBuilder.RefInstance Ref()
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputDeviceDescription
TYPE:  sealed struct
TOKEN: 0x200020F
SIZE:  0x48
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.String                   m_InterfaceName  // 0x10
  private           System.String                   m_DeviceClass  // 0x18
  private           System.String                   m_Manufacturer  // 0x20
  private           System.String                   m_Product  // 0x28
  private           System.String                   m_Serial  // 0x30
  private           System.String                   m_Version  // 0x38
  private           System.String                   m_Capabilities  // 0x40
PROPERTIES:
  interfaceName  get=0x02B76770  set=0x05392320
  deviceClass  get=0x04D88320  set=0x067D5E9C
  manufacturer  get=0x02B2ECC0  set=0x053908C0
  product  get=0x0385B100  set=0x05392C40
  serial  get=0x04D862C0  set=0x05390F40
  version  get=0x04D86240  set=0x02FDB880
  capabilities  get=0x011F36E0  set=0x05396200
  empty  get=0x0B1BE2F8
METHODS:
  RVA=0x0B1BE0E4  token=0x600139C  System.String ToString()
  RVA=0x0B1BDDCC  token=0x600139D  System.Boolean Equals(UnityEngine.InputSystem.Layouts.InputDeviceDescription other)
  RVA=0x0B1BDD40  token=0x600139E  System.Boolean Equals(System.Object obj)
  RVA=0x0B1BDEC4  token=0x600139F  System.Int32 GetHashCode()
  RVA=0x0B1BE360  token=0x60013A0  System.Boolean op_Equality(UnityEngine.InputSystem.Layouts.InputDeviceDescription left, UnityEngine.InputSystem.Layouts.InputDeviceDescription right)
  RVA=0x0B1BE39C  token=0x60013A1  System.Boolean op_Inequality(UnityEngine.InputSystem.Layouts.InputDeviceDescription left, UnityEngine.InputSystem.Layouts.InputDeviceDescription right)
  RVA=0x0B1BDFD8  token=0x60013A2  System.String ToJson()
  RVA=0x03AEADB0  token=0x60013A3  UnityEngine.InputSystem.Layouts.InputDeviceDescription FromJson(System.String json)
  RVA=0x0B1BDC74  token=0x60013A4  System.Boolean ComparePropertyToDeviceDescriptor(System.String propertyName, UnityEngine.InputSystem.Utilities.JsonParser.JsonString propertyValue, System.String deviceDescriptor)
END_CLASS

CLASS: UnityEngine.InputSystem.Layouts.InputDeviceMatcher
TYPE:  sealed struct
TOKEN: 0x2000211
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.Collections.Generic.KeyValuePair<UnityEngine.InputSystem.Utilities.InternedString,System.Object>[]m_Patterns  // 0x10
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkInterfaceKey  // static @ 0x0
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkDeviceClassKey  // static @ 0x10
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkManufacturerKey  // static @ 0x20
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkManufacturerContainsKey  // static @ 0x30
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkProductKey  // static @ 0x40
  private   static readonly UnityEngine.InputSystem.Utilities.InternedStringkVersionKey  // static @ 0x50
PROPERTIES:
  empty  get=0x0314CD60
  patterns  get=0x0B1BE844
METHODS:
  RVA=0x03AF1A30  token=0x60013A7  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithInterface(System.String pattern, System.Boolean supportRegex)
  RVA=0x0B1BE73C  token=0x60013A8  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithDeviceClass(System.String pattern, System.Boolean supportRegex)
  RVA=0x04A787E0  token=0x60013A9  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithManufacturer(System.String pattern, System.Boolean supportRegex)
  RVA=0x03DF6C50  token=0x60013AA  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithManufacturerContains(System.String noRegExPattern)
  RVA=0x03AF2800  token=0x60013AB  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithProduct(System.String pattern, System.Boolean supportRegex)
  RVA=0x0B1BE7C0  token=0x60013AC  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithVersion(System.String pattern, System.Boolean supportRegex)
  RVA=-1  // generic def  token=0x60013AD  UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithCapability(System.String path, TValue value)
  RVA=0x03AF0D70  token=0x60013AE  UnityEngine.InputSystem.Layouts.InputDeviceMatcher With(UnityEngine.InputSystem.Utilities.InternedString key, System.Object value, System.Boolean supportRegex)
  RVA=0x03AECB90  token=0x60013AF  System.Single MatchPercentage(UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
  RVA=0x03AECF50  token=0x60013B0  System.Boolean MatchSingleProperty(System.Object pattern, System.String value)
  RVA=0x03AEBBA0  token=0x60013B1  System.Boolean MatchSinglePropertyContains(System.Object pattern, System.String value)
  RVA=0x0B1BE57C  token=0x60013B2  System.Int32 GetNumPropertiesIn(UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
  RVA=0x0B1BE44C  token=0x60013B3  UnityEngine.InputSystem.Layouts.InputDeviceMatcher FromDeviceDescription(UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
  RVA=0x0B1BE5F4  token=0x60013B4  System.String ToString()
  RVA=0x03AECA60  token=0x60013B5  System.Boolean Equals(UnityEngine.InputSystem.Layouts.InputDeviceMatcher other)
  RVA=0x0B1BE3D8  token=0x60013B6  System.Boolean Equals(System.Object obj)
  RVA=0x03AECA20  token=0x60013B7  System.Boolean op_Equality(UnityEngine.InputSystem.Layouts.InputDeviceMatcher left, UnityEngine.InputSystem.Layouts.InputDeviceMatcher right)
  RVA=0x0B1BE8B4  token=0x60013B8  System.Boolean op_Inequality(UnityEngine.InputSystem.Layouts.InputDeviceMatcher left, UnityEngine.InputSystem.Layouts.InputDeviceMatcher right)
  RVA=0x0860F504  token=0x60013B9  System.Int32 GetHashCode()
  RVA=0x049CE070  token=0x60013BA  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DisableDeviceCommand
TYPE:  sealed struct
TOKEN: 0x2000174
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
PROPERTIES:
  Type  get=0x04DAC5A0
  typeStatic  get=0x04DAC5A0
METHODS:
  RVA=0x04DAC580  token=0x6000F8B  UnityEngine.InputSystem.LowLevel.DisableDeviceCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.EnableDeviceCommand
TYPE:  sealed struct
TOKEN: 0x2000175
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
PROPERTIES:
  Type  get=0x04DAC870
  typeStatic  get=0x04DAC870
METHODS:
  RVA=0x04DAC850  token=0x6000F8E  UnityEngine.InputSystem.LowLevel.EnableDeviceCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand
TYPE:  sealed struct
TOKEN: 0x2000176
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  private           System.Byte                     m_ImeEnabled  // 0x18
PROPERTIES:
  Type  get=0x04DAC880
  imeEnabled  get=0x0B22D1AC
  typeStatic  get=0x04DAC880
METHODS:
  RVA=0x0B22D184  token=0x6000F92  UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand Create(System.Boolean enabled)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
TYPE:  interface
TOKEN: 0x2000177
FIELDS:
PROPERTIES:
  typeStatic  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand
TYPE:  sealed struct
TOKEN: 0x2000178
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
PROPERTIES:
  Type  get=0x04DACA00
  typeStatic  get=0x04DACA00
METHODS:
  RVA=0x04DAC9E0  token=0x6000F96  UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputDeviceCommandDelegate
TYPE:  sealed class
TOKEN: 0x200017A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x096EA178  token=0x6000F97  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06222510  token=0x6000F98  System.Nullable<System.Int64> Invoke(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputDeviceCommand* command)
  RVA=0x05E43E3C  token=0x6000F99  System.IAsyncResult BeginInvoke(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputDeviceCommand* command, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD069C  token=0x6000F9A  System.Nullable<System.Int64> EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate
TYPE:  sealed class
TOKEN: 0x200017B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04CF2260  token=0x6000F9B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014E2060  token=0x6000F9C  System.Int64 Invoke(UnityEngine.InputSystem.LowLevel.InputDeviceCommand& command)
  RVA=0x0B231DA0  token=0x6000F9D  System.IAsyncResult BeginInvoke(UnityEngine.InputSystem.LowLevel.InputDeviceCommand& command, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD2D78  token=0x6000F9E  System.Int64 EndInvoke(UnityEngine.InputSystem.LowLevel.InputDeviceCommand& command, System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputDeviceCommand
TYPE:  sealed struct
TOKEN: 0x200017C
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kBaseCommandSize  // const
  public    static  System.Int32                    BaseCommandSize  // const
  public    static  System.Int64                    GenericFailure  // const
  public    static  System.Int64                    GenericSuccess  // const
  public            UnityEngine.InputSystem.Utilities.FourCCtype  // 0x10
  public            System.Int32                    sizeInBytes  // 0x14
PROPERTIES:
  payloadSizeInBytes  get=0x04DACA10
  payloadPtr  get=0x04D94D90
  typeStatic  get=0x011EC580
METHODS:
  RVA=0x04D85C40  token=0x6000FA1  System.Void .ctor(UnityEngine.InputSystem.Utilities.FourCC type, System.Int32 sizeInBytes)
  RVA=0x03AEA670  token=0x6000FA2  Unity.Collections.NativeArray<System.Byte> AllocateNative(UnityEngine.InputSystem.Utilities.FourCC type, System.Int32 payloadSize)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryCanRunInBackground
TYPE:  sealed struct
TOKEN: 0x200017D
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Boolean                  canRunInBackground  // 0x18
PROPERTIES:
  Type  get=0x04DACAD0
  typeStatic  get=0x04DACAD0
METHODS:
  RVA=0x04750A00  token=0x6000FA6  UnityEngine.InputSystem.LowLevel.QueryCanRunInBackground Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryDimensionsCommand
TYPE:  sealed struct
TOKEN: 0x200017E
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.Vector2             outDimensions  // 0x18
PROPERTIES:
  Type  get=0x04DACB10
  typeStatic  get=0x04DACB10
METHODS:
  RVA=0x04DACAE0  token=0x6000FA9  UnityEngine.InputSystem.LowLevel.QueryDimensionsCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand
TYPE:  sealed struct
TOKEN: 0x200017F
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Boolean                  isEnabled  // 0x18
PROPERTIES:
  Type  get=0x04DACB50
  typeStatic  get=0x04DACB50
METHODS:
  RVA=0x04DACB20  token=0x6000FAC  UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand
TYPE:  sealed struct
TOKEN: 0x2000180
SIZE:  0x118
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kMaxNameLength  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand.<nameBuffer>e__FixedBuffernameBuffer  // 0x18
PROPERTIES:
  Type  get=0x04DACC00
  typeStatic  get=0x04DACC00
METHODS:
  RVA=0x0B235950  token=0x6000FAE  System.String ReadLayoutName()
  RVA=0x0B235964  token=0x6000FAF  System.Void WriteLayoutName(System.String name)
  RVA=0x04DACBC0  token=0x6000FB1  UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand
TYPE:  sealed struct
TOKEN: 0x2000182
SIZE:  0x11C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kMaxNameLength  // const
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Int32                    scanOrKeyCode  // 0x18
  public            UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand.<nameBuffer>e__FixedBuffernameBuffer  // 0x1c
PROPERTIES:
  Type  get=0x04DACBB0
  typeStatic  get=0x04DACBB0
METHODS:
  RVA=0x0B23593C  token=0x6000FB3  System.String ReadKeyName()
  RVA=0x04DACB60  token=0x6000FB5  UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand Create(UnityEngine.InputSystem.Key key)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand
TYPE:  sealed struct
TOKEN: 0x2000184
SIZE:  0x420
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kMaxNameLength  // const
  private   static  System.Int32                    kMaxIdLength  // const
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.UInt64                   handle  // 0x18
  private           UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.<nameBuffer>e__FixedBuffernameBuffer  // 0x20
  private           UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand.<idBuffer>e__FixedBufferidBuffer  // 0x220
PROPERTIES:
  Type  get=0x04DACC50
  id  get=0x0B235990  set=0x0B2359B8
  name  get=0x0B2359A4  set=0x0B235ADC
  typeStatic  get=0x04DACC50
METHODS:
  RVA=0x04DACC10  token=0x6000FBC  UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QuerySamplingFrequencyCommand
TYPE:  sealed struct
TOKEN: 0x2000188
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Single                   frequency  // 0x18
PROPERTIES:
  Type  get=0x04DACC90
  typeStatic  get=0x04DACC90
METHODS:
  RVA=0x04DACC60  token=0x6000FBF  UnityEngine.InputSystem.LowLevel.QuerySamplingFrequencyCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.QueryUserIdCommand
TYPE:  sealed struct
TOKEN: 0x2000189
SIZE:  0x218
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  public    static  System.Int32                    kMaxIdLength  // const
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.InputSystem.LowLevel.QueryUserIdCommand.<idBuffer>e__FixedBufferidBuffer  // 0x18
PROPERTIES:
  Type  get=0x04DACCE0
  typeStatic  get=0x04DACCE0
METHODS:
  RVA=0x0B235950  token=0x6000FC1  System.String ReadId()
  RVA=0x04DACCA0  token=0x6000FC3  UnityEngine.InputSystem.LowLevel.QueryUserIdCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.RequestResetCommand
TYPE:  sealed struct
TOKEN: 0x200018B
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
PROPERTIES:
  Type  get=0x04DACD10
  typeStatic  get=0x04DACD10
METHODS:
  RVA=0x04DACCF0  token=0x6000FC6  UnityEngine.InputSystem.LowLevel.RequestResetCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.RequestSyncCommand
TYPE:  sealed struct
TOKEN: 0x200018C
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
PROPERTIES:
  Type  get=0x04DACD40
  typeStatic  get=0x04DACD40
METHODS:
  RVA=0x04DACD20  token=0x6000FC9  UnityEngine.InputSystem.LowLevel.RequestSyncCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand
TYPE:  sealed struct
TOKEN: 0x200018D
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  private           UnityEngine.Vector2             m_Position  // 0x18
PROPERTIES:
  Type  get=0x04DACDA0
  position  get=0x04D9C5E0
  typeStatic  get=0x04DACDA0
METHODS:
  RVA=0x04DACD80  token=0x6000FCD  UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand Create(UnityEngine.Vector2 cursorPosition)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.SetSamplingFrequencyCommand
TYPE:  sealed struct
TOKEN: 0x200018E
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Single                   frequency  // 0x18
PROPERTIES:
  Type  get=0x04DACDE0
  typeStatic  get=0x04DACDE0
METHODS:
  RVA=0x04DACDB0  token=0x6000FD0  UnityEngine.InputSystem.LowLevel.SetSamplingFrequencyCommand Create(System.Single frequency)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.WarpMousePositionCommand
TYPE:  sealed struct
TOKEN: 0x200018F
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            UnityEngine.Vector2             warpPositionInPlayerDisplaySpace  // 0x18
PROPERTIES:
  Type  get=0x04DACE70
  typeStatic  get=0x04DACE70
METHODS:
  RVA=0x04DACE50  token=0x6000FD3  UnityEngine.InputSystem.LowLevel.WarpMousePositionCommand Create(UnityEngine.Vector2 position)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.GamepadState
TYPE:  sealed struct
TOKEN: 0x2000190
SIZE:  0x2C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private   static  System.String                   ButtonSouthShortDisplayName  // const
  private   static  System.String                   ButtonNorthShortDisplayName  // const
  private   static  System.String                   ButtonWestShortDisplayName  // const
  private   static  System.String                   ButtonEastShortDisplayName  // const
  public            System.UInt32                   buttons  // 0x10
  public            UnityEngine.Vector2             leftStick  // 0x14
  public            UnityEngine.Vector2             rightStick  // 0x1c
  public            System.Single                   leftTrigger  // 0x24
  public            System.Single                   rightTrigger  // 0x28
PROPERTIES:
  Format  get=0x04DAC8F0
  format  get=0x04DAC8F0
METHODS:
  RVA=0x0B22E9DC  token=0x6000FD6  System.Void .ctor(UnityEngine.InputSystem.LowLevel.GamepadButton[] buttons)
  RVA=0x0B22E998  token=0x6000FD7  UnityEngine.InputSystem.LowLevel.GamepadState WithButton(UnityEngine.InputSystem.LowLevel.GamepadButton button, System.Boolean value)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.GamepadButton
TYPE:  sealed struct
TOKEN: 0x2000191
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonDpadUp  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonDpadDown  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonDpadLeft  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonDpadRight  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonNorth  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonEast  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonSouth  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonWest  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonLeftStick  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonRightStick  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonLeftShoulder  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonRightShoulder  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonStart  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonSelect  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonLeftTrigger  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonRightTrigger  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonX  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonY  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonA  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonB  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonCross  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonSquare  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonTriangle  // const
  public    static  UnityEngine.InputSystem.LowLevel.GamepadButtonCircle  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand
TYPE:  sealed struct
TOKEN: 0x2000192
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  public            UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.Single                   lowFrequencyMotorSpeed  // 0x18
  public            System.Single                   highFrequencyMotorSpeed  // 0x1c
PROPERTIES:
  Type  get=0x04DAC5E0
  typeStatic  get=0x04DAC5E0
METHODS:
  RVA=0x04DAC5B0  token=0x6000FDA  UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand Create(System.Single lowFrequency, System.Single highFrequency)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.ICustomDeviceReset
TYPE:  interface
TOKEN: 0x2000193
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000FDB  System.Void Reset()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IEventMerger
TYPE:  interface
TOKEN: 0x2000194
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000FDC  System.Boolean MergeForward(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IEventPreProcessor
TYPE:  interface
TOKEN: 0x2000195
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000FDD  System.Boolean PreProcessEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputUpdateCallbackReceiver
TYPE:  interface
TOKEN: 0x2000196
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000FDE  System.Void OnUpdate()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.ITextInputReceiver
TYPE:  interface
TOKEN: 0x2000197
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000FDF  System.Void OnTextInput(System.Char character)
  RVA=-1  // abstract  token=0x6000FE0  System.Void OnIMECompositionChanged(UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.JoystickState
TYPE:  sealed struct
TOKEN: 0x2000198
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            System.Int32                    buttons  // 0x10
  public            UnityEngine.Vector2             stick  // 0x14
PROPERTIES:
  kFormat  get=0x04DACA70
  format  get=0x04DACA70
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.KeyboardState
TYPE:  sealed struct
TOKEN: 0x200019A
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private   static  System.Int32                    kSizeInBits  // const
  private   static  System.Int32                    kSizeInBytes  // const
  public            UnityEngine.InputSystem.LowLevel.KeyboardState.<keys>e__FixedBufferkeys  // 0x10
PROPERTIES:
  Format  get=0x04DACA80
  format  get=0x04DACA80
METHODS:
  RVA=0x0B2356D4  token=0x6000FE4  System.Void .ctor(UnityEngine.InputSystem.Key[] pressedKeys)
  RVA=0x0B2356E4  token=0x6000FE5  System.Void .ctor(System.Boolean IMESelected, UnityEngine.InputSystem.Key[] pressedKeys)
  RVA=0x05396D64  token=0x6000FE6  System.Void Set(UnityEngine.InputSystem.Key key, System.Boolean state)
  RVA=0x030D1A60  token=0x6000FE7  System.Boolean Get(UnityEngine.InputSystem.Key key)
  RVA=0x0B2356B0  token=0x6000FE8  System.Void Press(UnityEngine.InputSystem.Key key)
  RVA=0x0B2356C8  token=0x6000FE9  System.Void Release(UnityEngine.InputSystem.Key key)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.MouseState
TYPE:  sealed struct
TOKEN: 0x200019C
SIZE:  0x30
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector2             position  // 0x10
  public            UnityEngine.Vector2             delta  // 0x18
  public            UnityEngine.Vector2             scroll  // 0x20
  public            System.UInt16                   buttons  // 0x28
  public            System.UInt16                   displayIndex  // 0x2a
  public            System.UInt16                   clickCount  // 0x2c
PROPERTIES:
  Format  get=0x04DACAA0
  format  get=0x04DACAA0
METHODS:
  RVA=0x0B2357B8  token=0x6000FEC  UnityEngine.InputSystem.LowLevel.MouseState WithButton(UnityEngine.InputSystem.LowLevel.MouseButton button, System.Boolean state)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.MouseButton
TYPE:  sealed struct
TOKEN: 0x200019D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.MouseButtonLeft  // const
  public    static  UnityEngine.InputSystem.LowLevel.MouseButtonRight  // const
  public    static  UnityEngine.InputSystem.LowLevel.MouseButtonMiddle  // const
  public    static  UnityEngine.InputSystem.LowLevel.MouseButtonForward  // const
  public    static  UnityEngine.InputSystem.LowLevel.MouseButtonBack  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.PenState
TYPE:  sealed struct
TOKEN: 0x200019E
SIZE:  0x34
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector2             position  // 0x10
  public            UnityEngine.Vector2             delta  // 0x18
  public            UnityEngine.Vector2             tilt  // 0x20
  public            System.Single                   pressure  // 0x28
  public            System.Single                   twist  // 0x2c
  public            System.UInt16                   buttons  // 0x30
  private           System.UInt16                   displayIndex  // 0x32
PROPERTIES:
  Format  get=0x04DACAB0
  format  get=0x04DACAB0
METHODS:
  RVA=0x0B2358F8  token=0x6000FEF  UnityEngine.InputSystem.LowLevel.PenState WithButton(UnityEngine.InputSystem.PenButton button, System.Boolean state)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.PointerState
TYPE:  sealed struct
TOKEN: 0x200019F
SIZE:  0x34
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private           System.UInt32                   pointerId  // 0x10
  public            UnityEngine.Vector2             position  // 0x14
  public            UnityEngine.Vector2             delta  // 0x1c
  public            System.Single                   pressure  // 0x24
  public            UnityEngine.Vector2             radius  // 0x28
  public            System.UInt16                   buttons  // 0x30
  public            System.UInt16                   displayIndex  // 0x32
PROPERTIES:
  kFormat  get=0x04DACAC0
  format  get=0x04DACAC0
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.AccelerometerState
TYPE:  sealed struct
TOKEN: 0x20001A0
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector3             acceleration  // 0x10
PROPERTIES:
  kFormat  get=0x04DAC4A0
  format  get=0x04DAC4A0
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.GyroscopeState
TYPE:  sealed struct
TOKEN: 0x20001A1
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector3             angularVelocity  // 0x10
PROPERTIES:
  kFormat  get=0x04DAC910
  format  get=0x04DAC910
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.GravityState
TYPE:  sealed struct
TOKEN: 0x20001A2
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector3             gravity  // 0x10
PROPERTIES:
  kFormat  get=0x04DAC900
  format  get=0x04DAC900
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.AttitudeState
TYPE:  sealed struct
TOKEN: 0x20001A3
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Quaternion          attitude  // 0x10
PROPERTIES:
  kFormat  get=0x04DAC510
  format  get=0x04DAC510
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.LinearAccelerationState
TYPE:  sealed struct
TOKEN: 0x20001A4
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            UnityEngine.Vector3             acceleration  // 0x10
PROPERTIES:
  kFormat  get=0x04DACA90
  format  get=0x04DACA90
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.TouchFlags
TYPE:  sealed struct
TOKEN: 0x20001A5
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsIndirectTouch  // const
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsPrimaryTouch  // const
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsTapPress  // const
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsTapRelease  // const
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsOrphanedPrimaryTouch  // const
  public    static  UnityEngine.InputSystem.LowLevel.TouchFlagsBeganInSameFrame  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.TouchState
TYPE:  sealed struct
TOKEN: 0x20001A6
SIZE:  0x48
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private   static  System.Int32                    kSizeInBytes  // const
  public            System.Int32                    touchId  // 0x10
  public            UnityEngine.Vector2             position  // 0x14
  public            UnityEngine.Vector2             delta  // 0x1c
  public            System.Single                   pressure  // 0x24
  public            UnityEngine.Vector2             radius  // 0x28
  public            System.Byte                     phaseId  // 0x30
  public            System.Byte                     tapCount  // 0x31
  public            System.Byte                     displayIndex  // 0x32
  public            System.Byte                     flags  // 0x33
  private           System.UInt32                   updateStepCount  // 0x34
  public            System.Double                   startTime  // 0x38
  public            UnityEngine.Vector2             startPosition  // 0x40
PROPERTIES:
  Format  get=0x04DACDF0
  phase  get=0x04D866B0  set=0x04D866D0
  isNoneEndedOrCanceled  get=0x0B237C54
  isInProgress  get=0x0B237C34
  isPrimaryTouch  get=0x0B237C74  set=0x0B237CF8
  isOrphanedPrimaryTouch  get=0x0B237C6C  set=0x0B237CD4
  isIndirectTouch  get=0x0B237C4C  set=0x0B237CB0
  isTap  get=0x0B237C7C  set=0x0B237D1C
  isTapPress  get=0x0B237C7C  set=0x0B237D1C
  isTapRelease  get=0x0B237C84  set=0x0B237D40
  beganInSameFrame  get=0x0B237C2C  set=0x0B237C8C
  format  get=0x04DACDF0
METHODS:
  RVA=0x0B2379EC  token=0x6001011  System.String ToString()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.TouchscreenState
TYPE:  sealed struct
TOKEN: 0x20001A7
SIZE:  0x278
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public    static  System.Int32                    MaxTouches  // const
  public            UnityEngine.InputSystem.LowLevel.TouchscreenState.<primaryTouchData>e__FixedBufferprimaryTouchData  // 0x10
  private   static  System.Int32                    kTouchDataOffset  // const
  public            UnityEngine.InputSystem.LowLevel.TouchscreenState.<touchData>e__FixedBuffertouchData  // 0x48
PROPERTIES:
  Format  get=0x04DACE30
  primaryTouch  get=0x0464E630
  touches  get=0x04DACE40
  format  get=0x04DACE30
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.ActionEvent
TYPE:  sealed struct
TOKEN: 0x20001AA
SIZE:  0x38
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  private           System.UInt16                   m_ControlIndex  // 0x24
  private           System.UInt16                   m_BindingIndex  // 0x26
  private           System.UInt16                   m_InteractionIndex  // 0x28
  private           System.Byte                     m_StateIndex  // 0x2a
  private           System.Byte                     m_Phase  // 0x2b
  private           System.Double                   m_StartTime  // 0x2c
  public            UnityEngine.InputSystem.LowLevel.ActionEvent.<m_ValueData>e__FixedBufferm_ValueData  // 0x34
PROPERTIES:
  Type  get=0x04DAC4C0
  startTime  get=0x04DAC4D0  set=0x04DAC500
  phase  get=0x04D895D0  set=0x04D89600
  valueData  get=0x04DAC4E0
  valueSizeInBytes  get=0x04DAC4F0
  stateIndex  get=0x015AF940  set=0x0B22AAE0
  controlIndex  get=0x04D96D00  set=0x0B22A9F8
  bindingIndex  get=0x04D993B0  set=0x0B22A98C
  interactionIndex  get=0x0B22A974  set=0x0B22AA64
  typeStatic  get=0x04DAC4C0
METHODS:
  RVA=0x0464E630  token=0x6001025  UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
  RVA=0x04DAC4B0  token=0x6001027  System.Int32 GetEventSizeWithValueSize(System.Int32 valueSizeInBytes)
  RVA=0x0B22A838  token=0x6001028  UnityEngine.InputSystem.LowLevel.ActionEvent* From(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DeltaStateEvent
TYPE:  sealed struct
TOKEN: 0x20001AC
SIZE:  0x2D
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  public            UnityEngine.InputSystem.Utilities.FourCCstateFormat  // 0x24
  public            System.UInt32                   stateOffset  // 0x28
  private           UnityEngine.InputSystem.LowLevel.DeltaStateEvent.<stateData>e__FixedBufferstateData  // 0x2c
PROPERTIES:
  deltaStateSizeInBytes  get=0x04DAC520
  deltaState  get=0x04DAC530
  typeStatic  get=0x04DAC540
METHODS:
  RVA=0x0464E630  token=0x600102C  UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
  RVA=0x0B22AE10  token=0x600102D  UnityEngine.InputSystem.LowLevel.DeltaStateEvent* From(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=0x0464E630  token=0x600102E  UnityEngine.InputSystem.LowLevel.DeltaStateEvent* FromUnchecked(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=0x0B22AB4C  token=0x600102F  Unity.Collections.NativeArray<System.Byte> From(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr& eventPtr, Unity.Collections.Allocator allocator)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent
TYPE:  sealed struct
TOKEN: 0x20001AE
SIZE:  0x24
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
PROPERTIES:
  typeStatic  get=0x04DAC550
METHODS:
  RVA=0x0464E630  token=0x6001031  UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
  RVA=0x0B22AF4C  token=0x6001032  UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent Create(System.Int32 deviceId, System.Double time)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DeviceRemoveEvent
TYPE:  sealed struct
TOKEN: 0x20001AF
SIZE:  0x24
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
PROPERTIES:
  typeStatic  get=0x04DAC560
METHODS:
  RVA=0x0464E630  token=0x6001034  UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
  RVA=0x0B22AFA4  token=0x6001035  UnityEngine.InputSystem.LowLevel.DeviceRemoveEvent Create(System.Int32 deviceId, System.Double time)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.DeviceResetEvent
TYPE:  sealed struct
TOKEN: 0x20001B0
SIZE:  0x24
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  public            System.Boolean                  hardReset  // 0x18
PROPERTIES:
  typeStatic  get=0x04DAC570
METHODS:
  RVA=0x0B22AFFC  token=0x6001037  UnityEngine.InputSystem.LowLevel.DeviceResetEvent Create(System.Int32 deviceId, System.Boolean hardReset, System.Double time)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
TYPE:  interface
TOKEN: 0x20001B1
FIELDS:
PROPERTIES:
  typeStatic  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IMECompositionEvent
TYPE:  sealed struct
TOKEN: 0x20001B2
SIZE:  0xA8
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  private   static  System.Int32                    kIMECharBufferSize  // const
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  public            UnityEngine.InputSystem.LowLevel.IMECompositionStringcompositionString  // 0x24
PROPERTIES:
  typeStatic  get=0x04DAC9D0
METHODS:
  RVA=0x0B231A70  token=0x600103A  UnityEngine.InputSystem.LowLevel.IMECompositionEvent Create(System.Int32 deviceId, System.String compositionString, System.Double time)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IMECompositionString
TYPE:  sealed struct
TOKEN: 0x20001B3
SIZE:  0x94
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Int32                    size  // 0x10
  private           UnityEngine.InputSystem.LowLevel.IMECompositionString.<buffer>e__FixedBufferbuffer  // 0x14
PROPERTIES:
  Count  get=0x02B77620
  Item  get=0x0B231D30
METHODS:
  RVA=0x0B231CC4  token=0x600103D  System.Void .ctor(System.String characters)
  RVA=0x0B231CA0  token=0x600103E  System.String ToString()
  RVA=0x0B231B70  token=0x600103F  System.Collections.Generic.IEnumerator<System.Char> GetEnumerator()
  RVA=0x0B231C98  token=0x6001040  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEvent
TYPE:  sealed struct
TOKEN: 0x20001B6
SIZE:  0x24
FIELDS:
  private   static  System.UInt32                   kHandledMask  // const
  private   static  System.UInt32                   kIdMask  // const
  private   static  System.Int32                    kBaseEventSize  // const
  public    static  System.Int32                    InvalidEventId  // const
  private   static  System.Int32                    kAlignment  // const
  private           UnityEngineInternal.Input.NativeInputEventm_Event  // 0x10
PROPERTIES:
  type  get=0x02B77620  set=0x04D86140
  sizeInBytes  get=0x04D87600  set=0x0381FC50
  eventId  get=0x04DACA50  set=0x04DACA60
  deviceId  get=0x04D875F0  set=0x04DA8A20
  time  get=0x0B23566C  set=0x03820740
  internalTime  get=0x04D9C270  set=0x04D9E9A0
  handled  get=0x0425D410  set=0x0420E3A0
METHODS:
  RVA=0x04033220  token=0x6001053  System.Void .ctor(UnityEngine.InputSystem.Utilities.FourCC type, System.Int32 sizeInBytes, System.Int32 deviceId, System.Double time)
  RVA=0x0B2354A8  token=0x6001056  System.String ToString()
  RVA=0x030CD860  token=0x6001057  UnityEngine.InputSystem.LowLevel.InputEvent* GetNextInMemory(UnityEngine.InputSystem.LowLevel.InputEvent* currentPtr)
  RVA=0x0B2353A8  token=0x6001058  UnityEngine.InputSystem.LowLevel.InputEvent* GetNextInMemoryChecked(UnityEngine.InputSystem.LowLevel.InputEvent* currentPtr, UnityEngine.InputSystem.LowLevel.InputEventBuffer& buffer)
  RVA=0x0B23536C  token=0x6001059  System.Boolean Equals(UnityEngine.InputSystem.LowLevel.InputEvent* first, UnityEngine.InputSystem.LowLevel.InputEvent* second)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEventBuffer
TYPE:  sealed struct
TOKEN: 0x20001B7
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IDisposable System.ICloneable
FIELDS:
  public    static  System.Int64                    BufferSizeUnknown  // const
  private           Unity.Collections.NativeArray<System.Byte>m_Buffer  // 0x10
  private           System.Int64                    m_SizeInBytes  // 0x20
  private           System.Int32                    m_EventCount  // 0x28
  private           System.Boolean                  m_WeOwnTheBuffer  // 0x2c
PROPERTIES:
  eventCount  get=0x011EF5B0
  sizeInBytes  get=0x02B2ECC0
  capacityInBytes  get=0x04D57490
  data  get=0x02B77630
  bufferPtr  get=0x0B232480
METHODS:
  RVA=0x030CEA20  token=0x600105F  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, System.Int32 eventCount, System.Int32 sizeInBytes, System.Int32 capacityInBytes)
  RVA=0x0B232348  token=0x6001060  System.Void .ctor(Unity.Collections.NativeArray<System.Byte> buffer, System.Int32 eventCount, System.Int32 sizeInBytes, System.Boolean transferNativeArrayOwnership)
  RVA=0x0B232088  token=0x6001061  System.Void AppendEvent(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, System.Int32 capacityIncrementInBytes, Unity.Collections.Allocator allocator)
  RVA=0x0B231E20  token=0x6001062  UnityEngine.InputSystem.LowLevel.InputEvent* AllocateEvent(System.Int32 sizeInBytes, System.Int32 capacityIncrementInBytes, Unity.Collections.Allocator allocator)
  RVA=0x0B2321E0  token=0x6001063  System.Boolean Contains(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x030A1D60  token=0x6001064  System.Void Reset()
  RVA=0x030CE4A0  token=0x6001065  System.Void AdvanceToNextEvent(UnityEngine.InputSystem.LowLevel.InputEvent*& currentReadPos, UnityEngine.InputSystem.LowLevel.InputEvent*& currentWritePos, System.Int32& numEventsRetainedInBuffer, System.Int32& numRemainingEvents, System.Boolean leaveEventInBuffer)
  RVA=0x0B232260  token=0x6001066  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.LowLevel.InputEventPtr> GetEnumerator()
  RVA=0x0B2322E0  token=0x6001067  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B232210  token=0x6001068  System.Void Dispose()
  RVA=0x0B23211C  token=0x6001069  UnityEngine.InputSystem.LowLevel.InputEventBuffer Clone()
  RVA=0x0B2322E8  token=0x600106A  System.Object System.ICloneable.Clone()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEventListener
TYPE:  sealed struct
TOKEN: 0x20001B9
SIZE:  0x11
IMPLEMENTS: System.IObservable`1
FIELDS:
  private   static  UnityEngine.InputSystem.LowLevel.InputEventListener.ObserverStates_ObserverState  // static @ 0x0
METHODS:
  RVA=0x0B23262C  token=0x6001071  UnityEngine.InputSystem.LowLevel.InputEventListener op_Addition(UnityEngine.InputSystem.LowLevel.InputEventListener _, System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice> callback)
  RVA=0x0B232798  token=0x6001072  UnityEngine.InputSystem.LowLevel.InputEventListener op_Subtraction(UnityEngine.InputSystem.LowLevel.InputEventListener _, System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice> callback)
  RVA=0x0B2324B0  token=0x6001073  System.IDisposable Subscribe(System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEventPtr
TYPE:  sealed struct
TOKEN: 0x20001BC
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly UnityEngine.InputSystem.LowLevel.InputEvent*m_EventPtr  // 0x10
PROPERTIES:
  valid  get=0x030CFC40
  handled  get=0x0B232A0C  set=0x0B232CA8
  id  get=0x0B232A2C  set=0x0B232D18
  type  get=0x030CF8C0
  sizeInBytes  get=0x03FB4800
  deviceId  get=0x0B2329F8  set=0x0B232C38
  time  get=0x0B232C18  set=0x0B232E00
  internalTime  get=0x03D44B10  set=0x0B232D90
  data  get=0x02B76770
  stateFormat  get=0x0400B620
  stateSizeInBytes  get=0x0B232B10
  stateOffset  get=0x0B232A40
METHODS:
  RVA=0x04D88B60  token=0x6001078  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=-1  // generic def  token=0x600108A  System.Boolean IsA()
  RVA=0x0B232980  token=0x600108B  UnityEngine.InputSystem.LowLevel.InputEventPtr Next()
  RVA=0x0B2329A0  token=0x600108C  System.String ToString()
  RVA=0x02B76770  token=0x600108D  UnityEngine.InputSystem.LowLevel.InputEvent* ToPointer()
  RVA=0x0B232968  token=0x600108E  System.Boolean Equals(UnityEngine.InputSystem.LowLevel.InputEventPtr other)
  RVA=0x0B232904  token=0x600108F  System.Boolean Equals(System.Object obj)
  RVA=0x02B77620  token=0x6001090  System.Int32 GetHashCode()
  RVA=0x02FB1840  token=0x6001091  System.Boolean op_Equality(UnityEngine.InputSystem.LowLevel.InputEventPtr left, UnityEngine.InputSystem.LowLevel.InputEventPtr right)
  RVA=0x02F019D0  token=0x6001092  System.Boolean op_Inequality(UnityEngine.InputSystem.LowLevel.InputEventPtr left, UnityEngine.InputSystem.LowLevel.InputEventPtr right)
  RVA=0x0464E630  token=0x6001093  UnityEngine.InputSystem.LowLevel.InputEventPtr op_Implicit(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x0464E630  token=0x6001094  UnityEngine.InputSystem.LowLevel.InputEventPtr From(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x0464E630  token=0x6001095  UnityEngine.InputSystem.LowLevel.InputEvent* op_Implicit(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0464E630  token=0x6001096  UnityEngine.InputSystem.LowLevel.InputEvent* FromInputEventPtr(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEventStream
TYPE:  sealed struct
TOKEN: 0x20001BD
SIZE:  0x88
FIELDS:
  private           UnityEngine.InputSystem.LowLevel.InputEventBufferm_NativeBuffer  // 0x10
  private           UnityEngine.InputSystem.LowLevel.InputEvent*m_CurrentNativeEventReadPtr  // 0x30
  private           UnityEngine.InputSystem.LowLevel.InputEvent*m_CurrentNativeEventWritePtr  // 0x38
  private           System.Int32                    m_RemainingNativeEventCount  // 0x40
  private   readonly System.Int32                    m_MaxAppendedEvents  // 0x44
  private           UnityEngine.InputSystem.LowLevel.InputEventBufferm_AppendBuffer  // 0x48
  private           UnityEngine.InputSystem.LowLevel.InputEvent*m_CurrentAppendEventReadPtr  // 0x68
  private           UnityEngine.InputSystem.LowLevel.InputEvent*m_CurrentAppendEventWritePtr  // 0x70
  private           System.Int32                    m_RemainingAppendEventCount  // 0x78
  private           System.Int32                    m_NumEventsRetainedInBuffer  // 0x7c
  private           System.Boolean                  m_IsOpen  // 0x80
PROPERTIES:
  isOpen  get=0x04D86A50
  remainingEventCount  get=0x04DACA20
  numEventsRetainedInBuffer  get=0x04D86B50
  currentEventPtr  get=0x030CE580
  numBytesRetainedInBuffer  get=0x0B23301C
METHODS:
  RVA=0x03F51800  token=0x600109C  System.Void .ctor(UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer, System.Int32 maxAppendedEvents)
  RVA=0x030A1610  token=0x600109D  System.Void Close(UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer)
  RVA=0x0B232E70  token=0x600109E  System.Void CleanUpAfterException()
  RVA=0x0B232EC4  token=0x600109F  System.Void Write(UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
  RVA=0x030CE510  token=0x60010A0  UnityEngine.InputSystem.LowLevel.InputEvent* Advance(System.Boolean leaveEventInBuffer)
  RVA=0x030CD8E0  token=0x60010A1  UnityEngine.InputSystem.LowLevel.InputEvent* Peek()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputEventTrace
TYPE:  sealed class
TOKEN: 0x20001BE
SIZE:  0xC8
IMPLEMENTS: System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   static  System.Int32                    kDefaultBufferSize  // const
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputEvenTraceMarker  // static @ 0x0
  private           System.Int32                    m_ChangeCounter  // 0x10
  private           System.Boolean                  m_Enabled  // 0x14
  private           System.Func<UnityEngine.InputSystem.LowLevel.InputEventPtr,UnityEngine.InputSystem.InputDevice,System.Boolean>m_OnFilterEvent  // 0x18
  private           System.Int32                    m_DeviceId  // 0x20
  private           UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<UnityEngine.InputSystem.LowLevel.InputEventPtr>>m_EventListeners  // 0x28
  private           System.Int64                    m_EventBufferSize  // 0x78
  private           System.Int64                    m_MaxEventBufferSize  // 0x80
  private           System.Int64                    m_GrowIncrementSize  // 0x88
  private           System.Int64                    m_EventCount  // 0x90
  private           System.Int64                    m_EventSizeInBytes  // 0x98
  private           System.UInt64                   m_EventBufferStorage  // 0xa0
  private           System.UInt64                   m_EventBufferHeadStorage  // 0xa8
  private           System.UInt64                   m_EventBufferTailStorage  // 0xb0
  private           System.Boolean                  m_HasWrapped  // 0xb8
  private           System.Boolean                  m_RecordFrameMarkers  // 0xb9
  private           UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo[]m_DeviceInfos  // 0xc0
  private   static  System.Int32                    kFileVersion  // static @ 0x8
PROPERTIES:
  FrameMarkerEvent  get=0x04DACA30
  deviceId  get=0x02B70F90  set=0x04D86330
  enabled  get=0x04D8EE50
  recordFrameMarkers  get=0x04D89310  set=0x0B235284
  eventCount  get=0x04D86220
  totalEventSizeInBytes  get=0x04D85EE0
  allocatedSizeInBytes  get=0x0B2351C8
  maxSizeInBytes  get=0x04D86260
  deviceInfos  get=0x0B2351DC
  onFilterEvent  get=0x0385B100  set=0x05392C40
  m_EventBuffer  get=0x04D86210  set=0x04D927B0
  m_EventBufferHead  get=0x04D86290  set=0x04D862F0
  m_EventBufferTail  get=0x04D86250  set=0x04D92630
  kFileFormat  get=0x04DACA40
EVENTS:
  onEvent  add=add_onEvent  remove=remove_onEvent
METHODS:
  RVA=0x0B2350D8  token=0x60010B1  System.Void .ctor(UnityEngine.InputSystem.InputDevice device, System.Int64 bufferSizeInBytes, System.Boolean growBuffer, System.Int64 maxBufferSizeInBytes, System.Int64 growIncrementSizeInBytes)
  RVA=0x0B235094  token=0x60010B2  System.Void .ctor(System.Int64 bufferSizeInBytes, System.Boolean growBuffer, System.Int64 maxBufferSizeInBytes, System.Int64 growIncrementSizeInBytes)
  RVA=0x0B234F0C  token=0x60010B3  System.Void WriteTo(System.String filePath)
  RVA=0x0B234940  token=0x60010B4  System.Void WriteTo(System.IO.Stream stream)
  RVA=0x0B233C10  token=0x60010B5  System.Void ReadFrom(System.String filePath)
  RVA=0x0B233D24  token=0x60010B6  System.Void ReadFrom(System.IO.Stream stream)
  RVA=0x0B23350C  token=0x60010B7  UnityEngine.InputSystem.LowLevel.InputEventTrace LoadFrom(System.String filePath)
  RVA=0x0B2333C0  token=0x60010B8  UnityEngine.InputSystem.LowLevel.InputEventTrace LoadFrom(System.IO.Stream stream)
  RVA=0x0B234668  token=0x60010B9  UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController Replay()
  RVA=0x0B2346D4  token=0x60010BA  System.Boolean Resize(System.Int64 newBufferSize, System.Int64 newMaxBufferSize)
  RVA=0x0B23307C  token=0x60010BB  System.Void Clear()
  RVA=0x0B2331BC  token=0x60010BC  System.Void Enable()
  RVA=0x0B2330B0  token=0x60010BD  System.Void Disable()
  RVA=0x0B233334  token=0x60010BE  System.Boolean GetNextEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr& current)
  RVA=0x0B2332D4  token=0x60010BF  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.LowLevel.InputEventPtr> GetEnumerator()
  RVA=0x0B234938  token=0x60010C0  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B23319C  token=0x60010C1  System.Void Dispose()
  RVA=0x0B233050  token=0x60010C8  System.Void Allocate()
  RVA=0x0B234628  token=0x60010C9  System.Void Release()
  RVA=0x0B233640  token=0x60010CA  System.Void OnBeforeUpdate()
  RVA=0x0B233718  token=0x60010CB  System.Void OnInputEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B235020  token=0x60010CD  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.StateEvent
TYPE:  sealed struct
TOKEN: 0x20001C5
SIZE:  0x29
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  private   static  System.Int32                    kStateDataSizeToSubtract  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  public            UnityEngine.InputSystem.Utilities.FourCCstateFormat  // 0x24
  private           UnityEngine.InputSystem.LowLevel.StateEvent.<stateData>e__FixedBufferstateData  // 0x28
PROPERTIES:
  stateSizeInBytes  get=0x04DACFA0
  state  get=0x04DACFB0
  typeStatic  get=0x04DACFC0
METHODS:
  RVA=0x0464E630  token=0x60010FC  UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
  RVA=-1  // generic def  token=0x60010FE  TState GetState()
  RVA=-1  // generic def  token=0x60010FF  TState GetState(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=-1  // generic def  token=0x6001100  System.Int32 GetEventSizeWithPayload()
  RVA=0x0B2497B8  token=0x6001101  UnityEngine.InputSystem.LowLevel.StateEvent* From(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=0x0464E630  token=0x6001102  UnityEngine.InputSystem.LowLevel.StateEvent* FromUnchecked(UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
  RVA=0x0B249788  token=0x6001103  Unity.Collections.NativeArray<System.Byte> From(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEventPtr& eventPtr, Unity.Collections.Allocator allocator)
  RVA=0x0B2494DC  token=0x6001104  Unity.Collections.NativeArray<System.Byte> FromDefaultStateFor(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEventPtr& eventPtr, Unity.Collections.Allocator allocator)
  RVA=0x0B24950C  token=0x6001105  Unity.Collections.NativeArray<System.Byte> From(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEventPtr& eventPtr, Unity.Collections.Allocator allocator, System.Boolean useDefaultState)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.TextEvent
TYPE:  sealed struct
TOKEN: 0x20001C7
SIZE:  0x28
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
FIELDS:
  public    static  System.Int32                    Type  // const
  public            UnityEngine.InputSystem.LowLevel.InputEventbaseEvent  // 0x10
  public            System.Int32                    character  // 0x24
PROPERTIES:
  typeStatic  get=0x04DACFD0
METHODS:
  RVA=0x0B249C30  token=0x6001107  UnityEngine.InputSystem.LowLevel.TextEvent* From(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B249B60  token=0x6001108  UnityEngine.InputSystem.LowLevel.TextEvent Create(System.Int32 deviceId, System.Char character, System.Double time)
  RVA=0x0B249BC8  token=0x6001109  UnityEngine.InputSystem.LowLevel.TextEvent Create(System.Int32 deviceId, System.Int32 character, System.Double time)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputUpdateDelegate
TYPE:  sealed class
TOKEN: 0x20001C8
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03EB3930  token=0x600110A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600110B  System.Void Invoke(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer)
  RVA=0x0B245F60  token=0x600110C  System.IAsyncResult BeginInvoke(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD0344  token=0x600110D  System.Void EndInvoke(UnityEngine.InputSystem.LowLevel.InputEventBuffer& eventBuffer, System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputRuntime
TYPE:  interface
TOKEN: 0x20001C9
FIELDS:
PROPERTIES:
  onUpdate  get=-1  // abstract  set=-1  // abstract
  onBeforeUpdate  get=-1  // abstract  set=-1  // abstract
  onShouldRunUpdate  get=-1  // abstract  set=-1  // abstract
  onDeviceDiscovered  get=-1  // abstract  set=-1  // abstract
  onPlayerFocusChanged  get=-1  // abstract  set=-1  // abstract
  isPlayerFocused  get=-1  // abstract
  onShutdown  get=-1  // abstract  set=-1  // abstract
  pollingFrequency  get=-1  // abstract  set=-1  // abstract
  currentTime  get=-1  // abstract
  currentTimeForFixedUpdate  get=-1  // abstract
  unscaledGameTime  get=-1  // abstract
  currentTimeOffsetToRealtimeSinceStartup  get=-1  // abstract
  runInBackground  get=-1  // abstract  set=-1  // abstract
  screenSize  get=-1  // abstract
  screenOrientation  get=-1  // abstract
  isInBatchMode  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600110E  System.Int32 AllocateDeviceId()
  RVA=-1  // abstract  token=0x600110F  System.Void Update(UnityEngine.InputSystem.LowLevel.InputUpdateType type)
  RVA=-1  // abstract  token=0x6001110  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEvent* ptr)
  RVA=-1  // abstract  token=0x6001111  System.Int64 DeviceCommand(System.Int32 deviceId, UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputRuntime
TYPE:  static class
TOKEN: 0x20001CA
SIZE:  0x10
FIELDS:
  public    static  UnityEngine.InputSystem.LowLevel.IInputRuntimes_Instance  // static @ 0x0
  public    static  System.Double                   s_CurrentTimeOffsetToRealtimeSinceStartup  // static @ 0x8
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputRuntimeExtensions
TYPE:  static class
TOKEN: 0x20001CB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600112A  System.Int64 DeviceCommand(UnityEngine.InputSystem.LowLevel.IInputRuntime runtime, System.Int32 deviceId, TCommand& command)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputMetrics
TYPE:  sealed struct
TOKEN: 0x20001CC
SIZE:  0x48
FIELDS:
  private           System.Int32                    <maxNumDevices>k__BackingField  // 0x10
  private           System.Int32                    <currentNumDevices>k__BackingField  // 0x14
  private           System.Int32                    <maxStateSizeInBytes>k__BackingField  // 0x18
  private           System.Int32                    <currentStateSizeInBytes>k__BackingField  // 0x1c
  private           System.Int32                    <currentControlCount>k__BackingField  // 0x20
  private           System.Int32                    <currentLayoutCount>k__BackingField  // 0x24
  private           System.Int32                    <totalEventBytes>k__BackingField  // 0x28
  private           System.Int32                    <totalEventCount>k__BackingField  // 0x2c
  private           System.Int32                    <totalUpdateCount>k__BackingField  // 0x30
  private           System.Double                   <totalEventProcessingTime>k__BackingField  // 0x38
  private           System.Double                   <totalEventLagTime>k__BackingField  // 0x40
PROPERTIES:
  maxNumDevices  get=0x02B77620  set=0x04D86140
  currentNumDevices  get=0x04D88B20  set=0x04D88B50
  maxStateSizeInBytes  get=0x04D88B10  set=0x04D88B40
  currentStateSizeInBytes  get=0x04D8CAD0  set=0x04D8CAE0
  currentControlCount  get=0x02B2E2D0  set=0x014F51F0
  currentLayoutCount  get=0x04D882A0  set=0x04D8F540
  totalEventBytes  get=0x011EF5B0  set=0x011EF9B0
  totalEventCount  get=0x04D86310  set=0x04D86320
  totalUpdateCount  get=0x02B70F90  set=0x04D86330
  totalEventProcessingTime  get=0x04D86D60  set=0x04D86D70
  totalEventLagTime  get=0x04D86A10  set=0x04D86A20
  averageEventBytesPerFrame  get=0x0B242894
  averageProcessingTimePerEvent  get=0x0B2428C0
  averageLagTimePerEvent  get=0x0B2428AC
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputUpdateType
TYPE:  sealed struct
TOKEN: 0x20001CD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeNone  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeDynamic  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeFixed  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeBeforeRender  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeEditor  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeManual  // const
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypeDefault  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputUpdate
TYPE:  static class
TOKEN: 0x20001CE
SIZE:  0x10
FIELDS:
  public    static  System.UInt32                   s_UpdateStepCount  // static @ 0x0
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdateTypes_LatestUpdateType  // static @ 0x4
  public    static  UnityEngine.InputSystem.LowLevel.InputUpdate.UpdateStepCounts_PlayerUpdateStepCount  // static @ 0x8
METHODS:
  RVA=0x030CE780  token=0x6001144  System.Void OnBeforeUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType type)
  RVA=0x030CE710  token=0x6001145  System.Void OnUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType type)
  RVA=0x0B245FFC  token=0x6001146  UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState Save()
  RVA=0x03DF7BB0  token=0x6001147  System.Void Restore(UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState state)
  RVA=0x0381FC20  token=0x6001148  UnityEngine.InputSystem.LowLevel.InputUpdateType GetUpdateTypeForPlayer(UnityEngine.InputSystem.LowLevel.InputUpdateType mask)
  RVA=0x030CE8B0  token=0x6001149  System.Boolean IsPlayerUpdate(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.NativeInputRuntime
TYPE:  class
TOKEN: 0x20001D1
SIZE:  0x48
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputRuntime
FIELDS:
  public    static readonly UnityEngine.InputSystem.LowLevel.NativeInputRuntimeinstance  // static @ 0x0
  private           System.Boolean                  m_RunInBackground  // 0x10
  private           System.Action                   m_ShutdownMethod  // 0x18
  private           UnityEngine.InputSystem.LowLevel.InputUpdateDelegatem_OnUpdate  // 0x20
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputUpdateType>m_OnBeforeUpdate  // 0x28
  private           System.Func<UnityEngine.InputSystem.LowLevel.InputUpdateType,System.Boolean>m_OnShouldRunUpdate  // 0x30
  private           System.Single                   m_PollingFrequency  // 0x38
  private           System.Boolean                  m_DidCallOnShutdown  // 0x3c
  private           System.Action<System.Boolean>   m_FocusChangedMethod  // 0x40
PROPERTIES:
  onUpdate  get=0x04D862C0  set=0x04A37690
  onBeforeUpdate  get=0x04D86240  set=0x03DFA4B0
  onShouldRunUpdate  get=0x011F36E0  set=0x04B49C00
  onDeviceDiscovered  get=0x0B247130  set=0x03EB3830
  onShutdown  get=0x0385B100  set=0x0B2471A8
  onPlayerFocusChanged  get=0x04D85A60  set=0x03EB42C0
  isPlayerFocused  get=0x05396550
  pollingFrequency  get=0x04D88360  set=0x03EB3880
  currentTime  get=0x030CFB80
  currentTimeForFixedUpdate  get=0x0B2470F0
  currentTimeOffsetToRealtimeSinceStartup  get=0x030CF780
  unscaledGameTime  get=0x05397920
  runInBackground  get=0x033F1FA0  set=0x04D86130
  screenSize  get=0x0B247170
  screenOrientation  get=0x05391B08
  isInBatchMode  get=0x0B247128
METHODS:
  RVA=0x0B247040  token=0x600114E  System.Int32 AllocateDeviceId()
  RVA=0x04B1ECE0  token=0x600114F  System.Void Update(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x0B2470A4  token=0x6001150  System.Void QueueEvent(UnityEngine.InputSystem.LowLevel.InputEvent* ptr)
  RVA=0x04320710  token=0x6001151  System.Int64 DeviceCommand(System.Int32 deviceId, UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr)
  RVA=0x0720F178  token=0x6001167  System.Void OnShutdown()
  RVA=0x0B247080  token=0x6001168  System.Boolean OnWantsToShutdown()
  RVA=0x048671F0  token=0x6001169  System.Void OnFocusChanged(System.Boolean focus)
  RVA=0x04DACEF0  token=0x600116D  System.Void .ctor()
  RVA=0x04D368B0  token=0x600116E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
TYPE:  interface
TOKEN: 0x20001D5
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6001175  System.Void OnNextUpdate()
  RVA=-1  // abstract  token=0x6001176  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // abstract  token=0x6001177  System.Boolean GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
TYPE:  interface
TOKEN: 0x20001D6
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6001178  System.Void NotifyControlStateChanged(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 monitorIndex)
  RVA=-1  // abstract  token=0x6001179  System.Void NotifyTimerExpired(UnityEngine.InputSystem.InputControl control, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
TYPE:  interface
TOKEN: 0x20001D7
FIELDS:
PROPERTIES:
  format  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputState
TYPE:  static class
TOKEN: 0x20001D8
SIZE:  0x10
FIELDS:
PROPERTIES:
  currentUpdateType  get=0x0B245E90
  updateCount  get=0x0B245EC8
  currentTime  get=0x04D50620
EVENTS:
  onChange  add=add_onChange  remove=remove_onChange
METHODS:
  RVA=0x0B24587C  token=0x6001180  System.Void Change(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=-1  // generic def  token=0x6001181  System.Void Change(UnityEngine.InputSystem.InputControl control, TState state, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=-1  // generic def  token=0x6001182  System.Void Change(UnityEngine.InputSystem.InputControl control, TState& state, UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B245B10  token=0x6001183  System.Boolean IsIntegerFormat(UnityEngine.InputSystem.Utilities.FourCC format)
  RVA=0x0B2455BC  token=0x6001184  System.Void AddChangeMonitor(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.UInt32 groupIndex)
  RVA=0x0B24577C  token=0x6001185  UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor AddChangeMonitor(UnityEngine.InputSystem.InputControl control, System.Action<UnityEngine.InputSystem.InputControl,System.Double,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Int64> valueChangeCallback, System.Int32 monitorIndex, System.Action<UnityEngine.InputSystem.InputControl,System.Double,System.Int64,System.Int32> timerExpiredCallback)
  RVA=0x0B245D00  token=0x6001186  System.Void RemoveChangeMonitor(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex)
  RVA=0x0B2454C8  token=0x6001187  System.Void AddChangeMonitorTimeout(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
  RVA=0x0B245C2C  token=0x6001188  System.Void RemoveChangeMonitorTimeout(UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, System.Int64 monitorIndex, System.Int32 timerIndex)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputStateBlock
TYPE:  sealed struct
TOKEN: 0x20001DA
SIZE:  0x20
FIELDS:
  public    static  System.UInt32                   InvalidOffset  // const
  public    static  System.UInt32                   AutomaticOffset  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatInvalid  // static @ 0x0
  private   static  System.Int32                    kFormatInvalid  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatBit  // static @ 0x4
  private   static  System.Int32                    kFormatBit  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatSBit  // static @ 0x8
  private   static  System.Int32                    kFormatSBit  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatInt  // static @ 0xc
  private   static  System.Int32                    kFormatInt  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatUInt  // static @ 0x10
  private   static  System.Int32                    kFormatUInt  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatShort  // static @ 0x14
  private   static  System.Int32                    kFormatShort  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatUShort  // static @ 0x18
  private   static  System.Int32                    kFormatUShort  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatByte  // static @ 0x1c
  private   static  System.Int32                    kFormatByte  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatSByte  // static @ 0x20
  private   static  System.Int32                    kFormatSByte  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatLong  // static @ 0x24
  private   static  System.Int32                    kFormatLong  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatULong  // static @ 0x28
  private   static  System.Int32                    kFormatULong  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatFloat  // static @ 0x2c
  private   static  System.Int32                    kFormatFloat  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatDouble  // static @ 0x30
  private   static  System.Int32                    kFormatDouble  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector2  // static @ 0x34
  private   static  System.Int32                    kFormatVector2  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector3  // static @ 0x38
  private   static  System.Int32                    kFormatVector3  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatQuaternion  // static @ 0x3c
  private   static  System.Int32                    kFormatQuaternion  // const
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector2Short  // static @ 0x40
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector3Short  // static @ 0x44
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector2Byte  // static @ 0x48
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatVector3Byte  // static @ 0x4c
  public    static readonly UnityEngine.InputSystem.Utilities.FourCCFormatPose  // static @ 0x50
  private   static  System.Int32                    kFormatPose  // const
  private           UnityEngine.InputSystem.Utilities.FourCC<format>k__BackingField  // 0x10
  private           System.UInt32                   m_ByteOffset  // 0x14
  private           System.UInt32                   <bitOffset>k__BackingField  // 0x18
  private           System.UInt32                   <sizeInBits>k__BackingField  // 0x1c
PROPERTIES:
  format  get=0x02B77620  set=0x04D86140
  byteOffset  get=0x04D88B20  set=0x04D88B50
  bitOffset  get=0x04D88B10  set=0x04D88B40
  sizeInBits  get=0x04D8CAD0  set=0x04D8CAE0
  alignedSizeInBytes  get=0x030CFC10
  effectiveByteOffset  get=0x0B243DFC
  effectiveBitOffset  get=0x0B243DB0
METHODS:
  RVA=0x0B2430B0  token=0x600118C  System.Int32 GetSizeOfPrimitiveFormatInBits(UnityEngine.InputSystem.Utilities.FourCC type)
  RVA=0x0B242C38  token=0x600118D  UnityEngine.InputSystem.Utilities.FourCC GetPrimitiveFormatFromType(System.Type type)
  RVA=0x03F51080  token=0x6001199  System.Int32 ReadInt(System.Void* statePtr)
  RVA=0x0B243BAC  token=0x600119A  System.Void WriteInt(System.Void* statePtr, System.Int32 value)
  RVA=0x030D1BC0  token=0x600119B  System.Single ReadFloat(System.Void* statePtr)
  RVA=0x0B2438D8  token=0x600119C  System.Void WriteFloat(System.Void* statePtr, System.Single value)
  RVA=0x0B2428D4  token=0x600119D  UnityEngine.InputSystem.Utilities.PrimitiveValue FloatToPrimitiveValue(System.Single value)
  RVA=0x0B243324  token=0x600119E  System.Double ReadDouble(System.Void* statePtr)
  RVA=0x0B2435D8  token=0x600119F  System.Void WriteDouble(System.Void* statePtr, System.Double value)
  RVA=0x0426F500  token=0x60011A0  System.Void Write(System.Void* statePtr, UnityEngine.InputSystem.Utilities.PrimitiveValue value)
  RVA=0x04B39120  token=0x60011A1  System.Void CopyToFrom(System.Void* toStatePtr, System.Void* fromStatePtr)
  RVA=0x04B0A8C0  token=0x60011A2  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputStateBuffers
TYPE:  sealed struct
TOKEN: 0x20001DB
SIZE:  0x48
FIELDS:
  public            System.UInt32                   sizePerBuffer  // 0x10
  public            System.UInt32                   totalSize  // 0x14
  public            System.Void*                    defaultStateBuffer  // 0x18
  public            System.Void*                    noiseMaskBuffer  // 0x20
  public            System.Void*                    resetMaskBuffer  // 0x28
  private           System.Void*                    m_AllBuffers  // 0x30
  private           UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffersm_PlayerStateBuffers  // 0x38
  private   static  System.Void*                    s_DefaultStateBuffer  // static @ 0x0
  private   static  System.Void*                    s_NoiseMaskBuffer  // static @ 0x8
  private   static  System.Void*                    s_ResetMaskBuffer  // static @ 0x10
  private   static  UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBufferss_CurrentBuffers  // static @ 0x18
METHODS:
  RVA=0x030CE850  token=0x60011A3  UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers GetDoubleBuffersFor(UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
  RVA=0x030D1250  token=0x60011A4  System.Void* GetFrontBufferForDevice(System.Int32 deviceIndex)
  RVA=0x0448E060  token=0x60011A5  System.Void* GetBackBufferForDevice(System.Int32 deviceIndex)
  RVA=0x030CE810  token=0x60011A6  System.Void SwitchTo(UnityEngine.InputSystem.LowLevel.InputStateBuffers buffers, UnityEngine.InputSystem.LowLevel.InputUpdateType update)
  RVA=0x03AE8D70  token=0x60011A7  System.Void AllocateAll(UnityEngine.InputSystem.InputDevice[] devices, System.Int32 deviceCount)
  RVA=0x03AE8E70  token=0x60011A8  UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers SetUpDeviceToBufferMappings(System.Int32 deviceCount, System.Byte*& bufferPtr, System.UInt32 sizePerBuffer, System.UInt32 mappingTableSizePerBuffer)
  RVA=0x03AE8CA0  token=0x60011A9  System.Void FreeAll()
  RVA=0x03AE8F10  token=0x60011AA  System.Void MigrateAll(UnityEngine.InputSystem.InputDevice[] devices, System.Int32 deviceCount, UnityEngine.InputSystem.LowLevel.InputStateBuffers oldBuffers)
  RVA=0x03AE8330  token=0x60011AB  System.Void MigrateDoubleBuffer(UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers newBuffer, UnityEngine.InputSystem.InputDevice[] devices, System.Int32 deviceCount, UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers oldBuffer)
  RVA=0x03AE90E0  token=0x60011AC  System.Void MigrateSingleBuffer(System.Void* newBuffer, UnityEngine.InputSystem.InputDevice[] devices, System.Int32 deviceCount, System.Void* oldBuffer)
  RVA=0x03AE9210  token=0x60011AD  System.UInt32 ComputeSizeOfSingleStateBuffer(UnityEngine.InputSystem.InputDevice[] devices, System.Int32 deviceCount)
  RVA=0x03AE9270  token=0x60011AE  System.UInt32 NextDeviceOffset(System.UInt32 currentOffset, UnityEngine.InputSystem.InputDevice device)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputStateHistory
TYPE:  class
TOKEN: 0x20001DD
SIZE:  0x68
IMPLEMENTS: System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
FIELDS:
  private   static  System.Int32                    kDefaultHistorySize  // const
  private           System.Action<UnityEngine.InputSystem.LowLevel.InputStateHistory.Record><onRecordAdded>k__BackingField  // 0x10
  private           System.Func<UnityEngine.InputSystem.InputControl,System.Double,UnityEngine.InputSystem.LowLevel.InputEventPtr,System.Boolean><onShouldRecordStateChange>k__BackingField  // 0x18
  private           UnityEngine.InputSystem.InputControl[]m_Controls  // 0x20
  private           System.Int32                    m_ControlCount  // 0x28
  private           Unity.Collections.NativeArray<System.Byte>m_RecordBuffer  // 0x30
  private           System.Int32                    m_StateSizeInBytes  // 0x40
  private           System.Int32                    m_RecordCount  // 0x44
  private           System.Int32                    m_HistoryDepth  // 0x48
  private           System.Int32                    m_ExtraMemoryPerRecord  // 0x4c
  private           System.Int32                    m_HeadIndex  // 0x50
  private           System.UInt32                   m_CurrentVersion  // 0x54
  private           System.Nullable<UnityEngine.InputSystem.LowLevel.InputUpdateType>m_UpdateMask  // 0x58
  private   readonly System.Boolean                  m_AddNewControls  // 0x60
PROPERTIES:
  Count  get=0x04D86530
  version  get=0x04D86540
  historyDepth  get=0x04D863D0  set=0x0B245328
  extraMemoryPerRecord  get=0x04D86600  set=0x042EF120
  updateMask  get=0x0B245160  set=0x0B245410
  controls  get=0x0B245100
  Item  get=0x0B244FB4  set=0x0B2451E4
  onRecordAdded  get=0x02B2ECC0  set=0x053908C0
  onShouldRecordStateChange  get=0x0385B100  set=0x05392C40
  bytesPerRecord  get=0x0B2450D8
METHODS:
  RVA=0x04B864C0  token=0x60011C4  System.Void .ctor(System.Int32 maxStateSizeInBytes)
  RVA=0x0B244D70  token=0x60011C5  System.Void .ctor(System.String path)
  RVA=0x0B244ECC  token=0x60011C6  System.Void .ctor(UnityEngine.InputSystem.InputControl control)
  RVA=0x0B244CFC  token=0x60011C7  System.Void .ctor(System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.InputControl> controls)
  RVA=0x0B244200  token=0x60011C8  System.Void Finalize()
  RVA=0x04DACED0  token=0x60011C9  System.Void Clear()
  RVA=0x0B243E48  token=0x60011CA  UnityEngine.InputSystem.LowLevel.InputStateHistory.Record AddRecord(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
  RVA=0x0B2449DC  token=0x60011CB  System.Void StartRecording()
  RVA=0x0B244B14  token=0x60011CC  System.Void StopRecording()
  RVA=0x0B244524  token=0x60011CD  UnityEngine.InputSystem.LowLevel.InputStateHistory.Record RecordStateChange(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x0B244710  token=0x60011CE  UnityEngine.InputSystem.LowLevel.InputStateHistory.Record RecordStateChange(UnityEngine.InputSystem.InputControl control, System.Void* statePtr, System.Double time)
  RVA=0x0B244270  token=0x60011CF  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.LowLevel.InputStateHistory.Record> GetEnumerator()
  RVA=0x0B244C40  token=0x60011D0  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B2441AC  token=0x60011D1  System.Void Dispose()
  RVA=0x0B244154  token=0x60011D2  System.Void Destroy()
  RVA=0x0B243F58  token=0x60011D3  System.Void Allocate()
  RVA=0x0B244510  token=0x60011D4  System.Int32 RecordIndexToUserIndex(System.Int32 index)
  RVA=0x04DACEE0  token=0x60011D5  System.Int32 UserIndexToRecordIndex(System.Int32 index)
  RVA=0x0B244324  token=0x60011D6  UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* GetRecord(System.Int32 index)
  RVA=0x0B2442D0  token=0x60011D7  UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* GetRecordUnchecked(System.Int32 index)
  RVA=0x0B243EC0  token=0x60011D8  UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* AllocateRecord(System.Int32& index)
  RVA=-1  // generic def  token=0x60011D9  TValue ReadValue(UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* data)
  RVA=0x0B244418  token=0x60011DA  System.Object ReadValueAsObject(UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* data)
  RVA=0x0B244C48  token=0x60011DB  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(UnityEngine.InputSystem.InputControl control, System.Double time, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.Int64 monitorIndex)
  RVA=0x041E1670  token=0x60011DC  System.Void UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(UnityEngine.InputSystem.InputControl control, System.Double time, System.Int64 monitorIndex, System.Int32 timerIndex)
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.InputStateHistory`1
TYPE:  class
TOKEN: 0x20001E3
EXTENDS: UnityEngine.InputSystem.LowLevel.InputStateHistory
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60011FD  System.Void .ctor(System.Nullable<System.Int32> maxStateSizeInBytes)
  RVA=-1  // not resolved  token=0x60011FE  System.Void .ctor(UnityEngine.InputSystem.InputControl<TValue> control)
  RVA=-1  // not resolved  token=0x60011FF  System.Void .ctor(System.String path)
  RVA=-1  // not resolved  token=0x6001200  System.Void Finalize()
  RVA=-1  // not resolved  token=0x6001201  UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue> AddRecord(UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue> record)
  RVA=-1  // not resolved  token=0x6001202  UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue> RecordStateChange(UnityEngine.InputSystem.InputControl<TValue> control, TValue value, System.Double time)
  RVA=-1  // not resolved  token=0x6001203  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.LowLevel.InputStateHistory.Record<TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6001204  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: UnityEngine.InputSystem.LowLevel.SelectObservable`2
TYPE:  class
TOKEN: 0x20001E6
IMPLEMENTS: System.IObservable`1
FIELDS:
  private   readonly System.IObservable<TSource>     m_Source  // 0x0
  private   readonly System.Func<TSource,TResult>    m_Filter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001223  System.Void .ctor(System.IObservable<TSource> source, System.Func<TSource,TResult> filter)
  RVA=-1  // not resolved  token=0x6001224  System.IDisposable Subscribe(System.IObserver<TResult> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.OnScreen.OnScreenButton
TYPE:  class
TOKEN: 0x2000133
SIZE:  0x38
EXTENDS: UnityEngine.InputSystem.OnScreen.OnScreenControl
IMPLEMENTS: UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerUpHandler
FIELDS:
  private           System.String                   m_ControlPath  // 0x30
PROPERTIES:
  controlPathInternal  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0B223FB0  token=0x6000E32  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B223F6C  token=0x6000E33  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x05393520  token=0x6000E36  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.OnScreen.OnScreenControl
TYPE:  abstract class
TOKEN: 0x2000134
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.InputSystem.InputControlm_Control  // 0x18
  private           UnityEngine.InputSystem.OnScreen.OnScreenControlm_NextControlOnDevice  // 0x20
  private           UnityEngine.InputSystem.LowLevel.InputEventPtrm_InputEventPtr  // 0x28
  private   static  System.Int32                    s_nbActiveInstances  // static @ 0x0
  private   static  UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo>s_OnScreenDevices  // static @ 0x8
PROPERTIES:
  controlPath  get=0x05395664  set=0x0B224E88
  control  get=0x0385B100
  controlPathInternal  get=-1  // abstract  set=-1  // abstract
  HasAnyActive  get=0x0B224E4C
METHODS:
  RVA=0x0B224630  token=0x6000E3C  System.Void SetupInputControl()
  RVA=-1  // generic def  token=0x6000E3D  System.Void SendValueToControl(TValue value)
  RVA=0x0B224598  token=0x6000E3E  System.Void SentDefaultValueToControl()
  RVA=0x0B2242B8  token=0x6000E40  System.Void OnEnable()
  RVA=0x0B224034  token=0x6000E41  System.Void OnDisable()
  RVA=0x0B223FEC  token=0x6000E42  System.String GetWarningMessage()
  RVA=0x05393520  token=0x6000E43  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.OnScreen.UGUIOnScreenControlUtils
TYPE:  static class
TOKEN: 0x2000136
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2291B8  token=0x6000E47  UnityEngine.RectTransform GetCanvasRectTransform(UnityEngine.Transform transform)
END_CLASS

CLASS: UnityEngine.InputSystem.OnScreen.OnScreenStick
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x90
EXTENDS: UnityEngine.InputSystem.OnScreen.OnScreenControl
IMPLEMENTS: UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IDragHandler
FIELDS:
  private   static  System.String                   kDynamicOriginClickable  // const
  private           System.Single                   m_MovementRange  // 0x30
  private           System.Single                   m_DynamicOriginRange  // 0x34
  private           System.String                   m_ControlPath  // 0x38
  private           UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviourm_Behaviour  // 0x40
  private           System.Boolean                  m_UseIsolatedInputActions  // 0x44
  private           UnityEngine.InputSystem.InputActionm_PointerDownAction  // 0x48
  private           UnityEngine.InputSystem.InputActionm_PointerMoveAction  // 0x50
  private           UnityEngine.Vector3             m_StartPos  // 0x58
  private           UnityEngine.Vector2             m_PointerDownPos  // 0x64
  private           System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>m_RaycastResults  // 0x70
  private           UnityEngine.EventSystems.PointerEventDatam_PointerEventData  // 0x78
  private           UnityEngine.InputSystem.Controls.TouchControlm_TouchControl  // 0x80
  private           System.Boolean                  m_IsIsolationActive  // 0x88
PROPERTIES:
  movementRange  get=0x04D86E20  set=0x04D86E30
  dynamicOriginRange  get=0x04D8E050  set=0x0B226C44
  useIsolatedInputActions  get=0x04D86A30  set=0x04D86A40
  controlPathInternal  get=0x04D85A50  set=0x046A91B0
  behaviour  get=0x04D865E0  set=0x04D86610
METHODS:
  RVA=0x0B226178  token=0x6000E48  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B2259A8  token=0x6000E49  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B226440  token=0x6000E4A  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B226450  token=0x6000E4B  System.Void Start()
  RVA=0x0B225910  token=0x6000E4C  System.Void OnDestroy()
  RVA=0x0B2250F8  token=0x6000E4D  System.Void BeginInteraction(UnityEngine.Vector2 pointerPosition, UnityEngine.Camera uiCamera)
  RVA=0x0B2255DC  token=0x6000E4E  System.Void MoveStick(UnityEngine.Vector2 pointerPosition, UnityEngine.Camera uiCamera)
  RVA=0x0B225408  token=0x6000E4F  System.Void EndInteraction()
  RVA=0x0B225C94  token=0x6000E50  System.Void OnPointerDown(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
  RVA=0x0B225C3C  token=0x6000E51  System.Void OnPointerChanged(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
  RVA=0x0B226230  token=0x6000E52  System.Void OnPointerMove(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
  RVA=0x0B226334  token=0x6000E53  System.Void OnPointerUp(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
  RVA=0x0B2254CC  token=0x6000E54  UnityEngine.Camera GetCameraFromCanvas()
  RVA=0x0B225A60  token=0x6000E55  System.Void OnDrawGizmosSelected()
  RVA=0x0B2252C8  token=0x6000E56  System.Void DrawGizmoCircle(UnityEngine.Vector2 center, System.Single radius)
  RVA=0x0B226AF8  token=0x6000E57  System.Void UpdateDynamicOriginClickableArea()
  RVA=0x0B226C20  token=0x6000E62  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.AxisDeadzoneProcessor
TYPE:  class
TOKEN: 0x20001E8
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   min  // 0x10
  public            System.Single                   max  // 0x14
PROPERTIES:
  minOrDefault  get=0x0B23991C
  maxOrDefault  get=0x0B2398BC
METHODS:
  RVA=0x0B239788  token=0x600122B  System.Single Process(System.Single value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B2397F8  token=0x600122C  System.String ToString()
  RVA=0x0B239894  token=0x600122D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.ClampProcessor
TYPE:  class
TOKEN: 0x20001E9
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   min  // 0x10
  public            System.Single                   max  // 0x14
METHODS:
  RVA=0x0B239E2C  token=0x600122E  System.Single Process(System.Single value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B239E48  token=0x600122F  System.String ToString()
  RVA=0x0B239ED8  token=0x6001230  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.CompensateDirectionProcessor
TYPE:  class
TOKEN: 0x20001EA
SIZE:  0x10
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
PROPERTIES:
  cachingPolicy  get=0x04D85B80
METHODS:
  RVA=0x0B23AB4C  token=0x6001231  UnityEngine.Vector3 Process(UnityEngine.Vector3 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B23AC7C  token=0x6001232  System.String ToString()
  RVA=0x0B23ACA8  token=0x6001234  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.CompensateRotationProcessor
TYPE:  class
TOKEN: 0x20001EB
SIZE:  0x10
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
PROPERTIES:
  cachingPolicy  get=0x04D85B80
METHODS:
  RVA=0x0B23ACD0  token=0x6001235  UnityEngine.Quaternion Process(UnityEngine.Quaternion value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B23ADD0  token=0x6001236  System.String ToString()
  RVA=0x0B23ADFC  token=0x6001238  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.InvertProcessor
TYPE:  class
TOKEN: 0x20001EC
SIZE:  0x10
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
METHODS:
  RVA=0x04DA64A0  token=0x6001239  System.Single Process(System.Single value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B246048  token=0x600123A  System.String ToString()
  RVA=0x0B246074  token=0x600123B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.InvertVector2Processor
TYPE:  class
TOKEN: 0x20001ED
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Boolean                  invertX  // 0x10
  public            System.Boolean                  invertY  // 0x11
METHODS:
  RVA=0x0B24609C  token=0x600123C  UnityEngine.Vector2 Process(UnityEngine.Vector2 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B2460F4  token=0x600123D  System.String ToString()
  RVA=0x0B24617C  token=0x600123E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.InvertVector3Processor
TYPE:  class
TOKEN: 0x20001EE
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Boolean                  invertX  // 0x10
  public            System.Boolean                  invertY  // 0x11
  public            System.Boolean                  invertZ  // 0x12
METHODS:
  RVA=0x0B2461B8  token=0x600123F  UnityEngine.Vector3 Process(UnityEngine.Vector3 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B246214  token=0x6001240  System.String ToString()
  RVA=0x0B2462C8  token=0x6001241  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.NormalizeProcessor
TYPE:  class
TOKEN: 0x20001EF
SIZE:  0x20
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   min  // 0x10
  public            System.Single                   max  // 0x14
  public            System.Single                   zero  // 0x18
METHODS:
  RVA=0x0B2472CC  token=0x6001242  System.Single Process(System.Single value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0426F6B0  token=0x6001243  System.Single Normalize(System.Single value, System.Single min, System.Single max, System.Single zero)
  RVA=0x0B24727C  token=0x6001244  System.Single Denormalize(System.Single value, System.Single min, System.Single max, System.Single zero)
  RVA=0x0B2472F8  token=0x6001245  System.String ToString()
  RVA=0x0B2473B8  token=0x6001246  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.NormalizeVector2Processor
TYPE:  class
TOKEN: 0x20001F0
SIZE:  0x10
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
METHODS:
  RVA=0x0B2473E0  token=0x6001247  UnityEngine.Vector2 Process(UnityEngine.Vector2 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B2473F8  token=0x6001248  System.String ToString()
  RVA=0x0B247424  token=0x6001249  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.NormalizeVector3Processor
TYPE:  class
TOKEN: 0x20001F1
SIZE:  0x10
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
METHODS:
  RVA=0x0B24744C  token=0x600124A  UnityEngine.Vector3 Process(UnityEngine.Vector3 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B24747C  token=0x600124B  System.String ToString()
  RVA=0x0B2474A8  token=0x600124C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.ScaleProcessor
TYPE:  class
TOKEN: 0x20001F2
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   factor  // 0x10
METHODS:
  RVA=0x04DACF20  token=0x600124D  System.Single Process(System.Single value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B24920C  token=0x600124E  System.String ToString()
  RVA=0x0B249270  token=0x600124F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.ScaleVector2Processor
TYPE:  class
TOKEN: 0x20001F3
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
METHODS:
  RVA=0x04DACF30  token=0x6001250  UnityEngine.Vector2 Process(UnityEngine.Vector2 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B2492AC  token=0x6001251  System.String ToString()
  RVA=0x0B24933C  token=0x6001252  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.ScaleVector3Processor
TYPE:  class
TOKEN: 0x20001F4
SIZE:  0x20
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public            System.Single                   z  // 0x18
METHODS:
  RVA=0x04DACF60  token=0x6001253  UnityEngine.Vector3 Process(UnityEngine.Vector3 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B249388  token=0x6001254  System.String ToString()
  RVA=0x0B249448  token=0x6001255  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Processors.StickDeadzoneProcessor
TYPE:  class
TOKEN: 0x20001F5
SIZE:  0x18
EXTENDS: UnityEngine.InputSystem.InputProcessor`1
FIELDS:
  public            System.Single                   min  // 0x10
  public            System.Single                   max  // 0x14
PROPERTIES:
  minOrDefault  get=0x0B249B00
  maxOrDefault  get=0x0B249AA0
METHODS:
  RVA=0x0B249964  token=0x6001258  UnityEngine.Vector2 Process(UnityEngine.Vector2 value, UnityEngine.InputSystem.InputControl control)
  RVA=0x0B2498F4  token=0x6001259  System.Single GetDeadZoneAdjustedValue(System.Single value)
  RVA=0x0B2499DC  token=0x600125A  System.String ToString()
  RVA=0x0B249A78  token=0x600125B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Switch.SwitchProControllerHID
TYPE:  class
TOKEN: 0x2000126
SIZE:  0x230
EXTENDS: UnityEngine.InputSystem.Gamepad
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver UnityEngine.InputSystem.LowLevel.IEventPreProcessor
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<captureButton>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.ButtonControl<homeButton>k__BackingField  // 0x218
  private   static readonly UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[]s_HandshakeSequence  // static @ 0x0
  private           System.Int32                    m_HandshakeStepIndex  // 0x220
  private           System.Double                   m_HandshakeTimer  // 0x228
  private   static  System.Byte                     JitterMaskLow  // const
  private   static  System.Byte                     JitterMaskHigh  // const
PROPERTIES:
  captureButton  get=0x04D8D8B0  set=0x06B8B238
  homeButton  get=0x04D8D3D0  set=0x06B1D640
METHODS:
  RVA=0x0B227680  token=0x6000E18  System.Void OnAdded()
  RVA=0x0B2273E4  token=0x6000E19  System.Void HandshakeRestart()
  RVA=0x0B227440  token=0x6000E1A  System.Void HandshakeTick()
  RVA=0x0B227728  token=0x6000E1B  System.Void OnNextUpdate()
  RVA=0x0B227730  token=0x6000E1C  System.Void OnStateEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x012081B0  token=0x6000E1D  System.Boolean GetStateOffsetForEvent(UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, System.UInt32& offset)
  RVA=0x0B227888  token=0x6000E1E  System.Boolean PreProcessEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
  RVA=0x053971D4  token=0x6000E1F  System.Void .ctor()
  RVA=0x0B227A0C  token=0x6000E20  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Switch.SwitchSupportHID
TYPE:  static class
TOKEN: 0x2000130
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03AF3430  token=0x6000E2B  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState
TYPE:  sealed struct
TOKEN: 0x2000131
SIZE:  0x18
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public    static  UnityEngine.InputSystem.Utilities.FourCCFormat  // static @ 0x0
  public            System.Byte                     leftStickX  // 0x10
  public            System.Byte                     leftStickY  // 0x11
  public            System.Byte                     rightStickX  // 0x12
  public            System.Byte                     rightStickY  // 0x13
  public            System.UInt16                   buttons1  // 0x14
  public            System.Byte                     buttons2  // 0x16
PROPERTIES:
  format  get=0x0B227394
METHODS:
  RVA=0x0B2272F8  token=0x6000E2D  UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState WithButton(UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button button, System.Boolean value)
  RVA=0x0B220288  token=0x6000E2E  System.Void Set(UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button button, System.Boolean state)
  RVA=0x0B227248  token=0x6000E2F  System.Void Press(UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button button)
  RVA=0x0B2272A0  token=0x6000E30  System.Void Release(UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button button)
  RVA=0x0B227358  token=0x6000E31  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.BaseInputOverride
TYPE:  class
TOKEN: 0x2000112
SIZE:  0x20
EXTENDS: UnityEngine.EventSystems.BaseInput
FIELDS:
  private   readonly System.String                   <compositionString>k__BackingField  // 0x18
PROPERTIES:
  compositionString  get=0x0385B100
METHODS:
  RVA=0x05393520  token=0x6000D21  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.ExtendedAxisEventData
TYPE:  class
TOKEN: 0x2000113
SIZE:  0x38
EXTENDS: UnityEngine.EventSystems.AxisEventData
IMPLEMENTS: UnityEngine.InputSystem.UI.INavigationEventData
FIELDS:
  private           UnityEngine.InputSystem.InputDevice<device>k__BackingField  // 0x30
PROPERTIES:
  device  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0B2207EC  token=0x6000D24  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
  RVA=0x0B220754  token=0x6000D25  System.String ToString()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.ExtendedPointerEventData
TYPE:  class
TOKEN: 0x2000114
SIZE:  0x198
EXTENDS: UnityEngine.EventSystems.PointerEventData
FIELDS:
  private           UnityEngine.InputSystem.InputControl<control>k__BackingField  // 0x160
  private           UnityEngine.InputSystem.InputDevice<device>k__BackingField  // 0x168
  private           System.Int32                    <touchId>k__BackingField  // 0x170
  private           UnityEngine.InputSystem.UI.UIPointerType<pointerType>k__BackingField  // 0x174
  private           System.Int32                    <uiToolkitPointerId>k__BackingField  // 0x178
  private           UnityEngine.Vector3             <trackedDevicePosition>k__BackingField  // 0x17c
  private           UnityEngine.Quaternion          <trackedDeviceOrientation>k__BackingField  // 0x188
PROPERTIES:
  control  get=0x04D8D8F0  set=0x06B8B288
  device  get=0x04D8DF00  set=0x06B8B954
  touchId  get=0x04D903B0  set=0x04D90430
  pointerType  get=0x04DAC1F0  set=0x04DAC230
  uiToolkitPointerId  get=0x04DA2300  set=0x04DA6C50
  trackedDevicePosition  get=0x04DAC210  set=0x04DAC250
  trackedDeviceOrientation  get=0x04DAC200  set=0x04DAC240
METHODS:
  RVA=0x05397DC4  token=0x6000D26  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
  RVA=0x0B220A84  token=0x6000D35  System.String ToString()
  RVA=0x04DAC1E0  token=0x6000D36  System.Int32 MakePointerIdForTouch(System.Int32 deviceId, System.Int32 touchId)
  RVA=0x04D94BA0  token=0x6000D37  System.Int32 TouchIdFromPointerId(System.Int32 pointerId)
  RVA=0x033F54E0  token=0x6000D38  System.Void ReadDeviceState()
  RVA=0x0B2207F4  token=0x6000D39  System.Int32 GetPenPointerId(UnityEngine.InputSystem.Pen pen)
  RVA=0x0B2209A8  token=0x6000D3A  System.Int32 GetTouchPointerId(UnityEngine.InputSystem.Controls.TouchControl touchControl)
END_CLASS

CLASS: UnityEngine.InputSystem.UI.UIPointerType
TYPE:  sealed struct
TOKEN: 0x2000115
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.UI.UIPointerTypeNone  // const
  public    static  UnityEngine.InputSystem.UI.UIPointerTypeMouseOrPen  // const
  public    static  UnityEngine.InputSystem.UI.UIPointerTypeTouch  // const
  public    static  UnityEngine.InputSystem.UI.UIPointerTypeTracked  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.UI.UIPointerBehavior
TYPE:  sealed struct
TOKEN: 0x2000116
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.UI.UIPointerBehaviorSingleMouseOrPenButMultiTouchAndTrack  // const
  public    static  UnityEngine.InputSystem.UI.UIPointerBehaviorSingleUnifiedPointer  // const
  public    static  UnityEngine.InputSystem.UI.UIPointerBehaviorAllPointersAsIs  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.UI.ExtendedSubmitCancelEventData
TYPE:  class
TOKEN: 0x2000117
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.BaseEventData
IMPLEMENTS: UnityEngine.InputSystem.UI.INavigationEventData
FIELDS:
  private           UnityEngine.InputSystem.InputDevice<device>k__BackingField  // 0x20
PROPERTIES:
  device  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x05392C40  token=0x6000D3D  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
END_CLASS

CLASS: UnityEngine.InputSystem.UI.INavigationEventData
TYPE:  interface
TOKEN: 0x2000118
FIELDS:
PROPERTIES:
  device  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.UI.InputSystemUIInputModule
TYPE:  class
TOKEN: 0x2000119
SIZE:  0x398
EXTENDS: UnityEngine.EventSystems.BaseInputModule
FIELDS:
  private   static  System.Single                   kClickSpeed  // const
  private           System.Single                   m_MoveRepeatDelay  // 0x48
  private           System.Single                   m_MoveRepeatRate  // 0x4c
  private           System.Single                   m_TrackedDeviceDragThresholdMultiplier  // 0x50
  private           UnityEngine.Transform           m_XRTrackingOrigin  // 0x58
  private   static  UnityEngine.InputSystem.DefaultInputActionsdefaultActions  // static @ 0x0
  private           UnityEngine.InputSystem.InputActionAssetm_ActionsAsset  // 0x60
  private           UnityEngine.InputSystem.InputActionReferencem_PointAction  // 0x68
  private           UnityEngine.InputSystem.InputActionReferencem_MoveAction  // 0x70
  private           UnityEngine.InputSystem.InputActionReferencem_SubmitAction  // 0x78
  private           UnityEngine.InputSystem.InputActionReferencem_CancelAction  // 0x80
  private           UnityEngine.InputSystem.InputActionReferencem_LeftClickAction  // 0x88
  private           UnityEngine.InputSystem.InputActionReferencem_MiddleClickAction  // 0x90
  private           UnityEngine.InputSystem.InputActionReferencem_RightClickAction  // 0x98
  private           UnityEngine.InputSystem.InputActionReferencem_ScrollWheelAction  // 0xa0
  private           UnityEngine.InputSystem.InputActionReferencem_TrackedDevicePositionAction  // 0xa8
  private           UnityEngine.InputSystem.InputActionReferencem_TrackedDeviceOrientationAction  // 0xb0
  private           System.Boolean                  m_DeselectOnBackgroundClick  // 0xb8
  private           UnityEngine.InputSystem.UI.UIPointerBehaviorm_PointerBehavior  // 0xbc
  private           UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehaviorm_CursorLockBehavior  // 0xc0
  private           System.Single                   m_ScrollDeltaPerTick  // 0xc4
  private   static  System.Collections.Generic.Dictionary<UnityEngine.InputSystem.InputAction,UnityEngine.InputSystem.UI.InputSystemUIInputModule.InputActionReferenceState>s_InputActionReferenceCounts  // static @ 0x8
  private           System.Boolean                  m_ActionsHooked  // 0xc8
  private           System.Boolean                  m_NeedToPurgeStalePointers  // 0xc9
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnPointDelegate  // 0xd0
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnMoveDelegate  // 0xd8
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnSubmitCancelDelegate  // 0xe0
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnLeftClickDelegate  // 0xe8
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnRightClickDelegate  // 0xf0
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnMiddleClickDelegate  // 0xf8
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnScrollWheelDelegate  // 0x100
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnTrackedDevicePositionDelegate  // 0x108
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_OnTrackedDeviceOrientationDelegate  // 0x110
  private           System.Action<System.Object>    m_OnControlsChangedDelegate  // 0x118
  private           System.Int32                    m_CurrentPointerId  // 0x120
  private           System.Int32                    m_CurrentPointerIndex  // 0x124
  private           UnityEngine.InputSystem.UI.UIPointerTypem_CurrentPointerType  // 0x128
  private           UnityEngine.InputSystem.Utilities.InlinedArray<System.Int32>m_PointerIds  // 0x130
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.UI.PointerModel>m_PointerStates  // 0x140
  private           UnityEngine.InputSystem.UI.NavigationModelm_NavigationState  // 0x358
  private           UnityEngine.InputSystem.UI.SubmitCancelModelm_SubmitCancelState  // 0x380
  private           UnityEngine.GameObject          m_LocalMultiPlayerRoot  // 0x390
PROPERTIES:
  deselectOnBackgroundClick  get=0x04D892C0  set=0x04D89320
  pointerBehavior  get=0x04DA32C0  set=0x04DA3360
  cursorLockBehavior  get=0x04DA4B70  set=0x04DA5DC0
  localMultiPlayerRoot  get=0x04D8DD80  set=0x06B8B79C
  scrollDeltaPerTick  get=0x04D9E380  set=0x04D9E510
  moveRepeatDelay  get=0x04D8D350  set=0x04D8D380
  moveRepeatRate  get=0x04D87860  set=0x04D91480
  explictlyIgnoreFocus  get=0x033F2090
  shouldIgnoreFocus  get=0x033F2030
  repeatRate  get=0x04D87860  set=0x04D91480
  repeatDelay  get=0x04D8D350  set=0x04D8D380
  xrTrackingOrigin  get=0x02E56440  set=0x036E5590
  trackedDeviceDragThresholdMultiplier  get=0x04D885A0  set=0x04D88FF0
  point  get=0x04D86230  set=0x0B223934
  scrollWheel  get=0x04D86210  set=0x0B2239A0
  leftClick  get=0x04D861F0  set=0x0B223890
  middleClick  get=0x04D86220  set=0x0B2238C8
  rightClick  get=0x04D85EE0  set=0x0B223968
  move  get=0x04D862B0  set=0x0B223900
  submit  get=0x04D862A0  set=0x0B2239D8
  cancel  get=0x04D86260  set=0x0B223858
  trackedDeviceOrientation  get=0x04D86250  set=0x0B223A0C
  trackedDevicePosition  get=0x04D86290  set=0x0B223A44
  trackedDeviceSelect  get=0x0B22364C  set=0x0B223A7C
  actionsAsset  get=0x04D86280  set=0x0B223698
  sendPointerHoverToParent  get=0x02FFF600
METHODS:
  RVA=0x03E9A360  token=0x6000D49  System.Void ActivateModule()
  RVA=0x0B222814  token=0x6000D4A  System.Boolean IsPointerOverGameObject(System.Int32 pointerOrTouchId)
  RVA=0x0B222458  token=0x6000D4B  UnityEngine.EventSystems.RaycastResult GetLastRaycastResult(System.Int32 pointerOrTouchId)
  RVA=0x033F31A0  token=0x6000D4C  UnityEngine.EventSystems.RaycastResult PerformRaycast(UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
  RVA=0x033F2AB0  token=0x6000D4D  System.Void ProcessPointer(UnityEngine.InputSystem.UI.PointerModel& state)
  RVA=0x033EF9F0  token=0x6000D4E  System.Boolean PointerShouldIgnoreTransform(UnityEngine.Transform t)
  RVA=0x033F3670  token=0x6000D4F  System.Void ProcessPointerMovement(UnityEngine.InputSystem.UI.PointerModel& pointer, UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
  RVA=0x033F2120  token=0x6000D50  System.Void ProcessPointerMovement(UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData, UnityEngine.GameObject currentPointerTarget)
  RVA=0x033F13A0  token=0x6000D51  System.Void ProcessPointerButton(UnityEngine.InputSystem.UI.PointerModel.ButtonState& button, UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x033F1CB0  token=0x6000D52  System.Void ProcessPointerButtonDrag(UnityEngine.InputSystem.UI.PointerModel.ButtonState& button, UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
  RVA=0x04073080  token=0x6000D53  System.Void ProcessPointerScroll(UnityEngine.InputSystem.UI.PointerModel& pointer, UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x033F5600  token=0x6000D54  System.Void ProcessNavigation(UnityEngine.InputSystem.UI.NavigationModel& navigationState)
  RVA=0x0B22265C  token=0x6000D55  System.Boolean IsMoveAllowed(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0B223074  token=0x6000D64  System.Void SwapAction(UnityEngine.InputSystem.InputActionReference& property, UnityEngine.InputSystem.InputActionReference newValue, System.Boolean actionsHooked, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> actionCallback)
  RVA=0x0B221F24  token=0x6000D79  System.Void AssignDefaultActions()
  RVA=0x0B223498  token=0x6000D7A  System.Void UnassignActions()
  RVA=0x04D831F0  token=0x6000D7D  System.Void Awake()
  RVA=0x0B222938  token=0x6000D7E  System.Void OnDestroy()
  RVA=0x03DFA6E0  token=0x6000D7F  System.Void OnEnable()
  RVA=0x0B222940  token=0x6000D80  System.Void OnDisable()
  RVA=0x03DFA810  token=0x6000D81  System.Void ResetPointers()
  RVA=0x03DFA8B0  token=0x6000D82  System.Boolean HasNoActions()
  RVA=0x03DFC030  token=0x6000D83  System.Void EnableAllActions()
  RVA=0x0B222384  token=0x6000D84  System.Void DisableAllActions()
  RVA=0x03DFC0F0  token=0x6000D85  System.Void EnableInputAction(UnityEngine.InputSystem.InputActionReference inputActionReference)
  RVA=0x0B223320  token=0x6000D86  System.Void TryDisableInputAction(UnityEngine.InputSystem.InputActionReference inputActionReference, System.Boolean isComponentDisabling)
  RVA=0x0B222550  token=0x6000D87  System.Int32 GetPointerStateIndexFor(System.Int32 pointerOrTouchId)
  RVA=0x033F3B00  token=0x6000D88  UnityEngine.InputSystem.UI.PointerModel& GetPointerStateForIndex(System.Int32 index)
  RVA=0x03526D80  token=0x6000D89  System.Int32 GetDisplayIndexFor(UnityEngine.InputSystem.InputControl control)
  RVA=0x03526A90  token=0x6000D8A  System.Int32 GetPointerStateIndexFor(UnityEngine.InputSystem.InputAction.CallbackContext& context)
  RVA=0x03526B50  token=0x6000D8B  System.Int32 GetPointerStateIndexFor(UnityEngine.InputSystem.InputControl control, System.Boolean createIfNotExists)
  RVA=0x048BA810  token=0x6000D8C  System.Int32 AllocatePointer(System.Int32 pointerId, System.Int32 displayIndex, System.Int32 touchId, UnityEngine.InputSystem.UI.UIPointerType pointerType, UnityEngine.InputSystem.InputControl control, UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputControl touchControl)
  RVA=0x0B222FA8  token=0x6000D8D  System.Boolean SendPointerExitEventsAndRemovePointer(System.Int32 index)
  RVA=0x0B222D08  token=0x6000D8E  System.Boolean RemovePointerAtIndex(System.Int32 index)
  RVA=0x03DFA950  token=0x6000D8F  System.Void PurgeStalePointers()
  RVA=0x03DFB300  token=0x6000D90  System.Boolean HaveControlForDevice(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputActionReference actionReference)
  RVA=0x03526E10  token=0x6000D91  System.Void OnPointCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x035269C0  token=0x6000D92  System.Boolean IgnoreNextClick(UnityEngine.InputSystem.InputAction.CallbackContext& context, System.Boolean wasPressed)
  RVA=0x03526A00  token=0x6000D93  System.Void OnLeftClickCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x035264C0  token=0x6000D94  System.Void OnRightClickCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B2229DC  token=0x6000D95  System.Void OnMiddleClickCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x03529820  token=0x6000D96  System.Boolean CheckForRemovedDevice(UnityEngine.InputSystem.InputAction.CallbackContext& context)
  RVA=0x0B222B00  token=0x6000D97  System.Void OnScrollCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B222A6C  token=0x6000D98  System.Void OnMoveCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B222BA8  token=0x6000D99  System.Void OnSubmitCancelCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B222BE8  token=0x6000D9A  System.Void OnTrackedDeviceOrientationCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B222C74  token=0x6000D9B  System.Void OnTrackedDevicePositionCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x04D85900  token=0x6000D9C  System.Void OnControlsChanged(System.Object obj)
  RVA=0x033F3880  token=0x6000D9D  System.Void FilterPointerStatesByType()
  RVA=0x033F36C0  token=0x6000D9E  System.Void Process()
  RVA=0x0B2222EC  token=0x6000D9F  System.Int32 ConvertUIToolkitPointerId(UnityEngine.EventSystems.PointerEventData sourcePointerData)
  RVA=0x03DF9E00  token=0x6000DA0  System.Void HookActions()
  RVA=0x0B2235AC  token=0x6000DA1  System.Void UnhookActions()
  RVA=0x03DFC250  token=0x6000DA2  System.Void SetActionCallbacks(System.Boolean install)
  RVA=0x03DFC3C0  token=0x6000DA3  System.Void SetActionCallback(UnityEngine.InputSystem.InputActionReference actionReference, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install)
  RVA=0x0B2235C0  token=0x6000DA4  UnityEngine.InputSystem.InputActionReference UpdateReferenceForNewAsset(UnityEngine.InputSystem.InputActionReference actionReference)
  RVA=0x0445D490  token=0x6000DA8  System.Void .ctor()
  RVA=0x04D126D0  token=0x6000DA9  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.MultiplayerEventSystem
TYPE:  class
TOKEN: 0x200011C
SIZE:  0x58
EXTENDS: UnityEngine.EventSystems.EventSystem
FIELDS:
  private           UnityEngine.GameObject          m_PlayerRoot  // 0x50
PROPERTIES:
  playerRoot  get=0x04D86270  set=0x0B223F48
METHODS:
  RVA=0x0B223E28  token=0x6000DAC  System.Void OnEnable()
  RVA=0x0B223E20  token=0x6000DAD  System.Void OnDisable()
  RVA=0x0B223D6C  token=0x6000DAE  System.Void InitializePlayerRoot()
  RVA=0x0B223E48  token=0x6000DAF  System.Void Update()
  RVA=0x0B223EFC  token=0x6000DB0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.NavigationModel
TYPE:  sealed struct
TOKEN: 0x200011D
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector2             move  // 0x10
  public            System.Int32                    consecutiveMoveCount  // 0x18
  public            UnityEngine.EventSystems.MoveDirectionlastMoveDirection  // 0x1c
  public            System.Single                   lastMoveTime  // 0x20
  public            UnityEngine.EventSystems.AxisEventDataeventData  // 0x28
  public            UnityEngine.InputSystem.InputDevicedevice  // 0x30
METHODS:
  RVA=0x053982E0  token=0x6000DB1  System.Void Reset()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.SubmitCancelModel
TYPE:  sealed struct
TOKEN: 0x200011E
SIZE:  0x20
FIELDS:
  public            UnityEngine.EventSystems.BaseEventDataeventData  // 0x10
  public            UnityEngine.InputSystem.InputDevicedevice  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.UI.PointerModel
TYPE:  sealed struct
TOKEN: 0x200011F
SIZE:  0x218
FIELDS:
  public            System.Boolean                  changedThisFrame  // 0x10
  public            UnityEngine.InputSystem.UI.PointerModel.ButtonStateleftButton  // 0x18
  public            UnityEngine.InputSystem.UI.PointerModel.ButtonStaterightButton  // 0xa8
  public            UnityEngine.InputSystem.UI.PointerModel.ButtonStatemiddleButton  // 0x138
  public            UnityEngine.InputSystem.UI.ExtendedPointerEventDataeventData  // 0x1c8
  private           UnityEngine.Vector2             m_ScreenPosition  // 0x1d0
  private           UnityEngine.Vector2             m_ScrollDelta  // 0x1d8
  private           UnityEngine.Vector3             m_WorldPosition  // 0x1e0
  private           UnityEngine.Quaternion          m_WorldOrientation  // 0x1ec
  private           System.Single                   m_Pressure  // 0x1fc
  private           System.Single                   m_AzimuthAngle  // 0x200
  private           System.Single                   m_AltitudeAngle  // 0x204
  private           System.Single                   m_Twist  // 0x208
  private           UnityEngine.Vector2             m_Radius  // 0x20c
PROPERTIES:
  pointerType  get=0x033F3B20
  screenPosition  get=0x04DAC2F0  set=0x03526B00
  worldPosition  get=0x04D911E0  set=0x0B226CAC
  worldOrientation  get=0x04DAC330  set=0x0B226C60
  scrollDelta  get=0x04DAC310  set=0x033F3BD0
  pressure  get=0x04DAC2C0  set=0x033F3D70
  azimuthAngle  get=0x04D92930  set=0x033F3D50
  altitudeAngle  get=0x04D92920  set=0x033F3D30
  twist  get=0x04D911D0  set=0x033F3D10
  radius  get=0x04DAC2D0  set=0x033F3D90
METHODS:
  RVA=0x048BAA20  token=0x6000DC5  System.Void .ctor(UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
  RVA=0x033F3B50  token=0x6000DC6  System.Void OnFrameFinished()
  RVA=0x033F3C20  token=0x6000DC7  System.Void CopyTouchOrPenStateFrom(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.InputSystem.UI.TrackedDeviceRaycaster
TYPE:  class
TOKEN: 0x2000121
SIZE:  0x40
EXTENDS: UnityEngine.EventSystems.BaseRaycaster
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData>m_RaycastResultsCache  // 0x20
  private   static  UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.UI.TrackedDeviceRaycaster>s_Instances  // static @ 0x0
  private   static readonly System.Collections.Generic.List<UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData>s_SortedGraphics  // static @ 0x18
  private           System.Boolean                  m_IgnoreReversedGraphics  // 0x28
  private           System.Boolean                  m_CheckFor2DOcclusion  // 0x29
  private           System.Boolean                  m_CheckFor3DOcclusion  // 0x2a
  private           System.Single                   m_MaxDistance  // 0x2c
  private           UnityEngine.LayerMask           m_BlockingMask  // 0x30
  private           UnityEngine.Canvas              m_Canvas  // 0x38
PROPERTIES:
  eventCamera  get=0x0B228E64
  blockingMask  get=0x04D864F0  set=0x04D86740
  checkFor3DOcclusion  get=0x04D86CA0  set=0x04D86CD0
  checkFor2DOcclusion  get=0x04D867B0  set=0x04D867C0
  ignoreReversedGraphics  get=0x011F33C0  set=0x011F33D0
  maxDistance  get=0x04D86AF0  set=0x04D86B00
  canvas  get=0x0B228DE0
METHODS:
  RVA=0x0B227E30  token=0x6000DE0  System.Void OnEnable()
  RVA=0x0B227D68  token=0x6000DE1  System.Void OnDisable()
  RVA=0x0B228860  token=0x6000DE2  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
  RVA=0x0B227EA0  token=0x6000DE3  System.Void PerformRaycast(UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
  RVA=0x0B2288D4  token=0x6000DE4  System.Void SortedRaycastGraphics(UnityEngine.Canvas canvas, UnityEngine.Ray ray, System.Collections.Generic.List<UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData> results)
  RVA=0x0B228374  token=0x6000DE5  System.Boolean RayIntersectsRectTransform(UnityEngine.RectTransform transform, UnityEngine.Ray ray, UnityEngine.Vector3& worldPosition, System.Single& distance)
  RVA=0x0B228D58  token=0x6000DE7  System.Void .ctor()
  RVA=0x0B228CC4  token=0x6000DE8  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.UI.VirtualMouseInput
TYPE:  class
TOKEN: 0x2000124
SIZE:  0x118
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.InputSystem.UI.VirtualMouseInput.CursorModem_CursorMode  // 0x18
  private           UnityEngine.UI.Graphic          m_CursorGraphic  // 0x20
  private           UnityEngine.RectTransform       m_CursorTransform  // 0x28
  private           System.Single                   m_CursorSpeed  // 0x30
  private           System.Single                   m_ScrollSpeed  // 0x34
  private           UnityEngine.InputSystem.InputActionPropertym_StickAction  // 0x38
  private           UnityEngine.InputSystem.InputActionPropertym_LeftButtonAction  // 0x50
  private           UnityEngine.InputSystem.InputActionPropertym_MiddleButtonAction  // 0x68
  private           UnityEngine.InputSystem.InputActionPropertym_RightButtonAction  // 0x80
  private           UnityEngine.InputSystem.InputActionPropertym_ForwardButtonAction  // 0x98
  private           UnityEngine.InputSystem.InputActionPropertym_BackButtonAction  // 0xb0
  private           UnityEngine.InputSystem.InputActionPropertym_ScrollWheelAction  // 0xc8
  private           UnityEngine.Canvas              m_Canvas  // 0xe0
  private           UnityEngine.InputSystem.Mouse   m_VirtualMouse  // 0xe8
  private           UnityEngine.InputSystem.Mouse   m_SystemMouse  // 0xf0
  private           System.Action                   m_AfterInputUpdateDelegate  // 0xf8
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_ButtonActionTriggeredDelegate  // 0x100
  private           System.Double                   m_LastTime  // 0x108
  private           UnityEngine.Vector2             m_LastStickValue  // 0x110
PROPERTIES:
  cursorTransform  get=0x04D86240  set=0x02FDB880
  cursorSpeed  get=0x04D86E20  set=0x04D86E30
  cursorMode  get=0x011EF5B0  set=0x0B22A3AC
  cursorGraphic  get=0x04D862C0  set=0x0B22A388
  scrollSpeed  get=0x04D8E050  set=0x04D8E060
  virtualMouse  get=0x04D88590
  stickAction  get=0x04D8F4A0  set=0x0B22A780
  leftButtonAction  get=0x04D9EAA0  set=0x0B22A548
  rightButtonAction  get=0x04DAC460  set=0x0B22A698
  middleButtonAction  get=0x04D9D1C0  set=0x0B22A5F0
  forwardButtonAction  get=0x04DAC440  set=0x0B22A490
  backButtonAction  get=0x04DAC420  set=0x0B22A2D0
  scrollWheelAction  get=0x04DAC480  set=0x0B22A750
METHODS:
  RVA=0x0B229688  token=0x6000E0A  System.Void OnEnable()
  RVA=0x0B2293F0  token=0x6000E0B  System.Void OnDisable()
  RVA=0x0B229D94  token=0x6000E0C  System.Void TryFindCanvas()
  RVA=0x0B229B9C  token=0x6000E0D  System.Void TryEnableHardwareCursor()
  RVA=0x0B229DF4  token=0x6000E0E  System.Void UpdateMotion()
  RVA=0x0B229258  token=0x6000E0F  System.Void OnButtonActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x09E32E34  token=0x6000E10  System.Void SetActionCallback(UnityEngine.InputSystem.InputActionProperty field, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install)
  RVA=0x0B229AA4  token=0x6000E11  System.Void SetAction(UnityEngine.InputSystem.InputActionProperty& field, UnityEngine.InputSystem.InputActionProperty value)
  RVA=0x0B229250  token=0x6000E12  System.Void OnAfterInputUpdate()
  RVA=0x0B22A2AC  token=0x6000E13  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Users.InputUser
TYPE:  sealed struct
TOKEN: 0x2000106
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    static  System.UInt32                   InvalidId  // const
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputUserOnChangeMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  k_InputCheckForUnpairMarker  // static @ 0x8
  private           System.UInt32                   m_Id  // 0x10
  private   static  UnityEngine.InputSystem.Users.InputUser.GlobalStates_GlobalState  // static @ 0x10
PROPERTIES:
  valid  get=0x0B210F6C
  index  get=0x0B210A94
  id  get=0x02B77620
  platformUserAccountHandle  get=0x0B210DE8
  platformUserAccountName  get=0x0B210EF4
  platformUserAccountId  get=0x0B210E7C
  pairedDevices  get=0x0B210D04
  lostDevices  get=0x0B210C1C
  actions  get=0x0B2107E0
  controlScheme  get=0x0B210984
  controlSchemeMatch  get=0x0B2108D4
  hasMissingRequiredDevices  get=0x0B210A18
  all  get=0x0B210858
  listenForUnpairedDeviceActivity  get=0x0B210BC8  set=0x0B21128C
EVENTS:
  onChange  add=add_onChange  remove=remove_onChange
  onUnpairedDeviceUsed  add=add_onUnpairedDeviceUsed  remove=remove_onUnpairedDeviceUsed
  onPrefilterUnpairedDeviceActivity  add=add_onPrefilterUnpairedDeviceActivity  remove=remove_onPrefilterUnpairedDeviceActivity
METHODS:
  RVA=0x0B20E910  token=0x6000CC5  System.String ToString()
  RVA=0x0B20B0A4  token=0x6000CC6  System.Void AssociateActionsWithUser(UnityEngine.InputSystem.IInputActionCollection actions)
  RVA=0x0B20A92C  token=0x6000CC7  UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax ActivateControlScheme(System.String schemeName)
  RVA=0x0B20EB34  token=0x6000CC8  System.Boolean TryFindControlScheme(System.String schemeName, UnityEngine.InputSystem.InputControlScheme& scheme)
  RVA=0x0B20B51C  token=0x6000CC9  System.Void FindControlScheme(System.String schemeName, UnityEngine.InputSystem.InputControlScheme& scheme)
  RVA=0x0B20A780  token=0x6000CCA  UnityEngine.InputSystem.Users.InputUser.ControlSchemeChangeSyntax ActivateControlScheme(UnityEngine.InputSystem.InputControlScheme scheme)
  RVA=0x0B20A398  token=0x6000CCB  System.Void ActivateControlSchemeInternal(System.Int32 userIndex, UnityEngine.InputSystem.InputControlScheme scheme)
  RVA=0x0B20F2C4  token=0x6000CCC  System.Void UnpairDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20F420  token=0x6000CCD  System.Void UnpairDevices()
  RVA=0x0B20E16C  token=0x6000CCE  System.Void RemoveLostDevicesForUser(System.Int32 userIndex)
  RVA=0x0B20F3C8  token=0x6000CCF  System.Void UnpairDevicesAndRemoveUser()
  RVA=0x0B20BB18  token=0x6000CD0  UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputDevice> GetUnpairedInputDevices()
  RVA=0x0B20B93C  token=0x6000CD1  System.Int32 GetUnpairedInputDevices(UnityEngine.InputSystem.InputControlList<UnityEngine.InputSystem.InputDevice>& list)
  RVA=0x0B20B840  token=0x6000CD2  System.Nullable<UnityEngine.InputSystem.Users.InputUser> FindUserPairedToDevice(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20B6F8  token=0x6000CD3  System.Nullable<UnityEngine.InputSystem.Users.InputUser> FindUserByAccount(UnityEngine.InputSystem.Users.InputUserAccountHandle platformUserAccountHandle)
  RVA=0x0B20B374  token=0x6000CD4  UnityEngine.InputSystem.Users.InputUser CreateUserWithoutPairedDevices()
  RVA=0x0B20D7BC  token=0x6000CD5  UnityEngine.InputSystem.Users.InputUser PerformPairingWithDevice(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Users.InputUser user, UnityEngine.InputSystem.Users.InputUserPairingOptions options)
  RVA=0x0B20C01C  token=0x6000CD6  System.Boolean InitiateUserAccountSelection(System.Int32 userIndex, UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.Users.InputUserPairingOptions options)
  RVA=0x02EED260  token=0x6000CD7  System.Boolean Equals(UnityEngine.InputSystem.Users.InputUser other)
  RVA=0x0B20B4B4  token=0x6000CD8  System.Boolean Equals(System.Object obj)
  RVA=0x02B77620  token=0x6000CD9  System.Int32 GetHashCode()
  RVA=0x07872540  token=0x6000CDA  System.Boolean op_Equality(UnityEngine.InputSystem.Users.InputUser left, UnityEngine.InputSystem.Users.InputUser right)
  RVA=0x0AAED910  token=0x6000CDB  System.Boolean op_Inequality(UnityEngine.InputSystem.Users.InputUser left, UnityEngine.InputSystem.Users.InputUser right)
  RVA=0x0B20AF28  token=0x6000CDC  System.Int32 AddUser()
  RVA=0x0B20E358  token=0x6000CDD  System.Void RemoveUser(System.Int32 userIndex)
  RVA=0x0B20C180  token=0x6000CDE  System.Void Notify(System.Int32 userIndex, UnityEngine.InputSystem.Users.InputUserChange change, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20EDC0  token=0x6000CDF  System.Int32 TryFindUserIndex(System.UInt32 userId)
  RVA=0x0B20EFA4  token=0x6000CE0  System.Int32 TryFindUserIndex(UnityEngine.InputSystem.Users.InputUserAccountHandle platformHandle)
  RVA=0x0B20EE6C  token=0x6000CE1  System.Int32 TryFindUserIndex(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20A9F4  token=0x6000CE2  System.Void AddDeviceToUser(System.Int32 userIndex, UnityEngine.InputSystem.InputDevice device, System.Boolean asLostDevice, System.Boolean dontUpdateControlScheme)
  RVA=0x0B20DC3C  token=0x6000CE3  System.Void RemoveDeviceFromUser(System.Int32 userIndex, UnityEngine.InputSystem.InputDevice device, System.Boolean asLostDevice)
  RVA=0x0B20F634  token=0x6000CE4  System.Void UpdateControlSchemeMatch(System.Int32 userIndex, System.Boolean autoPairMissing)
  RVA=0x0B20FFA4  token=0x6000CE5  System.Int64 UpdatePlatformUserAccount(System.Int32 userIndex, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20DA28  token=0x6000CE6  System.Int64 QueryPairedPlatformUserAccount(UnityEngine.InputSystem.InputDevice device, System.Nullable<UnityEngine.InputSystem.Users.InputUserAccountHandle>& platformAccountHandle, System.String& platformAccountName, System.String& platformAccountId)
  RVA=0x0B20BF5C  token=0x6000CE7  System.Boolean InitiateUserAccountSelectionAtPlatformLevel(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20C424  token=0x6000CE8  System.Void OnActionChange(System.Object obj, UnityEngine.InputSystem.InputActionChange change)
  RVA=0x0B20C4FC  token=0x6000CE9  System.Void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, UnityEngine.InputSystem.InputDeviceChange change)
  RVA=0x0B20B638  token=0x6000CEA  System.Int32 FindLostDevice(UnityEngine.InputSystem.InputDevice device, System.Int32 startIndex)
  RVA=0x0B20CB98  token=0x6000CEB  System.Void OnEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B20E6D8  token=0x6000CEC  UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
  RVA=0x0B20BB88  token=0x6000CED  System.Void HookIntoActionChange()
  RVA=0x0B20F0B4  token=0x6000CEE  System.Void UnhookFromActionChange()
  RVA=0x0B20BCDC  token=0x6000CEF  System.Void HookIntoDeviceChange()
  RVA=0x0B20F188  token=0x6000CF0  System.Void UnhookFromDeviceChange()
  RVA=0x0B20BE1C  token=0x6000CF1  System.Void HookIntoEvents()
  RVA=0x0B20F224  token=0x6000CF2  System.Void UnhookFromDeviceStateChange()
  RVA=0x0B20B3DC  token=0x6000CF3  System.Void DisposeAndResetGlobalState()
  RVA=0x0B20E68C  token=0x6000CF4  System.Void ResetGlobals()
  RVA=0x0B2104E8  token=0x6000CF5  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Users.InputUserAccountHandle
TYPE:  sealed struct
TOKEN: 0x200010E
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.String                   m_ApiName  // 0x10
  private           System.UInt64                   m_Handle  // 0x18
PROPERTIES:
  apiName  get=0x02B76770
  handle  get=0x04D88320
METHODS:
  RVA=0x0B223CAC  token=0x6000CFF  System.Void .ctor(System.String apiName, System.UInt64 handle)
  RVA=0x0B223C00  token=0x6000D00  System.String ToString()
  RVA=0x0B223AC8  token=0x6000D01  System.Boolean Equals(UnityEngine.InputSystem.Users.InputUserAccountHandle other)
  RVA=0x0B223B58  token=0x6000D02  System.Boolean Equals(System.Object obj)
  RVA=0x0B223D2C  token=0x6000D03  System.Boolean op_Equality(UnityEngine.InputSystem.Users.InputUserAccountHandle left, UnityEngine.InputSystem.Users.InputUserAccountHandle right)
  RVA=0x0B223D4C  token=0x6000D04  System.Boolean op_Inequality(UnityEngine.InputSystem.Users.InputUserAccountHandle left, UnityEngine.InputSystem.Users.InputUserAccountHandle right)
  RVA=0x0B223BC8  token=0x6000D05  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.InputSystem.Users.InputUserChange
TYPE:  sealed struct
TOKEN: 0x200010F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAdded  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeRemoved  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeDevicePaired  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeDeviceUnpaired  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeDeviceLost  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeDeviceRegained  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAccountChanged  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAccountNameChanged  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAccountSelectionInProgress  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAccountSelectionCanceled  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeAccountSelectionComplete  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeControlSchemeChanged  // const
  public    static  UnityEngine.InputSystem.Users.InputUserChangeControlsChanged  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Users.InputUserPairingOptions
TYPE:  sealed struct
TOKEN: 0x2000110
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.Users.InputUserPairingOptionsNone  // const
  public    static  UnityEngine.InputSystem.Users.InputUserPairingOptionsForcePlatformUserAccountSelection  // const
  public    static  UnityEngine.InputSystem.Users.InputUserPairingOptionsForceNoPlatformUserAccountSelection  // const
  public    static  UnityEngine.InputSystem.Users.InputUserPairingOptionsUnpairCurrentDevicesFromUser  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.Users.InputUserSettings
TYPE:  class
TOKEN: 0x2000111
SIZE:  0x40
FIELDS:
  private           System.String                   <customBindings>k__BackingField  // 0x10
  private           System.Boolean                  <invertMouseX>k__BackingField  // 0x18
  private           System.Boolean                  <invertMouseY>k__BackingField  // 0x19
  private           System.Nullable<System.Single>  <mouseSmoothing>k__BackingField  // 0x1c
  private           System.Nullable<System.Single>  <mouseSensitivity>k__BackingField  // 0x24
  private           System.Boolean                  <invertStickX>k__BackingField  // 0x2c
  private           System.Boolean                  <invertStickY>k__BackingField  // 0x2d
  private           System.Boolean                  <swapSticks>k__BackingField  // 0x2e
  private           System.Boolean                  <swapBumpers>k__BackingField  // 0x2f
  private           System.Boolean                  <swapTriggers>k__BackingField  // 0x30
  private           System.Boolean                  <swapDpadAndLeftStick>k__BackingField  // 0x31
  private           System.Single                   <vibrationStrength>k__BackingField  // 0x34
  private           System.String                   m_CustomBindings  // 0x38
PROPERTIES:
  customBindings  get=0x02B2ECC0  set=0x053908C0
  invertMouseX  get=0x04D86340  set=0x04D86350
  invertMouseY  get=0x04D866A0  set=0x04D866C0
  mouseSmoothing  get=0x04DAAC00  set=0x04DAC2B0
  mouseSensitivity  get=0x04DAC2A0  set=0x04DA5EC0
  invertStickX  get=0x04D869F0  set=0x04D86A00
  invertStickY  get=0x04D86CB0  set=0x04D86CE0
  swapSticks  get=0x04DA8630  set=0x04DA8650
  swapBumpers  get=0x04DA8640  set=0x04DA8660
  swapTriggers  get=0x04D865F0  set=0x04D86620
  swapDpadAndLeftStick  get=0x04D86AB0  set=0x04D86AC0
  vibrationStrength  get=0x04D8E050  set=0x04D8E060
METHODS:
  RVA=0x041E1670  token=0x6000D1E  System.Void Apply(UnityEngine.InputSystem.IInputActionCollection actions)
  RVA=0x041E1670  token=0x6000D1F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.InputActionTrace
TYPE:  sealed class
TOKEN: 0x2000231
SIZE:  0xA8
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IDisposable
FIELDS:
  private           System.Boolean                  m_SubscribedToAll  // 0x10
  private           System.Boolean                  m_OnActionChangeHooked  // 0x11
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputAction>m_SubscribedActions  // 0x18
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputActionMap>m_SubscribedActionMaps  // 0x30
  private           UnityEngine.InputSystem.LowLevel.InputEventBufferm_EventBuffer  // 0x48
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputActionState>m_ActionMapStates  // 0x68
  private           UnityEngine.InputSystem.Utilities.InlinedArray<UnityEngine.InputSystem.InputActionState>m_ActionMapStateClones  // 0x80
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_CallbackDelegate  // 0x98
  private           System.Action<System.Object,UnityEngine.InputSystem.InputActionChange>m_ActionChangeDelegate  // 0xa0
PROPERTIES:
  buffer  get=0x04DABA00
  count  get=0x04D86790
METHODS:
  RVA=0x041E1670  token=0x600147C  System.Void .ctor()
  RVA=0x0B1B91F8  token=0x600147D  System.Void .ctor(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B9264  token=0x600147E  System.Void .ctor(UnityEngine.InputSystem.InputActionMap actionMap)
  RVA=0x0B1B8970  token=0x600147F  System.Void SubscribeToAll()
  RVA=0x0B1B8F28  token=0x6001480  System.Void UnsubscribeFromAll()
  RVA=0x0B1B8B50  token=0x6001481  System.Void SubscribeTo(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B8A28  token=0x6001482  System.Void SubscribeTo(UnityEngine.InputSystem.InputActionMap actionMap)
  RVA=0x0B1B8FE0  token=0x6001483  System.Void UnsubscribeFrom(UnityEngine.InputSystem.InputAction action)
  RVA=0x0B1B9100  token=0x6001484  System.Void UnsubscribeFrom(UnityEngine.InputSystem.InputActionMap actionMap)
  RVA=0x0B1B87E4  token=0x6001485  System.Void RecordAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B1B819C  token=0x6001486  System.Void Clear()
  RVA=0x0B1B83DC  token=0x6001487  System.Void Finalize()
  RVA=0x0B1B8CA8  token=0x6001488  System.String ToString()
  RVA=0x0B1B83BC  token=0x6001489  System.Void Dispose()
  RVA=0x0B1B82B8  token=0x600148A  System.Void DisposeInternal()
  RVA=0x0B1B844C  token=0x600148B  System.Collections.Generic.IEnumerator<UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr> GetEnumerator()
  RVA=0x0B1B8CA0  token=0x600148C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B1B84CC  token=0x600148D  System.Void HookOnActionChange()
  RVA=0x0B1B8ECC  token=0x600148E  System.Void UnhookOnActionChange()
  RVA=0x0B1B858C  token=0x600148F  System.Void OnActionChange(System.Object actionOrMapOrAsset, UnityEngine.InputSystem.InputActionChange change)
  RVA=0x0B1B81E8  token=0x6001490  System.Void CloneActionStateBeforeBindingsChange(UnityEngine.InputSystem.InputActionMap actionMap)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ArrayHelpers
TYPE:  static class
TOKEN: 0x2000234
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60014A3  System.Int32 LengthSafe(TValue[] array)
  RVA=-1  // generic def  token=0x60014A4  System.Void Clear(TValue[] array)
  RVA=-1  // generic def  token=0x60014A5  System.Void Clear(TValue[] array, System.Int32 count)
  RVA=-1  // generic def  token=0x60014A6  System.Void Clear(TValue[] array, System.Int32& count)
  RVA=-1  // generic def  token=0x60014A7  System.Void EnsureCapacity(TValue[]& array, System.Int32 count, System.Int32 capacity, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014A8  System.Void DuplicateWithCapacity(TValue[]& array, System.Int32 count, System.Int32 capacity, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014A9  System.Boolean Contains(TValue[] array, TValue value)
  RVA=-1  // generic def  token=0x60014AA  System.Boolean ContainsReference(TValue[] array, TValue value)
  RVA=-1  // generic def  token=0x60014AB  System.Boolean ContainsReference(TFirst[] array, System.Int32 count, TSecond value)
  RVA=-1  // generic def  token=0x60014AC  System.Boolean ContainsReference(TFirst[] array, System.Int32 startIndex, System.Int32 count, TSecond value)
  RVA=-1  // generic def  token=0x60014AD  System.Boolean HaveDuplicateReferences(TFirst[] first, System.Int32 index, System.Int32 count)
  RVA=-1  // generic def  token=0x60014AE  System.Boolean HaveEqualElements(TValue[] first, TValue[] second, System.Int32 count)
  RVA=-1  // generic def  token=0x60014AF  System.Int32 IndexOf(TValue[] array, TValue value, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x60014B0  System.Int32 IndexOf(TValue[] array, System.Predicate<TValue> predicate)
  RVA=-1  // generic def  token=0x60014B1  System.Int32 IndexOf(TValue[] array, System.Predicate<TValue> predicate, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x60014B2  System.Int32 IndexOfReference(TFirst[] array, TSecond value, System.Int32 count)
  RVA=-1  // generic def  token=0x60014B3  System.Int32 IndexOfReference(TFirst[] array, TSecond value, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x60014B4  System.Int32 IndexOfValue(TValue[] array, TValue value, System.Int32 startIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x60014B5  System.Void Resize(Unity.Collections.NativeArray<TValue>& array, System.Int32 newSize, Unity.Collections.Allocator allocator)
  RVA=-1  // generic def  token=0x60014B6  System.Int32 Append(TValue[]& array, TValue value)
  RVA=-1  // generic def  token=0x60014B7  System.Int32 Append(TValue[]& array, System.Collections.Generic.IEnumerable<TValue> values)
  RVA=-1  // generic def  token=0x60014B8  System.Int32 AppendToImmutable(TValue[]& array, TValue[] values)
  RVA=-1  // generic def  token=0x60014B9  System.Int32 AppendWithCapacity(TValue[]& array, System.Int32& count, TValue value, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014BA  System.Int32 AppendListWithCapacity(TValue[]& array, System.Int32& length, TValues values, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014BB  System.Int32 AppendWithCapacity(Unity.Collections.NativeArray<TValue>& array, System.Int32& count, TValue value, System.Int32 capacityIncrement, Unity.Collections.Allocator allocator)
  RVA=-1  // generic def  token=0x60014BC  System.Void InsertAt(TValue[]& array, System.Int32 index, TValue value)
  RVA=-1  // generic def  token=0x60014BD  System.Void InsertAtWithCapacity(TValue[]& array, System.Int32& count, System.Int32 index, TValue value, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014BE  System.Void PutAtIfNotSet(TValue[]& array, System.Int32 index, System.Func<TValue> valueFn)
  RVA=-1  // generic def  token=0x60014BF  System.Int32 GrowBy(TValue[]& array, System.Int32 count)
  RVA=-1  // generic def  token=0x60014C0  System.Int32 GrowBy(Unity.Collections.NativeArray<TValue>& array, System.Int32 count, Unity.Collections.Allocator allocator)
  RVA=-1  // generic def  token=0x60014C1  System.Int32 GrowWithCapacity(TValue[]& array, System.Int32& count, System.Int32 growBy, System.Int32 capacityIncrement)
  RVA=-1  // generic def  token=0x60014C2  System.Int32 GrowWithCapacity(Unity.Collections.NativeArray<TValue>& array, System.Int32& count, System.Int32 growBy, System.Int32 capacityIncrement, Unity.Collections.Allocator allocator)
  RVA=-1  // generic def  token=0x60014C3  TValue[] Join(TValue value, TValue[] values)
  RVA=-1  // generic def  token=0x60014C4  TValue[] Merge(TValue[] first, TValue[] second)
  RVA=-1  // generic def  token=0x60014C5  TValue[] Merge(TValue[] first, TValue[] second, System.Collections.Generic.IEqualityComparer<TValue> comparer)
  RVA=-1  // generic def  token=0x60014C6  System.Void EraseAt(TValue[]& array, System.Int32 index)
  RVA=-1  // generic def  token=0x60014C7  System.Void EraseAtWithCapacity(TValue[] array, System.Int32& count, System.Int32 index)
  RVA=-1  // generic def  token=0x60014C8  System.Void EraseAtWithCapacity(Unity.Collections.NativeArray<TValue> array, System.Int32& count, System.Int32 index)
  RVA=-1  // generic def  token=0x60014C9  System.Boolean Erase(TValue[]& array, TValue value)
  RVA=-1  // generic def  token=0x60014CA  System.Void EraseAtByMovingTail(TValue[] array, System.Int32& count, System.Int32 index)
  RVA=-1  // generic def  token=0x60014CB  TValue[] Copy(TValue[] array)
  RVA=-1  // generic def  token=0x60014CC  TValue[] Clone(TValue[] array)
  RVA=-1  // generic def  token=0x60014CD  TNew[] Select(TOld[] array, System.Func<TOld,TNew> converter)
  RVA=-1  // generic def  token=0x60014CE  System.Void Swap(TValue& first, TValue& second)
  RVA=-1  // generic def  token=0x60014CF  System.Void MoveSlice(TValue[] array, System.Int32 sourceIndex, System.Int32 destinationIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x60014D0  System.Void EraseSliceWithCapacity(TValue[]& array, System.Int32& length, System.Int32 index, System.Int32 count)
  RVA=-1  // generic def  token=0x60014D1  System.Void SwapElements(TValue[] array, System.Int32 index1, System.Int32 index2)
  RVA=-1  // generic def  token=0x60014D2  System.Void SwapElements(Unity.Collections.NativeArray<TValue> array, System.Int32 index1, System.Int32 index2)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.CallbackArray`1
TYPE:  sealed struct
TOKEN: 0x2000238
FIELDS:
  private           System.Boolean                  m_CannotMutateCallbacksArray  // 0x0
  private           UnityEngine.InputSystem.Utilities.InlinedArray<TDelegate>m_Callbacks  // 0x0
  private           UnityEngine.InputSystem.Utilities.InlinedArray<TDelegate>m_CallbacksToAdd  // 0x0
  private           UnityEngine.InputSystem.Utilities.InlinedArray<TDelegate>m_CallbacksToRemove  // 0x0
PROPERTIES:
  length  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60014DA  System.Void Clear()
  RVA=-1  // not resolved  token=0x60014DB  System.Void AddCallback(TDelegate dlg)
  RVA=-1  // not resolved  token=0x60014DC  System.Void RemoveCallback(TDelegate dlg)
  RVA=-1  // not resolved  token=0x60014DD  System.Void LockForChanges()
  RVA=-1  // not resolved  token=0x60014DE  System.Void UnlockForChanges()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.Vector2MagnitudeComparer
TYPE:  sealed struct
TOKEN: 0x2000239
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=0x0B1C2A6C  token=0x60014DF  System.Int32 Compare(UnityEngine.Vector2 x, UnityEngine.Vector2 y)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.Vector3MagnitudeComparer
TYPE:  sealed struct
TOKEN: 0x200023A
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=0x0B1C2FD0  token=0x60014E0  System.Int32 Compare(UnityEngine.Vector3 x, UnityEngine.Vector3 y)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.CSharpCodeHelpers
TYPE:  static class
TOKEN: 0x200023B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1B6A50  token=0x60014E1  System.Boolean IsProperIdentifier(System.String name)
  RVA=0x0B1B697C  token=0x60014E2  System.Boolean IsEmptyOrProperIdentifier(System.String name)
  RVA=0x0B1B699C  token=0x60014E3  System.Boolean IsEmptyOrProperNamespaceName(System.String name)
  RVA=0x0B1B6B04  token=0x60014E4  System.String MakeIdentifier(System.String name, System.String suffix)
  RVA=0x0B1B6CE8  token=0x60014E5  System.String MakeTypeName(System.String name, System.String suffix)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.DelegateHelpers
TYPE:  static class
TOKEN: 0x200023C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02EE2F30  token=0x60014E6  System.Void InvokeCallbacksSafe(UnityEngine.InputSystem.Utilities.CallbackArray<System.Action>& callbacks, Unity.Profiling.ProfilerMarker marker, System.String callbackName, System.Object context)
  RVA=-1  // generic def  token=0x60014E7  System.Void InvokeCallbacksSafe(UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<TValue>>& callbacks, TValue argument, System.String callbackName, System.Object context)
  RVA=-1  // generic def  token=0x60014E8  System.Void InvokeCallbacksSafe(UnityEngine.InputSystem.Utilities.CallbackArray<System.Action<TValue1,TValue2>>& callbacks, TValue1 argument1, TValue2 argument2, Unity.Profiling.ProfilerMarker marker, System.String callbackName, System.Object context)
  RVA=-1  // generic def  token=0x60014E9  System.Boolean InvokeCallbacksSafe_AnyCallbackReturnsTrue(UnityEngine.InputSystem.Utilities.CallbackArray<System.Func<TValue1,TValue2,System.Boolean>>& callbacks, TValue1 argument1, TValue2 argument2, System.String callbackName, System.Object context)
  RVA=-1  // generic def  token=0x60014EA  System.Void InvokeCallbacksSafe_AndInvokeReturnedActions(UnityEngine.InputSystem.Utilities.CallbackArray<System.Func<TValue,System.Action>>& callbacks, TValue argument, System.String callbackName, System.Object context)
  RVA=-1  // generic def  token=0x60014EB  System.Boolean InvokeCallbacksSafe_AnyCallbackReturnsObject(UnityEngine.InputSystem.Utilities.CallbackArray<System.Func<TValue,TReturn>>& callbacks, TValue argument, System.String callbackName, System.Object context)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.DisplayStringFormatAttribute
TYPE:  class
TOKEN: 0x200023D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   <formatString>k__BackingField  // 0x10
PROPERTIES:
  formatString  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x60014EE  System.Void .ctor(System.String formatString)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ExceptionHelpers
TYPE:  static class
TOKEN: 0x200023E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1B7C10  token=0x60014EF  System.Boolean IsExceptionIndicatingBugInCode(System.Exception exception)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.FourCC
TYPE:  sealed struct
TOKEN: 0x200023F
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.Int32                    m_Code  // 0x10
METHODS:
  RVA=0x04D86140  token=0x60014F0  System.Void .ctor(System.Int32 code)
  RVA=0x04DAB9C0  token=0x60014F1  System.Void .ctor(System.Char a, System.Char b, System.Char c, System.Char d)
  RVA=0x0B1B7E6C  token=0x60014F2  System.Void .ctor(System.String str)
  RVA=0x04D88BE0  token=0x60014F3  System.Int32 op_Implicit(UnityEngine.InputSystem.Utilities.FourCC fourCC)
  RVA=0x04D88BE0  token=0x60014F4  UnityEngine.InputSystem.Utilities.FourCC op_Implicit(System.Int32 i)
  RVA=0x0B1B7CF0  token=0x60014F5  System.String ToString()
  RVA=0x02EED260  token=0x60014F6  System.Boolean Equals(UnityEngine.InputSystem.Utilities.FourCC other)
  RVA=0x0B1B7C94  token=0x60014F7  System.Boolean Equals(System.Object obj)
  RVA=0x02B77620  token=0x60014F8  System.Int32 GetHashCode()
  RVA=0x07872540  token=0x60014F9  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.FourCC left, UnityEngine.InputSystem.Utilities.FourCC right)
  RVA=0x0AAED910  token=0x60014FA  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.FourCC left, UnityEngine.InputSystem.Utilities.FourCC right)
  RVA=0x04D88BE0  token=0x60014FB  UnityEngine.InputSystem.Utilities.FourCC FromInt32(System.Int32 i)
  RVA=0x04D88BE0  token=0x60014FC  System.Int32 ToInt32(UnityEngine.InputSystem.Utilities.FourCC fourCC)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.InlinedArray`1
TYPE:  sealed struct
TOKEN: 0x2000240
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  public            System.Int32                    length  // 0x0
  public            TValue                          firstValue  // 0x0
  public            TValue[]                        additionalValues  // 0x0
PROPERTIES:
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60014FE  System.Void .ctor(TValue value)
  RVA=-1  // not resolved  token=0x60014FF  System.Void .ctor(TValue firstValue, TValue[] additionalValues)
  RVA=-1  // not resolved  token=0x6001500  System.Void .ctor(System.Collections.Generic.IEnumerable<TValue> values)
  RVA=-1  // not resolved  token=0x6001503  System.Void Clear()
  RVA=-1  // not resolved  token=0x6001504  System.Void ClearWithCapacity()
  RVA=-1  // not resolved  token=0x6001505  UnityEngine.InputSystem.Utilities.InlinedArray<TValue> Clone()
  RVA=-1  // not resolved  token=0x6001506  System.Void SetLength(System.Int32 size)
  RVA=-1  // not resolved  token=0x6001507  TValue[] ToArray()
  RVA=-1  // generic def  token=0x6001508  TOther[] ToArray(System.Func<TValue,TOther> mapFunction)
  RVA=-1  // not resolved  token=0x6001509  System.Int32 IndexOf(TValue value)
  RVA=-1  // not resolved  token=0x600150A  System.Int32 Append(TValue value)
  RVA=-1  // not resolved  token=0x600150B  System.Int32 AppendWithCapacity(TValue value, System.Int32 capacityIncrement)
  RVA=-1  // not resolved  token=0x600150C  System.Void AssignWithCapacity(UnityEngine.InputSystem.Utilities.InlinedArray<TValue> values)
  RVA=-1  // not resolved  token=0x600150D  System.Void Append(System.Collections.Generic.IEnumerable<TValue> values)
  RVA=-1  // not resolved  token=0x600150E  System.Void Remove(TValue value)
  RVA=-1  // not resolved  token=0x600150F  System.Void RemoveAtWithCapacity(System.Int32 index)
  RVA=-1  // not resolved  token=0x6001510  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6001511  System.Void RemoveAtByMovingTailWithCapacity(System.Int32 index)
  RVA=-1  // not resolved  token=0x6001512  System.Boolean RemoveByMovingTailWithCapacity(TValue value)
  RVA=-1  // not resolved  token=0x6001513  System.Boolean Contains(TValue value, System.Collections.Generic.IEqualityComparer<TValue> comparer)
  RVA=-1  // not resolved  token=0x6001514  System.Void Merge(UnityEngine.InputSystem.Utilities.InlinedArray<TValue> other)
  RVA=-1  // not resolved  token=0x6001515  System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
  RVA=-1  // not resolved  token=0x6001516  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.InputArrayExtensions
TYPE:  static class
TOKEN: 0x2000242
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600151C  System.Int32 IndexOfReference(UnityEngine.InputSystem.Utilities.InlinedArray<TValue> array, TValue value)
  RVA=-1  // generic def  token=0x600151D  System.Boolean Contains(UnityEngine.InputSystem.Utilities.InlinedArray<TValue> array, TValue value)
  RVA=-1  // generic def  token=0x600151E  System.Boolean ContainsReference(UnityEngine.InputSystem.Utilities.InlinedArray<TValue> array, TValue value)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.InternedString
TYPE:  sealed struct
TOKEN: 0x2000243
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IComparable`1
FIELDS:
  private   readonly System.String                   m_StringOriginalCase  // 0x10
  private   readonly System.String                   m_StringLowerCase  // 0x18
PROPERTIES:
  length  get=0x0B1BEB0C
METHODS:
  RVA=0x03AEEE80  token=0x6001520  System.Void .ctor(System.String text)
  RVA=0x03AEF200  token=0x6001521  System.Boolean IsEmpty()
  RVA=0x04D88320  token=0x6001522  System.String ToLower()
  RVA=0x0B1BEA44  token=0x6001523  System.Boolean Equals(System.Object obj)
  RVA=0x03AECB80  token=0x6001524  System.Boolean Equals(UnityEngine.InputSystem.Utilities.InternedString other)
  RVA=0x0B1BEA2C  token=0x6001525  System.Int32 CompareTo(UnityEngine.InputSystem.Utilities.InternedString other)
  RVA=0x03AF1A10  token=0x6001526  System.Int32 GetHashCode()
  RVA=0x03AED380  token=0x6001527  System.String ToString()
  RVA=0x03AECB80  token=0x6001528  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.InternedString a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x03AECB70  token=0x6001529  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.InternedString a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x0B1BEB98  token=0x600152A  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.InternedString a, System.String b)
  RVA=0x0B1BECB4  token=0x600152B  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.InternedString a, System.String b)
  RVA=0x0B1BEB1C  token=0x600152C  System.Boolean op_Equality(System.String a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x0B1BEC38  token=0x600152D  System.Boolean op_Inequality(System.String a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x0B1BED30  token=0x600152E  System.Boolean op_LessThan(UnityEngine.InputSystem.Utilities.InternedString left, UnityEngine.InputSystem.Utilities.InternedString right)
  RVA=0x0B1BEC14  token=0x600152F  System.Boolean op_GreaterThan(UnityEngine.InputSystem.Utilities.InternedString left, UnityEngine.InputSystem.Utilities.InternedString right)
  RVA=0x05395130  token=0x6001530  System.String op_Implicit(UnityEngine.InputSystem.Utilities.InternedString str)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.JsonParser
TYPE:  sealed struct
TOKEN: 0x2000244
SIZE:  0x28
FIELDS:
  private   readonly System.String                   m_Text  // 0x10
  private   readonly System.Int32                    m_Length  // 0x18
  private           System.Int32                    m_Position  // 0x1c
  private           System.Boolean                  m_MatchAnyElementInArray  // 0x20
  private           System.Boolean                  m_DryRun  // 0x21
PROPERTIES:
  isAtEnd  get=0x0B1BF65C
METHODS:
  RVA=0x03AED3A0  token=0x6001531  System.Void .ctor(System.String json)
  RVA=0x04DABA20  token=0x6001532  System.Void Reset()
  RVA=0x0B1BF590  token=0x6001533  System.String ToString()
  RVA=0x03AED7F0  token=0x6001534  System.Boolean NavigateToProperty(System.String path)
  RVA=0x03AED3E0  token=0x6001535  System.Boolean CurrentPropertyHasValueEqualTo(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue expectedValue)
  RVA=0x03AED9B0  token=0x6001536  System.Boolean ParseToken(System.Char token)
  RVA=0x0B1BF4BC  token=0x6001537  System.Boolean ParseValue()
  RVA=0x03AED4D0  token=0x6001538  System.Boolean ParseValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x0B1BF328  token=0x6001539  System.Boolean ParseStringValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x0B1BED54  token=0x600153A  System.Boolean ParseArrayValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x0B1BF120  token=0x600153B  System.Boolean ParseObjectValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x03AED570  token=0x600153C  System.Boolean ParseNumber(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x0B1BEFD0  token=0x600153D  System.Boolean ParseBooleanValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x0B1BF0C8  token=0x600153E  System.Boolean ParseNullValue(UnityEngine.InputSystem.Utilities.JsonParser.JsonValue& result)
  RVA=0x03AEDA20  token=0x600153F  System.Boolean SkipToValue()
  RVA=0x0B1BF4F0  token=0x6001540  System.Boolean SkipString(System.String text)
  RVA=0x03AEDA70  token=0x6001541  System.Void SkipWhitespace()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.MemoryHelpers
TYPE:  static class
TOKEN: 0x2000249
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x030D19D0  token=0x6001560  System.Boolean Compare(System.Void* ptr1, System.Void* ptr2, UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion region)
  RVA=0x0B1C381C  token=0x6001561  System.UInt32 ComputeFollowingByteOffset(System.UInt32 byteOffset, System.UInt32 sizeInBits)
  RVA=0x045B0B60  token=0x6001562  System.Void WriteSingleBit(System.Void* ptr, System.UInt32 bitOffset, System.Boolean value)
  RVA=0x030D1A60  token=0x6001563  System.Boolean ReadSingleBit(System.Void* ptr, System.UInt32 bitOffset)
  RVA=0x0B1C3834  token=0x6001564  System.Void MemCpyBitRegion(System.Void* destination, System.Void* source, System.UInt32 bitOffset, System.UInt32 bitCount)
  RVA=0x030D17E0  token=0x6001565  System.Boolean MemCmpBitRegion(System.Void* ptr1, System.Void* ptr2, System.UInt32 bitOffset, System.UInt32 bitCount, System.Void* mask)
  RVA=0x0B1C3A1C  token=0x6001566  System.Void MemSet(System.Void* destination, System.Int32 numBytes, System.Byte value)
  RVA=0x0B1C3924  token=0x6001567  System.Void MemCpyMasked(System.Void* destination, System.Void* source, System.Int32 numBytes, System.Void* mask)
  RVA=0x0B1C3B38  token=0x6001568  System.UInt32 ReadMultipleBitsAsUInt(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount)
  RVA=0x0B1C3D94  token=0x6001569  System.Void WriteUIntAsMultipleBits(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount, System.UInt32 value)
  RVA=0x0B1C3D10  token=0x600156A  System.Int32 ReadTwosComplementMultipleBitsAsInt(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount)
  RVA=0x0B1C3D44  token=0x600156B  System.Void WriteIntAsTwosComplementMultipleBits(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount, System.Int32 value)
  RVA=0x0B1C3AD8  token=0x600156C  System.Int32 ReadExcessKMultipleBitsAsInt(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount)
  RVA=0x0B1C3D18  token=0x600156D  System.Void WriteIntAsExcessKMultipleBits(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount, System.Int32 value)
  RVA=0x0B1C3B04  token=0x600156E  System.Single ReadMultipleBitsAsNormalizedUInt(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount)
  RVA=0x0B1C3D54  token=0x600156F  System.Void WriteNormalizedUIntAsMultipleBits(System.Void* ptr, System.UInt32 bitOffset, System.UInt32 bitCount, System.Single value)
  RVA=0x03AEFB90  token=0x6001570  System.Void SetBitsInBuffer(System.Void* buffer, System.Int32 byteOffset, System.Int32 bitOffset, System.Int32 sizeInBits, System.Boolean value)
  RVA=-1  // generic def  token=0x6001571  System.Void Swap(TValue& a, TValue& b)
  RVA=0x0B1C37C8  token=0x6001572  System.UInt32 AlignNatural(System.UInt32 offset, System.UInt32 sizeInBytes)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.MiscHelpers
TYPE:  static class
TOKEN: 0x200024B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6001577  TValue GetValueOrDefault(System.Collections.Generic.Dictionary<TKey,TValue> dictionary, TKey key)
  RVA=-1  // generic def  token=0x6001578  System.Collections.Generic.IEnumerable<TValue> EveryNth(System.Collections.Generic.IEnumerable<TValue> enumerable, System.Int32 n, System.Int32 start)
  RVA=-1  // generic def  token=0x6001579  System.Int32 IndexOf(System.Collections.Generic.IEnumerable<TValue> enumerable, TValue value)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.NameAndParameters
TYPE:  sealed struct
TOKEN: 0x200024D
SIZE:  0x28
FIELDS:
  private           System.String                   <name>k__BackingField  // 0x10
  private           UnityEngine.InputSystem.Utilities.ReadOnlyArray<UnityEngine.InputSystem.Utilities.NamedValue><parameters>k__BackingField  // 0x18
PROPERTIES:
  name  get=0x02B76770  set=0x05392320
  parameters  get=0x015AD730  set=0x0B1C440C
METHODS:
  RVA=0x0B1C41D0  token=0x6001587  System.String ToString()
  RVA=0x0B1C3F9C  token=0x6001588  System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.Utilities.NameAndParameters> ParseMultiple(System.String text)
  RVA=0x0B1C3FF4  token=0x6001589  System.Boolean ParseMultiple(System.String text, System.Collections.Generic.List<UnityEngine.InputSystem.Utilities.NameAndParameters>& list)
  RVA=0x0B1C4148  token=0x600158A  System.String ParseName(System.String text)
  RVA=0x03DFDEF0  token=0x600158B  UnityEngine.InputSystem.Utilities.NameAndParameters Parse(System.String text)
  RVA=0x03DFDC40  token=0x600158C  UnityEngine.InputSystem.Utilities.NameAndParameters ParseNameAndParameters(System.String text, System.Int32& index, System.Boolean nameOnly)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.NamedValue
TYPE:  sealed struct
TOKEN: 0x200024F
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    static  System.String                   Separator  // const
  private           System.String                   <name>k__BackingField  // 0x10
  private           UnityEngine.InputSystem.Utilities.PrimitiveValue<value>k__BackingField  // 0x18
PROPERTIES:
  name  get=0x02B76770  set=0x05392320
  value  get=0x015AD730  set=0x04DA4E20
  type  get=0x04D88B10
METHODS:
  RVA=0x0B1C467C  token=0x6001595  UnityEngine.InputSystem.Utilities.NamedValue ConvertTo(System.TypeCode type)
  RVA=-1  // generic def  token=0x6001596  UnityEngine.InputSystem.Utilities.NamedValue From(System.String name, TValue value)
  RVA=0x0B1C4C2C  token=0x6001597  System.String ToString()
  RVA=0x0B1C46D4  token=0x6001598  System.Boolean Equals(UnityEngine.InputSystem.Utilities.NamedValue other)
  RVA=0x0B1C4730  token=0x6001599  System.Boolean Equals(System.Object obj)
  RVA=0x0B1C47A8  token=0x600159A  System.Int32 GetHashCode()
  RVA=0x0B1C4CA0  token=0x600159B  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.NamedValue left, UnityEngine.InputSystem.Utilities.NamedValue right)
  RVA=0x0B1C4CCC  token=0x600159C  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.NamedValue left, UnityEngine.InputSystem.Utilities.NamedValue right)
  RVA=0x0B1C47F4  token=0x600159D  UnityEngine.InputSystem.Utilities.NamedValue[] ParseMultiple(System.String parameterString)
  RVA=0x0B1C4BF0  token=0x600159E  UnityEngine.InputSystem.Utilities.NamedValue Parse(System.String str)
  RVA=0x0B1C4958  token=0x600159F  UnityEngine.InputSystem.Utilities.NamedValue ParseParameter(System.String parameterString, System.Int32& index)
  RVA=0x0B1C441C  token=0x60015A0  System.Void ApplyToObject(System.Object instance)
  RVA=-1  // generic def  token=0x60015A1  System.Void ApplyAllToObject(System.Object instance, TParameterList parameters)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.NumberHelpers
TYPE:  static class
TOKEN: 0x2000250
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1C4CF8  token=0x60015A2  System.Int32 AlignToMultipleOf(System.Int32 number, System.Int32 alignment)
  RVA=0x0B1C4D28  token=0x60015A3  System.Int64 AlignToMultipleOf(System.Int64 number, System.Int64 alignment)
  RVA=0x0B1C4D10  token=0x60015A4  System.UInt32 AlignToMultipleOf(System.UInt32 number, System.UInt32 alignment)
  RVA=0x0B1C4D44  token=0x60015A5  System.Boolean Approximately(System.Double a, System.Double b)
  RVA=0x0B1C4DE4  token=0x60015A6  System.Single IntToNormalizedFloat(System.Int32 value, System.Int32 minValue, System.Int32 maxValue)
  RVA=0x0B1C4E2C  token=0x60015A7  System.Int32 NormalizedFloatToInt(System.Single value, System.Int32 intMinValue, System.Int32 intMaxValue)
  RVA=0x0B1C4F04  token=0x60015A8  System.Single UIntToNormalizedFloat(System.UInt32 value, System.UInt32 minValue, System.UInt32 maxValue)
  RVA=0x0B1C4E74  token=0x60015A9  System.UInt32 NormalizedFloatToUInt(System.Single value, System.UInt32 uintMinValue, System.UInt32 uintMaxValue)
  RVA=0x0B1C4EC4  token=0x60015AA  System.UInt32 RemapUIntBitsToNormalizeFloatToUIntBits(System.UInt32 value, System.UInt32 inBitSize, System.UInt32 outBitSize)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ForDeviceEventObservable
TYPE:  class
TOKEN: 0x2000251
SIZE:  0x28
IMPLEMENTS: System.IObservable`1
FIELDS:
  private           System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr>m_Source  // 0x10
  private           UnityEngine.InputSystem.InputDevicem_Device  // 0x18
  private           System.Type                     m_DeviceType  // 0x20
METHODS:
  RVA=0x0B1C3618  token=0x60015AB  System.Void .ctor(System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr> source, System.Type deviceType, UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B1C3560  token=0x60015AC  System.IDisposable Subscribe(System.IObserver<UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.Observable
TYPE:  static class
TOKEN: 0x2000253
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60015B1  System.IObservable<TValue> Where(System.IObservable<TValue> source, System.Func<TValue,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x60015B2  System.IObservable<TResult> Select(System.IObservable<TSource> source, System.Func<TSource,TResult> filter)
  RVA=-1  // generic def  token=0x60015B3  System.IObservable<TResult> SelectMany(System.IObservable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> filter)
  RVA=-1  // generic def  token=0x60015B4  System.IObservable<TValue> Take(System.IObservable<TValue> source, System.Int32 count)
  RVA=0x0B1C4F54  token=0x60015B5  System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr> ForDevice(System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr> source, UnityEngine.InputSystem.InputDevice device)
  RVA=-1  // generic def  token=0x60015B6  System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr> ForDevice(System.IObservable<UnityEngine.InputSystem.LowLevel.InputEventPtr> source)
  RVA=-1  // generic def  token=0x60015B7  System.IDisposable CallOnce(System.IObservable<TValue> source, System.Action<TValue> action)
  RVA=-1  // generic def  token=0x60015B8  System.IDisposable Call(System.IObservable<TValue> source, System.Action<TValue> action)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.Observer`1
TYPE:  class
TOKEN: 0x2000255
IMPLEMENTS: System.IObserver`1
FIELDS:
  private           System.Action<TValue>           m_OnNext  // 0x0
  private           System.Action                   m_OnCompleted  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015BB  System.Void .ctor(System.Action<TValue> onNext, System.Action onCompleted)
  RVA=-1  // not resolved  token=0x60015BC  System.Void OnCompleted()
  RVA=-1  // not resolved  token=0x60015BD  System.Void OnError(System.Exception error)
  RVA=-1  // not resolved  token=0x60015BE  System.Void OnNext(TValue evt)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.SelectManyObservable`2
TYPE:  class
TOKEN: 0x2000256
IMPLEMENTS: System.IObservable`1
FIELDS:
  private   readonly System.IObservable<TSource>     m_Source  // 0x0
  private   readonly System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>>m_Filter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015BF  System.Void .ctor(System.IObservable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> filter)
  RVA=-1  // not resolved  token=0x60015C0  System.IDisposable Subscribe(System.IObserver<TResult> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.TakeNObservable`1
TYPE:  class
TOKEN: 0x2000258
IMPLEMENTS: System.IObservable`1
FIELDS:
  private           System.IObservable<TValue>      m_Source  // 0x0
  private           System.Int32                    m_Count  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015C5  System.Void .ctor(System.IObservable<TValue> source, System.Int32 count)
  RVA=-1  // not resolved  token=0x60015C6  System.IDisposable Subscribe(System.IObserver<TValue> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.WhereObservable`1
TYPE:  class
TOKEN: 0x200025A
IMPLEMENTS: System.IObservable`1
FIELDS:
  private   readonly System.IObservable<TValue>      m_Source  // 0x0
  private   readonly System.Func<TValue,System.Boolean>m_Predicate  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015CB  System.Void .ctor(System.IObservable<TValue> source, System.Func<TValue,System.Boolean> predicate)
  RVA=-1  // not resolved  token=0x60015CC  System.IDisposable Subscribe(System.IObserver<TValue> observer)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.OneOrMore`2
TYPE:  sealed struct
TOKEN: 0x200025C
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private   readonly System.Boolean                  m_IsSingle  // 0x0
  private   readonly TValue                          m_Single  // 0x0
  private   readonly TList                           m_Multiple  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60015D3  System.Void .ctor(TValue single)
  RVA=-1  // not resolved  token=0x60015D4  System.Void .ctor(TList multiple)
  RVA=-1  // not resolved  token=0x60015D5  UnityEngine.InputSystem.Utilities.OneOrMore<TValue,TList> op_Implicit(TValue single)
  RVA=-1  // not resolved  token=0x60015D6  UnityEngine.InputSystem.Utilities.OneOrMore<TValue,TList> op_Implicit(TList multiple)
  RVA=-1  // not resolved  token=0x60015D7  System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
  RVA=-1  // not resolved  token=0x60015D8  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.PredictiveParser
TYPE:  sealed struct
TOKEN: 0x200025E
SIZE:  0x14
FIELDS:
  private           System.Int32                    m_Position  // 0x10
METHODS:
  RVA=0x03A6E6D0  token=0x60015DF  System.Void ExpectSingleChar(System.ReadOnlySpan<System.Char> str, System.Char c)
  RVA=0x02F13A50  token=0x60015E0  System.Int32 ExpectInt(System.ReadOnlySpan<System.Char> str)
  RVA=0x02F13AF0  token=0x60015E1  System.ReadOnlySpan<System.Char> ExpectString(System.ReadOnlySpan<System.Char> str)
  RVA=0x02F13C90  token=0x60015E2  System.Boolean AcceptSingleChar(System.ReadOnlySpan<System.Char> str, System.Char c)
  RVA=0x03525080  token=0x60015E3  System.Boolean AcceptString(System.ReadOnlySpan<System.Char> input, System.ReadOnlySpan<System.Char>& output)
  RVA=0x03D4EB50  token=0x60015E4  System.Void AcceptInt(System.ReadOnlySpan<System.Char> str)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.PrimitiveValue
TYPE:  sealed struct
TOKEN: 0x200025F
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1 System.IConvertible
FIELDS:
  private           System.TypeCode                 m_Type  // 0x10
  private           System.Boolean                  m_BoolValue  // 0x14
  private           System.Char                     m_CharValue  // 0x14
  private           System.Byte                     m_ByteValue  // 0x14
  private           System.SByte                    m_SByteValue  // 0x14
  private           System.Int16                    m_ShortValue  // 0x14
  private           System.UInt16                   m_UShortValue  // 0x14
  private           System.Int32                    m_IntValue  // 0x14
  private           System.UInt32                   m_UIntValue  // 0x14
  private           System.Int64                    m_LongValue  // 0x14
  private           System.UInt64                   m_ULongValue  // 0x14
  private           System.Single                   m_FloatValue  // 0x14
  private           System.Double                   m_DoubleValue  // 0x14
PROPERTIES:
  valuePtr  get=0x04DABDD0
  type  get=0x02B77620
  isEmpty  get=0x0426F6A0
METHODS:
  RVA=0x04DABD30  token=0x60015E8  System.Void .ctor(System.Boolean value)
  RVA=0x04DABC90  token=0x60015E9  System.Void .ctor(System.Char value)
  RVA=0x04DABD60  token=0x60015EA  System.Void .ctor(System.Byte value)
  RVA=0x04DABC80  token=0x60015EB  System.Void .ctor(System.SByte value)
  RVA=0x04DABD10  token=0x60015EC  System.Void .ctor(System.Int16 value)
  RVA=0x04DABDB0  token=0x60015ED  System.Void .ctor(System.UInt16 value)
  RVA=0x04DABD90  token=0x60015EE  System.Void .ctor(System.Int32 value)
  RVA=0x04DABD70  token=0x60015EF  System.Void .ctor(System.UInt32 value)
  RVA=0x04DABCF0  token=0x60015F0  System.Void .ctor(System.Int64 value)
  RVA=0x04DABCD0  token=0x60015F1  System.Void .ctor(System.UInt64 value)
  RVA=0x04DABCB0  token=0x60015F2  System.Void .ctor(System.Single value)
  RVA=0x04DABD40  token=0x60015F3  System.Void .ctor(System.Double value)
  RVA=0x0B1C54E4  token=0x60015F4  UnityEngine.InputSystem.Utilities.PrimitiveValue ConvertTo(System.TypeCode type)
  RVA=0x0B1C5760  token=0x60015F5  System.Boolean Equals(UnityEngine.InputSystem.Utilities.PrimitiveValue other)
  RVA=0x0B1C578C  token=0x60015F6  System.Boolean Equals(System.Object obj)
  RVA=0x0B1C69C4  token=0x60015F7  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.PrimitiveValue left, UnityEngine.InputSystem.Utilities.PrimitiveValue right)
  RVA=0x0B1C69E4  token=0x60015F8  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.PrimitiveValue left, UnityEngine.InputSystem.Utilities.PrimitiveValue right)
  RVA=0x0B1C60FC  token=0x60015F9  System.Int32 GetHashCode()
  RVA=0x0B1C6568  token=0x60015FA  System.String ToString()
  RVA=0x0B1C5DBC  token=0x60015FB  UnityEngine.InputSystem.Utilities.PrimitiveValue FromString(System.String value)
  RVA=0x02B77620  token=0x60015FC  System.TypeCode GetTypeCode()
  RVA=0x0B1C611C  token=0x60015FD  System.Boolean ToBoolean(System.IFormatProvider provider)
  RVA=0x0B1C6204  token=0x60015FE  System.Byte ToByte(System.IFormatProvider provider)
  RVA=0x0B1C620C  token=0x60015FF  System.Char ToChar(System.IFormatProvider provider)
  RVA=0x0B1C6238  token=0x6001600  System.DateTime ToDateTime(System.IFormatProvider provider)
  RVA=0x0B1C6294  token=0x6001601  System.Decimal ToDecimal(System.IFormatProvider provider)
  RVA=0x044B04E0  token=0x6001602  System.Double ToDouble(System.IFormatProvider provider)
  RVA=0x0B1C6204  token=0x6001603  System.Int16 ToInt16(System.IFormatProvider provider)
  RVA=0x0B1C6204  token=0x6001604  System.Int32 ToInt32(System.IFormatProvider provider)
  RVA=0x0B1C62D0  token=0x6001605  System.Int64 ToInt64(System.IFormatProvider provider)
  RVA=0x0B1C6204  token=0x6001606  System.SByte ToSByte(System.IFormatProvider provider)
  RVA=0x05397374  token=0x6001607  System.Single ToSingle(System.IFormatProvider provider)
  RVA=0x0B1C68D4  token=0x6001608  System.String ToString(System.IFormatProvider provider)
  RVA=0x0B1C68DC  token=0x6001609  System.Object ToType(System.Type conversionType, System.IFormatProvider provider)
  RVA=0x0B1C6928  token=0x600160A  System.UInt16 ToUInt16(System.IFormatProvider provider)
  RVA=0x0B1C6928  token=0x600160B  System.UInt32 ToUInt32(System.IFormatProvider provider)
  RVA=0x0B1C6934  token=0x600160C  System.UInt64 ToUInt64(System.IFormatProvider provider)
  RVA=0x0B1C6350  token=0x600160D  System.Object ToObject()
  RVA=-1  // generic def  token=0x600160E  UnityEngine.InputSystem.Utilities.PrimitiveValue From(TValue value)
  RVA=0x0B1C593C  token=0x600160F  UnityEngine.InputSystem.Utilities.PrimitiveValue FromObject(System.Object value)
  RVA=0x04DABB00  token=0x6001610  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Boolean value)
  RVA=0x04DABB40  token=0x6001611  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Char value)
  RVA=0x04DABB20  token=0x6001612  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Byte value)
  RVA=0x04DABBE0  token=0x6001613  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.SByte value)
  RVA=0x04DABB80  token=0x6001614  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Int16 value)
  RVA=0x04DABC20  token=0x6001615  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.UInt16 value)
  RVA=0x04DABBA0  token=0x6001616  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Int32 value)
  RVA=0x04DABC40  token=0x6001617  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.UInt32 value)
  RVA=0x04DABBC0  token=0x6001618  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Int64 value)
  RVA=0x04DABC60  token=0x6001619  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.UInt64 value)
  RVA=0x04DABC00  token=0x600161A  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Single value)
  RVA=0x04DABB60  token=0x600161B  UnityEngine.InputSystem.Utilities.PrimitiveValue op_Implicit(System.Double value)
  RVA=0x04DABB00  token=0x600161C  UnityEngine.InputSystem.Utilities.PrimitiveValue FromBoolean(System.Boolean value)
  RVA=0x04DABB40  token=0x600161D  UnityEngine.InputSystem.Utilities.PrimitiveValue FromChar(System.Char value)
  RVA=0x04DABB20  token=0x600161E  UnityEngine.InputSystem.Utilities.PrimitiveValue FromByte(System.Byte value)
  RVA=0x04DABBE0  token=0x600161F  UnityEngine.InputSystem.Utilities.PrimitiveValue FromSByte(System.SByte value)
  RVA=0x04DABB80  token=0x6001620  UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt16(System.Int16 value)
  RVA=0x04DABC20  token=0x6001621  UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt16(System.UInt16 value)
  RVA=0x04DABBA0  token=0x6001622  UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt32(System.Int32 value)
  RVA=0x04DABC40  token=0x6001623  UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt32(System.UInt32 value)
  RVA=0x04DABBC0  token=0x6001624  UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt64(System.Int64 value)
  RVA=0x04DABC60  token=0x6001625  UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt64(System.UInt64 value)
  RVA=0x04DABC00  token=0x6001626  UnityEngine.InputSystem.Utilities.PrimitiveValue FromSingle(System.Single value)
  RVA=0x04DABB60  token=0x6001627  UnityEngine.InputSystem.Utilities.PrimitiveValue FromDouble(System.Double value)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ReadOnlyArray`1
TYPE:  sealed struct
TOKEN: 0x2000260
IMPLEMENTS: System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private           TValue[]                        m_Array  // 0x0
  private           System.Int32                    m_StartIndex  // 0x0
  private           System.Int32                    m_Length  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001628  System.Void .ctor(TValue[] array)
  RVA=-1  // not resolved  token=0x6001629  System.Void .ctor(TValue[] array, System.Int32 index, System.Int32 length)
  RVA=-1  // not resolved  token=0x600162A  TValue[] ToArray()
  RVA=-1  // not resolved  token=0x600162B  System.Int32 IndexOf(System.Predicate<TValue> predicate)
  RVA=-1  // not resolved  token=0x600162C  UnityEngine.InputSystem.Utilities.ReadOnlyArray.Enumerator<TValue> GetEnumerator()
  RVA=-1  // not resolved  token=0x600162D  System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600162E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x600162F  UnityEngine.InputSystem.Utilities.ReadOnlyArray<TValue> op_Implicit(TValue[] array)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions
TYPE:  static class
TOKEN: 0x2000262
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6001638  System.Boolean Contains(UnityEngine.InputSystem.Utilities.ReadOnlyArray<TValue> array, TValue value)
  RVA=-1  // generic def  token=0x6001639  System.Boolean ContainsReference(UnityEngine.InputSystem.Utilities.ReadOnlyArray<TValue> array, TValue value)
  RVA=-1  // generic def  token=0x600163A  System.Int32 IndexOfReference(UnityEngine.InputSystem.Utilities.ReadOnlyArray<TValue> array, TValue value)
  RVA=-1  // generic def  token=0x600163B  System.Boolean HaveEqualReferences(UnityEngine.InputSystem.Utilities.ReadOnlyArray<TValue> array1, System.Collections.Generic.IReadOnlyList<TValue> array2, System.Int32 count)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.ISavedState
TYPE:  interface
TOKEN: 0x2000263
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600163C  System.Void StaticDisposeCurrentState()
  RVA=-1  // abstract  token=0x600163D  System.Void RestoreSavedState()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.SavedStructState`1
TYPE:  sealed class
TOKEN: 0x2000264
IMPLEMENTS: UnityEngine.InputSystem.Utilities.ISavedState
FIELDS:
  private           T                               m_State  // 0x0
  private           UnityEngine.InputSystem.Utilities.SavedStructState.TypedRestore<T>m_RestoreAction  // 0x0
  private           System.Action                   m_StaticDisposeCurrentState  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600163E  System.Void .ctor(T& state, UnityEngine.InputSystem.Utilities.SavedStructState.TypedRestore<T> restoreAction, System.Action staticDisposeCurrentState)
  RVA=-1  // not resolved  token=0x600163F  System.Void StaticDisposeCurrentState()
  RVA=-1  // not resolved  token=0x6001640  System.Void RestoreSavedState()
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.SpriteUtilities
TYPE:  static class
TOKEN: 0x2000266
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1C6A04  token=0x6001645  UnityEngine.Sprite CreateCircleSprite(System.Int32 radius, UnityEngine.Color32 colour)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.StringHelpers
TYPE:  static class
TOKEN: 0x2000267
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1C6C94  token=0x6001646  System.String Escape(System.String str, System.String chars, System.String replacements)
  RVA=0x0B1C7718  token=0x6001647  System.String Unescape(System.String str, System.String chars, System.String replacements)
  RVA=0x0B1C6C14  token=0x6001648  System.Boolean Contains(System.String str, System.Char ch)
  RVA=0x0B1C6BF4  token=0x6001649  System.Boolean Contains(System.String str, System.String text, System.StringComparison comparison)
  RVA=0x0B1C7100  token=0x600164A  System.String GetPlural(System.String str)
  RVA=0x0B1C7308  token=0x600164B  System.String NicifyMemorySize(System.Int64 numBytes)
  RVA=0x0B1C6F94  token=0x600164C  System.Boolean FromNicifiedMemorySize(System.String text, System.Int64& result, System.Int64 defaultMultiplier)
  RVA=0x0B1C6C34  token=0x600164D  System.Int32 CountOccurrences(System.String str, System.Char ch)
  RVA=0x0B1C76A8  token=0x600164E  System.Collections.Generic.IEnumerable<UnityEngine.InputSystem.Utilities.Substring> Tokenize(System.String str)
  RVA=0x0B1C7620  token=0x600164F  System.Collections.Generic.IEnumerable<System.String> Split(System.String str, System.Func<System.Char,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6001650  System.String Join(System.String separator, TValue[] values)
  RVA=-1  // generic def  token=0x6001651  System.String Join(System.Collections.Generic.IEnumerable<TValue> values, System.String separator)
  RVA=-1  // generic def  token=0x6001652  System.String MakeUniqueName(System.String baseName, System.Collections.Generic.IEnumerable<TExisting> existingSet, System.Func<TExisting,System.String> getNameFunc)
  RVA=0x03AEEF90  token=0x6001653  System.Boolean CharacterSeparatedListsHaveAtLeastOneCommonElement(System.String firstList, System.String secondList, System.Char separator)
  RVA=0x0B1C747C  token=0x6001654  System.Int32 ParseInt(System.String str, System.Int32 pos)
  RVA=0x0B1C7AC0  token=0x6001655  System.Boolean WriteStringToBuffer(System.String text, System.IntPtr buffer, System.Int32 bufferSizeInCharacters)
  RVA=0x0B1C7920  token=0x6001656  System.Boolean WriteStringToBuffer(System.String text, System.IntPtr buffer, System.Int32 bufferSizeInCharacters, System.UInt32& offset)
  RVA=0x0B1C7600  token=0x6001657  System.String ReadStringFromBuffer(System.IntPtr buffer, System.Int32 bufferSize)
  RVA=0x0B1C7504  token=0x6001658  System.String ReadStringFromBuffer(System.IntPtr buffer, System.Int32 bufferSize, System.UInt32& offset)
  RVA=0x0B1C72A4  token=0x6001659  System.Boolean IsPrintable(System.Char ch)
  RVA=0x0B1C7854  token=0x600165A  System.String WithAllWhitespaceStripped(System.String str)
  RVA=0x0B1C727C  token=0x600165B  System.Boolean InvariantEqualsIgnoreCase(System.String left, System.String right)
  RVA=0x0B1C6DD4  token=0x600165C  System.String ExpandTemplateString(System.String template, System.Func<System.String,System.String> mapFunc)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.Substring
TYPE:  sealed struct
TOKEN: 0x200026A
SIZE:  0x20
IMPLEMENTS: System.IComparable`1 System.IEquatable`1
FIELDS:
  private   readonly System.String                   m_String  // 0x10
  private   readonly System.Int32                    m_Index  // 0x18
  private   readonly System.Int32                    m_Length  // 0x1c
PROPERTIES:
  isEmpty  get=0x0B1C7F5C
  length  get=0x04D8CAD0
  index  get=0x04D88B10
  Item  get=0x0B1C7EDC
METHODS:
  RVA=0x0B1C7E70  token=0x600166E  System.Void .ctor(System.String str)
  RVA=0x05390898  token=0x600166F  System.Void .ctor(System.String str, System.Int32 index, System.Int32 length)
  RVA=0x0B1C7EA8  token=0x6001670  System.Void .ctor(System.String str, System.Int32 index)
  RVA=0x0B1C7BC4  token=0x6001671  System.Boolean Equals(System.Object obj)
  RVA=0x0B1C7C80  token=0x6001672  System.Boolean Equals(System.String other)
  RVA=0x0B1C7C5C  token=0x6001673  System.Boolean Equals(UnityEngine.InputSystem.Utilities.Substring other)
  RVA=0x0B1C7B54  token=0x6001674  System.Boolean Equals(UnityEngine.InputSystem.Utilities.InternedString other)
  RVA=0x0B1C7AE0  token=0x6001675  System.Int32 CompareTo(UnityEngine.InputSystem.Utilities.Substring other)
  RVA=0x0B1C7B10  token=0x6001676  System.Int32 Compare(UnityEngine.InputSystem.Utilities.Substring left, UnityEngine.InputSystem.Utilities.Substring right, System.StringComparison comparison)
  RVA=0x0B1C7D60  token=0x6001677  System.Boolean StartsWith(System.String str)
  RVA=0x0B1C7DE0  token=0x6001678  System.String Substr(System.Int32 index, System.Int32 length)
  RVA=0x0B1C7E14  token=0x6001679  System.String ToString()
  RVA=0x0B1C7D10  token=0x600167A  System.Int32 GetHashCode()
  RVA=0x0B1C7F64  token=0x600167B  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.Substring a, UnityEngine.InputSystem.Utilities.Substring b)
  RVA=0x0B1C7FE8  token=0x600167C  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.Substring a, UnityEngine.InputSystem.Utilities.Substring b)
  RVA=0x0B1C7FA8  token=0x600167D  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.Substring a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x0B1C8008  token=0x600167E  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.Substring a, UnityEngine.InputSystem.Utilities.InternedString b)
  RVA=0x0B1C7F84  token=0x600167F  System.Boolean op_Equality(UnityEngine.InputSystem.Utilities.InternedString a, UnityEngine.InputSystem.Utilities.Substring b)
  RVA=0x0B1C8028  token=0x6001680  System.Boolean op_Inequality(UnityEngine.InputSystem.Utilities.InternedString a, UnityEngine.InputSystem.Utilities.Substring b)
  RVA=0x0B1C7FC8  token=0x6001681  UnityEngine.InputSystem.Utilities.Substring op_Implicit(System.String s)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.TypeHelpers
TYPE:  static class
TOKEN: 0x200026B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6001685  TObject As(System.Object obj)
  RVA=0x0B1C8A10  token=0x6001686  System.Boolean IsInt(System.TypeCode type)
  RVA=0x0B1C8924  token=0x6001687  System.Type GetValueType(System.Reflection.MemberInfo member)
  RVA=0x0B1C85C0  token=0x6001688  System.String GetNiceTypeName(System.Type type)
  RVA=0x0B1C828C  token=0x6001689  System.Type GetGenericTypeArgumentFromHierarchy(System.Type type, System.Type genericTypeDefinition, System.Int32 argumentIndex)
END_CLASS

CLASS: UnityEngine.InputSystem.Utilities.TypeTable
TYPE:  sealed struct
TOKEN: 0x200026C
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<UnityEngine.InputSystem.Utilities.InternedString,System.Type>table  // 0x10
PROPERTIES:
  names  get=0x0B1C8C88
  internedNames  get=0x0B1C8C44
METHODS:
  RVA=0x03DF6720  token=0x600168C  System.Void Initialize()
  RVA=0x0B1C8A40  token=0x600168D  UnityEngine.InputSystem.Utilities.InternedString FindNameForType(System.Type type)
  RVA=0x03DF6650  token=0x600168E  System.Void AddTypeRegistration(System.String name, System.Type type)
  RVA=0x03DFDE40  token=0x600168F  System.Type LookupTypeRegistration(System.String name)
END_CLASS

CLASS: UnityEngine.InputSystem.XInput.IXboxOneRumble
TYPE:  interface
TOKEN: 0x20000FC
IMPLEMENTS: UnityEngine.InputSystem.Haptics.IDualMotorRumble UnityEngine.InputSystem.Haptics.IHaptics
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000CA2  System.Void SetMotorSpeeds(System.Single lowFrequency, System.Single highFrequency, System.Single leftTrigger, System.Single rightTrigger)
END_CLASS

CLASS: UnityEngine.InputSystem.XInput.XInputController
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x230
EXTENDS: UnityEngine.InputSystem.Gamepad
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<menu>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.ButtonControl<view>k__BackingField  // 0x218
  private           System.Boolean                  m_HaveParsedCapabilities  // 0x220
  private           UnityEngine.InputSystem.XInput.XInputController.DeviceSubTypem_SubType  // 0x224
  private           UnityEngine.InputSystem.XInput.XInputController.DeviceFlagsm_Flags  // 0x228
PROPERTIES:
  menu  get=0x04D8D8B0  set=0x06B8B238
  view  get=0x04D8D3D0  set=0x06B1D640
  subType  get=0x0B21E57C
  flags  get=0x0B21E54C
METHODS:
  RVA=0x0B21E470  token=0x6000CA9  System.Void FinishSetup()
  RVA=0x0B21E4BC  token=0x6000CAA  System.Void ParseCapabilities()
  RVA=0x053971D4  token=0x6000CAB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XInput.XInputControllerWindows
TYPE:  class
TOKEN: 0x2000102
SIZE:  0x230
EXTENDS: UnityEngine.InputSystem.XInput.XInputController
FIELDS:
METHODS:
  RVA=0x053971D4  token=0x6000CAC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XInput.XInputSupport
TYPE:  static class
TOKEN: 0x2000103
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03DF7120  token=0x6000CAD  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState
TYPE:  sealed struct
TOKEN: 0x2000104
SIZE:  0x1C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  public            System.UInt16                   buttons  // 0x10
  public            System.Byte                     leftTrigger  // 0x12
  public            System.Byte                     rightTrigger  // 0x13
  public            System.Int16                    leftStickX  // 0x14
  public            System.Int16                    leftStickY  // 0x16
  public            System.Int16                    rightStickX  // 0x18
  public            System.Int16                    rightStickY  // 0x1a
PROPERTIES:
  format  get=0x04DAC1B0
METHODS:
  RVA=0x04DAC180  token=0x6000CAF  UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState WithButton(UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.Button button)
END_CLASS

CLASS: UnityEngine.InputSystem.XR.PoseState
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x4C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
FIELDS:
  private   static  System.Int32                    kSizeInBytes  // const
  private   static readonly UnityEngine.InputSystem.Utilities.FourCCs_Format  // static @ 0x0
  public            System.Boolean                  isTracked  // 0x10
  public            UnityEngine.XR.InputTrackingStatetrackingState  // 0x14
  public            UnityEngine.Vector3             position  // 0x18
  public            UnityEngine.Quaternion          rotation  // 0x24
  public            UnityEngine.Vector3             velocity  // 0x34
  public            UnityEngine.Vector3             angularVelocity  // 0x40
PROPERTIES:
  format  get=0x0B219D64
METHODS:
  RVA=0x04DAC0B0  token=0x6000BEB  System.Void .ctor(System.Boolean isTracked, UnityEngine.XR.InputTrackingState trackingState, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 velocity, UnityEngine.Vector3 angularVelocity)
  RVA=0x0B219D28  token=0x6000BEC  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.PoseControl
TYPE:  class
TOKEN: 0x20000E0
SIZE:  0x1A8
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<isTracked>k__BackingField  // 0x178
  private           UnityEngine.InputSystem.Controls.IntegerControl<trackingState>k__BackingField  // 0x180
  private           UnityEngine.InputSystem.Controls.Vector3Control<position>k__BackingField  // 0x188
  private           UnityEngine.InputSystem.Controls.QuaternionControl<rotation>k__BackingField  // 0x190
  private           UnityEngine.InputSystem.Controls.Vector3Control<velocity>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.Vector3Control<angularVelocity>k__BackingField  // 0x1a0
PROPERTIES:
  isTracked  get=0x04D8DEE0  set=0x06B8B92C
  trackingState  get=0x04D8DD60  set=0x06B8B774
  position  get=0x04D8DA20  set=0x06B8B3B4
  rotation  get=0x04D8DD50  set=0x0539722C
  velocity  get=0x04D8DED0  set=0x05397218
  angularVelocity  get=0x04D8DA10  set=0x05397204
METHODS:
  RVA=0x0B219CA4  token=0x6000BF9  System.Void .ctor()
  RVA=0x0B219728  token=0x6000BFA  System.Void FinishSetup()
  RVA=0x0B2198D4  token=0x6000BFB  UnityEngine.InputSystem.XR.PoseState ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B219ACC  token=0x6000BFC  System.Void WriteValueIntoState(UnityEngine.InputSystem.XR.PoseState value, System.Void* statePtr)
  RVA=0x0B2194FC  token=0x6000BFD  UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRHMD
TYPE:  class
TOKEN: 0x20000E1
SIZE:  0x1E0
EXTENDS: UnityEngine.InputSystem.TrackedDevice
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyePosition>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.QuaternionControl<leftEyeRotation>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyePosition>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.QuaternionControl<rightEyeRotation>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.Vector3Control<centerEyePosition>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.QuaternionControl<centerEyeRotation>k__BackingField  // 0x1d8
PROPERTIES:
  leftEyePosition  get=0x04D8D960  set=0x053971DC
  leftEyeRotation  get=0x04D8DA90  set=0x05397240
  rightEyePosition  get=0x04D8DA80  set=0x06B8B42C
  rightEyeRotation  get=0x04D8D790  set=0x06B8B0D0
  centerEyePosition  get=0x04D8D7B0  set=0x06B8B0F8
  centerEyeRotation  get=0x04D87870  set=0x06B8B134
METHODS:
  RVA=0x0B21E86C  token=0x6000C0A  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000C0B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRController
TYPE:  class
TOKEN: 0x20000E2
SIZE:  0x1B0
EXTENDS: UnityEngine.InputSystem.TrackedDevice
FIELDS:
PROPERTIES:
  leftHand  get=0x0B21E728
  rightHand  get=0x0B21E7AC
METHODS:
  RVA=0x0B21E640  token=0x6000C0E  System.Void FinishSetup()
  RVA=0x053971D4  token=0x6000C0F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRControllerWithRumble
TYPE:  class
TOKEN: 0x20000E3
SIZE:  0x1B0
EXTENDS: UnityEngine.InputSystem.XR.XRController
FIELDS:
METHODS:
  RVA=0x0B21E5AC  token=0x6000C10  System.Void SendImpulse(System.Single amplitude, System.Single duration)
  RVA=0x053971D4  token=0x6000C11  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.TrackedPoseDriver
TYPE:  class
TOKEN: 0x20000E4
SIZE:  0xA8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingTypem_TrackingType  // 0x18
  private           UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateTypem_UpdateType  // 0x1c
  private           System.Boolean                  m_IgnoreTrackingState  // 0x20
  private           UnityEngine.InputSystem.InputActionPropertym_PositionInput  // 0x28
  private           UnityEngine.InputSystem.InputActionPropertym_RotationInput  // 0x40
  private           UnityEngine.InputSystem.InputActionPropertym_TrackingStateInput  // 0x58
  private           UnityEngine.Vector3             m_CurrentPosition  // 0x70
  private           UnityEngine.Quaternion          m_CurrentRotation  // 0x7c
  private           UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStatesm_CurrentTrackingState  // 0x8c
  private           System.Boolean                  m_RotationBound  // 0x90
  private           System.Boolean                  m_PositionBound  // 0x91
  private           System.Boolean                  m_TrackingStateBound  // 0x92
  private           System.Boolean                  m_IsFirstUpdate  // 0x93
  private           UnityEngine.InputSystem.InputActionm_PositionAction  // 0x98
  private           UnityEngine.InputSystem.InputActionm_RotationAction  // 0xa0
PROPERTIES:
  trackingType  get=0x011EF5B0  set=0x011EF9B0
  updateType  get=0x04D86310  set=0x04D86320
  ignoreTrackingState  get=0x04D866B0  set=0x04D866D0
  positionInput  get=0x04D9CD10  set=0x0B21B578
  rotationInput  get=0x04D88410  set=0x0B21B63C
  trackingStateInput  get=0x04DAC160  set=0x0B21B6AC
  positionAction  get=0x0B21B50C  set=0x0B21B524
  rotationAction  get=0x0B21B518  set=0x0B21B5E8
METHODS:
  RVA=0x0B21A194  token=0x6000C1E  System.Void BindActions()
  RVA=0x0B21AE98  token=0x6000C1F  System.Void UnbindActions()
  RVA=0x0B21A1C0  token=0x6000C20  System.Void BindPosition()
  RVA=0x0B21A33C  token=0x6000C21  System.Void BindRotation()
  RVA=0x0B21A4B8  token=0x6000C22  System.Void BindTrackingState()
  RVA=0x0B21AB0C  token=0x6000C23  System.Void RenameAndEnable(UnityEngine.InputSystem.InputAction action, System.String name)
  RVA=0x0B21AEC4  token=0x6000C24  System.Void UnbindPosition()
  RVA=0x0B21AFFC  token=0x6000C25  System.Void UnbindRotation()
  RVA=0x0B21B134  token=0x6000C26  System.Void UnbindTrackingState()
  RVA=0x0B21A8D0  token=0x6000C27  System.Void OnPositionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21A8BC  token=0x6000C28  System.Void OnPositionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21A950  token=0x6000C29  System.Void OnRotationPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21A930  token=0x6000C2A  System.Void OnRotationCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21A9A4  token=0x6000C2B  System.Void OnTrackingStatePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x04DAC150  token=0x6000C2C  System.Void OnTrackingStateCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x0B21AB38  token=0x6000C2D  System.Void Reset()
  RVA=0x0B21A160  token=0x6000C2E  System.Void Awake()
  RVA=0x0B21A818  token=0x6000C2F  System.Void OnEnable()
  RVA=0x0B21A77C  token=0x6000C30  System.Void OnDisable()
  RVA=0x0B21A748  token=0x6000C31  System.Void OnDestroy()
  RVA=0x0B21B32C  token=0x6000C32  System.Void UpdateCallback()
  RVA=0x0B21AA3C  token=0x6000C33  System.Void ReadTrackingState(System.Boolean hasResolvedPositionInputControl, System.Boolean hasResolvedRotationInputControl)
  RVA=0x0B21A9F0  token=0x6000C34  System.Void OnUpdate()
  RVA=0x0B21A734  token=0x6000C35  System.Void OnBeforeRender()
  RVA=0x0B21AA04  token=0x6000C36  System.Void PerformUpdate()
  RVA=0x0B21AD54  token=0x6000C37  System.Void SetLocalTransform(UnityEngine.Vector3 newPosition, UnityEngine.Quaternion newRotation)
  RVA=0x0B21A6D0  token=0x6000C38  System.Boolean HasStereoCamera(UnityEngine.Camera& cameraComponent)
  RVA=0x0B21A634  token=0x6000C39  System.Boolean HasResolvedControl(UnityEngine.InputSystem.InputAction action)
  RVA=0x041E1670  token=0x6000C3E  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0B21B26C  token=0x6000C3F  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0B21B4B4  token=0x6000C40  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRLayoutBuilder
TYPE:  class
TOKEN: 0x20000E8
SIZE:  0x28
FIELDS:
  private           System.String                   parentLayout  // 0x10
  private           System.String                   interfaceName  // 0x18
  private           UnityEngine.InputSystem.XR.XRDeviceDescriptordescriptor  // 0x20
  private   static readonly System.String[]                 poseSubControlNames  // static @ 0x0
  private   static readonly UnityEngine.InputSystem.XR.FeatureType[]poseSubControlTypes  // static @ 0x8
METHODS:
  RVA=0x0B21F8DC  token=0x6000C41  System.UInt32 GetSizeOfFeature(UnityEngine.InputSystem.XR.XRFeatureDescriptor featureDescriptor)
  RVA=0x0B21FACC  token=0x6000C42  System.String SanitizeString(System.String original, System.Boolean allowPaths)
  RVA=0x03AE95B0  token=0x6000C43  System.String OnFindLayoutForDevice(UnityEngine.InputSystem.Layouts.InputDeviceDescription& description, System.String matchedLayout, UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeCommandDelegate)
  RVA=0x0B21F72C  token=0x6000C44  System.String ConvertPotentialAliasToName(UnityEngine.InputSystem.Layouts.InputControlLayout layout, System.String nameOrAlias)
  RVA=0x0B21FAA4  token=0x6000C45  System.Boolean IsSubControl(System.String name)
  RVA=0x0B21F8A0  token=0x6000C46  System.String GetParentControlName(System.String name)
  RVA=0x0B21F970  token=0x6000C47  System.Boolean IsPoseControl(System.Collections.Generic.List<UnityEngine.InputSystem.XR.XRFeatureDescriptor> features, System.Int32 startIndex)
  RVA=0x0B21E9EC  token=0x6000C48  UnityEngine.InputSystem.Layouts.InputControlLayout Build()
  RVA=0x041E1670  token=0x6000C49  System.Void .ctor()
  RVA=0x0B21FC00  token=0x6000C4A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRUtilities
TYPE:  static class
TOKEN: 0x20000EA
SIZE:  0x10
FIELDS:
  public    static  System.String                   InterfaceMatchAnyVersion  // const
  public    static  System.String                   InterfaceV1  // const
  public    static  System.String                   InterfaceCurrent  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.FeatureType
TYPE:  sealed struct
TOKEN: 0x20000EB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.XR.FeatureTypeCustom  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeBinary  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeDiscreteStates  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeAxis1D  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeAxis2D  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeAxis3D  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeRotation  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeHand  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeBone  // const
  public    static  UnityEngine.InputSystem.XR.FeatureTypeEyes  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.UsageHint
TYPE:  sealed struct
TOKEN: 0x20000EC
SIZE:  0x18
FIELDS:
  public            System.String                   content  // 0x10
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRFeatureDescriptor
TYPE:  sealed struct
TOKEN: 0x20000ED
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Collections.Generic.List<UnityEngine.InputSystem.XR.UsageHint>usageHints  // 0x18
  public            UnityEngine.InputSystem.XR.FeatureTypefeatureType  // 0x20
  public            System.UInt32                   customSize  // 0x24
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRDeviceDescriptor
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x38
FIELDS:
  public            System.String                   deviceName  // 0x10
  public            System.String                   manufacturer  // 0x18
  public            System.String                   serialNumber  // 0x20
  public            UnityEngine.XR.InputDeviceCharacteristicscharacteristics  // 0x28
  public            System.Int32                    deviceId  // 0x2c
  public            System.Collections.Generic.List<UnityEngine.InputSystem.XR.XRFeatureDescriptor>inputFeatures  // 0x30
METHODS:
  RVA=0x05393424  token=0x6000C4D  System.String ToJson()
  RVA=0x0B21E830  token=0x6000C4E  UnityEngine.InputSystem.XR.XRDeviceDescriptor FromJson(System.String json)
  RVA=0x041E1670  token=0x6000C4F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Bone
TYPE:  sealed struct
TOKEN: 0x20000EF
SIZE:  0x30
FIELDS:
  public            System.UInt32                   m_ParentBoneIndex  // 0x10
  public            UnityEngine.Vector3             m_Position  // 0x14
  public            UnityEngine.Quaternion          m_Rotation  // 0x20
PROPERTIES:
  parentBoneIndex  get=0x02B77620  set=0x04D86140
  position  get=0x04D8C7B0  set=0x04DABF50
  rotation  get=0x02B2D510  set=0x04D8C980
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Eyes
TYPE:  sealed struct
TOKEN: 0x20000F0
SIZE:  0x5C
FIELDS:
  public            UnityEngine.Vector3             m_LeftEyePosition  // 0x10
  public            UnityEngine.Quaternion          m_LeftEyeRotation  // 0x1c
  public            UnityEngine.Vector3             m_RightEyePosition  // 0x2c
  public            UnityEngine.Quaternion          m_RightEyeRotation  // 0x38
  public            UnityEngine.Vector3             m_FixationPoint  // 0x48
  public            System.Single                   m_LeftEyeOpenAmount  // 0x54
  public            System.Single                   m_RightEyeOpenAmount  // 0x58
PROPERTIES:
  leftEyePosition  get=0x04D93250  set=0x04DABF80
  leftEyeRotation  get=0x04DABF70  set=0x04DABF90
  rightEyePosition  get=0x04D90520  set=0x04D90500
  rightEyeRotation  get=0x04D8FAD0  set=0x04D8FAE0
  fixationPoint  get=0x04D9E700  set=0x04D9DB00
  leftEyeOpenAmount  get=0x04D86400  set=0x04D86480
  rightEyeOpenAmount  get=0x04D8D350  set=0x04D8D380
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.XR.BoneControl
TYPE:  class
TOKEN: 0x20000F1
SIZE:  0x158
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.IntegerControl<parentBoneIndex>k__BackingField  // 0x140
  private           UnityEngine.InputSystem.Controls.Vector3Control<position>k__BackingField  // 0x148
  private           UnityEngine.InputSystem.Controls.QuaternionControl<rotation>k__BackingField  // 0x150
PROPERTIES:
  parentBoneIndex  get=0x04D8DA60  set=0x06B8B404
  position  get=0x04D8DAC0  set=0x06B8B468
  rotation  get=0x04D8D890  set=0x06B8B224
METHODS:
  RVA=0x0B2086D4  token=0x6000C6A  System.Void FinishSetup()
  RVA=0x0B2087D0  token=0x6000C6B  UnityEngine.InputSystem.XR.Bone ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B2088A8  token=0x6000C6C  System.Void WriteValueIntoState(UnityEngine.InputSystem.XR.Bone value, System.Void* statePtr)
  RVA=0x0B20893C  token=0x6000C6D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.EyesControl
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x1D0
EXTENDS: UnityEngine.InputSystem.InputControl`1
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<leftEyePosition>k__BackingField  // 0x198
  private           UnityEngine.InputSystem.Controls.QuaternionControl<leftEyeRotation>k__BackingField  // 0x1a0
  private           UnityEngine.InputSystem.Controls.Vector3Control<rightEyePosition>k__BackingField  // 0x1a8
  private           UnityEngine.InputSystem.Controls.QuaternionControl<rightEyeRotation>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.Vector3Control<fixationPoint>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.AxisControl<leftEyeOpenAmount>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.AxisControl<rightEyeOpenAmount>k__BackingField  // 0x1c8
PROPERTIES:
  leftEyePosition  get=0x04D8DED0  set=0x05397218
  leftEyeRotation  get=0x04D8DA10  set=0x05397204
  rightEyePosition  get=0x04D8D9C0  set=0x053971F0
  rightEyeRotation  get=0x04D8D960  set=0x053971DC
  fixationPoint  get=0x04D8DA90  set=0x05397240
  leftEyeOpenAmount  get=0x04D8DA80  set=0x06B8B42C
  rightEyeOpenAmount  get=0x04D8D790  set=0x06B8B0D0
METHODS:
  RVA=0x0B209684  token=0x6000C7C  System.Void FinishSetup()
  RVA=0x0B209858  token=0x6000C7D  UnityEngine.InputSystem.XR.Eyes ReadUnprocessedValueFromState(System.Void* statePtr)
  RVA=0x0B2099D4  token=0x6000C7E  System.Void WriteValueIntoState(UnityEngine.InputSystem.XR.Eyes value, System.Void* statePtr)
  RVA=0x0B209B0C  token=0x6000C7F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.XRSupport
TYPE:  static class
TOKEN: 0x20000F3
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03AF28A0  token=0x6000C80  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.BufferedRumble
TYPE:  sealed struct
TOKEN: 0x20000F4
SIZE:  0x30
FIELDS:
  private           UnityEngine.InputSystem.XR.Haptics.HapticCapabilities<capabilities>k__BackingField  // 0x10
  private           UnityEngine.InputSystem.InputDevice<device>k__BackingField  // 0x28
PROPERTIES:
  capabilities  get=0x04D910A0  set=0x04D88C00
  device  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0B208A54  token=0x6000C85  System.Void .ctor(UnityEngine.InputSystem.InputDevice device)
  RVA=0x0B208970  token=0x6000C86  System.Void EnqueueRumble(System.Byte[] samples)
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.HapticState
TYPE:  sealed struct
TOKEN: 0x20000F5
SIZE:  0x18
FIELDS:
  private           System.UInt32                   <samplesQueued>k__BackingField  // 0x10
  private           System.UInt32                   <samplesAvailable>k__BackingField  // 0x14
PROPERTIES:
  samplesQueued  get=0x02B77620  set=0x04D86140
  samplesAvailable  get=0x04D88B20  set=0x04D88B50
METHODS:
  RVA=0x04D85C40  token=0x6000C87  System.Void .ctor(System.UInt32 samplesQueued, System.UInt32 samplesAvailable)
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.GetCurrentHapticStateCommand
TYPE:  sealed struct
TOKEN: 0x20000F6
SIZE:  0x20
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  private           UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.UInt32                   samplesQueued  // 0x18
  public            System.UInt32                   samplesAvailable  // 0x1c
PROPERTIES:
  Type  get=0x04DABFD0
  typeStatic  get=0x04DABFD0
  currentState  get=0x04DABFE0
METHODS:
  RVA=0x04DABFA0  token=0x6000C8F  UnityEngine.InputSystem.XR.Haptics.GetCurrentHapticStateCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.HapticCapabilities
TYPE:  sealed struct
TOKEN: 0x20000F7
SIZE:  0x24
FIELDS:
  private   readonly System.UInt32                   <numChannels>k__BackingField  // 0x10
  private   readonly System.Boolean                  <supportsImpulse>k__BackingField  // 0x14
  private   readonly System.Boolean                  <supportsBuffer>k__BackingField  // 0x15
  private   readonly System.UInt32                   <frequencyHz>k__BackingField  // 0x18
  private   readonly System.UInt32                   <maxBufferSize>k__BackingField  // 0x1c
  private   readonly System.UInt32                   <optimalBufferSize>k__BackingField  // 0x20
PROPERTIES:
  numChannels  get=0x02B77620
  supportsImpulse  get=0x04D90BB0
  supportsBuffer  get=0x04D92DC0
  frequencyHz  get=0x04D88B10
  maxBufferSize  get=0x04D8CAD0
  optimalBufferSize  get=0x02B2E2D0
METHODS:
  RVA=0x04DAC090  token=0x6000C90  System.Void .ctor(System.UInt32 numChannels, System.Boolean supportsImpulse, System.Boolean supportsBuffer, System.UInt32 frequencyHz, System.UInt32 maxBufferSize, System.UInt32 optimalBufferSize)
  RVA=0x04DAC070  token=0x6000C91  System.Void .ctor(System.UInt32 numChannels, System.UInt32 frequencyHz, System.UInt32 maxBufferSize)
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand
TYPE:  sealed struct
TOKEN: 0x20000F8
SIZE:  0x2C
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  private           UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  public            System.UInt32                   numChannels  // 0x18
  public            System.Boolean                  supportsImpulse  // 0x1c
  public            System.Boolean                  supportsBuffer  // 0x1d
  public            System.UInt32                   frequencyHz  // 0x20
  public            System.UInt32                   maxBufferSize  // 0x24
  public            System.UInt32                   optimalBufferSize  // 0x28
PROPERTIES:
  Type  get=0x04DAC030
  typeStatic  get=0x04DAC030
  capabilities  get=0x04DAC040
METHODS:
  RVA=0x04DAC000  token=0x6000C9B  UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand Create()
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand
TYPE:  sealed struct
TOKEN: 0x20000F9
SIZE:  0x420
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kMaxHapticBufferSize  // const
  private   static  System.Int32                    kSize  // const
  private           UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  private           System.Int32                    channel  // 0x18
  private           System.Int32                    bufferSize  // 0x1c
  private           UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand.<buffer>e__FixedBufferbuffer  // 0x20
PROPERTIES:
  Type  get=0x04DAC100
  typeStatic  get=0x04DAC100
METHODS:
  RVA=0x0B219DB4  token=0x6000C9E  UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand Create(System.Byte[] rumbleBuffer)
END_CLASS

CLASS: UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand
TYPE:  sealed struct
TOKEN: 0x20000FB
SIZE:  0x24
IMPLEMENTS: UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
FIELDS:
  private   static  System.Int32                    kSize  // const
  private           UnityEngine.InputSystem.LowLevel.InputDeviceCommandbaseCommand  // 0x10
  private           System.Int32                    channel  // 0x18
  private           System.Single                   amplitude  // 0x1c
  private           System.Single                   duration  // 0x20
PROPERTIES:
  Type  get=0x04DAC140
  typeStatic  get=0x04DAC140
METHODS:
  RVA=0x04DAC110  token=0x6000CA1  UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand Create(System.Int32 motorChannel, System.Single motorAmplitude, System.Single motorDuration)
END_CLASS

CLASS: UnityEngine.XR.WindowsMR.Input.WMRHMD
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x1E8
EXTENDS: UnityEngine.InputSystem.XR.XRHMD
FIELDS:
  private           UnityEngine.InputSystem.Controls.ButtonControl<userPresence>k__BackingField  // 0x1e0
PROPERTIES:
  userPresence  get=0x04D8D930  set=0x06B8B2C4
METHODS:
  RVA=0x0B1A9A14  token=0x60000F0  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000F1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.WindowsMR.Input.HololensHand
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x1D0
EXTENDS: UnityEngine.InputSystem.XR.XRController
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.ButtonControl<airTap>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.AxisControl<sourceLossRisk>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.Vector3Control<sourceLossMitigationDirection>k__BackingField  // 0x1c8
PROPERTIES:
  deviceVelocity  get=0x04D8D960  set=0x053971DC
  airTap  get=0x04D8DA90  set=0x05397240
  sourceLossRisk  get=0x04D8DA80  set=0x06B8B42C
  sourceLossMitigationDirection  get=0x04D8D790  set=0x06B8B0D0
METHODS:
  RVA=0x0B198E64  token=0x60000FA  System.Void FinishSetup()
  RVA=0x053971D4  token=0x60000FB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.WindowsMR.Input.WMRSpatialController
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x238
EXTENDS: UnityEngine.InputSystem.XR.XRControllerWithRumble
FIELDS:
  private           UnityEngine.InputSystem.Controls.Vector2Control<joystick>k__BackingField  // 0x1b0
  private           UnityEngine.InputSystem.Controls.Vector2Control<touchpad>k__BackingField  // 0x1b8
  private           UnityEngine.InputSystem.Controls.AxisControl<grip>k__BackingField  // 0x1c0
  private           UnityEngine.InputSystem.Controls.ButtonControl<gripPressed>k__BackingField  // 0x1c8
  private           UnityEngine.InputSystem.Controls.ButtonControl<menu>k__BackingField  // 0x1d0
  private           UnityEngine.InputSystem.Controls.AxisControl<trigger>k__BackingField  // 0x1d8
  private           UnityEngine.InputSystem.Controls.ButtonControl<triggerPressed>k__BackingField  // 0x1e0
  private           UnityEngine.InputSystem.Controls.ButtonControl<joystickClicked>k__BackingField  // 0x1e8
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadClicked>k__BackingField  // 0x1f0
  private           UnityEngine.InputSystem.Controls.ButtonControl<touchpadTouched>k__BackingField  // 0x1f8
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceVelocity>k__BackingField  // 0x200
  private           UnityEngine.InputSystem.Controls.Vector3Control<deviceAngularVelocity>k__BackingField  // 0x208
  private           UnityEngine.InputSystem.Controls.AxisControl<batteryLevel>k__BackingField  // 0x210
  private           UnityEngine.InputSystem.Controls.AxisControl<sourceLossRisk>k__BackingField  // 0x218
  private           UnityEngine.InputSystem.Controls.Vector3Control<sourceLossMitigationDirection>k__BackingField  // 0x220
  private           UnityEngine.InputSystem.Controls.Vector3Control<pointerPosition>k__BackingField  // 0x228
  private           UnityEngine.InputSystem.Controls.QuaternionControl<pointerRotation>k__BackingField  // 0x230
PROPERTIES:
  joystick  get=0x04D8D960  set=0x053971DC
  touchpad  get=0x04D8DA90  set=0x05397240
  grip  get=0x04D8DA80  set=0x06B8B42C
  gripPressed  get=0x04D8D790  set=0x06B8B0D0
  menu  get=0x04D8D7B0  set=0x06B8B0F8
  trigger  get=0x04D87870  set=0x06B8B134
  triggerPressed  get=0x04D8D930  set=0x06B8B2C4
  joystickClicked  get=0x04D87930  set=0x06799DA0
  touchpadClicked  get=0x04D87940  set=0x06799DB4
  touchpadTouched  get=0x04D87920  set=0x06799D8C
  deviceVelocity  get=0x04D8DA40  set=0x06B8B3DC
  deviceAngularVelocity  get=0x04D8D8C0  set=0x06B8B24C
  batteryLevel  get=0x04D8D8B0  set=0x06B8B238
  sourceLossRisk  get=0x04D8D3D0  set=0x06B1D640
  sourceLossMitigationDirection  get=0x04D8D870  set=0x06B8B1FC
  pointerPosition  get=0x04D8D440  set=0x06B22BCC
  pointerRotation  get=0x04D8DAE0  set=0x06B8B490
METHODS:
  RVA=0x0B1A9A80  token=0x600011E  System.Void FinishSetup()
  RVA=0x053971D4  token=0x600011F  System.Void .ctor()
END_CLASS

