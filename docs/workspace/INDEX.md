# 工作区与发布记录

[全部文档](../INDEX.md)

## 现行说明与维护入口

- [工作区政策](WORKSPACE_POLICY.md)
- [配置与日常入口](CONFIGURATION.md)
- [工作区构建](BUILDING.md)
- [测试规范](TESTING.md)

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

不依赖游戏版本的构建记录单独保存：

- [新工作区全量构建验证](research/not_applicable/workspace-build/BUILD_VALIDATION.md)

## 软件发布记录

以下目录号是 Better Endfield 软件版本，游戏版本未据此推断。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Better Endfield 3.4.1（2026-10-01）](releases/3.4.1/RELEASE_3_4_1.md) | 3.4.1双端发布范围、工具/实验开关及验证边界 | 发布记录 | not_applicable |
| [Better Endfield 3.4.2](releases/3.4.2/RELEASE_3_4_2.md) | 3.4.2/Tools1.4.1功能与下载验证记录 | 发布记录 | not_applicable |
| [Better Endfield 3.5.0](releases/3.5.0/RELEASE_3_5_0.md) | 3.5.0/Tools1.5.0发布元数据、构建结果及旧测试失败边界 | 发布记录 | not_applicable |
| [Better Endfield 3.5.1](releases/3.5.1/RELEASE_3_5_1.md) | 双端模型悬浮窗、Android全局FOV及发布验证 | 发布记录 | 1.5.3 |
| [Better Endfield 3.5.2](releases/3.5.2/RELEASE_3_5_2.md) | BEM 1.4、武器/大招资源、热切换、PCUI输入、Workshop与桌面布局 | 发布记录 | 1.5.3 |
| [Better Endfield 3.5.3](releases/3.5.3/RELEASE_3_5_3.md) | Windows Steam 国服启动预览、重复注入保护与独立 BEM GUI | 发布记录 | not_applicable |

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [更新日志](../../CHANGELOG.md) | 已发布版本行为变化与3.4.1链接 | 发布记录 | 1.5.3 |
| [Better Endfield](../../README.en.md) | 英文功能矩阵、安装及作者/模块/专题导航 | 发布记录 | 1.5.3 |
| [Better Endfield](../../README.md) | 功能矩阵、安装及作者/模块/专题导航 | 发布记录 | 1.5.3 |
| [Third-Party Notices](../../THIRD_PARTY_NOTICES.md) | EIEM/Endfield-Poser来源、许可及本地UPSTREAM说明 | 来源/证据 | 1.5.3 |
| [工作区测试规范与入口](TESTING.md) | parent/test 维护的工作区说明 | 维护参考 | not_applicable |
| [工作区迁移与资料管理规范（方案草案）](WORKSPACE_POLICY.md) | parent/test 维护的工作区说明 | 工作约束 | not_applicable |
| [终末地资源映射清单](../../manifests/shared/resource-manifest-report.md) | 动作/语音映射的版本、输入与覆盖统计 | 生成报告 | 1.5.3 |
| [Task C handoff (2026-10-03)](../../native/tests/async_loading/README.md) | Request/Poll/Cancel、所有权/预算接口及独立验证目标 | 维护参考 | not_applicable |
| [研究归档](../../research/README.md) | 轻量研究范围、postmodel结论名、旧实现与生产边界 | 历史过程 | 1.5.3 |
