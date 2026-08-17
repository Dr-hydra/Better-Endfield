# OmniMix 对接 Better Endfield 交付要求

本文档是 OmniMix 项目组实现“终末地游戏集成”时使用的接口契约。OmniMix 与 Better Endfield 必须保持为两个独立项目；OmniMix 不加载游戏 DLL、不实现 Wwise Hook，也不直接修改 Better Endfield 配置。

## 1. 责任边界

### OmniMix 负责

- 定位用户选择的 Better Endfield 安装目录。
- 使用 OmniMix 自己的 `OmniMixBackendManager.FindBackendExe()` 取得当前后端的绝对路径。
- 调用 Better Endfield 提供的注册、查询和卸载命令。
- 启动和管理 OmniMix 后端，按照 OmniPcmShared 协议发布 PCM。
- 正确处理自然播放结束，保证共享缓冲中的尾部音频被客户端消费。
- 随 OmniMix 交付与下述 ABI 兼容的 `OmniPcmShared.dll`、C/C++ 头文件、版本号和 SHA-256。

### Better Endfield 负责

- 校验并保存 OmniMix 后端路径和稳定的客户端标识。
- 在用户启用音乐替换后加载 `BetterEndfield.Music.dll`。
- 必要时启动已注册的 OmniMix 后端，并连接对应共享内存会话。
- 完成 PCM 格式转换、缓冲、Wwise Audio Input 输出和原游戏音乐的暂停/恢复。
- 后端不可用、流中断或模块异常时恢复原游戏音乐。
- 提供 UI、诊断日志和卸载行为。

### 明确禁止

- OmniMix 不得向游戏目录或 Better Endfield 安装目录复制文件。
- OmniMix 不得直接编辑 `%LocalAppData%\BetterEndfield\BetterEndfield.ini`。
- OmniMix 不得依赖 `GameAssembly.dll` 的哈希、RVA 或官服登录 SDK。
- Better Endfield 不解析 OmniMix 曲库，不决定播放列表，也不访问 OmniMix 网络服务。
- 两侧不得把 PCK、BNK、WEM 或游戏资源作为集成依赖分发。

## 2. Better Endfield CLI 契约

OmniMix 应从用户选择的 Better Endfield 安装目录启动 `BetterEndfield.exe`。所有命令均为非交互命令：不得创建主窗口，不得显示对话框，结果通过退出码和标准输出返回。

### 注册后端

```powershell
BetterEndfield.exe --register-omnimix-backend "C:\path\to\OmniMixPlayer.Backend.exe" --silent
```

要求：

- 路径必须是绝对路径，目标文件名必须为 `OmniMixPlayer.Backend.exe`。
- Better Endfield 校验文件存在、为 x64 PE，并读取文件/产品版本。
- 后端目录必须包含兼容的 `native\x64\OmniPcmShared.dll`；注册时会调用 ABI 查询接口，要求 ABI 主版本 `2`、共享协议 `2` 和交错 `float32`。
- 注册仅保存后端位置，不自动启用音乐替换。
- 重复注册同一路径必须幂等；注册新路径应覆盖旧路径并保留稳定 `client_id`。
- 成功时 stdout 输出单行 JSON，不能输出本地化说明文字。

成功示例：

```json
{"schemaVersion":1,"registered":true,"backendExe":"C:\\OmniMix\\OmniMixPlayer.Backend.exe","clientId":"better-endfield-4b5147e2a8e64f49","backendVersion":"1.2.3.0","valid":true}
```

### 查询状态

```powershell
BetterEndfield.exe --query-omnimix-registration
```

查询始终输出单行 JSON。未注册不是命令执行故障：此时退出码仍为 `0`，`registered=false`。

```json
{"schemaVersion":1,"registered":false,"backendExe":"","clientId":"","backendVersion":"","valid":false,"reason":"not_registered"}
```

若已注册路径失效或 SDK 不兼容，返回 `registered=true`、`valid=false` 和稳定的机器可读 `reason`，例如 `file_missing`、`wrong_filename`、`not_x64_pe`、`sdk_missing`、`sdk_contract_missing`、`sdk_abi_incompatible`。

### 解除注册

```powershell
BetterEndfield.exe --unregister-omnimix --silent
```

解除注册只清除后端路径和本集成生成的状态，不卸载 OmniMix，不删除用户文件，也不改变 Better Endfield 其他模块开关。重复调用必须成功。

### 退出码

| 退出码 | 含义 |
| ---: | --- |
| `0` | 命令成功，包括“当前未注册”查询 |
| `2` | 参数缺失、重复或不合法 |
| `3` | 后端路径或 PE 校验失败 |
| `4` | 配置读取/写入失败 |
| `5` | 不支持的配置或接口版本 |
| `10` | 未分类内部错误 |

