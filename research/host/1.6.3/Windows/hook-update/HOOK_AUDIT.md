# 1.6.3 Windows 挂钩审计（2026-10-15）

客户端：`G:\Arknights Endfield`，GameAssembly PE 标识 `6abd2a51-1349d000`（时间戳/映像大小，不是哈希）。分析输入为定向元数据 `targeted-metadata.json`，没有生成全量 dump 或全量 IDA 数据库。

## 清单与扫描

源码清单由 137 条更新为 180 条：补齐投影屏模块 41 条、模型克隆观察的另外 3 个入口；动作模块移除 1 个已内联的冗余入口并更换 2 个失效入口。另补投影屏的 user32 输入盾及外部窗口输入桥/DirectInput 记录。

| 模块 | 托管挂钩数 |
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

最终扫描：`ok 131`、`partial 7`、`no-direct-callers 29`、`wrapper 12`、`unresolved 1`，没有可确认的 `never`。179 个描述符唯一匹配到非零 RVA。唯一缺失为 UI 可选的 `Beyond.CloudGameUtility.IsCloudGame()`；旧 2026-09-03 资料也不存在该类，不能视为本次回归。

1.6.3 大量方法将 IFix 展开为直接补丁表分派，旧 `IsPatched/GetPatch(id)` 扫描对这些入口无法识别。本次新增 `patch_table_scan.py`，使用“全局 root、table 偏移、函数/上下文槽偏移”作为指纹，识别出 104 个目标入口的插桩。候选还要共享原方法有特征的被调函数，才标记确认内联。若入口借入了更小方法的指纹，结果不能用于判定失效；已修正旧扫描器把这种情况误报为 `never/partial` 的行为。

## 动作模块修复

| 原入口 | 1.6.3 证据 | 处理 |
|---|---|---|
| `_TryExit` `0x5342950` | 无直接调用，内联进 `_TickMainFlow` `0x5342130`，共享被调函数 2/2 | 改挂 `_TryGetCurInterruptCommand` `0x53429A0`；只过滤当前持有冲刺的 `SpDashEnd + ForceExit` 命令，保留其他命令 |
| `_CheckTrackEnd` `0x5345820` | 无直接调用，内联进 `_TickMainFlow`，共享被调函数 3/3 | 改挂实际清理叶函数 `_DoExit` `0x68E9390`；沿用同一 handle、当前主流程、活动策略、保持条件、无显式命令、实体存活、End 阶段的全部门槛 |
| `_TickStatePerformInterrupt` `0x5172410` | 无直接调用，内联进 `PreLateTick` | 删除冗余入口钩；保留已安装的 `ShouldInterruptSpDash` 委托目标钩 |

新两个入口均有 3 个直接调用点。`_TryGetCurInterruptCommand` 另有一份内联于 `_RecoverEntityState`；恢复清理路径保留原行为，持续冲刺需要拦截的 `_TickMainFlow` 路径直接调用新入口。

委托覆盖不是推测：`_DoPlaySpDashPerform` 给 `_BeginPerform` 传入 condition 3；`CharacterAnimationComponent.cctor` 为字典 key 3 创建的委托取方法槽 `0x106A13D8`，其编码 `0x6001C795` 解码为 method index 58314，即 `ShouldInterruptSpDash`（token `0x0600E3CB`）。因此其零个直接 call 不代表挂钩不会触发。

局部 IDA 材料位于 `actions-local/`：只导入 5 个函数及其冷块，总计 **2,455 字节**代码；生成的稀疏 IDB 约 225 KiB。`pseudocode.c` 为辅助分析，未完整恢复类型，地址与调用分支应结合原指令理解。

## 其他内联发现

- `VoiceUtils.TryGetVoiceDuration(string, out float)` 的两份内联副本 `0xE1400` / `0x722DC20` 都调用 `_GetVoDurationFromVoData` `0x5C82280`，当前 voice 叶函数钩已覆盖。`CSharp_UnloadFilePackage` `0x6691D10` 前 64 字节仍包含原生目标缓存槽的 RIP 加载模式。
- `BattleRecorder.RecordCastSkill` 部分内联于 `Skill.DoCast`；模块已经优先挂 `DoCast`，只把 recorder 当作 fallback。
- `SolverManager.LateUpdate` 的指纹借自更小 helper；不能根据相同指纹及零直接调用判断 Unity LateUpdate 失效。
- `ForceStopCurrentStatePerform` 在 `SetMemoryShellAnimationSuppressed` 中有副本；原入口仍有调用。原动作帧检查会在 perform/owner 变化时取消会话，本次没有对该额外路径扩大拦截范围。
- 登录 `ChangeState` 在 `Start`、`UIMaterialAnimation.LateTick` 在另一调用方有副本；现有每帧逻辑以及 `CanvasUpdateRegistry.PerformUpdate` 渲染前修正仍在清单中。本次没有仅凭部分内联替换这些入口。
- `DeviceInfo.get_isConsoleUser` 存在部分内联；其余平台/输入接口仍能解析。此项与上述登录副本保留为运行时观察项，未宣称静态扫描能证明所有功能行为。

## 验证

- Windows Release 构建 `BetterEndfieldNext.Actions` 成功。
- `BetterEndfieldNext.ActionsTests` 现有生命周期策略测试通过，包含自然结束延期所有条件的反例。
- 更新清单后重新扫描一次，新入口均解析且有真实主流程调用证据。
- 未启动游戏做端到端行为测试；29 个无直接调用的目标包含委托、Unity 消息和反射入口，不能仅据静态结果判为失效。

构建产物：`build/game-update-1.6.3/native/stage/Release/modules/BetterEndfieldNext.Actions.dll`。完整结果见 `hook-inline-scan.json` / `hook-inline-scan.txt`。
