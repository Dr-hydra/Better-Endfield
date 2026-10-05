# 自定义模型与 BEM：游戏版本 1.5.3

[模块入口](../../INDEX.md)

这里保留研究、来源、实施阶段和历史结论。条目状态来自用途调查，研究正文中的来源、勘误、未验证项和适用边界保持原样。

## ANDROID_CUSTOM_MODEL_REUSE_AUDIT.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android 自定义模型替换：复用审计与执行记录](android-reuse/ANDROID_CUSTOM_MODEL_REUSE_AUDIT.md) | Android 复用审计与追加执行记录，早晚状态混合 | 历史过程 | 1.5.3 |

## ANDROID_CUSTOM_MODEL_STATIC_AUDIT.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android 模型替换静态兼容性审计（2026-09-20）](android-mesh/ANDROID_CUSTOM_MODEL_STATIC_AUDIT.md) | Android 静态兼容性调查，旧发布关闭/托管桥结论已纠正 | 部分结论已替代 | 1.5.3 |

## ANDROID_LIGHTING_SHADOW_LOD.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android 光照、阴影和 AI LOD 诊断](android-lighting-lod/ANDROID_LIGHTING_SHADOW_LOD.md) | Android 受光、阴影、LOD 与实机反馈边界 | 来源/证据 | 1.5.3 |

## ANDROID_MESH_SUBMISSION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android Mesh 提交与模型替换（2026-09-21）](android-mesh/ANDROID_MESH_SUBMISSION.md) | MeshData ABI、提交/回读及 Android 世界 LOD1 事务 | 阶段实施记录 | 1.5.3 |

## ANDROID_MESH_WRITE_METHOD_ENTRYPOINTS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Android Mesh 写入方法级入口静态调查](android-mesh/ANDROID_MESH_WRITE_METHOD_ENTRYPOINTS.md) | Mesh 方法级写入入口调查，部分结论已纠正 | 部分结论已替代 | 1.5.3 |

## BEM_CHARACTER_CATALOG.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [角色资料入库与 EFMI 原资源对应](bem-character-catalog/BEM_CHARACTER_CATALOG.md) | 32→33角色入库与EFMI对应的阶段变化、排除项 | 既有归档 | 1.5.3 |

## BEM_COMPONENTN_AUTOMATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [ComponentN 自动转换与角色资料接入](bem-character-catalog/BEM_COMPONENTN_AUTOMATION.md) | ComponentN首次接入、后补32角色身份的历史过程 | 既有归档 | 1.5.3 |

## BEM_EFMI_IDENTITIES.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [EFMI 原资源身份补全](bem-character-catalog/BEM_EFMI_IDENTITIES.md) | 360入口/1181纹理身份算法、版本manifest及10个多子网格限制 | 既有归档 | 1.5.3 |

## BEM_MATCHING_REVIEW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM matching review (2026-10-01)](bem-matching/BEM_MATCHING_REVIEW.md) | 共享Texture pin、错误元数据及Android导入匹配修正 | 既有归档 | 1.5.3 |

## BEM_MODEL_MANAGEMENT.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM 模型管理局部修正（2026-10-03）](bem-management/BEM_MODEL_MANAGEMENT.md) | 双端筛选/关闭全部的局部保存、UI及热切换证据 | 既有归档 | 1.5.3 |

## CHARACTER_MODEL_REPLACEMENT.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Better Endfield 角色模型替换可行性结论](model-replacement/CHARACTER_MODEL_REPLACEMENT.md) | 2026-09-08选型、EFMI实测及客户端约束 | 既有归档 | 1.5.3 |

