# Better Endfield 角色模型替换可行性结论

本文记录角色模型替换功能的选型结论,以及与 EFMI（Endfield Model Importer）生态兼容的评估。结论基于 2026-09-08 的实测,证据来源在各节标注。重新评估时应重跑对应的解析与比对,而不是沿用本文结论。

## 范围

- 在游戏内替换角色模型（几何 + 纹理），PC 与 Android 使用同一套运行时实现。
- 消费社区已有的 EFMI mod 资产，不复刻 EFMI 运行时。

非目标：动画/技能/数值改动、shader 替换与后处理改造（属渲染管线范畴，本文不覆盖）、把 EFMI 或 3DMigoto 作为运行时依赖。

## 硬约束

功能需同时落地 PC 与 Android。这条约束单独决定了大部分选型：

- EFMI 是 3DMigoto 分支，仅支持 D3D11；Android 侧为 Vulkan/GLES，该路径在移动端不存在。
- 因此任何依赖图形 API 拦截的方案都不能作为架构，只能作为 PC 端的临时兜底。
- 可跨平台的替换必须发生在引擎之内（资产层或对象层），由 Unity 负责后端翻译。

## EFMI 实测结论

证据来源：`SpectrumQT/EFMI-Package`、`SpectrumQT/EFMI-Tools`、`SpectrumQT/XXMI-Launcher`，2026-09-08 抓取。

### 运行形态

EFMI 是 3DMigoto 分支，不是资产格式。`EFMI-Package` 仓库根下即 `EFMI/d3dx.ini`（51 KB），另有 `Core/EFMI/*.hlsl`、Hunting、FrameAnalysis。启动方式见 `XXMI-Launcher/src/xxmi_launcher/core/packages/model_importers/efmi_package.py`：

```python
game_exe_names = ['Endfield.exe']
def get_start_cmd(...): return game_exe_path, ['-force-d3d11'], work_dir
custom_launch_inject_mode = 'Inject'
```

即：强制客户端切到 D3D11，然后注入 3DMigoto。

### 拦截层级

EFMI 在 `DrawIndexed` 时刻改写绑定的 IB/VB/SRV。此时资产已加载、prefab 已实例化、蒙皮矩阵已上传。它站在引擎外部，可得信息只有 buffer 哈希与索引数——mod 身份因此是 `$object_guid = 原模型 index_count`，这不是设计选择，是该层唯一可得的标识。

### 能力边界

能做：换网格（顶点数可变）、换纹理、热键控制部件显隐（Ini Toggles）、shape key（游戏本身不向 GPU 暴露 shape key，EFMI 用 compute shader 自行实现）、per-instance 区分（Spatial Identification）、LOD 分支、shader 替换与后处理。

不能做：动画、prefab、组件参数、骨骼层级、音频、UI 逻辑。

### 资源文件格式

发布包（用户下载到的）：

```
mod.ini                       3DMigoto 命令表，含逻辑不只是数据
Meshes/
  Component{N}_IB.buf
  Component{N}_VB0.buf        POSITION + ENCODEDDATA0（TBN）
  Component{N}_VB1.buf        TEXCOORD / COLOR
  Component{N}_VB2.buf        BLENDINDICES / BLENDWEIGHTS
  Component{N}_VB*_LOD{k}.buf
  Component{N}_VB2_LOD{k}_BlendRemap.buf
  Components.buf              后缀是 .buf，内容是 JSON：各 buffer 的 semantic/format/stride
Textures/
  Components-0-3 t=<hash> BC7-sRGB.dds
```

制作者侧的 Object Sources（通常不随 mod 发布）：`Metadata.json`（`ExtractedObject`：ib_hash / vb0_hash / vertex_count / index_count / weigthing_type / rotation / components[] / shapekeys / export_format）、`Component N.fmt`、`TextureUsage.json`。

`ExtractedObjectComponent` 字段：`mesh_name, cpu_posed, ib_hash, vb0_hash, vertex_offset, vertex_count, index_offset, index_count, vg_offset, vg_count, vg_map, lods[]`。

**格式是自描述的。** 顶点布局在 `Components.buf` 里明码给出；法线/切线编码算法在 `efmi-tools/data_models/data_model_efmi.py::decode_tbn_data_10_10_10_2` 中公开——`R10G10B10A2_UINT`，X/Y 为八面体编码法线，Z 为角度编码切线，bit30 为 packed 标志，bit31 为 bitangent sign；坐标系差异同样明写（`flip_bitangent_sign`、`flip_texcoord_v`，外加 `Metadata.json` 的 `rotation`）。解析不需要逆向。

