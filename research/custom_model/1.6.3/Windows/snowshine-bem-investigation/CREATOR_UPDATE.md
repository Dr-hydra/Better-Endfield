# BEM Tools 1.5.4 本地资料更新

2026-10-15。已更新源码资料并生成本地工具整包与资料补丁；没有更新 GitHub 发布附件。本文件记录 creator 工具侧工作，BE 4.0.4 的旧 BEM 运行时兼容由独立改动提供。

## 采集与合同

- world：`aurora-1_6_3-login-20261015`，PID 17216，53 条 renderer。
- UI：`aurora-1_6_3-login-ui-20261015`，PID 9064，12 条 renderer。
- 两次 session 分开保留，共 65 条记录均通过现有 `validate_observation` 与当前离线资料的精确引用、顶点数、索引数、骨骼路径/顺序、材质槽及 bindpose 校验。bindpose 使用 importer 已有的显式转置。
- 昼雪 legacy catalog 保留原 12 个组件编号。C1 更新为 87,288 个索引、23,141 个顶点及当前骨骼排列；EFMI 索引身份从 `0671628a` 更新为当前原资源计算出的 `e21af1da`，不把旧身份指向新合同。
- C5 的当前运行时步长为 `[40,16,32]`，来源布局依然不在现有自动转换规则范围，`source_layout_supported=false`。
- 新目录仍有 11 个可推导单区域索引身份、41 个纹理身份、8/12 个支持的来源布局。多 submesh 的 fur 仍保留身份缺项。
- 新增 `profiles/endfield-1.6.3/chr_0014_aurora-full.*`，世界/详情共 24 个 LOD0 组件，全部为 keep-only；三个替换/准备标志保持 false。它不包含游戏原网格或纹理 payload，也不宣称新增 Android 支持。

必要的 EFMI 业务身份来自本次 `NativeAssetReader --resource-identities`，不是分发产物哈希。没有计算整包哈希。

## 产物

| 产物 | 位置 | 字节 |
| --- | --- | ---: |
| 独立 GUI/CLI 完整包 | `releases/4.0.4/BEM-Tools-1.5.4-win-x64.zip` | 72,854,215 |
| 仅 catalog/profiles 资料补丁 | `releases/4.0.4/BEM-Tools-1.5.4-data-patch.zip` | 1,359,331 |
| BE 内置工具目录 | `build/release-4.0.4/tools/bem/dist/BetterEndfieldNext.BemConverter/` | — |
| 最终 ZIP 解压验证目录 | `build/release-4.0.4/bem154-portable/BEM-Tools/` | — |

补丁包含 86 个资料文件，可放到独立 BEM-Tools 根目录或 BE 的 `tools/BemConverter/`。完整包和补丁均保留之前加入的祀、明河三个形态资料。工具版本为 1.5.4，BE 总版本为 4.0.4；旧 4.0.3 产物未覆盖。

## 验证

- `python -m unittest test_import_runtime_catalog test_catalog_163 -v`：11 项通过，包含 bindpose 转置、旧合同拒绝、当前昼雪源身份匹配、旧索引数拒绝、两次独立采集证据和全部 6 套工程。
- 使用**最终 ZIP 解压后的**便携 CLI 实际执行 6 套工程的 `pack` 与 `validate`，全部成功。目录数据与源码逐文件按字节比较相等；资料补丁 86 个条目也完成逐字节核对。
- CLI 输出 `BEM Tools 1.5.4 / BEM 1.0+1.1+1.2+1.3+1.4`；GUI 产品版本为 1.5.4，文件版本为 1.5.4.0。
- 详细便携验证结果：`build/release-4.0.4/bem154-verification/summary.json`。

重建资料入口：`promote_creator_data.py --captures ../aurora-compat-runtime/captures --runs aurora-1_6_3-login-20261015 aurora-1_6_3-login-ui-20261015 --output <空目录> [--promote]`。`--promote` 仅更新仓库工具资料，不部署用户安装、不发布附件。
