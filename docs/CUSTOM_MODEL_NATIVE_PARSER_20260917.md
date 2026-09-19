# 通用原生角色资料解析器

## 目的与已实现范围

给转换器建立可复用的原生角色资料库，按需处理角色，不在程序里打包全角色 Mesh/贴图。
输入是安装目录 VFS 或已经提取的 AssetBundle；输出是带来源身份和明确缺口的 JSON。
只读游戏文件，不注入、不启动游戏、不执行 Mod 脚本、不修改 Shader、不计算产物哈希。

三层入口：

1. `tools/CustomModel/extract_native_bundles.py`：从当前 StreamingAssets＋Persistent 覆盖层提取
   manifest，精确选择角色 world/UI 资源并遍历依赖闭包。复用已有 VFS 代码。
2. `tools/CustomModel/NativeAssetReader`：使用本地 AnimeStudio 后端直接读取加载对象，保留
   serialized file＋PathID、PPtr 跨文件关系与 AssetBundle 内的资源路径。
3. `tools/CustomModel/parse_native_models.py`：解析引用图，输出 Mesh／骨骼／材质资料库，
   区分离线存储声明与运行时观测，保留无法解析的字段与来源。

普通 AnimeStudio JSON 导出会忽略对象自身 PathID 和所属文件；文件名也可能发生同名合并。
本工具不从这些缺少身份的 JSON 反推引用，不使用“同名即同对象”的兜底。

## 数据内容

- Mesh：精确身份、资源路径、顶点数、submesh 范围／拓扑、原索引数、bindpose 矩阵、骨骼名称 hash。
- 原始顶点通道：attribute、stream、offset、format、dimension 原始字节，保留 HG 高位标记。
  按格式宽度计算存储范围/对齐 stride，同时记录未知格式、重叠、padding 等情况。
- Renderer：GameObject/Transform 路径、所在资源根、Mesh 引用、bones 的原始顺序、rootBone、
  每根骨骼的对象身份/名称/层级路径、材质槽顺序。
- 材质：名称、Shader 引用与资源路径、贴图属性和对象名、尺寸/格式/mip/sampler、UV scale/offset、
  float/int/color 属性。
- Shader：基础图读取名称前缀和资源身份/路径；名称前缀为空时保留 null 和有效的 `.shader` 路径。
  可选 `--shader-metadata` 导出类型树元数据前缀及原始对象，供离线程序分析，不改写 Shader。
- 材质关键词：记录有效/无效关键词；可选 `--texture-bytes` 导出原始纹理数据用于逐字节核对。
  导出失败单独记录 `texture_export_error`，不丢弃已解析的纹理元数据。
- 来源：依赖库版本、Unity 文件版本、manifest version、perforce CL（源文件提供时）、
  提取的基础/覆盖层来源、读取错误。

**存储 stride 不自动等同运行时 VertexAttributeDescriptor。** 原始 dimension 高位带 HG 标记，
直接去掉高位并把结果当作原生上传声明不可靠。本工具不替调用方做这种隐式转换。
同一 Mesh 名可以有多个对象；世界/详情路径独立保留。

## 构建与使用

依赖：Python（VFS 提取还需要 PyCryptodome）、.NET SDK、兼容 AnimeStudio 源码，
以及已有的 ResConv。外部工具/游戏资产不纳入仓库。为修复高压缩率纹理误判，
`prepare_native_backend.py` 将外部源码复制到空 staging 目录，仅在副本修补 VFS 块检查和失败处理。
保留 512 MiB 单块、2 GiB 总大小限制，检查剩余输入，解压失败不再发布缓冲区。
脚本不修改外部 checkout；源代码不符合预期时拒绝套用补丁。

从开发工作树运行，下面 `$backend`、`$unpacker`、`$resconv` 应指向本机已有工具：

```powershell
python tools/CustomModel/prepare_native_backend.py --source 'E:\Dr.Hydra\Better Endfield\tools\EndfieldStudio' --output artifacts/native-parser/backend-source
dotnet build artifacts/native-parser/backend-source/AnimeStudio/AnimeStudio.csproj -c Release -f net8.0 -p:CopyLocalLockFileAssemblies=true -o artifacts/native-parser/backend
$backend = (Resolve-Path artifacts/native-parser/backend).Path
dotnet build tools/CustomModel/NativeAssetReader/NativeAssetReader.csproj -c Release "-p:AnimeStudioDir=$backend" -o artifacts/native-parser/reader-fixed

$unpacker = 'E:\Dr.Hydra\Better Endfield\tools\EndfieldUnpacker'
$resconv = 'E:\Dr.Hydra\Better Endfield\tools\FkArkEnd\ResConv\bin\Release\net10.0\ResConv.exe'
python tools/CustomModel/extract_native_bundles.py --game 'E:\Endfield Game' --character chr_0030_zhuangfy --unpacker $unpacker --resconv $resconv --output artifacts/native-parser/zhuangfangyi-inputs

dotnet artifacts/native-parser/reader-fixed/NativeAssetReader.dll artifacts/native-parser/zhuangfangyi-inputs artifacts/native-parser/zhuangfangyi-raw.json
python tools/CustomModel/parse_native_models.py artifacts/native-parser/zhuangfangyi-raw.json --output artifacts/native-parser/zhuangfangyi-database.json
```

