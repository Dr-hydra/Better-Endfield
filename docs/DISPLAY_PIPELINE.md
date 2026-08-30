# Better Endfield Display Pipeline

本文记录显示增强功能的集成方案。该功能不引入进程内模块，也不改动 Host、HookBroker 或模块 ABI；全部实现位于 UI 侧的部署与配置层。

## 范围

- 通过 OptiScaler 为客户端提供 FSR4 超分输出，输入侧复用客户端已有的 DLSS 集成。
- 由软件负责组件获取、文件部署、`OptiScaler.ini` 生成、冲突检测与安全卸载。
- 不分发 NVIDIA 或 AMD 的运行时二进制，不修改客户端自带文件。

非目标：帧生成（客户端已有原生 DLSS-FG，OptiFG 不适用）、HDR 与色调映射改造（属 ReShade/RenoDX 范畴，本文不覆盖）、DLSS 5 神经渲染（无公开 SDK，且依赖未发布的 NVIDIA 运行时）。

## 客户端环境实测

以下结论由 PE 导入表与导出表解析得到，记录于 2026-08-29，客户端目录 `Endfield.exe`。重新验证时应重跑解析而不是沿用本节结论。

### 静态导入链

`Endfield.exe` 的导入表只有 `EndfieldBase.dll`。`EndfieldBase.dll` 的导入表只有 `kernel32.dll`，10 个节，导入目录位于 RVA `0x22a0010`（约 36 MB 处），是加壳保护层，其余依赖在运行时解析。整条 EXE 静态链不带出任何可代理的槽位。

真正的可代理槽位来自 `UnityPlayer.dll` 的静态导入表：

```
KERNEL32  USER32  VERSION  ole32   SHLWAPI  SETUPAPI  ADVAPI32
GDI32     SHELL32 WINMM    OLEAUT32 IMM32   IPHLPAPI  WINHTTP
bcrypt    HID     CRYPT32  WS2_32  dwmapi   dxgi
```

`dxgi.dll` 在此表中。Windows 加载器按应用目录优先解析，因此客户端目录下的 `dxgi.dll` 会在 `UnityPlayer.dll` 被加载的同一时刻进入进程，早于任何图形设备与交换链创建。这是 OptiScaler 需要的时机，且无需任何外部协调。

`xinput` 仅以字符串形式出现在 `UnityPlayer.dll` 与 `SDL2.dll` 中，不在任何静态导入表或延迟导入表内，属运行时 `LoadLibrary` 加载。现有 `payloads/xinput1_4.dll` 代理因此在输入子系统初始化时才进入进程，晚于图形设备创建，不能用于承载图形层组件。

### 渲染与超分栈

客户端目录包含完整 NVIDIA Streamline 栈：

```
sl.interposer.dll  sl.common.dll  sl.dlss.dll     sl.dlss_g.dll
sl.dlss_d.dll      sl.reflex.dll  sl.pcl.dll      sl.deepdvc.dll
nvngx_dlss.dll     nvngx_dlssd.dll  nvngx_dlssg.dll  nvngx_deepdvc.dll
```

DLSS 调用路径为 `sl.interposer` → `sl.dlss` → `nvngx_dlss`，不是裸 NGX 调用。另有 `NvLowLatencyVk.dll`，对应 Vulkan 后端的低延迟支持。

客户端目录**不含**任何 `*fidelityfx*`、`ffx_*` 或 `*amd_*` 文件。客户端的 FSR3 为引擎内置实现，没有可供拦截的 FidelityFX API DLL。因此 OptiScaler 无法从 FSR3 侧取得输入，只能拦截 DLSS 输入；在非 NVIDIA 硬件上这要求启用 GPU 欺骗，使客户端暴露 DLSS 选项。

### 反作弊

`AntiCheatExpert/` 下存在内核驱动 `ACE-BASE.sys`、`ACE-CORE.sys`（含 `.sys2`/`.sysa`/`.sysa2` 变体）与用户态组件 `ACE-Base64.dll`、`ACE-CSI64.dll`、`ACE-Service64.exe`。向客户端目录部署文件是持久化改动，可见性高于运行时注入。部署路径必须由用户显式确认，与 XInput 代理的处理方式一致。

## 加载拓扑

```
客户端进程
├── dxgi.dll         OptiScaler          UnityPlayer.dll 静态导入时加载
│                                        → 拦截 Streamline/NGX，输出 FSR4
└── xinput1_4.dll    Better Endfield 代理 输入子系统初始化时加载（可选部署）
    └── 轮询 IL2CPP domain → runtime/BetterEndfield.Host.dll → modules/
```

两条链占用不同槽位、处于不同加载阶段，不共享 hook 引擎，不需要相互转发或排序。Host 的 IL2CPP 附着模型与 HookBroker 的验证规则不受影响。

## 代理槽位筛选

