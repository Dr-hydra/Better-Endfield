# Better Endfield Next 4.0.7

本轮发布 **Windows 4.0.7、Android 4.0.7（40007）与 BEM Tools 1.6.0**。共分发 3 个正式产物。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

> **致谢**：特别感谢 **@Saberverse** 在 BEM 1.5 格式规范设计、材质属性与关键字动态覆盖方案以及 NPC 路径适配中提供的关键技术支持与审阅材料。

## 更新

- **BEM 1.5 材质属性与关键字动态覆盖（material-overrides-v1）**：
  - 原生支持在模型绘制阶段对 Unity 材质属性（floats、colors、vectors）及着色器关键字（disable_keywords、enable_keywords）进行实例级动态覆盖；
  - 完美支持半透薄纱深度写入、水墨暗底色调校及高光抑制等复杂视觉需求，双端完全对齐。
- **骨骼根节点高度抬升（root-offset-v1）**：
  - 支持高跟鞋、厚底鞋与悬浮角色的根骨骼局部高度微调（root_offset），解决脚底插地穿模问题；
  - 内置 visual_pristine 基准防累加漂移与安全回滚保护。
- **会客室 / 帝江号 NPC 路径解包**：
  - 支持嵌套 Prefab 资源 `chr_0036_jsspsi02_postmodel` 正确定位并提取内部标准模型节点，彻底解决场景 NPC「祀」无法换装的问题。
- **客户端与「关闭模型校验」全链路联动**：
  - 桌面端 UI（BemPackageService / CustomModelPage）全面放行 BEM 1.5，页面文案同步更新；
  - 工具链与 Native 运行时端在开启 skip_validation 时自动放宽命名校验、执行数值饱和截断（Clamp）并扩容重载配额。
- **BEM Tools 1.6.0**：
  - 版本号前两位正式对齐游戏主版本（1.6.x），支持 BEM 1.5 标准打包、解包与校验。

## 附件范围

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| `BetterEndfieldNext-4.0.7-Setup.exe` | GitHub Release | Windows x64 安装包 |
| `BetterEndfieldNext-4.0.7-Android-arm64.apk` | GitHub Release | Android arm64 安装包 |
| `BEM-Tools-1.6.0-win-x64.zip` | GitHub Release | 完整独立创作者 GUI、CLI 和全量角色资料 |
| `BetterEndfieldNext-4.0.7-win-x64.zip` | 本地保留 | Windows x64 便携包 |
