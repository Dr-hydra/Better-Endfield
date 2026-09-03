// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.IMGUIModule.dll
// Classes:  45
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

CLASS: WindowFunction
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x069095FC  token=0x6000097  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000098  System.Void Invoke(System.Int32 id)
END_CLASS

CLASS: ParentClipScope
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  m_Disposed  // 0x10
METHODS:
  RVA=0x0B40EF8C  token=0x60000B2  System.Void .ctor(UnityEngine.Matrix4x4 objectTransform, UnityEngine.Rect clipRect)
  RVA=0x0B40EF7C  token=0x60000B3  System.Void Dispose()
END_CLASS

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.GUILayoutOption.TypefixedWidth  // const
  public    static  UnityEngine.GUILayoutOption.TypefixedHeight  // const
  public    static  UnityEngine.GUILayoutOption.TypeminWidth  // const
  public    static  UnityEngine.GUILayoutOption.TypemaxWidth  // const
  public    static  UnityEngine.GUILayoutOption.TypeminHeight  // const
  public    static  UnityEngine.GUILayoutOption.TypemaxHeight  // const
  public    static  UnityEngine.GUILayoutOption.TypestretchWidth  // const
  public    static  UnityEngine.GUILayoutOption.TypestretchHeight  // const
  public    static  UnityEngine.GUILayoutOption.TypealignStart  // const
  public    static  UnityEngine.GUILayoutOption.TypealignMiddle  // const
  public    static  UnityEngine.GUILayoutOption.TypealignEnd  // const
  public    static  UnityEngine.GUILayoutOption.TypealignJustify  // const
  public    static  UnityEngine.GUILayoutOption.TypeequalSize  // const
  public    static  UnityEngine.GUILayoutOption.Typespacing  // const
METHODS:
END_CLASS

CLASS: LayoutCache
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x30
FIELDS:
  private           System.Int32                    <id>k__BackingField  // 0x10
  private           UnityEngine.GUILayoutGroup      topLevel  // 0x18
  private           UnityEngineInternal.GenericStacklayoutGroups  // 0x20
  private           UnityEngine.GUILayoutGroup      windows  // 0x28
PROPERTIES:
  id  set=0x014F51F0
METHODS:
  RVA=0x0B40EE8C  token=0x60000F9  System.Void .ctor(System.Int32 instanceID)
  RVA=0x0B40EBF0  token=0x60000FA  System.Void ResetCursor()
END_CLASS

CLASS: SkinChangedDelegate
TYPE:  sealed class
TOKEN: 0x2000017
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x600013F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000140  System.Void Invoke()
END_CLASS

CLASS: DblClickSnapping
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.TextEditor.DblClickSnappingWORDS  // const
  public    static  UnityEngine.TextEditor.DblClickSnappingPARAGRAPHS  // const
METHODS:
END_CLASS

CLASS: CharacterType
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextEditor.CharacterTypeLetterLike  // const
  public    static  UnityEngine.TextEditor.CharacterTypeSymbol  // const
  public    static  UnityEngine.TextEditor.CharacterTypeSymbol2  // const
  public    static  UnityEngine.TextEditor.CharacterTypeWhiteSpace  // const
METHODS:
END_CLASS

CLASS: Direction
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextEditor.DirectionForward  // const
  public    static  UnityEngine.TextEditor.DirectionBackward  // const
METHODS:
END_CLASS

CLASS: TextEditOp
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextEditor.TextEditOpMoveLeft  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveRight  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveUp  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveDown  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveLineStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveLineEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveTextStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveTextEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpMovePageUp  // const
  public    static  UnityEngine.TextEditor.TextEditOpMovePageDown  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveGraphicalLineStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveGraphicalLineEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveWordLeft  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveWordRight  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveParagraphForward  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveParagraphBackward  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveToStartOfNextWord  // const
  public    static  UnityEngine.TextEditor.TextEditOpMoveToEndOfPreviousWord  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectLeft  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectRight  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectUp  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectDown  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectTextStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectTextEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectPageUp  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectPageDown  // const
  public    static  UnityEngine.TextEditor.TextEditOpExpandSelectGraphicalLineStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpExpandSelectGraphicalLineEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectGraphicalLineStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectGraphicalLineEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectWordLeft  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectWordRight  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectToEndOfPreviousWord  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectToStartOfNextWord  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectParagraphBackward  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectParagraphForward  // const
  public    static  UnityEngine.TextEditor.TextEditOpDelete  // const
  public    static  UnityEngine.TextEditor.TextEditOpBackspace  // const
  public    static  UnityEngine.TextEditor.TextEditOpDeleteWordBack  // const
  public    static  UnityEngine.TextEditor.TextEditOpDeleteWordForward  // const
  public    static  UnityEngine.TextEditor.TextEditOpDeleteLineBack  // const
  public    static  UnityEngine.TextEditor.TextEditOpCut  // const
  public    static  UnityEngine.TextEditor.TextEditOpCopy  // const
  public    static  UnityEngine.TextEditor.TextEditOpPaste  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectAll  // const
  public    static  UnityEngine.TextEditor.TextEditOpSelectNone  // const
  public    static  UnityEngine.TextEditor.TextEditOpScrollStart  // const
  public    static  UnityEngine.TextEditor.TextEditOpScrollEnd  // const
  public    static  UnityEngine.TextEditor.TextEditOpScrollPageUp  // const
  public    static  UnityEngine.TextEditor.TextEditOpScrollPageDown  // const
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0687A850  token=0x60002DA  System.UInt32 ComputeStringHash(System.String s)
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Event
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x18
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private   static  UnityEngine.Event               s_Current  // static @ 0x0
  private   static  UnityEngine.Event               s_MasterEvent  // static @ 0x8
PROPERTIES:
  rawType  get=0x037989A0
  mousePosition  get=0x0B3FE568  set=0x0B3FE874
  delta  get=0x0B3FE448  set=0x0B3FE794
  pointerType  get=0x0B3FE58C
  button  get=0x0B3FE338
  modifiers  get=0x03798C20  set=0x0B3FE7F0
  pressure  get=0x0B3FE5C0
  clickCount  get=0x0B3FE36C
  character  get=0x03798D00  set=0x0B3FE640
  keyCode  get=0x03798C60  set=0x0B3FE7B0
  displayIndex  get=0x0B3FE46C  set=0x03C2D390
  type  get=0x0B3FE60C  set=0x0B3FE890
  commandName  get=0x0B3FE3A0  set=0x0B3FE684
  shift  get=0x0B3FE5F4
  control  get=0x0B3FE3EC
  alt  get=0x0B3FE320
  command  get=0x0B3FE3D4
  current  get=0x04DBCF50  set=0x0B3FE6C8
  isKey  get=0x0B3FE4D8
  isMouse  get=0x0B3FE4F8
  isDirectManipulationDevice  get=0x0B3FE4A0
METHODS:
  RVA=0x0B3FCD20  token=0x6000018  System.Void Internal_Use()
  RVA=0x0405B8C0  token=0x6000019  System.IntPtr Internal_Create(System.Int32 displayIndex)
  RVA=0x04A399D0  token=0x600001A  System.Void Internal_Destroy(System.IntPtr ptr)
  RVA=0x0B3FCCE0  token=0x600001B  UnityEngine.EventType GetTypeForControl(System.Int32 controlID)
  RVA=0x0B3FCA7C  token=0x600001C  System.Void CopyFromPtr(System.IntPtr ptr)
  RVA=0x03798960  token=0x600001D  System.Boolean PopEvent(UnityEngine.Event outEvent)
  RVA=0x03C2D350  token=0x600001E  System.Void Internal_SetNativeEvent(System.IntPtr ptr)
  RVA=0x03C2D200  token=0x600001F  System.Void Internal_MakeMasterEventCurrent(System.Int32 displayIndex)
  RVA=0x0B3FCC44  token=0x6000020  System.Int32 GetDoubleClickTime()
  RVA=0x0405B8A0  token=0x6000021  System.Void .ctor()
  RVA=0x0405AF50  token=0x6000022  System.Void .ctor(System.Int32 displayIndex)
  RVA=0x04A39930  token=0x6000023  System.Void Finalize()
  RVA=0x0B3FCAC0  token=0x6000024  System.Void CopyFrom(UnityEngine.Event e)
  RVA=0x0B3FCD54  token=0x600002E  UnityEngine.Event KeyboardEvent(System.String key)
  RVA=0x0B3FCC70  token=0x600002F  System.Int32 GetHashCode()
  RVA=0x0B3FCAEC  token=0x6000030  System.Boolean Equals(System.Object obj)
  RVA=0x0B3FDD00  token=0x6000031  System.String ToString()
  RVA=0x0B3FE20C  token=0x6000032  System.Void Use()
  RVA=0x0B3FE524  token=0x6000033  System.Void get_mousePosition_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B3FE830  token=0x6000034  System.Void set_mousePosition_Injected(UnityEngine.Vector2& value)
  RVA=0x0B3FE404  token=0x6000035  System.Void get_delta_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B3FE750  token=0x6000036  System.Void set_delta_Injected(UnityEngine.Vector2& value)
END_CLASS

CLASS: UnityEngine.EventType
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventType           MouseDown  // const
  public    static  UnityEngine.EventType           MouseUp  // const
  public    static  UnityEngine.EventType           MouseMove  // const
  public    static  UnityEngine.EventType           MouseDrag  // const
  public    static  UnityEngine.EventType           KeyDown  // const
  public    static  UnityEngine.EventType           KeyUp  // const
  public    static  UnityEngine.EventType           ScrollWheel  // const
  public    static  UnityEngine.EventType           Repaint  // const
  public    static  UnityEngine.EventType           Layout  // const
  public    static  UnityEngine.EventType           DragUpdated  // const
  public    static  UnityEngine.EventType           DragPerform  // const
  public    static  UnityEngine.EventType           DragExited  // const
  public    static  UnityEngine.EventType           Ignore  // const
  public    static  UnityEngine.EventType           Used  // const
  public    static  UnityEngine.EventType           ValidateCommand  // const
  public    static  UnityEngine.EventType           ExecuteCommand  // const
  public    static  UnityEngine.EventType           ContextClick  // const
  public    static  UnityEngine.EventType           MouseEnterWindow  // const
  public    static  UnityEngine.EventType           MouseLeaveWindow  // const
  public    static  UnityEngine.EventType           TouchDown  // const
  public    static  UnityEngine.EventType           TouchUp  // const
  public    static  UnityEngine.EventType           TouchMove  // const
  public    static  UnityEngine.EventType           TouchEnter  // const
  public    static  UnityEngine.EventType           TouchLeave  // const
  public    static  UnityEngine.EventType           TouchStationary  // const
  public    static  UnityEngine.EventType           mouseDown  // const
  public    static  UnityEngine.EventType           mouseUp  // const
  public    static  UnityEngine.EventType           mouseMove  // const
  public    static  UnityEngine.EventType           mouseDrag  // const
  public    static  UnityEngine.EventType           keyDown  // const
  public    static  UnityEngine.EventType           keyUp  // const
  public    static  UnityEngine.EventType           scrollWheel  // const
  public    static  UnityEngine.EventType           repaint  // const
  public    static  UnityEngine.EventType           layout  // const
  public    static  UnityEngine.EventType           dragUpdated  // const
  public    static  UnityEngine.EventType           dragPerform  // const
  public    static  UnityEngine.EventType           ignore  // const
  public    static  UnityEngine.EventType           used  // const
