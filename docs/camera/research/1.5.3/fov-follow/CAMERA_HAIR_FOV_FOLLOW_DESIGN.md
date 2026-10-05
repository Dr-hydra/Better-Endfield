# 第一人称头发与阴影、全局 FOV、自由相机跟随方案

本记录只读核对 3.4.1 源码和已采集资源，未实现这些功能，未进行游戏内验证。

## 头发与阴影：已有问题和证据

Windows 与 Android 共用 `native/modules/camera/module.cpp`；Android 的编译入口见 `android/app/src/main/cpp/CMakeLists.txt:62`。

当前处理分为两条路径：

- `first_person_runtime.inc:354` 的 `FpTryDirectNamedHide` 对名称命中、palette 中没有 Head 子树外骨骼的 Renderer，直接设置 `Renderer.enabled=false`（`:378`）。该路径没有设置单独的影子保留策略，因此确实会一并停掉这个 Renderer 的投影；不能靠它的 `shadowProxyMesh` 继续投影。
- `first_person_runtime.inc:395` 的 `FpBuildPatch` 修改克隆 Mesh 的索引，并在 `:449` 保留已有 `shadowProxyMesh`，没有代理时保留裁剪前的 Mesh 供投影。这条路径有保留影子的设计，不能把所有阴影丢失归因于它。

另外，扫描没有排除独立阴影代理；名称规则包含 `hairshadow`（`module.cpp:1343`）。独立代理若被当作普通头发裁剪或禁用，也可能失去投影。需要运行日志确定反馈角色实际经过了哪条路径。

本轮第一人称诊断代理只读检查了手机 `/data/user/0/com.hypergryph.endfield/cache/betterendfield-diagnostics.log`：文件 362,227 字节，设备修改时间 2026-10-02 08:13，日志无时间戳、仅有 tid，已轮换且没有完整的开关操作开头。全文件共有 133 条 `named part requires topology fallback, but GPU readback is unavailable`。代表记录是第 2 行 `S_actor_aglina_face_01_shadowProxyMobile`，第 3 行 `hair_01_shadowProxyMobile`，第 8 行 `hair_01_lod1`、第 9 行 `hair_02_lod1`（tid 19646）。因此 Android 这次残留至少有实际的“混合/未确认独立部件需要裁剪、GPU 回读却不可用”证据，也确认阴影代理进入了可见头部扫描。该日志不能证明设置 `ShadowsOnly` 在 HG 移动渲染流程中一定有效，也不证明上述部件已被成功禁用或裁剪。

目前“精确”能力有限：

- 名称匹配只检查对象、Renderer、Mesh 三个名称（`module.cpp:1397`），没有 BEM 骨骼语义联动。
- `FpPalette` 已读取**当前 Renderer 的实际 bones** 和 bindposes，按 Head 的实际祖先关系分类（`first_person_runtime.inc:275`），并非只看 Head 名字。
- 拓扑规则对连通块累计 Head 子树骨骼权重，超过 50% 才隐藏整个块（`first_person_mesh.h:144`）。与身体连通的发片或头发、尾巴混合块可能需要更细的分类。
- 混合命名部件拒绝直接隐藏后，仍把 `part.matched` 作为 `hide_all` 传给拓扑裁剪（`first_person_runtime.inc:408`）。因此“直接隐藏拒绝”并没有真正防止后续整块误删，需要一并修正。
- GPU 回读不可用、顶点编码不支持、克隆验证失败都会使局部裁剪无法完成（`first_person_runtime.inc:405`、`:503`）。联动骨骼名称不能修复这些失败。

已有资源支持采用骨骼驱动，而不是继续堆名称关键词：噗切娜 `S_actor_purrchena_fur_03_lod0_20` 的 palette 包含 `hair_base_*`、Head、Neck 和帽檐骨骼（`tools/CustomModel/catalog/chr_0038_purrche.json:2779`），并非名叫 hair；`fur_01` 则包含围巾与躯干，不能把所有 fur 一律归为头发。归档的 PC 世界/详情记录确认该角色 hair_base 链在 Head 之下；这只能证明所检查资源，不能外推所有角色、平台、Mod。

