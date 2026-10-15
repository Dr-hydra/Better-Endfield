# Android 1.6.4 定向挂点与初始化核查

日期：2026-10-15。设备 `78572d34` / `25102RKBEC` / `myron`，安装包 `com.hypergryph.endfield`，**versionName=1.6.4、versionCode=61、arm64-v8a**。此次实际 Android 版本不是 Windows 的 1.6.3，因此输入与报告按 1.6.4 单独记录。

## 结论

本轮检查的持续冲刺入口、调用关系、字段与枚举，以及 Android MMD 地面查询合同均与当前实现相符，**没有发现需要修改共享 native 源码的 Android 新版差异**。BE 4.0.3 在该设备本轮启动后，UI 与 Camera 已成功初始化；Camera 四处挂钩和功能合同均报告就绪。

持续冲刺配置未启用，MMD 未播放，本轮没有实际操作冲刺开始/保持/退出或 MMD 地面采样。静态核查和模块初始化不能替代这两项实操。

## 输入与范围

- 从同一安装定向取得 `inputs/1.6.4/Android/libil2cpp.so`（549,013,048 字节）和 APK 中 `assets/bin/Data/Managed/Metadata/global-metadata.dat`（74,837,536 字节）。原始资料身份见同目录 `source.json`。
- 只传输了这两个文件；没有拉取 1.69 GB 的 APK，没有下载语言包或全量 VFS。
- 复用 `tools/HookInlineScan/extract_targeted_metadata.py` 的元数据类，补充本目录 `extract_android_targets.py` 的 ELF64、相对重定位与本机文件掩码读取。输出仅 11 个目标类和 16 个有界方法体，不生成全量 dump / IDA 数据库。
- Android metadata 的分块掩码、个别 ELF 字段/代码页掩码只在本地分析副本处理，原始文件、设备安装和官方配置没有改写。`global-metadata.target-readable.dat` 用于本次目标读取，未声明为全用途规范化 metadata。
- 静态 VA 均来自当前 ARM64 ELF，基址按零计算；没有复制 Windows PE RVA。

## Actions 调用证据

| 方法 | Android 1.6.4 RVA | 签名 / 调用证据 |
| --- | --- | --- |
| `CharPerformHandleBase._TickMainFlow` | `0x112c3c70` | 实例 `Void(Single)`；`0x112c3cb0` 调用 `_UpdateCommandData` |
| `CharPerformHandleBase._UpdateCommandData` | `0x112cb084` | `0x112cb0b4` 直接 `BL 0x112af854` |
| `CharPerformHandleBase._TryGetCurInterruptCommand` | `0x112af854` | 实例 `Boolean(InterruptCommandData&)`；当前拦截器的输出对象引用符合签名 |
| `CharPerformHandleBase._DoExit` | `0x112cae78` | 实例 `Void()`；主流程 `0x112c3e48` 与 `_TryExit` 的 `0x112cae68` 都直接调用它 |
| `CharacterSpecialDashBrain.ShouldInterruptSpDash` | `0x10449560` | **static** `Boolean(CharacterAnimationComponent)`，与当前 `CheckDetour` ABI 相符 |

Android 上 `_TryExit` 和 `_CheckTrackEnd` 仍有主流程直接调用，优化方式与 Windows 不同；当前选择的 `_TryGetCurInterruptCommand` 和 `_DoExit` 仍处于实际调用链，因此不需要为旧 Windows 入口保留兼容分支。

`ShouldInterruptSpDash` 的委托引用也已核实：`CharacterAnimationComponent._DoPlaySpDashPerform` 在 `0x1037424c` / `0x10374250` 传入值 3；静态构造函数 `0x103743f8` 在 `0x10374d80` 读取方法槽 `0x203b7030`，随后在 `0x10374d98` 以 key 3 加入字典。该槽编码 `0x6001c77d` 解码为 method index 58302 / token `0x0600e3bf`，对应 `ShouldInterruptSpDash`。因此不能因该方法缺少直接 `BL` 就判断挂钩不触发。

字段与枚举：

- `InterruptCommandData.interruptType` / `reason` 类型均匹配；对象字段偏移为 `0x10` / `0x14`。文件中的这两个偏移带 `0x2f2f2f2f` 掩码，报告保留原存储值；生产代码仍按运行时字段元数据读取，不硬编码偏移。
- `CharInteractPerformEnums.InterruptReason.SpDashEnd=12`。
- `CharInteractPerformEnums.InterruptType.ForceExit=2`。
- `MovementComponent.GroundedMoveGait.Sprint=2`。

