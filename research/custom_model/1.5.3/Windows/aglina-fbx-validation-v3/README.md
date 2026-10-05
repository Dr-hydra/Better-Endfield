# 第三版完整动作烘焙：修正 101 通道 muscle 映射

2026-09-13。用户反馈 v12 试播中“腿一直翘着”，定位为烘焙阶段的 muscle 通道错位，不是运行时导入问题。本目录是修正后的左右完整 209 帧重建，作为后续循环细修的唯一输入。

## 错误原因

原生 Endfield Humanoid Clip 的 Float 流每帧 151 个值，第 42 位起是 **101** 个 muscle（61 身体 + 左右手各 20）。旧脚本 `AglinaBake.cs` 把第 42 位起的 95 个值按恒等索引复制进标准 Unity 的 95 个 muscle。实际布局（与 EIEM 的兜底表一致，并由本次数据统计验证）：

| 标准索引 | 原生索引 | 说明 |
|---|---|---|
| 0–28 | 同 | 脊柱、头、眼、下颌、左腿 |
| 29–36 | +3 | 右腿；原生 29–31 是三路近常量的额外通道 |
| 37–94 | +6 | 双臂、手指；原生 40–42 是另外三路额外通道 |

恒等复制的后果：右大腿被三路近零的额外通道驱动、整段循环不动（局部旋转变化 0.0°），右膝接到大腿的前后摆动，左臂全部通道近零而僵直，右臂通道整体错位。旧版“手腕 muscle 往返差 4.5”实际是原生左肩通道（m43/m44）被当成手腕后超范围截断，本身就是错位信号。

## 本版做法

- 新脚本 `aglina-fbx-validation/UnityBake/Assets/Editor/AglinaBakeV2.cs`：按上表取值，六路额外通道忽略并记录统计（`unity-bake-report.json` 的 `ignoredNativeChannels`，均值绝对值都小于 0.01）。其余重建方式与第二版相同（原 Avatar、HumanPoseHandler、附属 Transform 轨道叠加、去除 XZ 行进）。
- `bake-input.json`、`avatar.json`、`hierarchy.json` 与第二版相同。

## 验证

| 检查 | 旧（v2） | 新（v3） |
|---|---:|---:|
| 映射 muscle 往返最大差 | 4.50 | 0.00004 |
| 右大腿局部旋转变化幅度（左 Clip） | 0.0° | 97.1° |
| 左上臂 / 左前臂变化幅度（左 Clip） | 0.0° / 0.0° | 89.3° / 64.3° |
| 四末端（双脚双手）两两距离：FK 与 Clip 自带 IK 目标点的平均差 | 0.232 m | 0.076 m |
| 其中双手一对的最大差 | 0.511 m | 0.009 m |
| FBX→Blender 骨骼世界位置最大转换误差 | — | 约 0.0000016 m |

IK 目标点是 Clip 里独立存储的通道，不经过 muscle 映射，因此这项比较是独立于映射假设的校验。脚部一对仍有约 0.29 m 的最大差，可能来自 Unity 脚部目标点的定义偏移，未进一步核实。

## 查看

- `aglina_mapping_fix_comparison.mp4`：左 Clip 完整 209 帧，左边旧映射、右边新映射并排。
- `mapping_fix_041/099/137.png`：谷底、峰值、第二谷底三帧的并排截图。
- `renders/`：左右各十个时刻的单独渲染；`aglina_spdash_{left,right}_review.blend`、`aglina_spdash_{left,right}_full.fbx`。

预览仍不含游戏实时 IK、物理、材质、道具。复现：`prepare_aglina_bake.py`（输入不变）→ Unity 批处理运行 `AglinaBakeV2.Run` → `AglinaFbxProbe --baked` → `validate_aglina_fbx_blender.py` → `render_aglina_mapping_fix.py`。
