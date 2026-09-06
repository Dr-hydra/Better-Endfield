# Combat Runtime Contracts

本文记录战斗数据模块（`BetterEndfield.CombatStats.dll`）下一阶段实现所依赖的游戏内数据结构与获取链，
用于 rDPS 增伤归属、角色装备/武器/技能/潜能快照、关卡元数据和分类维度。

字段偏移来自 `tools/EndfieldDumper/IL2CPP_Dump_Normal/Gameplay.Beyond.dll.cs`（游戏版本 1.4.4 的 dump），
仅作为当前版本的参考值；运行时仍通过 `il2cpp_field_get_offset` 按字段名解析，不硬编码地址，
也不依赖 `GameAssembly.dll` 哈希。B 服/官服只要保留下述类名与字段契约即可复用。

## 原则

- 只读游戏已经算好、存好的字段，不重算公式。
- 伤害与 buff 走公开的 `BattleRecorder` 入口，不依赖受 `enableRecord` 开关控制的 detail 链路。
- 角色快照走 `GamePlayer` 体系，独立于 `BattleRecorder`。
- 分类维度与显示名分别来自游戏自己的 `DamageScaleProcessorConfig` 与 TableCfg 本地化表，不维护自造映射。

## 入口总览

| 数据 | 权威入口 | 依赖 detail 开关 |
|---|---|---|
| 伤害事件 | `BattleRecorder.RecordDamage(ref Modifier)` | 否 |
| buff 生命周期 | `BattleRecorder.RecordBuffStart` / `RecordBuffFinish` | 否 |
| 技能施放开始 | `Skill.DoCast(onCastEnd, onExclusive, options, skipApplyCost)`;施法者取 `Skill.get_owner()`。`BattleRecorder.RecordCastSkill` 自 2026-09-03 客户端起被整体内联进 `DoCast`(唯一调用方),仅作回退 | 否 |
| 技能施放结束 | `Skill.CastEnd(finishType, interruptReason, interruptContext)`;`RecordEndSkill` 无调用方 | 否 |
| 出战角色列表 | `GamePlayer.squadManager.curSquad.slots` | 否 |
| 角色快照 | `GamePlayer.charBag.GetCharInfo(charInstId)` | 否 |
| 关卡 / 时长 | `BattleInfo.dungeonId` + 模块自身 session 计时 | 否 |
| 分类 zone | `DataManager.damageScaleProcessorConfig.allZones` | 否 |
| 中文名 | TableCfg `StrIdNumTable` + `I18nTextTable_CN` | 构建期生成 |

## 管理器获取

顶层单例：

```text
GameInstance.instance  静态 @0x0   (property getter)
  ├─ m_player 0x18   → GamePlayer
  └─ m_world  0x10   → GameWorld
```

静态引用兜底（可直接读静态字段，避免走完整链）：

- `GameInstance.dataManager` 静态 @0xa0 → `DataManager`
- `GameWorld.battle`       静态 @0x18 → `BattleManager`

`GamePlayer` 关键公开字段：

```text
squadManager    0x48   → SquadManager
charBag         0x60   → CharBagSystem
dungeonManager  0x1b8  → DungeonManager
inventory       0x70   → InventorySystem
```

## 伤害与 rDPS 归属

### 伤害落账字段

`BattleRecorder.DamageDetail`（`size 0x90`）：

```text
attackerId        0x18
defenderId        0x20
damageValue       0x28  最终伤害
hpDelta           0x30  实际血量变化
hpAfterDamage     0x38
damageType        0x40
damageDecorateMask 0x48
environmentId     0x50
serverActionId    0x58
damageUnitIndex   0x5c
originSkillId     0x60  来源技能
isCritical        0x68
atkScale          0x70
calcResult        0x78  公式最终结果
m_recordDamageScaleZones 0x80
```

### 乘区载体

`DamagePackData`（2026-09-03 客户端 `size 0x100`；旧版 0x110）：

