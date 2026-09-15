# EFMI 转换与源模型校验

`convert_efmi_poc.py` 生成现有 BEM PoC-2.4 文件。支持 ALPHA-12 的
`TextureOverride_EntryPoint_ComponentN` 和 ALPHA-4 的
`TextureOverride_ComponentN` 基础部件入口。INI 是数据输入，不执行其中的命令。

## 先检查样本

```powershell
python tools/CustomModel/efmi_source.py path/to/mod.zip --report artifacts/mod-report.json
```

缺少 `Meshes/Components.buf` 的包需要原模型声明文件：

```powershell
python tools/CustomModel/efmi_source.py path/to/mod.zip --source-profile tools/CustomModel/profiles/pelica-mod_7b260.source.json --report artifacts/pelica-report.json
```

佩丽卡 profile **仅对应 mod_7b260 的组件编号和本次提取的游戏资源**，不能用于其他佩丽卡包或其他游戏版本。角色名称相同并不意味着部件编号、布局和绑定相同。

压缩包可带一层目录。配置按路径段匹配 `mod.ini`，不会误选 `DISABLED_mod.ini` 或备份；多个真正的 `mod.ini` 会报歧义。可用 `--ini archive/path/mod.ini` 选择确切文件；缓冲区存在多个同名匹配时仍拒绝转换，尚不支持一次导入包含多个独立 Mod 的合集。

报告检查：原索引数、三条流的数量与 stride、源声明匹配、骨骼槽位上限、三角形索引边界、显式绘制范围、缺失文件和未定义资源。另记录 LOD 蒙皮流与外部命令依赖。`conversion_ready` 只表示结构检查未发现阻碍，不表示已经通过实机、骨骼语义或材质效果验收。

## 生成 source profile

输入是 AnimeStudio 的 Mesh JSON 和 TypeTree Dump，以及人工核实的 Component → 原 Mesh 映射。只接受当前确认的原始顶点布局，未知格式、通道间隙和重叠直接拒绝。

```powershell
python tools/CustomModel/build_source_profile.py --mapping tools/CustomModel/profiles/pelica-mod_7b260.mapping.json --json-dir artifacts/pelica-research/json/Mesh --dump-dir artifacts/pelica-research/dump/Mesh --output artifacts/pelica.source.json
```

profile 补足缺失声明；默认拒绝不兼容流。有证据的转换需显式记录在 `stream_adaptations`，目前只实现 `retain_uv0`（12 字节输入逐顶点保留前 8 字节 UV，要求目标仅声明 float2 UV）。皮肤槽位数量通过不能代替骨骼顺序、bindpose 和实例引用的验证。

## 转换

```powershell
python tools/CustomModel/convert_efmi_poc.py path/to/mod.zip --source-profile path/to/source.json -o artifacts/model.bempoc
```

检查失败会在写输出之前退出。现有有声明的 ALPHA-12 样本无需 profile，旧命令仍可使用。生成的 PoC-2.4 文件没有新增角色路由协议。运行时现可在启动时选择管理员或佩丽卡，佩丽卡通过内置的本样本组件映射校验；仍不是同时启用多角色 Mod 的运行时。

当前保留基础 VB2；报告中的其他 LOD 流不写入该格式。自定义 RabbitFX 命令不会自动变成原生 Material 参数。缺少原贴图格式标签时现有转换器仍沿用 DDS 格式作为槽位提示，报告会提示必须核对原生材质/slot pins。

## 佩丽卡当前结果（实机校正）

2026-09-15 21:49:40 实机日志：旧几何包加载 10 个部件、0 张贴图，仅成功替换 7 个。C0（头发）、C1（脸）、C9（cloth_03）因包中 VB1 stride=8、游戏实际 stride=12 被拒绝。

离线序列化导出缺少运行时存在的 TexCoord2。实机声明为 `TexCoord0 Float32×2 + TexCoord2 SNorm8×4`。现已撤销 C0/C1/C9 的 12→8 裁剪，全部 30 条顶点流及 10 个 IB 保留 ZIP 原字节。mapping 的 `runtime_layout` 保存实机证据；生成 profile 同时保留 `serialized_streams`，避免覆盖后丢失差异来源。C7 的 43524→43518 显式绘制范围纠正仍保留。

材质转换新增 `--material-profile`，按实际部件绑定与原贴图名称 pin 映射，完全替代文件名组件掩码猜测。当前 13 条绑定覆盖头发主图/参数图、身体和衣服主图/法线/部分参数图、发线、脸法线、虹膜；同一 DDS 可用于多个不同原贴图名称。运行时为佩丽卡 renderer 创建独立材质，保持部件掩码独立，避免共享纹理对象的掩码并集导致串部件。

```powershell
python tools/CustomModel/convert_efmi_poc.py path/to/mod_7b260.zip --source-profile tools/CustomModel/profiles/pelica-mod_7b260.source.json --material-profile tools/CustomModel/profiles/pelica-mod_7b260.materials.json -o artifacts/pelica-lod0-native-materials.bempoc
```

这是原生材质近似测试，尚不是完整 RabbitFX 复现：Lightmap→P（MetallicGlossMap）仍需视觉验证；脸主图存在两个候选、部分头发辅助贴图和衣服发光图未映射；原生身体材质没有已确认的 Lightmap 对应槽。未知项继续保留游戏原图，具体范围见 materials profile。BC7 法线编码与原生 Shader 的解码兼容性也待实机验证。

新包为 66,711,124 字节，10 部件、13 条贴图绑定。独立回读确认 30 条流、10 个 IB、13 份 DDS payload 与指定来源逐字节一致且 EOF 精确。无需也未计算产物哈希。

