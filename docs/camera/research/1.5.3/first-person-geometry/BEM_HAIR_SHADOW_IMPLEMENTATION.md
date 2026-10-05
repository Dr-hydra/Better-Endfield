# 第一人称去头发与阴影：双端实现记录

本次与 BEM 1.3 一同实现，但去头发不要求模型包升级到 1.3。旧包的替换几何也能使用 CPU 数据桥。

## 分类与角色数据

不新增一份必需的角色资源表。现有资源表仍负责角色及资源定位；现场剔除以当前 Renderer 的实际骨骼和绘制数据为依据，避免 PC、Android、LOD、世界/详情界面的 palette 顺序不同造成误判。以后如需经过验证的角色边界例外，挂在现有角色数据体系中即可，无需复制整套 catalog。

通用允许集合为实际 Head 子树，以及属于当前角色模型的明确 tail 骨骼链。Neck 不全局视为可移除。离开当前模型的同名骨骼、空 palette、空骨骼引用不能证明部件属于头部。

混合网格只剔除三角形的三个顶点全部正权重影响都落在允许集合内的面。未使用的 palette 项不影响这一判断；任何非零躯干、衣物或 Neck 影响保留该面。名称仅为扫描提示，不再触发混合网格整体隐藏，也不按连通块的头部权重多数删除衣服。已采数据与限制见 [骨骼分析](BEM_HAIR_BONE_ANALYSIS.md)。

| 当前数据条件 | 行为 |
| --- | --- |
| 整个 palette 均确认属于 Head/tail | 无需 GPU 回读，完整隐藏该独立部件并保留投影路径 |
| 当前 BEM 替换网格有 CPU 几何 | 从实际 position、skin、draw indices 精确裁剪可见索引 |
| 原版混合网格，GPU 回读可用 | 捕获并验证原始流，复制网格，仅修改可见索引 |
| 未知角色或资源表没有记录 | 使用现场 Head 祖先关系和明确的 tail 链，规则与已知角色一致 |
| 混合部件既无可用 CPU 几何，也无法 GPU 回读 | 保留无法确认的几何，不能仅靠名称删除躯干；可能仍看见部分头发 |

仅有名称的资源表不能恢复缺失的权重/索引。原版 Android 混合几何如果长期不可读，进一步完整覆盖仍需对应平台离线几何数据或经过验证的局部例外。BEM 的 keep/仅换纹理操作也不会凭空提供原版几何。

## 保留阴影

原实现直接关闭有投影的头发 Renderer，导致它的影子消失。现在：

- 原本投影的独立 Head/tail 部件使用 `ShadowCastingMode.ShadowsOnly`；保持 Renderer 启用。
- 原本 `Off` 的独立部件仅关闭自身可见 Renderer，不开启新的投影，已有影子代理保持原状。
- `shadowProxy` / `hairshadow` 部件不进入可见头发裁剪。
- 混合网格使用私有 clone 的索引隐藏头部，保留完整 shadow mesh。BEM 使用当前替换模型的完整源网格，避免继续投射原版轮廓。

[Unity 官方定义](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Rendering.ShadowCastingMode.ShadowsOnly.html)说明 ShadowsOnly 保持投影而不显示物体。**HG 双端自定义渲染的实际视觉效果仍需游戏内验收**；setter/getter、CPU 测试和编译通过不能替代该验收。

## 数据桥、内存和恢复

内部可选接口位于 `native/shared/include/BetterEndfield/CustomModelGeometry.h`，命名导出为 `BetterEndfield_QueryCustomModelGeometryV1`，不扩展 Host ABI。Windows 按名称查询模块；Android 使用可选弱符号。消费者仅在同步回调期间读取并复制视图，不访问 CustomModel 私有对象。

缓存只保留最终形变后的 XYZ、packed skin、UInt32 索引及实际 draw 范围；不保留纹理、TBN 或整个包。总上限 64 MiB，采用 LRU 与弱 mesh 身份检查。步长 4 的 skin 按生产上传规则使用第一个骨骼、隐含权重 1；步长 12/32 分别解码 UNorm16/Float32 权重。超预算、已淘汰、模块不可用时走通用回退。

退出、切人、LOD、换包或骨骼变化触发检查。恢复仅处理仍由 Camera 持有的状态：

- 外部 mesh 已接管时，不把旧可见 mesh 重新绑定回去。
- 仍留有本模块写入的影子时，独立清理；外部写入的新影子保留。换包后使用当前完整 mesh，不恢复上一包的轮廓。
- Off 隐藏后若新 mesh/palette 已接管，其隐藏状态保留，不重新开启 BEM 的 hidden 部件。
- palette 改序或语义不兼容时延迟恢复旧 mesh，保留 clone 的存活期；同次序、同名称的骨骼对象替换可正常恢复。
- 骨数组变化会重置失败预算；CPU 服务暂时 NotReady 时延迟重试。按 Renderer 轮转，防止一个未就绪部件挡住后续头发。

无 GPU 的 BEM 路径验证 clone 布局、submesh、bindposes 和实际 palette，按 Unity clone/索引上传 API 保留顶点。逐字节 GPU 流验证仅属于 GPU 可回读路径，不能将两者混称。

## 验证与验收范围

- 相机生产代码测试：11/11；其中头发/影子专项 57 项检查，通过第一人称生命周期、投影恢复、A→B 换包、外部投影写入、palette 改变与未知混合部件回退。
- 几何测试：连通头发/衣物、极小但非零身体权重、Neck、UV 缝、颈部封口、非法索引和权重。
- CustomModel CPU 桥测试：最终形变位置、实际 draw、弱对象地址复用、线程约束、缓存预算、同 mesh 更新和引用释放。
- Windows Camera/CustomModel、WinUI、Android Java 与 arm64 native Release 编译通过。未安装到游戏做视觉验收。

游戏验收建议覆盖：Aglina 的两块 hair、噗切娜 fur 混合部件、提夫罗斯头部/衣物混合 BEM；开关第一人称后影子完整；A→B 热切换后影子跟随 B；切人/LOD/退出恢复；未知角色及无 GPU 的 Android 原版混合网格保持身体。
