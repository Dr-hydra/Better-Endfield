# 原生角色资料探针开发工具

版本更新后复用的入口。`native_probe.inl` 是唯一探针源码，由生产 CustomModel 模块包含；没有请求文件时不采集。开发脚本保留在仓库，不加入普通玩家转换工具依赖。

本目录 `BuildProbe.ps1` 构建模块并运行针对性探针检查，可通过 `-DeployTo` 覆盖指定测试目录。使用前先按项目正常 CMake 流程配置 build，替换已加载 DLL 前退出游戏。

原生资料入库后，用 `CompleteSourceIdentities.ps1` 从相同版本离线资源重算 EFMI 原索引和纹理身份。该步骤不需要作者 Mod，也不重新启动游戏；命令和限制见 `docs/BEM_EFMI_IDENTITIES_20260920.md`。新版本更新必须重新核验，不能仅沿用旧 hash 表。

```powershell
# 从仓库根目录执行。先更新离线 manifest，不能沿用旧游戏版本标注。
./tools/CustomModel/developer-tools/Probe.ps1 -Action arm -Manifest artifacts/native-parser/endmin-inputs/Bundles/Windows/manifest.json -Characters chr_0002_endminm,chr_0017_yvonne,chr_0026_lastrite,chr_0033_camille
./tools/CustomModel/developer-tools/Probe.ps1 -Action status
./tools/CustomModel/developer-tools/Probe.ps1 -Action stop
```

默认跨重启持续采集，直至执行 stop；`-SingleSession` 可选择一次性采集。每次新游戏进程写入独立的 `<run>.<pid>.<tick>.jsonl`，共享这次采集任务的 manifest 与角色范围。汇总跨文件展示进度，但不会把不同进程写进同一原始文件；重复根的布局/骨骼/bindpose/材质契约冲突会单独报告。
每个进程内成功资源去重；重启后如果再次遇到同一角色，保留独立证据。没有再打开的角色保留上次结果，无需重看。

采集期间暂停本进程的模型替换，不修改用户设置。stop 移除待启动请求并通知正在运行的探针；当前进程仍保持替换暂停，下次启动恢复正常。采完后务必 stop；游戏版本更新后停止旧任务，用新 manifest 建立新 run，不跨版本合并。

数据在 `%LOCALAPPDATA%/BetterEndfield/catalog/custom-model/native-probe/`。status 可在游戏关闭后查看；`--run`/`-Run` 可选择历史任务。
矩阵、布局与材质信息是原生观测；EFMI GPU hash、Shader 语义和 world/UI 等价性仍需离线证据，不能仅据采集完成自动批准转换。

采集后的批量流程：上级目录 `prepare_sweep_offline.py` 联合提取当前快照依赖并逐角色解析，再由 `import_runtime_catalog.py` 核对身份、矩阵、骨骼和材质后生成 catalog。每个部件保留直接/等价复用来源，差异部件记录在排除清单；已有 EFMI 对应仅在原生契约一致时保留。用法和本次覆盖见 `docs/BEM_CHARACTER_CATALOG_20260919.md`。
定向探针工具保留在上一级 `runtime_native_probe.py`，完整说明见 `docs/CUSTOM_MODEL_RUNTIME_SWEEP_20260919.md`。
