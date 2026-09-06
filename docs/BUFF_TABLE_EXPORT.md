# Buff 资源表在线导出

本文记录如何一次性导出全部 `BuffData`，以便网页端把 buff 的机制字段（叠加规则、持续时间、图标路径、
分类标签）作为**随发布版本附带的静态资源表**，而不是塞进每一份战斗记录。

字段偏移来自 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/Gameplay.Beyond.dll.cs`
（游戏版本 1.4.4）。与 `COMBAT_RUNTIME_CONTRACTS.md` 一致，运行时按类名/字段名解析，不硬编码地址。

## 为什么必须在线导出

离线路线不可行：

- `research/combat-jsondata/Data/Json/BuffData/` 下的 2,872 个 `.json` 是 MemoryPack 序列化的二进制，
  后缀是假的。MemoryPack 是位置式格式，**不存字段名**——扫描全部 2,872 个文件，
  `attributeModifier` / `damageModifier` 这类 token 命中数为 0。想离线解析必须先逆出 schema。
- `research/table-dump/Table/BuffTable.json` 是空的（2 字节 `{}`），TableCfg 里没有 buff 数据。

而 native 模块本来就在游戏进程内持有 IL2CPP 访问权，`BuffData` 已经是它读取叠加规则的对象
（见 `native/modules/combat_stats/module.cpp` 中 `buff-data.stacking-settings` 的解析），
只是当前仅覆盖战斗中实际出现过的 buff。

导出器是本地开发工具，**不进发布模块**：实现放在 `tools/BuffProbe/`，
和 `build_buff_table.py` 一起构成 buff 表流水线。构建目标是 `EXCLUDE_FROM_ALL` 的，
产物落到 `${STAGE}/research/`，不出现在发布布局里。

### 离线路线的补充观察

`.json` 后缀虽是假的，但格式没有文档最初以为的那么封闭。实测两个文件：

- 首字节 `1e` = 30，正好等于 `BuffData` 的字段数，即 MemoryPack 的成员计数；
- 字符串是 `int32 字节长度 + UTF-8`（`08 00 00 00 "duration"`、`18 00 00 00 "buff_Ether_AttackUp_buff"`），
  不是上游 MemoryPack 的取反长度编码，说明这是 `Beyond.MemoryPack` 的简化实现；
- 每个文件都内嵌自己的 id，且重复字符串会重复出现（没有字符串池）。

卡点只剩一个：id 出现在文件约 80% 处而不是开头，说明**成员的序列化顺序不是 dump 里的偏移顺序**，
而 dump 不含方法体，读不出声明顺序。要离线解析就得靠 2872 个样本反推成员顺序
（"解析必须精确消费到文件末尾" 是很强的判别器，可做增量搜索）。这是一个独立的逆向项目，
不是写个脚本的事，所以当前仍走在线导出；但它是可行的，不是死路。

## 为什么值得做

网页端的 `buffDisplay()`（`web/src/lib/combat.ts`）靠 `sourceKind` / `sourceTemplateId` / `sourceSkillId`
反查武器/套装/技能/角色来生成显示名，这些字段目前**逐次挂载**写进记录。对一份真实记录做函数依赖检查
（466 次挂载 / 136 个不同 buff id，`tmp_analysis/resource_probe.py`）：

| 字段 | 在同一 buff id 内恒定 |
|---|---|
| `statusKind` | 136/136 |
| `contributionType` | 136/136 |
| `contributionKind` | 136/136 |
| `stacking`（去掉 `enhanceCount`） | **136/136** |
| `sourceKind` | 135/136 |
| `sourceTemplateId` / `sourceSkillId` | 134/136 |

即 schema 14 的 `statusApply.stacking` 整块都是 `BuffData.stackingSettings`，只有 `enhanceCount` 是运行时的。
把这些搬进资源表，每份快照省下 1,595 B（deflate 后），相当于精简档快照总量的一半。

那 1–2 个例外全部是 `buff_common_*` 共享 buff（任何角色/套装都能挂），规则很干净：

> `buff_common_*` 的来源随事件走；其余 buff id 自带唯一来源，可以完全进资源表。

## 入口

`Beyond.Gameplay.Core.BattleDataLoader`（`Gameplay.Beyond.dll`，继承 `Beyond.Gameplay.BaseGameManager`）：

| 成员 | 位置 | 说明 |
|---|---|---|
| `m_buffDataCache : Dictionary<string, BuffData>` | 字段 @0x78 | 已加载的 BuffData |
| `TryGetBuff(string buffId, out BuffData)` | RVA `0x0351CAB0` | 按需加载并返回 |
| `PreloadAllBuffData(List<string> buffIds)` | RVA `0x0351C990` | 批量预热 |
| `ClearCache()` | RVA `0x06037A18` | 释放缓存 |
| `S_PRELOAD_CNT_PER_FRAME` | 静态 @0x0 | 游戏自己的分帧预加载节流常量 |

dump 中 `BaseGameManager` 未暴露静态访问器，取实例最省事的方式是**hook `TryGetBuff` 一次，
从第一次真实调用中捕获 `this`**，此后自行循环调用。

buff id 列表直接用 `research/combat-jsondata/Data/Json/BuffData/` 的文件名（去掉 `.json`）。
游戏自身的 `BattlePreloadTable.buffDataPreloadConfig.buffList : List<string>` @0x10 只是预加载子集，不完整。

`GameplayTag` 只有 `tagId : int32`（值类型，数组内联 4 字节一个），`tagName` 是属性，
导出时必须调 `get_tagName` getter，否则拿到的是无意义的整数。

`BlackboardDouble` / `BlackboardInt` 继承 `BlackboardParamBase<,>`，有
`useBlackboardKey` / `value` / `blackboardKey` 三个字段：走 key 的没有静态值，
导出成 `{"k": "<key>"}` 交给消费方当动态量处理，走字面量的导出成 `{"v": <数>}`。
`duration`、`triggerInterval`、`maxTriggerCnt`、`AttributeModifier.param`、
`DamageScaleProcessor.addition` 全部受此影响。

## 可导出的字段

`Beyond.Gameplay.Core.BuffData`（继承 `IBattleServerData<>` / `IMemoryPackSerialize`）：

| 字段 | 偏移 | 用途 |
|---|---|---|
| `id : string` | 0x10 | 主键 |
| `iconConfig : BuffIconConfig` | 0x18 | → `_spritePath` @0x10，**真实图标路径** |
| `hasIcon : bool` | 0x20 | 是否在 HUD 显示 |
| `attributeModifier : AttributeModifierData` | 0x38 | → `attributeModifiers[]`，每项 `{modifyAttributeType, attributeType, formulaItem, param}` |
| `damageModifier : List<DamageModifier.Data>` | 0x40 | → `damageProcessors[]`，`DamageScaleProcessor` 带 `side` / `zoneName` / `addition` |
| `healModifier` / `poiseModifier` / `globalModifier` / `shieldConfigs` | 0x48–0x60 | 治疗 / 韧性 / 全局 / 护盾，决定分流时不能只看上面两个 |
| `triggerInterval` / `waitFirstTriggerInterval` / `maxTriggerCnt` | 0x88–0x98 | 周期触发型的节奏 |
| `applyTags : GameplayTag[]` | 0x68 | 分类标签 |
| `lifeType : Buff.LifeType` | 0x78 | 生命周期 |
| `duration : BlackboardDouble` | 0x80 | 基础持续时间 |
| **`stackingSettings : BuffStackingSettings`** | 0xa0 | 叠加契约，模块已能读全 5 个子字段 |
| `dispelConfig : DispelConfig` | 0xa8 | 驱散规则 |

`BuffStackingSettings` 的子字段（模块已解析）：`identifierType` / `stackingType` / `stackingKey` /
`useMaxStackCntKey` / `maxStackCnt`。`StackingType` 序数表见 `COMBAT_RUNTIME_CONTRACTS.md`。

### 拿不到的：名称

`BuffIconConfig` 只有 `_spritePath`，**没有 name 字段**——游戏 HUD 只显示 buff 图标，不显示名字。
所以显示名仍然由 `buffDisplay()` 从来源反推，这条不变。

离线两跳也走不通，**这一条与 `COMBAT_RUNTIME_CONTRACTS.md` 里"buff 中文名只能走
`StrIdNumTable.buff_id`"的说法冲突，以本文为准**：`StrIdNumTable` 是 string id → 紧凑整数的
注册表（`char_id` 里 `chr_0004_pelica → 10`），与本地化无关；本地化只走各配置表自带的
`{id: <int64>}` → `I18nTextTable_CN`，而 `BuffTable.json` 是空的，没有任何一行携带 buff 的 name id。
实测 2871/2872 个 buff id 在 `StrIdNumTable` 里有编号，拿编号查 `I18nTextTable_CN` 命中 **0**。

但 `_spritePath` 本身是净收益：原来解析不出来的 buff 是借用提供者头像当图标
（`icon: buff.sourceId ? characterIcon(...) : ""`），有了真实路径就能显示正确的图标。这条
已经接上了，见下文。

至于「它到底改了什么」（攻击力 +20%），后来走了另一条路：`attributeModifier.param` 里
81% 是黑板键，静态表给不出数，所以 schema 15 改成由记录器在运行时解析并写进
`statusApply.nominalEffects`，见 `COMBAT_RUNTIME_CONTRACTS.md`。

## 导出流程

`tools/BuffProbe/`（`BetterEndfield.BuffProbe`，`EXCLUDE_FROM_ALL`）：

1. `Initialize` 解析 `TryGetBuff` / `TailLateTick` / `ClearCache` / `get_tagName` 与上表全部字段。
2. hook `TryGetBuff`，**只用来从第一次真实调用里捕获 `this`**，随后自行驱动 loader。
3. 后台线程轮询热键（默认 `Ctrl+F9`，ini 可改），把请求**锁存**成一个原子标志。
4. hook `BattleManager.TailLateTick` 消费该标志，在游戏线程上执行导出。
5. 一次性遍历 `combat-buff-ids.txt`，逐个 `TryGetBuff` 并读出字段，
   写 `%LOCALAPPDATA%\BetterEndfield\combat-buffs.json`，最后 `ClearCache()`。
6. `tools/CombatDataExporter/build_buff_table.py` 消费该文件，产出网页端资源表。

```powershell
cmake --build build --config Release --target BetterEndfield.BuffProbe
python tools/CombatDataExporter/build_buff_table.py --emit-id-list <modules 目录>
# 把 dll / .module.ini / combat-buff-ids.txt 拷进 modules/
# 注入 → 进任意战斗 → Ctrl+F9 → 用完把这三个文件删掉
python tools/CombatDataExporter/build_buff_table.py
```

### 触发路径为什么是这样（三次失败换来的）

- **不能 hook `BattleManager.Tick`。** combat_stats 已经占了这个目标，host 的
  HookBroker 一个目标只允许一个属主。探针先注册就会把 combat_stats 的 Tick hook 顶掉，
  日志里是 `Hook conflict at a target already owned by betterendfield.buff-probe`，
  代价是发布模块失去技能收招的兜底关闭。`TailLateTick` 同样每帧调用且无人占用。
- **不能在 hook 里直接采键。** hook 只在游戏恰好调用时执行一次，
  按键的百来毫秒几乎永远错不上，实测一次都没采到。所以采键必须在独立线程上连续做。
- **不能拿 `TryGetBuff` 当执行点。** 客户端在战斗开始时就把 buff 数据预载完，
  之后不再调用它——锁存标志设上了却永远等不到执行。第一版日志里
  `exported 2872/2872 in 0 ms` 就是旁证：那 2872 次查询全是缓存命中，没有一次真的读盘。

### `BlackboardParamBase<,>` 的字段读不出来

`useBlackboardKey` / `value` / `blackboardKey` 声明在泛型基类上，
**IL2CPP 的按名查字段不走父类**，所以对着 `BlackboardDouble` 问一律 `not-found`；
换成泛型定义名 `BlackboardParamBase\`2` 也解析不到。探针因此保留一条偏移兜底
（`0x10` / `0x14` / `0x18`），依据是两个子类都是 `size: 0x20`，即对象头之后只有 16 字节，
只能容下 bool + 4 字节值 + 字符串指针这一种排法。

