# -*- coding: utf-8 -*-
# 校验 Blender -> 游戏 的旋转映射, 量化"朝向基座偏移"造成的错位。
# 复刻: 插件的 _to_unity + forward/up, 以及模块 shmem.cpp 的 LookRotation
#       与 link.cpp 相对模式下的四元数组合。
import math
import sys

try:
    sys.stdout.reconfigure(encoding='utf-8')   # 控制台若为 GBK 代码页请先 chcp 65001
except Exception:
    pass

def qmul(a, b):
    aw, ax, ay, az = a; bw, bx, by, bz = b
    return (aw*bw - ax*bx - ay*by - az*bz,
            aw*bx + ax*bw + ay*bz - az*by,
            aw*by - ax*bz + ay*bw + az*bx,
            aw*bz + ax*by - ay*bx + az*bw)

def qconj(q): return (q[0], -q[1], -q[2], -q[3])

def qnorm(q):
    n = math.sqrt(sum(c*c for c in q))
    return tuple(c/n for c in q)

def qaxis(axis, deg):
    h = math.radians(deg) * 0.5; s = math.sin(h)
    n = math.sqrt(sum(c*c for c in axis))
    return (math.cos(h), axis[0]/n*s, axis[1]/n*s, axis[2]/n*s)

def qrot(q, v):
    r = qmul(qmul(q, (0.0,) + tuple(v)), qconj(q))
    return (r[1], r[2], r[3])

def qeuler_xyz(x, y, z):
    return qnorm(qmul(qaxis((0,0,1), z), qmul(qaxis((0,1,0), y), qaxis((1,0,0), x))))

def qangle(a, b):
    d = min(1.0, abs(sum(p*q for p, q in zip(qnorm(a), qnorm(b)))))
    return math.degrees(2.0 * math.acos(d))

def ang3(a, b):
    d = max(-1.0, min(1.0, sum(x*y for x, y in zip(a, b))))
    return math.degrees(math.acos(d))

def cross(a, b):
    return (a[1]*b[2]-a[2]*b[1], a[2]*b[0]-a[0]*b[2], a[0]*b[1]-a[1]*b[0])

def norm3(v):
    n = math.sqrt(sum(c*c for c in v))
    return tuple(c/n for c in v) if n > 1e-9 else (0.0, 0.0, 0.0)

# ---- 插件侧: _to_unity 轴交换 + forward/up ----
def to_unity(v): return (v[0], v[2], v[1])

def to_unity_dirs(q):
    return to_unity(qrot(q, (0.0, 0.0, -1.0))), to_unity(qrot(q, (0.0, 1.0, 0.0)))

# ---- 模块侧: LookRotation ----
def look_rotation(f, u):
    fw = norm3(f); up = norm3(u)
    rt = norm3(cross(up, fw))
    upn = cross(fw, rt)
    m00, m01, m02 = rt[0], upn[0], fw[0]
    m10, m11, m12 = rt[1], upn[1], fw[1]
    m20, m21, m22 = rt[2], upn[2], fw[2]
    tr = m00 + m11 + m22
    if tr > 0:
        s = math.sqrt(tr + 1.0) * 2.0
        x, y, z, w = (m21-m12)/s, (m02-m20)/s, (m10-m01)/s, 0.25*s
    elif m00 > m11 and m00 > m22:
        s = math.sqrt(1.0 + m00 - m11 - m22) * 2.0
        x, y, z, w = 0.25*s, (m01+m10)/s, (m02+m20)/s, (m21-m12)/s
    elif m11 > m22:
        s = math.sqrt(1.0 + m11 - m00 - m22) * 2.0
        x, y, z, w = (m01+m10)/s, 0.25*s, (m12+m21)/s, (m02-m20)/s
    else:
        s = math.sqrt(1.0 + m22 - m00 - m11) * 2.0
        x, y, z, w = (m02+m20)/s, (m12+m21)/s, 0.25*s, (m10-m01)/s
    return qnorm((w, x, y, z))

def phi(q_blender):
    f, u = to_unity_dirs(q_blender)
    return look_rotation(f, u)

