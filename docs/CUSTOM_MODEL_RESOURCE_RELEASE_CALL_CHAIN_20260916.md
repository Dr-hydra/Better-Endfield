# CustomModel 正式版：资源加载调用链与分阶段确认

状态：设计已确认；正式资源实现已创建并接入 `module.cpp`，旧入口保留为 `module_legacy.cpp`。本轮离线验证通过，已部署到用户指定测试目录，实机由用户执行。正文的旧源码调用链仍作为迁移基线保留；当前实现、验证范围与剩余事项见 [重建进度与实机交接](CUSTOM_MODEL_RESOURCE_REBUILD_PROGRESS_20260916.md)。

日期：2026-09-16。源码基线：`dev/custom-model-design`，提交 `84b88bfb`。

## 1. 已确认的范围

- 正式版采用资源加载路径，在游戏交付目标模型资源之前完成替换。
- 不提供运行中的 Mod 热切换和用户回滚功能。
- 同一角色同时只能启用一个 Mod；当前阶段不支持热切换（用户已明确确认）。
- 先整理方法调用链，由用户确认关键点，再创建新的实现文件；各阶段分别确认。
- 所有工作留在 `.worktrees/dev-custom-model-design`，不混入 main。
- 沿用此前提出的性能目标：未用到的 Mod 保持懒加载，资源寿命尽量跟随游戏。
- LOD 机制提供独立开关：有 Mod 启用时强制打开；没有 Mod 启用时由独立开关控制。是否启用 Mod 取自有效启用配置，不等待角色资源加载或替换成功。

“不做回滚”不自动确定首次安装失败的处理方式，也不证明移除长期持有后 Unity 会及时释放资源。这两项单独确认。

## 2. 技术基线与证据边界

这是通过 Host ABI 和 IL2CPP 调用游戏的原生 C++ DLL，不是可编辑的 Unity 游戏项目。正式版继续使用现有 Host 接口、CMake 构建和 BEMPC24 v24 包格式，暂不引入新依赖或新格式。游戏内使用 HG 渲染与 NPC 管线；本轮没有重新确定 Unity 精确版本。

- `native/CMakeLists.txt` 当前以 `modules/custom_model/module.cpp` 构建 `BetterEndfield.CustomModel`。
- `module.cpp` 通过多个 `.inc` 拼接成一个翻译单元，甚至在文件边界关闭其他文件开始的函数。文件名不能视为真实模块边界。
- 当前资源模式仍要求 `target=both`；管理员和佩丽卡通过固定的两个 `CharacterSession` 管理。这是验证版限制，不是正式版设计结论。
- 包路径从 Host 的 catalog root 获取；`runtime.ini` 在初始化时读取。
- 用户已确认最近的资源路径版本：管理员详情模型显示正常，切人未感受到加载延迟。这是视觉反馈，没有量化帧耗时。
- 既有验证记录：18 个生命周期场景、15 个 Python 测试和实际佩丽卡包解析通过。它们不能证明真实 GPU 生命周期或自动卸载正确。
- 新鲜接收资源已经独立创建 Mesh；游戏从已修改模板自然克隆出来的对象仍可以共享模板资产。这种自然共享需要保留。

本文区分三种状态：**现状**来自当前源码；**建议**需用户批准；**未验证**需要游戏内证据。

## 3. 当前实际调用链

以下路径均相对 `native/modules/custom_model/`，符号名可直接搜索定位。

### 3.1 DLL 入口与初始化

```text
BetterEndfield_GetModuleApiV1                         [module_poc2_part_04.inc]
  → kApi.Initialize = InitializeWithAssetDeliveryTrace
    → 解析资源交付、克隆、人物初始化、材质等方法       [module_asset_delivery_trace.inc]
    → InitializeWithPreheatControlTrace              [module_preheat_control_trace.inc]
      → InitializeWithRendererArrivalTrace          [module_renderer_arrival_trace.inc]
        → InitializeWithNaturalResourceTrace        [module_natural_resource_trace.inc]
          → InitializeWithAiShadowCoverage          [module_ai_shadow_coverage.inc]
            → InitializeWithF12Dump                 [module_f12_state_dump.inc]
              → Initialize                          [module_poc2_part_03.inc]
                → LoadCharacterTarget               [module_character_target.inc]
                → ClearReplacementState（逐角色）
                → ResolveContracts
                → InstallPumpHook
                → StartHotkeyThread
                → ProbeIcallTable
    → 安装 Finish / Clone / Instantiate / SMS / 两种 PostDealLoadedModel hook
    → 两个角色 session.armed = true
    → g_delivery_enabled = true
```

这些包装层会通过 Host 代理拦截 `create_hook` 并替换 pump detour；并非互相独立的诊断文件。资源路径需要的 `g_natural_trace_get_components`、主线程判断和材质复制接口也来自这些层。

当前初始化不会在此处读取全部 BEM 数据；`BemPath()` 只是定位路径。真正读取在首次匹配资源后的 `ApplyReplacement`。

风险：当前交付 hook 分批安装失败后，为避免卸载仍被 hook 引用的 DLL，会返回成功并保持交付功能禁用。正式版必须明确安装失败与 hook 撤销策略，不能机械复制这段“成功但未启用”的处理。

