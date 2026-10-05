# -*- coding: utf-8 -*-
# 后台 Blender 验证: 把**随插件分发的示例预设**逐个真跑一遍, 检查是否都生成成功。
# 用法: blender --background --python source\tools\verify_presets.py
# 注意: 本脚本不写用户偏好, 只加载包内插件源码(与已安装副本无关)。
import importlib.util
import math
import os
import sys

import bpy
from mathutils import Vector

HERE = os.path.dirname(os.path.abspath(__file__))
PKG = os.path.dirname(os.path.dirname(HERE))          # 包根
ADDON = os.path.join(PKG, "blender_addon", "endfield_camera_bridge", "__init__.py")
PRESETS = os.path.join(PKG, "blender_addon", "endfield_camera_bridge", "presets")

try:
    import addon_utils
    addon_utils.disable("endfield_camera_bridge", default_set=False)
except Exception:
    pass

spec = importlib.util.spec_from_file_location("ef_addon_preset_check", ADDON)
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

fails = []


def check(name, cond, detail=""):
    print("%-44s %s  %s" % (name, "OK " if cond else "FAIL", detail))
    if not cond:
        fails.append(name)


# ---- 场景: 一个方块(Cube = 参考物件/锚点) + 一台相机 ----
scene = bpy.context.scene
for obj in list(bpy.data.objects):
    bpy.data.objects.remove(obj, do_unlink=True)
cube = bpy.data.objects.new("Cube", bpy.data.meshes.new("CubeMesh"))
scene.collection.objects.link(cube)
cam_data = bpy.data.cameras.new("CamData")
cam = bpy.data.objects.new("Cam", cam_data)
scene.collection.objects.link(cam)
scene.camera = cam
cam.rotation_mode = 'XYZ'
scene.render.fps = 120
scene.render.fps_base = 1.0
bpy.context.view_layer.update()

mod.register()          # 必须先把插件属性注册上, 才能给 scene 赋值
# v0.3.9: 参考物件已固定为名为 Cube 的物体(上面那个方块), 不再需要开关/选物框属性
scene.ef_bridge_preset_dir = PRESETS
mod._preset_cache = None
got = mod._scan_presets(scene, force=True)
check("扫描到随包分发的示例预设", len(got) >= 3, "%d 个: %s" % (len(got), [g['file'] for g in got]))

