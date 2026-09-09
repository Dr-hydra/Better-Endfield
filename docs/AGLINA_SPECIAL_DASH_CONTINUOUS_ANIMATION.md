# Aglina 特殊冲刺动画持续播放调查结论

> **2026-09-08 v8 实测反馈与 v9 修正：** 用户确认已能循环、道具保留，但粒子消失且接缝明显。22:52 日志显示实际落点约 0.087（约 0.30 秒），并非请求的中段；v8 的回绕确认规则过宽。v9（1.8.0）改为显式归一化 CrossFade 并验证落点范围。资源另确认两套特效自身寿命 2.7 / 3 秒、自动淡出、四个粒子均不循环；现按本次实例延后寿命计时、开启粒子循环，并在退出/回收前恢复原值。构建与离线测试通过，v9 仍待实测。详见文末“v9：实际落点与粒子寿命”。

> **2026-09-08 已落地 v8 实验实现（1.7.0）：** 用户随后授权实现。现已用游戏组件的同状态 CrossFade 实现完整起伏的相位衔接，补齐目标道具隐藏、Perform End 轨道清理和 Flying_Stop 的保持/退出处理；编译、策略测试、接口核对及 DLL 导出检查通过。测试包已更新，**尚未完成游戏内混合/根运动/自然度验证**。当前实现与退出说明见文末“v8 实现与测试包”，此前“未修改运行时代码”均为对应历史轮次记录。

> **2026-09-08 用户要求保留完整起伏后的补充：** 已将检查范围延后至 2.9 秒，没有把 Flying_Stop 当作骨骼检查截止线。后两个明显高度峰值为 **1.6333 秒、2.5667 秒**，中间低点为 2.2667 秒；该段长 **0.9333 秒**，适合继续研究“一次完整起伏”的节奏。两峰高度仅差约 0.087 资源单位，但 Root 参考旋转约差 81°，55 条主体 muscle 首尾差 RMS 约 0.45，**不能直接整姿态回绕**。此前 0.5 秒方案只是中段小幅循环对照，不应替代用户提出的完整起伏目标。详见文末“后两个高点之间的完整起伏”。

> **2026-09-08 循环切入候选：** 离线比较 455 组同侧重叠混合后，建议优先预览 **首次原片播至 1.70 秒，随后用 0.40 秒将原片 1.70–2.10 秒混合到同侧 1.20–1.60 秒**，形成约 0.50 秒重复周期。该参数是便于验证的候选，不是已经确认自然的循环：脚部参考旋转仍有明显差值，左侧源 Transform 轨道 207 还存在未定位骨骼的急转。详见文末“循环切入位置与混合候选”；未修改运行时代码。

> **2026-09-08 逐帧采样与实例入口补充：** 左右原片均已解出 **209 个 60 Hz 采样点，覆盖 3.466667 秒**，数秒时长存在于单条 Clip 内；当前绑定不是左右短片拼接。对 0.4–2.3167 秒范围内、长度至少 0.5 秒的接缝筛选，尚未找到可直接硬回绕的无缝区间。已定位角色组件的 `SetStateOverrideClip`（RVA `0x05F6B15C`）及两条正常退出过渡的名称 `SPdashLtoSprint` / `SPdashRtoSprint`；但 `SetTransitionOverride` 只能改时长、偏移和 Exit Time，不能据此声称已经能禁用退出。详见文末“逐帧数据与实例覆盖入口”。本轮仅研究，没有修改动作模块或替换游戏 DLL。

> **用户纠正后，以本条为准：** 上一轮把道具/特效 Perform 的清理位置过早当作动画保持的完整方案，现已撤回该结论。已从当前 Persistent overlay 导出标准/optNew 两个 Controller 及其真实左右 Clip：状态名为 **`SpDash_L`、`SpDash_R`**，分别绑定 `A_actor_aglina_sprint_dash_sp_l/r`，均长 3.466667 秒且非循环。左右 Clip 在 **2.34 秒发出 Flying_Stop**，随后有落地与普通疾跑脚步事件；正常 Sprint 过渡的 Exit Time 分别为 **0.9000001 / 0.91833866**。因此“等 normalizedTime>=1 再整段重播”会经过收尾段，不能满足持续保持过程动作。主线应改为定位左右 Clip 内可循环的过程区间，之后再配合保留道具/特效及正确退出，详见文末“左右过程动画与收尾段”。

> **2026-09-08 Perform 层结论（动画方案随后修正）：** 已完整解析目标 Perform 的 743 字节本地资源，并核对原生反序列化字段顺序。`startActions`、`loopActions` 为空，实际道具/特效动作全部在 `endActions`，其中 `logicId=11` 在 2.4 秒隐藏 `Weapons/wpn_misc_0013`。原生 `_CheckTrackEnd → _DoExit` 是 **End=5 阶段**的收尾，旧文把它读成 PreStart/Start/Loop 分支有误。ShowObject 定时隐藏与 End 阶段自然清理是道具/特效侧的配套控制点，不能直接阻止该资源进入 End；当时提出的 Animator 整段同侧重播已撤回，不能作为持续过程动作的完整方案。详见文末“资源解码与实现位置”及后续修正。

> 当前进展（2026-09-08）：游戏每次选择左或右特殊动作，并非左右交替。1.1.0 实机日志显示在动画进度约 0.69 时 Perform 消失，而特殊动画仍在播放。1.2.0 新增对目标 Perform `_TickMainFlow → _DoEnd` 自然结束的处理，保留显式中断，并增加动画长度/循环诊断。新版尚待实机验证，当前实现见 [动作模块 README](../native/modules/actions/README.md)。以下为历史调查，原“左右交替”和手动回绕假设已撤回。

> 最新修正：02:33 实机已明确加载 v5，仍直接进入 `Clear`。v5 的 `_TryExit` 日志条件依赖保持条件，不能据此判断 Hook 是否执行。v6（1.5.0）增加诊断；进一步确认 `IsPlayingSpDashPerform` 可能在退出命令前先变 false。v7（1.6.0）在 `_TryExit` 和 `InterruptSpDashPerform` 使用已记录 handle 及特殊 Animator 状态判断，不再依赖该瞬时标志。构建/策略回归通过，效果待实测。

调查对象：`chr_0013_aglina`  
调查时间：2026-09-06  
调查范围：当前客户端资源配置、IL2CPP 类型定义及 `GameAssembly.dll` 中的特殊冲刺调用链。

### 2026-09-08 测试版本核对

01:58–02:00 的测试仍打印 v2 启动标识。用户确认 `artifacts/BetterEndfield-win-x64` 是复制测试包使用的目录；核对该目录的动作 DLL，大小为 180736 字节，修改时间为 2026-09-07 20:39:28，含 v2 标识、不含 v3 标识。不能将这次结果当作 v3 实机失败。已将输出目录中 186368 字节的 v3 动作 DLL 更新到该测试目录，旧文件备份在 `tmp_analysis/actions-before-v3-test-update-20260908`，并核对新标识。没有修改启动路径选择逻辑。

用户观察到后续体力恢复；当前保持条件已覆盖 `Dash` 以及 `Grounded + Sprint + moving`，没有把体力恢复当作退出条件。这次旧版日志也显示已跨过 `Dash → grounded Sprint`，随后才丢失 Perform。v3 效果仍需更新后的实机日志验证。

## 结论

> 2026-09-07 调查阶段补充：用户要求的是「已经触发特殊冲刺后，在本次冲刺结束前持续保持特殊动作」。下面的方案 A 仅是定位退出原因的实验，不能视为完整实现；该阶段尚未修改运行时代码或完成实机验证。具体修正见文末。

Aglina 二段冲刺后的特殊动画不是单纯由普通冲刺计数或 `dashDuration` 控制的。它由两层机制共同组成：

1. Animator 的特殊冲刺状态：

   - `SpDashL`
   - `SpDashR`
   - 对应资源为：
     - `A_actor_aglina_sprint_dash_sp_l`
     - `A_actor_aglina_sprint_dash_sp_r`

2. 特殊冲刺 Perform：

   - Aglina 的 `spDashConfig` 中存在 `CharIntPerform_Aglina_Spdash`；
   - `CharacterAnimationComponent.StartSpDash()` 会调用 `_TryPlaySpDashPerform()` 启动该 Perform；
   - Perform 的结束由 `CharacterSpecialDashBrain.ShouldInterruptSpDash()` 和 `CharacterSpecialDashContext` 的冲刺状态判断共同决定。

因此，想让这段动画“持续”，至少需要处理特殊冲刺 Perform 的退出判断；如果还要求动作不断重复播放，则需要另外处理 `SpDashL/R` 对应 Clip 或 Animator 状态的循环属性。

## 已确认的调用链

```text
二段冲刺输入
    ↓
CharacterAnimationComponent.Dash()
    ↓
CharacterSpecialDashBrain.TryStartSpDash()
    ↓
CharacterAnimationComponent.StartSpDash(index)
    ↓
_TryPlaySpDashPerform(index)
    ↓
CharIntPerform_Aglina_Spdash
    ↓
Dash() 继续调用 CrossFadeToStateFixed，进入 SpDashL / SpDashR
    ↓
CharacterSpecialDashBrain.ShouldInterruptSpDash()
    ↓
Perform / inSpDash / strictInterruptedDash 检查
    ↓
特殊冲刺 Perform 被中断
```

`StartSpDash()` 本身会根据特殊冲刺索引调用 `_TryPlaySpDashPerform()`；这说明 Aglina 的特殊动画不是只靠普通 `Dash()` 播放一次，而是有独立的 Perform 层。

## 退出机制

`ShouldInterruptSpDash()` 的逻辑重点是：

- 当前角色必须仍然拥有并播放特殊冲刺 Perform；
- `CharacterSpecialDashContext.inSpDash` 必须仍然成立；
- `strictInterruptedDash` 不能成立。它与 `_NeedToEvaluate()` 是不同的判断，具体见文末的反汇编核对。

`CharacterSpecialDashContext` 内部维护了以下状态：

- `inSpDash`
- `lastInSpDash`
- `strictInDash`
- `lastStrictInDash`
- `strictInterruptedDash`

这些条件说明特殊冲刺存在逻辑退出路径，但静态调用链不能证明实机中最先发生的是 Perform 中断还是动画自然退出；必须记录退化前后的状态才能确定因果顺序。

## 哪些改法不足够

以下改法不能完整解决问题：

- 只把二段冲刺计数强制保持为 `2`：只能影响后续是否进入特殊冲刺，不能阻止已经启动的特殊 Perform 退出；
- 只修改普通 `dashDuration`：它属于角色动画额外配置，但不是 `ShouldInterruptSpDash()` 的直接退出条件；
- 只 Hook `get_playFirstDashAnim()`：会同时影响普通冲刺、完美闪避等其他调用方，副作用较大。

## 可行方案

### 方案 A：只保持特殊冲刺状态

Hook `CharacterSpecialDashBrain.ShouldInterruptSpDash()`，在以下条件下阻止退出：

- 当前角色是 Aglina；
- 当前特殊 Perform 是 `CharIntPerform_Aglina_Spdash`；
- 当前仍处于 `SpDashL` 或 `SpDashR`。

这样可以让特殊冲刺 Perform 不因为普通的严格冲刺状态变化而结束。

仍应保留以下强制中断：

- 换人；
- 角色死亡或实体释放；
- 受击/硬直等明确的强制打断；
- 动画组件失效。

### 方案 B：让动作本身循环

如果目标不是“保持最终姿势”，而是让冲刺动作持续重复播放，还需要修改：

- `A_actor_aglina_sprint_dash_sp_l` 的 Clip 循环属性；
- `A_actor_aglina_sprint_dash_sp_r` 的 Clip 循环属性；
- 或对应 Animator 状态的循环/退出 Transition。

运行时类型中，`AnimationClipAsyncInfo` 明确包含：

- `length`
- `framerate`
- `isHumanoid`
- `isLooping`

这个字段只能证明资源记录了循环信息，不能证明修改该字段会改变已加载 Clip 或 Animator 的实际播放行为。当前仓库中的动作清单没有为这两个特殊冲刺 Clip 生成可直接编辑的独立元数据，需要继续核对实际播放器和退出 Transition。

## 推荐实施顺序

1. 先实现方案 A，只 Hook 特殊冲刺 Perform 的退出判断；
2. 在游戏内确认特殊冲刺状态是否会因 Clip 播放完而自动跳出；
3. 如果状态仍会跳出，再单独修改 `SpDashL/R` 的循环属性或 Animator Transition；
4. 仅对 Aglina 的 `CharIntPerform_Aglina_Spdash` 生效，避免影响 Lifeng、Pograni 和普通角色。

## 当前结论的边界

已通过静态配置和反汇编确认：

- Aglina 存在独立的 `SpDashL/R` 状态；
- Aglina 存在 `CharIntPerform_Aglina_Spdash`；
- `StartSpDash()` 会启动该 Perform；
- `ShouldInterruptSpDash()` 会根据严格冲刺上下文决定是否中断。

