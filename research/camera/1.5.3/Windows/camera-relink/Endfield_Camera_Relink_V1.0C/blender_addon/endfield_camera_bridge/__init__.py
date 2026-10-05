bl_info = {
    "name": "Endfield Camera Relink",
    "author": "一块铅矾 (辅助模型: deepseek V4.1 flash / GPT6 Astra)",
    "version": (0, 3, 13),
    "blender": (3, 6, 0),
    "location": "View3D > Sidebar > Endfield Camera",
    "description": "Endfield Camera Relink V1.0C - streams the active Blender camera to the game camera",
    "category": "Camera",
}

import hashlib
import json
import math
import mmap
import os
import shutil
import socket
import struct
import tempfile
import time

import bpy
from mathutils import Matrix, Vector

# ---- 运镜原语: 逐字搬自 tools\anim\showcase_120fps_10s.py(已实拍验收) ----
EASES = {
    "linear": lambda x: x,
    "ease_in": lambda x: 1.0 - math.cos(x * math.pi * 0.5),           # 慢起
    "ease_out": lambda x: math.sin(x * math.pi * 0.5),                # 缓停
    "ease_in_out": lambda x: 0.5 - 0.5 * math.cos(math.pi * x),       # 两头缓
    "ease_in_cubic": lambda x: x ** 3,                                # 明显加速
    "ease_out_cubic": lambda x: 1.0 - (1.0 - x) ** 3,
}


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


MAP_NAME = "EndfieldCameraBridgeV1"
PACKET = struct.Struct("<4sIQdIi3f3f3f8f5fQ")
PACKET_SIZE = 128
MAGIC = b"ECB1"
VERSION = 1

FLAG_ENABLED = 1 << 0
FLAG_TRANSFORM = 1 << 1
FLAG_LENS = 1 << 2
FLAG_SUPPRESS_GAME_CONTROLLER = 1 << 3
# v1.1: 包里 5 个备用浮点(偏移 100/104/108)放参考物件位置 —— 它就是位置基线的原点
FLAG_REFERENCE = 1 << 4
# v0.2.2: 镜头模式。位 5 起空闲, 因此不改包长/协议版本:
#   置位   = 随角色镜头: 模块每帧读实时角色坐标 + 绝对写 transform(会关闭自由相机以便读取)
#   不置位 = 定镜头:     模块冻结角色坐标(武装瞬间取一次), 相机定点不动
# 两种模式都由模块每帧绝对写相机变换(pose_mode=3), 差别只在游戏侧原点是常量还是实时值。
# 面板上切换会立刻生效(下一个包就带上), 不需要重新武装。
FLAG_FOLLOW = 1 << 5
# v0.3.8: "隐藏游戏 UI(拍摄用)"。位 6。
#   置位   = 请求隐藏 HUD。模块用游戏自己的 AddUICamCullingMaskConfig("ECLHideUI", 0)
#            + _UpdateUICamCullingMask() 把**只渲染层 5 的 UICamera** 的遮罩压成 0 ——
#            世界由 MainCamera 渲染且它不含位 5, 所以动它碰不到世界、也不影响键位与攻击。
#   不置位 = 显示(撤销上面那条配置, 让游戏自己重算遮罩)。
# 实测(v1.0.0aj 指令 65): UI 整体消失; 移动/普通攻击/技能全部照常; 可逆。
# 注意: 你在游戏里主动呼出界面(ESC 菜单等)时, 游戏会重建它自己的配置栈 ——
#       模块默认"尊重游戏"会放行让界面显示, 等你关掉界面后自动重新隐藏。
FLAG_HIDE_UI = 1 << 6
# v0.3.10/11: 隐藏策略与范围都变了 —— 模块现在默认**严格保持**(全藏, 连 ESC 菜单/背包也不显示),
# 因为"尊重游戏"会让战斗中的 HUD(体力条/状态条)随游戏加层而整块回来。面板文案已同步。
# v0.3.9: 参考物件固定为这个物体名(面板上不再有"用不用参考物件"的开关与选物框)。
# 角色站位的替身就是它; 把它摆在角色脚下(地面高度即可, 不必精确)。
REFERENCE_OBJECT_NAME = "Cube"

# 武装脚本(相机控制台.cmd 菜单 [3] / arm_lens.ps1)会往系统临时目录写一个令牌;
# 插件发现新令牌就自动执行一次「姿态水平归零 + 位置归零」(固定行为, 不再做成开关)。
ARM_TOKEN_FILE = os.path.join(tempfile.gettempdir(), "EndfieldCameraLink_arm.txt")
# 面板选的镜头模式也会同步到一个文件, 好让武装脚本知道该不该开自由相机。
MODE_FILE = os.path.join(tempfile.gettempdir(), "EndfieldCameraLink_mode.txt")
# v0.2.4: 外部设置令牌(由 tools\ensure_blender.ps1 写)。内容形如:
#   "2026-09-11 23:55:01 fps=120 rate=120 autostart=1"
# 插件每 0.25 秒轮询一次, 内容与上次不同就应用一次 —— 用来让「控制台 [2] 启动游戏」这一步
# 顺手把 Blender 调成 120 帧拍摄状态(输出帧率 + 发送频率 + 自动开始发送), 不用人工点面板。
SETTINGS_FILE = os.path.join(tempfile.gettempdir(), "EndfieldCameraLink_settings.txt")

# v0.3.7: 发送频率上限 240 → 1000。
# 背景(v1.0.0y 复盘): 链路的位姿写入是"按有没有新包"触发的 —— 某游戏帧没有新包就不写位姿,
# 那一帧会露出游戏自己的相机位姿; 包率只要 ≤ 游戏帧率就必然漏帧 → 高强度运镜抽动。
# 实测 240(游戏 120fps 的 2×)已够用, 但 240 的上限让人无法再往上加余量, 因此抬高到 1000。
# 注意: 上限只表示"允许设置到多少" —— Blender 的定时器由 UI 事件循环调度, 1000Hz(1ms)
# 实际达不到, 面板值只是目标; 而且包构建成本随频率线性上升。240~480 是实际可用的区间。
RATE_MIN = 1
RATE_MAX = 1000

_mapping = None
_sequence = 0
_running = False
_arm_token = None
_last_arm_poll = 0.0
_last_mode_written = None
_last_settings_token = None
_timer_registered = False



def _to_unity(v):
    # Blender RH/Z-up -> Unity LH/Y-up. One Blender unit is treated as one metre.
    return (float(v.x), float(v.z), float(v.y))


def _sync_mode_file(scene):
    """把面板选的镜头模式写到临时文件, 供武装脚本决定要不要开官方自由相机。
    随角色模式必须让"快照/关卡相机"保持激活(偏移只对它有效), 也就是**不能**开自由相机。"""
    global _last_mode_written
    mode = 'follow' if scene.ef_bridge_lens_mode == 'FOLLOW' else 'static'
    if mode == _last_mode_written:
        return
    try:
        with open(MODE_FILE, "w", encoding="utf-8") as fh:
            fh.write(mode)
        _last_mode_written = mode
    except OSError:
        pass


def _open_mapping():
    global _mapping
    if _mapping is None:
        _mapping = mmap.mmap(-1, PACKET_SIZE, tagname=MAP_NAME, access=mmap.ACCESS_WRITE)
    return _mapping


def _reference_unity(scene):
    """参考物件(位置基线的原点)的 Unity 坐标。

    v0.3.9: **固定用名为 Cube 的物体** —— 面板上的"以参考物件为原点"开关与选物框都去掉了:
    这条链路本来就只有绝对锚点这一种可靠用法(增量锚点会随武装时刻漂), 留个开关只会让人
    不知不觉用错模式。找不到 Cube 时返回 None(退回旧增量行为), 面板会写明。
    """
    obj = bpy.data.objects.get(REFERENCE_OBJECT_NAME)
    if obj is None:
        return None
    return _to_unity(obj.matrix_world.translation * scene.ef_bridge_scale)


def _camera_packet(scene, stable_sequence):
    camera_obj = scene.camera
    if camera_obj is None or camera_obj.type != 'CAMERA':
        raise RuntimeError("场景没有活动相机")

    q = camera_obj.matrix_world.to_quaternion()
    position = _to_unity(camera_obj.matrix_world.translation * scene.ef_bridge_scale)
    forward = _to_unity(q @ Vector((0.0, 0.0, -1.0)))
    up = _to_unity(q @ Vector((0.0, 1.0, 0.0)))
    cam = camera_obj.data

    flags = FLAG_ENABLED | FLAG_TRANSFORM | FLAG_LENS
    if scene.ef_bridge_suppress_controller:
        flags |= FLAG_SUPPRESS_GAME_CONTROLLER
    if scene.ef_bridge_lens_mode == 'FOLLOW':
        flags |= FLAG_FOLLOW
    if scene.ef_bridge_hide_ui:
        flags |= FLAG_HIDE_UI
    _sync_mode_file(scene)
    ref = _reference_unity(scene)
    if ref is not None:
        flags |= FLAG_REFERENCE
        ref5 = (float(ref[0]), float(ref[1]), float(ref[2]), 0.0, 0.0)
    else:
        ref5 = (0.0, 0.0, 0.0, 0.0, 0.0)

    fps_base = scene.render.fps_base if scene.render.fps_base else 1.0
    fps = float(scene.render.fps) / float(fps_base)
    focus_distance = float(cam.dof.focus_distance) if cam.dof else 10.0
    f_stop = float(cam.dof.aperture_fstop) if cam.dof else 2.8
    values = (
        MAGIC, VERSION, stable_sequence, time.perf_counter(), flags, int(scene.frame_current),
        *position, *forward, *up,
        float(cam.lens), float(cam.sensor_width), float(cam.sensor_height),
        float(cam.clip_start), float(cam.clip_end), focus_distance, f_stop, fps,
        *ref5,
        stable_sequence,
    )
    return PACKET.pack(*values)


def send_camera(scene):
    global _sequence
    mapping = _open_mapping()
    _sequence += 2
    stable = _sequence

    # Mark the slot unstable before replacing the complete packet.
    mapping.seek(8)
    mapping.write(struct.pack("<Q", stable | 1))
    mapping.seek(0)
    mapping.write(_camera_packet(scene, stable))
    mapping.flush()


