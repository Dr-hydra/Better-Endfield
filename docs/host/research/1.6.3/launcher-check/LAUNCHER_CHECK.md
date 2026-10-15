# 1.6.3 官方启动器父进程检查

2026-10-15 本机已复现：BE 内置 Injector 直接创建 `Endfield.exe` 后，游戏提示“游戏启动失败 / 请通过官方启动器打开游戏”；由官方 `Games.exe` 正常启动、同时经现有 XInput 加载 BE 后成功进入开屏并完成资源采集。

## 直接证据

来源为 `%USERPROFILE%\AppData\LocalLow\HyperGryph\Endfield\sdklogs\` 中本机日志。这里只记录相关校验结果，不复制日志中的账号、会话或认证数据。

| 时间 / 入口 | 日志记录 | 结果 |
| --- | --- | --- |
| 09:07:54 / BE Injector 直接创建进程 | `OneChannelLC.log:12` 开始 `Launcher` 检查，随后 `Not found matched name` | errorCode 10100，提示从官方启动器启动 |
| 09:11:04 / 官方启动器 | `OneChannelLC.log:24` 记录 `Parent process check success:3604`，匹配 `D:\Hypergryph Launcher\1.6.0\Games.exe` | inner code 0，开屏正常 |

`u8sdk_pc.log:4575` 附近给出调用阶段 `HGU8SDKSetData → U8SDKImpl::U8StartLaunchCheck → ResolveLaunchErrorMsgBoxTexts`；当前 `U8SDK.dll` 中有对应提示字符串。无需 IDA 或全量反编译即可定位此次失败。

## 与更新的关系

| 本机历史日志 | 游戏标识 | U8SDK |
| --- | --- | --- |
| 10-15 00:08（u8sdk_pc.log:4241、4261） | prod_obt1.5.3 | 2.30.3.0 |
| 10-15 08:13 起（4364、4388） | prod_obt1.6.3 | 2.34.3.0 |

旧日志已有 `Found launch check:167`，最早可见 09-02，不能声称整套检查代码都是本次新增。但 `Start launch check` / `Launch check result` 的实际执行记录从本次新版启动开始出现。结合当前直接启动失败、官方父进程成功，结论是 **新版已实际执行或加强官方启动器父进程检查**，不是仅凭提示猜测缺某个命令行参数。

## BE 入口影响

`native/loaders/injector/main.cpp` 的现有路径使用 `CreateProcessW(... CREATE_SUSPENDED ...)` 直接创建游戏，因而不具备官方父进程。UI 选择 XInput 后的“保存并启动”仍可能直接 `Process.Start(Endfield.exe)`，同样不应当作官方启动入口。

当前可用方式：准备/安装现有 XInput 自启动代理，随后在官方启动器点击“开始游戏”。本轮该路径已实际采齐五个模型资源根，加载器与 Model/CustomModel 工作正常。没有修改客户端校验、伪装父进程或猜测认证参数。

后续若调整 BE 启动 UI，应接入官方正常启动流程，或在 XInput 模式保存配置后明确引导用户从官方启动器启动。此记录仅调查原因；本轮未修改生产启动入口。

## 官方快捷方式入口与 Steam 边界

2026-10-15 继续只读检查了本机官方安装目录、桌面快捷方式和注册协议，未启动程序或修改配置。本机桌面“明日方舟：终末地.lnk”提供了以下官方入口：

```text
目标：D:\Hypergryph Launcher\Launcher.exe
参数：--game=endfield --reason=4
工作目录：D:\Hypergryph Launcher
```

`Games.exe` 中的静态字符串同时可见 `--game`、`--reason` 和 `--game=%1 --reason=%2`。本机 `hypergryphlauncher-*` 注册协议的打开命令为 `"D:\Hypergryph Launcher\Launcher.exe" "%1"`；协议参数格式尚未确认，不应自行拼接猜测。

可据此评估由 BE / Steam 调用官方快捷方式入口，再由官方启动器创建游戏、通过 XInput 加载 BE 的正常启动流程。**已验证的部分仍只有官方 `Games.exe → Endfield.exe → XInput`**；本次没有执行快捷方式，尚未确认 `--game=endfield --reason=4` 会自动开始游戏，可能仅定位到终末地页面。Endfield 自定义启动参数能否通过该入口传递，也尚未确认。

当前 BE 两处入口仍需后续评估调整：`ui/BetterEndfieldNext.UI/MainWindow.xaml.cs` 的 XInput 启动分支直接调用 `Endfield.exe`；`ui/BetterEndfieldNext.UI/Services/SteamIntegrationService.cs` 的 `GenerateLaunchCommand` 只允许 `Endfield.exe`，生成 `"国服 Endfield.exe" 参数 %command%`。因此，补齐 Steam 元数据不等于已经验证新版国服启动链可用。

对启动检查小 DLL 和日志的检查，目前只支持“查询父进程路径并匹配父进程名称”这一结论；**完整允许名单尚未提取，不能断言 Steam 父进程一定会被拒绝**，也不能由 BE Injector 的失败推导出所有非官方启动器入口都失败。上述记录同样没有证实可通过增加某个参数解除检查。

Steam 的运行状态、时长、覆盖层和截图仍需通过实际客户端验证。官方启动器复用既有进程、提权或常驻等行为可能影响进程跟踪，当前没有可据以保证这些功能正常的实测结果。本轮未修改校验、未伪装父进程，也未建立全量反编译数据库。

## 后续局部分析：名称、签名和证书指纹

2026-10-15 随后的局部静态分析补齐了上文尚未确认的判断条件。前文保留的是当时只读日志与字符串检查所得结论；以下证据来自约 395 KB 的当前 `OneChannelLC.dll` 局部研究库，未建立 GameAssembly 全量数据库。

当前校验按以下顺序执行：

1. `getpid()` 后通过 Toolhelp 进程快照读取**直接父 PID**，没有祖先进程遍历。
2. `OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION)` 和 `QueryFullProcessImageNameW` 取得父进程完整路径，从中提取 basename。文件名与配置的 `parentNames` 逐项忽略大小写、按完整长度精确比较；无匹配返回 `10100`。
3. 使用该路径对磁盘文件调用 `WinVerifyTrust`，动作 GUID 为 `WINTRUST_ACTION_GENERIC_VERIFY_V2`。签名信任检查失败或无法读取签名证书时返回 `10101`。
4. 通过 `CryptQueryObject`、`CryptMsgGetParam`、`CertFindCertificateInStore` 枚举签名者和嵌套签名，并读取证书 SHA-1 / SHA-256 指纹。配置 `fingerprints` 非空时，至少一项必须忽略大小写精确匹配，否则返回 `10102`。

没有在这一检查函数中发现固定安装目录比较。完整路径用于定位并验证父进程对应的磁盘文件。签名 API 由编码字符串解码后通过 `LoadLibraryExA` / `GetProcAddress` 动态解析，因此早先导入表没有对应名字并不能排除签名校验。

**自行编译一个同名 `Games.exe` helper 不够**：即使名称通过，签名与证书指纹条件仍在。完整原始配置尚未提取，因此这里仍不推断 Steam 父进程是否属于当前允许名单。

局部确认的配置键为 `checkItems` 数组，以及每项中的 `name`、`parentNames`、`fingerprints`。三个 ordinal 导出的 Windows x64 ABI 为：

```cpp
// Ordinal 1：执行检查；userDataPath 下写入 sdklogs/OneChannelLC.log。
uint32_t OCL_1(const char* userDataPath, const char* configJson);

