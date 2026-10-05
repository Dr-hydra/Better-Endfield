# 游戏原生模型加载与 BEM 分帧方案（2026-10-03）

**建议先实现 BEM 自己的主线程分帧构建队列和提前预热，再验证严格延迟交付。** 原生资源请求、优先级调度、Unity AssetBundle 异步加载及下一帧完成回调已有静态确证；这不等于原生每张角色纹理都启用了 Unity async upload，也不等于运行时 `Texture2D.Apply` 会自动进入该管线。

本轮只研究：基线 `main dd7131b4` 加当时工作区改动，已读 `android/AGENTS.md`；只新增本文和 ignored `artifacts/native-loading-20261003/` 的研究材料。未修改生产代码、配置或包，未启动游戏、操作手机、发送外部消息；不涉及另一个代理负责的世界资源命名。

## 单次问题的证据边界

现有 `artifacts/model-upload-20261003/device-current-peak-diagnostics.log`：

| 行号 / 资源 | Texture 构造 / Apply | Texture 请求量 | Mesh 请求量 | 事务耗时 |
|---|---:|---:|---:|---:|
| 3438 / world | 8 / 8 | 387,856,640 B，369.89 MiB | 12,294,548 B，11.72 MiB | 1356 ms |
| 3561 / UI | 8 / 8 | 同上 | 同上 | 1637 ms |
| 3619 / 再次 UI | 8 / 8 | 同上 | 同上 | 850 ms |

这些是**三次独立构建**；用户观察的是**每次约 0.9GB 峰值后回落**，不能把三轮上传量相加当单次峰值。此次实际是 8K ASTC，最大单张 67,108,864 B（64 MiB）；八次 `built t=` 均为 `mips=1`。

当前 `CreateTextureFromBem` 连续执行 `.ctor(width,height,format,mipCount,linear)` → `LoadRawTextureData(IntPtr,size)` → `Apply(false,true)`；`PrepareResource` 连续处理所有组件，`ResourceFinish` 等整个事务结束后才调用游戏原函数。分别见 `native/modules/custom_model/module.cpp:1486、1906、2755、2880`。已有日志没有逐调用计时，**不能把整个 850–1637 ms 都归到某一次 Apply**，但同步交付栈内确实没有跨帧让出的步骤。

## 新核实：构造期默认初始化 / 上传候选

**当前本机 dump 没有 `createUninitialized=true` 重载，也没有 `DontUploadUponCreate` / `DontInitializePixels` 枚举成员。** 新版 UnityCsReference master 确实有它们，不能把新版接口当成本机或手机已可用的接口。

| 本机 D/UnityEngine.CoreModule.dll.cs | 完整参数签名 / 证据 |
|---|---|
| 9561 行，当前生产五参数 ctor | `System.Int32\|System.Int32\|UnityEngine.TextureFormat\|System.Int32\|System.Boolean`，返回 `System.Void`，PC RVA `0x0A33E24C` |
| 9559 行，旧版内部六参数 ctor | 上述五参数后是 **`System.IntPtr nativeTex`**，PC `0x041CB140`；不是 `System.Boolean createUninitialized` |
| 9557 行，可解析的 flags 候选 ctor | `System.Int32\|System.Int32\|UnityEngine.Experimental.Rendering.GraphicsFormat\|System.Int32\|UnityEngine.Experimental.Rendering.TextureCreationFlags`，返回 `System.Void`，PC `0x0A33E1B8` |
| 9551 / 9471 / 9473 行 | 内部 GraphicsFormat ctor、`Internal_CreateImpl`、`Internal_Create` 均接收 flags；PC `0x0358D1C0`、`0x0358D360`、`0x0358D310` |
| 16000–16008 行 | 当前枚举仅有 `None / MipChain / Crunch / HGDisableDefragmentation`，dump 不提供这些 const 的数值 |

