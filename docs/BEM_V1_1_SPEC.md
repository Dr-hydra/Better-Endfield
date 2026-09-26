# BEM 1.1 组合外观格式

2026-09-23。扩展名仍为 `.bem`。本规范只增加有限、静态的选项组合；容器头、payload 目录、顶点流、骨骼 palette、原生材质身份和纹理格式沿用 [BEM 1.0 规范](BEM_V1_SPEC.md)。Windows 与 Android 读取器接受 1.0 和 1.1；旧读取器拒绝 1.1。

## 包头与共用字段

40 字节包头中 `major=1, minor=1`，`schema=1`，`required_capabilities` 必含 `composable-options`，不可同时含 `fixed-appearances`。`target`、`meshes`、`textures`、payload 目录仍按 1.0 的身份和预算校验。1.1 使用 `option_groups` 与 `component_rules`，不使用 1.0 的 `default_appearance_id` 或 `appearances`。一个包仍只对应一个角色。

顶层示意（省略与 1.0 相同的身份、target、顶点流和纹理字段）：

```json
{
  "schema": 1,
  "package_id": "creator.character.outfits",
  "name": "组合外观", "author": "作者", "version": "1.0",
  "required_capabilities": ["native-materials", "palette-u8", "indices-u32", "composable-options"],
  "target": {"...": "与 BEM 1.0 相同"},
  "option_groups": [
    {"id": "outfit", "name": "服装", "default": "a",
     "choices": [{"id": "a", "name": "外观 A"}, {"id": "b", "name": "外观 B"}]},
    {"id": "part21", "name": "局部部件", "default": "on",
     "available_when": {"eq": ["outfit", "b"]},
     "choices": [{"id": "on", "name": "显示"}, {"id": "off", "name": "隐藏"}]}
  ],
  "component_rules": [
    {"target": 0, "candidates": [
      {"when": {"eq": ["outfit", "a"]}, "operation": "keep"},
      {"when": {"eq": ["outfit", "b"]}, "operation": "replace", "mesh": 0}
    ]}
  ],
  "meshes": [{"...": "与 1.0 相同的 vertex_count/index_size/streams/attributes/bones",
    "draws": [
      {"when": {"eq": ["part21", "on"]}, "indices": 7, "count": 300,
       "material_component": 0, "material_slot": 0,
       "material_name": "原生材质名", "textures": [0]}
    ]}],
  "textures": []
}
```

示意中的 `"..."` 不是合法字段；实际项目须填写完整的 1.0 共用字段。`component_rules` 必须按 `target` 0..N-1 顺序覆盖目标全部组件。每个候选操作为 `keep`、`hide` 或 `replace`；替换必须指向 `meshes` 中一项。任意可达选择下，每个组件恰好选中一个操作。`replace` 的 mesh 至少选中一条 draw。

`keep` 若保留原生网格却替换其材质贴图，可在该候选中加入 `material_overrides`，并在 `required_capabilities` 声明 `keep-material-textures`。示例：`{"operation":"keep","material_overrides":[{"material_slot":0,"material_name":"原生材质名","textures":[0]}]}`。槽位按本目标组件的原生材质表定位；纹理仍引用顶层 `textures`，运行时在该材质中按 `original_name` 唯一匹配原始贴图槽，只克隆和修改材质，不替换几何。每个 keep 候选不得重复材质槽，每项至少一张纹理；`hide`/`replace` 不得带此字段。包含字段的包必须声明能力，旧读取器会拒绝而不会静默忽略。所选 keep 纹理计入 32 张绑定及 payload/常驻预算。

## 选项与条件

每组有稳定 `id`、显示 `name`、`default` 和 1..16 个互斥 `choices`。组数最多 64，不再限制所有 choice 的笛卡尔积；33 个独立二值组的 8,589,934,592 种保存赋值可以由条件规则直接表示。组和值 ID 均符合 1.0 的稳定 ID 字符集。显示名可改，更新同一包时应保留语义未变的 ID。

`available_when` 只可引用清单中更早的组。组不可用时，管理器隐藏它但保留用户保存的 choice；条件求值时它没有有效值，针对该组的 `eq` 为 false。重新可用时恢复保存的 choice。可选的 `selection_constraints` 是条件数组，所有条件必须成立，表示源 Mod 实际可达的组合；默认设置必须可达。不能用隐藏组来偷偷删掉用户的保存值。

