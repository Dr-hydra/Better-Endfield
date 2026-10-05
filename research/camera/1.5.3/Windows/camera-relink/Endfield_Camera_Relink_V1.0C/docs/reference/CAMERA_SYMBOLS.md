# 终末地相机系统符号清单 + 实测结论

> 来源：自研模块运行时枚举 il2cpp 元数据 + **方法签名自省**（离线 Il2CppDumper / Cpp2IL 均因加固失败）。
> 程序集：`Gameplay.Beyond.dll`（游戏 IL2CPP 2021.3.34f5，metadata v29）。
> 本文档只记录**已在实机验证**的结论。

## 附: 完整图层名表 + 两台相机的遮罩归属（2026-09-14 实测, 指令 70 导出）

`LayerMask.LayerToName(0..31)` 的原始结果 —— 这是"要隐藏某类 UI 时该清哪一层"的查找表：

| 位 | 名字 | 位 | 名字 | 位 | 名字 |
|---|---|---|---|---|---|
| 0 | Default | 11 | Character | 21 | IK |
| 1 | TransparentFX | 12 | Enemy | 22 | NPC |
| 2 | Ignore Raycast | 13 | UIModel | 24 | UltimateShow |
| 3 | Fog | 14 | Building | 25 | BattleShape |
| 4 | Water | **15** | **UIInteract** | 26 | Physics |
| **5** | **UI** | **16** | **WorldUI** | 27 | DropItem |
| 6 | Walkable | 17 | Projectile | 28 | Hide |
| 7 | Climbable | 18 | AbilityEntity | 29 | Liquid |
| 8 | PostProcessVolume | 19 | Interactive | 30 | Gacha |
| 9 | Trigger | 20 | Terrain | 31 | (无名) |
| **10** | **UIPP** | | | | |

实测遮罩（同一时刻读到的值）：

```
MainCamera 遮罩 = 0xAFFFDBDF   (GetMainCamDefaultCullingMask / GetMainCameraCullingMask 同此)
  渲染   : 0,1,2,3,4,6,7,8,9,11,12,14,15(UIInteract),16(WorldUI),17,18,19,20,21,22,23,24,25,26,27,29,31
  不渲染 : 5(UI), 10(UIPP), 13(UIModel), 28(Hide), 30(Gacha)   ← 全是"UI 专用"层
UICamera   遮罩 = 0x00000020(只渲染层 5:UI), depth=2  (GetUICamDefaultCullingMask 也是 32)
  游戏在 UI 状态变化时会往它的**配置栈**加层: 实测出现过 0x420 = 位 5(UI) + 位 10(UIPP)
```

由此得到两条已实测确认的结论：

1. **屏幕 HUD 在层 5(UI)**，由 `UICamera` 绘制 → 把 UICamera 遮罩压成 0 就消失，而且**碰不到世界**
   （MainCamera 根本不渲染层 5）。通道：`AddUICamCullingMaskConfig("ECLHideUI", 0)`
   + `_UpdateUICamCullingMask()`。
2. **跟着角色/怪物走的那些条（怪物血条、状态条、角色体力条）在层 16(WorldUI)**，由 **MainCamera**
   绘制 → **压 UICamera 永远盖不住它们**。**2026-09-15 实测验收**：清掉位 16 后用户确认
   "怪物血条 + 角色体力条都消失、世界正常没缺东西" → 已设为默认(`ui_hide_maincam_clear_bits=65536`)。要清的是 MainCamera 遮罩里的**位 16**（必要时连
   位 15 UIInteract 的交互提示一起清）。通道：`AddMainCamCullingMaskConfig("ECLHideWorldUI", 新遮罩)`
   + `_UpdateMainCamCullingMask()`；还原 = `RemoveMainCamCullingMaskConfig` + 同款 Update
   （让游戏自己重算，不留残渣）。
   运行时工具：指令 **70**（只读图层表）/ **71**（`-A0 <位掩码> -A1 <秒>` 一次性测试，**到期自动还原**；
   不带 `-A1` = 设为持续）。

