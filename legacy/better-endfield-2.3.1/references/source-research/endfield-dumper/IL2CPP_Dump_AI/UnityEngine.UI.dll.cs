// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UI.dll
// Classes:  233
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

CLASS: ButtonClickedEvent
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent
FIELDS:
METHODS:
  RVA=0x05393538  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: <OnFinishSubmit>d__9
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.UI.Button           <>4__this  // 0x20
  private           System.Single                   <fadeTime>5__2  // 0x28
  private           System.Single                   <elapsedTime>5__3  // 0x2c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000016  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000017  System.Void System.IDisposable.Dispose()
  RVA=0x0B46BFB4  token=0x6000018  System.Boolean MoveNext()
  RVA=0x0B46C0A8  token=0x600001A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: IFactoryControls
TYPE:  interface
TOKEN: 0x2000012
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600006D  UnityEngine.GameObject CreateGameObject(System.String name, System.Type[] components)
END_CLASS

CLASS: DefaultRuntimeFactory
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x10
IMPLEMENTS: IFactoryControls
FIELDS:
  public    static  UnityEngine.UI.DefaultControls.IFactoryControlsDefault  // static @ 0x0
METHODS:
  RVA=0x0B464498  token=0x600006E  UnityEngine.GameObject CreateGameObject(System.String name, System.Type[] components)
  RVA=0x041E1670  token=0x600006F  System.Void .ctor()
  RVA=0x0B464508  token=0x6000070  System.Void .cctor()
END_CLASS

CLASS: Resources
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x48
FIELDS:
  public            UnityEngine.Sprite              standard  // 0x10
  public            UnityEngine.Sprite              background  // 0x18
  public            UnityEngine.Sprite              inputField  // 0x20
  public            UnityEngine.Sprite              knob  // 0x28
  public            UnityEngine.Sprite              checkmark  // 0x30
  public            UnityEngine.Sprite              dropdown  // 0x38
  public            UnityEngine.Sprite              mask  // 0x40
METHODS:
END_CLASS

CLASS: DropdownItem
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.ICancelHandler
FIELDS:
  private           UnityEngine.UI.Text             m_Text  // 0x18
  private           UnityEngine.UI.Image            m_Image  // 0x20
  private           UnityEngine.RectTransform       m_RectTransform  // 0x28
  private           UnityEngine.UI.Toggle           m_Toggle  // 0x30
PROPERTIES:
  text  get=0x0385B100  set=0x05392C40
  image  get=0x04D862C0  set=0x05390F40
  rectTransform  get=0x04D86240  set=0x02FDB880
  toggle  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0B464620  token=0x60000AB  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B4645A8  token=0x60000AC  System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x05393520  token=0x60000AD  System.Void .ctor()
END_CLASS

CLASS: OptionData
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x20
FIELDS:
  private           System.String                   m_Text  // 0x10
  private           UnityEngine.Sprite              m_Image  // 0x18
PROPERTIES:
  text  get=0x02B2ECC0  set=0x053908C0
  image  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x041E1670  token=0x60000B2  System.Void .ctor()
  RVA=0x053908C0  token=0x60000B3  System.Void .ctor(System.String text)
  RVA=0x05392C40  token=0x60000B4  System.Void .ctor(UnityEngine.Sprite image)
  RVA=0x032AF420  token=0x60000B5  System.Void .ctor(System.String text, UnityEngine.Sprite image)
END_CLASS

CLASS: OptionDataList
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData>m_Options  // 0x10
PROPERTIES:
  options  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x0B46B9CC  token=0x60000B8  System.Void .ctor()
END_CLASS

CLASS: DropdownEvent
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B46456C  token=0x60000B9  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass63_0
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x20
FIELDS:
  public            UnityEngine.UI.Dropdown.DropdownItemitem  // 0x10
  public            UnityEngine.UI.Dropdown         <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000BA  System.Void .ctor()
  RVA=0x0B46C0F4  token=0x60000BB  System.Void <Show>b__0(System.Boolean x)
END_CLASS

CLASS: <DelayedDestroyDropdownList>d__75
TYPE:  sealed class
TOKEN: 0x200001B
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Single                   delay  // 0x20
  public            UnityEngine.UI.Dropdown         <>4__this  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000BC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000BD  System.Void System.IDisposable.Dispose()
  RVA=0x0B46BECC  token=0x60000BE  System.Boolean MoveNext()
  RVA=0x0B46BF68  token=0x60000C0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: BlockingObjects
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.GraphicRaycaster.BlockingObjectsNone  // const
  public    static  UnityEngine.UI.GraphicRaycaster.BlockingObjectsTwoD  // const
  public    static  UnityEngine.UI.GraphicRaycaster.BlockingObjectsThreeD  // const
  public    static  UnityEngine.UI.GraphicRaycaster.BlockingObjectsAll  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.UI.GraphicRaycaster.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.UI.Graphic><>9__27_0  // static @ 0x8
METHODS:
  RVA=0x04D470B0  token=0x6000135  System.Void .cctor()
  RVA=0x041E1670  token=0x6000136  System.Void .ctor()
  RVA=0x0389F670  token=0x6000137  System.Int32 <Raycast>b__27_0(UnityEngine.UI.Graphic g1, UnityEngine.UI.Graphic g2)
END_CLASS

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.Type       Simple  // const
  public    static  UnityEngine.UI.Image.Type       Sliced  // const
  public    static  UnityEngine.UI.Image.Type       Tiled  // const
  public    static  UnityEngine.UI.Image.Type       Filled  // const
  public    static  UnityEngine.UI.Image.Type       Mirror  // const
  public    static  UnityEngine.UI.Image.Type       Quarter  // const
METHODS:
END_CLASS

CLASS: FillMethod
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.FillMethod Horizontal  // const
  public    static  UnityEngine.UI.Image.FillMethod Vertical  // const
  public    static  UnityEngine.UI.Image.FillMethod Radial90  // const
  public    static  UnityEngine.UI.Image.FillMethod Radial180  // const
  public    static  UnityEngine.UI.Image.FillMethod Radial360  // const
METHODS:
END_CLASS

CLASS: OriginHorizontal
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.OriginHorizontalLeft  // const
  public    static  UnityEngine.UI.Image.OriginHorizontalRight  // const
METHODS:
END_CLASS

CLASS: OriginVertical
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.OriginVerticalBottom  // const
  public    static  UnityEngine.UI.Image.OriginVerticalTop  // const
METHODS:
END_CLASS

CLASS: Origin90
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.Origin90   BottomLeft  // const
  public    static  UnityEngine.UI.Image.Origin90   TopLeft  // const
  public    static  UnityEngine.UI.Image.Origin90   TopRight  // const
  public    static  UnityEngine.UI.Image.Origin90   BottomRight  // const
METHODS:
END_CLASS

CLASS: Origin180
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.Origin180  Bottom  // const
  public    static  UnityEngine.UI.Image.Origin180  Left  // const
  public    static  UnityEngine.UI.Image.Origin180  Top  // const
  public    static  UnityEngine.UI.Image.Origin180  Right  // const
METHODS:
END_CLASS

CLASS: Origin360
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Image.Origin360  Bottom  // const
  public    static  UnityEngine.UI.Image.Origin360  Right  // const
  public    static  UnityEngine.UI.Image.Origin360  Top  // const
  public    static  UnityEngine.UI.Image.Origin360  Left  // const
METHODS:
END_CLASS

CLASS: SlicedSpriteParams
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x34
FIELDS:
  public            System.Boolean                  slicedParamNeedUpdate  // 0x10
  public            UnityEngine.Vector2             inner_PosMin  // 0x14
  public            UnityEngine.Vector2             inner_PosMax  // 0x1c
  public            UnityEngine.Vector2             inner_UVMin  // 0x24
  public            UnityEngine.Vector2             inner_UVMax  // 0x2c
METHODS:
  RVA=0x04DBEFB0  token=0x60001B8  System.Void .ctor(System.Boolean slicedParamNeedUpdate, UnityEngine.Vector2 inner_PosMin, UnityEngine.Vector2 inner_PosMax, UnityEngine.Vector2 inner_UVMin, UnityEngine.Vector2 inner_UVMax)
END_CLASS

CLASS: EDynamicAtlasActiveMode
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.UI.Image.EDynamicAtlasActiveModeActiveOnAwake  // const
  public    static  UnityEngine.UI.Image.EDynamicAtlasActiveModeActiveOnEnable  // const
METHODS:
END_CLASS

CLASS: ContentType
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.InputField.ContentTypeStandard  // const
  public    static  UnityEngine.UI.InputField.ContentTypeAutocorrected  // const
  public    static  UnityEngine.UI.InputField.ContentTypeIntegerNumber  // const
  public    static  UnityEngine.UI.InputField.ContentTypeDecimalNumber  // const
  public    static  UnityEngine.UI.InputField.ContentTypeAlphanumeric  // const
  public    static  UnityEngine.UI.InputField.ContentTypeName  // const
  public    static  UnityEngine.UI.InputField.ContentTypeEmailAddress  // const
  public    static  UnityEngine.UI.InputField.ContentTypePassword  // const
  public    static  UnityEngine.UI.InputField.ContentTypePin  // const
  public    static  UnityEngine.UI.InputField.ContentTypeCustom  // const
METHODS:
END_CLASS

CLASS: InputType
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.InputField.InputTypeStandard  // const
  public    static  UnityEngine.UI.InputField.InputTypeAutoCorrect  // const
  public    static  UnityEngine.UI.InputField.InputTypePassword  // const
METHODS:
END_CLASS

CLASS: CharacterValidation
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.InputField.CharacterValidationNone  // const
  public    static  UnityEngine.UI.InputField.CharacterValidationInteger  // const
  public    static  UnityEngine.UI.InputField.CharacterValidationDecimal  // const
  public    static  UnityEngine.UI.InputField.CharacterValidationAlphanumeric  // const
  public    static  UnityEngine.UI.InputField.CharacterValidationName  // const
  public    static  UnityEngine.UI.InputField.CharacterValidationEmailAddress  // const
METHODS:
END_CLASS

CLASS: LineType
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.InputField.LineTypeSingleLine  // const
  public    static  UnityEngine.UI.InputField.LineTypeMultiLineSubmit  // const
  public    static  UnityEngine.UI.InputField.LineTypeMultiLineNewline  // const
METHODS:
END_CLASS

CLASS: OnValidateInput
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B31A110  token=0x600025E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x600025F  System.Char Invoke(System.String text, System.Int32 charIndex, System.Char addedChar)
  RVA=0x0B57220C  token=0x6000260  System.IAsyncResult BeginInvoke(System.String text, System.Int32 charIndex, System.Char addedChar, System.AsyncCallback callback, System.Object object)
  RVA=0x09612FF8  token=0x6000261  System.Char EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: SubmitEvent
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B5722A8  token=0x6000262  System.Void .ctor()
END_CLASS

CLASS: EndEditEvent
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B568D1C  token=0x6000263  System.Void .ctor()
END_CLASS

CLASS: OnChangeEvent
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B5721D0  token=0x6000264  System.Void .ctor()
END_CLASS

CLASS: EditState
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.InputField.EditStateContinue  // const
  public    static  UnityEngine.UI.InputField.EditStateFinish  // const
METHODS:
END_CLASS

CLASS: <CaretBlink>d__169
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.UI.InputField       <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000265  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000266  System.Void System.IDisposable.Dispose()
  RVA=0x0B5722E4  token=0x6000267  System.Boolean MoveNext()
  RVA=0x0B572404  token=0x6000269  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <MouseDragOutsideRect>d__191
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.EventSystems.PointerEventDataeventData  // 0x20
  public            UnityEngine.UI.InputField       <>4__this  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600026B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600026C  System.Void System.IDisposable.Dispose()
  RVA=0x0B57249C  token=0x600026D  System.Boolean MoveNext()
  RVA=0x0B572724  token=0x600026F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: AspectMode
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.AspectRatioFitter.AspectModeNone  // const
  public    static  UnityEngine.UI.AspectRatioFitter.AspectModeWidthControlsHeight  // const
  public    static  UnityEngine.UI.AspectRatioFitter.AspectModeHeightControlsWidth  // const
  public    static  UnityEngine.UI.AspectRatioFitter.AspectModeFitInParent  // const
  public    static  UnityEngine.UI.AspectRatioFitter.AspectModeEnvelopeParent  // const
METHODS:
END_CLASS

CLASS: ScaleMode
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.CanvasScaler.ScaleModeConstantPixelSize  // const
  public    static  UnityEngine.UI.CanvasScaler.ScaleModeScaleWithScreenSize  // const
  public    static  UnityEngine.UI.CanvasScaler.ScaleModeConstantPhysicalSize  // const
METHODS:
END_CLASS

CLASS: ScreenMatchMode
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.CanvasScaler.ScreenMatchModeMatchWidthOrHeight  // const
  public    static  UnityEngine.UI.CanvasScaler.ScreenMatchModeExpand  // const
  public    static  UnityEngine.UI.CanvasScaler.ScreenMatchModeShrink  // const
METHODS:
END_CLASS

CLASS: Unit
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.CanvasScaler.UnitCentimeters  // const
  public    static  UnityEngine.UI.CanvasScaler.UnitMillimeters  // const
  public    static  UnityEngine.UI.CanvasScaler.UnitInches  // const
  public    static  UnityEngine.UI.CanvasScaler.UnitPoints  // const
  public    static  UnityEngine.UI.CanvasScaler.UnitPicas  // const
METHODS:
END_CLASS

CLASS: FitMode
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.ContentSizeFitter.FitModeUnconstrained  // const
  public    static  UnityEngine.UI.ContentSizeFitter.FitModeMinSize  // const
  public    static  UnityEngine.UI.ContentSizeFitter.FitModePreferredSize  // const
METHODS:
END_CLASS

CLASS: Corner
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.GridLayoutGroup.CornerUpperLeft  // const
  public    static  UnityEngine.UI.GridLayoutGroup.CornerUpperRight  // const
  public    static  UnityEngine.UI.GridLayoutGroup.CornerLowerLeft  // const
  public    static  UnityEngine.UI.GridLayoutGroup.CornerLowerRight  // const
METHODS:
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.GridLayoutGroup.AxisHorizontal  // const
  public    static  UnityEngine.UI.GridLayoutGroup.AxisVertical  // const
METHODS:
END_CLASS

CLASS: Constraint
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.GridLayoutGroup.ConstraintFlexible  // const
  public    static  UnityEngine.UI.GridLayoutGroup.ConstraintFixedColumnCount  // const
  public    static  UnityEngine.UI.GridLayoutGroup.ConstraintFixedRowCount  // const
METHODS:
END_CLASS

CLASS: <DelayedSetDirty>d__56
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.RectTransform       rectTransform  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000328  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000329  System.Void System.IDisposable.Dispose()
  RVA=0x04294B40  token=0x600032A  System.Boolean MoveNext()
  RVA=0x0B572450  token=0x600032C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.UI.LayoutRebuilder.<>c<>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.Component><>9__10_0  // static @ 0x8
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Component><>9__12_0  // static @ 0x10
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Component><>9__12_1  // static @ 0x18
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Component><>9__12_2  // static @ 0x20
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Component><>9__12_3  // static @ 0x28
METHODS:
  RVA=0x04D4C990  token=0x6000342  System.Void .cctor()
  RVA=0x041E1670  token=0x6000343  System.Void .ctor()
  RVA=0x0353E490  token=0x6000344  UnityEngine.UI.LayoutRebuilder <.cctor>b__5_0()
  RVA=0x03E504C0  token=0x6000345  System.Void <.cctor>b__5_1(UnityEngine.UI.LayoutRebuilder x)
  RVA=0x03A18CE0  token=0x6000346  System.Boolean <StripDisabledBehavioursFromList>b__10_0(UnityEngine.Component e)
  RVA=0x03B91740  token=0x6000347  System.Void <Rebuild>b__12_0(UnityEngine.Component e)
  RVA=0x031E8FA0  token=0x6000348  System.Void <Rebuild>b__12_1(UnityEngine.Component e)
  RVA=0x03B91960  token=0x6000349  System.Void <Rebuild>b__12_2(UnityEngine.Component e)
  RVA=0x03EF8680  token=0x600034A  System.Void <Rebuild>b__12_3(UnityEngine.Component e)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000059
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.UI.LayoutUtility.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__3_0  // static @ 0x8
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__4_0  // static @ 0x10
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__4_1  // static @ 0x18
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__5_0  // static @ 0x20
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__6_0  // static @ 0x28
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__7_0  // static @ 0x30
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__7_1  // static @ 0x38
  public    static  System.Func<UnityEngine.UI.ILayoutElement,System.Single><>9__8_0  // static @ 0x40
METHODS:
  RVA=0x04D4CB70  token=0x6000356  System.Void .cctor()
  RVA=0x041E1670  token=0x6000357  System.Void .ctor()
  RVA=0x03C6D650  token=0x6000358  System.Single <GetMinWidth>b__3_0(UnityEngine.UI.ILayoutElement e)
  RVA=0x03C6D670  token=0x6000359  System.Single <GetPreferredWidth>b__4_0(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E9B20  token=0x600035A  System.Single <GetPreferredWidth>b__4_1(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E95A0  token=0x600035B  System.Single <GetFlexibleWidth>b__5_0(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E97C0  token=0x600035C  System.Single <GetMinHeight>b__6_0(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E97C0  token=0x600035D  System.Single <GetPreferredHeight>b__7_0(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E96D0  token=0x600035E  System.Single <GetPreferredHeight>b__7_1(UnityEngine.UI.ILayoutElement e)
  RVA=0x031E9890  token=0x600035F  System.Single <GetFlexibleHeight>b__8_0(UnityEngine.UI.ILayoutElement e)
END_CLASS

CLASS: CullStateChangedEvent
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0539350C  token=0x6000389  System.Void .ctor()
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x2000063
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Navigation.Mode  None  // const
  public    static  UnityEngine.UI.Navigation.Mode  Horizontal  // const
  public    static  UnityEngine.UI.Navigation.Mode  Vertical  // const
  public    static  UnityEngine.UI.Navigation.Mode  Automatic  // const
  public    static  UnityEngine.UI.Navigation.Mode  Explicit  // const
METHODS:
END_CLASS

CLASS: Direction
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Scrollbar.DirectionLeftToRight  // const
  public    static  UnityEngine.UI.Scrollbar.DirectionRightToLeft  // const
  public    static  UnityEngine.UI.Scrollbar.DirectionBottomToTop  // const
  public    static  UnityEngine.UI.Scrollbar.DirectionTopToBottom  // const
METHODS:
END_CLASS

CLASS: ScrollEvent
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0453A600  token=0x60003FA  System.Void .ctor()
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x2000069
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Scrollbar.Axis   Horizontal  // const
  public    static  UnityEngine.UI.Scrollbar.Axis   Vertical  // const
METHODS:
END_CLASS

CLASS: <ClickRepeat>d__58
TYPE:  sealed class
TOKEN: 0x200006A
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.UI.Scrollbar        <>4__this  // 0x20
  public            UnityEngine.Vector2             screenPosition  // 0x28
  public            UnityEngine.Camera              camera  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003FB  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003FC  System.Void System.IDisposable.Dispose()
  RVA=0x0B57A574  token=0x60003FD  System.Boolean MoveNext()
  RVA=0x0B57A79C  token=0x60003FF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: MovementType
TYPE:  sealed struct
TOKEN: 0x200006C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.ScrollRect.MovementTypeUnrestricted  // const
  public    static  UnityEngine.UI.ScrollRect.MovementTypeElastic  // const
  public    static  UnityEngine.UI.ScrollRect.MovementTypeClamped  // const
METHODS:
END_CLASS

CLASS: ScrollbarVisibility
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.ScrollRect.ScrollbarVisibilityPermanent  // const
  public    static  UnityEngine.UI.ScrollRect.ScrollbarVisibilityAutoHide  // const
  public    static  UnityEngine.UI.ScrollRect.ScrollbarVisibilityAutoHideAndExpandViewport  // const
METHODS:
END_CLASS

CLASS: ScrollRectEvent
TYPE:  class
TOKEN: 0x200006E
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x05396540  token=0x600045E  System.Void .ctor()
END_CLASS

CLASS: Transition
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Selectable.TransitionNone  // const
  public    static  UnityEngine.UI.Selectable.TransitionColorTint  // const
  public    static  UnityEngine.UI.Selectable.TransitionSpriteSwap  // const
  public    static  UnityEngine.UI.Selectable.TransitionAnimation  // const
METHODS:
END_CLASS

CLASS: SelectionState
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Selectable.SelectionStateNormal  // const
  public    static  UnityEngine.UI.Selectable.SelectionStateHighlighted  // const
  public    static  UnityEngine.UI.Selectable.SelectionStatePressed  // const
  public    static  UnityEngine.UI.Selectable.SelectionStateSelected  // const
  public    static  UnityEngine.UI.Selectable.SelectionStateDisabled  // const
METHODS:
END_CLASS

CLASS: Direction
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Slider.Direction LeftToRight  // const
  public    static  UnityEngine.UI.Slider.Direction RightToLeft  // const
  public    static  UnityEngine.UI.Slider.Direction BottomToTop  // const
  public    static  UnityEngine.UI.Slider.Direction TopToBottom  // const
METHODS:
END_CLASS

CLASS: SliderEvent
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x05397550  token=0x60004FF  System.Void .ctor()
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Slider.Axis      Horizontal  // const
  public    static  UnityEngine.UI.Slider.Axis      Vertical  // const
METHODS:
END_CLASS

CLASS: MatEntry
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x40
FIELDS:
  public            UnityEngine.Material            baseMat  // 0x10
  public            UnityEngine.Material            customMat  // 0x18
  public            System.Int32                    count  // 0x20
  public            System.Int32                    stencilId  // 0x24
  public            UnityEngine.Rendering.StencilOp operation  // 0x28
  public            UnityEngine.Rendering.CompareFunctioncompareFunction  // 0x2c
  public            System.Int32                    readMask  // 0x30
  public            System.Int32                    writeMask  // 0x34
  public            System.Boolean                  useAlphaClip  // 0x38
  public            UnityEngine.Rendering.ColorWriteMaskcolorMask  // 0x3c
METHODS:
  RVA=0x04DBEFE0  token=0x6000510  System.Void .ctor()
END_CLASS

CLASS: ToggleTransition
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.Toggle.ToggleTransitionNone  // const
  public    static  UnityEngine.UI.Toggle.ToggleTransitionFade  // const
METHODS:
END_CLASS

CLASS: ToggleEvent
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0539350C  token=0x6000559  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.UI.ToggleGroup.<>c  <>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.UI.Toggle><>9__13_0  // static @ 0x8
  public    static  System.Func<UnityEngine.UI.Toggle,System.Boolean><>9__14_0  // static @ 0x10
METHODS:
  RVA=0x04D45970  token=0x6000568  System.Void .cctor()
  RVA=0x041E1670  token=0x6000569  System.Void .ctor()
  RVA=0x0B57A7E8  token=0x600056A  System.Boolean <AnyTogglesOn>b__13_0(UnityEngine.UI.Toggle x)
  RVA=0x0B57A7E8  token=0x600056B  System.Boolean <ActiveToggles>b__14_0(UnityEngine.UI.Toggle x)
END_CLASS

CLASS: Raycast3DCallback
TYPE:  sealed class
TOKEN: 0x2000085
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B5732D8  token=0x600056E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B572AF0  token=0x600056F  System.Boolean Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit& hit, System.Single f, System.Int32 i)
  RVA=0x0B5731F0  token=0x6000570  System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit& hit, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD1038  token=0x6000571  System.Boolean EndInvoke(UnityEngine.RaycastHit& hit, System.IAsyncResult result)
END_CLASS

CLASS: RaycastAllCallback
TYPE:  sealed class
TOKEN: 0x2000086
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B572958  token=0x6000572  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B572920  token=0x6000573  UnityEngine.RaycastHit[] Invoke(UnityEngine.Ray r, System.Single f, System.Int32 i)
  RVA=0x0B5733A0  token=0x6000574  System.IAsyncResult BeginInvoke(UnityEngine.Ray r, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6000575  UnityEngine.RaycastHit[] EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetRaycastNonAllocCallback
TYPE:  sealed class
TOKEN: 0x2000087
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B572B30  token=0x6000576  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B572AF0  token=0x6000577  System.Int32 Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit[] results, System.Single f, System.Int32 i)
  RVA=0x0B572BF8  token=0x6000578  System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit[] results, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x0539AB64  token=0x6000579  System.Int32 EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Raycast2DCallback
TYPE:  sealed class
TOKEN: 0x2000088
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B573128  token=0x600057A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B5730CC  token=0x600057B  UnityEngine.RaycastHit2D Invoke(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, System.Single f, System.Int32 i)
  RVA=0x0B573004  token=0x600057C  System.IAsyncResult BeginInvoke(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x08420754  token=0x600057D  UnityEngine.RaycastHit2D EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetRayIntersectionAllCallback
TYPE:  sealed class
TOKEN: 0x2000089
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B572958  token=0x600057E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B572920  token=0x600057F  UnityEngine.RaycastHit2D[] Invoke(UnityEngine.Ray r, System.Single f, System.Int32 i)
  RVA=0x0B572868  token=0x6000580  System.IAsyncResult BeginInvoke(UnityEngine.Ray r, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6000581  UnityEngine.RaycastHit2D[] EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetRayIntersectionAllNonAllocCallback
TYPE:  sealed class
TOKEN: 0x200008A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B572B30  token=0x6000582  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B572AF0  token=0x6000583  System.Int32 Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit2D[] results, System.Single f, System.Int32 i)
  RVA=0x0B572A20  token=0x6000584  System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit2D[] results, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x0539AB64  token=0x6000585  System.Int32 EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ColorTweenMode
TYPE:  sealed struct
TOKEN: 0x2000096
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenModeAll  // const
  public    static  UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenModeRGB  // const
  public    static  UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenModeAlpha  // const
METHODS:
END_CLASS

CLASS: ColorTweenCallback
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x05397350  token=0x60005D9  System.Void .ctor()
END_CLASS

CLASS: FloatTweenCallback
TYPE:  class
TOKEN: 0x2000099
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B57D574  token=0x60005E7  System.Void .ctor()
END_CLASS

CLASS: <Start>d__2
TYPE:  sealed class
TOKEN: 0x200009B
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            T                               tweenInfo  // 0x0
  private           System.Single                   <elapsedTime>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60005ED  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60005EE  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60005EF  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60005F1  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: PointerEventType
TYPE:  sealed struct
TOKEN: 0x200009D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UIElements.PanelEventHandler.PointerEventTypeDefault  // const
  public    static  UnityEngine.UIElements.PanelEventHandler.PointerEventTypeDown  // const
  public    static  UnityEngine.UIElements.PanelEventHandler.PointerEventTypeUp  // const
METHODS:
END_CLASS

CLASS: PointerEvent
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x80
IMPLEMENTS: UnityEngine.UIElements.IPointerEvent
FIELDS:
  private           System.Int32                    <pointerId>k__BackingField  // 0x10
  private           System.String                   <pointerType>k__BackingField  // 0x18
  private           System.Boolean                  <isPrimary>k__BackingField  // 0x20
  private           System.Int32                    <button>k__BackingField  // 0x24
  private           System.Int32                    <pressedButtons>k__BackingField  // 0x28
  private           UnityEngine.Vector3             <position>k__BackingField  // 0x2c
  private           UnityEngine.Vector3             <localPosition>k__BackingField  // 0x38
  private           UnityEngine.Vector3             <deltaPosition>k__BackingField  // 0x44
  private           System.Single                   <deltaTime>k__BackingField  // 0x50
  private           System.Int32                    <clickCount>k__BackingField  // 0x54
  private           System.Single                   <pressure>k__BackingField  // 0x58
  private           System.Single                   <tangentialPressure>k__BackingField  // 0x5c
  private           System.Single                   <altitudeAngle>k__BackingField  // 0x60
  private           System.Single                   <azimuthAngle>k__BackingField  // 0x64
  private           System.Single                   <twist>k__BackingField  // 0x68
  private           UnityEngine.Vector2             <radius>k__BackingField  // 0x6c
  private           UnityEngine.Vector2             <radiusVariance>k__BackingField  // 0x74
  private           UnityEngine.EventModifiers      <modifiers>k__BackingField  // 0x7c
PROPERTIES:
  pointerId  get=0x02B2E2D0  set=0x014F51F0
  pointerType  get=0x0385B100  set=0x05392C40
  isPrimary  get=0x04D866B0  set=0x04D866D0
  button  get=0x011F0020  set=0x011F0030
  pressedButtons  get=0x011F2A90  set=0x011F2AA0
  position  get=0x04D90540  set=0x04D90510
  localPosition  get=0x04D9E700  set=0x04D9DB00
  deltaPosition  get=0x04D914A0  set=0x04D914C0
  deltaTime  get=0x04D885A0  set=0x04D88FF0
  clickCount  get=0x04D86540  set=0x04D86590
  pressure  get=0x04D88400  set=0x04D88430
  tangentialPressure  get=0x04D91500  set=0x04D91510
  altitudeAngle  get=0x04D88480  set=0x04D884A0
  azimuthAngle  get=0x04D88470  set=0x04D88490
  twist  get=0x04D8F060  set=0x04D8F090
  radius  get=0x04DBF340  set=0x04DBF370
  radiusVariance  get=0x04DBF320  set=0x04DBF360
  modifiers  get=0x04D86410  set=0x04D86490
  shiftKey  get=0x0B5817D4
  ctrlKey  get=0x0B5817CC
  commandKey  get=0x0B5817C4
  altKey  get=0x0B5817BC
  actionKey  get=0x0B581788
METHODS:
  RVA=0x0B581260  token=0x600063F  System.Void Read(UnityEngine.UIElements.PanelEventHandler self, UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType)
  RVA=0x04DBF2F0  token=0x6000640  System.Void SetPosition(UnityEngine.Vector3 positionOverride, UnityEngine.Vector3 deltaOverride)
  RVA=0x041E1670  token=0x6000641  System.Void .ctor()
  RVA=0x0B581774  token=0x6000642  System.Boolean <Read>g__InRange|82_0(System.Int32 i, System.Int32 start, System.Int32 count)
END_CLASS

CLASS: FloatIntBits
TYPE:  sealed struct
TOKEN: 0x20000A0
SIZE:  0x14
FIELDS:
  public            System.Single                   f  // 0x10
  public            System.Int32                    i  // 0x10
METHODS:
END_CLASS

CLASS: InputButton
TYPE:  sealed struct
TOKEN: 0x20000A5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.PointerEventData.InputButtonLeft  // const
  public    static  UnityEngine.EventSystems.PointerEventData.InputButtonRight  // const
  public    static  UnityEngine.EventSystems.PointerEventData.InputButtonMiddle  // const
METHODS:
END_CLASS

CLASS: FramePressState
TYPE:  sealed struct
TOKEN: 0x20000A6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.PointerEventData.FramePressStatePressed  // const
  public    static  UnityEngine.EventSystems.PointerEventData.FramePressStateReleased  // const
  public    static  UnityEngine.EventSystems.PointerEventData.FramePressStatePressedAndReleased  // const
  public    static  UnityEngine.EventSystems.PointerEventData.FramePressStateNotChanged  // const
METHODS:
END_CLASS

CLASS: ProfilerMarkers
TYPE:  static class
TOKEN: 0x20000BC
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Profiling.ProfilerMarker  Update  // static @ 0x0
  public    static readonly Unity.Profiling.ProfilerMarker  TickModules  // static @ 0x8
  public    static readonly Unity.Profiling.ProfilerMarker  CurrentModuleProcess  // static @ 0x10
  public    static readonly Unity.Profiling.ProfilerMarker  RaycastAll  // static @ 0x18
  public    static readonly Unity.Profiling.ProfilerMarker  RaycastAllSort  // static @ 0x20
METHODS:
  RVA=0x04B1D7A0  token=0x60006D5  System.Void .cctor()
END_CLASS

CLASS: UIToolkitOverrideConfig
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x20
FIELDS:
  public            UnityEngine.EventSystems.EventSystemactiveEventSystem  // 0x10
  public            System.Boolean                  sendEvents  // 0x18
  public            System.Boolean                  createPanelGameObjectsOnStart  // 0x19
METHODS:
END_CLASS

CLASS: <>c__DisplayClass53_0
TYPE:  sealed class
TOKEN: 0x20000BE
SIZE:  0x18
FIELDS:
  public            UnityEngine.GameObject          go  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60006D6  System.Void .ctor()
  RVA=0x0B588900  token=0x60006D7  System.Void <CreateUIToolkitPanelGameObject>b__0()
END_CLASS

CLASS: TriggerEvent
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0B5888C4  token=0x60006EF  System.Void .ctor()
END_CLASS

CLASS: Entry
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x20
FIELDS:
  public            UnityEngine.EventSystems.EventTriggerTypeeventID  // 0x10
  public            UnityEngine.EventSystems.EventTrigger.TriggerEventcallback  // 0x18
METHODS:
  RVA=0x0B57C194  token=0x60006F0  System.Void .ctor()
END_CLASS

CLASS: EventFunction`1
TYPE:  sealed class
TOKEN: 0x20000C4
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600071E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600071F  System.Void Invoke(T1 handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=-1  // runtime  token=0x6000720  System.IAsyncResult BeginInvoke(T1 handler, UnityEngine.EventSystems.BaseEventData eventData, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000721  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ButtonState
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x20
FIELDS:
  private           UnityEngine.EventSystems.PointerEventData.InputButtonm_Button  // 0x10
  private           UnityEngine.EventSystems.PointerInputModule.MouseButtonEventDatam_EventData  // 0x18
PROPERTIES:
  eventData  get=0x0385B100  set=0x05392C40
  button  get=0x02B2E2D0  set=0x014F51F0
METHODS:
  RVA=0x041E1670  token=0x600075D  System.Void .ctor()
END_CLASS

CLASS: MouseState
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.EventSystems.PointerInputModule.ButtonState>m_TrackedButtons  // 0x10
METHODS:
  RVA=0x0B57D6AC  token=0x600075E  System.Boolean AnyPressesThisFrame()
  RVA=0x0B57D74C  token=0x600075F  System.Boolean AnyReleasesThisFrame()
  RVA=0x0B57D7EC  token=0x6000760  UnityEngine.EventSystems.PointerInputModule.ButtonState GetButtonState(UnityEngine.EventSystems.PointerEventData.InputButton button)
  RVA=0x0B57D924  token=0x6000761  System.Void SetButtonState(UnityEngine.EventSystems.PointerEventData.InputButton button, UnityEngine.EventSystems.PointerEventData.FramePressState stateForMouseButton, UnityEngine.EventSystems.PointerEventData data)
  RVA=0x0B57D974  token=0x6000762  System.Void .ctor()
END_CLASS

CLASS: MouseButtonEventData
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x20
FIELDS:
  public            UnityEngine.EventSystems.PointerEventData.FramePressStatebuttonState  // 0x10
  public            UnityEngine.EventSystems.PointerEventDatabuttonData  // 0x18
METHODS:
  RVA=0x0A9CB864  token=0x6000763  System.Boolean PressedThisFrame()
  RVA=0x0863E6DC  token=0x6000764  System.Boolean ReleasedThisFrame()
  RVA=0x041E1670  token=0x6000765  System.Void .ctor()
END_CLASS

CLASS: ProfilerMarkers
TYPE:  static class
TOKEN: 0x20000CC
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Profiling.ProfilerMarker  Process  // static @ 0x0
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessMouseEvent  // static @ 0x8
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessMousePress  // static @ 0x10
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessTouchEvents  // static @ 0x18
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessTouchPress  // static @ 0x20
METHODS:
  RVA=0x0B583294  token=0x600078A  System.Void .cctor()
END_CLASS

CLASS: InputMode
TYPE:  sealed struct
TOKEN: 0x20000CD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.StandaloneInputModule.InputModeMouse  // const
  public    static  UnityEngine.EventSystems.StandaloneInputModule.InputModeButtons  // const
METHODS:
END_CLASS

CLASS: ProfilerMarkers
TYPE:  static class
TOKEN: 0x20000CF
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Profiling.ProfilerMarker  Process  // static @ 0x0
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessTouchEvents  // static @ 0x8
  public    static readonly Unity.Profiling.ProfilerMarker  ProcessTouchPress  // static @ 0x10
METHODS:
  RVA=0x0B5831C0  token=0x600079A  System.Void .cctor()
END_CLASS

CLASS: RaycastHitComparer
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
  public    static  UnityEngine.EventSystems.PhysicsRaycaster.RaycastHitComparerinstance  // static @ 0x0
METHODS:
  RVA=0x0B5834DC  token=0x60007B7  System.Int32 Compare(UnityEngine.RaycastHit x, UnityEngine.RaycastHit y)
  RVA=0x041E1670  token=0x60007B8  System.Void .ctor()
  RVA=0x0B583508  token=0x60007B9  System.Void .cctor()
END_CLASS

CLASS: AutoScope
TYPE:  sealed struct
TOKEN: 0x20000DB
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007DB  System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1> marker, TP1 p1)
  RVA=-1  // not resolved  token=0x60007DC  System.Void Dispose()
END_CLASS

CLASS: AutoScope
TYPE:  sealed struct
TOKEN: 0x20000DD
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007E2  System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1,TP2> marker, TP1 p1, TP2 p2)
  RVA=-1  // not resolved  token=0x60007E3  System.Void Dispose()
END_CLASS

CLASS: AutoScope
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007E9  System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1,TP2,TP3> marker, TP1 p1, TP2 p2, TP3 p3)
  RVA=-1  // not resolved  token=0x60007EA  System.Void Dispose()
END_CLASS

CLASS: ProfilerMarkerScope
TYPE:  sealed struct
TOKEN: 0x20000E1
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60007FE  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker)
  RVA=0x041E1670  token=0x60007FF  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int32 context)
  RVA=0x041E1670  token=0x6000800  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context)
  RVA=0x041E1670  token=0x6000801  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Single context)
  RVA=0x041E1670  token=0x6000802  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.String context)
  RVA=0x041E1670  token=0x6000803  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.String context, System.String context2)
  RVA=0x041E1670  token=0x6000804  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context, System.String context2)
  RVA=0x041E1670  token=0x6000805  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context, System.Int64 context2)
  RVA=0x041E1670  token=0x6000806  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, UnityEngine.Object obj)
  RVA=0x041E1670  token=0x6000807  System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, UnityEngine.Transform transform, System.Boolean path)
  RVA=0x041E1670  token=0x6000808  System.Void Dispose()
END_CLASS

CLASS: __StaticArrayInitTypeSize=12
TYPE:  sealed struct
TOKEN: 0x20000E9
SIZE:  0x1C
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=121C3635C112D556F4C11A4FE6BDE6ED3F126C4B2B546811BDB64DE7BDED3A05CB  // static @ 0x0
METHODS:
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

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Unity.Profiling.ProfilerCounter`1
TYPE:  sealed struct
TOKEN: 0x20000D8
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007CC  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
  RVA=-1  // not resolved  token=0x60007CD  System.Void Sample(T value)
END_CLASS

CLASS: Unity.Profiling.ProfilerCounterValue`1
TYPE:  sealed struct
TOKEN: 0x20000D9
SIZE:  0x11
FIELDS:
PROPERTIES:
  Value  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60007CE  System.Void .ctor(System.String name)
  RVA=-1  // not resolved  token=0x60007CF  System.Void .ctor(System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
  RVA=-1  // not resolved  token=0x60007D0  System.Void .ctor(System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit, Unity.Profiling.ProfilerCounterOptions counterOptions)
  RVA=-1  // not resolved  token=0x60007D1  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit)
  RVA=-1  // not resolved  token=0x60007D2  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit, Unity.Profiling.ProfilerCounterOptions counterOptions)
  RVA=-1  // not resolved  token=0x60007D5  System.Void Sample()
END_CLASS

CLASS: Unity.Profiling.ProfilerMarker`1
TYPE:  sealed struct
TOKEN: 0x20000DA
SIZE:  0x11
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007D6  System.Void .ctor(System.String name, System.String param1Name)
  RVA=-1  // not resolved  token=0x60007D7  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name)
  RVA=-1  // not resolved  token=0x60007D8  System.Void Begin(TP1 p1)
  RVA=-1  // not resolved  token=0x60007D9  System.Void End()
  RVA=-1  // not resolved  token=0x60007DA  Unity.Profiling.ProfilerMarker.AutoScope<TP1> Auto(TP1 p1)
END_CLASS

CLASS: Unity.Profiling.ProfilerMarker`2
TYPE:  sealed struct
TOKEN: 0x20000DC
SIZE:  0x11
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007DD  System.Void .ctor(System.String name, System.String param1Name, System.String param2Name)
  RVA=-1  // not resolved  token=0x60007DE  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name, System.String param2Name)
  RVA=-1  // not resolved  token=0x60007DF  System.Void Begin(TP1 p1, TP2 p2)
  RVA=-1  // not resolved  token=0x60007E0  System.Void End()
  RVA=-1  // not resolved  token=0x60007E1  Unity.Profiling.ProfilerMarker.AutoScope<TP1,TP2> Auto(TP1 p1, TP2 p2)
END_CLASS

CLASS: Unity.Profiling.ProfilerMarker`3
TYPE:  sealed struct
TOKEN: 0x20000DE
SIZE:  0x11
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60007E4  System.Void .ctor(System.String name, System.String param1Name, System.String param2Name, System.String param3Name)
  RVA=-1  // not resolved  token=0x60007E5  System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name, System.String param2Name, System.String param3Name)
  RVA=-1  // not resolved  token=0x60007E6  System.Void Begin(TP1 p1, TP2 p2, TP3 p3)
  RVA=-1  // not resolved  token=0x60007E7  System.Void End()
  RVA=-1  // not resolved  token=0x60007E8  Unity.Profiling.ProfilerMarker.AutoScope<TP1,TP2,TP3> Auto(TP1 p1, TP2 p2, TP3 p3)
END_CLASS

CLASS: Unity.Profiling.ProfilerMarkerExtension
TYPE:  static class
TOKEN: 0x20000E0
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B582C80  token=0x60007EB  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int32 metadata)
  RVA=0x0B58314C  token=0x60007EC  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.UInt32 metadata)
  RVA=0x0B58305C  token=0x60007ED  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata)
  RVA=0x0B5830D4  token=0x60007EE  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.UInt64 metadata)
  RVA=0x0B582E34  token=0x60007EF  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Single metadata)
  RVA=0x0B582CF4  token=0x60007F0  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Double metadata)
  RVA=0x0B582C2C  token=0x60007F1  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.String metadata)
  RVA=0x0B582EAC  token=0x60007F2  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.String metadata, System.String metadata2)
  RVA=0x0B582F84  token=0x60007F3  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata, System.String metadata2)
  RVA=0x0B582D6C  token=0x60007F4  System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata, System.Int64 metadata2)
  RVA=0x012081B0  token=0x60007F5  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker)
  RVA=0x04DBF390  token=0x60007F6  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int32 context)
  RVA=0x04DBF390  token=0x60007F7  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context)
  RVA=0x04DBF390  token=0x60007F8  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Single context)
  RVA=0x04DBF390  token=0x60007F9  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.String context)
  RVA=0x04DBF380  token=0x60007FA  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.String context, System.String context2)
  RVA=0x04DBF380  token=0x60007FB  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context, System.String context2)
  RVA=0x04DBF380  token=0x60007FC  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context, System.Int64 context2)
  RVA=0x04DBF380  token=0x60007FD  Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, UnityEngine.Transform transform, System.Boolean path)
