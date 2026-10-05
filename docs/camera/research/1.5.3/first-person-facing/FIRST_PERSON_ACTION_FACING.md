# 第一人称动作、锚点与朝向排查（2026-10-03）

基线：`main dd7131b4`。相关祖先及 native/camera、tests、docs 路径未找到适用 AGENTS.md。只修改 `native/modules/camera/module.cpp` 的第一人称状态/转向部分及 `native/tests/camera_playback/first_person_tests.cpp`，不改 UI、头饰实现、MMD 或自由相机文件。

本次只有离线代码、dump 和 DLL 静态证据；没有启动游戏或实机复现。之前已修复的退出调度、Snapshot 延迟退出、角色/骨架归属、Brain 隔离和 scoped 缓存恢复保留，本次不重新实施这些工作。

## 确认路径

读取 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/` 内 Cinemachine、Gameplay.Beyond 和 UnityEngine.CoreModule dump，并只读反汇编本机 `D:/Arknights Endfield/GameAssembly.dll`。RVA 仅用于标识离线证据，实现全部通过具名契约解析。

| 证据 | 代码行为与影响 |
| --- | --- |
| `CameraState.CorrectedOrientation`，RVA `0x032FA8D0` | 调用 Quaternion 乘法，参数顺序是 `RawOrientation × OrientationCorrection`。原第一人称只读 raw，随后将 correction 清成单位旋转，确实丢弃游戏最终朝向的一部分。 |
| `CameraState.FinalOrientation`，RVA `0x03225780`，Dutch 分支 `0x04D3F66C` | 最终旋转再右乘绕本地 forward 的 Dutch 旋转。原眼位偏移仅依据 raw，因此有 correction / roll 时偏移基底与显示视角不一致。 |
| `Entity.rotation`，RVA `0x031DA960`；`meshRotation`，RVA `0x03234BD0` | 前者读取 RootComponent 保存的逻辑旋转，后者读取模型 Transform。dump 的 RootComponent 也分别保存 root Transform 和 model。原代码转动 ModelGo Transform，不能同步 Entity 逻辑朝向；纯 yaw 赋值还会丢失模型原有的旋转偏置。 |
| `RotatorComponent.SetRotation`，RVA `0x035EE780` | 调用 `RootComponent.set_rotation` 后调用 `StopRotate(-1)`。因此不能在技能/旋转任务运行时无条件调用该方法。 |
| `RootComponent.set_rotation`，RVA `0x02CE5550`；`_UpdateRotation`，RVA `0x02CE5330` | 更新逻辑旋转后更新 root Transform、forward 等状态。这提供了保持逻辑与视觉方向一致的现成入口。 |
| `SelfRotateAction._OnRootMotionUpdate`，RVA `0x06021EBC` | 计算 `m_rootMotionStartRot × rootMotionQuat`，调用上述 `RotatorComponent.SetRotation`。动作与第一人称争夺方向有具体游戏侧路径。 |
| `RefreshFirstPersonTarget → ApplyFirstPersonFacing` / `ApplyFirstPersonState` | 原转向在 TailLateTick/pump 中使用上一次 push 缓存的 forward；若当前帧 push 已完成，再转动身体会移动头部，而眼位还在转身前的位置。生命周期修复禁止轻量 heartbeat 转向，但没有消除这个顺序窗口。 |

这几条路径能解释可能的方向冲突及位置错位，不证明用户的某次攻击已经触发全部路径。攻击中的头骨位移本身也可能来自正常动画；本次没有把正常动画位移改为固定相机高度。

## 局部修正

- 保留 raw orientation、orientation correction 和 Dutch，按离线确认的最终旋转计算眼位 forward/up。只清除位置 correction，继续使用原 FOV、near clip 覆写与 scoped 缓存恢复。
- 完整帧刷新仅排队一次转身；属于会话的下一次 push 用自己的当前 view 执行，再读取头骨世界位置。其他 Brain、轻量 heartbeat 和重复 push 不额外转身。
- 空闲转身读取 `Entity.rotation`，通过 `RotatorComponent.SetRotation` 更新实体 root；用世界 Y 轴增量左乘当前旋转，保留现有倾斜及模型局部偏置，不再直接写 ModelGo 的世界旋转。
- 转向和退出恢复均检查 Entity alive / cinematic、移动 / 空中 / 上帧 root motion 旋转、RootMotionData.hasRootMotion、Rotator 旋转任务 / camera lock、AbilitySystem.inSkill / immobilized。状态不可读时停止转向；root-motion getter 的异常不会被当成“没有动作”。这些新增契约为可选能力，不作为整个第一人称开启的必需条件。
- 游戏逻辑旋转已偏离最后写入值时，放弃旧旋转所有权并让该次 push 保留游戏方向；活动动作期间退出同样不调用会结束旋转任务的 SetRotation。仍空闲且旋转仍属本会话时才恢复进入转向前的方向。

`RootMotionData.hasRootMotion` 本机离线 getter (`0x0301FBB0`) 检查 motion weight，因此不能仅以对象是否存在判断正在做 root motion。嵌套类型使用现有 resolver 支持的 `MovementComponent.RootMotionData` 名称。

`AbilitySystem.inSkill` 本机离线 getter (`0x0312D6E0`) 检查 `curSkill` 是否非空；不是从按键推测攻击状态。16 个新增具名契约均在当前 dump 中有对应声明，runtime 是否解析成功仍待实机确认。

## 验证

SSD 独立构建目录：`D:/work/BetterEndfield-checks/task4-first-person-action-20261003`。不修改项目全局 build；PC / Android 生产目标由主代理统一编译。

```powershell
$cmake = 'D:/work/Visual Studio/Common7/IDE/CommonExtensions/Microsoft/CMake/CMake/bin/cmake.exe'
& $cmake -S 'F:/Better Endfield/native/tests/camera_playback' -B 'D:/work/BetterEndfield-checks/task4-first-person-action-20261003' -G 'Visual Studio 17 2022' -A x64
& $cmake --build 'D:/work/BetterEndfield-checks/task4-first-person-action-20261003' --config Release --target camera_first_person_tests
& 'D:/work/BetterEndfield-checks/task4-first-person-action-20261003/Release/camera_first_person_tests.exe'
```

Windows Release 构建及测试通过：`PASS production first-person lifecycle: 1222 checks`。基线先运行通过 313 项断言。测试直接包含生产翻译单元，保留原生命周期用例，增加非交换 raw/correction/Dutch 组合、无效旋转、转向后采样头骨、当前 view 与缓存反向、重复/其他 Brain push、实体与模型偏置、移动/技能/root motion 等状态让权、getter 异常/缺失契约及游戏接管后的退出检查。负责文件的 `git diff --check` 通过。

离线反汇编摘录保存在 `D:/work/BetterEndfield-checks/task4-camera-facing-disassembly-20261003.txt`，不复制游戏文件。构建只观察到现有 character_motion 的 shared_ptr atomic C++20 弃用警告。

## 实机边界

尚需 PC / Android 实机确认：普通攻击、原地技能、位移技能、root motion 转身、移动/跳跃/受控状态，及上述行为中退出第一人称；特别检查攻击起始的实体方向与可见身体方向。离线 Host 只能验证分支和调用顺序，不能证明当前版本 runtime 解析、动画更新阶段或所有特殊动作状态完全等同模拟。

本次没有添加猜测偏移、游戏技能 hook 或固定眼位/动画过滤。若仍有攻击时相机偏移，建议实机同步记录 head/root 世界位置、Entity.rotation / meshRotation、CameraState raw/correction/Dutch 和动作状态，先区分动画移动头骨与方向同步问题，再决定是否增加特定行为处理。
