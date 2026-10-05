# 噗切娜角色资料（2026-10-01）

噗切娜 `chr_0038_purrche` 已加入 BEM Tools 1.4.0 的 `catalog/`，角色资料从 32 增至 33 个。标准资料随独立工具 ZIP 和 Windows 安装包内的转换工具分发；转换后的 BEM 包携带 Windows、Android 运行所需的模型目标、骨骼和材质资料，Android 不需要独立安装转换 catalog。

## 来源与验证

- 从当前 PC 游戏的 StreamingAssets → Persistent 覆盖层定点提取 manifest 和角色依赖，未修改游戏文件。
- manifest 版本：`2954fa80-23c1-1579-2b22-4ecfd6d70418`；当前 Persistent manifest chunk：`F457562363ECD54B18B2C92C53523992`。版本字段与历史相同，chunk 已变化，因此保留本轮独立来源记录，不把旧数据冒称本轮采集。
- 运行时任务：`purrche-pc-20261001-225354`，游戏进程 `6340`。详情根 `chr_0038_purrche_uimodel` 22 个 renderer，场景根 `chr_0038_purrche_postmodel` 77 个 renderer，99 项均完整。
- 运行时骨骼顺序、bindpose、索引/顶点数、材质顺序及顶点布局均已核对；离线解析覆盖全部 99 个 renderer，无未解析资源或引用。后端仅报告 CNKeys 配置文件首次创建，不存在资源解析缺项。
- 22 个 LOD0 主体部件的 world/UI Mesh、骨骼路径及材质引用等价；两路各有 22 份直接运行时证据。入库包括 9 个无歧义的完整单子网格索引入口和 29 项纹理身份。
- 雨雪全局纹理保留同 manifest 版本已有的源内容校验锚点；转换时仍逐次核对来源内容，并沿用游戏本身的全局资源。

## 自动转换边界

采样完整不表示每个部件均可自动转换。三个 `fur_*_lod0_20` 毛发部件使用多个 submesh，尚无通用索引入口；`furcard_01_lod0` 的原生布局不在当前自动几何转换范围内。十个 `eye_01` 至 `eye_10` 部件共享索引身份 `6bf2ba87`，单凭该身份无法选定部件，因此不发布有歧义的自动入口。具体限制保存在角色 `identity_issues` 和覆盖报告中。

本轮只采 PC。BEM 运行资料由双端共用；本轮没有验证 Android 专有的原始顶点布局，也没有进行新角色替换效果的实机验收。

## 分发范围

`catalog/chr_0038_purrche.json` 仅包含名称、映射、布局和来源证据，不包含游戏几何或纹理像素。原始 JSONL、离线 Bundle 与读取器输出留在本机研究产物，不进入发布包。采集已停止，临时探针 DLL 部署已恢复。
