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

`DamagePackData`（`size 0x110`）：

```text
calcResult              0x58  CalcResult.value
damageType              0x60
damageDecorateMask      0x68
atkScale                0x88
attackerDamageScaleZones 0x90  double[]
attackerAttributes      0x98  double[]
defenderDamageScaleZones 0xa0  double[]
defenderAttributes      0xa8  double[]
isBlocked               0xb0
useHpChangeAsDisplayValue 0xb1
```

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

`BuffTable.json` 为空，buff 中文名只能走 `StrIdNumTable.buff_id`；纯内部 buff 无本地化名时退回内部 id 或语义别名。
游戏另有 `I18nTextTable_EN/JP/KR/TC/...`，可一并支持多语言。建议构建期生成紧凑 `id → 中文名` 索引随模块分发。

## 存储结构

事件流 + 汇总，二进制紧凑存储：

- 伤害事件：时间戳、攻击者、技能、伤害、hp 变化、类型、暴击、rDPS 归属列表。
- buff 生命周期：开始（buffId、来源、目标、时长）、结束（buff 实例 id）。
- session 汇总：总伤害、各角色/各 zone 聚合。

实时阶段把每次 `ModifyDamageScaleZone` 写值聚合成伤害事件的最终归属，不落盘底层写值。
一场典型战斗约 200 KB ~ 低 MB 级。

## 不可依赖的链路

- `_RecordDamageDetail` 及后续 detail 方法：受 `SetInDetailRecording` 与
  `BattleRecorderConfig.enableRecord` 控制，普通战斗可能落在 `DontRecord`。
- `DetailReport.charStatus`（`DebugCharStatus`）：detail 产物，可能为空，改走 `CharInfo` 直读。
