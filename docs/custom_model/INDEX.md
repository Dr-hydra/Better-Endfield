# 自定义模型与 BEM

[全部文档](../INDEX.md)

## 现行说明与维护入口

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM Creator Guide](BEM_CREATOR_GUIDE.en.md) | 英文创作、工程、导出、分发指南 | 现行规范 | not_applicable |
| [BEM 创作者指南](BEM_CREATOR_GUIDE.md) | 中文创作、工程、导出、分发指南及软件内帮助原文 | 现行规范 | not_applicable |
| [BEM Format Specification (1.0–1.3)](BEM_FORMAT_SPEC.en.md) | 英文合并 BEM 1.0–1.3 容器、manifest 与字段规范 | 现行规范 | not_applicable |
| [BEM 格式规范（1.0–1.3）](BEM_FORMAT_SPEC.md) | 中文合并 BEM 1.0–1.3 容器、manifest 与字段规范 | 现行规范 | not_applicable |
| [BEM Runtime Behavior and Compatibility](BEM_RUNTIME_COMPATIBILITY.en.md) | 英文实际匹配、加载、热切换与拒绝信息 | 现行规范 | 1.5.3 |
| [BEM 运行时行为与兼容性](BEM_RUNTIME_COMPATIBILITY.md) | 中文实际匹配、加载、热切换与拒绝信息 | 现行规范 | 1.5.3 |
| [Converting Source Mods to BEM](BEM_SOURCE_MOD_CONVERSION.en.md) | 英文来源自动化、身份、配方和报错说明 | 现行规范 | not_applicable |
| [其他来源 Mod 转换为 BEM](BEM_SOURCE_MOD_CONVERSION.md) | 中文来源自动化、身份、配方和报错说明 | 现行规范 | not_applicable |
| [CustomModel 已知问题](CUSTOM_MODEL_KNOWN_ISSUES.md) | CM-001未定根因的问题登记、处理决定及证据入口 | 维护参考 | 1.5.3 |
| [通用原生角色资料解析器](CUSTOM_MODEL_NATIVE_PARSER.md) | 按需原生资料解析、数据结构及观测合并维护流程 | 维护参考 | 1.5.3 |

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

- [1.5.3 研究入口](research/1.5.3/INDEX.md)：51 篇。

## 历史 BEM 格式与通用工具研究

[格式版本、工具研究与既有归档入口](history/INDEX.md)。通用格式和工具资料不绑定游戏版本；当前中英文四组规范是本模块根目录的唯一维护来源。

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Runtime B handoff — 2026-10-03](../../native/modules/custom_model/model_runtime_notes.md) | 线程/身份/所有权、晚间回归修复和逐纹理解码追加记录 | 历史过程 | 1.5.3 |
| [README.betterendfield.md](../../native/shared/third_party/astcenc/README.betterendfield.md) | astcenc5.2.0固定commit、未修改上游及本地解码接口 | 来源/证据 | not_applicable |
| [README.betterendfield.md](../../native/shared/third_party/bcdec/README.betterendfield.md) | bcdec0.98固定commit、双许可与本地未修改声明 | 来源/证据 | not_applicable |
| [CustomModel 历史研究归档](../../research/custom-model/README.md) | 旧实现迁移位置、相对结构及仍用probe/convert_efmi_poc边界 | 历史过程 | 1.5.3 |
| [BEMv1 创作工具与源模型校验](../../tools/CustomModel/README.md) | CLI/Blender/资料parser/样本入口与历史能力导航 | 维护参考 | 1.5.3 |
| [BEM Blender exporter](../../tools/CustomModel/blender_addon/README.md) | Blender导出可编辑工程、显式对象契约与快速使用 | 维护参考 | 1.5.3 |
| [原生角色资料探针开发工具](../../tools/CustomModel/developer-tools/README.md) | 唯一native_probe、跨重启采集、停止和身份重算流程 | 维护参考 | 1.5.3 |
| [转换示例](../../tools/CustomModel/examples/README.md) | 真实资料配方、体型滑条、ShapeKey及workspace示例约束 | 维护参考 | 1.5.3 |
| [BEM creator workflow](../../tools/CustomModel/skills/bem-creator/SKILL.md) | 工具选择、英文相对参考路径和输出规则 | 分发参考副本 | not_applicable |
| [BEM Creator Guide](../../tools/CustomModel/skills/bem-creator/references/BEM_CREATOR_GUIDE.en.md) | 随技能分发的英文参考；主要内容来源docs/BEM_CREATOR_GUIDE.en.md | 分发参考副本 | not_applicable |
| [BEM 创作者指南](../../tools/CustomModel/skills/bem-creator/references/BEM_CREATOR_GUIDE.md) | 随技能分发的中文参考；主要内容来源docs/BEM_CREATOR_GUIDE.md | 分发参考副本 | not_applicable |
| [BEM Format Specification (1.0–1.3)](../../tools/CustomModel/skills/bem-creator/references/BEM_FORMAT_SPEC.en.md) | 随技能分发的英文参考；主要内容来源docs/BEM_FORMAT_SPEC.en.md | 分发参考副本 | not_applicable |
| [BEM 格式规范（1.0–1.3）](../../tools/CustomModel/skills/bem-creator/references/BEM_FORMAT_SPEC.md) | 随技能分发的中文参考；主要内容来源docs/BEM_FORMAT_SPEC.md | 分发参考副本 | not_applicable |
| [BEM Runtime Behavior and Compatibility](../../tools/CustomModel/skills/bem-creator/references/BEM_RUNTIME_COMPATIBILITY.en.md) | 随技能分发的英文参考；主要内容来源docs/BEM_RUNTIME_COMPATIBILITY.en.md | 分发参考副本 | not_applicable |
| [BEM 运行时行为与兼容性](../../tools/CustomModel/skills/bem-creator/references/BEM_RUNTIME_COMPATIBILITY.md) | 随技能分发的中文参考；主要内容来源docs/BEM_RUNTIME_COMPATIBILITY.md | 分发参考副本 | not_applicable |
| [Converting Source Mods to BEM](../../tools/CustomModel/skills/bem-creator/references/BEM_SOURCE_MOD_CONVERSION.en.md) | 随技能分发的英文参考；主要内容来源docs/BEM_SOURCE_MOD_CONVERSION.en.md | 分发参考副本 | not_applicable |
| [其他来源 Mod 转换为 BEM](../../tools/CustomModel/skills/bem-creator/references/BEM_SOURCE_MOD_CONVERSION.md) | 随技能分发的中文参考；主要内容来源docs/BEM_SOURCE_MOD_CONVERSION.md | 分发参考副本 | not_applicable |