**仅从发布包也能恢复结构信息。** `mod.ini` 模板（`efmi-tools/templates/mod.ini.j2`）会把这些写死进去：`$object_guid`、`$mesh_vertex_count`、`$component_count`、`$bones_count`；每个组件一段 `[TextureOverride_EntryPoint_ComponentN]`，带 `hash = <ib_hash>` 与 `match_index_count`；每个对象一行 `drawindexed = <index_count>, <index_offset>, 0`；`$\EFMIv1\gpu_posed`；LOD 层级结构。不需要向作者索取 Object Sources。

### 格式缺什么

| 缺失 | 原因 | 影响 |
| --- | --- | --- |
| 骨骼名、bindpose | frame dump 采集于 GPU 层，这些信息在下降过程中已丢失 | 需从运行时的原模型现取 |
| `vg_offset` / `vg_count`（发布包内） | 只在 Object Sources 的 `Metadata.json` 里 | 见"未决问题 1" |
| 语义化身份 | 该层只能看到 index_count | 需自建 index_count → 角色的反查 |
| COLOR0 语义 | 官方 modder guide：`Algoritm of its generation isn't figured out (yet)`，描边用 tangent-based 自动生成数据 | 原样搬运即可 |

`mod.ini` 中的 Ini Toggles、shape key 动态控制、per-instance 逻辑、custom shader 属于运行时逻辑而非数据，资产导入无法还原。`cpu_posed` 为真的组件 EFMI 自身也不支持改顶点缓冲。

## 客户端实测

以下结论由 `research/il2cpp-dumps/20260903-pc-current`、本地客户端目录与仓库既有工具解析得到。

### 引擎与渲染

- Unity **2021.3.34f5**（`Endfield_Data/globalgamemanagers`）。
- 自定义 SRP，托管实现：`HG.RenderPipelines.Runtime.dll`（dump 3.1 MB）、`HG.RenderPipelines.ScriptBridge.dll`、`Unity.RenderPipelines.Core.Runtime.dll`。含 RenderGraph V2 与命名 pass（`CharacterShadowPassData`、`DeferredLightingPassData`、`UberPostPassData`、`DecalPassData` 等）。整个渲染循环在托管代码内，IL2CPP 可达。

### 资源栈

- VFS：ChaCha20 加密的 `.blc`（索引）+ `.chk`（块），主密钥已在 `tools/EndfieldUnpacker/decrypt_vfs.py`，块尾带 CRC32 校验。
- AssetBundle 层被混淆：`tools/EndfieldStudio/AnimeStudio/BundleFile.cs:706` 存在 `case CompressionType.Lz4Inv when Game.Type.IsArknightsEndfieldCB2()`，配 `LZ4/LZ4Inv.cs`（literal token 位布局打乱、chunk 长度大端），另有 VFSAES 与头部 `0xa6` 启发式。说明 **UnityPlayer 的 bundle 读取器被客户端改过**。
- 现有工具链**只能读不能写**（`AnimeStudio.Patcher` 是 dnSpy 的 apphost patcher，与资源无关）。

### 关键拦截点

| 用途 | 契约 | dump 位置 |
| --- | --- | --- |
| 整包重定向 | `Beyond.VFS.VirtualFileSystem.LoadBundleFromFile(StringPathHash)` → `UnityEngine.AssetBundle`；另有 `LoadBundleFromFileAsync` | `Common.Beyond.dll.cs:70639` |
| 按资产加载 | `Beyond.Resource.ResourceManager.Load(StringPathHash, Type, RootCategory, EResourceRequestPriority)` → `FAssetProxyHandle` | `Common.Beyond.dll.cs:66017` |
| 角色网格创建 | `Beyond.NPC.Avatar.NPCAvatarCreatorUtils.CreateSMSGO(...)` 与 `CreateSMSInfoForPostModel(...)` | `Gameplay.Beyond.dll.cs:343605` |

`CreateSMSGO` 值得单列。签名中 `ELODLevel level` 是入参、`NPCGoPool emptyGoPool` 是池、`out SkinnedMeshRenderer[] smrs` 直接输出结果。这意味着角色生成、LOD 切换、池化复用**都经过同一个函数**，一个 post-hook 即可覆盖，无需自行遍历对象树。

