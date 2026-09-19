# Hash/LOD Mod 独立转换器与两份样本检查

> 本文保留最初样本检查。后续已实现限定范围的 BEMPC25 转换及运行时绑定，
> 最新状态见 [四项能力实现](CUSTOM_MODEL_V25_BINDING_IMPLEMENTATION_20260917.md)。
> 庄方宜默认包已通过用户实机外观检查。洁尔佩塔的默认几何与骨骼 Shader 已重新复核，
> 新结论见[洁尔佩塔默认状态复核](CUSTOM_MODEL_GILBERTA_REASSESSMENT_20260917.md)。
> 下文“未实现/未验证”保留为最初检查时点的记录，不代表当前整体状态。

## 范围和结论

按用户要求，为这类格式建立独立入口 `tools/CustomModel/convert_hash_lod.py`，
不修改原 `convert_efmi_poc.py` 的 ALPHA ComponentN 转换行为。

当前完成的是转换器前端：识别主 INI、读取资源声明、检查缓冲区和显式绘制范围、
记录条件绘制程序及转换缺口。**尚不能输出可运行的 BEM；两个样本均返回
`conversion_ready=false`。** 这不是压缩包损坏，而是需要实现新的骨架、绘制段及材质适配。
既有 source/material profile 不能直接套用到这些包。

原始输入只读，使用本机已有 7-Zip 解压到分支工作树的
`artifacts/converter-new-mods/`；不执行包里的 INI、HLSL 或脚本。
本轮不部署测试目录、不修改游戏或运行时、不计算产物哈希。

## 样本

| 输入 | 主配置 | LOD 入口 | DDS | 结构检查 |
| --- | --- | --- | --- | --- |
| `gilberta_12.rar` | `洁尔配塔.ini` | LOD0 × 4、LOD1 × 4 | 11 | 缓冲区、显式绘制范围和 DDS 检查未发现错误 |
| `061fe0a00d2904dd90ab25441e5c8f06.rar` | `庄方怡/庄方怡.ini` | LOD0 × 4、LOD1 × 4 | 13 | 同上 |

第二包按文件名和内容标识为庄方宜样本，原目录拼写为“庄方怡”；尚未用游戏原生资源身份核实角色映射。
“结构检查通过”不代表默认状态、Shader 效果、骨骼语义或实机显示通过。

### 洁尔佩塔

LOD0 数据（按首次绑定统计；绘制程序另行记录跨缓冲区切换）：

| 原绘制哈希 | 原索引数 | VB0/VB1/VB2 stride | 替换顶点数 | 替换索引数 | 最大索引 |
| --- | ---: | --- | ---: | ---: | ---: |
| `8c41c63c` | 167442 | 16/12/12 | 211076 | 986493 | 211075 |
| `959671a1` | 7080 | 16/12/12 | 2041 | 7080 | 2040 |
| `80402392` | 65130 | 16/12/12 | 21527 | 95724 | 21526 |
| `7233c71a` | 6123 | 16/12/12 | 136 | 588 | 135 |

- INI 有 33 个持久化开关默认值。开关控制索引范围内的绘制段，不能用整个部件可见/隐藏替代，也不能拼接所有分支。
- `TextureOverride_LOD0.7233c71a_6123_0` 在 `vs == 202 || vs == 203`
  分支切换到 `8c41c63c` 的 VB/IB，属于跨绘制入口复用。
  检查器已经按条件分支保留缓冲区绑定候选，避免用初始 588 个索引误报后续绘制越界。
- `CustomShader_RecordBones_*` / `CustomShader_RedirectCB1_*` 调用包内 HLSL。
  `record_bones_cs.hlsl` 从不同原始骨架偏移收集数据到 FakeT0；
  `redirect_cb1_cs.hlsl` 改写骨骼缓冲区偏移。`ResourceID_*` 提供 0、1000…7000 的存储偏移。
  这些是自定义 GPU 骨架存储地址，不能直接当作 Unity renderer 的骨骼编号。
- `CommandListJPTCrossIBBindSkin` 和 `...SkinAlt` 按 pass 切换 ps-t0/1/13/14；
  另依赖 `RabbitFx\SetTextures`。需要还原每段的原生材质语义，不能把 ps-t 槽号直接当作 Unity 属性 ID。
