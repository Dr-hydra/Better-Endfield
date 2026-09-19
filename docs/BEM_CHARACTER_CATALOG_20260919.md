# 角色资料入库与 EFMI 原资源对应

2026-09-20 更新：下文“源资源对应待补齐”是 1.1.2 阶段记录。1.1.3 已从原游戏数据补齐 32 角色的 360 个完整单子网格入口及 1,181 项纹理身份；详见 [EFMI 身份补全](BEM_EFMI_IDENTITIES_20260920.md)。10 个多子网格入口仍需专门规则，不作为缺采样处理。

## 本次入库

BEM Tools 1.1.2。游戏 manifest：`2954fa80-23c1-1579-2b22-4ecfd6d70418`，平台 Windows x64。

两轮原生运行时观察与当前 VFS 覆盖层的离线引用合并，产出 `tools/CustomModel/catalog/` 中 32 个角色、370 个 LOD0 部件。范围为主体 `S_actor_` Mesh；不包含嵌套武器、`vfxpart`、低 LOD 与阴影部件。

- 433 份部件/资源根契约有直接运行时观测；其余 307 份由离线等价证明复用。不是 32 角色双路直接采集。
- 等价条件：同一个 Mesh 对象、相同相对骨骼路径与顺序、相同材质对象与顺序、完整离线引用。直接观测还逐项核对索引数、顶点数、骨骼、材质和 bindpose；两路都有观察时检查顶点声明及 stride 一致。
- 当前离线后端保存序列化矩阵顺序，与运行时 `Mesh.get_bindposes` 返回顺序互为转置。导入器明确转换顺序后逐数值比较，容差仍为 `1e-5`；不会尝试多个方向择一放行。
- 每个部件保存直接/复用来源、run、资源根和原生 Mesh ID。`coverage.json` 汇总覆盖和缺项，不作为角色匹配表。
- 62 个部件虽然资料完整，但其顶点布局或多个 submesh 尚不在当前 ComponentN 自动转换实现范围内；选中替换会报告 `NATIVE_LAYOUT_UNSUPPORTED`。不根据 stride 猜顶点语义。
- 保留女管理员已有 EFMI 原资源对应和原有部件 ID；C10 已获得本轮直接布局观测。佩丽卡仅更新原生资料，没有读取其源 Mod 或引入专用规则。

两处不能作为 world/UI 共用部件入库：

| 角色 | 部件 | 差异 |
| --- | --- | --- |
| 卡缪 | `S_actor_camille_skill_01_lod0` | 同 Mesh，但世界模型四个技能材质槽，UI 使用另一个材质 |
| 提弗洛斯 | `S_actor_typhoea_cloth_01_lod0` | 同 Mesh/材质，但一处裙骨名称/路径对应不同（L/R） |

这些部件明确记录在 `excluded_components`，其余部件可独立复用。没有因页面通常共用就忽略差异。

## EFMI 哪些资料可跨 Mod 复用

检查官方 EFMI-Tools 提取、元数据和导出代码后，可以区分三层：

| 资料 | 可复用范围 |
| --- | --- |
| 原始 IB/VB/纹理 hash、原始几何范围、原骨骼分组、原纹理使用关系 | 同角色、相同原游戏资源/LOD、相同 hash 规则下可跨 Mod 复用；游戏更新需复核 |
| `ComponentN` 编号 | 提取对象内部编号，按部件位置等条件排序；不能当成全角色或所有导出版本的固定部位号 |
| 作者新增材质、RabbitFX、自定义 Shader、动态切换、重排后的骨架 | 属于来源逻辑；不能用原生角色资料自动推断其效果 |

EFMI Tools 的 Object Sources 输出包含 `Metadata.json`、原始缓冲及 `TextureUsage.json` 等。元数据记录原始 IB/VB hash、范围、`vg_map`、顶点格式及 LOD 信息；纹理输出也保留原资源 hash。该数据来自干净游戏的 Frame Dump，可以供多个作品复用，并非每个作者必须重新发明一份转换规则。

本次检查的官方仓库和 Release **未找到可直接下载接入的全角色 Object Sources / hash→Unity Mesh、骨骼、材质对应库**。提供的是提取工具、模板和运行框架；官方指南要求用户先生成 Frame Dump，再提取 Object Sources。未扩大此结论到所有第三方社区资源。

官方来源（本次检查版本）：

- [EFMI Tools 提取流程](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/guides/modder_guide.md)
- [Object Sources 元数据结构](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/migoto_io/object_extractor/migoto_object/metadata_format.py)
- [纹理使用关系导出](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/migoto_io/object_extractor/migoto_object/migoto_object_exporter.py)
- [组件排序和资源读取](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/migoto_io/object_extractor/migoto_object/migoto_object_builder.py)
- [EFMI Tools v0.6.5](https://github.com/SpectrumQT/EFMI-Tools/releases/tag/v0.6.5) 与 [EFMI Package v1.4.6](https://github.com/SpectrumQT/EFMI-Package/releases/tag/v1.4.6)。Package 树检查版本 `3cc2f4b89e791983484ac00ad5a3e15779957350`。

## 剩余接入边界

32 角色原生资料完成入库，不等于 32 角色源 Mod 全部自动转换。除女管理员已有核实映射外，其余角色仍标记 `source-resource-identities-pending`。Hash/LOD、自定义材质的既有专门转换路线不因此自动变成通用路线。

下一步可将官方工具生成的干净 Object Sources 与这里的原生资料建立有证据的对应，再保存成按角色/游戏版本复用的资源映射。需要核对原始几何、骨骼分组和纹理绑定；单凭组件编号、相似名字或索引数不能批准映射。正常标准包随后共用映射；自定义逻辑仍需独立处理。本次未实现 Object Sources 导入，也未要求用户重采全部角色。

## 后续游戏更新的开发入口

1. 用新 manifest 建立 `developer-tools/Probe.ps1` 采集任务，保留原始 run。
2. `prepare_sweep_offline.py` 对汇总中的角色联合提取依赖闭包，再逐角色离线解析；manifest 必须与采集一致。
3. `import_runtime_catalog.py --databases <离线目录> --captures <native-probe目录> --runs <run...> --names ui/BetterEndfield.UI/Assets/model/character-names.json --output <catalog目录>` 先完整验证再发布资料。已有源映射的契约改变会停止，要求维护者复核。
4. 针对性测试 `test_import_runtime_catalog` 与 `test_component_auto`，构建工具链。无需把离线原始文件、游戏像素或运行日志发给玩家。

本次标准资料 JSON 合计约 1.99 MB（压缩前），大块离线数据库仍留在研究产物目录。
