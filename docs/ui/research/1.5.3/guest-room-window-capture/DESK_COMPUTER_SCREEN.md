# 会客室桌面 Logo 电脑屏幕接入

2026-10-11。用户优先目标：会客室桌上显示终末地 Logo 的电脑。
工作分支 feat/guest-room-media-screen，源码位于 G:\Better Endfield。

## 现场确认

20:32:52–53 的显式投屏调查扫描了 1802/1802 个 Renderer；相机附近 83 个，未截断。
现场完整记录：build/next/media-screen/desk-survey-latest.json。

三块显示面位于以下对象树：

```text
#9800000032_int_system_spaceship_guest_terminal/mesh/
  int_system_spaceship_guest_terminal(Clone)#40/
    P_fxint_s_interactive_screen_table_2501/Internal/
      logo_bd
      logo_bd_02
      logo_bd_03
```

三者均为 Plane001、单一材质槽 0；这些是显示面中央的方形 Logo，并非整个宽屏。
原材质 M_fx_base01_lv001_ship_monitor_1144，
Shader 为 HGRP/Effect/VFXBaseV2，_MainTex 为 T_fx_ui_1103_UI。
现场实例 ID 为 -74034、-74064、-74094，仅用作调查证据，运行时不硬编码。

同组 wenli／wenli_02／wenli_03 是纹理叠层，使用
M_fx_s_interactive_screen_table_2501、T_fx_flow_1104_M；
glow_ad／glow_ad_02／glow_ad_03 使用 M_fx_s_interactive_screen_table_2502。
wenli* 对应完整显示面；新版将它们接管为接收端，临时关闭配对的 logo_bd* 与 glow_ad*，
停止时恢复材质及原 Renderer.enabled。每块屏幕分别核对和恢复，失效一块不会隐藏其他屏的原 Logo。

P_fxmap_base01_lv001_ship_credit_table_01 内的 logo_01／logo_02 等是桌面装饰；
S_iprop_map01_zmdtable+1_001_01_lod* 是桌子本体。两者均不接管。

## 离线材质核对

本机 E:\Endfield Game 当前 VFS manifest.hgmmap 与 1.5.3 Windows baseline 逐字节一致。
从 StreamingAssets／Persistent 叠加层只读提取目标 prefab、Logo 纹理、材质及动画共 8 包、
118905 字节。NativeAssetReader 读取 79 个对象，backend_issues=0。
记录见 build/next/media-screen/desk-resources/logo-graph.json 和 logo-bundles/。
没有写入游戏资源，没有计算产物哈希。

Logo 纹理为 512×512 BC7，_MainTex scale=(1,1)、offset=(0,0)，UV 旋转为单位矩阵，
UV 滚动为零。该材质仅绑定 _MainTex，启用 _USE_FOG；不存在绑定的外壳图集或遮罩纹理。
原 _TintColor RGB 为 0.4641509，透明混合 SrcBlend=1、DstBlend=10，_UseMainTexAsAlpha=0。
接入保留原 Shader、深度和混合设置，在副本上将 tint 设为白色、忽略顶点颜色，
并固定相关亮度／透明参数，防止原 Logo 的灰色调制影响视频。

## 实现与生命周期

desk_receivers.h 在成功手动开始投屏后搜索已加载 Renderer。
wall_screen_enabled 与 desk_screen_enabled 默认均为 true，UI 可独立选择大屏／小屏；
只开小屏无需寻找墙屏对象、读取 helper 或阻止大屏的原生轮播。
两者都关闭时不会启动捕获、解码或音频。配置变更沿用结束当前投屏的行为，保存后按启停键重新绑定。
target_object 仅用于墙屏。每帧最多处理 64 个对象，以 3 ms 为分片目标；
候选总量上限 100000，搜索超时 10 秒，匹配候选上限 36。查询返回值及候选均有 GC 根。
不在设置变更、初始化或搜索完成后自动绑定新实例；离场后需再次按启停键。