本节记录筛选过程，供后续需要确定性早期槽位时复用，当前方案不使用其结论。

从 `UnityPlayer.dll` 的 20 个静态导入出发，逐层排除：

| 排除依据 | 槽位 |
| --- | --- |
| KnownDLLs（`HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDLLs`，强制从 System32 解析，应用目录代理无效） | IMM32, SHLWAPI, SETUPAPI, SHELL32, ole32, OLEAUT32, ADVAPI32, GDI32, USER32, WS2_32, KERNEL32 |
| ACE 组件静态依赖 | VERSION, WINMM, IPHLPAPI, WINHTTP, CRYPT32, bcrypt |
| 本方案预留 | dxgi |

`ACE-Base64.dll`（61.8 MB，进程内加载）同时导入 `VERSION.dll` 与 `WINMM.dll`；`ACE-CSI64.dll`、`ACE-Service64.exe`、`ACE-Setup64.exe` 均导入 `VERSION.dll`。这两个常用代理名会把代理挂进 ACE 的依赖链，不可使用。

剩余可用槽位为 `HID.DLL` 与 `dwmapi.dll`：

| | HID.DLL | dwmapi.dll |
| --- | --- | --- |
| 导出 | 47 个，全部具名，序号 1–47 连续 | 113 个，仅 44 个具名，69 个无名导出，序号 100–212 有断档 |
| 进程内导入者 | 仅 `UnityPlayer.dll` | `UnityPlayer.dll`、`Qt5Widgets.dll` |
| 转发表 | 47 条 PE 转发，与现有 XInput 代理写法一致 | 无名导出需 `#序号` + `NONAME` 形式 |

若将来需要在图形设备创建前介入，使用 `HID.DLL`。注意其承载手柄输入，转发表错误会导致手柄失效。

现阶段不迁移 `xinput1_4.dll`：OptiScaler 自行占用 `dxgi.dll` 并由静态导入拉起，代理的加载时机对本功能无影响，迁移无收益且增加手柄输入的回归面。

## 组件获取与许可

OptiScaler 为 GPL-3.0，本项目为 AGPL-3.0，二者可结合，但分发 GPL 二进制需随附其许可与源码获取方式。为保持边界清晰，采用按需获取而非打包：

1. 优先使用 `payloads/optiscaler/` 下的本地副本（离线场景，需自行放置）。
2. 本地副本不存在时，从 OptiScaler 的 GitHub Release 下载**固定版本**，按内置 SHA256 清单校验后使用。
3. 校验失败一律中止，不回退到未校验的文件。

固定版本与哈希记录在 `manifests/shared/optiscaler.lock.json`，升级由该文件驱动，不跟随上游 latest。FSR4 的 INT8/FP8 模型 DLL 随 OptiScaler 发布包提供，同样纳入校验清单。

当前锁定 **v0.9.4**（2026-07-18），资产 `Optiscaler_0.9.4-final.20260718._MM.7z`，SHA256 `575CB4DF…E80EF0AD`，与上游 release 公布值一致。清单收录 18 个文件，排除 `setup_windows.bat`、`setup_linux.sh` 与发布包内的 README——本软件使用自己的部署流程，那三个文件会与之冲突。`OptiScaler.dll` 的目标名为 `dxgi.dll`，其余文件保持发布包内的相对路径（含 `D3D12_Optiscaler/` 与 `Licenses/` 两个子目录）。

不分发 NVIDIA 的 `nvngx_*` 或 Streamline 运行时；客户端自带的这些文件只读不改。

## 部署契约

沿用 `XInputDeploymentService` 的所有权模型：SHA256 校验、JSON 清单、拒绝覆盖或删除非本软件安装的文件。区别在于本功能是多文件部署，清单需逐文件记录。

客户端目录下由本功能拥有的文件：

```
dxgi.dll                              OptiScaler 主体
OptiScaler.ini                        由软件生成，不接受手工编辑后的回写
amd_fidelityfx_*.dll / *.dll          OptiScaler 发布包内的后端与模型文件
BetterEndfield.display.install.json   部署清单
```

清单结构：

```jsonc
{
  "Product": "BetterEndfield.DisplayPipeline",
  "OptiScalerVersion": "<锁定版本>",
  "Files": [ { "Name": "dxgi.dll", "Sha256": "..." } ],
  "PreexistingFiles": [ "..." ],
  "InstallRoot": "...",
  "InstalledUtc": "..."
}
```

`PreexistingFiles` 在首次部署前采集，记录客户端目录中与待写入文件同名的既有文件。卸载只删除 `Files` 中哈希仍然匹配的条目，`PreexistingFiles` 中的任何路径都不删除。虽然当前客户端目录不含 FidelityFX DLL，该机制仍保留：客户端更新可能引入同名文件，且上游卸载脚本有误删客户端自带伴随文件的先例。

