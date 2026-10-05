# -*- coding: utf-8 -*-
# 运镜体检: 逐帧测量生成出来的曲线, 找"速度/角度跳变"。
#
# 输出三组指标(逐帧):
#   · 线速度 |Δp|          —— 匀速运镜应当是常数
#   · 行进方向变化角        —— 折线近似圆弧时, 会在**关键帧处**出现尖峰(越小越顺)
#   · 转动角速度 |Δ∠|      —— 镜头姿态变化的每帧角度, 尖峰=转得一顿一顿
#
# 用法: blender --background --python source\tools\diag_preset_motion.py
import importlib.util
import math
import os

import bpy
from mathutils import Vector

HERE = os.path.dirname(os.path.abspath(__file__))
PKG = os.path.dirname(os.path.dirname(HERE))
ADDON = os.path.join(PKG, "blender_addon", "endfield_camera_bridge", "__init__.py")
PRESETS = os.path.join(PKG, "blender_addon", "endfield_camera_bridge", "presets")

try:
    import addon_utils
    addon_utils.disable("endfield_camera_bridge", default_set=False)
except Exception:
    pass

spec = importlib.util.spec_from_file_location("ef_motion_diag", ADDON)
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

scene = bpy.context.scene
for obj in list(bpy.data.objects):
    bpy.data.objects.remove(obj, do_unlink=True)
cube = bpy.data.objects.new("Cube", bpy.data.meshes.new("M"))
scene.collection.objects.link(cube)
cam = bpy.data.objects.new("Cam", bpy.data.cameras.new("C"))
scene.collection.objects.link(cam)
scene.camera = cam
cam.rotation_mode = 'XYZ'
scene.render.fps = 120
scene.render.fps_base = 1.0
mod.register()
# v0.3.9: 参考物件已固定为名为 Cube 的物体(上面那个方块), 不再需要开关/选物框属性
scene.ef_bridge_preset_dir = PRESETS
scene.ef_bridge_duration = 8.0


def sample(fcs, frame):
    loc = {}
    rot = {}
    for fc in fcs:
        if fc.data_path.endswith("location"):
            loc[fc.array_index] = fc.evaluate(frame)
        elif fc.data_path.endswith("rotation_euler"):
            rot[fc.array_index] = fc.evaluate(frame)
    p = Vector((loc.get(0, 0.0), loc.get(1, 0.0), loc.get(2, 0.0)))
    e = (rot.get(0, 0.0), rot.get(1, 0.0), rot.get(2, 0.0))
    from mathutils import Euler
    return p, Euler(e, 'XYZ')


def diag(idx, label):
    got = mod._scan_presets(scene, force=True)
    mod._apply_preset_defaults(scene, idx, force=True)
    scene.ef_bridge_preset_index = idx
    mod._run_preset(scene, idx)
    act = cam.animation_data.action
    fcs = mod.action_fcurves(act)
    end = scene.frame_end
    keys = sorted({int(round(kp.co[0])) for fc in fcs for kp in fc.keyframe_points})
    ps, qs = [], []
    for f in range(1, end + 1):
        p, e = sample(fcs, f)
        ps.append(p)
        qs.append(e.to_quaternion())
    speeds, turns, rots = [], [], []
    for i in range(1, len(ps)):
        v0 = ps[i] - ps[i - 1]
        speeds.append(v0.length)
        if i >= 2:
            v1 = ps[i - 1] - ps[i - 2]
            if v0.length > 1e-9 and v1.length > 1e-9:
                turns.append(math.degrees(v0.angle(v1)))
        rots.append(math.degrees(qs[i].rotation_difference(qs[i - 1]).angle))
    def stat(v):
        return (min(v), max(v), sum(v) / len(v)) if v else (0, 0, 0)
    sm, sM, sA = stat([s for s in speeds if s > 1e-9])
    tm, tM, tA = stat(turns)
    rm, rM, rA = stat(rots)
    print("")
    print("=== %s (%s) 帧 1-%d, %d 个关键帧 ===" % (label, got[idx]['name'], end, len(keys)))
    print("  线速度 m/帧 : min %.5f  max %.5f  均值 %.5f   (max/min=%.4f)"
          % (sm, sM, sA, (sM / sm) if sm > 1e-9 else float('inf')))
    print("  方向变化 度 : min %.4f  max %.4f  均值 %.4f" % (tm, tM, tA))
    print("  转动速度 度 : min %.4f  max %.4f  均值 %.4f" % (rm, rM, rA))
    if len(rots) > 2:
        steps = [max(rots[i + 1] / rots[i], rots[i] / rots[i + 1])
                 for i in range(len(rots) - 1) if rots[i] > 1e-9 and rots[i + 1] > 1e-9]
        if steps:
            i = steps.index(max(steps))
            print("  转动台阶比 : max %.4f @f%d   (相邻帧之间转动速度的比值; "
                  "全局 max/min 是几何必然, 台阶比才是肉眼看到的抖动)"
                  % (max(steps), i + 2))
    # 尖峰位置(只报最靠前/最靠后与最大的几处)
    if turns:
        order = sorted(range(len(turns)), key=lambda i: -turns[i])[:4]
        print("  方向变化最大处(帧): %s"
              % ", ".join("f%d=%.2f°" % (i + 3, turns[i]) for i in sorted(order)))
        n = len(turns)
        print("  头/中/尾 方向变化: f3=%.3f°  f%d=%.3f°  f%d=%.3f°"
              % (turns[0], n // 2 + 3, turns[n // 2], n + 2, turns[-1]))
    if rots:
        order = sorted(range(len(rots)), key=lambda i: -rots[i])[:4]
        print("  转动最快处(帧): %s"
              % ", ".join("f%d=%.2f°" % (i + 2, rots[i]) for i in sorted(order)))


for i in range(3):
    diag(i, ("环绕", "推拉", "升降")[i])

mod.unregister()
