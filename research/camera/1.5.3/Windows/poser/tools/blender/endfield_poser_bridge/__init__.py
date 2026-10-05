# -*- coding: utf-8 -*-
"""Endfield Poser Bridge — 把游戏角色骨骼桥接进 Blender 摆姿。

配合游戏内插件 Endfield Poser（poser.dll）的 localhost HTTP 服务器使用：
    http://127.0.0.1:18923

用法：
  1. 游戏内插件已加载（web 服务器随插件启动）。
  2. Blender 安装本插件：编辑(Edit) → 偏好设置(Preferences) → 插件(Add-ons)
     → 安装(Install) 选择本文件 → 勾选启用 "Endfield Poser Bridge"。
  3. 3D 视图 N 面板 → "Endfield" 分类 → Connect 连接游戏并创建骨架。
  4. 在 Blender Pose 模式摆姿势，点 "同步到游戏" 把姿势写回游戏（自动冻结）。

坐标系：Unity/终末地 Y 向上、左手系；Blender Z 向上、右手系。
换算：B_pos = (U_x, U_z, -U_y)；局部旋转增量 B_delta = R_x(-90°) * U_delta * R_x(90°)。
连接时把游戏当前姿势捕获为 Blender rest pose（v1 策略），Blender 中编辑的是
相对 rest 的 delta，写回游戏时再叠加回游戏 rest。
"""

bl_info = {
    "name": "Endfield Poser Bridge",
    "author": "Endfield Poser",
    "version": (0, 1, 0),
    "blender": (5, 2, 0),
    "location": "3D View > N panel > Endfield",
    "description": "Bridge Arknights: Endfield character bones to Blender for posing",
    "category": "Rigging",
}

import json
import math
import urllib.request

import bpy
import mathutils

HOST = "127.0.0.1"
PORT = 18923
ARM_NAME = "EndfieldRig"

# 全局会话状态
_state = {
    "connected": False,
    "status": "未连接",
    "rest_quat": {},     # 骨骼名 -> 游戏 local 静息四元数
    "rest_pos": {},      # 骨骼名 -> 游戏 local 静息位置
    "root_offset": mathutils.Vector((0.0, 0.0, 0.0)),
    "bones_rev": 0,      # 游戏骨骼列表版本号（角色切换后变化）
    "auto": False,
}

# R_x(-90°)：Unity Y-up → Blender Z-up 的轴向旋转（四元数 w,x,y,z）
AXIS_Q = mathutils.Quaternion(
    (math.cos(math.radians(-45.0)), math.sin(math.radians(-45.0)), 0.0, 0.0)
)


def _base():
    return "http://%s:%d" % (HOST, PORT)


def api_get(path, timeout=3):
    with urllib.request.urlopen(_base() + path, timeout=timeout) as resp:
        return json.loads(resp.read().decode("utf-8"))


def api_post(path, data, timeout=3):
    req = urllib.request.Request(
        _base() + path,
        data=json.dumps(data).encode("utf-8"),
        headers={"Content-Type": "application/json"},
    )
    with urllib.request.urlopen(req, timeout=timeout) as resp:
        return json.loads(resp.read().decode("utf-8"))


# ---- 坐标/旋转换算 ----
def u_to_b_vec(u):
    """Unity (x, y 上, z) -> Blender (x, z 上, -y)"""
    return mathutils.Vector((u[0], u[2], -u[1]))


def b_to_u_vec(b):
    """Blender -> Unity（u_to_b 的逆）"""
    return mathutils.Vector((b[0], -b[2], b[1]))


def uq_to_bq(q_u):
    return AXIS_Q * q_u * AXIS_Q.inverted()


def bq_to_uq(q_b):
    return AXIS_Q.inverted() * q_b * AXIS_Q


def _armature():
    obj = bpy.data.objects.get(ARM_NAME)
    return obj if obj and obj.type == "ARMATURE" else None


def _sanitize(name):
    return name.replace("\\", "_").replace("/", "_")