```text
attacker                0x18  ObjectPtr<AbilitySystem>
defender                0x28  ObjectPtr<AbilitySystem>
actionEnvironment       0x38  IActionEnvironment（替代旧版 skillCastInfo）
calcResult              0x40  CalcResult.value
damageType              0x48
damageDecorateMask      0x50
atkScale                0x78
attackerDamageScaleZones 0x80  double[]
attackerAttributes      0x88  double[]
defenderDamageScaleZones 0x90  double[]
defenderAttributes      0x98  double[]
isBlocked               0xa0
useHpChangeAsDisplayValue 0xa1
transferredSource       0xa8
```

来源技能不再是内嵌字段，而由 `actionEnvironment.FillSkillCastInfo()` 派生。运行时按对象实际类分派：
`Ability` → `castOriginSkill 0x60`；`Buff` → 自身 `skillCastInfo 0x100`；`GlobalBuff` /
`ComboSkillEnvironment` / `SkillHighlightEnvironment` 走接口默认实现，返回空。仍带 `skillCastInfo`
字段的客户端继续走旧路径。

方法与属性：

- `ModifyDamageScaleZone(side, zoneName, value)`：每个增伤处理器写乘区。
- `GetFinalAttackValue()`、`_GetFinalDamageScale()`、`_GetDamageScale(Zone)`。
- `defenderPoiseFactor`、`serverDetail`（`Proto.BATTLE_DAMAGE_DETAIL`）。

### 归属链

```text
ModifyDamageScaleZone(side, zoneName, value)
  ← DamageScaleProcessor   side 0x10, zoneName 0x18, addition 0x20 (BlackboardDouble)
    ← DamageModifier       m_data 0x18, m_owner 0x20
      ← Buff.m_damageModifiers 0x58
        → Buff.source        0x180  来源角色（rDPS 归账对象）
        → Buff.owner         0x170  目标角色
        → Buff.skillCastInfo 0x100 → SkillCastInfo.originSkill 0x18  来源技能
        → Buff.m_data        0x18   BuffData（装备/武器来源，间接）
```

`SkillCastInfo`：`skillCastId 0x10`、`originSkill 0x18`。

`ModifyDamageScaleZone` 本身不带来源，落地时需在 buff 创建/`Reset` 时维护
`buff 实例 → {来源角色, 来源技能, 装备/武器}` 映射，写值发生时反查。

### 归属三层

- 角色：`Buff.source`。
- 技能：`Buff.skillCastInfo.originSkill` + `DamageDetail.originSkillId`。
- 武器/套装：只接受资源导出阶段得到的唯一 `BuffId → weapon/suit templateId`
  引用，并与来源角色的战斗开始配置快照交叉验证。ID 前缀、时间接近和“当前仅装备
  一个候选项”均不能作为归属依据；不能唯一验证时写入 `unknown`。

schema 11 分开保存原始 `sourceEntityId` 与经验证的 `creditedActorId`。投射物和
`AbilityEntity` 的原始实体标识不得因角色 ID 归一化而丢失；只有运行时直接来源或
唯一静态技能映射能够证明角色归属时，才填写 `creditedActorId`。

## 出战角色与快照

### 出战列表

```text
GamePlayer.squadManager (0x48)  → SquadManager
  → curSquad (0xa0)             → Squad
    → slots  (0x20)             → List<Squad.Slot>
      每个 Slot:
        index      0x10
        charInstId 0x18  角色实例 id
        charId     0x20  角色模板 id（chr_xxxx）
        character  0x28  Entity
```

`Squad` 另有 `members 0x38`（`List<Entity>`）、`GetSlotByInstId`、`GetLeaderSlot`、
`GetMemberBySlot`；`SquadManager` 有 `squadMembers`、`slotCount`、`TryGetCharacter`。

### 角色快照

```text
GamePlayer.charBag (0x60)  → CharBagSystem
  → GetCharInfo(UInt64 charId)      → CharInfo
  → TryGetCharInfo(charId, out CharInfo, showError)
```

`CharBagSystem` 也有 `charInfos 0xb8` / `clientCharInfos 0xd0`
（`Dictionary<UInt64, CharInfo>`），可按 `Slot.charInstId` 直接查。

