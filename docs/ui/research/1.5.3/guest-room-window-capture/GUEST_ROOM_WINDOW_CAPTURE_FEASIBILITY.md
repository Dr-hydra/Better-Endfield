# 帝江号会客室媒体屏：窗口、图片与视频的双端可行性分析

分析日期：2026-10-11。工作区：`G:\Better Endfield`。源码基线：`40560f4c`，Better Endfield Next 4.0.1。

结论：**Windows 窗口投屏、双端本地图片和视频都值得做原型；Android 捕获其他应用的画面可行，但需要额外的平台适配和多任务验证。** Windows 已找到屏幕专用轮播与纹理接口，Android 有对应纹理 Helper 和视频播放器的静态线索；双端尚未完成运行时接入和性能验证。

用户已确定：声音复用现有稳定接口；按热键才开始替换，不做自动绑定。建议将功能组织为统一的会客室媒体屏，共享接管、恢复和播放控制，按来源和平台分别实现图片、视频、窗口/应用捕获。

## 证据与验证边界

本次检查了当前源码、工作区配置指定的 1.5.3 Windows 资源清单、2026-09-03 IL2CPP 导出与 Lua 资料、现有 Android 元数据与媒体桥，并核对 Microsoft、Android 和 Unity 官方文档。自动发现的本机游戏目录为 `E:\Endfield Game`；安装目录中 `UnityPlayer.dll` 的版本为 `2021.3.34f5`，`boot.config` 启用了 graphics jobs 和 native graphics jobs。

分析时游戏未运行。本次没有启动或注入游戏，没有替换资源、编写投屏功能或实测延迟。历史导出中的方法存在不等于当前调用一定经过该方法，内联和热更新也可能影响 Hook 命中。现有 1.5.3 输入不代表已经逐项核对本机最新资源；进入原型阶段要重新解析契约并验证实际调用。

## 屏幕的实际接入点

这块屏幕有自己的展示链路，不能只根据名称把它当成通用 `VideoPlayer`。

| 本地证据 | 已确认内容 | 对实现的意义 |
| --- | --- | --- |
| `Gameplay.Beyond.dll / 全局命名空间 / SpaceShipReceptionRoomImagePoster` | `Init`、`Reload`、`Clear`、`UpdateImagePoster`、`_EnqueueImagePoster`、`_DequeueImagePoster`、`_PosterCycleCoroutine`、`_StopSwitchCoroutine`；字段 `m_imagePosterGo`、`m_posterHelper`、`m_switchCor` | 有明确的初始化、轮播和释放边界，可定向获取屏幕实例并协调轮播 |
| `Beyond.Gameplay.Core.SpaceShipGameLevel.imagePoster` | 帝江号关卡持有上述展示对象 | 可从所属关卡验证屏幕身份，避免误选其他海报 |
| `HG.Rendering.Runtime.ReceptionRoomCharPosterHelper` | `Enqueue(Texture2D, String)`、`SetMaterialTexture(Renderer, String, Texture)`、`Tick(Single)`；字段 `m_topTargetRenderer`、`m_botTargetRenderer`、`m_materialPropertyBlock`、`m_instancedMaterials` | 已有纹理设置入口，同时存在两个渲染目标和材质实例管理；轮播过渡必须实查 |
| `ReceptionRoomCharPosterHelper.TextureAffectTarget` | 保存 `target` 和 `propertyName` | 原型可读取实际纹理属性名，不预设为 `_MainTex` |
| 静态字符串与资源清单 | UI 名为 `ReceptionRoomImagePoster`；清单包含对应 prefab；大图路径为 `Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/SpaceShip/ImagePoster/LargeSize/{0}.png` | 类名、资源路径与展示行为相互吻合 |
| `SpaceshipReceptionDisplayCtrl.lua` | 使用 `GetScreenWallPicIds` 和 `ChangeGuestRoomScreenWallPics` 管理游戏图片列表 | 本地投屏应接管客户端显示层；原图片选择涉及游戏数据，不应拿它传送本地帧 |

纹理更新会自然呈现在屏幕所在的三维场景中，继续接受该网格的透视、遮挡与材质效果。这是基于上述 Renderer 接口的推断；实际 UV、屏幕比例、发光强度和色彩仍待验证。

