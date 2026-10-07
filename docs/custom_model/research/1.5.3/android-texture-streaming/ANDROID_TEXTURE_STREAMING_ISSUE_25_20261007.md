# Android 模型启用后场景纹理模糊：issue #25 调查

日期：2026-10-07。问题：[issue #25](https://github.com/Dr-hydra/Better-Endfield/issues/25)。本轮开发分支：`fix/android-issues-25-26`。

## 最新结论与用户约束

- 2026-10-08 修复候选已实现并构建：根据已证实的分配裁减，加入独立的 Android 纹理池协调器，维持全局 LOD/NPC 和物理 LOD1 绑定；跟踪游戏预算请求与切档，在实际需求超出原池且系统内存允许时增加额度。策略与接口 Release 测试通过，APK 签名和 native 打包核对通过，已用 ADB 安装；用户已确认本轮场景贴图恢复正常。此前“未实现”的段落保留为调查阶段历史，最新行为见文末。
- 2026-10-08 完整启用采样：当前会话已读取具体场景纹理的 `loaded > desired`，且流式加载队列已清空。已观测 35 个 art-tag offset 全为 0，`masterTextureLimit=0`。这排除了这些纹理仅因目标 mip 已被设低而模糊的解释；尚未证明锁 LOD/NPC 如何增加需求或影响分配，也不是已确认画面修复。采样后已恢复原完整功能测试 APK 和临时请求文件，保存设置逐字节一致。
- 2026-10-08：按用户纠正，停止预算补偿实现。预算状态机、setter 和额度调整均未接入生产，也未写入手机；修复继续沿已经实机复现的全局 LOD/NPC → 原生资源选择链路定位。不能把此前的预算统计当作唯一根因或已经修复的证据。
- 直接解码 Android 场景后，172 个按组件类型、完整尺寸及资源 mapping 校验的对象，最高资源槽均从 `LODInfo[0]` 开始，最大值为 `FloatMax`，首槽范围非空。用原生 float32 选择公式重现 8 组输入，全部可选择最高候选槽。因此这些数据不支持“Android 首槽上界过小，`1e-7` 导致无法请求最高资源”的候选原因；没有据此改写 bias、LOD 索引或区间。
- 离线直查确认：Android 石头和角色的数组首档确实引用物理 LOD1；中高档 `LODStreamingOffset.All=0`；APK 三档 Unity `maximumLODLevel=0`、自定义 `lodOffset=0`。因此不能把 Android LOD1、默认 streaming offset=1 或默认 maxLOD=1 当作已找到的错误。**根因仍未唯一确定**。
- 用户最终澄清：**关闭全部模型后清晰**；此前“全部关闭后仍模糊”的表述已纠正。关闭模型同时退出全局 LOD/NPC 策略，因此该比较不能单独归因于模型纹理。
- 唯一已执行的隔离包保持完整全局 LOD/NPC 策略，不提交任何 BEM。新会话 `tid=22544` 明确 `diagnostic mode=lod-only`、`mods=0 standaloneLOD=1`；泵线程 `tid=22335` 两次记录 `pipeline LOD bias + NPC parameters applied/readback PASS`，本会话无模型事务。用户同位置反馈：**仍模糊**。
- 因而自定义模型贴图不是触发问题的必要条件。全局策略及它与原生 LOD/纹理资源管理的关系成为优先调查对象；原游戏资源增加后预算受压仍可能是中间环节，不能把“无 BEM”写成“排除预算”。该会话最后样本仍有 `pendingLoads=808 loading=2`，不冒充稳态纹理预算。
- 用户随后要求两项设计均保留、停止后续 A/B。`pipeline-only` 包仅完成构建，**没有安装或执行**。手机已用 `adb install -r` 装回 `BetterEndfield-3.5.3-issues25-26-test-Android-arm64.apk`；新增 A/B 源码入口已撤回。后续查实际消费者和全开模式状态，不继续要求关闭策略。
- 用户补充同策略在 PC 能使场景保持高精度。调查必须比较两端资源 LOD/注册策略；Android BEM LOD1 是设计，不改为 PC LOD0。

真机采集保存在 `build/android-issues-25-26/device/lod-only-first-capture` 和 `lod-only-blurry`。原始日志跨会话追加，以上线程/会话边界用于排除旧模型记录。临时包构建/安装记录分别为 `LOD-ONLY-BUILD.json`、`LOD-ONLY-INSTALL-78572d34.json`；安装前后手机启用配置与 durable index 逐字节相同，没有计算产物哈希。

## 反馈与范围

设备为联想 Y700 四代、骁龙 8 Elite（SM8750P）、ZUXOS 1.5.10.259。启用自定义模型后，石壁/台阶等场景细节持续模糊，贴近也不恢复；关闭全部模型并冷启动恢复。用户追加确认：**只启用一个模型就出现**，不需要多个模型或多次热切换。

已查看 issue 两张原始截图。启用模型的图中，近处石壁纹理明显平滑，禁用图同处可见细颗粒纹理；阶梯轮廓大体相近。该现象支持继续检查纹理 mip 驻留，但截图不能证明实际 Mesh LOD、加载的 mip 数或资源预算值。

用户明确确认：全局最高可用 LOD 锁定和扩大 NPC 可见范围是现有设计。本轮保留 `lod_pipeline=1`、`lod_npc=1`、全局 parent/art-tag bias 和五项 NPC 参数，不收窄 art tags、不关闭锁定、不减少可见范围。Android 场景接收器继续是 LOD1；管线入口名 `EnableForceLOD0` 不改变 BEM 的 Android LOD1 适配。

## 已确认的代码事实

- 模型运行时构造 `Texture2D`，按包声明上传全部纹理数据；同步和异步路径均调用 `Apply(false, true)`，保留已提供 mip 链并释放 Unity 可读 CPU 副本。因此不能把这个问题直接说成未释放 CPU 纹理副本。
- deferred payload 每个纹理上传后即释放。共享 `TexturePayloadStreamer` 的 CPU 解码预算与 Unity 游戏的 GPU/纹理流式加载预算是两件事。
- 同一事务按纹理索引和原始 sampler 去重；后续同角色模板/实例可复用仍绑定在已发布材质上的纹理。异步运行时每角色只允许一个 builder，未发现同角色并发 builder 重复创建的证据。
- 未发布事务在失败/取消时逆序 `Object.Destroy` 自建 Mesh/Material/Texture；发布后不保留自定义纹理的长期强 GC root。identity 表只保存字符串、上限 4096；retired lineage 保留弱引用而不负责强保活。
- 未发现模型模块写入全局 `mipMapBias`、Unity streaming 开关或 memory budget。sampler 的 mip bias 只复制到新构造的替换纹理，不修改原游戏贴图。

上述事实排除了一些明显实现错误，**不等于证明 native/GPU 驻留没有积累**。成功发布的运行时对象何时被 Unity 释放、游戏自身的 bundle/native 资源引用是否仍保留，需要实际日志和内存采样。单模型冷启动即可复现，也降低了“多次热切换积累”作为首要原因的优先级。

## 纹理预算假设及证据边界

Unity 官方说明：`streamingMipmapsMemoryBudget` 同时计算 streaming 与 non-streaming 纹理；non-streaming 纹理优先保留最大 mip，即使已经超预算，streaming 纹理会尽量降低 mip 来满足预算。[Unity 2022.3 API](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/QualitySettings-streamingMipmapsMemoryBudget.html)

`Texture.nonStreamingTextureMemory` 提供非流式纹理内存计数，可与 `currentTextureMemory`、`desiredTextureMemory`、`targetTextureMemory` 和 pending/loading 指标联合判断。[Unity 纹理流式 API](https://docs.unity3d.com/2022.3/Documentation/Manual/TextureStreaming-API.html)

运行时 BEM 纹理可能作为 non-streaming 占用，使场景 streaming 贴图长期降 mip；单个大包也有可能触发。初次离线调查时未连接 Android 设备，因此当时属于**高概率、尚未经本游戏实机验证的机制假设**。随后两轮实机数据见后文：预算压力已取得直接证据，但仍不能宣称已定位每一张地图贴图的 mip 或修好画面。游戏若使用额外的 HG 纹理流式系统，也可能需要进一步核对。

不根据假设直接增大全局预算、强制场景请求 mip0 或清除 native 资源；这些动作可能改变游戏画质管理或造成驻留增长。

## 本轮新增的只读诊断

源码：`native/modules/custom_model/android_texture_streaming_diagnostics.inc`，由双端共享 `module.cpp` 的 Android 分支调用。所有诊断契约为可选 getter，缺失仅记录一次降级，不影响模型交付。

- 已确认 Unity pump 线程上采样；首次 BEM 上传前、前 8 次包含新纹理的事务完成后，以及最多每 15 秒输出摘要。
- 输出 streaming active、budget MiB、non-streaming/current/desired/target 字节、pending/loading、streaming/non-streaming 数量。
- 上传后附该事务的 Texture 数、提交 payload 字节、去重节省和 live reuse 字节；明确标注这些是 BEM 输入统计，**不是实测 GPU 驻留**。失败事务记录 `transactionPublished=0`，不能把其销毁前瞬时占用解释成长期泄漏。
- 非 pump 的同步交付只排队，不执行新 Unity 查询。首次上传发生在 pump 确认前时，后续日志明确写 `deferred-upload-observation (before-first-upload unavailable)`，不会伪装成真实上传前基线。
- 只有显式 `inspect=1` 才每分钟调用一次 `FindObjectsOfTypeAll<Texture2D>`，后续最多检查 1024 个对象、记录 6 个 `loadedMipmapLevel > desiredMipmapLevel` 样本。`FindObjectsOfTypeAll` 本身仍会枚举完整引擎库存；1024 是后续属性读上限，不能称为完整枚举成本上限。默认不开启该扫描。
- mip 样本标记已知 BEM 与 `game-or-untracked`，说明采样数/库存数；不把子集统计或全局 non-streaming 增量全部归属某个 Mod。

默认摘要不修改 LOD、NPC、游戏预算、requested mip、原始材质或对象生命周期。

## 早期实机采集方案（已停止 A/B）

以下为早期采集方案，用户明确要求停止关闭 LOD/视距的 A/B 后，不再执行这组对照。保留命令仅供复核旧采集；后续只读现有正常启用状态。原始 logcat、`dumpsys meminfo` 及过滤日志可通过只读收集脚本保存：

```powershell
./android/tools/CollectTextureStreamingDiagnostics.ps1 -Serial <设备串号> -Stage baseline
./android/tools/CollectTextureStreamingDiagnostics.ps1 -Serial <设备串号> -Stage models-enabled
```

脚本默认读取公开 ADB 日志/内存信息；`-IncludePrivateRuntimeLog` 使用已有 root 权限只读游戏私有诊断日志。两者均不修改游戏状态。

优先对照首次模型上传前后的 budget、non-streaming 增量、Mod 实际提交字节、target/desired 差额。如果 Unity streaming active、non-streaming 达到预算且场景 mip 样本持续落后 desired，则进一步验证预算压力。若没有预算压力，转查 HG 纹理流式偏移/请求状态；若场景 mip 已是最高级，转查材质或几何 LOD，不能继续凭截图认定低 mip。

只有拿到上述证据后，才决定具体修复：减少真实重复驻留、提供独立贴图档位、为 BEM 建立受控纹理流式方案，或按证实的新增占用进行有恢复机制的预算补偿。继续保留用户确认的 LOD 与可见范围设计。

## 离线验证

新 `BetterEndfield.AndroidTextureStreamingDiagnosticsTests` 直接编译生产诊断 include，覆盖只允许确认 pump、无 setter、预算压力与缺失契约、正常/延后共享限频，以及 opt-in mip 样本读取/日志边界。

现有 `BetterEndfield.AndroidModelLodTests` 的 68 项维护检查仍通过，证明本轮没有改变 legacy/explicit LOD1、static/skinned、停用/停止与 pipeline 更换策略。`BetterEndfield.CustomModelBindingTests` 使用 `tools/CustomModel/test_bem_v1.py` 的真实 synthetic builder 输出运行，解析、材质/回滚/原资源所有权及 scene rebind 检查通过。

```powershell
cmake --build build/release-3.5.3/build/windows/win-x64/Release/native --config Release --target BetterEndfield.AndroidTextureStreamingDiagnosticsTests BetterEndfield.AndroidModelLodTests BetterEndfield.CustomModelBindingTests
./build/release-3.5.3/build/windows/win-x64/Release/native/Release/BetterEndfield.AndroidTextureStreamingDiagnosticsTests.exe
./build/release-3.5.3/build/windows/win-x64/Release/native/Release/BetterEndfield.AndroidModelLodTests.exe
./build/release-3.5.3/build/windows/win-x64/Release/native/Release/BetterEndfield.CustomModelBindingTests.exe build/tests/issue25/synthetic-v1.bem
```

构建与运行日志：`build/tests/issue25/native-build.log`、`build/tests/issue25/custom-model-binding.log`。APK 编译与设备画面验证由主任务统一记录；本记录不把离线测试写成游戏实机验收。

## 同日实机补充：两轮采样

用户安装测试 APK 后，确认队伍中两个 Mod 的场景仍模糊。第一轮日志为：

`build/android-issues-25-26/device/20261007-115914-two-mods/game-native-diagnostics.log`

| 时点/指标 | 实测 |
| --- | --- |
| 梨诺模型事务 | 25 张新纹理，提交 payload 252,706,816 B（241.00 MiB） |
| 佩丽卡模型事务 | 13 张新纹理，提交 payload 45,733,232 B（43.61 MiB） |
| 大世界 streaming | active=1，budget=600 MiB |
| 稳定 periodic non-streaming | 1,067,509,570 B（1018.06 MiB） |
| current / target | 两者均 1,253,504,706 B（1195.44 MiB） |
| desired | 1,791,996,162 B（1708.98 MiB） |
| pending / loading | 两者均为 0 |

这组稳定值确认 non-streaming 已超过纹理预算、实际占用收敛到低于 desired 的 target，且采样时没有正在排队的流式加载。它支持预算约束导致画面长期不再提升的解释，不能解释为一直还在下载高清图。仍没有具体石壁贴图的 `loadedMipmapLevel` 样本，也没有可比较的真正零 Mod 同地点基线，不能把所有 non-streaming 占用归给这两个包。

上传后瞬时 `currentBytes` 小于 `nonStreamingBytes`，随后稳定 periodic 恢复合理关系。这些统计更新时点可能不同，不据一帧的先后快照差异认定日志损坏或 native 内存错误。

梨诺实际运行时纹理为 BC7，`SupportsTextureFormat` 及 graphicsFormat 读回通过；佩丽卡为 ASTC 4×4 / 6×6。不能把“Android 一定已转 ASTC”作为该测试的前提，也不能根据 BC7 名称直接断言驱动没有原生支持。提交 payload 大小不等于驱动真实驻留。

用户随后反馈关闭全部 Mod 并冷启动仍模糊。第二轮采集：

`build/android-issues-25-26/device/20261007-120628-models-disabled/game-native-diagnostics.log`

但这份日志新会话 `tid=440` 明确仍提交安洁莉娜、佩丽卡、梨诺三套模型，runtime 初始化记录为 `mods=4`，管线/NPC 策略仍 active；主代理读取的 durable manager 状态仍为 4 项 enabled。文件名称和用户意图不能代替运行时证据，**该轮不能作为零 Mod 对照**。操作入口、设置是否实际发布和旧 index/配置缓存由主任务继续核对，不能据此责怪用户或认定关闭操作本身已成功。本文件还保留前一会话 `tid=27310` 的旧记录，分析必须区分会话。

第二轮新会话另有明确预算变化：首次确认 pump 为 **9043 MiB**，随后登录阶段为 **850 MiB**，进入大世界后为 **600 MiB**。因此任何预算补偿都必须跟随游戏画质/profile 生命周期，不能保存启动时 9043 就持续锁死，也不能把自己已经写入的补偿值再次当作 base 累加。

两轮游戏都是用户手动退出。采样时游戏进程已结束，`game-meminfo.txt` 为 `No process found`，没有 live PSS/Graphics 数据；不能把退出后的系统可用内存（约 7.2 GiB）当作游戏运行中的内存余量，也没有据此认定 crash 或 OOM 的证据。

## 修复策略评估：尚未实现

本轮新增实机证据后继续只读研究，以下是下一步候选方案及必要边界，不是已经实现或设备验收的修复。

### BEM native 生命周期记账

建议每个实际成功提交的 BEM Texture 以 native instance ID、格式/尺寸/mip 和弱 wrapper 记账。事务内去重、live reuse 不重复加额度；未发布失败资产进入销毁跟踪；旧 generation 不能因为配置 disabled 或弱 wrapper 消失就立即退款，因为已生成 clone 可能仍使用 native 纹理。

当前共享 `WeakObject::Get()` 依赖 managed wrapper。wrapper 被 GC 不代表 native 资源已消失，因此预算 ledger 不能简单依赖弱引用数量。Unity 官方 2022.3 源中有元数据可解析的 `Object.DoesObjectWithInstanceIDExist(Int32)` 和 `FindObjectFromInstanceID(Int32)`，旧游戏 dump 也有具名方法。可在确认 pump 上检查已登记 ID 的 native 存活，必要时仅查找已存在对象并核对 Texture 类型/ID；**不调用 ForceLoad、不给纹理加长期强 root、不猜 native 指针偏移**。当前游戏接口的可用性仍须实际解析确认。[Unity 对象绑定源码](https://github.com/Unity-Technologies/UnityCsReference/blob/2022.3/Runtime/Export/Scripting/UnityEngineObject.bindings.cs)

`Profiler.GetRuntimeMemorySizeLong(Object)` 可提供逐对象 native 内存，但官方说明 Profiler 不可用时返回 0，且 native memory 不能一概称为单独 GPU 驻留。当前发行游戏是否支持该计数未知；可先探测，返回 0/缺失时回退经过验证的 graphicsFormat+mip 存储估算，并在日志明确标为 estimate。`GetNativeTexturePtr` 是资源句柄，不提供大小；不能拿指针推算显存。[Unity native-size API](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Profiling.Profiler.GetRuntimeMemorySizeLong.html)

### 恢复型预算补偿与地图空间

每帧同步全场景资源没有必要。可以在确认 pump 上以有限频率维护预算状态机，保存**最后一次游戏要求的 base**、本模块最后写入值、当前 BEM allowance、读回结果和内部写入标志。游戏 profile/质量改写（9043→850→600）时重基，而非再次叠加自己的旧 allowance；native 直接更新可能绕过 managed setter，因此只 hook setter 还不够，需要读回核对。

启用补偿、停用、streaming 关闭、quality/profile 更换、场景资源释放、failed upload 和 shutdown 都应有测试。仅当仍拥有当前写入值时恢复最后的游戏 base；若外部已写新值，尊重外部状态。所有新 Unity setter 均在确认 pump 上执行，写入失败不应破坏模型事务。

**只有 BEM ledger 补偿不能保证消糊。** 第一轮两包提交 payload 合计 284.61 MiB，600+284.61=884.61 MiB，仍小于实测 non-streaming 1018.06 MiB。直接扣除 payload 得出的 733.44 MiB 也不是可靠原版基线，因为快照时点、原始材质保留、驱动存储及保留的 LOD/NPC 设计都会影响占用。需要真正零 Mod、相同位置与画质/profile 的 streaming 空间对照，或独立明确标为试验的受控目标，不能把全局超额或 desired 缺口全部计成某个 Mod。

若有可信 streaming 空间基线，可在 BEM 定向 allowance 之外评估保留地图流式空间的 floor。两者均受绝对上限、系统运行期间可用内存余量与低内存信号约束；达到边界时允许退化并输出诊断，不能无条件锁定 2 GiB 或强制所有地图 mip0。上限只限制本模块额外额度：游戏原 base 已经高于 cap 时，不把游戏预算反向裁低。

Android 共享系统内存，`ActivityManager.MemoryInfo.availMem/threshold/lowMemory` 等可以辅助低内存边界，但不等于每个进程的 GPU 保证额度；必须在游戏仍运行时采样，也不能保证绝对避免系统 OOM。[Android 内存信息 API](https://developer.android.com/reference/android/app/ActivityManager.MemoryInfo)

### 停用与卸载边界

现有 `ShutdownResourceModule` 明确不回滚已发布模型绑定，只停止 hooks/作业并清理本模块跟踪。因此若关闭模块时立刻恢复 600 MiB、BEM native 纹理却仍显示或仍驻留，地图预算压力可能再次出现。这与“恢复游戏设置”是同一生命周期必须明确处理的两面，不能只写恢复成功就宣称画面已恢复。

全部 Mod 停用也必须区分：配置已停用、所有已知实例已回绑 Original、native 资产已经释放。预算缩减应跟实际资产存活，不按 enabled 列表清空 ledger；不为了退款强制销毁仍被 clone 使用的共享纹理。模块彻底卸载后若不保留控制器、也不回滚模型，则恢复 base 后需要游戏重启的边界应明确记录。

### 防止日志轮转丢失基线

现有 Android `core/log.cpp` 的通用诊断文件超过 1 MiB 后整文件重开写入，且不同游戏会话继续追加。首轮首次 pump/上传前基线已经不在当前文件中，第二轮文件又同时包含两会话数据。

建议额外的独立纹理统计 journal，只读取游戏指标、写本模块自己的诊断文件：

- 每条记录带 process/session ID、单调时钟、事件名、原始 budget/current/non-streaming/desired/target 和 BEM 事务计数，避免将旧会话归到本次操作。
- 固定保留当前会话的首个确认 pump、真正上传前或明确 deferred 状态、前几次上传后的快照，放入单独小型 baseline 文件，不跟周期日志一起轮转。
- 周期摘要用小型有上限的 JSONL 文件与一份轮转副本，最多保留少量会话；不复制完整 package payload、用户配置或 material audit。
- 默认沿用 15 秒限频；磁盘写失败只降级，不影响游戏。路径使用现有诊断目录，不在 `/data/local/tmp` 猜写权限，不修改游戏资源/画质/设置，也不需要为记录加 texture 强 root。
- `FindObjectsOfTypeAll` 仍只在显式 inspect 模式下启用；journal 只保存既有摘要，不能为了留日志添加全场景每帧扫描。

journal 目前只是设计建议。没有修改预算代码、设备设置或原日志文件；后续实施和 APK 验证由主任务决定。

## 离线资产与画质档位核验（2026-10-07）

本段仅报告离线读取结果，不改变全局 LOD、NPC、纹理预算或设备配置。证据保存于 `build/issue25/offline-quality-profile/`，主要文件为 `sources.json`、`serialized-lod-mesh-proof.json`、`quality-feature-summary.json`。未计算产物哈希。

### Android 第一档对应作者的 LOD1，属于资产设计

从 Android 本地 VFS transfer 与 2026-10-01 资源快照集合读取同一石头 prefab `p_rock_map01_small+1_003_05`，并对照当前本机 PC VFS 的 Persistent overlay。完整 `LODGroup` 序列化数据如下：

| 平台 | 序列化 group 档数 | group[0] 主 mesh | group[1] 主 mesh | group[2] 主 mesh |
| --- | --- | --- | --- | --- |
| PC | 3 | 作者 LOD0 | 作者 LOD1 | 作者 LOD2 |
| Android | 2 | 作者 LOD1 | 作者 LOD2 | 无 |

这里不是通过文件名猜测绑定。Android group[0] 的 renderer `-1734542523158296151` → GameObject → MeshFilter `1605358927406500265` → mesh PPtr（external file ID 1、path ID `8331277837568228029`）→ 被引用 serialized file 中的实际 Mesh 对象 `S_rock_map01_small+1_003_05_lod1`，引用文件名也已核对相同。PC group[0] 的引用链实际落到 LOD0。Android 还在每档包含一个 shadow proxy renderer，未将其误当作主几何 mesh。

佩丽卡 `chr_0004_pelica_postmodel.prefab` 同样由 PC 的 4 档变为 Android 的 3 档；Android group[0] 的 12 个 renderer 均通过实际 mesh PPtr 与 external file 核验，全部落到作者 LOD1 mesh，涵盖身体、衣物、头发、面部及眼睛。因此，**这些已验证 Android 资产的逻辑 group 0 就是保留下来的最高档 LOD1**。不能把逻辑索引 0 直接解释为请求被剔除的 PC LOD0，也不能为了保留 LOD1 再统一给它加 1。

石头 class 205 的完整 type tree 只有 GameObject、reference point、size、fade 配置、`m_LODs` 的屏幕比例/renderer 引用及 enabled 状态，没有独立 LOD streaming resource 区间或资源索引映射字段。上述证据证明几何 group 的压缩，**不证明 native resource group 的区间与映射也已经压缩**；是否存在二者不一致，仍需独立资源数据与消费者证据，不能据此认定根因。

### LOD streaming offset 的平台表与档位来源

配置真实路径为 `assets/beyond/initialassets/settings/{mobilesettings,desktopsettings,commonsettings}.ini`，封装在 `initial/5d0494d1db10d3ab6b83c8ef.ab`。quality map、tier components 与设备规则封装在 `initial/ddefee213a58c09ceb2f383d.ab`。优先读取 Android 本地 overlay 后，其对应 INI、Android quality map/tier components/SettingRules 与本机 PC 包内的同名文本逐字节相同。这是离线资源值，不是模糊发生时手机内存中的值。

| 配置 | 明确读取到的值 |
| --- | --- |
| Mobile `[LODStreaming@2000]` | `LODStreamingOffset.All = 0` |
| Mobile `[LODStreaming@1000]` | All = 1；Grass/Bush/Tree/Vine = 0；load dirty = 4，unload dirty = 12 |
| Desktop `[LODStreaming@1000]` | load dirty = 4，unload dirty = 20；未声明 offset |
| Android quality tier 1000 | `HGLODStreamingComponent.enableLODStreaming = false`；HGRP tier = 1000 |

三个 INI 未声明 keep-last-resource。当前 PC `LODStreamingSettingParameters` 构造函数明确以 true 创建 `lodStreamingKeepLastLODResource`，All 与 per-tag offset 以 0 创建；参数名称通过当前 metadata literal 引用解码确认。Android 对应构造函数代码页受保护，不能把 PC 默认值称为 Android 实测值。

设备规则中 Adreno 830 的 score 为 550；Android quality map 对应默认 quality tier 5500；其 tier component 明确指定 HGRP tier 5000。这是默认 profile 的静态链，用户画质选项、运行期 feature override 和 IFix 可能改变最终选择。

档位选择不是通过 600 MiB 预算倒推。当前 PC metadata 重新映射后的代码显示：

- `HGRPTierQuality.Apply`（RVA `0x39b9730`）调用 settings hub 的 `ChangeSettingTier`，最终写入 `currentDeviceTier`（+0x1c）并刷新参数。
- `EnvironmentRenderingFeatureQuality.Apply`（`0x39b9820`）只覆盖 `RainAndWetness` 与 `VerticalOcclusionMap` 两个 feature；两个 literal 引用已解码，未覆盖 `LODStreaming`。
- `SettingParameterBase.AcquireParamValueInSettingTable`（`0x344a670`）优先读取 feature override；不存在时读取全局 current tier。候选 tier 的选择谓词为 `candidate <= requested`。

Android 离线本体进一步确认同样的参数来源：`HGRenderPipelineSettings.ChangeSettingTier`（`0x15405770`）写 +0x1c 并进入刷新；`AcquireParamValueInSettingTable`（`0x1540e660`）未命中 override 后，在 `0x1540e8dc` 分支读取 +0x1c。Android HGRP/Environment Apply 的相关代码页仍部分受保护，因此 Apply 的 feature 名结论以当前 PC 本体为证，结合 Android 同配置作跨平台推断。所有 Android RVA 仅描述本地离线 ELF，不用于直接写入当前手机。

由这些表与已核验选择机制可推：**普通默认/中高档配置的 All offset 应选到 0，不能默认把 offset=1 视为这台设备的模糊根因**。All=1 出现在最低档，而该最低 quality profile 又明确关闭 LOD streaming；这进一步限制了此候选的适用范围。模糊当次的实际全局档位、feature override、streaming active 与 IFix 状态尚未完整掌握，故不能宣布当次实际 offset 已证明为 0，也不能把全部 offset 强制改 0 当作已验证修复。

## 原生资源消费者与只读采样（离线核验）

2026-09-21 保存的 1.5.3 Android `libunity-runtime-code.so` 是解密后的代码镜像；原始加密 `libunity.so` 不能用来直接判定指令。以下位置用于研究复现，**没有作为运行时硬编码 RVA 写入生产代码**。

- `HGCullingSystem` parent/art-tag setter 分别为 `0x54014c`、`0x540174`，写入 bias 平方和相应阈值表。`1e-7` 来自游戏原生 `EnableForceLOD0`，不是 Texture 的 `mipMapBias`；现有两个 setter 的 ARM64 参数 ABI 与实际入口匹配。
- 全局帧更新 `0x11ba7cc` 调用 LOD 资源管理 `0x12e0a58`。资源选择 job `0x1304da4` 与几何选择 helper `0x121a0f4` 不是同一算法；资源选择在 `0x1305cbc` 加 per-art-tag 整数偏移，再按数量、available mask 与映射选择资源。几何 helper 未按 tag 消费这张整数偏移表。
- 偏移来自 `HGLODStreamingSystem.SetArtTagLODStreamingOffset(UInt32, Int32)`，native `0x5b3fa4` 写 HG manager 的独立表。`HGRenderPipeline.RegisterArtTagLODStreamingOffset` 单独按画质参数注册，`EnableForceLOD0` 没有改这张表。这说明需要核对渲染与资源选择协同，但尚未证明当前设备有效偏移非零。
- `0x1322d6c`／`0x1323308` 经 `0x6c3f2c`／`0x6c3ed8` 连到标准纹理 manager 的注册／移除 `0x803544`／`0x80407c`，有具体的 LOD 资源到纹理集合的生命周期连接。后续逐指令核验明确是每个 RendererInfo 的 provider，激活值来自 geometry 表记录 `+0`，不是已证明的资源槽引用计数；零／非零变化连接注销／注册。不能从 sentinel `8` 或索引名字推断当前石墙 mip，暂未证明它是错误卸载根因。
- `LODGroup.CalculateLOD` 的标准 native 路径 `0x516768→0x7212b8→0x7214c8→0x722668` 从 `maximumLODLevel` 索引开始遍历。因此该参数约束数组索引，不等同于名为 LOD1 的 mesh。它是否参与当前 HG ECS 场景几何，以及本次实际值，仍需核实。
- APK 的 `globalgamemanagers` class 47 已按 Unity `2021.3.34f5` fork 实际序列化顺序完整解析：540/540 字节；Performant／Balanced／High Fidelity 的 `maximumLODLevel` 和 `lodOffset` 全为 0，默认 `m_CurrentQuality=2`。High Fidelity 的启动 streaming budget 为 9043，与游戏启动日志一致；场景内 600 是之后游戏 profile 应用的值。解析器、primary release schema 和字段顺序依据保存在 `build/issue25/read_android_global_quality_defaults.py`、`android_global_quality_base_schema_2021_3_34f1.json`、`android_global_quality_defaults.json`。两个 fork 字段名称尚未解密，但类型、顺序与对齐由 native Transfer 调用验证，未扫描原始字节猜数值。离线默认值仍不能替代运行中的最终质量 Apply／feature override。
- `QueryLODStreamingStatus` 只返回全局 Loading／Unloading／Disabled 标志，native 寄存器宽度不能冒充 boxed enum 宽度；旧元数据底层为 Byte。Idle 不能证明高精度纹理驻留。

新增采样仍只读取状态：最大 LOD、全局 mip 限制（同时探测 Unity 2021 fork 的 `masterTextureLimit` 名称）、最大 mip 降级数、HG LOD streaming 开关和状态。另一个可选观察器通过实际具名 native icall 捕捉 streaming offset，逐次把原始参数原样转发一次后才记账，不改偏移或资源策略；托管包装器可能被游戏注册路径绕过，因此不靠它观察。未捕获的 tag 明确为 unknown，不记作 0；报告也不声称 35 项是同一瞬时快照。合同或 hook 不可用时保持正常模型和 LOD 行为。

观察器通过 124 项 Release 下实际执行的检查，覆盖参数转发、负值与边界、未知状态、重置及并发。纹理诊断和原有 68 项 Android LOD 检查通过；Android Release/lint、Windows shared CustomModel 编译通过。完整场景纹理枚举仍由 `inspect` 显式开启，默认不扫描，没有为采样增加强引用、预算 setter 或 mip 请求。此只读观察包目前**没有安装到手机**，手机保留已恢复的正常测试包；不是已经修复或通过验收的产物。

## 场景真实资源区间与消费者复现（2026-10-08）

离线证据位于 `build/issue25/factory-assets/README.md`、`static-lod-pairs.json` 和 `resource-choice-reproduction.json`。场景 Init/Streaming 数据的压缩格式为 LZ4Inv：literal/match token 位重排、match offset 大端；从原生 loader `0x13f0d6c → 0x1395f0c → 0x1000a64` 推导出的解码器与既有后台独立一致，12 个压缩样本均精确满足文件前缀声明的长度，且 FlatBuffer 根有效。

读取实体组时验证完整 component descriptor、SoA 字节总数、RenderObject KN 容量与 stride；资源组件的数量和 mapping 同对应 renderer 数量核对。静态样本有 14 个有效对象，三份 DynamicStreaming 初始场景有 158 个。全部首 `LODInfo.maxSquared` 为 `0x7f7fffff`，首资源 mapping 的结束索引为 1、2 或 4，均非空。

资源消费者首槽的 begin 固定为 0，mapping[0] 是 end；后续槽才使用前一个 mapping 作为 begin。首 mapping 大于 0 不会将 upper 换成后续区间。真实 float32 `1e-7` 平方后，静态首例的首范围约为 `(6.4e-15, 3.4e24]`；172 个对象在 8 组正常有限投影输入下均选择候选资源槽 0。这只能验证候选计算，不能把离线默认 `effective=8/availableMask=0` 写成运行时加载失败。

全局 LOD setter 不直接写纹理 mip。Mip 需求经相机、renderer bounds、UV 密度、transform scale 和投影参数计算；有效 RendererInfo 的 geometry 激活值会连接到纹理 provider 注册/移除。本段离线验证不能取得运行时 offset、available mask 或具体纹理 mip；随后完整启用采样补充了 offset 和部分场景纹理 mip，见下文。当前仍未确认消除模糊的生产修复。

## 完整启用的独立 mip 采样（2026-10-08）

用户同意保持全部现有设置正常进入模糊地点读取资源状态。没有再次关闭 LOD、NPC 或 BEM，也没有调整预算、请求纹理 mip 或操作原游戏材质。

采样包为 `BetterEndfield-3.5.3-issue25-normal-mip-observation-Android-arm64.apk`。独立缓存请求采用 `BE_TEXTURE_MIPS_V1` 和一次性 token；仅在确认 pump 的周期采样上，两次间隔至少 15 秒读到 pending/loading 都为 0 后才执行。失败枚举不消费 token；枚举与旧 inspect 共用 60 秒限频。后续最多检查 8192 项、输出 12 条，优先场景名称并排除已知生成 BEM ID；`FindObjectsOfTypeAll` 本身仍枚举引擎库存。未提供请求时默认不启用完整纹理枚举。

Android Release/lint、原签名和打包 native 库核对通过；采样 fixture 在 `/O2 /DNDEBUG /W4 /WX` 下通过。相关检查覆盖明确执行的条件，未依赖 Release 下会被删除的 `assert`。

原始文件保存在 `build/android-issues-25-26/device/20261007-163357-normal-full-feature-mips/runtime-full-feature/`。token 为 `normal-20261007-163357`。当前进程 PID 15952，pump tid 14724；日志还包含更早会话，不能将整个文件当成本次运行。

采样临近的稳定快照：

| 指标 | 实际值 |
| --- | --- |
| streaming budget | 600 MiB |
| nonStreamingBytes | 360,947,858 B（约 344.23 MiB） |
| currentBytes / targetBytes | 629,134,994 B（约 599.99 MiB） |
| desiredBytes | 1,088,751,570 B（约 1038.31 MiB） |
| pendingLoads / loading | 0 / 0 |
| qualityMaximumLOD / qualityLODOffset | 0 / 0 |
| masterTextureLimit / maxMipReduction | 0 / 2 |
| HGLODStreamingActive / status bits | 1 / 0 |
| 已观测 art-tag offset | 35/35 均为 0，累计 140 次写入；不是同时读取的整表 |

7250 个纹理对象被扫描，其中 4078 个报告 streaming；2699 个 `loaded > desired`、1375 个相等、4 个已加载精度高于 desired。与上一周期纹理计数的小差异不能冒充丢资源证据。

实际样本包括：

| 游戏纹理 | 原尺寸 | loaded / desired / requested |
| --- | --- | --- |
| `T_mod_map01_building+1_019_14_rgb_M` | 512×512 | 1 / 0 / -1 |
| `T_prop_map01_pipe+1_002_01_D` | 256×256 | 1 / 0 / -1 |
| `T_prop_map01_pipe+1_002_01_NRO` | 256×256 | 1 / 0 / -1 |
| `T_mod_map02_sfroof+1_001_02_NRO` | 1024×1024 | 2 / 1 / -1 |
| `T_mod_map02_sfroof+1_001_02_D` | 1056×1056 | 2 / 1 / -1 |

样本的高精度 desired 请求已经存在，但 loaded 仍比 desired 低一级，当前也不是持续排队等待高清图。`requested=-1` 是实际属性返回值，不能当作额外强制高精度请求。相邻总量已收敛到 600 MiB 附近而 desired 更高，支持分配阶段削减 mip 的解释；仍需沿原生分配器和 HG 资源注册核实触发链，不能只据总量宣布唯一根因。

样本适用性有两项限制：库存对象不能证明就是屏幕前那面石墙；名称匹配的 `rock` 也误命中了 `rocket`，12 条中前 7 条是工厂火箭纹理且 loaded=desired=2，因此不能将 12 条都称为墙面样本。以上 5 条场景名称样本不受这个误匹配影响，但也不代表屏幕可见性。

早前 LOD/NPC-only 的最后留存快照仍是 pending=808/loading=2，没有稳定的逐纹理 mip；不能拿它与本轮稳定数据做内存因果差额，也不能从提交 BEM payload 字节直接换算 GPU 驻留。

采样后临时请求已移除，原 `BetterEndfield-3.5.3-issues25-26-test-Android-arm64.apk` 已通过 ADB 恢复。`capture-journal.json` 记录安装、采样和恢复；模型索引与模块设置逐字节一致。没有计算产物哈希。此包仍是测试包，未宣称 issue #25 已修复，也未推送或发布。

### 原生分配器对这轮驻留差距的解释

`0x80038c` 的统计从 non-streaming 占用起算，再累加各 mip 的 desired/target/current。因此 600 MiB 是总池，不能把 non-streaming 再加到 target 上作为“实测总占用”。desired 超过 job 总池时，`0x800110` 逐步增大 target mip 编号，从 target 总量减去旧 mip 字节再加较低精度 mip 字节，直到满足总池条件；desired mip 原值保留。这是明确的预算后裁减路径，不依赖对 API 名字的猜测。

desired getter `0x802b48` 读取 compact 状态 `+1`（`0x809dd4` 由计算输出 `+5` 复制）；loaded getter `0x802bc4` 读取 64 字节记录 `+9`。目标载入写入记录 `+8`，任务完成路径 `0x80961c` 才将其低 5 位复制到 `+9`。pending getter 经 `0x809634` 比较 target 与 loaded。因此仅有 loaded>desired 尚不足以唯一判断原因，但本轮同时具备 desired 高于总池、target/current 贴近总池、pending/loading 为 0 和具体纹理驻留落后，支持已完成的分配裁减状态。

上游静态检查发现，全局 tiny bias 在资源 planner 的主透视分支放宽最高资源槽的距离门槛，该逐实体选择分支不读取视锥平面；它可维持更多**已加载实体**的最高资源，不能称为加载整个地图。有效 geometry/material 让每个 RendererInfo 的 provider 保持登记，但 mip 需求仍独立计算，texture manager 也有自己的参与筛选。没有找到 force 绕过注销、重复登记或强制远处 mip0 的确定错误。资源卸载清 handles 后，`0x12e0fdc → 0x1323308 → 0x6c3ed8 → 0x80407c` 的正常注销路径存在。

以上支持“LOD 策略扩大原生资源工作集，随后分配器降低 mip”这条可行机制，但单次库存尚不能量化 LOD、NPC、BEM 各自的增量，更没有可见石墙的 provider 身份。因此本轮没有将普通高精度需求增长包装成已定位的注册 bug，也没有修改预算、参与筛选、LOD 或 NPC 策略。完整只读证据在 `build/issue25/mip-allocation-native-proof.md`、`force-lod-texture-registration-review.md` 和 `runtime-mip-evidence-summary.json`。

## 修复候选：让纹理池跟随既定 LOD 策略的实际需求

用户要求继续到可执行解决方案。此前停止预算实现是因为只有总量猜测；现在逐纹理 loaded/desired 与原生分配器已闭合裁减环节。本修复直接处理这一已证实环节，不声称已经找到了可见石墙的所有上游贡献，也没有修改原生纹理注册或移除逻辑。

生产代码为 `native/modules/custom_model/android_texture_budget_policy.h` 和 `android_texture_budget_runtime.inc`。协调器只在确认的 Unity pump 上读取和修改纹理总池。全局最高可用 LOD、35 个 art tags、NPC 距离和物理 LOD1 模型接收器保持既定策略。

- 首次增额要求两次间隔至少 2 秒的有效观察：desired 比 target 高出超过 64 MiB，target 已达到当前池的 98%。确认后以 64 MiB 量化、预留 64 MiB 跟随 desired；desired 已包含 non-streaming，不再加 BEM payload 或 non-streaming 数值。
- 新增额度受 `min(2 GiB, MemTotal/6)` 总池上限及真实内存余量限制，预留 `max(1 GiB, MemTotal/8)`。可新增空间按**已驻留 current**与 MemAvailable 计算，不能把尚未兑现的 base 或 own 额度当作已驻留内存。游戏 base 已高于这些上限时不反向裁低它。
- 游戏 base、最后一次自身写入及未确认 attempt 分开。具名预算 icall 观察器记录外部同值请求；内部写入不计成新 base。预算写入与原生预算/画质 API 调用通过独立递归执行锁序列化，IO 元数据锁不跨 Unity 调用。候选写入前复查值和 epoch，写入后校验预算、tier 与 epoch。
- 具名 `SetQualityLevel` 在离开旧 tier 前归还该 tier 的自身预算，随后才执行游戏原切档，避免返回旧 tier 时把自身额度误当原值。同 tier 的普通 Apply 保留租约；同 tier 内部回写自身预算视为 Apply echo，不同预算仍作为游戏新请求。
- 需求降低超过 128 MiB，且量化候选稳定 30 秒后回收；微小字节变化不会永久重置稳定窗口。低内存时可立即缩减新增额度，仍不低于游戏 base。缺失关键观察或可靠内存数据不新增额度。
- setter 拒绝、读回缺失、读回仍为旧自身值均保留正确恢复状态。未确认 attempt 不直接当游戏新 base；迟读回确认后同步 IO 租约。无效 getter 不得被视为“已恢复”。
- 热切换撤销模型时等待现有资源发现/回绑队列结束，再释放对应额度；完全退出模块则在 Unity pump 上恢复并确认，之后才退 hooks。原模块停机不回绑已发布模型的既有边界仍存在：恢复原预算不等于释放已发布 BEM 资源。

异常跨线程的画质切换不能被当成已确认 Unity-thread 恢复：观察器不在未确认线程增加 Unity 查询/写入；仍留在旧 tier 的未恢复租约会阻止成功 ACK，不以超时冒充完成。正常游戏画质路径已通过具名 native 合同确认，接口证据在 `build/issue25/budget-setter-contract.md`。初始同步、重入与失败序列通过接口 fixture 覆盖，仍需要实机确认实际档位事件。

当前 1038.31 MiB 的需求在这台手机内存条件允许时，期望协调到 **1152 MiB**，随后由引擎重新选择 target/载入 mip。写入预算不代表 loaded 当场提升；验收必须检查同地点画面和后续 target/loaded 收敛。达到内存安全上限时仍可能保留 mip 裁减，不宣称所有设备或任意数量的大包都能保持全部高精度需求。

### 构建与验收状态

- 策略 fixture：364 个明确执行的 CHECK；接口 fixture：19 个场景、1801 个 CHECK，0 failures。独立 MSVC `/O2 /DNDEBUG /W4 /WX` 编译通过，CMake Release 构建与执行通过。
- Android Release/lint、Windows shared CustomModel 构建通过；APK 原发布签名校验和两份 arm64 native 库与最终 stripped 输出逐字节核对通过。
- 测试包：`build/android-issues-25-26/BetterEndfield-3.5.3-issue25-texture-pool-fix-Android-arm64.apk`。ADB 安装前后模型索引、模块配置逐字节一致；没有切换任何功能或修改持久设置。
- 安装与构建收据：`build/android-issues-25-26/TEXTURE-BUDGET-REPAIR-BUILD.json`。没有计算产物哈希；未推送分支、未覆盖正式 release。
- 用户已确认本轮场景贴图恢复正常。留存运行日志记录预算从游戏原值 600 MiB 增至 1024 MiB，再至 1152 MiB，均读回确认成功；采样 pump 线程为 tid 4801。最后样本 current=879577458、desired=1100737138、target=1105459826 字节，pendingLoads=1518、loading=1，因此只记录本轮画面验收通过，不宣称所有纹理已完成加载。证据目录：`build/android-issues-25-26/device/20261007-texture-pool-user-clear`。

### 悬浮窗列表的偶发误报

用户曾看到“无已安装模型”，随后重新打开恢复。手机私有索引和框架保存的索引均为 12 个模型、1 个启用；解析通过，保存的目录、授权及模型数据未变。该候选包没有改动原来的列表 Client、Provider 和 Page 代码，现有日志未捕获当时的 IPC 失败，不能把重新打开后恢复当作初始化或授权故障的确证。

已证实的界面缺陷是：初始 `[]` 占位值在读取失败时仍被渲染为空目录。修正后初始显示读取中，失败显示错误和重试；保留此前成功获取的列表，但清除过期 revision、禁用写入，成功重试后恢复。只有成功返回真实空索引才显示“无已安装模型”。设置桥补充按状态变化记录的错误分类和恢复日志，不输出 token、请求正文、revision 或原始设置。底层偶发失败原因仍待复现日志确认。

### 热切换首次启用普通角色的漏覆盖

用户进一步确认热切换实际不生效。本轮保留日志 `build/android-issues-25-26/device/20261008-overlay-recovered/game-native-diagnostics.log` 的同一会话（Unity pump tid 3101）记录：初始仅 pelica 模型启用；梨诺选择更新到达 native，`owner=chr_0035_liino state=enabled revision=2`，随后 selection accepted，资源发现却立即报告 `no affected loaded generation or enabled resource`。

`ScanSceneInstancesForRebind` 在构建首次启用发现表时跳过 Android 的非 explicit-resource adapter。普通角色采用 UI LOD0 donor 配对主世界 LOD1，首次启用时既没有生成模型记录，也没有显式资源发现入口，故配置接收成功后仍等候游戏重新投递。该跳过逻辑在 main 已存在，不能将本轮纹理池修改写成它的引入来源。

修复必须沿既有配对校验和 `ProcessResource` 事务处理已加载实例，保持真实 Android LOD1 与 UI donor 获取/释放规则；不能直接把普通角色放进仅适用于显式自有 donor 的匹配分支。悬浮窗原来的“启用（下次加载生效）”以及 Java updater 等待新资源日志同时需要改成实际热切换模式，避免把模式状态与资源完成状态混为一谈。

该路径现已实现：对发生选择变更的普通角色建立发现表，以 UI donor 的原始 LOD0 身份与世界独立 LOD1 对应关系预检；材质、shader、骨骼、存活对象和 receiver 唯一性校验保留。预检不解码 BEM、不创建替换 Mesh/Material、不写 renderer。UI donor 暂缺保留 pending，利用已有一秒扫描重试；结构性错误继续拒绝。最终仍由原 `ProcessResource` 配对事务发布。

生产 scanner 的 Release 和独立 `/O2 /DNDEBUG /W4 /WX` fixture 通过，覆盖首次启用、世界模板与自然 clone、错误身份/独立 indices/骨骼/材质、临时 donor 恢复、已生成 A/B 后的 Original 身份，以及原 Windows 首启和场景回绑；不编造该 fixture 未提供的 aggregate CHECK 数。完整命令：`build/issue25/android-legacy-discovery-validation.md`。原 Android 世界配对 213 项和 LOD 68 项检查通过。测试包 `BetterEndfield-3.5.3-issues25-26-hot-switch-fix-Android-arm64.apk` 已安装且索引/配置未变；随后用户确认本轮热切换可用。留存原生日志记录梨诺关闭回绑 committed，以及安洁首次启用发现三个 root、配对发布 PASS 和回绑 committed。

### 更新后需要先打开 BE 才能读取悬浮窗

用户进一步说明，读取失败发生在更新后，手动打开一次 BE 才恢复。实际 service 102 SDK 中 `registerListener` 只登记 listener 并消费已有 Binder，不主动发起框架连接；模块的 `.XposedService` Provider 收到 `SendBinder` 后才连接并发布快照。手机实际 LSPosed v2.2.0(7854) 的进程启动和前台 Activity observer 会触发模块 Binder 强制投递；不能将公开 master 的较旧 UID 逻辑当成本机实现。Game 到 BE 的包可见性仍有记录，持久索引和开关一致；更新后的 owner 未运行、stopped=true 是采样事实，不据此单独宣告每次 IPC 失败的异常类型。证据：`build/android-issues-25-26/service-lifecycle-readonly/POST-UPDATE-LIFECYCLE-REVIEW.md`。

补充非导出 `MY_PACKAGE_REPLACED` receiver，启动 Application 并在后台等待连接最多 8 秒、必定释放广播。receiver 是尽力初始化，不能独自保证 stopped/cached owner 的框架投递。读取设置桥失败时，只有仍在前台的游戏 Activity 才可显式唤醒 BE 的透明 `FrameworkBootstrapActivity`：不消费 Intent 内容、不改设置，前台保留 500–1500 ms 以覆盖已有缓存进程的 Binder 重投，随后退出并清理回调。

客户端只对 `read_models`/`read_fov` 的连接或调用授权失败使用恢复入口，跨页面共用 30 秒限频，单次最多 4 次、间隔 250 ms 的读取重试；Provider 的数据错误不触发启动。`edit_models`、`disable_models`、`edit_fov` 不自动重放，原 UID/token/revision 检查保留。Game 短暂 pause/resume 使用既有按键释放与页面恢复逻辑。元数据诊断写入每个进程的私有 cache，仅最后两条、有界 ASCII 状态，不写 token、索引内容、请求正文或 revision。

列表状态 66 项、恢复策略 35 项、真实 Activity/Receiver 生命周期 43 项 host 检查通过。Android Release/lint、签名与打包的两份 arm64 库逐字节核对通过；自动重连候选 `build/android-issues-25-26/BetterEndfield-3.5.3-issues25-26-overlay-reconnect-Android-arm64.apk` 已用 ADB 安装，保留 12 个模型及当前 2 个启用选择。用户已确认列表、自动连接与热切换可用。私有状态记录捕获 `transport_IllegalArgumentException authorization=valid`，随后 `bootstrap=connected`、`framework=connected authorization_ready=true`，最终 `read_models result=ok revision_present=true`，没有将异常类型冒充已读取的异常消息。证据目录 `build/android-issues-25-26/device/20261008-overlay-auto-reconnect-confirmed`，收据 `OVERLAY-RECONNECT-BUILD.json`。没有要求先打开 BE，也没有关闭 LOD/NPC/BEM 做对照；范围限于本轮手机与框架版本。
