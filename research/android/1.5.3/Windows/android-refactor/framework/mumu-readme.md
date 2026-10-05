# MuMu Android 15：Magisk + LSPosed 一键安装工具

> **版本：1.0.1**  
> **作者：Main**
>
> 面向 MuMu Android 15 实例的一键 Magisk、NeoZygisk 与 Vector（LSPosed）安装工具。

---

## 适用范围

本工具仅适用于：

```text
MuMu Android 15
```

不适用于：

- MuMu Android 12
- MuMu Android 9
- 其他安卓模拟器
- Android 15 以外的系统版本

安装器会自动读取设备的 Android 版本。如果检测结果不是 Android 15，脚本会立即停止，不会继续修改系统文件。

---

## 1.0.1 更新说明

- NeoZygisk 不再内置于工具包中，安装时下载官方最新稳定版。
- 下载的 NeoZygisk ZIP 会原样交给 Magisk 安装，不修改模块脚本或二进制。
- 支持复用已经校验的下载缓存。
- 支持通过 `packages\NeoZygisk.zip` 或 PowerShell 参数进行离线安装。
- 增加系统级 Magisk 兼容启动器，避免 MuMu 内置 Root 框架重复执行模块。
- 增加 KernelSU userspace 冲突处理，避免 NeoZygisk 报告：

  ```text
  Invalid root implementation: Multiple
  ```

- 保留模块 `sepolicy.rule` 动态加载逻辑。
- 增加 NeoZygisk monitor 数量、`TracerPid`、进程状态和模块运行状态检查。
- 修复 Windows PowerShell 5.1 中文编码及多层命令引号问题。

---

## 安装内容

安装器会完整安装或更新：

- Magisk 完整运行时
- Magisk 管理器
- NeoZygisk 官方最新稳定版
- Vector / LSPosed 官方最新稳定版
- MuMu 手工 Magisk 启动项
- 模块 SELinux 规则加载逻辑
- 系统级 Magisk 兼容启动器

默认情况下，Magisk、NeoZygisk 和 Vector 均从 GitHub 官方发布页获取最新稳定版本。

---

## 安装前准备

请启动需要安装的 MuMu Android 15 实例，并逐项完成以下设置。

### 1. 确认系统版本

当前实例必须为：

```text
Android 15
```

### 2. 创建实例快照

安装前必须为当前实例创建快照，以便设备无法启动时快速恢复。

### 3. 开启 Root 权限

进入：

```text
设备设置 → 开发者选项 → Root 权限
```

开启 Root 权限。

### 4. 开启系统盘读写

进入：

```text
设备设置 → 开发者选项 → 磁盘共享 → 可写系统盘
```

开启可写系统盘。

### 5. 开启网络桥接模式

进入：

```text
设备设置 → 网络 → 网络桥接模式
```

开启网络桥接模式。

### 6. 开启 ADB

确保：

- MuMu 已开启 ADB；
- 电脑与 MuMu 位于同一局域网；
- 电脑可以通过局域网连接 MuMu 的 ADB 服务。

> [!IMPORTANT]
> 如果刚刚修改了 Root 权限、可写系统盘或网络桥接模式，请先在 MuMu 窗口中点击一次“重启设备”，等待 Android 完整启动后再运行安装器。
>
> 不要只关闭模拟器窗口，也不要只重新连接 ADB。

---

## 运行方法

完整解压工具包后，双击：

```text
install.cmd
```

不要直接在压缩软件中运行。

脚本会先执行：

```bash
adb devices
```

随后列出当前 ADB 中记录的局域网设备，并要求选择 MuMu 设备地址。

支持以下输入：

| 输入方式 | 示例 |
|---|---|
| 列表编号 | `1` |
| IP 地址 | `192.168.1.42` |
| IP 和端口 | `192.168.1.42:5555` |

只输入 IP 时，默认使用：

```text
5555
```

最后一次选择的设备地址会保存到：

```text
config\last-device.txt
```

下次运行时：

- 直接按回车：使用上次地址；
- 修改输入内容：选择新的设备地址。

设备地址不通过 `install.cmd` 命令行参数传入。

---

## 安装过程

安装器会自动检查：

- Android 版本是否为 15；
- `adbd root` 是否真正生效；
- `/system` 是否可以实际写入；
- 设备 ABI 是否受支持；
- Magisk 核心运行时是否完整；
- NeoZygisk 模块包是否有效；
- Vector 模块包是否有效；
- 设备端 Shell 脚本是否通过语法检查；
- Magisk 与 MuMu 内置 Root 框架是否存在执行冲突。

完成安装文件写入后，脚本会要求在 MuMu 中点击：

```text
重启设备
```

点击重启后，返回命令窗口并输入：

```text
RESTART
```

脚本会通过以下状态确认设备已经完成真正的重启：

- `boot_id`
- `sys.boot_completed`

> [!WARNING]
> 关闭 MuMu 窗口、重新连接 ADB 或重新启动安装脚本，都不会被视为完整设备重启。

---

## 安装完成后的设置

设备重启完成后，安装器会自动打开 Magisk App。

### 请在 Magisk 中完成

1. 打开“模块”页面，开启 **NeoZygisk**。
2. 开启 **Vector**。
3. 本方案使用 NeoZygisk，请勿同时开启 Magisk 设置页面中的内置 Zygisk。
4. 保持 `Enforce DenyList` 关闭。

### 请在 MuMu 中完成

1. 返回当前设备窗口。
2. 确认 NeoZygisk 和 Vector 均处于启用状态。
3. 关闭 MuMu 自带 Root 权限：

   ```text
   设备设置 → 开发者选项 → Root 权限
   ```