绑定按手动触发设计：用户先选择来源，按下热键时读取当前关卡的屏幕对象及其 Helper，验证后才接管。没有进入会客室或对象不就绪时返回状态，不排队等待之后自动开始。默认不枚举全部 Renderer、不常驻扫描屏幕、不在 `Init/Reload` 后自动绑定。Android 用悬浮窗按钮触发同一命令，外接键盘可映射到同一热键；项目已有 `NativeCommandBridge.key` 虚拟按键桥。

接管时只操作确认属于屏幕实例的目标。这个 Helper 也用于角色海报，不能对全类型无条件替换。需要暂停或隔离该屏幕的原轮播，处理两个渲染目标及已排队的切换动作。优先通过游戏自己的 Helper 设置纹理；若其属性块或动画持续覆盖，再评估定向拦截 `SetMaterialTexture`。具体选择由运行时探针确定，不能仅依据方法名称判定。

再次按键或显式停止时恢复原生图片；离场或对象失效时解除引用并释放自建资源。仅在接管期间维护已绑定对象的有效性和释放边界，重新进入会客室需再次按键。按键控制的是是否接管，持续播放期间仍需逐帧或按来源帧率更新内容。

## 来源与双端能力

| 来源 | Windows | Android | 优先级与主要成本 |
| --- | --- | --- | --- |
| 本地 PNG/JPEG | 高可行性 | 高可行性，待确认屏幕实例 | 最先验证；只需解码及一次纹理上传 |
| 图片列表轮播 | 高可行性 | 高可行性，待确认屏幕实例 | 共用列表、间隔和切换逻辑，按需加载当前/下一张 |
| 本地视频文件 | 高可行性，已有 VideoPlayer 导出 | 有 VideoPlayer 静态线索，需运行时验证；可准备原生解码回退 | 优先尝试播放器直接输出纹理，并复用音频控制 |
| 本地窗口/其他应用画面 | WGC 候选明确 | MediaProjection 候选明确，但系统授权和后台源刷新受平台约束 | 双端分别实现捕获后端；Android 排在文件来源之后 |
| 电脑窗口传到安卓游戏 | 需增加发送端 | 需增加网络接收、解码和缓冲 | 属于额外的局域网串流来源，不等于双端具备同一功能 |

共同的屏幕绑定层接受 `UnityEngine.Texture`，来源可以交付 `Texture2D`、`RenderTexture` 或原始像素帧；不要强制所有来源都走 CPU 像素上传。共同控制层保存来源选择、播放/暂停/停止、进度、循环、音量、比例和恢复状态。捕获、解码、文件访问及 GPU 互操作留给平台适配。

### 图片

单张 PNG/JPEG 最适合验证 Android 屏幕材质。解码后创建纹理，按屏幕实际比例裁剪或留边，绑定一次即可；不需要持续上传静态图片。图片轮播由本模块管理，在原生轮播被接管期间只加载必要的当前/下一张，避免把整个相册常驻显存。GIF/WebP 动图可作为以后增加的解码来源，首版不必把它们算作普通静态图片。

Android 导入文件可以复用现有资源导入与复制流程，保证游戏进程能够读取来源；不能把管理应用选择得到的任意 `content://` URI 当成游戏进程一定有权访问的本地路径。

### 视频文件

优先尝试 `VideoPlayer` 读取本地文件，输出 `RenderTexture` 或内部 `Texture`，再赋给屏幕。Windows 导出包含 `url`、`targetTexture`、`texture`、`Prepare`、`isPrepared`、`time`、`isLooping` 和音频控制接口。Unity 官方支持文件 URL 和纹理输出，因此此路线有机会避免逐帧 CPU 读回和上传。[VideoPlayer](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Video.VideoPlayer.html)。

现有 `Enqueue` 的参数限定为 `Texture2D`，不能直接传入 `RenderTexture`。视频应使用接受基类 `Texture` 的 `SetMaterialTexture` 或相应 Renderer/材质属性块绑定，必要时复用一张 RenderTexture 做比例和颜色适配，不为满足 Enqueue 类型而每帧 ReadPixels。