### 3.2 资源交付主链

```text
BundleLoader.AssetProxy._FinishWithAsset(asset)
  → DeliveryFinishHook                               [module_asset_delivery_trace.inc:73]
    → MultiApplyModel(asset, "RESOURCE-BEFORE-FINISH", true)
                                                       [module_multi_path.inc:20]
      → 校验 resource 模式、主线程、重入锁
      → ObjectName → MultiCharacterForAsset
      → 校验角色 armed → CharacterScope
      → 校验暂停状态、方法可用性与最早允许帧
      → 临时 GCHandle：model、GetComponentsInChildren 返回数组
      → 枚举 SkinnedMeshRenderer（包含 inactive）
      → BuildTransformPath → IsTargetLod0Draw → SummarizeRenderer
      → 首次：ApplyReplacement(summaries)
      → 已激活：RefreshActiveReplacement(summaries)
      → 应用隐藏部件状态
    → g_delivery_original_finish(proxy, asset, method)
```

关键语义：

1. 替换在原始 `_FinishWithAsset` 调用之前，调用原函数后由游戏继续交付。
2. 当前匹配管理员／佩丽卡的 `postmodel`、`uimodel` 及它们的 `(Clone)` 精确名称。
3. 现状的非主线程交付直接跳过；重入也跳过。没有延迟队列或实例扫描补救。非主线程分支的历史触发背景尚未核实，不能据此认定正式版需要相同策略。用户于 2026-09-16 指定：仅记录此问题，正式版暂不实现“非主线程交付时跳过替换”。这也不代表已经确认回调一定在主线程，或允许在任意线程执行 Unity 调用。
4. 第二个 UI／世界资源到来时进入 `RefreshActiveReplacement`，不代表用户主动刷新。新实现必须保留“同角色不同资源分别处理”的能力。
5. 当前 gate 和日志有历史措辞，例如 F12／early replacement disabled；这些日志不能用于判断真实调用行为。

### 3.3 首次资源替换

`ApplyReplacement` 位于 [module_poc2_part_02.inc](../native/modules/custom_model/module_poc2_part_02.inc)。

```text
BemPath → LoadBemPoc → ValidateTargetPayload
  → 遍历 bem.components
    → MatchesTargetComponent（原始索引数及目标组件规则）
    → NoGeometry：记录为隐藏部件
    → ValidateRendererSkin + bones_count 校验
    → BuildMeshFromComponent(component, 当前接收者的原始 mesh)
    → SetSharedMesh + 读回验证
  → pending 移入 g_replaced，标记 active
  → 为 renderer / 原始 mesh / custom mesh 建立长期 GCHandle
  → PreparePelicaMaterials（当前双角色都会使用）
  → InstallModelTextures（整个模型一起处理）
  → ApplyMaskVisibility
  → g_active_bem = move(bem)，包数据留在角色 session
```

现状允许部分成功：组件失败后继续其他组件；材质与贴图失败也不一定使 `ApplyReplacement` 返回失败。不能把这个返回值当成“整个人物外观完整成功”。

### 3.4 同角色后续资源

`RefreshActiveReplacement`、`PrepareReplacement` 位于 [module_poc2_part_03.inc](../native/modules/custom_model/module_poc2_part_03.inc)。

- 先按 renderer 身份查现有记录；必要时恢复该 renderer 自己的 custom mesh／材质。
- 新鲜原始资源：从 `g_active_bem` 查组件，使用接收者自己的 Mesh 声明和 bindposes 创建新 Mesh。
- 已自然继承 custom mesh 的克隆：`ReplacementSource` 按实际 Mesh 对象识别来源；不再为它主动借用另一个原始资源的 Mesh。
- 收集本轮新准备的私有材质 renderer，循环结束后统一 `InstallModelTextures`。
- 旧资产代际、原始材质和骨骼等记录继续保留，服务于修复及回滚。这些状态不能原样进入不持久化的正式版。

正式版建议统一成“处理一个资源对象”的入口，不再使用“角色首次 / 角色刷新”两条构建逻辑。但重复交付、自然克隆、资源卸载后同名重载必须分别识别。

### 3.5 Mesh 创建：应保留的核心顺序

`BuildMeshFromComponent` 位于 [module_poc2_part_02.inc](../native/modules/custom_model/module_poc2_part_02.inc)，起始行 1135。

```text
ResolveEngineBindings
  → ReadVertexDeclaration(source_mesh) + ReadMeshStrides
  → 校验 BEM stride 等于实时 Mesh stride
  → 读取原始 bindposes，校验 max_bone
  → 创建 Mesh
  → 从 source native Mesh 复制 +0x1C8 字段
  → SafeSetVertexBufferParams（一次建立实时声明）
  → SafeSetVertexBufferData（上传原始打包数据流）
  → SafeSetIndexBufferParams / SafeSetIndexBufferData（UInt16）
  → subMeshCount=1 → SafeSetSubMesh（Triangles，baseVertex=0）
  → 设置原始 bindposes → RecalculateBounds
  → 校验顶点数、索引数、声明与 stride
  → 校验 has_bone_weights
  → SafeUploadMeshData（当前 UploadMeshData(false)）
  → 返回新 Mesh
```

