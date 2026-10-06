# PC 主世界首次启用模型的热切换修复

工作区 `G:\Better Endfield`，分支 `dev/bem-1.4-weapons-forms`。用户已确认上一轮修复后的武器与庄方宜大招可以正常加载；本轮问题是在主世界启用 Mod 没有反应。

## 实机证据与版本判断

18:50:25–18:54:49 会话保存于 `build/bem14/local-test/audit/hot-switch-20261006/runtime.log`，共 976 行。三次配置变化在 864、886、887 行被原生接收，但没有实例扫描、Job 创建、恢复或模型拒绝记录。随后再次交付的庄方宜/佩丽卡 UI 资源仍能提交。没有新崩溃；其他模块的闲置 Hook 诊断不能作为模型热切失败证据。

日志当时未记录变更资源身份，无法仅凭三条 accepted 确认每次具体改了哪个包。用户补充“主世界直接启用”，与下述可复现代码缺口一致。

`v3.5.0 → v3.5.1` 的匹配器、队列和 Android world adapter 没有变化；配置接收层的差异不足以证明本故障是 3.5.1 新回归。Android legacy 从 UI LOD0 donor 构建再绑定 world LOD1，PC 直接使用 world LOD0 donor。手机热切验收不能代替 PC 实测，本轮保持 Android 世界模型适配路径和 LOD1 设计。

## 可复现根因

禁用 Mod 时，游戏已经加载的 prefab/自然 Clone 没有被登记为模型 target；扫描又只认识生成 Mesh 和曾恢复 Original 的实例。首次启用能更新配置，却没有任何已知实例可更新，已有主世界角色保持原样。必须主动发现该变更资源已经加载的实例。

此前修复覆盖的是“曾替换→停用→再启用”，没有覆盖“从未替换过的资源→首次启用”；两者需要不同的实例发现证据。

新增回归还暴露一个调度缺陷：只发现 Clone 并取消模板 Job 后，模板残留 `desired=当前选择`，会被永久跳过。若随后禁用，该从未提交的模板反复尝试恢复，还可能阻塞其他实例。取消 Job 时现在清理对应 target 的未完成状态。

## 实现

- 配置接收时记录具体变更 owner、world/UI 资源、状态、package 和 revision；资源名按值累计，支持首个 pump 前变更及连续多次变更。
- Windows 对本轮变更且当前启用的资源发现已加载 prefab/Clone，包括未登记、从未替换的原生实例。要求真实 GameObject、精确资源根以及全部 receiver 类型/路径、pristine Mesh/索引、材质/shader、骨骼根归属通过，随后再进入完整生产事务，不按名称直接提交。
- 扫描 API/数组长度读取失败时保留待发现资源，一秒后重试；空数组与调用失败分别处理。
- 每个合格同步根入队都取消同 adapter 异步 Job，并清除被取消 target 的 `desired`/验证状态。pending 队列仍禁止同角色重建，处理时使用最新配置；已知失败选择保护保留。
- Android 不增加这条首次启用发现路径，也没有改变其 UI→world LOD1 适配。

## 验证

同一测试源码分别编译于精确修前 HEAD `7b9f918` 和修后源码。使用真实 `InstallRegistryUpdate → PumpModelJobs → discovery → ProcessResource`；Unity 对象与 GPU 上传由有类型/参数约束的 mock 提供。

| 场景 | 结果 |
| --- | --- |
| 修前首次启用 | exit 1，cached prefab 与自然 Clone 均保持 Original |
| 修后普通首次启用 | legacy world/UI、显式 body/ultimate 蒙皮、weapon 静态均通过 |
| 队列处理中 disable→enable | 收敛到最新选择，通过 |
| 首个 pump 前启用 | 不当作启动基线丢弃，通过 |
| FindAll 暂时返回 null | 保留待处理变更，实际一秒重试后通过 |
| 模板不在 FindAll，仅发现自然 Clone | 取消模板 Job 后可补建，停用恢复通过 |
| 错根/路径/类型/索引/材质/LOD/外部骨骼/死亡及同名 Sprite | 保持原绑定，没有越界替换 |
| 原有完整 binding、async、scene-rebind、resource-types、static | 全部通过 |

上述五个正向场景各运行 legacy 与显式资源，共十个场景，检查生产提交、停用 Original 恢复与 GC 句柄清理。`Array.GetLength` 的 dimension 参数也由 mock 强制校验。

红绿及构建日志均在 `build/bem14/local-test/audit/hot-switch-20261006/`：`first-enable-before.log`、`first-enable-after.log`、`binding-full-after.log`、`binding-async-after.log`、`windows-final-build.log`、`android-final-build.log`。测试用 v14 包是虚构资源的 Windows LOD0 合同，未安装为用户 Mod。

本轮回归证明该漏扫路径已修复，不等于游戏画面验收。真实主世界首次启用、停用、再次启用仍需在新进程中验证。用户当前配置保持原样，没有计算产物哈希，正式 Release 未更新。

## 本机部署

19:27 已更新 `E:\Better Endfield\modules\BetterEndfield.CustomModel.dll`，同步刷新本机 publish 副本。文件逐字节复核一致，`runtime.ini` 部署前后逐字节一致，没有启动或重启游戏。原文件备份在 `build/bem14/local-test/deploy-backup/20261006-192706-pc-first-enable/`，回执在本轮审计目录 `deployment.json`。Windows 最终构建及其首次启用/scene/async 独立回归通过，Android 最终原生构建通过；既有平台警告保留。
