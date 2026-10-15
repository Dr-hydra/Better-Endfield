# 1.6.3 Windows 挂钩与资源更新

2026-10-15，当前工作区 `G:\Better Endfield`，游戏 `G:\Arknights Endfield`。版本从本机 `globalgamemanagers` 的 PlayerSettings 确认为 **1.6.3**，Unity 为 `2021.3.34f5`。本轮仅针对新版 Windows 客户端，不新增旧游戏兼容层。

## 挂钩和运行时

- 当前清单覆盖 180 个托管挂钩，179 个匹配到有效入口。唯一缺项 `CloudGameUtility.IsCloudGame` 是既有可选接口，旧资料同样不存在，不构成本轮新增故障。
- 新版 IFix 改为直接补丁表分派，已更新检查工具。最终没有可确认的失效内联入口；7 个部分内联和 29 个无直接调用的目标，按委托、Unity 消息及现有外层/叶函数覆盖分别记录，不把静态结果当作实机功能验收。
- 持续冲刺的 `_TryExit`、`_CheckTrackEnd` 已内联进主流程。改挂仍被该流程调用的 `_TryGetCurInterruptCommand`、`_DoExit`，保留原有持有者和自然结束门槛。移除冗余状态中断入口，保留已证实的 `ShouldInterruptSpDash` 委托钩。
- 战斗统计改用新版 Proto 字段及实际集合布局；属性范围扩至 108；将原生修正类型值映射到内部八槽；补充新减免来源归因和物理异常倍率。
- MMD 地面辅助修正 `FindFloorResult` 内嵌 `RaycastHit` 的布局验证。该项是现有验证器误拒绝，本轮核查时修复。
- Host 所需 29 个 IL2CPP 导出均存在。另核对 260 个直接写在源码中的方法描述符及 17 个字段描述符；两项缺失均为可选接口，其中纹理 getter 属 Android 条件代码，不能据 Windows 输入判定 Android 状态。

详见[挂钩证据](HOOK_AUDIT.md)和[字段、公式证据](RUNTIME_CONTRACT_AUDIT.md)。没有生成全量 IL2CPP dump 或完整 IDA 数据库；IDA 仅对 5 个相关函数共 2,455 字节建立稀疏切片，其他工作使用定向元数据和局部反汇编。

## 资源工具链

正常入口仍为 `scripts/UpdateResourceManifests.ps1`，修复当前 BLC v5 布局、PCK 语言名编码及技能/Buff 的 pooled MemoryPack 读取。输入保存到 `inputs/1.6.3/Windows/`，人工采样的动画时长元数据继续复用。

| 资源 | 本轮结果 |
| --- | --- |
| 模型/动作 | 37 个角色及变体条目，4,719 条动作；新增 459、删除 2 |
| 四语短语音 | 660 个事件四语完整映射，比旧清单增加 22；映射 Media 均存在 |
| 战斗字典 | 35 个角色、269 个技能、84 把武器、276 件装备、407 个关卡 |
| Buff 来源 | 6,323 个配置完成结构化读取；706 个 Buff 的 1,959 条候选，2,772 条技能—Buff 关联 |
| 图片 | 仅提取 2 个目标 bundle，补齐 24 张技能/装备图；既有图片未覆盖 |

语音仍有 365 个具名事件缺对应 bank，明确保留缺口；这不是遗漏语言分包。当前头像来源没有祀和明河的新头像，未以其他图片代替。多来源 Buff 保留候选。完整资源说明见[增量更新记录](../../../../workspace/research/1.6.3/resource-update/RESOURCE_UPDATE.md)。

## 构建和检查

- `BetterEndfieldNext.Actions`、`BetterEndfieldNext.CombatStats`、`BetterEndfieldNext.Camera` 的 Windows Release 构建通过。
- 现有 Actions 策略测试通过；资源格式与 MemoryPack 读取共 6 项针对性测试通过。
- 现有战斗导出入口复用本轮已刷新的输入完成导出及内置检查，未重复提取配置。
- 构建目录：`build/game-update-1.6.3/native/stage/Release/modules/`；资源导出日志：`build/logs/game-update-1.6.3-combat-export.log`。

未启动游戏进行端到端验证，未覆盖安装目录或发布安装包。本轮静态适配与资源结果针对给定 Windows 安装，未声明 Android 新版已验证。保留用户原有未提交改动，没有计算额外产物哈希。