def local_axis_of(dq):
    """把增量四元数写成 (轴, 角); 轴取上半球。"""
    w = dq[0]
    ang = math.degrees(2.0 * math.acos(max(-1.0, min(1.0, abs(w)))))
    s = math.sqrt(max(0.0, 1.0 - w*w))
    if s < 1e-6: return (0.0, 0.0, 0.0), 0.0
    sign = 1.0 if w >= 0 else -1.0
    return (dq[1]*sign/s, dq[2]*sign/s, dq[3]*sign/s), ang

# ---- 场景: Blender 相机俯拍 30°/方位 25°; 游戏相机自己的姿态完全无关 ----
q_b_base = qeuler_xyz(30.0, 0.0, 25.0)
q_g_base = qeuler_xyz(-3.0, 0.0, -120.0)
q_u_base = phi(q_b_base)

print("== 0) 基座偏移 ==")
print("   映射后的 Blender 基线姿态 与 游戏相机基线姿态 相差: %.1f deg" % qangle(q_u_base, q_g_base))
print("   -> 相对模式下, 这个角度就是一个恒定的错位量, 用户会感觉 xyz 轴不正。")

def game_pose(q_b_now, mode):
    q_u_now = phi(q_b_now)
    if mode == "old":
        # 旧: 世界系增量 × 游戏基线朝向(左乘) —— 轴被 Δ 转过, 三轴互串
        delta = qnorm(qmul(q_u_now, qconj(q_u_base)))
        return qnorm(qmul(delta, q_g_base))
    if mode == "rel":
        # v1.0.0z 修正: 基线**本地帧**增量, 再右乘游戏基线朝向 —— 轴轴对应
        d_local = qnorm(qmul(qconj(q_u_base), q_u_now))
        return qnorm(qmul(q_g_base, d_local))
    if mode == "rel_world":
        # 2026-09-15 提案: **世界系增量**, 按世界轴映射到游戏世界系, 再左乘游戏基线朝向
        #   Δu = K (S Δ S^-1) K^-1 = φ(Δ) ⊗ conj(φ(1)),   K = φ(1) 是相机对象约定的固定基座
        #   Δ = 1 时 Δu = 1 -> 游戏相机仍落在自己的基线(零点不变)
        d_world = qnorm(qmul(q_b_now, qconj(q_b_base)))
        delta_u = qnorm(qmul(phi(d_world), qconj(phi((1.0, 0.0, 0.0, 0.0)))))
        return qnorm(qmul(delta_u, q_g_base))
    return q_u_now

print()
print("== 1) Blender 侧绕【本地 X】+10deg (纯俯仰), 看游戏相机自己转的是什么轴 ==")
print("    轴按“轴线”比较: 轴=(-1,0,0)角=+10deg 与 轴=(+1,0,0)角=-10deg 是同一个物理旋转;")
print("    Unity 左手系与 Blender 右手系的正角符号天然相反, 所以只比轴线方向。")
for mode in ("old", "new"):
    q_b_now = qnorm(qmul(q_b_base, qaxis((1,0,0), 10.0)))
    g0 = q_g_base if mode == "old" else q_u_base
    g1 = game_pose(q_b_now, mode)
    axis, ang = local_axis_of(qnorm(qmul(qconj(g0), g1)))
    off = ang3(axis, (1,0,0))
    off_line = min(off, 180.0 - off)
    print("   模式=%-3s 游戏相机本地增量 轴=(%+.2f,%+.2f,%+.2f) 角=%+.1fdeg | 轴线偏离本地X %.1fdeg%s"
          % (mode, axis[0], axis[1], axis[2], ang, off_line,
             "   <-- 不是纯俯仰(画面会歪)" if off_line > 2 else "   (纯俯仰 OK)"))

print()
print("== 2) Blender 侧绕【本地 Y】+10deg / 【本地 Z】+10deg (修正后应各为纯单轴) ==")
for nm, ax in (("本地Y", (0,1,0)), ("本地Z", (0,0,1))):
    q_b_now = qnorm(qmul(q_b_base, qaxis(ax, 10.0)))
    g1 = game_pose(q_b_now, "new")
    axis, ang = local_axis_of(qnorm(qmul(qconj(q_u_base), g1)))
    off = ang3(axis, ax)
    off_line = min(off, 180.0 - off)
    print("   %s: 轴=(%+.2f,%+.2f,%+.2f) 角=%+.1fdeg  轴线偏离对应轴 %.1fdeg"
          % (nm, axis[0], axis[1], axis[2], ang, off_line))

