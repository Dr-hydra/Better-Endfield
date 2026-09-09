# 动作模块 1.11.0（v12：过程段骨骼姿态覆盖原型）

## 当前状态

v12 使用 `actions/aglina_pose_v12.bin` 直接采样已批准的骨骼粗修帧，在目标角色 TailLateTick 后覆盖本地位置/旋转。保留原生 Animator、Controller、移动和特效逻辑；不再从启用分支调用 AssetBundle 或安装外部 Controller。沿用 external_loop 作为外部姿态开关，本机当前设置不变，新配置默认 false。

独立周期为 103/60 秒，进入/退出短混合约 0.12 秒；缓存有角色、Animator 根、层级和代次约束，常量通道去除自身上次写入的残留。82 项接口描述、新姿态测试及三组已有回归检查通过，尚未启动游戏。新的成功标志是 `Aglina v12: TailLate pose applied`，原生 v9 回绕日志仍会同时存在。

这版直接播放骨骼帧，不进行运行时 muscle 换算；此前最初导出阶段的 95/101 语义问题不因此被宣布修复。详见 `tmp_analysis/aglina-pose-overlay-v12/README.md`。以下保留历史试验记录。

## v11 原生包候选记录

v11 已将粗修结果编码回原生 Endfield ACL / AnimationClip TypeTree，并保留原生 AssetBundle 的 path flags、hash container 和引用结构。标准 2022 序列化对象不再进入交付包；Unity 2022 仅用于离线 HumanPose 计算。新文件为 `actions/aglina_native_return_v2.bundle`，加载使用当前游戏的 `LoadAsset(Int64, Type)` 和原生资源 ID。

左右动画、原生目录及最终归档的完整离线读回验证通过；ACL 解码后重放的全身最大位置误差约 0.264 毫米，循环端点差约 0.040 毫米。编译、三组测试和 75 项接口描述校验通过。当前本机测试配置已设 `external_loop=true`，新配置默认仍为 false；**游戏尚未重启测试，不能宣称实机崩溃已解决。**

加载阶段额外写入 `actions/aglina_native_return_v2.load.log`，每个关键原生调用前后及时落盘。成功仍需实际 Clip 确认及 `v11 imported loop wrap`。完整步骤、备份和限制见 `tmp_analysis/aglina-native-return-v2/README.md`。启动器的配置保存也保留已存在的 external_loop 开/关设置，避免整体保存或动作开关将其丢弃。

## v10.2 崩溃恢复记录

v10.1 实机在 `AssetBundle.LoadAsset` 读取动画时发生 Unity 原生访问违规。归档加载已返回，但尚未执行控制器替换。原版与标准包的 AnimationClip TypeTree 存在 ACL 和其他自定义字段差异，外层转换不足以保证动画内部格式兼容。

1.9.2 将 `external_loop` 默认值改为 false；部署包已从自动加载路径移开，当前用户配置也已关闭外部加载。原动作开关继续使用 v9 的循环和特效保持。托管异常回退不能捕获这次原生硬崩溃；内部格式转换与离线验证完成之前不再启用此候选。证据见 `tmp_analysis/aglina-return-v1/v10.1-crash-20260909-225137/analysis.md`。以下为历史试验实现记录。

## v10.1 历史试验

v10 首次实机在 `LoadFromFile` 的归档包头读取阶段失败，随后回退 v9。v10.1 将标准 UnityFS 转换成游戏当前 Endfield VFS 内层归档格式，使用原包已有的无压缩信息/数据块模式；动画字节不变。详细日志区分归档加载、左右 LoadAsset、Clip 属性校验及 GC pin 失败。当前测试目录已部署专用包；还未确认游戏接受。

在原有持续特殊冲刺开关下，若模块旁存在 `actions/aglina_return_v1.bundle`，尝试加载外部 Humanoid 动画；加载失败、接口不匹配或实际播放 Clip 未确认时回退 v9。可在动作配置中设置 `external_loop=false` 明确禁用本次实验，原开关和 v9 特效保持仍有效。

回导 Clip 保留 3.466667 秒时长和起手；源帧 40–143 是预先修闭合的 1.716667 秒循环窗口，随后有平滑接回原版收尾的过渡。进入游戏后，仅为当前角色创建基于原控制器的私有 AnimatorOverrideController，替换左右两条目标 Clip，保留原状态机。原地预览去掉的前進位移和 19 个原始音效/语音事件已补回；不会把整个带起手的 Clip 设置成全段循环。

