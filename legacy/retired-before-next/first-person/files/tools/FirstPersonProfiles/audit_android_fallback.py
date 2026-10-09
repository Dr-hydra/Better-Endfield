"""Offline regression for the Android no-geometry first-person path.

Android original character meshes are not readable and the GPU readback
icalls are stripped, so a mixed renderer cannot receive a cropped index copy.
This script replays, on existing Windows native.json graphs and extracted raw
VB/IB (the same structural evidence as audit_local_geometry.py), what the
runtime does for each visible LOD1 renderer:

* before: standalone Head/tail palettes (and Face/Hair local Head+Neck
  palettes) are hidden; hairshadow is skipped; every mixed renderer is kept.
* after:  pure-Head hairshadow is hidden; a mixed renderer collapses its live
  Head-subtree, tail-chain and profile head-accessory palette entries onto one
  zero-scale anchor per root (Neck and body entries untouched).

"Residual" counts visible triangles whose three vertices are fully weighted to
Hide semantics (Head subtree, head accessories, tail) and are still drawn.
"Moved" counts drawn triangles with a partial collapse weight. The Android
renderer shadow mode is not part of this evidence: the palette fallback is
only applied by the runtime to renderers that do not cast shadows.
"""
from __future__ import annotations

import argparse
import collections
import json
import math
import struct
from pathlib import Path

import audit_local_geometry as aud
import generate_profiles as gen

ROLES = aud.ROLES
LOD = "lod1"  # Android world renderers observed on device are lod1..lod3.


def region_of(path, source):
    return aud.bone_region(path, source)


def anchor_of(path, source):
    # Mirrors FpRedirectRoot: Head, else the highest tail-named ancestor, else
    # a profile HeadAccessory/Tail root. Neck and body never collapse.
    if gen.below(path, gen.HEAD):
        return gen.HEAD
    parts = path.split("/")
    for index, part in enumerate(parts):
        if gen.TAIL.match(part):
            return "/".join(parts[:index + 1])
    for rule in source["bone_rules"]:
        if rule["region"] in ("HeadAccessory", "Tail") and gen.below(path, rule["path"]):
            return rule["path"]
    return None


def kind_of(path, source):
    # Mirrors FpBoneKind: Head subtree and tail ancestry are 1, Neck is 2,
    # otherwise the profile semantic (Hide=1 for proven roots).
    if gen.below(path, gen.HEAD):
        return 1
    if path == gen.NECK:
        return 2
    if any(gen.TAIL.match(part) for part in path.split("/")):
        return 1
    return 1 if region_of(path, source) in ("HeadAccessory", "Tail") else 0