由此还牵出一个会读出垃圾的坑：**`BlackboardDouble` 的 `TSerializeValue` 是 `float` 不是
`double`**（"Double" 指的是返回类型，见它的 `.ctor(System.Single)`），按 8 字节读会错位。
实测数据反过来证实了这点——导出的 `param` 里出现 `0.319999993`，正是 `float(0.32)` 加宽的结果。

## 实测结果（游戏 1.4.4）

2872/2872 全部导出，`missing` 为空——文件名去掉 `.json` 就是 buffId，
`buff_Ether_AttackUp` 这类大小写混合的 `TryGetBuff` 也认。

| | 行数 | 原始 | deflate |
|---|---|---|---|
| `combat-buffs.min.json` | 686 | 92.7 KB | 9.5 KB |
| `combat-buffs-full.min.json` | 2186 | 153.1 KB | 15.6 KB |

主表字段覆盖：`at` 属性修饰 485、`dz` 乘区写入 188、`ic` 真实图标 303、`tg` 标签 83，
`d` / `ti` / `mt` / `st` 全覆盖。`duration` 有 1924 条字面量、948 条黑板键、0 条读取失败。

三处交叉验证：`buff_Ether_AttackUp_buff` 的 `at` 是 `{at:2, fi:6}`（属性 2 = 攻击力）；
`buff_common_affixes_enhance_fire` 的 `at:65` 落在 64–69 的 `*EnhancedDmgIncrease`
区间，正是 `amplification`（增幅）类目；黑板键 `"attack"` / `"duration"` 与直接
hexdump 那两个 MemoryPack 文件里看到的字面字符串完全一致。

