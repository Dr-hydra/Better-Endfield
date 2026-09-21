# Android Mesh 提交与模型替换（2026-09-21）

本轮目标是先跑通本地 MuMu 模拟器的模型替换数据链路。外观、动画变形、穿模、阴影和远近距离画面验证由用户完成。

## 用户回归反馈与修正

用户反馈所有详情模型不显示、世界未看到替换。反馈发生时配置是 `replace=1;rollback=1`，这会在绑定检查后立即恢复原模型，因此不能用于外观验收。原先共享事务还会持续强制全局 LOD0、修改 NPC 裁剪设置；该策略不适用于世界资源从 LOD1 开始的 Android 客户端，是全局消失的可疑原因，尚需用户复测确认。

Android 现已跳过全局 pipeline/quality/culling 方法解析、LOD bias hook 安装与 LOD 参数维护，只保留资源绑定事务；PC 逻辑不变。运行日志明确打印 rollback/replace 模式和 `global LOD/culling overrides disabled`。此前 MeshData 与绑定回读 PASS 仍仅说明数据写入/恢复，不能代表角色可见或最终替换已完成。

去掉全局覆盖后的复测：`model-rollback-20260921-005831/runtime.log`（PID 18056）确认世界与详情各 11 个组件 commit/restore PASS，缓存 UI donor 复用 PASS，paired validation PASS，且后续进程存活。Android APK 与 PC DLL/BindingTests 编译成功，PC synthetic binding tests 通过。

随后 `model-replace-20260921-010006/runtime.log`（PID 18937）通过保留替换测试：世界与详情分别 `Resource committed`，paired publication PASS，PASS 后观察期未出现 fatal 且同一进程存活。模拟器保留 `resource=auto;package=/data/local/tmp/e.bem;replace=1`，无 `rollback=1`，供用户验证详情恢复可见与女管理员替换外观。此处仍只报告资源绑定证据，等待用户画面结论。

当前世界适配只覆盖 LOD1，LOD2/3 仍是原模型。远距离自动切换后的连续替换尚未完成，不得通过恢复全局强制 LOD0 来掩盖这个缺口。

## 已定位的根因

上一轮 MeshData 探针中的三个调用约定错误共同影响了结果：

1. `MeshDataArray.get_Item/Dispose` 的实例参数传入了 boxed object。值类型方法的 `this` 必须传 `il2cpp_object_unbox` 返回的数据地址。
2. `MeshData.m_Ptr` 的字段偏移相对于 boxed object，却被加到了已经 unbox 的地址。现在通过 `field_get_value_object` 读取字段，不自行叠加头部或字段偏移。
3. `Array.SetValue(Object,Int32)` 的引用参数传入了 `&descriptor_box`。正确参数是 `descriptor_box` 本身。

修正后，现有 `Array.CreateInstance + il2cpp_value_box + Array.SetValue` 就能构造正确的 `VertexAttributeDescriptor[]`。不需要再增加托管桥。旧报告中关于“Houdini 无法填充值类型数组”的判断不能作为现版本结论。

## 提交实现

- [android_mesh_builder.cpp](../android/app/src/main/cpp/modules/custom_model/android_mesh_builder.cpp) 是 Android 的独立几何适配器，接口只在同一个 native library 内使用，没有扩展 `BE_HostApiV1`。
- 所有托管方法按程序集、类型、方法、参数类型与返回值精确解析。Android 不调用 PC 的 `SetVertexBufferParamsFromPtr` 等 raw setter。
- `AllocateWritableMeshData` → descriptor 数组写入与回读 → 三条 stream → index buffer → 全部 submesh → `ApplyAndDisposeWritableMeshData`。
- 提交后从目标 Mesh 重新 `AcquireReadOnlyMeshData`，逐字节比较三条顶点流和索引，核对 attribute 序列、vertex count、submesh count、每个 draw 的 index start/count/topology/base vertex。
- 每个临时托管对象有 GC root，MeshData 生命周期由 scope 释放；已经 ApplyAndDispose 的数组不会再 Dispose。
- 共享构建器仍执行原始 declaration 检查、bindpose 调色板准备、bindpose 回读、HasBoneWeights、UploadMeshData(false)、原生影响数回读，再允许发布。
- 原生 `m_BonesPerVertex` 仍来自 ELF/AArch64 两条具名归档路径的一致字段扫描，写入只发生在独立新 Mesh；没有写死 `0x1C0`。