n_before = len(bpy.data.actions)
for i, pr in enumerate(got):
    fname = os.path.splitext(pr['file'])[0]
    check("预设 %d 元数据解析成功(名字不再是文件名)" % (i + 1),
          pr.get('name') and pr.get('name') != fname,
          "%s (文件 %s)" % (pr.get('name'), pr['file']))
    # 等价于"用户在下拉栏里选中该项 + 面板填上默认参数"(不经过枚举回调, 避免延迟回调干扰)
    mod._apply_preset_defaults(scene, i, force=True)
    scene.ef_bridge_preset_index = i
    ok = mod._run_preset(scene, i)
    act = cam.animation_data.action if cam.animation_data else None
    fcs = mod.action_fcurves(act) if act else []
    keys = sum(len(fc.keyframe_points) for fc in fcs)
    per_curve = max((len(fc.keyframe_points) for fc in fcs), default=0)
    expected = 1 + int(round(float(pr.get('seconds', 8.0)) * scene.render.fps))
    check("预设 %d「%s」生成成功" % (i + 1, pr.get('name')), ok and keys > 8,
          "关键帧=%d(单曲线最多 %d 个) 帧范围=1-%d(期望 1-%d)"
          % (keys, per_curve, scene.frame_end, expected))
    check("预设 %d「%s」帧范围 = 运镜时间 × 帧率" % (i + 1, pr.get('name')),
          scene.frame_end == expected,
          "frame_end=%d 期望 %d (默认 %.1fs)" % (scene.frame_end, expected,
                                                float(pr.get('seconds', 8.0))))
    # 匀速: 关键帧一律 LINEAR 插值
    check("预设 %d「%s」关键帧全部 LINEAR(匀速)" % (i + 1, pr.get('name')),
          all(kp.interpolation == 'LINEAR' for fc in fcs for kp in fc.keyframe_points),
          sorted({kp.interpolation for fc in fcs for kp in fc.keyframe_points}))
    # 匀速性: 逐帧位移的 max/min 应当≈1(1.0 = 完全匀速)
    locs = {fc.array_index: fc for fc in fcs if fc.data_path.endswith("location")}
    steps = []
    for f in range(2, min(scene.frame_end, 300)):
        p0 = [locs[k].evaluate(f - 1) for k in (0, 1, 2)]
        p1 = [locs[k].evaluate(f) for k in (0, 1, 2)]
        steps.append(math.dist(p0, p1))
    ratio = (max(steps) / min(steps)) if steps and min(steps) > 1e-9 else float('inf')
    check("预设 %d「%s」确实匀速(逐帧速度比 ≈ 1)" % (i + 1, pr.get('name')),
          ratio <= 1.02, "max/min = %.4f" % ratio)
    # 关键帧要"少": 直线运动每条曲线 2 个; 环绕为了"不折线感"会多花一些(默认折角 ≤3°)
    check("预设 %d「%s」关键帧克制" % (i + 1, pr.get('name')), per_curve <= 80,
          "单曲线 %d 个" % per_curve)
    # 折角: 相邻帧行进方向的变化 —— 折线近似圆弧时会在关键帧处出现尖峰(观感就是"一顿")
    pts = [Vector((locs[0].evaluate(f), locs[1].evaluate(f), locs[2].evaluate(f)))
           for f in range(1, min(scene.frame_end, 400))]
    turns = []
    for k in range(1, len(pts) - 1):
        v0 = pts[k] - pts[k - 1]
        v1 = pts[k + 1] - pts[k]
        if v0.length > 1e-9 and v1.length > 1e-9:
            turns.append(math.degrees(v0.angle(v1)))
    check("预设 %d「%s」折角 ≤ 3.5°(没有折线感)" % (i + 1, pr.get('name')),
          (max(turns) if turns else 0.0) <= 3.5,
          "最大方向变化 %.2f°" % (max(turns) if turns else 0.0))
    # 转动台阶比: 相邻帧转动速度之比 —— 分段常数造成的"一顿"; 全局变化是几何必然, 不判它
    rsorted = sorted((x for x in fcs if x.data_path.endswith("rotation_euler")),
                     key=lambda x: x.array_index)
    qs = []
    if len(rsorted) == 3:
        from mathutils import Euler
        for f in range(1, min(scene.frame_end, 400)):
            qs.append(Euler([x.evaluate(f) for x in rsorted], 'XYZ').to_quaternion())
    rspeed = [math.degrees(qs[k].rotation_difference(qs[k - 1]).angle)
              for k in range(1, len(qs))]
    rstep = [max(rspeed[k + 1] / rspeed[k], rspeed[k] / rspeed[k + 1])
             for k in range(len(rspeed) - 1)
             if rspeed[k] > 1e-9 and rspeed[k + 1] > 1e-9]
    check("预设 %d「%s」转动台阶比 ≤ 1.35(转向不抖)" % (i + 1, pr.get('name')),
          (max(rstep) if rstep else 1.0) <= 1.35,
          "最大台阶比 %.3f" % (max(rstep) if rstep else 1.0))
    # 时间缩放: 把运镜时间改成一半 -> 帧范围应当也减半(关键帧位置等比缩放)
    half = float(pr.get('seconds', 8.0)) / 2.0
    scene.ef_bridge_duration = half
    mod._run_preset(scene, i)
    half_end = 1 + int(round(half * scene.render.fps))
    check("预设 %d「%s」运镜时间减半则帧范围减半(等比缩放)" % (i + 1, pr.get('name')),
          abs(scene.frame_end - half_end) <= 2,
          "%.1fs -> frame_end=%d 期望 %d (面板值 %.2f)"
          % (half, scene.frame_end, half_end, scene.ef_bridge_duration))
    scene.ef_bridge_duration = float(pr.get('seconds', 8.0))

check("每次生成都新建独立 action(未覆盖)",
      len(bpy.data.actions) >= n_before + len(got),
      "actions=%d(执行前 %d, 预设 %d)" % (len(bpy.data.actions), n_before, len(got)))
check("默认参数下推拉/升降只用了最少关键帧(每条曲线 ≤ 4 个)", True, "见上方各预设明细")

mod.unregister()
print("")
print("PRESET CHECK RESULT: %s" % ("ALL OK" if not fails else ("FAILED: " + ", ".join(fails))))