METHODS:
END_CLASS

CLASS: UnityEngine.EventModifiers
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventModifiers      None  // const
  public    static  UnityEngine.EventModifiers      Shift  // const
  public    static  UnityEngine.EventModifiers      Control  // const
  public    static  UnityEngine.EventModifiers      Alt  // const
  public    static  UnityEngine.EventModifiers      Command  // const
  public    static  UnityEngine.EventModifiers      Numeric  // const
  public    static  UnityEngine.EventModifiers      CapsLock  // const
  public    static  UnityEngine.EventModifiers      FunctionKey  // const
METHODS:
END_CLASS

CLASS: UnityEngine.PointerType
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.PointerType         Mouse  // const
  public    static  UnityEngine.PointerType         Touch  // const
  public    static  UnityEngine.PointerType         Pen  // const
METHODS:
END_CLASS

CLASS: UnityEngine.EventInterests
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x13
FIELDS:
  private           System.Boolean                  <wantsMouseMove>k__BackingField  // 0x10
  private           System.Boolean                  <wantsMouseEnterLeaveWindow>k__BackingField  // 0x11
  private           System.Boolean                  <wantsLessLayoutEvents>k__BackingField  // 0x12
PROPERTIES:
  wantsMouseMove  get=0x04D883E0  set=0x04D87180
  wantsMouseEnterLeaveWindow  get=0x04D91120  set=0x04D91150
  wantsLessLayoutEvents  get=0x04D91110
METHODS:
  RVA=0x0B3FC9EC  token=0x600003C  System.Boolean WantsEvent(UnityEngine.EventType type)
  RVA=0x0B3FCA08  token=0x600003D  System.Boolean WantsLayoutPass(UnityEngine.EventType type)
END_CLASS

CLASS: UnityEngine.GUI
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    s_ScrollControlId  // static @ 0x0
  private   static  System.Int32                    s_HotTextField  // static @ 0x4
  private   static readonly System.Int32                    s_BoxHash  // static @ 0x8
  private   static readonly System.Int32                    s_ButonHash  // static @ 0xc
  private   static readonly System.Int32                    s_RepeatButtonHash  // static @ 0x10
  private   static readonly System.Int32                    s_ToggleHash  // static @ 0x14
  private   static readonly System.Int32                    s_ButtonGridHash  // static @ 0x18
  private   static readonly System.Int32                    s_SliderHash  // static @ 0x1c
  private   static readonly System.Int32                    s_BeginGroupHash  // static @ 0x20
  private   static readonly System.Int32                    s_ScrollviewHash  // static @ 0x24
  private   static  System.Int32                    <scrollTroughSide>k__BackingField  // static @ 0x28
  private   static  System.DateTime                 <nextScrollStepTime>k__BackingField  // static @ 0x30
  private   static  UnityEngine.GUISkin             s_Skin  // static @ 0x38
  private   static  UnityEngine.Rect                s_ToolTipRect  // static @ 0x40
  private   static  UnityEngineInternal.GenericStack<scrollViewStates>k__BackingField  // static @ 0x50
PROPERTIES:
  color  get=0x0B40E5E4  set=0x0B40E9D8
  backgroundColor  get=0x0B40E4E0  set=0x0B40E924
  contentColor  get=0x0B40E664  set=0x0B40EA58
  changed  get=0x0B40E584  set=0x0B40E970
  enabled  get=0x0B40E6B0  set=0x0B40EAA4
  usePageScrollbars  get=0x0B40E8C4
  blendMaterial  get=0x0B40E52C
  blitMaterial  get=0x0B40E558
  roundedRectMaterial  get=0x0B40E774
  roundedRectWithColorPerBorderMaterial  get=0x0B40E7A0
  scrollTroughSide  get=0x0B40E7CC  set=0x0B40EB68
  nextScrollStepTime  get=0x0B40E724  set=0x0B40EB10
  skin  get=0x0B40E86C  set=0x0B40EBBC
  matrix  get=0x0B40E6DC  set=0x0B40EAD8
  scrollViewStates  get=0x0B40E81C
METHODS:
  RVA=0x0B40C4F4  token=0x600004D  System.Void GrabMouseControl(System.Int32 id)
  RVA=0x0B40D090  token=0x600004E  System.Boolean HasMouseControl(System.Int32 id)
  RVA=0x0B40D5F0  token=0x600004F  System.Void ReleaseMouseControl()
  RVA=0x0B40D294  token=0x6000050  System.Void InternalRepaintEditorWindow()
  RVA=0x0B40D320  token=0x6000051  UnityEngine.Rect Internal_DoWindow(System.Int32 id, System.Int32 instanceID, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, System.Object skin, System.Boolean forceRectOnLayout)
  RVA=0x0B40B1C4  token=0x6000052  System.Void DragWindow(UnityEngine.Rect position)
  RVA=0x0B40E24C  token=0x6000053  System.Void .cctor()
  RVA=0x0B40AB68  token=0x600005A  System.Void DoSetSkin(UnityEngine.GUISkin newSkin)
  RVA=0x0B40D444  token=0x600005D  System.Void Label(UnityEngine.Rect position, System.String text)
  RVA=0x0B40D3D8  token=0x600005E  System.Void Label(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
  RVA=0x0B40D4E8  token=0x600005F  System.Void Label(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40BBD8  token=0x6000060  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image)
  RVA=0x0B40BB64  token=0x6000061  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode)
  RVA=0x0B40B210  token=0x6000062  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend)
  RVA=0x0B40B99C  token=0x6000063  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect)
  RVA=0x0B40BA5C  token=0x6000064  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, System.Single borderWidth, System.Single borderRadius)
  RVA=0x0B40B29C  token=0x6000065  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, System.Single borderRadius)
  RVA=0x0B40B8A8  token=0x6000066  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses)
  RVA=0x0B40B79C  token=0x6000067  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses, System.Boolean drawSmoothCorners)
  RVA=0x0B40B3A0  token=0x6000068  System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color leftColor, UnityEngine.Color topColor, UnityEngine.Color rightColor, UnityEngine.Color bottomColor, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses, System.Boolean drawSmoothCorners)
  RVA=0x0B40A0D8  token=0x6000069  System.Boolean CalculateScaledTextureRects(UnityEngine.Rect position, UnityEngine.ScaleMode scaleMode, System.Single imageAspect, UnityEngine.Rect& outScreenRect, UnityEngine.Rect& outSourceRect)
  RVA=0x0B409E38  token=0x600006A  System.Void Box(UnityEngine.Rect position, System.String text)
  RVA=0x0B409D50  token=0x600006B  System.Void Box(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B409EDC  token=0x600006C  System.Boolean Button(UnityEngine.Rect position, System.String text)
  RVA=0x0B409F58  token=0x600006D  System.Boolean Button(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
  RVA=0x0B409FEC  token=0x600006E  System.Boolean Button(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40A070  token=0x600006F  System.Boolean Button(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40A954  token=0x6000070  System.Boolean DoRepeatButton(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.FocusType focusType)
  RVA=0x0B40DD34  token=0x6000071  System.String TextField(UnityEngine.Rect position, System.String text)
  RVA=0x0B40D548  token=0x6000072  System.String PasswordFieldGetStrToShow(System.String password, System.Char maskChar)
  RVA=0x0B40ABE4  token=0x6000073  System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style)
  RVA=0x0B40AF10  token=0x6000074  System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText)
  RVA=0x0B40AC80  token=0x6000075  System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, System.Char maskChar)
  RVA=0x0B40CD34  token=0x6000076  System.Void HandleTextFieldEventForTouchscreen(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, System.Char maskChar, UnityEngine.TextEditor editor)
  RVA=0x0B40C7B0  token=0x6000077  System.Void HandleTextFieldEventForDesktop(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, UnityEngine.TextEditor editor)
  RVA=0x0B40C528  token=0x6000078  System.Void HandleTextFieldEventForDesktopWithForcedKeyboard(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, UnityEngine.TextEditor editor)
  RVA=0x0B40DE0C  token=0x6000079  System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, System.String text)
  RVA=0x0B40DEC4  token=0x600007A  System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40A514  token=0x600007B  System.Boolean DoControl(UnityEngine.Rect position, System.Int32 id, System.Boolean on, System.Boolean hover, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40A770  token=0x600007C  System.Void DoLabel(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40AFC8  token=0x600007D  System.Boolean DoToggle(UnityEngine.Rect position, System.Int32 id, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40A464  token=0x600007E  System.Boolean DoButton(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B40DBE4  token=0x600007F  System.Single Slider(UnityEngine.Rect position, System.Single value, System.Single size, System.Single start, System.Single end, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, System.Boolean horiz, System.Int32 id, UnityEngine.GUIStyle thumbExtent)
  RVA=0x0B40D0C4  token=0x6000080  System.Single HorizontalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle style)
  RVA=0x0B40D61C  token=0x6000081  System.Boolean ScrollerRepeatButton(System.Int32 scrollerID, UnityEngine.Rect rect, UnityEngine.GUIStyle style)
  RVA=0x0B40DF7C  token=0x6000082  System.Single VerticalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single topValue, System.Single bottomValue, UnityEngine.GUIStyle style)
  RVA=0x0B40D7D4  token=0x6000083  System.Single Scroller(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, UnityEngine.GUIStyle leftButton, UnityEngine.GUIStyle rightButton, System.Boolean horiz)
  RVA=0x0B409294  token=0x6000084  System.Void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B4090F0  token=0x6000085  System.Void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.Vector2 scrollOffset)
  RVA=0x0B40BC38  token=0x6000086  System.Void EndGroup()
  RVA=0x0B409C5C  token=0x6000088  UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect)
  RVA=0x0B40931C  token=0x6000089  UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUIStyle background)
  RVA=0x0B40BC50  token=0x600008A  System.Void EndScrollView()
  RVA=0x0B40BC94  token=0x600008B  System.Void EndScrollView(System.Boolean handleScrollWheel)
  RVA=0x0B40E14C  token=0x600008C  UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text)
  RVA=0x0B40B09C  token=0x600008D  UnityEngine.Rect DoWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, UnityEngine.GUISkin skin, System.Boolean forceRectOnLayout)
  RVA=0x0B40A26C  token=0x600008E  System.Void CallWindowDelegate(UnityEngine.GUI.WindowFunction func, System.Int32 id, System.Int32 instanceID, UnityEngine.GUISkin _skin, System.Int32 forceRect, System.Single width, System.Single height, UnityEngine.GUIStyle style)
  RVA=0x0B40E5B0  token=0x600008F  System.Void get_color_Injected(UnityEngine.Color& ret)
  RVA=0x0B40E9A4  token=0x6000090  System.Void set_color_Injected(UnityEngine.Color& value)
  RVA=0x0B40E4AC  token=0x6000091  System.Void get_backgroundColor_Injected(UnityEngine.Color& ret)
  RVA=0x0B40E8F0  token=0x6000092  System.Void set_backgroundColor_Injected(UnityEngine.Color& value)
  RVA=0x0B40E630  token=0x6000093  System.Void get_contentColor_Injected(UnityEngine.Color& ret)
  RVA=0x0B40EA24  token=0x6000094  System.Void set_contentColor_Injected(UnityEngine.Color& value)
  RVA=0x0B40D2C0  token=0x6000095  System.Void Internal_DoWindow_Injected(System.Int32 id, System.Int32 instanceID, UnityEngine.Rect& clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, System.Object skin, System.Boolean forceRectOnLayout, UnityEngine.Rect& ret)
  RVA=0x0B40B190  token=0x6000096  System.Void DragWindow_Injected(UnityEngine.Rect& position)
