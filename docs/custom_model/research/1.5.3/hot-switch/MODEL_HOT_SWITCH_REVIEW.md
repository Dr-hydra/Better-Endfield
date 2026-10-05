# BEM 包切换与下次资源加载生效评估（2026-10-01）

结论：可以实现“修改当前选包后，在游戏下一次真正加载该角色的原始资源时使用新包”。现有资源交付事务可以继续使用，不需要把替换搬到每帧人物扫描。但当前代码尚未支持运行中修改包配置；“切换队伍一定立即换包”不能由现有代码证明，队伍、详情页和 Android UI donor 都可能复用已经改写的资源。

本文为静态代码评估，没有运行游戏、抓取新的资源交付日志或修改生产代码。旧文档曾明确限定暂不热切换；本次用户已重新提出该需求，因此这里讨论新方案，不把旧约束当成拒绝原因。

## 现有路径与证据

| 部分 | 代码位置 | 当前行为 |
| --- | --- | --- |
| PC 选包/选项保存 | `ui/BetterEndfield.UI/Services/BemPackageService.cs:220` | 原子写 `catalog/custom-model/runtime.ini`，只修改配置文件。 |
| PC 界面提示 | `ui/BetterEndfield.UI/Views/CustomModelPage.xaml.cs:97`、`:115`、`:182` | 启用包、外观和组合选项均提示下次启动游戏生效。 |
| PC Host 更新监测 | `native/shared/host/host_runtime.cpp:142`、`native/shared/host/settings_store.cpp:181` | 每 500 ms 检查 `BetterEndfield.ini` 的时间/大小；没有监测模型 `runtime.ini`。 |
| Host 模块通知 | `native/shared/host/module_manager.cpp:240` | 有模块 `configuration_changed` 通道，但传的是 `BetterEndfield.ini` 中的模块节。 |
| 共享包配置读取 | `native/modules/custom_model/module.cpp:2549`、`:2645` | `ReadRuntimeRegistry` 在初始化调用，PC 从 `runtime.ini` 读，Android 从 Host 内存配置读。 |
| 共享更新接口 | `native/modules/custom_model/module.cpp:2705` | `ResourceConfigurationChanged` 只更新 `StandaloneLod`；注释明确包和角色选择固定到重启。 |
| Android 包准备 | `android/app/src/main/java/dev/betterendfield/android/XposedEntry.java:36`、`:57` | Application attach 时读取远端设置并准备 BEM；没有选包变化订阅或重准备入口。 |
| Android 代际文件 | `android/app/src/main/java/dev/betterendfield/android/BemInstalledResources.java:8`、`:27` | 已用不可变 generation 文件将框架发布包复制进游戏私有目录，这种文件身份适合未来热更新。 |
| Android native 传参 | `android/app/src/main/java/dev/betterendfield/android/RuntimeBootstrap.java:98` | 首次加载 native 前设置 `BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG`。 |
| Android 适配器 | `android/app/src/main/cpp/modules/custom_model/custom_model_module.cpp:201`、`:378` | Start 读取环境变量，拼完整共享 registry 并初始化替换事务；没有更新 registry 方法。 |
| Android 命令通道 | `android/app/src/main/cpp/modules/custom_model/custom_model_module.cpp:254` | 已有 Unity 线程命令消费入口，目前仅识别 `overlay_hide`，包更新尚不支持。 |
| 双端共同交付钩子 | `native/modules/custom_model/module.cpp:2496`、`:2390` | `_FinishWithAsset` 原函数执行前，按资源名选择包并调用事务；未匹配、失败和重复交付都继续原游戏链。 |

## 为什么仅刷新配置不足够

