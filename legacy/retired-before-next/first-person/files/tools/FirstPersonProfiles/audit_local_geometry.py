"""Read only existing native VB/IB samples for FP profile regression.

No bundle extraction, hashes, Unity m_Skin reconstruction, palette-number
annotations or original geometry exports. Statistics count actual draw indices.
"""
from __future__ import annotations

import argparse
import collections
import json
import math
import struct
import subprocess
from pathlib import Path

import generate_profiles as gen

ROLES = ("chr_0002_endminm", "chr_0003_endminf", "chr_0004_pelica", "chr_0007_ikut",
         "chr_0013_aglina", "chr_0027_tangtang", "chr_0032_lizhiyan", "chr_0034_typhoea")


def attribute(mesh, number):
    return next((channel for channel in mesh["serialized_channels"]
                 if channel["attribute"] == number and channel["dimension_raw"] & 15), None)


def load_attribute(mesh, meshes_dir, channel):
    stream = next(item for item in mesh["resource_identity"]["vertex_streams"]
                  if item["slot"] == channel["stream"])
    path = meshes_dir / stream["file"]
    if path.parent.resolve() != meshes_dir.resolve():
        raise ValueError("stream outside the declared mesh directory")
    data = path.read_bytes()
    if len(data) != stream["byte_width"] or stream["vertex_count"] != mesh["vertex_count"]:
        raise ValueError("vertex stream size/count mismatch")
    return data, stream["stride"], channel["offset"]


def decode_skin(mesh, meshes_dir):
    indices = attribute(mesh, 13)
    weights = attribute(mesh, 12)
    position = attribute(mesh, 0)
    if not indices or not position or position["format"] != 0 or (position["dimension_raw"] & 15) != 3:
        raise ValueError("unsupported skin or position declaration")
    count = mesh["vertex_count"]
    idx_dim = indices["dimension_raw"] & 15
    idx_fmt = {6: "B", 8: "H", 10: "I"}.get(indices["format"])
    if idx_fmt is None or idx_dim not in (1, 4):
        raise ValueError("unsupported raw indices declaration")
    ibuf, istride, ioffset = load_attribute(mesh, meshes_dir, indices)
    pbuf, pstride, poffset = load_attribute(mesh, meshes_dir, position)
    if weights:
        wdim = weights["dimension_raw"] & 15
        wfmt, scale = {0: ("f", 1), 2: ("B", 255), 4: ("H", 65535)}.get(weights["format"], (None, 1))
        if wfmt is None or wdim != idx_dim:
            raise ValueError("unsupported raw weights declaration")
        wbuf, wstride, woffset = load_attribute(mesh, meshes_dir, weights)
    vertices = []
    for index in range(count):
        bones = struct.unpack_from("<" + idx_fmt * idx_dim, ibuf, index * istride + ioffset)
        values = [value / scale for value in struct.unpack_from("<" + wfmt * idx_dim, wbuf,
                                                               index * wstride + woffset)] if weights else [1.0]
        # The existing stride4 format uses its first index as an implicit rigid
        # weight. Remaining serialized slots are not invented skin influences.
        influences = list(zip(bones, values))
        total = sum(weight for _, weight in influences)
        if not 0.9 <= total <= 1.1 or any(not math.isfinite(weight) or not 0 <= weight <= 1.001
                                         for _, weight in influences):
            raise ValueError("invalid raw skin normalization")
        pos = struct.unpack_from("<3f", pbuf, index * pstride + poffset)
        if not all(math.isfinite(value) for value in pos):
            raise ValueError("invalid raw position")
        vertices.append((pos, influences))
    return vertices


def draw_triangles(mesh, meshes_dir):
    identity = mesh["resource_identity"]
    path = meshes_dir / identity["index_file"]
    if path.parent.resolve() != meshes_dir.resolve():
        raise ValueError("indices outside the declared mesh directory")
    data = path.read_bytes()
    size = identity["index_size"]
    if len(data) != identity["byte_width"] or size not in (2, 4):
        raise ValueError("invalid index buffer size")
    result = []
    for part in mesh["submeshes"]:
        count, start, base = part["index_count"], part["first_byte"], part["base_vertex"]
        if part["topology"] != "Triangles" or count % 3 or start % size or start + count * size > len(data):
            raise ValueError("invalid draw index range")
        values = struct.unpack_from("<" + ("H" if size == 2 else "I") * count, data, start)
        for offset in range(0, count, 3):
            triangle = tuple(value + base for value in values[offset:offset + 3])
            if min(triangle) < 0 or max(triangle) >= mesh["vertex_count"]:
                raise ValueError("draw index outside vertices")
            result.append(triangle)
    return result