提取目标必须是游戏目录外的空目录，避免旧版本 Bundle 混入。已有 Bundle 目录可以从第二层开始。
默认精确选 `postmodels/characters/..._postmodel.prefab` 和 `prefabs/uimodels/..._uimodel.prefab`，
不会把同名 NPC 版本或技能实体一起选入。额外变身资源用 `--extra-asset` 明确指定完整资源路径。
缺少目标资源或依赖会报错；不以其他同名 prefab 代替。

图解析会写出局部成功结果并在每个 renderer 下保留 `errors`。
CLI 成功表示资料库已生成，不表示所有引用完整；看 `offline_references_complete` 和 summary。
整项预期资源缺失时 `missing_resources` 会列出并返回退出码 2。

## 运行时观测合并接口

`parse_native_models.py --observations observed.json` 支持后续接入针对性 dump。
现已接入可选的定向资源交付探针，见[运行时采集步骤](CUSTOM_MODEL_RUNTIME_PROBE_20260917.md)。
庄方宜 world/UI 的八个目标 renderer 已完成真实采集并合入资料库。

```json
{
  "schema": 1,
  "evidence": "实机日志与观测时间/版本依据",
  "manifest_version": "必须与提取快照一致",
  "perforce_cl": "必须与提取快照一致；源文件为空时也保持空值",
  "renderers": [{
    "id": "资料库中的 serialized-file:PathID",
    "resource_root": "精确资源根名",
    "path": "精确 renderer Transform 路径",
    "mesh_name": "原 Mesh 名称",
    "original_index_count": 3,
    "strides": [16, 12, 12],
    "attributes": [[0,0,3,0],[1,3,4,0],[4,0,2,1],[6,3,4,1],[12,4,4,2],[13,6,4,2]]
  }]
}
```

以上为结构示例，数值不对应真实角色。运行时按资源/路径观测后，应关联到该次离线资料库中的明确 ID。
版本、资源、路径、Mesh 名、索引数任一不符，或观测重复/布局不合法，则拒绝合并。
运行时观测存到 `runtime_layout`，不覆盖 `serialized_layout`。
顶点声明观测不会自动把 Mod 的骨架与材质映射标记为已验证，`conversion_ready` 保持 false。

## 当前实测结果

### 佩丽卡既有 Bundle 集合

读到 51 个 Mesh、51 个 renderer。46 个 renderer 的离线引用完整；5 个脸部 renderer
有公共贴图引用缺失。新输出保留了普通按名导出可能丢失的重复身份。
本次直接 type tree 读取能记录到头发流的 16/12/12 存储声明，但仍不据此取消运行时布局核对。

### 当前安装版本庄方宜

从当前 VFS 提取 world/UI 及依赖共 89 包，无提取缺项。
修复后读取到 49 个 Mesh、64 个 renderer（world 52、UI 12），64 个 renderer 离线引用全部完整。
仍有一个 `initial/9f92bbd1e0f2191cffd3be18.ab` 内尾段识别错误保留在 `backend_errors`，
未造成这些 renderer 的引用缺失；不据此宣称全部 Bundle 内对象无错误。

#### 公共脸部高光复核

已确认上述缺项是读取后端误判，不是游戏缺少资源。对本次从当前 VFS 提取的
`main/fad70f510dbf5f5582d409ba.ab`，使用已有 `EndfieldUnpacker/AnimeStudio-net10/AnimeStudio.CLI.exe`
单包导出成功，得到 `T_actor_common_face_01_hl_M`（512×512）。此前管理员材质审计记录的
对象身份为 `cab-b6bb3cc4483165082b296844684c52da:-2443816471313249430`，
与庄方宜缺失引用完全一致；格式 BC7，10 级 mip。当前导出的 PNG 与此前管理员导出的 PNG
尺寸、模式、全部像素字节一致，未计算产物哈希。

