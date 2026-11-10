# ShizuPosed 免 root 可行性

日期：2026-11-10。审查对象为上游 `Sharif-bot-cmd/ShizuPosed` 快照 `b5f4930`（1.4.5，2026-11-10 克隆）与仓库当前 Android 端代码（libxposed API 102）。本轮只做源码级比对：**未安装 ShizuPosed，未在设备上运行，未接触目标游戏**。引用行号对应上述提交，仓库内不收录上游源码。

## 结论

分三层，不要混读。

1. **注入原语成立，且比 Shizuku 强一个量级——但前提是 `setuid` 能成功。** ShizuPosed 走 `app_process` + `setuid(目标 UID)` + 手动 `handleBindApplication`：setuid 成功时进程真实跑在游戏 UID 下，能读游戏私有目录、能装 ART hook；失败时它以 shell 域继续跑（见文末补正，这一步是整篇的分水岭）。这条路本身推翻了 [Shizuku 篇](SHIZUKU_FEASIBILITY_20261007.md) 里「从官方机制推导不出免 root 进程内入口」的范围限定——路径真实存在，只是不在 Shizuku 本体，而在 `app_process` 引导框架这一层。
2. **BE 现行代码在 ShizuPosed 上零功能，且是入口第一行就死。** [`XposedEntry.java:31`](../../../../../android/app/src/main/java/dev/betterendfield/next/XposedEntry.java#L31) 在 `onPackageReady` 里先取 `getRemotePreferences("module_settings")`；ShizuPosed 对它的实现是直接抛 `UnsupportedOperationException`（上游 `ShizuPosedXposedInterface.java:198`）。异常落进 `:89` 的外层 catch，`report("entry failed: ...")` 后返回，一个 hook 都不装。
3. **"兼容"需要的不是适配层，是一套移植分支。** 三个硬缺口（远程 IPC、管理端服务、入口时序）都在 BE 架构主干上，改完还要接受 ShizuPosed 的 hook 后端质量与"进程不是 zygote 子进程"这两个上限。

判定（2026-11-11 补实机权限实测后）：**零售量产机上不可行**——`setuid` 到游戏 UID 必然失败（`EPERM`），进程只能停在 shell 域，既写不了游戏私有目录也 attach 不上真游戏进程；只有在 userdebug / eng 设备上这条注入链才走得通，而那类设备本来不需要免 root。技术上"有可能"仅在这个前提下成立，产品上不建议。若仍要验证，先跑下面的 A 轮探针。

## 一、机制与可复现的代码事实

引导序列（详见上游 `core/XposedHook.java`）：

| 步骤 | 位置 | 事实 |
| --- | --- | --- |
| 以 shell(2000) 起 `app_process` | `service/ShizuPosedService.java:2394` | 用系统 `app_process` 二进制，dex 由框架投放到 shell 侧目录 |
| `setuid(目标 uid)` | `XposedHook.java:470` | 只接受 uid ≥ 10000；失败则降级为 2000，后续 `handleBindApplication` 通常失败 |
| 取 `ActivityThread` | `:484`、`:602`、`:631` | `systemMain()` → `currentActivityThread()` → 构造函数，逐级回退 |
| 取 classloader | `:640` | `ActivityThread.getPackageInfoNoCheck(appInfo).getClassLoader()`，即游戏真实的 `PathClassLoader` |
| 先装 hook，再建 Application | `:499` → `:543` | 模块入口在 `handleBindApplication` **之前**执行，bootstrap 期 hook 有效 |
| 派发 `onPackageReady` | `:566` | 在 `handleBindApplication` 返回**之后** |
| 进主循环 | `:580-588` | `prepareMainLooper()` + `Looper.loop()` |

hook 后端是固定链 `Pine AUTO → Pine REPLACEMENT → Amiru → CallSite → Native → Instrumentation → Proxy → Noop`，取第一个成功的。只有 Pine 两条能完整传对象参数/`this`/返回值；Amiru 与 Native 的对象参数为 `null`、`this` 为 `null`、无 after-hook；CallSite 上游自己声明不可用。

进程形态（上游 README 明说，代码也一致）：**不是 zygote 派生**。`ActivityThread` 由 `systemMain()` 取得，其 `attach(true)` 不向 AMS 注册应用进程；`getppid()` 是 `app_process`，SELinux label、`Uid:`、启动时间、auxv、cgroup 都是内核真值，XStealth 只伪造 userspace 读路径。

## 二、与 BE 现行契约的逐项比对

| 环节 | BE 实现 | ShizuPosed | 判断 |
| --- | --- | --- | --- |
| 框架 API 版本 | [`module.prop`](../../../../../android/app/src/main/resources/META-INF/xposed/module.prop) 声明 `minApiVersion=102` / `targetApiVersion=102`；依赖 `api:102.0.0` + `service:102.0.0` | 报 **101**，`getApiVersion()` 被改成抽象方法并硬编码 101（上游 `api/XposedInterface.java:32`、`:209`） | 不致命：`ModuleScanner`/`ModuleLoader` 只把 `minApiVersion` 当角标，**没有准入门槛**，BE 仍会被扫到、加载、然后在运行期失败 |
| 远程偏好 + 远程文件 | 设置、命令、状态全走 `getRemotePreferences("module_settings")` 与 `openRemoteFile("command.next"/"command.status")`（`XposedEntry.java:31/66/196`、`FrameworkSettings.java:47-188`） | `PROP_CAP_REMOTE = 0`。`getRemotePreferences` 抛 `UnsupportedOperationException`、`listRemoteFiles` 返回空数组、`openRemoteFile` 抛 `FileNotFoundException` | **致命**。BE 的 UI↔运行时通道全靠它 |
| 管理端 IPC | BE APK 用 `XposedServiceHelper` / `XposedService` 绑定框架服务（`FrameworkSettings.java:13-39`） | 未 vendor `io.github.libxposed.service` 包；替代物是自建的 `LSPosedManager` + `ModuleStatusProvider` ContentProvider + marker JSON | **致命**，需要另写一套（有替代方案，见第四节） |
| 入口时序 | 在 `onPackageReady` 里 hook `Application.attach`，在该 hook 回调中装 GameOverlay / RuntimeBootstrap（`XposedEntry.java:27-86`） | `onPackageReady` 在 `handleBindApplication` 返回后才派发（`:543` → `:566`），此时 `attach` 与 `onCreate` 都已跑完 | **致命**。该 hook 永不触发，需改挂 `attachBaseContext`/`onCreate`，或要求框架在 bind 前派发 |
| 帧泵 | hook `com.unity3d.player.UnityPlayer.nativeRender`（native 方法），在其回调里跑 `NativeCommandBridge.frame()`（`XposedEntry.java:104-117`） | 只有 Pine 后端可能挂住 JNI native 方法；`chain.getThisObject()`/`getArg(0)` 需要对象语义 | **未验证的高风险点**，这是唯一的帧驱动入口 |
| 原生运行时 | 从模块 APK 的 `nativeLibraryDir` 取 `libbetterendfieldnext_android.so`，`Runtime.nativeLoad(path, loader, caller)` 载入游戏命名空间（`RuntimeBootstrap.java:87-133`） | `PackageReadyParam.getClassLoader()` 传的是游戏真实 `PathClassLoader`（`LibXposedDriver.firePackageReady` 以 `appLoader` 调用） | 路径可行；`Runtime.nativeLoad` 在非 zygote 进程里能否落到 Unity 命名空间需实测 |
| 游戏私有目录 | `context.getFilesDir()` 写 BEM、第三方 SO、诊断、catalog（`BemInstalledResources.java:27`、`ThirdPartyRuntimeMaterializer.java:26`） | setuid 成功后即为游戏 UID，可读写；setuid 失败则仍是 shell 域，写不了（见文末补正） | **条件满足**，是相对 Shizuku 的最大改进，但成立与否取决于 `CAP_SETUID` |
| 启动模型 | 玩家从桌面图标启动 | 必须先经框架启动；`ProcessMonitor` 只能"再 spawn 一个同包进程"，**不能附加到已有 pid** | 门槛性缺陷：多进程（反外挂/推送/子进程）里只有被 bootstrap 的那个进程有 hook |
| 平台 | arm64-v8a，minSdk 29 | arm64，Android 10+ | 匹配 |

## 三、能迁与不能迁

| 能力 | 判断 | 边界 |
| --- | --- | --- |
| 进游戏进程、hook Java/ART 方法 | 成立（Pine 后端） | 对象参数与返回值只在 Pine 上可靠；`nativeRender` 是否可挂未验证 |
| 原生 IL2CPP / Dobby hook、形态驱动、渲染线程控制 | 理论上不受框架限制 | 只要 SO 载入成功且帧泵在跑，BE 原生侧与 PC 端同一套；帧泵挂住是前置条件 |
| 游戏私有目录读写、BEM 落盘、热切换 | 成立 | 与 root 方案等价，Shizuku 方案做不到 |
| 设置同步、命令通道、模块状态回传 | 不成立 | 需要自建通道，等于重做 `FrameworkSettings` 与 `NativeCommandBridge` 的收发两端 |
| system_server / 跨进程 / 附加到已运行进程 | 不成立 | 结构性缺口，上游无回退方案 |
| 热重载（`HookHandle.replaceHook`、`getId()`） | 不成立 | API 102 独有，上游明确不做 |
| 躲检测 | 部分成立，上限明确 | 内核态真值（Uid/SELinux/起始时间/auxv/cgroup/父进程 task struct）不可改；硬件证明、内核级监控、服务端交叉核对都不在覆盖范围 |

## 四、若要做：最小改造清单与验收条件

按顺序，前一关不过不进下一关。

**A 轮（go/no-go 闸门，半天量级）**：写一个只有十几行的探针模块，`onPackageReady` 里 hook `UnityPlayer.nativeRender` 打一行日志，不带 BE 任何代码。跑通 = 该游戏在 ShizuPosed 下能进、能挂 native 方法、能拿到游戏 ClassLoader。跑不通则整个议题结束，不必再谈移植。

**B 轮（控制通道替换）**：BE 的 `getRemotePreferences`/`openRemoteFile` 语义要等价重建。可行方案按代价排序：

1. **导出 ContentProvider**（BE APK 内，`exported=true` 且不设 permission）：游戏进程作为普通调用方向 `content://dev.betterendfield.next.*` 读写，UI 进程读同一 provider。跨 UID 可行，Android 11+ 包可见性需要在游戏侧能解析该 authority——游戏进程的 `Context` 是游戏 UID，`ContentResolver` 走系统转发，不受 `queries` 限制，但需要实测。
2. **公共存储目录**：双方都能写的 `/sdcard/Documents/...` 之类；Android 11+ 下 `Android/data/<pkg>` 互相不可读，不能沿用外部私有目录。
3. 保留轮询式 `command.next` 文件名语义，只换底层载体，可以把改动限制在 `FrameworkSettings` 与 `XposedEntry.startRemoteCommandPoller`。

**C 轮（入口时序 + 后端适配）**：把 `Application.attach` 目标改为 `attachBaseContext`/`onCreate`；启动时先读 `getFrameworkProperties()`，`PROP_CAP_REMOTE` 为 0 就走 B 轮通道，为 1 走原路径——这份探测同时能让代码在 LSPosed 与 ShizuPosed 上都成立。hook 安装处补能力探测，禁止在 `chain.getArg(i)` 可能为 `null` 的后端上装需要对象参数的 hook。

**D 轮（端到端）**：BEM 导入/启用/热切换、模型与语音、PCUI 鼠标捕获、相机、MMD、第三方 SO，逐项过。任一项依赖被砍掉的后端能力，就要单独记账。

## 五、风险

- **账号级风险（决定性）**：这是在线运营游戏，进程形态不是 zygote 派生、父进程是 `app_process`、SELinux label 可能与正常 fork 不同。上游 XStealth 自己列的不可覆盖项包括硬件证明、内核级监控与服务端交叉核对。终末地 Android 端具体用哪种完整性校验**本轮没有研究**，因此不做"一定被发现/一定没事"的断言；但把"玩家账号会不会被封"押在这里不合适。
- **依赖质量**：上游是 2026-09 起的个人项目（约 97 star），1.4.x 迭代极快，README 自列 20 余条结构性限制，其中 CallSite 后端自带"不可用"标注。BE 的正式发行不适合绑在这种依赖上。
- **玩家门槛**：Shizuku 每次重启后要重新 adb 授权（Android 11+ 可无线调试），比装一个 LSPosed 模块麻烦；且必须从框架里启动游戏。
- **双进程陷阱**：从桌面图标启动时监控只能再 spawn 一个，可能出现两个游戏进程、登录态互踢。

## 六、验证边界

本轮**没有**做：安装/运行 ShizuPosed、在任何设备上跑通引导、安装或启动目标游戏、验证 `nativeRender` 可 hook、验证 `setuid` 后 SELinux 行为、验证 `am start` 是否真把 UI 落进被 hook 的进程、验证导出 ContentProvider 的跨 UID 可达性、研究游戏完整性校验策略。上文所有"成立/可行/风险"判断都来自源码阅读，不含实机证据。

**2026-11-11 追加**：权限模型那一层已补实机实测（见文末补正）——在 Redmi `25102RKBEC`（Android 17）上，shell 身份 `setuid(10400)` 返回 `EPERM`，`run-as` 对非 debuggable 的正式游戏不可用，游戏私有目录直接 `Permission denied`。其余各项仍未验证。

补充一个待确认点：代码里没有 `attachApplication` 调用，`ActivityThread` 取自 `systemMain()`（其 `attach(true)` 不向 AMS 注册应用进程），按 AOSP 语义 `am start` 会另起一个 zygote 子进程。上游 README 声称路由启动可用，但"UI 是否真的落在被 hook 的那个进程里"必须由 A 轮实测回答。

## 2026-11-11 补正：`setuid` 这一关决定全篇

上游 `switchToUser`（`XposedHook.java:946-964`）做的事只有 `android.os.Process.setUid/setGid`，即裸的 `setuid(2)`；调用方是 Shizuku 起的 shell 进程（UID 2000）。**这一步能否成功，决定全篇成立与否。**

### 实测（Redmi `25102RKBEC`，Android 17 / SDK 37，`ro.build.type=user`、`ro.debuggable=0`、SELinux `Enforcing`）

目标机上 `com.hypergryph.endfield` 已安装，uid **10400**；BE 自身 `dev.betterendfield.next` uid 10222。

```
$ adb shell id
uid=2000(shell) gid=2000(shell) ... context=u:r:shell:s0

$ adb shell cat /proc/self/status | grep Cap
CapInh: 0000000000000000
CapPrm: 0000000000000000
CapEff: 0000000000000000
CapBnd: 00000000008000c0
```

`0x8000c0` 解出来是 bit6 `CAP_SETGID`、bit7 `CAP_SETUID`、bit23 `CAP_SYS_PTRACE`。**只看 bounding set 会误判成"能切"**：这三位的实际用途是留给 `/system/bin/run-as` 这类 setuid-root 二进制，普通 shell 进程的 permitted / effective 都是 0。

用 NDK 编了个静态探针（`temp/uidprobe/uidprobe.c`，arm64-v8a，未入库）在设备上以 shell 身份执行，目标取游戏 uid：

```
setgid(10400)  -> -1 errno=1(Operation not permitted)
setuid(10400)  -> -1 errno=1(Operation not permitted)
setresgid      -> -1 errno=1(Operation not permitted)
setresuid      -> -1 errno=1(Operation not permitted)
== after ==  uid=2000 euid=2000
opendir /data/user/0/com.hypergryph.endfield -> fail errno=13(Permission denied)
```

`run-as` 同样封死：`run-as: package not debuggable: com.hypergryph.endfield`，`dumpsys package` 里 `flags=0x0`（无 DEBUGGABLE），`ls -ld /data/user/0/com.hypergryph.endfield` 直接 `Permission denied`。

### 判定

- **零售量产机上 `switchToUser` 必然失败。** 上游失败只打一行 `Continuing as uid 2000` 就继续跑（`XposedHook.java:464-472`），不中止不报错；此时进程停在 `u:r:shell:s0`，写不了 `/data/user/0/<pkg>`，系统经 zygote 起的真游戏进程也 attach 不上（无有效 `CAP_SYS_PTRACE`，uid 也不匹配）。
- 先前按 AOSP 源码推断"现代 `adbd` 会把 `CAP_SETUID` 从 bounding set 摘掉"，**本机实测不成立**——这三位仍在 bounding set 里。真正的闸门是 permitted / effective 为 0，与 bounding set 无关。结论不变，原因按实测改写。
- 因此第一节第 1 条的"进程真实跑在游戏 UID 下"**是条件式**：只有在能拿到有效 `CAP_SETUID` 的环境（userdebug / eng，或另有 root）才成立。相应地，"能读游戏私有目录"这一优势在零售机上作废——而它原本是相对 Shizuku 唯一实质性的改进。这个分叉同时决定"自研同类框架"是否值得，见下一节。

## 自研同类框架的定位

结论：**代码量不是瓶颈，权限模型才是。** 需要自研的只有四块：`app_process` 引导（起进程、`handleBindApplication`、进 Looper）、classloader 链、hook 引擎（可直接用 Pine）、以及我们自己定义的控制通道（因为两端都归我们，不必实现 libxposed 的 remote prefs ABI，BE 侧反而更简单）。这四块合起来是可控的工程量。

但它整条链都压在 `CAP_SETUID` 上：拿不到就只能得到一个 shell 域进程（零售机已实测拿不到），拿得到（userdebug / 自编译 ROM）又没有免 root 的意义。所以**自研 app_process 框架不值得立项**。

真正能在量产机上免 root 跑起来的只有两类，都不依赖 Shizuku 的注入能力：

| 路线 | 机制 | 代价 |
| --- | --- | --- |
| **自修包（LSPatch 式）** | 把 loader 注入游戏 APK 的 `Application` 之前，重签安装；游戏由系统正常拉起，UID/进程形态零破绽 | 重签 → 不能覆盖升级、每次游戏更新重打、拆分包重签、登录与完整性校验风险 |
| **虚拟容器**（VirtualApp 家族，VirtualXposed 是这类方案跑 Xposed 模块的先例） | 游戏跑在容器进程内，容器伪造 UID/包信息，hook 由 Pine 类引擎完成 | Unity/IL2CPP 路径、图形、登录、反作弊全部高风险；容器自身要长期维护 |

Shizuku 在这两类里都只做辅助（启动、停止、拉日志、外部文件），与注入无关——这一点与 [Shizuku 篇](SHIZUKU_FEASIBILITY_20261007.md) 的结论一致。

## 2026-11-11 实机复刻：它自己就没跑起来

前提：同一台 Redmi `25102RKBEC`，**已 Root（KernelSU，`su` 返回 `u:r:ksu:s0`）**，`shizuku_server` 以 **root** 运行（`ps` 里 user=root），ShizuPosed 与 Shizuku 均已安装、管理器进程在跑。按本机条件，`setuid` 那关反而是通的——这是它能跑起来的最好环境。

**先看它自己的日志**（`/data/data/com.shizuposed.manager/files/.syscall_cache/logs/shizuposed.log`，root 读取）：

- `Shell base: /data/user/0/com.android.shell/files/.syscall_cache`，`XposedHook.dex` 已投放（410 KB）；
- `[MarkerCache] refresh: 0 marker(s)`，`hooked/` 目录为空；
- `launch.log` **0 字节**（最后一次写入时间 23:08）。

即：安装两天、服务一直在扫描进程，**从未有任何 app 被它成功 hook 过**。

**再按它的命令原样复刻**（`ShizuPosedService.java:2438-2458` 逐参数照抄，目标取 `dev.betterendfield.next` / uid 10222，以 root 执行）：

1. 原样执行 → 立即 abort：`Check failed: !location.empty() BOOTCLASSPATH and DEXOATBOOTCLASSPATH must not empty`。原因是它自己那句 `unset CLASSPATH; unset BOOTCLASSPATH`——本机 `adb shell` 的环境里 `BOOTCLASSPATH` 是有值的，`app_process` 靠它拼 `-Xbootclasspath`，unset 之后 ART 直接拒绝创建 VM。
2. 保留 `BOOTCLASSPATH` 再跑 → 换个位置 abort：`No pending exception expected: java.lang.ClassNotFoundException: dev.betterendfield.next`，崩在 `AndroidRuntime::startReg` 的 JNI 注册阶段。

两步都是在 `handleBindApplication` **之前**就死了，`setuid` 与 hook 安装根本没走到。

**边界**：这是复刻而非它自己的 spawn 路径——少了 `setsid -d` 前缀，环境变量也与它经 Shizuku 拿到的 shell 不完全一致。所以严格表述是"我没能让它在这台机器上跑起来"，不是"它在任何机器上都跑不起来"。但结合 `launch.log` 为 0 与 `hooked/` 为空，它与"宣称能力已兑现"的距离不是一点半点。

清理：设备端 `/data/local/tmp` 下的探针与脚本已删除，`temp/` 下的复刻脚本未入库。

### 它自己的 spawn 路径同样崩，而且被它自己判为成功

复刻之外，再抓一次**它自己发起**的 launch（用户在管理器里打开 `dev.betterendfield.next`），时间线对齐后没有任何解释空间：

| 时刻 | 来源 | 事件 |
| --- | --- | --- |
| 00:29:01.926 | 管理器日志 | `Force-stopping dev.betterendfield.next before routed launch` |
| 00:29:02.350 | 管理器日志 | `Launching ... binary=/system/bin/app_process64, uid=10222, spawn=/system/bin/setsid -d` |
| 00:29:02.485 | logcat `F/libc` | **`Fatal signal 6 (SIGABRT)` in `app_process64`**，`ppid: 1`，cmdline 里带 `-Djava.class.path=.../XposedHook.dex` |
| 00:29:02.589 | logcat `F/DEBUG` | `Abort message: 'Check failed: !location.empty() BOOTCLASSPATH and DEX2OATBOOTCLASSPATH must not be empty'` |
| 00:29:05.202 | 管理器日志 | `Bootstrap process up ... no marker after 2836ms — proceeding; app may have no applicable modules` |
| 00:29:05.300 | 管理器日志 | `UI opened for dev.betterendfield.next via component:dev.betterendfield.next/.MainActivity` |
| 00:29:05.406 | 管理器日志 | `Launch succeeded for dev.betterendfield.next` |

三件事同时成立：

1. 崩的是**它自己的** spawn（`ppid: 1`、`app_process64`、带 `-Dshizuposed.*` 参数），不是我的复刻——复刻之所以同样崩，是因为照抄了同样的命令。
2. 崩溃发生在其 spawn 后 135 ms；两秒半之后管理器因为"进程存在 + UI 打开了"就报 `Launch succeeded`。**它的成功判据里没有"hook 装上了"这一项**，只有一个 `hooked/<包名>` marker，而那次 `no marker after 2836ms`。
3. 界面上看到"应用/游戏正常起来了"，是随后 `am start` 的功劳：bootstrap 已死，AMS 照常 fork 一个 zygote 子进程，应用当然能正常启动。**"能起来"不能证明任何注入效果**，它本来就该能起来。

`hooked/` 目录自 2026-11-10 23:04 建目录起**一个文件都没有**——这台机器上从未成功 hook 过任何进程，包括用户手动启动的那次。

根因在它自己的命令行：`ShizuPosedService.java:2439` 的 `unset CLASSPATH; unset BOOTCLASSPATH`。新版 ART 在 boot classpath 为空时直接 abort（`Runtime::Create` 的 `!location.empty()` 检查），而 `app_process` 是靠 `BOOTCLASSPATH` 环境变量拼 `-Xbootclasspath` 的。补上该变量后崩溃点后移（见上一节第 2 步），说明后面还有第二层问题。

### 游戏进程的实测形态

00:29:10 起的 `com.hypergryph.endfield`（用户手动启动，界面正常）：

```
pid 27171   ppid 16792 (zygote64)   uid u0_a400(10400)
cmdline: com.hypergryph.endfield
SELinux: u:r:untrusted_app_34:s0:c144,c257,c512,c768
子进程:  31379  com.hypergryph.endfield:pushcore（同样 ppid=zygote64）
```

对照注入成功时**应该有**的形态：`ppid` 是 1（`setsid -d` 脱离后由 init 收养）或 shell 系进程，`cmdline` 里带 `-Djava.class.path=.../XposedHook.dex` 等一串 `-Dshizuposed.*`。实测四项全部落空：

| 判据 | 注入成功 | 实测 |
| --- | --- | --- |
| `cmdline` | 带 `XposedHook.dex` | 只有包名 |
| `ppid` | 1 或 spawn 上游 | 16792 `zygote64` |
| SELinux 域 | 随 spawn 来源，偏离 `untrusted_app` | `u:r:untrusted_app_34:s0`（标准应用域） |
| `hooked/<包名>` | 存在 | `hooked/` 自 23:04 建目录起为空 |

`logcat` 里也检索不到任何 `BetterEndfieldNext` 标签——运行时没有加载。结论：这次游戏是**标准 zygote 进程的普通启动**，与注入无关；bootstrap 在它启动前 8 秒就已 abort。
