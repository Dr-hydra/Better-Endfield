# Shizuku 免 root 方案可行性

日期：2026-10-07。审查对象为 Better Endfield Android 3.5.3 的代码与官方 Shizuku、Android/AOSP 文档、源码。目标场景是标准量产系统、未重打包的正式游戏和 adb 启动的 Shizuku；debug App、userdebug/eng 系统或已有 root 不作为免 root 通过证据。本次只做研究，未接入 SDK、修改设备、安装重打包游戏或验证游戏运行。

## 结论

**Shizuku 的 adb 模式不能直接替代 BE 当前的游戏进程注入框架。** 它可以提供启动、重启、日志和部分外部文件操作的辅助权限；BEM 模型、角色语音、PCUI、相机、MMD 和第三方原生模块仍需要代码先进入游戏进程。免 root 全功能应另立“游戏进程内加载器”项目，不能作为添加 Shizuku 依赖后即可完成的小改动。

当前 BE 运行时已经在游戏 UID 下工作，关键缺口是**如何在未修改的正式游戏中建立可用的进程内入口**。Shizuku 提供的独立 shell 服务没有该入口。这个结论由现有加载链与权限边界共同推导，未做目标手机上的注入实验。

## 当前 BE 的依赖链

| 环节 | 当前实现及代码位置 | Shizuku 替换范围 |
| --- | --- | --- |
| 框架进入游戏 | [`XposedEntry.java:18`](../../../../../android/app/src/main/java/dev/betterendfield/android/XposedEntry.java#L18) 继承 `XposedModule`；`:27` 的 `onPackageReady` 和 `:38` 的 `Application.attach` Hook | 不提供等价入口 |
| Java Hook 与帧泵 | [`XposedEntry.java:93`](../../../../../android/app/src/main/java/dev/betterendfield/android/XposedEntry.java#L93) Hook `UnityPlayer.nativeRender`，成功首帧加载，之后在 Unity 线程执行命令 | 独立 UserService 没有游戏 ClassLoader、帧回调或游戏对象 |
| 配置与资源跨进程分发 | [`FrameworkSettings.java:137`](../../../../../android/app/src/main/java/dev/betterendfield/android/FrameworkSettings.java#L137) 的 `publishBem`，`:182` 的远程偏好发布；[`XposedEntry.java:63`](../../../../../android/app/src/main/java/dev/betterendfield/android/XposedEntry.java#L63) 的 `openRemoteFile` | 可以设计另一套 Binder/FD 传输，但仍须已有游戏内接收端 |
| 游戏私有副本 | [`BemInstalledResources.java:27`](../../../../../android/app/src/main/java/dev/betterendfield/android/BemInstalledResources.java#L27) 使用游戏 `Context.getFilesDir()`；第三方模块在 [`ThirdPartyRuntimeMaterializer.java:26`](../../../../../android/app/src/main/java/dev/betterendfield/android/ThirdPartyRuntimeMaterializer.java#L26) 同样落到游戏私有目录 | shell 不能直接写入这个目录；进程内接收端能以游戏身份写入 |
| JNI/Unity 命名空间 | [`RuntimeBootstrap.java:87`](../../../../../android/app/src/main/java/dev/betterendfield/android/RuntimeBootstrap.java#L87) 定位 BE SO，`:112` 在目标 ClassLoader 下加载，`:133` 使用 `Runtime.nativeLoad` | 在 UserService 加载 SO 只进入服务进程，不会进入游戏 |
| IL2CPP 与原生 Hook | [`runtime.cpp:114`](../../../../../android/app/src/main/cpp/core/runtime.cpp#L114) 连接本进程已加载 IL2CPP；[`hook_broker.cpp:25`](../../../../../android/app/src/main/cpp/core/hook_broker.cpp#L25) 安装 Dobby Hook | 文件访问、启动授权不能替代进程内调用与内存修改 |

当前安卓不是“BE 调用 `su` 再 ptrace 游戏”的实现。特权部署/注入能力来自兼容框架；BE 已注入后的逻辑使用游戏自身权限。构建明确只接 [`libxposed API/service 102`](../../../../../android/app/build.gradle.kts#L262)，[安卓说明](../../../../../android/README.md#L64) 也注明单装 APK 不会注入。

## 权限边界

### adb、root、Sui 与 UserService

官方定义 `Shizuku.getUid()`：adb 启动为 UID **2000**，root 启动为 UID **0**。UserService 可以跑 Java/JNI，但它是以该身份运行的**另一进程**，并非目标游戏进程；其 `Context` 也不能等同普通 App 的完整 Context。Sui 是 Magisk 模块，需要已有 root 环境，不能作为免 root 方案。非 root Shizuku 在重启后要重新通过 adb 启动；Android 11 起可用无线调试，较早版本需要电脑。[Shizuku 官方 API 指南](https://github.com/RikkaApps/Shizuku-API#guide)

Binder 转发更换的是发往系统服务的请求身份，不会把 BE 主进程改成 root，也不会把其他 App 变成可加载 BE 的宿主。[Shizuku 官方原理说明](https://github.com/RikkaApps/Shizuku#how-does-shizuku-work)

### ptrace、进程内存与 SELinux

Linux `__ptrace_may_access` 要求调用方与目标的 UID/GID 满足匹配条件，或具有目标命名空间的 `CAP_SYS_PTRACE`；随后还检查 dumpable 状态并执行 LSM 检查。shell UID 与游戏 UID 不同，普通 adb 模式没有获得这些能力的承诺，因此无法凭 Shizuku 授权推断可附加正式游戏。[Linux 内核 ptrace 实现](https://github.com/torvalds/linux/blob/master/kernel/ptrace.c)

AOSP [`shell.te`](https://android.googlesource.com/platform/system/sepolicy/+/refs/heads/main/private/shell.te) 只在这里显式允许 `shell self:process ptrace`；这不是 shell 对任意 App 的许可。还需结合默认拒绝的 SELinux 和具体 ROM 策略判断，不能只凭 UID 或单条规则。SELinux 对 root 进程也适用，UID 0 本身不等于所有操作都可做。[Android SELinux 官方说明](https://source.android.com/docs/security/features/selinux)

`run-as` 只接受 installed、debuggable 的应用，源码显式拒绝非 debuggable 包。正式客户端若不可调试，Shizuku 不能利用 `run-as` 获得其身份；即便某测试包可调试，也不证明正式客户端路线成立。[AOSP run-as 源码](https://github.com/aosp-mirror/platform_system_core/blob/master/run-as/run-as.cpp)

### 私有目录与 Android/data 要分开判断

- `/data/user/0/<游戏包>`（常见 `/data/data/<游戏包>`）：Shizuku 官方明确 adb shell 不能访问其他 App 的私有数据。当前 BE 的 BEM、第三方 SO、MMD 和诊断缓存主要在游戏私有目录，因此不能用 shell 文件复制替代。[Shizuku adb/root 权限区别](https://github.com/RikkaApps/Shizuku-API#differents-of-the-privilege-betweent-adb-and-root)
- `/storage/emulated/0/Android/data/<游戏包>`：属于外部存储，普通 App/SAF 从 Android 11 起受限制。shell 是不同身份，AOSP FUSE 的 `is_app_accessible_path` 对低于 App UID 起点的调用方有单独处理，因此有外部文件管理的可行空间；实际列目录、读、创建、重命名仍需按 ROM、系统版本和挂载视图分别验证，不能承诺全部手机可写。[Android 11 存储规则](https://developer.android.com/about/versions/11/privacy/storage)、[AOSP FuseDaemon 源码](https://android.googlesource.com/platform/packages/providers/MediaProvider/+/refs/heads/master/jni/FuseDaemon.cpp)
- 外部资源可写不表示 BEM 自动生效：BEM 是 BE 运行时解析的包，游戏本身并不提供已确认的 BEM 加载协议。直接覆盖游戏资源包还需单独研究格式、索引、校验和更新，无法保留已有全部运行时能力。

## 可迁移功能与架构选择

| 功能/路线 | 判断 | 边界 |
| --- | --- | --- |
| 普通设置、包解析、创意工坊下载、BE 自己的文件管理 | 无需 root 的部分可保留 | 当前安装发布流程仍等待框架服务；若提供纯离线管理模式，需要拆开解析和发布职责，不必为此接 Shizuku |
| 启动、强制停止后重启、进程/版本检查 | 适合可选 Shizuku 辅助层 | 系统 API 和具体权限逐项检查，不能获取游戏内模块状态 |
| logcat、dumpsys、诊断导出 | 适合辅助层 | 可读系统日志不表示可读游戏私有诊断文件；私有文件需游戏内合作端导出 |
| Android/data 文件操作 | 条件可行 | 独立实机验证与限定目录，不声称完成运行时注入 |
| BEM/语音/相机/PCUI/MMD/第三方 SO 与热切换 | 仅 Shizuku 不可行 | 全部依赖游戏内运行时；UserService 可作为命令传输端，不能成为执行端 |
| Shizuku root 模式或 Sui | 可以作为已有 root 设备的权限代理研究 | 不属于免 root；仍需要注入入口和 SELinux/框架适配 |
| 重打包游戏 + 嵌入 BE 加载器 | 有理论可行性，尚未验证 | 改写游戏 APK/拆分包并重新签名；入口、libxposed 102、命名空间、远程文件和游戏更新链都要适配 |
| 虚拟容器 | 另立研究，暂不判定支持 | 只有容器确实控制游戏加载链才可能承载运行时；ARM64、Unity、文件路径、输入及登录兼容均未验证，Shizuku 本身不提供容器 |

启动/停止、输入与存储权限存在于 AOSP Shell Manifest，但会随系统版本变化；未来辅助层应做能力探测，不能以“已授权 Shizuku”替代逐操作检查。[AOSP Shell Manifest](https://github.com/aosp-mirror/platform_frameworks_base/blob/master/packages/Shell/AndroidManifest.xml)

重打包路线的公开先例是 LSPatch，其官方描述就是向目标 APK 插入 dex 和 SO。原仓库已归档；它不能直接证明当前 BE 的 libxposed 102 及远程偏好/文件服务可用，应先在自有测试 App 上验证这些接口，而非声称现有模块安装即可兼容。[LSPatch 官方仓库](https://github.com/LSPosed/LSPatch)

重签后的包无法作为原签名游戏的正常覆盖升级；通常需替换安装或改包名，数据保留、登录、支付、资源更新和拆分 APK 都成为独立问题。[Android 应用签名规则](https://developer.android.com/studio/publish/app-signing#considerations) 完整性服务可以识别包名/证书差异，但本次没有证据证明终末地使用哪种校验或会如何处理，不能推断一定通过、一定失败或确定的账号后果。[Play Integrity 应用完整性字段](https://developer.android.com/google/play/integrity/verdicts#application-integrity-field)

## 建议的下一步与通过条件

1. 保留现有框架后端。只有用户确实需要一键重启/日志导出/外部文件操作时才做可选 Shizuku 辅助层，名称明确为“启动与诊断辅助”，不宣称“免 root 模组”。
2. 若继续全功能免 root 研究，先使用自有可签名测试 App，验证进程内入口、游戏式 ClassLoader/JNI 命名空间、首帧回调、配置/FD 传输和 ARM64 原生 Hook。先证明加载器可用，再讨论目标游戏适配。
3. 在授权的测试环境记录 adb/root UID、SELinux context、服务死亡/重连、设备重启后的启动要求，并分别验证内部目录和 Android/data 的读写。不得因单台 debug/userdebug 设备成功就标记正式手机普遍支持。
4. 完整路线必须验证 BE UI 与游戏内模块同启停、BEM 导入/启用/热切换、模型与语音、第三方 SO、相机/PCUI，以及重签安装/更新/登录。未完成前维持“研究中”，不进入正式 APK 的承诺功能。

若只以“不 root、不修改正式游戏包、不使用可控制游戏加载的容器”为条件，目前没有从 Shizuku 官方机制和 BE 现有架构推导出的全功能路径。
