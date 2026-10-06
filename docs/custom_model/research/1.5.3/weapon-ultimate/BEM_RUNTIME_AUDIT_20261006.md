# BEM 武器、大招及热切换实机故障审计

工作区 `G:\Better Endfield`，分支 `dev/bem-1.4-weapons-forms`。测试安装在 `E:\Better Endfield`，游戏在 `E:\Endfield Game`。

后续用户已确认武器与大招首次加载正常；PC 主世界“从未替换过的资源首次启用”另有漏扫路径，后续调查、修复和红绿回归见 [PC 首次启用热切换](BEM_PC_HOT_SWITCH_FIRST_ENABLE_20261006.md)。

## 判断

本轮问题属于运行时边界和验收缺口，不能归为一个启用配置问题。格式解析、管理器和离线转换已有可复核证据；运行时存在崩溃阻断及热切换漏绑，尚未达到可发布状态。编译通过、离线合同匹配、`published=true` 均不能代替实际渲染验收。

## 已证实问题

| 问题 | 证据 | 归属 |
| --- | --- | --- |
| 武器同名 Sprite 被当作 GameObject | 18:07:10、18:13:33 两次 `unityplayer.dll+0xCC30B4` 访问异常；游戏栈为 `UIImage::_LoadSprite → CustomModel → GameObject::GetComponentsInChildren` | 入口原先缺少对象类型检查，本轮武器扩展使同名图标进入模型处理 |
| 大招关闭再开启后，已有实例漏绑 | 停用恢复两个 Clone；再次启用时 Clone `staleHits=0`、`instancesQueued=0`，仅模板再次提交 | `v3.5.1` 标签已有相同扫描缺陷；legacy 包同样受影响 |
| 旧角色 donor 拒绝不可定位 | 新日志 43 次 `Resource preparation failed`；泛化 donor 日志未区分 Mesh/索引、材料、shader、骨骼根归属 | 尚未证实是本轮回归，须保留严格匹配并增加诊断 |

`v3.5.1` 对应 `3ef6ed35`。该标签到本轮开发前 `3510fa70` 的 `module.cpp`、`model_job_runtime.inc` 无差异。旧扫描明确跳过空 selection 的 Original 记录，只凭旧 generated Mesh 发现实例；默认 clone hooks 关闭，未登记的自然 Clone 恢复后无法再次发现。

## 排除与边界

- 实机加载模块来自 E 安装，安装模块与本轮构建逐字节一致；三个新增包与转换样本逐字节一致。管理器读取 28 包无 Notice，当前启用包没有资源冲突。
- 对三个新增包重新核对 49 个 component：renderer 类型、完整路径、Mesh 名、原索引数、骨序及材质槽均与保存原生图谱一致，无须先重转包。
- 大招两个目标均有实际提交；两个自然 Clone 曾各命中 10 个生成 Mesh。因此并非完全没有解析、匹配或克隆传播。但该记录不检查实际可见性，也不证明当前屏幕绘制的是这些 LOD0 renderer。
- Windows LOD 强制代码与开发前相同。启用模型时，即使 `standalone_lod=false`，全局强制仍会申请启用；不能将该配置误判为 LOD 被关闭。Android 保留原有最高可用 LOD/通常 LOD1 的设计。
- 当前大招包不包含 `abilityentity_chr_0030_zhuangfy_ult_mirror_postmodel`；mirror 的网格/骨骼合同不同，不能借普通 ult 数据直接覆盖。
- `asset_path` 目前是制作和离线来源信息，运行时没有核验 AssetProxy 的实际加载路径；同名 GameObject 来源问题仍属于未完成的设计边界。

## 修复范围