`CharInfo` 关键字段：

```text
skillGroupLevelInfoList 0x50  List<SkillGroupLevelInfo>
talentInfo              0x58  TalentInfo
potentialLevel          0x60  潜能等级
equipSuit               0x88  装备套装
```

`SkillGroupLevelInfo`：`level 0x10`、`maxLevel 0x14`、`skillGroupId 0x18`。
`SkillLevelInfo`：`level 0x10`、`maxLevel 0x14`、`skillId 0x18`。

武器与技能数据兜底工具：

- `TryGetWeaponTemplateIdByCharInstId(charInstId, out weaponTemplateId)`
- `TryGetCharSkillGroupInfo(charId, skillGroupId, out SkillGroupLevelInfo)`
- `TryGetEquipSuitSkillIdAndLevel(equipSuit, out List<SkillLevelInfo>)`

## 关卡与时长

`BattleRecorder.m_curBattleInfo 0x20` → `BattleInfo`：

```text
modeId       0x10
dungeonId    0x18  关卡 id
gameId       0x20
gameInstId   0x28
gameUniqueId 0x30
battleId     0x38
```

`DetailReport`（detail 链路，仅作参考，不可依赖）：

```text
startTimeStamp  0x10
endTimeStamp    0x18
sceneId         0x30
battleGymLevel  0x38
charStatus      0x40  List<DebugCharStatus>（受 enableRecord 限制）
```

战斗时长以模块自身 session 起止时间为主，`dungeonId` 用于关卡名映射。

## 分类维度（zone）

```text
DataManager.damageScaleProcessorConfig 0x4d8
  → DamageScaleProcessorConfig
    → allZones 0x18  List<Zone>
      每个 Zone:
        name                  0x10
        alias                 0x18
        isMultiplyZone        0x20  乘区/加区
        mergeAttackerAndDefender 0x21
        isDamageTypeZone      0x22
        serverIndex           0x24
```

`DamageScaleProcessorConfig` 另有具名特殊区：

```text
damageTypeDamageIncreaseZoneName      0x20
igniteDamageIncreaseZoneName          0x28
skillTypeDamageIncreaseZoneName       0x30
damageToBrokenUnitIncreaseZoneName    0x38
damageEnhancedZoneName                0x40  增幅
damageVulnerableZoneName              0x48  脆弱
```

分类轴由运行时枚举 `allZones` 生成，替代硬编码的十类。

## 中文名映射

TableCfg（`research/table-dump/Table/`）两跳映射：

```text
内部 id（buff_xxx / sk_xxx / wpn_xxx / chr_xxx）
  → StrIdNumTable.json（buff_id / skill_id / item_id / char_id 分类）
  → 数字 strId
  → I18nTextTable_CN.json
  → 中文名
```

部分配置表直接携带数字 strId：

- `DungeonTable[].dungeonName.id`
- `WeaponBasicTable[].engName.id` / `weaponDesc.id`
- `SkillPatchTable[].description.id`

`BuffTable.json` 为空，**buff 没有中文名，客户端里根本不存在这个字符串**。`StrIdNumTable` 帮不上忙：
它是 string id → 紧凑整数的注册表（`char_id` 里 `chr_0004_pelica → 10`），与本地化无关；
2871/2872 个 buff id 在里面有编号，拿编号查 `I18nTextTable_CN` 命中 0。显示名只能由
`buffDisplay()` 从来源反推，详见 `BUFF_TABLE_EXPORT.md`。
游戏另有 `I18nTextTable_EN/JP/KR/TC/...`，可一并支持多语言。建议构建期生成紧凑 `id → 中文名` 索引随模块分发。

## 存储结构

事件流 + 汇总，二进制紧凑存储：

- 伤害事件：时间戳、攻击者、技能、伤害、hp 变化、类型、暴击、rDPS 归属列表。
- buff 生命周期：开始（buffId、来源、目标、时长）、结束（buff 实例 id）。
- session 汇总：总伤害、各角色/各 zone 聚合。

