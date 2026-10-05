# 相机、第一人称与 MMD

[全部文档](../INDEX.md)

## 现行说明与维护入口

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [任务 D：逐角色第一人称资料与 helper](FIRST_PERSON_PROFILES.md) | 逐角色profile/helper接入接口、规则覆盖与生成文件交接 | 维护参考 | 1.5.3 |

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

- [1.5.3 研究入口](research/1.5.3/INDEX.md)：17 篇。

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [EIEM upstream snapshot](../../native/modules/camera/eiem/UPSTREAM.md) | 固定EIEM快照、BE-PATCH、行为差异及双端适配边界 | 来源/证据 | 1.5.3 |
| [MmdAudio host JVM regression tests](../../native/tests/android_mmd_audio/README.md) | MediaPlayer/Looper替身、跨代竞争覆盖和历史已修复项 | 维护参考 | not_applicable |
| [MMD import JVM checks](../../native/tests/android_mmd_import/README.md) | 生产MMD解析/压缩包/导入规划的独立依赖和运行入口 | 维护参考 | not_applicable |
