# 双端第三方模块加载器与 UI 容器实现

日期：2026-10-02。Better Endfield Next 3.4.2 已实现双端第三方模块 ZIP 导入、游戏进程原生加载、网页容器、配置/消息/日志，以及可选共享 Hook 链。包 format 1、Native ABI 1；独立 SDK 版本 1.0.0。具体字段、示例与编译命令见 [第三方模块创作者指南](THIRD_PARTY_MODULE_CREATOR_GUIDE.md)。本文记录架构、实现边界与后续工作。

## 定位与分工

以原生模块加载器为主。我们提供包导入、Host 加载、生命周期接点、配置与消息运输、网页 UI 容器和可选共享 Hook 基础设施。开发者自行维护游戏函数表、偏移、签名、调用方式、功能逻辑、配置 schema 与 UI。

允许作者用 C/C++、Rust 或其他能产出目标平台原生库并提供 C ABI 入口的工具链；内部可以携带自己的脚本解释器、算法库或 Hook 工具。首版无需为他们另造统一脚本运行时，也不要求先把功能改成内置语义服务。

公开的是当前版本的接入约定和开发建议。模块的游戏版本适配、相互兼容、运行效果与稳定性由作者测试和说明。Host 返回加载/投递错误并记录日志；不能把原生崩溃包装成可恢复的普通错误。

| 我们实现 | 开发者实现 |
| --- | --- |
| 导入、按平台选择二进制、模块 ID 和启用配置 | Windows x64 DLL / Android arm64 SO |
| Host 中的固定入口与版本识别 | 游戏函数表、类型、签名、偏移与更新适配 |
| 网页容器、资源目录映射、消息桥 | HTML/CSS/JS、控件、布局、编辑器及业务协议 |
| 独立配置存储、按 ID 转发请求/结果 | 配置字段、默认值、校验与迁移 |
| 已注册 Hook 的归属、顺序与链基础设施 | Detour 签名、参数/返回值处理、线程和功能冲突协调 |
| 状态、日志、停用入口 | 自身修改的恢复、线程与对象生命周期处理 |

## 现有代码基础

- 内置 Windows `module_manager.cpp` 继续使用现有 `BE_ModuleApiV1` 与 contract。第三方走独立的 `native/shared/third_party_modules/third_party_host.cpp`，固定新入口为 `BetterEndfieldNext_GetThirdPartyModuleV1`，不改变内置 ABI。
- `ThirdPartyModule.h`、`ModuleApi.h`、`HookChain.h` 为公开头文件。runtime 是可选辅助能力，作者可自行维护游戏类型、地址与版本适配；初始化尚不可用时可通过 `get_runtime` 后续查询。
- Android 使用同一共享 Host 与动态 SO 通道；`ThirdPartyRuntimeMaterializer` 将已发布 ZIP 解包至游戏私有目录，`ThirdPartyRuntimeUpdater` 更新索引，游戏进程再执行加载。
- 两端 broker 已接入可选共享链，Windows 使用 MinHook，Android 使用 Dobby；既有独占 API 保持原语义。内置目标未统一迁移，遇到独占占用仍报 `Conflict`。
- 两端已新增第三方模块管理页与独立网页容器。通用请求/回复使用带 token 的 loopback Host 服务与有界队列，不复用内置 command pump 的单 pending 槽。

## UI：作者自由编写网页

应用的“第三方模块”列表提供导入、启用、排序、移除和页面入口；管理卡片显示名称、作者、版本、状态与错误，网页容器提供状态/日志入口。作者可以写自己的编辑器、图表、预览和交互。没有 UI 的原生模块仍可以启用；只有 UI 的包状态为 `ui_only`。

Windows 使用 WebView2，Android 使用 WebView；manifest 当前只有一个 `ui` 入口，同一包共用一套静态 HTML/CSS/JS，作者自行做响应式布局和按需要的浏览器能力适配。当前未提供主题/语言启动事件。