> 注意（踩过一次）：遮罩是**位掩码**，带位 31 的合法值（如 MainCamera 的 `0xAFFFDBDF`）作为有符号
> int 是**负数** —— 判断"读取是否成功"**绝不能用 `< 0`**，否则合法的世界相机遮罩会被当成读失败
> （1.0.0am 的世界清位实测就因此直接拒绝执行；1.0.0an 改成显式的成功标志）。
>
> **1.0.0ar 追加第二次踩坑**：同一条纪律对 `> 0` / `>= 0` 同样成立。保活的"目标遮罩"判有效时用了
> `<= 0` / `v > 0`，于是 `0xAFFFDBDF`（负数）被判成"推导不出来"，保活**一次都没动手**；
> `GameSysUiHideEnd` 里 `mainNormalMask >= 0` 的兜底也**恒为假**（自 1.0.0am 起从未执行过）。
> **唯一安全的哨兵是 `0`**（0 本来也永远不是合法遮罩：写 0 就是黑屏）。


---

其中 **层 10（UIPP）** 在 1.0.0ar 被定性：**ESC（时停）界面 = "模糊/冻结的世界画面 + 菜单"合成在同一张
层 10 全屏图上**（掩码探位实测：UI 相机只渲染位 10 时界面原封不动；只渲染位 5 时界面消失）。
因此**"只藏 ESC 菜单、留下那幅世界画面"在 cullingMask 层面做不到**，只能整块藏或整块留。

> 另测到一条渲染管线事实（1.0.0ar）：本作是自定义渲染管线（`HG.Rendering`），
> **`UnityEngine.Camera.clearFlags` 能读能写但不被采纳** —— 把它写成 3(Depth) 后 ESC 界面依旧全黑；
> 而 `enabled=false`（相机整体不工作）立刻让主相机画的世界露出来。
> 顺带：**遮罩=0 的相机照样执行清屏**，这是"严格隐藏后按 ESC 全黑"的真正原因。

## 一、最重要的一条：签名自省，不要猜参数

加固后的元数据里，方法名能拿到，但**参数类型必须自己问**。用
`il2cpp_method_get_param(method, i)` + `il2cpp_method_get_return_type(method)` +
`il2cpp_class_from_type` + `il2cpp_class_get_name/namespace` 得到真实签名。

实测签名表（`->` 右边是返回类型）：

```
CameraUtils.get_cameraManager()                                   -> CameraManager
CameraUtils.OpenMarketingCamera()                                 -> System.Void
CameraUtils.CloseMarketingCamera()                                -> System.Void
CameraUtils.EnableDOF(Beyond.Gameplay.CameraDOFDescriptor)        -> System.Void
CameraUtils.GetDOFData()                                          -> HG.Rendering.Runtime.HGDepthOfFieldData
CameraUtils.RecenterCamera()                                      -> System.Void
CameraUtils.ResetCameraPosition()                                 -> System.Void

CameraManager.GetMainMarketingCameraController()                  -> MarketingCameraController
CameraManager.GetMainLevelCameraController()                      -> LevelCameraController
CameraManager.SetOverrideFOVForCurrCamera(System.Single)          -> System.Void
CameraManager.ToggleSnapshotCamera(System.Boolean, System.Boolean)-> SnapshotCameraController
CameraManager.CreateOrGetTemporaryController(UnityEngine.GameObject) -> CameraControllerBase

MarketingCameraController.ChangeFov(System.Int32)                 -> System.Void
MarketingCameraController.SetMoveInput(System.Single, System.Single) -> System.Void
MarketingCameraController.SetRotateInput(System.Single, System.Single) -> System.Void
MarketingCameraController.Zoom(System.Single)                     -> System.Void

SnapshotCameraController.SetAperture(System.Single)               -> System.Void
SnapshotCameraController.SetFocusDistance(System.Single)          -> System.Void
SnapshotCameraController.SetAdditiveFOV(System.Single)            -> System.Void
SnapshotCameraController.SetZoomScale(System.Single)              -> System.Void
SnapshotCameraController.GetZoomScale()                           -> System.Single
SnapshotCameraController.UpdateSensorSize()                       -> System.Void
SnapshotCameraController.ActivateSnapshotCamera()                 -> System.Void
SnapshotCameraController.DeactivateSnapshotCamera()               -> System.Void
SnapshotCameraController.ApplySnapshotDofSettings()               -> System.Void
```

### 由此纠正的三个错误假设

