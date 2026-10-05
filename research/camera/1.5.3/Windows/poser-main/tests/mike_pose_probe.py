"""Empirical probe: how does the ARP Mike rig drive deform bones in IK vs FK mode?

Usage:
  blender --background --factory-startup -P tests/mike_pose_probe.py -- <blend> <out.txt>
"""

import sys
import bpy
from mathutils import Quaternion, Vector

OUT = []


def p(*args):
    OUT.append(" ".join(str(a) for a in args))


def head(pb):
    return pb.matrix.translation.copy()


def mat_delta(pb):
    m = pb.matrix
    return (tuple(round(v, 4) for v in m.translation),
            tuple(round(v, 4) for v in m.to_quaternion()))


def main():
    args = sys.argv
    print("ARGS:", args, flush=True)
    rest = args[args.index("--") + 1:] if "--" in args else []
    if len(rest) < 2:
        p("ERROR: pass blend and out paths")
        print("REST:", rest, flush=True)
        sys.exit(2)
    bpy.ops.wm.open_mainfile(filepath=rest[0])

    arm = bpy.data.objects["mike_rig"]
    pose = arm.pose
    pb = pose.bones

    p("ANIMATION DRIVERS (armature):")
    ad = arm.animation_data
    if ad and ad.drivers:
        for fc in ad.drivers:
            try:
                expr = fc.driver.expression
                vars_ = [(v.name, [t.data_path for t in v.targets])
                         for v in fc.driver.variables]
                p(f"  {fc.data_path}  = {expr}  vars={vars_}")
            except Exception as e:
                p("  err", fc.data_path, e)
    else:
        p("  (none)")

    p("CONSTRAINTS FULL (key bones):")
    key_bones = ["c_arm_fk.l", "arm_fk.l", "c_forearm_fk.l", "forearm_fk.l",
                 "c_hand_fk_scale_fix.l", "c_hand_fk.l",
                 "arm.l", "forearm.l", "hand.l",
                 "arm_ik.l", "forearm_ik.l", "arm_ik_nostr.l", "forearm_ik_nostr.l",
                 "c_hand_ik.l", "c_arms_pole.l",
                 "c_shoulder.l", "c_thigh_b.l", "thigh.l", "leg.l",
                 "thigh_ik.l", "leg_ik.l", "thigh_ik_nostr.l", "leg_ik_nostr.l",
                 "c_foot_ik.l", "foot_ik_target.l", "c_leg_pole.l",
                 "foot_fk.l", "leg_fk.l", "c_leg_fk.l", "c_foot_fk.l"]
    for name in key_bones:
        if name not in pb:
            p(f"  !! missing bone {name}")
            continue
        b = pb[name]
        cons = []
        for c in b.constraints:
            info = {
                "type": c.type,
                "name": c.name,
                "mute": c.mute,
                "influence": round(c.influence, 3),
            }
            for attr in ("target", "subtarget", "chain_length", "pole_target",
                         "pole_subtarget", "pole_angle", "use_tail", "mix_mode",
                         "map_from", "map_to", "from_min_x", "from_max_x",
                         "to_min_x", "to_max_x"):
                try:
                    v = getattr(c, attr)
                    if hasattr(v, "name"):
                        v = v.name
                    if v is not None and str(v) != "":
                        info[attr] = v
                except Exception:
                    pass
            try:
                d = c.driver
                if d:
                    info["driver"] = d.expression
                    info["driver_vars"] = [v.name + "=" + v.targets[0].data_path
                                           for v in d.variables]
            except Exception as e:
                info["driver_err"] = str(e)
            cons.append(info)
        if cons:
            p(f"  {name}:")
            for ci in cons:
                p(f"      {ci}")

    # --- empirical pose tests ---
    def set_rest():
        for b in pose.bones.values():
            b.matrix_basis.identity()
        # custom props back to defaults
        for b in pose.bones.values():
            for k in list(b.keys()):
                if k in ("ik_fk_switch", "auto_stretch", "stretch_length",
                         "fix_roll", "leg_pin", "thigh_twist", "fingers_grasp"):
                    if k == "ik_fk_switch":
                        b[k] = 0.0
                    elif k in ("auto_stretch", "stretch_length", "fix_roll", "fingers_grasp"):
                        b[k] = 1.0 if k != "fingers_grasp" else 0.0
                    elif k in ("leg_pin", "thigh_twist"):
                        b[k] = 0.0

    watch = ["c_arm_fk.l", "arm_fk.l", "c_forearm_fk.l", "forearm_fk.l",
             "arm.l", "forearm.l", "hand.l",
             "arm_ik.l", "forearm_ik.l", "c_hand_ik.l",
             "c_shoulder.l", "c_hand_fk.l"]

    def snapshot():
        bpy.context.view_layer.update()
        return {n: mat_delta(arm.pose.bones[n]) for n in watch}

    def diff(before, after):
        lines = []
        for n in watch:
            b = before[n]
            a = after[n]
            if b != a:
                lines.append(f"    {n}: loc {b[0]} -> {a[0]}  rot {b[1]} -> {a[1]}")
        return lines

    set_rest()
    dg = bpy.context.evaluated_depsgraph_get()
    base = snapshot()

    q = Quaternion((0.7071, 0.0, 0.7071, 0.0))  # 90 deg around Y (world-ish)
    p("rotation modes:", {n: pb[n].rotation_mode for n in watch})

    for switch in (0.0, 1.0):
        set_rest()
        pb["c_hand_ik.l"]["ik_fk_switch"] = switch
        dg = bpy.context.evaluated_depsgraph_get()
        before = snapshot()
        pb["c_arm_fk.l"].rotation_euler = (0.0, 1.5708, 0.0)
        dg = bpy.context.evaluated_depsgraph_get()
        after = snapshot()
        p(f"\n=== ROTATE c_arm_fk.l by 90deg Y | ik_fk_switch={switch} ===")
        for line in diff(before, after):
            p(line)

    # Move IK target in both modes
    for switch in (0.0, 1.0):
        set_rest()
        pb["c_hand_ik.l"]["ik_fk_switch"] = switch
        dg = bpy.context.evaluated_depsgraph_get()
        before = snapshot()
        pb["c_hand_ik.l"].location = Vector((0.6, 0.2, 1.4))
        dg = bpy.context.evaluated_depsgraph_get()
        after = snapshot()
        p(f"\n=== MOVE c_hand_ik.l to (0.6,0.2,1.4) | ik_fk_switch={switch} ===")
        for line in diff(before, after):
            p(line)

    with open(rest[1], "w", encoding="utf-8") as f:
        f.write("\n".join(OUT))


if __name__ == "__main__":
    main()