- 不调用 `RecalculateNormals/Tangents`，避免破坏游戏的打包通道。
- 不重新启用第二套 `InternalSetBoneWeights` 数据；当前使用 packed skin stream。
- 佩丽卡实时 UV 流的 12 字节布局已经验证，不能按旧离线导出改回 8 字节，也不应对所有角色硬编码 12。
- `+0x1C8` 是当前客户端的原生布局依赖，需要版本／能力检查。现状的上传后字段差异只写日志，不拒绝返回，正式版的硬失败条件待确认。
- 当前构建函数未完整封装所有中间对象的临时 GC root。迁移时应逐一审计，不能声称旧函数已满足“不长期持有”的构建保护要求。
- 单 submesh、UInt16 等是当前格式与样本能力边界，不等于任意 Mod 都能接受。

#### 2026-09-16 补充：`m_BonesPerVertex` 现有实现核对

结论：当前实现使用固定的原生字段偏移 `+0x1C8`；从对应原 Mesh 动态读取的是字段值，没有在运行时计算该字段偏移。

- `module_poc2_part_02.inc` 的 `ResolveEngineBindings` 通过 Host `resolve_field` 查询托管 `UnityEngine.Object.m_CachedPtr` 的偏移；查询失败仍回退到固定 `0x10`。`GetNativeObjectPointer` 据此取得原生对象指针。这一步解析的不是 `m_BonesPerVertex`。
- `BuildMeshFromComponent` 构造新 Mesh 后，直接调用 `TryReadNativeUInt32(source_native + 0x1C8, ...)`，再调用 `TryWriteNativeUInt32(new_native + 0x1C8, source_value)`。读写 helper 使用裸内存访问、SEH 保护及写后回读，没有字段身份或版本动态解析。上传后的检查同样读取 `new_native + 0x1C8`，不一致仅记录日志。
- `mesh.get_bones_per_vertex_value` 按方法名解析 `Mesh.GetBonesPerVertexValue()`，用于查询和日志；它没有参与字段地址定位。既有原生研究表明该 getter 从 Mesh 数据对象／通道声明计算类别，与序列化字段 `m_BonesPerVertex` 的读取位置不同，不能直接替代该字段。相关证据见 [原生 Mesh 研究](CUSTOM_MODEL_MESH_NATIVE_FINDINGS_20260914.md)。
- `module_mesh_dynamic_trace.inc` 的诊断路径虽检查固定 icall RVA、动态解析 `m_CachedPtr`，其字段实验仍直接使用 `+0x1C8`；这些诊断保护不等于构建路径已经具备跨版本字段定位。

因此正式版要保留已确认的源值复制和初始化顺序，但不能将当前代码描述为“偏移已经动态计算”。原生字段动态定位仍需单独实现并验证；既有序列化函数中 `m_BonesPerVertex` 字符串及其相邻字段地址操作可作为后续调查入口，当前尚无已验证的通用解析器。

### 3.6 材质与贴图

```text
PreparePelicaMaterials
  → get_sharedMaterials → clone 材质数组
  → 对每个槽调用 Material(original) 复制构造
  → set_sharedMaterials
  → RetainAsset(copy / source)                         [现状的长期保护]

InstallModelTextures
  → GroupModelTextureRenderers → TextureModelRoot
  → 对每个根对象分组调用 InstallCustomTextures
    → BuildLiveTextureSlots
      → 管理员：UnionTextureComponentMasks（原贴图对象身份）
      → 佩丽卡：保留逐 draw 的组件范围
    → 查显式槽位 / 原贴图名 CRC pin；或范围、格式、尺寸匹配
    → 歧义时拒绝猜测
    → CreateTextureFromBem → CopySamplerState
    → material.set_texture_by_id → 读回验证
    → 记录原槽、创建纹理和长期句柄                    [现状的回滚状态]
```

必须保留的修复：按**模型根对象身份**分组；同一模型全部组件准备完成后再匹配贴图。管理员 C9/C10 共享贴图的范围需要合并为 `{9,10}`。分组件装贴图曾导致人物详情材质错误。

材质复制保留原 Shader 及未替换参数／贴图。佩丽卡 RabbitFX 到原生材质的转换仍属近似，并未重建原 Mod 全部 Shader。

`CreateTextureFromBem` 使用 Texture2D 构造、原始数据上传和 `Apply(false, true)`。创建结果仅在单次 `InstallCustomTextures` 内复用，当前没有跨资源对象的纹理缓存。

### 3.7 每帧路径与 LOD

当前最外层 pump：

```text
DetourPumpWithPreheatControlTrace
  → DetourPumpWithRendererArrivalTrace
    → DetourPumpWithNaturalResourceTrace
      → DetourPumpWithAiShadowCoverage
        → DetourPump → 原 Canvas 回调 → PumpCustomModel → PumpMultiCharacter
        → resource 分支：PumpNpcLodLock
          → AnyCharacterActive → ApplyLockedCrowdLODs
      → PumpNaturalResourceTrace
    → PumpRendererArrivalTrace
  → PumpPreheatControlTrace
```