## 建议的联动方案

1. **先解决保影子与误删。** 对已确认的独立头部 Renderer 保持 enabled 原状态，使用按名称解析的 `shadowCastingMode=ShadowsOnly`；不再通过 enabled=false 隐藏。需要在当前 HG 渲染流程上验证该模式，尤其 Android。若该路径不生效，才评估使用可见裁剪 Mesh 加独立完整投影代理，不能宣称设置枚举就已验证。局部裁剪继续沿用已有完整 `shadowProxyMesh` 方案。扫描时把 `Shadow_Proxy` 等经过资源核对的代理分组为影子来源，避免将其再次当可见头发处理。
2. **以 live palette 为主，角色配置辅助。** 共用一份小型角色头部配置，记录 Head/Neck、已核实的头发/帽檐骨骼路径和必要别名；从 BEM catalog 的 names 与采集路径生成候选，经核对后发布。运行时用实际骨骼对象、祖先关系、模型相对路径确认。BEM 1.2 的 `BoneNameMatches` 已支持 aliases（`bem.h:112`）；复用这一规则，不把原始 donor index 当成 Mod palette index。BEM 更换 palette 的既有逻辑见 `custom_model/module.cpp:1695`。
3. **独立与混合分开。** 所有实际使用的影响都来自已确认头部集合时，可以整 Renderer 只投影。混合 Renderer 走子网格/三角形的蒙皮权重分类，只移除确认属于头部的部分；保留躯干、尾巴、围巾等。不能仅凭含 hair 的骨骼或名称就保证语义精确，Head 权重也可能用于非头发物体。连通块和颈部空间约束作为辅助，不能强制把混合块整体删除。极端 Mod 需要创作者给出部件/三角形级标注，不能从名称推导百分之百准确的分割。
4. **共享规则，不强制启用 BEM。** 原版角色也能用同一分类。当前模块 ABI 没有通用跨模块消息/资源查询服务；最小版本可以先共用分类库和精简配置，避免为了去头发新增完整运行时总线。将来若需要 BEM 提供已绑定部件语义，再引入正式接口。
5. **资源变化重新确认。** 缓存必须含角色/模型根、Renderer、Mesh、palette 与包版本，切角色、LOD、热切换后失效；只恢复自己仍持有的赋值，避免把 BEM 新 Mesh 覆盖回旧 Mesh。Android 的 BEM 世界适配会让新 Mesh 投影并关闭对应 Mobile 代理（`world_resource_adapter.inc:87`、`:133`），第一人称必须保存当前正在使用的完整 Mod Mesh，而不是强行恢复原版影子。

Android 不应把 GPU 回读作为方案前提。独立 Renderer 可以仅依据 live bones 与经验证的语义设置只投影模式；对混合 Mesh，更可靠的路线是在创作者工具导出时利用已知 CPU 几何，预生成第一人称专用索引/替代几何和明确隐藏语义，或给创作者提供手动标注。运行时按确切组件、Mesh、palette 与子网格契约选择该几何，不需要从 GPU 重读顶点。原版混合资源也可通过离线证据生成版本绑定的精简配置；不能直接把 PC 顶点/索引布局当成 Android 世界资源布局。BEM 的 names/aliases 只提供身份，不能凭这些字段分辨同一骨骼影响下的头发、衣服和尾巴。上述可选几何需要正式的包格式能力和模块间共享接口，现有 BEM 1.2 没有这项第一人称语义，不能宣称已经兼容。

优先级：保影子和混合部件误删 > 共享骨骼分类 > 创作者精细标注。先用反馈角色的日志区分 direct hide、GPU 不可用、回读失败和拓扑未命中，再做必要的资源分类补全。验证至少包含进入/退出、切人、BEM 热切换、LOD 和阴影；当前代码还不能证明 Android 保影子效果。

## 全局 FOV：可行，适合小步实现

