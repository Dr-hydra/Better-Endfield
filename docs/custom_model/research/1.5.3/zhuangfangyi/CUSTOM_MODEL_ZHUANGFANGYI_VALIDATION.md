# 庄方宜首个 BEMPC25 测试包

## 当前状态

离线资料和定向运行时数据已齐全，默认状态 `$swapkey7=0` 的真实转换包已生成并部署。
资料 profile 为 `verified=true`，表示身份、声明、骨架映射和显式材质配方已核实；
生成时的 `render_verified=false` 表示转换工具不会自动宣称实机通过。
随后用户反馈“看上去是正常的”，本轮默认状态的实机外观检查记为通过。
此反馈未逐项列出动作或技能场景，不扩展为所有动画/场景验收；不要求重复既有测试。
生成产物保留生成时状态，人工实机结论以本文记录为准。

限定能力仍是骨架与材质分离、按绘制段切换固定材质、合并骨架、16 位输入骨骼编号重映射。
未增加 Shader 改写、逐 pass 拦截、运行时换装、32 位输出索引或自动网格分片。
洁尔佩塔自定义 GPU 路线仍不在此次范围内。

## 数据依据

- 当前 VFS 快照：`2954fa80-23c1-1579-2b22-4ecfd6d70418`，perforce CL 源值为空。
- 89 个依赖包，49 个 Mesh、64 个 renderer，64/64 所需离线引用完整。
  一个 initial 包尾段错误仍保留，不宣称所有 Bundle 对象均解析成功。
- 实机 run：`zhuangfy-20260917-045215-521682`，单一游戏进程 `60248`。
  世界/详情各四个 renderer 全部通过路径、Mesh、索引数、骨骼顺序和材质槽验证。
- 八组合并骨架的 673 个组内编号均有原生 Transform/bindpose 对应；LOD1 组映射回 LOD0。
- 采集请求完成后自动停用，没有运行时附加或全局场景扫描。

实际顶点声明 `[attribute, format, dimension, stream]`：

```text
头发：[[0,0,3,0],[1,0,1,0],[4,0,2,1],[6,3,4,1],[12,4,4,2],[13,6,4,2]]
其他：[[0,0,3,0],[1,0,1,0],[4,0,2,1],[5,0,2,1],[6,3,4,1],[12,4,4,2],[13,6,4,2]]
stride：头发 [16,12,12]，其他 [16,20,12]
```

Normal 是 Float32×1，不能因同占四字节而替换为 UNorm8×4。世界/UI 完整声明一致。

## 材质处理与明确取舍

配方文件：`tools/CustomModel/profiles/zhuangfangyi-061fe0.materials.json`。
由离线材质属性、关键词、原生 Shader 元数据及实际 D3D11 字节码声明共同核对槽位。
分析在离线完成；使用系统 D3DDisassemble 读取程序，不注入、执行或改写 Shader。
Shader 解析只覆盖当前样本需要的结构，不宣称适配所有 pass/版本。

| 部件 | 处理 |
| --- | --- |
| 头发 | 保留原生 hair_01 与 hairt_01 两个材质槽；同一源 draw 重复索引，共用顶点和骨架 |
| 身体 | 源颜色图绑定 `_BaseMap`，法线图绑定 `_BumpMap` |
| cloth_01 | 保留原生材质贴图 |
| cloth_02 | 第一段保留原图；后续 t19/t20/t21 分别对应 `_BaseMap`、`_MetallicGlossMap`、`_BumpMap` |

**身体首段是有意按贴图语义适配，不是逐槽照搬。** 源首段颜色/法线位于 ps-t14/t15，
后续段位于 ps-t13/t14；当前原生皮肤 Shader 的 BaseMap/BumpMap 是 t13/t14。
因此两种源状态均按颜色/法线用途绑定原生属性；用户随后确认整体显示正常，未报告首段外观异常。
后续段保留的 t15 DDS 与已绑定法线逐字节相同，按别名处理。

十条全局 TextureOverride 对应八份唯一原生 DDS；源包所含 mip（各一层）与目标原生贴图
对应字节完全相同，尺寸、格式和实机 graphics format 也一致，因此保留原生完整 mip 链。
这里没有独立重算 3DMigoto 的 GPU hash，不把原生属性 ID 当 ps-t 槽号。
其余五份唯一 DDS 是实际自定义贴图；包内七条纹理记录包含身体不同资源名的别名记录。

