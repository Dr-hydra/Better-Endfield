# 角色详情页连续采集

本轮用户先逐个打开已拥有角色的详情页，后续再用开屏模块补未拥有角色。无需逐角色配置目标，也不在本轮主动加载所有模型。

**后续补采已升级为跨重启持续模式。** 开发入口和唯一探针源码集中于 `tools/CustomModel/developer-tools/`，参见该目录 README。下文单进程描述保留首轮行为；持续模式不会在启动时消费请求，而是每个进程独立写文件，直到执行 stop。

## 用户操作

1. 从测试目录启动新的游戏进程；先由开发侧确认 `native-probe.status.json` 为 `ready` 或已有采集记录。
2. 逐个打开角色详情页，等模型显示完整后停留约 3–5 秒，再切换下一个。无需快捷键或手工保存。
3. 结束后告知开发侧检查记录。未完整的项按报告补采，不要求重复所有已成功角色。

启动采集后，本次游戏进程暂停模型替换，避免采到已替换的 Mesh/材质。用户的 Mod 启用配置不改写。
请求在探针就绪时改名为 `<run>.started`，只用于一次新进程；下次普通启动恢复保存的 Mod 配置。停止采集不会在当前进程中途恢复替换。
如果在探针就绪前模型已缓存，单纯再次打开详情页不保证触发交付；因此要求新启动游戏，缺项仍可能需要下次单独补采。

## 采集内容及边界

请求从当前已提取的游戏 manifest 构建精确白名单，仅包括角色 `characters/..._postmodel.prefab` 和 `uimodels/..._uimodel.prefab`，不把同名 NPC 根自动混入。
既有 `_FinishWithAsset` 入口读取根下所有 SkinnedMeshRenderer（含未激活的 LOD），不做全局对象扫描、不调用 Mesh/材质 setter、不安装绘制拦截。
记录原生根/renderer 路径、Mesh 名、顶点/总索引数、submesh 数量、完整顶点声明、stride、骨骼名称与路径顺序、bindpose 矩阵、材质顺序、Shader 名、纹理属性 ID 和纹理名称/尺寸/图形格式。
矩阵以九位有效数字序列化；本轮不导出几何/纹理像素。材质属性名依旧由离线资源提供，运行时 property ID 不等于 EFMI GPU hash。

一个资源成功后去重；失败最多尝试三次资源交付，保留每次不完整记录。未加载的角色只记为未观察到，不推断账号拥有情况。
世界与详情页分别保留证据，不把只采到详情页写成两种资源均已采到。后续离线核对同 Mesh、骨骼/bindpose、材质后，才可讨论等价复用。
原始采集 ID 为 `runtime:<renderer path>`，不是离线 Bundle/PathID；后续入库必须按完整路径、Mesh、骨骼和材质身份对照，不能直接伪造离线 ID。
原生资料采集成功不自动批准 EFMI 源映射、RabbitFX 语义或实际替换效果。

## 开发侧入口

```powershell
python tools/CustomModel/runtime_sweep.py arm --manifest artifacts/native-parser/endmin-inputs/Bundles/Windows/manifest.json
python tools/CustomModel/runtime_sweep.py status
python tools/CustomModel/runtime_sweep.py stop
```

补采必须重新 arm 并启动新的游戏进程；上一轮就绪时请求已被消费，之后普通重启不会继续采集。
可用 `--characters chr_0017_yvonne chr_0026_lastrite chr_0033_camille` 限定补采角色，保留各自 world/UI 根，不需要重复已采角色。

需要多次重启的补采改用 `--persistent`，或 `developer-tools/Probe.ps1 -Action arm`（默认持续）。同一 run 下分别生成 `<run>.<pid>.<tick>.jsonl`；status 汇总所有进程的独立记录，重复资源的布局/骨骼/bindpose/材质契约不一致会标为冲突。stop 同时停止当前进程采集并移除下一次启动请求。