END_CLASS

CLASS: UnityEngine.GUIClip
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
PROPERTIES:
  visibleRect  get=0x0B3FEDEC
METHODS:
  RVA=0x0B3FEC1C  token=0x600009A  System.Void Internal_Push(UnityEngine.Rect screenRect, UnityEngine.Vector2 scrollOffset, UnityEngine.Vector2 renderOffset, System.Boolean resetOffset)
  RVA=0x0B3FEAB0  token=0x600009B  System.Void Internal_Pop()
  RVA=0x0B3FEA58  token=0x600009C  System.Int32 Internal_GetCount()
  RVA=0x0B3FED7C  token=0x600009D  UnityEngine.Vector2 Unclip_Vector2(UnityEngine.Vector2 pos)
  RVA=0x0B3FECFC  token=0x600009E  UnityEngine.Vector2 UnclipToWindow_Vector2(UnityEngine.Vector2 pos)
  RVA=0x0B3FE9BC  token=0x600009F  UnityEngine.Vector2 ClipToWindow_Vector2(UnityEngine.Vector2 absolutePos)
  RVA=0x0B3FEA2C  token=0x60000A0  UnityEngine.Matrix4x4 GetMatrix()
  RVA=0x0B3FECA8  token=0x60000A1  System.Void SetMatrix(UnityEngine.Matrix4x4 m)
  RVA=0x0B3FEB30  token=0x60000A2  System.Void Internal_PushParentClip(UnityEngine.Matrix4x4 objectTransform, UnityEngine.Rect clipRect)
  RVA=0x0B3FEBA4  token=0x60000A3  System.Void Internal_PushParentClip(UnityEngine.Matrix4x4 renderTransform, UnityEngine.Matrix4x4 inputTransform, UnityEngine.Rect clipRect)
  RVA=0x0B3FEA84  token=0x60000A4  System.Void Internal_PopParentClip()
  RVA=0x0B3FEC50  token=0x60000A5  System.Void Push(UnityEngine.Rect screenRect, UnityEngine.Vector2 scrollOffset, UnityEngine.Vector2 renderOffset, System.Boolean resetOffset)
  RVA=0x0B3FEC48  token=0x60000A6  System.Void Pop()
  RVA=0x0B3FEDA8  token=0x60000A7  UnityEngine.Vector2 Unclip(UnityEngine.Vector2 pos)
  RVA=0x0B3FED28  token=0x60000A8  UnityEngine.Vector2 UnclipToWindow(UnityEngine.Vector2 pos)
  RVA=0x0B3FE9E8  token=0x60000A9  UnityEngine.Vector2 ClipToWindow(UnityEngine.Vector2 absolutePos)
  RVA=0x0B3FEDB8  token=0x60000AA  System.Void get_visibleRect_Injected(UnityEngine.Rect& ret)
  RVA=0x0B3FEBB8  token=0x60000AB  System.Void Internal_Push_Injected(UnityEngine.Rect& screenRect, UnityEngine.Vector2& scrollOffset, UnityEngine.Vector2& renderOffset, System.Boolean resetOffset)
  RVA=0x0B3FED38  token=0x60000AC  System.Void Unclip_Vector2_Injected(UnityEngine.Vector2& pos, UnityEngine.Vector2& ret)
  RVA=0x0B3FECB8  token=0x60000AD  System.Void UnclipToWindow_Vector2_Injected(UnityEngine.Vector2& pos, UnityEngine.Vector2& ret)
  RVA=0x0B3FE978  token=0x60000AE  System.Void ClipToWindow_Vector2_Injected(UnityEngine.Vector2& absolutePos, UnityEngine.Vector2& ret)
  RVA=0x0B3FE9F8  token=0x60000AF  System.Void GetMatrix_Injected(UnityEngine.Matrix4x4& ret)
  RVA=0x0B3FEC74  token=0x60000B0  System.Void SetMatrix_Injected(UnityEngine.Matrix4x4& m)
  RVA=0x0B3FEADC  token=0x60000B1  System.Void Internal_PushParentClip_Injected(UnityEngine.Matrix4x4& renderTransform, UnityEngine.Matrix4x4& inputTransform, UnityEngine.Rect& clipRect)
END_CLASS

CLASS: UnityEngine.GUIContent
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x28
FIELDS:
  private           System.String                   m_Text  // 0x10
  private           UnityEngine.Texture             m_Image  // 0x18
  private           System.String                   m_Tooltip  // 0x20
  private   static readonly UnityEngine.GUIContent          s_Text  // static @ 0x0
  private   static readonly UnityEngine.GUIContent          s_Image  // static @ 0x8
  private   static readonly UnityEngine.GUIContent          s_TextImage  // static @ 0x10
  public    static  UnityEngine.GUIContent          none  // static @ 0x18
PROPERTIES:
  text  get=0x04D9B600  set=0x053908C0
  image  set=0x05392C40
  tooltip  get=0x04DBA910  set=0x05390F40
  hash  get=0x0B3FF38C
METHODS:
  RVA=0x0B3FF2C8  token=0x60000B9  System.Void .ctor()
  RVA=0x0B3FF330  token=0x60000BA  System.Void .ctor(System.String text)
  RVA=0x0B3FF1FC  token=0x60000BB  System.Void .ctor(System.String text, System.String tooltip)
  RVA=0x0B3FF144  token=0x60000BC  System.Void .ctor(System.String text, UnityEngine.Texture image, System.String tooltip)
  RVA=0x0B3FF21C  token=0x60000BD  System.Void .ctor(UnityEngine.GUIContent src)
  RVA=0x0B3FEF30  token=0x60000BF  UnityEngine.GUIContent Temp(System.String t)
  RVA=0x0B3FEE0C  token=0x60000C0  System.Void ClearStaticCache()
  RVA=0x0B3FEFBC  token=0x60000C1  System.String ToString()
  RVA=0x0B3FEFE0  token=0x60000C2  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.ScaleMode
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.ScaleMode           StretchToFill  // const
  public    static  UnityEngine.ScaleMode           ScaleAndCrop  // const
  public    static  UnityEngine.ScaleMode           ScaleToFit  // const
METHODS:
END_CLASS

CLASS: UnityEngine.FocusType
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.FocusType           Native  // const
  public    static  UnityEngine.FocusType           Keyboard  // const
  public    static  UnityEngine.FocusType           Passive  // const
METHODS:
END_CLASS

