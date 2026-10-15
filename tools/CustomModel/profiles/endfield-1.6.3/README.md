# Windows 1.6.3 新角色创作资料

BEM Tools 1.5.3 新增祀、明河及其 02/03 世界形态的原始资源合同、运行时布局和可编辑工程。本目录只提供目标资料，不包含完整原版模型或作者替换网格；平台均为 `windows-x64`。

## 祀：来源 Mod 自动识别

[正式角色目录](../../catalog/chr_0036_jsspsi.json) 已加入现有来源转换器，包含 15 个世界/详情配对部件、14 个原始索引资源身份和 58 个纹理资源身份。正常使用“检查来源 → 转换”即可按来源身份识别祀，无需手选 JSON。

当前 8 个部件符合已有来源布局规则。`brow_01` 和 `cloth_02/05/06/07/08/09` 的原始布局已采到，但来源转换规则尚不支持；其中 `brow_01` 有两个 submesh，未伪造单个索引身份。因此目录保持 `offline-resource-identities-partial`，不能承诺任意祀 Mod 自动转换成功。`cloth_04` 的世界/详情材质不同，未强行合并进旧双根目录。

目录的 `verified=true` 沿用“原生目标合同已观察”的含义，并不等于每个来源 Mod 已转换或替换画面已验证。现有转换器仍按部件布局、来源身份和材质规则检查。

## 明河三形态与显式目标

通过 [index.json](index.json) 选择准确的资源工程：

| 目标 | 工程 | LOD0 组件 |
| --- | --- | ---: |
| 祀完整世界＋详情 | [chr_0036_jsspsi-full.project.json](chr_0036_jsspsi-full.project.json) | 46（23＋23） |
| 祀世界 cloth_04 | [chr_0036_jsspsi-cloth04-world.project.json](chr_0036_jsspsi-cloth04-world.project.json) | 1 |
| 明河世界模型 | [chr_0037_minghe-world.project.json](chr_0037_minghe-world.project.json) | 20 |
| 明河世界模型 02 | [chr_0037_minghe02-world.project.json](chr_0037_minghe02-world.project.json) | 20 |
| 明河世界模型 03 | [chr_0037_minghe03-world.project.json](chr_0037_minghe03-world.project.json) | 20 |

每组同时包含 `.spec.json` 和 `.profile.json`。三个明河形态的实际角色 owner 都是 `chr_0037_minghe`，以精确资源根区分；没有虚构 `uimodel` 或新增角色身份。不同形态的材质合同分别保留。

这些 BEM 1.4 工程使用现有显式 resource/renderer/donor 格式，所有规则初始为 `keep`，没有替换 payload。作者应复制工程到自己的工作区，加入真实的 mesh/texture payload，再将对应规则改为 `replace`。可使用 GUI 的编辑工程打包路径，或在工具包根目录运行：

```powershell
.\BetterEndfieldNext.BemConverter.exe pack profiles/endfield-1.6.3/chr_0037_minghe-world.project.json -o minghe-keep.bem
.\BetterEndfieldNext.BemConverter.exe validate minghe-keep.bem
```

以上命令验证工程可读和格式正确，生成的 keep-only 包不会改变游戏模型。明河的显式资料不会被当成 legacy ComponentN 自动转换目录。`runtime_verified`、`conversion_ready`、`render_verified` 均保持 `false`；采到原始资源不等于验证了自定义替换、切换和恢复。

## 随包证据和缺项

- [采集摘要](evidence/capture-summary.json) 与同目录原始 JSONL 保存 5 个资源根、352 个 renderer 的真实 Windows 1.6.3 采集证据。
- 每个 profile 的 `evidence_file` 指向随包 `.native.json`，提供已观察的顶点声明/步长，以及从游戏原始 buffer、完整 mip 和运行时纹理绑定推导的 EFMI 资源身份。这些是业务资源身份，不是发布产物哈希。
- 显式资源的索引映射按 resource 分组，世界和详情之间不会因相同索引数据而误归并。不能自动映射的多 submesh/碰撞保留在 `identity_issues`。
- 明河 02/03 的公共 `T_default_mro_MRO` 原始纹理读取仍缺对应后端资源文件，身份未伪造；其他纹理身份已按各自实际绑定记录。
- 本目录全部相对引用随工具包移动；不要求原来的研究工作区。没有 Android donor、Android 资源声明或 Android 新版验证。

English: These are Windows 1.6.3 target resources. Jsspsi has a partial legacy catalog with real source identities; only supported per-component layouts can use ordinary source conversion. Minghe variants use exact BEM 1.4 world targets, all owned by `chr_0037_minghe`. The bundled explicit projects are editable keep-only starting points, not completed replacements. Readiness/rendering flags remain false and missing identities remain explicit.

## 昼雪 1.6.3 修订

[昼雪资料](SNOWSHINE.md) 更新当前世界/详情合同、EFMI 身份及 keep-only 工程；与既有祀、明河资料一并分发。
