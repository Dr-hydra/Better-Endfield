# Windows 1.6.3 挂钩审计与动作入口修复

当前客户端：`G:\Arknights Endfield`，GameAssembly PE 标识 `6abd2a51-1349d000`（时间戳及映像大小，不是哈希）。只提取源码涉及的类和方法；未生成全量 dump 或全量 IDA 数据库。

## 结论

Windows 清单由 137 项更新为 **180 项**：补齐投影屏 41 项与模型克隆观察 3 项；动作模块移除一个冗余入口并替换两个已内联入口。最终 179 项唯一解析到非零 RVA；唯一缺项是既有可选的 `Beyond.CloudGameUtility.IsCloudGame()`，不是本次更新新增的缺失。

静态结果为 `ok 131`、`partial 7`、`no-direct-callers 29`、`wrapper 12`、`unresolved 1`，没有可信的 `never`。无直接调用不等于失效，Unity 消息及委托入口必须保留这一判断边界。

| 模块 | 挂钩数 |
|---|---:|
| voice | 18 |
| actions | 22 |
| camera | 13 |
| combat_stats | 21 |
| custom_model | 8 |
| model | 15 |
| music | 13 |
| ui | 29 |
| media_screen | 41 |

## 已实施的动作模块修复

| 原入口 | 新版证据 | 处理 |
|---|---|---|
| `_TryExit` `0x5342950` | 无直接调用，内联进 `_TickMainFlow` | 改挂 `_TryGetCurInterruptCommand` `0x53429A0`，只过滤当前持有冲刺的 `SpDashEnd + ForceExit` 命令 |
| `_CheckTrackEnd` `0x5345820` | 无直接调用，内联进 `_TickMainFlow` | 改挂 `_DoExit` `0x68E9390`，保留同一 handle、当前主流程、活动策略、保持条件、无显式命令、实体存活、End 阶段的全部延期门槛 |
| `_TickStatePerformInterrupt` `0x5172410` | 内联进 `PreLateTick`，但仍通过委托调用冲刺中断谓词 | 删除冗余入口钩，保留 `ShouldInterruptSpDash` 委托目标钩 |

两个新入口各有 3 个直接调用点。`_TryGetCurInterruptCommand` 另有一份内联于 `_RecoverEntityState`；清理恢复路径保留原行为，需要拦截的主流程直接调用新入口。

委托目标已核实：`_DoPlaySpDashPerform` 传入 condition 3；`CharacterAnimationComponent.cctor` 为字典 key 3 创建的委托方法槽为 `0x106A13D8`，编码 `0x6001C795` 解码为 `ShouldInterruptSpDash`（method index 58314，token `0x0600E3CB`）。

局部 IDA 仅导入 5 个函数和冷块，合计 **2,455 字节代码**，稀疏 IDB 约 225 KiB。辅助伪代码没有完整恢复所有类型，调用签名应以元数据和原指令为准。

## 其他发现

- 两份语音时长内联副本均调用 `_GetVoDurationFromVoData`，现有 voice 叶函数钩已覆盖；原生 Wwise 卸载缓存槽的解析模式仍存在。
- `RecordCastSkill` 内联于 `Skill.DoCast`，现有战斗统计优先挂 `DoCast`，已覆盖。
- `SolverManager.LateUpdate` 使用了借入 helper 的入口指纹，不能据此判定 Unity 回调失效。
- 登录 `ChangeState`、材质 `LateTick`、控制台平台布尔值以及动作 `ForceStopCurrentStatePerform` 存在部分内联。本次没有只凭部分内联扩大拦截；登录每帧/渲染前修正及动作会话所有权检查仍保留。这些额外路径属于运行时观察项。

1.6.3 的直接 IFix 补丁表分派无法沿用旧 ID 扫描。本次工具新增“全局 root + table 偏移 + 函数/上下文槽”指纹，识别出 104 个目标入口，并排除借入 helper 指纹造成的确定性误报。工具支持定向方法 JSON；方法数据重读，PE 索引可复用。

## 验证与证据

Actions Windows Release 构建成功；现有生命周期策略测试通过，包含自然结束延期各门槛的反例。更新清单后完成一次最终扫描。未启动游戏进行端到端测试。

- [完整研究记录](../../../../../research/host/1.6.3/Windows/hook-update/HOOK_AUDIT.md)
- [定向元数据](../../../../../research/host/1.6.3/Windows/hook-update/targeted-metadata.json)
- [完整扫描结果](../../../../../research/host/1.6.3/Windows/hook-update/hook-inline-scan.json)
- [局部 IDA 伪代码](../../../../../research/host/1.6.3/Windows/hook-update/actions-local/pseudocode.c)
- [局部 IDA 脚本](../../../../../research/host/1.6.3/Windows/hook-update/actions-local/decompile_slice.py)
- [运行时字段及战斗公式审计](RUNTIME_CONTRACT_AUDIT.md)

Actions 产物：`build/game-update-1.6.3/native/stage/Release/modules/BetterEndfieldNext.Actions.dll`。
