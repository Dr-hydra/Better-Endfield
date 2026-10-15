# BEM Tools 1.5.3 新角色资源正式分发

2026-10-15。复用本专题已采集的 5 个资源根、352 条 renderer 记录；没有重新启动采集或建立全量资源库。仅对已提取的祀依赖闭包及明河共享依赖闭包各运行一次现有 `NativeAssetReader --resource-identities`，从原游戏索引 buffer、纹理 mip 和运行时格式/尺寸证据补全 EFMI 业务身份。没有计算发布产物哈希。

## 已进入正式工具目录

- `tools/CustomModel/catalog/chr_0036_jsspsi.json`：祀 15 个双根部件，14 个可确认索引身份、58 个纹理身份。正式 legacy catalog 从 33 增至 34 个角色；`coverage.json` 与 `source-identities.json` 增量加入对应行，旧角色资料未重建。
- `tools/CustomModel/profiles/endfield-1.6.3/`：5 套 BEM 1.4 spec/profile/可编辑工程及索引和说明。祀完整世界/详情 46 个 LOD0 renderer、世界 cloth_04 独立 1 个、明河世界三形态各 20 个。三路明河 owner 均为实际角色 `chr_0037_minghe`。
- 同目录 `evidence/`：原始 352 条运行时记录、采集摘要、每套 profile 的原生声明/步长和可推导 source 身份。当前目录共 24 个文件、5,651,099 字节，只含结构与元数据；无游戏顶点坐标数组、原始索引字节或纹理像素 payload。

全部平台声明为 Windows。证据引用在工具目录内相对解析，不需要原本 `research/` 工作区。

## 仍保留的限制

- 祀仅 8 个部件的来源布局符合现有自动转换规则，7 个 unsupported 标志不变；brow_01 多 submesh 未造单区域 IB 身份。正式状态是 `offline-resource-identities-partial`。
- 祀 cloth_04 世界/详情材质不同，仍不进入 legacy 双根共享合同；完整 BEM 1.4 工程保留两根各自合同。
- 明河没有 UI 根，未生成虚构 legacy catalog。每路已获得 19 个单区域索引身份；fur_01 多 submesh 的源匹配仍缺。明河/02/03 分别取得 56/51/56 个纹理身份，02/03 公共默认 MRO 后端读不到的身份保持缺项。
- 显式工程全部为 `keep`，无替换 payload，三个 `runtime_verified` / `conversion_ready` / `render_verified` 标志都保持 false。可正常由发布 CLI/GUI 的编辑工程路径读取打包，不能称为已完成的替换模型或任意来源自动转换。
- catalog 既有 `verified=true` 表示原生合同已经实际观察；本次依据已有双根直接采集保留该值，没有将未观察或不支持的内容改成 verified。

## 验证

`python -m unittest test_catalog_163 -v`：3 项通过。

1. 真实祀 14 个来源身份在整个正式目录唯一匹配；改变源 index count 被拒绝。
2. 8/7 布局支持边界、58 纹理身份、缺项和随包证据链接保持。
3. 5 套目标保留真实 owner 与 Windows 平台、全部 keep-only/未渲染验证状态；实际打包再读取 BEM 1.4，目标合同完整一致。

README 相对链接检查通过；目录内未发现本机绝对路径或研究目录依赖。最终独立 ZIP 解压后的便携 CLI 验证由发布流程统一执行，本文不预先声明该最终包已发布或游戏内替换验收。

可重建入口：`promote_creator_resources.py`。原始采集和 staging 继续保留作为历史证据，较早 `RUNTIME_IMPORT.md` 中“正式目录未更新”的描述对应当时状态，以本次正式分发增量记录为后续状态。