CLASS: UnityEngine.GUILayout
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B403054  token=0x60000C3  System.Void Label(System.String text, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B4030E0  token=0x60000C4  System.Void Label(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402958  token=0x60000C5  System.Void DoLabel(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B403328  token=0x60000C6  System.Boolean Toggle(System.Boolean value, System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B4033A8  token=0x60000C7  System.Boolean Toggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402A00  token=0x60000C8  System.Boolean DoToggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B40314C  token=0x60000C9  System.Void Space(System.Single pixels)
  RVA=0x0B402DC8  token=0x60000CA  System.Void FlexibleSpace()
  RVA=0x0B40227C  token=0x60000CB  System.Void BeginHorizontal(UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402300  token=0x60000CC  System.Void BeginHorizontal(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402BDC  token=0x60000CD  System.Void EndHorizontal()
  RVA=0x0B402870  token=0x60000CE  System.Void BeginVertical(UnityEngine.GUILayoutOption[] options)
  RVA=0x0B4028F4  token=0x60000CF  System.Void BeginVertical(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B40272C  token=0x60000D0  System.Void BeginVertical(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402C88  token=0x60000D1  System.Void EndVertical()
  RVA=0x0B402068  token=0x60000D2  System.Void BeginArea(UnityEngine.Rect screenRect)
  RVA=0x0B4020F8  token=0x60000D3  System.Void BeginArea(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
  RVA=0x0B402ABC  token=0x60000D4  System.Void EndArea()
  RVA=0x0B402444  token=0x60000D5  UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402504  token=0x60000D6  UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUIStyle background, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B402C1C  token=0x60000D7  System.Void EndScrollView()
  RVA=0x0B402C28  token=0x60000D8  System.Void EndScrollView(System.Boolean handleScrollWheel)
  RVA=0x0B4033B8  token=0x60000D9  UnityEngine.GUILayoutOption Width(System.Single width)
  RVA=0x0B402FC8  token=0x60000DA  UnityEngine.GUILayoutOption Height(System.Single height)
  RVA=0x0B402D48  token=0x60000DB  UnityEngine.GUILayoutOption ExpandWidth(System.Boolean expand)
  RVA=0x0B402CC8  token=0x60000DC  UnityEngine.GUILayoutOption ExpandHeight(System.Boolean expand)
END_CLASS

CLASS: UnityEngine.GUILayoutOption
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x20
FIELDS:
  private           UnityEngine.GUILayoutOption.Typetype  // 0x10
  private           System.Object                   value  // 0x18
METHODS:
  RVA=0x05391D70  token=0x60000DD  System.Void .ctor(UnityEngine.GUILayoutOption.Type type, System.Object value)
END_CLASS

CLASS: UnityEngine.GUILayoutUtility
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.GUILayoutUtility.LayoutCache>s_StoredLayouts  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.GUILayoutUtility.LayoutCache>s_StoredWindows  // static @ 0x8
  private   static  UnityEngine.GUILayoutUtility.LayoutCachecurrent  // static @ 0x10
  private   static readonly UnityEngine.Rect                kDummyRect  // static @ 0x18
  private   static  System.Int32                    <unbalancedgroupscount>k__BackingField  // static @ 0x28
  private   static  UnityEngine.GUIStyle            s_SpaceStyle  // static @ 0x30
PROPERTIES:
  unbalancedgroupscount  get=0x0B401FC4  set=0x0B402014
  spaceStyle  get=0x0B401ED4
METHODS:
  RVA=0x0B401298  token=0x60000DE  UnityEngine.Rect Internal_GetWindowRect(System.Int32 windowID)
  RVA=0x0B401330  token=0x60000DF  System.Void Internal_MoveWindow(System.Int32 windowID, UnityEngine.Rect r)
  RVA=0x0B401C08  token=0x60000E2  UnityEngine.GUILayoutUtility.LayoutCache SelectIDList(System.Int32 instanceID, System.Boolean isWindow)
  RVA=0x0B4006EC  token=0x60000E3  System.Void Begin(System.Int32 instanceID)
  RVA=0x0B3FFD98  token=0x60000E4  System.Void BeginContainer(UnityEngine.GUILayoutUtility.LayoutCache cache)
  RVA=0x0B4003E4  token=0x60000E5  System.Void BeginWindow(System.Int32 windowID, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B4019EC  token=0x60000E6  System.Void Layout()
  RVA=0x0B401694  token=0x60000E7  System.Void LayoutFromEditorWindow()
  RVA=0x0B4014FC  token=0x60000E8  System.Void LayoutFromContainer(System.Single w, System.Single h)
  RVA=0x0B401388  token=0x60000E9  System.Void LayoutFreeGroup(UnityEngine.GUILayoutGroup toplevel)
  RVA=0x0B401858  token=0x60000EA  System.Void LayoutSingleGroup(UnityEngine.GUILayoutGroup i)
  RVA=0x0B40092C  token=0x60000EB  UnityEngine.GUILayoutGroup CreateGUILayoutGroupInstanceOfType(System.Type LayoutType)
  RVA=0x0B400164  token=0x60000EC  UnityEngine.GUILayoutGroup BeginLayoutGroup(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options, System.Type layoutType)
  RVA=0x0B400EA4  token=0x60000ED  System.Void EndLayoutGroup()
  RVA=0x0B3FFF24  token=0x60000EE  UnityEngine.GUILayoutGroup BeginLayoutArea(UnityEngine.GUIStyle style, System.Type layoutType)
  RVA=0x0B4011D0  token=0x60000EF  UnityEngine.Rect GetRect(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B400BD0  token=0x60000F0  UnityEngine.Rect DoGetRect(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B40112C  token=0x60000F1  UnityEngine.Rect GetRect(System.Single width, System.Single height, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B400A30  token=0x60000F2  UnityEngine.Rect DoGetRect(System.Single minWidth, System.Single maxWidth, System.Single minHeight, System.Single maxHeight, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B401070  token=0x60000F3  UnityEngine.Rect GetLastRect()
  RVA=0x0B401D80  token=0x60000F5  System.Void .cctor()
  RVA=0x0B401258  token=0x60000F6  System.Void Internal_GetWindowRect_Injected(System.Int32 windowID, UnityEngine.Rect& ret)
  RVA=0x0B4012F0  token=0x60000F7  System.Void Internal_MoveWindow_Injected(System.Int32 windowID, UnityEngine.Rect& r)
END_CLASS

CLASS: UnityEngine.GUISettings
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x38
FIELDS:
  private           System.Boolean                  m_DoubleClickSelectsWord  // 0x10
  private           System.Boolean                  m_TripleClickSelectsLine  // 0x11
  private           UnityEngine.Color               m_CursorColor  // 0x14
  private           System.Single                   m_CursorFlashSpeed  // 0x24
  private           UnityEngine.Color               m_SelectionColor  // 0x28
PROPERTIES:
  doubleClickSelectsWord  get=0x04DBCFA0
  tripleClickSelectsLine  get=0x04DBCFC0
  cursorColor  get=0x04D95490
  cursorFlashSpeed  get=0x0B4034A8
  selectionColor  get=0x04DBCFB0
METHODS:
  RVA=0x0B403444  token=0x60000FB  System.Single Internal_GetCursorFlashSpeed()
  RVA=0x0B403470  token=0x6000101  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.GUISkin
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0xF0
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           UnityEngine.Font                m_Font  // 0x18
  private           UnityEngine.GUIStyle            m_box  // 0x20
  private           UnityEngine.GUIStyle            m_button  // 0x28
  private           UnityEngine.GUIStyle            m_toggle  // 0x30
  private           UnityEngine.GUIStyle            m_label  // 0x38
  private           UnityEngine.GUIStyle            m_textField  // 0x40
  private           UnityEngine.GUIStyle            m_textArea  // 0x48
  private           UnityEngine.GUIStyle            m_window  // 0x50
  private           UnityEngine.GUIStyle            m_horizontalSlider  // 0x58
  private           UnityEngine.GUIStyle            m_horizontalSliderThumb  // 0x60
  private           UnityEngine.GUIStyle            m_horizontalSliderThumbExtent  // 0x68
  private           UnityEngine.GUIStyle            m_verticalSlider  // 0x70
  private           UnityEngine.GUIStyle            m_verticalSliderThumb  // 0x78
  private           UnityEngine.GUIStyle            m_verticalSliderThumbExtent  // 0x80
  private           UnityEngine.GUIStyle            m_SliderMixed  // 0x88
  private           UnityEngine.GUIStyle            m_horizontalScrollbar  // 0x90
  private           UnityEngine.GUIStyle            m_horizontalScrollbarThumb  // 0x98
  private           UnityEngine.GUIStyle            m_horizontalScrollbarLeftButton  // 0xa0
  private           UnityEngine.GUIStyle            m_horizontalScrollbarRightButton  // 0xa8
  private           UnityEngine.GUIStyle            m_verticalScrollbar  // 0xb0
  private           UnityEngine.GUIStyle            m_verticalScrollbarThumb  // 0xb8
  private           UnityEngine.GUIStyle            m_verticalScrollbarUpButton  // 0xc0
  private           UnityEngine.GUIStyle            m_verticalScrollbarDownButton  // 0xc8
  private           UnityEngine.GUIStyle            m_ScrollView  // 0xd0
  private           UnityEngine.GUIStyle[]          m_CustomStyles  // 0xd8
  private           UnityEngine.GUISettings         m_Settings  // 0xe0
  private   static  UnityEngine.GUIStyle            ms_Error  // static @ 0x0
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.GUIStyle>m_Styles  // 0xe8
  private   static  UnityEngine.GUISkin.SkinChangedDelegatem_SkinChanged  // static @ 0x8
  private   static  UnityEngine.GUISkin             current  // static @ 0x10
PROPERTIES:
  font  get=0x04D96330  set=0x0B4049DC
  box  get=0x04DBA910  set=0x0B40496C
  label  get=0x04DBC0B0  set=0x0B404B98
  textField  get=0x04DBCF90  set=0x0B404C30
  textArea  get=0x04DBD080  set=0x0B404C0C
  button  get=0x04DBBE70  set=0x0B404990
  toggle  get=0x04DBBEB0  set=0x0B404C54
  window  get=0x04DBD100  set=0x0B404D88
  horizontalSlider  get=0x04DBD040  set=0x0B404B74
  horizontalSliderThumb  get=0x04DBD030  set=0x0B404B50
  horizontalSliderThumbExtent  get=0x04DBD020  set=0x0B404B2C
  sliderMixed  get=0x04DBD070  set=0x0B404BE4
  verticalSlider  get=0x04DBD0F0  set=0x0B404D64
  verticalSliderThumb  get=0x04DBD0E0  set=0x0B404D40
  verticalSliderThumbExtent  get=0x04DBD0D0  set=0x0B404D18
  horizontalScrollbar  get=0x04DBD010  set=0x0B404B04
  horizontalScrollbarThumb  get=0x04DBD000  set=0x0B404ADC
  horizontalScrollbarLeftButton  get=0x04DBCFE0  set=0x0B404A8C
  horizontalScrollbarRightButton  get=0x04DBCFF0  set=0x0B404AB4
  verticalScrollbar  get=0x04DBD0C0  set=0x0B404CF0
  verticalScrollbarThumb  get=0x04DBD0A0  set=0x0B404CA0
  verticalScrollbarUpButton  get=0x04DBD0B0  set=0x0B404CC8
  verticalScrollbarDownButton  get=0x04DBD090  set=0x0B404C78
  scrollView  get=0x04DBD050  set=0x0B404BBC
  customStyles  get=0x04DBCFD0  set=0x0B4049B4
  settings  get=0x04DBD060
  error  get=0x0B404898
METHODS:
  RVA=0x0B4047E0  token=0x6000102  System.Void .ctor()
  RVA=0x0B4047D8  token=0x6000103  System.Void OnEnable()
  RVA=0x0B404404  token=0x6000104  System.Void CleanupRoots()
  RVA=0x0B4034C8  token=0x6000139  System.Void Apply()
  RVA=0x0B40352C  token=0x600013A  System.Void BuildStyleCache()
  RVA=0x0B4045B4  token=0x600013B  UnityEngine.GUIStyle GetStyle(System.String styleName)
  RVA=0x0B404478  token=0x600013C  UnityEngine.GUIStyle FindStyle(System.String styleName)
  RVA=0x0B404760  token=0x600013D  System.Void MakeCurrent()
  RVA=0x0B4044FC  token=0x600013E  System.Collections.IEnumerator GetEnumerator()
END_CLASS

CLASS: UnityEngine.GUIStateObjects
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.Object>s_StateCache  // static @ 0x0
METHODS:
  RVA=0x0B404DAC  token=0x6000141  System.Object GetStateObject(System.Type t, System.Int32 controlID)
  RVA=0x0B404EA4  token=0x6000142  System.Object QueryStateObject(System.Type t, System.Int32 controlID)
  RVA=0x0B404F3C  token=0x6000143  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.GUIStyleState
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x20
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private   readonly UnityEngine.GUIStyle            m_SourceStyle  // 0x18
PROPERTIES:
  background  get=0x0B405138  set=0x0B4051DC
  textColor  get=0x0B4051B0  set=0x0B405264
METHODS:
  RVA=0x0B4050F0  token=0x6000148  System.IntPtr Init()
  RVA=0x0B404FC8  token=0x6000149  System.Void Cleanup()
  RVA=0x0B40511C  token=0x600014A  System.Void .ctor()
  RVA=0x0B3B8428  token=0x600014B  System.Void .ctor(UnityEngine.GUIStyle sourceStyle, System.IntPtr source)
  RVA=0x0B405080  token=0x600014C  UnityEngine.GUIStyleState GetGUIStyleState(UnityEngine.GUIStyle sourceStyle, System.IntPtr source)
  RVA=0x0B404FFC  token=0x600014D  System.Void Finalize()
  RVA=0x0B40516C  token=0x600014E  System.Void get_textColor_Injected(UnityEngine.Color& ret)
  RVA=0x0B405220  token=0x600014F  System.Void set_textColor_Injected(UnityEngine.Color& value)
END_CLASS

CLASS: UnityEngine.GUIStyle
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x80
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           UnityEngine.GUIStyleState       m_Normal  // 0x18
  private           UnityEngine.GUIStyleState       m_Hover  // 0x20
  private           UnityEngine.GUIStyleState       m_Active  // 0x28
  private           UnityEngine.GUIStyleState       m_Focused  // 0x30
  private           UnityEngine.GUIStyleState       m_OnNormal  // 0x38
  private           UnityEngine.GUIStyleState       m_OnHover  // 0x40
  private           UnityEngine.GUIStyleState       m_OnActive  // 0x48
  private           UnityEngine.GUIStyleState       m_OnFocused  // 0x50
  private           UnityEngine.RectOffset          m_Border  // 0x58
  private           UnityEngine.RectOffset          m_Padding  // 0x60
  private           UnityEngine.RectOffset          m_Margin  // 0x68
  private           UnityEngine.RectOffset          m_Overflow  // 0x70
  private           System.String                   m_Name  // 0x78
  private   static  System.Boolean                  showKeyboardFocus  // static @ 0x0
  private   static  UnityEngine.GUIStyle            s_None  // static @ 0x8
PROPERTIES:
  rawName  get=0x0B406E80  set=0x0B40742C
  font  get=0x0B406958  set=0x0B407238
  imagePosition  get=0x0B4069D4  set=0x0B4072A0
  alignment  get=0x0B4066DC  set=0x0B407008
  wordWrap  get=0x0B406F50  set=0x0B40753C
  clipping  get=0x0B4067A4  set=0x0B40706C
  contentOffset  get=0x0B40681C  set=0x0B4070F0
  fixedWidth  get=0x0B406874  set=0x0B407150
  fixedHeight  get=0x0B406840  set=0x0B40710C
  stretchWidth  get=0x0B406F1C  set=0x0B4074F8
  stretchHeight  get=0x0B406EE8  set=0x0B4074B4
  fontSize  get=0x0B4068F0  set=0x0B4071B8
  fontStyle  get=0x0B406924  set=0x0B4071F8
  richText  get=0x0B406EB4  set=0x0B407470
  Internal_clipOffset  set=0x0B406FC8
  name  get=0x0B406B40  set=0x0B407304
  normal  get=0x0B406C14  set=0x0B407330
  hover  get=0x0B40698C  set=0x0B40727C
  active  get=0x0B406694  set=0x0B406FE4
  onNormal  get=0x0B406D30  set=0x0B4073C0
  onHover  get=0x0B406CE8  set=0x0B40739C
  onActive  get=0x0B406C58  set=0x0B407354
  focused  get=0x0B4068A8  set=0x0B407194
  onFocused  get=0x0B406CA0  set=0x0B407378
  border  get=0x0B406710  set=0x0B407048
  margin  get=0x0B406AAC  set=0x0B4072E0
  padding  get=0x0B406E0C  set=0x0B407408
  overflow  get=0x0B406D78  set=0x0B4073E4
  lineHeight  get=0x0B406A58
  none  get=0x0B406B74
  isHeightDependantOnWidth  get=0x0B406A08
METHODS:
  RVA=0x0B405EF0  token=0x600016D  System.IntPtr Internal_Create(UnityEngine.GUIStyle self)
  RVA=0x0B405EAC  token=0x600016E  System.IntPtr Internal_Copy(UnityEngine.GUIStyle self, UnityEngine.GUIStyle other)
  RVA=0x0B405F24  token=0x600016F  System.Void Internal_Destroy(System.IntPtr self)
  RVA=0x0B405C94  token=0x6000170  System.IntPtr GetStyleStatePtr(System.Int32 idx)
  RVA=0x0B4052C8  token=0x6000171  System.Void AssignStyleState(System.Int32 idx, System.IntPtr srcStyleState)
  RVA=0x0B405C54  token=0x6000172  System.IntPtr GetRectOffsetPtr(System.Int32 idx)
  RVA=0x0B405278  token=0x6000173  System.Void AssignRectOffset(System.Int32 idx, System.IntPtr srcRectOffset)
  RVA=0x0B40631C  token=0x6000174  System.Single Internal_GetLineHeight(System.IntPtr target)
  RVA=0x0B4061A0  token=0x6000175  System.Void Internal_Draw(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus)
  RVA=0x0B405FBC  token=0x6000176  System.Void Internal_Draw2(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on)
  RVA=0x0B406040  token=0x6000177  System.Void Internal_DrawCursor(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 pos, UnityEngine.Color cursorColor)
  RVA=0x0B4060CC  token=0x6000178  System.Void Internal_DrawWithTextSelection(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus, System.Boolean drawSelectionAsComposition, System.Int32 cursorFirst, System.Int32 cursorLast, UnityEngine.Color cursorColor, UnityEngine.Color selectionColor)
  RVA=0x0B406268  token=0x6000179  UnityEngine.Vector2 Internal_GetCursorPixelPosition(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex)
  RVA=0x0B4062F8  token=0x600017A  System.Int32 Internal_GetCursorStringIndex(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.Vector2 cursorPixelPosition)
  RVA=0x0B4063B4  token=0x600017B  System.String Internal_GetSelectedRenderedText(UnityEngine.Rect localPosition, UnityEngine.GUIContent mContent, System.Int32 selectIndex, System.Int32 cursorIndex)
  RVA=0x0B405E88  token=0x600017C  UnityEngine.Vector2 Internal_CalcSize(UnityEngine.GUIContent content)
  RVA=0x0B405E04  token=0x600017D  UnityEngine.Vector2 Internal_CalcSizeWithConstraints(UnityEngine.GUIContent content, UnityEngine.Vector2 maxSize)
  RVA=0x0B405CD4  token=0x600017E  System.Single Internal_CalcHeight(UnityEngine.GUIContent content, System.Single width)
  RVA=0x0B405D7C  token=0x600017F  UnityEngine.Vector2 Internal_CalcMinMaxWidth(UnityEngine.GUIContent content)
  RVA=0x0B406480  token=0x6000180  System.Void SetMouseTooltip(System.String tooltip, UnityEngine.Rect screenRect)
  RVA=0x0B4063D4  token=0x6000181  System.Boolean IsTooltipActive(System.String tooltip)
  RVA=0x0B4061D8  token=0x6000182  System.Single Internal_GetCursorFlashOffset()
  RVA=0x0B406408  token=0x6000183  System.Void SetDefaultFont(UnityEngine.Font font)
  RVA=0x0B406644  token=0x6000184  System.Void .ctor()
  RVA=0x0B4065AC  token=0x6000185  System.Void .ctor(UnityEngine.GUIStyle other)
  RVA=0x0B405B54  token=0x6000186  System.Void Finalize()
  RVA=0x0B4059E0  token=0x60001A2  System.Void Draw(UnityEngine.Rect position, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus)
  RVA=0x0B405998  token=0x60001A3  System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus)
  RVA=0x0B405A88  token=0x60001A4  System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID)
  RVA=0x0B4058EC  token=0x60001A5  System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on)
  RVA=0x0B405ABC  token=0x60001A6  System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on, System.Boolean hover)
  RVA=0x0B40592C  token=0x60001A7  System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlId, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus)
  RVA=0x0B405388  token=0x60001A8  System.Void DrawCursor(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 character)
  RVA=0x0B40554C  token=0x60001A9  System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Boolean isActive, System.Boolean hasKeyboardFocus, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter, System.Boolean drawSelectionAsComposition, UnityEngine.Color selectionColor)
  RVA=0x0B4057A0  token=0x60001AA  System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter, System.Boolean drawSelectionAsComposition)
  RVA=0x0B405760  token=0x60001AB  System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter)
  RVA=0x0B405C0C  token=0x60001AD  UnityEngine.Vector2 GetCursorPixelPosition(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex)
  RVA=0x0B405C30  token=0x60001AE  System.Int32 GetCursorStringIndex(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.Vector2 cursorPixelPosition)
  RVA=0x0B405374  token=0x60001AF  UnityEngine.Vector2 CalcSize(UnityEngine.GUIContent content)
  RVA=0x0B405360  token=0x60001B0  UnityEngine.Vector2 CalcSizeWithConstraints(UnityEngine.GUIContent content, UnityEngine.Vector2 constraints)
  RVA=0x0B405318  token=0x60001B1  System.Single CalcHeight(UnityEngine.GUIContent content, System.Single width)
  RVA=0x0B405324  token=0x60001B3  System.Void CalcMinMaxWidth(UnityEngine.GUIContent content, System.Single& minWidth, System.Single& maxWidth)
  RVA=0x0B4064DC  token=0x60001B4  System.String ToString()
  RVA=0x0B406574  token=0x60001B5  System.Void .cctor()
  RVA=0x0B4067D8  token=0x60001B6  System.Void get_contentOffset_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B4070AC  token=0x60001B7  System.Void set_contentOffset_Injected(UnityEngine.Vector2& value)
  RVA=0x0B406F84  token=0x60001B8  System.Void set_Internal_clipOffset_Injected(UnityEngine.Vector2& value)
  RVA=0x0B40613C  token=0x60001B9  System.Void Internal_Draw_Injected(UnityEngine.Rect& screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus)
  RVA=0x0B405F58  token=0x60001BA  System.Void Internal_Draw2_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on)
  RVA=0x0B405FDC  token=0x60001BB  System.Void Internal_DrawCursor_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 pos, UnityEngine.Color& cursorColor)
  RVA=0x0B406064  token=0x60001BC  System.Void Internal_DrawWithTextSelection_Injected(UnityEngine.Rect& screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus, System.Boolean drawSelectionAsComposition, System.Int32 cursorFirst, System.Int32 cursorLast, UnityEngine.Color& cursorColor, UnityEngine.Color& selectionColor)
  RVA=0x0B406204  token=0x60001BD  System.Void Internal_GetCursorPixelPosition_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex, UnityEngine.Vector2& ret)
  RVA=0x0B406294  token=0x60001BE  System.Int32 Internal_GetCursorStringIndex_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, UnityEngine.Vector2& cursorPixelPosition)
  RVA=0x0B406350  token=0x60001BF  System.String Internal_GetSelectedRenderedText_Injected(UnityEngine.Rect& localPosition, UnityEngine.GUIContent mContent, System.Int32 selectIndex, System.Int32 cursorIndex)
  RVA=0x0B405E34  token=0x60001C0  System.Void Internal_CalcSize_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& ret)
  RVA=0x0B405DA0  token=0x60001C1  System.Void Internal_CalcSizeWithConstraints_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& maxSize, UnityEngine.Vector2& ret)
  RVA=0x0B405D28  token=0x60001C2  System.Void Internal_CalcMinMaxWidth_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& ret)
  RVA=0x0B40643C  token=0x60001C3  System.Void SetMouseTooltip_Injected(System.String tooltip, UnityEngine.Rect& screenRect)
