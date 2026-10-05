# BEM 历史文档

这里保存 BEM 格式与创作工具在开发过程中的规范版本、设计稿和研究记录，内容可能已过时，仅供追溯。

当前文档：

- [创作者指南](../BEM_CREATOR_GUIDE.md)
- [格式规范（1.0–1.3）](../BEM_FORMAT_SPEC.md)
- [运行时行为与兼容性](../BEM_RUNTIME_COMPATIBILITY.md)
- [其他来源 Mod 转换](../BEM_SOURCE_MOD_CONVERSION.md)

## 按 BEM 格式版本追溯

这些是原有归档，格式版本不是游戏版本；现行合并规范见上方入口。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM 创作者指南（第一版草案）](bem-1.0/BEM_CREATOR_GUIDE_DRAFT.md) | 第一版流程草案，尚未实现的旧声明与全角色采样计划 | 既有归档 | not_applicable |
| [BEM 第一版数据结构讨论稿](bem-1.0/BEM_V1_DESIGN_DRAFT.md) | 第一版容器设计讨论，旧BEMPC读取器/未冻结状态 | 既有归档 | not_applicable |
| [BEM 1.0 格式规范](bem-1.0/BEM_V1_SPEC.md) | 1.0初版正式协议及旧BEMPC不接受边界 | 既有归档 | not_applicable |
| [BEM v1.1 组合选择与热切换兼容设计（历史草案）](bem-1.1/BEM_V1_1_DESIGN.md) | 组合外观/热切换历史草案，draw payload方案已修正 | 既有归档 | not_applicable |
| [BEM 1.1 组合外观格式](bem-1.1/BEM_V1_1_SPEC.md) | 1.1组合外观与按draw读取的分版本规范 | 既有归档 | not_applicable |
| [BEM 1.2 规范（在 1.1 基础上的增量）](bem-1.2/BEM_V1_2_SPEC.md) | 1.2上限、texture_slots及资源骨别名增量 | 既有归档 | not_applicable |
| [BEM 1.3 体型滑条可行性与格式提案](bem-1.3/BEM_1_3_BODY_SLIDER_DESIGN.md) | BEM1.3实现前体型滑条提案，正文当时明确尚未实现1.3 | 既有归档 | not_applicable |
| [BEM 1.3 position sliders — creator workflow](bem-1.3/BEM_V1_3_CREATOR_GUIDE.md) | 1.3位置形变、delta/ShapeKey及示例创作流程 | 既有归档 | not_applicable |
| [BEM 1.3 — authored position parameters](bem-1.3/BEM_V1_3_SPEC.md) | 1.3作者位置参数、delta格式和运行时/工具规则 | 既有归档 | not_applicable |
| [EFMI 体型滑条：真实样本与上游执行路径](tooling/body-sliders/EFMI_BODY_SLIDER_RESEARCH.md) | 真实体型样本和上游ShapeKey执行链证据 | 既有归档 | not_applicable |
| [创作者工具工程化评估（2026-10-01）](tooling/creator-workflow/BEM_CREATOR_WORKFLOW_REVIEW.md) | 工程化评估、1.2别名导出修正与最终最小实现 | 既有归档 | not_applicable |
| [7z 内未压缩 DDS 读取修复](tooling/dds/BEM_DDS_READING.md) | 7z未压缩RGBA/L8 DDS像素声明修复样本 | 既有归档 | not_applicable |
| [逐绘制段贴图绑定：样包与官方实现交叉核对](tooling/efmi-compatibility/BEM_PER_DRAW_COMPATIBILITY.md) | 逐draw贴图机制样本与官方接口交叉研究 | 既有归档 | not_applicable |
| [RabbitFX 兼容性与自动化边界](tooling/efmi-compatibility/BEM_RABBITFX_COMPATIBILITY.md) | RabbitFX不同层级自动化边界及样本验证要求 | 既有归档 | not_applicable |
| [BEM 工具链与多包 ZIP（2026-09-19）](tooling/toolchain/BEM_TOOLCHAIN.md) | 1.0.0工具inspect/convert/unpack/pack及ZIP阶段交付 | 既有归档 | not_applicable |

## 原归档中的研究与实施记录

原 archive/bem 的版本研究材料按模块、配置中的游戏版本和专题保存，仍维持既有归档状态。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android BEM file opening / sharing — 2026-09-27](../../android/research/1.5.3/bem-open-with/ANDROID_BEM_OPEN_WITH.md) | Android BEM打开/分享、单一patch基线和生命周期 | 既有归档 | 1.5.3 |
| [角色资料入库与 EFMI 原资源对应](../research/1.5.3/bem-character-catalog/BEM_CHARACTER_CATALOG.md) | 32→33角色入库与EFMI对应的阶段变化、排除项 | 既有归档 | 1.5.3 |
| [ComponentN 自动转换与角色资料接入](../research/1.5.3/bem-character-catalog/BEM_COMPONENTN_AUTOMATION.md) | ComponentN首次接入、后补32角色身份的历史过程 | 既有归档 | 1.5.3 |
| [EFMI 原资源身份补全](../research/1.5.3/bem-character-catalog/BEM_EFMI_IDENTITIES.md) | 360入口/1181纹理身份算法、版本manifest及10个多子网格限制 | 既有归档 | 1.5.3 |
| [BEM 模型管理局部修正（2026-10-03）](../research/1.5.3/bem-management/BEM_MODEL_MANAGEMENT.md) | 双端筛选/关闭全部的局部保存、UI及热切换证据 | 既有归档 | 1.5.3 |
| [BEM matching review (2026-10-01)](../research/1.5.3/bem-matching/BEM_MATCHING_REVIEW.md) | 共享Texture pin、错误元数据及Android导入匹配修正 | 既有归档 | 1.5.3 |
| [Better Endfield 角色模型替换可行性结论](../research/1.5.3/model-replacement/CHARACTER_MODEL_REPLACEMENT.md) | 2026-09-08选型、EFMI实测及客户端约束 | 既有归档 | 1.5.3 |