实时阶段把每次 `ModifyDamageScaleZone` 写值聚合成伤害事件的最终归属，不落盘底层写值。
一场典型战斗约 200 KB ~ 低 MB 级。

## 伤害公式（2026-09-03 客户端反汇编，rDPS 归因的依据）

`BattleFormula.CalculateDamage(ref pack, ref isCritical, ref isBlocked, ref reportInfo)`：

```text
damage = poiseFactor(=1)
       × calcResult × Π_zones scale(zone)              GetFinalAttackValue() = calcResult × _GetFinalDamageScale()
       × attacker[62 WeaknessDmgScalar]
       × (1 + attacker[10 CriticalDamageIncrease])     仅暴击时；暴击由 Dice(attacker[9 CriticalRate]) 决定
       × 1 / (1 + k(attackerLevel) × defender[3 Def])  _GetDefResistanceValue；damageType==Real(1) 特判
       × (1 - defender[63 ShelterDmgScalar])
       × max(0, (1 - defender[Resistance_t] / 100) × defender[DamageTakenScalar_t])   _GetDamageTypeResistanceValue
       × attacker[49 IgniteDamageScalar]               仅 damageDecorateMask & 0x0FD00038
```

`calcResult` 由 `DamageAction._CalculateDamageResultByType` 预先算出，普通技能等于
`attacker[2 Atk] × atkScale`；持续伤害等不按攻击力算，账本用 `factors.attackLinked` 标记，
只有 linked 的命中才把攻击力增益计入份额。

`_GetDamageScale(zone)`：`scale = max(0, A[i] × D[i])`，`mergeAttackerAndDefender` 的区为
`max(0, A[i] + D[i] - 1)`。读取前先把属性折进数组（写回 `attackerZones/defenderZones`）：

| 配置名（DamageScaleProcessorConfig） | 折入 | 属性 |
|---|---|---|
| damageTypeDamageIncreaseZoneName | 攻方 | 50-55 按伤害类型（Physical/Fire/Pulse/Cryst/Natural/Ether） |
| igniteDamageIncreaseZoneName | 攻方 | 35-38 *BurstDamageIncrease |
| skillTypeDamageIncreaseZoneName | 攻方 | 17/28/32/33 普攻/终结技/战技/连携 |
| damageToBrokenUnitIncreaseZoneName | 攻方 | 61 |
| damageEnhancedZoneName | 攻方 | 64-69 *EnhancedDmgIncrease（增幅） |
| damageVulnerableZoneName | 守方 | 70-75 *VulnerableDmgIncrease（脆弱） |

多个角色可以是同一个区（当前客户端 `NormalCalcZone` 同时承担 damageType/ignite/skillType/brokenUnit）。
`ModifyDamageScaleZone(side, name, value)`：非乘法区 `zones[idx] += value`，乘法区 `zones[idx] *= 1 + value`。

DamageType 枚举：0 Physical、1 Real、2 Fire、3 Pulse、4 Cryst、5 LifeDrain、6 Natural、7 Ether。
抗性枚举顺序与元素顺序不同：94 Physical、95 Natural、96 Cryst、97 Pulse、98 Fire、99 Ether；
DamageTakenScalar：4 Physical、5 Fire、6 Pulse、7 Cryst、48 Natural、60 Ether。

### 属性公式

`Attributes.GetAllModifierValues(type)` 返回 9 项：

```text
[0] raw  [1] Σ baseAddition  [2] Σ baseMultiplier  [3] Σ baseFinalAddition  [4] Π baseFinalScalar
         [5] Σ addition      [6] Σ multiplier      [7] Σ finalAddition      [8] Π finalScalar
base  = clamp((clamp(raw + [1]) × max(0, 1 + [2]) + [3]) × [4])
final = clamp(((base + [5]) × max(0, 1 + [6]) + [7]) × [8])
```

