# v10.1 首次专用包试播崩溃

用户报告：2026-09-10。崩溃发生于 2026-09-09 22:51:37（转储目录时间为 UTC 14:51:37）。当前样本保存 Unity crash.dmp、Player.log、发生崩溃时的 Actions 1.9.1 DLL，以及禁用前的配置备份。

## 已确认的位置

- 宿主日志 22:50:34 显示 v10.1 启用。宿主异步日志没有保留下最后的加载阶段日志，因此不能根据缺少“archive accepted”判断包没加载。
- Unity 转储：异常线程 37188，`0xC0000005` 写访问违规，RIP `unityplayer+0xE97923`，目标地址 `0x45FEA7E000`。指令为 `vmovntdq [rcx+0x80], ymm0`，处于连续填充内存的循环中。
- 调用栈含 `BetterEndfield.Actions+0x6811`、`+0x595E`、`+0xA665`，之后是原生 `CharacterSpecialDashBrain.TryStartSpDash`。
- 对保存的精确 DLL 反汇编：`+0x6811` 是 `LoadExternalAssets` 的左右循环内 `Invoke(BundleAsset, ...)` 返回地址；调用前设置 `aglina_left/right` 名称和类型参数。`+0x595E` 是 `InstallExternal` 调用 `LoadExternalAssets` 后的位置。
- 该调用只会在 `LoadFromFile` 返回活着的 Bundle、且 Bundle GC pin 成功之后执行。因此这次已越过归档读取，崩溃在 `LoadAsset` 内部；尚未创建/安装本次私有控制器，更未进入循环接缝或粒子逻辑。

## 动画格式差异

用已有 AnimeStudio 的 SerializedFile 只读解析标准包的元数据/TypeTree，没有让游戏读取新包。结果保存在 `standard-clip-schema.json`。

原版 AnimationClip 顶层存在这些标准 2022 包没有的字段：`m_TransferCompressed`、`m_aclType`、`m_AclCompressedBuffer`、`m_ClipTag`、`m_TransitionRotateMode`、`m_TransitionRotateDirType`、`m_TotalSize`、`m_TransitionRotateCurveIndex`。Muscle 内 DenseClip 也扩展了 ACL 结构，AnimationEvent 增加 `hashCodeType`。原版数据依据 `aglina-controller-raw/CAB-7772877a4d28dd736840e8579433319c--242825731236656247.tree.json`；解析代码依据 `AnimeStudio/Classes/AnimationClip.cs`。

**结论边界：**确定了 LoadAsset 原生崩溃及 schema 不匹配，优先怀疑游戏读取非定制动画结构导致长度/偏移解释错误。尚无 Unity 私有符号，不能声称已经锁定读错的具体字段；不能仅凭填充内存处崩溃证明是哪一个数组。外层包可读、离线 FK/闭合验证通过，都不足以证明游戏 AnimationClip 序列化兼容。把版本号改为 2021 或单纯安装 2021 编辑器不能补齐游戏自定义字段。

## 已采取的恢复

1. 配置 `BetterEndfield.ini` 的动作模块增加 `external_loop=false`，保留 `enabled=true` 和其他用户设置。
2. artifacts 和 stage 的 `actions/aglina_return_v1.bundle` 重命名为 `.bundle.crash-disabled-20260910`，保留供离线分析。
3. Actions 1.9.2 默认 `external_loop=false`，原动作开关继续走 v9，启动日志明确标注外部导入关闭。显式试验配置入口保留，但本轮不再部署可自动加载的试验包。
4. 原生硬崩溃不会变成托管异常返回；不能靠现有 bool/exception 分支保证回退，也不尝试捕获访问违规后继续运行已损坏的 Unity 状态。

## 后续实现路径

优先以原版左右 Clip 的原生 TypeTree 和骨骼/绑定信息为模板，将已粗修的关键帧编码回游戏支持的轨道结构，并补齐 ACL、根运动、事件等字段。先完成离线写出→按游戏结构读回、绑定数量与索引范围校验、ACL 解码和姿态/根运动比较，再考虑新的实机加载测试。现阶段尚未实现该内部格式写入器；FBX、Blender 和粗修结果保留，不需要重做艺术编辑。
