# CustomModel 资源交付与 LOD A/B 验证

工作树：`.worktrees/dev-custom-model-design`。测试目录：`E:\Dr.Hydra\Better Endfield\artifacts\BetterEndfield-win-x64`。用户授权直接覆盖、不备份；游戏由用户启动和验证。没有修改主工作树源码，没有计算产物哈希。

收尾状态：用户确认当前新版原生资源实现开发完成并授权推送开发分支。正式版采用已通过的 B5 行为；临时实验开关、B6 观察代码、新增测试程序和 A/B 构建/部署脚本已撤除。下文保留实验期间的证据，最终去留与未执行项目见末节。

## 共同材质样本及 A 结果

管理员原包 24 条纹理中：`290f53c8` 是公共脸部 `_HighlightMap`，`9750205d` 是公共表情 `_EmotionMap`；两个实际材质槽均已查到，补充明确名称 pin。它们不是隐藏部位纹理。

`7d330dbd` 为公共雨滴纹理，`908140d5` 为公共雪纹理，属于 HG 全局天气资源，不在该模型序列化材质槽中。四项均已与当前客户端压缩纹理原始数据逐字节核对：前三项匹配 mip0，雪纹理匹配 mip2。只对这份已验证样本保留 22 项材质纹理，雨雪继续使用游戏原资源；不推广为无条件删项或运行时忽略未绑定纹理。

样本 `artifacts/resource-delivery-ab/endmin-materials-verified.bempoc` 为 54,871,928 字节。保留的几何与纹理 payload 不变；详情证据在 `artifacts/endmin-material-audit/material-verification.json` 和 `prepare_endmin_material_ab.py`。

A-baseline 于 21:08:02 部署。随后用户确认“替换都正常”，并指出 AI 角色没有替换、AI 会自动切换低 LOD。按会话上下文记录管理员和佩丽卡世界、详情替换通过，AI LOD 未通过。不能据此宣称 B1～B4 修复了资源交付，也不能把最初佩丽卡失败的原因定为生命周期。

## 互斥版本

| 版本 | 相对 A 的唯一实验项 | 实机状态 |
| --- | --- | --- |
| A-baseline | 原重建资源行为，共同材质样本 | 世界、详情正常；AI 未替换 |
| B1-retain-through-finish | 临时构造强引用延至原交付回调返回 | 未测试 |
| B2-verify-duplicate-bindings | 同根重复交付也验证当前绑定 | 未测试 |
| B3-main-thread-only | 非主线程入口跳过 | 未测试；设计明确暂缓，不因实验自动转正 |
| B4-legacy-visible-writes | 可见部件不重复写 enabled | 未测试 |
| B5-sustain-lod0 | 持续保护 LOD bias，校验并修复质量/NPC 参数 | 用户确认 LOD 正常，懒加载也通过 |
| B6-lod0-skin-field-audit | 在已通过的 B5 上只增加全部自定义 Mesh 字段只读观测 | 专项检查通过；用户无法复现，登记为已知问题 CM-001，未确认根因 |

实验期间，标准 CMake 默认 A（0），B5 使用显式变体 5，没有同时打开 B1～B4。收尾后的正式 CMake 已移除变体配置，直接使用 B5 的持续 LOD 行为。

## LOD 源码与当前原生实现证据

旧 `module_npc_lod_lock.inc:94` 同样以两个布尔标记直接返回。旧版在角色替换激活后才开启；新版本根据已启用 Mod 提前开启。两者都调用 `HGRenderPipeline.EnableForceLOD0`，都不构成持续锁定。新 `LodState::Update` 原同管线 applied 快速返回也不核对实际状态。

读取当前本地 GameAssembly.dll，结合 20260903 IL2CPP 元数据离线反汇编，记录于 `artifacts/resource-delivery-ab/lod-native-audit.json`：

- `EnableForceLOD0` 的未热修复分支设置 parent bias 为 `1e-7`，并将 art tag 0～34 设置为相同值；没有持久锁定标志。
- `OnArtTagSettingParametersChanged` 在 deviceType 非 4 时调用 `RegisterArtTagLODBias`，重新向相同的 parent/art tag setter 写入画质参数；不需要更换管线对象。
- `DisableForceLOD0` 调用 `RegisterArtTagLODBias` 恢复当前游戏配置。
- 这些方法包含 iFix 分支；静态证据证明存在覆盖机制，并不证明本轮游戏已触发，也不代表热修复分支完全等同。B5 实机干预计数用于进一步核实。
- RVA 仅用于离线审查记录；运行时按程序集、类型、方法签名解析 setter，无原生地址硬编码。

## B5 行为及验证

启用仍复用原 `EnableForceLOD0`；成功后保护 `HGCullingSystem.set_parentLODBias` 和 `SetArtTagLODBias` 的已核验 0～34 范围，每次调用仍向原 setter 转发一次。锁未启用时原样转发；关闭、失败回退和管线更换先解除保护，再调用原恢复接口。

Canvas 维护入口读回 `maximumLODLevel` 及五个 NPC 静态参数，只在被改动时重写并读回。修复失败清除 applied、恢复原始快照；不把被游戏覆盖后的值保存为新的原始值。管线弱引用、Mod 强制开启与独立开关规则保留。没有增加模型实例扫描或低 LOD 网格补写。

日志 `LOD guard active` 表示保护已启用；`LOD guard interventions: parent=… artTags=… parameterRepairs=…` 为实际阻止覆盖或完成参数修复的累计计数，变化时最多每秒记录一次。计数不是最终画面通过的证明。