print()
print("== 3) 三个本地轴的完整响应矩阵 (修正后 = 标准正交, 轴轴对应) ==")
print("   %-11s %-25s %s" % ("Blender 轴", "旧: 相对(三轴互串)", "新: 绝对(轴轴对应)"))
for nm, ax in (("X", (1,0,0)), ("Y", (0,1,0)), ("Z", (0,0,1))):
    q_b_now = qnorm(qmul(q_b_base, qaxis(ax, 10.0)))
    row_old, _ = local_axis_of(qnorm(qmul(qconj(q_g_base), game_pose(q_b_now, "old"))))
    row_new, _ = local_axis_of(qnorm(qmul(qconj(q_u_base), game_pose(q_b_now, "new"))))
    print("   %-11s (%+.2f,%+.2f,%+.2f)     (%+.2f,%+.2f,%+.2f)"
          % ("Blender " + nm, row_old[0], row_old[1], row_old[2],
             row_new[0], row_new[1], row_new[2]))

print()
print("== 4) 游戏相机与“映射后的 Blender 相机”差多少 (新模式应恒为 0) ==")
for mode in ("old", "new"):
    cases = [("初始", q_b_base),
             ("本地X +10deg", qnorm(qmul(q_b_base, qaxis((1,0,0), 10.0)))),
             ("世界Z +20deg", qnorm(qmul(qaxis((0,0,1), 20.0), q_b_base)))]
    for label, q_b_now in cases:
        g = game_pose(q_b_now, mode)
        u = phi(q_b_now)
        look_err = ang3(qrot(g, (0,0,1)), qrot(u, (0,0,1)))
        print("   模式=%-3s %-13s 姿态差 %6.1fdeg | 视线方向差 %5.1fdeg"
              % (mode, label, qangle(g, u), look_err))

# ---------------------------------------------------------------------------
# 5) v1.0.0z: 相对(增量)模式的修正校验
#    判据(全部是数值判据, 与左右手系/欧拉约定无关):
#      ① 三个本地轴各自转 10° → 游戏相机的**本地增量轴**必须与对应轴重合(偏差 ≈ 0)
#      ② 游戏相机的本地增量 == 映射后 Blender 相机的本地增量(同一物理旋转)
#      ③ Blender 回到基线姿态 → 游戏相机必须正好回到自己的基线姿态(零点是游戏基线)
#      ④ 增量幅度保真(角大小一致)
# ---------------------------------------------------------------------------
print()
print("== 5) 相对(增量)模式修正校验 [v1.0.0z] ==")
fails = []
for nm, ax in (("本地X", (1,0,0)), ("本地Y", (0,1,0)), ("本地Z", (0,0,1))):
    q_b_now = qnorm(qmul(q_b_base, qaxis(ax, 10.0)))
    g_old = game_pose(q_b_now, "old")
    g_rel = game_pose(q_b_now, "rel")
    a_old, ang_old = local_axis_of(qnorm(qmul(qconj(q_g_base), g_old)))
    a_rel, ang_rel = local_axis_of(qnorm(qmul(qconj(q_g_base), g_rel)))
    off_old = ang3(a_old, ax); off_old = min(off_old, 180.0 - off_old)
    off_rel = ang3(a_rel, ax); off_rel = min(off_rel, 180.0 - off_rel)
    ok = off_rel <= 0.01 and abs(abs(ang_rel) - 10.0) <= 0.01
    if not ok:
        fails.append("%s 修正后轴偏差 %.3fdeg 角 %.3fdeg" % (nm, off_rel, abs(ang_rel)))
    print("   %s: 旧 轴=(%+.2f,%+.2f,%+.2f) 偏 %.1fdeg 角 %+.1f | "
          "新 轴=(%+.2f,%+.2f,%+.2f) 偏 %.2fdeg 角 %+.3f  %s"
          % (nm, a_old[0], a_old[1], a_old[2], off_old, ang_old,
             a_rel[0], a_rel[1], a_rel[2], off_rel, ang_rel,
             "OK" if ok else "FAIL"))