`AttributesCalculator._CalculateFinalAttribute` 的 multiplier 形参已经是 `1 + Σ`，直接把 Σ 传进去
会得到错误结果（第一版账本的 `fullValue` 即因此全错），所以模块自己按上式计算，并用
`packValue` 交叉校验（1202/1202 一致）。`Buff.GetValue` 对应 [5..8]，`Buff.GetBaseValue` 对应 [1..4]。

### 活跃修饰器

`Attributes.GetModifiers(type)` 返回 `HashSet<IAttributesModifier>`，这是游戏真正求和的集合。
生命周期映射（RecordBuffStart/Finish）会残留已被顶掉的叠层实例（如 `buff_wpn_lance_0014_damageup`
5 层上限之外的实例仍活着但不再被求和），所以属性归因只信这个集合。元素按运行时类指针分类：
`Buff` 走 `Buff.GetValue/GetBaseValue`，其余（Equip/Potential/Talent/WeaponAttributeModifier、CardSkill）
计入 `otherModifiers`。`HashSet` 按 IL2CPP 布局直读：`_slots 0x18`、`_count 0x20`、`_lastIndex 0x24`，
Slot 16 字节 `{hashCode, next, value@8}`，已用槽 `hashCode >= 0`；枚举数与 `_count` 不符即判失败并回退映射。

## 每次命中的证据账本与归因（schema 14，2026-09-05 起）

每个 `damage` 效果带 `ledger`，内容是这次命中在游戏内被写过的全部数值证据，
**不经语义目录过滤、不区分来源是否己方**，`rdpsShares` 由其派生：

```text
ledger.originSkillId / calcResult / atkScale
ledger.attackerZones[] / defenderZones[]   最终乘区数组
ledger.factors                             attack, attackLinked, zoneProduct, defense, defResistance,
                                           typeResistance, critical, critRate, critDamage, weakness,
                                           shelter, ignite, igniteApplies（即上面公式的各项）
ledger.zoneWrites[]                        每个 DamageModifier 对乘区的写入：statusInstanceId, statusId,
                                           sourceId, ownerId, side, zoneIndex, zoneName, before, after,
                                           external, semantic
ledger.attributeGroups[]                   攻/守方所有伤害相关属性 + 映射声称有贡献的属性：values[9]、
                                           min/max、fullValue（模块公式）、packValue、liveEnumerated、
                                           liveBuffs、otherModifiers
ledger.attributeWrites[]                   活跃集合里每个 Buff 的 8 项贡献 + withoutValue
ledger.contributors[]                      归因结果：mechanism(zoneWrite/attribute/instantModifier/
                                           artsStrength)、contributionKind、factor(=去掉后伤害/实际伤害)、
                                           fraction、external、excluded、semantic
ledger.externalFraction / selfFraction     1 - Π factor（外部角色 / 自身增益）
ledger.staleMapBuffs                       映射里有贡献却不在任何活跃集合中的 buff 数
dictionary.zones[]                         运行时 allZones：index, name, multiply, mergeSides, roles
diagnostics.attribution                    hits, hitsWithExternal, liveSetFailures, groupSumMismatch,
                                           zoneResidualUnexplained, lateBuffs, staleMapBuffs, attackUnlinked
```

归因规则：

- 外部 = `creditedActorId`（或 `Buff.source`）是 `chr_` 且不等于攻击者；敌方来源和自身增益不进份额。
- 语义目录只做命名与 `excluded/structural` 排除，不再要求 `verified`。
- 每个 (buff, 机制, 位置) 算一个 factor：乘区写入按 additive/multiply 回退到写入前；属性贡献按属性公式
  算 `full - without` 的差，再按伤害公式映射（Atk 线性、区属性回退到区、Def 用 `k` 反推、抗性/承伤
  按 `(1 - R/100) × T`、暴击伤害仅暴击命中、暴击率按期望值）。
- skillType/ignite/brokenUnit 属性是否生效由乘区残差决定：`A[i] - 1 - Σ写入 - 无条件属性` 必须被
  某个候选子集精确解释，否则记 `zoneResidualUnexplained`。
- 份额：`externalFraction = 1 - Π factor`，按 `-ln(factor)` 权重在外部贡献者之间分配。

