# PostModel 定点静态检查（2026-09-13）

## 输入与范围

- 工作树：`dev/custom-model-design`，检查时 HEAD `cb8a2bef`。
- 二进制：`E:\Endfield Game\GameAssembly.dll`。
- 符号参考：主工作区 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal`。
- IDA 9.2，`-a -A` 关闭自动分析；8 个具名目标，加 3 个必要的直接被调函数。没有全库交叉引用扫描，没有运行游戏，没有修改模块或部署 DLL。
- 当前 dump 的目标入口均命中 DLL 的 PE 异常表函数起点。此检查支持使用这些入口分析，不能代替运行时版本验证。
- `inspect.py` 是本次局部指令导出脚本，`focused.json` 保存调用地址与汇编。没有进行全量反编译。
- 部分函数有远端代码块、泛型共享入口、间接调用及内联。导出不是完整调用图；未命名跳转目标也可能是尾调用。装配函数在 `0x03DC8F00` 存在未解码路径，不把缺失调用视为不存在。

## 1. 预加载不必经过被监控的 I18N 入口

`SquadManager._PreloadEndmin`（`0x0399D6D0`）包含：

- `0x0399D770`：调用 `CharUtils.get_curEndminCharTemplateId`。
- `0x0399D793`：调用 `PreloadManagerUtils.PreloadCharByID`。
- `0x0399D811`：调用 `BattleDataLoader.PreloadEntitySkillData`。

`PreloadGroupChar._Preload_Mesh`（`0x0399D3F0`）包含：

- `0x0399D4C6`：`DataManager.TryGetEntityTemplate`。
- `0x0399D535`、`0x0399D578`：访问 `modelData` 和 `ModelData.loadPath`。
- `0x0399D598`：路径转换入口 `0x0351CDA0`。
- `0x0399D5CF`：调用泛型共享入口 `0x03F33240`。
- `0x0399D5F1`：返回的 handle 交给 `PreloadGroupBase.AddHandle`。

`0x03F33240` 的已检查路径：

- `0x03F33291`：取得 `ResourceManager.instance`。
- `0x03F33359`：接口分派调用。
- `0x03F333AD`：另一分支直接调用 `BundleResourceManager.LoadAsync`（`0x03038BC0`）。

因此，静态代码存在绕过当前 `I18NAssetLoader.Load` hook 的模型预加载路径。尚未证明用户那次 ENDMIN 正是从这里执行，需要运行时关联 hash 与 handle。

## 2. 装配阶段确实优先使用 preloadCache

当前 `CreateNpcComponentByPostModel` 签名比旧 dump 多一个 `Dictionary<Int64,FAssetProxyHandle> preloadCache` 参数。

在 `0x03DC646B` 调用 `0x03CD2410` 时，传入缓存、loader 和路径转换结果。后者：

- `0x03CD2442`：判断缓存是否为空。
- `0x03CD248F`：执行查找，负索引进入缺失分支。
- `0x03CD24D9`：对取出的 handle 调用 `FAssetProxyHandle.IsValid`。
- 有效时复制并返回已有 handle（`0x03CD2516` 起）。
- 缺失/无效时在 `0x03CD2555` 进入另一泛型/间接加载调用；本轮未展开其实现。

这意味着只拦装配时的新 Load 会漏掉缓存命中。上层入口的返回值也不等于资源已可修改；必须区分异步请求发起与资源完成。

## 3. 实例化覆盖不能只依赖一个 Unity Clone 重载

`PrefabInstantiateProxy._PreloadPrefab`（`0x02F670F0`）读取实例化模式、`m_useBeyondInstantiate`、`selfObject` 与异步操作字段。

- `0x02F67292` 直接调用 `Object.Instantiate` 的一个入口。
- 其他分支调用不同实例化入口，且会检查 `m_instantiateOperation`（对象偏移 `0x20`）。
- `0x02F672C3` 写入 `selfObject`（偏移 `0x28`）。
- `0x02F672E4` 直接写入 loaded 标志（偏移 `0x18`），存在 `_PreloadEnd` 逻辑内联。
- `OnCompleted` 中 `0x03D7F4FA` 调用 `RegisterOnCompleteInNextFrame`，完成通知还有下一帧派发边界。

不能只 hook `_PreloadEnd` 或单个 `Internal_CloneSingleWithParent` 就认为覆盖了所有资源完成/实例创建。

## 4. 挂接不是可靠的“已完成绑定”判据

`CreateNpcComponentByPostModel`（`0x03DC5D20`）包含资源获取和克隆、部件装配、Animator 设置、挂点与布料处理：

- `0x03DC670A`：`CreateMeshAssetsInfoForPostModel`。
- `0x03DC73D8` 起：解析 `Animator.set_avatar`、`SetTransformHierarchy`、`AnimatorUtility.Deoptimize...` 等 native 调用。
- `0x03DC7FEA`：`_SetAccessoryRootBone`。
- `0x03DC875C`：`ClothProcess.Init`。

独立检查的 `_RefreshAllPartsBind`（`0x03DAAC80`）中，在 `Transform.SetParent` 和局部位置/旋转设置后，还有四处 `SetSMRRootBone` 调用，以及 SkinnedMeshRenderer 的 native setter。这些是该函数内的代码证据，尚未证明本次 gameplay 装配一定调用此函数。

因此，不能由层级上出现 `entity/mesh` 推断所有骨架和渲染状态已准备完成。

## 建议的下一步（尚未实施）

只增加定向、只读运行时关联：

1. 在角色预加载上下文记录角色 ID；在 `BundleResourceManager.LoadAsync` 按目标 hash 记录请求与返回 handle。
2. 在 `CreateNpcComponentByPostModel` 入口/出口记录目标路径、缓存命中与实际模型实例；泛型共享入口不能直接假定为单一具名方法，需先核对运行时 MethodInfo/ABI。
3. 关联代理完成与实例身份，确认资源何时首次可用，以及复制/绑定发生在哪一侧。

最终替换候选应是“首次资源完成、尚未被实例化或绑定消费”的交付边界；如果需要每实例替换，则选择克隆后、部件和骨骼绑定前的边界。目前还没有证据锁定唯一安全写入点，不直接在 LoadAsync 返回时替换。
