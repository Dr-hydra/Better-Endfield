# 通用模型匹配、异步构建与第一人称实现（2026-10-03）

本轮已经实现生产行为，并在 Windows/Android 编译、离线回归及覆盖部署后交付。基线为 main `dd7131b4` 加本会话已有工作区改动；没有提交、推送或新增开发分支，保留用户已有设置。UI 未新增说明文字。

## 已实现

- 通用定位以真实资源根、接收者路径、实际 sharedMesh 或保存的 Original 身份筛选，Renderer 名与 Mesh 名分别处理。保留唯一性、布局、骨骼、材质、索引与 LOD/proxy 验证，移除艾尔黛拉逐角色 if；缓存已经改写的 Mesh 不误当 pristine。
- Finish 只登记受控交付，并继续游戏原调用一次。已确认的 Unity Canvas 帧泵通过 `Time.frameCount` 驱动持久构建任务；后台队列纯 CPU 解压，最多两个 worker，大包独占；全部角色/world/UI 共用预算。
- Texture 构造、raw 数据写入、Apply 分不同帧步骤。默认共同预算为每帧 32MiB、一个重操作、软 2ms；单个大操作允许独占并追加 cooldown。当前可见接收者优先，同时轮转其它已登记需求、取消过期 revision、对克隆和 world/详情定向补绑。
- 兼容的存活 Mesh/Texture 成品跨事务复用，实例自己的 bones 和可变材质重新绑定。纯 Morph 更改继续复用不变贴图。闲置成品共同预算 256MiB、TTL 10 秒，淘汰只撤额外持有，不 Destroy 已发布/借用/游戏原资产。
- 第一人称新增具名托管 Mesh 后备：直接 icall 不齐时读取描述、复制私有网格、写入并读回索引。完整源几何继续用于投影，原网格保持；退出/切人/外部接管沿既有恢复规则。
- 骨骼表覆盖 32 个已采主骨架，另补噗切娜 LOD0；6 个角色额外帽链按用户最新“FP 观感优先”授权启用，标记名称/路径推定。模型 ID/骨骼路径映射当前 palette，不保存固定 bone index。
- 脸/头发若 palette 只有 Head/Neck，允许完整隐藏。混合部件采用当前非零权重及 Neck→Head 空间范围，头颈贡献至少 0.5 时允许额外隐藏局部；不会缩放骨骼，完整投影保留。未知模型继续通用处理。

## 验证

- Windows Host、全部生产模块与 UI publish 成功；Android arm64 Release APK 和 lint 成功。
- 生产翻译单元的第一人称生命周期、头饰/投影、托管 Mesh 后备 3 项 CTest 通过；CPU 几何/重试回归通过。
- 原生 parser、rollback、hot switch、CPU bridge 回归通过；生产 Finish/pump 异步测试通过，覆盖不同帧贴图步骤、UI 成品命中、Morph 贴图复用、四角色公平、取消、TTL 和实际绑定回写。
- 通用定位独立 67 项、Android world 162 项检查通过；后台调度 55 项及 BEM 容量/形变 3 项 CTest 通过。
- 骨骼表 helper 2163 项、5 项资料检查和 25 个实际 Mesh raw VB/IB 回归通过；静态样本提弗洛斯 face/hair 额外隐藏 118/124 个边界面，不能当手机视觉验收。

## 部署

Android：`artifacts/BetterEndfield-Android-3.4.3-20261003-async-profiles.apk`，10,452,749 字节，`adb install -r` 成功。

Windows：已覆盖 `E:/Better Endfield` 的 UI、Host、全部生产模块/overlay 与 loader/payload 共 16 个二进制；仅复制相关编译产物，未清空目录或覆盖用户配置。

本轮没有主动启动游戏或点击界面。游戏需正常重新启动以加载更新后的注入代码。

## 实际限制

队列支持已登记的最多四角色需求及一个 UI 构建者，并不声称取得完整配队快照并提前发现所有未请求角色。当前仅覆盖已有交付与两个标准 clone 入口，额外 pool/clone 路由仍需现场确认。

首次未 Ready 时游戏正常交付原版，随后定向补绑；这不是强制扣住所有原生完成回调。单张 64MiB Apply 仍不可中断并可能长帧。大包目前是独占整包解压，并非真正 payload 流式解码。

构建与 Host 模拟回归不能证明实际 GPU 峰值降低到固定数字，也不能证明 HG 双端影子和所有帽饰完全隐藏。用户仍需实机核验提弗洛斯死库水、其它帽饰、四角色切换、反复开详情及新旧包切换；后续以每帧提交统计和资源命中记录核对效果。

更细的文件/接口交接见 `native/modules/custom_model/model_runtime_notes.md`、`native/tests/async_loading/README.md`、`docs/camera/FIRST_PERSON_PROFILES.md`。