已知瑕疵：`unknownProcessors=86`，即 86 个 `DamageProcessorBase` 不是
`DamageScaleProcessor` 子类而被跳过。大概率是治疗/韧性类处理器，不影响伤害分析；
如果后面发现某些 buff 缺乘区信息，从这里查。

## 产出：两张表

分流判据是**六项全空**——`attributeModifier`、`damageModifier`、`healModifier`、
`poiseModifier`、`globalModifier`、`shieldConfigs` 都没有内容的 buff（特效标记、出生、
巡逻回血、韧性抗性等）才归入懒加载那张。只看前两项会把护盾和治疗类 buff 误判成
「与伤害无关」，而它们在时间轴上是用户想看到的。

两张表都按 **id 注册表下标**索引，不存字符串 key：2872 个 buff id 已经全部在
`tools/CombatDataExporter/id_registry.json` 里（快照本来就按下标引用它们），
重复一遍字符串要多花约 100 KB 包体。

| 文件 | 内容 | 行数 / 图标 | 打包后 | 加载时机 |
|---|---|---|---|---|
| `combat-buffs.min.json` | 六项里有任一项非空的 buff | 686 / 57 精灵 | 10.5 KB gzip | 打开战斗详情时 |
| `combat-buffs-full.min.json` | 其余的（纯标记 / 特效 / AI 状态） | 2186 / 82 精灵 | 17.8 KB gzip | 记录里出现主表没有的 buff 时 |

