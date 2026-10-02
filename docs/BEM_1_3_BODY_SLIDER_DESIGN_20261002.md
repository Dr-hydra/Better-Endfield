# BEM 1.3 体型滑条可行性与格式提案

2026-10-02。本文是拟议设计，**仓库当前正式格式仍为 BEM 1.0 / 1.1 / 1.2，没有已经实现的 BEM 1.3**。本轮只读代码、规范及已有角色/Blender 报告；没有修改运行时、启动游戏实验或重跑 Blender。

## 结论与建议路线

可以引入滑条，但必须由包作者提供可形变的几何或经过审阅的骨骼控制定义。已有的骨骼名称、蒙皮权重与资源身份只能帮助定位和绑定，不能凭空生成可靠的胖瘦、肩宽、胸围或衣服形态。

推荐把正式 BEM 1.3 的核心设计为“**有默认中性值的参数 + 同拓扑形态增量**”。首个运行后端在参数确定时用包内 CPU 数据合成新网格，随后走现有资源交付/热切换事务。无需 Android 原网格 GPU 读回；正常游玩保持现有蒙皮，避免新增每帧形变计算。

后续 [EFMI 实际执行路径调研](EFMI_BODY_SLIDER_RESEARCH_20261002.md) 已确认官方连续形态本身只改位置、沿用原 TBN，因此方向 codec **不是复现 EFMI 现有能力的前置门槛**。推荐优先做同款位置 morph。终末地使用打包的方向通道，不能直接套普通 Unity BlendShape 或 RecalculateNormals 并承诺大幅塑形光照正确；方向重建是后续可选质量增强。作者提供完整预烘焙档位、以 BEM 1.2 choice 做分档滑条仍是备选方案。

## 当前实现依据

| 现状 | 代码/资料 | 对滑条的意义 |
| --- | --- | --- |
| 连续滑块明确在 1.1 静态格式支持范围之外；1.2 扩展没有改变这一点 | [BEM_V1_1_SPEC.md](BEM_V1_1_SPEC.md)、[BEM_V1_2_SPEC.md](BEM_V1_2_SPEC.md) | 不能把现有 choice 当作已经支持连续参数 |
| 原生读取器头部只接受 `minor <= 2`，Python 只接受 0/1/2 | `native/modules/custom_model/bem.cpp:93`、`tools/CustomModel/bem_v1.py:216` | 新格式需显式升级读取器；开发者跳过校验开关也不能使旧读取器获得解码能力 |
| Palette 从原生 donor 复制实际 Transform / bindpose，要求同 mesh space | `native/modules/custom_model/module.cpp:1691` 起的 `PreparePalette` | 名称别名只是身份匹配，不提供新骨骼、比例或新 bindpose |
| EFMI 转换器只透传原生三流，不解码重排几何；ShapeKeys 源被归为需人工适配 | `tools/CustomModel/convert_efmi_poc.py:4`、`source_classification.py:38` | EFMI 中任意 Shader 驱动的体型控制不能自动搬进新格式 |
| 不重算法线/切线：normal、encoded tangent、bitangent sign 由游戏 shader 解包 | `native/modules/custom_model/module.cpp:1336` | 形变位置和正确形变表面方向是两件事，须核对实际布局编码 |
| Android 编译共享 custom model 实现，world 路由用 UI LOD0 donor 重绑 world LOD1 的骨架 | `android/app/src/main/cpp/CMakeLists.txt:121`、`world_resource_adapter.inc:1` | 共用形态数据可复用，仍不能按 world LOD1 顶点序号修改 UI LOD0 数据 |
| 噗切娜紧凑 catalog 有资源、骨骼、布局和顶点计数，没有体型 target | `tools/CustomModel/catalog/chr_0038_purrche.json` | catalog 不能直接变成自动捏体工具 |

历史 `tmp_analysis/aglina-fbx-validation-v2/` 的 Blender 报告包含 10 个网格、419 根骨骼及动画转换验证。报告验证的是关节/动画转换，并没有验证体型形态、原版材质和游戏实时物理。本轮未打开 `.blend` 检查内部形态键，不把这些场景当作已具备体型导出的证据。

## 四种实现方式的取舍