## 回归验证

```powershell
python -m unittest discover -s tools/CustomModel -p "test_*.py" -v
```

2026-09-15：12 项测试通过，管理员原样本转换结果与修改前转换器逐字节一致（56,182,760 字节），没有计算文件哈希。游戏资源导出过程中有共享 Shader 解析错误，本次只采用成功导出的 Mesh/类型树，未据此宣称 Shader 兼容。

## 佩丽卡游戏管线测试入口

**程序目录与 catalog 目录不同。** 当前启动器使用 `%LOCALAPPDATA%\BetterEndfield\catalog` 作为 catalog 根目录。DLL/模块 INI 放在程序目录的 `modules`，模型包与角色配置必须放到 catalog，不能只复制到程序目录。

在 Host 的 catalog 根目录下放置：

```text
custom-model/
  runtime.ini
  pelica-lod0-native-materials.bempoc
```

`runtime.ini` 内容：

```ini
[CustomModel]
target=pelica
```

配置在模块初始化时读取，切换角色需要重启游戏。缺省为 `endminf`，继续使用原管理员包路径。未知 target 拒绝初始化。不要将其他佩丽卡 Mod 当成本样本使用：内置映射只对应 `mod_7b260`。

更新 CustomModel DLL 后，进入游戏并展示佩丽卡，按 F12 应用；F12 再次按下会回滚后重建，F11 恢复。新出现的活动实例继续由主线程刷新接管。日志应出现 `selected target: pelica`、目标 renderer 扫描以及实际成功部件数。原生 LOD0 锁定链路保持启用。

首次匹配同时检查组件 ID、renderer 名、原 Mesh 名和索引数；后续继承实例以已记录 Mesh 身份和组件名核对。佩丽卡仅接受 10 部件的包，贴图必须显式 pin 到原贴图名称。材质副本保留原 Shader 和参数，并替换已映射贴图；不使用管理员的其他 LOD Mesh/骨骼复用、无匹配 vfxpart 隐藏或 C9 专属 F6/F7/F8、9 字段实验。数字 0 的目标诊断仍可使用。

本轮验证：DLL 编译通过；11 项假 Host 生命周期场景通过（含角色隔离、组件身份、刷新和回滚），并通过生产 BEM 加载器读取实际佩丽卡几何包。这些检查不执行 Unity 顶点上传或游戏渲染，实机显示、骨骼语义及展示界面仍待验收。当前 C0/C1/C9 已恢复 12 字节原始流，材质近似包尚待实机验收。

分支测试装配目录：`artifacts/pelica-research/runtime-stage/`，含 `modules/BetterEndfield.CustomModel.dll` 及上述 `custom-model/` 文件。部署时 `modules` 复制到程序目录，`custom-model` 复制到实际 catalog 根目录。

2026-09-15 部署修正：已更新指定程序目录 `artifacts/BetterEndfield-win-x64/modules`，并将佩丽卡包和 `runtime.ini` 安装到 `%LOCALAPPDATA%\BetterEndfield\catalog\custom-model`。此前只复制到程序目录导致配置未读取，日志确认加载了旧管理员包（11 部件、24 贴图）。修正后用运行时同款 Windows INI API 读回 `target=pelica`。角色配置仅在初始化时读取，需重启游戏才能生效，实机效果仍待验证。


### 本轮验证与部署

15 项 Python 测试、11 项假 Host 生命周期场景（新增私有材质隔离和恢复）、Release DLL 构建、实际材质包生产解析通过。测试不等于 Unity GPU 渲染验收。新默认文件名是 `pelica-lod0-native-materials.bempoc`，需要同时更新 DLL 和 catalog 包，并重启游戏。F12 应用后应检查成功部件数和 `Textures: installed=...`（具体日志以运行时为准），F11 检查恢复。

第二版已同步至指定程序目录的 DLL 与 `%LOCALAPPDATA%\BetterEndfield\catalog\custom-model` 的新包，覆盖前备份于分支研究目录 deployment-backups。管理员样本输出与原基线逐字节一致（56,182,760 字节）。


## 双角色双路径实验

已增加管理员＋佩丽卡双角色状态隔离与独立材质。当前部署配置 `target=both`、`path=instance`，启动即启用两个角色；F11 同时停用并回滚，F12 同时重建。需重启游戏加载新版 DLL。两套 BEM 都放在实际 catalog/custom-model 下。

第二轮配置 `path=resource`，必须另起一次游戏加载验证。资源模式只在主线程 `_FinishWithAsset` 通知消费者前修改模板，没有实例扫描或 AI 附着兜底。尚未完成双角色任一路径的实机验收。步骤和证据标准见 `docs/CUSTOM_MODEL_MULTI_MOD_REPLACEMENT_WINDOW_20260915.md` 文末。

双角色刷新修复：材质创建统一使用 Array.Clone + Material 复制构造器，避免 Renderer.materials 返回已有实例导致 F12 重建被拒绝。15 项生命周期测试通过；游戏内重复刷新待确认。


### 2026-09-15 资源路径详情模型修复测试

当前双角色配置为 `target=both`、`path=resource`。新交付的原始模型按各 renderer 的源 Mesh 独立构建，材质独立复制；贴图按模型根对象整组匹配，修复后续管理员详情资源逐组件匹配导致 cloth_01 D/N/P 丢失的问题。游戏对已修改模板的自然克隆仍继承模板资源，并保留可回滚的来源追踪。部署后需完全重启游戏，检查管理员/佩丽卡详情页及反复切换；视觉结果尚待确认。详见双路径研究文档末节。