Android 的 `research/android/1.5.3/Windows/android-153/global-metadata.dat` 中可以找到 `UnityEngineVideoVideoPlayerWrap`、`UnityEngine.VideoModule` 模块名称线索和视频事件类型引用；这不是完整的运行时解析报告，不能据此承诺接口已可调用。同一资料中未找到 Windows 的 `SpaceShipReceptionRoomImagePoster` 同名字符串，但有 `ReceptionRoomCharPosterHelper` 及 `SetMaterialTexture` 标识，因此 Android 的具体屏幕获取路径需要独立验证。

若 Android 的 Unity 播放器契约或纹理输出不可用，候选回退为 `MediaPlayer` 或 `MediaCodec` 解码到 Surface，再传递到 Unity 纹理。OpenGL ES 的 SurfaceTexture 使用 `GL_TEXTURE_EXTERNAL_OES`，不能直接当成普通 `GL_TEXTURE_2D` 给现有屏幕 Shader 使用；通常需要转换/绘制到普通 2D 纹理，且更新要在正确的 GL context 线程执行。Vulkan 需要对应的 buffer/image 互操作，不能套用 GL 句柄。[MediaCodec](https://developer.android.com/reference/android/media/MediaCodec)、[SurfaceTexture](https://developer.android.com/reference/android/graphics/SurfaceTexture)。

首版验证文件优先选 MP4 容器、H.264 视频和 AAC 音频，实际支持依设备与系统解码器验证。循环、暂停和跳转按同一时间轴控制；解码准备、首帧可用以及 seek 完成之前保持明确的播放状态。[Unity 文件兼容性](https://docs.unity3d.com/2021.3/Documentation/Manual/VideoSources-FileCompatibility.html)、[Android 媒体格式](https://developer.android.com/media/platform/supported-formats)。

### 复用音频接口

声音纳入方案，复用 `BE_LocalMusicApiV1` 的 open/play/pause/seek/close/status/gain。Windows Music 模块通过 Media Foundation 解码并送入 Wwise Audio Input；Android 的同名能力由 JNI 桥接 `MmdAudio` 的 MediaPlayer。它们共用调用形式，但 Android 现有实现不是相同的 Wwise 输出路径。

视频画面与声音必须协调：优先用音频实际播放位置作为主时钟，播放、暂停、跳转和循环共同执行，关闭视频播放器自身声音以避免重复。PC API 的 position 字段定义为 Wwise 已消费的播放位置；Android 提供 MediaPlayer 位置快照。Unity 的 `externalReferenceTime` 可作为同步候选，但要验证当前播放器实际行为，不采用每帧 seek 的同步方式。[外部视频时钟](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Video.VideoPlayer-externalReferenceTime.html)。

当前 PC `local_track.inc` 的扩展名白名单为 WAV/MP3/M4A/AAC/FLAC/WMA，直接把 MP4 传给现有 open 会被拒绝。视频音轨可先解复用为受支持的本地音轨并继续使用现有接口，或在验证后补充容器/流选择适配。现有接口也没有公开实时 PCM push，因此窗口/应用实时声音需要来源取音及流输入适配；声音输出本身不必另起一套系统。该接口一次占用一条本地音轨，媒体屏与 MMD 播放需要协调使用权。

Android 捕获其他应用的声音可使用 AudioPlaybackCapture，但依赖 RECORD_AUDIO、MediaProjection 授权、同一用户配置，以及源应用允许捕获；稳定的播放接口并不能保证任意来源应用都可取音。[Android 播放音频捕获](https://developer.android.com/media/platform/av-capture)。

### Android 其他应用画面

Android 5 起有 MediaProjection，Android 14 起提供单应用分享选择；当前 BE APK 的 minSdk 为 29、targetSdk 为 35。采用公开 API 的捕获会话需要系统授权，Android 14 的会话 token 不可复用；当前 Manifest 尚无 mediaProjection 前台服务声明，需在管理 APK 中补充相应服务和权限。这是系统捕获要求，不影响图片或视频文件的手动启动。[MediaProjection](https://developer.android.com/media/grow/media-projection)。

要区分“采集整个手机显示”与“采集选中的源应用”。全屏采集包含游戏时会递归拍到会客室屏幕；单应用来源更合适，但源应用被切到后台后是否继续出帧要实测。分屏、浮窗/PiP 能让两个应用同时可见，但游戏和源应用是否支持相应多任务模式仍取决于设备及应用。建议 Android 先落地图片与视频，再单独验证应用捕获体验。

如果捕获服务在 BE 管理 APK，而纹理在游戏进程，需通过共享缓冲/IPC 传送画面。不能把一个进程的 GL texture ID 直接交给另一个进程使用；GPU buffer 跨进程传递需要另做验证，CPU 帧传递可用于低分辨率原型。

## Windows 捕获与传输方案

捕获端首选 **Windows.Graphics.Capture（WGC）**。Microsoft 提供按窗口句柄创建捕获对象的 `CreateForWindow`，要求 Windows 10 1903 或更高版本，符合现代 Windows 本地投屏的使用场景。运行前还要调用 `GraphicsCaptureSession.IsSupported()`。WGC 输出 D3D11 帧，需根据帧的 `ContentSize` 处理有效区域，并在窗口大小变化时重建帧池。[CreateForWindow](https://learn.microsoft.com/en-us/windows/win32/api/windows.graphics.capture.interop/nf-windows-graphics-capture-interop-igraphicscaptureiteminterop-createforwindow)、[屏幕捕获与帧生命周期](https://learn.microsoft.com/en-us/windows/apps/develop/media-authoring-processing/screen-capture)。

可使用 `CreateFreeThreaded` 接收后台帧，避免依赖游戏线程的 DispatcherQueue。捕获回调只处理捕获资源和帧缓冲；Unity 对象操作与纹理上传在已验证的 Unity 主线程回调中执行。[帧池线程模型](https://learn.microsoft.com/en-us/uwp/api/windows.graphics.capture.direct3d11captureframepool)。

| 路线 | 数据链路 | 判断 |
| --- | --- | --- |
| 第一版：CPU 像素上传 | WGC → 缩放/裁剪 → GPU 读回 → 最新帧缓冲 → `LoadRawTextureData` / `Apply` → 屏幕 | 现有工程基础最多，适合证明接入与测量性能；有读回和上传开销 |
| 后续：GPU 纹理传递 | WGC → 可复用 GPU 纹理 → 游戏设备上的复制/共享与同步 → Unity 纹理 → 屏幕 | 可减少 CPU 往返；设备、图形后端和渲染线程同步明显增加实现成本 |
| 图形层替换回退 | 定位屏幕 draw/resource 并在图形 API 层替换 | 只在 Unity 接口路线受阻时评估；维护负担更大，还需核对与显示增强的组合 |

第一版可让用户在 BE 中选择窗口，把显式选中的 HWND 通过控制通道交给 Windows 专用原生模块，由模块管理捕获会话和固定数量的缓冲。这样不需要每帧跨进程传送像素；停止投屏和窗口关闭应立即结束捕获。若后续需要把捕获隔离到辅助进程，可用共享内存或共享 GPU 资源传送帧，控制消息保持小而独立。

Desktop Duplication 主要针对桌面图像；对于本需求，单窗口 WGC 的对象范围更合适。整桌面捕获还会把游戏画面本身纳入来源，容易产生递归画面。[Desktop Duplication](https://learn.microsoft.com/en-us/windows/win32/direct3ddxgi/desktop-dup-api)。

## 现有源码可复用的能力

- Host 已支持按程序集、类型、签名和字段动态解析、对象分配与调用、共享 Hook 链，无需添加固定客户端地址。
- `native/modules/custom_model/module.cpp` 已创建 `Texture2D`，使用 `LoadRawTextureData(IntPtr, Int32)`、`Apply(Boolean, Boolean)` 和 `Material.SetTexture`。动态纹理上传有现成参考。
- 现有模块使用 `Canvas.SendWillRenderCanvases` 驱动 Unity 线程内的维护，可作为候选帧调度入口；投屏仍需确认所在场景的回调与调用频率。
- `native/modules/model/module.cpp` 已有运行时纹理和材质副本、恢复及销毁的实现经验。

**不能直接照搬 BEM 纹理上传的全部参数。** BEM 一次性上传使用 `Apply(false, true)` 丢弃 CPU 可读副本；持续写入的 CPU 投屏纹理应保持可读，例如无 mip 的纹理使用 `Apply(false, false)`。纹理和缓冲要复用，不逐帧创建对象或编码 PNG/JPEG。Unity 官方说明原始数据先写入 CPU 内存，之后由 Apply 上传，且 Apply 会复制整张纹理。[LoadRawTextureData](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture2D.LoadRawTextureData.html)、[Apply](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture2D.Apply.html)。

GPU 路线可参考 `CreateExternalTexture` 和 `GL.IssuePluginEvent`，历史导出也包含这些接口。但现有 BE Host 加载普通功能 DLL，不等于它已经通过 Unity 的原生插件机制取得 `IUnityInterfaces` 或图形设备回调。WGC 的 D3D11 surface 也不能无条件作为当前游戏纹理使用：要验证实际后端、设备/适配器、格式、资源共享能力与同步。在 D3D12 路径中还要处理跨 API 资源和 fence。官方插件接口明确区分主线程和渲染线程。[外部纹理](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture2D.CreateExternalTexture.html)、[原生渲染插件线程](https://docs.unity3d.com/2021.3/Documentation/Manual/NativePluginInterface.html)。

不要长期保存并绑定已经归还帧池的捕获 surface，应先复制到自己管理的纹理。也不应每帧取一次 `GetNativeTexturePtr` 作为普通更新手段；现有源码把它用于上传同步，GPU 路线应只在创建或重建资源时取得并管理句柄。

## 性能边界

以下是 4 字节/像素、无 mip 的**单份原始帧数据量计算**，不是实测吞吐、显存占用或可达到的帧率。CPU 路线还包含读回、内存复制、上传和等待开销。

| 输出规格 | 单帧 | 单份帧流量 |
| --- | --- | --- |
| 1280×720，15 FPS | 3.52 MiB | 52.7 MiB/s |
| 1280×720，30 FPS | 3.52 MiB | 105.5 MiB/s |
| 1920×1080，30 FPS | 7.91 MiB | 237.3 MiB/s |
| 1920×1080，60 FPS | 7.91 MiB | 474.6 MiB/s |

建议从 720p/15 FPS 开始，再测 720p/30 FPS。窗口来源是 4K 时先缩放再读回；输出画布应按实测屏幕比例调整，上表的 16:9 只是预算示例。原型只消费最新完整帧，缓冲固定为 2–3 份，处理不过来就丢弃旧帧，防止队列累积延迟。采用稳定分辨率、按需重建和可配置帧率上限；屏幕不在当前场景时停止上传。

记录捕获时间戳、上传耗时、显示更新频率、丢帧、游戏帧时间与 CPU/GPU 占用。没有这些测量前，不承诺固定延迟、1080p60 或低性能设备体验。

## 首版范围及待验证问题

建议第一阶段为双端图片/图片轮播，第二阶段为双端本地视频及现有音频接口，第三阶段为 Windows 窗口捕获，第四阶段再验证 Android 应用捕获。统一按热键或 Android 面板按钮开始接管，提供来源、播放状态、比例和恢复控制。画面仅在本机客户端显示，访客不会因此收到同一画面。点击屏幕控制来源应用及局域网串流可以作为后续扩展。

需要重点验证：

1. **原轮播与过渡。** 两个 Renderer 是否交替切换，动画/属性块是否持续覆盖，停止后如何恢复当前原图。
2. **材质与画面。** 读取 `propertyName`，确认 UV 朝向、屏幕比例、BGRA/RGBA 通道、sRGB/线性空间、发光和后处理效果；HDR 来源需要专门的色彩处理。
3. **源窗口状态。** 普通应用和浏览器分别验证前台、被遮挡、最小化、改尺寸和关闭。被遮挡不等同于最小化；后台应用也可能主动停止刷新，不能提前保证所有窗口持续输出。
4. **捕获限制。** 系统可排除特定窗口的捕获；受保护内容不在首版兼容承诺中。窗口无法捕获时显示明确状态并恢复原画。[窗口捕获排除](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowdisplayaffinity)。
5. **生命周期与时间。** 离场、关卡重载、停用后不残留引用，重进后需再次手动触发；时间冻结时是否仍需实时播放，调度应使用墙钟并验证冻结后的回调。
6. **后端与组合。** 实际 D3D11/D3D12、双显卡以及 OptiScaler 开关分别验证；本机引擎版本和静态 boot 配置不能证明正在运行的图形后端。

## 下一步验证顺序

| 阶段 | 操作 | 通过条件 |
| --- | --- | --- |
| A：对象定位 | 手动触发时分别获取双端会客室对象、Helper、两个 Renderer、属性名、原始纹理和真实调用 | 唯一识别屏幕，排除相邻海报；确认双端实际契约 |
| B：静态替换 | 手动绑定带文字和色块的自建纹理，经过轮播周期，再停用、离场、重进 | 原轮播不覆盖，恢复正确，重进后不会自动接管 |
| C：动态纹理 | 持久纹理播放本地计时器/移动色块，不接窗口捕获 | 连续更新稳定，无持续内存增长；确认主线程调度 |
| D：视频与声音 | 验证双端 VideoPlayer，接入本地音轨、暂停/跳转/循环；不可用时验证平台解码回退 | 有画面、声音和可控同步；原生图恢复与音轨关闭正确 |
| E：窗口投屏 | 接入 WGC，从 720p/15 FPS 升到 30 FPS，测窗口状态和游戏帧时间 | 形成可复现的兼容范围与性能数据，再决定是否需要 GPU 优化 |
| F：Android 应用捕获 | 验证系统授权、来源应用刷新、前台服务、跨进程帧传递及手机负载 | 明确支持设备与多任务场景，再决定是否进入正式功能 |

最大的共同未知项是双端屏幕的实际获取、轮播覆盖与恢复，文件来源的基础接口比跨应用捕获更容易验证。因此，最有价值的第一步是 A/B 阶段的双端手动图片替换；通过后再验证视频播放器和声音，最后扩展捕获来源。

## 本地资料入口

- [Host 运行时接口](../../../../../native/shared/include/BetterEndfieldNext/ModuleApi.h)
- [现有纹理创建与上传实现](../../../../../native/modules/custom_model/module.cpp)
- [运行时纹理副本与恢复参考](../../../../../native/modules/model/module.cpp)
- [屏幕展示对象与 Helper 的 IL2CPP 导出](../../../../../research/host/1.5.3/Windows/host-il2cpp/20260903-pc-current/IL2CPP_Dump_Normal/Gameplay.Beyond.dll.cs)
- [Unity 接口导出](../../../../../research/host/1.5.3/Windows/host-il2cpp/20260903-pc-current/IL2CPP_Dump_Normal/UnityEngine.CoreModule.dll.cs)
- [屏幕资源路径静态字符串](../../../../../research/host/1.5.3/Windows/host-il2cpp/20260903-pc-current/IL2CPP_Dump_Strings.txt)
- [图片列表配置 Lua](../../../../../research/host/1.5.3/Windows/lua-full/LuaScripts/UI/Panels/SpaceshipReceptionDisplay/SpaceshipReceptionDisplayCtrl.lua)
- [1.5.3 Windows 资源清单](../../../../../inputs/1.5.3/Windows/baseline/Bundles/Windows/manifest.json)
- [PC 视频播放器导出](../../../../../research/host/1.5.3/Windows/host-il2cpp/20260903-pc-current/IL2CPP_Dump_Normal/UnityEngine.VideoModule.dll.cs)
- [Android 元数据线索](../../../../../research/android/1.5.3/Windows/android-153/global-metadata.dat)
- [共用音频 API](../../../../../native/shared/include/BetterEndfieldNext/LocalMusic.h)
- [PC 本地音轨解码与格式检查](../../../../../native/modules/music/local_track.inc)
- [Android 音频桥](../../../../../android/app/src/main/cpp/core/local_music_android.cpp)
- [Android MediaPlayer 音轨实现](../../../../../android/app/src/main/java/dev/betterendfield/next/MmdAudio.java)
- [Android 虚拟按键及命令桥](../../../../../android/app/src/main/java/dev/betterendfield/next/NativeCommandBridge.java)