OmniMix 必须同时检查进程退出码和 JSON；不得根据中文界面文本判断状态。

## 3. OmniPcmShared 交付与 ABI

OmniMix 项目组需提供一个可被原生 x64 模块动态加载的稳定发行包。运行时 DLL 必须位于注册后端的固定相对路径，SDK 头文件和校验文件可放在发行包的开发资料目录：

```text
<backend-root>/OmniMixPlayer.Backend.exe
<backend-root>/native/x64/OmniPcmShared.dll
<sdk-root>/include/OmniPcmShared.h
<sdk-root>/VERSION
<sdk-root>/SHA256SUMS
```

为兼容便携调试布局，Better Endfield 也接受
`<backend-root>/OmniPcmShared.dll`。正式发布应使用 `native\x64` 布局；
Better Endfield 自身模块目录中的同名 DLL 仅用于开发诊断，不属于 OmniMix 的安装契约。

要求：

- Windows x64，C ABI，导出函数使用明确 calling convention。
- 当前兼容契约为 OmniPcmShared ABI `2.x`、共享内存协议 `2`、交错 `float32`；实例通过 `OmniPcm_OpenInstanceUtf8` 打开，状态通过带 `size` 字段的 `OmniPcm_GetSnapshotV2` 读取。ABI 查询和这两个 V2 入口均为必需导出，不接受旧版固定尺寸快照降级。
- 注入进程版本必须使用静态 MSVC 运行时（`/MT`）或等价的自包含实现，不能绑定游戏目录内已加载的 `MSVCP140.dll`/`VCRUNTIME140.dll`。
- 结构体包含 `size`/`version` 字段；新增字段只能向后追加。
- 明确字符串编码、所有权、线程安全性和错误码。
- DLL 的加载不得隐式启动 UI、联网或修改注册表。
- 客户端必须能够查询协议版本、列举/打开实例、读取流描述、读取 PCM、查询心跳和关闭会话。
- 读取 API 必须明确返回帧数而非字节数，并提供采样率、声道数、样本格式、`stream_id`、写入游标及播放状态。
- Better Endfield 只要求 PCM；首选交错 `float32`，允许 OmniMix 文档中已有的兼容格式。
- 映射名使用：

```text
Global\OmniMixPlayer_PCM_<instance_id>
```

OmniMix README、头文件和实际实现必须使用相同命名。发布时需向 Better Endfield 项目组提供 SDK 版本、下载/产物来源和 DLL SHA-256，用于依赖锁定。

## 4. 后端启动约定

Better Endfield 会在首次连接失败时隐藏启动已注册的 `OmniMixPlayer.Backend.exe`。当前契约要求后端不带参数即可启动并创建全局端口发现文件；Better Endfield 随后通过 `OmniPcmClient_ConnectInstance` 提交 `client_id` 并取得 `instance_id`，不通过命令行创建实例。

首次失败并启动后端后，Better Endfield 会销毁旧控制客户端，并在约 500 ms 后重新创建客户端以再次发现端口；后端不得假定客户端会永久使用启动前的默认端口。

要求：

- 已有兼容后端实例时，Better Endfield 会先连接而不会重复启动；若仍发生重复调用，后端必须快速复用或正常退出，不能启动互相争用的多个实例。
- 后端路径的工作目录不得影响资源定位。
- 后端启动后应在约定超时内创建共享映射并开始心跳。
- `client_id` 只用于实例关联，不作为安全凭据。
- Better Endfield 退出不代表 OmniMix 必须退出；所有权和空闲退出策略由 OmniMix 决定。
- 后端若将来要求启动参数，必须保持无参数入口兼容，或先提供版本化能力查询并与 Better Endfield 联合升级。

## 5. 播放生命周期语义

### 自然结束

当前实现中，解码器 EOF 后立即切换下一首会截断仍在共享缓冲中的尾部音频。OmniMix 必须实现以下顺序：

```text
MarkDecoderEof
-> 等待 readCursor >= finalWriteCursor
-> 等待 audibleCursor 接近 finalWriteCursor
-> MarkEnded
-> NaturalEnd
```

- `finalWriteCursor` 在 EOF 时冻结。
- `readCursor` 表示客户端已从共享缓冲取走的最后位置。
- `audibleCursor` 表示预计已经经过客户端/设备缓冲并实际可闻的位置。
- 接近阈值须按帧或毫秒定义，并设有故障超时；超时需记录原因后结束，不能永久阻塞播放队列。
- OmniMix 现有 `IsClientDrained()` 应进入自然结束状态机，而不是仅作为未使用的辅助方法。

### 手动操作

