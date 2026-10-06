# BEM 1.4 创作者工具、文档与 Android 同步

2026-10-06，工作区 `G:\Better Endfield`，分支 `dev/bem-1.4-weapons-forms`。PC 武器、大招及热切换速度已获用户验收；本轮补齐创作者分发遗漏和 Android 显式资源首次启用路径。

## 创作者入口

| 入口 | 当前能力与边界 |
| --- | --- |
| `pack/unpack/build/validate` | 支持 1.4 显式多资源、静态/蒙皮网格，以及已有选项和位置形变 |
| `inspect/validate --resource/--platform` | 输出筛选计划；校验仍检查整个包，不生成筛选后的包 |
| `bundle` | 已修复旧 `character_id` 报告字段导致 1.4 包失败；支持旧角色、1.4 角色和静态武器的混合集合 |
| `target-profile` | 可从 Android 或双平台原生图谱生成未验证起点；只从原生 bundle 路径记录来源平台，未知来源不伪造证据 |
| 通用 EFMI `convert` | 保留旧角色 catalog/recipe 路线，尚未泛化为任意武器或大招转换器 |
| Blender 插件 | 保留旧角色 16/12/12 蒙皮导出，尚不直接生成 1.4 资源表或静态武器 |

1.4 顶层 `target.platform` 保留历史格式常量 `windows-x64`，实际平台按 `resources[].platforms` 判断。生成器没有把占位字段误当作来源证据，也不提高 `runtime_verified`、`conversion_ready` 或 `render_verified`。

已同步中英文创作者指南、源 Mod 转换说明、运行时兼容文档、1.4 规范、工具 README 和 `bem-creator` Skill。九份 Skill 引用文档由主文档同步，并修正对应目录的链接。Windows 打包入口也补上主帮助目录中的 1.4 规范，避免指南链接只有工具子目录才能访问。

旧正式 `releases/3.5.1/BEM-Tools-1.5.0-win-x64.zip` 没有 1.4 文档和示例，本轮没有覆盖它。新测试工具包的 `--version` 输出包含 `BEM 1.0+1.1+1.2+1.3+1.4`；不要只凭工具版本号判断支持范围。

## Android 同步

Android 的 1.4 元数据读取、导入、平台筛选、资源冲突、武器目标呈现、贴图重写和 own-donor 冷加载原先已接线，不能把它们都当作本轮新增。

真实缺口在首次启用发现：相关预检和扫描原先被 Windows 编译条件排除。现改为共享平台策略，Android 只发现 1.4 显式资源，按声明的 LOD、精确 renderer 路径、Mesh/index、材质、骨骼和真实 GameObject 类型预检；静态与蒙皮均覆盖。Windows 默认策略和最终提交仍保持显式 LOD0 限制。

Android 旧包 UI LOD0→world LOD1 适配保留；本轮没有把旧角色的首次启用发现改成 own-donor 算法。显式包可使用自身 LOD1 donor。模块停止时清除待重绑强引用、待发现资源和扫描 revision/retry 状态，避免队列跨模块生命周期残留；不改变停模块时保留已发布模型的策略。

新增 Android 管理逻辑回归覆盖普通形态、大招、武器共同启用，Windows-only 资源过滤、跨 owner Android 资源冲突、参数/选项与 runtime 路径顺序，以及全停用状态。没有修改 UI 说明、设置 key、签名或版本号。

## 验证

- 创作者 bundle/工程回归 22 项，target-profile 回归 11 项通过。M0178、M0184、Nait3D 真实 Windows 样本解包→重打包的完整 manifest 与解码 payload 一致，原文件与验证标志未改变。
- 新独立 EXE 已验证内置合成工程构建、Windows ultimate 与 Android weapon 计划、编辑工程往返、旧版与 1.4 ZIP 打包/检查/解包、Android target-profile→工程→包，以及文档/Skill/示例包含情况。
- 原生 Android discovery 与实际 Shutdown 生命周期回归通过；既有 AndroidWorld 213 项、AndroidLOD 68 项、完整 binding、async、Windows first-enable 均通过。
- Android overlay host 四组通过；安装状态检查包含 122 项 BEM、35 项第三方模块及 hot-switch/overlay 检查，全部通过。

原生日志位于 `build/bem14/android-discovery-audit/`；创作者冻结构建、烟测、Windows 模块和 Android APK 构建日志位于 `build/bem14/local-test/creator-sync/`。未计算产物哈希。

## 本机交付与实机边界

新创作者 ZIP 位于 `build/bem14/local-test/creator-sync/artifacts/3.5.1/BEM-Tools-1.5.0-win-x64.zip`，完整可执行目录位于其 `build/dist/BetterEndfield.BemConverter/`。Android 测试 APK 使用原 Release 签名，交付到 `build/bem14/local-test/creator-sync/BetterEndfield-3.5.1-bem1.4-Android-arm64.apk`；构建排除正式 Release 归档任务。

Windows 模块和 Android `assembleRelease` 均构建成功。Android APK 的两个 arm64 原生库逐字节核对编译后的 stripped 输出。本机 `E:\Better Endfield` 的创作者 CLI 与帮助文档同步为此版本，先备份原工具及帮助文件，逐文件核对工具副本；玩家 `runtime.ini` 保持逐字节一致。回执为本轮测试目录中的 `deployment.json`，没有重新安装 Mod 或启动游戏。

本机未连接 ADB 设备，Android 尚无本轮手机画面验收。新原生发现回归使用合成资源；真实 Windows 样包不能通过添加 Android 标签伪装成已核实手机 donor。Android 武器/大招还需要相应手机资源合同和实际启用、停用、再次启用的画面验证。
