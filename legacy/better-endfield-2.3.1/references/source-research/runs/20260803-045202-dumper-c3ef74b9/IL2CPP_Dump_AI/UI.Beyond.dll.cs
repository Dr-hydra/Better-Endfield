// ========================================================
// Dumped by @desirepro
// Assembly: UI.Beyond.dll
// Classes:  414
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

CLASS: UIMinSizeAdjust
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.RectTransform       _panelRect  // 0x18
METHODS:
  RVA=0x0B08A864  token=0x6000001  System.Void Start()
  RVA=0x0B08A86C  token=0x6000002  System.Void _SetTextureScaleByWidth()
  RVA=0x05393520  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: RichTextInfo
TYPE:  abstract class
TOKEN: 0x200000F
SIZE:  0x18
FIELDS:
  public            System.Int32                    index  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000092  System.Void .ctor()
END_CLASS

CLASS: InlineImageInfo
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x38
EXTENDS: RichTextInfo
FIELDS:
  public            System.String                   path  // 0x18
  public            System.Single                   width  // 0x20
  public            System.Single                   height  // 0x24
  public            UnityEngine.Color               color  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000093  System.Void .ctor()
END_CLASS

CLASS: RichTextTagType
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIText.RichTextTagTypeImage  // const
  public    static  Beyond.UI.UIText.RichTextTagTypeWidth  // const
  public    static  Beyond.UI.UIText.RichTextTagTypeHeight  // const
  public    static  Beyond.UI.UIText.RichTextTagTypeScale  // const
  public    static  Beyond.UI.UIText.RichTextTagTypeColor  // const
METHODS:
END_CLASS

CLASS: RichTextStyleSeq
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIText.RichTextStyleSeqNORMAL  // const
  public    static  Beyond.UI.UIText.RichTextStyleSeqDARK_BACKGROUND  // const
  public    static  Beyond.UI.UIText.RichTextStyleSeqLIGHT_BACKGROUND  // const
METHODS:
END_CLASS

CLASS: ParamValueType
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeNone  // const
  public    static  Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeString  // const
  public    static  Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeInt  // const
  public    static  Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeFloat  // const
  public    static  Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeColor  // const
METHODS:
END_CLASS

CLASS: RichTextTag
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            Beyond.UI.UIText.RichTextTagTyperichTextTagType  // 0x10
  public            Beyond.UI.UIText.RichTextAnalyzer.ParamValueTypeparamValueType  // 0x14
METHODS:
END_CLASS

CLASS: RichTextParam
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIText.RichTextTagTyperichTextTagType  // 0x10
  public            Beyond.UI.UIText.RichTextAnalyzer.AnyTypeStructvalue  // 0x18
METHODS:
END_CLASS

CLASS: AnyTypeStruct
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x20
FIELDS:
  public            System.Int32                    intValue  // 0x10
  public            System.Single                   floatValue  // 0x10
  public            System.String                   stringValue  // 0x10
  public            UnityEngine.Color               colorValue  // 0x10
METHODS:
END_CLASS

CLASS: ProcessRichTextEntryFunc
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F283E0  token=0x600009C  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x062213C0  token=0x600009D  Beyond.UI.UIText.RichTextInfo Invoke(Beyond.UI.UIText.RichTextAnalyzer.RichTextParam mainParam, System.Collections.Generic.List<Beyond.UI.UIText.RichTextAnalyzer.RichTextParam> subParams, Cysharp.Text.Utf16ValueStringBuilder& stringBuilder)
  RVA=0x0B08716C  token=0x600009E  System.IAsyncResult BeginInvoke(Beyond.UI.UIText.RichTextAnalyzer.RichTextParam mainParam, System.Collections.Generic.List<Beyond.UI.UIText.RichTextAnalyzer.RichTextParam> subParams, Cysharp.Text.Utf16ValueStringBuilder& stringBuilder, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD0344  token=0x600009F  Beyond.UI.UIText.RichTextInfo EndInvoke(Cysharp.Text.Utf16ValueStringBuilder& stringBuilder, System.IAsyncResult result)
END_CLASS

CLASS: RichTextAnalyzer
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
  private   static  System.Char                     CUSTOM_RICH_TEXT_LEFT_BRACKET  // const
  private   static  System.Char                     CUSTOM_RICH_TEXT_RIGHT_BRACKET  // const
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIText.RichTextAnalyzer.RichTextTag>s_tagToTypeDic  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIText.RichTextAnalyzer.RichTextTag>s_tagAttributeToTypeDic  // static @ 0x8
  public    static  System.String                   FONT_SHADOW_OPENING_TAG  // const
  public    static  System.String                   FONT_SHADOW_CLOSING_TAG  // const
  public    static  System.String                   FONT_MAT_OPENING_TAG  // const
  public    static  System.String                   FONT_MAT_CLOSING_TAG  // const
  private   static readonly System.Collections.Generic.List<Beyond.UI.UIText.RichTextAnalyzer.RichTextParam>s_subParams  // static @ 0x10
METHODS:
  RVA=0x02F27A80  token=0x6000094  System.String PreAnalyzeRichText(System.String text, System.Boolean needParseActionId)
  RVA=0x02F27370  token=0x6000095  System.String AnalyzeRichText(System.String text, Beyond.UI.UIText.RichTextAnalyzer.ProcessRichTextEntryFunc processFunc, System.Collections.Generic.List<Beyond.UI.UIText.RichTextInfo>& infos)
  RVA=0x03524C70  token=0x6000096  System.Boolean _TryParseRichTextEntry(System.ReadOnlySpan<System.Char> span, Cysharp.Text.Utf16ValueStringBuilder& stringBuilder, Beyond.UI.UIText.RichTextAnalyzer.ProcessRichTextEntryFunc processFunc, Beyond.UI.UIText.RichTextInfo& richTextInfo)
  RVA=0x035248E0  token=0x6000097  System.Boolean _TryParseParam(System.ReadOnlySpan<System.Char> span, System.Boolean& isMain, Beyond.UI.UIText.RichTextAnalyzer.RichTextParam& param)
  RVA=0x0B087480  token=0x6000098  Beyond.UI.UIText.RichTextInfo ProcessRichTextEntryFallback(Beyond.UI.UIText.RichTextAnalyzer.RichTextParam mainParam, System.Collections.Generic.List<Beyond.UI.UIText.RichTextAnalyzer.RichTextParam> subParams, Cysharp.Text.Utf16ValueStringBuilder& stringBuilder)
  RVA=0x0B087214  token=0x6000099  System.Int32 GetCharacterCountWithoutRichText(System.ReadOnlySpan<System.Char> str)
  RVA=0x03525BA0  token=0x600009A  System.Int32 _GetHashCode(System.ReadOnlySpan<System.Char> str)
  RVA=0x04865A10  token=0x600009B  System.Void .cctor()
END_CLASS

CLASS: HyperlinkUITextWrap
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIText                uiText  // 0x10
  public            System.String                   hyperlinkUITextGroupId  // 0x18
  public            System.Int32                    order  // 0x20
  public            System.Boolean                  isDisplayable  // 0x24
METHODS:
  RVA=0x0B08693C  token=0x60000A0  System.Void Clear()
  RVA=0x041E1670  token=0x60000A1  System.Void .ctor()
END_CLASS

CLASS: HyperlinkUITextGroup
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.HashSet<Beyond.UI.UIText.HyperlinkUITextWrap>wrapSet  // 0x10
  public            System.String                   groupId  // 0x18
  public            System.Int32                    displayableCount  // 0x20
METHODS:
  RVA=0x0B085D58  token=0x60000A2  System.Void Add(Beyond.UI.UIText.HyperlinkUITextWrap wrap)
  RVA=0x0B085E78  token=0x60000A3  System.Void Remove(Beyond.UI.UIText.HyperlinkUITextWrap wrap)
  RVA=0x0B085DF4  token=0x60000A4  System.Void ChangeDisplayable(Beyond.UI.UIText.HyperlinkUITextWrap wrap, System.Boolean displayable)
  RVA=0x0B085F14  token=0x60000A5  System.Void _AddDisplayableCount()
  RVA=0x0B08603C  token=0x60000A6  System.Void _SubDisplayableCount()
  RVA=0x0B085F70  token=0x60000A7  System.Void _NotifyDisplayableChanged()
  RVA=0x0B086094  token=0x60000A8  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.UI.UIText.HyperlinkUITextManager.<>c<>9  // static @ 0x0
  public    static  System.Comparison<Beyond.UI.UIText.HyperlinkUITextWrap><>9__13_0  // static @ 0x8
  public    static  System.Action<Beyond.UI.UIText.HyperlinkUITextGroup><>9__16_0  // static @ 0x10
  public    static  System.Action<Beyond.UI.UIText.HyperlinkUITextWrap><>9__16_1  // static @ 0x18
METHODS:
  RVA=0x04D47110  token=0x60000B5  System.Void .cctor()
  RVA=0x041E1670  token=0x60000B6  System.Void .ctor()
  RVA=0x0B0875DC  token=0x60000B7  System.Int32 <GetGroupDisplayableUIText>b__13_0(Beyond.UI.UIText.HyperlinkUITextWrap a, Beyond.UI.UIText.HyperlinkUITextWrap b)
  RVA=0x0B0876FC  token=0x60000B8  System.Void <.ctor>b__16_0(Beyond.UI.UIText.HyperlinkUITextGroup group)
  RVA=0x0B087750  token=0x60000B9  System.Void <.ctor>b__16_1(Beyond.UI.UIText.HyperlinkUITextWrap wrap)
END_CLASS

CLASS: HyperlinkUITextManager
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x28
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,Beyond.UI.UIText.HyperlinkUITextGroup>m_hyperlinkUITextGroupMap  // 0x10
  private           Beyond.PoolCore.ObjectPool<Beyond.UI.UIText.HyperlinkUITextGroup>m_hyperlinkUITextGroupPool  // 0x18
  private           Beyond.PoolCore.ObjectPool<Beyond.UI.UIText.HyperlinkUITextWrap>m_hyperlinkUITextWrapPool  // 0x20
  private   static  Beyond.UI.UIText.HyperlinkUITextManagers_instance  // static @ 0x0
  private   static readonly System.Collections.Generic.List<Beyond.UI.UIText.HyperlinkUITextWrap>SORT_WRAP_TEMP_LIST  // static @ 0x8
PROPERTIES:
  instance  get=0x0351CAF0
METHODS:
  RVA=0x0B086134  token=0x60000AA  System.Void AddWrap(Beyond.UI.UIText uiText, System.String groupId, System.Int32 order)
  RVA=0x0351CC20  token=0x60000AB  System.Void RemoveWrap(Beyond.UI.UIText uiText)
  RVA=0x0B086280  token=0x60000AC  System.Void ChangeWrapGroup(Beyond.UI.UIText.HyperlinkUITextWrap wrap, System.String newGroupId, System.Int32 newOrder)
  RVA=0x0B0863E4  token=0x60000AD  System.Void ChangeWrapOrder(Beyond.UI.UIText.HyperlinkUITextWrap wrap, System.Int32 newOrder)
  RVA=0x0351D8E0  token=0x60000AE  System.Void UpdateWrapDisplayable(Beyond.UI.UIText uiText)
  RVA=0x0390F700  token=0x60000AF  System.Boolean IsGroupDisplayable(System.String groupId)
  RVA=0x0B086450  token=0x60000B0  System.Collections.Generic.List<Beyond.UI.UIText> GetGroupDisplayableUIText(System.String groupId)
  RVA=0x0390F750  token=0x60000B1  Beyond.UI.UIText.HyperlinkUITextGroup _GetGroup(System.String groupId, System.Boolean autoCreate)
  RVA=0x0351D9E0  token=0x60000B2  System.Boolean _IsUITextDisplayable(Beyond.UI.UIText uiText)
  RVA=0x048F8260  token=0x60000B3  System.Void .ctor()
  RVA=0x04D12770  token=0x60000B4  System.Void .cctor()
END_CLASS

CLASS: ImageGOInfo
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x28
FIELDS:
  public            UnityEngine.GameObject          go  // 0x10
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.UI.Image            image  // 0x20
METHODS:
END_CLASS

CLASS: <_DelayedChangeActiveImageGo>d__116
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIText                <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000BA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000BB  System.Void System.IDisposable.Dispose()
  RVA=0x0B087BB0  token=0x60000BC  System.Boolean MoveNext()
  RVA=0x0B087DD8  token=0x60000BE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: AnimationState
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIConst.AnimationStateStop  // const
  public    static  Beyond.UI.UIConst.AnimationStateIn  // const
  public    static  Beyond.UI.UIConst.AnimationStateLoop  // const
  public    static  Beyond.UI.UIConst.AnimationStateOut  // const
  public    static  Beyond.UI.UIConst.AnimationStateInEasing  // const
  public    static  Beyond.UI.UIConst.AnimationStateOthers  // const
METHODS:
END_CLASS

CLASS: CommonUISegment
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIConst.CommonUISegmentInteractiveEntity  // const
  public    static  Beyond.UI.UIConst.CommonUISegmentLevelScriptModule  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x48
FIELDS:
  public            Beyond.UI.UIModelLoader         <>4__this  // 0x10
  public            System.Int32                    requestId  // 0x18
  public            Beyond.Resource.FAssetProxyHandlehandle  // 0x1c
  public            System.String                   modelPath  // 0x30
  public            UnityEngine.Transform           parent  // 0x38
  public            System.Action<UnityEngine.GameObject>callback  // 0x40
METHODS:
  RVA=0x041E1670  token=0x60000D0  System.Void .ctor()
  RVA=0x0B087770  token=0x60000D1  System.Void <LoadModelAsync>b__0(Beyond.Resource.FAssetProxyUntrackedHandle proxy)
END_CLASS

CLASS: MeunItemInfo
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x58
FIELDS:
  public            System.String                   itemName  // 0x10
  public            Beyond.UI.UIButton              button  // 0x18
  public            Beyond.UI.UIToggle              toggle  // 0x20
  public            System.Boolean                  showIcon  // 0x28
  public            UnityEngine.Sprite              sprite  // 0x30
  public            Beyond.UI.UIText                text  // 0x38
  public            System.String                   textId  // 0x40
  public            System.String                   textIdForToggleOff  // 0x48
  public            System.Int32                    priority  // 0x50
PROPERTIES:
  isForceValid  get=0x0B0870E4
METHODS:
  RVA=0x0B086FC8  token=0x60000FE  System.Boolean IsValid()
  RVA=0x0B086E98  token=0x60000FF  System.String GetText()
  RVA=0x0B086D88  token=0x6000100  UnityEngine.Sprite GetSprite()
  RVA=0x0B086B04  token=0x6000101  System.Void Execute()
  RVA=0x0B086C80  token=0x6000102  System.String GetItemName()
  RVA=0x04D8E3B0  token=0x6000103  System.Void .ctor()
END_CLASS

CLASS: OverrideValidState
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CustomUIStyle.OverrideValidStateNone  // const
  public    static  Beyond.UI.CustomUIStyle.OverrideValidStateForceValid  // const
  public    static  Beyond.UI.CustomUIStyle.OverrideValidStateForceNotValid  // const
METHODS:
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.FullScreenSceneBlurMarker.StateOnEnable  // const
  public    static  Beyond.UI.FullScreenSceneBlurMarker.StateOnDisable  // const
  public    static  Beyond.UI.FullScreenSceneBlurMarker.StateOnDestroy  // const
METHODS:
END_CLASS

CLASS: Snap
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x20
FIELDS:
  public            System.Boolean                  enable  // 0x10
  public            System.Single                   velocityThreshold  // 0x14
  public            System.Single                   duration  // 0x18
  public            Beyond.Interpolator.EEaseType   easing  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x600016B  System.Void .ctor()
END_CLASS

CLASS: AutoScrollState
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x30
FIELDS:
  public            System.Boolean                  enable  // 0x10
  public            System.Boolean                  elastic  // 0x11
  public            System.Single                   duration  // 0x14
  public            Beyond.Interpolator.EasingFunctioneasingFunction  // 0x18
  public            System.Single                   startTime  // 0x20
  public            System.Single                   endPosition  // 0x24
  public            System.Action                   onComplete  // 0x28
METHODS:
  RVA=0x0B0B619C  token=0x600016C  System.Void Reset()
  RVA=0x0B0B613C  token=0x600016D  System.Void Complete()
  RVA=0x041E1670  token=0x600016E  System.Void .ctor()
END_CLASS

CLASS: MovementDirection
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UILoopScrollRect.MovementDirectionLeft  // const
  public    static  Beyond.UI.UILoopScrollRect.MovementDirectionRight  // const
  public    static  Beyond.UI.UILoopScrollRect.MovementDirectionUp  // const
  public    static  Beyond.UI.UILoopScrollRect.MovementDirectionDown  // const
METHODS:
END_CLASS

CLASS: Cell
TYPE:  class
TOKEN: 0x200003F
SIZE:  0x38
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x20
  private           System.Int32                    <index>k__BackingField  // 0x28
  private           System.Int32                    <realIndex>k__BackingField  // 0x2c
  private           System.Single                   <position>k__BackingField  // 0x30
PROPERTIES:
  index  get=0x011F2A90  set=0x011F2AA0
  realIndex  get=0x04D86710  set=0x04D86720
  position  get=0x04D86E20  set=0x04D86E30
  isVisible  get=0x0B0B63C0
METHODS:
  RVA=0x0B0B62C8  token=0x6000198  System.Void SetVisible(System.Boolean active)
  RVA=0x0B0B6328  token=0x6000199  System.Void UpdatePosition(System.String clipName, System.Single position)
  RVA=0x04DA7B20  token=0x600019A  System.Void .ctor()
END_CLASS

CLASS: <_AutoAnimationInFinish>d__61
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.LuaPanel              <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001C0  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001C1  System.Void System.IDisposable.Dispose()
  RVA=0x03545A40  token=0x60001C2  System.Boolean MoveNext()
  RVA=0x0B0B9014  token=0x60001C4  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ShapeType
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.NonDrawingGraphic.ShapeTypeRectangle  // const
  public    static  Beyond.UI.NonDrawingGraphic.ShapeTypeCircle  // const
METHODS:
END_CLASS

CLASS: <>c__19`1
TYPE:  sealed class
TOKEN: 0x2000047
FIELDS:
  public    static readonly Beyond.UI.NotchAdapter.<>c__19<T><>9  // static @ 0x0
  public    static  System.Predicate<T>             <>9__19_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001EE  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001EF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001F0  System.Boolean <_RemoveNullRef>b__19_0(T x)
END_CLASS

CLASS: ParallaxConfig
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x28
FIELDS:
  public            UnityEngine.RectTransform       target  // 0x10
  public            System.Single                   ratio  // 0x18
  public            UnityEngine.Vector2             oriPosition  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60001F7  System.Void .ctor()
END_CLASS

CLASS: ICustomViewType
TYPE:  interface
TOKEN: 0x200004C
FIELDS:
PROPERTIES:
  viewType  get=-1  // abstract
METHODS:
END_CLASS

CLASS: ICustomSpacing
TYPE:  interface
TOKEN: 0x200004D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000207  System.Single GetCustomSpacing()
END_CLASS

CLASS: IVirtualView
TYPE:  interface
TOKEN: 0x200004E
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000208  System.Void AttachView(UnityEngine.GameObject view)
  RVA=-1  // abstract  token=0x6000209  System.Void DetachView()
  RVA=-1  // abstract  token=0x600020A  UnityEngine.GameObject GetAttachedView()
  RVA=-1  // abstract  token=0x600020B  System.Int32 GetViewID()
  RVA=-1  // abstract  token=0x600020C  UnityEngine.GameObject GetPrefab()
  RVA=-1  // abstract  token=0x600020D  System.Single GetPreferSize()
END_CLASS

CLASS: VirtualView
TYPE:  abstract class
TOKEN: 0x200004F
SIZE:  0x18
IMPLEMENTS: IVirtualView Beyond.IHotfixable
FIELDS:
  protected         UnityEngine.GameObject          m_gameObj  // 0x10
PROPERTIES:
  isAttached  get=0x0B0C1200
METHODS:
  RVA=0x0B0C1048  token=0x600020F  System.Void AttachView(UnityEngine.GameObject gameObj)
  RVA=0x0B0C10F4  token=0x6000210  System.Void DetachView()
  RVA=0x0B0C1158  token=0x6000211  UnityEngine.GameObject GetAttachedView()
  RVA=0x0B0C11A8  token=0x6000212  System.Int32 GetViewID()
  RVA=-1  // abstract  token=0x6000213  System.Void OnViewAttached()
  RVA=-1  // abstract  token=0x6000214  System.Void OnViewDetached()
  RVA=-1  // abstract  token=0x6000215  UnityEngine.GameObject GetPrefab()
  RVA=-1  // abstract  token=0x6000216  System.Single GetPreferSize()
  RVA=0x041E1670  token=0x6000217  System.Void .ctor()
END_CLASS

CLASS: SimpleAdapter
TYPE:  class
TOKEN: 0x2000051
EXTENDS: Beyond.UI.UIRecycleLayoutAdapter
FIELDS:
  private           Beyond.UI.UIRecycleLayoutSimpleHolder<TVirtualView>m_holder  // 0x0
  private           System.Collections.Generic.List<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView>m_virtualViews  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000227  System.Void .ctor(Beyond.UI.UIRecycleLayoutSimpleHolder<TVirtualView> holder)
  RVA=-1  // not resolved  token=0x6000228  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild()
  RVA=-1  // not resolved  token=0x6000229  System.Void NotifyRebuild()
  RVA=-1  // not resolved  token=0x600022A  System.Void InsertView(System.Int32 index, TVirtualView virtualView)
  RVA=-1  // not resolved  token=0x600022B  System.Void AppendView(TVirtualView virtualView)
  RVA=-1  // not resolved  token=0x600022C  System.Void AddView(TVirtualView virtualView)
  RVA=-1  // not resolved  token=0x600022D  System.Void RemoveView(System.Int32 index)
  RVA=-1  // not resolved  token=0x600022E  System.Void RemoveAllViews(System.Boolean dontRebuild)
  RVA=-1  // not resolved  token=0x600022F  System.Int32 GetCount()
  RVA=-1  // not resolved  token=0x6000230  TVirtualView GetVirtualView(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000231  UnityEngine.GameObject GetView(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000232  System.Void NotifySizeChanged(TVirtualView virtualView)
  RVA=-1  // not resolved  token=0x6000233  System.Void NotifyAllSizeChanged()
  RVA=-1  // not resolved  token=0x6000234  System.Void NotifySizeChanged(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000235  System.Void OnInit()
  RVA=-1  // not resolved  token=0x6000236  System.Void OnDisposed()
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000052
FIELDS:
  public            Beyond.UI.UIRecycleLayoutSimpleHolder<TVirtualView><>4__this  // 0x0
  public            System.Int32                    naviIndex  // 0x0
  public            System.Boolean                  isBefore  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000237  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000238  System.Void <_WrapNavigation>b__0()
END_CLASS

CLASS: Direction
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIRecycleLayoutGroup.DirectionVertical  // const
  public    static  Beyond.UI.UIRecycleLayoutGroup.DirectionHorizontal  // const
METHODS:
END_CLASS

CLASS: AdaptMode
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIRecycleLayoutGroup.AdaptModeNormal  // const
  public    static  Beyond.UI.UIRecycleLayoutGroup.AdaptModeMultiPrefab  // const
METHODS:
END_CLASS

CLASS: IViewHandler
TYPE:  interface
TOKEN: 0x2000056
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600025F  Beyond.UI.UIRecycleLayoutAdapter.IVirtualView GetView(System.Int32 index)
  RVA=-1  // abstract  token=0x6000260  System.Int32 GetViewCount()
  RVA=-1  // abstract  token=0x6000261  System.Boolean InsertView(System.Int32 index, Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=-1  // abstract  token=0x6000262  System.Boolean AddView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=-1  // abstract  token=0x6000263  System.Void NotifyViewSizeChanged(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=-1  // abstract  token=0x6000264  System.Void NotifyAllViewSizeChanged()
  RVA=-1  // abstract  token=0x6000265  System.Void NotifyRebuild()
  RVA=-1  // abstract  token=0x6000266  System.Boolean RemoveView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=-1  // abstract  token=0x6000267  System.Single GetElementPosByIndex(System.Int32 index)
  RVA=-1  // abstract  token=0x6000268  UnityEngine.Bounds GetElementBoundsByIndex(System.Int32 index)
  RVA=-1  // abstract  token=0x6000269  Beyond.UI.UIRecycleLayoutGroup.Direction GetDirection()
  RVA=-1  // abstract  token=0x600026A  System.Single GetSizeOnAxis()
  RVA=-1  // abstract  token=0x600026B  System.Single GetViewportSizeOnAxis()
  RVA=-1  // abstract  token=0x600026C  System.Int32 GetElementIndex(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
END_CLASS

CLASS: LayoutMeta
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x20
FIELDS:
  public            System.Single                   pos  // 0x10
  public            System.Single                   size  // 0x14
  public            System.Int32                    index  // 0x18
  public            System.Single                   curTotalSize  // 0x1c
METHODS:
END_CLASS

CLASS: ViewPool
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x38
FIELDS:
  private           UnityEngine.GameObject          m_prefab  // 0x10
  private           UnityEngine.Transform           m_container  // 0x18
  private           System.Collections.Generic.List<UnityEngine.GameObject>m_activeObjs  // 0x20
  private           System.Collections.Generic.List<UnityEngine.GameObject>m_pooledObjs  // 0x28
  private           System.Int32                    <viewType>k__BackingField  // 0x30
PROPERTIES:
  viewType  get=0x04D864F0  set=0x04D86740
METHODS:
  RVA=0x0B0C0F58  token=0x600026F  System.Void .ctor(System.Int32 viewType, UnityEngine.GameObject prefab, UnityEngine.Transform container)
  RVA=0x0B0C0AE0  token=0x6000270  UnityEngine.GameObject Alloc(System.Boolean& isNewlyCreated)
  RVA=0x0B0C0DAC  token=0x6000271  System.Boolean Recycle(UnityEngine.GameObject obj)
  RVA=0x0B0C0C70  token=0x6000272  System.Void RecycleAll()
END_CLASS

CLASS: ViewMgr
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x28
IMPLEMENTS: Beyond.IHotfixable IViewHandler
FIELDS:
  private           Beyond.UI.UIRecycleLayoutGroup  m_closure  // 0x10
  private           Beyond.ListDict<System.Int32,Beyond.UI.UIRecycleLayoutGroup.ViewPool>m_viewPools  // 0x18
  private           System.Collections.Generic.List<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView>m_views  // 0x20
METHODS:
  RVA=0x0B0C0A08  token=0x6000273  System.Void .ctor(Beyond.UI.UIRecycleLayoutGroup closure)
  RVA=0x0B0C0528  token=0x6000274  System.Void RebuildAll(Beyond.UI.UIRecycleLayoutAdapter adapter)
  RVA=0x0B0C032C  token=0x6000275  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> GetViews()
  RVA=0x0B0BFEE0  token=0x6000276  System.Void DetachView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view, UnityEngine.GameObject curView)
  RVA=0x0B0BFE18  token=0x6000277  System.Void AttachView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C020C  token=0x6000278  Beyond.UI.UIRecycleLayoutAdapter.IVirtualView GetView(System.Int32 index)
  RVA=0x0B0C0198  token=0x6000279  System.Int32 GetViewCount()
  RVA=0x0B0C037C  token=0x600027A  System.Boolean InsertView(System.Int32 index, Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0BFD7C  token=0x600027B  System.Boolean AddView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C077C  token=0x600027C  System.Boolean RemoveView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C04B8  token=0x600027D  System.Void NotifyViewSizeChanged(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C0404  token=0x600027E  System.Void NotifyAllViewSizeChanged()
  RVA=0x0B0C0460  token=0x600027F  System.Void NotifyRebuild()
  RVA=0x0B0C00D4  token=0x6000280  System.Single GetElementPosByIndex(System.Int32 index)
  RVA=0x0B0BFFD0  token=0x6000281  UnityEngine.Bounds GetElementBoundsByIndex(System.Int32 index)
  RVA=0x0B0BFF78  token=0x6000282  Beyond.UI.UIRecycleLayoutGroup.Direction GetDirection()
  RVA=0x0B0C013C  token=0x6000283  System.Single GetSizeOnAxis()
  RVA=0x0B0C0294  token=0x6000284  System.Single GetViewportSizeOnAxis()
  RVA=0x0B0C0068  token=0x6000285  System.Int32 GetElementIndex(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C07F0  token=0x6000286  Beyond.UI.UIRecycleLayoutGroup.ViewPool _EnsureViewPool(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0C0974  token=0x6000287  System.Void _NotifyLayoutChanged(System.Int32 fromIndex)
END_CLASS

CLASS: Padding
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x20
FIELDS:
  public            System.Int32                    top  // 0x10
  public            System.Int32                    left  // 0x14
  public            System.Int32                    bottom  // 0x18
  public            System.Int32                    right  // 0x1c
METHODS:
END_CLASS

CLASS: EmptyAdapter
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x18
EXTENDS: Beyond.UI.UIRecycleLayoutAdapter
FIELDS:
METHODS:
  RVA=0x0B0B6418  token=0x6000288  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild()
  RVA=0x0B0B6464  token=0x6000289  System.Void OnInit()
  RVA=0x041E1670  token=0x600028A  System.Void .ctor()
  RVA=0x0B0B64A8  token=0x600028B  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> <>iFixBaseProxy_GenerateViewsForRebuild()
END_CLASS

CLASS: MultiVirtualView
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x30
EXTENDS: VirtualView
FIELDS:
  private           Beyond.UI.UIRecycleMultiHolder  m_closure  // 0x18
  public            System.Single                   preferSize  // 0x20
  public            System.String                   prefabKey  // 0x28
METHODS:
  RVA=0x0B0B6BFC  token=0x6000294  System.Int32 GetIndex()
  RVA=0x0B0B6ED4  token=0x6000295  System.Void .ctor(System.Single initSize, System.String key, Beyond.UI.UIRecycleMultiHolder closure)
  RVA=0x0B0B6CC8  token=0x6000296  UnityEngine.GameObject GetPrefab()
  RVA=0x0B0B6D4C  token=0x6000297  System.Single GetPreferSize()
  RVA=0x0B0B6E38  token=0x6000298  System.Void OnViewDetached()
  RVA=0x0B0B6D9C  token=0x6000299  System.Void OnViewAttached()
END_CLASS

CLASS: Config
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x20
FIELDS:
  public            System.String                   key  // 0x10
  public            UnityEngine.GameObject          cellTemplate  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600029A  System.Void .ctor()
END_CLASS

CLASS: SimpleVirtualView
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x28
EXTENDS: VirtualView
FIELDS:
  private           Beyond.UI.UIRecycleSingleHolder m_holder  // 0x18
  public            System.Single                   preferSize  // 0x20
METHODS:
  RVA=0x0B0B7FA4  token=0x60002A3  System.Int32 GetIndex()
  RVA=0x0B0B8278  token=0x60002A4  System.Void .ctor(System.Single initSize, Beyond.UI.UIRecycleSingleHolder holder)
  RVA=0x0B0B805C  token=0x60002A5  UnityEngine.GameObject GetPrefab()
  RVA=0x0B0B80B8  token=0x60002A6  System.Single GetPreferSize()
  RVA=0x0B0B81C0  token=0x60002A7  System.Void OnViewDetached()
  RVA=0x0B0B8108  token=0x60002A8  System.Void OnViewAttached()
END_CLASS

CLASS: CellInfo
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x30
FIELDS:
  public            System.Int32                    index  // 0x10
  public            UnityEngine.GameObject          obj  // 0x18
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x20
  public            UnityEngine.Coroutine           cor  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60002BD  System.Void .ctor()
END_CLASS

CLASS: <_ShowCell>d__23
TYPE:  sealed class
TOKEN: 0x2000064
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Int32                    delayIndex  // 0x20
  public            Beyond.UI.ToastList.CellInfo    cell  // 0x28
  public            Beyond.UI.ToastList             <>4__this  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002BE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002BF  System.Void System.IDisposable.Dispose()
  RVA=0x0B0B9060  token=0x60002C0  System.Boolean MoveNext()
  RVA=0x0B0B92C0  token=0x60002C2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ClipHolder
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x20
FIELDS:
  public            UnityEngine.AnimationClip       clip  // 0x10
  public            System.String                   alias  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002F4  System.Void .ctor()
END_CLASS

CLASS: AnimationRuntime
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x30
FIELDS:
  public            Beyond.UI.UIAnimationTween.Optionsoption  // 0x10
  public            UnityEngine.AnimationState      state  // 0x20
  public            System.Boolean                  isPlaying  // 0x28
METHODS:
  RVA=0x0384AA80  token=0x600032E  System.Single GetClipLength()
  RVA=0x041E1670  token=0x600032F  System.Void .ctor()
END_CLASS

CLASS: FOptions
TYPE:  sealed struct
TOKEN: 0x200006C
SIZE:  0x14
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  public            DG.Tweening.Ease                animEase  // 0x10
METHODS:
  RVA=0x03698310  token=0x6000330  Beyond.UI.UIAnimationWrapper.FOptions GetDefault()
END_CLASS

CLASS: CachedSampleClipInfo
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIAnimationWrapper.AnimationRuntimeruntime  // 0x10
  public            System.Single                   position  // 0x18
  public            System.Boolean                  isDirty  // 0x1c
METHODS:
  RVA=0x05393540  token=0x6000331  System.Void .ctor(System.Boolean isDirty)
END_CLASS

CLASS: <>c__DisplayClass37_0
TYPE:  sealed class
TOKEN: 0x200006E
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    <>4__this  // 0x10
  public            DG.Tweening.TweenCallback       callback  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000332  System.Void .ctor()
  RVA=0x035447D0  token=0x6000333  System.Void <PlayOutAnimation>b__0()
END_CLASS

CLASS: <>c__DisplayClass46_0
TYPE:  sealed class
TOKEN: 0x200006F
SIZE:  0x28
FIELDS:
  public            DG.Tweening.TweenCallback       callback  // 0x10
  public            Beyond.UI.UIAnimationWrapper    <>4__this  // 0x18
  public            System.Boolean                  autoPlayLoop  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000334  System.Void .ctor()
  RVA=0x03543880  token=0x6000335  System.Void <PlayInAnimation>b__0()
END_CLASS

CLASS: <>c__DisplayClass53_0
TYPE:  sealed class
TOKEN: 0x2000070
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    <>4__this  // 0x10
  public            System.Boolean                  autoPlayLoop  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000336  System.Void .ctor()
  RVA=0x03542590  token=0x6000337  System.Void <_OnInAnimationFinished>b__0()
END_CLASS

CLASS: <>c__DisplayClass74_0
TYPE:  sealed class
TOKEN: 0x2000071
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    <>4__this  // 0x10
  public            DG.Tweening.TweenCallback       onComplete  // 0x18
  public            Beyond.UI.UIAnimationTween      animTween  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000338  System.Void .ctor()
  RVA=0x03545240  token=0x6000339  System.Void <PlayWithTween>b__0()
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x1C
FIELDS:
  public            System.Boolean                  isInverse  // 0x10
  public            System.Single                   startPosition  // 0x14
  public            System.Single                   speed  // 0x18
METHODS:
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x2000074
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIAnimationTween      <>4__this  // 0x10
  public            Beyond.UI.UIAnimationWrapper    wrapper  // 0x18
  public            System.String                   name  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000343  System.Void .ctor()
  RVA=0x032EBEE0  token=0x6000344  System.Void <.ctor>b__0(System.Single value)
  RVA=0x032EBF20  token=0x6000345  System.Void <.ctor>b__2(System.Single value)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.UI.UIAnimationTween.<>c  <>9  // static @ 0x0
  public    static  DG.Tweening.Core.DOGetter<System.Single><>9__14_1  // static @ 0x8
METHODS:
  RVA=0x04D431B0  token=0x6000346  System.Void .cctor()
  RVA=0x041E1670  token=0x6000347  System.Void .ctor()
  RVA=0x04D879D0  token=0x6000348  System.Single <.ctor>b__14_1()
END_CLASS

CLASS: TweenHandler
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x30
IMPLEMENTS: ITweenHandler Beyond.IHotfixable
FIELDS:
  private           Beyond.UI.UIAnimationSwitchTweenm_closure  // 0x10
  private           System.Boolean                  m_isIn  // 0x18
  private           System.Boolean                  m_pendingClear  // 0x19
  private           DG.Tweening.TweenCallback       m_pendingCallback  // 0x20
  private           System.Boolean                  m_isComplete  // 0x28
METHODS:
  RVA=0x0B0C15EC  token=0x6000353  System.Void .ctor(Beyond.UI.UIAnimationSwitchTween closure, System.Boolean isIn)
  RVA=0x0B0C1328  token=0x6000354  System.Boolean IsPlaying()
  RVA=0x0B0C13F0  token=0x6000355  System.Void KillIfNecessary()
  RVA=0x0B0C14B0  token=0x6000356  Beyond.UI.UISwitchTween.ITweenHandler OnComplete(DG.Tweening.TweenCallback callback)
  RVA=0x0B0C152C  token=0x6000357  Beyond.UI.UISwitchTween.ITweenHandler SetAutoKill(System.Boolean autoKill)
  RVA=0x0B0C1584  token=0x6000358  System.Void _OnComplete()
END_CLASS

CLASS: Builder
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    animWrapper  // 0x10
  public            System.Boolean                  dontDisableGameObject  // 0x18
METHODS:
  RVA=0x0B0C1284  token=0x6000359  Beyond.UI.UIAnimationSwitchTween Build()
END_CLASS

CLASS: BigRectRecoverState
TYPE:  sealed class
TOKEN: 0x200007E
SIZE:  0x50
FIELDS:
  public            UnityEngine.Vector2             anchoredPosition  // 0x10
  public            UnityEngine.Vector2             anchorMin  // 0x18
  public            UnityEngine.Vector2             anchorMax  // 0x20
  public            UnityEngine.Vector2             pivot  // 0x28
  public            UnityEngine.Vector3             localScale  // 0x30
  public            System.Single                   zoomValue  // 0x3c
  public            UnityEngine.Vector2             anchoredPosOffset  // 0x40
  public            System.Single                   horizontalOffset  // 0x48
  public            System.Single                   verticalOffset  // 0x4c
METHODS:
  RVA=0x041E1670  token=0x60003E4  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass82_0
TYPE:  sealed class
TOKEN: 0x200007F
SIZE:  0x18
FIELDS:
  public            DG.Tweening.TweenCallback       onComplete  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003E5  System.Void .ctor()
  RVA=0x04D30F10  token=0x60003E6  System.Void <FocusNode>b__0()
END_CLASS

CLASS: <_DoClickAnimation>d__30
TYPE:  sealed class
TOKEN: 0x2000084
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIButton              <>4__this  // 0x20
  private           System.Single                   <currentTime>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000458  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000459  System.Void System.IDisposable.Dispose()
  RVA=0x0B0C16E8  token=0x600045A  System.Boolean MoveNext()
  RVA=0x0B0C1828  token=0x600045C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_OnPress>d__59
TYPE:  sealed class
TOKEN: 0x2000085
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIButton              <>4__this  // 0x20
  public            UnityEngine.EventSystems.PointerEventDataeventData  // 0x28
  private           System.Single                   <finishTime>5__2  // 0x30
  private           UnityEngine.Vector2             <pressPos>5__3  // 0x34
  private           System.Single                   <_longPressMaxDragDistSq>5__4  // 0x3c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600045E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600045F  System.Void System.IDisposable.Dispose()
  RVA=0x042EE3D0  token=0x6000460  System.Boolean MoveNext()
  RVA=0x0B0C1874  token=0x6000462  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FWorldCanvasParams
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x30
FIELDS:
  public            System.Single                   screenRatio  // 0x10
  public            UnityEngine.Vector2             screenSize  // 0x14
  public            UnityEngine.Vector2             uiRootSize  // 0x1c
  public            UnityEngine.Vector3             uiRootScale  // 0x24
METHODS:
END_CLASS

CLASS: FScreenCanvasParams
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x24
FIELDS:
  public            System.Single                   screenRatio  // 0x10
  public            UnityEngine.Vector2             screenSize  // 0x14
  public            Beyond.UI.CanvasMatchMode       matchMode  // 0x1c
  public            System.Single                   matchWidthOrHeight  // 0x20
METHODS:
END_CLASS

CLASS: CommonParams
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x30
FIELDS:
  public            System.Single                   alpha  // 0x10
  public            System.Single                   duration  // 0x14
  public            System.Boolean                  ignoreTimeScale  // 0x18
  public            UnityEngine.Color               targetColor  // 0x1c
  public            System.Boolean                  useAlpha  // 0x2c
  public            System.Boolean                  useRGB  // 0x2d
  public            System.Boolean                  isStaticColorOpt  // 0x2e
METHODS:
END_CLASS

CLASS: GraphicOpt
TYPE:  sealed class
TOKEN: 0x200008C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04640420  token=0x600048B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06229908  token=0x600048C  System.Void Invoke(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x0B0C6CCC  token=0x600048D  System.IAsyncResult BeginInvoke(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600048E  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DynEle
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x28
FIELDS:
  public            System.String                   groupId  // 0x10
  public            UnityEngine.UI.Graphic          graphic  // 0x18
  public            System.Boolean                  useStaticColor  // 0x20
METHODS:
END_CLASS

CLASS: LayoutType
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UICustomLayoutElement.LayoutTypeNone  // const
  public    static  Beyond.UI.UICustomLayoutElement.LayoutTypeLeftToRight  // const
METHODS:
END_CLASS

CLASS: LayoutData
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x28
FIELDS:
  public            System.Int32                    verNum  // 0x10
  public            System.Single                   scale  // 0x14
  public            System.Single                   alpha  // 0x18
  public            UnityEngine.Vector2             position  // 0x1c
  public            System.Int32                    layoutType  // 0x24
METHODS:
  RVA=0x041E1670  token=0x60004BF  System.Void .ctor()
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIDialogText.UpdateModeAuto  // const
  public    static  Beyond.UI.UIDialogText.UpdateModeManual  // const
METHODS:
END_CLASS

CLASS: <_ContinueDragOnDisable>d__43
TYPE:  sealed class
TOKEN: 0x200009D
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIDragItem            <>4__this  // 0x20
  private           UnityEngine.EventSystems.PointerEventData<eData>5__2  // 0x28
  private           Beyond.PoolCore.PooledList<UnityEngine.EventSystems.RaycastResult><raycastResults>5__3  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600053D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600053E  System.Void System.IDisposable.Dispose()
  RVA=0x02E94D20  token=0x600053F  System.Boolean MoveNext()
  RVA=0x02E96850  token=0x6000541  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass44_0
TYPE:  sealed class
TOKEN: 0x200009F
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIDropdown            <>4__this  // 0x10
  public            System.Boolean                  activeList  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000571  System.Void .ctor()
  RVA=0x0B0C6D88  token=0x6000572  System.Void <_ToggleOptions>b__0()
END_CLASS

CLASS: <>c__DisplayClass48_0
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x20
FIELDS:
  public            System.Int32                    index  // 0x10
  public            Beyond.UI.UIDropdown            <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000573  System.Void .ctor()
  RVA=0x03F01B90  token=0x6000574  System.Void <Refresh>b__0(UnityEngine.EventSystems.PointerEventData evt)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x20
FIELDS:
  public            System.Single                   startSize  // 0x10
  public            Beyond.UI.UIFoldoutComponent    <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60005A9  System.Void .ctor()
  RVA=0x04D85F70  token=0x60005AA  System.Single <OnFoldOut>b__0()
  RVA=0x0B0C6D54  token=0x60005AB  System.Void <OnFoldOut>b__1(System.Single value)
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x20
FIELDS:
  public            System.Single                   startSize  // 0x10
  public            Beyond.UI.UIFoldoutComponent    <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60005AC  System.Void .ctor()
  RVA=0x04D85F70  token=0x60005AD  System.Single <OnFoldIn>b__0()
  RVA=0x0B0C6D54  token=0x60005AE  System.Void <OnFoldIn>b__1(System.Single value)
END_CLASS

CLASS: WrapMode
TYPE:  sealed struct
TOKEN: 0x20000AD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIGridLayoutNaviWrapper.WrapModeNaviGroup  // const
  public    static  Beyond.UI.UIGridLayoutNaviWrapper.WrapModeScrollRect  // const
  public    static  Beyond.UI.UIGridLayoutNaviWrapper.WrapModeScrollList  // const
  public    static  Beyond.UI.UIGridLayoutNaviWrapper.WrapModeLayoutNaviWrapper  // const
METHODS:
END_CLASS

CLASS: Param
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x20
FIELDS:
  public            System.Single                   maxAngle  // 0x10
  public            UnityEngine.AnimationCurve      valueCurve  // 0x18
METHODS:
  RVA=0x04D7F870  token=0x6000631  System.Void .ctor()
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x20000B9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIInertiaViewPager.StateIDLE  // const
  public    static  Beyond.UI.UIInertiaViewPager.StateDRAGING  // const
  public    static  Beyond.UI.UIInertiaViewPager.StateINERTIA  // const
  public    static  Beyond.UI.UIInertiaViewPager.StateALIGNING  // const
METHODS:
END_CLASS

CLASS: IScrollHandler
TYPE:  interface
TOKEN: 0x20000BA
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
PROPERTIES:
  velocity  get=-1  // abstract
  isDragging  get=-1  // abstract
  scrollProgress  get=-1  // abstract  set=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60006AC  System.Void OnClear()
  RVA=-1  // abstract  token=0x60006B1  System.Void StopMoving()
END_CLASS

CLASS: CustomScrollHandler
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x20
IMPLEMENTS: IScrollHandler Beyond.IHotfixable
FIELDS:
  private           System.Boolean                  m_isScrolling  // 0x10
  private           Beyond.UI.UIScrollRect          m_scrollRect  // 0x18
PROPERTIES:
  velocity  get=0x0B0D93E8
  scrollProgress  get=0x0B0D935C  set=0x0B0D9468
  isDragging  get=0x0B0D92F4
METHODS:
  RVA=0x0B0D91C4  token=0x60006B2  System.Void .ctor(Beyond.UI.UIScrollRect scrollRect)
  RVA=0x0B0D8F50  token=0x60006B3  System.Void OnClear()
  RVA=0x0B0D90A4  token=0x60006B8  System.Void StopMoving()
  RVA=0x0B0D9104  token=0x60006B9  System.Void _OnScrollBegin(UnityEngine.Vector2 delta)
  RVA=0x0B0D9164  token=0x60006BA  System.Void _OnScrollEnd(UnityEngine.Vector2 delta)
END_CLASS

CLASS: EmptyScrollHandler
TYPE:  sealed struct
TOKEN: 0x20000BC
SIZE:  0x11
IMPLEMENTS: IScrollHandler Beyond.IHotfixable
FIELDS:
PROPERTIES:
  velocity  get=0x0B0D985C
  isDragging  get=0x0B0D97C4
  scrollProgress  get=0x0B0D9810  set=0x0B0D98A8
METHODS:
  RVA=0x0B0D9780  token=0x60006BF  System.Void StopMoving()
  RVA=0x0B0D973C  token=0x60006C0  System.Void OnClear()
END_CLASS

CLASS: FlingToNext
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x1C
FIELDS:
  public            System.Boolean                  enable  // 0x10
  public            System.Single                   minSpd  // 0x14
  public            System.Single                   maxSpd  // 0x18
METHODS:
END_CLASS

CLASS: ScrollOptions
TYPE:  sealed struct
TOKEN: 0x20000BE
SIZE:  0x1C
FIELDS:
  public            Beyond.UI.UIInertiaViewPager.FlingToNextflingToNext  // 0x10
METHODS:
END_CLASS

CLASS: DragContext
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x18
FIELDS:
  public            System.Single                   startPos  // 0x10
METHODS:
  RVA=0x0B0D94FC  token=0x60006C1  System.Void BeginDrag(Beyond.UI.UIInertiaViewPager pager)
  RVA=0x0B0D956C  token=0x60006C2  Beyond.UI.UIInertiaViewPager.State EndDrag(Beyond.UI.UIInertiaViewPager pager)
  RVA=0x0B0D95D8  token=0x60006C3  System.Boolean _TryFlingToNext(Beyond.UI.UIInertiaViewPager pager)
  RVA=0x041E1670  token=0x60006C4  System.Void .ctor()
END_CLASS

CLASS: BlockerDir
TYPE:  sealed struct
TOKEN: 0x20000C0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIInertiaViewPager.BlockerDirUPWARD  // const
  public    static  Beyond.UI.UIInertiaViewPager.BlockerDirDOWNNWARD  // const
  public    static  Beyond.UI.UIInertiaViewPager.BlockerDirBOTH  // const
METHODS:
END_CLASS

CLASS: InertiaBlocker
TYPE:  sealed struct
TOKEN: 0x20000C1
SIZE:  0x18
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
  public            Beyond.UI.UIInertiaViewPager.BlockerDirdir  // 0x10
  public            System.Int32                    index  // 0x14
METHODS:
  RVA=0x0B0D98FC  token=0x60006C5  System.Boolean IsDirMatch(System.Single vec)
END_CLASS

CLASS: BlockerManager
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x20
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
  private   static  System.Single                   INERTIA_BIAS  // const
  private           System.Collections.Generic.List<Beyond.UI.UIInertiaViewPager.InertiaBlocker>m_inertiaBlockers  // 0x10
  private           System.Single                   m_lastIndex  // 0x18
  private           System.Boolean                  <isInertiaing>k__BackingField  // 0x1c
PROPERTIES:
  isInertiaing  get=0x04D86150  set=0x04D86160
METHODS:
  RVA=0x0B0D8CE0  token=0x60006C8  System.Void Init(System.Int32 pageCount, System.Collections.Generic.List<System.Int32> blockFrames)
  RVA=0x0B0D8E78  token=0x60006C9  System.Void StartInertia(System.Single fromIndex)
  RVA=0x0B0D8AB4  token=0x60006CA  System.Boolean HitInertiaBlock(System.Single curIndex, System.Single& preferTo)
  RVA=0x0B0D8ED8  token=0x60006CB  System.Void .ctor()
END_CLASS

CLASS: Timing
TYPE:  sealed struct
TOKEN: 0x20000C4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIInertiaViewPager.ScrollEffectConfig.TimingHALF_VALUE  // const
  public    static  Beyond.UI.UIInertiaViewPager.ScrollEffectConfig.TimingFULL_VALUE  // const
METHODS:
END_CLASS

CLASS: ScrollEffectConfig
TYPE:  sealed struct
TOKEN: 0x20000C3
SIZE:  0x28
FIELDS:
  public            System.Action                   onScrollToItem  // 0x10
  public            System.Action                   onAlignedToItem  // 0x18
  public            System.Single                   minScrollInterval  // 0x20
  public            Beyond.UI.UIInertiaViewPager.ScrollEffectConfig.Timingtiming  // 0x24
METHODS:
  RVA=0x0B0D9984  token=0x60006CC  System.Boolean IsEmpty()
END_CLASS

CLASS: ScrollEffectTrigger
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x40
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
  private   static  System.Single                   DFT_MIN_INTERVAL  // const
  private           System.Boolean                  m_isValid  // 0x10
  private           Beyond.UI.UIInertiaViewPager.ScrollEffectConfigm_config  // 0x18
  private           System.Int32                    m_lastFocusIndex  // 0x30
  private           System.Single                   m_curTime  // 0x34
  private           System.Single                   m_lastScrollTime  // 0x38
  private           System.Boolean                  m_hasScrolled  // 0x3c
METHODS:
  RVA=0x0B0D9C24  token=0x60006CD  System.Void .ctor(Beyond.UI.UIInertiaViewPager.ScrollEffectConfig config)
  RVA=0x0B0D9A9C  token=0x60006CE  System.Void NotifyScrolling(System.Single curIndex, System.Single deltaTime)
  RVA=0x0B0D99E4  token=0x60006CF  System.Void MarkHasScrolled()
  RVA=0x0B0D9A38  token=0x60006D0  System.Void NotifyAlignFinish()
  RVA=0x0B0D9BB4  token=0x60006D1  System.Void Reset(System.Int32 curIndex)
END_CLASS

CLASS: <_KeyboardHeightChange>d__63
TYPE:  sealed class
TOKEN: 0x20000C7
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIInputField          <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000704  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000705  System.Void System.IDisposable.Dispose()
  RVA=0x0B0D9CE4  token=0x6000706  System.Boolean MoveNext()
  RVA=0x0B0D9D3C  token=0x6000708  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: IAction
TYPE:  interface
TOKEN: 0x20000CA
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000747  System.Void DoAction()
END_CLASS

CLASS: <_NextFrameCoroutine>d__6
TYPE:  sealed class
TOKEN: 0x20000CB
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UILayoutDimensionListener<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000748  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000749  System.Void System.IDisposable.Dispose()
  RVA=0x0B0D9D88  token=0x600074A  System.Boolean MoveNext()
  RVA=0x0B0D9DE0  token=0x600074C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass48_0
TYPE:  sealed class
TOKEN: 0x20000CF
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIMultiSelectDropdown <>4__this  // 0x10
  public            System.Boolean                  activeList  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007A5  System.Void .ctor()
  RVA=0x0B0C6D88  token=0x60007A6  System.Void <_ToggleOptions>b__0()
END_CLASS

CLASS: <>c__DisplayClass59_0
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x20
FIELDS:
  public            System.Int32                    index  // 0x10
  public            Beyond.UI.UIMultiSelectDropdown <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007A7  System.Void .ctor()
  RVA=0x0B0E1F2C  token=0x60007A8  System.Void <Refresh>b__0(UnityEngine.EventSystems.PointerEventData evt)
END_CLASS

CLASS: GetCellSizeMode
TYPE:  sealed struct
TOKEN: 0x20000D2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UINonUnifiedScrollList.GetCellSizeModeByGetCellSizeFunc  // const
  public    static  Beyond.UI.UINonUnifiedScrollList.GetCellSizeModeByLayoutGroup  // const
  public    static  Beyond.UI.UINonUnifiedScrollList.GetCellSizeModeGetAfterCellCreated  // const
METHODS:
END_CLASS

CLASS: Cell
TYPE:  class
TOKEN: 0x20000DC
SIZE:  0x28
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.GameObject          realGameObject  // 0x20
METHODS:
  RVA=0x0B0E1ED8  token=0x600084D  System.Void SetSelect(System.Boolean select)
  RVA=0x041E1670  token=0x600084E  System.Void .ctor()
END_CLASS

CLASS: AutoLayoutWhenFewType
TYPE:  sealed struct
TOKEN: 0x20000E0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollList.AutoLayoutWhenFewTypeCenter  // const
  public    static  Beyond.UI.UIScrollList.AutoLayoutWhenFewTypeLeftOrTop  // const
  public    static  Beyond.UI.UIScrollList.AutoLayoutWhenFewTypeRightOrBottom  // const
METHODS:
END_CLASS

CLASS: Cell
TYPE:  class
TOKEN: 0x20000E1
SIZE:  0x40
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x20
  public            Beyond.UI.IUIFoldoutComp        UIFoldoutComp  // 0x28
  public            System.Boolean                  canCache  // 0x30
  public            UnityEngine.GameObject          realGameObject  // 0x38
METHODS:
  RVA=0x04D8D3B0  token=0x60008EB  System.Void .ctor()
END_CLASS

CLASS: MoveTipsType
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollList.MoveTipsTypeNone  // const
  public    static  Beyond.UI.UIScrollList.MoveTipsTypeHigher  // const
  public    static  Beyond.UI.UIScrollList.MoveTipsTypeLower  // const
  public    static  Beyond.UI.UIScrollList.MoveTipsTypeBoth  // const
METHODS:
END_CLASS

CLASS: Padding
TYPE:  sealed struct
TOKEN: 0x20000E3
SIZE:  0x20
FIELDS:
  public            System.Single                   top  // 0x10
  public            System.Single                   left  // 0x14
  public            System.Single                   right  // 0x18
  public            System.Single                   bottom  // 0x1c
METHODS:
END_CLASS

CLASS: ScrollAlignType
TYPE:  sealed struct
TOKEN: 0x20000E4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollList.ScrollAlignTypeCenter  // const
  public    static  Beyond.UI.UIScrollList.ScrollAlignTypeTop  // const
  public    static  Beyond.UI.UIScrollList.ScrollAlignTypeBottom  // const
METHODS:
END_CLASS

CLASS: IScrollListNaviWrapper
TYPE:  interface
TOKEN: 0x20000E5
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60008EC  System.Boolean TryWrapNavigate(UnityEngine.UI.NaviDirection dir)
END_CLASS

CLASS: MoveTipsType
TYPE:  sealed struct
TOKEN: 0x20000E8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollRect.MoveTipsTypeNone  // const
  public    static  Beyond.UI.UIScrollRect.MoveTipsTypeHigher  // const
  public    static  Beyond.UI.UIScrollRect.MoveTipsTypeLower  // const
  public    static  Beyond.UI.UIScrollRect.MoveTipsTypeBoth  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass17_0
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector2             contentPos  // 0x10
  public            Beyond.UI.UIScrollRect          <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000918  System.Void .ctor()
  RVA=0x04D95390  token=0x6000919  UnityEngine.Vector2 <_CustomSetPosOnScroll>b__0()
  RVA=0x0B0EE260  token=0x600091A  System.Void <_CustomSetPosOnScroll>b__1(UnityEngine.Vector2 pos)
END_CLASS

CLASS: ERedDotState
TYPE:  sealed struct
TOKEN: 0x20000EB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotStateDISABLED  // const
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotStateNEW  // const
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotStateNORMAL  // const
METHODS:
END_CLASS

CLASS: ERedDotDir
TYPE:  sealed struct
TOKEN: 0x20000EC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDirUP  // const
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDirRIGHT  // const
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDirDOWN  // const
  public    static  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDirLEFT  // const
METHODS:
END_CLASS

CLASS: RedDotInfo
TYPE:  class
TOKEN: 0x20000ED
SIZE:  0x28
FIELDS:
  public    readonly UnityEngine.GameObject          redDotRoot  // 0x10
  public    readonly UnityEngine.GameObject          redDotNormal  // 0x18
  public    readonly UnityEngine.GameObject          redDotNew  // 0x20
PROPERTIES:
  state  get=0x0B0EE120
  normalPos  get=0x0B0EDF48
  newPos  get=0x0B0EDD70
  normalRect  get=0x0B0EE024
  newRect  get=0x0B0EDE4C
METHODS:
  RVA=0x0B0EDD38  token=0x6000937  System.Void .ctor(UnityEngine.GameObject redDotRoot, UnityEngine.GameObject redDotNormal, UnityEngine.GameObject redDotNew)
END_CLASS

CLASS: ScrollMode
TYPE:  sealed struct
TOKEN: 0x20000EF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollText.ScrollModeMesh  // const
  public    static  Beyond.UI.UIScrollText.ScrollModeAnchoredPosition  // const
METHODS:
END_CLASS

CLASS: ScrollState
TYPE:  sealed struct
TOKEN: 0x20000F0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIScrollText.ScrollStateScrolling  // const
  public    static  Beyond.UI.UIScrollText.ScrollStateIdle  // const
  public    static  Beyond.UI.UIScrollText.ScrollStateFadingOut  // const
  public    static  Beyond.UI.UIScrollText.ScrollStateFadingIn  // const
METHODS:
END_CLASS

CLASS: IAlphaHandler
TYPE:  interface
TOKEN: 0x20000F1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000948  System.Void HandleAlpha(System.Single alpha)
END_CLASS

CLASS: CanvasGroupHandler
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x18
IMPLEMENTS: IAlphaHandler
FIELDS:
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000949  System.Void .ctor(UnityEngine.CanvasGroup canvasGroup)
  RVA=0x0B0ECE0C  token=0x600094A  System.Void HandleAlpha(System.Single alpha)
END_CLASS

CLASS: IScrollHandler
TYPE:  interface
TOKEN: 0x20000F3
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600094B  System.Void ApplyScroll(System.Single scrollOffset)
  RVA=-1  // abstract  token=0x600094C  System.Void ResetScroll()
  RVA=-1  // abstract  token=0x600094D  System.Void UpdateScrollStatus(System.Boolean isScroll)
END_CLASS

CLASS: PositionScrollHandler
TYPE:  class
TOKEN: 0x20000F4
SIZE:  0x18
IMPLEMENTS: IScrollHandler
FIELDS:
  private           Beyond.UI.UIScrollText          m_closure  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600094E  System.Void .ctor(Beyond.UI.UIScrollText scrollText)
  RVA=0x0B0EDB34  token=0x600094F  System.Void ApplyScroll(System.Single scrollOffset)
  RVA=0x0B0EDBE0  token=0x6000950  System.Void ResetScroll()
  RVA=0x0B0EDC54  token=0x6000951  System.Void UpdateScrollStatus(System.Boolean isScroll)
END_CLASS

CLASS: MeshScrollHandler
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x18
IMPLEMENTS: IScrollHandler
FIELDS:
  private           Beyond.UI.UIScrollText          m_closure  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000952  System.Void .ctor(Beyond.UI.UIScrollText uiScrollText)
  RVA=0x0B0ECE78  token=0x6000953  System.Void ApplyScroll(System.Single scrollOffset)
  RVA=0x0B0ECEDC  token=0x6000954  System.Void ResetScroll()
  RVA=0x0B0ECF34  token=0x6000955  System.Void UpdateScrollStatus(System.Boolean isScroll)
  RVA=0x0B0ECFF0  token=0x6000956  System.Void _ApplyScroll(System.Single scrollOffset)
  RVA=0x0B0ED2B4  token=0x6000957  System.Void _UpdateMeshClipping(TMPro.TMP_TextInfo textInfo)
END_CLASS

CLASS: FocusBlockMessageData
TYPE:  sealed struct
TOKEN: 0x20000F7
SIZE:  0x30
FIELDS:
  public            System.Int32                    panelId  // 0x10
  public            System.Boolean                  isGroup  // 0x14
  public            System.Int32                    id  // 0x18
  public            UnityEngine.RectTransform       rectTransform  // 0x20
  public            System.Boolean                  noHighlight  // 0x28
  public            System.Boolean                  useNormalFrame  // 0x29
  public            System.Boolean                  useDarkFrame  // 0x2a
  public            System.Int32                    panelSortingOrder  // 0x2c
METHODS:
END_CLASS

CLASS: <_ApplyDamping>d__46
TYPE:  sealed class
TOKEN: 0x20000F9
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UISemiCircleScrollList<>4__this  // 0x20
  private           System.Single                   <targetOffset>5__2  // 0x28
  private           System.Single                   <elapsed>5__3  // 0x2c
  private           System.Single                   <initialOffset>5__4  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000997  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000998  System.Void System.IDisposable.Dispose()
  RVA=0x0B0EE2A0  token=0x6000999  System.Boolean MoveNext()
  RVA=0x0B0EE5D0  token=0x600099B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: StateInfo
TYPE:  class
TOKEN: 0x20000FE
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Collections.Generic.List<UnityEngine.GameObject>activeObjects  // 0x18
METHODS:
  RVA=0x0B0EE210  token=0x60009C0  System.String ToString()
  RVA=0x035735B0  token=0x60009C1  System.Void .ctor()
  RVA=0x0669AD6C  token=0x60009C2  System.String <>iFixBaseProxy_ToString()
END_CLASS

CLASS: SlicedSpriteParams
TYPE:  sealed struct
TOKEN: 0x2000102
SIZE:  0x34
FIELDS:
  public            System.Int32                    slicedEnabled  // 0x10
  public            UnityEngine.Vector2             inner_PosMin  // 0x14
  public            UnityEngine.Vector2             inner_PosMax  // 0x1c
  public            UnityEngine.Vector2             inner_UVMin  // 0x24
  public            UnityEngine.Vector2             inner_UVMax  // 0x2c
METHODS:
  RVA=0x04DAAF30  token=0x6000A16  System.Void .ctor(System.Int32 slicedEnabled, UnityEngine.Vector2 inner_PosMin, UnityEngine.Vector2 inner_PosMax, UnityEngine.Vector2 inner_UVMin, UnityEngine.Vector2 inner_UVMax)
END_CLASS

CLASS: UIType
TYPE:  sealed struct
TOKEN: 0x2000104
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UISoftMaskable.UIType Default  // const
  public    static  Beyond.UI.UISoftMaskable.UIType Text  // const
  public    static  Beyond.UI.UISoftMaskable.UIType UIVFX  // const
METHODS:
END_CLASS

CLASS: TestLine
TYPE:  sealed struct
TOKEN: 0x2000105
SIZE:  0x118
FIELDS:
  public            UnityEngine.UIVertex            v0  // 0x10
  public            UnityEngine.UIVertex            v1  // 0x7c
  public            UnityEngine.Vector2             p0  // 0xe8
  public            UnityEngine.Vector2             p1  // 0xf0
  public            UnityEngine.Vector2             t0  // 0xf8
  public            UnityEngine.Vector2             t1  // 0x100
  public            UnityEngine.Vector2             dir  // 0x108
  public            UnityEngine.Vector2             normal  // 0x110
METHODS:
END_CLASS

CLASS: RenderType
TYPE:  sealed struct
TOKEN: 0x2000107
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UISortingOrder.RenderTypeEffect  // const
  public    static  Beyond.UI.UISortingOrder.RenderTypeUI  // const
  public    static  Beyond.UI.UISortingOrder.RenderTypeParticle  // const
METHODS:
END_CLASS

CLASS: StateConfig
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x10
  public            System.String                   stateName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000A56  System.Void .ctor()
END_CLASS

CLASS: StyleConfig
TYPE:  class
TOKEN: 0x200010B
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIStyleInputValidTypeMaskinputValidType  // 0x10
  public            System.Collections.Generic.List<Beyond.UI.UIStyleByState.StateConfig>stateConfigs  // 0x18
METHODS:
  RVA=0x044B1860  token=0x6000A57  System.Void .ctor()
END_CLASS

CLASS: ScrollListLayoutInfo
TYPE:  class
TOKEN: 0x2000116
SIZE:  0x28
FIELDS:
  public            UnityEngine.UI.VerticalLayoutGrouplayoutGroup  // 0x10
  public            UnityEngine.RectTransform       layoutRectTransform  // 0x18
  public            UnityEngine.UI.LayoutElement    cell  // 0x20
PROPERTIES:
  cellHeight  get=0x0B0F801C
METHODS:
  RVA=0x041E1670  token=0x6000AF5  System.Void .ctor()
END_CLASS

CLASS: <_ScrollLayouts>d__12
TYPE:  sealed class
TOKEN: 0x2000117
SIZE:  0x68
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UIWorldLevelScrollListAnimCtrl<>4__this  // 0x20
  public            System.Boolean                  isUp  // 0x28
  public            System.Int32                    count  // 0x2c
  private           System.Single                   <elapsedTime>5__2  // 0x30
  private           System.Single                   <duration>5__3  // 0x34
  private           UnityEngine.Vector2             <upStartPos>5__4  // 0x38
  private           UnityEngine.Vector2             <downStartPos>5__5  // 0x40
  private           UnityEngine.Vector2             <middleStartPos>5__6  // 0x48
  private           UnityEngine.Vector2             <upEndPos>5__7  // 0x50
  private           UnityEngine.Vector2             <downEndPos>5__8  // 0x58
  private           UnityEngine.Vector2             <middleEndPos>5__9  // 0x60
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000AF6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000AF7  System.Void System.IDisposable.Dispose()
  RVA=0x0B0F8414  token=0x6000AF8  System.Boolean MoveNext()
  RVA=0x0B0F8B48  token=0x6000AFA  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: MotionLevel
TYPE:  sealed struct
TOKEN: 0x200011D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.MobileMotionManager.MotionLevelNONE  // const
  public    static  Beyond.UI.MobileMotionManager.MotionLevelLOW  // const
  public    static  Beyond.UI.MobileMotionManager.MotionLevelMEDIUM  // const
  public    static  Beyond.UI.MobileMotionManager.MotionLevelHIGH  // const
METHODS:
END_CLASS

CLASS: FontAssetRef
TYPE:  class
TOKEN: 0x2000120
SIZE:  0x40
FIELDS:
  public            Beyond.I18n.I18nFontSearchData.FontAssetInfofontAssetInfo  // 0x10
  private           System.Collections.Generic.HashSet<TMPro.TMP_Text>m_refTexts  // 0x18
  private           Beyond.Resource.FAssetProxyHandlem_assetHandle  // 0x20
  private           TMPro.TMP_FontAsset             m_fontAsset  // 0x38
METHODS:
  RVA=0x0B0F76C4  token=0x6000B22  TMPro.TMP_FontAsset GetFontAsset(TMPro.TMP_Text refText)
  RVA=0x0B0F785C  token=0x6000B23  System.Void RemoveRefText(TMPro.TMP_Text refText)
  RVA=0x0B0F7614  token=0x6000B24  System.Void Dispose()
  RVA=0x04659420  token=0x6000B25  System.Void .ctor()
END_CLASS

CLASS: FLerpByTime
TYPE:  sealed struct
TOKEN: 0x2000122
SIZE:  0x40
FIELDS:
  public    static  Beyond.UI.TweenUtils.FLerpByTimeEMPTY  // static @ 0x0
  private           System.Boolean                  <isEmpty>k__BackingField  // 0x10
  private           Beyond.FScaledStopwatch         m_timer  // 0x18
  public            System.Single                   startValue  // 0x30
  public            System.Single                   endValue  // 0x34
  public            System.Single                   duration  // 0x38
PROPERTIES:
  isEmpty  get=0x04D883E0  set=0x04D87180
METHODS:
  RVA=0x0B0F73B8  token=0x6000B29  System.Void Start(Beyond.ITickOwner timeScaleProvider)
  RVA=0x0B0F7294  token=0x6000B2A  System.Single GetValue(System.Boolean& isTweenFinished)
  RVA=0x0B0F741C  token=0x6000B2B  System.Void .cctor()
END_CLASS

CLASS: FOptions
TYPE:  sealed struct
TOKEN: 0x2000126
FIELDS:
  public            TComp                           cellTemplate  // 0x0
  public            UnityEngine.Transform           parent  // 0x0
  public            System.Action<TComp>            onItemCreated  // 0x0
METHODS:
END_CLASS

CLASS: ItemWrapper
TYPE:  class
TOKEN: 0x2000127
FIELDS:
  public            TComp                           comp  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000B3C  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000129
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UILoadImageSprite     <>4__this  // 0x10
  public            System.String                   path  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000B42  System.Void .ctor()
  RVA=0x0B0F81F4  token=0x6000B43  System.Void <_LoadSprite>b__0(System.Boolean isOk, Beyond.Resource.FAssetProxyHandle handle)
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x200012C
SIZE:  0x20
FIELDS:
  private           System.Action                   <onShown>k__BackingField  // 0x10
  private           System.Action                   <onHiden>k__BackingField  // 0x18
PROPERTIES:
  onShown  get=0x02B76770  set=0x05392320
  onHiden  get=0x04D88320  set=0x067D5E9C
METHODS:
END_CLASS

CLASS: TweenContext
TYPE:  sealed struct
TOKEN: 0x200012D
SIZE:  0x14
FIELDS:
  public            System.Single                   lastStopPos  // 0x10
METHODS:
  RVA=0x0B0F80FC  token=0x6000B60  System.Boolean IsInterapted()
  RVA=0x034059B0  token=0x6000B61  System.Void OnReset()
  RVA=0x03405950  token=0x6000B62  System.Void OnTweenStop(Beyond.UI.UISwitchTween.ITweenProgress tweenProgress)
END_CLASS

CLASS: ITweenHandler
TYPE:  interface
TOKEN: 0x200012E
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000B63  Beyond.UI.UISwitchTween.ITweenHandler SetAutoKill(System.Boolean autoKill)
  RVA=-1  // abstract  token=0x6000B64  Beyond.UI.UISwitchTween.ITweenHandler OnComplete(DG.Tweening.TweenCallback callback)
  RVA=-1  // abstract  token=0x6000B65  System.Boolean IsPlaying()
  RVA=-1  // abstract  token=0x6000B66  System.Void KillIfNecessary()
END_CLASS

CLASS: ITweenProgress
TYPE:  interface
TOKEN: 0x200012F
IMPLEMENTS: Beyond.IHotfixable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000B67  System.Single GetCurrPos()
END_CLASS

CLASS: TweenWrapper
TYPE:  class
TOKEN: 0x2000130
SIZE:  0x18
IMPLEMENTS: ITweenHandler Beyond.IHotfixable
FIELDS:
  private           DG.Tweening.Tween               m_tween  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000B68  System.Void .ctor(DG.Tweening.Tween tween)
  RVA=0x0B0F8190  token=0x6000B69  System.Boolean IsActive()
  RVA=0x034059E0  token=0x6000B6A  System.Boolean IsPlaying()
  RVA=0x034057A0  token=0x6000B6B  System.Void KillIfNecessary()
  RVA=0x03405BA0  token=0x6000B6C  Beyond.UI.UISwitchTween.ITweenHandler OnComplete(DG.Tweening.TweenCallback callback)
  RVA=0x03405B00  token=0x6000B6D  Beyond.UI.UISwitchTween.ITweenHandler SetAutoKill(System.Boolean autoKill)
END_CLASS

CLASS: <WaitForTweening>d__19
TYPE:  sealed class
TOKEN: 0x2000131
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.UISwitchTween         <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000B6E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000B6F  System.Void System.IDisposable.Dispose()
  RVA=0x0B0F8378  token=0x6000B70  System.Boolean MoveNext()
  RVA=0x0B0F83C8  token=0x6000B72  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Durations
TYPE:  sealed struct
TOKEN: 0x2000133
SIZE:  0x20
FIELDS:
  public            System.Single                   show  // 0x10
  public            System.Single                   showDelay  // 0x14
  public            System.Single                   hide  // 0x18
  public            System.Single                   hideDelay  // 0x1c
METHODS:
  RVA=0x045924A0  token=0x6000B8B  System.Single GetShowDuration(System.Single defaultDuration)
  RVA=0x045924E0  token=0x6000B8C  System.Single GetHideDuration(System.Single defaultDuration)
END_CLASS

CLASS: Builder
TYPE:  sealed struct
TOKEN: 0x2000134
SIZE:  0x30
FIELDS:
  public            UnityEngine.CanvasGroup         alphaHandler  // 0x10
  public            System.Boolean                  useTimeScale  // 0x18
  public            System.Boolean                  dontDisableGameObject  // 0x19
  public            System.Boolean                  controlBlockRaycast  // 0x1a
  public            System.Single                   duration  // 0x1c
  public            Beyond.UI.FadeSwitchTween.DurationscomplexDuration  // 0x20
METHODS:
  RVA=0x03F0F420  token=0x6000B8D  Beyond.UI.FadeSwitchTween Build()
END_CLASS

CLASS: TweenHandler
TYPE:  class
TOKEN: 0x2000136
SIZE:  0x20
IMPLEMENTS: ITweenHandler Beyond.IHotfixable
FIELDS:
  private           DG.Tweening.Tween               m_alphaTweener  // 0x10
  private           DG.Tweening.Tween               m_posTweener  // 0x18
METHODS:
  RVA=0x032AF420  token=0x6000B99  System.Void .ctor(DG.Tweening.Tween alpha, DG.Tweening.Tween pos)
  RVA=0x0B103BBC  token=0x6000B9A  System.Boolean IsPlaying()
  RVA=0x0B103C24  token=0x6000B9B  System.Void KillIfNecessary()
  RVA=0x0B103CAC  token=0x6000B9C  Beyond.UI.UISwitchTween.ITweenHandler OnComplete(DG.Tweening.TweenCallback callback)
  RVA=0x0B103D34  token=0x6000B9D  Beyond.UI.UISwitchTween.ITweenHandler SetAutoKill(System.Boolean autoKill)
END_CLASS

CLASS: UIAtlasProcessStatus
TYPE:  sealed struct
TOKEN: 0x2000138
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessStatusInvalid  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessStatusInsertWaiting  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessStatusInsertDone  // const
METHODS:
END_CLASS

CLASS: UIAtlasProcessFailureCause
TYPE:  sealed struct
TOKEN: 0x2000139
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseNoFailure  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseImageHasNullMainTexture  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseImageHasNullSprite  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseTextureWidthExceedLimit  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseTextureHeightExceedLimit  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseTextureASTCNotSupportedUnderEditor  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureLevelInfo  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseSpriteHasNonQuadMesh  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseSpriteHasAssociatedAlphaSplitTexture  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureLevelWarning  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseAtlasTextureHasNoSpaceLeft  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseSpriteRectHasNoPadding  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureLevelError  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseTextureFormatNotCompatible  // const
  public    static  Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCauseFailureCauseTextureSizeNotAlignedForCopyTexture  // const
METHODS:
END_CLASS

CLASS: UIAtlasHandle
TYPE:  sealed struct
TOKEN: 0x200013A
SIZE:  0x34
FIELDS:
  public            Beyond.UI.UIAtlasManager.UIAtlasProcessStatusstatus  // 0x10
  public            Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCausefailureCause  // 0x14
  public            System.Int32                    panelLevel  // 0x18
  public            System.Int32                    atlasIndex  // 0x1c
  public            UnityEngine.RectInt             atlasRect  // 0x20
  public            System.Int32                    textureId  // 0x30
METHODS:
  RVA=0x04DAAFD0  token=0x6000BB5  System.Void .ctor(Beyond.UI.UIAtlasManager.UIAtlasProcessStatus status, Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCause failureCause, System.Int32 panelLevel, System.Int32 atlasIndex, UnityEngine.RectInt atlasRect, System.Int32 textureId)
  RVA=0x0B103FD0  token=0x6000BB6  System.Boolean IsValid()
  RVA=0x03696CD0  token=0x6000BB7  Beyond.UI.UIAtlasManager.UIAtlasHandle CreateInvalidHandle(Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCause cause)
END_CLASS

CLASS: TextureRefHandle
TYPE:  sealed struct
TOKEN: 0x200013C
SIZE:  0x24
FIELDS:
  public            System.Int32                    refCount  // 0x10
  public            UnityEngine.RectInt             rect  // 0x14
METHODS:
  RVA=0x04D9C150  token=0x6000BC3  System.Void .ctor(System.Int32 refCount, UnityEngine.RectInt rect)
END_CLASS

CLASS: UIAtlasPage
TYPE:  class
TOKEN: 0x200013B
SIZE:  0x28
FIELDS:
  private   readonly UnityEngine.Texture2D           <pageTexture>k__BackingField  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIAtlasManager.UIAtlasPage.TextureRefHandle><textureRefDict>k__BackingField  // 0x18
  private   readonly HG.Rendering.Runtime.AtlasMaxRect<rectBinPack>k__BackingField  // 0x20
PROPERTIES:
  maxFreeRectWidth  get=0x0B106128
  maxFreeRectHeight  get=0x0B1060D0
  pageTexture  get=0x02B2ECC0
  textureRefDict  get=0x0385B100
  rectBinPack  get=0x04D862C0
METHODS:
  RVA=0x0B105F40  token=0x6000BB8  System.Void .ctor(System.Int32 panelDepth, System.Int32 index, UnityEngine.Experimental.Rendering.GraphicsFormat atlasFormat)
  RVA=0x0B105E14  token=0x6000BBE  UnityEngine.RectInt InsertRect(System.Int32 width, System.Int32 height)
  RVA=0x0B105B90  token=0x6000BBF  System.Void CopyIntoAtlas(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture texture, UnityEngine.RectInt& rect)
  RVA=0x0B105EB8  token=0x6000BC0  System.Void InsertRects(System.Collections.Generic.List<UnityEngine.RectInt> rects, System.Collections.Generic.List<UnityEngine.RectInt> dst)
  RVA=0x0B105D3C  token=0x6000BC1  System.Void FreeRect(UnityEngine.RectInt& rect)
  RVA=0x0B105DA8  token=0x6000BC2  System.Void FreeRects(System.Collections.Generic.List<UnityEngine.RectInt>& rects)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x2000140
SIZE:  0x20
FIELDS:
  public            System.String                   url  // 0x10
  public            System.Action                   onClose  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BD0  System.Void .ctor()
  RVA=0x0B103DE8  token=0x6000BD1  System.Void <Start>b__0(System.Int32 i, System.String s)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000141
SIZE:  0x20
FIELDS:
  public            System.String                   url  // 0x10
  public            System.Action                   onClose  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BD2  System.Void .ctor()
  RVA=0x0B103EE0  token=0x6000BD3  System.Void <StartHGBrowser>b__0(System.String s)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000152
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.UI.UIState.UIStateController.<>c<>9  // static @ 0x0
  public    static  System.Predicate<Beyond.UI.UIState.StateAnimationInfo><>9__10_0  // static @ 0x8
METHODS:
  RVA=0x04D4C690  token=0x6000C27  System.Void .cctor()
  RVA=0x041E1670  token=0x6000C28  System.Void .ctor()
  RVA=0x0B103DD0  token=0x6000C29  System.Boolean <Tick>b__10_0(Beyond.UI.UIState.StateAnimationInfo info)
END_CLASS

CLASS: CharOffsetData
TYPE:  sealed struct
TOKEN: 0x200017A
FIELDS:
  public            System.Single                   scale  // 0x0
  public            System.Single                   xAdvance  // 0x0
  public            System.Single                   xOffset  // 0x0
  public            System.Single                   yOffset  // 0x0
  public            System.Int32                    prefabIndex  // 0x0
METHODS:
END_CLASS

CLASS: BufferInternalNode
TYPE:  sealed struct
TOKEN: 0x200017C
FIELDS:
  public            Beyond.UI.GPUI.GPUIHandle       handle  // 0x0
  public            System.Int32                    startIndex  // 0x0
  public            System.Int32                    previousLength  // 0x0
  public            System.Int32                    sortingOrder  // 0x0
METHODS:
END_CLASS

CLASS: DynamicAtlasFailureCause
TYPE:  sealed struct
TOKEN: 0x200017E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseNoFailure  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseInvalidStaticAtlas  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseInvalidDynamicAtlasRect  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseSpriteHasNullTexture  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseSpriteHasAssociatedAlphaSplitTexture  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseTextureWidthExceedLimit  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseTextureHeightExceedLimit  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseEngineAtlasCreateFailed  // const
  public    static  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCauseFailureCauseEngineAtlasAllocateFailed  // const
METHODS:
END_CLASS

CLASS: DynamicSpriteHandle
TYPE:  sealed struct
TOKEN: 0x200017F
SIZE:  0x20
FIELDS:
  public            UnityEngine.Sprite              sprite  // 0x10
  public            System.Int32                    spriteInstanceId  // 0x18
METHODS:
  RVA=0x0357C1F0  token=0x6000C9C  System.Boolean IsValid()
  RVA=0x0357B2E0  token=0x6000C9D  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle CreateInvalidHandle()
END_CLASS

CLASS: SpriteRecord
TYPE:  sealed class
TOKEN: 0x2000180
SIZE:  0x30
FIELDS:
  public            UnityEngine.Sprite              sprite  // 0x10
  public            System.Int32                    spriteInstanceId  // 0x18
  public            System.Int32                    refCount  // 0x1c
  public            UnityEngine.Vector4             uv  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000C9E  System.Void .ctor()
END_CLASS

CLASS: DynamicPrefabNodeKey
TYPE:  sealed struct
TOKEN: 0x2000182
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    readonly System.Int32                    basePrefabOffset  // 0x10
  public    readonly UnityEngine.Sprite              sprite  // 0x18
  public    readonly System.Int32                    spriteInstanceId  // 0x20
METHODS:
  RVA=0x0357D700  token=0x6000CAA  System.Void .ctor(System.Int32 basePrefabOffset, UnityEngine.Sprite sprite)
  RVA=0x0357D660  token=0x6000CAB  System.Boolean Equals(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey other)
  RVA=0x0B0FE3F0  token=0x6000CAC  System.Boolean Equals(System.Object obj)
  RVA=0x040DEC60  token=0x6000CAD  System.Int32 GetHashCode()
  RVA=0x0357DAD0  token=0x6000CAE  System.Int32 _GetSpriteInstanceId(UnityEngine.Sprite sprite)
  RVA=0x0B0FE4A4  token=0x6000CAF  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x0B0FE50C  token=0x6000CB0  System.Int32 <>iFixBaseProxy_GetHashCode()
END_CLASS

CLASS: DynamicPrefabNodeEntry
TYPE:  sealed class
TOKEN: 0x2000183
SIZE:  0x30
FIELDS:
  public            Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKeykey  // 0x10
  public            System.UInt16                   dynamicPrefabId  // 0x28
  public            System.Int32                    refCount  // 0x2c
METHODS:
  RVA=0x041E1670  token=0x6000CB1  System.Void .ctor()
END_CLASS

CLASS: SpriteOverrideState
TYPE:  sealed struct
TOKEN: 0x2000186
SIZE:  0x48
FIELDS:
  public            System.Boolean                  enabled  // 0x10
  public            Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandlespriteHandle  // 0x18
  public            Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKeyactiveKey  // 0x28
  public            System.UInt16                   dynamicPrefabId  // 0x40
METHODS:
END_CLASS

CLASS: CanvasSnapshot
TYPE:  sealed struct
TOKEN: 0x200018D
SIZE:  0x68
FIELDS:
  public            UnityEngine.Matrix4x4           matrix  // 0x10
  public            System.Single                   scaleFactor  // 0x50
  public            UnityEngine.RenderMode          renderMode  // 0x54
  public            UnityEngine.Camera              camera  // 0x58
  public            System.Int32                    screenWidth  // 0x60
  public            System.Int32                    screenHeight  // 0x64
METHODS:
END_CLASS

CLASS: ShaderIDs
TYPE:  static class
TOKEN: 0x200018F
SIZE:  0x10
FIELDS:
  private   static readonly System.Int32                    CANVAS_OBJECT_TO_WORLD_MATRIX  // static @ 0x0
  private   static readonly System.Int32                    VAT_TEX  // static @ 0x4
  private   static readonly System.Int32                    MAIN_TEX  // static @ 0x8
  private   static readonly System.Int32                    SPRITE_TEX  // static @ 0xc
METHODS:
  RVA=0x04CB1600  token=0x6000D67  System.Void .cctor()
END_CLASS

CLASS: __StaticArrayInitTypeSize=24
TYPE:  sealed struct
TOKEN: 0x200019A
SIZE:  0x28
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x2000199
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24FE78C65211DD0B56A97024FB61111E686EF1FE054AA132BA58E2891AC496F1EE  // static @ 0x0
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.UI.UIScrollCellCircleEffect
TYPE:  class
TOKEN: 0x200000C
SIZE:  0xA8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Transform           _target  // 0x68
  private           System.Single                   _radius  // 0x70
  private           UnityEngine.Transform           _effectNode  // 0x78
  private           System.Boolean                  _faceToUICamera  // 0x80
  private           System.Boolean                  _alwaysFaceToCamera  // 0x81
  private           UnityEngine.Vector3             m_lastPosition  // 0x84
  private           Beyond.UI.LuaPanel              m_panel  // 0x90
  private           UnityEngine.Transform           <lookAtCamera>k__BackingField  // 0x98
  private           System.Boolean                  m_left  // 0xa0
PROPERTIES:
  tickOption  get=0x0B08B848
  panel  get=0x0B08B788
  lookAtCamera  get=0x04D85EE0  set=0x06352D0C
METHODS:
  RVA=0x0B08B2DC  token=0x600002D  System.Void _InitState()
  RVA=0x0B08B5B8  token=0x600002E  System.Void _UpdatePos()
  RVA=0x0B08B3A0  token=0x600002F  System.Void _UpdateLookAtCamera()
  RVA=0x0B08B13C  token=0x6000030  System.Void SetLookAtCamera(UnityEngine.Transform faceCamera, System.Boolean update)
  RVA=0x0B08B0E4  token=0x6000031  System.Void OnEnable()
  RVA=0x0B08B1D8  token=0x6000032  System.Void Start()
  RVA=0x0B08B280  token=0x6000033  System.Void Tick(System.Single deltaTime)
  RVA=0x0B08B038  token=0x6000034  System.Void ForceUpdate()
  RVA=0x0B08B088  token=0x6000035  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B08B778  token=0x6000036  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000037  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x6000038  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x6000039  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06BD8710  token=0x600003A  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.Gameplay.UI.UIScrollCellSlantEffect
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x88
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           System.Single                   _topY  // 0x68
  private           System.Single                   _bottomY  // 0x6c
  private           System.Single                   _leftX  // 0x70
  private           System.Single                   _maxWidth  // 0x74
  private           UnityEngine.AnimationCurve      _curve  // 0x78
  private           UnityEngine.Transform[]         _cells  // 0x80
PROPERTIES:
  tickOption  get=0x04A79630
METHODS:
  RVA=0x0364C480  token=0x600003C  System.Void Tick(System.Single deltaTime)
  RVA=0x039C2B00  token=0x600003D  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600003E  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x600003F  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.PoolCore.ListPoolInitializer_UI_Beyond
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_initialized  // static @ 0x0
METHODS:
  RVA=0x04CB6B40  token=0x600001C  System.Void InitializeAtRuntime()
  RVA=0x04CB6B70  token=0x600001D  System.Void Initialize()
END_CLASS

CLASS: Beyond.Scripts.UI.Tools.UIGridLayoutElement
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutElement
FIELDS:
  private           UnityEngine.UI.GridLayoutGroup  m_gridLayoutGroup  // 0x18
PROPERTIES:
  minWidth  get=0x0B0884B4
  preferredWidth  get=0x0B088614
  flexibleWidth  get=0x0B088368
  minHeight  get=0x0B088404
  preferredHeight  get=0x0B088564
  flexibleHeight  get=0x0B088318
  layoutPriority  get=0x0B0883B8
METHODS:
  RVA=0x0B08807C  token=0x600001E  System.Void CalculateLayoutInputHorizontal()
  RVA=0x0B0880BC  token=0x600001F  System.Void CalculateLayoutInputVertical()
  RVA=0x0B0880FC  token=0x6000027  System.Single _GetPreferredSize(System.Boolean widthOrHeight)
  RVA=0x05393520  token=0x6000028  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000017  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000018  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600001B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIText
TYPE:  class
TOKEN: 0x200000E
SIZE:  0xC98
EXTENDS: TMPro.TextMeshProUGUI
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp UnityEngine.ISerializationCallbackReceiver UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler
FIELDS:
  public            Beyond.UI.UIText.RichTextStyleSeqrichTextStyleSeq  // 0xbc8
  private           System.Boolean                  m_textHasHyperlink  // 0xbcc
  private   static readonly System.Text.RegularExpressions.RegexRICH_TEXT_INNERMOST_REGEX  // static @ 0x0
  private   static  System.Int32                    RICH_TEXT_RESOLVE_MAX_ITERATIONS  // const
  private   static readonly System.Text.RegularExpressions.RegexHYPERLINK_TEXT_REGEX  // static @ 0x8
  private   static readonly System.Text.RegularExpressions.RegexPHONETIC_REGEX  // static @ 0x10
  public    static  System.String                   HYPERLINK_ID_PREFIX  // const
  private           System.Boolean                  _loadByCode  // 0xbcd
  private           System.String                   _textId  // 0xbd0
  private           System.Boolean                  _devOnly  // 0xbd8
  private           System.Int32                    _externalStaticWidth  // 0xbdc
  private           System.Int32                    _externalStaticHeight  // 0xbe0
  private           System.Boolean                  m_firstSetByCode  // 0xbe4
  private   static readonly System.Collections.Generic.HashSet<Beyond.GEnums.EnvLang>s_forceLeftAlignLanguage  // static @ 0x18
  private           System.Collections.Generic.List<Beyond.UI.UIText.RichTextInfo>m_richTextInfos  // 0xbe8
  private           Beyond.TickFunction             m_updateFunction  // 0xbf0
  private           Beyond.UI.UIText.RichTextAnalyzer.ProcessRichTextEntryFuncm_processRichTextEntryFunc  // 0xbf8
  public            UnityEngine.Events.UnityEvent<System.String>onClickLink  // 0xc00
  public            UnityEngine.Events.UnityEvent<System.String,System.Boolean>onHoverLinkChange  // 0xc08
  private           System.Boolean                  m_isPointerInside  // 0xc10
  private           System.String                   m_curHoverLinkId  // 0xc18
  private           System.Collections.Generic.List<System.String>m_linkList  // 0xc20
  private           System.Boolean                  m_linkShrunk  // 0xc28
  private           System.String                   _hyperlinkUITextGroupId  // 0xc30
  private           System.Int32                    _hyperlinkUITextOrder  // 0xc38
  private           Beyond.UI.UIText.HyperlinkUITextWrapm_hyperlinkUITextWrap  // 0xc40
  private           Beyond.UI.LuaPanel              m_panel  // 0xc48
  public            System.Int32                    sourceCNFontId  // 0xc50
  private           TMPro.TMP_FontAsset             m_i18nFontAsset  // 0xc58
  private   static  System.Text.StringBuilder       s_stringBuilder  // static @ 0x20
  public            System.Boolean                  useMinMaxSize  // 0xc60
  public            UnityEngine.Vector2             maxSize  // 0xc64
  public            UnityEngine.Vector2             minSize  // 0xc6c
  private   readonly System.Collections.Generic.Queue<UnityEngine.GameObject>m_inlineImagePool  // 0xc78
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIText.ImageGOInfo>m_indexToImageGo  // 0xc80
  private   static  System.String                   INLINE_IMAGE_FOLDER_PATH  // const
  private   static  System.String                   KEY_HINT_ICON_COMMON_FOLDER  // const
  private   readonly System.Collections.Generic.Dictionary<System.String,Beyond.Resource.FAssetProxyHandle>m_imageCache  // 0xc88
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Boolean>m_delayedActiveChangeImageGOIndexes  // 0xc90
PROPERTIES:
  textId  get=0x0B08EF14
  text  get=0x03E60EB0  set=0x03E866B0
  processRichTextEntryFunc  get=0x02F27B70
  panel  get=0x0B08EE54
  m_fontAsset  get=0x0351DFC0  set=0x0B08F018
  fontSharedMaterial  get=0x040B60F0  set=0x0B08EF60
METHODS:
  RVA=0x04488730  token=0x6000040  System.Void SetAndResolveTextStyle(System.String targetText)
  RVA=0x03D2B040  token=0x6000041  System.String GetPhoneticText(Beyond.GEnums.PhoneticType phoneticType, System.String id)
  RVA=0x03D2AFD0  token=0x6000042  System.Void SetPhoneticText(Beyond.GEnums.PhoneticType phoneticType, System.String id)
  RVA=0x035CCDD0  token=0x6000043  System.String _ResolveTextStyle(System.String text, System.Int32 richSeqIndex)
  RVA=0x0B08D7C4  token=0x6000044  System.String _GetHyperlinkText(System.String text, System.Boolean& hasLink)
  RVA=0x03329F10  token=0x6000046  System.Void OnEnvLangChanged(Beyond.GEnums.EnvLang lang)
  RVA=0x0382AA20  token=0x6000049  System.Void _CreateUpdateFunction()
  RVA=0x03519BC0  token=0x600004A  System.Void _ReleaseUpdateFunction()
  RVA=0x044042B0  token=0x600004B  System.Void ClearMesh()
  RVA=0x02F27920  token=0x600004C  System.Void RefreshPopulateText()
  RVA=0x0B08BE40  token=0x600004D  UnityEngine.Material LoadMaterial(System.String materialName)
  RVA=0x03519750  token=0x600004E  System.Void OnDestroy()
  RVA=0x0351DDA0  token=0x600004F  System.Void Awake()
  RVA=0x0351E150  token=0x6000050  TMPro.TMP_FontAsset GetFontAsset()
  RVA=0x0351DEC0  token=0x6000051  System.Void _GenerateRuntimeText()
  RVA=0x02F27C70  token=0x6000052  System.Void _ProcessRichTextInfo()
  RVA=0x04006240  token=0x6000053  System.Void _OnPreRenderText(TMPro.TMP_TextInfo textInfo)
  RVA=0x0B08E398  token=0x6000055  Beyond.UI.UIText.RichTextInfo _ProcessRichTextEntry(Beyond.UI.UIText.RichTextAnalyzer.RichTextParam mainParam, System.Collections.Generic.List<Beyond.UI.UIText.RichTextAnalyzer.RichTextParam> subParams, Cysharp.Text.Utf16ValueStringBuilder& stringBuilder)
  RVA=0x0B08C134  token=0x6000056  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B08C5C0  token=0x6000057  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B08C6AC  token=0x6000058  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04128720  token=0x6000059  System.Void Start()
  RVA=0x0351C9B0  token=0x600005A  System.Void OnEnable()
  RVA=0x03519820  token=0x600005B  System.Void OnDisable()
  RVA=0x0B08EA88  token=0x600005C  System.Void _UpdateTick(System.Single deltaTime)
  RVA=0x0B08CB18  token=0x600005D  System.Void ShrinkLinkTags()
  RVA=0x0B08D1D0  token=0x600005E  System.Boolean TryGetLinkId(UnityEngine.Vector3 position, System.String& linkId)
  RVA=0x0B08D3BC  token=0x600005F  System.Boolean TryGetLinkId(System.Int32 linkIndex, System.String& linkId)
  RVA=0x0B08D518  token=0x6000060  System.Boolean TryGetOriginalLinkId(System.String linkId, System.String& originalLinkId)
  RVA=0x0B08D030  token=0x6000061  System.Boolean TryGetCharacterStartPosAndHeight(System.Int32 charIndex, UnityEngine.Vector3& worldPos, System.Single& charHeight)
  RVA=0x0B08CE90  token=0x6000062  System.Boolean TryGetCharacterEndPosAndHeight(System.Int32 charIndex, UnityEngine.Vector3& worldPos, System.Single& charHeight)
  RVA=-1  // generic def  token=0x6000063  System.Void CombineStringWithLanguageSpilt(T1 t1, T2 t2)
  RVA=0x0B08BA94  token=0x6000064  System.Void CombineStringWithLanguageSpilt(System.String t1, System.String t2)
  RVA=-1  // generic def  token=0x6000065  System.Void CombineStringReverseForIndonesianAndVietnamese(T1 t1, T2 t2)
  RVA=0x0B08B9E8  token=0x6000066  System.Void CombineStringReverseForIndonesianAndVietnamese(System.String t1, System.String t2)
  RVA=0x0B08C808  token=0x6000067  System.Void SetHyperlinkUITextGroup(System.String groupId, System.Int32 order)
  RVA=0x0390F680  token=0x6000068  System.Boolean IsHyperlinkUITextGroupDisplayable(System.String groupId)
  RVA=0x0B08BB40  token=0x6000069  System.Collections.Generic.List<Beyond.UI.UIText> GetGroupDisplayableHyperlinkUIText(System.String groupId)
  RVA=0x0351CA30  token=0x600006B  System.Void ClearComponent()
  RVA=0x0B08D6A0  token=0x600006C  System.Void UpdateI18NFontAsset()
  RVA=0x0351EA50  token=0x6000071  System.Void _RefreshMatTexture()
  RVA=0x0B08C964  token=0x6000072  System.Void SetSharedMaterial(UnityEngine.Material mat)
  RVA=0x03F709E0  token=0x6000073  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x04032550  token=0x6000074  System.Void OnAfterDeserialize()
  RVA=0x0B08B894  token=0x6000075  TMPro.TMP_SubMeshUI AddSubTextObject(TMPro.TextMeshProUGUI textComponent, TMPro.MaterialReference materialReference)
  RVA=0x0B08BF4C  token=0x6000076  System.String LocalStringCombineFormat(System.Int32 count)
  RVA=0x03E64370  token=0x6000077  UnityEngine.Vector2 CalculatePreferredValues(System.Single& fontSize, UnityEngine.Vector2 marginSize, System.Boolean isTextAutoSizingEnabled, System.Boolean isWordWrappingEnabled)
  RVA=0x0B08BBC8  token=0x6000078  UnityEngine.Sprite GetOrLoadSprite(System.String path)
  RVA=0x0351D600  token=0x6000079  System.Void DisposeInlineImageCache()
  RVA=0x02F27FA0  token=0x600007A  System.Void _ClearInlineImage()
  RVA=0x0B08DFB8  token=0x600007B  Beyond.UI.UIText.ImageGOInfo _GetImageGoByIndex(System.Int32 index)
  RVA=0x0B08E190  token=0x600007C  UnityEngine.GameObject _GetOrCreateImageGO()
  RVA=0x0B08E9CC  token=0x600007D  System.Void _ReturnImageGO(UnityEngine.GameObject go)
  RVA=0x0B08D738  token=0x600007E  System.Collections.IEnumerator _DelayedChangeActiveImageGo()
  RVA=0x03695650  token=0x600007F  System.Void .ctor()
  RVA=0x0432B870  token=0x6000080  System.Void .cctor()
  RVA=0x0B08D688  token=0x6000081  System.String <>iFixBaseProxy_get_text()
  RVA=0x0B08D698  token=0x6000082  System.Void <>iFixBaseProxy_set_text(System.String P0)
  RVA=0x0B08D658  token=0x6000083  System.Void <>iFixBaseProxy_ClearMesh()
  RVA=0x0B08D678  token=0x6000084  System.Void <>iFixBaseProxy_RefreshPopulateText()
  RVA=0x011EC580  token=0x6000085  UnityEngine.Material <>iFixBaseProxy_LoadMaterial(System.String P0)
  RVA=0x0B08D660  token=0x6000086  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B08D638  token=0x6000087  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x6000088  System.Void <>iFixBaseProxy_Start()
  RVA=0x0B08D670  token=0x6000089  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B08D668  token=0x600008A  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x04D8D1B0  token=0x600008B  TMPro.TMP_FontAsset <>iFixBaseProxy_get_m_fontAsset()
  RVA=0x05397170  token=0x600008C  System.Void <>iFixBaseProxy_set_m_fontAsset(TMPro.TMP_FontAsset P0)
  RVA=0x04D8D910  token=0x600008D  UnityEngine.Material <>iFixBaseProxy_get_fontSharedMaterial()
  RVA=0x0B08D690  token=0x600008E  System.Void <>iFixBaseProxy_set_fontSharedMaterial(UnityEngine.Material P0)
  RVA=0x0B08D680  token=0x600008F  System.Void <>iFixBaseProxy_SetSharedMaterial(UnityEngine.Material P0)
  RVA=0x0B08D5F8  token=0x6000090  TMPro.TMP_SubMeshUI <>iFixBaseProxy_AddSubTextObject(TMPro.TextMeshProUGUI P0, TMPro.MaterialReference P1)
  RVA=0x0B08D640  token=0x6000091  UnityEngine.Vector2 <>iFixBaseProxy_CalculatePreferredValues(System.Single& P0, UnityEngine.Vector2 P1, System.Boolean P2, System.Boolean P3)
END_CLASS

CLASS: Beyond.UI.UIConst
TYPE:  static class
TOKEN: 0x200001F
SIZE:  0x10
FIELDS:
  public    static  System.Single                   PC_REFERENCE_RESOLUTION_SCALE  // const
  public    static  System.Single                   MOBILE_PAD_DEVICE_REFERENCE_RESOLUTION_SCALE  // const
  public    static readonly UnityEngine.Vector3             OUT_OF_SCREEN_POS  // static @ 0x0
  public    static  System.Single                   STANDARD_HORIZONTAL_RESOLUTION  // const
  public    static  System.Single                   STANDARD_VERTICAL_RESOLUTION  // const
  public    static  System.Int32                    INVALID_FINGER_ID  // const
  public    static  System.Single                   STANDARD_RATIO  // static @ 0xc
  public    static  System.Single                   SCREEN_SPACE_CAMERA_PANEL_DISTANCE  // const
  public    static  System.String                   UI_BLUR_MATERIAL  // const
  public    static  System.String                   CS_POPUP_PANEL_PATH  // const
  public    static  System.String                   NAVIGATION_TARGET_CLICK_NO_HINT_ACTION_ID  // const
  public    static  System.String                   NAVIGATION_TARGET_CLICK_ACTION_ID  // const
  public    static  System.String                   UI_SPRITE_CHAR_HEAD_RECTANGLE  // const
  public    static  System.String                   UI_SPRITE_CHAR_REMOTE_ICON  // const
  public    static  System.String                   UI_SPRITE_CHAR_REMOTE_ICON_700  // const
  public    static  System.String                   UI_SPRITE_DIALOG_BG  // const
  public    static  System.String                   UI_SPRITE_DIALOG_OPTION_ICON  // const
  public    static  System.String                   SLIDER_STICK_SCROLL_HORI_ACTION_ID  // const
PROPERTIES:
  STANDARD_HORIZONTAL_RESOLUTION_INT  get=0x0B087F6C
  STANDARD_VERTICAL_RESOLUTION_INT  get=0x0B087FB8
  CUR_STANDARD_HORIZONTAL_RESOLUTION  get=0x03C00930
  CUR_STANDARD_VERTICAL_RESOLUTION  get=0x03C008C0
  dragThreshold  get=0x0B088004
METHODS:
  RVA=0x03C004D0  token=0x60000C5  System.Single GetResolutionScale()
  RVA=0x0B087E24  token=0x60000C6  System.Boolean IsPadDevice()
  RVA=0x04D3A030  token=0x60000C7  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIModelLoader
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Collections.Generic.Dictionary<UnityEngine.GameObject,Beyond.Resource.FAssetProxyHandle>m_instance2AssetHandle  // 0x10
  private           System.Int32                    m_requestCounter  // 0x18
  private           System.Collections.Generic.HashSet<System.Int32>m_requestIds  // 0x20
  private           System.Boolean                  m_disposed  // 0x28
  private   static  System.Collections.Generic.List<MagicaCloth.MagicaBoneCloth>s_magicaBoneCloths  // static @ 0x0
METHODS:
  RVA=0x040E52D0  token=0x60000C8  System.Void .ctor()
  RVA=0x0B08ACE0  token=0x60000C9  UnityEngine.GameObject LoadModel(System.String modelPath, UnityEngine.Transform parent)
  RVA=0x0B08AAFC  token=0x60000CA  System.Int32 LoadModelAsync(System.String modelPath, UnityEngine.Transform parent, System.Action<UnityEngine.GameObject> callback)
  RVA=0x0B08AA74  token=0x60000CB  System.Void Cancel(System.Int32 requestId)
  RVA=0x0B08AEC0  token=0x60000CC  System.Void UnloadModel(UnityEngine.GameObject modelGo)
  RVA=0x02F65460  token=0x60000CD  System.Void _Clear()
  RVA=0x02F65420  token=0x60000CE  System.Void Dispose()
  RVA=0x04D16830  token=0x60000CF  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIUtils
TYPE:  static class
TOKEN: 0x2000024
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    MAX_SHARED_INT_STRING  // const
  private   static readonly System.String[]                 s_sharedIntStringPool  // static @ 0x0
  private   static  System.Int32                    MAX_SHARED_INT_ROMAN_STRING  // const
  private   static readonly System.String[]                 s_sharedIntStringRomanPool  // static @ 0x8
  private   static  UnityEngine.Vector3[]           s_worldCorners  // static @ 0x10
  private   static  System.String                   RICH_TEXT_PATTERN  // const
METHODS:
  RVA=0x03848850  token=0x60000D2  System.String GetSharedIntString(System.Int32 value)
  RVA=0x0B08F9E8  token=0x60000D3  System.String GetSharedIntStringRoman(System.Int32 value)
  RVA=0x03098A60  token=0x60000D4  System.Boolean IsScreenPosInRectTransform(UnityEngine.Vector3 screenPos, UnityEngine.RectTransform rectTransform, UnityEngine.Camera uiCamera)
  RVA=0x0B08FB64  token=0x60000D5  System.Boolean IsScreenPosInRectTransform(UnityEngine.Vector2 screenPos, UnityEngine.RectTransform rectTransform, UnityEngine.Camera uiCamera)
  RVA=0x0B08F1C4  token=0x60000D6  UnityEngine.Bounds CalcBoundOfRectTransform(UnityEngine.RectTransform transform, UnityEngine.RectTransform local)
  RVA=0x0B09019C  token=0x60000D7  System.Boolean TryGetTouch(System.Int32 touchId, UnityEngine.InputSystem.EnhancedTouch.Touch& touch)
  RVA=0x030477E0  token=0x60000D8  System.Void SetAlpha(UnityEngine.UI.Graphic image, System.Single alpha)
  RVA=0x0B08FF14  token=0x60000D9  System.Void SetColorWithoutAlpha(UnityEngine.UI.Graphic image, UnityEngine.Color color)
  RVA=0x032EE4D0  token=0x60000DA  System.Void ClearUIComponents(UnityEngine.GameObject gameObject)
  RVA=0x0B08FCF8  token=0x60000DB  UnityEngine.Rect RectTransformToScreenRect(UnityEngine.RectTransform rectTransform, UnityEngine.Camera uiCamera)
  RVA=0x0B08F510  token=0x60000DC  UnityEngine.Vector2 ClampPointInRectTransform(UnityEngine.RectTransform source, UnityEngine.RectTransform target, UnityEngine.Vector2 sourcePoint)
  RVA=0x0468BDB0  token=0x60000DD  UnityEngine.Vector3 GetNodeScaleOffset(UnityEngine.RectTransform referenceTransform, UnityEngine.RectTransform selfTransform)
  RVA=0x0B08F6CC  token=0x60000DE  UnityEngine.Color GetColorByString(System.String strColor)
  RVA=0x0B08FFAC  token=0x60000DF  System.Void SetMaskRatio(UnityEngine.UI.RectMask2D mask, System.Single r)
  RVA=0x0B08F940  token=0x60000E0  System.Single GetCursorTipOffsetX(System.Single defaultOffset)
  RVA=0x0B08F8E8  token=0x60000E1  System.Single GetCursorTipOffsetInScreen(System.Single defaultOffsetInScreen)
  RVA=0x0B090080  token=0x60000E2  System.Boolean TryGetTextWithoutRichStyle(System.String richText, System.String& rawText)
  RVA=0x04CB4840  token=0x60000E3  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.ActiveSyncHelper
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.GameObject>_objs  // 0x18
  private           System.Boolean                  _isInverse  // 0x20
METHODS:
  RVA=0x0378E780  token=0x60000E4  System.Void Awake()
  RVA=0x0378EBC0  token=0x60000E5  System.Void OnEnable()
  RVA=0x0378EB80  token=0x60000E6  System.Void OnDisable()
  RVA=0x0378F030  token=0x60000E7  System.Void Sync()
  RVA=0x03695570  token=0x60000E8  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.Billboard
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x70
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            UnityEngine.Camera              sceneCamera  // 0x68
PROPERTIES:
  tickOption  get=0x0B084F50
METHODS:
  RVA=0x0B084DC4  token=0x60000EA  System.Void OnEnable()
  RVA=0x0B084E70  token=0x60000EB  System.Void Tick(System.Single deltaTime)
  RVA=0x039C2B00  token=0x60000EC  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60000ED  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x60000EE  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x60000EF  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.CinemachineGyroscopeEffect
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x30
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            System.Single                   offsetX  // 0x28
  public            System.Single                   offsetY  // 0x2c
METHODS:
  RVA=0x0B084FA0  token=0x60000F0  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x05393520  token=0x60000F1  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CommonItemToastList
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x278
EXTENDS: Beyond.UI.UIScrollList
FIELDS:
METHODS:
  RVA=0x0396C790  token=0x60000F2  System.Void ScrollToIndex(System.Int32 index, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType, System.Boolean graduallyShowInFastMode)
  RVA=0x0396C530  token=0x60000F3  System.Void AdjustContainerPos(System.Int32 index, System.Boolean fastMode)
  RVA=0x0396C6D0  token=0x60000F4  UnityEngine.Vector2 _GetUnConstraintTargetPos(System.Int32 index)
  RVA=0x039C1F30  token=0x60000F5  System.Void .ctor()
  RVA=0x04CC5EE0  token=0x60000F6  UnityEngine.Vector2 <AdjustContainerPos>b__1_0()
  RVA=0x047C3FC0  token=0x60000F7  System.Void <AdjustContainerPos>b__1_1(UnityEngine.Vector2 x)
END_CLASS

CLASS: Beyond.UI.ControllerSideMenuItemList
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Collections.Generic.List<Beyond.UI.ControllerSideMenuItemList.MeunItemInfo>menuItems  // 0x18
  public            UnityEngine.Sprite              icon  // 0x20
  public            System.String                   title  // 0x28
  public            UnityEngine.RectTransform       contentPosTrans  // 0x30
  public            System.Boolean                  isFullScreen  // 0x38
  private   static readonly System.Collections.Generic.HashSet<System.String>s_forceValidItemList  // static @ 0x0
METHODS:
  RVA=0x0B0853D0  token=0x60000F8  System.Void ToggleItemForceValid(System.String itemName, System.Boolean forceValid)
  RVA=0x0B08532C  token=0x60000F9  System.Boolean IsItemForceValid(System.String itemName)
  RVA=0x0B085298  token=0x60000FA  System.Void ClearForceValidItemList()
  RVA=0x04B37520  token=0x60000FB  System.Void .ctor()
  RVA=0x04D18720  token=0x60000FC  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.CustomNotchAdapt
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  isLeft  // 0x18
  public            System.Boolean                  isRight  // 0x19
  public            System.Boolean                  isBottom  // 0x1a
  public            System.Boolean                  isFullHorizontalStretch  // 0x1b
METHODS:
  RVA=0x03695570  token=0x6000104  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIStyleInputValidType
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIStyleInputValidType All  // const
  public    static  Beyond.UI.UIStyleInputValidType Keyboard  // const
  public    static  Beyond.UI.UIStyleInputValidType Touch  // const
  public    static  Beyond.UI.UIStyleInputValidType Controller  // const
  public    static  Beyond.UI.UIStyleInputValidType KeyboardAndTouch  // const
  public    static  Beyond.UI.UIStyleInputValidType KeyboardAndController  // const
  public    static  Beyond.UI.UIStyleInputValidType TouchAndController  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIStyleInputValidTypeMask
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIStyleInputValidTypeMaskKeyboard  // const
  public    static  Beyond.UI.UIStyleInputValidTypeMaskTouch  // const
  public    static  Beyond.UI.UIStyleInputValidTypeMaskController  // const
  public    static  Beyond.UI.UIStyleInputValidTypeMaskAll  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.ActiveType
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.ActiveType            UseGameObjectActive  // const
  public    static  Beyond.UI.ActiveType            UseScale  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.CustomUIStyle
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIStyleInputValidType inputValidType  // 0x18
  public            Beyond.UI.ActiveType            activeType  // 0x1c
  private           UnityEngine.UI.LayoutElement    _layoutElement  // 0x20
  private           UnityEngine.Animator            _animator  // 0x28
  private           Beyond.UI.CustomUIStyle.OverrideValidStatem_overrideValidState  // 0x30
PROPERTIES:
  inputValidTypeMask  get=0x0B0855F0  set=0x0B0856F0
  overrideValidState  get=0x0B0856A4  set=0x0B0857A4
  isValid  get=0x040F34C0
METHODS:
  RVA=0x0B0854EC  token=0x6000109  System.Void _OnActiveTypeChanged()
  RVA=0x039D27E0  token=0x600010B  System.Void OnDestroy()
  RVA=0x040F3410  token=0x600010C  System.Void OnEnable()
  RVA=0x03CECD20  token=0x600010D  System.Void Awake()
  RVA=0x040F3450  token=0x600010E  System.Void CheckState()
  RVA=0x040F35D0  token=0x600010F  System.Void ToggleActiveState(System.Boolean isActive)
  RVA=0x040F3500  token=0x6000110  System.Boolean IsShowTypeValid(Beyond.UI.UIStyleInputValidType inputValidType)
  RVA=0x03CEC790  token=0x6000111  System.Boolean IsValidInput(Beyond.UI.UIStyleInputValidTypeMask inputValidTypeMask)
  RVA=0x03695570  token=0x6000112  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.DeactivateNaviOnEnable
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   key  // 0x18
METHODS:
  RVA=0x0B085894  token=0x6000113  System.Void OnEnable()
  RVA=0x0B085804  token=0x6000114  System.Void OnDisable()
  RVA=0x05393520  token=0x6000115  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.DecoLineActiveHelper
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x88
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.GameObject>checkTargets  // 0x68
  public            System.Collections.Generic.List<UnityEngine.GameObject>secondCheckTargets  // 0x70
  private           UnityEngine.UI.LayoutElement    _layoutElement  // 0x78
  private           System.Boolean                  m_oriIgnoreLayout  // 0x80
PROPERTIES:
  tickOption  get=0x04413130
METHODS:
  RVA=0x0B085924  token=0x6000117  System.Void Reset()
  RVA=0x045E6330  token=0x6000118  System.Void OnEnable()
  RVA=0x033F0700  token=0x6000119  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B08599C  token=0x600011A  System.Void Sync()
  RVA=0x0B0859D0  token=0x600011B  System.Boolean _HasActiveTarget(System.Collections.Generic.List<UnityEngine.GameObject> targets)
  RVA=0x0B085A9C  token=0x600011C  System.Void _ToggleVisible(System.Boolean isVisible)
  RVA=0x039C2B00  token=0x600011D  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600011E  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x600011F  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x6000120  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.Empty4Raycast
TYPE:  class
TOKEN: 0x2000033
SIZE:  0xE8
EXTENDS: UnityEngine.UI.MaskableGraphic
FIELDS:
METHODS:
  RVA=0x0B085BDC  token=0x6000121  System.Void .ctor()
  RVA=0x0B085B70  token=0x6000122  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x0B085BD4  token=0x6000123  System.Void <>iFixBaseProxy_OnPopulateMesh(UnityEngine.UI.VertexHelper P0)
END_CLASS

CLASS: Beyond.UI.FitAspectRatioByFOV
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: IScreenSizeListener
FIELDS:
  public            System.Single                   referenceAspect  // 0x18
  private           System.Single                   m_referenceVerticalFOV  // 0x1c
  private           Cinemachine.CinemachineVirtualCameram_virtualCamera  // 0x20
  private           System.Single                   m_currentAspect  // 0x28
  private           System.Single                   _maxFov  // 0x2c
  private           System.Single                   m_overrideMaxFov  // 0x30
PROPERTIES:
  maxFov  get=0x0B085C94  set=0x0B085CF4
METHODS:
  RVA=0x0B085BFC  token=0x6000126  System.Void Start()
  RVA=0x03C035D0  token=0x6000127  System.Void OnEnable()
  RVA=0x03C03560  token=0x6000128  System.Void OnDisable()
  RVA=0x03C03640  token=0x6000129  System.Void OnScreenSizeChanged(System.Int32 width, System.Int32 height)
  RVA=0x03C03700  token=0x600012A  System.Void _AdjustFOV()
  RVA=0x04D398A0  token=0x600012B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.FixRotation
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x78
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            UnityEngine.Vector3             rotation  // 0x68
PROPERTIES:
  tickOption  get=0x0465E4E0
METHODS:
  RVA=0x03160F10  token=0x600012D  System.Void LateTick(System.Single deltaTime)
  RVA=0x04CE63F0  token=0x600012E  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600012F  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000130  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.FullScreenSceneBlurMarker
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Int32                    s_nextId  // static @ 0x0
  public    static  System.Action<System.Int32,Beyond.UI.FullScreenSceneBlurMarker.State,System.Boolean,System.Boolean>s_onFullScreenSceneBlurMarkerStateChanged  // static @ 0x8
  private           System.Int32                    <id>k__BackingField  // 0x18
  public            System.Boolean                  useWhiteBlur  // 0x1c
  public            System.Boolean                  useSceneColorPS  // 0x1d
  public            System.Boolean                  thisLuaPanelHidesCamera  // 0x1e
  private           System.Boolean                  m_didNotifyEnable  // 0x1f
PROPERTIES:
  id  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x048AE840  token=0x6000131  System.Int32 _GetNextId()
  RVA=0x048AE7D0  token=0x6000134  System.Void Awake()
  RVA=0x04701DA0  token=0x6000135  System.Void OnEnable()
  RVA=0x04701D00  token=0x6000136  System.Void OnDisable()
  RVA=0x04CADED0  token=0x6000137  System.Void OnDestroy()
  RVA=0x04CD97C0  token=0x6000138  System.Void .ctor()
  RVA=0x04D7A790  token=0x6000139  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.InputBindingGroupNaviDecorator
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x198
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.Input.InputBindingGroupMonoTargetm_inputBindingGroup  // 0x188
  public            UnityEngine.Events.UnityEvent<System.Boolean>onGroupSetAsNaviTarget  // 0x190
METHODS:
  RVA=0x046B4BA0  token=0x600013A  System.Void _SetSelfEnabledStateIfNecessary()
  RVA=0x037D5880  token=0x600013B  System.Void Awake()
  RVA=0x046B4B20  token=0x600013C  System.Void OnEnable()
  RVA=0x0485C6F0  token=0x600013D  System.Void OnDestroy()
  RVA=0x0B0869EC  token=0x600013E  System.Void _OnSetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode)
  RVA=0x0485C730  token=0x600013F  System.Void ClearComponent()
  RVA=0x047D96D0  token=0x6000140  System.Void .ctor()
  RVA=0x0B0869D4  token=0x6000141  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0869E4  token=0x6000142  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0869DC  token=0x6000143  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x041E1670  token=0x6000144  System.Void <>iFixBaseProxy__OnSetAsNaviTarget(System.Boolean P0, System.Boolean P1)
END_CLASS

CLASS: Beyond.UI.IUIClearable
TYPE:  interface
TOKEN: 0x2000039
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000145  System.Void ClearComponent()
END_CLASS

CLASS: Beyond.UI.UILoopScrollRect
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x100
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IScrollHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.RectTransform       _viewport  // 0x68
  private           Beyond.UI.ScrollDirection       _scrollDirection  // 0x70
  private           UnityEngine.UI.ScrollRect.MovementType_movementType  // 0x74
  private           System.Single                   _elasticity  // 0x78
  private           System.Single                   _scrollSensitivity  // 0x7c
  private           System.Single                   _dragSensitivity  // 0x80
  private           System.Boolean                  _inertia  // 0x84
  private           System.Single                   _decelerationRate  // 0x88
  private           Beyond.UI.UILoopScrollRect.Snap _snap  // 0x90
  private           Beyond.UI.UIToggle              _scrollToggle  // 0x98
  private           System.String                   _audioOnClick  // 0xa0
  private           System.String                   _audioOnScrollTo  // 0xa8
  private           System.String                   _audioAutoBack  // 0xb0
  private   readonly Beyond.UI.UILoopScrollRect.AutoScrollStatem_autoScrollState  // 0xb8
  private           System.Action<System.Single>    m_onValueChanged  // 0xc0
  private           System.Action<System.Int32>     m_onSelectionChanged  // 0xc8
  private           UnityEngine.Vector2             m_beginDragPointerPosition  // 0xd0
  private           System.Single                   m_scrollStartPosition  // 0xd8
  private           System.Single                   m_prevPosition  // 0xdc
  private           System.Single                   m_currentPosition  // 0xe0
  private           System.Int32                    m_totalCount  // 0xe4
  private           System.Int32                    m_currentIndex  // 0xe8
  private           System.Boolean                  m_hold  // 0xec
  private           System.Boolean                  m_scrolling  // 0xed
  private           System.Boolean                  m_dragging  // 0xee
  private           System.Boolean                  m_hasPlayStartAudio  // 0xef
  private           System.Single                   m_velocity  // 0xf0
  private           System.Int32                    m_lastScrollIndex  // 0xf4
  private           System.Collections.Generic.List<Beyond.UI.UIToggle>m_scrollToggles  // 0xf8
PROPERTIES:
  scrollDirection  get=0x0B08A5D4
  viewportSize  get=0x0B08A6C0
  scrollSensitivity  get=0x0B08A620  set=0x0B08A808
  Position  get=0x0B08A530  set=0x0B08A780
  tickOption  get=0x0B08A670
  currentIndex  get=0x0B08A584
METHODS:
  RVA=0x0B088A4C  token=0x600014E  System.Void ScrollTo(System.Single position, System.Single duration, Beyond.Interpolator.EEaseType easing, System.Action onComplete)
  RVA=0x0B088970  token=0x600014F  System.Void ScrollToMultiLoop(System.Single position, System.Single duration, Beyond.Interpolator.EEaseType easing, System.Action onComplete)
  RVA=0x0B088908  token=0x6000150  System.Void OnValueChanged(System.Action<System.Single> callback)
  RVA=0x0B0888A0  token=0x6000151  System.Void OnSelectionChanged(System.Action<System.Int32> callback)
  RVA=0x0B0887CC  token=0x6000152  System.Void JumpTo(System.Int32 index)
  RVA=0x0B088B28  token=0x6000153  System.Void SetTotalCount(System.Int32 totalCount)
  RVA=0x0B088708  token=0x6000154  Beyond.UI.UILoopScrollRect.MovementDirection GetMovementDirection(System.Int32 sourceIndex, System.Int32 destIndex)
  RVA=0x0B089554  token=0x6000155  System.Void UnityEngine.EventSystems.IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0895D8  token=0x6000156  System.Void UnityEngine.EventSystems.IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0896A0  token=0x6000157  System.Void UnityEngine.EventSystems.IScrollHandler.OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0891B4  token=0x6000158  System.Void UnityEngine.EventSystems.IBeginDragHandler.OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B08931C  token=0x6000159  System.Void UnityEngine.EventSystems.IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0894E8  token=0x600015A  System.Void UnityEngine.EventSystems.IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B088B90  token=0x600015B  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0886C4  token=0x600015C  System.Void ClearComponent()
  RVA=0x0B08993C  token=0x600015D  System.Single _CalculateOffset(System.Single position)
  RVA=0x0B08A0CC  token=0x600015E  System.Void _ScrollTo(System.Single position, System.Single duration, Beyond.Interpolator.EasingFunction easingFunction, System.Action onComplete)
  RVA=0x0B089F24  token=0x600015F  System.Void _ScrollToMultiLoop(System.Single position, System.Single duration, Beyond.Interpolator.EasingFunction easingFunction, System.Action onComplete)
  RVA=0x0B08A2C4  token=0x6000160  System.Void _UpdatePosition(System.Single position)
  RVA=0x0B089A78  token=0x6000161  System.Void _PlayEndAudio()
  RVA=0x0B089820  token=0x6000162  System.Single _CalculateMovementAmount(System.Single sourcePosition, System.Single destPosition)
  RVA=0x0B0899D4  token=0x6000163  System.Single _CircularPosition(System.Single p, System.Int32 size)
  RVA=0x0B089E80  token=0x6000164  System.Single _RubberDelta(System.Single overStretching, System.Single viewSize)
  RVA=0x0B08A3BC  token=0x6000165  System.Void _UpdateSelection(System.Int32 index)
  RVA=0x0B089AFC  token=0x6000166  System.Void _RefreshScrollToggleCount(System.Int32 count)
  RVA=0x0B089D9C  token=0x6000167  System.Void _RefreshToggle()
  RVA=0x0B08A454  token=0x6000168  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000169  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x600016A  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UILoopScrollView
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x108
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.GameObject          _cellTemplate  // 0x68
  private           Beyond.UI.UILoopScrollRect      _scrollRect  // 0x70
  private           UnityEngine.RectTransform       _cellContainer  // 0x78
  private           System.Boolean                  _animPosition  // 0x80
  private           System.String                   _scrollClipName  // 0x88
  private           System.Single                   _cellInterval  // 0x90
  private           System.Single                   _scrollOffset  // 0x94
  private           System.Single                   _notChooseScale  // 0x98
  private           System.Single                   _fixInterval  // 0x9c
  protected         System.Single                   _tweenDuration  // 0xa0
  protected         Beyond.Interpolator.EEaseType   _tweenEase  // 0xa4
  protected         Beyond.Input.InputBindingGroupMonoTarget_scrollBindingGroup  // 0xa8
  protected         System.Boolean                  _stickScrollIsLeft  // 0xb0
  protected         System.Boolean                  _stickScrollEnableArrow  // 0xb1
  protected         System.Single                   _stickScrollTolerance  // 0xb4
  private           Beyond.UI.UISelectableNaviGroup _naviGroup  // 0xb8
  private           System.Boolean                  m_initialized  // 0xc0
  private           System.Single                   m_currentPosition  // 0xc4
  private           System.Single                   m_cellSize  // 0xc8
  private           System.Single                   m_notChooseCellSize  // 0xcc
  private           System.Single                   m_notChooseInterval  // 0xd0
  private           System.Collections.Generic.List<Beyond.UI.UILoopScrollView.Cell>m_pool  // 0xd8
  private           System.Boolean                  m_controllerInited  // 0xe0
  private           System.Single                   m_stickScrollTime  // 0xe4
  private           System.Single                   m_nextStickStepTime  // 0xe8
  private           System.Int32                    m_nextStickStepTimeIndex  // 0xec
  private   static  System.Single[]                 s_stickScrollDuration  // static @ 0x0
  private           System.Int32                    <count>k__BackingField  // 0xf0
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0xf8
  public            Beyond.DisposedUnityEvent<System.Int32>onScrollEnd  // 0x100
PROPERTIES:
  count  get=0x04D885B0  set=0x04D88610
  controllerStickScrollEnabled  get=0x0B0BCD74
  tickOption  get=0x0B0BCE14
METHODS:
  RVA=0x0B0BB6A8  token=0x6000173  System.Void UpdatePosition(System.Single position)
  RVA=0x0B0BB264  token=0x6000174  System.Void OnScrollEnd(System.Int32 index)
  RVA=0x0B0BB370  token=0x6000175  System.Void ScrollToIndex(System.Int32 index, System.Boolean fastMode)
  RVA=0x0B0BB624  token=0x6000176  System.Void UpdateCount(System.Int32 newCount)
  RVA=0x0B0BB5C0  token=0x6000177  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0BAE68  token=0x6000178  UnityEngine.GameObject GetCurrentCell()
  RVA=0x0B0BB208  token=0x6000179  System.Void OnRelease()
  RVA=0x0B0BADC8  token=0x600017A  System.Void ClearComponent()
  RVA=0x0B0BB1AC  token=0x600017B  System.Void OnEnable()
  RVA=0x0B0BAFF0  token=0x600017C  System.Void Initialize()
  RVA=0x0B0BC8B8  token=0x600017D  System.Void _UpdatePosition(System.Single position, System.Boolean forceRefresh)
  RVA=0x0B0BB9A0  token=0x600017E  System.Void _OnScrollEnd(System.Int32 index)
  RVA=0x0B0BB2C8  token=0x600017F  System.Void OnUpdateCell(Beyond.UI.UILoopScrollView.Cell cell, System.Int32 index)
  RVA=0x0B0BBA2C  token=0x6000180  System.Void _ResizePool(System.Single firstPosition)
  RVA=0x0B0BB79C  token=0x6000181  System.Int32 _GetNeedCount(System.Single firstPosition)
  RVA=0x0B0BC440  token=0x6000182  System.Void _UpdateCells(System.Single firstPosition, System.Int32 firstIndex, System.Boolean forceRefresh)
  RVA=0x0B0BC274  token=0x6000183  System.Void _UpdateCellsAnimationPosition(System.Single firstPosition, System.Int32 firstIndex, System.Boolean forceRefresh)
  RVA=0x0B0BB87C  token=0x6000184  System.Void _InitController()
  RVA=0x0B0BBE00  token=0x6000185  System.Void _ScrollToNaviTarget(UnityEngine.UI.Selectable target)
  RVA=0x0B0BB414  token=0x6000186  System.Void ScrollToObject(UnityEngine.GameObject go)
  RVA=0x0B0BBCDC  token=0x6000187  System.Void _ScrollToCell(Beyond.UI.UILoopScrollView.Cell cell)
  RVA=0x0B0BBF50  token=0x6000188  System.Void _TickRefreshControllerStickScroll(System.Single deltaTime)
  RVA=0x0B0BBEB4  token=0x6000189  System.Void _ScrollToNext(System.Boolean positive)
  RVA=0x0B0BB710  token=0x600018A  System.Int32 _CircularIndex(System.Int32 i, System.Int32 size)
  RVA=0x0B0BCC0C  token=0x600018B  System.Void .ctor()
  RVA=0x0B0BCAFC  token=0x600018C  System.Void .cctor()
  RVA=0x069AFD0C  token=0x600018D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x600018E  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x069AFD04  token=0x600018F  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x01538B40  token=0x6000190  System.Void <>iFixBaseProxy_OnEnable()
END_CLASS

CLASS: Beyond.UI.LuaPanel
TYPE:  class
TOKEN: 0x2000040
SIZE:  0xB8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public    static  System.Collections.Generic.Dictionary<System.String,Beyond.UI.LuaPanel>s_openedLuaPanels  // static @ 0x0
  private           System.Boolean                  _isPCOnlyPanel  // 0x18
  private           System.Boolean                  _isControllerPanel  // 0x19
  public            System.Boolean                  inited  // 0x1a
  private           System.Boolean                  _isWorldUI  // 0x1b
  private           UnityEngine.Vector3             _uiRot  // 0x1c
  private           System.Boolean                  _blockWhileAnim  // 0x28
  private           System.Int32                    <panelLevel>k__BackingField  // 0x2c
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x30
  private           Beyond.Audio.AudioUIUtil.UIAudioType_uiAudioType  // 0x38
  private           UnityEngine.RectTransform       <rectTransform>k__BackingField  // 0x40
  private           UnityEngine.Canvas              <canvas>k__BackingField  // 0x48
  public            System.Collections.Generic.HashSet<Beyond.UI.UISortingOrder>sortingOrderComps  // 0x50
  private           UnityEngine.Camera              <uiCamera>k__BackingField  // 0x58
  private           System.Single                   <planeDistance>k__BackingField  // 0x60
  private           System.Int32                    <panelId>k__BackingField  // 0x64
  private           System.String                   <panelName>k__BackingField  // 0x68
  private           System.Boolean                  m_showRot  // 0x70
  private           Beyond.Input.InputBindingGroupMonoTargetm_inputBindingGroupMonoTarget  // 0x78
  private           UnityEngine.UI.GraphicRaycaster[]m_graphicRaycasters  // 0x80
  private           System.Boolean                  m_started  // 0x88
  private           UnityEngine.Coroutine           m_animationInCoroutine  // 0x90
  private           System.Boolean                  m_waitAutoAnimInFinished  // 0x98
  public            Beyond.DisposedUnityEvent       onAnimationInFinished  // 0xa0
  private           System.Boolean                  m_animationInFinished  // 0xa8
  private           System.Collections.Generic.HashSet<UnityEngine.GameObject>m_animationInWaitTargets  // 0xb0
PROPERTIES:
  blockWhileAnim  get=0x04505A80
  panelLevel  get=0x04D86710  set=0x04D86720
  rectTransform  get=0x04D85A60  set=0x042B4AE0
  canvas  get=0x04D86200  set=0x035A41A0
  uiCamera  get=0x02E56440  set=0x036E5590
  planeDistance  get=0x04D88480  set=0x04D884A0
  panelId  get=0x04D88110  set=0x04D88120
  panelName  get=0x04D86230  set=0x0485F170
  IsWorldUI  get=0x0B0B64B0  set=0x0B0B6598
  isPCOnlyPanel  get=0x031E9FC0
  isControllerPanel  get=0x0B0B654C
  animationInFinished  get=0x0B0B64FC
METHODS:
  RVA=0x037D3920  token=0x60001AE  System.Void Awake()
  RVA=0x037D39E0  token=0x60001AF  System.Void _TrySetRot()
  RVA=0x037D4BA0  token=0x60001B0  System.Void AddSortingOrderComp(Beyond.UI.UISortingOrder comp)
  RVA=0x045ECD00  token=0x60001B1  System.Void RemoveSortingOrderComp(Beyond.UI.UISortingOrder comp)
  RVA=0x045C5170  token=0x60001B2  System.Void Start()
  RVA=0x038D3A20  token=0x60001B3  System.Void OnEnable()
  RVA=0x038D39B0  token=0x60001B4  System.Void OnDisable()
  RVA=0x046581C0  token=0x60001B5  System.Void OnDestroy()
  RVA=0x038D3AD0  token=0x60001B6  System.Collections.IEnumerator _AutoAnimationInFinish()
  RVA=0x032E9FB0  token=0x60001B8  System.Boolean InformAnimationInStart(UnityEngine.GameObject target)
  RVA=0x03545CC0  token=0x60001B9  System.Void InformAnimationInEnd(UnityEngine.GameObject target, System.Boolean forceEnd)
  RVA=0x03545D40  token=0x60001BA  System.Void _TryCallAnimationInFinished(System.Boolean forceFinished)
  RVA=0x034366E0  token=0x60001BB  System.Void BlockAllInput()
  RVA=0x034365E0  token=0x60001BC  System.Void RecoverAllInput()
  RVA=0x04658200  token=0x60001BD  System.Void ClearComponent()
  RVA=0x03EE9E50  token=0x60001BE  System.Void .ctor()
  RVA=0x04D17910  token=0x60001BF  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.MouseHoverTipsPosHelper
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x80
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.RectOffset          _padding  // 0x68
  private           Beyond.UI.LuaPanel              m_panel  // 0x70
  private           UnityEngine.RectTransform       m_rectTransform  // 0x78
PROPERTIES:
  tickOption  get=0x0B0B6BAC
  panel  get=0x0B0B6B1C
METHODS:
  RVA=0x0B0B6650  token=0x60001C8  System.Void OnAwake()
  RVA=0x0B0B6740  token=0x60001C9  System.Void OnEnable()
  RVA=0x0B0B65F0  token=0x60001CA  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0B679C  token=0x60001CB  System.Void UpdatePosition()
  RVA=0x039C2B00  token=0x60001CC  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60001CD  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60001CE  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x60001CF  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x60001D0  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.NaviToThisGroupOnEnable
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UISelectableNaviGroup target  // 0x18
METHODS:
  RVA=0x0B0B7050  token=0x60001D1  System.Void Reset()
  RVA=0x0B0B6F04  token=0x60001D2  System.Void Awake()
  RVA=0x0B0B6FB4  token=0x60001D3  System.Void OnEnable()
  RVA=0x05393520  token=0x60001D4  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.NonDrawingGraphic
TYPE:  class
TOKEN: 0x2000044
SIZE:  0xB8
EXTENDS: UnityEngine.UI.Graphic
IMPLEMENTS: UnityEngine.ICanvasRaycastFilter
FIELDS:
  public            Beyond.UI.NonDrawingGraphic.ShapeTypeshapeType  // 0xb0
PROPERTIES:
  Radius  get=0x03094480
METHODS:
  RVA=0x0440A720  token=0x60001D6  System.Void SetMaterialDirty()
  RVA=0x0430BC30  token=0x60001D7  System.Void SetVerticesDirty()
  RVA=0x0B0B70D0  token=0x60001D8  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x03093330  token=0x60001D9  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x030943A0  token=0x60001DA  System.Boolean _IsRaycastLocationValidCircle(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x03695540  token=0x60001DB  System.Void .ctor()
  RVA=0x0B0B7134  token=0x60001DC  System.Void <>iFixBaseProxy_SetMaterialDirty()
  RVA=0x0B0B713C  token=0x60001DD  System.Void <>iFixBaseProxy_SetVerticesDirty()
  RVA=0x0B085BD4  token=0x60001DE  System.Void <>iFixBaseProxy_OnPopulateMesh(UnityEngine.UI.VertexHelper P0)
END_CLASS

CLASS: Beyond.UI.NotchAdapter
TYPE:  sealed class
TOKEN: 0x2000046
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Vector2             selfMarginSize  // 0x18
  public            System.Collections.Generic.List<UnityEngine.RectTransform>leftSideUIList  // 0x20
  public            System.Collections.Generic.List<UnityEngine.RectTransform>rightSideUIList  // 0x28
  public            System.Collections.Generic.List<UnityEngine.RectTransform>bottomSideUIList  // 0x30
  public            System.Collections.Generic.List<UnityEngine.RectTransform>fullHorizontalStretchUIList  // 0x38
  public            System.Boolean                  alwaysFindSideUI  // 0x40
  private           UnityEngine.UI.CanvasScaler     m_canvasScaler  // 0x48
  private           Beyond.UI.UICanvasScaleHelper   m_notchHost  // 0x50
METHODS:
  RVA=0x03BFD4E0  token=0x60001DF  System.Void Awake()
  RVA=0x03BFD3D0  token=0x60001E0  System.Void OnDestroy()
  RVA=0x03BFCF90  token=0x60001E1  System.Void FindAllSideUI(System.Boolean needClear)
  RVA=0x0B0B7144  token=0x60001E2  System.Void ApplyNotch()
  RVA=0x03BFD570  token=0x60001E3  System.Void ApplyNotch(UnityEngine.Vector2 curMarginSize)
  RVA=0x03BFCEF0  token=0x60001E4  System.Void ClearList()
  RVA=0x03BFDB20  token=0x60001E5  System.Void ClearNullRef()
  RVA=0x0B0B71AC  token=0x60001E6  System.Void UnApplyNotch()
  RVA=0x03BFD350  token=0x60001E7  System.Void ApplyNewNotch()
  RVA=0x03BFDCC0  token=0x60001E8  System.Void _UpdateMargeSizeFromNotchInfo(System.Boolean forceUpdate)
  RVA=0x03BFD310  token=0x60001E9  System.Void _OnCanvasChanged()
  RVA=-1  // generic def  token=0x60001EA  System.Void _RemoveNullRef(System.Collections.Generic.List<T> list)
  RVA=0x03BFC330  token=0x60001EB  System.Void _TraverseUI(UnityEngine.RectTransform root)
  RVA=0x03BFC7D0  token=0x60001EC  System.Void _TryAddToSideUI(UnityEngine.RectTransform rect, Beyond.UI.CustomNotchAdapt custom)
  RVA=0x03694470  token=0x60001ED  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.ParallaxHelper
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<Beyond.UI.ParallaxHelper.ParallaxConfig>_configs  // 0x18
  private           System.Single                   _maxOffset  // 0x20
  private           System.Single                   m_curValue  // 0x24
PROPERTIES:
  curValue  get=0x0B0B75F0  set=0x0B0B7640
METHODS:
  RVA=0x0B0B721C  token=0x60001F3  System.Void InitConfigs()
  RVA=0x0B0B7598  token=0x60001F4  System.Void ResetValue()
  RVA=0x0B0B73A4  token=0x60001F5  System.Void RefreshPos()
  RVA=0x05393520  token=0x60001F6  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.PercentNumberTweener
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x88
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIText                _text  // 0x68
  private           System.String                   _format  // 0x70
  private           Beyond.UI.UIImage               _progressImg  // 0x78
  private           System.Int32                    m_cacheValue  // 0x80
PROPERTIES:
  tickOption  get=0x0B0B78EC
METHODS:
  RVA=0x0B0B781C  token=0x60001F9  System.Void Reset()
  RVA=0x0B0B77BC  token=0x60001FA  System.Void OnEnable()
  RVA=0x0B0B76A4  token=0x60001FB  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0B789C  token=0x60001FC  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60001FD  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x60001FE  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x60001FF  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIRecycleLayoutAdapter
TYPE:  abstract class
TOKEN: 0x200004B
SIZE:  0x18
FIELDS:
  private           Beyond.UI.UIRecycleLayoutGroup.IViewHandler<viewHandler>k__BackingField  // 0x10
PROPERTIES:
  viewHandler  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x0B0BCEB0  token=0x6000200  System.Void Init(Beyond.UI.UIRecycleLayoutGroup.IViewHandler handler)
  RVA=0x0B0BCE64  token=0x6000203  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild()
  RVA=-1  // abstract  token=0x6000204  System.Void OnInit()
  RVA=0x041E1670  token=0x6000205  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRecycleLayoutSimpleHolder`1
TYPE:  abstract class
TOKEN: 0x2000050
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIScrollRect          _scrollRect  // 0x0
  protected         System.Single                   _scrollDuration  // 0x0
  protected         DG.Tweening.Ease                _scrollEase  // 0x0
  protected         System.Boolean                  _enableLoopWrappedNavigation  // 0x0
  private           System.Boolean                  m_isInited  // 0x0
  protected         Beyond.UI.UIRecycleLayoutSimpleHolder.SimpleAdapter<TVirtualView>m_simpleAdapter  // 0x0
  protected         Beyond.UI.UIRecycleLayoutGroup  m_layoutGroup  // 0x0
  public            Beyond.DisposedUnityEvent       doOnceAfterLayout  // 0x0
  private           System.Collections.Generic.List<UnityEngine.UI.Selectable>m_wrapNaviSelectables  // 0x0
  protected         DG.Tweening.Tween               m_scrollTween  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000218  System.Void _InitIfNot()
  RVA=-1  // not resolved  token=0x6000219  System.Void OnAwake()
  RVA=-1  // not resolved  token=0x600021A  System.Void _AfterLayout()
  RVA=-1  // not resolved  token=0x600021B  System.Void _WrapNavigation(UnityEngine.UI.NaviDirection dir)
  RVA=-1  // abstract  token=0x600021C  System.Void OnAdapterInit()
  RVA=-1  // not resolved  token=0x600021D  System.Void ScrollTo(System.Int32 index, System.Boolean fastMode)
  RVA=-1  // not resolved  token=0x600021E  System.Void ClearScrollTweenIfNeeded()
  RVA=-1  // not resolved  token=0x600021F  System.Void ScrollToInternal(System.Single normalizedPosition, System.Boolean fastMode)
  RVA=-1  // not resolved  token=0x6000220  UnityEngine.GameObject GetView(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000221  System.Void NotifyAllSizeChanged()
  RVA=-1  // not resolved  token=0x6000222  System.Void NotifySizeChanged(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000223  System.Void NotifySizeChanged(TVirtualView virtualView)
  RVA=-1  // not resolved  token=0x6000224  System.Void RemoveView(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000225  System.Void RemoveAllViews()
  RVA=-1  // not resolved  token=0x6000226  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRecycleLayoutGroup
TYPE:  class
TOKEN: 0x2000053
SIZE:  0xC8
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.UI.ILayoutElement Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.UI.UIRecycleLayoutGroup.Direction_direction  // 0x68
  private           UnityEngine.RectTransform       _viewport  // 0x70
  private           UnityEngine.RectTransform       _content  // 0x78
  private           System.Single                   _notCachedBufferSize  // 0x80
  private           System.Int32                    _layoutPriority  // 0x84
  private           Beyond.UI.UIRecycleLayoutGroup.Padding_padding  // 0x88
  private           System.Single                   _spacing  // 0x98
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIRecycleLayoutGroup.LayoutMeta>m_layoutMetaMap  // 0xa0
  private           Beyond.UI.UIRecycleLayoutAdapterm_adapter  // 0xa8
  private           Beyond.UI.UIRecycleLayoutGroup.ViewMgrm_viewMgr  // 0xb0
  private           System.Single                   <sizeOnAxis>k__BackingField  // 0xb8
  public            Beyond.DisposedUnityEvent       onLayoutUpdated  // 0xc0
PROPERTIES:
  tickOption  get=0x0B0BEBB0
  adapter  get=0x0B0BE6E0
  viewMgr  get=0x0B0BEC00
  sizeOnAxis  get=0x04DA32D0  set=0x04D87E30
  spacing  get=0x0B0BEB5C
  padding  get=0x0B0BEA34
  minWidth  get=0x0B0BE900
  minHeight  get=0x0B0BE8B4
  flexibleWidth  get=0x0B0BE818
  flexibleHeight  get=0x0B0BE7CC
  layoutPriority  get=0x0B0BE864
  preferredWidth  get=0x0B0BEAFC
  preferredHeight  get=0x0B0BEA9C
  paddingFront  get=0x0B0BE9C0
  paddingBack  get=0x0B0BE94C
  content  get=0x0B0BE77C
METHODS:
  RVA=0x0B0BCF20  token=0x6000245  System.Void CalculateLayoutInputHorizontal()
  RVA=0x0B0BCF64  token=0x6000246  System.Void CalculateLayoutInputVertical()
  RVA=0x0B0BD480  token=0x600024B  System.Void _ApplyLayoutMeta(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view, Beyond.UI.UIRecycleLayoutGroup.LayoutMeta meta)
  RVA=0x0B0BDD20  token=0x600024C  UnityEngine.Vector2 _GetVisibleRange(UnityEngine.Bounds viewBound)
  RVA=0x0B0BD9B4  token=0x600024D  UnityEngine.Bounds _GetElementBoundsFromMeta(Beyond.UI.UIRecycleLayoutGroup.LayoutMeta meta)
  RVA=0x0B0BD3D0  token=0x600024E  Beyond.UI.UIRecycleLayoutAdapter SetAdapter(Beyond.UI.UIRecycleLayoutAdapter adapter)
  RVA=0x0B0BCFA8  token=0x600024F  System.Void ClearComponent()
  RVA=0x0B0BD038  token=0x6000250  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0BD098  token=0x6000251  System.Void OnLateTick()
  RVA=0x0B0BDFB8  token=0x6000252  System.Void _RebuildAllViews()
  RVA=0x0B0BE330  token=0x6000253  System.Void _UpdateViews(System.Int32 fromIndex)
  RVA=0x0B0BDE30  token=0x6000254  System.Boolean _InsertView(System.Int32 index, Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0BE0C8  token=0x6000255  System.Boolean _RemoveView(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0BE26C  token=0x6000256  System.Void _UpdateViewsFrom(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0BDC50  token=0x6000257  System.Single _GetElementPosByIndex(System.Int32 index)
  RVA=0x0B0BD880  token=0x6000258  UnityEngine.Bounds _GetElementBoundsByIndex(System.Int32 index)
  RVA=0x0B0BDB9C  token=0x6000259  System.Int32 _GetElementIndex(Beyond.UI.UIRecycleLayoutAdapter.IVirtualView view)
  RVA=0x0B0BE070  token=0x600025B  System.Void _RefreshLayout()
  RVA=0x0B0BE614  token=0x600025C  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600025D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x600025E  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIRecycleMultiHolder
TYPE:  class
TOKEN: 0x200005C
SIZE:  0xD8
EXTENDS: Beyond.UI.UIRecycleLayoutSimpleHolder`1
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Collections.Generic.List<Beyond.UI.UIRecycleMultiHolder.Config>_configs  // 0xa8
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0xb0
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onDetachCell  // 0xb8
  public            System.Func<System.Int32,System.Single>getCellDefaultSize  // 0xc0
  public            System.Func<System.Int32,System.String>getCellPrefabKey  // 0xc8
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.GameObject>m_prefabMap  // 0xd0
METHODS:
  RVA=0x0B0BEE90  token=0x600028C  Beyond.UI.UIRecycleMultiHolder.MultiVirtualView GetVirtualView(System.Int32 index)
  RVA=0x0B0BF340  token=0x600028D  System.Void UpdateCount(System.Int32 newCount, System.Boolean setTop)
  RVA=0x0B0BF3C0  token=0x600028E  System.Void UpdateCount(System.Int32 newCount, System.Int32 fastScrollToIndex)
  RVA=0x0B0BEF1C  token=0x600028F  System.Void InsertView(System.Int32 index, System.String key, System.Single initSize)
  RVA=0x0B0BECB0  token=0x6000290  System.Void AddView(System.String key, System.Single initSize)
  RVA=0x0B0BF010  token=0x6000291  System.Void OnAdapterInit()
  RVA=0x0B0BED90  token=0x6000292  System.Void ClearComponent()
  RVA=0x0B0BF5A4  token=0x6000293  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRecycleSingleHolder
TYPE:  class
TOKEN: 0x200005F
SIZE:  0xD0
EXTENDS: Beyond.UI.UIRecycleLayoutSimpleHolder`1
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Int32                    m_count  // 0xa8
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0xb0
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onDetachCell  // 0xb8
  public            System.Func<System.Int32,System.Single>getCellDefaultSize  // 0xc0
  private           UnityEngine.GameObject          _cellTemplate  // 0xc8
METHODS:
  RVA=0x0B0BF8CC  token=0x600029B  Beyond.UI.UIRecycleSingleHolder.SimpleVirtualView GetVirtualView(System.Int32 index)
  RVA=0x0B0BFA9C  token=0x600029C  System.Void UpdateCount(System.Int32 newCount, System.Boolean setTop)
  RVA=0x0B0BFB1C  token=0x600029D  System.Void UpdateCount(System.Int32 newCount, System.Int32 fastScrollToIndex)
  RVA=0x0B0BF958  token=0x600029E  System.Void InsertView(System.Int32 index, System.String key, System.Single initSize)
  RVA=0x0B0BF714  token=0x600029F  System.Void AddView(System.String key, System.Single initSize)
  RVA=0x0B0BFA3C  token=0x60002A0  System.Void OnAdapterInit()
  RVA=0x0B0BF7E4  token=0x60002A1  System.Void ClearComponent()
  RVA=0x0B0BFCB0  token=0x60002A2  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.SimpleUITextSingleLine
TYPE:  class
TOKEN: 0x2000061
SIZE:  0xCA8
EXTENDS: Beyond.UI.UIText
FIELDS:
  private   static  Unity.Profiling.ProfilerMarker  s_GenerateTextMarker  // static @ 0x0
  private   static  Unity.Profiling.ProfilerMarker  s_GenerateTextPhaseIMarker  // static @ 0x8
  private   static  Unity.Profiling.ProfilerMarker  s_CharacterLookupMarker  // static @ 0x10
  private   static  Unity.Profiling.ProfilerMarker  s_HandleGPOSFeaturesMarker  // static @ 0x18
  private   static  Unity.Profiling.ProfilerMarker  s_CalculateVerticesPositionMarker  // static @ 0x20
  private   static  Unity.Profiling.ProfilerMarker  s_ComputeTextMetricsMarker  // static @ 0x28
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVisibleCharacterMarker  // static @ 0x30
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVisibleCharacterMarker_1  // static @ 0x38
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVisibleCharacterMarker_2  // static @ 0x40
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVisibleCharacterMarker_3  // static @ 0x48
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVisibleCharacterMarker_4  // static @ 0x50
  private   static  Unity.Profiling.ProfilerMarker  s_HandleWhiteSpacesMarker  // static @ 0x58
  private   static  Unity.Profiling.ProfilerMarker  s_HandleHorizontalLineBreakingMarker  // static @ 0x60
  private   static  Unity.Profiling.ProfilerMarker  s_HandleVerticalLineBreakingMarker  // static @ 0x68
  private   static  Unity.Profiling.ProfilerMarker  s_SaveGlyphVertexDataMarker  // static @ 0x70
  private   static  Unity.Profiling.ProfilerMarker  s_ComputeCharacterAdvanceMarker  // static @ 0x78
  private   static  Unity.Profiling.ProfilerMarker  s_HandleCarriageReturnMarker  // static @ 0x80
  private   static  Unity.Profiling.ProfilerMarker  s_HandleLineTerminationMarker  // static @ 0x88
  private   static  Unity.Profiling.ProfilerMarker  s_SavePageInfoMarker  // static @ 0x90
  private   static  Unity.Profiling.ProfilerMarker  s_SaveProcessingStatesMarker  // static @ 0x98
  private   static  Unity.Profiling.ProfilerMarker  s_GenerateTextPhaseIIMarker  // static @ 0xa0
  private   static  Unity.Profiling.ProfilerMarker  s_GenerateTextPhaseIIIMarker  // static @ 0xa8
  public            System.Boolean                  m_b  // 0xc98
  private           System.Action<TMPro.TMP_TextInfo>OnPreRenderText  // 0xca0
EVENTS:
  OnPreRenderText  add=add_OnPreRenderText  remove=remove_OnPreRenderText
METHODS:
  RVA=0x0B0903B8  token=0x60002A9  System.Void GenerateTextMesh()
  RVA=0x0B0B7E34  token=0x60002AC  System.Void .ctor()
  RVA=0x0B0B7944  token=0x60002AD  System.Void .cctor()
  RVA=0x0B0B793C  token=0x60002AE  System.Void <>iFixBaseProxy_GenerateTextMesh()
END_CLASS

CLASS: Beyond.UI.ToastList
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.GameObject          _cellTemplate  // 0x18
  private           System.Single                   _showDuration  // 0x20
  private           System.Single                   _delay  // 0x24
  private           System.Int32                    _maxShowingCount  // 0x28
  private           System.Boolean                  <inAnimation>k__BackingField  // 0x2c
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0x30
  public            Beyond.DisposedUnityEvent       onAllToastFinished  // 0x38
  private           System.Int32                    m_totalCount  // 0x40
  private           System.Int32                    m_maxFinishedIndex  // 0x44
  private           System.Int32                    m_maxShowingIndex  // 0x48
  private           System.Int32                    m_waitingCount  // 0x4c
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.ToastList.CellInfo>m_showingCells  // 0x50
  private           System.Collections.Generic.List<Beyond.UI.ToastList.CellInfo>m_cachedCells  // 0x58
PROPERTIES:
  inAnimation  get=0x04D869F0  set=0x04D86A00
METHODS:
  RVA=0x0B0B83EC  token=0x60002B1  System.Void Awake()
  RVA=0x0B0B86A8  token=0x60002B2  System.Void OnDisable()
  RVA=0x0B0B87C4  token=0x60002B3  Beyond.UI.ToastList.CellInfo _GetCell()
  RVA=0x0B0B86F8  token=0x60002B4  System.Void _CacheCell(Beyond.UI.ToastList.CellInfo cell)
  RVA=0x0B0B8298  token=0x60002B5  System.Void AddToast(System.Int32 count, System.Int32 startIndex)
  RVA=0x0B0B8BAC  token=0x60002B6  System.Void _StartBatch(System.Int32 batchCount)
  RVA=0x0B0B8AF0  token=0x60002B7  System.Collections.IEnumerator _ShowCell(System.Int32 delayIndex, Beyond.UI.ToastList.CellInfo cell)
  RVA=0x0B0B8CB4  token=0x60002B8  System.Void _TryShowNewCell()
  RVA=0x0B0B8938  token=0x60002B9  System.Void _OnCellFinished(Beyond.UI.ToastList.CellInfo cell)
  RVA=0x0B0B844C  token=0x60002BA  System.Void ClearAllToast()
  RVA=0x0B0B8614  token=0x60002BB  System.Void ClearComponent()
  RVA=0x0B0B8EB0  token=0x60002BC  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UI3DScrollList
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x278
EXTENDS: Beyond.UI.UIScrollList
FIELDS:
METHODS:
  RVA=0x0B0B930C  token=0x60002C4  System.Void MoveCellTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0B0B93CC  token=0x60002C5  System.Void .ctor()
  RVA=0x0B0B93C4  token=0x60002C6  System.Void <>iFixBaseProxy_MoveCellTo(Beyond.UI.UIScrollList.Cell P0, System.Int32 P1)
END_CLASS

CLASS: Beyond.UI.UIActionKeyHint
TYPE:  class
TOKEN: 0x2000066
SIZE:  0xB8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private   static readonly UnityEngine.Color               NORMAL_COLOR  // static @ 0x0
  private   static readonly UnityEngine.Color               HIDE_COLOR  // static @ 0x10
  private   static readonly UnityEngine.Color               DISABLED_COLOR  // static @ 0x20
  private   static  System.Collections.Generic.List<Beyond.UI.UIActionKeyHint>s_keyHintCache  // static @ 0x30
  private   static  System.Boolean                  s_inited  // static @ 0x38
  public    static  System.Boolean                  s_stopCheckBindingEnabledForGuide  // static @ 0x39
  private   static  System.String                   INVALID_KEY_STR  // const
  private           Beyond.Input.IBindingView       m_target  // 0x18
  public            System.String                   actionId  // 0x20
  public            UnityEngine.Component           btnTarget  // 0x28
  public            Beyond.Input.BindingViewActionTypebindingViewActionType  // 0x30
  private           System.Boolean                  _useCustomText  // 0x34
  private           System.Boolean                  _showSecondAction  // 0x35
  private           System.Boolean                  _onlyShowOnLongPress  // 0x36
  private           System.Boolean                  _adaptForceGrayState  // 0x37
  private           UnityEngine.Transform           _content  // 0x38
  private           UnityEngine.CanvasGroup         _contentCanvasGroup  // 0x40
  private           Beyond.UI.UIImage               _icon  // 0x48
  private           Beyond.UI.UIImage               _longPressIcon  // 0x50
  private           UnityEngine.RectTransform       _pressHintNode  // 0x58
  private           Beyond.UI.UIImage               _pressHintProgressImg  // 0x60
  private           Beyond.UI.UIText                _text  // 0x68
  private           Beyond.UI.UIImage               _secondIcon  // 0x70
  private           UnityEngine.RectTransform       _secondNode  // 0x78
  private           Beyond.UI.UIImage               _modifyIcon  // 0x80
  private           UnityEngine.GameObject          _mutexGameObject  // 0x88
  private           System.Boolean                  _changeScaleOnHide  // 0x90
  public            System.Int32                    bindingId  // 0x94
  public            System.Func<System.Boolean>     checkVirtualMouseClickHintFunc  // 0x98
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0xa0
  public            System.Boolean                  autoUpdate  // 0xa8
  private           System.Boolean                  <hintHidden>k__BackingField  // 0xa9
  private           System.Int32                    m_cachedBindingId  // 0xac
  private           System.Boolean                  m_isLongPressAction  // 0xb0
  private           System.Single                   m_lastPressStartTime  // 0xb4
  private   static  System.Single                   LONG_PRESS_HINT_HIDE_TIME  // const
  private   static  System.Single                   LONG_PRESS_HINT_SHOW_MIN_VALUE  // const
PROPERTIES:
  target  get=0x02FA9540
  showCanvasGroup  get=0x0B0B9E6C
  hintHidden  get=0x02ED0880  set=0x04D90740
METHODS:
  RVA=0x033EC440  token=0x60002C7  System.Void _TryInit()
  RVA=0x049C6740  token=0x60002C8  System.Void _RefreshAllKeyHints()
  RVA=0x02FA7780  token=0x60002C9  System.Void _OnInputLateTickAll()
  RVA=0x0B0B9B60  token=0x60002CA  System.Void _OnControllerTypeChange(Beyond.DeviceInfo.ControllerType controllerType)
  RVA=0x033EC2E0  token=0x60002CF  System.Void Awake()
  RVA=0x036D2960  token=0x60002D0  System.Void OnEnable()
  RVA=0x036D27C0  token=0x60002D1  System.Void OnDisable()
  RVA=0x036D4A00  token=0x60002D2  System.Void OnDestroy()
  RVA=0x0B0B9418  token=0x60002D3  System.Void Reset()
  RVA=0x0B0B9C58  token=0x60002D4  System.Void _OnInputLateTick()
  RVA=0x036D2B50  token=0x60002D5  System.Void UpdateKeyHint()
  RVA=0x0B0B9530  token=0x60002D6  System.Void SetActionId(System.String id, System.Boolean forceUpdate)
  RVA=0x0B0B9674  token=0x60002D7  System.Void SetKeyHint(System.String actId, Beyond.Input.IBindingView hintTarget, System.Boolean forceUpdate)
  RVA=0x037D5110  token=0x60002D8  System.Void SetBindingId(System.Int32 bId, System.Boolean forceUpdate)
  RVA=0x0B0B95B0  token=0x60002D9  System.Void SetBindingId(System.Int32 bId, System.String actId, Beyond.Input.IBindingView hintTarget, System.Boolean forceUpdate)
  RVA=0x037D51D0  token=0x60002DA  System.Void _OnChangeTarget()
  RVA=0x0B0B9BDC  token=0x60002DB  System.Void _OnInputActionKeyUpdated(Beyond.EventData<System.String>& data)
  RVA=0x036D3ED0  token=0x60002DC  System.Void SetSecondActionId(System.String id, System.Boolean isPrimary)
  RVA=0x036D3FA0  token=0x60002DD  System.Void _SetSecondIcon(Beyond.Input.PlayerActionInfo actionInfo, System.Boolean isPrimary)
  RVA=0x02FA8C80  token=0x60002DE  System.Void _CheckInteractable(System.Boolean isInit)
  RVA=0x037CD3A0  token=0x60002DF  System.Boolean _IsInputEnabled()
  RVA=0x0B0B9910  token=0x60002E0  System.Boolean _IsInputGray()
  RVA=0x04814240  token=0x60002E1  System.String GetTextStr()
  RVA=0x0B0B9744  token=0x60002E2  System.Void SetText(System.String text)
  RVA=0x036D40E0  token=0x60002E3  System.Boolean GetAndSetActionIcon(Beyond.UI.UIImage iconImage, Beyond.Input.PlayerActionInfo actionInfo, System.Boolean usingController, System.Boolean& isLongPressAction, System.Boolean isPrimary)
  RVA=0x036D2AB0  token=0x60002E4  System.Boolean GetAndSetActionModifyIcon(Beyond.Input.PlayerActionInfo actionInfo, System.Boolean usingController, System.Boolean isPrimary)
  RVA=0x036D4690  token=0x60002E5  System.Boolean _SetKeyCodeSprite(Beyond.UI.UIImage iconImage, System.String iconPath)
  RVA=0x0B0B97B0  token=0x60002E6  System.Void _CheckVirtualMouseClickHint()
  RVA=0x0B0B9C80  token=0x60002E7  System.Void _UpdatePressHint()
  RVA=0x0B0B9860  token=0x60002E8  System.Void _GetLongPressInfo(System.Single& startTime, System.Single& longPressTime)
  RVA=0x037CE280  token=0x60002E9  System.String GetActionId()
  RVA=0x036D3E50  token=0x60002EA  Beyond.Input.PlayerActionInfo GetActionInfo()
  RVA=0x036D2A60  token=0x60002EB  System.Void ClearComponent()
  RVA=0x036D3DC0  token=0x60002EC  System.Void SetContentState(System.Boolean contentState)
  RVA=0x03698F40  token=0x60002ED  System.Void .ctor()
  RVA=0x04B699F0  token=0x60002EE  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIAnimationHolder
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.IAnimationClipSource
FIELDS:
  private           System.Collections.Generic.List<Beyond.UI.UIAnimationHolder.ClipHolder>_clips  // 0x18
METHODS:
  RVA=0x0B0B9F58  token=0x60002EF  UnityEngine.AnimationClip FindByName(System.String name)
  RVA=0x0B0B9EF0  token=0x60002F0  UnityEngine.AnimationClip FindByNameOrAlias(System.String nameOrAlias)
  RVA=0x0B0BA014  token=0x60002F1  UnityEngine.AnimationClip _FindByString(System.String id, System.Boolean useAlias)
  RVA=0x0B0B9FC0  token=0x60002F2  System.Void GetAnimationClips(System.Collections.Generic.List<UnityEngine.AnimationClip> results)
  RVA=0x04CB6680  token=0x60002F3  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIAnimationLoopSynchronizer
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   syncLoopAnimKey  // 0x18
  private   readonly System.Collections.Generic.List<Beyond.UI.UIAnimationWrapper>m_children  // 0x20
METHODS:
  RVA=0x0466FA60  token=0x60002F5  System.Void RegisterChild(Beyond.UI.UIAnimationWrapper child)
  RVA=0x0B0BA228  token=0x60002F6  System.Void UnregisterChild(Beyond.UI.UIAnimationWrapper child)
  RVA=0x03543CE0  token=0x60002F7  System.Single GetLoopStartPosition()
  RVA=0x04CB65F0  token=0x60002F8  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIAnimationWrapper
TYPE:  class
TOKEN: 0x200006A
SIZE:  0xD0
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.AnimationClip       _animationIn  // 0x18
  private           UnityEngine.AnimationClip       _animationInEasing  // 0x20
  private           UnityEngine.AnimationClip       _animationLoop  // 0x28
  private           UnityEngine.AnimationClip       _animationOut  // 0x30
  public            System.Boolean                  autoPlay  // 0x38
  private           System.Boolean                  _onlyOnce  // 0x39
  private           System.Boolean                  _needSyncLoopAnim  // 0x3a
  private           System.String                   _syncLoopAnimKey  // 0x40
  private           Beyond.UI.UIAnimationWrapper.FOptions_options  // 0x48
  private           UnityEngine.Animation           m_animation  // 0x50
  private           System.Collections.Generic.Dictionary<System.String,Beyond.UI.UIAnimationWrapper.AnimationRuntime>m_animPool  // 0x58
  private           Beyond.UI.UIAnimationTween      m_curTween  // 0x60
  private           System.Boolean                  m_isInAutoPlayed  // 0x68
  private           Beyond.UI.UIAnimationLoopSynchronizerm_loopSynchronizer  // 0x70
  private   static readonly System.Collections.Generic.List<Beyond.UI.UIAnimationLoopSynchronizer>s_tempSyncList  // static @ 0x0
  public            System.String                   curStateName  // 0x78
  public            Beyond.UI.UIConst.AnimationStatecurState  // 0x80
  public            Beyond.DisposedUnityEvent       onAnimationInEasingFinished  // 0x88
  private           System.Boolean                  m_needNotifyAnimationInEasingFinished  // 0x90
  private           System.Single                   m_lastPlayAnimationInStartTime  // 0x94
  private           System.Boolean                  m_started  // 0x98
  private           Beyond.UI.UIAnimationWrapper.CachedSampleClipInfom_cachedSampleClipInfo  // 0xa0
  private           System.Boolean                  m_needInformAnimationInEnd  // 0xb0
  private           Beyond.UI.LuaPanel              m_panel  // 0xb8
  private           System.String                   _audioOnOpen  // 0xc0
  private           System.String                   _audioOnClose  // 0xc8
PROPERTIES:
  animationIn  get=0x035425C0
  animationInEasing  get=0x0B0BAD28
  animationLoop  get=0x0B0BAD78
  animationOut  get=0x03D2AFA0
  curTween  get=0x03543DA0
  panel  get=0x032EA2C0
METHODS:
  RVA=0x04D79890  token=0x60002FD  System.Void SetOptions(Beyond.UI.UIAnimationWrapper.FOptions options)
  RVA=0x032ECD90  token=0x60002FF  System.Void _InitAnimation()
  RVA=0x0B0BAB08  token=0x6000300  System.Void _AnimationInLoopCheck()
  RVA=0x0B0BAA30  token=0x6000301  System.Void _AnimationAutomaticallyCheck()
  RVA=0x032EB7B0  token=0x6000302  System.Void Awake()
  RVA=0x0B0BA560  token=0x6000303  System.Boolean IsStarted()
  RVA=0x03544530  token=0x6000304  System.Void PlayOutAnimation(DG.Tweening.TweenCallback callback)
  RVA=0x0B0BA688  token=0x6000305  System.Void SetAnimationInClip(System.String clipName)
  RVA=0x0B0BA700  token=0x6000306  System.Void SetAnimationInEasingClip(System.String clipName)
  RVA=0x0B0BA778  token=0x6000307  System.Void SetAnimationLoopClip(System.String clipName)
  RVA=0x0B0BA7F0  token=0x6000308  System.Void SetAnimationOutClip(System.String clipName)
  RVA=0x0B0BA380  token=0x6000309  System.Single GetInClipLength()
  RVA=0x0B0BA4C0  token=0x600030A  System.Single GetOutClipLength()
  RVA=0x0B0BA420  token=0x600030B  System.Single GetLoopClipLength()
  RVA=0x03543B90  token=0x600030C  System.Void PlayLoopAnimation(System.Single speed, System.Boolean preserveStartPosition)
  RVA=0x032EA020  token=0x600030D  System.Void PlayInAnimation(DG.Tweening.TweenCallback callback, System.Boolean autoPlayLoop, System.Single speed)
  RVA=0x0B0BA8D0  token=0x600030E  System.Void SkipInAnimation()
  RVA=0x03CCED70  token=0x600030F  System.Void SampleToInAnimationBegin()
  RVA=0x03848BE0  token=0x6000310  System.Void SampleToInAnimationEnd()
  RVA=0x04CDC520  token=0x6000311  System.Void SampleToOutAnimationBegin()
  RVA=0x0384B800  token=0x6000312  System.Void SampleToOutAnimationEnd()
  RVA=0x035438C0  token=0x6000313  System.Void _OnInAnimationFinished(System.Boolean autoPlayLoop)
  RVA=0x03543A90  token=0x6000314  System.Void _OnInEasingAnimationFinished(System.Boolean autoPlayLoop)
  RVA=0x032EBDE0  token=0x6000315  UnityEngine.AnimationState GetClip(System.String aniName)
  RVA=0x0384A980  token=0x6000316  System.Single GetClipLength(System.String aniName)
  RVA=0x032EC900  token=0x6000317  System.Void _SampleClipAtLength(Beyond.UI.UIAnimationWrapper.AnimationRuntime runtime, System.Single position)
  RVA=0x0384A900  token=0x6000318  System.Void SampleClipAtPercent(System.String clipName, System.Single percent, System.Boolean clearTween)
  RVA=0x032EC6E0  token=0x6000319  System.Void SampleClip(System.String clipName, System.Single position, System.Boolean clearTween)
  RVA=0x032ED470  token=0x600031A  System.Void ClearTween(System.Boolean executeOnComplete)
  RVA=0x032E9F20  token=0x600031B  System.Boolean _CheckShouldAutoPlayAnimationIn()
  RVA=0x041CE700  token=0x600031C  System.Void Start()
  RVA=0x032E9D80  token=0x600031D  System.Void OnEnable()
  RVA=0x032EBD10  token=0x600031E  System.Void OnDisable()
  RVA=0x032EE8F0  token=0x600031F  System.Void OnDestroy()
  RVA=0x0B0BA2B4  token=0x6000320  System.Boolean CheckStopped()
  RVA=0x0B0BA5B0  token=0x6000321  System.Void Play(UnityEngine.AnimationClip animationClip, DG.Tweening.TweenCallback onComplete, Beyond.UI.UIConst.AnimationState newState, System.Single startPlayTime, System.Boolean clearTween)
  RVA=0x03CD5DD0  token=0x6000322  System.Void Play(System.String stateName, DG.Tweening.TweenCallback onComplete, Beyond.UI.UIConst.AnimationState newState, System.Single startPlayTime, System.Boolean clearTween)
  RVA=0x032EA210  token=0x6000323  System.Void PlayWithTween(UnityEngine.AnimationClip animationClip, DG.Tweening.TweenCallback onComplete, Beyond.UI.UIConst.AnimationState newState, System.Single startPlayTime, System.Boolean clearTween, System.Single speed)
  RVA=0x032EA350  token=0x6000324  System.Void PlayWithTween(System.String stateName, DG.Tweening.TweenCallback onComplete, Beyond.UI.UIConst.AnimationState newState, System.Single startPlayTime, System.Boolean clearTween, System.Single speed)
  RVA=0x0B0BA308  token=0x6000325  System.Single GetCurPlayingTime()
  RVA=0x0B0BA868  token=0x6000327  System.Void SetAudioOnOpen(System.String audioOnOpen)
  RVA=0x032EA1B0  token=0x6000328  System.Void PlayOpenAudio()
  RVA=0x03544680  token=0x6000329  System.Void PlayCloseAudio()
  RVA=0x032ED3F0  token=0x600032A  System.Void ClearComponent()
  RVA=0x03543B30  token=0x600032B  System.Void _NotifyAnimationInEasingFinished()
  RVA=0x036981B0  token=0x600032C  System.Void .ctor()
  RVA=0x04D16950  token=0x600032D  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIAnimationTween
TYPE:  class
TOKEN: 0x2000072
SIZE:  0x30
FIELDS:
  public    static readonly Beyond.UI.UIAnimationTween.OptionsDEFAULT_OPTION  // static @ 0x0
  private           DG.Tweening.Tween               m_handler  // 0x10
  private           System.Single                   m_tweenValue  // 0x18
  private           UnityEngine.GameObject          m_animTarget  // 0x20
  private           System.Single                   <clipLength>k__BackingField  // 0x28
PROPERTIES:
  handler  get=0x032ED660
  tweenValue  get=0x03543DD0
  clipLength  get=0x04D86BF0  set=0x04D86C00
METHODS:
  RVA=0x0B0C1AB0  token=0x600033E  System.Void .ctor(UnityEngine.GameObject target, Beyond.UI.UIAnimationWrapper wrapper, System.String name)
  RVA=0x032ED9C0  token=0x600033F  System.Void .ctor(UnityEngine.GameObject target, Beyond.UI.UIAnimationWrapper wrapper, System.String name, Beyond.UI.UIAnimationTween.Options options)
  RVA=0x04255010  token=0x6000340  System.Single GetValue()
  RVA=0x032EC3A0  token=0x6000341  System.Void _SetValue(Beyond.UI.UIAnimationWrapper wrapper, System.String name, System.Single value)
  RVA=0x04D53780  token=0x6000342  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIAnimationSwitchTween
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x40
EXTENDS: Beyond.UI.UISwitchTween
FIELDS:
  private           Beyond.UI.UIAnimationWrapper    m_animWrapper  // 0x30
  private           System.Boolean                  m_dontDisableGameObject  // 0x38
METHODS:
  RVA=0x05396200  token=0x6000349  System.Void .ctor(Beyond.UI.UIAnimationWrapper animWrapper)
  RVA=0x0B0C1970  token=0x600034A  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfHide()
  RVA=0x0B0C1A04  token=0x600034B  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfShow()
  RVA=0x0B0C1918  token=0x600034C  System.Void BeforeShowEffect()
  RVA=0x0B0C18C0  token=0x600034D  System.Void AfterHideEffect()
  RVA=0x04B30370  token=0x600034E  System.Void ResetToState(System.Boolean isShow)
  RVA=0x04B30460  token=0x600034F  System.Void _SetGameObjectActive(System.Boolean isActive)
  RVA=0x0B0C1AA0  token=0x6000350  System.Void <>iFixBaseProxy_BeforeShowEffect()
  RVA=0x0B0C1A98  token=0x6000351  System.Void <>iFixBaseProxy_AfterHideEffect()
  RVA=0x0B0C1AA8  token=0x6000352  System.Void <>iFixBaseProxy_ResetToState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.UIArea
TYPE:  class
TOKEN: 0x2000079
SIZE:  0xA0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerExitHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerEnter  // 0x68
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerExit  // 0x70
  private           System.Boolean                  m_pointerInArea  // 0x78
  public            UnityEngine.GameObject          _highlightNode  // 0x80
  private           System.Boolean                  _usePositionCheckMode  // 0x88
  private           UnityEngine.RectTransform       m_rectTransform  // 0x90
  private           UnityEngine.Canvas              m_canvas  // 0x98
PROPERTIES:
  pointerInArea  get=0x0B0C21F8
  usePositionCheckMode  get=0x0B0C2294
  tickOption  get=0x0B0C2244
METHODS:
  RVA=0x0B0C2140  token=0x600035D  System.Void _SetHighlightNodeActive(System.Boolean active)
  RVA=0x0B0C1DBC  token=0x600035E  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C1E58  token=0x600035F  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C1BBC  token=0x6000360  System.Void OnAwake()
  RVA=0x0B0C1EF4  token=0x6000361  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0C2094  token=0x6000362  UnityEngine.Vector2 _GetPointerScreenPosition()
  RVA=0x0B0C1D5C  token=0x6000363  System.Void OnEnable()
  RVA=0x0B0C1CF8  token=0x6000364  System.Void OnDisable()
  RVA=0x0B0C1C94  token=0x6000365  System.Void OnDestroy()
  RVA=0x0B0C1B50  token=0x6000366  System.Void ClearComponent()
  RVA=0x039C2B00  token=0x6000367  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000368  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000369  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x600036A  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x01538B40  token=0x600036B  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x600036C  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B20  token=0x600036D  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIAutoCloseArea
TYPE:  class
TOKEN: 0x200007A
SIZE:  0xD0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp Beyond.Input.IBindingGroupTarget
FIELDS:
  public    static  System.Boolean                  s_stopCheckShouldClose  // static @ 0x0
  public            System.Collections.Generic.List<UnityEngine.RectTransform>fixedSafeAreas  // 0x68
  public            UnityEngine.RectTransform       tmpSafeArea  // 0x70
  public            System.Boolean                  alsoBlockKeyBoardInput  // 0x78
  private           System.Boolean                  _includeInAutoCloseLayer  // 0x79
  private           UnityEngine.Camera              m_fallbackUICamera  // 0x80
  public            UnityEngine.GameObject          toggleObj  // 0x88
  public            Beyond.DisposedUnityEvent       onTriggerAutoClose  // 0x90
  private           System.String                   _closeActionId  // 0x98
  private           System.Boolean                  _enableCloseActionOnController  // 0xa0
  private           Beyond.UI.UIAnimationWrapper    m_animationWrapper  // 0xa8
  private   static  System.Collections.Generic.LinkedList<UnityEngine.GameObject>s_autoCloseLayerList  // static @ 0x8
  private           Beyond.UI.LuaPanel              m_panel  // 0xb0
  private           System.Int32                    <groupId>k__BackingField  // 0xb8
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0xc0
  private           System.Boolean                  m_bindingInit  // 0xc8
PROPERTIES:
  tickOption  get=0x046DBBC0
  includeInAutoCloseLayer  get=0x044F4560  set=0x0B0C277C
  panel  get=0x044552D0
  groupId  get=0x04D8E580  set=0x04D8E5A0
  parentTarget  get=0x04D892F0  set=0x06AC5ACC
  groupEnabled  get=0x0B0C26F4
METHODS:
  RVA=0x048B9860  token=0x6000372  System.Void Start()
  RVA=0x0B0C24E8  token=0x6000373  System.Void Reset()
  RVA=0x037CEB00  token=0x6000374  System.Void LateTick(System.Single deltaTime)
  RVA=0x044F44B0  token=0x6000375  System.Void OnEnable()
  RVA=0x044F4360  token=0x6000376  System.Void OnDisable()
  RVA=0x0B0C22E4  token=0x6000377  System.Void ChangeEnableCloseActionOnController(System.Boolean enabled)
  RVA=0x043B3250  token=0x6000378  System.Void CloseSelf()
  RVA=0x0B0C25A4  token=0x6000379  System.Void TryCloseSelf()
  RVA=0x0B0C23F8  token=0x600037A  System.Void OpenSelf(System.Boolean executeOnComplete)
  RVA=0x037CEBC0  token=0x600037B  System.Boolean ShouldClose()
  RVA=0x04B3A270  token=0x600037C  System.Void ClearComponent()
  RVA=0x0B0C2650  token=0x600037D  System.Void _AutoCloseToggleObjWhenOut()
  RVA=0x044F4590  token=0x600037E  System.Void AddToAutoCloseLayer(UnityEngine.GameObject go)
  RVA=0x044F4410  token=0x600037F  System.Void RemoveFromAutoCloseLayer(UnityEngine.GameObject go)
  RVA=0x0447E100  token=0x6000380  System.Boolean IsTopAutoCloseLayer(UnityEngine.GameObject go)
  RVA=0x04455210  token=0x6000381  UnityEngine.Camera _GetUICamera()
  RVA=0x048B9930  token=0x6000387  System.Void InitBinding()
  RVA=0x044F4670  token=0x6000388  System.Void _UpdateState()
  RVA=0x0B0C2354  token=0x6000389  System.Void OnDestroy()
  RVA=0x045F98E0  token=0x600038A  System.Void .ctor()
  RVA=0x04D13DE0  token=0x600038B  System.Void .cctor()
  RVA=0x069AFD0C  token=0x600038C  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x600038D  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B40  token=0x600038E  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x600038F  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B20  token=0x6000390  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIBigLogo
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x88
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.GameObject          _rootNode  // 0x18
  public            Beyond.UI.UIImage               _bigLogoImage  // 0x20
  private           UnityEngine.GameObject          _stretchNode  // 0x28
  public            Beyond.UI.UIImage               _stretchImageTop  // 0x30
  public            Beyond.UI.UIImage               _stretchImageBottom  // 0x38
  public            Beyond.UI.UIImage               _bg  // 0x40
  public            Beyond.UI.UIImage               _originalImage  // 0x48
  public            Beyond.UI.UIImage               _backgroundMask  // 0x50
  public            UnityEngine.CanvasGroup         _originalImageMain  // 0x58
  public            UnityEngine.RectTransform       _fitImageMain  // 0x60
  public            Beyond.UI.UIImage               _fitImageTop  // 0x68
  public            UnityEngine.CanvasGroup         _fitImageTopBg  // 0x70
  public            Beyond.UI.UIImage               _fitImageBottom  // 0x78
  public            UnityEngine.CanvasGroup         _fitImageBottomBg  // 0x80
METHODS:
  RVA=0x0B0C2CFC  token=0x6000391  System.Void SetBigLogoEditor(System.String bigLogoPath, System.Boolean useStretchImage, System.Boolean showOnTop, System.Boolean hideBackground, System.Boolean useOriginalImage, System.Boolean useFitImage)
  RVA=0x0B0C2F70  token=0x6000392  System.Void ShowBigLogo(System.Boolean useStretchImage, System.Boolean showOnTop, System.Boolean useOriginalImage, System.Boolean useFitImage, System.Single originalImageScale)
  RVA=0x0B0C2D94  token=0x6000393  System.Void SetBigLogoMask(UnityEngine.Color maskColor)
  RVA=0x0B0C282C  token=0x6000394  System.Void ClearBigLogoMask()
  RVA=0x0B0C28DC  token=0x6000395  System.Void ClearBigLogo(System.Boolean showOnTop)
  RVA=0x0B0C2ACC  token=0x6000396  System.Void SetAlpha(System.Single alpha, System.Boolean showOnTop)
  RVA=0x0B0C2EA8  token=0x6000397  System.Void SetMaskAlpha(System.Single alpha)
  RVA=0x05393520  token=0x6000398  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIBigRectButton
TYPE:  class
TOKEN: 0x200007C
SIZE:  0x2E8
EXTENDS: Beyond.UI.UIButton
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IScrollHandler
FIELDS:
  private           Beyond.UI.UIBigRectHelper       _bigRectHelper  // 0x2e0
METHODS:
  RVA=-1  // generic def  token=0x6000399  System.Void _TryThroughEventDataToBigRectTouchPanelRaycast(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> eventFunction)
  RVA=0x0463DB40  token=0x600039A  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x045FC110  token=0x600039B  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C32D0  token=0x600039C  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C322C  token=0x600039D  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C3374  token=0x600039E  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C3418  token=0x600039F  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03698EC0  token=0x60003A0  System.Void .ctor()
  RVA=0x0B0C34BC  token=0x60003A1  System.Void <>iFixBaseProxy_OnPointerDown(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0C34C4  token=0x60003A2  System.Void <>iFixBaseProxy_OnPointerUp(UnityEngine.EventSystems.PointerEventData P0)
END_CLASS

CLASS: Beyond.UI.UIBigRectHelper
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x1E8
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Boolean                  m_inited  // 0x68
  private           System.Boolean                  m_openTweenFinished  // 0x69
  private           Beyond.DisposedUnityEvent       m_onOpenTweenFinished  // 0x70
  private           System.Single                   m_zoom  // 0x78
  private           System.Single                   m_zoomValue  // 0x7c
  private           UnityEngine.Vector2             m_oriSize  // 0x80
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>m_zoomTween  // 0x88
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions>m_focusTween  // 0x90
  private           UnityEngine.Vector2             m_anchoredPosOffset  // 0x98
  private           System.Single                   m_horizontalOffset  // 0xa0
  private           System.Single                   m_verticalOffset  // 0xa4
  private           UnityEngine.Vector2             m_lastScale  // 0xa8
  private           UnityEngine.Vector2             m_nodesOffsetWithContainerNode  // 0xb0
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0xb8
  private           UnityEngine.RectTransform       m_rootRectTransform  // 0xc0
  private           System.Single                   m_zoomRangeMin  // 0xc8
  private           System.Boolean                  m_useOverrideMinZoomRange  // 0xcc
  private           System.Single                   m_zoomRangeOverrideMin  // 0xd0
  private           Beyond.UI.UITouchPanel          _touchPanel  // 0xd8
  private           UnityEngine.RectTransform       _containerBigRectTransform  // 0xe0
  private           UnityEngine.RectTransform       _nodes  // 0xe8
  private           System.Single                   _moveSpeed  // 0xf0
  private           System.Single                   _zoomSpeed  // 0xf4
  private           System.Single                   _zoomRangeMax  // 0xf8
  private           System.Boolean                  _needResetPositionAndScale  // 0xfc
  private           System.Boolean                  _needCenterZoomMobile  // 0xfd
  private           System.Collections.Generic.List<System.Single>_zoomThresholds  // 0x100
  private           Beyond.DisposedUnityEvent<System.Int32,System.Boolean>m_zoomEvent  // 0x108
  private           System.Single                   _zoomTweenTime  // 0x110
  private           System.Single                   _moveNodeXOffset  // 0x114
  private           System.Single                   _moveNodeYOffset  // 0x118
  private           System.Single                   _openZoomTweenTime  // 0x11c
  private           UnityEngine.AnimationCurve      _openZoomCurve  // 0x120
  private           System.Single                   _focusNodeTime  // 0x128
  private           UnityEngine.AnimationCurve      _focusNodeCurve  // 0x130
  private           System.Boolean                  _containerEdgeAlignLeft  // 0x138
  private           UnityEngine.RectOffset          _padding  // 0x140
  private           System.Single                   _paddingBottomIncludeControllerHintBar  // 0x148
  private           System.Single                   _controllerContainerHorizontalMoveSpeed  // 0x14c
  private           System.Single                   _controllerContainerVerticalMoveSpeed  // 0x150
  private           System.Single                   _controllerZoomRate  // 0x154
  private           UnityEngine.GameObject          _controllerZoomKeyHint  // 0x158
  private           System.Boolean                  _useFocusRect  // 0x160
  private           System.Boolean                  _useClickKeyHint  // 0x161
  private           UnityEngine.Transform           _raycastRoot  // 0x168
  private           UnityEngine.RectTransform       _controllerFocusRect  // 0x170
  private           System.Single                   _controllerFocusRectHorizontalMoveSpeed  // 0x178
  private           System.Single                   _controllerFocusRectVerticalMoveSpeed  // 0x17c
  private           System.Single                   _controllerFocusLeftBoundPadding  // 0x180
  private           System.Single                   _controllerFocusRightBoundPadding  // 0x184
  private           System.Single                   _controllerFocusBottomBoundPadding  // 0x188
  private           System.Single                   _controllerFocusTopBoundPadding  // 0x18c
  private           UnityEngine.Vector2             m_lastStickValue  // 0x190
  private           UnityEngine.EventSystems.PointerEventDatam_controllerFocusEventData  // 0x198
  private   readonly System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>m_raycastResults  // 0x1a0
  private           UnityEngine.UI.Selectable       m_lastHitSelectable  // 0x1a8
  private           System.Int32                    m_bindingGroupId  // 0x1b0
  private   static  System.String                   CLICK_ACTION_ID  // const
  private   static  System.String                   CLICK_NO_HINT_ACTION_ID  // const
  private   static  System.Single                   DELTA_TIME_TO_STICK_VALUE_RATIO  // const
  private           System.Int32                    <clickBindingId>k__BackingField  // 0x1b4
  private           System.Boolean                  <controllerMoveEnabled>k__BackingField  // 0x1b8
  private           System.Boolean                  <controllerZoomEnabled>k__BackingField  // 0x1b9
  public    readonly Beyond.DisposedUnityEvent       onControllerMoveStart  // 0x1c0
  public    readonly Beyond.DisposedUnityEvent       onControllerMoveStop  // 0x1c8
  public    readonly Beyond.DisposedUnityEvent       onControllerZoom  // 0x1d0
  public    readonly Beyond.DisposedUnityEvent       onControllerFocusEnterSelectable  // 0x1d8
  public    readonly Beyond.DisposedUnityEvent       onControllerFocusExitSelectable  // 0x1e0
PROPERTIES:
  tickOption  get=0x04733320
  OnOpenTweenFinished  get=0x0B0C4B64
  zoomRangeMin  get=0x03E9AC30
  zoomEvent  get=0x0B0C4BB4
  touchPanel  get=0x0445E920
  clickBindingId  get=0x04D84A90  set=0x04DAAE00
  controllerMoveEnabled  get=0x04D90170  set=0x04D90180
  controllerZoomEnabled  get=0x04DAADF0  set=0x04DAAE10
METHODS:
  RVA=0x039D2F90  token=0x60003A8  System.Void OnAwake()
  RVA=0x03FD45D0  token=0x60003A9  System.Void Tick(System.Single deltaTime)
  RVA=0x03E9AE60  token=0x60003AA  System.Void LateTick(System.Single deltaTime)
  RVA=0x04B2CD70  token=0x60003AB  System.Void OnRelease()
  RVA=0x03E9AF20  token=0x60003AC  System.Void _OnDrag(UnityEngine.Vector2 value)
  RVA=0x0B0C3A88  token=0x60003AD  System.Void ManuallyZoom(System.Single value, System.Boolean needTween)
  RVA=0x0B0C48DC  token=0x60003AE  System.Void _RefreshPivotPosition()
  RVA=0x0B0C4110  token=0x60003AF  System.Void _OnZoom(System.Single value, System.Boolean needTween)
  RVA=0x03E9A810  token=0x60003B0  System.Void _SetZoomValueAndScale(System.Single value, System.Boolean needTween)
  RVA=0x03E9BAC0  token=0x60003B1  System.Void _SetScale(System.Boolean needTween, System.Single zoomTweenTime, UnityEngine.AnimationCurve curve, DG.Tweening.TweenCallback onFinish)
  RVA=0x03E9B840  token=0x60003B2  System.Void _GetContainerRectBounds(UnityEngine.Vector2& leftBottom, UnityEngine.Vector2& rightTop)
  RVA=0x03E9B570  token=0x60003B3  UnityEngine.Vector2 _GetAdjustedPosition(UnityEngine.Vector2 position)
  RVA=0x03E9AFD0  token=0x60003B4  System.Void _PosAdjust()
  RVA=0x0B0C46DC  token=0x60003B5  System.Void _RefreshPivotPositionToMousePosition()
  RVA=0x0315F0C0  token=0x60003B6  System.Void _RefreshPivotPosition(UnityEngine.Vector3 targetScreenPosition)
  RVA=0x03E9A700  token=0x60003B7  System.Void _UpdatePositionOffsetAfterZoomed()
  RVA=0x0B0C4984  token=0x60003B8  System.Void _TryAlignWhenInit()
  RVA=0x03E9A960  token=0x60003B9  System.Void Init()
  RVA=0x0B0C3654  token=0x60003BA  System.Void FocusNode(System.Single nodeX, System.Single nodeY, DG.Tweening.TweenCallback onComplete)
  RVA=0x03E9B040  token=0x60003BB  System.Void FocusNode(UnityEngine.RectTransform nodeRect, System.Boolean needTween, DG.Tweening.TweenCallback onComplete)
  RVA=0x0B0C3EBC  token=0x60003BC  System.Void ZoomToFullRect(DG.Tweening.TweenCallback onComplete)
  RVA=0x0B0C34CC  token=0x60003BD  System.Void ChangePaddingRight(System.Int32 right)
  RVA=0x04D11310  token=0x60003BE  System.Void SetZoomRangeMax(System.Single maxValue)
  RVA=0x04CE4B80  token=0x60003BF  System.Void OverrideZoomRangeMin(System.Single minValue)
  RVA=0x03E9A6A0  token=0x60003C0  System.Void SyncZoomValue(System.Single zoomValue, System.Boolean needTween)
  RVA=0x0B0C3854  token=0x60003C1  Beyond.UI.UIBigRectHelper.BigRectRecoverState GetRecoverState()
  RVA=0x0B0C3A0C  token=0x60003C2  System.Void GetZoomRange(System.Single& rangeMin, System.Single& rangeMax)
  RVA=0x0B0C3804  token=0x60003C3  System.Single GetCurrentZoomValue()
  RVA=0x0B0C3540  token=0x60003C4  System.Void ChangePivotPositionToTarget(UnityEngine.RectTransform target)
  RVA=0x0315F030  token=0x60003C5  System.Void ResetPivotPositionToScreenCenter()
  RVA=0x0B0C3B04  token=0x60003C6  System.Void RecoverContainerState(Beyond.UI.UIBigRectHelper.BigRectRecoverState state)
  RVA=0x046EA510  token=0x60003C7  System.Void ClearComponent()
  RVA=0x046EA5F0  token=0x60003C8  System.Void ClearAllTween()
  RVA=0x037D5DB0  token=0x60003CF  System.Void _InitControllerBindings()
  RVA=0x03FD4860  token=0x60003D0  System.Boolean _GetIsControllerMoveEnabled()
  RVA=0x03FD4760  token=0x60003D1  System.Boolean _GetIsControllerZoomEnabled()
  RVA=0x03E9AD00  token=0x60003D2  System.Void _ResetControllerState()
  RVA=0x03FD4620  token=0x60003D3  System.Void _TickRefreshController(System.Single deltaTime)
  RVA=0x03FD47B0  token=0x60003D4  System.Void _TickRefreshControllerStickMove(System.Single deltaTime)
  RVA=0x0B0C419C  token=0x60003D5  System.Void _RefreshControllerFocusRaycastResults(System.Boolean ignoreTouchPanel, UnityEngine.UI.Selectable& hitSelectable, Beyond.UI.UITouchPanel& hitTouchPanel)
  RVA=0x03E9AEB0  token=0x60003D6  System.Void _TickCheckControllerFocusRaycastTargets()
  RVA=0x0B0C3F44  token=0x60003D7  System.Void _OnControllerClick()
  RVA=0x03FD4680  token=0x60003D8  System.Void _TickRefreshControllerStickZoom(System.Single deltaTime)
  RVA=0x03FD46D0  token=0x60003D9  System.Void _TickRefreshControllerZoomKeyHint()
  RVA=0x0445D560  token=0x60003DA  System.Void .ctor()
  RVA=0x03E9AC70  token=0x60003DB  System.Void <OnAwake>b__63_0(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C3E1C  token=0x60003DC  System.Void <OnAwake>b__63_1(System.Single zoomValue)
  RVA=0x0B0C3E9C  token=0x60003DD  System.Void <_SetScale>b__72_0()
  RVA=0x0B0C3CF4  token=0x60003DE  System.Void <Init>b__80_0()
  RVA=0x069AFD0C  token=0x60003DF  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60003E0  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x60003E1  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06BD8710  token=0x60003E2  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x069AFD04  token=0x60003E3  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UIBlendImage
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x238
EXTENDS: Beyond.UI.UIImage
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.String,UnityEngine.Material>s_materials  // static @ 0x0
  private           UnityEngine.Material            m_originalMaterial  // 0x228
  private           System.Boolean                  m_initialized  // 0x230
METHODS:
  RVA=0x0B0C4CA8  token=0x60003E7  System.Void SetMaterialKey(System.String key)
  RVA=0x0B0C4C04  token=0x60003E8  System.Void SetColor(System.String materialId, UnityEngine.Color c)
  RVA=0x0B0C4F38  token=0x60003E9  System.Void SetTexture(System.String materialId, UnityEngine.Texture t)
  RVA=0x03C11520  token=0x60003EA  System.Void ClearMaterialCache()
  RVA=0x0B0C4FC4  token=0x60003EB  System.Void .ctor()
  RVA=0x04D168C0  token=0x60003EC  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIBlurMono
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x88
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Canvas              m_canvas  // 0x68
  private           Beyond.UI.LuaPanel              m_panel  // 0x70
  private           System.Int32                    m_sortingOrder  // 0x78
  private           HG.Rendering.Runtime.HGCamera   m_camera  // 0x80
PROPERTIES:
  tickOption  get=0x04A355E0
  canvas  get=0x03F02FB0
  panel  get=0x03F02E50
METHODS:
  RVA=0x03F02D60  token=0x60003F0  System.Void OnEnable()
  RVA=0x03F02B50  token=0x60003F1  System.Void OnDisable()
  RVA=0x0B0C4FCC  token=0x60003F2  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0C50BC  token=0x60003F3  System.Void UpdateSortingOrder(System.Int32 order)
  RVA=0x03F02BB0  token=0x60003F4  HG.Rendering.Runtime.HGCamera _GetCamera()
  RVA=0x039C2B00  token=0x60003F5  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60003F6  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x60003F7  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x60003F8  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B50  token=0x60003F9  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIBlurRT
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.UI.RawImage         _rawImage  // 0x18
  private           System.Boolean                  _autoUpdate  // 0x20
  private           HG.Rendering.Runtime.RTExtractionType_rtExtractionType  // 0x24
  private           HG.Rendering.Runtime.RTExtractionDuration_rtExtractionDuration  // 0x28
  private           UnityEngine.Rendering.RTHandle  m_rtHandle  // 0x30
  private           System.Boolean                  _needUseSceneColorPS  // 0x38
  private           UnityEngine.Rendering.RTHandle  m_useSceneColorPsRtHandle  // 0x40
  private           System.Boolean                  m_curIsUseSceneColorPS  // 0x48
  private           UnityEngine.Texture             <customBlurTexture>k__BackingField  // 0x50
PROPERTIES:
  rawImage  get=0x0B0C5504
  customBlurTexture  get=0x04D86270  set=0x02FDAEA0
METHODS:
  RVA=0x0B0C5198  token=0x60003FB  System.Void Reset()
  RVA=0x037DE9F0  token=0x60003FC  System.Void InitRT()
  RVA=0x037DD390  token=0x60003FD  System.Void Register()
  RVA=0x0B0C5340  token=0x60003FE  System.Void UnRegister()
  RVA=0x0B0C5430  token=0x60003FF  System.Void UpdateRT()
  RVA=0x04A06D90  token=0x6000400  System.Void OnEnable()
  RVA=0x04CA2BD0  token=0x6000401  System.Void OnDisable()
  RVA=0x0B0C5148  token=0x6000402  System.Void OnDestroy()
  RVA=0x037DD120  token=0x6000403  System.Void _Clear()
  RVA=0x037DBF40  token=0x6000404  System.Void SetUseSceneColorPS(System.Boolean useSceneColorPS, System.Boolean forceUpdate)
  RVA=0x0B0C5218  token=0x6000407  System.Void SetCustomBlurImg(UnityEngine.Texture texture)
  RVA=0x04D516C0  token=0x6000408  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIButton
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x2E0
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.Input.IBindingGroupTarget Beyond.Input.IBindingView Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.String                   _audioPressStart  // 0x188
  private           System.String                   _audioOnClick  // 0x190
  private           System.String                   _audioRelease  // 0x198
  private           System.String                   _audioLongPress  // 0x1a0
  private           System.String                   _audioInvalid  // 0x1a8
  private           System.Boolean                  _noAudioWhenAutoTriggerOnClick  // 0x1b0
  private   static  System.Single                   MIN_DRAG_DIST  // const
  private   static  System.Single                   DOUBLE_CLICK_MAX_INTERVAL  // const
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onClick  // 0x1b8
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onDoubleClick  // 0x1c0
  public            System.Boolean                  _ignoreClickOnDrag  // 0x1c8
  public            System.Boolean                  _clickThrough  // 0x1c9
  private           System.Single                   _clickCd  // 0x1cc
  private           Beyond.LangKey                  _invalidClickLangKey  // 0x1d0
  private           System.Single                   m_lastValidClickTime  // 0x1e0
  private           System.Single                   m_lastClickTime  // 0x1e4
  private           System.Boolean                  _useClickAnimation  // 0x1e8
  private           UnityEngine.AnimationCurve      _clickAnimationCurve  // 0x1f0
  private           System.Single                   _clickAnimationDuration  // 0x1f8
  private           UnityEngine.Vector3             m_oriScale  // 0x1fc
  private           UnityEngine.Coroutine           m_clickCor  // 0x208
  private           System.Boolean                  m_scaled  // 0x210
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onLongPress  // 0x218
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onPressStart  // 0x220
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onPressEnd  // 0x228
  private           System.Single                   _longPressTime  // 0x230
  public            System.Boolean                  _ignoreClickOnLongPress  // 0x234
  public            Beyond.UI.UIImage               longPressImg  // 0x238
  private           System.Single                   _longPressImgShowDelay  // 0x240
  private           System.Single                   _longPressMaxDragDist  // 0x244
  private           System.Boolean                  _forceEnableButtonPressAudio  // 0x248
  private           System.Boolean                  m_pressDownWaitClick  // 0x249
  private           UnityEngine.Vector2             <startPressPos>k__BackingField  // 0x24c
  private           System.Int32                    <pressTouchId>k__BackingField  // 0x254
  private           UnityEngine.Coroutine           m_longPressCor  // 0x258
  private           System.Boolean                  m_triggeredLongPress  // 0x260
  public            System.Boolean                  noTextBtn  // 0x261
  private           Beyond.UI.UIText                m_autoFindLabel  // 0x268
  private           Beyond.UI.UIText                _label  // 0x270
  private           System.Int32                    m_groupId  // 0x278
  private           System.Boolean                  m_bindingInit  // 0x27c
  private           Beyond.Input.InputBindingGroupMonoTarget<parent>k__BackingField  // 0x280
  private           System.String                   m_customBindingViewLabelText  // 0x288
  public            System.String                   hintTextId  // 0x290
  private   readonly Beyond.DisposedUnityEvent       <bindingViewUpdateEvent>k__BackingField  // 0x298
  private           UnityEngine.RectTransform       _redDotTrans  // 0x2a0
  private           System.String                   _audioGamePadLongPress  // 0x2a8
  private           Beyond.UI.LuaPanel              m_panel  // 0x2b0
  private           System.Int32                    m_hoverBindingGroupId  // 0x2b8
  public            System.String                   clickHintTextId  // 0x2c0
  public            System.String                   longPressHintTextId  // 0x2c8
  private           Beyond.Input.ActionOnSetNaviTarget_actionOnSetNaviTarget  // 0x2d0
  private           System.Boolean                  _useNoHintAction  // 0x2d4
  private           System.Int32                    <hoverConfirmBindingId>k__BackingField  // 0x2d8
PROPERTIES:
  startPressPos  get=0x04DAAE30  set=0x04DAAE70
  pressTouchId  get=0x04DAAE20  set=0x04DAAE60
  longPressTime  get=0x0B0C5C60
  curPressPos  get=0x0B0C5BCC
  triggeredLongPress  get=0x0B0C5E2C
  text  get=0x0B0C5D54  set=0x048587A0
  label  get=0x048588D0
  groupEnabled  get=0x04175BD0
  groupId  get=0x02FA7AB0
  parentTarget  get=0x0B0C5CB4
  parent  get=0x04D8DD90  set=0x06B8B7B0
  bindingViewActionId  get=0x04300180
  customBindingViewLabelText  get=0x048146C0  set=0x04750CE0
  bindingViewLabelText  get=0x048145D0
  hintText  get=0x04814610
  bindingViewUpdateEvent  get=0x04D8DA30
  redDotTrans  get=0x0B0C5D04
  bindingEnabled  get=0x0B0C5B7C
  bindingViewState  get=0x02FAA1C0
  panel  get=0x03799EC0
  hoverBindingGroupId  get=0x03793AC0
  actionOnSetNaviTarget  get=0x0B0C5B2C
  hoverConfirmBindingId  get=0x04CE6410  set=0x04DAAE50
METHODS:
  RVA=0x037D4910  token=0x6000409  System.Void Awake()
  RVA=0x03794DE0  token=0x600040A  System.Void OnEnable()
  RVA=0x037963D0  token=0x600040B  System.Void OnDisable()
  RVA=0x0351DA80  token=0x600040C  System.Void OnDestroy()
  RVA=0x031EAC10  token=0x600040D  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0379A830  token=0x600040E  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03798DF0  token=0x600040F  System.Void _Press(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03798130  token=0x6000410  System.Void _PlayClickAnimation(System.Boolean reverse)
  RVA=0x0B0C5878  token=0x6000411  System.Collections.IEnumerator _DoClickAnimation()
  RVA=0x03797EA0  token=0x6000419  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03798180  token=0x600041A  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037980C0  token=0x600041B  System.Collections.IEnumerator _OnPress(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03796440  token=0x600041C  System.Void _StopLongPressCor()
  RVA=0x0351CC90  token=0x6000420  System.Void ClearComponent()
  RVA=0x02FA7B20  token=0x6000426  System.Void InitBinding()
  RVA=0x04A1DCC0  token=0x6000427  System.Void OnInteractableChanged()
  RVA=0x03793450  token=0x6000428  System.Void _UpdateState()
  RVA=0x033FD550  token=0x6000432  System.Boolean _IsAnyButtonEventActionInGuideUse()
  RVA=0x04300130  token=0x6000433  System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType)
  RVA=0x04B09D00  token=0x6000434  System.Void Beyond.Input.IBindingView.OnInputKeyDown(System.Boolean isActionTrigger)
  RVA=0x04B42580  token=0x6000435  System.Void Beyond.Input.IBindingView.OnInputKeyUp(System.Boolean isActionTrigger)
  RVA=0x043DF550  token=0x6000436  System.Void _OnUpdateKey()
  RVA=0x033FDBF0  token=0x6000437  System.String _GetUIButtonKeyHintActionId(Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData> buttonEvent)
  RVA=0x03793730  token=0x600043A  System.Void _RefreshHoverEnableNode(System.Boolean active)
  RVA=0x0B0C5644  token=0x600043B  System.Boolean GetMouseActionHints(System.String& clickHintTextId, System.String& longPressHintTextId)
  RVA=0x0B0C56D4  token=0x600043C  System.Void OnNaviTargetEnabledAgain()
  RVA=0x0B0C5908  token=0x600043E  System.Void _OnSetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode)
  RVA=0x037D1500  token=0x6000441  System.Void _InitActionOnSetNaviTarget()
  RVA=0x0B0C5A94  token=0x6000442  System.Void _ToggleNaviInputBinding(System.Boolean active)
  RVA=0x0B0C55A4  token=0x6000443  System.Void ChangeActionOnSetNaviTarget(Beyond.Input.ActionOnSetNaviTarget action)
  RVA=0x03697780  token=0x6000444  System.Void .ctor()
  RVA=0x0B0C5554  token=0x6000445  System.Boolean Beyond.Input.IBindingView.get_interactable()
  RVA=0x04D84D90  token=0x6000446  System.Void <InitBinding>b__82_0()
  RVA=0x0B0C5764  token=0x6000447  System.Void <InitBinding>b__82_1()
  RVA=0x0B0C57B0  token=0x6000448  System.Void <InitBinding>b__82_2()
  RVA=0x03798170  token=0x6000449  System.Void <InitBinding>b__82_3()
  RVA=0x037986A0  token=0x600044A  System.Void <InitBinding>b__82_4()
  RVA=0x0B0C582C  token=0x600044B  System.Void <_InitActionOnSetNaviTarget>b__133_0()
  RVA=0x0B0C580C  token=0x600044C  System.Void <>iFixBaseProxy_OnPointerDown(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0C5814  token=0x600044D  System.Void <>iFixBaseProxy_OnPointerUp(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0869D4  token=0x600044E  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0869E4  token=0x600044F  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0447EC90  token=0x6000450  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0869DC  token=0x6000451  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0C581C  token=0x6000452  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x041E1670  token=0x6000453  System.Void <>iFixBaseProxy_OnInteractableChanged()
  RVA=0x0B0C5824  token=0x6000454  System.Void <>iFixBaseProxy__RefreshHoverEnableNode(System.Boolean P0)
  RVA=0x0B0C57FC  token=0x6000455  System.Boolean <>iFixBaseProxy_GetMouseActionHints(System.String& P0, System.String& P1)
  RVA=0x0B0C5804  token=0x6000456  System.Void <>iFixBaseProxy_OnNaviTargetEnabledAgain()
  RVA=0x041E1670  token=0x6000457  System.Void <>iFixBaseProxy__OnSetAsNaviTarget(System.Boolean P0, System.Boolean P1)
END_CLASS

CLASS: Beyond.UI.CanvasMatchMode
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CanvasMatchMode       Width  // const
  public    static  Beyond.UI.CanvasMatchMode       Height  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UICanvasScaleHelper
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: IScreenSizeListener Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Boolean                  _useCustomStandardResolution  // 0x18
  private           UnityEngine.Vector2             _customStandardResolution  // 0x1c
  private           System.Single                   _customPCReferenceResolutionScale  // 0x24
  private           System.Collections.Generic.HashSet<UnityEngine.Canvas>m_canvasList  // 0x28
  private           System.Collections.Generic.HashSet<UnityEngine.UI.CanvasScaler>m_canvasScalerList  // 0x30
  private           System.Boolean                  m_isCanvasChanged  // 0x38
  public            System.Boolean                  isSingleCanvas  // 0x39
  public            UnityEngine.Camera              uiCamera  // 0x40
  public            Beyond.DisposedUnityEvent       onScreenSizeChanged  // 0x48
  public            Beyond.DisposedUnityEvent       onCanvasChanged  // 0x50
PROPERTIES:
  standardHorizontalResolution  get=0x03C00430
  standardVerticalResolution  get=0x03C003F0
  standardResolutionScale  get=0x03C00470
  canvasScalerList  get=0x0B0C608C
METHODS:
  RVA=0x047B2810  token=0x6000468  System.Void Awake()
  RVA=0x047B2850  token=0x6000469  System.Void _OnInit()
  RVA=0x03C03270  token=0x600046A  System.Void Start()
  RVA=0x03C037A0  token=0x600046B  System.Void OnDestroy()
  RVA=0x03C017D0  token=0x600046C  System.Void UpdateCanvas()
  RVA=0x03C03500  token=0x600046D  System.Void OnScreenSizeChanged(System.Int32 width, System.Int32 height)
  RVA=0x03C006A0  token=0x600046E  System.Void AddCanvas(UnityEngine.Canvas c, UnityEngine.UI.CanvasScaler s)
  RVA=0x0479C450  token=0x600046F  System.Void RemoveCanvas(UnityEngine.Canvas c, UnityEngine.UI.CanvasScaler s)
  RVA=0x0B0C5ED8  token=0x6000470  System.Void _OnInputTypeChanged()
  RVA=0x03DB8A80  token=0x6000471  System.Void _CanvasOnWillRenderCanvases()
  RVA=0x03C009A0  token=0x6000472  Beyond.UI.UICanvasScaleHelper.FWorldCanvasParams CalcWorldCanvasParams(UnityEngine.Camera camera, UnityEngine.Transform uiRoot, System.Boolean useLocalPos)
  RVA=0x03C00770  token=0x6000473  Beyond.UI.UICanvasScaleHelper.FScreenCanvasParams CalcScreenCanvasParams()
  RVA=0x03C00810  token=0x6000474  System.Single CalcCurrentScreenRatio(UnityEngine.Vector2& screenSize)
  RVA=0x03C00380  token=0x6000475  UnityEngine.Vector2 GetProperCanvasResolution()
  RVA=0x0B0C5E7C  token=0x6000476  System.Void ForceCanvasUpdate()
  RVA=0x03C03910  token=0x6000477  System.Void ClearComponent()
  RVA=0x045D0960  token=0x6000478  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIColorGroup
TYPE:  class
TOKEN: 0x200008A
SIZE:  0xE0
EXTENDS: Beyond.UI.NonDrawingGraphic
FIELDS:
  private           UnityEngine.UI.Graphic[]        _colorElements  // 0xb8
  private           UnityEngine.Color               _color  // 0xc0
  private           System.Boolean                  m_isOperating  // 0xd0
  private           System.Collections.Generic.List<Beyond.UI.UIColorGroup.DynEle>m_dynList  // 0xd8
PROPERTIES:
  color  get=0x0B0C6B5C  set=0x0B0C6BC4
METHODS:
  RVA=0x0B0C60DC  token=0x600047B  System.Void AttachGraphic(UnityEngine.UI.Graphic graphic, System.Boolean useStaticColor)
  RVA=0x0B0C633C  token=0x600047C  System.Void AttachGraphicsWithGroup(System.Collections.Generic.List<UnityEngine.UI.Graphic> graphic, System.String groupId)
  RVA=0x0B0C6658  token=0x600047D  System.Void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale)
  RVA=0x0B0C6774  token=0x600047E  System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha, System.Boolean useRGB)
  RVA=0x04640320  token=0x600047F  System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha)
  RVA=0x046404B0  token=0x6000480  System.Void _ApplyOptToGraphics(Beyond.UI.UIColorGroup.GraphicOpt opt, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x0B0C6950  token=0x6000481  System.Void _CrossFadeAlpha(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x0B0C69E4  token=0x6000482  System.Void _CrossFadeColorRGB(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x04891630  token=0x6000483  System.Void _CrossFadeColorAlpha(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x0B0C6AC0  token=0x6000484  System.Void _SetColor(UnityEngine.UI.Graphic graphic, Beyond.UI.UIColorGroup.CommonParams param)
  RVA=0x039C4AD0  token=0x6000485  System.Void .ctor()
  RVA=0x04D8C200  token=0x6000486  UnityEngine.Color <>iFixBaseProxy_get_color()
  RVA=0x0B0C6930  token=0x6000487  System.Void <>iFixBaseProxy_set_color(UnityEngine.Color P0)
  RVA=0x0B0C68B4  token=0x6000488  System.Void <>iFixBaseProxy_CrossFadeAlpha(System.Single P0, System.Single P1, System.Boolean P2)
  RVA=0x0B0C68F4  token=0x6000489  System.Void <>iFixBaseProxy_CrossFadeColor(UnityEngine.Color P0, System.Single P1, System.Boolean P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B0C68C4  token=0x600048A  System.Void <>iFixBaseProxy_CrossFadeColor(UnityEngine.Color P0, System.Single P1, System.Boolean P2, System.Boolean P3)
END_CLASS

CLASS: Beyond.UI.UIControllerKeyIcon
TYPE:  class
TOKEN: 0x200008E
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.String                   _keyIconName  // 0x18
  private           Beyond.UI.UIImage               m_image  // 0x20
  private   static  System.Collections.Generic.List<Beyond.UI.UIControllerKeyIcon>s_iconCache  // static @ 0x0
  private   static  System.Boolean                  s_initialized  // static @ 0x8
METHODS:
  RVA=0x0B0C72DC  token=0x600048F  System.Void _TryStaticInit()
  RVA=0x0B0C7140  token=0x6000490  System.Void _OnControllerTypeChange(Beyond.DeviceInfo.ControllerType controllerType)
  RVA=0x0B0C6DCC  token=0x6000491  System.Void Awake()
  RVA=0x0B0C6F1C  token=0x6000492  System.Void OnEnable()
  RVA=0x0B0C6E78  token=0x6000493  System.Void OnDisable()
  RVA=0x0B0C70A4  token=0x6000494  System.Void SetKeyIconName(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x0B0C6FC8  token=0x6000495  System.Void RefreshKeyIcon()
  RVA=0x03695570  token=0x6000496  System.Void .ctor()
  RVA=0x04CECCD0  token=0x6000497  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UICustomLayout
TYPE:  class
TOKEN: 0x200008F
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           UnityEngine.RectTransform       _dragArea  // 0x18
  public            System.Collections.Generic.List<Beyond.UI.UICustomLayoutHandle>layoutHandles  // 0x20
  private           ZSimpleJSON.JSONArray           m_eventLog  // 0x28
PROPERTIES:
  dragArea  get=0x0B0CA7E8
METHODS:
  RVA=0x0B0C9B10  token=0x6000499  System.Void Awake()
  RVA=0x0B0C9C58  token=0x600049A  System.Boolean IsDirty()
  RVA=0x0B0C9DB4  token=0x600049B  System.Void ResetToDefault(System.Boolean saveChanges)
  RVA=0x0B0C9F40  token=0x600049C  System.Void Save(System.Int32 ratio)
  RVA=0x0B0CA1F0  token=0x600049D  System.Void _LogEvent(System.Collections.Generic.Dictionary<Beyond.UI.UICustomLayoutElement,Beyond.UI.UICustomLayoutElement.LayoutData> layoutDataMap)
  RVA=0x0B0CA730  token=0x600049E  System.Void .ctor()
  RVA=0x041E1670  token=0x600049F  System.Void <>iFixBaseProxy_Awake()
END_CLASS

CLASS: Beyond.UI.UICustomLayoutElement
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x80
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Int32                    _verNum  // 0x18
  private           System.String                   _elementKey  // 0x20
  private           UnityEngine.Transform           _overrideScaleTransform  // 0x28
  private           UnityEngine.CanvasGroup         _overrideCanvasGroup  // 0x30
  private           UnityEngine.RectTransform       _overrideHighlightRect  // 0x38
  public            Beyond.DisposedUnityEvent       onLayoutDataLoaded  // 0x40
  private           Beyond.UI.LuaPanel              m_panel  // 0x48
  private           System.Single                   m_scale  // 0x50
  private           System.Single                   m_alpha  // 0x54
  private           UnityEngine.Vector2             m_position  // 0x58
  private           Beyond.UI.UICustomLayoutElement.LayoutTypem_layoutType  // 0x60
  private           System.Single                   m_defaultScale  // 0x64
  private           System.Single                   m_defaultAlpha  // 0x68
  private           UnityEngine.Vector2             m_defaultPosition  // 0x6c
  private           UnityEngine.RectTransform       m_rectTransform  // 0x78
PROPERTIES:
  elementKey  get=0x0B0C7FE0
  highlightRect  get=0x0B0C8030
  scale  get=0x0B0C8170  set=0x0B0C82E4
  alpha  get=0x0B0C7F90  set=0x0B0C81C0
  position  get=0x0B0C8114  set=0x0B0C827C
  layoutType  get=0x0B0C80C8  set=0x0B0C8224
  panel  get=0x03BFCC30
METHODS:
  RVA=0x03BFBFF0  token=0x60004AB  System.Void Awake()
  RVA=0x04A21300  token=0x60004AC  System.Void Start()
  RVA=0x0B0C77E8  token=0x60004AD  System.Void OnDestroy()
  RVA=0x0B0C7970  token=0x60004AE  System.Void ResetToDefault()
  RVA=0x0B0C7CFC  token=0x60004AF  System.Void _ApplyPosition()
  RVA=0x0B0C7C18  token=0x60004B0  System.Void _ApplyAlpha()
  RVA=0x0B0C7D68  token=0x60004B1  System.Void _ApplyScale()
  RVA=0x03BFC110  token=0x60004B2  System.Void _SetDefaultValues()
  RVA=0x0B0C74BC  token=0x60004B3  System.String GetElementKeyWithRatio(System.Int32 ratio)
  RVA=0x0B0C7E3C  token=0x60004B4  System.Boolean _CheckElementKeyIsValid()
  RVA=0x0B0C7A38  token=0x60004B5  System.Boolean SaveData(Beyond.UI.UICustomLayoutElement.LayoutData& layoutData, System.Boolean isResetToDefault, System.Int32 ratio)
  RVA=0x0B0C7598  token=0x60004B6  System.Void LoadData()
  RVA=0x0B0C7F40  token=0x60004B7  System.Void _OnHudLayoutSaved()
  RVA=0x03BFCB10  token=0x60004B8  UnityEngine.Vector2 _GetMarginSize()
  RVA=0x0B0C7464  token=0x60004B9  System.Void ClearComponent()
  RVA=0x0B0C78DC  token=0x60004BA  System.Void RefreshAll()
  RVA=0x047A2460  token=0x60004BB  System.Void .ctor()
  RVA=0x041E1670  token=0x60004BC  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x60004BD  System.Void <>iFixBaseProxy_Start()
  RVA=0x041E1670  token=0x60004BE  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UICustomLayoutHandle
TYPE:  class
TOKEN: 0x2000093
SIZE:  0xB8
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            Beyond.DisposedUnityEvent       onClick  // 0x18
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onBeginDrag  // 0x20
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x28
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onEndDrag  // 0x30
  public            System.String                   hintTextId  // 0x38
  private           Beyond.UI.LuaPanel              m_panel  // 0x40
  private           System.Single                   m_scale  // 0x48
  private           System.Single                   m_alpha  // 0x4c
  private           UnityEngine.Vector2             m_position  // 0x50
  private           Beyond.UI.UICustomLayoutElement.LayoutTypem_layoutType  // 0x58
  private           UnityEngine.Vector2             m_defaultPosition  // 0x5c
  private           System.Boolean                  m_isChanged  // 0x64
  private           System.Boolean                  m_isResetToDefault  // 0x65
  private           UnityEngine.RectTransform       m_rectTransform  // 0x68
  private           UnityEngine.RectTransform       m_parentRectTransform  // 0x70
  private           Beyond.UI.UICustomLayoutElement m_element  // 0x78
  private           Beyond.UI.UICustomLayout        m_layout  // 0x80
  private           UnityEngine.Vector2             m_dragPosMin  // 0x88
  private           UnityEngine.Vector2             m_dragPosMax  // 0x90
  private           UnityEngine.Vector3[]           m_dragAreaCorners  // 0x98
  private           UnityEngine.Vector3[]           m_highlightCorners  // 0xa0
  private           System.Boolean                  m_isPosRangeDirty  // 0xa8
  private           UnityEngine.Vector2             m_dragOffset  // 0xac
PROPERTIES:
  scale  get=0x0B0C9858  set=0x0B0C9A84
  alpha  get=0x0B0C96A8  set=0x0B0C98A8
  position  get=0x0B0C97FC  set=0x0B0C99A4
  layoutType  get=0x0B0C96F8  set=0x0B0C991C
  panel  get=0x0B0C9744
  Element  get=0x0B0C95F4
METHODS:
  RVA=0x0B0C8348  token=0x60004CA  System.Void Awake()
  RVA=0x0B0C87AC  token=0x60004CB  System.Void OnDestroy()
  RVA=0x0B0C9288  token=0x60004CC  System.Void _OnElementDataLoaded()
  RVA=0x0B0C8EE4  token=0x60004CD  System.Void _CalculatePositionRange()
  RVA=0x0B0C8E00  token=0x60004CE  System.Void _ApplyPosition()
  RVA=0x0B0C8D2C  token=0x60004CF  System.Void _ApplyAlpha()
  RVA=0x0B0C8E74  token=0x60004D0  System.Void _ApplyScale()
  RVA=0x0B0C8D94  token=0x60004D1  System.Void _ApplyLayoutType()
  RVA=0x0B0C8654  token=0x60004D2  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C8910  token=0x60004D3  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C8A5C  token=0x60004D4  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C8C5C  token=0x60004D5  System.Boolean Save(Beyond.UI.UICustomLayoutElement.LayoutData& layoutData, System.Int32 ratio)
  RVA=0x0B0C8B50  token=0x60004D6  System.Void ResetToDefault()
  RVA=0x0B0C8608  token=0x60004D7  System.Boolean IsDirty()
  RVA=0x0B0C93B4  token=0x60004D8  System.Void _SetChanged(System.Boolean changed)
  RVA=0x0B0C8AE8  token=0x60004D9  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0C9364  token=0x60004DA  System.Void _OnSystemDisplaySizeChanged()
  RVA=0x0B0C8554  token=0x60004DB  System.Void ClearComponent()
  RVA=0x0B0C9464  token=0x60004DC  System.Void .ctor()
  RVA=0x041E1670  token=0x60004DD  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x60004DE  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIDialogText
TYPE:  class
TOKEN: 0x2000094
SIZE:  0xE8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  protected         Beyond.UI.UIText                _uiText  // 0x68
  private           System.Single                   _textRevealSpeed  // 0x70
  private           System.Boolean                  _autoPlay  // 0x74
  private           Beyond.UI.UIDialogText.UpdateMode_updateMode  // 0x78
  private           System.Boolean                  _useAutoScroll  // 0x7c
  private           System.Single                   _originalHeight  // 0x80
  private           System.Boolean                  _useAspectRatio  // 0x84
  private           System.Single                   _autoScrollSpeed  // 0x88
  private           System.Single                   _autoWaitTime  // 0x8c
  private           System.Single                   _maxAspectRatio  // 0x90
  private           System.Single                   _minAspectRatio  // 0x94
  private           System.Single                   _maxAspectRatioScrollSpeed  // 0x98
  private           System.Single                   _minAspectRatioScrollSpeed  // 0x9c
  private           System.Single                   _maxAspectRatioWaitTime  // 0xa0
  private           System.Single                   _minAspectRatioWaitTime  // 0xa4
  private           System.Boolean                  m_playing  // 0xa8
  private           System.Int32                    m_revealedCharNum  // 0xac
  private           System.Int32                    m_totalCharNum  // 0xb0
  private           System.Single                   m_timeToNextCharacter  // 0xb4
  protected         System.String                   m_curText  // 0xb8
  private           System.Boolean                  m_hasScrolled  // 0xc0
  private           System.Single                   m_startTime  // 0xc4
  private           System.Boolean                  m_scrollHasEnd  // 0xc8
  private           System.Boolean                  m_needAutoScroll  // 0xc9
  private           UnityEngine.Vector2             m_targetPos  // 0xcc
  private           UnityEngine.Vector2             m_originalPos  // 0xd4
  private           System.Single                   m_percent  // 0xdc
  private           System.Boolean                  m_locked  // 0xe0
PROPERTIES:
  tickOption  get=0x04A79660
  useAspectRatio  get=0x0B0CB5DC
  notUseAspectRatio  get=0x0B0CB41C
  playing  get=0x0B0CB47C
  uiText  get=0x0B0CB58C
  totalCharacterNum  get=0x0B0CB53C
  textRevealSpeed  get=0x0B0CB4EC  set=0x0B0CB638
METHODS:
  RVA=0x04D5A080  token=0x60004E7  System.Void OnAwake()
  RVA=0x04488480  token=0x60004E8  System.Void Start()
  RVA=0x0B0CB228  token=0x60004E9  System.Void OnDisable()
  RVA=0x04D25A60  token=0x60004EA  System.Void OnEnable()
  RVA=0x03E61060  token=0x60004EB  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0CB280  token=0x60004EC  System.Void OnVisibleCharactersChanged(System.Int32 count)
  RVA=0x0B0CAE00  token=0x60004ED  System.Single GetScrollSpeed()
  RVA=0x0B0CAC6C  token=0x60004EE  System.Single GetAutoWaitTime()
  RVA=0x03E610C0  token=0x60004EF  System.Void _UpdateTextAuto()
  RVA=0x03E611B0  token=0x60004F0  System.Void _TryAutoScroll()
  RVA=0x04488690  token=0x60004F1  System.Void RefreshAutoScrollData()
  RVA=0x044885B0  token=0x60004F2  System.Void SetText(System.String text, System.Boolean resetPlay)
  RVA=0x0B0CB370  token=0x60004F3  System.Void _RefreshPosY()
  RVA=0x0B0CB2D4  token=0x60004F4  System.Void SeekToEnd()
  RVA=0x04D25AA0  token=0x60004F5  System.Void RefreshText()
  RVA=0x04488540  token=0x60004F6  System.Void Play()
  RVA=0x04B4A7A0  token=0x60004F7  System.Void ResetPlay()
  RVA=0x03E61200  token=0x60004F8  System.Void StopPlay()
  RVA=0x0B0CAEE4  token=0x60004F9  System.Void ManualUpdateByPercent(System.Single percent)
  RVA=0x0B0CB034  token=0x60004FA  System.Void ManualUpdateByTime(System.Single time)
  RVA=0x0B0CAD50  token=0x60004FB  System.Single GetDisplayAllCharactersTime()
  RVA=0x04A209B0  token=0x60004FC  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60004FD  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60004FE  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B30  token=0x60004FF  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B40  token=0x6000500  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x6000501  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIDialogTextWithCursor
TYPE:  class
TOKEN: 0x2000096
SIZE:  0x118
EXTENDS: Beyond.UI.UIDialogText
FIELDS:
  private           System.Single                   _textGapTime  // 0xe8
  private           Beyond.UI.UIAnimationWrapper    _cursorAnimation  // 0xf0
  private           UnityEngine.RectTransform       _cursorLoopHide  // 0xf8
  private           UnityEngine.RectTransform       _cursorGroup  // 0x100
  private           Beyond.UI.UIImage               _background  // 0x108
  public            System.Boolean                  isLastTextLine  // 0x110
  private           System.Int32                    m_lastCount  // 0x114
METHODS:
  RVA=0x0B0CABE0  token=0x6000502  System.Void ResetPlay()
  RVA=0x0B0CA894  token=0x6000503  System.Void OnVisibleCharactersChanged(System.Int32 count)
  RVA=0x0B0CA838  token=0x6000504  System.Single GetDisplayAllCharactersTime()
  RVA=0x0B0CAC50  token=0x6000505  System.Void .ctor()
  RVA=0x0B0CAC48  token=0x6000506  System.Void <>iFixBaseProxy_ResetPlay()
  RVA=0x0B0CAC40  token=0x6000507  System.Void <>iFixBaseProxy_OnVisibleCharactersChanged(System.Int32 P0)
  RVA=0x0B0CAC38  token=0x6000508  System.Single <>iFixBaseProxy_GetDisplayAllCharactersTime()
END_CLASS

CLASS: Beyond.UI.EasyUIOptionData
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x28
FIELDS:
  public            System.Int32                    index  // 0x10
  public            System.String                   optionIcon  // 0x18
  public            System.String                   optionText  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000509  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIDialogTimelineOptionCell
TYPE:  class
TOKEN: 0x2000098
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIImage               _optionIcon  // 0x18
  private           Beyond.UI.UIText                _optionText  // 0x20
METHODS:
  RVA=0x05393520  token=0x600050A  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIDialogTimelineText
TYPE:  class
TOKEN: 0x2000099
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIDialogText          _dialogText  // 0x18
  private           Beyond.UI.UIText                _actorName  // 0x20
  private           UnityEngine.GameObject          _waitNode  // 0x28
  private           Beyond.UI.UIAnimationWrapper    _radioNode  // 0x30
  private           Beyond.UI.UIImage               _iconImage  // 0x38
  private           Beyond.UI.UIImage               _iconBlueMask  // 0x40
  public            Beyond.UI.UIText                centerText  // 0x48
  private           System.String                   m_currentRadioIcon  // 0x50
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x58
PROPERTIES:
  canvasGroup  get=0x0B0CBA34
  dialogText  get=0x0B0CBAC4
  actorName  get=0x0B0CB9E4
  waitNode  get=0x0B0CBB14
METHODS:
  RVA=0x0B0CB818  token=0x600050F  System.Void UpdateAlpha(System.Single alpha)
  RVA=0x0B0CB694  token=0x6000510  System.Void SetRadioIcon(System.Boolean useRadio, System.String radioIcon)
  RVA=0x0B0CB8D8  token=0x6000511  System.Void _ClearRadioIcon()
  RVA=0x05393520  token=0x6000512  System.Void .ctor()
  RVA=0x0B0CB7D4  token=0x6000513  System.Void <_ClearRadioIcon>b__19_0()
END_CLASS

CLASS: Beyond.UI.UIDrag
TYPE:  class
TOKEN: 0x200009A
SIZE:  0x70
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.RectTransform       _dragArea  // 0x18
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onBeginDrag  // 0x20
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onEndDrag  // 0x28
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x30
  private           UnityEngine.RectTransform       m_rectTransform  // 0x38
  private           UnityEngine.RectTransform       m_parentRectTransform  // 0x40
  private           UnityEngine.Vector2             m_dragOffset  // 0x48
  private           UnityEngine.Vector2             m_dragPosMin  // 0x50
  private           UnityEngine.Vector2             m_dragPosMax  // 0x58
  private           UnityEngine.Vector3[]           m_dragAreaCorners  // 0x60
  private           UnityEngine.Canvas              m_canvas  // 0x68
METHODS:
  RVA=0x0B0CD1B8  token=0x6000514  System.Void Awake()
  RVA=0x0B0CD780  token=0x6000515  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CD354  token=0x6000516  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CD984  token=0x6000517  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CCD78  token=0x6000518  System.Void ApplyDragArea()
  RVA=0x0B0CD2B0  token=0x6000519  System.Void ClearComponent()
  RVA=0x0B0CDA10  token=0x600051A  System.Void .ctor()
  RVA=0x041E1670  token=0x600051B  System.Void <>iFixBaseProxy_Awake()
END_CLASS

CLASS: Beyond.UI.UIDragHandler
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Int32                    _dragThreshold  // 0x18
  private           UnityEngine.Vector2             m_startPressPos  // 0x1c
  private           System.Boolean                  m_isDragThresholdMet  // 0x24
  private           System.Boolean                  m_isDragging  // 0x25
  public            Beyond.UI.UIButton              button  // 0x28
  public            System.Boolean                  ignoreDragOnLongPress  // 0x30
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onBeginDrag  // 0x38
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x40
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onEndDrag  // 0x48
PROPERTIES:
  dragThreshold  get=0x0B0CBF10
METHODS:
  RVA=0x0B0CBB64  token=0x600051D  System.Void ClearComponent()
  RVA=0x0B0CBE9C  token=0x600051E  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CBC08  token=0x600051F  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CBC9C  token=0x6000520  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CBE08  token=0x6000521  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x045F07D0  token=0x6000522  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIDragItem
TYPE:  class
TOKEN: 0x200009C
SIZE:  0xC8
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public    static  UnityEngine.RectTransform       s_commonDragObjectParent  // static @ 0x0
  private           XLua.LuaTable                   <luaTable>k__BackingField  // 0x18
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onBeginDragEvent  // 0x20
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDragEvent  // 0x28
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDragEventWhenCantStartDrag  // 0x30
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onEndDragEvent  // 0x38
  public            UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.GameObject>onUpdateDragObject  // 0x40
  public            System.Boolean                  canStartDrag  // 0x48
  private           System.Boolean                  _endDragOnDisable  // 0x49
  public            System.Boolean                  disableDrag  // 0x4a
  private           System.Boolean                  _isInScrollList  // 0x4b
  private           System.Boolean                  _hideDragObjectInScrollList  // 0x4c
  private           UnityEngine.GameObject          _dragPrefab  // 0x50
  private           UnityEngine.RectTransform       _dragObjectParent  // 0x58
  public            UnityEngine.Vector2             dragPivot  // 0x60
  private           UnityEngine.UI.Image            _draggingHintImage  // 0x68
  private           UnityEngine.CanvasGroup         _draggingHintCanvasGroup  // 0x70
  private           UnityEngine.GameObject          _draggingHintObject  // 0x78
  private           Beyond.UI.LuaPanel              m_panel  // 0x80
  private           UnityEngine.RectTransform       m_rect  // 0x88
  private           Beyond.UI.UIScrollRect          m_scrollRect  // 0x90
  private           System.Boolean                  m_isInScroll  // 0x98
  private           UnityEngine.GameObject          m_curDragObj  // 0xa0
  private           System.Boolean                  m_inDragging  // 0xa8
  private           UnityEngine.Color               m_draggingHintImageOriColor  // 0xac
  private           UnityEngine.Coroutine           m_dragOnDisableCoroutine  // 0xc0
PROPERTIES:
  luaTable  get=0x0385B100  set=0x05392C40
  inDragging  get=0x0B0CCBB4
  panel  get=0x0B0CCCB4
  dragObjectParent  get=0x02E968A0  set=0x02E96C00
  m_dragParentRect  get=0x0B0CCC04
  curDragObj  get=0x0B0CCB64
METHODS:
  RVA=0x04950ED0  token=0x600052B  System.Void Awake()
  RVA=0x04845B50  token=0x600052C  System.Void Start()
  RVA=0x04D1A6F0  token=0x600052D  System.Void OnDisable()
  RVA=0x0B0CC92C  token=0x600052E  System.Collections.IEnumerator _ContinueDragOnDisable()
  RVA=0x0B0CC574  token=0x600052F  System.Void OnDestroy()
  RVA=0x04A79500  token=0x6000530  System.Void OnEnable()
  RVA=0x0B0CC014  token=0x6000531  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CC5D4  token=0x6000532  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04D1A730  token=0x6000533  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CC9BC  token=0x6000534  System.Void _StopDragView()
  RVA=0x0B0CBF90  token=0x6000535  System.Void ClearComponent()
  RVA=0x036D0E40  token=0x6000536  System.Void ClearEvents()
  RVA=0x039C2D00  token=0x6000537  System.Void .ctor()
  RVA=0x041E1670  token=0x6000538  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x6000539  System.Void <>iFixBaseProxy_Start()
  RVA=0x041E1670  token=0x600053A  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x041E1670  token=0x600053B  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x041E1670  token=0x600053C  System.Void <>iFixBaseProxy_OnEnable()
END_CLASS

CLASS: Beyond.UI.UIDropdown
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x290
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp Beyond.Input.IBindingGroupTarget Beyond.Input.IBindingView
FIELDS:
  public            UnityEngine.Events.UnityEvent<System.Int32>onSelectCell  // 0x188
  public            UnityEngine.Events.UnityEvent<System.Int32,Beyond.UI.UIDropdownOption,System.Boolean>onUpdateCell  // 0x190
  public            UnityEngine.Events.UnityEvent<System.Int32,Beyond.UI.UIDropdown>onUpdateCaption  // 0x198
  public            UnityEngine.Events.UnityEvent<System.Boolean>onToggleOptList  // 0x1a0
  public            System.Func<System.Int32,System.Int32,System.Boolean>onValidateSelectCell  // 0x1a8
  public            Beyond.UI.UIText                captionText  // 0x1b0
  public            Beyond.UI.UIImage               captionIcon  // 0x1b8
  private           System.Int32                    <selectedIndex>k__BackingField  // 0x1c0
  private           System.Int32                    <optionCount>k__BackingField  // 0x1c4
  private           System.Boolean                  <isListActive>k__BackingField  // 0x1c8
  private           Beyond.UI.UIDropdownOption      _optionTemplate  // 0x1d0
  private           UnityEngine.Transform           _arrowImageTrans  // 0x1d8
  private           UnityEngine.RectTransform       _listContainer  // 0x1e0
  private           System.Single                   _listContainerMaxHeight  // 0x1e8
  private           System.Single                   _animationTime  // 0x1ec
  private           UnityEngine.RectTransform       _selfArea  // 0x1f0
  private           UnityEngine.RectTransform       _maskRect  // 0x1f8
  private           System.Boolean                  _autoCloseOnSelect  // 0x200
  private           System.String                   _audioToggleOn  // 0x208
  private           System.String                   _audioToggleOff  // 0x210
  private           System.String                   _audioInvalid  // 0x218
  private           System.String                   _audioNavigation  // 0x220
  private           System.Collections.Generic.List<Beyond.UI.UIDropdownOption>m_options  // 0x228
  private           Beyond.UI.LuaPanel              m_panel  // 0x230
  private           UnityEngine.UI.LayoutGroup      m_content  // 0x238
  private           UnityEngine.Camera              m_fallbackUICamera  // 0x240
  private           Beyond.UI.UIScrollRect          m_listScrollRect  // 0x248
  private           UnityEngine.CanvasGroup         m_listContainerCanvasGroup  // 0x250
  public            System.String                   openDropDownActionId  // 0x258
  public            System.String                   openDropDownBindingTextId  // 0x260
  private           System.Int32                    m_bindingGroupId  // 0x268
  private           System.Int32                    m_naviBindingGroupId  // 0x26c
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0x270
  private           Beyond.UI.UISelectableNaviGroup m_listNaviGroup  // 0x278
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x280
  private   readonly Beyond.DisposedUnityEvent       <bindingViewUpdateEvent>k__BackingField  // 0x288
PROPERTIES:
  selectedIndex  get=0x04D8E9A0  set=0x04D8EA50
  optionCount  get=0x04D8E9B0  set=0x04D8EA60
  isListActive  get=0x04DA3320  set=0x04DAAE80
  panel  get=0x03F03420
  groupId  get=0x0B0CE604
  parentTarget  get=0x04D8DD90  set=0x06B8B7B0
  groupEnabled  get=0x0B0CE57C
  bindingViewActionId  get=0x0B0CE334
  bindingViewLabelText  get=0x0B0CE384
  bindingViewUpdateEvent  get=0x04D8D820
  redDotTrans  get=0x0B0CE654
  bindingEnabled  get=0x0B0CE2E4
  bindingViewState  get=0x0B0CE41C
METHODS:
  RVA=0x03EAD500  token=0x600054A  System.Void Awake()
  RVA=0x047F9CD0  token=0x600054B  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x03F02AA0  token=0x600054C  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04CCE880  token=0x600054D  System.Void Init(UnityEngine.Events.UnityAction<System.Int32,Beyond.UI.UIDropdownOption,System.Boolean> onUpdateCell, UnityEngine.Events.UnityAction<System.Int32> onSelectCell, UnityEngine.Events.UnityAction<System.Int32,Beyond.UI.UIDropdown> onUpdateCaption)
  RVA=0x03F03740  token=0x600054E  System.Void ToggleOptions(System.Boolean activeList)
  RVA=0x03F03790  token=0x600054F  System.Void _ToggleOptions(System.Boolean activeList, System.Boolean isInit)
  RVA=0x03F03060  token=0x6000550  System.Void Update()
  RVA=0x03F034D0  token=0x6000551  System.Void SetSelected(System.Int32 index, System.Boolean forceUpdate, System.Boolean needNotifySelect, System.Boolean validateSelect)
  RVA=0x0B0CDFE8  token=0x6000552  System.Void ScrollToSelected()
  RVA=0x0356CED0  token=0x6000553  System.Void Refresh(System.Int32 count, System.Int32 newSelectedIndex, System.Boolean needNotifySelect)
  RVA=0x04A37E10  token=0x6000554  System.Void ClearComponent()
  RVA=0x03F03230  token=0x6000555  UnityEngine.Camera _GetUICamera()
  RVA=0x0B0CDF28  token=0x6000560  System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType)
  RVA=0x0B0CDE30  token=0x6000561  System.Void Beyond.Input.IBindingView.OnInputKeyDown(System.Boolean isActionTrigger)
  RVA=0x0B0CDE84  token=0x6000562  System.Void Beyond.Input.IBindingView.OnInputKeyUp(System.Boolean isActionTrigger)
  RVA=0x03EAD680  token=0x6000563  System.Void _InitBinding()
  RVA=0x0B0CE190  token=0x6000564  System.Void _ControllerToggle()
  RVA=0x0B0CE124  token=0x6000565  System.Void _ControllerCancel()
  RVA=0x0B0CE208  token=0x6000566  UnityEngine.UI.Selectable _GetDefaultNaviTarget()
  RVA=0x04B450E0  token=0x6000567  System.Void OnEnable()
  RVA=0x04B45170  token=0x6000568  System.Void OnDisable()
  RVA=0x04A37D70  token=0x6000569  System.Void OnDestroy()
  RVA=0x047599C0  token=0x600056A  System.Void .ctor()
  RVA=0x0B0CDED8  token=0x600056B  System.Boolean Beyond.Input.IBindingView.get_interactable()
  RVA=0x0B0869D4  token=0x600056C  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0C581C  token=0x600056D  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x0B0869E4  token=0x600056E  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0447EC90  token=0x600056F  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0869DC  token=0x6000570  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIDropdownOption
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIButton              button  // 0x18
  public            Beyond.UI.UIText                label  // 0x20
  public            Beyond.UI.UIText                secondLabel  // 0x28
  public            Beyond.UI.UIImage               icon  // 0x30
  public            UnityEngine.GameObject          selectedNode  // 0x38
  public            UnityEngine.GameObject          notSelectedNode  // 0x40
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x48
  public            UnityEngine.GameObject          stripeNode  // 0x50
  public            System.Boolean                  stripeIsTop  // 0x58
METHODS:
  RVA=0x03F4AC10  token=0x6000575  System.Void SetText(System.String text)
  RVA=0x0B0CDD7C  token=0x6000576  System.Void SetState(System.String stateName)
  RVA=0x03698DF0  token=0x6000577  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CommonDropHintType
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CommonDropHintType    None  // const
  public    static  Beyond.UI.CommonDropHintType    Square  // const
  public    static  Beyond.UI.CommonDropHintType    Circle  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIDropItem
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x80
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IDropHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           XLua.LuaTable                   <luaTable>k__BackingField  // 0x18
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDropEvent  // 0x20
  public            UnityEngine.Events.UnityEvent<System.Boolean>onToggleHighlight  // 0x28
  public            System.Action                   onDestroy  // 0x30
  public            Beyond.UI.CommonDropHintType    commonDropHintType  // 0x38
  private           UnityEngine.GameObject          _highlightHint  // 0x40
  private           System.Boolean                  _scaleWhenHighlight  // 0x48
  private           System.Single                   _scaleWhenActive  // 0x4c
  private           System.Boolean                  _useUIState  // 0x50
  private           Beyond.UI.UIStyleByState.StateConfig_stateConfigNormal  // 0x58
  private           Beyond.UI.UIStyleByState.StateConfig_stateConfigHilight  // 0x60
  private           UnityEngine.UI.Selectable       m_selectable  // 0x68
  private           UnityEngine.RectTransform       m_rectTransform  // 0x70
  private           System.Single                   m_oriScale  // 0x78
  private           System.Boolean                  m_isInHighlight  // 0x7c
PROPERTIES:
  luaTable  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x036D0DF0  token=0x600057A  System.Void ClearEvents()
  RVA=0x047750F0  token=0x600057B  System.Void Awake()
  RVA=0x0456EF00  token=0x600057C  System.Void OnEnable()
  RVA=0x0456EEC0  token=0x600057D  System.Void OnDisable()
  RVA=0x0B0CDBB8  token=0x600057E  System.Void OnDestroy()
  RVA=0x0456EF60  token=0x600057F  System.Void ToggleHighlight(System.Boolean active, System.Boolean forceToggle)
  RVA=0x0B0CDC58  token=0x6000580  System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CDB20  token=0x6000581  System.Void ClearComponent()
  RVA=0x039C2BB0  token=0x6000582  System.Void .ctor()
  RVA=0x041E1670  token=0x6000583  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x6000584  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x041E1670  token=0x6000585  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x041E1670  token=0x6000586  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIElementFollower
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0xD0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           System.Boolean                  _followMouse  // 0x68
  public            System.Boolean                  followFixedPos  // 0x69
  private           UnityEngine.Transform           _targetTransform  // 0x70
  private           UnityEngine.Vector3             _followPosition  // 0x78
  private           System.Boolean                  _changeScale  // 0x84
  private           UnityEngine.AnimationCurve      _scaleCurve  // 0x88
  public            UnityEngine.Vector3             displayOffset  // 0x90
  public            UnityEngine.Vector3             displayUIOffset  // 0x9c
  private   static readonly Unity.Profiling.ProfilerMarker  UPDATE_POSITION_MARKER  // static @ 0x0
  public            Beyond.DisposedUnityEvent       onExitScreen  // 0xa8
  public            Beyond.DisposedUnityEvent       onEnterScreen  // 0xb0
  private           System.Boolean                  <inScreen>k__BackingField  // 0xb8
  private           Beyond.UI.LuaPanel              m_panel  // 0xc0
  private           UnityEngine.RectTransform       m_rectTransform  // 0xc8
PROPERTIES:
  tickOption  get=0x0460F810
  followPosition  get=0x0B0CEA54  set=0x0B0CEB18
  targetTransform  get=0x0B0CEAC8  set=0x0B0CEB9C
  inScreen  get=0x04D892C0  set=0x04D89320
  panel  get=0x03E4DC20
METHODS:
  RVA=0x04CC4BB0  token=0x600058F  System.Void OnAwake()
  RVA=0x03E4D9C0  token=0x6000590  System.Void OnEnable()
  RVA=0x03E4DA10  token=0x6000591  System.Void LateTick(System.Single deltaTime)
  RVA=0x03E4DA50  token=0x6000592  System.Void UpdatePosition()
  RVA=0x0B0CE6A0  token=0x6000593  System.Void UpdatePosition(UnityEngine.Vector3 worldPos)
  RVA=0x04735620  token=0x6000594  System.Void .ctor()
  RVA=0x04D38BA0  token=0x6000595  System.Void .cctor()
  RVA=0x069AFD0C  token=0x6000596  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000597  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x6000598  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x6000599  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIExtendScrollRect
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IScrollHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler
FIELDS:
  public            Beyond.UI.UIScrollRect          baseScrollRect  // 0x18
  private           System.Boolean                  _enableDrag  // 0x20
  private           Beyond.UI.UIScrollList          _extendScrollList  // 0x28
METHODS:
  RVA=0x0B0CEECC  token=0x600059A  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CEC08  token=0x600059B  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CECF4  token=0x600059C  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0CEDE0  token=0x600059D  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03695570  token=0x600059E  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.IUIFoldoutComp
TYPE:  interface
TOKEN: 0x20000A6
FIELDS:
PROPERTIES:
  defaultFoldOut  get=0x02FFF600
METHODS:
  RVA=-1  // abstract  token=0x60005A0  System.Void Refresh(System.Int32 index)
  RVA=-1  // abstract  token=0x60005A1  System.Void OnFoldOut(System.Boolean fastMode)
  RVA=-1  // abstract  token=0x60005A2  System.Void OnFoldIn(System.Boolean fastMode)
END_CLASS

CLASS: Beyond.UI.UIFoldoutComponent
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIFoldoutComp
FIELDS:
  protected         UnityEngine.GameObject          _foldContent  // 0x18
  protected         Beyond.UI.UINonUnifiedScrollList_nonUnifiedScrollList  // 0x20
  public            System.Single                   foldInSize  // 0x28
  public            System.Single                   duration  // 0x2c
  public            DG.Tweening.Ease                easeType  // 0x30
  protected         System.Boolean                  m_isFoldOut  // 0x34
  private           DG.Tweening.Tweener             m_tween  // 0x38
  protected         System.Int32                    m_index  // 0x40
PROPERTIES:
  defaultFoldOut  get=0x0B0CF5C8
METHODS:
  RVA=0x0B0CF54C  token=0x60005A3  System.Void Refresh(System.Int32 index)
  RVA=0x0B0CF294  token=0x60005A5  System.Void OnFoldOut(System.Boolean fastMode)
  RVA=0x0B0CEFE4  token=0x60005A6  System.Void OnFoldIn(System.Boolean fastMode)
  RVA=0x0B0CEF78  token=0x60005A7  System.Void OnDestroy()
  RVA=0x04D80BD0  token=0x60005A8  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIGraphicAnimation
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x90
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private   static  System.Int32                    s_vfxMainTextSTId  // static @ 0x0
  private   static  System.Int32                    s_tintColorAlpha  // static @ 0x4
  private           UnityEngine.UI.MaskableGraphic  _graphic  // 0x68
  private           UnityEngine.Material            _material  // 0x70
  private           System.Single                   _alpha  // 0x78
  private           UnityEngine.Vector2             _scale  // 0x7c
  private           UnityEngine.Material            m_materialInstance  // 0x88
PROPERTIES:
  tickOption  get=0x0416CC10
  MaterialInstance  get=0x049585B0
METHODS:
  RVA=0x035763F0  token=0x60005B1  System.Void OnAwake()
  RVA=0x0B0CF614  token=0x60005B2  System.Void OnRelease()
  RVA=0x033A0760  token=0x60005B3  System.Void LateTick(System.Single deltaTime)
  RVA=0x03576430  token=0x60005B4  System.Void _Init()
  RVA=0x039C44C0  token=0x60005B5  System.Void .ctor()
  RVA=0x04D22560  token=0x60005B6  System.Void .cctor()
  RVA=0x069AFD0C  token=0x60005B7  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60005B8  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x069AFD04  token=0x60005B9  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06BD8710  token=0x60005BA  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIGridLayoutGroup
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x78
EXTENDS: UnityEngine.UI.GridLayoutGroup
FIELDS:
METHODS:
  RVA=0x0B0CF718  token=0x60005BB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIGridLayoutNaviWrapper
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x58
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: IScrollListNaviWrapper
FIELDS:
  private           UnityEngine.UI.GridLayoutGroup  _gridLayoutGroup  // 0x18
  private           UnityEngine.UI.LayoutGroup      _layoutGroup  // 0x20
  private           Beyond.UI.UIGridLayoutNaviWrapper.WrapMode_wrapMode  // 0x28
  private           Beyond.UI.UIScrollRect          _scrollRect  // 0x30
  private           Beyond.UI.UISelectableNaviGroup _naviGroup  // 0x38
  private           System.Boolean                  _enableCircleWrapNavigation  // 0x40
  private           System.Boolean                  m_isInited  // 0x41
  private           System.Boolean                  m_isVertical  // 0x42
  private           UnityEngine.UI.LayoutGroup      m_layoutGroup  // 0x48
  private           System.Collections.Generic.List<UnityEngine.UI.Selectable>m_wrapNaviSelectables  // 0x50
METHODS:
  RVA=0x0B0CF948  token=0x60005BC  System.Void _InitIfNot()
  RVA=0x0B0CF720  token=0x60005BD  System.Void Start()
  RVA=0x0B0CF77C  token=0x60005BE  System.Boolean TryWrapNavigate(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0CFACC  token=0x60005BF  System.Void _InitWrapNavigation()
  RVA=0x0B0D02B4  token=0x60005C0  System.Void _NaviGridLayout(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0CFEF8  token=0x60005C1  System.Boolean _NaviGridLayoutImpl(UnityEngine.UI.NaviDirection dir, System.Boolean enableCircleWrapNavigation)
  RVA=0x0B0D0184  token=0x60005C2  System.Boolean _NaviGridLayoutInit(System.Boolean isBefore, System.Boolean enableCircleWrapNavigation)
  RVA=0x0B0CFC70  token=0x60005C3  System.Boolean _NaviChildren(System.Int32 curIndex, System.Int32 childCount, System.Boolean isBefore, System.Boolean enableCircleWrapNavigation)
  RVA=0x0B0CF7E0  token=0x60005C4  UnityEngine.UI.Selectable _FindValidSelectable(System.Boolean isBefore)
  RVA=0x0B0D031C  token=0x60005C5  System.Void _SetCircleWrapScroll(System.Boolean isBefore)
  RVA=0x039C1EA0  token=0x60005C6  System.Void .ctor()
  RVA=0x041E1670  token=0x60005C7  System.Void <>iFixBaseProxy_Start()
END_CLASS

CLASS: Beyond.UI.UIGroupScrollList
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x308
EXTENDS: Beyond.UI.UIScrollList
FIELDS:
  protected         UnityEngine.GameObject          _groupTitleTemplate  // 0x278
  protected         UnityEngine.GameObject          _groupBGCell  // 0x280
  private           System.Single                   _groupSpace  // 0x288
  public            System.Func<System.Int32,System.Int32>getCellCountInGroup  // 0x290
  public            System.Func<System.Int32,System.Single>getGroupTitleSize  // 0x298
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateGroupTitle  // 0x2a0
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateGroupBG  // 0x2a8
  private           System.Int32                    m_groupCount  // 0x2b0
  private           System.Int32[]                  m_cellCountPerGroup  // 0x2b8
  private           System.Int32[]                  m_groupFirstRow  // 0x2c0
  private           System.Int32                    m_totalRows  // 0x2c8
  private           System.Single                   m_defaultGroupTitleHeight  // 0x2cc
  private           System.Single                   m_defaultGroupTitleWidth  // 0x2d0
  private   readonly System.Collections.Generic.List<System.Single>m_groupTitleSizeList  // 0x2d8
  private           System.Collections.Generic.Stack<Beyond.UI.UIScrollList.Cell>m_titleCellCache  // 0x2e0
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIScrollList.Cell>m_showingTitles  // 0x2e8
  private           System.Collections.Generic.Stack<Beyond.UI.UIScrollList.Cell>m_groupBGCellCache  // 0x2f0
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIScrollList.Cell>m_showingGroupBGs  // 0x2f8
  private           System.Boolean                  m_isMultiPage  // 0x300
  private   static  System.String                   CACHED_TITLE_GAME_OBJECT_NAME  // const
  private   static  System.String                   CACHED_GROUP_BG_GAME_OBJECT_NAME  // const
PROPERTIES:
  totalCellCount  get=0x0B0D86F0
  isMultiPage  get=0x0B0D86A0
METHODS:
  RVA=0x0B0D324C  token=0x60005C9  System.Void _BuildGroupLayout()
  RVA=0x0B0D443C  token=0x60005CA  System.Void _EnsureGroupTitleSizeCacheCount(System.Int32 groupCount)
  RVA=0x0B0D46D8  token=0x60005CB  System.Single _GetDefaultGroupTitleSize()
  RVA=0x0B0D4314  token=0x60005CC  System.Single _CalcGroupTitleSize(System.Int32 groupIndex)
  RVA=0x0B0D5F58  token=0x60005CD  System.Void _RefreshAllGroupTitleSizes()
  RVA=0x0B0D5FDC  token=0x60005CE  System.Void _RefreshGroupTitleSizeWithoutEnsure(System.Int32 groupIndex)
  RVA=0x0B0D6084  token=0x60005CF  System.Void _RefreshGroupTitleSize(System.Int32 groupIndex)
  RVA=0x0B0D5020  token=0x60005D0  System.Single _GetGroupTitleSize(System.Int32 groupIndex)
  RVA=0x0B0D5224  token=0x60005D1  System.Boolean _GetRowInfo(System.Int32 row, System.Int32& groupIndex, System.Boolean& isTitleRow, System.Int32& cellRowInGroup)
  RVA=0x0B0D5164  token=0x60005D2  System.Single _GetRowHeight(System.Int32 row)
  RVA=0x0B0D4620  token=0x60005D3  System.Single _GetCumulativeSizeBeforeRow(System.Int32 row)
  RVA=0x0B0D50D0  token=0x60005D4  System.Int32 _GetRowForCell(System.Int32 groupIndex, System.Int32 cellIndexInGroup)
  RVA=0x0B0D457C  token=0x60005D5  System.Int32 _GetCellIndexForGroupCell(System.Int32 groupIndex, System.Int32 cellIndexInGroup)
  RVA=0x0B0D4744  token=0x60005D6  System.Boolean _GetGroupAndCellFromCellIndex(System.Int32 cellIndex, System.Int32& groupIndex, System.Int32& cellIndexInGroup)
  RVA=0x0B0D4A84  token=0x60005D7  System.Void _GetGroupCellIndexRange(System.Int32 groupIndex, System.Int32& startCellIndex, System.Int32& endCellIndex)
  RVA=0x0B0D4D64  token=0x60005D8  UnityEngine.Vector2Int _GetGroupRangeFromScrollOffset(System.Single scrollOffset, System.Single viewSize, System.Single bufferSize)
  RVA=0x0B0D4C8C  token=0x60005D9  UnityEngine.Vector2Int _GetGroupRangeFromCellIndexRange(UnityEngine.Vector2Int cellIndexRange)
  RVA=0x0B0D58B0  token=0x60005DA  UnityEngine.Vector2Int _MergeGroupRange(UnityEngine.Vector2Int lhs, UnityEngine.Vector2Int rhs)
  RVA=0x0B0D537C  token=0x60005DB  System.ValueTuple<System.Single,System.Single> _GetScrollInfo()
  RVA=0x0B0D259C  token=0x60005DC  System.Void SyncViewSize()
  RVA=0x0B0D1810  token=0x60005DD  System.Void InGameAwake()
  RVA=0x0B0D2A74  token=0x60005DE  System.Void UpdateCount(System.Int32 newCount)
  RVA=0x0B0D2AC8  token=0x60005DF  System.Void UpdateCount(System.Int32 newCount, System.Int32 fastScrollToIndex, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D2CD4  token=0x60005E0  System.Void UpdateGroup(System.Int32 groupCount)
  RVA=0x0B0D2D48  token=0x60005E1  System.Void UpdateGroup(System.Int32 groupCount, System.Boolean setTop, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow)
  RVA=0x0B0D2B60  token=0x60005E2  System.Void UpdateGroup(System.Int32 groupCount, System.Int32 fastScrollToIndex, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D2F70  token=0x60005E3  System.Void UpdateSingleGroup(System.Int32 groupIndex, System.Nullable<System.Int32> newCellCount)
  RVA=0x0B0D1980  token=0x60005E4  System.Void NotifyGroupTitleSizeChange(System.Int32 groupIndex)
  RVA=0x0B0D041C  token=0x60005E5  System.Int32 AppendCellToGroup(System.Int32 groupIndex)
  RVA=0x0B0D1AF0  token=0x60005E6  System.Boolean RemoveLastCellFromGroup(System.Int32 groupIndex)
  RVA=0x0B0D4F90  token=0x60005E7  System.Int32 _GetGroupStartCellIndex(System.Int32 groupIndex)
  RVA=0x0B0D61A8  token=0x60005E8  System.Void _RemapShowingCellIndicesAfterGroupResize(System.Int32 groupIndex, System.Int32 oldCellCount, System.Int32 newCellCount)
  RVA=0x0B0D54B0  token=0x60005E9  System.Int32 _GetScrollTargetRow(System.Int32 groupIndex, System.Int32 cellIndexInGroup, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D65FC  token=0x60005EA  System.Void _ScrollToRow(System.Int32 row, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D6548  token=0x60005EB  System.Void _ScrollToGroup(System.Int32 groupIndex, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D20B8  token=0x60005EC  System.Void ScrollToGroup(System.Int32 groupIndex, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType, System.Boolean graduallyShowInFastMode)
  RVA=0x0B0D21B4  token=0x60005ED  System.Void ScrollToIndex(System.Int32 index, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType, System.Boolean graduallyShowInFastMode)
  RVA=0x0B0D2288  token=0x60005EE  System.Void ScrollToIndex(System.Int32 groupIndex, System.Int32 cellIndexInGroup, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType, System.Boolean graduallyShowInFastMode)
  RVA=0x0B0D1018  token=0x60005EF  UnityEngine.Vector2Int GetGroupRangeInView(System.Single bufferSize)
  RVA=0x0B0D6104  token=0x60005F1  System.Void _RefreshIsMultiPage()
  RVA=0x0B0D2A18  token=0x60005F2  System.Void UpdateContainerSize()
  RVA=0x0B0D0E54  token=0x60005F3  System.Single GetContainerSize(System.Boolean ignoreAutoLayout)
  RVA=0x0B0D11AC  token=0x60005F4  UnityEngine.Vector2Int GetIndexRangeFromScrollOffset(System.Single scrollOffset, System.Single viewSize, System.Single bufferSize)
  RVA=0x0B0D5804  token=0x60005F5  System.Boolean _IsGroupInCellRange(System.Int32 groupIndex, System.Int32 startCellIndex, System.Int32 endCellIndex)
  RVA=0x0B0D5C94  token=0x60005F6  System.Void _MoveTitleTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 groupIndex)
  RVA=0x0B0D5954  token=0x60005F7  System.Void _MoveCellTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 groupIndex, System.Int32 cellIndexInGroup)
  RVA=0x0B0D18D4  token=0x60005F8  System.Void MoveCellTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0B0D141C  token=0x60005F9  System.Single GetTargetPosByIndex(System.Int32 index, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0D5560  token=0x60005FA  Beyond.UI.UIScrollList.Cell _GetTitleCell()
  RVA=0x0B0D424C  token=0x60005FB  System.Void _CacheTitleCell(Beyond.UI.UIScrollList.Cell cell)
  RVA=0x0B0D481C  token=0x60005FC  Beyond.UI.UIScrollList.Cell _GetGroupBGCell()
  RVA=0x0B0D4184  token=0x60005FD  System.Void _CacheGroupBGCell(Beyond.UI.UIScrollList.Cell cell)
  RVA=0x0B0D4B54  token=0x60005FE  System.Single _GetGroupLength(System.Int32 groupIndex)
  RVA=0x0B0D5AD8  token=0x60005FF  System.Void _MoveGroupBGTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 groupIndex)
  RVA=0x0B0D6688  token=0x6000600  System.Void _UpdateGroupBGByRange(UnityEngine.Vector2Int groupRange, System.Boolean forceUpdateContent, System.Boolean ignoreListActive)
  RVA=0x0B0D2E04  token=0x6000601  System.Void UpdateShowingCells(System.Boolean forceUpdateContent, System.Boolean forceClearShowingCells, System.Boolean ignoreListActive)
  RVA=0x0B0D052C  token=0x6000602  System.Void CacheOutOfRangeCells(UnityEngine.Vector2Int showRange, UnityEngine.Vector2Int notCacheRange)
  RVA=0x0B0D1A28  token=0x6000603  System.Void ProcessCellAtIndex(System.Int32 index, System.Boolean forceUpdateContent, System.Boolean ignoreListActive)
  RVA=0x0B0D1BF0  token=0x6000604  System.Void RepositionOutOfRangeCells(UnityEngine.Vector2Int showRange)
  RVA=0x0B0D7F88  token=0x6000605  System.Void _UpdateTitleOrCell(System.Boolean isTitle, System.Int32 groupIndex, System.Int32 cellIndexInGroup, System.Int32 cellIndex, System.Boolean forceUpdateContent, System.Boolean ignoreListActive)
  RVA=0x0B0D5E24  token=0x6000606  System.Void _OnUpdateTitleOrCell(Beyond.UI.UIScrollList.Cell cell, System.Boolean isTitle, System.Int32 groupIndex, System.Int32 cellIndexInGroup)
  RVA=0x0B0D68F8  token=0x6000607  System.Void _UpdateShowingCellsForGroup(System.Int32 dirtyGroupIndex)
  RVA=0x0B0D2868  token=0x6000608  System.Boolean TryResolveWrapDirection(UnityEngine.UI.NaviDirection dir, System.Boolean& isBefore)
  RVA=0x0B0D2664  token=0x6000609  System.Boolean TryGetWrapTargetIndex(UnityEngine.UI.NaviDirection dir, System.Int32 currentIndex, System.Boolean isBefore, System.Int32& targetIndex)
  RVA=0x0B0D43A0  token=0x600060A  System.Int32 _CellsInGroupRow(System.Int32 row, System.Int32 totalRowsInGroup, System.Int32 cellCount)
  RVA=0x0B0D0B48  token=0x600060B  System.Void ClearShowingCells()
  RVA=0x0B0D0A60  token=0x600060C  System.Void ClearComponent()
  RVA=0x0B0D1664  token=0x600060D  UnityEngine.GameObject Get(System.Int32 cellIndex)
  RVA=0x0B0D1714  token=0x600060E  UnityEngine.GameObject Get(System.Int32 groupIndex, System.Int32 cellIndexInGroup)
  RVA=0x0B0D10F4  token=0x600060F  UnityEngine.GameObject GetGroupTitle(System.Int32 groupIndex)
  RVA=0x0B0D23DC  token=0x6000610  System.Void SetCellCanCache(System.Int32 cellIndex, System.Boolean canCache)
  RVA=0x0B0D2484  token=0x6000611  System.Void SetCellCanCache(System.Int32 groupIndex, System.Int32 cellIndexInGroup, System.Boolean canCache)
  RVA=0x0B0D0DF0  token=0x6000612  System.String DebugDumpLayoutToConsoleAndClipboard()
  RVA=0x0B0D3508  token=0x6000613  System.String _BuildLayoutDebugDumpText()
  RVA=0x0B0D8460  token=0x6000614  System.Void .ctor()
  RVA=0x0B0D29D8  token=0x6000615  System.Void <>iFixBaseProxy_SyncViewSize()
  RVA=0x0B0D29A0  token=0x6000616  System.Void <>iFixBaseProxy_InGameAwake()
  RVA=0x0B0D29C0  token=0x6000617  System.Void <>iFixBaseProxy_ScrollToIndex(System.Int32 P0, System.Boolean P1, Beyond.UI.UIScrollList.ScrollAlignType P2, System.Boolean P3)
  RVA=0x0B0D2A10  token=0x6000618  System.Boolean <>iFixBaseProxy_get_isMultiPage()
  RVA=0x0B0D29F8  token=0x6000619  System.Void <>iFixBaseProxy_UpdateContainerSize()
  RVA=0x0B0D2980  token=0x600061A  System.Single <>iFixBaseProxy_GetContainerSize(System.Boolean P0)
  RVA=0x0B0D2988  token=0x600061B  UnityEngine.Vector2Int <>iFixBaseProxy_GetIndexRangeFromScrollOffset(System.Single P0, System.Single P1, System.Single P2)
  RVA=0x0B0B93C4  token=0x600061C  System.Void <>iFixBaseProxy_MoveCellTo(Beyond.UI.UIScrollList.Cell P0, System.Int32 P1)
  RVA=0x0B0D2998  token=0x600061D  System.Single <>iFixBaseProxy_GetTargetPosByIndex(System.Int32 P0, Beyond.UI.UIScrollList.ScrollAlignType P1)
  RVA=0x0B0D2A00  token=0x600061E  System.Void <>iFixBaseProxy_UpdateShowingCells(System.Boolean P0, System.Boolean P1, System.Boolean P2)
  RVA=0x0B0D2968  token=0x600061F  System.Void <>iFixBaseProxy_CacheOutOfRangeCells(UnityEngine.Vector2Int P0, UnityEngine.Vector2Int P1)
  RVA=0x0B0D29A8  token=0x6000620  System.Void <>iFixBaseProxy_ProcessCellAtIndex(System.Int32 P0, System.Boolean P1, System.Boolean P2)
  RVA=0x0B0D29B8  token=0x6000621  System.Void <>iFixBaseProxy_RepositionOutOfRangeCells(UnityEngine.Vector2Int P0)
  RVA=0x0B0D29F0  token=0x6000622  System.Boolean <>iFixBaseProxy_TryResolveWrapDirection(UnityEngine.UI.NaviDirection P0, System.Boolean& P1)
  RVA=0x0B0D29E0  token=0x6000623  System.Boolean <>iFixBaseProxy_TryGetWrapTargetIndex(UnityEngine.UI.NaviDirection P0, System.Int32 P1, System.Boolean P2, System.Int32& P3)
  RVA=0x0B0D2978  token=0x6000624  System.Void <>iFixBaseProxy_ClearShowingCells()
  RVA=0x0B0D2970  token=0x6000625  System.Void <>iFixBaseProxy_ClearComponent()
END_CLASS

CLASS: Beyond.UI.UIGyroscopeEffect
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0xE0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Boolean                  enableDetect  // 0x68
  public            System.Single                   mobileGravityScaler  // 0x6c
  public            System.Single                   controllerGravityScaler  // 0x70
  public            System.Single                   controllerAccSmoothFactor  // 0x74
  public            Beyond.UI.UIGyroscopeEffect.Paramx  // 0x78
  public            Beyond.UI.UIGyroscopeEffect.Paramy  // 0x80
  public            DG.Tweening.Ease                ease  // 0x88
  public            System.Single                   time  // 0x8c
  private           UnityEngine.Vector3             m_lastValue  // 0x90
  private           Cinemachine.CinemachineVirtualCameram_vcam  // 0xa0
  private           Beyond.UI.CinemachineGyroscopeEffectm_vcamGyroscopeEffect  // 0xa8
  private           System.Boolean                  m_isVcamMode  // 0xb0
  private           System.Single                   m_baseGravityY  // 0xb4
  private           System.Single                   m_baseGravityYAccumulatedTime  // 0xb8
  private           System.Single                   m_lastGravityY  // 0xbc
  private           UnityEngine.Vector3             m_smoothedControllerAccValue  // 0xc0
  public            DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions>handler  // 0xd0
  public            DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>tween  // 0xd8
PROPERTIES:
  tickOption  get=0x048B9F30
METHODS:
  RVA=0x04CB6830  token=0x6000627  System.Void OnAwake()
  RVA=0x03403E30  token=0x6000628  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0D9E2C  token=0x6000629  System.Void OnRelease()
  RVA=0x0496E710  token=0x600062A  System.Void .ctor()
  RVA=0x0B0D9ED4  token=0x600062B  UnityEngine.Vector3 <Tick>b__22_0()
  RVA=0x0B0D9F08  token=0x600062C  System.Void <Tick>b__22_1(UnityEngine.Vector3 value)
  RVA=0x069AFD0C  token=0x600062D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600062E  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x600062F  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x069AFD04  token=0x6000630  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UIHighlightMask
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.RectTransform       _up  // 0x68
  private           UnityEngine.RectTransform       _down  // 0x70
  private           UnityEngine.RectTransform       _left  // 0x78
  private           UnityEngine.RectTransform       _right  // 0x80
  private           UnityEngine.RectTransform       m_rect  // 0x88
  private           Beyond.UI.LuaPanel              m_panel  // 0x90
PROPERTIES:
  tickOption  get=0x0B0DA48C
  panel  get=0x0B0DA3C8
METHODS:
  RVA=0x0B0D9F30  token=0x6000634  System.Void OnAwake()
  RVA=0x0B0D9FC0  token=0x6000635  System.Void OnEnable()
  RVA=0x0B0DA01C  token=0x6000636  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0DA07C  token=0x6000637  System.Void TrySyncRect()
  RVA=0x039C2B00  token=0x6000638  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000639  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600063A  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x600063B  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x600063C  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIImage
TYPE:  class
TOKEN: 0x20000B2
SIZE:  0x228
EXTENDS: UnityEngine.UI.HGImage
FIELDS:
  private           System.Int32                    _atlasHandleId  // 0x190
  private           System.Collections.Generic.List<Beyond.UI.UIImage>_referencedImages  // 0x198
  private           Beyond.UI.UIAtlasManager.UIAtlasHandlem_atlasHandle  // 0x1a0
  private           System.Action                   m_onEnableRuntimeAtlas  // 0x1c8
  private           Beyond.UI.LuaPanel              m_panel  // 0x1d0
  private           System.Boolean                  m_hasBeenEnabled  // 0x1d8
  private           System.String                   m_cachedSpritePath  // 0x1e0
  protected         Beyond.Resource.FAssetProxyHandlem_spriteResHandle  // 0x1e8
  protected         Beyond.Resource.FAssetProxyHandlem_matResHandle  // 0x1fc
  public    static  System.String                   SPRITE_PATH_FORMAT  // const
  public    static  System.String                   SPRITE_ROOT_PATH_FORMAT  // const
  public            System.Boolean                  enableAutoImgUsePathReference  // 0x210
  public            System.Boolean                  imgUsePathReference  // 0x211
  public            System.String                   imgRefPath  // 0x218
  private           System.Boolean                  m_imgIsPathReference  // 0x220
PROPERTIES:
  packIntoRuntimeAtlas  get=0x0B0DB984  set=0x0B0DBA24
  atlasHandleId  get=0x0B0DB8E4  set=0x030938F0
  panel  get=0x031EBBA0
  atlasHandle  get=0x0B0DB934
  panelLevel  get=0x0B0DB9D4
  color  get=0x031BABD0  set=0x030466A0
  mainTexture  get=0x031EDB00
METHODS:
  RVA=0x0309A110  token=0x6000644  System.Void RegisterOnEnableRuntimeAtlas(System.Action callBack)
  RVA=0x0309BFF0  token=0x6000645  System.Void UnregisterOnEnableRuntimeAtlas(System.Action callBack)
  RVA=0x0B0DB7F8  token=0x6000646  System.Void OnEnableRuntimeAtlas()
  RVA=0x03093770  token=0x6000649  System.Void _OnSpriteChanged(UnityEngine.Sprite lastOne)
  RVA=0x03FC41E0  token=0x600064A  System.Void OnPopulateMeshFallback(UnityEngine.UI.VertexHelper toFill)
  RVA=0x03093A70  token=0x600064B  System.Void Awake()
  RVA=0x031EB820  token=0x600064C  System.Void OnEnable()
  RVA=0x0351A0A0  token=0x600064D  System.Void OnDisable()
  RVA=0x03093F30  token=0x600064E  System.Void OnDestroy()
  RVA=0x031EB3E0  token=0x600064F  System.Void SetNativeSize()
  RVA=0x0B0DB854  token=0x6000650  System.Void SetNativeSizeIgnoreRefScale()
  RVA=0x03914E50  token=0x6000651  System.Void LoadSprite(System.String folder, System.String spriteName)
  RVA=0x045F23B0  token=0x6000652  System.Void LoadSprite(System.String path)
  RVA=0x036D4790  token=0x6000653  System.Void LoadSpriteWithOutFormat(System.String path, System.Boolean ignoreEnable)
  RVA=0x030486F0  token=0x6000654  System.Void _LoadSprite(System.String path, System.Boolean ignoreEnable)
  RVA=0x0B0DB5BC  token=0x6000655  System.Void LoadMaterial(System.String path, System.Boolean newInstance)
  RVA=0x04695BB0  token=0x6000656  System.Void ReleaseSprite()
  RVA=0x03094090  token=0x6000657  System.Void DisposeSpriteHandle()
  RVA=0x041A6140  token=0x6000659  System.Void _V2AtlasInitialize()
  RVA=0x03695F50  token=0x600065A  System.Void .ctor()
  RVA=0x04D8C200  token=0x600065B  UnityEngine.Color <>iFixBaseProxy_get_color()
  RVA=0x0B0C6930  token=0x600065C  System.Void <>iFixBaseProxy_set_color(UnityEngine.Color P0)
  RVA=0x0B0DB8D4  token=0x600065D  System.Void <>iFixBaseProxy__OnSpriteChanged(UnityEngine.Sprite P0)
  RVA=0x041E1670  token=0x600065E  System.Void <>iFixBaseProxy_OnPopulateMeshFallback(UnityEngine.UI.VertexHelper P0)
  RVA=0x0B0DB8A4  token=0x600065F  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0DB8BC  token=0x6000660  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0DB8B4  token=0x6000661  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0DB8AC  token=0x6000662  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0DB8CC  token=0x6000663  System.Void <>iFixBaseProxy_SetNativeSize()
  RVA=0x0B0DB8C4  token=0x6000664  System.Void <>iFixBaseProxy_ReleaseSprite()
  RVA=0x0B0DB8DC  token=0x6000665  UnityEngine.Texture <>iFixBaseProxy_get_mainTexture()
END_CLASS

CLASS: Beyond.UI.UIImageBlur
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0xB8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Single                   scale  // 0x68
  private           UnityEngine.RectTransform       m_rect  // 0x70
  private           System.Single                   m_lastScale  // 0x78
  private           Beyond.UI.UIImage               m_image  // 0x80
  private           UnityEngine.Texture2D           m_srcTex  // 0x88
  private           UnityEngine.RenderTexture       m_tarTex  // 0x90
  private           System.Int32                    m_uiImageBlurTexId  // 0x98
  private           UnityEngine.Rect                m_atlasRect  // 0x9c
  private           System.Action                   m_dynamicCallback  // 0xb0
PROPERTIES:
  tickOption  get=0x04A355B0
METHODS:
  RVA=0x037E33F0  token=0x6000667  System.Void OnAwake()
  RVA=0x037E1FD0  token=0x6000668  System.Void OnChangeSprite()
  RVA=0x0B0DA560  token=0x6000669  System.Void Tick(System.Single deltaTime)
  RVA=0x0484FBD0  token=0x600066A  System.Void OnDisable()
  RVA=0x0B0DA4DC  token=0x600066B  System.Void OnRelease()
  RVA=0x039C2B00  token=0x600066C  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600066D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600066E  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x600066F  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x01538B30  token=0x6000670  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x069AFD04  token=0x6000671  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UIImageFillAmountOffset
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIImage               m_followImage  // 0x18
  public            System.Single                   fillOffset  // 0x20
  private           System.Single                   m_fillAmount  // 0x24
PROPERTIES:
  fillAmount  get=0x0B0DA850  set=0x0B0DA8A0
METHODS:
  RVA=0x0B0DA6BC  token=0x6000674  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOFillAmount(System.Single endValue, System.Single duration)
  RVA=0x05393520  token=0x6000675  System.Void .ctor()
  RVA=0x0B0DA840  token=0x6000676  System.Single <DOFillAmount>b__6_0()
  RVA=0x0B0DA848  token=0x6000677  System.Void <DOFillAmount>b__6_1(System.Single x)
END_CLASS

CLASS: Beyond.UI.UIImageFillFollower
TYPE:  class
TOKEN: 0x20000B5
SIZE:  0x80
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private   static  System.Single                   BACK_FOLLOW_THRESHOLD  // const
  private           Beyond.UI.UIImage               _frontFill  // 0x68
  private           System.Single                   _followSpeed  // 0x70
  private           Beyond.UI.UIImage               m_image  // 0x78
PROPERTIES:
  tickOption  get=0x0B0DAC70
METHODS:
  RVA=0x0B0DA9D8  token=0x6000679  System.Void OnAwake()
  RVA=0x0B0DAA7C  token=0x600067A  System.Void OnEnable()
  RVA=0x0B0DAB5C  token=0x600067B  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0DAAF0  token=0x600067C  System.Void RefreshFillAmount()
  RVA=0x0B0DAC60  token=0x600067D  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600067E  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600067F  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x6000680  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x6000681  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIImageFillTailFollower
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x88
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIImage               _followImage  // 0x68
  public            System.Single                   followOffset  // 0x70
  private           UnityEngine.RectTransform       m_selfRectTransform  // 0x78
  private           UnityEngine.RectTransform       m_followImageRectTransform  // 0x80
PROPERTIES:
  tickOption  get=0x0B0DB108
METHODS:
  RVA=0x0B0DACC0  token=0x6000683  System.Void OnAwake()
  RVA=0x0B0DAD84  token=0x6000684  System.Void OnEnable()
  RVA=0x0B0DB09C  token=0x6000685  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0DADEC  token=0x6000686  System.Void RefreshFollower()
  RVA=0x039C2B00  token=0x6000687  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000688  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000689  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x600068A  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x600068B  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIImagePair
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Collections.Generic.List<Beyond.UI.UIImage>referencedImages  // 0x18
PROPERTIES:
  color  set=0x0B0DB42C
  first  get=0x0B0DB394
METHODS:
  RVA=0x0B0DB284  token=0x600068C  System.Void LoadSprite(System.String folder, System.String spriteName)
  RVA=0x0B0DB158  token=0x600068D  System.Void LoadSprite(System.String path)
  RVA=0x0B0DB314  token=0x6000690  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIInertiaViewPager
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0xF0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.UI.UIScrollRect          _scrollRect  // 0x68
  private           System.Single                   _animationDuration  // 0x70
  private           System.Single                   _endScrollSpd  // 0x74
  private           Beyond.UI.UIInertiaViewPager.ScrollEffectConfig.Timing_timing  // 0x78
  private           System.Single                   _minScrollInterval  // 0x7c
  private           System.String                   _audioGear  // 0x80
  private           System.String                   _audioGearLock  // 0x88
  public            System.Boolean                  flingToNextEnable  // 0x90
  public            System.Single                   flingToNextMinSpd  // 0x94
  public            System.Single                   flingToNextMaxSpd  // 0x98
  private           System.Int32                    m_currentPage  // 0x9c
  private           Beyond.UI.UIInertiaViewPager.Statem_state  // 0xa0
  private           Beyond.UI.UIInertiaViewPager.IScrollHandlerm_scrollHandler  // 0xa8
  private           Beyond.UI.UIInertiaViewPager.ScrollEffectTriggerm_effectTrigger  // 0xb0
  private           Beyond.UI.UIInertiaViewPager.BlockerManagerm_blockers  // 0xb8
  private           Beyond.UI.UIInertiaViewPager.DragContextm_dragContext  // 0xc0
  private           System.Int32                    m_fromPage  // 0xc8
  private           System.Int32                    m_toPage  // 0xcc
  private           System.Single                   m_tweenStartTime  // 0xd0
  private           System.Single                   m_tweenStartValue  // 0xd4
  private           System.Single                   m_tweenTargetValue  // 0xd8
  private           System.Boolean                  m_isInited  // 0xdc
  private           System.Int32                    <pageCount>k__BackingField  // 0xe0
  public            Beyond.DisposedUnityEvent<System.Int32>onStateChanged  // 0xe8
PROPERTIES:
  tickOption  get=0x0B0DCA94
  isUpdating  get=0x0B0DCA40
  pageCount  get=0x04D9A880  set=0x04DA3470
  currentPage  get=0x0B0DC988  set=0x0B0DCAE4
  currentScrollIndex  get=0x0B0DC9D8
METHODS:
  RVA=0x0B0DC508  token=0x6000692  System.Void _PlayAudioGear()
  RVA=0x0B0DC48C  token=0x6000693  System.Void _PlayAudioGearLock()
  RVA=0x0B0DC11C  token=0x6000694  System.Void _InitIfNot()
  RVA=0x0B0DBA80  token=0x600069B  System.Void ClearComponent()
  RVA=0x0B0DBD2C  token=0x600069C  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0DBCA4  token=0x600069D  System.Void SetPageCount(System.Int32 pageCount, System.Collections.Generic.List<System.Int32> segmentFrames)
  RVA=0x0B0DBB48  token=0x600069E  System.Void MoveToPage(System.Int32 pageIndex)
  RVA=0x0B0DBBD8  token=0x600069F  System.Void OnAwake()
  RVA=0x0B0DBC34  token=0x60006A0  System.Void OnEnable()
  RVA=0x0B0DC584  token=0x60006A1  System.Single _ScrollValue2PageIndex(System.Single value)
  RVA=0x0B0DC410  token=0x60006A2  System.Single _PageIndex2ScrollValue(System.Single index)
  RVA=0x0B0DC5FC  token=0x60006A3  System.Int32 _ScrollValueAlignToPage(System.Single value)
  RVA=0x0B0DC680  token=0x60006A4  System.Void _SwitchToPage(System.Int32 targetIndex, System.Boolean useTween)
  RVA=0x0B0DC0A4  token=0x60006A5  System.Void _AutoAlign()
  RVA=0x0B0DC338  token=0x60006A6  System.Void _OnStateChanged(Beyond.UI.UIInertiaViewPager.State from, Beyond.UI.UIInertiaViewPager.State to)
  RVA=0x0B0DC84C  token=0x60006A7  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60006A8  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x60006A9  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06C294C4  token=0x60006AA  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x60006AB  System.Void <>iFixBaseProxy_OnEnable()
END_CLASS

CLASS: Beyond.UI.UIInputField
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x448
EXTENDS: TMPro.TMP_InputField
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            System.String                   activeInputActionId  // 0x390
  public            System.String                   deactivateInputActionId  // 0x398
  private           System.Int32                    <activeInputBindingId>k__BackingField  // 0x3a0
  private           System.Int32                    <deactivateInputBindingId>k__BackingField  // 0x3a4
  private           Beyond.UI.LuaPanel              m_panel  // 0x3a8
  public            System.Boolean                  isDebugInputField  // 0x3b0
  private           UnityEngine.Camera              m_fallbackUICamera  // 0x3b8
  private           UnityEngine.GameObject          m_fallbackUIRoot  // 0x3c0
  private           UnityEngine.RectTransform       m_fallbackRectTransform  // 0x3c8
  private           System.Int32                    m_activateBindingGroupId  // 0x3d0
  private           System.Boolean                  m_isNaviFocused  // 0x3d4
  private           System.Boolean                  m_isCloudInputting  // 0x3d5
  private           Beyond.UI.UIState.UIStateController_uiStateController  // 0x3d8
  private           System.String                   _onFocusStateName  // 0x3e0
  private           System.String                   _onFocusLostStateName  // 0x3e8
  private           System.String                   _audioPressStart  // 0x3f0
  public            UnityEngine.RectTransform       m_keyboardOffsetTarget  // 0x3f8
  public            UnityEngine.RectTransform       m_offsetTargetParent  // 0x400
  public            UnityEngine.RectTransform       m_inputFieldRect  // 0x408
  public            System.Int32                    m_pcDebug  // 0x410
  public            System.Single                   m_keyboardExtraOffsetY  // 0x414
  private           UnityEngine.Vector2             m_baseKeyboardOffsetTarget  // 0x418
  private           UnityEngine.Vector2             m_baseKeyboardLocalPos  // 0x420
  private           System.Single                   m_inputFieldOffsetY  // 0x428
  private           System.Boolean                  m_enableKeyboardOffsetCal  // 0x42c
  private           UnityEngine.Coroutine           m_heightChangeCoroutine  // 0x430
  private           System.Int32                    m_lastHeight  // 0x438
  private           System.Int32                    m_nowHeight  // 0x43c
  public            System.String                   audioOnActive  // 0x440
  private   static  Beyond.UI.UIInputField          s_mFocusedInputField  // static @ 0x0
PROPERTIES:
  activeInputBindingId  get=0x04DAAE90  set=0x04DAAEB0
  deactivateInputBindingId  get=0x04DAAEA0  set=0x04DAAEC0
  panel  get=0x0B0DD0C8
  isControllerInputValid  get=0x03BA8140
  s_focusedInputField  get=0x03BA80F0  set=0x03BA86F0
METHODS:
  RVA=0x04B47C70  token=0x60006D8  System.Void SendOnFocus()
  RVA=0x04CB1920  token=0x60006D9  System.Void SendOnFocusLost()
  RVA=0x037D5FA0  token=0x60006DA  System.Void Awake()
  RVA=0x04CA2A60  token=0x60006DB  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x04B6C8C0  token=0x60006DC  System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x037987D0  token=0x60006DD  System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x03BA7EB0  token=0x60006DE  System.Void ActivateInputField()
  RVA=0x04CC9BB0  token=0x60006DF  System.Boolean TryActivateInputFieldCustom()
  RVA=0x03BA7FB0  token=0x60006E0  System.Void DeactivateInputField(System.Boolean clearSelection)
  RVA=0x047A96A0  token=0x60006E1  System.Void OnDestroy()
  RVA=0x02EDCBD0  token=0x60006E2  System.Void OnEnable()
  RVA=0x02EDCC10  token=0x60006E3  System.Void OnDisable()
  RVA=0x046B5530  token=0x60006E4  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0B0DCB48  token=0x60006E5  System.Void OnInteractableChanged()
  RVA=0x02EDCC50  token=0x60006E6  System.Void _UpdateBindingEnable()
  RVA=0x047A9790  token=0x60006E7  System.Void ClearComponent()
  RVA=0x0B0DCF10  token=0x60006E8  System.Void _OnCloudGameKeyboardComplete(System.String value, System.Boolean submit)
  RVA=0x0B0DCC34  token=0x60006E9  UnityEngine.Camera _GetCamera()
  RVA=0x047A98C0  token=0x60006EA  System.Void _VirtualKeyboardOnDestroy()
  RVA=0x04CA2AB0  token=0x60006EB  System.Void _VirtualKeyboardSelect()
  RVA=0x04B6C910  token=0x60006EC  System.Void _VirtualKeyboardDeselect()
  RVA=0x037D6430  token=0x60006ED  System.Void _VirtualKeyboardAwake()
  RVA=0x0B0DD074  token=0x60006EE  System.Void _UpdateKeyboardOffset(System.Int32 offsetHeight)
  RVA=0x0B0DCBF0  token=0x60006EF  System.Void _CleanupAndroidObjects()
  RVA=0x037D6470  token=0x60006F0  System.Void _VirtualKeyboardInitJavaObj()
  RVA=0x0B0DCE74  token=0x60006F1  System.Collections.IEnumerator _KeyboardHeightChange(System.Action<System.Int32> onHeightChanged)
  RVA=0x0405B310  token=0x60006F4  System.Void .ctor()
  RVA=0x0B0DCB98  token=0x60006F5  System.Void <Awake>b__28_0()
  RVA=0x0B0DCBE8  token=0x60006F6  System.Void <>iFixBaseProxy_SendOnFocus()
  RVA=0x0B0DCBE0  token=0x60006F7  System.Void <>iFixBaseProxy_SendOnFocusLost()
  RVA=0x0B0869D4  token=0x60006F8  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0DCBD0  token=0x60006F9  System.Void <>iFixBaseProxy_OnSelect(UnityEngine.EventSystems.BaseEventData P0)
  RVA=0x0B0DCBB8  token=0x60006FA  System.Void <>iFixBaseProxy_OnDeselect(UnityEngine.EventSystems.BaseEventData P0)
  RVA=0x0B0DCBD8  token=0x60006FB  System.Void <>iFixBaseProxy_OnUpdateSelected(UnityEngine.EventSystems.BaseEventData P0)
  RVA=0x0B0DCBA8  token=0x60006FC  System.Void <>iFixBaseProxy_ActivateInputField()
  RVA=0x012081B0  token=0x60006FD  System.Boolean <>iFixBaseProxy_TryActivateInputFieldCustom()
  RVA=0x0B0DCBB0  token=0x60006FE  System.Void <>iFixBaseProxy_DeactivateInputField(System.Boolean P0)
  RVA=0x0B0869DC  token=0x60006FF  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0DCBC8  token=0x6000700  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0DCBC0  token=0x6000701  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0C581C  token=0x6000702  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x041E1670  token=0x6000703  System.Void <>iFixBaseProxy_OnInteractableChanged()
END_CLASS

CLASS: Beyond.UI.UIJoystick
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x188
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp Beyond.Input.IBindingGroupTarget
FIELDS:
  private           UnityEngine.RectTransform       _runBg  // 0x18
  private           UnityEngine.RectTransform       _walkBg  // 0x20
  private           UnityEngine.RectTransform       _thumb  // 0x28
  private           UnityEngine.RectTransform       _directionIndicate  // 0x30
  private           UnityEngine.RectTransform       _walkDirectionIndicate  // 0x38
  private           UnityEngine.RectTransform       _rootCanvas  // 0x40
  private           UnityEngine.RectTransform       _triggerArea  // 0x48
  private           UnityEngine.UI.GraphicRaycaster _raycaster  // 0x50
  private           System.Single                   _walkRation  // 0x58
  private           System.Single                   _autoSprintStartLength  // 0x5c
  private           System.Single                   _autoSprintStopLength  // 0x60
  private           System.Single                   _autoSprintStartAngle  // 0x64
  private           System.Single                   _autoSprintStopAngle  // 0x68
  public            System.Single                   margin  // 0x6c
  public            System.Single                   bgVisibleTimeWhenEnable  // 0x70
  private           System.Single                   m_bgHideTime  // 0x74
  private           System.Boolean                  _isDynamic  // 0x78
  private           Beyond.UI.LuaPanel              m_panel  // 0x80
  private           UnityEngine.Vector2             <jsValue>k__BackingField  // 0x88
  private           Beyond.UI.UITouchPanel          _dynamicArea  // 0x90
  private           System.Boolean                  _dynamicButDontMove  // 0x98
  private           System.Boolean                  _useRewiredPlayerInput  // 0x99
  private           System.String                   _controllerRewiredActionX  // 0xa0
  private           System.String                   _controllerRewiredActionY  // 0xa8
  private           Beyond.Input.KeyboardKeyCode    _keyboardUp  // 0xb0
  private           Beyond.Input.KeyboardKeyCode    _keyboardDown  // 0xb4
  private           Beyond.Input.KeyboardKeyCode    _keyboardLeft  // 0xb8
  private           Beyond.Input.KeyboardKeyCode    _keyboardRight  // 0xbc
  private           System.Boolean                  m_active  // 0xc0
  private           System.Boolean                  m_activatedByTouch  // 0xc1
  public            Beyond.DisposedUnityEvent       onTouchStart  // 0xc8
  public            Beyond.DisposedUnityEvent       onTouchEnd  // 0xd0
  public            Beyond.DisposedUnityEvent       onMoveStart  // 0xd8
  public            Beyond.DisposedUnityEvent<UnityEngine.Vector2>onMove  // 0xe0
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0xe8
  public            Beyond.DisposedUnityEvent<System.Boolean>onToggleAutoSprint  // 0xf0
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xf8
  private           UnityEngine.RectTransform       m_rect  // 0x100
  private           UnityEngine.Vector2             m_oriAnchorMin  // 0x108
  private           UnityEngine.Vector2             m_oriAnchorMax  // 0x110
  private           UnityEngine.Vector2             m_oriAnchorPos  // 0x118
  private           UnityEngine.Vector2             m_oriPivot  // 0x120
  private           System.Boolean                  m_isWalking  // 0x128
  private           System.Boolean                  m_isRunning  // 0x129
  private           System.Boolean                  m_isSprinting  // 0x12a
  private           Beyond.UI.UIAnimationWrapper    m_walkBgAnim  // 0x130
  private           Beyond.UI.UIAnimationWrapper    m_runBgAnim  // 0x138
  private           System.Boolean                  m_isWalkBgVisible  // 0x140
  private           System.Boolean                  m_isRunBgVisible  // 0x141
  private           System.Collections.Generic.List<UnityEngine.InputSystem.UI.ExtendedPointerEventData>m_exPointers  // 0x148
  private           UnityEngine.InputSystem.UI.ExtendedPointerEventDatam_fakePointerEventData  // 0x150
  private           UnityEngine.InputSystem.UI.ExtendedPointerEventDatam_activePointerEventData  // 0x158
  private           Beyond.UI.UICustomLayoutElement m_customLayoutElement  // 0x160
  private           UnityEngine.Vector2             m_lastFrameActiveStickVal  // 0x168
  private           UnityEngine.EventSystems.PointerEventData<curDragEventData>k__BackingField  // 0x170
  private           System.Int32                    m_groupId  // 0x178
  private           System.Boolean                  m_bindingInit  // 0x17c
  private           Beyond.Input.InputBindingGroupMonoTarget<parent>k__BackingField  // 0x180
PROPERTIES:
  panel  get=0x0B0DD8E4
  isDynamic  get=0x03092CB0  set=0x0B0DDA48
  walkRation  get=0x0B0DD9F8  set=0x039D4DF0
  active  get=0x04CC5A60
  activeTouchId  get=0x0B0DD828
  jsValue  get=0x04D9E750  set=0x04D85ED0
  m_thumbPosMaxLength  get=0x03790D10
  curDragEventData  get=0x04D8DEF0  set=0x06B8B940
  groupEnabled  get=0x03792ED0
  shouldCheckInput  get=0x03792E40
  groupId  get=0x0B0DD88C
  parentTarget  get=0x0B0DD9A8
  parent  get=0x04D8DD60  set=0x06B8B774
METHODS:
  RVA=0x0B0DD64C  token=0x6000716  System.Void Reset()
  RVA=0x039D3170  token=0x6000717  System.Void Awake()
  RVA=0x04B4B8D0  token=0x6000718  System.Void OnEnable()
  RVA=0x04CE2A30  token=0x6000719  System.Void Start()
  RVA=0x0B0DD294  token=0x600071A  System.Void OnDestroy()
  RVA=0x037913E0  token=0x600071B  System.Void _Tick()
  RVA=0x0B0DD4C4  token=0x600071C  System.Void OnDisable()
  RVA=0x03091360  token=0x600071D  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x044C0620  token=0x600071E  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x046FDA20  token=0x600071F  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037901B0  token=0x6000720  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x042EE960  token=0x6000721  System.Void CheckShouldActive()
  RVA=0x039D4BE0  token=0x6000722  System.Void _SetVisible(System.Boolean visible)
  RVA=0x039D48D0  token=0x6000723  System.Void _ChangeDynamic(System.Boolean dynamic, System.Boolean force)
  RVA=0x03092D80  token=0x6000724  System.Void _SetRectTransform(System.Boolean toOri)
  RVA=0x039D4C40  token=0x6000725  System.Void _UpdateWalkBgScale()
  RVA=0x03092BD0  token=0x6000726  System.Void _Activate(System.Boolean fromTouch, UnityEngine.Vector2 showPos, UnityEngine.Vector2 initJsValue)
  RVA=0x03791310  token=0x6000727  System.Void _UpdateGait()
  RVA=0x03790D90  token=0x6000728  System.Void _UpdateIndicator()
  RVA=0x03791120  token=0x6000729  System.Void _UpdateBg()
  RVA=0x037911D0  token=0x600072A  System.Void _SetWalkBgVisible(System.Boolean visible, System.Boolean ignoreAnim)
  RVA=0x03791250  token=0x600072B  System.Void _SetRunBgVisible(System.Boolean visible, System.Boolean ignoreAnim)
  RVA=0x0B0DD728  token=0x600072C  System.Void _OnHudLayoutLoaded(Beyond.EventData<System.String>& eventData)
  RVA=0x0B0DD18C  token=0x600072D  System.Void ClearComponent()
  RVA=0x039D46A0  token=0x6000734  System.Void InitBinding()
  RVA=0x039D47F0  token=0x6000735  System.Void _UpdateState()
  RVA=0x03EB2780  token=0x6000736  System.Void .ctor()
  RVA=0x0B0DD71C  token=0x6000737  System.Void <Awake>b__76_0(UnityEngine.Vector2 _)
END_CLASS

CLASS: Beyond.UI.UILayoutDimensionListener
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: Beyond.IHotfixable UnityEngine.UI.ICanvasElement
FIELDS:
  private           Beyond.ListSet<Beyond.UI.UILayoutDimensionListener.IAction>m_actionsWhenLayoutReady  // 0x18
  public            Beyond.DisposedUnityEvent       eventOnPostLayout  // 0x20
METHODS:
  RVA=0x0B0DDB3C  token=0x6000738  System.Void GraphicUpdateComplete()
  RVA=0x0B0DDB80  token=0x6000739  System.Void LayoutComplete()
  RVA=0x0B0DDCFC  token=0x600073A  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x0B0DDFE0  token=0x600073B  System.Collections.IEnumerator _NextFrameCoroutine()
  RVA=0x0B0DDAAC  token=0x600073C  System.Void DoOnceOnPostLayout(Beyond.UI.UILayoutDimensionListener.IAction action)
  RVA=0x0B0DDC5C  token=0x600073D  System.Void OnEnable()
  RVA=0x0B0DDCAC  token=0x600073E  System.Void OnRectTransformDimensionsChange()
  RVA=0x0B0DDBC4  token=0x600073F  System.Void OnDestroy()
  RVA=0x0B0DE070  token=0x6000740  System.Void _SetDirty()
  RVA=0x0B0DDDCC  token=0x6000741  System.Void _InvokePostLayoutCallback()
  RVA=0x04B21870  token=0x6000742  System.Void .ctor()
  RVA=0x0B0DDD7C  token=0x6000743  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
  RVA=0x041E1670  token=0x6000744  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x041E1670  token=0x6000745  System.Void <>iFixBaseProxy_OnRectTransformDimensionsChange()
  RVA=0x041E1670  token=0x6000746  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UILeftSubtitle
TYPE:  class
TOKEN: 0x20000CC
SIZE:  0xB0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIDialogTextWithCursor_dialogText1  // 0x68
  private           Beyond.UI.UIDialogTextWithCursor_dialogText2  // 0x70
  private           Beyond.UI.UIDialogTextWithCursor_dialogText3  // 0x78
  private           Beyond.UI.UIDialogTextWithCursor_dialogText4  // 0x80
  private           Beyond.UI.UIAnimationWrapper    _panelAnimation  // 0x88
  private           System.Int32                    m_maxDisplayIndex  // 0x90
  private   readonly System.Collections.Generic.List<Beyond.UI.UIDialogTextWithCursor>m_dialogTextList  // 0x98
  private           System.Boolean                  m_isDisplaying  // 0xa0
  private           System.Single                   m_currTime  // 0xa4
  private           System.Single                   m_textStayTime  // 0xa8
  private           System.Boolean                  m_isAutoPlay  // 0xac
PROPERTIES:
  tickOption  get=0x0B0DE9A8
METHODS:
  RVA=0x0B0DE750  token=0x600074F  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0DE6E0  token=0x6000750  System.Void StartAutoPlay(System.Single textStayTime)
  RVA=0x0B0DE1F4  token=0x6000751  System.Void SetLeftSubTitle(System.String text1, System.String text2, System.String text3, System.String text4)
  RVA=0x0B0DE814  token=0x6000752  System.Single UpdateByPlayTime(System.Single currTime)
  RVA=0x0B0DE0FC  token=0x6000753  System.Void Exit()
  RVA=0x0B0DE670  token=0x6000754  System.Void SetVisible(System.Boolean visible)
  RVA=0x0B0DE914  token=0x6000755  System.Void .ctor()
  RVA=0x0B0DE808  token=0x6000756  System.Void <Exit>b__17_0()
  RVA=0x069AFD0C  token=0x6000757  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x6000758  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIMaterialAnimation
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0x240
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private   static  System.Single                   FLOAT_SET_EPS  // const
  private   static  System.Single                   VECTOR_SET_EPS  // const
  private   static  System.Single                   COLOR_SET_EPS  // const
  private           UnityEngine.UI.MaskableGraphic  _maskableGraphic  // 0x68
  private           UnityEngine.Material            _material  // 0x70
  private           System.String                   _propertiesName1  // 0x78
  private           System.Single                   _propertiesSet1  // 0x80
  private           System.String                   _propertiesName2  // 0x88
  private           System.Single                   _propertiesSet2  // 0x90
  private           System.String                   _propertiesName3  // 0x98
  private           System.Single                   _propertiesSet3  // 0xa0
  private           System.String                   _propertiesName4  // 0xa8
  private           System.Single                   _propertiesSet4  // 0xb0
  private           System.String                   _propertiesName5  // 0xb8
  private           System.Single                   _propertiesSet5  // 0xc0
  private           System.String                   _propertiesName6  // 0xc8
  private           System.Single                   _propertiesSet6  // 0xd0
  private           System.String                   _propertiesName7  // 0xd8
  private           System.Single                   _propertiesSet7  // 0xe0
  private           System.String                   _propertiesName8  // 0xe8
  private           System.Single                   _propertiesSet8  // 0xf0
  private           System.String                   _propertiesName9  // 0xf8
  private           System.Single                   _propertiesSet9  // 0x100
  private           System.String                   _propertiesName10  // 0x108
  private           System.Single                   _propertiesSet10  // 0x110
  private           System.String                   _propertiesVectorName1  // 0x118
  private           UnityEngine.Vector4             _propertiesVector1  // 0x120
  private           System.String                   _propertiesVectorName2  // 0x130
  private           UnityEngine.Vector4             _propertiesVector2  // 0x138
  private           System.String                   _propertiesVectorName3  // 0x148
  private           UnityEngine.Vector4             _propertiesVector3  // 0x150
  private           System.String                   _propertiesVectorName4  // 0x160
  private           UnityEngine.Vector4             _propertiesVector4  // 0x168
  private           System.String                   _propertiesVectorName5  // 0x178
  private           UnityEngine.Vector4             _propertiesVector5  // 0x180
  private           System.String                   _propertiesColorName1  // 0x190
  private           UnityEngine.Color               _propertiesColor1  // 0x198
  private           System.String                   _propertiesColorName2  // 0x1a8
  private           UnityEngine.Color               _propertiesColor2  // 0x1b0
  private           System.String                   _propertiesColorName3  // 0x1c0
  private           UnityEngine.Color               _propertiesColor3  // 0x1c8
  private           UnityEngine.Material            m_matIns  // 0x1d8
  private           UnityEngine.Material            m_cachedMaterial  // 0x1e0
  private           System.String                   m_cachedFloatName1  // 0x1e8
  private           System.String                   m_cachedFloatName2  // 0x1f0
  private           System.String                   m_cachedFloatName3  // 0x1f8
  private           System.String                   m_cachedVectorName1  // 0x200
  private           System.String                   m_cachedColorName1  // 0x208
  private           System.Single                   m_cachedFloatValue1  // 0x210
  private           System.Single                   m_cachedFloatValue2  // 0x214
  private           System.Single                   m_cachedFloatValue3  // 0x218
  private           UnityEngine.Vector4             m_cachedVectorValue1  // 0x21c
  private           UnityEngine.Color               m_cachedColorValue1  // 0x22c
PROPERTIES:
  tickOption  get=0x044DA7C0
METHODS:
  RVA=0x03B70D30  token=0x600075A  System.Void LateTick(System.Single deltaTime)
  RVA=0x045E18D0  token=0x600075B  System.Void OnAwake()
  RVA=0x03B70C70  token=0x600075C  System.Void OnRelease()
  RVA=0x03B71650  token=0x600075D  System.Void _ResetSetCache()
  RVA=0x0B0DEA48  token=0x600075E  System.Boolean _NearlyEqual(System.Single left, System.Single right)
  RVA=0x0B0D88E4  token=0x600075F  System.Boolean _NearlyEqual(UnityEngine.Vector4 left, UnityEngine.Vector4 right)
  RVA=0x0B0D88E4  token=0x6000760  System.Boolean _NearlyEqual(UnityEngine.Color left, UnityEngine.Color right)
  RVA=0x0B0DEB2C  token=0x6000761  System.Void _TrySetFloatCached(UnityEngine.Material material, System.String propertyName, System.Single newValue, System.String& cachedPropertyName, System.Single& cachedValue)
  RVA=0x0B0DEBC4  token=0x6000762  System.Void _TrySetVectorCached(UnityEngine.Material material, System.String propertyName, UnityEngine.Vector4 newValue, System.String& cachedPropertyName, UnityEngine.Vector4& cachedValue)
  RVA=0x0B0DEA64  token=0x6000763  System.Void _TrySetColorCached(UnityEngine.Material material, System.String propertyName, UnityEngine.Color newValue, System.String& cachedPropertyName, UnityEngine.Color& cachedValue)
  RVA=0x03B70DB0  token=0x6000764  System.Void _TryUpdate()
  RVA=0x0B0DE9F8  token=0x6000765  System.Void ForceUpdate()
  RVA=0x039C2B00  token=0x6000766  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000767  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000768  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x06C294C4  token=0x6000769  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x069AFD04  token=0x600076A  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UIMultiSelectDropdown
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0x290
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp Beyond.Input.IBindingGroupTarget Beyond.Input.IBindingView
FIELDS:
  public            UnityEngine.Events.UnityEvent<System.Int32,System.Boolean>onValueChanged  // 0x188
  public            UnityEngine.Events.UnityEvent<System.Int32,Beyond.UI.UIDropdownOption,System.Boolean>onUpdateCell  // 0x190
  public            UnityEngine.Events.UnityEvent<System.Int32,Beyond.UI.UIMultiSelectDropdown>onUpdateCaption  // 0x198
  public            UnityEngine.Events.UnityEvent<System.Boolean>onToggleOptList  // 0x1a0
  public            System.Func<System.Int32,System.Boolean,System.Boolean>onValidateValueChanged  // 0x1a8
  public            Beyond.UI.UIText                captionText  // 0x1b0
  public            Beyond.UI.UIImage               captionIcon  // 0x1b8
  private           System.Int32                    <optionCount>k__BackingField  // 0x1c0
  private           System.Int32                    <selectedCount>k__BackingField  // 0x1c4
  private           System.Int32                    <lastSelectedIndex>k__BackingField  // 0x1c8
  private           System.Boolean                  <isListActive>k__BackingField  // 0x1cc
  private           Beyond.UI.UIDropdownOption      _optionTemplate  // 0x1d0
  private           UnityEngine.Transform           _arrowImageTrans  // 0x1d8
  private           UnityEngine.RectTransform       _listContainer  // 0x1e0
  private           System.Single                   _listContainerMaxHeight  // 0x1e8
  private           System.Single                   _animationTime  // 0x1ec
  private           UnityEngine.RectTransform       _selfArea  // 0x1f0
  private           UnityEngine.RectTransform       _maskRect  // 0x1f8
  private           System.String                   _audioToggleOn  // 0x200
  private           System.String                   _audioToggleOff  // 0x208
  private           System.String                   _audioInvalid  // 0x210
  private           System.String                   _audioNavigation  // 0x218
  private   readonly System.Collections.Generic.List<Beyond.UI.UIDropdownOption>m_options  // 0x220
  private   readonly System.Collections.Generic.List<System.Boolean>m_selectedStates  // 0x228
  private           Beyond.UI.LuaPanel              m_panel  // 0x230
  private           UnityEngine.UI.LayoutGroup      m_content  // 0x238
  private           UnityEngine.Camera              m_fallbackUICamera  // 0x240
  private           Beyond.UI.UIScrollRect          m_listScrollRect  // 0x248
  private           UnityEngine.CanvasGroup         m_listContainerCanvasGroup  // 0x250
  public            System.String                   openDropDownActionId  // 0x258
  public            System.String                   openDropDownBindingTextId  // 0x260
  private           System.Int32                    m_bindingGroupId  // 0x268
  private           System.Int32                    m_naviBindingGroupId  // 0x26c
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0x270
  private           Beyond.UI.UISelectableNaviGroup m_listNaviGroup  // 0x278
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x280
  private   readonly Beyond.DisposedUnityEvent       <bindingViewUpdateEvent>k__BackingField  // 0x288
PROPERTIES:
  optionCount  get=0x04D8E9A0  set=0x04D8EA50
  selectedCount  get=0x04D8E9B0  set=0x04D8EA60
  lastSelectedIndex  get=0x04DAAEE0  set=0x04DAAF00
  isListActive  get=0x04DAAED0  set=0x04DAAEF0
  panel  get=0x0B0E1D08
  groupId  get=0x0B0E1CB8
  parentTarget  get=0x04D8DD90  set=0x06B8B7B0
  groupEnabled  get=0x0B0E1C30
  bindingViewActionId  get=0x0B0E19E8
  bindingViewLabelText  get=0x0B0E1A38
  bindingViewUpdateEvent  get=0x04D8D820
  redDotTrans  get=0x0B0E1DCC
  bindingEnabled  get=0x0B0E1998
  bindingViewState  get=0x0B0E1AD0
METHODS:
  RVA=0x0B0DEC70  token=0x6000774  System.Void Awake()
  RVA=0x0B0E1008  token=0x6000775  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0B0DF7A8  token=0x6000776  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0DF40C  token=0x6000777  System.Void Init(UnityEngine.Events.UnityAction<System.Int32,Beyond.UI.UIDropdownOption,System.Boolean> onUpdateCell, UnityEngine.Events.UnityAction<System.Int32,System.Boolean> onValueChanged, UnityEngine.Events.UnityAction<System.Int32,Beyond.UI.UIMultiSelectDropdown> onUpdateCaption)
  RVA=0x0B0E0484  token=0x6000778  System.Void ToggleOptions(System.Boolean activeList)
  RVA=0x0B0E1190  token=0x6000779  System.Void _ToggleOptions(System.Boolean activeList, System.Boolean isInit)
  RVA=0x0B0E04E8  token=0x600077A  System.Void Update()
  RVA=0x0B0E0154  token=0x600077B  System.Void SetSelected(System.Int32 index, System.Boolean isOn, System.Boolean forceUpdate, System.Boolean needNotifySelect, System.Boolean validateSelect)
  RVA=0x0B0DFF94  token=0x600077C  System.Void SetSelectedList(System.Collections.Generic.IEnumerable<System.Int32> indices, System.Boolean isOn, System.Boolean needNotifySelect, System.Boolean validateSelect)
  RVA=0x0B0DF06C  token=0x600077D  System.Void ClearSelected(System.Boolean notify)
  RVA=0x0B0DF510  token=0x600077E  System.Boolean IsSelected(System.Int32 index)
  RVA=0x0B0DF208  token=0x600077F  System.Collections.Generic.List<System.Int32> GetSelectedIndices()
  RVA=0x0B0DF30C  token=0x6000780  System.Collections.Generic.List<System.Boolean> GetSelectedStates()
  RVA=0x0B0DFF38  token=0x6000781  System.Void ScrollToLastSelected()
  RVA=0x0B0DFDFC  token=0x6000782  System.Void ScrollToIndex(System.Int32 index)
  RVA=0x0B0DFD90  token=0x6000783  System.Void Refresh(System.Int32 count)
  RVA=0x0B0DF88C  token=0x6000784  System.Void Refresh(System.Int32 count, System.Collections.Generic.IEnumerable<System.Int32> selectedIndices, System.Boolean needNotifyValueChanged)
  RVA=0x0B0DEF04  token=0x6000785  System.Void ClearComponent()
  RVA=0x0B0E0FA4  token=0x6000786  System.Boolean _IsValidIndex(System.Int32 index)
  RVA=0x0B0E10BC  token=0x6000787  System.Void _ResizeSelectedStates(System.Int32 count)
  RVA=0x0B0E096C  token=0x6000788  System.Int32 _GetLastSelectedIndex()
  RVA=0x0B0E0A08  token=0x6000789  UnityEngine.Camera _GetUICamera()
  RVA=0x0B0DF148  token=0x6000794  System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType)
  RVA=0x0B0DEE0C  token=0x6000795  System.Void Beyond.Input.IBindingView.OnInputKeyDown(System.Boolean isActionTrigger)
  RVA=0x0B0DEE60  token=0x6000796  System.Void Beyond.Input.IBindingView.OnInputKeyUp(System.Boolean isActionTrigger)
  RVA=0x0B0E0C4C  token=0x6000797  System.Void _InitBinding()
  RVA=0x0B0E0788  token=0x6000798  System.Void _ControllerToggle()
  RVA=0x0B0E071C  token=0x6000799  System.Void _ControllerCancel()
  RVA=0x0B0E0800  token=0x600079A  UnityEngine.UI.Selectable _GetDefaultNaviTarget()
  RVA=0x0B0DF708  token=0x600079B  System.Void OnEnable()
  RVA=0x0B0DF668  token=0x600079C  System.Void OnDisable()
  RVA=0x0B0DF5B0  token=0x600079D  System.Void OnDestroy()
  RVA=0x0B0E16CC  token=0x600079E  System.Void .ctor()
  RVA=0x0B0DEEB4  token=0x600079F  System.Boolean Beyond.Input.IBindingView.get_interactable()
  RVA=0x0B0869D4  token=0x60007A0  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0C581C  token=0x60007A1  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x0B0869E4  token=0x60007A2  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0447EC90  token=0x60007A3  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0869DC  token=0x60007A4  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UINonUnifiedScrollList
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x2D8
EXTENDS: Beyond.UI.UIScrollList
FIELDS:
  public            Beyond.UI.UINonUnifiedScrollList.GetCellSizeModegetCellSizeMode  // 0x278
  private           System.Boolean                  _recalculateAfterSizeChange  // 0x27c
  private           System.Boolean                  _forceUpdateCellSizeCache  // 0x27d
  public            System.Func<System.Int32,System.Int32>getSubCellCount  // 0x280
  public            System.Func<System.Int32,System.Single>getCellSize  // 0x288
  public            System.Single                   overrideContainSize  // 0x290
  private           UnityEngine.UI.GridLayoutGroup  m_templateCellGridLayoutGroup  // 0x298
  private           System.Collections.Generic.Dictionary<System.Int32,System.Single>m_cellSizeCache  // 0x2a0
  private           System.Int32                    m_containerTopIndex  // 0x2a8
  private   static  System.Single                   CONTAINER_SIZE_BUFFER  // const
  public            System.Single                   additiveContainerSizeBuffer  // 0x2ac
  private           System.Single                   m_lastScrollStep  // 0x2b0
  private           System.Boolean                  m_isRecalculatingSize  // 0x2b4
  private           System.Boolean                  m_isCalculatingContainerSize  // 0x2b5
  private           System.Int32                    m_averageCount  // 0x2b8
  private           System.Single                   m_averageCellSize  // 0x2bc
  private           System.Boolean                  m_needRecalculateAverage  // 0x2c0
  private           System.Collections.Generic.Dictionary<System.Int32,System.Boolean>m_cellFoldStateDic  // 0x2c8
  private           Beyond.UI.IUIFoldoutComp        m_cellTemplateFoldoutComp  // 0x2d0
PROPERTIES:
  currentStep  get=0x0B0E50E0  set=0x0B0E52A4
METHODS:
  RVA=0x0B0E3668  token=0x60007A9  System.Void InitBasicInfo()
  RVA=0x0B0E1F84  token=0x60007AA  System.Void CalcSomeCountNum()
  RVA=0x0B0E2D94  token=0x60007AB  System.Single GetLeftLength(System.Boolean checkVertical, System.Int32 checkCount)
  RVA=0x0B0E4C30  token=0x60007AC  System.Void UpdateCount(System.Int32 newCount)
  RVA=0x0B0E494C  token=0x60007AD  System.Void UpdateCount(System.Int32 newCount, System.Boolean setTop, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow)
  RVA=0x0B0E4A0C  token=0x60007AE  System.Void UpdateCount(System.Int32 newCount, System.Int32 fastScrollToIndex, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0E3730  token=0x60007AF  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0E3978  token=0x60007B0  System.Void OnScroll(UnityEngine.Vector2 v)
  RVA=0x0B0E29CC  token=0x60007B1  UnityEngine.Vector2 GetGridLayoutSize(UnityEngine.UI.GridLayoutGroup grid, System.Int32 count)
  RVA=0x0B0E4D00  token=0x60007B2  System.Single _GetCellSize(System.Int32 index)
  RVA=0x0B0E252C  token=0x60007B3  System.Single GetContainerSize(System.Boolean ignoreAutoLayout)
  RVA=0x0B0E22C8  token=0x60007B4  System.Single GetCenterIndex()
  RVA=0x0B0E3174  token=0x60007B5  UnityEngine.Vector2Int GetShowRange(System.Single bufferSize, System.Boolean ignoreGraduallyShow)
  RVA=0x0B0E2F04  token=0x60007B6  UnityEngine.Vector2Int GetNotCacheRange()
  RVA=0x0B0E37AC  token=0x60007B7  System.Void MoveCellTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0B0E3A00  token=0x60007B8  System.Void OnUpdateCell(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0B0E3430  token=0x60007B9  System.Single GetTargetPosByIndex(System.Int32 index, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0E5008  token=0x60007BA  System.Single _GetCellsSizeTo(System.Int32 index)
  RVA=0x0B0E3CB4  token=0x60007BB  System.Void ScrollTo(System.Int32 index, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x0B0E1FEC  token=0x60007BC  System.Void ClearComponent()
  RVA=0x0B0E4248  token=0x60007BF  System.Void SetStepTo(System.Single step)
  RVA=0x0B0E3F80  token=0x60007C0  System.Void SetCurrentStep(System.Single currentStep)
  RVA=0x0B0E2FDC  token=0x60007C1  System.Single GetScrollBarSize()
  RVA=0x0B0E2BAC  token=0x60007C2  System.Single GetLastScrollStep()
  RVA=0x0B0E4CA4  token=0x60007C3  System.Void UpdateLastScrollStep()
  RVA=0x0B0E2E4C  token=0x60007C4  System.Single GetNormalizedPosition()
  RVA=0x0B0E4180  token=0x60007C5  System.Void SetNormalizedPosition(System.Single normalizedPosition)
  RVA=0x0B0E42BC  token=0x60007C6  System.Void ToggleByState(System.Int32 index, System.Boolean isFoldOut, System.Boolean fastMode)
  RVA=0x0B0E44A0  token=0x60007C7  System.Void Toggle(System.Int32 index, System.Boolean fastMode)
  RVA=0x0B0E2098  token=0x60007C8  System.Void FoldAll(System.Boolean isFoldOut)
  RVA=0x0B0E38AC  token=0x60007C9  System.Void NotifyCellSizeChange(System.Int32 index, System.Single size)
  RVA=0x039C1F80  token=0x60007CA  System.Void .ctor()
  RVA=0x0B0E46C8  token=0x60007CB  System.Void <GetScrollBarSize>g__RecalculateAverageCellSize|43_0()
  RVA=0x0B0E48BC  token=0x60007CC  System.Void <>iFixBaseProxy_InitBasicInfo()
  RVA=0x0B0E4894  token=0x60007CD  System.Void <>iFixBaseProxy_CalcSomeCountNum()
  RVA=0x0B0E48A4  token=0x60007CE  System.Single <>iFixBaseProxy_GetLeftLength(System.Boolean P0, System.Int32 P1)
  RVA=0x0B0E4934  token=0x60007CF  System.Void <>iFixBaseProxy_UpdateCount(System.Int32 P0)
  RVA=0x0B0E4914  token=0x60007D0  System.Void <>iFixBaseProxy_UpdateCount(System.Int32 P0, System.Boolean P1, System.Boolean P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B0E48F4  token=0x60007D1  System.Void <>iFixBaseProxy_UpdateCount(System.Int32 P0, System.Int32 P1, System.Boolean P2, System.Boolean P3, System.Boolean P4, Beyond.UI.UIScrollList.ScrollAlignType P5)
  RVA=0x0B0E48C4  token=0x60007D2  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x0B0E48CC  token=0x60007D3  System.Void <>iFixBaseProxy_OnScroll(UnityEngine.Vector2 P0)
  RVA=0x0B0D2980  token=0x60007D4  System.Single <>iFixBaseProxy_GetContainerSize(System.Boolean P0)
  RVA=0x0B0E489C  token=0x60007D5  System.Single <>iFixBaseProxy_GetCenterIndex()
  RVA=0x0B0E48B4  token=0x60007D6  UnityEngine.Vector2Int <>iFixBaseProxy_GetShowRange(System.Single P0, System.Boolean P1)
  RVA=0x0B0E48AC  token=0x60007D7  UnityEngine.Vector2Int <>iFixBaseProxy_GetNotCacheRange()
  RVA=0x0B0B93C4  token=0x60007D8  System.Void <>iFixBaseProxy_MoveCellTo(Beyond.UI.UIScrollList.Cell P0, System.Int32 P1)
  RVA=0x0B0E48D4  token=0x60007D9  System.Void <>iFixBaseProxy_OnUpdateCell(Beyond.UI.UIScrollList.Cell P0, System.Int32 P1)
  RVA=0x0B0D2998  token=0x60007DA  System.Single <>iFixBaseProxy_GetTargetPosByIndex(System.Int32 P0, Beyond.UI.UIScrollList.ScrollAlignType P1)
  RVA=0x0B0E48DC  token=0x60007DB  System.Void <>iFixBaseProxy_ScrollTo(System.Int32 P0, System.Boolean P1, Beyond.UI.UIScrollList.ScrollAlignType P2)
  RVA=0x0B0D2970  token=0x60007DC  System.Void <>iFixBaseProxy_ClearComponent()
  RVA=0x0B0E493C  token=0x60007DD  System.Single <>iFixBaseProxy_get_currentStep()
  RVA=0x0B0E4944  token=0x60007DE  System.Void <>iFixBaseProxy_set_currentStep(System.Single P0)
  RVA=0x0B0E48EC  token=0x60007DF  System.Void <>iFixBaseProxy_SetStepTo(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UINonUnifiedScrollRect
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x1F8
EXTENDS: Beyond.UI.UIScrollRect
FIELDS:
  private           System.Boolean                  m_isInited  // 0x1e0
  private           System.Boolean                  m_isRebuilding  // 0x1e1
  private           Beyond.UI.UINonUnifiedScrollListm_scrollList  // 0x1e8
  private           Beyond.UI.UILayoutDimensionListenerm_dimensionListener  // 0x1f0
METHODS:
  RVA=0x0B0E56FC  token=0x60007E0  System.Void _InitIfNot()
  RVA=0x0B0E5618  token=0x60007E1  System.Void UpdateScrollbars(UnityEngine.Vector2 offset)
  RVA=0x0B0E5318  token=0x60007E2  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x0B0E58FC  token=0x60007E3  System.Void _UpdateNonUnifiedScrollbars()
  RVA=0x0B0E5388  token=0x60007E4  System.Void SetHorizontalNormalizedPosition(System.Single value)
  RVA=0x0B0E54C8  token=0x60007E5  System.Void SetVerticalNormalizedPosition(System.Single value)
  RVA=0x0B0E5844  token=0x60007E6  System.Void _OnPostLayout()
  RVA=0x03EB34B0  token=0x60007E7  System.Void .ctor()
  RVA=0x0B0E5610  token=0x60007E8  System.Void <>iFixBaseProxy_UpdateScrollbars(UnityEngine.Vector2 P0)
  RVA=0x0B0E5608  token=0x60007E9  System.Void <>iFixBaseProxy_Rebuild(UnityEngine.UI.CanvasUpdate P0)
  RVA=0x05397FA8  token=0x60007EA  System.Void <>iFixBaseProxy_SetHorizontalNormalizedPosition(System.Single P0)
  RVA=0x053945E8  token=0x60007EB  System.Void <>iFixBaseProxy_SetVerticalNormalizedPosition(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UINumberScrollTween
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0xA0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.UI.UIText                bindingUIText  // 0x68
  public            System.Single                   tweenDuration  // 0x70
  public            System.Boolean                  useCustomNumberTweenCurve  // 0x74
  public            UnityEngine.AnimationCurve      numberTweenCurve  // 0x78
  private           System.String                   m_tweenToText  // 0x80
  private           System.Single                   m_tweenStartTime  // 0x88
  private           System.Single                   m_curTime  // 0x8c
  private           System.Boolean                  m_inTransition  // 0x90
  private           System.Single                   m_fromNumber  // 0x94
  private           System.Single                   m_toNumber  // 0x98
PROPERTIES:
  tickOption  get=0x0B0E5F60
  tweenToText  get=0x0B0E5FB0  set=0x0B0E60B4
  text  get=0x0B0E5EAC  set=0x0B0E6000
METHODS:
  RVA=0x0B0E5A74  token=0x60007F1  System.Void Start()
  RVA=0x0B0E5B04  token=0x60007F2  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0E5D5C  token=0x60007F3  System.Void _TweenToTarget(System.String targetText)
  RVA=0x0B0E5BD4  token=0x60007F4  System.Void _CleanupBeforeTween()
  RVA=0x0B0E5C24  token=0x60007F5  System.Void _HandleNumberScrollTween(System.Single fromNumber, System.Single toNumber, System.Single curTime)
  RVA=0x0B0E5E60  token=0x60007F6  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60007F7  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x60007F8  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIPCScaleHelper
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Vector3             baseScale  // 0x18
METHODS:
  RVA=0x047009D0  token=0x60007F9  System.Void OnEnable()
  RVA=0x048AC930  token=0x60007FA  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRawImage
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0x100
EXTENDS: UnityEngine.UI.RawImage
FIELDS:
METHODS:
  RVA=0x039C4AF0  token=0x60007FB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRegionBuildingTexManager
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Shader              _regionMapShader  // 0x18
  public            UnityEngine.Texture2D           minimapOutlineTex  // 0x20
  private   static  System.Int32                    s_buildingOutlineTex  // static @ 0x0
  private           UnityEngine.Renderer            m_meshRenderer  // 0x28
  private   static  UnityEngine.MaterialPropertyBlocks_materialPropertyBlock  // static @ 0x8
PROPERTIES:
  materialPropertyBlock  get=0x03521DE0
METHODS:
  RVA=0x03521990  token=0x60007FD  System.Void OnEnable()
  RVA=0x0B0E6118  token=0x60007FE  System.Void OnValidate()
  RVA=0x046AA5F0  token=0x60007FF  System.Void OnDisable()
  RVA=0x03521BF0  token=0x6000800  System.Void _UpdateTexture(UnityEngine.Renderer meshRenderer, UnityEngine.Shader minimapTerrainShader, UnityEngine.Texture2D buildingTex)
  RVA=0x03695570  token=0x6000801  System.Void .ctor()
  RVA=0x04D54AD0  token=0x6000802  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIScaleHolder
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0xA0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.RectTransform       m_rectTransform  // 0x68
  private           UnityEngine.RectTransform       m_parentTransform  // 0x70
  private           System.Boolean                  m_holdValid  // 0x78
  private           UnityEngine.Vector3             _extraScale  // 0x7c
  private           System.Boolean                  _onlyHoldOnEnable  // 0x88
  public            UnityEngine.RectTransform       referenceTransform  // 0x90
  public            System.Single                   animationExtraScale  // 0x98
PROPERTIES:
  tickOption  get=0x03E63EE0
METHODS:
  RVA=0x031E77C0  token=0x6000804  System.Void OnAwake()
  RVA=0x033EF340  token=0x6000805  System.Void OnEnable()
  RVA=0x033F0D30  token=0x6000806  System.Void LateTick(System.Single deltaTime)
  RVA=0x033F1190  token=0x6000807  System.Boolean _IsValidScale(UnityEngine.Vector3 scale)
  RVA=0x033F0DD0  token=0x6000808  System.Void _HoldScale()
  RVA=0x0B0E6208  token=0x6000809  System.Void SetExtraScale(UnityEngine.Vector3 extraScale)
  RVA=0x040E36B0  token=0x600080A  UnityEngine.Vector3 GetExtraScale()
  RVA=0x039C47A0  token=0x600080B  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600080C  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600080D  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x600080E  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x600080F  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIScrollbar
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x1D0
EXTENDS: UnityEngine.UI.Scrollbar
FIELDS:
METHODS:
  RVA=0x04D2B9F0  token=0x6000810  System.Void Awake()
  RVA=0x0453A540  token=0x6000811  System.Void .ctor()
  RVA=0x0B0869D4  token=0x6000812  System.Void <>iFixBaseProxy_Awake()
END_CLASS

CLASS: Beyond.UI.UIScrollbarKeyHint
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x78
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIScrollRect          m_scrollRect  // 0x68
  private           Beyond.UI.UIImage               m_image  // 0x70
PROPERTIES:
  tickOption  get=0x0B0ECD3C
METHODS:
  RVA=0x0B0ECB70  token=0x6000814  System.Void OnAwake()
  RVA=0x0B0ECC20  token=0x6000815  System.Void Tick(System.Single deltaTime)
  RVA=0x039C2B00  token=0x6000816  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000817  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000818  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x6000819  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIScrollCircle
TYPE:  class
TOKEN: 0x20000DB
SIZE:  0xF8
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IInitializePotentialDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.UI.ICanvasElement Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.GameObject          _cellTemplate  // 0x68
  private           System.Int32                    _totalNum  // 0x70
  private           System.Single                   _tweenTime  // 0x74
  private           UnityEngine.RectTransform       m_Viewport  // 0x78
  private           System.Single                   m_Elasticity  // 0x80
  private           System.Single                   _speedFactor  // 0x84
  private           System.Int32                    <count>k__BackingField  // 0x88
  public            UnityEngine.Events.UnityEvent<System.Int32,System.Int32,System.Boolean>onCenterIndexChanged  // 0x90
  public            UnityEngine.Events.UnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0x98
  private           UnityEngine.Vector2             m_PointerStartLocalCursor  // 0xa0
  private           UnityEngine.Vector2             m_tmpDragPosition  // 0xa8
  private           System.Boolean                  m_Dragging  // 0xb0
  private           System.Boolean                  m_inited  // 0xb1
  private           System.Single                   m_Velocity  // 0xb4
  private           UnityEngine.Vector2             m_curScreenPos  // 0xb8
  private           UnityEngine.RectTransform       m_ViewRect  // 0xc0
  private           Beyond.UI.LuaPanel              m_panel  // 0xc8
  private           System.Single                   m_radius  // 0xd0
  private           System.Single                   m_cellAngle  // 0xd4
  private           System.Collections.Generic.List<Beyond.UI.UIScrollCircle.Cell>m_showingCells  // 0xd8
  private           System.Single                   m_rangeMax  // 0xe0
  private           System.Single                   m_curAngle  // 0xe4
  private           System.Single                   m_lastAngle  // 0xe8
  private           System.Int32                    m_curCenterIndex  // 0xec
  private           DG.Tweening.Tween               m_tween  // 0xf0
PROPERTIES:
  tickOption  get=0x0B0E812C
  elasticity  get=0x0B0E8014  set=0x0B0E8308
  count  get=0x04D86890  set=0x04D868C0
  velocity  get=0x0B0E817C  set=0x0B0E8368
  panel  get=0x0B0E8068
  viewRect  get=0x0B0E81D0
METHODS:
  RVA=0x0B0E6AD8  token=0x6000823  System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E63EC  token=0x6000824  System.Boolean IsActive()
  RVA=0x0B0E65D0  token=0x6000825  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E6A6C  token=0x6000826  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E6788  token=0x6000827  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E7DE4  token=0x6000828  System.Void _UpdateSpeed()
  RVA=0x0B0E6ECC  token=0x6000829  System.Boolean _CheckCanMove(System.Single deltaAngle)
  RVA=0x0B0E7824  token=0x600082A  System.Void _TryUpdateCells()
  RVA=0x0B0E78A8  token=0x600082B  System.Void _UpdateAuto()
  RVA=0x0B0E6B44  token=0x600082C  System.Void OnRelease()
  RVA=0x0B0E7134  token=0x600082D  System.Single _GetDeltaAngle(UnityEngine.Vector2 position)
  RVA=0x0B0E7370  token=0x600082E  System.Void _InitCells()
  RVA=0x0B0E761C  token=0x600082F  System.Void _Init()
  RVA=0x0B0E7710  token=0x6000830  System.Void _OnAwakeInit()
  RVA=0x0B0E7B9C  token=0x6000831  System.Void _UpdateScrollRange()
  RVA=0x0B0E72FC  token=0x6000832  System.Int32 _GetRealIndex(System.Int32 index)
  RVA=0x0B0E7C04  token=0x6000833  System.Void _UpdateShowingCells()
  RVA=0x0B0E7A1C  token=0x6000834  System.Void _UpdateCenterIndex(System.Boolean forceUpdate)
  RVA=0x0B0E6F50  token=0x6000835  System.Void _DoScroll(System.Single tmpAngle, System.Boolean direct)
  RVA=0x0B0E6BB4  token=0x6000836  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E6C08  token=0x6000837  System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing)
  RVA=0x0B0E6530  token=0x6000838  System.Void LayoutComplete()
  RVA=0x0B0E63A8  token=0x6000839  System.Void GraphicUpdateComplete()
  RVA=0x0B0E643C  token=0x600083A  System.Boolean IsDestroyed()
  RVA=0x0B0E69E8  token=0x600083B  System.Void OnEnable()
  RVA=0x0B0E6730  token=0x600083C  System.Void OnDisable()
  RVA=0x0B0E6D58  token=0x600083D  System.Void Start()
  RVA=0x0B0E6574  token=0x600083E  System.Void OnAwake()
  RVA=0x0B0E64BC  token=0x600083F  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0E6E10  token=0x6000840  System.Void UpdateCount(System.Int32 newCount, System.Boolean setTop)
  RVA=0x0B0E6C5C  token=0x6000841  System.Void ScrollToIndex(System.Int32 index, System.Boolean direct)
  RVA=0x0B0E62FC  token=0x6000842  UnityEngine.GameObject Get(System.Int32 index)
  RVA=0x0B0E628C  token=0x6000843  System.Void ClearComponent()
  RVA=0x0B0E7E9C  token=0x6000844  System.Void .ctor()
  RVA=0x0B0E6DC0  token=0x6000845  UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform()
  RVA=0x0B0E6DA8  token=0x6000846  System.Void <_DoScroll>b__59_0()
  RVA=0x069AFD0C  token=0x6000847  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x069AFD04  token=0x6000848  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x01538B40  token=0x6000849  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x600084A  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x06C294C4  token=0x600084B  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06BD8710  token=0x600084C  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIScrollContainerFollower
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIScrollList          _followTargetList  // 0x68
  private           Beyond.UI.UIScrollRect          _followTargetRect  // 0x70
  private           UnityEngine.RectTransform       m_rectTransform  // 0x78
  private           System.Single                   m_currentSize  // 0x80
  private           System.Boolean                  m_targetSizeChanged  // 0x84
  private           UnityEngine.Vector2             m_currentPos  // 0x88
  private           System.Boolean                  m_targetPosChanged  // 0x90
  private           System.Boolean                  m_setValid  // 0x91
  private           System.Boolean                  m_isFollowContent  // 0x92
PROPERTIES:
  tickOption  get=0x0B0E8BDC
METHODS:
  RVA=0x0B0E8768  token=0x6000850  System.Void OnAwake()
  RVA=0x0B0E83C8  token=0x6000851  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0E8850  token=0x6000852  System.Void _RefreshSizeAndPositionToTargetContainer()
  RVA=0x04D26580  token=0x6000853  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000854  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000855  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06BD8710  token=0x6000856  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ScrollDirection
TYPE:  sealed struct
TOKEN: 0x20000DE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.ScrollDirection       Vertical  // const
  public    static  Beyond.UI.ScrollDirection       Horizontal  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIScrollList
TYPE:  class
TOKEN: 0x20000DF
SIZE:  0x278
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  protected         UnityEngine.GameObject          _cellTemplate  // 0x68
  private           Beyond.UI.ScrollDirection       _scrollDirection  // 0x70
  private           System.Boolean                  _expandCellSize  // 0x74
  private           System.Boolean                  _alsoExpandCellSizeInAnotherDirection  // 0x75
  protected         UnityEngine.Vector2             _space  // 0x78
  protected         Beyond.UI.UIScrollList.Padding  _padding  // 0x80
  protected         System.Boolean                  _ignorePaddingWhenScroll  // 0x90
  protected         System.Single                   _notCacheBufferSize  // 0x94
  protected         System.Single                   _extraCacheBufferSize  // 0x98
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onUpdateCell  // 0xa0
  public            Beyond.DisposedUnityEvent       onGraduallyShowFinish  // 0xa8
  public            Beyond.DisposedUnityEvent       onScrollEnd  // 0xb0
  public            Beyond.DisposedUnityEvent<System.Int32>onUpdateCount  // 0xb8
  public            System.Func<System.Int32,System.String>getCellName  // 0xc0
  public    static  Beyond.DisposedUnityEvent<System.String>s_onGraduallyShowFinish  // static @ 0x0
  protected         System.Boolean                  _inverse  // 0xc8
  protected         System.Boolean                  _autoLayoutWhenFew  // 0xc9
  protected         Beyond.UI.UIScrollList.AutoLayoutWhenFewType_autoLayoutWhenFewType  // 0xcc
  protected         System.Boolean                  _autoCenterInOtherDir  // 0xd0
  private           UnityEngine.Transform           _centerNode  // 0xd8
  protected         System.Boolean                  _hideRayCastWhenLessThanOnePage  // 0xe0
  protected         System.Boolean                  _clampWhenLessThanOnePage  // 0xe1
  protected         UnityEngine.UI.ScrollRect.MovementTypem_lastScrollRectMovementType  // 0xe4
  protected         UnityEngine.RectTransform       _container  // 0xe8
  protected         Beyond.UI.UIScrollRect          _scrollRect  // 0xf0
  private           System.Int32                    <count>k__BackingField  // 0xf8
  private           System.Single                   <containerSizeWithoutAutoLayout>k__BackingField  // 0xfc
  protected         UnityEngine.RectTransform       m_rect  // 0x100
  protected         System.Int32                    m_countPerLine  // 0x108
  protected         System.Int32                    m_maxShowingLine  // 0x10c
  protected         System.Int32                    m_maxShowingCellCount  // 0x110
  protected         System.Int32                    m_maxCellCountWhenNoScroll  // 0x114
  protected         System.Single                   m_viewWidth  // 0x118
  protected         System.Single                   m_viewHeight  // 0x11c
  protected         System.Single                   m_cellWidth  // 0x120
  protected         System.Single                   m_cellHeight  // 0x124
  protected         UnityEngine.UI.Graphic          m_graphic  // 0x128
  protected         Beyond.UI.UIScrollList.Padding  m_realPadding  // 0x130
  private           UnityEngine.Vector3[]           m_cellCorners  // 0x140
  protected         System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIScrollList.Cell>m_showingCells  // 0x148
  private           System.Collections.Generic.Stack<Beyond.UI.UIScrollList.Cell>m_cellCache  // 0x150
  private   static  System.String                   CACHED_CELL_GAME_OBJECT_NAME  // const
  public            UnityEngine.Events.UnityEvent   onBeginDrag  // 0x158
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x160
  public            UnityEngine.Events.UnityEvent   onEndDrag  // 0x168
  private           UnityEngine.GameObject          _moveTipsLeftOrTop  // 0x170
  private           UnityEngine.GameObject          _moveTipsRightOrBottom  // 0x178
  private           System.Single                   m_moveTipsIgnoredRange  // 0x180
  private           System.Boolean                  _useViewEffect  // 0x184
  private           System.Boolean                  _symmetricViewEffect  // 0x185
  private           System.Boolean                  _needDummyParent  // 0x186
  private           UnityEngine.AnimationCurve      _viewEffectAnimation  // 0x188
  private           UnityEngine.Vector2             _effectAnchorPoint  // 0x190
  private           System.Boolean                  _useDefaultScaleEffect  // 0x198
  private           UnityEngine.AnimationCurve      _scaleEffectAnimation  // 0x1a0
  private           System.Boolean                  _useDefaultArcEffect  // 0x1a8
  private           UnityEngine.AnimationCurve      _arcEffectAnimation  // 0x1b0
  public            UnityEngine.Events.UnityEvent<UnityEngine.GameObject,System.Int32,System.Single>onChangeView  // 0x1b8
  protected         System.Single                   _tweenDuration  // 0x1c0
  protected         DG.Tweening.Ease                _tweenEase  // 0x1c4
  protected         System.Single                   _changeStepRatio  // 0x1c8
  protected         DG.Tweening.Tween               m_adjustTween  // 0x1d0
  protected         System.Single                   m_totalLength  // 0x1d8
  protected         System.Single                   m_cellLength  // 0x1dc
  protected         UnityEngine.Vector2             m_centerPos  // 0x1e0
  public            System.Boolean                  enableAutoScroll  // 0x1e8
  private           System.Single                   _startAutoScrollTime  // 0x1ec
  private           System.Single                   _autoScrollSpeed  // 0x1f0
  public            Beyond.UI.UIArea                autoScrollUpArea  // 0x1f8
  public            Beyond.UI.UIArea                autoScrollDownArea  // 0x200
  private           System.Single                   m_curAutoScrollSpd  // 0x208
  private           System.Int32                    m_autoScrollTimerKey  // 0x20c
  protected         System.Boolean                  _enableGraduallyShow  // 0x210
  private           System.Single                   _graduallyShowTime  // 0x214
  private           System.Single                   _graduallyShowDelayTime  // 0x218
  protected         System.Boolean                  m_inGraduallyShow  // 0x21c
  protected         System.Single                   m_graduallyShowedMaxIndex  // 0x220
  protected         System.Single                   m_curGraduallyCanShowMaxIndex  // 0x224
  private           System.Boolean                  m_needInformAnimationInEnd  // 0x228
  private   static  System.Single                   GRADUALLY_SHOW_UPDATE_MAX_INTERVAL  // const
  public    static  System.Int32                    s_maxGraduallyShowAddCountPerFrame  // static @ 0x8
  private           System.Boolean                  _enableSelectedNavigation  // 0x229
  private           System.Boolean                  _noNavigationKeyHint  // 0x22a
  private           System.Boolean                  _enableNavigationLoop  // 0x22b
  private           System.Boolean                  _customNavigationActionId  // 0x22c
  private           System.String                   _customNavigationPreviousActionId  // 0x230
  private           System.String                   _customNavigationNextActionId  // 0x238
  public            UnityEngine.Events.UnityEvent<UnityEngine.GameObject,System.Int32>onSelectedCell  // 0x240
  public            UnityEngine.Events.UnityEvent<UnityEngine.GameObject,System.Int32,System.Boolean>onCellSelectedChanged  // 0x248
  public            System.Func<System.Int32>       getCurSelectedIndex  // 0x250
  private           System.Int32                    m_curSelectedIndex  // 0x258
  private           System.Int32                    m_bindingGroupId  // 0x25c
  private           System.Int32                    m_naviBindingGroupId  // 0x260
  protected         System.Boolean                  _enableWrapNavigation  // 0x264
  protected         System.Boolean                  _enableCircleWrapNavigation  // 0x265
  private           System.Boolean                  _clearNaviGroupOnUpdateCount  // 0x266
  private           System.Collections.Generic.List<UnityEngine.UI.Selectable>m_wrapNaviSelectables  // 0x268
  private           Beyond.UI.LuaPanel              m_panel  // 0x270
PROPERTIES:
  tickOption  get=0x047513C0
  space  get=0x048AC4B0
  autoLayoutWhenFew  get=0x0B0EC790
  isHorizontal  get=0x0396EF60
  isVertical  get=0x0396EF30
  count  get=0x04DA61F0  set=0x04DA6220
  containerSizeWithoutAutoLayout  get=0x04D8E1A0  set=0x04D8E1F0
  isMultiPage  get=0x0436E810
  maxShowingCellCount  get=0x036F3780
  countPerLine  get=0x0B0EC888
  cellWidth  get=0x0B0EC834
  cellHeight  get=0x0B0EC7E0
  currentStep  get=0x0396E250  set=0x0B0ECAEC
  moveTips  get=0x0396E910
  curSelectedIndex  get=0x0B0EC8D8  set=0x0B0ECA44
  curSelectedObj  get=0x0B0EC944
  enableSelectedNavigation  get=0x0B0EC9A4
  naviBindingGroupId  get=0x0B0EC9F4
  panel  get=0x03972670
METHODS:
  RVA=0x04B3A230  token=0x6000865  System.Void OnAwake()
  RVA=0x036F3530  token=0x6000866  System.Void Tick(System.Single deltaTime)
  RVA=0x03E31E80  token=0x6000867  System.Void LateTick(System.Single deltaTime)
  RVA=0x036F3890  token=0x6000868  System.Void OnEnable()
  RVA=0x03972440  token=0x6000869  System.Void OnDisable()
  RVA=0x03E31ED0  token=0x600086A  System.Void TryRecalculateSize()
  RVA=0x039D0BE0  token=0x600086B  System.Void InGameAwake()
  RVA=0x0B0EBC74  token=0x600086C  System.Void _EditorAwake()
  RVA=0x03970500  token=0x600086D  System.Void Init(System.Boolean isPreview)
  RVA=0x03970320  token=0x600086E  System.Void InitConfig()
  RVA=0x04B83790  token=0x600086F  System.Void InitBasicInfo()
  RVA=0x04553710  token=0x6000870  System.Void SyncViewSize()
  RVA=0x0396D350  token=0x6000871  System.Void CalcSomeCountNum()
  RVA=0x0396D3E0  token=0x6000872  System.Int32 _GetLineCount(System.Boolean calcVertical)
  RVA=0x0396E1E0  token=0x6000873  System.Void OnScroll(UnityEngine.Vector2 v)
  RVA=0x0B0EAB64  token=0x6000874  System.Void SetTop(System.Boolean needUpdate)
  RVA=0x0396CCC0  token=0x6000875  System.Void UpdateCount(System.Int32 newCount, System.Boolean setTop, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow)
  RVA=0x0396CD50  token=0x6000876  System.Void UpdateCount(System.Int32 newCount, System.Int32 fastScrollToIndex, System.Boolean forceRecalculate, System.Boolean onlyUpdateNewCellContent, System.Boolean skipGraduallyShow, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x04D3EA10  token=0x6000877  System.Void UpdateCount(System.Int32 newCount)
  RVA=0x0B0EBA08  token=0x6000878  System.Void UpdateShowingCells(System.Action<System.Int32,UnityEngine.GameObject> updateFunc)
  RVA=0x03970440  token=0x6000879  System.Single GetLeftLength(System.Boolean checkVertical, System.Int32 checkCount)
  RVA=0x03970290  token=0x600087A  System.Void _SetRealPadding(System.Boolean isVerticalPadding, System.Single firstAddedLength, System.Single secondAddedLength)
  RVA=0x03970070  token=0x600087B  System.Void UpdateContainerSize()
  RVA=0x0396E0B0  token=0x600087C  System.Single GetContainerSize(System.Boolean ignoreAutoLayout)
  RVA=0x0396DF80  token=0x600087D  System.Void SetContainerSize(System.Single size)
  RVA=0x045F1AC0  token=0x600087E  System.Void ClearShowingCells()
  RVA=0x0396E370  token=0x600087F  System.Void UpdateShowingCells(System.Boolean forceUpdateContent, System.Boolean forceClearShowingCells, System.Boolean ignoreListActive)
  RVA=0x0396F610  token=0x6000880  System.Void CacheOutOfRangeCells(UnityEngine.Vector2Int showRange, UnityEngine.Vector2Int notCacheRange)
  RVA=0x0396E5E0  token=0x6000881  System.Void ProcessCellAtIndex(System.Int32 index, System.Boolean forceUpdateContent, System.Boolean ignoreListActive)
  RVA=0x0396F410  token=0x6000882  System.Void RepositionOutOfRangeCells(UnityEngine.Vector2Int showRange)
  RVA=0x0396E650  token=0x6000883  System.Void UpdateCell(System.Int32 index, System.Boolean forceUpdateContent, System.Boolean ignoreListActive)
  RVA=0x0396EAB0  token=0x6000884  UnityEngine.Vector2Int GetShowRange(System.Single bufferSize, System.Boolean ignoreGraduallyShow)
  RVA=0x0396EDA0  token=0x6000885  UnityEngine.Vector2Int GetIndexRangeFromScrollOffset(System.Single scrollOffset, System.Single viewSize, System.Single bufferSize)
  RVA=0x0B0E9410  token=0x6000886  UnityEngine.Vector2Int GetRangeInView()
  RVA=0x0396E470  token=0x6000887  UnityEngine.Vector2Int GetNotCacheRange()
  RVA=0x0396EC80  token=0x6000888  System.Void MoveCellTo(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0B0EA1B0  token=0x6000889  System.Void SetCellCanCache(System.Int32 index, System.Boolean canCache)
  RVA=0x046BAFA0  token=0x600088A  System.Void OnUpdateCell(Beyond.UI.UIScrollList.Cell cell, System.Int32 index)
  RVA=0x0396CFE0  token=0x600088B  Beyond.UI.UIScrollList.Cell GetCell()
  RVA=0x0B0E8C2C  token=0x600088C  System.Void CacheShowingCell(System.Int32 index)
  RVA=0x045F1E60  token=0x600088D  System.Void CacheCell(Beyond.UI.UIScrollList.Cell cell)
  RVA=0x0B0EA904  token=0x6000890  System.Void SetStepTo(System.Single step)
  RVA=0x0B0E9C9C  token=0x6000891  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E9D04  token=0x6000892  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0E9D90  token=0x6000893  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0396E4B0  token=0x6000895  System.Void UpdateMoveTips()
  RVA=0x0396FA90  token=0x6000896  System.Void UpdateViewEffect()
  RVA=0x0B0E9A00  token=0x6000897  UnityEngine.GameObject Get(System.Int32 index)
  RVA=0x0B0E8CFC  token=0x6000898  Beyond.UI.UIScrollList.Cell GetCell(System.Int32 index)
  RVA=0x0396CFA0  token=0x6000899  System.Int32 GetRealIndex(System.Int32 index)
  RVA=0x039729B0  token=0x600089A  System.Void OnRelease()
  RVA=0x046926D0  token=0x600089B  System.Void ClearComponent()
  RVA=0x0B0EA2DC  token=0x600089C  System.Void SetPaddingBottom(System.Single value)
  RVA=0x0B0EA450  token=0x600089D  System.Void SetPaddingTop(System.Single value)
  RVA=0x0B0EA3D4  token=0x600089E  System.Void SetPaddingRight(System.Single value)
  RVA=0x0B0EA358  token=0x600089F  System.Void SetPaddingLeft(System.Single value)
  RVA=0x0B0EA4CC  token=0x60008A0  System.Void SetPadding(UnityEngine.Rect value)
  RVA=0x0481B180  token=0x60008A1  Beyond.UI.UIScrollList.Padding GetPadding(System.Boolean isRealPadding)
  RVA=0x0B0E98C4  token=0x60008A2  System.Void GetShowingCellsIndexRange(System.Int32& firstIndex, System.Int32& lastIndex)
  RVA=0x0B0EA848  token=0x60008A3  System.Void SetSpace(UnityEngine.Vector2 newSpace)
  RVA=0x0B0EA0BC  token=0x60008A4  System.Void ScrollToIndex(System.Int32 index, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType, System.Boolean graduallyShowInFastMode)
  RVA=0x039708B0  token=0x60008A5  System.Void ScrollTo(System.Int32 index, System.Boolean fastMode, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x03970720  token=0x60008A6  System.Void _ScrollToPos(System.Single targetPos, System.Boolean fastMode)
  RVA=0x039723E0  token=0x60008A7  System.Void TryClearAdjustTween()
  RVA=0x0396D610  token=0x60008A8  System.Single GetCenterIndex()
  RVA=0x03970920  token=0x60008A9  System.Single GetTargetPosByIndex(System.Int32 index, Beyond.UI.UIScrollList.ScrollAlignType alignType)
  RVA=0x039D0640  token=0x60008AA  System.Void _InitAutoScroll()
  RVA=0x0B0EC4B8  token=0x60008AB  System.Void _OnEnterAutoScrollUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0EC684  token=0x60008AC  System.Void _OnExitAutoScrollUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0EC390  token=0x60008AD  System.Void _OnEnterAutoScrollDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0EC5E0  token=0x60008AE  System.Void _OnExitAutoScrollDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0EC728  token=0x60008AF  System.Void _SetAutoScrollSpd(System.Single spd)
  RVA=0x036F35C0  token=0x60008B0  System.Void _UpdateAutoScroll(System.Single deltaTime)
  RVA=0x0B0E9DF8  token=0x60008B1  System.Void RestartGraduallyShow()
  RVA=0x03972560  token=0x60008B2  System.Void StartGraduallyShow()
  RVA=0x036F3620  token=0x60008B3  System.Void _UpdateGraduallyShow(System.Single deltaTime)
  RVA=0x0B0EAC2C  token=0x60008B4  System.Void SkipGraduallyShow()
  RVA=0x03972720  token=0x60008B5  System.Void StopGraduallyShow()
  RVA=0x039D07D0  token=0x60008BB  System.Void _InitNavigation()
  RVA=0x0B0E9C04  token=0x60008BC  System.Void NavigateSelected(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0E92AC  token=0x60008BD  System.Int32 GetNaviOffset(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0E9208  token=0x60008BE  System.Int32 GetNaviNewIndex(System.Int32 oldIndex, UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0EA5C4  token=0x60008BF  System.Void SetSelectedIndex(System.Int32 index, System.Boolean needNotify, System.Boolean forceUpdate, System.Boolean needScroll)
  RVA=0x0B0E9AA8  token=0x60008C0  System.Void ListWrapNavigate(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0EB684  token=0x60008C1  System.Boolean TryResolveWrapDirection(UnityEngine.UI.NaviDirection dir, System.Boolean& isBefore)
  RVA=0x0B0EAC88  token=0x60008C2  System.Boolean TryGetCurrentFocusedCell(System.Int32& currentIndex, Beyond.UI.UIScrollList.Cell& currentCell)
  RVA=0x0B0EB4D4  token=0x60008C3  System.Boolean TryNavigateInsideCell(Beyond.UI.UIScrollList.Cell cell, UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0EB324  token=0x60008C4  System.Boolean TryGetWrapTargetIndex(UnityEngine.UI.NaviDirection dir, System.Int32 currentIndex, System.Boolean isBefore, System.Int32& targetIndex)
  RVA=0x0B0EB798  token=0x60008C5  System.Boolean TrySetNaviTargetByIndex(System.Int32 index, System.Boolean isBefore)
  RVA=0x0B0EAF74  token=0x60008C6  System.Boolean TryGetSelectableFromCell(Beyond.UI.UIScrollList.Cell cell, System.Boolean isBefore, UnityEngine.UI.Selectable& selectable)
  RVA=0x02E97340  token=0x60008C7  System.Boolean _IsValidWrapNaviTarget(UnityEngine.UI.Selectable selectable)
  RVA=0x0B0EB5C0  token=0x60008C8  System.Boolean TryNormalizeFirstWrapIndex(System.Int32& nextIndex, System.Boolean isBefore)
  RVA=0x0B0EB3B0  token=0x60008C9  System.Boolean TryNavigateBySequentialIndex(System.Int32 startIndex, System.Boolean isBefore)
  RVA=0x0B0E8F08  token=0x60008CA  System.Int32 GetNaviManagerTargetIndex()
  RVA=0x039D0960  token=0x60008CB  System.Void _InitControllerNavi()
  RVA=0x0B0EBBB8  token=0x60008CC  System.Void _ClearRelatedNaviGroup()
  RVA=0x0B0E8E3C  token=0x60008CE  System.Int32 GetMousePosIndex(UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0E9EA0  token=0x60008CF  System.Int32 ScreenPos2Index(UnityEngine.Vector2 targetScreenPos, UnityEngine.UI.NaviDirection dir)
  RVA=0x0B0EC194  token=0x60008D0  System.Void _GetScreenPosLineColumnInfo(UnityEngine.Vector2 targetScreenPos, System.Int32& line, System.Int32& column)
  RVA=0x0B0E8DB4  token=0x60008D1  System.Int32 GetIndexOf(System.Int32 column, System.Int32 line)
  RVA=0x039C2EA0  token=0x60008D2  System.Void .ctor()
  RVA=0x04CEC340  token=0x60008D3  System.Void .cctor()
  RVA=0x0B0EB934  token=0x60008D4  System.Single <_ScrollToPos>b__162_0()
  RVA=0x0B0EB958  token=0x60008D5  System.Void <_ScrollToPos>b__162_1(System.Single x)
  RVA=0x0B0EB980  token=0x60008D6  System.Void <_ScrollToPos>b__162_2()
  RVA=0x0B0EB9BC  token=0x60008D7  System.Single <_ScrollToPos>b__162_3()
  RVA=0x0B0EB9E0  token=0x60008D8  System.Void <_ScrollToPos>b__162_4(System.Single y)
  RVA=0x0B0EB980  token=0x60008D9  System.Void <_ScrollToPos>b__162_5()
  RVA=0x0B0EB91C  token=0x60008DA  System.Void <_OnEnterAutoScrollUp>b__175_0()
  RVA=0x0B0EB90C  token=0x60008DB  System.Void <_OnEnterAutoScrollDown>b__177_0()
  RVA=0x0B0EB8CC  token=0x60008DC  System.Void <_InitNavigation>b__216_0()
  RVA=0x0B0EB8DC  token=0x60008DD  System.Void <_InitNavigation>b__216_1()
  RVA=0x0B0EB8EC  token=0x60008DE  System.Void <_InitNavigation>b__216_2()
  RVA=0x0B0EB8FC  token=0x60008DF  System.Void <_InitNavigation>b__216_3()
  RVA=0x0B0EB8EC  token=0x60008E0  System.Void <_InitNavigation>b__216_4()
  RVA=0x0B0EB8FC  token=0x60008E1  System.Void <_InitNavigation>b__216_5()
  RVA=0x0B0EB8CC  token=0x60008E2  System.Void <_InitNavigation>b__216_6()
  RVA=0x0B0EB8DC  token=0x60008E3  System.Void <_InitNavigation>b__216_7()
  RVA=0x069AFD0C  token=0x60008E4  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60008E5  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x60008E6  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06BD8710  token=0x60008E7  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B40  token=0x60008E8  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x60008E9  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x069AFD04  token=0x60008EA  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.AutoScrollStrategy
TYPE:  sealed struct
TOKEN: 0x20000E6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.AutoScrollStrategy    None  // const
  public    static  Beyond.UI.AutoScrollStrategy    AlignNearbyBound  // const
  public    static  Beyond.UI.AutoScrollStrategy    AlignCenter  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIScrollRect
TYPE:  class
TOKEN: 0x20000E7
SIZE:  0x1E0
EXTENDS: UnityEngine.UI.ScrollRect
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions>m_scrollTween  // 0x150
  public            System.Single                   smoothScrollTweenSpd  // 0x158
  public            DG.Tweening.Ease                smoothScrollTweenEase  // 0x15c
  public            System.Single                   smoothScrollTweenMaxDur  // 0x160
  private           System.Boolean                  m_lastUpdateIsDragging  // 0x164
  private           Beyond.UI.LuaPanel              m_panel  // 0x168
  protected         System.Boolean                  _useLeftStick  // 0x170
  protected         System.Single                   _stickScrollSpeed  // 0x174
  protected         System.Single                   _stickScrollTolerance  // 0x178
  protected         Beyond.UI.UISelectableNaviGroup _naviGroup  // 0x180
  protected         System.Boolean                  _enableWrapNavigation  // 0x188
  protected         Beyond.Input.InputBindingGroupMonoTarget_scrollBindingGroup  // 0x190
  public            System.Boolean                  controllerScrollEnabled  // 0x198
  private           System.Boolean                  m_controllerInited  // 0x199
  private           System.Boolean                  m_navigationWrapped  // 0x19a
  protected         Beyond.UI.AutoScrollStrategy    _autoScrollStrategy  // 0x19c
  protected         System.Single                   _alignBoundPaddingX  // 0x1a0
  protected         System.Single                   _alignBoundPaddingY  // 0x1a4
  private           UnityEngine.GameObject          _moveTipsLeftOrTop  // 0x1a8
  private           UnityEngine.GameObject          _moveTipsRightOrBottom  // 0x1b0
  private           UnityEngine.Camera              m_fallbackUICamera  // 0x1b8
  private           System.Boolean                  _enableOverScrollEffect  // 0x1c0
  private           System.Single                   _overScrollTriggerValue  // 0x1c4
  private           UnityEngine.GameObject          _overScrollEffectPrevNode  // 0x1c8
  private           UnityEngine.GameObject          _overScrollEffectNextNode  // 0x1d0
  public            Beyond.DisposedUnityEvent<System.Boolean>onOverScrollEffect  // 0x1d8
PROPERTIES:
  inScrollTween  get=0x0B0F19DC
  contentStartPosition  get=0x0B0F187C  set=0x0B0F1CA4
  dragging  get=0x0B0F198C
  controllerStickScrollEnabled  get=0x0B0F18E0
  panel  get=0x0B0F1BE0
  naviGroup  get=0x0B0F1B90
  moveTips  get=0x0B0F1A50
METHODS:
  RVA=0x039D2680  token=0x60008F0  System.Void Awake()
  RVA=0x039D39B0  token=0x60008F1  System.Void OnEnable()
  RVA=0x03C94D90  token=0x60008F2  System.Void Update()
  RVA=0x03972230  token=0x60008F3  System.Void ScrollTo(UnityEngine.Vector2 position, System.Boolean noTween)
  RVA=0x039722A0  token=0x60008F4  System.Void KillScrollTween()
  RVA=0x0B0F05BC  token=0x60008F5  System.Boolean IsCellViewed(UnityEngine.RectTransform cell)
  RVA=0x0B0F0490  token=0x60008F6  System.Void ClampContentToBounds()
  RVA=0x04A1C730  token=0x60008F7  System.Void OnDestroy()
  RVA=0x0B0F0F04  token=0x60008F8  System.Void _CustomSetPosOnScroll(UnityEngine.Vector2 position)
  RVA=0x03E0B630  token=0x60008F9  System.Void UpdateScrollEnableNode()
  RVA=0x0B0F06A8  token=0x60008FA  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F0738  token=0x60008FB  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F07C8  token=0x60008FC  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x039CFC90  token=0x60008FD  System.Void ClearComponent()
  RVA=0x033F6770  token=0x60008FF  System.Void LateUpdate()
  RVA=0x03C94E20  token=0x6000903  System.Void _TickRefreshControllerStickScroll()
  RVA=0x044013B0  token=0x6000904  System.Void ScrollToNaviTarget(UnityEngine.UI.Selectable target)
  RVA=0x0B0F0870  token=0x6000905  System.Void RectWrapNavigate(UnityEngine.UI.NaviDirection dir)
  RVA=0x039D3CD0  token=0x6000906  System.Void _InitController()
  RVA=0x0B0F0B44  token=0x6000907  System.Void WrapNavigation(UnityEngine.Events.UnityAction<UnityEngine.UI.NaviDirection> wrapper)
  RVA=0x044014B0  token=0x6000908  System.Void AutoScrollToRectTransform(UnityEngine.RectTransform target, System.Boolean noTween)
  RVA=0x03C94EE0  token=0x600090A  System.Void _UpdateMoveTips()
  RVA=0x0B0F1638  token=0x600090B  UnityEngine.Camera _GetUICamera()
  RVA=0x04476210  token=0x600090C  System.Void _CheckOverScrollEffect(UnityEngine.Vector2 value)
  RVA=0x0B0F0D28  token=0x600090D  System.Void _CheckOverScrollOnEndDrag(UnityEngine.Vector2 value)
  RVA=0x03EB2AA0  token=0x600090E  System.Void .ctor()
  RVA=0x041E1670  token=0x600090F  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0F0B24  token=0x6000910  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x041E1670  token=0x6000911  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0F0B3C  token=0x6000912  System.Void <>iFixBaseProxy__CustomSetPosOnScroll(UnityEngine.Vector2 P0)
  RVA=0x0B0F0B34  token=0x6000913  System.Void <>iFixBaseProxy_UpdateScrollEnableNode()
  RVA=0x0B0F0B14  token=0x6000914  System.Void <>iFixBaseProxy_OnBeginDrag(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0F0B1C  token=0x6000915  System.Void <>iFixBaseProxy_OnDrag(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0F0B2C  token=0x6000916  System.Void <>iFixBaseProxy_OnEndDrag(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0F0B0C  token=0x6000917  System.Void <>iFixBaseProxy_LateUpdate()
END_CLASS

CLASS: Beyond.UI.UIScrollRectEdgeRedDot
TYPE:  class
TOKEN: 0x20000EA
SIZE:  0xF0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.UI.UIScrollRect          m_uiScrollRect  // 0x68
  private           UnityEngine.RectTransform       m_uiScrollRectTransform  // 0x70
  private           Beyond.UI.UIScrollList          m_uiScrollList  // 0x78
  private           UnityEngine.UI.RectMask2D       m_rectMask2D  // 0x80
  private           System.Collections.Generic.Dictionary<UnityEngine.GameObject,Beyond.UI.UIScrollRectEdgeRedDot.RedDotInfo>m_redDotInfoDict  // 0x88
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState>m_redDotStateDict  // 0x90
  private           UnityEngine.Vector2Int          m_scrollListShowRange  // 0x98
  public            System.Func<System.Int32,System.Int32>getRedDotStateAt  // 0xa0
  private           Beyond.UI.UIScrollRectEdgeRedDot.ERedDotStatem_upRedDotState  // 0xa8
  private           Beyond.UI.UIScrollRectEdgeRedDot.ERedDotStatem_downRedDotState  // 0xac
  private           System.Boolean                  _isHorizontal  // 0xb0
  private           UnityEngine.GameObject          _upNormal  // 0xb8
  private           UnityEngine.GameObject          _upNew  // 0xc0
  private           UnityEngine.GameObject          _downNormal  // 0xc8
  private           UnityEngine.GameObject          _downNew  // 0xd0
  private           UnityEngine.RectTransform       _upParent  // 0xd8
  private           UnityEngine.RectTransform       _downParent  // 0xe0
  private           System.Single                   _upEdgeOffset  // 0xe8
  private           System.Single                   _downEdgeOffset  // 0xec
PROPERTIES:
  tickOption  get=0x0B0F0440
METHODS:
  RVA=0x0B0EE950  token=0x600091C  System.Void OnAwake()
  RVA=0x0B0EE9AC  token=0x600091D  System.Void OnEnable()
  RVA=0x0B0EEB78  token=0x600091E  System.Void OnRelease()
  RVA=0x0B0EF620  token=0x600091F  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0EF3CC  token=0x6000920  System.Void ResetReferences()
  RVA=0x0B0EEFE8  token=0x6000921  System.Void ResetRedDotPosition()
  RVA=0x0B0F00B0  token=0x6000922  System.Void _UpdateCacheDictForRangeChange(UnityEngine.Vector2Int newRange)
  RVA=0x0B0F0018  token=0x6000923  System.Void _OnScrollListUpdateCount(System.Int32 newCount)
  RVA=0x0B0EEE30  token=0x6000924  System.Void RegisterRedDot(UnityEngine.GameObject redDotRoot, UnityEngine.GameObject redDotNormal, UnityEngine.GameObject redDotNew)
  RVA=0x0B0EF83C  token=0x6000925  System.Void UnregisterRedDot(UnityEngine.GameObject redDotRoot)
  RVA=0x0B0EECA4  token=0x6000926  System.Void RegisterRedDotStatesForScrollList()
  RVA=0x0B0EF8FC  token=0x6000927  System.Void UpdateRedDotStateInDir()
  RVA=0x0B0EF9C4  token=0x6000928  System.Void _CalculateRedDotStateInDir(Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState& upState, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState& downState)
  RVA=0x0B0F02A0  token=0x6000929  System.Void _UpdateRedDotGameObjects(UnityEngine.GameObject normal, UnityEngine.GameObject newer, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState state)
  RVA=0x0B0EE678  token=0x600092A  System.Boolean IsRedDotInDir(Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState state, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDir dir, Beyond.UI.UIScrollRectEdgeRedDot.RedDotInfo redDotInfo)
  RVA=0x0B0EE61C  token=0x600092B  System.Void ClearComponent()
  RVA=0x04D83030  token=0x600092C  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600092D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600092E  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x600092F  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x6000930  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x01538B50  token=0x6000931  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIScrollText
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0xC8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           System.Single                   _speed  // 0x68
  private           System.Single                   _stayTime  // 0x6c
  private           System.Single                   _fadeTime  // 0x70
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0x78
  private           Beyond.UI.UIScrollText.ScrollMode_scrollMode  // 0x80
  private           System.Boolean                  _disableClipping  // 0x84
  private           System.Boolean                  m_isInited  // 0x85
  private           Beyond.UI.UIText                m_tmp  // 0x88
  private           UnityEngine.RectTransform       m_rect  // 0x90
  private           UnityEngine.RectTransform       m_parentRect  // 0x98
  private           Beyond.UI.UIScrollText.IScrollHandlerm_scrollHandler  // 0xa0
  private           Beyond.UI.UIScrollText.IAlphaHandlerm_alphaHandler  // 0xa8
  private           Beyond.UI.UIScrollText.ScrollStatem_state  // 0xb0
  private           System.Single                   m_scrollOffset  // 0xb4
  private           System.Single                   m_currStateTime  // 0xb8
  private           System.Boolean                  m_needScroll  // 0xbc
  private           System.Single                   m_textDiff  // 0xc0
  private           TMPro.TextAlignmentOptions      m_originalAlignment  // 0xc4
PROPERTIES:
  tickOption  get=0x04A35580
METHODS:
  RVA=0x049BC050  token=0x6000939  System.Void _InitIfNot()
  RVA=0x0B0F20AC  token=0x600093A  System.Void ResetTextScroll()
  RVA=0x0B0F2220  token=0x600093B  System.Void ResetToStatic()
  RVA=0x0B0F1D08  token=0x600093C  System.Single GetSingleScrollDuration()
  RVA=0x0B0F23DC  token=0x600093D  System.Void _ResetTextScrollInternal(System.Boolean needScroll, System.Single textDiff)
  RVA=0x049BC010  token=0x600093E  System.Void OnAwake()
  RVA=0x0B0F1E90  token=0x600093F  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0F24F8  token=0x6000940  System.Void _UpdateScrollState(System.Single deltaTime, System.Single textDiff)
  RVA=0x0B0F2494  token=0x6000941  System.Void _ResetTextScroll()
  RVA=0x0B0F2280  token=0x6000942  System.Void _ApplyTextAlignment(System.Boolean isScroll)
  RVA=0x0B0F232C  token=0x6000943  System.Void _ApplyTextScroll(System.Single scrollOffset)
  RVA=0x04D25E90  token=0x6000944  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000945  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000946  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06BD8710  token=0x6000947  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISelectableNaviGroup
TYPE:  class
TOKEN: 0x20000F6
SIZE:  0x130
EXTENDS: UnityEngine.UI.SelectableNaviGroup
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            System.Collections.Generic.List<Beyond.Input.InputBindingGroupMonoTarget>relatedInputBindingGroups  // 0xa0
  public            System.Func<UnityEngine.UI.Selectable>getDefaultSelectableFunc  // 0xa8
  public            System.Func<System.Boolean>     canFocusFun  // 0xb0
  public            System.String                   focusActionId  // 0xb8
  public            System.String                   stopFocusActionId  // 0xc0
  public            System.Boolean                  hideItemTipsOnUnFocus  // 0xc8
  public            System.Boolean                  useDefaultTargetOnFocus  // 0xc9
  public            System.Boolean                  useFocusHighlight  // 0xca
  public            System.Boolean                  focusHighlightMask  // 0xcb
  public            System.Boolean                  focusHighlightNormalFrame  // 0xcc
  public            System.Boolean                  focusHighlightDarkFrame  // 0xcd
  public            UnityEngine.RectTransform       focusOverrideHighlightRect  // 0xd0
  public            System.Int32                    focusPanelSortingOrder  // 0xd8
  public            Beyond.UI.UIActionKeyHint       focusActionKeyHint  // 0xe0
  public            Beyond.UI.UIActionKeyHint       stopFocusActionKeyHint  // 0xe8
  public            UnityEngine.Events.UnityEvent<System.Boolean>onIsFocusedChange  // 0xf0
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0xf8
  private           Beyond.Input.InputBindingGroupMonoTargetm_bindingMonoGroup  // 0x100
  private           UnityEngine.RectTransform       m_rectTransform  // 0x108
  private           UnityEngine.UI.Selectable       m_lastFocusNaviTarget  // 0x110
  private           System.String                   m_focusBindingText  // 0x118
  private           System.Int32                    m_focusBindingId  // 0x120
  private           System.Int32                    m_stopFocusBindingId  // 0x124
  private           System.Boolean                  m_focusBindingCreated  // 0x128
  private           System.Boolean                  m_isInFocusHighlight  // 0x129
  private   static  System.Int32                    INVALID_BINDING_ID  // const
PROPERTIES:
  FocusBindingId  get=0x0B0F2FF4
  StopFocusBindingId  get=0x0B0F3044
  lastFocusNaviTarget  get=0x0B0F3094
METHODS:
  RVA=0x037D3880  token=0x6000958  System.Void Awake()
  RVA=0x037D3D40  token=0x6000959  System.Void OnEnable()
  RVA=0x045F41D0  token=0x600095A  System.Void OnDisable()
  RVA=0x037D3F90  token=0x600095B  System.Void _OnIsTopLayerChanged()
  RVA=0x046B1ED0  token=0x600095C  System.Void _OnSelectableNaviGroupDisabled()
  RVA=0x037D2E40  token=0x600095D  System.Void _BeforeSelectableNaviGroupDestroy()
  RVA=0x0B0F2E9C  token=0x600095E  System.Void _OnSetLayerSelectedTarget(UnityEngine.UI.Selectable value)
  RVA=0x037D1010  token=0x600095F  System.Void _OnRemoveFromLayerStack()
  RVA=0x0306A3A0  token=0x6000960  System.Void NaviToThisGroup(System.Boolean forceDefault)
  RVA=0x037D4010  token=0x6000961  System.Void _RefreshRelatedBindingGroupsState()
  RVA=0x0306A540  token=0x6000962  UnityEngine.UI.Selectable _FindDefaultSelectable()
  RVA=0x039CFBA0  token=0x6000963  System.Void OnDestroy()
  RVA=0x039CFD30  token=0x6000964  System.Void ClearComponent()
  RVA=0x0B0F2AFC  token=0x6000965  System.Void TryChangeNaviPartnerOnUp(Beyond.UI.UISelectableNaviGroup partner, System.Boolean isAdd)
  RVA=0x0B0F297C  token=0x6000966  System.Void TryChangeNaviPartnerOnDown(Beyond.UI.UISelectableNaviGroup partner, System.Boolean isAdd)
  RVA=0x0B0F29FC  token=0x6000967  System.Void TryChangeNaviPartnerOnLeft(Beyond.UI.UISelectableNaviGroup partner, System.Boolean isAdd)
  RVA=0x0B0F2A7C  token=0x6000968  System.Void TryChangeNaviPartnerOnRight(Beyond.UI.UISelectableNaviGroup partner, System.Boolean isAdd)
  RVA=0x0B0F2F04  token=0x6000969  System.Void _TryChangeNaviPartner(System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup> list, Beyond.UI.UISelectableNaviGroup partner, System.Boolean isAdd)
  RVA=0x037D3A60  token=0x600096D  System.Void _InitFocusBinding()
  RVA=0x037D0DF0  token=0x600096E  System.Void _RefreshFocusBindings()
  RVA=0x0B0F2874  token=0x600096F  System.Void RefreshFocusBindings()
  RVA=0x0B0F2BB0  token=0x6000970  System.Void _OnFocus(System.Boolean onlyStartHighlight)
  RVA=0x037D12E0  token=0x6000971  System.Void _OnStopFocus(System.Boolean onlyStopHighlight, System.Boolean ignoreTopLayerCheck)
  RVA=0x037D3ED0  token=0x6000972  System.Void _RefreshFocusActionKeyHint()
  RVA=0x037D3DC0  token=0x6000973  System.Void _RefreshStopFocusActionKeyHint()
  RVA=0x0B0F2810  token=0x6000974  System.Void ManuallyFocus(System.Boolean onlyStartHighlight)
  RVA=0x037D2DF0  token=0x6000975  System.Void ManuallyStopFocus(System.Boolean onlyStopHighlight)
  RVA=0x0B0F28C4  token=0x6000976  System.Void SetFocusBindingText(System.String bindingText)
  RVA=0x04B87E20  token=0x6000977  System.Void ClearLastFocusNaviTarget()
  RVA=0x037D2E80  token=0x6000978  System.Void ManuallyRefreshRelatedBindingGroups()
  RVA=0x040F9920  token=0x6000979  System.Void .ctor()
  RVA=0x0B0F2B94  token=0x600097A  System.Void <_InitFocusBinding>b__51_0()
  RVA=0x0B0F2BA0  token=0x600097B  System.Void <_InitFocusBinding>b__51_1()
  RVA=0x0B0F2B84  token=0x600097C  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x053979C0  token=0x600097D  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0F2B8C  token=0x600097E  System.Void <>iFixBaseProxy__OnIsTopLayerChanged()
  RVA=0x041E1670  token=0x600097F  System.Void <>iFixBaseProxy__OnSelectableNaviGroupDisabled()
  RVA=0x041E1670  token=0x6000980  System.Void <>iFixBaseProxy__BeforeSelectableNaviGroupDestroy()
  RVA=0x041E1670  token=0x6000981  System.Void <>iFixBaseProxy__OnSetLayerSelectedTarget(UnityEngine.UI.Selectable P0)
  RVA=0x041E1670  token=0x6000982  System.Void <>iFixBaseProxy__OnRemoveFromLayerStack()
  RVA=0x0B0F2B7C  token=0x6000983  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UISemiCircleScrollList
TYPE:  class
TOKEN: 0x20000F8
SIZE:  0x118
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler
FIELDS:
  public            UnityEngine.RectTransform       content  // 0x68
  private           System.Single                   _innerRadius  // 0x70
  private           System.Single                   _outerRadius  // 0x74
  private           System.Single                   _padding  // 0x78
  private           System.Int32                    _cellCount  // 0x7c
  private           UnityEngine.GameObject          _cellPrefab  // 0x80
  private           System.Single                   _startAngle  // 0x88
  private           System.Boolean                  _reverseDirection  // 0x8c
  private           UnityEngine.RectTransform       m_rectTransform  // 0x90
  public            System.Single                   currentAngleOffset  // 0x98
  private           System.Single                   m_targetAngleOffset  // 0x9c
  private           System.Single                   _hideAngel  // 0xa0
  private           System.Single                   _duration  // 0xa4
  private           System.Collections.Generic.List<UnityEngine.GameObject>m_cells  // 0xa8
  private           System.Boolean                  m_inEndDragAnim  // 0xb0
  private           System.Boolean                  m_inDragging  // 0xb1
  protected         Beyond.Input.InputBindingGroupMonoTarget_scrollBindingGroup  // 0xb8
  protected         System.Single                   _stickScrollTolerance  // 0xc0
  protected         System.Single                   _stickScrollSpeed  // 0xc4
  private           System.Int32                    m_lastClosestCellIndex  // 0xc8
  private           System.Int32                    m_currentCellCount  // 0xcc
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onCellShow  // 0xd0
  public            Beyond.DisposedUnityEvent<System.Int32>onClosestCellChanged  // 0xd8
  public            Beyond.DisposedUnityEvent       onDrag  // 0xe0
  public            Beyond.DisposedUnityEvent       onEndDrag  // 0xe8
  private           UnityEngine.Coroutine           m_dampingCoroutine  // 0xf0
  private           System.String                   _audioHoldStart  // 0xf8
  private           System.String                   _audioHoldEnd  // 0x100
  private           System.String                   _audioAlignment  // 0x108
  protected         System.Boolean                  _dpadScrollEnabled  // 0x110
  protected         System.Single                   _dpadScrollSpeed  // 0x114
PROPERTIES:
  tickOption  get=0x0B0F4830
  controllerStickScrollEnabled  get=0x0B0F4790
METHODS:
  RVA=0x0B0F386C  token=0x6000986  System.Void OnAwake()
  RVA=0x0B0F3994  token=0x6000987  System.Void OnDisable()
  RVA=0x0B0F3CD8  token=0x6000988  System.Void Tick(System.Single deltaTime)
  RVA=0x0B0F4374  token=0x6000989  System.Void _TickRefreshControllerStickScroll()
  RVA=0x0B0F420C  token=0x600098A  System.Void _InitializeCells()
  RVA=0x0B0F3C38  token=0x600098B  System.Void RefreshLayout(System.Int32 newCellCount, System.Int32 targetCellIndex)
  RVA=0x0B0F3D44  token=0x600098C  System.Void UpdateLayout()
  RVA=0x0B0F3A18  token=0x600098D  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F3758  token=0x600098E  System.Void ClearComponent()
  RVA=0x0B0F3910  token=0x600098F  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F3B50  token=0x6000990  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F417C  token=0x6000991  System.Collections.IEnumerator _ApplyDamping()
  RVA=0x0B0F4630  token=0x6000992  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000993  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000994  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B30  token=0x6000995  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B50  token=0x6000996  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISemiCircleScrollListPair
TYPE:  class
TOKEN: 0x20000FA
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UISemiCircleScrollListm_scrollListMaster  // 0x18
  public            Beyond.UI.UISemiCircleScrollList[]m_scrollLists  // 0x20
  public            Beyond.DisposedUnityEvent<UnityEngine.GameObject,System.Int32>onCellShow  // 0x28
  public            Beyond.DisposedUnityEvent<System.Int32>onClosestCellChanged  // 0x30
METHODS:
  RVA=0x0B0F30E4  token=0x600099D  System.Void Awake()
  RVA=0x0B0F3514  token=0x600099E  System.Void _OnCellShow(UnityEngine.GameObject go, System.Int32 index)
  RVA=0x0B0F35B4  token=0x600099F  System.Void _OnClosestCellChanged(System.Int32 index)
  RVA=0x0B0F363C  token=0x60009A0  System.Void _OnDrag()
  RVA=0x0B0F36D8  token=0x60009A1  System.Void _OnEndDrag()
  RVA=0x0B0F3458  token=0x60009A2  System.Void RefreshLayout(System.Int32 newCellCount, System.Int32 targetCellIndex)
  RVA=0x05393520  token=0x60009A3  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISequenceImage
TYPE:  class
TOKEN: 0x20000FB
SIZE:  0x90
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Boolean                  autoPlay  // 0x68
  public            System.Single                   frameRate  // 0x6c
  public            System.Collections.Generic.List<UnityEngine.Sprite>sprites  // 0x70
  private           System.Boolean                  <isPlaying>k__BackingField  // 0x78
  private           Beyond.UI.UIImage               m_image  // 0x80
  private           System.Single                   m_curFrameCount  // 0x88
PROPERTIES:
  tickOption  get=0x046BEFA0
  isPlaying  get=0x04D86F90  set=0x04D86FA0
METHODS:
  RVA=0x04A05750  token=0x60009A7  System.Void OnAwake()
  RVA=0x048BD7C0  token=0x60009A8  System.Void OnEnable()
  RVA=0x049D6D30  token=0x60009A9  System.Void OnDisable()
  RVA=0x031E9FF0  token=0x60009AA  System.Void Tick(System.Single deltaTime)
  RVA=0x048BD810  token=0x60009AB  System.Void Play()
  RVA=0x049D6D80  token=0x60009AC  System.Void Stop(System.Boolean resetToFirstFrame)
  RVA=0x031EA080  token=0x60009AD  System.Void SetSprite(System.Int32 index)
  RVA=0x045A38E0  token=0x60009AE  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60009AF  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60009B0  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x60009B1  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x60009B2  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B50  token=0x60009B3  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISimpleActiveAnimationHelper
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0x90
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Boolean                  isActive  // 0x68
  public            System.String                   toActiveAnimationName  // 0x70
  private           UnityEngine.Animation           m_animation  // 0x78
  private           UnityEngine.AnimationState      m_animationState  // 0x80
  private           System.Single                   m_animationTime  // 0x88
  private           System.Single                   m_curAnimationTime  // 0x8c
PROPERTIES:
  tickOption  get=0x0B0F4B98
METHODS:
  RVA=0x0B0F4994  token=0x60009B5  System.Void Start()
  RVA=0x0B0F4880  token=0x60009B6  System.Void OnEnable()
  RVA=0x0B0F48F0  token=0x60009B7  System.Void Skip()
  RVA=0x0B0F4A70  token=0x60009B8  System.Void Tick(System.Single deltaTime)
  RVA=0x04D10E70  token=0x60009B9  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60009BA  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x60009BB  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B50  token=0x60009BC  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISimpleStateController
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<Beyond.UI.UISimpleStateController.StateInfo>_states  // 0x18
  private           System.String                   m_curStateName  // 0x20
PROPERTIES:
  curStateName  get=0x0B0F4BE8
METHODS:
  RVA=0x0378F2A0  token=0x60009BE  System.Void SetState(System.String stateName)
  RVA=0x0446CA60  token=0x60009BF  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISizeFollower
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            UnityEngine.RectTransform       target  // 0x68
  public            UnityEngine.Vector2             addedSize  // 0x70
  public            System.Boolean                  isSumSize  // 0x78
  public            System.Boolean                  followWidth  // 0x79
  public            System.Boolean                  followHeight  // 0x7a
  public            System.Boolean                  regulateAnchoredPosition  // 0x7b
  public            UnityEngine.Vector2             minSize  // 0x7c
  public            UnityEngine.Vector2             maxSize  // 0x84
  private           UnityEngine.RectTransform       m_rectTransform  // 0x90
PROPERTIES:
  tickOption  get=0x042D2570
  rectTransform  get=0x03BCDC90
METHODS:
  RVA=0x03BCD960  token=0x60009C5  System.Void LateTick(System.Single deltaTime)
  RVA=0x03BCD9E0  token=0x60009C6  System.Void SyncSize()
  RVA=0x039C44E0  token=0x60009C7  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60009C8  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x60009C9  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISlider
TYPE:  class
TOKEN: 0x2000100
SIZE:  0x278
EXTENDS: UnityEngine.UI.Slider
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IEndDragHandler Beyond.Input.IBindingGroupTarget Beyond.Input.IBindingView
FIELDS:
  public            System.Single                   snapCenterRange  // 0x1f0
  public            System.String                   audioSlide  // 0x1f8
  public            System.String                   audioClickHandle  // 0x200
  private           System.Boolean                  m_dragging  // 0x208
  private           System.Boolean                  m_gamepadStickScrolling  // 0x209
  public            UnityEngine.UI.Slider.SliderEventonEndDragSlider  // 0x210
  public            UnityEngine.UI.Slider.SliderEventonClickSlider  // 0x218
  public            UnityEngine.UI.Slider.SliderEventonClickSliderHandle  // 0x220
  private           Beyond.TickFunction             m_gamepadStickScrollTickFunction  // 0x228
  public            System.String                   sliderAddActionId  // 0x230
  public            System.String                   sliderReduceActionId  // 0x238
  private   readonly Beyond.DisposedUnityEvent       <bindingViewUpdateEvent>k__BackingField  // 0x240
  private           System.Int32                    m_bindingGroupId  // 0x248
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x250
  public            System.Boolean                  snapStep  // 0x258
  public            System.Single                   stepValue  // 0x25c
  protected         System.Boolean                  _enableGamepadStickScroll  // 0x260
  protected         System.Single                   _gamepadStickScrollTolerance  // 0x264
  protected         System.Single                   _gamepadStickOverPushThreshold  // 0x268
  protected         System.Int32                    _gamepadStickOverPushMultiplier  // 0x26c
  protected         System.Int32                    _gamepadStickScrollBatchCount  // 0x270
  private           System.Int32                    m_gamepadStickScrollHoriBindingId  // 0x274
PROPERTIES:
  bindingViewActionId  get=0x0B0F5744
  bindingViewLabelText  get=0x0B0F5794
  bindingViewUpdateEvent  get=0x04D8DE80
  redDotTrans  get=0x0B0F5A20
  bindingEnabled  get=0x0B0F56F4
  bindingViewState  get=0x0B0F580C
  groupId  get=0x0465E310
  parentTarget  get=0x04D8DCE0  set=0x06B8B6E8
  groupEnabled  get=0x0B0F5998
METHODS:
  RVA=0x042B6E80  token=0x60009CA  System.Void _TryPostSlideAudioEvent()
  RVA=0x03F714B0  token=0x60009CB  System.Single ClampValue(System.Single input)
  RVA=0x0B0F4D30  token=0x60009CC  System.Single CustomProcessNormalizedValue(System.Single val)
  RVA=0x042B6E40  token=0x60009CD  System.Void OnValueChanged()
  RVA=0x0B0F4F10  token=0x60009CE  System.Void OnClickSliderBar()
  RVA=0x0B0F4F94  token=0x60009CF  System.Void OnClickSliderHandle()
  RVA=0x04535700  token=0x60009D0  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0B0F4EB4  token=0x60009D1  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0F504C  token=0x60009D2  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D3510  token=0x60009D3  System.Void Awake()
  RVA=0x03935580  token=0x60009D4  System.Void OnEnable()
  RVA=0x0447EBD0  token=0x60009D5  System.Void OnDisable()
  RVA=0x0B0F5350  token=0x60009D6  System.Void _CreateGamepadStickScrollTickFunction()
  RVA=0x0447ECA0  token=0x60009D7  System.Void _ReleaseGamepadStickScrollTickFunction()
  RVA=0x0B0F5474  token=0x60009D8  System.Void _GamepadStickScrollTick(System.Single deltaTime)
  RVA=0x04845F60  token=0x60009D9  System.Void ClearComponent()
  RVA=0x0465E270  token=0x60009DA  System.Void OnDestroy()
  RVA=0x0B0F4DBC  token=0x60009E1  System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType)
  RVA=0x0B0F4C38  token=0x60009E2  System.Void Beyond.Input.IBindingView.OnInputKeyDown(System.Boolean isActionTrigger)
  RVA=0x0B0F4C8C  token=0x60009E3  System.Void Beyond.Input.IBindingView.OnInputKeyUp(System.Boolean isActionTrigger)
  RVA=0x037D3560  token=0x60009E8  System.Void _InitBinding()
  RVA=0x0B0F50FC  token=0x60009E9  System.Void _AddValue()
  RVA=0x0B0F5688  token=0x60009EA  System.Void _ReduceValue()
  RVA=0x037D3690  token=0x60009EB  System.Void _InitGamepadStickScroll()
  RVA=0x0B0F55C0  token=0x60009EC  System.Void _OnGamepadStickScrollHoriValue()
  RVA=0x0B0F5168  token=0x60009ED  System.Void _ApplyGamepadStickScroll(System.Single deltaConcern)
  RVA=0x0435C390  token=0x60009EE  System.Void .ctor()
  RVA=0x0B0F4CE0  token=0x60009EF  System.Boolean Beyond.Input.IBindingView.get_interactable()
  RVA=0x0B0F50EC  token=0x60009F0  System.Single <>iFixBaseProxy_ClampValue(System.Single P0)
  RVA=0x04D9A920  token=0x60009F1  System.Single <>iFixBaseProxy_CustomProcessNormalizedValue(System.Single P0)
  RVA=0x041E1670  token=0x60009F2  System.Void <>iFixBaseProxy_OnValueChanged()
  RVA=0x041E1670  token=0x60009F3  System.Void <>iFixBaseProxy_OnClickSliderBar()
  RVA=0x041E1670  token=0x60009F4  System.Void <>iFixBaseProxy_OnClickSliderHandle()
  RVA=0x0B0C581C  token=0x60009F5  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x0B0869D4  token=0x60009F6  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0F50F4  token=0x60009F7  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0447EC90  token=0x60009F8  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0869DC  token=0x60009F9  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UISoftMask
TYPE:  class
TOKEN: 0x2000101
SIZE:  0x1C0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Vector3[]           m_fourCornersArray  // 0x68
  private           UnityEngine.Vector3[]           m_fourCornersArrayWorld  // 0x70
  private           System.Collections.Generic.List<Beyond.UI.UISoftMaskable>m_cacheSoftMaskables  // 0x78
  private   static  System.Collections.Generic.List<UnityEngine.Canvas>s_cacheCanvas  // static @ 0x0
  private           UnityEngine.UI.Image            m_image  // 0x80
  private           System.Boolean                  m_registeredWillRenderCanvases  // 0x88
  private           UnityEngine.Shader              _maskableDefaultShader  // 0x90
  private           UnityEngine.Shader              _maskableTextShader  // 0x98
  private   static  System.Int32                    s_SoftMaskTexId  // static @ 0x8
  private   static  System.Int32                    s_SoftMaskTexSTId  // static @ 0xc
  private   static  System.Int32                    s_WorldToSoftMaskId  // static @ 0x10
  private   static  System.Int32                    s_InnerSoftMaskId  // static @ 0x14
  private   static  System.Int32                    s_InnerSoftMaskUVId  // static @ 0x18
  private   static  System.Int32                    s_SpriteSlicedFlag  // static @ 0x1c
  private           UnityEngine.Texture             m_mainTexture  // 0xa0
  private           UnityEngine.Vector4             m_spriteTillingOffset  // 0xa8
  private           UnityEngine.Matrix4x4           m_worldToSoftMask  // 0xb8
  private           UnityEngine.Vector4             m_innerSoftMask  // 0xf8
  private           UnityEngine.Vector4             m_innerSoftMaskUV  // 0x108
  private           UnityEngine.UI.Image.Type       m_lastImageType  // 0x118
  private           UnityEngine.Sprite              m_lastSprite  // 0x120
  private           UnityEngine.Rect                m_lastPixelAdjustedRect  // 0x128
  private           System.Single                   m_lastPixelsPerUnit  // 0x138
  private           System.Single                   m_lastPixelsPerUnitMultiplier  // 0x13c
  private           System.Boolean                  m_lastEnableRuntimeAtlas  // 0x140
  private           UnityEngine.Texture             m_lastRuntimeAtlasTexture  // 0x148
  private           UnityEngine.Rect                m_lastRuntimeAtlasRect  // 0x150
  private           UnityEngine.Rect                m_lastRuntimeAtlasTextureRect  // 0x160
  private           UnityEngine.Vector4             m_lastRuntimeAtlasBorder  // 0x170
  private           Beyond.UI.UISoftMask.SlicedSpriteParamsm_slicedSpriteParams  // 0x180
  private           UnityEngine.Material            m_softMaskDefaultMaterial  // 0x1a8
  private           System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material>m_softMaskTextMaterials  // 0x1b0
  private           System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material>m_softMaskUIVFXMaterials  // 0x1b8
PROPERTIES:
  tickOption  get=0x04111830
  softMaskDefaultMaterial  get=0x039409B0
METHODS:
  RVA=0x03940CA0  token=0x60009FB  UnityEngine.Texture _GetSoftMaskTexture()
  RVA=0x03940420  token=0x60009FD  UnityEngine.Material GetTextSoftMaskMaterial(UnityEngine.Material baseMaterial)
  RVA=0x039409E0  token=0x60009FE  UnityEngine.Material GetUIVFXSoftMaskMaterial(UnityEngine.Material baseMaterial)
  RVA=0x0B0F5A6C  token=0x60009FF  UnityEngine.Vector3 GetCornerCanvas(System.Int32 i)
  RVA=0x0B0F5B0C  token=0x6000A00  UnityEngine.Vector3[] GetFourCornnerArrayWorld()
  RVA=0x0309A280  token=0x6000A01  System.Void OnEnable()
  RVA=0x03099ED0  token=0x6000A02  System.Void OnDisable()
  RVA=0x031C4710  token=0x6000A03  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x03B58FA0  token=0x6000A04  System.Void _OnWillRenderCanvases()
  RVA=0x03099BF0  token=0x6000A05  System.Void _Clear()
  RVA=0x03099E80  token=0x6000A06  System.Void _DestroyMaterial(UnityEngine.Material material)
  RVA=0x033A0B90  token=0x6000A07  System.Void _UpdateParam()
  RVA=0x031DF530  token=0x6000A08  System.Void _UpdateChildren(System.Boolean markVertices, System.Boolean markMaterial)
  RVA=0x031C2FD0  token=0x6000A09  System.Boolean _IsImageParamChanged(UnityEngine.UI.Image image)
  RVA=0x03EE49C0  token=0x6000A0A  System.Void _UpdateImageCache(UnityEngine.UI.Image image)
  RVA=0x031CD8D0  token=0x6000A0B  Beyond.UI.UISoftMask.SlicedSpriteParams _CalculateSlicedSpriteParams(UnityEngine.UI.Image image)
  RVA=0x031CDF30  token=0x6000A0C  UnityEngine.Vector4 _GetAdjustedBorders(UnityEngine.Vector4 border, UnityEngine.Rect adjustedRect, UnityEngine.Rect originalRect)
  RVA=0x0B0F5B5C  token=0x6000A0D  UnityEngine.Vector4 _GetRuntimeAtlasSpriteInnerUV(UnityEngine.UI.Image image)
  RVA=0x0B0F5DE4  token=0x6000A0E  UnityEngine.Vector4 _GetRuntimeAtlasSpritePadding(UnityEngine.UI.Image image)
  RVA=0x04485580  token=0x6000A0F  System.Void _OnEnableRuntimeAtlas()
  RVA=0x039C4500  token=0x6000A10  System.Void .ctor()
  RVA=0x04A33210  token=0x6000A11  System.Void .cctor()
  RVA=0x069AFD0C  token=0x6000A12  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x6000A13  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x6000A14  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x06C37238  token=0x6000A15  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISoftMaskable
TYPE:  class
TOKEN: 0x2000103
SIZE:  0xB0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.UI.IMaterialModifier UnityEngine.UI.IMeshModifier
FIELDS:
  public            System.Boolean                  _clipMeshToSaveOverDraw  // 0x68
  private           System.Collections.Generic.List<UnityEngine.UIVertex>m_cacheVertexSteram  // 0x70
  private           System.Collections.Generic.List<UnityEngine.UIVertex>m_cacheDstVertexSteram  // 0x78
  protected static readonly System.String                   UI_MINIMAP_MIST_KEYWORD  // static @ 0x0
  protected static readonly System.String                   UI_VFX_PARAM  // static @ 0x8
  private           Beyond.UI.UISoftMaskable.TestLine[]m_testLinesCache  // 0x80
  private           UnityEngine.Material            m_softMaskModefiedMat  // 0x88
  private           UnityEngine.UI.Graphic          m_graphic  // 0x90
  private           Beyond.UI.UISoftMask            m_softMask  // 0x98
  private           Beyond.UI.UIText                m_uiText  // 0xa0
  private           Beyond.UI.UISoftMaskable.UIType m_uiType  // 0xa8
PROPERTIES:
  tickOption  get=0x03E1DBB0
  softMaskModefiedMat  get=0x0B0FA97C
  graphic  get=0x031E0600
  softMask  get=0x039408E0
  uiText  get=0x0B0FA9CC
METHODS:
  RVA=0x03940790  token=0x6000A1C  UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
  RVA=0x031E0040  token=0x6000A1D  System.Void OnEnable()
  RVA=0x031DFF40  token=0x6000A1E  System.Void OnDisable()
  RVA=0x031DEB00  token=0x6000A1F  System.Void LateTick(System.Single deltaTime)
  RVA=0x031DFA00  token=0x6000A20  System.Void MarkGraphicVerticesDirty()
  RVA=0x031DEC60  token=0x6000A21  System.Void MarkGraphicMaterialDirty()
  RVA=0x0B0F909C  token=0x6000A22  System.Void ModifyMesh(UnityEngine.Mesh mesh)
  RVA=0x0B0FA83C  token=0x6000A23  System.Boolean _Equal(System.Single a, System.Single b)
  RVA=0x0B0FA47C  token=0x6000A24  UnityEngine.Vector2 _ComputeLineCross(UnityEngine.Vector2 line0p0, UnityEngine.Vector2 line0p1, UnityEngine.Vector2 line1p0, UnityEngine.Vector2 line1p1)
  RVA=0x0B0FA6BC  token=0x6000A25  System.Void _ComputePointAtTriangleUV(UnityEngine.Vector2 p0, UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, UnityEngine.Vector2 q, System.Single& u, System.Single& v)
  RVA=0x0B0F93D0  token=0x6000A26  System.Void _ClipTriangleByClampSoftMaskUV(UnityEngine.UIVertex[] vertices, UnityEngine.UIVertex[] resultPoint, System.Boolean& canSkip)
  RVA=0x0B0F90F0  token=0x6000A27  System.Void _ClipRect()
  RVA=0x039406E0  token=0x6000A28  System.Void ModifyMesh(UnityEngine.UI.VertexHelper vertHelper)
  RVA=0x0B0FA8C0  token=0x6000A29  System.Void _SetUIVertexUV(UnityEngine.UIVertex& uiVertex, UnityEngine.Vector2 uv0, UnityEngine.Vector2 uv1, UnityEngine.Vector2 softMaskUV)
  RVA=0x039C47D0  token=0x6000A2A  System.Void .ctor()
  RVA=0x04D11EB0  token=0x6000A2B  System.Void .cctor()
  RVA=0x069AFD0C  token=0x6000A2C  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x6000A2D  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x6000A2E  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x06BD8710  token=0x6000A2F  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UISortingOrder
TYPE:  class
TOKEN: 0x2000106
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    _sortingOrderOffset  // 0x18
  private           Beyond.UI.UISortingOrder.RenderType_renderType  // 0x1c
  private           UnityEngine.Renderer            m_renderer  // 0x20
  private           UnityEngine.Renderer[]          m_childrenRenderers  // 0x28
  private           UnityEngine.ParticleSystemRenderer[]m_childrenParticleRenderers  // 0x30
  private           UnityEngine.Canvas              m_canvas  // 0x38
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0x40
METHODS:
  RVA=0x045A6830  token=0x6000A30  System.Void OnEnable()
  RVA=0x037D56D0  token=0x6000A31  System.Void _GetRenderer()
  RVA=0x037D5660  token=0x6000A32  System.Void _GetChildrenRenderers()
  RVA=0x0B0FAA94  token=0x6000A33  System.Void _GetChildrenParticleRenderers()
  RVA=0x037D4F60  token=0x6000A34  System.Void _GetCanvas()
  RVA=0x037D4C10  token=0x6000A35  System.Void SetOrder(System.Int32 baseOrder)
  RVA=0x037D3720  token=0x6000A36  System.Void Awake()
  RVA=0x045ECC60  token=0x6000A37  System.Void OnDestroy()
  RVA=0x047C82B0  token=0x6000A38  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIStepScrollList
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x2A8
EXTENDS: Beyond.UI.UIScrollList
FIELDS:
  public            Beyond.DisposedUnityEvent<System.Int32,System.Int32>onCenterIndexChanged  // 0x278
  public            Beyond.DisposedUnityEvent       onScrollOrDragEnd  // 0x280
  public            Beyond.DisposedUnityEvent       onScrollOrDragStart  // 0x288
  protected         System.Single                   _easyDragSpdThreshold  // 0x290
  protected         System.Single                   _easyDragDistPercentThreshold  // 0x294
  private           System.Int32                    m_startDragCenterIndex  // 0x298
  private           System.Int32                    m_centerIndex  // 0x29c
  private           UnityEngine.Vector2             m_scrollStartNormalizedPos  // 0x2a0
PROPERTIES:
  centerIndex  get=0x04D7C710
METHODS:
  RVA=0x039D09D0  token=0x6000A3A  System.Void Init(System.Boolean isPreview)
  RVA=0x04B335D0  token=0x6000A3B  System.Void InitConfig()
  RVA=0x0396D490  token=0x6000A3C  System.Void OnScroll(UnityEngine.Vector2 v)
  RVA=0x0B0FABA0  token=0x6000A3D  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FACA0  token=0x6000A3E  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FAEB8  token=0x6000A3F  System.Void SetTop(System.Boolean needUpdate)
  RVA=0x0396D4F0  token=0x6000A40  System.Void UpdateShowingCells(System.Boolean forceUpdateContent, System.Boolean forceClearShowingCells, System.Boolean ignoreListActive)
  RVA=0x0B0FAB1C  token=0x6000A41  System.Void ClearComponent()
  RVA=0x0B0FB08C  token=0x6000A42  System.Void _OnScrollStart(UnityEngine.Vector2 normalizedPos)
  RVA=0x0B0FAF80  token=0x6000A43  System.Void _OnScrollEnd(UnityEngine.Vector2 normalizedPos)
  RVA=0x0B0FB110  token=0x6000A44  System.Void _TryAutoScroll(System.Boolean isEasyDrag, System.Boolean goNext)
  RVA=0x0396D5B0  token=0x6000A45  System.Int32 _GetClampedIndex()
  RVA=0x0B0FAC30  token=0x6000A46  System.Void OnDisable()
  RVA=0x039C2090  token=0x6000A47  System.Void .ctor()
  RVA=0x0B0FAF58  token=0x6000A48  System.Void <>iFixBaseProxy_Init(System.Boolean P0)
  RVA=0x0B0FAF50  token=0x6000A49  System.Void <>iFixBaseProxy_InitConfig()
  RVA=0x0B0E48CC  token=0x6000A4A  System.Void <>iFixBaseProxy_OnScroll(UnityEngine.Vector2 P0)
  RVA=0x0B0FAF60  token=0x6000A4B  System.Void <>iFixBaseProxy_OnBeginDrag(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0FAF70  token=0x6000A4C  System.Void <>iFixBaseProxy_OnEndDrag(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0FAF78  token=0x6000A4D  System.Void <>iFixBaseProxy_SetTop(System.Boolean P0)
  RVA=0x0B0D2A00  token=0x6000A4E  System.Void <>iFixBaseProxy_UpdateShowingCells(System.Boolean P0, System.Boolean P1, System.Boolean P2)
  RVA=0x0B0D2970  token=0x6000A4F  System.Void <>iFixBaseProxy_ClearComponent()
  RVA=0x0B0FAF68  token=0x6000A50  System.Void <>iFixBaseProxy_OnDisable()
END_CLASS

CLASS: Beyond.UI.UIStyleByState
TYPE:  class
TOKEN: 0x2000109
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Collections.Generic.List<Beyond.UI.UIStyleByState.StyleConfig>styleConfigs  // 0x18
METHODS:
  RVA=0x03CECC10  token=0x6000A51  System.Void Awake()
  RVA=0x039D4470  token=0x6000A52  System.Void OnDestroy()
  RVA=0x03CEBEB0  token=0x6000A53  System.Void UpdateStyle()
  RVA=0x03CEC740  token=0x6000A54  System.Boolean _IsValidStyle(Beyond.UI.UIStyleByState.StyleConfig styleConfig)
  RVA=0x045A5FE0  token=0x6000A55  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISubtitle
TYPE:  class
TOKEN: 0x200010C
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIText                _topText  // 0x18
  private           Beyond.UI.UIText                _bottomText  // 0x20
METHODS:
  RVA=0x0B0FB2D8  token=0x6000A58  System.Void Start()
  RVA=0x0B0FB408  token=0x6000A59  System.Void _SetAsDefault()
  RVA=0x0B0FB1D4  token=0x6000A5A  System.Void SetSubtitle(System.String subtitle, System.Boolean showOnTop)
  RVA=0x0B0FB328  token=0x6000A5B  System.Void UpdateAlpha(System.Single alpha, System.Boolean isTop)
  RVA=0x05393520  token=0x6000A5C  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UITextPair
TYPE:  class
TOKEN: 0x200010D
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Collections.Generic.List<Beyond.UI.UIText>referencedTexts  // 0x18
PROPERTIES:
  text  get=0x0B0FBAEC  set=0x0B0FBD38
  color  set=0x0B0FBBA8
  first  get=0x0B0FBA54
METHODS:
  RVA=0x0B0FB764  token=0x6000A61  System.Void SetAndResolveTextStyle(System.String targetText)
  RVA=0x0B0FB8D0  token=0x6000A62  System.Void SetPhoneticText(Beyond.GEnums.PhoneticType phoneticType, System.String id)
  RVA=0x049D3620  token=0x6000A63  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.TimelineUIBindingType
TYPE:  sealed struct
TOKEN: 0x200010E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.TimelineUIBindingType None  // const
  public    static  Beyond.UI.TimelineUIBindingType Mask  // const
  public    static  Beyond.UI.TimelineUIBindingType BigLogoFitImageTop  // const
  public    static  Beyond.UI.TimelineUIBindingType BigLogoFitImageBottom  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UITimelineBindingTarget
TYPE:  class
TOKEN: 0x200010F
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.TimelineUIBindingType bindingType  // 0x18
METHODS:
  RVA=0x05393520  token=0x6000A64  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIToggle
TYPE:  class
TOKEN: 0x2000110
SIZE:  0x280
EXTENDS: UnityEngine.UI.Toggle
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.GameObject>_onObjects  // 0x1b0
  private           System.Collections.Generic.List<UnityEngine.GameObject>_offObjects  // 0x1b8
  private           Beyond.UI.UIState.UIStateController_uiStateController  // 0x1c0
  private           System.String                   _onStateName  // 0x1c8
  private           System.String                   _offStateName  // 0x1d0
  private           System.String                   _onAnimation  // 0x1d8
  private           System.String                   _offAnimation  // 0x1e0
  private           System.String                   _audioToggleOn  // 0x1e8
  private           System.String                   _audioToggleOff  // 0x1f0
  private           System.String                   _audioInvalid  // 0x1f8
  public            System.Boolean                  _ignoreClickOnDrag  // 0x200
  private           UnityEngine.Vector2             <startPressPos>k__BackingField  // 0x204
  private           Beyond.LangKey                  _invalidClickLangKey  // 0x210
  public            System.Func<System.Boolean,System.Boolean>checkIsValueValid  // 0x220
  private           UnityEngine.Animation           m_animation  // 0x228
  private           System.Int32                    m_groupId  // 0x230
  public            System.String                   toggleActionId  // 0x238
  private           System.Int32                    <toggleBindingId>k__BackingField  // 0x240
  private           System.Boolean                  m_started  // 0x244
  public            System.String                   clickOnHintTextId  // 0x248
  public            System.String                   clickOffHintTextId  // 0x250
  private           System.String                   m_customBindingViewLabelText  // 0x258
  private           Beyond.Input.ActionOnSetNaviTarget_actionOnSetNaviTarget  // 0x260
  private           System.Boolean                  _useNoHintAction  // 0x264
  public            System.String                   onHintTextId  // 0x268
  public            System.String                   offHintTextId  // 0x270
  private           System.Int32                    <hoverConfirmBindingId>k__BackingField  // 0x278
PROPERTIES:
  startPressPos  get=0x04D928D0  set=0x04D92960
  toggleBindingId  get=0x04DAAF70  set=0x04DAAF90
  customBindingViewLabelText  get=0x02E98080  set=0x02E983E0
  hoverConfirmBindingId  get=0x04DAAF60  set=0x04DAAF80
METHODS:
  RVA=0x037D2EC0  token=0x6000A69  System.Void Awake()
  RVA=0x0378E7C0  token=0x6000A6A  System.Void OnEnable()
  RVA=0x0378E710  token=0x6000A6B  System.Void OnDisable()
  RVA=0x0378E860  token=0x6000A6C  System.Void _UpdateBindingEnable()
  RVA=0x0B0FC5FC  token=0x6000A6D  System.Void OnDestroy()
  RVA=0x0378E2D0  token=0x6000A6E  System.Void Start()
  RVA=0x0B0FC564  token=0x6000A6F  System.Void InternalToggle()
  RVA=0x037D3190  token=0x6000A70  System.Void _InitToggleBinding()
  RVA=0x0B0FCC84  token=0x6000A71  System.Boolean _OnToggleFail()
  RVA=0x0B0FC98C  token=0x6000A72  System.Void PlayAudio()
  RVA=0x0378E220  token=0x6000A73  System.Void OnValueChange()
  RVA=0x047DC570  token=0x6000A74  System.Void _NotifyIsHover(System.Boolean isHover)
  RVA=0x0378EC00  token=0x6000A75  System.Void _UpdateObjects()
  RVA=0x0378E320  token=0x6000A76  System.Void _PlayAnimation(System.Boolean skipToEnd)
  RVA=0x0481F770  token=0x6000A77  System.Void ClearComponent()
  RVA=0x0B0FC6C4  token=0x6000A78  System.Void OnInteractableChanged()
  RVA=0x0B0FC4C8  token=0x6000A7B  System.Boolean GetMouseActionHints(System.String& clickHintTextId, System.String& longPressHintTextId)
  RVA=0x0B0FC898  token=0x6000A7C  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FC780  token=0x6000A7D  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FC92C  token=0x6000A7E  System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0B0FCA84  token=0x6000A7F  System.Void TryInternalToggle()
  RVA=0x0B0FC714  token=0x6000A82  System.Void OnNaviTargetEnabledAgain()
  RVA=0x0B0FCB40  token=0x6000A83  System.Void _OnSetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode)
  RVA=0x037D2F50  token=0x6000A84  System.Void _InitActionOnSetNaviTarget()
  RVA=0x0B0FCDD4  token=0x6000A85  System.Void _ToggleNaviInputBinding(System.Boolean active)
  RVA=0x037D3150  token=0x6000A86  System.Void _TryRefreshToggleBindingText()
  RVA=0x0378E280  token=0x6000A87  System.Void _TryRefreshHoverConfirmBindingText()
  RVA=0x02E976D0  token=0x6000A88  System.Void _TryRefreshBindingText(System.Int32 bindingId, System.Boolean useCustomBindingViewLabelText)
  RVA=0x0B0FCA28  token=0x6000A89  System.Void RefreshHintText()
  RVA=0x0435DDD0  token=0x6000A8A  System.Void .ctor()
  RVA=0x0B0869D4  token=0x6000A8B  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0FCB30  token=0x6000A8C  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0FCB28  token=0x6000A8D  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0FCB20  token=0x6000A8E  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x05393868  token=0x6000A8F  System.Void <>iFixBaseProxy_Start()
  RVA=0x0B0FCB18  token=0x6000A90  System.Void <>iFixBaseProxy_InternalToggle()
  RVA=0x041E1670  token=0x6000A91  System.Void <>iFixBaseProxy_OnValueChange()
  RVA=0x0B0C581C  token=0x6000A92  System.Void <>iFixBaseProxy__NotifyIsHover(System.Boolean P0)
  RVA=0x041E1670  token=0x6000A93  System.Void <>iFixBaseProxy_OnInteractableChanged()
  RVA=0x0B0C57FC  token=0x6000A94  System.Boolean <>iFixBaseProxy_GetMouseActionHints(System.String& P0, System.String& P1)
  RVA=0x0B0C580C  token=0x6000A95  System.Void <>iFixBaseProxy_OnPointerDown(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0FCB38  token=0x6000A96  System.Void <>iFixBaseProxy_OnPointerClick(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0AC6E908  token=0x6000A97  System.Void <>iFixBaseProxy_OnSubmit(UnityEngine.EventSystems.BaseEventData P0)
  RVA=0x0B0C5804  token=0x6000A98  System.Void <>iFixBaseProxy_OnNaviTargetEnabledAgain()
  RVA=0x041E1670  token=0x6000A99  System.Void <>iFixBaseProxy__OnSetAsNaviTarget(System.Boolean P0, System.Boolean P1)
END_CLASS

CLASS: Beyond.UI.UIToggleGroup
TYPE:  class
TOKEN: 0x2000111
SIZE:  0x48
EXTENDS: UnityEngine.UI.ToggleGroup
FIELDS:
  private           System.String                   _moveToNextActionId  // 0x28
  private           System.String                   _moveToPreviousActionId  // 0x30
  private           System.Boolean                  _inverse  // 0x38
  private           System.Int32                    <moveToNextBindingId>k__BackingField  // 0x3c
  private           System.Int32                    <moveToPreviousBindingId>k__BackingField  // 0x40
PROPERTIES:
  moveToNextBindingId  get=0x04D86640  set=0x04D86670
  moveToPreviousBindingId  get=0x04D865E0  set=0x04D86610
METHODS:
  RVA=0x049CE2A0  token=0x6000A9E  System.Void Awake()
  RVA=0x0481BF00  token=0x6000A9F  System.Void OnEnable()
  RVA=0x0B0FBFFC  token=0x6000AA0  System.Void OnDisable()
  RVA=0x0B0FBF14  token=0x6000AA1  System.Void OnDestroy()
  RVA=0x0B0FBE64  token=0x6000AA2  System.Void MoveToNext()
  RVA=0x0B0FBEBC  token=0x6000AA3  System.Void MoveToPrevious()
  RVA=0x0B0FC0EC  token=0x6000AA4  System.Void _MoveTo(System.Boolean isNext)
  RVA=0x045C5DC0  token=0x6000AA5  System.Void .ctor()
  RVA=0x041E1670  token=0x6000AA6  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0481BFF0  token=0x6000AA7  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x041E1670  token=0x6000AA8  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x041E1670  token=0x6000AA9  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.FingerMoveDirection
TYPE:  sealed struct
TOKEN: 0x2000112
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.FingerMoveDirection   None  // const
  public    static  Beyond.UI.FingerMoveDirection   Anisotropy  // const
  public    static  Beyond.UI.FingerMoveDirection   Same  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UITouchPanel
TYPE:  class
TOKEN: 0x2000113
SIZE:  0x1E8
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp Beyond.Input.IBindingGroupTarget
FIELDS:
  private           System.Single                   _gestureZoomSensitivity  // 0x68
  private           System.Single                   _dragThreshold  // 0x6c
  private           System.Single                   _longPressTime  // 0x70
  private           System.Single                   _longPressMaxDist  // 0x74
  private           System.Single                   _mouseZoomSensitivity  // 0x78
  private           System.Single                   _scrollZoomSensitivity  // 0x7c
  private           System.Boolean                  _useGesturePixelSizeAdjustment  // 0x80
  private           System.Single                   _multiClickInterval  // 0x84
  private           System.Int32                    _multiClickPixelOffset  // 0x88
  private           System.Boolean                  _ignoreClickOnLongPress  // 0x8c
  private           System.Boolean                  _clickThrough  // 0x8d
  private           System.Boolean                  _preventPressWhenClickThrough  // 0x8e
  private           System.Boolean                  m_triggeredLongPress  // 0x8f
  private           System.Int32                    m_clickCount  // 0x90
  private           System.Single                   m_lastClickTime  // 0x94
  private           UnityEngine.Vector2             m_lastClickPos  // 0x98
  private           System.Boolean                  m_isPress  // 0xa0
  private           System.Boolean                  m_isInGestureZoom  // 0xa1
  private           System.Boolean                  m_isActionLongPress  // 0xa2
  private           System.Boolean                  m_dragStarted  // 0xa3
  private           UnityEngine.Vector2             m_dragPosition  // 0xa4
  private           UnityEngine.RectTransform       m_rect  // 0xb0
  private           System.Single                   m_deltaX  // 0xb8
  private           System.Single                   m_deltaY  // 0xbc
  private           UnityEngine.Vector2             m_lastFrameTouchPos  // 0xc0
  private           UnityEngine.EventSystems.PointerEventDatam_touch  // 0xc8
  private           System.Int32                    m_touchId  // 0xd0
  private           System.Int32                    m_touchId1  // 0xd4
  private           System.Int32                    m_touchId2  // 0xd8
  private           System.Single                   m_actionLongPressStartTime  // 0xdc
  private           UnityEngine.Vector2             m_actionLongPressStartPos  // 0xe0
  private           System.Single                   m_pixelSizeAdjustment  // 0xe8
  private           UnityEngine.Vector2             m_touch1Pos  // 0xec
  private           UnityEngine.Vector2             m_touch1LastPos  // 0xf4
  private           UnityEngine.Vector2             m_touch2Pos  // 0xfc
  private           UnityEngine.Vector2             m_touch2LastPos  // 0x104
  private           UnityEngine.Vector3             m_lastMousePos  // 0x10c
  private           System.Boolean                  m_lastFrameIsRightMouseButtonPress  // 0x118
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onClick  // 0x120
  public            Beyond.Input.UIEvent<UnityEngine.EventSystems.PointerEventData>onRightClick  // 0x128
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onPress  // 0x130
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onLongPress  // 0x138
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onDoubleClick  // 0x140
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onRelease  // 0x148
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x150
  public            Beyond.DisposedUnityEvent<UnityEngine.Vector2>onDragBegin  // 0x158
  public            Beyond.DisposedUnityEvent<UnityEngine.Vector2>onDragEnd  // 0x160
  public            Beyond.DisposedUnityEvent<System.Single>onZoom  // 0x168
  public            Beyond.DisposedUnityEvent<UnityEngine.Vector2,Beyond.UI.FingerMoveDirection>onHorizonAndVerticalZoom  // 0x170
  public            Beyond.DisposedUnityEvent       onDragToLeft  // 0x178
  public            Beyond.DisposedUnityEvent       onDragToRight  // 0x180
  public            Beyond.DisposedUnityEvent       onPointerEnter  // 0x188
  public            Beyond.DisposedUnityEvent       onPointerExit  // 0x190
  public            Beyond.DisposedUnityEvent       onDragToUp  // 0x198
  public            Beyond.DisposedUnityEvent       onDragToDown  // 0x1a0
  public            Beyond.DisposedUnityEvent<UnityEngine.Vector2>onRightMouseButtonPress  // 0x1a8
  public            Beyond.DisposedUnityEvent<System.Single>onScroll  // 0x1b0
  private           UnityEngine.Vector2             <curPressPos>k__BackingField  // 0x1b8
  private           System.Boolean                  <isPointerEntered>k__BackingField  // 0x1c0
  public            System.Boolean                  enableZoom  // 0x1c1
  public            Beyond.DisposedUnityEvent<UnityEngine.EventSystems.PointerEventData>rawOnDrag  // 0x1c8
  private           System.Single                   m_leftMousePressDownTimeWithNoCursor  // 0x1d0
  private           System.Single                   m_rightMousePressDownTimeWithNoCursor  // 0x1d4
  private           System.Int32                    m_groupId  // 0x1d8
  private           System.Boolean                  m_bindingInit  // 0x1dc
  private           Beyond.Input.InputBindingGroupMonoTarget<parent>k__BackingField  // 0x1e0
PROPERTIES:
  tickOption  get=0x046BEF70
  touchPos  get=0x0B0FD1F0
  curPressPos  get=0x04DAAFA0  set=0x04DAAFC0
  isPointerEntered  get=0x04D8FB30  set=0x04D8FC00
  isDragging  get=0x0B0FD100
  isInGestureZoom  get=0x0B0FD150
  groupEnabled  get=0x03796660
  groupId  get=0x037D5980
  parentTarget  get=0x0B0FD1A0
  parent  get=0x04D8D930  set=0x06B8B2C4
METHODS:
  RVA=0x037D65E0  token=0x6000AB2  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D66D0  token=0x6000AB3  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D6310  token=0x6000AB4  System.Void DoEndDrag(UnityEngine.Vector2 dragPos)
  RVA=0x037D67E0  token=0x6000AB5  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x045D0D60  token=0x6000AB6  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D6920  token=0x6000AB7  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x04A1A6D0  token=0x6000AB8  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0379B570  token=0x6000AB9  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0379B520  token=0x6000ABA  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x045D0EA0  token=0x6000ABB  System.Void _OnActionPress(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D6A80  token=0x6000ABC  System.Void _OnActionRelease(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D6B90  token=0x6000ABD  System.Boolean _IsSameTouchFromPress(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x037D62A0  token=0x6000ABE  System.Void ClearTouchInfo()
  RVA=0x0B0FCF20  token=0x6000ABF  System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FD004  token=0x6000AC0  System.Void SimulateClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B0FCE6C  token=0x6000AC1  System.Void DeActiveTouch(System.Int32 touchId)
  RVA=0x037D5D30  token=0x6000AC2  System.Void OnAwake()
  RVA=0x033FEA50  token=0x6000AC3  System.Void Tick(System.Single deltaTime)
  RVA=0x033FF0C0  token=0x6000AC4  System.Void LateTick(System.Single deltaTime)
  RVA=0x037D5CF0  token=0x6000AC5  System.Void OnEnable()
  RVA=0x037D6230  token=0x6000AC6  System.Void OnDisable()
  RVA=0x037D58E0  token=0x6000AC7  System.Void OnDestroy()
  RVA=0x0451EEB0  token=0x6000AC8  System.Void OnRelease()
  RVA=0x037D59C0  token=0x6000ACE  System.Void InitBinding()
  RVA=0x037D5C10  token=0x6000ACF  System.Void _UpdateState()
  RVA=0x0451EEF0  token=0x6000AD0  System.Void ClearComponent()
  RVA=0x03EB2CB0  token=0x6000AD1  System.Void .ctor()
  RVA=0x0B0FD068  token=0x6000AD2  System.Void <InitBinding>b__112_0()
  RVA=0x0B0FD0B4  token=0x6000AD3  System.Void <InitBinding>b__112_1()
  RVA=0x069AFD0C  token=0x6000AD4  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000AD5  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x6000AD6  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06BD8710  token=0x6000AD7  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B40  token=0x6000AD8  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x6000AD9  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B20  token=0x6000ADA  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x069AFD04  token=0x6000ADB  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UIVFXManager
TYPE:  class
TOKEN: 0x2000114
SIZE:  0xA0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: UnityEngine.UI.IMaterialModifier
FIELDS:
  private   static  System.Int32                    s_vfxMainTexSTId  // static @ 0x0
  private           UnityEngine.Vector4             m_spriteTillingOffset  // 0x68
  private           UnityEngine.Material            m_uiDefaultVFXMaterial  // 0x78
  private           System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material>m_UIVfxMaterials  // 0x80
  private           UnityEngine.Shader              m_uiDefaultShader  // 0x88
  private           UnityEngine.UI.Graphic          m_graphic  // 0x90
  private           Beyond.UI.UISoftMaskable        m_softMaskable  // 0x98
PROPERTIES:
  tickOption  get=0x0B0FDC54
  graphic  get=0x0B0FDAC4
  softMaskable  get=0x0B0FDB8C
METHODS:
  RVA=0x0B0FD464  token=0x6000ADF  System.Void OnEnable()
  RVA=0x0B0FD270  token=0x6000AE0  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B0FD824  token=0x6000AE1  System.Void _OnEnableRuntimeAtlas()
  RVA=0x0B0FD308  token=0x6000AE2  System.Void OnDisable()
  RVA=0x0B0FD5EC  token=0x6000AE3  System.Void _Clear()
  RVA=0x0B0FD79C  token=0x6000AE4  System.Void _DestroyMaterial(UnityEngine.Material material)
  RVA=0x0B0FD874  token=0x6000AE5  System.Void _UpdateParam()
  RVA=0x04B093A0  token=0x6000AE6  UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
  RVA=0x049AF5D0  token=0x6000AE7  System.Void .ctor()
  RVA=0x04D54A80  token=0x6000AE8  System.Void .cctor()
  RVA=0x069AFD0C  token=0x6000AE9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x6000AEA  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x6000AEB  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B30  token=0x6000AEC  System.Void <>iFixBaseProxy_OnDisable()
END_CLASS

CLASS: Beyond.UI.UIWorldLevelScrollListAnimCtrl
TYPE:  class
TOKEN: 0x2000115
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIWorldLevelScrollListAnimCtrl.ScrollListLayoutInfoupLayoutGroup  // 0x18
  public            Beyond.UI.UIWorldLevelScrollListAnimCtrl.ScrollListLayoutInfodownLayoutGroup  // 0x20
  public            Beyond.UI.UIWorldLevelScrollListAnimCtrl.ScrollListLayoutInfomiddleLayoutGroup  // 0x28
  public            UnityEngine.AnimationCurve      speedCurve  // 0x30
  private           System.Collections.IEnumerator  m_enumerator  // 0x38
  private           System.String                   _audioEnd  // 0x40
METHODS:
  RVA=0x0B0FDEE4  token=0x6000AED  System.Void UpdateUpPos(System.Int32 current)
  RVA=0x0B0FDD14  token=0x6000AEE  System.Void UpdateDownPos(System.Int32 current)
  RVA=0x0B0FDDF8  token=0x6000AEF  System.Void UpdateMidPos(System.Int32 current)
  RVA=0x0B0FDFA0  token=0x6000AF0  System.Void UpdateWorldLevelScrollList(System.Boolean isUp, System.Int32 count)
  RVA=0x0B0FDCA4  token=0x6000AF1  System.Void OnDisable()
  RVA=0x0B0FE04C  token=0x6000AF2  System.Collections.IEnumerator _ScrollLayouts(System.Boolean isUp, System.Int32 count)
  RVA=0x0B0FE100  token=0x6000AF3  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.VirtualMouseInitPos
TYPE:  class
TOKEN: 0x2000118
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.LuaPanel              m_panel  // 0x18
PROPERTIES:
  panel  get=0x0B0FE2D8
METHODS:
  RVA=0x0B0FE15C  token=0x6000AFD  System.Void OnEnable()
  RVA=0x05393520  token=0x6000AFE  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CSPopupPanel
TYPE:  class
TOKEN: 0x2000119
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.String                   KEY  // const
  public            Beyond.UI.UIButton              confirmBtn  // 0x18
  public            Beyond.UI.UIButton              cancelBtn  // 0x20
  public            Beyond.UI.UIText                contentTxt  // 0x28
  public            UnityEngine.GameObject          bg1  // 0x30
  public            UnityEngine.GameObject          bg2  // 0x38
  public            UnityEngine.GameObject          warningImg  // 0x40
  public            Beyond.UI.UIScrollRect          scrollRect  // 0x48
  private           System.Action                   m_onConfirm  // 0x50
  private           System.Action                   m_onCancel  // 0x58
  private           Beyond.Resource.FAssetProxyHandlem_handle  // 0x60
  public    static  System.Collections.Generic.HashSet<Beyond.UI.CSPopupPanel>s_instances  // static @ 0x0
  public            System.Int32                    timeScaleKey  // 0x74
METHODS:
  RVA=0x0B0F5FE0  token=0x6000AFF  System.Void Awake()
  RVA=0x0B0F69FC  token=0x6000B00  System.Void _OnConfirm(UnityEngine.EventSystems.PointerEventData p)
  RVA=0x0B0F6958  token=0x6000B01  System.Void _OnCancel(UnityEngine.EventSystems.PointerEventData p)
  RVA=0x0B0F6318  token=0x6000B02  System.Void OnDestroy()
  RVA=0x0B0F64D4  token=0x6000B03  System.Void Show(System.String content, System.Action onConfirm, System.Boolean showCancel, System.Action onCancel, System.String confirmTxt, System.String cancelTxt, System.Boolean showWarning)
  RVA=0x0B0F6124  token=0x6000B04  System.Void ClearAllPanels()
  RVA=0x05393520  token=0x6000B05  System.Void .ctor()
  RVA=0x0B0F6AA0  token=0x6000B06  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.LuaUIRoot
TYPE:  class
TOKEN: 0x200011A
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  public            Beyond.SerializeReferenceDictionary<System.String,Beyond.UI.LuaUIRootNode>nodeDic  // 0x18
METHODS:
  RVA=0x0B0F7CB8  token=0x6000B07  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.LuaUIRootNode
TYPE:  class
TOKEN: 0x200011B
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  public            System.String                   panelName  // 0x18
  public            System.String                   panelFolder  // 0x20
METHODS:
  RVA=0x05393520  token=0x6000B08  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.MobileMotionManager
TYPE:  class
TOKEN: 0x200011C
SIZE:  0x20
EXTENDS: Beyond.BaseManager
FIELDS:
  private   static  System.String                   COMMON_MOTION_SHORT  // const
  private   static  System.String                   COMMON_MOTION_OPERATE_SUCCESS  // const
  private   static  System.String                   COMMON_MOTION_OPERATE_FAILURE  // const
  private   static  System.String                   MOTION_LEVEL_LOW  // const
  private   static  System.String                   MOTION_LEVEL_MEDIUM  // const
  private   static  System.String                   MOTION_LEVEL_HIGH  // const
  private           Beyond.UI.MobileMotionManager.MotionLevelm_currMotionLevel  // 0x18
METHODS:
  RVA=0x04A30330  token=0x6000B09  System.Void OnInit()
  RVA=0x0B0F7D38  token=0x6000B0A  System.Void OnRelease()
  RVA=0x0B0F7F30  token=0x6000B0B  System.UInt32 PostEventCommonShort()
  RVA=0x0B0F7EBC  token=0x6000B0C  System.UInt32 PostEventCommonOperateSuccess()
  RVA=0x0B0F7E48  token=0x6000B0D  System.UInt32 PostEventCommonOperateFailure()
  RVA=0x0B0F7FA4  token=0x6000B0E  System.UInt32 PostEvent(System.String eventName)
  RVA=0x04A30440  token=0x6000B0F  System.Void _CheckMotionLevelAndPostIfChange()
  RVA=0x041E1670  token=0x6000B10  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.I18nFontLoader
TYPE:  static class
TOKEN: 0x200011E
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Int32,Beyond.Resource.FAssetProxyHandle>s_id2FontDic  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.String,Beyond.Resource.FAssetProxyHandle>s_name2MaterialDic  // static @ 0x8
  private   static  Beyond.Resource.FAssetProxyHandles_fontSearchDataHandle  // static @ 0x10
PROPERTIES:
  fontSearchData  get=0x0351E260
METHODS:
  RVA=0x0351E3F0  token=0x6000B12  System.Boolean IsInvalid()
  RVA=0x0B0F7A6C  token=0x6000B13  UnityEngine.Material GetMaterial(System.String matName)
  RVA=0x0351E1B0  token=0x6000B14  TMPro.TMP_FontAsset GetI18nFont(System.Int32 cnFontId)
  RVA=0x0B0F79F4  token=0x6000B15  TMPro.TMP_FontAsset GetCNFont(System.Int32 cnFontId)
  RVA=0x0351E520  token=0x6000B16  TMPro.TMP_FontAsset _GetFontById(System.Int32 fontId)
  RVA=0x0B0F7954  token=0x6000B17  System.Int32 GetCNFontId(TMPro.TMP_FontAsset font)
  RVA=0x046BB800  token=0x6000B18  System.Boolean IsFontDefaultMaterial(UnityEngine.Material mat)
  RVA=0x04CA41A0  token=0x6000B19  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.DynamicFontAssetLoader
TYPE:  class
TOKEN: 0x200011F
SIZE:  0x18
IMPLEMENTS: TMPro.IFontAssetLoader
FIELDS:
  private           System.Collections.Generic.List<Beyond.UI.DynamicFontAssetLoader.FontAssetRef><fontAssets>k__BackingField  // 0x10
  private   static  System.Collections.Generic.HashSet<System.UInt32>s_notFoundUnicodes  // static @ 0x0
PROPERTIES:
  fontAssets  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x04659250  token=0x6000B1C  System.Void .ctor()
  RVA=0x04A12CA0  token=0x6000B1D  System.Void AddNotFoundUnicode(System.UInt32 unicode, System.Boolean forceLog)
  RVA=0x0B0F6CD0  token=0x6000B1E  System.Boolean TryGetFontAsset(TMPro.TMP_Text refText, System.UInt32 unicode, TMPro.TMP_FontAsset& fontAsset)
  RVA=0x0B0F7128  token=0x6000B1F  System.Void TryRemoveRefText(TMPro.TMP_Text refText)
  RVA=0x0B0F6B2C  token=0x6000B20  System.Void Dispose()
  RVA=0x04D18450  token=0x6000B21  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.TweenUtils
TYPE:  static class
TOKEN: 0x2000121
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02F44780  token=0x6000B26  System.Boolean TickAsCountDown(System.Single& countDown, System.Single timeDelta)
END_CLASS

CLASS: Beyond.UI.UIAnimationLayerMixPlayer
TYPE:  class
TOKEN: 0x2000123
SIZE:  0x70
EXTENDS: Beyond.Playables.SingleMixerAssetPlayer`4
FIELDS:
  private           UnityEngine.Animator            m_target  // 0x60
  private           System.String                   m_outputName  // 0x68
METHODS:
  RVA=0x03D502F0  token=0x6000B2C  System.Void .ctor(UnityEngine.Animator target, System.String outputName)
  RVA=0x03D4F830  token=0x6000B2D  System.Void LockToClip(UnityEngine.AnimationClip clip, System.Single clipPos)
  RVA=0x0421E7C0  token=0x6000B2E  UnityEngine.Playables.PlayableGraph CreateGraph()
  RVA=0x04398AF0  token=0x6000B2F  UnityEngine.Animations.AnimationLayerMixerPlayable CreateMixer(UnityEngine.Playables.PlayableGraph graph, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FMixerOptions<UnityEngine.AnimationClip>& options)
  RVA=0x04D31C50  token=0x6000B30  UnityEngine.Animations.AnimationPlayableOutput CreateOutput(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x03431700  token=0x6000B31  UnityEngine.Animations.AnimationClipPlayable CreateSourceFromAsset(UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableGraph graph, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FPlayableMeta<UnityEngine.AnimationClip>& meta)
END_CLASS

CLASS: Beyond.UI.UIAnimationMixPlayer
TYPE:  class
TOKEN: 0x2000124
SIZE:  0x70
EXTENDS: Beyond.Playables.SingleMixerAssetPlayer`4
FIELDS:
  private           UnityEngine.Animator            m_target  // 0x60
  private           System.String                   m_outputName  // 0x68
METHODS:
  RVA=0x03D50170  token=0x6000B32  System.Void .ctor(UnityEngine.Animator target, System.String outputName)
  RVA=0x03D501E0  token=0x6000B33  System.Void LockToClip(UnityEngine.AnimationClip clip, System.Single clipPos)
  RVA=0x0421E810  token=0x6000B34  UnityEngine.Playables.PlayableGraph CreateGraph()
  RVA=0x03E0FD50  token=0x6000B35  UnityEngine.Animations.AnimationMixerPlayable CreateMixer(UnityEngine.Playables.PlayableGraph graph, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FMixerOptions<UnityEngine.AnimationClip>& options)
  RVA=0x03FC8990  token=0x6000B36  UnityEngine.Animations.AnimationPlayableOutput CreateOutput(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x034319D0  token=0x6000B37  UnityEngine.Animations.AnimationClipPlayable CreateSourceFromAsset(UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableGraph graph, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FPlayableMeta<UnityEngine.AnimationClip>& meta)
END_CLASS

CLASS: Beyond.UI.UIListCache`1
TYPE:  class
TOKEN: 0x2000125
FIELDS:
  private           Beyond.UI.UIListCache.FOptions<TComp>m_options  // 0x0
  private           System.Collections.Generic.List<Beyond.UI.UIListCache.ItemWrapper<TComp>>m_items  // 0x0
  private           System.Int32                    m_count  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000B38  System.Void .ctor(Beyond.UI.UIListCache.FOptions<TComp> options)
  RVA=-1  // not resolved  token=0x6000B39  System.Void Refresh(System.Int32 count, System.Action<TComp,System.Int32> refreshFunc, System.Boolean shouldHide, System.Action<TComp,System.Int32> onDisableFunc)
  RVA=-1  // not resolved  token=0x6000B3A  TComp GetItem(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000B3B  Beyond.UI.UIListCache.ItemWrapper<TComp> _CreateNewItem()
END_CLASS

CLASS: Beyond.UI.UILoadImageSprite
TYPE:  class
TOKEN: 0x2000128
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  isAsync  // 0x18
  public            System.Boolean                  setNativeSize  // 0x19
  protected         System.String                   m_spritePath  // 0x20
  protected         UnityEngine.UI.Image            m_imageComp  // 0x28
  protected         Beyond.Resource.FAssetProxyHandlem_handle  // 0x30
PROPERTIES:
  spritePath  get=0x0B0F8FC8  set=0x0B0F9018
METHODS:
  RVA=0x0B0F8C3C  token=0x6000B3F  System.Void OnDestroy()
  RVA=0x0B0F8C9C  token=0x6000B40  System.Void _LoadSprite(System.String path)
  RVA=0x05393520  token=0x6000B41  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILoadImageSpriteByName
TYPE:  class
TOKEN: 0x200012A
SIZE:  0x50
EXTENDS: Beyond.UI.UILoadImageSprite
FIELDS:
  private           System.String                   _folderPath  // 0x48
PROPERTIES:
  spriteName  set=0x0B0F8B94
METHODS:
  RVA=0x03695570  token=0x6000B45  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISwitchTween
TYPE:  abstract class
TOKEN: 0x200012B
SIZE:  0x30
FIELDS:
  private           Beyond.UI.UISwitchTween.Options m_options  // 0x10
  private           Beyond.UI.UISwitchTween.ITweenHandlerm_tween  // 0x20
  private           System.Boolean                  m_isInited  // 0x28
  private           System.Boolean                  m_isShowing  // 0x29
  private           Beyond.UI.UISwitchTween.TweenContextm_context  // 0x2c
PROPERTIES:
  isTweening  get=0x03405A30
  isShow  get=0x04B48120  set=0x03406FE0
METHODS:
  RVA=-1  // abstract  token=0x6000B46  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfShow()
  RVA=-1  // abstract  token=0x6000B47  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfHide()
  RVA=0x0B0FB57C  token=0x6000B48  System.Void BeforeShowEffect()
  RVA=0x03405A90  token=0x6000B49  System.Void BeforeHideEffect()
  RVA=0x04A3B9B0  token=0x6000B4A  System.Void AfterShowEffect()
  RVA=0x0B0FB538  token=0x6000B4B  System.Void AfterHideEffect()
  RVA=0x0B0FB60C  token=0x6000B4C  System.Void ResetToState(System.Boolean isShow)
  RVA=0x03405CC0  token=0x6000B4D  System.Void Show()
  RVA=0x03407030  token=0x6000B4E  System.Void Hide()
  RVA=0x0B0FB6D4  token=0x6000B4F  System.Collections.IEnumerator WaitForTweening()
  RVA=0x0B0FB660  token=0x6000B50  System.Void SetOptions(Beyond.UI.UISwitchTween.Options options)
  RVA=0x034057E0  token=0x6000B54  System.Void Reset(System.Boolean isShow)
  RVA=0x048AB730  token=0x6000B55  System.Void Clear()
  RVA=0x03405840  token=0x6000B56  System.Void _ClearTweenInternal(System.Boolean isReset)
  RVA=0x0B0FB5C0  token=0x6000B57  Beyond.UI.UISwitchTween.TweenContext GetContext()
  RVA=0x034058E0  token=0x6000B58  System.Void ClearTween()
  RVA=0x041E1670  token=0x6000B59  System.Void .ctor()
  RVA=0x04A3B980  token=0x6000B5A  System.Void <Show>b__17_0()
  RVA=0x04CB2740  token=0x6000B5B  System.Void <Hide>b__18_0()
END_CLASS

CLASS: Beyond.UI.FadeSwitchTween
TYPE:  class
TOKEN: 0x2000132
SIZE:  0x58
EXTENDS: Beyond.UI.UISwitchTween
FIELDS:
  public    static  System.Single                   DEFAULT_TWEEN_DURATION  // const
  private           UnityEngine.CanvasGroup         m_alphaHandler  // 0x30
  private           System.Boolean                  m_ignoreTimeScale  // 0x38
  private           System.Single                   m_activeAlpha  // 0x3c
  private           System.Boolean                  <dontDisableGameObject>k__BackingField  // 0x40
  private           System.Boolean                  <controlBlockRaycast>k__BackingField  // 0x41
  private           Beyond.UI.FadeSwitchTween.Durations<complexDuration>k__BackingField  // 0x44
  private           System.Single                   <duration>k__BackingField  // 0x54
PROPERTIES:
  dontDisableGameObject  get=0x02D480A0  set=0x02D480B0
  controlBlockRaycast  get=0x04D86F40  set=0x04D86F60
  complexDuration  get=0x04DAAF10  set=0x04DAAF20
  duration  get=0x04D86E00  set=0x04D86E10
  alphaHandler  get=0x0B0F7548
  activeAlpha  get=0x0B0F74F8  set=0x0B0F7598
METHODS:
  RVA=0x0B0F74D4  token=0x6000B7A  System.Void .ctor(UnityEngine.CanvasGroup alphaHandler, System.Boolean ignoreTimeScale)
  RVA=0x03F0F500  token=0x6000B7B  System.Void .ctor(UnityEngine.CanvasGroup alphaHandler, System.Single duration, System.Boolean ignoreTimeScale)
  RVA=0x04590660  token=0x6000B81  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfHide()
  RVA=0x04591580  token=0x6000B82  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfShow()
  RVA=0x0450BA10  token=0x6000B83  System.Void BeforeShowEffect()
  RVA=0x0450BA70  token=0x6000B84  System.Void AfterHideEffect()
  RVA=0x0450BAB0  token=0x6000B85  System.Void ResetToState(System.Boolean isShow)
  RVA=0x0450BB20  token=0x6000B86  System.Void SetObjectActive(UnityEngine.CanvasGroup alphaHandler, System.Boolean isActive)
  RVA=0x0B0F7484  token=0x6000B87  System.Void Release()
  RVA=0x0B0C1AA0  token=0x6000B88  System.Void <>iFixBaseProxy_BeforeShowEffect()
  RVA=0x0B0C1A98  token=0x6000B89  System.Void <>iFixBaseProxy_AfterHideEffect()
  RVA=0x0B0C1AA8  token=0x6000B8A  System.Void <>iFixBaseProxy_ResetToState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.FadeTranslationSwitchTween
TYPE:  class
TOKEN: 0x2000135
SIZE:  0x60
EXTENDS: Beyond.UI.UISwitchTween
FIELDS:
  private   static  System.Single                   DEFAULT_TWEEN_DURATION  // const
  private           UnityEngine.CanvasGroup         m_alphaHandler  // 0x30
  private           UnityEngine.RectTransform       m_posHandler  // 0x38
  private           UnityEngine.Vector2             m_hidePos  // 0x40
  private           UnityEngine.Vector2             m_showPos  // 0x48
  private           System.Single                   m_duration  // 0x50
  private           System.Single                   m_hideDelay  // 0x54
  private           System.Single                   m_showDelay  // 0x58
METHODS:
  RVA=0x0B0FEA50  token=0x6000B8E  System.Void .ctor(UnityEngine.CanvasGroup alphaHandler, UnityEngine.RectTransform posHandler, UnityEngine.Vector2 hidePos, UnityEngine.Vector2 showPos, System.Single duration, System.Single hideDelay, System.Single showDelay)
  RVA=0x0B0FE62C  token=0x6000B8F  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfHide()
  RVA=0x0B0FE76C  token=0x6000B90  Beyond.UI.UISwitchTween.ITweenHandler GenerateTweenOfShow()
  RVA=0x0B0FE5C8  token=0x6000B91  System.Void BeforeShowEffect()
  RVA=0x0B0FE564  token=0x6000B92  System.Void AfterHideEffect()
  RVA=0x0B0FE8AC  token=0x6000B93  System.Void ResetToState(System.Boolean isShow)
  RVA=0x0B0FE970  token=0x6000B94  System.Single _GetTargetAlpha(System.Boolean isShow)
  RVA=0x0B0FE9D8  token=0x6000B95  UnityEngine.Vector2 _GetTargetPos(System.Boolean isShow)
  RVA=0x0B0C1AA0  token=0x6000B96  System.Void <>iFixBaseProxy_BeforeShowEffect()
  RVA=0x0B0C1A98  token=0x6000B97  System.Void <>iFixBaseProxy_AfterHideEffect()
  RVA=0x0B0C1AA8  token=0x6000B98  System.Void <>iFixBaseProxy_ResetToState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.UIAtlasManager
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x50
EXTENDS: Beyond.Singleton`1
FIELDS:
  private           Beyond.TickFunction             m_lateTickFunction  // 0x10
  public    static  System.Int32                    ATLAS_PAGE_WIDTH  // const
  public    static  System.Int32                    ATLAS_PAGE_HEIGHT  // const
  public    static  System.Int32                    IMAGE_USING_ATLAS_MAX_SIZE  // const
  public    static  System.Int32                    UI_IMAGE_BLOCK_ALIGNMENT  // const
  public    static  System.Int32                    PANEL_LEVEL_COUNT  // const
  public    static  System.Int32                    MAX_ATLAS_PER_PANEL_LEVEL  // const
  public    static  System.Int32                    PROCESS_COUNT_PER_FRAME  // const
  public    static  UnityEngine.Experimental.Rendering.GraphicsFormatATLAS_PAGE_FORMAT  // const
  private           System.Boolean                  m_enableRuntimeAtlas  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.UIImage>m_imageDict  // 0x20
  private           Beyond.UI.UIAtlasManager.UIAtlasPage[,]m_atlasPagePool  // 0x28
  private           System.Collections.Generic.Queue<Beyond.UI.UIImage>m_insertQueue  // 0x30
  private           System.Collections.Generic.Queue<Beyond.UI.UIAtlasManager.UIAtlasHandle>m_freeQueue  // 0x38
  private           UnityEngine.Rendering.CommandBufferm_commandBuffer  // 0x40
  private           UnityEngine.Experimental.Rendering.GraphicsFormatm_atlasPageFormat  // 0x48
  private   static  System.Boolean                  <enableRuntimeAtlasFromScript>k__BackingField  // static @ 0x0
PROPERTIES:
  enableRuntimeAtlas  get=0x031EB7B0
  enableRuntimeAtlasFromScript  get=0x0B105ADC  set=0x0B105B54
  releaseImageSpriteAfterInsert  get=0x0B105B14
METHODS:
  RVA=0x0B105AA4  token=0x6000BA2  System.Void .ctor()
  RVA=0x0B1041D0  token=0x6000BA3  System.Void OnInit()
  RVA=0x0B1043D0  token=0x6000BA4  System.Void OnUnInit()
  RVA=0x0B1044A4  token=0x6000BA5  System.Void Release()
  RVA=0x0B104948  token=0x6000BA6  System.Void _CreateTickFunction()
  RVA=0x0B1048E0  token=0x6000BA7  System.Void _ClearTickFunction()
  RVA=0x0B104020  token=0x6000BA8  System.Void AddUIImageToManager(Beyond.UI.UIImage image)
  RVA=0x0B104420  token=0x6000BA9  System.Void ReProcessInsertForUIImage(Beyond.UI.UIImage image)
  RVA=0x030942B0  token=0x6000BAA  System.Void RemoveUIImageFromManager(Beyond.UI.UIImage image)
  RVA=0x03093730  token=0x6000BAB  System.Void AddUIImageToManagerOnInstantiate(Beyond.UI.UIImage image)
  RVA=0x0B1040A4  token=0x6000BAC  System.Int32 GetAtlasCount()
  RVA=0x0B104168  token=0x6000BAD  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B1047E0  token=0x6000BAE  System.Void _AddUIImageToManager(Beyond.UI.UIImage image)
  RVA=0x0B1057B8  token=0x6000BAF  System.Void _ReProcessInsertForUIImage(Beyond.UI.UIImage image)
  RVA=0x0B105930  token=0x6000BB0  System.Void _RemoveUIImageFromManager(Beyond.UI.UIImage image)
  RVA=0x0B1044F4  token=0x6000BB1  System.Void _AddUIImageToManagerOnInstantiate(Beyond.UI.UIImage image)
  RVA=0x0B104A70  token=0x6000BB2  System.Boolean _IsRuntimeAtlasCompatible(Beyond.UI.UIImage image, Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCause& failureCause)
  RVA=0x0B104D84  token=0x6000BB3  System.Void _ProcessInsertQueue()
  RVA=0x0B1054F8  token=0x6000BB4  System.Void _ProcessRemoveQueue()
END_CLASS

CLASS: Beyond.UI.UIRedDotAnimation
TYPE:  class
TOKEN: 0x200013D
SIZE:  0x30
EXTENDS: Beyond.Singleton`1
FIELDS:
  private           Beyond.UI.UIGraphicAnimation    m_uiGraphicAnimation  // 0x10
  private           UnityEngine.Material            m_materialInstance  // 0x18
  private           System.Collections.Generic.HashSet<Beyond.UI.UIImage>m_redDotImages  // 0x20
  private           UnityEngine.GameObject          m_root  // 0x28
METHODS:
  RVA=0x04D216F0  token=0x6000BC4  System.Void .ctor()
  RVA=0x04958250  token=0x6000BC5  System.Void Init(UnityEngine.GameObject redDotAnimationRoot)
  RVA=0x0B106180  token=0x6000BC6  System.Void UnInit()
  RVA=0x035766D0  token=0x6000BC7  System.Void AddRedDotImage(Beyond.UI.UIImage img)
  RVA=0x03576170  token=0x6000BC8  System.Void RemoveRedDotImage(Beyond.UI.UIImage img)
  RVA=0x03576770  token=0x6000BC9  System.Void _UpdateRedDotAnimation()
END_CLASS

CLASS: Beyond.UI.UIRedDotAnimationNode
TYPE:  class
TOKEN: 0x200013E
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIImage               m_image  // 0x18
METHODS:
  RVA=0x0459E060  token=0x6000BCA  System.Void Awake()
  RVA=0x03576640  token=0x6000BCB  System.Void OnEnable()
  RVA=0x035761E0  token=0x6000BCC  System.Void OnDisable()
  RVA=0x03695570  token=0x6000BCD  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.WebApplication
TYPE:  static class
TOKEN: 0x200013F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B106B6C  token=0x6000BCE  System.Void Start(System.String url, System.Action onClose)
  RVA=0x0B1069A0  token=0x6000BCF  System.Void StartHGBrowser(System.String url, System.Action onClose)
END_CLASS

CLASS: Beyond.UI.GPUI.ChangeState
TYPE:  sealed struct
TOKEN: 0x2000154
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.ChangeState      Stable  // const
  public    static  Beyond.UI.GPUI.ChangeState      New  // const
  public    static  Beyond.UI.GPUI.ChangeState      ChangeSize  // const
  public    static  Beyond.UI.GPUI.ChangeState      ChangeData  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.RuntimeNodeInfo
TYPE:  sealed struct
TOKEN: 0x2000155
SIZE:  0x40
FIELDS:
  public            Beyond.UI.GPUI.NodeType         nodeType  // 0x10
  public            Beyond.UI.GPUI.SimpleTextAlignmenttextAlignment  // 0x14
  public            System.Int32                    nodeIndex  // 0x18
  public            System.Int32                    childNodeCount  // 0x1c
  public            System.UInt32                   color  // 0x20
  public            UnityEngine.Vector2             position  // 0x24
  public            System.String                   textOverride  // 0x30
  public            System.Boolean                  changed  // 0x38
  public            System.Boolean                  hide  // 0x39
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.RuntimeAnimationData
TYPE:  class
TOKEN: 0x2000156
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Single                   duration  // 0x18
  public            System.Collections.Generic.List<Beyond.UI.GPUI.GPUInstanceData>template  // 0x20
METHODS:
  RVA=0x0363C7C0  token=0x6000C31  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.RuntimePrefabData
TYPE:  class
TOKEN: 0x2000157
SIZE:  0x30
FIELDS:
  public            System.Int32                    templateNodeCount  // 0x10
  public            System.Collections.Generic.List<Beyond.UI.GPUI.RuntimeNodeInfo>nodes  // 0x18
  public            System.Collections.Generic.Dictionary<System.String,System.Int32>nodesNameMap  // 0x20
  public            System.Collections.Generic.List<Beyond.UI.GPUI.RuntimeAnimationData>animations  // 0x28
METHODS:
  RVA=0x045ABD20  token=0x6000C32  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedPrefabData
TYPE:  sealed struct
TOKEN: 0x2000158
SIZE:  0x70
FIELDS:
  public            UnityEngine.Vector4             uv  // 0x10
  public            UnityEngine.Vector4             aniPosScaleMinMax  // 0x20
  public            UnityEngine.Vector4             aniPosAddMinMax  // 0x30
  public            UnityEngine.Vector2             aniURange  // 0x40
  public            System.Single                   aniPosScaleV  // 0x48
  public            System.Single                   aniPosAddV  // 0x4c
  public            System.Single                   aniColorV  // 0x50
  public            System.Single                   aniDurationInv  // 0x54
  public            System.Int32                    materialType  // 0x58
  public            System.Int32                    materialParam1  // 0x5c
  public            System.Single                   aniFillV  // 0x60
  public            System.Byte                     fillType  // 0x64
  public            System.Byte                     fillOriginFlags  // 0x65
  public            System.Byte                     wrapMode  // 0x66
  public            System.Byte                     fillReserved  // 0x67
  public            UnityEngine.Vector2             padding  // 0x68
  public    static  System.Single                   INVALID_ANI_FILL_V  // const
  public    static  System.Byte                     WRAP_MODE_CLAMP  // const
  public    static  System.Byte                     WRAP_MODE_LOOP  // const
  public    static  System.Byte                     FILL_ORIGIN_MASK  // const
  public    static  System.Byte                     FILL_CLOCKWISE_BIT  // const
METHODS:
  RVA=0x0357E0C0  token=0x6000C33  System.Void .ctor(Beyond.UI.GPUI.ExtendedNodeSerializeData data)
  RVA=0x0357E1B0  token=0x6000C34  System.Byte _NormalizeWrapMode(UnityEngine.WrapMode wrapMode)
  RVA=0x0357E1E0  token=0x6000C35  System.Byte _PackFillOriginFlags(System.Int32 fillOrigin, System.Boolean fillClockwise)
  RVA=0x04460600  token=0x6000C36  Beyond.UI.GPUI.GPUIExtendedPrefabData CreateFontNode(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedPrefabDataLite
TYPE:  sealed struct
TOKEN: 0x2000159
SIZE:  0x40
FIELDS:
  public            System.UInt32                   pack0  // 0x10
  public            System.UInt32                   pack1  // 0x14
  public            System.UInt32                   pack2  // 0x18
  public            System.UInt32                   pack3  // 0x1c
  public            System.UInt32                   pack4  // 0x20
  public            System.UInt32                   pack5  // 0x24
  public            System.UInt32                   pack6  // 0x28
  public            System.UInt32                   pack7  // 0x2c
  public            System.UInt32                   pack8  // 0x30
  public            System.UInt32                   pack9  // 0x34
  public            System.UInt32                   pack10  // 0x38
  public            System.UInt32                   padding  // 0x3c
  private   static  System.Int32                    FILL_TYPE_SHIFT  // const
  private   static  System.Int32                    FILL_ORIGIN_SHIFT  // const
  private   static  System.Int32                    FILL_CLOCKWISE_SHIFT  // const
  private   static  System.Int32                    WRAP_MODE_SHIFT  // const
  private   static  System.UInt16                   FILL_META_MASK  // const
METHODS:
  RVA=0x0B0FF380  token=0x6000C37  System.Byte _NormalizeWrapMode(UnityEngine.WrapMode wrapMode)
  RVA=0x0B0FF4FC  token=0x6000C38  System.Void .ctor(Beyond.UI.GPUI.ExtendedNodeSerializeData data, UnityEngine.Vector2 texelSizeInv, UnityEngine.Vector2 aniTexelSizeInv)
  RVA=0x0B0FF3D0  token=0x6000C39  System.Void .ctor(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
  RVA=0x0B0FF28C  token=0x6000C3A  Beyond.UI.GPUI.GPUIExtendedPrefabDataLite CreateFontNode(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedInstanceData
TYPE:  sealed struct
TOKEN: 0x200015A
SIZE:  0x30
FIELDS:
  public            System.Single                   timingOrScaleX  // 0x10
  public            System.UInt32                   colorOrScaleYPacked  // 0x14
  public            UnityEngine.Vector2             position  // 0x18
  public            System.UInt16                   prefabId  // 0x20
  public            System.UInt16                   parentId  // 0x22
  public            System.UInt16                   grandParentId  // 0x24
  public            System.Byte                     matType  // 0x26
  public            System.Byte                     drawType  // 0x27
  public            System.Int32                    auxiliaryData0  // 0x28
  public            System.Int32                    auxiliaryData1  // 0x2c
  public    static  System.UInt16                   INVALID_NODE_ID  // const
  public    static  System.Byte                     INVALID_FILL_OVERRIDE  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIRootTransformState
TYPE:  sealed struct
TOKEN: 0x200015B
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Quaternion          rotation  // 0x1c
  public            UnityEngine.Vector3             scale  // 0x2c
PROPERTIES:
  Default  get=0x03AA9D50
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUISubrootDomainState
TYPE:  sealed struct
TOKEN: 0x200015C
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector2             position  // 0x10
  public            UnityEngine.Vector2             scale  // 0x18
  public            System.Single                   rotationZ  // 0x20
  public            System.Boolean                  visible  // 0x24
PROPERTIES:
  Default  get=0x02FA9C00
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedAnimationState
TYPE:  sealed struct
TOKEN: 0x200015D
SIZE:  0x20
FIELDS:
  public            System.Int32                    animationIndex  // 0x10
  public            System.Single                   animationTime  // 0x14
  public            System.Single                   animationDuration  // 0x18
  public            System.Boolean                  initialized  // 0x1c
PROPERTIES:
  Default  get=0x04CA29C0
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedSubrootInfo
TYPE:  sealed struct
TOKEN: 0x200015E
SIZE:  0x20
FIELDS:
  public            System.String                   subrootName  // 0x10
  public            System.Int32                    anchorNodeIndex  // 0x18
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIResolvedSubrootState
TYPE:  sealed struct
TOKEN: 0x200015F
SIZE:  0x3C
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Quaternion          rotation  // 0x1c
  public            UnityEngine.Vector3             scale  // 0x2c
  public            System.Boolean                  visible  // 0x38
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedTransformUtility
TYPE:  static class
TOKEN: 0x2000160
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03AB7310  token=0x6000C3E  Beyond.UI.GPUI.GPUIResolvedSubrootState ResolveSubroot(Beyond.UI.GPUI.GPUIRootTransformState& root, Beyond.UI.GPUI.GPUISubrootDomainState& subroot)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedRuntimeNodeInfo
TYPE:  sealed struct
TOKEN: 0x2000161
SIZE:  0x68
FIELDS:
  public            Beyond.UI.GPUI.NodeType         nodeType  // 0x10
  public            Beyond.UI.GPUI.SimpleTextAlignmenttextAlignment  // 0x14
  public            System.Int32                    nodeIndex  // 0x18
  public            System.Int32                    childNodeCount  // 0x1c
  public            System.Int32                    subrootIndex  // 0x20
  public            System.UInt32                   color  // 0x24
  public            UnityEngine.Vector2             position  // 0x28
  public            UnityEngine.Vector2             scale  // 0x30
  public            System.Single                   rotationZ  // 0x38
  public            System.Single                   fillAmount  // 0x3c
  public            System.Single                   nodeTextSize  // 0x40
  public            System.Single                   textAutoSizeWidth  // 0x44
  public            System.Single                   textAutoSizeMinScale  // 0x48
  public            System.Int32                    textAutoSizeMaxGlyphCount  // 0x4c
  public            System.Boolean                  nodeTextBold  // 0x50
  public            System.String                   textOverride  // 0x58
  public            System.Boolean                  hide  // 0x60
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedRuntimeAnimationData
TYPE:  class
TOKEN: 0x2000162
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Int32                    subrootIndex  // 0x18
  public            System.Single                   duration  // 0x1c
  public            System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedInstanceData>template  // 0x20
METHODS:
  RVA=0x0463BE70  token=0x6000C3F  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData
TYPE:  class
TOKEN: 0x2000163
SIZE:  0x50
FIELDS:
  public            System.Int32                    templateNodeCount  // 0x10
  public            System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedRuntimeNodeInfo>nodes  // 0x18
  public            System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedSubrootInfo>subroots  // 0x20
  public            System.Collections.Generic.Dictionary<System.String,System.Int32>nodesNameMap  // 0x28
  public            System.Collections.Generic.Dictionary<System.String,System.Int32>subrootNameMap  // 0x30
  public            System.Collections.Generic.Dictionary<System.String,System.Int32>rootAnimationNameMap  // 0x38
  public            System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.Dictionary<System.String,System.Int32>>subrootAnimationNameMaps  // 0x40
  public            System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedRuntimeAnimationData>animations  // 0x48
METHODS:
  RVA=0x045F96C0  token=0x6000C40  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedPackingUtility
TYPE:  static class
TOKEN: 0x2000164
SIZE:  0x10
FIELDS:
  private   static  System.Single                   SNORM16_SCALE  // const
METHODS:
  RVA=0x0B0FF1CC  token=0x6000C41  System.UInt32 PackPrefabAndParent(System.UInt16 prefabId, System.UInt16 parentId)
  RVA=0x0B0FF154  token=0x6000C42  System.UInt32 PackGrandParentMatTypeAndDrawType(System.UInt16 grandParentId, System.Byte matType, System.Byte drawType)
  RVA=0x03AB72A0  token=0x6000C43  System.UInt32 PackRootScaleY(System.Single scaleY)
  RVA=0x0B0FF22C  token=0x6000C44  System.UInt32 PackUInt16Pair(System.UInt16 low, System.UInt16 high)
  RVA=0x0B0FF108  token=0x6000C45  System.Int32 PackFillOverrideByte(System.Byte fillOverride)
  RVA=0x03BB9900  token=0x6000C46  System.Int32 PackTextScale(System.Single scale)
  RVA=0x02F3E2C0  token=0x6000C47  System.Void PackQuaternion(UnityEngine.Quaternion rotation, System.Int32& packedAuxiliary0, System.Int32& packedAuxiliary1)
  RVA=0x02F3E900  token=0x6000C48  System.Int16 _PackSnorm16(System.Single value)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUILayoutInfo
TYPE:  static class
TOKEN: 0x2000165
SIZE:  0x10
FIELDS:
PROPERTIES:
  LegacyPrefabStride  get=0x0B100068
  LegacyPrefabLiteStride  get=0x0B100000
  LegacyInstanceStride  get=0x0B0FFF98
  ExtendedPrefabStride  get=0x0B0FFE78
  ExtendedPrefabLiteStride  get=0x0B0FFE10
  ExtendedInstanceStride  get=0x0B0FFDA8
  IsLegacyLayoutAligned  get=0x0B0FFF3C
  IsExtendedLayoutAligned  get=0x0B0FFEE0
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUPrefabData
TYPE:  sealed struct
TOKEN: 0x2000166
SIZE:  0x60
FIELDS:
  public            UnityEngine.Vector4             uv  // 0x10
  public            UnityEngine.Vector4             aniPosScaleMinMax  // 0x20
  public            UnityEngine.Vector4             aniPosAddMinMax  // 0x30
  public            UnityEngine.Vector2             aniURange  // 0x40
  public            System.Single                   aniPosScaleV  // 0x48
  public            System.Single                   aniPosAddV  // 0x4c
  public            System.Single                   aniColorV  // 0x50
  public            System.Single                   aniDurationInv  // 0x54
  public            System.Int32                    materialType  // 0x58
  public            System.UInt32                   materialParam1  // 0x5c
METHODS:
  RVA=0x040FB5C0  token=0x6000C51  System.Void .ctor(Beyond.UI.GPUI.NodeSerializeData data)
  RVA=0x043EA580  token=0x6000C52  System.Void .ctor(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale)
  RVA=0x043EA4E0  token=0x6000C53  Beyond.UI.GPUI.GPUPrefabData CreateFontNode(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUPrefabDataLite
TYPE:  sealed struct
TOKEN: 0x2000167
SIZE:  0x40
FIELDS:
  public            System.Int16                    uvX  // 0x10
  public            System.Int16                    uvY  // 0x12
  public            System.Int16                    uvZ  // 0x14
  public            System.Int16                    uvW  // 0x16
  public            System.UInt16                   aniPosScaleMinX  // 0x18
  public            System.UInt16                   aniPosScaleMinY  // 0x1a
  public            System.UInt16                   aniPosScaleMaxX  // 0x1c
  public            System.UInt16                   aniPosScaleMaxY  // 0x1e
  public            System.UInt16                   aniPosAddMinX  // 0x20
  public            System.UInt16                   aniPosAddMinY  // 0x22
  public            System.UInt16                   aniPosAddMaxX  // 0x24
  public            System.UInt16                   aniPosAddMaxY  // 0x26
  public            System.Int16                    aniURangeX  // 0x28
  public            System.Int16                    aniURangeY  // 0x2a
  public            System.Int16                    aniPosScaleV  // 0x2c
  public            System.Int16                    aniPosAddV  // 0x2e
  public            System.Int16                    aniColorV  // 0x30
  public            System.Int16                    aniDuration  // 0x32
  public            System.UInt16                   materialType  // 0x34
  public            System.UInt16                   materialParam1  // 0x36
  public            System.UInt32                   padding  // 0x38
  public            System.UInt32                   padding2  // 0x3c
METHODS:
  RVA=0x0B1001C4  token=0x6000C54  System.Void .ctor(Beyond.UI.GPUI.NodeSerializeData data, UnityEngine.Vector2 texelSizeInv, UnityEngine.Vector2 aniTexelSizeInv)
  RVA=0x0B10041C  token=0x6000C55  System.Void .ctor(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
  RVA=0x0B1000D0  token=0x6000C56  Beyond.UI.GPUI.GPUPrefabDataLite CreateFontNode(UnityEngine.Vector4 uv, UnityEngine.Vector4 scale, UnityEngine.Vector2 texelSizeInv)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUInstanceData
TYPE:  sealed struct
TOKEN: 0x2000168
SIZE:  0x30
FIELDS:
  public            System.Single                   animeTime  // 0x10
  public            System.UInt32                   color  // 0x14
  public            UnityEngine.Vector2             pos  // 0x18
  public            System.UInt16                   prefabId  // 0x20
  public            System.UInt16                   parentId  // 0x22
  public            System.UInt16                   grandParentId  // 0x24
  public            System.Byte                     matType  // 0x26
  public            System.Byte                     drawType  // 0x27
  public            System.UInt32                   padding0  // 0x28
  public            System.UInt32                   padding1  // 0x2c
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUILayoutType
TYPE:  sealed struct
TOKEN: 0x2000169
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.GPUILayoutType   Legacy  // const
  public    static  Beyond.UI.GPUI.GPUILayoutType   Extended  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIImageFillType
TYPE:  sealed struct
TOKEN: 0x200016A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.GPUIImageFillTypeNone  // const
  public    static  Beyond.UI.GPUI.GPUIImageFillTypeHorizontal  // const
  public    static  Beyond.UI.GPUI.GPUIImageFillTypeVertical  // const
  public    static  Beyond.UI.GPUI.GPUIImageFillTypeRadial360  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.NodeType
TYPE:  sealed struct
TOKEN: 0x200016B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.NodeType         Root  // const
  public    static  Beyond.UI.GPUI.NodeType         Image  // const
  public    static  Beyond.UI.GPUI.NodeType         Text  // const
  public    static  Beyond.UI.GPUI.NodeType         Fill  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.SimpleTextAlignment
TYPE:  sealed struct
TOKEN: 0x200016C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUI.SimpleTextAlignmentLeft  // const
  public    static  Beyond.UI.GPUI.SimpleTextAlignmentCenter  // const
  public    static  Beyond.UI.GPUI.SimpleTextAlignmentRight  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.GPUI.NodeSerializeData
TYPE:  class
TOKEN: 0x200016D
SIZE:  0x60
FIELDS:
  public            UnityEngine.Vector4             uv  // 0x10
  public            UnityEngine.Vector4             aniPosScaleMinMax  // 0x20
  public            UnityEngine.Vector4             aniPosAddMinMax  // 0x30
  public            UnityEngine.Vector2             aniURange  // 0x40
  public            System.Single                   aniPosScaleV  // 0x48
  public            System.Single                   aniPosAddV  // 0x4c
  public            System.Single                   aniColorV  // 0x50
  public            System.Single                   aniDurationInv  // 0x54
  public            System.Int32                    materialType  // 0x58
  public            System.UInt32                   materialParam1  // 0x5c
METHODS:
  RVA=0x041E1670  token=0x6000C57  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.ExtendedNodeSerializeData
TYPE:  class
TOKEN: 0x200016E
SIZE:  0x78
FIELDS:
  public            UnityEngine.Vector4             uv  // 0x10
  public            UnityEngine.Vector4             aniPosScaleMinMax  // 0x20
  public            UnityEngine.Vector4             aniPosAddMinMax  // 0x30
  public            System.Single                   aniPosScaleV  // 0x40
  public            System.Single                   aniPosAddV  // 0x44
  public            System.Single                   aniColorV  // 0x48
  public            System.Single                   aniFillV  // 0x4c
  public            UnityEngine.Vector2             aniURange  // 0x50
  public            System.Single                   aniDurationInv  // 0x58
  public            System.Int32                    materialType  // 0x5c
  public            System.UInt32                   materialParam1  // 0x60
  public            UnityEngine.WrapMode            wrapMode  // 0x64
  public            Beyond.UI.GPUI.GPUIImageFillTypefillType  // 0x68
  public            System.Int32                    fillOrigin  // 0x6c
  public            System.Boolean                  fillClockwise  // 0x70
METHODS:
  RVA=0x041E1670  token=0x6000C58  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.NodeMetadata
TYPE:  class
TOKEN: 0x200016F
SIZE:  0x58
FIELDS:
  public            System.String                   nodeName  // 0x10
  public            Beyond.UI.GPUI.NodeType         nodeType  // 0x18
  public            System.String                   nodeTextId  // 0x20
  public            System.String                   nodeText  // 0x28
  public            Beyond.UI.GPUI.SimpleTextAlignmenttextAlignment  // 0x30
  public            System.Single                   nodeTextSize  // 0x34
  public            System.Boolean                  nodeTextBold  // 0x38
  public            System.Int32                    textAutoSizeMaxGlyphCount  // 0x3c
  public            System.Single                   textAutoSizeWidth  // 0x40
  public            System.Single                   textAutoSizeMinScale  // 0x44
  public            System.String                   subrootName  // 0x48
  public            System.Int32                    subrootIndex  // 0x50
METHODS:
  RVA=0x02B92810  token=0x6000C59  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.SubrootSerializeData
TYPE:  class
TOKEN: 0x2000170
SIZE:  0x20
FIELDS:
  public            System.String                   subrootName  // 0x10
  public            System.Int32                    anchorNodeIndex  // 0x18
METHODS:
  RVA=0x04D87910  token=0x6000C5A  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.AnimationSerializeData
TYPE:  class
TOKEN: 0x2000171
SIZE:  0x28
FIELDS:
  public            System.String                   animationName  // 0x10
  public            System.Single                   animationTime  // 0x18
  public            System.Int32                    totalFrames  // 0x1c
  public            UnityEngine.WrapMode            wrapMode  // 0x20
  public            System.Int32                    renderNodesOffset  // 0x24
METHODS:
  RVA=0x041E1670  token=0x6000C5B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.ExtendedAnimationSerializeData
TYPE:  class
TOKEN: 0x2000172
SIZE:  0x40
FIELDS:
  public            System.String                   animationName  // 0x10
  public            System.String                   subrootName  // 0x18
  public            System.Int32                    subrootIndex  // 0x20
  public            System.Single                   animationTime  // 0x24
  public            System.Int32                    totalFrames  // 0x28
  public            UnityEngine.WrapMode            wrapMode  // 0x2c
  public            System.Int32                    renderNodesOffset  // 0x30
  public            System.Collections.Generic.List<System.Int32>affectedNodeIndices  // 0x38
METHODS:
  RVA=0x043E7750  token=0x6000C5C  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.PrefabSerializeData
TYPE:  class
TOKEN: 0x2000173
SIZE:  0x38
FIELDS:
  public            System.String                   prefabName  // 0x10
  public            System.Int32                    nodeCount  // 0x18
  public            System.Boolean                  animationOnly  // 0x1c
  public            System.Collections.Generic.List<Beyond.UI.GPUI.NodeMetadata>nodeMetas  // 0x20
  public            System.Collections.Generic.List<Beyond.UI.GPUI.NodeSerializeData>renderNodes  // 0x28
  public            System.Collections.Generic.List<Beyond.UI.GPUI.AnimationSerializeData>animations  // 0x30
METHODS:
  RVA=0x043DABD0  token=0x6000C5D  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.ExtendedPrefabSerializeData
TYPE:  class
TOKEN: 0x2000174
SIZE:  0x40
FIELDS:
  public            System.String                   prefabName  // 0x10
  public            System.Int32                    nodeCount  // 0x18
  public            System.Boolean                  animationOnly  // 0x1c
  public            System.Collections.Generic.List<Beyond.UI.GPUI.SubrootSerializeData>subroots  // 0x20
  public            System.Collections.Generic.List<Beyond.UI.GPUI.NodeMetadata>nodeMetas  // 0x28
  public            System.Collections.Generic.List<Beyond.UI.GPUI.ExtendedNodeSerializeData>renderNodes  // 0x30
  public            System.Collections.Generic.List<Beyond.UI.GPUI.ExtendedAnimationSerializeData>animations  // 0x38
METHODS:
  RVA=0x043DA8F0  token=0x6000C5E  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.PrefabGroupSerializeData
TYPE:  class
TOKEN: 0x2000175
SIZE:  0x40
IMPLEMENTS: Beyond.MemoryPack.IMemoryPackSerialize
FIELDS:
  public            Beyond.UI.GPUI.GPUILayoutType   layoutType  // 0x10
  public            Beyond.Resource.StringPathHash  spriteTexturePath  // 0x18
  public            Beyond.Resource.StringPathHash  vatTexturePath  // 0x20
  public            System.Collections.Generic.List<Beyond.UI.GPUI.PrefabSerializeData>prefabs  // 0x28
  public            System.Collections.Generic.List<System.String>textIds  // 0x30
  public            System.String                   importantChars  // 0x38
METHODS:
  RVA=0x049935D0  token=0x6000C5F  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.ExtendedPrefabGroupSerializeData
TYPE:  class
TOKEN: 0x2000176
SIZE:  0x68
IMPLEMENTS: Beyond.MemoryPack.IMemoryPackSerialize
FIELDS:
  public            Beyond.UI.GPUI.GPUILayoutType   layoutType  // 0x10
  public            Beyond.Resource.StringPathHash  spriteTexturePath  // 0x18
  public            Beyond.Resource.StringPathHash  vatTexturePath  // 0x20
  public            System.Boolean                  hasTextNodes  // 0x28
  public            System.Int32                    prefabBufferSize  // 0x2c
  public            System.Int32                    fontPrefabBufferSize  // 0x30
  public            System.Boolean                  simpleInstanceBuffer  // 0x34
  public            System.Boolean                  enableDynamicSpriteAtlas  // 0x35
  public            System.Int32                    dynamicSpriteAtlasX  // 0x38
  public            System.Int32                    dynamicSpriteAtlasY  // 0x3c
  public            System.Int32                    dynamicSpriteAtlasW  // 0x40
  public            System.Int32                    dynamicSpriteAtlasH  // 0x44
  public            System.Int32                    dynamicPrefabBufferCapacity  // 0x48
  public            System.Collections.Generic.List<Beyond.UI.GPUI.ExtendedPrefabSerializeData>prefabs  // 0x50
  public            System.Collections.Generic.List<System.String>textIds  // 0x58
  public            System.String                   importantChars  // 0x60
METHODS:
  RVA=0x0472E7D0  token=0x6000C60  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.BaseRuntimeInstance`1
TYPE:  abstract class
TOKEN: 0x2000177
FIELDS:
  public            System.Int32                    version  // 0x0
  public            Beyond.UI.GPUI.ChangeState      state  // 0x0
  public            System.Int32                    length  // 0x0
  public            System.Int32                    sortingOrder  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x6000C61  System.Void Recycle()
  RVA=-1  // abstract  token=0x6000C62  System.Void DoUpdate(Unity.Collections.NativeArray<TData>& buffer, System.Int32 startIndexInBuffer, System.Boolean full)
  RVA=-1  // not resolved  token=0x6000C63  System.Void MarkChangeData()
  RVA=-1  // not resolved  token=0x6000C64  System.Void MarkChangeSize()
  RVA=-1  // not resolved  token=0x6000C65  System.Void MarkStable()
  RVA=-1  // not resolved  token=0x6000C66  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.CodePoint
TYPE:  class
TOKEN: 0x2000178
SIZE:  0x10
FIELDS:
  public    static  System.UInt32                   HIGH_SURROGATE_START  // const
  public    static  System.UInt32                   HIGH_SURROGATE_END  // const
  public    static  System.UInt32                   LOW_SURROGATE_START  // const
  public    static  System.UInt32                   LOW_SURROGATE_END  // const
  public    static  System.UInt32                   UNICODE_PLANE01_START  // const
METHODS:
  RVA=0x0B0FE390  token=0x6000C67  System.UInt32 ConvertToUTF32(System.UInt32 highSurrogate, System.UInt32 lowSurrogate)
  RVA=0x041E1670  token=0x6000C68  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.BaseTextSystem`1
TYPE:  abstract class
TOKEN: 0x2000179
FIELDS:
  protected static  System.Single                   DEFAULT_WHITESPACE_SPACING  // const
  protected         TMPro.TMP_FontAsset             m_fontAsset  // 0x0
  protected         UnityEngine.Material            m_tmpMaterial  // 0x0
  protected         Beyond.UI.GPUI.SimpleBufferManager<T>m_prefabBufferManager  // 0x0
  protected         System.Single                   m_padding  // 0x0
  protected         UnityEngine.Vector2             m_texelSizeInv  // 0x0
  protected         System.Boolean                  m_isThaiMode  // 0x0
  protected         System.Int32                    m_prefabDestIndex  // 0x0
  protected         System.Int32                    m_fontBufferCount  // 0x0
  protected readonly System.Int32                    m_fontBufferCapacity  // 0x0
  protected readonly System.Func<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector2,T>m_prefabCreatorFunc  // 0x0
  protected readonly System.Collections.Generic.List<T>m_dynamicBufferCache  // 0x0
  protected readonly System.Collections.Generic.Dictionary<System.UInt32,System.Int32>m_charCodeToIndexMap  // 0x0
  protected readonly System.Collections.Generic.List<Beyond.UI.GPUI.BaseTextSystem.CharOffsetData<T>>m_tempOffsetCache  // 0x0
  protected readonly System.Collections.Generic.Dictionary<System.UInt32,Beyond.UI.GPUI.BaseTextSystem.CharOffsetData<T>>m_charaOffsetCache  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C69  System.Void .ctor(System.Int32 fontBufferCapacity, System.Func<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector2,T> prefabCreatorFunc)
  RVA=-1  // not resolved  token=0x6000C6A  System.Void Init(TMPro.TMP_FontAsset fontAsset, UnityEngine.Material tmpMaterial, Beyond.UI.GPUI.SimpleBufferManager<T> simpleBufferManager, System.Boolean isThaiMode)
  RVA=-1  // not resolved  token=0x6000C6B  System.Void RegisterCharacterToPrefabBuffer(System.String text)
  RVA=-1  // not resolved  token=0x6000C6C  System.Int32 GetNodeCount(System.String text)
  RVA=-1  // not resolved  token=0x6000C6D  System.Void UploadBufferIfNeeded()
  RVA=-1  // not resolved  token=0x6000C6E  System.Single CalculateAutoSizeScale(System.Single totalWidth, System.Single autoSizeWidth, System.Single nodeTextSize, System.Single autoSizeMinScale)
  RVA=-1  // not resolved  token=0x6000C6F  System.Void DoTextAlignAndDynamicBuffer(System.String text, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single& currentX)
  RVA=-1  // not resolved  token=0x6000C70  System.Void DoTextAlignAndDynamicBuffer(System.String text, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single& currentX, System.Single& totalWidth)
  RVA=-1  // not resolved  token=0x6000C71  System.Int32 PutOneCharacterToPrefabBufferDynamic(System.UInt32 c)
  RVA=-1  // not resolved  token=0x6000C72  System.Void _LoadAllCharacterToPrefabBuffer(System.String text)
  RVA=-1  // not resolved  token=0x6000C73  System.Void _InitPrefabDataByTMPCharacter(TMPro.TMP_Character character, T& prefabData)
  RVA=-1  // not resolved  token=0x6000C74  System.Void OnFontBufferCapacityExceeded()
  RVA=-1  // not resolved  token=0x6000C75  System.Void OnCharacterMissing(System.UInt32 c)
  RVA=-1  // not resolved  token=0x6000C76  System.Boolean IsUpVowel(System.Char c)
  RVA=-1  // not resolved  token=0x6000C77  System.Boolean IsLowVowel(System.Char c)
  RVA=-1  // not resolved  token=0x6000C78  System.Boolean IsToneMark(System.Char c)
  RVA=-1  // not resolved  token=0x6000C79  System.Boolean IsAscenderThai(System.Char c)
END_CLASS

CLASS: Beyond.UI.GPUI.ExInstanceBufferManager`2
TYPE:  sealed class
TOKEN: 0x200017B
EXTENDS: Beyond.UI.GPUI.InstanceBufferManagerBase`2
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  MARKER_UPDATE_PERF  // static @ 0x0
  private           Unity.Collections.NativeArray<UnityEngine.CanvasRenderer.InstanceRange>m_gpuGroups  // 0x0
  private           System.Collections.Generic.List<TInstance>m_runtimeInstanceView  // 0x0
  private           System.Collections.Generic.List<Beyond.UI.GPUI.ExInstanceBufferManager.BufferInternalNode<TInstance,TData>>m_bufferInternalNodes  // 0x0
  private           System.Collections.Generic.List<Beyond.UI.GPUI.ExInstanceBufferManager.BufferInternalNode<TInstance,TData>>m_pendingNodes  // 0x0
  private           System.Collections.Generic.List<Beyond.UI.GPUI.ExInstanceBufferManager.BufferInternalNode<TInstance,TData>>m_addNodes  // 0x0
  private           System.Collections.Generic.HashSet<Beyond.UI.GPUI.GPUIHandle>m_removeNodes  // 0x0
  private           System.Boolean                  m_hasAnyChanges  // 0x0
  private           System.Boolean                  m_updateBufferOnly  // 0x0
  private           System.Int32                    m_currentStartIndex  // 0x0
  private           System.Int32                    m_currentLength  // 0x0
  private           System.Int32                    m_capacity  // 0x0
  private   readonly System.Int32                    m_maxCapacity  // 0x0
  private   readonly System.Int32                    m_gpuGroupCapacity  // 0x0
  private           System.Int32                    m_groupCount  // 0x0
  private   readonly System.Boolean                  m_noSortingOrder  // 0x0
PROPERTIES:
  startIndex  get=-1  // not resolved
  nodeCount  get=-1  // not resolved
  capacity  get=-1  // not resolved
  gpuGroups  get=-1  // not resolved
  gpuGroupCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C7F  System.Void .ctor(System.Int32 initCapacity, System.Int32 maxCapacity, System.Int32 gpuGroupCapacity, System.Boolean noSortingOrder)
  RVA=-1  // not resolved  token=0x6000C80  System.Void BindInstanceList(System.Collections.Generic.List<TInstance> runtimeInstanceView)
  RVA=-1  // not resolved  token=0x6000C81  System.Void MarkNeedUpdate()
  RVA=-1  // not resolved  token=0x6000C82  System.Void _FillInstanceQueue(Unity.Collections.NativeArray<TData>& oldBuffer, System.Int32& newTotalLength, System.Int32& newTotalLengthWithEmptySlot, System.Int32& fullUpdateIndex)
  RVA=-1  // not resolved  token=0x6000C83  System.Void DoUpdate(UnityEngine.CanvasRenderer canvasRenderer)
  RVA=-1  // not resolved  token=0x6000C84  System.Void _NewGpuGroup(System.Int32 start, System.Int32 end)
  RVA=-1  // not resolved  token=0x6000C85  System.Void Add(Beyond.UI.GPUI.GPUIHandle instance)
  RVA=-1  // not resolved  token=0x6000C86  System.Void Remove(Beyond.UI.GPUI.GPUIHandle instance)
  RVA=-1  // not resolved  token=0x6000C87  System.Void _AllocBuffer(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000C88  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000C89  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedDynamicAtlas
TYPE:  sealed class
TOKEN: 0x200017D
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static  System.Int32                    DEFAULT_PADDING  // const
  public    static  System.Int32                    IMAGE_USING_ATLAS_MAX_SIZE  // const
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.SpriteRecord>m_spriteRecords  // 0x10
  private           UnityEngine.UICustomAtlasHandle m_atlasHandle  // 0x18
  private           UnityEngine.Texture2D           m_runtimeTexture  // 0x20
  private           UnityEngine.RectInt             m_dynamicRect  // 0x28
  private           System.Boolean                  m_initialized  // 0x38
PROPERTIES:
  runtimeTexture  get=0x033C42C0
  dynamicAtlasRect  get=0x0B0FECF0
  isInitialized  get=0x03944C90
METHODS:
  RVA=0x03944CC0  token=0x6000C8D  System.Void .ctor(UnityEngine.Texture staticTexture, UnityEngine.RectInt dynamicRect, System.Int32 padding)
  RVA=0x03944D70  token=0x6000C8E  System.Void Initialize(UnityEngine.Texture staticTexture, UnityEngine.RectInt dynamicRect)
  RVA=0x03944E00  token=0x6000C8F  System.Void Initialize(UnityEngine.Texture2D staticTexture, UnityEngine.RectInt dynamicRect)
  RVA=0x0357B080  token=0x6000C90  System.Boolean Acquire(UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle& handle, UnityEngine.Vector4& uv)
  RVA=0x0B0FEB5C  token=0x6000C91  System.Boolean Release(Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle handle)
  RVA=0x0357A870  token=0x6000C92  System.Boolean Release(Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle handle, System.Boolean& fullyReleased)
  RVA=0x03944F10  token=0x6000C93  System.Void Dispose()
  RVA=0x039451A0  token=0x6000C94  UnityEngine.UICustomAtlasCopyStatus GetAtlasStatus()
  RVA=0x0357BB90  token=0x6000C95  System.Boolean _TryValidateSprite(UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCause& failureCause)
  RVA=0x03945380  token=0x6000C96  System.Boolean _IsValidStaticTexture(UnityEngine.Texture2D staticTexture)
  RVA=0x03945280  token=0x6000C97  System.Boolean _IsValidDynamicAtlasRect(UnityEngine.Texture2D staticTexture, UnityEngine.RectInt dynamicRect)
  RVA=0x03944FC0  token=0x6000C98  System.Boolean _IsAtlasCreationValid(UnityEngine.UICustomAtlasCopyStatus status)
  RVA=0x0357BD90  token=0x6000C99  UnityEngine.Vector2Int _GetSpriteContentSize(UnityEngine.Sprite sprite)
  RVA=0x0357B330  token=0x6000C9A  System.Int32 _GetSpriteInstanceId(UnityEngine.Sprite sprite)
  RVA=0x0B0FEBE0  token=0x6000C9B  System.Void _LogFailure(UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCause failureCause)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager
TYPE:  sealed class
TOKEN: 0x2000181
SIZE:  0x68
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.Boolean                  m_liteMode  // 0x10
  private   readonly Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabData>m_prefabBufferManager  // 0x18
  private   readonly Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabDataLite>m_litePrefabBufferManager  // 0x20
  private   readonly Beyond.UI.GPUI.GPUIExtendedPrefabData[]m_prefabMirror  // 0x28
  private   readonly Beyond.UI.GPUI.GPUIExtendedPrefabDataLite[]m_litePrefabMirror  // 0x30
  private   readonly UnityEngine.Vector2             m_spriteTexelSizeInv  // 0x38
  private   readonly System.Collections.Generic.Stack<System.Int32>m_freeSlots  // 0x40
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey,Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeEntry>m_entries  // 0x48
  private   readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4>m_spriteUvMap  // 0x50
  private   readonly System.Collections.Generic.HashSet<System.Int32>m_dirtySlots  // 0x58
  private           System.Boolean                  m_disposed  // 0x60
METHODS:
  RVA=0x03945680  token=0x6000C9F  System.Void .ctor(Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabData> prefabBufferManager, Beyond.UI.GPUI.GPUIExtendedPrefabData[] prefabMirror, System.Int32 capacity)
  RVA=0x0B0FEF40  token=0x6000CA0  System.Void .ctor(Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabDataLite> prefabBufferManager, Beyond.UI.GPUI.GPUIExtendedPrefabDataLite[] prefabMirror, UnityEngine.Vector2 spriteTexelSizeInv, System.Int32 capacity)
  RVA=0x0B0FED58  token=0x6000CA1  System.Void Dispose()
  RVA=0x0357B370  token=0x6000CA2  System.Void RegisterSpriteUV(UnityEngine.Sprite sprite, UnityEngine.Vector4 spriteUv)
  RVA=0x0357E230  token=0x6000CA3  System.Void UnregisterSpriteUV(UnityEngine.Sprite sprite)
  RVA=0x0357C320  token=0x6000CA4  System.Boolean TryAcquire(System.Int32 basePrefabOffset, UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& key, System.UInt16& dynamicPrefabId)
  RVA=0x0357AA20  token=0x6000CA5  System.Void Release(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey key)
  RVA=0x033C43E0  token=0x6000CA6  System.Void FlushIfNeeded()
  RVA=-1  // generic def  token=0x6000CA7  System.Void _ReserveSlots(Beyond.UI.GPUI.SimpleBufferManager<T> bufferManager, System.Int32 capacity)
  RVA=0x0B0FEE3C  token=0x6000CA8  System.Void _SetLiteUV(Beyond.UI.GPUI.GPUIExtendedPrefabDataLite& data, UnityEngine.Vector4 spriteUv, UnityEngine.Vector2 spriteTexelSizeInv)
  RVA=0x0357B3F0  token=0x6000CA9  System.Int32 _GetSpriteInstanceId(UnityEngine.Sprite sprite)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedDynamicSpriteOverrideManager
TYPE:  sealed class
TOKEN: 0x2000184
SIZE:  0x20
FIELDS:
  private   readonly Beyond.UI.GPUI.GPUIExtendedDynamicAtlasm_dynamicAtlas  // 0x10
  private   readonly Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManagerm_dynamicPrefabNodeManager  // 0x18
METHODS:
  RVA=0x032AF420  token=0x6000CB2  System.Void .ctor(Beyond.UI.GPUI.GPUIExtendedDynamicAtlas dynamicAtlas, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager dynamicPrefabNodeManager)
  RVA=0x0357AF90  token=0x6000CB3  System.Boolean TryAcquireDynamicSprite(UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle& spriteHandle)
  RVA=0x0357C290  token=0x6000CB4  System.Boolean TryAcquireDynamicPrefabNode(System.Int32 basePrefabOffset, UnityEngine.Sprite sprite, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& key, System.UInt16& dynamicPrefabId)
  RVA=0x0357C220  token=0x6000CB5  System.Boolean TryAcquireDynamicPrefabNode(System.Int32 basePrefabOffset, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle spriteHandle, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& key, System.UInt16& dynamicPrefabId)
  RVA=0x0357C670  token=0x6000CB6  System.Boolean MatchesDynamicPrefabNode(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey key, System.Int32 basePrefabOffset, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle spriteHandle)
  RVA=0x0357A9C0  token=0x6000CB7  System.Void ReleaseDynamicPrefabNode(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey key)
  RVA=0x0357DDB0  token=0x6000CB8  System.Void ReleaseDynamicSprite(Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle spriteHandle)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedRuntimeInstance
TYPE:  sealed class
TOKEN: 0x2000185
SIZE:  0xB0
EXTENDS: Beyond.UI.GPUI.BaseRuntimeInstance`1
FIELDS:
  public    static  System.Int32                    ROOT_NODE_INDEX  // const
  public    static  System.UInt32                   INVALID_NODE_ID  // const
  private           Beyond.UI.GPUI.GPUIExtendedRuntimePrefabDatam_prefab  // 0x20
  private           Beyond.UI.GPUI.GPUIExtendedRuntimeNodeInfo[]m_nodeInfo  // 0x28
  private           Beyond.UI.GPUI.GPUISubrootDomainState[]m_subroots  // 0x30
  private           Beyond.UI.GPUI.GPUIExtendedAnimationStatem_rootAnimationState  // 0x38
  private           Beyond.UI.GPUI.GPUIExtendedAnimationState[]m_subrootAnimationStates  // 0x48
  private           System.Int32[]                  m_subrootProxyNodeIndices  // 0x50
  private           System.Int32                    m_subrootProxyNodeCount  // 0x58
  private           Beyond.UI.GPUI.IExtendedTextSystemm_textSystem  // 0x60
  private           Beyond.UI.GPUI.GPUIExtendedDynamicSpriteOverrideManagerm_dynamicSpriteOverrideManager  // 0x68
  private           Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState[]m_spriteOverrides  // 0x70
  private           System.Int32                    m_nodeInfoCount  // 0x78
  private           System.Single                   m_animationDuration  // 0x7c
  private           Beyond.UI.GPUI.GPUIRootTransformStatem_rootTransform  // 0x80
  private           System.Boolean                  m_needsSpriteOverrideRetry  // 0xa8
PROPERTIES:
  Prefab  get=0x02F47CC0
  AnimationDuration  get=0x0B0FFD58
METHODS:
  RVA=0x03AA9900  token=0x6000CBB  System.Void Create(Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData prefab, Beyond.UI.GPUI.IExtendedTextSystem textSystem, Beyond.UI.GPUI.GPUIExtendedDynamicSpriteOverrideManager dynamicSpriteOverrideManager)
  RVA=0x0357A980  token=0x6000CBC  System.Void Recycle()
  RVA=0x02FA80A0  token=0x6000CBD  System.Void DoUpdate(Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& buffer, System.Int32 startIndexInBuffer, System.Boolean full)
  RVA=0x02F47DF0  token=0x6000CBE  System.Void SetRootPosition(UnityEngine.Vector3 position)
  RVA=0x02F47EF0  token=0x6000CBF  System.Void SetRootScale(UnityEngine.Vector2 scale)
  RVA=0x042E49B0  token=0x6000CC0  System.Void SetRootRotation(UnityEngine.Quaternion rotation)
  RVA=0x03AA2720  token=0x6000CC1  System.Void StartAnimation(System.Single animationTime, System.Int32 animationIndex)
  RVA=0x0B0FFA38  token=0x6000CC2  System.Boolean StartSubrootAnimation(System.Int32 subrootIndex, System.Single animationTime, System.Int32 animationIndex)
  RVA=0x02F48230  token=0x6000CC3  System.Void SetNodeVisibility(System.Int32 index, System.Boolean visible)
  RVA=0x0363C2A0  token=0x6000CC4  System.Void SetNodeColor(System.Int32 index, UnityEngine.Color color)
  RVA=0x033C1660  token=0x6000CC5  System.Void SetNodeFillAmount(System.Int32 index, System.Single fillAmount)
  RVA=0x0B0FF93C  token=0x6000CC6  System.Void SetSubrootState(System.Int32 subrootIndex, UnityEngine.Vector2 position, UnityEngine.Vector2 scale, System.Single rotationZ)
  RVA=0x03AB6E80  token=0x6000CC7  System.Void SetSubrootVisibility(System.Int32 subrootIndex, System.Boolean visible)
  RVA=0x03848AD0  token=0x6000CC8  System.Void SetText(System.Int32 index, System.String text)
  RVA=0x0357BE00  token=0x6000CC9  System.Boolean SetSpriteOverride(System.Int32 index, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle spriteHandle)
  RVA=0x0357AE10  token=0x6000CCA  System.Boolean ClearSpriteOverride(System.Int32 index)
  RVA=0x0B0FF87C  token=0x6000CCB  System.Void ClearDynamicSpriteOverrides()
  RVA=0x0B0FFB7C  token=0x6000CCC  System.Void UpdateDynamicSpriteContext(Beyond.UI.GPUI.GPUIExtendedDynamicSpriteOverrideManager dynamicSpriteOverrideManager)
  RVA=0x03AB7110  token=0x6000CCD  System.Void _WriteRootNode(Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& buffer, System.Int32 startIndexInBuffer, System.Int32& indexInBuffer, Beyond.UI.GPUI.GPUIExtendedInstanceData templateNode)
  RVA=0x03AB6F00  token=0x6000CCE  System.Void _WriteSubrootProxyNodes(Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& buffer, System.Int32 startIndexInBuffer, System.Int32& indexInBuffer)
  RVA=0x03AA9DD0  token=0x6000CCF  System.Void _InitializeAnimationStates()
  RVA=0x02FA96E0  token=0x6000CD0  Beyond.UI.GPUI.GPUIExtendedAnimationState _GetAnimationStateForNode(Beyond.UI.GPUI.GPUIExtendedRuntimeNodeInfo& node)
  RVA=0x0331E560  token=0x6000CD1  System.Int32 _FindPreferredRootAnimationIndex()
  RVA=0x02FA8BD0  token=0x6000CD2  Beyond.UI.GPUI.GPUIExtendedRuntimeAnimationData _GetAnimationData(System.Int32 animationIndex)
  RVA=0x0443E790  token=0x6000CD3  System.Int32 _FindFirstAnimationIndex(System.Int32 subrootIndex)
  RVA=0x045B3EA0  token=0x6000CD4  System.Int32 _FindDefaultSubrootAnimationIndex(System.Int32 subrootIndex)
  RVA=0x02FA98E0  token=0x6000CD5  System.Void _ApplySpriteOverride(System.Int32 nodeIndex, Beyond.UI.GPUI.GPUIExtendedInstanceData& templateNode)
  RVA=0x0357C0B0  token=0x6000CD6  System.Boolean _TryPrimeSpriteOverride(System.Int32 index, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& overrideState)
  RVA=0x0B0FFBE0  token=0x6000CD7  System.Boolean _TryPrimeSpriteOverrideWithReleasedSlot(System.Int32 index, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& previousState, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& nextState)
  RVA=0x0357DBB0  token=0x6000CD8  System.Boolean _TryResolveCurrentBasePrefabOffset(System.Int32 index, System.Int32& basePrefabOffset)
  RVA=0x0357C000  token=0x6000CD9  System.Void _ReleaseSpriteOverrideResources(Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& overrideState)
  RVA=0x0357AEB0  token=0x6000CDA  System.Void _ClearSpriteOverride(System.Int32 index)
  RVA=0x0357AE60  token=0x6000CDB  System.Void _ClearAllSpriteOverrides()
  RVA=0x02FA7720  token=0x6000CDC  System.UInt32 _UnpackPrefabId(System.UInt32 packedPrefabParent)
  RVA=0x0357DD40  token=0x6000CDD  System.Boolean _HasActivePrefabNode(Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& overrideState)
  RVA=0x02FA9840  token=0x6000CDE  System.Byte _PackFillOverride(System.Single fillAmount)
  RVA=0x04CB2850  token=0x6000CDF  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedSystem
TYPE:  class
TOKEN: 0x2000187
SIZE:  0x220
EXTENDS: Beyond.UI.GPUI.GPUISharedGraphicBase
FIELDS:
  private   static  System.String                   DEFAULT_SHADER_NAME  // const
  private   static  System.Int32                    MAX_PREFAB_BUFFER_SIZE  // const
  private   static  System.Int32                    SIMPLE_INSTANCE_INIT_CAPACITY  // const
  public            System.Int32                    sourceCNFontId  // 0x150
  private           TMPro.TMP_FontAsset             m_i18NFontAsset  // 0x158
  public            System.Boolean                  noSortingOrder  // 0x160
  private           UnityEngine.Texture             m_vatTexture  // 0x168
  private           UnityEngine.Texture             m_spriteTexture  // 0x170
  private           Beyond.Resource.FAssetProxyHandlem_vatTextureHandle  // 0x178
  private           Beyond.Resource.FAssetProxyHandlem_spriteTextureHandle  // 0x18c
  private           System.Single                   m_timeForShader  // 0x1a0
  private   readonly System.Collections.Generic.Dictionary<System.String,Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData>m_prefabMap  // 0x1a8
  private   readonly System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedRuntimeInstance>m_instances  // 0x1b0
  private   readonly System.Collections.Generic.Queue<System.Int32>m_recyclePool  // 0x1b8
  private   readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Single,Beyond.UI.GPUI.GPUIHandle>>m_destroyQueue  // 0x1c0
  private           System.Boolean                  m_liteMode  // 0x1c8
  private           System.Action                   m_beforeRenderSync  // 0x1d0
  private           Beyond.UI.GPUI.GPUIExtendedDynamicAtlasm_dynamicAtlas  // 0x1d8
  private           Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManagerm_dynamicPrefabNodeManager  // 0x1e0
  private           Beyond.UI.GPUI.GPUIExtendedDynamicSpriteOverrideManagerm_dynamicSpriteOverrideManager  // 0x1e8
  private           Beyond.UI.GPUI.GPUIExtendedPrefabData[]m_prefabMirror  // 0x1f0
  private           Beyond.UI.GPUI.GPUIExtendedPrefabDataLite[]m_litePrefabMirror  // 0x1f8
  private           Beyond.UI.GPUI.InstanceBufferManagerBase<Beyond.UI.GPUI.GPUIExtendedRuntimeInstance,Beyond.UI.GPUI.GPUIExtendedInstanceData>m_instanceBufferManager  // 0x200
  private           Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabData>m_prefabBufferManager  // 0x208
  private           Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUIExtendedPrefabDataLite>m_litePrefabBufferManager  // 0x210
  private           Beyond.UI.GPUI.IExtendedTextSystemm_textSystem  // 0x218
PROPERTIES:
  fontAsset  get=0x03942950  set=0x0B0B4374
EVENTS:
  m_beforeRenderSync  add=add_m_beforeRenderSync  remove=remove_m_beforeRenderSync
METHODS:
  RVA=0x04CCAC20  token=0x6000CE4  TMPro.TMP_FontAsset GetFontAsset()
  RVA=0x0474F750  token=0x6000CE5  System.Void InitMaterials()
  RVA=0x033298F0  token=0x6000CE6  System.Void RegisterBeforeRenderSync(System.Action callback)
  RVA=0x0B0B430C  token=0x6000CE7  System.Void UnregisterBeforeRenderSync(System.Action callback)
  RVA=0x03AA96A0  token=0x6000CE8  System.Boolean CreateInstance(System.String prefabName, UnityEngine.Vector3 initialPosition, Beyond.UI.GPUI.GPUIHandle& handle, System.Int32 sortingOrder)
  RVA=0x03AAA030  token=0x6000CE9  System.Boolean DestroyInstance(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x02F462A0  token=0x6000CEA  System.Boolean IsInstanceValid(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x02F47D30  token=0x6000CEB  System.Boolean SetRootPosition(Beyond.UI.GPUI.GPUIHandle handle, UnityEngine.Vector3 position)
  RVA=0x02F47E40  token=0x6000CEC  System.Boolean SetRootScale(Beyond.UI.GPUI.GPUIHandle handle, UnityEngine.Vector2 scale)
  RVA=0x042E48F0  token=0x6000CED  System.Boolean SetRootRotation(Beyond.UI.GPUI.GPUIHandle handle, UnityEngine.Quaternion rotation)
  RVA=0x02F48160  token=0x6000CEE  System.Boolean SetNodeVisibility(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, System.Boolean visible)
  RVA=0x03AB6D50  token=0x6000CEF  System.Boolean SetSubrootVisibility(Beyond.UI.GPUI.GPUIHandle handle, System.String subrootName, System.Boolean visible)
  RVA=0x0B0B3DBC  token=0x6000CF0  System.Boolean SetSubrootTransform(Beyond.UI.GPUI.GPUIHandle handle, System.String subrootName, UnityEngine.Vector2 position, UnityEngine.Vector2 scale, System.Single rotationZ)
  RVA=0x0363C1D0  token=0x6000CF1  System.Boolean SetColor(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, UnityEngine.Color color)
  RVA=0x033C14C0  token=0x6000CF2  System.Boolean SetImageFillAmount(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, System.Single fillAmount)
  RVA=0x03848A00  token=0x6000CF3  System.Boolean SetText(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, System.String text)
  RVA=0x0357AC10  token=0x6000CF4  System.Boolean SetSprite(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, UnityEngine.Sprite sprite)
  RVA=0x03AA2560  token=0x6000CF5  System.Boolean StartAnimation(Beyond.UI.GPUI.GPUIHandle handle, System.Boolean destroyWhenStopped, System.String animationName)
  RVA=0x03AA2650  token=0x6000CF6  System.Boolean StartAnimation(Beyond.UI.GPUI.GPUIHandle handle, System.Boolean destroyWhenStopped, System.Int32 animationIndex)
  RVA=0x03AA2790  token=0x6000CF7  System.Boolean TryGetAnimationDuration(Beyond.UI.GPUI.GPUIHandle handle, System.String animationName, System.Single& duration)
  RVA=0x0B0B3F44  token=0x6000CF8  System.Boolean StartSubrootAnimation(Beyond.UI.GPUI.GPUIHandle handle, System.String subrootName, System.String animationName)
  RVA=0x0B0B412C  token=0x6000CF9  System.Boolean TryGetSubrootAnimationDuration(Beyond.UI.GPUI.GPUIHandle handle, System.String subrootName, System.String animationName, System.Single& duration)
  RVA=0x03944800  token=0x6000CFA  System.Void _DisposeLoadedTextures()
  RVA=0x0B0B3BD8  token=0x6000CFB  System.Void OnDestroy()
  RVA=0x03941720  token=0x6000CFC  System.Void OnEnable()
  RVA=0x0B0B3D08  token=0x6000CFD  System.Void OnDisable()
  RVA=0x033C3BB0  token=0x6000CFE  System.Void _OnPreWillRenderCanvases()
  RVA=0x03942650  token=0x6000CFF  System.Void _Init(System.String jsonPath)
  RVA=0x03941870  token=0x6000D00  System.Void _EnsureInstanceBufferManager(System.Boolean useSimpleInstanceBuffer)
  RVA=0x03942AA0  token=0x6000D01  System.Boolean _TryResolveAndValidateGroupBufferConfig(Beyond.UI.GPUI.ExtendedPrefabGroupSerializeData groupData, System.Int32& prefabBufferSize, System.Int32& fontPrefabBufferSize)
  RVA=0x03943C60  token=0x6000D02  System.Int32 _CalculateRequiredPrefabBufferSize(System.Collections.Generic.List<Beyond.UI.GPUI.ExtendedPrefabSerializeData> prefabs)
  RVA=0x033C3C50  token=0x6000D03  System.Void _DoLateUpdate()
  RVA=0x033C4030  token=0x6000D04  System.Void _RenderNodes()
  RVA=0x033C3DC0  token=0x6000D05  System.Void _DestroyAnimatedInstance()
  RVA=0x03942C00  token=0x6000D06  System.Void _PreloadText(Beyond.UI.GPUI.ExtendedPrefabGroupSerializeData data)
  RVA=0x03943DD0  token=0x6000D07  System.Void _LoadGroupData(Beyond.UI.GPUI.ExtendedPrefabGroupSerializeData data)
  RVA=0x0363F680  token=0x6000D08  Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData _LoadPrefabData(Beyond.UI.GPUI.ExtendedPrefabSerializeData prefab)
  RVA=0x046BC380  token=0x6000D09  System.Void _InitPrefabBufferManager(System.Boolean hasTextNodes, System.Int32 prefabBufferSize, System.Int32 fontPrefabBufferSize)
  RVA=0x04563A60  token=0x6000D0A  System.Int32 _FindAnchorNodeIndex(System.Collections.Generic.List<Beyond.UI.GPUI.NodeMetadata> nodeMetas, System.String subrootName)
  RVA=0x0357DE30  token=0x6000D0B  System.Int32 _LoadPrefabNodeBuffer(Beyond.UI.GPUI.ExtendedPrefabSerializeData prefab, System.Int32 totalNodeCount)
  RVA=0x03944AE0  token=0x6000D0C  System.Void _InitDynamicPrefabNodeManager(Beyond.UI.GPUI.ExtendedPrefabGroupSerializeData data)
  RVA=0x039450A0  token=0x6000D0D  System.Boolean _CanSubmitInstanceRange()
  RVA=0x03944850  token=0x6000D0E  System.Void _ResetLiveDynamicSpriteState()
  RVA=0x039449B0  token=0x6000D0F  System.Void _UpdateLiveDynamicSpriteContext()
  RVA=0x033C3E40  token=0x6000D10  System.Void _SyncDynamicAtlasRuntimeTexture()
  RVA=0x03AA97F0  token=0x6000D11  Beyond.UI.GPUI.GPUIHandle _GetOneHandle()
  RVA=0x02F463C0  token=0x6000D12  System.Boolean _IsHandleInvalid(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x02F46E30  token=0x6000D13  System.Boolean _TryResolveNode(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance& instance, System.Int32& nodeIndex)
  RVA=0x03B96050  token=0x6000D14  System.Void .ctor()
  RVA=0x0B0B42EC  token=0x6000D15  System.Void <>iFixBaseProxy_InitMaterials()
  RVA=0x0B0B42F4  token=0x6000D16  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0B4304  token=0x6000D17  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0B42FC  token=0x6000D18  System.Void <>iFixBaseProxy_OnDisable()
END_CLASS

CLASS: Beyond.UI.GPUI.IExtendedTextSystem
TYPE:  interface
TOKEN: 0x2000188
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000D19  System.Void RegisterCharacterToPrefabBuffer(System.String text)
  RVA=-1  // abstract  token=0x6000D1A  System.Void UploadBufferIfNeeded()
  RVA=-1  // abstract  token=0x6000D1B  System.Int32 GetNodeCount(System.String text)
  RVA=-1  // abstract  token=0x6000D1C  System.Void CreateTextNodes(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single textScale, System.Boolean isBold)
  RVA=-1  // abstract  token=0x6000D1D  System.Void CreateTextNodes(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single textScale, System.Boolean isBold)
  RVA=-1  // abstract  token=0x6000D1E  System.Void CreateTextNodesAutoSize(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single autoSizeWidth, System.Single nodeTextSize, System.Single autoSizeMinScale, System.Boolean isBold)
  RVA=-1  // abstract  token=0x6000D1F  System.Void CreateTextNodesAutoSize(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single autoSizeWidth, System.Single nodeTextSize, System.Single autoSizeMinScale, System.Boolean isBold)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIExtendedTextSystem`1
TYPE:  sealed class
TOKEN: 0x2000189
EXTENDS: Beyond.UI.GPUI.BaseTextSystem`1
IMPLEMENTS: Beyond.UI.GPUI.IExtendedTextSystem
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000D20  System.Void .ctor(System.Int32 fontBufferCapacity, System.Func<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector2,T> prefabCreatorFunc)
  RVA=-1  // not resolved  token=0x6000D21  System.Void CreateTextNodes(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single textScale, System.Boolean isBold)
  RVA=-1  // not resolved  token=0x6000D22  System.Void CreateTextNodes(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single textScale, System.Boolean isBold)
  RVA=-1  // not resolved  token=0x6000D23  System.Void CreateTextNodesAutoSize(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single autoSizeWidth, System.Single nodeTextSize, System.Single autoSizeMinScale, System.Boolean isBold)
  RVA=-1  // not resolved  token=0x6000D24  System.Void CreateTextNodesAutoSize(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUIExtendedInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 secondaryParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment, System.Single autoSizeWidth, System.Single nodeTextSize, System.Single autoSizeMinScale, System.Boolean isBold)
  RVA=-1  // not resolved  token=0x6000D25  Beyond.UI.GPUI.GPUIExtendedInstanceData _CreateGlyphInstance(Beyond.UI.GPUI.BaseTextSystem.CharOffsetData<T> textWidth, System.Single currentX, System.Int32 parentId, System.Int32 secondaryParentId, System.Single textScale, System.Boolean isBold)
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIHandle
TYPE:  sealed struct
TOKEN: 0x200018A
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Int32                    index  // 0x10
  public            System.Int32                    version  // 0x14
METHODS:
  RVA=0x03AA6330  token=0x6000D26  System.Boolean Equals(Beyond.UI.GPUI.GPUIHandle other)
  RVA=0x0B0B4468  token=0x6000D27  System.Boolean Equals(System.Object obj)
  RVA=0x03E00C00  token=0x6000D28  System.Int32 GetHashCode()
  RVA=0x03AA62E0  token=0x6000D29  System.Boolean op_Equality(Beyond.UI.GPUI.GPUIHandle left, Beyond.UI.GPUI.GPUIHandle right)
  RVA=0x03AA6040  token=0x6000D2A  System.Boolean op_Inequality(Beyond.UI.GPUI.GPUIHandle left, Beyond.UI.GPUI.GPUIHandle right)
  RVA=0x0B0B4504  token=0x6000D2B  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x0B0B4564  token=0x6000D2C  System.Int32 <>iFixBaseProxy_GetHashCode()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIPrefabProxyBase
TYPE:  class
TOKEN: 0x200018B
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Single                   POSITION_SYNC_EPSILON  // const
  private   static  System.Single                   POSITION_SYNC_EPSILON_SQR  // const
  private           System.String                   _prefabName  // 0x18
  private           System.Boolean                  _autoCreate  // 0x20
  private           System.Boolean                  _autoResolveSyncManager  // 0x21
  private           System.Int32                    _sortingOrder  // 0x24
  private           UnityEngine.RectTransform       m_rectTransform  // 0x28
  private           Beyond.UI.GPUI.GPUIHandle       m_handle  // 0x30
  private           Beyond.UI.GPUI.GPUIExtendedSystemm_targetSystem  // 0x38
  private           Beyond.UI.GPUI.GPUIExtendedSystemm_instanceOwnerSystem  // 0x40
  private           Beyond.UI.GPUI.GPUIProxySyncManagerm_syncManager  // 0x48
  private           System.Boolean                  m_syncManagerExplicitlySet  // 0x50
  private           System.Boolean                  m_missingSyncManagerLogged  // 0x51
  private           System.Boolean                  m_missingTargetSystemLogged  // 0x52
  private           System.Boolean                  m_positionDirty  // 0x53
  private           System.Boolean                  m_hasSyncedPosition  // 0x54
  private           UnityEngine.Vector2             m_lastCanvasLocalPosition  // 0x58
PROPERTIES:
  prefabName  get=0x0B0B4A64
  autoCreate  get=0x0B0B49C8
  sortingOrder  get=0x0B0B4AB4
  targetSystem  get=0x02F46D50
  syncManager  get=0x0B0B4B00
  handle  get=0x02F46DC0
  hasInstance  get=0x0B0B4A14
METHODS:
  RVA=0x03AA5D30  token=0x6000D34  System.Void SetSyncManager(Beyond.UI.GPUI.GPUIProxySyncManager syncManager)
  RVA=0x03AA82A0  token=0x6000D35  System.Boolean TryCreateInstance()
  RVA=0x03AA61C0  token=0x6000D36  System.Boolean TryDestroyInstance()
  RVA=0x0B0B4748  token=0x6000D37  System.Boolean TrySyncPosition()
  RVA=0x03AA5F30  token=0x6000D38  System.Void MarkPositionDirty()
  RVA=0x02F46080  token=0x6000D39  System.Boolean IsHandleValid()
  RVA=0x03AA9520  token=0x6000D3A  System.Void OnInstanceCreated()
  RVA=0x04776CA0  token=0x6000D3B  System.Void OnInstanceDestroyed()
  RVA=0x03096830  token=0x6000D3C  System.Void TryPrepareForBeforeRender()
  RVA=0x030968A0  token=0x6000D3D  System.Boolean TrySyncPosition(UnityEngine.RectTransform targetCanvasRect, UnityEngine.Camera targetCamera, System.Boolean forceSync)
  RVA=0x04695C70  token=0x6000D3E  System.Void Awake()
  RVA=0x03AA4FA0  token=0x6000D3F  System.Void OnEnable()
  RVA=0x03AA43A0  token=0x6000D40  System.Void OnDisable()
  RVA=0x03AA5EF0  token=0x6000D41  System.Void OnRectTransformDimensionsChange()
  RVA=0x0B0B4604  token=0x6000D42  System.Void OnTransformParentChanged()
  RVA=0x0B0B45B4  token=0x6000D43  System.Void OnDidApplyAnimationProperties()
  RVA=0x03AA6620  token=0x6000D44  System.Void _CacheRectTransform()
  RVA=0x0B0B47D4  token=0x6000D45  System.Boolean _TryResolveRuntimeReferences()
  RVA=0x03AA6680  token=0x6000D46  System.Boolean _TryResolveSyncManager()
  RVA=0x03AA6380  token=0x6000D47  System.Boolean _TryResolveTargetSystem()
  RVA=0x03AA5F60  token=0x6000D48  System.Void _HandleTargetSystemChanged(Beyond.UI.GPUI.GPUIExtendedSystem oldTargetSystem)
  RVA=0x0B0B4848  token=0x6000D49  System.Boolean _TryResolveTargetCanvas(UnityEngine.RectTransform& targetCanvasRect, UnityEngine.Camera& targetCamera)
  RVA=0x03096250  token=0x6000D4A  System.Boolean _TryGetCanvasLocalPosition(UnityEngine.RectTransform targetCanvasRect, UnityEngine.Camera targetCamera, UnityEngine.Vector2& canvasLocalPosition)
  RVA=0x0461E660  token=0x6000D4B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIProxySyncManager
TYPE:  class
TOKEN: 0x200018C
SIZE:  0x88
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.GPUI.GPUIExtendedSystemm_targetSystem  // 0x18
  private           Beyond.UI.GPUI.GPUIExtendedSystemm_boundSystem  // 0x20
  private   readonly System.Collections.Generic.List<Beyond.UI.GPUI.GPUIPrefabProxyBase>m_activeProxies  // 0x28
  private           Beyond.UI.GPUI.GPUIProxySyncManager.CanvasSnapshotm_lastSnapshot  // 0x30
PROPERTIES:
  targetSystem  get=0x0B0B4C40
METHODS:
  RVA=0x03AA64C0  token=0x6000D4D  System.Boolean TryResolveTargetSystem(Beyond.UI.GPUI.GPUIExtendedSystem& targetSystem)
  RVA=0x03AA81B0  token=0x6000D4E  System.Void RegisterProxy(Beyond.UI.GPUI.GPUIPrefabProxyBase proxy)
  RVA=0x03AA31E0  token=0x6000D4F  System.Void UnregisterProxy(Beyond.UI.GPUI.GPUIPrefabProxyBase proxy)
  RVA=0x0B0B4B50  token=0x6000D50  System.Void MarkDirty(Beyond.UI.GPUI.GPUIPrefabProxyBase proxy)
  RVA=0x03096560  token=0x6000D51  System.Void FlushBeforeRender()
  RVA=0x04D1A850  token=0x6000D52  System.Void OnEnable()
  RVA=0x0B0B4BF0  token=0x6000D53  System.Void OnDisable()
  RVA=0x03AA6570  token=0x6000D54  System.Void _AutoAssignTargetSystem()
  RVA=0x03AA9550  token=0x6000D55  System.Void _RegisterToSystem()
  RVA=0x04CACF00  token=0x6000D56  System.Void _UnregisterFromSystem()
  RVA=0x03096AB0  token=0x6000D57  System.Boolean _TryResolveCanvasContext(UnityEngine.Canvas canvas, UnityEngine.RectTransform& canvasRect, UnityEngine.Camera& camera)
  RVA=0x03096EF0  token=0x6000D58  System.Boolean _SnapshotAndCheckChanged(UnityEngine.Canvas canvas, UnityEngine.RectTransform canvasRect, UnityEngine.Camera camera)
  RVA=0x048E2670  token=0x6000D59  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUISharedGraphicBase
TYPE:  abstract class
TOKEN: 0x200018E
SIZE:  0x150
EXTENDS: UnityEngine.UI.MaskableGraphic
FIELDS:
  public    static readonly System.String                   CONFIG_JSON_PATH_FORMAT  // static @ 0x0
  public    static  System.Int32                    RENDER_BUFFER_INIT_CAPACITY  // const
  public    static  System.Int32                    RENDER_BUFFER_MAX_CAPACITY  // const
  public    static readonly System.Int32                    RENDER_GROUP_SIZE  // static @ 0x8
  public    static readonly System.Int32                    RENDER_GROUP_SIZE_GL  // static @ 0xc
  public            System.Collections.Generic.List<UnityEngine.Material>materialTemplates  // 0xe8
  public            System.String                   configGroupName  // 0xf0
  protected readonly System.Collections.Generic.List<UnityEngine.Material>m_instancedMaterials  // 0xf8
  protected         UnityEngine.Transform           m_targetCanvasTransform  // 0x100
  protected         UnityEngine.Matrix4x4           m_canvasObjectToWorldMatrix  // 0x108
  protected         System.Boolean                  m_inited  // 0x148
  protected         System.Boolean                  m_rendererDisabled  // 0x149
METHODS:
  RVA=0x03941B20  token=0x6000D5A  System.Void InitializeSharedState()
  RVA=0x03940D70  token=0x6000D5B  System.Void UpdateSharedTextures(UnityEngine.Texture vatTexture, UnityEngine.Texture fontTexture, UnityEngine.Texture spriteTexture)
  RVA=0x03AA7D30  token=0x6000D5C  System.Void UpdateWorldMat(System.Boolean force)
  RVA=0x049CDE20  token=0x6000D5D  System.Void InitMaterials()
  RVA=0x039424C0  token=0x6000D5E  System.Void CleanMaterials()
  RVA=0x04624990  token=0x6000D5F  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x037DCB40  token=0x6000D60  System.Void UpdateMaterial()
  RVA=0x0B0B4C90  token=0x6000D61  System.Void OnDestroy()
  RVA=0x03B96820  token=0x6000D62  System.Void .ctor()
  RVA=0x04D28180  token=0x6000D63  System.Void .cctor()
  RVA=0x0B0B4CEC  token=0x6000D64  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B085BD4  token=0x6000D65  System.Void <>iFixBaseProxy_OnPopulateMesh(UnityEngine.UI.VertexHelper P0)
  RVA=0x0B0B4CF4  token=0x6000D66  System.Void <>iFixBaseProxy_UpdateMaterial()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUISubrootTag
TYPE:  class
TOKEN: 0x2000190
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.String                   _subrootName  // 0x18
PROPERTIES:
  subrootName  get=0x0B0B4D58
METHODS:
  RVA=0x0B0B4CFC  token=0x6000D69  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUISystem
TYPE:  class
TOKEN: 0x2000191
SIZE:  0x1E8
EXTENDS: Beyond.UI.GPUI.GPUISharedGraphicBase
FIELDS:
  public    static readonly System.Int32                    PREFAB_BUFFER_SIZE  // static @ 0x0
  public    static readonly System.Int32                    PREFAB_BUFFER_FONT_SIZE  // static @ 0x4
  public            System.Int32                    sourceCNFontId  // 0x150
  private           TMPro.TMP_FontAsset             m_i18NFontAsset  // 0x158
  public            System.Boolean                  noSortingOrder  // 0x160
  private           System.Boolean                  m_liteMode  // 0x161
  private           UnityEngine.Texture             m_vatTexture  // 0x168
  private           UnityEngine.Texture             m_spriteTexture  // 0x170
  private           Beyond.Resource.FAssetProxyHandlem_vatTextureHandle  // 0x178
  private           Beyond.Resource.FAssetProxyHandlem_spriteTextureHandle  // 0x18c
  private           System.Single                   m_timeForShader  // 0x1a0
  private           System.Collections.Generic.Dictionary<System.String,Beyond.UI.GPUI.RuntimePrefabData>m_prefabMap  // 0x1a8
  private           System.Collections.Generic.List<Beyond.UI.GPUI.RuntimeInstance>m_instances  // 0x1b0
  private           System.Collections.Generic.Queue<System.Int32>m_recyclePool  // 0x1b8
  private           System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Single,Beyond.UI.GPUI.GPUIHandle>>m_destroyQueue  // 0x1c0
  private           Beyond.UI.GPUI.ITextSystem      m_simpleTextSystem  // 0x1c8
  private           Beyond.UI.GPUI.ExInstanceBufferManager<Beyond.UI.GPUI.RuntimeInstance,Beyond.UI.GPUI.GPUInstanceData>m_instanceBufferManager  // 0x1d0
  private           Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUPrefabData>m_prefabBufferManager  // 0x1d8
  private           Beyond.UI.GPUI.SimpleBufferManager<Beyond.UI.GPUI.GPUPrefabDataLite>m_litePrefabBufferManager  // 0x1e0
PROPERTIES:
  fontAsset  get=0x0465A480  set=0x0B0B53B4
METHODS:
  RVA=0x03AAA690  token=0x6000D6C  System.Boolean CreateInstance(System.String prefabName, UnityEngine.Vector3 initialPosition, Beyond.UI.GPUI.GPUIHandle& handle, System.Int32 sortingOrder)
  RVA=0x03AAA520  token=0x6000D6D  System.Boolean DestroyInstance(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x03098800  token=0x6000D6E  System.Boolean IsInstanceValid(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x030988A0  token=0x6000D6F  System.Boolean SetRootPosition(Beyond.UI.GPUI.GPUIHandle handle, UnityEngine.Vector3 position)
  RVA=0x0B0B4EB8  token=0x6000D70  System.Boolean SetRootScale(Beyond.UI.GPUI.GPUIHandle handle, UnityEngine.Vector2 scale)
  RVA=0x03AA6720  token=0x6000D71  System.Boolean StartAnimation(Beyond.UI.GPUI.GPUIHandle handle, System.Boolean destroyWhenStopped, System.String animationName, System.Boolean resetDestroy)
  RVA=0x0B0B50A8  token=0x6000D72  System.Boolean StartAnimation(Beyond.UI.GPUI.GPUIHandle handle, System.Boolean destroyWhenStopped, System.Int32 animationIndex, System.Boolean resetDestroy)
  RVA=0x0B0B4FA0  token=0x6000D73  System.Boolean SetVisibility(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, System.Boolean visible)
  RVA=0x0363BD90  token=0x6000D74  System.Boolean SetColor(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, UnityEngine.Color color)
  RVA=0x0363BE90  token=0x6000D75  System.Boolean SetText(Beyond.UI.GPUI.GPUIHandle handle, System.String nodeName, System.String text)
  RVA=0x0B0B4DA8  token=0x6000D76  System.Void OnDestroy()
  RVA=0x04659D90  token=0x6000D77  System.Void OnEnable()
  RVA=0x0B0B4E04  token=0x6000D78  System.Void OnDisable()
  RVA=0x04624950  token=0x6000D79  System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
  RVA=0x037DC750  token=0x6000D7A  System.Void UpdateMaterial()
  RVA=0x03AA7910  token=0x6000D7B  System.Void _PreWillRenderCanvases()
  RVA=0x04659EA0  token=0x6000D7C  System.Void _Init(System.String jsonPath)
  RVA=0x0465A5D0  token=0x6000D7D  TMPro.TMP_FontAsset GetFontAsset()
  RVA=0x03AA7950  token=0x6000D7E  System.Void _DoLateUpdate()
  RVA=0x04D50C00  token=0x6000D7F  System.Void _DisposeLoadedTextures()
  RVA=0x0B0B52F4  token=0x6000D80  System.Void _DoOnDestroy()
  RVA=0x03AA7AE0  token=0x6000D81  System.Void _RenderNodes()
  RVA=0x03AA7A00  token=0x6000D82  System.Void _DestroyAnimatedInstance()
  RVA=0x039430E0  token=0x6000D83  System.Void _PreloadTextInGroupData(Beyond.UI.GPUI.PrefabGroupSerializeData data)
  RVA=0x039435F0  token=0x6000D84  System.Void _LoadGroupData(Beyond.UI.GPUI.PrefabGroupSerializeData data)
  RVA=0x0363DF90  token=0x6000D85  Beyond.UI.GPUI.RuntimePrefabData _LoadPrefabData(Beyond.UI.GPUI.PrefabSerializeData prefab)
  RVA=0x040FB390  token=0x6000D86  System.Int32 _LoadPrefabNodeBuffer(Beyond.UI.GPUI.PrefabSerializeData prefab, System.Int32 totalNodeCount)
  RVA=0x03AAA150  token=0x6000D87  System.Boolean _CreateInstance(System.String prefabName, Beyond.UI.GPUI.GPUIHandle& handle, System.Int32 sortingOrder)
  RVA=0x03AAA350  token=0x6000D88  System.Boolean _DestroyInstance(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x03AAA240  token=0x6000D89  Beyond.UI.GPUI.GPUIHandle _GetOneHandle()
  RVA=0x03098960  token=0x6000D8A  System.Boolean _IsHandleInvalid(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x03AAA100  token=0x6000D8B  System.Void _RecycleOneHandle(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=0x049598B0  token=0x6000D8C  System.Void .ctor()
  RVA=0x04D5DF50  token=0x6000D8D  System.Void .cctor()
  RVA=0x0B0B42F4  token=0x6000D8E  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B0B4304  token=0x6000D8F  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x0B0B42FC  token=0x6000D90  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x0B0B52E4  token=0x6000D91  System.Void <>iFixBaseProxy_OnPopulateMesh(UnityEngine.UI.VertexHelper P0)
  RVA=0x0B0B52EC  token=0x6000D92  System.Void <>iFixBaseProxy_UpdateMaterial()
END_CLASS

CLASS: Beyond.UI.GPUI.GPUIUtils
TYPE:  static class
TOKEN: 0x2000192
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Vector3             DEFAULT_NORMAL  // static @ 0x0
  public    static readonly UnityEngine.Vector4             DEFAULT_TANGENT  // static @ 0xc
METHODS:
  RVA=0x0B0B54A8  token=0x6000D93  UnityEngine.Mesh CreateQuadMesh()
  RVA=0x0363C340  token=0x6000D94  System.UInt32 ColorToRGBAUint32(UnityEngine.Color color)
  RVA=0x04B52960  token=0x6000D95  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.GPUI.InstanceBufferManagerBase`2
TYPE:  abstract class
TOKEN: 0x2000193
IMPLEMENTS: System.IDisposable
FIELDS:
PROPERTIES:
  nodeCount  get=-1  // abstract
  gpuGroups  get=-1  // abstract
  gpuGroupCount  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000D99  System.Void BindInstanceList(System.Collections.Generic.List<TInstance> runtimeInstanceView)
  RVA=-1  // abstract  token=0x6000D9A  System.Void MarkNeedUpdate()
  RVA=-1  // abstract  token=0x6000D9B  System.Void Add(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=-1  // abstract  token=0x6000D9C  System.Void Remove(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=-1  // abstract  token=0x6000D9D  System.Void DoUpdate(UnityEngine.CanvasRenderer canvasRenderer)
  RVA=-1  // abstract  token=0x6000D9E  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000D9F  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.RuntimeInstance
TYPE:  sealed class
TOKEN: 0x2000194
SIZE:  0x60
EXTENDS: Beyond.UI.GPUI.BaseRuntimeInstance`1
FIELDS:
  public    static  System.Int32                    ROOT_NODE_INDEX  // const
  private           Beyond.UI.GPUI.ITextSystem      m_textSystem  // 0x20
  private           Beyond.UI.GPUI.RuntimePrefabDatam_prefab  // 0x28
  private           Beyond.UI.GPUI.RuntimeAnimationDatam_animationData  // 0x30
  private           System.Int32                    m_prefabNodeOffset  // 0x38
  private           Beyond.UI.GPUI.RuntimeNodeInfo[]m_nodeInfo  // 0x40
  private           System.Int32                    m_nodeInfoCount  // 0x48
  private           System.Single                   m_animationDuration  // 0x4c
  private           System.Single                   m_animationTime  // 0x50
  private           UnityEngine.Vector2             m_rootScale  // 0x54
PROPERTIES:
  Prefab  get=0x0363BF90
  AnimationDuration  get=0x048ACC20
METHODS:
  RVA=0x044299D0  token=0x6000DA2  System.Void Create(Beyond.UI.GPUI.RuntimePrefabData prefab, Beyond.UI.GPUI.ITextSystem textSystem)
  RVA=0x047D8430  token=0x6000DA3  System.Void Recycle()
  RVA=0x0363ED70  token=0x6000DA4  System.Void DoUpdate(Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUInstanceData>& buffer, System.Int32 startIndexInBuffer, System.Boolean full)
  RVA=0x0B0B5B14  token=0x6000DA5  System.Void SetRootScale(UnityEngine.Vector2 rootScale)
  RVA=0x030989E0  token=0x6000DA6  System.Void SetPosition(System.Int32 index, UnityEngine.Vector3 position)
  RVA=0x04429B20  token=0x6000DA7  System.Void StartAnimation(System.Single animationTime, System.Int32 animationIndex)
  RVA=0x0B0B5B94  token=0x6000DA8  System.Void SetVisibility(System.Int32 index, System.Boolean visible)
  RVA=0x0363BFC0  token=0x6000DA9  System.Void SetColor(System.Int32 index, UnityEngine.Color newColor)
  RVA=0x0363C550  token=0x6000DAA  System.Void SetText(System.Int32 index, System.String text)
  RVA=0x04D33C40  token=0x6000DAB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUI.SimpleBufferManager`1
TYPE:  class
TOKEN: 0x2000195
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Int32                    m_objectSize  // 0x0
  private           System.Int32                    m_count  // 0x0
  private           System.Int32                    m_capacity  // 0x0
  private           System.Boolean                  m_isFrozen  // 0x0
  private           System.Int32                    m_writingRef  // 0x0
  private           UnityEngine.CanvasRenderer      m_canvasRenderer  // 0x0
  private           Unity.Collections.NativeArray<T>m_cpuArray  // 0x0
PROPERTIES:
  cpuArray  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000DAD  System.Void .ctor(System.Int32 initCapacity, UnityEngine.CanvasRenderer canvasRenderer)
  RVA=-1  // not resolved  token=0x6000DAE  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000DAF  System.Void Reset()
  RVA=-1  // not resolved  token=0x6000DB0  System.Void OpenWrite()
  RVA=-1  // not resolved  token=0x6000DB1  System.Void Freeze()
  RVA=-1  // not resolved  token=0x6000DB2  System.Boolean Allocate(System.Int32 size, System.Int32& index)
END_CLASS

CLASS: Beyond.UI.GPUI.SimpleInstanceBufferManager`2
TYPE:  sealed class
TOKEN: 0x2000196
EXTENDS: Beyond.UI.GPUI.InstanceBufferManagerBase`2
FIELDS:
  private   readonly System.Int32                    m_gpuGroupCapacity  // 0x0
  private   readonly System.Collections.Generic.List<Beyond.UI.GPUI.GPUIHandle>m_handles  // 0x0
  private           Unity.Collections.NativeArray<UnityEngine.CanvasRenderer.InstanceRange>m_gpuGroups  // 0x0
  private           System.Collections.Generic.List<TInstance>m_runtimeInstanceView  // 0x0
  private           System.Boolean                  m_hasAnyChanges  // 0x0
  private           System.Int32                    m_capacity  // 0x0
  private           System.Int32                    m_groupCount  // 0x0
  private           System.Int32                    m_currentLength  // 0x0
PROPERTIES:
  nodeCount  get=-1  // not resolved
  gpuGroups  get=-1  // not resolved
  gpuGroupCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000DB6  System.Void .ctor(System.Int32 initCapacity, System.Int32 gpuGroupCapacity)
  RVA=-1  // not resolved  token=0x6000DB7  System.Void BindInstanceList(System.Collections.Generic.List<TInstance> runtimeInstanceView)
  RVA=-1  // not resolved  token=0x6000DB8  System.Void MarkNeedUpdate()
  RVA=-1  // not resolved  token=0x6000DB9  System.Void Add(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=-1  // not resolved  token=0x6000DBA  System.Void Remove(Beyond.UI.GPUI.GPUIHandle handle)
  RVA=-1  // not resolved  token=0x6000DBB  System.Void DoUpdate(UnityEngine.CanvasRenderer canvasRenderer)
  RVA=-1  // not resolved  token=0x6000DBC  System.Void _AddGroup(System.Int32 start, System.Int32 end)
  RVA=-1  // not resolved  token=0x6000DBD  System.Void _EnsureGroupCapacity(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000DBE  System.Void Dispose()
END_CLASS

CLASS: Beyond.UI.GPUI.ITextSystem
TYPE:  interface
TOKEN: 0x2000197
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000DBF  System.Void RegisterCharacterToPrefabBuffer(System.String text)
  RVA=-1  // abstract  token=0x6000DC0  System.Void UploadBufferIfNeeded()
  RVA=-1  // abstract  token=0x6000DC1  System.Int32 GetNodeCount(System.String text)
  RVA=-1  // abstract  token=0x6000DC2  System.Void CreateTextNodes(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 grandParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment)
  RVA=-1  // abstract  token=0x6000DC3  System.Void CreateTextNodes(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 grandParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment)
END_CLASS

CLASS: Beyond.UI.GPUI.SimpleTextSystem`1
TYPE:  sealed class
TOKEN: 0x2000198
EXTENDS: Beyond.UI.GPUI.BaseTextSystem`1
IMPLEMENTS: Beyond.UI.GPUI.ITextSystem
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000DC4  System.Void .ctor(System.Int32 fontBufferCapacity, System.Func<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector2,T> creatorFunc)
  RVA=-1  // not resolved  token=0x6000DC5  System.Void CreateTextNodes(System.String text, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 grandParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment)
  RVA=-1  // not resolved  token=0x6000DC6  System.Void CreateTextNodes(System.String text, System.Collections.Generic.List<Beyond.UI.GPUI.GPUInstanceData> dest, System.Int32 startIndex, System.Int32 capacity, System.Int32 parentId, System.Int32 grandParentId, Beyond.UI.GPUI.SimpleTextAlignment alignment)
  RVA=-1  // not resolved  token=0x6000DC7  Beyond.UI.GPUI.GPUInstanceData _CreateGlyphInstance(Beyond.UI.GPUI.BaseTextSystem.CharOffsetData<T> textWidth, System.Single currentX, System.Int32 parentId, System.Int32 grandParentId)
END_CLASS

CLASS: Beyond.UI.UIState.OnValueChangedAttribute
TYPE:  class
TOKEN: 0x2000142
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  private   readonly System.String                   <actionName>k__BackingField  // 0x10
PROPERTIES:
  actionName  get=0x02B2ECC0
METHODS:
  RVA=0x053908C0  token=0x6000BD5  System.Void .ctor(System.String name)
END_CLASS

CLASS: Beyond.UI.UIState.StateAnimationInfo
TYPE:  class
TOKEN: 0x2000143
SIZE:  0x30
FIELDS:
  private           System.Boolean                  <IsFinished>k__BackingField  // 0x10
  private   readonly UnityEngine.AnimationClip       <Clip>k__BackingField  // 0x18
  private   readonly UnityEngine.GameObject          <Target>k__BackingField  // 0x20
  private           System.Single                   <StartTime>k__BackingField  // 0x28
  private   readonly System.Single                   m_duration  // 0x2c
PROPERTIES:
  IsFinished  get=0x015EFCE0  set=0x04D86130
  Clip  get=0x0385B100
  Target  get=0x04D862C0
  StartTime  get=0x04D86BF0  set=0x04D86C00
METHODS:
  RVA=0x0B1005C4  token=0x6000BDC  System.Void .ctor(UnityEngine.AnimationClip clip, UnityEngine.GameObject target)
  RVA=0x0B100514  token=0x6000BDD  System.Void OnUpdate()
END_CLASS

CLASS: Beyond.UI.UIState.AnimationImpactType
TYPE:  sealed struct
TOKEN: 0x2000144
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.AnimationImpactTypeStartAnimation  // const
  public    static  Beyond.UI.UIState.AnimationImpactTypeStopAnimation  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.StateDescription
TYPE:  class
TOKEN: 0x2000145
SIZE:  0x298
FIELDS:
  public            Beyond.UI.UIState.AnimationImpactTypeanimationImpactType  // 0x10
  public            UnityEngine.AnimationClip       animationClip  // 0x18
  public            UnityEngine.GameObject          animationTarget  // 0x20
  public            UnityEngine.Animator            animator  // 0x28
  public            Beyond.UI.UIState.AnimatorImpactTypeanimImpact  // 0x30
  public            System.String                   animParamSetBoolTrue  // 0x38
  public            System.String                   animParamSetBoolFalse  // 0x40
  public            System.String                   animParamSetTrigger  // 0x48
  public            System.String                   animParamResetTrigger  // 0x50
  public            System.String                   animParamSetInteger  // 0x58
  public            System.Int32                    animParamSetIntegerValue  // 0x60
  public            System.String                   animParamSetFloat  // 0x68
  public            System.Int32                    animParamSetFloatValue  // 0x70
  public            Beyond.UI.UIButton              button  // 0x78
  public            System.Boolean                  buttonInteractable  // 0x80
  public            UnityEngine.CanvasGroup         canvasGroup  // 0x88
  public            Beyond.UI.UIState.CanvasGroupImpactTypecanvasGroupImpactType  // 0x90
  public            System.Single                   canvasGroupAlpha  // 0x94
  public            System.Boolean                  canvasGroupInteractable  // 0x98
  public            UnityEngine.Color               canvasGroupColor  // 0x9c
  public            UnityEngine.Behaviour           component  // 0xb0
  public            System.Boolean                  componentIsEnabled  // 0xb8
  public            Beyond.UI.UIState.StateDescriptionTargetTypetargetType  // 0xbc
  public            UnityEngine.GameObject          gameObject  // 0xc0
  public            System.Boolean                  gameObjectIsActive  // 0xc8
  public            UnityEngine.UI.Graphic          graphic  // 0xd0
  public            UnityEngine.Material            graphicMaterial  // 0xd8
  public            UnityEngine.UI.GridLayoutGroup  gridLayoutGroup  // 0xe0
  public            Beyond.UI.UIState.GridLayoutGroupImpactTypegridLayoutGroupImpactType  // 0xe8
  public            UnityEngine.RectOffset          gridLayoutGroupPadding  // 0xf0
  public            UnityEngine.Vector2             gridLayoutGroupSpacing  // 0xf8
  public            UnityEngine.Vector2             gridLayoutGroupCellSize  // 0x100
  public            System.Int32                    gridLayoutConstraintCount  // 0x108
  public            Beyond.UI.UIImage               image  // 0x110
  public            Beyond.UI.UIState.ImageImpactTypeimageImpact  // 0x118
  public            System.Boolean                  imageEnabled  // 0x11c
  public            UnityEngine.Sprite              imageSetSprite  // 0x120
  public            UnityEngine.Color               imageSetColor  // 0x128
  public            System.String                   imageSetMaterialFloatParam  // 0x138
  public            System.Single                   imageSetMaterialFloatValue  // 0x140
  private           UnityEngine.Material            <ImageMaterialClone>k__BackingField  // 0x148
  public            UnityEngine.UI.LayoutElement    layoutElement  // 0x150
  public            Beyond.UI.UIState.LayoutElementImpactTypelayoutElementImpactType  // 0x158
  public            System.Single                   layoutElementPreferredWidth  // 0x15c
  public            System.Single                   layoutElementPreferredHeight  // 0x160
  public            System.Single                   layoutElementMinWidth  // 0x164
  public            System.Single                   layoutElementMinHeight  // 0x168
  public            System.Single                   layoutElementFlexibleWidth  // 0x16c
  public            System.Single                   layoutElementFlexibleHeight  // 0x170
  public            UnityEngine.UI.HorizontalOrVerticalLayoutGrouplayoutGroup  // 0x178
  public            Beyond.UI.UIState.LayoutGroupImpactTypelayoutGroupImpactType  // 0x180
  public            UnityEngine.RectOffset          layoutGroupPadding  // 0x188
  public            System.Single                   layoutGroupSpacing  // 0x190
  public            UnityEngine.TextAnchor          layoutGroupChildAlignment  // 0x194
  public            System.Boolean                  layoutGroupReverseArrangement  // 0x198
  public            UnityEngine.RectTransform       rectTransform  // 0x1a0
  public            Beyond.UI.UIState.RectTransformImpactTyperectTransformImpactType  // 0x1a8
  public            UnityEngine.Vector2             rectTransformPosition  // 0x1ac
  public            UnityEngine.Vector2             rectTransformSize  // 0x1b4
  public            UnityEngine.Vector3             rectTransformRotation  // 0x1bc
  public            UnityEngine.Vector3             rectTransformScale  // 0x1c8
  public            System.Single                   rectTransformWidth  // 0x1d4
  public            System.Single                   rectTransformHeight  // 0x1d8
  public            System.Single                   rectTransformTop  // 0x1dc
  public            System.Single                   rectTransformBottom  // 0x1e0
  public            System.Single                   rectTransformLeft  // 0x1e4
  public            System.Single                   rectTransformRight  // 0x1e8
  public            UnityEngine.Vector2             rectTransformAnchorMin  // 0x1ec
  public            UnityEngine.Vector2             rectTransformAnchorMax  // 0x1f4
  public            UnityEngine.Vector2             rectTransformPivot  // 0x1fc
  public            Beyond.UI.UIScrollList          scrollList  // 0x208
  public            Beyond.UI.UIState.ScrollListImpactTypescrollListImpactType  // 0x210
  public            Beyond.UI.UIScrollList.Padding  scrollListPadding  // 0x214
  public            UnityEngine.Vector2             scrollListSpace  // 0x224
  public            UnityEngine.UI.Selectable       selectable  // 0x230
  public            System.Boolean                  selectableInteractable  // 0x238
  public            Beyond.UI.UIState.UIStateControlleruiStateController  // 0x240
  public            System.String                   stateName  // 0x248
  public            Beyond.UI.UIText                text  // 0x250
  public            Beyond.UI.UIState.TextImpactTypetextImpact  // 0x258
  public            UnityEngine.Color               textColor  // 0x25c
  public            System.String                   textId  // 0x270
  public            System.Single                   textFontSize  // 0x278
  public            System.Single                   textMaxFontSize  // 0x27c
  public            UnityEngine.Vector2             textMaxSize  // 0x280
  public            UnityEngine.Vector2             textMinSize  // 0x288
  public            TMPro.HorizontalAlignmentOptionstextHorizontalAlignment  // 0x290
PROPERTIES:
  IsEmpty  get=0x03CEB110
  ReadableDescription  get=0x0B102740
  ImageMaterialClone  get=0x04D8DAC0  set=0x06B8B468
METHODS:
  RVA=0x0B100D4C  token=0x6000BDE  System.Boolean EqualsAnimation(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100618  token=0x6000BDF  System.Single ApplyAnimation(Beyond.UI.UIState.StateReference state)
  RVA=0x0B100DE4  token=0x6000BE0  System.Boolean EqualsAnimator(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100794  token=0x6000BE1  System.Void ApplyAnimator()
  RVA=0x0B100E7C  token=0x6000BE2  System.Boolean EqualsButton(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B1008B8  token=0x6000BE3  System.Void ApplyButton()
  RVA=0x0B100F14  token=0x6000BE4  System.Boolean EqualsCanvasGroup(Beyond.UI.UIState.StateDescription other)
  RVA=0x048F6D00  token=0x6000BE5  System.Void ApplyCanvasGroup()
  RVA=0x0B100FB4  token=0x6000BE6  System.Boolean EqualsComponent(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100918  token=0x6000BE7  System.Void ApplyComponent()
  RVA=0x0B1018B4  token=0x6000BEA  System.String ToString()
  RVA=0x0B103998  token=0x6000BEB  System.Boolean op_Equality(Beyond.UI.UIState.StateDescription left, Beyond.UI.UIState.StateDescription right)
  RVA=0x0B103A14  token=0x6000BEC  System.Boolean op_Inequality(Beyond.UI.UIState.StateDescription left, Beyond.UI.UIState.StateDescription right)
  RVA=0x0B1017C0  token=0x6000BED  System.Boolean Equals(System.Object obj)
  RVA=0x0B101850  token=0x6000BEE  System.Int32 GetHashCode()
  RVA=0x0B101734  token=0x6000BEF  System.Boolean Equals(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B101948  token=0x6000BF0  System.Boolean _EqualsByTargetType(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B101B90  token=0x6000BF1  System.Int32 _GetTargetHashCode()
  RVA=0x0B101054  token=0x6000BF2  System.Boolean EqualsGameObject(Beyond.UI.UIState.StateDescription other)
  RVA=0x03CEB2E0  token=0x6000BF3  System.Void ApplyGameObject()
  RVA=0x0B1010F4  token=0x6000BF4  System.Boolean EqualsGraphic(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B10097C  token=0x6000BF5  System.Void ApplyGraphic()
  RVA=0x0B101194  token=0x6000BF6  System.Boolean EqualsGridLayoutGroup(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B1009E4  token=0x6000BF7  System.Void ApplyGridLayoutGroup()
  RVA=0x0B101C70  token=0x6000BF8  System.Void _OnGridLayoutGroupChanged()
  RVA=0x0B101234  token=0x6000BFB  System.Boolean EqualsImage(Beyond.UI.UIState.StateDescription other)
  RVA=0x03CEB8D0  token=0x6000BFC  System.Void ApplyImage()
  RVA=0x0B101D80  token=0x6000BFD  System.Void _OnImageChanged()
  RVA=0x0B1012D4  token=0x6000BFE  System.Boolean EqualsLayoutElement(Beyond.UI.UIState.StateDescription other)
  RVA=0x0344CAC0  token=0x6000BFF  System.Void ApplyLayoutElement()
  RVA=0x0B101ED4  token=0x6000C00  System.Void _OnLayoutElementChanged()
  RVA=0x0B101374  token=0x6000C01  System.Boolean EqualsLayoutGroup(Beyond.UI.UIState.StateDescription other)
  RVA=0x049955D0  token=0x6000C02  System.Void ApplyLayoutGroup()
  RVA=0x0B101FA0  token=0x6000C03  System.Void _OnLayoutGroupChanged()
  RVA=0x0B101414  token=0x6000C04  System.Boolean EqualsRectTransform(Beyond.UI.UIState.StateDescription other)
  RVA=0x03CEB970  token=0x6000C05  System.Void ApplyRectTransform()
  RVA=0x0B102098  token=0x6000C06  System.Void _OnRectTransformChanged()
  RVA=0x0B1014B4  token=0x6000C07  System.Boolean EqualsScrollList(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100BA0  token=0x6000C08  System.Void ApplyScrollList()
  RVA=0x0B10256C  token=0x6000C09  System.Void _OnScrollListChanged()
  RVA=0x0B101554  token=0x6000C0A  System.Boolean EqualsSelectable(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100CE8  token=0x6000C0B  System.Void ApplySelectable()
  RVA=0x0B1015F4  token=0x6000C0C  System.Boolean EqualsState(Beyond.UI.UIState.StateDescription other)
  RVA=0x0B100B38  token=0x6000C0D  System.Single ApplyInnerState()
  RVA=0x0B101694  token=0x6000C0E  System.Boolean EqualsText(Beyond.UI.UIState.StateDescription other)
  RVA=0x03CEB370  token=0x6000C0F  System.Void ApplyText()
  RVA=0x0B102660  token=0x6000C10  System.Void _OnTextChanged()
  RVA=0x03F09B90  token=0x6000C11  System.Void .ctor()
  RVA=0x0669AD6C  token=0x6000C12  System.String <>iFixBaseProxy_ToString()
  RVA=0x02FB1840  token=0x6000C13  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x04D87100  token=0x6000C14  System.Int32 <>iFixBaseProxy_GetHashCode()
END_CLASS

CLASS: Beyond.UI.UIState.AnimatorImpactType
TYPE:  sealed struct
TOKEN: 0x2000146
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.AnimatorImpactTypeSetBoolTrue  // const
  public    static  Beyond.UI.UIState.AnimatorImpactTypeSetBoolFalse  // const
  public    static  Beyond.UI.UIState.AnimatorImpactTypeSetTrigger  // const
  public    static  Beyond.UI.UIState.AnimatorImpactTypeResetTrigger  // const
  public    static  Beyond.UI.UIState.AnimatorImpactTypeSetInteger  // const
  public    static  Beyond.UI.UIState.AnimatorImpactTypeSetFloat  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.CanvasGroupImpactType
TYPE:  sealed struct
TOKEN: 0x2000147
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.CanvasGroupImpactTypeSetAlpha  // const
  public    static  Beyond.UI.UIState.CanvasGroupImpactTypeSetInteractable  // const
  public    static  Beyond.UI.UIState.CanvasGroupImpactTypeSetColor  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.StateDescriptionTargetType
TYPE:  sealed struct
TOKEN: 0x2000148
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeGameObject  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeAnimator  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeImage  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeText  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeRectTransform  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeButton  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeCanvasGroup  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeAnimation  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeState  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeLayoutElement  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeComponent  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeGraphic  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeLayoutGroup  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeScrollList  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeGridLayoutGroup  // const
  public    static  Beyond.UI.UIState.StateDescriptionTargetTypeSelectable  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.GridLayoutGroupImpactType
TYPE:  sealed struct
TOKEN: 0x2000149
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.GridLayoutGroupImpactTypePadding  // const
  public    static  Beyond.UI.UIState.GridLayoutGroupImpactTypeSpacing  // const
  public    static  Beyond.UI.UIState.GridLayoutGroupImpactTypeCellSize  // const
  public    static  Beyond.UI.UIState.GridLayoutGroupImpactTypeConstraintCount  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.ImageImpactType
TYPE:  sealed struct
TOKEN: 0x200014A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.ImageImpactTypeSetEnabled  // const
  public    static  Beyond.UI.UIState.ImageImpactTypeSetSprite  // const
  public    static  Beyond.UI.UIState.ImageImpactTypeSetColor  // const
  public    static  Beyond.UI.UIState.ImageImpactTypeSetMaterialFloatValue  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.LayoutElementImpactType
TYPE:  sealed struct
TOKEN: 0x200014B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementPreferredWidth  // const
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementPreferredHeight  // const
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementMinWidth  // const
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementMinHeight  // const
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementFlexibleWidth  // const
  public    static  Beyond.UI.UIState.LayoutElementImpactTypeLayoutElementFlexibleHeight  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.LayoutGroupImpactType
TYPE:  sealed struct
TOKEN: 0x200014C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.LayoutGroupImpactTypePadding  // const
  public    static  Beyond.UI.UIState.LayoutGroupImpactTypeSpacing  // const
  public    static  Beyond.UI.UIState.LayoutGroupImpactTypeChildAlignment  // const
  public    static  Beyond.UI.UIState.LayoutGroupImpactTypeReverseArrangement  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.RectTransformImpactType
TYPE:  sealed struct
TOKEN: 0x200014D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.RectTransformImpactTypeUndefined  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetPosition  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetSize  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetRotation  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetScale  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetWidth  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetHeight  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetTop  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetBottom  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetLeft  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetRight  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetAnchors  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeSetPivot  // const
  public    static  Beyond.UI.UIState.RectTransformImpactTypeForceRebuildLayout  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.ScrollListImpactType
TYPE:  sealed struct
TOKEN: 0x200014E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.ScrollListImpactTypePadding  // const
  public    static  Beyond.UI.UIState.ScrollListImpactTypeSpace  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.TextImpactType
TYPE:  sealed struct
TOKEN: 0x200014F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIState.TextImpactTypeSetColor  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetTextById  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetFontSize  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetHorizontalAlignment  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetMaxFontSize  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetMaxSize  // const
  public    static  Beyond.UI.UIState.TextImpactTypeSetMinSize  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.UIState.StateReference
TYPE:  class
TOKEN: 0x2000150
SIZE:  0x40
FIELDS:
  public            System.String                   stateName  // 0x10
  public            System.Collections.Generic.List<Beyond.UI.UIState.StateDescription>descriptions  // 0x18
  private           System.Collections.Generic.List<Beyond.UI.UIState.StateAnimationInfo>m_scheduledForPlaying  // 0x20
  private           System.Collections.Generic.List<Beyond.UI.UIState.StateAnimationInfo>m_scheduledForStopping  // 0x28
  private           Beyond.UI.UIState.UIStateControllerm_controller  // 0x30
  private           System.Boolean                  m_isDirty  // 0x38
METHODS:
  RVA=0x03CEB330  token=0x6000C15  System.Void SetController(Beyond.UI.UIState.UIStateController controller)
  RVA=0x0B103A7C  token=0x6000C16  System.Void ScheduleForPlaying(Beyond.UI.UIState.StateAnimationInfo stateAnimationInfo)
  RVA=0x0B103B1C  token=0x6000C17  System.Void ScheduleForStopping(Beyond.UI.UIState.StateAnimationInfo stateAnimationInfo)
  RVA=0x0401F580  token=0x6000C18  System.Void HandleScheduledStates()
  RVA=0x039C46E0  token=0x6000C19  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIState.UIStateController
TYPE:  class
TOKEN: 0x2000151
SIZE:  0x80
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            System.Collections.Generic.List<Beyond.UI.UIState.StateReference>states  // 0x68
  private           System.String                   <currentStateName>k__BackingField  // 0x70
  private   readonly System.Collections.Generic.List<Beyond.UI.UIState.StateAnimationInfo>m_animations  // 0x78
PROPERTIES:
  tickOption  get=0x040CAFF0
  currentStateName  get=0x04D862B0  set=0x063AD760
METHODS:
  RVA=0x0B106530  token=0x6000C1D  System.Void OnStateReferenceDirty()
  RVA=0x03CEA2B0  token=0x6000C1E  System.Single SetState(System.String stateName, System.Boolean forceUpdate)
  RVA=0x03CEA380  token=0x6000C1F  System.Single _ApplyState(Beyond.UI.UIState.StateReference state)
  RVA=0x0401E450  token=0x6000C20  System.Void Tick(System.Single deltaTime)
  RVA=0x0401F530  token=0x6000C21  System.Void _HandleStateAnimations(Beyond.UI.UIState.StateReference stateReference)
  RVA=0x0B106580  token=0x6000C22  System.Void Run(Beyond.UI.UIState.StateAnimationInfo newInfo)
  RVA=0x0B1067B4  token=0x6000C23  System.Void Stop(Beyond.UI.UIState.StateAnimationInfo newInfo)
  RVA=0x039C4650  token=0x6000C24  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000C25  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x6000C26  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIState.UIStateControllerUtils
TYPE:  static class
TOKEN: 0x2000153
SIZE:  0x10
FIELDS:
  private   static  System.Single                   s_updateFinishTime  // static @ 0x0
METHODS:
  RVA=0x0B106338  token=0x6000C2A  System.Void ForceRebuildLayout(UnityEngine.GameObject gameObject)
  RVA=0x03CEC7E0  token=0x6000C2B  System.Void SetLeft(UnityEngine.RectTransform rt, System.Single left)
  RVA=0x03CEC8C0  token=0x6000C2C  System.Void SetRight(UnityEngine.RectTransform rt, System.Single right)
  RVA=0x04CA6FE0  token=0x6000C2D  System.Void SetTop(UnityEngine.RectTransform rt, System.Single top)
  RVA=0x04CAD810  token=0x6000C2E  System.Void SetBottom(UnityEngine.RectTransform rt, System.Single bottom)
  RVA=0x0B106438  token=0x6000C2F  System.String GetName(UnityEngine.Object obj)
  RVA=0x0B1064E4  token=0x6000C30  System.Void StartEditorUpdateLoop(System.Single duration)
END_CLASS

CLASS: Cysharp.Text.TextMeshProExtensions
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000005  System.Void SetText(TMPro.TMP_Text text, T arg0)
  RVA=-1  // generic def  token=0x6000006  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0)
  RVA=-1  // generic def  token=0x6000007  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x6000008  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000009  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x600000A  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x600000B  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x600000C  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x600000D  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x600000E  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x600000F  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=-1  // generic def  token=0x6000010  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
  RVA=-1  // generic def  token=0x6000011  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
  RVA=-1  // generic def  token=0x6000012  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
  RVA=-1  // generic def  token=0x6000013  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
  RVA=-1  // generic def  token=0x6000014  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
  RVA=-1  // generic def  token=0x6000015  System.Void SetTextFormat(TMPro.TMP_Text text, System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
  RVA=0x0B0874FC  token=0x6000016  System.Void SetText(TMPro.TMP_Text text, Cysharp.Text.Utf16ValueStringBuilder stringBuilder)
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200019B
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000DC8  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x063625A4  token=0x6000DC9  System.Void __Gen_Wrap_0()
  RVA=0x063633B4  token=0x6000DCA  System.Void __Gen_Wrap_1(System.Object P0)
  RVA=0x09E3D9AC  token=0x6000DCB  System.Single __Gen_Wrap_2(System.Object P0, System.Boolean P1)
  RVA=0x066C0730  token=0x6000DCC  System.Single __Gen_Wrap_3(System.Object P0)
  RVA=0x06360E84  token=0x6000DCD  System.Int32 __Gen_Wrap_4(System.Object P0)
  RVA=0x06366E40  token=0x6000DCE  Beyond.TickType __Gen_Wrap_5(System.Object P0)
  RVA=0x0B0B2484  token=0x6000DCF  Beyond.UI.LuaPanel __Gen_Wrap_6(System.Object P0)
  RVA=0x066C43C8  token=0x6000DD0  System.Void __Gen_Wrap_7(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x066C288C  token=0x6000DD1  System.Void __Gen_Wrap_8(System.Object P0, System.Single P1)
  RVA=0x0B0B3AA8  token=0x6000DD2  System.String __Gen_Wrap_9(System.Object P0, System.Boolean& P1)
  RVA=0x0B09B4A4  token=0x6000DD3  System.String __Gen_Wrap_10(System.Object P0, System.Int32 P1)
  RVA=0x0B09BB68  token=0x6000DD4  Beyond.UI.UIText.HyperlinkUITextManager __Gen_Wrap_11()
  RVA=0x06366BC4  token=0x6000DD5  System.Boolean __Gen_Wrap_12(System.Object P0, System.Object P1)
  RVA=0x0B09CC78  token=0x6000DD6  Beyond.UI.UIText.HyperlinkUITextGroup __Gen_Wrap_13(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x06377B18  token=0x6000DD7  System.Void __Gen_Wrap_14(System.Object P0, System.Object P1)
  RVA=0x0B09DE30  token=0x6000DD8  System.String __Gen_Wrap_15(Beyond.GEnums.PhoneticType P0, System.Object P1)
  RVA=0x09C81AB0  token=0x6000DD9  System.Void __Gen_Wrap_16(System.Object P0, Beyond.GEnums.PhoneticType P1, System.Object P2)
  RVA=0x0B09F148  token=0x6000DDA  System.String __Gen_Wrap_17(System.Object P0)
  RVA=0x0635FF24  token=0x6000DDB  System.Void __Gen_Wrap_18(Beyond.GEnums.EnvLang P0)
  RVA=0x0B0A0350  token=0x6000DDC  System.Boolean __Gen_Wrap_19(System.Object P0, System.Object P1, System.String& P2)
  RVA=0x0B0A0B28  token=0x6000DDD  System.Boolean __Gen_Wrap_20(System.Object P0, UnityEngine.Vector3 P1, System.String& P2)
  RVA=0x0B0A15B4  token=0x6000DDE  System.Collections.IEnumerator __Gen_Wrap_21(System.Object P0)
  RVA=0x0B0A1EAC  token=0x6000DDF  System.String __Gen_Wrap_22(System.Object P0, System.Boolean P1)
  RVA=0x0B0A2614  token=0x6000DE0  UnityEngine.Sprite __Gen_Wrap_23(System.Object P0, System.Object P1)
  RVA=0x0B0A2F24  token=0x6000DE1  System.Int32 __Gen_Wrap_24(System.ReadOnlySpan<System.Char> P0)
  RVA=0x0B0A369C  token=0x6000DE2  Beyond.UI.UIText.RichTextInfo __Gen_Wrap_25(Beyond.UI.UIText.RichTextAnalyzer.RichTextParam P0, System.Object P1, Cysharp.Text.Utf16ValueStringBuilder& P2)
  RVA=0x0B0A41A4  token=0x6000DE3  Beyond.UI.UIText.RichTextInfo __Gen_Wrap_26(System.Object P0, Beyond.UI.UIText.RichTextAnalyzer.RichTextParam P1, System.Object P2, Cysharp.Text.Utf16ValueStringBuilder& P3)
  RVA=0x0B0A4B40  token=0x6000DE4  Beyond.UI.UIText.RichTextAnalyzer.ProcessRichTextEntryFunc __Gen_Wrap_27(System.Object P0)
  RVA=0x0B0A52EC  token=0x6000DE5  System.Boolean __Gen_Wrap_28(System.ReadOnlySpan<System.Char> P0, System.Boolean& P1, Beyond.UI.UIText.RichTextAnalyzer.RichTextParam& P2)
  RVA=0x0B0A5C5C  token=0x6000DE6  System.Boolean __Gen_Wrap_29(System.ReadOnlySpan<System.Char> P0, Cysharp.Text.Utf16ValueStringBuilder& P1, System.Object P2, Beyond.UI.UIText.RichTextInfo& P3)
  RVA=0x0B0A6700  token=0x6000DE7  System.String __Gen_Wrap_30(System.Object P0, System.Object P1, System.Collections.Generic.List<Beyond.UI.UIText.RichTextInfo>& P2)
  RVA=0x0B0A73B0  token=0x6000DE8  UnityEngine.GameObject __Gen_Wrap_31(System.Object P0)
  RVA=0x0B0A824C  token=0x6000DE9  Beyond.UI.UIText.ImageGOInfo __Gen_Wrap_32(System.Object P0, System.Int32 P1)
  RVA=0x0B0A8EC0  token=0x6000DEA  UnityEngine.Material __Gen_Wrap_33(System.Object P0)
  RVA=0x0B0A9514  token=0x6000DEB  UnityEngine.Material __Gen_Wrap_34(System.Object P0, System.Object P1)
  RVA=0x063623A8  token=0x6000DEC  System.Boolean __Gen_Wrap_35()
  RVA=0x0B0AA978  token=0x6000DED  Beyond.I18n.I18nFontSearchData __Gen_Wrap_36()
  RVA=0x063632E4  token=0x6000DEE  System.Boolean __Gen_Wrap_37(System.Object P0)
  RVA=0x0B0AC144  token=0x6000DEF  TMPro.TMP_FontAsset __Gen_Wrap_38(System.Int32 P0)
  RVA=0x0B0ACD1C  token=0x6000DF0  TMPro.TMP_FontAsset __Gen_Wrap_39(System.Object P0)
  RVA=0x09C808B0  token=0x6000DF1  System.Void __Gen_Wrap_40(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3)
  RVA=0x066F4838  token=0x6000DF2  System.Void __Gen_Wrap_41(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0B0AF3B0  token=0x6000DF3  System.Boolean __Gen_Wrap_42(System.Object P0, System.Int32 P1, System.String& P2)
  RVA=0x0B0B00CC  token=0x6000DF4  System.Boolean __Gen_Wrap_43(System.Object P0, System.Int32 P1, UnityEngine.Vector3& P2, System.Single& P3)
  RVA=0x06364290  token=0x6000DF5  System.Void __Gen_Wrap_44(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x071AD678  token=0x6000DF6  System.Int32 __Gen_Wrap_45(Beyond.UI.UIText.HyperlinkUITextWrap P0, Beyond.UI.UIText.HyperlinkUITextWrap P1)
  RVA=0x0B0B0ED0  token=0x6000DF7  System.Collections.Generic.List<Beyond.UI.UIText> __Gen_Wrap_46(System.Object P0, System.Object P1)
  RVA=0x0B0B0FC8  token=0x6000DF8  System.Collections.Generic.List<Beyond.UI.UIText> __Gen_Wrap_47(System.Object P0)
  RVA=0x0B0B10A4  token=0x6000DF9  TMPro.TMP_SubMeshUI __Gen_Wrap_48(System.Object P0, System.Object P1, TMPro.MaterialReference P2)
  RVA=0x0B0B11F8  token=0x6000DFA  System.String __Gen_Wrap_49(System.Int32 P0)
  RVA=0x0B0B12E4  token=0x6000DFB  UnityEngine.Vector2 __Gen_Wrap_50(System.Object P0, System.Single& P1, UnityEngine.Vector2 P2, System.Boolean P3, System.Boolean P4)
  RVA=0x063621C4  token=0x6000DFC  System.Int32 __Gen_Wrap_51()
  RVA=0x09CB4A30  token=0x6000DFD  System.Single __Gen_Wrap_52()
  RVA=0x0B0B1474  token=0x6000DFE  UnityEngine.GameObject __Gen_Wrap_53(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0B0B1588  token=0x6000DFF  System.Void __Gen_Wrap_54(Beyond.Resource.FAssetProxyUntrackedHandle P0)
  RVA=0x071B1424  token=0x6000E00  System.Int32 __Gen_Wrap_55(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x066F3838  token=0x6000E01  System.Void __Gen_Wrap_56(System.Object P0, System.Int32 P1)
  RVA=0x0B0B1670  token=0x6000E02  System.Boolean __Gen_Wrap_57(UnityEngine.Vector3 P0, System.Object P1, System.Object P2)
  RVA=0x0B0B17B4  token=0x6000E03  System.Boolean __Gen_Wrap_58(UnityEngine.Vector2 P0, System.Object P1, System.Object P2)
  RVA=0x0B0B18F0  token=0x6000E04  UnityEngine.Bounds __Gen_Wrap_59(System.Object P0, System.Object P1)
  RVA=0x0B0B1A10  token=0x6000E05  System.Boolean __Gen_Wrap_60(System.Int32 P0, UnityEngine.InputSystem.EnhancedTouch.Touch& P1)
  RVA=0x0B0B1B58  token=0x6000E06  System.Void __Gen_Wrap_61(System.Object P0, UnityEngine.Color P1)
  RVA=0x0B0B1C6C  token=0x6000E07  UnityEngine.Rect __Gen_Wrap_62(System.Object P0, System.Object P1)
  RVA=0x0B0B1D94  token=0x6000E08  UnityEngine.Vector2 __Gen_Wrap_63(System.Object P0, System.Object P1, UnityEngine.Vector2 P2)
  RVA=0x0B0B1ED0  token=0x6000E09  UnityEngine.Vector3 __Gen_Wrap_64(System.Object P0, System.Object P1)
  RVA=0x0B0B1FF0  token=0x6000E0A  UnityEngine.Color __Gen_Wrap_65(System.Object P0)
  RVA=0x09E20CFC  token=0x6000E0B  System.Single __Gen_Wrap_66(System.Single P0)
  RVA=0x0B0B20FC  token=0x6000E0C  System.Boolean __Gen_Wrap_67(System.Object P0, System.String& P1)
  RVA=0x0B0B2220  token=0x6000E0D  System.Void __Gen_Wrap_68(System.Object P0, System.Object P1, Cinemachine.CinemachineCore.Stage P2, Cinemachine.CameraState& P3, System.Single P4)
  RVA=0x066C13F0  token=0x6000E0E  System.Int32 __Gen_Wrap_69(System.Object P0, System.Int32 P1)
  RVA=0x0B0B2574  token=0x6000E0F  UnityEngine.Vector2 __Gen_Wrap_70(System.Object P0, System.Int32 P1)
  RVA=0x0B0B266C  token=0x6000E10  UnityEngine.Vector2 __Gen_Wrap_71()
  RVA=0x0B0B2724  token=0x6000E11  System.Void __Gen_Wrap_72(UnityEngine.Vector2 P0)
  RVA=0x066C44C0  token=0x6000E12  System.Void __Gen_Wrap_73(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x0B0B280C  token=0x6000E13  UnityEngine.Vector2Int __Gen_Wrap_74(System.Object P0, System.Single P1, System.Single P2, System.Single P3)
  RVA=0x0B0B2948  token=0x6000E14  UnityEngine.Vector2Int __Gen_Wrap_75(System.Object P0, System.Single P1, System.Boolean P2)
  RVA=0x0B0B2A70  token=0x6000E15  UnityEngine.Vector2Int __Gen_Wrap_76(System.Object P0)
  RVA=0x0B0B2B60  token=0x6000E16  System.Void __Gen_Wrap_77(System.Object P0, UnityEngine.Vector2Int P1, UnityEngine.Vector2Int P2)
  RVA=0x0B0B2C90  token=0x6000E17  Beyond.UI.UIScrollList.Cell __Gen_Wrap_78(System.Object P0)
  RVA=0x066C2B40  token=0x6000E18  System.Void __Gen_Wrap_79(System.Object P0, System.Boolean P1)
  RVA=0x0B0B2D80  token=0x6000E19  DG.Tweening.Tween __Gen_Wrap_80(System.Object P0)
  RVA=0x0B0B2E70  token=0x6000E1A  Beyond.UI.UIAnimationTween __Gen_Wrap_81(System.Object P0)
  RVA=0x0B0B2F60  token=0x6000E1B  UnityEngine.AnimationState __Gen_Wrap_82(System.Object P0, System.Object P1)
  RVA=0x071C15C8  token=0x6000E1C  System.Void __Gen_Wrap_83(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x0B0B306C  token=0x6000E1D  System.Void __Gen_Wrap_84(System.Object P0, System.Object P1, System.Single P2, System.Boolean P3)
  RVA=0x0B0B3174  token=0x6000E1E  System.Void __Gen_Wrap_85(System.Object P0, System.Object P1, System.Object P2, Beyond.UI.UIConst.AnimationState P3, System.Single P4, System.Boolean P5, System.Single P6)
  RVA=0x071C0948  token=0x6000E1F  System.Void __Gen_Wrap_86(System.Object P0, System.Single P1, System.Boolean P2)
  RVA=0x0B0B32AC  token=0x6000E20  UnityEngine.AnimationClip __Gen_Wrap_87(System.Object P0)
  RVA=0x071B8250  token=0x6000E21  System.Void __Gen_Wrap_88(System.Object P0, System.Object P1, System.Boolean P2, System.Single P3)
  RVA=0x0B0B339C  token=0x6000E22  System.Void __Gen_Wrap_89(System.Object P0, System.Int32 P1, System.Boolean P2, System.Boolean P3)
  RVA=0x0B0B34A4  token=0x6000E23  System.Void __Gen_Wrap_90(System.Object P0, UnityEngine.Vector2Int P1)
  RVA=0x0B0B35B4  token=0x6000E24  System.Void __Gen_Wrap_91(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3)
  RVA=0x06360E84  token=0x6000E25  Beyond.UI.UIScrollList.MoveTipsType __Gen_Wrap_92(System.Object P0)
  RVA=0x0B0B36BC  token=0x6000E26  System.Void __Gen_Wrap_93(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x0B0B37D0  token=0x6000E27  System.Void __Gen_Wrap_94(System.Object P0, System.Int32 P1, System.Boolean P2, Beyond.UI.UIScrollList.ScrollAlignType P3, System.Boolean P4)
  RVA=0x0B0B38DC  token=0x6000E28  Beyond.UI.UIText __Gen_Wrap_95(System.Object P0)
  RVA=0x0B0B39CC  token=0x6000E29  UnityEngine.Sprite __Gen_Wrap_96(System.Object P0)
  RVA=0x06366E40  token=0x6000E2A  Beyond.UI.UIStyleInputValidTypeMask __Gen_Wrap_97(System.Object P0)
  RVA=0x06A5FB54  token=0x6000E2B  System.Void __Gen_Wrap_98(System.Object P0, Beyond.UI.UIStyleInputValidTypeMask P1)
  RVA=0x06366E40  token=0x6000E2C  Beyond.UI.CustomUIStyle.OverrideValidState __Gen_Wrap_99(System.Object P0)
  RVA=0x06361FA4  token=0x6000E2D  System.Boolean __Gen_Wrap_100(Beyond.UI.UIStyleInputValidType P0)
  RVA=0x06A5FB54  token=0x6000E2E  System.Void __Gen_Wrap_101(System.Object P0, Beyond.UI.CustomUIStyle.OverrideValidState P1)
  RVA=0x063655AC  token=0x6000E2F  System.Boolean __Gen_Wrap_102(Beyond.UI.UIStyleInputValidTypeMask P0)
  RVA=0x066C2400  token=0x6000E30  System.Void __Gen_Wrap_103(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x07199240  token=0x6000E31  System.Void __Gen_Wrap_104(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x06360E84  token=0x6000E32  Beyond.UI.ScrollDirection __Gen_Wrap_105(System.Object P0)
  RVA=0x0B09B164  token=0x6000E33  System.Single __Gen_Wrap_106(System.Object P0, System.Single P1, System.Int32 P2)
  RVA=0x09CBD694  token=0x6000E34  System.Single __Gen_Wrap_107(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x0B09B26C  token=0x6000E35  System.Void __Gen_Wrap_108(System.Object P0, System.Single P1, System.Single P2, System.Object P3, System.Object P4)
  RVA=0x0B09B388  token=0x6000E36  System.Void __Gen_Wrap_109(System.Object P0, System.Single P1, System.Single P2, Beyond.Interpolator.EEaseType P3, System.Object P4)
  RVA=0x071AFE44  token=0x6000E37  Beyond.UI.UILoopScrollRect.MovementDirection __Gen_Wrap_110(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x066D3DA4  token=0x6000E38  System.Single __Gen_Wrap_111(System.Object P0, System.Single P1)
  RVA=0x09CA0D64  token=0x6000E39  System.Int32 __Gen_Wrap_112(System.Object P0, System.Single P1)
  RVA=0x071AFE44  token=0x6000E3A  System.Int32 __Gen_Wrap_113(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0B09B5B0  token=0x6000E3B  System.Void __Gen_Wrap_114(System.Object P0, System.Single P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0B09B6A8  token=0x6000E3C  UnityEngine.UI.Selectable __Gen_Wrap_115(System.Object P0)
  RVA=0x0B09B798  token=0x6000E3D  System.Boolean __Gen_Wrap_116(System.Object P0, UnityEngine.Vector2 P1, System.Object P2)
  RVA=0x0B09B8D4  token=0x6000E3E  System.Collections.Generic.IList<Beyond.UI.UIRecycleLayoutAdapter.IVirtualView> __Gen_Wrap_117(System.Object P0)
  RVA=0x0B09B9B0  token=0x6000E3F  Beyond.UI.UIRecycleLayoutAdapter __Gen_Wrap_118(System.Object P0)
  RVA=0x0B09BA8C  token=0x6000E40  Beyond.UI.UIRecycleLayoutGroup.ViewMgr __Gen_Wrap_119(System.Object P0)
  RVA=0x0B09BC34  token=0x6000E41  Beyond.UI.UIRecycleLayoutGroup.Padding __Gen_Wrap_120(System.Object P0)
  RVA=0x0B09BD2C  token=0x6000E42  UnityEngine.RectTransform __Gen_Wrap_121(System.Object P0)
  RVA=0x0B09BE1C  token=0x6000E43  System.Void __Gen_Wrap_122(System.Object P0, System.Object P1, Beyond.UI.UIRecycleLayoutGroup.LayoutMeta P2)
  RVA=0x0B09BF38  token=0x6000E44  UnityEngine.Vector2 __Gen_Wrap_123(System.Object P0, UnityEngine.Bounds P1)
  RVA=0x0B09C060  token=0x6000E45  UnityEngine.Bounds __Gen_Wrap_124(System.Object P0, Beyond.UI.UIRecycleLayoutGroup.LayoutMeta P1)
  RVA=0x0B09C1A4  token=0x6000E46  Beyond.UI.UIRecycleLayoutAdapter __Gen_Wrap_125(System.Object P0, System.Object P1)
  RVA=0x0B09C29C  token=0x6000E47  Beyond.UI.UIRecycleLayoutGroup.ViewPool __Gen_Wrap_126(System.Object P0, System.Object P1)
  RVA=0x0B09C394  token=0x6000E48  UnityEngine.GameObject __Gen_Wrap_127(System.Object P0, System.Boolean& P1)
  RVA=0x09CAB6E4  token=0x6000E49  System.Boolean __Gen_Wrap_128(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x0B09C4B0  token=0x6000E4A  Beyond.UI.UIRecycleLayoutAdapter.IVirtualView __Gen_Wrap_129(System.Object P0, System.Int32 P1)
  RVA=0x0B09C5A8  token=0x6000E4B  System.Single __Gen_Wrap_130(System.Object P0, System.Int32 P1)
  RVA=0x0B09C680  token=0x6000E4C  UnityEngine.Bounds __Gen_Wrap_131(System.Object P0, System.Int32 P1)
  RVA=0x071AD678  token=0x6000E4D  System.Int32 __Gen_Wrap_132(System.Object P0, System.Object P1)
  RVA=0x06366E40  token=0x6000E4E  Beyond.UI.UIRecycleLayoutGroup.Direction __Gen_Wrap_133(System.Object P0)
  RVA=0x0B09C7A0  token=0x6000E4F  Beyond.UI.UIRecycleMultiHolder.MultiVirtualView __Gen_Wrap_134(System.Object P0, System.Int32 P1)
  RVA=0x0B09C898  token=0x6000E50  System.Void __Gen_Wrap_135(System.Object P0, System.Int32 P1, System.Object P2, System.Single P3)
  RVA=0x0B09C990  token=0x6000E51  Beyond.UI.UIRecycleSingleHolder.SimpleVirtualView __Gen_Wrap_136(System.Object P0, System.Int32 P1)
  RVA=0x0B09CA88  token=0x6000E52  Beyond.UI.ToastList.CellInfo __Gen_Wrap_137(System.Object P0)
  RVA=0x0B09CB64  token=0x6000E53  System.Collections.IEnumerator __Gen_Wrap_138(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x07195DFC  token=0x6000E54  System.Boolean __Gen_Wrap_139(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0B09CDA0  token=0x6000E55  System.Boolean __Gen_Wrap_140(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3, System.Boolean& P4, System.Boolean P5)
  RVA=0x0B09CEF8  token=0x6000E56  Beyond.Input.IBindingView __Gen_Wrap_141(System.Object P0)
  RVA=0x0B09CFE8  token=0x6000E57  Beyond.Input.PlayerActionInfo __Gen_Wrap_142(System.Object P0)
  RVA=0x07196850  token=0x6000E58  System.Boolean __Gen_Wrap_143(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3)
  RVA=0x06301ED4  token=0x6000E59  System.Void __Gen_Wrap_144(Beyond.DeviceInfo.ControllerType P0)
  RVA=0x0B09D0D8  token=0x6000E5A  System.Void __Gen_Wrap_145(System.Object P0, Beyond.EventData<System.String>& P1)
  RVA=0x0B09D1F0  token=0x6000E5B  System.Void __Gen_Wrap_146(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x0B09D2E8  token=0x6000E5C  System.Void __Gen_Wrap_147(System.Object P0, System.Int32 P1, System.Object P2, System.Object P3, System.Boolean P4)
  RVA=0x0B09D3F4  token=0x6000E5D  UnityEngine.AnimationClip __Gen_Wrap_148(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x0B09D508  token=0x6000E5E  UnityEngine.AnimationClip __Gen_Wrap_149(System.Object P0, System.Object P1)
  RVA=0x0B09D600  token=0x6000E5F  System.Void __Gen_Wrap_150(System.Object P0, Beyond.UI.UIAnimationWrapper.FOptions P1)
  RVA=0x066C0800  token=0x6000E60  System.Single __Gen_Wrap_151(System.Object P0, System.Object P1)
  RVA=0x0B09D70C  token=0x6000E61  System.Void __Gen_Wrap_152(System.Object P0, System.Object P1, System.Object P2, Beyond.UI.UIConst.AnimationState P3, System.Single P4, System.Boolean P5)
  RVA=0x0B09D834  token=0x6000E62  Beyond.UI.UIAnimationWrapper.FOptions __Gen_Wrap_153()
  RVA=0x071BE740  token=0x6000E63  System.Void __Gen_Wrap_154(System.Object P0, System.Object P1, System.Object P2, System.Single P3)
  RVA=0x0B09D8EC  token=0x6000E64  Beyond.UI.UISwitchTween.ITweenHandler __Gen_Wrap_155(System.Object P0)
  RVA=0x0B09D9DC  token=0x6000E65  Beyond.UI.UISwitchTween.ITweenHandler __Gen_Wrap_156(System.Object P0, System.Object P1)
  RVA=0x0B09DAE8  token=0x6000E66  Beyond.UI.UISwitchTween.ITweenHandler __Gen_Wrap_157(System.Object P0, System.Boolean P1)
  RVA=0x0B09DBF4  token=0x6000E67  Beyond.UI.UIAnimationSwitchTween __Gen_Wrap_158(Beyond.UI.UIAnimationSwitchTween.Builder& P0)
  RVA=0x0B09DD40  token=0x6000E68  UnityEngine.Camera __Gen_Wrap_159(System.Object P0)
  RVA=0x0B09DF28  token=0x6000E69  System.Void __Gen_Wrap_160(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, System.Boolean P4, System.Boolean P5, System.Boolean P6)
  RVA=0x0B09E04C  token=0x6000E6A  System.Void __Gen_Wrap_161(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3, System.Boolean P4, System.Single P5)
  RVA=0x0B09E164  token=0x6000E6B  System.Collections.IEnumerator __Gen_Wrap_162(System.Object P0, System.Object P1)
  RVA=0x0B09E270  token=0x6000E6C  Beyond.DisposedUnityEvent __Gen_Wrap_163(System.Object P0)
  RVA=0x0B09E34C  token=0x6000E6D  Beyond.DisposedUnityEvent<System.Int32,System.Boolean> __Gen_Wrap_164(System.Object P0)
  RVA=0x0B09E428  token=0x6000E6E  Beyond.UI.UITouchPanel __Gen_Wrap_165(System.Object P0)
  RVA=0x0B09E518  token=0x6000E6F  System.Void __Gen_Wrap_166(System.Object P0, UnityEngine.Vector2& P1, UnityEngine.Vector2& P2)
  RVA=0x0B09E678  token=0x6000E70  UnityEngine.Vector2 __Gen_Wrap_167(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x066C296C  token=0x6000E71  System.Void __Gen_Wrap_168(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B09E7A8  token=0x6000E72  System.Void __Gen_Wrap_169(System.Object P0, UnityEngine.Vector3 P1)
  RVA=0x0B09E8C4  token=0x6000E73  System.Void __Gen_Wrap_170(System.Object P0, System.Boolean P1, System.Single P2, System.Object P3, System.Object P4)
  RVA=0x069ABD7C  token=0x6000E74  System.Void __Gen_Wrap_171(System.Single P0)
  RVA=0x0B09E9E0  token=0x6000E75  System.Void __Gen_Wrap_172(System.Object P0, System.Boolean P1, UnityEngine.UI.Selectable& P2, Beyond.UI.UITouchPanel& P3)
  RVA=0x0B09EB6C  token=0x6000E76  System.Void __Gen_Wrap_173(System.Object P0, System.Single P1, System.Single P2, System.Object P3)
  RVA=0x09C8698C  token=0x6000E77  System.Void __Gen_Wrap_174(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3)
  RVA=0x0B09EC68  token=0x6000E78  Beyond.UI.UIBigRectHelper.BigRectRecoverState __Gen_Wrap_175(System.Object P0)
  RVA=0x0B09ED44  token=0x6000E79  System.Void __Gen_Wrap_176(System.Object P0, System.Single& P1, System.Single& P2)
  RVA=0x0B09EE74  token=0x6000E7A  System.Void __Gen_Wrap_177(System.Object P0, System.Object P1, UnityEngine.Color P2)
  RVA=0x0B09EF90  token=0x6000E7B  UnityEngine.Canvas __Gen_Wrap_178(System.Object P0)
  RVA=0x0B09F06C  token=0x6000E7C  HG.Rendering.Runtime.HGCamera __Gen_Wrap_179(System.Object P0)
  RVA=0x0B09F238  token=0x6000E7D  UnityEngine.UI.RawImage __Gen_Wrap_180(System.Object P0)
  RVA=0x0B09F314  token=0x6000E7E  UnityEngine.Vector2 __Gen_Wrap_181(System.Object P0)
  RVA=0x0B09F404  token=0x6000E7F  Beyond.Input.IBindingGroupTarget __Gen_Wrap_182(System.Object P0)
  RVA=0x0B09F4E0  token=0x6000E80  System.String __Gen_Wrap_183(System.Object P0, System.Object P1)
  RVA=0x06360E84  token=0x6000E81  Beyond.Input.BindingViewState __Gen_Wrap_184(System.Object P0)
  RVA=0x0B09F5EC  token=0x6000E82  System.String __Gen_Wrap_185(System.Object P0, Beyond.Input.BindingViewActionType P1)
  RVA=0x0B09F6F8  token=0x6000E83  System.Boolean __Gen_Wrap_186(System.Object P0, System.String& P1, System.String& P2)
  RVA=0x06366E40  token=0x6000E84  Beyond.Input.ActionOnSetNaviTarget __Gen_Wrap_187(System.Object P0)
  RVA=0x06A5FB54  token=0x6000E85  System.Void __Gen_Wrap_188(System.Object P0, Beyond.Input.ActionOnSetNaviTarget P1)
  RVA=0x0B09F864  token=0x6000E86  System.Collections.Generic.HashSet<UnityEngine.UI.CanvasScaler> __Gen_Wrap_189(System.Object P0)
  RVA=0x0B09F940  token=0x6000E87  System.Single __Gen_Wrap_190(UnityEngine.Vector2& P0)
  RVA=0x0B09FA54  token=0x6000E88  Beyond.UI.UICanvasScaleHelper.FWorldCanvasParams __Gen_Wrap_191(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x0B09FB90  token=0x6000E89  Beyond.UI.UICanvasScaleHelper.FScreenCanvasParams __Gen_Wrap_192()
  RVA=0x0B09FC80  token=0x6000E8A  System.Void __Gen_Wrap_193(System.Object P0, Beyond.UI.UIColorGroup.CommonParams P1)
  RVA=0x0B09FD98  token=0x6000E8B  System.Void __Gen_Wrap_194(System.Object P0, System.Object P1, Beyond.UI.UIColorGroup.CommonParams P2)
  RVA=0x0B09FECC  token=0x6000E8C  System.Void __Gen_Wrap_195(System.Object P0, System.Single P1, System.Single P2, System.Boolean P3)
  RVA=0x0B09FFC8  token=0x6000E8D  System.Void __Gen_Wrap_196(System.Object P0, UnityEngine.Color P1, System.Single P2, System.Boolean P3, System.Boolean P4, System.Boolean P5)
  RVA=0x0B0A0114  token=0x6000E8E  System.Void __Gen_Wrap_197(System.Object P0, UnityEngine.Color P1, System.Single P2, System.Boolean P3, System.Boolean P4)
  RVA=0x06A5FB54  token=0x6000E8F  System.Void __Gen_Wrap_198(System.Object P0, Beyond.Input.GamepadKeyCode P1)
  RVA=0x0B0A0260  token=0x6000E90  Beyond.UI.UICustomLayoutElement __Gen_Wrap_199(System.Object P0)
  RVA=0x06366E40  token=0x6000E91  Beyond.UI.UICustomLayoutElement.LayoutType __Gen_Wrap_200(System.Object P0)
  RVA=0x0B0A04A0  token=0x6000E92  System.Boolean __Gen_Wrap_201(System.Object P0, Beyond.UI.UICustomLayoutElement.LayoutData& P1, System.Boolean P2, System.Int32 P3)
  RVA=0x0B0A05F0  token=0x6000E93  System.Boolean __Gen_Wrap_202(System.Object P0, Beyond.UI.UICustomLayoutElement.LayoutData& P1, System.Int32 P2)
  RVA=0x06A5FB54  token=0x6000E94  System.Void __Gen_Wrap_203(System.Object P0, Beyond.UI.UICustomLayoutElement.LayoutType P1)
  RVA=0x0B0A0730  token=0x6000E95  UnityEngine.CanvasGroup __Gen_Wrap_204(System.Object P0)
  RVA=0x0B0A080C  token=0x6000E96  Beyond.UI.UIDialogText __Gen_Wrap_205(System.Object P0)
  RVA=0x09CB9EA8  token=0x6000E97  System.Void __Gen_Wrap_206(System.Object P0, System.Boolean P1, System.Object P2)
  RVA=0x066E8E30  token=0x6000E98  System.Void __Gen_Wrap_207(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x0B0A08E8  token=0x6000E99  System.Void __Gen_Wrap_208(System.Object P0, System.Int32 P1, System.Boolean P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B0A09FC  token=0x6000E9A  System.Void __Gen_Wrap_209(System.Object P0, UnityEngine.Vector2 P1, System.Boolean P2)
  RVA=0x0B0A0CA4  token=0x6000E9B  System.Void __Gen_Wrap_210(System.Object P0, System.Int32 P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0B0A0DA8  token=0x6000E9C  System.Single __Gen_Wrap_211(System.Object P0, System.Boolean P1, System.Int32 P2)
  RVA=0x0B0A0EAC  token=0x6000E9D  System.Void __Gen_Wrap_212(System.Object P0, System.Boolean P1, System.Single P2, System.Single P3)
  RVA=0x0B0A0FB8  token=0x6000E9E  System.Void __Gen_Wrap_213(System.Object P0, UnityEngine.Rect P1)
  RVA=0x09CBD4CC  token=0x6000E9F  System.Single __Gen_Wrap_214(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x0B0A10CC  token=0x6000EA0  UnityEngine.Vector3 __Gen_Wrap_215(System.Object P0)
  RVA=0x0B0A11E0  token=0x6000EA1  UnityEngine.Transform __Gen_Wrap_216(System.Object P0)
  RVA=0x0B0A12D0  token=0x6000EA2  System.Void __Gen_Wrap_217(System.Object P0, System.Int32 P1, System.Single P2)
  RVA=0x0B0A13C8  token=0x6000EA3  System.Boolean __Gen_Wrap_218(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x0B0A14BC  token=0x6000EA4  UnityEngine.UI.Selectable __Gen_Wrap_219(System.Object P0, System.Boolean P1)
  RVA=0x0B0A16A4  token=0x6000EA5  System.Boolean __Gen_Wrap_220(System.Object P0, System.Int32 P1, System.Int32 P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B0A17B4  token=0x6000EA6  System.Boolean __Gen_Wrap_221(System.Object P0, UnityEngine.UI.NaviDirection P1, System.Boolean P2)
  RVA=0x06A5FB54  token=0x6000EA7  System.Void __Gen_Wrap_222(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x069AB898  token=0x6000EA8  System.Boolean __Gen_Wrap_223(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x0B0A18A8  token=0x6000EA9  System.Boolean __Gen_Wrap_224(System.Object P0, System.Int32 P1, System.Int32& P2, System.Boolean& P3, System.Int32& P4)
  RVA=0x0B0A1A24  token=0x6000EAA  System.Boolean __Gen_Wrap_225(System.Object P0, System.Int32 P1, System.Int32& P2, System.Int32& P3)
  RVA=0x06A60048  token=0x6000EAB  System.Void __Gen_Wrap_226(System.Object P0, System.Int32 P1, System.Int32& P2, System.Int32& P3)
  RVA=0x0B0A1B70  token=0x6000EAC  UnityEngine.Vector2Int __Gen_Wrap_227(System.Object P0, UnityEngine.Vector2Int P1)
  RVA=0x0B0A1C90  token=0x6000EAD  UnityEngine.Vector2Int __Gen_Wrap_228(System.Object P0, UnityEngine.Vector2Int P1, UnityEngine.Vector2Int P2)
  RVA=0x0B0A1DD0  token=0x6000EAE  System.ValueTuple<System.Single,System.Single> __Gen_Wrap_229(System.Object P0)
  RVA=0x0B0A1FB8  token=0x6000EAF  System.Int32 __Gen_Wrap_230(System.Object P0, System.Boolean P1)
  RVA=0x0B0A20A4  token=0x6000EB0  UnityEngine.GameObject __Gen_Wrap_231(System.Object P0, System.Int32 P1)
  RVA=0x0B0A219C  token=0x6000EB1  Beyond.UI.UIScrollList.Cell __Gen_Wrap_232(System.Object P0, System.Int32 P1)
  RVA=0x071BF140  token=0x6000EB2  System.Single __Gen_Wrap_233(System.Object P0, System.Int32 P1, Beyond.UI.UIScrollList.ScrollAlignType P2)
  RVA=0x0B0A2294  token=0x6000EB3  System.Void __Gen_Wrap_234(System.Object P0, System.Int32 P1, System.Boolean P2, Beyond.UI.UIScrollList.ScrollAlignType P3)
  RVA=0x071AB448  token=0x6000EB4  System.Int32 __Gen_Wrap_235(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x071AFE44  token=0x6000EB5  System.Int32 __Gen_Wrap_236(System.Object P0, System.Int32 P1, UnityEngine.UI.NaviDirection P2)
  RVA=0x0B0A239C  token=0x6000EB6  System.Boolean __Gen_Wrap_237(System.Object P0, UnityEngine.UI.NaviDirection P1, System.Boolean& P2)
  RVA=0x0B0A24B4  token=0x6000EB7  System.Boolean __Gen_Wrap_238(System.Object P0, System.Int32& P1, Beyond.UI.UIScrollList.Cell& P2)
  RVA=0x0AC41DA8  token=0x6000EB8  System.Boolean __Gen_Wrap_239(System.Object P0, System.Object P1, UnityEngine.UI.NaviDirection P2)
  RVA=0x0B0A2720  token=0x6000EB9  System.Boolean __Gen_Wrap_240(System.Object P0, UnityEngine.UI.NaviDirection P1, System.Int32 P2, System.Boolean P3, System.Int32& P4)
  RVA=0x0B0A2858  token=0x6000EBA  System.Boolean __Gen_Wrap_241(System.Object P0, System.Object P1, System.Boolean P2, UnityEngine.UI.Selectable& P3)
  RVA=0x0B0A17B4  token=0x6000EBB  System.Boolean __Gen_Wrap_242(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x0B0A29A8  token=0x6000EBC  System.Boolean __Gen_Wrap_243(System.Object P0, System.Int32& P1, System.Boolean P2)
  RVA=0x0B0A2AC0  token=0x6000EBD  System.Void __Gen_Wrap_244(System.Object P0, System.Int32 P1, System.Int32 P2, System.Boolean P3, System.Boolean P4, System.Boolean P5, Beyond.UI.UIScrollList.ScrollAlignType P6)
  RVA=0x0B0A2BF4  token=0x6000EBE  Beyond.UI.UISelectableNaviGroup __Gen_Wrap_245(System.Object P0)
  RVA=0x0B0A2CE4  token=0x6000EBF  System.Void __Gen_Wrap_246(System.Object P0, System.Int32 P1, System.Boolean P2, System.Boolean P3, System.Boolean P4, System.Boolean P5)
  RVA=0x09CCA508  token=0x6000EC0  System.Void __Gen_Wrap_247(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0B0A2E08  token=0x6000EC1  System.Void __Gen_Wrap_248(System.Object P0, System.Int32 P1, System.Nullable<System.Int32> P2)
  RVA=0x069AB898  token=0x6000EC2  System.Boolean __Gen_Wrap_249(System.Object P0, System.Int32 P1)
  RVA=0x09CD9300  token=0x6000EC3  System.Int32 __Gen_Wrap_250(System.Object P0, System.Int32 P1, System.Int32 P2, Beyond.UI.UIScrollList.ScrollAlignType P3)
  RVA=0x0B0A302C  token=0x6000EC4  System.Void __Gen_Wrap_251(System.Object P0, System.Int32 P1, System.Int32 P2, System.Boolean P3, Beyond.UI.UIScrollList.ScrollAlignType P4, System.Boolean P5)
  RVA=0x0B0A3140  token=0x6000EC5  UnityEngine.Vector2Int __Gen_Wrap_252(System.Object P0, System.Single P1)
  RVA=0x09C9DD08  token=0x6000EC6  System.Boolean __Gen_Wrap_253(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3)
  RVA=0x09CBA2C4  token=0x6000EC7  System.Void __Gen_Wrap_254(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0B0A323C  token=0x6000EC8  System.Void __Gen_Wrap_255(System.Object P0, UnityEngine.Vector2Int P1, System.Boolean P2, System.Boolean P3)
  RVA=0x0B0A3358  token=0x6000EC9  System.Void __Gen_Wrap_256(System.Object P0, System.Object P1, System.Boolean P2, System.Int32 P3, System.Int32 P4)
  RVA=0x0B0A3464  token=0x6000ECA  System.Void __Gen_Wrap_257(System.Object P0, System.Boolean P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Boolean P5, System.Boolean P6)
  RVA=0x09CD9300  token=0x6000ECB  System.Int32 __Gen_Wrap_258(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0B0A3588  token=0x6000ECC  UnityEngine.GameObject __Gen_Wrap_259(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0B0A385C  token=0x6000ECD  UnityEngine.Vector3 __Gen_Wrap_260()
  RVA=0x0B0A393C  token=0x6000ECE  System.Void __Gen_Wrap_261(UnityEngine.Vector3 P0)
  RVA=0x0B0A3A28  token=0x6000ECF  Beyond.UI.UIAtlasManager.UIAtlasHandle& __Gen_Wrap_262(System.Object P0)
  RVA=0x0B0A3B18  token=0x6000ED0  Beyond.UI.UIAtlasManager.UIAtlasHandle __Gen_Wrap_263(Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCause P0)
  RVA=0x0B0A3C30  token=0x6000ED1  UnityEngine.Texture __Gen_Wrap_264(System.Object P0)
  RVA=0x0B0A3D20  token=0x6000ED2  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> __Gen_Wrap_265(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x0B0A3E38  token=0x6000ED3  Beyond.UI.UIImage __Gen_Wrap_266(System.Object P0)
  RVA=0x0B0A3F14  token=0x6000ED4  System.Boolean __Gen_Wrap_267(Beyond.UI.UIInertiaViewPager.ScrollEffectConfig& P0)
  RVA=0x071AD678  token=0x6000ED5  Beyond.UI.UIInertiaViewPager.State __Gen_Wrap_268(System.Object P0, System.Object P1)
  RVA=0x0B0A4060  token=0x6000ED6  System.Boolean __Gen_Wrap_269(Beyond.UI.UIInertiaViewPager.InertiaBlocker& P0, System.Single P1)
  RVA=0x0B0A4364  token=0x6000ED7  System.Boolean __Gen_Wrap_270(System.Object P0, System.Single P1, System.Single& P2)
  RVA=0x0A9C157C  token=0x6000ED8  System.Void __Gen_Wrap_271(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x09C830FC  token=0x6000ED9  System.Void __Gen_Wrap_272(System.Object P0, Beyond.UI.UIInertiaViewPager.State P1, Beyond.UI.UIInertiaViewPager.State P2)
  RVA=0x066F4C88  token=0x6000EDA  System.Void __Gen_Wrap_273(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x0B0A4480  token=0x6000EDB  System.Single __Gen_Wrap_274(Beyond.UI.UIInertiaViewPager.EmptyScrollHandler& P0)
  RVA=0x0B0A45A4  token=0x6000EDC  System.Boolean __Gen_Wrap_275(Beyond.UI.UIInertiaViewPager.EmptyScrollHandler& P0)
  RVA=0x0B0A46C8  token=0x6000EDD  System.Void __Gen_Wrap_276(Beyond.UI.UIInertiaViewPager.EmptyScrollHandler& P0, System.Single P1)
  RVA=0x0B0A47F8  token=0x6000EDE  System.Void __Gen_Wrap_277(Beyond.UI.UIInertiaViewPager.EmptyScrollHandler& P0)
  RVA=0x0B0A491C  token=0x6000EDF  Beyond.UI.UIInputField __Gen_Wrap_278()
  RVA=0x0B0A49E8  token=0x6000EE0  System.Void __Gen_Wrap_279(System.Object P0, System.Boolean P1, UnityEngine.Vector2 P2, UnityEngine.Vector2 P3)
  RVA=0x066F3838  token=0x6000EE1  System.Void __Gen_Wrap_280(System.Object P0, UnityEngine.UI.CanvasUpdate P1)
  RVA=0x06373F5C  token=0x6000EE2  System.Void __Gen_Wrap_281(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x0B0A4C30  token=0x6000EE3  System.Void __Gen_Wrap_282(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B0A4D3C  token=0x6000EE4  System.Collections.Generic.List<System.Int32> __Gen_Wrap_283(System.Object P0)
  RVA=0x0B0A4E18  token=0x6000EE5  System.Collections.Generic.List<System.Boolean> __Gen_Wrap_284(System.Object P0)
  RVA=0x0B0A4EF4  token=0x6000EE6  System.Void __Gen_Wrap_285(System.Object P0, System.Int32 P1, System.Object P2, System.Boolean P3)
  RVA=0x066C46D4  token=0x6000EE7  System.Void __Gen_Wrap_286(System.Object P0, System.Single P1, System.Single P2, System.Single P3)
  RVA=0x0B0A4FEC  token=0x6000EE8  UnityEngine.MaterialPropertyBlock __Gen_Wrap_287()
  RVA=0x0B0A50A4  token=0x6000EE9  System.Boolean __Gen_Wrap_288(System.Object P0, UnityEngine.Vector3 P1)
  RVA=0x0B0A51CC  token=0x6000EEA  System.Single __Gen_Wrap_289(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x0B0A5494  token=0x6000EEB  System.Boolean __Gen_Wrap_290(System.Object P0, System.Single P1)
  RVA=0x0B0A5584  token=0x6000EEC  Beyond.UI.UIScrollList.Padding __Gen_Wrap_291(System.Object P0, System.Boolean P1)
  RVA=0x0B0A569C  token=0x6000EED  System.Void __Gen_Wrap_292(System.Object P0, System.Int32& P1, System.Int32& P2)
  RVA=0x0B0A57C4  token=0x6000EEE  System.Int32 __Gen_Wrap_293(System.Object P0, UnityEngine.Vector2 P1, UnityEngine.UI.NaviDirection P2)
  RVA=0x0B0A58EC  token=0x6000EEF  System.Void __Gen_Wrap_294(System.Object P0, UnityEngine.Vector2 P1, System.Int32& P2, System.Int32& P3)
  RVA=0x06366E40  token=0x6000EF0  Beyond.UI.UIScrollRect.MoveTipsType __Gen_Wrap_295(System.Object P0)
  RVA=0x09C808B0  token=0x6000EF1  System.Void __Gen_Wrap_296(System.Object P0, System.Object P1, System.Object P2, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState P3)
  RVA=0x06366E40  token=0x6000EF2  Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState __Gen_Wrap_297(System.Object P0)
  RVA=0x0B0A5A5C  token=0x6000EF3  UnityEngine.Rect __Gen_Wrap_298(System.Object P0)
  RVA=0x0B0A5B54  token=0x6000EF4  System.Boolean __Gen_Wrap_299(System.Object P0, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState P1, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotDir P2, System.Object P3)
  RVA=0x0B0A569C  token=0x6000EF5  System.Void __Gen_Wrap_300(System.Object P0, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState& P1, Beyond.UI.UIScrollRectEdgeRedDot.ERedDotState& P2)
  RVA=0x066C2E48  token=0x6000EF6  System.Void __Gen_Wrap_301(System.Object P0, System.Boolean P1, System.Single P2)
  RVA=0x0B0A5E4C  token=0x6000EF7  UnityEngine.Vector3 __Gen_Wrap_302(System.Object P0, System.Int32 P1)
  RVA=0x0B0A5F68  token=0x6000EF8  UnityEngine.Vector3[] __Gen_Wrap_303(System.Object P0)
  RVA=0x0B0A6058  token=0x6000EF9  UnityEngine.Vector4 __Gen_Wrap_304(System.Object P0)
  RVA=0x0B0A6164  token=0x6000EFA  UnityEngine.Vector4 __Gen_Wrap_305(UnityEngine.Vector4 P0, UnityEngine.Rect P1, UnityEngine.Rect P2)
  RVA=0x0B0A6300  token=0x6000EFB  Beyond.UI.UISoftMask.SlicedSpriteParams __Gen_Wrap_306(System.Object P0)
  RVA=0x0B0A6418  token=0x6000EFC  UnityEngine.UI.Graphic __Gen_Wrap_307(System.Object P0)
  RVA=0x0B0A6508  token=0x6000EFD  Beyond.UI.UISoftMask __Gen_Wrap_308(System.Object P0)
  RVA=0x0B0A65F8  token=0x6000EFE  System.Boolean __Gen_Wrap_309(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x0B0A6864  token=0x6000EFF  UnityEngine.Vector2 __Gen_Wrap_310(System.Object P0, UnityEngine.Vector2 P1, UnityEngine.Vector2 P2, UnityEngine.Vector2 P3, UnityEngine.Vector2 P4)
  RVA=0x0B0A6A18  token=0x6000F00  System.Void __Gen_Wrap_311(System.Object P0, UnityEngine.Vector2 P1, UnityEngine.Vector2 P2, UnityEngine.Vector2 P3, UnityEngine.Vector2 P4, System.Single& P5, System.Single& P6)
  RVA=0x0B0A6C1C  token=0x6000F01  System.Void __Gen_Wrap_312(UnityEngine.UIVertex& P0, UnityEngine.Vector2 P1, UnityEngine.Vector2 P2, UnityEngine.Vector2 P3)
  RVA=0x0B0A6E6C  token=0x6000F02  System.Void __Gen_Wrap_313(System.Object P0, System.Object P1, System.Object P2, System.Boolean& P3)
  RVA=0x0B0A6F98  token=0x6000F03  Beyond.UI.UISoftMaskable __Gen_Wrap_314(System.Object P0)
  RVA=0x0B0A7088  token=0x6000F04  System.Collections.IEnumerator __Gen_Wrap_315(System.Object P0, System.Boolean P1, System.Int32 P2)
  RVA=0x09CBA994  token=0x6000F05  System.Void __Gen_Wrap_316(System.Object P0, System.Boolean P1, System.Int32 P2)
  RVA=0x0B0A719C  token=0x6000F06  System.Void __Gen_Wrap_317(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3, System.Object P4, System.Object P5, System.Boolean P6)
  RVA=0x06366E40  token=0x6000F07  System.UInt32 __Gen_Wrap_318(System.Object P0)
  RVA=0x0B0A72D4  token=0x6000F08  System.Void __Gen_Wrap_319(System.UInt32 P0, System.Boolean P1)
  RVA=0x0B0A74A0  token=0x6000F09  TMPro.TMP_FontAsset __Gen_Wrap_320(System.Object P0, System.Object P1)
  RVA=0x0B0A75AC  token=0x6000F0A  System.Boolean __Gen_Wrap_321(System.Object P0, System.Object P1, System.UInt32 P2, TMPro.TMP_FontAsset& P3)
  RVA=0x0B0A770C  token=0x6000F0B  System.Boolean __Gen_Wrap_322(System.Single& P0, System.Single P1)
  RVA=0x0B0A7820  token=0x6000F0C  System.Void __Gen_Wrap_323(Beyond.UI.TweenUtils.FLerpByTime& P0, System.Object P1)
  RVA=0x0B0A7984  token=0x6000F0D  System.Single __Gen_Wrap_324(Beyond.UI.TweenUtils.FLerpByTime& P0, System.Boolean& P1)
  RVA=0x0B0A7B1C  token=0x6000F0E  UnityEngine.Playables.PlayableGraph __Gen_Wrap_325(System.Object P0)
  RVA=0x0B0A7C28  token=0x6000F0F  UnityEngine.Animations.AnimationLayerMixerPlayable __Gen_Wrap_326(System.Object P0, UnityEngine.Playables.PlayableGraph P1, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FMixerOptions<UnityEngine.AnimationClip>& P2)
  RVA=0x0B0A7DBC  token=0x6000F10  UnityEngine.Animations.AnimationPlayableOutput __Gen_Wrap_327(System.Object P0, UnityEngine.Playables.PlayableGraph P1)
  RVA=0x0B0A7F08  token=0x6000F11  UnityEngine.Animations.AnimationClipPlayable __Gen_Wrap_328(System.Object P0, System.Object P1, UnityEngine.Playables.PlayableGraph P2, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FPlayableMeta<UnityEngine.AnimationClip>& P3)
  RVA=0x0B0A80B8  token=0x6000F12  UnityEngine.Animations.AnimationMixerPlayable __Gen_Wrap_329(System.Object P0, UnityEngine.Playables.PlayableGraph P1, Beyond.Playables.GenericShareSingleMixerAssetPlayer.FMixerOptions<UnityEngine.AnimationClip>& P2)
  RVA=0x0B0A837C  token=0x6000F13  System.Void __Gen_Wrap_330(System.Boolean P0, Beyond.Resource.FAssetProxyHandle P1)
  RVA=0x0B0A8480  token=0x6000F14  System.Void __Gen_Wrap_331(Beyond.UI.UISwitchTween.TweenContext& P0)
  RVA=0x0B0A85A8  token=0x6000F15  System.Void __Gen_Wrap_332(Beyond.UI.UISwitchTween.TweenContext& P0, System.Object P1)
  RVA=0x0B0A86EC  token=0x6000F16  System.Void __Gen_Wrap_333(System.Object P0, Beyond.UI.UISwitchTween.Options P1)
  RVA=0x0B0A87EC  token=0x6000F17  Beyond.UI.UISwitchTween.TweenContext __Gen_Wrap_334(System.Object P0)
  RVA=0x0B0A88C8  token=0x6000F18  System.Boolean __Gen_Wrap_335(Beyond.UI.UISwitchTween.TweenContext& P0)
  RVA=0x0B0A89F0  token=0x6000F19  System.Single __Gen_Wrap_336(Beyond.UI.FadeSwitchTween.Durations& P0, System.Single P1)
  RVA=0x0B0A8B50  token=0x6000F1A  Beyond.UI.FadeSwitchTween __Gen_Wrap_337(Beyond.UI.FadeSwitchTween.Builder& P0)
  RVA=0x0B0A8CB8  token=0x6000F1B  UnityEngine.Vector2 __Gen_Wrap_338(System.Object P0, System.Boolean P1)
  RVA=0x0B0A8DB0  token=0x6000F1C  System.Void __Gen_Wrap_339(System.Object P0, UnityEngine.RectInt& P1)
  RVA=0x09E3E3C4  token=0x6000F1D  System.Boolean __Gen_Wrap_340(System.Object P0, System.Object P1, Beyond.UI.UIAtlasManager.UIAtlasProcessFailureCause& P2)
  RVA=0x0B0A8FB0  token=0x6000F1E  UnityEngine.RectInt __Gen_Wrap_341(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0B0A90D4  token=0x6000F1F  System.Void __Gen_Wrap_342(System.Object P0, System.Object P1, System.Object P2, UnityEngine.RectInt& P3)
  RVA=0x0B0A9214  token=0x6000F20  System.Boolean __Gen_Wrap_343(Beyond.UI.UIAtlasManager.UIAtlasHandle& P0)
  RVA=0x0B0A9360  token=0x6000F21  System.Void __Gen_Wrap_344(System.Object P0, System.Collections.Generic.List<UnityEngine.RectInt>& P1)
  RVA=0x06365664  token=0x6000F22  System.Void __Gen_Wrap_345(System.Int32 P0, System.String P1)
  RVA=0x066C296C  token=0x6000F23  System.Void __Gen_Wrap_346(System.String P0)
  RVA=0x066C53B4  token=0x6000F24  System.Boolean __Gen_Wrap_347(Beyond.UI.UIState.StateAnimationInfo P0)
  RVA=0x06363EE4  token=0x6000F25  System.Byte __Gen_Wrap_348(UnityEngine.WrapMode P0)
  RVA=0x0B0A943C  token=0x6000F26  System.Byte __Gen_Wrap_349(System.Int32 P0, System.Boolean P1)
  RVA=0x0B0A9620  token=0x6000F27  Beyond.UI.GPUI.GPUIExtendedPrefabData __Gen_Wrap_350(UnityEngine.Vector4 P0, UnityEngine.Vector4 P1, UnityEngine.Vector2 P2)
  RVA=0x0B0A97E0  token=0x6000F28  Beyond.UI.GPUI.GPUIExtendedPrefabDataLite __Gen_Wrap_351(UnityEngine.Vector4 P0, UnityEngine.Vector4 P1, UnityEngine.Vector2 P2)
  RVA=0x0B0A9978  token=0x6000F29  Beyond.UI.GPUI.GPUIRootTransformState __Gen_Wrap_352()
  RVA=0x0B0A9A74  token=0x6000F2A  Beyond.UI.GPUI.GPUISubrootDomainState __Gen_Wrap_353()
  RVA=0x0B0A9B68  token=0x6000F2B  Beyond.UI.GPUI.GPUIExtendedAnimationState __Gen_Wrap_354()
  RVA=0x0B0A9C58  token=0x6000F2C  Beyond.UI.GPUI.GPUIResolvedSubrootState __Gen_Wrap_355(Beyond.UI.GPUI.GPUIRootTransformState& P0, Beyond.UI.GPUI.GPUISubrootDomainState& P1)
  RVA=0x0B0A9E1C  token=0x6000F2D  System.UInt32 __Gen_Wrap_356(System.UInt16 P0, System.UInt16 P1)
  RVA=0x0B0A9F08  token=0x6000F2E  System.UInt32 __Gen_Wrap_357(System.UInt16 P0, System.Byte P1, System.Byte P2)
  RVA=0x0B0A9FFC  token=0x6000F2F  System.UInt32 __Gen_Wrap_358(System.Single P0)
  RVA=0x0AC4228C  token=0x6000F30  System.Int32 __Gen_Wrap_359(System.Byte P0)
  RVA=0x0B0A9FFC  token=0x6000F31  System.Int32 __Gen_Wrap_360(System.Single P0)
  RVA=0x0B0A9FFC  token=0x6000F32  System.Int16 __Gen_Wrap_361(System.Single P0)
  RVA=0x0B0AA0CC  token=0x6000F33  System.Void __Gen_Wrap_362(UnityEngine.Quaternion P0, System.Int32& P1, System.Int32& P2)
  RVA=0x0B0AA23C  token=0x6000F34  Beyond.UI.GPUI.GPUPrefabData __Gen_Wrap_363(UnityEngine.Vector4 P0, UnityEngine.Vector4 P1, UnityEngine.Vector2 P2)
  RVA=0x0B0AA3F4  token=0x6000F35  Beyond.UI.GPUI.GPUPrefabDataLite __Gen_Wrap_364(UnityEngine.Vector4 P0, UnityEngine.Vector4 P1, UnityEngine.Vector2 P2)
  RVA=0x06A18890  token=0x6000F36  System.UInt32 __Gen_Wrap_365(System.UInt32 P0, System.UInt32 P1)
  RVA=0x0B0AA58C  token=0x6000F37  UnityEngine.Texture2D __Gen_Wrap_366(System.Object P0)
  RVA=0x0B0AA67C  token=0x6000F38  UnityEngine.RectInt __Gen_Wrap_367(System.Object P0)
  RVA=0x0B0AA774  token=0x6000F39  System.Boolean __Gen_Wrap_368(System.Object P0, System.Object P1, UnityEngine.RectInt P2)
  RVA=0x0B0AA8A0  token=0x6000F3A  System.Boolean __Gen_Wrap_369(System.Object P0, UnityEngine.UICustomAtlasCopyStatus P1)
  RVA=0x06A5FB54  token=0x6000F3B  System.Void __Gen_Wrap_370(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCause P1)
  RVA=0x0B0AAA44  token=0x6000F3C  System.Void __Gen_Wrap_371(System.Object P0, System.Object P1, UnityEngine.RectInt P2)
  RVA=0x0B0AAB60  token=0x6000F3D  Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle __Gen_Wrap_372()
  RVA=0x09E3E3C4  token=0x6000F3E  System.Boolean __Gen_Wrap_373(System.Object P0, System.Object P1, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicAtlasFailureCause& P2)
  RVA=0x0B0AAC38  token=0x6000F3F  System.Boolean __Gen_Wrap_374(System.Object P0, System.Object P1, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle& P2, UnityEngine.Vector4& P3)
  RVA=0x0B0AADD0  token=0x6000F40  System.Boolean __Gen_Wrap_375(Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle& P0)
  RVA=0x0B0AAF1C  token=0x6000F41  System.Boolean __Gen_Wrap_376(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P1, System.Boolean& P2)
  RVA=0x0B0AB078  token=0x6000F42  System.Boolean __Gen_Wrap_377(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P1)
  RVA=0x06360E84  token=0x6000F43  UnityEngine.UICustomAtlasCopyStatus __Gen_Wrap_378(System.Object P0)
  RVA=0x0B0AB188  token=0x6000F44  System.Void __Gen_Wrap_379(System.Object P0, System.Object P1, UnityEngine.Vector4 P2)
  RVA=0x0B0AB2A4  token=0x6000F45  System.Void __Gen_Wrap_380(Beyond.UI.GPUI.GPUIExtendedPrefabDataLite& P0, UnityEngine.Vector4 P1, UnityEngine.Vector2 P2)
  RVA=0x0B0AB474  token=0x6000F46  System.Boolean __Gen_Wrap_381(System.Object P0, System.Int32 P1, System.Object P2, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& P3, System.UInt16& P4)
  RVA=0x0B0AB620  token=0x6000F47  System.Void __Gen_Wrap_382(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey P1)
  RVA=0x0B0AB73C  token=0x6000F48  System.Boolean __Gen_Wrap_383(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& P0, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey P1)
  RVA=0x0B0AB8D8  token=0x6000F49  System.Boolean __Gen_Wrap_384(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& P0, System.Object P1)
  RVA=0x0B0ABA40  token=0x6000F4A  System.Int32 __Gen_Wrap_385(Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& P0)
  RVA=0x0B0ABB9C  token=0x6000F4B  System.Boolean __Gen_Wrap_386(System.Object P0, System.Object P1, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle& P2)
  RVA=0x0B0ABCE8  token=0x6000F4C  System.Boolean __Gen_Wrap_387(System.Object P0, System.Int32 P1, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P2, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey& P3, System.UInt16& P4)
  RVA=0x0B0ABEB8  token=0x6000F4D  System.Boolean __Gen_Wrap_388(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicPrefabNodeManager.DynamicPrefabNodeKey P1, System.Int32 P2, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P3)
  RVA=0x0B0AC030  token=0x6000F4E  System.Void __Gen_Wrap_389(System.Object P0, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P1)
  RVA=0x0B0AC230  token=0x6000F4F  Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData __Gen_Wrap_390(System.Object P0)
  RVA=0x0B0AC320  token=0x6000F50  Beyond.UI.GPUI.GPUIExtendedRuntimeAnimationData __Gen_Wrap_391(System.Object P0, System.Int32 P1)
  RVA=0x0B0AC42C  token=0x6000F51  System.Boolean __Gen_Wrap_392(Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& P0)
  RVA=0x0B0AC560  token=0x6000F52  System.Void __Gen_Wrap_393(System.Object P0, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& P1)
  RVA=0x0B0AC69C  token=0x6000F53  System.Void __Gen_Wrap_394(System.Object P0, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& P1, System.Int32 P2, System.Int32& P3, Beyond.UI.GPUI.GPUIExtendedInstanceData P4)
  RVA=0x0B0AC878  token=0x6000F54  System.Void __Gen_Wrap_395(System.Object P0, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& P1, System.Int32 P2, System.Int32& P3)
  RVA=0x0B0ACA18  token=0x6000F55  Beyond.UI.GPUI.GPUIExtendedAnimationState __Gen_Wrap_396(System.Object P0, Beyond.UI.GPUI.GPUIExtendedRuntimeNodeInfo& P1)
  RVA=0x071B0CA0  token=0x6000F56  System.UInt32 __Gen_Wrap_397(System.UInt32 P0)
  RVA=0x0B0ACBA8  token=0x6000F57  System.Void __Gen_Wrap_398(System.Object P0, System.Int32 P1, Beyond.UI.GPUI.GPUIExtendedInstanceData& P2)
  RVA=0x0B0A9FFC  token=0x6000F58  System.Byte __Gen_Wrap_399(System.Single P0)
  RVA=0x0B0ACE0C  token=0x6000F59  System.Void __Gen_Wrap_400(System.Object P0, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUIExtendedInstanceData>& P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0B0ACF84  token=0x6000F5A  System.Void __Gen_Wrap_401(System.Object P0, UnityEngine.Quaternion P1)
  RVA=0x0636104C  token=0x6000F5B  System.Void __Gen_Wrap_402(System.Object P0, System.Single P1, System.Int32 P2)
  RVA=0x0B0AD098  token=0x6000F5C  System.Boolean __Gen_Wrap_403(System.Object P0, System.Int32 P1, System.Single P2, System.Int32 P3)
  RVA=0x0B0AD1A0  token=0x6000F5D  System.UInt32 __Gen_Wrap_404(UnityEngine.Color P0)
  RVA=0x0B0AD2A8  token=0x6000F5E  System.Void __Gen_Wrap_405(System.Object P0, System.Int32 P1, UnityEngine.Color P2)
  RVA=0x0B0AD3D4  token=0x6000F5F  System.Void __Gen_Wrap_406(System.Object P0, System.Int32 P1, UnityEngine.Vector2 P2, UnityEngine.Vector2 P3, System.Single P4)
  RVA=0x0B0AD528  token=0x6000F60  System.Boolean __Gen_Wrap_407(System.Object P0, System.Int32 P1, System.Int32& P2)
  RVA=0x0B0AD640  token=0x6000F61  System.Boolean __Gen_Wrap_408(System.Object P0, System.Int32 P1, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& P2)
  RVA=0x0B0AD7E4  token=0x6000F62  System.Boolean __Gen_Wrap_409(System.Object P0, System.Int32 P1, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& P2, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance.SpriteOverrideState& P3)
  RVA=0x0B0ADA20  token=0x6000F63  System.Boolean __Gen_Wrap_410(System.Object P0, System.Int32 P1, Beyond.UI.GPUI.GPUIExtendedDynamicAtlas.DynamicSpriteHandle P2)
  RVA=0x0B0ADB48  token=0x6000F64  Beyond.UI.GPUI.GPUIHandle __Gen_Wrap_411(System.Object P0)
  RVA=0x0B0ADC38  token=0x6000F65  System.Boolean __Gen_Wrap_412(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, Beyond.UI.GPUI.GPUIHandle& P3, System.Int32 P4)
  RVA=0x0B0ADDD0  token=0x6000F66  System.Boolean __Gen_Wrap_413(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1)
  RVA=0x0B0ADEEC  token=0x6000F67  System.Boolean __Gen_Wrap_414(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, UnityEngine.Vector3 P2)
  RVA=0x0B0AE044  token=0x6000F68  System.Boolean __Gen_Wrap_415(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, UnityEngine.Vector2 P2)
  RVA=0x0B0AE1A0  token=0x6000F69  System.Boolean __Gen_Wrap_416(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, UnityEngine.Quaternion P2)
  RVA=0x0B0AE2F0  token=0x6000F6A  System.Boolean __Gen_Wrap_417(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, Beyond.UI.GPUI.GPUIExtendedRuntimeInstance& P3, System.Int32& P4)
  RVA=0x0B0AE498  token=0x6000F6B  System.Boolean __Gen_Wrap_418(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, System.Boolean P3)
  RVA=0x0B0AE5D4  token=0x6000F6C  System.Boolean __Gen_Wrap_419(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, UnityEngine.Vector2 P3, UnityEngine.Vector2 P4, System.Single P5)
  RVA=0x0B0AE75C  token=0x6000F6D  System.Boolean __Gen_Wrap_420(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, UnityEngine.Color P3)
  RVA=0x0B0AE8C0  token=0x6000F6E  System.Boolean __Gen_Wrap_421(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, System.Single P3)
  RVA=0x0B0AE9FC  token=0x6000F6F  System.Boolean __Gen_Wrap_422(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, System.Object P3)
  RVA=0x0B0AEB38  token=0x6000F70  System.Boolean __Gen_Wrap_423(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Boolean P2, System.Int32 P3)
  RVA=0x0B0AEC74  token=0x6000F71  System.Boolean __Gen_Wrap_424(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Boolean P2, System.Object P3)
  RVA=0x0B0AEDB0  token=0x6000F72  System.Boolean __Gen_Wrap_425(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, System.Single& P3)
  RVA=0x0B0AEF14  token=0x6000F73  System.Boolean __Gen_Wrap_426(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Object P2, System.Object P3, System.Single& P4)
  RVA=0x0B0AF078  token=0x6000F74  System.Boolean __Gen_Wrap_427(System.Object P0, System.Object P1, System.Int32& P2, System.Int32& P3)
  RVA=0x0B0AF1C4  token=0x6000F75  System.Void __Gen_Wrap_428(System.Object P0, System.Boolean P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0B0AF2BC  token=0x6000F76  System.Int32 __Gen_Wrap_429(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0B0AF4F0  token=0x6000F77  Beyond.UI.GPUI.GPUIExtendedRuntimePrefabData __Gen_Wrap_430(System.Object P0, System.Object P1)
  RVA=0x0B0AF5E8  token=0x6000F78  System.Boolean __Gen_Wrap_431(Beyond.UI.GPUI.GPUIHandle& P0, Beyond.UI.GPUI.GPUIHandle P1)
  RVA=0x0B0AF750  token=0x6000F79  System.Boolean __Gen_Wrap_432(Beyond.UI.GPUI.GPUIHandle& P0, System.Object P1)
  RVA=0x0B0AF894  token=0x6000F7A  System.Int32 __Gen_Wrap_433(Beyond.UI.GPUI.GPUIHandle& P0)
  RVA=0x0B0AF9CC  token=0x6000F7B  System.Boolean __Gen_Wrap_434(Beyond.UI.GPUI.GPUIHandle P0, Beyond.UI.GPUI.GPUIHandle P1)
  RVA=0x0B0AFAFC  token=0x6000F7C  Beyond.UI.GPUI.GPUIExtendedSystem __Gen_Wrap_435(System.Object P0)
  RVA=0x0B0AFBEC  token=0x6000F7D  Beyond.UI.GPUI.GPUIProxySyncManager __Gen_Wrap_436(System.Object P0)
  RVA=0x0B0AFCC8  token=0x6000F7E  System.Boolean __Gen_Wrap_437(System.Object P0, UnityEngine.RectTransform& P1, UnityEngine.Camera& P2)
  RVA=0x0B0AFE50  token=0x6000F7F  System.Boolean __Gen_Wrap_438(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x0B0AFF68  token=0x6000F80  System.Boolean __Gen_Wrap_439(System.Object P0, System.Object P1, System.Object P2, UnityEngine.Vector2& P3)
  RVA=0x0B0B024C  token=0x6000F81  System.Boolean __Gen_Wrap_440(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x0B0B0360  token=0x6000F82  System.Boolean __Gen_Wrap_441(System.Object P0, Beyond.UI.GPUI.GPUIExtendedSystem& P1)
  RVA=0x0B0B0498  token=0x6000F83  System.Boolean __Gen_Wrap_442(System.Object P0, System.Object P1, Beyond.UI.GPUI.GPUIHandle& P2, System.Int32 P3)
  RVA=0x0B0B05F0  token=0x6000F84  System.Void __Gen_Wrap_443(System.Object P0, System.Int32 P1, UnityEngine.Vector3 P2)
  RVA=0x0B0B0724  token=0x6000F85  System.Void __Gen_Wrap_444(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1)
  RVA=0x0B0B0834  token=0x6000F86  Beyond.UI.GPUI.RuntimePrefabData __Gen_Wrap_445(System.Object P0)
  RVA=0x0B0B0924  token=0x6000F87  System.Boolean __Gen_Wrap_446(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Boolean P2, System.Object P3, System.Boolean P4)
  RVA=0x0B0B0A70  token=0x6000F88  System.Boolean __Gen_Wrap_447(System.Object P0, Beyond.UI.GPUI.GPUIHandle P1, System.Boolean P2, System.Int32 P3, System.Boolean P4)
  RVA=0x0B0B0BA8  token=0x6000F89  Beyond.UI.GPUI.RuntimePrefabData __Gen_Wrap_448(System.Object P0, System.Object P1)
  RVA=0x0B0B0CA0  token=0x6000F8A  UnityEngine.Mesh __Gen_Wrap_449()
  RVA=0x0B0B0D58  token=0x6000F8B  System.Void __Gen_Wrap_450(System.Object P0, Unity.Collections.NativeArray<Beyond.UI.GPUI.GPUInstanceData>& P1, System.Int32 P2, System.Boolean P3)
  RVA=0x04D379C0  token=0x6000F8C  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200019C
SIZE:  0x58
EXTENDS: IFix.Core.AnonymousStorey
IMPLEMENTS: System.IDisposable System.Collections.IEnumerator System.Collections.Generic.IEnumerator`1
FIELDS:
  private           System.Int32                    methodId_0  // 0x40
  private           System.Int32                    methodId_1  // 0x44
  private           System.Int32                    methodId_2  // 0x48
  private           System.Int32                    methodId_3  // 0x4c
  private           System.Int32                    methodId_4  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0B0B58E0
  System.Collections.IEnumerator.Current  get=0x0B0B598C
METHODS:
  RVA=0x06365FC0  token=0x6000F8D  System.Void System.IDisposable.Dispose()
  RVA=0x06365D60  token=0x6000F8E  System.Boolean MoveNext()
  RVA=0x06365E98  token=0x6000F90  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B0B5A38  token=0x6000F92  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000F93  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200019D
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000F94  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B0B5E1C  token=0x6000F95  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02FA9650  token=0x6000F96  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B0B5CD8  token=0x6000F97  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B0B5DA4  token=0x6000F98  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B0B5E80  token=0x6000F99  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B0B5C34  token=0x6000F9A  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200019E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-PoolCore-ListPoolInitializer_UI_Beyond-Initialize0  // const
  public    static  IFix.IDMAP0                     Beyond-PoolCore-ListPoolInitializer_UI_Beyond-InitializeAtRuntime0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-CalculateLayoutInputHorizontal0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-CalculateLayoutInputVertical0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-_GetPreferredSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_minWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_preferredWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_flexibleWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_minHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_preferredHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_flexibleHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-UI-Tools-UIGridLayoutElement-get_layoutPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-_InitState0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-_UpdatePos0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-_UpdateLookAtCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-SetLookAtCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-ForceUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellCircleEffect-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellSlantEffect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-UI-UIScrollCellSlantEffect-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_GetHyperlinkText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ResolveTextStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-get_instance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-_IsUITextDisplayable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-_GetGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-_NotifyDisplayableChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-_AddDisplayableCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-_SubDisplayableCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-ChangeDisplayable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-UpdateWrapDisplayable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-SetAndResolveTextStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-GetPhoneticText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-SetPhoneticText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_textId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnEnvLangChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-set_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ReleaseUpdateFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-TryGetOriginalLinkId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-TryGetLinkId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_UpdateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_CreateUpdateFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_DelayedChangeActiveImageGo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-ClearMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-PreAnalyzeRichText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-GetOrLoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-GetCharacterCountWithoutRichText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-ProcessRichTextEntryFallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ProcessRichTextEntry0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_processRichTextEntryFunc0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-_GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-_TryParseParam0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-_TryParseRichTextEntry0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RichTextAnalyzer-AnalyzeRichText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ReturnImageGO0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ClearInlineImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_GetOrCreateImageGO0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_GetImageGoByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_ProcessRichTextInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-RefreshPopulateText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-GetMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-LoadMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-Remove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-RemoveWrap0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_OnPreRenderText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-DisposeInlineImageCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_GenerateRuntimeText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-IsInvalid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-get_fontSearchData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-IsFontDefaultMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-_RefreshMatTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-_GetFontById0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-GetI18nFont0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-GetFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextWrap-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextGroup-Add0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-AddWrap0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-ChangeWrapGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-ChangeWrapOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-SetHyperlinkUITextGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-ShrinkLinkTags0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-TryGetLinkId1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-TryGetCharacterStartPosAndHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-TryGetCharacterEndPosAndHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-CombineStringWithLanguageSpilt0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-CombineStringReverseForIndonesianAndVietnamese0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-IsGroupDisplayable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-IsHyperlinkUITextGroupDisplayable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-HyperlinkUITextManager-GetGroupDisplayableUIText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-GetGroupDisplayableHyperlinkUIText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-UpdateI18NFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_m_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-GetCNFontId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-set_m_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-get_fontSharedMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-set_fontSharedMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-SetSharedMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-OnAfterDeserialize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-AddSubTextObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-LocalStringCombineFormat0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIText-CalculatePreferredValues0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-get_STANDARD_HORIZONTAL_RESOLUTION_INT0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-get_STANDARD_VERTICAL_RESOLUTION_INT0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-IsPadDevice0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-GetResolutionScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-get_CUR_STANDARD_HORIZONTAL_RESOLUTION0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-get_CUR_STANDARD_VERTICAL_RESOLUTION0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIConst-get_dragThreshold0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-LoadModel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-LoadModelAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-Cancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-UnloadModel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIModelLoader-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetSharedIntString0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetSharedIntStringRoman0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-IsScreenPosInRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-IsScreenPosInRectTransform1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-CalcBoundOfRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-TryGetTouch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-SetAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-SetColorWithoutAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-ClearUIComponents0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-RectTransformToScreenRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-ClampPointInRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetNodeScaleOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetColorByString0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-SetMaskRatio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetCursorTipOffsetInScreen0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-GetCursorTipOffsetX0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUtils-TryGetTextWithoutRichStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ActiveSyncHelper-Sync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ActiveSyncHelper-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ActiveSyncHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ActiveSyncHelper-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Billboard-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Billboard-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Billboard-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CinemachineGyroscopeEffect-PostPipelineStageCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetRealIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_isHorizontal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonItemToastList-_GetUnConstraintTargetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonItemToastList-AdjustContainerPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_isMultiPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-CacheCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ClearShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_isVertical0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetIndexRangeFromScrollOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetShowRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetNotCacheRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-CacheOutOfRangeCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-MoveCellTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnUpdateCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-RecoverAllInput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-_TryCallAnimationInFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-InformAnimationInEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationTween-get_handler0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-ClearTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_NotifyAnimationInEasingFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_curTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationTween-get_tweenValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLoopSynchronizer-GetLoopStartPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_InitAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_SampleClipAtLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-InformAnimationInStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayWithTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayLoopAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_OnInEasingAnimationFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_animationIn0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayWithTween1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_OnInAnimationFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayOpenAudio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayInAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ProcessCellAtIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-RepositionOutOfRangeCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_moveTips0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateMoveTips0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateViewEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonItemToastList-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-ToggleItemForceValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-IsItemForceValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-ClearForceValidItemList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-GetItemName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-get_isForceValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_Press0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_OnUpdateKey0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_InitActionOnSetNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_label0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-GetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-GetSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ControllerSideMenuItemList-MeunItemInfo-Execute0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-get_inputValidTypeMask0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-set_inputValidTypeMask0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-get_overrideValidState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-IsShowTypeValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-get_isValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-ToggleActiveState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-CheckState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-set_overrideValidState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-_OnActiveTypeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CustomUIStyle-IsValidInput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DeactivateNaviOnEnable-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DeactivateNaviOnEnable-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DecoLineActiveHelper-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DecoLineActiveHelper-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DecoLineActiveHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DecoLineActiveHelper-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DecoLineActiveHelper-_HasActiveTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Empty4Raycast-OnPopulateMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-get_maxFov0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-_AdjustFOV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-set_maxFov0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FitAspectRatioByFOV-OnScreenSizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FixRotation-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FixRotation-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FullScreenSceneBlurMarker-_GetNextId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FullScreenSceneBlurMarker-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FullScreenSceneBlurMarker-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FullScreenSceneBlurMarker-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FullScreenSceneBlurMarker-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-_SetSelfEnabledStateIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-InputBindingGroupNaviDecorator-_OnSetAsNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_scrollDirection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_viewportSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_scrollSensitivity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-set_scrollSensitivity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_Position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-AutoScrollState-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-set_Position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-get_currentIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_CircularPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_RefreshToggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_PlayEndAudio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_UpdateSelection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_CalculateMovementAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_ScrollTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-ScrollTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_ScrollToMultiLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-ScrollToMultiLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-OnSelectionChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-JumpTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_RefreshScrollToggleCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-SetTotalCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-GetMovementDirection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IPointerDownHandler.OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IPointerUpHandler.OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_CalculateOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IScrollHandler.OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IBeginDragHandler.OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-_RubberDelta0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IDragHandler.OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-UnityEngine.EventSystems.IEndDragHandler.OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-AutoScrollState-Complete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollRect-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-get_controllerStickScrollEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_OnScrollEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-OnScrollEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-Initialize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_GetNeedCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-Cell-SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_ResizePool0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_CircularIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-Cell-get_isVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-OnUpdateCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-Cell-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_UpdateCellsAnimationPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_UpdateCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_ScrollToNext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_TickRefreshControllerStickScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-GetCurrentCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_ScrollToCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-ScrollToObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_ScrollToNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-_InitController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoopScrollView-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-get_blockWhileAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-set_IsWorldUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-get_IsWorldUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-get_isPCOnlyPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-get_isControllerPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-_TrySetRot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-AddSortingOrderComp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-RemoveSortingOrderComp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-BlockAllInput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-_AutoAnimationInFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LuaPanel-get_animationInFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MouseHoverTipsPosHelper-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NaviToThisGroupOnEnable-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NaviToThisGroupOnEnable-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_FindDefaultSelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-NaviToThisGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NaviToThisGroupOnEnable-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-get_Radius0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-SetMaterialDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-SetVerticesDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-OnPopulateMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-_IsRaycastLocationValidCircle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NonDrawingGraphic-IsRaycastLocationValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-ClearNullRef0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-ApplyNotch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-ApplyNewNotch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-_OnCanvasChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-_UpdateMargeSizeFromNotchInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-ClearList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-_TryAddToSideUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-_TraverseUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-FindAllSideUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-ApplyNotch1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-NotchAdapter-UnApplyNotch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ParallaxHelper-get_curValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ParallaxHelper-RefreshPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ParallaxHelper-set_curValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ParallaxHelper-InitConfigs0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ParallaxHelper-ResetValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PercentNumberTweener-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PercentNumberTweener-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PercentNumberTweener-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PercentNumberTweener-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-GenerateViewsForRebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-VirtualView-get_isAttached0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-VirtualView-AttachView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-VirtualView-DetachView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-VirtualView-GetAttachedView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutAdapter-VirtualView-GetViewID0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_adapter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_viewMgr0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_spacing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_padding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_minWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_minHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_flexibleWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_flexibleHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_layoutPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-CalculateLayoutInputHorizontal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-CalculateLayoutInputVertical0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_preferredWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_preferredHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_paddingFront0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_paddingBack0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-get_content0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_ApplyLayoutMeta0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_GetVisibleRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_GetElementBoundsFromMeta0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewPool-RecycleAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-RebuildAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetViews0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_UpdateViews0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_RebuildAllViews0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-SetAdapter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-_EnsureViewPool0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewPool-Recycle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-DetachView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewPool-Alloc0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-AttachView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-OnLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_InsertView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_RemoveView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_UpdateViewsFrom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_GetElementPosByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_GetElementBoundsByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_GetElementIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-_RefreshLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetViewCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-InsertView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-AddView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-RemoveView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-NotifyViewSizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-NotifyAllViewSizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-NotifyRebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetElementPosByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetElementBoundsByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetDirection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetSizeOnAxis0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetViewportSizeOnAxis0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-GetElementIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-ViewMgr-_NotifyLayoutChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-EmptyAdapter-GenerateViewsForRebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleLayoutGroup-EmptyAdapter-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-GetVirtualView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-UpdateCount1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-InsertView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-AddView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-OnAdapterInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-MultiVirtualView-GetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-MultiVirtualView-GetPrefab0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-MultiVirtualView-GetPreferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-MultiVirtualView-OnViewDetached0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleMultiHolder-MultiVirtualView-OnViewAttached0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-GetVirtualView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-UpdateCount1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-InsertView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-AddView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-OnAdapterInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-SimpleVirtualView-GetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-SimpleVirtualView-GetPrefab0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-SimpleVirtualView-GetPreferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-SimpleVirtualView-OnViewDetached0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRecycleSingleHolder-SimpleVirtualView-OnViewAttached0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SimpleUITextSingleLine-GenerateTextMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_CacheCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-ClearAllToast0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_GetCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayCloseAudio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-PlayOutAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_TryShowNewCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetOutClipLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_OnCellFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_ShowCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-_StartBatch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-AddToast0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ToastList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UI3DScrollList-MoveCellTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-DisposeSpriteHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-_LoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-LoadSpriteWithOutFormat0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_SetKeyCodeSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-GetAndSetActionIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_SetSecondIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetSecondActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-get_target0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-GetActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-GetActionInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetContentState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-GetAndSetActionModifyIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-GetTextStr0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_IsInputGray0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_IsInputEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_CheckInteractable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-UpdateKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_RefreshAllKeyHints0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_OnControllerTypeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_OnInputLateTickAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_TryInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-get_showCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_OnInputActionKeyUpdated0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-_OnChangeTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetBindingId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetBindingId1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIActionKeyHint-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationHolder-_FindByString0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationHolder-FindByName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationHolder-FindByNameOrAlias0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationHolder-GetAnimationClips0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLoopSynchronizer-RegisterChild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLoopSynchronizer-UnregisterChild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_animationInEasing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_animationLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-get_animationOut0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_AnimationInLoopCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_AnimationAutomaticallyCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-IsStarted0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetAnimationInClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetAnimationInEasingClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetAnimationLoopClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetAnimationOutClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetInClipLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetLoopClipLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SkipInAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleToInAnimationBegin0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleToInAnimationEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleToOutAnimationBegin0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleToOutAnimationEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-AnimationRuntime-GetClipLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetClipLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SampleClipAtPercent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-_CheckShouldAutoPlayAnimationIn0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-CheckStopped0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-Play0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-Play1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationTween-GetValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-GetCurPlayingTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-SetAudioOnOpen0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationWrapper-FOptions-GetDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationTween-_SetValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-GenerateTweenOfHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-GenerateTweenOfShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-_SetGameObjectActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-BeforeShowEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-AfterHideEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-ResetToState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-TweenHandler-IsPlaying0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-TweenHandler-KillIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-TweenHandler-OnComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-TweenHandler-SetAutoKill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-TweenHandler-_OnComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationSwitchTween-Builder-Build0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-get_pointerInArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-get_usePositionCheckMode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-_SetHighlightNodeActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-_GetPointerScreenPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIArea-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-get_includeInAutoCloseLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-AddToAutoCloseLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-RemoveFromAutoCloseLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-set_includeInAutoCloseLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-_AutoCloseToggleObjWhenOut0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-CloseSelf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-TryCloseSelf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-_UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-_GetUICamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-IsTopAutoCloseLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-ShouldClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-ChangeEnableCloseActionOnController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-OpenSelf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAutoCloseArea-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-SetBigLogoEditor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-ShowBigLogo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-SetBigLogoMask0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-ClearBigLogoMask0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-ClearBigLogo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-SetAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigLogo-SetMaskAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_StopLongPressCor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_DoClickAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_PlayClickAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_OnPress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectButton-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-get_OnOpenTweenFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-get_zoomRangeMin0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-get_zoomEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-get_touchPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_GetContainerRectBounds0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_GetAdjustedPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_RefreshPivotPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ResetPivotPositionToScreenCenter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_RefreshPivotPositionToMousePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_RefreshPivotPosition1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_UpdatePositionOffsetAfterZoomed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_PosAdjust0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_SetScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_SetZoomValueAndScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_OnZoom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_RefreshControllerFocusRaycastResults0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_OnControllerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_InitControllerBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_GetIsControllerMoveEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TickRefreshControllerStickMove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_GetIsControllerZoomEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TickRefreshControllerStickZoom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TickRefreshControllerZoomKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TickRefreshController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TickCheckControllerFocusRaycastTargets0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ManuallyZoom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_TryAlignWhenInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-_ResetControllerState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-FocusNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-FocusNode1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ZoomToFullRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ChangePaddingRight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-SetZoomRangeMax0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-OverrideZoomRangeMin0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-SyncZoomValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-GetRecoverState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-GetZoomRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-GetCurrentZoomValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ChangePivotPositionToTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ClearAllTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-RecoverContainerState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBigRectHelper-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlendImage-SetMaterialKey0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlendImage-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlendImage-SetTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlendImage-ClearMaterialCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-get_canvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-_GetCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-UpdateSortingOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurMono-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-get_rawImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-InitRT0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-SetUseSceneColorPS0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-Register0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-UnRegister0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-UpdateRT0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlurRT-SetCustomBlurImg0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_longPressTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_curPressPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_triggeredLongPress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-set_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_parentTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnInteractableChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_GetUIButtonKeyHintActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_bindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_customBindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-set_customBindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_hintText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_bindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_redDotTrans0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_bindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_IsAnyButtonEventActionInGuideUse0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_bindingViewState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-GetBindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-Beyond.Input.IBindingView.OnInputKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-Beyond.Input.IBindingView.OnInputKeyUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_hoverBindingGroupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_RefreshHoverEnableNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-GetMouseActionHints0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-OnNaviTargetEnabledAgain0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-get_actionOnSetNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_ToggleNaviInputBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-_OnSetAsNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-ChangeActionOnSetNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIButton-Beyond.Input.IBindingView.get_interactable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-get_standardHorizontalResolution0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-get_standardVerticalResolution0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-get_standardResolutionScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-get_canvasScalerList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-GetProperCanvasResolution0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-_OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-_OnInputTypeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-_CanvasOnWillRenderCanvases0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-CalcCurrentScreenRatio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-CalcWorldCanvasParams0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-CalcScreenCanvasParams0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-UpdateCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-OnScreenSizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-AddCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-RemoveCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICanvasScaleHelper-ForceCanvasUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-get_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-_SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-_ApplyOptToGraphics0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-set_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-AttachGraphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-AttachGraphicsWithGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-_CrossFadeAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-CrossFadeAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-_CrossFadeColorRGB0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-CrossFadeColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-_CrossFadeColorAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIColorGroup-CrossFadeColor1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-RefreshKeyIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-_OnControllerTypeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-_TryStaticInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIControllerKeyIcon-SetKeyIconName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-get_dragArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-IsDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-IsDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_SetChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_Element0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_GetMarginSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_ApplyScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_ApplyAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_ApplyPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-ResetToDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_alpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_scale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_layoutType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-ResetToDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_CheckElementKeyIsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-GetElementKeyWithRatio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-SaveData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-Save0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_elementKey0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-_LogEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-Save0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayout-ResetToDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-get_highlightRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-set_scale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-set_alpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-set_position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-set_layoutType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_SetDefaultValues0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-LoadData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-_OnHudLayoutSaved0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutElement-RefreshAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_scale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_ApplyScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_CalculatePositionRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_ApplyPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-set_position0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-set_scale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_alpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_ApplyAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-set_alpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_layoutType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_ApplyLayoutType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-set_layoutType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_OnElementDataLoaded0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-_OnSystemDisplaySizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICustomLayoutHandle-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_useAspectRatio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_notUseAspectRatio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_playing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_uiText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_totalCharacterNum0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-get_textRevealSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-set_textRevealSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-ResetPlay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-_RefreshPosY0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-RefreshAutoScrollData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-Play0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-RefreshText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-OnVisibleCharactersChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-StopPlay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-GetAutoWaitTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-GetScrollSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-_TryAutoScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-_UpdateTextAuto0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-SeekToEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-ManualUpdateByPercent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-ManualUpdateByTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogText-GetDisplayAllCharactersTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTextWithCursor-ResetPlay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTextWithCursor-OnVisibleCharactersChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTextWithCursor-GetDisplayAllCharactersTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-get_canvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-get_dialogText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-get_actorName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-get_waitNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-UpdateAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-LoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-_ClearRadioIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDialogTimelineText-SetRadioIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-ApplyDragArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDrag-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-get_dragThreshold0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragHandler-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-get_inDragging0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-get_dragObjectParent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-set_dragObjectParent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-get_m_dragParentRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-get_curDragObj0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-_StopDragView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-_ContinueDragOnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-ClearEvents0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDragItem-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_GetDefaultNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_ToggleOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-ManuallyFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_ControllerToggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnStopFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-ManuallyStopFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-ToggleOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_ControllerCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-_GetUICamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-SetSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_GetUICamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-KillScrollTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-ScrollTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-AutoScrollToRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-ScrollToSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_bindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_bindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_redDotTrans0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_bindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-get_bindingViewState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-GetBindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Beyond.Input.IBindingView.OnInputKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Beyond.Input.IBindingView.OnInputKeyUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdown-Beyond.Input.IBindingView.get_interactable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdownOption-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateReference-SetController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-get_IsEmpty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyGraphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-SetTop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-SetBottom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-SetLeft0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-SetRight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-ForceRebuildLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyButton0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyAnimator0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyLayoutElement0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyInnerState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-OnStateReferenceDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateReference-ScheduleForPlaying0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-StartEditorUpdateLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateAnimationInfo-OnUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateReference-ScheduleForStopping0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyLayoutGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetLeftLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_SetRealPadding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetPadding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetSpace0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyScrollList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplyGridLayoutGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ApplySelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-_ApplyState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropdownOption-SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-ClearEvents0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-ToggleHighlight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDropItem-OnDrop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-get_followPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-UpdatePosition1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-set_followPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-get_targetTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-set_targetTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIElementFollower-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIExtendScrollRect-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIExtendScrollRect-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIExtendScrollRect-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIExtendScrollRect-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFoldoutComponent-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFoldoutComponent-get_defaultFoldOut0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-NotifyCellSizeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFoldoutComponent-OnFoldOut0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFoldoutComponent-OnFoldIn0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFoldoutComponent-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-get_MaterialInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGraphicAnimation-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_InitIfNot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_SetCircleWrapScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_NaviGridLayoutInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_FindValidSelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_NaviChildren0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_NaviGridLayoutImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_NaviGridLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-RectWrapNavigate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-WrapNavigation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-_InitWrapNavigation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGridLayoutNaviWrapper-TryWrapNavigate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-get_totalCellCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetDefaultGroupTitleSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_EnsureGroupTitleSizeCacheCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_CalcGroupTitleSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_RefreshGroupTitleSizeWithoutEnsure0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_RefreshAllGroupTitleSizes0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_BuildGroupLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_RefreshGroupTitleSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupTitleSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetRowInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetRowHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetCumulativeSizeBeforeRow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetRowForCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetCellIndexForGroupCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupAndCellFromCellIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupCellIndexRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupRangeFromScrollOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupRangeFromCellIndexRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_MergeGroupRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetScrollInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_GetLineCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-CalcSomeCountNum0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SyncViewSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-SyncViewSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-InitBasicInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_SetAutoScrollSpd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_OnEnterAutoScrollUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_OnExitAutoScrollUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_OnEnterAutoScrollDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_OnExitAutoScrollDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_InitAutoScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_curSelectedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-Get0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_curSelectedObj0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-set_curSelectedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetCell1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-StopGraduallyShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SkipGraduallyShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetTargetPosByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryClearAdjustTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_ScrollToPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ScrollTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_currentStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-StartGraduallyShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-RestartGraduallyShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetSelectedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetNaviOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetNaviNewIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-NavigateSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_InitNavigation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryResolveWrapDirection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryGetCurrentFocusedCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryNavigateInsideCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryGetWrapTargetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryGetSelectableFromCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TrySetNaviTargetByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryNormalizeFirstWrapIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryNavigateBySequentialIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ListWrapNavigate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_InitControllerNavi0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-InGameAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-InGameAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateCount1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-InitConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_naviGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-ClearLastFocusNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_ClearRelatedNaviGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateGroup1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateGroup2  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupStartCellIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_RemapShowingCellIndicesAfterGroupResize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateSingleGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-NotifyGroupTitleSizeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-AppendCellToGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-RemoveLastCellFromGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetScrollTargetRow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_ScrollToRow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_ScrollToGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ScrollToGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ScrollToIndex1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-GetGroupRangeInView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-get_isMultiPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_RefreshIsMultiPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-GetContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-GetIndexRangeFromScrollOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_IsGroupInCellRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_MoveTitleTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_MoveCellTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-MoveCellTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-GetTargetPosByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetTitleCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_CacheTitleCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupBGCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_CacheGroupBGCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_GetGroupLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_MoveGroupBGTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_UpdateGroupBGByRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_OnUpdateTitleOrCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_UpdateTitleOrCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-UpdateShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-CacheOutOfRangeCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ProcessCellAtIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-RepositionOutOfRangeCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_UpdateShowingCellsForGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-TryResolveWrapDirection0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_CellsInGroupRow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-TryGetWrapTargetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ClearShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-Get0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-Get1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-GetGroupTitle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-SetCellCanCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-SetCellCanCache1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-_BuildLayoutDebugDumpText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGroupScrollList-DebugDumpLayoutToConsoleAndClipboard0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGyroscopeEffect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGyroscopeEffect-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGyroscopeEffect-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGyroscopeEffect-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-TrySyncRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHighlightMask-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_packIntoRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-set_packIntoRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_atlasHandleId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-set_atlasHandleId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_atlasHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_panelLevel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-RegisterOnEnableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-UnregisterOnEnableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-OnEnableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-set_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-get_enableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasHandle-CreateInvalidHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_ReProcessInsertForUIImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-ReProcessInsertForUIImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-_OnSpriteChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-OnPopulateMeshFallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-get_releaseImageSpriteAfterInsert0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_AddUIImageToManagerOnInstantiate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-AddUIImageToManagerOnInstantiate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_AddUIImageToManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-AddUIImageToManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_RemoveUIImageFromManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-RemoveUIImageFromManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-SetNativeSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-SetNativeSizeIgnoreRefScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-LoadSprite1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-LoadMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-ReleaseSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-get_mainTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImage-_V2AtlasInitialize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-OnChangeSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageBlur-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillAmountOffset-get_fillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillAmountOffset-set_fillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillAmountOffset-DOFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillFollower-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillFollower-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillFollower-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillFollower-RefreshFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillTailFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillTailFollower-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillTailFollower-RefreshFollower0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillTailFollower-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImageFillTailFollower-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImagePair-LoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImagePair-LoadSprite1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImagePair-set_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIImagePair-get_first0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_PlayAudioGear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_PlayAudioGearLock0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-get_currentPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ScrollEffectConfig-IsEmpty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ScrollEffectTrigger-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_InitIfNot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-get_isUpdating0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_ScrollValue2PageIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_PageIndex2ScrollValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_SwitchToPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-set_currentPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-get_currentScrollIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-DragContext-BeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_ScrollValueAlignToPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-DragContext-_TryFlingToNext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-DragContext-EndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-BlockerManager-StartInertia0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-InertiaBlocker-IsDirMatch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-BlockerManager-HitInertiaBlock0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_AutoAlign0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ScrollEffectTrigger-NotifyScrolling0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ScrollEffectTrigger-NotifyAlignFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-_OnStateChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-BlockerManager-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-SetPageCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-MoveToPage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-_OnScrollBegin0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-_OnScrollEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-OnClear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-get_velocity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-get_scrollProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-set_scrollProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_dragging0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-get_isDragging0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-CustomScrollHandler-StopMoving0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-get_velocity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-get_isDragging0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-get_scrollProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-set_scrollProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-StopMoving0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-EmptyScrollHandler-OnClear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInertiaViewPager-ScrollEffectTrigger-MarkHasScrolled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-get_isControllerInputValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-SendOnFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-SendOnFocusLost0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_VirtualKeyboardInitJavaObj0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_VirtualKeyboardAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_VirtualKeyboardSelect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnSelect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_VirtualKeyboardDeselect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnDeselect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnUpdateSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-set_s_focusedInputField0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-ActivateInputField0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_OnCloudGameKeyboardComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-TryActivateInputFieldCustom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_UpdateBindingEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-get_s_focusedInputField0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-DeactivateInputField0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_VirtualKeyboardOnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-OnInteractableChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_GetCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_UpdateKeyboardOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_CleanupAndroidObjects0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIInputField-_KeyboardHeightChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_isDynamic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_SetRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_ChangeDynamic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-set_isDynamic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_walkRation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_UpdateWalkBgScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-set_walkRation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_active0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_activeTouchId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_m_thumbPosMaxLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_shouldCheckInput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_UpdateGait0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_UpdateIndicator0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_SetWalkBgVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_SetRunBgVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_UpdateBg0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_Activate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_OnHudLayoutLoaded0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-_Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-CheckShouldActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIJoystick-get_parentTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-GraphicUpdateComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-LayoutComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-_InvokePostLayoutCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-_NextFrameCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-Rebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-DoOnceOnPostLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-_SetDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-OnRectTransformDimensionsChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILayoutDimensionListener-UnityEngine.UI.ICanvasElement.get_transform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-UpdateByPlayTime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-Exit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-StartAutoPlay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILeftSubtitle-SetLeftSubTitle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-_ResetSetCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-_TryUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMaterialAnimation-ForceUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_GetDefaultNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_ToggleOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_ControllerToggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-ToggleOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_ControllerCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_GetUICamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_IsValidIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_GetLastSelectedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-SetSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-SetSelectedList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-ClearSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-IsSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-GetSelectedIndices0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-GetSelectedStates0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-ScrollToLastSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-_ResizeSelectedStates0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Refresh1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_bindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_bindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_redDotTrans0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_bindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-get_bindingViewState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-GetBindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Beyond.Input.IBindingView.OnInputKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Beyond.Input.IBindingView.OnInputKeyUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMultiSelectDropdown-Beyond.Input.IBindingView.get_interactable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-InitBasicInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-CalcSomeCountNum0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetLeftLength0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateCount1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-UpdateCount1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-UpdateCount2  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-TryRecalculateSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetGridLayoutSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-_GetCellSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_inScrollTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_contentStartPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-set_contentStartPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetContainerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetCenterIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetShowRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetNotCacheRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-_GetCellsSizeTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-MoveCellTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISizeFollower-get_rectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISizeFollower-SyncSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-OnUpdateCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetTargetPosByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-ScrollTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-get_currentStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetStepTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-set_currentStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-SetStepTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-SetCurrentStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetScrollBarSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetLastScrollStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-UpdateLastScrollStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-GetNormalizedPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-SetNormalizedPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-ToggleByState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-Toggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollList-FoldAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-_UpdateNonUnifiedScrollbars0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-_OnPostLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-_InitIfNot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-UpdateScrollbars0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-Rebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-SetHorizontalNormalizedPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINonUnifiedScrollRect-SetVerticalNormalizedPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-get_tweenToText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-_CleanupBeforeTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-_TweenToTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-set_tweenToText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-get_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-set_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-_HandleNumberScrollTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UINumberScrollTween-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPCScaleHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionBuildingTexManager-get_materialPropertyBlock0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionBuildingTexManager-_UpdateTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionBuildingTexManager-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionBuildingTexManager-OnValidate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionBuildingTexManager-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-_IsValidScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-_HoldScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-SetExtraScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScaleHolder-GetExtraScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollbar-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollbarKeyHint-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollbarKeyHint-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_controllerStickScrollEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollbarKeyHint-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-get_elasticity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-set_elasticity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-get_velocity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-set_velocity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-get_viewRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnInitializePotentialDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-IsActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_GetDeltaAngle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_CheckCanMove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_UpdateSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_GetRealIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_UpdateShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_UpdateCenterIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_TryUpdateCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_UpdateAuto0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_InitCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_OnAwakeInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_UpdateScrollRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-_DoScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-Rebuild0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-LayoutComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-GraphicUpdateComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-IsDestroyed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-ScrollToIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-UpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-Get0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-UnityEngine.UI.ICanvasElement.get_transform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollCircle-Cell-SetSelect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollContainerFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollContainerFollower-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_autoLayoutWhenFew0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollContainerFollower-_RefreshSizeAndPositionToTargetContainer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollContainerFollower-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_space0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_maxShowingCellCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_countPerLine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_cellWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_cellHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_EditorAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_UpdateGraduallyShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_UpdateAutoScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetTop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateCount2  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-UpdateShowingCells1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetRangeInView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetCellCanCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-CacheShowingCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-set_currentStep0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetPaddingBottom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetPaddingTop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetPaddingRight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-SetPaddingLeft0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetPadding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetShowingCellsIndexRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetCenterIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_enableSelectedNavigation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-get_naviBindingGroupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetNaviManagerTargetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetIndexOf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-ScreenPos2Index0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-GetMousePosIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollList-_GetScreenPosLineColumnInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_CheckOverScrollEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-ScrollToNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_InitController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_TickRefreshControllerStickScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-get_moveTips0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_UpdateMoveTips0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-IsCellViewed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-ClampContentToBounds0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_CustomSetPosOnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-UpdateScrollEnableNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-_CheckOverScrollOnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRect-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-ResetReferences0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-_UpdateRedDotGameObjects0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RegisterRedDotStatesForScrollList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-_OnScrollListUpdateCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-_UpdateCacheDictForRangeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RedDotInfo-get_state0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RedDotInfo-get_newPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RedDotInfo-get_normalPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RedDotInfo-get_newRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RedDotInfo-get_normalRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-IsRedDotInDir0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-_CalculateRedDotStateInDir0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-UpdateRedDotStateInDir0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-ResetRedDotPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-RegisterRedDot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-UnregisterRedDot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollRectEdgeRedDot-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_InitIfNot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_ApplyTextAlignment0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_ResetTextScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_ResetTextScrollInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-ResetTextScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-ResetToStatic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-GetSingleScrollDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_UpdateScrollState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-_ApplyTextScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-CanvasGroupHandler-HandleAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-PositionScrollHandler-ApplyScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-PositionScrollHandler-ResetScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-PositionScrollHandler-UpdateScrollStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-MeshScrollHandler-_UpdateMeshClipping0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-MeshScrollHandler-_ApplyScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-MeshScrollHandler-ApplyScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-MeshScrollHandler-ResetScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIScrollText-MeshScrollHandler-UpdateScrollStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_RefreshFocusBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_InitFocusBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_RefreshRelatedBindingGroupsState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_RefreshFocusActionKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_RefreshStopFocusActionKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnIsTopLayerChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnSelectableNaviGroupDisabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_BeforeSelectableNaviGroupDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnSetLayerSelectedTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_OnRemoveFromLayerStack0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-_TryChangeNaviPartner0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-TryChangeNaviPartnerOnUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-TryChangeNaviPartnerOnDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-TryChangeNaviPartnerOnLeft0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-TryChangeNaviPartnerOnRight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-get_FocusBindingId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-get_StopFocusBindingId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-get_lastFocusNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-RefreshFocusBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-SetFocusBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISelectableNaviGroup-ManuallyRefreshRelatedBindingGroups0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-get_controllerStickScrollEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-_InitializeCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-UpdateLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-_ApplyDamping0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-_TickRefreshControllerStickScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-RefreshLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollList-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-_OnCellShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-_OnClosestCellChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-_OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-_OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISemiCircleScrollListPair-RefreshLayout0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-SetSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-Play0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-Stop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISequenceImage-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleActiveAnimationHelper-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleActiveAnimationHelper-Skip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleActiveAnimationHelper-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleActiveAnimationHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleActiveAnimationHelper-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleStateController-get_curStateName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleStateController-SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISimpleStateController-StateInfo-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISizeFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISizeFollower-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_TryPostSlideAudioEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-ClampValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-CustomProcessNormalizedValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnClickSliderBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnClickSliderHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_ApplyGamepadStickScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_OnGamepadStickScrollHoriValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_InitGamepadStickScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_AddValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_ReduceValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_ReleaseGamepadStickScrollTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_GamepadStickScrollTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-_CreateGamepadStickScrollTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_bindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_bindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_redDotTrans0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_bindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-get_bindingViewState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-GetBindingViewActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-Beyond.Input.IBindingView.OnInputKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-Beyond.Input.IBindingView.OnInputKeyUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISlider-Beyond.Input.IBindingView.get_interactable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_GetSoftMaskTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-get_softMaskDefaultMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-GetTextSoftMaskMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-GetUIVFXSoftMaskMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-GetCornerCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-GetFourCornnerArrayWorld0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_UpdateImageCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_GetRuntimeAtlasSpritePadding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_GetRuntimeAtlasSpriteInnerUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_GetAdjustedBorders0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_CalculateSlicedSpriteParams0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-get_graphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-MarkGraphicVerticesDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-MarkGraphicMaterialDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_UpdateChildren0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_UpdateParam0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_OnEnableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_OnWillRenderCanvases0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_DestroyMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-_IsImageParamChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMask-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-get_softMaskModefiedMat0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-get_softMask0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-get_uiText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-GetModifiedMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-ModifyMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_Equal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_ComputeLineCross0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_ComputePointAtTriangleUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_SetUIVertexUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_ClipTriangleByClampSoftMaskUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-_ClipRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISoftMaskable-ModifyMesh1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-_GetRenderer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-_GetChildrenRenderers0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-_GetChildrenParticleRenderers0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-_GetCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-SetOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISortingOrder-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-get_centerIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-_GetClampedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-_TryAutoScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-_OnScrollEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-_OnScrollStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-InitConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-SetTop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-UpdateShowingCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStepScrollList-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStyleByState-_IsValidStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStyleByState-UpdateStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStyleByState-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIStyleByState-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISubtitle-_SetAsDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISubtitle-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISubtitle-SetSubtitle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISubtitle-UpdateAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-get_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-set_text0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-set_color0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-get_first0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-SetAndResolveTextStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITextPair-SetPhoneticText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_OnToggleFail0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-TryInternalToggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-get_customBindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_TryRefreshBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_TryRefreshToggleBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_TryRefreshHoverConfirmBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_InitActionOnSetNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_InitToggleBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_UpdateObjects0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_UpdateBindingEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-PlayAudio0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-InternalToggle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnValueChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_NotifyIsHover0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnInteractableChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-set_customBindingViewLabelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-GetMouseActionHints0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnSubmit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-OnNaviTargetEnabledAgain0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_ToggleNaviInputBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-_OnSetAsNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggle-RefreshHintText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-_MoveTo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-MoveToNext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-MoveToPrevious0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIToggleGroup-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_touchPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_isDragging0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_isInGestureZoom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-_IsSameTouchFromPress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-DoEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-_OnActionPress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-_OnActionRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-ClearTouchInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-SimulateClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-DeActiveTouch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-_UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITouchPanel-get_parentTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-get_graphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-get_softMaskable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-_UpdateParam0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-_OnEnableRuntimeAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-_DestroyMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVFXManager-GetModifiedMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-ScrollListLayoutInfo-get_cellHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-UpdateUpPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-UpdateDownPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-UpdateMidPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-_ScrollLayouts0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-UpdateWorldLevelScrollList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWorldLevelScrollListAnimCtrl-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-VirtualMouseInitPos-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-VirtualMouseInitPos-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-_OnConfirm0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-_OnCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-Show0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CSPopupPanel-ClearAllPanels0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-_CheckMotionLevelAndPostIfChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-PostEventCommonShort0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-PostEventCommonOperateSuccess0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MobileMotionManager-PostEventCommonOperateFailure0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-I18nFontLoader-GetCNFont0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-AddNotFoundUnicode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-FontAssetRef-GetFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-TryGetFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-FontAssetRef-RemoveRefText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-TryRemoveRefText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-FontAssetRef-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DynamicFontAssetLoader-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TweenUtils-TickAsCountDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TweenUtils-FLerpByTime-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TweenUtils-FLerpByTime-GetValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLayerMixPlayer-LockToClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLayerMixPlayer-CreateGraph0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLayerMixPlayer-CreateMixer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLayerMixPlayer-CreateOutput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationLayerMixPlayer-CreateSourceFromAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationMixPlayer-LockToClip0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationMixPlayer-CreateGraph0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationMixPlayer-CreateMixer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationMixPlayer-CreateOutput0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAnimationMixPlayer-CreateSourceFromAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoadImageSprite-get_spritePath0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoadImageSprite-_LoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoadImageSprite-set_spritePath0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoadImageSprite-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILoadImageSpriteByName-set_spriteName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-BeforeShowEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-BeforeHideEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-AfterShowEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-AfterHideEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-ResetToState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenContext-OnReset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenContext-OnTweenStop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-ClearTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-_ClearTweenInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-Show0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-Hide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-get_isTweening0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-WaitForTweening0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-SetOptions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-get_isShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-set_isShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-GetContext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenContext-IsInterapted0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenWrapper-IsActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenWrapper-IsPlaying0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenWrapper-KillIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenWrapper-OnComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISwitchTween-TweenWrapper-SetAutoKill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-get_alphaHandler0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-get_activeAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-set_activeAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-Durations-GetHideDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-GenerateTweenOfHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-Durations-GetShowDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-GenerateTweenOfShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-SetObjectActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-BeforeShowEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-AfterHideEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-ResetToState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeSwitchTween-Builder-Build0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-_GetTargetAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-_GetTargetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-GenerateTweenOfHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-GenerateTweenOfShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-BeforeShowEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-AfterHideEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-ResetToState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-TweenHandler-IsPlaying0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-TweenHandler-KillIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-TweenHandler-OnComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FadeTranslationSwitchTween-TweenHandler-SetAutoKill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-FreeRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_ProcessRemoveQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_IsRuntimeAtlasCompatible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-get_maxFreeRectWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-get_maxFreeRectHeight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-InsertRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-CopyIntoAtlas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_ProcessInsertQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_CreateTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-_ClearTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-OnUnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-GetAtlasCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasHandle-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-InsertRects0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAtlasManager-UIAtlasPage-FreeRects0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimation-_UpdateRedDotAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimation-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimation-UnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimation-AddRedDotImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimation-RemoveRedDotImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimationNode-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimationNode-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRedDotAnimationNode-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WebApplication-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WebApplication-StartHGBrowser0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsAnimator0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsButton0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateControllerUtils-GetName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-get_ReadableDescription0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsLayoutElement0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsGraphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsLayoutGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsScrollList0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsGridLayoutGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-EqualsSelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_EqualsByTargetType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-op_Equality0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-op_Inequality0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-Equals1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_GetTargetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnGridLayoutGroupChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnImageChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnLayoutElementChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnLayoutGroupChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnRectTransformChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnScrollListChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateDescription-_OnTextChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-Run0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-Stop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-StateReference-HandleScheduledStates0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-_HandleStateAnimations0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIState-UIStateController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPrefabData-_NormalizeWrapMode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPrefabData-_PackFillOriginFlags0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPrefabData-CreateFontNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPrefabDataLite-_NormalizeWrapMode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPrefabDataLite-CreateFontNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIRootTransformState-get_Default0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISubrootDomainState-get_Default0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedAnimationState-get_Default0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedTransformUtility-ResolveSubroot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackPrefabAndParent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackGrandParentMatTypeAndDrawType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackRootScaleY0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackUInt16Pair0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackFillOverrideByte0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackTextScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-_PackSnorm160  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedPackingUtility-PackQuaternion0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_LegacyPrefabStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_LegacyPrefabLiteStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_LegacyInstanceStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_ExtendedPrefabStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_ExtendedPrefabLiteStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_ExtendedInstanceStride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_IsLegacyLayoutAligned0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUILayoutInfo-get_IsExtendedLayoutAligned0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUPrefabData-CreateFontNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUPrefabDataLite-CreateFontNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-CodePoint-ConvertToUTF320  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-get_runtimeTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-get_dynamicAtlasRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-get_isInitialized0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_IsValidStaticTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_IsValidDynamicAtlasRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_IsAtlasCreationValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_LogFailure0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Initialize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Initialize1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-DynamicSpriteHandle-CreateInvalidHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_GetSpriteContentSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_TryValidateSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-_GetSpriteInstanceId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Acquire0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-DynamicSpriteHandle-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-Release1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicAtlas-GetAtlasStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-_GetSpriteInstanceId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-RegisterSpriteUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-UnregisterSpriteUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-_SetLiteUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-TryAcquire0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-FlushIfNeeded0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-DynamicPrefabNodeKey-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-DynamicPrefabNodeKey-Equals1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-DynamicPrefabNodeKey-GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicPrefabNodeManager-DynamicPrefabNodeKey-_GetSpriteInstanceId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-TryAcquireDynamicSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-TryAcquireDynamicPrefabNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-TryAcquireDynamicPrefabNode1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-MatchesDynamicPrefabNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-ReleaseDynamicPrefabNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedDynamicSpriteOverrideManager-ReleaseDynamicSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-get_Prefab0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-get_AnimationDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_FindFirstAnimationIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_FindPreferredRootAnimationIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_GetAnimationData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_FindDefaultSubrootAnimationIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_InitializeAnimationStates0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_HasActivePrefabNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_ReleaseSpriteOverrideResources0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_ClearSpriteOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_ClearAllSpriteOverrides0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-Recycle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_WriteRootNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_WriteSubrootProxyNodes0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_GetAnimationStateForNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_UnpackPrefabId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_ApplySpriteOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_PackFillOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-DoUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetRootPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetRootScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetRootRotation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-StartAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-StartSubrootAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetNodeVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIUtils-ColorToRGBAUint320  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetNodeColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetNodeFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetSubrootState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetSubrootVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_TryResolveCurrentBasePrefabOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_TryPrimeSpriteOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-_TryPrimeSpriteOverrideWithReleasedSlot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-SetSpriteOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-ClearSpriteOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-ClearDynamicSpriteOverrides0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedRuntimeInstance-UpdateDynamicSpriteContext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-GetFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-get_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-set_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-InitMaterials0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-RegisterBeforeRenderSync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-UnregisterBeforeRenderSync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_GetOneHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-CreateInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_IsHandleInvalid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-DestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-IsInstanceValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetRootPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetRootScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetRootRotation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_TryResolveNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetNodeVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetSubrootVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetSubrootTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetImageFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-SetSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-StartAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-StartAnimation1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-TryGetAnimationDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-StartSubrootAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-TryGetSubrootAnimationDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_DisposeLoadedTextures0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-CleanMaterials0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-UpdateSharedTextures0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_SyncDynamicAtlasRuntimeTexture0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_DestroyAnimatedInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-UpdateWorldMat0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_CanSubmitInstanceRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_RenderNodes0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_DoLateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_OnPreWillRenderCanvases0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-InitMaterials0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-InitializeSharedState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_CalculateRequiredPrefabBufferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_TryResolveAndValidateGroupBufferConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_EnsureInstanceBufferManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_InitPrefabBufferManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_PreloadText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_ResetLiveDynamicSpriteState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_LoadPrefabNodeBuffer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_FindAnchorNodeIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_LoadPrefabData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_InitDynamicPrefabNodeManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_UpdateLiveDynamicSpriteContext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_LoadGroupData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIExtendedSystem-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIHandle-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIHandle-Equals1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIHandle-GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIHandle-op_Equality0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIHandle-op_Inequality0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_prefabName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_autoCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_sortingOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_targetSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_syncManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_handle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-IsHandleValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-get_hasInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-UnregisterProxy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-_AutoAssignTargetSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-_TryResolveCanvasContext0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-_SnapshotAndCheckChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_TryResolveTargetCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_TryGetCanvasLocalPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-TrySyncPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-TrySyncPosition1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-MarkPositionDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnInstanceCreated0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-TryCreateInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-TryPrepareForBeforeRender0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-FlushBeforeRender0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-_UnregisterFromSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-_RegisterToSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-RegisterProxy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-TryResolveTargetSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_TryResolveTargetSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnInstanceDestroyed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_HandleTargetSystemChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-SetSyncManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-TryDestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_CacheRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-_TryResolveSyncManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnRectTransformDimensionsChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnTransformParentChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIPrefabProxyBase-OnDidApplyAnimationProperties0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-get_targetSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-MarkDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIProxySyncManager-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-OnPopulateMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISharedGraphicBase-UpdateMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISubrootTag-get_subrootName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-GetFontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-get_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-set_fontAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_GetOneHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_CreateInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_IsHandleInvalid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-SetPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-SetRootPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-CreateInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_RecycleOneHandle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_DestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-DestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-IsInstanceValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-SetRootScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-SetRootScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-get_Prefab0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-StartAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-get_AnimationDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-StartAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-StartAnimation1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-SetVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_DisposeLoadedTextures0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_DoOnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_DestroyAnimatedInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_RenderNodes0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_DoLateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_PreWillRenderCanvases0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_PreloadTextInGroupData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_LoadPrefabNodeBuffer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_LoadPrefabData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_LoadGroupData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-OnPopulateMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUISystem-UpdateMaterial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-GPUIUtils-CreateQuadMesh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-Recycle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-DoUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUI-RuntimeInstance-SetVisibility0  // const
METHODS:
END_CLASS