def level_camera(obj):
    """把相机姿态调成与地面水平: 保留水平朝向角, 俯仰与滚转归零。
    返回 None 表示成功, 否则返回失败原因字符串。"""
    if obj is None or obj.type != 'CAMERA':
        return "场景没有活动相机"
    m = obj.matrix_world.to_3x3()
    look = m @ Vector((0.0, 0.0, -1.0))   # Blender 相机看自己本地 -Z
    look.z = 0.0
    if look.length < 1e-4:
        # 相机几乎垂直朝上/朝下: 用当前右向量推出水平朝向(名义上保留 yaw)
        right = m @ Vector((1.0, 0.0, 0.0))
        right.z = 0.0
        if right.length < 1e-4:
            right = Vector((1.0, 0.0, 0.0))
        look = Vector((-right.y, right.x, 0.0))
    if look.length < 1e-4:
        return "无法推断水平朝向(相机姿态退化)"
    look.normalize()
    up = Vector((0.0, 0.0, 1.0))
    back = -look
    x_axis = up.cross(back)          # 相机本地 X(右)
    x_axis.normalize()
    y_axis = back.cross(x_axis)      # 相机本地 Y(上)
    rot = Matrix(((x_axis.x, y_axis.x, back.x, 0.0),
                  (x_axis.y, y_axis.y, back.y, 0.0),
                  (x_axis.z, y_axis.z, back.z, 0.0),
                  (0.0, 0.0, 0.0, 1.0)))
    loc = obj.matrix_world.translation.copy()
    obj.matrix_world = Matrix.Translation(loc) @ rot
    bpy.context.view_layer.update()
    return None


def zero_camera_location(obj):
    """把相机世界位置设为 (0,0,0)（保留朝向）。有父级也能正确处理。
    返回 None 表示成功，否则返回失败原因。"""
    if obj is None or obj.type != 'CAMERA':
        return "场景没有活动相机"
    mw = obj.matrix_world.copy()
    mw.translation = Vector((0.0, 0.0, 0.0))
    obj.matrix_world = mw
    bpy.context.view_layer.update()
    return None


def _poll_arm_request(scene):
    """武装脚本写了新令牌 -> 执行一次「姿态水平归零 + 位置归零」。
    v0.2.2: 这两件事固定执行(它们是拍片工作状态的定义), 不再做成面板开关。"""
    global _arm_token
    try:
        st = os.stat(ARM_TOKEN_FILE)
        with open(ARM_TOKEN_FILE, "r", encoding="utf-8") as fh:
            token = fh.read().strip()
    except OSError:
        return
    if not token or token == _arm_token:
        return
    first = _arm_token is None
    _arm_token = token
    if first and (time.time() - st.st_mtime) > 600:
        # 插件刚启动, 而令牌是很久以前某次武装留下的: 不追溯执行
        return
    cam = scene.camera
    no_zero = token.endswith("nozero")     # arm_lens.ps1 -NoZero
    acts = []
    err = level_camera(cam)
    acts.append("水平归零" if not err else ("水平失败(%s)" % err))
    if no_zero:
        acts.append("位置归零(跳过)")
    else:
        err = zero_camera_location(cam)
        acts.append("位置归零" if not err else ("位置失败(%s)" % err))
    scene.ef_bridge_arm_status = "武装 %s: %s" % (token, ", ".join(acts))


def _apply_settings(scene):
    """v0.2.4: 读外部设置令牌(由 tools\\ensure_blender.ps1 写), 内容变了就应用一次。
    支持: fps=<输出帧率> rate=<发送频率> autostart=<0|1>"""
    global _last_settings_token, _running
    try:
        with open(SETTINGS_FILE, "r", encoding="utf-8") as fh:
            token = fh.read().strip()
    except OSError:
        return
    if not token or token == _last_settings_token:
        return
    _last_settings_token = token
    vals = {}
    for part in token.replace("\n", " ").split():
        if "=" in part:
            k, _, v = part.partition("=")
            vals[k.strip().lower()] = v.strip()
    acts = []
    try:
        if "fps" in vals:
            fps = max(1, min(1000, int(float(vals["fps"]))))
            scene.render.fps_base = 1.0        # 免得 fps/fps_base 凑出小数帧率
            scene.render.fps = fps
            acts.append("输出帧率=%d" % fps)
        if "rate" in vals:
            rate = max(RATE_MIN, min(RATE_MAX, int(float(vals["rate"]))))   # v0.3.7: 上限 1000
            scene.ef_bridge_rate = rate
            acts.append("发送频率=%d" % rate)
        if vals.get("autostart", "0") not in ("0", "false", "no", ""):
            if not _running:
                _running = True
                _register_timer()
            acts.append("已自动开始发送")
        scene.ef_bridge_ext_status = "外部设置: " + (", ".join(acts) if acts else token)
    except Exception as exc:  # noqa: BLE001
        scene.ef_bridge_ext_status = "外部设置应用失败: %s" % exc


def _timer():
    global _running, _last_arm_poll, _timer_registered
    # v0.2.4: 这个轮询计时器**一直在跑**(在 register() 里就注册), 于是:
    #   · 武装令牌即使你没点「开始实时发送」也会被执行;
    #   · 外部设置令牌(帧率/发送频率/自动开始发送)才可能生效 —— 否则会先有鸡先有蛋:
    #     自动开始发送要靠轮询, 而轮询原本只在"已开始发送"时才跑。
    scene = bpy.context.scene
    try:
        now = time.perf_counter()
        if now - _last_arm_poll > 0.25:
            _last_arm_poll = now
            _poll_arm_request(scene)
            _apply_settings(scene)
        if _running:
            send_camera(scene)
            scene.ef_bridge_status = "正在发送"
    except Exception as exc:
        scene.ef_bridge_status = str(exc)
    hz = max(RATE_MIN, min(RATE_MAX, int(scene.ef_bridge_rate)))
    return 1.0 / hz


def _register_timer():
    """注册(或在已注册时保留)常驻轮询计时器。设计成幂等, 供 register() 与"开始发送"共用。"""
    global _timer_registered
    try:
        if not bpy.app.timers.is_registered(_timer):
            bpy.app.timers.register(_timer, first_interval=0.0, persistent=True)
        _timer_registered = True
    except Exception:  # noqa: BLE001
        pass


class EFBRIDGE_OT_start(bpy.types.Operator):
    bl_idname = "ef_bridge.start"
    bl_label = "开始实时发送"

    def execute(self, context):
        global _running
        _running = True
        _register_timer()
        context.scene.ef_bridge_status = "正在启动"
        return {'FINISHED'}


class EFBRIDGE_OT_stop(bpy.types.Operator):
    bl_idname = "ef_bridge.stop"
    bl_label = "停止"

    def execute(self, context):
        global _running
        _running = False
        context.scene.ef_bridge_status = "已停止"
        return {'FINISHED'}


class EFBRIDGE_OT_send_once(bpy.types.Operator):
    bl_idname = "ef_bridge.send_once"
    bl_label = "发送当前帧"

    def execute(self, context):
        try:
            send_camera(context.scene)
            context.scene.ef_bridge_status = "已发送当前帧"
            return {'FINISHED'}
        except Exception as exc:
            self.report({'ERROR'}, str(exc))
            return {'CANCELLED'}


# ---- v0.3.13: 「修复虚化」按钮 ----
# 为什么需要(2026-09-15 实测定案): 光圈/对焦是通过 `SnapshotCameraController` 落到主相机上的,
#   而**进过一次官方相机(拍照/营销)之后, 游戏会换掉那个快照相机控制器** —— 模块手里缓存的那个
#   就陈旧了(SetAperture 不报错、但画面毫无反应, 表现就是"自由相机的虚化失效")。
#   修复 = 让模块重新取一次实例(指令 78: 重取 + 把当前镜头参数喂回 + Apply)。
CMD_PORT = 9601  # 模块的指令通道(UDP 127.0.0.1), 与 tools\send_cmd2.ps1 同一个端口


def send_command(cmd_id, a0=0.0, a1=0.0, a2=0.0, a3=0.0, repeat=3):
    """向模块发一个 ECM2 指令包(与 tools\\send_cmd2.ps1 完全同一套协议)。

    包格式: magic b'ECM2' + u32 id + float a0..a3 = 24 字节。
    这里只用它做"一次性动作"按钮(修复虚化); 位姿仍然走共享内存那条通道。
    """
    buf = struct.pack("<4sIffff", b"ECM2", int(cmd_id), float(a0), float(a1), float(a2), float(a3))
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        for _ in range(max(1, int(repeat))):
            sock.sendto(buf, ("127.0.0.1", CMD_PORT))
    finally:
        sock.close()


class EFBRIDGE_OT_fix_dof(bpy.types.Operator):
    bl_idname = "ef_bridge.fix_dof"
    bl_label = "修复虚化"
    bl_description = ("进过一次官方相机(拍照/营销)后, 自由相机的虚化会失效 —— 游戏换掉了快照相机控制器, "
                      "模块手里的那个已经陈旧。点这里让模块重新取一次实例, 并把当前镜头参数喂回去 "
                      "(指令 78)。")

    def execute(self, context):
        try:
            send_command(78, repeat=3)
            context.scene.ef_bridge_status = "已发送: 修复虚化 (指令 78)"
            return {'FINISHED'}
        except Exception as exc:  # noqa: BLE001
            self.report({'ERROR'}, "修复虚化失败: %s" % exc)
            return {'CANCELLED'}


