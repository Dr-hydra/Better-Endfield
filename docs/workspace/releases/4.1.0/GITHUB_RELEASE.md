Better Endfield Next **4.1.0** 与 BEM Tools **1.7.0**，带来 BEM 1.5 连续动画与动效驱动（dynamic-drivers-v1）、原生单模型 ZIP 容器支持、双端启动自动更新检查与安卓端噗切娜主世界模型校验修复。

## Windows 4.1.0 & Android 4.1.0

- **BEM 1.5 连续动画与动效驱动（dynamic-drivers-v1）**：
  - 原生双端时钟驱动器与波形动画生成（PingPong、Sine、Sawtooth、Triangle 等），支持单次（once）与循环（loop）播放，停止时自动重新上传基准位姿安全恢复原形；
  - Android 端采用 SetNativeArrayForChannelImpl 安全多流顶点通道，丝滑播放道具抽插与身体形变动画，与滑条功能稳定共存；
  - 桌面端悬浮窗支持“开始 / 暂停 / 停止”三态控制，支持动效音频联动（driver_audio）；
  - 桌面客户端支持自定义独立快捷键并实现实时存储。
- **BEM 单模型 ZIP 容器原生支持**：
  - 双端运行时与工具链原生支持包含根 manifest.json 与 payloads/ 的单模型 .zip 容器文件，方便创作者打包分发与玩家直接加载。
- **双端启动自动检查更新**：
  - 桌面端与安卓端新增启动时自动检查更新功能，发现新版本时在状态栏或对话框提醒并提供一键直达下载页，可在“关于”界面手动检查更新或关闭自动检查。
- **安卓端噗切娜主世界 LOD 映射与模型校验修复**：
  - 补全噗切娜（chr_0038_purrche）全量 22 个组件在安卓端 UI LOD0 与主世界模型之间的映射关系，解决帽子部件（fur_02）在安卓主世界模型中保留 lod0 命名导致的校验失败与节点缺失问题。

## BEM Tools 1.7.0

- **动效驱动规范支持**：完整支持 BEM 1.5 动效驱动（dynamic-drivers-v1）规范导出、检验与构建。
- **ZIP 容器支持**：新增 pack-zip 与 unpack 命令，全面支持单模型 ZIP 容器打包与解包。

## 下载

| 附件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.0-Setup.exe | Windows x64 主程序安装包 |
| BetterEndfieldNext-4.1.0-Android-arm64.apk | Android 4.1.0 Release APK |
| BEM-Tools-1.7.0-win-x64.zip | 完整独立创作者 GUI、CLI 和全量角色资料 |