1. **已有资源去重没有选包版本。** `CompletedResource`（共享 `module.cpp:2171`）只保存 adapter、root 和 custom mesh/material 的弱引用。`IsCompletedResource:2203` 对同一 root，或自然克隆共享相同 mesh/material 的对象，直接判定已替换；未包含包路径、文件 generation、选项、校验模式或配置 revision。
2. **覆盖 registry 有悬空指针风险。** `mod_registry.h:40` 由 registry 的 `unique_ptr<OwnedCharacterAdapter>` 拥有 adapter；`ParseModRegistry`（`mod_registry.cpp:73`）创建它，completed 记录只保存裸 adapter 指针。直接重读覆盖 `g_registry`，旧记录引用的 adapter 会失效，不能简单在 pump 中调用现有 `ReadRuntimeRegistry`。
3. **清空去重后，接收者不再是原始 donor。** `PrepareResource`（`module.cpp:1893`）把当前 renderer.sharedMesh/materials 当成原始资产，校验原始索引数，并从它构造新 Mesh、材质和骨骼。已经替换过的对象包含上一包的几何、调色板、材质和贴图，可能被正常校验拒绝，也可能在放宽模式下错误套用第二包。关闭校验不能解决 donor 来源问题。
4. **CPU payload 缓存也需要包内容身份。** `AcquirePayload`（`module.cpp:2347`）当前 key 是路径、appearance/编码 options、skip_validation，128 MiB 上限、10 秒 TTL。切选项已可由不同 key 区分，但同路径覆盖 BEM 在 TTL 内会命中旧内容；热切换需不可变文件 generation 或显式 revision。
5. **Android world 与详情 donor 相连。** `world_resource_adapter.inc:12` 同步加载 UI LOD0 donor；`:15` 优先重用已提交 UI donor 的 mesh/material。即使 world 资源是新对象，只要 UI donor 仍在缓存，新配置也不能假设取得干净原始 UI 资产。world/UI 的 completed 和 donor 必须使用同一选包 revision。
6. **当前有意不持有完整原始资产。** `module.cpp:1605` 提交后释放构建期强 GC roots，completed 只保存自定义资产弱身份；原 mesh/material/bones 只在临时 `PreparedBinding` 中保留。模块停止也不回滚已交付模型（`:2746`）。因此现成代码没有可直接用于下一包的长期原始 donor。
7. **启动无包时可能没有替换入口。** `module.cpp:2674` 仅有启用包/探针时安装资源交付 hook；Android `native_bridge.cpp:84` 只在 custom 配置非空时创建 CustomModelModule，RuntimeBootstrap 在完全无功能时还会跳过 native 加载。要支持从“没有任何启用包”热开首个包，需要预留该入口，不能只支持已有模块的更新。

## 最小可行版本

建议先实现有明确边界的“下次新原始资源加载生效”，用于检验游戏实际缓存行为。

- PC：保留选包/选项原子写文件；新增 `runtime.ini` revision/通知或有限频率监测。不能依赖当前仅监测 `BetterEndfield.ini` 的 Host 通道。后台读取并解析候选 registry，Unity pump 在交付事务之外原子提交快照。
- Android：重新获取 remote preferences 的新 BEM index，只在 index/selection revision 变化时异步准备新的不可变 generation 文件。准备成功才通过 native bridge/现有 Unity frame 队列提交完整共享配置；禁止 UI/Binder/background 线程直接改 Unity 对象。
- 双端：registry 使用有寿命的快照；completed 以稳定角色/资源身份加 revision 记录，旧 adapter 在旧记录仍存活时保持有效。切换一个角色不应导致其他角色的去重失效。
- 持续识别旧资产：记录生成 mesh/material 归属的 revision，旧模板及其自然克隆继续交付已有包，显示该角色“等待资源重新加载”。不能为套第二个包把当前 custom mesh 当原始 mesh。
- Android：world 与 UI donor revision 必须一致；旧 cached UI donor 只允许服务它原来的 revision，不能被新包 world 构建误用。
- 文件：运行中第一版只切换已安装的不可变包和选项。PC 导入/覆盖/删除仍有 `RequireGameClosed`（`BemPackageService.cs:240`）保护；若将来支持运行中导入，需像 Android 一样保留旧 generation，不能删除待加载快照仍引用的文件。
- 暂不主动卸载游戏资源、不全局调用 UnloadUnusedAssets，不强行扫描/替换当前队伍对象。旧与新 revision 允许短期并存，生效状态按世界/详情资源分别反馈。

这个版本的工作量高于“重读一个 ini”，但可以沿用现有 ParseModRegistry、AcquirePayload、构建/提交/失败回滚和 Unity pump。缓存资源仍可能一直显示旧包，界面必须准确显示“已保存，等待资源重新加载”，不能承诺切队伍立刻生效。

## 如果要保证缓存对象再次使用时也更新

需要第二阶段解决原始 donor 和资源池生命周期。可选方向：核实游戏针对单一资源的缓存驱逐/重新加载接口，或在首次替换时保留可重建新包所需的原始 donor 数据与原资产身份。当前代码没有已验证的单资源强制卸载路径。

保留完整原始 Mesh、Material、Texture 的强引用会增加内存并改变目前“提交后不额外强持有”的策略，尤其与 Android 降峰值目标相冲突。应先观察哪些原始数据必须保留，是否能复制较小的 Mesh 声明/bindpose、材质模板与原贴图槽身份，而不是长期留住整套世界/UI 资源。

