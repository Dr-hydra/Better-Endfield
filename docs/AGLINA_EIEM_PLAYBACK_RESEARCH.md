# EIEM 对艾洁莉娜外部循环播放的参考

2026-09-10。用户反馈 v11 仍不行，并要求先不要回滚、不要持续反复验证，转而研究 https://github.com/Sasye/EIEM 。本轮只读取源码和已有数据、记录结论；没有编译、试播、修改配置或替换部署文件。

源码快照：`4f0b13f92cf43d14baf696794aa5b2d285aed26a`，提交日期 2026-09-01。只读研究副本位于 `tmp_analysis/EIEM`，未运行该项目的程序、安装器或构建脚本。

## 结论

EIEM 有直接帮助，主要价值是运行时姿态播放与游戏特殊接口的处理。它并未提供可直接修复我们 AssetBundle 加载问题的打包器；已查看的 MUS4 与 DirectVmd 播放链路没有把外部动作交给 AssetBundle.LoadAsset 反序列化为 AnimationClip。

### MUS4 路径

`ExportMuscleAnimation.cs` 在 Unity 中取 HumanPose；`src/muscle_player.h` 读取自定义二进制，帧内容为 body position、body rotation、95 个标准 muscle，以及可选的手臂/手指旋转、表情权重。`GetFrame` 对采样做插值，播放时间由播放器控制。

`src/animation.h:639` 的 `InitMusclePoseHandler` 使用当前角色 Avatar 和根 Transform 创建 HumanPoseHandler，并持有托管对象/数组。`src/trojan.h:3828` 的 `MuscleAnimationTick` 负责采样，通过窗口消息把姿态应用转到主线程；`ApplyMmdPoseOnMainThread` 才进行游戏对象写入。

游戏中的 SetHumanPose 接口有裁剪/入口解析问题。`src/init.h:668` 起尝试内部 icall；约 727–817 行还通过 GetHumanPose 包装函数定位缓存槽和相应 setter。`src/trojan.h:1311` 的实际应用分支会临时切换调用槽来执行 setter。源码同时保留了 GetInternalAvatarPose hook 等路径，不能把所有历史/备用实现都当成当前主流程，也不能把普通 Unity 的公开 SetHumanPose 签名直接视为游戏里可调用。

### DirectVmd 路径

当前 README 的默认模式是 DirectVmd。`src/direct_vmd_runtime.h` 维护采样和角色/代次信息；`src/ghost_rig.h:3347` 把采样姿态写到参考骨架，`GhostRig_ApplyPhase3TargetFk`（4598）把姿态应用到目标角色，其他阶段处理手指、twist、眼睛等。`GhostRig_BeforeFinalIK`（7625）按主线程、角色归属、场景/代次和帧号约束提交顺序，并与 FinalIK 协同。

这条思路适合我们已有的逐帧骨骼数据：不必为了播放已经烘焙的 Transform 再生成 VMD，也不必照搬完整 GhostRig；应借鉴采样、写入时机、归属控制和恢复机制。

## 发现我们现有代码的一项实质问题：95 与 101 通道

EIEM 的 `src/trojan.h:21` 明确提供标准 95 通道到游戏扩展通道的兜底映射：

| 标准 muscle 索引 | EIEM 兜底游戏索引 |
| --- | --- |
| 0–28 | 原索引 |
| 29–36 | 原索引 + 3 |
| 37–94 | 原索引 + 6 |

实际运行还通过 `BuildDynamicMuscleMap`（130）读取 `HumanTrait.MuscleName` 按名称映射；`InitMmdPoseOnMainThread`（528）取游戏返回数组的真实长度，`ApplyMmdPoseOnMainThread`（1459 附近）按映射写值。因此这张表是 EIEM 的兜底，不能替代对当前游戏名称的正确对应。

本地已保存的原生左 Clip 中，`m_MuscleClip.m_DeltaPose.m_DoFArray` 长度为 61，左右 `m_HandPose.m_DoFArray` 各为 20，共 101；原生 Humanoid 属性还包含 42–142 这 101 个连续索引，与扩展通道结构吻合。

而我们的代码：

- `tmp_analysis/aglina-fbx-validation/UnityBake/Assets/Editor/AglinaBake.cs:53` 从原生 Float 的偏移 42 连续复制到标准 HumanPose muscle 数组。
- `tmp_analysis/aglina-fbx-validation/UnityBake/Assets/Editor/AglinaNativePose.cs:42` 将标准 muscle 直接写回原生 `42+muscle`；第 45 行又按相同假设读回。

这套恒等映射需要修正；我们此前将标准 95 之后的额外部分视为其他通道的解释不可靠。前面的微小离线误差是相同假设下的往返自洽，不能证明原生 muscle 的语义对应正确，也不能证明预览忠实还原了游戏原动作。该问题足以影响姿态；但不应凭它就断言是本次 LoadAsset 失败/原生崩溃的唯一原因，本轮没有继续诊断新一次失败的具体栈。

## 对现有目标的建议

建议将后续实现重点改为“原生动作状态继续运行，特殊冲刺过程段覆盖视觉姿态”。已有粗修帧和按路径关联的骨骼数据可以保留，但涉及 Humanoid 的原生→标准→原生通道对应需纠正。

1. 保留本模块对特殊冲刺状态、移动、退出命令、道具、粒子和事件生命周期的管理。
2. 以简单的姿态帧文件驱动独立循环时钟，在目标角色的动画计算后、合适的 IK/布料阶段提交姿态。优先利用已有骨骼轨道；如使用 HumanPose，需要解决 95↔101 映射及实际可调用入口。
3. 只在明确的特殊冲刺过程窗口接管，起手和收尾仍衔接原动作；退出时让覆盖权重退到原生姿态，避免另一个硬切。区分人物世界移动与骨架内部起伏，避免前进位移施加两次。
4. EIEM 为完整舞蹈播放会关闭目标 Animator，并改变 IK/角色移动行为；我们需要普通操作仍可打断冲刺，因此不能整套照搬这些控制策略。保留 Animator 后在正确阶段覆盖姿态是待实现的适配方案，不是 EIEM 已替我们证明可用的特殊冲刺方案。

这轮到源码结论为止，不再追加打包、编码、循环测试或部署。后续若落实新播放方式，应围绕这条运行时管线开展工作，避免继续将同一套离线读回结果当作游戏兼容证据。

## 源码入口

- [MUS4 帧与读取器](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/muscle_player.h#L14)
- [标准到游戏的 muscle 映射](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/trojan.h#L21)
- [主线程姿态应用](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/trojan.h#L1311)
- [内部接口解析](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/init.h#L668)
- [目标角色 FK 应用](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/ghost_rig.h#L4598)
- [FinalIK 前的写入调度](https://github.com/Sasye/EIEM/blob/4f0b13f92cf43d14baf696794aa5b2d285aed26a/src/ghost_rig.h#L7625)