尚未通过实机确认：

- `SpDashL/R` Clip 到末尾时 Animator 是否自行退出；
- 将 Clip 设置为循环后，移动、根运动和碰撞逻辑是否仍然正常；
- 特殊 Perform 的循环是否会重复触发一次性音效或特效事件。

## 相关证据

- [Aglina 动画配置](../research/combat-jsondata/Data/Json/AnimationConfig/anim_cfg_chr_0013_aglina.json)
- [CharacterAnimationComponent 定义](../research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_AI/Gameplay.Beyond.dll.cs#L259688)
- [CharacterSpecialDashContext 定义](../research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_AI/Gameplay.Beyond.dll.cs#L264207)
- [CharacterSpecialDashBrain 定义](../research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_AI/Gameplay.Beyond.dll.cs#L264241)
- [AnimationClipAsyncInfo 定义](../research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_AI/Gameplay.Beyond.dll.cs#L265749)
- [ClipMontageData 定义](../research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_AI/Gameplay.Beyond.dll.cs#L265845)

## 2026-09-07：按「持续至退出冲刺」修正实现目标

### 目标行为

特殊冲刺仍按游戏原有条件触发。触发后，在同一次有效冲刺期间维持特殊动作；若动画需要循环，应只循环适合持续移动的区间。停止冲刺、切换角色、死亡、实体释放或强制打断时立即交还游戏原有控制。下一次冲刺重新按原有条件判断，不继承上次的保持状态。

### 本次核对发现

在当前客户端原生、未被 IFix 替换的分支中：

```text
_NeedToEvaluate() = inDash && !lastInSpDash && inSpDash
strictInterruptedDash = lastStrictInDash && !strictInDash
ShouldInterruptSpDash(charAnimComp) =
    !charAnimComp.IsPlayingSpDashPerform()
    || context == null
    || !context.inSpDash
    || context.strictInterruptedDash
```

`_NeedToEvaluate()` 检测进入特殊冲刺的边沿，不是持续计时器，也不是 `ShouldInterruptSpDash()` 的直接调用目标。核对地址分别为 `0x05FA358C`、`0x05FA35F4`、`0x05FA3178`；地址仅用于本版本研究，不应硬编码进运行时。

`CharacterSpecialDashBrain.Update()` 在 `!context.inDash || !context.inSpDash` 时清空 `m_lastEntry`（`0x02D2AF1B–0x02D2AF39`）。因此，一旦特殊动画退出，仅依赖 `lastEntryName` 或 `SpDashL/R` 的保持条件也可能丢失。需要在特殊冲刺自然触发时记录本次保持状态，并以独立的有效冲刺及强制中断条件结束它，不能把要维持的动画状态同时当作唯一的生命周期依据。

`ShouldInterruptSpDash` 的反汇编把第一个参数直接作为 `CharacterAnimationComponent` 使用，符合静态方法调用形式。实现 Hook 前必须核对元数据中的 static 标志；不能因为方法属于 Brain 类型，就假定存在一个额外的 Brain `this` 参数。

### 完整实现需要覆盖的两层

1. **生命周期**：只对 Aglina 自然触发的目标特殊冲刺保持会话；仍要区分动画退化与真正的冲刺结束，并保留强制打断。不能无条件让 `ShouldInterruptSpDash` 返回 `false`。
2. **动作播放**：确认实际播放源、自然结束及 Transition。若动作播完仍会切回普通冲刺，再在可持续的动作区间循环或调整该状态的自然退出规则。不能每帧调用 `StartSpDash()`，因为它也会尝试启动 Perform；也不能只将动作停在最后一帧当成循环完成。

循环区间及重复播放对音效、特效、根运动的影响尚未确认，不能编造固定循环起止时间。

### 实机验收与定位

在特殊冲刺开始、动画状态变化、原始中断判断变为 true、Perform 停止时记录：当前/下一动画状态、normalizedTime、inDash、inSpDash、strictInDash、strictInterruptedDash、目标 Perform 是否仍在播放。按变化事件记录，避免逐帧刷日志。

- 持续冲刺超过原退化时间后，仍播放特殊动作。
- 停止冲刺及强制打断后正常退出，不残留姿势、特效或保持标志。
- 连续多次进入/退出，左右动作和其他角色均保持正常。
- 循环衔接无明显跳帧，移动速度和碰撞行为不因重播改变。

判读重点：若动画先退出，优先处理播放/Transition；若原始中断判断先成立，优先处理其具体条件；若两者都未变化而 Perform 自行结束，继续检查 Perform 自身时长。完成这一步之前，不能将「只 Hook 退出判断」标记为已解决退化。

## 2026-09-07：可落地的首版实现设计

状态：以下是最初基于本机客户端类型定义和原生分支反汇编的实现设计。当前实现参见动作模块 README；循环区间及游戏内时序仍需验证。IFix 热更可能改变原生分支行为。

### 新确认：Dash 与持续 Sprint 要分开

`CharacterSpecialDashContext.get_inDash()` 检查移动状态机中的 Dash 分支及附加条件；不能把它直接等同于用户理解的整个持续冲刺过程。

`Beyond.Gameplay.Core.PlayerController.get_isMainCharacterSprinting()`（研究 RVA `0x02E2C140`）读取：

```text
mainCharacter.movementComponent.moveGait.gait == Sprint
```

其原生分支取 `MoveGait.gait` 并与值 2 比较，未读取 `m_inSprint` 或冲刺键输入。运行时应调用已解析的 getter，不硬编码对象偏移或枚举值。也可通过目标实体读取其移动步态，但必须先保证该实体仍是当前主控角色。

因此会话的保持条件应覆盖「已自然触发的特殊 Dash 起步，随后持续 Sprint」；Dash 结束且已经转为 Sprint 不能清除会话。反过来，步态也不能单独覆盖受击、死亡等强制动作；这些中断优先于保持判断。

### 新确认：动作与 Perform 的入口确实分离

`Dash()` 的特殊分支先选择 `HASH_SP_DASH_L/R`，调用 `TryStartSpDash(right)`，然后回到共同分支调用 `CrossFadeToStateFixed(...)` 播放所选状态。当前调用使用 layer 0。

`StartSpDash(index)` 的原生分支只检查实体条件与索引，然后调用 `_TryPlaySpDashPerform(index)`；它本身没有 Animator 播放调用。因此反复调用 `StartSpDash` 无法作为动作回绕方案。

已确认的可用接口：

| 用途 | 已存在的接口 | 首版用途 |
| --- | --- | --- |
| 观察自然触发 | `CharacterAnimationComponent.StartSpDash(int)` | 原方法完成后核对目标 Perform，建立待确认会话 |
| 识别实际动作 | `GetCurrentAnimatorStateInfo(int)` / `GetNextAnimatorStateInfo(int)` | 确认 SpDashL/R、方向及实际层；不能仅由 index 推断方向 |
| 检查持续冲刺 | `PlayerController.get_isMainCharacterSprinting()` | 覆盖 Dash 之后的持续 Sprint 生命周期 |
| 设置动作进度 | `ComplexAnimatorComponent.PlayStateNormalized(string, int, float)` | 到达循环尾部时回绕一次 |
| 平滑回绕候选 | `ComplexAnimatorComponent.CrossFadeToStateFixed(...)` | 仅当直接回绕接缝明显时，实验同状态短混合 |
| 保持配套 Perform | `CharacterSpecialDashBrain.ShouldInterruptSpDash(CharacterAnimationComponent)` | 仅在目标会话和独立有效移动条件成立时，过滤动画自然退化所致退出 |
| 清理生命周期 | `CharacterAnimationComponent.ReleaseInternal(bool)`、Brain `_OnMainCharacterChanged(...)` / `Release()` | 原始释放/换人逻辑之前清除会话 |

`PlayStateNormalized`（研究 RVA `0x05F69394`）原生分支在组件有效时直接调用 `Animator.Play(string, layer, normalizedTime)`，不调用 Perform 启动函数。它也没有替模块处理动画缓存或事件：需要验证回绕后游戏状态缓存何时刷新，以及 AnimationEvent、根运动的行为，不能宣称完全没有重播副作用。

### 模块与会话

建议新增独立的 `native/modules/movement/` 模块，沿用 Host 的 `resolve_method` / `resolve_field` / `create_hook` 契约，避免把功能混入相机或登录模型模块。功能初始关闭，并分别保存左右动作的循环区间。

会话建议保存：目标实体身份、动画组件受控引用、生命周期代次、实际状态名/哈希、层、阶段（Pending/Active）、循环次数、回绕待确认标志。初始化时只接受 `chr_0013_aglina` 和 `CharIntPerform_Aglina_Spdash`；可读取当前 `PerformRequest.GetPlayingPerformId()`，不能把 Brain 的 entry 名称当成 Perform ID。

`StartSpDash` 完成时 Animator 可能尚未切到特殊动作，因为 `Dash()` 随后才 CrossFade。因此先进入 Pending，等实际当前/下一状态出现 SpDashL/R 后才进入 Active；Pending 必须有限期，并在真正退出有效移动或被打断时立即失效。下一状态检查用于捕获进入过程，不代表已经可以对混合中的动作回绕。

### 每帧算法

```text
没有 Pending/Active 会话：直接走原游戏逻辑

目标不再是主控 / 实体或组件无效 / 已死亡 / 强制动作正在接管：
    先清除会话，原游戏逻辑正常完成打断

既不在本次特殊 Dash 起步，也不在持续 Sprint：
    清除会话，停止属于本会话的特殊 Perform，交还原动画控制

Pending：
    只有观察到目标特殊状态后才激活，不主动制造第一次特殊冲刺

Active：
    若当前状态是目标 SpDash、没有冲突 Transition，且进度到达 loopEnd：
        PlayStateNormalized(stateName, layer, loopStart)
        设置待确认标志，等后续帧实际进度回落后再允许下一次回绕

    若已经转入已确认的普通 Sprint 状态，且没有其他动作接管：
        可进行一次受控恢复；恢复不成功则结束会话并记诊断
        不能对任意非 SpDash 状态强行重播，不能无限逐帧抢状态
```

首版优先在原生 Tick 周期试验回绕，并观察是否在缓存更新和中断判断前生效。`PreLateTick()` 已确认先调用 `_Internal_CacheAnimatorMemory()`，随后调用特殊冲刺的 `_TryToInterruptSpDashPerform()`（`0x03127AA4`、`0x03127B7A`）；不能在原始 PreLateTick 完成后才补救，并假定中断还没发生。具体选择 Tick 前/后或 PreLateTick 前，需要通过一轮帧序日志确认，不能根据函数名决定。

### 如何选择循环区间

将左右动作分别记录一个自然播放周期，观察姿势、normalizedTime、开始退化的时间和事件位置。`loopStart` 选在起步过渡之后，`loopEnd` 选在自然退出之前，并确保两端姿势能衔接。按实际帧步长留提前量，低帧率和变速时仍要在退出前回绕；不能硬编码「每两秒重播」或直接假定 0.95 就安全。

如果整个动作是单次起步、没有合适的持续区间，则需要制作/取得适合循环的持续 Clip；进度回绕不能凭空保证动作自然。若需真正改自然退出 Transition，游戏还提供 `SetTransitionOverride(name/hash, duration, offset, exitTime)` 和 `RemoveTransitionOverride(...)`。但需要先取得确切 Transition 标识及原覆盖值，只处理自然到时出口，保留受击/死亡出口，并在退出时恢复原覆盖，不能全局清空覆盖。

### 验证顺序

先让 Aglina 单次特殊冲刺跨过原来的退化时刻，确认动作回绕和配套 Perform 都保持；随后验证停止、攻击、受击、死亡、换人和开关关闭。最后检验左右动作、低帧率、暂停/时间缩放、转向、坡面/碰撞，以及音效特效是否叠加。`brain.IsValid()` 原生分支主要检查配置/组件，不能替代死亡和主控身份判断。

这版设计优先使用「受控的动作进度回绕 + 覆盖 Dash→Sprint 的会话」，不依赖改二进制资源。若实机证明反复进入同状态带来不可接受的事件或接缝，再转向专用循环 Clip 和精确 Transition 覆盖。


## 实机反馈后的控制链修正

首版日志在尚未进入特殊状态时即出现 `special=0, outgoing=1, loops=0`。该过渡发生在 Pending 入场阶段，不应按 Active 退出处理。

继续反汇编确认 `CharacterAnimationBlackboard.OnUpdate()`（`0x0336DA91–0x0336DA9E`）每帧执行 `set_isDashing(moveMode == Dash)`。Dash 结束、角色进入地面 Sprint 时，该动画参数会被清除；这提供了一条比动作回绕更直接的保持入口。版本 1.1.0 只在本次目标特殊冲刺及有效地面 Sprint 条件下延后清除，所有时间与左右过渡仍由原 Animator 控制。是否还有其他独立的退出条件，需要新版实机日志继续确认。


## 2026-09-08：Perform 先于动画末尾结束

五次 1.1.0 记录均在 normalizedTime 0.688–0.691 附近失去 Perform，仍处特殊状态且无退出 Transition。模块随后因归属条件失败撤销黑板保持。这里的 normalizedTime 不是秒数，也不能单独证明 Clip 的循环设置。

反汇编确认 `CharPerformHandleBase._TickMainFlow()` 的 `0x037163F6` 调用 `_DoEnd()`。前置条件包含 `_CheckEnd()`，以及非循环 Perform 的时间轴轨道自然完成；此路径绕过特殊冲刺 Brain 的两个 Hook。`_CheckEnd` 中固定时长比较位于 `0x04E1F546–0x04E1F551`，但尚不能认定该资源本次正是固定时长分支触发，需要运行时计时/命令日志确认。

1.2.0 仅在目标 handle 的主流程、有效保持、Start/Loop 阶段、无中断命令且实体存活时延后 `_DoEnd`，不改时间轴计时。新增 Clear 与显式命令观测，并记录 Clip 长度/循环、归一化进度是否越过 1。左右方向仍由游戏决定。


## 2026-09-08：当前结论（暂停修改）

> 历史阶段记录。以下状态分支已按接续调研修正；“只出现一次进度日志”的解释与建议顺序，以文末的新证据为准。

本轮暂不再修改动作模块、构建 DLL 或复制测试目录。对 v7 的静态调用链和实机日志复核后，当前结论如下。

### 已确认的实际清理路径

v7 日志中的 `Clear` 栈为：

```text
CharPerformMainHandle.Clear(false)
  <- CharPerformHandleBase._TickMainFlow()
  <- CharPerformHandleBase._DoExit()
```

`0x03716479` 是 `_TickMainFlow` 调用 `_DoExit()` 后的返回地址。对应的原生分支是：

```text
Perform state == End (5)
  -> _TryExit(commandData)
  -> _CheckTrackEnd()
  -> true
  -> _DoExit()
  -> Clear(false)
```

这条收尾分支本身没有调用 `_DoEnd()`，但不能据此排除更早已执行 `_DoEnd()`。`natural_end_deferrals=0` 仅说明模块没有成功延后它；本资源在启动阶段就可能通过同步 `Tick(0)` 进入 End，早于 `StartDetour` 保存 handle。

### `_CheckTrackEnd` 的含义

`CharPerformHandleBase._CheckTrackEnd()`（`0x0371A510`）读取 `normalTrack`，然后检查：

1. `NormalTrack.isPlaying`（`0x0371A670`）；
2. `NormalTrack.HasActionPlayingOrWaitPlay()`（`0x0371A3D0`）。

当轨道不再播放，或者没有正在播放/等待播放的动作时，返回结束；`_TickMainFlow` 的 **End 分支**随后直接调用 `_DoExit`。本资源的可见道具和特效正是在 End 阶段建立，不能用“禁止进入 End”替代保持策略。

### 日志与原生行为的对应

实机 v7 已确认加载：

```text
Actions enabled: Aglina SpDashEnd hold and selected-side replay (v7).
Aglina action constants: SpDashEnd=12, ForceExit=2, DashMode=20, GroundedMode=1, SprintGait=2
```

在清理前记录到：

```text
mode=20, gait=2, moving=1, airborne=0, owned=1
Aglina Perform _TryExit: command=0, reason=-1, type=-1, hold=1
```

`command=0` 表示这次 `_TryExit` 没有当前中断命令。原生 `_TryExit` 在没有命令且实体仍有效时返回 false，于是流程继续检查轨道；它不是 `SpDashEnd + ForceExit`，所以 v7 按设计会交给原方法处理，随后由 `_CheckTrackEnd` 导向 `_DoExit`。

同一轮日志没有出现 `suppressed InterruptSpDashPerform`、`Perform _DoEnd: defer=1` 或同侧重播记录，最终仍在约两秒后清理。由此可以排除“v7 只是枚举值不匹配”这一解释，当前未覆盖的入口是 `_CheckTrackEnd`/`_DoExit` 或更早的轨道结束状态。

### 动画和 Perform 是两个生命周期

`CharacterSpecialDashBrain.TryStartSpDash()` 只负责选择条件、调用 `CharacterAnimationComponent.StartSpDash(index)`，随后 `Dash()` 在 layer 0 上 `CrossFadeToStateFixed` 到 `SpDashL/R`。`StartSpDash` 自身不负责循环 Animator。

因此这里同时存在两个独立问题：

- `SpDashL/R` 是非循环 Clip（实机 `clip_loop=0`，长度约 3.467 秒，另一侧曾读到约 3.041 秒）；
- `CharIntPerform_Aglina_Spdash` 的 Perform 可能先因普通轨道结束而被 `_DoExit` 清理。

即使把 Animator 状态重播成功，也不能自动延长已经被 `Clear` 的 Perform；反过来，延长 Perform 也不能让非循环 Clip 自己无缝循环。

### 当前仍未证实的部分

- 本次资源的 `normalTrack` 是否在约两秒时变为非播放，以及其具体 `trackId`；
- `CharIntPerform_Aglina_Spdash` 的 `m_isLooping`、`m_fixedTime`、`m_totalDuration` 实际运行时值；
- v7 之后只出现一次动画进度采样的原因，是 `PreLateTick` 调用对象/调度变化，还是查询时机处在缓存更新之前；
- 是否存在 IFix 分支或其他外部调用提前让 Perform 进入结束路径。

上述内容是暂停时的调查范围。接续调研进一步查明了资源阶段、定时隐藏与同步启动链，具体实现位置及诊断优先级见下节。

## 2026-09-08 接续调研：资源解码与实现位置

### 1. 证据范围与解析校验

本轮只读取本地客户端、已有实机日志和工作区资源快照。研究对象：

- `E:\Endfield Game\GameAssembly.dll`，与 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/` 的方法定义对照。
- `research/combat-jsondata/Data/Json/CharInteractPerformCfgs/CharIntPerform_Aglina_Spdash.json`，实际是 MemoryPack 二进制，长度 743 字节。
- `%LocalAppData%\BetterEndfield\logs\BetterEndfield.log` 中 02:51:39 加载 v7、02:52:47–02:53:01 的运行记录。

资源解码脚本为 `tmp_analysis/inspect_aglina_perform_20260908.py`，输出 `tmp_analysis/aglina-perform-decoded-20260908.json`，包含各字段的文件字节范围。严格检查成员计数、布尔值、长度、未知类型及文件末尾；本样本消费 **743/743** 字节。没有改写输入资源，也没有计算产物哈希。

字段顺序来自生成的 MemoryPack wrapper，另与原生反序列化核对：

| 类型 | 原生 Deserialize RVA | 成员数 | 校验 |
| --- | --- | --- | --- |
| CharInteractPerformRuntimeCfg | `0x042082C0` | 27 | 27 个 setter 的调用顺序一致 |
| ObjectShowActData | `0x039985E0` | 18 | bodyType 为内联结构复制，其余 setter 顺序一致 |
| EffectPlayActData | `0x039981C0` | 21 | bodyType 为内联结构复制，其余 setter 顺序一致 |

`BodyType` 的底层类型是 **Byte**，不能按 Int32 读。校验摘要保存在 `tmp_analysis/aglina-schema-verification-20260908.json`。

Action union dispatcher 为 `0x0438D900`，跳转表 RVA `0x0438E1F8`：tag 32 指向 `0x0438D95F`，引用类型槽 `0x0D060B48`，与 ObjectShow 反序列化的实例创建槽一致；tag 25 指向 `0x0438D9A3`，类型槽 `0x0D060988` 与 EffectPlay 一致。因此动作类型不只是根据文件中的字符串猜测。

**限制：**这是本地已提取资源快照的确定结果；本轮没有运行游戏，也没有重新导出当前 VFS overlay 中该项的运行时对象。上线实现前仍应核对 live cfg、IFix 状态和解析入口，避免把本地快照直接当成运行时最终配置。

### 2. 目标资源的实际内容

| 字段 | 解码值 |
| --- | --- |
| `performType` | 0；原生准备流程仅在值为 1 时设置 `m_isLooping=true` |
| `fixedTime` | -1.0 |
| `usePreStartActions` | false |
| `preStartActions` / `startActions` / `loopActions` | 均为空 |
| `endActions` | 4 项，见下表 |
| `tmpObjects[0].tmpObjectPath` | `Weapons/wpn_misc_0013` |
| `tmpObjects[0].performEndNotDestroy` | false |

| endActions 项 | 类型 | logicId | delay（秒） | 内容 |
| --- | --- | --- | --- | --- |
| 0 | ObjectShowActData | 4 | 0 | 显示临时物体 0，挂到角色 0，mountPoint=63 |
| 1 | ObjectShowActData | 11 | 2.400000095 | 隐藏临时物体 0，show=false |
| 2 | EffectPlayActData | 15 | 0 | 显示 effect 0：`P_fxbat_aglina_sprint_dash_sp_01` |
| 3 | EffectPlayActData | 16 | 0 | 显示 effect 1：`P_fxbat_aglina_sprint_dash_sp_02` |

4 个动作的 `duration` 都是 -1、`useEvent=false`、`devOnly=false`。不能把 `duration=-1` 单独解释为 Animator 或 Perform 无限循环。

关键文件偏移（半开区间，十进制）：`endActions=[235,573)`，定时隐藏的 delay 为 `[333,337)`，`fixedTime=[573,577)`，`loopActions=[646,650)`，`performType=[654,658)`，`startActions=[662,666)`。这是定位用信息，不是直接二进制改写建议。

`2.4 / 3.467 ≈ 0.692`，与旧实机在归一化进度约 0.69 时失去 Perform 的现象吻合。这是资源和日志之间的强对应；精确到运行帧的先后关系仍需新诊断确认。

### 3. 为什么 `_DoEnd` 很可能早于当前会话记录

本地未走 IFix 的原生链如下：

```text
StartSpDash
  -> _TryPlaySpDashPerform
  -> _DoPlaySpDashPerform
  -> _BeginPerform
  -> PlayInteractPerformOnSquadMember / PlayInteractPerformOnEntity
  -> Prepare / _PrepareAndPlayCor.MoveNext
  -> _DoPlay -> _DoStart
  -> _SwitchState(PreStart=2, Start=3)
  -> _DoNormalActions(startActions=[])
  -> Tick(0)                         // 同步、可重入
  -> _TickMainFlow(Start)
  -> _CheckEnd() == true             // fixedTime 无正值，start/loop 均空
  -> _DoEnd -> _SwitchState(Loop=4, End=5)
  -> _DoNormalActions(endActions)    // 建立道具、特效和 2.4 秒的隐藏动作
```

注意 `_SwitchState` 的 oldState 参数在 `_DoEnd` 中硬编码为 4，不能把它当成实际上一帧必定是 Loop 的证据。诊断必须同时读取进入函数前的 `get_curState()`。

关键地址：

- `_PrepareAndPlayCor.MoveNext`：`0x0371EF00`；检查角色就绪后在 `0x0371F0B1` 调用 `_DoPlay`。
- `_DoPlay`：`0x038EB1A0`；`usePreStartActions=false` 时在 `0x038EB24C` 尾调用 `_DoStart`。
- `_DoNormalActions`：`0x0371AE90`；枚举完成后的 `0x0371B24F` 明确调用 `Tick(0)`，包括空列表。
- `_CheckEnd`：`0x0371B670`；startActions 与 loopActions 都为空时，在 `0x0371B6FB` 返回 true，不能仅靠设置 `m_isLooping=true` 消除这一条件。
- `_DoEnd`：`0x0454C6B0`；清理旧轨道后切换状态并提交 endActions。

当前 `native/modules/actions/module.cpp:424` 的 `StartDetour` 首先调用 `g_start`，返回后才填写 `g_session.handle`。如果角色与资源已就绪，上述同步链可能在保存 handle 前走完；`EndDetour` 因尚无匹配会话而直接放行，也不打印目标日志。若准备协程尚未就绪，具体时序会不同，应通过诊断区分。

因此现有日志缺少 `_DoEnd` 记录，不能证明该方法没有执行或 Hook 未安装；更不能据此追加一个无条件禁止 End 的补丁。对这个资源来说，End 是实际内容所在的阶段。

### 4. 纠正状态分支与日志解释

完整 `_TickMainFlow` 的 `0x03716356` 执行 `ecx = curState - 2`，随后比较 `ecx == 3`，即 **curState == 5**。EndState 的 `get_stateId`（`0x04656470`）在 `0x0465648D` 返回 5，数值并非只按枚举声明顺序推断。

| 实际状态 | 原生行为 |
| --- | --- |
| PreStart=2 | `_TryExit`、`_CheckEnd`；轨道完成则 `_DoStart` |
| Start=3 | `_TryExit`、`_CheckEnd`；轨道完成则 `DoLoop(true)` |
| Loop=4 | `_TryExit`、`_CheckEnd`；非循环且轨道不播放时 `_DoEnd` |
| End=5 | `_TryExit`；`_CheckTrackEnd` 为 true 时 `_DoExit` |

主流程调用 `_DoExit` 的返回地址 `0x03716479` 与 v7 Clear 栈一致。在这条未走 IFix 的原生路径中，清理时已经处于 End。

`module.cpp:512` 将 normalizedTime 截为整数 bucket，只在 bucket 变化时打印进度。0.02 到 0.69 均为 bucket 0；在 2.4 秒左右清理之前只有一次进度日志完全符合当前日志设计。**撤回“仅一次采样说明 PreLateTick 没持续执行”的推断**，也暂不把缓存时机或调度变化作为主线。

`_TryExit` 同样有节流：首次命令为空的日志不是清理帧的完整快照。后续诊断应绑定阶段变化与临界时间，不依靠“有没有打印”猜测是否调用。

### 5. 当时的候选实现位置与边界（部分撤回，尚未实现）

> 本节的 ShowObject / 轨道清理证据仍成立，但属于配套生命周期问题。把现有整段 Animator 重播当作过程动作循环的方案已撤回，原因和新证据见文末。

优先考虑当前模块内的三个协同位置，保留原生初始入场和道具创建：

| 职责 | 位置 | 限定条件与原因 |
| --- | --- | --- |
| 保留冲刺道具 | `CharPerformHandleBase.ShowObject(ObjectShowActData)`，RVA `0x03A0A2B0` | 仅同一主控 Aglina / 目标 Perform handle / 有效保持会话，精确匹配本资源 actorIndex=0、show=false 的定时隐藏动作。live cfg 验证 logicId=11 与约 2.4 秒 delay 后再使用，不能全局吞掉隐藏动作。 |
| 保留 Perform 生命周期 | `_CheckTrackEnd()`，RVA `0x0371A510` | 先保留原结果；仅目标 handle 在其自身主流程、**End 阶段**、角色有效且仍在有效 Dash/地面 Sprint、无待执行退出命令时延后自然清理。配置关闭、攻击/受击接管、换人、腾空、死亡及失去归属时放行并走现有退出流程。 |
| 重复特殊动画 | 现有 `PreLateDetour` / `ShouldReplaySelected` | 在 handle、道具和状态仍有效的前提下，按本次游戏选择的左/右状态重播。首次过 1 及普通移动过渡的回绕效果仍需实机确认，不能据静态证据声称无缝。 |

选择 `ShowObject` 入口而不是事后重新激活物体，是因为 `show=false` 的冷分支 `0x04E8DB32` 不仅关闭物体，还在 `0x04E8DB86` 调用 `_ClearObjectParent`，随后 `0x04E8DB9C` 执行 `SetSelfLogicActive(false)`。必须在破坏挂接关系之前处理。

定时隐藏与轨道清理必须一起考虑：只延后 `_CheckTrackEnd`，道具仍会先被隐藏；只抑制隐藏，随后的 Clear 仍会移除道具和特效。仅重播 Animator 也不会重新创建这些对象。

真正退出时继续交给原生 `_DoExit` / `Clear` 回收。基类 Clear（`0x03A0CD20`）包含时间轴清理、`_ClearTmpObjects`（调用点 `0x03A0CE43`）与 `_ClearAllEffects`（`0x03A0CE52`）。因此不能全局拦截 Clear，也不应在 `_DoExit` 的 `_BeforeExit` / `_RecoverEntityState` 已产生副作用后再修复状态。

备选是给**本次 handle 独立配置副本**重排阶段，迁移入场动作并明确循环/退出动作；这比两个定点保持入口更改动资源语义，且要验证资源共享及回收契约，暂不作为首选。直接改共享 cfg、把 fixedTime 改成极大值或只把 m_isLooping 置 true 都不能完整解决当前资源的语义。

### 6. 下一轮最小诊断清单

实现前优先补齐以下观测；本轮没有添加运行时 Hook：

1. `StartSpDash` 原方法调用前/返回后，以及目标 handle 首次 `_DoEnd` / `_SwitchState`：记录实际 curState、目标实体/Perform ID、handle、是否已 armed。早期识别不能只依靠尚未写入的 `m_currentRequest` 或 `g_session.handle`；可用 `_BeginPerform` 的目标 ID/组件调用上下文联合 handle 自身身份。用于确认 End 是否早于 arm。
2. arming 后一次性读取 live cfg：四个动作列表计数、performType、fixedTime、目标隐藏动作字段，以及 `m_isLooping`、`m_endStartTime`、`m_curTime`。
3. `ShowObject` 的目标隐藏动作、`_CheckTrackEnd` 首次返回 true：记录实际 curState、normalTrack ID/播放状态/是否还有动作、m_hasCommand、归属与移动条件；比较隐藏和 Clear 的时序。
4. Animator 每 0.25 秒或 normalizedTime 每 0.1 变化记录一次，避免原整数 bucket 隐藏 0–1 区间内的进度；实机验证保持超过两次 Clip 长度，并覆盖停止、攻击、受击、腾空、换人和关闭配置。

若 live cfg 或调用顺序不同于离线快照，再检查 `_TickMainFlow`（IFix id `0x2D47`）、`_CheckEnd`（`0x2D4B`）、`_DoEnd`（`0x2D4C`）、`_CheckTrackEnd`（`0x2D51`）的 patch 状态及最终解析的入口。反汇编脚本的“最近方法”标注会把远端冷代码误标成 `Task.NotifyDebuggerOfWaitCompletion`；本节对冷分支的归属依据真实跳转源，不采用这个名称作为证据。

结论：本轮已定位资源中的 2.4 秒隐藏动作、End 阶段自然清理与现有 Animator 重播的协作位置。运行时保持策略尚未实现、未构建、未实机验证。

## 2026-09-08 用户纠正后：左右过程动画与收尾段

### 1. 明确区分两个系统

`CharIntPerform_Aglina_Spdash` 的 `endActions` 只证明道具和特效怎样显示/隐藏，**不包含左右骨骼 AnimationClip**。Perform 的 End 状态也不能直接解释成 Animator 的退出动画状态。此前用 2.4 秒的道具隐藏解释整个动作退化，范围过大。

真实左右姿态由 `Dash()` 选择 `HASH_SP_DASH_L/R`，在启动 Perform 后调用 `CrossFadeToStateFixed` 播放。用户指出的“保持过程动作，不能反复播放收尾”是动画侧必须单独解决的问题。

### 2. 从当前 VFS 导出的真实 Controller 与绑定

通过现有 `RefreshEndfieldResourceInputs.extract_overlay` 定点读取，标准和 optNew Controller 均来自 **Persistent** 层：

| Controller | Bundle |
| --- | --- |
| `ac_chr_0013_aglina.controller` | `main/7236e73e93fb5d5090916a07.ab` |
| `ac_chr_0013_aglina_optnew.controller` | `main/fd4ecb775e73de1265fd22c0.ab` |

两个 Controller 的相关状态、Clip 引用与过渡条件一致：

| 状态（真实名称） | Base Layer 状态索引 | Clip 索引 | AnimationClip | Clip PathID |
| --- | --- | --- | --- | --- |
| `SpDash_L` | 37 | 74 | `A_actor_aglina_sprint_dash_sp_l` | -242825731236656247 |
| `SpDash_R` | 38 | 75 | `A_actor_aglina_sprint_dash_sp_r` | -633966152451736911 |

左 Clip 位于 `CAB-7772877a4d28dd736840e8579433319c`，右 Clip 位于 `CAB-7e5ac3233687f1f1cd9130a07eb92de8`。两者都读取到 `m_StartTime=0`、`m_StopTime=3.466667`、`m_SampleRate=60`、`m_LoopTime=false`、`m_LoopBlend=false`，对应 Controller state 的 `m_Loop=false`。

旧文 `SpDashL/R` 是字段名称简写；资源中的状态含下划线。真实字符串的 CRC32 与运行日志的状态 hash 对应：左 528430122，右有符号 -445642423。这里的 CRC32 仅用于还原 Unity 标识，不是产物完整性哈希。

### 3. 左右 Clip 的事件已经暴露收尾时间段

两条 Clip 的相关事件时刻相同：

| Clip 时间（秒） | 事件 | 对循环方案的约束 |
| --- | --- | --- |
| 0.06933333 | jump 衣物/挂件声音 | 从 0 重播会重复入场音效 |
| 0.09510794 | `TriggerVoice(action_dash_sp)` | 从 0 重播会再次触发语音 |
| 0.13866666 | `Aglina_SprintDashSp_Flying_Play` | 飞行声音开始 |
| 2.3400002 | `Aglina_SprintDashSp_Flying_Stop` / falling_stop | 收尾信号早于整条 Clip 末尾 |
| 2.6346667 | `player_fol_cloth_land_lit` | 落地相关声音 |
| 2.8149333 | `OnCustomFootStep(player_fol_fs_land_lit)` | 落地脚步 |
| 3.1130667 / 3.4112 | `OnCustomFootStep(player_fol_fs_sprint)` | 普通疾跑脚步 |

事件表支持这两条 Clip 带有入场、飞行过程和收尾安排；**不能只凭事件时刻确定骨骼姿态循环的精确起止帧**，还需要姿态和根运动采样。2.34 秒约为 normalizedTime 0.675，2.4 秒约为 0.692，两者都早于当前回绕阈值 1。

本轮还重新读取了当前 overlay 的 Perform 文件，与上一轮 743 字节快照逐字节相等。因此旧资源解析本身没有失效，错误在于把这一路资源控制当成左右过程动画的充分解释。

### 4. 普通 Sprint 过渡有独立退出时间

Controller 中两侧到普通 Sprint 的 Transition 均要求：

- `isAccelerating`：If（mode=1）。
- `desiredGait > 1.5`：Greater（mode=3）。
- `m_HasExitTime=true`。

| 过渡 | Exit Time（归一化） | 过渡时长（秒） | 目标时间偏移 |
| --- | --- | --- | --- |
| `SpDash_L -> Sprint` | 0.9000001 | 0.33774567 | 0.3555173 |
| `SpDash_R -> Sprint` | 0.91833866 | 0.2847669 | 0 |

这两条过渡没有 `isDashing` 条件，所以仅保持该黑板参数不能阻止这两个出口。腾空、减速、碰撞和停止还有独立过渡，不能整体关闭。

当前 `ShouldReplaySelected` 等到 `time>=1` 或已经出现向普通移动的 Transition 才回绕；对于上述资源，飞行停止/落地相关事件已在此之前发生。即使把 Perform 永久保留，这个动画策略也依然会包含收尾、再入场和重复语音的风险。

### 5. 修正后的调研与实现方向

1. **优先定位左右 Clip 的过程区间**：分别采样两侧的根位移、朝向、主要关节姿态及挂点，寻找能够接续的过程帧；不能把 2.34 秒音效时刻当成自动确认的循环终点，也不能把 0.138 秒当成循环起点。
2. 将“自然入场一次 → 所选侧过程区间循环 → 真正退出时允许收尾”作为动画目标。整条 Clip 的简单 loop flag 或 `Play(hash,0,0)` 都不是当前已经验证的答案。
3. `PreLateDetour` 仍是候选的观察/控制位置，但回绕策略须改为有依据的过程区间与事件处理；需要同时验证引擎缓存、混合和根运动是否接受这种回绕。也可考虑实例级 AnimationClip/Animator 替换，待过程区间明确后比较方案。
4. 道具隐藏、Perform 收尾和飞行音效作为配套控制，按本次冲刺的身份与生命周期处理。用户退出冲刺时仍需落地/收尾、停止循环声音并回收道具；不能把这部分全局屏蔽。

本轮没有改动作模块，也没有发布测试 DLL。已从“仅推测 Perform 生命周期”推进到 Controller、左右实际 Clip 和 Transition 的资源证据；精确的过程循环区间与视觉衔接尚未确定。

### 6. 可复核产物与读取方式

- `tmp_analysis/extract_aglina_animation_inputs.py`：从当前 overlay 定点导出 Controller、Perform 及 Controller 依赖。
- `tmp_analysis/AnimatorProbe/`：独立离线读取器，通过 bundle 自带 TypeTree 解码，不加载游戏进程。
- `tmp_analysis/aglina-controller-raw/AC_chr_0013_aglina-spdash-summary.json` 与 `AC_chr_0013_aglina_optNew-spdash-summary.json`：状态、Clip PPtr 与全部出口条件。
- `tmp_analysis/aglina-clips-summary-20260908.json`：两侧 Clip 长度、循环标志和完整事件表。
- `tmp_analysis/aglina-controller-raw/*.tree.json` / `*.tree.txt`：Controller 和两条目标 Clip 的原始结构输出。

现有 AnimeStudio AnimatorController 硬编码读取器将此版本的 `m_TOSData` 按旧字典格式读取，报 duplicate key=11。本轮改用资源自带 TypeTree，两个 Controller 均有 405 个 TypeTree 节点、两个 Clip 均有 587 个节点，完整解码时没有字节消费不匹配告警。该绕行只在独立研究读取器中完成，没有修改原工具或游戏代码。

## 2026-09-08 继续定位：逐帧数据与实例覆盖入口

### 1. 游戏内数秒动作的来源

左右两条 Clip 本身各长 **3.466667 秒**。本轮从资源中的 ACL 压缩缓冲实际解出每条 **209 个采样点、60 Hz**，即 208 个时间间隔。结合 Controller 每侧状态各绑定一条 Clip 的证据，数秒动作由单条长片提供，没有看到依靠左右短片交替拼成数秒的结构。

原片内已经安排了入场、特殊冲刺过程和收尾：2.34 秒出现 Flying_Stop，随后有落地、普通疾跑脚步事件；末段再经约 0.28–0.34 秒的 Controller 过渡混合到普通 Sprint。这里存在的是末端状态混合。**3.466667 秒是资源时间，不等于已经实测的游戏墙钟时长**，运行时速度、混合和中断可能改变实际观感。事件只能标注相关时刻，不能单独裁定姿态阶段边界。

### 2. 解压方法与数据校验

独立 C++ 探针使用 [ACL v2.1.0 官方解压接口](https://github.com/nfrechette/acl/blob/v2.1.0/docs/decompressing_a_track_list.md)。资源序列化版本 10 对应 ACL 2.1；按每帧 `frame / 60`、nearest 采样解压。构建的只是离线读取程序，没有构建游戏动作模块。

| 数据 | 左 | 右 |
| --- | --- | --- |
| Transform 轨道数 | 321 | 320 |
| RootMotion 标量轨道数 | 28 | 28 |
| Float 标量轨道数 | 151 | 151 |
| 每轨采样点数 | 209 | 209 |
| 采样率 | 60 Hz | 60 Hz |

已验证缓冲结构、维度和所有样本有限性，旋转四元数范数接近 1（本地输出误差在约 2e-7 内，具体见 metadata）；RootMotion 的 28 条轨道与 Float 的前 28 条逐值一致。未计算产物哈希。

Humanoid bindings 的前 143 项 `attribute=0..142`、`customType=8`。按本地 `MuscleHelper.cs` 对应表，Float 0–6 是 Motion，7–13 是 Root，14–41 是四肢参考变换，**42–96 才是本次比较的 55 条主体 muscle 通道**，97–142 包含手指等通道。不能把前 143 项全部当作 muscle。Root 相对平移使用 Root T 减 Motion T，数值单位仅记作资源单位，不擅自解释成米。

全部 Transform 旋转指标可能包含挂件等轨道，尚未完成逐骨骼名称映射，不能将它直接称作主要关节误差。图中的 Motion Z 也不是实机角色世界坐标。

### 3. 过程接缝筛选结果

筛选帧 24–139（0.4–2.3167 秒），候选区间至少 30 帧（0.5 秒）；每侧比较 3741 对首尾帧。比较主体 muscle 差值、Root 相对平移差值及全部 Transform 四元数夹角。此范围用于初筛，不代表已经认定全过程边界。

以主体 muscle RMS 最小排序，两侧都落在帧 69→99（1.15→1.65 秒）：

| 指标 | 左 | 右 |
| --- | --- | --- |
| 55 muscle 首尾差 RMS | 0.14049 | 0.13189 |
| muscle 最大绝对差 | 0.42419 | 0.42423 |
| Root 相对平移首尾差（资源单位） | 0.49871 | 0.49958 |
| 全部 Transform 旋转首尾差 RMS | 12.14° | 11.40° |
| 全部 Transform 旋转最大差 | 56.08° | 55.96° |

**这不是推荐的循环区间**，只是指定范围内单一指标的最小值。改按全部旋转 RMS 排序，最小值仍约 11.22° / 11.13°；改按 Root 相对平移差排序，虽然可降至约 0.08 资源单位，但主体 muscle 最大差约 1.03。尚未找到多个指标同时接近的可直接硬回绕端点。

因此，不能声称原片中已经找到现成无缝 loop。该筛选也不证明所有区间都无法循环：更短区间、插值切点、混合过渡、轨道修整和制作新的过程 Clip 尚未评估。端点接近仍不足以保证速度连续和视觉无缝。

源曲线的前向 Motion Z 在过程段持续增长，最终约为 26.234 资源单位；整个 Clip 回绕也必须处理根运动，而不只是姿态。左右 `SG_TimeRef` 曲线分别从约 0→0.5、0.5→1，另有 RootMotionWeight、BlockTransition 等参数曲线，这些是资源曲线值，不能直接代替实机最终参数。

图表见 [逐帧时序图](../tmp_analysis/aglina-samples/pose-timing.png)，包括前向 Motion、Root 相对高度、逐帧全部旋转变化；它是源数据分析图，不是角色渲染预览。

### 4. 已定位的角色实例入口

`ComplexAnimatorComponent` 含 `m_animator +0x148`、`m_stateOverride +0x188`、`m_stateOverrideDirty +0x198`、`m_refreshStateOverrideDirty +0x199`。当前 DLL 的入口如下，地址不能跨游戏版本照搬：

| 方法 | RVA | 用途与限制 |
| --- | --- | --- |
| `SetStateOverrideClip(int stateHash, AnimationClip clip)` | `0x05F6B15C` | 经角色组件覆盖指定状态的 Clip |
| `_SetStateOverrideClipInternal(int, AnimationClip)` | `0x05F6D960` | 操作该组件的 RuntimeAnimatorStateOverride |
| `RemoveStateOverrideClip(int)` | `0x05F6A2A4` | 移除指定状态覆盖 |
| `SetTransitionOverride(int transitionNameHash, float duration, float offset, float exitTime)` | `0x05F6B310` | 按过渡名称修改三个数值 |
| `RemoveTransitionOverride(int)` | `0x05F6A3A8` | 移除指定过渡覆盖 |
| `_SyncAnimatorStateOverrideAndClips()` | `0x03231780` | 同步状态覆盖与 Clip 绑定 |
| `PlayStateNormalized` | `0x05F69394` | 最终委托 Animator.Play；存在入口不代表回绕无缝 |
| `_PlayInFixedTimeAndCache` | `0x05F6D3B0` | 游戏组件的播放及缓存路径，回绕方案需核对 |

Clip 内部 setter 在 `0x05F6DAF3` 调用 Unity `Internal_SetClip`（`0x042688A0`），在 `0x05F6DB4D` 调用 `Animator.AddClipsToBindingsNoGrown`，随后在 `0x05F6DB52` 置刷新标志。由此可以研究经角色组件覆盖状态，不必先假设必须全局替换整个 Controller；底层实际共享和回收行为仍须实机验证。

公开 Clip setter 在内部调用未成功时，可能于 `0x05F6B1D8` 将请求加入 `_Internal_AddStateOverrideCommand`。**返回 false 不能一律理解为没有任何后续动作**，需要核对排队、刷新与完成时机；传入空 Clip 则转向移除覆盖。

### 5. 两条普通疾跑过渡的准确标识与能力边界

Controller 的 `m_TOSData` 字符串与 `m_UserID` 对应得到：

| 过渡名称 | m_UserID（uint32） | 另一个字段 m_ID（不是本 API 参数） |
| --- | --- | --- |
| `SPdashLtoSprint` | 2212299967 | 372894335 |
| `SPdashRtoSprint` | 3890191708 | 3937994162 |

`SetTransitionOverride` 使用的是过渡**名称**标识，对应 Unity `RuntimeAnimatorStateOverride.SetTransition(int transUserNameID, ...)`。不要误传 Transition 的 `m_ID`，也不要误传 `SpDash_L/R` 的状态 hash。调用 int 参数时保留上述 uint32 的 32 位位模式。

此 API 只提供 **duration、offset、exitTime**，未暴露 enabled、HasExitTime 或条件修改。不能把“找到过渡覆盖”写成“已找到禁用该过渡的开关”；极大 Exit Time 也只是一种延迟假设，不能据此保证无限保持。需要继续定位两条普通 Sprint 出口的选择性控制，同时保留停止、腾空、碰撞、攻击等合理退出。

### 6. 目前实现缺口与复核文件

候选方向是“入场一次 → 当前侧经过修整的过程动画 → 真实退出”，经角色组件覆盖状态是一条已定位的入口。仍缺：过程 Clip 的接缝/速度处理、普通 Sprint 过渡的选择性控制、根运动与同步曲线契约、事件与 Perform 生命周期协同、实例覆盖的加载/恢复验证。当前不能据离线定位发布运行时修复。

复核产物：

- `tmp_analysis/AclProbe/main.cpp`、`CMakeLists.txt`：独立解压探针。
- `tmp_analysis/analyze_aglina_pose_samples.py`：解压、维度/有限性校验、接缝筛选与图表生成。
- `tmp_analysis/aglina-samples/{left,right}-metadata.json`：采样率、帧数、轨道数和数值校验。
- `tmp_analysis/aglina-samples/pose-analysis.json`、`pose-timing.png`：筛选结果、轨迹与时序图。
- `tmp_analysis/aglina-samples/{left,right}-{Transform,RootMotion,Float}.f32`：帧优先原始浮点样本，维度见 metadata；Transform 每轨为四元数 4、平移 3、缩放 3。
- `tmp_analysis/aglina-instance-override-20260908.txt`：角色实例覆盖相关反汇编。
- `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/Gameplay.Beyond.dll.cs`：组件字段与方法签名；`UnityEngine.AnimationModule.dll.cs`：RuntimeAnimatorStateOverride 接口。

本轮验证为离线资源与原生代码检查；未修改 `native/modules/actions`，未运行游戏内姿态预览或循环测试，未构建/复制游戏 DLL。

## 2026-09-08 循环切入位置与混合候选

### 1. 优先预览的时间安排

当前更值得预览的切入点是**第一次播放约 1.70 秒时开始接管**，保留此前自然入场。左右使用相同时间安排，但始终使用本次游戏选择的那一侧 Clip，不能混成左右交替。

| 阶段 | 源片时间/帧（60 Hz，帧号从 0 开始） | 操作 |
| --- | --- | --- |
| 首次入场 | 0–1.70 秒，帧 0–102 | 原样播放 |
| 首次重叠混合 | 出源 1.70–2.10 秒，帧 102–126；入源 1.20–1.60 秒，帧 72–96 | 两路同侧源片同时前进，用 0.40 秒将权重从前者转到后者 |
| 混合完成 | 入源已到 1.60 秒，帧 96 | 从这个位置继续，**不是完成后再跳回 1.20 秒** |
| 下一次混合 | 再播到源时间 1.70 秒 | 重复上述重叠混合，形成约 0.50 秒周期 |

有效周期是 `2.10 - 1.20 - 0.40 = 0.50` 秒；原片总时间窗 1.20–2.10 秒虽然有 0.90 秒，重叠部分不能重复计入周期。对应原始 3.466667 秒 Clip 的归一化时间，首次触发约 0.49038、目标起点约 0.34615、混合完成后目标约 0.46154。实际控制必须考虑实时速度和状态混合，不能把这些离线秒数当作固定墙钟定时器。

在当前两侧完整事件表中，**1.20–2.10 秒均没有 Clip 事件**；它避开了入场语音和 2.34 秒 Flying_Stop。但 Perform 的独立定时隐藏仍然存在，所以选对动画时间窗不能代替生命周期处理。

### 2. 为什么优先从中段接管

按 55 条主体 muscle 的逐帧变化估算，1.1–1.7 秒区间比 0.5–0.8 秒及 2.0–2.3 秒平缓，躯干尤其稳定；Root 相对高度在约 1.6 秒附近达到过程段高点。围绕这个高点衔接，能保留过程姿态并减小反复入场/落地的感觉。

这不是说中段静止：Root 相对高度、腿部及参考旋转仍持续改变。仅硬跳某个时间点不够；上一轮的最小端点 muscle 差也不能直接作为本轮混合起点。

本轮在源帧 36–138 范围内比较 455 组候选，分别固定重复周期为 0.3、0.5、0.8、1.2 秒，混合时间为 0.2、0.3、0.4 秒，且混合不能长于重复周期。分别计算左右两侧，以较差一侧的结果筛选，避免只照顾一侧。

离线混合权重使用 `w(u)=6u^5-15u^4+10u^3`。它的首尾权重一、二阶导数为零，在源曲线本身足够连续时可避免权重突然变化带来的接缝；**这不保证混合中途没有较大的加速度，也不等于游戏 Animator 默认 CrossFade 的权重曲线**。

排序指标为混合额外引入的速度项 `w'(目标姿态-源姿态)`：分别计算 muscle 和 Root 相对平移的 RMS，再除以各自过程段源速度 RMS，最后相加。它是透明的离线筛选规则，不是经视觉标定的自然度分数。0.5 秒周期、0.4 秒混合下，把 Root 项权重从 0.5 改到 1、2，得到的时间窗均为 1.20–2.10 秒，但仍只说明这组有限候选的排序稳定。

### 3. 候选对比

以下均为独立候选，时间窗包含重叠区域；修正速度为左侧主体 muscle 的 RMS，单位是 muscle 值/秒，不是角速度或实际位移速度。

| 源片时间窗 | 混合时长 | 重复周期 | muscle 修正速度 RMS | 合成 Root 相对高度峰谷差（资源单位） | 用途 |
| --- | --- | --- | --- | --- | --- |
| 1.35–1.95 秒 | 0.30 秒 | 0.30 秒 | 0.572 | 0.091 | 小幅短循环备选；周期很短且全周期都在混合，需检查是否有快频抖动或近似定格感 |
| 1.30–2.00 秒 | 0.20 秒 | 0.50 秒 | 1.176 | 0.195 | 短混合对照，修正较急 |
| 1.25–2.05 秒 | 0.30 秒 | 0.50 秒 | 0.788 | 0.195 | 备选；首次在 1.75 秒开始混合 |
| **1.20–2.10 秒** | **0.40 秒** | **0.50 秒** | **0.589** | **0.195** | **优先预览候选** |
| 1.0333–2.2333 秒 | 0.40 秒 | 0.80 秒 | 0.715 | 0.364 | 周期更慢，但起伏更大；可作为观感对照 |

0.5 秒周期的 0.4 秒混合，右侧 muscle 修正速度 RMS 为 0.578，与左侧接近。相比 0.2 秒混合，左侧这个指标约下降一半。这个比较只支持“修正更缓”，不能宣称视觉质量提高一半。

预览优先级是工程判断：先看 0.5 秒周期的两档混合，再比较 0.3 秒的小幅方案。不能仅因为短循环指标更低就自动把最短周期当作最自然。

### 4. 旋转与脚部检查揭示的限制

对上述 0.4 秒混合候选额外使用最短路径 SLERP 检查全部 Transform 旋转。重叠两路的旋转差 RMS 约为左 16.02°、右 15.06°，所以这仍是需要混合修整的过程，绝不是端点本就相同的无缝片段。

Humanoid Root 参考旋转重叠差约 23.2°，左右脚参考旋转差约 47.8–50.9°，手部约 7–10°。这些是 Humanoid 参考通道的比较，不等同于最终渲染脚骨的世界旋转；脚部 IK 求解尚未运行。它们说明只平滑高度和 muscle 曲线还不能完成验证。

左侧全部旋转轨道的合成最大单帧变化约 85.88°，右侧约 8.31°。回查源片，左侧 **Transform 轨道 207 在帧 93→94 已有约 85.61° 变化**，前后数帧也存在急转，不能把这完全归因于循环接缝。该轨道尚未映射到骨骼/挂件名称，也未确认其实际蒙皮或显示影响；不能擅自删掉它，也不能把它认定为主体骨骼动作。

因此目前只能确定“哪里值得先试”，不能据源曲线图确认成品已经自然。下一步视觉检查应重点看脚/小腿是否抽动、身体俯仰及高度是否机械重复，以及左侧轨道 207 对应对象的实际影响。

### 5. 对实现位置的约束

现有 `native/modules/actions/module.cpp` 的 `PreLateDetour` 在 `ShouldReplaySelected` 成立时调用 `Animator.Play(hash, layer, offset=0)`；`dash_policy.h` 仍以进度超过 1 或正在转普通移动为触发。该入口可继续用于角色身份、退出条件和阶段监测，但**不能只把阈值改成 0.49、offset 改成 0.346 就实现上述重叠混合**：Play 是跳转，不会自动让两路同侧时间同时前进。

若采用本候选，应研究独立过程 Clip 或确实支持双路同侧采样的播放结构；游戏组件 `SetStateOverrideClip` / `CrossFadeToStateFixed` 是已有候选入口，但同状态混合、缓存、事件及实际曲线契约尚未验证，不能把标准 CrossFade 名称当成等价实现的证明。

根运动必须与姿态分开处理。候选窗源 Motion Z 平均速度约 7.0584 资源单位/秒；离线姿态混合排除了这个累计平移，使用 Root T 减 Motion T。若把两条绝对 Motion 位置直接按姿态权重混合，目标时间落后 0.5 秒，对应约 3.5292 资源单位的位置差，可能引入错误回拉。实际实现需对齐/累计根运动并核对引擎消费方式，不能直接把这里的差值换成世界位移。

真正退出时停止再次安排循环，并由原生移动/动作接管；是否及如何进入原片收尾仍需与停止、腾空、攻击等具体出口匹配。不能一律强制播放完整尾段，也不能一律屏蔽所有出口。

### 6. 本轮产物与验证范围

- `tmp_analysis/analyze_aglina_loop_blends.py`：可重跑的有限候选筛选、两侧比较、权重敏感性、旋转/参考通道检查及图表生成。
- `tmp_analysis/aglina-samples/loop-blend-analysis.json`：455 组筛选的各周期/混合时长最优候选及详细指标；未保存全部候选表，可由脚本重算。
- [循环候选曲线图](../tmp_analysis/aglina-samples/loop-blend-candidates.png)：展示左侧原始高度与两种候选合成曲线，不是角色渲染或游戏录像。

脚本已运行完成，验证合成周期样本数、四元数有限性/单位长度和 SLERP 两端取值。图表已打开检查。未做完整骨骼蒙皮、IK、引擎混合或实机自然度验证；未修改动作模块、构建/替换游戏 DLL。

## 2026-09-08 后两个高点之间的完整起伏

### 1. 用户纠正与检查范围

用户指出游戏中能观察到两次高点，希望以一次完整起伏作为循环。最初本轮按低点到低点检查 0.65–2.2667 秒，没有比较后两个高点，范围不够；现已延伸读取至 2.9 秒，并单独记录声音事件，不用 2.34 秒 Flying_Stop 自动裁掉后续骨骼数据。

Root Y 减 Motion Y 的局部极值如下，都是资源相对高度，不能直接当作实机世界坐标或肉眼观察的全部运动：

| 特征 | 帧号（0 起） | 源时间 | 左高度 | 右高度 |
| --- | --- | --- | --- | --- |
| 较早入场峰值 | 8 | 0.1333 秒 | 1.6756 | 1.6802 |
| 前低点 | 39 | 0.6500 秒 | 0.6828 | 0.6822 |
| 中段高点 | 98 | **1.6333 秒** | 1.5514 | 1.5515 |
| 中间低点 | 136 | **2.2667 秒** | 1.0174 | 1.0176 |
| 后一个高点 | 154 | **2.5667 秒** | 1.6383 | 1.6375 |
| 后续低点 | 175 | 2.9167 秒 | 0.5736 | 0.5742 |

因此可以明确研究 **1.6333 → 2.2667 → 2.5667 秒，即高→低→高**，共 56 个采样间隔、0.9333 秒。这比此前 0.5 秒的小幅合成循环更贴近“一次完整起伏”的目标。仅凭离线曲线不能确认哪两个峰值与用户肉眼计数一一对应，但本轮已实际覆盖并比较了后一个明显高点。

### 2. 高度比较支持继续研究，姿态比较不支持直接硬切

| 两个高点的首尾比较 | 左 | 右 |
| --- | --- | --- |
| 高度差（资源单位） | 0.08691 | 0.08601 |
| Root 相对平移差的长度 | 0.16508 | 0.16348 |
| 55 条主体 muscle 差 RMS | 0.45758 | 0.45220 |
| muscle 最大绝对差 | 2.14122 | 2.14099 |
| Root 参考旋转夹角 | 81.31° | 81.28° |
| 左/右脚参考旋转夹角 | 32.57° / 46.07° | 46.08° / 32.58° |
| 左/右手参考旋转夹角 | 60.52° / 44.61° | 44.52° / 63.36° |

两端 Motion Q 相同，Root 的约 81° 差异不能用 Motion Q 在这两帧的变化解释；但 Root/手脚均为 Humanoid 源参考通道，仍不能代替蒙皮和 IK 后的实际视觉角度。

两端高度导数都接近零，左约 -0.0484 / +0.0666 资源单位每秒，右约 -0.0506 / +0.0668；这是高点附近的离散中央差分，不是严格零速度。相比低点到低点约 0.335 的高度差，这一段确实更容易讨论高度闭合。**高度相近不代表身体姿态重复**，后一个峰值处已经存在明显转向/姿态差，不能直接调用 Play 从 2.5667 跳回 1.6333。

### 3. 峰值附近挪动切点的对照

保持周期 0.9333 秒，将首尾同时前后移动，左侧结果如下；右侧趋势相同：

| 时间窗 | 高度差 | 高度速度差 | muscle 首尾 RMS |
| --- | --- | --- | --- |
| 1.5000–2.4333 秒 | -0.05859 | +1.90502 | 0.25456 |
| 1.5667–2.5000 秒 | +0.03787 | +1.23705 | 0.31920 |
| **1.6333–2.5667 秒** | **+0.08691** | **+0.11503** | **0.45758** |
| 1.7000–2.6333 秒 | +0.05671 | -1.13817 | 0.55178 |
| 1.7667–2.7000 秒 | -0.05913 | -2.06243 | 0.73407 |

往前挪有助于减小姿态差，但两个时刻的升降速度更不一致；往后挪则姿态差进一步变大。不能只选高度差最小的那一行作为无缝接点。1.50–2.4333 秒还没覆盖第二个实际峰值，不能将其描述成已经保留完整的峰到峰源动作。

### 4. 声音与姿态分开处理

峰到峰区间包含：2.3400002 秒 Flying_Stop、同刻 falling_stop 衣物事件，2.3573334 秒 cloth_dodge。后一个高点之后，2.6346667 秒有 land 衣物事件、2.8149333 秒有落地脚步事件。事件可作为后段用途的辅助证据，**不单独证明 2.34 秒之后每个骨骼帧都不可用于循环**。

若采用峰到峰片段制作过程 loop，需让这些一次性事件服从真实退出，不能每次起伏都重复停止飞行声音；Perform 的 2.4 秒隐藏也需要独立处理。这里只记录所需契约，没有屏蔽任何事件或修改运行时。

### 5. 更新后的方向与另一方案对照

将 **0.9333 秒、完整高→低→高**作为优先研究的节奏模板，分别处理左右 Clip 的高度闭合、Root 旋转和手脚姿态接缝。不能把它理解成“所有轨道整段原样无限重播”。是否保留原始大幅转向、如何混合而不扭手脚，需要对应姿态预览后判断；尚未生成可用 Unity loop Clip。

本轮也保留了最初低点到低点的对照：0.65–2.2667 秒长 1.6167 秒，经 0.3/0.4 秒重叠后周期约为 1.3167/1.2167 秒。但左侧合成垂直峰值速度分别约 2.745/2.622 资源单位每秒，原区间约 1.298，存在加速下沉的风险。它虽能形成大幅起伏，不应未经预览就称为更自然。

此前 0.5 秒方案仍可作为小幅、较高频的对照，不能用它来替代用户现在明确指出的完整起伏。当前最有价值的下一步是看 1.63、2.27、2.57 秒的实际角色姿态，尤其是两峰之间的转向与手脚变化，而不是继续只根据高度曲线缩短循环。

### 6. 产物

- `tmp_analysis/analyze_aglina_late_peaks.py`、`tmp_analysis/aglina-samples/late-peak-analysis.json`：两个后峰、邻近切点、首尾姿态/参考旋转及事件检查。
- [后两个高点的分析图](../tmp_analysis/aglina-samples/late-peak-candidate.png)：完整高度曲线、后段相对第一高点的 muscle 差及峰到峰片段。
- `tmp_analysis/analyze_aglina_full_bob.py`、`tmp_analysis/aglina-samples/full-bob-analysis.json`、`full-bob-candidates.png`：前低点到后低点的重叠混合对照。

两份脚本均已运行并检查图表，本轮仅研究与记录，未修改动作模块或构建/替换游戏 DLL。

## 2026-09-08 v8 实现与测试包

### 实现的起伏相位

本版为 1.7.0 / v8 experimental，修改范围是动作模块及其测试、依赖清单和文档。没有生成新 AnimationClip，也没有改共享 Controller。

保持首次原生入场；在源帧 **136（2.2667 秒）**开始 18 帧约 **0.3 秒**同侧混合，目标从源帧 **80（1.3333 秒）**前进至 **98（1.6333 秒）**，同时出源从 136 前进至 **154（2.5667 秒）**。这样两路的上升末端在混合结束时对齐，下一轮继续从第一个高点附近下降；正常速度下重复周期为 **56 帧 / 0.9333 秒**。

此处目标起点比第一个高点早 0.3 秒，不能误写成从 1.6333 秒开始混合，否则混合结束时目标已离开高点。实际触发使用源帧归一化比例，以实时 Animator state length 换算固定时间参数，并补偿少量帧越界。游戏时间缩放仍须实测。

通过 `ComplexAnimatorComponent.CrossFadeToStateFixed` 及 `_CrossFadeInFixedTimeAndCache` 路径提交，选用游戏的 `HermiteCubic` 与 `CurrentThenNext`；没有用 Animator.Play 硬跳。该游戏混合不是研究脚本的五次权重函数，且不会自动消除两个高点原有的姿态差异。目的在于提供可测候选，不能声称已经修出无缝资源。

每次提交后等待实际转场/时间回落；等待期间不重复提交。同状态混合未被接受或长时间停滞、进度已经迟到、身份丢失或出现其他出口时，撤销保持并交回原生流程。运行时不修改本次左右选择。

### 配套保持与退出

- `_DoEnd` Hook 已移除：目标资源必须正常进入 End 才能创建其道具和特效。
- `_CheckTrackEnd` 仅对同一 handle、其自身主流程、End 阶段、角色存活且仍满足保持、无待处理显式命令时延后自然完成。
- `ShowObject` 仅拦截当前 handle 的 `logicId=11 / actorIndex=0 / show=false / delay≈2.4`，在解除挂接前延后隐藏，其他显示操作照常执行。
- `SpDashEnd + ForceExit` 自动命令在有效保持中不再入队；保留 `_TryExit` 对此前已入队自动命令的处理。其他显式命令撤销会话并传递原方法。
- 归属判断读取具体 PerformRequest handle，不再在 PreLate 保持中依赖可提前变 false 的 `IsPlayingSpDashPerform`；开始会话时仍校验角色和 Perform ID。
- 对目标角色的 Flying_Stop 进行定点延后，涵盖普通 AnimatorMono uint 入口及快速音效使用的 AudioManager Entity/uint 入口。只有游戏线程、当前有效会话、无回调的目标请求参与延后；有回调请求和其他声音照常处理。退出时补发已延后的 Flying_Stop。实体释放/工作线程关闭时不向已释放实体强行发音效。
- 停止移动、攻击/受击等显式接管、腾空、换人、关闭配置、释放或混合失败时不再安排下一轮，恢复最后原生请求的 isDashing，允许原生 Interrupt/Clear 清理道具和特效。正常 Sprint 过渡若已经发生，也不会拉回特殊动画。

**“自然回退”的含义：** 原版分别在约 3.12 / 3.18 秒开始向普通 Sprint 混合，不是等完整 3.467 秒播放完。本版保持期间不断安排中段下一轮；取消保持后不再干预后续动画出口。停止/攻击可以立即接管，不强制等尾段；仅关闭配置且仍在跑时，可继续走原有片段的自然出口。既不是倒放，也没有额外强制重播一遍完整收尾。

### 验证与交付

- `BetterEndfield.Actions` 和 `BetterEndfield.ActionsTests` 的 Release 构建通过。
- 策略回归覆盖 100 次循环调度、两峰相位/周期、首次原生混合、请求确认、停滞超时、帧越界、其他过渡优先、无效输入、End 自然清理条件和定点隐藏条件。
- `tmp_analysis/verify_aglina_v8_contracts.py` 对照当前本地 dump，核对 45 个方法签名或属性访问器存在性；属性访问器的完整运行时类型契约仍由宿主解析器验证。结果为 `tmp_analysis/aglina-v8-method-contracts.json`。
- DLL 可加载并导出 `betterendfield.actions / 1.7.0 / ABI 1`，目标测试包与构建输出逐字节一致；未计算产物哈希。
- 音频依赖 `Audio.Beyond.dll` 已加入动作模块 manifest。构建单个动作目标没有刷新共享 staging manifest，交付时已从源文件同步并核对依赖行。

交付文件：`artifacts/BetterEndfield-win-x64/modules/BetterEndfield.Actions.dll`（196096 字节）及同目录 `betterendfield.actions.module.ini`。旧 DLL/manifest 保存在 `tmp_analysis/actions-before-v8-test-update-20260908-180414`。

运行日志的加载标识应为 `Actions enabled: Aglina peak-to-peak bob blend and End-track hold (v8 experimental).`；循环记录为 `Aglina v8 same-side bob blend`。重启游戏后使用现有动作模块开关测试：持续至少 10 秒，再分别停止、攻击、腾空、换人和关闭开关。尚未取得本版实机日志，不能将离线测试视作游戏内成功；尤其需要确认自转场是否被引擎接受、根运动是否正常、左侧急转轨道和手脚接缝是否可接受。

## 2026-09-08 v9：实际落点与粒子寿命

### 1. v8 实机证据修正了此前判断

用户确认“武器/道具还在，发光或粒子消失”，循环可持续但存在明显接缝。本轮读取 `%LOCALAPPDATA%/BetterEndfield/logs/BetterEndfield.log` 最近记录，截取为 `tmp_analysis/aglina-v8-latest-log.txt`。

22:52:40 的请求为 `normalized=0.655, target_seconds=1.336, blend_seconds=0.300`，随后当前进度从约 0.704 回到 **0.087**，再经过 0.1、0.2、0.3 等入场进度；后几轮重复同样模式。状态长度 3.467 秒，因此 0.087 对应约 0.30 秒。若中段目标生效，混合完成应约在 normalizedTime **0.47**。这不是已经完成 0.933 秒目标循环的证据，而是旧调度器误将任何时间回落都当作成功。

日志同时有 `deferred owned Perform End track cleanup`，持续多轮后 deferrals 到 790；结合用户道具仍在的观察，不能再把粒子消失归咎于同一个道具隐藏问题。退出处的 Clear 保留原生行为。

### 2. v9 的动画修改

把播放入口替换为 `UnityEngine.Animator.CrossFade(int,float,int,float,float,AnimationBlendStyle,AnimatorTransitionInterruptionSource,bool)`，直接传入归一化目标/时长，避免依赖此前未在实机生效的固定时间组件路径。v8 目标在该路径的哪一层丢失尚未最终确定，不把它归因于已经证实的某个引擎缺陷。

候选相位保持不变：触发 `136/208`，入源目标 `80/208`，混合 `18/208`，周期 `56/208`。使用 HermiteCubic、保留 CurrentThenNext 中断，根运动混合开关为 false。下一状态进度增加到日志中；回绕确认必须落在请求中段的容许范围内，v8 实际出现的 0.087 会被拒绝并撤销保持。不会重复用错误的入场回绕伪装成功。

这一轮优先校正实际落点；没有同时改成更短循环或再次拍定另一组姿态切点。若 v9 落点正确后仍有明显接缝，再用实机画面对照源姿态调整混合窗口。两个高点存在旋转差异这一离线结论仍成立。

### 3. 两套视觉特效有自己的结束计时

已从当前 VFS overlay 定点提取两个 prefab 及其依赖（14 个 bundle），通过 TypeTree 读取 EffectSetting 和 ParticleSystem：

| 特效 | EffectLogicCfg.duration | isLoop | autoFade / endFadeTime |
| --- | --- | --- | --- |
| `P_fxbat_aglina_sprint_dash_sp_01` | 2.7 秒 | 0 | 1 / 0.35 秒 |
| `P_fxbat_aglina_sprint_dash_sp_02` | 3.0 秒 | 0 | 1 / 0.35 秒 |

两套各有两个 ParticleSystem，共四个，全部 `looping=false`。`_02` 一层持续发射粒子，另一层只有一次 count=1 的发光粒子：该粒子 lifetime=3 秒，系统 lengthInSec=5 秒。只开启系统循环，发光层每 5 秒才重发而粒子 3 秒就死亡，会留下约 2 秒空档。

EffectInstance.DurationTick 的非循环分支在 `0x02D57238` 扣减 delta，`0x02D57269` 写回 remainTime；同函数还推进 runningTime 和自动淡出。它不依赖 Perform 的轨道是否已经结束。因此保留 Perform 不会自动保留特效寿命。

### 4. v9 的实例特效保持与恢复

使用本次 Perform 的 `m_effects` 字典键 0/1，反射调用实际泛型 Dictionary 的 ContainsKey/get_Item 和 ObjectPtr.Lock，校验 cachedUid 对应的当前对象，再核对 `_01/_02` 名称。等待异步加载完成后，枚举该效果 GameObject 的 ParticleSystem；保存并固定引用原始 loop/duration，再启用实例循环。对 `_02` 的 5 秒发光系统，将其周期设为 2.5 秒以衔接 3 秒粒子；此项在初次设置时停止/重启该粒子，不重复创建整套效果。

只对这两个已准备好的实例、且会话保持条件成立时，将 `DurationTick` 的 delta 改为 0；不冻结 ParticleSystem 模拟、FollowTick 或全局时间，不改共享 EffectSetting/EffectLogicCfg。因冻结的是原始寿命和淡出计时，保持期间无需不断堆叠新的 EffectInstance。

真正退出或原生 Finish/OnStop 发生时，先撤销实例记录，再停止目标粒子、恢复原始 loop/duration 并释放引用，最后继续原生清理。恢复路径做幂等处理，避免 Finish→OnStop 嵌套重复操作；失效 Unity 对象只释放引用，工作线程关闭时不调用 Unity。原生显式 Finish 不被拦住。

### 5. 验证与产物

- Release 构建通过；`BetterEndfield.ActionsTests` 包含 v8 实际错落点回归、正确中段确认及已有 100 次循环/退出测试。
- 新 `BetterEndfield.ActionsEffectsTests` 直接覆盖生产恢复/Finish 路径，以托管 API 替身验证恢复原值、回收幂等、失效对象、无关效果与关闭时不调用 Unity。它没有模拟粒子渲染。
- 61 个方法/属性访问器与本地 dump 核对通过，结果保存为 `tmp_analysis/aglina-current-method-contracts.json`。运行时泛型 Lock 和粒子实例操作仍须实机验证。
- 特效证据：`tmp_analysis/aglina-v9-effect-summary.json`、`aglina-v9-effect-raw/*.tree.json`、`aglina-v9-effect-bundles.json`；DurationTick 证据为 `aglina-v9-lifetime-branch.txt`。

v9 启动标识：`Actions enabled: Aglina normalized bob blend and continuous particles (v9 experimental).`。重点核对循环落点约 0.47、`effect 0/1 continuous particles prepared: count=2` 和 `held target effect lifetime/auto-fade clock`；持续跑 10 秒后检查停止/攻击时粒子是否清理。当前未取得 v9 游戏内结果，不能声称已消除全部接缝或完成粒子实机验收。

测试包 DLL 与 manifest 已更新至 `artifacts/BetterEndfield-win-x64/modules`；导出版本 1.8.0、ABI 1，DLL 208384 字节，已与构建输出逐字节核对。旧 v8 备份：`tmp_analysis/actions-before-v9-test-update-20260908-231923`。未计算产物哈希。

## 2026-09-09：外部导出及第二版验证

用户反馈 v9 衔接仍不自然，随后选择外部动画修循环的方向，并提出先看完整动作、去掉起手和收尾后保留较长中间段。外部工作已从资源解析推进至可在 Blender 查看、编辑的左右完整时长 FBX。

- 现有 AnimeStudio 对目标游戏 ACL Clip 的直接 ModelConverter 结果为零条轨道，不能把“写出 FBX 成功”视为动画导出成功。本次用原 Avatar、独立解码的 ACL Transform/Humanoid 数据，经本机 Unity 2022.3.62f3 HumanPoseHandler 还原主体，叠加附属骨骼曲线，再复用现有 FBX 写出器。
- 首版 `tmp_analysis/aglina-fbx-validation` 已按用户要求导出后暂停；存在身体/脸部网格缺失、单位缩小 100 倍及待解释的肌肉数值差。用户恢复工作后生成独立第二版 `tmp_analysis/aglina-fbx-validation-v2`，未覆盖首版 Blender 工程。
- 第二版补齐脸部、身体 LOD0 Mesh，每侧共 10 个网格；FBX 单位标记修正为每单位 100 厘米，Blender 中按米查看。两侧均保持 209 采样点、60 FPS、400 条 Transform 轨道。
- 肌肉值最大差 4.5 集中于手腕旋转，前臂旋转存在约 4 的差。重新把 GetHumanPose 的结果应用回骨骼，Unity 的角度检查未检测到局部旋转差，全部层级世界位置最大变化约 0.07 毫米。因此不能把肌肉参数差直接当作几何姿态错误。
- 逐帧 FK 对照验证：Blender 导入骨骼相对离线烘焙骨骼的位置最大差左约 0.0020 毫米、右约 0.0017 毫米；半帧对照线性位置/SLERP 参考最大差约 0.15 毫米，未发现转换引起的大幅跳变。
- 已生成并重新读取检查 `aglina_full_comparison.mp4`（1100×660 / 60 FPS / 209 帧），以及左右单侧 Blender 工程、并排 Blender 工程和十时刻渲染。

这些验证证明离线重建到 FBX/Blender 的转换可靠，不代表已与游戏实时姿态、IK、物理完全等价。预览未加载原版贴图、粒子、道具，并去除了 Motion 行进参考；原始带行进 ACL 样本仍保留，当前文件不是根运动无损回导资源。

全身边界搜索已替代单看高点：在起点 0.20–1.00 秒、终点 1.867–2.733 秒的有界范围内，较长中段候选如源帧 22–125（0.367–2.083 秒）比旧 98–154 高点窗口的全身首尾匹配更接近，但选定关节的位置 RMS 仍有约 23–25 厘米，不能直接裁剪即宣称无缝。此结果只用于人工编辑候选，不是最终循环区间。

第二版说明及完整证据见 `tmp_analysis/aglina-fbx-validation-v2/README.md`。本轮未修 Loop 曲线、未建立游戏外部动画加载器、未修改运行时 DLL；下一阶段是选定较长过程段并修首尾关键帧。

## 2026-09-09：Loop 关键帧粗修验证

用户随后授权“修一下关键帧，不用太精细，做验证用”。已生成 `tmp_analysis/aglina-loop-test-v1`，保留原始和第二版工程。

使用源帧 22–125 的较长中段，每轮 1.716667 秒；首尾各 18 帧用跨边界的连续姿态过渡粗修，位置/缩放三次 Hermite、旋转球面三次 Bezier，并烘焙为每侧 400 条 Transform 轨道。源帧 41–106 的内部姿态保留，附属头发/衣摆轨道也做闭合。输出 104 个采样点，含一个闭合端点，实际周期 103 帧。

FBX 已在 Blender 实际导入：左右端点骨骼位置差和抽查的第 4 轮对应姿态位置差均为 0；与烘焙结果的逐帧位置转换误差小于 0.002 毫米。生成了连续四轮的直接裁剪 / 粗修后对照 MP4，以及左右独立 Loop FBX 和 Blender 工程。接缝前后实际渲染确认粗修版消除了原裁剪在该处的身体/腿部硬跳，但未做手脚接触、衣摆穿插等精修。

此阶段仅为外部关键帧验证，未建立游戏回导器、未改运行时，不能视为游戏内自然度验收。可查看入口：`tmp_analysis/aglina-loop-test-v1/aglina_loop_before_after_4cycles.mp4`；说明和指标见该目录 `README.md`。

## 2026-09-09：v10 回导测试包（Actions 1.9.0）

用户认可粗修视频后授权游戏回导。现已将回导器和动画资源装入 `artifacts/BetterEndfield-win-x64`，尚未启动游戏实测。

回导 Clip 保留原版 208/60 秒时长和起手，在帧 40–143 内使用粗修闭合 Loop，相位对齐到原版帧 40；之后平滑接回原收尾。前進 Motion 和 19 个音效/语音事件已还原。Unity 导入使用原 T Pose/骨骼映射、完整附属骨骼 Mask，并去除 FBX 多出的路径前缀及无关 Root Transform 行进曲线，避免重复移动。

运行时只给当前目标角色安装私有 AnimatorOverrideController，以原控制器为基础替换左右目标 Clip。实际 CurrentAnimatorClipInfo 确认 `BE_Aglina_Return_L/R` 后，采用零时长同状态 CrossFade 在闭合窗口回绕，并验证下一帧落点。原状态机和其他过渡保持原生优先；退出特殊状态后恢复原控制器，配置变化和组件释放也进入清理路径。依赖、Bundle 或播放确认失败时回退 v9；仍沿用 v9 的道具、粒子和 Flying_Stop 生命周期逻辑。

本地验证：75 个接口描述匹配；三组 C++ 测试及 Release 编译通过。资源在原 Avatar 骨架上经 PlayableGraph 播放，首尾位置最大差约 0.0013 毫米，根运动每轮前進约 12.0167 米、侧向误差小于 0.000002 米。测试包 DLL 1.9.0 / ABI 1、动画 Bundle 8,244,849 字节，已与构建产物逐字节比对。旧模块备份为 `tmp_analysis/actions-before-v10-test-update-20260909-131805`。

**实机待验：**构建编辑器是 2022.3.62f3，原资源头是 2021.3.34f5；还需确认游戏可加载资源、私有替换控制器能生效及回绕根运动连续。日志须出现 `external return clip confirmed active` 和 `v10 imported loop wrap` 才能判定测试到外部动画，`using v9 loop` 是降级。完整证据和试用说明见 `tmp_analysis/aglina-return-v1/README.md`。

## 2026-09-09：首次回导失败定位与 v10.1 归档格式修复

13:22:56 的宿主日志证明 Actions 1.9.0 已启用。13:24:19 加载失败并回退 v9，后续全是旧回绕；游戏 Player.log 报 `Unable to read header from archive file`，指向部署的回导包。文件与构建原件逐字节相同，失败在归档读取层，尚未进入 Animator 替换。Player.log 同时确认实际游戏引擎为 2021.3.34f5。

标准 UnityFS 与游戏当前 Endfield VFS 内层包头、块目录编码不同。已实现 `tmp_analysis/AglinaBundleWrap` 转换器，保持内部序列化动画内容不变，使用原游戏包已存在的 encFlags=8 / flags=0x240 无压缩模式。现有 AnimeStudio 解码器总长度字段的 ror64(18) 有误，按实际原包应为 rol64(14)；转换器使用正确逆运算，对 209 个原始包核验长度。现有工具源码未改。

输出专用包 9,764,240 字节，通过独立 VFSFile 解码及嵌入文件逐字节比较，原生样本重封装也通过。Actions 1.9.1 增加 LoadFromFile、左右 LoadAsset、Clip 属性及 pin 的分段日志。编译、三组 C++ 测试、75 项接口描述校验、DLL ABI/版本检查及部署字节比较通过。已更新 artifacts 测试目录与 stage；前一部署备份路径见 `tmp_analysis/aglina-return-v1/v10.1-backup-path.txt`。

本轮没有启动游戏，**仍未确认游戏接受转换后的归档或内部 2022 动画**。完整重启后先观察 `archive accepted by LoadFromFile`，之后仍必须有 `external return clip confirmed active` 和 `v10 imported loop wrap` 才能算实际外部播放。若归档通过但资产加载失败，下一步处理 2022→2021 序列化兼容；保留分层验证，不能仅修改版本字符串宣称兼容。

## 2026-09-10：v10.1 实机崩溃，1.9.2 关闭外部回导

用户报告加载动画时游戏崩溃。保存的 Unity 转储对应 2026-09-09 22:51:37，异常为原生写访问违规 0xC0000005。根据崩溃 DLL 的精确反汇编，Actions+0x6811 位于 LoadExternalAssets 内的 LoadAsset 返回地址，证明 LoadFromFile 和 Bundle pin 已完成，尚未安装私有控制器。宿主异步日志没有保存最后阶段记录，定位以转储和指令为准。

元数据对比确认原版 AnimationClip 比标准 2022 包多出 8 个顶层自定义字段（含 m_aclType、m_AclCompressedBuffer），Muscle/DenseClip 与 AnimationEvent 也有定制结构。优先怀疑内部序列化布局不兼容，尚未确定引擎具体在哪个字段读错。不能将问题简化为版本号或仅认为换 2021 编辑器就能解决。

已将用户配置 external_loop=false、将 artifacts/stage 的试验包重命名隔离，Actions 1.9.2 默认关闭外部导入并恢复 v9 原循环。三组测试、75 项接口描述校验、DLL 版本/ABI、部署字节比对、配置其他节保留验证均通过；未重启游戏验证。托管异常回退不能接住此原生崩溃，不采用捕获访问违规后继续运行的做法。

下一阶段应以原版 Clip 的原生 TypeTree、骨骼绑定和 ACL 结构为模板，把粗修轨道写回并离线解码对比，再制作新的实机候选。当前内部格式写入器尚未实现。证据、转储和恢复验证见 `tmp_analysis/aglina-return-v1/v10.1-crash-20260909-225137/analysis.md`。不要再启用前一候选包。

## 2026-09-10：v11 原生序列化与 ACL 写入器完成，实机待验

用户要求继续修复，现已实现内部格式写入器。以原游戏 e1d1cdbf5f2ff6b856a9ffdb.ab / 7e26489f4608426164b5a007.ab 为模板；新 TypeTree 写入器先对左右 AnimationClip（74）、AssetBundle（142）四个原对象原样写回，均与源字节完全相同。进一步确认 AssetBundle 本身也有定制 m_PathFlags/m_HashContainer，修复涵盖它和动画内部结构，不能只补 Clip 字段。

粗修骨架经原 Avatar 逆投影回 Humanoid 通道，附属 Transform 取粗修结果，三段轨道以原格式 ACL 2.1 / 版本 10、QVV 格式 3/1/1 写回并保持 16 字节对齐。保持原起手/收尾、帧 40–143 窗口、绑定、mask、常量表、事件扩展、ClipTag 等结构，Motion 数值不变。m_TotalSize 按 ACL 缓冲区长度增量更新。原 Avatar 逆投影最大世界位置误差 0.0054 毫米；最终 ACL 解码重放最大世界误差 0.264 毫米、窗口端点差 0.040 毫米。

合并左右对象为独立原生序列化文件，更新对象表/长度/偏移并排序；原生目录中的资源 ID、字符串路径与 preload 引用保持对应，使用独立 CAB/Bundle 身份。最终专用归档 1,107,500 字节；独立 VFS 解码与字节比较通过，两个 Clip 均由原生解析器完整读取，原生 AssetBundle 目录和完整 TypeTree 通过验证。每侧保留 19 个事件，窗口前进约 12.016709 米。

Actions 1.10.0 使用游戏已有 LoadAsset(Int64, Type) 加载新包 aglina_native_return_v2.bundle；增加及时落盘的加载阶段日志。三组测试和 75 项接口描述校验通过，新增覆盖 Int64 参数传递与失败路径。另修复启动器保存整个配置或动作开关时丢弃 external_loop 的问题，保留显式开启/关闭；配置保留测试通过。当前本机恢复启用该候选，新配置默认关闭。

本輪没有启动游戏，仍需实机确认加载、Clip 真实播放、根运动回绕与退出。离线 FK 不包含游戏 IK/布料/粒子；四肢目标保留原目标数据并做同周期连续处理，没有用尚未确认的目标坐标约定强行重建。不能把离线验证当作游戏验收。新文档与报告见 `tmp_analysis/aglina-native-return-v2/README.md`；旧崩溃候选保持隔离。

## 2026-09-10：用户反馈仍失败，转向 EIEM 源码研究

用户明确要求先不要回滚和持续验证。本轮未改配置、DLL 或部署，也未编译/试播，仅研究 EIEM（快照 4f0b13f92cf43d14baf696794aa5b2d285aed26a）。其 MUS4 / DirectVmd 播放通过运行时姿态写入完成，不依赖外部 AnimationClip 的 AssetBundle 加载；可借鉴主线程/IK 阶段调度、角色归属、帧采样与恢复机制。

发现现有导出/回投的恒等 muscle 映射有问题：EIEM 显式处理标准 95→游戏 101 的索引/名称映射，而我们在 AglinaBake / AglinaNativePose 里直接连续复制 95 项。本地原生 DeltaPose 的身体 61 项和双手各 20 项也支持扩展布局。此前微小离线误差只证明同一映射假设下的自洽，不能再作为游戏 muscle 语义正确的证据。此问题需修正，但本轮未把它断言为新一次加载失败的唯一原因。

建议后续转为保留原生动作状态/移动/特效生命周期，在特殊冲刺过程段按循环时钟覆盖视觉姿态。EIEM 会为舞蹈关闭 Animator，我们的玩法场景不能整套照搬。源码证据、现有代码位置、适配边界及下一步方向见 `docs/AGLINA_EIEM_PLAYBACK_RESEARCH.md`。

## 2026-09-10：v12 运行时骨骼姿态原型

用户授权尝试新路线，已实现并部署 Actions 1.11.0。现有 external_loop 开关改为选择姿态文件播放，启用分支不再调用旧 AssetBundle/外部 Controller；旧文件保留，本机配置未改。新数据 aglina_pose_v12.bin 直接来自已批准的骨骼粗修帧，399 个具名骨骼、左右各 104 点、60 Hz、103 帧周期；人物世界根和缩放不写入。此机制不依赖 95/101 的运行时转换，但未宣称最初导出阶段的通道映射问题已修复。

原生状态/移动/特效/中断继续运行，现有 v9 原生回绕保持生命周期；视觉时钟独立。目标角色 TailLateTick 原函数执行后应用位置与四元数，进入与退出采用约 0.12 秒混合。原生未重新写入的常量通道会识别并剔除本模块残留，清理时保留外部新值；按 Animator 根、骨骼归属和代次约束操作，组件释放前清理，关闭线程不调用 Unity。

Release 编译、82 个接口描述、新增文件/时钟/混合/所有权清理检查和三组已有回归测试通过。本轮未启动游戏，TailLateTick 后是否仍有其他组件写回，以及真实帧开销和显示效果，尚待试播。日志须看到 TailLate pose applied 才算新路径实际写入；v9 normalized bob blend 可同时存在，不再代表新视觉路径失败。文档、备份与发布状态见 tmp_analysis/aglina-pose-overlay-v12/README.md。
