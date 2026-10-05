# 资源交付边界续查（2026-09-14）

沿用 findings.md 的当前 DLL、dump 和局部 IDA 数据库。新增检查 16 个入口，导出 delivery.json、asset-completion.json、delivery-confirm.json。关闭全局自动分析，未改模块、构建或部署。以下均为静态正常实现路径，IFix 分支及实际目标角色是否命中尚需运行时确认。

## 最有价值的候选：AssetProxy._FinishWithAsset

`Beyond.Resource.Runtime.BundleLoader.AssetProxy._FinishWithAsset(UnityEngine.Object asset)`，RVA `0x03825490`。

- `0x0382555E`：将入参 asset 写入 `this+0xD0`，与 dump 的 `m_asset` 对应，后接 GC 写屏障。
- 检查 Unity 对象存活及 manager，`0x03825623` 写 `status`（+0x38）为 2。
- `0x03825941` 清空 `m_request`（+0xD8）。
- 正常实现内未见完成回调派发；调用方随后执行 `_OnAsyncCompleted`。
- `AssetProxy.Get`（0x03944E50）正常路径在 `0x03944E85` 直接返回 +0xD0，说明此处写入的正是后续消费对象。

已验证的汇合路径：

| 路径 | 调用证据 |
| --- | --- |
| 同步 `_LoadImmediate` | `0x03824426` BundleProxy.LoadAsset；`0x03824439` / `0x038244E8` FinishWithAsset；`0x038244F8` OnAsyncCompleted |
| `_FinishAsyncRequest` | `0x03825423` FinishWithAsset；冷块可见 AssetBundleRequest.get_asset 和组件类型转换 |
| 帧更新 `UpdateLoading` | `0x02C8E023` FinishWithAsset，紧接 `0x02C8E02D` OnAsyncCompleted |
| `_OnAsyncCompleted` | `0x03170BF8` / `0x03170CE6` Manager.RegisterOnCompleteInNextFrame |

这比仅监控异步返回 handle 更准确，也不依赖 PrefabInstantiateProxy._PreloadEnd 是否内联。可优先在此入口按目标 path hash 观察资源；若将来替换入参对象，应仍调用原实现，让它维护状态、检查和引用写屏障。暂不建议直接写 m_asset 字段。

此处是 BundleLoader 的交付边界，不是 Unity 内部反序列化最初入口，也不能证明对象此前从未被引擎内部代码使用。

## 请求到代理的上游

`BundleResourceManager.LoadAsync` → `_LoadAssetInternal`（0x0303A610）→ `Manager.LoadAsset`（0x0303A9D0）。

- `0x0303A770`：RuntimeManifestBinary.TryGetAssetInfoFromPath。
- `0x0303A7AE`：Manager.LoadAsset。
- `0x0303AA8E`：BundleProxyContainer.GetOrCreateAssetProxy。
- `0x0303AB3F`：Manager._LoadAssetProxy。

存在代理复用入口；本轮未继续展开容器和 _LoadAssetProxy，不能声称每次请求都会再次触发 FinishWithAsset。安装监控前已驻留的资源仍可能漏过，需要在首次交付前启动，另以装配消费记录检查覆盖。

## 克隆重载确认

前轮装配调用的 0x03BD3510，在 `0x03BD3559` 调用 `Object.Internal_CloneSingle`（0x03BD3600）。当前仅监控 Internal_CloneSingleWithParent 不覆盖该路径。后续应通过原资源实例 ID 与克隆结果关联，不能用层级路径代替身份关联。

## 提前替换必须保留的部件匹配关系

dump 的 NPCAvatarCreatorUtils 静态 +0x48 是 `Dictionary<string,SkinnedMeshRenderer> POST_MODEL_MESH_DICTIONARY`。

`CreateMeshAssetsInfoForPostModel`：

- `0x03DC4F4B` 取得 sharedMesh；`0x03DC4F5E` 取得 Mesh.name。
- `0x03DC4F7C` 将名称与 renderer 交给字典操作。
- 分 LOD 调用 CreateSMSInfoForPostModel，然后 AssignSkin；最后清理临时字典。

`CreateSMSInfoForPostModel`（0x03DC51F0）：

- 从 SubMeshInfo 的 +0x60/+0x68 取得 rootBoneName/rootBoneID，放入输出根骨骼信息。
- `0x03DC5354` 取 SubMeshInfo.meshName（+0x30）。
- `0x03DC5358` 使用该名称查询上述字典，并根据布尔返回值分流。
- 成功分支在 `0x03DC53A2` 将找到的 renderer 放入输出数组；未匹配分支做平台条件相关处理。

因此，模型不是只靠 GameObject/renderer 名称匹配；至少该路径实际依赖 **Mesh.name 与 SubMeshInfo.meshName 一致**，骨骼信息另从配置获取。直接换入任意命名的 Mesh，即使时机提前，也可能让原装配无法找到对应部件。替换方案需要维护这些关系，或同步调整配置。此发现是方案约束，尚不是旧版随机异常的已证实根因。

## 下一步最小运行时证据

1. FinishWithAsset 入口/出口：只记录目标角色 postmodel 的 pathOrNameHash、代理 instanceUid/objectID、asset InstanceID、真实类型、状态和线程；按首次加载记录部件 mesh 名称。
2. 装配消费：记录缓存取得的原对象、克隆对象以及 Mesh.name / SubMeshInfo.meshName 的匹配结果。
3. 核实目标首次交付早于克隆，且所有实际消费对象都能关联；然后才实施目标资源替换。

不要扩大全局 SetParent/SetActive 监控；不要在 LoadAsync 返回时假定完成；不要先恢复晚期 renderer 热替换。

## 证据边界

脚本只沿局部直接分支解码，不追所有间接调用；未命名尾调用可能混入导出，异常处理末尾也可能出现填充指令。所有关键结论取自明确地址的字段操作和具名调用，不依赖导出调用总数。_LoadImmediate 在 0x038247A8 的路径未解码。泛型 Get、缓存 fallback 的间接分派未锁定唯一 MethodInfo；不为其编造方法名或安装猜测 ABI 的 hook。