本机定点反汇编确认：生产五参数 wrapper 调用旧内部 ctor；内部 ctor 在 `mipCount=1`、非 Crunch（本次 ASTC）分支把 flags 置 0，再交给 `Internal_Create`。GraphicsFormat flags 重载会把调用者 flags 传入同一创建链。`Internal_CreateImpl` 最终进入解析出的引擎 icall；**本轮没有其底层初始化、GPU 分配或提交行为证据**。窗口保存在 `artifacts/native-loading-20261003/Texture2D-*.asm.txt`，由 `inspect_texture_creation.py` 按 dump 中精确签名生成。

[UnityCsReference 2021.3 / Texture.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/2021.3/Runtime/Export/Graphics/Texture.cs#L756) 的对应 ctor 只组合 `MipChain / Crunch`；其 [GraphicsEnums.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/2021.3/Runtime/Export/Graphics/GraphicsEnums.cs#L568) 中 `DontInitializePixels = 1 << 2` 只是“内部使用”的注释，未公开定义，且没有 `DontUploadUponCreate`。

本轮读取的 [master / Texture.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/88ce7b60434ba7a8ca0218590a4cb509971788ad/Runtime/Export/Graphics/Texture.cs#L869) 则明确：`createUninitialized=true` 会组合 `DontUploadUponCreate | DontInitializePixels`；false 路径不加它们。[master / GraphicsEnums.cs](https://github.com/Unity-Technologies/UnityCsReference/blob/88ce7b60434ba7a8ca0218590a4cb509971788ad/Runtime/Export/Graphics/GraphicsEnums.cs#L626) 定义了 `1 << 10` 与 `1 << 2`。这是**新版托管代码设置跳过标志的证据**，不证明游戏定制的 2021.3 底层也认同这些位值，更不证明它会保留 raw 写入所需的 storage。源码快照和来源记录保存在研究目录。

结论：**“五参数 ctor 先做默认像素初始化/首次上传，随后 raw + Apply 重写”是值得优先验证的单次峰值候选；当前只确认未请求跳过，并未确认发生两次 GPU 上传。** 现有 `texturePayloadBytes` 只统计 raw + Apply 请求，不能测出 ctor 的默认像素成本；不能据此把一次 369.89 MiB 直接翻倍解释 900MB。

实施顺序：下次验证先分开测 ctor / raw copy / Apply 的时长与单次内存时间线。若手机精确元数据有六参数 bool 重载，才优先评估 `createUninitialized=true`；若只有 GraphicsFormat + flags 重载，需先证明该引擎支持对应 flags，再用正确 ASTC GraphicsFormat、sRGB 与 mipCount 创建。**不得从 master 硬编码 `0x404`、猜 Android RVA，或把 `HGDisableDefragmentation` 当成跳过初始化。** 即使证实能跳过创建期工作，它也不会让最后 Apply 自动 async，分帧方案仍然有必要。

## 新核实：安卓诊断与 production 默认

| 路径 | 默认执行条件 | 是否 Blit / ReadPixels |
|---|---|---|
| `AndroidAuditNormalTexture`，android_mesh_builder.cpp:172 | `inspection_enabled=true` **且**原纹理名精确等于 `T_actor_endminf_cloth_01_N`；进程内最多一次尝试 | 是：64×64 临时 RT，Graphics.Blit → 同步 ReadPixels → 九点 GetPixel，最后释放临时对象 |
| `AndroidAuditMaterialCopy`，同文件:108；module.cpp:1828 | Android Draw 材质复制路径执行，无 inspect guard | 否；检查 shader / queue / keywords / 已知 scalar、color 属性，有多次主线程 Invoke 和日志 |
| `AndroidAuditTextureColorSpace`，同文件:157；module.cpp:1800 | 每个该路径新建的替换 Texture 执行，无 inspect guard | 否；读取 GraphicsFormat、判断 IsSRGBFormat 并记录日志 |
| `InspectAndroidRenderers`，module.cpp:2488 | `AndroidInspectionEnabled()` 为真，且有 completed 资源；五秒限频 | 否；枚举 renderer / mesh / 可见性 |

`inspection_enabled` 初始 false；`ConfigureAndroidMeshBuilder` 从 `ConfigValue(config,"inspect")=="1"` 设置（custom_model_module.cpp:463–464），header 默认参数也为 false。正常安装配置生成器 `BemInstalledResources.java:47–49` **没有写 `inspect`**，因此 production 默认不会执行上述法线 Blit/ReadPixels。

本次 Aglina 单轮 8K 上传的纹理名也不符合 Endminf 限制，即使另有 `inspect=1`，该法线采样路径仍不能解释这几个 Aglina 事务。当前日志有 material copy / colorspace 审计，不能将这些日志误读成执行了同步 GPU 回读。它们的重复属性查询和日志可能增加 CPU 耗时，需逐阶段计时后判断占比；不据此认定是 900MB GPU 峰值来源。

## 原生具名链路：哪些已经确认

证据来自已有 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/`，下文简称 D；定点核对本机 `D:\Arknights Endfield\GameAssembly.dll`。DLL 的 ImageSize 为 `0xF7CC000`，与已有 dump 日志一致；UnityPlayer 文件版本为 `2021.3.34f5`。这属于 **PC 静态证据**，没有验证手机当前 ARM64 方法 ABI 或游戏 IFix 热补丁的实际分支。地址只用于复核，不得移植为 Android RVA。

```text
I18NAssetLoader.LoadAsync(path, type, category)
  → ResourceManager.LoadAsync(path, type, category, priority)
  → BundleResourceManager.LoadAsync → _LoadAssetInternal
  → BundleLoader.Manager.LoadAsset → _LoadAssetProxy
  → 资源 operation 调度 / AssetProxy.LoadAsync
  → BundleProxy.LoadAssetAsync → Unity AssetBundleRequest

AssetProxy.UpdateLoading
  → _FinishWithAsset(asset)
  → _OnAsyncCompleted
  → Manager.RegisterOnCompleteInNextFrame
  → Manager.LateUpdateAll / _InvokePendingNextTickOnCompleted
```

前半的 operation 调度存在同步、异步和优先级分支；图不表示所有请求必然走异步。

| 定点证据（D/Common.Beyond.dll.cs） | 确认的行为 / PC RVA |
|---|---|
| `I18NAssetLoader.LoadAsync`，64451 行 | 三参数、返回 `FAssetProxyHandle` 的非泛型重载确实调用 `ResourceManager.LoadAsync`；`0x05BC2B24` → `0x0355FB50` |
| `BundleResourceManager.LoadAsync` / `_LoadAssetInternal`，67346 / 67374 行 | `0x03038BC0` → `0x0303A610` → `Manager.LoadAsset 0x0303A9D0`；并构造 tracked handle |
| `Manager.LoadAsset` / `_LoadAssetProxy`，5001 / 5003 行 | 使用 `BundleProxyContainer.GetOrCreateAssetProxy`，再进入 `0x0303ABF0`；存在 operation 入队、优先级提升及强制同步分支 |
| `AssetProxy.LoadAsync`，4081 行 | `0x03824850` 调用 `BundleProxy.LoadAssetAsync(StringPathHash,Type) 0x038250E0`，保存 `AssetBundleRequest` |
| `BundleProxy`，5270 / 5276 行 | `_LoadAssetBundleAsync 0x05AEEA44` 调用 Unity `AssetBundle.LoadFromFileAsync`；字符串资源重载 `0x05AEDEE8` 调用 Unity `AssetBundle.LoadAssetAsync` / `LoadSubAssetAsync` |
| `Manager.UpdateAll` / `_UpdateOperationState`，4947 / 5037 行 | `0x03560900` 更新 loading/operation；`0x02C8EA30` 存在 legacy 与 `OperationPriorityScheduler.Update` 分支。未确认运行时采用哪种模式或并发数 |
| `AssetProxy.UpdateLoading`，4091 行 | **`0x02C8E023` 调用 `_FinishWithAsset`，紧接着 `0x02C8E02D` 调用 `_OnAsyncCompleted`**；这是延迟交付的关键约束 |
| `_OnAsyncCompleted` / 下一帧队列，4095 / 4991 / 4953 行 | `0x03170B60` 把回调交给 `RegisterOnCompleteInNextFrame 0x03170D70`；manager 的 `m_pendingNextTickOnCompleted`、`LateUpdateAll` → `_InvokePendingNextTickOnCompleted 0x03560770` 有代码确证 |
| handle，65265–65296 行 | `isDone`、`hasError`、`Get()`、`AddOnProxyCompleted`、`Dispose` 均存在。`Get 0x03944C00` 的普通 AssetProxy 分支读取已存 asset；另有 `LoadImmediate 0x02E74E10` → `ConvertToSyncLoad 0x02E750C0`，不得用它轮询异步完成 |

实际类型是 `FAssetProxyHandle` / `FAssetProxyUntrackedHandle` 和 `IAssetProxy`，没有以名称相似为由假设另一种可直接调用的“FAssetProxy 上传器”。游戏队列调度的是 bundle / asset operation，**未找到能把一段 BEM ASTC 内存直接加入 GPU 上传队列的公开方法确证**。

研究脚本 `artifacts/native-loading-20261003/inspect_native_loading.py` 仅读取选定 dump、PE 头/异常目录与有限方法窗口；注释反汇编和索引在同目录。窗口可能包含邻近函数，也可能缺少分离的冷代码；本文只采用人工核对过的调用位置，未做全 DLL 扫描、复制或新 dump。

## Unity async upload / mip 流送：支持不等于正在使用

D/UnityEngine.CoreModule.dll.cs 有 `AsyncUploadTimeSlicedUpdate`（1245 行）、`QualitySettings.asyncUploadTimeSlice / asyncUploadBufferSize / asyncUploadPersistentBuffer`（6721–6723 行）和 streaming mip 设置（6731–6736 行）。`Texture2D` 有只读 `streamingMipmaps`、requested/loaded mip 状态及 `IsRequestedMipmapLevelLoaded`（9457–9465、9507 行）。**引擎暴露这些 API 已确认；当前手机开关、预算、原生贴图的 streaming 标志及实际上传路径未读取，留待实机。**

[Unity 2021.3 官方上传说明](https://docs.unity3d.com/2021.3/Documentation/Manual/LoadingTextureandMeshData.html)解释了真正的区别：满足导入/构建条件的原生资产可把 header 与 `.resS` 数据分开，用共享 ring buffer、多线程和多个帧上传。纹理条件包括非 Read/Write、非 Resources、Android 构建启用 LZ4。`asyncUploadTimeSlice` 是这条引擎管线的预算，不能当 BEM Apply 的限时器；buffer 设置也不是进程 GPU 内存硬上限。

同一官方说明要求异步 Mesh 没有 bone weights、没有 BlendShapes 等。**角色蒙皮 Mesh 不能仅凭 `LoadAssetAsync` 就判定使用了该标准异步 Mesh 上传管线**；游戏定制引擎是否另有路径，本轮没有证据。

[Apply 官方说明](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture2D.Apply.html)仅支持这里的确定结论：`Apply(false,true)` 保留现成 mip、不生成新 mip，并放弃 Unity 可读 CPU 副本。它没有为本次 BEM 路径提供可等待的上传完成 token，也未保证调用返回时 driver staging 已回收。运行时新建 Texture、复制 raw 数据后设置不可读，不能据此补齐原生 `.resS` 的导入/构建条件；改 `asyncUpload*` 或直接再 Apply 都没有“自动异步”的证据。

[mip 流送说明](https://docs.unity3d.com/2021.3/Documentation/Manual/TextureStreaming.html)要求纹理的导入配置和可流送 mip 数据。此次 BEM 是单 mip；即使调 `requestedMipmapLevel`，也没有较小 mip 可先交付。完整运行时 mip 链本身也不等于原生 streaming backing。若以后研究 bundle 预制方案，还需核对定制 Unity 版本、移动平台、序列化兼容及资源索引；不能承诺制作一个普通 AssetBundle 就能直接接入游戏。

原生**可能**更平稳的依据是 operation 调度、缓存复用入口，以及符合条件时的引擎共享上传缓冲；原版实际尺寸/格式/mip 和 BEM 也可能不同。尚无同场景原版 GPU 时间线，不能写成“原生绝不卡顿”或已确定 0.9GB 的唯一来源。

## 推荐实现：BEM 主线程预算 + 提前准备

把现有同步 `PrepareResource` 拆为持久 Job：`Decode/Plan → WaitDonor → BuildMeshes/Textures → Validate → Ready → Commit`。后台只做文件读取、Zstd 解压、纯数据计划/校验；所有 Unity 对象、材质、Mesh、Texture、绑定及销毁留在 Unity 主线程。既有格式支持、布局/骨骼/材质校验和 world/UI 联合事务继续作为发布条件。

1. **一个资源构建队列，共享每帧预算。** 优先当前可见角色/UI，其他任务等待；初始只允许一个活动上传 job。接在现有 `ResourcePump`（Canvas.SendWillRenderCanvases，module.cpp:2904），用帧号去重，因为 Canvas 回调不保证一帧只来一次；后续再验证覆盖无 Canvas 场景的 tick。
2. **字节、对象数、时间同时限制。** 起步用每帧 16–32 MiB、至多一张 Texture、主线程软预算 2–4 ms；耗时超预算立即留到下帧。构造、raw copy、Apply 的每一步都检查预算，可拆到不同帧，但不要同时创建全部空 Texture。Mesh 同样预算化，先按组件分批；单个 Mesh builder 也属于不可中断步骤。数值是待调参起点，并非实测结果。
3. **超大单张单独处理。** 64 MiB Texture 必须允许独占一个超预算步骤，否则队列永远不能前进。这组八张都大于 16 MiB，因此起步方案每帧最多处理一张，至少八个上传帧，实际还包括 donor、Mesh 和验证。单张 ctor/raw copy/Apply 仍可能造成长帧，已有 API 不能保证把一次 64 MiB Apply 切成 2–4 ms。循环 sleep、一次调用内重复 pump、或先把所有 raw 数据写完再集中 Apply，都不能实现真正跨帧降峰。
4. **限制提交密度，谨慎称为“在途预算”。** 大纹理后可先留一至两个实际渲染帧再发下一张；GPU 落后时仍可能叠加，不能把“等两帧”当上传完成 fence。submitted bytes 只记 API 请求量。新纹理最终约 369.89 MiB 的资源容量并不会因为分帧而消失，原版资源与新资源并存的部分也不会自动消失；目标是降低临时副本/driver staging 的同时叠加。
5. **持久所有权与一次发布。** Job 自己持有 decoded backing、donor tracked handle、asset/renderer/新对象强 GC roots、selection generation 和资源身份；GC root 不能代替游戏资源 retain。不能把栈上的 `ConstructionScope` 或其引用保存到下一帧：其析构会销毁未发布对象，`g_construction` 也只是 thread-local 当前 scope。Ready 后一次 `CommitResource`，失败逆序恢复；只清理本 Job 创建且未被发布引用的对象。

提前触发点可放在已有模型选择/已知资源请求阶段，使解压和 donor 等待先完成。Android 当前 `AndroidLoadUiDonor` 使用 **同步** `I18NAssetLoader.Load` + `Get`（android_mesh_builder.cpp:367–400），可研究改成三参数 `LoadAsync`，逐帧查询 `isDone / hasError`，完成后才 `Get`；持有 boxed handle root 并最终 `Dispose`。需要给 donor 请求设置仅获取原资源的构建上下文，避免其完成回调又触发一轮同步 BEM 准备。

预热完成时，正常资源交付只消费 Ready 结果并做整体绑定。冷请求早于 Ready 时，保留原版正常交付可作为首阶段退路；代价是本次替换可能推迟。若选择后补当前显示对象，必须走已验证的 live renderer 绑定事务，修改已交付 prefab 并不保证修改了所有实例。**必须首次显示即替换的需求，需要下面的严格延迟入口；单独增加后台解压或队列无法解决它。**

## 严格延迟交付：可设计，但还不能直接接入

**不要仅让 `_FinishWithAsset` hook 存下参数后返回。** 已核对的 PC 普通 `UpdateLoading` 随后仍会 `_OnAsyncCompleted`，把回调加入下一帧队列；若没有原 finish 设置 asset/status、清 request，就可能提前回调、反复完成或交付空对象。`RegisterOnCompleteInNextFrame` 是游戏自己的私有通知队列，不是可暂停任意长度的 BEM 上传任务入口。

候选入口是目标 `AssetProxy.UpdateLoading` 的完成通知之前：仅对已确认的异步请求，在 Unity request 已完成且可以获取 asset 后创建 Job，等待期间不执行该次原 `UpdateLoading`，让 proxy 保持 loading；每帧运行 BEM Job，Ready 时恢复一次原 `UpdateLoading`，由 `ResourceFinish` 消费已准备结果，然后游戏原 finish / onCompleted 顺序继续。**这是实现方案，尚未验证手机状态机允许这样等待。**

接入前只需要定点验证：

- ARM64 完整签名解析、request 字段与 `isDone` 查询、目标路径实际经过此入口；IFix 是否改变顺序。
- operation 更新是否允许多帧保持 loading，等待时是否有重复进入、超时、取消/换场景/换选项，以及 tracked handle 是否确实保护 asset/bundle 生命周期。
- `LoadImmediate`、`ConvertToSyncLoad`、`ForceFinishAsyncRequest`、manager 的强制完成/等待分支。同步调用可能阻塞帧泵，**等待 BEM 下一帧会死锁**；必须检测并取消该 Job 或走明确的同步退路，不能宣称全部请求都可无感异步化。
- world/UI donor 依赖不能形成“world 等 donor，而 donor completion 又等 world”的环；重入 guard、解绑和取消必须配对。

因此不建议本次直接挂新 hook 或写状态字段。现有 `Load/Get/Dispose` 有安卓生产使用与日志证据；`LoadAsync/isDone/AddOnProxyCompleted` 有 PC 声明/部分静态调用证据，手机的精确解析、线程与所有权还需验证。主线程帧泵和事务实现已有源码基础，但新增 Job 的跨帧生命周期也要验证。

## CPU 解压与 GPU 峰值分开验收

Zstd 解压、decoded cache 与输出 vector 复制属于 CPU 路径；移动最后一次消费的数据、共享 backing、后台解压可降低 CPU 副本或阻塞，却不会改变这轮 387,856,640 B 的 ASTC 纹理请求。ASTC 是 GPU 压缩块，不应再无证据假设成功路径必然 CPU 解码成 RGBA32。

分帧减少一次连续提交的密度，有机会降低 Unity/驱动暂存重叠；实际改善还取决于 renderer/GPU 进度。现有日志未测 GPU residency / staging，不能承诺 0.9GB 会降到某个固定值。事务内去重与后续 UI 成品复用可以减少真实新建数量，但本轮重点仍是**单次**上传峰值，不用它们替代节流方案。

后续获授权后只需一次固定包、固定场景的冷构建对照：记录最长主线程步骤、每帧提交量、单轮峰值和 Ready 延迟；已有事务总计继续使用。只有出现原生异步标记 `AsyncUploadManager.ScheduleAsyncRead / AsyncResourceUpload` 等，才认定该次资产实际进入 Unity async upload。若单张长帧仍不可接受，下一档方案是用户选择降低分辨率，或单独研究兼容原生 bundle/专用上传后端；现有 ctor/raw/Apply 路线无法承诺无卡顿。本轮未新增实机诊断或测试。