# ---- 操作符：连接并建臂 ----
class EPB_OT_connect(bpy.types.Operator):
    bl_idname = "endfield.connect"
    bl_label = "连接游戏并创建骨架"
    bl_description = "拉取游戏骨骼，创建 Armature（当前姿势作为 rest pose）"

    def execute(self, context):
        try:
            data = api_get("/api/allbones")
            allbones = data["bones"]
        except Exception as exc:  # noqa: BLE001
            _state["status"] = "连接失败: %s" % exc
            self.report({"ERROR"}, _state["status"])
            return {"CANCELLED"}

        _state["bones_rev"] = data.get("rev", 0)
        _state["rest_quat"] = {}
        _state["rest_pos"] = {}
        for b in allbones:
            key = _sanitize(b["name"])
            _state["rest_quat"][key] = mathutils.Quaternion(
                (b.get("lrw", 1.0), b.get("lrx", 0.0),
                 b.get("lry", 0.0), b.get("lrz", 0.0))
            )
            _state["rest_pos"][key] = mathutils.Vector(
                (b.get("lpx", 0.0), b.get("lpy", 0.0), b.get("lpz", 0.0))
            )

        # 根骨世界坐标作为原点偏移（角色远离 Blender 原点时自动居中）
        root = None
        for b in allbones:
            if b.get("parent", -2) < 0:
                root = b
                break
        if root is None and allbones:
            root = allbones[0]
        _state["root_offset"] = (
            u_to_b_vec((root["x"], root["y"], root["z"]))
            if root else mathutils.Vector((0.0, 0.0, 0.0))
        )

        # 清理旧骨架
        old = _armature()
        if old is not None:
            bpy.data.objects.remove(old, do_unlink=True)

        arm_data = bpy.data.armatures.new(ARM_NAME)
        obj = bpy.data.objects.new(ARM_NAME, arm_data)
        context.scene.collection.objects.link(obj)
        bpy.context.view_layer.objects.active = obj
        obj.select_set(True)

        pos_b = []
        for b in allbones:
            pos_b.append(u_to_b_vec((b["x"], b["y"], b["z"])) - _state["root_offset"])

        # 世界旋转（Unity 空间，沿父链连乘），供叶子骨方向用
        world_q = []
        for i, b in enumerate(allbones):
            q = mathutils.Quaternion(
                (b.get("lrw", 1.0), b.get("lrx", 0.0),
                 b.get("lry", 0.0), b.get("lrz", 0.0)))
            pi = b.get("parent", -1)
            if pi >= 0 and pi < len(world_q) and world_q[pi] is not None:
                world_q.append(world_q[pi] * q)
            else:
                world_q.append(q)

        # 每根骨的 tail 指向它的第一个子关节（真实长度与朝向）
        first_child = {}
        for i, b in enumerate(allbones):
            pi = b.get("parent", -1)
            if pi >= 0 and pi not in first_child:
                first_child[pi] = i

        bpy.ops.object.mode_set(mode="EDIT")
        edit_bones = arm_data.edit_bones
        created = []
        for i, b in enumerate(allbones):
            created.append(edit_bones.new(_sanitize(b["name"])))
        # 父级链：先设 parent。对“父尾 = 第一个子骨 head”的骨
        # 用 use_connect=True 连接（标准的 Blender 链表示；也避免 Blender 5.2
        # 对未连接子骨 head/tail 存储的异常值）。其余子骨不连接。
        for i, b in enumerate(allbones):
            pi = b.get("parent", -1)
            if pi >= 0 and pi < len(created):
                created[i].parent = created[pi]
                created[i].use_connect = (first_child.get(pi) == i)
        # 最后统一设置 head/tail（父级赋值会移动骨头，位置在最后覆盖）
        for i, b in enumerate(allbones):
            head = pos_b[i]
            if i in first_child:
                tail = pos_b[first_child[i]]
            else:
                # 叶子骨：沿自身世界方向伸一小段（Unity +Y → Blender 方向）
                d_u = world_q[i] @ mathutils.Vector((0.0, 1.0, 0.0))
                d_b = u_to_b_vec(d_u)
                if d_b.length < 1e-4:
                    d_b = mathutils.Vector((0.0, 0.0, 1.0))
                tail = head + d_b.normalized() * 0.05
            created[i].head = head
            created[i].tail = tail

        bpy.ops.object.mode_set(mode="POSE")
        _state["connected"] = True
        _state["status"] = "已连接：%d 根骨骼" % len(allbones)
        self.report({"INFO"}, _state["status"])
        return {"FINISHED"}


# ---- 操作符：刷新骨架（角色切换后重建 Armature 并导入当前姿势）----
class EPB_OT_refresh(bpy.types.Operator):
    bl_idname = "endfield.refresh"
    bl_label = "刷新骨架（角色切换后）"
    bl_description = "重新拉取游戏骨骼重建 Armature，并导入当前姿势"

    def execute(self, context):
        bpy.ops.endfield.connect("EXEC_DEFAULT")
        bpy.ops.endfield.sync_to_blender("EXEC_DEFAULT")
        return {"FINISHED"}


# ---- 操作符：游戏 → Blender ----
class EPB_OT_sync_to_blender(bpy.types.Operator):
    bl_idname = "endfield.sync_to_blender"
    bl_label = "导入姿势（游戏→Blender）"
    bl_description = "把游戏当前姿势同步到 Blender（相对 rest 的 delta）"

    def execute(self, context):
        arm = _armature()
        if arm is None:
            self.report({"ERROR"}, "请先 Connect 创建骨架")
            return {"CANCELLED"}
        try:
            pose = api_get("/api/pose")["pose"]
        except Exception as exc:  # noqa: BLE001
            _state["status"] = "读取姿势失败: %s" % exc
            self.report({"ERROR"}, _state["status"])
            return {"CANCELLED"}

        bpy.context.view_layer.objects.active = arm
        bpy.ops.object.mode_set(mode="POSE")
        pbones = arm.pose.bones
        updated = 0
        for e in pose:
            name = e.get("n") or ""
            if name not in pbones:
                continue
            key = _sanitize(name)
            rest_q = _state["rest_quat"].get(key)
            if rest_q is None:
                continue
            q_u = mathutils.Quaternion(
                (e["q"][3], e["q"][0], e["q"][1], e["q"][2]))
            delta_u = q_u * rest_q.inverted()
            pb = pbones[name]
            pb.rotation_mode = "QUATERNION"
            pb.rotation_quaternion = uq_to_bq(delta_u).normalized()
            rest_p = _state["rest_pos"].get(key,
                                            mathutils.Vector((0.0, 0.0, 0.0)))
            delta_p_u = mathutils.Vector(
                (e["p"][0] - rest_p.x, e["p"][1] - rest_p.y,
                 e["p"][2] - rest_p.z))
            pb.location = u_to_b_vec(delta_p_u)
            updated += 1
        _state["status"] = "已导入 %d 根骨骼" % updated
        self.report({"INFO"}, _state["status"])
        return {"FINISHED"}