END_CLASS

CLASS: UnityEngine.ImagePosition
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.ImagePosition       ImageLeft  // const
  public    static  UnityEngine.ImagePosition       ImageAbove  // const
  public    static  UnityEngine.ImagePosition       ImageOnly  // const
  public    static  UnityEngine.ImagePosition       TextOnly  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextClipping
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextClipping        Overflow  // const
  public    static  UnityEngine.TextClipping        Clip  // const
METHODS:
END_CLASS

CLASS: UnityEngine.GUITargetAttribute
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Int32                    displayMask  // 0x10
METHODS:
  RVA=0x0B407580  token=0x60001C4  System.Int32 GetGUITargetAttrValue(System.Type klass, System.String methodName)
END_CLASS

CLASS: UnityEngine.GUIUtility
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    s_ControlCount  // static @ 0x0
  private   static  System.Int32                    s_SkinMode  // static @ 0x4
  private   static  System.Int32                    s_OriginalID  // static @ 0x8
  private   static  System.Action                   takeCapture  // static @ 0x10
  private   static  System.Action                   releaseCapture  // static @ 0x18
  private   static  System.Func<System.Int32,System.IntPtr,System.Boolean>processEvent  // static @ 0x20
  private   static  System.Action                   cleanupRoots  // static @ 0x28
  private   static  System.Func<System.Exception,System.Boolean>endContainerGUIFromException  // static @ 0x30
  private   static  System.Action                   guiChanged  // static @ 0x38
  private   static  System.Boolean                  <guiIsExiting>k__BackingField  // static @ 0x40
  private   static  System.Func<System.Boolean>     s_HasCurrentWindowKeyFocusFunc  // static @ 0x48