def bone_region(path, source):
    matching = [rule for rule in source["bone_rules"] if
                (gen.below(path, rule["path"]) if rule["subtree"] else path == rule["path"])]
    return max(matching, key=lambda rule: len(rule["path"]))["region"] if matching else "Body"


def bind_origin(matrix):
    # Native JSON exports row-major Matrix4x4. Solve A*origin = -translation.
    rows = [[matrix[row * 4 + column] for column in range(3)] + [-matrix[row * 4 + 3]] for row in range(3)]
    for column in range(3):
        pivot = max(range(column, 3), key=lambda row: abs(rows[row][column]))
        if abs(rows[pivot][column]) < 1e-10:
            raise ValueError("singular bindpose")
        rows[column], rows[pivot] = rows[pivot], rows[column]
        scale = rows[column][column]
        rows[column] = [value / scale for value in rows[column]]
        for row in range(3):
            if row != column:
                factor = rows[row][column]
                rows[row] = [value - factor * target for value, target in zip(rows[row], rows[column])]
    return tuple(row[3] for row in rows)


def local_frame(mesh, palette, source):
    if len(mesh["bindposes"]) != len(palette):
        raise ValueError("bindpose/palette count mismatch")
    try:
        neck = bind_origin(mesh["bindposes"][palette.index(source["neck_path"])])
        head = bind_origin(mesh["bindposes"][palette.index(source["head_path"])])
    except ValueError:
        return None
    axis = tuple(h - n for h, n in zip(head, neck))
    length = math.sqrt(sum(value * value for value in axis))
    if length < 1e-5 or not math.isfinite(length):
        return None
    return neck, tuple(value / length for value in axis), length


def audit_renderer(mesh, palette, source, meshes_dir, rule, model, renderer_path, helper):
    vertices = decode_skin(mesh, meshes_dir)
    triangles = draw_triangles(mesh, meshes_dir)
    used = {vertex for triangle in triangles for vertex in triangle}
    regions = [bone_region(path, source) for path in palette]
    quote = lambda value: json.dumps(value, ensure_ascii=False)
    requests = [f"B {quote(model)} {quote(path)}" for path in palette]
    candidates = []
    frame = local_frame(mesh, palette, source) if rule else None
    generic, accessory, local, counts = set(), set(), set(), collections.Counter()
    used_hat_paths = set()
    for index in used:
        position, influences = vertices[index]
        sums = collections.defaultdict(float)
        for bone, weight in influences:
            if weight <= 0:
                continue
            if bone >= len(regions):
                raise ValueError("nonzero raw influence outside current palette")
            sums[regions[bone]] += weight
            if regions[bone] == "HeadAccessory":
                used_hat_paths.add(palette[bone])
        if not sums["Body"] and not sums["Neck"] and not sums["HeadAccessory"]:
            generic.add(index)
        if not sums["Body"] and not sums["Neck"]:
            accessory.add(index)
        if sums["Head"] and sums["Neck"]:
            counts["head_neck_mixed_vertices"] += 1
        if sums["HeadAccessory"]:
            counts["accessory_influenced_vertices"] += 1
        if rule and frame:
            origin, axis, length = frame
            delta = tuple(p - o for p, o in zip(position, origin))
            projection = sum(a * b for a, b in zip(delta, axis))
            along = projection / length
            radius = math.sqrt(sum((d - projection * a) ** 2 for d, a in zip(delta, axis))) / length
            head = sums["Head"] + sums["HeadAccessory"]
            other = sums["Body"] + sums["Tail"]
            requests.append(f"V {quote(model)} {quote(renderer_path)} {rule['lod']} " +
                            f"{head:.17g} {sums['Neck']:.17g} {other:.17g} {along:.17g} {radius:.17g} 1")
            candidates.append(index)
    result = subprocess.run([str(helper), "--probe-stdin"], input="\n".join(requests) + "\n",
                            encoding="utf-8", capture_output=True, check=True)
    decisions = result.stdout.splitlines()
    if len(decisions) != len(requests):
        raise ValueError("C++ helper response count mismatch")
    region_numbers = {"Body": 0, "Head": 1, "Neck": 2, "Tail": 3, "HeadAccessory": 4}
    for region, decision in zip(regions, decisions[:len(palette)]):
        semantic = 0 if region == "Body" else 2 if region == "Neck" else 1
        if decision != f"{semantic} {region_numbers[region]}":
            raise ValueError("C++ current-path classification differs from native graph")
    for index, decision in zip(candidates, decisions[len(palette):]):
        if decision == "1":
            local.add(index)
        elif decision != "0":
            raise ValueError("invalid C++ vertex classification")
    for triangle in triangles:
        if len(set(triangle)) < 3:
            counts["degenerate_draw_triangles"] += 1
            continue
        original = all(index in generic for index in triangle)
        extra = all(index in accessory for index in triangle)
        final = all(index in accessory or index in local for index in triangle)
        if original:
            counts["generic_head_tail_triangles"] += 1
        if extra and not original:
            counts["added_accessory_triangles"] += 1
        if final and not extra:
            counts["added_local_boundary_triangles"] += 1
        if not final:
            counts["retained_triangles"] += 1
    return dict(mesh=mesh["name"], mesh_id=mesh["id"], draw_triangles=len(triangles),
                referenced_vertices=len(used), local_frame_available=bool(frame),
                actual_hat_paths=sorted(used_hat_paths), **dict(sorted(counts.items())))


