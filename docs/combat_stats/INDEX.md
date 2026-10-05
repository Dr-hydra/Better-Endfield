# 战斗数据

[全部文档](../INDEX.md)

## 现行说明与维护入口

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Buff 资源表在线导出](BUFF_TABLE_EXPORT.md) | Buff 在线导出方法、游戏1.4.4字段及静态表发布边界 | 维护参考 | 1.5.3 |
| [Combat Runtime Contracts](COMBAT_RUNTIME_CONTRACTS.md) | 战斗数据解析、rDPS归属、快照与版本字段合同 | 维护参考 | 1.5.3 |

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

本模块没有独立迁入的版本研究；上述维护说明保留自身来源与适用范围。

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [战斗语义目录](../../manifests/combat/README.md) | combat-semantics.besem的消费者、字段、覆盖边界与生成入口 | 维护参考 | 1.5.3 |
| [战斗悬浮窗头像来源](../../native/modules/combat_stats/assets/SOURCE.md) | CEP角色头像来源、112×112处理和更新命令 | 来源/证据 | 1.5.3 |
| [CombatDataExporter](../../tools/CombatDataExporter/README.md) | 最新战斗字典、Buff反向索引和实际网站图标统一导出 | 维护参考 | 1.5.3 |
