# BEM Tools on an Android Host

This page covers running BEM Tools on an Android device (Termux, a root shell, or an adb shell) and how it differs from the Windows desktop build. Skip it when the host is not Android.

The distribution is `BEM-Tools-<version>-android-arm64.tar.gz`, published with the other assets on the release page. It is a source tree, not an APK, and it has no GUI.

## 1. Establish where you are

```sh
/data/local/tmp/bem-cli/bem-tools doctor
```

Proceed with this page when `host.is_android` is `true`. A missing command means a repository checkout or the Windows distribution — follow the default entry points in [SKILL.md](../SKILL.md).

`doctor` answers four questions at once instead of probing for them one by one:

- `python` — interpreter version and path; below 3.10 the tool cannot run, and `python.ext_suffix` / `python.platform_tag` decide whether a prebuilt extension loads
- `deps.zstd_backend` — where zstd comes from: `cext` for the `zstandard` extension, `stdlib` for `compression.zstd` (Python 3.14+)
- `capabilities.source_formats` — the source formats this device actually reads
- `paths.tmpdir` — the scratch directory `convert` will use

When `success` is `false`, fix `issues` first. Converting with a missing dependency only produces a misleading format error.

## 2. Command entry points

| Host | Entry point |
|---|---|
| Windows distribution | `BEM-Tools\BetterEndfieldNext.BemConverter.exe` |
| Repository checkout | `python tools/CustomModel/bem_tool.py` |
| Android | `/data/local/tmp/bem-cli/bem-tools` (default install) |

There is no `.exe` on Android, and do not assemble `python bem_tool.py` by hand: `bem-tools` sets `TMPDIR`, `PYTHONPATH` and UTF-8 output, then picks either the system `python3` or a bundled runtime. Calling the module directly writes temporary files into the current directory, which fails immediately on `/sdcard`.

The command set matches `bem_tool.py`, plus three:

```
inspect  convert  validate  pack  pack-zip  unpack  bundle
new-project  build  init-workspace  target-profile  doctor  install-wheel  repair
```

## 3. Capability differences

| Capability | Android | Notes |
|---|---|---|
| Sources: directory, ZIP | Supported | ZIP goes through `zipfile`, the same path as desktop |
| Sources: RAR, 7z | **Not supported** | No 7-Zip backend ships; refuses with `ARCHIVE_BACKEND` |
| GUI | None | CLI only |
| Native asset dump | None | Needs .NET and Windows tooling |
| `bem_repair.py` and friends | **Available** | See below |
| `bem_assemble.py` | **Available** | See below |

The last two rows invert the Windows distribution, and that is worth remembering. The PyInstaller build ships only `catalog/` and `profiles/` with no `.py` files, so `bem_repair.py` cannot find `component_auto.py` and refuses with `ENTRY_TEMPLATE_UNAVAILABLE`. The Android package is a source tree: three levels up from `app/skills/bem-creator/scripts/` is `app/`, so the same check passes.

```sh
/data/local/tmp/bem-cli/bem-tools repair ib-audit <project> --draw N
python3 /data/local/tmp/bem-cli/app/skills/bem-creator/scripts/bem_repair.py --help
```

The first form is a forward provided by `bem-tools`; it is equivalent to the second.

## 4. Paths and permissions

Three hard constraints, in order:

1. `/sdcard` (including `Download`) is readable and writable but mounted `noexec` — scripts and binaries will not run from there.
2. Since Android 10 (`targetSdk ≥ 29`) SELinux denies `execve` on an app's private directory, whatever the permission bits say.
3. `/data/local/tmp` is both writable and executable, the only location without a caveat, which is why `install.sh` defaults there.

Working conventions:

- Reading sources: `/sdcard/Download/...` is fine.
- Writing output: prefer `/data/local/tmp/...` and let the user move it. Writing to `/sdcard` also works, because the code stages temporary files with `mkstemp` in the destination directory and never crosses a filesystem boundary.
- Scratch space: the entry script points `TMPDIR` at `/data/local/tmp`, then `<install>/tmp`, then `$HOME`. Override with `BEM_TOOLS_TMP`.
- There is no `/tmp` on Android. Without `TMPDIR`, `tempfile` falls back to the current directory, which is the most common porting failure.

