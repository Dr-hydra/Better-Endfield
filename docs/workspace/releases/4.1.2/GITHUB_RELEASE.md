Better Endfield Next **4.1.2** 开发记录。

## Windows 4.1.2 & Android 4.1.2

- **桌面端角色外观新增「打开模型文件夹」快捷入口**：
  - 「角色外观」管理页顶部的快捷按钮明确更名为「打开模型文件夹」；
  - 点击后直接打开实际存放 `.bem` 文件的目录（`<程序安装目录>/models`，若无写权限则自动回退 AppData 目录），方便直接查看、备份与管理模型包文件。

- **安卓端界面布局优化：移除顶部横幅，底部导航四等分**：
  - 移除主界面顶部大横幅，减少纵向空间占用；
  - 将赞助按钮移至底部导航栏并取消特殊高亮色，统一采用标签页视觉样式；
  - 底部导航栏重构为「主页 / 增强功能 / 关于 / 赞助」四键并列，采用等分宽度（`weight=1`）与文字动态自适应缩放，确保在不同分辨率与屏幕比例下恒定保持四等分对称排布。

- **优化桌面端持续冲刺界面层级排布**：
  - 将「隐藏机甲与光效」选项与说明调整至「梨诺」设置项正下方并缩进展示，修正此前「祀」插入在梨诺与其子选项之间导致的排布与归属混乱；
  - 机甲光效子选项与梨诺启用状态联动，未启用梨诺时自动禁用子项，层级关系一目了然。

- **修复桌面端发布遗漏管理员外观模块动态库**：
  - 修复 `native/CMakeLists.txt` 中 `BetterEndfieldNext.Layout` 目标遗漏 `BetterEndfieldNext.WeaponAppearance` 依赖项的问题，确保 Windows 完整构建时自动编译该 DLL 并部署到 `modules/` 目录；
  - 在 `BuildBetterEndfieldNext.ps1` 发布前完整性检查清单中补齐 `BetterEndfieldNext.WeaponAppearance.dll` 与 `betterendfieldnext.weapon_appearance.module.ini` 等模块文件，避免构建与打包漏发。

- **管理员外观功能规范化：双端统一默认关闭**：
  - 桌面端 `WeaponAppearanceConfiguration` 默认启用状态、ini 读取缺省回退值以及 `betterendfieldnext.weapon_appearance.module.ini` 清单全部调整为 `enabled=false`；
  - 双端共享原生模块 `weapon_appearance/module.cpp` 的默认运行配置结构体同步调整为 `enabled = false`；
  - 与安卓端 `ModuleSettings.isAppearanceEnabled`（默认 `false`）完全对齐，消除未配置时自动覆写外观与特效的行为，双端统一遵循“默认保持原版、用户手动选择开启”的行为规范。

- **BEM Tools 新增 Android CLI 产物（1.6.3）**：新增 `BEM-Tools-1.6.3-android-arm64.tar.gz`，让手机上的 agent 自己完成 `inspect` / `convert` / `validate` / `pack`，不必把源包传回 Windows。
  - 不是 APK，也不是 GUI：解压后是源码形态的 `app/` 加一个 mksh 兼容的 `bem-tools` 入口，负责 `TMPDIR`、`PYTHONPATH`、UTF-8 与退出码，并转发 `doctor` / `install-wheel` / `repair`；
  - 源格式只保留目录与 ZIP，RAR/7z 按 `ARCHIVE_BACKEND` 拒绝；成品 2.0 MB 压缩 / 11.5 MB 解压，默认装到 `/data/local/tmp/bem-cli`。

- **Android CLI 在 Python 3.14 上零依赖**：Termux 现装的 3.14.8 自带 `compression.zstd`，新增 `zstd_compat.py` 在扩展缺失时接管；两条后端输出逐字节相同（zstd 1.5.7），手机产出的包与 Windows 产出的 digest 一致，`doctor` 的 `deps.zstd_backend` 会说明当前走哪条。

- **随包提供 bionic arm64 的 zstandard wheel**：用 NDK 交叉编译出 `zstandard-0.25.0-cp314-cp314-android_24_arm64_v8a.whl`（301 KB），`bem-tools install-wheel` 按本机 `EXT_SUFFIX` 解包复制，不走 pip、不需要设备上的编译器。

- **产物版本对齐 1.6.3**：Windows 工具包、Android CLI 与 DSH 插件三件产物同版本发布；DSH 插件版本与 `TOOL_VERSION` 由构建脚本强校验。

## 下载

候选包产出后填写。