class EFBRIDGE_PT_panel(bpy.types.Panel):
    bl_label = "Endfield Camera"
    bl_idname = "EFBRIDGE_PT_panel"
    bl_space_type = 'VIEW_3D'
    bl_region_type = 'UI'
    bl_category = "Endfield Camera"

    def draw(self, context):
        layout = self.layout
        scene = context.scene
        layout.prop(scene, "camera")
        layout.prop(scene, "ef_bridge_rate")
        layout.prop(scene, "ef_bridge_scale")
        layout.prop(scene, "ef_bridge_suppress_controller")

        # v0.3.9: 位置原点固定为参考物件(名为 Cube 的物体) —— 去掉"用不用参考物件"的开关
        # 与选物框, 只留一行状态。理由: 那条链路只有绝对锚点这一种可靠用法, 留开关只会让人
        # 不知不觉用错模式(增量锚点会随武装时刻漂)。
        ref = _reference_unity(scene)
        if ref is None:
            layout.label(text="位置原点: 场景里没有 %s(退回增量锚点)" % REFERENCE_OBJECT_NAME,
                         icon='ERROR')
        else:
            layout.label(text="位置原点: %s → (%.2f, %.2f, %.2f)"
                              % (REFERENCE_OBJECT_NAME, ref[0], ref[1], ref[2]),
                         icon='OBJECT_DATA')

        box2 = layout.box()
        box2.label(text="镜头模式")
        box2.prop(scene, "ef_bridge_lens_mode", text="")
        if scene.ef_bridge_lens_mode == 'FOLLOW':
            box2.label(text="随角色: 每帧读实时角色坐标并跟随(会关闭自由相机)", icon='TRACKER')
        else:
            box2.label(text="定镜头: 武装瞬间冻结角色坐标, 相机定点不动", icon='LOCKED')
        # v0.3.8: 隐藏游戏 UI(拍摄用)。模块只压渲染层(UI 相机 + 主相机可选清层), 不碰输入。
        rowui = box2.row(align=True)
        rowui.prop(scene, "ef_bridge_hide_ui")
        if scene.ef_bridge_hide_ui:
            box2.label(text="隐藏 HUD: 只压 UI 相机遮罩, 键位/攻击不受影响", icon='HIDE_ON')
        # v0.3.13: 修复虚化 —— 进过官方相机后自由相机的虚化会失效(游戏换了快照相机控制器), 点一下救回来
        # (说明放在按钮的 tooltip 里, 面板上不再多占一行文字)
        box2.operator("ef_bridge.fix_dof", icon='LOOP_BACK')
        if scene.ef_bridge_arm_status:
            box2.label(text=scene.ef_bridge_arm_status[:48])
        # v0.3.9: 不再显示"外部设置: ..."那行固定文本(控制台 [2] 启动游戏仍会调 Blender,
        # 只是不再占用面板; 需要确认时看 ef_bridge_ext_status 属性或日志)。

        row = layout.row(align=True)
        row.operator("ef_bridge.start", icon='PLAY')
        row.operator("ef_bridge.stop", icon='PAUSE')
        layout.operator("ef_bridge.send_once", icon='EXPORT')
        layout.label(text="状态：" + scene.ef_bridge_status)
        layout.label(text="通道：" + MAP_NAME)

        # ---- 运镜预设 ----
        # 【重要】这一段在 draw 里跑: 只能**读**数据, 不能写 Blender 属性。
        # 写过一次(把枚举同步回 index)—— 结果切换预设时该面板剩余绘制被跳过,
        # "生成这个运镜"按钮直接消失。同步现在放在枚举的 update 回调里。
        box3 = layout.box()
        box3.label(text="运镜预设", icon='CAMERA_DATA')
        try:
            rowd = box3.row(align=True)
            rowd.prop(scene, "ef_bridge_preset_dir", text="")
            rowd.operator("ef_bridge.preset_open_dir", text="", icon='FILE_FOLDER')
            rowd.operator("ef_bridge.preset_refresh", text="", icon='FILE_REFRESH')
            if not (scene.ef_bridge_preset_dir or '').strip():
                # 字段留空 = 用包内目录(v0.3.6 起预设数据与路径设置都在包内)
                box3.label(text="目录(包内): " + _default_preset_dir()[:52])
            if _preset_note:
                box3.label(text=_preset_note[:52], icon='INFO')
            presets = _scan_presets(scene)
            if not presets:
                box3.label(text="目录里还没有 .py 预设(点文件夹图标打开目录放进去)", icon='INFO')
                box3.label(text="目录: " + (scene.ef_bridge_preset_dir or _default_preset_dir()))
            else:
                box3.prop(scene, "ef_bridge_preset", text="")
                # v0.3.9: 下标再夹一次 —— _preset_current_index 已经夹过, 但目录刚被换掉/
                # 缓存刚失效时仍可能越界, 一旦越界整个预设区就只剩一行报错。
                meta = presets[min(_preset_current_index(scene, presets), len(presets) - 1)]
                if meta.get('desc'):
                    box3.label(text=str(meta['desc'])[:52])
                box3.label(text="文件: %s" % meta.get('file', ''), icon='FILE_SCRIPT')
                # 通用参数: 运镜时间(秒) —— 关键帧按它等比缩放; 标签里带上该预设的默认值
                # v0.3.9: "默认秒数"曾经直接 float() 抛错(预设头里写了非数字就会让整个
                # 预设区变成一行报错) —— 现在容错, 解析不出就按 8.0 显示。
                try:
                    secs_default = float(meta.get('seconds', 8.0) or 8.0)
                except (TypeError, ValueError):
                    secs_default = 8.0
                rowt = box3.row(align=True)
                rowt.prop(scene, "ef_bridge_duration",
                          text="运镜时间(秒, 默认 %.1f)" % secs_default)
                # v0.3.9: 面板只注册了 ef_bridge_p0..p3, 预设若声明更多参数,
                # 老代码会去访问不存在的 ef_bridge_p4 → AttributeError → 整个预设区报错。
                params = list(meta.get('params') or [])
                for i, p in enumerate(params[:PRESET_PARAM_MAX]):
                    box3.prop(scene, "ef_bridge_p%d" % i,
                              text=str(p.get('label', p.get('key', '')))[:24])
                if len(params) > PRESET_PARAM_MAX:
                    box3.label(text="该预设声明了 %d 个参数, 面板最多调前 %d 个(其余用默认值)"
                                    % (len(params), PRESET_PARAM_MAX), icon='INFO')
                rowr = box3.row(align=True)
                rowr.operator("ef_bridge.preset_run", icon='PLAY')
                rowr.operator("ef_bridge.preset_defaults", text="", icon='LOOP_BACK')
                if scene.ef_bridge_preset_status:
                    box3.label(text=scene.ef_bridge_preset_status[:52])
                # v0.3.9 修: 这里原来写的是 icon='UNDO' —— Blender 5.x 的图标枚举里**没有**
                # UNDO(1033 个合法名里查不到), 于是 label() 抛异常、预设区剩余绘制全被跳过,
                # 面板上只剩一行"预设面板出错: UILayout.label(): ... enum \"UNDO\" not found"。
                box3.label(text="生成到独立 action(不覆盖); 出错可 Ctrl+Z", icon='LOOP_BACK')
        except Exception as exc:  # noqa: BLE001  预设出问题不能把整个面板拖垮
            # v0.3.9: 面板只放得下一行, 完整 traceback 写进预设日志(以前只有一行摘要,
            # 出问题时无从下手 —— 这次就是靠猜图标名才找到的)。
            _log_preset_panel_error(exc)
            box3.label(text="预设面板出错: %s: %s" % (type(exc).__name__, str(exc)[:40]),
                       icon='ERROR')
            box3.label(text="详情见 %s" % PRESET_LOG, icon='TEXT')


# ============================================================================
# 运镜预设 (v0.3.0)
#
# 需求: 在插件里读取"指定文件夹"内的运镜脚本, 点一下就在 Blender 里执行并生成对应运镜。
# 设计(与 docs\06 的评估一致):
#   · 预设 = 一个 .py 文件, 文件头用一行 `# EFPRESET: {...}` 描述自己(名字/说明/参数),
#     宿主**只解析这一行**就能列出预设 —— "列出预设"这个动作不执行任何代码。
#   · 生成规则: 一律**新建独立 action**(EF_Preset_<名字>, 重名自动加序号), 绝不覆盖用户动画;
#     生成前 undo_push, 出错可 Ctrl+Z 整体撤销。
#   · 时长一律用"秒"表达, 帧数 = 秒 × 场景帧率(换帧率不会让运镜变快变慢)。
#   · 自适应关键帧加密(弧长/加速度容限/首尾加密)与 Blender 5.x 槽位动作兼容层,
#     都**逐字搬自** tools\anim\showcase_120fps_10s.py 里已实拍验收过的那份实现。
# ============================================================================

PRESET_HEADER = "# EFPRESET:"
PRESET_PARAM_MAX = 4          # 面板最多暴露 4 个数值参数(够用且实现简单)
PRESET_LOG = os.path.join(tempfile.gettempdir(), "EndfieldCameraLink_preset.log")

_preset_cache = None
_preset_cache_dir = None
_preset_note = ""             # 目录创建/示例复制之类的提示 —— 不在 draw 里写 Blender 数据
_bundled_synced = False       # 每次会话只同步一次示例预设


def _log_preset_panel_error(exc):
    """把"预设面板出错"的完整 traceback 追加到预设日志。

    v0.3.9 加的: 面板上只能显示一行摘要, 而这类错误几乎都是"某个 API 参数写错"
    (例如 icon='UNDO' 在 Blender 5.x 已不存在) —— 没有 traceback 就只能靠猜。
    只在出错时写文件, 不影响正常绘制。"""
    import traceback
    try:
        with open(PRESET_LOG, "a", encoding="utf-8") as lf:
            lf.write("=== 预设面板出错: %s: %s ===\n%s\n"
                     % (type(exc).__name__, exc, traceback.format_exc()))
    except OSError:
        pass


def _collect_icon_literals():
    """扫本文件里所有**真实调用**中 icon='...' 的实参, 返回 [(行号, 图标名)]。

    用 AST 而不是正则: 正则会把注释/文档字符串里写的 `icon='UNDO'` 这类说明文字也算进去
    (第一版就是这么被自己坑的 —— 注释里举例子反而触发了 4 个假阳性)。
    只认 `f(..., icon='X')` 这种字面量; 动态传参(icon=变量)无法静态检查, 不在此列。
    拆成独立函数是为了让自检脚本也能直接调用它(见 source\\tools\\verify_addon_selftest.py)。"""
    import ast
    out = []
    try:
        with open(os.path.abspath(__file__), encoding="utf-8") as fh:
            src = fh.read()
        tree = ast.parse(src)
    except (OSError, SyntaxError):
        return out
    for node in ast.walk(tree):
        if not isinstance(node, ast.Call):
            continue
        for kw in node.keywords:
            if (kw.arg == "icon" and isinstance(kw.value, ast.Constant)
                    and isinstance(kw.value.value, str)):
                out.append((kw.value.lineno, kw.value.value))
    return out