Release 编译通过。资源回归程序 806 项检查通过，包括：A 重现同管线被覆盖而 applied 仍为真；B5 阻止 parent 和全部 35 个 tag 覆盖；读回修复五项 NPC 和质量参数；稳定帧不重复开启；关闭透传恢复；管线更换；修复失败回退；原函数调用次数与 MethodInfo 转发。并使用当前 UnityPlayer 验证 `m_BonesPerVertex` 位移，以及管理员 11 组件/22 纹理、佩丽卡 10 组件/13 纹理实包解析。

构建存在既有 MinHook CMake 兼容性弃用提示及 HDE64 C4701 提示，无新模块编译错误。

下一次实机重点：同一角色由玩家控制切换为 AI 队友后，近远距离是否仍保持替换；同时回归角色详情。若仍失败，依据 B5 实际拦截计数和当前资源调用链继续缩小范围，不将静态推断写成已证实根因。

## B5 实机通过及后续随机部位问题

用户已确认 B5 的 LOD 和懒加载正常。21:29～21:31 会话确实记录了 parent 4 次、art tag 140 次覆盖拦截，以及 5 次 NPC 参数修复。这证实本次运行存在游戏回写，不再只是静态推断。上述 LOD 测试步骤已完成，不要求用户重复执行。

用户另报配队界面管理员偶发丢眼睛，切换角色无效，退出重进配队界面恢复。用户明确强调：Mesh 损坏可能随机影响任意部位，不能仅检查眼睛。当前尚未确定根因。

本轮已有日志中，管理员 C7 原生字段每次为 1；其他部位按对应原版为 1 或 4。字段初始化仍在几何/bindposes 上传前执行，偏移来自两个一致的序列化函数；上传后不一致会拒绝提交。已有成功记录没有证明字段取值或定位错误，也不能证明交付之后一直正确。原有检查缺少交付后阶段。

B6 保留 B5 全部行为，只读观测所有已创建自定义 Mesh 的 `m_BonesPerVertex`，每个部位记录各自原版字段预期值。观察阶段为 `committed`、`after-finish`、`redelivery` 和每秒维护；维护只在字段、绑定、可见性或存活状态变化时输出 `Mesh skin audit`。记录资源根、部件、Mesh 和 renderer 身份；使用弱引用，不增加长期强持有，不修改字段或自动重建。

范围限制：记录的是交付资源里已登记的 Mesh 和 renderer；自然实例共享同一 Mesh 时也能观察该 Mesh 字段变化。游戏若另行复制出未登记的独立 Mesh，不会被这项观察自动纳入，不能把无异常日志解释为所有场景实例均正常。

仅编译本次模块和测试目标，运行 `BetterEndfield.CustomModelResourceTests.exe --mesh-skin-audit-only`：94 项相关断言通过。覆盖两角色不同部位独立变化、原版来源各自保存、回调返回检查去重、绑定/可见性变化、死亡对象不读取、临时强引用全部释放、旧 B5 不启用观测。没有重跑完整资源、贴图、LOD 或实包解析测试。

最新处理决定：用户后续无法复现，要求将问题独立归档，详见 [已知问题 CM-001](CUSTOM_MODEL_KNOWN_ISSUES.md)。暂停主动复现和诊断，不把暂未再现视为修复通过；仅在后续自然出现时结合现有只读日志继续调查。无需重测已经验收的 LOD 和懒加载。

## 开发收尾：未执行的 A/B 与正式行为

2026-09-16，用户表示暂未发现其他问题，要求撤掉测试和检测代码、记录未完成 A/B，然后确认新版开发完成并推送远端开发分支。以下未执行项保留为历史记录，不作为已验证结论，也不继续要求用户补测。

| 未执行项目 | 原拟验证内容 | 正式版保留行为 |
| --- | --- | --- |
| B1-retain-through-finish | 临时构造强引用是否需要持续到原 `_FinishWithAsset` 返回 | 沿用已实机通过的构造期保护；原回调前释放临时保护 |
| B2-verify-duplicate-bindings | 同一资源根的绑定被改回后，再次交付是否应重新核对 | 保留已通过版本的同根判重；未引入实验中的重复绑定检查 |
| B3-main-thread-only | 非主线程入口跳过是否影响资源覆盖 | 按已确认设计继续暂缓主线程跳过策略；互斥锁不等于线程校验 |
| B4-legacy-visible-writes | 可见部件不重复调用 enabled setter 是否影响显示 | 保留正常提交时的 enabled 写入与读回 |

B5 的 LOD 机制和懒加载已实机通过，持续 bias 保护及质量/NPC 参数维护转为正式逻辑。B6 的专项程序已通过，但偶发故障没有复现、未完成故障发生时的采样或根因验证；只读跟踪撤除，CM-001 继续记录为未解决。

本次撤除：变体头文件及 CMake 开关、B1～B4 实验路径、B6 Mesh 字段观测及附加弱引用、LOD 干预计数和周期统计、仅用于日志的 Mesh getter、逐部件成功跟踪、A/B 构建/部署脚本，以及本轮新增的资源/Hook 验证程序。旧版源码和原有 legacy 生命周期测试保留作历史参考，不进入正式 DLL。

保留必要的生产校验：源 Mesh 身份与声明、动态字段定位和初始化、上传后字段一致性、材质必需绑定、提交读回与失败恢复。LOD 每帧维护属于已验证修复，不能随检测代码一并撤除。

清理验证仅限正式 CustomModel/Host Release 编译、临时分支残留检查和差异格式检查；不再重复完整自动测试或已通过的游戏场景。更细的 GPU 释放测量、弱托管包装器提前消失等边界没有新增实机结论，仍不应扩大“懒加载通过”的含义。
