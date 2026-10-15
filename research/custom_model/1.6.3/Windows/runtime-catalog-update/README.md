# 新增角色 BEM 资料采集准备

状态：2026-10-15 已经通过官方启动器、临时 XInput 自启动和开屏模型路径采齐 5 个目标，共 352 条完整 renderer 记录；无需连接服务器进入地图。结果见 [CAPTURE_RESULT.md](CAPTURE_RESULT.md)。以下保留采集范围与入库限制。

相对原 33 个模型条目，新增祀、明河及明河 02/03 变体；它们在正式 `tools/CustomModel/catalog/` 中均无条目。噗切娜已在此前入库。

| 条目 | 当前可采资源 |
| --- | --- |
| `chr_0036_jsspsi` 祀 | `chr_0036_jsspsi_uimodel`、`chr_0036_jsspsi_postmodel` |
| `chr_0037_minghe` 明河 | `chr_0037_minghe_postmodel` |
| `chr_0037_minghe02` 明河模型 02 | `chr_0037_minghe02_postmodel` |
| `chr_0037_minghe03` 明河模型 03 | `chr_0037_minghe03_postmodel` |

共 5 个资源根。现有 `runtime_sweep.make_request` 已验证接受这些真实 ID；02/03 无额外下划线，不需要修改 ID 正则。

运行时需要实际触发原生资源加载：祀详情页对应 UI 根；世界/开屏模型对应 postmodel 根。现有开屏预设已包含四个条目的模型及动作路径，可用作定向加载入口，但当前客户端尚未实测加载。sweep 是被动观察，不会自动加载全角色。

采集内容为 renderer 路径、Mesh 顶点/索引数、顶点声明和 stride、骨骼路径和顺序、bindpose、材质及 Shader 等原生契约。它不等于 EFMI GPU hash/纹理身份已经建立，也不直接批准模型转换。

现有链条的实际限制：

- `prepare_sweep_offline.py`/`extract_native_bundles.py` 的角色入口要求 world/UI 双根；祀可以沿用。
- `import_runtime_catalog.py:build_role` 同样要求 world/UI 部件配对。明河三个条目当前没有 UI 根，直接调用会排除全部部件。
- 因此明河可先保存这三路运行时观测，离线按实际 world 资源提取；正式入库需明确支持 world-only 目标，不能构造不存在的 UI 根或冒称双路等价。
- 采集前应构建当前 CustomModel 探针并确认实际 BE 安装目录。旧本机 `test.be_install_dir` 仍指 E 盘，不能把它未经核实当作新版部署目标。
- 本次应在独立 catalog 暂存目录导入、补 source identities 后，再合并正式目录。现有 importer 会按本次数据库重写 coverage，而 identities 脚本会遍历所给 catalog 全部角色；直接指向正式目录可能缩减 coverage 或触发旧角色全量重读。
- 当前 manifest 的 Version GUID 与历史相同，不能单凭它混用旧采样。保留本轮 1.6.3 输入目录、实际资源来源和独立 run。

现有入口：`tools/CustomModel/developer-tools/BuildProbe.ps1`、`Probe.ps1`、`runtime_sweep.py`。专题的 `capture_control.py` 管理 plan/arm/select/status/stop/restore，保留本轮改动键的原值；`Start-Capture.ps1` 安装现成代理并准备请求，随后由官方启动器正常启动。当前客户端直接运行 EXE 会提示通过官方启动器打开，不使用该路径采集。

临时 `[Loader] install_root` 和 `[Host] modules_root` 都指向 `build/bem-capture-1.6.3`，仅加载 Model/CustomModel。不改变 LOCALAPPDATA，沿用现有着色器缓存。采集结束先 stop，游戏正常退出后 restore，并卸载本轮临时代理。
