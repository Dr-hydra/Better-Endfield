# 会客室媒体屏首版实现（2026-10-11）

工作区：`G:\Better Endfield`。分支：`feat/guest-room-media-screen`，基于 `40560f4c`。
本记录描述分支内的试验实现。PC 实机日志及游戏窗口截图已验证窗口画面投屏；
图片、视频、暂停/恢复和手机仍需完整实机验收，不视为正式发布。

## 实现范围

| 功能 | Windows | Android |
| --- | --- | --- |
| PNG/JPEG、图片轮播 | 已接入 | 已接入，管理器导入后复制到游戏私有目录 |
| MP4 视频 | Unity VideoPlayer 输出到 RenderTexture | 同一实现；运行时要求客户端保留 VideoPlayer 接口 |
| 视频声音 | 既有 LocalMusic 接口，Media Foundation 解码 MP4 音轨并通过 Wwise 输出 | 既有 LocalMusic JNI 接口，通过 MediaPlayer 输出 |
| 本地窗口画面 | 独立辅助进程运行 Windows Graphics Capture，通过共享内存传递最新帧 | 未实现 MediaProjection |
| 窗口鼠标／键盘操控 | 热键进入试验模式，按屏幕网格 UV 定位并由辅助进程投递定向输入 | 未实现 |
| 电脑窗口传往安卓 | 未实现发送端 | 未实现网络接收端 |

窗口来源当前仅捕获画面。既有音频接口接受本地文件，实时窗口音频需要另做采集及 PCM 输入适配。
MP4 无音轨或音轨无法解码时，启用声音会使该会话失败，可关闭“播放视频声音”试验画面。

Windows “会客室媒体屏”页面提供来源、文件/窗口选择、热键、输出大小、轮播间隔和视频声音配置。
Android 在增强功能中导入文件，游戏悬浮窗提供开始/恢复原画、停止、暂停/继续和按秒跳转。
Android 文件和配置在游戏启动时准备，修改来源后需重启游戏。启用开关不会自动开始替换。

## 接管与释放

默认主键盘 `-`（0 后面的键）启停，暂停/继续快捷键默认未设置。两个快捷键都可以自定义，支持
Ctrl、Alt、Shift、Win 组合；Windows 点击输入框后按键录入，Backspace/Delete 清空，INI 保存为 NONE。
旧原型的默认 F8 配置会迁移为 `-`；配置改用 `toggle_hotkey`、`pause_hotkey` 文本，沿用现有统一按键编码。
窗口列表刷新只用于来源选择。
只在收到按键或按钮命令后调用 `GameObject.Find`：默认精确尝试
`ReceptionRoomImagePoster` 和 `ReceptionRoomImagePoster(Clone)`；高级设置可指定准确层级路径。
不遍历所有 Renderer，不在进房间或资源重新加载后自动绑定。

绑定要求目标存在 `ReceptionRoomCharPosterHelper`，读取上下两个 Renderer 的实际 `sharedMaterial`，
保留原材质并创建独立副本。图片屏使用 `_MainTex`，与 `SpaceShipReceptionRoomImagePoster._EnqueueImagePoster`
传给 Helper.Enqueue 的属性一致；不使用角色海报的 `targetPosterSlot.propertyName`（本次实机为 `_EmissiveMap`）。
先确认材质存在 `_MainTex`，再把画布写入副本并将副本赋给 Renderer。游戏重设材质时重新赋回副本。
Enqueue/Dequeue 拦截仅作用于已绑定的那一个 Helper，不依赖调用线程与画面上传线程相同。
Windows 还定向拦截该 Helper 对应的图片轮播控制器的入队、出队和更新，不影响其他会客室海报。
失败、显式停止、配置变化、场景对象释放都取消当前会话和旧读取请求。
停止时仅当 Renderer 仍使用本模块的材质副本，才恢复保存的原材质，随后销毁模块拥有的副本和纹理。
游戏失去场景对象后不会重新绑定，用户需再次触发。Android 退后台立即通过既有音频接口暂停声音，
VideoPlayer 的暂停及恢复由随后可用的帧回调处理，不在 Activity 回调中调用 Unity。

文件读取在工作线程完成；解码、纹理上传、材质变更和 Unity 对象销毁在帧线程完成。
首次写入后同时读取 Renderer.sharedMaterial 和 Material.GetTexture 校验实际绑定，
Receiver 日志记录原图片纹理名称，第一次窗口帧记录上传字节数及非黑像素数。
默认 1920×1080、窗口 60 FPS；分辨率提供 720p、1080p、1440p、4K，窗口帧率提供 30/60/90/120 FPS，
两者都保留自定义。输出允许单边 64–3840、总像素不超过 3840×2160、帧率 1–120。
Windows 捕获采用最新帧缓冲，设置的 FPS 是采集上限；实际刷新取决于来源变化、捕获吞吐和游戏帧率，
尚未测量持续帧率、延迟、功耗或对游戏帧率的影响。
桌面控制器在启用窗口来源时启动闲置的 MediaCapture 辅助进程；仅游戏中的显式启动请求会开始捕获。
辅助进程使用当前用户会话内的命名共享内存和互斥量，与游戏交换 RGBA 帧、请求代次、状态及心跳。
客户端丢失、辅助进程丢失、来源关闭、请求被替换或帧损坏都结束当前会话，不会重新绑定。
启动窗口投屏前保持控制器打开；已开始的捕获在控制器退出后继续到当前游戏会话停止。
图片限制 32 MiB、8192 像素单边和 16777216 总像素，解码前检查 PNG/JPEG 编码尺寸。
Android 导入的视频限制 512 MiB，索引最多 256 个文件；当前管理页尚无媒体库清理入口。
音频接口为单通道：视频和 MMD 同时使用时会竞争所有权，检测到失去音频所有权后停止媒体屏。
播放器准备超时 30 秒。静音结束检测、循环、时钟同步和实际解码兼容性仍需实机确认。