def _check_icon_names():
    """启动时自检: 扫本文件里所有 icon='...' 字面量, 与本版本 Blender 的图标枚举比对。

    v0.3.9 加的。成因很具体: 面板里 `box3.label(icon='UNDO')` —— UNDO 在 Blender 5.2 的
    1033 个合法图标里不存在, 于是 label() 抛异常、**预设区剩余绘制全被跳过**, 面板上只剩
    一行"预设面板出错: UILayout.label(): ... enum \\"UNDO\\" not found"。这种错在运行前
    grep 一下就完了, 所以做成启动自检: 发现非法图标名就写进预设日志(只在有问题时写)。
    """
    try:
        icon_param = bpy.types.UILayout.bl_rna.functions["label"].parameters["icon"]
        valid = set(i.identifier for i in icon_param.enum_items)
        if not valid:
            return
        bad = [(line, name) for line, name in _collect_icon_literals() if name not in valid]
        if bad:
            with open(PRESET_LOG, "a", encoding="utf-8") as lf:
                lf.write("=== 图标名自检: 以下 icon='...' 在本版本 Blender 里不存在 ===\n")
                for line, name in bad:
                    lf.write("  __init__.py:%d  icon='%s'\n" % (line, name))
            print("[EndfieldCameraBridge] 警告: 有 %d 个非法图标名(详见 %s)"
                  % (len(bad), PRESET_LOG))
    except Exception:  # noqa: BLE001  自检本身绝不能让插件装不上
        pass


def _bundled_preset_dir():
    """随插件分发的示例预设(插件目录内的 presets/)。它同时是**种子来源**:
    用户/包内的预设目录缺文件时会从这里补齐。"""
    return os.path.join(os.path.dirname(os.path.abspath(__file__)), 'presets')


def _package_root():
    """包根目录(EndfieldCameraLink 包所在的那一层)。
    从何而来: 安装脚本会把 package_dir 写进**已安装插件目录**下的 package_path.txt
    (插件装在 %APPDATA%/.../addons/ 里, 与包根毫无相对关系, 所以必须由安装脚本告知)。
    找不到就返回 None(例如用 ZIP 单独装插件的情况) —— 那时退回用插件目录内的示例预设。"""
    here = os.path.dirname(os.path.abspath(__file__))
    ptr = os.path.join(here, 'package_path.txt')
    try:
        with open(ptr, 'r', encoding='utf-8') as fh:
            d = fh.read().strip()
        if d and os.path.isdir(d):
            return d
    except OSError:
        pass
    return None


def _package_paths_ini():
    root = _package_root()
    if not root:
        return None
    ini = os.path.join(root, 'paths.ini')
    return ini if os.path.isfile(ini) else None


def _ini_value(path, key):
    try:
        with open(path, 'r', encoding='utf-8', errors='replace') as fh:
            for line in fh:
                s = line.strip()
                if s.startswith(';') or s.startswith('#'):
                    continue
                if '=' in s:
                    k, _, v = s.partition('=')
                    if k.strip().lower() == key:
                        return v.strip()
    except OSError:
        pass
    return None


def _default_preset_dir():
    """预设目录(v0.3.6: **放在包内**, 方便整个包分发)。解析顺序:
      ① 包内 paths.ini 的 preset_dir(路径设置也在包内, 改这里即可全局生效);
      ② <包根>/presets;
      ③ 插件目录内的示例预设(单独 ZIP 装插件、没有包根时的兜底);
      ④ %APPDATA%/EndfieldCameraLink/presets(最后兜底)。"""
    ini = _package_paths_ini()
    if ini:
        v = _ini_value(ini, 'preset_dir')
        if v and os.path.isdir(v):
            return v
    root = _package_root()
    if root:
        cand = os.path.join(root, 'presets')
        if os.path.isdir(cand):
            return cand
    bundled = _bundled_preset_dir()
    if os.path.isdir(bundled):
        return bundled
    base = os.environ.get('APPDATA') or os.path.expanduser('~')
    return os.path.join(base, 'EndfieldCameraLink', 'presets')


def _ensure_preset_dir(scene):
    """用户预设目录不存在就创建, 并把随插件分发的示例预设复制进去;
    目录已存在时也会**同步**示例预设(见 _sync_bundled_presets; 用户改过的不动)。
    **注意: 这个函数会在"面板绘制"路径里被调用 —— 绝不写任何 Blender 数据**,
    提示信息放在模块变量 _preset_note 里, 由面板显示 / 算子写入状态行。"""
    global _preset_note, _bundled_synced
    d = scene.ef_bridge_preset_dir or _default_preset_dir()
    if not os.path.isdir(d):
        try:
            os.makedirs(d)
        except OSError as exc:
            _preset_note = "无法创建预设目录: %s" % exc
            return None, _preset_note
        src = _bundled_preset_dir()
        copied = 0
        if os.path.isdir(src):
            for fn in sorted(os.listdir(src)):
                if fn.endswith('.py') and fn != '__init__.py':
                    try:
                        shutil.copyfile(os.path.join(src, fn), os.path.join(d, fn))
                        copied += 1
                    except OSError:
                        pass
        if copied:
            _preset_note = "已创建预设目录并放入 %d 个示例预设" % copied
    if not _bundled_synced:            # 每次会话只同步一次, 不拖慢面板重绘
        _bundled_synced = True
        try:
            if os.path.abspath(d) != os.path.abspath(_bundled_preset_dir()):
                _sync_bundled_presets(scene, d)
        except Exception as exc:  # noqa: BLE001
            _preset_note = "示例预设同步失败(不影响使用): %s" % exc
    return d, None


def _sync_bundled_presets(scene, d):
    """把随插件分发的示例预设同步进用户目录(v0.3.5 修的一个真缺陷)。

    【为什么必须做这件事】早期实现只在"用户目录不存在"时复制一次示例预设 ——
    于是插件升级后用户目录里的旧文件**永远不会更新**, 用户以为升级了却一直在跑旧脚本。
    实测证据: 三个"奇怪的速度/角度跳变"(环绕首尾突变/推拉开头先后退/升降结尾改方向)
    全部来自用户目录里 0.3.0 版的旧预设, 而不是新代码。

    同步规则(先备份再覆盖; 用户改过的一律不动):
      · 用户文件与随包版本字节相同                     -> 什么都不做;
      · 清单记录的上次安装哈希 == 用户文件哈希(没改过) -> 备份旧版到 _bundled_backup/ 后覆盖;
      · 用户改过(哈希与清单记录不符)                   -> 保留他的版本, 新版写到 _bundled_new/;
      · 清单里没有记录(老版本装的)                     -> 视为过期, 备份后覆盖。
    清单文件: <用户目录>/.bundled.json
    """
    global _preset_note
    src = _bundled_preset_dir()
    if not src or not os.path.isdir(src) or not os.path.isdir(d):
        return 0
    manifest_path = os.path.join(d, '.bundled.json')
    manifest = {}
    try:
        with open(manifest_path, 'r', encoding='utf-8') as fh:
            got = json.load(fh)
        if isinstance(got, dict):
            manifest = got
    except (OSError, ValueError):
        manifest = {}

    def sha(path):
        try:
            with open(path, 'rb') as fh:
                return hashlib.sha1(fh.read()).hexdigest()
        except OSError:
            return ''

    added, updated, kept = 0, 0, []
    for fn in sorted(os.listdir(src)):
        if not fn.endswith('.py') or fn == '__init__.py':
            continue
        bpath = os.path.join(src, fn)
        upath = os.path.join(d, fn)
        bh = sha(bpath)
        if not bh:
            continue
        uh = sha(upath) if os.path.exists(upath) else None
        rec = (manifest.get(fn) or {}).get('hash')
        try:
            if uh is None:
                shutil.copyfile(bpath, upath)
                added += 1
            elif uh == bh:
                pass                                  # 已是最新
            elif rec is not None and uh != rec:
                # 用户改过 -> 不动他的文件, 新版放到 _bundled_new/ 供对比
                newdir = os.path.join(d, '_bundled_new')
                os.makedirs(newdir, exist_ok=True)
                shutil.copyfile(bpath, os.path.join(newdir, fn))
                kept.append(fn)
            else:
                # 没改过(或清单里没记录 = 老版本装的) -> 备份后更新
                bakdir = os.path.join(d, '_bundled_backup')
                os.makedirs(bakdir, exist_ok=True)
                if uh is not None:
                    shutil.copyfile(upath, os.path.join(bakdir, fn))
                shutil.copyfile(bpath, upath)
                updated += 1
        except OSError:
            continue
        manifest[fn] = {"hash": bh,
                        "version": ".".join(str(x) for x in bl_info["version"])}
    try:
        with open(manifest_path, 'w', encoding='utf-8') as fh:
            json.dump(manifest, fh, ensure_ascii=False, indent=1)
    except OSError:
        pass

    if updated or added:
        _preset_note = "示例预设已同步(新增 %d / 更新 %d, 旧版在 _bundled_backup)" % (added,
                                                                                updated)
    if kept:
        _preset_note = ("你改过的 %s 未被动, 新版放在 _bundled_new/ 供对比"
                        % ", ".join(kept))[:120]
    return updated + added


def _parse_preset(path):
    """只读文件头几行解析 `# EFPRESET: {json}` —— 不执行任何代码。
    允许元数据跨多行: `# EFPRESET: {` 后继续用 `# ...` 注释行补齐(方便作者排版),
    遇到第一行非注释就停止收集。"""
    meta = {"name": os.path.splitext(os.path.basename(path))[0], "desc": "",
            "seconds": 8.0, "params": []}
    try:
        with open(path, 'r', encoding='utf-8', errors='replace') as fh:
            head = fh.read(8192)
    except OSError:
        return None
    lines = head.splitlines()[:60]
    started = False
    chunks = []
    for line in lines:
        s = line.strip()
        if not started:
            if s.startswith(PRESET_HEADER):
                started = True
                chunks.append(s[len(PRESET_HEADER):].strip())
            continue
        if s.startswith('#'):                      # 续行: 继续收集
            chunks.append(s.lstrip('#').strip())
        else:                                      # 第一行非注释 -> 元数据结束
            break
    if started:
        # 增量解析: 从头往后拼注释行, **一解析成功就停**。
        # 不能"把后面所有 # 行都拼进来" —— JSON 结束后紧跟着的普通说明注释会让它变成非法 JSON
        # (实测踩到: 三个示例预设的元数据全部退化成文件名)。
        candidate = None
        joined = chunks[0] if chunks else ""
        for i in range(len(chunks)):
            if i > 0:
                joined += " " + chunks[i]
            try:
                got = json.loads(joined)
            except ValueError:
                continue
            if isinstance(got, dict):
                candidate = got
            break
        if candidate is not None:
            meta.update(candidate)
        else:
            meta["desc"] = "(EFPRESET 头不是合法 JSON, 已用默认名)"
    meta['params'] = [p for p in meta.get('params', []) if isinstance(p, dict)][:PRESET_PARAM_MAX]
    return meta


