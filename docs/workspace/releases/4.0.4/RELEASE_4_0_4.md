# Better Endfield Next 4.0.4

本轮发布 **Windows 4.0.4、BEM Tools 1.5.4 和角色资料补丁**，面向当前终末地 Windows 1.6.3。Android 同步版本为 4.0.4 / versionCode 40004，仅生成本地 APK，本次不上传 Android 附件，语音资源后续补充。Next 安装身份和签名沿用现有配置。

公开发布正文见 [GITHUB_RELEASE.md](GITHUB_RELEASE.md)。

## 更新

- **Steam 自动配置**：应用 Steam 国服配置时自动写入当前账号的启动选项；更换加载方式、路径或升级后，Steam 完整退出时更新命令。启动前检查实际配置，避免直接运行空占位 EXE 导致“平台无效”。保留原选项，移除 BE 配置时恢复，不改其他账号或游戏的配置。
- **已有文件处理**：正确 AppID 的 `steam_appid.txt` 可保留并直接更新，普通移除也保留该辅助文件。其他冲突需先显示完整目标路径、文件数与大小，用户确认后清理并重建；自动更新不执行强制清理，确认后文件变化会拒绝执行。
- **启动窗口**：从 BE 或 Steam 调用 Launcher / Injector 时隐藏常驻控制台，保留启动错误提示和游戏生命周期跟踪。
- **昼雪旧 BEM**：Windows 增加准确的 1.5.3 → 1.6.3 兼容规则，适配 C1 cloth_01 原索引数 `87120 → 87288`、顶点数 `23073 → 23141` 及 10 个骨骼槽位重排。核对当前 renderer 路径、87 根骨骼及 bindpose、材质后重定位 donor 索引；旧包自定义 palette 和顶点权重不改。正常加载、keep/hide、跨组件借骨骼、Job、首次启用与热切换共用检查，不对其他角色或未知变化放宽验证。
- **持续冲刺**：纳入用户已经完成的 Actions 1.13.6 修复。新版缺少的 `AnimatorStateInfo.get_loop` 只用于日志，现在缺失或调用失败时记为 `unknown`，不会使整个动作模块或有效帧读取失败；核心状态、时间、长度读取仍必须成功。
- **BEM Tools 1.5.4**：昼雪当前世界/详情原生资料已分别采集，共 65 条 renderer，更新原 12 组件目录、11 个索引身份与 41 个纹理身份，新增世界/详情共 24 部件的 BEM 1.4 keep-only 工程。保留祀、明河资料，独立工具、BE 内置资料和数据补丁同步分发。

昼雪 C5 眉毛的当前运行时流步幅为 `[40,16,32]`。正式 catalog 中 8/12 个部件符合现有来源自动转换布局；未支持项不伪装为可转换。旧 BEM 替换网格沿用包自己的声明，与新源 Mod 自动转换是否受支持是不同路径。新显式工程保持 keep-only，其 `runtime_verified`、`conversion_ready`、`render_verified` 标记不因原生采集或旧包验收而擅自改为 true。

## 附件范围

本地产物目录为 `releases/4.0.4/`。最终文件和构建记录由发布流程复核，本记录不计算产物哈希。

| 文件 | 分发范围 | 用途 |
| --- | --- | --- |
| `BetterEndfieldNext-4.0.4-Setup.exe` | GitHub Release | Windows x64 安装包 |
| `BetterEndfieldNext-4.0.4-win-x64.zip` | 本地保留 | Windows x64 ZIP 包 |
| `BEM-Tools-1.5.4-win-x64.zip` | GitHub Release | 完整独立创作者 GUI、CLI 和角色资料 |
| `BEM-Tools-1.5.4-data-patch.zip` | GitHub Release | 仅 `catalog/` 与 `profiles/` 的角色资料补丁 |
| `BetterEndfieldNext-4.0.4-Android-arm64.apk` | **仅本地，不发布** | Android 4.0.4 / 40004，后续补语音 |