条件只能是 `true`/`false`，或 `{"eq":["group","choice"]}`、`{"all":[条件,...]}`、`{"any":[条件,...]}`、`{"not":条件}`。条件树深度最多 16；`all/any` 每节点 1..32 项。源 INI 命令、任意表达式和 Shader 代码不会在包中执行。

## Draw 与按需读取

1.1 的 mesh 沿用三个顶点流、`vertex_count`、`index_size`、`attributes`、`bones`；**不再用 mesh 级 `indices` 和固定 `index_count`**。每条候选 draw 自带 `indices` payload ID 和 `count`，可带 `when`。该 payload 恰好保存 `count × index_size` 字节。需要两个材质 draw 使用相同几何时，可以引用同一索引 payload；打包器按字节相同去重。

读取器先只读包头、manifest 与目录，按当前设置选择组件和 draw，随后只读取所选 draw 的索引 payload、所选 mesh 的三个顶点流和所选 draw 引用的纹理 payload。未选内容专用 payload 不读取、不解压、不上传。所选 draw 按清单顺序拼为一条连续输出索引流，重新计算各 draw 的 `start`；允许共享/重复使用源索引，输出空间按实际拼接后的大小计。这样可表达同一部件内的贴图、材质和绘制段开关，而无需枚举完整外观组合。

容器最大 2 GiB、manifest 最大 4 MiB、目录至多 4096 项、单 payload 至多 512 MiB。BEM 1.1 单次选定状态的已解码 payload 缓存合计与重建后常驻资源分别最多 768 MiB；BEM 1.0 的对应上限仍为 512 MiB。最多 32 张选定纹理、每组件最多 256 条最终 draw、选定索引最多 16777216 个。1.1 的组件操作候选与 draw 候选总数最多 512。创作者工具用精确多值决策图证明所有可达赋值下操作唯一、替换 mesh 至少选中一条 draw，并求出最大 draw、索引、纹理绑定及两种内存预算；不会抽样代替校验。决策图最多 250000 个非终端节点，单次预算优化或计数最多 250000 个记忆状态；超过时明确拒绝。游戏加载时再次检查当前选择及实际 payload 内容。

Python 创作者接口 `bem_v11.analyze_selection_space(manifest, payloads=None)` 在结构校验后返回精确 `saved_assignments` 与 `effective_selections`，后者将不可用组的保存值归一化，因此代表不同的有效选择。结果还包括 `max_selected_draws`、`max_selected_indices`、`max_selected_textures`、`symbolic_nodes`；传入 payload 字节时增加 `max_selected_decoded_bytes` 与 `max_resident_bytes`。`bem_tool.py validate` 的 JSON 报告在 `selection_space` 字段提供同一结果。这些数量来自完整条件空间的精确推导，能够用于离线验收报告。

## 设置与热切换兼容

管理器按包和组保存 choice；Windows `runtime.ini` 使用 `options=outfit:a&part21:on`，Android 安装索引保存同一组 ID→choice ID，启动时传给共用原生读取器。缺省组采用包内 `default`。两个平台的运行配置上限为 1 MiB。1.0 的 `appearance=<id>` 保持不变。

包必须保留所有候选资源和条件规则，不能按安装时选择裁剪。对同一未修改的 `.bem`，A→B→A 要重建相同组件、索引、draw、材质及纹理引用，不重新导入或改写包。未来运行时可据此准备新状态、复用共有资源并原子替换；当前版本**只在下次启动应用选择，不提供热切换 UI、快捷键或运行时替换事务**。单次选择的 768 MiB 预算不是未来切换瞬间旧/新两态重叠的峰值承诺。

Android 将完整包复制到游戏私有磁盘目录以保留将来可选内容；复制磁盘文件与把全部模型资源读入运行内存是两回事。模型加载只读当前选择所需 payload。

Shader、RabbitFX 特效、时间动画、连续滑块和任意源脚本不在此静态格式的支持范围。创作者应清理源 INI 中的广告/杂讯，并以可验证的原生资源身份、材质/贴图映射与可达状态为准；无法表达的效果须在报告中标明，不可默默丢弃后称完全等价。
