# Task A runtime glue / 2026-10-03

基线 main dd7131b4 + 原工作区。只修改下列负责文件；没有修改 module.cpp、custom_model_binding_tests.cpp、cache/scheduler、camera 或 UI，没有构建 APK、安装或启动游戏。已有未提交改动保留。

**最新接入核对**：共享 module.cpp 已有 helper include/回调。读取时 `.inc` 在 `g_completed` 后而 world adapter 在其前；Android 编译必须把 helper include 前移到本记录第 3 步的位置。只读回调仍使用 `renderer_name`，Original/完成/缓存入口的 ReceiverKey 与材质/骨骼归属回读尚需 runtime 代理完成。这些 module.cpp 改动均非本代理所写。当前会话未暴露 send_input 或任何代理通信工具，未能直接发送给 runtime agent `01a10109-ec24-7291-9558-80dee8c2ac88`；已在 commentary 通知并提供本记录路径。

- native/modules/custom_model/generic_model_matcher.h（新）
- native/modules/custom_model/generic_model_matcher.inc（新）
- native/modules/custom_model/resource_policy.h
- android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc
- native/tests/android_world_binding_tests.cpp
- native/tests/generic_model_matching_tests.cpp（新）
- native/tests/generic_model_matching/CMakeLists.txt（新）

## module.cpp 精确接入位置与接口

1. TU 顶部，在现有 `#include "resource_policy.h"` 后增加：

```cpp
#include "generic_model_matcher.h"
```

2. `component.get_transform` 合同旁增加：

```cpp
{"game_object.get_transform",
    {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "get_transform", nullptr, "UnityEngine.Transform", 0}, true},
```

3. 在 `PreparedBinding` / `DonorMesh` / `DonorMaterials` / `DonorBones` / `UseSavedOriginal` 声明之后、`PrepareResource` 之前包含：

```cpp
#include "generic_model_matcher.inc"
```

此位置已有 `GetRendererEnabled`、`IsNativeObjectAlive`、`InvokeValue`。`.inc` 内只做枚举／路径／身份读取，不创建 Mesh/material/bone 数组。使用当前真实 root Transform，路径支持 `(Clone)` root 和外层 scene parent，拒绝循环、深度截断、非 descendant、同相对路径多对象。

4. runtime 在 `g_completed` 及其绑定字段定义之后实现以下确切函数；这是 Android adapter 唯一必需的新 runtime 回调：

```cpp
bool ReadGenericPristineMesh(const CharacterAdapter& adapter, void* asset,
    const GenericRendererCandidate& candidate,
    GenericMatching::MeshIdentity& identity);
```

只读 lineage 合同：以 `SameAdapter`、resource route、`candidate.key`（角色/route/path/region）查记录；当前 Mesh/material 弱身份和已提交 enabled/bone-path/shadow 状态必须一致，同 root 不能跳过回读。不同角色或 route 的生成资产不能当 pristine。多条可用 Original 返回 `Ambiguous`。已知 custom 且无 Original 返回 `CompletedWithoutOriginal`，不通过其保留的原 Mesh 名认定 pristine。不匹配／失活返回 `Unavailable` 或 false，不改写记录，不创建骨骼数组。

有活 Original 时调用：

```cpp
return ReadGenericMeshIdentity(original_mesh,
    GenericMatching::DonorOrigin::SavedOriginal, identity);
```

未改写 current Mesh 时调用：

```cpp
return ReadGenericMeshIdentity(candidate.mesh,
    GenericMatching::DonorOrigin::Pristine, identity);
```

`ReadGenericMeshIdentity` 读取实际完整 Mesh 名、原 Mesh 的每个 submesh 索引数；校验活对象和 1..256 submesh 范围。轻量完成身份用字符串/计数及 `WeakObject` 存储，不能长期保存 `MeshIdentity::mesh` 裸指针。强 Original 仍只服从现有热切换策略。

5. `PrepareResource` 的原 `ObjectName(renderer) == ComponentRendererName(...)` 循环替换为以下模式。候选验证回调应只读检查适用的骨骼/material/texture/空间/布局证据；跨组件引用的完整映射唯一后才执行原 `PreparePalette` / `BuildMeshFromComponent` / material 构建，保留这些上传检查：