仅拦截队伍切换或 GameObject 克隆仍不够：它们可能使用同一已改写模板。若选择在缓存对象再次交付/实例化时重绑，必须有干净 donor、按 revision 的事务、失败保持上一包、world/UI/shadow/bones 一致性，并确认旧实例何时释放；这已属于完整热切换而不是简单配置刷新。

## 建议的最先验证项

用临时、目标角色限定的诊断记录 `_FinishWithAsset` 资源名、root/mesh identity、world/UI 类型和选包 revision。用户改变包后依次测试：切出队伍并切回、关闭再打开详情页、切图、回登录再进入。以实际资源交付和对象身份判断是否刷新，不把 UI 页面变化当成资源重载证据。

基础逻辑验证覆盖：未命中角色不变化、旧 registry adapter 寿命、旧 custom clone 不套新包、清空启用项、切换失败仍交付旧对象、新资源使用新 revision、Android donor revision 一致。同一场景多次 A→B→A 时再观察 CPU/GPU 内存、重复 mesh/texture 数与 old generation 释放，确认热切换没有进一步推高加载峰值。

## 本轮状态

最初评估阶段未实施生产代码；随后用户确认切配队/详情会重新交付资源，并授权实现双端实验开关。现已实施以下版本，默认关闭：

- `hot_switch` 与 `loading_optimization` 在共享 registry 中默认为 false，与开发者校验模式一样以本次游戏启动快照为准；切换这些开关需要重启游戏。选包、generation、外观/组合选项可在启动 hot_switch 后更新。
- PC 在实际资源交付前，最多每 500 ms 读取 `runtime.ini` 并精确比较内容；不计算哈希、不主动刷新场景。Android Java 后台准备新不可变包后，通过 `NativeCommandBridge.updateCustomModelConfig(String)` 排队，JNI 返回是否入队；Unity frame 再转交共享 registry，下一次资源交付消费。空启用列表也可启动实验模块。
- 选包 key 包括不可变/文件代际、选项、校验模式和优化模式；Android 文件路径带 generation，PC 包安装/覆盖仍要求游戏关闭。候选 metadata 诊断、坏 payload 或无效 options 拒绝整个更新，保留之前有效 registry，不把失败误判为关包。
- Adapter 用 shared ownership 跟随 completed 记录，旧 registry 释放不会形成悬空指针。同一 root 始终只有一条 active 记录；之前版本移除 root 弱身份，仅保留生成 mesh/material 的弱归属及共享原始 snapshot，以便仍使用旧模板资产的自然克隆再次交付时可找到原 donor。旧 custom 资产没有增加强引用，弱资产全部失活后由 Prune 清掉归属；不无限保留 retired registry。
- **仅实验热切换开启时**，首次成功交付保留原 Mesh、Material 数组和原骨骼数组。新包构建使用这些 pristine donor；当前已绑定的旧包资产单独保留为失败回滚目标。自然克隆需要按原骨骼路径重绑自己的骨骼；原始 donor 不可用时拒绝切换，保留已有模型。
- 再次交付已改写缓存 root 时按新 key 重建；关包时恢复原始 mesh/material/bones/enabled，Android 同时恢复 shadow 状态和代理可见性。没有全局 Unload 或全场景人物扫描。
- Android 新 UI donor 与 world 准备 bindings 合并为一次提交事务，旧缓存 donor 必须匹配当前 key，否则用保存的原始 donor 重建。准备/提交失败会保留上一版；正常回滚失败继续保留可能仍绑定的资产，与现有硬失败语义一致。

**内存取舍：** 热切换必须保留可重建原始 donor，因此会比默认模式额外持有原 mesh/material/骨骼及它们引用的贴图。这是实验开关的明确成本，不应宣称热切换同时减少这部分内存。关闭开关并重启恢复默认无长期原资产强持有策略；加载优化主要减少自定义贴图重复创建和解析期复制，两者独立。

生产 native fixture 已覆盖缓存 A→B→A、选择 key、关包恢复、失败回滚、原材质 donor、旧 adapter 寿命、同 root 去重记录和 GCHandle 释放；另补了原 mesh/bones donor 与当前 rollback 分离、metadata 拒绝保留旧配置的场景。它不使用真实图形设备，不能代替 Android/PC 游戏中几何、骨骼、world/UI 生命周期及内存趋势测试。
