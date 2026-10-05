"""Headless anatomy dump of the ARP Mike demo rig (study tool).

Usage:
  blender --background --factory-startup -P tests/mike_anatomy.py -- <blend> <out.txt>
"""

import sys
import bpy

OUT = []


def p(*args):
    OUT.append(" ".join(str(a) for a in args))


def dump_armature(arm):
    p("=" * 70)
    p("ARMATURE:", arm.name)
    p("custom props:", {k: v for k, v in arm.items() if not k.startswith("_")})

    try:
        p("collections:", [c.name for c in arm.data.collections])
    except Exception as e:
        p("collections err:", e)

    bones = arm.data.bones
    p("-" * 70)
    p("BONE TREE (name | parent | connect | head | tail)")

    def walk(parent, depth):
        for b in bones:
            if parent is None:
                match = b.parent is None
            else:
                match = b.parent is not None and b.parent.name == parent
            if match:
                h = tuple(round(v, 3) for v in b.head_local)
                t = tuple(round(v, 3) for v in b.tail_local)
                p("  " * depth + f"{b.name} | parent={b.parent.name if b.parent else '-'} | "
                  f"conn={int(b.use_connect)} | head={h} | tail={t} | len={round(b.length, 3)}")
                walk(b.name, depth + 1)

    for b in bones:
        if b.parent is None:
            walk(b.name, 0)

    p("-" * 70)
    p("POSE BONES: constraints & custom props")
    for pb in arm.pose.bones:
        cons = []
        for c in pb.constraints:
            extra = {}
            for attr in ("chain_length", "target", "subtarget", "pole_target", "pole_subtarget",
                         "pole_angle", "use_tail", "mix_mode", "influence", "space"):
                try:
                    v = getattr(c, attr)
                    if hasattr(v, "name"):
                        v = v.name
                    if v is not None and v != "":
                        extra[attr] = v
                except Exception:
                    pass
            try:
                drv = c.driver
                if drv:
                    extra["driver"] = drv.expression
                    extra["driver_vars"] = [v.name + "=" + v.targets[0].data_path
                                            for v in drv.variables]
            except Exception:
                pass
            cons.append((c.type, c.name, extra))
        props = {k: v for k, v in pb.items() if not k.startswith("_")}
        if cons or props:
            p(f"PB {pb.name}")
            for ct in cons:
                p(f"    CONSTRAINT {ct[0]} '{ct[1]}' {ct[2]}")
            if props:
                p(f"    PROPS {props}")


def main():
    args = sys.argv
    if "--" in args:
        rest = args[args.index("--") + 1:]
    else:
        rest = []
    if len(rest) < 2:
        p("ERROR: pass blend path and out path after --")
        sys.exit(2)
    blend_path, out_path = rest[0], rest[1]

    bpy.ops.wm.open_mainfile(filepath=blend_path)

    p("OBJECTS:")
    for o in bpy.data.objects:
        p(f"  {o.type:12s} {o.name}  loc={tuple(round(v,3) for v in o.location)}")

    for o in bpy.data.objects:
        if o.type == "ARMATURE":
            dump_armature(o)

    with open(out_path, "w", encoding="utf-8") as f:
        f.write("\n".join(OUT))


if __name__ == "__main__":
    main()
