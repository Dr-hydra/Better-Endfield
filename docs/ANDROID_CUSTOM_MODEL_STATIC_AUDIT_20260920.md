# Android 模型替换静态兼容性审计（2026-09-20）

> 2026-09-21 更新：本文保留上一轮调查记录。MeshData 数组失败已定位为调用约定错误；安卓提交、回读及资源事务的新进展见 [ANDROID_MESH_SUBMISSION_20260921.md](ANDROID_MESH_SUBMISSION_20260921.md)。关于“必须新增托管桥”“replace 始终关闭”的旧结论已被后续验证取代。


Mesh 写入方法级入口的独立表见 [ANDROID_MESH_WRITE_METHOD_ENTRYPOINTS_20260920.md](ANDROID_MESH_WRITE_METHOD_ENTRYPOINTS_20260920.md)。

## 结论

PC 端 native/modules/custom_model/module.cpp 不能直接作为 Android 的模型发布实现。BEM 解码、组件匹配、事务状态机和资源身份规则可以复用；Mesh 原生上传、icall 名称、异常保护、模块生命周期和 LOD 依赖必须由 Android 适配层重新实现。

当前 Android 发布入口已主动关闭。配置含 replace=1 时，模块记录 Android replacement publication disabled pending static ABI audit 并返回，不安装资源替换 hook。这样旧配置不会在游戏启动阶段进入未经验证的共享发布链。只读资源/Mesh 探针仍可用，恢复包已在模拟器启动并保持运行。

## 已确认的静态事实

### 1. 目标运行环境不是原生 ARM64

模拟器系统 ABI 是 x86_64，游戏 APK 的 Unity/IL2CPP 库是 arm64-v8a，通过 Houdini 转译运行。此前的只读探针能工作，不能推导 Dobby 对 ARM64 目标的写入 hook、函数指针 ABI 和异常行为也安全。每个写入点必须在目标转译环境单独验证。

### 2. 七个 PC raw Mesh icall 尚未解析成功；不能用明文搜索证明不存在

对转移的 libunity.so 的静态字符串检查，PC 代码依赖的这些名称在当前 Android 库中没有出现：

~~~text
UnityEngine.Mesh::GetVertexBufferStride
UnityEngine.Mesh::get_vertexBufferCount
UnityEngine.Mesh::SetVertexBufferParamsFromPtr
UnityEngine.Mesh::InternalSetVertexBufferData
UnityEngine.Mesh::SetIndexBufferParams
UnityEngine.Mesh::InternalSetIndexBufferData
UnityEngine.Mesh::SetSubMesh_Injected
~~~

当前库中出现的是另一组 API：

~~~text
UnityEngine.Mesh/MeshData::SetVertexBufferParamsFromArray
UnityEngine.Mesh/MeshData::SetIndexBufferParamsImpl
UnityEngine.Mesh/MeshData::SetSubMeshImpl_Injected
UnityEngine.Mesh/MeshData::GetVertexDataPtr
UnityEngine.Mesh/MeshData::GetIndexDataPtr
UnityEngine.Mesh::SetNativeArrayForChannelImpl
UnityEngine.Mesh::UploadMeshDataImpl
~~~

新增的只读扫描器 [inspect_android_mesh_api.py](../tools/CustomModel/inspect_android_mesh_api.py) 已从 ARM64 ELF 的注册表入口解析出 11 个名字及其当前磁盘代码 entry：`SetNativeArrayForChannelImpl=0x517abc`、`UploadMeshDataImpl=0x51a714`、`MeshData.GetVertexDataPtr=0x51ac74`、`GetVertexDataSize=0x51ac78`、`GetIndexDataPtr=0x51ac90`、`GetIndexDataSize=0x51ac94`、`SetVertexBufferParamsFromArray=0x51ace4`、`SetIndexBufferParamsImpl=0x51ad60`、`SetSubMeshCount=0x51ad90`、`SetSubMeshImpl_Injected=0x51ad94`、`MeshDataArray.ApplyToMeshImpl=0x51aefc`。这些是注册表证据，不是已经确认的函数签名；运行时 entry 仍需和 IL2CPP 元数据逐项核对。