def audit(roles_root, manifest, helper):
    output = []
    for model in ROLES:
        profile = next(profile for profile in manifest["profiles"] if profile["model_id"] == model)
        source = profile["sources"][0]  # Visible world LOD0 sample; no UI double counting.
        directory = roles_root / model
        objects, transforms, paths = gen.graph_paths(gen.read_json(directory / "native.json"))
        for renderer in objects.values():
            if renderer["type"] != "SkinnedMeshRenderer":
                continue
            go = objects[renderer["game_object"]["id"]]
            transform = next(ref["id"] for ref in go["components"] if ref.get("id") in transforms)
            full = paths[transform]
            prefix = source["model_root"] + "/Mesh_all/lod0/"
            if not full.startswith(prefix):
                continue
            relative = full[len(source["model_root"]) + 1:]
            palette = [paths[ref["id"]][len(source["model_root"]) + 1:] for ref in renderer["bones"]]
            if any(not paths[ref["id"]].startswith(source["model_root"] + "/") for ref in renderer["bones"]):
                raise ValueError("foreign renderer palette")
            mesh = objects[renderer["mesh"]["id"]]
            rule = next((rule for rule in source["local_rules"] if rule["renderer_path"] == relative), None)
            has_hat = any(bone_region(path, source) == "HeadAccessory" for path in palette)
            if not rule and not has_hat:
                continue
            try:
                result = audit_renderer(mesh, palette, source, directory / "meshes", rule, model, relative, helper)
                result.update(model_id=model, renderer_path=relative, status="checked")
            except (ValueError, KeyError, struct.error) as error:
                result = dict(model_id=model, renderer_path=relative, mesh=mesh["name"],
                              status="unsupported", reason=str(error))
            output.append(result)
    return dict(schema=1, source="Existing Windows native.json graphs and extracted raw VB/IB; world LOD0 only",
                policy_note="Accessory semantics and spatial thresholds are authorized inference/policy. Raw draw/weight counts are measured.",
                cpp_helper_verified=True, android_geometry_verified=False, in_game_verified=False, samples=sorted(output,
                key=lambda sample: (sample["model_id"], sample["renderer_path"])))


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--roles-root", type=Path, default=Path("F:/zmd_bem/research/identities/roles"))
    parser.add_argument("--manifest", type=Path, default=Path(__file__).with_name("profiles.generated.json"))
    parser.add_argument("--output", type=Path, default=Path(__file__).with_name("geometry_regression.generated.json"))
    parser.add_argument("--helper", type=Path, default=gen.REPO /
                        "artifacts/first-person-profiles-tests-20261003/first_person_profiles_tests.exe")
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    if not args.helper.is_file():
        raise SystemExit("Build standalone first_person_profiles_tests first, or specify --helper")
    data = audit(args.roles_root, gen.read_json(args.manifest), args.helper)
    content = json.dumps(data, ensure_ascii=False, indent=2) + "\n"
    if args.check:
        if args.output.read_text(encoding="utf-8") != content:
            raise SystemExit("geometry regression changed")
    else:
        args.output.write_text(content, encoding="utf-8", newline="\n")
    samples = data["samples"]
    print(f"Raw draw regression: {sum(s['status'] == 'checked' for s in samples)} checked; "
          f"{sum(s['status'] != 'checked' for s in samples)} unsupported")


if __name__ == "__main__":
    main()
