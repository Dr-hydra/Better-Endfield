import os
import sys
import mathutils

# 按脚本位置找桥接插件目录（原来是写死的本机路径，换台机器就跑不了）
_HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, os.path.join(_HERE, "..", "tools", "blender", "endfield_poser_bridge"))
import bpy
import __init__ as epb


def fake_get(path):
    if path == "/api/allbones":
        bones = [
            {"i": 0, "name": "root", "parent": -1, "x": 0, "y": 0, "z": 0,
             "lrx": 0, "lry": 0, "lrz": 0, "lrw": 1},
            {"i": 1, "name": "arm", "parent": 0, "x": 0, "y": 1, "z": 0,
             "lrx": 0, "lry": 0, "lrz": 0, "lrw": 1},
            {"i": 2, "name": "fore", "parent": 1, "x": 0, "y": 2, "z": 0,
             "lrx": 0, "lry": 0, "lrz": 0, "lrw": 1},
            {"i": 3, "name": "hand", "parent": 2, "x": 0, "y": 3, "z": 0,
             "lrx": 0, "lry": 0, "lrz": 0, "lrw": 1},
        ]
        return {"ok": True, "rev": 1, "bones": bones}
    raise RuntimeError(path)


epb.api_get = fake_get
print("MODULE_FILE:", epb.__file__)
epb.register()
bpy.ops.endfield.connect("EXEC_DEFAULT")

arm = bpy.data.objects.get("EndfieldRig")
bones = arm.data.bones
print("armature loc:", arm.location, "rot:", arm.rotation_euler)
for n in ("root", "arm", "fore", "hand"):
    b = bones[n]
    m = b.matrix_local
    print("BONE", n, "pos", tuple(round(v, 4) for v in m.translation),
          "dir", tuple(round(v, 4) for v in m.col[1].to_3d()),
          "parent", b.parent.name if b.parent else None,
          "conn", b.use_connect)
ok = True


def check(name, cond):
    global ok
    print(("PASS " if cond else "FAIL ") + name)
    if not cond:
        ok = False


check("count==4", len(bones) == 4)
check("root.parent None", bones["root"].parent is None)
check("arm.parent root", (bones["arm"].parent and bones["arm"].parent.name) == "root")
check("fore.parent arm", (bones["fore"].parent and bones["fore"].parent.name) == "arm")
check("hand.parent fore", (bones["hand"].parent and bones["hand"].parent.name) == "fore")
check("arm connected", bones["arm"].use_connect)
check("fore connected", bones["fore"].use_connect)
check("hand connected", bones["hand"].use_connect)

exp_pos = {
    "root": (0.0, 0.0, 0.0),
    "arm": (0.0, 0.0, -1.0),
    "fore": (0.0, 0.0, -2.0),
    "hand": (0.0, 0.0, -3.0),
}
for n, ep in exp_pos.items():
    got = tuple(round(v, 4) for v in bones[n].matrix_local.translation)
    check("m_local pos %s" % n, got == ep)
for n in ("root", "arm", "fore"):
    got = tuple(round(v, 4) for v in bones[n].matrix_local.col[1].to_3d())
    check("m_local dir %s" % n, got == (0.0, 0.0, -1.0))
check("hand stub len", abs(bones["hand"].length - 0.05) < 1e-3)

bpy.ops.object.mode_set(mode="POSE")
for n, ep in exp_pos.items():
    got = tuple(round(v, 4) for v in arm.pose.bones[n].matrix.translation)
    check("pose pos %s" % n, got == ep)
bpy.ops.object.mode_set(mode="OBJECT")

print("RESULT:", "OK" if ok else "FAILED")