## 验证与产物

已完成 Windows 原生 Layout（含新模块和 Music）、桌面 UI Release 构建；Android arm64 原生构建、
Debug APK 和 Lint 构建。UI 编译无警告、错误；Android Lint 为 0 errors、173 warnings，包含已有页面的提示。

模拟宿主 CTest 覆盖手动启动、目标/纹理分配失败回滚、仅一个 Helper 被接管、恢复材质、
其它纹理所有者、离场不重绑、取消读取、配置变更、图片尺寸，以及视频音频等待、同步时钟、暂停、跳转、循环和音频所有权丢失。
Windows 配置程序通过既有 5 项、媒体配置 6 项与快捷键 3 项检查；Android JVM 缓存检查通过；测试清单 17 项检查通过。
快捷键模拟覆盖 Ctrl 修饰键要求、长按防重复、组合键启停/恢复、Alt+P 暂停、持键修改配置不自启，以及 NONE 保留显式按钮控制。
在部署到 `E:\Better Endfield` 的控制器中，实际输入 `Ctrl+-`、`Ctrl+Shift+-`、主键盘 `-`、`Alt+P` 和 Backspace 均通过录入检查。
文本框使用 PreviewKeyDown 捕获，避免 Ctrl+- 被控件默认处理为软连字符；测试完成后显示值恢复为 `-` / F9。
后续 PC 实机日志已验证 IL2CPP 合约解析、手动绑定及窗口帧上传；
17:38 后游戏窗口截图已确认浏览器画面显示在会客室屏幕上。Unity 视频解码和手机仍待验收。

Gradle `testDebugUnitTest` 未发现测试：仓库现有 Java 测试使用 `main` 入口，不是 JUnit。
本次 Android 媒体文件检查使用独立 JVM 入口执行，未把 Gradle 的该结果记为测试通过。

- Windows 试验目录：`build/next/media-screen/windows-app/`，启动器 `BetterEndfieldNext.exe`，包含编译 UI 和原生布局；未捆绑 BEM 转换工具。
- 原生布局：`build/next/media-screen/windows/stage/Release/`。
- Android APK：`build/next/android/gradle/app/outputs/apk/debug/app-debug.apk`。
- 构建及测试日志：`build/next/media-screen/`。

构建 Android 时使用独立本机覆盖 `config/workspace.media-screen.local.json`，指定现有 Python 3.12、
JDK 21 和 SDK/NDK，避开迁移后失效的旧 Python 虚拟环境。原本机配置没有改动。未计算产物哈希。

## 游戏内验收顺序

1. 先选小尺寸 PNG，进入会客室，确认未按键时仍为游戏原画。按主键盘 `-`（Android 按开始）检查目标名、纹理属性、Receiver 的材质/Shader 及首次写入校验日志；无法绑定则记录实际对象路径，不启动常驻扫描。
2. 检查画面比例、上下目标、材质发光和 UV 方向；再次触发检查原画恢复，离场后重新进入检查未自动播放。特别观察原切换动画是否仍影响已绑定 Renderer。
3. 试验轮播和静音 MP4，检查暂停、结束、循环、跳转；Android 先验证客户端实际可解析 VideoPlayer。
4. 启用 MP4 声音，检查独占通道和长时间音画漂移，再试验 Windows 窗口关闭、缩放、最小化、切换及性能。

用户已多次启动游戏测试，模块在当前测试环境加载；尚未安装 APK、提交 Git commit 或发布版本。
Android 应用捕获、局域网串流、实时窗口音频为后续工作。

## 2026-10-11 首次游戏日志修复

用户实机运行日志在 15:12 至 16:42 多次出现
`Missing media contract: MaterialPropertyBlock.GetTexture`，紧接着宿主拒绝加载媒体屏模块。
此时模块未安装帧钩子，也没有处理按键或尝试屏幕绑定；不能根据这一结果判断窗口捕获是否可用。
当前 PC 导出的 MaterialPropertyBlock 保留 SetTexture，但没有 GetTexture。