原始 JSONL、请求元数据和汇总保存到 `%LOCALAPPDATA%\BetterEndfield\catalog\custom-model\native-probe\`。
每轮唯一 run ID，记录预期 manifest 版本及进程 ID；不跨游戏进程追加。版本是离线请求的来源版本，不冒称运行时查询得到。
汇总区分已完整、采集不完整、未观察到；写入过程中只忽略尚未结束的最后一行，已结束的损坏记录明确报错。
`stop` 等待运行时主线程维护回调写出 `stopped`；已有输出保留。原定向 V1 探针继续支持。

边界：最多 512 个根、每根 512 个 renderer、每资源 16 MiB、每轮约 512 MiB。只持有交付期间的临时托管引用。
本次已验证请求解析、资源发现、去重、只读采集、bindpose 输出、停止、引用释放，以及 Python 进度/来源校验。实机初始化和数据完整性等待用户启动后确认。

## 首轮实机结果

Run：`characters-20260919-202150-881718`，游戏进程 `19900`，来自测试目录中的探针 DLL。
用户完成已拥有角色的详情页浏览后，记录了 28 个角色详情模型、22 个世界模型，共 50 个资源根、1,617 条 renderer 记录（含不同 LOD 等子节点）。
完整性检查未发现不完整 renderer、非有限/尺寸错误的 bindpose 矩阵、骨骼与 bindpose 数量不符、越界顶点声明、缺失材质/Shader 名或 BetterEndfield 替换 Mesh。
女管理员 11 部件 × world/UI 共 22 项、佩丽卡 10 部件 × world/UI 共 20 项，与既有资料的 Mesh 索引数、完整骨骼顺序、材质顺序、stride 和顶点声明均一致。

白名单中尚未观察到详情资源：管理员（男）`chr_0002_endminm`、伊冯 `chr_0017_yvonne`、别礼 `chr_0026_lastrite`、卡缪 `chr_0033_camille`。此项表示未采到，不推断账号是否拥有。
另有 `chr_0036_jsspsi`、`chr_0037_chenpast`、`chr_0038_purrche` 在本次 manifest 中仅找到 world 根，不计作用户漏看详情页；后续开屏/离线阶段单独核查用途。
部分已采详情的角色尚未采到 world，先保留差异，待离线等价核对或开屏补采，不要求本轮重复全部角色。

完整审计：`artifacts/native-parser/characters-20260919-202150-881718.audit.json`；原始数据及汇总仍在上述本机 `native-probe` 目录。没有将这些原始观察自动标记为全角色转换资料已完成，也没有生成 EFMI GPU hash。

2026-09-19 20:41 核查：原 JSONL 未增长，三个角色尚无补采记录。已准备独立补采 run `characters-20260919-204150-191549`，仅选择伊冯、别礼、卡缪共六个 world/UI 根，等待用户新启动并确认探针就绪后采集。

随后用户要求跨多次重启补采四个角色。上述未启动的三角色请求已由新的持续请求取代，目标为男管理员、伊冯、别礼、卡缪，共八个 world/UI 根。保留首轮及未启动请求元数据，不伪造补采成功记录。

## 四角色跨重启补采结果

Run：`characters-20260919-205128-125595`。四个独立游戏进程的 JSONL 均通过来源和完整性核对：

| 角色 | 已采资源 | renderer 记录 | 进程 |
| --- | --- | ---: | ---: |
| 管理员（男） | `chr_0002_endminm_postmodel` | 53 | 27564 |
| 伊冯 | `chr_0017_yvonne_postmodel` | 51 | 32916 |
| 别礼 | `chr_0026_lastrite_postmodel` | 66 | 34160 |
| 卡缪 | `chr_0033_camille_postmodel` | 83 | 31300 |

合计 253 条 renderer 记录。没有发现不完整记录、矩阵非有限值、骨骼/bindpose 数量不符、非法顶点声明、缺失材质/Shader 名、替换 Mesh 或跨进程契约冲突。审计保存在 `artifacts/native-parser/characters-20260919-205128-125595.audit.json`。
四者详情页根均未直接采到；这是开屏加载 postmodel 的预期范围，后续需依据离线引用、骨骼/bindpose 与材质证明 UI 等价后才复用观测。
两轮累计覆盖 32 个不同角色的至少一种原生模型，不能写成 32 个角色的 world/UI 双路均直接完成。用户确认操作结束后执行 stop，解除后续重启自动采集；保留原始文件与工具供版本更新复用。

## 资料入库结果

当前快照的 32 角色离线解析已完成，与两轮观察合并入库 370 个 LOD0 主体部件：433 份资源根/部件契约直接观察，307 份由相同 Mesh、骨骼路径/顺序和材质身份复用。卡缪技能部件材质、提弗洛斯衣服部件骨骼存在 world/UI 差异，排除共用映射。62 个部件的布局或多子网格只记录资料，尚不支持自动转换。

19 项针对性 Python 检查、UI 报告展示检查通过；以新目录重转女管理员既有标准包并通过原生 BEM 校验。没有重测游戏运行时，也没有读取佩丽卡源包。详细证据、工具命令和 EFMI 资源对应缺项见 [角色资料入库说明](BEM_CHARACTER_CATALOG_20260919.md)。