资源模式跳过 `PumpMultiCharacter` 的实例扫描；AI 层在此模式直接走 `PumpNpcLodLock`，不执行 `PumpAiShadowCoverage` 的 Mesh 补偿。外层诊断 pump 仍存在，是否做具体工作取决于各自开关。

当前 LOD 改动包括：HG `EnableForceLOD0`、`QualitySettings.maximumLODLevel=0`、NPC 距离 1000m、最大数量 100、裁剪索引 0、关闭 camera physic cull。它们是全局行为，不能把“保留 LOD0”直接等同于“这些全保留”。

### 3.8 生命周期与退出

- `RetainAsset` 同时建立 GCHandle 并增加 `DontUnloadUnusedAsset`；此外 replacement、texture 等记录还有各自长期句柄。
- 当前角色 session 长期保存 BEM CPU 数据、原始与自定义资产以及 renderer 记录。
- `ShutdownWithAssetDeliveryTrace` 关闭相关开关，调用 `EarlyDeliveryShutdown` 和包装层的 Shutdown，最终进入 `Shutdown`。
- 底层 Shutdown 停止热键线程、释放 hooks 和句柄并清空记录；因可能不在 Unity 主线程，不执行场景恢复。`ReleaseRetainedAssets(false)` 不恢复对象的保护标记。
- 没有已验证的“游戏释放最后一个使用者 → Mod 的全部 CPU/GPU 数据同步释放”链路。

## 4. 正式版建议调用链（符号为拟定名称，尚未实现）

```text
InitializeResourceModule
  → ValidateHostApi
  → LoadModRegistry（角色、资源身份、包路径、适配规则；不读大块 payload）
  → ResolveRequiredContracts
  → InstallResourceDeliveryHook
  → InitializeLodPolicy（存在启用 Mod 或独立 LOD 开关开启时生效）

OnResourceDelivery(proxy, asset)
  → 校验重入条件（交付线程事实另行核实，暂不设计非主线程跳过分支）
  → MatchResourceToMod
  → 检查同一存活资源是否已完成；新代资源允许重新处理
  → 临时保护 asset 与本次构建对象
  → AcquirePayload（首次需求读取；缓存寿命另行确认）
  → CollectLod0Components（仅交付资源的子树）
  → ValidateModelPlan（组件、骨骼、声明、材质槽）
  → BuildModelAssets（独立 Mesh、私有材质、完整分组贴图）
  → CommitModelBindings（写入并读回校验）
  → 释放临时对象／payload 租用，记录最小处理结果
  → 原始 _FinishWithAsset（正常控制流中恰好一次）

ShutdownResourceModule
  → 停止接收新任务，安全解除 hook，清理模块自身数据
  → 不提供恢复原版外观操作
```

“资源加载路径”在这里仍是原资源交付前修改模板，沿用已验证窗口，不是重新实现游戏 BundleLoader。

建议把包身份、资源代际和临时构建结果显式传参，避免 `CharacterScope` 切全局变量及固定两个 session。首次正式版可先保留两个已验证角色的适配描述，再扩展注册表；不要同时重写转换器。

## 5. 生命周期设计待定项

| 对象 | 建议责任 | 尚需验证／确认 |
| --- | --- | --- |
| Mod 索引与适配规则 | 模块保存少量元数据，启动读取 | 同角色多个包冲突时建议拒绝，不能靠目录顺序选中 |
| BEM CPU payload | 按目标资源需求读取，有界复用 | 构建完立即释放会增加 UI／世界接连加载的重复 I/O；短期缓存时长与容量待定 |
| 构建中托管对象 | 使用临时 GCHandle，完成赋值或失败清理后解除 | 必须覆盖数组、Mesh、Material、Texture 等中间步骤 |
| 已交付 Mesh／Material／Texture | 建议移除模块的长期强引用，让模板和实例的正常引用维持使用期 | Unity 回收时机、游戏 bundle 强制卸载及原 Shader／辅助贴图寿命尚未验证 |
| 去重记录 | 不强持有 renderer／资产，只记录必要身份与处理状态 | 裸指针或 instance ID 不能永久判重；需要可验证的存活／代际机制 |
| 构建失败但未交付资产 | 模块在合适的 Unity 线程主动清理 | 不应把失败残留也交给“未来自然 GC” |

自然引用关系是：资源模板／实例 → Renderer → Mesh 和 Material → Texture。删除长期 GCHandle 不等于销毁 Unity 原生对象；动态创建资产也不自动成为原 Bundle 的所有物。C++ 容器内的 payload 更不会由游戏 GC 清空。

没有真实释放证据之前，只能称为“取消额外持有的方案”，不能声称“已跟随游戏自动释放”。不要通过每次切角色调用全局 `UnloadUnusedAssets` 来掩盖问题。

## 6. 代码迁移清单