实际 CurrentAnimatorClipInfo 确认 `BE_Aglina_Return_L/R` 后，使用同状态、零混合时长的归一化 CrossFade 在闭合端点回绕，保留帧越界补偿并观察真实落点。退出仍交由游戏处理，待原特殊状态退出混合后恢复原控制器，避免恢复操作改变正在退出的姿态。换控制器时只恢复仍由本模块拥有的实例；组件释放时清理，关停线程不调用 Unity。

**实机待验：**动画由本机 Unity 2022.3.62f3 构建，Player.log 确认实际游戏引擎为 2021.3.34f5。专用归档已通过独立解码器逐字节回读，但序列化动画兼容性仍待游戏确认。每次进程仅尝试加载一次，替换包后须完整重启游戏和启动器。测试时应看到：

- `Aglina v10.1: archive accepted by LoadFromFile`
- `Aglina v10: external Humanoid clips loaded`
- `Aglina v10: external return clip confirmed active`
- `Aglina v10 imported loop wrap`，约从 0.688 回到 0.192（含实际帧越界量）
- 退出后 `Aglina v10: original controller restored`

若看到 `using v9 loop`，说明回导分支没有启用，不能据此评价外部动画效果。当前测试资源及构建证据在 `tmp_analysis/aglina-return-v1`，粗修原文件在 `tmp_analysis/aglina-loop-test-v1`。三组本地测试覆盖原动作策略、粒子恢复、新控制器所有权/失败重试/100 次闭合回绕；不替代游戏实测。

模块 ID：`betterendfield.actions`。启动器「动作模块」提供艾洁莉娜持续特殊冲刺开关，默认关闭。游戏在每次特殊冲刺选择左或右动作，本模块保留该选择。

## 历史版本 v9

用户已确认 v8 能循环、道具仍在，但粒子消失且接缝明显。22:52 的实机日志显示 v8 请求目标约 1.33 秒，混合后实际进度却回到 normalizedTime≈0.087，即约 0.30 秒；此前的“只要时间回落就算成功”误接受了入场重播。

v9 改用 `UnityEngine.Animator.CrossFade` 的显式归一化时间入口，避开原固定时间组件路径。保持约 0.933 秒的候选周期和同侧选择，不把不同目标区间、混合宽度同时改动。目标起点约 0.3846，混合完成应在约 0.4712 附近；若实际落回入场或落点偏离范围，就撤销保持并打印 `Aglina v9 loop rejected`。这是新的实测候选，尚未取得 v9 实机反馈。

粒子侧已从当前 overlay 读取两套目标 prefab：`_01` 特效寿命 2.7 秒、`_02` 寿命 3 秒，均自动淡出且非循环，四个 ParticleSystem 也均非循环。v9：

- 从本次 Perform 的 `m_effects` 键 0/1 获取 ObjectPtr，调用 Lock 校验当前实例，并核对目标特效名称；不按全局名称寻找或修改其他角色特效。
- 对加载完成的实例 ParticleSystem 保存原始 loop/duration 并开启循环。`_02` 的单粒子发光层寿命 3 秒而系统周期 5 秒，单改 loop 会留下空档，因此该层实例周期改为 2.5 秒，退出恢复 5 秒。
- 仅在目标会话有效保持期间，将这两个实例 `DurationTick` 的寿命/自动淡出计时 delta 置 0；粒子模拟、FollowTick 和其他效果照常运行。不修改共享 prefab、EffectLogicCfg 或全局粒子速度。
- 在会话退出、EffectInstance.Finish / OnStop 原生回收之前停止目标粒子并恢复原 loop/duration，释放引用；不屏蔽原生 Finish。失效 Unity 对象只释放托管引用，工作线程关闭时不调用 Unity。

日志标识：`Actions enabled: Aglina normalized bob blend and continuous particles (v9 experimental).`

关键日志：`normalized bob blend`（目标均为归一化值）、`animation progress`（含 next 和 transition）、`effect 0/1 continuous particles prepared: count=2`、`held target effect lifetime/auto-fade clock`。发现通用字典/资源身份或粒子设置不符合时，记录失败并跳过该特效修改，避免操作错误对象。

已通过 Release 构建、循环策略测试（新增 v8 错落点回归）、生产恢复/Finish 路径的 API 替身测试（恢复原参数、嵌套回收幂等、失效对象、无关对象和线程关闭）、61 个方法/属性访问器的本地 dump 核对。替身测试不等于 Unity 粒子渲染测试；循环落点、发光连续性与身体接缝仍需重启游戏确认。