两张都是独立的懒加载 chunk，不进主包（主包只多了 0.5 KB 的加载器）。**都依赖 id 注册表**
——行按注册表下标索引，要拿 buffId 反查就得先有 `combat-ids.min.json`，所以
`buffTable.ts` 里两者是一起 await 的。

### 网页端怎么用它

`web/src/lib/buffTable.ts` 把下标表翻成 `Map<buffId, row>`，`useBuffTable(record)`
负责加载并在到货时 bump 一个版本号，让 memo 化的显示重算。主要用途只有一个：

- **`ic` → 真实图标。** `buffDisplay()` 现在先查表，查到就用 buff 自己的精灵；查不到才
  退回原来的逻辑（武器/套装/技能图标，或提供者头像）。区别在于后者本质上是**施加者的**
  图标而不是这个状态的图标。473 个 buff 有自己的精灵，落到 120 个 PNG（`web/public/icons/buffs/`）。
- **`d` → 悬浮卡里的「配置 Xs」**，和实测区间并排。只在是字面量时显示。

`at` / `dz` 这两列**没有接**：schema 15 的 `statusApply.nominalEffects` 是运行时解析出来的
真实量级，比配置里 81% 都是黑板键的 `at` 强，接了反而是退步。它们留在表里是给以后
（比如离线查询某个 buff 配了什么）用的。

`buff_wpn_sword_0019` 是唯一一个引用了不存在精灵的 buff（游戏配置里的悬空引用），
两条渲染路径都会静默降级：canvas 侧图片只请求一次不重试，DOM 侧有 `onError` 隐藏。

两张表都带 `tableVersion`。快照头部记录所依赖的版本；版本对不上时回退到 `buffShortName(id)`
显示原始 id，而不是显示空白。

## 风险与注意事项

- **会卡一下。** 2,872 次资源加载（平均 863 B/个，合计约 2.5 MB）一次做完，主线程冻结一两帧。
  这是一次性的开发者操作，探针刻意没有分帧——分帧要引入状态机，而收益只是让开发者少看一次卡顿。
- **跑完必须 `ClearCache()`**，否则 2,872 个 `BuffData` 常驻内存。探针在写完文件后调用。
- **文件名到 buffId 的映射需要验证。** 大概率是去掉 `.json` 后缀，但 `buff_Ether_AttackUp.json`
  这类大小写混合的要确认 `TryGetBuff` 是否接受。探针把加载失败的 id 写进输出的 `missing`
  （最多记 64 条），跑完先看这个数。
- **随游戏版本固定。** 与现有字典导出一样，游戏更新需重跑。表版本号写进快照头部。
- 导出得到的是**机制数据**，不含任何玩家数据，可以安全地随发布版本分发。
