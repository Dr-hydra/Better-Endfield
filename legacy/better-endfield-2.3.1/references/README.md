# 2.3.1 旧版参考资料

本目录保存从原工作位置剪切进来的旧版 PC 导出和逆向研究记录。兼容边界为
VFS manifest `5f521eb8-5202-dcdf-2412-02d992d0d771`，代码截止提交为
`191b715987e74de95b4a28d02377546a8bb1830f`。

## 原始目录归档

`source-research/` 保留原目录结构和文件内容：

- `character-catalog/`：旧版 32 个 PrefabInfo 角色候选、模型路径、4103 个动作、
  走路动画元数据以及对应的 Unity JSON 导出。该目录不含提弗洛斯。
- `table-current/`：旧版 `AudioDialog.bytes` 与解码后的 `AudioDialog.json`；其中
  `.bytes` 的大小和 SHA-256 与旧资源报告完全一致。
- `endfield-unpacker-decrypt-output/`：旧版 JsonData、PrefabInfo、原始 Lua 和解密
  Lua 导出。
- `endfield-unpacker-config/`：旧导出器的游戏目录缓存；外层工具下已清空该状态。
- `endfield-dumper/`：旧版完整 IL2CPP C#/AI dump、字符串、场景扫描、运行日志和
  候选报告。提交快照中的同版本副本仍位于 `../source/tools/EndfieldDumper/`。
- `appdata-research/`：截至 2026-08-31 的 HUD、相机、类型区间和 IDA 分析记录。
- `intro-animation-inspection/`、`lizhiyan-*`、`pelica-animation-check/`：登录模型、
  动画序列、Animator 和 MonoBehaviour 导出。
- `music-system/`、`bank-cache/`、`voice-sv-check/`、`wwiser-sample/`：旧语音与
  Wwise 结构研究。
- `experiments/` 和 `model-resource-findings.md`：旧模型替换试验源码与结论。
- `runs/`：旧 Dumper 的逐次运行目录。
- `maps/`：旧版资源和登录主题映射文件。

## 本地载荷

`../local-only/` 保存旧版 `state/`、日语 PCK 和 `bnk-v1d4` 原始输入。其中
`state/vfs-manifest/` 包含 2026-08-03 的旧版完整 `manifest.hgmmap` 和解码后的
`manifest.json`，其 VFS UUID 与最终清单一致，但 hgmmap 大小为 `46497641`
bytes，不是资源报告记录的 2026-08-13 最终输入 `47235775` bytes。最终输入原件
当前已不存在，因此精确边界仍以 `../source/manifests/shared/resource-manifest-report.md`
记录的大小和 SHA-256 为准。旧 IDA 数据库和其他大型二进制状态也一并保留。
这些内容只用于本机复查，受 `../.gitignore` 排除，不得提交或发布。位于
`source-research/` 内的 `.ab/.bnk/.wem/.chk`、工具二进制和导出图片同样被排除。

## 外层清理结果

完成移动后：

- 项目根部 `research/` 只保留 2026-09-02 新版输入及其生成结果：
  `current-inputs`、`character-catalog-current`、`table-dump`、`combat-jsondata`，
  以及 `bank-pck/bnk`。
- `tools/EndfieldDumper/` 已移走，制作新 dump 时必须创建新的运行目录。
- `tools/EndfieldUnpacker/DecryptOutput/` 已移走，新导出会从空目录开始。
- 根目录 `state/`、`runs/` 和 `Maps/` 已移走，新一轮研究会从空状态开始。
- `%LOCALAPPDATA%/BetterEndfield/research/` 已移走，新版运行时研究不会与旧记录混合。

不要在本目录内继续运行导出器或 Dumper。新版输出应先进入新的日期目录，确认
版本和完整性后再决定是否建立下一份独立归档。