### 贡献分类（`contributionType` / `contributionKind`）

分类对应伤害公式里的一个因子，攻方在前守方在后，乘区类目按客户端
`DamageScaleProcessorConfig` 给该乘区分配的角色判定，而不是乘区下标：

| # | id | 名称 | 判据 |
| --- | --- | --- | --- |
| 0 | `direct` | 直伤 | 攻击者自留 |
| 1 | `attack` | 攻击力 | 属性 2 |
| 2 | `damageIncrease` | 增伤 | damageType / skillType / brokenUnit 角色乘区，属性 17/28/32/33/50-55/61 |
| 3 | `abnormalIncrease` | 异常增伤 | ignite 角色乘区，属性 35-38、点燃系数 49 |
| 4 | `amplification` | 增幅 | enhanced 角色乘区，属性 64-69 |
| 5 | `critical` | 暴击 | 属性 9（按期望）、10（仅暴击命中） |
| 6 | `independentZone` | 独立乘区 | 无属性角色的乘区（ProdCalcZone / ComboCalcZone / RaceCalcZone） |
| 7 | `fragile` | 脆弱 | vulnerable 角色乘区，属性 70-75/80-85 |
| 8 | `vulnerabilityTaken` | 承伤 | 守方 damageType 乘区，承伤系数 4/5/6/7/48/60 |
| 9 | `defense` | 减防 | 属性 3 |
| 10 | `resistance` | 减抗 | 属性 94-99 |
| 11 | `artsStrength` | 法术强度 | `InstantModifyAttribute` 瞬时修饰 |
| 12 | `other` | 其他 | 弱点 62 / 庇护 63 等未归类项 |

共享该编号的还有：覆盖层分段（`kRdpsContributionCount = 13`）、`characters[].rdpsByContribution[]`、
`timeline[].rdpsByContribution[]`、桌面端 `CombatRdpsCategories`、web `CONTRIBUTION_CATEGORIES`。
改这张表必须同步全部五处，并 bump schema。

### 操作（`actions[]`）

`Skill.DoCast` 开、`Skill.CastEnd` 关；`result` 只在真正被打断时给 `interrupted`：

```text
result        completed / interrupted / cancelled / superseded / openAtSessionEnd
finishType    Skill.FinishType：completed | interrupted（仅正常关闭的 skillCast）
interruptReason  Skill.InterruptReason：default, enterFreeState, aiManual, mud, detachSkill,
                 interruptAction, dash, castNextSkill, levelScript, narrative
```

注意 `InterruptReason` 在正常结束的技能上也非零（普攻收招通常是 `enterFreeState`），
所以判断“被打断”只能看 `finishType`，不能看 reason。

### 状态叠加（`statusApply.stacking`）

同名状态能不能叠、最多叠几层，由 `BuffData.stackingSettings`
（`Beyond.Gameplay.Core.BuffStackingSettings`）决定，随每个实例记录：

```text
type / typeId  StackingType 序号与名称
maxStack       Buff.get_maxStackCount()，已解析 useMaxStackCntKey 的黑板取值
enhanceCount   本实例见过的最高 Buff.m_enhanceCnt（叠层型把层数收在单实例里）
keyed / key    identifierType==StackingKey 时，多个 buff id 共用同一组层数
```

| # | typeId | 含义 | 多实例 |
| --- | --- | --- | --- |
| 0 | `unlimited` | 无限叠加 | 是 |
| 1 | `highPriority` | 取最高优先级 | 否 |
| 2 | `stack` | 叠加，上限 `maxStack` | 是 |
| 3 | `enhance` | 叠层强化 | 否 |
| 4 | `refresh` | 刷新持续时间 | 否 |
| 5 | `extend` | 延长持续时间 | 否 |
| 6 | `modify` | 改写 | 否 |
| 7 | `unique` | 唯一 | 否 |
| 8 | `enhanceAndRefresh` | 叠层强化并刷新 | 否 |
| 9 | `overwriteDuration` | 覆盖持续时间 | 否 |
| 10 | `enhanceAndOverwriteDuration` | 叠层强化并覆盖时长 | 否 |
| 11 | `highPriorityWithMaxStack` | 优先级叠加 | 是 |
| 12 | `timedGrowingEnhance` | 随时间增长叠层 | 否 |

