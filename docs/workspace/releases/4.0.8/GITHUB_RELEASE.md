Better Endfield Next **4.0.8** 与 BEM Tools **1.6.1**，修复 1.6.0 冻结包无法导入 `bem_v15` 导致 CLI 校验与打包失败的问题，补齐 BEM 1.5 回归覆盖，并改进安装升级清理。

## Windows 4.0.8 & Android 4.0.8

- **安装升级清理**：升级时清理旧的 `tools/BemConverter/_internal` 运行时目录，避免旧 Python、DLL 与 GUI 文件残留。
- **Android 4.0.8（40008）**：沿用现有安装身份与发布签名，本次不涉及桌面语音模块调整。

## BEM Tools 1.6.1

- **冻结包修复**：清除 `bem_v15.py` 中的裸 NUL 字节，恢复模块导入；为 PyInstaller 补齐 `bem_v15` hidden import。
- **构建校验**：构建后检查冻结包的 `--version`、导入警告，并实际执行一次 BEM 校验。
- **BEM 1.5 回归**：修复材质覆盖中的浮点与数值数组被通用 manifest 校验提前拒绝的问题。
- **兼容性**：BEM 1.0–1.4 校验不再依赖 BEM 1.5 模块加载；新增 BEM 1.5 材质覆盖与根骨骼位移回归测试。

## 下载

| 附件 | 用途 |
| --- | --- |
| `BetterEndfieldNext-4.0.8-Setup.exe` | Windows x64 主程序安装包 |
| `BetterEndfieldNext-4.0.8-Android-arm64.apk` | Android 4.0.8 Release APK |
| `BEM-Tools-1.6.1-win-x64.zip` | 完整独立创作者 GUI、CLI 和全量角色资料 |
