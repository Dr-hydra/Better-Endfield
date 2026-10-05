# 庄方宜离线转换资料

64/64 renderer 引用完整；公共高光已由修复后的读取器解析。

## 合并骨架映射

| 组 | 原生 LOD0 来源 | 骨骼数 |
| --- | --- | ---: |
| 1 | S_actor_zhuangfy_hair_01_lod0 | 53 |
| 6 | S_actor_zhuangfy_hair_01_lod0 | 52 |
| 3 | S_actor_zhuangfy_body_01_lod0 | 61 |
| 5 | S_actor_zhuangfy_body_01_lod0 | 49 |
| 0 | S_actor_zhuangfy_cloth_01_lod0 | 207 |
| 7 | S_actor_zhuangfy_cloth_01_lod0 | 207 |
| 2 | S_actor_zhuangfy_cloth_02_lod0 | 22 |
| 4 | S_actor_zhuangfy_cloth_02_lod0 | 22 |

八组使用原生 Transform 身份及 bindpose 核对，世界/UI 四个组件身份一致。

## 已完成并部署测试包

- world/UI 八个 renderer 的完整运行时顶点声明已核实，Normal 为 Float32×1。
- 十个全局覆盖对应八份唯一原生 DDS，mip 字节/格式一致，保留原生完整 mip 链。
- 显式贴图绑定使用已审阅材质配方；身体首段按颜色/法线语义适配原生属性。
- 头发保留两个材质槽，重复 draw 共用几何/骨架。

最终 `zhuangfangyi-verified-profile.json` 为 verified=true、render_verified=false。
`zhuangfangyi-default-v25.bempoc` 为 147,341,124 字节，四组件、七条纹理记录；
生产包解析及三角色配置路由通过，测试目录和实际 catalog 已部署。
准备阶段草稿不作为当前放行状态。用户反馈“看上去是正常的”，默认包实机外观检查通过。
具体动作/技能覆盖未单独确认；生成时 render_verified=false 保留，人工结论以验证记录为准。
完整记录见 `docs/CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION_20260917.md`。

读取器还保留一个 initial 包尾段错误，64 个 renderer 的所需引用均已解析。