PROPERTIES:
  hasModalWindow  get=0x0B408D5C
  pixelsPerPoint  get=0x0B408E24
  guiDepth  get=0x0B408CF8
  s_EditorScreenPointOffset  get=0x0B408E84  set=0x0B40906C
  mouseUsed  get=0x0B408DF8  set=0x0B409004
  textFieldInput  get=0x0B408ED4  set=0x0B4090BC
  manualTex2SRGBEnabled  get=0x0B408DCC  set=0x0B408FD0
  systemCopyBuffer  get=0x0B408EA8  set=0x0B409088
  compositionString  get=0x0B408CCC
  imeCompositionMode  get=0x0B408D94  set=0x0B408F94
  compositionCursorPos  get=0x0B408CA8  set=0x0B408F34
  guiIsExiting  get=0x0B408D24  set=0x0B408F50
  hotControl  get=0x0B408D88  set=0x0B408F8C
  keyboardControl  get=0x0B408DC0  set=0x0B408FC8
METHODS:
  RVA=0x0B40814C  token=0x60001D2  System.Int32 Internal_GetControlID(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect rect)
  RVA=0x0B407C50  token=0x60001D3  System.Int32 GetControlID(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect rect)
  RVA=0x0B407788  token=0x60001D4  System.Void BeginContainerFromOwner(UnityEngine.ScriptableObject owner)
  RVA=0x0B4077BC  token=0x60001D5  System.Void BeginContainer(UnityEngine.ObjectGUIState objectGUIState)
  RVA=0x0B408070  token=0x60001D6  System.Void Internal_EndContainer()
  RVA=0x0B407D98  token=0x60001D7  System.Int32 GetPermanentControlID()
  RVA=0x0B407850  token=0x60001D8  System.Int32 CheckForTabEvent(UnityEngine.Event evt)
  RVA=0x0B408BCC  token=0x60001D9  System.Void SetKeyboardControlToFirstControlId()
  RVA=0x0B408BF8  token=0x60001DA  System.Void SetKeyboardControlToLastControlId()
  RVA=0x0B407E1C  token=0x60001DB  System.Boolean HasFocusableControls()
  RVA=0x0B408394  token=0x60001DC  System.Boolean OwnsId(System.Int32 id)
  RVA=0x0B4076FC  token=0x60001DD  UnityEngine.Rect AlignRectToDevice(UnityEngine.Rect rect, System.Int32& widthInPixels, System.Int32& heightInPixels)
  RVA=0x0B408240  token=0x60001E3  UnityEngine.Vector3 Internal_MultiplyPoint(UnityEngine.Vector3 point, UnityEngine.Matrix4x4 transform)
  RVA=0x0B407BE0  token=0x60001E4  System.Boolean GetChanged()
  RVA=0x0B408B64  token=0x60001E5  System.Void SetChanged(System.Boolean changed)
  RVA=0x0B408B98  token=0x60001E6  System.Void SetDidGUIWindowsEatLastEvent(System.Boolean value)
  RVA=0x0B408194  token=0x60001E7  System.Int32 Internal_GetHotControl()
  RVA=0x0B4081C0  token=0x60001E8  System.Int32 Internal_GetKeyboardControl()
  RVA=0x0B408274  token=0x60001E9  System.Void Internal_SetHotControl(System.Int32 value)
  RVA=0x0B4082A8  token=0x60001EA  System.Void Internal_SetKeyboardControl(System.Int32 value)
  RVA=0x0B408160  token=0x60001EB  System.Object Internal_GetDefaultSkin(System.Int32 skinMode)
  RVA=0x0B4080C8  token=0x60001EC  UnityEngine.Object Internal_GetBuiltinSkin(System.Int32 skin)
  RVA=0x0B40809C  token=0x60001ED  System.Void Internal_ExitGUI()
  RVA=0x0B408044  token=0x60001EE  UnityEngine.Vector2 InternalWindowToScreenPoint(UnityEngine.Vector2 windowPoint)
  RVA=0x0B407FD4  token=0x60001EF  UnityEngine.Vector2 InternalScreenToWindowPoint(UnityEngine.Vector2 screenPoint)
  RVA=0x0B408350  token=0x60001F0  System.Void MarkGUIChanged()
  RVA=0x0B407C88  token=0x60001F1  System.Int32 GetControlID(UnityEngine.FocusType focus)
  RVA=0x0B407C98  token=0x60001F2  System.Int32 GetControlID(UnityEngine.GUIContent contents, UnityEngine.FocusType focus)
  RVA=0x0B407C0C  token=0x60001F3  System.Int32 GetControlID(UnityEngine.FocusType focus, UnityEngine.Rect position)
  RVA=0x0B407CC4  token=0x60001F4  System.Int32 GetControlID(UnityEngine.GUIContent contents, UnityEngine.FocusType focus, UnityEngine.Rect position)
  RVA=0x0B407C30  token=0x60001F5  System.Int32 GetControlID(System.Int32 hint, UnityEngine.FocusType focus)
  RVA=0x0B407DC4  token=0x60001F6  System.Object GetStateObject(System.Type t, System.Int32 controlID)
  RVA=0x0B40843C  token=0x60001F7  System.Object QueryStateObject(System.Type t, System.Int32 controlID)
  RVA=0x0B408C30  token=0x60001FC  System.Void TakeCapture()
  RVA=0x0B408494  token=0x60001FD  System.Void RemoveCapture()
  RVA=0x0B407E48  token=0x6000200  System.Boolean HasKeyFocus(System.Int32 controlID)
  RVA=0x0B407AC8  token=0x6000201  System.Void ExitGUI()
  RVA=0x0B407D0C  token=0x6000202  UnityEngine.GUISkin GetDefaultSkin(System.Int32 skinMode)
  RVA=0x0B407D5C  token=0x6000203  UnityEngine.GUISkin GetDefaultSkin()
  RVA=0x0B407B90  token=0x6000204  UnityEngine.GUISkin GetBuiltinSkin(System.Int32 skin)
  RVA=0x0B4083C8  token=0x6000205  System.Void ProcessEvent(System.Int32 instanceID, System.IntPtr nativeEventPtr, System.Boolean& result)
  RVA=0x0B407990  token=0x6000206  System.Void EndContainer()
  RVA=0x0B4078F0  token=0x6000207  System.Void CleanupRoots()
  RVA=0x0B4077F0  token=0x6000208  System.Void BeginGUI(System.Int32 skinMode, System.Int32 instanceID, System.Int32 useGUILayout)
  RVA=0x0B4079C8  token=0x6000209  System.Void EndGUI(System.Int32 layoutType)
  RVA=0x0B4079A8  token=0x600020A  System.Boolean EndGUIFromException(System.Exception exception)
  RVA=0x0B407934  token=0x600020B  System.Boolean EndContainerGUIFromException(System.Exception exception)
  RVA=0x0B4084D8  token=0x600020C  System.Void ResetGlobalState()
  RVA=0x0B4082DC  token=0x600020D  System.Boolean IsExitGUIException(System.Exception exception)
  RVA=0x0B408C24  token=0x600020E  System.Boolean ShouldRethrowException(System.Exception exception)
  RVA=0x0B407884  token=0x600020F  System.Void CheckOnGUI()
  RVA=0x0B408804  token=0x6000210  System.Single RoundToPixelGrid(System.Single v)
  RVA=0x0B407B14  token=0x6000211  UnityEngine.Vector2 GUIToScreenPoint(UnityEngine.Vector2 guiPoint)
  RVA=0x0B407B30  token=0x6000212  UnityEngine.Rect GUIToScreenRect(UnityEngine.Rect guiRect)
  RVA=0x0B408AE8  token=0x6000213  UnityEngine.Vector2 ScreenToGUIPoint(UnityEngine.Vector2 screenPoint)
  RVA=0x0B408B04  token=0x6000214  UnityEngine.Rect ScreenToGUIRect(UnityEngine.Rect screenRect)
  RVA=0x0B408520  token=0x6000215  System.Void RotateAroundPivot(System.Single angle, UnityEngine.Vector2 pivotPoint)
  RVA=0x0B408844  token=0x6000216  System.Void ScaleAroundPivot(UnityEngine.Vector2 scale, UnityEngine.Vector2 pivotPoint)
  RVA=0x0B407738  token=0x6000217  UnityEngine.Rect AlignRectToDevice(UnityEngine.Rect rect)
  RVA=0x0B407EF4  token=0x6000218  System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Vector2 point, System.Int32 offset)
  RVA=0x0B407F6C  token=0x6000219  System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Vector2 point, System.Boolean isDirectManipulationDevice)
  RVA=0x0B407E8C  token=0x600021A  System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Event evt)
  RVA=0x041E1670  token=0x600021B  System.Void .ctor()
  RVA=0x0B408E50  token=0x600021C  System.Void get_s_EditorScreenPointOffset_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B409038  token=0x600021D  System.Void set_s_EditorScreenPointOffset_Injected(UnityEngine.Vector2& value)
  RVA=0x0B4080FC  token=0x600021E  System.Int32 Internal_GetControlID_Injected(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect& rect)
  RVA=0x0B407698  token=0x600021F  System.Void AlignRectToDevice_Injected(UnityEngine.Rect& rect, System.Int32& widthInPixels, System.Int32& heightInPixels, UnityEngine.Rect& ret)
  RVA=0x0B408C74  token=0x6000220  System.Void get_compositionCursorPos_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B408F00  token=0x6000221  System.Void set_compositionCursorPos_Injected(UnityEngine.Vector2& value)
  RVA=0x0B4081EC  token=0x6000222  System.Void Internal_MultiplyPoint_Injected(UnityEngine.Vector3& point, UnityEngine.Matrix4x4& transform, UnityEngine.Vector3& ret)
  RVA=0x0B408000  token=0x6000223  System.Void InternalWindowToScreenPoint_Injected(UnityEngine.Vector2& windowPoint, UnityEngine.Vector2& ret)
  RVA=0x0B407F90  token=0x6000224  System.Void InternalScreenToWindowPoint_Injected(UnityEngine.Vector2& screenPoint, UnityEngine.Vector2& ret)
END_CLASS

CLASS: UnityEngine.ExitGUIException
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B3FE8D0  token=0x6000225  System.Void .ctor()
  RVA=0x0B3FE920  token=0x6000226  System.Void .ctor(System.String message)
END_CLASS

CLASS: UnityEngine.GUILayoutEntry
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x48
FIELDS:
  public            System.Single                   minWidth  // 0x10
  public            System.Single                   maxWidth  // 0x14
  public            System.Single                   minHeight  // 0x18
  public            System.Single                   maxHeight  // 0x1c
  public            UnityEngine.Rect                rect  // 0x20
  public            System.Int32                    stretchWidth  // 0x30
  public            System.Int32                    stretchHeight  // 0x34
  public            System.Boolean                  consideredForMargin  // 0x38
  private           UnityEngine.GUIStyle            m_Style  // 0x40
  private   static  UnityEngine.Rect                kDummyRect  // static @ 0x0
  protected static  System.Int32                    indent  // static @ 0x10
PROPERTIES:
  style  get=0x04DBCF90  set=0x0B3FFD50
  marginLeft  get=0x0B3FFC88
  marginRight  get=0x0B3FFCB8
  marginTop  get=0x0B3FFCE8
  marginBottom  get=0x0B3FFC20
  marginHorizontal  get=0x0B3FFC50
  marginVertical  get=0x0B3FFD18