> 待确认：命名空间为 `Beyond.NPC.Avatar`，可控角色是否同样走该路径尚未验证。`CreateSMSInfoForPostModel` 与 `POST_MODEL_MESH_DICTIONARY` 同 `models.csv` 中 33 个 `chr_XXXX_postmodel.prefab` 对应。

### Unity API 可用性

IL2CPP 会裁剪未使用的方法，故逐项确认（`UnityEngine.CoreModule.dll.cs:8917` Mesh、`:8454` SkinnedMeshRenderer）：

- `Mesh..ctor()`、`SetVertices` / `SetNormals` / `SetTangents` / `SetColors` / `SetUVs` / `SetTriangles` / `SetIndices`、`UploadMeshData`、`RecalculateBounds` — 在
- `Mesh.bindposes` / `boneWeights` / `subMeshCount` / `indexFormat` 的 setter — 在
- `SkinnedMeshRenderer.sharedMesh` / `bones` / `rootBone` 的 setter — 在
- `Mesh.GetVertexBuffer(int)`、`SkinnedMeshRenderer.GetVertexBuffer()` / `GetPreviousVertexBuffer()`、`vertexBufferTarget` setter、`GraphicsBuffer.SetData` — 在

即：EFMI 用 3DMigoto 实现的"直接写 GPU 顶点缓冲"，Unity 2021 自带官方 API 且跨后端。移动端对 `GraphicsBuffer.Target` 的支持限制未验证。

- `AssetBundle.LoadFromFile` / `LoadFromFileAsync` / `LoadFromMemoryAsync` — 在（`UnityEngine.AssetBundleModule.dll.cs`）

### 跨平台一致性

比对 `research/character-catalog-current/models.csv`（PC，20260903）与 `android/research/device-1.5.3/character-catalog/models.csv`：

```
PC      3475 条 model_asset / 1902 个唯一路径
Android 3475 条 model_asset / 1902 个唯一路径
路径交集 1935 / 1935，assetSize 差异 0，bundleName 差异 0
```

模型资产两端完全一致。推论：对 PC 模型制作的 mod 几何在移动端同样适用；`index_count` 身份识别跨平台有效；bundle hash 两端通用；**一份转换产物两个平台共用**。

纹理格式不在该 catalog 覆盖范围内，未验证，但两端几乎必然不同（见"未决问题 2"）。

### 现有跨平台构建

`android/app/src/main/cpp/CMakeLists.txt:30` 直接编译 `native/modules/model/module.cpp`——PC 与 Android 共用同一份模块源码，Android 侧仅由 `login_model_module.cpp`（306 行）提供 `BE_HostApiV1` 宿主适配。该模块能跨平台，正是因为它不接触图形 API。

现有 `BetterEndfield.Model.dll` 的模型能力仅覆盖登录场景演员，且属于"引用替换"——加载游戏内已存在的另一份 prefab，不涉及自定义资产。

## 替换层级与选型

```
层 1  引用替换          资产引用解析时      已实现（登录场景）
层 3  Bundle 重定向     bundle 加载时
层 2  运行时对象覆写    对象实例化之后
────────────────────────────────────────
EFMI                     DrawIndexed        引擎之外
```

层 1/2/3 均在引擎内，天然 API 无关，均可上移动端。EFMI 所在的层不可移植。

### 层 2 与层 3 的取舍

| | 层 2 运行时覆写 | 层 3 Bundle 重定向 |
| --- | --- | --- |
| 运行时 hook | 1 个收口点（`CreateSMSGO`）+ 若干补丁点 | 1 个（`LoadBundleFromFile`） |
| LOD / 池化 / 多实例 | 需处理（收口点已覆盖大部分） | 游戏自身处理 |
| 换装 / 部件显隐 / 材质重赋 | 需处理 | 自动 |
| 停用还原 | 需持有原引用并还原 | 取消 hook 即可 |
| 游戏更新维护面 | 中等、分散 | 极小、单点 |
| 离线制作成本 | 无，运行时构建 Mesh | 需产出 bundle，工具链尚不具备写回 |
| 主要未知 | 骨骼映射（未决问题 1） | 格式兼容（未决问题 3） |

层 3 的结构性优势是替换发生在源头，所有下游消费者自动受益，复杂度移入可调试的离线工具；代价是引入一个当前无法回答的格式兼容问题。

### 结论