# ② 本地增量与映射后 Blender 的本地增量一致(逐轴, 含符号)
for nm, ax in (("本地X", (1,0,0)), ("本地Y", (0,1,0)), ("本地Z", (0,0,1))):
    q_b_now = qnorm(qmul(q_b_base, qaxis(ax, 10.0)))
    d_game = qnorm(qmul(qconj(q_g_base), game_pose(q_b_now, "rel")))
    d_blen = qnorm(qmul(qconj(q_u_base), phi(q_b_now)))
    err = qangle(d_game, d_blen)
    ok = err <= 0.01
    if not ok:
        fails.append("%s 本地增量与映射后 Blender 不一致(%.3fdeg)" % (nm, err))
    print("   %s 本地增量 vs 映射后 Blender 本地增量: 差 %.4fdeg  %s"
          % (nm, err, "OK" if ok else "FAIL"))

# ③ 零点必须仍落在"游戏基线姿态"上
g_at_base = game_pose(q_b_base, "rel")
err0 = qangle(g_at_base, q_g_base)
ok0 = err0 <= 1e-6
if not ok0:
    fails.append("回到基线姿态时未落在游戏基线(%.4fdeg)" % err0)
print("   Blender 回到基线 -> 游戏相机 = 游戏基线姿态: 差 %.6fdeg  %s" % (err0, "OK" if ok0 else "FAIL"))

# ④ 任意大角度增量下判据仍成立(绕本地 Y 转 75°)
q_b_now = qnorm(qmul(q_b_base, qaxis((0,1,0), 75.0)))
a_big, ang_big = local_axis_of(qnorm(qmul(qconj(q_g_base), game_pose(q_b_now, "rel"))))
off_big = ang3(a_big, (0,1,0)); off_big = min(off_big, 180.0 - off_big)
ok_big = off_big <= 0.01 and abs(abs(ang_big) - 75.0) <= 0.01
if not ok_big:
    fails.append("本地Y 75deg 偏差 %.3fdeg 角 %.3fdeg" % (off_big, abs(ang_big)))
print("   大角度(本地Y 75deg): 偏 %.3fdeg 角 %.3fdeg  %s"
      % (off_big, abs(ang_big), "OK" if ok_big else "FAIL"))

# ---------------------------------------------------------------------------
# 6) 2026-09-15: "**增量是否在标准(世界)坐标系里?**"
#    用户反馈: "之前修过增量变换; 映射确实正确了, 但 Blender 里的增量变换不再是标准坐标系"。
#    这里把四种写法的**响应轴**都量出来, 判据全部与姿态无关:
#      世界系判据: Blender 绕【世界轴】W 转 10° -> 游戏相机自己的世界系增量 d=q_now⊗conj(q_zero)
#                  的轴 是否 = 标准像 S(W)=to_unity(W)
#      本地系判据: Blender 绕【本地轴】L 转 10° -> 游戏相机自己的本地系增量 conj(q_zero)⊗q_now
#                  的轴 是否 = S(L)=to_unity(L)
#    四种写法:
#      new       绝对: 游戏相机朝向 = 映射后的 Blender 朝向(v16 起的默认)
#      old       相对/世界系增量: delta = q_now ⊗ conj(q_base), 再**左乘**游戏基线(1.0.0y 及以前)
#      rel       相对/本地帧增量: delta = conj(q_base) ⊗ q_now, 再**右乘**游戏基线(v1.0.0z 现行)
#      rel_world 相对/世界系增量(与 old 同值, 独立推导): Δu = φ(Δ) ⊗ conj(φ(1)), 左乘游戏基线
# ---------------------------------------------------------------------------
K = phi((1.0, 0.0, 0.0, 0.0))     # 相机对象约定基座 φ(1): Blender 相机(-Z) -> Unity 相机(+Z)

def game_pose_b(q_b_b, q_b_now, mode, q_g_b):
    """指定 Blender 基线姿态 + 游戏基线朝向的版本。q_b_b/q_b_now 为 Blender 姿态。"""
    q_u_b = phi(q_b_b)
    q_u_now = phi(q_b_now)
    if mode == "new":
        return q_u_now
    if mode == "old":
        return qnorm(qmul(qnorm(qmul(q_u_now, qconj(q_u_b))), q_g_b))
    if mode == "rel":
        return qnorm(qmul(q_g_b, qnorm(qmul(qconj(q_u_b), q_u_now))))
    if mode == "rel_world":
        d = qnorm(qmul(q_b_now, qconj(q_b_b)))
        return qnorm(qmul(qnorm(qmul(phi(d), qconj(K))), q_g_b))
    raise ValueError(mode)

