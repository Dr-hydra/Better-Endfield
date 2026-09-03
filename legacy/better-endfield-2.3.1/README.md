# Better Endfield 2.3.1 只读对照快照

此目录归档上一版 Windows 维护线截至指定提交的最终源码和随版本保存的参考文件。
它是后续适配新版 PC 客户端时的固定对照，不是当前构建输入。

## 版本边界

| 项目 | 值 |
| --- | --- |
| Better Endfield 版本 | `2.3.1` |
| 平台 | Windows 桌面端 |
| 正式发布起点 | `dd3d70069614d2c7d4789bbf21668e291831bc9d`（Release Better Endfield 2.3.1） |
| 归档截止提交 | `191b715987e74de95b4a28d02377546a8bb1830f`（Fix HUD hiding without freezing input） |
| 截止提交日期 | `2026-08-31 11:08:34 +08:00` |
| 归档日期 | `2026-09-03` |
| Git 标签 | 无 `v2.3.1` 标签；以归档截止提交的完整 ID 为准 |
| 游戏可读版本号 | 原提交未记录，不能可靠判定 |
| 游戏 VFS manifest | `5f521eb8-5202-dcdf-2412-02d992d0d771` |
| Windows manifest 输入 | `47235775` bytes；详细 SHA-256 见归档报告 |
| AudioDialog 输入 | `4697156` bytes；详细 SHA-256 见归档报告 |

这里的产品版本仍是 Better Endfield `2.3.1`，但源码范围不是只到发布提交：它包含
从 `dd3d700` 到用户指定的 `191b715` 为止的全部后续提交。游戏侧使用 VFS
manifest 标识作为精确兼容边界，不将后来 Android 客户端显示的 `1.5.3` 反推为
此 Windows 快照的游戏版本。

## 内容

`source/` 是截止提交的完整已跟踪文件树，共 737 个文件，未经修改，主要包括：

- `native/`：Host、加载器以及模型、语音、音乐、战斗数据和触控 UI 模块源码。
- `ui/`：2.3.1 Windows 管理界面源码及当时随版的模型、语音、战斗数据资源。
- `manifests/`：动作、角色短语音和战斗语义参考清单。
- `scripts/`：当时用于构建、资源导出和清单生成的脚本。
- `docs/`、`installer/`、`tools/`、解决方案和构建配置：复现历史实现所需的其余已跟踪文件。
- `web/`：截止提交时的战斗数据可视化工具源码与静态资源。
- `android/`：2.3.1 提交中已经存在的开发期状态，仅因完整快照而保留；它不代表
  Android 3.0.1 正式版。

`references/` 保存从项目外层和本地 AppData 剪切进来的旧版资源导出、IL2CPP
dump、运行时研究记录和试验源码，详见 `references/README.md`。

`local-only/` 保存只能本机保留的 PCK/BNK 等历史输入，由本目录的 `.gitignore`
隔离，不得提交或发布。

关键资源报告位于
`source/manifests/shared/resource-manifest-report.md`。其中记录了 31 个角色、
4075 个动作、937 个具名角色短语音 Event，以及四语 PCK Media 校验结果。

## 冻结约定

不要直接修改 `source/`。后续发现历史说明有误时，只修订本文件并保留更正记录；
若确实需要另一个历史状态，应新建独立版本目录并记录其来源提交。当前源码、资源
生成器和发布流程不得从本目录读取文件。

Git 可见的归档不包含从游戏客户端提取的 PCK、BNK、WEM、VFS 原始载荷或客户端
二进制。为满足本机完整剪切归档，相关历史输入仅放在被忽略的 `local-only/`，以及
`references/source-research/` 中按扩展名忽略的位置；不得将它们加入提交或发布物。
