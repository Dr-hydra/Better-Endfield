# Android 宿主上的 BEM Tools

本页说明 agent 在 Android 设备（Termux、root shell 或 adb shell）上运行 BEM Tools 时，与 Windows 桌面端的差异。宿主不是 Android 时不必读本页。

发行包名为 `BEM-Tools-<version>-android-arm64.tar.gz`，与其他产物一样从 release 页面下载。它是源码形态，不是 APK，也不含 GUI。

## 1. 先确认自己在哪

```sh
/data/local/tmp/bem-cli/bem-tools doctor
```

`host.is_android` 为 `true` 时按本页执行。命令不存在说明当前是仓库检出或 Windows 发行版，按 [SKILL.md](../SKILL.md) 的默认入口走。

`doctor` 一次给出四类事实，比逐个试探便宜：

- `python`：解释器版本与路径，低于 3.10 直接不可用；`python.ext_suffix` 与 `python.platform_tag` 决定预编译扩展能否加载
- `deps.zstd_backend`：zstd 后端来源，`cext`（`zstandard` 扩展）或 `stdlib`（Python 3.14+ 的 `compression.zstd`）
- `capabilities.source_formats`：这台设备实际能读的源格式
- `paths.tmpdir`：临时目录，决定能否在 `convert` 里落中间产物

`success` 为 `false` 时先修 `issues`，不要带着缺依赖的环境去转换——那只会得到一条误导性的格式错误。

## 2. 命令入口

| 宿主 | 入口 |
|---|---|
| Windows 发行版 | `BEM-Tools\BetterEndfieldNext.BemConverter.exe` |
| 仓库检出 | `python tools/CustomModel/bem_tool.py` |
| Android | `/data/local/tmp/bem-cli/bem-tools`（默认装法） |

Android 上不存在 `.exe`，也不要自己拼 `python bem_tool.py`：`bem-tools` 会先设好 `TMPDIR`、`PYTHONPATH`、UTF-8 输出，再在系统 `python3` 与随包运行时之间择一。绕过它调用会把临时文件写到当前目录，在 `/sdcard` 上直接踩 FUSE。

命令集与 `bem_tool.py` 完全一致，另外多两个：

```
inspect  convert  validate  pack  pack-zip  unpack  bundle
new-project  build  init-workspace  target-profile  doctor  install-wheel  repair
```

## 3. 能力差异

| 能力 | Android | 说明 |
|---|---|---|
| 源：目录、ZIP | 支持 | ZIP 走标准库 `zipfile`，与桌面端同一路径 |
| 源：RAR、7z | **不支持** | 发行包不带 7-Zip 后端，拒绝码 `ARCHIVE_BACKEND` |
| GUI | 无 | 只有 CLI |
| 原生资产 dump | 无 | 依赖 .NET 与 Windows 工具链，手机上做不了 |
| `bem_repair.py` 等修复脚本 | **可用** | 见下 |
| `bem_assemble.py` | **可用** | 见下 |

最后两行与 Windows 发行版相反，值得记住。Windows 的 PyInstaller 产物只留 `catalog/` 与 `profiles/`，不带 `.py`，所以 `bem_repair.py` 的 `_tools_root()` 找不到 `component_auto.py`，会以 `ENTRY_TEMPLATE_UNAVAILABLE` 拒绝；Android 包是源码形态，`app/skills/bem-creator/scripts/` 向上三层就是 `app/`，这个检查反而能通过。

调用方式：

```sh
/data/local/tmp/bem-cli/bem-tools repair ib-audit <project> --draw N
python3 /data/local/tmp/bem-cli/app/skills/bem-creator/scripts/bem_repair.py --help
```

前者是 `bem-tools` 提供的转发，等价于后者。

## 4. 路径与权限

三条硬约束，顺序不能颠倒：

1. `/sdcard`（含 `Download`）可读写，但挂载为 `noexec`——脚本和可执行文件放这里跑不起来。
2. Android 10 起（`targetSdk ≥ 29`）应用私有目录被 SELinux 禁止 `execve`，即便 `chmod 755` 也没用。
3. `/data/local/tmp` 同时可写可执行，是唯一没有附加条件的落点，所以 `install.sh` 默认装这里。

给 agent 的操作约定：

- 读源：`/sdcard/Download/...` 直接读，没问题。
- 写产物：优先 `/data/local/tmp/...`，完成后由用户自行拷走；直接写 `/sdcard` 也能成功，因为代码用同目录 `mkstemp` 落临时文件，不会触发跨文件系统的 `EXDEV`。
- 临时目录：`TMPDIR` 由入口脚本指向 `/data/local/tmp`（其次 `<安装目录>/tmp`、`$HOME`）。需要覆盖时设 `BEM_TOOLS_TMP`。
- 设备没有 `/tmp`。`tempfile` 找不到 `TMPDIR` 时会退到当前目录，这是移植期最常见的坑。

