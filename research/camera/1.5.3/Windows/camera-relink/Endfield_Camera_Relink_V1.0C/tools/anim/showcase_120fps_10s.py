# -*- coding: utf-8 -*-
"""终末地角色展示运镜（Blender 端脚本）

────────────────────────────────────────────────────────────
用法（三步）
  1) 游戏侧先武装：相机控制台.cmd 菜单 [3] 武装相机；Blender 插件面板点「开始实时发送」
  2) Blender 顶部切到「Scripting / 脚本」工作区 → Open → 选本文件 → Run Script（Alt+P）
  3) 按空格播放：游戏相机会同步跟着走（位置以参考方块为原点，朝向绝对）

时长与帧数（关键：帧数 = 时长 × 帧率）
  · 硬约束是 `TARGET_SECONDS = 10.0`（运镜 10 秒，速度恒定，不随帧率变化）
  · 帧数不手填，由帧率推出：帧范围 = 1 .. 1+round(TARGET_SECONDS × 场景帧率)
      120fps（本人当前场景）→ **1200 帧**（帧范围 1..1201）★
      24fps → 240 帧    30fps → 300 帧    60fps → 600 帧
  · 想换总帧数就改 TARGET_SECONDS 或场景帧率；想强行指定帧数可用 `FRAME_COUNT`（会改变时长）。

空间前提（与模块映射一致）
  · 角色头部中心 = (0,0,0)，世界 up = +Z，角色面向 -X，角色右手边 = +Y
  · 相机位移 1:1 出现在游戏里；朝向完全跟随 Blender

运镜设计（策略：特写 / 环绕 / 推 / 拉 / 摇 / 移 + 速度曲线；时间为 10 秒基准）
  t 0.0-2.2  特写推近（前右 3/4，1.30m→0.72m）        SINE-EASE_OUT（推到底减速）
  t 2.2-2.7  微横移蓄势（移）                          LINEAR（匀速微动）
  t 2.7-5.6  环绕 148° + 缓拉（0.74m→1.50m，边绕边抬）  SINE-EASE_IN_OUT
  t 5.6-6.9  摇下 + 降到腰线（全身揭示）               SINE-EASE_IN_OUT（两端软着陆）
  t 6.9-8.7  拉远 + 侧移（3.30m 低角度仰拍英雄镜头）     SINE-EASE_IN_OUT
  t 8.7-10.0 缓停呼吸（英雄机位收尾）                   SINE-EASE_OUT

安全说明
  · 不改动你已有的动画：会新建一个 action 并切换过去，原 action 名字会打印出来
  · 不写盘：只改内存，脚本结束不会自动保存（要留就自己 Ctrl+S）
  · 相机如有父级会自动换算成局部坐标；如有约束(constraints)会提示（约束会让脚本结果不准）
"""

import math

import bpy
from mathutils import Matrix, Vector

# ────────── 可调参数 ──────────
TARGET_SECONDS = 10.0    # 【硬约束】运镜时长(秒)：整段严格铺满这么长时间，速度不随帧率变化
# 帧数不手填：帧范围 = 1 .. 1+round(TARGET_SECONDS × 场景帧率)
#   24fps → 241 帧   30fps → 301 帧   60fps → 601 帧   120fps → 1201 帧
# 若成片必须"120 帧铺满 10 秒"，那是 12fps 的播放率（120 ÷ 10 = 12），
# 做法：渲染帧率设 12，或把 1201 帧按每 10 帧取 1 帧导出（脚本末尾会打印换算）。
FRAME_COUNT = None       # 可选：强行指定帧数(如 120)。**帧数一旦固定，时长就变成
                         # FRAME_COUNT/fps 秒**，运镜会随之变快/变慢 —— 一般保持 None。
SET_FPS_TO_MATCH = False # 仅当同时设了 FRAME_COUNT 时有效：True = 把场景帧率改成
                         # FRAME_COUNT/TARGET_SECONDS，让"帧数+时长"同时成立。
                         # 警告：改场景帧率会改变你场景里其它动画的时间，默认关闭。