数据补丁可按包内 `README-DATA-PATCH.md` 覆盖到既有 `BEM-Tools` 根目录或 BE 的 `tools/BemConverter` 目录；覆盖前保留自己修改过的同名资料。它不包含 GUI、CLI 或运行时 DLL，不能代替 BE 4.0.4 的昼雪旧包兼容更新。需要完整工具时使用完整 ZIP。

## 已完成的验证

- **发布构建**：Windows UI 4.0.4.0 的 Release 构建、签名、隔离 Steam 页面和 8 类发布态 JSON 检查通过。Android 4.0.4 / 40004 Release APK 已本地生成，大小 6,066,313 字节，本次不上传、不新增手机实机验证结论。
- **Steam**：204 项合成服务检查通过，包含账号选择、选项恢复、外部改动、正确 AppID 辅助文件、显式清理、链接和目录重叠保护、取消、Steam 中途启动及新增文件保留。正式包的混淆 DTO 往返与隔离 Steam 页面检查通过；用户确认 Steam 修复可用。计时、覆盖层和 Steam Input 未在本轮单独验收。
- **昼雪原生资料**：分别使用开屏目标加载 world/UI，两次独立进程获得 65 条完整 renderer 记录。两根 C1 的骨骼名、相对路径、87 个 bindpose 数组完全一致；运行时矩阵排列与离线 graph 的转置关系已按实测校准。
- **昼雪兼容代码**：666 项生产路径检查和 228 项纯策略检查通过，包含 keep/hide、跨组件、SavedOriginal、Job、发现路径及未知变化拒绝；既有同步、异步、首次启用、geometry、clone、NPC 回归通过。
- **真实旧包显示**：12:52:02，实际开屏替换提交 `chr_0014_aurora_postmodel` 的 12 个组件，构建 3 个替换网格；用户随后反馈“可以，通过”。游戏于 12:52:29 正常退出，退出码 0。这是代表旧包的开屏 world 模型验收；UI 记录证明原生资料采集，不等同于旧包详情页替换画面已经验收，也没有逐一运行全部七份包。
- **BEM Tools**：1.5.4 完整 ZIP 在独立便携目录中检查版本，catalog/profiles 与源资料逐字节一致；祀、明河和昼雪的 6 套工程均 pack/validate 通过。结果见 `build/release-4.0.4/bem154-verification/summary.json`。
- **Actions**：本轮仅纳入并编译用户已完成的修复，未重复运行冲刺测试或新增游戏内冲刺验收。既有审计确认 85 个方法、13 个实例字段、12 个枚举、4 个静态动画 hash 字段、29 个 Hook ABI 预期与 8 个导出匹配，唯一缺少的方法是可选诊断 `get_loop`。

真实旧包日志的精简证据，来源于 `build/aurora-compat-1.6.3/live/local/BetterEndfieldNext/logs/BetterEndfieldNext.log` 第 726、737 行：

```text
12:52:02 Aurora legacy donor accepted resource=chr_0014_aurora_postmodel oldIndices=87120 currentIndices=87288 vertices=23141 palette=87 remappedSlots=10
12:52:02 Resource committed: chr_0014_aurora_postmodel components=12
```

## 依据与未完成项

- [昼雪静态差异与七份旧包目标对照](../../../custom_model/research/1.6.3/aurora-bem-investigation/STATIC_FINDINGS.md)
- [昼雪创作资料与转换边界](../../../../tools/CustomModel/profiles/endfield-1.6.3/SNOWSHINE.md)
- [Steam 使用与恢复](../../../host/STEAM_CN_LAUNCH.md)
- [持续冲刺接口审计](../../../../research/host/1.6.3/Windows/actions-contract-fix/CONTRACT_AUDIT.md)

Android 本轮不发布、不声称新增语音已齐备；昼雪精确兼容仅限 Windows，未新增 Android 昼雪适配或实机验收结论。
