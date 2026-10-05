# -*- coding: utf-8 -*-
# 用"真插件"代码验证 横滚/俯仰/偏航 的映射是否符合惯例。
#
# 判据(与左右手系/欧拉约定无关):
#   把两台相机的"物理本地方向"取出来 —— 右(R)/上(U)/前(F=视线) —— 组成 3x3 帧矩阵,
#   然后在各自相机本地算 增量旋转 D = F0ᵀ · F1。
#   若 Blender 侧 与 游戏侧 的 D 完全相同, 说明"绕本地某轴转 θ"这件事在两边是同一个物理动作,
#   即 pitch/yaw/roll 的语义与方向与 Blender 完全一致(符合惯例); 若 D 是镜像, 就是错位。
import importlib.util
import math
import struct
import sys

import bpy
from mathutils import Matrix, Quaternion, Vector

try:
    sys.stdout.reconfigure(encoding="utf-8")
except Exception:
    pass

ADDON = r"D:\EndfieldCameraLink\blender_addon\endfield_camera_bridge\__init__.py"

# Blender 启动时可能已加载 addons 目录里同一个插件, 先禁用避免类名双重注册
try:
    import addon_utils
    addon_utils.disable("endfield_camera_bridge", default_set=False)
except Exception as exc:  # noqa: BLE001
    print("跳过禁用已安装插件:", exc)

spec = importlib.util.spec_from_file_location("ef_rot_addon", ADDON)
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

scene = bpy.context.scene
for o in list(bpy.data.objects):
    bpy.data.objects.remove(o, do_unlink=True)
cam_data = bpy.data.cameras.new("CamData")
cam = bpy.data.objects.new("Cam", cam_data)
scene.collection.objects.link(cam)
cam.rotation_mode = 'QUATERNION'
scene.camera = cam
mod.register()


def norm3(v):
    n = math.sqrt(sum(c * c for c in v))
    return Vector((v[0] / n, v[1] / n, v[2] / n)) if n > 1e-12 else Vector((0, 0, 0))


def cross(a, b):
    return Vector((a[1] * b[2] - a[2] * b[1], a[2] * b[0] - a[0] * b[2], a[0] * b[1] - a[1] * b[0]))


def look_rotation(f, u):
    """复刻 src/shmem.cpp 的 LookRotation"""
    fw = norm3(f)
    up = norm3(u)
    rt = norm3(cross(up, fw))
    upn = cross(fw, rt)
    m = Matrix(((rt.x, upn.x, fw.x), (rt.y, upn.y, fw.y), (rt.z, upn.z, fw.z)))
    return m.to_quaternion()


def game_quat():
    """按插件+模块的真实链路算出游戏相机四元数(绝对朝向模式)"""
    pkt = mod._camera_packet(scene, 2)
    fwd = Vector(struct.unpack_from("<3f", pkt, 44))
    up = Vector(struct.unpack_from("<3f", pkt, 56))
    return look_rotation(fwd, up)


def blender_frame():
    """Blender 相机的物理本地帧: 列 = (右, 上, 视线)"""
    m = cam.matrix_world.to_3x3()
    r = m @ Vector((1.0, 0.0, 0.0))
    u = m @ Vector((0.0, 1.0, 0.0))
    f = m @ Vector((0.0, 0.0, -1.0))       # Blender 相机看本地 -Z
    return Matrix(((r.x, u.x, f.x), (r.y, u.y, f.y), (r.z, u.z, f.z))), (r, u, f)


def game_frame():
    m = game_quat().to_matrix()
    r = m @ Vector((1.0, 0.0, 0.0))
    u = m @ Vector((0.0, 1.0, 0.0))
    f = m @ Vector((0.0, 0.0, 1.0))        # Unity 相机看本地 +Z
    return Matrix(((r.x, u.x, f.x), (r.y, u.y, f.y), (r.z, u.z, f.z))), (r, u, f)


def L(v):
    """插件的轴向换算 _to_unity"""
    return Vector((v[0], v[2], v[1]))


fails = []


def check(name, ok, detail=""):
    print("%-52s %s  %s" % (name, "OK " if ok else "FAIL", detail))
    if not ok:
        fails.append(name)


# 俯仰 20° + 偏航 35° + 横滚 15° 基准姿态(故意都不为零, 避免退化巧合)
base = (Quaternion((0, 0, 1), math.radians(35.0)) @
        Quaternion((1, 0, 0), math.radians(-20.0)) @
        Quaternion((0, 0, 1), math.radians(15.0)))
base.normalize()

cases = [
    ("本地 X (俯仰 pitch)", Vector((1, 0, 0))),
    ("本地 Y (偏航 yaw)", Vector((0, 1, 0))),
    ("本地 Z (横滚 roll)", Vector((0, 0, 1))),
    ("世界 Z", Vector((0, 0, 1))),
]

print("=== 0) 姿态保真: 游戏相机的 (右/上/视线) 是否等于插件的轴向换算结果 ===")
cam.rotation_quaternion = base
bpy.context.view_layer.update()
_, (rb, ub, fb) = blender_frame()
_, (rg, ug, fg) = game_frame()
check("视线 = 换算(Blender视线)", (L(fb) - fg).length < 1e-6, "%.6f" % (L(fb) - fg).length)
check("上方向 = 换算(Blender上方向)", (L(ub) - ug).length < 1e-6, "%.6f" % (L(ub) - ug).length)
check("右方向 = 换算(Blender右方向)", (L(rb) - rg).length < 1e-6, "%.6f" % (L(rb) - rg).length)

