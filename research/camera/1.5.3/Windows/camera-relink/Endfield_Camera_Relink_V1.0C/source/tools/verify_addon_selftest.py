# -*- coding: utf-8 -*-
# 后台 Blender 自检: 加载包内插件源码, 验证新版基线锚点/水平归零/武装令牌是否正常。
# 注意: 本脚本不写用户偏好(不调用 save_userpref), 不会影响已安装的插件。
import importlib.util
import math
import os
import struct
import sys

import bpy
from mathutils import Vector

ADDON = r"D:\EndfieldCameraLink\blender_addon\endfield_camera_bridge\__init__.py"

# Blender 启动时可能已加载 addons 目录里"同一个插件", 先禁用它, 否则类名双重注册
# (仅在本次会话内注销, 不写用户偏好)
try:
    import addon_utils
    addon_utils.disable("endfield_camera_bridge", default_set=False)
except Exception as exc:  # noqa: BLE001
    print("跳过禁用已安装插件:", exc)

spec = importlib.util.spec_from_file_location("ef_addon_selftest", ADDON)
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

fails = []


def check(name, cond, detail=""):
    print("%-46s %s  %s" % (name, "OK " if cond else "FAIL", detail))
    if not cond:
        fails.append(name)


# ---- 场景: 一个立方体 + 一台任意姿态的相机 ----
scene = bpy.context.scene
for obj in list(bpy.data.objects):
    bpy.data.objects.remove(obj, do_unlink=True)
cube = bpy.data.objects.new("Cube", bpy.data.meshes.new("CubeMesh"))
scene.collection.objects.link(cube)
cam_data = bpy.data.cameras.new("CamData")
cam = bpy.data.objects.new("Cam", cam_data)
scene.collection.objects.link(cam)
scene.camera = cam
cam.location = (2.0, -3.0, 1.7)
cam.rotation_mode = 'XYZ'
cam.rotation_euler = (1.2, 0.3, 0.4)          # 明显俯仰 + 滚转
bpy.context.view_layer.update()

mod.register()
check("注册后属性齐全",
      all(hasattr(scene, n) for n in ("ef_bridge_lens_mode", "ef_bridge_arm_status",
                                      "ef_bridge_hide_ui", "ef_bridge_preset_dir")))
check("版本号已升到 0.3.13", mod.bl_info["version"] == (0, 3, 13), str(mod.bl_info["version"]))
# v0.3.9: 面板里的 icon='...' 字面量必须都是本版本 Blender 认得的名字
# (就是 icon='UNDO' 那个 bug: 非法图标名会让 label() 抛异常、整个预设区只剩一行报错)
_icons = set(i.identifier for i in bpy.types.UILayout.bl_rna.functions["label"]
             .parameters["icon"].enum_items)
_bad_icons = [(src_line, name) for src_line, name in mod._collect_icon_literals()
              if name not in _icons]
check("所有 icon='...' 都是合法图标名", not _bad_icons, str(_bad_icons))

# ---- 1) 水平归零 ----
before = cam.matrix_world.to_quaternion()
before_look = before @ Vector((0.0, 0.0, -1.0))
err = mod.level_camera(cam)
q = cam.matrix_world.to_quaternion()
look = q @ Vector((0.0, 0.0, -1.0))
up = q @ Vector((0.0, 1.0, 0.0))
check("level_camera 成功", err is None, str(err))
check("水平: 视线 z 分量为 0", abs(look.z) < 1e-5, "look=(%.4f, %.4f, %.4f)" % (look.x, look.y, look.z))
check("水平: 上方向指向天顶", up.angle(Vector((0.0, 0.0, 1.0))) < 1e-4,
      "%.6f deg" % math.degrees(up.angle(Vector((0.0, 0.0, 1.0)))))
yaw_before = math.degrees(math.atan2(before_look.y, before_look.x))
yaw_after = math.degrees(math.atan2(look.y, look.x))
check("水平: 保留水平朝向角", abs(yaw_before - yaw_after) < 1e-3,
      "yaw %.3f -> %.3f" % (yaw_before, yaw_after))
check("水平: 位置未改动", (cam.matrix_world.translation - Vector((2.0, -3.0, 1.7))).length < 1e-6)

