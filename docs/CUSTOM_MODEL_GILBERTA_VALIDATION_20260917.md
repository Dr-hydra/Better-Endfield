# 洁尔佩塔默认外观 BEMPC25 测试包

> 2026-09-18：用户报告“没替换”。日志证实四组件已提交；离线几何比对确认默认分支保留原服装。
> 用户随后明确选择 P 键另一套服装，并允许调整索引/分片限制。现已部署 Outfit B＋32 位索引版本，
> 以[后续修正记录](CUSTOM_MODEL_GILBERTA_OUTFIT_B_20260918.md)为当前状态，本文保留首版记录。

## 交付状态

从 `gilberta_12.rar` 样本的全部持久化默认值生成实际测试包，已经安装并启用。
本次只修改离线转换工具及测试代码；生产 CustomModel DLL、包协议版本均未变。
`conversion_ready=true`、`render_verified=false`，等待用户实机确认。
未支持 `$swapkey9=1`、面板/快捷键换装、任意自定义 Shader 或逐 pass 运行时拦截。

## 转换方式

新增 `tools/CustomModel/convert_reviewed_draws.py` 和
`profiles/gilberta-12.reviewed.json`。配方记录明确审阅的源程序文本、五份骨骼 Shader、
默认绘制范围、原生材质来源和语义取舍。源主 INI 或骨骼 Shader 有效内容发生变化时拒绝转换，
不会把所有 CustomShader 当空操作。输入 Mod 脚本不执行，源文件不改写。
此入口是已审阅样本的静态适配器，不是任意 HLSL/3DMigoto 通用解释器。

沿用采集 `aglina-20260917-1` 的八个真实运行时声明，并再次核对世界/UI 的 Mesh 身份、
骨骼名称/路径顺序、bindposes、材质槽。源骨骼数据搬运不改变 cloth 的局部骨骼顺序，
lowering 新增 `local_bones` 显式局部编号映射，和合并骨架区间互斥，拒绝无来源及越界编号。
本次四个组件分别使用自身的原生骨架，皮肤材质跨组件借用不改变几何骨架。

| 输出组件 | 顶点 | 输出索引 | palette 骨骼 | draw |
| --- | ---: | ---: | ---: | ---: |
| C0 cloth_01 | 49041 | 198615 | 168 | 11 |
| C1 hair_02（尾巴） | 2041 | 7080 | 13 | 1 |
| C2 hair_01 | 15499 | 130260 | 38 | 2 |
| C3 body_01 | 136 | 588 | 3 | 1 |

C0 前两段使用 C3 原生皮肤材质，其余九段用 C0 原生衣服材质。
跨入口重复只保留一次；每段的三角形/顶点内容不改变。
头发三段源范围连续且材质状态相同，先合成一个 submesh，再重复完整范围供原生第二材质槽使用。
尾巴两个连续同状态范围也合并。没有网格分片，所有输出索引仍为 UINT16。

## 材质取舍

六条全局覆盖的 DDS mip 数据、格式及实机 graphics format 与原生贴图一致，
保留原生贴图完整 mip 链。第七条皮肤覆盖和默认显式皮肤绘制共同使用
`Skin-DiffuseMap.dds`，绑定原生 `T_actor_aglina_body_01_D` 对应的 `_BaseMap`。
包内仅一份 8192×8192 BC7 sRGB 颜色图，作用于 C0 的皮肤段及 C3。

**明确限制：保留原生皮肤法线关闭状态。** 实际 `M_actor_aglina_body_01` 只有
`_DIFF_RAMP_ON`、`_SHADOW_LUT_TEX`，`_UseBumpMap=0`，`_BumpMap` 未绑定。
本次不注入法线属性、不启用新关键词，因此源 `Skin-NormalMap.dds` 未交付。
这份图不是恒定中性法线，不能声称它与“不绑定法线”逐像素等价；这是本次原生材质适配的明确取舍。
皮肤形状、高光细节及跨材质接缝须通过本次实机判断，不宣称所有 EFMI pass 完全等价。
默认状态不使用另一张皮肤颜色图和 Skin-LightMap；不打包未选中换装分支。

## 可重复生成

在开发工作树内运行（依赖已提取的原生资料和实机观测）：

```powershell
dotnet artifacts/native-parser/reader-fixed/NativeAssetReader.dll artifacts/native-parser/gilberta-inputs artifacts/native-parser/gilberta-texture-raw.json --texture-bytes artifacts/native-parser/gilberta-texture-bytes
python tools/CustomModel/convert_reviewed_draws.py artifacts/converter-new-mods/gilberta --recipe tools/CustomModel/profiles/gilberta-12.reviewed.json --database artifacts/native-parser/gilberta-database.json --observations artifacts/native-parser/gilberta-observed.json --native-textures artifacts/native-parser/gilberta-texture-raw.json --texture-dir artifacts/native-parser/gilberta-texture-bytes --output artifacts/native-parser/gilberta-default-v25.bempoc --report artifacts/native-parser/gilberta-conversion.json
```

报告及 profile 位于 `artifacts/native-parser/gilberta-conversion.json` 和
`gilberta-conversion.profile.json`。源程序配方可提交；完整游戏资料和 BEM 包留在本地产物目录。
原生纹理本次读取 87 文件、1779 对象、0 后端错误。

## 验证和部署

- lowering 19 项针对性测试通过，新增局部骨架映射顺序、越界和混用合并区间拒绝。
- 已审阅源流程的 3 项测试通过：有效代码/默认值/Shader 修改拒绝、注释允许、路径越界拒绝。
- `validate_gilberta_package.py` 独立读取实际包，逐三角形核对源索引映射、VB0/VB1 字节、
  权重和原生骨骼编号，并检查各段 donor/材质槽/纹理掩码、DDS 原字节和精确 EOF。
- 生产 C++ BEM 解析器通过；生产配置解析器验证四个角色及洁尔佩塔 world/UI 路由。
- 未重跑既有角色、LOD 或懒加载实机场景，未计算产物哈希。

实际包为 **70,453,962 字节**，已复制并逐块比较字节：

- `E:\Dr.Hydra\Better Endfield\artifacts\BetterEndfield-win-x64\custom-model\gilberta-default-v25.bempoc`
- `C:\Users\28377\AppData\Local\BetterEndfield\catalog\custom-model\gilberta-default-v25.bempoc`

实际 catalog 的 `runtime.ini` 新增 `[Adapter.aglina]`、`[Mod.aglina]`，
保留管理员、佩丽卡、庄方宜三项启用配置。程序测试目录里的 `gilberta-runtime.ini` 是配置审阅副本。
配置初始化时读取，下次从原测试启动器重新进入游戏生效。未合并、push 或发布 Release。

用户仅检查洁尔佩塔世界和详情页：皮肤颜色/接缝、衣服、头发/尾巴，以及动作时拉伸或错位。
不要求重复验证其他角色或已确认功能。
