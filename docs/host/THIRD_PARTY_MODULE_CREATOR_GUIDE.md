# 第三方模块创作者指南

适用：Better Endfield Next 3.5.2，第三方包格式 1 / Native ABI 1。Windows x64 与 Android arm64 使用同一套包与网页消息协议。SDK 版本独立为 1.0.0。

## 先运行 Echo 示例

SDK 中的 `packages/BetterEndfieldNext-Echo-1.0.0-Dual.zip` 是完整双端包。进入管理应用的“第三方模块”，导入 ZIP 并启用，连接正在运行的游戏，再打开模块页面。点击发送应收到带有计数、原请求与当前配置的回复；修改 Label 后保存可验证配置回调。模块原生代码不依赖游戏地址，也不改变游戏行为。

Android 导入时需要框架连接，以便将 ZIP 发布到游戏可读取的目录。Windows 通过管理应用安装目录记录供游戏 Host 读取。只有另一平台二进制的包可记录，但不能在当前平台启用。UI-only 包可以打开页面并保存配置，没有原生消息接收端。

仓库示例位于 `tools/ThirdPartyModules/echo/`；独立 SDK 位于 `examples/echo/`。公开头文件全部位于 `include/BetterEndfieldNext/`：`ThirdPartyModule.h`、`ModuleApi.h`、`HookChain.h`。实际字段和类型以随 SDK 分发的头文件为准。

## 包结构与 manifest

ZIP 根目录必须直接包含 `module.json`，不要在它外面再包一层目录：

```text
module.json
native/windows-x64/example.echo.dll
native/android-arm64/libexample.echo.so
ui/index.html
ui/style.css
ui/app.js
```

```json
{
  "format": 1,
  "id": "example.echo",
  "name": "Echo / Counter",
  "author": "Better Endfield Next",
  "version": "1.0.0",
  "abi": 1,
  "libraries": {
    "windows-x64": "native/windows-x64/example.echo.dll",
    "android-arm64": "native/android-arm64/libexample.echo.so"
  },
  "ui": "ui/index.html",
  "default_configuration": { "label": "Echo" },
  "dependencies": []
}
```

- ID 为稳定 ASCII 标识，规则是 `[A-Za-z0-9][A-Za-z0-9_.-]{0,95}`。`betterendfield.*` 与 `voice.character` 为内置保留名称，按不区分大小写检查。原生入口的 ID 必须与 manifest 完全相同。
- `libraries` 可省略不支持的平台；`ui` 可省略；二进制和 UI 至少要有一种。
- `dependencies` 描述其他第三方模块 ID，不是内置服务名称。依赖不得为自身，不得使用保留 ID，最多 128 项。缺失、停用或加载失败的依赖会阻止正常启动；声明依赖不自动下载它们。
- 配置为 JSON 对象。首次导入使用 `default_configuration`，升级保留原配置；新增字段的默认值和配置迁移由模块作者处理。
- 所有路径为包内相对路径，分隔符使用 `/`。不接受绝对路径、`..`、反斜线、冒号、Windows 保留文件名、重复条目及大小写碰撞。不要依赖软链接或从包外加载 UI 文件。
- 当前上限：ZIP 256 MiB、解包总大小 256 MiB、单条目 128 MiB、4096 个条目、manifest 256 KiB；配置/安装索引 1 MiB。这些是导入限制，不是模块可占用的运行内存额度。

应用为每次安装建立不可变的 UUID 目录。新包默认停用；同 ID 升级保留启用状态和原始配置。已经加载的二进制升级需要重启游戏；状态会显示 `restart_required`。当前版本不自动清理旧安装目录或 Android 旧 ZIP，以免正在运行的库仍需读取其资源。

## 原生入口与生命周期

导出固定 C 入口 `BetterEndfieldNext_GetThirdPartyModuleV1`，返回生命周期结构。以下代码说明入口形态；完整可编译实现见 Echo：

```cpp
#include <BetterEndfieldNext/ThirdPartyModule.h>

static BE_Result BE_CALL Initialize(const BE_ThirdPartyHostV1* host,
                                    const char* configuration_json);
static BE_Result BE_CALL Configure(const char* configuration_json);
static BE_Result BE_CALL Message(const char* request_id, const char* body_json);
static void BE_CALL Shutdown();

static const BE_ThirdPartyModuleV1 module = {
    sizeof(BE_ThirdPartyModuleV1), 1, "example.echo",
    Initialize, Configure, Message, Shutdown
};

#if defined(_WIN32)
#define MODULE_EXPORT __declspec(dllexport)
#else
#define MODULE_EXPORT __attribute__((visibility("default")))
#endif
extern "C" MODULE_EXPORT const BE_ThirdPartyModuleV1* BE_CALL
BetterEndfieldNext_GetThirdPartyModuleV1() { return &module; }
```

