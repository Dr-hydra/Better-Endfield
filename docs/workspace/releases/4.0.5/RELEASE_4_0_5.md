# Better Endfield Next 4.0.5

本轮发布 **Windows 4.0.5、Android 4.0.5（40005）与 BEM Tools 1.5.5**。共分发 3 个正式产物。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

## 更新

- **多启动器支持与免启动器直接拉起**：
  - 支持官方及各渠道服启动器自动扫描与下拉列表切换，支持记住用户手动选择的有效启动器。
  - 新增“跳过官方启动器”设置项；在免启动器模式下使用 XInput 时，启动器辅助进程自动请求管理员权限静默拉起游戏，保证功能正常。
  - 集成 Unity 输入系统独立指针路由策略（`AllPointersAsIs`），彻底根治 iOS / Moonlight 串流触控按钮按下不释放卡死的问题；解耦鼠标转触控模拟器，提供 `mouse_to_touch_enabled` 开关。
- **Android 1.6.4 全语种短语音目录同步**：
  - 提取小米设备 1.6.4 热更后 VFS 音频包，四语种各 1,359 个 Banks 均已通过 HIRC/Media 校验。
  - 解决韩语包缺失问题，四语种覆盖全部 35 个角色共 140 目录，映射率 100%。
  - 重构媒体屏管理页面，统一为 Better Endfield 原生 UI 风格。
- **BEM Tools 1.5.5**：
  - 合并 4.0.4 补充包资料，昼雪、祀、明河全部整合入主包，不再分发单独数据补丁包。

## 附件范围

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| `BetterEndfieldNext-4.0.5-Setup.exe` | GitHub Release | Windows x64 安装包 |
| `BetterEndfieldNext-4.0.5-Android-arm64.apk` | GitHub Release | Android arm64 安装包 |
| `BEM-Tools-1.5.5-win-x64.zip` | GitHub Release | 完整独立创作者 GUI、CLI 和全量角色资料 |
| `BetterEndfieldNext-4.0.5-win-x64.zip` | 本地保留 | Windows x64 便携包 |