END_CLASS

CLASS: Unity.Profiling.MonoObject_64
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x20
FIELDS:
  public            System.Void*                    p1  // 0x10
  public            System.Void*                    p2  // 0x18
METHODS:
END_CLASS

CLASS: Unity.Profiling.MonoObject_32
TYPE:  sealed struct
TOKEN: 0x20000E3
SIZE:  0x20
FIELDS:
  public            System.Void*                    p1  // 0x10
  public            System.Void*                    p2  // 0x14
METHODS:
END_CLASS

CLASS: Unity.Profiling.MonoString_64
TYPE:  sealed struct
TOKEN: 0x20000E4
SIZE:  0x28
FIELDS:
  public            Unity.Profiling.MonoObject_64   obj  // 0x10
  public            System.Int32                    length  // 0x20
  public            System.Char                     firstChar  // 0x24
METHODS:
END_CLASS

CLASS: Unity.Profiling.MonoString_32
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x20
FIELDS:
  public            Unity.Profiling.MonoObject_32   obj  // 0x10
  public            System.Int32                    length  // 0x18
  public            System.Char                     firstChar  // 0x1c
METHODS:
END_CLASS

CLASS: Unity.Profiling.ProfilerString
TYPE:  class
TOKEN: 0x20000E6
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.Int32                    MAX  // const
  private           System.String                   m_buffer  // 0x10
  private           System.Int32                    m_length  // 0x18
  private           System.Int32*                   m_strCount  // 0x20
  private           System.UInt64                   m_handler  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000809  System.Void .ctor(System.Int32 count)
  RVA=0x041E1670  token=0x600080A  System.Void Dispose()
  RVA=0x02B2ECC0  token=0x600080B  System.String ToString()
  RVA=0x0B5834BC  token=0x600080C  System.String op_Implicit(Unity.Profiling.ProfilerString handle)
  RVA=0x0B5833D4  token=0x600080D  System.Void Append(System.String value)
END_CLASS

CLASS: Unity.Profiling.ProfilerUtility
TYPE:  sealed struct
TOKEN: 0x20000E7
SIZE:  0x11
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600080E  System.Byte GetProfilerMarkerDataType()
END_CLASS

CLASS: UnityEngine.EventSystems.AxisEventData
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.BaseEventData
FIELDS:
  private           UnityEngine.Vector2             <moveVector>k__BackingField  // 0x20
  private           UnityEngine.EventSystems.MoveDirection<moveDir>k__BackingField  // 0x28
PROPERTIES:
  moveVector  get=0x04DA5EA0  set=0x04D86500
  moveDir  get=0x011F2A90  set=0x011F2AA0
METHODS:
  RVA=0x0B57B33C  token=0x6000653  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
END_CLASS

CLASS: UnityEngine.EventSystems.AbstractEventData
TYPE:  abstract class
TOKEN: 0x20000A2
SIZE:  0x18
FIELDS:
  protected         System.Boolean                  m_Used  // 0x10
PROPERTIES:
  used  get=0x015EFCE0
METHODS:
  RVA=0x04D8CCB0  token=0x6000654  System.Void Reset()
  RVA=0x015EFCD0  token=0x6000655  System.Void Use()
  RVA=0x041E1670  token=0x6000657  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.EventSystems.BaseEventData
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x20
EXTENDS: UnityEngine.EventSystems.AbstractEventData
FIELDS:
  private   readonly UnityEngine.EventSystems.EventSystemm_EventSystem  // 0x18
PROPERTIES:
  currentInputModule  get=0x0A8DA4B4
  selectedObject  get=0x0A8DA594  set=0x0B57B364
METHODS:
  RVA=0x05392C40  token=0x6000658  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
END_CLASS

CLASS: UnityEngine.EventSystems.PointerEventData
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x160
EXTENDS: UnityEngine.EventSystems.BaseEventData
FIELDS:
  private           UnityEngine.GameObject          <pointerEnter>k__BackingField  // 0x20
  private           UnityEngine.GameObject          m_PointerPress  // 0x28
  private           UnityEngine.GameObject          <lastPress>k__BackingField  // 0x30
  private           UnityEngine.GameObject          <rawPointerPress>k__BackingField  // 0x38
  private           UnityEngine.GameObject          <pointerDrag>k__BackingField  // 0x40
  private           UnityEngine.GameObject          <pointerClick>k__BackingField  // 0x48
  private           UnityEngine.EventSystems.RaycastResult<pointerCurrentRaycast>k__BackingField  // 0x50
  private           UnityEngine.EventSystems.RaycastResult<pointerPressRaycast>k__BackingField  // 0x98
  public            System.Collections.Generic.List<UnityEngine.GameObject>hovered  // 0xe0
  private           System.Boolean                  <eligibleForClick>k__BackingField  // 0xe8
  private           System.Int32                    <pointerId>k__BackingField  // 0xec
  private           UnityEngine.Vector2             <position>k__BackingField  // 0xf0
  private           UnityEngine.Vector2             <delta>k__BackingField  // 0xf8
  private           UnityEngine.Vector2             <pressPosition>k__BackingField  // 0x100
  private           UnityEngine.Vector3             <worldPosition>k__BackingField  // 0x108
  private           UnityEngine.Vector3             <worldNormal>k__BackingField  // 0x114
  private           System.Single                   <clickTime>k__BackingField  // 0x120
  private           System.Int32                    <clickCount>k__BackingField  // 0x124
  private           UnityEngine.Vector2             <scrollDelta>k__BackingField  // 0x128
  private           System.Boolean                  <useDragThreshold>k__BackingField  // 0x130
  private           System.Boolean                  <dragging>k__BackingField  // 0x131
  private           UnityEngine.EventSystems.PointerEventData.InputButton<button>k__BackingField  // 0x134
  private           System.Single                   <pressure>k__BackingField  // 0x138
  private           System.Single                   <tangentialPressure>k__BackingField  // 0x13c
  private           System.Single                   <altitudeAngle>k__BackingField  // 0x140
  private           System.Single                   <azimuthAngle>k__BackingField  // 0x144
  private           System.Single                   <twist>k__BackingField  // 0x148
  private           UnityEngine.Vector2             <radius>k__BackingField  // 0x14c
  private           UnityEngine.Vector2             <radiusVariance>k__BackingField  // 0x154
  private           System.Boolean                  <fullyExited>k__BackingField  // 0x15c
  private           System.Boolean                  <reentered>k__BackingField  // 0x15d
PROPERTIES:
  pointerEnter  get=0x04D862C0  set=0x05390F40
  lastPress  get=0x011F36E0  set=0x05396200
  rawPointerPress  get=0x04D85A50  set=0x046A91B0
  pointerDrag  get=0x04D85A60  set=0x042B4AE0
  pointerClick  get=0x04D86200  set=0x035A41A0
  pointerCurrentRaycast  get=0x04DBF160  set=0x0B5811E4
  pointerPressRaycast  get=0x04DBF1A0  set=0x0B58121C
  eligibleForClick  get=0x04D8C8C0  set=0x04D8C8F0
  pointerId  get=0x04D9DF40  set=0x04D9E020
  position  get=0x04D8E090  set=0x04D8E0B0
  delta  get=0x04DBF130  set=0x04DBF270
  pressPosition  get=0x04DBF1E0  set=0x04DBF2A0
  worldPosition  get=0x04D91A30  set=0x04D91A70
  worldNormal  get=0x04D90F70  set=0x04D90FF0
  clickTime  get=0x04DBD7E0  set=0x04DBD820
  clickCount  get=0x04D90A80  set=0x04D90AB0
  scrollDelta  get=0x04DBF240  set=0x04DA14B0
  useDragThreshold  get=0x04D92620  set=0x04D92640
  dragging  get=0x04DBF150  set=0x04DBF280
  button  get=0x04DA3240  set=0x04DBDA30
  pressure  get=0x04DA3250  set=0x04DBF2B0
  tangentialPressure  get=0x04DA7940  set=0x04DBC020
  altitudeAngle  get=0x04D8E180  set=0x04D8E1D0
  azimuthAngle  get=0x04DBF120  set=0x04DBF260
  twist  get=0x04D90E70  set=0x04D90E80
  radius  get=0x04DBF220  set=0x04DBF2D0
  radiusVariance  get=0x04DBF200  set=0x04DBF2C0
  fullyExited  get=0x04DA7930  set=0x04DBF290
  reentered  get=0x04DA7C60  set=0x04DBF2E0
  enterEventCamera  get=0x0B5810D8
  pressEventCamera  get=0x0B58115C
  pointerPress  get=0x04D86240  set=0x033F30B0
METHODS:
  RVA=0x03E9B9B0  token=0x6000696  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
  RVA=0x033F2A80  token=0x6000697  System.Boolean IsPointerMoving()
  RVA=0x0B580B40  token=0x6000698  System.Boolean IsScrolling()
  RVA=0x0B580B68  token=0x600069D  System.String ToString()
END_CLASS

CLASS: UnityEngine.EventSystems.EventHandle
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.EventHandleUnused  // const
  public    static  UnityEngine.EventSystems.EventHandleUsed  // const
METHODS:
END_CLASS

CLASS: UnityEngine.EventSystems.IEventSystemHandler
TYPE:  interface
TOKEN: 0x20000A8
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerMoveHandler
TYPE:  interface
TOKEN: 0x20000A9
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600069E  System.Void OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerEnterHandler
TYPE:  interface
TOKEN: 0x20000AA
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600069F  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerExitHandler
TYPE:  interface
TOKEN: 0x20000AB
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A0  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerDownHandler
TYPE:  interface
TOKEN: 0x20000AC
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A1  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerUpHandler
TYPE:  interface
TOKEN: 0x20000AD
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A2  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IPointerClickHandler
TYPE:  interface
TOKEN: 0x20000AE
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A3  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IBeginDragHandler
TYPE:  interface
TOKEN: 0x20000AF
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A4  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IInitializePotentialDragHandler
TYPE:  interface
TOKEN: 0x20000B0
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A5  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IDragHandler
TYPE:  interface
TOKEN: 0x20000B1
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A6  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IEndDragHandler
TYPE:  interface
TOKEN: 0x20000B2
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A7  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IDropHandler
TYPE:  interface
TOKEN: 0x20000B3
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A8  System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IScrollHandler
TYPE:  interface
TOKEN: 0x20000B4
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A9  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IUpdateSelectedHandler
TYPE:  interface
TOKEN: 0x20000B5
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AA  System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.ISelectHandler
TYPE:  interface
TOKEN: 0x20000B6
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AB  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IDeselectHandler
TYPE:  interface
TOKEN: 0x20000B7
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AC  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.IMoveHandler
TYPE:  interface
TOKEN: 0x20000B8
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AD  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.ISubmitHandler
TYPE:  interface
TOKEN: 0x20000B9
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AE  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.ICancelHandler
TYPE:  interface
TOKEN: 0x20000BA
IMPLEMENTS: UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AF  System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.EventSystem
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x50
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.EventSystems.BaseInputModule>m_SystemInputModules  // 0x18
  private           UnityEngine.EventSystems.BaseInputModulem_CurrentInputModule  // 0x20
  private   static  System.Collections.Generic.List<UnityEngine.EventSystems.EventSystem>m_EventSystems  // static @ 0x0
  private           UnityEngine.GameObject          m_FirstSelected  // 0x28
  private           System.Boolean                  m_sendNavigationEvents  // 0x30
  private           System.Int32                    m_DragThreshold  // 0x34
  private           UnityEngine.GameObject          m_CurrentSelected  // 0x38
  private           System.Boolean                  m_HasFocus  // 0x40
  private           System.Boolean                  m_SelectionGuard  // 0x41
  private           UnityEngine.EventSystems.BaseEventDatam_DummyData  // 0x48
  private   static readonly System.Comparison<UnityEngine.EventSystems.RaycastResult>s_RaycastComparer  // static @ 0x8
  private   static  UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfigs_UIToolkitOverride  // static @ 0x10
PROPERTIES:
  current  get=0x03795B00  set=0x0B57C964
  sendNavigationEvents  get=0x04D865F0  set=0x04D86620
  pixelDragThreshold  get=0x04D86730  set=0x04D86760
  currentInputModule  get=0x04D862C0
  firstSelectedGameObject  get=0x04D86240  set=0x02FDB880
  currentSelectedGameObject  get=0x04D85A50
  lastSelectedGameObject  get=0x011EC580
  isFocused  get=0x02D480A0
  alreadySelecting  get=0x04D86F40
  baseEventDataCache  get=0x0B57C8F0
  isUIToolkitActiveEventSystem  get=0x04648650
  sendUIToolkitEvents  get=0x04647B00
  createUIToolkitPanelGameObjectsOnStart  get=0x04648500
METHODS:
  RVA=0x04D12950  token=0x60006BC  System.Void .ctor()
  RVA=0x03DFA5D0  token=0x60006BD  System.Void UpdateModules()
  RVA=0x03E9A200  token=0x60006BF  System.Void SetSelectedGameObject(UnityEngine.GameObject selected, UnityEngine.EventSystems.BaseEventData pointer)
  RVA=0x0B57C648  token=0x60006C1  System.Void SetSelectedGameObject(UnityEngine.GameObject selected)
  RVA=0x0389F8B0  token=0x60006C2  System.Int32 RaycastComparer(UnityEngine.EventSystems.RaycastResult lhs, UnityEngine.EventSystems.RaycastResult rhs)
  RVA=0x02FC7540  token=0x60006C3  System.Void RaycastAll(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> raycastResults)
  RVA=0x0B57C4C0  token=0x60006C4  System.Boolean IsPointerOverGameObject()
  RVA=0x0B57C440  token=0x60006C5  System.Boolean IsPointerOverGameObject(System.Int32 pointerId)
  RVA=0x0B57C67C  token=0x60006C9  System.Void SetUITookitEventSystemOverride(UnityEngine.EventSystems.EventSystem activeEventSystem, System.Boolean sendEvents, System.Boolean createPanelGameObjectsOnStart)
  RVA=0x0B57C200  token=0x60006CA  System.Void CreateUIToolkitPanelGameObject(UnityEngine.UIElements.BaseRuntimePanel panel)
  RVA=0x04647B70  token=0x60006CB  System.Void Start()
  RVA=0x0B57C4D0  token=0x60006CC  System.Void OnDestroy()
  RVA=0x04647A40  token=0x60006CD  System.Void OnEnable()
  RVA=0x0B57C560  token=0x60006CE  System.Void OnDisable()
  RVA=0x03795770  token=0x60006CF  System.Void TickModules()
  RVA=0x03F26150  token=0x60006D0  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x037952F0  token=0x60006D1  System.Void Update()
  RVA=0x04CB05B0  token=0x60006D2  System.Void ChangeEventModule(UnityEngine.EventSystems.BaseInputModule module)
  RVA=0x0B57C7C0  token=0x60006D3  System.String ToString()
  RVA=0x04B20F90  token=0x60006D4  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.EventSystems.EventTrigger
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerExitHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IInitializePotentialDragHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IDropHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.EventSystems.IUpdateSelectedHandler UnityEngine.EventSystems.ISelectHandler UnityEngine.EventSystems.IDeselectHandler UnityEngine.EventSystems.IMoveHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.EventSystems.ICancelHandler
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry>m_Delegates  // 0x18
PROPERTIES:
  delegates  get=0x0B57CCB8  set=0x05392C40
  triggers  get=0x0B57CCC0  set=0x05392C40