## CUSTOM_MODEL_ARCHITECTURE.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel 架构设计](architecture/CUSTOM_MODEL_ARCHITECTURE.md) | 实现前设计冻结及样本待验证项，不是现行稳定契约 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_BONES_PER_VERTEX_ANDROID.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [`m_BonesPerVertex`：当前实现与 Android 适配结论](mesh-skinning/CUSTOM_MODEL_BONES_PER_VERTEX_ANDROID.md) | native Mesh 蒙皮字段动态解析和 Android 适配调查 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_CAPABILITY_COMPATIBILITY.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [CustomModel 当前能力与兼容性声明](capability-v25/CUSTOM_MODEL_CAPABILITY_COMPATIBILITY.md) | v24/v25阶段能力矩阵，工具README仍将其作为入口 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_EARLY_DELIVERY_CONSTRAINTS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [初次资源交付替换的约束](early-delivery/CUSTOM_MODEL_EARLY_DELIVERY_CONSTRAINTS.md) | 首次资源交付实验、源部件与骨架语义边界 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_EFMI_SAMPLE_ENDMIN_CASUALWEAR.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [EFMI 样本分析：Endmin in Casualwear](endmin-casualwear/CUSTOM_MODEL_EFMI_SAMPLE_ENDMIN_CASUALWEAR.md) | 真实EFMI样本结构、draw/bone/texture设计输入 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_GILBERTA_OUTFIT_B.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [洁尔佩塔外观分支修正与 UInt32 索引](gilberta-outfits/CUSTOM_MODEL_GILBERTA_OUTFIT_B.md) | Outfit B用户选择、UInt32索引放宽与测试包证据 | 阶段实施记录 | 1.5.3 |

## CUSTOM_MODEL_GILBERTA_REASSESSMENT.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [洁尔佩塔 gilberta_12 默认状态复核](gilberta-outfits/CUSTOM_MODEL_GILBERTA_REASSESSMENT.md) | 默认分支复核，实际选衣与索引范围后由Outfit B纠正 | 部分结论已替代 | 1.5.3 |

## CUSTOM_MODEL_GILBERTA_VALIDATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [洁尔佩塔默认外观 BEMPC25 测试包](gilberta-outfits/CUSTOM_MODEL_GILBERTA_VALIDATION.md) | 默认外观测试包验证，后改为Outfit B及UInt32 | 部分结论已替代 | 1.5.3 |

## CUSTOM_MODEL_HASH_LOD_CONVERTER.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Hash/LOD Mod 独立转换器与两份样本检查](hash-lod-conversion/CUSTOM_MODEL_HASH_LOD_CONVERTER.md) | 初次Hash/LOD样本评估，能力已由v25实施记录更新 | 部分结论已替代 | 1.5.3 |

## CUSTOM_MODEL_LOD_LOCK_AND_VFX_FINDINGS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [终末地运行时 NPC LOD 锁定与发光部件治理逆向调研报告（2026-09-14）](lod-bones-vfx/CUSTOM_MODEL_LOD_LOCK_AND_VFX_FINDINGS.md) | 原生LOD0方案校正及早期Hook/VFX研究证据 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_MESH_NATIVE_FINDINGS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [Mesh 生成与 HG 蒙皮路径定位（2026-09-14）](mesh-skinning/CUSTOM_MODEL_MESH_NATIVE_FINDINGS.md) | Mesh生成、HG蒙皮、F12诊断和证据边界 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_MOD_7B260_INSPECTION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [mod_7b260.zip 静态检查（2026-09-15）](mod-7b260/CUSTOM_MODEL_MOD_7B260_INSPECTION.md) | 单个源包静态结构与转换阻点调查 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_MULTI_LOD_BONE_ELEVATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [终末地模型替换 LOD 方案现状与跨 LOD 骨骼补齐记录（2026-09-15）](lod-bones-vfx/CUSTOM_MODEL_MULTI_LOD_BONE_ELEVATION.md) | 原生LOD0策略及跨LOD骨骼辅助实现记录 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_MULTI_MOD_REPLACEMENT_WINDOW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [多角色 Mod 的替换窗口调研与建议（2026-09-15）](replacement-window/CUSTOM_MODEL_MULTI_MOD_REPLACEMENT_WINDOW.md) | 多角色替换窗口、双路径实验及文末验证进展 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_POC1_C9_PLAN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel PoC-1：Endmin Casualwear 衣物优先](endmin-casualwear/CUSTOM_MODEL_POC1_C9_PLAN.md) | 首次C9衣物视觉PoC与C10隐藏验证计划 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_POC21_RAW_CHANNEL.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel PoC-2.1: guarded Mesh raw-channel path](mesh-raw-poc/CUSTOM_MODEL_POC21_RAW_CHANNEL.md) | 旧raw-channel假设；正文明确由PoC2.2取代 | 部分结论已替代 | 1.5.3 |