状态机与 `XInputDeploymentState` 对齐：`Unavailable` / `NotInstalled` / `Installed` / `UpdateAvailable` / `Conflict`。任一目标文件已存在且不属于本软件时，整体进入 `Conflict` 并拒绝安装，不做部分部署。

## GPU 检测与后端策略

检测通过 DXGI 适配器枚举取得 VendorId 与 DeviceId，据此判定架构代次。策略矩阵：

| 硬件 | 默认后端 | 说明 |
| --- | --- | --- |
| RDNA4（RX 9000） | FSR4 FP8 | 硬件加速，代价约 3–5% |
| RDNA3（RX 7000） | FSR4 INT8 | 官方支持，代价约 7–13% |
| RDNA2（RX 6000） | FSR3.1 增强 | FSR4 INT8 可选并标注实验；无 WMMA，DP4a 模拟，代价约 9–20% 且画质弱于 FP8 |
| Intel Arc | XeSS 2.x | 有原生 DX11 后端，不走 FSR4 |
| NVIDIA | 不部署 | 客户端原生 DLSS 已是最佳路径 |

默认值只是初始建议，最终由用户在 UI 中选择。RDNA2 上不把 FSR4 设为默认，是因为在该硬件上它与 FSR3.1 的取舍需要逐机验证，不宜预设。

## OptiScaler.ini 托管

生成器读取发布包内附注释的 `OptiScaler.ini` 模板，只覆写受管键，其余键与全部注释原样保留。模板中找不到受管键时直接失败，使版本漂移可见。不从零构造该文件：键名随上游版本变动，凭记忆构造会写出无效配置且不报错。

以 v0.9.4 为准的受管键：

| 节 | 键 | 取值依据 |
| --- | --- | --- |
| `[Upscalers]` | `Dx11Upscaler`、`VulkanUpscaler` | FSR4 为 `fsr31_12`（DX11 走 dx11on12、Vulkan 走 VKon12，均经 DX12 interop）；FSR3.1 为 `fsr31`；XeSS 为 `xess` |
| `[Inputs]` | `EnableDlssInputs` | 恒为 `true`。客户端无 FidelityFX API DLL，FSR3 输入不可拦截 |
| `[FSR]` | `Fsr4Update` | 启用 FSR3.X → FSR4 升级。默认 `auto` 仅对 RDNA4 为真，RDNA2/RDNA3 必须显式置真 |
| `[FSR]` | `Fsr4ForceEnableInt8` | RDNA2/RDNA3 必须置真，默认为假 |
| `[FSR]` | `FsrAgilitySDKUpgrade` | 仅 Windows 10 且启用 FSR4 时置真，依赖部署的 `D3D12_Optiscaler/` |
| `[Spoofing]` | `Dxgi` | 非 NVIDIA 硬件启用，否则客户端不暴露 DLSS 选项 |
| `[Spoofing]` | `StreamlineSpoofing` | 客户端走完整 Streamline 栈，需单独放开该层 |
| `[FrameGen]` | `Enabled`、`FGInput`、`FGOutput` | 恒为 `false` / `nofg` / `nofg` |
| `[Dx11withDx12]` | `UseDelayedInit` | 启用 FSR4 时置真，延迟 interop 初始化以提高兼容性 |
| `[Log]` | `LogToFile`、`LogLevel` | 由 UI 诊断开关控制，默认关闭 |

早期文档（含 DeepWiki 的 Compatibility and Troubleshooting 页）列出的 `TextureSyncMethod`、`CopyBackSyncMethod`、`SyncAfterDx12` 与 `DontCreateD3D12DeviceForLuma` **在 0.9.4 中均不存在**，那些是旧版本或旧分支的键名，不要采用。`[Dx11withDx12]` 在 0.9.4 中只有 `UseDelayedInit` 与 `DontUseNTShared`。

OptiFG 不启用：客户端已有原生 DLSS-FG，且上游明确 OptiFG 仅适用于无原生帧生成的场景。

## 渲染后端约束

客户端提供 Vulkan 与 DX11 两种后端，无 DX12 选项。FSR4 在两者下均经 DX12 interop 运行。软件应在部署前读取客户端当前后端设置并提示，不在未验证的后端上静默启用。

## 待实测

以下项目在本文撰写时未验证，实测后应回写本节：

- DX11 与 Vulkan 两种后端下 DX12 interop 的稳定性与画质差异。
- OptiScaler 在 Streamline 集成（而非裸 NGX）下的拦截层选择与实际生效情况。
- RDNA2 上 FSR4 INT8 相对客户端原生 FSR3 的等帧率画质对比，用于确认 RDNA2 默认值是否需要调整。
- 客户端更新后 `UnityPlayer.dll` 导入表是否仍包含 `dxgi.dll`。该前提失效会使整个方案失效，应纳入版本适配检查。
