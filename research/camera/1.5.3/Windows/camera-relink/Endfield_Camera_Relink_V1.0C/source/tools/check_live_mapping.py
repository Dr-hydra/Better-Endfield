# -*- coding: utf-8 -*-
# 现场对账: 读插件正在发的共享内存姿态 -> 复刻映射(与 shmem.cpp 的 LookRotation 一致)
#           -> 与模块日志里"写入后 rot"对比。同时核对位置映射(参考物件锚点)。
import mmap, math, re, struct, sys, os

try:
    sys.stdout.reconfigure(encoding='utf-8')
except Exception:
    pass

MAP_NAME = "EndfieldCameraBridgeV1"
log = r'D:\EndfieldCameraLink\game_mod\EndfieldCamLink.log'

# ---- 与 verify_rotation_mapping.py 相同的四元数工具 ----
def qmul(a, b):
    aw, ax, ay, az = a; bw, bx, by, bz = b
    return (aw*bw - ax*bx - ay*by - az*bz, aw*bx + ax*bw + ay*bz - az*by,
            aw*by - ax*bz + ay*bw + az*bx, aw*bz + ax*by - ay*bx + az*bw)
def qconj(q): return (q[0], -q[1], -q[2], -q[3])
def qnorm(q):
    n = math.sqrt(sum(c*c for c in q)); return tuple(c/n for c in q)
def cross(a, b):
    return (a[1]*b[2]-a[2]*b[1], a[2]*b[0]-a[0]*b[2], a[0]*b[1]-a[1]*b[0])
def norm3(v):
    n = math.sqrt(sum(c*c for c in v))
    return tuple(c/n for c in v) if n > 1e-9 else (0.0, 0.0, 0.0)
def look_rotation(f, u):
    fw = norm3(f); up = norm3(u); rt = norm3(cross(up, fw)); upn = cross(fw, rt)
    m00, m01, m02 = rt[0], upn[0], fw[0]
    m10, m11, m12 = rt[1], upn[1], fw[1]
    m20, m21, m22 = rt[2], upn[2], fw[2]
    tr = m00 + m11 + m22
    if tr > 0:
        s = math.sqrt(tr + 1.0) * 2.0; x, y, z, w = (m21-m12)/s, (m02-m20)/s, (m10-m01)/s, 0.25*s
    elif m00 > m11 and m00 > m22:
        s = math.sqrt(1.0 + m00 - m11 - m22) * 2.0; x, y, z, w = 0.25*s, (m01+m10)/s, (m02+m20)/s, (m21-m12)/s
    elif m11 > m22:
        s = math.sqrt(1.0 + m11 - m00 - m22) * 2.0; x, y, z, w = (m01+m10)/s, 0.25*s, (m12+m21)/s, (m02-m20)/s
    else:
        s = math.sqrt(1.0 + m22 - m00 - m11) * 2.0; x, y, z, w = (m02+m20)/s, (m12+m21)/s, 0.25*s, (m10-m01)/s
    return qnorm((w, x, y, z))
def qangle(a, b):
    d = min(1.0, abs(sum(p*q for p, q in zip(qnorm(a), qnorm(b)))))
    return math.degrees(2.0 * math.acos(d))

# ---- 读共享内存 ----
raw = None
for size in (128, 256, 4096):
    try:
        m = mmap.mmap(-1, size, tagname=MAP_NAME, access=mmap.ACCESS_READ)
        m.seek(0); raw = m.read(128); m.close(); break
    except Exception as e:
        last = e
if raw is None:
    print("打不开共享内存 %s: %s" % (MAP_NAME, last)); sys.exit(2)
