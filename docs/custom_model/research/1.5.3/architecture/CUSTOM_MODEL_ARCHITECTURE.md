# BetterEndfield.CustomModel 架构设计

状态：**设计冻结（实现前）**  
分支：`dev/custom-model-design`  
目标模块：`BetterEndfield.CustomModel`

> 本文只记录已经确定的架构边界，以及进入实现前必须通过样本验证的开放项。开放项在验证前不得固化为稳定文件协议或运行时契约。

## 1. 目标

新增独立模块 `BetterEndfield.CustomModel`，用于把第三方自定义角色模型应用到《明日方舟：终末地》的运行时角色实例，同时保持 Windows 与 Android 的共同实现路径。

第一阶段优先兼容 EFMI Tools 生成的标准角色模型资源子集，但 EFMI 只作为**导入源格式**，不作为 Better Endfield 的运行时格式或运行时依赖。

最终数据流：

```text
EFMI MOD
  |
  | Better Endfield 软件端导入/转换
  v
Better Endfield Model Package (.bem)
  |
  | 游戏进程内
  v
BetterEndfield.CustomModel
  |
  v
Unity Mesh / SkinnedMeshRenderer
  |
  v
游戏原生 Skeleton / Animator / Avatar / Shader
```

## 2. 已确定的架构决策

### 2.1 新增独立功能模块

自定义模型替换不继续堆入现有 `BetterEndfield.Model`。

职责划分：

- `BetterEndfield.Model`
  - 登录场景角色与动画控制；
  - 游戏内 PostModel 资源之间的替换；
  - 登录视觉相关能力。
- `BetterEndfield.CustomModel`
  - 外部模型包加载；
  - 目标角色/Renderer 捕获；
  - Unity Mesh 构造；
  - SkinnedMeshRenderer Mesh 替换；
  - Skeleton / bind pose 绑定；
  - 自定义纹理绑定；
  - 替换生命周期与回滚。

两个模块不得相互链接形成强依赖。可复用的 Unity/IL2CPP helper 应放入共享代码或通过 Host ABI 提供。

建议源码位置：

```text
native/modules/custom_model/
    module.cpp
    runtime/
    bem/
```

Windows 构建产物建议为：

```text
modules/BetterEndfield.CustomModel.dll
```

Android 继续遵循当前项目模式：复用同一份核心 native 源码，由 Android Host adapter 提供 IL2CPP、Hook、对象操作与配置能力。

### 2.2 替换层固定在 Unity Mesh / SkinnedMeshRenderer 层

不在以下层级实现主替换逻辑：

- Endfield VFS / `.chk` / AssetBundle 物理替换层；
- D3D11 DrawCall 层；
- Vulkan DrawCall 层；
- 3DMigoto/XXMI 图形 API 兼容层。

主运行时边界为：

```text
GameObject
  -> SkinnedMeshRenderer
      -> Mesh
      -> bones / rootBone
      -> Materials / Textures
```

第一原则：尽量保留游戏自己创建的角色对象、Animator、Avatar、Skeleton、角色逻辑组件、Shader 与 Material，仅替换自定义模型真正需要改变的 Mesh/Texture 数据。

### 2.3 EFMI 转换在软件端完成

游戏进程内的 `BetterEndfield.CustomModel` **不解析完整 EFMI 运行时语义**。

EFMI 导入器负责：

- 读取标准 EFMI `mod.ini`；
- 定位模型 Component；
- 读取 VB/IB Buffer；
- 解析 EFMI Tools 使用的顶点布局；
- 收集纹理资源与 TextureOverride 映射；
- 做兼容性检查；
- 转换为 Better Endfield 自有中间格式 `.bem`。

运行时只认识 `.bem`，不要求 EFMI、3DMigoto 或 XXMI 存在。

禁止在第一阶段实现完整 3DMigoto INI VM，包括但不限于任意 `CommandList`、自定义 shader、任意表达式和图形 API 状态机模拟。

### 2.4 `.bem` 是通用中间格式，不是“EFMI 缓存”

`.bem` 的设计必须与 EFMI 解耦，以便未来增加：

```text
EFMI -> BEM
Blender exporter -> BEM
glTF/其他导入器 -> BEM
```

因此运行时模型接口不能暴露 EFMI 专属概念作为核心 ABI。

### 2.5 Windows 与 Android 共用模型语义

运行时算法应保持平台无关：

```text
BEM
 -> component mapping
 -> Unity Mesh creation
 -> skeleton binding
 -> renderer apply
```

平台差异只能存在于：

- 文件来源/权限；
- native library 装载；
- IL2CPP Host adapter；
- 可选的纹理预处理/压缩策略。

不得为了 Android 重新设计一套 Vulkan 模型替换系统。

## 3. 建议模块内部结构