MODES6 = (("new", "绝对"), ("old", "相对·世界系(1.0.0y-)"),
          ("rel", "相对·本地帧(现行)"), ("rel_world", "相对·世界系(提案)"))

def axis_err(a, b):
    off = ang3(a, b)
    return min(off, 180.0 - off)

print()
print("== 6) 增量在标准坐标系里吗?  [2026-09-15 用户反馈核查] ==")
print("   基线错位 Δ = %.1fdeg —— 相对模式的所有偏差都源于它" % qangle(phi(q_b_base), q_g_base))
print()
print("   [表1] Blender 绕【世界轴】+10deg -> 游戏相机的世界系增量轴 与 S(W) 的偏差")
print("   %-8s %-13s %-22s %-22s %-22s" % ("", "绝对", "相对·世界系(1.0.0y-)", "相对·本地帧(现行)", "相对·世界系(提案)"))
for nm, ax in (("世界X", (1,0,0)), ("世界Y", (0,1,0)), ("世界Z", (0,0,1))):
    q_b_now = qnorm(qmul(qaxis(ax, 10.0), q_b_base))
    want = to_unity(ax)
    cells = []
    for mode, _ in MODES6:
        zero = game_pose_b(q_b_base, q_b_base, mode, q_g_base)
        now = game_pose_b(q_b_base, q_b_now, mode, q_g_base)
        a, ang = local_axis_of(qnorm(qmul(now, qconj(zero))))
        off = axis_err(a, want)
        cells.append("偏 %6.2fdeg 角%6.2f" % (off, abs(ang)))
        if mode in ("new", "old", "rel_world") and off > 0.05:
            fails.append("%s 世界增量在 %s 模式下轴偏差 %.3fdeg" % (nm, mode, off))
    print("   %-8s %-15s %-24s %-24s %-24s" % (nm, cells[0], cells[1], cells[2], cells[3]))

print()
print("   [表2] Blender 绕【本地轴】+10deg -> 游戏相机的本地系增量轴 与本地轴 L 的偏差")
for nm, ax in (("本地X", (1,0,0)), ("本地Y", (0,1,0)), ("本地Z", (0,0,1))):
    q_b_now = qnorm(qmul(q_b_base, qaxis(ax, 10.0)))
    want = ax                       # 本地系判据: 比对相机**本地坐标轴编号**(与 §5 同判据)
    cells = []
    for mode, _ in MODES6:
        zero = game_pose_b(q_b_base, q_b_base, mode, q_g_base)
        now = game_pose_b(q_b_base, q_b_now, mode, q_g_base)
        a, ang = local_axis_of(qnorm(qmul(qconj(zero), now)))     # 游戏相机本地系增量
        off = axis_err(a, want)
        cells.append("偏 %6.2fdeg 角%6.2f" % (off, abs(ang)))
        if mode in ("new", "rel") and off > 0.05:
            fails.append("%s 本地增量在 %s 模式下轴偏差 %.3fdeg" % (nm, mode, off))
    print("   %-8s %-15s %-24s %-24s %-24s" % (nm, cells[0], cells[1], cells[2], cells[3]))

print()
print("   [表3] 基线无关性: 换 5 个 Blender 基线姿态, 同一【世界轴】增量的响应轴最大漂移")
BASES = [qeuler_xyz(30.0, 0.0, 25.0), qeuler_xyz(-10.0, 5.0, 200.0), qeuler_xyz(0.0, 80.0, 0.0),
         qeuler_xyz(12.0, -40.0, 133.0), qeuler_xyz(-25.0, 15.0, -70.0)]