## CUSTOM_MODEL_POC22_RAW_STREAM.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel PoC-2.2: exact source layout via engine bindings](mesh-raw-poc/CUSTOM_MODEL_POC22_RAW_STREAM.md) | 精确源布局ABI与旧PoC纠错，catalog构建保留证据标签 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_PURRCHE.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [噗切娜角色资料（2026-10-01）](purrche-catalog/CUSTOM_MODEL_PURRCHE.md) | 噗切娜catalog来源、自动化范围和随包分发边界 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_REBUILD_BEHAVIOR_COMPARISON.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [CustomModel 重建前后行为对照](resource-delivery/CUSTOM_MODEL_REBUILD_BEHAVIOR_COMPARISON.md) | 资源重建前后静态对照；正文声明早期失败不代表收尾结果 | 部分结论已替代 | 1.5.3 |

## CUSTOM_MODEL_RESOURCE_AB_VALIDATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [CustomModel 资源交付与 LOD A/B 验证](resource-delivery/CUSTOM_MODEL_RESOURCE_AB_VALIDATION.md) | B5/A-B实机记录、撤除项与未执行项，catalog构建证据 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_RESOURCE_REBUILD_PROGRESS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [CustomModel 原生资源重建：完成记录](resource-delivery/CUSTOM_MODEL_RESOURCE_REBUILD_PROGRESS.md) | 资源重建阶段完成、实验撤除及UI/转换器未包含范围 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_RESOURCE_RELEASE_CALL_CHAIN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [CustomModel 正式版：资源加载调用链与分阶段确认](resource-delivery/CUSTOM_MODEL_RESOURCE_RELEASE_CALL_CHAIN.md) | 资源交付调用链与迁移基线，生产module.cpp注释回指 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_RUNTIME_PROBE.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel Runtime Probe](runtime-probes/CUSTOM_MODEL_RUNTIME_PROBE.md) | 早期Windows只读探针目的、第一轮结果与实验入口 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_RUNTIME_PROBE_ZHUANGFANGYI.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [庄方宜定向运行时探测](runtime-probes/CUSTOM_MODEL_RUNTIME_PROBE_ZHUANGFANGYI.md) | 庄方宜定向采集结果及原操作流程，后转连续探针 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_RUNTIME_SWEEP.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [角色详情页连续采集](runtime-probes/CUSTOM_MODEL_RUNTIME_SWEEP.md) | 全角色连续采集、持续模式升级与catalog来源标签 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BetterEndfield.CustomModel 运行时验证：Endministrator (F)](endmin-casualwear/CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN.md) | 管理员源布局/组件/骨骼设计输入及catalog构建证据 | 来源/证据 | 1.5.3 |

## CUSTOM_MODEL_V25_BINDING_IMPLEMENTATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEMPC25：限定四项兼容能力](capability-v25/CUSTOM_MODEL_V25_BINDING_IMPLEMENTATION.md) | BEMPC25四项受限能力、profile和旧wire布局实现记录 | 历史过程 | 1.5.3 |

## CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [庄方宜首个 BEMPC25 测试包](zhuangfangyi/CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION.md) | 庄方宜默认测试包、数据/材质取舍及实机边界 | 来源/证据 | 1.5.3 |