METHODS:
  RVA=0x05393520  token=0x60006DA  System.Void .ctor()
  RVA=0x0B57CAC0  token=0x60006DD  System.Void Execute(UnityEngine.EventSystems.EventTriggerType id, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CC48  token=0x60006DE  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC58  token=0x60006DF  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CBD8  token=0x60006E0  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CBE8  token=0x60006E1  System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC38  token=0x60006E2  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC68  token=0x60006E3  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC28  token=0x60006E4  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC88  token=0x60006E5  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CBC8  token=0x60006E6  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CC78  token=0x60006E7  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC18  token=0x60006E8  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0B57CCA8  token=0x60006E9  System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CC08  token=0x60006EA  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CBA8  token=0x60006EB  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CBF8  token=0x60006EC  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57CC98  token=0x60006ED  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CBB8  token=0x60006EE  System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
END_CLASS

CLASS: UnityEngine.EventSystems.EventTriggerType
TYPE:  sealed struct
TOKEN: 0x20000C2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.EventTriggerTypePointerEnter  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypePointerExit  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypePointerDown  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypePointerUp  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypePointerClick  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeDrag  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeDrop  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeScroll  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeUpdateSelected  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeSelect  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeDeselect  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeMove  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeInitializePotentialDrag  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeBeginDrag  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeEndDrag  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeSubmit  // const
  public    static  UnityEngine.EventSystems.EventTriggerTypeCancel  // const
METHODS:
END_CLASS

CLASS: UnityEngine.EventSystems.ExecuteEvents
TYPE:  static class
TOKEN: 0x20000C3
SIZE:  0x10
FIELDS:
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerMoveHandler>s_PointerMoveHandler  // static @ 0x0
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerEnterHandler>s_PointerEnterHandler  // static @ 0x8
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerExitHandler>s_PointerExitHandler  // static @ 0x10
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerDownHandler>s_PointerDownHandler  // static @ 0x18
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerUpHandler>s_PointerUpHandler  // static @ 0x20
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerClickHandler>s_PointerClickHandler  // static @ 0x28
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IInitializePotentialDragHandler>s_InitializePotentialDragHandler  // static @ 0x30
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IBeginDragHandler>s_BeginDragHandler  // static @ 0x38
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDragHandler>s_DragHandler  // static @ 0x40
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IEndDragHandler>s_EndDragHandler  // static @ 0x48
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDropHandler>s_DropHandler  // static @ 0x50
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IScrollHandler>s_ScrollHandler  // static @ 0x58
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IUpdateSelectedHandler>s_UpdateSelectedHandler  // static @ 0x60
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISelectHandler>s_SelectHandler  // static @ 0x68
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDeselectHandler>s_DeselectHandler  // static @ 0x70
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IMoveHandler>s_MoveHandler  // static @ 0x78
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISubmitHandler>s_SubmitHandler  // static @ 0x80
  private   static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ICancelHandler>s_CancelHandler  // static @ 0x88
  private   static readonly System.Collections.Generic.List<UnityEngine.Transform>s_InternalTransformList  // static @ 0x90
PROPERTIES:
  pointerMoveHandler  get=0x0B57D3E4
  pointerEnterHandler  get=0x0B57D37C
  pointerExitHandler  get=0x0B57D3B0
  pointerDownHandler  get=0x0B57D348
  pointerUpHandler  get=0x0B57D418
  pointerClickHandler  get=0x0B57D314
  initializePotentialDrag  get=0x0B57D274
  beginDragHandler  get=0x0B57D0AC
  dragHandler  get=0x0B57D1A0
  endDragHandler  get=0x0B57D224
  dropHandler  get=0x0B57D1D4
  scrollHandler  get=0x0B57D44C
  updateSelectedHandler  get=0x0B57D540
  selectHandler  get=0x0B57D49C
  deselectHandler  get=0x0B57D150
  moveHandler  get=0x0B57D2C4
  submitHandler  get=0x0B57D4EC
  cancelHandler  get=0x0B57D0FC
METHODS:
  RVA=-1  // generic def  token=0x60006F1  T ValidateEventData(UnityEngine.EventSystems.BaseEventData data)
  RVA=0x0B57CEA4  token=0x60006F2  System.Void Execute(UnityEngine.EventSystems.IPointerMoveHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0379AE30  token=0x60006F3  System.Void Execute(UnityEngine.EventSystems.IPointerEnterHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0379ADD0  token=0x60006F4  System.Void Execute(UnityEngine.EventSystems.IPointerExitHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0379AD10  token=0x60006F5  System.Void Execute(UnityEngine.EventSystems.IPointerDownHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0379AD70  token=0x60006F6  System.Void Execute(UnityEngine.EventSystems.IPointerUpHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0379ACB0  token=0x60006F7  System.Void Execute(UnityEngine.EventSystems.IPointerClickHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x04A26280  token=0x60006F8  System.Void Execute(UnityEngine.EventSystems.IInitializePotentialDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x037D64A0  token=0x60006F9  System.Void Execute(UnityEngine.EventSystems.IBeginDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x037D6750  token=0x60006FA  System.Void Execute(UnityEngine.EventSystems.IDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x037D6540  token=0x60006FB  System.Void Execute(UnityEngine.EventSystems.IEndDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CF58  token=0x60006FC  System.Void Execute(UnityEngine.EventSystems.IDropHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CD44  token=0x60006FD  System.Void Execute(UnityEngine.EventSystems.IScrollHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x03798760  token=0x60006FE  System.Void Execute(UnityEngine.EventSystems.IUpdateSelectedHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x04CA2A10  token=0x60006FF  System.Void Execute(UnityEngine.EventSystems.ISelectHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x04B6C870  token=0x6000700  System.Void Execute(UnityEngine.EventSystems.IDeselectHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57D038  token=0x6000701  System.Void Execute(UnityEngine.EventSystems.IMoveHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CF40  token=0x6000702  System.Void Execute(UnityEngine.EventSystems.ISubmitHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57CDB8  token=0x6000703  System.Void Execute(UnityEngine.EventSystems.ICancelHandler handler, UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x041AEF30  token=0x6000716  System.Void GetEventChain(UnityEngine.GameObject root, System.Collections.Generic.IList<UnityEngine.Transform> eventChain)
  RVA=-1  // generic def  token=0x6000717  System.Boolean Execute(UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> functor)
  RVA=-1  // generic def  token=0x6000718  UnityEngine.GameObject ExecuteHierarchy(UnityEngine.GameObject root, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> callbackFunction)
  RVA=-1  // generic def  token=0x6000719  System.Boolean ShouldSendToComponent(UnityEngine.Component component)
  RVA=-1  // generic def  token=0x600071A  System.Void GetEventList(UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.EventSystems.IEventSystemHandler> results)
  RVA=-1  // generic def  token=0x600071B  System.Boolean CanHandleEvent(UnityEngine.GameObject go)
  RVA=-1  // generic def  token=0x600071C  UnityEngine.GameObject GetEventHandler(UnityEngine.GameObject root)
  RVA=0x0468AC20  token=0x600071D  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.EventSystems.BaseInput
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x18
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
PROPERTIES:
  compositionString  get=0x04118260
  imeCompositionMode  get=0x0B57BC94  set=0x0539555C
  compositionCursorPos  get=0x0B57BC8C  set=0x0B57BCD4
  mousePresent  get=0x0B47AE10
  mousePosition  get=0x0B57BC9C
  mouseScrollDelta  get=0x0B57BCC4
  touchSupported  get=0x0B57BCCC
  touchCount  get=0x09F48384
METHODS:
  RVA=0x0B57BC68  token=0x6000728  System.Boolean GetMouseButtonDown(System.Int32 button)
  RVA=0x0B57BC74  token=0x6000729  System.Boolean GetMouseButtonUp(System.Int32 button)
  RVA=0x0B57BC80  token=0x600072A  System.Boolean GetMouseButton(System.Int32 button)
  RVA=0x0AA1FDE0  token=0x600072F  UnityEngine.Touch GetTouch(System.Int32 index)
  RVA=0x0B57BC5C  token=0x6000730  System.Single GetAxisRaw(System.String axisName)
  RVA=0x0B47AE04  token=0x6000731  System.Boolean GetButtonDown(System.String buttonName)
  RVA=0x05393520  token=0x6000732  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.EventSystems.BaseInputModule
TYPE:  abstract class
TOKEN: 0x20000C6
SIZE:  0x48
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  protected         System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>m_RaycastResultCache  // 0x18
  private           UnityEngine.EventSystems.AxisEventDatam_AxisEventData  // 0x20
  private           UnityEngine.EventSystems.EventSystemm_EventSystem  // 0x28
  private           UnityEngine.EventSystems.BaseEventDatam_BaseEventData  // 0x30
  protected         UnityEngine.EventSystems.BaseInputm_InputOverride  // 0x38
  private           UnityEngine.EventSystems.BaseInputm_DefaultInput  // 0x40
PROPERTIES:
  input  get=0x03795BA0
  inputOverride  get=0x04D85A50  set=0x046A91B0
  eventSystem  get=0x04D86240
METHODS:
  RVA=0x03DFA850  token=0x6000737  System.Void OnEnable()
  RVA=0x0B57BC3C  token=0x6000738  System.Void OnDisable()
  RVA=-1  // abstract  token=0x6000739  System.Void Process()
  RVA=0x033F34A0  token=0x600073A  UnityEngine.EventSystems.RaycastResult FindFirstRaycast(System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> candidates)
  RVA=0x0B57B478  token=0x600073B  UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(System.Single x, System.Single y)
  RVA=0x0B57B408  token=0x600073C  UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(System.Single x, System.Single y, System.Single deadZone)
  RVA=0x03F32D60  token=0x600073D  UnityEngine.GameObject FindCommonRoot(UnityEngine.GameObject g1, UnityEngine.GameObject g2)
  RVA=0x0B57B570  token=0x600073E  System.Void HandlePointerExitAndEnter(UnityEngine.EventSystems.PointerEventData currentPointerData, UnityEngine.GameObject newEnterTarget)
  RVA=0x0B57B488  token=0x600073F  UnityEngine.EventSystems.AxisEventData GetAxisEventData(System.Single x, System.Single y, System.Single moveDeadZone)
  RVA=0x03E9A450  token=0x6000740  UnityEngine.EventSystems.BaseEventData GetBaseEventData()
  RVA=0x012081B0  token=0x6000741  System.Boolean IsPointerOverGameObject(System.Int32 pointerId)
  RVA=0x03E148A0  token=0x6000742  System.Boolean ShouldActivateModule()
  RVA=0x041E1670  token=0x6000743  System.Void DeactivateModule()
  RVA=0x041E1670  token=0x6000744  System.Void ActivateModule()
  RVA=0x041E1670  token=0x6000745  System.Void UpdateModule()
  RVA=0x02FFF600  token=0x6000746  System.Boolean IsModuleSupported()
  RVA=0x0B57B38C  token=0x6000747  System.Int32 ConvertUIToolkitPointerId(UnityEngine.EventSystems.PointerEventData sourcePointerData)
  RVA=0x0445D4D0  token=0x6000748  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.EventSystems.PointerInputModule
TYPE:  abstract class
TOKEN: 0x20000C7
SIZE:  0x58
EXTENDS: UnityEngine.EventSystems.BaseInputModule
FIELDS:
  public    static  System.Int32                    kMouseLeftId  // const
  public    static  System.Int32                    kMouseRightId  // const
  public    static  System.Int32                    kMouseMiddleId  // const
  public    static  System.Int32                    kFakeTouchesId  // const
  protected         System.Collections.Generic.Dictionary<System.Int32,UnityEngine.EventSystems.PointerEventData>m_PointerData  // 0x48
  private   readonly UnityEngine.EventSystems.PointerInputModule.MouseStatem_MouseState  // 0x50
METHODS:
  RVA=0x0B581FF4  token=0x6000749  System.Boolean GetPointerData(System.Int32 id, UnityEngine.EventSystems.PointerEventData& data, System.Boolean create)
  RVA=0x0B582710  token=0x600074A  System.Void RemovePointerData(UnityEngine.EventSystems.PointerEventData data)
  RVA=0x0B5820E8  token=0x600074B  UnityEngine.EventSystems.PointerEventData GetTouchPointerEventData(UnityEngine.Touch input, System.Boolean& pressed, System.Boolean& released)
  RVA=0x0B58198C  token=0x600074C  System.Void CopyFromTo(UnityEngine.EventSystems.PointerEventData from, UnityEngine.EventSystems.PointerEventData to)
  RVA=0x0B58276C  token=0x600074D  UnityEngine.EventSystems.PointerEventData.FramePressState StateForMouseButton(System.Int32 buttonId)
  RVA=0x0B581C00  token=0x600074E  UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData()
  RVA=0x0B581C0C  token=0x600074F  UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData(System.Int32 id)
  RVA=0x0B581BD8  token=0x6000750  UnityEngine.EventSystems.PointerEventData GetLastPointerEventData(System.Int32 id)
  RVA=0x0A9CED1C  token=0x6000751  System.Boolean ShouldStartDrag(UnityEngine.Vector2 pressPos, UnityEngine.Vector2 currentPos, System.Single threshold, System.Boolean useDragThreshold)
  RVA=0x0B5826C4  token=0x6000752  System.Void ProcessMove(UnityEngine.EventSystems.PointerEventData pointerEvent)
  RVA=0x0B5824A4  token=0x6000753  System.Void ProcessDrag(UnityEngine.EventSystems.PointerEventData pointerEvent)
  RVA=0x0B58243C  token=0x6000754  System.Boolean IsPointerOverGameObject(System.Int32 pointerId)
  RVA=0x0B5817DC  token=0x6000755  System.Void ClearSelection()
  RVA=0x0B5827E4  token=0x6000756  System.String ToString()
  RVA=0x0B581B0C  token=0x6000757  System.Void DeselectIfSelectionChanged(UnityEngine.GameObject currentOverGo, UnityEngine.EventSystems.BaseEventData pointerEvent)
  RVA=0x0B582A78  token=0x6000758  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.EventSystems.StandaloneInputModule
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0xB8
EXTENDS: UnityEngine.EventSystems.PointerInputModule
FIELDS:
  private           System.Single                   m_PrevActionTime  // 0x58
  private           UnityEngine.Vector2             m_LastMoveVector  // 0x5c
  private           System.Int32                    m_ConsecutiveMoveCount  // 0x64
  private           UnityEngine.Vector2             m_LastMousePosition  // 0x68
  private           UnityEngine.Vector2             m_MousePosition  // 0x70
  private           UnityEngine.GameObject          m_CurrentFocusedGameObject  // 0x78
  private           UnityEngine.EventSystems.PointerEventDatam_InputPointerEvent  // 0x80
  private           System.String                   m_HorizontalAxis  // 0x88
  private           System.String                   m_VerticalAxis  // 0x90
  private           System.String                   m_SubmitButton  // 0x98
  private           System.String                   m_CancelButton  // 0xa0
  private           System.Single                   m_InputActionsPerSecond  // 0xa8
  private           System.Single                   m_RepeatDelay  // 0xac
  private           System.Boolean                  m_ForceModuleActive  // 0xb0
PROPERTIES:
  inputMode  get=0x011EC580
  allowActivationOnMobileDevice  get=0x04D88CC0  set=0x04D88D20
  forceModuleActive  get=0x04D88CC0  set=0x04D88D20
  inputActionsPerSecond  get=0x04D8E650  set=0x04D8E680
  repeatDelay  get=0x04D8E660  set=0x04D8E690
  horizontalAxis  get=0x04D861F0  set=0x05395D30
  verticalAxis  get=0x04D86220  set=0x06402330
  submitButton  get=0x04D85EE0  set=0x06352D0C
  cancelButton  get=0x04D86210  set=0x05391334
METHODS:
  RVA=0x0B586BD8  token=0x6000766  System.Void .ctor()
  RVA=0x02FFF600  token=0x6000778  System.Boolean ShouldIgnoreEventsOnNoFocus()
  RVA=0x0B586AC4  token=0x6000779  System.Void UpdateModule()
  RVA=0x0B5861EC  token=0x600077A  System.Void ReleaseMouse(UnityEngine.EventSystems.PointerEventData pointerEvent, UnityEngine.GameObject currentOverGo)
  RVA=0x0B5868A8  token=0x600077B  System.Boolean ShouldActivateModule()
  RVA=0x0B58407C  token=0x600077C  System.Void ActivateModule()
  RVA=0x0B5841A4  token=0x600077D  System.Void DeactivateModule()
  RVA=0x0B586094  token=0x600077E  System.Void Process()
  RVA=0x0B584ACC  token=0x600077F  System.Boolean ProcessTouchEvents()
  RVA=0x0B584D08  token=0x6000780  System.Void ProcessTouchPress(UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released)
  RVA=0x0B586670  token=0x6000781  System.Boolean SendSubmitEventToSelectedObject()
  RVA=0x0B5841AC  token=0x6000782  UnityEngine.Vector2 GetRawMoveVector()
  RVA=0x0B586468  token=0x6000783  System.Boolean SendMoveEventToSelectedObject()
  RVA=0x0B584518  token=0x6000784  System.Void ProcessMouseEvent()
  RVA=0x012081B0  token=0x6000785  System.Boolean ForceAutoSelect()
  RVA=0x0B5842B0  token=0x6000786  System.Void ProcessMouseEvent(System.Int32 id)
  RVA=0x0B5867D4  token=0x6000787  System.Boolean SendUpdateEventToSelectedObject()
  RVA=0x0B5845E0  token=0x6000788  System.Void ProcessMousePress(UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData data)
  RVA=0x04D862A0  token=0x6000789  UnityEngine.GameObject GetCurrentFocusedGameObject()
END_CLASS

CLASS: UnityEngine.EventSystems.TouchInputModule
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0x78
EXTENDS: UnityEngine.EventSystems.PointerInputModule
FIELDS:
  private           UnityEngine.Vector2             m_LastMousePosition  // 0x58
  private           UnityEngine.Vector2             m_MousePosition  // 0x60
  private           UnityEngine.EventSystems.PointerEventDatam_InputPointerEvent  // 0x68
  private           System.Boolean                  m_ForceModuleActive  // 0x70
PROPERTIES:
  allowActivationOnStandalone  get=0x04D86A50  set=0x04D86A80
  forceModuleActive  get=0x04D86A50  set=0x04D86A80
METHODS:
  RVA=0x0B5888BC  token=0x600078B  System.Void .ctor()
  RVA=0x0B588764  token=0x6000790  System.Void UpdateModule()
  RVA=0x0B586D9C  token=0x6000791  System.Boolean IsModuleSupported()
  RVA=0x0B588410  token=0x6000792  System.Boolean ShouldActivateModule()
  RVA=0x0B588898  token=0x6000793  System.Boolean UseFakeInput()
  RVA=0x0B58832C  token=0x6000794  System.Void Process()
  RVA=0x0B586CAC  token=0x6000795  System.Void FakeTouches()
  RVA=0x0B586DCC  token=0x6000796  System.Void ProcessTouchEvents()
  RVA=0x0B586FE0  token=0x6000797  System.Void ProcessTouchPress(UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released)
  RVA=0x0B5841A4  token=0x6000798  System.Void DeactivateModule()
  RVA=0x0B5884E8  token=0x6000799  System.String ToString()
END_CLASS

CLASS: UnityEngine.EventSystems.MoveDirection
TYPE:  sealed struct
TOKEN: 0x20000D0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.EventSystems.MoveDirectionLeft  // const
  public    static  UnityEngine.EventSystems.MoveDirectionUp  // const
  public    static  UnityEngine.EventSystems.MoveDirectionRight  // const
  public    static  UnityEngine.EventSystems.MoveDirectionDown  // const
  public    static  UnityEngine.EventSystems.MoveDirectionNone  // const
METHODS:
END_CLASS

CLASS: UnityEngine.EventSystems.RaycasterManager
TYPE:  static class
TOKEN: 0x20000D1
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.List<UnityEngine.EventSystems.BaseRaycaster>s_Raycasters  // static @ 0x0
METHODS:
  RVA=0x040DD360  token=0x600079B  System.Void AddRaycaster(UnityEngine.EventSystems.BaseRaycaster baseRaycaster)
  RVA=0x0B583A58  token=0x600079C  System.Collections.Generic.List<UnityEngine.EventSystems.BaseRaycaster> GetRaycasters()
  RVA=0x040DD290  token=0x600079D  System.Void RemoveRaycasters(UnityEngine.EventSystems.BaseRaycaster baseRaycaster)
  RVA=0x04D171C0  token=0x600079E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.EventSystems.BaseRaycaster
TYPE:  abstract class
TOKEN: 0x20000D2
SIZE:  0x20
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           UnityEngine.EventSystems.BaseRaycasterm_RootRaycaster  // 0x18
PROPERTIES:
  eventCamera  get=-1  // abstract
  priority  get=0x011EC580
  sortOrderPriority  get=0x04DBF110
  renderOrderPriority  get=0x04DBF110
  rootRaycaster  get=0x0389FF30
METHODS:
  RVA=-1  // abstract  token=0x600079F  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
  RVA=0x0B57BFF8  token=0x60007A5  System.String ToString()
  RVA=0x040DD330  token=0x60007A6  System.Void OnEnable()
  RVA=0x040DD260  token=0x60007A7  System.Void OnDisable()
  RVA=0x05390C40  token=0x60007A8  System.Void OnCanvasHierarchyChanged()
  RVA=0x05390C40  token=0x60007A9  System.Void OnTransformParentChanged()
  RVA=0x05393520  token=0x60007AA  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.EventSystems.Physics2DRaycaster
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x48
EXTENDS: UnityEngine.EventSystems.PhysicsRaycaster
FIELDS:
  private           UnityEngine.RaycastHit2D[]      m_Hits  // 0x40
METHODS:
  RVA=0x0B5802C0  token=0x60007AB  System.Void .ctor()
  RVA=0x0B57FD24  token=0x60007AC  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
END_CLASS

CLASS: UnityEngine.EventSystems.PhysicsRaycaster
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x40
EXTENDS: UnityEngine.EventSystems.BaseRaycaster
FIELDS:
  protected static  System.Int32                    kNoEventMaskSet  // const
  protected         UnityEngine.Camera              m_EventCamera  // 0x20
  protected         UnityEngine.LayerMask           m_EventMask  // 0x28
  protected         System.Int32                    m_MaxRayIntersections  // 0x2c
  protected         System.Int32                    m_LastMaxRayIntersections  // 0x30
  private           UnityEngine.RaycastHit[]        m_Hits  // 0x38
PROPERTIES:
  eventCamera  get=0x0B580A28
  depth  get=0x0B5809A4
  finalEventMask  get=0x0B580AB8
  eventMask  get=0x011F2A90  set=0x011F2AA0
  maxRayIntersections  get=0x04D86710  set=0x04D86720
METHODS:
  RVA=0x0B580984  token=0x60007AD  System.Void .ctor()
  RVA=0x0B5802C8  token=0x60007B5  System.Boolean ComputeRayAndDistance(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Ray& ray, System.Int32& eventDisplayIndex, System.Single& distanceToClipPlane)
  RVA=0x0B580598  token=0x60007B6  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
END_CLASS

CLASS: UnityEngine.EventSystems.RaycastResult
TYPE:  sealed struct
TOKEN: 0x20000D6
SIZE:  0x58
FIELDS:
  private           UnityEngine.GameObject          m_GameObject  // 0x10
  public            UnityEngine.EventSystems.BaseRaycastermodule  // 0x18
  public            System.Single                   distance  // 0x20
  public            System.Single                   index  // 0x24
  public            System.Int32                    depth  // 0x28
  public            System.Int32                    sortingLayer  // 0x2c
  public            System.Int32                    sortingOrder  // 0x30
  public            UnityEngine.Vector3             worldPosition  // 0x34
  public            UnityEngine.Vector3             worldNormal  // 0x40
  public            UnityEngine.Vector2             screenPosition  // 0x4c
  public            System.Int32                    displayIndex  // 0x54
PROPERTIES:
  gameObject  get=0x02B76770  set=0x05392320
  isValid  get=0x0B5839E4
METHODS:
  RVA=0x0B58356C  token=0x60007BD  System.Void Clear()
  RVA=0x0B583614  token=0x60007BE  System.String ToString()
END_CLASS

CLASS: UnityEngine.EventSystems.UIBehaviour
TYPE:  abstract class
TOKEN: 0x20000D7
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60007BF  System.Void Awake()
  RVA=0x041E1670  token=0x60007C0  System.Void OnEnable()
  RVA=0x041E1670  token=0x60007C1  System.Void Start()
  RVA=0x041E1670  token=0x60007C2  System.Void OnDisable()
  RVA=0x041E1670  token=0x60007C3  System.Void OnDestroy()
  RVA=0x02FA7940  token=0x60007C4  System.Boolean IsActive()
  RVA=0x041E1670  token=0x60007C5  System.Void OnRectTransformDimensionsChange()
  RVA=0x041E1670  token=0x60007C6  System.Void OnBeforeTransformParentChanged()
  RVA=0x041E1670  token=0x60007C7  System.Void OnTransformParentChanged()
  RVA=0x041E1670  token=0x60007C8  System.Void OnDidApplyAnimationProperties()
  RVA=0x041E1670  token=0x60007C9  System.Void OnCanvasHierarchyChanged()
  RVA=0x031BD4A0  token=0x60007CA  System.Boolean IsDestroyed()
  RVA=0x05393520  token=0x60007CB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.AnimationTriggers
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x38
FIELDS:
  private   static  System.String                   kDefaultNormalAnimName  // const
  private   static  System.String                   kDefaultHighlightedAnimName  // const
  private   static  System.String                   kDefaultPressedAnimName  // const
  private   static  System.String                   kDefaultSelectedAnimName  // const
  private   static  System.String                   kDefaultDisabledAnimName  // const
  private           System.String                   m_NormalTrigger  // 0x10
  private           System.String                   m_HighlightedTrigger  // 0x18
  private           System.String                   m_PressedTrigger  // 0x20
  private           System.String                   m_SelectedTrigger  // 0x28
  private           System.String                   m_DisabledTrigger  // 0x30
PROPERTIES:
  normalTrigger  get=0x02B2ECC0  set=0x053908C0
  highlightedTrigger  get=0x0385B100  set=0x05392C40
  pressedTrigger  get=0x04D862C0  set=0x05390F40
  selectedTrigger  get=0x04D86240  set=0x02FDB880
  disabledTrigger  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x03697710  token=0x600000D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.Button
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x190
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.ISubmitHandler
FIELDS:
  private           UnityEngine.UI.Button.ButtonClickedEventm_OnClick  // 0x188
PROPERTIES:
  onClick  get=0x04D8DA20  set=0x06B8B3B4
METHODS:
  RVA=0x0B45FEEC  token=0x600000E  System.Void .ctor()
  RVA=0x0B45FE74  token=0x6000011  System.Void Press()
  RVA=0x0B45FDE8  token=0x6000012  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B45FE10  token=0x6000013  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B45FD8C  token=0x6000014  System.Collections.IEnumerator OnFinishSubmit()
END_CLASS

CLASS: UnityEngine.UI.CanvasUpdate
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.CanvasUpdate     Prelayout  // const
  public    static  UnityEngine.UI.CanvasUpdate     Layout  // const
  public    static  UnityEngine.UI.CanvasUpdate     PostLayout  // const
  public    static  UnityEngine.UI.CanvasUpdate     PreRender  // const
  public    static  UnityEngine.UI.CanvasUpdate     LatePreRender  // const
  public    static  UnityEngine.UI.CanvasUpdate     MaxUpdateValue  // const
METHODS:
END_CLASS

CLASS: UnityEngine.UI.ICanvasElement
TYPE:  interface
TOKEN: 0x2000009
FIELDS:
PROPERTIES:
  transform  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600001C  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=-1  // abstract  token=0x600001E  System.Void LayoutComplete()
  RVA=-1  // abstract  token=0x600001F  System.Void GraphicUpdateComplete()
  RVA=-1  // abstract  token=0x6000020  System.Boolean IsDestroyed()
END_CLASS

CLASS: UnityEngine.UI.CanvasUpdateRegistry
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x30
FIELDS:
  private   static  UnityEngine.UI.CanvasUpdateRegistrys_Instance  // static @ 0x0
  private           System.Boolean                  m_PerformingLayoutUpdate  // 0x10
  private           System.Boolean                  m_PerformingGraphicUpdate  // 0x11
  private           Unity.Profiling.ProfilerMarker[]m_CanvasUpdateProfilerStrings  // 0x18
  private   static  System.String                   m_CullingUpdateProfilerString  // const
  private   readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.ICanvasElement>m_LayoutRebuildQueue  // 0x20
  private   readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.ICanvasElement>m_GraphicRebuildQueue  // 0x28
  private   static readonly System.Comparison<UnityEngine.UI.ICanvasElement>s_SortLayoutFunction  // static @ 0x8
PROPERTIES:
  instance  get=0x0351BE00
METHODS:
  RVA=0x04821CD0  token=0x6000021  System.Void .ctor()
  RVA=0x031BE530  token=0x6000023  System.Boolean ObjectValidForUpdate(UnityEngine.UI.ICanvasElement element)
  RVA=0x031BD690  token=0x6000024  System.Void CleanInvalidItems()
  RVA=0x031BD940  token=0x6000025  System.Void PerformUpdate()
  RVA=0x03B9A390  token=0x6000026  System.Int32 ParentCount(UnityEngine.Transform child)
  RVA=0x03B9A250  token=0x6000027  System.Int32 SortLayoutList(UnityEngine.UI.ICanvasElement x, UnityEngine.UI.ICanvasElement y)
  RVA=0x039D3C60  token=0x6000028  System.Void RegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351B260  token=0x6000029  System.Boolean TryRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351C0B0  token=0x600002A  System.Boolean InternalRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351BB50  token=0x600002B  System.Void RegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0B45FF78  token=0x600002C  System.Boolean TryRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351BEE0  token=0x600002D  System.Boolean InternalRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351B370  token=0x600002E  System.Void UnRegisterCanvasElementForRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351ABA0  token=0x600002F  System.Void InternalUnRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351AD10  token=0x6000030  System.Void InternalUnRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element)
  RVA=0x0351AE80  token=0x6000031  System.Boolean IsRebuildingLayout()
  RVA=0x0351AEC0  token=0x6000032  System.Boolean IsRebuildingGraphics()
  RVA=0x04CED4F0  token=0x6000033  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.ColorBlock
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x68
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           UnityEngine.Color               m_NormalColor  // 0x10
  private           UnityEngine.Color               m_HighlightedColor  // 0x20
  private           UnityEngine.Color               m_PressedColor  // 0x30
  private           UnityEngine.Color               m_SelectedColor  // 0x40
  private           UnityEngine.Color               m_DisabledColor  // 0x50
  private           System.Single                   m_ColorMultiplier  // 0x60
  private           System.Single                   m_FadeDuration  // 0x64
  public    static  UnityEngine.UI.ColorBlock       defaultColorBlock  // static @ 0x0
PROPERTIES:
  normalColor  get=0x02B77630  set=0x04D8D270
  highlightedColor  get=0x02B2D510  set=0x04D8C980
  pressedColor  get=0x04D8C200  set=0x04D8C210
  selectedColor  get=0x04D87480  set=0x04D87490
  disabledColor  get=0x04D905C0  set=0x04D926F0
  colorMultiplier  get=0x04D885A0  set=0x04D88FF0
  fadeDuration  get=0x04D86E00  set=0x04D86E10
METHODS:
  RVA=0x04B2FF10  token=0x6000042  System.Void .cctor()
  RVA=0x0B460300  token=0x6000043  System.Boolean Equals(System.Object obj)
  RVA=0x0B4603B0  token=0x6000044  System.Boolean Equals(UnityEngine.UI.ColorBlock other)
  RVA=0x0B4605B8  token=0x6000045  System.Boolean op_Equality(UnityEngine.UI.ColorBlock point1, UnityEngine.UI.ColorBlock point2)
  RVA=0x0B46064C  token=0x6000046  System.Boolean op_Inequality(UnityEngine.UI.ColorBlock point1, UnityEngine.UI.ColorBlock point2)
  RVA=0x0B460534  token=0x6000047  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.UI.ClipperRegistry
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x20
FIELDS:
  private   static  UnityEngine.UI.ClipperRegistry  s_Instance  // static @ 0x0
  private   readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.IClipper>m_Clippers  // 0x10
  private   readonly Unity.Profiling.ProfilerMarker  clippersMarker  // 0x18
PROPERTIES:
  instance  get=0x031BCA30
METHODS:
  RVA=0x04CE72F0  token=0x6000048  System.Void .ctor()
  RVA=0x031BC990  token=0x600004A  System.Void Cull()
  RVA=0x0B45FFD0  token=0x600004B  System.Void CullOriginal()
  RVA=0x04424750  token=0x600004C  System.Void Register(UnityEngine.UI.IClipper c)
  RVA=0x03D91BD0  token=0x600004D  System.Void Unregister(UnityEngine.UI.IClipper c)
END_CLASS

CLASS: UnityEngine.UI.Clipping
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B460068  token=0x600004E  UnityEngine.Rect FindCullAndClipWorldRect(System.Collections.Generic.List<UnityEngine.UI.RectMask2D> rectMaskParents, System.Boolean& validRect)
END_CLASS

CLASS: UnityEngine.UI.IClipper
TYPE:  interface
TOKEN: 0x200000E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600004F  System.Void PerformClipping()
END_CLASS

CLASS: UnityEngine.UI.IClippable
TYPE:  interface
TOKEN: 0x200000F
FIELDS:
PROPERTIES:
  gameObject  get=-1  // abstract
  rectTransform  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000051  System.Void RecalculateClipping()
  RVA=-1  // abstract  token=0x6000053  System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect)
  RVA=-1  // abstract  token=0x6000054  System.Void SetClipRect(UnityEngine.Rect value, System.Boolean validRect)
  RVA=-1  // abstract  token=0x6000055  System.Void SetClipSoftness(UnityEngine.Vector2 clipSoftness)
  RVA=-1  // abstract  token=0x6000056  System.Void SetHGClipSoftness(UnityEngine.Vector4 hgClipSoftness)
END_CLASS

CLASS: UnityEngine.UI.RectangularVertexClipper
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x20
FIELDS:
  private   readonly UnityEngine.Vector3[]           m_WorldCorners  // 0x10
  private   readonly UnityEngine.Vector3[]           m_CanvasCorners  // 0x18
METHODS:
  RVA=0x0B46BC90  token=0x6000057  UnityEngine.Rect GetCanvasRect(UnityEngine.RectTransform t, UnityEngine.Canvas c)
  RVA=0x0B46BA44  token=0x6000058  UnityEngine.Rect GetCanvasRectAABB(UnityEngine.RectTransform t, UnityEngine.Canvas c)
  RVA=0x0404D5C0  token=0x6000059  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.DefaultControls
TYPE:  static class
TOKEN: 0x2000011
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.UI.DefaultControls.IFactoryControlsm_CurrentFactory  // static @ 0x0
  private   static  System.Single                   kWidth  // const
  private   static  System.Single                   kThickHeight  // const
  private   static  System.Single                   kThinHeight  // const
  private   static  UnityEngine.Vector2             s_ThickElementSize  // static @ 0x8
  private   static  UnityEngine.Vector2             s_ThinElementSize  // static @ 0x10
  private   static  UnityEngine.Vector2             s_ImageElementSize  // static @ 0x18
  private   static  UnityEngine.Color               s_DefaultSelectableColor  // static @ 0x20
  private   static  UnityEngine.Color               s_PanelColor  // static @ 0x30
  private   static  UnityEngine.Color               s_TextColor  // static @ 0x40
PROPERTIES:
  factory  get=0x0B464448
METHODS:
  RVA=0x0B463F70  token=0x600005B  UnityEngine.GameObject CreateUIElementRoot(System.String name, UnityEngine.Vector2 size, System.Type[] components)
  RVA=0x0B46401C  token=0x600005C  UnityEngine.GameObject CreateUIObject(System.String name, UnityEngine.GameObject parent, System.Type[] components)
  RVA=0x0B4640F0  token=0x600005D  System.Void SetDefaultTextValues(UnityEngine.UI.Text lbl)
  RVA=0x0B4640A0  token=0x600005E  System.Void SetDefaultColorTransitionValues(UnityEngine.UI.Selectable slider)
  RVA=0x0B464268  token=0x600005F  System.Void SetParentAndAlign(UnityEngine.GameObject child, UnityEngine.GameObject parent)
  RVA=0x0B4641A8  token=0x6000060  System.Void SetLayerRecursively(UnityEngine.GameObject go, System.Int32 layer)
  RVA=0x0B462214  token=0x6000061  UnityEngine.GameObject CreatePanel(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B4606E4  token=0x6000062  UnityEngine.GameObject CreateButton(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B46384C  token=0x6000063  UnityEngine.GameObject CreateText(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B461B78  token=0x6000064  UnityEngine.GameObject CreateImage(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B462444  token=0x6000065  UnityEngine.GameObject CreateRawImage(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B463100  token=0x6000066  UnityEngine.GameObject CreateSlider(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B462CAC  token=0x6000067  UnityEngine.GameObject CreateScrollbar(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B4639B0  token=0x6000068  UnityEngine.GameObject CreateToggle(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B461C84  token=0x6000069  UnityEngine.GameObject CreateInputField(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B460A40  token=0x600006A  UnityEngine.GameObject CreateDropdown(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B462550  token=0x600006B  UnityEngine.GameObject CreateScrollView(UnityEngine.UI.DefaultControls.Resources resources)
  RVA=0x0B464334  token=0x600006C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.Dropdown
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x1F8
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.EventSystems.ICancelHandler
FIELDS:
  private           UnityEngine.RectTransform       m_Template  // 0x188
  private           UnityEngine.UI.Text             m_CaptionText  // 0x190
  private           UnityEngine.UI.Image            m_CaptionImage  // 0x198
  private           UnityEngine.UI.Text             m_ItemText  // 0x1a0
  private           UnityEngine.UI.Image            m_ItemImage  // 0x1a8
  private           System.Int32                    m_Value  // 0x1b0
  private           UnityEngine.UI.Dropdown.OptionDataListm_Options  // 0x1b8
  private           UnityEngine.UI.Dropdown.DropdownEventm_OnValueChanged  // 0x1c0
  private           System.Single                   m_AlphaFadeSpeed  // 0x1c8
  private           UnityEngine.GameObject          m_Dropdown  // 0x1d0
  private           UnityEngine.GameObject          m_Blocker  // 0x1d8
  private           System.Collections.Generic.List<UnityEngine.UI.Dropdown.DropdownItem>m_Items  // 0x1e0
  private           UnityEngine.UI.CoroutineTween.TweenRunner<UnityEngine.UI.CoroutineTween.FloatTween>m_AlphaTweenRunner  // 0x1e8
  private           System.Boolean                  validTemplate  // 0x1f0
  private   static  System.Int32                    kHighSortingLayer  // const
  private   static  UnityEngine.UI.Dropdown.OptionDatas_NoOptionData  // static @ 0x0
PROPERTIES:
  template  get=0x04D8DA20  set=0x0B4673B8
  captionText  get=0x04D8DD50  set=0x0B46730C
  captionImage  get=0x04D8DED0  set=0x0B4672E4
  itemText  get=0x04D8DA10  set=0x0B46735C
  itemImage  get=0x04D8D9C0  set=0x0B467334
  options  get=0x0B4672C4  set=0x0B467384
  onValueChanged  get=0x04D8DA80  set=0x06B8B42C
  alphaFadeSpeed  get=0x04D8E980  set=0x04D8EA30
  value  get=0x04D8E930  set=0x0B4673E0
METHODS:
  RVA=0x0B465B2C  token=0x6000083  System.Void SetValueWithoutNotify(System.Int32 input)
  RVA=0x0B465B38  token=0x6000084  System.Void Set(System.Int32 value, System.Boolean sendCallback)
  RVA=0x0B46719C  token=0x6000085  System.Void .ctor()
  RVA=0x0B464D90  token=0x6000086  System.Void Awake()
  RVA=0x0B46709C  token=0x6000087  System.Void Start()
  RVA=0x0B465704  token=0x6000088  System.Void OnDisable()
  RVA=0x0B4658A8  token=0x6000089  System.Void RefreshShownValue()
  RVA=0x0B464B20  token=0x600008A  System.Void AddOptions(System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> options)
  RVA=0x0B464918  token=0x600008B  System.Void AddOptions(System.Collections.Generic.List<System.String> options)
  RVA=0x0B464A1C  token=0x600008C  System.Void AddOptions(System.Collections.Generic.List<UnityEngine.Sprite> options)
  RVA=0x0B464E78  token=0x600008D  System.Void ClearOptions()
  RVA=0x0B465C28  token=0x600008E  System.Void SetupTemplate(UnityEngine.Canvas rootCanvas)
  RVA=-1  // generic def  token=0x600008F  T GetOrAddComponent(UnityEngine.GameObject go)
  RVA=0x0B465798  token=0x6000090  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B465798  token=0x6000091  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B4656FC  token=0x6000092  System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B466270  token=0x6000093  System.Void Show()
  RVA=0x0B464ED8  token=0x6000094  UnityEngine.GameObject CreateBlocker(UnityEngine.Canvas rootCanvas)
  RVA=0x0B465420  token=0x6000095  System.Void DestroyBlocker(UnityEngine.GameObject blocker)
  RVA=0x0B46530C  token=0x6000096  UnityEngine.GameObject CreateDropdownList(UnityEngine.GameObject template)
  RVA=0x0B46546C  token=0x6000097  System.Void DestroyDropdownList(UnityEngine.GameObject dropdownList)
  RVA=0x0B465360  token=0x6000098  UnityEngine.UI.Dropdown.DropdownItem CreateItem(UnityEngine.UI.Dropdown.DropdownItem itemTemplate)
  RVA=0x041E1670  token=0x6000099  System.Void DestroyItem(UnityEngine.UI.Dropdown.DropdownItem item)
  RVA=0x0B46468C  token=0x600009A  UnityEngine.UI.Dropdown.DropdownItem AddItem(UnityEngine.UI.Dropdown.OptionData data, System.Boolean selected, UnityEngine.UI.Dropdown.DropdownItem itemTemplate, System.Collections.Generic.List<UnityEngine.UI.Dropdown.DropdownItem> items)
  RVA=0x0B464D04  token=0x600009B  System.Void AlphaFadeList(System.Single duration, System.Single alpha)
  RVA=0x0B464B88  token=0x600009C  System.Void AlphaFadeList(System.Single duration, System.Single start, System.Single end)
  RVA=0x0B465A8C  token=0x600009D  System.Void SetAlpha(System.Single alpha)
  RVA=0x0B4654B8  token=0x600009E  System.Void Hide()
  RVA=0x0B4653B4  token=0x600009F  System.Collections.IEnumerator DelayedDestroyDropdownList(System.Single delay)
  RVA=0x0B4655AC  token=0x60000A0  System.Void ImmediateDestroyDropdownList()
  RVA=0x0B4657A0  token=0x60000A1  System.Void OnSelectItem(UnityEngine.UI.Toggle toggle)
  RVA=0x0B46712C  token=0x60000A2  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.FontData
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x40
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           UnityEngine.Font                m_Font  // 0x10
  private           System.Int32                    m_FontSize  // 0x18
  private           UnityEngine.FontStyle           m_FontStyle  // 0x1c
  private           System.Boolean                  m_BestFit  // 0x20
  private           System.Int32                    m_MinSize  // 0x24
  private           System.Int32                    m_MaxSize  // 0x28
  private           UnityEngine.TextAnchor          m_Alignment  // 0x2c
  private           System.Boolean                  m_AlignByGeometry  // 0x30
  private           System.Boolean                  m_RichText  // 0x31
  private           UnityEngine.HorizontalWrapMode  m_HorizontalOverflow  // 0x34
  private           UnityEngine.VerticalWrapMode    m_VerticalOverflow  // 0x38
  private           System.Single                   m_LineSpacing  // 0x3c
PROPERTIES:
  defaultFontData  get=0x0B467438
  font  get=0x02B2ECC0  set=0x053908C0
  fontSize  get=0x011EF5B0  set=0x011EF9B0
  fontStyle  get=0x04D86310  set=0x04D86320
  bestFit  get=0x04D866B0  set=0x04D866D0
  minSize  get=0x011F0020  set=0x011F0030
  maxSize  get=0x011F2A90  set=0x011F2AA0
  alignment  get=0x04D86710  set=0x04D86720
  alignByGeometry  get=0x04D865F0  set=0x04D86620
  richText  get=0x04D86AB0  set=0x04D86AC0
  horizontalOverflow  get=0x04D86730  set=0x04D86760
  verticalOverflow  get=0x04D863B0  set=0x04D86430
  lineSpacing  get=0x04D863E0  set=0x04D86460
METHODS:
  RVA=0x041E1670  token=0x60000DB  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0B4673EC  token=0x60000DC  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x60000DD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.FontUpdateTracker
TYPE:  static class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Font,System.Collections.Generic.HashSet<UnityEngine.UI.Text>>m_Tracked  // static @ 0x0
METHODS:
  RVA=0x0B46761C  token=0x60000DE  System.Void TrackText(UnityEngine.UI.Text t)
  RVA=0x0B4674AC  token=0x60000DF  System.Void RebuildForFont(UnityEngine.Font f)
  RVA=0x0B46783C  token=0x60000E0  System.Void UntrackText(UnityEngine.UI.Text t)
  RVA=0x0B467A20  token=0x60000E1  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.Graphic
TYPE:  abstract class
TOKEN: 0x200001E
SIZE:  0xB0
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ICanvasElement
FIELDS:
  protected static  UnityEngine.Material            s_DefaultUI  // static @ 0x0
  protected static  UnityEngine.Texture2D           s_WhiteTexture  // static @ 0x8
  private   static  System.String                   s_DefaultMaterialName  // static @ 0x10
  private   static  System.String                   s_OverlayShaderKeyword  // static @ 0x18
  protected         UnityEngine.Material            m_Material  // 0x18
  private           UnityEngine.Color               m_Color  // 0x20
  protected         System.Boolean                  m_SkipLayoutUpdate  // 0x30
  protected         System.Boolean                  m_SkipMaterialUpdate  // 0x31
  private           System.Boolean                  m_RaycastTarget  // 0x32
  private           UnityEngine.Vector4             m_RaycastPadding  // 0x34
  private           System.Boolean                  m_RaycastIgnoreMask  // 0x44
  private           UnityEngine.RectTransform       m_RectTransform  // 0x48
  private           UnityEngine.CanvasRenderer      m_CanvasRenderer  // 0x50
  private           UnityEngine.Canvas              m_Canvas  // 0x58
  private           System.Boolean                  m_VertsDirty  // 0x60
  private           System.Boolean                  m_MaterialDirty  // 0x61
  protected         UnityEngine.Events.UnityAction  m_OnDirtyLayoutCallback  // 0x68
  protected         UnityEngine.Events.UnityAction  m_OnDirtyVertsCallback  // 0x70
  protected         UnityEngine.Events.UnityAction  m_OnDirtyMaterialCallback  // 0x78
  protected static  UnityEngine.Mesh                s_Mesh  // static @ 0x20
  private   static readonly UnityEngine.UI.VertexHelper     s_VertexHelper  // static @ 0x28
  protected         UnityEngine.Mesh                m_CachedMesh  // 0x80
  protected         UnityEngine.Vector2[]           m_CachedUvs  // 0x88
  private           UnityEngine.UI.CoroutineTween.TweenRunner<UnityEngine.UI.CoroutineTween.ColorTween>m_ColorTweenRunnerLazy  // 0x90
  protected         System.Boolean                  m_EnableRuntimeAtlas  // 0x98
  protected         UnityEngine.Texture             m_RuntimeAtlasTexture  // 0xa0
  private           System.Boolean                  <useLegacyMeshGeneration>k__BackingField  // 0xa8
PROPERTIES:
  defaultMaterialName  get=0x0B467EC4  set=0x0B467F64
  defaultGraphicMaterial  get=0x031BCDA0
  overlayShaderKeyword  get=0x0B467F14  set=0x0B467FF4
  color  get=0x04D8C200  set=0x03046910
  raycastTarget  get=0x04D86DC0  set=0x044550F0
  raycastPadding  get=0x04D89180  set=0x04D89190
  m_ColorTweenRunner  get=0x0343F2F0
  enableRuntimeAtlas  get=0x04D86C50  set=0x0B467FC4
  runtimeAtlasTexture  get=0x04D86210  set=0x0B468054
  useLegacyMeshGeneration  get=0x04D90720  set=0x04D90750
  depth  get=0x031BF800
  rectTransform  get=0x031C2170
  canvas  get=0x02FCB610
  canvasRenderer  get=0x031BFEC0
  defaultMaterial  get=0x031BCD70
  material  get=0x03E9D0E0  set=0x0425D120
  materialForRendering  get=0x031EE800
  mainTexture  get=0x04D394C0
  workerMesh  get=0x031F2480
METHODS:
  RVA=0x03697340  token=0x60000F4  System.Void .ctor()
  RVA=0x03D23600  token=0x60000F5  System.Void SetAllDirty()
  RVA=0x03C98E90  token=0x60000F6  System.Void SetLayoutDirty()
  RVA=0x0351B640  token=0x60000F7  System.Void SetVerticesDirty()
  RVA=0x0351B8D0  token=0x60000F8  System.Void SetMaterialDirty()
  RVA=0x0351AB00  token=0x60000F9  System.Void OnRectTransformDimensionsChange()
  RVA=0x035197A0  token=0x60000FA  System.Void OnBeforeTransformParentChanged()
  RVA=0x03F3AF00  token=0x60000FB  System.Void OnTransformParentChanged()
  RVA=0x02FA9D50  token=0x60000FF  System.Void CacheCanvas()
  RVA=0x031EBEA0  token=0x6000106  System.Void OnEnable()
  RVA=0x0351A480  token=0x6000107  System.Void OnDisable()
  RVA=0x03094210  token=0x6000108  System.Void OnDestroy()
  RVA=0x031C3F80  token=0x6000109  System.Void OnCanvasHierarchyChanged()
  RVA=0x031EAB40  token=0x600010A  System.Void OnCullingChanged()
  RVA=0x031BF970  token=0x600010B  System.Void Rebuild(UnityEngine.UI.CanvasUpdate update)
  RVA=0x041E1670  token=0x600010C  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x600010D  System.Void GraphicUpdateComplete()
  RVA=0x031BEC70  token=0x600010E  System.Void UpdateMaterial()
  RVA=0x031F2660  token=0x600010F  System.Void UpdateGeometry()
  RVA=0x031F1FC0  token=0x6000110  System.Void DoMeshGeneration()
  RVA=0x046923F0  token=0x6000111  System.Void DoLegacyMeshGeneration()
  RVA=0x041E1670  token=0x6000113  System.Void OnFillVBO(System.Collections.Generic.List<UnityEngine.UIVertex> vbo)
  RVA=0x04A27120  token=0x6000114  System.Void OnPopulateMesh(UnityEngine.Mesh m)
  RVA=0x03240F40  token=0x6000115  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x0B467B50  token=0x6000116  System.Void OnDidApplyAnimationProperties()
  RVA=0x041E1670  token=0x6000117  System.Void SetNativeSize()
  RVA=0x031F0020  token=0x6000118  System.Boolean Raycast(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x0B467B60  token=0x6000119  UnityEngine.Vector2 PixelAdjustPoint(UnityEngine.Vector2 point)
  RVA=0x031C2550  token=0x600011A  UnityEngine.Rect GetPixelAdjustedRect()
  RVA=0x0343EE00  token=0x600011B  System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha)
  RVA=0x0343EE80  token=0x600011C  System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha, System.Boolean useRGB)
  RVA=0x04CC9850  token=0x600011D  UnityEngine.Color CreateColorFromAlpha(System.Single alpha)
  RVA=0x04CC9780  token=0x600011E  System.Void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale)
  RVA=0x0B467C94  token=0x600011F  System.Void RegisterDirtyLayoutCallback(UnityEngine.Events.UnityAction action)
  RVA=0x0B467DAC  token=0x6000120  System.Void UnregisterDirtyLayoutCallback(UnityEngine.Events.UnityAction action)
  RVA=0x02EDDB70  token=0x6000121  System.Void RegisterDirtyVerticesCallback(UnityEngine.Events.UnityAction action)
  RVA=0x02EDD170  token=0x6000122  System.Void UnregisterDirtyVerticesCallback(UnityEngine.Events.UnityAction action)
  RVA=0x0B467D20  token=0x6000123  System.Void RegisterDirtyMaterialCallback(UnityEngine.Events.UnityAction action)
  RVA=0x0B467E38  token=0x6000124  System.Void UnregisterDirtyMaterialCallback(UnityEngine.Events.UnityAction action)
  RVA=0x04B473C0  token=0x6000125  System.Void .cctor()
  RVA=0x053953F4  token=0x6000126  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.GraphicRaycaster
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x40
EXTENDS: UnityEngine.EventSystems.BaseRaycaster
FIELDS:
  protected static  System.Int32                    kNoEventMaskSet  // const
  private           System.Boolean                  m_IgnoreReversedGraphics  // 0x20
  private           UnityEngine.UI.GraphicRaycaster.BlockingObjectsm_BlockingObjects  // 0x24
  protected         UnityEngine.LayerMask           m_BlockingMask  // 0x28
  private           UnityEngine.Canvas              m_Canvas  // 0x30
  private           System.Collections.Generic.List<UnityEngine.UI.Graphic>m_RaycastResults  // 0x38
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.Graphic>s_SortedGraphics  // static @ 0x0
PROPERTIES:
  sortOrderPriority  get=0x03DC3FB0
  renderOrderPriority  get=0x03DC4A00
  ignoreReversedGraphics  get=0x04D866B0  set=0x04D866D0
  blockingObjects  get=0x011F0020  set=0x011F0030
  blockingMask  get=0x011F2A90  set=0x011F2AA0
  canvas  get=0x02FC99B0
  eventCamera  get=0x03096CF0
METHODS:
  RVA=0x043B7CB0  token=0x600012F  System.Void .ctor()
  RVA=0x02FC7E70  token=0x6000131  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
  RVA=0x031BEDF0  token=0x6000133  System.Void Raycast(UnityEngine.Canvas canvas, UnityEngine.Camera eventCamera, UnityEngine.Vector2 pointerPosition, System.Collections.Generic.IList<UnityEngine.UI.Graphic> foundGraphics, System.Collections.Generic.List<UnityEngine.UI.Graphic> results)
  RVA=0x043B7B80  token=0x6000134  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.GraphicRegistry
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x20
FIELDS:
  private   static  UnityEngine.UI.GraphicRegistry  s_Instance  // static @ 0x0
  private   readonly System.Collections.Generic.Dictionary<UnityEngine.Canvas,UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.Graphic>>m_Graphics  // 0x10
  private   readonly System.Collections.Generic.Dictionary<UnityEngine.Canvas,UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.Graphic>>m_RaycastableGraphics  // 0x18
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.Graphic>s_EmptyList  // static @ 0x8
PROPERTIES:
  instance  get=0x02FC7CD0
METHODS:
  RVA=0x04A20380  token=0x6000138  System.Void .ctor()
  RVA=0x031EBFF0  token=0x600013A  System.Void RegisterGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic)
  RVA=0x031EC3A0  token=0x600013B  System.Void RegisterRaycastGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic)
  RVA=0x0351A630  token=0x600013C  System.Void UnregisterGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic)
  RVA=0x0351A8A0  token=0x600013D  System.Void UnregisterRaycastGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic)
  RVA=0x0B467AAC  token=0x600013E  System.Collections.Generic.IList<UnityEngine.UI.Graphic> GetGraphicsForCanvas(UnityEngine.Canvas canvas)
  RVA=0x02FC7830  token=0x600013F  System.Collections.Generic.IList<UnityEngine.UI.Graphic> GetRaycastableGraphicsForCanvas(UnityEngine.Canvas canvas)
  RVA=0x043B7C10  token=0x6000140  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.HGImage
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x190
EXTENDS: UnityEngine.UI.Image
FIELDS:
METHODS:
  RVA=0x031BB800  token=0x6000141  System.Void UpdateGeometry()
  RVA=0x031C29F0  token=0x6000142  System.Boolean CanUseBatchedPath()
  RVA=0x031E85B0  token=0x6000143  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill)
  RVA=0x041E1670  token=0x6000144  System.Void OnPopulateMeshFallback(UnityEngine.UI.VertexHelper toFill)
  RVA=0x03696D70  token=0x6000145  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.HGUIBatchInit
TYPE:  static class
TOKEN: 0x2000024
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04821F30  token=0x6000146  System.Void Initialize()
END_CLASS

CLASS: UnityEngine.UI.HGUIBatchClipper
TYPE:  static class
TOKEN: 0x2000025
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  <Enabled>k__BackingField  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  s_BatchPrepareMarker  // static @ 0x8
  private   static readonly Unity.Profiling.ProfilerMarker  s_BatchComputeMarker  // static @ 0x10
  private   static readonly Unity.Profiling.ProfilerMarker  s_BatchDispatchMarker  // static @ 0x18
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.RectMask2D>s_BatchMasks  // static @ 0x20
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.MaskableGraphic>s_FlatTargets  // static @ 0x28
PROPERTIES:
  Enabled  get=0x0B468124  set=0x0B468174
METHODS:
  RVA=0x031C0310  token=0x6000149  System.Void CullBatched(System.Collections.Generic.IList<UnityEngine.UI.IClipper> clippers)
  RVA=0x0404D2D0  token=0x600014A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.HGImageBatchHelper
TYPE:  static class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  <Enabled>k__BackingField  // static @ 0x0
  private   static  System.Int32                    kMaxBatchImageCount  // const
  private   static  Unity.Collections.NativeArray<UnityEngine.UI.HGImageBatchBindings.HGBatchImageInput>s_BatchBuffer  // static @ 0x8
  private   static  System.Int32                    s_BatchCount  // static @ 0x18
  private   static readonly Unity.Profiling.ProfilerMarker  s_BatchFlushMarker  // static @ 0x20
PROPERTIES:
  Enabled  get=0x0B468080  set=0x0B4680D0
METHODS:
  RVA=0x031BB3F0  token=0x600014D  System.Void Enqueue(UnityEngine.UI.Image image)
  RVA=0x031BC560  token=0x600014E  System.Void Flush()
  RVA=0x031BB740  token=0x600014F  System.Void EnsureBatchBuffer()
  RVA=0x031BB290  token=0x6000150  System.Void FillHGImageData(UnityEngine.UI.Image image, UnityEngine.CanvasRenderer.HGImageData& data)
  RVA=0x04D30690  token=0x6000151  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.IGraphicEnabledDisabled
TYPE:  interface
TOKEN: 0x2000027
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000152  System.Void OnSiblingGraphicEnabledDisabled()
END_CLASS

CLASS: UnityEngine.UI.Image
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x190
EXTENDS: UnityEngine.UI.MaskableGraphic
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver UnityEngine.UI.ILayoutElement UnityEngine.ICanvasRaycastFilter
FIELDS:
  protected static  UnityEngine.Material            s_ETC1DefaultUI  // static @ 0x0
  private           UnityEngine.Sprite              m_Sprite  // 0xe8
  protected         UnityEngine.Rect                m_RuntimeAtlasRect  // 0xf0
  protected         UnityEngine.Rect                m_RuntimeAtlasTextureRect  // 0x100
  protected         UnityEngine.Vector4             m_RuntimeAtlasBorder  // 0x110
  protected         System.Single                   m_RuntimeAtlasPixelsPerUnit  // 0x120
  private           UnityEngine.Sprite              m_OverrideSprite  // 0x128
  private           UnityEngine.UI.Image.Type       m_Type  // 0x130
  private           System.Boolean                  m_PreserveAspect  // 0x134
  private           System.Boolean                  m_FillCenter  // 0x135
  private           UnityEngine.UI.Image.FillMethod m_FillMethod  // 0x138
  private           System.Single                   m_FillAmount  // 0x13c
  private           System.Boolean                  m_FillClockwise  // 0x140
  private           System.Int32                    m_FillOrigin  // 0x144
  private           System.Single                   m_AlphaHitTestMinimumThreshold  // 0x148
  private           System.Boolean                  m_Tracked  // 0x14c
  private           System.Boolean                  m_UseSpriteMesh  // 0x14d
  private           System.Single                   m_PixelsPerUnitMultiplier  // 0x150
  private           System.Single                   m_CachedReferencePixelsPerUnit  // 0x154
  public            UnityEngine.UI.Image.SlicedSpriteParamsslicedSpriteParams  // 0x158
  private           System.Boolean                  bAwakeOnce  // 0x17c
  private   static readonly UnityEngine.Vector2[]           s_VertScratch  // static @ 0x8
  private   static readonly UnityEngine.Vector2[]           s_UVScratch  // static @ 0x10
  private   static  System.Int32                    QuadCount  // const
  private   static readonly UnityEngine.Vector3[]           s_Xy  // static @ 0x18
  private   static readonly UnityEngine.Vector3[]           s_Uv  // static @ 0x20
  private   static  System.Collections.Generic.List<UnityEngine.UI.Image>m_TrackedTexturelessImages  // static @ 0x28
  private   static  System.Boolean                  s_Initialized  // static @ 0x30
  protected static  System.Int32                    VALIDATE_MAX_VERT_COUNT  // const
  private           UnityEngine.UI.Image.EDynamicAtlasActiveModem_DynamicAtlasActiveMode  // 0x17d
  protected         System.Boolean                  _packIntoRuntimeAtlas  // 0x17e
  protected         System.Int32                    m_panelLevel  // 0x180
  public            System.Boolean                  keepOriginUV  // 0x184
  private           System.Action                   dynamicAtlasCallBack  // 0x188
  private   static  System.Boolean                  _sDynamicAtlasInitialized  // static @ 0x31
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.Image>s_FreeList  // static @ 0x38
  private   static readonly System.Collections.Generic.HashSet<UnityEngine.UI.Image>s_TrackedDynamicAtlasImages  // static @ 0x40
  private   static readonly System.Collections.Generic.List<UnityEngine.UI.Image>s_FreeListRevert  // static @ 0x48
  private   static readonly System.Collections.Generic.HashSet<UnityEngine.UI.Image>s_TrackedDynamicAtlasSpriteRevert  // static @ 0x50
  public    static  System.Boolean                  s_AtlasV2  // static @ 0x58
PROPERTIES:
  hasCullOverride  get=0x012081B0
  runtimeAtlasRect  get=0x04DA1270  set=0x04DA1320
  runtimeAtlasTextureRect  get=0x04DA1250  set=0x04DA12F0
  runtimeAtlasBorder  get=0x04DA1240  set=0x04DA12E0
  runtimeAtlasPixelsPerUnit  get=0x04DBD7E0  set=0x04DBD820
  sprite  get=0x04D88590  set=0x031EA420
  enableRuntimeAtlasRaw  get=0x04D86C50
  enableRuntimeAtlas  get=0x03C04DB0  set=0x0B467FC4
  overrideSprite  get=0x053914BC  set=0x0B46B824
  activeSprite  get=0x02FC95E0
  type  get=0x04D8D520  set=0x0B46B910
  preserveAspect  get=0x04DA7910  set=0x0B46B8B0
  fillCenter  get=0x04DBD7C0  set=0x0B46B6A4
  fillMethod  get=0x04D8D0B0  set=0x0B46B764
  fillAmount  get=0x04DA7940  set=0x02F54FD0
  fillClockwise  get=0x04D8E4D0  set=0x0B46B704
  fillOrigin  get=0x04DBD7D0  set=0x0B46B7C8
  eventAlphaThreshold  get=0x04DBD7A0  set=0x04DBD800
  alphaHitTestMinimumThreshold  get=0x04D90E70  set=0x04D90E80
  useSpriteMesh  get=0x04DBD7F0  set=0x0B46B96C
  defaultETC1GraphicMaterial  get=0x0B46B5DC
  mainTexture  get=0x031EDF80
  hasBorder  get=0x031CE170
  pixelsPerUnitMultiplier  get=0x04DA7AE0  set=0x0B46B88C
  pixelsPerUnit  get=0x031C4370
  multipliedPixelsPerUnit  get=0x031CD2B0
  material  get=0x031BCAB0  set=0x0425D110
  minWidth  get=0x04D879D0
  preferredWidth  get=0x031E9C70
  flexibleWidth  get=0x04DA9E10
  minHeight  get=0x04D879D0
  preferredHeight  get=0x031E99C0
  flexibleHeight  get=0x04DA9E10
  layoutPriority  get=0x011EC580
  DynamicAtlasActiveMode  get=0x04DBD790
METHODS:
  RVA=0x04695BF0  token=0x6000161  System.Void ReleaseSprite()
  RVA=0x03093960  token=0x6000162  System.Void _OnSpriteChanged(UnityEngine.Sprite oldOne)
  RVA=0x04DBD780  token=0x6000163  System.Void DisableSpriteOptimizations()
  RVA=0x03696E30  token=0x600017B  System.Void .ctor()
  RVA=0x041E1670  token=0x6000185  System.Void OnBeforeSerialize()
  RVA=0x03C82850  token=0x6000186  System.Void OnAfterDeserialize()
  RVA=0x0B46ACF0  token=0x6000187  System.Void PreserveSpriteAspectRatio(UnityEngine.Rect& rect, UnityEngine.Vector2 spriteSize)
  RVA=0x031E8D40  token=0x6000188  UnityEngine.Vector4 GetDrawingDimensions(System.Boolean shouldPreserveAspect)
  RVA=0x031EA1B0  token=0x6000189  System.Void SetNativeSize()
  RVA=0x031E85F0  token=0x600018A  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill)
  RVA=0x031ED7C0  token=0x600018B  System.Void TrackSprite()
  RVA=0x031EBCB0  token=0x600018C  System.Void OnEnable()
  RVA=0x0351A2F0  token=0x600018D  System.Void OnDisable()
  RVA=0x03093B40  token=0x600018E  System.Void Awake()
  RVA=0x03094110  token=0x600018F  System.Void OnDestroy()
  RVA=0x031BD110  token=0x6000190  System.Void UpdateMaterial()
  RVA=0x031C3A40  token=0x6000191  System.Void OnCanvasHierarchyChanged()
  RVA=0x031E87D0  token=0x6000192  System.Void GenerateSimpleSprite(UnityEngine.UI.VertexHelper vh, System.Boolean lPreserveAspect)
  RVA=0x0B46A364  token=0x6000193  System.Void GenerateSprite(UnityEngine.UI.VertexHelper vh, System.Boolean lPreserveAspect)
  RVA=0x031CC8E0  token=0x6000194  System.Void GenerateSlicedSprite(UnityEngine.UI.VertexHelper toFill)
  RVA=0x031CECC0  token=0x6000195  System.Void GenerateTiledSprite(UnityEngine.UI.VertexHelper toFill)
  RVA=0x0B469DFC  token=0x6000196  System.Void GenerateMirrorSprite(UnityEngine.UI.VertexHelper toFill)
  RVA=0x0B469468  token=0x6000197  System.Void GenerateMirrorQuarter(UnityEngine.UI.VertexHelper toFill)
  RVA=0x0B4681C8  token=0x6000198  System.Void AddQuad(UnityEngine.UI.VertexHelper vertexHelper, UnityEngine.Vector3[] quadPositions, UnityEngine.Color32 color, UnityEngine.Vector3[] quadUVs)
  RVA=0x032407C0  token=0x6000199  System.Void AddQuad(UnityEngine.UI.VertexHelper vertexHelper, UnityEngine.Vector2 posMin, UnityEngine.Vector2 posMax, UnityEngine.Color32 color, UnityEngine.Vector2 uvMin, UnityEngine.Vector2 uvMax)
  RVA=0x031CE290  token=0x600019A  UnityEngine.Vector4 GetAdjustedBorders(UnityEngine.Vector4 border, UnityEngine.Rect adjustedRect)
  RVA=0x0B4682EC  token=0x600019B  System.Void GenerateFilledSprite(UnityEngine.UI.VertexHelper toFill, System.Boolean preserveAspect)
  RVA=0x0B46ADD8  token=0x600019C  System.Boolean RadialCut(UnityEngine.Vector3[] xy, UnityEngine.Vector3[] uv, System.Single fill, System.Boolean invert, System.Int32 corner)
  RVA=0x0B46AF08  token=0x600019D  System.Void RadialCut(UnityEngine.Vector3[] xy, System.Single cos, System.Single sin, System.Boolean invert, System.Int32 corner)
  RVA=0x041E1670  token=0x600019E  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x600019F  System.Void CalculateLayoutInputVertical()
  RVA=0x03093020  token=0x60001A7  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 screenPoint, UnityEngine.Camera eventCamera)
  RVA=0x03093540  token=0x60001A8  UnityEngine.Vector2 MapCoordinate(UnityEngine.Vector2 local, UnityEngine.Rect rect)
  RVA=0x0B46B2E4  token=0x60001A9  System.Void RebuildImage(UnityEngine.U2D.SpriteAtlas spriteAtlas)
  RVA=0x0B46B46C  token=0x60001AA  System.Void TrackImage(UnityEngine.UI.Image g)
  RVA=0x0B46B56C  token=0x60001AB  System.Void UnTrackImage(UnityEngine.UI.Image g)
  RVA=0x03E51A10  token=0x60001AC  System.Void OnDidApplyAnimationProperties()
  RVA=0x0B46AC48  token=0x60001AD  UnityEngine.Vector4 GetRuntimeAtlasSpritePadding()
  RVA=0x0B46AB3C  token=0x60001AE  UnityEngine.Vector4 GetRuntimeAtlasSpriteOuterUV()
  RVA=0x0B46A910  token=0x60001AF  UnityEngine.Vector4 GetRuntimeAtlasSpriteInnerUV()
  RVA=0x0B46A7E4  token=0x60001B0  System.String GetParticlePath()
  RVA=0x03093BA0  token=0x60001B2  System.Void RegisterDynamicAtlas()
  RVA=0x031BC430  token=0x60001B3  System.Void UnRegisterDynamicAtlas(UnityEngine.Sprite _sprite)
  RVA=0x0309A1A0  token=0x60001B4  System.Void RegisterDynamicAtlasCallBack(System.Action cb)
  RVA=0x0309C080  token=0x60001B5  System.Void UnRegisterDynamicAtlasCallBack(System.Action cb)
  RVA=0x031BB860  token=0x60001B6  System.Void _CanvasRreWillRenderCanvases()
  RVA=0x048B4620  token=0x60001B7  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.IMask
TYPE:  interface
TOKEN: 0x2000032
FIELDS:
PROPERTIES:
  rectTransform  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60001B9  System.Boolean Enabled()
END_CLASS

CLASS: UnityEngine.UI.IMaskable
TYPE:  interface
TOKEN: 0x2000033
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BB  System.Void RecalculateMasking()
END_CLASS

CLASS: UnityEngine.UI.InputField
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x2A8
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IUpdateSelectedHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.UI.ICanvasElement UnityEngine.UI.ILayoutElement
FIELDS:
  protected         UnityEngine.TouchScreenKeyboard m_Keyboard  // 0x188
  private   static readonly System.Char[]                   kSeparators  // static @ 0x0
  private   static  System.Boolean                  s_IsQuestDevice  // static @ 0x8
  protected         UnityEngine.UI.Text             m_TextComponent  // 0x190
  protected         UnityEngine.UI.Graphic          m_Placeholder  // 0x198
  private           UnityEngine.UI.InputField.ContentTypem_ContentType  // 0x1a0
  private           UnityEngine.UI.InputField.InputTypem_InputType  // 0x1a4
  private           System.Char                     m_AsteriskChar  // 0x1a8
  private           UnityEngine.TouchScreenKeyboardTypem_KeyboardType  // 0x1ac
  private           UnityEngine.UI.InputField.LineTypem_LineType  // 0x1b0
  private           System.Boolean                  m_HideMobileInput  // 0x1b4
  private           UnityEngine.UI.InputField.CharacterValidationm_CharacterValidation  // 0x1b8
  private           System.Int32                    m_CharacterLimit  // 0x1bc
  private           UnityEngine.UI.InputField.SubmitEventm_OnSubmit  // 0x1c0
  private           UnityEngine.UI.InputField.EndEditEventm_OnDidEndEdit  // 0x1c8
  private           UnityEngine.UI.InputField.OnChangeEventm_OnValueChanged  // 0x1d0
  private           UnityEngine.UI.InputField.OnValidateInputm_OnValidateInput  // 0x1d8
  private           UnityEngine.Color               m_CaretColor  // 0x1e0
  private           System.Boolean                  m_CustomCaretColor  // 0x1f0
  private           UnityEngine.Color               m_SelectionColor  // 0x1f4
  protected         System.String                   m_Text  // 0x208
  private           System.Single                   m_CaretBlinkRate  // 0x210
  private           System.Int32                    m_CaretWidth  // 0x214
  private           System.Boolean                  m_ReadOnly  // 0x218
  private           System.Boolean                  m_ShouldActivateOnSelect  // 0x219
  protected         System.Int32                    m_CaretPosition  // 0x21c
  protected         System.Int32                    m_CaretSelectPosition  // 0x220
  private           UnityEngine.RectTransform       caretRectTrans  // 0x228
  protected         UnityEngine.UIVertex[]          m_CursorVerts  // 0x230
  private           UnityEngine.TextGenerator       m_InputTextCache  // 0x238
  private           UnityEngine.CanvasRenderer      m_CachedInputRenderer  // 0x240
  private           System.Boolean                  m_PreventFontCallback  // 0x248
  protected         UnityEngine.Mesh                m_Mesh  // 0x250
  private           System.Boolean                  m_AllowInput  // 0x258
  private           System.Boolean                  m_ShouldActivateNextUpdate  // 0x259
  private           System.Boolean                  m_UpdateDrag  // 0x25a
  private           System.Boolean                  m_DragPositionOutOfBounds  // 0x25b
  private   static  System.Single                   kHScrollSpeed  // const
  private   static  System.Single                   kVScrollSpeed  // const
  protected         System.Boolean                  m_CaretVisible  // 0x25c
  private           UnityEngine.Coroutine           m_BlinkCoroutine  // 0x260
  private           System.Single                   m_BlinkStartTime  // 0x268
  protected         System.Int32                    m_DrawStart  // 0x26c
  protected         System.Int32                    m_DrawEnd  // 0x270
  private           UnityEngine.Coroutine           m_DragCoroutine  // 0x278
  private           System.String                   m_OriginalText  // 0x280
  private           System.Boolean                  m_WasCanceled  // 0x288
  private           System.Boolean                  m_HasDoneFocusTransition  // 0x289
  private           UnityEngine.WaitForSecondsRealtimem_WaitForSecondsRealtime  // 0x290
  private           System.Boolean                  m_TouchKeyboardAllowsInPlaceEditing  // 0x298
  private           System.Boolean                  m_IsCompositionActive  // 0x299
  private   static  System.String                   kEmailSpecialCharacters  // const
  private   static  System.String                   kOculusQuestDeviceModel  // const
  private           UnityEngine.Event               m_ProcessingEvent  // 0x2a0
  private   static  System.Int32                    k_MaxTextLength  // const
PROPERTIES:
  input  get=0x0B570BE8
  compositionString  get=0x0B570B2C
  mesh  get=0x0B570CB8
  cachedInputTextGenerator  get=0x0B570A08
  shouldHideMobileInput  get=0x0B5710A4  set=0x0B5717B0
  shouldActivateOnSelect  get=0x0B571080  set=0x04DBEFA0
  text  get=0x04D8D8C0  set=0x0B571AB8
  isFocused  get=0x04D8FD90
  caretBlinkRate  get=0x04D928F0  set=0x0B571130
  caretWidth  get=0x04DB9DC0  set=0x0B571238
  textComponent  get=0x04D8DD50  set=0x0B5717FC
  placeholder  get=0x04D8DED0  set=0x0B57168C
  caretColor  get=0x0B570A84  set=0x0B571198
  customCaretColor  get=0x04DBEF70  set=0x0B5713E0
  selectionColor  get=0x04DBEF80  set=0x0B571724
  onEndEdit  get=0x04D8D790  set=0x0B571554
  onSubmit  get=0x04D8DA80  set=0x0B5715A0
  onValueChange  get=0x04D8D7B0  set=0x0B571638
  onValueChanged  get=0x04D8D7B0  set=0x0B571640
  onValidateInput  get=0x04D87870  set=0x0B5715EC
  characterLimit  get=0x04D91080  set=0x0B571294
  contentType  get=0x04DA3200  set=0x0B571384
  lineType  get=0x04D8E930  set=0x0B5714B0
  inputType  get=0x04DA7C40  set=0x0B5713F8
  touchScreenKeyboard  get=0x04D8DA20
  keyboardType  get=0x04D90790  set=0x0B571454
  characterValidation  get=0x04D8EAC0  set=0x0B571328
  readOnly  get=0x04D8EDA0  set=0x04D8EDB0
  multiLine  get=0x0B570D5C
  asteriskChar  get=0x04DBEF60  set=0x0B5710D4
  wasCanceled  get=0x04DBEF90
  caretPositionInternal  get=0x0B570AD4  set=0x0B5711D8
  caretSelectPositionInternal  get=0x0B570B00  set=0x0B571220
  hasSelection  get=0x0B570BB8
  caretPosition  get=0x0B570B00  set=0x0B5711F0
  selectionAnchorPosition  get=0x0B570AD4  set=0x0B5716D8
  selectionFocusPosition  get=0x0B570B00  set=0x0B571764
  clipboard  get=0x0B31DF8C  set=0x0B31E494
  minWidth  get=0x04D879D0
  preferredWidth  get=0x0B570F20
  flexibleWidth  get=0x04DA9E10
  minHeight  get=0x04D879D0
  preferredHeight  get=0x0B570D74
  flexibleHeight  get=0x04DA9E10
  layoutPriority  get=0x04D85B80
METHODS:
  RVA=0x0B570814  token=0x60001BE  System.Void .ctor()
  RVA=0x0B56F2F8  token=0x60001C7  System.Void SetTextWithoutNotify(System.String input)
  RVA=0x0B56F304  token=0x60001C8  System.Void SetText(System.String value, System.Boolean sendCallback)
  RVA=0x0B56A0E8  token=0x60001F5  System.Void ClampPos(System.Int32& pos)
  RVA=0x0B56DF84  token=0x6000201  System.Void OnEnable()
  RVA=0x0B56DB80  token=0x6000202  System.Void OnDisable()
  RVA=0x0B56A08C  token=0x6000203  System.Collections.IEnumerator CaretBlink()
  RVA=0x0B56ECE4  token=0x6000204  System.Void SetCaretVisible()
  RVA=0x0B56EC7C  token=0x6000205  System.Void SetCaretActive()
  RVA=0x0B56F7F0  token=0x6000206  System.Void UpdateCaretMaterial()
  RVA=0x0B56E394  token=0x6000207  System.Void OnFocus()
  RVA=0x0B56EAD4  token=0x6000208  System.Void SelectAll()
  RVA=0x0B56D9C4  token=0x6000209  System.Void MoveTextEnd(System.Boolean shift)
  RVA=0x0B56DA18  token=0x600020A  System.Void MoveTextStart(System.Boolean shift)
  RVA=0x0B56F6E0  token=0x600020D  System.Boolean TouchScreenKeyboardShouldBeUsed()
  RVA=0x0B56BEA0  token=0x600020E  System.Boolean InPlaceEditing()
  RVA=0x0B56BE44  token=0x600020F  System.Boolean InPlaceEditingChanged()
  RVA=0x0B56F740  token=0x6000210  System.Void UpdateCaretFromKeyboard()
  RVA=0x0B56C46C  token=0x6000211  System.Void LateUpdate()
  RVA=0x0B56E804  token=0x6000212  UnityEngine.Vector2 ScreenToLocal(UnityEngine.Vector2 screen)
  RVA=0x0B56BCE0  token=0x6000213  System.Int32 GetUnclampedCharacterLineFromPosition(UnityEngine.Vector2 pos, UnityEngine.TextGenerator generator)
  RVA=0x0B56B8F8  token=0x6000214  System.Int32 GetCharacterIndexFromPosition(UnityEngine.Vector2 pos)
  RVA=0x0B56D65C  token=0x6000215  System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56DB34  token=0x6000216  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56DDCC  token=0x6000217  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56D708  token=0x6000218  System.Collections.IEnumerator MouseDragOutsideRect(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56E1F4  token=0x6000219  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56E3C4  token=0x600021A  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56C098  token=0x600021B  UnityEngine.UI.InputField.EditState KeyPressed(UnityEngine.Event evt)
  RVA=0x0B56C038  token=0x600021C  System.Boolean IsValidChar(System.Char c)
  RVA=0x0B56E7EC  token=0x600021D  System.Void ProcessEvent(UnityEngine.Event e)
  RVA=0x0B56E634  token=0x600021E  System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B56BC54  token=0x600021F  System.String GetSelectedString()
  RVA=0x0B56A874  token=0x6000220  System.Int32 FindtNextWordBegin()
  RVA=0x0B56D918  token=0x6000221  System.Void MoveRight(System.Boolean shift, System.Boolean ctrl)
  RVA=0x0B56A930  token=0x6000222  System.Int32 FindtPrevWordBegin()
  RVA=0x0B56D86C  token=0x6000223  System.Void MoveLeft(System.Boolean shift, System.Boolean ctrl)
  RVA=0x0B56A5F0  token=0x6000224  System.Int32 DetermineCharacterLine(System.Int32 charPos, UnityEngine.TextGenerator generator)
  RVA=0x0B56D408  token=0x6000225  System.Int32 LineUpCharacterPosition(System.Int32 originalPos, System.Boolean goToFirstChar)
  RVA=0x0B56D218  token=0x6000226  System.Int32 LineDownCharacterPosition(System.Int32 originalPos, System.Boolean goToLastChar)
  RVA=0x0B56D860  token=0x6000227  System.Void MoveDown(System.Boolean shift)
  RVA=0x0B56D77C  token=0x6000228  System.Void MoveDown(System.Boolean shift, System.Boolean goToLastChar)
  RVA=0x0B56DA58  token=0x6000229  System.Void MoveUp(System.Boolean shift)
  RVA=0x0B56DA64  token=0x600022A  System.Void MoveUp(System.Boolean shift, System.Boolean goToFirstChar)
  RVA=0x0B56A418  token=0x600022B  System.Void Delete()
  RVA=0x0B56A9EC  token=0x600022C  System.Void ForwardSpace()
  RVA=0x0B569FAC  token=0x600022D  System.Void Backspace()
  RVA=0x0B56BEC4  token=0x600022E  System.Void Insert(System.Char c)
  RVA=0x0B5700F4  token=0x600022F  System.Void UpdateTouchKeyboardFromEditChanges()
  RVA=0x0B56EBEC  token=0x6000230  System.Void SendOnValueChangedAndUpdateLabel()
  RVA=0x0B56EC0C  token=0x6000231  System.Void SendOnValueChanged()
  RVA=0x0B56EB0C  token=0x6000232  System.Void SendOnEndEdit()
  RVA=0x0B56EB7C  token=0x6000233  System.Void SendOnSubmit()
  RVA=0x0B569814  token=0x6000234  System.Void Append(System.String input)
  RVA=0x0B569594  token=0x6000235  System.Void Append(System.Char input)
  RVA=0x0B56FCC0  token=0x6000236  System.Void UpdateLabel()
  RVA=0x0B56BFCC  token=0x6000237  System.Boolean IsSelectionVisible()
  RVA=0x0B56BBAC  token=0x6000238  System.Int32 GetLineStartPosition(UnityEngine.TextGenerator gen, System.Int32 line)
  RVA=0x0B56BAF0  token=0x6000239  System.Int32 GetLineEndPosition(UnityEngine.TextGenerator gen, System.Int32 line)
  RVA=0x0B56ED24  token=0x600023A  System.Void SetDrawRangeToContainCaretPosition(System.Int32 caretPos)
  RVA=0x0B56A9E4  token=0x600023B  System.Void ForceLabelUpdate()
  RVA=0x0B56D610  token=0x600023C  System.Void MarkGeometryAsDirty()
  RVA=0x0B56E7F4  token=0x600023D  System.Void Rebuild(UnityEngine.UI.CanvasUpdate update)
  RVA=0x041E1670  token=0x600023E  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x600023F  System.Void GraphicUpdateComplete()
  RVA=0x0B56F8D4  token=0x6000240  System.Void UpdateGeometry()
  RVA=0x0B56989C  token=0x6000241  System.Void AssignPositioningIfNeeded()
  RVA=0x0B56E218  token=0x6000242  System.Void OnFillVBO(UnityEngine.Mesh vbo)
  RVA=0x0B56AA88  token=0x6000243  System.Void GenerateCaret(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset)
  RVA=0x0B56A11C  token=0x6000244  System.Void CreateCursorVerts()
  RVA=0x0B56B148  token=0x6000245  System.Void GenerateHighlight(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset)
  RVA=0x0B57013C  token=0x6000246  System.Char Validate(System.String text, System.Int32 pos, System.Char ch)
  RVA=0x0B56947C  token=0x6000247  System.Void ActivateInputField()
  RVA=0x0B569180  token=0x6000248  System.Void ActivateInputFieldInternal()
  RVA=0x0B56E5B4  token=0x6000249  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B56E39C  token=0x600024A  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B56A284  token=0x600024B  System.Void DeactivateInputField()
  RVA=0x0B56DB58  token=0x600024C  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B56E5F0  token=0x600024D  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B56A6B0  token=0x600024E  System.Void EnforceContentType()
  RVA=0x0B56A7E0  token=0x600024F  System.Void EnforceTextHOverflow()
  RVA=0x0B56F678  token=0x6000250  System.Void SetToCustomIfContentTypeIsNot(UnityEngine.UI.InputField.ContentType[] allowedContentTypes)
  RVA=0x0B56F6C8  token=0x6000251  System.Void SetToCustom()
  RVA=0x0B56A68C  token=0x6000252  System.Void DoStateTransition(UnityEngine.UI.Selectable.SelectionState state, System.Boolean instant)
  RVA=0x041E1670  token=0x6000253  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x6000254  System.Void CalculateLayoutInputVertical()
  RVA=0x0B570778  token=0x600025C  System.Void .cctor()
  RVA=0x053953F4  token=0x600025D  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.AspectRatioFitter
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutSelfController UnityEngine.UI.ILayoutController
FIELDS:
  private           UnityEngine.UI.AspectRatioFitter.AspectModem_AspectMode  // 0x18
  private           System.Single                   m_AspectRatio  // 0x1c
  private           UnityEngine.RectTransform       m_Rect  // 0x20
  private           System.Boolean                  m_DelayedSetDirty  // 0x28
  private           System.Boolean                  m_DoesParentExist  // 0x29
  private           UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x2a
PROPERTIES:
  aspectMode  get=0x011EF5B0  set=0x0B568AEC
  aspectRatio  get=0x04D868F0  set=0x0B568B44
  rectTransform  get=0x0449D7D0
METHODS:
  RVA=0x04CC8BA0  token=0x6000276  System.Void .ctor()
  RVA=0x0449D220  token=0x6000277  System.Void OnEnable()
  RVA=0x0449D1E0  token=0x6000278  System.Void Start()
  RVA=0x0449D2B0  token=0x6000279  System.Void OnDisable()
  RVA=0x0B568A6C  token=0x600027A  System.Void OnTransformParentChanged()
  RVA=0x04703A40  token=0x600027B  System.Void Update()
  RVA=0x0449D4C0  token=0x600027C  System.Void OnRectTransformDimensionsChange()
  RVA=0x0449D4D0  token=0x600027D  System.Void UpdateRect()
  RVA=0x0449D2F0  token=0x600027E  System.Single GetSizeDeltaToProduceSize(System.Single size, System.Int32 axis)
  RVA=0x0449D3F0  token=0x600027F  UnityEngine.Vector2 GetParentSize()
  RVA=0x041E1670  token=0x6000280  System.Void SetLayoutHorizontal()
  RVA=0x041E1670  token=0x6000281  System.Void SetLayoutVertical()
  RVA=0x0449D4C0  token=0x6000282  System.Void SetDirty()
  RVA=0x0449D870  token=0x6000283  System.Boolean IsComponentValidOnObject()
  RVA=0x0449D210  token=0x6000284  System.Boolean IsAspectModeValid()
  RVA=0x04D867B0  token=0x6000285  System.Boolean DoesParentExists()
END_CLASS

CLASS: UnityEngine.UI.CanvasScaler
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x60
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           UnityEngine.UI.CanvasScaler.ScaleModem_UiScaleMode  // 0x18
  protected         System.Single                   m_ReferencePixelsPerUnit  // 0x1c
  protected         System.Single                   m_ScaleFactor  // 0x20
  protected         UnityEngine.Vector2             m_ReferenceResolution  // 0x24
  protected         UnityEngine.UI.CanvasScaler.ScreenMatchModem_ScreenMatchMode  // 0x2c
  protected         System.Single                   m_MatchWidthOrHeight  // 0x30
  private   static  System.Single                   kLogBase  // const
  protected         UnityEngine.UI.CanvasScaler.Unitm_PhysicalUnit  // 0x34
  protected         System.Single                   m_FallbackScreenDPI  // 0x38
  protected         System.Single                   m_DefaultSpriteDPI  // 0x3c
  protected         System.Single                   m_DynamicPixelsPerUnit  // 0x40
  private           UnityEngine.Canvas              m_Canvas  // 0x48
  private           System.Single                   m_PrevScaleFactor  // 0x50
  private           System.Single                   m_PrevReferencePixelsPerUnit  // 0x54
  protected         System.Boolean                  m_PresetInfoIsWorld  // 0x58
PROPERTIES:
  uiScaleMode  get=0x011EF5B0  set=0x011EF9B0
  referencePixelsPerUnit  get=0x04D868F0  set=0x04D86900
  scaleFactor  get=0x011EC8A0  set=0x0B568C58
  referenceResolution  get=0x04D88060  set=0x03C00870
  screenMatchMode  get=0x04D86710  set=0x04D86720
  matchWidthOrHeight  get=0x04D86E20  set=0x04D86E30
  physicalUnit  get=0x04D86730  set=0x04D86760
  fallbackScreenDPI  get=0x04D88360  set=0x04D88370
  defaultSpriteDPI  get=0x04D863E0  set=0x0B568C44
  dynamicPixelsPerUnit  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x044165B0  token=0x600029A  System.Void .ctor()
  RVA=0x03099A20  token=0x600029B  System.Void OnEnable()
  RVA=0x02FC71A0  token=0x600029C  System.Void Canvas_preWillRenderCanvases()
  RVA=0x03099830  token=0x600029D  System.Void OnDisable()
  RVA=0x03BFDC20  token=0x600029E  System.Void HandleManually()
  RVA=0x02FC7330  token=0x600029F  System.Void Handle()
  RVA=0x030998B0  token=0x60002A0  System.Void HandleWorldCanvas()
  RVA=0x04D7EDB0  token=0x60002A1  System.Void HandleConstantPixelSize()
  RVA=0x02F108F0  token=0x60002A2  System.Void HandleScaleWithScreenSize()
  RVA=0x0B568BA0  token=0x60002A3  System.Void HandleConstantPhysicalSize()
  RVA=0x03099940  token=0x60002A4  System.Void SetScaleFactor(System.Single scaleFactor)
  RVA=0x03099910  token=0x60002A5  System.Void SetReferencePixelsPerUnit(System.Single referencePixelsPerUnit)
END_CLASS

CLASS: UnityEngine.UI.ContentSizeFitter
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutSelfController UnityEngine.UI.ILayoutController
FIELDS:
  protected         UnityEngine.UI.ContentSizeFitter.FitModem_HorizontalFit  // 0x18
  protected         UnityEngine.UI.ContentSizeFitter.FitModem_VerticalFit  // 0x1c
  private           UnityEngine.RectTransform       m_Rect  // 0x20
  private           UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x28
PROPERTIES:
  horizontalFit  get=0x011EF5B0  set=0x0B568C6C
  verticalFit  get=0x04D86310  set=0x0B568CC4
  rectTransform  get=0x031E91F0
METHODS:
  RVA=0x03695570  token=0x60002AB  System.Void .ctor()
  RVA=0x031E8550  token=0x60002AC  System.Void OnEnable()
  RVA=0x031E8510  token=0x60002AD  System.Void OnDisable()
  RVA=0x031E8550  token=0x60002AE  System.Void OnRectTransformDimensionsChange()
  RVA=0x031E9160  token=0x60002AF  System.Void HandleSelfFittingAlongAxis(System.Int32 axis)
  RVA=0x031E90F0  token=0x60002B0  System.Void SetLayoutHorizontal()
  RVA=0x053965AC  token=0x60002B1  System.Void SetLayoutVertical()
  RVA=0x031E8560  token=0x60002B2  System.Void SetDirty()
END_CLASS

CLASS: UnityEngine.UI.GridLayoutGroup
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x78
EXTENDS: UnityEngine.UI.LayoutGroup
FIELDS:
  protected         UnityEngine.UI.GridLayoutGroup.Cornerm_StartCorner  // 0x58
  protected         UnityEngine.UI.GridLayoutGroup.Axism_StartAxis  // 0x5c
  protected         UnityEngine.Vector2             m_CellSize  // 0x60
  protected         UnityEngine.Vector2             m_Spacing  // 0x68
  protected         UnityEngine.UI.GridLayoutGroup.Constraintm_Constraint  // 0x70
  protected         System.Int32                    m_ConstraintCount  // 0x74
PROPERTIES:
  startCorner  get=0x04D86570  set=0x0B568F04
  startAxis  get=0x04D86560  set=0x0B568EB8
  cellSize  get=0x04DBEF20  set=0x0B568D64
  spacing  get=0x04DBEF40  set=0x0B568E64
  constraint  get=0x04D868A0  set=0x0B568E18
  constraintCount  get=0x04D868B0  set=0x0B568DB8
METHODS:
  RVA=0x049D3A50  token=0x60002BF  System.Void .ctor()
  RVA=0x03B92180  token=0x60002C0  System.Void CalculateLayoutInputHorizontal()
  RVA=0x03B91B00  token=0x60002C1  System.Void CalculateLayoutInputVertical()
  RVA=0x0B568D58  token=0x60002C2  System.Void SetLayoutHorizontal()
  RVA=0x053965BC  token=0x60002C3  System.Void SetLayoutVertical()
  RVA=0x03B91C30  token=0x60002C4  System.Void SetCellsAlongAxis(System.Int32 axis)
END_CLASS

CLASS: UnityEngine.UI.HorizontalLayoutGroup
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x68
EXTENDS: UnityEngine.UI.HorizontalOrVerticalLayoutGroup
FIELDS:
METHODS:
  RVA=0x0416C540  token=0x60002C5  System.Void .ctor()
  RVA=0x03B91900  token=0x60002C6  System.Void CalculateLayoutInputHorizontal()
  RVA=0x053953A0  token=0x60002C7  System.Void CalculateLayoutInputVertical()
  RVA=0x053915EC  token=0x60002C8  System.Void SetLayoutHorizontal()
  RVA=0x0539658C  token=0x60002C9  System.Void SetLayoutVertical()
END_CLASS

CLASS: UnityEngine.UI.HorizontalOrVerticalLayoutGroup
TYPE:  abstract class
TOKEN: 0x200004D
SIZE:  0x68
EXTENDS: UnityEngine.UI.LayoutGroup
FIELDS:
  protected         System.Single                   m_Spacing  // 0x58
  protected         System.Boolean                  m_ChildForceExpandWidth  // 0x5c
  protected         System.Boolean                  m_ChildForceExpandHeight  // 0x5d
  protected         System.Boolean                  m_ChildControlWidth  // 0x5e
  protected         System.Boolean                  m_ChildControlHeight  // 0x5f
  protected         System.Boolean                  m_ChildScaleWidth  // 0x60
  protected         System.Boolean                  m_ChildScaleHeight  // 0x61
  protected         System.Boolean                  m_ReverseArrangement  // 0x62
PROPERTIES:
  spacing  get=0x04D88400  set=0x0B569130
  childForceExpandWidth  get=0x04D86D90  set=0x045531B0
  childForceExpandHeight  get=0x04DA5B40  set=0x0B568FF0
  childControlWidth  get=0x04DA5B60  set=0x0B568FA0
  childControlHeight  get=0x04D8E420  set=0x0B568F50
  childScaleWidth  get=0x04D88340  set=0x0B569090
  childScaleHeight  get=0x04D892D0  set=0x0B569040
  reverseArrangement  get=0x04D8FF20  set=0x0B5690E0
METHODS:
  RVA=0x033F8790  token=0x60002DA  System.Void CalcAlongAxis(System.Int32 axis, System.Boolean isVertical)
  RVA=0x033F7B20  token=0x60002DB  System.Void SetChildrenAlongAxis(System.Int32 axis, System.Boolean isVertical)
  RVA=0x033F8D20  token=0x60002DC  System.Void GetChildSizes(UnityEngine.RectTransform child, System.Int32 axis, System.Boolean controlSize, System.Boolean childForceExpand, System.Single& min, System.Single& preferred, System.Single& flexible)
  RVA=0x0416C540  token=0x60002DD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.ILayoutElement
TYPE:  interface
TOKEN: 0x200004E
FIELDS:
PROPERTIES:
  minWidth  get=-1  // abstract
  preferredWidth  get=-1  // abstract
  flexibleWidth  get=-1  // abstract
  minHeight  get=-1  // abstract
  preferredHeight  get=-1  // abstract
  flexibleHeight  get=-1  // abstract
  layoutPriority  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60002DE  System.Void CalculateLayoutInputHorizontal()
  RVA=-1  // abstract  token=0x60002DF  System.Void CalculateLayoutInputVertical()
END_CLASS

CLASS: UnityEngine.UI.ILayoutController
TYPE:  interface
TOKEN: 0x200004F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002E7  System.Void SetLayoutHorizontal()
  RVA=-1  // abstract  token=0x60002E8  System.Void SetLayoutVertical()
END_CLASS

CLASS: UnityEngine.UI.ILayoutGroup
TYPE:  interface
TOKEN: 0x2000050
IMPLEMENTS: UnityEngine.UI.ILayoutController
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.UI.ILayoutSelfController
TYPE:  interface
TOKEN: 0x2000051
IMPLEMENTS: UnityEngine.UI.ILayoutController
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.UI.ILayoutIgnorer
TYPE:  interface
TOKEN: 0x2000052
FIELDS:
PROPERTIES:
  ignoreLayout  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.UI.LayoutElement
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x38
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutElement UnityEngine.UI.ILayoutIgnorer
FIELDS:
  private           System.Boolean                  m_IgnoreLayout  // 0x18
  private           System.Single                   m_MinWidth  // 0x1c
  private           System.Single                   m_MinHeight  // 0x20
  private           System.Single                   m_PreferredWidth  // 0x24
  private           System.Single                   m_PreferredHeight  // 0x28
  private           System.Single                   m_FlexibleWidth  // 0x2c
  private           System.Single                   m_FlexibleHeight  // 0x30
  private           System.Int32                    m_LayoutPriority  // 0x34
PROPERTIES:
  ignoreLayout  get=0x04D86340  set=0x033F1220
  minWidth  get=0x04D868F0  set=0x0B571BD4
  minHeight  get=0x011EC8A0  set=0x0B571B78
  preferredWidth  get=0x04D89850  set=0x0344CDB0
  preferredHeight  get=0x04D86BF0  set=0x0344CB70
  flexibleWidth  get=0x04D86AF0  set=0x03CD5350
  flexibleHeight  get=0x04D86E20  set=0x0B571AC4
  layoutPriority  get=0x04D86730  set=0x0B571B20
METHODS:
  RVA=0x041E1670  token=0x60002EC  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x60002ED  System.Void CalculateLayoutInputVertical()
  RVA=0x03695EF0  token=0x60002FC  System.Void .ctor()
  RVA=0x0344CBD0  token=0x60002FD  System.Void OnEnable()
  RVA=0x0344CBD0  token=0x60002FE  System.Void OnTransformParentChanged()
  RVA=0x0344CBD0  token=0x60002FF  System.Void OnDisable()
  RVA=0x0344CBD0  token=0x6000300  System.Void OnDidApplyAnimationProperties()
  RVA=0x0344CBD0  token=0x6000301  System.Void OnBeforeTransformParentChanged()
  RVA=0x0344CBE0  token=0x6000302  System.Void SetDirty()
END_CLASS

CLASS: UnityEngine.UI.LayoutGroup
TYPE:  abstract class
TOKEN: 0x2000054
SIZE:  0x58
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutElement UnityEngine.UI.ILayoutGroup UnityEngine.UI.ILayoutController
FIELDS:
  protected         UnityEngine.RectOffset          m_Padding  // 0x18
  protected         UnityEngine.TextAnchor          m_ChildAlignment  // 0x20
  private           UnityEngine.RectTransform       m_Rect  // 0x28
  protected         UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x30
  private           UnityEngine.Vector2             m_TotalMinSize  // 0x34
  private           UnityEngine.Vector2             m_TotalPreferredSize  // 0x3c
  private           UnityEngine.Vector2             m_TotalFlexibleSize  // 0x44
  private           System.Collections.Generic.List<UnityEngine.RectTransform>m_RectChildren  // 0x50
PROPERTIES:
  padding  get=0x0385B100  set=0x049956B0
  childAlignment  get=0x02B70F90  set=0x0B571CDC
  rectTransform  get=0x031F1F20
  rectChildren  get=0x04D86270
  minWidth  get=0x053957B0
  preferredWidth  get=0x05391670
  flexibleWidth  get=0x05391650
  minHeight  get=0x05391630
  preferredHeight  get=0x05391640
  flexibleHeight  get=0x05391660
  layoutPriority  get=0x011EC580
  isRootLayoutGroup  get=0x03DC3580
METHODS:
  RVA=0x031F1AA0  token=0x6000309  System.Void CalculateLayoutInputHorizontal()
  RVA=-1  // abstract  token=0x600030A  System.Void CalculateLayoutInputVertical()
  RVA=-1  // abstract  token=0x6000312  System.Void SetLayoutHorizontal()
  RVA=-1  // abstract  token=0x6000313  System.Void SetLayoutVertical()
  RVA=0x03695590  token=0x6000314  System.Void .ctor()
  RVA=0x044067D0  token=0x6000315  System.Void OnEnable()
  RVA=0x042561A0  token=0x6000316  System.Void OnDisable()
  RVA=0x044067D0  token=0x6000317  System.Void OnDidApplyAnimationProperties()
  RVA=0x0B571C3C  token=0x6000318  System.Single GetTotalMinSize(System.Int32 axis)
  RVA=0x0539241C  token=0x6000319  System.Single GetTotalPreferredSize(System.Int32 axis)
  RVA=0x0B571C30  token=0x600031A  System.Single GetTotalFlexibleSize(System.Int32 axis)
  RVA=0x033F7010  token=0x600031B  System.Single GetStartOffset(System.Int32 axis, System.Single requiredSpaceWithoutPadding)
  RVA=0x033F7270  token=0x600031C  System.Single GetAlignmentOnAxis(System.Int32 axis)
  RVA=0x033F76D0  token=0x600031D  System.Void SetLayoutInputForAxis(System.Single totalMin, System.Single totalPreferred, System.Single totalFlexible, System.Int32 axis)
  RVA=0x0B571C48  token=0x600031E  System.Void SetChildAlongAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos)
  RVA=0x03C5D6D0  token=0x600031F  System.Void SetChildAlongAxisWithScale(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single scaleFactor)
  RVA=0x03B920B0  token=0x6000320  System.Void SetChildAlongAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single size)
  RVA=0x033F7400  token=0x6000321  System.Void SetChildAlongAxisWithScale(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single size, System.Single scaleFactor)
  RVA=0x03DC3550  token=0x6000323  System.Void OnRectTransformDimensionsChange()
  RVA=0x044067D0  token=0x6000324  System.Void OnTransformChildrenChanged()
  RVA=-1  // generic def  token=0x6000325  System.Void SetProperty(T& currentValue, T newValue)
  RVA=0x03519E40  token=0x6000326  System.Void SetDirty()
  RVA=0x043BB440  token=0x6000327  System.Collections.IEnumerator DelayedSetDirty(UnityEngine.RectTransform rectTransform)
END_CLASS

CLASS: UnityEngine.UI.LayoutRebuilder
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x20
IMPLEMENTS: UnityEngine.UI.ICanvasElement
FIELDS:
  private           UnityEngine.RectTransform       m_ToRebuild  // 0x10
  private           System.Int32                    m_CachedHashFromTransform  // 0x18
  private   static  UnityEngine.Pool.ObjectPool<UnityEngine.UI.LayoutRebuilder>s_Rebuilders  // static @ 0x0
PROPERTIES:
  transform  get=0x02B2ECC0
METHODS:
  RVA=0x0351B220  token=0x600032E  System.Void Initialize(UnityEngine.RectTransform controller)
  RVA=0x05396170  token=0x600032F  System.Void Clear()
  RVA=0x048F3650  token=0x6000330  System.Void .cctor()
  RVA=0x03EA5880  token=0x6000331  System.Void ReapplyDrivenProperties(UnityEngine.RectTransform driven)
  RVA=0x041916B0  token=0x6000333  System.Boolean IsDestroyed()
  RVA=0x031F0E10  token=0x6000334  System.Void StripDisabledBehavioursFromList(System.Collections.Generic.List<UnityEngine.Component> components)
  RVA=0x03543F10  token=0x6000335  System.Void ForceRebuildLayoutImmediate(UnityEngine.RectTransform layoutRoot)
  RVA=0x03543FC0  token=0x6000336  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x031F13B0  token=0x6000337  System.Void PerformLayoutControl(UnityEngine.RectTransform rect, UnityEngine.Events.UnityAction<UnityEngine.Component> action)
  RVA=0x031F0680  token=0x6000338  System.Void PerformLayoutCalculation(UnityEngine.RectTransform rect, UnityEngine.Events.UnityAction<UnityEngine.Component> action)
  RVA=0x02FCBF00  token=0x6000339  System.Void MarkLayoutForRebuild(UnityEngine.RectTransform rect)
  RVA=0x031F0F50  token=0x600033A  System.Boolean ValidController(UnityEngine.RectTransform layoutRoot, System.Collections.Generic.List<UnityEngine.Component> comps)
  RVA=0x0351AF00  token=0x600033B  System.Void MarkLayoutRootForRebuild(UnityEngine.RectTransform controller)
  RVA=0x040E03C0  token=0x600033C  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x600033D  System.Void GraphicUpdateComplete()
  RVA=0x011EF5B0  token=0x600033E  System.Int32 GetHashCode()
  RVA=0x03D30750  token=0x600033F  System.Boolean Equals(System.Object obj)
  RVA=0x0B571D28  token=0x6000340  System.String ToString()
  RVA=0x041E1670  token=0x6000341  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.LayoutUtility
TYPE:  static class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x033F9120  token=0x600034B  System.Single GetMinSize(UnityEngine.RectTransform rect, System.Int32 axis)
  RVA=0x033F8FE0  token=0x600034C  System.Single GetPreferredSize(UnityEngine.RectTransform rect, System.Int32 axis)
  RVA=0x033F9010  token=0x600034D  System.Single GetFlexibleSize(UnityEngine.RectTransform rect, System.Int32 axis)
  RVA=0x033F7A30  token=0x600034E  System.Single GetMinWidth(UnityEngine.RectTransform rect)
  RVA=0x033F9410  token=0x600034F  System.Single GetPreferredWidth(UnityEngine.RectTransform rect)
  RVA=0x033F7940  token=0x6000350  System.Single GetFlexibleWidth(UnityEngine.RectTransform rect)
  RVA=0x03DB8680  token=0x6000351  System.Single GetMinHeight(UnityEngine.RectTransform rect)
  RVA=0x033F9230  token=0x6000352  System.Single GetPreferredHeight(UnityEngine.RectTransform rect)
  RVA=0x03DB8770  token=0x6000353  System.Single GetFlexibleHeight(UnityEngine.RectTransform rect)
  RVA=0x05392498  token=0x6000354  System.Single GetLayoutProperty(UnityEngine.RectTransform rect, System.Func<UnityEngine.UI.ILayoutElement,System.Single> property, System.Single defaultValue)
  RVA=0x031EF3A0  token=0x6000355  System.Single GetLayoutProperty(UnityEngine.RectTransform rect, System.Func<UnityEngine.UI.ILayoutElement,System.Single> property, System.Single defaultValue, UnityEngine.UI.ILayoutElement& source)
END_CLASS

CLASS: UnityEngine.UI.VerticalLayoutGroup
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x68
EXTENDS: UnityEngine.UI.HorizontalOrVerticalLayoutGroup
FIELDS:
METHODS:
  RVA=0x0416C540  token=0x6000360  System.Void .ctor()
  RVA=0x03B91930  token=0x6000361  System.Void CalculateLayoutInputHorizontal()
  RVA=0x053953B0  token=0x6000362  System.Void CalculateLayoutInputVertical()
  RVA=0x053915FC  token=0x6000363  System.Void SetLayoutHorizontal()
  RVA=0x0539659C  token=0x6000364  System.Void SetLayoutVertical()
END_CLASS

CLASS: UnityEngine.UI.Mask
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x40
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.ICanvasRaycastFilter UnityEngine.UI.IMaterialModifier
FIELDS:
  private           UnityEngine.RectTransform       m_RectTransform  // 0x18
  private           System.Boolean                  m_ShowMaskGraphic  // 0x20
  private           UnityEngine.UI.Graphic          m_Graphic  // 0x28
  private           UnityEngine.Material            m_MaskMaterial  // 0x30
  private           UnityEngine.Material            m_UnmaskMaterial  // 0x38
PROPERTIES:
  rectTransform  get=0x03091280
  showMaskGraphic  get=0x04D866B0  set=0x0B571D88
  graphic  get=0x041D2210
METHODS:
  RVA=0x0489D040  token=0x6000369  System.Void .ctor()
  RVA=0x041D1CA0  token=0x600036A  System.Boolean MaskEnabled()
  RVA=0x041E1670  token=0x600036B  System.Void OnSiblingGraphicEnabledDisabled()
  RVA=0x041D20C0  token=0x600036C  System.Void OnEnable()
  RVA=0x041D1D30  token=0x600036D  System.Void OnDisable()
  RVA=0x030911F0  token=0x600036E  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x041D1F10  token=0x600036F  UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
END_CLASS

CLASS: UnityEngine.UI.MaskableGraphic
TYPE:  abstract class
TOKEN: 0x200005C
SIZE:  0xE8
EXTENDS: UnityEngine.UI.Graphic
IMPLEMENTS: UnityEngine.UI.IClippable UnityEngine.UI.IMaskable UnityEngine.UI.IMaterialModifier
FIELDS:
  protected         System.Boolean                  m_ShouldRecalculateStencil  // 0xb0
  protected         UnityEngine.Material            m_MaskMaterial  // 0xb8
  private           UnityEngine.UI.RectMask2D       m_ParentMask  // 0xc0
  private           System.Boolean                  m_Maskable  // 0xc8
  private           System.Boolean                  m_IsMaskingGraphic  // 0xc9
  protected         System.Boolean                  m_IncludeForMasking  // 0xca
  private           UnityEngine.UI.MaskableGraphic.CullStateChangedEventm_OnCullStateChanged  // 0xd0
  protected         System.Boolean                  m_ShouldRecalculate  // 0xd8
  protected         System.Int32                    m_StencilValue  // 0xdc
  private   readonly UnityEngine.Vector3[]           m_Corners  // 0xe0
PROPERTIES:
  onCullStateChanged  get=0x04D876B0  set=0x0678D7BC
  maskable  get=0x04D8E270  set=0x0B5721A8
  isMaskingGraphic  get=0x04DA2350  set=0x041D2380
  hasCullOverride  get=0x02FFF600
  rootCanvasRect  get=0x0B571E74
METHODS:
  RVA=0x031E9E20  token=0x6000377  System.Void UpdateCullDirect(System.Boolean cull)
  RVA=0x031EE3E0  token=0x6000378  UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
  RVA=0x0B571E14  token=0x6000379  System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect)
  RVA=0x031EB1D0  token=0x600037A  System.Void UpdateCull(System.Boolean cull)
  RVA=0x031EB0E0  token=0x600037B  System.Void SetClipRect(UnityEngine.Rect clipRect, System.Boolean validRect)
  RVA=0x031BFD70  token=0x600037C  System.Void SetClipSoftness(UnityEngine.Vector2 clipSoftness)
  RVA=0x031BFC20  token=0x600037D  System.Void SetHGClipSoftness(UnityEngine.Vector4 hgClipSoftness)
  RVA=0x031EBD20  token=0x600037E  System.Void OnEnable()
  RVA=0x0351A3E0  token=0x600037F  System.Void OnDisable()
  RVA=0x03F3AEA0  token=0x6000380  System.Void OnTransformParentChanged()
  RVA=0x041E1670  token=0x6000381  System.Void ParentMaskStateChanged()
  RVA=0x031C3D80  token=0x6000382  System.Void OnCanvasHierarchyChanged()
  RVA=0x031EC620  token=0x6000384  System.Void UpdateClipParent()
  RVA=0x03F59BE0  token=0x6000385  System.Void RecalculateClipping()
  RVA=0x0351C410  token=0x6000386  System.Void RecalculateMasking()
  RVA=0x03696ED0  token=0x6000387  System.Void .ctor()
  RVA=0x053916BC  token=0x6000388  UnityEngine.GameObject UnityEngine.UI.IClippable.get_gameObject()
END_CLASS

CLASS: UnityEngine.UI.MaskUtilities
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x031EF090  token=0x600038A  System.Void Notify2DMaskStateChanged(UnityEngine.Component mask)
  RVA=0x041D23E0  token=0x600038B  System.Void NotifyStencilStateChanged(UnityEngine.Component mask)
  RVA=0x031ED4E0  token=0x600038C  UnityEngine.Transform FindRootSortOverrideCanvas(UnityEngine.Transform start)
  RVA=0x031ED130  token=0x600038D  System.Int32 GetStencilDepth(UnityEngine.Transform transform, UnityEngine.Transform stopAfter)
  RVA=0x031EB530  token=0x600038E  System.Boolean IsDescendantOrSelf(UnityEngine.Transform father, UnityEngine.Transform child)
  RVA=0x031EC810  token=0x600038F  UnityEngine.UI.RectMask2D GetRectMaskForClippable(UnityEngine.UI.IClippable clippable)
  RVA=0x03D91980  token=0x6000390  System.Void GetRectMasksForClip(UnityEngine.UI.RectMask2D clipper, System.Collections.Generic.List<UnityEngine.UI.RectMask2D> masks)
  RVA=0x041E1670  token=0x6000391  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.IMaterialModifier
TYPE:  interface
TOKEN: 0x200005F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000392  UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
END_CLASS

CLASS: UnityEngine.UI.Misc
TYPE:  static class
TOKEN: 0x2000060
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B572CC8  token=0x6000393  System.Void Destroy(UnityEngine.Object obj)
  RVA=0x0485FD10  token=0x6000394  System.Void DestroyImmediate(UnityEngine.Object obj)
END_CLASS

CLASS: UnityEngine.UI.MultipleDisplayUtilities
TYPE:  static class
TOKEN: 0x2000061
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B572D94  token=0x6000395  System.Boolean GetRelativeMousePositionForDrag(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Vector2& position)
  RVA=0x0324F2F0  token=0x6000396  UnityEngine.Vector3 RelativeMouseAtScaled(UnityEngine.Vector2 position)
END_CLASS

CLASS: UnityEngine.UI.Navigation
TYPE:  sealed struct
TOKEN: 0x2000062
SIZE:  0x38
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           UnityEngine.UI.Navigation.Mode  m_Mode  // 0x10
  private           System.Boolean                  m_WrapAround  // 0x14
  private           UnityEngine.UI.Selectable       m_SelectOnUp  // 0x18
  private           UnityEngine.UI.Selectable       m_SelectOnDown  // 0x20
  private           UnityEngine.UI.Selectable       m_SelectOnLeft  // 0x28
  private           UnityEngine.UI.Selectable       m_SelectOnRight  // 0x30
PROPERTIES:
  mode  get=0x02B77620  set=0x04D86140
  wrapAround  get=0x04D90BB0  set=0x04D90BC0
  selectOnUp  get=0x04D88320  set=0x067D5E9C
  selectOnDown  get=0x02B2ECC0  set=0x053908C0
  selectOnLeft  get=0x0385B100  set=0x05392C40
  selectOnRight  get=0x04D862C0  set=0x05390F40
  defaultNavigation  get=0x04DBEFF0
METHODS:
  RVA=0x03E58120  token=0x60003A4  System.Boolean Equals(UnityEngine.UI.Navigation other)
END_CLASS

CLASS: UnityEngine.UI.RawImage
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x100
EXTENDS: UnityEngine.UI.MaskableGraphic
FIELDS:
  private           UnityEngine.Texture             m_Texture  // 0xe8
  private           UnityEngine.Rect                m_UVRect  // 0xf0
PROPERTIES:
  hasCullOverride  get=0x012081B0
  mainTexture  get=0x0429BCE0
  texture  get=0x04D88590  set=0x037DC210
  uvRect  get=0x04DA1270  set=0x0B572FA4
METHODS:
  RVA=0x039C5080  token=0x60003A6  System.Void .ctor()
  RVA=0x0B572E6C  token=0x60003AC  System.Void SetNativeSize()
  RVA=0x04032A90  token=0x60003AD  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x03E51A10  token=0x60003AE  System.Void OnDidApplyAnimationProperties()
END_CLASS

CLASS: UnityEngine.UI.RectMask2D
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x98
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.IClipper UnityEngine.ICanvasRaycastFilter
FIELDS:
  private   readonly UnityEngine.UI.RectangularVertexClipperm_VertexClipper  // 0x18
  private           UnityEngine.RectTransform       m_RectTransform  // 0x20
  private           System.Collections.Generic.HashSet<UnityEngine.UI.MaskableGraphic>m_MaskableTargets  // 0x28
  private           System.Collections.Generic.HashSet<UnityEngine.UI.IClippable>m_ClipTargets  // 0x30
  private           System.Boolean                  m_ShouldRecalculateClipRects  // 0x38
  private           System.Collections.Generic.List<UnityEngine.UI.RectMask2D>m_Clippers  // 0x40
  private           UnityEngine.Rect                m_LastClipRectCanvasSpace  // 0x48
  private           System.Boolean                  m_ForceClip  // 0x58
  private           UnityEngine.Vector4             m_Padding  // 0x5c
  private           UnityEngine.Vector2Int          m_Softness  // 0x6c
  private           UnityEngine.Vector4             m_HGSoftness  // 0x74
  private           UnityEngine.Canvas              m_Canvas  // 0x88
  private           UnityEngine.Vector3[]           m_Corners  // 0x90
PROPERTIES:
  padding  get=0x04DA8720  set=0x0B573E70
  softness  get=0x04DBF020  set=0x0B573E80
  hgSoftness  get=0x04DBF010  set=0x0B573E30
  Canvas  get=0x031C28A0
  canvasRect  get=0x0B573B54
  rectTransform  get=0x031BF930
  rootCanvasRect  get=0x0B573BCC
  shouldRecalculateClipRects  get=0x011F2A70  set=0x011F2A80
  clippers  get=0x04D85A60
  maskableTargets  get=0x04D86240
  clipTargets  get=0x011F36E0
  lastClipRectCanvasSpace  get=0x04D8FD50  set=0x04D91540
  forceClip  get=0x04D86F70  set=0x04D86F80
  canvasRef  get=0x053914B4
METHODS:
  RVA=0x0404D490  token=0x60003B8  System.Void .ctor()
  RVA=0x04424720  token=0x60003B9  System.Void OnEnable()
  RVA=0x03D918E0  token=0x60003BA  System.Void OnDisable()
  RVA=0x030933E0  token=0x60003BB  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x0B573458  token=0x60003BD  System.Void PerformClipping()
  RVA=0x031C1D80  token=0x60003BE  System.Void UpdateClipSoftness()
  RVA=0x031EB2C0  token=0x60003BF  System.Void AddClippable(UnityEngine.UI.IClippable clippable)
  RVA=0x031EAE20  token=0x60003C0  System.Void RemoveClippable(UnityEngine.UI.IClippable clippable)
  RVA=0x04D919E0  token=0x60003C1  System.Void OnTransformParentChanged()
  RVA=0x042ADE00  token=0x60003C2  System.Void OnCanvasHierarchyChanged()
  RVA=0x03D91950  token=0x60003CD  System.Void RecalculateClipperChain()
END_CLASS

CLASS: UnityEngine.UI.Scrollbar
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x1D0
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IInitializePotentialDragHandler UnityEngine.UI.ICanvasElement
FIELDS:
  private           UnityEngine.RectTransform       m_HandleRect  // 0x188
  private           UnityEngine.UI.Scrollbar.Directionm_Direction  // 0x190
  private           System.Single                   m_Value  // 0x194
  private           System.Single                   m_Size  // 0x198
  private           System.Int32                    m_NumberOfSteps  // 0x19c
  private           UnityEngine.UI.Scrollbar.ScrollEventm_OnValueChanged  // 0x1a0
  private           UnityEngine.RectTransform       m_ContainerRect  // 0x1a8
  private           UnityEngine.Vector2             m_Offset  // 0x1b0
  private           UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x1b8
  private           UnityEngine.Coroutine           m_PointerDownRepeat  // 0x1c0
  private           System.Boolean                  isPointerDownAndNotDragging  // 0x1c8
  private           System.Boolean                  m_DelayedUpdateVisuals  // 0x1c9
PROPERTIES:
  handleRect  get=0x04D8DA20  set=0x0B576148
  direction  get=0x04DBF060  set=0x0B5760EC
  value  get=0x04033F40  set=0x05396DA0
  size  get=0x04D8EDC0  set=0x04033C10
  numberOfSteps  get=0x04D8F920  set=0x0B5761B0
  onValueChanged  get=0x04D8DA10  set=0x05397204
  stepSize  get=0x0B5760BC
  axis  get=0x04033F20
  reverseValue  get=0x04033F70
METHODS:
  RVA=0x0453A550  token=0x60003D2  System.Void .ctor()
  RVA=0x0B575E2C  token=0x60003D5  System.Void SetValueWithoutNotify(System.Single input)
  RVA=0x041E1670  token=0x60003DD  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x041E1670  token=0x60003DE  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x60003DF  System.Void GraphicUpdateComplete()
  RVA=0x04033C80  token=0x60003E0  System.Void OnEnable()
  RVA=0x0447EC90  token=0x60003E1  System.Void OnDisable()
  RVA=0x04D265A0  token=0x60003E2  System.Void Update()
  RVA=0x04033F90  token=0x60003E3  System.Void UpdateCachedReferences()
  RVA=0x04033CE0  token=0x60003E4  System.Void Set(System.Single input, System.Boolean sendCallback)
  RVA=0x04033C50  token=0x60003E5  System.Void OnRectTransformDimensionsChange()
  RVA=0x04033D80  token=0x60003E8  System.Void UpdateVisuals()
  RVA=0x0B575E38  token=0x60003E9  System.Void UpdateDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B5755D4  token=0x60003EA  System.Void DoUpdateDrag(UnityEngine.Vector2 handleCorner, System.Single remainingSize)
  RVA=0x0B575710  token=0x60003EB  System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B575764  token=0x60003EC  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B575974  token=0x60003ED  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B575C08  token=0x60003EE  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57555C  token=0x60003EF  System.Collections.IEnumerator ClickRepeat(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B5754CC  token=0x60003F0  System.Collections.IEnumerator ClickRepeat(UnityEngine.Vector2 screenPosition, UnityEngine.Camera camera)
  RVA=0x0B575CD0  token=0x60003F1  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B575A18  token=0x60003F2  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0B575674  token=0x60003F3  UnityEngine.UI.Selectable FindSelectableOnLeft()
  RVA=0x0B5756A8  token=0x60003F4  UnityEngine.UI.Selectable FindSelectableOnRight()
  RVA=0x0B5756DC  token=0x60003F5  UnityEngine.UI.Selectable FindSelectableOnUp()
  RVA=0x0B575640  token=0x60003F6  UnityEngine.UI.Selectable FindSelectableOnDown()
  RVA=0x0B5759FC  token=0x60003F7  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B575CF0  token=0x60003F8  System.Void SetDirection(UnityEngine.UI.Scrollbar.Direction direction, System.Boolean includeRectLayouts)
  RVA=0x053953F4  token=0x60003F9  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.ScrollRect
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x150
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IInitializePotentialDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.UI.ICanvasElement UnityEngine.UI.ILayoutElement UnityEngine.UI.ILayoutGroup UnityEngine.UI.ILayoutController
FIELDS:
  private           UnityEngine.RectTransform       m_Content  // 0x18
  private           System.Boolean                  m_Horizontal  // 0x20
  private           System.Boolean                  m_Vertical  // 0x21
  protected         UnityEngine.GameObject          m_scrollEnableNode  // 0x28
  private           UnityEngine.UI.ScrollRect.MovementTypem_MovementType  // 0x30
  private           System.Single                   m_Elasticity  // 0x34
  private           System.Boolean                  m_Inertia  // 0x38
  private           System.Single                   m_DecelerationRate  // 0x3c
  private           System.Single                   m_ScrollSensitivity  // 0x40
  private           UnityEngine.RectTransform       m_Viewport  // 0x48
  private           UnityEngine.UI.Scrollbar        m_HorizontalScrollbar  // 0x50
  private           UnityEngine.UI.Scrollbar        m_VerticalScrollbar  // 0x58
  private           UnityEngine.UI.ScrollRect.ScrollbarVisibilitym_HorizontalScrollbarVisibility  // 0x60
  private           UnityEngine.UI.ScrollRect.ScrollbarVisibilitym_VerticalScrollbarVisibility  // 0x64
  private           System.Single                   m_HorizontalScrollbarSpacing  // 0x68
  private           System.Single                   m_VerticalScrollbarSpacing  // 0x6c
  private           UnityEngine.UI.ScrollRect.ScrollRectEventm_OnValueChanged  // 0x70
  public            System.Boolean                  disableScroll  // 0x78
  public            UnityEngine.UI.ScrollRect.ScrollRectEventOnScrollStart  // 0x80
  public            UnityEngine.UI.ScrollRect.ScrollRectEventOnScrollEnd  // 0x88
  private           UnityEngine.Vector2             m_PointerStartLocalCursor  // 0x90
  protected         UnityEngine.Vector2             m_ContentStartPosition  // 0x98
  private           UnityEngine.RectTransform       m_ViewRect  // 0xa0
  protected         UnityEngine.Bounds              m_ContentBounds  // 0xa8
  private           UnityEngine.Bounds              m_ViewBounds  // 0xc0
  private           UnityEngine.Vector2             m_Velocity  // 0xd8
  protected         System.Boolean                  m_Dragging  // 0xe0
  protected         System.Boolean                  m_onScrollEndCbFlag  // 0xe1
  protected         System.Boolean                  m_Scrolling  // 0xe2
  private           UnityEngine.Vector2             m_PrevPosition  // 0xe4
  private           UnityEngine.Bounds              m_PrevContentBounds  // 0xec
  private           UnityEngine.Bounds              m_PrevViewBounds  // 0x104
  private           System.Boolean                  m_HasRebuiltLayout  // 0x11c
  private           System.Boolean                  m_HSliderExpand  // 0x11d
  private           System.Boolean                  m_VSliderExpand  // 0x11e
  private           System.Single                   m_HSliderHeight  // 0x120
  private           System.Single                   m_VSliderWidth  // 0x124
  private           UnityEngine.RectTransform       m_Rect  // 0x128
  private           UnityEngine.RectTransform       m_HorizontalScrollbarRect  // 0x130
  private           UnityEngine.RectTransform       m_VerticalScrollbarRect  // 0x138
  private           UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x140
  private   readonly UnityEngine.Vector3[]           m_Corners  // 0x148
PROPERTIES:
  content  get=0x0385B100  set=0x05392C40
  horizontal  get=0x04D866B0  set=0x04D866D0
  vertical  get=0x04D86850  set=0x04D86860
  movementType  get=0x04D864F0  set=0x04D86740
  elasticity  get=0x04D8E050  set=0x04D8E060
  inertia  get=0x011F2A70  set=0x011F2A80
  decelerationRate  get=0x04D863E0  set=0x04D86460
  scrollSensitivity  get=0x04D863F0  set=0x04D86470
  viewport  get=0x04D86200  set=0x0B5754A8
  horizontalScrollbar  get=0x04D86270  set=0x0B575144
  verticalScrollbar  get=0x02E56440  set=0x0B575320
  horizontalScrollbarVisibility  get=0x04D86790  set=0x0B575138
  verticalScrollbarVisibility  get=0x04D88110  set=0x0B575314
  horizontalScrollbarSpacing  get=0x04D8F060  set=0x0B57512C
  verticalScrollbarSpacing  get=0x04DA3230  set=0x0B575308
  onValueChanged  get=0x04D862B0  set=0x063AD760
  viewRect  get=0x033F6EF0
  velocity  get=0x04DBF030  set=0x04DBF050
  rectTransform  get=0x039D3BC0
  normalizedPosition  get=0x0396F070  set=0x0B5752CC
  horizontalNormalizedPosition  get=0x0396F0B0  set=0x05397FA8
  verticalNormalizedPosition  get=0x0396F250  set=0x053945E8
  hScrollingNeeded  get=0x03E5E6B0
  vScrollingNeeded  get=0x03E5E600
  minWidth  get=0x04DA9E10
  preferredWidth  get=0x04DA9E10
  flexibleWidth  get=0x04DA9E10
  minHeight  get=0x04DA9E10
  preferredHeight  get=0x04DA9E10
  flexibleHeight  get=0x04DA9E10
  layoutPriority  get=0x04DA2BB0
METHODS:
  RVA=0x03EB2B60  token=0x6000425  System.Void .ctor()
  RVA=0x04033360  token=0x6000426  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x041E1670  token=0x6000427  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x6000428  System.Void GraphicUpdateComplete()
  RVA=0x040337D0  token=0x6000429  System.Void UpdateCachedData()
  RVA=0x039D39F0  token=0x600042A  System.Void OnEnable()
  RVA=0x039D37F0  token=0x600042B  System.Void OnDisable()
  RVA=0x045E1060  token=0x600042C  System.Boolean IsActive()
  RVA=0x033F6720  token=0x600042D  System.Void EnsureLayoutHasRebuilt()
  RVA=0x0B575100  token=0x600042E  System.Void StopMovement()
  RVA=0x0B574E34  token=0x600042F  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData data)
  RVA=0x0B57510C  token=0x6000430  System.Void _CustomSetPosOnScroll(UnityEngine.Vector2 position)
  RVA=0x04A26320  token=0x6000431  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B5749F4  token=0x6000432  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B574E10  token=0x6000433  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B574B20  token=0x6000434  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x039722E0  token=0x6000435  System.Void SetContentAnchoredPosition(UnityEngine.Vector2 position)
  RVA=0x033F4C20  token=0x6000436  System.Void LateUpdate()
  RVA=0x04033630  token=0x6000437  System.Void UpdatePrevData()
  RVA=0x04033480  token=0x6000438  System.Void UpdateScrollbars(UnityEngine.Vector2 offset)
  RVA=0x05397FA8  token=0x600043F  System.Void SetHorizontalNormalizedPosition(System.Single value)
  RVA=0x04573010  token=0x6000440  System.Void SetVerticalNormalizedPosition(System.Single value)
  RVA=0x04573020  token=0x6000441  System.Void SetNormalizedPosition(System.Single value, System.Int32 axis)
  RVA=0x0B575018  token=0x6000442  System.Single RubberDelta(System.Single overStretching, System.Single viewSize)
  RVA=0x039D3B60  token=0x6000443  System.Void OnRectTransformDimensionsChange()
  RVA=0x041E1670  token=0x6000446  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x6000447  System.Void CalculateLayoutInputVertical()
  RVA=0x04033740  token=0x600044F  System.Void SetLayoutHorizontal()
  RVA=0x03EF8850  token=0x6000450  System.Void SetLayoutVertical()
  RVA=0x03270630  token=0x6000451  System.Void UpdateScrollbarVisibility()
  RVA=0x03270900  token=0x6000452  System.Void UpdateOneScrollbarVisibility(System.Boolean xScrollingNeeded, System.Boolean xAxisEnabled, UnityEngine.UI.ScrollRect.ScrollbarVisibility scrollbarVisibility, UnityEngine.UI.Scrollbar scrollbar)
  RVA=0x03EF89B0  token=0x6000453  System.Void UpdateScrollbarLayout()
  RVA=0x03E0B730  token=0x6000454  System.Void UpdateScrollEnableNode()
  RVA=0x033F6990  token=0x6000455  System.Void UpdateBounds()
  RVA=0x033F6850  token=0x6000456  System.Void AdjustBounds(UnityEngine.Bounds& viewBounds, UnityEngine.Vector2& contentPivot, UnityEngine.Vector3& contentSize, UnityEngine.Vector3& contentPos)
  RVA=0x02F3C280  token=0x6000457  UnityEngine.Bounds GetBounds()
  RVA=0x02F3C5E0  token=0x6000458  UnityEngine.Bounds InternalGetBounds(UnityEngine.Vector3[] corners, UnityEngine.Matrix4x4& viewWorldToLocalMatrix)
  RVA=0x033F6440  token=0x6000459  UnityEngine.Vector2 CalculateOffset(UnityEngine.Vector2 delta)
  RVA=0x033F6490  token=0x600045A  UnityEngine.Vector2 InternalCalculateOffset(UnityEngine.Bounds& viewBounds, UnityEngine.Bounds& contentBounds, System.Boolean horizontal, System.Boolean vertical, UnityEngine.UI.ScrollRect.MovementType movementType, UnityEngine.Vector2& delta)
  RVA=0x039D3B70  token=0x600045B  System.Void SetDirty()
  RVA=0x0B575060  token=0x600045C  System.Void SetDirtyCaching()
  RVA=0x053953F4  token=0x600045D  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.Selectable
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x188
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IMoveHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler UnityEngine.EventSystems.ISelectHandler UnityEngine.EventSystems.IDeselectHandler
FIELDS:
  protected static  UnityEngine.UI.Selectable[]     s_Selectables  // static @ 0x0
  protected static  System.Int32                    s_SelectableCount  // static @ 0x8
  private           System.Boolean                  m_EnableCalled  // 0x18
  private           UnityEngine.UI.Navigation       m_Navigation  // 0x20
  private           UnityEngine.UI.Selectable.Transitionm_Transition  // 0x48
  private           UnityEngine.UI.ColorBlock       m_Colors  // 0x4c
  private           UnityEngine.UI.SpriteState      m_SpriteState  // 0xa8
  private           UnityEngine.UI.AnimationTriggersm_AnimationTriggers  // 0xc8
  private           System.Boolean                  m_Interactable  // 0xd0
  private           UnityEngine.UI.Graphic          m_TargetGraphic  // 0xd8
  protected         System.Boolean                  m_NotifyHover  // 0xe0
  protected         UnityEngine.GameObject          m_HoverEnableNode  // 0xe8
  protected         System.String                   m_HoverAudioKey  // 0xf0
  protected         System.Boolean                  _enableUnityNavi  // 0xf8
  private           System.Boolean                  m_GroupsAllowInteraction  // 0xf9
  protected         System.Int32                    m_CurrentIndex  // 0xfc
  private           System.Boolean                  <isPointerInside>k__BackingField  // 0x100
  private           System.Boolean                  <isPointerDown>k__BackingField  // 0x101
  private           System.Boolean                  <hasSelection>k__BackingField  // 0x102
  private   readonly System.Collections.Generic.List<UnityEngine.CanvasGroup>m_CanvasGroupCache  // 0x108
  public    static  UnityEngine.Events.UnityEvent<UnityEngine.UI.Selectable,System.Boolean>onAnyHoverChange  // static @ 0x10
  private   readonly UnityEngine.Events.UnityEvent<System.Boolean><onHoverChange>k__BackingField  // 0x110
  public            System.Boolean                  enableControllerNavi  // 0x118
  public            UnityEngine.Transform           overrideNaviHintRectTransform  // 0x120
  public            System.Single                   overrideNaviHintRectScale  // 0x128
  public            System.Boolean                  changeNaviHintParent  // 0x12c
  public            System.Boolean                  hideNaviHint  // 0x12d
  public            System.Boolean                  needNaviConfirmKeyHint  // 0x12e
  public            System.Boolean                  useExplicitNaviSelect  // 0x12f
  public            System.Boolean                  banExplicitOnLeft  // 0x130
  public            System.Boolean                  banExplicitOnRight  // 0x131
  public            System.Boolean                  banExplicitOnUp  // 0x132
  public            System.Boolean                  banExplicitOnDown  // 0x133
  public            System.Func<UnityEngine.UI.NaviDirection,UnityEngine.UI.Selectable>customNaviTargetInDirFunc  // 0x138
  private           UnityEngine.UI.Selectable       m_ExplicitSelectOnLeft  // 0x140
  private           UnityEngine.UI.Selectable       m_ExplicitSelectOnRight  // 0x148
  private           UnityEngine.UI.Selectable       m_ExplicitSelectOnUp  // 0x150
  private           UnityEngine.UI.Selectable       m_ExplicitSelectOnDown  // 0x158
  private           System.Boolean                  <isNaviTarget>k__BackingField  // 0x160
  public            System.Action<System.Boolean,System.Boolean,System.Boolean>onIsNaviTargetChanged  // 0x168
  private           System.Boolean                  <isNavigationSilent>k__BackingField  // 0x170
  public            UnityEngine.UI.SelectableNaviGroupnaviGroup  // 0x178
  private           System.Boolean                  m_naviInited  // 0x180
PROPERTIES:
  allSelectablesArray  get=0x0B577454
  allSelectableCount  get=0x0B577404
  allSelectables  get=0x0B5774D8
  hoverAudioKey  get=0x04D8D1A0
  navigation  get=0x04D8E8C0  set=0x037D4B20
  transition  get=0x04D863D0  set=0x0B57776C
  colors  get=0x04DBF070  set=0x0B577604
  spriteState  get=0x04DBF0B0  set=0x0B5776A0
  animationTriggers  get=0x04D8D1D0  set=0x0B5775A8
  targetGraphic  get=0x04D88EF0  set=0x0B577710
  interactable  get=0x04D90CF0  set=0x03409A80
  isPointerInside  get=0x04DA5D60  set=0x04DBF0E0
  isPointerDown  get=0x04DAA4D0  set=0x04DBF0D0
  hasSelection  get=0x04D8C8D0  set=0x04D8C900
  image  get=0x0B577568  set=0x06902698
  animator  get=0x05393874
  currentSelectionState  get=0x03795D60
  onHoverChange  get=0x04D8D1B0
  isNaviTarget  get=0x04D8E6A0  set=0x04D90350
  isNavigationSilent  get=0x04D905A0  set=0x04D905B0
METHODS:
  RVA=0x0B576240  token=0x6000462  System.Int32 AllSelectablesNoAlloc(UnityEngine.UI.Selectable[] selectables)
  RVA=0x03697410  token=0x6000478  System.Void .ctor()
  RVA=0x037D4960  token=0x600047C  System.Void Awake()
  RVA=0x02FC9670  token=0x600047D  System.Void OnCanvasGroupChanged()
  RVA=0x03795DB0  token=0x600047E  System.Boolean IsInteractable()
  RVA=0x0B576E4C  token=0x600047F  System.Void OnDidApplyAnimationProperties()
  RVA=0x03794EC0  token=0x6000480  System.Void OnEnable()
  RVA=0x04892DC0  token=0x6000481  System.Void OnTransformParentChanged()
  RVA=0x037D5010  token=0x6000482  System.Void OnSetProperty()
  RVA=0x03795DD0  token=0x6000483  System.Void OnDisable()
  RVA=0x037974D0  token=0x6000484  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x037962A0  token=0x6000486  System.Void InstantClearState()
  RVA=0x03796050  token=0x6000487  System.Void DoStateTransition(UnityEngine.UI.Selectable.SelectionState state, System.Boolean instant)
  RVA=0x0B576638  token=0x6000488  UnityEngine.UI.Selectable FindSelectable(UnityEngine.Vector3 dir)
  RVA=0x0B576B98  token=0x6000489  UnityEngine.Vector3 GetPointOnRectEdge(UnityEngine.RectTransform rect, UnityEngine.Vector2 dir)
  RVA=0x0B576DBC  token=0x600048A  System.Void Navigate(UnityEngine.EventSystems.AxisEventData eventData, UnityEngine.UI.Selectable sel)
  RVA=0x0B576428  token=0x600048B  UnityEngine.UI.Selectable FindSelectableOnLeft()
  RVA=0x0B5764D8  token=0x600048C  UnityEngine.UI.Selectable FindSelectableOnRight()
  RVA=0x0B576588  token=0x600048D  UnityEngine.UI.Selectable FindSelectableOnUp()
  RVA=0x0B576378  token=0x600048E  UnityEngine.UI.Selectable FindSelectableOnDown()
  RVA=0x0B576E54  token=0x600048F  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x03796550  token=0x6000490  System.Void StartColorTween(UnityEngine.Color targetColor, System.Boolean instant)
  RVA=0x0B5762E8  token=0x6000491  System.Void DoSpriteSwap(UnityEngine.Sprite newSprite)
  RVA=0x037966B0  token=0x6000492  System.Void TriggerAnimation(System.String triggername)
  RVA=0x0B576D6C  token=0x6000493  System.Boolean IsHighlighted()
  RVA=0x03797500  token=0x6000494  System.Boolean IsPressed()
  RVA=0x0379B190  token=0x6000495  System.Void EvaluateAndTransitionToSelectionState()
  RVA=0x0379AC80  token=0x6000496  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0379AC50  token=0x6000497  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0379B050  token=0x6000498  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0379B160  token=0x6000499  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04CA2B20  token=0x600049A  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x04B6C980  token=0x600049B  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B576F30  token=0x600049C  System.Void Select()
  RVA=0x03793A20  token=0x600049D  System.Void _RefreshHoverEnableNode(System.Boolean active)
  RVA=0x041E1670  token=0x600049E  System.Void OnInteractableChanged()
  RVA=0x031EAD80  token=0x60004A0  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0351DB80  token=0x60004A1  System.Void OnDestroy()
  RVA=0x0B576B70  token=0x60004A2  System.Boolean GetMouseActionHints(System.String& clickHintTextId, System.String& longPressHintTextId)
  RVA=0x0B576FFC  token=0x60004A7  System.Void SetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode, System.Boolean isGroupChanged)
  RVA=0x0B576EC4  token=0x60004A8  System.Void OnNaviTargetEnabledAgain()
  RVA=0x041E1670  token=0x60004A9  System.Void _OnSetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode)
  RVA=0x037D4B70  token=0x60004AA  System.Void TryFindNaviGroup()
  RVA=0x0B576AF8  token=0x60004AB  UnityEngine.UI.SelectableNaviGroup GetLayerGroup()
  RVA=0x0B5771A4  token=0x60004AC  System.Boolean TryFindExplicitNaviTarget(UnityEngine.UI.NaviDirection dir, UnityEngine.UI.Selectable& target)
  RVA=0x0B57732C  token=0x60004AD  System.Boolean banExplicitNavi(UnityEngine.UI.NaviDirection dir)
  RVA=0x03795F90  token=0x60004AE  System.Void _TryRegisterOnNaviGroup()
  RVA=0x0351DBC0  token=0x60004AF  System.Void _TryUnRegisterOnNaviGroup()
  RVA=0x06B8B404  token=0x60004B0  System.Void SetExplicitSelectOnLeft(UnityEngine.UI.Selectable selectable)
  RVA=0x06B8B468  token=0x60004B1  System.Void SetExplicitSelectOnRight(UnityEngine.UI.Selectable selectable)
  RVA=0x06B8B224  token=0x60004B2  System.Void SetExplicitSelectOnUp(UnityEngine.UI.Selectable selectable)
  RVA=0x06B8B274  token=0x60004B3  System.Void SetExplicitSelectOnDown(UnityEngine.UI.Selectable selectable)
  RVA=0x0B577144  token=0x60004B4  System.Void SetExplicitSelect(UnityEngine.UI.Selectable left, UnityEngine.UI.Selectable right, UnityEngine.UI.Selectable up, UnityEngine.UI.Selectable down)
  RVA=0x04CB0DE0  token=0x60004B5  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.NaviDirection
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.NaviDirection    None  // const
  public    static  UnityEngine.UI.NaviDirection    Up  // const
  public    static  UnityEngine.UI.NaviDirection    Down  // const
  public    static  UnityEngine.UI.NaviDirection    Left  // const
  public    static  UnityEngine.UI.NaviDirection    Right  // const
METHODS:
END_CLASS

CLASS: UnityEngine.UI.NaviStrategy
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.NaviStrategy     Normal  // const
  public    static  UnityEngine.UI.NaviStrategy     Strict  // const
  public    static  UnityEngine.UI.NaviStrategy     NotInternalFirst  // const
  public    static  UnityEngine.UI.NaviStrategy     VerticalOnlyWithInternalWrap  // const
  public    static  UnityEngine.UI.NaviStrategy     HorizontalOnlyWithInternalWrap  // const
METHODS:
END_CLASS

CLASS: UnityEngine.UI.NavigationBindingType
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UI.NavigationBindingTypeInValid  // const
  public    static  UnityEngine.UI.NavigationBindingTypeAllDirections  // const
  public    static  UnityEngine.UI.NavigationBindingTypeHorizontalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeVerticalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeArrowAllDirections  // const
  public    static  UnityEngine.UI.NavigationBindingTypeArrowHorizontalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeArrowVerticalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsAllDirections  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsHorizontalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsVerticalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsOnlyAllDirections  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsOnlyHorizontalOnly  // const
  public    static  UnityEngine.UI.NavigationBindingTypeRightJsOnlyVerticalOnly  // const
METHODS:
END_CLASS

CLASS: UnityEngine.UI.SelectableNaviGroup
TYPE:  abstract class
TOKEN: 0x2000075
SIZE:  0xA0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  isIsolate  // 0x18
  public            System.Boolean                  isLayer  // 0x19
  public            System.Boolean                  removeLayerOnDisable  // 0x1a
  public            System.Boolean                  isDummy  // 0x1b
  public            UnityEngine.UI.SelectableNaviGroupparentNaviGroup  // 0x20
  public            UnityEngine.UI.NaviStrategy     internalNaviStrategy  // 0x28
  public            UnityEngine.UI.NavigationBindingTypenavigationBindingType  // 0x2c
  public            System.Boolean                  enablePartner  // 0x30
  public            System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup>naviPartnerOnUp  // 0x38
  public            System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup>naviPartnerOnDown  // 0x40
  public            System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup>naviPartnerOnLeft  // 0x48
  public            System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup>naviPartnerOnRight  // 0x50
  protected         System.Boolean                  m_inited  // 0x58
  protected         UnityEngine.UI.Selectable       m_layerSelectedTarget  // 0x60
  public            System.Collections.Generic.HashSet<UnityEngine.UI.Selectable>targets  // 0x68
  public            System.Collections.Generic.HashSet<UnityEngine.UI.SelectableNaviGroup>subGroups  // 0x70
  public            UnityEngine.Events.UnityEvent<UnityEngine.UI.Selectable>onSetLayerSelectedTarget  // 0x78
  public            UnityEngine.Events.UnityEvent<System.Boolean>onIsTopLayerChanged  // 0x80
  public            UnityEngine.Events.UnityEvent<UnityEngine.UI.NaviDirection>onDefaultNaviFailed  // 0x88
  public            UnityEngine.Events.UnityEvent   onRemoveFromLayerStack  // 0x90
  protected         System.Boolean                  m_isTopLayer  // 0x98
PROPERTIES:
  LayerSelectedTarget  get=0x037D2DD0
  IsTopLayer  get=0x04D86C50  set=0x0B576228
METHODS:
  RVA=0x0B576220  token=0x60004B7  System.Void Reset()
  RVA=0x037D3D90  token=0x60004B8  System.Void OnEnable()
  RVA=0x053979C0  token=0x60004B9  System.Void OnDisable()
  RVA=0x039CFBF0  token=0x60004BA  System.Void OnDestroy()
  RVA=0x037D3E20  token=0x60004BB  System.Void _Init()
  RVA=0x037D4820  token=0x60004BC  System.Void TryFindParentNaviGroup()
  RVA=0x037D1390  token=0x60004BD  UnityEngine.UI.SelectableNaviGroup GetLayerGroup()
  RVA=0x037D1280  token=0x60004C0  System.Void SetLayerSelectedTarget(UnityEngine.UI.Selectable value, System.Boolean silentMode)
  RVA=0x037D14D0  token=0x60004C1  System.Void OnRemoveFromLayerStack()
  RVA=0x037D3FE0  token=0x60004C2  System.Void _OnIsTopLayerChanged()
  RVA=0x041E1670  token=0x60004C3  System.Void _OnSelectableNaviGroupInited()
  RVA=0x041E1670  token=0x60004C4  System.Void _BeforeSelectableNaviGroupDestroy()
  RVA=0x041E1670  token=0x60004C5  System.Void _OnSelectableNaviGroupDisabled()
  RVA=0x041E1670  token=0x60004C6  System.Void _OnSetLayerSelectedTarget(UnityEngine.UI.Selectable value)
  RVA=0x041E1670  token=0x60004C7  System.Void _OnRemoveFromLayerStack()
  RVA=0x040F99E0  token=0x60004C8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.SetPropertyUtility
TYPE:  static class
TOKEN: 0x2000076
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03046980  token=0x60004C9  System.Boolean SetColor(UnityEngine.Color& currentValue, UnityEngine.Color newValue)
  RVA=-1  // generic def  token=0x60004CA  System.Boolean SetStruct(T& currentValue, T newValue)
  RVA=-1  // generic def  token=0x60004CB  System.Boolean SetClass(T& currentValue, T newValue)
END_CLASS

CLASS: UnityEngine.UI.Slider
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x1F0
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IInitializePotentialDragHandler UnityEngine.UI.ICanvasElement
FIELDS:
  private           UnityEngine.RectTransform       m_FillRect  // 0x188
  private           UnityEngine.RectTransform       m_HandleRect  // 0x190
  private           UnityEngine.UI.Slider.Direction m_Direction  // 0x198
  private           System.Single                   m_MinValue  // 0x19c
  private           System.Single                   m_MaxValue  // 0x1a0
  private           System.Boolean                  m_WholeNumbers  // 0x1a4
  protected         System.Single                   m_Value  // 0x1a8
  private           UnityEngine.UI.Slider.SliderEventm_OnValueChanged  // 0x1b0
  private           UnityEngine.UI.Image            m_FillImage  // 0x1b8
  private           UnityEngine.Transform           m_FillTransform  // 0x1c0
  private           UnityEngine.RectTransform       m_FillContainerRect  // 0x1c8
  private           UnityEngine.Transform           m_HandleTransform  // 0x1d0
  private           UnityEngine.RectTransform       m_HandleContainerRect  // 0x1d8
  private           UnityEngine.Vector2             m_Offset  // 0x1e0
  private           UnityEngine.DrivenRectTransformTrackerm_Tracker  // 0x1e8
  private           System.Boolean                  m_DelayedUpdateVisuals  // 0x1e9
PROPERTIES:
  fillRect  get=0x04D8DA20  set=0x0B5782F8
  handleRect  get=0x04D8DD50  set=0x0B578360
  direction  get=0x04D8F930  set=0x0B57829C
  minValue  get=0x04DBF0F0  set=0x046FF340
  maxValue  get=0x04D8E940  set=0x046FF500
  wholeNumbers  get=0x04DBF100  set=0x0B5783F8
  value  get=0x03936F60  set=0x05390984
  normalizedValue  get=0x03936EB0  set=0x0B5783C8
  onValueChanged  get=0x04D8D960  set=0x053971DC
  stepSize  get=0x0B578270
  axis  get=0x03936E90
  reverseValue  get=0x03936F90
METHODS:
  RVA=0x0B578034  token=0x60004DA  System.Void SetValueWithoutNotify(System.Single input)
  RVA=0x0435C2D0  token=0x60004E0  System.Void .ctor()
  RVA=0x041E1670  token=0x60004E1  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x041E1670  token=0x60004E2  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x60004E3  System.Void GraphicUpdateComplete()
  RVA=0x03935610  token=0x60004E4  System.Void OnEnable()
  RVA=0x0447EC90  token=0x60004E5  System.Void OnDisable()
  RVA=0x0419EE40  token=0x60004E6  System.Void Update()
  RVA=0x0B5778C8  token=0x60004E7  System.Void OnDidApplyAnimationProperties()
  RVA=0x03937030  token=0x60004E8  System.Void UpdateCachedReferences()
  RVA=0x0B5777C4  token=0x60004E9  System.Single ClampValue(System.Single input)
  RVA=0x03936A10  token=0x60004EA  System.Void Set(System.Single input, System.Boolean sendCallback)
  RVA=0x041E1670  token=0x60004EB  System.Void OnValueChanged()
  RVA=0x041E1670  token=0x60004EC  System.Void OnClickSliderBar()
  RVA=0x041E1670  token=0x60004ED  System.Void OnClickSliderHandle()
  RVA=0x04736930  token=0x60004EE  System.Void OnRectTransformDimensionsChange()
  RVA=0x03936AE0  token=0x60004F1  System.Void UpdateVisuals()
  RVA=0x0B578040  token=0x60004F2  System.Void UpdateDrag(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Camera cam)
  RVA=0x04D9A920  token=0x60004F3  System.Single CustomProcessNormalizedValue(System.Single val)
  RVA=0x0B575710  token=0x60004F4  System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B577D1C  token=0x60004F5  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B577AE4  token=0x60004F6  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B577B34  token=0x60004F7  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0B57782C  token=0x60004F8  UnityEngine.UI.Selectable FindSelectableOnLeft()
  RVA=0x0B577860  token=0x60004F9  UnityEngine.UI.Selectable FindSelectableOnRight()
  RVA=0x0B577894  token=0x60004FA  UnityEngine.UI.Selectable FindSelectableOnUp()
  RVA=0x0B5777F8  token=0x60004FB  UnityEngine.UI.Selectable FindSelectableOnDown()
  RVA=0x0B5759FC  token=0x60004FC  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B577EF8  token=0x60004FD  System.Void SetDirection(UnityEngine.UI.Slider.Direction direction, System.Boolean includeRectLayouts)
  RVA=0x053953F4  token=0x60004FE  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.SpriteState
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x30
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           UnityEngine.Sprite              m_HighlightedSprite  // 0x10
  private           UnityEngine.Sprite              m_PressedSprite  // 0x18
  private           UnityEngine.Sprite              m_SelectedSprite  // 0x20
  private           UnityEngine.Sprite              m_DisabledSprite  // 0x28
PROPERTIES:
  highlightedSprite  get=0x02B76770  set=0x05392320
  pressedSprite  get=0x04D88320  set=0x067D5E9C
  selectedSprite  get=0x02B2ECC0  set=0x053908C0
  disabledSprite  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0B57846C  token=0x6000508  System.Boolean Equals(UnityEngine.UI.SpriteState other)
END_CLASS

CLASS: UnityEngine.UI.StencilMaterial
TYPE:  static class
TOKEN: 0x200007C
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<UnityEngine.UI.StencilMaterial.MatEntry>m_List  // static @ 0x0
  private   static  System.Boolean                  s_EnableStencilMaterialWarning  // static @ 0x8
METHODS:
  RVA=0x011EC580  token=0x6000509  UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID)
  RVA=0x041D2250  token=0x600050A  UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID, UnityEngine.Rendering.StencilOp operation, UnityEngine.Rendering.CompareFunction compareFunction, UnityEngine.Rendering.ColorWriteMask colorWriteMask)
  RVA=0x0B57864C  token=0x600050B  System.Void LogWarningWhenNotInBatchmode(System.String warning, UnityEngine.Object context)
  RVA=0x031DEDB0  token=0x600050C  UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID, UnityEngine.Rendering.StencilOp operation, UnityEngine.Rendering.CompareFunction compareFunction, UnityEngine.Rendering.ColorWriteMask colorWriteMask, System.Int32 readMask, System.Int32 writeMask)
  RVA=0x0351C220  token=0x600050D  System.Void Remove(UnityEngine.Material customMat)
  RVA=0x0B578538  token=0x600050E  System.Void ClearAll()
  RVA=0x04CECE10  token=0x600050F  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.Text
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x118
EXTENDS: UnityEngine.UI.MaskableGraphic
IMPLEMENTS: UnityEngine.UI.ILayoutElement
FIELDS:
  private           UnityEngine.UI.FontData         m_FontData  // 0xe8
  protected         System.String                   m_Text  // 0xf0
  private           UnityEngine.TextGenerator       m_TextCache  // 0xf8
  private           UnityEngine.TextGenerator       m_TextCacheForLayout  // 0x100
  protected static  UnityEngine.Material            s_DefaultText  // static @ 0x0
  protected         System.Boolean                  m_DisableFontTextureRebuiltCallback  // 0x108
  private   readonly UnityEngine.UIVertex[]          m_TempVerts  // 0x110
PROPERTIES:
  hasCullOverride  get=0x012081B0
  cachedTextGenerator  get=0x0B579360
  cachedTextGeneratorForLayout  get=0x0B5792E4
  mainTexture  get=0x0B5794B4
  font  get=0x0B579454  set=0x0B579B38
  text  get=0x04D8D1A0  set=0x0B579DCC
  supportRichText  get=0x0B5799F0  set=0x0B579D88
  resizeTextForBestFit  get=0x0B579990  set=0x0B579CBC
  resizeTextMinSize  get=0x0B5799D0  set=0x0B579D44
  resizeTextMaxSize  get=0x0B5799B0  set=0x0B579D00
  alignment  get=0x0B5792C4  set=0x0B579A6C
  alignByGeometry  get=0x0B5792A4  set=0x0B579A30
  fontSize  get=0x0B579414  set=0x0B579AB0
  horizontalOverflow  get=0x0B579474  set=0x0B579C2C
  verticalOverflow  get=0x0B579A10  set=0x0B579E98
  lineSpacing  get=0x0B579494  set=0x0B579C70
  fontStyle  get=0x0B579434  set=0x0B579AF4
  pixelsPerUnit  get=0x0B57963C
  minWidth  get=0x04D879D0
  preferredWidth  get=0x0B5798A0
  flexibleWidth  get=0x04DA9E10
  minHeight  get=0x04D879D0
  preferredHeight  get=0x0B579778
  flexibleHeight  get=0x04DA9E10
  layoutPriority  get=0x011EC580
METHODS:
  RVA=0x0B5791F8  token=0x6000512  System.Void .ctor()
  RVA=0x0B5787C4  token=0x6000516  System.Void FontTextureChanged()
  RVA=0x0B578B70  token=0x6000532  System.Void OnEnable()
  RVA=0x0B578B24  token=0x6000533  System.Void OnDisable()
  RVA=0x0B579190  token=0x6000534  System.Void UpdateGeometry()
  RVA=0x0B57876C  token=0x6000535  System.Void AssignDefaultFont()
  RVA=0x0B5786D4  token=0x6000536  System.Void AssignDefaultFontIfNecessary()
  RVA=0x0B57888C  token=0x6000537  UnityEngine.TextGenerationSettings GetGenerationSettings(UnityEngine.Vector2 extents)
  RVA=0x0B578A98  token=0x6000538  UnityEngine.Vector2 GetTextAnchorPivot(UnityEngine.TextAnchor anchor)
  RVA=0x0B578BD8  token=0x6000539  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill)
  RVA=0x041E1670  token=0x600053A  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x600053B  System.Void CalculateLayoutInputVertical()
END_CLASS

CLASS: UnityEngine.UI.Toggle
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x1B0
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.UI.ICanvasElement
FIELDS:
  public            UnityEngine.UI.Toggle.ToggleTransitiontoggleTransition  // 0x188
  public            UnityEngine.UI.Graphic          graphic  // 0x190
  private           UnityEngine.UI.ToggleGroup      m_Group  // 0x198
  public            UnityEngine.UI.Toggle.ToggleEventonValueChanged  // 0x1a0
  private           System.Boolean                  m_IsOn  // 0x1a8
PROPERTIES:
  group  get=0x04D8DED0  set=0x0B57A54C
  isOn  get=0x04DA3310  set=0x0378E5A0
METHODS:
  RVA=0x0435DEF0  token=0x6000545  System.Void .ctor()
  RVA=0x041E1670  token=0x6000546  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x041E1670  token=0x6000547  System.Void LayoutComplete()
  RVA=0x041E1670  token=0x6000548  System.Void GraphicUpdateComplete()
  RVA=0x0B57A390  token=0x6000549  System.Void OnDestroy()
  RVA=0x0378E820  token=0x600054A  System.Void OnEnable()
  RVA=0x0378E750  token=0x600054B  System.Void OnDisable()
  RVA=0x0B57A40C  token=0x600054C  System.Void OnDidApplyAnimationProperties()
  RVA=0x0378E9F0  token=0x600054D  System.Void SetToggleGroup(UnityEngine.UI.ToggleGroup newGroup, System.Boolean setMemberValue)
  RVA=0x053979D0  token=0x6000550  System.Void SetIsOnWithoutNotify(System.Boolean value)
  RVA=0x0378E5B0  token=0x6000551  System.Void Set(System.Boolean value, System.Boolean sendCallback)
  RVA=0x0378E920  token=0x6000552  System.Void PlayEffect(System.Boolean instant)
  RVA=0x05393868  token=0x6000553  System.Void Start()
  RVA=0x0B57A348  token=0x6000554  System.Void InternalToggle()
  RVA=0x0B57A518  token=0x6000555  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0AC6E908  token=0x6000556  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x041E1670  token=0x6000557  System.Void OnValueChange()
  RVA=0x053953F4  token=0x6000558  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
END_CLASS

CLASS: UnityEngine.UI.ToggleGroup
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           System.Boolean                  m_AllowSwitchOff  // 0x18
  protected         System.Collections.Generic.List<UnityEngine.UI.Toggle>m_Toggles  // 0x20
PROPERTIES:
  allowSwitchOff  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x045C5E70  token=0x600055C  System.Void .ctor()
  RVA=0x0481BFF0  token=0x600055D  System.Void Start()
  RVA=0x0481BFF0  token=0x600055E  System.Void OnEnable()
  RVA=0x0B57A1FC  token=0x600055F  System.Void ValidateToggleIsInGroup(UnityEngine.UI.Toggle toggle)
  RVA=0x0B579F4C  token=0x6000560  System.Void NotifyToggleOn(UnityEngine.UI.Toggle toggle, System.Boolean sendCallback)
  RVA=0x0B57A1A8  token=0x6000561  System.Void UnregisterToggle(UnityEngine.UI.Toggle toggle)
  RVA=0x0B57A07C  token=0x6000562  System.Void RegisterToggle(UnityEngine.UI.Toggle toggle)
  RVA=0x0481C000  token=0x6000563  System.Void EnsureValidState()
  RVA=0x0481C340  token=0x6000564  System.Boolean AnyTogglesOn()
  RVA=0x0481C260  token=0x6000565  System.Collections.Generic.IEnumerable<UnityEngine.UI.Toggle> ActiveToggles()
  RVA=0x0B579EDC  token=0x6000566  UnityEngine.UI.Toggle GetFirstActiveToggle()
  RVA=0x0B57A0D0  token=0x6000567  System.Void SetAllTogglesOff(System.Boolean sendCallback)
END_CLASS

CLASS: UnityEngine.UI.ReflectionMethodsCache
TYPE:  class
TOKEN: 0x2000084
SIZE:  0x40
FIELDS:
  public            UnityEngine.UI.ReflectionMethodsCache.Raycast3DCallbackraycast3D  // 0x10
  public            UnityEngine.UI.ReflectionMethodsCache.RaycastAllCallbackraycast3DAll  // 0x18
  public            UnityEngine.UI.ReflectionMethodsCache.GetRaycastNonAllocCallbackgetRaycastNonAlloc  // 0x20
  public            UnityEngine.UI.ReflectionMethodsCache.Raycast2DCallbackraycast2D  // 0x28
  public            UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllCallbackgetRayIntersectionAll  // 0x30
  public            UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallbackgetRayIntersectionAllNonAlloc  // 0x38
  private   static  UnityEngine.UI.ReflectionMethodsCaches_ReflectionMethodsCache  // static @ 0x0
PROPERTIES:
  Singleton  get=0x0B574964
METHODS:
  RVA=0x0B573EA8  token=0x600056C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.VertexHelper
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x60
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.Vector3>m_Positions  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Color32>m_Colors  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Vector4>m_Uv0S  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Vector4>m_Uv1S  // 0x28
  private           System.Collections.Generic.List<UnityEngine.Vector4>m_Uv2S  // 0x30
  private           System.Collections.Generic.List<UnityEngine.Vector4>m_Uv3S  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Vector3>m_Normals  // 0x40
  private           System.Collections.Generic.List<UnityEngine.Vector4>m_Tangents  // 0x48
  private           System.Collections.Generic.List<System.Int32>m_Indices  // 0x50
  private   static readonly UnityEngine.Vector4             s_DefaultTangent  // static @ 0x0
  private   static readonly UnityEngine.Vector3             s_DefaultNormal  // static @ 0x10
  private           System.Boolean                  m_ListsInitalized  // 0x58
PROPERTIES:
  currentVertCount  get=0x03141B90
  currentIndexCount  get=0x0B57B0F0
METHODS:
  RVA=0x041E1670  token=0x6000586  System.Void .ctor()
  RVA=0x0B57AEB8  token=0x6000587  System.Void .ctor(UnityEngine.Mesh m)
  RVA=0x03242670  token=0x6000588  System.Void InitializeListIfRequired()
  RVA=0x04028CD0  token=0x6000589  System.Void Dispose()
  RVA=0x031F9AA0  token=0x600058A  System.Void Clear()
  RVA=0x0B57A9B4  token=0x600058D  System.Void PopulateUIVertex(UnityEngine.UIVertex& vertex, System.Int32 i)
  RVA=0x0B57AB70  token=0x600058E  System.Void SetUIVertex(UnityEngine.UIVertex vertex, System.Int32 i)
  RVA=0x031F3240  token=0x600058F  System.Void FillMesh(UnityEngine.Mesh mesh)
  RVA=0x03241FE0  token=0x6000590  System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0, UnityEngine.Vector4 uv1, UnityEngine.Vector4 uv2, UnityEngine.Vector4 uv3, UnityEngine.Vector3 normal, UnityEngine.Vector4 tangent)
  RVA=0x03241930  token=0x6000591  System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0, UnityEngine.Vector4 uv1, UnityEngine.Vector3 normal, UnityEngine.Vector4 tangent)
  RVA=0x03241250  token=0x6000592  System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0)
  RVA=0x03BA6F90  token=0x6000593  System.Void AddVert(UnityEngine.UIVertex v)
  RVA=0x03131840  token=0x6000594  System.Void AddTriangle(System.Int32 idx0, System.Int32 idx1, System.Int32 idx2)
  RVA=0x037972C0  token=0x6000595  System.Void AddUIVertexQuad(UnityEngine.UIVertex[] verts)
  RVA=0x0B57A804  token=0x6000596  System.Void AddUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<System.Int32> indices)
  RVA=0x0B57A8C4  token=0x6000597  System.Void AddUIVertexTriangleStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts)
  RVA=0x0B57A93C  token=0x6000598  System.Void GetUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> stream)
  RVA=0x049704A0  token=0x6000599  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.UI.BaseVertexEffect
TYPE:  abstract class
TOKEN: 0x200008C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600059A  System.Void ModifyVertices(System.Collections.Generic.List<UnityEngine.UIVertex> vertices)
  RVA=0x041E1670  token=0x600059B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.BaseMeshEffect
TYPE:  abstract class
TOKEN: 0x200008D
SIZE:  0x20
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.IMeshModifier
FIELDS:
  private           UnityEngine.UI.Graphic          m_Graphic  // 0x18
PROPERTIES:
  graphic  get=0x0B57BF74
METHODS:
  RVA=0x0B57BEF4  token=0x600059D  System.Void OnEnable()
  RVA=0x0B57BE74  token=0x600059E  System.Void OnDisable()
  RVA=0x0B57BDF4  token=0x600059F  System.Void OnDidApplyAnimationProperties()
  RVA=0x0B57BCF0  token=0x60005A0  System.Void ModifyMesh(UnityEngine.Mesh mesh)
  RVA=-1  // abstract  token=0x60005A1  System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x05393520  token=0x60005A2  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.IVertexModifier
TYPE:  interface
TOKEN: 0x200008E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60005A3  System.Void ModifyVertices(System.Collections.Generic.List<UnityEngine.UIVertex> verts)
END_CLASS

CLASS: UnityEngine.UI.IMeshModifier
TYPE:  interface
TOKEN: 0x200008F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60005A4  System.Void ModifyMesh(UnityEngine.Mesh mesh)
  RVA=-1  // abstract  token=0x60005A5  System.Void ModifyMesh(UnityEngine.UI.VertexHelper verts)
END_CLASS

CLASS: UnityEngine.UI.Outline
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x40
EXTENDS: UnityEngine.UI.Shadow
FIELDS:
METHODS:
  RVA=0x0B57DC84  token=0x60005A6  System.Void .ctor()
  RVA=0x0B57D9EC  token=0x60005A7  System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh)
END_CLASS

CLASS: UnityEngine.UI.PositionAsUV1
TYPE:  class
TOKEN: 0x2000091
SIZE:  0x20
EXTENDS: UnityEngine.UI.BaseMeshEffect
FIELDS:
METHODS:
  RVA=0x05393520  token=0x60005A8  System.Void .ctor()
  RVA=0x0B582B30  token=0x60005A9  System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh)
END_CLASS

CLASS: UnityEngine.UI.Shadow
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x40
EXTENDS: UnityEngine.UI.BaseMeshEffect
FIELDS:
  private           UnityEngine.Color               m_EffectColor  // 0x20
  private           UnityEngine.Vector2             m_EffectDistance  // 0x30
  private           System.Boolean                  m_UseGraphicAlpha  // 0x38
  private   static  System.Single                   kMaxEffectDistance  // const
PROPERTIES:
  effectColor  get=0x04D8C200  set=0x0B583E40
  effectDistance  get=0x04D9E6C0  set=0x0B583ECC
  useGraphicAlpha  get=0x011F2A70  set=0x0B583FF4
METHODS:
  RVA=0x04D79B90  token=0x60005AA  System.Void .ctor()
  RVA=0x0B583A8C  token=0x60005B1  System.Void ApplyShadowZeroAlloc(System.Collections.Generic.List<UnityEngine.UIVertex> verts, UnityEngine.Color32 color, System.Int32 start, System.Int32 end, System.Single x, System.Single y)
  RVA=0x0B583D08  token=0x60005B2  System.Void ApplyShadow(System.Collections.Generic.List<UnityEngine.UIVertex> verts, UnityEngine.Color32 color, System.Int32 start, System.Int32 end, System.Single x, System.Single y)
  RVA=0x0B583D18  token=0x60005B3  System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh)
END_CLASS

CLASS: UnityEngine.UI.Collections.IndexedSet`1
TYPE:  class
TOKEN: 0x2000093
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.List<T>m_List  // 0x0
  private           System.Collections.Generic.Dictionary<T,System.Int32>m_Dictionary  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60005B4  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x60005B5  System.Boolean AddUnique(T item)
  RVA=-1  // not resolved  token=0x60005B6  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x60005B7  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x60005B8  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60005B9  System.Void Clear()
  RVA=-1  // not resolved  token=0x60005BA  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x60005BB  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x60005BE  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x60005BF  System.Void Insert(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x60005C0  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60005C3  System.Void RemoveAll(System.Predicate<T> match)
  RVA=-1  // not resolved  token=0x60005C4  System.Void Sort(System.Comparison<T> sortLayoutFunction)
  RVA=-1  // not resolved  token=0x60005C5  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UI.CoroutineTween.ITweenValue
TYPE:  interface
TOKEN: 0x2000094
FIELDS:
PROPERTIES:
  ignoreTimeScale  get=-1  // abstract
  duration  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60005C6  System.Void TweenValue(System.Single floatPercentage)
  RVA=-1  // abstract  token=0x60005C9  System.Boolean ValidTarget()
END_CLASS

CLASS: UnityEngine.UI.CoroutineTween.ColorTween
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x48
IMPLEMENTS: UnityEngine.UI.CoroutineTween.ITweenValue
FIELDS:
  private           UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenCallbackm_Target  // 0x10
  private           UnityEngine.Color               m_StartColor  // 0x18
  private           UnityEngine.Color               m_TargetColor  // 0x28
  private           UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenModem_TweenMode  // 0x38
  private           System.Single                   m_Duration  // 0x3c
  private           System.Boolean                  m_IgnoreTimeScale  // 0x40
PROPERTIES:
  startColor  get=0x015AD730  set=0x04DA4E20
  targetColor  get=0x04D890C0  set=0x04D890D0
  tweenMode  get=0x011F2A90  set=0x011F2AA0
  duration  get=0x04D86AF0  set=0x04D86B00
  ignoreTimeScale  get=0x04D865F0  set=0x04D86620
METHODS:
  RVA=0x0374AE50  token=0x60005D4  System.Void TweenValue(System.Single floatPercentage)
  RVA=0x04259CB0  token=0x60005D5  System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<UnityEngine.Color> callback)
  RVA=0x04D865F0  token=0x60005D6  System.Boolean GetIgnoreTimescale()
  RVA=0x04D86AF0  token=0x60005D7  System.Single GetDuration()
  RVA=0x0374AFD0  token=0x60005D8  System.Boolean ValidTarget()
END_CLASS

CLASS: UnityEngine.UI.CoroutineTween.FloatTween
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x28
IMPLEMENTS: UnityEngine.UI.CoroutineTween.ITweenValue
FIELDS:
  private           UnityEngine.UI.CoroutineTween.FloatTween.FloatTweenCallbackm_Target  // 0x10
  private           System.Single                   m_StartValue  // 0x18
  private           System.Single                   m_TargetValue  // 0x1c
  private           System.Single                   m_Duration  // 0x20
  private           System.Boolean                  m_IgnoreTimeScale  // 0x24
PROPERTIES:
  startValue  get=0x04D9C280  set=0x04DAC1D0
  targetValue  get=0x04D9CC80  set=0x04DACD70
  duration  get=0x04D85F70  set=0x04D88280
  ignoreTimeScale  get=0x04D8EE50  set=0x04D8EE70
METHODS:
  RVA=0x0B57D648  token=0x60005E2  System.Void TweenValue(System.Single floatPercentage)
  RVA=0x0B57D5B0  token=0x60005E3  System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<System.Single> callback)
  RVA=0x04D8EE50  token=0x60005E4  System.Boolean GetIgnoreTimescale()
  RVA=0x04D85F70  token=0x60005E5  System.Single GetDuration()
  RVA=0x03A7A090  token=0x60005E6  System.Boolean ValidTarget()
END_CLASS

CLASS: UnityEngine.UI.CoroutineTween.TweenRunner`1
TYPE:  class
TOKEN: 0x200009A
FIELDS:
  protected         UnityEngine.MonoBehaviour       m_CoroutineContainer  // 0x0
  protected         System.Collections.IEnumerator  m_Tween  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60005E8  System.Collections.IEnumerator Start(T tweenInfo)
  RVA=-1  // not resolved  token=0x60005E9  System.Void Init(UnityEngine.MonoBehaviour coroutineContainer)
  RVA=-1  // not resolved  token=0x60005EA  System.Void StartTween(T info)
  RVA=-1  // not resolved  token=0x60005EB  System.Void StopTween()
  RVA=-1  // not resolved  token=0x60005EC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UIElements.PanelEventHandler
TYPE:  class
TOKEN: 0x200009C
SIZE:  0x38
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerMoveHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.EventSystems.ICancelHandler UnityEngine.EventSystems.IMoveHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.EventSystems.ISelectHandler UnityEngine.EventSystems.IDeselectHandler UnityEngine.EventSystems.IPointerExitHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.UIElements.IRuntimePanelComponent
FIELDS:
  private           UnityEngine.UIElements.BaseRuntimePanelm_Panel  // 0x18
  private   readonly UnityEngine.UIElements.PanelEventHandler.PointerEventm_PointerEvent  // 0x20
  private           System.Boolean                  m_Selecting  // 0x28
  private           UnityEngine.Event               m_Event  // 0x30
  private   static  UnityEngine.EventModifiers      s_Modifiers  // static @ 0x0
PROPERTIES:
  panel  get=0x0385B100  set=0x0B57F5F0
  selectableGameObject  get=0x0B57F5DC
  eventSystem  get=0x0B57F58C
METHODS:
  RVA=0x0B57DE14  token=0x60005F7  System.Void OnEnable()
  RVA=0x0B57DD78  token=0x60005F8  System.Void OnDisable()
  RVA=0x0B57ECC8  token=0x60005F9  System.Void RegisterCallbacks()
  RVA=0x0B57F28C  token=0x60005FA  System.Void UnregisterCallbacks()
  RVA=0x0B57DEF8  token=0x60005FB  System.Void OnPanelDestroyed()
  RVA=0x0B57DD80  token=0x60005FC  System.Void OnElementFocus(UnityEngine.UIElements.FocusEvent e)
  RVA=0x041E1670  token=0x60005FD  System.Void OnElementBlur(UnityEngine.UIElements.BlurEvent e)
  RVA=0x0B57E6F8  token=0x60005FE  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57DD64  token=0x60005FF  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57E378  token=0x6000600  System.Void OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57E464  token=0x6000601  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57DF04  token=0x6000602  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57E13C  token=0x6000603  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57E0C8  token=0x6000604  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57E790  token=0x6000605  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57DC98  token=0x6000606  System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B57DE1C  token=0x6000607  System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0B57E5B8  token=0x6000608  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B57EE70  token=0x6000609  System.Void SendEvent(UnityEngine.UIElements.EventBase e, UnityEngine.EventSystems.BaseEventData sourceEventData)
  RVA=0x0B57EEBC  token=0x600060A  System.Void SendEvent(UnityEngine.UIElements.EventBase e, UnityEngine.Event sourceEvent)
  RVA=0x0B57F434  token=0x600060B  System.Void Update()
  RVA=0x0B57DC8C  token=0x600060C  System.Void LateUpdate()
  RVA=0x0B57E85C  token=0x600060D  System.Void ProcessImguiEvents(System.Boolean isSelected)
  RVA=0x0B57E9A8  token=0x600060E  System.Void ProcessKeyboardEvent(UnityEngine.Event e)
  RVA=0x0B57EAA4  token=0x600060F  System.Void ProcessTabEvent(UnityEngine.Event e)
  RVA=0x0B57F0DC  token=0x6000610  System.Void SendTabEvent(UnityEngine.Event e, System.Int32 direction)
  RVA=0x0B57EFF4  token=0x6000611  System.Void SendKeyUpEvent(UnityEngine.Event e, UnityEngine.KeyCode keyCode, UnityEngine.EventModifiers modifiers)
  RVA=0x0B57EF0C  token=0x6000612  System.Void SendKeyDownEvent(UnityEngine.Event e, UnityEngine.KeyCode keyCode, UnityEngine.EventModifiers modifiers)
  RVA=0x0B57F1A0  token=0x6000613  System.Void SendTextEvent(UnityEngine.Event e, System.Char c, UnityEngine.EventModifiers modifiers)
  RVA=0x0B57EB18  token=0x6000614  System.Boolean ReadPointerData(UnityEngine.UIElements.PanelEventHandler.PointerEvent pe, UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType)
  RVA=0x0B57F4F4  token=0x6000615  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.UIElements.PanelRaycaster
TYPE:  class
TOKEN: 0x200009F
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.BaseRaycaster
IMPLEMENTS: UnityEngine.UIElements.IRuntimePanelComponent
FIELDS:
  private           UnityEngine.UIElements.BaseRuntimePanelm_Panel  // 0x20
PROPERTIES:
  panel  get=0x04D862C0  set=0x0B57FCB4
  selectableGameObject  get=0x0B57FC84
  sortOrderPriority  get=0x0B57FC98
  renderOrderPriority  get=0x0B57FC6C
  eventCamera  get=0x011EC580
METHODS:
  RVA=0x0B57FB44  token=0x6000645  System.Void RegisterCallbacks()
  RVA=0x0B57FBD8  token=0x6000646  System.Void UnregisterCallbacks()
  RVA=0x0B57F660  token=0x6000647  System.Void OnPanelDestroyed()
  RVA=0x0B57F66C  token=0x600064B  System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList)
  RVA=0x04D94B90  token=0x600064D  System.Int32 ConvertFloatBitsToInt(System.Single f)
  RVA=0x05393520  token=0x600064E  System.Void .ctor()
END_CLASS