页面从不可变安装目录加载。Windows 已使用 WebView2 本地目录映射，Android 使用 WebView 请求拦截读取该目录；两端每个安装代次使用独立 HTTPS 来源。Android 注入桥在作者脚本前执行，外部请求受容器限制。桥绑定所属模块；页面传来的 module_id 不用于选择其他模块，token 不暴露给网页。

页面可以选用现有前端框架，分发编译后的静态文件。第三方原生 UI 若作者自行创建，由作者处理其平台和窗口生命周期；首版公开的可嵌入容器是网页，不另做 WinUI 控件 DLL 与 Android View 的跨平台装载协议。应用页面、游戏内窗口与悬浮层是不同的承载位置；首版把完整编辑器放在应用页面。

## 最小消息桥

UI 在应用进程，原生模块在游戏进程。网页按钮不能直接在 UI 进程调用游戏 DLL/SO。路径为：作者页面 → 应用消息桥 → 现有框架连接/跨进程运输 → 游戏 Host → 对应模块；结果反向返回。

已注入的 `window.betterEndfield` 前端桥提供：

- `readConfig()` / `saveConfig(object)`：应用持久化原始配置，已连接时通知 Host；字段含义和迁移由作者处理。
- `send(body)`：把作者定义的 JSON 送到自己的原生模块，Promise 代表接收确认。
- `onmessage(listener)`：收到带 `request_id` 的 reply 与 body，或 event；返回取消订阅函数。
- `status()`：查询自身连接/模块状态与日志。请求有超时，消息不持久化。

运输层为请求和每模块响应/事件保留有界队列，区分接收确认、模块回复、未连接与超时；未实现作者声明的高频合并策略。关闭页面只撤销页面订阅，不停用原生模块。loopback HTTP 的 `/send`、`/poll`、`/configure` 和 `/status` 由应用桥使用，网页无需直接实现它们。

Host 不理解业务 body，不列举所有游戏操作。作者可以自定义任意消息类型，但固定的包入口、消息信封和生命周期接点仍是加载所必需的接入协议。游戏函数表与这些接点分开。

生命周期、配置、消息、shutdown 当前均在 Host worker 串行执行；没有面向作者的通用游戏主线程任务 API。作者自行安排正确的游戏调用线程。Hook 执行线程由目标实际调用方决定，可选 runtime 就绪不改变这个约束。

## 包与加载

建议 ZIP 中包含 `module.json`、`native/windows-x64/*.dll`、`native/android-arm64/*.so`、`ui/` 和作者自己的资源。只支持一端的模块可以省略另一端二进制。

manifest 只描述 ID、名称、作者、模块版本、入口/加载 ABI、平台二进制、可选模块依赖、UI 入口及默认配置。加载 ABI 与游戏函数表版本分开；作者可另写其已测试的游戏版本作为说明。包内声明路径保持在自己的安装目录，模块 ID 不得与内置或已安装模块冲突。

Host 选当前平台入口，在游戏进程加载 `BetterEndfieldNext_GetThirdPartyModuleV1` 返回的 `BE_ThirdPartyModuleV1`。新结构与 `BE_ThirdPartyHostV1` 带 version / struct_size，保持内置 Host ABI 不变。initialize/on_message 必需，configuration_changed/shutdown 可选。

Windows 在游戏内加载对应 DLL。Android 在框架连接后发布 ZIP，游戏私有目录 materializer 校验并提取原生库，再由游戏 Host 加载；导入应用不执行第三方 native。Echo 静态链接运行库；作者携带额外动态依赖时须自行验证目标进程 linker namespace 和依赖解析。

采用进程内驻留：启用/停用与配置更新可实时处理，已加载二进制改变安装代次则保留旧库并显示 restart_required，重启游戏后换用新库。库与链转发器不任意热卸载；旧 UI/原生资源目录和 Android ZIP 当前也未自动清理。页面可独立刷新，重新排序不承诺即时重排已注册 Hook 链。

