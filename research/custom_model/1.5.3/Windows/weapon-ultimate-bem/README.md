# 武器与庄方宜大招：离线研究证据

完整结论见 `docs/custom_model/research/1.5.3/weapon-ultimate/BEM_WEAPON_ULTIMATE_FEASIBILITY.md`。

- `summary.json`：从当前 Windows VFS 定向提取的 8 个目标、133 个 bundle 的精简审计；233 个 SMR 与 4 个静态 Renderer。
- `audit.py`：读取本地 `native-extended.json`，重建摘要；不改游戏、不计算产物哈希。
- `protocol_probe.py`／`protocol-probe.json`：现有 BEM 1.3 的合成格式探针，非可玩 Mod。
- `reader-probe.patch`：在正式 NativeAssetReader 源码副本上增加 MeshRenderer／MeshFilter 元数据支持的研究差异。正式源码未改。

`inputs/`、原始图谱、解析图谱、日志、研究读取器副本和合成包只保留在本机，均不纳入 Git。没有进行设备安装或游戏内验证。

从仓库根目录运行：

```powershell
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/audit.py
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/protocol_probe.py
```

当前提取输入由 `tools/CustomModel/extract_native_bundles.py` 产生：`--character chr_0030_zhuangfy` 加六个 `--extra-asset`，具体完整路径保存在 `summary.json.selected_assets`，普通 world/UI 两项由脚本自动添加。工作区重排后执行该脚本需令 `PYTHONPATH` 包含仓库 `scripts` 目录。

读取器原型沿用 `tools/CustomModel/NativeAssetReader` 的 C# 文件和项目，在副本应用 `reader-probe.patch`，指定 `AnimeStudioDir=toolchains/native-asset-reader/identities` 构建。其输出保存为 `native-extended.json`。底层有公共 bundle 子流告警和研究工具首次启动 CNKeys 告警，摘要与日志保留这些限制。