匹配同时核对叶节点名、Internal 父节点、特效祖先、guest_terminal 祖先、单一材质槽、
原材质、Shader 与原贴图。不依赖 #40 等运行时后缀、实例 ID、相机距离或模糊 screen 名称。
重复的同组同名 Logo 面视为歧义并跳过。

每个 wenli 面须存在同组同后缀、唯一且有效的 logo_bd 面；采用 Logo 的已验证材质副本，
保留 wenli 的原材质用于恢复。通过 Renderer.localBounds 与 Transform.lossyScale 取得本地 XY 显示面的比例，
避免用旋转后的世界 AABB 或方形 Logo 尺寸猜测宽屏比例。
离线 prefab 的三组 scale 为 (10.25,4)、(6.5,3.15)、(7.15,4.65)，约为 2.56:1、2.06:1、1.54:1。

aspect_fit.h 提供 fit／crop／stretch 三种模式，默认 fit 保持比例并留黑边。
每块屏幕各有按实际比例构造的 RenderTexture，尺寸保持在用户画布分辨率预算内；
通过 GPU Blit 与 CopyTexture 完成等比缩放、裁剪或拉伸。
窗口、图片和视频仍只捕获／解码一次；捕获画布的黑边会按实际来源内容矩形去除后重新适配，避免重复黑边。
窗口和图片只在来源帧／虚拟指针变化时更新小屏，视频用 get_frame 判断新帧；暂停画面复用已有纹理。
动态改变窗口尺寸时重新调整中间纹理，并销毁、解除根引用旧纹理。
VideoPlayer 尺寸／帧号接口缺失或调用异常时回退为公共画布适配，不停止大屏视频。
仅配对屏幕的六个 Logo／发光叠层会暂时隐藏；原本禁用的叠层保持禁用。
停止、来源变更、离场和模块退出时取消搜索、恢复自有材质及叠层、销毁副本并释放 GC 根。
恢复时核对材质所有权，保留其他功能或游戏后来写入的材质。
桌屏缺失、搜索／材质准备失败、单块屏幕或副本失效均不停止墙屏投屏。

Alt+- 继续使用同一虚拟指针。新增桌屏若 Mesh 不可读，会跳过其首次定位求交，
其他可读显示面仍可用于初次定位；都不可读时指针从来源中心开始，持续移动使用相对鼠标输入。
屏幕坐标到来源坐标的转换包括适配产生的黑边／裁剪和来源内容矩形。
桌屏搜索与画面替换为 Windows／Android 共享代码；Android 设置页同步独立开关及三种适配模式。
Android 上的对象树和最终视觉效果尚未实机验证。
Windows 只读 receiver_inspection.h 调查工具仍保留，仅首轮显式投屏执行。

## 验证与部署

Windows 静态合约审计：107 个方法、3 个字段，0 缺失。三项 CTest 通过。
新增测试覆盖无隐式扫描、精确候选／单槽／Shader／原纹理校验、同源副本与原材质不变、
叠层反复关闭与原状态恢复、外部材质所有权、桌屏／副本释放不影响墙屏、分帧上限、
取消及释放 GC 根、局部准备失败、接口失败、重复候选歧义。
另覆盖三种显示比例／模式、去除来源黑边、等比裁剪、适配后的指针坐标、静态帧不重复 GPU 缩放、
大小屏独立开关、只开小屏不接管墙屏以及全部关闭不启动来源。
桌面配置测试验证默认迁移、开关／模式持久化与全部关闭时的音频模块请求。

定位版于 20:31 部署到 E:\Better Endfield，保留了用户全部设置；本轮桌屏版部署记录
位于 build/next/media-screen/desk-projection-deploy-*/deployment.json。
宽屏修正版的 UI／原生部署记录位于 build/next/media-screen/screen-fit-deploy-*/deployment.json。
最终游戏显示效果仍需部署后实机复核。