def _scan_presets(scene, force=False):
    """扫描预设目录(带缓存: 目录/文件数/最新修改时间没变就复用)。"""
    global _preset_cache, _preset_cache_dir
    d, err = _ensure_preset_dir(scene)
    if not d:
        _preset_cache = []
        return _preset_cache
    try:
        files = sorted(fn for fn in os.listdir(d)
                       if fn.endswith('.py') and not fn.startswith('_'))
    except OSError:
        files = []
    stamp = (d, len(files),
             max([os.path.getmtime(os.path.join(d, f)) for f in files], default=0))
    if not force and _preset_cache is not None and _preset_cache_dir == stamp:
        return _preset_cache
    out = []
    for fn in files:
        meta = _parse_preset(os.path.join(d, fn))
        if meta:
            meta['file'] = fn
            meta['path'] = os.path.join(d, fn)
            out.append(meta)
    _preset_cache = out
    _preset_cache_dir = stamp
    return out


class _EfPresetAPI:
    """注入给预设脚本的 API 对象(预设里叫 ef)。"""

    def __init__(self, scene, params, preset_name, duration=8.0):
        self.scene = scene
        self.camera = scene.camera
        self.params = dict(params)
        self.name = preset_name
        self.duration = max(0.2, float(duration))     # 运镜时间(秒) —— 面板上的那一项
        self.notes = []
        self.dutch_sign = -1.0
        self.fps = float(scene.render.fps) / (float(scene.render.fps_base) or 1.0)
        self.action = None
        self.old_action = None
        self.focus_on = True
        self.key_count = 0
        self.end_frame = 1
        self._prev_euler = None
        self._pmat_inv = None
        self._speeds = []
        self._linear_ranges = []      # [(秒起, 秒止), ...] 这些区间里的关键帧用 LINEAR 插值
        self._eased_ranges = []       # 其余区间(缓动)用 BEZIER + AUTO_CLAMPED

    # ---- 基础 ----
    def log(self, msg):
        self.notes.append(str(msg))

    def seconds(self, s):
        return float(s)

    def frames(self, seconds):
        return int(round(float(seconds) * self.fps))

    def t(self, x):
        """归一化时间 [0,1] → 秒。预设一律按归一化时间书写, 于是"运镜时间"参数一改,
        全部关键帧(位置与时间)就整体**等比缩放** —— 这就是"关键帧按输入时间缩放"的实现方式。"""
        return float(x) * float(self.duration)

    def frame_at(self, seconds):
        return 1 + float(seconds) * self.fps

    def frame(self, x):
        """归一化时间 [0,1] → 帧号。"""
        return self.frame_at(self.t(x))

    def ease(self, kind, x):
        return EASES.get(kind, EASES["linear"])(x)

    def ease_names(self):
        return sorted(EASES.keys())

    def param(self, key, default=0.0):
        try:
            return float(self.params.get(key, default))
        except (TypeError, ValueError):
            return float(default)

    def anchor(self):
        """运镜坐标系原点 = 参考物件(v0.3.9 起固定为名为 Cube 的物体)位置, 找不到就用世界原点。
        约定: 角色头部中心 = 锚点(与游戏侧映射一致)。"""
        obj = bpy.data.objects.get(REFERENCE_OBJECT_NAME)
        if obj is None:
            return Vector((0.0, 0.0, 0.0))
        return obj.matrix_world.translation.copy()

    # ---- 机位 ----
    def pose(self, theta=0.0, radius=1.5, height=0.0, aim_height=0.0, dutch=0.0):
        """球坐标机位 → (世界位置, 旋转矩阵, 到目标距离)。
        θ=0 在角色正前方(角色面向 -X), θ 增大转向角色右手边(+Y)。"""
        th = math.radians(float(theta))
        base = self.anchor()
        pos = base + Vector((-math.cos(th) * float(radius), math.sin(th) * float(radius),
                             float(height)))
        aim = base + Vector((0.0, 0.0, float(aim_height)))
        forward = (aim - pos)
        if forward.length < 1e-6:
            forward = Vector((1.0, 0.0, 0.0))
        forward.normalize()
        up_ref = Vector((0.0, 0.0, 1.0))
        if abs(forward.dot(up_ref)) > 0.999:      # 垂直向下/向上时的退化保护
            up_ref = Vector((0.0, 1.0, 0.0))
        back = -forward
        x_axis = up_ref.cross(back)
        x_axis.normalize()
        y_axis = back.cross(x_axis)
        d = math.radians(float(dutch)) * self.dutch_sign
        x_r = x_axis * math.cos(d) - y_axis * math.sin(d)
        y_r = y_axis * math.cos(d) + x_axis * math.sin(d)
        rot = Matrix(((x_r.x, y_r.x, back.x, 0.0),
                      (x_r.y, y_r.y, back.y, 0.0),
                      (x_r.z, y_r.z, back.z, 0.0),
                      (0.0, 0.0, 0.0, 1.0)))
        return pos, rot, (aim - pos).length

    def unwrap_euler(self, cur, prev):
        """把欧拉角各通道解到与上一帧连续的那一支(避免 360° 跳变)。"""
        out = list(cur)
        for i in range(3):
            while out[i] - prev[i] > math.pi:
                out[i] -= 2 * math.pi
            while out[i] - prev[i] < -math.pi:
                out[i] += 2 * math.pi
        return out

    def _setup(self, action_name):
        cam = self.camera
        if cam.parent is not None:
            pmat = cam.parent.matrix_world @ cam.matrix_parent_inverse
            self._pmat_inv = pmat.inverted()
        if cam.animation_data is None:
            cam.animation_data_create()
        self.old_action = cam.animation_data.action
        base = "EF_Preset_%s" % action_name
        act = bpy.data.actions.get(base)
        n = 2
        while act is not None:                    # 默认新建, 重名加序号, 绝不覆盖
            act = bpy.data.actions.get("%s_%d" % (base, n))
            n += 1
        act = bpy.data.actions.new(base if n == 2 else "%s_%d" % (base, n - 1))
        cam.animation_data.action = act
        self.action = act
        self.log("新 action: %s (原 action %s 未改动)" % (
            act.name, self.old_action.name if self.old_action else "无"))
        return act

    def _apply_pose(self, frame, pos, rot, dist=None, key=True):
        cam = self.camera
        basis = Matrix.Translation(pos) @ rot
        if self._pmat_inv is not None:
            basis = self._pmat_inv @ basis
        cam.location = basis.to_translation()
        use_quat = (cam.rotation_mode == 'QUATERNION')
        if use_quat:
            cam.rotation_quaternion = basis.to_quaternion()
        else:
            e = basis.to_euler(cam.rotation_mode)
            cur = [e.x, e.y, e.z]
            if self._prev_euler is not None:
                cur = self.unwrap_euler(cur, self._prev_euler)
            self._prev_euler = cur
            cam.rotation_euler = cur
        if key:
            cam.keyframe_insert("location", frame=frame)
            cam.keyframe_insert("rotation_quaternion" if use_quat else "rotation_euler",
                                frame=frame)
            if self.focus_on and dist is not None:
                cam.data.dof.focus_distance = max(0.05, dist)
                cam.data.keyframe_insert("dof.focus_distance", frame=frame)
            self.key_count += 1
            self.end_frame = max(self.end_frame, int(round(frame)))

    def key(self, t, theta=0.0, radius=1.5, height=0.0, aim_height=0.0, dutch=0.0):
        """在 t 秒处打一个关键帧(机位用球坐标表达)。"""
        pos, rot, dist = self.pose(theta, radius, height, aim_height, dutch)
        self._apply_pose(self.frame_at(t), pos, rot, dist)
        return pos

    def sweep(self, t0, t1, ease, a, b, step_max=0.25, step_arc=0.12, step_tol=0.004,
              dense_ends=True):
        """把机位从 a 平滑变到 b, 并自适应加密关键帧。
        a / b 是 dict: theta / radius / height / aim_height / dutch。
        自适应策略就是 showcase 里那套: 快速段按弧长(0.12m)加密, 加减速段按偏差容限
        (0.004m)加密, 慢速段放宽到 0.25s, 首尾 0.15s 再加密一档防止手柄被压平。"""
        keys = ("theta", "radius", "height", "aim_height", "dutch")
        pa = {k: float(a.get(k, 0.0)) for k in keys}
        pb = {k: float(b.get(k, 0.0)) for k in keys}
        t0 = float(t0)
        t1 = float(t1)
        span = max(1e-9, t1 - t0)

        def sample(t):
            x = min(1.0, max(0.0, (t - t0) / span))
            k = self.ease(ease, x)
            return {kk: pa[kk] + (pb[kk] - pa[kk]) * k for kk in keys}

        def world(t):
            p = sample(t)
            return self.pose(p["theta"], p["radius"], p["height"], p["aim_height"],
                             p["dutch"])[0]

        t = t0
        while True:
            self.key(t, **sample(t))
            if t >= t1 - 1e-9:
                break
            eps = 1e-3
            v = (world(min(t1, t + eps)) - world(t)).length / eps
            dt = step_max
            if v > 1e-6:
                dt = min(dt, step_arc / v)
            v2 = (world(min(t1, t + 0.05)) - world(t)).length / 0.05
            acc = abs(v2 - v) / 0.05
            if acc > 1e-6:
                dt = min(dt, math.sqrt(2.0 * step_tol / acc))
            if dense_ends and (t < t0 + 0.15 or t > t1 - 0.15):
                dt = min(dt, 0.05)
            self._speeds.append(v)
            t = min(t1, t + max(1.0 / self.fps, dt))
        return self

    def orbit(self, t0, t1, ease, theta0, theta1, radius=1.5, height=0.0, aim_height=0.0,
              dutch=0.0):
        """环绕(只改 θ)。"""
        return self.sweep(t0, t1, ease,
                          dict(theta=theta0, radius=radius, height=height,
                               aim_height=aim_height, dutch=dutch),
                          dict(theta=theta1, radius=radius, height=height,
                               aim_height=aim_height, dutch=dutch))

    def dolly(self, t0, t1, ease, radius0, radius1, theta=0.0, height=0.0, aim_height=0.0,
              dutch=0.0):
        """推拉(只改半径)。"""
        return self.sweep(t0, t1, ease,
                          dict(theta=theta, radius=radius0, height=height,
                               aim_height=aim_height, dutch=dutch),
                          dict(theta=theta, radius=radius1, height=height,
                               aim_height=aim_height, dutch=dutch))

    def crane(self, t0, t1, ease, height0, height1, theta=0.0, radius=1.5, aim_height=0.0,
              dutch=0.0):
        """升降(只改高度)。"""
        return self.sweep(t0, t1, ease,
                          dict(theta=theta, radius=radius, height=height0,
                               aim_height=aim_height, dutch=dutch),
                          dict(theta=theta, radius=radius, height=height1,
                               aim_height=aim_height, dutch=dutch))

    # ---- 匀速运镜(关键帧尽量少) ----
    # 思路: 匀速段用 **LINEAR 插值** 表达 —— 只要关键帧落在真值上, 中间就是精确匀速,
    # 因此不需要"自适应加密"那一套(那是给缓动段用的)。关键帧数量只由**几何误差**决定:
    #   · 纯推拉 / 纯升降: 路径本身就是直线 → **2 个关键帧就精确**;
    #   · 环绕: 相邻关键帧之间走的是弦, 弦与弧的最大偏差 = r(1-cos(Δθ/2)),
    #     由它反解出允许的角度步长(默认容差 1cm), 于是 180°/1.6m 只需十几个关键帧
    #     (对比: 之前自适应加密是 294 个)。
    def _linear_step_deg(self, radius, tol=0.01, ang_step=30.0):
        if radius <= 1e-6:
            return ang_step
        k = min(1.0, max(1e-9, float(tol) / float(radius)))
        step_tol = math.degrees(2.0 * math.acos(1.0 - k)) if k < 1.0 else 180.0
        return max(1.0, min(float(ang_step), step_tol))

    def _measure_linear(self, pa, pb, n):
        """给定关键帧数 n, 量出这一段"用 LINEAR 插值近似"的三个误差:
          · 弦偏差(米): 位置偏离真实轨迹多少;
          · 折角(度): 相邻线段的方向变化 —— 圆弧被折线近似时, 每个关键帧处会"拐一下",
                      这就是实际观感里的"一顿一顿";
          · 瞄准偏差(度): 关键帧之间是线性插值欧拉角, 而真正的"看着目标"是 atan 曲线 ——
                      两者之差就是目标在画面里上下漂移的角度。
        返回 (弦偏差, 折角, 瞄准偏差, 转角速率台阶比)。
        转角速率台阶比 = 各段"每帧转动角速度"的 max/min —— 用 LINEAR 插值时角速度是**分段常数**,
        所以每个关键帧处会有一次台阶; 这个比值就是台阶的严重程度(1.0 = 完全平滑)。
        实测教训: 升降段俯仰角本身是 atan 曲线(中间快两头慢), 关键帧太少时台阶比能到 2.2 倍,
        肉眼就是"转到某个位置会顿一下" —— 所以它必须和另外三个判据一起约束。"""
        keys = ("theta", "radius", "height", "aim_height", "dutch")
        pa = {k: float(pa.get(k, 0.0) or 0.0) for k in keys}
        pb = {k: float(pb.get(k, 0.0) or 0.0) for k in keys}
        pts, aims, dirs = [], [], []
        for i in range(n + 1):
            k = i / float(n)
            p = {kk: pa[kk] + (pb[kk] - pa[kk]) * k for kk in keys}
            pos, rot, _ = self.pose(p["theta"], p["radius"], p["height"], p["aim_height"],
                                    p["dutch"])
            pts.append(pos)
            # 真实视线方向(相机看向锚点方向)与"关键帧处记录的欧拉角"
            aims.append((self.anchor() + Vector((0.0, 0.0, p["aim_height"])) - pos).normalized())
            dirs.append(rot.to_euler('XYZ'))
        # 折角: 相邻弦方向的最大夹角
        max_kink = 0.0
        for i in range(1, n):
            v0 = pts[i] - pts[i - 1]
            v1 = pts[i + 1] - pts[i]
            if v0.length > 1e-9 and v1.length > 1e-9:
                max_kink = max(max_kink, math.degrees(v0.angle(v1)))
        # 弦偏差 + 瞄准偏差: 在每段中间取若干子样本
        max_chord = 0.0
        max_aim = 0.0
        for i in range(n):
            for s in (0.25, 0.5, 0.75):
                k = (i + s) / float(n)
                p = {kk: pa[kk] + (pb[kk] - pa[kk]) * k for kk in keys}
                true_pos, true_rot, _ = self.pose(p["theta"], p["radius"], p["height"],
                                                  p["aim_height"], p["dutch"])
                chord = pts[i] + (pts[i + 1] - pts[i]) * s
                max_chord = max(max_chord, (true_pos - chord).length)
                # 插值欧拉角(unwrap 到与前一关键帧连续)下的视线方向
                e0, e1 = dirs[i], dirs[i + 1]
                cur = [e1[j] for j in range(3)]
                for j in range(3):
                    while cur[j] - e0[j] > math.pi:
                        cur[j] -= 2 * math.pi
                    while cur[j] - e0[j] < -math.pi:
                        cur[j] += 2 * math.pi
                ei = [e0[j] + (cur[j] - e0[j]) * s for j in range(3)]
                from mathutils import Euler
                vi = Euler(ei, 'XYZ').to_matrix() @ Vector((0.0, 0.0, -1.0))
                true_aim = (self.anchor() + Vector((0.0, 0.0, p["aim_height"])) - true_pos)
                if true_aim.length > 1e-9 and vi.length > 1e-9:
                    max_aim = max(max_aim,
                                  math.degrees(vi.normalized().angle(true_aim.normalized())))
        # 转角速率: rates[i] = 第 i 段的每帧转动速度(LINEAR 插值下每段是常数)。
        # 要区分两件事, 别把几何必然当成缺陷:
        #   · 全局 max/min = **几何必然** —— 升降时相机要一直咬住中心, 俯仰角是 atan 曲线,
        #     中段本来就该转得快些;
        #   · **相邻两段的比值**(台阶)才是肉眼看到的抖动 —— 它是"分段常数"造成的, 关键帧越多越小。
        # 判据只约束台阶, 全局比值只做信息展示。
        from mathutils import Euler
        rates = []
        for i in range(1, n + 1):
            e0, e1 = dirs[i - 1], dirs[i]
            cur = [e1[j] for j in range(3)]
            for j in range(3):
                while cur[j] - e0[j] > math.pi:
                    cur[j] -= 2 * math.pi
                while cur[j] - e0[j] < -math.pi:
                    cur[j] += 2 * math.pi
            dq = Euler(e0, 'XYZ').to_quaternion().rotation_difference(
                Euler(cur, 'XYZ').to_quaternion())
            rates.append(math.degrees(dq.angle))
        pos_rates = [r for r in rates if r > 1e-9]
        rot_ratio = (max(pos_rates) / min(pos_rates)) if len(pos_rates) > 1 else 1.0
        rot_step = 1.0
        for i in range(len(rates) - 1):
            if rates[i] > 1e-9 and rates[i + 1] > 1e-9:
                rot_step = max(rot_step, rates[i + 1] / rates[i], rates[i] / rates[i + 1])
        return max_chord, max_kink, max_aim, rot_step, rot_ratio

    def _linear_key_count(self, pa, pb, tol=0.01, kink_deg=3.0, aim_deg=0.5, rot_jump=1.25,
                          n_max=240):
        """最小的关键帧数, 使四个误差同时达标(数值搜索, 精确而非估算)。
        四个判据: 弦偏差 / 折角 / 瞄准偏差 / **相邻段角速度台阶比**。"""
        keys = ("theta", "radius", "height", "aim_height", "dutch")
        a = {k: float(pa.get(k, 0.0) or 0.0) for k in keys}
        b = {k: float(pb.get(k, 0.0) or 0.0) for k in keys}
        for n in range(1, n_max + 1):     # n=1 -> 两个关键帧(直线运动的最小表示)
            chord, kink, aim, step, _ratio = self._measure_linear(a, b, n)
            if chord <= tol and kink <= kink_deg and aim <= aim_deg and step <= rot_jump:
                return n
        return n_max

    def _sweep_linear(self, x0, x1, a, b, tol=0.01, kink_deg=3.0, aim_deg=0.5, rot_jump=1.25):
        """归一化时间 [x0,x1] 上把机位从 a 匀速变到 b(a/b 是 dict)。
        关键帧数由"弦偏差 / 折角 / 瞄准偏差"三个判据数值求解 —— 匀速靠 LINEAR 插值保证,
        所以关键帧只需满足精度, 不需要加密(早期自适应贝塞尔加密要近 300 个)。"""
        keys = ("theta", "radius", "height", "aim_height", "dutch")
        pa = {k: float(a.get(k, 0.0)) for k in keys}
        pb = {k: float(b.get(k, 0.0)) for k in keys}
        t0, t1 = self.t(x0), self.t(x1)
        n = 1
        if any(abs(pa[k] - pb[k]) > 1e-9 for k in keys):
            n = self._linear_key_count(pa, pb, tol, kink_deg, aim_deg, rot_jump)
        for i in range(n + 1):
            k = i / float(n)
            self.key(t0 + (t1 - t0) * k,
                     **{kk: pa[kk] + (pb[kk] - pa[kk]) * k for kk in keys})
        self._linear_ranges.append((t0, t1))
        self.note_keys = getattr(self, "note_keys", 0) + (n + 1)
        return self

    def orbit_linear(self, x0, x1, theta0, theta1, radius=1.8, height=0.0, aim_height=0.0,
                     tol=0.01, kink_deg=3.0, aim_deg=0.5, rot_jump=1.25):
        """匀速环绕(只改方位角, 水平机位, 视线咬住锚点中心)。
        关键帧数按"折角 ≤ kink_deg"等三个判据自动求解(容差越严越顺、关键帧越多)。"""
        return self._sweep_linear(x0, x1,
                                  dict(theta=theta0, radius=radius, height=height,
                                       aim_height=aim_height),
                                  dict(theta=theta1, radius=radius, height=height,
                                       aim_height=aim_height),
                                  tol=tol, kink_deg=kink_deg, aim_deg=aim_deg,
                                  rot_jump=rot_jump)

    def dolly_linear(self, x0, x1, radius0, radius1, theta=30.0, height=0.0,
                     aim_height=0.0, tol=0.01, kink_deg=3.0, aim_deg=0.5, rot_jump=1.25):
        """匀速推拉(只改半径)。路径是直线 -> 折角/弦偏差恒为 0; 关键帧数只看瞄准偏差。"""
        return self._sweep_linear(x0, x1,
                                  dict(theta=theta, radius=radius0, height=height,
                                       aim_height=aim_height),
                                  dict(theta=theta, radius=radius1, height=height,
                                       aim_height=aim_height),
                                  tol=tol, kink_deg=kink_deg, aim_deg=aim_deg,
                                  rot_jump=rot_jump)

    def crane_linear(self, x0, x1, height0, height1, theta=10.0, radius=2.0,
                     aim_height=0.0, tol=0.01, kink_deg=3.0, aim_deg=0.5, rot_jump=1.25):
        """匀速升降(只改高度)。路径是直线 -> 关键帧数只看瞄准偏差(高度跨度大时会自动加密)。"""
        return self._sweep_linear(x0, x1,
                                  dict(theta=theta, radius=radius, height=height0,
                                       aim_height=aim_height),
                                  dict(theta=theta, radius=radius, height=height1,
                                       aim_height=aim_height),
                                  tol=tol, kink_deg=kink_deg, aim_deg=aim_deg,
                                  rot_jump=rot_jump)

    def mark(self, name, x):
        """在归一化时间 x 处放一个时间轴标记。"""
        try:
            self.scene.timeline_markers.new(name, frame=int(round(self.frame(x))))
        except Exception:  # noqa: BLE001
            pass

    def finish(self, seconds=None, set_range=True):
        """收尾: 设定关键帧插值(匀速段 LINEAR / 缓动段 BEZIER+AUTO_CLAMPED)、帧范围、汇总。
        幂等 —— 预设自己调过之后, 宿主收尾那次直接返回(否则状态行会重复一遍汇总)。"""
        if self.action is None:
            return 0
        if getattr(self, "_finished", False):
            return self.end_frame
        self._finished = True
        lin = [(self.frame_at(a), self.frame_at(b)) for a, b in self._linear_ranges]
        for fc in action_fcurves(self.action):
            for kp in fc.keyframe_points:
                f = kp.co[0]
                in_linear = any(a - 0.51 <= f <= b + 0.51 for a, b in lin)
                if in_linear:
                    kp.interpolation = 'LINEAR'
                else:
                    kp.interpolation = 'BEZIER'
                    kp.handle_left_type = 'AUTO_CLAMPED'
                    kp.handle_right_type = 'AUTO_CLAMPED'
            fc.update()
        if seconds is None:
            seconds = self.duration
        self.end_frame = int(round(self.frame_at(seconds)))
        if set_range:
            self.scene.frame_start = 1
            self.scene.frame_end = self.end_frame
            self.scene.frame_set(1)
        peak = max(self._speeds) if self._speeds else 0.0
        mode = "匀速(少关键帧)" if self._linear_ranges and not self._eased_ranges else "缓动"
        self.log("%s | %d 个关键帧 | 帧范围 1-%d(%d 帧 / %.2f 秒 @%g fps)%s"
                 % (mode, self.key_count, self.end_frame, self.end_frame, seconds, self.fps,
                    (" | 峰值 %.2f m/s" % peak) if peak else ""))
        return self.end_frame


