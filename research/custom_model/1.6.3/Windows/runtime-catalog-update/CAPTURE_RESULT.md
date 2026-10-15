# 1.6.3 新角色开屏运行时采集

2026-10-15，run `bem-1_6_3-login-20261015-091033-3bb4aa`，游戏进程 `14396`。使用官方 Hypergryph 启动器正常启动，现成 BE XInput 自启动加载 `build/bem-capture-1.6.3` 中本轮编译的 Host、Model、CustomModel。服务器维护期间，通过 Model 的本地 I18N 资源加载路径完成采集。

| 资源根 | Renderer | 完整 | 尝试次数 |
| --- | ---: | --- | ---: |
| `chr_0036_jsspsi_postmodel` | 86 | 是 | 1 |
| `chr_0037_minghe_postmodel` | 81 | 是 | 1 |
| `chr_0037_minghe02_postmodel` | 81 | 是 | 1 |
| `chr_0037_minghe03_postmodel` | 81 | 是 | 1 |
| `chr_0036_jsspsi_uimodel` | 23 | 是 | 1 |

共 5/5 根、4 个角色/变体条目、352 条 renderer 记录，无未观察或不完整目标。祀的 UI 预制体通过精确资源路径临时送入同一个开屏加载器，观察发生在资源交付边界，不要求它在开屏正常播放动作，也未伪称进入过角色详情页。

原始 JSONL 与请求元数据已复制到本专题 `captures/`；运行汇总在 `runs/<run>/summary.json`。离线资料与已知差异见 [OFFLINE_INPUTS.md](OFFLINE_INPUTS.md)。每个资源均含网格数量/布局、骨骼顺序、bindpose、材质与 Shader 等信息。正式 BEM 入库与 EFMI 原资源身份补全是后续独立步骤，采集完成不等于所有部件已支持自动转换。

本轮先前两次准备/直接 EXE 启动尝试没有产生目标采样，不与本次结果合并。当前客户端直接 EXE 启动报“请通过官方启动器打开游戏”；原因另外调查，没有修改客户端启动检查。

用户正常退出后，已执行 `Cleanup-Capture.ps1`：本轮临时 XInput DLL、归属标记及启动标记均已移除，采集请求已解除；所有本轮改过的 INI 键均恢复原值，逐键差异为零。状态为 `restored`。游戏 EXE 和资源未改写，正式 BE 安装目录未覆盖。

352/352 记录与离线数据库的网格、索引/顶点数、骨骼顺序、材质槽及 bindpose 均匹配，运行时布局全部通过现有读取检查。四个条目已生成 `runtime-observations.json` 和 `database.observed.json`。祀 15 个可双根配对部件已进入独立 `catalog-staging`，其中 8 个布局支持现有转换规则、7 个暂不支持；源资源身份仍待补齐。明河仅 world 的资料按现有 BEM 1.4 资源目标路径准备，不伪造 UI 根。正式 catalog 未被覆盖。
