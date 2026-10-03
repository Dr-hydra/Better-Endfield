# RenoDX fork 第一人称隐藏：源码对比与双端方案

结论：该 fork 的隐藏更激进，主要依靠**约定名称整块隐藏、连通块 Head 权重多数、body 部件的 Head+Neck 过半权重**。这些规则可以覆盖更多 Neck 边界，但没有通用 Spine2 帽骨识别、普通 MeshRenderer 隐藏或 Android 无 GPU 数据的解决方案。可借鉴扫描、影子身份核对和 clone/恢复检查；不宜直接采用其整块名称规则和多数权重规则处理未知 BEM 混合躯干。

## 来源、版本与研究范围

- 仓库：[ItsTheSewerRat/renodx](https://github.com/ItsTheSewerRat/renodx)。2026-10-03 查询默认 `main` 为 `c43c78b75bab2c79fbc7d7b0d2b7588d2f93fce9`；该默认分支的 `src/games/endfield` 是画质实现，不能据其 shadows 着色器推断第一人称逻辑。
- 第一人称实际来源：[endfield-enhancer 提交 `7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7`](https://github.com/ItsTheSewerRat/renodx/tree/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer)，提交时间 `2026-09-13T21:07:37+02:00`。公开 `endfield-enhancer` 分支和 `Endfield-Enhancer-1.0` 标签当时均指向它。另核对 `endfield`、`endfield-dx11`、`endfield-fg` 的分支头树，均没有这组 enhancer 相机源文件；nightly 默认 main 不能替代上述功能分支。
- 在 SSD `D:/work/BetterEndfield-checks/renodx-endfield-20261003` 使用 shallow、`--filter=blob:none`、sparse、detached checkout；仅检出相机相关文本、入口/构建身份/许可证与祖先 AGENTS。enhancer 已检出 20 个文本文件，共 317,595 字节，Git pack 约 439 KiB；未复制整个源码仓库到 F 盘。
- 已读公开 fork 根及 `src/games/AGENTS.md`；本轮仅静态阅读，未构建/执行 fork、下载旧二进制或安装/启动游戏。我们的对照是 `main dd7131b4` 上已完成的本地头饰修复及当前共享工作树，未重跑上一轮测试，未新增生产行为。

以下外部链接全部固定到上述 enhancer commit；标记“推论”的内容不是现场复现。

网络中断后的续查完全基于上述本地快照：已重新核对 HEAD 与关键隐藏/投影函数，检出的源码没有本地改动。核心源码和 LICENSE 均已取得，研究结论不依赖继续联网；中断后没有刷新远程分支，因此不能确认此后新增提交或任何未公开构建的行为。

## 部件识别与混合 mesh：确切行为

| 机制 | fork 源码证据 | 与我们的区别及风险 |
| --- | --- | --- |
| 名称认定专用头部 | [camera_mesh.hpp:16–27][names]：renderer 名称必须 `s_actor_` 开头、含 `_lod`、不含 `shadowproxy`；`_face_/_hair_/_brow_/_eyebrow_/_iris_/_eyeshadow_/_hairshadow_` 为 dedicated；`_body_` 为 body_skin | 没有 `hat/maozi/brim/fur/cloth` 专用规则。我们仅把名称当提示，隐藏依据是实际骨骼和 draw。Mod 沿用原版 hair/face renderer 名称却放入身体时，fork 会误删整个可见部件 |
| Head/Neck 分类 | [camera_mesh_runtime.hpp:253–365][palette]：Head 的 `IsChildOf` 为 Head；仅经名称核对的 Head 直接父级为 Neck；其余为 0 | 与我们的 Head 祖先思路接近，但没有我们的明确 tail 链，也没有 Spine2 帽链例外。`maozi_a1_M/a2_M/a3_M` 在 Spine2 下，仍是 0 |
| dedicated 整块隐藏 | [camera_mesh_copy.hpp:49–110][decode]：dedicated 或全 Head palette 的路径跳过真实 skin 解码；[FilterHeadComponents:56–61][filter] 将所有三角形退化 | 可以隐藏命名 hair/face 的 Neck 混合边界；不是证明这些面只有头部影响。全 Head palette 路径是合理的保守证明，但 dedicated 名称分支没有这种证明 |
| 混合部件按连通块隐藏 | [FilterHeadComponents:64–112][filter]：三角形连通，并焊接位置、权重、骨骼完全相同的顶点；整块 Head 权重合计严格超过总权重一半即隐藏 | 一个连通头发/衣领或帽/身体混合块可能一起消失；Head 少数时，真正的头饰也可能整块留下。我们逐面要求三个顶点所有正权重均属于允许集合 |
| body 的 Neck 扩展 | [同函数:97–106][filter]：仅 body_skin 类，每个顶点 Head+Neck 量化权重都超过 32767 的三角形也隐藏 | 可删除全部 Neck 影响的 body 面，以及仍有近一半身体影响的面；不适合作为通用 Neck 策略。我们的非零 Neck/身体影响继续保留 |
| 权重及 draw 口径 | [camera_mesh_copy.hpp:94–110][decode] 将 Float32 权重量化为 UInt16；过滤整个捕获 index buffer 后，才核对 submesh；[过滤:89–100][filter] 遍历全部顶点统计 | 极小正身体权重可能量化成 0；连通统计不只计算实际 submesh draw 引用顶点。我们的 BEM CPU 路径保留 Float32 正权重语义，按实际 draw indices 分类 |

静态反例：四个连通顶点中三个纯 Head、一个纯身体，三角形分别为 `0,1,2` 和 `1,2,3`。fork 的整块 Head 比例为 3/4，因此两面都退化；我们的规则仅隐藏第一面。这里是直接代入源码条件的推导，**没有执行其代码**。

对 Spine2 帽骨：独立帽块全部权重为此类骨骼时，fork 的 Head 权重为 0，无法据此隐藏。若帽块碰巧与 Head 多数的几何连通，会被附带删除；这不能替代帽子语义识别，可能同时吞掉衣物。此前 Android 女管理员 `cloth_01_lod1` 的 156 项 palette 及 `maozi` 路径证据见 [头饰局部记录](FIRST_PERSON_HEAD_ACCESSORIES_20261003.md)；该记录仍缺原版权重/索引，不能证明帽子面的具体边界。

## 扫描、不可读 mesh 与 Android

- [camera_mesh_runtime.hpp:234–250][scan] 用 `GetComponentsInChildren(SkinnedMeshRenderer, includeInactive=true)`，可覆盖较深层级及同对象多个 skinned renderer；最多处理 512 个 renderer、单次候选上限 128。我们手动递归上限为深度 16/4096 节点、每对象 32 个 skinned renderer，未知顶点数的提前过滤已在上一轮修复。批量查询可作为扫描对照，但其 512/128 限制也不是无限覆盖。
- **两者当前都没有普通 MeshRenderer/MeshFilter 隐藏路径。** fork 明确只取 SkinnedMeshRenderer 类型，不能把泛用 `Renderer::shadowProxyMesh` setter 理解成支持所有 Renderer。
- [PollOne:103–145][readback]、[Start:201–220][gpu_init] 使用 `GraphicsBuffer::InternalGetData`；顶点 buffer 构造原生 descriptor 的只读 alias，清除 target 的位 2，再取数据。它绕过的是标准 mesh CPU 可读性/取数限制，仍需要后端能提供 buffer 数据，**不是现成 CPU 模型缓存，也不是 AsyncGPUReadback 不可用就一定可工作的证明**。
- [game_build.hpp:48][build] 限定两个 Windows UnityPlayer 文件身份，且 [clone 解析][awake] 依赖 x64 指令、固定 RVA、返回地址、原生 descriptor/managed array 偏移。部署 [metadata.json][metadata] 标为 Vulkan/x64；没有 Android ARM64 实现。不能把这条 Windows 取数路径当作 Android 无 GPU 回读的解决方案。
- 我们的 BEM CPU V1 桥直接读取当前替换模型 position/skin/indices/draw，支持 stride4 隐含首权重及 stride12/32，不依赖 GPU 回读；原版、keep/仅换纹理没有此几何时仍保留未知混合面。原版 Android 的缺口需要平台对应的几何/已验证遮罩，不能靠 fork 的名称或 descriptor 偏移补齐。

## 阴影与 EFMI/外部换模兼容边界

**阴影。** [Start:273–314][proxy_scan] 先收集当前模型 renderer 的 `shadowProxyMesh` 对象，跳过 sharedMesh 身份属于这些 proxy 的 renderer，并跳过名称含 `shadowproxy` 的对象。[绑定:76–129][binding] 把可见 mesh 换成私有 filtered clone，投影使用原有 proxy；只有没有 proxy 时才用完整 source mesh，没有独立 `ShadowsOnly` 快路径。

这比单看名称多了一个可借鉴的检查，但 mesh 身份本身不能证明某 renderer 仅用于投影：可见和投影也可能共享同一个 Mesh。应结合 renderer 角色/投影状态核对，而非全局禁止处理所有被引用的 Mesh。

我们对 `shadowproxy/hairshadow` 名称统一跳过。fork 的 `_hairshadow_` 反而属于 dedicated，在未被 proxy 身份规则排除时会隐藏。因此 `hairshadow` 是值得定点核对的遗漏候选：如果实际是可见 Head 辅助件，现有名称过滤会保留它；如果是影子代理，直接隐藏可能损伤投影。当前没有其材质/pass/可见性的现场证据，**本轮不改变这条规则**。

fork 保留 source/proxy 并非保证换模影子正确：如果 EFMI/BEM 替换后仍留着原版 proxy，它仍优先投射旧轮廓。我们的 **CPU 混合裁剪路径**明确将当前完整 BEM source 用作 shadow source，并在 A→B 外部接管时独立退休旧影子；这不能从 fork 的“保原 proxy”推导出来。我们的独立 `ShadowsOnly` 路径并不主动改写 shadowProxyMesh，因此也需单独确认 HG 是否继续使用某个旧 proxy，不能把混合路径的保证推广到所有独立部件。

**EFMI。** 已读的当前相机源文件没有 EFMI/3DMigoto 模块识别、版本协商、语义数据或 CPU 几何接口；可确认的是通用外部 mesh 防护：

1. [camera_mesh_copy.hpp:183–229][clone] 使用线程局部 `CloneAwakeScope`，只延后特定 clone 调用链的 Awake，核对其确属返回 clone；重上传捕获的顶点、索引和 submesh，确认 bindposes/blendshape 数量，再调用原生 Awake。[camera.hpp:374,494][hook] 将相应 hook 纳入 Camera 安装，并要求签名解析成功。
2. [copy:232–312][verify] 回读比较 clone 顶点、原/source 与 clone 索引，确认源 mesh 和 palette 未变化后再准备绑定；[UpdateBinding][binding] 对外部换 mesh/proxy 拒绝覆盖，只恢复自己仍持有的状态，并维护 `updateWhenOffscreen`。
3. **代码推论：**这可减少半成品 clone 被初始化/第三方回调接管的机会，也能读取 EFMI 已替换且稳定下来的 GPU 几何；但不能证明任意 EFMI 版本、安装次序或 hook 链都兼容。没有与外部模型服务的明确交接协议。
4. [PaletteMatches:16–21][binding] 保留的是 bones 数组对象，并比较数组内容；父级改变或同一数组原地改写不能可靠表达旧分类。[known/seen:291–309][proxy_scan] 复用同 renderer/mesh/palette 的结果，也没有几何修改代次检查。相比之下，我们上一轮已经对实际分类变化续发预算/使 patch 失效；同 mesh 原地更新几何仍应由后续明确的模型代次约定处理。

因此应借鉴“验证后赋值、外部接管优先、完整源投影”这些约束，保留我们正式 BEM CPU 桥；不要为了 EFMI 兼容直接引入 Windows 私有 Awake/descriptor 偏移到双端公共层。作者 [CN 支持提交][cn] 也明确写明 CN 实机行为未验证，不能当作我们的双端验收。

## 分阶段双端兼容方案（仅建议，尚未实现）

| 阶段 | 具体动作 | 安全边界与验证对象 |
| --- | --- | --- |
| 0：辨明当前残留 | 内部定点采集 renderer 类型/路径、mesh 身份、实际投影模式/proxy、CPU 桥状态、Head/Neck/`maozi` 的路径和已裁剪 draw 数；对比递归扫描与 includeInactive 的批量查询；特别核对 `hairshadow` 是否真的不可见 | 记录在日志/docs，不增加 UI 说明文字。先把漏扫、未覆盖 Renderer 类型、身体权重混合、缺几何分开，不按名称强删 |
| 1：明确独立部件 | 有普通 MeshRenderer 样本时，补 `MeshRenderer`/`MeshFilter` 类型、`MeshFilter.get_sharedMesh` 与 `GameObject.GetComponentsInChildren(Type, Boolean)` 契约，复用现有 Component Transform 和 Renderer 投影读写；仅对当前模型内、已确认独立头饰应用可恢复 ShadowsOnly/Off 路径 | 不能仅因未知静态整身 mesh 的 Transform 在 Head 下就认定全是头饰。影子代理、外部 enabled/mode 写入、退出/LOD 恢复一并核对 |
| 2：精确补语义 | 在现有角色/资源体系中挂经过验证的帽链根或部件语义，键含模型资源/版本/LOD、部件及实际 bone path；扩展允许集合为 Head+tail+这些已确认的 Spine2 帽链，再按当前 draw 的所有非零影响逐面裁剪。对已确认独立 face/hair 的 Neck 边界采用局部规则 | 不全局放行 Neck、Spine2 或 `hat/maozi` 名称；BEM 换包不能继承原 renderer 的“专用头部”例外。BEM mixed 使用当前包 CPU 数据，未知旧包仍按原保守规则，不强制升级所有包 |
| 3：补 Android 原版数据和生命周期 | 对原版 mixed、keep/纹理替换且无可用回读的 mesh，使用对应平台/版本/LOD、draw 范围及 palette 映射核对的离线遮罩或 CPU 数据；缓存跟随选包/形变代次，保留当前完整源投影与外部接管恢复 | 没有匹配数据就保留不确定面。PC 数据不自动套 Android；禁用新增投影的 Off 部件保持 Off；独立及 mixed 都检查 A→B 后影子轮廓、切人/LOD/退出及外部 hidden 状态 |

后续验收重点：女管理员 Spine2 `maozi`/`cloth_01`，噗切娜混合 `fur_01`、全 Head `fur_02`、含 Neck `fur_03`，提弗洛斯逆兔/校园 `cloth_02` 的不同身体比例，以及确认后加入的普通 MeshRenderer 样本。GPU 不可用、CPU NotReady/淘汰、热换包和原投影 Off 应单独覆盖；未取得对应证据前不能宣称这些具体残留已经解决。

## License 与移植边界

- 目标 commit 的根 [LICENSE][license] 为 **MIT**，版权声明 `Copyright (c) 2025 Carlos Lopez Jr.`；[addon.cpp:1–3][spdx] 也标记 `SPDX-License-Identifier: MIT`。目标相机目录没有单独 LICENSE 覆盖。公开源码可复制、修改、分发，但复制或实质性改写其代码时须保留版权及 MIT 许可声明；应记录 fork commit/文件来源。
- 我们根 `LICENSE` 是 AGPL v3。可将 MIT 相机算法纳入项目并保留原声明，项目既有 AGPL 分发/源码义务不因此消失；不需要把整个 fork 或 Windows/ReShade/Detours 依赖一并纳入。
- 纯 CPU 分类/拓扑算法可依法移植，但其多数/名称策略不符合我们保留未知躯干的要求，技术上不推荐原样使用。骨骼语义、双端 CPU 数据协议及恢复约束宜独立实现；如果实际复制其实现，仍保留 MIT 通知。私有 Unity RVA、x64 hook、反编译着色器及外部子模块不属于本次必要移植范围，根 MIT 不能替代各外部依赖/游戏资产的独立授权。

## 本轮检查与交付

已核对公开 refs/commit、相关源码分支树、许可证/文件许可标识，以及上述函数到最终绑定的调用链；所有能力/风险结论来自静态源码或明确标注的推论。只新增本记录，未改生产源码、未执行 fork、未重跑已有修复测试，未创建/提交/推送 Better Endfield 分支。本轮没有实机隐藏/投影/EFMI 联动验收。

[names]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh.hpp#L16-L27
[filter]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh.hpp#L36-L112
[decode]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_copy.hpp#L49-L135
[palette]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_runtime.hpp#L253-L365
[scan]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_runtime.hpp#L234-L250
[readback]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_runtime.hpp#L103-L145
[gpu_init]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_runtime.hpp#L201-L220
[build]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/game_build.hpp#L48-L52
[awake]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_clone.hpp#L1-L57
[metadata]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/metadata.json
[proxy_scan]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_runtime.hpp#L273-L314
[binding]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_binding.hpp#L16-L143
[clone]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_copy.hpp#L183-L229
[hook]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera.hpp#L361-L497
[verify]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/camera_mesh_copy.hpp#L232-L312
[cn]: https://github.com/ItsTheSewerRat/renodx/commit/c73b87e635c1c3ee17e1713f48adc43da914425b
[license]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/LICENSE
[spdx]: https://github.com/ItsTheSewerRat/renodx/blob/7b6ff8f19c4f233a3bf5054ae6ebf109d760eaf7/src/games/endfield-enhancer/addon.cpp#L1-L3