测试包已更新：`artifacts/BetterEndfield-win-x64/modules/BetterEndfield.Actions.dll`（1.8.0，208384 字节）及同目录依赖 manifest。旧 v8 文件备份在 `tmp_analysis/actions-before-v9-test-update-20260908-231923`。复制测试包时同时更新 manifest，其中新增了粒子模块和 CoreModule 依赖。

```powershell
cmake --build build/native --config Release --target BetterEndfield.Actions BetterEndfield.ActionsTests BetterEndfield.ActionsEffectsTests --parallel 4
& build/native/Release/BetterEndfield.ActionsTests.exe
& build/native/Release/BetterEndfield.ActionsEffectsTests.exe
```

## v8 实现记录（已由 v9 替换固定时间播放入口）

这是一版可实测的同状态混合实现，尚未完成游戏内自然度验证。以下为当前行为；后面的 v4–v7 内容仅保留历史记录，其中整段 Play 重播与阻止 `_DoEnd` 的方案已经撤换。

- 首次自然入场。原片中后两个高点约在 1.633 / 2.567 秒，中间低点约在 2.267 秒。
- 在约 2.267 秒开始 0.300 秒同侧混合，目标从 **1.333 秒**前进到 **1.633 秒**。把两次上升的末端对齐到同一高点，正常速度下每轮约 0.933 秒。目标没有从 1.633 秒开始，因为那样会错开高点相位。
- 通过游戏 `ComplexAnimatorComponent.CrossFadeToStateFixed` 及其缓存路径提交；使用游戏 `HermiteCubic` 混合，保留 `CurrentThenNext` 中断。它不等同于此前离线五次多项式混合，也没有制作新 Clip 或修改共享 Controller。
- 使用归一化帧位及实时状态长度计算固定时间参数，并补偿触发帧的小幅越界。实际速度/时间缩放行为仍需实机验证。
- 播放请求后观察实际转场和进度回落。失败或迟到时撤销保持，不逐帧重试、不假装已经回绕；已经发生的其他过渡，包括回 Sprint，均放行。
- `ShowObject` 仅延后本次 handle 的 `logicId=11 / actorIndex=0 / show=false / delay≈2.4` 道具隐藏。保持本次 Perform 的 End 阶段自然 `_CheckTrackEnd` 清理；**不再 Hook `_DoEnd`**，确保道具/特效可以在 End 阶段正常创建。
- 对当前角色、当前有效保持会话的 `Flying_Stop` 声音事件延后一次，退出时补发。涵盖普通 AnimatorMono 和使用 Entity PostEvent 的快速音效入口；有回调的请求保留原行为，其他音效不屏蔽。音效过滤只在已记录游戏线程上访问会话。
- 只延后 `SpDashEnd + ForceExit` 自动退出。停止、攻击/受击等显式命令、腾空、换人、释放、关闭配置或混合失败时撤销会话，恢复游戏请求的黑板值，由原生动作和 Clear 接管。

### 原版和本版如何回普通疾跑

原版两侧分别在 normalizedTime 约 0.9000 / 0.91834（原片速度下约 3.12 / 3.18 秒）开始向普通 Sprint 混合，持续约 0.338 / 0.285 秒；不是等整条 3.467 秒全播完才回退。2.34 秒声音收尾和 2.4 秒道具隐藏是另两条独立时间线。

v8 在保持期间较早安排下一轮，因此正常情况下到不了上述 Sprint Exit Time。停止保持后不再安排循环，允许原生过渡/中断接管；**不是倒放，也不强制等完整动画末尾**。松开移动可能立即走停止分支，攻击或受击也可以立即打断。如果只是关闭配置且仍在跑，当前片段可继续走原生正常出口。最终视觉衔接及根运动效果待实测。

### 测试与日志

入口标识：`Actions enabled: Aglina peak-to-peak bob blend and End-track hold (v8 experimental).`

循环请求：`Aglina v8 same-side bob blend`，包含状态 hash、触发进度、目标秒数、混合秒数和次数。0.1 归一化进度档记录动画推进，避免原先整数 bucket 看不到原片中段变化。

配套保持：`deferred logic 11 prop hide`、`deferred owned Perform End track cleanup`、`deferred Flying_Stop until session exit`。失败退出：`loop interrupted, late, or not acknowledged`；不把请求成功当作视觉循环成功。