| 方式 | 能做到什么 | 主要限制 | 建议 |
| --- | --- | --- | --- |
| 骨骼局部缩放/偏移 | 按指定骨骼及蒙皮影响改变局部大小；共享骨骼的衣服常可一起动 | 影响子节点、道具和共享 renderer；动画、IK、布料、约束可能覆盖或放大改动；非均匀缩放可能引入剪切 | 后续独立能力，先限定作者审阅的控制集合，不作为首版通用捏体方式 |
| 包内形态增量，参数确定后烘焙 CPU 顶点流 | 连续调整作者提供的同拓扑形态；沿用骨骼动画；无新增每帧 morph 后端 | 作者须提供衣服/附件 target；大比例改动可能与原关节中心、IK 不匹配；方向编码需补齐 | 推荐 BEM 1.3 主路线 |
| Unity 原生 BlendShape | 创建形态 frame，再用 renderer weight 实时预览 | 当前 custom model 没有 AddBlendShapeFrame 导出/上传链；普通 normal/tangent API 与打包方向通道不等价；内存与实际 backend 成本尚未实测 | 可在后续验证布局和双端后端后加入，不能因相机 MMD 已调用 SetBlendShapeWeight 就认为已具备支持 |
| 预生成完整形态 / 多网格插值 | 完整档位可直接用现有替换事务；同拓扑的网格才可做顶点对应插值 | 档位数据体积较大；不同拓扑、权重、UV、palette 或材质段无法直接插值；多个独立档位组合会膨胀 | 分档滑条适合作为近期原型；任意两个模型之间不提供自动插值 |

Unity 的 AddBlendShapeFrame 接收与网格顶点数对应的增量，SetBlendShapeWeight 面向已存在的同拓扑形态。这证明普通 Unity API 的表达能力，不证明终末地当前布局或 Android 实际渲染后端能直接接受本提案。[Unity AddBlendShapeFrame](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Mesh.AddBlendShapeFrame.html)、[Unity SetBlendShapeWeight](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/SkinnedMeshRenderer.SetBlendShapeWeight.html)。

## 拟议最小格式

继续沿用 `.bem`、40 字节容器头、`major=1`、`schema=1`、payload 目录和现有有限外观规则。使用新字段/能力时写 `minor=3`；无新能力的导出仍按现有最低所需版本写出。新增能力名称暂定，尚未注册：

- `body-parameters`：参数声明、持久化和 UI 解释。
- `mesh-position-deltas`：在现有 replacement mesh 上合成位置增量。
- 正式方向重建另以 `packed-frame-morphs` 声明，必须对应经过验证的布局 codec；不能假定所有原生布局通用。

下面只是字段草案，不是当前写入器可接受的包。`parameters` 是 UInt32 tick 值，`mesh_deformations` 的 `mesh` 指向现有 `meshes` 中项；位置增量是绑定姿态下该网格的本地坐标，而不是当前动画姿态或世界坐标。

```json
{
  "parameters": [
    {
      "id": "waist",
      "name": "腰围",
      "min": 0,
      "max": 1000,
      "neutral": 500,
      "default": 500,
      "step": 1,
      "available_when": true
    }
  ],
  "mesh_deformations": [
    {
      "mesh": 0,
      "parameter": "waist",
      "shading": "preserve-base",
      "frames": [
        { "value": 0, "encoding": "sparse-position-f32", "count": 1500, "payload": 12 },
        { "value": 500, "neutral": true },
        { "value": 1000, "encoding": "sparse-position-f32", "count": 1600, "payload": 13 }
      ]
    }
  ]
}
```

`preserve-base` 明确表示只改位置、保留原方向，与已核对的 EFMI runtime morph 语义一致；它不是大幅形变光照正确性的保证。如需增强表面方向质量，应另用已核对的方向重建模式。不能对已经打包的 normal/tangent 数值或字节直接线性插值，不能用通用 RecalculateNormals 代替验证。

首版建议采用 normalized 0..1000 tick，JSON 不引入任意浮点数/负数，继续遵循当前 manifest 全树 UInt32 数值约定。实际增量中的负数和浮点数放在二进制 payload。参数名显示为作者定义，不把“腰围 600”解释成真实厘米。

### 插值与合成规则

