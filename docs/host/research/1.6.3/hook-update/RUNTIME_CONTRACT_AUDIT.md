# Windows 1.6.3 运行时字段与战斗公式适配

范围：当前 `G:\Arknights Endfield`，只读定向 IL2CPP 元数据及六个相关函数的局部 x64 反汇编；未建立完整反编译数据库、未计算文件哈希。证据位于 `research/host/1.6.3/Windows/hook-update/targeted-metadata.json` 与同目录 `*.asm.txt`。

## 需要修复的契约

- **协议字段更换生成器。** `Beyond.Network.Proto.dll` 的 `BATTLE_DAMAGE_DETAIL`、`BATTLE_DAMAGE_MODIFIER_ARGS`、`BATTLE_MODIFIER_HANDLE` 改用 ProtoBuf：模块引用的七个旧下划线字段已不存在。现在使用 `__pbn__ModifierArgs` 与 `ProcessorDebugArgs/AttackerModifiers/DefenderModifiers/BuffInstId/Index` 的实际 backing 字段。
- **协议集合布局变化。** 新 `Proto.MapField<TKey,TValue>` 继承 `Dictionary<TKey,TValue>`，不再是在 `+0x10` 包装 Dictionary；通过运行时 `get_Count` 取计数。`Proto.RepeatedField<T>` 使用 `m_items/m_count`；开放泛型的离线字段偏移为零，必须从实际运行时泛型类取偏移。仅使用 1.6.3 命名，无旧生成器回退。
- **属性上限从 101 增至 108。** 新增六种 `*DamageTakenReduction`（101–106）和 `ComboSkillAtbCostAddition`（107），扩大捕获范围。
- **原生枚举不等于会话贡献槽。** 当前 `ModifierType` 有效值为 `0,1,3,4,5,6,7,8`；将 `InstantModifyAttribute.formulaItem` 显式转换为原有内部八槽，防止错位及最后一槽被丢弃。会话格式保持不变。
- **地面检测布局验证误拒绝。** `FindFloorResult.floorHit` 是嵌套 `RaycastHit`，不是两项直接 `Vector3` 字段。验证改为 `floorHit@0x10` 加 `RaycastHit.m_Point@0`、`m_Normal@0xC`（均未装箱偏移）。该布局与旧 dump 相同，属于检查器缺陷，不应归因为新版字段漂移。

## 局部公式证据与处理

| 当前函数 / RVA | 结论 |
|---|---|
| `Attributes.GetAllModifierValues` / `0x08ED2B90` | `0x08ED2C8E` 分配 9 元素数组，4、8 项为乘法恒等值 1；八种贡献排列未变，不扩大数组至 ModifierType.Enum。 |
| `Attributes._GetOtherAttributeFinalScalar` / `0x0534DE80` | 将承伤属性 `4/5/6/7/48/60` 的最终倍率分别乘以 `101/105/104/103/102/106` 的属性值；例如 `0x0534DF76` 为 Natural，`0x0534DFA0` 为 Physical。 |
| `BattleFormula.CalculateDamage` / `0x053540C0` | 主公式保留抗性、承伤、Weakness、Shelter、暴击与燃烧倍率。`0x05354972` 仍测试燃烧掩码 `0x0FD00038`；`0x05354999` 测试 `0x4001C000` 后在 `0x053549BB` 乘攻击方属性 25，即物理异常倍率。 |
| `BattleFormula._GetDamageTypeResistanceValue` / `0x05355070` | 仍读取抗性 94–99 与承伤 4/5/6/7/48/60，新减免已经折入承伤属性，不能在最终公式再乘一次。 |

实现补充六种新减免属性的捕获及 rDPS 来源归因，并补物理异常条件倍率的捕获/归因。总伤害仍来自游戏结算值。新增字段不会被伪装成额外独立伤害区。

## 无需调整的部分与限制

- 当前 PE 为 x64，Host 使用的 29 个 IL2CPP 导出全部存在；UnityPlayer 为 `2021.3.34f5`，Host 入口 ABI 无需改变。
- 五个装备字段的实际偏移仍为 `templateId=0x18`、`weaponLv=0x38`、`refineLv=0x3C`、`breakthroughLv=0x40`、`enhanceAttrLevels=0x38`。
- `AbilitySystem.Modifier.DeltaType.Minus=2`；关键 Modifier 字段继续由元数据动态定位。`DamagePackData.skillCastInfo` 缺失已由当前 `actionEnvironment` 路径处理。
- EIEM 的 FinalIK 主布局已有动态解析；`PrepareFace` 在所需 SkeletalMorph 字段缺失时会关闭对应能力，不能把上游 `SafeOff` 初值直接判定为当前运行时硬编码依赖。
- 静态检查不证明服务器 IFix 覆盖路径或实际地图运行效果；本次未启动游戏做战斗/舞蹈实测。

## 构建

独立目录 `build/game-update-1.6.3/native`，MSVC 19.44 / Release：`BetterEndfieldNext.CombatStats`、`BetterEndfieldNext.Camera` 均成功；Camera 仅出现原有 `shared_ptr` atomic 弃用警告。产物在该目录的 `stage/Release/modules`。`git diff --check` 通过。
