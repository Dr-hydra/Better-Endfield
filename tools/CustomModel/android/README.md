# BEM Tools — Android CLI

在 Android 上运行的 BEM 创作者命令行。同一套转换逻辑，去掉 GUI、去掉 7-Zip 后端，
只保留 `zip` 与已解包目录两种源格式。

适合的场景：手机上的 agent 需要自己完成 `inspect` / `convert` / `pack` / `validate`，
不必把源包传回 Windows 主机。

## 环境要求

| 项 | 要求 | 说明 |
|---|---|---|
| 架构 | arm64（aarch64） | 也支持 x86_64 设备，只要解释器可用 |
| Python | 3.10 及以上 | Termux：`pkg install python`（当前为 3.14） |
| zstd 后端 | 二选一 | Python 3.14+ 自带 `compression.zstd`，否则装随包 wheel |
| 存放位置 | 可执行目录 | `/data/local/tmp/` 或 Termux 的 `$PREFIX`，**不要放 /sdcard** |

`/sdcard` 挂载为 `noexec`，脚本和二进制在那里无法执行；Android 10 起应用私有目录
（`targetSdk ≥ 29`）也被 SELinux 禁止 `execve`。`/data/local/tmp` 同时可写可执行，
所以是默认安装位置。

## 安装

```sh
# 1. 解压到任意可写目录
tar -xzf BEM-Tools-<version>-android-arm64.tar.gz
cd BEM-Tools-<version>-android-arm64

# 2. 部署并自检
sh install.sh

# 3. 自检通过后即可使用
/data/local/tmp/bem-cli/bem-tools doctor
```

`install.sh` 会检查 Python 版本、确认 zstd 后端、把文件复制到 `/data/local/tmp/bem-cli`，
最后跑一次 `doctor`。目标目录可以自己指定：`sh install.sh /data/local/tmp/bem-cli`。

### zstd 后端

转换要读写 zstd 帧，后端有两条，`doctor` 的 `deps.zstd_backend` 会说在哪条上：

| 值 | 来源 | 代价 |
|---|---|---|
| `stdlib` | Python 3.14+ 的 `compression.zstd` | 零安装；Termux 现装即此 |
| `cext` | `zstandard` 扩展 | 装一次（随包 wheel） |

两条后端压缩同一份数据的结果逐字节相同（都是 zstd 1.5.7），所以手机上产出的包与
Windows 产出的 digest 一致。源码里仍写 `import zstandard`，`app/zstd_compat.py`
在扩展缺失时接管，调用方不用分支。

补装扩展：

```sh
bem-tools install-wheel                 # 用 <安装目录>/wheels 里的 wheel
bem-tools install-wheel path/to.whl     # 指定文件
bem-tools install-wheel --target DIR    # 指定落点（默认 site-packages）
```

它**不走 pip**：Termux 的 Python 报 `sys.platform == 'android'`，pip 要
`android_<api>_arm64_v8a` 标签；Linux 形态的嵌入式解释器（Chaquopy 等）要
`linux_aarch64`。一个 wheel 文件名只能带一个平台标签，所以安装器把 wheel 里的
`zstandard/` 直接拷进 `site-packages`，并按本机 `EXT_SUFFIX` 重命名扩展文件——
两种宿主都适用，也不需要编译器。

兜底仍是 pip（会编译，需 `pkg install clang`）：

```sh
python3 -m pip install --no-index --find-links wheels zstandard
python3 -m pip install zstandard
```

### 重新构建 wheel

随包 wheel 是 `zstandard-0.25.0-cp314-cp314-android_24_arm64_v8a.whl`，用 NDK 交叉编译：

```sh
python tools/CustomModel/android/build_zstandard_wheel.py \
    --sdist zstandard-0.25.0.tar.gz \
    --python-include /path/to/include/python3.14 \   # Termux: $PREFIX/include/python3.14
    --ndk /path/to/android-ndk \                     # 需要 llvm-strip
    --output tools/CustomModel/android/wheels
```

脚本先校验产物是 ELF64/AArch64、导出 `PyInit_backend_c`、`NEEDED` 只有
`libc.so`/`libm.so`/`libdl.so`，再打包成 wheel。`--ext-suffix` 与 `--platform-tag`
可覆盖，用于别的 Android 解释器。Termux 升级 Python 后要重跑一次。

## 用法

```sh
bem-tools <command> [args...]
```