def _preset_items(self, context):
    """下拉栏的条目(动态枚举): 每次面板重绘都会被调用, 所以预设目录是**自动读取**的。
    标识符用索引字符串(稳定好解析), 名字取 EFPRESET 头里的 name。"""
    scene = context.scene if context is not None else bpy.context.scene
    out = []
    for i, pr in enumerate(_scan_presets(scene)):
        out.append((str(i), pr.get('name') or pr.get('file', '?'),
                    (pr.get('desc', '') or '')[:120], 'NONE', i))
    if not out:
        out.append(('%NONE%', '(目录里没有预设)', '把 .py 预设放进预设文件夹, 面板会自动列出', 'ERROR', 0))
    return out


def _preset_current_index(scene, presets):
    """当前下拉栏选中项的下标(**只读**, 会夹到合法范围)。
    为什么只读: 这个函数在面板绘制路径里被调用, 而 Blender **禁止在 draw 里写 ID 属性** ——
    写了会抛错并跳过该面板的剩余绘制(实测表现: 切换预设后, 排在后面的"生成这个运镜"
    按钮整个消失)。所以这里只算不写, 同步交给枚举的 update 回调(_preset_on_pick)。"""
    if not presets:
        return 0
    try:
        i = int(str(scene.ef_bridge_preset))
    except (TypeError, ValueError):
        i = int(getattr(scene, "ef_bridge_preset_index", 0) or 0)
    return max(0, min(i, len(presets) - 1))