# ---- 2) 报文: 参考物件位置必须落在偏移 100/104/108 ----
cube.location = (1.0, 2.0, 3.0)
bpy.context.view_layer.update()
pkt = mod._camera_packet(scene, 2)
vals = struct.unpack("<4sIQdIi3f3f3f8f5fQ", pkt)
check("报文长度 128", len(pkt) == 128, str(len(pkt)))
check("参考物件 flag 已置位", (vals[4] & mod.FLAG_REFERENCE) != 0, hex(vals[4]))
ref_bytes = (struct.unpack_from("<f", pkt, 100)[0],
             struct.unpack_from("<f", pkt, 104)[0],
             struct.unpack_from("<f", pkt, 108)[0])
check("参考物件位置在 100/104/108", ref_bytes == (1.0, 3.0, 2.0), str(ref_bytes))
# struct 字段序: 0 magic,1 ver,2 seq,3 ts,4 flags,5 frame,6-8 pos,9-11 fwd,12-14 up,
#                15-22 八个镜头浮点, 23-27 五个备用浮点(参考物件在前三个), 28 seqEnd
check("通用解析第 24-26 项一致", vals[23:26] == (1.0, 3.0, 2.0), str(vals[23:26]))
check("相机位置仍正确", abs(vals[6] - 2.0) < 1e-6 and abs(vals[7] - 1.7) < 1e-6 and abs(vals[8] + 3.0) < 1e-6,
      str(vals[6:9]))
check("尾部序号一致(防撕裂)", vals[-1] == 2)

# ---- 2b) v0.3.8: 隐藏游戏 UI 的 flag 位(bit 6) ----
check("FLAG_HIDE_UI 是位 6", mod.FLAG_HIDE_UI == (1 << 6), hex(mod.FLAG_HIDE_UI))
check("默认不请求隐藏 UI", (vals[4] & mod.FLAG_HIDE_UI) == 0, hex(vals[4]))
scene.ef_bridge_hide_ui = True
pkt_h = mod._camera_packet(scene, 6)
vh = struct.unpack("<4sIQdIi3f3f3f8f5fQ", pkt_h)
check("勾选后 bit6 置位", (vh[4] & mod.FLAG_HIDE_UI) != 0, hex(vh[4]))
check("勾选隐藏 UI 不影响其它位",
      (vh[4] & ~mod.FLAG_HIDE_UI) == (vals[4] & ~mod.FLAG_HIDE_UI),
      "%s vs %s" % (hex(vh[4]), hex(vals[4])))
scene.ef_bridge_hide_ui = False
pkt_h2 = mod._camera_packet(scene, 8)
vh2 = struct.unpack("<4sIQdIi3f3f3f8f5fQ", pkt_h2)
check("取消勾选后 bit6 清零", (vh2[4] & mod.FLAG_HIDE_UI) == 0, hex(vh2[4]))

# ---- 3) v0.3.9: 参考物件已固定为名为 Cube 的物体(开关与选物框都删了) ----
check("参考物件开关/选物框属性已移除",
      not hasattr(scene, "ef_bridge_use_reference") and
      not hasattr(scene, "ef_bridge_reference"))
check("固定物体名 = Cube", getattr(mod, "REFERENCE_OBJECT_NAME", None) == "Cube",
      str(getattr(mod, "REFERENCE_OBJECT_NAME", None)))
# Cube 存在 -> bit4 置位; 把 Cube 改名 -> bit4 清零(退回增量锚点)
cube.name = "NotTheCube"
pkt2 = mod._camera_packet(scene, 4)
v2 = struct.unpack("<4sIQdIi3f3f3f8f5fQ", pkt2)
check("没有 Cube 时参考物件 flag 清零", (v2[4] & mod.FLAG_REFERENCE) == 0, hex(v2[4]))
cube.name = "Cube"
pkt3 = mod._camera_packet(scene, 5)
v3 = struct.unpack("<4sIQdIi3f3f3f8f5fQ", pkt3)
check("Cube 回来时 flag 恢复置位", (v3[4] & mod.FLAG_REFERENCE) != 0, hex(v3[4]))

# ---- 4) 武装令牌 -> 自动水平 + 位置归零 ----
cam.rotation_euler = (0.9, 0.0, -0.5)
cam.location = (7.0, -2.0, 3.0)
bpy.context.view_layer.update()
check("位置归零属性已注册(0.2.2: 归零/水平已改为固定行为, 不再做开关)",
      not hasattr(scene, "ef_bridge_zero_on_arm") and not hasattr(scene, "ef_bridge_level_on_arm"))