// Ordinal 2：返回内部 JSON 结果字符串；调用方不释放。
const char* OCL_2();

// Ordinal 3：尝试启动官方启动器；独立校验探针不调用它。
bool OCL_3(int isCN);
```

函数 RVA、伪代码来源和配置解码见[局部分析记录](../../../../../research/host/1.6.3/Windows/launcher-check/allowlist-analysis.md)。

## 独立原始 DLL 探针与启动桥验证

已实现独立研究 helper：自行创建一个**真正官方、签名完整的 `Games.exe` 暂停进程**，通过 Windows 正规 `PROC_THREAD_ATTRIBUTE_PARENT_PROCESS` 指定它为目标进程的父进程，再启动目标。官方父进程的主线程始终不恢复，因而不进入官方启动器 UI；helper 等待目标退出，正常结束或处理失败时只清理它自己创建的暂停父进程，不终止既有启动器或目标游戏。此方式没有修改游戏 DLL 或禁用游戏检查。

使用当前原始 `OneChannelLC.dll` 的隔离探针已取得以下结果：

| 探针入口 | 原始 DLL 返回 | 证据 |
| --- | --- | --- |
| 普通 PowerShell 父进程 | `10100`，名称不匹配 | [probe-baseline.json](../../../../../research/host/1.6.3/Windows/launcher-check/probe-baseline.json) |
| helper 创建真实暂停 `Games.exe` 并指定为父进程 | `0`，结果记录官方 `games.exe` 路径 | [checker-result.json](../../../../../research/host/1.6.3/Windows/launcher-check/runs/probe-20261015-093153/checker-result.json) |

**这两次探针使用的是根据本机已知官方签名重建的测试 fixture，不是游戏原始检查配置。** 成功结果验证了真实 DLL 的判断机制和 helper 的父进程设置，不能单独替代完整游戏使用自身配置的启动验证。

随后于 09:32:20 启动完整游戏，使用游戏自身的真实配置通过了检查：helper 创建的官方父进程 PID 为 `29624`，`Endfield.exe` PID 为 `7368`；`OneChannelLC.log` 在 `09:32:34.945` 记录 `Parent process check success:29624`，紧随的结果内层 `code` 为 `0`。这次完整游戏运行没有使用探针 fixture，证明该启动桥在本机当前 CN 1.6.3 的真实启动校验中可用。证据见 [SDK 检查摘录](../../../../../research/host/1.6.3/Windows/launcher-check/runs/game-20261015-093220/sdk-check-evidence.log) 和 [游戏窗口截图](../../../../../research/host/1.6.3/Windows/launcher-check/runs/game-20261015-093220/game-window.png)。

本节写入时，窗口显示 `CN_WIN_REL_1.6.3`，正在开屏资源加载阶段，稳定开屏仍待观察。登录/服务器连接、Steam 时长和覆盖层、BE 加载尚未验证；本次完整游戏测试未安装 XInput 代理。目前仍是独立研究入口，尚未接入 BE 生产 UI 或 Steam 替换启动命令。

本次游戏随后以退出码 `0` 结束；`bridge.log` 记录 `Suspended parent cleaned: 29624`，真实游戏退出后的父进程清理也已完成。无需卸载代理或恢复游戏文件，本次入口没有安装或修改这些内容。

实现、生命周期约束与使用方式见 [helper README](../../../../../research/host/1.6.3/Windows/launcher-check/helper/README.md)；研究入口为 [Start-LaunchBridge.ps1](../../../../../research/host/1.6.3/Windows/launcher-check/Start-LaunchBridge.ps1)，支持独立 `Probe` 和完整 `Game` 两种模式。

## 4.0.3 生产接入

后续已将该机制提炼为 `native/loaders/shared/official_launcher.cpp`，正式 Launcher 和原 Injector 共用。自动从官方协议、安装项和安装目录寻找签名有效且组织为鹰角的 `Games.exe`，不固定盘符、版本目录或证书指纹。Injector 保留既有注入方式、XInput 冲突检查，并在初始化异常时清理本次目标；两入口均等游戏结束后清理自建父进程。

BE 普通启动、快捷方式与已有 Steam 命令已接入：注入器模式无需 XInput，XInput 模式用 Launcher；代理只由用户手动点击安装按钮部署。Steam ACF 应用不改变加载模式。正式混淆包与安装包已完成构建，Steam 参数/资料往返及隔离页面检查通过；真实 Steam 客户端计时和覆盖层仍未验收。见[4.0.3 发布记录](../../../../workspace/releases/4.0.3/RELEASE_4_0_3.md)。