通用读取器当前使用的 net8 后端拒绝 `compressedSize=2890, uncompressedSize=131072`
的合法块：其 `VFSFile.ReadBlocks` 检查要求解压大小不超过整个文件（4160 字节）的 20 倍，
此处 `131072 > 83200`，造成误判。替代 CLI 的成功导出证明当前游戏资源可离线解出。
证据在 `artifacts/native-parser/highlight-current.log` 和 `highlight-current/Texture2D/`。
随后已完成独立 staging 后端修复，并重新读取同一组 89 包，资料库现已包含该对象。
原始图和资料库 source 记录 `backend_patch=bounded-vfs-blocks-v1`，区分旧后端结果。
实际单包验证：正常文件解析出 1 个 Texture2D、无错误；截断 64 字节后退出码 2，未发布纹理。

Mod 的四个 LOD0 原索引数，在 world/UI 两侧都找到了候选：

| 原索引数 | Mesh | bones | 序列化 stride | 材质槽 |
| ---: | --- | ---: | --- | ---: |
| 52332 | S_actor_zhuangfy_hair_01_lod0 | 53 | 16/12/12 | 2 |
| 7014 | S_actor_zhuangfy_body_01_lod0 | 61 | 16/20/12 | 1 |
| 156318 | S_actor_zhuangfy_cloth_01_lod0 | 207 | 16/20/12 | 1 |
| 13452 | S_actor_zhuangfy_cloth_02_lod0 | 22 | 16/20/12 | 1 |

头发有 `M_actor_zhuangfy_hair_01` 与 `M_actor_zhuangfy_hairt_01` 两个材质槽，不能自动只保留第一个。
候选关系尚不等于 Mod 分组/材质语义验证通过。

产物在分支 `artifacts/native-parser/`：原始图、规范化资料库、提取快照及 `zhuangfangyi-summary.md`。
真实游戏数据不提交；后续 profile 只选用必要的已核实字段。

## 从资料库准备转换 profile

```powershell
python tools/CustomModel/prepare_native_profile.py 'artifacts/converter-new-mods/061fe0/庄方怡' --database artifacts/native-parser/zhuangfangyi-database.json --world chr_0030_zhuangfy_postmodel --ui chr_0030_zhuangfy_uimodel --output artifacts/native-parser/zhuangfangyi-profile-preparation.json
```

输出包含候选组件、完整材质/贴图目录、选中的默认绘制段、全局贴图覆盖和 `profile_draft`。
只按资源根、LOD 和唯一原索引数关联候选；歧义拒绝。合并骨架的 LOD1→LOD0 重映射要求
相同 Transform ID 和 bindpose，不按同名或相同骨骼数量猜测。世界/UI 再核对 Mesh 身份、
骨骼相对路径顺序和材质槽身份。草稿始终 `verified=false`，不把离线存储声明当运行时观测。

庄方宜四个候选组件的 world/UI 对照一致，八组合并骨架均可映射，全部 673 个组内编号有来源。
Mod 的 UV 输入 stride 为头发 12、其他三件 20，现已和世界/UI 实际运行时声明核对一致。
Normal 通道实际是 Float32×1，不能因为占用同样的四字节便填写成 UNorm8×4。
头发一 submesh、两材质槽：报告给出有依据的重复材质绘制候选，lowering 已支持单个源 draw 的
`repeat_material_slots`，最终配方已据此保留两个原生材质槽。

`finalize_native_profile.py` 将真实观测、资料库、原生纹理字节与显式材质配方合并为最终 profile。
它不会推断材质意图：配方必须覆盖选中状态；保留源贴图须有一致字节或明确绑定证据。
庄方宜已生成真实 BEMPC25 包、启用 Adapter 并部署测试安装，用户随后确认“看上去是正常的”。
十项全局覆盖对应八份唯一原生 DDS，核实依据是 mip 字节、格式和目标材质，未重算 3DMigoto GPU hash。
身体首段存在槽位语义适配，详见[验证记录及完整生成命令](CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION_20260917.md)。

## 验证

解析器构建通过（0 警告/0 错误）。8 项针对性测试通过：
跨文件引用、同名与同 PathID 隔离、重复身份、层级环、缺贴图后继续保留其他属性、
骨骼/矩阵有效性、HG 标记保留、序列化/运行时布局并存及过期观测拒绝、依赖闭包和 NPC 排除。
以上为解析器阶段验证；后续 Shader/材质证据新增四项针对性测试通过，真实包已通过生产解析并部署。
未重跑既有游戏场景；用户已确认默认转换包整体显示正常，具体动作/技能覆盖未单独确认。