REFERENCE_DURATION = 10.0  # 下面 SEGMENTS 表按"10 秒基准"书写，实际按 TARGET_SECONDS 等比缩放
KEY_STEP_MAX = 0.25      # 慢速段的关键帧间隔上限（秒）：越大越好手改
KEY_STEP_ARC = 0.12      # 快速段的"位移步长上限"（米）：越小越贴合弧线（0.12m → 弧线误差 <2mm）
KEY_STEP_TOL = 0.004     # 加/减速段的"偏差容限"（米）：按 |a| 加密，保证缓起缓停的形状不被手柄压平
TRACK_FOCUS = True       # 顺手把 dof.focus_distance 关键帧打到"相机到目标的距离"
ACTION_NAME = "EF_Showcase_10s"
DUTCH_SIGN = -1.0        # 荷兰角符号：+1/-1 决定画面往哪边倾；嫌反了就改成 +1.0
SPEED_WARN = 2.6         # 峰值速度超过该值(m/s)时提示（10 秒版设计峰值 1.87 m/s）

# 每段： (t0, t1, ease, θ0,θ1, R0,R1, h0,h1, aim0,aim1, dutch0,dutch1)
#   θ = 相机绕角色竖直轴的方位角(度)：0°=角色正前方(-X)，90°=角色右手边(+Y)，180°=正后方
#   R = 到头部中心的水平距离(米)   h = 相对头部中心的高度偏移(米)   aim = 注视点高度偏移(米)
#   dutch = 荷兰角(度)
#
# 节奏按"角色宣传片"的分量配：特写 2.2s → 微移 0.5s → 环绕 2.9s → 降摇揭示 1.3s
#                            → 拉远侧移(英雄) 1.8s → 缓停收尾 1.3s
# 峰值速度控制在 ~1.7 m/s 以内（环绕峰值约 78°/s），避免变成甩镜。
SEGMENTS = [
    # ① 特写推近（前右 3/4，1.30m → 0.72m，视线始终咬住头部）
    (0.0, 2.2, "ease_out", 30, 30, 1.30, 0.72, 0.08, 0.02, 0.00, 0.00, 0.0, 0.0),
    # ② 微横移蓄势（小幅侧滑 + 略微抬头）
    (2.2, 2.7, "linear", 30, 48, 0.72, 0.74, 0.02, 0.06, 0.00, -0.03, 0.0, 0.0),
    # ③ 环绕 + 缓拉（右侧 → 正后方，半径 0.74m → 1.50m，边绕边抬）
    (2.7, 5.6, "ease_in_out", 48, 196, 0.74, 1.50, 0.06, 0.34, -0.03, -0.22, 0.0, 0.0),
    # ④ 摇下 + 降到腰线（俯角转为平视，注视点从胸口下移到腰 → 全身揭示；两端都软着陆）
    (5.6, 6.9, "ease_in_out", 196, 226, 1.50, 1.72, 0.34, -0.30, -0.22, -0.78, 0.0, 0.0),
    # ⑤ 拉远 + 侧移（1.72m → 3.30m，机位降到膝上，转为仰拍英雄镜头 + 轻微荷兰角）
    (6.9, 8.7, "ease_in_out", 226, 258, 1.72, 3.30, -0.30, -0.55, -0.78, -0.18, 0.0, 2.2),
    # ⑥ 缓停 + 微漂（在英雄机位上慢慢停住，给结尾留一个"呼吸"）
    (8.7, 10.0, "ease_out", 258, 266, 3.30, 3.45, -0.55, -0.58, -0.18, -0.16, 2.2, 2.4),
]

# ────────── 缓动 ──────────
EASES = {
    "linear": lambda x: x,
    "ease_in": lambda x: 1.0 - math.cos(x * math.pi * 0.5),           # 慢起
    "ease_out": lambda x: math.sin(x * math.pi * 0.5),                # 缓停
    "ease_in_out": lambda x: 0.5 - 0.5 * math.cos(math.pi * x),       # 两头缓
    "ease_in_cubic": lambda x: x ** 3,                                # 明显加速
    "ease_out_cubic": lambda x: 1.0 - (1.0 - x) ** 3,
}


def lerp(a, b, k):
    return a + (b - a) * k