移除这一必需合约，改为拦截 Renderer.SetPropertyBlock，记录已绑定 Renderer 的最后写入者。
停止时恢复由本模块写入的属性块，其他 Renderer 的写入不会改变会话归属。
启动回归覆盖没有 GetTexture 的客户端，以及属性块写入钩子的安装；运行时回归覆盖外部写入后的恢复行为。
测试程序新增 `--contracts` 参数，可导出生产 Contracts 实际请求的方法与字段签名。
对照当前 PC 游戏导出检查 49 个方法、4 个字段的签名，没有缺失；嵌套字段的声明由导出单独列出，静态检查不能代替运行时嵌套类型解析。
Windows、Android arm64 原生编译及 CTest 通过。修正版原生模块已再次部署到 `E:\Better Endfield`。
当前运行中的游戏仍使用本次启动时的加载结果，需要重启游戏后再次进入会客室按 `-`；尚未确认修复后的实际绑定和画面效果。

## 2026-10-11 游戏进程窗口捕获修复

16:57 实机日志确认媒体屏成功加载，三次按键均绑定到
`ReceptionRoomImagePoster(Clone)`，属性为 `_EmissiveMap`，两个 Renderer。
紧接着 WGC 返回 `You do not have permission to capture the given window.`。
选中窗口为 Edge，HWND 787732、PID 6784，显示保护标志为 0。
使用同一 WGC 实现在游戏外捕获该窗口，成功取得 320×180 RGBA 帧（230400 字节），
55641 个非黑像素。说明当前失败发生在游戏进程的捕获调用环境中；尚未确定具体是哪项进程权限或策略。

将 WGC、D3D11 设备和画面读回迁到 `runtime/BetterEndfieldNext.MediaCapture.exe`，
由桌面控制器以自身环境启动，游戏内模块仅发送显式请求并接收画面。
不改变游戏令牌、系统隐私设置或 Edge 设置。命名通道限本地会话；默认对象权限来自创建者令牌。
初版输出限定 1920×1920、最多 30 FPS（后续已扩档，见末节），锁内复制帧；请求代次和客户端身份检查防止旧停止命令取消新会话。
15 秒没有帧或 3 秒没有心跳会报错并停止。WGC 错误现在包含失败阶段和 HRESULT。
辅助进程的单例、空闲启动及退出受控制器生命周期管理；已激活的捕获可在控制器退出后继续，直到客户端释放或失去心跳。

Windows 原生模块、辅助进程和 UI Release 构建通过，UI 0 警告、0 错误。
CTest 两项通过，新增 IPC 检查显式启停、帧完整性、重启隔离、畸形帧、错误传播、替代请求、其他客户端归属及辅助进程失联。
实际运行辅助进程和独立接收端，通过新的 IPC 链路取得同一 Edge 窗口的 RGBA 画面。
游戏内使用辅助进程后的纹理上传和显示仍待实机重试确认。
已更新部署到 `E:\Better Endfield`，同时保留构建目录中的完整试验布局。
部署后的控制器自动启动 `runtime/BetterEndfieldNext.MediaCapture.exe`，闲置时检查为
active=0、client_pid=0；通过这一实际部署的辅助进程再次取得 Edge 画面，随后接收端停止会话。
登记的 media_screen.runtime runner 也通过两项 CTest；凭据记录文件尺寸和时间，没有计算产物哈希。

## 2026-10-11 材质实例与重复采集修复

17:11 至 17:12 的新日志确认独立采集请求成功，第二次请求已到达 `playing:window`，
但用户仍未看到新画面。同时，第一次请求中的辅助进程在 17:11:54 崩溃，控制器随后自动重启它。

对当前 `GameAssembly.dll` 的 ReceptionRoomCharPosterHelper.SetMaterialTexture（RVA 0x0627F47C）
反汇编发现：它调用 `_GetInstanceMaterial`（0x0627F84C），然后直接调用 Material.SetTexture(String, Texture)
（0x03F38340）。原型的 MaterialPropertyBlock 写入与游戏自己的实现路径不一致。
改为通过 Helper 取得同一材质实例，并调用 Helper.SetMaterialTexture；保存/恢复的是这一材质上的纹理属性。
首次设置后用该材质的 GetTexture 确认指向画布，避免仅凭 API 调用未抛异常就报告成功。
日志新增 Renderer 名、材质名、Shader 名以及首次窗口帧的字节数和非黑像素数。