`initialize` 与 `on_message` 为必需回调；`configuration_changed` 与 `shutdown` 可为空。结构提供 `struct_size` 和 `version` 用于 ABI 识别；读取可选尾部字段前应先检查大小，检查函数指针是否为空。

Host 提供模块身份、当前平台、包根目录与以下通用操作：

| 字段 | 用途 |
| --- | --- |
| `log(context, message)` | 写入本模块诊断日志 |
| `reply(context, request_id, result, json)` | 返回对应请求的 JSON 回复 |
| `emit(context, json)` | 推送本模块 JSON 事件 |
| `runtime` | 可选 `BE_HostApiV1` 低层辅助接口 |
| `get_runtime(context)` | 后续查询可能刚变为可用的 runtime；可返回空 |
| `hooks` | 可选共享 Hook 链接口 |

`initialize`、配置变化、消息和 `shutdown` 在 Host worker 上串行调用，**不代表游戏主线程**。不要阻塞 worker；有线程要求的游戏操作由作者安排到正确的调用线程。Hook 回调运行于实际调用目标函数的线程。可选 runtime 已准备好也不意味着任意 Unity 方法可从 worker 调用。

UTF-8 字符串只在回调期间借用；返回后仍需使用的请求、配置与路径必须复制。模块负责自身线程、任务、对象和修改的恢复，停用/重新启用时 `initialize` 可能再次执行。不要让自己的工作在停用后继续依赖已经失效的生命周期上下文。

已经加载的 DLL/SO 保持进程驻留，停用调用生命周期收尾；不会任意执行 `FreeLibrary` / `dlclose`。删除模块也不意味着机器码立即从游戏进程移除。原生访问违规或信号可以令游戏退出；C++ 异常捕获不提供进程隔离。作者需要自行维护游戏类型、地址、函数签名与版本适配。

`BE_Result`：`0 Ok`、`1 InvalidArgument`、`2 NotReady`、`3 NotFound`、`4 ContractMismatch`、`5 Conflict`、`6 Failed`。遇到不可用的辅助能力或冲突，应报告清楚的模块状态并停止该功能。

## 网页 UI 与消息

作者提交编译后的静态 HTML/CSS/JavaScript，可以使用自己的框架和响应式布局。Windows 使用 WebView2，Android 使用 WebView。每个安装代次具有独立本地 HTTPS 来源；页面只能读取自己的包资源，外部页面或网络资源不会作为模块 UI 加载。把依赖的 JS/CSS/字体打包到 ZIP 中，避免 CDN。

应用注入 `window.betterEndfieldNext`：

```js
const config = await window.betterEndfieldNext.readConfig();
await window.betterEndfieldNext.saveConfig({ ...config, label: "My module" });

const unsubscribe = window.betterEndfieldNext.onmessage(message => {
  if (message.kind === "reply") {
    console.log(message.request_id, message.result, message.body);
  } else if (message.kind === "event") {
    console.log(message.body);
  }
});

const accepted = await window.betterEndfieldNext.send({ action: "count" });
const status = await window.betterEndfieldNext.status();
// status: { connected, module }
// 页面销毁前取消监听：unsubscribe();
```

| 方法 | 行为 |
| --- | --- |
| `readConfig()` | Promise，读取自身 JSON 对象配置 |
| `saveConfig(object)` | Promise，持久化自身配置；返回 `{saved:true}`，已连接时通知 Host |
| `send(body)` | Promise，发送任意 JSON，结果代表 Host 接收，不是业务执行完成 |
| `status()` | Promise，仅返回本模块状态与连接情况 |
| `onmessage(listener)` | 订阅回复/事件，返回取消订阅函数 |

实际原生回复通过 `onmessage` 收到，如：

```json
{"kind":"reply","request_id":"...","result":0,"body":{"counter":1}}
```

```json
{"kind":"event","body":{"type":"counter","value":1}}
```

Host 将页面固定绑定到自己的模块身份，网页传入的 `module_id` 不能选择其他模块。身份验证 token 保存在应用和 Host 配置中，不交给网页。作者只需要使用上述桥，不需要自行连接 loopback HTTP 或实现运输协议 `better-endfield-next.module-ui.v1`。

配置可在游戏离线时保存，下次连接使用最新持久化配置。UI-only 模块的状态为 `ui_only`，发送原生消息会失败。页面关闭不等于停用原生模块。网页请求超时为 15 秒；Host 队列有容量限制，消息与事件不持久化，作者需要明确自己的业务重试方式。当前没有主题/语言启动事件和面向作者的统一游戏主线程任务 API；页面需要自行处理布局和业务调度。