def evaluate(t_ref, time_scale=1.0):
    """在"基准时间" t_ref(秒, 0~REFERENCE_DURATION) 处求运镜参数 (θ, R, h, aim, dutch)。
    time_scale: 实际时长/基准时长的比例 —— 分镜表总是按基准写，实际播放时整体等比缩放。"""
    t = min(REFERENCE_DURATION, max(0.0, t_ref / time_scale))
    seg = SEGMENTS[-1]
    for s in SEGMENTS:
        if s[0] <= t <= s[1] + 1e-9:
            seg = s
            break
    t0, t1, ease_name, th0, th1, r0, r1, h0, h1, a0, a1, d0, d1 = seg
    span = max(1e-9, t1 - t0)
    x = min(1.0, max(0.0, (t - t0) / span))
    k = EASES[ease_name](x)
    return (lerp(th0, th1, k), lerp(r0, r1, k), lerp(h0, h1, k),
            lerp(a0, a1, k), lerp(d0, d1, k))


def camera_pose(theta_deg, radius, height, aim_height, dutch_deg):
    """由运镜参数算出相机世界位置与朝向矩阵（看 aim 点，up 取世界 +Z，可加荷兰角）"""
    th = math.radians(theta_deg)
    # θ=0 → 角色正前方(-X)；θ 增大 → 转向角色右手边(+Y)
    pos = Vector((-math.cos(th) * radius, math.sin(th) * radius, height))
    aim = Vector((0.0, 0.0, aim_height))
    forward = (aim - pos)
    if forward.length < 1e-6:
        forward = Vector((1.0, 0.0, 0.0))
    forward.normalize()
    up_ref = Vector((0.0, 0.0, 1.0))
    if abs(forward.dot(up_ref)) > 0.999:      # 垂直向下/向上时的退化保护
        up_ref = Vector((0.0, 1.0, 0.0))
    back = -forward                            # Blender 相机看本地 -Z
    x_axis = up_ref.cross(back)
    x_axis.normalize()
    y_axis = back.cross(x_axis)
    # 荷兰角：绕视线轴滚转
    d = math.radians(dutch_deg) * DUTCH_SIGN
    x_r = x_axis * math.cos(d) - y_axis * math.sin(d)
    y_r = y_axis * math.cos(d) + x_axis * math.sin(d)
    rot = Matrix(((x_r.x, y_r.x, back.x, 0.0),
                  (x_r.y, y_r.y, back.y, 0.0),
                  (x_r.z, y_r.z, back.z, 0.0),
                  (0.0, 0.0, 0.0, 1.0)))
    return pos, rot, (aim - pos).length


def unwrap_euler(cur, prev):
    """把欧拉角各通道解到与上一帧连续的那一支（避免 360° 跳变）"""
    out = list(cur)
    for i in range(3):
        while out[i] - prev[i] > math.pi:
            out[i] -= 2 * math.pi
        while out[i] - prev[i] < -math.pi:
            out[i] += 2 * math.pi
    return out


def action_fcurves(action):
    """取 action 的全部 fcurve —— 兼容 Blender 4.4+ 的槽位动作系统与旧版 action。
    (注意: 5.x 里 action.fcurves 这个属性已不存在, 直接访问会 AttributeError)"""
    out = []
    try:
        out.extend(list(action.fcurves))
    except AttributeError:
        pass
    if out:
        return out
    for layer in getattr(action, "layers", []):
        for strip in getattr(layer, "strips", []):
            for chbag in getattr(strip, "channelbags", []):
                try:
                    out.extend(chbag.fcurves)
                except AttributeError:
                    pass
    return out


