# Better Endfield Next 4.1.0

本轮发布 **Windows 4.1.0、Android 4.1.0（40100）与 BEM Tools 1.6.2**。共分发 3 个正式产物。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

## 更新

- **BEM 1.5 连续动画与动效驱动（dynamic-drivers-v1）**：
  - 原生支持双端时钟驱动器与波形动画生成（PingPong、Sine、Sawtooth、Triangle 等），支持单次（once）与循环（loop）播放，停止时自动重新上传基准位姿并安全回归原形；
  - Android 端采用 SetNativeArrayForChannelImpl 安全多流顶点更新通道，丝滑播放道具及身体形变动画，并完美与滑条共存；
  - 桌面端悬浮窗支持“开始 / 暂停 / 停止”三态控制；
  - 支持动效音频联动播放（driver_audio）；
  - 桌面客户端支持自定义独立快捷键并实现实时存储。
- **BEM 单模型 ZIP 容器原生支持**：
  - 双端运行时与工具链原生支持解包、打包与加载包含根 manifest.json 与 payloads/ 的单模型 .zip 容器文件。
- **双端启动自动检查更新**：
  - 桌面端与安卓端新增“启动时自动检查更新”功能，发现新版本时在状态栏或对话框中提示，并提供直达下载页按钮；可在“关于”界面手动检查或关闭自动检查。
- **安卓端噗切娜主世界 LOD 映射与模型校验修复**：
  - 补全噗切娜（chr_0038_purrche）全量 22 个组件在安卓端 UI LOD0 与主世界模型之间的映射关系；
  - 解决帽子部件（S_actor_purrchena_fur_02_lod0）在安卓主世界模型中保留 lod0 命名导致的校验失败与节点缺失问题。
- **BEM Tools 1.6.2**：
  - 完整支持 BEM 1.5 动态驱动器（dynamic-drivers-v1）规范与 ZIP 单包打包 / 解包支持（pack-zip / unpack）。

## 附件范围

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| BetterEndfieldNext-4.1.0-Setup.exe | GitHub Release | Windows x64 安装包 |
| BetterEndfieldNext-4.1.0-Android-arm64.apk | GitHub Release | Android arm64 安装包 |
| BEM-Tools-1.6.2-win-x64.zip | GitHub Release | 完整独立创作者 GUI、CLI 和全量角色资料 |
| BetterEndfieldNext-4.1.0-win-x64.zip | 本地保留 | Windows x64 便携包 |