WER 报告异常码 0xC0000005、辅助进程偏移 0x5862。小型转储表明崩溃点调用
Windows.Graphics.Capture 的缓存接口，虚表地址已不属于任何加载模块。
测试曾开启并停止一次采集，空闲时最后一个工作线程释放 COM，随后再次启动访问旧缓存。
这与微软记录的 [C++/WinRT 工厂缓存引用已释放模块问题](https://devblogs.microsoft.com/oldnewthing/20211105-00/?p=105878) 一致。
辅助进程主线程改为维持 MTA，所有会话销毁后、进程退出前才清理工厂缓存并释放 COM。

修复后的辅助进程连续启停 16 次真实 Edge 捕获，交替输出 320×180、1280×720，均取得有效非黑帧；
同一进程重复启停没有崩溃。新的原生合约与当前 PC 导出核对：50 个方法、4 个字段，没有缺失。
Windows 及 Android arm64 原生编译通过。材质路径的模拟回归增加未实际写入纹理时的失败检查，
验证只修改屏幕材质纹理、保留 Renderer 属性、停止时恢复原纹理并保留其他纹理所有者。
游戏内最终显示仍需实机验收，不能把上述结果等同于已看到投屏画面。

## 2026-10-11 图片屏纹理属性纠正

17:22 至 17:23 日志显示 Helper 材质缓存的 `_EmissiveMap` 已指向画布，
窗口帧为 3686400 字节、约 890440 个非黑像素，但用户确认屏幕仍在两张游戏原图之间轮播。
因此采集成功和材质 GetTexture 读回成功不足以确认实际显示。

进一步检查当前 `GameAssembly.dll` 的图片轮播方法：RVA 0x05D1707A 读取属性字符串，
0x05D17087 将其作为第三个参数传给 ReceptionRoomCharPosterHelper.Enqueue。
字符串槽 RVA 0x0D0DB748 中的 metadata usage token 为 0xA000E299，对应 string literal 索引 29004；
当前 `Endfield_Data/il2cpp_data/Metadata/global-metadata.dat` 解出值为 `_MainTex`。
Helper.Enqueue 将该参数放入临时 TextureAffectTarget，而不是读取供角色海报使用的 targetPosterSlot。
之前从后者读取 `_EmissiveMap`，实际写到了另一纹理槽，这是已确认的实现错误。

本次改为显式写图片屏的 `_MainTex`，增加 Material.HasProperty 检查；使用材质副本直接绑定 Renderer，
首次同时校验副本归属和纹理，保留原材质及其他纹理槽。Windows 对已绑定 Helper 对应的原轮播控制器
进行定向抑制，并记录第一次抑制；其他控制器及停止后的轮播正常放行。
这些改动只在手动触发后接管，未增加自动搜索或自动绑定。

Windows、Android arm64 原生编译通过，CTest 两项通过。新增回归以不同纹理槽模拟 `_MainTex` 和
`_EmissiveMap`，确认修改的是图片槽、发光槽保持原值、材质缺少图片槽时失败回滚；另覆盖实际材质重设、
停止恢复、其他材质所有者，以及跨线程 Helper 调用和控制器归属。当前 PC 静态合约审计为
55 个方法、3 个字段，0 缺失。Android 尚未实机验证。

17:37 修正版模块已部署到 `E:\Better Endfield` 并同步试验布局，旧模块备份保存在
`build/next/media-screen/main-texture-deploy-20261011-173704/`。未计算产物哈希。
控制器及采集进程保持运行。登记的 media_screen.runtime runner 通过两项 CTest，
本轮结果目录为 `build/tests/runs/20261011T093808596624Z-2480/`。

用户随后启动游戏（PID 24728）。17:38:19 绑定 photo_02，原 `_MainTex` 为
`pic_5_chr_0035_liino`；日志确认 Renderer 已绑定模块材质并使用 `_MainTex` 画布。
17:38:20 收到有效窗口帧，17:38:21 记录原轮播被抑制。
读取当前前台游戏窗口的截图 `build/next/media-screen/game-screen-current.png`，
已直接看到会客室屏幕显示 Edge 的 YouTube 页面及正在播放的画面，确认窗口投屏实际显示。
尚未在此次运行验证暂停、停止恢复、长时间稳定性或其他媒体来源。

## 2026-10-11 BGM 开关、空暂停快捷键与高档位

用户接受窗口声音由来源应用直接输出，要求增加窗口投屏时暂停游戏 BGM 的开关、清空暂停快捷键，
并为分辨率和 FPS 提供较高档位及自定义。Windows 设置页已提供对应控件；当前部署采用
1080p/60、窗口 BGM 暂停开关开启、暂停/继续键为空，启停键仍为主键盘 `-`。
已有其他自定义快捷键仍可保存，不对所有用户的显式绑定做全局迁移。

BGM 控制通过 Music 模块新增的命名能力 `BetterEndfieldNext_GetMusicControlApiV1`，独立于本地文件音频所有权。
显式开始窗口投屏且开关开启时申请暂停 token；Music 的帧回调暂停原生 BGM 及 OmniMix 替换输出，
不打开音频文件、不接入窗口音频、不静音来源应用。当前本地文件音频仍优先使用已有播放通道。
暂停画面不释放 BGM 请求，因为来源窗口的声音仍在播放；停止、离场、配置变化、采集失败和关停均释放请求。
多个请求者须分别释放 token，过期或重复释放不会取消其他请求。恢复遵守现有游戏自身暂停状态。
仅需要窗口 BGM 控制时会加载 Music，但不会开启本地音频播放或 OmniMix 替换。

分辨率档位为 1280×720、1920×1080、2560×1440、3840×2160；帧率为 30、60、90、120，均可切换自定义。
Android 文件来源页面同步分辨率档位与自定义，暂停键默认清空；未添加尚不可用的窗口捕获或窗口 FPS 控件。
Android 保存现有 FPS 值，视频仍由其原始帧率及 Unity 播放器驱动。

采集上限扩至 4K 总像素和 120 FPS；共享帧最大 33177600 字节。IPC 的布局与端点升级为 V2，
控制器、采集进程和游戏内接收端必须成套更新。活跃采集的发送/接收轮询从 10ms 改为 3ms，
采集节流改用精确秒周期，避免旧限制挡住高档位。空闲辅助进程仍不采集。

Windows 模块（MediaScreen、MediaCapture、Music）、UI、Android arm64 原生与 Debug APK 编译通过。
UI 编译 0 警告、0 错误；Android 本轮原生编译无警告。两项 CTest 及登记的 runtime runner 通过，
包括完整 4K 帧首尾字节验证、高档位/空键解析、BGM 请求归属、关闭开关、暂停画面、失败回滚和离场释放。
配置检查确认窗口 BGM 控制的 Music 依赖与本地文件音频依赖分开，4K/120 不再被旧模型上限截断。
本轮 runner 结果：`build/tests/runs/20261011T100208130298Z-36208/`。

真实 WGC 与 V2 接收端连续 6 次启停，分别以 1080p/60、1440p/90、4K/120 配置重复获取有效帧，
确认各自 8294400、14745600、33177600 字节完整输出。这里验证了配置接受和帧完整性，未测得持续 120 FPS。
原选 Edge 当时已最小化，试验 10 秒没有首帧；保留来源窗口状态，改用未最小化的控制器窗口完成上述采集。

18:00 已部署到 `E:\Better Endfield` 并重开控制器，同步 `windows-app` 试验布局；备份为
`build/next/media-screen/presets-deploy-20261011-180005/`。在部署后的真实设置页上，4K/120 的选择与保存、
自定义输入显隐、BGM 关闭保存和空暂停键均通过；检查后恢复 1080p/60、BGM 暂停开启。
本轮尚未进行游戏内听感验收，也未安装 Android APK。未计算产物哈希。


## 2026-10-11 视频暂停键与窗口操控试验

用户澄清 Ctrl+- 用于视频暂停／继续，Alt+- 用于窗口操控，主键盘 - 保留投屏启停。
Windows 设置页提供三个独立、可手输及捕获组合键的配置项；视频暂停项仅在视频来源显示和响应，
窗口操控项仅在窗口来源显示和响应。默认及本机部署配置改为上述三个键。
媒体屏在本模块中采用精确修饰键匹配，并在同一基础键释放前保留消费状态，避免 Ctrl／Alt 先松开后
仍按住 - 导致投屏启停。其他模块的热键语义没有修改。

窗口操控只在已手动投屏且已有有效帧后进入。入口读取绑定 Renderer 的 MeshFilter、可读网格的
顶点／UV／三角形和材质纹理缩放／偏移，通过当前 Camera 的屏幕射线和 Transform 求交插值。
捕获进程随帧提供原始尺寸及画布中实际图像矩形，点击换算考虑黑边和上传 Y 翻转；来源窗口位置用
DWM 扩展边框和客户区／子窗口坐标换算，辅助进程使用每显示器 DPI 上下文。黑边、屏幕之外、窗口
非客户区不转发点击。按下后移动出屏幕的释放使用最后有效位置，避免卡住鼠标按钮。
网格不可读、接口缺失或窗口线程不符时只拒绝操控，保留已有画面与 BGM 暂停租约；未猜测屏幕 UV。

操控期间以独立 InputManager 光标请求及 Cursor 状态显示并解锁鼠标，通过游戏窗口消息入口抑制
键盘／鼠标／Raw Input，InputManager._CheckInput 及 Rewired 常用按钮／轴查询阻止相同输入驱动
角色和视角。退出 Alt+-／Escape、失去游戏前台、停止投屏、来源关闭／最小化、配置变化、队列满或
发送失败时退出并释放。未对其他模块自行轮询系统热键的行为做全局改写。

辅助进程接口升级为 V3，UI、共享内存及测试端点同步更新，V2 不复用。输入由显式操控代次和当前
捕获请求隔离，校验 HWND／PID、子窗口归属及前台客户端；保持各按键和鼠标按下的接收窗口，在
操控退出、客户端失联、代次变化和辅助进程退出时向同一接收窗口释放。没有切换来源窗口前台或
发送全局 SendInput。输入消息用有超时的 SendMessageTimeout 顺序调用，绕过目标消息循环的
TranslateMessage；字符由游戏已有的 Unicode CHAR 事件传递一次，避免 KEYDOWN 被目标再次
翻译产生重复文本，并保持方向键和字符的顺序。后台修饰键、程序特有 Raw Input 和 IME 仍需逐应用
实测；此版不声称任意应用都支持。

验证：Windows 原生、Windows UI 及 Android 共享原生编译通过；三项 CTest 覆盖热键互斥、Ctrl+-
视频双通道暂停、抑制恢复、UV／黑边／Y 翻转、控制失败保留投屏、IPC 代次与队列满退出、真实
Win32 EDIT 的后台文本／点击／滚轮／释放、前台保持及 PID／窗口关闭拒绝。UI 配置测试通过；
当前 PC 合约审计为 91 个方法、3 个字段，0 缺失。
另用独立 Edge 测试页验证：点击计数 1，输入 BEtest、左箭头、X 得到 BEtesXt，滚轮计数 1，
前台保持。结果存于 build/next/media-screen/input-browser-result.json。测试页启用浏览器后台渲染
参数，验证的是消息输入链路，不代表最小化来源可捕获。所有测试窗口已关闭。
游戏内新操控尚未实测，尤其需要确认实际屏幕网格可读、映射位置和角色／视角抑制；Android 未增加
窗口操控，也未安装新 APK。未计算产物哈希。


18:52 已将最终试验版部署到 E:\Better Endfield 并同步 windows-app 布局，控制器重开为 PID 33488，
采集辅助进程为 PID 13648。备份为 build/next/media-screen/control-deploy-20261011-185259/。
18:53 登记 runner 的三项 CTest 通过，结果目录为 build/tests/runs/20261011T105327048920Z-23348/。
部署后的真实设置页验证三个默认键及自定义 Ctrl+Alt+F12／Ctrl+Shift+F11 的保存，检查视频来源下
暂停键和窗口来源下操控键的显示；随后恢复窗口来源、-／Ctrl+-／Alt+-，1080p/60 和原有 BGM 设置。
共享内存读回辅助进程协议 V3，配置保存及进程启动正常。游戏尚未启动进行新操控验收。

## 2026-10-11 单指针与持续悬浮修复

真实游戏已确认屏幕表面可读（各 4 个顶点、2 个三角形），但来源捕获的光标和游戏桌面光标
并非同一坐标。Alt+- 操控改为锁定并隐藏游戏系统光标，WGC 会话关闭 IsCursorCaptureEnabled；
屏幕中只绘制一个虚拟指针。入口射线映射仅用于初始位置，之后使用已有 WM_INPUT 的鼠标移动量，
不重新注册 Raw Input 设备。没有 Raw Input 时使用排除回中事件的传统移动回退；出现 Raw Input
后丢弃重复的传统移动。相邻移动量按和合并，按钮和滚轮读取同一来源坐标。

虚拟指针使用来源归一化坐标，边界限制在来源图像内，窗口缩放保持相对位置。指针绘制考虑
画布黑边和 Unity 上传的 Y 翻转，只保存并还原指针占用的小矩形。静态来源没有新捕获帧时也能
更新指针；退出时清除缓存中的指针。退出恢复原 Cursor 状态，只有游戏仍在前台且原状态未锁定
时才恢复保存的桌面位置，避免切到其他应用后拉回鼠标。

实测 Chromium 对后台定向鼠标消息会按真实桌面光标生成 WM_MOUSELEAVE，单纯周期补发移动
会造成悬浮菜单反复进入／离开。因此新增 runtime/BetterEndfieldNext.MediaInputHooks.dll，
通过 Windows 的线程专属 WH_GETMESSAGE 钩子临时加载到显式选择的来源窗口线程。它仅在
当前操控窗口及其子窗口中、游戏保持前台且操控心跳未过期时过滤队列中的物理 HOVER／LEAVE；
辅助进程同步发送的虚拟 hover/leave 不经过该队列。退出先发送 leave，再撤销所有线程钩子。
不会安装全局消息钩子，也不改写来源应用的键盘或 GetCursorPos 等 API。

来源 hover 按系统停留时间和范围生成，拖动时不生成 hover，移动到其他子控件、非客户区及
退出时生成 leave。新钩子组件与采集进程一起构建／部署；帧与输入 IPC 保持 V3。
操控失败只释放输入，继续保留投屏与 BGM 租约。独立顶层提示窗口不一定被单窗口 WGC 捕获，
直接查询真实系统鼠标的应用仍需逐应用适配；本版未实现 IME、32 位或任意来源应用的通用保证。

验证：三项 CTest 通过，覆盖鼠标消息入口、移动合并与去重、游戏光标隐藏／恢复、来源边界和
缩放、绘制坐标与输入坐标一致、静态／冻结帧更新及无残影、控制失败保留投屏、真实 Win32
悬浮／离开、物理离开过滤、心跳／前台／窗口范围限制及退出后的原生消息恢复。
独立 Edge 测试页持续悬浮 1 秒：entered=1、CSS 菜单可见，退出后 left=1，坐标为 495,340
（捕获来源点 500,420，包含边框与浏览器工具栏换算），前台保持；修复前为反复 entered/left=14。
原浏览器点击、字符与方向键、滚轮试验仍为 clicks=1、text=BEtesXt、wheel=1。
Windows 原生及 Android 共享原生编译通过，PC 合约审计仍为 91 个方法、3 个字段、0 缺失。
尚需游戏内验证新指针的实际显示和手感，未安装 Android APK。未计算产物哈希。

19:48 已部署 MediaScreen、MediaCapture 及 MediaInputHooks 到 E:\Better Endfield，并同步
windows-app 布局；控制器 PID 29736，采集辅助进程 PID 12944。备份为
build/next/media-screen/pointer-deploy-20261011-194827/。设置字节保持一致，保留用户当前
2560×1440/30 FPS、BGM 暂停开启及 -／Ctrl+-／Alt+-，未重写 INI。
登记 runtime runner 三项测试通过，结果为 build/tests/runs/20261011T114741113932Z-36236/。
发布用 /MT 钩子 DLL 另在独立 Edge 来源中完成持续悬浮实测，entered=1、CSS 可见、退出 left=1，
前台保持；试验浏览器窗口均已关闭。此时游戏未运行，未把模拟验证记作游戏内验收。

### 2026-10-11：恢复系统鼠标默认模式并补齐输入采集

用户反馈网页及游戏来源无输入响应，并接受原方案的双鼠标和偏移。
日志只证明此前相对操控曾开启，不能证明输入采集或送达，尚未确认单一根因。
`window_control_mode` 默认 `screen`，旧配置自动采用此默认值；UI 可选择
“屏幕定位（系统鼠标）”，原 `relative` 保留为实验选项。
系统模式请求显示游戏鼠标并解除锁定，每次移动／点击按显示面的实际网格射线与
UV、材质缩放及宽屏适配转换到来源像素。屏幕外按下和滚轮不转发，松开仍送到
最后有效位置以结束拖拽，离开显示面向来源发送 leave。此模式不绘制虚拟鼠标，
WGC 恢复系统鼠标捕获；相对模式继续隐藏系统指针并绘制虚拟指针。

游戏输入采集增加当前帧线程及已注册 Raw Input 目标线程的 WH_GETMESSAGE，
不更改游戏的 Raw Input 注册。普通按键在被替换为 WM_NULL 前仅翻译一次；
NOLEGACY 键盘、鼠标按钮和滚轮从原始包获取。仅游戏前台、PM_REMOVE 且属于
游戏进程的消息被采集。系统位置轮询补充鼠标悬浮，退出撤销全部线程钩子。
日志每 5 秒报告 raw／keyboard／mouse／forwarded 数量及 IPC 读写计数，
不记录实际按键或输入文字；采集失败只退出操控，保持投屏与 BGM 租约。

验证：三项 CTest，包含真实线程消息队列、普通按键唯一字符、NOLEGACY 采集、
前台和 PM_REMOVE 范围、屏幕定位、移出后松开、来源 leave，以及原相对模式回归。
独立后台 Edge 页面得到 clicks=1、text=BEtesXt、wheel=1，前台保持。
桌面配置测试及发布、Windows 原生和 Android 共享原生构建通过；
静态合约审计为 107 个方法、3 个字段、0 缺失。游戏内操控仍需部署后实机复核。
后台窗口消息对只接受前台 Raw Input／DirectInput 的来源游戏没有通用保证。
未安装 Android APK，未计算产物哈希。

### 2026-10-11：完整回退到最初部署版的操控链路

用户实测系统鼠标修正版仍不如最初方案。最新日志存在 Win32 1460 发送超时，
输入进入 IPC 队列后操控退出，尚不足以断定所有体验问题的根因。
本次从当天开发记录恢复 18:52 部署前的真实源码，包含实际完成的文件修改与
18:50 的 shell 重写；中间的 PostMessage/UI Automation 草稿没有用于本次回退。
正式初版同样使用同步 SendMessageTimeout，不能将异步发送视为初版差异。

来源端 window_input.h 完整恢复为原文件，不注入来源悬浮钩子、不周期重发
悬浮消息，不再发送后来新增的离屏 leave。游戏端恢复原 WndProc 采集和
逐事件网格坐标映射，不安装新消息队列钩子、不读取 Raw Input 数据、不轮询
系统位置、不绘制虚拟指针。原有游戏输入抑制、按下接收窗口归属、屏幕外松开、
错误退出及 BGM 租约保持逻辑保留。窗口捕获恢复 WGC 默认系统指针行为。
单鼠标／相对模式 UI、配置和钩子构建依赖已撤销。

与恢复出的游戏操控源码相比，只增加桌屏 fit/crop/stretch 的五行 UV 换算，
保留后续桌屏完整显示面、大小屏独立开关、视频 Ctrl+-、窗口 Alt+- 和画质设置。
来源端原文件以及原始真实 Win32 输入测试保持一致；游戏端回归检查映射位置、
离屏松开、原始键／字符顺序、无 Raw Input 合成及控制失败不影响画面。
三项 CTest、独立后台 Edge 的点击／BEtesXt／滚轮和前台保持检查通过。
Windows UI、Windows 原生和 Android 共享原生构建验证；游戏内手感由部署后实测确认。
回退的源码记录位于 build/next/media-screen/first-input-recovery/，
部署备份记录位于 build/next/media-screen/original-control-deploy-*/deployment.json。

### Qt 来源窗口的重复字符修复

用户反馈一个字母输入两次且大小写不同。当前显式选择的来源为微信 4.1.15.13，
窗口类 Qt51514QWindowIcon。独立 Qt 5.15 输入框复现一次按键的 KEYDOWN 和 CHAR
各插入一次；此前仅 Edge 的验证不足以覆盖 Qt 路径。
[Qt 5.15 键盘源码](https://github.com/qt/qtbase/blob/5.15/src/plugins/platforms/windows/qwindowskeymapper.cpp)
在 KEYDOWN 缺少同线程待取 CHAR 时自行翻译字符，CHAR／IME_CHAR 也可单独生成文字事件。

只对已选择的 Qt QWindow 来源，普通可打印键以原始字符内容投递，不再同时发送
产生文字的 KEYDOWN／KEYUP；Ctrl／Alt／Win 快捷键仍发送按键，方向键、退格和 Enter
仍发送按下／松开，显式控制字符不重复发送。Qt 键盘消息按队列顺序投递，普通字符
使用 WM_IME_CHAR 独立文本入口，防止 Qt 的导航键 look-ahead 消费后续字符。
[WM_IME_CHAR 文档](https://learn.microsoft.com/en-us/windows/win32/intl/wm-ime-char)
规定 Unicode 窗口的字符参数为 Unicode 值。该路径不实现输入法组合过程。
非 Qt 来源继续使用原输入方式；游戏端、鼠标、屏幕适配、热键和共享内存 V3 未改动。
Qt 未修饰的普通字母按文本处理，依赖纯字母 KEYDOWN 的应用快捷键需另做焦点适配。

三项 CTest 通过，覆盖原 Win32 EDIT 和模拟 Qt 双路径、大小写／Unicode、导航及快捷键
消息保留。独立原生 Qt QLineEdit 实测 BEtesXt、Xa、aAzX!，退格后恢复 BEtest，
Enter 信号恰好一次，全部保持前台；Edge 仍得到 clicks=1、BEtesXt、wheel=1。
只更新采集辅助进程并保留配置。未向真实微信聊天窗口发送测试输入；游戏内微信验收待用户复核。
验证结果位于 build/next/media-screen/input-qt-after-*.json，
部署备份位于 build/next/media-screen/qt-text-deploy-*/deployment.json，未计算产物哈希。

### Muse Dash 来源的输入兼容性诊断

本机安装为 Unity 2019.4.41f1、IL2CPP，包含 Rewired 以及 Unity 的新旧输入模块。
只读解析确认发布配置已开启后台运行；这不等于确认后台键鼠设备可用。
隔离探针调用实际 WindowInput：普通按键和点击消息各送达一次，F24 的 Raw Input
为零，消息处理时 GetAsyncKeyState/GetKeyState 的按下位仍为零，前台保持。
现有应用测试不能作为来源游戏兼容性的证据。具体后端尚未动态确认，
此次未修改、构建或部署产品运行时，不宣称已修复 Muse Dash。
详见 [INPUT_COMPATIBILITY.md](INPUT_COMPATIBILITY.md)。

### 2026-10-12：来源游戏输入桥与终末地输入屏蔽

经用户授权继续实施，在选定 UnityWndClass／UnrealWindow 来源并按操控热键后，
采集辅助进程启用来源进程内的 GameInputBridge。提供合成 Raw Input、原生按键状态、
来源局部焦点及鼠标位置，兼容标准 DirectInput8 设备状态和缓冲事件。
普通应用与 Qt 文本修复保持原路径，使用用户接受的初版系统鼠标映射。
桥不使用全局 SendInput，不改变真实系统前台；退出发送键鼠松开并恢复原接口透传。
两秒心跳租约在辅助进程失联时释放输入；已加载模块重连解析来源 PE 导出表，
避免使用不同版本的本地导出 RVA。桥使用静态 CRT。

用户保持 Muse Dash 主菜单运行，后台 probe 实测 Unity GetKey(F15) 为 0→1→0、
GetMouseButton(0) 为 0→1→0，Rewired 键盘同样读到 0→1→0。
来源帧计数继续增加，位置符合客户区 (8,8) 换算，真实系统前台保持。
只用保留键与左上角鼠标测试，未进入对局；未把此证据当作会客室完整操控或玩法验收。
结果保存在 build/next/media-screen/muse-input-probe-20261012-004627/result.json。

终末地端在操控期间屏蔽普通窗口键鼠、Raw Input、原生键状态、Rewired 与 Unity legacy 查询。
Esc 转给来源，不再退出操控；Alt+- 退出仍通过真实键态读取工作。
退出后等待实体键鼠松开并经过两帧再恢复游戏输入，屏蔽 Alt 系统菜单命令，
进入／退出清 Unity 旧输入缓存。新 hooks 使用 Host 组合链，与现有 Raw Input observer 共存。

Windows 发布模块／采集辅助进程、四项 CTest 和 Android 共享原生编译通过。
测试包括跨进程 Raw Input／DirectInput 状态、实际前台／鼠标范围保持、字符唯一性、
Esc 转发、终末地读取屏蔽、退出实体键松开屏障及来源已加载桥重连。
本机 1.5.3 静态合约检查：130 个方法、3 个字段、0 缺失。
UE 实机、buffer-only Raw Input 来源、非标准 DirectInput 格式、无限相对鼠标及玩法延迟未验收；
终末地内完整屏蔽效果仍待实机验证。未安装 Android APK，未计算产物哈希。

01:11 已部署 MediaScreen、MediaCapture 和 GameInputBridge 到 E:\Better Endfield，
并同步 windows-app 布局。控制器 PID 42572、采集辅助进程 PID 49904；
Muse Dash PID 49664 保持运行，部署时终末地未运行。
三个文件逐字节复制核对通过，INI 字节保持一致，未改变来源、热键、画质、BGM 或屏幕开关。
备份为 build/next/media-screen/game-input-deploy-20261012-011102/，
登记 runner 四项测试通过，结果为 build/tests/runs/20261011T171011028365Z-46988/。
登记入口保持无参数 CTest；来源桥 DLL 记作构建凭据的 support_files，四个 EXE 为发现集合。

部署后用户在会客室实测并反馈“效果可以”，授权合并到 main 并推送远端。
本版投影操控获得用户验收；UE、其他来源和未逐项测试的按键／玩法仍沿用上述验证边界。