| 当前逻辑 | 正式版处理建议 |
| --- | --- |
| Host ABI、BEM 解析、范围校验 | 保留，分离角色全局状态依赖 |
| 资源交付前 hook、局部 renderer 枚举 | 保留，脱离 trace 初始化包装 |
| 实时声明／stride、packed skin、bindposes、写后验证 | 保留，单独标注客户端布局依赖 |
| 每个新鲜资源独立构建 Mesh | 保留；允许游戏自然克隆共享 |
| 私有 Material、整模型贴图分组、组件 mask、显式 pins | 保留，返回结构化成功／失败结果 |
| hidden / NoGeometry | 保留包语义；删除 F4 人工切换 |
| F11/F12、热键线程、用户回滚与旧代资产保留 | 移除 |
| 人物初始化 hook、实例扫描、AI donor Mesh 补偿 | 不进入正式资源替换链 |
| clone / Instantiate / SMS trace、预热与到达诊断、BakeMesh 探针 | 不作为正式链依赖；先抽取其中真正用到的解析接口 |
| g_replaced / g_installed_slots / 原材质和原骨骼长期保留 | 改为本次构建的临时状态及最小去重信息，具体生命周期待确认 |
| LOD 每帧维护及全局 NPC 改动 | 已确认复用有效机制；独立控制，存在启用 Mod 时强制开启，无 Mod 时允许单独开启 |
| 构建失败清理、hook 退出同步 | 保留并明确；不属于用户热回滚功能 |

## 7. 用户确认节点

逐阶段确认。后一个阶段可以补充调查和设计，但未获批准不实现对应行为。当前仅修改文档；各节点的已确认部分以本节及决定记录为准，其余内容待确认。

| 节点 | 用户需要确认的内容 | 本文建议 |
| --- | --- | --- |
| K1：入口及运行范围 | 是否沿用交付前窗口，覆盖世界／详情资源 | 建议保留现窗口；配置／包变更重启生效。非主线程跳过策略按用户要求仅记录，暂不实现，历史背景和实际线程情况待核实 |
| K2：角色与包身份 | 首版角色范围、同角色多个包冲突、组件适配来源 | 已确认同角色同时仅启用一个 Mod，暂不支持热切换；角色描述、冲突报错和身份规则等具体实现待确认 |
| K3：Mesh 创建 | 实时布局、独立新建、packed skin、原生字段检查与不兼容拒绝条件 | 已确认沿用已验证构建算法；新 Mesh 构造后先从对应原版初始化 `+0x1C8`／`m_BonesPerVertex`，再上传自定义几何和 bindposes。其余不兼容拒绝条件待确认 |
| K4：材质与首次安装失败 | 必需贴图缺失时允许部分换装，还是整份资源保留原样 | **已确认：保持原样，不能替换一半。** 必须先完成并验证整份资源的 Mesh、材质、贴图和绑定，再一次性提交；任一必需环节失败则撤销本次未完成提交并继续交付原资源 |
| K5：懒加载与释放 | payload 缓存策略、是否移除长期 GCHandle／卸载保护、去重存活机制 | **方向已确认，效果待验证：**构建期保留临时保护；提交后取消模块长期强持有；自然释放和缓存参数通过实机观测后定案 |
| K6：LOD | LOD 与 NPC 参数、独立开关和强制启用条件 | 已确认整体复用现有机制；有效状态 = 有 Mod 启用 OR 独立 LOD 开关；无 Mod 时也可单独运行 |
| K7：新文件与构建接入 | 文件位置、何时切换 DLL 的编译入口、旧代码保存方式 | 先创建 `native/modules/custom_model/module_resource.cpp`，初期不接入现有目标；实现审核后以构建选择切换，两套实现不同时加载 |
| K8：发布门槛 | 自动测试和游戏验证覆盖、何时替换测试 DLL | 每段审核后继续；全部关键场景通过再接入／部署，push 按用户指令执行 |

K4 是新的失败语义建议，不属于用户已经确定的“不做热切换和回滚”。若连本次提交失败恢复也不接受，需要另定策略，不能默认为允许半套资源交付。

### K2 展开：角色识别与包映射（部分已确认）

已确认同一角色同时只能启用一个 Mod，暂不支持热切换。其余内容是供用户审核的具体方案，继续整理文档不代表全部方案已获批准。

#### 当前映射

| 角色 | 世界资源名 | 详情资源名 | 当前包文件 |
| --- | --- | --- | --- |
| 管理员（女） | `chr_0003_endminf_postmodel` | `chr_0003_endminf_uimodel` | `endmin-casualwear-c9.bempoc` |
| 佩丽卡 | `chr_0004_pelica_postmodel` | `chr_0004_pelica_uimodel` | `pelica-lod0-native-materials.bempoc` |

当前 `MultiCharacterForAsset` 按上述精确名字及其 `(Clone)` 形式匹配。它没有用交付日志里的资源 hash 来选择 Mod。两种资源名可以指向同一包，但每个新鲜资源使用自己的原始 Mesh 和材质进行适配。

当前组件匹配强度不同：管理员按角色 renderer 前缀和原始索引总数匹配；佩丽卡额外要求组件 ID 对应的精确 renderer／Mesh 名称和预期索引数。LOD0 筛选还排除 shadowProxy、lod1–3 路径及 `SK_actor_female` 路径。这些规则具有样本背景，不能直接宣称是通用角色规则。

#### 建议的数据与调用关系

```text
启动读取启用配置与适配描述
  → 建立 资源身份 → 角色适配描述 → 当前启用包路径 的索引
  → 游戏交付资源
    → 精确命中索引：按需读取对应 BEM
    → 仅枚举该资源子树内的目标 LOD0 renderer
    → 组件 ID + 名称规则 + 原始索引数校验
    → 后续执行实时布局与骨骼兼容性检查
```

