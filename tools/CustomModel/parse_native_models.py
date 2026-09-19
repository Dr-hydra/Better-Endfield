"""Build a reference-resolved native character metadata database.

Consumes NativeAssetReader output, never name-joins unrelated objects. Offline
storage layouts are retained separately from any verified runtime observation.
This database is source evidence, not an automatically approved Mod profile.
"""
import argparse
import json
import math
from pathlib import Path


FORMAT_BYTES = (4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4)


def serialized_layout(channels):
    streams, issues, seen = {}, [], set()
    if not channels: return {"channels": [], "streams": [], "issues": ["serialized channels unavailable"]}
    for channel in channels:
        attr, stream, offset, fmt, raw = (channel[k] for k in ("attribute", "stream", "offset", "format", "dimension_raw"))
        if attr in seen: issues.append(f"duplicate channel {attr}")
        seen.add(attr)
        if raw == 0: continue
        dim = raw & 15
        if not 0 <= attr < 14 or not 0 <= stream < 4 or offset < 0 or not 0 <= fmt < len(FORMAT_BYTES) or not 1 <= dim <= 4:
            issues.append(f"unknown serialized channel {attr}: {channel}"); continue
        if raw != dim: issues.append(f"channel {attr} has HG flags 0x{raw & 240:02x}; runtime meaning needs observation")
        streams.setdefault(stream, []).append((offset, offset + dim * FORMAT_BYTES[fmt], attr))
    result = []
    for stream, ranges in sorted(streams.items()):
        end = 0
        for start, stop, attr in sorted(ranges):
            if start < end: issues.append(f"stream {stream} overlapping channel {attr}")
            if start > end: issues.append(f"stream {stream} has padding/gap before channel {attr}")
            end = max(end, stop)
        result.append({"stream": stream, "minimum_stride": end,
                       "aligned_storage_stride": (end + 3) // 4 * 4})
    return {"channels": channels, "streams": result, "issues": issues,
            "runtime_equivalence_verified": False}


class Graph:
    def __init__(self, raw):
        if raw.get("schema") != 1: raise ValueError("unsupported raw graph schema")
        self.objects = {}
        self.transforms = {}
        for obj in raw["objects"]:
            if obj["id"] in self.objects: raise ValueError(f"duplicate serialized identity: {obj['id']}")
            self.objects[obj["id"]] = obj
            if obj["type"] == "Transform":
                go = obj["game_object"].get("id")
                if go in self.transforms: raise ValueError("multiple transforms for one GameObject")
                self.transforms[go] = obj

    def get(self, pointer, expected=None, nullable=False):
        key = pointer.get("id") if pointer else None
        if key is None:
            if pointer and pointer.get("path_id", "0") != "0": raise ValueError("unresolved external file identity")
            if nullable: return None
            raise ValueError("required reference is null")
        result = self.objects.get(key)
        if result is None: raise ValueError(f"unresolved {expected or 'object'} reference: {key}")
        if expected and result["type"] != expected: raise ValueError(f"reference type mismatch: {key}")
        return result

    def path(self, transform):
        names, ids, seen = [], [], set()
        while transform is not None:
            if transform["id"] in seen or len(seen) >= 256: raise ValueError("transform hierarchy cycle/depth limit")
            seen.add(transform["id"]); ids.append(transform["id"])
            go = self.get(transform["game_object"], "GameObject")
            names.append(go["name"])
            transform = self.get(transform["parent"], "Transform", nullable=True)
        return {"names": list(reversed(names)), "ids": list(reversed(ids)), "path": "/".join(reversed(names))}


def parse(raw, observations=None):
    graph = Graph(raw)
    meshes = {}
    for obj in graph.objects.values():
        if obj["type"] != "Mesh": continue
        errors = []
        subs = obj.get("submeshes", [])
        if not subs or any(s["topology"] != "Triangles" or s["index_count"] <= 0 or s["index_count"] % 3 for s in subs):
            errors.append("unsupported/empty submesh topology")
        poses = obj.get("bindposes", [])
        if not poses or any(len(m) != 16 or any(not math.isfinite(x) for x in m) or not any(m) for m in poses):
            errors.append("invalid bindpose matrix array")
        meshes[obj["id"]] = {"id": obj["id"], "name": obj["name"], "vertex_count": obj["vertex_count"],
                              "asset_paths": obj.get("asset_paths", []),
                              "original_index_count": sum(s["index_count"] for s in subs), "submeshes": subs,
                              "bindposes": poses, "bone_name_hashes": obj.get("bone_name_hashes"),
                              "serialized_layout": serialized_layout(obj.get("serialized_channels", [])),
                              "errors": errors + ([obj["layout_error"]] if obj.get("layout_error") else [])}
    renderers = []
    for obj in graph.objects.values():
        if obj["type"] != "SkinnedMeshRenderer": continue
        result = {"id": obj["id"], "name": None, "path": None, "resource_root": None,
                  "bones": [], "materials": [], "errors": [], "warnings": [], "runtime_layout": None}
        renderers.append(result)
        try:
            go = graph.get(obj["game_object"], "GameObject")
            result["name"] = go["name"]
            transform = graph.transforms.get(go["id"])
            if transform is None: raise ValueError("renderer transform missing")
            path = graph.path(transform)
            result.update(path=path["path"], transform_ids=path["ids"], resource_root=path["names"][0],
                          transform={k: transform[k] for k in ("position", "rotation", "scale")})
            mesh = graph.get(obj["mesh"], "Mesh")
            result["mesh_id"] = mesh["id"]
            result["mesh_name"] = mesh["name"]
            result["original_index_count"] = meshes[mesh["id"]]["original_index_count"]
            result["errors"].extend(meshes[mesh["id"]]["errors"])
            result["root_bone"] = obj.get("root_bone")
            for index, ref in enumerate(obj["bones"]):
                try:
                    bone = graph.get(ref, "Transform")
                    bone_go = graph.get(bone["game_object"], "GameObject")
                    bp = graph.path(bone)
                    result["bones"].append({"index": index, "id": bone["id"], "name": bone_go["name"], "path": bp["path"]})
                except ValueError as exc:
                    result["bones"].append({"index": index, "id": ref.get("id"), "name": None})
                    result["errors"].append(f"bone {index}: {exc}")
            if len(result["bones"]) != len(meshes[mesh["id"]]["bindposes"]):
                result["errors"].append("bones/bindposes count mismatch")
            for slot, ref in enumerate(obj["materials"]):
                material_row = {"slot": slot, "id": ref.get("id"), "textures": []}
                result["materials"].append(material_row)
                try:
                    material = graph.get(ref, "Material")
                    material_row.update(name=material["name"], shader=material["shader"], keywords=material.get("keywords"),
                                        floats=material.get("floats"), ints=material.get("ints"), colors=material.get("colors"))
                    shader = graph.objects.get(material["shader"].get("id"))
                    material_row["shader"] = {**material["shader"], "name": (shader.get("name") or None) if shader else None,
                                              "asset_paths": shader.get("asset_paths", []) if shader else [], "code_parsed": False}
                    # Shader code is intentionally not parsed in this metadata tool.
                    for prop in material["textures"]:
                        texture_row = {"property": prop["property"], "id": prop["texture"].get("id"),
                                       "name": None, "scale": prop["scale"], "offset": prop["offset"], "metadata": None}
                        material_row["textures"].append(texture_row)
                        try:
                            tex = graph.get(prop["texture"], "Texture2D", nullable=True)
                            if tex:
                                texture_row.update(name=tex["name"], metadata={k: tex.get(k) for k in
                                                   ("width", "height", "format", "mips", "sampler")})
                        except ValueError as exc:
                            texture_row["error"] = str(exc)
                            result["errors"].append(f"material {slot}/{prop['property']}: {exc}")
                except ValueError as exc: result["errors"].append(f"material {slot}: {exc}")
            result["warnings"].append("offline layout only; full runtime vertex declaration needs observation")
        except ValueError as exc:
            result["errors"].append(str(exc))
        result["offline_references_complete"] = not result["errors"]
        result["conversion_ready"] = False
    result = {"schema": 1, "kind": "native_character_metadata", "source": {k: raw.get(k) for k in
              ("source", "backend", "backend_patch", "input_directory", "snapshot", "files", "backend_errors")},
              "meshes": meshes, "renderers": renderers, "runtime_verified": False}
    if observations: apply_observations(result, observations)
    result["resources"] = {root: [r["id"] for r in renderers if r["resource_root"] == root]
                           for root in sorted({r["resource_root"] for r in renderers if r["resource_root"]})}
    snapshot = raw.get("snapshot") or {}
    expected = {Path(a["path"]).stem for a in snapshot.get("assets", [])}
    result["missing_resources"] = sorted(expected - set(result["resources"]))
    result["summary"] = {"meshes": len(meshes), "renderers": len(renderers),
                         "offline_complete": sum(r["offline_references_complete"] for r in renderers),
                         "runtime_layout_observed": sum(r["runtime_layout"] is not None for r in renderers)}
    return result


def apply_observations(database, observations):
    if observations.get("schema") != 1 or not observations.get("evidence"):
        raise ValueError("runtime observations require schema=1 and evidence")
    snapshot = database["source"].get("snapshot")
    if snapshot and (observations.get("perforce_cl") != snapshot.get("perforce_cl") or
                     observations.get("manifest_version") != snapshot.get("manifest_version")):
        raise ValueError("runtime observations refer to a different/missing game snapshot")
    seen = set()
    for observation in observations["renderers"]:
        key = observation["id"]
        if key in seen: raise ValueError("duplicate runtime renderer observation")
        seen.add(key)
        matches = [r for r in database["renderers"] if r["id"] == key]
        if len(matches) != 1: raise ValueError("runtime observation identity is absent/ambiguous")
        row = matches[0]
        for field in ("resource_root", "path", "mesh_name", "original_index_count"):
            if observation[field] != row.get(field): raise ValueError(f"stale runtime observation: {field}")
        attributes, strides = observation["attributes"], observation["strides"]
        if len(strides) != 3 or any(type(s) is not int or not 0 < s <= 64 for s in strides):
            raise ValueError("invalid runtime strides")
        if not attributes or [a[0] for a in attributes] != sorted({a[0] for a in attributes}):
            raise ValueError("runtime attributes must be unique and sorted")
        actual = [0, 0, 0]
        for attr, fmt, dim, stream in attributes:
            if not 0 <= attr < 14 or not 0 <= fmt < len(FORMAT_BYTES) or not 1 <= dim <= 4 or not 0 <= stream < 3:
                raise ValueError("invalid runtime vertex descriptor")
            actual[stream] += FORMAT_BYTES[fmt] * dim
        if actual != strides: raise ValueError("runtime descriptor/stride mismatch")
        row["runtime_layout"] = {"attributes": attributes, "strides": strides, "evidence": observations["evidence"]}
        # Layout observations do not establish Mod bone/material semantics.
        row["warnings"] = [w for w in row["warnings"] if not w.startswith("offline layout only")]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("graph", type=Path)
    parser.add_argument("--output", required=True, type=Path)
    parser.add_argument("--observations", type=Path)
    args = parser.parse_args()
    raw = json.loads(args.graph.read_text(encoding="utf-8-sig"))
    observations = json.loads(args.observations.read_text(encoding="utf-8-sig")) if args.observations else None
    result = parse(raw, observations)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    tmp = args.output.with_suffix(args.output.suffix + ".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2, allow_nan=False), encoding="utf-8")
    tmp.replace(args.output)
    print(json.dumps(result["summary"], ensure_ascii=False))
    return 0 if result["renderers"] and not result["missing_resources"] else 2


if __name__ == "__main__":
    raise SystemExit(main())