```text
native/modules/custom_model/
├─ module.cpp
│   ├─ 模块 ABI
│   ├─ 配置生命周期
│   └─ Hook 安装/卸载
│
├─ runtime/
│   ├─ character_capture.cpp
│   ├─ renderer_matcher.cpp
│   ├─ mesh_builder.cpp
│   ├─ skeleton_binder.cpp
│   ├─ texture_binder.cpp
│   └─ replacement_session.cpp
│
└─ bem/
    ├─ bem_reader.cpp
    ├─ bem_manifest.cpp
    └─ bem_types.h
```

软件端导入器建议独立于 native 运行时：

```text
UI / converter layer
└─ EFMI Importer
    ├─ ini pattern reader
    ├─ buffer decoder
    ├─ texture importer
    ├─ compatibility validator
    └─ BEM writer
```

具体落在现有 UI 项目、公共 .NET 库还是独立 converter 项目，在检查现有 UI 工程依赖后决定；该选择不改变运行时边界。

## 4. 运行时流程

目标流程：

```text
模块启用
  |
  v
读取配置与 BEM manifest
  |
  v
等待目标角色实例进入稳定生命周期
  |
  v
捕获 GameObject / SkinnedMeshRenderer
  |
  v
按 BEM component mapping 找到目标 Renderer
  |
  v
读取原 Renderer Skeleton / rootBone / 原 Mesh bind pose 信息
  |
  v
创建 UnityEngine.Mesh
  |
  v
写入 vertex/index/skin 数据
  |
  v
绑定 Skeleton / bind poses
  |
  v
应用 sharedMesh
  |
  v
按支持范围应用纹理
  |
  v
保存回滚状态
```

任何阶段失败时，应恢复被修改 Renderer 的原始状态，不留下半替换对象。

## 5. Skeleton 策略

第一阶段方向固定为：**复用目标角色原生 Skeleton 与 Animator，而不是从 EFMI 构建新的 Unity 骨架层级。**

预期使用：

- 原 `SkinnedMeshRenderer.bones`；
- 原 `rootBone`；
- 原角色 `Animator` / `Avatar`；
- 可验证时复用/重建对应 bind poses。

EFMI 的 Bone Index / Weight 必须转换到这个原生 Skeleton 语义。

### 仍需验证

以下内容在拿到样本前不写死：

1. EFMI 导出后的 bone index 是否可直接对应目标 Component 的原 bone slot；
2. 不同 Component 是否拥有不同局部 bone table；
3. Merged Skeleton 输出如何表达 remap；
4. 原 Mesh bindposes 能否直接用于自定义顶点，还是需要由目标 bones 重新计算；
5. 跨服/跨平台同角色 Renderer 与 bone slot 是否稳定一致。

## 6. Material / Texture 策略

第一阶段不重建 Endfield 角色 Shader/Material 系统。

默认：

```text
原 Renderer
  + 原 Material / Shader
  + 自定义 Mesh
  + 按明确映射替换部分 Texture
```

优先验证 Mesh/skin 能正确跟随动画，再开启纹理兼容。

EFMI 的 DDS 不作为 Android 运行时必须支持的输入。软件端转换器应负责解码或转码为 BEM 可消费的跨平台纹理表示。

最终选择 RGBA、KTX2、ASTC、保留 BCn 或多平台变体，需要根据目标 Unity Texture API 和设备验证结果决定。

## 7. BEM v1：已确定与待确定内容

### 已确定

BEM 至少要表达：

- 格式版本；
- 模型 ID / 显示信息；
- 目标角色标识；
- Component 列表；
- 每个 Component 的顶点与索引数据；
- Skinning 数据；
- Component 到目标 Renderer 的稳定匹配信息；
- 纹理资源与目标 Material/Texture slot 的映射；
- 源格式与导入诊断信息；
- 能力/兼容性标记。

### 待样本验证后冻结

以下不在当前设计阶段写死：

- container 是 ZIP、单一二进制还是 manifest + sidecar；
- vertex binary layout；
- index 固定 16/32 位还是按 Component 标记；
- bone index 位宽；
- Renderer identity 的最终键；
- texture payload 格式；
- LOD 与 shape key 的 v1/v2 边界。

原则：先用真实 EFMI MOD 和游戏运行时数据确定最小充分字段，再冻结 schema。

## 8. EFMI 第一阶段兼容范围

第一阶段目标是“EFMI Tools 标准角色模型资源的可移植子集”，而不是宣称兼容任意 XXMI/3DMigoto Mod。

计划支持：

- 标准 Component；
- VB0 几何数据；
- VB1 UV / vertex color（存在时）；
- VB2 bone index / bone weight（存在时）；
- IB；
- 多 Component；
- 基础 TextureOverride 映射；
- 原角色 Skeleton / Animator；
- 原角色 Material / Shader。

第一阶段明确不保证：