已通过 Release 构建与策略回归，覆盖 100 次同侧混合调度、真实进度回落确认、拒绝/停滞超时、首次入场、显式出口优先、无效数据和定点道具匹配；45 个方法/属性访问器描述与当前本地 IL2CPP dump 核对通过。测试 DLL 位于 `artifacts/BetterEndfield-win-x64/modules/BetterEndfield.Actions.dll`，需要重启游戏加载。实测优先检查保持至少 10 秒，以及停止、攻击、腾空、换人、关闭配置。

限制：两个源高点的 Root/手脚姿态并不相同，v8 用同侧混合验证可行性，尚未证明接缝自然；同状态转场、游戏时间缩放、快速声音分支和根运动仍以实机日志/画面为准。

## 历史版本记录（以下不代表 v8 当前策略）

## v4 修正（2026-09-08）

v3 实机仍失败：02:06 的日志确认加载 v3，随后直接 `perform cleared`，`natural_end_deferrals=0`。左右动画均报告 `clip_loop=0`，状态长度约 3.04–3.47 秒。不能再将这一结果归因于测试版本没有更新。

新增 `_TickStatePerformInterrupt` 拦截：其原生退出分支（`0x04D11149`）直接调用 `ForceExit(false)` 并清空当前 PerformRequest，绕过特殊冲刺的 Interrupt/ForceStop 方法。只在当前主控 Aglina、目标 Perform 仍归属且处于有效 Dash/地面 Sprint 时延后这一通用状态中断检查。此分支静态调用链已确认；此前 Clear 的具体运行时调用栈尚未记录，v4 增加了调用栈诊断。

对于非循环 Clip，在 normalizedTime 到 1，或观察到当前特殊状态向游戏 `HASH_STATE_RUN` / `HASH_STATE_SPRINT` 的过渡时，以当前特殊状态 hash 调用 Animator.Play(layer 0, normalizedTime 0)。不重新随机左右、不使用任意 0.7 截断阈值。其他过渡、归属丢失及移动条件失效不重播。整段重播的衔接效果尚需实机确认，不能等同于已验证的无缝循环。

构建与策略回归通过。测试 DLL 更新在 `artifacts/BetterEndfield-win-x64/modules/BetterEndfield.Actions.dll`；v3 备份在 `tmp_analysis/actions-before-v4-test-update-20260908`。下面保留 v3 自然结束拦截说明，该机制仍存在，但不足以独自解决实机问题。

## v5 修正（1.4.0）

v4 日志已确认 `_TickStatePerformInterrupt` 不是实际入口。调用栈为：

```text
CharPerformHandleBase._TickMainFlow
  -> _TryExit(InterruptCommandData)
  -> _DoExit()
  -> CharPerformMainHandle.Clear(false)
```

`_TryExit` 会在命令类型为 `ForceExit` 时直接调用 `_DoExit`。v5 增加 `_TryExit` Hook，读取命令对象的 `reason` 与 `interruptType`，只在它们分别等于游戏枚举 `SpDashEnd` 和 `ForceExit`，且当前仍是同一个 Aglina 特殊冲刺 Perform、角色仍在 Dash/地面 Sprint 时返回 false，阻止这一自动结束命令。攻击、受击、移动、死亡以及其他显式命令继续调用原方法。

此版本还会记录 `Aglina Perform _TryExit: reason=..., type=..., suppress=...`，用于确认游戏实际发出的命令；如果命令不是 `SpDashEnd + ForceExit`，不会被模块吞掉。自然结束后同侧动画重播逻辑保持不变。

## v6 诊断补充（1.5.0）

v5 没有打印 `_TryExit`，但其日志条件要求“命令存在且保持条件已成立”，因此不能据此判断 Hook 没有执行。v6 在目标 handle 的每次 `_TryExit` 首次调用时都记录命令指针及 `moveMode/gait/moving/airborne/owned/hold`，并记录启动时解析出的枚举值。这样可以区分命令为空、移动条件不成立、Perform 归属变化和枚举值不匹配。

## v7 修正（1.6.0）

v5/v6 的保持判断仍要求 `IsPlayingSpDashPerform()` 为 true。实际退出栈表明游戏会先清掉这个瞬时标志，再处理 `SpDashEnd + ForceExit`，因此 `_TryExit` 和 `InterruptSpDashPerform` 都提前放行。v7 在这两个入口使用已记录的 pinned Perform handle、特殊 Animator 状态和移动状态判断，不再重复要求瞬时播放标志；只要仍是本次特殊状态且角色仍在有效移动，就拦截该自动结束命令。

