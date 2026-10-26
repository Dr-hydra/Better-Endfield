# Better Endfield Next 4.1.2

本版为 **Windows 4.1.2 与 Android 4.1.2（40102）** 的开发记录，BEM Tools 维持 **1.6.3**。
开发进行中，各项完成后逐条追加；构建与验证结果在候选包产出后补写。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)；产物清单见同目录 [ASSETS.json](ASSETS.json)。

## 更新

<!-- 每完成一项就往这里追加一条，格式与 4.1.1 发布记录保持一致。 -->

- **优化桌面端持续冲刺界面层级排布**：
  - 将「隐藏机甲与光效」选项与说明调整至「梨诺」设置项正下方并缩进展示，修正此前「祀」插入在梨诺与其子选项之间导致的排布与归属混乱；
  - 机甲光效子选项与梨诺启用状态联动，未启用梨诺时自动禁用子项，层级关系一目了然。

- **修复桌面端发布遗漏管理员外观模块动态库**：
  - 修复 `native/CMakeLists.txt` 中 `BetterEndfieldNext.Layout` 目标遗漏 `BetterEndfieldNext.WeaponAppearance` 依赖项的问题，确保 Windows 完整构建时自动编译该 DLL 并部署到 `modules/` 目录；
  - 在 `BuildBetterEndfieldNext.ps1` 发布前完整性检查清单中补齐 `BetterEndfieldNext.WeaponAppearance.dll` 与 `betterendfieldnext.weapon_appearance.module.ini` 等模块文件，避免构建与打包漏发。

- **管理员外观功能规范化：双端统一默认关闭**：
  - 桌面端 `WeaponAppearanceConfiguration` 默认启用状态、ini 读取缺省回退值以及 `betterendfieldnext.weapon_appearance.module.ini` 清单全部调整为 `enabled=false`；
  - 双端共享原生模块 `weapon_appearance/module.cpp` 的默认运行配置结构体同步调整为 `enabled = false`；
  - 与安卓端 `ModuleSettings.isAppearanceEnabled`（默认 `false`）完全对齐，消除未配置时自动覆写外观与特效的行为，双端统一遵循“默认保持原版、用户手动选择开启”的行为规范。

## 附件范围

候选包产出后填写。

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| BetterEndfieldNext-4.1.2-Setup.exe | 待定 | Windows x64 安装包 |
| BetterEndfieldNext-4.1.2-Android-arm64.apk | 待定 | Android arm64 安装包 |
| BEM-Tools-1.6.3-win-x64.zip | 待定 | 独立创作者 GUI、CLI 和全量角色资料 |
| dsh-bem-creator-1.6.3.tgz | 待定 | DeepSeek Harness 插件，安装创作者 skill；需与上面的 ZIP 一起使用 |

## 验证边界

- **桌面端构建与依赖检验**：CMake 编译目标 `BetterEndfieldNext.WeaponAppearance` 成功生成 `BetterEndfieldNext.WeaponAppearance.dll`；`BetterEndfieldNext.Layout` 成功包含该依赖；
- **配置与默认行为验证**：`WeaponAppearanceConfiguration` 实例与 ini 序列化确认 `enabled=false`；原生模块默认 `Configuration.enabled=false`；安卓端默认 `appearance_enabled=false`。