def _apply_preset_defaults(scene, index, force=False):
    """把预设元数据里声明的默认值写进面板参数(运镜时间 + 最多 4 个数值参数)。
    为什么需要: 这些默认值本来只写在预设文件头的 JSON 里, 面板属性却是 0 ——
    首次使用会拿着"半径 0"这种退化参数去生成(实测踩到)。现在:
      · 切换预设时(force)总是套用该预设的默认值, 用户随后可自由改;
      · 生成前(force=False)只在"运镜时间还没设过(≤0)"时套用一次, 免得覆盖用户输入。"""
    presets = _scan_presets(scene)
    if not presets or index < 0 or index >= len(presets):
        return False
    meta = presets[index]
    if not force and float(getattr(scene, "ef_bridge_duration", 0.0) or 0.0) > 0.0:
        return False
    scene.ef_bridge_duration = max(0.2, float(meta.get('seconds', 8.0) or 8.0))
    decl = list(meta.get('params') or [])
    for i in range(PRESET_PARAM_MAX):
        v = float(decl[i].get('default', 0.0)) if i < len(decl) else 0.0
        setattr(scene, "ef_bridge_p%d" % i, v)
    return True


def _preset_on_pick(self, context):
    """下拉栏的 update 回调(draw 之外执行, 所以可以安全写属性):
    同步内部索引, 并把该预设声明的默认参数套用到面板(用户随后可以随便改)。"""
    try:
        i = int(str(self.ef_bridge_preset))
    except (TypeError, ValueError):
        return
    self.ef_bridge_preset_index = i
    try:
        _apply_preset_defaults(self, i, force=True)
    except Exception:  # noqa: BLE001
        pass


def _run_preset(scene, index):
    """执行预设: 解析 → 注入 ef → exec(全新命名空间) → 收尾。
    出错不抛给 UI: 状态行给一行摘要, 完整 traceback 追加到日志文件。"""
    presets = _scan_presets(scene)
    if not presets:
        scene.ef_bridge_preset_status = "没有可用预设(目录: %s)" % scene.ef_bridge_preset_dir
        return False
    if index < 0 or index >= len(presets):
        index = 0
    meta = presets[index]
    # 首次(还没设过运镜时间)时套用预设声明的默认参数; 已设过则完全尊重用户输入
    _apply_preset_defaults(scene, index, force=False)
    scene.ef_bridge_preset_index = index
    # 【不要在这里写下拉栏(ef_bridge_preset)】写它会触发枚举的 update 回调, 而 Blender 的
    # 属性回调可能是**延迟**执行的 —— 于是"套用默认参数"会在本函数读走运镜时间之后再回来把它
    # 改回默认值, 表现为"用户输入 4 秒却按 8 秒生成"(实测踩到)。下拉栏的显示同步由
    # 选择算子/面板负责, 执行路径只认 ef_bridge_preset_index。

    if scene.camera is None or scene.camera.type != 'CAMERA':
        scene.ef_bridge_preset_status = "失败: 场景没有活动相机(选中相机后 Ctrl+Numpad0)"
        return False

    params = {}
    for i, p in enumerate(meta.get('params', [])):
        params[p.get('key', 'p%d' % i)] = getattr(scene, "ef_bridge_p%d" % i)
    # v0.3.3: "运镜时间"是通用参数(不在预设自己声明的参数里), 直接来自面板输入框。
    duration = float(scene.ef_bridge_duration or meta.get('seconds', 8.0))
    params['duration'] = duration
    api = _EfPresetAPI(scene, params, meta.get('name', 'Preset'), duration)

    try:
        with open(meta['path'], 'r', encoding='utf-8') as fh:
            src = fh.read()
        try:
            bpy.ops.ed.undo_push(message="运镜预设: %s" % meta.get('name', ''))
        except Exception:  # noqa: BLE001  后台模式没有 undo 栈, 忽略
            pass
        api._setup(meta.get('name', 'Preset'))
        g = {"__name__": "ef_preset", "__file__": meta['path'], "ef": api,
             "bpy": bpy, "math": math, "Matrix": Matrix, "Vector": Vector}
        exec(compile(src, meta['path'], 'exec'), g)     # 命名空间每次新建, 预设之间不串变量
        if callable(g.get("build")):
            g["build"](api, api.params)
        if api.action is not None and api.key_count > 0:
            api.finish(duration)
    except Exception:  # noqa: BLE001
        import traceback
        tb = traceback.format_exc()
        try:
            with open(PRESET_LOG, "a", encoding="utf-8") as lf:
                lf.write("=== %s (%s) ===\n%s\n" % (meta.get('name'), meta['path'], tb))
        except OSError:
            pass
        lines = tb.strip().splitlines()
        scene.ef_bridge_preset_status = "失败: %s (详见 %s)" % (
            (lines[-1] if lines else "未知错误")[:120], PRESET_LOG)
        return False

    detail = " | ".join(api.notes[-2:]) if api.notes else ""
    scene.ef_bridge_preset_status = "已生成 %s — %s" % (
        api.action.name if api.action else meta.get('name'), detail)
    return True


