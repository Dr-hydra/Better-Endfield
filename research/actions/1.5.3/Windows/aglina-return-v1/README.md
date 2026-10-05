# 外部 Loop 回导研究 / Actions 1.9.2 已恢复 v9

**后续修复候选已转到 `../aglina-native-return-v2/README.md`（Actions 1.10.0 / v11）。** 新候选重建原生 AnimationClip、ACL 和 AssetBundle 结构；本目录旧标准对象包仍隔离。下方是前次失败与恢复记录，不代表 v11 当前部署状态。

## 最新实机结果：v10.1 在读取 AnimationClip 时崩溃

2026-09-09 22:51:37，专用归档包通过 LoadFromFile 后，在 LoadAsset 内触发 Unity 原生写访问违规。精确 DLL 反汇编确认调用位置，不是控制器替换或循环接缝。原版 Clip 比标准包增加 ACL、事件等自定义序列化字段，外层包转换不足以实现内部动画兼容；具体读错字段仍未确定。

已隔离 artifacts/stage 的候选包、将当前配置 `external_loop=false`，Actions 1.9.2 同时默认关闭外部加载。原动作开关恢复 v9。**不要执行下方历史试用步骤或重新放回候选包。** 后续先做原生动画结构写入和离线读回验证；粗修 FBX/Blender 保留。详情见 `v10.1-crash-20260909-225137/analysis.md`，其中保存转储、崩溃 DLL 和标准包 TypeTree。以下为历史记录。

## 当前候选修复：专用归档格式，尚待实机

已将测试目录和 stage 中的包替换为 `endfield-bundle/aglina_return_v1.bundle`（9,764,240 字节），DLL 更新为 1.9.1。旧标准 UnityFS 原件保留在 `bundle/`，更换前的部署备份路径记录于 `v10.1-backup-path.txt`。

转换器 `../AglinaBundleWrap/Program.cs` 将 UnityFS 的嵌入文件重新封装为当前 Endfield VFS 内层归档：48 字节头、encFlags=8、flags=0x240、无压缩目录/数据、128 KiB 数据块。此模式在原游戏包中已有；不改游戏 VFS、不改动画序列化字节、不修改引擎。不要把原 `bundle/` 标准包再次覆盖进模块。

发现现有 AnimeStudio `VFSUtils.ReadHeader` 的总长度公式有误：应为 `rol64(14)`，源码写为 `ror64(18)`；信息块位于前部时，其解码器不使用该字段，所以此前没有暴露。编码器使用正确逆运算，已对输入目录 209 个原包核验长度。没有修改工具链源码。新包通过现有独立 VFSFile 解码器读回，嵌入文件的名称、flags、内容逐字节一致；原游戏样本重新封装也通过回读。报告见 `endfield-bundle/wrap-report.json`。

1.9.1 日志分清 LoadFromFile 返回空/异常、归档已接受、左右 LoadAsset、Clip 属性与 pin 阶段。编译、三组 C++ 测试、75 项接口描述校验通过。部署与构建产物做逐字节比对，未计算哈希。

试用须完整重启游戏及启动器，继续使用原动作开关并触发特殊冲刺。先观察 `archive accepted by LoadFromFile`，再观察 `external return clip confirmed active` 和 `v10 imported loop wrap`；仅包头通过不代表动作已替换。新包内部仍为 2022.3.62f3 动画数据，而实际游戏引擎为 2021.3.34f5，下一次试播可能暴露序列化格式差异；不能将离线验证当作实机通过。后文保留 v10 初次交付记录。

## 最新实机结果：2026-09-09 13:24，加载失败

用户首次试用后确认回导未生效。13:22:56 日志已显示 v10 模块启动；13:24:19 记录 Bundle/Clip 校验失败并回退 v9，随后全部回绕为 `v9 normalized bob blend`。游戏 `Player.log` 明确报 `Unable to read header from archive file`，目标就是已部署的 `aglina_return_v1.bundle`，之后报 `Failed to read data for the AssetBundle`。失败发生在包头读取阶段，尚未进入动画替换或姿态播放。

已核对部署文件与构建文件逐字节一致（8,244,849 字节），因此不是少复制资源或误用旧 DLL。Player.log 同时明确显示引擎版本为 `2021.3.34f5 (0)`，这次不再只是根据资源头推测。当前外部包是标准 UnityFS / 2022.3.62f3，原游戏提取包头则是不同的二进制格式；现有证据不能把失败仅归因于 Unity 版本号，需进一步定位游戏定制包格式/加载入口的兼容要求。

证据摘录：`v10-first-game-test.log`。本次查日志未改动 DLL、动画或测试包。

2026-09-09，用户认可粗修视频后授权制作游戏回导版本。