当前 `field_of_view` 只设置自由相机的控制值（`free_camera_runtime.inc:808`），第一人称还有自己的 FOV。普通第三人称没有持续的全局覆盖。

已有能力：

- `Camera.get/set_fieldOfView` 元数据契约：`module.cpp:421`。
- 最终 `CinemachineBrain.PushStateToUnityCamera` 接点：`module.cpp:1959`。
- `LensSettings.FieldOfView` 按当前元数据解析：`module.cpp:1264`。
- 自由相机在最终 CameraState 写 FOV（`free_camera_runtime.inc:279`），冻结时有直接相机写入和非缩放时间心跳（`:284`、`:864`）。

建议增加独立的默认关闭开关和 FOV 值，普通游戏主相机在最终输出阶段只改 lens FOV，不接管位置、朝向或移动输入。目标相机应独立于“自由相机已进入”的状态解析，并按当前主相机及 Brain 归属过滤；沿用 `BrainDrivesActiveCamera` 的归属核对思路（`:720`），防止波及其他 Camera。

模式优先级应明确：VMD/路径、第一人称、自由相机保留自己的 FOV；全局值作用于普通相机。也可以另设全模式覆盖，但不应默默取消作者的 VMD 变焦。固定值会压掉游戏原生变焦；若希望保留缩放变化，可以提供相对偏移模式，但这是产品行为选择，不是底层接口障碍。

关闭后优先停止覆盖，让下一次原生 CameraState 输出恢复；不能无条件写回启用时的旧 FOV，因为场景或相机可能已经更换。冻结且没有原生输出时，才用针对同一有效相机捕获的最近原生值恢复。正交相机、剧情及详情等范围需要补相机类别/投影模式的可靠检测；当前代码没有完整的全场景分类，不能承诺每种界面同样有效。

成本与风险主要是模式优先级和相机生命周期，算法与性能开销较低。建议在第一人称退出/切人生命周期问题定位完毕后实现。

## 自由相机跟随角色相对移动：可行，先做平移

当前 `CharacterPose` 已读取当前主角色模型根的位置和水平朝向（`free_camera_runtime.inc:301`），但自由镜头普通操作仍在世界坐标中移动；`MotionAnchor` 在运镜开始时取固定锚点（`:347`）。VMD 已有 MMD 舞台参考逻辑（`:509`），不代表普通自由镜头已有实时跟随。

最小功能只跟角色平移：每次自由镜头计算时读取新角色位置，计算其相对上次的位置变化，并将同一位移加到自由镜头 target、smoothed 和当前 view；保持原镜头旋转和 FOV。手动移动仍修改相对偏移。这样人物跑动时镜头随人物移动，人物转身不会突然旋转镜头。将相同位移作用于目标与平滑状态，避免把跟随误差塞进已有指数平滑（`:701`）导致多余拖尾。

可选的进阶“跟随转向”以角色 yaw 保存本地偏移，使用 `p_camera=p_actor+R_yaw*offset`；只跟水平朝向，避免动作中的俯仰/骨骼摆动带动镜头。继续自由鼠标控制；是否看向人物是另一选项，不能与相对移动自动绑在一起。

生命周期要求：每帧重新确认主角色/模型身份，不复用长期裸指针。切人时重设锚点并保留当前世界镜头，传送时默认重设锚点或明确选择随传送移动；资源暂时没有模型时冻结最后视图，恢复后重新建立参考，避免一次累积大位移。该规则与本次第一人称切人排查共用原则。

第一版仅手动自由镜头启用；运镜、关键帧、VMD 有各自锚点和轨迹语义，开始这些播放时暂停普通跟随或转入专门的相对坐标播放模式，避免位移被加两遍。世界冻结时人物不动，现有相机心跳仍允许手动调整。无需新增角色移动/碰撞接口；镜头自动避墙属于后续功能，当前自由镜头没有这项保证。

建议顺序：第一人称生命周期问题 > 全局 FOV > 自由镜头平移跟随 > 转向跟随/相对轨迹。两项拓展都能用共享原生代码覆盖双端，UI/配置/Android JNI 命令需要分别补齐。
