# 初次资源交付替换的约束

## 已验证链路

0.0.38 实机安洁：FinishWithAsset 返回资产 000000439370EBC0，Instantiate 使用同一对象，克隆 0000004392F53240（instanceID -103592）。ComplexModelViewPart 和 BaseModelViewPart 后处理前后均为此克隆。五个快照的 41 个 Renderer、Mesh 指针/名称及 bones 长度逐项相同。没有证明每个 bone 引用和矩阵相同；未证明动画内部状态已经就绪。NPC CreateSMSInfoForPostModel 未命中。

管理员不在队伍也会提前交付 postmodel；UI 使用单独 uimodel。不能将管理员预加载规律推广至所有角色。

## 当前替换资源实际包含什么

读取测试配置目录的 endmin-casualwear-c9.bempoc（56,182,760 字节，格式 24）：11 个 Component。数据由 component header、三条顶点流、索引与贴图区域组成，没有独立骨架、bindpose、Avatar 或 AnimatorController。没有计算产物哈希。

| Component | 原索引数 | 自定义顶点数 | 声明最大骨骼槽位 |
| --- | ---: | ---: | ---: |
| C0 | 27615 | 7911 | 27 |
| C1 | 9000 | 1807 | 52 |
| C2 | 4524 | 1548 | 0 |
| C3 | 20577 | 2639 | 13 |
| C4 | 1638 | 508 | 41 |
| C5 | 16524 | 3490 | 43 |
| C6 | 117 | 41 | 0 |
| C7 | 1386 | 276 | 5 |
| C8 | 90 | 34 | 20 |
| C9 | 101994 | 12873 | 155 |
| C10 | 2286 | 0（无几何） | 0 |

C2/C6 含隐藏标志。以上最大索引来自文件头，并非本轮重新逐顶点验算。

## 源码确认的关系与校验缺口

- PrepareReplacement 以 original_index_count 匹配部件；不是按模型、LOD、Mesh 名称的联合标识匹配。初次交付方案必须限定目标预制体和明确部件，避免把同索引数误认为同一来源。
- BuildMeshFromComponent 复制原 Mesh 的顶点声明、bindposes，使用 BEM 的权重/索引，不建立新的骨骼层级。
- ValidateRendererSkin 检查使用的槽位未越界、骨骼 native 对象存活、bindpose 有限且非零；没有验证逐槽骨骼名称/层级顺序，也没有验证 bindpose 与该骨骼坐标系的对应关系。
- 新 Mesh 当前命名为 BetterEndfield.C*.BEM。早期替换应保留原 Mesh 名称；已知 NPC 路径按 Mesh.name 匹配，当前 gameplay 路径未证实存在同样依赖，但不能主动破坏原标识。
- 实机的 bones 长度相同不等于骨骼语义相同。旧文档仅凭最大索引和长度相符得出的“语义已确认”不能替代逐槽比对。

这些是校验缺口和方案约束，不是已证实的随机地面错位根因。

## 下一步实施边界

1. 保留原 postmodel/uimodel 的层级、bones 数组顺序、rootBone、Avatar、AnimatorController 和组件。现有 BEM 无法独立替代一个完整角色预制体。
2. 在原资源首次交付、尚未克隆消费时，对精确匹配的目标部件构建 Mesh，保留原 bindpose/名称；所有部件先准备成功，再提交修改，失败须完整撤回。
3. 不直接修改被 postmodel 与 uimodel 共享的原 Mesh；使用独立 Mesh 并分别处理两个预制体入口，否则影响范围会越过当前目标。
4. 当前 Finish 钩子只筛选 postmodel 的两个 hash；UI 完整覆盖尚未具备，不可声称已经完成。
5. 下一次定向采样应比较一个部件（管理员 C9）的逐槽 bone 名称/相对路径、rootBone、bindpose，以及模板和克隆的 Animator/Avatar。先验证对应关系，不调用 BakeMesh、不重新初始化 Animator。

本轮只做文件/源码核对和记录，未修改或部署运行时逻辑。