| 接口 | 错误假设 | 实测真相 | 后果 |
|---|---|---|---|
| `MarketingCameraController.ChangeFov` | 以为是 float 增量 | 是 **`System.Int32`** | 传 `-15.0f` 时位模式 `0xC1700000` 被当作 `-1047527424`，FOV 被巨大负速率拖成负数 → 投影矩阵失效 → **画面全灰** |
| `CameraUtils.EnableDOF` | 以为传 float/bool 即可 | 需要 **`CameraDOFDescriptor` 对象** | 传浮点会按对象指针解引用 → 崩溃风险 |
| `CameraManager.CreateOrGetTemporaryController` | 以为收 `Type` | 1 参重载收 **`UnityEngine.GameObject`** | 按 Type 传参 → 崩溃风险 |

**结论**：调用任何托管方法前，先用元数据核对 `method_get_param` 的参数类型；不符就**不调用**。
这条纪律在实机上拦下了一次必然的崩溃。

---

## 二、拿到快照（拍照）相机实例的正路

`SnapshotCameraController` 是唯一提供光圈/对焦的类，但它是实例对象。实测可行的获取顺序：

1. `CameraManager.ToggleSnapshotCamera(true, false)` —— **它的返回值就是 `SnapshotCameraController`**。
   实测返回 `0x4fc7eee60`，紧接着 `GetZoomScale()` 返回真实值 `0.4800`，证明是活实例。
   副作用：进入拍照模式。
2. `Object.FindObjectsOfType(Type)` / `Resources.FindObjectsOfTypeAll(Type)` —— 本作里解析成功，
   但实机返回 **null**（不可用）。
3. `Resources.FindObjectsOfTypeAll` 同理不可用。
4. **不可用**：`Object.FindObjectOfType(Type)`（已被托管剥离删掉）、
   `Object.FindObjectOfType(Type,bool)`（解析得到但调用抛托管异常）。

### 严重教训：不要在自举/探测阶段自动调用 `ToggleSnapshotCamera`

在标题画面自动调用它之后，关卡加载阶段出现：

```
at Beyond.Gameplay.View.CameraControllerBase.Init (CameraManager cameraMgr)
at Beyond.Gameplay.View.CameraManager.LoadPersistentController (GameObject, Boolean)
at Beyond.Gameplay.Core.GameLevelLoader+LoadingPipeline+StartLevelStep+<DoExecuteAsync>d__12
```

游戏**卡死在 loading 界面**。它改动了相机管理器状态，破坏了关卡加载时的相机初始化。

**规则**：进入拍照模式必须由用户显式触发（指令 27 / `tools\arm_lens.ps1`）；
自举探测阶段只能做只读操作。

---

## 三、已实测生效的三条镜头控制路径（这就是"焦距 + 光圈"）

> **v1.0.0as 补记（关键，2026-09-15 实测定案）**：`SetAperture` / `SetFocusDistance` **只是"写数值"**，
> 必须再调一次 **`SnapshotCameraController.ApplySnapshotDofSettings()`** 才会把景深**激活并挂到主相机**上
> —— 只有游戏自己进拍照模式时才会做这一步，**这就是"光圈只在进官方相机后才生效"的全部原因**。
> 实测（指令 77 双向对照）：`f/1.4 + Apply` → 明显虚化 ✓；`f/22 + Apply` → 几乎全清楚 ✓；
> 而**已激活之后**只 `SetAperture` 不 Apply → **仍然实时生效** ⇒ Apply 的作用是"激活这条挂接"。
> 另有配对方法 **`ResetSnapshotDofSettings()`**（恢复游戏默认）。
>
> 两个工程级坑（都实测踩到）：
> 1. **实例会陈旧**：进一次官方相机（拍照/营销）后游戏会换一个 `SnapshotCameraController`，
>    我们手里那个之后再 `SetAperture`/`Apply` **不报任何托管异常、日志正常，但画面毫无反应**；
>    修法 = **重新取一次实例**（`CameraManager.ToggleSnapshotCamera(true,false)`，即武装第一步，指令 78）。
> 2. **写入要节流**：每个数据包都写一次（一秒 6~7 次）会让画面"抽动"；实测节流到 400ms 一次即可。

| 能力 | 接口 | 实测证据 |
|---|---|---|
| 焦距 → 视场角 | `CameraManager.SetOverrideFOVForCurrCamera(float)` | 视觉变焦可见；Blender 85mm → FOV 16.07 已下发 |
| 光圈 | `SnapshotCameraController.SetAperture(float)` | f/1.4 背景明显虚化；改 f/16 虚化消失（双向可证） |
| 对焦距离 | `SnapshotCameraController.SetFocusDistance(float)` | 调用无异常，参与景深 |
| 变焦 | `SnapshotCameraController.SetZoomScale(float)` / `GetZoomScale()` | 读回 0.4800 |