原模型往往关闭了 Read/Write，因此公开 AcquireReadOnlyMeshData 会拒绝它。读取源布局时使用已解析的 `MeshDataArray(Mesh,bool)`，`checkReadWrite=false`；这一路只查询 vertex/buffer count 和 buffer size，不获取或读写源顶点地址。独立新 Mesh 的提交回读仍使用公开的 checked API。其生命周期和参数语义可对照 [Unity MeshData 源码](https://github.com/Unity-Technologies/UnityCsReference/blob/2022.3/Runtime/Export/Graphics/MeshData.cs)。

## PC LOD0 与 Android 世界 LOD1

本客户端的女管理员世界 prefab 包含 LOD1/2/3，详情 prefab 保留 LOD0。只取旧日志最后几行会误以为世界 prefab 只有 LOD3。

实时采集的 11 个源组件比较见 `artifacts/android-refactor/lod0-lod1-comparison.json`：5 个组件索引数相同；多数骨骼顺序相同，但衣服存在调色板顺序差异、缺失骨骼和材质槽差异。不能用简单的 `_lod0` → `_lod1` 重命名绕过校验。

[world_resource_adapter.inc](../android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc) 使用同角色详情 prefab 作为准确的 LOD0 构建来源：

1. 在世界资源交付时，用游戏的 StringPathHash/I18NAssetLoader 加载同角色 uimodel；不实例化或显示它，使用结束后 Dispose 资源句柄。
2. PC 事务层按原 BEM 的组件名称、索引数、骨骼调色板和材质身份，从该 LOD0 来源构建完整替换数据。
3. 精确定位世界 prefab 的 `Mesh_all/lod1/<component>_lod1`，要求 renderer 坐标空间一致。
4. 每个调色板骨骼按相对于资源根的完整 Transform 路径映射，要求名称与静止姿态矩阵一致。不会按两个 LOD 的数组序号强行对应。
5. 把生成的 Mesh、材质和指向**世界角色骨骼**的新调色板交给同一套 Apply/Restore 事务。
6. 如果详情 prefab 已经替换，必须先验证 completed resource 的 Mesh、骨骼、材质、enabled 状态，再复用它，避免拿替换后的索引数重新匹配原版身份。

PC 的 LOD 前置检查保留；Android 保留游戏自身的 LOD/裁剪设置。两端继续保留绑定失败回滚。Android 世界和详情模型分别记录实际 renderer 名称，用于重复交付识别。

## 非画面证据

环境：MuMu Android 15，设备 `127.0.0.1:16448`；游戏 Android 1.5.3，系统 x86_64，通过 Houdini 运行 ARM64 Unity/IL2CPP。验证不等于真 ARM64 手机兼容性证明。

使用已有 `/data/local/tmp/e.bem` 女管理员测试包：

| 检查 | 证据 |
|---|---|
| MeshData scratch | 8 个有几何组件全部通过；包含 C9 的 7 个 draw |
| 提交后字节/布局回读 | `model-meshdata-20260921-004738/runtime.log`，PID 14897 |
| 世界角色构建 | `mesh-submit-world-rollback.log`，PID 14043；11 个组件，完整骨骼路径及 rest matrix 比较通过 |
| 世界角色绑定与恢复 | 同日志 `Android renderer commit/restore PASS: chr_0003_endminf_postmodel components=11` |
| 贴图 | 模拟器接受 BC7 sRGB/linear 和 BC5；尺寸、格式、mip 与材质槽读回通过 |
| PC 回归 | CustomModel DLL 与 BindingTests 重新编译；synthetic BEMv1 的 parser、donor identity、material isolation、rollback、ownership、appearance/LOD routing 通过 |
| ELF 扫描器 | 5 项现有 Android Mesh API 扫描单元测试通过 |

证据均位于 `artifacts/android-refactor/`。没有以“进程存在”代替模型替换通过，也没有对外观效果作判断。

## 复测入口

```powershell
./android/Test-CustomModel.ps1 -Serial 127.0.0.1:16448 -Mode MeshData
./android/Test-CustomModel.ps1 -Serial 127.0.0.1:16448 -Mode Rollback
./android/Test-CustomModel.ps1 -Serial 127.0.0.1:16448 -Mode Replace
```

脚本限定本地 root ADB 目标，安装 modern debug APK，使用单次游戏 PID 收集日志；检查 PASS 标记后继续观察进程，并检查 fatal。MeshData/Rollback 完成或失败时停止测试进程并恢复先前 debug property；Replace 成功后保留当前替换状态，便于用户验证画面。

`rollback=1` 会在正常绑定回读成功后恢复所有原字段。手动继续打开详情页还能验证 UI commit/restore，以及“UI 已提交后再准备世界模型”的缓存顺序。

`capture=1;replace=1` 是只读源元数据采集模式：生成现有 native sweep request，临时清空内存中的替换 registry；数据输出到游戏 cache 下 `custom-model-probe/custom-model/native-probe/`。不改已保存配置。

## 用户画面验收

- 女管理员的世界模型与干员详情模型是否都显示测试包外观。
- 待机、走跑、转向及已有动作的蒙皮变形，头发/衣服/脸部有无异常。
- 材质、透明部分、法线与阴影；尤其世界模型使用详情来源材质后的实际效果。
- 远近距离、切换角色、关闭再打开详情、重新进入场景后是否保持替换。

本轮先覆盖当前测试包、当前模拟器和这两类角色资源。其他角色、真实手机纹理格式支持与画面效果仍需要独立验证。