每条 mesh/parameter 曲线必须在 `min`、`neutral`、`max` 有定义，关键帧按 value 严格递增；neutral 帧的增量严格为零且不带 payload。帧间做分段线性插值，多个参数的位移在不可变 base 上相加：

`position(v) = base_position(v) + Σ interpolated_delta(parameter, v)`。

每次从 base 重建，不能在上一次结果上累加；回到 neutral 时复用原始字节，避免 A→B→A 累积误差。初版不引入任意表达式、参数乘积驱动、时间动画或 source INI 执行。`available_when` 只引用现有有限 option；不可用参数的有效值回 neutral，保存值仍保留。滑条值不参与现有 DD 的有限 geometry/material 条件，避免未经定义地把连续值塞进条件空间证明。

`sparse-position-f32` 草案每条记录为 little-endian `uint32 vertex_index + float32 dx/dy/dz`，16 字节。索引严格递增、不重复且小于对应 `vertex_count`；缺省顶点增量为零。dense 形式可另定为 `vertex_count × float3`。浮点增量须有限，payload 长度须与 encoding/count 相符。位置属性按实际 `attributes` 找到并读写，仅支持已实现的 position 格式；其余流、权重、palette、draw、UV 不变。

正式方向模式宜导出 canonical normal/tangent target，在可信 codec 下解包 base、插值、归一化并重新编码；tangent handedness 的处理需明确，不能混入默认线性插值。若 codec 尚未验证，就明确拒绝这一模式或只导出已烘焙的完整档位。不同拓扑 target 不能因顶点总数相等就放行。

## 作者数据和创作者工具

体型轴须由作者制作。身体、衣服、贴身配件应为同一轴提供相容 target；角色权重只负责动画绑定，不会自动把裸身形态传播到另一件衣服。工具可以提示漏掉的服装，不能凭包名或骨骼名判断某件衣服已经完成塑形。

在现有 `.bemproj.json` 导出项目流程上增加参数面板、绑定的 mesh/shape 轴和报告。现有任务文件负责 source/output/参数；形态映射应写在其引用的可编辑项目，而不是让导出任务携带运行时代码。

可接受两类作者输入：Blender 的 Basis + shape keys，或已有原生流对应的同拓扑 target buffers。Blender 导出须先补齐 BEM geometry/layout encoder；当前任务 GUI 不是已经存在的 Blender 通用导出器。每次导出必须保持稳定的源顶点到导出顶点映射，处理 UV/法线拆点，统一 mesh-local space 与单位，锁定 triangulation、拓扑、skin weights、palette、材质段和切线编码。不同形态不能各自重新排序或独立应用改变拓扑的 modifier 后按索引相减。

工具应验证默认值中性、单轴端点、多个轴极值组合、重要动画姿态下关节/衣服表现，并报告哪些网格支持哪些轴。位置范围和解码内存可通过每顶点区间计算保守界，不能仅抽样便宣称全部连续值的极值已证明。作者的艺术/动画质量仍需实看；极端参数可能穿模、拉伸或与脚底 IK 脱节。

若完整档位已在 EFMI/原生包里存在，先复用 BEM 1.2 choice + mesh candidate，UI 可以做 11 或 21 档的滑条。它的输出是离散档位，不把档位切换描述为连续顶点形变；也不盲目枚举多个体型轴的全部笛卡尔积。

## 双端状态、热切换和第一人称关联

Windows/Android 使用相同参数 ID 和整数 tick；按 `package_id + character_id + parameter_id` 保存。参数不可用时保留保存值、有效值回 neutral；删除参数、范围变化或语义更新要给出明确迁移策略。旧包没有参数时行为不变，新读取器继续读取旧版本。旧读取器须明确报告版本/能力不支持，不能静默忽略体型控制。

运行配置可增设规范化的 `parameters=waist:620&shoulder:480`，按参数 ID 排序。参数有效值必须进入 registry `selection_key`、payload cache key 和 completed-resource key；否则当前路径会把改了参数的包视为同一次加载。安装器/Java 索引、Windows BemPackageService/report、两个 UI 都须携带参数元数据，而不只更新共享 C++ 读取器。

