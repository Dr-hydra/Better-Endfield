Better Endfield Next **4.1.2** 与 BEM Tools **1.6.3**，带来管理员外观模块 DLL 修复与双端默认关闭、持续冲刺与安卓界面排布优化、BEM Tools Android CLI 源码发行包与 DeepSeek Harness 全功能三维模型预览插件。

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

- **DSH 插件补齐工具面与预览面**：`dsh-bem-creator` 从「只带指令」变成「skill + 工具 + 预览页」，在会话里直接看成品模型，不用来回导出截图。
  - `bem` 工具提供 `inspect` / `validate` / `preview` / `convert` / `doctor`，读包、查渲染级问题、建预览场景、调 CLI 都走同一个入口；工具用裸 JSON Schema 自注册，profile 里解析不到 `@deepseek-ai/dsh-tools` 也能装；
  - 预览页挂在会话视图槽位：拖拽旋转、滚轮/双指缩放、外观与选项组切换、形态参数滑条、部件勾选、贴图/素模切换；滑条真的在动网格（读 `mesh_deformations` 稀疏帧做插值后回传位置缓冲）；
  - 贴图在 1–4 个 worker 里解 BC1/BC3/BC4/BC5/BC7，解完立刻丢 CPU 副本，避免五张 4096×2048 的 RGBA 把手机 WebView 撑爆；ASTC 明确不解码，宿主干脆不发 blob 并带 `skipped` 原因，客户端只显示一句「N 张 ASTC 贴图不在浏览器内解码」，几何照常显示；
  - 预览能站直了：包里的顶点是 Z-up，相机是 Y-up，宿主按包围盒判定后一次性旋转位置与形变增量，模型不再横躺；着色改成线性空间计算、末尾 gamma 编码，并让法线始终朝向视点（网格沿 UV 缝拆点、绕序混杂，`gl_FrontFacing` 不可信），素模平均亮度约翻倍，取景也改按垂直视场计算，头脚不再被裁；
  - 插件改为随包携带 BEM Tools 源码树（2.0 MB），`convert` 不再需要机器上装过 bemtools；宿主只需有 Python 3.10+（3.14+ 免依赖，更早版本 `pip install zstandard`），缺什么 `doctor` 会直接给出补救命令。

- **产物版本对齐 1.6.3**：Windows 工具包、Android CLI 与 DSH 插件三件产物同版本发布；DSH 插件版本与 `TOOL_VERSION` 由构建脚本强校验。

## 下载

| 附件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.2-Setup.exe | Windows x64 主程序安装包 |
| BetterEndfieldNext-4.1.2-Android-arm64.apk | Android 4.1.2 Release APK |
| BEM-Tools-1.6.3-win-x64.zip | 完整独立创作者 GUI、CLI 和全量角色资料 |
| BEM-Tools-1.6.3-android-arm64.tar.gz | 安卓端 BEM CLI（源码形态，供 agent 在 Termux / root shell 直接调用） |
| dsh-bem-creator-1.6.3.tgz | DeepSeek Harness 插件（创作者 skill + bem 工具 + 会话内模型预览） |