with open(mod.ARM_TOKEN_FILE, "w", encoding="utf-8") as fh:
    fh.write("SELFTEST-TOKEN")
mod._arm_token = None
mod._poll_arm_request(scene)
look2 = cam.matrix_world.to_quaternion() @ Vector((0.0, 0.0, -1.0))
loc = cam.matrix_world.translation
check("武装令牌触发自动水平", abs(look2.z) < 1e-5 and "水平归零" in scene.ef_bridge_arm_status,
      scene.ef_bridge_arm_status)
check("武装令牌触发位置归零到 (0,0,0)", loc.length < 1e-6 and "位置归零" in scene.ef_bridge_arm_status,
      "pos=(%.4f, %.4f, %.4f)" % (loc.x, loc.y, loc.z))
check("位置归零保留朝向(见下一步 zero_camera_location 专项)", True, "")

# ---- 4b) 归零函数本身：只动位置、保留朝向 ----
cam.rotation_euler = (0.7, 0.1, -0.4)      # 任意姿态(不做水平化)
cam.location = (3.0, -1.5, 0.8)
bpy.context.view_layer.update()
q_before = cam.matrix_world.to_quaternion()
err_zero = mod.zero_camera_location(cam)
loc_after = cam.matrix_world.translation
q_after = cam.matrix_world.to_quaternion()
check("zero_camera_location 只改位置、朝向不变",
      err_zero is None and loc_after.length < 1e-6
      and math.degrees(q_before.rotation_difference(q_after).angle) < 1e-4,
      "err=%s pos=%.4f 姿态差=%.2e°" % (err_zero, loc_after.length,
                                      math.degrees(q_before.rotation_difference(q_after).angle)))
mod._poll_arm_request(scene)
check("同一令牌不重复触发(状态未变)", "SELFTEST-TOKEN" in scene.ef_bridge_arm_status)
check("令牌文件路径在临时目录", os.path.dirname(mod.ARM_TOKEN_FILE) == __import__("tempfile").gettempdir(),
      mod.ARM_TOKEN_FILE)

# ---- 5) 令牌 nozero 后缀 -> 只水平不归零 ----
cam.location = (4.0, 1.0, 2.0)
bpy.context.view_layer.update()
with open(mod.ARM_TOKEN_FILE, "w", encoding="utf-8") as fh:
    fh.write("SELFTEST-TOKEN2 nozero")
mod._poll_arm_request(scene)
check("nozero 后缀跳过位置归零",
      (cam.matrix_world.translation - Vector((4.0, 1.0, 2.0))).length < 1e-6
      and "位置归零(跳过)" in scene.ef_bridge_arm_status,
      "pos=(%.2f, %.2f, %.2f) | %s" % (cam.location.x, cam.location.y, cam.location.z,
                                       scene.ef_bridge_arm_status))

# ---- 6) 0.2.2 新增: 镜头模式(定镜头 / 随角色) ----
# 面板选择要同时进两处: 包里的 FLAG_FOLLOW 位(模块据此实时切 pose_mode) + 模式文件
# (武装脚本据此决定要不要开自由相机)。
check("镜头模式属性已注册且默认定镜头",
      hasattr(scene, "ef_bridge_lens_mode") and scene.ef_bridge_lens_mode == 'STATIC',
      getattr(scene, "ef_bridge_lens_mode", "<未注册>"))
mod._last_mode_written = None
scene.ef_bridge_lens_mode = 'STATIC'
p_static = mod._camera_packet(scene, 42)
flags_static = struct.unpack_from("<I", p_static, 24)[0]
check("定镜头: 包内不带 FLAG_FOLLOW", (flags_static & mod.FLAG_FOLLOW) == 0, hex(flags_static))
check("定镜头: 模式文件写 static",
      open(mod.MODE_FILE, encoding="utf-8").read().strip() == "static",
      open(mod.MODE_FILE, encoding="utf-8").read().strip())

mod._last_mode_written = None
scene.ef_bridge_lens_mode = 'FOLLOW'
p_follow = mod._camera_packet(scene, 44)
flags_follow = struct.unpack_from("<I", p_follow, 24)[0]
check("随角色: 包内带 FLAG_FOLLOW", (flags_follow & mod.FLAG_FOLLOW) != 0, hex(flags_follow))
check("随角色: 其余 flag 不受影响(仍启用/位姿/镜头/参考物件)",
      (flags_follow & (mod.FLAG_ENABLED | mod.FLAG_TRANSFORM | mod.FLAG_LENS
                       | mod.FLAG_REFERENCE)) == (mod.FLAG_ENABLED | mod.FLAG_TRANSFORM
                                                  | mod.FLAG_LENS | mod.FLAG_REFERENCE),
      hex(flags_follow))