4. 在 MuMu 窗口中点击“重启设备”，不要从 Magisk 中重启。
5. 重启完成后打开 NeoZygisk 检查状态。

> [!CAUTION]
> NeoZygisk 与 Magisk 内置 Zygisk 不应同时启用，否则可能导致模块冲突、注入异常或设备启动问题。

---

## 最终正常状态

NeoZygisk 页面应显示：

```text
monitor: tracing
zygote64: injected
daemon64: running
```

同时应满足：

- 系统中只有一个 `zygisk-ptrace64 monitor`；
- `/proc/1/status` 中的 `TracerPid` 指向该 monitor；
- `zygiskd64` 正常运行；
- `lspd` 正常运行；
- 不再出现：

  ```text
  Invalid root implementation: Multiple
  ```

---

## 在线下载与缓存

默认情况下，安装器会查询并下载：

- Magisk 最新稳定版
- NeoZygisk 最新稳定版
- Vector 最新稳定版

下载文件保存在：

```text
work\downloads
```

安装失败后重新运行时，可以复用已经完成校验的缓存。

如果 GitHub API 返回资产 SHA-256 digest，安装器会进行完整哈希校验；否则会至少检查下载文件大小和模块结构。

实际使用的模块 ZIP SHA-256 会写入主机端安装日志。

---

## 离线安装

本工具包不内置 NeoZygisk。

### 方式一：使用 packages 目录

将官方 NeoZygisk ZIP 命名为：

```text
NeoZygisk.zip
```

放入：

```text
packages
```

安装器会优先使用该文件。

### 方式二：通过 PowerShell 参数指定

```powershell
powershell -ExecutionPolicy Bypass -File .\Install-Mumu-Magisk-Vector.ps1 `
  -MagiskApk .\Magisk.apk `
  -NeoZygiskZip .\NeoZygisk.zip `
  -VectorZip .\Vector.zip
```

参数说明：

| 参数 | 用途 |
|---|---|
| `-MagiskApk` | 指定本地 Magisk APK |
| `-NeoZygiskZip` | 指定本地 NeoZygisk 模块 ZIP |
| `-VectorZip` | 指定本地 Vector 模块 ZIP |

本地 NeoZygisk ZIP 必须是标准模块包，根目录应包含：

```text
module.prop
customize.sh
```

模块 ID 应为：

```text
zygisksu
```

安装器只会校验和安装该 ZIP，不会修改其内容。

---

## 覆盖安装

已经安装旧版本时，可以直接运行本版：

```text
install.cmd
```

安装器会完整更新 Magisk、NeoZygisk、Vector 和启动环境。

旧版中曾经内置或修改过的 NeoZygisk，会被在线下载的官方模块完整替换。

---

## 常用验证命令

首先查看保存的设备地址：

```text
config\last-device.txt
```

假设设备地址为：

```text
192.168.1.42:5555
```

### 检查 Magisk 版本

```bash
adb -s 192.168.1.42:5555 shell "/debug_ramdisk/magisk -v"
```

### 检查 Magisk Root

```bash
adb -s 192.168.1.42:5555 shell "/debug_ramdisk/su -c id"
```

正常输出应包含：

```text
uid=0(root)
```

### 检查模块状态

```bash
adb -s 192.168.1.42:5555 shell "/debug_ramdisk/su -c 'find /data/adb/modules -name unloaded -print'"
```

没有输出通常表示不存在处于 `unloaded` 状态的模块。

### 检查 NeoZygisk monitor

```bash
adb -s 192.168.1.42:5555 shell "ps -A | grep zygisk-ptrace"
```

正常情况下只应存在一个 monitor。

### 检查 init 的 TracerPid

```bash
adb -s 192.168.1.42:5555 shell "grep TracerPid /proc/1/status"
```

### 检查 LSPosed 数据目录

```bash
adb -s 192.168.1.42:5555 shell "/debug_ramdisk/su -c 'ls -la /data/adb/lspd 2>/dev/null'"
```

---

## 日志位置

### 主机端日志

```text
logs\install-日期时间.log
```

例如：

```text
logs\install-20260801-231500.log
```

### 设备端日志

```text
/data/local/tmp/mumu-magisk-oneclick.log
```

```text
/data/local/tmp/magisk_init.log
```

安装失败、模块异常或设备重启异常时，请优先检查这些日志。

---

## 卸载方法

双击：

```text
uninstall.cmd
```

卸载器会：

1. 读取上次保存的 MuMu 设备地址；
2. 允许在卸载前修改设备地址；
3. 移除 Magisk 启动项；
4. 移除 NeoZygisk 和 Vector；
5. 移除 Magisk 运行时；
6. 移除本工具安装的系统级 Magisk 兼容启动器；
7. 恢复安装前备份的同名系统文件和 Root userspace 文件。

是否卸载 Magisk 管理器 APK，可以通过卸载脚本参数选择。

卸载完成后，请在 MuMu 窗口中手动点击：

```text
重启设备
```

---

## 风险说明

> [!WARNING]
> 本工具属于针对 MuMu Android 15 的非官方 live/manual Magisk 注入方案。
>
> 修改系统文件可能导致：
>
> - 实例无法正常启动；
> - Magisk 或模块加载失败；
> - 模拟器更新后功能失效；
> - 系统文件与 MuMu 原始状态不一致；
> - 需要通过快照恢复实例。

首次安装前必须创建并保留可用快照。

默认下载的是安装时 GitHub 标记的最新稳定版本，上游模块更新可能改变兼容性。需要固定版本时，请使用本地文件参数指定已经自行校验的安装包。

使用本工具即表示使用者已经了解相关风险，并自行承担因系统修改产生的后果。