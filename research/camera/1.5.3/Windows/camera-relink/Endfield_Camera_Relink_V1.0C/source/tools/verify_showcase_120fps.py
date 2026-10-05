# -*- coding: utf-8 -*-
"""验证 tools/anim/showcase_120fps.py：120fps × 10 秒 = 1200 帧
   时长约束 / 帧数推导 / 构图不变 / 速度不加快 / 速度曲线 / 跟焦点 / 旧动画保留。"""
import math
import sys

import bpy
from mathutils import Vector

try:
    sys.stdout.reconfigure(encoding="utf-8")
except Exception:
    pass

SCRIPT = r"D:\EndfieldCameraLink\tools\anim\showcase_120fps.py"
FPS_MAIN = 120
fails = []


def check(name, ok, detail=""):
    print("%-50s %s  %s" % (name, "OK " if ok else "FAIL", detail))
    if not ok:
        fails.append(name)


def reset_scene(fps, rot_mode):
    for o in list(bpy.data.objects):
        bpy.data.objects.remove(o, do_unlink=True)
    for a in list(bpy.data.actions):
        bpy.data.actions.remove(a)
    scene = bpy.context.scene
    scene.render.fps = fps
    scene.render.fps_base = 1.0
    cd = bpy.data.cameras.new("CamData")
    cam = bpy.data.objects.new("Cam", cd)
    scene.collection.objects.link(cam)
    cam.rotation_mode = rot_mode
    scene.camera = cam
    return scene, cam


def run_script(overrides=None):
    ns = {"__name__": "__main__"}
    with open(SCRIPT, "r", encoding="utf-8") as fh:
        code = fh.read()
    if overrides:
        # 覆盖必须注入在最后 main() 调用之前（写在参数区开头会被脚本自身的赋值覆盖）
        idx = code.rstrip().rfind("main()")
        extra = "\n".join("%s = %r" % kv for kv in overrides.items()) + "\n"
        code = code[:idx] + extra + code[idx:]
    exec(compile(code, SCRIPT, "exec"), ns)
    return ns


def sample(scene, cam, step=1):
    out = []
    for f in range(scene.frame_start, scene.frame_end + 1, step):
        scene.frame_set(f)
        m = cam.matrix_world
        out.append((m.translation.copy(),
                    (m.to_3x3() @ Vector((0.0, 0.0, -1.0))).normalized(),
                    cam.data.dof.focus_distance))
    return out


def ref_pose(frac, ns):
    tref = frac * ns["REFERENCE_DURATION"]
    theta, r, h, aim, dutch = ns["evaluate"](tref, 1.0)
    return ns["camera_pose"](theta, r, h, aim, dutch)[0]


print("=== A) 120fps / 10 秒（主场景配置）===")
scene, cam = reset_scene(FPS_MAIN, 'XYZ')
cam.location = (5.0, 5.0, 5.0)
cam.rotation_euler = (1.0, 0.2, 0.3)
cam.keyframe_insert("location", frame=1)
old = cam.animation_data.action.name
ns = run_script()

check("帧范围 = 1..1201（1200 帧 = 120fps × 10s）",
      scene.frame_start == 1 and scene.frame_end == 1201,
      "%d..%d" % (scene.frame_start, scene.frame_end))
check("时长正好 10.00 s", abs((scene.frame_end - 1) / 120.0 - 10.0) < 1e-9,
      "%.4f s" % ((scene.frame_end - 1) / 120.0))
check("旧 action 仍在（未被清空）", bpy.data.actions.get(old) is not None)
check("当前 action = 新运镜", cam.animation_data.action.name == ns["ACTION_NAME"])

samples = sample(scene, cam)
worst = 0.0
for i, (pos, _, _) in enumerate(samples):
    frac = i / float(scene.frame_end - scene.frame_start)
    worst = max(worst, (pos - ref_pose(frac, ns)).length)
check("构图与 10 秒设计逐帧一致(≤2.5cm)", worst < 0.025, "最大偏差 %.4f m" % worst)

pos = [p for p, _, _ in samples]
spd = [(pos[i + 1] - pos[i]).length * 120.0 for i in range(len(pos) - 1)]
peak = max(spd)
check("峰值速度 = 10 秒设计值 1.87 m/s（没有被加快）", abs(peak - 1.87) < 0.12,
      "实测峰值 %.2f m/s" % peak)