check("随角色: 模式文件写 follow",
      open(mod.MODE_FILE, encoding="utf-8").read().strip() == "follow",
      open(mod.MODE_FILE, encoding="utf-8").read().strip())
check("随角色: 包长仍为 128 字节(协议未变)", len(p_follow) == mod.PACKET_SIZE, len(p_follow))
scene.ef_bridge_lens_mode = 'STATIC'

# ---- 7) 0.2.4 新增: 外部设置令牌(帧率 / 发送频率 / 自动开始发送) ----
check("外部设置状态属性已注册", hasattr(scene, "ef_bridge_ext_status"))
scene.render.fps = 24
scene.render.fps_base = 1.0
scene.ef_bridge_rate = 60
mod._running = False
mod._last_settings_token = None
with open(mod.SETTINGS_FILE, "w", encoding="utf-8") as fh:
    fh.write("2026-09-11 23:59:00 fps=120 rate=120 autostart=1")
mod._apply_settings(scene)
check("外部设置: 输出帧率改为 120", scene.render.fps == 120 and scene.render.fps_base == 1.0,
      "fps=%s base=%s" % (scene.render.fps, scene.render.fps_base))
check("外部设置: 发送频率改为 120", scene.ef_bridge_rate == 120, scene.ef_bridge_rate)
check("外部设置: autostart 触发了开始发送", bool(mod._running), mod._running)
check("外部设置: 应用结果记录在属性里(v0.3.9 起面板不再显示那行固定文本)",
      "外部设置" in scene.ef_bridge_ext_status, scene.ef_bridge_ext_status)
# 幂等: 同一个令牌不重复应用(把 fps 改回 24 后喂同一令牌, 不应被改回 120)
scene.render.fps = 24
mod._apply_settings(scene)
check("外部设置: 同一令牌不重复应用(幂等)", scene.render.fps == 24, scene.render.fps)
# 上限夹紧: rate 面板属性上限 v0.3.7 起是 1000
mod._last_settings_token = None
with open(mod.SETTINGS_FILE, "w", encoding="utf-8") as fh:
    fh.write("2026-09-11 23:59:01 fps=120 rate=999")
mod._apply_settings(scene)
check("外部设置: 上限内的频率原样接受(999)", scene.ef_bridge_rate == 999, scene.ef_bridge_rate)
mod._last_settings_token = None
with open(mod.SETTINGS_FILE, "w", encoding="utf-8") as fh:
    fh.write("2026-09-11 23:59:02 fps=120 rate=5000")
mod._apply_settings(scene)
check("外部设置: 超上限按 1000 夹紧", scene.ef_bridge_rate == mod.RATE_MAX == 1000,
      scene.ef_bridge_rate)
check("属性硬上限 = 1000", scene.bl_rna.properties['ef_bridge_rate'].hard_max == 1000,
      scene.bl_rna.properties['ef_bridge_rate'].hard_max)
# 3.6 的旧上限 240 必须已经不再是天花板(否则"填 360 生效不了")
scene.ef_bridge_rate = 360
check("属性允许 360(旧上限 240 已放开)", scene.ef_bridge_rate == 360, scene.ef_bridge_rate)
scene.ef_bridge_rate = 60
# 常驻轮询计时器: 不依赖"是否已开始发送"(否则"自动开始发送"会先有鸡先有蛋)
mod._running = False
mod._register_timer()
check("常驻轮询计时器可注册", bpy.app.timers.is_registered(mod._timer))

# ---- 8) 0.3.0 新增: 运镜预设(扫描 / 元数据 / 执行 / 隔离 / 错误处理) ----
preset_dir = os.path.join(__import__("tempfile").gettempdir(), "ef_preset_selftest")
os.makedirs(preset_dir, exist_ok=True)
for _f in os.listdir(preset_dir):
    try:
        os.remove(os.path.join(preset_dir, _f))
    except OSError:
        pass

