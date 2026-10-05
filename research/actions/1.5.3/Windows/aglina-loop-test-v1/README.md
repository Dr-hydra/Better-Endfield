# 特殊冲刺 Loop 粗修验证版

2026-09-09。用户授权粗修关键帧，先验证外部 Loop 路线，不做精细美术修整。

## 查看

- `aglina_loop_before_after_4cycles.mp4`：左侧动作的直接裁剪 / 粗修后对照，连续四轮，约 6.87 秒。画面左边 BEFORE，右边 AFTER。
- `aglina_loop_before_after_4cycles.blend`：同一对照工程。
- `aglina_spdash_left_loop_test.blend`、`aglina_spdash_right_loop_test.blend`：左右独立循环工程。
- `aglina_spdash_left_loop_test.fbx`、`aglina_spdash_right_loop_test.fbx`：烘焙后的左右 Loop 关键帧。
- `*_uncorrected.fbx` 仅供比较原始裁剪接缝，不是修好版本。

## 修了什么

以第二版已验证的骨骼动画为输入，取源采样帧 **22–125**（约 0.367–2.083 秒），周期 **103/60 = 1.716667 秒**。文件含 104 个关键时刻：第 104 个是与第 1 个相同姿态的闭合端点。Blender 的 Cycles 修改器按 103 帧周期重复，避免重复端点带来额外停顿。

首尾各 18 帧（0.3 秒）粗修，共组成跨循环边界的 0.6 秒连续过渡。位置和缩放使用三次 Hermite 曲线，旋转使用最短弧球面三次 Bezier；边界控制参考相邻原始帧的速度。此过渡已烘焙成实际骨骼关键帧，不依赖游戏运行时双混。

身体、手脚及头发/衣摆等附属骨骼一起闭合；源帧 **41–106** 的内部动作保持原姿态。原完整动画和第二版工程未覆盖。本轮未精修手脚接触、衣摆穿插或道具约束。

## 验证

左右各 400 条骨骼 Transform 轨道、10 个 LOD0 网格。

- 烘焙数据首尾位置、旋转闭合；没有非有限值或非正缩放。
- 104 个采样时刻的 FBX → Blender 骨骼位置转换最大误差：左约 0.00152 毫米，右约 0.00180 毫米。
- Blender 首尾骨骼位置差和抽查至第 4 轮的对应姿态位置差均为 0。
- 已实际渲染连续四轮和接缝前后画面。对照图中直接裁剪版本有身体/腿部硬跳，粗修版本跨接缝连续；这不等于精细自然度或游戏内验收。

预览继承第二版限制：无游戏实时 IK/物理、原版材质、特效、道具；采用去掉行进参考的原地动作。FBX 提供闭合关键帧，未来游戏导入时仍需配置循环播放和衔接入口。本轮没有修改运行时 DLL 或执行游戏回导。

证据：`loop-edit-report.json`、`blender-loop-validation.json`、`seam_102.png` 至 `seam_106.png`、`blender-preview.log`。复现脚本为上级 `make_aglina_loop_test.py`、`AglinaFbxProbe`、`preview_aglina_loop_test.py`。