`Next`、`Previous`、`Seek`、停止和重新选曲属于显式用户操作，不等待旧流自然排空：

- 递增或更换 `stream_id`。
- 丢弃旧 generation 的待播数据。
- 立即公布新流元数据和游标。
- Better Endfield 根据 `stream_id` 切换本地 generation，旧数据不得混入新歌。

### 心跳与失效

- 后端应周期更新单调心跳。
- 暂停、缓冲、播放、自然结束和错误必须是可区分状态。
- 进程消失、心跳超时、格式不支持或游标回退时，Better Endfield 会停止自定义 Event 并恢复原游戏音乐。

## 6. OmniMix 游戏集成页

OmniMix 的“游戏集成”页面只负责桥接，不承载 Better Endfield 的音乐替换设置。

页面至少提供：

- Better Endfield 安装目录选择与自动定位。
- 当前后端绝对路径展示。
- `注册`、`修复注册`、`解除注册` 三个动作。
- 通过 `--query-omnimix-registration` 得到的实时状态：未注册、有效、路径失效、版本不兼容。
- 明确提示“是否替换登录/主界面/游戏内音乐”等设置在 Better Endfield 内完成。

自动定位得到多个候选时必须让用户选择。不得将游戏目录误认为 Better Endfield 安装目录；有效安装至少应包含：

```text
BetterEndfield.exe
runtime\BetterEndfield.Host.dll
modules\
```

## 7. 兼容性与升级

- CLI JSON 使用 `schemaVersion`；OmniMix 必须忽略未知字段。
- OmniPcmShared 使用独立 ABI 版本，不与 OmniMix 产品版本绑定。
- Better Endfield 的官服/B服兼容由其基于 IL2CPP 元数据签名的动态解析负责，OmniMix 无需区分渠道。
- 后端升级或移动后，OmniMix 应重新执行注册命令。
- 若 ABI 主版本不兼容，Better Endfield 保持原游戏音乐并记录诊断，不得导致游戏启动失败。

## 8. OmniMix 侧验收清单

OmniMix 项目组提交前至少验证：

1. 首次注册、重复注册和覆盖旧路径。
2. 查询未注册、有效注册、文件被移动和文件被删除。
3. 解除注册幂等，且不删除 OmniMix 或 Better Endfield 文件。
4. 带空格和非 ASCII 的绝对路径。
5. 后端已运行时重复启动不会产生冲突实例。
6. 连续播放、暂停/恢复、Seek、Next 和切歌后的 `stream_id` 隔离。
7. 自然 EOF 的最后一段 PCM 确实被消费并可闻，之后才自动下一首。
8. Better Endfield 客户端断开、崩溃或未安装时，OmniMix 后端继续按自身策略稳定运行。
9. `OmniPcmShared.dll`、头文件、README 的 ABI 与映射名一致。
10. 安装、便携版、升级和卸载场景均不会向游戏目录写入集成文件。

联调交付时请同时提供：SDK 产物、版本与 SHA-256、后端启动参数说明、CLI 联调结果、自然结束测试日志，以及一个不依赖真实曲库的 48 kHz 立体声测试流。

## 9. 2026-08-16 联调状态

当前 `playerbuild` 的后端和 `native/x64/OmniPcmShared.dll` 已通过 Better Endfield 注册、
ABI 2.0 校验、自动启动、共享内存连接、暂停/恢复、Seek、切歌、自然排空、进程终止后
自动重连等实测。Better Endfield 仓库在联调中未修改 OmniMix 文件。

当前部署包尚未包含 SDK README 所列的 `test_stream_48k_stereo.wav`。本次自然 EOF 使用
现有曲目近尾 Seek 验证；OmniMix 项目组正式交付时仍需补充该 2 秒本地测试流，供无账号、
无曲库和低流量环境做确定性验收。

当前 `OmniMixPlayer.Backend.exe` 的 Windows 文件/产品版本读取结果为 `0.0.0.0`。ABI 与
运行时功能不受影响，但正式部署应写入真实版本资源，使 Better Endfield 注册状态和升级诊断
能够显示可追踪的后端版本。

## 10. 2026-08-17 收尾状态

Better Endfield 连接实例时已声明 `QueueManagement`、`Seek`、`AudioPlayback` 与
`ServerControlledPlayback`。OmniMix 的 `insertIntoQueue` 能力校验不再拒绝该实例；实例
重新连接即可刷新能力，无需要求用户删除队列或实例配置。

联调期间曾偶发一次呈现线程崩溃，但后续无法稳定复现，现有证据也不足以确认它与
OmniMix、Better Endfield 音乐模块或图形后端有关。该现象不构成集成兼容性要求；OmniMix
无需因此修改 PCM、队列或后端生命周期实现。