# 8a) 正常预设: 元数据跨行 + 参数 + 环绕
with open(os.path.join(preset_dir, "01_ok.py"), "w", encoding="utf-8") as fh:
    fh.write('# EFPRESET: {"name": "测试环绕", "desc": "自检用",\n'
             '#            "seconds": 2.0,\n'
             '#            "params": [{"key": "radius", "label": "半径", "default": 1.5}]}\n'
             'def build(ef, params):\n'
             '    ef.mark("起点", 0.0)\n'
             '    ef.orbit(0.0, 2.0, "ease_in_out", 0.0, 90.0, radius=ef.param("radius", 1.5),\n'
             '             height=0.2, aim_height=0.1)\n'
             '    ef.finish(2.0)\n')
# 8b) 会抛异常的预设(验证错误处理不打崩 UI)
with open(os.path.join(preset_dir, "02_boom.py"), "w", encoding="utf-8") as fh:
    fh.write('# EFPRESET: {"name": "自检异常", "desc": "故意报错"}\n'
             'raise RuntimeError("self-test on purpose")\n')

scene.ef_bridge_preset_dir = preset_dir
mod._preset_cache = None
got = mod._scan_presets(scene, force=True)


def idx_of(name):
    return [i for i, g in enumerate(got) if g['file'] == name][0]
check("预设扫描: 至少找到 2 个测试预设(同步也会放进随包示例, 所以按文件名判断)",
      {"01_ok.py", "02_boom.py"} <= {g['file'] for g in got}, [g['file'] for g in got])
check("预设元数据: 跨行 JSON 解析出名字与参数",
      got[0].get('name') == "测试环绕" and len(got[0].get('params', [])) == 1,
      "%s / %s" % (got[0].get('name'), len(got[0].get('params', []))))

# 用户已有动画必须不被碰
bpy.data.actions.new("USER_Original")
if cam.animation_data is None:
    cam.animation_data_create()
cam.animation_data.action = bpy.data.actions["USER_Original"]
n_before = len(bpy.data.actions)

IDX_OK = idx_of("01_ok.py")
IDX_BOOM = idx_of("02_boom.py")
mod._apply_preset_defaults(scene, IDX_OK, force=True)
scene.ef_bridge_p0 = 1.2
ok_preset = mod._run_preset(scene, IDX_OK)
new_action = cam.animation_data.action
check("预设执行: 成功且状态行有结果", bool(ok_preset) and "已生成" in scene.ef_bridge_preset_status,
      scene.ef_bridge_preset_status)
check("预设执行: 新建了独立 action(EF_Preset_*)",
      new_action is not None and new_action.name.startswith("EF_Preset_"),
      new_action.name if new_action else "无")
check("预设执行: 用户原有 action 未被删除/改写",
      bpy.data.actions.get("USER_Original") is not None and len(bpy.data.actions) == n_before + 1,
      "actions=%d(执行前 %d)" % (len(bpy.data.actions), n_before))
fcs = mod.action_fcurves(new_action)
n_keys = sum(len(fc.keyframe_points) for fc in fcs)
check("预设执行: 关键帧已写入", n_keys > 10, "fcurve=%d 关键帧=%d" % (len(fcs), n_keys))
moving = [fc for fc in fcs if fc.data_path.endswith("location") and fc.array_index in (0, 1)]
check("预设执行: 机位确实在动(首末值不同)",
      any(abs(fc.evaluate(1) - fc.evaluate(scene.frame_end)) > 0.05 for fc in moving),
      "检查了 %d 条位置曲线" % len(moving))
check("预设执行: 帧范围按 秒×帧率 设定",
      scene.frame_end == 1 + int(round(2.0 * scene.render.fps)),
      "frame_end=%d fps=%s" % (scene.frame_end, scene.render.fps))
check("预设执行: 时间轴标记已放置", any(m.name == "起点" for m in scene.timeline_markers))

# ---- 8c) 0.3.5: 示例预设同步(这条正是"用户目录里一直是旧版预设"那个真缺陷的守门人) ----
sync_dir = os.path.join(__import__("tempfile").gettempdir(), "ef_preset_sync")
if os.path.isdir(sync_dir):
    import shutil as _sh
    _sh.rmtree(sync_dir, ignore_errors=True)
os.makedirs(sync_dir, exist_ok=True)
bundled = mod._bundled_preset_dir()
b_files = sorted(f for f in os.listdir(bundled) if f.endswith('.py'))
mod._bundled_synced = False
mod._sync_bundled_presets(scene, sync_dir)
check("同步: 空目录首次同步会装入全部示例预设 + 写清单",
      all(os.path.exists(os.path.join(sync_dir, f)) for f in b_files)
      and os.path.exists(os.path.join(sync_dir, '.bundled.json')),
      "%d 个示例" % len(b_files))

