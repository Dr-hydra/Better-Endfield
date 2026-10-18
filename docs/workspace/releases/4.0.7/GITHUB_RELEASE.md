Better Endfield Next **4.0.7** 与 BEM Tools **1.6.0**，正式引入 BEM 1.5 材质覆盖与根骨骼位移扩展规范，解决角色高跟鞋插地与复杂视觉表现，支持会客室 NPC 嵌套模型路径，并实现关闭模型校验（`skip_validation`）全链路联动。

> **致谢**：特别感谢 **@Saberverse** 在 BEM 1.5 格式规范设计、材质属性与关键字动态覆盖方案以及 NPC 路径适配中提供的关键技术支持与审阅材料。

## Windows 4.0.7 & Android 4.0.7

- **BEM 1.5 材质属性与关键字覆盖（`material-overrides-v1`）**：原生支持在模型绘制（Draw）阶段对 Unity 材质属性（`floats`、`colors`、`vectors`）及着色器关键字（`disable_keywords`、`enable_keywords`）进行实例级动态覆盖；完美支持半透薄纱深度写入、水墨暗底色调校及高光抑制等复杂视觉需求，双端完全对齐，零侵入无粉屏风险。
- **骨骼根节点高度抬升（`root-offset-v1`）**：支持高跟鞋、厚底鞋与悬浮角色的根骨骼局部高度微调（`root_offset`），自动解决脚底陷入地面的插地问题，内置 `visual_pristine` 基准防高度累积漂移与安全回滚保护。
- **会客室 / 帝江号 NPC 路径解包**：支持嵌套 Prefab 资源 `chr_0036_jsspsi02_postmodel` 正确定位并提取内部标准模型节点，彻底解决场景 NPC「祀」无法换装的问题。
- **客户端与「关闭模型校验」全链路联动**：桌面端 UI 全面放行并识别 BEM 1.5 模型包，页面文案同步更新；工具链与 Native 运行时端在开启 `skip_validation` 时自动放宽命名校验、执行数值饱和截断（Clamp）并扩容重载配额。

## BEM Tools 1.6.0

- **版本号对齐**：工具链版本前两位正式跟随游戏版本走（对齐当前 1.6.x 架构）。
- **纯净 BEM 1.5 导出与验证**：内置独立 GUI 和 CLI，支持 `material_overrides` 与 `root_offset` 的打包、解包、转换与全量规则校验。

## 下载

| 附件 | 用途 |
| --- | --- |
| `BetterEndfieldNext-4.0.7-Setup.exe` | Windows x64 主程序安装包，包含 BEM 1.5 材质覆盖与 NPC 路径修复 |
| `BetterEndfieldNext-4.0.7-Android-arm64.apk` | Android 4.0.7 Release APK，包含 BEM 1.5 双端原生支持 |
| `BEM-Tools-1.6.0-win-x64.zip` | 完整独立创作者 GUI、CLI 和全量角色资料（版本跟随游戏 1.6.x） |