class EFBRIDGE_OT_preset_refresh(bpy.types.Operator):
    bl_idname = "ef_bridge.preset_refresh"
    bl_label = "刷新预设列表"

    def execute(self, context):
        got = _scan_presets(context.scene, force=True)
        context.scene.ef_bridge_preset_status = "已发现 %d 个预设" % len(got)
        return {'FINISHED'}


class EFBRIDGE_OT_preset_open_dir(bpy.types.Operator):
    bl_idname = "ef_bridge.preset_open_dir"
    bl_label = "打开预设文件夹"

    def execute(self, context):
        d, err = _ensure_preset_dir(context.scene)
        if not d:
            self.report({'ERROR'}, err or "无法打开预设目录")
            return {'CANCELLED'}
        try:
            os.startfile(d)          # Windows: 用资源管理器打开
        except Exception:  # noqa: BLE001
            self.report({'INFO'}, d)
        return {'FINISHED'}


class EFBRIDGE_OT_preset_select(bpy.types.Operator):
    bl_idname = "ef_bridge.preset_select"
    bl_label = "选择预设"
    index: bpy.props.IntProperty(default=0)

    def execute(self, context):
        context.scene.ef_bridge_preset_index = int(self.index)
        try:
            context.scene.ef_bridge_preset = str(int(self.index))
        except (TypeError, ValueError):
            pass
        return {'FINISHED'}


class EFBRIDGE_OT_preset_defaults(bpy.types.Operator):
    bl_idname = "ef_bridge.preset_defaults"
    bl_label = "恢复默认参数"
    bl_description = "把当前预设声明的默认参数与运镜时间恢复到面板上"

    def execute(self, context):
        idx = context.scene.ef_bridge_preset_index
        if _apply_preset_defaults(context.scene, idx, force=True):
            context.scene.ef_bridge_preset_status = "已套用该预设的默认参数"
            return {'FINISHED'}
        self.report({'ERROR'}, "没有可用的预设")
        return {'CANCELLED'}


class EFBRIDGE_OT_preset_run(bpy.types.Operator):
    bl_idname = "ef_bridge.preset_run"
    bl_label = "生成这个运镜"
    bl_description = "在当前相机上新建一个独立 action 并写入运镜关键帧(不动你原有的动画)"
    index: bpy.props.IntProperty(default=-1)

    def execute(self, context):
        idx = int(self.index)
        if idx < 0:
            idx = context.scene.ef_bridge_preset_index
        if not _run_preset(context.scene, idx):
            self.report({'ERROR'}, context.scene.ef_bridge_preset_status)
            return {'CANCELLED'}
        return {'FINISHED'}


CLASSES = (EFBRIDGE_OT_start, EFBRIDGE_OT_stop, EFBRIDGE_OT_send_once, EFBRIDGE_OT_fix_dof,
           EFBRIDGE_PT_panel,
           EFBRIDGE_OT_preset_refresh, EFBRIDGE_OT_preset_open_dir,
           EFBRIDGE_OT_preset_select, EFBRIDGE_OT_preset_run,
           EFBRIDGE_OT_preset_defaults)


def register():
    global _timer_registered
    for cls in CLASSES:
        bpy.utils.register_class(cls)
    bpy.types.Scene.ef_bridge_rate = bpy.props.IntProperty(name="发送频率", default=60, min=RATE_MIN, max=RATE_MAX, subtype='FREQUENCY')
    bpy.types.Scene.ef_bridge_scale = bpy.props.FloatProperty(name="位置比例", default=1.0, min=0.0001, max=10000.0)
    bpy.types.Scene.ef_bridge_suppress_controller = bpy.props.BoolProperty(name="暂停游戏原相机控制", default=True)
    bpy.types.Scene.ef_bridge_status = bpy.props.StringProperty(name="状态", default="已停止")
    # v0.3.9: 参考物件不再做成选项 —— 固定用名为 Cube 的物体(见 REFERENCE_OBJECT_NAME)。
    # 旧的 ef_bridge_use_reference / ef_bridge_reference 两个属性已删除; 老工程文件里残留的
    # 值无所谓(属性不存在就等于没设), 行为统一为"绝对锚点, 原点 = Cube"。
    bpy.types.Scene.ef_bridge_lens_mode = bpy.props.EnumProperty(
        name="镜头模式",
        description="定镜头: 武装那一刻取一次角色坐标并冻结, 相机钉在世界上那一点不动。"
                    "随角色: 每帧读实时角色坐标, 相机跟着角色走。"
                    "两者都由模块每帧绝对写相机变换(对游戏自身相机状态与鼠标免疫)"
                    " —— 区别只在游戏侧原点是常量还是实时值",
        items=[('STATIC', "定镜头", "相机定点不动(武装瞬间冻结角色坐标, 此后不用角色坐标干预)"),
               ('FOLLOW', "随角色镜头", "相机跟随角色(每帧读实时角色坐标; 会关闭自由相机以便读取)")],
        default='STATIC')
    bpy.types.Scene.ef_bridge_arm_status = bpy.props.StringProperty(name="武装", default="")
    # v0.3.8: 隐藏游戏 UI(拍摄用)。模块侧只压 UI 相机的渲染遮罩(层 5), 不碰输入。
    bpy.types.Scene.ef_bridge_hide_ui = bpy.props.BoolProperty(
        name="隐藏游戏 UI(拍摄用)",
        description="勾选后由模块把只渲染 UI 层(层 5)的 UICamera 遮罩压成 0, 画面里的 HUD 消失, "
                    "而世界由 MainCamera 渲染(它不含层 5)所以完全不受影响; "
                    "键位与攻击照常可用(与原生相机按 X 隐藏 UI 不同, 那条会让很多键位失效)。"
                    "v0.3.11 起连**世界空间的 UI**也一起藏: 模块隐藏时会同时清掉主相机遮罩里的"
                    "层 16(WorldUI) —— 怪物血条/状态条、角色体力条都会消失(2026-09-15 实测, 世界画面无损)。"
                    "你按 ESC 呼出的界面仍可见(默认策略=尊重游戏, 关掉后自动重新隐藏); "
                    "想全藏连菜单也不显示: 模块指令 -Id 66 -A0 1",
        default=False)
    bpy.types.Scene.ef_bridge_ext_status = bpy.props.StringProperty(name="外部设置", default="")
    # ---- 运镜预设 ----
    bpy.types.Scene.ef_bridge_preset_dir = bpy.props.StringProperty(
        name="预设文件夹", default="", subtype='DIR_PATH',
        description="需要读取的运镜脚本目录(.py); 留空则用默认目录, 首次会自动放入示例预设")
    bpy.types.Scene.ef_bridge_preset_index = bpy.props.IntProperty(name="预设索引", default=0)
    # v0.3.3: 通用参数"运镜时间(秒)" —— 预设按归一化时间书写, 关键帧(位置+时间)随它等比缩放。
    # 切换预设时由 _preset_on_pick 重置为该预设元数据里的默认秒数。
    bpy.types.Scene.ef_bridge_duration = bpy.props.FloatProperty(
        name="运镜时间", default=8.0, min=0.2, max=600.0, precision=2, subtype='TIME',
        description="整段运镜的时长(秒); 关键帧与机位位移都按它等比缩放(帧数 = 秒 × 场景帧率)")
    # v0.3.1: 下拉栏本体 —— 动态枚举, items 每帧面板重绘时重算(于是自动跟随文件夹内容)。
    bpy.types.Scene.ef_bridge_preset = bpy.props.EnumProperty(
        name="预设", items=_preset_items, update=_preset_on_pick,
        description="预设文件夹里的运镜脚本; 点开可直接切换")
    bpy.types.Scene.ef_bridge_preset_status = bpy.props.StringProperty(name="预设状态", default="")
    for _i in range(PRESET_PARAM_MAX):
        setattr(bpy.types.Scene, "ef_bridge_p%d" % _i,
                bpy.props.FloatProperty(name="参数 %d" % (_i + 1), default=0.0, precision=3))
    # v0.2.4: 常驻轮询(武装令牌 + 外部设置令牌) —— 不依赖"是否已开始发送"。
    # 用 register 之后的第一个 timer 回调注册, 避免在 register() 内直接碰 context。
    _timer_registered = False
    # v0.3.9: 启动自检图标名(见 _check_icon_names 的注释 —— icon='UNDO' 那次的教训)
    _check_icon_names()
    try:
        bpy.app.timers.register(_register_timer, first_interval=0.5, persistent=True)
    except Exception:  # noqa: BLE001
        pass


def unregister():
    global _running, _mapping
    _running = False
    try:
        if bpy.app.timers.is_registered(_timer):
            bpy.app.timers.unregister(_timer)
        if bpy.app.timers.is_registered(_register_timer):
            bpy.app.timers.unregister(_register_timer)
    except Exception:  # noqa: BLE001
        pass
    if _mapping is not None:
        _mapping.close()
        _mapping = None
    for prop in ("ef_bridge_rate", "ef_bridge_scale", "ef_bridge_suppress_controller",
                 "ef_bridge_status",
                 "ef_bridge_lens_mode", "ef_bridge_arm_status", "ef_bridge_ext_status",
                 "ef_bridge_hide_ui",
                 "ef_bridge_preset_dir", "ef_bridge_preset_index", "ef_bridge_preset_status",
                 "ef_bridge_preset", "ef_bridge_duration"):
        if hasattr(bpy.types.Scene, prop):
            delattr(bpy.types.Scene, prop)
    for _i in range(PRESET_PARAM_MAX):
        _n = "ef_bridge_p%d" % _i
        if hasattr(bpy.types.Scene, _n):
            delattr(bpy.types.Scene, _n)
    for cls in reversed(CLASSES):
        bpy.utils.unregister_class(cls)


if __name__ == "__main__":
    register()