FOV 与焦距换算：`FOV = 2 * atan(传感器高 / (2 * 焦距))`（本作 Unity 相机 `sensorSize = (36, 24)`，
故用高度 24mm 做垂直 FOV）。

### 关键可用组合（拍片工作状态）

**官方自由相机 + 景深可以共存**：`OpenMarketingCamera()` 打开自由相机后，
快照相机的光圈仍然作用于画面。实测确认："是自由相机视角，而且虚化还在"。
→ 也就是"边用自由相机走位、边由 Blender 控制光圈"这个状态是成立的。

---

## 四、Unity `Camera` 上不存在的属性（托管剥离）

实测 `UnityEngine.Camera` 上**没有** `get_aperture` / `set_aperture` /
`get_focusDistance` / `set_focusDistance` —— 游戏的 IL2CPP 托管剥离把它们删掉了。

仍然可用（已读回实测值）：

```
fov=60.000 physical=0 focal=50.000 sensor=(36.00,24.00) clip=(0.100,10000.000)
```

所以光圈**不能**走 Unity 物理相机，只能走游戏自己的景深系统。

---

## 五、自由相机（营销相机）：位姿控制的接口

```
MarketingCameraController.SetMoveInput(float x, float y)     移动（模拟输入）
MarketingCameraController.SetRotateInput(float x, float y)   旋转（模拟输入）
MarketingCameraController.Zoom(float)
MarketingCameraController.ChangeFov(System.Int32)            速率(注意是 int, 且见上文灰屏)
```
另有 `Attach()` / `AttachToPoint(1)` / `AttachToGroup(1)` / `EnterRollMode()` /
`ChangeMoveSpeed(1)` / `ChangeRotationSpeed(1)` / `ChangeTimeScale(1)` 等。

**为什么要用模拟输入而不是写 transform**：直接写 `Camera.main.transform` 会与
Cinemachine（`CameraManager.cinemachineBrainCpt`）冲突，每帧被 Brain 覆盖，表现为画面"虚空"。
用模拟输入做闭环伺服不写 transform，从根上避开这个冲突。

闭环方案：读 `Camera.main` 当前位姿 → 与 Blender 目标位姿求误差 →
按比例喂 `SetMoveInput` / `SetRotateInput`。**这一部分尚未实现**。

---

## 六、IL2CPP 加固环境的血泪纪律

### 1) 绝对不要读写托管对象的字段内存

v2 / v3 用 `il2cpp_field_get_value` + `il2cpp_class_get_field_from_name` 做字段反射，
**两次把游戏打崩**（访问违例）。崩溃栈明确落在本模块内：

```
il2cpp_class_get_field_from_name          (GameAssembly)
  EndfieldCamLink ×2                       (本模块)
    ecl::GameSysDumpNamedFields()
      ecl::OnGameTick()
        TimeManager::TailLateTick
```

即使先按 `field_get_type` / `class_is_valuetype` / `class_value_size` 算好长度再读，
对加固版本依然不可靠。**结论：整体放弃字段反射。**
只保留两类操作：① 已解析托管方法 + `runtime_invoke`；② 只读元数据枚举（类名/方法名/字段名）。

### 2) 启动期必须给自举线程做 GC 注册

`Collecting from unknown thread` 是本作已知的偶发致命错误
（`Endfield.gc_log` 的创建时间是 2026-07-29，早于任何注入尝试，内容是同一句）。

缓解措施（已实施，v4 起稳定）：自举线程在启动后**完全静止 8 秒**
（IL2CPP 初始化最脆弱的窗口不碰任何 VM 导出），一旦 `il2cpp_domain_get()` 可用就
**立刻 `il2cpp_thread_attach(domain)`** 把自己注册进 GC 域，然后再做别的。

### 3) 命令包与参数类型
UDP 指令：`ECMD`(12 字节) = magic + u32 id + float；`ECM2`(24 字节) = magic + u32 id + 4×float。
托管调用必须在 Unity 线程（锚点 `CameraManager.TailLateTick`）上执行。