print()
print("=== 1) 逐轴增量: 两边'本地增转旋转矩阵'是否完全相同(判据与约定无关) ===")
print("    (D 用物理本地帧 右/上/前 表达; 完全相同 => pitch/yaw/roll 语义与方向一致)")
for name, axis in cases:
    cam.rotation_quaternion = base
    bpy.context.view_layer.update()
    FB0, _ = blender_frame()
    FU0, _ = game_frame()

    if axis == Vector((0, 0, 1)) and "世界" in name:
        cam.rotation_quaternion = (Quaternion((0, 0, 1), math.radians(10.0)) @ base).normalized()
    else:
        cam.rotation_quaternion = (base @ Quaternion(axis, math.radians(10.0))).normalized()
    bpy.context.view_layer.update()
    FB1, _ = blender_frame()
    FU1, _ = game_frame()

    DB = FB0.transposed() @ FB1
    DU = FU0.transposed() @ FU1
    diff = max(abs(DB[i][j] - DU[i][j]) for i in range(3) for j in range(3))
    qb = DB.to_quaternion()
    qu = DU.to_quaternion()
    ang_b = math.degrees(qb.angle)
    ang_u = math.degrees(qu.angle)
    check("%-22s D 完全相同" % name, diff < 1e-5,
          "最大元素差=%.2e | Blender 角=%.3f° 游戏角=%.3f° | 轴差=%.3f°"
          % (diff, ang_b, ang_u, math.degrees(qb.rotation_difference(qu).angle)))

print()
print("=== 2) 直觉核对(水平基准相机 + 与观看者一致的度量) ===")
WORLD_UP_B = Vector((0.0, 0.0, 1.0))   # Blender 世界 up = +Z
WORLD_UP_U = Vector((0.0, 1.0, 0.0))   # 游戏世界 up = +Y

# 水平基准: 先"平视前方"(Blender 相机绕本地 X +90°), 再绕世界 Z 偏航 30°
level = Quaternion((0, 0, 1), math.radians(30.0)) @ Quaternion((1, 0, 0), math.radians(90.0))
level.normalize()
base = level


def frames():
    return blender_frame(), game_frame()


def set_pose(q):
    cam.rotation_quaternion = q
    bpy.context.view_layer.update()


def image_roll(world_up, right, up):
    """画面滚转: 世界up 在相机(右,上)平面内的方位角; 0=画面水平"""
    return math.degrees(math.atan2(world_up.dot(right), world_up.dot(up)))


# --- 俯仰 ---
set_pose(base)
(_, (_, _, fb0)), (_, (_, _, fg0)) = frames()
set_pose((base @ Quaternion((1, 0, 0), math.radians(10.0))).normalized())
(_, (_, _, fb1)), (_, (_, _, fg1)) = frames()
db = fb1.dot(WORLD_UP_B) - fb0.dot(WORLD_UP_B)
dg = fg1.dot(WORLD_UP_U) - fg0.dot(WORLD_UP_U)
check("本地 X 正转 => 两边同样抬视线", db > 1e-4 and abs(db - dg) < 1e-6,
      "视线·世界up 增量: Blender %+.4f / 游戏 %+.4f" % (db, dg))

# --- 偏航 ---
set_pose(base)
(_, (rb0, _, _)), (_, (rg0, _, _)) = frames()
set_pose((base @ Quaternion((0, 1, 0), math.radians(10.0))).normalized())
(_, (_, _, fb2)), (_, (_, _, fg2)) = frames()
check("本地 Y 正转 => 两边同样向左偏航",
      fb2.dot(rb0) < -1e-4 and abs(fb2.dot(rb0) - fg2.dot(rg0)) < 1e-6,
      "视线·原右方向: Blender %+.4f / 游戏 %+.4f (负=向左)" % (fb2.dot(rb0), fg2.dot(rg0)))

# --- 横滚(观看者度量: 世界up 在画面里的倾角) ---
set_pose(base)
(_, (rb, ub, _)), (_, (rg, ug, _)) = frames()
rb0v = image_roll(WORLD_UP_B, rb, ub)
rg0v = image_roll(WORLD_UP_U, rg, ug)
set_pose((base @ Quaternion((0, 0, 1), math.radians(10.0))).normalized())
(_, (rb2, ub2, _)), (_, (rg2, ug2, _)) = frames()
db = image_roll(WORLD_UP_B, rb2, ub2) - rb0v
dg = image_roll(WORLD_UP_U, rg2, ug2) - rg0v
# 报文里的方向向量是 float32, 角度残差 ~1e-5° 属正常, 故容差取 1e-3°
check("本地 Z 正转 => 两边画面同向同量滚转",
      db * dg > 0 and abs(abs(db) - abs(dg)) < 1e-3,
      "世界up 画面倾角变化: Blender %+.4f° / 游戏 %+.4f°" % (db, dg))

mod.unregister()
print()
print("ROT SELFTEST: %s" % ("ALL OK" if not fails else ("FAILED: " + ", ".join(fails))))