建议分清两份信息：

- **角色适配描述**：角色 ID、世界／详情资源名、组件名称和身份规则、材质匹配策略。首版迁移两个已验证角色，适配描述的存储形式暂不限定为新的 JSON 或包格式。
- **Mod 启用记录**：选中哪个角色的哪个包、包路径。大块几何和贴图仍在 BEM 中，创建启动索引无需完整读取它们。

这使角色范围可以扩展，也能消除 `CharacterScope` 切换全局角色变量、`target=both` 和固定两个运行 session 的依赖。无需因此同时重写 BEM 格式或转换器。

#### 本节点待确认的具体选择

1. 首轮迁移与验收使用管理员（女）和佩丽卡；实现不把角色数量写死为两个。
2. 先沿用已验证的资源精确名称作为索引入口，不把未知的 hash 映射加入主链。世界和详情使用同一启用包，分别构建各自资源。
3. **已确认：同一角色同时只能启用一个 Mod，暂不支持热切换，不做多包叠加。** 安装多个候选包可以保留；若配置同时启用互相冲突的包，建议拒绝该角色的冲突配置并报告，不影响其他角色。冲突处理方式及具体配置生效时机仍是实现建议。
4. 未命中的资源正常交付；命中后的组件不兼容如何处理交由 K4 确认。不能仅凭原始索引数跨角色猜测对应关系。
5. `(Clone)` 名称只能帮助识别目标角色，不能证明该对象已经转换。重复交付／自然克隆的判重交由 K5 设计，不能只按资源名永久跳过。

### K3 展开：Mesh 创建迁移边界（待确认）

建议首版保留已经游戏验证的构建算法，主要整理函数边界和依赖：

- 输入明确为组件 payload、当前接收 renderer、它自己的原始 Mesh；不从另一人物或详情实例取 donor Mesh。
- 读取当前原始 Mesh 的声明、stride 和 bindposes；校验 payload 与骨骼容量后，再创建独立 Mesh。
- 保留一次建立声明、上传 packed 顶点／蒙皮流与索引、设置 bindposes、计算 bounds、写后验证和上传的顺序，不重算法线／切线。
- **HG 蒙皮字段初始化是新建 Mesh 的固定前置步骤：** Mesh 构造完成后，先从对应原版 Mesh 读取原生 `+0x1C8` 字段（对应 `m_BonesPerVertex`），尽早写入新 Mesh；该初始化必须发生在自定义几何数据和 bindposes 上传之前。不能等上传后再补，也不能使用另一个角色或另一资源的值。
- 保留当前原生 Mesh `+0x1C8` 字段的必要处理，将其集中在客户端兼容函数中，说明来源和适用边界。它不能因为看起来像补丁就被删除；上传后不一致是否升级为失败需要单独决定。
- 新鲜世界／详情资源各自创建；游戏自然克隆已经替换的模板时，允许共享模板的 Mesh，不要求每个克隆重新构建。
- 创建过程使用临时对象保护与失败清理；交付后的长期引用策略由 K5 决定。

本节点先确认“沿用已验证的 Mesh 构建算法，整理依赖与职责”的方向。暂不同时改变 BEM 格式、UInt16／单 submesh 能力或 `UploadMeshData(false)` 的 CPU 副本策略。整模型遇到构建失败后的交付语义由 K4 确认。

### K5 展开：懒加载、提交与自然生命周期（待确认）

K4 已确认整份资源必须原子提交，因此生命周期分成三个阶段：

1. **构建阶段**：按需读取目标 BEM；临时保护 asset、数组、Mesh、Material、Texture 以及构建中间对象，直到所有读回校验完成。
2. **提交阶段**：确认整个人物的 Mesh、材质、贴图和绑定都成功后，再写入目标 renderer。写入失败时恢复本次尚未完成的写入并销毁本次创建的对象，继续交付原资源。
3. **使用阶段**：提交完成后，模块不提供热切换或回滚。建议移除模块为“防 GC”而额外设置的长期 `GCHandle`／`DontUnloadUnusedAsset`，只保留游戏 Renderer、Mesh、Material、Texture 之间的正常引用；但这必须通过实际卸载和内存观测验证。

建议的懒加载边界是：启动只加载 Mod 索引、角色资源身份和包路径；首次匹配目标资源时读取该角色的 BEM。包数据在本次构建提交后是否立即释放，取决于重复资源交付的成本；可先采用有上限的短期 CPU 缓存，但不预加载未使用角色的包。

需要注意：移除模块句柄不会主动销毁 Unity 原生对象；游戏自然销毁 GameObject 也不保证动态创建资产立即释放。正式版应记录资源代际和弱身份状态，避免重复处理，但不能使用已经失效的裸指针作为永久所有权。

**K5 验证项：** 方向已确认，但自然释放不是理论假设的结论。需要实机验证角色切换、详情／世界对象销毁、对象池复用、再次加载，以及 CPU／纹理／Mesh／显存趋势。短期 payload 缓存的容量和时长仍待定。

### K6 展开：LOD 与全局 NPC 设置（已确认，补充独立开关）

