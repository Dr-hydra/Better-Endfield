# 梨诺持续冲刺的机甲显隐与光效挂点（1.13.1）

## 1.13.4：补齐漂浮物光效保持（2026-09-15）

实机反馈脚部光效已经保持，漂浮物两侧仍会熄灭。原来的保持表只有 4 个槽，仅启用 actor 0–2；虽然跟随刷新遍历 22 个槽，却不会延长其寿命。漂浮武器的 `weapons_base_L/R_a_01_jnt_start_02` 位于 actor 10/11，Perform 的 duration 为 -1，但 prefab 的 EffectLogic 寿命为 1.8 秒，内部两组粒子分别为 1.2/1.0 秒且未循环。证据来自 `tmp_analysis/liino-perform-decoded.json` 和 `tmp_analysis/liino-transition-effects/` 的 CAB-26523d41e4415b2ac8f8d785a34effc3、CAB-69ba94be4a6d646c6d77a3b8b9f5c452。

保持表与会话槽扩到 22，将 actor 10/11 及同挂点的 `jnt_lvdong`（20/21）纳入现有寿命冻结、粒子循环与退出恢复流程。效果仍按槽号和名称核对；入场闪光、退场粒子和材质效果未加入循环。生产路径回归覆盖四个新增槽的粒子准备、寿命保持、外部实例放行、腾空退出与参数恢复。实际持续观感仍需加载新版模块后在游戏内确认。

## 1.13.1 原始修复

2026-09-14 实机反馈：悬浮机甲消失后重新出现，光效与身体错位；洁尔佩塔没有同类问题。

原实现只拦截机甲网格隐藏并保持三个粒子效果的寿命。梨诺的 Perform 还会在 1.5667 秒触发 `EffectPlayActData`（logic 20、actor 4），播放材质特效 `P_fxbat_liino_sprint_dash_sp_04`。该特效的 rendererMask 为 67115008，持续 1 秒，溶解曲线约在归一化 0.62 时达到 1。网格仍可见时，材质先溶解消失、特效结束后恢复，解释了“消失又出现”。单独拦截 `SetCharMeshGroupShow` 无法阻止材质溶解。

1.13.1 的处理：

- 仅对主控梨诺当前拥有的持续冲刺，在有效保持期间跳过上述退场溶解，以及 1.6 秒的四个机甲/脚部退场粒子（actor 14–17，logic 31/42）。入场效果照常播放；停止、腾空、换人、其他 Perform 与其他角色照常交给游戏。
- 网格隐藏拦截要求同一个 Perform、同一个 Entity、游戏线程和 `MeshGroup1/2`，枚举值从运行时元数据读取。
- 最终骨骼姿态覆盖后，从当前 Perform 的 22 个效果槽重新锁定 `ObjectPtr`，核对梨诺冲刺效果名称并检查 Unity 对象存活，再调用游戏的 `EffectInstance.ManualUpdateFollow()`。这包含短时入场粒子和持续光效，沿用原生挂点、偏移与旋转；不推进寿命、重播或重新模拟粒子。每次调用期间持有临时托管引用，调用后释放。
- 新接口为可选契约，解析或挂钩失败会记录日志，不阻断洁尔佩塔的冲刺功能。动作模块版本升为 1.13.1。

本地证据位于主工作区 `tmp_analysis/liino-perform-decoded.json`、`liino-render-vfx/*.114.tree.json`、`liino-transition-effects/`、`liino-manual-follow-dis.txt`、`liino-effect-onplay-dis.txt`。反汇编确认 `ManualUpdateFollow` 以零 delta 调用跟随更新，`EffectPlayAction.OnPlay` 负责创建并保存效果实例。未改共享资源或游戏文件。

验证：重新编译 Actions DLL 及四个动作测试程序；新增生产钩子回归覆盖指定退场效果拦截、外部 Perform/角色/入场/腾空/关闭放行、Entity 和网格组范围，以及存活检查和仅刷新跟随、不停止粒子。骨骼数据保持此前的低位平滑飞行版本。离线测试不代替游戏画面验收；需要重启游戏确认长时间冲刺、左右两侧、转向和退出的效果。

全程无机甲、无光效可作为另一个外观选项实现，但本次修复未加入该选项。它需要在原生入场前接管显示策略，同时处理机甲/腿甲网格、普通粒子和材质 VFX，并在退出时恢复原生状态。仅关闭当前三个持续粒子仍会留下腿甲、入场效果及材质发光。
