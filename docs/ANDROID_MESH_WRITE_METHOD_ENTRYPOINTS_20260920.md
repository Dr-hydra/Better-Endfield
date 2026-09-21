# Android Mesh 写入方法级入口静态调查

> 2026-09-21 更新：本文保留上一轮调查记录。MeshData 数组失败已定位为调用约定错误；安卓提交、回读及资源事务的新进展见 [ANDROID_MESH_SUBMISSION_20260921.md](ANDROID_MESH_SUBMISSION_20260921.md)。关于“必须新增托管桥”“replace 始终关闭”的旧结论已被后续验证取代。


日期：2026-09-20

本轮只使用已提取的 Android `libunity.so` 注册表、有限 ARM64 反汇编和真实进程中的 IL2CPP 方法元数据。没有调用写入入口，也没有安装 Mesh 写入 Hook。

## 结论

Android 存在可以安装方法级 Hook 的 Mesh 写入入口，但它们不是 PC 当前构建器所使用的同一套 raw icall ABI。入口分成三层：

1. `Mesh` 旧 setter 链：可以观察顶点、索引、bindposes 和 Upload，但无法保持 PC 的 packed stream 写入方式。
2. `Mesh.MeshData` 构造链：这是 Android 当前最完整的写入链，包含布局、顶点/索引缓冲和子网格设置；它要求显式 `MeshData` native 指针和托管 `VertexAttributeDescriptor[]`。
3. `MeshDataArray.ApplyToMeshImpl`：这是最终提交边界，适合观察或做事务提交前后的验证，不是独立的字段 setter。

因此，“方法级 Hook 存在”已经可以确认；“PC 和 Android 共用同一个 Mesh 写入 Hook”仍不能确认。PC 直接调用 `SetVertexBufferParamsFromPtr` 等 raw icall，Android 当前注册表中对应的 PC 入口没有形成同 ABI 的证据。

## Android 当前注册入口

以下地址是 `libunity.so` 当前磁盘映像中的注册 entry，实际加载地址由模块运行时解析；它们不是可以跨版本硬编码的绝对地址。

| 方法 | entry | 参数特征 | 写入作用 | Hook 判断 |
|---|---:|---|---|---|
| `Mesh.SetIndicesImpl` | `0x517390` | Mesh 指针加索引数组/范围参数 | 旧 Mesh 索引写入 | 可观察，ABI 需按当前版本解析 |
| `Mesh.SetArrayForChannelImpl` | `0x517938` | Mesh 指针加托管数组/通道参数 | 旧 Mesh 顶点通道写入 | 可观察，不能直接承载三条 packed stream |
| `Mesh.SetNativeArrayForChannelImpl` | `0x517ABC` | Mesh 指针加 NativeArray 通道参数 | 旧 Mesh 通道写入 | 可观察，不能假定等于 PC raw pointer API |
| `Mesh.SetBoneWeightsImpl` | `0x518F20` | Mesh 指针加骨骼权重数据 | Unity 骨骼权重存储 | 不作为 `m_BonesPerVertex` setter |
| `Mesh.InternalSetBoneWeights` | `0x5190F0` | 内部骨骼权重路径 | Unity 骨骼权重存储 | 已排除目标字段 setter |
| `Mesh.set_bindposes` | `0x51977C` | Mesh 指针加矩阵数组 | bindposes 写入 | 可观察/调用，但不是 packed geometry 入口 |
| `Mesh.set_subMeshCount` | `0x519EF0` | Mesh 指针加数量 | 子网格数量 | 可观察/调用 |
| `Mesh.UploadMeshDataImpl` | `0x51A714` | Mesh 指针加 readable 标志 | 上传/锁定 Mesh 数据 | 只能作为晚期观察点 |

这些 entry 的注册 stub 和 native code window 位于 [android-mesh-api-report.json](../artifacts/android-refactor/engine/android-mesh-api-report.json)。

## MeshData 方法级入口

真实进程元数据给出的托管方法签名如下；`IntPtr` 是显式的 MeshData native 指针参数，不是可以省略的托管实例参数。