- 真实 GameObject 类型门禁覆盖 Finish、同步处理、捕获、完成判定、恢复及异步注册/入队；同名 Sprite/Texture2D 原样透传，类型契约缺失时拒绝处理。
- 重新启用时，将已验证、仍存活的 Original 根记录重新排入同步队列。保留精确根名、实例身份及生产 receiver/donor 校验，不扩大为仅按名字猜测全场原网格。
- 同一资源 adapter 的同步队列未清空前阻止创建异步 Job；同步成功后同步更新已登记 target 的选择状态，覆盖 world/UI 多帧及快速再次切换。
- donor 拒绝诊断输出期望索引、实际候选路径/Mesh/索引和具体拒绝原因，最多六个候选。真实匹配与诊断共用材料/shader/骨骼检查，未放松合同。

## 测试盲点

旧匹配单测只执行 header 算法，兼容回调恒 true；旧运行时 mock 不区分 GameObject/Sprite。自然 Clone 测试复制理想 Mesh/材料/根内骨骼，并主动登记 Clone；没有覆盖默认关闭 clone hooks 时的停用→再启用扫描。测试数量不能证明上述生产边界已覆盖。

验证结果：

| 范围 | 结果 |
| --- | --- |
| 同名 Sprite/Texture2D 类型边界及合法 static/skinned prefab/Clone | 新 `--resource-types` 通过；执行生产 Finish/捕获/提交，非 GameObject 不调用 Unity 成员、不注册 GC root、不入队 |
| 未登记自然 Clone 的 A→停用→B | 新 `--scene-rebind` 修后通过；legacy world/UI 五个有效根、explicit static 两个有效根，拒绝改名/错根/死亡对象，覆盖队列中的再次 revision 和句柄释放 |
| 同一 scene 回归使用 HEAD 原扫描函数 | 隔离源码构建后失败，明确报 `enable after disable left the verified natural clone on Original Mesh`；不是仅修改测试预期模拟故障 |
| 重复 Finish 及材料/骨骼漂移 | 不重复构建；缺 Original 和根外骨骼保持拒绝并给出具体原因 |
| 原有 Windows binding、static、async | 全部通过；本机测试构建树也独立跑五组入口全部 exit 0 |
| Android 路由与 LOD 状态机 | 213 项 world 路由检查、68 项 LOD 检查通过，保留 legacy/explicit LOD1 |
| Windows DLL / Android 共享原生代码 | Release 构建成功；Android 仍有平台既有 unused/deprecated 警告，未宣称零警告 |

日志位于 `build/bem14/local-test/audit/`，含 `resource-types.log`、`scene-rebind.log`、`legacy-binding.log`、`async-binding.log`、`windows-runtime-build.log`、`android-runtime-build.log`。原扫描对照代码及构建保存在该目录下 `scene-rebind-before/`。

本轮只验证生产协调器与模拟 Unity 对象边界，没有游戏渲染设备测试。首次开大、实际可见 LOD、mirror、连续回池和旧包 donor 拒绝的实机验收仍未完成；不能宣称全部视觉问题已修好。

## 日志

本次新增运行日志保存为 `build/bem14/local-test/audit/runtime-new.log`：大招首次提交在 2740、2774 行，Clone 的生成 Mesh 传播在 3018–3019 行，关闭恢复在 3065–3070 行，重新启用漏绑在 3115 行。

崩溃栈保存于 `C:\Users\28377\AppData\Local\Temp\Hypergryph\Endfield\Crashes\Crash_2026-10-06_101331969\Player.log`；另一崩溃目录末段为 `Crash_2026-10-06_100709599`。WER 和事件日志也记录两次相同异常。

18:47 已将修复后的 Windows CustomModel DLL 更新到 `E:\Better Endfield`，同步刷新本机 publish 副本及安装规范/审计文档。原文件备份为 `build/bem14/local-test/deploy-backup/20261006-184705-runtime-audit/`，部署回执为 `build/bem14/local-test/audit/deployment-receipt.json`。逐字节复核复制结果，`runtime.ini` 部署前后逐字节一致；没有重启游戏。

本轮没有改变用户当前模型启用状态，没有计算产物哈希，正式 Release 未覆盖。
