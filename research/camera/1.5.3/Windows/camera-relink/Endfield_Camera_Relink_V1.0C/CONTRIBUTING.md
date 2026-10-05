# 贡献指南

先看 [USAGE_BOUNDARY.md](USAGE_BOUNDARY.md)：本项目**只做"单机／拍照模式的取景与镜头联动"**。
以下类型的需求**一律不受理**，提了也会被关掉：

- 联机／竞技相关用途
- 修改数值、物品、货币、角色属性
- 绕过或对抗反作弊
- 账号安全、代练、脚本挂机

## 报告问题（Issue）前请准备

1. **版本信息**：`VERSION.txt` 顶部"发布名称 / 包版本"，以及 `blender_addon\endfield_camera_bridge\__init__.py` 里的插件版本号
2. **现象**：期望什么、实际什么；能稳定复现的操作步骤
3. **日志尾部**：`相机控制台.cmd` → 菜单 **[5] 查看运行日志**（贴最后 30~50 行即可）
   - 注意：**贴日志前请自行删掉任何个人信息**（本机路径、UID 等）
4. **环境**：Windows 版本、游戏版本、Blender 版本

## 提交代码（Pull Request）

- 改模块源码（`source\src`）请附上**构建与实测结果**：`source\build.ps1` 能编译通过 + 游戏内验证过的现象
- 改脚本（`.ps1`）请确保 **UTF-8 BOM** 没丢（详情见 `docs\11_开源与发布检查清单.md`；Windows PowerShell 5.1 对无 BOM 的中文脚本会乱码）
- 改文档请同步 `VERSION.txt` 的对应条目
- 一次 PR 只做一件事，便于审查

## 提交前的自查

```powershell
# 1) 有没有把本机配置/日志/二进制带进来(应当没有任何输出)
git status --short
git check-ignore -v paths.ini game_mod\EndfieldCamLink.log game_mod\EndfieldCamLink.dll

# 2) 源码能不能编译(source\build.ps1 需要 MinGW-w64 g++)
powershell -ExecutionPolicy Bypass -File source\build.ps1 -OutDir build_ui
```

## 许可

提交代码即表示你同意以本项目的 [MIT 许可](LICENSE) 发布你的贡献；
随包第三方组件仍适用其自身许可（见 [NOTICE](NOTICE)）。
