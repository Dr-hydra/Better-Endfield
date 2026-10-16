# Better Endfield Next 4.0.6

本轮发布 **Windows 4.0.6、Android 4.0.6（40006）与 BEM Tools 1.5.5**。共分发 3 个正式产物。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

## 更新

- **Steam 覆盖层与链式启动修复**：
  - 针对 Steam 替换启动即使开启管理员权限也无法呼出 Steam 叠加层的问题，重构启动器流程，在挂起状态下的游戏子进程中完成 Steam Overlay 模块注入，彻底恢复 Shift+Tab 叠加层快捷键呼出与状态同步。
- **Android 会客室媒体屏崩溃修复**：
  - 解决手机版会客室媒体屏播放视频时闪退的问题。针对 Android Vulkan 渲染模式下 Unity 原生 VideoPlayer 引发的底层驱动崩溃，自动回退并接入 Android NDK 原生 `MediaCodec` / `libmediandk.so` 解码流水线，直连 SurfaceTexture 纹理渲染，彻底根治视频播放闪退。真机测试已通过。
- **桌面端新角色头像与战斗字典增量同步**：
  - 增量提取并规范化补充新角色「祀（chr_0036_jsspsi）」与「明河（chr_0037_minghe）」的高清头像资源，完整嵌入桌面端 UI 程序集；重新运行增量流水线同步战斗字典，角色总数增量更新至 36 位，同步刷新 Native Overlay 与 Web 字典定义。

## 附件范围

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| `BetterEndfieldNext-4.0.6-Setup.exe` | GitHub Release | Windows x64 安装包 |
| `BetterEndfieldNext-4.0.6-Android-arm64.apk` | GitHub Release | Android arm64 安装包 |
| `BEM-Tools-1.5.5-win-x64.zip` | GitHub Release | 完整独立创作者 GUI、CLI 和全量角色资料 |
| `BetterEndfieldNext-4.0.6-win-x64.zip` | 本地保留 | Windows x64 便携包 |