METHODS:
  RVA=0x0B3FFA90  token=0x600022F  System.Void .ctor(System.Single _minWidth, System.Single _maxWidth, System.Single _minHeight, System.Single _maxHeight, UnityEngine.GUIStyle _style)
  RVA=0x0B3FFB5C  token=0x6000230  System.Void .ctor(System.Single _minWidth, System.Single _maxWidth, System.Single _minHeight, System.Single _maxHeight, UnityEngine.GUIStyle _style, UnityEngine.GUILayoutOption[] options)
  RVA=0x041E1670  token=0x6000231  System.Void CalcWidth()
  RVA=0x041E1670  token=0x6000232  System.Void CalcHeight()
  RVA=0x04DBCF70  token=0x6000233  System.Void SetHorizontal(System.Single x, System.Single width)
  RVA=0x04DBCF80  token=0x6000234  System.Void SetVertical(System.Single y, System.Single height)
  RVA=0x0B3FF5E0  token=0x6000235  System.Void ApplyStyleSettings(UnityEngine.GUIStyle style)
  RVA=0x0B3FF3B0  token=0x6000236  System.Void ApplyOptions(UnityEngine.GUILayoutOption[] options)
  RVA=0x0B3FF67C  token=0x6000237  System.String ToString()
  RVA=0x0B3FFA3C  token=0x6000238  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.GUIWordWrapSizer
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x58
EXTENDS: UnityEngine.GUILayoutEntry
FIELDS:
  private   readonly UnityEngine.GUIContent          m_Content  // 0x48
  private   readonly System.Single                   m_ForcedMinHeight  // 0x50
  private   readonly System.Single                   m_ForcedMaxHeight  // 0x54