模块状态包含加载错误和日志。日志保留最近 64 条，每条最多 4096 字节；适合诊断，不应当作持久化数据存储。

## 可选共享 Hook 链

公开接口位于 `HookChain.h`。访问前检查 `host->hooks`、版本、结构大小和方法指针。对于需要共同管理的目标，`create(context, module_id, target, detour, &next, &handle)` 注册本模块节点；`disable(context, handle)` 停用节点，`disable_module(context, module_id)` 停用本模块节点。

同一目标只有一处底层补丁，参与共享链的节点按**实际注册顺序**调用。声明依赖会影响模块启动先后；没有同级模块稳定 ID 排序或任意运行中优先级重排的承诺。

`next` 是稳定的后继转发入口，最后转到原函数，不保证绕过所有其他模块。通常调用一次 `next`；直接重新调用被 Hook 的目标入口可能递归。作者负责 detour 与目标的精确平台 ABI，包括参数、返回值和隐藏参数。链只处理透明转发，不识别函数含义，也不自动解决模块之间的游戏状态冲突。

停用节点后，它的转发入口仍向后继转发，转发器与 trampoline 保持驻留。重新注册同一模块/同一 detour 会获得新的 handle，可能复用稳定 next 转发入口；必须更新两个输出，不要继续使用已停用的旧 handle。

原有 `BE_HostApiV1.create_hook` 保持独占语义。内置模块的既有 Hook 大多仍为独占，**未自动迁入共享链**；共享链碰到这些目标会返回 `Conflict`，反向也一样。作者私自使用其他 Hook 库或直接修改目标，不属于 Host 的链管理范围。共享链不可用、未准备好或冲突都需处理；它不提供任意二进制修改的兼容性保证。

## 编译与打包

Echo 使用 CMake 3.22+、C++20。独立 SDK 解压后在根目录执行：

```powershell
cmake -S examples/echo -B build/echo-windows -A x64
cmake --build build/echo-windows --config Release
```

Windows 需要包含 C++ 编译工具的 Visual Studio Build Tools，生成 x64 DLL。Android 示例用 NDK、Ninja 构建 arm64，示例参数：

```powershell
cmake -S examples/echo -B build/echo-android -G Ninja `
  -DANDROID_ABI=arm64-v8a -DANDROID_PLATFORM=android-29 `
  -DANDROID_STL=c++_static -DCMAKE_BUILD_TYPE=Release `
  "-DCMAKE_TOOLCHAIN_FILE=C:/Android/ndk/27.2.12479018/build/cmake/android.toolchain.cmake" `
  "-DCMAKE_MAKE_PROGRAM=C:/Android/cmake/3.22.1/bin/ninja.exe"
cmake --build build/echo-android --config Release
```

将上述 NDK/Ninja 路径改成自己的安装目录。示例静态链接 MSVC runtime / Android libc++，并启用 Android 16 KiB 页对齐。自带外部动态依赖时，作者须验证目标游戏进程的依赖解析与 Android linker namespace；管理应用中 `System.load` 成功不代表游戏进程可以加载。

单平台构建输出 `build/echo-*/package/`，manifest 只包含该平台。双端包须合并两个平台文件，并恢复原始双端 `module.json`。仓库提供的可复现脚本会构建或复用原生输出、合并当前 UI、导出双端示例和 SDK：

```powershell
# 仓库根目录：默认复用现有 standalone Echo 构建输出
./tools/ThirdPartyModules/BuildSdk.ps1
# 重新编译两个平台
./tools/ThirdPartyModules/BuildSdk.ps1 -Build
```

脚本导出 `BetterEndfieldNext-Echo-1.0.0-Dual.zip` 和 `BetterEndfieldNext-ThirdPartySDK-1.0.0.zip`。SDK 包含三份公开头文件、本指南、完整 Echo 工程、双端原生库及完整导入 ZIP；不包含用户配置、安装索引或身份验证 token。作品分发时只需发布你的模块 ZIP，无需打包整个 SDK。

## 发布前验证

先在双端独立导入 ZIP，检查列表的作者/版本/平台状态，启用后查询连接、加载状态和模块日志。验证配置保存、重新连接、停用/重新启用以及升级后重启游戏的行为。网页收到 acceptance 后还要检查业务回复；不要把“已接收”展示成“功能已完成”。

运行时与游戏绑定的作品还应说明已测试的游戏版本、已知冲突和平台差异。应用能验证包结构与入口 ABI，不能代替作者验证自己提供的原生代码、游戏调用、线程处理和 Hook 语义。