mod._bundled_synced = False
n2 = mod._sync_bundled_presets(scene, sync_dir)
check("同步: 已是最新时不做任何改动", n2 == 0, "改动数 %d" % n2)

one = b_files[0]
with open(os.path.join(sync_dir, one), "w", encoding="utf-8") as fh:
    fh.write("# 假装这是老版本装进去的旧内容\n")
os.remove(os.path.join(sync_dir, '.bundled.json'))      # 老版本没有清单记录
mod._bundled_synced = False
mod._sync_bundled_presets(scene, sync_dir)
with open(os.path.join(sync_dir, one), encoding="utf-8") as fh:
    now = fh.read()
with open(os.path.join(bundled, one), encoding="utf-8") as fh:
    want = fh.read()
check("同步: 旧版(清单无记录)会被备份并更新为随包版本",
      now == want and os.path.exists(os.path.join(sync_dir, '_bundled_backup', one)),
      "备份目录: %s" % os.path.isdir(os.path.join(sync_dir, '_bundled_backup')))

with open(os.path.join(sync_dir, one), "w", encoding="utf-8") as fh:
    fh.write("# 用户自己改过的版本\n")
mod._bundled_synced = False
mod._sync_bundled_presets(scene, sync_dir)
with open(os.path.join(sync_dir, one), encoding="utf-8") as fh:
    kept_user = fh.read()
check("同步: 用户改过的文件不动, 新版放到 _bundled_new/",
      kept_user.strip() == "# 用户自己改过的版本"
      and os.path.exists(os.path.join(sync_dir, '_bundled_new', one)),
      "用户版保留=%s" % ("# 用户自己改过" in kept_user))

# 重名 -> 自动加序号(默认新建, 绝不覆盖)
mod._run_preset(scene, IDX_OK)
ef_names = sorted(a.name for a in bpy.data.actions if a.name.startswith("EF_Preset_"))
check("预设执行: 重名自动新建序号版本(不覆盖)", len(ef_names) == 2 and ef_names[1].endswith("_2"),
      ef_names)

# 下拉栏(v0.3.1): 动态枚举自动跟随目录内容 + 双向同步
items = mod._preset_items(scene, bpy.context)   # Blender 的 items 回调签名是 (self, context)
item_names = [it[1] for it in items]
check("下拉栏: 条目数 = 扫描到的预设数", len(items) == len(got), item_names)
check("下拉栏: 条目标识符是索引、名字来自元数据",
      items[IDX_OK][0] == str(IDX_OK) and items[IDX_OK][1] == "测试环绕", items[IDX_OK])
scene.ef_bridge_preset = str(IDX_BOOM)
check("下拉栏: 选中某项时索引同步", mod._preset_current_index(scene, got) == IDX_BOOM,
      "index=%d 期望 %d" % (scene.ef_bridge_preset_index, IDX_BOOM))
# 语义: 下拉栏(枚举)是 UI 的权威来源 —— 用户点的是它, 所以 index 跟着 enum 走。
# 程序化路径(按索引执行)则两边都设, 保证下拉栏跟着切过去。
# 0.3.2 的关键回归防线: 面板绘制(draw)路径**绝不能写 Blender 属性** ——
# 写了一次(把枚举同步回 index), Blender 会抛错并跳过该面板剩余绘制,
# 表现就是"切换预设后生成按钮消失"。这里静态地把三个绘制期函数跑一遍并比对属性。
before = (scene.ef_bridge_preset, scene.ef_bridge_preset_index, scene.ef_bridge_preset_status)
_draw_path = (mod._scan_presets(scene), mod._preset_items(scene, bpy.context),
              mod._preset_current_index(scene, got))
after = (scene.ef_bridge_preset, scene.ef_bridge_preset_index, scene.ef_bridge_preset_status)
check("绘制路径只读: 三个绘制期函数不改动任何属性", before == after,
      "before=%s after=%s" % (before, after))

# 选中项改变 -> update 回调把内部索引同步过去(draw 之外执行, 允许写)
scene.ef_bridge_preset = "1"
check("下拉栏切换: update 回调把索引同步过去", scene.ef_bridge_preset_index == 1,
      "index=%d" % scene.ef_bridge_preset_index)
