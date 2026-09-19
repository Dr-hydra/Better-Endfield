# 逐绘制段贴图绑定：样包与官方实现交叉核对

本次为通用转换规则研究，不是已实现逐段自动转换的声明。没有读取佩丽卡源包，没有改变测试安装的放行条件。

后续样本规则和 RabbitFX 分层见 [RabbitFX 兼容性与自动化边界](BEM_RABBITFX_COMPATIBILITY_20260920.md)。逐绘制段能力必须以机制样本验证后再放行，单个包不会直接改变全局规则。

## 样本

| 来源 | 入口/绘制流程 | 直接 ps-t 写入 | 附加限制 |
| --- | --- | --- | --- |
| 女管理员 casualwear | ALPHA-12 风格 callback/实例分发、ComponentN | 未发现；按原资源 hash 覆盖 | 当前已支持的自动路线 |
| 莱万汀 Cheshire slot15 | 标注 ALPHA-4，直接 run 绘制列表，drawindexedinstanced、lod_detected | 5 个绘制列表，共 14 条写入 | 旧入口模板；部分原 hash 覆盖段被注释，改为直接指定槽 |
| 庄方宜 061fe0 | Hash/LOD、callback、合并骨架 | 4 个绘制列表（含 LOD1），多个段改变贴图状态 | 骨架索引对应；现有专门配方含人工判断，不能原样推广 |
| 洁尔佩塔 12 | Hash/LOD、实例绘制、额外 panel INI | 主模型和面板均存在 | 自定义 Shader、常量缓冲/骨骼重定向、槽位备份恢复；不能归类为单纯换贴图 |

莱万汀并不是每条 draw 都改变材质：例如 Component0 的 cloth/gloves 两段共享同一组四个槽写入，Component8 四段也共享一组状态。通用解析器应跟踪实际状态和作用范围，不按 draw 数量推断材质数量。

## 官方规定的范围

当前 EFMI-Tools 模板在绘制前调用 `CommandList\EFMIv1\OverrideTextures`，EFMI API 检查 ps-t0..22，根据被绑定资源的 hash 触发 TextureOverride。这是按资源身份覆盖，不是把固定编号定义成固定材质属性。

历史 ALPHA-4 官方模板使用本地 `CommandListTriggerResourceOverrides`（检查 ps-t0..20）、直接 run 绘制列表、LOD 蒙皮流选择及 drawindexedinstanced。莱万汀主体框架接近这一代，但不是未经修改的原始模板：官方该历史版本入口使用 vb0_hash，样包入口另有 index count，且其 hash 已与当前原生 IB 数据对应；还增加了直接槽位赋值。不能只根据开头 ALPHA-4 注释选择解释方式。

当前和历史标准模板都没有提供全角色统一的 `ps-tN → _BaseMap/_BumpMap` 表。官方提取器的 `TextureUsage.json` 按组件、槽位记录纹理 hash、Shader hash、draw call 和格式信息，这本身也表明需要关联具体绘制上下文。

来源：

- [当前模板](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/templates/mod.ini.j2)
- [ALPHA-4 官方历史模板](https://github.com/SpectrumQT/EFMI-Tools/blob/926dd498136aaa438b3456da6a4b550533268f30/efmi-tools/templates/per_component.ini.j2)
- [EFMI 资源覆盖 API](https://github.com/SpectrumQT/EFMI-Package/blob/3cc2f4b89e791983484ac00ad5a3e15779957350/EFMI/Core/EFMI/API.ini)
- [TextureUsage 上下文记录](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/migoto_io/object_extractor/migoto_object/textures_descriptor.py)

## 为什么不能全局固定槽号

既有离线 Shader 证据中，庄方宜 Skin ForwardLit 使用 BaseMap=t13、BumpMap=t14；Cloth ForwardLit 使用 BaseMap=t19、MetallicGlossMap=t20、BumpMap=t21。莱万汀源声明还出现 t16 起的衣服贴图组和 t15 起的另一组。其文件名中的原资源标签只能作为调查线索，不能代替 Shader 绑定证据批准对应，尤其部分 TextureOverride 已注释。

现有 `cloth-shader-bindings.json`、`skin-shader-bindings.json` 已能解析游戏 Shader 的命名资源描述、变体及 D3D11 槽声明。它们尚未标准化为全角色可用的材质绑定目录；运行时骨骼/顶点采集也不自动包含 GPU ps-t 对应。

## 通用实现应拆分的工作

1. **版本化入口解释**：识别真实语句结构，分别支持已核实的旧式直接分发与新版 callback；LOD 锁定下解析高 LOD 蒙皮流，保留隐藏/原始绘制，拒绝未知实例或动态计算。
2. **逐段状态解析**：在每个 draw 保存当时的索引范围、缓冲、贴图资源和条件默认值；跨段继承、恢复与 helper 调用必须明确，不能把全包贴图列表套到所有段上。
3. **共享材质绑定资料**：按游戏快照、平台、原生 Shader/pass/变体与材质属性生成槽位对应，角色引用共用规则。绑定唯一且可核实时自动转换；不按包名、组件编号或 DDS 文件名猜。
4. **BEM 输出**：每段选择原生 donor 材质并绑定该段贴图；重复使用同一原生材质不同贴图时由既有材质克隆机制隔离，共享 payload。BEM 已有此表达能力，无需改包格式。
5. **边界**：静态、可核实的直接贴图绑定应走通用自动路线；自定义 Shader/形态变化/不明确变体仍报告具体限制。庄方宜旧配方中曾对不一致槽位做语义修正，那是专用判断，不可当通用规范。

因此当前对出现 ps-t 赋值的来源统一标记 requires_mapping 是实现尚未接入，不是这类来源原则上不能自动转换。先完成入口解释和共享槽位资料，再扩大放行，比为每个 Mod 增添一份配方更可复用。