测试时应看到 `Actions enabled: Aglina SpDashEnd hold and selected-side replay (v7).`，并在第一次自动结束时看到 `suppress=1` 或 `suppressed InterruptSpDashPerform`。

## 2026-09-08 实机证据

1.1.0 已能进入特殊状态并延后清除 `isDashing`，但仍不能持续。多次日志在 normalizedTime 约 0.688–0.691 时出现：`perform=0, special=1, outgoing=0`，然后模块撤销保持。这说明观察到的取消先发生在 Perform 归属/播放状态一侧，当时动画尚未到 normalizedTime=1，也没有正在退出的 Transition。不能仅据此认定整个退化问题都是 Clip 长度导致。

## 新补上的自然结束路径

当前原生代码：`CharPerformHandleBase._TickMainFlow()` 除了处理显式退出，还会因 `_CheckEnd()` 或时间轴轨道完成而调用 `_DoEnd()`（研究调用地址 `0x037163F6`）。`_CheckEnd()` 检查中断命令、固定时长等条件；轨道完成也有独立分支。因此前两版仅处理动画黑板和特殊冲刺 Brain 的中断不够。

1.2.0 Hook `_TickMainFlow` 和 `_DoEnd`，仅在以下条件同时满足时延后 `_DoEnd`：

- 同一个已记录的 Aglina 特殊 Perform handle；
- 本次调用位于它自己的 `_TickMainFlow` 中；
- 保持会话已激活，角色仍处于有效 Dash 或地面 Sprint，动画和 Perform 身份匹配；
- 当前阶段是 Start 或 Loop，实体仍存活；
- 没有待执行的显式中断命令。

不停止时间轴计时、不改共享配置、不重启 Perform，不屏蔽 `_TryExit`、死亡或显式命令。Clear/释放路径撤销会话。非目标角色/Perform 全部走原逻辑。

**该自然结束分支已通过反汇编确认，但 1.2.0 尚未实机验证。** 若还有动画本身到尾退出，需要根据新增日志再判断；当前版本未宣称已经解决所有退出路径。

## 原有保持与退出

- `StartSpDash` 完成后校验 `chr_0013_aglina`、主控身份和 `CharIntPerform_Aglina_Spdash`，记录具体 handle。
- 入场 Pending 最多等待 1 秒游戏时间，允许旧普通 Dash 的过渡先完成。状态 ID 读取游戏的 `HASH_SP_DASH_L/R` 静态字段。
- 动画黑板原生更新每帧设置 `isDashing = (moveMode == Dash)`。仅对保持中的目标会话，在实际 Dash 或 Grounded + Sprint + 地面移动且不在空中时，延后清除该动画参数。
- `ShouldInterruptSpDash` 和 `InterruptSpDashPerform` 只对当前有效会话抑制自然退化中断。
- 停止、攻击/受击接管、腾空、换人、死亡、关闭配置、强制停止或释放都保留退出。
- 清理时恢复游戏最后请求的黑板参数值；不向释放后的组件或工作线程调用 Unity。

## 配置

```ini
[betterendfield.actions]
schema_version=2
enabled=false
diagnostics=true
```

兼容首版 schema 1 的开关，忽略旧循环区间配置。开关默认关闭，下一次自然触发生效。

## 新日志与验收

- `selected state=left/right`：本次实际选择、动画长度及 `clip_loop`。
- `Perform _DoEnd: defer=...`：是否延后自然结束，Perform 当前时间、固定时长、总时长、循环标志、阶段及命令标志。
- `Perform interrupt command`：显式中断的 reason/type/weak，全部转交原方法。
- `animation progress`：动画是否经过 normalizedTime=1，以及自然结束已延后多少次。
- `exit`：当前/下一状态、进度、长度、循环标志和 Perform 是否仍归属本会话。

测试时保持冲刺超过原退化时刻，再检查停止、攻击、受击、腾空、换人及关闭功能。如果 `defer=1` 后仍退化，可从动画是否经过 1、是否出现 outgoing 和具体命令区分下一条退出路径。

原生模块、启动器发布和生命周期策略回归测试通过；实机效果仍待确认。

```powershell
cmake --build build/native --config Release --target BetterEndfield.Actions BetterEndfield.ActionsTests --parallel 4
& build/native/Release/BetterEndfield.ActionsTests.exe
```
