# 第一人称相机与颈部补面（2026-09-15）

状态：相机模块 1.5.0；已实现并进行离线构建/几何测试，尚未实机验证本轮改动。

## 相机

继续沿用 `CinemachineBrain.PushStateToUnityCamera(CameraState&)` 最终状态覆写。
眼位由当前主控角色头骨定位；Lens FOV、NearClipPlane、位置和修正量通过运行期字段契约处理。
游戏自带拍照第一人称只在检测到已开启时退出，避免它隐藏整个身体。

## 本轮实现与证据

参考文件：用户提供的 `终末地enhancer.zip` 中的 `renodx-endfield-enhancer.addon64`。
本轮 IDA 产物保存在 `tmp_analysis/enhancer-port/`：

- `enhancer.dll.i64`：数据库。
- `0x1800b2a90.c`：头部处理、开口三角化、克隆、上传、验证和绑定的大函数。
- `0x1800afaf0.c`：缓冲读取与原生描述结构别名处理。
- `functions.json`：函数入口及关联的错误字符串。

原 mod 的补面沿用开口已有顶点，通过平面投影和耳切法生成索引。它没有为补面添加一个中心顶点。
可见副本网格与原始阴影网格分别绑定，并在退出时恢复。
原 mod 为读取带 Index 标志的缓冲，复制了固定原生描述结构、修改 target 位，并创建临时别名；这里不移植这种依赖内部布局的代码。

## 当前管线

1. 按名称解析 `il2cpp_resolve_icall` 的 Mesh、GraphicsBuffer、AsyncGPUReadback 和 Renderer 接口。
2. 比对元数据中的 `AsyncGPUReadbackRequest` 值类型大小/字段和 `SubMeshDescriptor` 字段布局。
3. 查询当前网格每个属性的 format、dimension、stream、offset，以及各 stream stride 和子网格描述。
4. 通过 `AsyncGPUReadback.Request_Internal_GraphicsBuffer_2_Injected` 按字节回读顶点及索引缓冲；检查完成状态、错误和实际字节数。
5. 解码位置和骨骼权重，仅用于几何判断。原始顶点数据、UV、打包法线和 HG 自定义通道不重写。
6. 在拓扑分析中合并位置和权重接近的缝合顶点；按连通部分的头骨权重判定头部，或对按名称命中的头部 Renderer 隐藏全部三角形。
7. 从剩余完整拓扑取真实边界，按绑定姿态中的颈/头骨坐标和骨骼权重筛选颈部开口；耳切法生成 n-2 个三角形，沿用原顶点索引。
8. `Internal_CloneSingle` 创建 Mesh 副本，仅重建副本索引缓冲及子网格范围。原顶点通道不重新编码。
9. 上传后回读副本和原件：核对副本顶点数据与原件一致、副本索引与预期一致、原件没有改变、bindposes 与骨骼未变化。
10. 再次确认 Renderer 仍使用捕获时的原网格，才设置副本网格、阴影代理和 updateWhenOffscreen。
11. 扫描所有 LOD 的 SkinnedMeshRenderer，每帧至多尝试一个 Renderer。定期检测模型/骨骼替换，退出或禁用时恢复；恢复失败的已绑定副本保留引用，避免销毁仍在使用的网格。

源码：

- `native/modules/camera/first_person_mesh.h`：不依赖 Unity 的拓扑、蒙皮判断和耳切算法。
- `native/modules/camera/first_person_retry.h`：按 Renderer 管理重试预算与失效清理。
- `native/modules/camera/first_person_runtime.inc`：接口适配、GPU 回读、克隆验证和生命周期。
- `native/modules/camera/module.cpp`：相机控制与入口。

## 工作树复用关系

参考 `.worktrees/dev-custom-model-design/native/modules/custom_model/module_poc2_part_02.inc` 的具名 icall 解析、索引缓冲上传、子网格布局与源声明验证方式。
没有拷贝其固定对象偏移回退、特定角色布局限制或 HG 原生字段诊断写入。
IL2CPP 数组长度、字节长度和头部大小通过导出函数获取，避免把 Matrix4x4 数组长度误当 float 数量。

## 配置与边界

- `first_person_hide_head`：启用头部网格处理，同时保留原阴影。
- `first_person_fill_neck_hole`：仅控制封口；关闭后头部隐藏仍然工作。
- `first_person_neck_plug_scale`：兼容原键名，现在表示颈部开口识别范围倍率，默认 1；不缩放原顶点。UI 已改名。
- 当前解码支持 Float32x3 位置，UInt8/UInt16/UInt32 骨骼索引，Float32/UNorm8/UNorm16 权重，以及单骨骼无显式权重布局。不支持的编码拒绝该网格。
- 不为跨多个材质子网格的同一开口猜材质；不为非流形、自交或无法完整三角化的环强行封口。
- GPU 回读目前在首次处理单个 Renderer 时等待完成，可能出现短暂帧耗时增加；没有每帧全模型回读。
- “跨版本”指按名称与当前元数据/网格声明解析并校验；接口、签名或格式不兼容时停止该部分处理，不保证所有未来版本无条件支持。
- 不使用 `Mesh.AcquireReadOnlyMeshData`，也不再声称该公开接口能读取任意 `isReadable=false` 网格。

## 验证

构建：`cmake --build artifacts/betterendfield-native-build --config Release --target BetterEndfield.Camera BetterEndfield.FirstPersonMeshTests`。
运行 `artifacts/betterendfield-native-build/Release/BetterEndfield.FirstPersonMeshTests.exe`。
覆盖：Renderer 独立重试预算、失效补丁/换网格/移出扫描范围后的重试重置、开口 n-2 封口、保持原顶点/三角形、UV 缝合、不同蒙皮不误合并、头身连通部分区分、凹多边形面积、自交拒绝、越界索引与异常权重拒绝。

接口名称存在性已对当前 UnityPlayer 二进制逐项检查；这不能代替运行时 ABI 和渲染验证。
实机仍需验证：进入/退出第一人称、低头/转头/跑动、头部阴影、开关封口、切角色、LOD、换模型及禁用模块后的恢复。
成功日志：`First person mesh: <renderer>, hidden=..., cap=..., rings=..., GPU verified, original shadow retained.`
失败日志包含接口缺失、ABI mismatch、GPU capture failed、unsupported vertex encoding、clone verification failed 或 renderer binding failed。

参考接口签名：Unity 官方 2021.3 `Mesh.bindings.cs`、`GraphicsBuffer.bindings.cs`、`AsyncGPUReadback.bindings.cs`。