序号来自 `get_isStackType`（{2,3,8,10,11,12}）、`get_isPriorityType`（{1,11}）、
`IsEnhanceType`（{3,8,10}）三个函数里直接比较的立即数，不是猜的声明顺序。

### 状态标称量级（`statusApply.nominalEffects`，schema 15，2026-09-06 起）

`meanMultiplier` 回答的是「这场里这个 buff 实际把伤害乘了多少」。它的对照面——
「这个 buff 本来配了多少」——不能从 BuffData 配置里读：配置里 1023 个数值中有 833 个
（81%）不是字面量而是黑板键（`{"k":"rate"}`），量级由技能等级、潜能、词条档位在挂载时
决定，静态表里根本没有那个数（见 `BUFF_TABLE_EXPORT.md`）。

所以标称量级只能在运行时取，来源有两个：

```text
Buff.GetValue(attributeType, out add, out mul, out finalAdd, out finalScalar)
Buff.GetBaseValue(...)          # 同上，填 base_* 一半
```

按 `Buff.attributeMask` 逐属性调用，拿到的是**已解析**的每槽位值——黑板已代入、
叠层已折算。模块本来就在采（`CaptureBuffAttributeContributions`），schema 15 之前只
用于内部分类，没有写进记录。

单靠它有个盲区：参数在**伤害结算时**才解析的 buff（命中敌方数量、目标身上的层数、
自身当前血量）不写在 buff 自己的黑板上，而是在 `InstantModifyAttributeProcessor.Process`
里对着**伤害包的** blackboard 现算。`Buff.GetValue` 看不见这类。因此第二个来源是
`InstantModifyAttributeHook` 里已经算出来的 `AttributeModifier.param`，按 buff 实例
并入同一张表。

记录成区间而非单值：

```text
attributeType  Beyond.GEnums.AttributeType 序号
slot           Beyond.GEnums.ModifierType 序号 0-7
min / max      该 (属性, 槽位) 在这个 buff 实例存活期间观察到的量级范围
```

`min == max` 是常态，表示 buff 全程只有一个量级。区间张开本身是信息：说明它随某个东西
缩放（叠层、词条档位、这一击打到几个人）。会变这件事游戏自己就承认——`Buff` 为此专门
抛 `OnBlackboardValueChange`，`Buff.ModifyAttributes` 把 `enhanceCount` 当入参。

槽位语义与属性公式一致：`base = ((raw + [4]) × (1 + [5]) + [6]) × [7]`，final 用
`[0]-[3]` 同构。所以 3 / 7 是直乘、中性值为 1，其余为加、中性值为 0；判定用的就是这个。
中文名见 `web/src/lib/buffTerms.ts` 的 `ATTRIBUTE_NAMES` / `MODIFIER_SLOT_NAMES`。

`BuffStackingGroup` 顶满后会静默挤掉最旧的一层，被挤掉的 `Buff` 不走
`Buff.Finish`，所以记录器看不到它的结束。web 端 `resolveStackGroups()` 用上面的
上限重放每个叠加组补回这些结束时间，并给被挤掉的区间打 `displaced`。归因本身不
受影响：它读的是 `Attributes.GetModifiers()` 活集，游戏丢掉的实例本来就不在里面。

`dictionary.zoneNames` 仍保留观察学习到的名字，与 `zones[]` 不一致时日志 `[rdps-zones] observed zone name mismatch`。

## 不可依赖的链路

- `_RecordDamageDetail` 及后续 detail 方法：受 `SetInDetailRecording` 与
  `BattleRecorderConfig.enableRecord` 控制，普通战斗可能落在 `DontRecord`。
- `DetailReport.charStatus`（`DebugCharStatus`）：detail 产物，可能为空，改走 `CharInfo` 直读。