当前代码中，资源路径的每帧 pump 会在任一角色 session active 时调用 `PumpNpcLodLock`，同时影响：

- HG 管线 `EnableForceLOD0`；
- `QualitySettings.maximumLODLevel=0`；
- NPC 可见距离、最大渲染数量、camera cull index 和 physic cull 开关。

这些设置作用域是全局的，不只作用于当前 Mod 资源。它们与资源交付 hook 的职责不同：资源替换需要交付窗口；LOD 策略决定游戏何时允许目标资源使用 LOD0。用户已明确：全局 LOD 锁定是为了防止 AI 控制角色进入未适配的 LOD 后出现异常，因此属于正式版的功能性前置条件。当前转换包只携带 LOD0，不能让 AI 角色在运行中切到其他 LOD。

LOD 的既有算法和参数继续复用，控制入口与角色替换状态解耦。资源链继续只筛选和替换目标资源实际交付的 LOD0；管线 ForceLOD0 与 `maximumLODLevel=0` 继续作为防止 AI 控制角色异常的全局防护。NPC 距离、最大数量和 camera／physic cull 参数沿用现有实现，后续只做必要的回归验证，不在正式版整理阶段重写算法。

**独立开关规则（用户已确认）：**

```text
effective_lod_enabled = has_enabled_mod || standalone_lod_enabled
```

| 有 Mod 启用 | 独立 LOD 开关 | 实际状态 |
| --- | --- | --- |
| 否 | 关 | 关闭 |
| 否 | 开 | 开启 |
| 是 | 关或开 | 强制开启 |

`has_enabled_mod` 根据本次运行采用的有效 Mod 启用配置决定；不能继续使用 `AnyCharacterActive()`（已有替换成功）作为开关依据，也不能因世界／详情模型卸载而关闭。无 Mod 时，LOD 所需接口和维护入口仍须能够独立初始化，不依赖 BEM 读取或资源替换成功。独立开关保存用户意愿，Mod 的强制开启只影响实际状态，不覆盖用户的独立开关设置；Mod 配置仍遵守不热切换的边界。

UI 有 Mod 启用时应显示强制开启原因；无 Mod 时允许用户独立控制。UI 的具体呈现留到 UI 阶段。进入／退出时机、全局参数恢复和性能影响均需要回归验证。当前旧代码的 `ResumeLockedCrowdLODs` 没有恢复 `QualitySettings.maximumLODLevel`，且 NPC 距离第二组分量没有独立保存；正式版实现独立关闭时需补齐原状态保存与恢复，不能仅沿用函数名就认定已经完整恢复。

**K6 已确认方向：** 整体保留 LOD 锁定及现有附带参数，新增上述独立控制规则；构建期迁移算法，同时纠正状态管理与恢复缺口。

### K7 展开：正式版新文件与接入边界

正式版不在现有 `module.cpp` 和 PoC `.inc` 上继续叠加补丁。旧实现标记为 legacy；新实现最终沿用原来的 `native/modules/custom_model/module.cpp` 名称，以保持 CMake 入口和发布模块名稳定。迁移期间先在独立临时文件中审阅新实现，完成后再替换入口。

```text
正式版入口
  → Host / 配置 / Mod 索引
  → 资源交付 hook
  → 资源身份匹配
  → 按需读取 BEM
  → LOD0 renderer 收集
  → 完整 Mesh / Material / Texture 构建
  → 原子提交或保持原样
  → 原始资源交付
```

建议从现有实现中迁移经过验证的底层能力（BEM 解析、Mesh 构建、材质复制、贴图匹配和 LOD 锁定），但不继续 include 整套 PoC 包装链，也不迁移 F11/F12、实例扫描、回滚资产世代和诊断探针。每个迁移函数在新文件中注明来源和职责，避免因为复制代码而重新引入全局角色状态。

接入顺序建议：

1. 旧 `module.cpp` 先改名或归档为明确的 legacy 文件，新实现暂存在独立临时文件，不影响现有可运行验证版本。
2. 先完成编译、静态检查和 fake Host 的几何／材质原子提交测试。
3. 关键确认项全部完成后，再在 CMake 中切换正式版目标；运行时只加载一套 CustomModel DLL。
4. 游戏验证通过后才部署到 artifacts；push 另行执行。

新文件是否采用单一 `.cpp` 还是后续拆成 `.h/.cpp`，取决于迁移后的实际依赖。第一版优先保证调用链可读、生命周期可审阅，不追求一次性重构所有底层 helper。

**K7 已确认方向：** 旧实现标记为 legacy，新实现最终使用原来的 `module.cpp` 名称。具体替换动作仍在调用链、测试和生命周期确认完成后执行；期间不让两套 DLL 同时加载。

### K8 展开：切换、测试与发布门槛（待确认）

正式版切换按以下顺序进行：

旧代码直接加上legacy后缀，新版本直接用正式名称。完成游戏实机测试后push然后进行UI层面设计，随后是包转换器设计。都完成后才算工作完成并考虑合并到主分支和主工作树。另外，新代码构建过程中要尽量保证代码的跨版本性，即不使用固定偏移值一类实现（实时计算的可以），尽量少的新建代码而复用确定逻辑，但是可以对代码进行优化。

