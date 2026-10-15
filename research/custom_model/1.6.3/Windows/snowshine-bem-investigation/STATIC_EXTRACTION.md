# 昼雪当前客户端静态提取

2026-10-15，从 `G:\Arknights Endfield` 只读提取昼雪 `chr_0014_aurora` 的 world/UI prefab 依赖闭包。未启动游戏、未注入、未写游戏目录，未计算产物哈希。

先按 `StreamingAssets` → `Persistent` 覆盖顺序读取当前有效 `manifest.hgmmap`，与今天 `runtime-catalog-update/minghe-shared-inputs` 中用于生成 JSON 的原始 manifest **逐字节比较相等**，因此复用该 JSON 定位资源，没有再次生成约 230 MB 的 manifest JSON。此验证并不依赖版本 GUID 相等。

本次仅提取 87 个 bundle，共 92,728,414 字节（88.43 MiB），全部有效条目来自 Persistent 层。未解码或导出纹理像素、Shader 字节码，也未开启 `--resource-identities`。原始游戏 bundle 位于已忽略的 `current/inputs/Bundles/`，不得提交。

| 项目 | 结果 |
| --- | --- |
| world prefab | `assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0014_aurora_postmodel.prefab` |
| UI prefab | `assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0014_aurora_uimodel.prefab` |
| 当前 world/UI bundleIndex | 148016 / 140393 |
| Reader 文件 / 对象 | 87 / 1622 |
| Reader 后端警告或错误 | 0 |
| Mesh | 53 |
| Renderer | world 53 + UI 12 = 65 |
| 引用完整 Renderer | 65 / 65 |
| 缺失 prefab | 0 |
| 运行时布局观测 | 0 |

证据：

- `extract_current.py`：本次有界提取流程，拒绝非空输出目录与游戏内输出。
- `current/inputs/extraction.json`：有效 overlay 来源、资源身份、bundle 列表与字节数。
- `current/raw.json`：原始对象图，包含精确对象引用、Mesh 标识、顶点数、子网格索引数/拓扑、bindpose、骨骼引用和序列化通道。
- `current/database.json`：当前引用解析后的 renderer/mesh/bone 元数据。
- `prior-database.json`：用同一个 parser 从旧 `1.5.3/shared/postmodel-native-parser-history/sweep-identities/chr_0014_aurora.json` 解析的对照资料。

旧图谱使用同一 parser 也得到 53 个 Mesh、65 个 Renderer、65 个引用完整 Renderer。详细字段比较由同目录静态对比报告负责；**数量相同不等于模型、索引数据或运行时布局完全相同**。此元数据模式不导出完整顶点/索引 payload，不能证明这些原始数据逐字节未变，也不能代替运行时匹配拒绝日志。