for nm, ax in (("世界X", (1,0,0)), ("世界Y", (0,1,0)), ("世界Z", (0,0,1))):
    want = to_unity(ax)
    cells = []
    for mode, _ in MODES6:
        worst = 0.0
        for b in BASES:
            zero = game_pose_b(b, b, mode, q_g_base)
            now = game_pose_b(b, qnorm(qmul(qaxis(ax, 10.0), b)), mode, q_g_base)
            a, _ = local_axis_of(qnorm(qmul(now, qconj(zero))))
            worst = max(worst, axis_err(a, want))
        cells.append("%6.2fdeg" % worst)
        if mode in ("new", "old", "rel_world") and worst > 0.05:
            fails.append("%s 世界增量在 %s 模式下基线漂移 %.3fdeg" % (nm, mode, worst))
    print("   %-8s 绝对 %-8s 世界系(旧) %-8s 本地帧(现行) %-8s 世界系(提案) %-8s"
          % (nm, cells[0], cells[1], cells[2], cells[3]))

print()
print("   [表4] 把游戏基线对齐到映射后的 Blender 基线(Δ=0)后, 两种系是否同时精确?")
qg_aligned = phi(q_b_base)
worst_w = dict((m, 0.0) for m, _ in MODES6)
worst_l = dict((m, 0.0) for m, _ in MODES6)
for b in BASES:
    qg = phi(b)
    for nm, ax in (("X", (1,0,0)), ("Y", (0,1,0)), ("Z", (0,0,1))):
        for mode, _ in MODES6:
            zero = game_pose_b(b, b, mode, qg)
            noww = game_pose_b(b, qnorm(qmul(qaxis(ax, 10.0), b)), mode, qg)
            aw, _a = local_axis_of(qnorm(qmul(noww, qconj(zero))))
            worst_w[mode] = max(worst_w[mode], axis_err(aw, to_unity(ax)))
            nowl = game_pose_b(b, qnorm(qmul(b, qaxis(ax, 10.0))), mode, qg)
            al, _b = local_axis_of(qnorm(qmul(qconj(zero), nowl)))
            worst_l[mode] = max(worst_l[mode], axis_err(al, ax))   # 本地轴编号判据
for mode, lab in MODES6:
    ok = worst_w[mode] <= 0.05 and worst_l[mode] <= 0.05
    if not ok:
        fails.append("Δ=0 时 %s 模式仍有偏差(世界 %.3f / 本地 %.3f)" % (mode, worst_w[mode], worst_l[mode]))
    print("      %-22s 世界轴偏差 %6.3fdeg | 本地轴偏差 %6.3fdeg  %s"
          % (lab, worst_w[mode], worst_l[mode], "OK 两系同时精确" if ok else "FAIL"))

print()
print("   [折中] 结论: Δ≠0 时, 相对模式只能在一个系里精确 —— 两种系不可能同时标准;")
print("          Δ=0(游戏基线对齐到映射后的 Blender 基线)时四写法全部精确。")
print("   [对应] 运行时模式 ↔ 本表列 (v1.0.0at):")
print("          绝对(pose_rot_absolute=true)                = 绝对列")
print("          相对·标准(pose_rot_increment_frame=0, 默认) = 绝对列(等价武装时对齐基线 Δ=0)")
print("          相对·世界系(指令 38 a0=2 / frame=1)         = 相对·世界系(1.0.0y-)列")
print("          相对·本地帧(指令 38 a0=3 / frame=2)         = 相对·本地帧(现行)列 = 1.0.0z 语义")
print("   [零点] 回到基线姿态时游戏相机是否落回自己的基线:")
for mode, lab in MODES6:
    e = qangle(game_pose_b(q_b_base, q_b_base, mode, q_g_base), q_g_base)
    note = "OK" if e <= 1e-6 else "N/A(绝对模式跟随映射姿态, 基线错位 %.1fdeg)" % qangle(q_b_base, q_u_base) if mode == "new" else "FAIL"
    if mode != "new" and e > 1e-6:
        fails.append("%s 零点偏移 %.6fdeg" % (mode, e))
    print("      %-22s 差 %.6fdeg  %s" % (lab, e, note))

print()
if fails:
    print("ROTATION MAPPING RESULT: FAIL")
    for f in fails:
        print("   - " + f)
    sys.exit(1)
print("ROTATION MAPPING RESULT: ALL OK  (绝对模式与相对·世界系在同一套轴向映射下精确)")