## 5. Output contract

Identical to desktop, so callers parse both the same way:

- Every command prints exactly one JSON document to stdout, `ensure_ascii=false`, two-space indent.
- Success is `exit 0` with `"success": true`; failure is `exit 2` with `"success": false` and the reason in `issues[].code` / `issues[].message`.
- `--report <path>` writes a second copy; the path may not overwrite an input or the output.
- stderr only carries interpreter-level failures. Normal failures never write stderr.

Do not truncate stdout on long runs: `convert` emits once, at the end, with no progress lines.

## 6. The host does not rewrite the platform declaration

A converted package carries `target.platform`, decided by the profile and the source, not by where the converter ran. The bundled `catalog/` only covers `windows-x64` character data, so a package converted on Android is still a Windows target.

`--platform android-arm64` on `inspect` / `validate` only changes how `selection_plan` is expanded; it does not change the package. Do not expect an Android target merely because the host is a phone — that needs `android-arm64` profiles and receiver evidence, which is a different question from the runtime host.

## 7. Error codes

| Code | Meaning | Action |
|---|---|---|
| `PYTHON_TOO_OLD` | Interpreter below 3.10 | `pkg install python`, or set `BEM_PYTHON` |
| `ZSTANDARD_MISSING` | Neither zstd backend is available | `bem-tools install-wheel`, or move to Python 3.14+ |
| `WHEEL_UNSUPPORTED_PYTHON` | The bundled wheel's `cp3XX` does not match this interpreter | Upgrade Python, or rebuild with `build_zstandard_wheel.py` |
| `IMPORT_FAILED` | The wheel landed but the extension will not load | Compare `EXT_SUFFIX`; this is an ABI mismatch |
| `TMPDIR_NOT_WRITABLE` | Scratch directory not writable | `export TMPDIR=/data/local/tmp` |
| `ARCHIVE_BACKEND` | Source is RAR/7z | Unpack to a directory, or repack as zip |
| `ARCHIVE_FORMAT` | Neither zip nor directory | Check the extension and file header |

When `Permission denied` appears at execution rather than conversion time, confirm the script is not on `/sdcard`, then confirm `chmod 755` took effect.

## 8. Install and upgrade

```sh
tar -xzf BEM-Tools-<version>-android-arm64.tar.gz
sh BEM-Tools-<version>-android-arm64/install.sh           # -> /data/local/tmp/bem-cli
sh BEM-Tools-<version>-android-arm64/install.sh <dir>     # custom location
```

`install.sh` checks the Python version, settles the zstd backend, copies the tree, and finishes with `doctor`. Re-running it upgrades in place: `app/` is replaced wholesale, and `catalog/` / `profiles/` come from the new package without manual cleanup.

## 9. The zstd backend

Conversion reads and writes zstd frames: payloads are zstd-compressed, and the reader checks the declared content size before decompressing. Two backends cover that, and `deps.zstd_backend` in `doctor` says which one is live:

| Value | Source | Cost on device |
|---|---|---|
| `stdlib` | `compression.zstd`, Python 3.14+ | Nothing to install; the Termux `python` package is 3.14 and ships it |
| `cext` | the `zstandard` extension, bundled wheel or pip | One install |

Both backends emit identical bytes: zstd 1.5.7 on either side, same level, so a package converted on a phone has the same digests as one converted on Windows. The source still says `import zstandard`; `app/zstd_compat.py` attaches the stdlib implementation when the extension is missing, so agents never branch on it.

To install the extension, use the bundled wheel:

```sh
/data/local/tmp/bem-cli/bem-tools install-wheel        # defaults to <install>/wheels
```

It deliberately **does not use pip**. Termux's Python reports `sys.platform == 'android'`, so pip expects an `android_<api>_arm64_v8a` tag, while Linux-flavoured embedded interpreters such as Chaquopy expect `linux_aarch64`. A wheel filename carries one platform tag, so the installer copies `zstandard/` out of the wheel into `site-packages` and renames the extension to the local `EXT_SUFFIX` instead. That works on both hosts and needs no compiler. Failures come back as `WHEEL_UNSUPPORTED_PYTHON` (interpreter mismatch) or `IMPORT_FAILED` (ABI mismatch), each with the next step.