### 4) 绝对不要在每帧数据流里做"对象查找"

这是第三次崩溃的根因，形态和前两次完全不同（**调用栈里没有本模块**，所以更难查）：

```
[Error] FindAllObjectsOfType: The type has to be derived from UnityEngine.Object. Type is Boolean.
        （重复数千次）
[Critical] Commit memory failed, error code is: 0x1000000
崩溃栈 28 帧全部是 unityplayer（无 EndfieldCamLink 帧）
```

**因果链**：快照相机实例未武装时，`ApplyShmemLens` 每帧都去调
`Resources.FindObjectsOfTypeAll(Type)` 兜底找实例。该接口会枚举**场景内全部已加载对象**，
每次调用产生大量托管分配。Blender 以 60fps 推送 → 每帧都在枚举整个场景 →
内存被吃光 → `Commit memory failed` → 引擎崩溃。Player.log 因此涨到 1.3MB。

**规则**：
- `Object.FindObjectsOfType(Type)` / `Resources.FindObjectsOfTypeAll(Type)` /
  `Object.FindObjectOfType(Type,bool)` —— 本作里都**不可用**（返回 null / 抛异常 / 报上述 Error），
  且成本极高。**已从代码中彻底删除**。
- 任何"查找/枚举/遍历"类操作只能由**显式指令**触发，绝不可挂在每帧回调或数据流上。
- 快照相机实例只有一条获取路径：`ToggleSnapshotCamera(true,false)` 的返回值，
  且只由 `tools\arm_lens.ps1`（内部是指令 27）显式触发。
- 参数变化去重必须**先记录再尝试**，否则失败时每帧重试同样会刷屏甚至拖垮进程。

### 5) 共享内存通道是单一命名映射，没有写入方标识

`EndfieldCameraBridgeV1` 只能有一个写入方。若同时有两个 Blender 实例在发送：

- 两边 `_sequence` 都从 0 各自累加，序号互相碰撞；
- 读端的去重判断 `seq == g_lastSequence` 因此几乎每帧都判定为"新帧"；
- 现象：两边参数被交替应用，**画面在两个值之间来回跳**，模块日志暴涨
  （实测 1408 帧刷出约 3800 行；单写入方时 12 秒增长 0 字节）。

实测取值序列可以直观看到这种交替：

```
焦距 135.00 / SetAperture(2.80) / 焦距 85.00 / SetAperture(1.40) /
焦距 50.00  / SetAperture(2.80) / 焦距 85.00 / SetAperture(1.40) ...
```

**使用规则**：发送中的 Blender 只保留一个。
**待改进**：给包加写入方标识或按 `timestamp` 单调性过滤，并把序号从共享内存里
做全局自增（而非各进程自增），即可根治该问题。

### 6) 映射是持久的，启动时会先吃到上一轮的陈旧包

共享内存对象在最后一个句柄关闭前一直存在。因此新会话启动时，
读端会把映射里残留的**上一轮最后一包**当作新帧应用一次
（表现为游戏 FOV 一开始就跳到一个旧值）。
**待改进**：校验 `timestamp` 新鲜度（例如只接受最近 200ms 内的包）。

### 7) 位姿写入必须挂在 CinemachineBrain.LateUpdate 之后

实测帧内顺序（决定性发现）：

```
CameraManager.TailLateTick(我们原锚点)  →  ...  →  CinemachineBrain.LateUpdate  →  渲染
```

- 在 `TailLateTick` 里写 `Camera.main.transform`，会被随后运行的 Brain **每帧覆盖**，
  表现为"位置完全不动"（写是写进去了，读回也对，但渲染用的是引擎的值）。
- 正解：钩住 `Cinemachine.CinemachineBrain.LateUpdate`（程序集 `Cinemachine.dll`，
  注意 v2/v3 命名空间分别是 `Cinemachine` / `Unity.Cinemachine`），
  在**调用完原函数之后**写，这是渲染前最后的时机。

由此还引出一个"读数时机"陷阱：

```
在 TailLateTick 里读 Camera.main.transform.position
  → 读到的是我们上一帧自己写进去的值, 不是引擎位姿!
```

实测后果：相对基线被污染成 `游戏 pos=(0.00,0.23,6.91)`（那是我们自己的写入）。
**正解**：把 Brain 钩子里"写入前"读到的值缓存下来，作为引擎位姿的唯一来源。