- 任意 INI 脚本；
- 自定义 Compute Shader / CustomShader；
- 任意 CommandList；
- GPU ShapeKey pipeline；
- EFMI Merged Skeleton 高级逻辑；
- Spatial Identification；
- 任意 DrawCall 状态覆盖；
- D3D11/Vulkan shader injection。

这些能力以后如果需要支持，应转换成 Better Endfield 自己明确建模的高级 BEM feature，而不是在 runtime 中模拟 3DMigoto。

## 9. 第一阶段 PoC

PoC 不追求完整 UI 或完整 EFMI 导入。

### PoC-1：BEM -> Unity Mesh

手工/离线把一个最简单的 EFMI 角色 MOD 转成临时 BEM 数据。

目标：

1. `BetterEndfield.CustomModel` 能被 Host 加载；
2. 捕获一个确定目标角色的 `SkinnedMeshRenderer`；
3. 从 BEM 创建 `UnityEngine.Mesh`；
4. 正确写入 geometry / index / skin；
5. 复用原 Skeleton；
6. 自定义模型可跟随原角色动画正常变形；
7. 关闭模块或失败后可恢复原 Mesh。

通过标准：模型在至少一个 Windows 场景稳定播放角色动画，无崩溃、无明显错骨。

### PoC-2：EFMI Importer

PoC-1 成功后，再实现 EFMI -> BEM 软件端转换。

通过标准：同一 EFMI MOD 不需要人工修改二进制即可导入并生成 PoC-1 可加载的模型包。

### PoC-3：Android parity

将同一 BEM 模型包交给 Android 版本的相同 CustomModel 核心逻辑。

通过标准：不依赖 Vulkan DrawCall Hook，在 Android ARM64 客户端上使用 Unity/IL2CPP 对象层完成相同替换。

## 10. 需要收集的实现资料

进入 PoC-1 前，最有价值的资料如下。

### A. EFMI MOD 样本（最高优先级）

至少一个**能在当前 EFMI 正常工作的、尽量简单的角色替换 MOD**，完整保留：

- `mod.ini`；
- 所有 `Meshes/*.buf` 或等价 Buffer 文件；
- 所有 `Textures/*.dds`；
- MOD 作者若额外提供 metadata/config，也一并保留。

最好再提供第二个具有下列任一特征的复杂样本，用于判断 BEM v1 是否需要预留能力：

- 多 Component；
- 多 LOD；
- Merged Skeleton；
- ShapeKey；
- Toggle。

### B. EFMI Tools 导出时对应的原角色信息

需要知道每个样本是从哪个角色导出的，至少提供：

- 角色 ID / 名称；
- 使用的 EFMI Tools / EFMI Package 版本；
- 若有 Frame Analysis / dump 目录，可一并提供；
- 若导出工具生成原始 component metadata，保留完整文件。

### C. 游戏运行时 Renderer / Mesh 探针结果

对与 EFMI 样本对应的角色，在游戏内输出：

- 所有 `SkinnedMeshRenderer` 名称；
- 每个 Renderer 的 `sharedMesh.name`；
- vertexCount；
- subMeshCount；
- indexCount；
- bones 数量；
- `rootBone.name`；
- Material 数量与名称；
- 可读取时的 vertex attributes / index format / bindpose count。

如果目前没有探针，PoC 开始时可以先在 `CustomModel` 研究代码里补一个只读 dump。

### D. 原始 EFMI Component -> 游戏 Renderer 对应关系

如果你已经知道 EFMI `Component0/1/...` 分别对应游戏中的哪个 Renderer，请把已有映射或判断依据提供出来。

如果不知道也没关系；可以通过 index count、buffer layout 和运行时 dump 联合建立映射。

### E. Android 上模型资源导入的产品形态

实现 Android 软件端 importer 前再确定即可：

- 希望手机直接导入 EFMI zip/folder；还是
- PC 端转换成 `.bem` 后传到手机；还是
- 两种都支持。

这一项不阻塞 Windows PoC。

## 11. 暂不需要的资料

PoC-1 阶段暂时不需要：

- 完整 Endfield VFS 解包格式；
- Vulkan pipeline dump；
- D3D11 shader dump；
- 整套 3DMigoto/XXMI 源码移植；
- 所有角色的完整资源包。

如果 Mesh 层 PoC 失败并确认原因在游戏自定义 Renderer/skin pipeline，再针对性向更底层补研究，不提前扩大范围。

## 12. 成功判据

该设计成立的核心判据不是“能够解释 EFMI INI”，而是：

> 同一份由 EFMI 转换得到的 BEM 模型，可以在 Windows 与 Android 上通过 Better Endfield 的 Unity/IL2CPP 对象层挂到目标角色的 SkinnedMeshRenderer，并复用原角色 Skeleton/Animator 正确播放动画，而无需 D3D11/Vulkan DrawCall Hook。