- 输入 IB 为 R32_UINT，LOD0 最大索引 211075。旧转换器按 16 位读取/写出 IB，不能直接缩窄；
  需支持 32 位端到端索引，或在确定默认绘制集合后进行有证据的顶点重映射/分片。
  表中统计包含所有导出顶点，尚不能断言默认配置也必然需要同样规模。

### 庄方宜

| 原绘制哈希 | 原索引数 | VB0/VB1/VB2 stride | 替换顶点数 | 替换索引数 | 最大索引 |
| --- | ---: | --- | ---: | ---: | ---: |
| `6af95e58` | 52332 | 16/12/16 | 14370 | 52332 | 14369 |
| `fe47dc61` | 7014 | 16/20/16 | 17465 | 92934 | 17464 |
| `0f9e1087` | 156318 | 16/20/16 | 1731 | 7362 | 1730 |
| `a8b9073c` | 13452 | 16/20/16 | 62127 | 283833 | 62126 |

- 使用 `EFMIv1\MergedSkeleton_AttachComponent`，声明 `$bones_count = 673`，
  `BLENDINDICES = R16G16B16A16_UINT`。旧转换器按 4 个字节读取骨骼索引，不能复用。
- `CommandListInitializeMergedSkeleton` 声明 8 个组件的骨架区间：
  offsets 为 0、207、260、282、343、365、414、466；
  counts 为 207、53、22、61、22、49、52、207。
  这些区间是合并骨架信息，不能用“减去当前组件 offset”替代实际骨骼映射，必须先核实是否跨组件加权。
- UV 流部分为 20 字节。不能因为旧样本使用 12 字节就截断，需要原生 Mesh 声明与通道语义。
- 只有一个声明的持久化开关 `$swapkey7=0`，但多个绘制段切换身体/衣物贴图；
  仍需要逐段材质处理。
- 输入同为 R32_UINT；这份包各 LOD0 IB 最大索引均小于 65536，几何索引可在验证后考虑无损缩窄。
  这不解决 16 位骨骼索引或合并骨架问题。

## 独立转换流程的待实现部分

1. **已实现：解析与检查。** 任意名称主 INI 选择（忽略备份、禁用配置和无模型入口的面板），
   直接入口/回调入口、LOD/hash/索引身份、VB 长度和数量一致性、R16/R32 IB、
   显式 draw 范围/base vertex、条件分支中的 VB/IB 重绑、DDS 解析、缺失文件检查。
   JSON 保留原绘制程序、命令依赖、默认值和骨架初始化文本。
2. **未实现：语义解析。** 默认开关求值、pass 条件、命令列表副作用、跨 IB 绘制消重，
   形成确定的几何/材质绘制段。当前仅检查显式绑定路径；不执行条件，也不声称完整模拟 EFMI。
3. **未实现：原生资源映射。** 从当前游戏验证两个角色世界/详情资源、原 Mesh、
   renderer、完整顶点声明、bones 和 bindposes；新增角色适配。当前运行时只注册管理员和佩丽卡。
4. **未实现：骨架转换。** EFMIv1 合并骨架和自定义 GPU 骨架分别适配，
   生成可验证的 Unity 骨骼顺序、权重索引与 bindpose 对应，不能只凭数字范围通过就视为正确。
5. **未实现：输出。** 决定绘制段如何对应 submesh/renderer，原生材质属性映射，
   验证索引宽度和包协议；之后才允许生成 BEM。先以庄方宜推进合并骨架分支更容易限定范围，
   洁尔佩塔再补自定义 Shader、跨 IB 和大索引支持。

未完成语义适配时，`--geometry-only` 也不能绕过骨架与布局检查。

## 使用与验证

RAR 先用现有解压工具解压；新入口目前直接接受文件夹或 ZIP。

```powershell
python tools/CustomModel/convert_hash_lod.py artifacts/converter-new-mods/gilberta --report artifacts/converter-new-mods/gilberta-report.json
python tools/CustomModel/convert_hash_lod.py artifacts/converter-new-mods/061fe0 --report artifacts/converter-new-mods/zhuangfangyi-report.json
python -m unittest discover -s tools/CustomModel -p test_hash_lod.py -v
```

两份报告均为 8 个入口、0 个结构错误，并列出尚不支持的转换要求；退出码 2 表示不能转换，
不是报告写入失败。8 项新增测试通过，覆盖入口选择歧义、回调、越界/base vertex、
大索引、骨架门禁、条件绑定及跨缓冲区绘制。本轮只验证新增前端和这两个输入，未重跑原生模块或游戏测试。
