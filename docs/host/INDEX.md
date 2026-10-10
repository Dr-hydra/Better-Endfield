# Host 与内置模块

[全部文档](../INDEX.md)

第三方模块及第一人称源码已迁移至 [Endfield Mod Loader](https://github.com/Dr-hydra/Endfield-Mod-Loader)。BE 保留通用 Host、内置模块加载和共享 Hook 链，第三方模块不再参与双端构建。

## 现行说明与维护入口

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Better Endfield Next Runtime Interfaces](GAME_INTERFACES.md) | Host/模块、动态IL2CPP、登录/语音等运行时接口 | 维护参考 | 1.5.3 |
| [Steam 国服启动使用与验证](STEAM_CN_LAUNCH.md) | Windows 预览、元数据更新、管理员选项与 XInput 冲突处理 | 3.5.3 预览功能 | not_applicable |

## 研究与阶段记录

九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。

- [1.5.3 研究入口](research/1.5.3/INDEX.md)：1 篇。
- [Steam 国服启动预研及实现记录：ACF、XInput 与权限](research/not_applicable/steam-launch/STEAM_CN_LAUNCH_FEASIBILITY_20261007.md)
- [Shizuku 免 root 可行性：进程内入口与权限边界](research/not_applicable/shizuku/SHIZUKU_FEASIBILITY_20261007.md)

## 邻接文档与分发来源

这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [HookInlineScan](../../tools/HookInlineScan/README.md) | 具名Hook入口是否仍被调用的静态扫描和内联诊断 | 维护参考 | not_applicable |