1. **架构主线为层 2 或层 3，二者取一，由未决问题 3 的实验结论决定。** 两者都 API 无关、都可跨平台、产物两端通用。
2. **EFMI 仅作为离线资产来源，不作为运行时依赖。** 转换在桌面执行一次，PC 与 Android 消费同一份中间产物。移动端不存在其他生态接入方式，故导入器属必需组件而非可选项。
3. **PC 端集成 EFMI（注入 3DMigoto + `-force-d3d11`）可作为短期生态兜底，但明确为 PC-only，不参与架构决策。** 该路径与 `docs/DISPLAY_PIPELINE.md` 的 OptiScaler 方案互斥（`-force-d3d11` 会使 FSR4 路径与 DLSS-FG 失效），若实施须做成互斥模式。`native/loaders/injector/main.cpp:569` 已具备 `CREATE_SUSPENDED` 启动与任意参数透传，增量很小。
4. **不下到 SRP、GfxDevice 或图形 API 层。** HG 的托管 SRP 是可行的 API 无关拦截层，但仅在需要"改输入表达不了"的渲染流程改动时才动；模型替换属于改输入，无需拦截输出。

### 建议的实现形状

```
EFMI mod（GameBanana，对 PC 模型制作）
      │  离线转换器（桌面，一次性）
      ▼
中间格式（显式 Unity 顶点布局 + 权重 + 纹理清单 + 目标 object_guid）
      │  同一份 native/modules/model/module.cpp
      ├──► PC 运行时
      └──► Android 运行时
```

离线阶段承担：buffer 解析、TBN 解码、三角化、BC7→ASTC 转码、面数预算检查、`mod.ini` 分诊（检测 `[KeySwap*]`、ShapeKeys、自定义 `[ShaderOverride]`、`gpu_posed = 0`、多 LOD，据此判定可导入或必须走 EFMI）。运行时只做定位与装配。

## 未决问题

按依赖顺序排列。前两项不需要进入游戏。

**1. VG 索引与 `SkinnedMeshRenderer.bones[]` 的对应关系**（决定几何 mod 能否成立）

假设：一个 EFMI component 对应一个 SkinnedMeshRenderer 或 submesh，则 component 内的 BLENDINDICES 直接是该 renderer `bones[]` 的下标，`vg_offset` 只是帧缓冲排布偏移，与还原无关。

验证方式：运行时 dump 目标角色各 renderer 的 `bones.Length`、`sharedMesh.vertexCount`、`GetIndexCount(submesh)`，与 `mod.ini` 的 `match_index_count`、`$bones_count` 比对。因两端资产一致，PC 上验证一次即可。

假设不成立时的兜底：按骨骼名匹配，需要额外的名称来源。

**2. 移动端纹理格式**

EFMI 输出 BC7 DDS（桌面独占）。Android 侧预期为 ASTC。需确认 block size，决定转换器输出目标。转码会有质量损失，PC 与移动需分别产出纹理包。

**3. 客户端是否接受 Unity 原生打包的 bundle**（决定层 2 / 层 3 的选型）

客户端 UnityPlayer 已被改造以读取 Lz4Inv/VFSAES 格式。若 HG 是**新增**压缩类型分支，标准 LZ4 与不压缩 bundle 应照常加载；若是**替换**标准实现，则需使用不压缩 bundle。

验证方式：用 Unity 2021.3.34f5 打一个不压缩的最小 bundle，经 IL2CPP 调用 `AssetBundle.LoadFromFile` 观察返回。结论为二值，直接决定架构。

**4. `CreateSMSGO` 是否覆盖可控角色**（仅层 2 需要）

**5. 移动端 `GraphicsBuffer.Target` 支持范围**（仅在实现 compute 驱动形变时需要）

## 风险

- **反作弊**：客户端目录含 ACE 内核驱动与进程内组件。当前的托管 hook 可见性低于注入 3DMigoto 与 `-force-d3d11`。若实施 PC 端 EFMI 集成，须向用户明示。
- **EFMI 处于 alpha**：mod 内写死 `$required_efmi_version`，Core 带 `ErrorOldVersionMod.md` 用于拒绝旧 mod。格式为移动靶，导入器需容忍版本漂移。
- **许可**：EFMI 与 3DMigoto 为 GPLv3。参考其格式实现无碍，直接复用代码会传染。若做 PC 端集成，应比照 `OptiScalerDeploymentService` 与 `manifests/shared/optiscaler.lock.json` 的模式从上游 release 获取（EFMI-Package 自带 ECDSA P-384 签名校验），不随安装包分发二进制。
- **性能预算**：移动端对高面数自定义模型的承受力显著低于 PC，转换器需设预算门限。