# ---- 操作符：Blender → 游戏 ----
class EPB_OT_sync_to_game(bpy.types.Operator):
    bl_idname = "endfield.sync_to_game"
    bl_label = "导出姿势（Blender→游戏）"
    bl_description = "把 Blender 当前姿势写回游戏（自动冻结角色）"

    def execute(self, context):
        arm = _armature()
        if arm is None:
            self.report({"ERROR"}, "请先 Connect 创建骨架")
            return {"CANCELLED"}
        try:
            status = api_get("/api/status")
            if not status.get("frozen"):
                api_post("/api/freeze", {"on": True})
        except Exception as exc:  # noqa: BLE001
            _state["status"] = "游戏通信失败: %s" % exc
            self.report({"ERROR"}, _state["status"])
            return {"CANCELLED"}

        payload = []
        for pb in arm.pose.bones:
            key = pb.name
            rest_q = _state["rest_quat"].get(key)
            if rest_q is None:
                continue  # 非游戏骨骼（如自定义控制骨）不写回
            q_b = pb.rotation_quaternion
            delta_u = bq_to_uq(q_b)
            target_q = (delta_u * rest_q).normalized()
            entry = {
                "n": key,
                "q": [target_q.x, target_q.y, target_q.z, target_q.w],
            }
            rest_p = _state["rest_pos"].get(
                key, mathutils.Vector((0.0, 0.0, 0.0)))
            delta_p_u = b_to_u_vec(pb.location)
            target_p = rest_p + delta_p_u
            entry["p"] = [target_p.x, target_p.y, target_p.z]
            payload.append(entry)

        try:
            api_post("/api/pose", {"pose": payload})
        except Exception as exc:  # noqa: BLE001
            _state["status"] = "写回失败: %s" % exc
            self.report({"ERROR"}, _state["status"])
            return {"CANCELLED"}
        _state["status"] = "已写回 %d 根骨骼" % len(payload)
        self.report({"INFO"}, _state["status"])
        return {"FINISHED"}


# ---- 自动同步（Blender → 游戏，定时器）----
def _auto_timer():
    if _state["auto"]:
        try:
            st = api_get("/api/status")
            rev = st.get("bones_rev", 0)
            if rev != _state.get("bones_rev", 0):
                # 游戏角色切换：先重建骨架并导入姿势，本次跳过写回
                _state["bones_rev"] = rev
                bpy.ops.endfield.refresh("EXEC_DEFAULT")
            else:
                arm = _armature()
                if arm is not None and arm.pose is not None:
                    bpy.ops.endfield.sync_to_game("EXEC_DEFAULT")
        except Exception:  # noqa: BLE001
            pass
    return 0.15


def _on_auto_changed(self, context):
    _state["auto"] = bool(self.epb_auto)


bpy.types.Scene.epb_auto = bpy.props.BoolProperty(
    name="自动同步（Blender→游戏）",
    description="开启后持续把 Blender 姿势写回游戏（约 6 次/秒）",
    default=False,
    update=_on_auto_changed,
)


# ---- 面板 ----
class EPB_PT_main(bpy.types.Panel):
    bl_label = "Endfield Poser Bridge"
    bl_idname = "EPB_PT_main"
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"
    bl_category = "Endfield"

    def draw(self, context):
        layout = self.layout
        layout.label(text=_state["status"])
        layout.separator()
        layout.operator("endfield.connect", icon="WORLD")
        layout.operator("endfield.refresh", icon="FILE_REFRESH")
        layout.operator("endfield.sync_to_blender", icon="IMPORT")
        layout.operator("endfield.sync_to_game", icon="EXPORT")
        layout.prop(context.scene, "epb_auto")


classes = (
    EPB_OT_connect,
    EPB_OT_refresh,
    EPB_OT_sync_to_blender,
    EPB_OT_sync_to_game,
    EPB_PT_main,
)


def register():
    for cls in classes:
        bpy.utils.register_class(cls)
    bpy.app.timers.register(_auto_timer, persistent=True)


def unregister():
    bpy.app.timers.unregister(_auto_timer)
    for cls in reversed(classes):
        bpy.utils.unregister_class(cls)
    del bpy.types.Scene.epb_auto


if __name__ == "__main__":
    register()