详细签名、字段、调用站点和局部汇编见 `targets/targeted-contracts.json` 与同目录 `.asm.txt`。

## Android MMD 路径与 ABI

需纠正先前 1.6.3 readiness 记录：`eiem_slot.inc` 顶层 `#if defined(__ANDROID__)` 直接包含 `compat/android_slot.inc`；桌面的原生 `float* position` typedef 和 `VerifyFloorResultLayout` 位于 `#else`，**没有编译进 Android**。

Android 真正使用 `compat/android_terrain.inc`：按完整签名解析方法，通过 `runtime_invoke` 传入参数，并以实际托管装箱对象、GC pin 和 `object_unbox` 保存 `FindFloorResult`；结果通过字段名和托管值类型方法读取，不依赖 Windows 原生 ABI。

本机元数据确认：

```text
ComputeFloorDist(Vector3, Single, FindFloorResult&, Single, Boolean, Single, Boolean) : Void
FindFloor(Vector3, FindFloorResult&, Single) : Void
FindFloorResult.Clear() : Void
FindFloorResult.IsWalkableFloor() : Boolean
FindFloorResult.get_floorNormal() : Vector3
```

`ComputeFloorDist` 的 ARM64 方法体确实按 HFA 从 `s0/s1/s2` 接收坐标、`s3` 接收扫距、`x1` 接收结果引用、`s4` 接收半径、`w2/w3` 接收布尔、`s5` 接收偏移。该事实说明 Windows 原生指针 typedef 不能直接移植，但当前 Android 托管调用通路已经处理 ABI，不需要增加没有执行路径的 Android typedef 分支。

字段也全部匹配 Android 查询器：`isHit:Boolean`、`floorDist:Single`、`floorHit:RaycastHit`、`RaycastHit.m_Point:Vector3`。附带检查的未装箱布局为 `isHit@0`、`walkableFloor@1`、`floorDist@8`、`floorHit@0x10`，`RaycastHit.m_Point@0`、`m_Normal@0xC`。`get_floorNormal` 的实际 ARM64 读取 `FindFloorResult+0x1C/+0x24`，与嵌入 hit 的 normal 一致。生产 Android 路径仍通过字段元数据，不将这些数值写入代码。

## 本轮设备初始化结果

主流程已以 `adb install -r` 保留数据更新 BE **4.0.3 / versionCode 40003**，并启动一次 BE 与游戏。本子任务随后只读采集，不重复启动、不修改设置。

读取：`/data/user/0/com.hypergryph.endfield/cache/betterendfieldnext-diagnostics.log`，设备文件时间为 10:02。完整 BE 专属日志保存在本目录 `device-diagnostics.txt`；文件采用追加写入，无逐行日期，因此用本次末段 `tid=10430` 与设备时间确认，前段 `tid=19281` 属先前启动，不混作本次证据。

- UI：`BetterEndfieldNext.UI module initialized successfully.`
- Camera：`free_camera=ready, anti_dither=ready, time_heartbeat=ready`。
- Camera 挂钩：`camera_manager.tail_late_tick`、`cinemachine.push_state`、`unity.time.unscaled_delta.get`、`camera.process_dither` 均记录 installed。
- Camera：`BetterEndfieldNext.Camera module initialized successfully.`；用户当前配置为 `enabled=true`、free camera / anti dither / pause 关闭，保留原值。
- CustomModel 的 MeshData transaction 和 pipeline LOD 参数回读也成功；可选 `android.global_mip_limit` 诊断接口不可用，不据此宣称整个模型功能失败或全部验证完成。
- Actions：只读 `module_settings.xml` 确认 `dash_enabled=false`、`actions_configuration` 为空；按现有模块启动规则不会初始化 Actions。未启用项没有日志是预期行为，本次没有擅自启用。
- MMD：未提供 VMD 播放动作、未执行地面查询，不能把 Camera 初始化等同于 MMD 整条运行路径验证。

读取时游戏已不在进程列表。系统 `dumpsys activity exit-info` 最新主进程记录为 **2026-10-15 10:02:47.772，USER REQUESTED / FORCE STOP，`stop com.hypergryph.endfield due to SwipeUpClean`**；证据见 `latest-game-exit.txt`。这次离开由系统记录为划掉清理，没有将其猜测为初始化崩溃，也没有写成游戏自主正常退出。

机器可读摘要：`verification-summary.json`。本轮无需修改共享 native 源码；未计算产物哈希。
