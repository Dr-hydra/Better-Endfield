# 第一人称退出与切人问题诊断（2026-10-02）

范围：只读审计 Windows / Android 共用代码及已有日志，没有修改功能、部署模块、改变用户配置或主动触发游戏崩溃。

## 当前结论

已确认退出请求的执行有调度缺口，角色切换的锚点失效处理也不完整。这些是可定位的代码问题，但已有日志不足以将用户的限角残留或切人崩溃归因到某一条调用。不能将裸指针访问、GPU 回读或 Snapshot 状态直接写成已证实的崩溃根因。

第一人称代码没有画面水平/俯仰角 clamp；`first_person_side_look_limit` 控制身体转向触发阈值，第一人称朝向来自游戏原有 CameraState。若确认 `g_first_person_active=false` 后仍有限角，需要继续看游戏侧相机模式/输入范围，不能通过添加一个“解除限角”写入去猜修复。

## 代码证据

### 1. 配置关闭只排队退出，心跳不处理第一人称

- `native/modules/camera/module.cpp:2554`、`:2561`：配置关闭首先更新 `g_first_person_camera_enabled`。
- `:2642`：将退出请求排队，没有在此清理（配置线程不能直接执行 Unity 生命周期操作）。
- `:1895`：真正的禁用/退出处理在 `PumpFirstPerson`。
- `:1939`：`Time.unscaledDeltaTime` 心跳会处理自由相机、MMD、角色动作，**没有**处理第一人称。
- `:1967`：CameraState 第一人称分支只看 `g_first_person_active`，未同时检查功能开关/退出请求。
- `:1992`、`:2014`：常规退出依赖 TailLateTick；CameraTick 只有 TailLateTick 未安装时才后备调用。

因此，在 TailLateTick 暂停而非卸载、仍存在相机 push 或渲染帧的窗口内，关闭配置后活动状态可以悬挂。暂停、菜单/过场或切场景是应检验的触发条件，当前没有该场景的操作日志证明用户限角恰由此引起。

Android 还有共用 generation 的问题：`:1165` 的 `PumpFreeCameraControl` 单独增加 `g_android_pump_generation`，`:2037` 的后备帧据此认为所有相机 pump 均已执行。仅运行 unscaled 心跳也会增加 generation，而没有运行第一人称 pump，后备帧便可能跳过退出处理。

### 2. 切人窗口的锚点未统一失效

- `module.cpp:1873`：只有新主控角色非空且与旧角色不同才释放旧锚点。角色暂时为 null 时仍持有旧角色、头/身体/颈锚点。
- `:1887`：只独立检查头 Transform 存活，没有独立验证 body / neck / 活动摄像机。
- `:1672`、`:1679`、`:1701`：身体转向只检查非空，随后访问并更新保存的 body Transform。
- `:1968`：CameraState push 使用头锚点早于帧末 `RefreshFirstPersonTarget`。头部的 Unity 存活检查挡住已销毁对象，但无法区分“旧角色还活着但已不再是主控”。
- `:1819`、`:1822`、`:1874`：进入时先绑定头、再把角色身份保持 null，第一次角色刷新不释放已有锚点；进入与换人恰好交错时可能保留与新身份不一致的锚点。

这解释了为什么切人需要一个明确的未绑定/重绑定窗口。GCHandle 保留托管 wrapper 不等于它所属的 Unity 原生对象仍有效，也不等于它仍属于当前角色；现有存活检查不能代替归属检查。

### 3. 身体锚点存在未经校验的类型假设

`module.cpp:1647`–`:1650` 将 `PlayerController.GetMainCharacter` 返回的角色对象作为 `UnityEngine.Component.get_transform` 的 instance。契约绑定的是 Component 方法，并未证明当前 `Beyond.Gameplay.Core.Entity` 的继承关系；失败后才回退到模型 GameObject。

仓库其他角色资源路径使用 `Entity.get_modelCom → BaseModelComponent.GetModelGo → GameObject.get_transform`（`:1468`、`:1480`，以及 `character_motion_runtime.inc:83`）。当前没有可用运行时类型记录确认 Entity 继承 Component。离线 `tmp_analysis/android-153/global-metadata.dat` 的直接解析因 magic 检查失败，不能据此推断类型。

建议去掉这个类型假设，沿已有模型 GameObject 路径绑定身体；角色水平朝向若要驱动真实 Entity，应解析并验证角色自身的朝向接口。

### 4. 第一人称没有限定目标 Brain

`module.cpp:1967` 将每个 Cinemachine Brain 的 push 都送入第一人称覆写；自由相机的相邻分支在 `:1970` 使用 `BrainDrivesActiveCamera`，第一人称没有这项检查。主相机被菜单/详情/过场相机替换时仍保持旧会话，也未像自由相机 `free_camera_runtime.inc:846` 一样验证摄像机身份。