### 8) 位姿必须是「相对基线」，否则画面虚空

Blender 场景坐标与游戏世界坐标毫无关系。实测把 Blender 的绝对坐标直接套上去，
相机被搬到离关卡约 400 单位处（`Y≈0`，落在地形内）→ 画面虚空/发灰。

```
目标 = 游戏基线 + (Blender当前 − Blender基线)      // 位置
q目标 = (qBlender × conj(qBlender基线)) × q游戏基线  // 旋转：仅"相对·世界系增量"档位（见 §8b）
```

配套两条硬性要求：
- **基线不能被高频重建**。曾写成"每帧丢帧就重建基线"，结果用户刚做的位移被当成新基线
  吸收掉 → 表现为"怎么动都不跟随"。改为**只在断流 >3s 时重建**。
- **必须有断流安全阀**：数据流停 >500ms 就停止写位姿、交还游戏相机。
  否则相机会永久卡在最后写入的位置，若那位置在关卡外玩家会一直看虚空且无法恢复。

### 8b) 旋转必须用「绝对」，不能跟着位置一起相对（2026-09-10b 修正）
上面那条公式用于**位置**是对的（也仅用于位置）。同一套"增量叠加"用在**旋转**上会错：

```
q目标 = (qBlender × conj(qBlender基线)) × q游戏基线     // 错
```

因为两侧基线朝向天然相差一个常量角 Δ（Blender 相机朝向是构图选的，游戏相机朝向是游戏给的），
游戏相机实际绕的轴 = Blender 本地轴再被 Δ 转一次 → 俯仰混进滚转、三轴互串，
用户表现为"绕 xyz 转不是标准直角坐标系，很难操作"。

**正解（v1.0.0at 起的三档，默认第 1 档）**：
1. **相对·标准（默认）**：旋转与"绝对"一致（等价武装时对齐基线 Δ=0），两系同时一一对应；
   增量只作用在位置上 —— 这与 v16 的结论"位置用增量是对的，旋转不能"一致。
   `pose_rot_absolute=false` + `pose_rot_increment_frame=0`（指令 38 `-A0 0`）。
2. **相对·世界系增量**：`q目标 = (q_now × conj(q_base)) × q游戏基线`（指令 38 `-A0 2`）——
   绕 Blender **世界轴** 0.00° 且换姿态不漂移；代价是绕相机**本地轴**偏 52.6~63.1°（俯仰带出滚转）。
3. **相对·本地帧增量（1.0.0z 语义）**：`q目标 = q游戏基线 × (conj(q_base) × q_now)`（指令 38 `-A0 3`）——
   绕相机**本地轴** 0.00°；代价是绕 Blender **世界轴**偏 38.8~76.9°（换姿态最大漂移 87°）。

**为什么必须二选一**：相对模式的零点"游戏相机自己的基线朝向"与映射后的 Blender 基线相差 Δ
（现场实测 53.3°/89.0°）。增量四元数只能左乘（世界系）或右乘（相机本地系），所以 Δ≠0 时
**不可能两系同时 1:1**；Δ=0 时四种写法全部 0.000°。这也是"改 Blender 端轴朝向修不了"的原因：
偏差随姿态变化（不是常量轴重标），且 Blender 端轴约定正是绝对映射 1:1 的成因，动它会歪掉绝对映射。

数值验证结果（复刻 `_to_unity` + `LookRotation` + 四元数组合；Blender 基线俯角 30°、
游戏基线方位 −120°）：

| Blender 侧动作 | 增量叠加（旧） | 绝对（现行） | 本地帧增量（1.0.0z） |
|---|---|---|---|
| 绕本地 X 俯仰 10° | 游戏转轴 (0.45,−0.76,−0.46)，偏离自身 X 轴 63° | (−1,0,0)，纯俯仰 | (−1,0,0)，纯俯仰 |
| 本地 X / Y / Z 三轴响应 | 三轴互相串 | 0.00° / 0.00° / 0.00° | 0.00° / 0.00° / 0.00° |
| 世界 X / Y / Z 三轴响应 | 0.00° / 0.00° / 0.00° | 0.00° / 0.00° / 0.00° | **38.81° / 65.70° / 76.88°** |

**符号约定**：Unity 左手系、Blender 右手系，同一物理转向在两边的欧拉/四元数符号相反
（Blender 本地 X 正转 = 抬头，Unity 对应负角 = 同样抬头）。**以物理方向一致为准，不要比对数值。**