更正：此前运行日志是八项中的七项 unresolved，UploadMeshDataImpl 已解析成功。普通明文搜索遗漏了编码名称，因此不能证明这七项不存在。PC typedef 与参数单位也不能直接视为 Android ABI 证据。失败发生在共享模块初始化阶段，尚未安装资源替换 hook。

#### 注册表复核（本轮更新）

扫描器现只接受完整的五条 ARM64 注册指令：ADRP x0/name、ADRP x1/entry、两条匹配寄存器的 ADD、尾跳 registrar；entry 与 registrar 必须属于 ELF 可执行段。名称从 x0 地址读取，允许按固定前缀推导单字节 XOR 并要求段内终止、完整可打印名称。这是离线解码证据，不代表运行时已解码/已注册。扫描限定这种 stub 形状，不宣称枚举了全部注册途径。

当前可执行代码样本获得 62 个 Mesh/MeshData 注册项。其中名称地址 0x16904F 经 XOR 0x52 解得 UnityEngine.Mesh/MeshDataArray::CreateNewMeshDatas，入口 0x51AEF8 尾跳 0xFC60F8；AcquireReadOnlyMeshData 也使用编码名称。这纠正了“字符串里没有分配接口”的潜在误判。七个 PC 入口在本扫描覆盖的注册项中仍未出现，是否有其他注册路径尚未证明。

已核对以下有界函数片段（ELF VA 仅供本样本研究，不写入运行时代码）：

| 操作 | icall entry → 实现 | 指令证据与边界 |
| --- | --- | --- |
| CreateNewMeshDatas | 0x51AEF8 → 0xFC60F8 | 使用 x0 输出指针数组和 w1 数量，循环分配 0x190 字节对象、调用构造，再保存指针；推断为独立 MeshData 分配路径，未调用 |
| ReleaseMeshDatas | 0x51AEF4 → 0xFC606C | 遍历指针数组，递减引用计数，最后引用时析构释放；不能直接用 free |
| GetVertexDataPtr/Size | 0x51AC74/78 → 0xFC538C/A4 | x0 为数据对象、w1 为 stream；地址依赖 stream 描述，size 为 stride × vertexCount；必须先验证 stream 范围 |
| SetVertexBufferParamsFromArray | 0x51ACE4 → 0xFC57CC → 0xFC5834 | 有托管数组转换，元素大小 16；不能把 C++ descriptor vector 当托管数组传入，桥接层的间接调用尚待核实 |
| ApplyToMeshImpl | 0x51AEFC → 0xFC615C | 先转换 Mesh 对象，按索引格式校验索引范围，再进入 Mesh 数据应用路径；Mesh 和 MeshData 指针类型不能混用 |

本轮安装的 api_audit=1 入口仅枚举当前元数据与解析名称；不创建 Mesh、不安装资源 hook、不调用上述 entry。嵌套类型通过 namespace=UnityEngine、class=Mesh.MeshData / Mesh.MeshDataArray 解析，修正了此前把 UnityEngine.Mesh 错当 namespace 的问题。真正的 builder 仍未开放。

### 3. Android 的异常保护不能沿用 Windows 形状

platform_compat.h 将 __try/__except 定义成普通 if/else。它不会捕获非法读写或错误函数调用。PC 的 SafeSet* 包装在 Android 上不能作为 ABI 防护，必须改成：

1. 先解析托管方法或已确认的 native entry；
2. 用参数/返回值的静态和运行时证据确认 ABI；
3. 只在可回读的独立对象上测试；
4. 失败时通过显式状态返回，不依赖异常捕获。

### 4. 字段扫描证据和几何上传证据是两条独立链

当前加载代码的只读扫描仍得到两条具名路径：

