# 1.6.3 新角色离线资料

2026-10-15 已完成四个条目的定点资源提取与元数据读取。没有操作游戏、写 AppData 或改正式 catalog，没有计算产物哈希。使用现有 `extract_native_bundles.py`、`NativeAssetReader` 和 `parse_native_models.py`，解包及读取串行且进程设为 BelowNormal。

| 条目 | Bundle | Mesh | Renderer / 引用完整 | LOD0 部件（含 VFX） | LOD0 引用骨骼路径 |
| --- | ---: | ---: | ---: | ---: | ---: |
| 祀（world + UI） | 165 | 86 | 109 / 109 | world 23 + UI 23 | 各 375 |
| 明河 | 106 | 83 | 81 / 81 | 20 | 358 |
| 明河 02 | 107 | 83 | 81 / 81 | 20 | 358 |
| 明河 03 | 105 | 83 | 81 / 81 | 20 | 358 |

每个角色目录中 `inputs/extraction.json` 记录资源来源与依赖，`raw.json` 保存原始对象图，`database.json` 保存引用解析后的骨骼、bindpose、Mesh、材质和离线顶点布局。明河三个目录的 bundle 通过硬链接复用 `minghe-shared-inputs` 中的闭包文件。详细部件与骨骼数量见 `offline-inputs-summary.json`。

## 运行时采集的最小范围

先加载祀、明河、明河 02、明河 03 的四个 postmodel。Shader 编译完成后再操作。离线阶段未生成任何运行时观测，全部 `runtime_layout_observed=0`。

祀有 16 个非 VFX LOD0 主部件，其中 15 个 world/UI 可按现有入库器配对；`S_actor_jsspsi_cloth_04_lod0` 两根材质槽不同，不能把开屏 world 观测当成此 UI 部件的等价证据。祀单部件最大 159 个骨骼。

明河每个变体有 18 个非 VFX LOD0 主部件，单部件最大 175 个骨骼。三路 LOD0 的 20 个 Mesh 身份相同，但 02 与原版有 15 个部件材质不同、03 有 14 个，所以仍应各加载一次，不应只采原版就声明另外两路已验证。现有 importer 要求 world/UI 配对，需支持仅 world 目标后才能正式入库，不应生成不存在的 uimodel。

## 保留的后端限制

明河 02/03 的共同依赖 `initial/9f92bbd1e0f2191cffd3be18.ab`（2461 字节）有一次读取警告。Manifest 对应资源是 `packages/com.hg.render-pipelines/runtime/renderpipelineresources/texture/default/t_default_mro_mro.tga`。现有后端报剩余长度 -16；原始警告保留在各 `reader.log` 和数据库的 `source.backend_errors`。

这不影响目前 81/81 目标 renderer 的 Mesh、骨骼、材质引用完整性，`missing_resources=[]`；但不宣称整个依赖闭包已无警告解析。当前任务不展开 Shader 代码、纹理像素或全量资源。