相关实现参考：[EFMI API.ini](https://github.com/SpectrumQT/EFMI-Package/blob/main/EFMI/Core/EFMI/API.ini)。
证据保存在 `artifacts/native-parser/zhuangfangyi-material-evidence.json`、
`zhuangfangyi-shader-binding-evidence.json` 和 `zhuangfangyi-texture-byte-comparison.json`。

## 实际包

文件 `artifacts/native-parser/zhuangfangyi-default-v25.bempoc`，147,341,124 字节。

| 组件 | 顶点 | 输出索引 | palette 骨骼 | draw |
| --- | ---: | ---: | ---: | ---: |
| C0 hair | 14370 | 104664 | 53 | 2 |
| C1 body | 7672 | 40167 | 77 | 2 |
| C2 cloth_01 | 1731 | 7362 | 11 | 2 |
| C3 cloth_02 | 59607 | 269799 | 61 | 11 |

所有组件满足 UINT16 输出索引与最多 256 骨骼槽限制。输入 UINT16 骨骼编号映射到原生 UINT8 palette。

生成命令（在开发工作树中运行，先完成解析与实机观测导入）：

```powershell
python tools/CustomModel/finalize_native_profile.py 'artifacts/converter-new-mods/061fe0/庄方怡' --database artifacts/native-parser/zhuangfangyi-database.json --observations artifacts/native-parser/zhuangfangyi-observed.json --native-textures artifacts/native-parser/zhuangfangyi-texture-raw.json --texture-dir artifacts/native-parser/texture-bytes --recipe tools/CustomModel/profiles/zhuangfangyi-061fe0.materials.json --output artifacts/native-parser/zhuangfangyi-verified-profile.json --evidence-output artifacts/native-parser/zhuangfangyi-material-evidence.json
python tools/CustomModel/convert_hash_lod.py 'artifacts/converter-new-mods/061fe0/庄方怡' --profile artifacts/native-parser/zhuangfangyi-verified-profile.json --report artifacts/native-parser/zhuangfangyi-conversion.json --output artifacts/native-parser/zhuangfangyi-default-v25.bempoc
```

资料库、游戏资源、完整 profile 和包为本地产物，不提交游戏数据。

## 部署和针对性验证

包已复制到下面两个位置，逐块比较确认字节一致，未计算产物哈希：

- `E:\Dr.Hydra\Better Endfield\artifacts\BetterEndfield-win-x64\custom-model\zhuangfangyi-default-v25.bempoc`
- `C:\Users\28377\AppData\Local\BetterEndfield\catalog\custom-model\zhuangfangyi-default-v25.bempoc`

实际生效配置是第二处目录的 `runtime.ini`，加入 `[Adapter.zhuangfy]` 和 `[Mod.zhuangfy]`，
同时显式保留管理员与佩丽卡。测试目录中的 `zhuangfangyi-runtime.ini` 是此配置的审阅副本。
不能只复制包到程序目录便认为 catalog 已安装。

本轮验证：

- lowering 17 项测试通过，包含纹理名称 pin 最高位为 1 时按 UInt32 位模式写入的回归。
- Shader/材质证据 4 项测试通过，覆盖 LZ4、DXBC 边界、描述段边界及原生 mip 字节/格式拒绝。
- 真实包通过生产 BEM 加载器；三角色配置通过生产 registry 路由校验。
- 当前 GameAssembly 只读导出表确认存在新 typed array 所需的两个 il2cpp 导出。

没有重复测试既有 LOD、懒加载或其他角色。以上不替代游戏内骨骼数组构建、GPU 上传与渲染验证。

本轮给用户的定向检查范围为：

1. 详情页：几何完整、皮肤颜色/法线、衣服贴图、头发双材质显示。
2. 大世界：同样外观，以及移动/动作时跨部件骨骼有无拉伸或错位。

2026-09-17 用户反馈：“看上去是正常的”。据此记录默认转换包的实机外观通过，
不再安排重复验证；具体动作/技能覆盖未单独确认，不将该反馈扩大到所有场景。
本次只更新记录，没有改包、重新部署或重复构建测试。
当前未合并、push 或发布 Release。