## 5. 输出契约

与桌面端一致，调用方按同一套规则解析：

- 每条命令都向 stdout 打印**一份** JSON 文档，`ensure_ascii=false`、两空格缩进。
- 成功 `exit 0`、`"success": true`；失败 `exit 2`、`"success": false`，原因在 `issues[].code` 与 `issues[].message`。
- 需要留档时加 `--report <path>`，JSON 会另存一份；该路径不允许覆盖输入或输出。
- stderr 只在解释器层出错（语法、缺模块）时出现，正常失败不写 stderr。

在手机上跑长任务时不要用管道截断 stdout 早期内容——`convert` 只在结束时一次性输出，过程中没有进度行。

## 6. 平台声明不会被宿主改写

`convert` 产出的包带 `target.platform`，由 profile 与源决定，与运行宿主无关。当前 `catalog/` 只提供 `windows-x64` 角色资料，所以在 Android 上转换得到的仍是 Windows 目标包。

`inspect` / `validate` 上的 `--platform android-arm64` 只影响 `selection_plan` 的展开方式，不改包内容。不要因为宿主是手机就期待"自动产出 Android 包"——那需要 `android-arm64` 的 profile 与接收器证据，与运行环境不是一回事。

## 7. 常见错误码

| 错误码 | 含义 | 处理 |
|---|---|---|
| `PYTHON_TOO_OLD` | 解释器低于 3.10 | `pkg install python`，或 `BEM_PYTHON=` 指定 |
| `ZSTANDARD_MISSING` | 两种 zstd 后端都不可用 | `bem-tools install-wheel`；或升到 Python 3.14+ |
| `WHEEL_UNSUPPORTED_PYTHON` | 随包 wheel 的 `cp3XX` 与本机解释器不符 | 升 Python，或用 `build_zstandard_wheel.py` 按本机版本重编 |
| `IMPORT_FAILED` | wheel 已落盘但扩展加载失败 | 看 `EXT_SUFFIX` 是否与本机一致，多半是 ABI 不匹配 |
| `TMPDIR_NOT_WRITABLE` | 临时目录不可写 | `export TMPDIR=/data/local/tmp` |
| `ARCHIVE_BACKEND` | 源是 RAR/7z | 解压成目录，或重打包为 zip |
| `ARCHIVE_FORMAT` | 既不是 zip 也不是目录 | 查扩展名与文件头 |

`Permission denied` 出现在执行阶段而不是转换阶段时，先确认脚本不在 `/sdcard` 上，再确认 `chmod 755` 生效。

## 8. 安装与升级

```sh
tar -xzf BEM-Tools-<version>-android-arm64.tar.gz
sh BEM-Tools-<version>-android-arm64/install.sh          # -> /data/local/tmp/bem-cli
sh BEM-Tools-<version>-android-arm64/install.sh <目标目录>  # 自定义位置
```

`install.sh` 会校验 Python 版本、确认 zstd 后端、复制文件、最后跑一次 `doctor`。升级时重复执行即可，它整目录覆盖 `app/`；`catalog/` 与 `profiles/` 会保持新版本内容，不需要手动清理。

## 9. zstd 后端

转换要读写 zstd 帧：payload 用 zstd 压缩，读之前还要核对帧头声明的解压尺寸。这里有两条后端，`doctor` 的 `deps.zstd_backend` 告诉你在哪条上：

| 值 | 来源 | 设备上的代价 |
|---|---|---|
| `stdlib` | Python 3.14+ 的 `compression.zstd` | 零安装。当前 Termux `pkg install python` 装的 3.14 就自带 |
| `cext` | `zstandard` 扩展（随包 wheel 或 pip） | 装一次 |

两条后端产出同样的字节：两边都是 zstd 1.5.7，同一份数据在同一级别下压缩结果逐字节相同，手机上的 `convert` 产物与 Windows 上的 digest 一致。源码里仍然写 `import zstandard`，`app/zstd_compat.py` 在扩展缺失时把标准库实现挂上去，agent 不需要分支。

需要补装时用随包 wheel：

```sh
/data/local/tmp/bem-cli/bem-tools install-wheel        # 默认找 <安装目录>/wheels
```

它**不走 pip**。Termux 的 Python 报 `sys.platform == 'android'`，pip 认的是 `android_<api>_arm64_v8a` 标签；Linux 形态的嵌入式解释器（如 Chaquopy）认 `linux_aarch64`。一个文件名只能带一个平台标签，所以安装器把 wheel 里的 `zstandard/` 直接拷进 `site-packages`，并按本机 `EXT_SUFFIX` 重命名扩展文件——两种宿主都适用，也不需要编译器。失败时返回 `WHEEL_UNSUPPORTED_PYTHON`（解释器版本不符）或 `IMPORT_FAILED`（ABI 不符），两者都写明下一步。
