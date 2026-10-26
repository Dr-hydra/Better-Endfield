# Better Endfield Next 4.1.2

本版为 **Windows 4.1.2 与 Android 4.1.2（40102）** 的开发记录，BEM Tools 维持 **1.6.3**。
开发进行中，各项完成后逐条追加；构建与验证结果在候选包产出后补写。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)；产物清单见同目录 [ASSETS.json](ASSETS.json)。

## 更新

<!-- 每完成一项就往这里追加一条，格式与 4.1.1 发布记录保持一致。 -->

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
  - 不是 APK，也不是 GUI：解压后是源码形态的 `app/` 加一个 mksh 兼容的 `bem-tools` 入口；入口解析自身路径（含符号链接）、把 `TMPDIR` 指向 `/data/local/tmp`、注入 `PYTHONPATH` 与 UTF-8、透传 exit 0/2，并转发 `doctor` / `install-wheel` / `repair`；
  - 源格式只保留目录与 ZIP：RAR/7z 需要 7-Zip 后端，发行包不带，命中时按 `ARCHIVE_BACKEND` 拒绝而不是缺可执行文件；
  - 打包用 AST 可达性分析从入口算白名单，模块从 39 降到 24；成品 2.0 MB 压缩 / 11.5 MB 解压；
  - 默认装到 `/data/local/tmp/bem-cli`：`/sdcard` 挂载为 `noexec`，Android 10 起应用私有目录也被 SELinux 拒绝 `execve`。构建入口 `scripts/BuildBemCliAndroid.ps1`。

- **Android CLI 在 Python 3.14 上零依赖**：Termux 当前 `pkg install python` 装的是 3.14.8，自带 PEP 784 的 `compression.zstd`，设备不必再装 `zstandard`。
  - 新增 `zstd_compat.py`：扩展缺失时把标准库实现挂成 `zstandard`，扩展存在时完全不接管，Windows 行为不变；`doctor` 的 `deps.zstd_backend` 报 `stdlib` 或 `cext`；
  - 两条后端输出逐字节相同（都是 zstd 1.5.7），手机产出的包与 Windows 产出的 digest 一致；用真实 `TohsakaRin-aglina-by-Dr-hydra.bem` 在 3.13（扩展）与 3.14（标准库）上校验，除源路径外输出完全一致；
  - 回归：`audit/test_bem_repair.py` 141 例、`test_bem_v1` 9 例、`test_bem_projects` 10 例、`test_bem_v11` 13 例（1 skip）在两套后端上全绿。

- **随包提供 bionic arm64 的 zstandard wheel**：Termux 没有 zstandard 包，PyPI 只有 glibc/musl 轮子，`pip install zstandard` 会在设备上拉 clang 现编。
  - 用 NDK 27 clang 加 Termux `python` 包内的 3.14 头文件交叉编译 `zstandard 0.25.0` 的 C 扩展，产出 `zstandard-0.25.0-cp314-cp314-android_24_arm64_v8a.whl`（301 KB；strip 后 604 KB 的 ELF64/AArch64 共享对象，只依赖 `libc`/`libm`/`libdl`，只导出 `PyInit_backend_c`）；
  - `bem-tools install-wheel` 不走 pip 而是解包复制：Termux 的 Python 报 `sys.platform == 'android'`，pip 认 `android_<api>_arm64_v8a` 标签，Chaquopy 一类 Linux 形态解释器认 `linux_aarch64`，一个 wheel 文件名只能带一个平台标签；安装器按本机 `EXT_SUFFIX` 落盘，两种宿主都适用且不需要编译器；
  - 构建脚本 `tools/CustomModel/android/build_zstandard_wheel.py`，Termux 换 Python 版本后重跑即可。

- **创作者技能补 Android 运行规范，SKILL.md 增加宿主分叉**：新增 `references/ANDROID_RUNTIME.md` 与 `.en.md`，覆盖入口差异、能力差异表、路径与权限三条硬约束、输出契约、错误码与安装升级；`SKILL.md` 原先把入口写死成 `.exe` 或 `python bem_tool.py`，改为按宿主分叉的三行表并写明第一步是 `doctor`；`bem_tool.py` 的源格式提示改为按 7-Zip 后端是否可用动态拼装，Windows 文案逐字未变。

- **产物版本对齐 1.6.3**：`BEM-Tools-1.6.3-win-x64.zip`、`BEM-Tools-1.6.3-android-arm64.tar.gz` 与 `dsh-bem-creator-1.6.3.tgz` 三件产物同版本发布。DSH 插件版本与 `bem_tool.py` 的 `TOOL_VERSION` 由构建脚本强校验，不一致会直接构建失败；本轮双方均为 1.6.3，版本号本身未变，只把发布记录补齐。

## 附件范围

候选包产出后填写。

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| BetterEndfieldNext-4.1.2-Setup.exe | 待定 | Windows x64 安装包 |
| BetterEndfieldNext-4.1.2-Android-arm64.apk | 待定 | Android arm64 安装包 |
| BEM-Tools-1.6.3-win-x64.zip | 待定 | 独立创作者 GUI、CLI 和全量角色资料 |
| BEM-Tools-1.6.3-android-arm64.tar.gz | 待定 | 安卓端 BEM CLI（源码形态，供 agent 在 Termux / root shell 直接调用；无 GUI，只吃目录与 ZIP） |
| dsh-bem-creator-1.6.3.tgz | 待定 | DeepSeek Harness 插件，安装创作者 skill；需与上面的 ZIP 一起使用 |

## 验证边界

- **桌面端构建与依赖检验**：CMake 编译目标 `BetterEndfieldNext.WeaponAppearance` 成功生成 `BetterEndfieldNext.WeaponAppearance.dll`；`BetterEndfieldNext.Layout` 成功包含该依赖；
- **配置与默认行为验证**：`WeaponAppearanceConfiguration` 实例与 ini 序列化确认 `enabled=false`；原生模块默认 `Configuration.enabled=false`；安卓端默认 `appearance_enabled=false`。