### 8c) 拍照相机本来就是「角色 + 相机偏移」模型（2026-09-10c 发现）

运行时符号清单里，`Beyond.Gameplay.View.SnapshotCameraController` 的成员直接说明了拍照
相机的内部模型：

```
M GetCameraOffset(0)      M SetCameraOffset(1)     M AddCameraOffset(2)
M GetCameraRotation(0)    M SetCameraRotation(2)   M GetCameraRoll(0) / SetCameraRoll(1)
M OnMainCharacterChange(1)  M _HideChar(0) / _ShowChar(0)
F m_cameraOffset  F m_postRotation  F m_invisibleMainChar  F offsetSpeed  F rotationSpeed
```

即：**相机位置 = 角色位置 + 相机偏移**。推论：

- 「游戏内相机 ↔ 角色」的相对位置是**游戏原生概念**，不需要自己去反推角色 Transform；
- 取角色位置：`角色位置 = Camera.main.transform.position − GetCameraOffset()`；
- 也可以直接用 `SetCameraOffset(Vector3)` 让游戏自己把相机摆到"角色 + 偏移"处
  （比每帧改写 `Camera.main.transform` 更贴近原生语义，是后续可选的改进方向）。

相关类（`Beyond.Gameplay.View`）：`CameraControllerBase`（含 `get_cameraTrans`）、
`LevelCameraController`、`MarketingCameraController`、`DynamicTargetFreeLookCameraController`、
`CameraVirtualTargetNode`（含 `get_target` / `_GetTargetPosition`）、
`CameraManager`（含 `get_curVirtualCam` / `SetFollowOffset` / `SetLookAtOffset`）。

**当前实现**用的是"游戏基线点"这条路：武装那一刻游戏相机的位置（拍照模式下即相对角色的位置）
作为游戏侧原点，Blender 侧原点用参考物件（初始方块）。理由：只依赖已验证可行的
`Camera.main.transform` 写入路径，不必猜测偏移量的坐标系与平滑语义。
`GetCameraOffset` 已解析并用于诊断（指令 39 打印"推导角色位置"）。

### 9) `il2cpp_init` 钩子抓不到 —— 只能用时间兜底

原计划用 `il2cpp_init`（GameAssembly 导出，`GetProcAddress` 即可）精确判定 VM 就绪：
回调返回后才允许碰 IL2CPP 接口，从根上避免"与 `il2cpp_init` 并发访问未初始化 VM"。

**实测失败**：`il2cpp_init` 与 `il2cpp_init_utf16` 两个钩子都能装上，但**从未被触发**——
UnityPlayer 走的既不是这两个导出（它们在 Windows 上只是包装）。

**现方案**：保守时间兜底 —— 等 `il2cpp_init` 最多 25s，然后退化为"导出可用 + 静置 3s"
才开始碰 VM。实测可稳定避开竞态（代价是启动后约 28s 才 Ready）。

### 10) MinHook 只能挂一个钩子的坑

`MH_Initialize()` 第二次调用返回 `MH_ERROR_ALREADY_INITIALIZED`。
早期 `InstallHook` 把它当失败直接 `return false`，**导致第二个钩子永远装不上**
（Brain 钩子就是这么失败的）。另外失败分支不能调 `MH_Uninitialize()`，那会把已装好的钩子一起拆掉。

---

## 八、验收状态（2026-09-10）

已在实机完成端到端验收：**在 Blender 里改 `Focal Length` 与 `F-Stop`，游戏画面实时跟随。**

```
[lens] 焦距 135.00mm + 传感器高 24.00mm -> FOV 10.16 (已应用)
[gamesys] SetOverrideFOVForCurrCamera(10.16)
[lens] 光圈 f/2.00 (来自 Blender) 已应用
[lens] 对焦距离 6.000 m (来自 Blender) 已应用
```

压力测试：连续 30 秒推送 1408 帧 + 4 次参数切换，游戏内存 9168 → 9205MB
（仅正常堆波动），无崩溃、无失控增长。

---

## 七、帧末锚点

`CameraManager.TailLateTick(float)` —— 任何相机模式下每帧运行，位于帧末，是最佳锚点。
备用：`CameraMono._ProcessDitherByPitch`（仅角色相机模式有效）。