magic = raw[0:4]
ver, seq = struct.unpack_from("<IQ", raw, 4)
flags, frame = struct.unpack_from("<Ii", raw, 24)
pos = struct.unpack_from("<3f", raw, 32)
fwd = struct.unpack_from("<3f", raw, 44)
up = struct.unpack_from("<3f", raw, 56)
focal, sensorW, sensorH = struct.unpack_from("<3f", raw, 68)
refPos = struct.unpack_from("<3f", raw, 100)
print("=== 插件发出的包 ===")
print("magic=%s ver=%d seq=%d flags=0x%X frame=%d" % (magic, ver, seq, flags, frame))
print("pos    = (%.2f, %.2f, %.2f)" % pos)
print("fwd(已映射到Unity) = (%+.4f, %+.4f, %+.4f)" % fwd)
print("up (已映射到Unity) = (%+.4f, %+.4f, %+.4f)" % up)
print("refPos = (%.2f, %.2f, %.2f)  hasRef=%d" % (refPos[0], refPos[1], refPos[2], 1 if flags & 16 else 0))
print("focal=%.2f sensorH=%.3f -> vFOV=%.2f" % (focal, sensorH, 2*math.degrees(math.atan(sensorH/(2*focal))) if focal > 0.01 else -1))

# ---- 复刻模块的映射 ----
q_expect = look_rotation(fwd, up)
ax_ang = 2.0*math.degrees(math.acos(min(1.0, abs(q_expect[0]))))
s = math.sqrt(max(0.0, 1.0 - q_expect[0]*q_expect[0]))
axis = (q_expect[1]/s, q_expect[2]/s, q_expect[3]/s) if s > 1e-6 else (0,0,0)
print("\n=== 独立复刻的映射结果(模块应写这个朝向) ===")
print("q = (x=%+.5f y=%+.5f z=%+.5f w=%+.5f)  角=%.3f° 轴=(%+.3f %+.3f %+.3f)"
      % (q_expect[1], q_expect[2], q_expect[3], q_expect[0], ax_ang, axis[0], axis[1], axis[2]))

# ---- 取日志里最后一条逐帧追踪行, 对比写入的 rot ----
pat = re.compile(r'\[diag:BrainLateUpdate\][^\n]*?写入后=pos\(([^)]*)\)\s*rot\(([^)]*)\)')
last = None
with open(log, 'r', encoding='utf-8', errors='replace') as fh:
    for line in fh:
        mm = pat.search(line)
        if mm:
            last = mm
if not last:
    print("\n日志里没有 [diag:BrainLateUpdate] 行(逐帧追踪未开)"); sys.exit(3)
wb_pos = [float(x) for x in last.group(1).split(',')]
wb = [float(x) for x in last.group(2).split(',')]
q_game = qnorm((wb[3], wb[0], wb[1], wb[2]))   # 日志按 (x,y,z,w) 打印
print("\n=== 日志里最后一条 %s ===" % "diag")
print("写入后 pos=(%.2f, %.2f, %.2f)" % tuple(wb_pos))
print("写入后 rot=(x%+.5f y%+.5f z%+.5f w%+.5f)" % tuple(wb))
print("\n>>> 映射对账(方向): 独立复刻 vs 模块写入 = %.4f° %s"
      % (qangle(q_expect, q_game), "一致 OK" if qangle(q_expect, q_game) < 0.5 else "不一致 <<<<"))

# ---- 位置对账(参考物件锚点): 目标 = 游戏基线点 + (pos - refPos) ----
print(">>> 位置对账: 需要日志里的「游戏基线点」与目标 pos")
print("    本帧应为: 基线点 + (pos - refPos) = 基线点 + (%.2f, %.2f, %.2f)"
      % (pos[0]-refPos[0], pos[1]-refPos[1], pos[2]-refPos[2]))
print("    日志写入后 pos = (%.2f, %.2f, %.2f)  => 推出的基线点 = (%.2f, %.2f, %.2f)"
      % (wb_pos[0], wb_pos[1], wb_pos[2],
         wb_pos[0]-(pos[0]-refPos[0]), wb_pos[1]-(pos[1]-refPos[1]), wb_pos[2]-(pos[2]-refPos[2])))