| 命令 | 作用 |
|---|---|
| `inspect <src>` | 识别源格式、匹配的角色 profile、可转换性 |
| `convert <src> -o <out.bem>` | 源 Mod → BEM（`--recipe` 可指定配方） |
| `validate <out.bem>` | 校验已生成的包 |
| `pack` / `unpack` / `pack-zip` / `bundle` | 工程与包格式互转、多包合并 |
| `new-project` / `build` / `init-workspace` | 导出工程工作流 |
| `target-profile` | 按 spec 生成资源目标 |
| `doctor` | 环境自检（本发行版新增） |
| `install-wheel` | 把随包 zstandard wheel 装进 site-packages（本发行版新增） |
| `repair` | 转发 `bem_repair.py` 子命令（本发行版新增） |

每个命令都会向 stdout 打印**一份 JSON 文档**：

- 成功：`exit 0`，`"success": true`
- 失败：`exit 2`，`"success": false`，原因在 `issues[].code` / `issues[].message`

调用方不需要解析 stderr。要落盘审计记录时加 `--report <path>`。

`doctor` 的输出用于首检：

```json
{
  "tool": "bem-tools",
  "tool_version": "1.6.3",
  "success": true,
  "host": { "is_android": true, "abi": "arm64-v8a", "sdk": "34", "root": false },
  "python": { "version": "3.14.8", "platform_tag": "android-34-arm64_v8a",
              "ext_suffix": ".cpython-314-aarch64-linux-android.so" },
  "deps": { "zstandard": null, "zstd_backend": "stdlib" },
  "capabilities": { "source_formats": ["zip", "directory"], "archive_backend": false }
}
```

## 与 Windows 版的差异

| 能力 | Windows (`win-x64`) | Android (`arm64`) |
|---|---|---|
| GUI（BemTools 窗口） | 有 | 无，仅 CLI |
| 源格式 | 目录 / ZIP / RAR / 7z | **目录 / ZIP** |
| `bem_assemble.py` | 仓库内可用 | 不可用（需仓库源码树） |
| 原生资产 dump | 可用（需 .NET + Windows 工具） | 不可用 |
| 输出契约 | 单 JSON + exit 0/2 | 相同 |

源包是 `.rar` / `.7z` 时，会返回 `ARCHIVE_BACKEND`，提示先解压成目录或重新打包为 zip。
这与 Windows 版缺少 7-Zip 组件时的行为一致。

## 故障排查

| 错误码 | 含义 | 处理 |
|---|---|---|
| `PYTHON_TOO_OLD` | 解释器低于 3.10 | `pkg install python`，或 `BEM_PYTHON=/path/to/python3` |
| `ZSTANDARD_MISSING` | 两种 zstd 后端都不可用 | `bem-tools install-wheel`，或升到 3.14+ |
| `WHEEL_UNSUPPORTED_PYTHON` | 随包 wheel 与解释器版本不符 | 升 Python，或按本机版本重编 wheel |
| `IMPORT_FAILED` | wheel 已落盘但扩展加载失败 | 比对 `EXT_SUFFIX`，是 ABI 不匹配 |
| `TMPDIR_NOT_WRITABLE` | 临时目录不可写 | `export TMPDIR=/data/local/tmp` |
| `ARCHIVE_BACKEND` | 源是 rar/7z | 解压成目录，或重打包为 zip |
| `ARCHIVE_FORMAT` | 文件既不是 zip 也不是目录 | 检查扩展名与文件头 |

`Permission denied` 出现在执行阶段时，先确认脚本不在 `/sdcard` 上，并且
`chmod 755 bem-tools` 已生效。

## 目录结构

```
bem-cli/
├── bem-tools        # POSIX sh 入口（mksh 兼容）
├── install.sh       # 部署脚本
├── app/
│   ├── bem_tool.py  # CLI 主体
│   ├── bem_android.py  # doctor / install-wheel
│   ├── zstd_compat.py  # 扩展缺失时接到 compression.zstd
│   ├── catalog/     # 角色与资源目录
│   ├── profiles/    # 目标 profile
│   ├── skills/      # 创作者技能文档（agent 可直接读）
│   └── examples/    # 可运行的示例工程
├── wheels/          # 预编译的 zstandard wheel
└── tmp/             # 兜底临时目录
```

## 环境变量

| 变量 | 默认 | 作用 |
|---|---|---|
| `BEM_PYTHON` | 自动探测 `python3`/`python` | 指定解释器 |
| `BEM_TOOLS_TMP` | `/data/local/tmp` → `<root>/tmp` → `$HOME` | 临时目录 |
| `TMPDIR` | 同上 | 标准临时目录变量 |
