# 原生格式回导候选 v11 / Actions 1.10.0

2026-09-10，针对 v10.1 在 LoadAsset 内崩溃重新制作。已更新 `artifacts/BetterEndfield-win-x64` 与 stage，当前本机动作配置 `external_loop=true`，新配置默认值仍为 false。**本轮没有启动游戏，实机接受与播放仍待验证。**

测试目录的启动器也已更新，修复整体保存/动作开关保存时丢弃隐藏 external_loop 设置的问题。保留显式 true 或 false，不覆盖未来明确提供的新值；不从其他 INI 节误读同名键。独立配置回归测试和 WinUI 发布通过，旧启动器一并备份。DLL、Bundle、启动器均与构建产物逐字节比较，版本/ABI 和无关配置保留验证见 `deployment-verification.json`。

## 本次修复

不再把标准 Unity 2022 的 AnimationClip/AssetBundle 对象送进游戏。以原游戏左右 Clip 的完整 TypeTree、2021.3.34f5 序列化文件和原生 AssetBundle 为模板，仅更新必要数据：

- 粗修骨架经原 Avatar 的 HumanPoseHandler 逆投影回原 Humanoid 通道；原版有绑定的 321/320 条附属 Transform 直接取粗修数据。
- 编码为与原包一致的 ACL 2.1 / 数据版本 10、QVV 旋转/平移/缩放格式 3/1/1，缓冲区按原版对齐到 16 字节。不生成外部数据库或剥离关键帧。
- 完整保留原曲线绑定、子轨道 mask、常量表、事件扩展字段、ClipTag 等定制结构。保持原名对应的左右语义，调试名改为 `BE_Aglina_Return_L/R`。m_TotalSize 按三段 ACL 缓冲区长度变化更新，其余结构/容量保持原版。
- 保留原起手与尾段、208/60 秒长度、40–143 帧闭合窗口、原 Motion 轨道和每侧 19 个事件；RootMotion 缓冲区保持与 Float 前 28 通道一致。
- 使用原生 AssetBundle 的 m_PathFlags、m_HashContainer、m_PreloadTable 等结构；合并左右对象、更新对象表/长度/偏移，按 PathID 排序。原始字符串键与资源 ID 对保留，数字目录按 ID 排序。采用独立 CAB 名称和 Bundle 名称，避免与原资源身份相同。
- 模块使用游戏存在的 `LoadAsset(Int64, Type)`，按原生目录中的左右资源 ID 取 Clip；继续验证名称、人形标记、长度和真实播放。只读取新文件 `actions/aglina_native_return_v2.bundle`，不再读取已隔离的旧包。

Unity 2022 这次只用作离线 HumanPose 数值计算，交付包的内部 TypeTree、序列化版本与对象布局来自原游戏，不是标准 2022 编辑器导出结构。

## 验证结果

- 新写入器对原版左右 AnimationClip（74）及 AssetBundle（142）四个对象原样回写，全部逐字节一致。
- 最终 Endfield 归档独立解码后，内部序列化文件与封装前逐字节一致；两个 Clip 均通过游戏专用 AnimationClip 读取器和 TypeTree 读取器，均完整消费对象字节。原生 AssetBundle 目录前缀与完整 TypeTree 均读回通过。
- Native 目录 ID → preload → PathID → Clip 名称链路一致；所有未编辑定制字段和 19 个事件与原版相同。
- ACL 重编码再解码确认版本、60 Hz、209 样本、轨道数、索引、常量及四肢四元数范围。每侧前进 Motion 数值读回与原解码样本相同，闭合窗口前进约 12.016709 米。
- 原 Avatar 上，编码前骨骼重放最大位置误差约 0.0054 毫米；最终 ACL 解码重放最大误差左 0.264 毫米、右 0.212 毫米；循环端点位置差左 0.0395 毫米、右 0.0131 毫米。
- 最大附属局部旋转压缩差约 0.032 度。原版 drop-W 四元数格式在 W 接近零时会放大浮点舍入，因此同时验证了最终世界空间误差，没有仅看组件误差判断。
- 三组 C++ 测试、75 项当前游戏接口描述校验通过；新增测试覆盖 Int64 资源 ID 的传参、左右选择、异常及无效侧。最终 DLL 仅统一日志版本标签后重新编译。

主要报告：`final-validation.json`、`native-encode-report.json`、`native-pose-report.json`、`native-pose-decoded-report.json`、`bundle/wrap-report.json`。最终包 1,107,500 字节。

### 验证的边界

离线骨架重放没有执行游戏 IK、布料、特效或完整状态机。手脚目标轨道使用原目标数据在同一周期窗口做连续处理；没有在不确定坐标约定的情况下反推覆盖这些目标。原版目标不一定等于 FK 手脚位置，不能把骨架误差当作 IK 效果验证。

现有 AnimeStudio 归档读取器有已确认的总长度旋转公式问题，封装器使用经 209 个原包验证的正确公式；现有读取器的整体长度字段未用来替代本地长度验证。不得将其合成的版本字符串当作真实游戏版本。

## 实机测试及日志

完整重启启动器和游戏，使用原动作开关触发艾洁莉娜特殊冲刺。当前测试配置已启用本次外部回导。成功至少需要依次看到：

1. `Aglina v11: archive accepted by LoadFromFile`
2. 左右 `LoadAsset returned` 及 Clip 属性验证、`native Humanoid clips loaded`
3. `external return clip confirmed active`
4. `v11 imported loop wrap`

加载期间另有即时写盘日志：`artifacts/BetterEndfield-win-x64/modules/actions/aglina_native_return_v2.load.log`。每个加载入口前后 WriteFile + FlushFileBuffers，避免宿主缓冲日志在原生崩溃时丢失最后阶段。只在一次加载流程中写少量记录，不在逐帧循环中写盘。Unity 原生访问违规依然不能靠托管异常回退捕获。

备份路径见 `backup-path.txt`。若需要停用候选，将用户配置动作节的 `external_loop` 设为 false，原开关回到 v9。旧崩溃包继续保持隔离，不覆盖粗修源文件。

## 复现入口

`AglinaNativeClip extract` → `native_clip_codec.py` 原样验证 → `prepare_native_return.py` → Unity `AglinaNativePose.Run` → `encode_native_return.py` → Unity `AglinaNativePose.Run -aglinaDecoded` → `package_native_return.py` → `AglinaBundleWrap` → `AglinaNativeClip verifybundle`。

原始提取输入是 `aglina-animation-inputs/Bundles/Windows/main/e1d1cdbf5f2ff6b856a9ffdb.ab` 和 `7e26489f4608426164b5a007.ab`。没有修改游戏资源文件、绕过引擎校验或计算交付文件哈希；ACL 编码器自身写入格式要求的内部校验字段。