## GENERIC_MODEL_MATCHING_DESIGN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [通用模型定位与身份验证方案（2026-10-03）](matching-identity/GENERIC_MODEL_MATCHING_DESIGN.md) | 通用身份链、真实Mesh/Original定位合同及覆盖研究 | 研究/提案 | 1.5.3 |

## HOT_SWITCH_LEGACY_PATH_RESEARCH.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [热切换与 legacy 实例路径调研（2026-10-03）](hot-switch/HOT_SWITCH_LEGACY_PATH_RESEARCH.md) | 借鉴旧实例路径的目标重绑、donor复用和回收方案 | 研究/提案 | 1.5.3 |

## MODEL_CAMERA_IMPLEMENTATION.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [通用模型匹配、异步构建与第一人称实现（2026-10-03）](model-camera-integration/MODEL_CAMERA_IMPLEMENTATION.md) | 模型匹配、分帧、缓存、第一人称的阶段集成交付；部分已漂移 | 阶段实施记录 | 1.5.3 |

## MODEL_HOT_SWITCH_REVIEW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM 包切换与下次资源加载生效评估（2026-10-01）](hot-switch/MODEL_HOT_SWITCH_REVIEW.md) | 下次交付换包、pristine donor及缓存限制的静态方案 | 研究/提案 | 1.5.3 |

## MODEL_MATCHING_LOADING_DECISIONS.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [本轮方案结论（2026-10-03）](model-camera-integration/MODEL_MATCHING_LOADING_DECISIONS.md) | 研究阶段通用匹配、四角色、UI复用和第一人称优先级决策 | 研究/提案 | 1.5.3 |

## MODEL_MEMORY_REVIEW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [BEM 加载内存检查（2026-10-01）](loading-memory/MODEL_MEMORY_REVIEW.md) | 加载内存代码/包分析、重复构造和后续测量边界 | 研究/提案 | 1.5.3 |

## MODEL_UPLOAD_PEAK_REVIEW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [模型首次加载的瞬时 GPU 压力（2026-10-03）](loading-memory/MODEL_UPLOAD_PEAK_REVIEW.md) | 首次GPU峰值调查、world/UI重复上传修正与统计能力 | 阶段实施记录 | 1.5.3 |

## NATIVE_MODEL_LOADING_RESEARCH.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [游戏原生模型加载与 BEM 分帧方案（2026-10-03）](loading-memory/NATIVE_MODEL_LOADING_RESEARCH.md) | 原生异步加载静态链路、BEM分帧和严格延迟交付建议 | 研究/提案 | 1.5.3 |

## PARALLEL_MODEL_CAMERA_REVIEW.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [双端模型与安卓相机改动记录（2026-10-03）](model-camera-integration/PARALLEL_MODEL_CAMERA_REVIEW.md) | 多项局部修正与研究范围的整合集成检查点 | 阶段实施记录 | 1.5.3 |

## TEAM_MODEL_LOADING_DESIGN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [最多四角色的全局模型加载队列设计（2026-10-03）](loading-memory/TEAM_MODEL_LOADING_DESIGN.md) | 四角色共同worker/帧预算、Ready预热和延迟策略提案 | 研究/提案 | 1.5.3 |

## UI_MODEL_CACHE_DESIGN.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [UI 模型成品资产缓存设计（2026-10-03，任务 C）](ui-model-cache/UI_MODEL_CACHE_DESIGN.md) | UI成品池身份/所有权/TTL和兼容复用方案，当前空闲池已撤 | 研究/提案 | 1.5.3 |

## WORLD_MODEL_BINDING.md

| 文档 | 用途 | 状态 | 游戏版本 |
| --- | --- | --- | --- |
| [世界模型绑定核对（2026-10-03）](matching-identity/WORLD_MODEL_BINDING.md) | 世界Mesh/Renderer名、fur后缀与局部资源绑定修正 | 阶段实施记录 | 1.5.3 |