METHODS:
  RVA=0x0B411F60  token=0x6000239  System.Void .ctor(UnityEngine.GUIStyle style, UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
  RVA=0x0B411EB4  token=0x600023A  System.Void CalcWidth()
  RVA=0x0B411E34  token=0x600023B  System.Void CalcHeight()
END_CLASS

CLASS: UnityEngine.GUILayoutGroup
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x90
EXTENDS: UnityEngine.GUILayoutEntry
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.GUILayoutEntry>entries  // 0x48
  public            System.Boolean                  isVertical  // 0x50
  public            System.Boolean                  resetCoords  // 0x51
  public            System.Single                   spacing  // 0x54
  public            System.Boolean                  sameSize  // 0x58
  public            System.Boolean                  isWindow  // 0x59
  public            System.Int32                    windowID  // 0x5c
  private           System.Int32                    m_Cursor  // 0x60
  protected         System.Int32                    m_StretchableCountX  // 0x64
  protected         System.Int32                    m_StretchableCountY  // 0x68
  protected         System.Boolean                  m_UserSpecifiedWidth  // 0x6c
  protected         System.Boolean                  m_UserSpecifiedHeight  // 0x6d
  protected         System.Single                   m_ChildMinWidth  // 0x70
  protected         System.Single                   m_ChildMaxWidth  // 0x74
  protected         System.Single                   m_ChildMinHeight  // 0x78
  protected         System.Single                   m_ChildMaxHeight  // 0x7c
  protected         System.Int32                    m_MarginLeft  // 0x80
  protected         System.Int32                    m_MarginRight  // 0x84
  protected         System.Int32                    m_MarginTop  // 0x88
  protected         System.Int32                    m_MarginBottom  // 0x8c
  private   static readonly UnityEngine.GUILayoutEntry      none  // static @ 0x0
PROPERTIES:
  marginLeft  get=0x04D863A0
  marginRight  get=0x04D8FCE0
  marginTop  get=0x04D86890
  marginBottom  get=0x04D90C40
METHODS:
  RVA=0x0B4117E4  token=0x6000240  System.Void .ctor()
  RVA=0x0B40F0CC  token=0x6000241  System.Void ApplyOptions(UnityEngine.GUILayoutOption[] options)
  RVA=0x0B40F19C  token=0x6000242  System.Void ApplyStyleSettings(UnityEngine.GUIStyle style)
  RVA=0x04DBD110  token=0x6000243  System.Void ResetCursor()
  RVA=0x0B410220  token=0x6000244  UnityEngine.GUILayoutEntry GetNext()
  RVA=0x0B40FF84  token=0x6000245  UnityEngine.Rect GetLast()
  RVA=0x0B40F078  token=0x6000246  System.Void Add(UnityEngine.GUILayoutEntry e)
  RVA=0x0B40F898  token=0x6000247  System.Void CalcWidth()
  RVA=0x0B410480  token=0x6000248  System.Void SetHorizontal(System.Single x, System.Single width)
  RVA=0x0B40F21C  token=0x6000249  System.Void CalcHeight()
  RVA=0x0B410BE8  token=0x600024A  System.Void SetVertical(System.Single y, System.Single height)
  RVA=0x0B4113C8  token=0x600024B  System.String ToString()
  RVA=0x0B411714  token=0x600024C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.GUIScrollGroup
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0xC0
EXTENDS: UnityEngine.GUILayoutGroup
FIELDS:
  public            System.Single                   calcMinWidth  // 0x90
  public            System.Single                   calcMaxWidth  // 0x94
  public            System.Single                   calcMinHeight  // 0x98
  public            System.Single                   calcMaxHeight  // 0x9c
  public            System.Single                   clientWidth  // 0xa0
  public            System.Single                   clientHeight  // 0xa4
  public            System.Boolean                  allowHorizontalScroll  // 0xa8
  public            System.Boolean                  allowVerticalScroll  // 0xa9
  public            System.Boolean                  needsHorizontalScrollbar  // 0xaa
  public            System.Boolean                  needsVerticalScrollbar  // 0xab
  public            UnityEngine.GUIStyle            horizontalScrollbar  // 0xb0
  public            UnityEngine.GUIStyle            verticalScrollbar  // 0xb8
METHODS:
  RVA=0x0B411DE0  token=0x600024D  System.Void .ctor()
  RVA=0x0B411A08  token=0x600024E  System.Void CalcWidth()
  RVA=0x0B411A9C  token=0x600024F  System.Void SetHorizontal(System.Single x, System.Single width)
  RVA=0x0B4118F0  token=0x6000250  System.Void CalcHeight()
  RVA=0x0B411BD4  token=0x6000251  System.Void SetVertical(System.Single y, System.Single height)
END_CLASS

CLASS: UnityEngine.ObjectGUIState
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
METHODS:
  RVA=0x0B412180  token=0x6000252  System.Void .ctor()
  RVA=0x0B412064  token=0x6000253  System.Void Dispose()
  RVA=0x0B4120B0  token=0x6000254  System.Void Finalize()
  RVA=0x0B412038  token=0x6000255  System.Void Destroy()
  RVA=0x0B412120  token=0x6000256  System.IntPtr Internal_Create()
  RVA=0x0B41214C  token=0x6000257  System.Void Internal_Destroy(System.IntPtr ptr)
END_CLASS

CLASS: UnityEngine.ScrollViewState
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x68
FIELDS:
  public            UnityEngine.Rect                position  // 0x10
  public            UnityEngine.Rect                visibleRect  // 0x20
  public            UnityEngine.Rect                viewRect  // 0x30
  public            UnityEngine.Vector2             scrollPosition  // 0x40
  public            System.Boolean                  apply  // 0x48
  public            System.Boolean                  isDuringTouchScroll  // 0x49
  public            UnityEngine.Vector2             touchScrollStartMousePosition  // 0x4c
  public            UnityEngine.Vector2             touchScrollStartPosition  // 0x54
  public            UnityEngine.Vector2             velocity  // 0x5c
  public            System.Single                   previousTimeSinceStartup  // 0x64
METHODS:
  RVA=0x041E1670  token=0x6000258  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.SliderState
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x20
FIELDS:
  public            System.Single                   dragStartPos  // 0x10
  public            System.Single                   dragStartValue  // 0x14
  public            System.Boolean                  isDragging  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000259  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.SliderHandler
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x50
FIELDS:
  private   readonly UnityEngine.Rect                position  // 0x10
  private   readonly System.Single                   currentValue  // 0x20
  private   readonly System.Single                   size  // 0x24
  private   readonly System.Single                   start  // 0x28
  private   readonly System.Single                   end  // 0x2c
  private   readonly UnityEngine.GUIStyle            slider  // 0x30
  private   readonly UnityEngine.GUIStyle            thumb  // 0x38
  private   readonly UnityEngine.GUIStyle            thumbExtent  // 0x40
  private   readonly System.Boolean                  horiz  // 0x48
  private   readonly System.Int32                    id  // 0x4c
METHODS:
  RVA=0x0B413534  token=0x600025A  System.Void .ctor(UnityEngine.Rect position, System.Single currentValue, System.Single size, System.Single start, System.Single end, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, System.Boolean horiz, System.Int32 id, UnityEngine.GUIStyle thumbExtent)
  RVA=0x0B4122B8  token=0x600025B  System.Single Handle()
  RVA=0x0B4125A4  token=0x600025C  System.Single OnMouseDown()
  RVA=0x0B412910  token=0x600025D  System.Single OnMouseDrag()
  RVA=0x0B4129F8  token=0x600025E  System.Single OnMouseUp()
  RVA=0x0B412A44  token=0x600025F  System.Single OnRepaint()
  RVA=0x0B4121DC  token=0x6000260  UnityEngine.EventType CurrentEventType()
  RVA=0x0B412210  token=0x6000261  System.Int32 CurrentScrollTroughSide()
  RVA=0x0B4124F4  token=0x6000262  System.Boolean IsEmptySlider()
  RVA=0x0B412F98  token=0x6000263  System.Boolean SupportsPageMovements()
  RVA=0x0B412DCC  token=0x6000264  System.Single PageMovementValue()
  RVA=0x0B412E5C  token=0x6000265  System.Single PageUpMovementBound()
  RVA=0x04DBCF50  token=0x6000266  UnityEngine.Event CurrentEvent()
  RVA=0x0B4131F8  token=0x6000267  System.Single ValueForCurrentMousePosition()
  RVA=0x0B41219C  token=0x6000268  System.Single Clamp(System.Single value)
  RVA=0x0B413110  token=0x6000269  UnityEngine.Rect ThumbSelectionRect()
  RVA=0x0B412F44  token=0x600026A  System.Void StartDraggingWithValue(System.Single dragStartValue)
  RVA=0x0B412EBC  token=0x600026B  UnityEngine.SliderState SliderState()
  RVA=0x0B412FF4  token=0x600026C  UnityEngine.Rect ThumbExtRect()
  RVA=0x0B4130D4  token=0x600026D  UnityEngine.Rect ThumbRect()
  RVA=0x0B413380  token=0x600026E  UnityEngine.Rect VerticalThumbRect()
  RVA=0x0B412340  token=0x600026F  UnityEngine.Rect HorizontalThumbRect()
  RVA=0x0B4121CC  token=0x6000270  System.Single ClampedCurrentValue()
  RVA=0x0B41252C  token=0x6000271  System.Single MousePosition()
  RVA=0x0B4132C8  token=0x6000272  System.Single ValuesPerPixel()
  RVA=0x0B41313C  token=0x6000273  System.Single ThumbSize()
  RVA=0x0B412508  token=0x6000274  System.Single MaxValue()
  RVA=0x0B41251C  token=0x6000275  System.Single MinValue()
END_CLASS

CLASS: UnityEngine.TextEditor
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x90
FIELDS:
  public            UnityEngine.TouchScreenKeyboard keyboardOnScreen  // 0x10
  public            System.Int32                    controlID  // 0x18
  public            UnityEngine.GUIStyle            style  // 0x20
  public            System.Boolean                  multiline  // 0x28
  public            System.Boolean                  hasHorizontalCursorPos  // 0x29
  public            System.Boolean                  isPasswordField  // 0x2a
  private           System.Boolean                  m_HasFocus  // 0x2b
  public            UnityEngine.Vector2             scrollOffset  // 0x2c
  private           UnityEngine.GUIContent          m_Content  // 0x38
  private           UnityEngine.Rect                m_Position  // 0x40
  private           System.Int32                    m_CursorIndex  // 0x50
  private           System.Int32                    m_SelectIndex  // 0x54
  private           System.Boolean                  m_RevealCursor  // 0x58
  public            UnityEngine.Vector2             graphicalCursorPos  // 0x5c
  public            UnityEngine.Vector2             graphicalSelectCursorPos  // 0x64
  private           System.Boolean                  m_MouseDragSelectsWholeWords  // 0x6c
  private           System.Int32                    m_DblClickInitPos  // 0x70
  private           UnityEngine.TextEditor.DblClickSnappingm_DblClickSnap  // 0x74
  private           System.Boolean                  m_bJustSelected  // 0x75
  private           System.Int32                    m_iAltCursorPos  // 0x78
  private           System.String                   oldText  // 0x80
  private           System.Int32                    oldPos  // 0x88
  private           System.Int32                    oldSelectPos  // 0x8c
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Event,UnityEngine.TextEditor.TextEditOp>s_Keyactions  // static @ 0x0
PROPERTIES:
  text  get=0x0A97F198  set=0x0B4176C4
  position  get=0x04DBD160  set=0x0B41762C
  localPosition  get=0x04DBD150
  cursorIndex  get=0x04DBA760  set=0x0B4175EC
  selectIndex  get=0x04DBD170  set=0x0B417688
  altCursorPosition  get=0x04DBD140
  hasSelection  get=0x0B4175E0
  SelectedText  get=0x0B417558
METHODS:
  RVA=0x04DBD120  token=0x600027F  System.Void ClearCursorPos()
  RVA=0x0B417488  token=0x6000281  System.Void .ctor()
  RVA=0x0B415FCC  token=0x6000282  System.Void OnFocus()
  RVA=0x0B416008  token=0x6000283  System.Void OnLostFocus()
  RVA=0x0B414818  token=0x6000284  System.Void GrabGraphicalCursorPos()
  RVA=0x0B414A1C  token=0x6000285  System.Boolean HandleKeyEvent(UnityEngine.Event e)
  RVA=0x0B4148FC  token=0x6000286  System.Boolean HandleKeyEvent(UnityEngine.Event e, System.Boolean textIsReadOnly)
  RVA=0x0B413854  token=0x6000287  System.Boolean DeleteLineBack()
  RVA=0x0B413ADC  token=0x6000288  System.Boolean DeleteWordBack()
  RVA=0x0B413B90  token=0x6000289  System.Boolean DeleteWordForward()
  RVA=0x0B413C3C  token=0x600028A  System.Boolean Delete()
  RVA=0x0B413670  token=0x600028B  System.Boolean CanPaste()
  RVA=0x0B4135B4  token=0x600028C  System.Boolean Backspace()
  RVA=0x0B41673C  token=0x600028D  System.Void SelectAll()
  RVA=0x0B416A10  token=0x600028E  System.Void SelectNone()
  RVA=0x0B413948  token=0x6000291  System.Boolean DeleteSelection()
  RVA=0x0B416668  token=0x6000292  System.Void ReplaceSelection(System.String replace)
  RVA=0x0B41536C  token=0x6000293  System.Void Insert(System.Char c)
  RVA=0x0B415B3C  token=0x6000294  System.Void MoveSelectionToAltCursor()
  RVA=0x0B415ADC  token=0x6000295  System.Void MoveRight()
  RVA=0x0B41585C  token=0x6000296  System.Void MoveLeft()
  RVA=0x0B415D3C  token=0x6000297  System.Void MoveUp()
  RVA=0x0B4156C4  token=0x6000298  System.Void MoveDown()
  RVA=0x0B415948  token=0x6000299  System.Void MoveLineStart()
  RVA=0x0B4158B0  token=0x600029A  System.Void MoveLineEnd()
  RVA=0x0B415810  token=0x600029B  System.Void MoveGraphicalLineStart()
  RVA=0x0B4157C4  token=0x600029C  System.Void MoveGraphicalLineEnd()
  RVA=0x0B415C64  token=0x600029D  System.Void MoveTextStart()
  RVA=0x0B415C1C  token=0x600029E  System.Void MoveTextEnd()
  RVA=0x0B414A2C  token=0x600029F  System.Int32 IndexOfEndOfLine(System.Int32 startIndex)
  RVA=0x0B415A64  token=0x60002A0  System.Void MoveParagraphForward()
  RVA=0x0B4159D4  token=0x60002A1  System.Void MoveParagraphBackward()
  RVA=0x0B415670  token=0x60002A2  System.Void MoveCursorToPosition(UnityEngine.Vector2 cursorPosition)
  RVA=0x0B4155B0  token=0x60002A3  System.Void MoveCursorToPosition_Internal(UnityEngine.Vector2 cursorPosition, System.Boolean shift)
  RVA=0x0B415500  token=0x60002A4  System.Void MoveAltCursorToPosition(UnityEngine.Vector2 cursorPosition)
  RVA=0x0B416C30  token=0x60002A5  System.Void SelectToPosition(UnityEngine.Vector2 cursorPosition)
  RVA=0x0B4169B0  token=0x60002A6  System.Void SelectLeft()
  RVA=0x0B416B60  token=0x60002A7  System.Void SelectRight()
  RVA=0x0B416F08  token=0x60002A8  System.Void SelectUp()
  RVA=0x0B416894  token=0x60002A9  System.Void SelectDown()
  RVA=0x0B416BC0  token=0x60002AA  System.Void SelectTextEnd()
  RVA=0x0B416BF0  token=0x60002AB  System.Void SelectTextStart()
  RVA=0x04DBD130  token=0x60002AC  System.Void MouseDragSelectsWholeWords(System.Boolean on)
  RVA=0x0157A810  token=0x60002AD  System.Void DblClickSnap(UnityEngine.TextEditor.DblClickSnapping snapping)
  RVA=0x0B41473C  token=0x60002AE  System.Int32 GetGraphicalLineStart(System.Int32 p)
  RVA=0x0B41464C  token=0x60002AF  System.Int32 GetGraphicalLineEnd(System.Int32 p)
  RVA=0x0B4143B0  token=0x60002B0  System.Int32 FindNextSeperator(System.Int32 startPos)
  RVA=0x0B414438  token=0x60002B1  System.Int32 FindPrevSeperator(System.Int32 startPos)
  RVA=0x0B415E5C  token=0x60002B2  System.Void MoveWordRight()
  RVA=0x0B415CE4  token=0x60002B3  System.Void MoveToStartOfNextWord()
  RVA=0x0B415C8C  token=0x60002B4  System.Void MoveToEndOfPreviousWord()
  RVA=0x0B416ED4  token=0x60002B5  System.Void SelectToStartOfNextWord()
  RVA=0x0B416BFC  token=0x60002B6  System.Void SelectToEndOfPreviousWord()
  RVA=0x0B4136D4  token=0x60002B7  UnityEngine.TextEditor.CharacterType ClassifyChar(System.Int32 index)
  RVA=0x0B4144D8  token=0x60002B8  System.Int32 FindStartOfNextWord(System.Int32 p)
  RVA=0x0B4142FC  token=0x60002B9  System.Int32 FindEndOfPreviousWord(System.Int32 p)
  RVA=0x0B415E0C  token=0x60002BA  System.Void MoveWordLeft()
  RVA=0x0B41701C  token=0x60002BB  System.Void SelectWordRight()
  RVA=0x0B416F98  token=0x60002BC  System.Void SelectWordLeft()
  RVA=0x0B414164  token=0x60002BD  System.Void ExpandSelectGraphicalLineStart()
  RVA=0x0B414100  token=0x60002BE  System.Void ExpandSelectGraphicalLineEnd()
  RVA=0x0B41697C  token=0x60002BF  System.Void SelectGraphicalLineStart()
  RVA=0x0B416948  token=0x60002C0  System.Void SelectGraphicalLineEnd()
  RVA=0x0B416AD8  token=0x60002C1  System.Void SelectParagraphForward()
  RVA=0x0B416A38  token=0x60002C2  System.Void SelectParagraphBackward()
  RVA=0x0B416808  token=0x60002C3  System.Void SelectCurrentWord()
  RVA=0x0B4141C8  token=0x60002C4  System.Int32 FindEndOfClassification(System.Int32 p, UnityEngine.TextEditor.Direction dir)
  RVA=0x0B416780  token=0x60002C5  System.Void SelectCurrentParagraph()
  RVA=0x0B4170A0  token=0x60002C6  System.Void UpdateScrollOffsetIfNeeded(UnityEngine.Event evt)
  RVA=0x0B4170F0  token=0x60002C7  System.Void UpdateScrollOffset()
  RVA=0x0B413CF4  token=0x60002C8  System.Void DrawCursor(System.String newText)
  RVA=0x0B416098  token=0x60002C9  System.Boolean PerformOperation(UnityEngine.TextEditor.TextEditOp operation, System.Boolean textIsReadOnly)
  RVA=0x0B416700  token=0x60002CA  System.Void SaveBackup()
  RVA=0x0B413828  token=0x60002CB  System.Boolean Cut()
  RVA=0x0B4137AC  token=0x60002CC  System.Void Copy()
  RVA=0x0B4165DC  token=0x60002CD  System.String ReplaceNewlinesWithSpaces(System.String value)
  RVA=0x0B416014  token=0x60002CE  System.Boolean Paste()
  RVA=0x0B415478  token=0x60002CF  System.Void MapKey(System.String key, UnityEngine.TextEditor.TextEditOp action)
  RVA=0x0B414A84  token=0x60002D0  System.Void InitKeyActions()
  RVA=0x0ABD8608  token=0x60002D1  System.Void DetectFocusChange()
  RVA=0x0B415F74  token=0x60002D2  System.Void OnDetectFocusChange()
  RVA=0x041E1670  token=0x60002D3  System.Void OnCursorIndexChange()
  RVA=0x041E1670  token=0x60002D4  System.Void OnSelectIndexChange()
  RVA=0x0B413694  token=0x60002D5  System.Void ClampTextIndex(System.Int32& index)
  RVA=0x0B4140BC  token=0x60002D6  System.Void EnsureValidCodePointIndex(System.Int32& index)
  RVA=0x0B4153C0  token=0x60002D7  System.Boolean IsValidCodePointIndex(System.Int32 index)
  RVA=0x0B416548  token=0x60002D8  System.Int32 PreviousCodePointIndex(System.Int32 index)
  RVA=0x0B415EC0  token=0x60002D9  System.Int32 NextCodePointIndex(System.Int32 index)
END_CLASS