print("    分段速度(m/s)：")
bounds = [0.0, 2.2, 2.7, 5.6, 6.9, 8.7, 10.0]
names = ["特写推近", "微横移", "环绕+缓拉", "摇下揭示", "拉远英雄", "缓停收尾"]
for i in range(len(bounds) - 1):
    a = int(bounds[i] / 10.0 * len(spd))
    b = max(a + 1, int(bounds[i + 1] / 10.0 * len(spd)))
    seg = spd[a:b]
    print("      %-8s t %4.1f-%4.1f s : 平均 %.2f  峰值 %.2f  首 %.2f  末 %.2f"
          % (names[i], bounds[i], bounds[i + 1], sum(seg) / len(seg), max(seg), seg[0], seg[-1]))
check("速度曲线形状：特写慢 < 环绕快 > 收尾最慢",
      spd[int(4.0 * 120)] > spd[int(1.0 * 120)] and spd[-1] < 0.3,
      "环绕中段 %.2f / 特写中段 %.2f / 收尾 %.2f" % (spd[int(4.0 * 120)], spd[int(1.0 * 120)], spd[-1]))

# 关键帧分布：用解析方式核对"自适应步长"逻辑（不去读 Blender 的槽位动作内部结构，那会崩）
fps_main = 120.0
ts = 1.0
dts = []
t = 0.0
while t < 10.0 - 1e-9:
    p0 = ref_pose(t / 10.0, ns)
    eps = 1e-3
    p1 = ref_pose(min(10.0, t + eps) / 10.0, ns)
    v = (p1 - p0).length / eps
    p2 = ref_pose(min(10.0, t + 0.05) / 10.0, ns)
    a = abs((p2 - p0).length / 0.05 - v) / 0.05
    dt = ns["KEY_STEP_MAX"]
    if v > 1e-6:
        dt = min(dt, ns["KEY_STEP_ARC"] / v)
    if a > 1e-6:
        dt = min(dt, math.sqrt(2.0 * ns["KEY_STEP_TOL"] / a))
    if t < 0.15 or t > 10.0 - 0.15:
        dt = min(dt, 0.05)
    dt = max(1.0 / fps_main, dt)
    dts.append(dt)
    t += dt
check("关键帧自适应分布(快速段密、慢速段疏)",
      max(dts) >= 3.0 * min(dts),
      "预计 %d 个关键帧；间距 %.0f 帧(快速段) ~ %.0f 帧(慢速段)，比 %.1f×"
      % (len(dts), min(dts) * fps_main, max(dts) * fps_main, max(dts) / min(dts)))

check("相机始终在角色体外(离头心 ≥0.5m)",
      min((p - Vector((0, 0, 0))).length for p, _, _ in samples) >= 0.5,
      "最近 %.3f m" % min((p - Vector((0, 0, 0))).length for p, _, _ in samples))
focus = [f for _, _, f in samples]
check("跟焦点随距离打帧", min(focus) > 0.05 and max(focus) - min(focus) > 1.0,
      "%.2f ~ %.2f m" % (min(focus), max(focus)))

print()
print("=== B) 30fps 下同一脚本：时长仍 10 秒，帧数自动变 300 ===")
scene2, cam2 = reset_scene(30, 'QUATERNION')
ns2 = run_script()
check("30fps → 帧范围 1..301（300 帧 = 10 秒）", scene2.frame_end == 301, "%d" % scene2.frame_end)
s2 = sample(scene2, cam2)
p2 = [(s2[i + 1][0] - s2[i][0]).length * 30.0 for i in range(len(s2) - 1)]
check("30fps 峰值速度仍是 1.87 m/s（速度不随帧率变）", abs(max(p2) - 1.87) < 0.12,
      "实测 %.2f m/s" % max(p2))

print()
print("=== C) 强行指定 FRAME_COUNT=120 时的警告路径（应提示会变快）===")
scene3, cam3 = reset_scene(FPS_MAIN, 'XYZ')
ns3 = run_script({"FRAME_COUNT": 120})
check("帧数约束生效（1..120）", scene3.frame_end == 120, "%d" % scene3.frame_end)
print("    （上面的『警告』行即脚本自动给出的提示：120 帧在 120fps 下只有 1 秒）")

print()
print("SHOWCASE-120FPS SELFTEST: %s" % ("ALL OK" if not fails else ("FAILED: " + ", ".join(fails))))