def classify(model, roles_root, manifest):
    profile = next(item for item in manifest["profiles"] if item["model_id"] == model)
    source = profile["sources"][0]
    directory = roles_root / model
    objects, transforms, paths = gen.graph_paths(gen.read_json(directory / "native.json"))
    root = source["model_root"]
    rows = []
    for renderer in objects.values():
        if renderer["type"] != "SkinnedMeshRenderer":
            continue
        go = objects[renderer["game_object"]["id"]]
        transform = next(ref["id"] for ref in go["components"] if ref.get("id") in transforms)
        full = paths[transform]
        if not full.startswith(f"{root}/Mesh_all/{LOD}/"):
            continue
        relative = full[len(root) + 1:]
        palette = [paths[ref["id"]][len(root) + 1:] for ref in renderer["bones"]]
        kinds = [kind_of(path, source) for path in palette]
        anchors = [anchor_of(path, source) for path in palette]
        if not any(kinds):
            continue
        mesh = objects[renderer["mesh"]["id"]]
        name = mesh["name"].lower()
        hairshadow = "hairshadow" in name
        rule = next((item for item in source["local_rules"] if item["renderer_path"] == relative), None)
        pure = all(kind == 1 for kind in kinds)
        scoped = (rule is not None and rule["scope"] in ("Face", "Hair") and
                  all(kind in (1, 2) for kind in kinds) and any(kind == 1 for kind in kinds))
        direct = (pure or scoped) and not (hairshadow and not pure)
        try:
            vertices = aud.decode_skin(mesh, directory / "meshes")
            triangles = aud.draw_triangles(mesh, directory / "meshes")
        except (ValueError, KeyError, struct.error) as error:
            rows.append(dict(model_id=model, renderer_path=relative, status="unsupported", reason=str(error)))
            continue
        info = []
        for _, influences in vertices:
            hide = other = 0.0
            collapse = collections.defaultdict(float)
            for bone, weight in influences:
                if weight <= 0:
                    continue
                if kinds[bone] == 1:
                    hide += weight
                else:
                    other += weight
                if anchors[bone]:
                    collapse[anchors[bone]] += weight
            unanchored = sum(weight for bone, weight in influences if weight > 0 and not anchors[bone])
            info.append((other <= 0, dict(collapse), unanchored))
        hidable = collapsed = moved = 0
        for triangle in triangles:
            if len(set(triangle)) < 3:
                continue
            states = [info[index] for index in triangle]
            if all(state[0] for state in states):
                hidable += 1
            keys = {key for state in states for key in state[1]}
            if len(keys) == 1 and all(state[2] <= 0 for state in states):
                collapsed += 1
            elif keys:
                moved += 1
        drawn = sum(1 for triangle in triangles if len(set(triangle)) == 3)
        if direct:
            before_mode = after_mode = "direct_hide"
        elif hairshadow:
            before_mode, after_mode = "skipped_hairshadow", ("direct_hide" if pure else "skipped_hairshadow")
        else:
            before_mode, after_mode = "retained_mixed", "palette_collapse"
        if hairshadow and pure:
            before_mode = "skipped_hairshadow"
        before_residual = 0 if before_mode == "direct_hide" else hidable
        if after_mode == "direct_hide":
            after_residual, after_moved = 0, 0
        elif after_mode == "palette_collapse":
            # A collapsed triangle is zero-area; a Hide-only triangle that spans
            # two roots or uses tail bones is still drawn.
            after_residual = max(0, hidable - collapsed)
            after_moved = moved
        else:
            after_residual, after_moved = hidable, 0
        rows.append(dict(model_id=model, renderer_path=relative, mesh=mesh["name"], status="checked",
                         drawn_triangles=drawn, hide_only_triangles=hidable,
                         before=before_mode, after=after_mode,
                         residual_before=before_residual, residual_after=after_residual,
                         collapsed_triangles=collapsed if after_mode == "palette_collapse" else 0,
                         moved_triangles=after_moved,
                         collapse_roots=sorted({key.rsplit("/", 1)[-1] for key in anchors if key})
                         if after_mode == "palette_collapse" else []))
    return rows


def audit(roles_root, manifest):
    samples = []
    for model in ROLES:
        samples.extend(classify(model, roles_root, manifest))
    samples.sort(key=lambda row: (row["model_id"], row["renderer_path"]))
    totals = collections.defaultdict(lambda: collections.Counter())
    for row in samples:
        if row["status"] != "checked":
            continue
        totals[row["model_id"]]["residual_before"] += row["residual_before"]
        totals[row["model_id"]]["residual_after"] += row["residual_after"]
        totals[row["model_id"]]["moved_after"] += row["moved_triangles"]
    return dict(schema=1, lod=LOD,
                source="Existing Windows native.json graphs and extracted raw VB/IB, replayed with Android no-geometry policy",
                assumption="Mixed Mesh_all renderers do not cast shadows themselves (HG Shadow_Proxy renderers do); runtime verifies per renderer.",
                android_geometry_verified=False, in_game_verified=False,
                totals={model: dict(sorted(counter.items())) for model, counter in sorted(totals.items())},
                samples=samples)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--roles-root", type=Path, default=Path("F:/zmd_bem/research/identities/roles"))
    parser.add_argument("--manifest", type=Path, default=Path(__file__).with_name("profiles.generated.json"))
    parser.add_argument("--output", type=Path, default=Path(__file__).with_name("android_fallback_regression.generated.json"))
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    data = audit(args.roles_root, gen.read_json(args.manifest))
    content = json.dumps(data, ensure_ascii=False, indent=2) + "\n"
    if args.check:
        if args.output.read_text(encoding="utf-8") != content:
            raise SystemExit("android fallback regression changed")
    else:
        args.output.write_text(content, encoding="utf-8", newline="\n")
    typhoea = data["totals"].get("chr_0034_typhoea", {})
    if typhoea.get("residual_after", 1) >= typhoea.get("residual_before", 0):
        raise SystemExit("Typhoea residual head-only triangles did not decrease")
    for model, total in data["totals"].items():
        print(f"{model}: residual {total.get('residual_before', 0)} -> {total.get('residual_after', 0)}, "
              f"moved {total.get('moved_after', 0)}")


if __name__ == "__main__":
    main()
