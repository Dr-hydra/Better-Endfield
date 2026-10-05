# Host 与第三方模块

[全部文档](../INDEX.md)

## 现行说明与维护入口

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [双端第三方模块加载器与 UI 容器实现](CREATOR_MODULE_API_DESIGN.md) | 已实现第三方加载器架构、Host/作者责任及后续工作 | 维护参考 | not_applicable |
| [Better Endfield Runtime Interfaces](GAME_INTERFACES.md) | Host/模块、动态IL2CPP、登录/语音等运行时接口 | 维护参考 | 1.5.3 |
| [第三方模块创作者指南](THIRD_PARTY_MODULE_CREATOR_GUIDE.md) | 第三方包格式1/Native ABI1/SDK1.0.0作者接入指南 | 现行规范 | not_applicable |

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

- [1.5.3 研究入口](research/1.5.3/INDEX.md)：1 篇。

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [HookInlineScan](../../tools/HookInlineScan/README.md) | 具名Hook入口是否仍被调用的静态扫描和内联诊断 | 维护参考 | not_applicable |
