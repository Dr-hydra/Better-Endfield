# 项目文档导航

现行说明按模块维护；版本研究放在 `docs/<module>/research/<game-version>/<topic>/`。用户已确认九月初更新后为 1.5.3，此前资料使用 pre-1.5.3；同版热更新由资源快照区分。通用 BEM 格式、工具与替身测试不绑定游戏版本。研究、已实施阶段、部分取代结论与既有归档保留原文的验证范围和来源。

| 模块 | 入口 |
| --- | --- |
| 自定义模型与 BEM | [custom_model](custom_model/INDEX.md) |
| 相机与 MMD（含历史第一人称研究） | [camera](camera/INDEX.md) |
| 动作与特殊冲刺 | [actions](actions/INDEX.md) |
| 战斗数据 | [combat_stats](combat_stats/INDEX.md) |
| Host 与内置模块 | [host](host/INDEX.md) |
| 语音 | [voice](voice/INDEX.md) |
| 音乐输入与 OmniMix | [music](music/INDEX.md) |
| Android 平台 | [android](android/INDEX.md) |
| 界面与显示 | [ui](ui/INDEX.md) |
| 登录展示模型 | [model](model/INDEX.md) |
| 网页与后端 | [web](web/INDEX.md) |
| 工作区与发布记录 | [workspace](workspace/INDEX.md) |

## 清单与引用

- [文档用途、状态、版本与来源清单](../config/document-catalog.json)
- [公开文件名到源码位置](../config/documents.json)：安装目录文档名保持原 basename。
- [全量旧路径到新路径映射](../config/document-path-migrations.json)：供外部消费者、代码注释和证据引用集成。
- [迁移方法与离线检查](workspace/DOCUMENT_MIGRATION.md)