正式版不以“编译通过”作为完成条件。尤其是 Unity 原生对象自然释放、真实 GPU 资源回收和 AI LOD 行为，必须有游戏内证据；fake Host 测试只能覆盖逻辑契约，不能替代这些验证。


## 8. 新文件创建与验收方式

1. 先确认 K1–K2：固定入口、角色／资源身份契约和覆盖边界，更新本文决定记录。
2. 确认 K3–K4：写清构建输入输出和失败语义，再确认 K7 并创建新文件。新入口不继续 include 整套 PoC 补丁；按依赖迁移已验证函数，保留来源映射。
3. 确认 K5–K6：实现寿命、去重和 LOD 策略。创建骨架不代表这两项自动获批，也不能用旧持久化实现暗中填补。
4. 通过 K8：再切换构建入口和进行游戏测试。旧实现保留为对照基线，避免一次删光后无法核对行为。

新文件建议先作为一个可审阅的 C++ 翻译单元，内部按入口、契约、解析、Mesh、材质、生命周期组织。是否再拆 `.h/.cpp` 由实际依赖决定；重点是不再把函数体跨 `.inc` 拆开。

验收关注真实行为：

- 单角色／双角色、世界／详情先后两种顺序、同名不同根对象。
- 游戏自然克隆不重复转换；重复交付不反复复制材质；卸载后同名资源可重新加载。
- 包缺失、损坏、骨骼／stride 不匹配、材质创建失败、赋值失败时的确定行为。
- 必需接口解析失败与 hook 部分安装失败的安全退出。
- 无热键、无人物初始化替换和实例扫描补偿。
- 未加载角色时不读取其大包；记录首次 I/O、解析、构建、上传耗时。
- 最后使用者包括世界、详情、模板缓存与对象池；确认卸载及再次加载后显示正确，并观察 CPU 内存、纹理／Mesh 数和显存趋势。
- LOD／阴影／距离变化下外观正确，同时检查全局 NPC 性能影响。
- LOD 开关覆盖三种状态：无 Mod 且独立开关关闭、无 Mod 且独立开关开启、有 Mod 启用时强制开启；未加载目标角色时也遵守此规则，关闭后恢复进入前的全局参数。

现有 fake Host 生命周期测试可复用其中与几何／材质正确性有关的断言，但需要围绕新入口重写测试边界。真实 GPU 创建与自动卸载必须游戏内验证，不能由 fake Host 测试代替。

## 9. 决定记录

| 日期 | 决定 | 状态 |
| --- | --- | --- |
| 2026-09-16 | 资源加载路径；无 Mod 热切换与用户回滚 | 用户已确认 |
| 2026-09-16 | 先记录调用链，再分阶段确认并创建新实现文件 | 用户已确认 |
| 2026-09-16 | 非主线程交付跳过策略仅作为历史问题记录，正式版暂不实现；未确认其必要性 | 用户已确认暂缓；本次只改文档，旧代码不变 |
| 2026-09-16 | 同一角色同时只能启用一个 Mod，暂不支持热切换 | 用户已确认；不代表冲突配置处理等细节已确认 |
| 2026-09-16 | 新 Mesh 的 HG 蒙皮字段初始化顺序 | 用户已确认：构造后尽早从对应原版 Mesh 初始化 `+0x1C8`／`m_BonesPerVertex`，先于自定义几何和 bindposes 上传 |
| 2026-09-16 | 资源替换失败语义 | 用户已确认：材质或贴图等必需环节失败时保持原样，不允许半套替换 |
| 2026-09-16 | K5 长期引用方向 | 用户确认作为验证项：构建期临时保护；提交后取消模块长期强持有；自然释放效果须实机验证，不能提前视为已解决 |
| 2026-09-16 | K1、K2 其余内容及 K3–K8 的具体实现选择 | 确认 |
| 2026-09-16 | 本轮设计解读及重建边界 | 用户再次确认；既有确认继续有效 |
| 2026-09-16 | LOD 独立开关 | 用户已确认：有 Mod 启用时强制打开；无 Mod 时可以单独开启。有效状态取两者逻辑 OR，不依赖替换成功或角色资源存活 |

后续每次确认在此记录具体选择和影响；不以“整体可行”代替对关键语义的确认。


## 2026-09-16 原生重建阶段收尾决定

用户已确认世界/详情替换、持续 LOD 和懒加载正常，暂未发现其他可复现问题，授权撤除临时测试/检测代码，确认当前原生重建阶段开发完成并推送 `origin/dev/custom-model-design`。正式逻辑采用已验证的 B5 持续 LOD 锁定，保留必要的数据和提交完整性校验。

B1～B4 未执行实机 A/B，B6 未取得故障发生时样本，均如实登记于 [A/B 记录](CUSTOM_MODEL_RESOURCE_AB_VALIDATION_20260916.md)；偶发配队部位缺失保留为 [已知问题 CM-001](CUSTOM_MODEL_KNOWN_ISSUES.md)，不宣称修复，不继续主动复现。

本次仅推送开发分支，不合并 main。原计划中的 UI 和包转换器作为后续阶段，当前完成声明不包含这两项。后续验证范围按用户要求只覆盖修改及直接影响路径，避免重复已通过场景。