```cpp
std::vector<GenericRendererCandidate> index;
if (!BuildGenericRendererIndex(adapter, asset, renderers, index)) return false;
std::vector<GenericMatching::Candidate> candidates;
for (const auto& c : index) {
    GenericMatching::MeshIdentity pristine;
    if (!ReadGenericPristineMesh(adapter, asset, c, pristine)) continue;
    candidates.push_back({c.key, c.renderer, std::move(pristine)});
}
GenericMatching::Request request{
    adapter.id, index.front().key.resource, identity.name,
    GenericMatching::Region::Lod0, identity.indices, !bem.skip_validation, {}};
auto match = GenericMatching::SelectUnique(candidates, request,
    [&](const GenericMatching::Candidate& c) { return ValidateReadOnlyCandidate(c); });
if (match.status != GenericMatching::MatchStatus::Matched) return false;
void* matched = candidates[match.index].renderer;
```

`ValidateReadOnlyCandidate` 是此处 runtime 的验证谓词占位名，并非新提供的函数。不能用按序挑选、strip 数字或候选构建产生副作用来替代。多个组件选中同一 renderer 用 `GenericMatching::DistinctReceivers` 拒绝。

6. `UseSavedOriginal` 以同一 receiver key 查询；唯一性判定后才做克隆骨骼数组重建。`RememberResource` 在覆盖前抓取已核验 pristine 名/索引/弱身份/receiver key，成功提交才发布完成记录。`IsCompletedResource` 包括同 root 在内逐 key 回读，clone 骨骼按真实本地路径而非仅骨骼名称。`ReadCompletedAndroidDonor` 由已验证 completed key 读 current custom，不再按 Renderer 名；无 Original 只复用同 selection。关闭包恢复与 proxy 恢复也按同一 key。`ComponentRendererName` 已保留无特例兼容接口，但正常匹配不能再依赖它。

## Android 生产行为

`PrepareAndroidWorldResource` 保留原默认参数调用兼容，增加末尾可选参数：

```cpp
std::span<const GenericMatching::ExactLodRelation> lod_relations = {},
const GenericMatching::AssetScope& asset_scope = {}
```

UI 实际 receiver path 产生 legacy 精确 `Mesh_all/lod0/<name>_lod0 -> Mesh_all/lod1/<name>_lod1` 候选；完整 UI Mesh `_20` 不被改写。目标完整 Mesh 独立检查。精确关系输入带 Android 平台、真实当前游戏资源 snapshot、角色、两端 route/path/完整 Mesh 名以及 reference/name 已确认状态。缺版本、仅 PC 表、仅名字/仅引用、重复对应不接受。目标索引合同属于 world，不是 UI 原索引数。不能把 BEM selection、日期或模块版本充当资源 snapshot；当前没有发布或硬编码全角色 Android `_8` 表。

proxy 以同 root 的 pristine world Mesh 实际对象引用找唯一 owner，包含未替换可见组件，支持多个 mobile proxy；desktop/nested/VFX 不并入。专用另一份 Mesh 且缺独立关系证据时拒绝，不能按名字隐藏。无几何且未隐藏不额外查询／修改 proxy。缓存 world/proxy 用 Original 引用；无可核验来源拒绝。保持 paired UI/world 发布和当前状态回滚，不在失败时返回半套输出。

## 局部回归

源码在 standalone 测试工程内由 MSVC C++20 编译；没有构建整个 native app 或 APK。运行命令：

```text
cmake -S native/tests/generic_model_matching -B artifacts/generic-model-matching-implementation-20261003/build -G "Visual Studio 17 2022" -A x64 -DCMAKE_GENERATOR_INSTANCE=D:/work/Visual Studio
cmake --build artifacts/generic-model-matching-implementation-20261003/build --config Release --parallel 2
ctest --test-dir artifacts/generic-model-matching-implementation-20261003/build -C Release --output-on-failure -V
```

最终计数记录在 `build/Testing/Temporary/LastTest.log`；本记录文件位于 ignored artifacts，不改其它文档。7 角色 `_20` / 6 角色 `_8` 来源为已有 Windows 名称审计，同 LOD 用例不外推 Android 跨 LOD已实机验证。没有性能测量，不声称 GPU 峰值改善。

最终局部结果：GenericModelMatchingTests **67** checks；AndroidWorldBindingTests **162** checks；CTest **2/2** 通过，Release 编译无 warning；负责的既有文件 `git diff --check` 通过。主代理／runtime 完成全入口 glue 后再负责整体构建和安装。
