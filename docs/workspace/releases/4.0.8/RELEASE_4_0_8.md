# Better Endfield Next 4.0.8

本轮发布 **Windows 4.0.8、Android 4.0.8（40008）与 BEM Tools 1.6.1**。共分发 3 个正式产物。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

## 更新

- **BEM Tools 1.6.1**：
  - 修复 `bem_v15.py` 中的裸 NUL 字节，恢复模块导入；
  - 为 PyInstaller 补齐 `bem_v15` hidden import，并在冻结后执行真实 BEM 校验；
  - 旧版 BEM 1.0–1.4 校验不再依赖 1.5 模块加载；
  - 修复材质覆盖中的浮点/数值数组被通用 manifest 校验提前拒绝。
- **BEM 1.5 回归测试**：覆盖材质覆盖、根骨骼位移、1.0–1.4 兼容路径与 CLI 校验报告。
- **安装器升级清理**：清理 `tools/BemConverter/_internal`，避免旧 Python 运行时和 DLL 残留。

## 附件范围

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| `BetterEndfieldNext-4.0.8-Setup.exe` | GitHub Release | Windows x64 安装包 |
| `BetterEndfieldNext-4.0.8-Android-arm64.apk` | GitHub Release | Android arm64 安装包 |
| `BEM-Tools-1.6.1-win-x64.zip` | GitHub Release | 完整独立创作者 GUI、CLI 和全量角色资料 |
| `BetterEndfieldNext-4.0.8-win-x64.zip` | 本地保留 | Windows x64 便携包 |
