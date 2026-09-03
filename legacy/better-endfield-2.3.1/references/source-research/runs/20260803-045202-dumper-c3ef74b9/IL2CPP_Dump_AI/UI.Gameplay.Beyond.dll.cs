// ========================================================
// Dumped by @desirepro
// Assembly: UI.Gameplay.Beyond.dll
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

CLASS: UIEquipRotHelper
TYPE:  class
TOKEN: 0x2000002
SIZE:  0xC0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Transform           _rotParent  // 0x68
  private           UnityEngine.Transform           _rotTarget  // 0x70
  private           UnityEngine.Transform           _posTarget  // 0x78
  private           System.Single                   _maxLength  // 0x80
  private           System.Collections.Generic.List<UnityEngine.Transform>_slots  // 0x88
  private           System.Collections.Generic.List<UnityEngine.Transform>_rotRoots  // 0x90
  private           System.Single                   _tweenTime  // 0x98
  private           UnityEngine.Vector3             m_desRot  // 0x9c
  private           UnityEngine.Vector3             m_desZero  // 0xa8
  private           System.Int32                    m_curIndex  // 0xb4
  private           System.Int32                    m_maxNum  // 0xb8
  private           System.Boolean                  <playing>k__BackingField  // 0xbc
PROPERTIES:
  tickOption  get=0x04D8D060
  playing  get=0x04D8E640  set=0x04D8E670
METHODS:
  RVA=0x0B11484C  token=0x6000004  UnityEngine.Vector3 _ClampRot(UnityEngine.Vector3 rot)
  RVA=0x0B1148D4  token=0x6000005  UnityEngine.Vector3 _GetNearZero(UnityEngine.Vector3 rot)
  RVA=0x0B114930  token=0x6000006  System.Void _Init()
  RVA=0x0B114130  token=0x6000007  System.Void OnAwake()
  RVA=0x0B11430C  token=0x6000008  System.Void Tick(System.Single deltaTime)
  RVA=0x0B114178  token=0x6000009  System.Void SelectPart(System.Int32 index)
  RVA=0x0B114150  token=0x600000A  System.Void OnRelease()
  RVA=0x0B114A14  token=0x600000B  System.Void .ctor()
  RVA=0x04DAB010  token=0x600000C  System.Void <SelectPart>b__22_0()
END_CLASS

CLASS: UIWatchPanelCut
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Vector3             m_watchpanelPos  // 0x68
  private           UnityEngine.Vector3             m_watchpanelDir  // 0x74
  private           UnityEngine.Vector3             m_watchpanelRadius  // 0x80
  public            System.Single                   minimapRadius  // 0x8c
  public            System.Single                   eclosionRange  // 0x90
PROPERTIES:
  tickOption  get=0x04D8D060
METHODS:
  RVA=0x04422590  token=0x600000E  System.Void Start()
  RVA=0x0B114A28  token=0x600000F  UnityEngine.Vector3 GetIntersectWithLineAndPlane(UnityEngine.Vector3 point, UnityEngine.Vector3 direct, UnityEngine.Vector3 planeNormal, UnityEngine.Vector3 planePoint)
  RVA=0x04422620  token=0x6000010  System.Void Tick(System.Single deltaTime)
  RVA=0x039C2B00  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: AnimStateInfo
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            System.Int32                    fillAmount  // 0x10
  public            System.String                   animName  // 0x18
METHODS:
END_CLASS

CLASS: DisplayState
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UICharPassiveLiino.DisplayStateIdle  // const
  public    static  Beyond.UI.UICharPassiveLiino.DisplayStateNormal  // const
  public    static  Beyond.UI.UICharPassiveLiino.DisplayStateUltimate  // const
METHODS:
END_CLASS

CLASS: StateInfo
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x20
FIELDS:
  public            System.Int32                    count  // 0x10
  public            System.String                   stateName  // 0x18
METHODS:
END_CLASS

CLASS: PendingEffect
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x10
  public            UnityEngine.Vector3             worldPos  // 0x18
  public            System.Single                   triggerTime  // 0x24
METHODS:
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    cached  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600005B  System.Void .ctor()
  RVA=0x0B112808  token=0x600005C  System.Void <_CacheHideCallbacks>b__0()
END_CLASS

CLASS: RingState
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.ComboSpecialNodeRingQte.RingStateNone  // const
  public    static  Beyond.UI.ComboSpecialNodeRingQte.RingStateEarlyState  // const
  public    static  Beyond.UI.ComboSpecialNodeRingQte.RingStatePerfectState  // const
  public    static  Beyond.UI.ComboSpecialNodeRingQte.RingStateMissState  // const
  public    static  Beyond.UI.ComboSpecialNodeRingQte.RingStatePressedState  // const
METHODS:
END_CLASS

CLASS: CoalitionDetail
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x28
FIELDS:
  public            Beyond.UI.DamageTextBase        instance  // 0x10
  public            System.Single                   time  // 0x18
  public            System.Double                   value  // 0x20
METHODS:
END_CLASS

CLASS: CoalitionDetail
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x28
FIELDS:
  public            Beyond.UI.DamageTextBaseV2      instance  // 0x10
  public            System.Single                   time  // 0x18
  public            System.Double                   value  // 0x20
METHODS:
END_CLASS

CLASS: IgnitePrefabOverride
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x28
FIELDS:
  public            Beyond.GEnums.EnergyShardType   energyShardType  // 0x10
  public            System.String                   overrideName  // 0x18
  public            System.Boolean                  isCrystBreak  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000A2  System.Void .ctor()
END_CLASS

CLASS: SpawnPointConfig
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x20
FIELDS:
  public            Beyond.Gameplay.Core.ObjectType objectType  // 0x10
  public            System.Boolean                  followTarget  // 0x14
  public            System.Boolean                  useHitPoint  // 0x15
  public            Beyond.Gameplay.MountPoint      targetMountPoint  // 0x18
METHODS:
  RVA=0x04DAB000  token=0x60000B8  System.Void .ctor()
END_CLASS

CLASS: SpawnPointConfig
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x20
FIELDS:
  public            Beyond.Gameplay.Core.ObjectType objectType  // 0x10
  public            System.Boolean                  followTarget  // 0x14
  public            System.Boolean                  useHitPoint  // 0x15
  public            Beyond.Gameplay.MountPoint      targetMountPoint  // 0x18
METHODS:
  RVA=0x04DAB000  token=0x60000D0  System.Void .ctor()
END_CLASS

CLASS: DamageTextSortingPriority
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.DamageTextBaseV2.DamageTextSortingPriorityLevel0  // const
  public    static  Beyond.UI.DamageTextBaseV2.DamageTextSortingPriorityLevel1  // const
  public    static  Beyond.UI.DamageTextBaseV2.DamageTextSortingPriorityLevel2  // const
METHODS:
END_CLASS

CLASS: DamageTextBaseConfig
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x58
FIELDS:
  public            Beyond.UI.DamageTextType        type  // 0x10
  public            System.Single                   showDuration  // 0x14
  public            System.Collections.Generic.List<Beyond.UI.DamageTextBaseV2.SpawnPointConfig>spawnPointConfigs  // 0x18
  public            Beyond.UI.DamageTextBaseV2.DamageTextSortingPrioritysortingPriority  // 0x20
  public            System.String                   prefabName  // 0x28
  public            System.Single                   textNodeAlpha  // 0x30
  public            System.Collections.Generic.List<System.String>textNodeName  // 0x38
  public            System.Collections.Generic.List<System.String>colorNodeName  // 0x40
  public            System.String                   leftAnimation  // 0x48
  public            System.String                   rightAnimation  // 0x50
METHODS:
  RVA=0x0448A9D0  token=0x60000D1  System.Void .ctor()
END_CLASS

CLASS: HeadBarType
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.HeadBarCtrl.HeadBarTypeMob  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarTypeElite  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarTypeEnemyPart  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarTypeCNT  // const
METHODS:
END_CLASS

CLASS: HeadBarRefreshReason
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonOnCreate  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonOnAddHeadBar  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonOnRemoveHeadBar  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonOnHeadBarShowConfigChanged  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonTakeDamage  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonFullHpOrZeroHp  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonEnemyPartEnableChanged  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonLateUpdate  // const
  public    static  Beyond.UI.HeadBarCtrl.HeadBarRefreshReasonDialog  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass55_0
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x30
FIELDS:
  public            Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>abilitySystemPtr  // 0x10
  public            Beyond.UI.UIHeadBarFollower     bar  // 0x20
  public            Beyond.UI.HeadBarCtrl           <>4__this  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000115  System.Void .ctor()
  RVA=0x039C22A0  token=0x6000116  System.Void <_RefreshHeadBar>b__1()
  RVA=0x0B117E6C  token=0x6000117  System.Void <_RefreshHeadBar>b__2()
END_CLASS

CLASS: <>c__DisplayClass59_0
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x30
FIELDS:
  public            Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>abilitySystemPtr  // 0x10
  public            Beyond.UI.UILvInfoBar           bar  // 0x20
  public            Beyond.UI.HeadBarCtrl           <>4__this  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000118  System.Void .ctor()
  RVA=0x04693550  token=0x6000119  System.Void <_UpdateLevelInfoBar>b__0()
END_CLASS

CLASS: <>c__DisplayClass61_0
TYPE:  sealed class
TOKEN: 0x2000033
SIZE:  0x30
FIELDS:
  public            Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>abilitySystemPtr  // 0x10
  public            Beyond.UI.UITacticalItemBar     bar  // 0x20
  public            Beyond.UI.HeadBarCtrl           <>4__this  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600011A  System.Void .ctor()
  RVA=0x0B117EF4  token=0x600011B  System.Void <_UpdateTacticalItemBar>b__0()
END_CLASS

CLASS: HpDisplayState
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIHeadBar.HpDisplayStateNormal  // const
  public    static  Beyond.UI.UIHeadBar.HpDisplayStateStay  // const
  public    static  Beyond.UI.UIHeadBar.HpDisplayStateDecrease  // const
METHODS:
END_CLASS

CLASS: HeadLabelType
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIHeadLabel.HeadLabelTypeRoot  // const
  public    static  Beyond.UI.UIHeadLabel.HeadLabelTypeBubble  // const
  public    static  Beyond.UI.UIHeadLabel.HeadLabelTypeText  // const
  public    static  Beyond.UI.UIHeadLabel.HeadLabelTypeIcon  // const
  public    static  Beyond.UI.UIHeadLabel.HeadLabelTypeCount  // const
METHODS:
END_CLASS

CLASS: VisibleSource
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIHeadLabel.VisibleSourceComp  // const
  public    static  Beyond.UI.UIHeadLabel.VisibleSourceSystem  // const
  public    static  Beyond.UI.UIHeadLabel.VisibleSourceFloatingUI  // const
  public    static  Beyond.UI.UIHeadLabel.VisibleSourceCount  // const
METHODS:
END_CLASS

CLASS: HeadLabelRootData
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           root  // 0x10
  public            Beyond.UI.UIHeadLabel.HeadLabelTypeheadLabelType  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000173  System.Void .ctor()
END_CLASS

CLASS: HeadLabelRootVisibleData
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             originalPosition  // 0x10
  public            System.Collections.Generic.Dictionary<Beyond.UI.UIHeadLabel.VisibleSource,System.Boolean>visibleSource  // 0x20
PROPERTIES:
  realVisible  get=0x036D0280
METHODS:
  RVA=0x036CF7F0  token=0x6000175  System.Void Init(UnityEngine.Vector3 position)
  RVA=0x036CFDA0  token=0x6000176  System.Void UpdateVisible(Beyond.UI.UIHeadLabel.VisibleSource source, System.Boolean visible)
  RVA=0x036CF830  token=0x6000177  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass49_0
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIHeadLabel.HeadLabelTypeheadLabelType  // 0x10
  public            Beyond.UI.UIHeadLabel           <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000178  System.Void .ctor()
  RVA=0x036CFCB0  token=0x6000179  System.Void <_UpdateFloatingUIVisibleCallback>b__0(System.Boolean visible)
END_CLASS

CLASS: PoiseDisplayState
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIPoiseBar.PoiseDisplayStateNormal  // const
  public    static  Beyond.UI.UIPoiseBar.PoiseDisplayStateStay  // const
  public    static  Beyond.UI.UIPoiseBar.PoiseDisplayStateIncrease  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x18
FIELDS:
  public            Beyond.UI.AtbCell               cell  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001BB  System.Void .ctor()
  RVA=0x0B117E50  token=0x60001BC  System.Void <OnGainAtb>b__0()
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0x20
FIELDS:
  public            Beyond.UI.ComboSkillHint        hint  // 0x10
  public            Beyond.UI.ComboSkillPanel       <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001DA  System.Void .ctor()
  RVA=0x0B117DC8  token=0x60001DB  System.Void <_RemoveComboSkill>b__0()
END_CLASS

CLASS: <>c__DisplayClass23_0
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x18
FIELDS:
  public            Beyond.UI.ComboSkillHint        hint  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001DC  System.Void .ctor()
  RVA=0x0B117DFC  token=0x60001DD  System.Void <_OnComboSkillClearAll>b__0()
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x20
FIELDS:
  public            Beyond.UI.ComboSkillPanel       <>4__this  // 0x10
  public            Beyond.UI.ComboSkillHint        hint  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001DE  System.Void .ctor()
  RVA=0x0B117E1C  token=0x60001DF  System.Void <CastComboSkill>b__0()
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x200004F
SIZE:  0x30
FIELDS:
  public            System.UInt64                   cachedLoadKey  // 0x10
  public            Beyond.UI.GPUIBuffCell          <>4__this  // 0x18
  public            System.String                   inputSpritePath  // 0x20
  public            System.Action                   onSpriteLoaded  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000227  System.Void .ctor()
  RVA=0x02E9BA30  token=0x6000228  System.Void <Init>b__0(UnityEngine.Sprite sprite)
END_CLASS

CLASS: BuffNodeType
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.GPUIBuffNode.BuffNodeTypeHeadBarCommon  // const
  public    static  Beyond.UI.GPUIBuffNode.BuffNodeTypeHeadBarAttached  // const
  public    static  Beyond.UI.GPUIBuffNode.BuffNodeTypeSquadIconCommon  // const
  public    static  Beyond.UI.GPUIBuffNode.BuffNodeTypeMainCharHpBarCommon  // const
METHODS:
END_CLASS

CLASS: BuffCellGroup
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x38
FIELDS:
  public            Beyond.Gameplay.Core.BuffIconConfig.BuffIconStylebuffIconStyle  // 0x10
  public            Beyond.UI.GPUIBuffCell          buffCellTemplate  // 0x18
  public            System.Collections.Generic.List<Beyond.UI.GPUIBuffCell>buffCellPool  // 0x20
  public            Beyond.DynamicFastLookupCollection<System.UInt64,Beyond.UI.GPUIBuffCell>buffCellDict  // 0x28
  public            Beyond.DynamicFastLookupCollection<System.UInt64,Beyond.UI.GPUIBuffCell>buffCellWithOutAnim  // 0x30
METHODS:
  RVA=0x03AA4690  token=0x6000247  Beyond.UI.GPUIBuffCell CreateBuffCell(UnityEngine.Transform transform, Beyond.UI.GPUI.GPUIProxySyncManager syncManager)
  RVA=0x03AA2BB0  token=0x6000248  System.Void RemoveBuffCell(Beyond.UI.GPUIBuffCell buffCell)
  RVA=0x03AA2AD0  token=0x6000249  System.Void ClearAllBuffCells()
  RVA=0x041E1670  token=0x600024A  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x30
FIELDS:
  public            Beyond.UI.GPUIBuffNode          <>4__this  // 0x10
  public            Beyond.UI.GPUIBuffCell          buffCell  // 0x18
  public            Beyond.Gameplay.Core.BuffData   buffData  // 0x20
  public            System.Boolean                  playAnim  // 0x28
  public            System.Int32                    enhanceCnt  // 0x2c
METHODS:
  RVA=0x041E1670  token=0x600024B  System.Void .ctor()
  RVA=0x03AA3290  token=0x600024C  System.Void <_AddBuffIconInternal>b__0()
END_CLASS

CLASS: <>c__DisplayClass28_0
TYPE:  sealed class
TOKEN: 0x2000054
SIZE:  0x30
FIELDS:
  public            Beyond.UI.GPUIBuffNode          <>4__this  // 0x10
  public            Beyond.Gameplay.Core.BuffData   buffData  // 0x18
  public            System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>stackBuffList  // 0x20
  public            System.Boolean                  playAnim  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600024D  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass28_1
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x20
FIELDS:
  public            Beyond.UI.GPUIBuffCell          newBuffCell  // 0x10
  public            Beyond.UI.GPUIBuffNode.<>c__DisplayClass28_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600024E  System.Void .ctor()
  RVA=0x0B11E4F4  token=0x600024F  System.Void <_AddStackBuffIconInternal>b__0()
END_CLASS

CLASS: <>c__DisplayClass28_2
TYPE:  sealed class
TOKEN: 0x2000056
SIZE:  0x20
FIELDS:
  public            Beyond.UI.GPUIBuffCell          buffCell  // 0x10
  public            Beyond.UI.GPUIBuffNode.<>c__DisplayClass28_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000250  System.Void .ctor()
  RVA=0x0B11E59C  token=0x6000251  System.Void <_AddStackBuffIconInternal>b__1()
END_CLASS

CLASS: <>c__DisplayClass30_0
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>stackBuffList  // 0x10
  public            Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>buffPtr  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000252  System.Void .ctor()
  RVA=0x0B11E640  token=0x6000253  System.Void <_RemoveStackBuffIconInternal>b__0()
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x2000058
SIZE:  0x38
FIELDS:
  public            System.Action                   extraRecycleAction  // 0x10
  public            Beyond.UI.GPUIBuffNode.BuffCellGroupbuffCellGroup  // 0x18
  public            System.UInt64                   buffUid  // 0x20
  public            Beyond.UI.GPUIBuffCell          buffCell  // 0x28
  public            Beyond.UI.GPUIBuffNode          <>4__this  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000254  System.Void .ctor()
  RVA=0x04A1C240  token=0x6000255  System.Void <_RemoveSingleBuffCell>b__0()
  RVA=0x0B11E690  token=0x6000256  System.Void <_RemoveSingleBuffCell>b__1()
END_CLASS

CLASS: HpScreenEffectConfig
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x20
FIELDS:
  public            System.Single                   minHpPercent  // 0x10
  public            System.Single                   maxHpPercent  // 0x14
  public            System.String                   effectName  // 0x18
METHODS:
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x200005F
SIZE:  0x20
FIELDS:
  public            Beyond.UI.OutOfScreenTargetArrowarrow  // 0x10
  public            Beyond.UI.OutOfScreenTargetsCtrl<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002AD  System.Void .ctor()
  RVA=0x0B11E398  token=0x60002AE  System.Void <_InitArrowCallbacks>b__0()
  RVA=0x0B11E440  token=0x60002AF  System.Void <_InitArrowCallbacks>b__1()
  RVA=0x0B11E4B8  token=0x60002B0  System.Void <_InitArrowCallbacks>b__2()
END_CLASS

CLASS: ArrowRuntimeState
TYPE:  sealed class
TOKEN: 0x2000061
SIZE:  0x30
FIELDS:
  public            Beyond.UI.GPUI.GPUIHandle       handle  // 0x10
  public            System.Boolean                  rootLoopPending  // 0x18
  public            System.Single                   rootLoopAt  // 0x1c
  public            System.Boolean                  rootDestroyPending  // 0x20
  public            System.Single                   rootDestroyAt  // 0x24
  public            System.Boolean                  skillHidePending  // 0x28
  public            System.Single                   skillHideAt  // 0x2c
METHODS:
  RVA=0x041E1670  token=0x60002C3  System.Void .ctor()
END_CLASS

CLASS: PressingMode
TYPE:  sealed struct
TOKEN: 0x2000063
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.SkillButton.PressingModeNone  // const
  public    static  Beyond.UI.SkillButton.PressingModeNormal  // const
  public    static  Beyond.UI.SkillButton.PressingModeUltimate  // const
METHODS:
END_CLASS

CLASS: PressHandler
TYPE:  abstract class
TOKEN: 0x2000064
SIZE:  0x18
FIELDS:
  protected         Beyond.UI.SkillButton           m_owner  // 0x10
PROPERTIES:
  skillSetting  get=0x03671B90
  needUpdate  get=0x0B11CF50
METHODS:
  RVA=0x053908C0  token=0x60002FD  System.Void .ctor(Beyond.UI.SkillButton owner)
  RVA=-1  // abstract  token=0x60002FF  System.Void OnPressStart()
  RVA=-1  // abstract  token=0x6000300  System.Void OnUpdate(System.Single deltaTime)
  RVA=-1  // abstract  token=0x6000301  System.Void OnPressEnd()
  RVA=-1  // abstract  token=0x6000302  System.Void OnPressCancel()
  RVA=0x0B11CEC8  token=0x6000303  System.Void OnOtherPressSkillButtonStart()
  RVA=0x0B11CF0C  token=0x6000304  System.Void OnUltimateSkillChanged()
END_CLASS

CLASS: PressHandlerLongPress
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x20
EXTENDS: PressHandler
FIELDS:
  private           Beyond.UI.SkillButton.PressingModem_pressingMode  // 0x18
  private           System.Single                   m_pressingSkillTime  // 0x1c
PROPERTIES:
  needUpdate  get=0x03EE22D0
METHODS:
  RVA=0x053908C0  token=0x6000306  System.Void .ctor(Beyond.UI.SkillButton owner)
  RVA=0x03C250F0  token=0x6000307  System.Void OnPressStart()
  RVA=0x03671A00  token=0x6000308  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x03671810  token=0x6000309  System.Void OnPressEnd()
  RVA=0x0384A880  token=0x600030A  System.Void OnPressCancel()
  RVA=0x04A79750  token=0x600030B  System.Void OnOtherPressSkillButtonStart()
  RVA=0x0B11CE64  token=0x600030C  System.Void OnUltimateSkillChanged()
  RVA=0x0B11C6DC  token=0x600030D  System.Boolean <>iFixBaseProxy_get_needUpdate()
  RVA=0x0B11CEC0  token=0x600030E  System.Void <>iFixBaseProxy_OnOtherPressSkillButtonStart()
  RVA=0x0B11C6D4  token=0x600030F  System.Void <>iFixBaseProxy_OnUltimateSkillChanged()
END_CLASS

CLASS: PressHandlerIndividualUltimate
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x20
EXTENDS: PressHandler
FIELDS:
  private           Beyond.UI.SkillButton.PressingModem_pressingMode  // 0x18
  private           System.Single                   m_pressingSkillTime  // 0x1c
PROPERTIES:
  needUpdate  get=0x0B11CE14
METHODS:
  RVA=0x053908C0  token=0x6000311  System.Void .ctor(Beyond.UI.SkillButton owner)
  RVA=0x0B11CC74  token=0x6000312  System.Void OnPressStart()
  RVA=0x0B11CD54  token=0x6000313  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0B11CB7C  token=0x6000314  System.Void OnPressEnd()
  RVA=0x0B11CAC8  token=0x6000315  System.Void OnPressCancel()
  RVA=0x0B11C6DC  token=0x6000316  System.Boolean <>iFixBaseProxy_get_needUpdate()
END_CLASS

CLASS: PressHandlerDrag
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x50
EXTENDS: PressHandler
FIELDS:
  private           Beyond.UI.SkillButton.PressingModem_pressingMode  // 0x18
  private           System.Single                   m_pressingSkillTime  // 0x1c
  private           UnityEngine.RectTransform       m_indicatorRoot  // 0x20
  private           System.Single                   m_cancelRadiusSqr  // 0x28
  private           Beyond.UI.UIAnimationWrapper    m_indicatorAnim  // 0x30
  private           System.Boolean                  m_inCancelArea  // 0x38
  private           Beyond.UI.UIText                m_ultimateName  // 0x40
  private           Beyond.UI.UIImage               m_ultimateIcon  // 0x48
PROPERTIES:
  needUpdate  get=0x0B11CA78
METHODS:
  RVA=0x0B11C77C  token=0x6000318  System.Void .ctor(Beyond.UI.SkillButton owner)
  RVA=0x0B11C06C  token=0x6000319  System.Void OnPressStart()
  RVA=0x0B11C314  token=0x600031A  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0B11C6E4  token=0x600031B  System.Boolean _InCancelArea()
  RVA=0x0B11BE28  token=0x600031C  System.Void OnPressEnd()
  RVA=0x0B11BD60  token=0x600031D  System.Void OnPressCancel()
  RVA=0x0B11C2B8  token=0x600031E  System.Void OnUltimateSkillChanged()
  RVA=0x0B11C6DC  token=0x600031F  System.Boolean <>iFixBaseProxy_get_needUpdate()
  RVA=0x0B11C6D4  token=0x6000320  System.Void <>iFixBaseProxy_OnUltimateSkillChanged()
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x30
FIELDS:
  public            Beyond.UI.UIBuffCell            <>4__this  // 0x10
  public            System.String                   inputSpritePath  // 0x18
  public            System.Action                   onSpriteLoaded  // 0x20
  public            System.UInt64                   cachedLoadKey  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000381  System.Void .ctor()
  RVA=0x04CE50D0  token=0x6000382  System.Void <Init>b__0(UnityEngine.Sprite sprite)
END_CLASS

CLASS: BuffNodeType
TYPE:  sealed struct
TOKEN: 0x200006F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIBuffNode.BuffNodeTypeHeadBarCommon  // const
  public    static  Beyond.UI.UIBuffNode.BuffNodeTypeHeadBarAttached  // const
  public    static  Beyond.UI.UIBuffNode.BuffNodeTypeSquadIconCommon  // const
  public    static  Beyond.UI.UIBuffNode.BuffNodeTypeMainCharHpBarCommon  // const
METHODS:
END_CLASS

CLASS: BuffCellGroup
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x38
FIELDS:
  public            Beyond.Gameplay.Core.BuffIconConfig.BuffIconStylebuffIconStyle  // 0x10
  public            Beyond.UI.UIBuffCell            buffCellTemplate  // 0x18
  public            System.Collections.Generic.List<Beyond.UI.UIBuffCell>buffCellPool  // 0x20
  public            Beyond.DynamicFastLookupCollection<System.UInt64,Beyond.UI.UIBuffCell>buffCellDict  // 0x28
  public            Beyond.DynamicFastLookupCollection<System.UInt64,Beyond.UI.UIBuffCell>buffCellWithOutAnim  // 0x30
METHODS:
  RVA=0x031D5D50  token=0x600039C  Beyond.UI.UIBuffCell CreateBuffCell(UnityEngine.Transform transform)
  RVA=0x039C2380  token=0x600039D  System.Void RemoveBuffCell(Beyond.UI.UIBuffCell buffCell)
  RVA=0x03AA0E20  token=0x600039E  System.Void ClearAllBuffCells(Beyond.LuaResourceLoader loader)
  RVA=0x041E1670  token=0x600039F  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x2000071
SIZE:  0x28
FIELDS:
  public            Beyond.UI.UIBuffNode            <>4__this  // 0x10
  public            System.Boolean                  playAnim  // 0x18
  public            Beyond.Gameplay.Core.BuffData   buffData  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003A0  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass27_1
TYPE:  sealed class
TOKEN: 0x2000072
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIBuffCell            newBuffCell  // 0x10
  public            Beyond.UI.UIBuffNode.<>c__DisplayClass27_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003A1  System.Void .ctor()
  RVA=0x0B11FE20  token=0x60003A2  System.Void <_AddBuffIconInternal>b__0()
END_CLASS

CLASS: <>c__DisplayClass27_2
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIBuffCell            buffCell  // 0x10
  public            Beyond.UI.UIBuffNode.<>c__DisplayClass27_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003A3  System.Void .ctor()
  RVA=0x0B11FE74  token=0x60003A4  System.Void <_AddBuffIconInternal>b__1()
END_CLASS

CLASS: <>c__DisplayClass27_3
TYPE:  sealed class
TOKEN: 0x2000074
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIBuffCell            buffCell  // 0x10
  public            Beyond.UI.UIBuffNode.<>c__DisplayClass27_0CS$<>8__locals3  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003A5  System.Void .ctor()
  RVA=0x0B11FFB4  token=0x60003A6  System.Void <_AddBuffIconInternal>b__2()
END_CLASS

CLASS: <>c__DisplayClass28_0
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x30
FIELDS:
  public            Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>buffPtr  // 0x10
  public            Beyond.UI.UIBuffNode.BuffCellGroupbuffCellGroup  // 0x20
  public            Beyond.UI.UIBuffNode            <>4__this  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60003A7  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass28_1
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>stackBuffList  // 0x10
  public            Beyond.UI.UIBuffCell            buffCell  // 0x18
  public            Beyond.UI.UIBuffNode.<>c__DisplayClass28_0CS$<>8__locals1  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003A8  System.Void .ctor()
  RVA=0x0B1200F4  token=0x60003A9  System.Void <_RemoveBuffIconInternal>b__0()
  RVA=0x0B120208  token=0x60003AA  System.Void <_RemoveBuffIconInternal>b__1()
END_CLASS

CLASS: <>c__DisplayClass28_2
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x20
FIELDS:
  public            Beyond.UI.UIBuffCell            buffCell  // 0x10
  public            Beyond.UI.UIBuffNode.<>c__DisplayClass28_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003AB  System.Void .ctor()
  RVA=0x0B12031C  token=0x60003AC  System.Void <_RemoveBuffIconInternal>b__2()
  RVA=0x0B1203F0  token=0x60003AD  System.Void <_RemoveBuffIconInternal>b__3()
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x200007B
SIZE:  0x18
FIELDS:
  public            Beyond.Gameplay.Core.Buff       attachBuff  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003C1  System.Void .ctor()
  RVA=0x0B11FD90  token=0x60003C2  System.Void <_DoRefreshUI>b__0(Beyond.UI.UISpellInflictionOnCharCell cell, System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.UI.UISpellInflictionOnCharNode.<>c<>9  // static @ 0x0
  public    static  System.Action<Beyond.UI.UISpellInflictionOnCharCell,System.Int32><>9__19_1  // static @ 0x8
METHODS:
  RVA=0x0B1204C4  token=0x60003C3  System.Void .cctor()
  RVA=0x041E1670  token=0x60003C4  System.Void .ctor()
  RVA=0x0B11FD68  token=0x60003C5  System.Void <_DoRefreshUI>b__19_1(Beyond.UI.UISpellInflictionOnCharCell cell, System.Int32 index)
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x200007D
SIZE:  0x18
FIELDS:
  public            Beyond.Gameplay.Core.Buff       buff  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003C6  System.Void .ctor()
  RVA=0x0B11FDD8  token=0x60003C7  System.Void <_OnBuffIconChange>b__0(Beyond.UI.UISpellInflictionOnCharCell cell, System.Int32 index)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x200007E
SIZE:  0x18
FIELDS:
  public            Beyond.Gameplay.Core.Buff       buff  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003C8  System.Void .ctor()
  RVA=0x0B11FDD8  token=0x60003C9  System.Void <_OnBuffEnhanceChanged>b__0(Beyond.UI.UISpellInflictionOnCharCell cell, System.Int32 index)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x2000084
SIZE:  0x18
FIELDS:
  public            System.Int32                    charIndex  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003FB  System.Void .ctor()
  RVA=0x02E9D930  token=0x60003FC  System.Void <_RegisterAllCombos>b__0()
END_CLASS

CLASS: CommonTrackItemState
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CommonTrackerUpdate.CommonTrackItemStateShow  // const
  public    static  Beyond.UI.CommonTrackerUpdate.CommonTrackItemStateHide  // const
  public    static  Beyond.UI.CommonTrackerUpdate.CommonTrackItemStateShowToHide  // const
METHODS:
END_CLASS

CLASS: CommonTrackerDataState
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CommonTrackerUpdate.CommonTrackerDataStateActive  // const
  public    static  Beyond.UI.CommonTrackerUpdate.CommonTrackerDataStateInActive  // const
METHODS:
END_CLASS

CLASS: CommonTrackerData
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x28
FIELDS:
  public            Beyond.UI.CommonTrackerUpdate.CommonTrackerDataStateactiveState  // 0x10
  public            Beyond.Gameplay.CommonTrackingPointInfoBasetrackData  // 0x18
  public            System.String                   key  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600046B  System.Void .ctor()
END_CLASS

CLASS: CommonTrackerItem
TYPE:  class
TOKEN: 0x2000091
SIZE:  0x28
FIELDS:
  public            Beyond.UI.CommonTrackerUpdate.CommonTrackItemStateshowState  // 0x10
  public            UnityEngine.GameObject          obj  // 0x18
  public            Beyond.UI.CommonTrackerNodeComp commonTrackerNodeComp  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600046C  System.Void .ctor()
END_CLASS

CLASS: ETemperatureState
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureStateNone  // const
  public    static  Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureStateLow  // const
  public    static  Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureStateSafe  // const
  public    static  Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureStateHigh  // const
  public    static  Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureStateFinish  // const
METHODS:
END_CLASS

CLASS: CoolerUnitLangKeys
TYPE:  static class
TOKEN: 0x2000099
SIZE:  0x10
FIELDS:
  public    static  System.String                   TOO_COLD  // const
  public    static  System.String                   GOOD  // const
  public    static  System.String                   TOO_HOT  // const
  public    static  System.String                   FINISH  // const
METHODS:
END_CLASS

CLASS: TempColorState
TYPE:  sealed struct
TOKEN: 0x200009B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.CoolerUnitTemperatureUI.TempColorStateBlue  // const
  public    static  Beyond.UI.CoolerUnitTemperatureUI.TempColorStateGreen  // const
  public    static  Beyond.UI.CoolerUnitTemperatureUI.TempColorStateRed  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x20
FIELDS:
  public            Beyond.UI.FacLineCell           <>4__this  // 0x10
  public            DG.Tweening.TweenCallback       callback  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60004E3  System.Void .ctor()
  RVA=0x0B12CAAC  token=0x60004E4  System.Void <PlayLineDecoAnimation>b__0()
END_CLASS

CLASS: LineData
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x28
FIELDS:
  public            Beyond.UI.FacLineCell           start  // 0x10
  public            Beyond.UI.FacLineCell           end  // 0x18
  public            Beyond.UI.FacLineCell           link  // 0x20
METHODS:
END_CLASS

CLASS: ETemperatureState
TYPE:  sealed struct
TOKEN: 0x20000A8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureStateNone  // const
  public    static  Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureStateLow  // const
  public    static  Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureStateSafe  // const
  public    static  Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureStateHigh  // const
  public    static  Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureStateFinish  // const
METHODS:
END_CLASS

CLASS: CraneSpritePath
TYPE:  class
TOKEN: 0x20000B2
SIZE:  0x18
FIELDS:
  public            System.String                   stateSpritePath  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000587  System.Void .ctor()
END_CLASS

CLASS: MaskShapeType
TYPE:  sealed struct
TOKEN: 0x20000B5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UILevelMapLimitInRect.MaskShapeTypeRectangle  // const
  public    static  Beyond.UI.UILevelMapLimitInRect.MaskShapeTypeCircle  // const
METHODS:
END_CLASS

CLASS: DisappearRectShapeType
TYPE:  sealed struct
TOKEN: 0x20000B8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UILevelMapMissionArea.DisappearRectShapeTypeRectangle  // const
  public    static  Beyond.UI.UILevelMapMissionArea.DisappearRectShapeTypeCircle  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20000BC
SIZE:  0x18
FIELDS:
  public            System.String                   targetLevelId  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60005C5  System.Void .ctor()
  RVA=0x0B1371E4  token=0x60005C6  System.Void <InitSwitchButton>b__0(UnityEngine.EventSystems.PointerEventData eventData)
END_CLASS

CLASS: SettlementRegionSpritePath
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x18
FIELDS:
  public            System.String                   path  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60005E8  System.Void .ctor()
END_CLASS

CLASS: SettlementRegionSpriteData
TYPE:  class
TOKEN: 0x20000C3
SIZE:  0x18
FIELDS:
  public            Beyond.SerializeFieldDictionary<System.Int32,Beyond.UI.UISettlementRegion.SettlementRegionSpritePath>spriteWithTier  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60005E9  System.Void .ctor()
END_CLASS

CLASS: AwardMarkPoint
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector2             markPoint  // 0x10
  public            System.Boolean                  markValue  // 0x18
METHODS:
  RVA=0x04DAB020  token=0x6000624  System.Void .ctor(UnityEngine.Vector2 point, System.Boolean value)
END_CLASS

CLASS: IconGroup
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0x28
FIELDS:
  public            UnityEngine.GameObject          root  // 0x10
  public            UnityEngine.UI.Image            normal  // 0x18
  public            UnityEngine.UI.Image            error  // 0x20
PROPERTIES:
  activeSelf  get=0x0B142024
METHODS:
  RVA=0x0B141E3C  token=0x6000692  System.Void SetActiveIfNecessary(System.Boolean active)
  RVA=0x0B141F04  token=0x6000693  System.Void Switch(System.Boolean err)
  RVA=0x041E1670  token=0x6000695  System.Void .ctor()
END_CLASS

CLASS: StatusHintGroup
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x38
FIELDS:
  public            UnityEngine.GameObject          root  // 0x10
  public            UnityEngine.GameObject          localLinkHint  // 0x18
  public            UnityEngine.GameObject          globalLinkHint  // 0x20
  public            UnityEngine.GameObject          outageHint  // 0x28
  public            UnityEngine.GameObject          distLimitHint  // 0x30
METHODS:
  RVA=0x0B14213C  token=0x6000696  System.Void Switch(Beyond.Gameplay.Factory.PowerAutoConnectStatus status)
  RVA=0x041E1670  token=0x6000697  System.Void .ctor()
END_CLASS

CLASS: TravelPoleStatusSetting
TYPE:  sealed struct
TOKEN: 0x20000D9
SIZE:  0x20
FIELDS:
  public            System.Boolean                  showTextInfoBg  // 0x10
  public            UnityEngine.GameObject          textToShow  // 0x18
METHODS:
END_CLASS

CLASS: ERank
TYPE:  sealed struct
TOKEN: 0x20000DB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.UIRaceModuleRankController.ERankNone  // const
  public    static  Beyond.UI.UIRaceModuleRankController.ERankFirst  // const
  public    static  Beyond.UI.UIRaceModuleRankController.ERankSecond  // const
  public    static  Beyond.UI.UIRaceModuleRankController.ERankThird  // const
  public    static  Beyond.UI.UIRaceModuleRankController.ERankFourth  // const
  public    static  Beyond.UI.UIRaceModuleRankController.ERankFifth  // const
METHODS:
END_CLASS

CLASS: IconGroup
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x28
FIELDS:
  public            UnityEngine.GameObject          root  // 0x10
  public            UnityEngine.UI.Image            normal  // 0x18
  public            UnityEngine.UI.Image            error  // 0x20
PROPERTIES:
  activeSelf  get=0x0B142084
METHODS:
  RVA=0x0B141EA4  token=0x60006C3  System.Void SetActiveIfNecessary(System.Boolean active)
  RVA=0x0B141F94  token=0x60006C4  System.Void Switch(System.Boolean err)
  RVA=0x041E1670  token=0x60006C6  System.Void .ctor()
END_CLASS

CLASS: StatusHintGroup
TYPE:  class
TOKEN: 0x20000DE
SIZE:  0x18
FIELDS:
  public            UnityEngine.GameObject          root  // 0x10
METHODS:
  RVA=0x0B1420DC  token=0x60006C7  System.Void Switch(System.Boolean outOfRange)
  RVA=0x041E1670  token=0x60006C8  System.Void .ctor()
END_CLASS

CLASS: __StaticArrayInitTypeSize=12
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x1C
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000E4
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1211328A8F924E37E9332EFD9B6E621017D16D4199479DF8ADFCC16A5EB52CB3EB  // static @ 0x0
METHODS:
  RVA=0x0687A850  token=0x6000702  System.UInt32 ComputeStringHash(System.String s)
END_CLASS

CLASS: Beyond.PoolCore.ListPoolInitializer_UI_Gameplay_Beyond
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_initialized  // static @ 0x0
METHODS:
  RVA=0x04CE2100  token=0x6000013  System.Void InitializeAtRuntime()
  RVA=0x04CE2130  token=0x6000014  System.Void Initialize()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000016  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000017  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000018  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICharPassiveBase
TYPE:  abstract class
TOKEN: 0x200000B
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_boundAbilityPtr  // 0x18
  private           System.Boolean                  m_buffIconListenerRegistered  // 0x28
  private           System.Boolean                  m_destroying  // 0x29
PROPERTIES:
  listenBuffIconChange  get=0x04CDBE60
METHODS:
  RVA=0x0B1128B8  token=0x600001B  System.Void OnCharPassiveNumChanged(System.Single num)
  RVA=0x0B112838  token=0x600001C  System.Void OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x04CEB8F0  token=0x600001D  System.Void OnBound(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x038C8BB0  token=0x600001E  System.Void OnBeforeUnbind(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x038C8BE0  token=0x600001F  System.Void Bind(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x038C8D30  token=0x6000020  System.Void Unbind()
  RVA=0x0B112908  token=0x6000021  System.Void OnDestroy()
  RVA=0x05393520  token=0x6000022  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICharPassiveCounter
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x68
EXTENDS: Beyond.UI.UICharPassiveBase
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x30
  public            UnityEngine.GameObject          countTextRoot  // 0x38
  public            Beyond.UI.UIText                countText  // 0x40
  public            Beyond.UI.UIImage[]             layerImages  // 0x48
  public            System.String                   normalStateName  // 0x50
  public            System.String                   activeStateName  // 0x58
  public            System.Int32                    activeCount  // 0x60
  private           System.Int32                    m_currentCount  // 0x64
METHODS:
  RVA=0x0B11295C  token=0x6000023  System.Void OnCharPassiveNumChanged(System.Single num)
  RVA=0x05393520  token=0x6000024  System.Void .ctor()
  RVA=0x0B112ADC  token=0x6000025  System.Void <>iFixBaseProxy_OnCharPassiveNumChanged(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UICharPassiveLiino
TYPE:  class
TOKEN: 0x200000D
SIZE:  0xE8
EXTENDS: Beyond.UI.UICharPassiveBase
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x30
  public            System.String                   idleStateName  // 0x38
  public            System.String                   normalStateName  // 0x40
  public            Beyond.UI.UIImage               normalProgressImage  // 0x48
  public            Beyond.UI.UIAnimationWrapper    normalAnimationWrapper  // 0x50
  public            Beyond.UI.UIAnimationWrapper    normalDecoAnimationWrapper  // 0x58
  public            System.Single                   normalLoopThreshold  // 0x60
  public            System.String                   ultimateStateName  // 0x68
  public            Beyond.UI.UIImage               ultimateProgressImage  // 0x70
  public            Beyond.UI.UIAnimationWrapper    ultimateAnimationWrapper  // 0x78
  public            Beyond.UI.UIAnimationWrapper    ultimateDecoAnimationWrapper  // 0x80
  public            System.Single                   ultimateLoopThreshold  // 0x88
  public            System.String                   normalBuffId  // 0x90
  public            System.String                   ultimateBuffId  // 0x98
  public            Beyond.UI.UICharPassiveLiino.AnimStateInfo[]animStateInfos  // 0xa0
  private           Beyond.TickFunction             m_lateTickFunction  // 0xa8
  private           System.Boolean                  m_tickRegistered  // 0xb0
  private           System.Boolean                  m_tickRunning  // 0xb1
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_normalBuffPtr  // 0xb8
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_ultimateBuffPtr  // 0xc8
  private           Beyond.UI.UICharPassiveLiino.DisplayStatem_displayState  // 0xd8
  private           System.Boolean                  m_playingOut  // 0xdc
  private           System.Boolean                  m_loopStarted  // 0xdd
  private           System.Int32                    m_triggeredAnimStateCount  // 0xe0
PROPERTIES:
  listenBuffIconChange  get=0x0B113E10
METHODS:
  RVA=0x0B112AE4  token=0x6000027  System.Void Awake()
  RVA=0x0B113098  token=0x6000028  System.Void OnDestroy()
  RVA=0x0B112C90  token=0x6000029  System.Void OnBound(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x0B112C20  token=0x600002A  System.Void OnBeforeUnbind(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x0B112ED0  token=0x600002B  System.Void OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B113B58  token=0x600002C  System.Void _TryTrackBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.String id)
  RVA=0x0B113278  token=0x600002D  System.Void _ForceIdle()
  RVA=0x0B11375C  token=0x600002E  System.Void _RefreshDisplay()
  RVA=0x0B11369C  token=0x600002F  System.Void _OnLateTick(System.Single deltaTime)
  RVA=0x0B1131B0  token=0x6000030  System.Void _CheckLoopThreshold(Beyond.Gameplay.Core.Buff activeBuff)
  RVA=0x0B11344C  token=0x6000031  System.Void _InitDecoThresholds(Beyond.Gameplay.Core.Buff activeBuff)
  RVA=0x0B113C20  token=0x6000032  System.Void _UpdateDecoThresholds(System.Single progress)
  RVA=0x0B113368  token=0x6000033  Beyond.Gameplay.Core.Buff _GetActiveBuffForDisplay()
  RVA=0x0B1135E0  token=0x6000034  System.Boolean _IsBuffValid(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> ptr)
  RVA=0x0B106E00  token=0x6000035  System.Single _CalcProgress(Beyond.Gameplay.Core.Buff buff)
  RVA=0x0B1139C8  token=0x6000036  System.Void _SetProgress(Beyond.Gameplay.Core.Buff activeBuff)
  RVA=0x0B113ABC  token=0x6000037  System.Void _SetTickRunning(System.Boolean running)
  RVA=0x0B113428  token=0x6000038  Beyond.UI.UIAnimationWrapper _GetAnimWrapper(Beyond.UI.UICharPassiveLiino.DisplayState state)
  RVA=0x0B113438  token=0x6000039  Beyond.UI.UIAnimationWrapper _GetDecoWrapper(Beyond.UI.UICharPassiveLiino.DisplayState state)
  RVA=0x05393520  token=0x600003A  System.Void .ctor()
  RVA=0x0B113168  token=0x600003B  System.Void <_RefreshDisplay>b__35_0()
  RVA=0x0B113160  token=0x600003C  System.Boolean <>iFixBaseProxy_get_listenBuffIconChange()
  RVA=0x0B113158  token=0x600003D  System.Void <>iFixBaseProxy_OnDestroy()
  RVA=0x0B11312C  token=0x600003E  System.Void <>iFixBaseProxy_OnBound(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B113124  token=0x600003F  System.Void <>iFixBaseProxy_OnBeforeUnbind(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B113134  token=0x6000040  System.Void <>iFixBaseProxy_OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P0, System.Boolean P1, Beyond.Gameplay.Core.Buff.FinishReason P2)
END_CLASS

CLASS: Beyond.UI.UICharPassiveLizhiyan
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x50
EXTENDS: Beyond.UI.UICharPassiveBase
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x30
  public            Beyond.UI.UIText                countText  // 0x38
  public            Beyond.UI.UICharPassiveMultiStates.StateInfo[]states  // 0x40
  public            System.Int32                    fullCount  // 0x48
  private           System.Int32                    m_currentCount  // 0x4c
METHODS:
  RVA=0x0B113E58  token=0x6000041  System.Void OnCharPassiveNumChanged(System.Single num)
  RVA=0x05393520  token=0x6000042  System.Void .ctor()
  RVA=0x0B112ADC  token=0x6000043  System.Void <>iFixBaseProxy_OnCharPassiveNumChanged(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UICharPassiveMultiStates
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x58
EXTENDS: Beyond.UI.UICharPassiveBase
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x30
  public            Beyond.UI.UIText                countText  // 0x38
  public            Beyond.UI.UIText                extraCountText  // 0x40
  public            Beyond.UI.UICharPassiveMultiStates.StateInfo[]states  // 0x48
  public            System.Int32                    fullCount  // 0x50
  private           System.Int32                    m_currentCount  // 0x54
METHODS:
  RVA=0x0B113FA0  token=0x6000044  System.Void OnCharPassiveNumChanged(System.Single num)
  RVA=0x05393520  token=0x6000045  System.Void .ctor()
  RVA=0x0B112ADC  token=0x6000046  System.Void <>iFixBaseProxy_OnCharPassiveNumChanged(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UICharPassiveZhuangfy
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x88
EXTENDS: Beyond.UI.UICharPassiveBase
FIELDS:
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0x30
  public            Beyond.UI.UIText                countText  // 0x38
  public            Beyond.UI.UICharPassiveMultiStates.StateInfo[]states  // 0x40
  public            Beyond.UI.UIImage[]             layerImages  // 0x48
  public            System.Int32                    fullCount  // 0x50
  public            UnityEngine.Color               emptyColor  // 0x54
  public            UnityEngine.Color               normalColor  // 0x64
  public            UnityEngine.Color               overloadColor  // 0x74
  private           System.Int32                    m_currentCount  // 0x84
METHODS:
  RVA=0x03CEBCC0  token=0x6000047  System.Void OnCharPassiveNumChanged(System.Single num)
  RVA=0x03695570  token=0x6000048  System.Void .ctor()
  RVA=0x0B112ADC  token=0x6000049  System.Void <>iFixBaseProxy_OnCharPassiveNumChanged(System.Single P0)
END_CLASS

CLASS: Beyond.UI.LiinoRandomLikeEffect
TYPE:  class
TOKEN: 0x2000014
SIZE:  0xE8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            UnityEngine.RectTransform       leftArea  // 0x68
  public            UnityEngine.RectTransform       centerArea  // 0x70
  public            UnityEngine.RectTransform       rightArea  // 0x78
  public            Beyond.UI.UIAnimationWrapper[]  bigEffectAnim  // 0x80
  public            Beyond.UI.UIAnimationWrapper[]  smallEffectAnim  // 0x88
  public            System.Int32                    highFreqWeight  // 0x90
  public            System.Int32                    lowFreqWeight  // 0x94
  public            System.Int32                    maxConsecutiveSameSide  // 0x98
  public            System.Int32                    maxConsecutiveSameHalf  // 0x9c
  public            System.Int32                    minSmallCount  // 0xa0
  public            System.Int32                    maxSmallCount  // 0xa4
  public            UnityEngine.Vector2             smallOffsetRange  // 0xa8
  public            System.Single                   smallDelayTime  // 0xb0
  public            System.Single                   effectSpeed  // 0xb4
  private           System.Int32                    m_consecutiveLeftCount  // 0xb8
  private           System.Int32                    m_consecutiveRightCount  // 0xbc
  private           System.Int32                    m_consecutiveUpperCount  // 0xc0
  private           System.Int32                    m_consecutiveLowerCount  // 0xc4
  private           System.Int32                    m_bigEffectIndex  // 0xc8
  private           System.Int32                    m_smallEffectIndex  // 0xcc
  private   readonly System.Collections.Generic.List<Beyond.UI.LiinoRandomLikeEffect.PendingEffect>m_pendingEffects  // 0xd0
  private   readonly System.Collections.Generic.HashSet<Beyond.UI.UIAnimationWrapper>m_pendingSmallAnims  // 0xd8
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.UIAnimationWrapper,DG.Tweening.TweenCallback>m_hideCallbacks  // 0xe0
PROPERTIES:
  tickOption  get=0x0B1127BC
METHODS:
  RVA=0x0B11167C  token=0x600004B  System.Void OnAwake()
  RVA=0x0B111D0C  token=0x600004C  System.Void _CacheHideCallbacks(Beyond.UI.UIAnimationWrapper[] anims)
  RVA=0x0B1117D4  token=0x600004D  System.Void PlayEffect()
  RVA=0x0B111B64  token=0x600004E  System.Void Tick(System.Single deltaTime)
  RVA=0x0B111704  token=0x600004F  System.Void OnHide()
  RVA=0x0B111E64  token=0x6000050  System.Void _ClearAnims(Beyond.UI.UIAnimationWrapper[] anims)
  RVA=0x0B111F08  token=0x6000051  Beyond.UI.UIAnimationWrapper _GetNextAvailableBig()
  RVA=0x0B111FEC  token=0x6000052  Beyond.UI.UIAnimationWrapper _GetNextAvailableSmall()
  RVA=0x0B112464  token=0x6000053  UnityEngine.RectTransform _SelectArea()
  RVA=0x0B112578  token=0x6000054  System.Boolean _SelectHalf()
  RVA=0x0B112144  token=0x6000055  UnityEngine.Vector3 _GetRandomPointInArea(UnityEngine.RectTransform area, System.Boolean isUpper)
  RVA=0x0B1122D0  token=0x6000056  System.Void _PlayEffectAt(Beyond.UI.UIAnimationWrapper anim, UnityEngine.Vector3 worldPos)
  RVA=0x0B11263C  token=0x6000057  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000058  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000059  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x600005A  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ComboSpecialNodeBase
TYPE:  abstract class
TOKEN: 0x2000017
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  protected         Beyond.UI.ComboSkillHint        m_owner  // 0x18
PROPERTIES:
  needTick  get=-1  // abstract
METHODS:
  RVA=0x0B106FC8  token=0x600005E  System.Void OnAssignData(Beyond.UI.ComboSkillHint owner)
  RVA=0x0B107068  token=0x600005F  System.Void OnReady()
  RVA=0x0B1070E8  token=0x6000060  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B107028  token=0x6000061  System.Void OnCast()
  RVA=0x0B1070A8  token=0x6000062  System.Void OnRemove()
  RVA=-1  // abstract  token=0x6000063  System.Void OnRelease()
  RVA=0x05393520  token=0x6000064  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.ComboSpecialNodeRingQte
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x70
EXTENDS: Beyond.UI.ComboSpecialNodeBase
FIELDS:
  public            UnityEngine.RectTransform       ring  // 0x20
  public            UnityEngine.RectTransform       perfectRegion  // 0x28
  public            Beyond.UI.UIState.UIStateControllerringStateController  // 0x30
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x38
  private           System.Single                   m_initialWidth  // 0x40
  private           System.Single                   m_perfectWidth  // 0x44
  private           System.Single                   m_missWidth  // 0x48
  private           System.Single                   m_totalTime  // 0x4c
  private           System.Single                   m_activeStartTime  // 0x50
  private           System.Single                   m_remainTimeWhenStart  // 0x54
  private           Beyond.UI.ComboSpecialNodeRingQte.RingStatem_currentState  // 0x58
  private           UnityEngine.GameObject          m_bgRing01  // 0x60
  private           UnityEngine.GameObject          m_bgRing02  // 0x68
PROPERTIES:
  needTick  get=0x0B108018
METHODS:
  RVA=0x0B107138  token=0x6000066  System.Void OnAssignData(Beyond.UI.ComboSkillHint owner)
  RVA=0x0B1076C4  token=0x6000067  System.Void OnCast()
  RVA=0x0B1079E0  token=0x6000068  System.Void OnRemove()
  RVA=0x0B107A30  token=0x6000069  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B1078B0  token=0x600006A  System.Void OnRelease()
  RVA=0x0B107DE0  token=0x600006B  System.Void _OnComboSkillRingQte(Beyond.EventData<System.Int32,System.Single,System.Single>& evtData)
  RVA=0x0B107D1C  token=0x600006C  System.Single _GetRemainTime()
  RVA=0x0B107F60  token=0x600006D  System.Void _Reset()
  RVA=0x0B107FEC  token=0x600006E  System.Void .ctor()
  RVA=0x0B107CFC  token=0x600006F  System.Void <>iFixBaseProxy_OnAssignData(Beyond.UI.ComboSkillHint P0)
  RVA=0x0B107D04  token=0x6000070  System.Void <>iFixBaseProxy_OnCast()
  RVA=0x0B107D0C  token=0x6000071  System.Void <>iFixBaseProxy_OnRemove()
  RVA=0x0B107D14  token=0x6000072  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.DamageTextType
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.DamageTextType        Level0Normal  // const
  public    static  Beyond.UI.DamageTextType        Level1Normal  // const
  public    static  Beyond.UI.DamageTextType        Level0Critical  // const
  public    static  Beyond.UI.DamageTextType        Level1Critical  // const
  public    static  Beyond.UI.DamageTextType        Level0Block  // const
  public    static  Beyond.UI.DamageTextType        Level1Block  // const
  public    static  Beyond.UI.DamageTextType        EnemyDamage  // const
  public    static  Beyond.UI.DamageTextType        Heal  // const
  public    static  Beyond.UI.DamageTextType        IgniteBuff  // const
  public    static  Beyond.UI.DamageTextType        Airborne  // const
  public    static  Beyond.UI.DamageTextType        Crush  // const
  public    static  Beyond.UI.DamageTextType        KnockDown  // const
  public    static  Beyond.UI.DamageTextType        Fracture  // const
  public    static  Beyond.UI.DamageTextType        Immune  // const
  public    static  Beyond.UI.DamageTextType        CommonFlowText  // const
  public    static  Beyond.UI.DamageTextType        LevelUpText  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.DamageTextCtrl
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x190
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            UnityEngine.RectTransform       content  // 0x70
  public            UnityEngine.RectTransform       prefabNode  // 0x78
  public            UnityEngine.Vector2             textRandomAreaSize  // 0x80
  public            UnityEngine.Vector2             textMinDist  // 0x88
  public            System.Int32                    textPosMaxRandomTimes  // 0x90
  public            UnityEngine.Color               damageTextColorPhysical  // 0x94
  public            UnityEngine.Color               damageTextColorReal  // 0xa4
  public            UnityEngine.Color               damageTextColorFire  // 0xb4
  public            UnityEngine.Color               damageTextColorPulse  // 0xc4
  public            UnityEngine.Color               damageTextColorCryst  // 0xd4
  public            UnityEngine.Color               damageTextColorNatural  // 0xe4
  public            UnityEngine.Color               damageTextColorEther  // 0xf4
  public            UnityEngine.Vector2             mainChrDmgTxtSpawnOffset  // 0x104
  public            UnityEngine.Vector2             mainChrDmgTxtMoveSpawnOffset  // 0x10c
  public            System.Int32                    mainChrDmgTxtMoveNum  // 0x114
  public            System.Single                   mainChrDmgTxtMoveSpawnWaitTime  // 0x118
  public            UnityEngine.Vector2             guardDmgTxtSpawnOffset  // 0x11c
  public            UnityEngine.Vector2             guardDmgTxtSpawnAreaSize  // 0x124
  public            System.Single                   dmgTxtCoalitionMaxTime  // 0x12c
  public            System.Boolean                  enableGuardDmgTxtOverwrite  // 0x130
  public            UnityEngine.Vector2             physicalInflictionTextOffset  // 0x134
  public            UnityEngine.Vector2             spellInflictionTextOffset  // 0x13c
  private   static readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextType,System.Int32>PRE_LOAD_TEXT_COUNTS  // static @ 0x0
  private   static  System.String                   GUARD_OVERWRITE_COALITION_KEY  // const
  private   static  System.String                   CRYST_BREAK_TEXT_ID  // const
  private           System.Boolean                  m_isShow  // 0x144
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextType,Beyond.UI.DamageTextBase>m_textPrefabMap  // 0x148
  private   readonly System.Collections.Generic.List<Beyond.UI.DamageTextBase>m_showingTextInstances  // 0x150
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextType,System.Collections.Generic.Queue<Beyond.UI.DamageTextBase>>m_textPool  // 0x158
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextBase,UnityEngine.Vector3>m_showingTextPos  // 0x160
  private   readonly System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<System.String,Beyond.UI.DamageTextCtrl.CoalitionDetail>>>m_entityCoalitionGroupMap  // 0x168
  private   readonly System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<System.Int32,System.Single>>m_entityTextTimeMap  // 0x170
  private   readonly System.Collections.Generic.Dictionary<System.UInt64,Beyond.UI.DamageTextLevelUp>m_charLevelUpTextMap  // 0x178
  private   readonly System.Collections.Generic.List<Beyond.EventData<System.UInt64,System.Int32>>m_charLevelUpTextToShow  // 0x180
  private   readonly System.Collections.Generic.List<Beyond.UI.DamageTextBase>m_toRecycleInTick  // 0x188
PROPERTIES:
  tickOption  get=0x04A799C0
  outOfScreenPos  get=0x0B10ED84
METHODS:
  RVA=0x0B10BAB4  token=0x6000075  System.Void OnCreate()
  RVA=0x0B10B670  token=0x6000076  System.Void OnClose()
  RVA=0x0B10AEDC  token=0x6000077  System.Void DebugChangeImpl(System.Boolean disableThis)
  RVA=0x0B10C350  token=0x6000078  System.Void OnShow()
  RVA=0x0B10C304  token=0x6000079  System.Void OnHide()
  RVA=0x04CDF3D0  token=0x600007A  System.Void Tick(System.Single deltaTime)
  RVA=0x0B10CEA4  token=0x600007B  System.Void _OnHpChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem.Modifier>& eventData)
  RVA=0x0B10E4C0  token=0x600007C  System.Void _OnIgniteBuffText(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String,Beyond.GEnums.EnergyShardType>& eventData)
  RVA=0x0B10E984  token=0x600007D  System.Void _OnPhysicalInflictionApplied(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,Beyond.Gameplay.Core.AbilitySystemForEnemyPart,Beyond.Gameplay.PhysicalInflictionType>& eventData)
  RVA=0x0B10E764  token=0x600007E  System.Void _OnImmuneText(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String>& eventData)
  RVA=0x0B10EACC  token=0x600007F  System.Void _OnSquadInFightChanged(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B10CD70  token=0x6000080  System.Void _OnCreateFlowText(Beyond.EventData<UnityEngine.Vector3,System.String,UnityEngine.Color>& eventData)
  RVA=0x0B10CAEC  token=0x6000081  System.Void _OnCharLevelUp(Beyond.EventData<System.UInt64,System.Int32>& eventData)
  RVA=0x0B10C738  token=0x6000082  UnityEngine.Vector2 _GetRandomTextOffset(System.Single width, System.Single height)
  RVA=0x0B10C8F4  token=0x6000083  System.Boolean _IsTextPosValid(UnityEngine.Vector3 newPos)
  RVA=0x0B10C584  token=0x6000084  Beyond.UI.DamageTextBase _CreateTextInstance(Beyond.UI.DamageTextType textType)
  RVA=0x0B10C800  token=0x6000085  Beyond.UI.DamageTextBase _InstantiateTextInstance(Beyond.UI.DamageTextType textType)
  RVA=0x0B10EB7C  token=0x6000086  System.Void _RecycleTextInstance(Beyond.UI.DamageTextBase textInstance)
  RVA=0x04607D10  token=0x6000087  System.Void .ctor()
  RVA=0x049C12D0  token=0x6000088  System.Void .cctor()
  RVA=0x0B10A3CC  token=0x6000089  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x600008A  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.DamageTextCtrlV2
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x1A8
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            Beyond.UI.GPUI.GPUISystem       gpuiSystem  // 0x70
  public            System.Collections.Generic.List<Beyond.UI.DamageTextBaseV2.DamageTextBaseConfig>damageTextConfigs  // 0x78
  public            System.Collections.Generic.List<Beyond.UI.DamageTextCtrlV2.IgnitePrefabOverride>ignitePrefabConfig  // 0x80
  public            System.Boolean                  useWorldRandomArea  // 0x88
  public            UnityEngine.Vector2             textRandomAreaSize  // 0x8c
  public            UnityEngine.Vector2             textRandomAreaSizeWorld  // 0x94
  public            System.Single                   textRandomAreaWorldCameraDistance  // 0x9c
  public            UnityEngine.Vector2             textMinDist  // 0xa0
  public            System.Int32                    textPosMaxRandomTimes  // 0xa8
  public            UnityEngine.Color               damageTextColorPhysical  // 0xac
  public            UnityEngine.Color               damageTextColorReal  // 0xbc
  public            UnityEngine.Color               damageTextColorFire  // 0xcc
  public            UnityEngine.Color               damageTextColorPulse  // 0xdc
  public            UnityEngine.Color               damageTextColorCryst  // 0xec
  public            UnityEngine.Color               damageTextColorNatural  // 0xfc
  public            UnityEngine.Color               damageTextColorEther  // 0x10c
  public            UnityEngine.Vector2             mainChrDmgTxtSpawnOffset  // 0x11c
  public            UnityEngine.Vector2             mainChrDmgTxtMoveSpawnOffset  // 0x124
  public            System.Int32                    mainChrDmgTxtMoveNum  // 0x12c
  public            System.Single                   mainChrDmgTxtMoveSpawnWaitTime  // 0x130
  public            System.Single                   dmgTxtCoalitionMaxTime  // 0x134
  public            UnityEngine.Vector2             guardDmgTxtSpawnOffset  // 0x138
  public            UnityEngine.Vector2             guardDmgTxtSpawnAreaSize  // 0x140
  public            System.Boolean                  enableGuardDmgTxtOverwrite  // 0x148
  public            UnityEngine.Vector2             physicalInflictionTextOffset  // 0x14c
  public            UnityEngine.Vector2             spellInflictionTextOffset  // 0x154
  private   static  System.String                   GUARD_OVERWRITE_COALITION_KEY  // const
  private   static  System.String                   CRYST_BREAK_TEXT_ID  // const
  private           System.Boolean                  m_isShow  // 0x15c
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextType,Beyond.UI.DamageTextBaseV2.DamageTextBaseConfig>m_textConfigMap  // 0x160
  private   readonly System.Collections.Generic.List<Beyond.UI.DamageTextBaseV2>m_showingTextInstances  // 0x168
  private   readonly System.Collections.Generic.Queue<Beyond.UI.DamageTextBaseV2>m_textPool  // 0x170
  private   readonly System.Collections.Generic.Dictionary<Beyond.UI.DamageTextBaseV2,UnityEngine.Vector3>m_showingTextPos  // 0x178
  private   readonly System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<System.String,Beyond.UI.DamageTextCtrlV2.CoalitionDetail>>>m_entityCoalitionGroupMap  // 0x180
  private   readonly System.Collections.Generic.Dictionary<Beyond.Gameplay.Core.AbilitySystem,System.Collections.Generic.Dictionary<System.Int32,System.Single>>m_entityTextTimeMap  // 0x188
  private   readonly System.Collections.Generic.Dictionary<System.UInt64,Beyond.UI.DamageTextBaseV2>m_charLevelUpTextMap  // 0x190
  private   readonly System.Collections.Generic.List<Beyond.EventData<System.UInt64,System.Int32>>m_charLevelUpTextToShow  // 0x198
  private   readonly System.Collections.Generic.List<Beyond.UI.DamageTextBaseV2>m_toRecycleInTick  // 0x1a0
PROPERTIES:
  tickOption  get=0x04A799F0
  outOfScreenPos  get=0x0B10AE50
METHODS:
  RVA=0x0424A520  token=0x600008D  System.Void OnCreate()
  RVA=0x0B109F84  token=0x600008E  System.Void OnClose()
  RVA=0x0B109864  token=0x600008F  System.Void DebugChangeImpl(System.Boolean disableThis)
  RVA=0x048904D0  token=0x6000090  System.Void OnShow()
  RVA=0x04CA3070  token=0x6000091  System.Void OnHide()
  RVA=0x03098560  token=0x6000092  System.Void Tick(System.Single deltaTime)
  RVA=0x03AAA9B0  token=0x6000093  System.Void _OnHpChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem.Modifier>& eventData)
  RVA=0x0B10A74C  token=0x6000094  System.Void _OnIgniteBuffText(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String,Beyond.GEnums.EnergyShardType>& eventData)
  RVA=0x0B10AD04  token=0x6000095  System.Void _OnPhysicalInflictionApplied(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,Beyond.Gameplay.Core.AbilitySystemForEnemyPart,Beyond.Gameplay.PhysicalInflictionType>& eventData)
  RVA=0x0B10AB0C  token=0x6000096  System.Void _OnImmuneText(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String>& eventData)
  RVA=0x03CD55F0  token=0x6000097  System.Void _OnSquadInFightChanged(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B10A61C  token=0x6000098  System.Void _OnCreateFlowText(Beyond.EventData<UnityEngine.Vector3,System.String,UnityEngine.Color>& eventData)
  RVA=0x0B10A3D4  token=0x6000099  System.Void _OnCharLevelUp(Beyond.EventData<System.UInt64,System.Int32>& eventData)
  RVA=0x04608B90  token=0x600009A  System.Boolean _UseWorldSpaceRandomArea(UnityEngine.Vector3 textWorldPos)
  RVA=0x03772E50  token=0x600009B  UnityEngine.Vector2 _GetRandomTextOffset(System.Single width, System.Single height, System.Boolean useWorldOffset, UnityEngine.Vector3 textWorldPos)
  RVA=0x0443F0A0  token=0x600009C  System.Boolean _IsTextPosValid(UnityEngine.Vector3 newPos)
  RVA=0x03AAA840  token=0x600009D  Beyond.UI.DamageTextBaseV2 _CreateTextInstance(Beyond.UI.DamageTextType textType, System.String overrideName)
  RVA=0x03AAA400  token=0x600009E  System.Void _RecycleTextInstance(Beyond.UI.DamageTextBaseV2 textInstance)
  RVA=0x04608010  token=0x600009F  System.Void .ctor()
  RVA=0x0B10A3CC  token=0x60000A0  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x60000A1  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.DamageTextBase
TYPE:  class
TOKEN: 0x2000020
SIZE:  0xC0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.UI.DamageTextType        type  // 0x68
  public            System.Single                   showDuration  // 0x6c
  public            System.Collections.Generic.List<Beyond.UI.DamageTextBase.SpawnPointConfig>spawnPointConfigs  // 0x70
  public            Beyond.UI.UIText                text  // 0x78
  public            UnityEngine.Animation           anim  // 0x80
  private           System.Boolean                  m_isActive  // 0x88
  private           System.Boolean                  m_isFollowingTransform  // 0x89
  private           UnityEngine.Transform           m_followedTransform  // 0x90
  private           UnityEngine.Vector3             m_followedPosition  // 0x98
  private           UnityEngine.Vector2             m_uiOffset  // 0xa4
  public            Beyond.Gameplay.PeriodicTimer   durationTimer  // 0xb0
  protected         Beyond.UnSafeString.UnSafeStringHandlem_textHandle  // 0xb8
PROPERTIES:
  tickOption  get=0x044EBAE0
  isFollowingTransform  get=0x0B10935C
  worldPosition  get=0x0B109408
  uiOffset  get=0x0B1093A8
METHODS:
  RVA=0x044F4CB0  token=0x60000A7  System.Void OnAwake()
  RVA=0x0B1085BC  token=0x60000A8  System.Void OnRelease()
  RVA=0x0B109064  token=0x60000A9  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x0B1087AC  token=0x60000AA  System.Void SetFollowTarget(Beyond.Gameplay.Core.AbilitySystem target, Beyond.Gameplay.Core.AbilitySystem textTargetWhenTransferred, System.Nullable<UnityEngine.Vector3> hitPoint)
  RVA=0x0B109004  token=0x60000AB  System.Void SetUIOffset(UnityEngine.Vector2 offset)
  RVA=0x0B10855C  token=0x60000AC  System.Void Clear()
  RVA=0x0B10872C  token=0x60000AD  System.Void SetColor(UnityEngine.Color color)
  RVA=0x0B10867C  token=0x60000AE  System.Void PlayAnimation()
  RVA=0x0B1090E4  token=0x60000AF  System.Void _UpdatePosition()
  RVA=0x0B108E5C  token=0x60000B0  System.Void SetText(System.String newText)
  RVA=0x0B108F30  token=0x60000B1  System.Void SetText(System.Int32 newValue)
  RVA=0x0B108D68  token=0x60000B2  System.Void SetText(System.String prefix, System.Int32 newValue)
  RVA=0x039C3430  token=0x60000B3  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60000B4  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60000B5  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x069AFD04  token=0x60000B6  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06C37238  token=0x60000B7  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.DamageTextBaseV2
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x60
FIELDS:
  private   static readonly UnityEngine.Vector2             INVALID_POS  // static @ 0x0
  private           System.Boolean                  m_isFollowingTransform  // 0x10
  private           UnityEngine.Transform           m_followedTransform  // 0x18
  private           UnityEngine.Vector3             m_followedPosition  // 0x20
  private           UnityEngine.Vector2             m_uiOffset  // 0x2c
  private           Beyond.UI.GPUI.GPUIHandle       m_uiHandle  // 0x34
  private           Beyond.UI.GPUI.GPUISystem       m_uiSystem  // 0x40
  private           Beyond.UI.DamageTextBaseV2.DamageTextBaseConfigm_config  // 0x48
  private           System.UInt64                   <targetCharInst>k__BackingField  // 0x50
  protected         Beyond.UnSafeString.UnSafeStringHandlem_textHandle  // 0x58
PROPERTIES:
  targetCharInst  get=0x04D86270  set=0x04D867A0
  textNodeAlpha  get=0x03AAA800
  showDuration  get=0x04CAE170
  isFollowingTransform  get=0x030983A0
  isActive  get=0x030987B0
  worldPosition  get=0x03098340
  uiOffset  get=0x048B5F10
METHODS:
  RVA=0x044F4C70  token=0x60000C1  System.Void .ctor(Beyond.UI.GPUI.GPUISystem uiSystem)
  RVA=0x0B1080BC  token=0x60000C2  System.Void OnRelease()
  RVA=0x03AAA5A0  token=0x60000C3  System.Void Assign(Beyond.UI.DamageTextBaseV2.DamageTextBaseConfig config, System.String overrideName)
  RVA=0x03098840  token=0x60000C4  System.Void Tick(UnityEngine.Vector2 screenCenter, System.Single scaleFactor)
  RVA=0x04410220  token=0x60000C5  System.Void SetFollowTarget(Beyond.Gameplay.Core.AbilitySystem target, Beyond.Gameplay.Core.AbilitySystem textTargetWhenTransferred, System.Nullable<UnityEngine.Vector3> hitPoint)
  RVA=0x03AAA7C0  token=0x60000C6  System.Void SetUIOffset(UnityEngine.Vector2 offset)
  RVA=0x03AAA4D0  token=0x60000C7  System.Void Clear()
  RVA=0x0B108060  token=0x60000C8  System.Void DestroyDisplay()
  RVA=0x0363A4D0  token=0x60000C9  System.Void SetColor(UnityEngine.Color color)
  RVA=0x03AAA740  token=0x60000CA  System.Void PlayAnimation(System.Boolean isLeft)
  RVA=0x03097620  token=0x60000CB  System.Void _UpdatePosition(UnityEngine.Vector2 screenCenter, System.Single scaleFactor)
  RVA=0x0B108354  token=0x60000CC  System.Void SetText(System.String newText)
  RVA=0x0363B350  token=0x60000CD  System.Void SetText(System.Int32 newValue)
  RVA=0x0B108124  token=0x60000CE  System.Void SetText(System.String prefix, System.Int32 newValue)
  RVA=0x04D75B00  token=0x60000CF  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.DamageTextBlock
TYPE:  class
TOKEN: 0x2000026
SIZE:  0xC8
EXTENDS: Beyond.UI.DamageTextDamageBase
FIELDS:
  public            Beyond.UI.UIImage               blockIcon  // 0xc0
METHODS:
  RVA=0x0B1094EC  token=0x60000D2  System.Void SetColor(UnityEngine.Color color)
  RVA=0x039C21D0  token=0x60000D3  System.Void .ctor()
  RVA=0x0B109594  token=0x60000D4  System.Void <>iFixBaseProxy_SetColor(UnityEngine.Color P0)
END_CLASS

CLASS: Beyond.UI.DamageTextCriticalLevel0
TYPE:  class
TOKEN: 0x2000027
SIZE:  0xC8
EXTENDS: Beyond.UI.DamageTextDamageBase
FIELDS:
  public            Beyond.UI.UIText                criticalIcon  // 0xc0
METHODS:
  RVA=0x0B1095B4  token=0x60000D5  System.Void SetColor(UnityEngine.Color color)
  RVA=0x039C21D0  token=0x60000D6  System.Void .ctor()
  RVA=0x0B109594  token=0x60000D7  System.Void <>iFixBaseProxy_SetColor(UnityEngine.Color P0)
END_CLASS

CLASS: Beyond.UI.DamageTextCriticalLevel1
TYPE:  class
TOKEN: 0x2000028
SIZE:  0xE0
EXTENDS: Beyond.UI.DamageTextDamageBase
FIELDS:
  public            Beyond.UI.UIText                criticalIcon  // 0xc0
  public            Beyond.UI.UIText                criticalIconShadow  // 0xc8
  public            Beyond.UI.UIImage               bg01  // 0xd0
  public            Beyond.UI.UIImage               bg02  // 0xd8
  private   static  System.String                   ANIM_NAME_CRITICAL_LEFT  // const
  private   static  System.String                   ANIM_NAME_CRITICAL_RIGHT  // const
METHODS:
  RVA=0x0B109748  token=0x60000D8  System.Void SetColor(UnityEngine.Color color)
  RVA=0x0B109654  token=0x60000D9  System.Void PlayAnimation(System.Boolean isLeft)
  RVA=0x039C21D0  token=0x60000DA  System.Void .ctor()
  RVA=0x0B109594  token=0x60000DB  System.Void <>iFixBaseProxy_SetColor(UnityEngine.Color P0)
  RVA=0x0B10985C  token=0x60000DC  System.Void <>iFixBaseProxy_PlayAnimation(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.DamageTextDamageBase
TYPE:  class
TOKEN: 0x2000029
SIZE:  0xC0
EXTENDS: Beyond.UI.DamageTextBase
FIELDS:
METHODS:
  RVA=0x0B10EE10  token=0x60000DD  System.Void PlayAnimation(System.Boolean isLeft)
  RVA=0x039C21D0  token=0x60000DE  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.DamageTextIgniteBuff
TYPE:  class
TOKEN: 0x200002A
SIZE:  0xD8
EXTENDS: Beyond.UI.DamageTextBase
FIELDS:
  public            Beyond.UI.UIImage               bg  // 0xc0
  public            Beyond.UI.UIText                textDuplication  // 0xc8
  public            Beyond.UI.UIState.UIStateControllerstateController  // 0xd0
METHODS:
  RVA=0x044F4840  token=0x60000DF  System.Void OnAwake()
  RVA=0x0B10EEC4  token=0x60000E0  System.Void OnRelease()
  RVA=0x0B10F07C  token=0x60000E1  System.Void SetUIState(Beyond.GEnums.EnergyShardType energyShardType, System.Boolean isCrystBreak)
  RVA=0x0B10EF7C  token=0x60000E2  System.Void SetTextAndDuplication(System.String newText)
  RVA=0x039C21D0  token=0x60000E3  System.Void .ctor()
  RVA=0x0B10F1C0  token=0x60000E4  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x0B10F1C8  token=0x60000E5  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.DamageTextLevelUp
TYPE:  class
TOKEN: 0x200002B
SIZE:  0xC8
EXTENDS: Beyond.UI.DamageTextBase
FIELDS:
  private           System.UInt64                   <targetCharInst>k__BackingField  // 0xc0
PROPERTIES:
  targetCharInst  get=0x04D892F0  set=0x04D89340
METHODS:
  RVA=0x039C21D0  token=0x60000E8  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.DamageTextNormalLevel1
TYPE:  class
TOKEN: 0x200002C
SIZE:  0xC0
EXTENDS: Beyond.UI.DamageTextDamageBase
FIELDS:
  private   static  System.String                   ANIM_NAME_NORMAL_LEFT  // const
  private   static  System.String                   ANIM_NAME_NORMAL_RIGHT  // const
METHODS:
  RVA=0x0B10F1D0  token=0x60000E9  System.Void PlayAnimation(System.Boolean isLeft)
  RVA=0x039C21D0  token=0x60000EA  System.Void .ctor()
  RVA=0x0B10985C  token=0x60000EB  System.Void <>iFixBaseProxy_PlayAnimation(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.FootBarDashCell
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  isHalfCell  // 0x18
  public            Beyond.UI.UIImage               bgImage  // 0x20
  public            UnityEngine.RectTransform       cellTrans  // 0x28
  public            Beyond.UI.UIImage               grayImage  // 0x30
  public            Beyond.UI.UIImage               whiteImage  // 0x38
  public            Beyond.UI.UIAnimationWrapper    animWrapper  // 0x40
  private           System.Int32                    m_index  // 0x48
  private   static  System.Int32                    MAX_DASH_COUNT_IN_RING  // const
  private   static  System.Single                   FILL_AMOUNT_PER_CELL  // const
  private   static  System.Single                   ANGLE_PER_CELL  // const
  private   static  System.Single                   ANGLE_PER_HALF_CELL  // const
  private   static  System.Single                   ANGLE_PER_QUARTER_CELL  // const
METHODS:
  RVA=0x0B10F4A0  token=0x60000EC  System.Void SetIndex(System.Int32 index)
  RVA=0x0B10F2C4  token=0x60000ED  System.Void SetDashCount(System.Single dashFillCount)
  RVA=0x0B10F564  token=0x60000EE  System.Void StartPlayNotEnoughAnim()
  RVA=0x0B10F614  token=0x60000EF  System.Void StopPlayNotEnoughAnim()
  RVA=0x05393520  token=0x60000F0  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.HeadBarCtrl
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x160
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  private           UnityEngine.GameObject          _mobHeadBarPrefab  // 0x70
  private           UnityEngine.GameObject          _eliteHeadBarPrefab  // 0x78
  private           UnityEngine.GameObject          _enemyPartHeadBarPrefab  // 0x80
  private           UnityEngine.GameObject          _levelInfoBarPrefab  // 0x88
  private           UnityEngine.GameObject          _vigilanceBarPrefab  // 0x90
  private           UnityEngine.GameObject          _tacticalItemBarPrefab  // 0x98
  private           UnityEngine.GameObject          _headBuffPrefab  // 0xa0
  private           System.Single                   _hideHeadBarNotInFightDelay  // 0xa8
  private           System.Int32                    _headBarInvisibleEnemyCount  // 0xac
  private           System.Single                   _headBarInvisibleHpNoChangeTime  // 0xb0
  private           System.Single                   _headBarInvisibleScreenEdgeRatio  // 0xb4
  private           System.Single                   _headBarInvisibleDistance  // 0xb8
  private   static  System.Single                   TICK_SURROUNDING_INTERVAL  // const
  private   static  System.String                   HEAD_BAR_BROKEN_ANIM  // const
  private   static readonly System.Int32[]                  HEAD_BAR_PREWARM_CNT  // static @ 0x0
  private           UnityEngine.Transform           m_worldObjectRoot  // 0xc0
  private   static  System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>s_abilitySystemsToShowBar  // static @ 0x8
  private   static  System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>s_abilitySystemsToHideBar  // static @ 0x10
  private   static  System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>s_surroundingEnemies  // static @ 0x18
  private           System.Single                   m_lastTickSurrounding  // 0xc8
  private           System.Boolean                  m_inDialog  // 0xcc
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UIHeadBarFollower>m_headBarDict  // 0xd0
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UIHeadBarFollower>m_headBarWithOutAnimDict  // 0xd8
  private           System.Collections.Generic.List<Beyond.UI.UIHeadBarFollower>m_mobHeadBarPool  // 0xe0
  private           System.Collections.Generic.List<Beyond.UI.UIHeadBarFollower>m_eliteHeadBarPool  // 0xe8
  private           System.Collections.Generic.List<Beyond.UI.UIHeadBarFollower>m_enemyPartHeadBarPool  // 0xf0
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.Gameplay.Core.AbilitySystem.Modifier>m_nonFullHpAbilitySystems  // 0xf8
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,System.ValueTuple<Beyond.Gameplay.Core.AbilitySystem.Modifier,Beyond.Gameplay.PeriodicTimer>>m_enemyFirstDamageDict  // 0x100
  private           System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>m_enabledEnemyPartsByAction  // 0x108
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UILvInfoBar>m_levelInfoBarDict  // 0x110
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UILvInfoBar>m_levelInfoBarWithOutAnimDict  // 0x118
  private           System.Collections.Generic.List<Beyond.UI.UILvInfoBar>m_levelInfoBarPool  // 0x120
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UIVigilanceBar>m_vigilanceBarDict  // 0x128
  private           System.Collections.Generic.List<Beyond.UI.UIVigilanceBar>m_vigilanceBarPool  // 0x130
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UITacticalItemBar>m_tacticalItemBarDict  // 0x138
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UITacticalItemBar>m_tacticalItemBarWithOutAnimDict  // 0x140
  private           System.Collections.Generic.List<Beyond.UI.UITacticalItemBar>m_tacticalItemBarPool  // 0x148
  private           System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.UIHeadBuff>m_headBuffDict  // 0x150
  private           System.Collections.Generic.List<Beyond.UI.UIHeadBuff>m_headBuffPool  // 0x158
PROPERTIES:
  tickOption  get=0x04658830
  forceHide  get=0x038C9480
METHODS:
  RVA=0x04249710  token=0x60000F3  System.Void OnCreate(UnityEngine.Transform worldObjectRoot)
  RVA=0x0B10F6C4  token=0x60000F4  System.Void OnClose()
  RVA=0x04514590  token=0x60000F5  System.Void OnShow()
  RVA=0x04CD9700  token=0x60000F6  System.Void OnHide()
  RVA=0x038C8DF0  token=0x60000F7  System.Void LateTick(System.Single deltaTime)
  RVA=0x03F45C50  token=0x60000F8  System.Void _OnAddHeadBar(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem>& eventData)
  RVA=0x03F45C90  token=0x60000F9  System.Void _OnRemoveHeadBar(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,System.Boolean>& eventData)
  RVA=0x04D2BB30  token=0x60000FA  System.Void _OnHeadBarShowConfigChanged()
  RVA=0x04242F60  token=0x60000FB  System.Void _OnHpChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem.Modifier>& eventData)
  RVA=0x0B110B24  token=0x60000FC  System.Void _OnEnemyPartEnableChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystemForEnemyPart,System.Boolean>& eventData)
  RVA=0x03F45CD0  token=0x60000FD  System.Void _RefreshHeadBar(Beyond.UI.HeadBarCtrl.HeadBarRefreshReason reason)
  RVA=0x039C2430  token=0x60000FE  System.Void _PrewarmHeadBar()
  RVA=0x0375CCB0  token=0x60000FF  Beyond.UI.HeadBarCtrl.HeadBarType _GetHeadBarType(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x038CB040  token=0x6000100  System.Void _UpdateHeadBar(System.Single deltaTime)
  RVA=0x038C9500  token=0x6000101  System.Void _UpdateLevelInfoBar(System.Single deltaTime)
  RVA=0x038CA4F0  token=0x6000102  System.Void _UpdateVigilanceBar(System.Single deltaTime)
  RVA=0x02F53D60  token=0x6000103  System.Void _UpdateTacticalItemBar(System.Single deltaTime)
  RVA=0x0B1109E4  token=0x6000104  System.Void _OnBountyEnemyChanged()
  RVA=0x0B110CA4  token=0x6000105  System.Void _RefreshHeadBuff()
  RVA=0x0B1106EC  token=0x6000106  System.String _FindBountyBuffIcon(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x038C8EA0  token=0x6000107  System.Void _UpdateSurroundingEnemies(System.Single deltaTime)
  RVA=-1  // generic def  token=0x6000108  T _CreateBar(Beyond.UI.HeadBarCtrl.HeadBarType headBarType)
  RVA=-1  // generic def  token=0x6000109  System.Void _RecycleBar(T bar)
  RVA=-1  // generic def  token=0x600010A  System.Collections.Generic.List<T> _GetBarPool(Beyond.UI.HeadBarCtrl.HeadBarType headBarType)
  RVA=-1  // generic def  token=0x600010B  UnityEngine.GameObject _GetBarPrefab(Beyond.UI.HeadBarCtrl.HeadBarType headBarType)
  RVA=0x0B110C30  token=0x600010C  System.Void _OnInteractNpc(Beyond.EventData<System.String,Beyond.Gameplay.Core.Entity>& eventData)
  RVA=0x0B110AA8  token=0x600010D  System.Void _OnDialogStart(Beyond.EventData<System.Boolean,Beyond.Gameplay.DialogEnums.DialogType>& eventData)
  RVA=0x0B110A34  token=0x600010E  System.Void _OnDialogFinish(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B111620  token=0x600010F  System.Void _SetInDialog(System.Boolean inDialog)
  RVA=0x03ECC370  token=0x6000110  System.Void .ctor()
  RVA=0x03ECC920  token=0x6000111  System.Void .cctor()
  RVA=0x03F47CB0  token=0x6000112  System.Boolean <_RefreshHeadBar>g__ToShow|55_0(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x0B10A3CC  token=0x6000113  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000114  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.HpChangeDisplayData
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x28
FIELDS:
  public            System.Int32                    priority  // 0x10
  public            System.Single                   minDamagePercent  // 0x14
  public            System.Single                   maxDamagePercent  // 0x18
  public            System.Single                   stayTime  // 0x1c
  public            System.Single                   decreaseSpeed  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600011C  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIHeadBar
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x148
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  _showHpNumber  // 0x18
  private           System.Boolean                  _showHpChangeEffect  // 0x19
  private           System.Collections.Generic.List<Beyond.UI.HpChangeDisplayData>_hpChangeConfig  // 0x20
  private           System.Boolean                  _hidePoiseBarWhenEmpty  // 0x28
  private           System.Boolean                  _showPoiseChangeEffect  // 0x29
  private           System.Collections.Generic.List<Beyond.UI.HpChangeDisplayData>_poiseChangeConfig  // 0x30
  private           System.Boolean                  _highlightWhenWeakLock  // 0x38
  private           System.Single                   _weakLockHighlightScale  // 0x3c
  private           System.Single                   _weakLockScaleTransitionTime  // 0x40
  private           System.Int32                    _lvFontSize  // 0x44
  private           Beyond.UI.UIAnimationWrapper    _barRootAnim  // 0x48
  private           Beyond.UI.UIState.UIStateController_lvNodeState  // 0x50
  private           Beyond.UI.UIText                _lvText  // 0x58
  private           Beyond.UI.UIText                _dangerousLvText  // 0x60
  private           Beyond.UI.UIText                _extremeDangerousLvText  // 0x68
  private           Beyond.UI.UIAnimationWrapper    _extremeDangerousLvAnim  // 0x70
  private           UnityEngine.CanvasGroup         _focusHighlight  // 0x78
  private           System.Collections.Generic.List<Beyond.UI.UIImage>_hpFastFillList  // 0x80
  private           System.Collections.Generic.List<Beyond.UI.UIImage>_hpSlowFillList  // 0x88
  private           UnityEngine.Transform           _hpNumberNode  // 0x90
  private           Beyond.UI.UIText                _curHpText  // 0x98
  private           Beyond.UI.UIText                _maxHpText  // 0xa0
  private           UnityEngine.RectTransform       _hpParentRect  // 0xa8
  private           UnityEngine.RectTransform       _hpEffectRect  // 0xb0
  private           UnityEngine.CanvasGroup         _hpEffectCanvasGroup  // 0xb8
  private           UnityEngine.GameObject          _hpBrokenNode  // 0xc0
  private           Beyond.UI.UIPoiseBar            _poiseBar  // 0xc8
  private           Beyond.UI.UIWeaknessNode        _weaknessBar  // 0xd0
  private           Beyond.UI.UIBuffNode            _attachedBuffNode  // 0xd8
  private           Beyond.UI.UIBuffNode            _normalBuffNode  // 0xe0
  private   readonly Beyond.LuaResourceLoader        m_loader  // 0xe8
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_target  // 0xf0
  private           System.Boolean                  m_hasPoise  // 0x100
  private           Beyond.UI.HpChangeDisplayData   m_currentHpChangeData  // 0x108
  private           Beyond.UI.UIHeadBar.HpDisplayStatem_hpDisplayState  // 0x110
  private   readonly Beyond.Gameplay.PeriodicTimer   m_hpStayTimer  // 0x118
  private           System.Nullable<System.Single>  m_targetFillValue  // 0x120
  private           System.Nullable<System.Single>  m_transferToHighlightScaleTime  // 0x128
  private           System.Nullable<System.Single>  m_transferToOriginalScaleTime  // 0x130
  private           UnityEngine.Vector3             m_beforeTransferScale  // 0x138
  public    static  System.Single                   EFFECT_OFFSET  // const
METHODS:
  RVA=0x0B118034  token=0x600011D  System.Void OnDestroy()
  RVA=0x039C32A0  token=0x600011E  System.Void OnPreWarm()
  RVA=0x03A9EE50  token=0x600011F  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x03B4FDD0  token=0x6000120  System.Void ShowModifierAfterSetTarget(Beyond.Gameplay.Core.AbilitySystem.Modifier& modifier)
  RVA=0x03A9FD20  token=0x6000121  System.Void Clear()
  RVA=0x03AA2250  token=0x6000122  System.Void Refresh()
  RVA=0x03B50FD0  token=0x6000123  System.Void _OnHpChange(Beyond.Gameplay.Core.Entity entity, System.Double change, System.Boolean isMaxHpChanged)
  RVA=0x0B118090  token=0x6000124  System.Void _OnForceSetHp(Beyond.Gameplay.Core.Entity entity)
  RVA=0x03B52130  token=0x6000125  System.Void _CalculateHpChangeDisplay(Beyond.Gameplay.Core.AbilitySystem target, System.Double deltaValue)
  RVA=0x035F85D0  token=0x6000126  System.Void _UpdateHpDisplay(System.Single deltaTime)
  RVA=0x04A1D4E0  token=0x6000127  System.Void _OnPoiseChange(Beyond.Gameplay.Core.Entity entity, System.Single change, System.Boolean hideUIEffect)
  RVA=0x0B118258  token=0x6000128  System.Void _OnMultiTimeWeaknessActivate(Beyond.Gameplay.Core.Entity entity, System.Boolean active, System.Int32 count)
  RVA=0x0B1183CC  token=0x6000129  System.Void _OnWeaknessCountChanged(Beyond.Gameplay.Core.Entity entity, System.Int32 total, System.Int32 count)
  RVA=0x0464D8F0  token=0x600012A  System.Void _OnMainTargetChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,System.Boolean>& eventData)
  RVA=0x035F7D60  token=0x600012B  System.Void _UpdateScale(System.Single deltaTime)
  RVA=0x035F7C40  token=0x600012C  System.Void _SetImageFill(System.Collections.Generic.List<Beyond.UI.UIImage> imageList, System.Single fillValue)
  RVA=0x035F9450  token=0x600012D  System.Void UpdateData(System.Single deltaTime)
  RVA=0x049CDC60  token=0x600012E  System.Void OnBeforeHpBroken()
  RVA=0x039C2250  token=0x600012F  System.Void OnAfterHpBroken()
  RVA=0x039C49E0  token=0x6000130  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIHeadBarFollower
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x150
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  public            Beyond.UI.HeadBarCtrl.HeadBarTypeheadBarType  // 0x110
  private           System.Boolean                  _haveGuideLine  // 0x114
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x118
  private           Beyond.UI.UIHeadBar             _headBar  // 0x120
  private           UnityEngine.GameObject          _guideLineNode  // 0x128
  private           UnityEngine.RectTransform       _guideLine  // 0x130
  private           UnityEngine.RectTransform       _followTargetEndPoint  // 0x138
  private           UnityEngine.RectTransform       _headBarEndPoint  // 0x140
  private           System.Boolean                  m_headBarVisible  // 0x148
PROPERTIES:
  actualVisible  get=0x03EC1440
METHODS:
  RVA=0x039C2AB0  token=0x6000132  System.Void OnPreWarm()
  RVA=0x033D24E0  token=0x6000133  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x03B4FD80  token=0x6000134  System.Void ShowModifierAfterSetTarget(Beyond.Gameplay.Core.AbilitySystem.Modifier& modifier)
  RVA=0x03A9F9B0  token=0x6000135  System.Void Clear()
  RVA=0x0B117F7C  token=0x6000136  System.Void Refresh()
  RVA=0x044C80D0  token=0x6000137  System.Void SetHeadBarVisible(System.Boolean visible)
  RVA=0x035F93F0  token=0x6000138  System.Void UpdateData(System.Single deltaTime)
  RVA=0x044E5DD0  token=0x6000139  System.Void _UpdateGuideLine()
  RVA=0x033D4DC0  token=0x600013A  System.Boolean TryGetOriginalFollowPos(UnityEngine.Vector3& followPos)
  RVA=0x049CDC10  token=0x600013B  System.Void OnBeforeHpBroken()
  RVA=0x039C2200  token=0x600013C  System.Void OnAfterHpBroken()
  RVA=0x039C2B10  token=0x600013D  System.Void .ctor()
  RVA=0x0B11802C  token=0x600013E  System.Boolean <>iFixBaseProxy_get_actualVisible()
  RVA=0x0B118014  token=0x600013F  System.Void <>iFixBaseProxy_SetTarget(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B11800C  token=0x6000140  System.Void <>iFixBaseProxy_Clear()
  RVA=0x0B118024  token=0x6000141  System.Void <>iFixBaseProxy_UpdateData(System.Single P0)
  RVA=0x0B11801C  token=0x6000142  System.Boolean <>iFixBaseProxy_TryGetOriginalFollowPos(UnityEngine.Vector3& P0)
END_CLASS

CLASS: Beyond.UI.UIHeadBuff
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x118
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  private           Beyond.UI.UIState.UIStateController_stateController  // 0x110
METHODS:
  RVA=0x0B118498  token=0x6000143  System.Void SetBuffIcon(System.String iconName)
  RVA=0x0B118514  token=0x6000144  System.Boolean TryGetOriginalFollowPos(UnityEngine.Vector3& followPos)
  RVA=0x039C1E70  token=0x6000145  System.Void .ctor()
  RVA=0x0B11801C  token=0x6000146  System.Boolean <>iFixBaseProxy_TryGetOriginalFollowPos(UnityEngine.Vector3& P0)
END_CLASS

CLASS: Beyond.UI.UIHeadLabel
TYPE:  class
TOKEN: 0x2000039
SIZE:  0xF0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           UnityEngine.Transform           _emojiNode  // 0x68
  private           Beyond.Gameplay.Core.FloatingUIFitter_rootDis  // 0x70
  private           Beyond.Gameplay.Core.ObjectType <targetType>k__BackingField  // 0x78
  private           System.UInt64                   <entityLogicId>k__BackingField  // 0x80
  private           System.String                   m_headLabelIcon  // 0x88
  private           System.String                   m_headLabelIconOverwrite  // 0x90
  private           UnityEngine.Vector3             m_originalPosition  // 0x98
  private           System.Boolean                  m_hasBindEvent  // 0xa4
  private           System.Boolean                  m_hasBindTargetEvent  // 0xa5
  private           Beyond.SerializeFieldDictionary<Beyond.UI.UIHeadLabel.HeadLabelType,Beyond.UI.UIHeadLabel.HeadLabelRootData>_subRoots  // 0xa8
  private           System.Collections.Generic.Dictionary<Beyond.UI.UIHeadLabel.HeadLabelType,Beyond.UI.UIHeadLabel.HeadLabelRootVisibleData>m_subRootsVisible  // 0xb0
  private           System.Collections.Generic.Dictionary<UnityEngine.Transform,Beyond.UI.UIHeadLabel.HeadLabelType>m_subRootsReverse  // 0xb8
  private           UnityEngine.Transform           m_camera  // 0xc0
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity>m_target  // 0xc8
  private           UnityEngine.Transform           m_followTarget  // 0xd8
  private           Beyond.NPC.Avatar.NPCAvatarMountPointInfom_npcMountPointInfo  // 0xe0
  private           System.Boolean                  m_inited  // 0xe8
  private           System.Boolean                  m_visibleDirty  // 0xe9
PROPERTIES:
  tickOption  get=0x04281220
  iconHolder  get=0x0B118A30
  targetType  get=0x04D863C0  set=0x04D86440
  entityLogicId  get=0x04D86260  set=0x04D862E0
  headLabelIcon  get=0x04790910  set=0x036D21F0
  missionHeadLabelIcon  get=0x047908E0  set=0x036D21A0
METHODS:
  RVA=0x0B1188A8  token=0x600014D  System.Void _OnSubRootsCollectionChanged()
  RVA=0x036CE7B0  token=0x6000152  System.Void _TryInitRootData()
  RVA=0x036D0D10  token=0x6000153  System.Void SetVisibleDirty()
  RVA=0x036CFE00  token=0x6000154  System.Void _UpdateVisible()
  RVA=0x036CFCF0  token=0x6000155  System.Void SetSubRootVisible(Beyond.UI.UIHeadLabel.HeadLabelType type, Beyond.UI.UIHeadLabel.VisibleSource source, System.Boolean visible, System.Boolean updateNow)
  RVA=0x036CF980  token=0x6000156  System.Void SetTarget(Beyond.Gameplay.Core.Entity target)
  RVA=0x036D0ED0  token=0x6000157  System.Void _TryUpdateVisibleCallback()
  RVA=0x045BAAE0  token=0x6000158  System.Void _ClearVisibleCallback()
  RVA=0x036D2240  token=0x6000159  System.Void Clear()
  RVA=0x0B118684  token=0x600015A  System.Void SetActive(System.Boolean active)
  RVA=0x036D0D40  token=0x600015B  System.Void ClearEmojis()
  RVA=0x03962710  token=0x600015C  System.Void _UpdateFloatingUIVisibleCallback()
  RVA=0x03961DB0  token=0x600015D  System.Void _ClearFloatingUIVisibleCallback()
  RVA=0x0B118764  token=0x600015E  System.Void _OnMissionTypeIconChanged(Beyond.EventData<System.String>& data)
  RVA=0x036D26B0  token=0x600015F  System.Void _BindMissionTracking()
  RVA=0x036D23C0  token=0x6000160  System.Void _UnBindMissionTracking()
  RVA=0x036D2590  token=0x6000161  System.Void _BindTargetEvent()
  RVA=0x036D24D0  token=0x6000162  System.Void _UnBindTargetEvent()
  RVA=0x03C1AE50  token=0x6000163  System.Void _OnNpcVisibleChanged(Beyond.EventData<System.String>& eventData)
  RVA=0x02F451B0  token=0x6000164  System.Void _UpdatePos()
  RVA=0x036CFBF0  token=0x6000165  System.Void _RefreshVisible(System.Boolean bubbleRoot, System.Boolean textNode, System.Boolean iconHolderNode)
  RVA=0x02F44E30  token=0x6000166  System.Void _TryUpdateVisible()
  RVA=0x036CE6B0  token=0x6000167  System.Void OnAwake()
  RVA=0x02F450F0  token=0x6000168  System.Void LateTick(System.Single deltaTime)
  RVA=0x036D2650  token=0x6000169  System.Void OnEnable()
  RVA=0x036D2360  token=0x600016A  System.Void OnDisable()
  RVA=0x0B118620  token=0x600016B  System.Void OnRelease()
  RVA=0x039C3300  token=0x600016C  System.Void .ctor()
  RVA=0x069AFD0C  token=0x600016D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600016E  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06BD8710  token=0x600016F  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B40  token=0x6000170  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x6000171  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x069AFD04  token=0x6000172  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UILvInfoBar
TYPE:  class
TOKEN: 0x200003F
SIZE:  0x140
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x110
  private           Beyond.UI.UIState.UIStateController_lvNodeState  // 0x118
  private           Beyond.UI.UIText                _lvText  // 0x120
  private           Beyond.UI.UIText                _dangerousLvText  // 0x128
  private           Beyond.UI.UIText                _extremeDangerousLvText  // 0x130
  private           Beyond.UI.UIAnimationWrapper    _extremeDangerousLvAnim  // 0x138
METHODS:
  RVA=0x03CEA140  token=0x600017A  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x033D4E90  token=0x600017B  System.Boolean TryGetOriginalFollowPos(UnityEngine.Vector3& followPos)
  RVA=0x039C1E70  token=0x600017C  System.Void .ctor()
  RVA=0x0B118014  token=0x600017D  System.Void <>iFixBaseProxy_SetTarget(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B11801C  token=0x600017E  System.Boolean <>iFixBaseProxy_TryGetOriginalFollowPos(UnityEngine.Vector3& P0)
END_CLASS

CLASS: Beyond.UI.UIPoiseBar
TYPE:  class
TOKEN: 0x2000040
SIZE:  0xD0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIImage               _increaseFill  // 0x18
  private           UnityEngine.RectTransform       _increaseHead  // 0x20
  private           UnityEngine.CanvasGroup         _increaseCanvasGroup  // 0x28
  private           Beyond.UI.UIImage               _decreaseFill  // 0x30
  private           UnityEngine.CanvasGroup         _decreaseCanvasGroup  // 0x38
  private           Beyond.UI.UIPoiseKnot           _poiseKnot  // 0x40
  private           Beyond.UI.UIAnimationWrapper    _poiseBreakAnim  // 0x48
  private           UnityEngine.CanvasGroup         _poiseBreakCanvasGroup  // 0x50
  private           UnityEngine.RectTransform       _poiseParentRect  // 0x58
  private           UnityEngine.RectTransform       _poiseEffectRect  // 0x60
  private           UnityEngine.CanvasGroup         _poiseEffectCanvasGroup  // 0x68
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_owner  // 0x70
  private           System.Boolean                  m_isInPoiseRecovery  // 0x80
  private           System.Boolean                  m_hidePoiseBarWhenEmpty  // 0x81
  private           System.Boolean                  m_showPoiseChangeEffect  // 0x82
  private           System.Collections.Generic.List<Beyond.UI.HpChangeDisplayData>m_poiseChangeConfig  // 0x88
  private           Beyond.UI.HpChangeDisplayData   m_currentPoiseChangeData  // 0x90
  private           Beyond.UI.UIPoiseBar.PoiseDisplayStatem_poiseDisplayState  // 0x98
  private   readonly Beyond.Gameplay.PeriodicTimer   m_poiseStayTimer  // 0xa0
  private           System.Nullable<System.Single>  m_targetFillValue  // 0xa8
  private           System.Single                   m_slowFillValue  // 0xb0
  private           System.Boolean                  m_enemyPartHide  // 0xb4
  private           System.Collections.Generic.List<Beyond.UI.UIPoiseKnot>m_poiseKnotList  // 0xb8
  private           System.Collections.Generic.List<Beyond.UI.UIPoiseKnot>m_poiseKnotPool  // 0xc0
  private           System.Boolean                  m_isPoiseKnotInited  // 0xc8
  public    static  System.Single                   EFFECT_OFFSET  // const
PROPERTIES:
  owner  get=0x035F9590
  maxPoise  get=0x03CEFD70
  realPoise  get=0x03CEED80
  hidePoiseBar  get=0x03CEEDD0
METHODS:
  RVA=0x03CECEB0  token=0x6000183  System.Void Init(Beyond.Gameplay.Core.AbilitySystem owner, System.Boolean hidePoiseBarWhenEmpty, System.Boolean showPoiseChangeEffect, System.Collections.Generic.List<Beyond.UI.HpChangeDisplayData> poiseChangeConfig)
  RVA=0x03A9FC70  token=0x6000184  System.Void Clear()
  RVA=0x03CED1D0  token=0x6000185  System.Void OnPoiseChange(Beyond.Gameplay.Core.Entity entity, System.Single change, System.Boolean hideUIEffect)
  RVA=0x03AA2370  token=0x6000186  System.Void Refresh()
  RVA=0x035F94E0  token=0x6000187  System.Void UpdatePoiseBar(System.Single deltaTime)
  RVA=0x03CEFDC0  token=0x6000188  System.Void _CalculatePoiseChangeDisplay(System.Double deltaValue)
  RVA=0x035F8D80  token=0x6000189  System.Void _UpdatePoiseDisplay(System.Single deltaTime)
  RVA=0x03B1AB70  token=0x600018A  System.Void _TryInitPoiseKnot(Beyond.Gameplay.Core.AbilitySystem owner)
  RVA=0x03A9FAC0  token=0x600018B  System.Void _ClearPoiseKnot()
  RVA=0x0B118AD0  token=0x600018C  Beyond.UI.UIPoiseKnot _CreatePoiseKnot()
  RVA=0x04563E40  token=0x600018D  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UITacticalItemBar
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x138
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  private           System.Single                   _castFillMin  // 0x110
  private           System.Single                   _castFillMax  // 0x114
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x118
  private           Beyond.UI.UIImage               _castFill  // 0x120
  private           Beyond.UI.UIImage               _itemIcon  // 0x128
  private   static  System.String                   UI_SPRITE_ITEM  // const
  private           System.String                   m_itemId  // 0x130
METHODS:
  RVA=0x0B118DF8  token=0x600018E  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x0B118C88  token=0x600018F  System.Void Clear()
  RVA=0x0B119004  token=0x6000190  System.Void UpdateData(System.Single deltaTime)
  RVA=0x0B118D10  token=0x6000191  System.Boolean IsTacticalItemActive()
  RVA=0x039C1E80  token=0x6000192  System.Void .ctor()
  RVA=0x0B118014  token=0x6000193  System.Void <>iFixBaseProxy_SetTarget(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B11800C  token=0x6000194  System.Void <>iFixBaseProxy_Clear()
  RVA=0x0B118024  token=0x6000195  System.Void <>iFixBaseProxy_UpdateData(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIVigilanceBar
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x138
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  private           Beyond.UI.UIAnimationWrapper    _vigilanceNode  // 0x110
  private           Beyond.UI.UIImage               _vigilanceFill  // 0x118
  private           UnityEngine.CanvasGroup         _vigilanceDim  // 0x120
  private           UnityEngine.CanvasGroup         _vigilanceLight  // 0x128
  private           System.Boolean                  m_isVigilanceActive  // 0x130
  private           System.Boolean                  m_isVigilanceFull  // 0x131
  private           System.Boolean                  m_isVigilanceVisible  // 0x132
PROPERTIES:
  actualVisible  get=0x03EC1480
  needUpdateTransform  get=0x03FC21F0
METHODS:
  RVA=0x033D2550  token=0x6000198  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x03A9F970  token=0x6000199  System.Void Clear()
  RVA=0x0B1190BC  token=0x600019A  System.Void Refresh()
  RVA=0x037FCE70  token=0x600019B  System.Void UpdateData(System.Single deltaTime)
  RVA=0x037FD200  token=0x600019C  System.Void _UpdateVigilanceNode()
  RVA=0x0464C940  token=0x600019D  System.Void _SetVigilanceActive(System.Boolean active)
  RVA=0x033D4F60  token=0x600019E  System.Boolean TryGetOriginalFollowPos(UnityEngine.Vector3& followPos)
  RVA=0x039C1E70  token=0x600019F  System.Void .ctor()
  RVA=0x04D84A80  token=0x60001A0  System.Void <_UpdateVigilanceNode>b__15_0()
  RVA=0x04D84EF0  token=0x60001A1  System.Void <_UpdateVigilanceNode>b__15_1()
  RVA=0x0B11802C  token=0x60001A2  System.Boolean <>iFixBaseProxy_get_actualVisible()
  RVA=0x0B1191E8  token=0x60001A3  System.Boolean <>iFixBaseProxy_get_needUpdateTransform()
  RVA=0x0B118014  token=0x60001A4  System.Void <>iFixBaseProxy_SetTarget(Beyond.Gameplay.Core.AbilitySystem P0)
  RVA=0x0B11800C  token=0x60001A5  System.Void <>iFixBaseProxy_Clear()
  RVA=0x0B118024  token=0x60001A6  System.Void <>iFixBaseProxy_UpdateData(System.Single P0)
  RVA=0x0B11801C  token=0x60001A7  System.Boolean <>iFixBaseProxy_TryGetOriginalFollowPos(UnityEngine.Vector3& P0)
END_CLASS

CLASS: Beyond.UI.UIWeaknessNode
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.CanvasGroup         weaknessCell  // 0x18
  private   readonly System.Collections.Generic.List<UnityEngine.CanvasGroup>m_weaknessCells  // 0x20
METHODS:
  RVA=0x0B1191F0  token=0x60001A8  System.Void Awake()
  RVA=0x0B11935C  token=0x60001A9  System.Void SetTotalCellNum(System.Int32 num)
  RVA=0x0B119290  token=0x60001AA  System.Void SetCurrentCellNum(System.Int32 num)
  RVA=0x046D9FD0  token=0x60001AB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.AtbCell
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIImage               fill  // 0x18
  public            Beyond.UI.UIAnimationWrapper    fillLoop  // 0x20
  public            UnityEngine.RectTransform       blinkRect  // 0x28
  public            Beyond.UI.UIAnimationWrapper    recover  // 0x30
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x38
  public            UnityEngine.UI.LayoutElement    layoutElement  // 0x40
  public            System.Single                   recoverInitFillAmount  // 0x48
  public            System.Boolean                  atbRecovering  // 0x4c
METHODS:
  RVA=0x03695570  token=0x60001AC  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.AtbNode
TYPE:  class
TOKEN: 0x2000046
SIZE:  0xC8
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            Beyond.UI.AtbCell               atbCell  // 0x70
  public            UnityEngine.RectTransform       atbGroup  // 0x78
  public            Beyond.UI.UIAnimationWrapper    atbSkillHint  // 0x80
  public            UnityEngine.Color               atbColorFull  // 0x88
  public            UnityEngine.Color               atbColorNotFull  // 0x98
  public            System.Single                   atbFillSpeed  // 0xa8
  public            System.String                   atbHintFillOnEvent  // 0xb0
  private   readonly System.Collections.Generic.List<Beyond.UI.AtbCell>m_atbValueCellList  // 0xb8
  private           System.Boolean                  m_atbShowing  // 0xc0
  private           System.Single                   m_showingAtbValue  // 0xc4
  private   static  System.Single                   ATB_PRESS_THRESHOLD  // const
PROPERTIES:
  tickOption  get=0x04A79A80
  battleManager  get=0x02F587A0
  skillSetting  get=0x03CD5D60
METHODS:
  RVA=0x03CD4D40  token=0x60001B0  System.Void OnCreate()
  RVA=0x0B114B8C  token=0x60001B1  System.Void OnClose()
  RVA=0x02F57CD0  token=0x60001B2  System.Void LateTick(System.Single deltaTime)
  RVA=0x03CD3A50  token=0x60001B3  System.Void CheckAtbLoopAnim()
  RVA=0x03CD5690  token=0x60001B4  System.Void _RefreshAtbShowing()
  RVA=0x04D33140  token=0x60001B5  System.Void _OnAtbCellOutAnimCallback()
  RVA=0x03CD5520  token=0x60001B6  System.Void OnInFightChanged(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B114D10  token=0x60001B7  System.Void OnGainAtb(Beyond.EventData<System.Single>& eventData)
  RVA=0x04CB67A0  token=0x60001B8  System.Void .ctor()
  RVA=0x0B10A3CC  token=0x60001B9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x60001BA  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ComboSkillHint
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.String                   SPECIAL_NODE_PATH  // const
  public            UnityEngine.CanvasGroup         content  // 0x18
  public            Beyond.UI.UIButton              button  // 0x20
  public            Beyond.UI.UIImage               fill  // 0x28
  public            Beyond.UI.UIImage               icon  // 0x30
  public            Beyond.UI.UIImage               charHead  // 0x38
  public            UnityEngine.GameObject          maskNode  // 0x40
  public            Beyond.UI.UIImage               mask2Img  // 0x48
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x50
  public            Beyond.UI.ComboSpecialNodeBase  specialNode  // 0x58
  public            System.Boolean                  needTick  // 0x60
  public            System.Int32                    charIndex  // 0x64
  private           Beyond.UI.ComboSkillPanel       m_owner  // 0x68
  private           Beyond.Gameplay.Core.SkillData  m_cachedSkillData  // 0x70
  private           System.String                   m_lastSpecialNodeName  // 0x78
METHODS:
  RVA=0x03C42990  token=0x60001BD  System.Void Init(Beyond.UI.ComboSkillPanel owner, System.Int32 charIndex, System.Boolean isDefaultPanel)
  RVA=0x0B115210  token=0x60001BE  System.Void OnReady()
  RVA=0x0B1151A4  token=0x60001BF  System.Void OnCast()
  RVA=0x0B1152E4  token=0x60001C0  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B115278  token=0x60001C1  System.Void OnRemove()
  RVA=0x0B115350  token=0x60001C2  System.Void Release()
  RVA=0x03C42800  token=0x60001C3  System.Void AssignData(Beyond.Gameplay.Core.AbilitySystem abilityCom)
  RVA=0x0B1153BC  token=0x60001C4  System.Void SetVisible(System.Boolean visible)
  RVA=0x04A27CC0  token=0x60001C5  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.ComboSkillPanel
TYPE:  class
TOKEN: 0x2000049
SIZE:  0xB0
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public    static  System.String                   UI_SPRITE_ROUND_CHAR_HEAD  // const
  public            Beyond.UI.UIButton              buttonCast  // 0x70
  public            UnityEngine.RectTransform       infoNode  // 0x78
  public            Beyond.UI.ComboSkillHint[]      comboSkillHints  // 0x80
  public            Beyond.UI.UICustomLayoutElement customLayoutElement  // 0x88
  public            Beyond.UI.UIState.UIStateControllerinfoNodeStateController  // 0x90
  public            System.Single                   comboHintDisableAlpha  // 0x98
  public            System.Single                   comboHintFirstScale  // 0x9c
  private   readonly System.Collections.Generic.List<System.Int32>m_charIndexList  // 0xa0
  private           UnityEngine.Events.UnityAction  m_onLayoutDataLoaded  // 0xa8
PROPERTIES:
  tickOption  get=0x04A79A50
METHODS:
  RVA=0x03C421B0  token=0x60001C7  System.Void OnCreate(System.Boolean isDefaultPanel, System.Boolean isControllerPanel)
  RVA=0x03C425E0  token=0x60001C8  System.Void OnShow()
  RVA=0x0B115778  token=0x60001C9  System.Void OnRelease()
  RVA=0x03C42CD0  token=0x60001CA  System.Void _UnbindListeners()
  RVA=0x0B115828  token=0x60001CB  System.Void PreTick(System.Single deltaTime)
  RVA=0x03409BF0  token=0x60001CC  System.Void _CheckUpdate()
  RVA=0x03C426C0  token=0x60001CD  System.Void _OnBattleTeamChanged()
  RVA=0x0B115A84  token=0x60001CE  System.Void _OnActiveSkillDataRefreshed(Beyond.EventData<System.UInt64,System.String>& eventData)
  RVA=0x0B115C00  token=0x60001CF  System.Void _OnComboSkillReady(Beyond.EventData<System.Int32>& eventData)
  RVA=0x0B115DB4  token=0x60001D0  System.Void _OnComboSkillRemove(Beyond.EventData<System.Int32>& eventData)
  RVA=0x0B115E18  token=0x60001D1  System.Void _RemoveComboSkill(System.Int32 charIndex)
  RVA=0x03409E30  token=0x60001D2  System.Void _OnComboSkillClearAll()
  RVA=0x0B115418  token=0x60001D3  System.Void CastComboSkill(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03C42620  token=0x60001D4  System.Void _ResortSiblingIndex()
  RVA=0x04CAB300  token=0x60001D5  System.Void .ctor()
  RVA=0x0B115A04  token=0x60001D6  System.Void <OnCreate>b__12_0()
  RVA=0x0B10A3CC  token=0x60001D7  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B115A7C  token=0x60001D8  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06FF4E4C  token=0x60001D9  System.Void <>iFixBaseProxy_PreTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.GPUIAttachedBuffCell
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x118
EXTENDS: Beyond.UI.GPUIBuffCell
FIELDS:
  private           System.String                   _buffIconPhantomNodeName  // 0xc0
  private           System.String                   _buffStackRingNodeName  // 0xc8
  private           System.String                   _buffStackRingSubRootNodeName  // 0xd0
  private           System.Collections.Generic.List<System.String>_buffStackRingConsumeSubroots  // 0xd8
  private           System.String                   _buffStackRingConsumeAnimationName  // 0xe0
  private           System.Single                   _stackRingFillChangeSpeed  // 0xe8
  private           System.Single                   _igniteFillDelay  // 0xec
  private           System.Single                   _buffStackRingInitRotZ  // 0xf0
  private   static  System.String                   BUFF_ANIM_IGNITE  // const
  private           System.Int32                    m_maxStackCount  // 0xf4
  private           System.Single                   m_currentStackRingFillAmount  // 0xf8
  private           System.Nullable<System.Single>  m_targetStackRingFillAmount  // 0xfc
  private           Beyond.Gameplay.PeriodicTimer   m_igniteFillDelayTimer  // 0x108
  private           System.Boolean                  m_isInConsumeAnim  // 0x110
  private           System.Single                   m_consumeAnimEndTime  // 0x114
PROPERTIES:
  defaultBuffIconNodeName  get=0x0B1168A4
METHODS:
  RVA=0x0B11603C  token=0x60001E1  System.Void DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B116204  token=0x60001E2  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B115FDC  token=0x60001E3  System.Void BeforePlayOutAnim()
  RVA=0x0B1163E8  token=0x60001E4  System.Void SetStackCount(System.Int32 stackCount, System.Boolean playAnim)
  RVA=0x0B116144  token=0x60001E5  System.Void OnBuffIgnite()
  RVA=0x02E99490  token=0x60001E6  System.Boolean SetBuffIcon(UnityEngine.Sprite sprite)
  RVA=0x02E990B0  token=0x60001E7  System.Void ClearBuffIconOverride()
  RVA=0x0B1165D4  token=0x60001E8  System.Void SetStackElementActive(System.Boolean showStackCount)
  RVA=0x0B1166B8  token=0x60001E9  System.Void _PlayConsumeAnimations(System.Int32 stackCount)
  RVA=0x0419E480  token=0x60001EA  System.Void .ctor()
  RVA=0x0B1166B0  token=0x60001EB  System.String <>iFixBaseProxy_get_defaultBuffIconNodeName()
  RVA=0x0B116670  token=0x60001EC  System.Void <>iFixBaseProxy_DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P0)
  RVA=0x0B116698  token=0x60001ED  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
  RVA=0x0B116668  token=0x60001EE  System.Void <>iFixBaseProxy_BeforePlayOutAnim()
  RVA=0x0B1166A0  token=0x60001EF  System.Void <>iFixBaseProxy_SetStackCount(System.Int32 P0, System.Boolean P1)
  RVA=0x0B116690  token=0x60001F0  System.Void <>iFixBaseProxy_OnBuffIgnite()
  RVA=0x02E998C0  token=0x60001F1  System.Boolean <>iFixBaseProxy_SetBuffIcon(UnityEngine.Sprite P0)
  RVA=0x02E998B0  token=0x60001F2  System.Void <>iFixBaseProxy_ClearBuffIconOverride()
  RVA=0x0B1166A8  token=0x60001F3  System.Void <>iFixBaseProxy_SetStackElementActive(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.GPUIBuffCell
TYPE:  class
TOKEN: 0x200004E
SIZE:  0xC0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.String                   BUFF_ICON_SET_FAILED_MESSAGE  // const
  private           Beyond.UI.GPUI.GPUIPrefabProxyBase_gpuiProxy  // 0x18
  private           System.String                   _buffIconNodeName  // 0x20
  private           System.Boolean                  _canShowStackCountText  // 0x28
  private           System.Collections.Generic.List<System.String>_stackNodeNames  // 0x30
  private           System.String                   _stackCountTextNodeName  // 0x38
  private           System.Boolean                  _canShowWarningBg  // 0x40
  private           System.Collections.Generic.List<System.String>_warningBgNodeNames  // 0x48
  private           System.String                   _animationInName  // 0x50
  private           System.String                   _animationOutName  // 0x58
  private           System.String                   _strongInAnimationName  // 0x60
  private           System.String                   _igniteOutAnimationName  // 0x68
  private           UnityEngine.Vector2             _visibleRootScale  // 0x70
  private           UnityEngine.Vector2             _hiddenRootScale  // 0x78
  protected         Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_buffPtr  // 0x80
  private           UnityEngine.Sprite              m_currentSprite  // 0x90
  private           System.Action                   m_animationCompleteCallback  // 0x98
  private           System.Single                   m_animationCompleteTime  // 0xa0
  private           System.Boolean                  m_animationCompletePending  // 0xa4
  private           System.Boolean                  m_isCleared  // 0xa5
  private           System.Boolean                  m_isVisualInited  // 0xa6
  private           System.Boolean                  m_isBuffIconOverrideActive  // 0xa7
  private           System.Boolean                  m_shouldBuffIconOverrideActive  // 0xa8
  private           System.Boolean                  m_visible  // 0xa9
  private           System.UInt64                   <nextInstanceId>k__BackingField  // 0xb0
  private           System.String                   <curSpritePath>k__BackingField  // 0xb8
PROPERTIES:
  defaultBuffIconNodeName  get=0x0464CFC0
  isVisualInited  get=0x03AA2DD0
  canUpdateVisual  get=0x02F45840
  nextInstanceId  get=0x04D86250  set=0x04D92630
  curSpritePath  get=0x04D85EF0  set=0x06AC5AF4
  buffData  get=0x02F49770
  orderPriority  get=0x02F49650
  buffInstanceUid  get=0x04505A30
METHODS:
  RVA=0x03AA5CD0  token=0x60001FE  System.Void SetSyncManager(Beyond.UI.GPUI.GPUIProxySyncManager syncManager)
  RVA=0x031D7EF0  token=0x60001FF  System.Void Init(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.LuaResourceLoader loader, System.Action onSpriteLoaded)
  RVA=0x03AA6090  token=0x6000200  System.Void Clear()
  RVA=0x0B116910  token=0x6000201  System.Void BeforePlayOutAnim()
  RVA=0x02F45A80  token=0x6000202  System.Void OnTick(System.Single deltaTime)
  RVA=0x02F47F60  token=0x6000203  System.Void SetVisible(System.Boolean visible)
  RVA=0x02E9A080  token=0x6000204  System.Void SetBuffIconOverrideActive(System.Boolean active)
  RVA=0x038487D0  token=0x6000205  System.Void SetStackCount(System.Int32 stackCount, System.Boolean playAnim)
  RVA=0x0B116954  token=0x6000206  System.Void OnBuffIgnite()
  RVA=0x03AA6160  token=0x6000207  System.Void ClearAnimation(System.Boolean executeOnComplete)
  RVA=0x03AA28A0  token=0x6000208  System.Void PlayInAnimation(System.Action callback)
  RVA=0x04D1A430  token=0x6000209  System.Void PlayOutAnimation(System.Action callback)
  RVA=0x0B116A00  token=0x600020A  System.Void PlayStrongInAnimation(System.Action callback)
  RVA=0x0B116998  token=0x600020B  System.Void PlayIgniteOutAnimation(System.Action callback)
  RVA=0x03AA4360  token=0x600020C  System.Void SampleToInAnimationEnd()
  RVA=0x03AA23C0  token=0x600020D  System.Void PlayWithAnimation(System.String animationName, System.Action callback)
  RVA=0x046DD4B0  token=0x600020E  System.Void DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x04234550  token=0x600020F  System.Void VisualInit(UnityEngine.Sprite sprite)
  RVA=0x02E9A7D0  token=0x6000210  System.Boolean SetBuffIcon(UnityEngine.Sprite sprite)
  RVA=0x02E998D0  token=0x6000211  System.Void ClearBuffIconOverride()
  RVA=0x02E9AFB0  token=0x6000212  System.Void _LogSetBuffIconFailure(UnityEngine.Sprite sprite)
  RVA=0x04558630  token=0x6000213  System.Void SetStackElementActive(System.Boolean isActive)
  RVA=0x0363C0D0  token=0x6000214  System.Void SetWarningState(System.Boolean isWarning)
  RVA=0x0357AB70  token=0x6000215  System.Boolean TrySetSprite(System.String nodeName, UnityEngine.Sprite sprite)
  RVA=0x03848960  token=0x6000216  System.Boolean TrySetText(System.String nodeName, System.String text)
  RVA=0x0363C120  token=0x6000217  System.Boolean TrySetColor(System.String nodeName, UnityEngine.Color color)
  RVA=0x0B116A68  token=0x6000218  System.Boolean TrySetNodeRotZ(System.String subRoot, System.Single rotz)
  RVA=0x02F480C0  token=0x6000219  System.Boolean TrySetNodeVisibility(System.String nodeName, System.Boolean visible)
  RVA=0x02F48040  token=0x600021A  System.Void TrySetNodeVisibility(System.Collections.Generic.List<System.String> nodeNames, System.Boolean visible)
  RVA=0x033C12A0  token=0x600021B  System.Boolean TrySetImageFillAmount(System.String nodeName, System.Single fillAmount)
  RVA=0x03AA2490  token=0x600021C  System.Boolean TryStartRootAnimation(System.String animationName, System.Single& duration)
  RVA=0x0B116B48  token=0x600021D  System.Boolean TryStartSubrootAnimation(System.String subrootName, System.String animationName, System.Single& duration)
  RVA=0x02F45D40  token=0x600021E  System.Boolean TryEnsureGPUIInstance()
  RVA=0x042355F0  token=0x600021F  System.Boolean _TryCreateGPUIInstance()
  RVA=0x03AA6110  token=0x6000220  System.Boolean _TryDestroyGpuiInstance()
  RVA=0x02F46490  token=0x6000221  System.Boolean _TryResolveGpui(Beyond.UI.GPUI.GPUIExtendedSystem& targetSystem, Beyond.UI.GPUI.GPUIHandle& handle)
  RVA=0x02F45C80  token=0x6000222  System.Boolean _TryResolveGPUIProxy()
  RVA=0x02F47FC0  token=0x6000223  System.Boolean _TrySetRootScale(UnityEngine.Vector2 scale)
  RVA=0x02F45B80  token=0x6000224  System.Void _UpdateAnimationComplete()
  RVA=0x04558150  token=0x6000225  System.String _ResolveNodeName(System.String configuredNodeName, System.String defaultNodeName)
  RVA=0x0419E690  token=0x6000226  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.GPUIBuffNode
TYPE:  class
TOKEN: 0x2000050
SIZE:  0xC0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.SerializeFieldDictionary<Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle,Beyond.UI.GPUIBuffCell>_buffStyleCellTemplateDict  // 0x68
  private           Beyond.UI.GPUIBuffNode.BuffNodeType_buffNodeType  // 0x70
  private           System.Int32                    _maxBuffCellCount  // 0x74
  private           System.Boolean                  m_isFirstInited  // 0x78
  private           System.Boolean                  m_missingGPUIProxySyncManagerLogged  // 0x79
  private           Beyond.UI.GPUI.GPUIProxySyncManagerm_gpuiProxySyncManager  // 0x80
  private   readonly Beyond.LuaResourceLoader        m_loader  // 0x88
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_owner  // 0x90
  private   readonly Beyond.DynamicFastLookupCollection<Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle,Beyond.UI.GPUIBuffNode.BuffCellGroup>m_buffCellGroupDict  // 0xa0
  private   readonly Beyond.DynamicFastLookupCollection<System.String,System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>>m_stackBuffsDict  // 0xa8
  private   readonly System.Collections.Generic.List<Beyond.UI.GPUIBuffCell>m_orderedBuffCellList  // 0xb0
  private   readonly System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_addBuffIconBuffer  // 0xb8
PROPERTIES:
  tickOption  get=0x045C5FF0
METHODS:
  RVA=0x03A9CF00  token=0x600022A  System.Void _OnFirstInit()
  RVA=0x03AA5040  token=0x600022B  System.Void OnPreWarm(Beyond.UI.GPUI.GPUIProxySyncManager syncManager)
  RVA=0x03A9B7D0  token=0x600022C  System.Void Init(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x02F4A050  token=0x600022D  System.Void Refresh()
  RVA=0x03AA28F0  token=0x600022E  System.Void Clear()
  RVA=0x03308790  token=0x600022F  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x0B116C54  token=0x6000230  System.Void OnRelease()
  RVA=0x03AA2E00  token=0x6000231  System.Void OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x03AA2E90  token=0x6000232  System.Boolean _IsBuffIconInThisNode(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x03AA2F60  token=0x6000233  System.Void _OnAddBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x03AA2FD0  token=0x6000234  System.Void _OnRemoveBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x03AA43F0  token=0x6000235  System.Void _AddBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean playAnim)
  RVA=0x0B116D1C  token=0x6000236  System.Void _AddStackBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean playAnim, Beyond.Gameplay.Core.BuffData buffData, Beyond.UI.GPUIBuffNode.BuffCellGroup buffCellGroup)
  RVA=0x03AA3060  token=0x6000237  System.Void _RemoveBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B117964  token=0x6000238  System.Void _RemoveStackBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.Gameplay.Core.Buff.FinishReason finishReason, Beyond.Gameplay.Core.BuffData buffData, Beyond.UI.GPUIBuffNode.BuffCellGroup buffCellGroup)
  RVA=0x03AA2C30  token=0x6000239  System.Void _RemoveSingleBuffCell(System.UInt64 buffUid, Beyond.UI.GPUIBuffCell buffCell, Beyond.Gameplay.Core.Buff.FinishReason finishReason, Beyond.UI.GPUIBuffNode.BuffCellGroup buffCellGroup, System.Action extraRecycleAction)
  RVA=0x0B1176A8  token=0x600023A  System.Void _OnBuffEnhanceChanged(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B11781C  token=0x600023B  System.Void _OnBuffIgnite(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x03A9BA20  token=0x600023C  System.Void _RegisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x03A9B8C0  token=0x600023D  System.Void _UnregisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x03AA4770  token=0x600023E  Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle _GetIconStyle(Beyond.Gameplay.Core.Buff buff)
  RVA=0x02F49410  token=0x600023F  System.Void _SetBuffCellSiblingInOrder(Beyond.UI.GPUIBuffCell buffCell)
  RVA=0x02F482A0  token=0x6000240  System.Void _DealWithBuffCellVisible()
  RVA=0x03AA3300  token=0x6000241  System.Void _PlayInAnimation(Beyond.UI.GPUIBuffCell buffCell, Beyond.Gameplay.Core.BuffData buffData, System.Boolean playAnim)
  RVA=0x02E9B370  token=0x6000242  System.Boolean _IsBuffCellVisibleByLimit(Beyond.UI.GPUIBuffCell buffCell)
  RVA=0x04664C90  token=0x6000243  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000244  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C37238  token=0x6000245  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
  RVA=0x069AFD04  token=0x6000246  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.GPUILifeTimeBuffCell
TYPE:  class
TOKEN: 0x2000059
SIZE:  0xE8
EXTENDS: Beyond.UI.GPUIBuffCell
FIELDS:
  private           System.String                   _buffLifeTimeBarNodeName  // 0xc0
  private           UnityEngine.Color               _normalLifeTimeBarColor  // 0xc8
  private           UnityEngine.Color               _warningLifeTimeBarColor  // 0xd8
METHODS:
  RVA=0x04234500  token=0x6000257  System.Void VisualInit(UnityEngine.Sprite sprite)
  RVA=0x02F458D0  token=0x6000258  System.Void OnTick(System.Single deltaTime)
  RVA=0x0363C060  token=0x6000259  System.Void SetWarningState(System.Boolean isWarning)
  RVA=0x033C0B80  token=0x600025A  System.Void _UpdateFillAmount()
  RVA=0x0419E610  token=0x600025B  System.Void .ctor()
  RVA=0x0B119604  token=0x600025C  System.Void <>iFixBaseProxy_VisualInit(UnityEngine.Sprite P0)
  RVA=0x0B116698  token=0x600025D  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
  RVA=0x0B1195FC  token=0x600025E  System.Void <>iFixBaseProxy_SetWarningState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.HpBarComboNode
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.GameObject          comboCountLayout  // 0x18
  public            Beyond.UI.UIText                comboCountText  // 0x20
  private           Beyond.UI.UIAnimationWrapper    m_animationWrapper  // 0x28
  private           System.Int32                    m_lastCount  // 0x30
  private   static  System.String                   COMMON_COMBO_GLOBAL_BUFF_ID  // const
METHODS:
  RVA=0x04248B60  token=0x600025F  System.Void Awake()
  RVA=0x0B11960C  token=0x6000260  System.Void OnDestroy()
  RVA=0x0B1198F4  token=0x6000261  System.Void _OnGlobalBuffStart(Beyond.EventData<Beyond.Gameplay.Core.GlobalBuff,System.String>& eventData)
  RVA=0x0B119858  token=0x6000262  System.Void _OnGlobalBuffFinish(Beyond.EventData<Beyond.Gameplay.Core.GlobalBuff,System.String,Beyond.Gameplay.Core.GlobalBuff.FinishReason>& eventData)
  RVA=0x042485D0  token=0x6000263  System.Void _RefreshComboCount(System.Boolean isEarly)
  RVA=0x0B119790  token=0x6000264  System.Void _AnimInCallback()
  RVA=0x0B1197F4  token=0x6000265  System.Void _AnimOutCallback()
  RVA=0x05393520  token=0x6000266  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.MainCharHpBar
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x240
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.UI.UIAnimationWrapper    hpBar  // 0x68
  public            Beyond.UI.UIText                hpText  // 0x70
  public            Beyond.UI.UIImage               hpWhiteFill  // 0x78
  public            UnityEngine.GameObject          blinkAnimObject  // 0x80
  public            System.Single                   hpBlinkPercent  // 0x88
  public            System.Single                   hpChangeColorPercent  // 0x8c
  public            Beyond.UI.UIImage               hpFillHigh  // 0x90
  public            Beyond.UI.UIImage               hpFillLow  // 0x98
  public            UnityEngine.RectTransform       hpVfxNode  // 0xa0
  public            Beyond.UI.UIImage[]             hpVfxImages  // 0xa8
  public            Beyond.SerializeFieldDictionary<Beyond.GEnums.SpellInflictionOnCharType,UnityEngine.Color>hpVfXColors  // 0xb0
  public            System.Collections.Generic.List<Beyond.UI.HpChangeDisplayData>hpChangeConfigList  // 0xb8
  public            System.Collections.Generic.List<Beyond.UI.MainCharHpBar.HpScreenEffectConfig>hpScreenEffectConfigList  // 0xc0
  public            System.Single                   hpScreenEffectCd  // 0xc8
  public            Beyond.UI.UIBuffNode            buffNode  // 0xd0
  public            Beyond.UI.GPUIBuffNode          gpuiBuffNode  // 0xd8
  public            Beyond.UI.UISpellInflictionOnCharNodespellInflictionOnCharNode  // 0xe0
  public            Beyond.UI.UIImage               buffIcon  // 0xe8
  public            UnityEngine.Transform           leftLayout  // 0xf0
  public            UnityEngine.Transform           charPassiveNode  // 0xf8
  public            UnityEngine.GameObject          ultimateProgress  // 0x100
  public            Beyond.UI.UIImage               ultimateProgressFill  // 0x108
  public            System.Single                   ultimateProgressFillSmoothTime  // 0x110
  public            UnityEngine.Color               colorDefault  // 0x114
  public            UnityEngine.Color               colorRed  // 0x124
  public            UnityEngine.Color               colorYellow  // 0x134
  public            UnityEngine.Color               colorBlue  // 0x144
  public            UnityEngine.Color               colorGreen  // 0x154
  private           System.Boolean                  _useGPUIBuff  // 0x164
  private   static  System.String                   HP_BLINK_ANIM  // const
  private           System.Boolean                  m_nodeShowing  // 0x165
  private           Beyond.Gameplay.Core.Entity     m_mainChar  // 0x168
  private           Beyond.UI.HpChangeDisplayData   m_hpChangeConfig  // 0x170
  private           Beyond.UI.UIHeadBar.HpDisplayStatem_hpDisplayState  // 0x178
  private           System.Nullable<System.Single>  m_targetHpValue  // 0x17c
  private   readonly Beyond.Gameplay.PeriodicTimer   m_hpStayTimer  // 0x188
  private           System.Boolean                  m_hpBarShowing  // 0x190
  private           System.Boolean                  m_hpBarShaking  // 0x191
  private           Beyond.UI.UIAnimationWrapper    m_hpVfxAnimWrapper  // 0x198
  private           Beyond.UI.UIHudFadeController   m_hudFadeController  // 0x1a0
  private   static readonly System.Collections.Generic.HashSet<System.String>s_tempCurPassiveUIPrefabNames  // static @ 0x0
  private   static readonly System.Collections.Generic.List<System.String>s_tempUnusedPassiveUIPrefabNames  // static @ 0x8
  private   readonly System.Collections.Generic.Dictionary<System.String,Beyond.UI.UICharPassiveBase>m_allPassiveBases  // 0x1a8
  private           Beyond.UI.UICharPassiveBase     m_curPassiveBase  // 0x1b0
  private           Beyond.UI.UIAnimationWrapper    m_buffIconAnimWrapper  // 0x1b8
  private           Beyond.UI.UIAnimationWrapper    m_shakeAnimWrapper  // 0x1c0
  private           Beyond.UI.UIAnimationWrapper    m_hpWhiteAnimWrapper  // 0x1c8
  private           Beyond.UI.UIAnimationWrapper    m_ultimateBuffProgressAnimWrapper  // 0x1d0
  private           Beyond.UnSafeString.UnSafeStringHandlem_hpTextHandle  // 0x1d8
  private   readonly Beyond.LuaResourceLoader        m_loader  // 0x1e0
  private           System.Single                   m_lastHpScreenEffectTime  // 0x1e8
  private           System.Boolean                  m_ultimateProgressShowing  // 0x1ec
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_ultimateBuffPtr  // 0x1f0
  private           System.Single                   m_ultimateBuffProgressShowingFillAmount  // 0x200
  private           System.Single                   m_ultimateBuffProgressSmoothVelocity  // 0x204
  private   readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>m_ultimateBuffProgressRegisteredCharacters  // 0x208
  private   readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_activeUltimateBuffs  // 0x210
  private   readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_activeCharHpBarVfxBuffs  // 0x218
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_charHpBarVfxBuffPtr  // 0x220
  private           Beyond.UI.GPUI.GPUIProxySyncManagerm_gpuiProxySyncManager  // 0x230
  private           System.Boolean                  m_gpuiBuffNodePreWarmed  // 0x238
  private           System.Boolean                  m_normalBuffNodePreWarmed  // 0x239
  private           System.Boolean                  m_buffModeInitialized  // 0x23a
PROPERTIES:
  tickOption  get=0x04A79900
METHODS:
  RVA=0x044F48F0  token=0x6000268  System.Void OnAwake()
  RVA=0x04D613A0  token=0x6000269  System.Void SetGPUIBuffSync(Beyond.UI.GPUI.GPUIProxySyncManager gpuiProxySyncManager)
  RVA=0x04B382A0  token=0x600026A  System.Void SetGPUIBuffMode(System.Boolean enabled)
  RVA=0x03CCEC90  token=0x600026B  System.Void OnEnable()
  RVA=0x0B11998C  token=0x600026C  System.Void OnRelease()
  RVA=0x04850E80  token=0x600026D  System.Void OnShow()
  RVA=0x049856C0  token=0x600026E  System.Void OnHide()
  RVA=0x03E252E0  token=0x600026F  System.Void _ClearMainChar()
  RVA=0x03CCEE30  token=0x6000270  System.Void LateTick(System.Single deltaTime)
  RVA=0x03CCEF30  token=0x6000271  System.Boolean _CheckHpBarShowing()
  RVA=0x03CCF0C0  token=0x6000272  System.Void _SetHpBarShowing(System.Boolean showing, System.Boolean animate)
  RVA=0x0B11A470  token=0x6000273  System.Void _OnMainCharChange(Beyond.EventData<Beyond.Gameplay.SetLeaderReason,Beyond.Gameplay.Core.Entity,Beyond.Gameplay.Core.Entity>& data)
  RVA=0x03E26F10  token=0x6000274  System.Void _ProcessOnMainCharChange(Beyond.Gameplay.Core.Entity newMainChar)
  RVA=0x03E27E90  token=0x6000275  System.Void _OnTeamChanged()
  RVA=0x03639BF0  token=0x6000276  System.Void _OnHpChange(Beyond.Gameplay.Core.Entity entity, System.Double change, System.Boolean isMaxHpChanged)
  RVA=0x036399D0  token=0x6000277  System.Void _SetHpFillAmount(System.Single hpPct)
  RVA=0x03638570  token=0x6000278  System.Void _CalculateHpChangeDisplay(Beyond.Gameplay.Core.AbilitySystem target, System.Double deltaValue)
  RVA=0x03CCF290  token=0x6000279  System.Void _UpdateHpDisplay(System.Single deltaTime)
  RVA=0x0B11A4D4  token=0x600027A  System.Void _OnTakeDamage(Beyond.Gameplay.Core.Entity entity, System.Boolean hideMainCharHpScreenEffect)
  RVA=0x03CD0500  token=0x600027B  System.Void _OnMainCharHpActiveChange()
  RVA=0x03E26C20  token=0x600027C  System.Void _EnsureBuffNodePreWarmed()
  RVA=0x03E26B70  token=0x600027D  System.Void _InitCurrentBuffNode(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x03E256F0  token=0x600027E  System.Void _ClearBuffNode()
  RVA=0x03E25730  token=0x600027F  System.Void _ClearBuffNode(System.Boolean useGPUIBuff)
  RVA=0x0B11A17C  token=0x6000280  System.Boolean _IsCharHpBarVfxBuffValid(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x04426DB0  token=0x6000281  System.Void _RebuildCharHpBarVfxBuffCache()
  RVA=0x03E26E70  token=0x6000282  Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> _GetLatestValidCharHpBarVfxBuffPtrFromCache()
  RVA=0x03E26A50  token=0x6000283  System.Void _OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x03E258B0  token=0x6000284  System.Void _RefreshBuffChangeCharHpBarVfx()
  RVA=0x04D32C20  token=0x6000285  System.Void _HpVfxOutAnimCallback()
  RVA=0x04A30790  token=0x6000286  System.Void _OnCharPassiveNumChanged(System.Single newNum)
  RVA=0x03E28E90  token=0x6000287  Beyond.UI.UICharPassiveBase _GetOrCreateCharPassiveBase(System.String prefabName)
  RVA=0x03E26730  token=0x6000288  System.Void _OnProgressBarBuffChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B11A34C  token=0x6000289  System.Void _OnBuffEnhanceChanged(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B11A08C  token=0x600028A  System.Void _BlinkBuffIcon(System.String spritePath)
  RVA=0x03E26270  token=0x600028B  System.Void _SetUltimateBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B11AA98  token=0x600028C  System.Void _UltimateBuffProgressOutAnimCallback()
  RVA=0x03A9BBF0  token=0x600028D  System.Void _RebuildUltimateBuffTracking()
  RVA=0x0B11A23C  token=0x600028E  System.Boolean _IsUltimateBuffValid(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x03E26080  token=0x600028F  Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> _GetUltimateBuffForAbility(Beyond.Gameplay.Core.AbilitySystem ability)
  RVA=0x03E25E00  token=0x6000290  System.Void _SelectUltimateBuff()
  RVA=0x0B11A8CC  token=0x6000291  System.Void _RefreshUltimateBuffProgress(System.Single deltaTime)
  RVA=0x0B11AA54  token=0x6000292  System.Void _SetUltimateProgressFill(System.Single realFillAmount, System.Single totalDuration)
  RVA=0x03ECBF00  token=0x6000293  System.Void .ctor()
  RVA=0x04CA3FC0  token=0x6000294  System.Void .cctor()
  RVA=0x0B11A05C  token=0x6000295  System.Void <_SetHpBarShowing>b__76_0()
  RVA=0x0B119E94  token=0x6000296  System.Void <_BlinkBuffIcon>b__100_0(UnityEngine.Sprite sprite)
  RVA=0x0B119F94  token=0x6000297  System.Void <_BlinkBuffIcon>b__100_1()
  RVA=0x0B11A028  token=0x6000298  System.Void <_BlinkBuffIcon>b__100_2()
  RVA=0x069AFD0C  token=0x6000299  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600029A  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x600029B  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x600029C  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06BD8710  token=0x600029D  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.OutOfScreenTargetArrow
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rect  // 0x18
  public            Beyond.UI.UIAnimationWrapper    anim  // 0x20
  public            Beyond.UI.UIAnimationWrapper    inSkillAnim  // 0x28
  public            DG.Tweening.TweenCallback       cachedRemoveCallback  // 0x30
  public            DG.Tweening.TweenCallback       cachedHideCallback  // 0x38
  public            DG.Tweening.TweenCallback       cachedInSkillCallback  // 0x40
METHODS:
  RVA=0x05393520  token=0x600029E  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.OutOfScreenTargetsCtrl
TYPE:  class
TOKEN: 0x200005E
SIZE:  0xA8
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            System.Single                   ellipseXRadius  // 0x70
  public            System.Single                   ellipseYRadius  // 0x74
  public            UnityEngine.RectTransform       main  // 0x78
  public            Beyond.UI.OutOfScreenTargetArrowarrowModel  // 0x80
  private   readonly System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.OutOfScreenTargetArrow>m_arrows  // 0x88
  private   readonly System.Collections.Generic.List<Beyond.UI.OutOfScreenTargetArrow>m_arrowsCache  // 0x90
  private   readonly System.Collections.Generic.List<Beyond.UI.OutOfScreenTargetArrow>m_arrowsWithOutAnimation  // 0x98
  private   static readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>s_toRemoveArrowKeys  // static @ 0x0
  private   static readonly System.Collections.Generic.List<Beyond.Gameplay.Core.AbilitySystem>s_currentEnemyKeys  // static @ 0x8
  private           System.Boolean                  m_disabled  // 0xa0
  private   static readonly Beyond.Gameplay.Core.GameplayTagHIDE_ARROW_TAG  // static @ 0x10
PROPERTIES:
  tickOption  get=0x04A797B0
METHODS:
  RVA=0x0B11B458  token=0x60002A0  System.Void OnCreate()
  RVA=0x0B11B354  token=0x60002A1  System.Void OnClose()
  RVA=0x0B11B58C  token=0x60002A2  System.Void OnShow()
  RVA=0x0B11B0CC  token=0x60002A3  System.Void DebugChangeImpl(System.Boolean disableThis)
  RVA=0x03F2A240  token=0x60002A4  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B11B6D4  token=0x60002A5  Beyond.UI.OutOfScreenTargetArrow _CreateOneArrow()
  RVA=0x0B11B8EC  token=0x60002A6  System.Void _InitArrowCallbacks(Beyond.UI.OutOfScreenTargetArrow arrow)
  RVA=0x0B11BC08  token=0x60002A7  System.ValueTuple<UnityEngine.Vector2,System.Single> _ScreenPosToUIPos(UnityEngine.Vector3 screenPos)
  RVA=0x0B11BA94  token=0x60002A8  System.Void _OnEnemyCastSkillToMainChar(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem>& eventData)
  RVA=0x049E4BC0  token=0x60002A9  System.Void .ctor()
  RVA=0x03ECB9E0  token=0x60002AA  System.Void .cctor()
  RVA=0x0B10A3CC  token=0x60002AB  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x60002AC  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.OutOfScreenTargetsCtrlV2
TYPE:  class
TOKEN: 0x2000060
SIZE:  0xA0
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  private   static  System.String                   ROOT_ANIMATION_IN  // const
  private   static  System.String                   ROOT_ANIMATION_OUT  // const
  private   static  System.String                   ROOT_ANIMATION_LOOP  // const
  private   static  System.String                   SKILL_SUBROOT  // const
  private   static  System.String                   SKILL_ANIMATION_IN  // const
  public            System.Single                   ellipseXRadius  // 0x70
  public            System.Single                   ellipseYRadius  // 0x74
  public            Beyond.UI.GPUI.GPUIExtendedSystemgpuiSystem  // 0x78
  public            System.String                   gpuiConfigGroupName  // 0x80
  public            System.String                   gpuiPrefabName  // 0x88
  private   readonly System.Collections.Generic.Dictionary<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>,Beyond.UI.OutOfScreenTargetsCtrlV2.ArrowRuntimeState>m_arrowStates  // 0x90
  private   static readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>>s_toRemoveArrowKeys  // static @ 0x0
  private   static readonly System.Collections.Generic.List<Beyond.Gameplay.Core.AbilitySystem>s_currentEnemyKeys  // static @ 0x8
  private           System.Boolean                  m_isShow  // 0x98
  private           System.Boolean                  m_disabled  // 0x99
  private   static readonly Beyond.Gameplay.Core.GameplayTagHIDE_ARROW_TAG  // static @ 0x10
PROPERTIES:
  tickOption  get=0x04A797E0
METHODS:
  RVA=0x04249D20  token=0x60002B2  System.Void OnCreate()
  RVA=0x0B11AB60  token=0x60002B3  System.Void OnClose()
  RVA=0x04B513F0  token=0x60002B4  System.Void OnShow()
  RVA=0x04CA2E00  token=0x60002B5  System.Void OnHide()
  RVA=0x0B11AAF8  token=0x60002B6  System.Void DebugChangeImpl(System.Boolean disableThis)
  RVA=0x038CC850  token=0x60002B7  System.Void LateTick(System.Single deltaTime)
  RVA=0x03AB6A40  token=0x60002B8  System.Boolean _CreateArrowState(Beyond.UI.OutOfScreenTargetsCtrlV2.ArrowRuntimeState& state)
  RVA=0x03AB6930  token=0x60002B9  System.Void _RestartVisibleState(Beyond.UI.OutOfScreenTargetsCtrlV2.ArrowRuntimeState state)
  RVA=0x03AB6C30  token=0x60002BA  System.Void _ScheduleDestroy(Beyond.UI.OutOfScreenTargetsCtrlV2.ArrowRuntimeState state)
  RVA=0x038CE6C0  token=0x60002BB  System.Void _UpdateRuntimeStates()
  RVA=0x0B11AEA0  token=0x60002BC  System.Void _RecycleAll()
  RVA=0x042EA300  token=0x60002BD  System.ValueTuple<UnityEngine.Vector2,System.Single> _ScreenPosToUIPos(UnityEngine.Vector3 screenPos)
  RVA=0x0B11AC6C  token=0x60002BE  System.Void _OnEnemyCastSkillToMainChar(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem>& eventData)
  RVA=0x04B0B740  token=0x60002BF  System.Void .ctor()
  RVA=0x03ECBB20  token=0x60002C0  System.Void .cctor()
  RVA=0x0B10A3CC  token=0x60002C1  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x60002C2  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.SkillButton
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x368
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public    static  System.String                   UI_SPRITE_SKILL_ICON  // const
  private   static  System.Single                   HIGHLIGHT_CHECK_INTERVAL  // const
  public            UnityEngine.CanvasGroup         viewNodeCanvasGroup  // 0x70
  public            Beyond.UI.UIAnimationWrapper    deadNode  // 0x78
  public            UnityEngine.GameObject          emptyNode  // 0x80
  public            System.Single                   deadAlpha  // 0x88
  public            UnityEngine.Color               colorDefault  // 0x8c
  public            UnityEngine.Color               colorRed  // 0x9c
  public            UnityEngine.Color               colorYellow  // 0xac
  public            UnityEngine.Color               colorBlue  // 0xbc
  public            UnityEngine.Color               colorGreen  // 0xcc
  public            System.Single                   cantCastSkillAlpha  // 0xdc
  public            Beyond.UI.UIButton              normalButton  // 0xe0
  public            Beyond.UI.UIImage               normalIcon  // 0xe8
  public            Beyond.UI.UIImage               normalIconShadow  // 0xf0
  public            Beyond.UI.UIImage               normalColorBg  // 0xf8
  public            Beyond.UI.UIImage               cdMask  // 0x100
  public            Beyond.UI.UIImage               cdBg  // 0x108
  public            Beyond.UI.UIText                cdText  // 0x110
  public            Beyond.UI.UIAnimationWrapper    cdRefresh  // 0x118
  public            UnityEngine.CanvasGroup         normalCanvasGroup  // 0x120
  public            Beyond.UI.UIAnimationWrapper    strengLight  // 0x128
  public            UnityEngine.GameObject          normalMoveEffectsBg  // 0x130
  public            Beyond.UI.UIAnimationWrapper    hint  // 0x138
  public            Beyond.UI.UIAnimationWrapper    normalButtonAnim  // 0x140
  public            UnityEngine.Transform           normalKeyHint  // 0x148
  public            Beyond.UI.UIImage               notRecommendMask  // 0x150
  public            Beyond.UI.UIImage               notRecommendIcon  // 0x158
  public            Beyond.UI.UIImage               normalPressFill  // 0x160
  public            Beyond.UI.UIImage               normalCountdownFill  // 0x168
  public            UnityEngine.Color               normalCountdownFillWeakColor  // 0x170
  public            UnityEngine.RectTransform       normalCountdownFillTrail  // 0x180
  public            Beyond.UI.UIAnimationWrapper    contentAnimWrapper  // 0x188
  public            Beyond.UI.UIButton              ultimateButton  // 0x190
  public            Beyond.UI.UIImage               ultimateIcon  // 0x198
  public            Beyond.UI.UIImage               ultimateIconShadow  // 0x1a0
  public            Beyond.UI.UIImage               ultimateColorBg  // 0x1a8
  public            Beyond.UI.UIImage               uspRing  // 0x1b0
  public            Beyond.UI.UIImage               uspRingSmall  // 0x1b8
  public            Beyond.UI.UIImage               centerImage  // 0x1c0
  public            Beyond.UI.UIImage               glow01  // 0x1c8
  public            Beyond.UI.UIImage               outRing  // 0x1d0
  public            UnityEngine.CanvasGroup         ultimateCanvasGroup  // 0x1d8
  public            Beyond.UI.UIImage               pressFill  // 0x1e0
  public            Beyond.UI.UIAnimationWrapper    ultimateButtonAnim  // 0x1e8
  public            Beyond.UI.UIImage               ultKeyIcon  // 0x1f0
  public            UnityEngine.Transform           ultKeyHint  // 0x1f8
  public            System.Single                   uspFillSpeed  // 0x200
  public            Beyond.UI.UIImage               ultimateFillCompleteImage  // 0x208
  public            UnityEngine.GameObject          ultimateBar  // 0x210
  public            Beyond.UI.UIImage               ultimateBarFill  // 0x218
  public            System.Single                   ultimateBarFillMin  // 0x220
  public            System.Single                   ultimateBarFillMax  // 0x224
  public            Beyond.UI.UIImage               ultimateCdFill  // 0x228
  public            Beyond.UI.UIImage               ultimateBarFillFrame  // 0x230
  public            UnityEngine.GameObject          ultimateCountdownNode  // 0x238
  public            Beyond.UI.UIImage               ultimateCountdownFill  // 0x240
  public            Beyond.UI.UIImage               ultimateCountdownFillBg  // 0x248
  public            Beyond.UI.UIImage               ultimateCountdownBarFill  // 0x250
  public            Beyond.UI.UIImage               touchPressUspRing  // 0x258
  public            UnityEngine.RectTransform       touchPressIndicator  // 0x260
  private           System.Int32                    m_charIndex  // 0x268
  private           Beyond.DeviceInfo.InputType     m_panelType  // 0x26c
  private           Beyond.Gameplay.CharInfo        m_charInfo  // 0x270
  private           Beyond.Gameplay.Core.SkillData  m_ultimateSkillData  // 0x278
  private           Beyond.Gameplay.Core.SkillData  m_normalSkillData  // 0x280
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_charAbilityPtr  // 0x288
  private           Beyond.Gameplay.Core.Skill      m_normalSkill  // 0x298
  private           Beyond.Gameplay.Core.Skill      m_ultimateSkill  // 0x2a0
  private           System.Boolean                  m_isEmpty  // 0x2a8
  private           System.Single                   m_ultimatePressingTime  // 0x2ac
  private           System.Boolean                  m_isDeadState  // 0x2b0
  private           System.Boolean                  m_normalSkillReady  // 0x2b1
  private           System.Single                   m_lastHighlightCheckTime  // 0x2b4
  private           System.Single                   m_uspTargetFill  // 0x2b8
  private           System.Boolean                  m_ultimateReady  // 0x2bc
  private           UnityEngine.Color               m_normalIconShadowDefaultColor  // 0x2c0
  private           System.Boolean                  m_showNotRecommendState  // 0x2d0
  private           Beyond.UI.SkillButton.PressHandlerm_pressHandler  // 0x2d8
  private           System.Boolean                  m_hasNotRecommendIcon  // 0x2e0
  private           Beyond.UI.UIAnimationWrapper    m_ultimateBarAnim  // 0x2e8
  private           Beyond.UI.UIAnimationWrapper    m_ultimateCountdownAnim  // 0x2f0
  private           Beyond.HudFadeType              m_hudFadeType  // 0x2f8
  private           System.String                   m_actionId  // 0x300
  private           System.String                   m_curNormalIconName  // 0x308
  private           System.String                   m_curUltimateIconName  // 0x310
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_normalProgressBuffPtr  // 0x318
  private           System.Single                   m_normalProgressOutDuration  // 0x328
  private           DG.Tweening.TweenCallback       m_normalProgressOutCallback  // 0x330
  private           DG.Tweening.TweenCallback       m_ultCountDownOutCallback  // 0x338
  private           UnityEngine.Color               m_normalCountdownFillDefaultColor  // 0x340
  private           System.Boolean                  m_isNormalCountdownFillWeakStyle  // 0x350
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_ultimateProgressBuffPtr  // 0x358
  private   static  System.Action<System.Int32>     s_onPressSkillButtonStart  // static @ 0x0
PROPERTIES:
  tickOption  get=0x04422970
  skillSetting  get=0x0B11E304
METHODS:
  RVA=0x03D2ADC0  token=0x60002C6  System.Void OnAwake()
  RVA=0x045F6FD0  token=0x60002C7  System.Void OnEnable()
  RVA=0x0B11CF9C  token=0x60002C8  System.Void Close()
  RVA=0x03C43890  token=0x60002C9  System.Void FirstTimeInit(System.Int32 charIndex, System.Boolean isDefaultPanel, System.Boolean isControllerPanel)
  RVA=0x0384B8B0  token=0x60002CA  System.Void RefreshSkillButton()
  RVA=0x0B11E298  token=0x60002CB  System.Boolean _TryGetAliveAbilitySystem(Beyond.Gameplay.Core.AbilitySystem& abilitySys)
  RVA=0x0B11DA40  token=0x60002CC  System.Boolean _MustUpdate()
  RVA=0x03C81880  token=0x60002CD  System.Void PreTick(System.Single deltaTime)
  RVA=0x03C5DBC0  token=0x60002CE  System.Void _InitSkillData()
  RVA=0x030452D0  token=0x60002CF  System.Void _UpdateMainUI(System.Single deltaTime)
  RVA=0x02E9C0A0  token=0x60002D0  System.Void _RefreshNormalSkillCd()
  RVA=0x02E9D050  token=0x60002D1  System.Void _RefreshUltimateSkillCd(System.Boolean costAvailable)
  RVA=0x0384AD10  token=0x60002D2  System.Void _CheckNormalSkillHighlight()
  RVA=0x0B11E090  token=0x60002D3  System.Void _SetNotRecommendState(System.Boolean active)
  RVA=0x0B11D7D0  token=0x60002D4  System.Void OnCharacterDie()
  RVA=0x0384B760  token=0x60002D5  System.Void _RefreshDeadState(System.Boolean anim)
  RVA=0x0B11E18C  token=0x60002D6  System.Void _SetUspFillAmount(System.Single fillAmount)
  RVA=0x0384AB50  token=0x60002D7  System.Void _SetPressFillActive(System.Boolean active)
  RVA=0x03671520  token=0x60002D8  System.Void _SetPressFillAmount(System.Single fillAmount)
  RVA=0x03E26670  token=0x60002D9  System.Void _OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B11DE44  token=0x60002DA  System.Void _SetNormalCountdownFillVisible(System.Boolean visible)
  RVA=0x0B11DDDC  token=0x60002DB  System.Void _ResetNormalCountdownFillStyle()
  RVA=0x0B11E11C  token=0x60002DC  System.Void _SetUltimateCountdownFillVisible(System.Boolean visible, System.Boolean playOutAnim)
  RVA=0x03046ED0  token=0x60002DD  System.Void _RefreshNormalProgressBuff()
  RVA=0x030469C0  token=0x60002DE  System.Void _RefreshUltimateProgressBuff()
  RVA=0x03D40940  token=0x60002DF  System.Void OnUspChange(Beyond.Gameplay.Core.Entity entity)
  RVA=0x0B11D9E4  token=0x60002E0  System.Boolean _CanCastSkill(Beyond.Gameplay.Core.Skill skill)
  RVA=0x03671C00  token=0x60002E1  System.Void CastSkill(Beyond.Gameplay.Core.Skill skill)
  RVA=0x04701980  token=0x60002E2  System.Void OnPressSkillStart(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x048E30A0  token=0x60002E3  System.Void _OnOtherPressSkillButtonStart(System.Int32 charIndex)
  RVA=0x047CED80  token=0x60002E4  System.Void OnPressSkillEnd(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x036D0AF0  token=0x60002E5  System.Void OnPressCancelled()
  RVA=0x0B11DA00  token=0x60002E6  System.Void _CastUltimateSkill()
  RVA=0x0B11D82C  token=0x60002E7  System.Void OnPressUltimateSkillStart(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B11D930  token=0x60002E8  System.Void ToggleControllerSkillIndicator(System.Boolean active)
  RVA=0x0B11DB88  token=0x60002E9  System.Void _OnCharPotentialUnlock(Beyond.EventData<System.UInt64,System.UInt32>& eventData)
  RVA=0x04B39B70  token=0x60002EA  System.Void _OnCharSkillChange(Beyond.EventData<System.UInt64,System.String>& eventData)
  RVA=0x0B11DC04  token=0x60002EB  System.Void _OnCharUltimateSkillChange(Beyond.EventData<System.UInt64,System.String>& eventData)
  RVA=0x0B11DD60  token=0x60002EC  System.Void _OnSkillUpgradeSuccess(Beyond.EventData<System.UInt64,System.String,System.Int32>& eventData)
  RVA=0x0B11DB04  token=0x60002ED  System.Void _OnActiveSkillDataRefreshed(Beyond.EventData<System.UInt64,System.String>& eventData)
  RVA=0x02E9BC80  token=0x60002EE  System.Void _OnSkillCdChanged(Beyond.EventData<Beyond.Gameplay.Core.Skill>& eventData)
  RVA=0x0384AC40  token=0x60002EF  System.Void _OnSkillButtonActiveConfigChanged()
  RVA=0x0B11DC98  token=0x60002F0  System.Void _OnInputActionKeyUpdated(Beyond.EventData<System.String>& eventData)
  RVA=0x0B11DD10  token=0x60002F1  System.Void _OnKeyboardTypeChanged()
  RVA=0x03C43060  token=0x60002F2  System.Void _UpdateLongPressKeyHint()
  RVA=0x03849B30  token=0x60002F3  System.Void _SetGoActive(System.Boolean active)
  RVA=0x03069EE0  token=0x60002F4  System.Void SetEmpty(System.Boolean isEmpty)
  RVA=0x036D0A50  token=0x60002F5  System.Void ClearBtnClick()
  RVA=0x047C3910  token=0x60002F6  System.Void .ctor()
  RVA=0x02E9BB60  token=0x60002F7  System.Void <OnAwake>b__98_0()
  RVA=0x02E9BBC0  token=0x60002F8  System.Void <OnAwake>b__98_1()
  RVA=0x0B10A3CC  token=0x60002F9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60002FA  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x60002FB  System.Void <>iFixBaseProxy_OnEnable()
END_CLASS

CLASS: Beyond.UI.SquadIcon
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x300
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  private   static  System.String                   UI_CHAR_HEAD_PREFIX  // const
  private   static  System.String                   UI_SPRITE_DIR_CHAR_HEAD  // const
  private   static  System.String                   UI_SPRITE_DIR_ITEM  // const
  private   static  System.Single                   HP_CHANGE_TOLERANCE  // const
  public    static  System.String                   CHAR_PASSIVE_UI_PATH  // const
  public            Beyond.UI.UIButton              button  // 0x70
  public            UnityEngine.CanvasGroup         canvasGroup  // 0x78
  public            Beyond.UI.UIAnimationWrapper    charIconNode  // 0x80
  public            UnityEngine.CanvasGroup         keyHint  // 0x88
  public            UnityEngine.GameObject          viewNode  // 0x90
  public            UnityEngine.GameObject          emptyNode  // 0x98
  public            Beyond.UI.UIAnimationWrapper    viewAnimationWrapper  // 0xa0
  public            Beyond.UI.UIAnimationWrapper    layoutAnimationWrapper  // 0xa8
  public            System.Single                   cantSwitchAlpha  // 0xb0
  public            Beyond.UI.UIImage               characterIcon  // 0xb8
  public            Beyond.UI.UIImage               characterIconDead  // 0xc0
  public            Beyond.UI.UIImage               charCdMask  // 0xc8
  public            Beyond.UI.UIAnimationWrapper    charMainEffectNode  // 0xd0
  public            Beyond.UI.UIAnimationWrapper    charDeadNode  // 0xd8
  public            UnityEngine.GameObject          hpNode  // 0xe0
  public            Beyond.UI.UIAnimationWrapper    hpFlashAnim  // 0xe8
  public            UnityEngine.RectTransform       hpFillRect  // 0xf0
  public            System.Single                   hpFlashPercent  // 0xf8
  public            System.Single                   hpRedPercent  // 0xfc
  public            System.Single                   hpShakePercent  // 0x100
  public            Beyond.UI.UIImage               hpFill  // 0x108
  public            Beyond.UI.UIImage               hpRedFill  // 0x110
  public            Beyond.UI.UIAnimationWrapper    hpRecoverAnim  // 0x118
  public            UnityEngine.RectTransform       hpVfxNode  // 0x120
  public            Beyond.UI.UIImage[]             hpVfxImages  // 0x128
  public            Beyond.SerializeFieldDictionary<Beyond.GEnums.SpellInflictionOnCharType,UnityEngine.Color>hpVfXColors  // 0x130
  public            UnityEngine.GameObject          levelNode  // 0x138
  public            Beyond.UI.UIAnimationWrapper    levelUpAnim  // 0x140
  public            Beyond.UI.UIText                animLevelTxt  // 0x148
  public            Beyond.UI.UIText                levelTxt  // 0x150
  public            UnityEngine.RectTransform       comboCdNode  // 0x158
  public            Beyond.UI.UIAnimationWrapper    comboCdBarAnim  // 0x160
  public            Beyond.UI.UIImage               comboCdFill  // 0x168
  public            Beyond.UI.UIAnimationWrapper    comboReadyAnim  // 0x170
  public            Beyond.UI.UIBuffNode            buffNode  // 0x178
  public            Beyond.UI.GPUIBuffNode          gpuiBuffNode  // 0x180
  public            UnityEngine.CanvasGroup         tacticalCanvasGroup  // 0x188
  public            UnityEngine.GameObject          tacticalCdNode  // 0x190
  public            UnityEngine.GameObject          tacticalCastBar  // 0x198
  public            UnityEngine.GameObject          tacticalDotNode  // 0x1a0
  public            UnityEngine.GameObject          tacticalProhibitNode  // 0x1a8
  public            UnityEngine.GameObject          tacticalEmptyNode  // 0x1b0
  public            Beyond.UI.UIImage               tacticalIcon  // 0x1b8
  public            Beyond.UI.UIImage               tacticalCastFill  // 0x1c0
  public            System.Single                   tacticalDeadAlpha  // 0x1c8
  public            System.Single                   tacticalItemUsedUpAlpha  // 0x1cc
  public            UnityEngine.GameObject          fixedIcon  // 0x1d0
  public            UnityEngine.GameObject          trailIcon  // 0x1d8
  public            UnityEngine.GameObject          indieTrailIcon  // 0x1e0
  public            UnityEngine.CanvasGroup         igniteBuffTextNode  // 0x1e8
  public            UnityEngine.Animation           igniteBuffTextAnim  // 0x1f0
  public            Beyond.UI.UIText                igniteBuffText  // 0x1f8
  public            UnityEngine.Color               igniteBuffColorPulse  // 0x200
  public            UnityEngine.Color               igniteBuffColorCryst  // 0x210
  public            UnityEngine.Color               igniteBuffColorNatural  // 0x220
  public            UnityEngine.Color               igniteBuffColorFire  // 0x230
  public            UnityEngine.Transform           charPassiveNode  // 0x240
  private           System.Boolean                  <isEmpty>k__BackingField  // 0x248
  private           Beyond.DeviceInfo.InputType     m_panelType  // 0x24c
  private           System.Boolean                  m_isMainCharacter  // 0x250
  private           System.Boolean                  m_teamSwitchUnlocked  // 0x251
  private           System.Int32                    m_charIndex  // 0x254
  private           System.String                   m_charIndexStr  // 0x258
  private           Beyond.Gameplay.CharInfo        m_charInfo  // 0x260
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_charAbilityPtr  // 0x268
  private           System.Collections.Generic.List<Beyond.UI.TacticalDotCell>m_tacticalDots  // 0x278
  private           System.Single                   m_tacticalCastFillMax  // 0x280
  private           System.Int32                    m_lastLevel  // 0x284
  private           System.Boolean                  m_indicatorShowing  // 0x288
  private           System.Boolean                  m_lastComboCdReady  // 0x289
  private           System.Boolean                  m_comboReadyAnimShowing  // 0x28a
  private           System.Boolean                  m_comboIconShowing  // 0x28b
  private           Beyond.UI.UIHudFadeController   m_hudFadeController  // 0x290
  private           Beyond.UI.UIAnimationWrapper    m_hpVfxAnimWrapper  // 0x298
  private           System.String                   m_lastCharPassiveName  // 0x2a0
  private           Beyond.UI.UICharPassiveBase     m_charPassiveBase  // 0x2a8
  private           DG.Tweening.Sequence            m_keyHintFadeSequence  // 0x2b0
  private           System.Boolean                  m_inDeadState  // 0x2b8
  private           Beyond.UI.UIAnimationWrapper    m_tacticalSwitchAnim  // 0x2c0
  private           System.Nullable<System.Boolean> m_tacticalInProgress  // 0x2c8
  private           System.Int32                    m_tacticalItemLastDisplayCount  // 0x2cc
  private           System.Single                   m_comboCdNodeHeight  // 0x2d0
  private   readonly System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_activeCharHpBarVfxBuffs  // 0x2d8
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_charHpBarVfxBuffPtr  // 0x2e0
  private           Beyond.UI.GPUI.GPUIProxySyncManagerm_gpuiProxySyncManager  // 0x2f0
  private           System.Boolean                  m_gpuiBuffNodePreWarmed  // 0x2f8
  private           System.Boolean                  m_normalBuffNodePreWarmed  // 0x2f9
  private           System.Boolean                  m_buffModeInitialized  // 0x2fa
  private           System.Boolean                  m_useGPUIBuff  // 0x2fb
PROPERTIES:
  tickOption  get=0x04422940
  isEmpty  get=0x04D90930  set=0x04D90960
METHODS:
  RVA=0x0396C800  token=0x6000324  System.Void OnAwake()
  RVA=0x04CADDE0  token=0x6000325  System.Void SetGPUIBuffSync(Beyond.UI.GPUI.GPUIProxySyncManager gpuiProxySyncManager)
  RVA=0x0306A950  token=0x6000326  System.Void SetGPUIBuffMode(System.Boolean enableGPUIBuff)
  RVA=0x03C43230  token=0x6000327  System.Void FirstTimeInit(System.Int32 index, System.Boolean isDefaultPanel, System.Boolean isControllerPanel)
  RVA=0x0B11EE10  token=0x6000328  System.Void Close()
  RVA=0x03849CA0  token=0x6000329  System.Void InitSquadIcon(System.Boolean showFixed, System.Boolean showTrail)
  RVA=0x02F550E0  token=0x600032A  System.Boolean _MustUpdate()
  RVA=0x02F53890  token=0x600032B  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B11FCCC  token=0x600032C  System.Boolean _TryGetAliveAbilitySystem(Beyond.Gameplay.Core.AbilitySystem& abilitySys)
  RVA=0x03C5F7C0  token=0x600032D  System.Void _UpdateComboSkillIcon(System.Boolean showAnim)
  RVA=0x03069F70  token=0x600032E  System.Void SetEmpty(System.Boolean empty)
  RVA=0x0B11F748  token=0x600032F  System.Void ToggleIndicator(System.Boolean isShow)
  RVA=0x0B11F364  token=0x6000330  System.Void InformKeyHint()
  RVA=0x03847E70  token=0x6000331  System.Void _OnHpChange(Beyond.Gameplay.Core.Entity entity, System.Double change, System.Boolean isMaxHpChanged)
  RVA=0x03848600  token=0x6000332  System.Void _SetLevelText()
  RVA=0x0B11F984  token=0x6000333  System.Void _OnIgniteBuffText(Beyond.EventData<System.UInt64,System.String,Beyond.GEnums.EnergyShardType>& eventData)
  RVA=0x0B11F514  token=0x6000334  System.Void OnLevelChange()
  RVA=0x04665560  token=0x6000335  System.Void _TryShowLevelUpAnim()
  RVA=0x03544320  token=0x6000336  System.Void SetDeadState(System.Boolean dead)
  RVA=0x03848450  token=0x6000337  System.Void _EnsureBuffNodePreWarmed()
  RVA=0x03848D10  token=0x6000338  System.Void _InitCurrentBuffNode(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x0B11F7D8  token=0x6000339  System.Void _ClearBuffNode()
  RVA=0x03047570  token=0x600033A  System.Void _UpdateIconAlpha()
  RVA=0x0B11EC64  token=0x600033B  System.Boolean CanSwitchToCenter(System.Boolean showTip)
  RVA=0x0B11F570  token=0x600033C  System.Void OnPressCharIconStart(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x03849210  token=0x600033D  System.Void OnTacticalItemChange()
  RVA=0x0339FF90  token=0x600033E  System.Void _UpdateTacticalItemStatus()
  RVA=0x0B11F6EC  token=0x600033F  System.Void OnTeamSwitchUnlocked()
  RVA=0x0B11FB34  token=0x6000340  System.Void _OnMainCharChange(Beyond.EventData<Beyond.Gameplay.SetLeaderReason,Beyond.Gameplay.Core.Entity,Beyond.Gameplay.Core.Entity>& data)
  RVA=0x03E265C0  token=0x6000341  System.Void _OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x03848210  token=0x6000342  System.Void _RefreshBuffChangeCharHpBarVfx()
  RVA=0x0B11F854  token=0x6000343  System.Void _HpVfxOutAnimCallback()
  RVA=0x04735700  token=0x6000344  System.Void _OnCharPassiveNumChanged(System.Single newNum)
  RVA=0x0B11F8C4  token=0x6000345  System.Boolean _IsCharHpBarVfxBuffValid(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x02F497C0  token=0x6000346  System.Void _RebuildCharHpBarVfxBuffCache()
  RVA=0x038496B0  token=0x6000347  Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> _GetLatestValidCharHpBarVfxBuffPtrFromCache()
  RVA=0x03ECC150  token=0x6000348  System.Void .ctor()
  RVA=0x035425F0  token=0x6000349  System.Void <_UpdateComboSkillIcon>b__108_0()
  RVA=0x049D6EF0  token=0x600034A  System.Void <_UpdateComboSkillIcon>b__108_1()
  RVA=0x04A3A260  token=0x600034B  System.Void <_OnHpChange>b__112_0()
  RVA=0x0B11F7A4  token=0x600034C  System.Void <_TryShowLevelUpAnim>b__116_0()
  RVA=0x0B10A3CC  token=0x600034D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600034E  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06BD8710  token=0x600034F  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.TacticalDotCell
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIImage               dotBg  // 0x18
  public            Beyond.UI.UIImage               dot  // 0x20
  public            Beyond.UI.UIImage               dotWarning  // 0x28
  private           Beyond.UI.UIAnimationWrapper    _anim  // 0x30
  private           System.Boolean                  _showDotWarning  // 0x38
  private           System.Boolean                  m_markInit  // 0x39
  private           System.Boolean                  m_isDotActive  // 0x3a
METHODS:
  RVA=0x038494F0  token=0x6000350  System.Void Init()
  RVA=0x033A0540  token=0x6000351  System.Void SetDotActive(System.Boolean isActive, System.Boolean playAnim)
  RVA=0x0339FEF0  token=0x6000352  System.Void SetDotUsedUp(System.Boolean isUsedUp)
  RVA=0x03695570  token=0x6000353  System.Void .ctor()
  RVA=0x0B11FD38  token=0x6000354  System.Void <SetDotActive>b__8_0()
END_CLASS

CLASS: Beyond.UI.UIAbnormalBuffCell
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x88
EXTENDS: Beyond.UI.UIBuffCell
FIELDS:
  private           Beyond.UI.UIImage               _buffLifeTimeBar  // 0x78
  private           Beyond.UI.UIState.UIStateController_abnormalLevelState  // 0x80
METHODS:
  RVA=0x0B120528  token=0x6000355  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B1206A8  token=0x6000356  System.Void VisualInit(UnityEngine.Sprite sprite)
  RVA=0x0B1205F4  token=0x6000357  System.Void SetWarningState(System.Boolean isWarning)
  RVA=0x0432B600  token=0x6000358  System.Void .ctor()
  RVA=0x0B120690  token=0x6000359  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
  RVA=0x0B1206A0  token=0x600035A  System.Void <>iFixBaseProxy_VisualInit(UnityEngine.Sprite P0)
  RVA=0x0B120698  token=0x600035B  System.Void <>iFixBaseProxy_SetWarningState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.UIAttachedBuffCell
TYPE:  class
TOKEN: 0x200006B
SIZE:  0xB8
EXTENDS: Beyond.UI.UIBuffCell
FIELDS:
  private           Beyond.UI.UIImage               _buffIconPhantom  // 0x78
  private           Beyond.UI.UIImage               _buffStackRing  // 0x80
  private           System.Collections.Generic.List<Beyond.UI.UIAnimationWrapper>_buffStackRingConsumeAnims  // 0x88
  private           System.Single                   _stackRingFillChangeSpeed  // 0x90
  private           System.Single                   _igniteFillDelay  // 0x94
  private   static  System.String                   BUFF_ANIM_IGNITE  // const
  private           System.Int32                    m_maxStackCount  // 0x98
  private           System.Nullable<System.Single>  m_targetStackRingFillAmount  // 0x9c
  private           Beyond.Gameplay.PeriodicTimer   m_igniteFillDelayTimer  // 0xa8
  private           System.Boolean                  m_isInConsumeAnim  // 0xb0
METHODS:
  RVA=0x0450F160  token=0x600035C  System.Void DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x044B3940  token=0x600035D  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B120908  token=0x600035E  System.Void BeforePlayOutAnim()
  RVA=0x0B120B78  token=0x600035F  System.Void SetBuffIcon(UnityEngine.Sprite sprite)
  RVA=0x0B120BF8  token=0x6000360  System.Void SetStackElementActive(System.Boolean showStackCount)
  RVA=0x045D1C80  token=0x6000361  System.Void SetStackCount(System.Int32 stackCount, System.Boolean playAnim)
  RVA=0x0B120A6C  token=0x6000362  System.Void OnBuffIgnite()
  RVA=0x0432B610  token=0x6000363  System.Void .ctor()
  RVA=0x04DA6460  token=0x6000364  System.Void <SetStackCount>b__15_0()
  RVA=0x0B120C88  token=0x6000365  System.Void <>iFixBaseProxy_DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P0)
  RVA=0x0B120690  token=0x6000366  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
  RVA=0x0B120C80  token=0x6000367  System.Void <>iFixBaseProxy_BeforePlayOutAnim()
  RVA=0x0B120CB0  token=0x6000368  System.Void <>iFixBaseProxy_SetBuffIcon(UnityEngine.Sprite P0)
  RVA=0x0B120CC0  token=0x6000369  System.Void <>iFixBaseProxy_SetStackElementActive(System.Boolean P0)
  RVA=0x0B120CB8  token=0x600036A  System.Void <>iFixBaseProxy_SetStackCount(System.Int32 P0, System.Boolean P1)
  RVA=0x0B120CA8  token=0x600036B  System.Void <>iFixBaseProxy_OnBuffIgnite()
END_CLASS

CLASS: Beyond.UI.UIBuffCell
TYPE:  class
TOKEN: 0x200006C
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  protected         Beyond.UI.UIImage               _buffIcon  // 0x18
  protected         System.Boolean                  _canShowStackCountText  // 0x20
  protected         UnityEngine.GameObject          _stackCountTextNode  // 0x28
  protected         Beyond.UI.UIText                _buffStackCountText  // 0x30
  protected         Beyond.UI.UIAnimationWrapper    _anim  // 0x38
  protected         UnityEngine.CanvasGroup         _canvasGroup  // 0x40
  protected         System.Boolean                  _canShowWarningBg  // 0x48
  protected         Beyond.UI.UIImage               _warningBg  // 0x50
  protected         Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>m_buffPtr  // 0x58
  private           System.UInt64                   <nextInstanceId>k__BackingField  // 0x68
  private           System.String                   <curSpritePath>k__BackingField  // 0x70
PROPERTIES:
  nextInstanceId  get=0x04D86230  set=0x04D86700
  curSpritePath  get=0x04D862B0  set=0x063AD760
  anim  get=0x0B121094
  buffData  get=0x0B1210E4
  orderPriority  get=0x0B1211FC
  buffInstanceUid  get=0x0B121170
METHODS:
  RVA=0x031D68D0  token=0x6000374  System.Void Init(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.LuaResourceLoader loader, System.Action onSpriteLoaded)
  RVA=0x0450F2A0  token=0x6000375  System.Void DataInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B120F58  token=0x6000376  System.Void VisualInit(UnityEngine.Sprite sprite)
  RVA=0x039C3D80  token=0x6000377  System.Void Clear()
  RVA=0x0B120CC8  token=0x6000378  System.Void BeforePlayOutAnim()
  RVA=0x044B39D0  token=0x6000379  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B120E2C  token=0x600037A  System.Void SetVisible(System.Boolean visible)
  RVA=0x045D3B70  token=0x600037B  System.Void SetStackCount(System.Int32 stackCount, System.Boolean playAnim)
  RVA=0x0B120D0C  token=0x600037C  System.Void OnBuffIgnite()
  RVA=0x0B120D50  token=0x600037D  System.Void SetBuffIcon(UnityEngine.Sprite sprite)
  RVA=0x0B120DBC  token=0x600037E  System.Void SetStackElementActive(System.Boolean isActive)
  RVA=0x0B120EE0  token=0x600037F  System.Void SetWarningState(System.Boolean isWarning)
  RVA=0x0432B6A0  token=0x6000380  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIBuffNode
TYPE:  class
TOKEN: 0x200006E
SIZE:  0xB8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.SerializeFieldDictionary<Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle,Beyond.UI.UIBuffCell>_buffStyleCellTemplateDict  // 0x68
  private           Beyond.UI.UIBuffNode.BuffNodeType_buffNodeType  // 0x70
  private           System.Int32                    _maxBuffCellCount  // 0x74
  private           System.Boolean                  m_isFirstInited  // 0x78
  private   readonly Beyond.LuaResourceLoader        m_loader  // 0x80
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_owner  // 0x88
  private   readonly Beyond.DynamicFastLookupCollection<Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle,Beyond.UI.UIBuffNode.BuffCellGroup>m_buffCellGroupDict  // 0x98
  private   readonly Beyond.DynamicFastLookupCollection<System.String,System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>>m_stackBuffsDict  // 0xa0
  private   readonly System.Collections.Generic.List<Beyond.UI.UIBuffCell>m_orderedBuffCellList  // 0xa8
  private   readonly System.Collections.Generic.HashSet<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_addBuffIconBuffer  // 0xb0
  private   static  System.String                   BUFF_ANIM_STRONG_IN  // const
  private   static  System.String                   BUFF_ANIM_IGNITE_OUT  // const
PROPERTIES:
  tickOption  get=0x0440A540
METHODS:
  RVA=0x039C3DF0  token=0x6000384  System.Void _OnFirstInit()
  RVA=0x039C3600  token=0x6000385  System.Void OnPreWarm()
  RVA=0x03A99950  token=0x6000386  System.Void Init(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x03AA0F00  token=0x6000387  System.Void Refresh()
  RVA=0x03AA0130  token=0x6000388  System.Void Clear()
  RVA=0x03AA0270  token=0x6000389  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x0B121268  token=0x600038A  System.Void OnRelease()
  RVA=0x031D53F0  token=0x600038B  System.Void OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x03AA22D0  token=0x600038C  System.Boolean _IsBuffIconInThisNode(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B121330  token=0x600038D  System.Void _OnAddBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x031D5480  token=0x600038E  System.Void _OnRemoveBuff(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x031D5910  token=0x600038F  System.Void _AddBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean playAnim)
  RVA=0x031D5590  token=0x6000390  System.Void _RemoveBuffIconInternal(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B1213D4  token=0x6000391  System.Void _OnBuffEnhanceChanged(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x0B121548  token=0x6000392  System.Void _OnBuffIgnite(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x03A9B430  token=0x6000393  System.Void _RegisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x03A9B600  token=0x6000394  System.Void _UnregisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x031D5E40  token=0x6000395  Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle _GetIconStyle(Beyond.Gameplay.Core.Buff buff)
  RVA=0x0B1216B0  token=0x6000396  System.Void _SetBuffCellSiblingInOrder(Beyond.UI.UIBuffCell buffCell)
  RVA=0x031D5520  token=0x6000397  System.Void _DealWithBuffCellVisible()
  RVA=0x039C4290  token=0x6000398  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000399  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C37238  token=0x600039A  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
  RVA=0x069AFD04  token=0x600039B  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UILifeTimeBuffCell
TYPE:  class
TOKEN: 0x2000078
SIZE:  0x80
EXTENDS: Beyond.UI.UIBuffCell
FIELDS:
  private           Beyond.UI.UIImage               _buffLifeTimeBar  // 0x78
METHODS:
  RVA=0x0B122C50  token=0x60003AE  System.Void OnTick(System.Single deltaTime)
  RVA=0x0B122D44  token=0x60003AF  System.Void SetWarningState(System.Boolean isWarning)
  RVA=0x0432B600  token=0x60003B0  System.Void .ctor()
  RVA=0x0B120690  token=0x60003B1  System.Void <>iFixBaseProxy_OnTick(System.Single P0)
  RVA=0x0B120698  token=0x60003B2  System.Void <>iFixBaseProxy_SetWarningState(System.Boolean P0)
END_CLASS

CLASS: Beyond.UI.UISpellInflictionOnCharCell
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIState.UIStateController_stateController  // 0x18
  private           Beyond.UI.UIAnimationWrapper    _animationWrapper  // 0x20
  private           System.Boolean                  m_isActive  // 0x28
METHODS:
  RVA=0x0B12310C  token=0x60003B3  System.Void SetCellActive(System.Boolean active, System.Boolean forceRefresh)
  RVA=0x05393520  token=0x60003B4  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISpellInflictionOnCharNode
TYPE:  class
TOKEN: 0x200007A
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Transform           _attachBuffCellRoot  // 0x18
  private           Beyond.UI.UISpellInflictionOnCharCell_attachBuffCell  // 0x20
  private           Beyond.UI.UIImage               _abnormalBuffProgressBar  // 0x28
  private           Beyond.UI.UIAnimationWrapper    _abnormalBuffProgressAnim  // 0x30
  private   static  System.Collections.Generic.List<System.String>s_attachBuffIds  // static @ 0x0
  private   static  System.Collections.Generic.List<System.String>s_abnormalBuffIds  // static @ 0x8
  private           System.Boolean                  m_isFirstInited  // 0x38
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_owner  // 0x40
  private           System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_attachBuffs  // 0x50
  private           System.Collections.Generic.List<Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff>>m_abnormalBuffs  // 0x58
  private           Beyond.UI.UIListCache<Beyond.UI.UISpellInflictionOnCharCell>m_attachBuffCellCache  // 0x60
PROPERTIES:
  isInCcDungeon  get=0x03E26950
METHODS:
  RVA=0x03E25CA0  token=0x60003B6  System.Void _OnFirstInit()
  RVA=0x03E264A0  token=0x60003B7  System.Void OnInit(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> abilitySystemPtr)
  RVA=0x03CCEFA0  token=0x60003B8  System.Void OnTick(System.Single deltaTime)
  RVA=0x03E25780  token=0x60003B9  System.Void OnClear()
  RVA=0x03E25B30  token=0x60003BA  System.Void _RegisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x03E25170  token=0x60003BB  System.Void _UnregisterAbilityBuffEvent(Beyond.Gameplay.Core.AbilitySystem abilitySystem)
  RVA=0x0B123244  token=0x60003BC  System.Void _DoRefreshUI()
  RVA=0x03E26830  token=0x60003BD  System.Void _OnBuffIconChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr, System.Boolean applied, Beyond.Gameplay.Core.Buff.FinishReason finishReason)
  RVA=0x0B1235A8  token=0x60003BE  System.Void _OnBuffEnhanceChanged(Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> buffPtr)
  RVA=0x04A30210  token=0x60003BF  System.Void .ctor()
  RVA=0x04B32F20  token=0x60003C0  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIHudFadeBase
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIStyleInputValidType validType  // 0x18
PROPERTIES:
  globalNeedShow  get=0x03968D40
METHODS:
  RVA=0x05397DA0  token=0x60003CB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIHudFadeConsoleController
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x50
EXTENDS: Beyond.UI.UIHudFadeBase
FIELDS:
  public            System.String                   key  // 0x20
  public            System.String                   foldState  // 0x28
  private           UnityEngine.Animator            m_animator  // 0x30
  private           System.Boolean                  m_curShowing  // 0x38
  private           DG.Tweening.Sequence            m_fadeSequence  // 0x40
  private           System.Int32                    m_keyHash  // 0x48
  private           System.Int32                    m_foldStateHash  // 0x4c
METHODS:
  RVA=0x0B12191C  token=0x60003CC  System.Void Awake()
  RVA=0x0B121C68  token=0x60003CD  System.Void OnDestroy()
  RVA=0x0B121DFC  token=0x60003CE  System.Void OnShow()
  RVA=0x0B121ED4  token=0x60003CF  System.Void _CheckNeedShow()
  RVA=0x0B122074  token=0x60003D0  System.Void _OnFadeHud()
  RVA=0x0B1220C4  token=0x60003D1  System.Void _OnInformShow(Beyond.EventData<Beyond.UI.UIStyleInputValidTypeMask>& eventData)
  RVA=0x0B121AD8  token=0x60003D2  System.Void InformShow()
  RVA=0x0B122130  token=0x60003D3  System.Void .ctor()
  RVA=0x0B121EAC  token=0x60003D4  System.Void <_CheckNeedShow>b__10_0()
  RVA=0x0B121EAC  token=0x60003D5  System.Void <InformShow>b__13_0()
END_CLASS

CLASS: Beyond.UI.UIHudFadeController
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x40
EXTENDS: Beyond.UI.UIHudFadeBase
FIELDS:
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x20
  private           System.Boolean                  m_curShowing  // 0x28
  private           DG.Tweening.Sequence            m_fadeSequence  // 0x30
  private   readonly System.Collections.Generic.HashSet<System.String>m_hudPreventFadeState  // 0x38
PROPERTIES:
  needShow  get=0x03968CF0
  setting  get=0x03968FC0
METHODS:
  RVA=0x04792F50  token=0x60003D8  System.Void Awake()
  RVA=0x0B12219C  token=0x60003D9  System.Void OnDestroy()
  RVA=0x03967990  token=0x60003DA  System.Void _OnFadeHud()
  RVA=0x039679D0  token=0x60003DB  System.Void _CheckNeedShow()
  RVA=0x038481A0  token=0x60003DC  System.Void SetLocalNeedShow(System.String state, System.Boolean needShowHud)
  RVA=0x0396B660  token=0x60003DD  System.Void _OnInformShow(Beyond.EventData<Beyond.UI.UIStyleInputValidTypeMask>& eventData)
  RVA=0x0396B6B0  token=0x60003DE  System.Void InformShow()
  RVA=0x048AD370  token=0x60003DF  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIHudFadeTouchSkill
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x38
EXTENDS: Beyond.UI.UIHudFadeBase
FIELDS:
  private           Beyond.UI.UIAnimationWrapper    m_animationWrapper  // 0x20
  private           System.Boolean                  m_curShowing  // 0x28
  private           System.Boolean                  m_animInOrLoop  // 0x29
  private           DG.Tweening.Sequence            m_fadeSequence  // 0x30
METHODS:
  RVA=0x0B122330  token=0x60003E0  System.Void Awake()
  RVA=0x0B1225E4  token=0x60003E1  System.Void OnDestroy()
  RVA=0x0B122778  token=0x60003E2  System.Void OnShow()
  RVA=0x0B12299C  token=0x60003E3  System.Void _CheckNeedShow()
  RVA=0x0B122B88  token=0x60003E4  System.Void _OnFadeHud()
  RVA=0x0B122BD8  token=0x60003E5  System.Void _OnInformShow(Beyond.EventData<Beyond.UI.UIStyleInputValidTypeMask>& eventData)
  RVA=0x0B1224D0  token=0x60003E6  System.Void InformShow()
  RVA=0x0B122C44  token=0x60003E7  System.Void .ctor()
  RVA=0x0B122970  token=0x60003E8  System.Void <_CheckNeedShow>b__7_0()
  RVA=0x0B122834  token=0x60003E9  System.Void <InformShow>b__10_0()
  RVA=0x0B122948  token=0x60003EA  System.Void <InformShow>b__10_1()
END_CLASS

CLASS: Beyond.UI.BattleControllerInputController
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  Beyond.UI.BattleControllerInputController<instance>k__BackingField  // static @ 0x0
  private           Beyond.Input.GamepadKeyCode     m_indicatorKey  // 0x10
  private           Beyond.Input.GamepadKeyCode     m_attackKey  // 0x14
  private           Beyond.Input.GamepadKeyCode     <comboSkillButtonKey>k__BackingField  // 0x18
  private           Beyond.Input.InputManager.DelayedComboBinding[]m_skillTapCombos  // 0x20
  private           Beyond.Input.InputManager.DelayedComboBinding[]m_skillTriggerCombos  // 0x28
PROPERTIES:
  instance  get=0x04890DE0  set=0x0B11EC14
  comboSkillButtonKey  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x0448E8C0  token=0x60003ED  System.Void Create()
  RVA=0x0B11E748  token=0x60003EE  System.Void DestroyInstance()
  RVA=0x0448E9E0  token=0x60003F1  System.Void .ctor()
  RVA=0x0B11E7A0  token=0x60003F2  System.Void Dispose()
  RVA=0x045E0770  token=0x60003F3  System.Boolean GetControllerIndicatorState()
  RVA=0x04790090  token=0x60003F4  System.Boolean CheckNormalAttackBtn()
  RVA=0x0B11E8AC  token=0x60003F5  System.Int32 TryPressControllerIndicatorWhenSkillButtonJustPressed()
  RVA=0x0448EAF0  token=0x60003F6  System.Void _RefreshCachedKeys()
  RVA=0x0448EC10  token=0x60003F7  System.Void _RefreshUltimateMode2Available()
  RVA=0x0448ED80  token=0x60003F8  System.Void _RegisterAllCombos()
  RVA=0x0B11EAE8  token=0x60003F9  System.Void _UnregisterAllCombos()
  RVA=0x0B11E980  token=0x60003FA  System.Void _OnInputActionKeyUpdated(Beyond.EventData<System.String>& data)
END_CLASS

CLASS: Beyond.UI.UIAbilityTargetFollower
TYPE:  class
TOKEN: 0x2000085
SIZE:  0x110
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            Beyond.Gameplay.MountPoint      mountPoint  // 0x70
  public            System.Single                   minDistance  // 0x74
  public            System.Single                   maxDistance  // 0x78
  public            UnityEngine.AnimationCurve      scaleCurve  // 0x80
  public            System.Single                   deltaTowardCamera  // 0x88
  public            System.Boolean                  applyHeadBar2DOffset  // 0x8c
  public            System.Boolean                  outOfScreenCorrection  // 0x8d
  public            System.Single                   startCorrectionScreenHeight  // 0x90
  public            System.Single                   endCorrectionScreenHeight  // 0x94
  public            UnityEngine.Canvas              canvas  // 0x98
  public            UnityEngine.CanvasGroup         canvasGroup  // 0xa0
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0xa8
  protected         Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem>m_target  // 0xb0
  protected         UnityEngine.Transform           m_followTransform  // 0xc0
  protected         UnityEngine.Transform           m_heightFollowTransform  // 0xc8
  protected         UnityEngine.Transform           m_heightBaseTransform  // 0xd0
  protected         System.Single                   m_heightRangeMin  // 0xd8
  protected         System.Single                   m_heightRangeMax  // 0xdc
  private           UnityEngine.Transform           m_cameraTransform  // 0xe0
  private           UnityEngine.Vector3             m_preDeltaPosition  // 0xe8
  private           System.Single                   m_preDeltaDistance  // 0xf4
  private           System.Boolean                  m_needReassignFollowTransform  // 0xf8
  private           System.Single                   m_deltaTowardCamera  // 0xfc
  private           UnityEngine.Vector3             m_originalScale  // 0x100
  private           System.Boolean                  m_distanceVisible  // 0x10c
  private           System.Boolean                  m_finalVisibleForUpdate  // 0x10d
PROPERTIES:
  tickOption  get=0x043D5FA0
  actualVisible  get=0x03EC1520
  needUpdateTransform  get=0x03FC2230
METHODS:
  RVA=0x04248FC0  token=0x6000400  System.Void OnAwake()
  RVA=0x033D28C0  token=0x6000401  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x033D26C0  token=0x6000402  System.Void SetTarget(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x03A9FA10  token=0x6000403  System.Void Clear()
  RVA=0x033D50E0  token=0x6000404  System.Boolean TryGetOriginalFollowPos(UnityEngine.Vector3& followPos)
  RVA=0x041E31F0  token=0x6000405  System.Void UpdateData(System.Single deltaTime)
  RVA=0x033D2A60  token=0x6000406  System.Void _TryRefreshFollowTransform()
  RVA=0x033D2850  token=0x6000407  System.Void _TryRefreshHeightFollowTransform()
  RVA=0x033D2B50  token=0x6000408  System.Void _UpdateSelfTransform()
  RVA=0x033D30B0  token=0x6000409  System.Void _CalculatePosition()
  RVA=0x033D3A90  token=0x600040A  System.Void _UpdatePosition()
  RVA=0x033D3C60  token=0x600040B  System.Void _UpdateScale()
  RVA=0x033D3010  token=0x600040C  System.Void _SetDistanceVisible(System.Boolean visible)
  RVA=0x0464C9C0  token=0x600040D  System.Void OnActualVisibleChange(System.Boolean visible, System.Boolean forceUpdate)
  RVA=0x033D5030  token=0x600040E  UnityEngine.Vector3 GetAdjustedHeightOffset(Beyond.Gameplay.Core.AbilitySystem target)
  RVA=0x039C3550  token=0x600040F  System.Void .ctor()
  RVA=0x04D84AA0  token=0x6000410  System.Void <OnActualVisibleChange>b__45_0()
  RVA=0x0B10A3CC  token=0x6000411  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000412  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x06C37238  token=0x6000413  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UILockAim
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x140
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  public            System.Single                   showCancelProgressRatio  // 0x110
  public            UnityEngine.CanvasGroup         aimGroup  // 0x118
  public            Beyond.UI.UIImage               aimRingImage  // 0x120
  public            Beyond.UI.UIImage               rotateImage  // 0x128
  public            UnityEngine.CanvasGroup         breakingAttackGroup  // 0x130
  private           Beyond.UI.UIAnimationWrapper    m_aimGroupAnimWrapper  // 0x138
METHODS:
  RVA=0x04248D30  token=0x6000414  System.Void OnAwake()
  RVA=0x0B122DE0  token=0x6000415  System.Void OnRelease()
  RVA=0x049BA3A0  token=0x6000416  System.Void Start()
  RVA=0x04B6B5E0  token=0x6000417  System.Void UpdateData(System.Single deltaTime)
  RVA=0x0B123070  token=0x6000418  System.Void _OnLockTargetFromAutoToManual()
  RVA=0x049BA3F0  token=0x6000419  System.Void _RefreshTarget(Beyond.Gameplay.Core.AbilitySystem _)
  RVA=0x039C21E0  token=0x600041A  System.Void .ctor()
  RVA=0x0B123068  token=0x600041B  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x0B115A7C  token=0x600041C  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x0B118024  token=0x600041D  System.Void <>iFixBaseProxy_UpdateData(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIMainCharFootBar
TYPE:  class
TOKEN: 0x2000087
SIZE:  0xF8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.Gameplay.MountPoint      mountPoint  // 0x68
  public            UnityEngine.Vector3             worldOffset  // 0x6c
  public            Beyond.UI.UIAnimationWrapper    dashBar  // 0x78
  public            UnityEngine.Transform           dashBarParent  // 0x80
  public            UnityEngine.Transform           dashBarTransform  // 0x88
  public            UnityEngine.Vector3             dashBarWorldOffset  // 0x90
  public            System.Single                   dashBarScale  // 0x9c
  public            System.Single                   dashBarScaleMobile  // 0xa0
  public            Beyond.UI.FootBarDashCell       dashFillFull  // 0xa8
  public            Beyond.UI.FootBarDashCell       dashFillHalf  // 0xb0
  public            System.Single                   smoothTime  // 0xb8
  private           Beyond.Gameplay.Core.Entity     m_mainChar  // 0xc0
  private           System.Boolean                  m_dashBarNeedShow  // 0xc8
  private           System.Boolean                  m_dashBarShowing  // 0xc9
  private           UnityEngine.Transform           m_target  // 0xd0
  private           UnityEngine.Vector3             m_smoothVelocity  // 0xd8
  private           System.Boolean                  m_notEnoughAnimShowing  // 0xe4
  private           System.Single                   m_curDashCellCount  // 0xe8
  private           System.Single                   m_curDashFillCount  // 0xec
  private           System.Collections.Generic.List<Beyond.UI.FootBarDashCell>m_dashFillList  // 0xf0
  private   static readonly Unity.Profiling.ProfilerMarker  PROFILER_MARKER_UPDATE  // static @ 0x0
PROPERTIES:
  tickOption  get=0x0B131668
METHODS:
  RVA=0x0B1304EC  token=0x600041F  System.Void OnAwake()
  RVA=0x0B1308CC  token=0x6000420  System.Void OnEnable()
  RVA=0x0B130A54  token=0x6000421  System.Void OnRelease()
  RVA=0x0B131300  token=0x6000422  System.Void _OnMainCharChange(Beyond.EventData<Beyond.Gameplay.SetLeaderReason,Beyond.Gameplay.Core.Entity,Beyond.Gameplay.Core.Entity>& data)
  RVA=0x0B1313F4  token=0x6000423  System.Void _ProcessOnMainCharChange()
  RVA=0x0B12FB70  token=0x6000424  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B130DE8  token=0x6000425  System.Void _OnDashCountChanged(System.Single curCount)
  RVA=0x0B131114  token=0x6000426  System.Void _OnDashFailed()
  RVA=0x0B13129C  token=0x6000427  System.Void _OnInteractNpc(Beyond.EventData<System.String,Beyond.Gameplay.Core.Entity>& eventData)
  RVA=0x0B131230  token=0x6000428  System.Void _OnDialogStart(Beyond.EventData<System.Boolean,Beyond.Gameplay.DialogEnums.DialogType>& eventData)
  RVA=0x0B1311CC  token=0x6000429  System.Void _OnDialogFinish(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B13148C  token=0x600042A  System.Void _SetInDialog(System.Boolean inDialog)
  RVA=0x0B1313A4  token=0x600042B  System.Void _OnOutAnimCallback()
  RVA=0x0B131584  token=0x600042C  System.Void .ctor()
  RVA=0x0B131524  token=0x600042D  System.Void .cctor()
  RVA=0x069AFD0C  token=0x600042E  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x600042F  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x6000430  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x6000431  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06BD8710  token=0x6000432  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIMainCharFootBarArc
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x158
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.Gameplay.MountPoint      mountPoint  // 0x68
  public            UnityEngine.Vector3             worldOffset  // 0x6c
  public            Beyond.UI.UIAnimationWrapper    dashBar  // 0x78
  public            Beyond.UI.UIState.UIStateControllerdashBarStateController  // 0x80
  public            UnityEngine.Transform           dashBarParent  // 0x88
  public            UnityEngine.RectTransform       dashBarTransform  // 0x90
  public            Beyond.UI.UIImage               dashFill  // 0x98
  public            Beyond.UI.UIImage               dashFillLow  // 0xa0
  public            Beyond.UI.UIImage               dashFillWhite  // 0xa8
  public            Beyond.UI.UIAnimationWrapper    notEnoughAnim  // 0xb0
  public            UnityEngine.Vector3             dashBarWorldOffset  // 0xb8
  public            System.Single                   dashBarScale  // 0xc4
  public            System.Single                   dashBarScaleMobile  // 0xc8
  public            System.Single                   smoothTime  // 0xcc
  public            System.Int32                    dashCountMin  // 0xd0
  public            System.Int32                    dashCountMax  // 0xd4
  public            System.Single                   fillWhiteHoldTime  // 0xd8
  public            System.Single                   fillWhiteDecreaseSpeed  // 0xdc
  public            UnityEngine.AnimationCurve      scaleCurve  // 0xe0
  public            System.Single                   scaleSmoothTime  // 0xe8
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xf0
  private           Beyond.Gameplay.Core.Entity     m_mainChar  // 0xf8
  private           System.Boolean                  m_dashBarNeedShow  // 0x100
  private           System.Boolean                  m_dashBarShowing  // 0x101
  private           UnityEngine.Transform           m_target  // 0x108
  private           UnityEngine.Vector3             m_smoothVelocity  // 0x110
  private           System.Boolean                  m_notEnoughAnimShowing  // 0x11c
  private           System.Single                   m_originalScale  // 0x120
  private           System.Single                   m_dampingScale  // 0x124
  private   readonly Beyond.Gameplay.PeriodicTimer   m_fillWhiteStayTimer  // 0x128
  private   readonly System.Collections.Generic.HashSet<System.String>m_disableState  // 0x130
  private           DG.Tweening.TweenCallback       m_cachedOnOutAnimCallback  // 0x138
  private           System.Single                   m_curDashCellCount  // 0x140
  private           System.Single                   m_curDashFillAmount  // 0x144
  private           Beyond.UI.UIHeadBar.HpDisplayStatem_dashWhiteState  // 0x148
  private           System.Collections.Generic.Dictionary<System.Int32,System.String>m_dashCountStateNames  // 0x150
PROPERTIES:
  tickOption  get=0x046587D0
METHODS:
  RVA=0x04249F50  token=0x6000434  System.Void OnAwake()
  RVA=0x045136A0  token=0x6000435  System.Void OnEnable()
  RVA=0x0B12F27C  token=0x6000436  System.Void OnRelease()
  RVA=0x0B12F9C0  token=0x6000437  System.Void _OnMainCharChange(Beyond.EventData<Beyond.Gameplay.SetLeaderReason,Beyond.Gameplay.Core.Entity,Beyond.Gameplay.Core.Entity>& data)
  RVA=0x045138E0  token=0x6000438  System.Void _ProcessOnMainCharChange()
  RVA=0x0364D9B0  token=0x6000439  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x04CB1530  token=0x600043A  System.Void _OnDashCountChanged(System.Single curCount)
  RVA=0x0B12F770  token=0x600043B  System.Void _OnDashFailed()
  RVA=0x0B12F95C  token=0x600043C  System.Void _OnInteractNpc(Beyond.EventData<System.String,Beyond.Gameplay.Core.Entity>& eventData)
  RVA=0x0B12F88C  token=0x600043D  System.Void _OnDialogStart(Beyond.EventData<System.Boolean,Beyond.Gameplay.DialogEnums.DialogType>& eventData)
  RVA=0x0B12F828  token=0x600043E  System.Void _OnDialogFinish(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0B12FA38  token=0x600043F  System.Void _OnPlayCutscene(Beyond.EventData<Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle>& eventData)
  RVA=0x0B12F8F8  token=0x6000440  System.Void _OnFinishCutscene(Beyond.EventData<System.Boolean>& eventData)
  RVA=0x0415CBE0  token=0x6000441  System.Void _OnForbidSystemChanged(Beyond.EventData<Beyond.Gameplay.ForbidType,System.Boolean>& eventData)
  RVA=0x0B12FA9C  token=0x6000442  System.Void _SetInDialog(System.Boolean inDialog)
  RVA=0x0458D110  token=0x6000443  System.Void SetUIDisable(System.String key, System.Boolean disable)
  RVA=0x0458D1E0  token=0x6000444  System.Void _RefreshDisableState()
  RVA=0x049B1340  token=0x6000445  System.Void _OnOutAnimCallback()
  RVA=0x0476CE20  token=0x6000446  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000447  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x6000448  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x6000449  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x600044A  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06C37238  token=0x600044B  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIPoiseKnot
TYPE:  class
TOKEN: 0x2000089
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0x18
  private           Beyond.UI.UIAnimationWrapper    _animationWrapper  // 0x20
METHODS:
  RVA=0x03CECE50  token=0x600044C  System.Void SetVisible(System.Boolean visible)
  RVA=0x0B1316B8  token=0x600044D  System.Void KnotBreak()
  RVA=0x03695570  token=0x600044E  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIWeakLockAim
TYPE:  class
TOKEN: 0x200008A
SIZE:  0x118
EXTENDS: Beyond.UI.UIAbilityTargetFollower
FIELDS:
  public            UnityEngine.CanvasGroup         aimGroup  // 0x110
METHODS:
  RVA=0x04249C20  token=0x600044F  System.Void OnAwake()
  RVA=0x038C89F0  token=0x6000450  System.Void Start()
  RVA=0x0B131724  token=0x6000451  System.Void OnRelease()
  RVA=0x038C8A40  token=0x6000452  System.Void _OnMainTargetChanged(Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,System.Boolean>& data)
  RVA=0x038C8A80  token=0x6000453  System.Void _RefreshTarget()
  RVA=0x039C1E70  token=0x6000454  System.Void .ctor()
  RVA=0x0B123068  token=0x6000455  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x0B115A7C  token=0x6000456  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.BlueprintPreviewHelper
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x20
FIELDS:
  private           System.UInt16[][]               m_data  // 0x10
  private           System.Int32                    m_width  // 0x18
  private           System.Int32                    m_height  // 0x1c
METHODS:
  RVA=0x0B123B90  token=0x6000457  System.Void SetSize(System.Int32 width, System.Int32 height)
  RVA=0x0B123770  token=0x6000458  System.Void AddGridValue(System.Int32 x, System.Int32 y, System.Byte value)
  RVA=0x0B1238E8  token=0x6000459  System.Void BatchAddGridValue(System.Int32 xMin, System.Int32 yMin, System.Int32 width, System.Int32 height, System.Byte value)
  RVA=0x0B123AC0  token=0x600045A  System.Byte GetGridValue(System.Int32 x, System.Int32 y, System.Byte& value2)
  RVA=0x041E1670  token=0x600045B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CommonTrackerNodeComp
TYPE:  class
TOKEN: 0x200008C
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.UI.UIBlackboxTracker     blackboxTracker  // 0x18
  public            Beyond.UI.UICampfireTracker     campfireTracker  // 0x20
  public            Beyond.UI.UICommonTracker       commonTracker  // 0x28
  public            Beyond.UI.UICommonLevelTracker  commonLevelTracker  // 0x30
  private   static  System.Single                   TOO_FAR_DISTANCE  // const
METHODS:
  RVA=0x0B123DB0  token=0x600045C  System.Void ShowStyleType(Beyond.Gameplay.CommonTrackingPointStyleType styleType, Beyond.Gameplay.CommonTrackingPointInfoBase trackerData)
  RVA=0x0B123D10  token=0x600045D  System.Void PlayOutAnimation(Beyond.Gameplay.CommonTrackingPointStyleType styleType)
  RVA=0x0B124000  token=0x600045E  System.Void UpdatePosition(Beyond.Gameplay.CommonTrackingPointInfoBase trackerData)
  RVA=0x05393520  token=0x600045F  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CommonTrackerUpdate
TYPE:  class
TOKEN: 0x200008D
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rootTransform  // 0x18
  public            UnityEngine.GameObject          templateTrackerGo  // 0x20
  public            UnityEngine.Transform           trackerParentTransform  // 0x28
  private           System.Collections.Generic.List<Beyond.UI.CommonTrackerUpdate.CommonTrackerItem>m_trackerItemCache  // 0x30
  private           System.Collections.Generic.List<Beyond.UI.CommonTrackerUpdate.CommonTrackerData>m_trackerDatas  // 0x38
  private           System.Collections.Generic.List<Beyond.UI.CommonTrackerUpdate.CommonTrackerItem>m_trackerItems  // 0x40
  public    static readonly System.Int32                    PLAY_ANIM_IN_SIGNAL  // static @ 0x0
PROPERTIES:
  _inFactoryTopView  get=0x034E0C60
METHODS:
  RVA=0x034E0EC0  token=0x6000461  System.Void UpdateCommonTrackers(System.Boolean missionHudOpen, System.Int32 onShowSignal)
  RVA=0x04A34A60  token=0x6000462  System.Void AllTrackersPlayOutAnimation()
  RVA=0x0B124458  token=0x6000463  System.Boolean _InitTrackInfoByIndex(System.Int32 trackerIdx, System.String key, Beyond.Gameplay.CommonTrackingPointInfoBase trackData)
  RVA=0x0B12419C  token=0x6000464  Beyond.UI.CommonTrackerUpdate.CommonTrackerItem _CreateNewMissionTracker()
  RVA=0x034E0E40  token=0x6000465  System.Void _DeleteInvalidTracker()
  RVA=0x0B124584  token=0x6000466  System.Void _ShowTrackerGo(Beyond.UI.CommonTrackerUpdate.CommonTrackerItem trackerItem, Beyond.Gameplay.CommonTrackingPointInfoBase trackerData)
  RVA=0x0B1243E4  token=0x6000467  System.Void _HideTrackerGo(Beyond.UI.CommonTrackerUpdate.CommonTrackerItem trackerItem)
  RVA=0x0B124340  token=0x6000468  System.Int32 _FindTrackIndexByKey(System.String key)
  RVA=0x04A1BDD0  token=0x6000469  System.Void .ctor()
  RVA=0x04D7AA90  token=0x600046A  System.Void .cctor()
END_CLASS

CLASS: Beyond.UI.UIBlackboxTracker
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.RectTransform       arrowRotator  // 0x20
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x28
  public            Beyond.UI.UIText                distanceTxt  // 0x30
METHODS:
  RVA=0x0B12CAFC  token=0x600046D  System.Void UpdatePosition(UnityEngine.Vector2 uiPos, System.Single uiAngle, System.Boolean isOutBound, System.Single distance, System.Boolean needShowDistance)
  RVA=0x05393520  token=0x600046E  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICampfireTracker
TYPE:  class
TOKEN: 0x2000093
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.RectTransform       arrowRotator  // 0x20
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x28
  public            Beyond.UI.UIImage               image  // 0x30
  private   static  System.String                   ORG_ICON  // const
METHODS:
  RVA=0x0B12CE18  token=0x600046F  System.Void UpdatePosition(UnityEngine.Vector2 uiPos, System.Single uiAngle, System.Boolean isOutBound, System.Single distance, System.Boolean needShowDistance)
  RVA=0x0B12CD58  token=0x6000470  System.Void UpdateImage(Beyond.Gameplay.CommonTrackingPointInfoBase trackerData)
  RVA=0x05393520  token=0x6000471  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICommonLevelTracker
TYPE:  class
TOKEN: 0x2000094
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.RectTransform       arrowRotator  // 0x20
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x28
  public            Beyond.UI.UIText                distanceTxt  // 0x30
METHODS:
  RVA=0x0B12CF28  token=0x6000472  System.Void UpdatePosition(Beyond.Gameplay.PosValueState posValueState, System.Boolean needShowDistance, UnityEngine.Vector2 uiPos, System.Single uiAngle, System.Boolean isOutBound, System.Single distance, Beyond.Gameplay.CommonTrackMapTier mapTier)
  RVA=0x05393520  token=0x6000473  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICommonTracker
TYPE:  class
TOKEN: 0x2000095
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       rectTransform  // 0x18
  public            UnityEngine.RectTransform       arrowRotator  // 0x20
  public            Beyond.UI.UIText                distanceTxt  // 0x28
  public            Beyond.UI.UIAnimationWrapper    animationWrapper  // 0x30
  public            Beyond.UI.UIImage               levelImage  // 0x38
  public            UnityEngine.RectTransform       normalNode  // 0x40
  public            Beyond.UI.UIImage               normalShadowImage  // 0x48
  public            Beyond.UI.UIImage               normalImage  // 0x50
  public            Beyond.UI.UIImage               arrowUpImage  // 0x58
  public            UnityEngine.RectTransform       arrowUpRectTransform  // 0x60
  private   static  System.String                   ORG_ICON  // const
METHODS:
  RVA=0x0B12D194  token=0x6000474  System.Void UpdateImage(Beyond.Gameplay.CommonTrackingPointInfoBase trackerData)
  RVA=0x0B12D284  token=0x6000475  System.Void UpdatePosition(Beyond.Gameplay.PosValueState posValueState, System.Boolean needShowDistance, UnityEngine.Vector2 uiPos, System.Single uiAngle, System.Boolean isOutBound, System.Single distance, Beyond.Gameplay.CommonTrackMapTier mapTier)
  RVA=0x05393520  token=0x6000476  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.CoolerUnitProgressBarUI
TYPE:  class
TOKEN: 0x2000096
SIZE:  0x100
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.Core.CoolerUnitComponentm_coolerUnitComponent  // 0xc8
  private           System.Single                   m_displayProgress  // 0xd0
  private   static  System.Single                   LERP_SPEED  // const
  private   static  System.Single                   MAX_STEP  // const
  public            Beyond.UI.UIText                progressDesc  // 0xd8
  public            Beyond.UI.UIText                progressText  // 0xe0
  public            Beyond.UI.UIImage               progressImage  // 0xe8
  public            UnityEngine.Transform           progressUIRoot  // 0xf0
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xf8
PROPERTIES:
  tickOption  get=0x0B124E94
METHODS:
  RVA=0x0B124624  token=0x6000478  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B1249AC  token=0x6000479  System.Void SetUIForward()
  RVA=0x0B124880  token=0x600047A  System.Void SetPos()
  RVA=0x0B124B2C  token=0x600047B  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B124BD8  token=0x600047C  System.Void _TickCanvasGroup(System.Single deltaTime)
  RVA=0x0B124D08  token=0x600047D  System.Void _UpdateProgress(System.Single deltaTime)
  RVA=0x039C2B00  token=0x600047E  System.Void .ctor()
  RVA=0x0B124BD0  token=0x600047F  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x6000480  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B124BC0  token=0x6000481  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x6000482  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x6000483  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.CoolerUnitTemperatureCircleUI
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x138
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private   static  System.String                   AUDIO_EVENT_SUCCEED  // const
  private   static readonly UnityEngine.Color               COLOR_BLUE  // static @ 0x0
  private   static readonly UnityEngine.Color               COLOR_GREEN  // static @ 0x10
  private   static readonly UnityEngine.Color               COLOR_RED  // static @ 0x20
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.Core.CoolerUnitComponentm_coolerUnitComponent  // 0xc8
  public            Beyond.UI.UIImage               bg  // 0xd0
  public            Beyond.UI.UIImage               warningBg  // 0xd8
  public            Beyond.UI.UIText                desc  // 0xe0
  public            UnityEngine.Transform           tempCircleRoot  // 0xe8
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xf0
  private           Beyond.LangKey                  m_customDesc  // 0xf8
  private           Beyond.LangKey                  m_highTempDesc  // 0x108
  private           Beyond.LangKey                  m_lowTempDesc  // 0x118
  private           Beyond.LangKey                  m_goodTempDesc  // 0x128
PROPERTIES:
  tickOption  get=0x0B125B90
METHODS:
  RVA=0x0B124EE4  token=0x6000485  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B125360  token=0x6000486  System.Void SetUIForward()
  RVA=0x0B125234  token=0x6000487  System.Void SetPos()
  RVA=0x0B1254E0  token=0x6000488  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B1258E4  token=0x6000489  System.Void _TickCanvasGroup(System.Single deltaTime)
  RVA=0x0B125A14  token=0x600048A  System.Void _UpdateUIState()
  RVA=0x0B125604  token=0x600048B  System.Void _SetState(Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureState state)
  RVA=0x0B125580  token=0x600048C  System.String _GetLangKeyOrDefault(Beyond.LangKey customKey, System.String defaultKey)
  RVA=0x039C2B00  token=0x600048D  System.Void .ctor()
  RVA=0x0B125B18  token=0x600048E  System.Void .cctor()
  RVA=0x0B124BD0  token=0x600048F  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x6000490  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B124BC0  token=0x6000491  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x6000492  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x6000493  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.CoolerUnitTemperatureUI
TYPE:  class
TOKEN: 0x200009A
SIZE:  0x120
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private   static readonly System.Single                   MIN_Y_POS  // static @ 0x0
  private   static readonly System.Single                   MAX_HEIGHT  // static @ 0x4
  private   static readonly UnityEngine.Color               COLOR_BLUE  // static @ 0x8
  private   static readonly UnityEngine.Color               COLOR_GREEN  // static @ 0x18
  private   static readonly UnityEngine.Color               COLOR_RED  // static @ 0x28
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.Core.CoolerUnitComponentm_coolerUnitComponent  // 0xc8
  public            System.Single                   greenBarTotalHeight  // 0xd0
  public            System.Single                   initBarPosY  // 0xd4
  public            System.Single                   totalBarPosY  // 0xd8
  public            System.Single                   initHandPosY  // 0xdc
  public            System.Single                   totalHandPosY  // 0xe0
  private           System.Single                   m_greenBarCurHeight  // 0xe4
  public            Beyond.UI.UIImage               progressImage  // 0xe8
  public            UnityEngine.RectTransform       greenBar  // 0xf0
  public            UnityEngine.RectTransform       goodHand  // 0xf8
  public            UnityEngine.Transform           temperatureUI  // 0x100
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x108
  private           System.Boolean                  m_initBarSize  // 0x110
  private           System.Boolean                  m_initPosSynced  // 0x111
  private           System.Single                   m_lastMount  // 0x114
  private           Beyond.UI.CoolerUnitTemperatureUI.TempColorStatem_colorState  // 0x118
  private           System.Boolean                  m_colorStateInited  // 0x11c
  private   static  System.Single                   COLOR_EPS  // const
  private   static  System.Single                   SNAP_THRESHOLD  // const
PROPERTIES:
  tickOption  get=0x0B126CBC
METHODS:
  RVA=0x0B125C50  token=0x6000495  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B125BE0  token=0x6000496  System.Void Clear()
  RVA=0x0B125F90  token=0x6000497  System.Void SetUIForward()
  RVA=0x0B125E64  token=0x6000498  System.Void SetPos()
  RVA=0x0B126110  token=0x6000499  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B1261A4  token=0x600049A  System.Void _TickCanvasGroup(System.Single deltaTime)
  RVA=0x0B125DE8  token=0x600049B  System.Void NormalizeRange(System.Single& min, System.Single& max)
  RVA=0x0B1262D4  token=0x600049C  System.Void _UpdateUIInfo(System.Single deltaTime)
  RVA=0x0B126760  token=0x600049D  System.Void _UpdateUIPos(System.Single deltaTime)
  RVA=0x0B126CA8  token=0x600049E  System.Void .ctor()
  RVA=0x0B126C08  token=0x600049F  System.Void .cctor()
  RVA=0x0B124BD0  token=0x60004A0  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x60004A1  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B12619C  token=0x60004A2  System.Void <>iFixBaseProxy_Clear()
  RVA=0x0B124BC0  token=0x60004A3  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x60004A4  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x60004A5  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.CoolerUnitTotalProgressBarUI
TYPE:  class
TOKEN: 0x200009C
SIZE:  0xF8
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private           System.Single                   m_displayProgress  // 0xc0
  private           System.Int32                    m_lastDisplayPercent  // 0xc4
  private   static readonly System.String[]                 S_PERCENT_STRING  // static @ 0x0
  public            Beyond.UI.UIText                progressDesc  // 0xc8
  public            Beyond.UI.UIText                progressText  // 0xd0
  public            Beyond.UI.UIImage               progressImage  // 0xd8
  public            UnityEngine.Transform           progressUIRoot  // 0xe0
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xe8
  private           Beyond.Gameplay.Core.WaterAbsorbedImpactControllerm_module  // 0xf0
PROPERTIES:
  tickOption  get=0x0B1276B4
METHODS:
  RVA=0x0B127044  token=0x60004A7  System.String[] _GeneratePercentStrings()
  RVA=0x0B126D0C  token=0x60004A8  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B1271AC  token=0x60004A9  System.Void _ResetUI()
  RVA=0x0B12711C  token=0x60004AA  System.Void _GetCanvas()
  RVA=0x0B12743C  token=0x60004AB  System.Boolean _TryGetModule(System.UInt64 id)
  RVA=0x0B126E88  token=0x60004AC  System.Void SetUIForward()
  RVA=0x0B126DDC  token=0x60004AD  System.Void SetPos()
  RVA=0x0B126FC8  token=0x60004AE  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B12730C  token=0x60004AF  System.Void _TickCanvasGroup(System.Single deltaTime)
  RVA=0x0B127554  token=0x60004B0  System.Void _UpdateProgress(System.Single deltaTime)
  RVA=0x0B127228  token=0x60004B1  System.Void _SetProgressText(System.Int32 percent)
  RVA=0x0B1276A0  token=0x60004B2  System.Void .ctor()
  RVA=0x0B127650  token=0x60004B3  System.Void .cctor()
  RVA=0x0B124BD0  token=0x60004B4  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x60004B5  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B124BC0  token=0x60004B6  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x60004B7  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x60004B8  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.DoodadFactoryGasCoreScanUI
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x110
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.Core.FactoryGasCoreComponentm_coreComponent  // 0xc8
  private           Beyond.Gameplay.Core.FactoryGasCoreComponentDatam_instanceData  // 0xd0
  private           System.String                   m_domainId  // 0xd8
  public            Beyond.UI.UIText                gasCoreName  // 0xe0
  public            UnityEngine.Transform           icon  // 0xe8
  public            UnityEngine.Transform           high  // 0xf0
  public            UnityEngine.Transform           low  // 0xf8
  private           Beyond.UI.UIText                m_lowText  // 0x100
  private           Beyond.UI.UIText                m_highText  // 0x108
  public    static  System.Int32                    LEVEL_ONE  // const
  public    static  System.Int32                    LEVEL_TWO  // const
  public    static  System.Int32                    LEVEL_THREE  // const
  public    static  System.Int32                    LEVEL_FOUR  // const
PROPERTIES:
  tickOption  get=0x0B128500
METHODS:
  RVA=0x0B127A38  token=0x60004BA  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B127704  token=0x60004BB  System.Void DoRelease()
  RVA=0x0B128068  token=0x60004BC  System.Void SetUIForward()
  RVA=0x0B127E4C  token=0x60004BD  System.Void SetPos()
  RVA=0x0B1281F8  token=0x60004BE  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B1277DC  token=0x60004BF  System.Void InitUI()
  RVA=0x0B127AF0  token=0x60004C0  System.Void RefreshUI()
  RVA=0x0B128270  token=0x60004C1  System.Void _BindDomainChange()
  RVA=0x0B1283FC  token=0x60004C2  System.Void _UnbindDomainChange()
  RVA=0x0B128374  token=0x60004C3  System.Void _OnDomainDevelopmentChange(Beyond.EventData<System.String,System.UInt32,System.UInt64>& data)
  RVA=0x039C2B00  token=0x60004C4  System.Void .ctor()
  RVA=0x0B124BD0  token=0x60004C5  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x60004C6  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B128258  token=0x60004C7  System.Void <>iFixBaseProxy_DoRelease()
  RVA=0x0B124BC0  token=0x60004C8  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x60004C9  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x60004CA  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
  RVA=0x0B128260  token=0x60004CB  System.Void <>iFixBaseProxy_InitUI()
  RVA=0x0B128268  token=0x60004CC  System.Void <>iFixBaseProxy_RefreshUI()
END_CLASS

CLASS: Beyond.UI.DoodadFactoryGasScanUI
TYPE:  class
TOKEN: 0x200009E
SIZE:  0xD8
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.Core.FactoryGasComponentm_coreComponent  // 0xc8
  public            Beyond.UI.UIText                gasName  // 0xd0
  private   static readonly System.String                   X_OFFSET  // static @ 0x0
  private   static readonly System.String                   Z_OFFSET  // static @ 0x8
  private   static readonly System.String                   Y_OFFSET  // static @ 0x10
PROPERTIES:
  tickOption  get=0x0B128D68
METHODS:
  RVA=0x0B128698  token=0x60004CE  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B128550  token=0x60004CF  System.Void DoRelease()
  RVA=0x0B128AA4  token=0x60004D0  System.Void SetUIForward()
  RVA=0x0B128748  token=0x60004D1  System.Void SetPos()
  RVA=0x0B128C34  token=0x60004D2  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B1285CC  token=0x60004D3  System.Void InitUI()
  RVA=0x039C2B00  token=0x60004D4  System.Void .ctor()
  RVA=0x0B128C94  token=0x60004D5  System.Void .cctor()
  RVA=0x0B124BD0  token=0x60004D6  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x60004D7  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B128258  token=0x60004D8  System.Void <>iFixBaseProxy_DoRelease()
  RVA=0x0B124BC0  token=0x60004D9  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x60004DA  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x60004DB  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
  RVA=0x0B128260  token=0x60004DC  System.Void <>iFixBaseProxy_InitUI()
END_CLASS

CLASS: Beyond.UI.FacLineCell
TYPE:  class
TOKEN: 0x200009F
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Single                   HSV_VALUE_RATIO  // const
  private           UnityEngine.RectTransform       _rectTransform  // 0x18
  public            UnityEngine.RectTransform       topAgent  // 0x20
  public            UnityEngine.RectTransform       bottomAgent  // 0x28
  public            Beyond.UI.UIAnimationWrapper    lineAnimationWrapper  // 0x30
  public            Beyond.UI.UIAnimationWrapper    decoAnimationWrapper  // 0x38
  private           System.Collections.Generic.List<Beyond.UI.UIImage>m_imageList  // 0x40
METHODS:
  RVA=0x0B128DB8  token=0x60004DD  System.Void Awake()
  RVA=0x0B1291E0  token=0x60004DE  System.Void PlayLineAnimation(System.String animationName, DG.Tweening.TweenCallback callback)
  RVA=0x0B1292CC  token=0x60004DF  System.Void PlayLineDecoAnimation(System.String animationName, DG.Tweening.TweenCallback callback)
  RVA=0x0B128EFC  token=0x60004E0  System.Void ChangeLineColor(UnityEngine.Color color)
  RVA=0x0B12914C  token=0x60004E1  System.Void ChangeLineWidth(System.Single width)
  RVA=0x048AC280  token=0x60004E2  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.FacLineDrawer
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.RectTransform       m_rectTransform  // 0x18
  private   readonly System.Collections.Generic.Dictionary<System.UInt32,Beyond.UI.FacLineDrawer.LineData>m_lineCache  // 0x20
  private   readonly System.Collections.Generic.List<UnityEngine.GameObject>m_instantiatedLineCell  // 0x28
  private           System.UInt32                   m_nextLineId  // 0x30
  private   static  System.UInt32                   INVALID_LINE_ID  // const
  public            UnityEngine.RectTransform       startDrawAgent  // 0x38
  public            UnityEngine.RectTransform       endDrawAgent  // 0x40
  public            UnityEngine.GameObject          linkLinePrefab  // 0x48
METHODS:
  RVA=0x0B12946C  token=0x60004E5  System.Void Awake()
  RVA=0x0B129DAC  token=0x60004E6  System.Void OnDestroy()
  RVA=0x0B12A0CC  token=0x60004E7  System.Void _Clear()
  RVA=0x0B12A288  token=0x60004E8  UnityEngine.GameObject _CloneLineObject(UnityEngine.GameObject sourceObject, UnityEngine.Transform locateTransform)
  RVA=0x0B12A3C4  token=0x60004E9  System.Void _RefreshCopyLineObjectColor(UnityEngine.GameObject lineObject, UnityEngine.Color color)
  RVA=0x0B129618  token=0x60004EA  System.Void ClearComponent()
  RVA=0x0B1296B8  token=0x60004EB  System.UInt32 DrawLine(Beyond.UI.FacLineCell start, Beyond.UI.FacLineCell end)
  RVA=0x0B129DFC  token=0x60004EC  System.Void PlayLineAnimation(System.UInt32 lineId, System.String animationName, DG.Tweening.TweenCallback callback)
  RVA=0x0B129F84  token=0x60004ED  System.Void PlayPortLineDecoAnimation(System.UInt32 lineId, System.String animationName, System.Boolean isStart, DG.Tweening.TweenCallback callback)
  RVA=0x0B1294EC  token=0x60004EE  System.Void ChangeLineColor(System.UInt32 lineId, UnityEngine.Color color)
  RVA=0x0B129668  token=0x60004EF  System.Void ClearDrawer()
  RVA=0x04A7DBF0  token=0x60004F0  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.FacPipeAnimationSetting
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.String                   PIPE_ANIMATION_PATH  // const
  public            System.String                   decoInAnimation  // 0x18
  public            System.String                   decoOutAnimation  // 0x20
  public            System.String                   itemAnimation  // 0x28
  public            System.Boolean                  needBoldAnimations  // 0x30
  public            System.String                   boldDecoInAnimation  // 0x38
  public            System.String                   boldDecoOutAnimation  // 0x40
  public            System.String                   boldItemAnimation  // 0x48
METHODS:
  RVA=0x03695570  token=0x60004F1  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.FacRegionUpgradeEffectController
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.GameObject          _waitUpgradeNode  // 0x18
  private           UnityEngine.GameObject          _upgradedRegionNode  // 0x20
  private           UnityEngine.GameObject          _waitUpgradeSelectNode  // 0x28
METHODS:
  RVA=0x0B12A640  token=0x60004F2  System.Void _RefreshNodeState(UnityEngine.GameObject node, System.Boolean active)
  RVA=0x0B12A540  token=0x60004F3  System.Void SetUpgradeState(System.Boolean isUpgraded)
  RVA=0x0B12A4C0  token=0x60004F4  System.Void SetSelectState(System.Boolean isSelected)
  RVA=0x0B12A5D0  token=0x60004F5  System.Void SetVisibleState(System.Boolean isVisible)
  RVA=0x05393520  token=0x60004F6  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIFacTechTreePanel
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x150
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  private           System.Boolean                  m_inited  // 0x70
  private           System.Single                   m_zoom  // 0x74
  private           System.Single                   m_zoomValue  // 0x78
  private           UnityEngine.Vector2             m_oriSize  // 0x7c
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>m_zoomTween  // 0x88
  private           System.Single                   m_moveLeft  // 0x90
  private           System.Single                   m_oldMoveLeft  // 0x94
  private           System.Single                   m_moveTime  // 0x98
  private           System.Boolean                  m_openTweenFinished  // 0x9c
  private           UnityEngine.Vector2             m_anchoredPosOffset  // 0xa0
  private           System.Single                   m_horizontalOffset  // 0xa8
  private           System.Single                   m_verticalOffset  // 0xac
  private           UnityEngine.Vector2             m_lastScale  // 0xb0
  private           Beyond.UI.LuaPanel              _techTreeLuaPanel  // 0xb8
  private           Beyond.UI.UITouchPanel          _touchPanel  // 0xc0
  private           UnityEngine.RectTransform       _techTreeRectTransform  // 0xc8
  private           UnityEngine.RectTransform       _techTreePanel  // 0xd0
  private           System.Single                   _titleHight  // 0xd8
  private           System.Single                   _moveSpeed  // 0xdc
  private           System.Single                   _zoomSpeed  // 0xe0
  private           System.Single                   _zoomDefault  // 0xe4
  private           System.Single                   _zoomRangeMin  // 0xe8
  private           System.Single                   _zoomRangeMax  // 0xec
  private           System.Single                   _showNameScale  // 0xf0
  private           System.Single                   _zoomTweenTime  // 0xf4
  private           System.Single                   _moveNodeXOffset  // 0xf8
  private           System.Single                   _moveNodeYOffset  // 0xfc
  private           System.Single                   _startZoomTweenTime  // 0x100
  private           UnityEngine.AnimationCurve      _openUICurve  // 0x108
  private           System.Single                   _movePosTime  // 0x110
  private           UnityEngine.AnimationCurve      _movePosCurve  // 0x118
  private           System.Single                   _movePanelTime  // 0x120
  private           UnityEngine.AnimationCurve      _movePanelCurve  // 0x128
  private           System.Single                   _unlockNodeTime  // 0x130
  private           UnityEngine.AnimationCurve      _unlockNodeCurve  // 0x138
  private           System.Boolean                  m_controllerInitialized  // 0x140
  private           System.Boolean                  m_isDetailShown  // 0x141
  private           System.Single                   _controllerDragSpeed  // 0x144
  private           System.Single                   _controllerZoomSpeed  // 0x148
PROPERTIES:
  tickOption  get=0x0B12F22C
METHODS:
  RVA=0x0B12DA5C  token=0x60004F8  System.Void OnRelease()
  RVA=0x0B12DBDC  token=0x60004F9  System.Void Tick(System.Single deltaTime)
  RVA=0x0B12D674  token=0x60004FA  System.Void InitData()
  RVA=0x0B12E220  token=0x60004FB  System.Void _OnDrag(UnityEngine.Vector2 value)
  RVA=0x0B12E42C  token=0x60004FC  System.Void _OnZoom(System.Single value, System.Boolean needTween)
  RVA=0x0B12EC94  token=0x60004FD  System.Void _SetScale(System.Boolean needTween, System.Single zoomTweenTime, UnityEngine.AnimationCurve curve, DG.Tweening.TweenCallback onFinish)
  RVA=0x0B12E838  token=0x60004FE  System.Void _RefreshAll()
  RVA=0x0B12E5D4  token=0x60004FF  System.Void _PosAdjust()
  RVA=0x0B12E8C4  token=0x6000500  System.Void _RefreshPivotPosition()
  RVA=0x0B12EFD4  token=0x6000501  System.Void _UpdatePositionOffsetAfterZoomed()
  RVA=0x0B12D88C  token=0x6000502  System.Void MoveNode(System.Single dis, System.Single nodeX, System.Single nodeY, System.Boolean ignorePos)
  RVA=0x0B12D5D0  token=0x6000503  System.Void FocusNode(System.Single nodeX, System.Single nodeY, DG.Tweening.TweenCallback onComplete)
  RVA=0x0B12DADC  token=0x6000504  System.Void OnUnlockTech(System.Single nodeX, System.Single nodeY, DG.Tweening.TweenCallback onComplete)
  RVA=0x0B12DDD0  token=0x6000505  System.Void ZoomToFullTechTree(DG.Tweening.TweenCallback onComplete)
  RVA=0x0B12E2C8  token=0x6000506  System.Void _OnUnlockMoveNode(System.Single nodeX, System.Single nodeY)
  RVA=0x0B12E03C  token=0x6000507  System.Void _ControllerUpdate()
  RVA=0x0B12E0E0  token=0x6000508  System.Void _ControllerZoomUpdate()
  RVA=0x0B12DE54  token=0x6000509  System.Void _ControllerDragUpdate()
  RVA=0x0B12DB80  token=0x600050A  System.Void SetDetailShownState(System.Boolean isShown)
  RVA=0x0B12F1D4  token=0x600050B  System.Void .ctor()
  RVA=0x0B12DC60  token=0x600050C  System.Void <InitData>b__39_0(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B12DCEC  token=0x600050D  System.Void <InitData>b__39_1(System.Single zoomValue)
  RVA=0x0B12DD5C  token=0x600050E  System.Void <InitData>b__39_2()
  RVA=0x0B10A3CC  token=0x600050F  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B115A7C  token=0x6000510  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x01538B50  token=0x6000511  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ForgeIronProgressBarUI
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0xF0
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  public            Beyond.UI.UIText                progressDesc  // 0xc0
  public            Beyond.UI.UIText                progressText  // 0xc8
  public            Beyond.UI.UIImage               progressImage  // 0xd0
  public            UnityEngine.Transform           progressUIRoot  // 0xd8
  private           Beyond.Gameplay.InteractiveLogicForgeIronm_logic  // 0xe0
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xe8
PROPERTIES:
  tickOption  get=0x0B12AFD8
METHODS:
  RVA=0x0B12A6F4  token=0x6000513  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B12A818  token=0x6000514  System.Void SetPos()
  RVA=0x0B12A954  token=0x6000515  System.Void SetUIForward()
  RVA=0x0B12AB90  token=0x6000516  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B12ACA4  token=0x6000517  System.Void UpdateProgress()
  RVA=0x039C2B00  token=0x6000518  System.Void .ctor()
  RVA=0x0B124BD0  token=0x6000519  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x600051A  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B124BB8  token=0x600051B  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC0  token=0x600051C  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BC8  token=0x600051D  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ForgeIronTemperatureCircleUI
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x120
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private   static  System.String                   AUDIO_EVENT_SUCCEED  // const
  private   static readonly UnityEngine.Color               COLOR_BLUE  // static @ 0x0
  private   static readonly UnityEngine.Color               COLOR_GREEN  // static @ 0x10
  private   static readonly UnityEngine.Color               COLOR_RED  // static @ 0x20
  public            Beyond.UI.UIImage               bg  // 0xc0
  public            Beyond.UI.UIImage               warningBg  // 0xc8
  public            Beyond.UI.UIText                desc  // 0xd0
  public            UnityEngine.Transform           tempCircleRoot  // 0xd8
  public            System.Int32                    belongRound  // 0xe0
  private           Beyond.Gameplay.InteractiveLogicForgeIronm_logic  // 0xe8
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0xf0
  private           System.String                   m_customDesc  // 0xf8
  private           System.String                   m_highTempDesc  // 0x100
  private           System.String                   m_lowTempDesc  // 0x108
  private           System.String                   m_goodTempDesc  // 0x110
  private           System.Boolean                  m_hasPlaySuccessAudio  // 0x118
PROPERTIES:
  tickOption  get=0x0B12BD54
METHODS:
  RVA=0x0B12B080  token=0x600051F  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B12B028  token=0x6000520  System.Void Clear()
  RVA=0x0B12B178  token=0x6000521  System.Void SetPos()
  RVA=0x0B12B2D0  token=0x6000522  System.Void SetUIForward()
  RVA=0x0B12B628  token=0x6000523  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B12B7B4  token=0x6000524  System.Void UpdateUIState()
  RVA=0x0B12B90C  token=0x6000525  System.Void _SetState(System.Boolean max, System.Boolean low, System.Boolean high, Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureState state)
  RVA=0x039C2B00  token=0x6000526  System.Void .ctor()
  RVA=0x0B12BCDC  token=0x6000527  System.Void .cctor()
  RVA=0x0B124BD0  token=0x6000528  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x6000529  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B12619C  token=0x600052A  System.Void <>iFixBaseProxy_Clear()
  RVA=0x0B124BB8  token=0x600052B  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC0  token=0x600052C  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BC8  token=0x600052D  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.ForgeIronTemperatureUI
TYPE:  class
TOKEN: 0x20000A9
SIZE:  0x110
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  private   static readonly UnityEngine.Color               COLOR_BLUE  // static @ 0x0
  private   static readonly UnityEngine.Color               COLOR_GREEN  // static @ 0x10
  private   static readonly UnityEngine.Color               COLOR_RED  // static @ 0x20
  public            System.Single                   greenBarTotalHeight  // 0xc0
  public            System.Single                   initBarPosY  // 0xc4
  public            System.Single                   totalBarPosY  // 0xc8
  public            System.Single                   initHandPosY  // 0xcc
  public            System.Single                   totalHandPosY  // 0xd0
  private           System.Single                   m_greenBarCurHeight  // 0xd4
  public            Beyond.UI.UIImage               progressImage  // 0xd8
  public            UnityEngine.RectTransform       greenBar  // 0xe0
  public            UnityEngine.RectTransform       goodHand  // 0xe8
  public            UnityEngine.Transform           temperatureUI  // 0xf0
  private           Beyond.Gameplay.InteractiveLogicForgeIronm_logic  // 0xf8
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x100
  private           System.Boolean                  m_initBarSize  // 0x108
  private           System.Single                   m_lastMount  // 0x10c
PROPERTIES:
  tickOption  get=0x0B12CA5C
METHODS:
  RVA=0x0B12BDA4  token=0x600052F  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B12BE9C  token=0x6000530  System.Void SetPos()
  RVA=0x0B12BFD8  token=0x6000531  System.Void SetUIForward()
  RVA=0x0B12C144  token=0x6000532  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B12C264  token=0x6000533  System.Void UpdateUIInfo()
  RVA=0x0B12C554  token=0x6000534  System.Void UpdateUIPos()
  RVA=0x0B12CA48  token=0x6000535  System.Void .ctor()
  RVA=0x0B12C9D0  token=0x6000536  System.Void .cctor()
  RVA=0x0B124BD0  token=0x6000537  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x6000538  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B124BB8  token=0x6000539  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC0  token=0x600053A  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BC8  token=0x600053B  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
END_CLASS

CLASS: Beyond.UI.GuideFakeButton
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x1C0
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            Beyond.UI.UIButton              targetButton  // 0x188
  public            Beyond.UI.UIToggle              targetToggle  // 0x190
  public            Beyond.UI.UIDropdown            targetDropdown  // 0x198
  public            System.String                   targetKeyHintActionId  // 0x1a0
  private           System.String                   m_targetActionId  // 0x1a8
  public            System.Action                   onClick  // 0x1b0
  private           Beyond.Input.InputBindingGroupMonoTargetm_parent  // 0x1b8
METHODS:
  RVA=0x0B1322C0  token=0x600053C  System.Void ForceToggleTargetBinding(System.Boolean active, System.Boolean forDrag)
  RVA=0x0B131FD8  token=0x600053D  System.Void CopyTargetBinding()
  RVA=0x0B132D20  token=0x600053E  System.Void _OnTriggerPlayerAction(Beyond.EventData<System.String>& actionId)
  RVA=0x0B131EC8  token=0x600053F  System.Void ClearTarget()
  RVA=0x0B131D28  token=0x6000540  System.Void ClearBinding()
  RVA=0x0B131C90  token=0x6000541  System.Void Awake()
  RVA=0x0B132730  token=0x6000542  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132B14  token=0x6000543  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B13287C  token=0x6000544  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B1329C8  token=0x6000545  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132490  token=0x6000546  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B131E78  token=0x6000547  System.Void ClearComponent()
  RVA=0x0B132C70  token=0x6000548  System.Void _ClearOnClick()
  RVA=0x04D5DE60  token=0x6000549  System.Void .ctor()
  RVA=0x0B0869D4  token=0x600054A  System.Void <>iFixBaseProxy_Awake()
  RVA=0x0B0C580C  token=0x600054B  System.Void <>iFixBaseProxy_OnPointerDown(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B0C5814  token=0x600054C  System.Void <>iFixBaseProxy_OnPointerUp(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B132C60  token=0x600054D  System.Void <>iFixBaseProxy_OnPointerEnter(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x0B132C68  token=0x600054E  System.Void <>iFixBaseProxy_OnPointerExit(UnityEngine.EventSystems.PointerEventData P0)
END_CLASS

CLASS: Beyond.UI.RegionMap3DCfg
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x60
FIELDS:
  public            UnityEngine.GameObject          model  // 0x10
  public            UnityEngine.GameObject          ground  // 0x18
  public            Beyond.UI.UIRegionMap3DUICell   ui  // 0x20
  public            UnityEngine.GameObject          glitch  // 0x28
  public            UnityEngine.Vector4             uiRect  // 0x30
  private           System.String                   _ui  // 0x40
  private           System.String                   _model  // 0x48
  private           System.String                   _ground  // 0x50
  private           System.Boolean                  <isLoaded>k__BackingField  // 0x58
PROPERTIES:
  isLoaded  get=0x04D86F70  set=0x04D86F80
METHODS:
  RVA=0x03D2A790  token=0x6000551  System.Void Load(Beyond.UI.RegionMapSetting regionMapSetting)
  RVA=0x041E1670  token=0x6000552  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.RegionMapShowType
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.UI.RegionMapShowType     Map  // const
  public    static  Beyond.UI.RegionMapShowType     Watch  // const
METHODS:
END_CLASS

CLASS: Beyond.UI.RegionMapSetting
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x110
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public    static  System.String                   UI_REGION_MAP_TRACK_ICON  // const
  public            UnityEngine.Transform           uiRoot  // 0x70
  public            UnityEngine.Transform           buildingRoot  // 0x78
  public            UnityEngine.Transform           groundRoot  // 0x80
  private           UnityEngine.Transform           _modelNode  // 0x88
  private           UnityEngine.Transform           _uiNode  // 0x90
  private           UnityEngine.RectTransform       _uiPlayerMark  // 0x98
  private           UnityEngine.Color               _normalModelOuterColor  // 0xa0
  private           UnityEngine.Color               _selectedModelOuterColor  // 0xb0
  private           UnityEngine.Vector3             _moveFinalRotation  // 0xc0
  private           Beyond.SerializeFieldDictionary<System.String,Beyond.UI.RegionMap3DCfg>_cfg  // 0xd0
  private           UnityEngine.Transform           m_currLoadedRegionTransform  // 0xd8
  private           Beyond.UI.RegionMapShowType     m_curShowType  // 0xe0
  private           System.Single                   m_animTimer  // 0xe4
  private           System.Single                   m_animDuration  // 0xe8
  private           System.Boolean                  m_playAnim  // 0xec
  private   readonly System.Collections.Generic.List<UnityEngine.RectTransform>m_trackIconList  // 0xf0
  private   readonly System.Collections.Generic.Dictionary<UnityEngine.Renderer,UnityEngine.Material[]>m_refreshedMaterials  // 0xf8
  private           UnityEngine.Color               m_normalLevelModelColor  // 0x100
  private   static  System.String                   OUTER_COLOR_PROPERTY_NAME  // const
  private   static  UnityEngine.MaterialPropertyBlocks_materialPropertyBlock  // static @ 0x0
PROPERTIES:
  tickOption  get=0x04D25FB0
  cfg  get=0x03D2A6A0
  m_materialPropertyBlock  get=0x0B134BD8
METHODS:
  RVA=0x0B1344DC  token=0x6000555  System.Void OnDestroy()
  RVA=0x03D2B8B0  token=0x6000556  System.Void _InitUI()
  RVA=0x036CCE40  token=0x6000557  System.Void _InitPlayerIcon()
  RVA=0x03D2CC60  token=0x6000558  System.Void _InitTrackIcon()
  RVA=0x03D2BE00  token=0x6000559  System.Void _InitSelect()
  RVA=0x036CB4E0  token=0x600055A  System.Void _SetMapPos(UnityEngine.RectTransform rect, UnityEngine.Vector3 pos, Beyond.Gameplay.BasicLoadConfigInfo uiLevelMapConfig)
  RVA=0x037D6C20  token=0x600055B  System.Void _RefreshMaterials()
  RVA=0x0B1349D8  token=0x600055C  System.Void _ClearMaterials()
  RVA=0x03D2C0F0  token=0x600055D  System.Void InitData(Beyond.UI.RegionMapShowType shotType, UnityEngine.Transform center, UnityEngine.Transform moveRoot, System.Single watchRadius)
  RVA=0x036CD1E0  token=0x600055E  System.Void MoveToPlayer(UnityEngine.Transform center, UnityEngine.Transform moveRoot)
  RVA=0x0B134420  token=0x600055F  UnityEngine.Transform GetNodeTransform(System.String levelId)
  RVA=0x0B1340C4  token=0x6000560  UnityEngine.Vector3 GetNodeMoveTargetLocalPosition(System.String levelId, UnityEngine.Camera uiCamera, UnityEngine.Transform moveRoot)
  RVA=0x0B13403C  token=0x6000561  Beyond.UI.RegionMap3DCfg GetCfg(System.String levelId)
  RVA=0x03D2A6D0  token=0x6000562  System.Boolean IsCircleIntersectingRectangle(UnityEngine.Vector2 circleCenter, System.Single circleRadius, UnityEngine.Rect rectangle)
  RVA=0x0B134850  token=0x6000563  System.Void SetLoadedRegionTransform(UnityEngine.Transform regionTransform)
  RVA=0x0B1348B8  token=0x6000564  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> StartRegionRectTransformMoveRotationTween(System.Single tweenDuration, UnityEngine.AnimationCurve tweenCurve)
  RVA=0x0B134664  token=0x6000566  System.Void SetControllerLevelSelectedState(System.String levelId, System.Boolean isSelected)
  RVA=0x04B0B830  token=0x6000567  System.Void .ctor()
  RVA=0x0B10A3CC  token=0x6000568  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B20  token=0x6000569  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.UIFacMainRegionConfig
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x30
FIELDS:
  public            System.String                   levelId  // 0x10
  public            System.Int32                    panelIndex  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Sprite>levelSprites  // 0x20
  public            System.Collections.Generic.List<UnityEngine.Vector3>levelPositions  // 0x28
METHODS:
  RVA=0x045A3970  token=0x600056A  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIFacMainRegion
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.String                   m_levelId  // 0x18
  private           System.Int32                    m_panelIndex  // 0x20
  private           System.Collections.Generic.List<Beyond.UI.UIFacMainRegionConfig>_configList  // 0x28
  private           UnityEngine.RectTransform       _rectTransform  // 0x30
  private           Beyond.UI.UIImage               _image  // 0x38
METHODS:
  RVA=0x0B139A40  token=0x600056B  System.Void _OnMainRegionLevelChanged(System.String levelId, System.Int32 panelIndex)
  RVA=0x03B77F30  token=0x600056C  System.Void _RefreshRegionState()
  RVA=0x03B77CB0  token=0x600056D  System.Void OnDestroy()
  RVA=0x03B77CF0  token=0x600056E  System.Void InitMainRegion(System.String levelId, System.Int32 panelIndex)
  RVA=0x03B77E20  token=0x600056F  System.Void ClearComponent()
  RVA=0x03695570  token=0x6000570  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapCommonStaticElement
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private   static  System.String                   SPRITE_FOLDER  // const
  private           Beyond.UI.UIImage               m_image  // 0x18
  private           Beyond.Gameplay.MapStaticElementDatam_elementData  // 0x20
  private           System.Collections.Generic.List<System.Int32>m_conditionHandles  // 0x28
  private           System.Int32                    m_activeVisibilityIndex  // 0x30
  private           System.Int32                    m_activeImageIndex  // 0x34
  private           System.Boolean                  m_visibilityInitialized  // 0x38
  private           System.Boolean                  m_imageInitialized  // 0x39
METHODS:
  RVA=0x0400CF80  token=0x6000571  System.Void InitCommonStaticElement(Beyond.Gameplay.MapStaticElementData elementData)
  RVA=0x0400D7F0  token=0x6000572  System.Void _RegisterPhaseConditions(Beyond.Gameplay.MapManager mapManager, System.Collections.Generic.List<Beyond.Gameplay.CommonElementVisibilityPhaseData> phases)
  RVA=0x0400CD60  token=0x6000573  System.Void _RegisterPhaseConditions(Beyond.Gameplay.MapManager mapManager, System.Collections.Generic.List<Beyond.Gameplay.CommonElementImagePhaseData> phases)
  RVA=0x0B139FAC  token=0x6000574  System.Void _OnAnyConditionChanged(System.Int32 handle, System.Boolean _)
  RVA=0x0400D0E0  token=0x6000575  System.Void _ReevaluateAndApply()
  RVA=0x0400D340  token=0x6000576  System.Int32 _FindMatchingPhaseIndex(Beyond.Gameplay.MapManager mapManager, System.Collections.Generic.List<Beyond.Gameplay.CommonElementVisibilityPhaseData> phases)
  RVA=0x0400D2C0  token=0x6000577  System.Int32 _FindMatchingPhaseIndex(Beyond.Gameplay.MapManager mapManager, System.Collections.Generic.List<Beyond.Gameplay.CommonElementImagePhaseData> phases)
  RVA=0x0B139DA8  token=0x6000578  System.Void _LoadImage(System.String imgPath, System.Single imgScale)
  RVA=0x0400D6A0  token=0x6000579  System.Void _UnloadImage()
  RVA=0x0400D400  token=0x600057A  System.Void ClearComponent()
  RVA=0x0400CD20  token=0x600057B  System.Void OnDestroy()
  RVA=0x0B139C74  token=0x600057C  System.Boolean EvaluateInitialVisibility(Beyond.Gameplay.MapStaticElementData elementData)
  RVA=0x04CAE1F0  token=0x600057D  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapCrane
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.CombineKey               m_eventKey  // 0x18
  private           Beyond.Resource.CachedPathAssetLoaderm_assetLoader  // 0x20
  private   static  System.String                   CRANE_SPRITE_FOLDER  // const
  private   static  System.String                   CRANE_GAME_VAR_KEY  // const
  private           Beyond.SerializeReferenceDictionary<System.Int64,Beyond.UI.UILevelMapCrane.CraneSpritePath>_stateSpriteDict  // 0x28
  private           UnityEngine.RectTransform       _rectTransform  // 0x30
  private           Beyond.UI.UIImage               _image  // 0x38
METHODS:
  RVA=0x0B13A078  token=0x600057E  System.Void OnDestroy()
  RVA=0x0B13A33C  token=0x600057F  System.Void _RefreshCraneState()
  RVA=0x0B13A274  token=0x6000580  System.Void _ClearSpriteAsset()
  RVA=0x0B13A2DC  token=0x6000581  System.Void _OnCraneStateChanged(Beyond.EventData<System.Int64,System.Int64>& eventData)
  RVA=0x0B13A0D4  token=0x6000582  System.Void _BindAll()
  RVA=0x03D87B40  token=0x6000583  System.Void _UnBindAll()
  RVA=0x0B13A01C  token=0x6000584  System.Void InitCrane()
  RVA=0x03D87B00  token=0x6000585  System.Void ClearComponent()
  RVA=0x03695570  token=0x6000586  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapGameplayArea
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Boolean                  m_initialized  // 0x18
  private           UnityEngine.RectTransform       _rectTransform  // 0x20
  private           UnityEngine.RectTransform       _circleRectTransform  // 0x28
METHODS:
  RVA=0x0B13A624  token=0x6000588  System.Void _RefreshGameplayArea(Beyond.Gameplay.GameplayAreaInstRuntimeData areaData)
  RVA=0x0B13A5C0  token=0x6000589  System.Void Init(Beyond.Gameplay.GameplayAreaInstRuntimeData areaData)
  RVA=0x04CE5040  token=0x600058A  System.Void ClearComponent()
  RVA=0x03695570  token=0x600058B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapLimitInRect
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0xC0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Boolean                  m_needLimitedInRect  // 0x68
  public            UnityEngine.Vector2             originalRectPosition  // 0x6c
  public            UnityEngine.Vector3             originalWorldPosition  // 0x74
  public            System.Boolean                  isLimitedInRect  // 0x80
  public            UnityEngine.Events.UnityEvent<System.Boolean>onIsLimitedInRectChanged  // 0x88
  private           UnityEngine.RectTransform       _rectTransform  // 0x90
  private           UnityEngine.RectTransform       _limitRectTransform  // 0x98
  private           Beyond.UI.UILevelMapLimitInRect.MaskShapeType_maskShapeType  // 0xa0
  private           System.Single                   _circleLimitRadius  // 0xa4
  private           System.Collections.Generic.List<UnityEngine.GameObject>_limitActiveNodes  // 0xa8
  private           System.Collections.Generic.List<UnityEngine.GameObject>_limitInactiveNodes  // 0xb0
  private           System.Collections.Generic.List<UnityEngine.RectTransform>_limitDirectionNodes  // 0xb8
PROPERTIES:
  tickOption  get=0x045B1FA0
METHODS:
  RVA=0x0378F250  token=0x600058D  System.Void LateTick(System.Single deltaTime)
  RVA=0x0378E6D0  token=0x600058E  System.Void OnEnable()
  RVA=0x04751340  token=0x600058F  System.Void OnRelease()
  RVA=0x0378F840  token=0x6000590  System.Void _LimitInRect()
  RVA=0x04751380  token=0x6000591  System.Void ClearComponent()
  RVA=0x04976190  token=0x6000592  System.Void StartLimitMarkInRect()
  RVA=0x04A3D6F0  token=0x6000593  System.Void StopLimitMarkInRect()
  RVA=0x0B13A75C  token=0x6000594  System.Void ForceRefreshLimitState()
  RVA=0x039C2B00  token=0x6000595  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000596  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000597  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x01538B40  token=0x6000598  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x6000599  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.UILevelMapLine
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x90
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.Boolean                  m_initialized  // 0x68
  private           System.Single                   m_basicWidth  // 0x6c
  private           System.Single                   m_basicHeight  // 0x70
  private           System.Single                   m_extraScale  // 0x74
  public            UnityEngine.RectTransform       rectTransform  // 0x78
  public            UnityEngine.RectTransform       referenceTransform  // 0x80
  public            System.Boolean                  onlyHoldOnEnable  // 0x88
PROPERTIES:
  tickOption  get=0x04006900
METHODS:
  RVA=0x0364D290  token=0x600059B  System.Void OnEnable()
  RVA=0x0364EF20  token=0x600059C  System.Void _HoldLineSize()
  RVA=0x0364EE80  token=0x600059D  System.Void LateTick(System.Single deltaTime)
  RVA=0x040E3510  token=0x600059E  System.Void Init(System.Single basicLength)
  RVA=0x044362D0  token=0x600059F  System.Void ClearComponent()
  RVA=0x039C44F0  token=0x60005A0  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60005A1  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x60005A2  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x06BD8710  token=0x60005A3  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UILevelMapMissionArea
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0xC8
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           UnityEngine.GameObject          m_associatedTrackingMark  // 0x68
  private           System.Boolean                  m_initialized  // 0x70
  private   readonly UnityEngine.Vector3[]           m_disappearWorldCorners  // 0x78
  private   readonly UnityEngine.Vector3[]           m_circleWorldCorners  // 0x80
  private           System.Single                   m_missionGuideArea  // 0x88
  public            System.Boolean                  needUseCenterPosition  // 0x8c
  private           Beyond.UI.LuaPanel              _luaPanel  // 0x90
  private           UnityEngine.RectTransform       _rectTransform  // 0x98
  private           UnityEngine.RectTransform       _circleRectTransform  // 0xa0
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0xa8
  private           System.Boolean                  _coexistWithMissionTrack  // 0xb0
  private           System.Single                   _notCoexistGuideAreaRadius  // 0xb4
  private           UnityEngine.RectTransform       _disappearRect  // 0xb8
  private           Beyond.UI.UILevelMapMissionArea.DisappearRectShapeType_disappearRectShape  // 0xc0
PROPERTIES:
  tickOption  get=0x0B13B204
  needCheckMissionTrackCoexist  get=0x0B13B13C
  coexistWithMissionTrack  get=0x0B13B0EC
  notNeedCheckCoexistBecauseOfRadius  get=0x0B13B1A8
METHODS:
  RVA=0x0B13A8CC  token=0x60005A8  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B13AFD4  token=0x60005A9  System.Void _RefreshMissionArea(Beyond.Gameplay.MissionTrackingMarkInstRuntimeDate missionMarkInstRuntimeData)
  RVA=0x0B13A9D8  token=0x60005AA  System.Boolean _IsCircleOutsideDisappearRect()
  RVA=0x0B13A92C  token=0x60005AB  System.Void _DisappearIfOutRect()
  RVA=0x0B13A7FC  token=0x60005AC  System.Void Init(Beyond.Gameplay.MissionTrackingMarkInstRuntimeDate missionMarkInstRuntimeData, UnityEngine.GameObject associatedTrackingMark)
  RVA=0x04CADF70  token=0x60005AD  System.Void ClearComponent()
  RVA=0x0B13A7AC  token=0x60005AE  System.Void ForceRefreshDisappearIfOutRect()
  RVA=0x049709F0  token=0x60005AF  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60005B0  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x60005B1  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UILevelMapMisty
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.CombineKey               m_eventKey  // 0x18
  private           System.Boolean                  m_needBind  // 0x1c
  private   static  System.Int64                    INVISIBLE_STATE_VALUE  // const
  private   static  System.String                   MISTY_GAME_VAR_KEY  // const
METHODS:
  RVA=0x0B13B370  token=0x60005B2  System.Void OnDestroy()
  RVA=0x0B13B5D0  token=0x60005B3  System.Void _RefreshMistyState()
  RVA=0x0B13B560  token=0x60005B4  System.Void _OnMistyStateChanged(Beyond.EventData<System.Int64,System.Int64>& eventData)
  RVA=0x0B13B3C0  token=0x60005B5  System.Void _BindAll()
  RVA=0x0B13B64C  token=0x60005B6  System.Void _UnBindAll()
  RVA=0x0B13B254  token=0x60005B7  System.Void InitMisty()
  RVA=0x0B13B2B8  token=0x60005B8  System.Boolean IsMistyVisible()
  RVA=0x04CE5010  token=0x60005B9  System.Void ClearComponent()
  RVA=0x03695570  token=0x60005BA  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapNarrativeAreaText
TYPE:  class
TOKEN: 0x20000BA
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.UI.UIText                m_text  // 0x18
  private           Beyond.Gameplay.MapStaticElementDatam_elementData  // 0x20
  private           System.Int32                    m_conditionHandle  // 0x28
  private           System.String                   m_currentTextId  // 0x30
  private           System.Boolean                  m_initialized  // 0x38
METHODS:
  RVA=0x040243C0  token=0x60005BB  System.Void InitNarrativeAreaText(Beyond.Gameplay.MapStaticElementData elementData)
  RVA=0x0B13B9A4  token=0x60005BC  System.Void _OnAnyConditionChanged(System.Int32 handle, System.Boolean _)
  RVA=0x04024460  token=0x60005BD  System.Void _ReevaluateAndApply()
  RVA=0x04256330  token=0x60005BE  System.Void ClearComponent()
  RVA=0x042562F0  token=0x60005BF  System.Void OnDestroy()
  RVA=0x0B13B730  token=0x60005C0  Beyond.Gameplay.MapStaticElementConditionExpression _MergeAllConditionExpressions(System.Collections.Generic.List<Beyond.Gameplay.MapNarrativeTextPhaseData> phases)
  RVA=0x045DACD0  token=0x60005C1  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapSwitchBtn
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.UI.UIButton              _button  // 0x18
  private           Beyond.UI.UIText                _text  // 0x20
  private           UnityEngine.GameObject          _mainContent  // 0x28
  private           UnityEngine.RectTransform       _direction  // 0x30
METHODS:
  RVA=0x03D2B190  token=0x60005C2  System.Void InitSwitchButton(System.String targetLevelId, System.Single directionAngle)
  RVA=0x0443CFF0  token=0x60005C3  System.Void ClearComponent()
  RVA=0x03695570  token=0x60005C4  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILevelMapTouchPanelWrapper
TYPE:  class
TOKEN: 0x20000BD
SIZE:  0xB0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UITouchPanel          _touchPanel  // 0x68
  private           System.Single                   _scrollCorrectionCoefficient  // 0x70
  private           UnityEngine.Transform           _raycastRoot  // 0x78
  private           System.Int32                    m_lastTouchCount  // 0x80
  private           UnityEngine.EventSystems.PointerEventDatam_dragPointerEventData  // 0x88
  private           UnityEngine.EventSystems.PointerEventDatam_scrollPointerEventData  // 0x90
  private           System.Boolean                  m_inWrapperDragging  // 0x98
  private           UnityEngine.Vector2             m_lastPointerPos  // 0x9c
  private   readonly System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>m_raycastResults  // 0xa8
PROPERTIES:
  tickOption  get=0x0B13C464
METHODS:
  RVA=0x0B13BA14  token=0x60005C8  System.Void OnAwake()
  RVA=0x0B13BB6C  token=0x60005C9  System.Void Tick(System.Single deltaTime)
  RVA=0x0B13C168  token=0x60005CA  System.Boolean _IsFirstRaycastSelectable(UnityEngine.EventSystems.PointerEventData pointerEventData)
  RVA=0x0B13BC78  token=0x60005CB  System.Void _CheckDrag()
  RVA=0x0B13BFC0  token=0x60005CC  System.Void _CheckScroll()
  RVA=0x0B13C3DC  token=0x60005CD  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60005CE  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60005CF  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B50  token=0x60005D0  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIRegionMap3DPanel
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0xE8
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public    static  System.String                   HOVER_ANIM_NAME  // const
  private           Beyond.UI.LuaPanel              _luaPanel  // 0x70
  private           Beyond.UI.UIAnimationWrapper    _panelAnimationWrapper  // 0x78
  private           UnityEngine.Transform           _zoomRoot  // 0x80
  private           UnityEngine.Transform           _moveRoot  // 0x88
  private           System.Single                   _moveDuration  // 0x90
  private           System.Single                   _completeRatio  // 0x94
  private           UnityEngine.AnimationCurve      _moveCurve  // 0x98
  private           UnityEngine.AnimationCurve      _rotationCurve  // 0xa0
  private           System.Single                   _zoomDistance  // 0xa8
  private           UnityEngine.AnimationCurve      _zoomCurve  // 0xb0
  private           Beyond.UI.RegionMapSetting      m_regionMapSetting  // 0xb8
  private           System.String                   m_toLevel  // 0xc0
  private           System.String                   m_toInsId  // 0xc8
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>m_moveTween  // 0xd0
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions>m_rotateTween  // 0xd8
  private           DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>m_zoomTween  // 0xe0
PROPERTIES:
  tickOption  get=0x0B13D250
METHODS:
  RVA=0x0B13D048  token=0x60005D2  UnityEngine.Transform _GetNodeTransform(System.String levelId)
  RVA=0x0B13CF94  token=0x60005D3  UnityEngine.Vector3 _GetMoveTargetLocalPosition(System.String levelId)
  RVA=0x0B13D0B4  token=0x60005D4  System.Void _OnDoMoveTween()
  RVA=0x0B13C9B0  token=0x60005D5  System.Void InitPanel(Beyond.UI.RegionMapSetting regionMapSetting)
  RVA=0x0B13CAC8  token=0x60005D6  System.Void OnClickLevelBtn(System.String levelId, System.String insId)
  RVA=0x0B13CE10  token=0x60005D7  System.Void OnLevelHoverChanged(System.String levelId, System.Boolean isHover)
  RVA=0x0B13D234  token=0x60005D8  System.Void .ctor()
  RVA=0x0B10A3CC  token=0x60005D9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
END_CLASS

CLASS: Beyond.UI.UIRegionMap3DUICell
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x88
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  public            Beyond.UI.UIText                showName  // 0x70
  public            UnityEngine.RectTransform       iconRoot  // 0x78
  public            Beyond.UI.UIButton              btn  // 0x80
METHODS:
  RVA=0x04D526C0  token=0x60005DA  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRegionMapTrackIcon
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x80
EXTENDS: Beyond.UI.UIPanelBase
FIELDS:
  private           Beyond.UI.UIImage               _icon  // 0x70
  private           Beyond.UI.UIImage               _track  // 0x78
METHODS:
  RVA=0x0B13D29C  token=0x60005DB  System.Void InitData(Beyond.Gameplay.MissionTrackingMarkInstRuntimeDate data)
  RVA=0x04D526C0  token=0x60005DC  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UISettlementRegion
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  private           System.String                   m_settlementId  // 0x18
  private           Beyond.UI.UISettlementRegion.SettlementRegionSpriteDatam_settlementSpriteData  // 0x20
  private           System.Boolean                  m_needBind  // 0x28
  private           Beyond.CombineKey               m_bindKey  // 0x2c
  private           Beyond.SerializeFieldDictionary<System.String,Beyond.UI.UISettlementRegion.SettlementRegionSpriteData>_spriteDict  // 0x30
  private           UnityEngine.RectTransform       _rectTransform  // 0x38
  private           Beyond.UI.UIImage               _image  // 0x40
  public    static  System.String                   SETTLEMENT_REGION_SPRITE_FOLDER  // const
METHODS:
  RVA=0x03091320  token=0x60005DD  System.Void OnDestroy()
  RVA=0x035836E0  token=0x60005DE  System.Void _RefreshSpriteWithTier(System.Int32 tier)
  RVA=0x035835A0  token=0x60005DF  System.Void _RefreshSettlementRegionState()
  RVA=0x0B13D584  token=0x60005E0  System.Void _OnSettlementUnlocked(Beyond.EventData<System.String>& param)
  RVA=0x0B13D488  token=0x60005E1  System.Void _BindAll()
  RVA=0x0B13D5F4  token=0x60005E2  System.Void _UnBindAll()
  RVA=0x03583540  token=0x60005E3  System.Void InitSettlementRegion(System.String settlementId, UnityEngine.Vector3 centerPos)
  RVA=0x046270E0  token=0x60005E4  System.Boolean GetNeedRefreshSettlementRegionTier()
  RVA=0x0B13D424  token=0x60005E5  System.Void RefreshSettlementRegionWithTier(System.Int32 tier)
  RVA=0x03092CE0  token=0x60005E6  System.Void ClearComponent()
  RVA=0x03695570  token=0x60005E7  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.ChessboardDrop
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IDropHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDropEvent  // 0x18
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerEnterEvent  // 0x20
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerExitEvent  // 0x28
METHODS:
  RVA=0x0B1318F8  token=0x60005EA  System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B131994  token=0x60005EB  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B131A30  token=0x60005EC  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B131ACC  token=0x60005ED  System.Boolean _CheckEventValid(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B131830  token=0x60005EE  System.Void ClearComponent()
  RVA=0x0B1318A8  token=0x60005EF  System.Void OnDestroy()
  RVA=0x0B131BAC  token=0x60005F0  System.Void .ctor()
  RVA=0x041E1670  token=0x60005F1  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.PuzzleCellDrag
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler
FIELDS:
  private           Beyond.UI.PuzzleDrag            m_parent  // 0x18
  private           System.Boolean                  _needHovering  // 0x20
METHODS:
  RVA=0x0B132DAC  token=0x60005F2  System.Void Init(Beyond.UI.PuzzleDrag puzzleDrag)
  RVA=0x0B132E10  token=0x60005F3  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132E7C  token=0x60005F4  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132EE8  token=0x60005F5  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132FC0  token=0x60005F6  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B13310C  token=0x60005F7  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B132F54  token=0x60005F8  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B13302C  token=0x60005F9  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B13309C  token=0x60005FA  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x05393520  token=0x60005FB  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.PuzzleControllerHelper
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x30
EXTENDS: UnityEngine.EventSystems.UIBehaviour
FIELDS:
  private           Beyond.UI.LuaPanel              m_luaPanel  // 0x18
  private           UnityEngine.EventSystems.PointerEventDatam_controllerFocusEventData  // 0x20
  private   readonly System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>m_raycastResults  // 0x28
PROPERTIES:
  raycastResults  get=0x0B13365C
METHODS:
  RVA=0x0B133178  token=0x60005FD  System.Void Awake()
  RVA=0x0B133268  token=0x60005FE  System.Int32 TryGetPuzzleSlotIndexByPos(UnityEngine.RectTransform gridCell)
  RVA=0x0B1335DC  token=0x60005FF  System.Void .ctor()
  RVA=0x041E1670  token=0x6000600  System.Void <>iFixBaseProxy_Awake()
END_CLASS

CLASS: Beyond.UI.PuzzleDrag
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x70
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IPointerExitHandler
FIELDS:
  private           UnityEngine.CanvasGroup         m_canvasGroup  // 0x18
  private           Beyond.UI.LuaPanel              m_panel  // 0x20
  private           System.Int32                    m_luaIndex  // 0x28
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onBeginDragEvent  // 0x30
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onDragEvent  // 0x38
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onEndDragEvent  // 0x40
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerDownEvent  // 0x48
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerUpEvent  // 0x50
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerClickEvent  // 0x58
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerEnterEvent  // 0x60
  public            UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>onPointerExitEvent  // 0x68
PROPERTIES:
  panel  get=0x0B133F84
  luaIndex  get=0x0B133F38
METHODS:
  RVA=0x0B1336AC  token=0x6000603  System.Void Awake()
  RVA=0x0B1337FC  token=0x6000604  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B1338F8  token=0x6000605  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133990  token=0x6000606  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133AC8  token=0x6000607  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133C6C  token=0x6000608  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133A3C  token=0x6000609  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133B54  token=0x600060A  System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B133BE0  token=0x600060B  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0B13372C  token=0x600060C  System.Void ClearComponent()
  RVA=0x0B1338A8  token=0x600060D  System.Void OnDestroy()
  RVA=0x0B133CF8  token=0x600060E  System.Void SetIndex(System.Int32 index)
  RVA=0x0B133D50  token=0x600060F  System.Void .ctor()
  RVA=0x041E1670  token=0x6000610  System.Void <>iFixBaseProxy_Awake()
  RVA=0x041E1670  token=0x6000611  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.UI.ScratchOffLotteryCoating
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Texture             brushTexture  // 0x18
  public            UnityEngine.Material            renderMat  // 0x20
  public            UnityEngine.RectTransform       rectTransform  // 0x28
  public            UnityEngine.Canvas              canvas  // 0x30
  public            System.Int32                    lotteryColumn  // 0x38
  private   static  System.Single                   MARK_FIRST  // const
  private   static  System.Single                   MARK_SECOND  // const
  private           System.Int32[][]                m_pixelArray  // 0x40
  private           System.Collections.Generic.Dictionary<System.Int32,System.Boolean>m_texPixelDic  // 0x48
  private           System.Int32                    m_pixelAccount  // 0x50
  private           UnityEngine.RenderTexture       m_renderTexture  // 0x58
  private   static  System.Int32                    RENDER_TEXTURE_WIDTH  // const
  private   static  System.Int32                    RENDER_TEXTURE_HEIGHT  // const
  private           UnityEngine.Vector2             m_lastFramePos  // 0x60
  private           UnityEngine.Vector2             m_thisFramePos  // 0x68
  private           System.Collections.Generic.List<Beyond.UI.ScratchOffLotteryCoating.AwardMarkPoint>m_awardMarkPoints  // 0x70
METHODS:
  RVA=0x0B135E78  token=0x6000612  System.Void Awake()
  RVA=0x0B1363AC  token=0x6000613  System.Void OnDestroy()
  RVA=0x0B136718  token=0x6000614  System.Void _DrawBlank()
  RVA=0x0B136C84  token=0x6000615  System.Void _InitPixelDic()
  RVA=0x0B136B34  token=0x6000616  System.Void _InitMarkPoint()
  RVA=0x0B135F40  token=0x6000617  System.Void Draw(System.Single x, System.Single y)
  RVA=0x02E9E1B0  token=0x6000618  UnityEngine.Rect _ClampDrawRect(System.Single x, System.Single y, System.Single width, System.Single height)
  RVA=0x0B136DE0  token=0x6000619  System.Void _MarkScratchPos(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)
  RVA=0x0B13646C  token=0x600061A  System.Void _AwardMarkScratchPos(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)
  RVA=0x0B136410  token=0x600061B  System.Single OutputCompletePercent()
  RVA=0x0B136230  token=0x600061C  System.Boolean FlagCompleteMarkPoint()
  RVA=0x0B136834  token=0x600061D  System.Collections.Generic.List<UnityEngine.Vector2> _FigureUpCreateRouteLinePoints(UnityEngine.Vector2 start, UnityEngine.Vector2 end, System.Single thickness)
  RVA=0x0B136614  token=0x600061E  UnityEngine.Vector2 _ComputeBrushUV(UnityEngine.Vector2 localPos)
  RVA=0x0B135BE4  token=0x600061F  System.Void ApplyScratch(UnityEngine.Vector2 position)
  RVA=0x0B135EDC  token=0x6000620  System.Void BeginScratch(UnityEngine.Vector2 position)
  RVA=0x0B1361D8  token=0x6000621  System.Void EndScratch(UnityEngine.Vector2 position)
  RVA=0x0B137020  token=0x6000622  UnityEngine.Vector2 _ScreenPosToUiLocalPos(UnityEngine.Vector2 screenPos, UnityEngine.RectTransform transform, UnityEngine.Camera cam)
  RVA=0x0B137104  token=0x6000623  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIMusicVolumeVisual
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x90
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private   static readonly System.Int32                    FREQUENCY_DATA_ID  // static @ 0x0
  private   static readonly System.Int32                    ENABLE_RANDOM_GRAM_ID  // static @ 0x4
  private   static  System.Int32                    BAND_COUNT  // const
  private           Beyond.UI.UIImage               m_uiImage  // 0x68
  private           UnityEngine.Material            m_material  // 0x70
  private           System.Single[]                 m_bandValues  // 0x78
  private           UnityEngine.Vector4[]           m_bandVectors  // 0x80
  private           UnityEngine.UI.Graphic          m_graphic  // 0x88
PROPERTIES:
  tickOption  get=0x0B13C960
  graphic  get=0x0B13C898
METHODS:
  RVA=0x0B13C554  token=0x6000627  System.Void Start()
  RVA=0x0B13C504  token=0x6000628  System.Void OnEnable()
  RVA=0x0B13C4B4  token=0x6000629  System.Void OnDisable()
  RVA=0x0B13C5F4  token=0x600062A  System.Void Tick(System.Single deltaTime)
  RVA=0x0B13C814  token=0x600062B  System.Void .ctor()
  RVA=0x0B13C798  token=0x600062C  System.Void .cctor()
  RVA=0x069AFD0C  token=0x600062D  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B40  token=0x600062E  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x01538B30  token=0x600062F  System.Void <>iFixBaseProxy_OnDisable()
  RVA=0x01538B50  token=0x6000630  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.RiftScanUI
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0xF8
EXTENDS: Beyond.UI.WorldUIController
FIELDS:
  public    static  System.String                   CONTROLLER_STATE_RELEASE  // const
  public    static  System.String                   CONTROLLER_STATE_SUPPRESS  // const
  public    static  System.String                   CONTROLLER_STATE_PROCESSED  // const
  public    static  System.Single                   RIGHT_OFFSET  // const
  public    static  System.Single                   RIFT_CIRCLE_RADIUS  // const
  private           Beyond.Gameplay.Core.Entity     m_targetEntity  // 0xc0
  private           Beyond.Gameplay.InteractiveRiftComponentm_riftComponent  // 0xc8
  private           Beyond.Gameplay.InteractiveRiftComponentDatam_instanceData  // 0xd0
  public            UnityEngine.Transform           pendingLayout  // 0xd8
  public            Beyond.UI.UIText                typeNameText  // 0xe0
  public            Beyond.UI.UIText                typePercentageTxtText  // 0xe8
  public            Beyond.UI.UIState.UIStateControllerctrlState  // 0xf0
PROPERTIES:
  tickOption  get=0x0B135B94
METHODS:
  RVA=0x0B134E4C  token=0x6000632  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B134CAC  token=0x6000633  System.Void DoRelease()
  RVA=0x0B1353FC  token=0x6000634  System.Void SetUIForward()
  RVA=0x0B134FB0  token=0x6000635  System.Void SetPos()
  RVA=0x0B13558C  token=0x6000636  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B134D3C  token=0x6000637  System.Void InitUI()
  RVA=0x0B134EFC  token=0x6000638  System.Void RefreshUI()
  RVA=0x0B13592C  token=0x6000639  System.Void _UpdateUI(System.Int32 currentState)
  RVA=0x0B1355EC  token=0x600063A  System.String _GetStateName()
  RVA=0x0B135718  token=0x600063B  System.String _GetTypeDesText(System.Int32 currentState, System.Int32 state, System.Int32 amount, System.Int32 amountMax)
  RVA=0x0B13583C  token=0x600063C  System.String _GetTypePercentText(System.Int32 amount, System.Int32 amountMax)
  RVA=0x039C2B00  token=0x600063D  System.Void .ctor()
  RVA=0x0B124BD0  token=0x600063E  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x0B124BA8  token=0x600063F  System.Void <>iFixBaseProxy_Init(Beyond.Gameplay.Core.Entity P0, System.String P1, System.UInt64 P2, System.Int32 P3)
  RVA=0x0B128258  token=0x6000640  System.Void <>iFixBaseProxy_DoRelease()
  RVA=0x0B124BC0  token=0x6000641  System.Void <>iFixBaseProxy_SetUIForward()
  RVA=0x0B124BB8  token=0x6000642  System.Void <>iFixBaseProxy_SetPos()
  RVA=0x0B124BC8  token=0x6000643  System.Void <>iFixBaseProxy_TickUI(System.Single P0)
  RVA=0x0B128260  token=0x6000644  System.Void <>iFixBaseProxy_InitUI()
  RVA=0x0B128268  token=0x6000645  System.Void <>iFixBaseProxy_RefreshUI()
END_CLASS

CLASS: Beyond.UI.UIBrokenLine
TYPE:  class
TOKEN: 0x20000CC
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Material            m_material  // 0x18
  private   static  System.Int32                    MAX_SET_VALUE_COUNT  // const
  private   static  System.String                   UI_BROKEN_LINE_SHADER_NAME  // const
  private   static  System.String                   Y_VALUE_PROPERTY_NAME_FORMAT  // const
  private   static  System.String                   X_SCALE_PROPERTY_NAME  // const
  private   static  System.String                   LINE_WEIGHT_PROPERTY_NAME  // const
  private           System.Collections.Generic.List<System.Single>m_tmpList  // 0x20
  private           System.Boolean                  m_isInit  // 0x28
  private           System.Single                   _lineWeight  // 0x2c
METHODS:
  RVA=0x0B137270  token=0x6000646  System.Void Awake()
  RVA=0x0B13772C  token=0x6000647  System.Void _Init()
  RVA=0x0B1372C8  token=0x6000648  System.Void OnDestroy()
  RVA=0x0B137348  token=0x6000649  System.Void SetYValueByArray(System.Single[] yValue, System.Int32 count)
  RVA=0x0B137440  token=0x600064A  System.Void SetYValue(System.Collections.Generic.List<System.Single> yValue, System.Int32 count)
  RVA=0x0B137928  token=0x600064B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UICommonScanController
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0xB0
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           System.UInt64                   <entityLogicId>k__BackingField  // 0x68
  protected         UnityEngine.Transform           m_camera  // 0x70
  protected         Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity>m_entity  // 0x78
  protected         UnityEngine.Transform           m_followTarget  // 0x88
  public            System.Single                   yOffset  // 0x90
  public    static readonly System.Int32                    STATE_ON  // static @ 0x0
  public    static readonly System.Int32                    STATE_OFF  // static @ 0x4
  public    static  System.Int32                    MAX_HP_RATIO  // const
  public    static  System.Single                   TICK_REFRESH_NEED  // const
  public    static  System.Int32                    DONT_REFRESH  // const
  private           System.Boolean                  m_uiActive  // 0x94
  protected         System.Int64                    m_nextRefreshTime  // 0x98
  protected         System.Single                   m_hpRatio  // 0xa0
  protected         UnityEngine.Vector3             m_fixedPoint  // 0xa4
PROPERTIES:
  tickOption  get=0x0B1381F8
  entityLogicId  get=0x04D86230  set=0x04D86700
  uiActive  get=0x0B138248  set=0x0B138298
METHODS:
  RVA=0x0B137C80  token=0x6000651  System.Void SetTarget(Beyond.Gameplay.Core.Entity target, System.Int64 nextRefresh, System.Single hpRatio)
  RVA=0x0B138008  token=0x6000652  System.Void _UpdatePos()
  RVA=0x0B137AA0  token=0x6000653  System.Void SetFixPoint(UnityEngine.Vector3 pos, System.UInt64 waterVolume)
  RVA=0x0B137DDC  token=0x6000654  System.Void Tick(System.Single deltaTime)
  RVA=0x0B137E5C  token=0x6000655  System.Void _FaceToCamera()
  RVA=0x0B1379B0  token=0x6000656  System.Void Clear()
  RVA=0x039C2B00  token=0x6000657  System.Void .ctor()
  RVA=0x0B1381A8  token=0x6000658  System.Void .cctor()
  RVA=0x069AFD0C  token=0x6000659  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x600065A  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UICountDownBar
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private           Beyond.UI.UIText                _countDownTxt  // 0x68
  private           System.Single                   m_countDownTime  // 0x70
  private           System.Int32                    m_id  // 0x74
  private           UnityEngine.Transform           m_target  // 0x78
  private           Beyond.Gameplay.Core.CountDownShowAction.Datam_data  // 0x80
  private           UnityEngine.Transform           m_camera  // 0x88
  private           System.Boolean                  m_update  // 0x90
PROPERTIES:
  tickOption  get=0x0B13886C
  update  get=0x0B1388BC  set=0x0B13890C
METHODS:
  RVA=0x0B13839C  token=0x600065E  System.Void InitData(System.Int32 id, Beyond.Gameplay.Core.AbilitySystem target, Beyond.Gameplay.Core.CountDownShowAction.Data data)
  RVA=0x0B13830C  token=0x600065F  System.Void Clear()
  RVA=0x0B1384EC  token=0x6000660  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B138574  token=0x6000661  System.Void _UpdateData(System.Single deltaTime)
  RVA=0x0B138630  token=0x6000662  System.Void _UpdatePos()
  RVA=0x039C2B00  token=0x6000663  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000664  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000665  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIDoodadMineCoreScanController
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0xC8
EXTENDS: Beyond.UI.UICommonScanController
FIELDS:
  public            UnityEngine.GameObject          canMineIcon  // 0xb0
  public            Beyond.UI.UIText                formingText  // 0xb8
  public            Beyond.UI.UIText                purityText  // 0xc0
METHODS:
  RVA=0x0B138A64  token=0x6000666  System.Void SyncConditonInfo(System.Boolean forming, System.String condition)
  RVA=0x0B138980  token=0x6000667  System.Void SetPurity(System.Boolean forming, System.String conditionText)
  RVA=0x0B138AE0  token=0x6000668  System.Void UpdateUI(System.Boolean forming, System.String condition)
  RVA=0x0B138B5C  token=0x6000669  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIDoodadPlantCoreScanController
TYPE:  class
TOKEN: 0x20000D0
SIZE:  0xF0
EXTENDS: Beyond.UI.UICommonScanController
FIELDS:
  private   static  System.UInt32                   OFF  // const
  private   static  System.UInt32                   DARK  // const
  private   static  System.UInt32                   LIGHT  // const
  private   static  System.UInt32                   GROW  // const
  public            Beyond.UI.UIText                coreNameText  // 0xb0
  public            Beyond.UI.UIText                coreSizeText  // 0xb8
  public            UnityEngine.GameObject          refreshTimeRoot  // 0xc0
  public            Beyond.UI.UIText                refreshTimeText  // 0xc8
  public            System.Collections.Generic.List<UnityEngine.GameObject>flowerDark  // 0xd0
  public            System.Collections.Generic.List<UnityEngine.GameObject>flowerLight  // 0xd8
  public            System.Collections.Generic.List<UnityEngine.GameObject>flowerGrow  // 0xe0
  private           System.Single                   m_tickTimeSum  // 0xe8
METHODS:
  RVA=0x0B138F6C  token=0x600066A  System.Void Tick(System.Single deltaTime)
  RVA=0x0B138BF8  token=0x600066B  System.Void SetCoreInfo(System.String coreName, System.String coreSize)
  RVA=0x0B138DF4  token=0x600066C  System.Void SetRefreshInfo(System.Int64 nextRefreshTime)
  RVA=0x0B138BA8  token=0x600066D  System.Void RefreshTextNow()
  RVA=0x0B138C94  token=0x600066E  System.Void SetQuantityInfo(System.Int32 current, System.Int32 nextRefresh, System.Int32 maxCount)
  RVA=0x0B138E64  token=0x600066F  System.Void SetSingle(System.Int32 index, System.UInt32 state)
  RVA=0x0B13903C  token=0x6000670  System.Void _SyncTimeText()
  RVA=0x0B1392BC  token=0x6000671  System.Void .ctor()
  RVA=0x0B139034  token=0x6000672  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIDoodadScanController
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0xD0
EXTENDS: Beyond.UI.UICommonScanController
FIELDS:
  public            Beyond.UI.UIText                timeCom  // 0xb0
  public            UnityEngine.Transform           refreshFull  // 0xb8
  public            UnityEngine.Transform           refreshProgress  // 0xc0
  private           System.Single                   m_tickTimeSum  // 0xc8
METHODS:
  RVA=0x0B1393CC  token=0x6000673  System.Void SyncRefreshInfo(System.Int64 nextRefresh, System.Single hpRatio)
  RVA=0x0B1394DC  token=0x6000674  System.Void Tick(System.Single deltaTime)
  RVA=0x0B139654  token=0x6000675  System.Void _RefreshCountDown()
  RVA=0x0B1395A4  token=0x6000676  System.Void UpdateUI(System.Int64 nextRefresh, System.Single hpRatio)
  RVA=0x0B1399EC  token=0x6000677  System.Void .ctor()
  RVA=0x0B139034  token=0x6000678  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIGeneralTracker
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Transform           _arrowRotator  // 0x18
  private           UnityEngine.RectTransform       m_rect  // 0x20
PROPERTIES:
  rect  get=0x0B139BC8
METHODS:
  RVA=0x0B139ACC  token=0x600067A  System.Void UpdatePosition(UnityEngine.Vector2 uiPos, System.Single uiAngle, System.Boolean isOutBound)
  RVA=0x05393520  token=0x600067B  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UILiquidPoolScanController
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0xE8
EXTENDS: Beyond.UI.UICommonScanController
FIELDS:
  public            UnityEngine.GameObject          unLimitGroup  // 0xb0
  public            UnityEngine.GameObject          limitedGroup  // 0xb8
  public            Beyond.UI.UIText                currentAmountText  // 0xc0
  public            Beyond.UI.UIText                maxAmountText  // 0xc8
  public            Beyond.UI.UIText                waterNameText  // 0xd0
  private   static  System.String                   EMPTY_LIQUID  // const
  private           Beyond.LangKey                  m_key  // 0xd8
METHODS:
  RVA=0x0B1421E0  token=0x600067C  System.Void SetWaterInfo(System.UInt64 waterId, System.Int32 x, System.Int32 y, System.Int32 z)
  RVA=0x0B142750  token=0x600067D  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIPanelBase
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x70
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public    static  System.String                   UI_SPRITE_PATH  // const
  private           Beyond.LuaResourceLoader        m_loader  // 0x68
PROPERTIES:
  tickOption  get=0x049E6B10
METHODS:
  RVA=0x0B1427D4  token=0x600067F  System.Void OnRelease()
  RVA=0x03849520  token=0x6000680  UnityEngine.Sprite LoadSprite(System.String path, System.String name)
  RVA=0x03C431B0  token=0x6000681  UnityEngine.Sprite LoadSpriteWithOutFormat(System.String path)
  RVA=0x03D2AB50  token=0x6000682  UnityEngine.GameObject LoadGameObject(System.String path)
  RVA=0x03849630  token=0x6000683  System.String GetSpritePath(System.String path, System.String name)
  RVA=0x039C4460  token=0x6000684  System.Void .ctor()
  RVA=0x069AFD04  token=0x6000685  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x069AFD0C  token=0x6000686  Beyond.TickType <>iFixBaseProxy_get_tickOption()
END_CLASS

CLASS: Beyond.UI.UIPowerPoleAutoConnectTracker
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0xD0
EXTENDS: Beyond.UI.UIGeneralTracker
FIELDS:
  private           UnityEngine.GameObject          _rootDistance  // 0x28
  private           Beyond.UI.UIText                _textDistance  // 0x30
  private           UnityEngine.Color               _textColorNormal  // 0x38
  private           UnityEngine.Color               _textColorDisable  // 0x48
  private           UnityEngine.Color               _textColorError  // 0x58
  private           UnityEngine.CanvasGroup         _colorScaleControlGroup  // 0x68
  private           System.Single                   _activeScale  // 0x70
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_defaultIcon  // 0x78
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_diffuserIcon  // 0x80
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_hubIcon  // 0x88
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_subhubIcon  // 0x90
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_terminalIcon  // 0x98
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_stationIcon  // 0xa0
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_udpipeLoaderIcon  // 0xa8
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup_udpipeUnloaderIcon  // 0xb0
  private           Beyond.UI.UIPowerPoleAutoConnectTracker.StatusHintGroup_statusHintGroup  // 0xb8
  private           System.Nullable<Beyond.Gameplay.Factory.PowerAutoConnectStatus>m_currentStatus  // 0xc0
  private           System.Nullable<System.Boolean> m_currentUdpipeError  // 0xc8
METHODS:
  RVA=0x0B142898  token=0x6000687  System.Void UpdateDistance(System.Single distance)
  RVA=0x0B142838  token=0x6000688  System.Void SetDistanceTextVisible(System.Boolean value)
  RVA=0x0B142FC4  token=0x6000689  System.Void _ActualUpdateStatus(Beyond.Gameplay.Factory.PowerAutoConnectStatus status)
  RVA=0x0B142B00  token=0x600068A  System.Void UpdateStatus(Beyond.Gameplay.Factory.PowerAutoConnectStatus status)
  RVA=0x0B142A50  token=0x600068B  System.Void UpdateStatusNormal()
  RVA=0x0B142AA8  token=0x600068C  System.Void UpdateStatusTooFar()
  RVA=0x0B142C54  token=0x600068D  System.Void _ActualUdpipeError(System.Boolean error)
  RVA=0x0B142BA8  token=0x600068E  System.Void UpdateUdpipeError(System.Boolean error)
  RVA=0x0B142E9C  token=0x600068F  System.Void _ActualUpdateIcon(Beyond.UI.UIPowerPoleAutoConnectTracker.IconGroup img)
  RVA=0x0B142964  token=0x6000690  System.Void UpdateNodeType(System.Int32 nodeType)
  RVA=0x0B143240  token=0x6000691  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIPowerPoleFastTravelTracker
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x158
EXTENDS: Beyond.UI.UIGeneralTracker
FIELDS:
  private           Beyond.UI.UIText                _textDistance  // 0x28
  private           UnityEngine.Color               _textColorNormal  // 0x30
  private           UnityEngine.Color               _textColorWarning  // 0x40
  private           UnityEngine.Color               _textColorHighlighted  // 0x50
  private           UnityEngine.Color               _textColorDisabled  // 0x60
  private           UnityEngine.GameObject          _parentNormal  // 0x70
  private           Beyond.UI.UIAnimationWrapper    _animationWrapperNormal  // 0x78
  private           UnityEngine.GameObject          _imageNormal  // 0x80
  private           UnityEngine.GameObject          _imageNormalOther  // 0x88
  private           UnityEngine.GameObject          _imageWarning  // 0x90
  private           UnityEngine.GameObject          _imageHighlighted  // 0x98
  private           UnityEngine.GameObject          _imageHighlightedOther  // 0xa0
  private           UnityEngine.GameObject          _imageDisabled  // 0xa8
  private           UnityEngine.GameObject          _parentUpgraded  // 0xb0
  private           Beyond.UI.UIAnimationWrapper    _animationWrapperUpgraded  // 0xb8
  private           UnityEngine.GameObject          _imageNormalUpgraded  // 0xc0
  private           UnityEngine.GameObject          _imageNormalUpgradedOther  // 0xc8
  private           UnityEngine.GameObject          _imageWarningUpgraded  // 0xd0
  private           UnityEngine.GameObject          _imageHighlightedUpgraded  // 0xd8
  private           UnityEngine.GameObject          _imageHighlightedUpgradedOther  // 0xe0
  private           UnityEngine.GameObject          _imageDisabledUpgraded  // 0xe8
  private           UnityEngine.GameObject          _parentDefaultNext  // 0xf0
  private           Beyond.UI.UIAnimationWrapper    _animationWrapperNext  // 0xf8
  private           UnityEngine.GameObject          _imageNormalDefaultNext  // 0x100
  private           UnityEngine.GameObject          _imageNormalDefaultNextOther  // 0x108
  private           UnityEngine.GameObject          _imageWarningDefaultNext  // 0x110
  private           UnityEngine.GameObject          _imageHighlightedDefaultNext  // 0x118
  private           UnityEngine.GameObject          _imageHighlightedDefaultNextOther  // 0x120
  private           UnityEngine.GameObject          _imageDisabledDefaultNext  // 0x128
  private           UnityEngine.GameObject          _textInfoBg  // 0x130
  private           Beyond.SerializeFieldDictionary<Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleStatus,Beyond.UI.UIPowerPoleFastTravelTracker.TravelPoleStatusSetting>_statusSettingDict  // 0x138
  private           Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleIconm_iconStatus  // 0x140
  private           Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleStatusm_status  // 0x144
  private           System.Boolean                  m_isHighlighted  // 0x148
  private           System.Boolean                  m_isOtherSocial  // 0x149
  private           System.String                   m_distanceFormatText  // 0x150
PROPERTIES:
  isWarning  get=0x0B1441C0
  isDisabled  get=0x0B144148
  allowToHighlight  get=0x0B143CF4
  iconStatus  get=0x0B143DE0  set=0x0B144274
  status  get=0x0B144224  set=0x0B144314
  animationWrapper  get=0x0B143D60
  imageNormal  get=0x0B144040
  imageWarning  get=0x0B1440C4
  imageDisabled  get=0x0B143E30
  imageHighlighted  get=0x0B143F38
  imageNormalOther  get=0x0B143FBC
  imageHighlightedOther  get=0x0B143EB4
METHODS:
  RVA=0x0B1432FC  token=0x60006A6  System.Void PlayFocus()
  RVA=0x0B143450  token=0x60006A7  System.Void UpdateIconStatus(Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleIcon value)
  RVA=0x0B14357C  token=0x60006A8  System.Void UpdateStatus(Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleStatus value)
  RVA=0x0B1434B4  token=0x60006A9  System.Void UpdateIsHighlighted(System.Boolean value)
  RVA=0x0B143518  token=0x60006AA  System.Void UpdateIsOtherSocial(System.Boolean value)
  RVA=0x0B143370  token=0x60006AB  System.Void UpdateDistance(UnityEngine.Vector3 value)
  RVA=0x0B1435E0  token=0x60006AC  System.Void _OnStatusChanged()
  RVA=0x0B14386C  token=0x60006AD  System.Void _UpdateIcon()
  RVA=0x0B14329C  token=0x60006AE  System.Void OnEnable()
  RVA=0x0B143C8C  token=0x60006AF  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIRaceModuleRankController
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0xC8
EXTENDS: Beyond.TickableUIMono
FIELDS:
  private   readonly Beyond.LuaResourceLoader        m_loader  // 0x68
  public            Beyond.UI.UIImage               image  // 0x70
  public            UnityEngine.Transform           greyNode  // 0x78
  public            UnityEngine.Transform           lightNode  // 0x80
  public            Beyond.UI.UIText                text  // 0x88
  public    static  System.String                   FIRST_ICON  // const
  public    static  System.String                   SECOND_ICON  // const
  public    static  System.String                   THIRD_ICON  // const
  public    static  System.String                   FOURTH_ICON  // const
  public    static  System.String                   FIFTH_ICON  // const
  private           UnityEngine.Sprite              m_sprite1  // 0x90
  private           UnityEngine.Sprite              m_sprite2  // 0x98
  private           UnityEngine.Sprite              m_sprite3  // 0xa0
  private           UnityEngine.Sprite              m_sprite4  // 0xa8
  private           UnityEngine.Sprite              m_sprite5  // 0xb0
  private           Beyond.UI.UIRaceModuleRankController.ERankm_rankEnum  // 0xb8
  private           System.Single                   m_restTime  // 0xbc
  private           System.Boolean                  m_begin  // 0xc0
PROPERTIES:
  tickOption  get=0x0B144E30
METHODS:
  RVA=0x0B144B08  token=0x60006B1  System.Void _ResetData()
  RVA=0x0B144380  token=0x60006B2  System.Void OnAwake()
  RVA=0x0B1444B4  token=0x60006B3  System.Void OnRelease()
  RVA=0x0B144B68  token=0x60006B4  System.Void _ShowBaseTrans(System.Boolean show)
  RVA=0x0B1448A8  token=0x60006B5  System.Void _LoadAllSprite()
  RVA=0x0B144C24  token=0x60006B6  System.Void _UpdateRaceModuleRank(Beyond.EventData<System.Int32,System.Single>& rankData)
  RVA=0x0B1445EC  token=0x60006B7  System.Void Tick(System.Single deltaTime)
  RVA=0x0B144DC0  token=0x60006B8  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60006B9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60006BA  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x069AFD04  token=0x60006BB  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x01538B50  token=0x60006BC  System.Void <>iFixBaseProxy_Tick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.UIUdpipeConnectTracker
TYPE:  class
TOKEN: 0x20000DC
SIZE:  0x88
EXTENDS: Beyond.UI.UIGeneralTracker
FIELDS:
  private           UnityEngine.GameObject          _rootDistance  // 0x28
  private           Beyond.UI.UIText                _textDistance  // 0x30
  private           UnityEngine.Color               _textColorNormal  // 0x38
  private           UnityEngine.Color               _textColorError  // 0x48
  private           System.Single                   _activeScale  // 0x58
  private           Beyond.UI.UIUdpipeConnectTracker.IconGroup_defaultIcon  // 0x60
  private           Beyond.UI.UIUdpipeConnectTracker.IconGroup_udpipeLoaderIcon  // 0x68
  private           Beyond.UI.UIUdpipeConnectTracker.IconGroup_udpipeUnloaderIcon  // 0x70
  private           Beyond.UI.UIUdpipeConnectTracker.StatusHintGroup_statusHintGroup  // 0x78
  private           System.Nullable<System.Boolean> m_currentOutofRange  // 0x80
METHODS:
  RVA=0x0B144E80  token=0x60006BD  System.Void UpdateDistance(System.Single distance)
  RVA=0x0B145124  token=0x60006BE  System.Void _ActualUpdateStatus(System.Boolean outOfRange)
  RVA=0x0B144FDC  token=0x60006BF  System.Void UpdateStatus(System.Boolean outOfRange)
  RVA=0x0B145088  token=0x60006C0  System.Void _ActualUpdateIcon(Beyond.UI.UIUdpipeConnectTracker.IconGroup img)
  RVA=0x0B144F4C  token=0x60006C1  System.Void UpdateNodeType(System.Int32 nodeType)
  RVA=0x0B145224  token=0x60006C2  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIVideoHelper
TYPE:  class
TOKEN: 0x20000DF
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            CriWare.CriManaMovieControllerForUIcriMovieController  // 0x18
  public            System.String                   videoPath  // 0x20
  public            System.Boolean                  restartOnEnable  // 0x28
  private           Beyond.UI.UIImage               m_image  // 0x30
METHODS:
  RVA=0x0B145420  token=0x60006C9  System.Void Reset()
  RVA=0x0B1453A4  token=0x60006CA  System.Void ResetVideo()
  RVA=0x0B145274  token=0x60006CB  System.Void Awake()
  RVA=0x0B14533C  token=0x60006CC  System.Void OnEnable()
  RVA=0x0B1454A0  token=0x60006CD  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.UIWaterDroneBar
TYPE:  class
TOKEN: 0x20000E0
SIZE:  0x130
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            Beyond.Gameplay.MountPoint      mountPoint  // 0x68
  public            UnityEngine.Transform           waterDroneBarParent  // 0x70
  public            UnityEngine.Vector3             waterDroneBarLocalOffset  // 0x78
  public            System.Single                   smoothTime  // 0x84
  public            Beyond.UI.UISlider              slider  // 0x88
  private           Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity>m_waterDronePtr  // 0x90
  private           UnityEngine.Transform           _waterDroneTrans  // 0xa0
  private           UnityEngine.Vector3             m_smoothVelocity  // 0xa8
  public            Beyond.UI.UIState.UIStateControllerwaterStateController  // 0xb8
  public            Beyond.UI.UIState.UIStateControllericonStateController  // 0xc0
  private   static readonly Unity.Profiling.ProfilerMarker  PROFILER_MARKER_UPDATE  // static @ 0x0
  public    static  System.Single                   SLIDER_MAX_VALUE  // const
  public            Beyond.UI.UIAnimationWrapper    uiAnimationWrapper  // 0xc8
  private           UnityEngine.Animation           _animation  // 0xd0
  private           UnityEngine.AnimationClip       _switchStateClip  // 0xd8
  private           UnityEngine.AnimationClip       _farLoopClip  // 0xe0
  private           UnityEngine.AnimationClip       _emptyLoopClip  // 0xe8
  private           System.String                   m_switchStateClipName  // 0xf0
  private           System.String                   m_farLoopClipName  // 0xf8
  private           System.String                   m_emptyLoopClipName  // 0x100
  private           System.Single                   _clampedYawAngle  // 0x108
  private           System.Single                   _clampedPitchAngle  // 0x10c
  private           System.Collections.Generic.List<System.String>_hydrantTemplateIds  // 0x110
  private           System.Collections.Generic.List<System.String>_tankTemplateIds  // 0x118
  private           System.Boolean                  m_isInfinityLiquid  // 0x120
  private           System.Boolean                  m_isBagEmpty  // 0x121
  private           System.Boolean                  m_noRemainLiquid  // 0x122
  private           System.Boolean                  m_isBagEmptyAndNoRemainLiquid  // 0x123
  private           System.Int32                    m_stateChangeFrame  // 0x124
  private           System.Boolean                  m_stateDirty  // 0x128
  private           System.Boolean                  m_isPlayingFarLoop  // 0x129
PROPERTIES:
  tickOption  get=0x0B146E44
METHODS:
  RVA=0x0B145A90  token=0x60006CF  System.Void OnAwake()
  RVA=0x0B146808  token=0x60006D0  System.Void _OnWaterDroneInfinityLiquidChanged(Beyond.EventData<System.Boolean>& data)
  RVA=0x0B146674  token=0x60006D1  System.Void _OnWaterDroneEmptyChanged(Beyond.EventData<System.Boolean>& data)
  RVA=0x0B146874  token=0x60006D2  System.Void _OnWaterDroneInteractiveChanged(Beyond.EventData<System.String>& data)
  RVA=0x0B146954  token=0x60006D3  System.Void _OnWaterDroneSourceTypeChanged(Beyond.EventData<Beyond.Gameplay.WaterDroneSourceType>& data)
  RVA=0x0B1468E0  token=0x60006D4  System.Void _OnWaterDroneRelaxChanged(Beyond.EventData<System.Boolean>& data)
  RVA=0x0B1467A4  token=0x60006D5  System.Void _OnWaterDroneHydrantStateChanged(Beyond.EventData<Beyond.Gameplay.CustomAbilityComponent.WaterDroneByHydrantState>& data)
  RVA=0x0B1466E0  token=0x60006D6  System.Void _OnWaterDroneHydrantStateChanged(Beyond.Gameplay.CustomAbilityComponent.WaterDroneByHydrantState state)
  RVA=0x0B145FE4  token=0x60006D7  System.Void OnEnable()
  RVA=0x0B146130  token=0x60006D8  System.Void OnRelease()
  RVA=0x0B146524  token=0x60006D9  System.Void _OnWaterDroneChange(Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity> waterDronePtr)
  RVA=0x0B14649C  token=0x60006DA  System.Void _ClearWaterDrone()
  RVA=0x0B1454EC  token=0x60006DB  System.Void LateTick(System.Single deltaTime)
  RVA=0x0B146B20  token=0x60006DC  System.Void _UpdateState()
  RVA=0x0B1469C0  token=0x60006DD  System.Void _SetIconStateController(System.String stateName)
  RVA=0x0B146A6C  token=0x60006DE  System.Void _UpdateSlider()
  RVA=0x04D35770  token=0x60006DF  System.Void .ctor()
  RVA=0x04D38B40  token=0x60006E0  System.Void .cctor()
  RVA=0x069AFD0C  token=0x60006E1  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06C294C4  token=0x60006E2  System.Void <>iFixBaseProxy_OnAwake()
  RVA=0x01538B40  token=0x60006E3  System.Void <>iFixBaseProxy_OnEnable()
  RVA=0x069AFD04  token=0x60006E4  System.Void <>iFixBaseProxy_OnRelease()
  RVA=0x06BD8710  token=0x60006E5  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: Beyond.UI.WaterDroneJoystickCtrl
TYPE:  class
TOKEN: 0x20000E1
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIJoystick            _joystick  // 0x18
  public            System.Single                   cameraInputScaleX  // 0x20
  public            System.Single                   cameraInputScaleY  // 0x24
METHODS:
  RVA=0x04D848C0  token=0x60006E6  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.WorldUIController
TYPE:  class
TOKEN: 0x20000E2
SIZE:  0xC0
EXTENDS: Beyond.TickableMono
FIELDS:
  private           System.UInt64                   <entityLogicId>k__BackingField  // 0x68
  protected         UnityEngine.Transform           m_camera  // 0x70
  protected         Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity>m_entity  // 0x78
  protected         UnityEngine.Vector3             m_parentPos  // 0x88
  protected         Beyond.Gameplay.Core.InteractiveLogicControllerm_logicController  // 0x98
  protected         UnityEngine.GameObject          m_postModel  // 0xa0
  protected         Beyond.Gameplay.Core.InteractiveLogicBaseComponentm_logicBaseComponent  // 0xa8
  protected         System.UInt64                   m_id  // 0xb0
  protected         System.Int32                    m_typeSegment  // 0xb8
  protected         System.Boolean                  m_uiActive  // 0xbc
PROPERTIES:
  entityLogicId  get=0x04D86230  set=0x04D86700
  tickOption  get=0x0B14755C
  uiActive  get=0x0B1475AC  set=0x0B1475FC
METHODS:
  RVA=0x0B147120  token=0x60006EC  System.Void Init(Beyond.Gameplay.Core.Entity target, System.String path, System.UInt64 id, System.Int32 typeSegment)
  RVA=0x0B147490  token=0x60006ED  System.Void TryGetNeedComponent()
  RVA=0x0B147330  token=0x60006EE  System.Void SetPos()
  RVA=0x0B147374  token=0x60006EF  System.Void SetUIForward()
  RVA=0x0B146F30  token=0x60006F0  System.Void FaceToCamera()
  RVA=0x0B1470DC  token=0x60006F1  System.Void InitUI()
  RVA=0x0B1472EC  token=0x60006F2  System.Void RefreshUI()
  RVA=0x0B14740C  token=0x60006F3  System.Void Tick(System.Single deltaTime)
  RVA=0x0B1473B8  token=0x60006F4  System.Void TickUI(System.Single deltaTime)
  RVA=0x0B146E94  token=0x60006F5  System.Void Clear()
  RVA=0x0B147280  token=0x60006F6  System.Void OnRelease()
  RVA=0x0B146EEC  token=0x60006F7  System.Void DoRelease()
  RVA=0x039C2B00  token=0x60006F8  System.Void .ctor()
  RVA=0x069AFD0C  token=0x60006F9  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x60006FA  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x069AFD04  token=0x60006FB  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.UI.Mission.MissionHudBGSizeUpdater
TYPE:  class
TOKEN: 0x20000E3
SIZE:  0x98
EXTENDS: Beyond.TickableUIMono
FIELDS:
  public            UnityEngine.RectTransform       bgTransform  // 0x68
  public            UnityEngine.RectTransform       objectiveList  // 0x70
  public            UnityEngine.RectTransform       objectiveContent  // 0x78
  public            UnityEngine.RectTransform       leftLayoutTransform  // 0x80
  public            UnityEngine.UI.VerticalLayoutGroupleftNodeVerticalLayoutGroup  // 0x88
  public            System.Single                   bgSizeGetterDeltaY  // 0x90
PROPERTIES:
  tickOption  get=0x04A79840
METHODS:
  RVA=0x03FE7A80  token=0x60006FD  System.Void LateTick(System.Single deltaTime)
  RVA=0x033F7740  token=0x60006FE  System.Void UpdateBgSize()
  RVA=0x04D830D0  token=0x60006FF  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000700  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x06BD8710  token=0x6000701  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x20000E6
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000703  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x063625A4  token=0x6000704  System.Void __Gen_Wrap_0()
  RVA=0x063632E4  token=0x6000705  System.Boolean __Gen_Wrap_1(System.Object P0)
  RVA=0x066C288C  token=0x6000706  System.Void __Gen_Wrap_2(System.Object P0, System.Single P1)
  RVA=0x0B13F208  token=0x6000707  System.Void __Gen_Wrap_3(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, System.Boolean P2, Beyond.Gameplay.Core.Buff.FinishReason P3)
  RVA=0x06377B18  token=0x6000708  System.Void __Gen_Wrap_4(System.Object P0, System.Object P1)
  RVA=0x063633B4  token=0x6000709  System.Void __Gen_Wrap_5(System.Object P0)
  RVA=0x0B14099C  token=0x600070A  System.Boolean __Gen_Wrap_6(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1)
  RVA=0x0B14110C  token=0x600070B  Beyond.Gameplay.Core.Buff __Gen_Wrap_7(System.Object P0)
  RVA=0x066C2B40  token=0x600070C  System.Void __Gen_Wrap_8(System.Object P0, System.Boolean P1)
  RVA=0x0B141D20  token=0x600070D  System.Void __Gen_Wrap_9(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, System.Object P2)
  RVA=0x06366E40  token=0x600070E  Beyond.TickType __Gen_Wrap_10(System.Object P0)
  RVA=0x0B13D6E8  token=0x600070F  UnityEngine.RectTransform __Gen_Wrap_11(System.Object P0)
  RVA=0x0B13D7C4  token=0x6000710  UnityEngine.Vector3 __Gen_Wrap_12(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x0B13D8F0  token=0x6000711  Beyond.UI.UIAnimationWrapper __Gen_Wrap_13(System.Object P0)
  RVA=0x0B13D9E0  token=0x6000712  System.Void __Gen_Wrap_14(System.Object P0, System.Object P1, UnityEngine.Vector3 P2)
  RVA=0x0B13DB04  token=0x6000713  System.Void __Gen_Wrap_15(System.Object P0, Beyond.EventData<System.Int32,System.Single,System.Single>& P1)
  RVA=0x0B13DC20  token=0x6000714  UnityEngine.Vector3 __Gen_Wrap_16()
  RVA=0x0B13DD14  token=0x6000715  Beyond.UI.DamageTextBase __Gen_Wrap_17(System.Object P0, Beyond.UI.DamageTextType P1)
  RVA=0x066F4838  token=0x6000716  System.Void __Gen_Wrap_18(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x066F3838  token=0x6000717  System.Void __Gen_Wrap_19(System.Object P0, System.Int32 P1)
  RVA=0x0B13DE0C  token=0x6000718  System.Void __Gen_Wrap_20(System.Object P0, UnityEngine.Color P1)
  RVA=0x0B13DF20  token=0x6000719  System.Void __Gen_Wrap_21(System.Object P0, System.Object P1, System.Object P2, System.Nullable<UnityEngine.Vector3> P3)
  RVA=0x0B13E060  token=0x600071A  UnityEngine.Vector2 __Gen_Wrap_22(System.Object P0)
  RVA=0x0B13E150  token=0x600071B  System.Void __Gen_Wrap_23(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x0B13E264  token=0x600071C  UnityEngine.Vector3 __Gen_Wrap_24(System.Object P0)
  RVA=0x0B13E378  token=0x600071D  UnityEngine.Vector2 __Gen_Wrap_25(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x0B13E490  token=0x600071E  System.Boolean __Gen_Wrap_26(System.Object P0, UnityEngine.Vector3 P1)
  RVA=0x0B13E5B8  token=0x600071F  System.Void __Gen_Wrap_27(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem.Modifier>& P1)
  RVA=0x09C8E3F8  token=0x6000720  System.Void __Gen_Wrap_28(System.Object P0, Beyond.GEnums.EnergyShardType P1, System.Boolean P2)
  RVA=0x0B13E770  token=0x6000721  System.Void __Gen_Wrap_29(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String,Beyond.GEnums.EnergyShardType>& P1)
  RVA=0x0B13E894  token=0x6000722  System.Void __Gen_Wrap_30(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,UnityEngine.Vector3,UnityEngine.Vector2,System.String>& P1)
  RVA=0x0B13E9B8  token=0x6000723  System.Void __Gen_Wrap_31(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,Beyond.Gameplay.Core.AbilitySystemForEnemyPart,Beyond.Gameplay.PhysicalInflictionType>& P1)
  RVA=0x0B13EAD4  token=0x6000724  System.Void __Gen_Wrap_32(System.Object P0, Beyond.EventData<System.Boolean>& P1)
  RVA=0x0B13EBF4  token=0x6000725  System.Void __Gen_Wrap_33(System.Object P0, Beyond.EventData<UnityEngine.Vector3,System.String,UnityEngine.Color>& P1)
  RVA=0x0B13ED18  token=0x6000726  System.Void __Gen_Wrap_34(System.Object P0, Beyond.EventData<System.UInt64,System.Int32>& P1)
  RVA=0x06364290  token=0x6000727  System.Void __Gen_Wrap_35(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0B13EE3C  token=0x6000728  Beyond.UI.DamageTextBaseV2 __Gen_Wrap_36(System.Object P0, Beyond.UI.DamageTextType P1, System.Object P2)
  RVA=0x066C0730  token=0x6000729  System.Single __Gen_Wrap_37(System.Object P0)
  RVA=0x0B13EF60  token=0x600072A  UnityEngine.Vector2 __Gen_Wrap_38(System.Object P0, System.Single P1, System.Single P2, System.Boolean P3, UnityEngine.Vector3 P4)
  RVA=0x0B13F0D8  token=0x600072B  System.Void __Gen_Wrap_39(System.Object P0, UnityEngine.Vector2 P1, System.Single P2)
  RVA=0x0B13F344  token=0x600072C  Beyond.UI.HeadBarCtrl.HeadBarType __Gen_Wrap_40(Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> P0)
  RVA=0x0B13F44C  token=0x600072D  System.Boolean __Gen_Wrap_41(System.Object P0, UnityEngine.Vector3& P1)
  RVA=0x07199240  token=0x600072E  System.Void __Gen_Wrap_42(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x071C15C8  token=0x600072F  System.Void __Gen_Wrap_43(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x071C13A4  token=0x6000730  System.Void __Gen_Wrap_44(System.Object P0, System.Object P1, System.Double P2)
  RVA=0x0B13F58C  token=0x6000731  System.Void __Gen_Wrap_45(System.Object P0, System.Object P1, System.Double P2, System.Boolean P3)
  RVA=0x0B13F694  token=0x6000732  System.Void __Gen_Wrap_46(System.Object P0, Beyond.Gameplay.Core.AbilitySystem.Modifier& P1)
  RVA=0x066F3838  token=0x6000733  System.Void __Gen_Wrap_47(System.Object P0, Beyond.UI.HeadBarCtrl.HeadBarRefreshReason P1)
  RVA=0x0B13F84C  token=0x6000734  System.Void __Gen_Wrap_48(System.Object P0, Beyond.EventData<System.String,Beyond.Gameplay.Core.Entity>& P1)
  RVA=0x0B13F95C  token=0x6000735  System.Void __Gen_Wrap_49(System.Object P0, Beyond.EventData<System.Boolean,Beyond.Gameplay.DialogEnums.DialogType>& P1)
  RVA=0x0B13FA74  token=0x6000736  System.Void __Gen_Wrap_50(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem>& P1)
  RVA=0x0B13FB98  token=0x6000737  System.Void __Gen_Wrap_51(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystem,System.Boolean>& P1)
  RVA=0x0B13FCBC  token=0x6000738  System.Void __Gen_Wrap_52(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.AbilitySystemForEnemyPart,System.Boolean>& P1)
  RVA=0x0B13FDCC  token=0x6000739  System.String __Gen_Wrap_53(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> P1)
  RVA=0x06360610  token=0x600073A  Beyond.Gameplay.Core.BuffIconConfig.BuffIconStyle __Gen_Wrap_54(System.Object P0, System.Object P1)
  RVA=0x0B13FEEC  token=0x600073B  Beyond.Gameplay.Core.BuffData __Gen_Wrap_55(System.Object P0)
  RVA=0x0B13FFDC  token=0x600073C  System.Void __Gen_Wrap_56(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1)
  RVA=0x066C296C  token=0x600073D  System.Void __Gen_Wrap_57(UnityEngine.Sprite P0)
  RVA=0x0B1400F0  token=0x600073E  System.Void __Gen_Wrap_58(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, System.Object P2, System.Object P3)
  RVA=0x066C44C0  token=0x600073F  System.Void __Gen_Wrap_59(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06360E84  token=0x6000740  System.Int32 __Gen_Wrap_60(System.Object P0)
  RVA=0x067CDAC0  token=0x6000741  System.UInt64 __Gen_Wrap_61(System.Object P0)
  RVA=0x0B14022C  token=0x6000742  Beyond.UI.UIBuffCell __Gen_Wrap_62(System.Object P0, System.Object P1)
  RVA=0x0B140338  token=0x6000743  System.Void __Gen_Wrap_63(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, System.Boolean P2)
  RVA=0x0B140464  token=0x6000744  Beyond.UI.UIPoiseKnot __Gen_Wrap_64(System.Object P0)
  RVA=0x0B140554  token=0x6000745  Beyond.Gameplay.Core.AbilitySystem __Gen_Wrap_65(System.Object P0)
  RVA=0x0B140644  token=0x6000746  System.Void __Gen_Wrap_66(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, System.Object P4)
  RVA=0x0B14075C  token=0x6000747  System.Void __Gen_Wrap_67(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, Beyond.Gameplay.Core.Buff.FinishReason P2)
  RVA=0x0B140888  token=0x6000748  System.Void __Gen_Wrap_68(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.AbilitySystem> P1)
  RVA=0x071BAFA8  token=0x6000749  System.Void __Gen_Wrap_69(System.Object P0, System.Double P1)
  RVA=0x0B0B306C  token=0x600074A  System.Void __Gen_Wrap_70(System.Object P0, System.Object P1, System.Single P2, System.Boolean P3)
  RVA=0x09CA8840  token=0x600074B  System.Void __Gen_Wrap_71(System.Object P0, System.Object P1, System.Boolean P2, System.Int32 P3)
  RVA=0x09CBA2C4  token=0x600074C  System.Void __Gen_Wrap_72(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0B140ABC  token=0x600074D  UnityEngine.Vector3 __Gen_Wrap_73(System.Object P0, System.Object P1)
  RVA=0x0B140BEC  token=0x600074E  UnityEngine.Transform __Gen_Wrap_74(System.Object P0)
  RVA=0x0B140CC8  token=0x600074F  System.String __Gen_Wrap_75(System.Object P0)
  RVA=0x0B140DB8  token=0x6000750  System.Void __Gen_Wrap_76(System.Object P0, UnityEngine.Vector3 P1)
  RVA=0x066C44C0  token=0x6000751  System.Void __Gen_Wrap_77(System.Object P0, Beyond.UI.UIHeadLabel.VisibleSource P1, System.Boolean P2)
  RVA=0x0B140ED4  token=0x6000752  System.Void __Gen_Wrap_78(System.Object P0, Beyond.UI.UIHeadLabel.HeadLabelType P1, Beyond.UI.UIHeadLabel.VisibleSource P2, System.Boolean P3, System.Boolean P4)
  RVA=0x0B140FE8  token=0x6000753  System.Void __Gen_Wrap_79(System.Object P0, Beyond.EventData<System.String>& P1)
  RVA=0x066DCC44  token=0x6000754  System.Void __Gen_Wrap_80(System.Boolean P0)
  RVA=0x0B0B35B4  token=0x6000755  System.Void __Gen_Wrap_81(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3)
  RVA=0x0B1411E8  token=0x6000756  System.String __Gen_Wrap_82(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0B141310  token=0x6000757  UnityEngine.Sprite __Gen_Wrap_83(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0B141438  token=0x6000758  Beyond.Gameplay.Core.BattleManager __Gen_Wrap_84(System.Object P0)
  RVA=0x0B141528  token=0x6000759  Beyond.Gameplay.SkillSetting __Gen_Wrap_85(System.Object P0)
  RVA=0x0B141618  token=0x600075A  System.Void __Gen_Wrap_86(System.Object P0, Beyond.EventData<System.Single>& P1)
  RVA=0x066C48C8  token=0x600075B  System.Void __Gen_Wrap_87(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0B141730  token=0x600075C  UnityEngine.GameObject __Gen_Wrap_88(System.Object P0, System.Object P1)
  RVA=0x0B14183C  token=0x600075D  System.Void __Gen_Wrap_89(System.Object P0, Beyond.EventData<System.Int32>& P1)
  RVA=0x0B141950  token=0x600075E  System.Void __Gen_Wrap_90(System.Object P0, Beyond.EventData<System.UInt64,System.String>& P1)
  RVA=0x0B141A74  token=0x600075F  System.Boolean __Gen_Wrap_91(System.Object P0, Beyond.UI.GPUI.GPUIExtendedSystem& P1, Beyond.UI.GPUI.GPUIHandle& P2)
  RVA=0x07198A94  token=0x6000760  System.Boolean __Gen_Wrap_92(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x07195DFC  token=0x6000761  System.Boolean __Gen_Wrap_93(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0719628C  token=0x6000762  System.Boolean __Gen_Wrap_94(System.Object P0, System.Object P1, System.Single& P2)
  RVA=0x0B141BF4  token=0x6000763  System.Boolean __Gen_Wrap_95(System.Object P0, System.Object P1, System.Object P2, System.Single& P3)
  RVA=0x01367100  token=0x6000764  System.Boolean __Gen_Wrap_96(System.Object P0, System.Object P1)
  RVA=0x014AB310  token=0x6000765  System.Boolean __Gen_Wrap_97(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x012C0160  token=0x6000766  System.Void __Gen_Wrap_98(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x02EC0350  token=0x6000767  System.Boolean __Gen_Wrap_99(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x02E9E650  token=0x6000768  System.Boolean __Gen_Wrap_100(System.Object P0, System.Object P1, UnityEngine.Color P2)
  RVA=0x02E9EDD0  token=0x6000769  System.Void __Gen_Wrap_101(System.Object P0, System.UInt64 P1, System.Object P2, Beyond.Gameplay.Core.Buff.FinishReason P3, System.Object P4, System.Object P5)
  RVA=0x02E9F3B0  token=0x600076A  System.Void __Gen_Wrap_102(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, Beyond.Gameplay.Core.Buff.FinishReason P2, System.Object P3, System.Object P4)
  RVA=0x01B58F50  token=0x600076B  System.Void __Gen_Wrap_103(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x02E9FC40  token=0x600076C  Beyond.UI.GPUIBuffCell __Gen_Wrap_104(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x02EA0760  token=0x600076D  System.Void __Gen_Wrap_105(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> P1, System.Boolean P2, System.Object P3, System.Object P4)
  RVA=0x02EA0FF0  token=0x600076E  System.Void __Gen_Wrap_106(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.GlobalBuff,System.String>& P1)
  RVA=0x02EA1670  token=0x600076F  System.Void __Gen_Wrap_107(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.GlobalBuff,System.String,Beyond.Gameplay.Core.GlobalBuff.FinishReason>& P1)
  RVA=0x02EA1CF0  token=0x6000770  Beyond.Gameplay.GameplayMiscSetting __Gen_Wrap_108(System.Object P0)
  RVA=0x02EA2630  token=0x6000771  Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> __Gen_Wrap_109(System.Object P0)
  RVA=0x012A0D50  token=0x6000772  System.Void __Gen_Wrap_110(Beyond.UI.UISpellInflictionOnCharCell P0, System.Int32 P1)
  RVA=0x02EA2F10  token=0x6000773  Beyond.ObjectPtr<Beyond.Gameplay.Core.Buff> __Gen_Wrap_111(System.Object P0, System.Object P1)
  RVA=0x02EA38F0  token=0x6000774  Beyond.UI.UICharPassiveBase __Gen_Wrap_112(System.Object P0, System.Object P1)
  RVA=0x02EA4320  token=0x6000775  System.Void __Gen_Wrap_113(System.Object P0, Beyond.EventData<Beyond.Gameplay.SetLeaderReason,Beyond.Gameplay.Core.Entity,Beyond.Gameplay.Core.Entity>& P1)
  RVA=0x02EA49A0  token=0x6000776  Beyond.UI.OutOfScreenTargetArrow __Gen_Wrap_114(System.Object P0)
  RVA=0x02EA52E0  token=0x6000777  System.ValueTuple<UnityEngine.Vector2,System.Single> __Gen_Wrap_115(System.Object P0, UnityEngine.Vector3 P1)
  RVA=0x02EA5F80  token=0x6000778  System.Boolean __Gen_Wrap_116(System.Object P0, Beyond.UI.OutOfScreenTargetsCtrlV2.ArrowRuntimeState& P1)
  RVA=0x02EA69B0  token=0x6000779  System.Void __Gen_Wrap_117(System.Object P0, Beyond.EventData<System.UInt64,System.UInt32>& P1)
  RVA=0x02EA7030  token=0x600077A  System.Void __Gen_Wrap_118(System.Object P0, Beyond.EventData<System.UInt64,System.String,System.Int32>& P1)
  RVA=0x02EA76B0  token=0x600077B  System.Void __Gen_Wrap_119(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.Skill>& P1)
  RVA=0x02EA7D30  token=0x600077C  UnityEngine.Sprite __Gen_Wrap_120(System.Object P0, System.Object P1)
  RVA=0x014A7B10  token=0x600077D  System.Void __Gen_Wrap_121(System.Object P0, System.Int32 P1, System.Boolean P2, System.Boolean P3)
  RVA=0x02EA8750  token=0x600077E  System.Boolean __Gen_Wrap_122(System.Object P0, System.Boolean P1)
  RVA=0x02EA8A40  token=0x600077F  System.Void __Gen_Wrap_123(System.Object P0, Beyond.EventData<System.UInt64,System.String,Beyond.GEnums.EnergyShardType>& P1)
  RVA=0x02EA90C0  token=0x6000780  System.Void __Gen_Wrap_124(System.Object P0, Beyond.EventData<Beyond.UI.UIStyleInputValidTypeMask>& P1)
  RVA=0x02EA9750  token=0x6000781  System.Void __Gen_Wrap_125(System.Object P0, Beyond.EventData<Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle>& P1)
  RVA=0x02EA9DD0  token=0x6000782  System.Void __Gen_Wrap_126(System.Object P0, Beyond.EventData<Beyond.Gameplay.ForbidType,System.Boolean>& P1)
  RVA=0x01992E90  token=0x6000783  System.Void __Gen_Wrap_127(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x02EAA460  token=0x6000784  System.Void __Gen_Wrap_128(System.Object P0, System.Int32 P1, System.Int32 P2, System.Byte P3)
  RVA=0x02EAA760  token=0x6000785  System.Void __Gen_Wrap_129(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Byte P5)
  RVA=0x02EAAA90  token=0x6000786  System.Byte __Gen_Wrap_130(System.Object P0, System.Int32 P1, System.Int32 P2, System.Byte& P3)
  RVA=0x012ABC50  token=0x6000787  System.Void __Gen_Wrap_131(System.Object P0, Beyond.Gameplay.CommonTrackingPointStyleType P1, System.Object P2)
  RVA=0x0133CDE0  token=0x6000788  System.Void __Gen_Wrap_132(System.Object P0, Beyond.Gameplay.CommonTrackingPointStyleType P1)
  RVA=0x02EAADB0  token=0x6000789  System.Void __Gen_Wrap_133(System.Object P0, UnityEngine.Vector2 P1, System.Single P2, System.Boolean P3, System.Single P4, System.Boolean P5)
  RVA=0x02EAB4B0  token=0x600078A  System.Void __Gen_Wrap_134(System.Object P0, Beyond.Gameplay.PosValueState P1, System.Boolean P2, UnityEngine.Vector2 P3, System.Single P4, System.Boolean P5, System.Single P6, Beyond.Gameplay.CommonTrackMapTier P7)
  RVA=0x0137AF60  token=0x600078B  System.Int32 __Gen_Wrap_135(System.Object P0, System.Object P1)
  RVA=0x02EABBF0  token=0x600078C  Beyond.UI.CommonTrackerUpdate.CommonTrackerItem __Gen_Wrap_136(System.Object P0)
  RVA=0x023FDBB0  token=0x600078D  System.Boolean __Gen_Wrap_137(System.Object P0, System.Int32 P1, System.Object P2, System.Object P3)
  RVA=0x01921E20  token=0x600078E  System.Void __Gen_Wrap_138(System.Object P0, System.Boolean P1, System.Int32 P2)
  RVA=0x02EAC520  token=0x600078F  System.Void __Gen_Wrap_139(System.Object P0, System.Object P1, System.Object P2, System.UInt64 P3, System.Int32 P4)
  RVA=0x02EAC9A0  token=0x6000790  System.String __Gen_Wrap_140(System.Object P0, Beyond.LangKey P1, System.Object P2)
  RVA=0x012A0D50  token=0x6000791  System.Void __Gen_Wrap_141(System.Object P0, Beyond.UI.CoolerUnitTemperatureCircleUI.ETemperatureState P1)
  RVA=0x026772F0  token=0x6000792  System.Void __Gen_Wrap_142(System.Object P0, System.Single& P1, System.Single& P2)
  RVA=0x02EAD720  token=0x6000793  System.String[] __Gen_Wrap_143()
  RVA=0x023C52D0  token=0x6000794  System.Boolean __Gen_Wrap_144(System.Object P0, System.UInt64 P1)
  RVA=0x02EADF60  token=0x6000795  System.Void __Gen_Wrap_145(System.Object P0, Beyond.EventData<System.String,System.UInt32,System.UInt64>& P1)
  RVA=0x02EAE5E0  token=0x6000796  UnityEngine.GameObject __Gen_Wrap_146(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x02EAF0E0  token=0x6000797  System.Void __Gen_Wrap_147(System.Object P0, System.Object P1, UnityEngine.Color P2)
  RVA=0x013816E0  token=0x6000798  System.UInt32 __Gen_Wrap_148(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x01B51BD0  token=0x6000799  System.Void __Gen_Wrap_149(System.Object P0, System.UInt32 P1, System.Object P2, System.Object P3)
  RVA=0x01D88760  token=0x600079A  System.Void __Gen_Wrap_150(System.Object P0, System.UInt32 P1, System.Object P2, System.Boolean P3, System.Object P4)
  RVA=0x02EAF820  token=0x600079B  System.Void __Gen_Wrap_151(System.Object P0, System.UInt32 P1, UnityEngine.Color P2)
  RVA=0x02EAFE90  token=0x600079C  System.Void __Gen_Wrap_152(System.Object P0, System.Boolean P1, System.Single P2, System.Object P3, System.Object P4)
  RVA=0x02E82400  token=0x600079D  System.Void __Gen_Wrap_153(System.Object P0, System.Single P1, System.Boolean P2)
  RVA=0x012A5970  token=0x600079E  System.Void __Gen_Wrap_154(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x01494100  token=0x600079F  System.Void __Gen_Wrap_155(System.Single P0)
  RVA=0x022A7A00  token=0x60007A0  System.Void __Gen_Wrap_156(System.Object P0, System.Single P1, System.Single P2, System.Single P3, System.Boolean P4)
  RVA=0x02D4D1A0  token=0x60007A1  System.Void __Gen_Wrap_157(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x02EB0320  token=0x60007A2  System.Void __Gen_Wrap_158(System.Object P0, System.Single P1, System.Single P2, System.Object P3)
  RVA=0x02EB06F0  token=0x60007A3  System.Void __Gen_Wrap_159(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3, Beyond.UI.ForgeIronTemperatureCircleUI.ETemperatureState P4)
  RVA=0x02EB0A20  token=0x60007A4  System.Collections.Generic.Dictionary<System.String,Beyond.UI.RegionMap3DCfg> __Gen_Wrap_160(System.Object P0)
  RVA=0x02EB1360  token=0x60007A5  System.Void __Gen_Wrap_161(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, System.Object P3)
  RVA=0x02EB1B70  token=0x60007A6  System.Boolean __Gen_Wrap_162(UnityEngine.Vector2 P0, System.Single P1, UnityEngine.Rect P2)
  RVA=0x02EB24B0  token=0x60007A7  System.Void __Gen_Wrap_163(System.Object P0, Beyond.UI.RegionMapShowType P1, System.Object P2, System.Object P3, System.Single P4)
  RVA=0x02EB2930  token=0x60007A8  UnityEngine.Transform __Gen_Wrap_164(System.Object P0, System.Object P1)
  RVA=0x02EB3350  token=0x60007A9  UnityEngine.Vector3 __Gen_Wrap_165(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x02EB3EE0  token=0x60007AA  Beyond.UI.RegionMap3DCfg __Gen_Wrap_166(System.Object P0, System.Object P1)
  RVA=0x02EB4900  token=0x60007AB  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> __Gen_Wrap_167(System.Object P0, System.Single P1, System.Object P2)
  RVA=0x02EB5340  token=0x60007AC  UnityEngine.MaterialPropertyBlock __Gen_Wrap_168()
  RVA=0x02EB5B80  token=0x60007AD  System.Void __Gen_Wrap_169(System.Object P0, Beyond.EventData<System.Int64,System.Int64>& P1)
  RVA=0x02EB6200  token=0x60007AE  System.Boolean __Gen_Wrap_170()
  RVA=0x02EB63E0  token=0x60007AF  Beyond.Gameplay.MapStaticElementConditionExpression __Gen_Wrap_171(System.Object P0)
  RVA=0x02EB6D20  token=0x60007B0  System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> __Gen_Wrap_172(System.Object P0)
  RVA=0x02EB7650  token=0x60007B1  Beyond.UI.LuaPanel __Gen_Wrap_173(System.Object P0)
  RVA=0x01DFDB20  token=0x60007B2  System.Void __Gen_Wrap_174(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4)
  RVA=0x02EB7F80  token=0x60007B3  UnityEngine.Rect __Gen_Wrap_175(System.Single P0, System.Single P1, System.Single P2, System.Single P3)
  RVA=0x02EB8830  token=0x60007B4  System.Collections.Generic.List<UnityEngine.Vector2> __Gen_Wrap_176(System.Object P0, UnityEngine.Vector2 P1, UnityEngine.Vector2 P2, System.Single P3)
  RVA=0x02EB9600  token=0x60007B5  UnityEngine.Vector2 __Gen_Wrap_177(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x02EBA260  token=0x60007B6  UnityEngine.Vector2 __Gen_Wrap_178(System.Object P0, UnityEngine.Vector2 P1, System.Object P2, System.Object P3)
  RVA=0x02EBB050  token=0x60007B7  UnityEngine.UI.Graphic __Gen_Wrap_179(System.Object P0)
  RVA=0x02EBB980  token=0x60007B8  System.String __Gen_Wrap_180(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4)
  RVA=0x02EBC360  token=0x60007B9  System.String __Gen_Wrap_181(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0253AE10  token=0x60007BA  System.Void __Gen_Wrap_182(System.Object P0, System.Object P1, System.Int64 P2, System.Single P3)
  RVA=0x02EBCCD0  token=0x60007BB  System.Void __Gen_Wrap_183(System.Object P0, UnityEngine.Vector3 P1, System.UInt64 P2)
  RVA=0x01B51BD0  token=0x60007BC  System.Void __Gen_Wrap_184(System.Object P0, System.Int32 P1, System.Object P2, System.Object P3)
  RVA=0x012918C0  token=0x60007BD  System.Void __Gen_Wrap_185(System.Object P0, System.Boolean P1, System.Object P2)
  RVA=0x012F70E0  token=0x60007BE  System.Void __Gen_Wrap_186(System.Object P0, System.Int64 P1)
  RVA=0x01992E90  token=0x60007BF  System.Void __Gen_Wrap_187(System.Object P0, System.Int32 P1, System.UInt32 P2)
  RVA=0x01A13EB0  token=0x60007C0  System.Void __Gen_Wrap_188(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3)
  RVA=0x02EBD360  token=0x60007C1  System.Void __Gen_Wrap_189(System.Object P0, System.Int64 P1, System.Single P2)
  RVA=0x02EBD650  token=0x60007C2  System.Void __Gen_Wrap_190(System.Object P0, UnityEngine.Vector2 P1, System.Single P2, System.Boolean P3)
  RVA=0x02EBDD10  token=0x60007C3  System.Void __Gen_Wrap_191(System.Object P0, System.UInt64 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4)
  RVA=0x012A0D50  token=0x60007C4  System.Void __Gen_Wrap_192(System.Object P0, Beyond.Gameplay.Factory.PowerAutoConnectStatus P1)
  RVA=0x01298AE0  token=0x60007C5  Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleIcon __Gen_Wrap_193(System.Object P0)
  RVA=0x02EBE030  token=0x60007C6  UnityEngine.GameObject __Gen_Wrap_194(System.Object P0)
  RVA=0x012A0D50  token=0x60007C7  System.Void __Gen_Wrap_195(System.Object P0, Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleIcon P1)
  RVA=0x01298AE0  token=0x60007C8  Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleStatus __Gen_Wrap_196(System.Object P0)
  RVA=0x012A0D50  token=0x60007C9  System.Void __Gen_Wrap_197(System.Object P0, Beyond.Gameplay.Core.GameMech.TravelPoleBrain.TravelPoleStatus P1)
  RVA=0x02EBE960  token=0x60007CA  System.Void __Gen_Wrap_198(System.Object P0, Beyond.EventData<System.Int32,System.Single>& P1)
  RVA=0x02EBEFE0  token=0x60007CB  System.Void __Gen_Wrap_199(System.Object P0, Beyond.EventData<Beyond.Gameplay.WaterDroneSourceType>& P1)
  RVA=0x012A0D50  token=0x60007CC  System.Void __Gen_Wrap_200(System.Object P0, Beyond.Gameplay.CustomAbilityComponent.WaterDroneByHydrantState P1)
  RVA=0x02EBF660  token=0x60007CD  System.Void __Gen_Wrap_201(System.Object P0, Beyond.EventData<Beyond.Gameplay.CustomAbilityComponent.WaterDroneByHydrantState>& P1)
  RVA=0x02EBFCE0  token=0x60007CE  System.Void __Gen_Wrap_202(System.Object P0, Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity> P1)
  RVA=0x04D37640  token=0x60007CF  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x20000E7
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B147670  token=0x60007D0  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x60007D1  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x20000E8
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60007D2  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B1478EC  token=0x60007D3  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02F45C30  token=0x60007D4  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B1477A8  token=0x60007D5  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B147874  token=0x60007D6  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B147950  token=0x60007D7  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B147704  token=0x60007D8  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x20000E9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-PoolCore-ListPoolInitializer_UI_Gameplay_Beyond-Initialize0  // const
  public    static  IFix.IDMAP0                     Beyond-PoolCore-ListPoolInitializer_UI_Gameplay_Beyond-InitializeAtRuntime0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-get_listenBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-OnBound0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-OnBeforeUnbind0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-Unbind0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-Bind0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveBase-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveCounter-OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-get_listenBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_IsBuffValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_GetActiveBuffForDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_SetProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_SetTickRunning0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_UpdateDecoThresholds0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_CheckLoopThreshold0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_OnLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_ForceIdle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_TryTrackBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_InitDecoThresholds0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-_RefreshDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-OnBound0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-OnBeforeUnbind0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLiino-OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveLizhiyan-OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveMultiStates-OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICharPassiveZhuangfy-OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_CacheHideCallbacks0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_SelectArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_SelectHalf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_GetRandomPointInArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_GetNextAvailableBig0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_PlayEffectAt0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_GetNextAvailableSmall0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-PlayEffect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-_ClearAnims0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-LiinoRandomLikeEffect-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeBase-OnAssignData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeBase-OnReady0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeBase-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeBase-OnCast0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeBase-OnRemove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-get_needTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-_OnComboSkillRingQte0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-OnAssignData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-_Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-OnCast0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-OnRemove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSpecialNodeRingQte-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-get_outOfScreenPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_InstantiateTextInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_RecycleTextInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_CreateTextInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetText1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetFollowTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-get_uiOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetUIOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-get_isFollowingTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-get_worldPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextDamageBase-PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_GetRandomTextOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_IsTextPosValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnHpChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextIgniteBuff-SetUIState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextIgniteBuff-SetTextAndDuplication0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnIgniteBuffText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-SetText2  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnImmuneText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnPhysicalInflictionApplied0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnSquadInFightChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnCreateFlowText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-_OnCharLevelUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-DebugChangeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrl-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-get_outOfScreenPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-Assign0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_CreateTextInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_showDuration0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetText1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_textNodeAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetFollowTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_isFollowingTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_worldPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_uiOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetUIOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_UseWorldSpaceRandomArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_GetRandomTextOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_IsTextPosValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-DestroyDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnHpChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-SetText2  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnIgniteBuffText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnImmuneText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnPhysicalInflictionApplied0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnSquadInFightChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnCreateFlowText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_RecycleTextInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-_OnCharLevelUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-DebugChangeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-_UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-get_isActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCtrlV2-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-_UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBase-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBaseV2-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextBlock-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCriticalLevel0-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCriticalLevel1-SetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextCriticalLevel1-PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextIgniteBuff-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextIgniteBuff-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DamageTextNormalLevel1-PlayAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FootBarDashCell-SetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FootBarDashCell-SetDashCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FootBarDashCell-StartPlayNotEnoughAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FootBarDashCell-StopPlayNotEnoughAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-get_forceHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_SetInDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_GetHeadBarType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-get_actualVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_TryRefreshHeightFollowTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_TryRefreshFollowTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-TryGetOriginalFollowPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_CalculatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-OnActualVisibleChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_SetDistanceVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-get_needUpdateTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_UpdateScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-_UpdateSelfTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_SetImageFill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_CalculateHpChangeDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnHpChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-ShowModifierAfterSetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-ShowModifierAfterSetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-OnAfterHpBroken0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-OnAfterHpBroken0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-OnBeforeHpBroken0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-OnBeforeHpBroken0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_RefreshHeadBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnInteractNpc0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnDialogStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnDialogFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnAddHeadBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnRemoveHeadBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnHeadBarShowConfigChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnHpChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnEnemyPartEnableChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_FindBountyBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBuff-SetBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_RefreshHeadBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_OnBountyEnemyChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_OnFirstInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-OnPreWarm0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-OnPreWarm0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-OnPreWarm0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_PrewarmHeadBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-BuffCellGroup-RemoveBuffCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-BuffCellGroup-ClearAllBuffCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_IsBuffIconInThisNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_GetIconStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-get_anim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-SetBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-get_buffData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-SetStackElementActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-SetWarningState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-VisualInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-DataInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-SetStackCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-get_orderPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-get_buffInstanceUid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_DealWithBuffCellVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_SetBuffCellSiblingInOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-BuffCellGroup-CreateBuffCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_AddBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_UpdateSurroundingEnemies0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-SetHeadBarVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_UpdateHeadBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_UpdateLevelInfoBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_UpdateVigilanceBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITacticalItemBar-IsTacticalItemActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-_UpdateTacticalItemBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HeadBarCtrl-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseKnot-SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-_ClearPoiseKnot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-_CreatePoiseKnot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-_TryInitPoiseKnot0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-get_owner0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-get_realPoise0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-get_maxPoise0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-get_hidePoiseBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_OnAddBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-BeforePlayOutAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_RemoveBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_OnRemoveBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_OnBuffEnhanceChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_UnregisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-_RegisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnForceSetHp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseKnot-KnotBreak0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-_CalculatePoiseChangeDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-OnPoiseChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnPoiseChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeaknessNode-SetTotalCellNum0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnMultiTimeWeaknessActivate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeaknessNode-SetCurrentCellNum0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnWeaknessCountChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_OnMainTargetChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_UpdateHpDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-_UpdateScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-_UpdatePoiseDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPoiseBar-UpdatePoiseBar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBar-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-get_actualVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-_UpdateGuideLine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-GetAdjustedHeightOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBarFollower-TryGetOriginalFollowPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadBuff-TryGetOriginalFollowPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-get_iconHolder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_OnSubRootsCollectionChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-get_headLabelIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-SetVisibleDirty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-set_headLabelIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-get_missionHeadLabelIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-set_missionHeadLabelIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-HeadLabelRootVisibleData-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_TryInitRootData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-HeadLabelRootVisibleData-get_realVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_UpdateVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-HeadLabelRootVisibleData-UpdateVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-SetSubRootVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_OnNpcVisibleChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_BindTargetEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_UpdateFloatingUIVisibleCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_TryUpdateVisibleCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_UpdatePos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_OnMissionTypeIconChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_BindMissionTracking0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_ClearFloatingUIVisibleCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_ClearVisibleCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_RefreshVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-ClearEmojis0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_UnBindMissionTracking0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_UnBindTargetEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-SetActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-_TryUpdateVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHeadLabel-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILvInfoBar-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILvInfoBar-TryGetOriginalFollowPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-GetSpritePath0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-LoadSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITacticalItemBar-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITacticalItemBar-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UITacticalItemBar-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-get_actualVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-get_needUpdateTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-_SetVigilanceActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-_UpdateVigilanceNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVigilanceBar-TryGetOriginalFollowPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeaknessNode-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-get_battleManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-get_skillSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-_OnAtbCellOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-CheckAtbLoopAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-_RefreshAtbShowing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-OnInFightChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-OnGainAtb0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-AtbNode-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_ResortSiblingIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-GetControllerIndicatorState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-OnCast0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_CheckUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-CastComboSkill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-OnReady0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-OnRemove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-LoadGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillHint-AssignData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_OnComboSkillReady0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_RemoveComboSkill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_OnComboSkillRemove0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_OnBattleTeamChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_OnComboSkillClearAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_OnActiveSkillDataRefreshed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-_UnbindListeners0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ComboSkillPanel-PreTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-get_defaultBuffIconNodeName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-DataInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-DataInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_UpdateAnimationComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_canUpdateVisual0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_TryResolveGPUIProxy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TryEnsureGPUIInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_TryResolveGpui0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetImageFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-BeforePlayOutAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-BeforePlayOutAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetStackCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-ClearAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TryStartRootAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-PlayWithAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TryStartSubrootAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-_PlayConsumeAnimations0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-SetStackCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_defaultBuffIconNodeName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_ResolveNodeName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_LogSetBuffIconFailure0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-ClearBuffIconOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-SetBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-ClearBuffIconOverride0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetNodeVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetNodeVisibility1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetStackElementActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetNodeRotZ0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIAttachedBuffCell-SetStackElementActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_isVisualInited0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_buffData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_orderPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-get_buffInstanceUid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetSyncManager0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_TryCreateGPUIInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetWarningState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_TrySetRootScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-VisualInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-_TryDestroyGpuiInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SetBuffIconOverrideActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-PlayInAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-PlayOutAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-PlayStrongInAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-PlayIgniteOutAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-SampleToInAnimationEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffCell-TrySetColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_OnFirstInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-OnPreWarm0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_IsBuffIconInThisNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_OnAddBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_GetIconStyle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-BuffCellGroup-RemoveBuffCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_DealWithBuffCellVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_RemoveSingleBuffCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_RemoveStackBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_RemoveBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_OnRemoveBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_OnBuffEnhanceChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_UnregisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_RegisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-BuffCellGroup-ClearAllBuffCells0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_SetBuffCellSiblingInOrder0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_IsBuffCellVisibleByLimit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_PlayInAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-BuffCellGroup-CreateBuffCell0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_AddStackBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-_AddBuffIconInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUIBuffNode-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUILifeTimeBuffCell-_UpdateFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUILifeTimeBuffCell-VisualInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUILifeTimeBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GPUILifeTimeBuffCell-SetWarningState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-_AnimOutCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-_AnimInCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-_RefreshComboCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-_OnGlobalBuffStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-_OnGlobalBuffFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-HpBarComboNode-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_SetHpFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_CalculateHpChangeDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeBase-get_globalNeedShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-get_needShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-get_setting0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-_CheckNeedShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-SetLocalNeedShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-InformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnHpChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnTakeDamage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_BlinkBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_IsCharHpBarVfxBuffValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_HpVfxOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_RefreshBuffChangeCharHpBarVfx0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_GetLatestValidCharHpBarVfxBuffPtrFromCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnBuffEnhanceChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_ClearBuffNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_ClearBuffNode1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharCell-SetCellActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-get_isInCcDungeon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_OnBuffEnhanceChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_UnregisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-OnClear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_ClearMainChar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_RebuildCharHpBarVfxBuffCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_EnsureBuffNodePreWarmed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_InitCurrentBuffNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_OnFirstInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_RegisterAbilityBuffEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-_DoRefreshUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_IsUltimateBuffValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_GetUltimateBuffForAbility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_UltimateBuffProgressOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_SetUltimateBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_SelectUltimateBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_GetOrCreateCharPassiveBase0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_ProcessOnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnProgressBarBuffChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_RebuildUltimateBuffTracking0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnTeamChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_OnMainCharHpActiveChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-SetGPUIBuffSync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-SetGPUIBuffMode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_CheckHpBarShowing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_SetHpBarShowing0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-_UpdateHpDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISpellInflictionOnCharNode-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-MainCharHpBar-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-_OnEnemyCastSkillToMainChar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-DebugChangeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-_InitArrowCallbacks0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-_CreateOneArrow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-_ScreenPosToUIPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrl-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_OnEnemyCastSkillToMainChar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_RecycleAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-OnClose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-OnHide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-DebugChangeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_UpdateRuntimeStates0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_ScheduleDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_CreateArrowState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_RestartVisibleState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-_ScreenPosToUIPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-OutOfScreenTargetsCtrlV2-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-get_skillSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandler-OnOtherPressSkillButtonStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnOtherPressSkillButtonStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnUspChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_RefreshNormalProgressBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_RefreshUltimateProgressBuff0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_InitSkillData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnPressCancelled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_RefreshDeadState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_CheckNormalSkillHighlight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_RefreshNormalSkillCd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_RefreshUltimateSkillCd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_UpdateMainUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_SetPressFillActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_SetGoActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnSkillButtonActiveConfigChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-RefreshSkillButton0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnCharPotentialUnlock0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnCharSkillChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandler-OnUltimateSkillChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnCharUltimateSkillChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnSkillUpgradeSuccess0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnActiveSkillDataRefreshed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnSkillCdChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-LoadSpriteWithOutFormat0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_UpdateLongPressKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnInputActionKeyUpdated0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_OnKeyboardTypeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnPressSkillStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnPressSkillEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnPressUltimateSkillStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-FirstTimeInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-OnCharacterDie0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-_SetPressFillAmount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-CastSkill0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-ToggleControllerSkillIndicator0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-SetEmpty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-ClearBtnClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandler-get_skillSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandler-get_needUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-get_needUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnPressStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnPressEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnPressCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnOtherPressSkillButtonStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerLongPress-OnUltimateSkillChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerIndividualUltimate-get_needUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerIndividualUltimate-OnPressStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerIndividualUltimate-OnUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerIndividualUltimate-OnPressEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerIndividualUltimate-OnPressCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-get_needUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-OnPressStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-OnUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-OnPressEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-OnPressCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SkillButton-PressHandlerDrag-OnUltimateSkillChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-SetGPUIBuffSync0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_ClearBuffNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_EnsureBuffNodePreWarmed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_InitCurrentBuffNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-SetGPUIBuffMode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-CanSwitchToCenter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-OnPressCharIconStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_OnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_OnIgniteBuffText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-FirstTimeInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_OnHpChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_IsCharHpBarVfxBuffValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_HpVfxOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_RefreshBuffChangeCharHpBarVfx0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_GetLatestValidCharHpBarVfxBuffPtrFromCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_OnBuffIconChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_OnCharPassiveNumChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-SetDeadState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_RebuildCharHpBarVfxBuffCache0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_UpdateComboSkillIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_TryShowLevelUpAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_SetLevelText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TacticalDotCell-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-OnTacticalItemChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-InitSquadIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_MustUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_UpdateIconAlpha0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TacticalDotCell-SetDotActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-TacticalDotCell-SetDotUsedUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-_UpdateTacticalItemStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-SetEmpty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-ToggleIndicator0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-InformKeyHint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-OnLevelChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SquadIcon-OnTeamSwitchUnlocked0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbnormalBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbnormalBuffCell-VisualInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbnormalBuffCell-SetWarningState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-DataInit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-BeforePlayOutAnim0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-SetBuffIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-SetStackElementActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-SetStackCount0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAttachedBuffCell-OnBuffIgnite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBuffNode-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILifeTimeBuffCell-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILifeTimeBuffCell-SetWarningState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-_CheckNeedShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-_OnFadeHud0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-InformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-_OnInformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeConsoleController-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-_OnFadeHud0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-_OnInformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeController-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-_CheckNeedShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-_OnFadeHud0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-InformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-_OnInformShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIHudFadeTouchSkill-OnShow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-_RefreshUltimateMode2Available0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-_RefreshCachedKeys0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-_OnInputActionKeyUpdated0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-_UnregisterAllCombos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-DestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-CheckNormalAttackBtn0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-TryPressControllerIndicatorWhenSkillButtonJustPressed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BattleControllerInputController-_RegisterAllCombos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIAbilityTargetFollower-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-_RefreshTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-_OnLockTargetFromAutoToManual0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILockAim-UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnDashFailed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_ProcessOnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_SetInDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnInteractNpc0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnDialogStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnDialogFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnDashCountChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-_OnOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBar-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnOutAnimCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnDashFailed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_ProcessOnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnMainCharChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_RefreshDisableState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_SetInDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnInteractNpc0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnDialogStart0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnDialogFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnPlayCutscene0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnFinishCutscene0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnForbidSystemChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-_OnDashCountChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMainCharFootBarArc-SetUIDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeakLockAim-_RefreshTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeakLockAim-_OnMainTargetChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeakLockAim-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeakLockAim-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWeakLockAim-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BlueprintPreviewHelper-SetSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BlueprintPreviewHelper-AddGridValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BlueprintPreviewHelper-BatchAddGridValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-BlueprintPreviewHelper-GetGridValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICampfireTracker-UpdateImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonTracker-UpdateImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerNodeComp-ShowStyleType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerNodeComp-PlayOutAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBlackboxTracker-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICampfireTracker-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonLevelTracker-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonTracker-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerNodeComp-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-get__inFactoryTopView0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_FindTrackIndexByKey0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_CreateNewMissionTracker0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_InitTrackInfoByIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_HideTrackerGo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_DeleteInvalidTracker0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-_ShowTrackerGo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-UpdateCommonTrackers0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CommonTrackerUpdate-AllTrackersPlayOutAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-TryGetNeedComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-_TickCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-_UpdateProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitProgressBarUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-_TickCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-_GetLangKeyOrDefault0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-_SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-_UpdateUIState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureCircleUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-set_uiActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-_TickCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-NormalizeRange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-_UpdateUIInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-_UpdateUIPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTemperatureUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_GeneratePercentStrings0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_ResetUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_GetCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_TryGetModule0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_TickCanvasGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_SetProgressText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-_UpdateProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-CoolerUnitTotalProgressBarUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-RefreshUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-_OnDomainDevelopmentChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-_BindDomainChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-_UnbindDomainChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-DoRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-DoRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-FaceToCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasCoreScanUI-RefreshUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-DoRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-DoodadFactoryGasScanUI-InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineCell-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineCell-PlayLineAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineCell-PlayLineDecoAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineCell-ChangeLineColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineCell-ChangeLineWidth0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-_CloneLineObject0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-_RefreshCopyLineObjectColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-DrawLine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-PlayLineAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-PlayPortLineDecoAnimation0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-ChangeLineColor0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacLineDrawer-ClearDrawer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacRegionUpgradeEffectController-_RefreshNodeState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacRegionUpgradeEffectController-SetUpgradeState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacRegionUpgradeEffectController-SetSelectState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-FacRegionUpgradeEffectController-SetVisibleState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_PosAdjust0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_UpdatePositionOffsetAfterZoomed0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_RefreshPivotPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_SetScale0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_OnZoom0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_ControllerZoomUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_ControllerDragUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_ControllerUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_RefreshAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-InitData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-MoveNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-_OnUnlockMoveNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-FocusNode0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-OnUnlockTech0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-ZoomToFullTechTree0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacTechTreePanel-SetDetailShownState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-UpdateProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronProgressBarUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-_SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-UpdateUIState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureCircleUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-UpdateUIInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-UpdateUIPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ForgeIronTemperatureUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-ForceToggleTargetBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-_OnTriggerPlayerAction0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-CopyTargetBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-ClearBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-_ClearOnClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-ClearTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-GuideFakeButton-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMap3DCfg-Load0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-get_cfg0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_ClearMaterials0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMapTrackIcon-InitData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_SetMapPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_InitTrackIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_InitPlayerIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_InitSelect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-_RefreshMaterials0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-MoveToPlayer0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-IsCircleIntersectingRectangle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-InitData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-GetNodeTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-GetNodeMoveTargetLocalPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-GetCfg0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-SetLoadedRegionTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-StartRegionRectTransformMoveRotationTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-get_m_materialPropertyBlock0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RegionMapSetting-SetControllerLevelSelectedState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacMainRegion-_RefreshRegionState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacMainRegion-_OnMainRegionLevelChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacMainRegion-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacMainRegion-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIFacMainRegion-InitMainRegion0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_FindMatchingPhaseIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_FindMatchingPhaseIndex1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_UnloadImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_LoadImage0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_ReevaluateAndApply0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_OnAnyConditionChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_RegisterPhaseConditions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-_RegisterPhaseConditions1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-InitCommonStaticElement0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCommonStaticElement-EvaluateInitialVisibility0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-_ClearSpriteAsset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-_RefreshCraneState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-_OnCraneStateChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-_UnBindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-_BindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapCrane-InitCrane0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapGameplayArea-_RefreshGameplayArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapGameplayArea-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapGameplayArea-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-_LimitInRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-StartLimitMarkInRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-StopLimitMarkInRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLimitInRect-ForceRefreshLimitState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-_HoldLineSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapLine-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-get_coexistWithMissionTrack0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-get_notNeedCheckCoexistBecauseOfRadius0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-get_needCheckMissionTrackCoexist0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-_IsCircleOutsideDisappearRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-_DisappearIfOutRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-_RefreshMissionArea0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMissionArea-ForceRefreshDisappearIfOutRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-IsMistyVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-_RefreshMistyState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-_OnMistyStateChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-_UnBindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-_BindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapMisty-InitMisty0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-_MergeAllConditionExpressions0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-_ReevaluateAndApply0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-_OnAnyConditionChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-InitNarrativeAreaText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapNarrativeAreaText-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapSwitchBtn-InitSwitchButton0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapSwitchBtn-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-_IsFirstRaycastSelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-_CheckDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-_CheckScroll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILevelMapTouchPanelWrapper-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-_GetNodeTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-_GetMoveTargetLocalPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-_OnDoMoveTween0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-InitPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-OnClickLevelBtn0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRegionMap3DPanel-OnLevelHoverChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-_RefreshSpriteWithTier0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-_RefreshSettlementRegionState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-_OnSettlementUnlocked0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-_UnBindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-_BindAll0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-InitSettlementRegion0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-GetNeedRefreshSettlementRegionTier0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UISettlementRegion-RefreshSettlementRegionWithTier0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-_CheckEventValid0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-OnDrop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ChessboardDrop-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnBeginDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnEndDrag0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnPointerDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnPointerUp0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnPointerClick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnPointerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleCellDrag-OnPointerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleControllerHelper-get_raycastResults0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleControllerHelper-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-get_luaIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleControllerHelper-TryGetPuzzleSlotIndexByPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-get_panel0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-PuzzleDrag-SetIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_DrawBlank0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_InitPixelDic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_InitMarkPoint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_MarkScratchPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_AwardMarkScratchPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_ClampDrawRect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-Draw0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-OutputCompletePercent0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-FlagCompleteMarkPoint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_FigureUpCreateRouteLinePoints0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_ComputeBrushUV0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-_ScreenPosToUiLocalPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-ApplyScratch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-BeginScratch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-ScratchOffLotteryCoating-EndScratch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-get_graphic0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIMusicVolumeVisual-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-DoRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-SetUIForward0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-SetPos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-_GetStateName0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-_GetTypeDesText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-_GetTypePercentText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-_UpdateUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-RiftScanUI-RefreshUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBrokenLine-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBrokenLine-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBrokenLine-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBrokenLine-SetYValue0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIBrokenLine-SetYValueByArray0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-get_uiActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-set_uiActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-_UpdatePos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-SetFixPoint0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-_FaceToCamera0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICommonScanController-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-get_update0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-set_update0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-_UpdatePos0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-_UpdateData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-InitData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UICountDownBar-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadMineCoreScanController-SetPurity0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadMineCoreScanController-SyncConditonInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadMineCoreScanController-UpdateUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-_SyncTimeText0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-SetCoreInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-SetRefreshInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-RefreshTextNow0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-SetSingle0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadPlantCoreScanController-SetQuantityInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadScanController-SyncRefreshInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadScanController-_RefreshCountDown0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadScanController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIDoodadScanController-UpdateUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGeneralTracker-get_rect0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIGeneralTracker-UpdatePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UILiquidPoolScanController-SetWaterInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPanelBase-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateDistance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-SetDistanceTextVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-IconGroup-Switch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-StatusHintGroup-Switch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-_ActualUpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateStatusNormal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateStatusTooFar0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-_ActualUdpipeError0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateUdpipeError0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-IconGroup-SetActiveIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-_ActualUpdateIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-IconGroup-get_activeSelf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleAutoConnectTracker-UpdateNodeType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_isWarning0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_isDisabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_allowToHighlight0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_iconStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageNormal0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageNormalOther0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageWarning0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageHighlighted0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageHighlightedOther0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_imageDisabled0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-_UpdateIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-set_iconStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_status0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-_OnStatusChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-set_status0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-get_animationWrapper0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-PlayFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-UpdateIconStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-UpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-UpdateIsHighlighted0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-UpdateIsOtherSocial0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-UpdateDistance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIPowerPoleFastTravelTracker-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-_ResetData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-_ShowBaseTrans0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-_LoadAllSprite0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-_UpdateRaceModuleRank0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIRaceModuleRankController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-UpdateDistance0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-IconGroup-Switch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-StatusHintGroup-Switch0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-_ActualUpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-UpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-IconGroup-SetActiveIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-_ActualUpdateIcon0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-IconGroup-get_activeSelf0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIUdpipeConnectTracker-UpdateNodeType0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVideoHelper-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVideoHelper-ResetVideo0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVideoHelper-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIVideoHelper-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneInfinityLiquidChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneEmptyChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneInteractiveChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneSourceTypeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneRelaxChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneHydrantStateChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneHydrantStateChanged1  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-OnAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_ClearWaterDrone0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_OnWaterDroneChange0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_SetIconStateController0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-_UpdateSlider0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-UIWaterDroneBar-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-get_uiActive0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-TickUI0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-WorldUIController-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Mission-MissionHudBGSizeUpdater-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Mission-MissionHudBGSizeUpdater-UpdateBgSize0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-Mission-MissionHudBGSizeUpdater-LateTick0  // const
METHODS:
END_CLASS