scene.ef_bridge_preset = "0"
mod._run_preset(scene, IDX_OK)
check("下拉栏: 按索引执行时下拉栏也切过去",
      str(scene.ef_bridge_preset) == str(IDX_OK) and scene.ef_bridge_preset_index == IDX_OK,
      "enum=%s index=%d" % (scene.ef_bridge_preset, scene.ef_bridge_preset_index))
# 模拟"选中的预设文件被删掉了" -> 下拉栏里的值变成陈旧值, 应当被夹回合法范围(而不是报错)
scene.ef_bridge_preset = str(len(got) - 1)
mod._preset_current_index(scene, got)
clamped = mod._preset_current_index(scene, got[:-1])      # 少一个预设
check("下拉栏: 预设被删后选中项被夹回合法范围",
      clamped == len(got) - 2, "夹回 %d (原 %d)" % (clamped, len(got) - 1))
# 换个(空)目录 -> 下拉栏应当自动跟随并给出占位条目(顺带验证"自动读取目录内容")
empty_dir = os.path.join(__import__("tempfile").gettempdir(), "ef_preset_empty")
os.makedirs(empty_dir, exist_ok=True)
for _f in os.listdir(empty_dir):
    try:
        os.remove(os.path.join(empty_dir, _f))
    except OSError:
        pass
saved_dir = scene.ef_bridge_preset_dir
scene.ef_bridge_preset_dir = empty_dir
mod._preset_cache = None
items_empty = mod._preset_items(scene, bpy.context)
check("下拉栏: 换目录后自动跟随(空目录给占位条目, 不报错)",
      items_empty[0][0] == "%NONE%" and mod._preset_current_index(scene, []) == 0,
      items_empty[0][:2])
scene.ef_bridge_preset_dir = saved_dir
mod._preset_cache = None
check("下拉栏: 换回原目录后条目恢复", len(mod._preset_items(scene, bpy.context)) >= 2,
      [it[1] for it in mod._preset_items(scene, bpy.context)])
ok_boom = mod._run_preset(scene, IDX_BOOM)
check("预设异常: 不抛出、返回失败并写明定位",
      (not ok_boom) and "失败" in scene.ef_bridge_preset_status and os.path.exists(mod.PRESET_LOG),
      scene.ef_bridge_preset_status)

# ---- 9b) 0.3.6: 预设数据与路径设置"在包内"(方便整个包分发) ----
check("包内路径: 源码树里没有 package_path.txt 时, 默认目录退回插件自带的示例预设",
      mod._package_root() is None and mod._default_preset_dir() == mod._bundled_preset_dir(),
      mod._default_preset_dir())

fake_pkg = os.path.join(__import__("tempfile").gettempdir(), "ef_fake_pkg")
os.makedirs(os.path.join(fake_pkg, "presets"), exist_ok=True)
with open(os.path.join(fake_pkg, "paths.ini"), "w", encoding="utf-8") as fh:
    fh.write("; 注释行\npackage_dir=%s\npreset_dir=%s\n" % (fake_pkg,
                                                          os.path.join(fake_pkg, "presets")))
_orig_pkg_root = mod._package_root
mod._package_root = lambda: fake_pkg
check("包内路径: 有 package_path.txt 时默认目录 = <包根>/presets",
      mod._default_preset_dir() == os.path.join(fake_pkg, "presets"),
      mod._default_preset_dir())
other = os.path.join(fake_pkg, "presets2")
os.makedirs(other, exist_ok=True)
with open(os.path.join(fake_pkg, "paths.ini"), "w", encoding="utf-8") as fh:
    fh.write("preset_dir=%s\n" % other)
check("包内路径: paths.ini 的 preset_dir 优先(可自定义且随包分发)",
      mod._default_preset_dir() == other, mod._default_preset_dir())
check("包内路径: 能解析 ini 里的键(忽略注释行)",
      mod._ini_value(os.path.join(fake_pkg, "paths.ini"), "preset_dir") == other)
mod._package_root = _orig_pkg_root

mod.unregister()
try:
    os.remove(mod.ARM_TOKEN_FILE)
    os.remove(mod.MODE_FILE)
    os.remove(mod.SETTINGS_FILE)
except OSError:
    pass
print("")
print("SELFTEST RESULT: %s" % ("ALL OK" if not fails else ("FAILED: " + ", ".join(fails))))