def main():
    scene = bpy.context.scene
    cam = scene.camera
    if cam is None or cam.type != 'CAMERA':
        print("[运镜] 失败：场景没有活动相机（选中相机后 Ctrl+Numpad0 设为活动相机）")
        return

    fps = float(scene.render.fps) / (float(scene.render.fps_base) or 1.0)

    # ── 时长优先：帧数 = 时长 × 帧率 ──
    if FRAME_COUNT is not None:
        if SET_FPS_TO_MATCH:
            want = FRAME_COUNT / TARGET_SECONDS
            scene.render.fps = int(round(want))
            scene.render.fps_base = 1.0
            fps = float(scene.render.fps)
            print("[运镜] 已把场景帧率改为 %g fps，使 %d 帧 = %.3f 秒（注意：会影响场景里其它动画）"
                  % (fps, FRAME_COUNT, TARGET_SECONDS))
        duration = max(1.0, FRAME_COUNT - 1.0) / fps
        frame_end = FRAME_COUNT
        if abs(duration - TARGET_SECONDS) > 0.05:
            print("[运镜] 警告：你指定了 FRAME_COUNT=%d，它在 %g fps 下只有 %.3f 秒"
                  "（目标 %.1f 秒 → 运镜会快 %.2f 倍）。"
                  % (FRAME_COUNT, fps, duration, TARGET_SECONDS, TARGET_SECONDS / duration))
            print("[运镜]        若想两者同时成立：把 SET_FPS_TO_MATCH 改 True"
                  "（帧率会变成 %.2f），或去掉 FRAME_COUNT 让帧数按时长自动推导。"
                  % (FRAME_COUNT / TARGET_SECONDS))
    else:
        duration = TARGET_SECONDS
        frame_end = 1 + int(round(TARGET_SECONDS * fps))

    time_scale = duration / REFERENCE_DURATION
    print("[运镜] 时长优先：帧数 = 时长 × 帧率 → %g fps × %.3f 秒 = %d 帧"
          "（帧范围 1..%d）；分镜表按 %.0f 秒基准等比缩放 ×%.3f"
          % (fps, duration, frame_end, frame_end, REFERENCE_DURATION, time_scale))

    if cam.parent is not None:
        print("[运镜] 提示：相机有父级 %s，位置/旋转会换算成父级局部空间" % cam.parent.name)
    if cam.constraints:
        print("[运镜] 警告：相机有 %d 个约束(%s)，约束会在动画之后生效，机位可能不准"
              % (len(cam.constraints), ", ".join(c.name for c in cam.constraints)))

    # 保留原有动画：新建 action 并切过去
    if cam.animation_data is None:
        cam.animation_data_create()
    old_action = cam.animation_data.action
    new_action = bpy.data.actions.new(ACTION_NAME)
    cam.animation_data.action = new_action
    print("[运镜] 新 action: %s ；原 action: %s（未删除，需要时可切回）"
          % (ACTION_NAME, old_action.name if old_action else "无"))

    # 父级换算用矩阵
    if cam.parent is not None:
        pmat = cam.parent.matrix_world @ cam.matrix_parent_inverse
        pmat_inv = pmat.inverted()
    else:
        pmat_inv = None

    mode = cam.rotation_mode
    use_quat = (mode == 'QUATERNION')
    prev_euler = None
    focus_keys = []
    speeds = []

    n_keys = 0
    t = 0.0
    while True:
        frame = 1 + t * fps
        theta, radius, height, aim_h, dutch = evaluate(min(t, duration), time_scale)
        pos, rot, dist = camera_pose(theta, radius, height, aim_h, dutch)
        basis = Matrix.Translation(pos) @ rot
        if pmat_inv is not None:
            basis = pmat_inv @ basis

        cam.location = basis.to_translation()
        if use_quat:
            cam.rotation_quaternion = basis.to_quaternion()
        else:
            e = basis.to_euler(mode)
            cur = [e.x, e.y, e.z]
            if prev_euler is not None:
                cur = unwrap_euler(cur, prev_euler)
            prev_euler = cur
            cam.rotation_euler = cur
        cam.keyframe_insert("location", frame=frame)
        cam.keyframe_insert("rotation_quaternion" if use_quat else "rotation_euler",
                            frame=frame)
        if TRACK_FOCUS:
            cam.data.dof.focus_distance = max(0.05, dist)
            cam.data.keyframe_insert("dof.focus_distance", frame=frame)
            focus_keys.append((frame, dist))
        n_keys += 1
        if t >= duration - 1e-9:
            break
        # 自适应步长：快速段按"位移步长"加密, 加/减速段按"偏差容限"加密, 慢速段放宽到 KEY_STEP_MAX
        eps = 1e-3
        p_a, _, _ = camera_pose(*evaluate(min(duration, t + eps), time_scale))
        p_b, _, _ = camera_pose(*evaluate(t, time_scale))
        v = (p_a - p_b).length / eps
        speeds.append(v)
        dt = KEY_STEP_MAX
        if v > 1e-6:
            dt = min(dt, KEY_STEP_ARC / v)
        # 加速度: 用于"缓起缓停"段加密(否则首/末帧手柄会被压平, 吃掉设计的加减速)
        p_c, _, _ = camera_pose(*evaluate(min(duration, t + 0.05), time_scale))
        v2 = (p_c - p_b).length / 0.05
        a = abs(v2 - v) / 0.05
        if a > 1e-6:
            dt = min(dt, math.sqrt(2.0 * KEY_STEP_TOL / a))
        # 首/末帧的贝塞尔手柄没有外侧邻居, 会被 AUTO_CLAMPED 压平 —— 那里再加密一档,
        # 把"设计的起手加速"与"收尾减速"保留下来。
        if t < 0.15 * time_scale or t > duration - 0.15 * time_scale:
            dt = min(dt, 0.05 * time_scale)
        dt = max(1.0 / fps, dt)
        t = min(duration, t + dt)

    # 关键帧手柄：自动钳制(不越界、平滑)
    curves = action_fcurves(new_action)
    for fc in curves:
        for kp in fc.keyframe_points:
            kp.interpolation = 'BEZIER'
            kp.handle_left_type = 'AUTO_CLAMPED'
            kp.handle_right_type = 'AUTO_CLAMPED'
        fc.update()

    scene.frame_start = 1
    scene.frame_end = frame_end
    scene.frame_set(1)

    print("[运镜] 已写入 %d 个关键帧（自适应间距 %.0f-%.2fs），帧范围 %d-%d（共 %d 帧，%.3f 秒）"
          % (n_keys, 1.0 / fps, KEY_STEP_MAX * time_scale, scene.frame_start,
             scene.frame_end, frame_end, duration))
    if speeds:
        peak = max(speeds)
        print("[运镜] 速度：平均 %.2f m/s，峰值 %.2f m/s（10 秒设计基准 1.87 m/s）"
              % (sum(speeds) / len(speeds), peak))
        if peak > SPEED_WARN:
            print("[运镜] 提示：峰值 %.1f m/s 偏快 —— 说明实际时长比 %.0f 秒短了。"
                  "把 TARGET_SECONDS 调回 %.0f（或去掉 FRAME_COUNT）即可恢复原节奏。"
                  % (peak, REFERENCE_DURATION, REFERENCE_DURATION))
    print("[运镜] 速度曲线：每段按设计缓动 → 特写缓停 / 环绕两头缓 / 揭示软着陆 / 收尾缓停")
    if focus_keys:
        print("[运镜] 对焦距离已随距离打帧：%.2fm(近) ~ %.2fm(远)"
              % (min(d for _, d in focus_keys), max(d for _, d in focus_keys)))
    print("[运镜] 现在按空格即可播放；游戏侧会同步跟随。")

    # 镜头/景别参考（方便判断特写够不够紧；若镜头不是 50mm 想重算，告诉我焦距即可）
    try:
        rx, ry = scene.render.resolution_x, scene.render.resolution_y
        sw = cam.data.sensor_width
        sh = cam.data.sensor_height if cam.data.sensor_fit != 'AUTO' else \
            sw * (ry / float(rx) if cam.data.sensor_fit == 'VERTICAL' else 1.0)
        f = cam.data.lens
        # 以 50mm/36mm 传感器为常见基准，横向视角
        fov_h = 2.0 * math.degrees(math.atan(sw / (2.0 * f)))
        d_close = 0.72
        print("[运镜] 镜头 %.1fmm / 传感器宽 %.1fmm → 水平视角 %.1f°；"
              "特写机位(离头心 %.2fm)画面宽约 %.2fm（成人头部宽约 0.22m）"
              % (f, sw, fov_h, d_close, 2.0 * d_close * math.tan(math.radians(fov_h / 2.0))))
    except Exception as exc:  # noqa: BLE001
        print("[运镜] 镜头信息读取跳过:", exc)


main()