这会把旧角色眼位和近裁剪/FOV 写入不属于会话的相机状态，是相机切换时的重要干扰点。它没有改原始朝向或游戏的轴限值，仍不能直接证明限角残留。

### 5. Snapshot 不是已证实的原因

`first_person_runtime.inc:528` 仅在检测到游戏拍照第一人称已开启时将它关闭，没有开启该模式。`ExitFirstPerson` 的确没有再次调用它，但不能据此宣称本模块“留下了游戏第一人称开关”。需要退出时 Snapshot 标志的实际读数后，再决定是否处理该状态；不能无条件改变用户的拍照模式。

## 已有日志及边界

- PC：`%LOCALAPPDATA%/BetterEndfield/logs/BetterEndfield.log` 最新写入 2026-10-01 22:55:46。现有第一人称进入/退出/切人记录最新为 **2026-09-16**，早于 9 月 30 日的身体转向与直接 Renderer 隐藏更新，不能作为当前故障的复现证据。10 月 1 日近期会话配置为 `first_person=false`。
- PC `crash.log` 是桌面 UI 缺少 `SponsorForegroundBrush` 的 XAML 启动异常，与游戏内切人无关。
- Android 设备 `78572d34` 已连接，检查时 `com.hypergryph.endfield` 未运行。仅执行只读 adb 查询。
- Android 持久诊断：`/data/user/0/com.hypergryph.endfield/cache/betterendfield-diagnostics.log`，362,227 字节，文件时间 2026-10-02 08:13。此文件已轮换，保留范围没有第一人称 enable/disable/切人事件；不能用缺失事件断言未发生。
- Android 该文件有 133 条 Aglina `named part requires topology fallback, but GPU readback is unavailable`，包括 `hair_01`、`hair_02`、`face_01` 和 `*_shadowProxyMobile`。这是头发处理失败的实证，不是切人崩溃的实证。
- Android 最新 12,000 条 logcat 未检出相关第一人称操作；crash buffer / 最新 tombstone 是一个离线 UI 布局断言程序与另一应用的异常，没有本次游戏崩溃栈。

## 建议最小修复顺序（未实施）

1. 在 Unity 线程的统一相机帧调度中处理第一人称退出/切换请求；Android 对第一人称单独记录是否已 pump。配置关闭立即禁止新的第一人称 CameraState 覆写，资源清理由主线程完成。
2. 统一失效角色/模型/摄像机身份。主控为 null、身份变化或任意必要锚点失效时先停止身体转向与第一人称 pose，再恢复旧网格并释放旧锚点；新角色模型就绪后一次性建立一致会话。
3. 身体绑定沿验证过的 ModelGo 路径；头/body/neck 调用前均检查存活和当前会话归属。第一人称 push 只允许当前会话的主摄像机 Brain。
4. 如果退出清理恢复网格失败，保留其所有权并重试；不要把“活动相机已退出”和“网格全部恢复”合并成一个布尔状态。
5. 加入少量有节制的诊断记录：请求/执行退出的帧序号与来源、当前角色/模型/摄像机 identity、绑定代次、Snapshot 标志、游戏线程 ID；崩溃用对应版本原生符号定位堆栈。避免每帧完整树/回读失败重复刷屏。

## 验证建议

- 离线生命周期测试：进入第一人称→暂停常规 TailLateTick→配置禁用→只驱动 unscaled/nativeRender，断言下一主线程帧完成退出；验证 Android 的自由相机 heartbeat 不能掩盖第一人称待处理请求。
- 离线角色切换测试：角色 A→null→B（A 原生对象分别存活、销毁）；确保过渡帧不再调用 A 锚点，无类型错误的 Component 调用，旧 Renderer 恢复与新会话绑定严格有序。
- 双 Brain 测试：主摄像机和详情/过场摄像机同时 push，只有会话所属摄像机能接受第一人称修改；摄像机 identity 变化时退出或重绑定。
- 实机确认：正常关闭、冻结世界后关闭、切队伍、切角色、详情/拍照/菜单开关、切后台后返回、模型包热切换；记录退出后 active/配置/Snapshot 标志及游戏相机输入范围。如果 active 已为 false 但仍限角，继续定位游戏侧限角来源。

现有 `first_person_mesh_tests.cpp` 覆盖几何与重试预算；`android_rebuild/camera_test.cpp` 覆盖 hook 准备状态与世界暂停。它们没有覆盖上述第一人称退出调度/角色归属生命周期，因此不能以现有测试通过宣布这两项用户问题已解决。