## 已交付

测试包 `artifacts/BetterEndfield-win-x64` 已更新：

- `modules/BetterEndfield.Actions.dll`：1.9.0 / ABI 1。
- `modules/actions/aglina_return_v1.bundle`：左右回导 Humanoid Clip，8,244,849 字节。
- 原动作开关继续使用；不需要更换 UI。`external_loop` 配置默认 true，可手动设为 false 禁用回导实验。

旧模块和 manifest 备份在 `tmp_analysis/actions-before-v10-test-update-20260909-131805`。第一版、第二版和粗修 FBX/Blender 工程均保留。没有启动游戏或声称实机验收通过。

## 资源结构

`BE_Aglina_Return_L/R` 均为非 Legacy 的 Humanoid Clip，保持原版 3.466667 秒时长：

- 帧 0–40：原版起手。
- 帧 40–143：将粗修 Loop 的相位旋转到与原版帧 40 一致的位置，周期 103/60 秒。进入边界没有额外硬切。
- 帧 144–161：逐渐接回原版收尾；之后沿用原尾段。持续冲刺期间在 143 回到 40，不播放这段尾部。

原地预览移除的 Motion 平移按原 Avatar humanScale 还原后，交由 Unity 导入器提取 Humanoid 根运动；19 个原版音效/语音事件以实际秒数写回独立 AnimationClip。只打包动画，不把角色模型网格一起加载进游戏。

导入修正包括：原 Avatar 的 T Pose 和 22 个 Humanoid 映射、显式包含所有附属骨骼的导入 Mask、移除 FBX 多出的 `chr_0013_aglina_postmodel/` 路径前缀，仅保留原动画实际存在的附属 Transform 路径，避免 `Root` 行进曲线与 Humanoid 根运动重复应用。

## 运行时

首次目标冲刺时在游戏线程加载模块旁的 Bundle，验证左右 Clip 名称、Humanoid 标记和长度。基于当前控制器创建私有 AnimatorOverrideController，仅替换左右特殊冲刺 Clip；不改共享 Controller 或原资源。

只有 CurrentAnimatorClipInfo 确认实际播放新 Clip 后，才启用 40/208–143/208 的闭合窗口回绕。回绕使用已在 v9 核对的 8 参数 Animator.CrossFade 入口，混合时长为 0，保留 overshoot 补偿；随后必须观察到真实回绕，禁止假报成功。其他动作过渡优先。

停止、攻击、换人等退出仍交给游戏；原特殊状态完全离开后恢复原控制器，组件释放时也清理。只恢复仍属于本模块的 Controller，避免覆盖游戏随后换上的控制器。恢复调用异常时保留所有权供后续重试；工作线程关停不调用 Unity。v9 的道具、粒子和 Flying_Stop 生命周期保持继续沿用。

接口不支持、缺包、载入失败或新 Clip 未实际生效时使用 v9；安装后回滚尚未成功则结束保持，不能误把新 Clip 当作 v9 时间窗继续重播。

## 已完成验证

- 75 个方法/属性描述与当前本地 IL2CPP dump 核对。
- Release 编译，原动作策略测试、粒子恢复测试、新 Controller 所有权/恢复异常重试/100 次回绕策略测试通过。
- Unity 2022.3.62f3 构建 Bundle 并重新加载，两侧 Avatar humanScale 均等于原始 0.99172956。
- 在按原 Avatar 创建的骨架上通过 PlayableGraph 播放回导 Clip：循环端点最大世界位置差左约 0.000001213 米、右约 0.000000635 米。
- 开启根运动后，一轮前进约 12.0167 米，侧向累计位移小于 0.000002 米；没有把原地预览错误当作移动动画交付。
- 测试包 DLL / Bundle 已与构建输出逐字节核对，DLL 导出描述符为 1.9.0 / ABI 1。未另算产物哈希。

`unity-return-report.json`、`unity-return.log`、`native-build.log` 为证据。原资源头是 2021.3.34f5，不能仅凭本机编辑器验证保证游戏对 2022.3 构建资源的兼容性；此项和游戏自定义 Animator/根运动实现仍待实测。

## 游戏试用

用更新后的测试包启动游戏，开启原持续特殊冲刺开关，持续冲刺数秒，再停止、攻击、换人。重点观察实际动作、前进位移、粒子与退出是否正常。

日志 `%LOCALAPPDATA%/BetterEndfield/logs/BetterEndfield.log` 中应出现 `external Humanoid clips loaded`、`external return clip confirmed active`、`v10 imported loop wrap`；回绕约从 normalizedTime 0.688 到 0.192，退出后出现 `original controller restored`。若出现 `using v9 loop`，说明本轮并未使用回导动画。