| 方法 | IL2CPP 方法 entry | native icall | 作用 | 结论 |
|---|---:|---:|---|---|
| `MeshData.SetVertexBufferParamsFromArray` | `0x40003CB63DDC` | `libunity.so+0x51ACE4` | 把托管 descriptor 数组转换为 native layout | 当前 Android 首要布局入口；需要安全构造托管数组 |
| `MeshData.SetIndexBufferParamsImpl` | `0x40003CB63E30` | `libunity.so+0x51AD60` | 创建索引缓冲 | 可作为方法级调用/观察入口 |
| `MeshData.SetSubMeshCount` | `0x40003CB63E84` | `libunity.so+0x51AD90` | 设置子网格数量 | 可作为方法级调用/观察入口 |
| `MeshData.SetSubMeshImpl_Injected` | `0x40003CB63F24` | `libunity.so+0x51AD94` | 写入 `SubMeshDescriptor` | 可作为方法级调用/观察入口 |
| `MeshData.GetVertexDataPtr` | `0x40003CB63AB4` | `libunity.so+0x51AC74` | 取得顶点缓冲地址 | 只读/写入数据窗口，不是布局 setter |
| `MeshData.GetVertexDataSize` | `0x40003CB63AF8` | `libunity.so+0x51AC78` | 取得顶点缓冲大小 | 边界校验入口 |
| `MeshData.GetIndexDataPtr` | `0x40003CB63C58` | `libunity.so+0x51AC90` | 取得索引缓冲地址 | 只读/写入数据窗口 |
| `MeshData.GetIndexDataSize` | `0x40003CB63C94` | `libunity.so+0x51AC94` | 取得索引缓冲大小 | 边界校验入口 |
| `MeshDataArray.ApplyToMeshImpl` | `0x40003CB64668` | `libunity.so+0x51AEFC` | 将 MeshDataArray 提交到 Mesh | 最终提交边界，适合观察和事务校验 |

静态反汇编还确认：`SetVertexBufferParamsFromArray` 会先处理托管数组，再调用 `libunity.so+0xFC57CC`；`ApplyToMeshImpl` 最终进入 `0xFC615C`。这些是当前版本的实现证据，不应当当作跨版本固定偏移。

## 方法级 Hook 与直接调用的边界

方法 entry 和 native icall entry 都可以作为 Dobby 的目标，但它们的职责不同：

- Hook IL2CPP 方法 entry，可以在托管绑定边界观察参数；要真正修改参数，必须满足生成 wrapper 的真实 ABI。
- Hook `libunity.so` icall，可以观察 Unity 原生数据提交；必须按当前 ARM64 实现确认参数和寄存器来源。
- `runtime_invoke` 适合调用已解析的托管方法，不等于可以安全构造所有 value-type 托管数组。
- `m_BonesPerVertex` 仍来自动态扫描出的 native Mesh 字段，当前没有证据表明上述 MeshData 方法会自动写入它。

当前探针已在 Unity 资源线程验证 Mesh、MeshDataArray、`get_Item` 和 `m_Ptr`。`mesh_data_probe=2` 进一步验证了 `Array.CreateInstance`、`Array.SetValue` 和 `il2cpp_value_box` 的组合，但当前 IL2CPP/Houdini 对 value-type descriptor 的 SetValue 路径会抛出 `ArgumentException` 或破坏托管堆；该写入模式已限定为显式实验开关并默认关闭。descriptor 数组仍需要游戏内托管桥或现成数组对象作为安全来源。

## 对 PC 复用性的结论

PC 现有构建器调用的是：

```text
SetVertexBufferParamsFromPtr
InternalSetVertexBufferData
SetIndexBufferParams
InternalSetIndexBufferData
SetSubMesh_Injected
UploadMeshDataImpl
```

Android 本轮只确认了 `MeshData` 链和旧 `Mesh` setter 的独立入口，不能把 PC typedef 直接套到 Android。可复用的是 BEM 数据校验、字段动态扫描、资源匹配和事务语义；Mesh 几何提交必须由 Android 专用适配器实现。

## 推荐调查顺序

1. 在托管层提供安全的 `VertexAttributeDescriptor[]` 构造桥。
2. 只调用 `SetVertexBufferParamsFromArray` 和 `GetVertexDataPtr/Size`，完成顶点流回读。
3. 再验证索引、子网格和 `ApplyToMeshImpl`。
4. 最后验证 bindposes、字段回读和 renderer 发布。

在第 1 步完成前，不重新打开 `replace=1`，也不把 `UploadMeshDataImpl` 或 `InternalSetBoneWeights` 当作字段 setter。
