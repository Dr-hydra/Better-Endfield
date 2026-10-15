# BEM 链路核查：BE 4.0.3

2026-10-15。Windows 游戏 1.6.3，Android 游戏实际为 1.6.4。检查范围是导入与解析、配置到运行时、资源匹配、网格/材质/骨骼构建和停用恢复。没有发现本轮需要修改的 BEM 生产代码；只修复了一个陈旧测试工程的生产服务引用，并保留受限的隔离导入入口。

## Windows 实际加载

使用手机现有的凛 → 洁尔佩塔包（包版本 1.2.6，BEM 格式 1.0）作为代表性旧包。它的实际 manifest 是 Windows 角色目标；Android 通过既有 legacy donor 适配使用同一包。没有将安装登记中的平台资源键误认成 BEM 1.4 的显式资源声明。

1. 使用生产转换器严格校验原始包，再由生产 `BemPackageService.ImportAsync` 导入隔离库。原包未重新打包；严格校验保持开启。
2. 使用发布包中的原始 4.0.3 Injector、Bootstrap、Host、Model 和 CustomModel 二进制。仅该次进程的 `LOCALAPPDATA` 指向测试目录，正式 BE 配置与模型库不变，未安装 XInput。
3. 通过开屏 Model 路径加载 `chr_0013_aglina_postmodel`，不依赖服务器进入队伍或战斗。
4. 10:18:19 记录 `Resource committed`，10 个组件提交成功，实际构建并上传 3 个网格、15 张纹理，事务 203 ms。10:18:21 完成资源投递登记。窗口显示替换轮廓，用户反馈“看上去应该没有问题”。开屏呈现样式仍限制了材质色彩的完整画面验收。
5. 测试进程 PID 20452 于 10:19:04 以退出码 0 结束。开屏模块记录原层级恢复与资源释放；这属于开屏清理，不能替代 BEM 热切换恢复证据。测试进程已在隔离配置停用前结束，因此本轮未完成同一游戏进程中的“停用→重新启用”实测，没有为了补此项反复重启。

正式 `BetterEndfieldNext.ini` 与 `ui-settings.json` 前后逐字节一致；测试后游戏目录仍无 XInput 代理。未接管后来启动的其他游戏进程。

证据：[精简运行日志](../../../../../research/custom_model/1.6.3/Windows/bem-chain-audit/live-evidence.log)、[结果记录](../../../../../research/custom_model/1.6.3/Windows/bem-chain-audit/live-result.json)、[开屏截图](../../../../../research/custom_model/1.6.3/Windows/bem-chain-audit/rin-enabled.png)。

## 静态合同与离线事务

| 检查层 | 结果 |
| --- | --- |
| Windows 新版合同 | 88/88 方法匹配，8/8 挂点解析；65 个必需方法无缺失 |
| Windows 原生网格 | 当前 UnityPlayer 字节映射解析 `m_BonesPerVertex=0x1C8`，两条独立序列化证据一致；关键值类型布局匹配 |
| Android 新版合同 | 69/69 方法、10/10 Unity 字段匹配；当前 libunity 原生字段 `0x1C0`，两条独立证据一致 |
| 导入/管理 | 严格校验、ZIP、启停、更新、冲突与持久化通过；坏包保留原库 |
| BEM 1.4 | 共享解析/逐资源目标等 53 项通过；Windows LOD0 与 Android LOD1 测试输入分别符合生产边界 |
| 绑定与恢复 | 当前生产源码的热切换、首次启用、克隆、NPC、失败回滚与停用恢复模拟通过 |
| Android 世界绑定 | 实际 Android adapter 的 213 项主机模拟通过；Java 选择写入及热切换策略通过 |

Windows 一个可选克隆入口没有直接调用者，未据此宣称入口失效；真实替换走已确认的同步资源交付。完整方法、字段、构建命令与测试证据见 [Windows 静态报告](../../../../../research/custom_model/1.6.3/Windows/bem-chain-audit/STATIC_AUDIT.md)、[离线链路记录](../../../../../research/custom_model/1.6.3/Windows/bem-chain-audit/offline-validation.md)和 [Android 报告](../../../../../research/custom_model/1.6.4/Android/bem-chain-audit/ANDROID_BEM_CHAIN_AUDIT.md)。

## 保留的验证边界

- Android 缺语言资源，用户明确当前不能进入游戏；本轮没有启动手机、修改其配置或下载语言包。API 与布局通过不代表新版实际 donor 的网格、材质、骨骼和 UI→world LOD1 切换已运行验证。
- Android 生成的 LOD 对应表仍来自既有资源快照，未将其标成 1.6.4 新采集资料。严格运行时校验仍会拒绝不匹配的绑定并保留原模型。
- 本次 PC 代表包通过，不等于所有旧角色包、BEM 1.4 武器/形态和新角色都完成实机验收。新角色 352 条既有采集资料证明读取/布局成功；待补来源身份的资料和仅 staging 的目录没有晋级为正式转换支持。
- 没有建立全量反编译数据库，没有额外计算产物哈希，没有因为检查而修改 4.0.3 发布包。
