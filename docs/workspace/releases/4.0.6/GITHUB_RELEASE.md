Better Endfield Next **4.0.6** 与 BEM Tools **1.5.5**，修复 Steam 叠加层链式启动注入、解决 Android 会客室媒体屏视频解码闪退，并增量同步桌面端新角色头像与战斗字典。

## Windows 4.0.6

- **Steam 覆盖层与链式启动修复**：针对 Steam 替换启动即使开启管理员权限也无法呼出 Steam 叠加层的问题，重构启动器注入流程，在挂起的游戏子进程中注入 Steam Overlay 模块，彻底恢复 Shift+Tab 叠加层呼出与状态传递。
- **桌面端新角色头像与战斗字典增量同步**：增量提取并规范化裁剪新角色「祀（chr_0036_jsspsi）」与「明河（chr_0037_minghe）」的高清头像资源，作为嵌入资源打包入桌面端程序集；重跑战斗数据流水线，将战斗字典与 Native Overlay 角色元数据同步更新至 36 位角色。

## Android 4.0.6

- **会客室媒体屏视频播放崩溃修复**：解决在 Android Vulkan 模式下播放媒体屏视频导致的驱动崩溃与闪退问题；实现 NDK 原生 `MediaCodec` / `libmediandk.so` 解码回退方案，通过 SurfaceTexture 直连渲染，彻底根治视频播放崩溃。

## BEM Tools 1.5.5

- 延续 1.5.5 架构，支持昼雪、祀、明河的 Windows 1.6.3 目标合同与全量角色 profiles 资料。

## 下载

| 附件 | 用途 |
| --- | --- |
| `BetterEndfieldNext-4.0.6-Setup.exe` | Windows x64 主程序安装包，包含 Steam 叠加层与新角色头像修复 |
| `BetterEndfieldNext-4.0.6-Android-arm64.apk` | Android 4.0.6 Release APK，包含会客室媒体屏 MediaCodec 解码修复 |
| `BEM-Tools-1.5.5-win-x64.zip` | 完整独立创作者 GUI、CLI 和全量角色资料 |