~~~text
descriptor path: 0x167F92C
reader path:     0x1681778
field offset:    0x1C0 (448)
archive_functions=2, descriptor_paths=1, reader_paths=1
~~~

该证据支持新建 Mesh 的字段读写实验；它不证明 Android 的顶点、索引、子网格和 bindpose 上传 ABI。独立 Mesh 实验已记录 initialize=PASS source_value=4，但正式渲染器发布仍必须等待 Android MeshData 链路验证。

## 本次启动失败的定位

第二次替换测试日志显示七个 raw icall unresolved，最后返回 shared custom model initialization failed。之后的桥接改动存在确定的源码缺陷：把 void* (*)(void* context, const char* name) 强转为 void* (*)(const char*) 并只传 name，导致回调把名称地址当 context 解引用，第二实参也无保证。这是未定义行为，已整体移除，不应归因于手机架构或游戏兼容性。现有 tombstone 捕获的是 CrashSight/Houdini 的 NewStringUTF/strlen 崩溃，不能单凭该堆栈证明首个故障点；该源码错误是高度相关的首因候选。

同时发现并修正：共享 V1 ABI 无版本扩展、Android 写入回读未比较原值、把 dlopen handle 包装为假 mapped span、跳过 PC LOD 前置条件。PC 前置检查已恢复；共享 initialize 的 Android 分支也直接返回 NotReady，不能绕过上层开关进入未验证发布路径。字段写入通过不等于模型替换通过；此前把配置设为 replace=1 就告知“真实替换已可测试”的说法不成立，编译时字段写入门仍然关闭。

已采取恢复措施：

- Android 配置 replace=1 现在显式拒绝发布，不安装共享资源 hook；
- 移除了临时增加的 V1 host ABI 字段，避免破坏已有模块 ABI；
- 重新构建并安装后，使用 mesh_probe=1 启动游戏，进程保持运行，资源探针继续工作。

## 后续 Android 适配顺序

1. 用当前 Android MeshData 方法元数据、完整注册 stub 和实现指令建立方法级调用表；若方法被裁剪，明确记录，不能把名称存在当作签名通过。
2. 先在未发布的独立 Mesh 上完成 MeshDataArray/ApplyToMeshImpl 或等价路径的顶点、索引、子网格、bindpose 和字段回读。
3. 通过一个 Android 专用 AndroidMeshBuilder 接口向共享 BEM/事务层提供“已验证 Mesh 构造器”，共享层只处理资源匹配、准备和回滚。
4. 单目标资源完成构造、材质、骨骼、renderer 绑定和恢复测试后，才重新打开 replace=1。

本审计不把 Android 替换标为完成；当前可交付状态是 BEM 解析、资源命中、字段探针、MeshData 托管调用探针和静态兼容性边界。

### MeshData 托管调用探针（已完成安全边界验证）

`mesh_data_probe=1` 已在 Unity 资源线程验证未发布 Mesh、`MeshDataArray`、`get_Item`、`m_Ptr`
和全部 MeshData 方法契约，且会在 descriptor 数组写入前安全清理。`mesh_data_probe=2` 的写入实验
确认 `Array.CreateInstance` 能进入托管链，但 `Array.SetValue` 写入 boxed
`VertexAttributeDescriptor` 在当前 IL2CPP/Houdini 组合下会抛出 `ArgumentException`，另一种 native
数组路径曾造成托管堆损坏；写入模式已限定为显式实验开关并默认关闭。因而本轮没有 `PASS`，也
没有触碰原资源或 renderer；需要游戏内托管桥或现成 descriptor 数组来源后，才能继续顶点、索引、
子网格和 bindpose 验证。`replace=1` 仍保持关闭。

验证：离线扫描器的真实 stub、寄存器错配拒绝、明文/XOR、段内字符串终止及 ELF 边界共五项测试通过；最新 Android modern debug 与 PC CustomModelBindingTests 均构建通过。报告保存在 artifacts/android-refactor/engine/android-mesh-api-report.json，包含有界反汇编窗口，未采集私有游戏数据。