首版在滑条释放/点击应用时只接受最终参数，合并频繁输入。热切换关闭时按现有启动流程应用；打开时在下一次正常资源交付应用，新 mesh 准备成功后提交、失败保留旧状态。UI 应显示“待资源刷新”，不承诺拖动过程中立即刷新场上角色。下一阶段才考虑对当前实例做实时预览。

缓存基于 base mesh、有限 option、参数有效值和包世代；只留最近/当前所需态，不缓存 1001 个 tick 对应的完整 mesh。Android 的 UI donor 与 world 重绑定共用相同已合成 CPU 状态，按各自现有空间与 palette 核验，不从 GPU 读旧网格再塑形。更新后的 mesh/localBounds 须覆盖形态和动画活动范围；现有 RecalculateBounds 只更新 mesh 数据，不等于已证明 SkinnedMeshRenderer 的动态包围盒正确。

体型参数不改变第一人称启停或视角限制，不直接修改相机模块保存的状态。资源/mesh 世代变化须使第一人称的 renderer 隐藏绑定和相关缓存失效并重建，切人时不得继承上个实例的指针。只改顶点不会自动移动 Head 骨骼/相机锚点；若作者改变头部表面，需按现有锚点重新验收近裁剪。头发语义、第一人称专用几何可作为独立可选能力研究，不列为最小体型滑条的必需字段。

## GPU/内存与加载代价

CPU 烘焙后保持相同顶点/索引数与原生材质，稳定画面不增加形態 target 的每帧处理；代价集中在参数提交/资源交付的解码、合成和上传。目标数据可按选中的 mesh/轴及相邻两帧按需读取，顺序处理轴，避免把所有档位和所有方向数据同时展开。

示例估算均只计顶点形态数据，不是实测显存：100,000 顶点、8 轴、每轴 2 个非中性端点，dense position 增量约 18.31 MiB；20% 顶点受影响的 sparse position 约 4.88 MiB。若未来每端点再存 canonical normal/tangent，dense P/N/T 约 54.93 MiB；sparse P/N/T（含 uint32 索引）约 12.21 MiB。实际目录压缩、不同轴影响范围、布局 stride 及 allocator 峰值另计。

新旧 mesh 事务重叠仍可能短时增加 GPU/CPU 占用；不能把 BEM 1.2 的 1536 MiB 校验上限当成 Android 实际可承受的内存。滑条仅几何变化时尽量共用材质/贴图，区分 base 常驻、target 解码、合成输出、临时缓冲和旧态/新态重叠预算；把全部形态常驻 GPU 的 BlendShape 后端不作为首版默认。

骨骼控制的额外几何数据最小，但跨动画/布料/IK 的变换合成和恢复成本较高。未来若加入，必须从当前动画结果施加增量，并在该角色退出/换包/关闭功能时清除控制；不能每帧对上一次 localScale 继续乘，也不能在动画执行后恢复成进入功能前某一帧的旧姿态。多人/MMD 实例须按各自世代管理，禁止把同角色资源的一根 Transform 当成所有实例的控制对象。

## 实施门槛与验收

1. 先选一个角色和一套作者可修改的同拓扑身体+衣服，确认 2–3 个小幅轴；不要一开始宣称全角色通用捏体。
2. 验证具体 layout 的 position 编辑，以 preserve-base 复现 EFMI 同款位置 morph；打包方向 codec 单独作为质量增强验证，完整预烘焙档位作为备选。
3. Python 作者校验、共用原生解析/合成、Android 安装转换保留新 payload、双端参数持久化/UI/cache key 一起完成，再宣布支持 BEM 1.3。
4. 验证 neutral 字节复原、反复 A→B→A、隐藏 option 恢复参数、换包/切人/详情与 world 切换、热切换失败回退、第一人称可见性、影子、动画及服装极值表现。
5. 分别测 PC 与 Android 稳态及切换峰值、提交延迟；再决定控制数/关键帧数/target 内存的正式上限。可先建议最多 16 个轴、每轴 8 关键帧作工具原型约束，尚不作为已经确定的格式标准。

本轮没有实现格式或滑条，没有生成新包，也没有改变现有 BEM 1.2 的行为。经后续 EFMI 源码调研修正，建议审阅通过后优先安排位置 morph 的小范围原型，方向 codec 另行增强，然后定版连续形态能力。