## 同目标 Hook：一处底层补丁，多节点调用链

可以让同一个函数挂多个模块，但必须让 Host 的 broker 共管它们。同一地址只安装一次 MinHook/Dobby 补丁，在它上方维护模块调用链。

参与共享链的典型顺序：`目标入口 → 模块 A → 模块 B → 游戏原函数`。模块拿到的 `next` 表示调用下一节点，最后一个节点再到真实 trampoline。作者有意不调用 next 时会截断后续链；Host 不承诺各功能的行为因此自动兼容。既有内置 exclusive Hook 未自动成为链节点。

具体约定：

1. 提供 `chain` 与 `exclusive` 两种 Hook 注册模式。chain 支持多节点；确实需要独占的注册仍可报冲突。不是所有 Hook 都必须改成链。
2. 节点按实际注册顺序组织；依赖影响模块启动先后，应用支持调整安装索引顺序。同级稳定 ID 排序、运行中任意优先级重排未承诺。
3. Host 为每个节点提供稳定的 next 转发入口，而不是把某个后来会卸载的 detour 地址直接交给其他模块。节点停用后转发到后继，移除一个模块不拆掉其他模块仍在使用的底层 Hook。
4. 转发器按 Windows x64 / Android arm64 的 ABI 处理透明转发，不解码游戏参数。作者保证其 detour 与目标的参数、返回值、隐藏参数及调用约定一致；不能用统一的 `void(void)` 分发器处理任意函数。自报签名标签可以帮助提示冲突，但不能代替作者正确声明。
5. 模块只注册自己的节点。链节点/转发器保持驻留。停用后重新注册获得新 handle，稳定 next 可能复用；作者必须刷新返回的 handle/next，不复用旧停用句柄。
6. 文档建议通常调用 next 一次、避免再次调用被 Hook 的目标造成递归、避免阻塞游戏线程。作者仍可按功能需要主动截断调用或自行选择实现。
7. Host 仅管理经过 broker 注册的 Hook。模块自建 Hook 库、直接修改同一目标或与外部加载器叠加时，作者自行协调；Host 不将这些外部操作伪称为已参与其共享链。

已保留既有 V1 的单占语义，通过 `BE_HookChainApiV1` 增加可选共享链。chain 与 exclusive 双向检查占用并返回 Conflict。后续如需让第三方与特定内置目标共存，需逐个评估并迁移内置节点；当前不宣称已完成这些迁移。

共享链是加载器内部最需要验证的一块。至少覆盖不同 ABI 形态的透明传参/返回、两个模块的顺序、停用中间节点、一次原函数调用、递归和仍在运行的回调。验证转发基础设施，不替作者验证其游戏逻辑或功能兼容。

## 当前交付与后续工作

1. 已交付双端不可变包目录、索引、DLL/SO Host、生命周期、配置、状态与日志，包路径/容量/保留 ID 校验。
2. 已交付双端网页容器、消息桥与 Echo/Counter 同包双端样例。包导入、更新、配置、HTTP 和 JS 桥有回归验证。
3. 已交付两平台可选共享链，保留独占契约；含多节点、停用、透明转发/返回与冲突回归。特定内置节点迁移仍属后续工作。
4. 已交付[创作者指南](THIRD_PARTY_MODULE_CREATOR_GUIDE.md)、三份公开头文件与 `tools/ThirdPartyModules/BuildSdk.ps1`，导出完整双端 Echo ZIP 和独立 SDK ZIP。
5. 后续可评估旧代次清理、游戏主线程任务接口、主题/语言启动信息及内置链迁移；这些不属于当前已公开能力。

核心文档教如何被加载、收发消息、使用 Host 可选能力与管理生命周期。游戏函数表和功能实现由各作者自行提供与维护。SDK 内容使用明确文件白名单，不携带 private index、token 或用户配置。
